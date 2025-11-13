using System;
using System.Net;

namespace Softcode.GTex.ExceptionHelper
{
    [Serializable]
    public class SoftcodeForbiddenException : ItmException
    {
        public SoftcodeForbiddenException(string message)
            : base(message, (int)HttpStatusCode.Forbidden)
        {
        }

        public SoftcodeForbiddenException(string format, params object[] args)
            : this(string.Format(format, args))
        {
        }
    }
}
