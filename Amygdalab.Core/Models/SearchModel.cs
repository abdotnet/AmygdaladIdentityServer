using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amygdalab.Core.Models
{
    public class SearchModel
    {
        public SearchModel()
        {
            PageNumber = PageNumber == 0 ? 1 : PageNumber;
        }

        public long UserId { get; set; }
        private const int MaxPageCount = 50; //{ get; set; }
        public int PageNumber { get; set; } = 1;
        private int pageSize = 50;
        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = (value > MaxPageCount) ? MaxPageCount : value;
            }
        }
    }
}
