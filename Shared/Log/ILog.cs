using System;

namespace Shared.Log
{
    public interface ILog
    {
        void Log(string p0);

        void Log(Exception p0);
    }
}
