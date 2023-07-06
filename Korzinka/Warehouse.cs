using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;


namespace Korzinka
{

    public class Warehouse : WarehouseModel
    {

        public List<Product> GetRemainingProducts()
        {
            try
            {
                var file = File.ReadAllText(PathToWarehouseFile);
                var products = JsonConvert.DeserializeObject<List<Product>>(file);
                return products.OrderBy(p => p.Name).ToList();
            }
            catch
            {
                Console.WriteLine("File not found");
                return new List<Product>();
            }
        }

        public void UpdateQuantity(OperationModel operation)
        {
            var remainingProducts = GetRemainingProducts();

            if (operation.InOrOut == "in")
            {
                operation.Products.ForEach(p =>
                {
                    for (int i = 0; i < remainingProducts.Count; i++)
                    {
                        if (remainingProducts[i].Name == p.Name)
                        {
                            remainingProducts[i].Quantity += p.Quantity;
                            break;
                        }
                    }

                });
            }
            else
            {
                operation.Products.ForEach(p =>
                {
                    for (int i = 0; i < remainingProducts.Count; i++)
                    {
                        if (remainingProducts[i].Name == p.Name)
                        {
                            remainingProducts[i].Quantity -= p.Quantity;
                            break;
                        }
                    }

                });
            }

            string write = JsonConvert.SerializeObject(remainingProducts);

            File.WriteAllText(PathToWarehouseFile, write);

        }
        




    }
}
