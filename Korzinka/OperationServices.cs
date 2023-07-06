using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korzinka
{
    public class OperationServices
    {
        WorkWithFile file = new WorkWithFile();

        public OperationModel Receipt(List<Product> products)
        {
            int count = 1;
            foreach (var i in products)
            {
                Console.WriteLine($"{count++}. {i.Name}");
            }
            OperationModel receipt = new OperationModel("in");

            Console.WriteLine("Tovar qo'shish (+): ");

            while (true)
            {
                //Console.Clear();
                Console.Write(">>> ");

                string choice = Console.ReadLine();
                int choose = 0;

                var product = products[0];

                if (choice == "+")
                {
                    Console.Write("Nomi: "); string name = Console.ReadLine();

                    foreach(var item in products)
                    {
                        if (item.Name.ToLower() == name.ToLower())
                        {
                            product = item;
                            goto sur;
                        }
                    }

                    Console.Write("Birligi: "); string unit = Console.ReadLine();
                    file.AddToCatalog(name, unit);
                }
                else
                {
                    
                    choose = int.Parse(choice)% products.Count();
                }
                if (choose == 0)
                {
                    break;
                }

                product = products[choose - 1];

            sur:
                Console.Write("Miqdori: "); decimal quantity = decimal.Parse(Console.ReadLine());
                Console.Write("Narxi: "); decimal price = decimal.Parse(Console.ReadLine());
                product.Quantity = quantity;
                product.Price = price;

                receipt.Products.Add(product);

            }
            receipt.Time = DateTime.Now;
            receipt.Id = Output.GenerateId();
            return receipt;
        }

        public OperationModel Sale(List<Product> products)
        {

            OperationModel sale = new OperationModel("out");
            int count = 1;
            foreach (var i in products)
            {
                Console.WriteLine($"{count++}. {i.Name} --> {i.Price}");
            }
            while (true)
            {

                Console.Write("Choose : ");

                int choose = int.Parse(Console.ReadLine());

                if (choose == 0)
                {
                    break;
                }

            qaytar:
                Console.Write("Quantity : ");
                decimal quantity = decimal.Parse(Console.ReadLine());

                var product = products[choose - 1];

                if (product.Quantity < quantity)
                {
                    Console.WriteLine("Mahsulot yetarli emas ");
                    goto qaytar;
                }

                product.Quantity = quantity;
                product.Price *= 1.2m;

                sale.Products.Add(product);

            }

            sale.Time = DateTime.Now;
            sale.Id = Output.GenerateId();
            return sale;

        }



    }
}
