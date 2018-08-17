using System;
using System.Collections.Generic;
using System.Text;

namespace Bleatingsheep.Osu.ApiV2
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string msg, Exception innerException) : base(msg, innerException) { }
    }
}
