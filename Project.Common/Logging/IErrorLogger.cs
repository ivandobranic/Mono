using System;

namespace Project.Common.Logging
{
    public interface IErrorLogger
    {
        void LogError(Exception ex);
    }
}
