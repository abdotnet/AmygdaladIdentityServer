using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amygdalab.Core.Models
{
    public class ErrorResponse
    {
        public List<ErrorModel> errors { get; set; } = new List<ErrorModel>();
    }
}
