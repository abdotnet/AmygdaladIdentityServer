using Amygdalab.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amygdalab.Core.Exceptions
{
   public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message)
        {
            base.Code = Constants.ResponseCodes.NotFound;
            base.httpStatusCode = System.Net.HttpStatusCode.NotFound;
        }
    }
}
