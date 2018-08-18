using System;

namespace Bleatingsheep.Osu.ApiV2b
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string msg, Exception innerException) : base(msg, innerException) { }
    }
}
