using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korzinka
{
    public class OperationModel
    {
        public string Id { get; set; }
        public string InOrOut { get; set; }
        public List<Product> Products { get; set; }
        public DateTime Time { get; set; }

        public OperationModel(string inOrOut)
        {
            Products = new List<Product>();
            InOrOut = inOrOut;
            Id = "";
        }   
    }
}
