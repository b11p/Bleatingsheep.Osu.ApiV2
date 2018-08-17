using System;
using System.Collections.Generic;
using System.Text;

namespace Bleatingsheep.Osu.ApiV2
{
    public class BadDataException : ApplicationException
    {
        public BadDataException() { }
        public BadDataException(string message) : base(message) { }
        public BadDataException(string msg, Exception innerException) : base(msg, innerException) { }
    }
}
