using System;
using System.Collections.Generic;
using System.Text;

namespace Bleatingsheep.Osu.ApiV2
{
    public class NetworkFailException : ApplicationException
    {
        public NetworkFailException() { }
        public NetworkFailException(string message) : base(message) { }
        public NetworkFailException(string msg, Exception innerException) : base(msg, innerException) { }
    }
}
