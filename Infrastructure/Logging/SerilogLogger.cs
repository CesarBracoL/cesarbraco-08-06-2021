using Microsoft.Extensions.Configuration;
using System;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.AwsCloudWatch;
using Amazon.CloudWatchLogs;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Domain.Common;
using Domain;

namespace Infrastructure.Logging
{
    public class SerilogLogger : IAppLogger
    {
        private ILogger _logger;
        private readonly IConfiguration _configuration;
        private string ApplicationName { get; set; }

        public SerilogLogger(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string _dateTimeFormatUtc = "yyyy-MM-ddTHH:mm:ss.fffzzz";

        public IAppLogger GetLogger()
        {
            return this;
        }

        public IAppLogger SetLogger(string applicationName)
        {
            ApplicationName = applicationName;
            var logGroup = _configuration[string.Concat("Serilog:", ApplicationName)];
            var prefix = DateTime.UtcNow.ToString("yyyy/MM/ddThh.mm.ss");

            var logLevel = _configuration.GetValue<string>("Serilog:MinimumLevel:Default"); // read level from appsettings.json
            if (!Enum.TryParse<LogEventLevel>(logLevel, true, out var level))
            {
                level = LogEventLevel.Information; // or set default value
            }

            var options = new CloudWatchSinkOptions
            {
                LogGroupName = logGroup,
                MinimumLogEventLevel = LogEventLevel.Verbose,
                CreateLogGroup = true,
                LogStreamNameProvider = new DefaultLogStreamProvider(),
                TextFormatter = new CustomJsonFormatter(),
            };

            _logger = new LoggerConfiguration()
                .MinimumLevel.Is(level)
                .Enrich.FromLogContext()
                .CreateLogger();

            Log.Logger = _logger;

            return this;
        }

        public void Trace(string message, params object[] args)
        {
            var logMessage = LogMessage(LogLevel.Trace, message, args);

            Log.Verbose(logMessage);
        }

        public void Debug(string message, params object[] args)
        {
            var logMessage = LogMessage(LogLevel.Debug, message, args);

            Log.Debug(logMessage);
        }

        public void Info(string message, params object[] args)
        {
            var logMessage = LogMessage(LogLevel.Info, message, args);

            Log.Information(logMessage);
        }

        public void Warning(string message, params object[] args)
        {
            var logMessage = LogMessage(LogLevel.Warn, message, args);

            Log.Warning(logMessage);
        }

        public void Error(string message, params object[] args)
        {

            var logMessage = LogMessage(LogLevel.Error, message, args);

            Log.Error(logMessage);
        }

        public void Error(Exception exception, params object[] args)
        {

            var logMessage = LogMessage(LogLevel.Error, exception, exception.Message, args);

            Log.Error(logMessage);
        }

        public void Error(Exception exception, string message, params object[] args)
        {
            var logMessage = LogMessage(LogLevel.Error, exception, message, args);

            Log.Error(logMessage);
        }

        private string LogMessage(LogLevel logLevel, string message = null, params object[] args)
        {
            return LogMessage(logLevel, null, message, args);
        }

        private string LogMessage(LogLevel logLevel, Exception exception = null, string message = null, params object[] args)
        {
            var datetime = Helpers.GetDateTimeUtcLocal().ToStringOrEmptyWithFormat(_dateTimeFormatUtc);
            var levelName = logLevel.ToString().ToUpper();
            var msg = ParseMessage(message);

            var logInfo = new LogInfo
            {
                Timestamp = datetime,
                Level = levelName,
                Message = msg
            };

            var properties = ParseProperties(message, args);

            WriteApplicationProperty(ApplicationName, properties);
            WriteExceptionProperty(exception, properties);

            logInfo.Properties = properties;

            return ParseToJson(logInfo);
        }

        private static Dictionary<string, object> ParseProperties(string message, IReadOnlyList<object> args)
        {
            var properties = new Dictionary<string, object>();
            var propertyNames = ParsePropertyNames(message).ToArray();

            var count = propertyNames.Length;

            if (count > args.Count)
                count = args.Count;

            for (var i = 0; i < count; i++)
            {
                WriteProperty(propertyNames[i], args[i], properties);
            }

            return properties;
        }

        private static IEnumerable<string> ParsePropertyNames(string message)
        {
            var propertyNames = new List<string>();

            if (string.IsNullOrWhiteSpace(message))
                return propertyNames;

            var template = ParseTemplate(message);

            propertyNames = template.Split(new[] { "{", "}" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(prop => prop.Trim())
                .Where(prop => !string.IsNullOrWhiteSpace(prop) && IsValidPropertyName(prop)).ToList();

            return propertyNames;
        }

        private static bool IsValidPropertyName(string name)
        {
            const string pattern = "^[a-zA-Z][a-zA-Z0-9]";
            return Regex.IsMatch(name, pattern);
        }

        private static void WriteProperty(string propertyName, object arg, IDictionary<string, object> properties)
        {
            switch (arg.GetType().ToString())
            {
                case "System.String":
                    var jsonObject = TryParseStringToJson(arg.ToString());
                    properties.Add(propertyName, jsonObject);
                    break;

                default:
                    properties.Add(propertyName, arg);
                    break;
            }
        }

        private static void WriteApplicationProperty(string applicationName, IDictionary<string, object> properties)
        {
            if (string.IsNullOrWhiteSpace(applicationName))
                return;

            properties.Add("application", applicationName);
        }

        private static void WriteExceptionProperty(Exception exception, IDictionary<string, object> properties)
        {
            if (exception == null)
                return;

            var exceptionProperties = new Dictionary<string, object>();

            if (exception.Data.Count != 0)
            {
                exceptionProperties.Add("data", exception.Data);
            }

            if (!string.IsNullOrEmpty(exception.HelpLink))
            {
                exceptionProperties.Add("helpLink", exception.HelpLink);
            }

            if (exception.HResult != 0)
            {
                exceptionProperties.Add("hResult", exception.HResult);
            }

            exceptionProperties.Add("message", exception.Message);
            exceptionProperties.Add("source", exception.Source);
            exceptionProperties.Add("stackTrace", exception.StackTrace);

            if (exception.TargetSite != null)
            {
                exceptionProperties.Add("targetSite", exception.TargetSite.ToString());
            }

            if (exception.InnerException != null)
            {
                exceptionProperties.Add("innerException", exception.InnerException);
            }

            properties.Add(nameof(Exception).ToLowerInvariant(), exceptionProperties);
        }

        private static string ParseTemplate(string message)
        {
            if (message == null)
                return string.Empty;

            var template = message;

            var index = message.IndexOf("{", StringComparison.Ordinal);
            if (index < 0)
                return template;

            template = message.Substring(index);

            return template;
        }

        private static string ParseMessage(string message)
        {
            if (message == null)
                return string.Empty;

            var template = message;

            var index = message.IndexOf("{", StringComparison.Ordinal);
            if (index < 0)
                return template;

            template = message.Substring(0, index - 1);

            return template.Trim();
        }

        public static dynamic TryParseStringToJson(string inputMessage)
        {
            try
            {
                return JsonConvert.DeserializeObject(inputMessage);
            }
            catch (Exception)
            {
                return inputMessage;
            }
        }

        private static string ParseToJson(LogInfo logInfo)
        {
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy
                {
                    OverrideSpecifiedNames = false
                }
            };

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = contractResolver
            };

            return JsonConvert.SerializeObject(logInfo, jsonSerializerSettings);
        }
    }
}
