using Amygdalab.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amygdalab.Core.Exceptions
{
    public class AlreadyExistException : BaseException
    {
        public AlreadyExistException(string message) : base(message)
        {
            base.Code = Constants.ResponseCodes.AlreadyExist;
            base.httpStatusCode = System.Net.HttpStatusCode.BadRequest;
        }
    }
}
