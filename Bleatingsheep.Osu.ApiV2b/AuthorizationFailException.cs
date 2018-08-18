using System;
using System.Collections.Generic;
using System.Text;

namespace Bleatingsheep.Osu.ApiV2
{
    public class AuthorizationFailException : ApplicationException
    {
        public AuthorizationFailException() { }
        public AuthorizationFailException(string message) : base(message) { }
        public AuthorizationFailException(string msg, Exception innerException) : base(msg, innerException) { }
    }
}
