using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amygdalab.Core.Contracts.Response
{
    public class ProductHistoryResponse
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string ImageUrl { get; set; }
        public long Quantity { get; set; }
    }
}
