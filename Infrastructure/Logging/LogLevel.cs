using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logging
{
    /// <summary>
    /// Defines available log levels.
    /// </summary>
    public class LogLevel
    {
        public static readonly LogLevel Trace = new LogLevel(0, nameof(Trace));
        public static readonly LogLevel Debug = new LogLevel(1, nameof(Debug));
        public static readonly LogLevel Info = new LogLevel(2, nameof(Info));
        public static readonly LogLevel Warn = new LogLevel(3, nameof(Warn));
        public static readonly LogLevel Error = new LogLevel(4, nameof(Error));
        public static readonly LogLevel Fatal = new LogLevel(5, nameof(Fatal));
        public static readonly LogLevel Off = new LogLevel(6, nameof(Off));

        public static IEnumerable<LogLevel> List() => new[] { Trace, Debug, Info, Warn, Error, Fatal, Off };

        private LogLevel(int ordinal, string name)
        {
            Ordinal = ordinal;
            Name = name;
        }

        public int Ordinal { get; }

        public string Name { get; }

        public static LogLevel FromOrdinal(int ordinal)
        {
            var logLevel = List().SingleOrDefault(s => s.Ordinal == ordinal);

            if (logLevel == null)
            {
                throw new ArgumentException("Invalid ordinal.");
            }

            return logLevel;
        }

        public static LogLevel FromString(string levelName)
        {
            var logLevel = List().SingleOrDefault(s => string.Equals(s.Name, levelName, StringComparison.CurrentCultureIgnoreCase));

            if (logLevel == null)
            {
                throw new ArgumentException($"Unknown log level: {levelName}");
            }

            return logLevel;
        }

        public override string ToString() => Name;

        public override int GetHashCode() => Ordinal;
    }
}
