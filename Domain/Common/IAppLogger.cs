using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public interface IAppLogger
    {
        IAppLogger GetLogger();
        IAppLogger SetLogger(string name);

        void Trace(string message, params object[] args);

        void Debug(string message, params object[] args);

        void Info(string message, params object[] args);

        void Warning(string message, params object[] args);

        void Error(string message, params object[] args);

        void Error(Exception exception, params object[] args);

        void Error(Exception exception, string message, params object[] args);
    }
}
