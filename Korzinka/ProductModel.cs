using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korzinka
{
    public class ProductModel
    {
        public string Name { get; set; }

        public string Unit { get; set; }

        public decimal Price { get; set; }

        public ProductModel(string name, string unit, decimal price)
        {
            Name = name;
            Unit = unit;
            Price = price;
        }

    }
}
