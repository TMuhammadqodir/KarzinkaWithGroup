using Korzinka;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Korzinka
{
    public class Output
    {

        public static void SetCustomConsoleColor(int red, int green, int blue)
        {
            Console.Write("\u001b[38;2;" + red + ";" + green + ";" + blue + "m");
        }

        public static void ResetConsoleColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }


        OperationServices services = new OperationServices();
        Warehouse warehouse = new Warehouse();
        WorkWithFile file = CommonObjects.file;
        Istoriya history = new Istoriya();

        public void Menu(UserRole role)
        {
            Console.Clear();
            if (role != UserRole.Admin)
            {
                return;
            }

            while (true)
            {
                SetCustomConsoleColor(255,70,70);
                Console.WriteLine($@"
+-------------------------------------------------------+
|                                                       |
|            KORZINKA YANADA YAXSHIROQ                  |
|                                                       |
|                      MENU                             |
|              1.XARID                                  |
|              2.SOTUV                                  |
|              3.TARIX                                  |
|              4.OMBOR QOLDIG'I                         |
|              5.CHIQISH                                |
|                                                       |
|                                                       |
|                                                       |
|                                                       |
+-------------------------------------------------------+
  ");
                Console.ResetColor();
                Console.Write(">>> ");
                int a = int.Parse(Console.ReadLine());

                if (a == 5)
                {
                    Console.WriteLine("xaridingiz uchun tashakkur");
                    break;

                }
                if (a > 5)
                {
                    Console.WriteLine(" siz natogri raqam kirittingiz boshqattan urinib koring");
                }

                switch (a)
                {
                    case 1:
                        var receipt = services.Receipt(file.GetAllProductData());
                        file.AddToFile(receipt);
                        this.BillOfLading(receipt);
                        break;

                    case 2:
                        var sale = services.Sale(file.GetAllProductsFromFile());
                        file.GetProductFromFile(sale);
                        this.Cheque(sale);
                        break;
                    case 3:
                        history.IstoriyaInfo(file.GetIstoryOfWarehouse());
                        break;
                    case 4:
                        this.RemainingProducts();
                        break;

                }

            }
        }

        public void Cheque(OperationModel sale)
        {
            
            //Console.BackgroundColor = ConsoleColor.White;
            //Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n                 KORZINKA.UZ                  ");
            Console.WriteLine("              Odatdagidan yaxshi              ");
            Console.WriteLine(" -------------------------------------------- \n");
            Console.WriteLine($" Sana:                  {sale.Time,      21} ");
            Console.WriteLine($" Chek raqami:                   {sale.Id,13} \n");
            Console.WriteLine(" -------------------------------------------- ");

            Console.WriteLine("\n                   TOVARLAR                   \n");
            
            
            decimal overalSum = 0;
            sale.Products.ForEach(s => 
            {
                //Product product = s.Product;
                string name = s.Name;
                decimal qty = s.Quantity;
                string unit = s.Unit;
                decimal price = s.Price;
                decimal sum = decimal.Round(price * qty, 2);
                overalSum += sum;
                string numbers = $"{qty} {unit} x {price} = {sum}";
                if (name.Length + numbers.Length <= 44)
                {
                    name += "                   ";
                    Console.WriteLine($" {name.Substring(0, 43-numbers.Length)} {numbers}");
                }
                else
                {
                    Console.WriteLine($" {name.Substring(0, Math.Min(name.Length, 44))}");
                    Console.WriteLine($" {numbers,44}");
                }
                Console.WriteLine("                                ------------- ");
            }
            );
            Console.WriteLine(" ============================================ ");
            Console.Write("\n HAMMASI: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{overalSum,35} ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" Shundan QQS: {decimal.Round(overalSum-(overalSum/1.12m),2), 31} \n");
            Console.WriteLine(" -------------------------------------------- ");
            Console.WriteLine("          Xaridingiz uchun tashakkur!         \n");
            
        }


        public void BillOfLading(OperationModel receipt)
        {
            string id = receipt.Id;
            List<Product> products = receipt.Products;
            DateTime dateTime = receipt.Time;


            Console.WriteLine();
            Console.WriteLine("                               KIRIM YUK XATI                             \n");
            Console.WriteLine("+----+----------------------------+----------+-------------+--------------+");
            Console.WriteLine("|  N | Tovar                      |   Soni   |   Narxi     |    Summa     |");
            Console.WriteLine("+----+----------------------------+----------+-------------+--------------+");

            int index = 1;
            foreach (var item in products)
            {
                Console.WriteLine($@"|{index++, 4}|{item.Name,-28}|{item.Quantity,10}|{item.Price,13}|{item.Quantity * item.Price,14}|");

                Console.WriteLine("+----+----------------------------+----------+-------------+--------------+");


            }
        }

        public void RemainingProducts()
        {
            //Console.WriteLine("=========================================================");
            Console.WriteLine("\n                                OMBOR QOLDIG'I HISOBOTI                            \n");
            Console.WriteLine(" +---------------------------------------------+----------+-----------+-------------+ ");
            Console.WriteLine(" |                   Tovar                     |   Soni   |   Narxi   |    Summa    | ");
            Console.WriteLine(" +---------------------------------------------+----------+-----------+-------------+ ");
            decimal overalSum = 0;
            //var remProds = warehouse.GetRemainingProducts();
            var remProds = file.GetAllProductsFromFile();
            remProds.ForEach(p =>
            {
                decimal sum = p.Price * p.Quantity;
                overalSum += sum;
                Console.WriteLine($" | {p.Name+", "+p.Unit, -43} | {p.Quantity, 8} | {p.Price, 9} | {sum, 11} |");
            });
            Console.WriteLine(" +---------------------------------------------+----------+-----------+-------------+ ");
            Console.WriteLine(" |                                                                                  | ");
            Console.WriteLine($" |                Ombordagi jami tovarlar aktivi, so'm                 {overalSum, 12} | ");
            Console.WriteLine(" |                                                                                  | ");
            Console.WriteLine(" +----------------------------------------------------------------------------------+ ");
        }


        public static string GenerateId()
        {
            string hedefFayl = "D:\\Korzinka\\Files\\ids.txt";
            
            var file = File.ReadAllLines(hedefFayl).ToList();
            int id = 0;
            if (file.Count > 0)
            {
                id = int.Parse(file[0]);
            }

            int yangiId = id + 1;
            FaylgaYoz(hedefFayl, yangiId.ToString());

            return $"{yangiId:D8}";
        }

        static void FaylgaYoz(string faylNomi, string matn)
        {
            using (StreamWriter yozuvchi = new StreamWriter(faylNomi))
            {
                yozuvchi.WriteLine(matn);
            }
        }

    }
}


