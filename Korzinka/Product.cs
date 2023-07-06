using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korzinka
{
    public class Product : ProductModel
    {

        public decimal Quantity { get; set; }

        public Product(string name, string unit, decimal price = 0, decimal quantity = 0) : base(name, unit, price)
        {
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"{Name}, {Unit}";
        }

    }
}
