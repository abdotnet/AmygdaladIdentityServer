using Amygdalab.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amygdalab.Data.Entities
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string ImageUrl { get; set; }
        public long Quantity { get; set; }
        public ICollection<ProductHistory> ProductHistories { get; set; } = new List<ProductHistory>();

    }
}
