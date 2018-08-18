using System;

namespace Bleatingsheep.Osu.ApiV2b
{
    public class NetworkFailException : ApplicationException
    {
        public NetworkFailException() { }
        public NetworkFailException(string message) : base(message) { }
        public NetworkFailException(string msg, Exception innerException) : base(msg, innerException) { }
    }
}
