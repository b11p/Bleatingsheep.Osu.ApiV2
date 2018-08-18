using System;

namespace Bleatingsheep.Osu.ApiV2b
{
    public class AuthorizationFailException : ApplicationException
    {
        public AuthorizationFailException() { }
        public AuthorizationFailException(string message) : base(message) { }
        public AuthorizationFailException(string msg, Exception innerException) : base(msg, innerException) { }
    }
}
