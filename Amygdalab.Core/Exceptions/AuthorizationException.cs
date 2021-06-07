using Amygdalab.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amygdalab.Core.Exceptions
{
    public class AuthorizationException : BaseException
    {

        public AuthorizationException(string message) : base(message)
        {
            base.Code = Constants.ResponseCodes.Unauthorized;
            base.httpStatusCode = System.Net.HttpStatusCode.Unauthorized;
        }
    }
}
