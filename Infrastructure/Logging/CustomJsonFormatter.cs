using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Json;
using System.IO;

namespace Infrastructure.Logging
{
    public class CustomJsonFormatter : ITextFormatter
    {
        readonly JsonValueFormatter _valueFormatter;

        /// <summary>
        /// Construct a <see cref="CompactJsonFormatter"/>, optionally supplying a formatter for
        /// <see cref="LogEventPropertyValue"/>s on the event.
        /// </summary>
        /// <param name="valueFormatter">A value formatter, or null.</param>
        public CustomJsonFormatter(JsonValueFormatter valueFormatter = null)
        {
            _valueFormatter = valueFormatter ?? new JsonValueFormatter(typeTagName: "$type");
        }

        /// <summary>
        /// Format the log event into the output. Subsequent events will be newline-delimited.
        /// </summary>
        /// <param name="logEvent">The event to format.</param>
        /// <param name="output">The output.</param>
        public void Format(LogEvent logEvent, TextWriter output)
        {
            //FormatEvent(logEvent, output, _valueFormatter);
            output.Write(logEvent.MessageTemplate.Text);
            output.WriteLine();
        }
    }
}
