using System;

namespace Bleatingsheep.Osu.ApiV2b
{
    public class BadDataException : ApplicationException
    {
        public BadDataException() { }
        public BadDataException(string message) : base(message) { }
        public BadDataException(string msg, Exception innerException) : base(msg, innerException) { }
    }
}
