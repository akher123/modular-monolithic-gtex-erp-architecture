using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.ExceptionHelper
{
    [Serializable]
    public class SoftcodeUnauthorizedException : SoftcodeException
    {
        public SoftcodeUnauthorizedException(string message)
            : base(message, (int)HttpStatusCode.Unauthorized)
        {
        }

        public SoftcodeUnauthorizedException(string format, params object[] args)
            : base(string.Format(format, args), (int)HttpStatusCode.Unauthorized)
        {
        }

    }
}
