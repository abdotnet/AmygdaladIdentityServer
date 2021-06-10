using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Amygdalab.Web.Controllers.V1
{
    public class BaseController : ControllerBase
    {
        public string TraceId
        {
            get
            {
                return Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            }
        }
       
    }
}
