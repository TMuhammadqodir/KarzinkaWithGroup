using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

//using System.Linq;



namespace Korzinka
{



    public class Catalog
    {


        public string PathToFile = "D:\\Korzinka\\Files\\Warehouse.json";


        public Catalog(string pathToFile)
        {
            //PathToFile = pathToFile;
        }

        public void AddToProducts(Product product)
        {

            var products = this.GetAllProducts();

            bool exists = products.Any(p => p.Name.ToLower() == product.Name.ToLower());

            if (exists) return;

            products.Add(product);

            string write = JsonConvert.SerializeObject(product);

            File.WriteAllText(PathToFile, write);

        }

        public bool ProductNameExists(Product product)
        {
            return this.GetAllProducts().Any(p => p.Name.ToLower() == product.Name.ToLower());
        }

        public Product GetProduct(string productName)
        {

            var product = this.GetAllProducts().FirstOrDefault
            (p =>
                p.Name.ToLower() == productName.ToLower()
            );
            return product;

        }

        public List<Product> GetAllProducts()
        {
            var file = File.ReadAllText(PathToFile);

            var products = JsonConvert.DeserializeObject<List<Product>>(file);

            //if (products != null && products.Count > 0)
            //{
            //    return new List<Product>();
            //}

            return products.OrderBy(p => p.Name).ToList();

        }
    }

    
}
