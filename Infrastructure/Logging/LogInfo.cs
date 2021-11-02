using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Logging
{
    public class LogInfo
    {
        public string Timestamp { get; set; }

        public string Level { get; set; }

        public string Message { get; set; }

        public Dictionary<string, object> Properties { get; set; }
    }
}
