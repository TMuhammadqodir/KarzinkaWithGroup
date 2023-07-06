using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Korzinka
{
    public class WorkWithFile
    {

        string PathtoWarehouse = "D:\\Korzinka\\Files\\Warehouse.txt";
        string PathToCatalog = "D:\\Korzinka\\Files\\Catalog.txt";
        string PathToHistory = "D:\\Korzinka\\Files\\History.txt";

        public void AddToFile(OperationModel receipt)
        {

            List<List<string>> FileData = GetInfoFromFile();

            receipt.Products.ForEach(product =>
            {

                string name = product.Name;
                string amount = product.Quantity.ToString();
                string unit = product.Unit.ToString();
                decimal price = product.Price;


                bool check = false;
                int ind = 0;

                for (int i = 0; i < FileData.Count; i++)
                {
                    if (FileData[i][0].ToLower() == name.ToLower())
                    {
                        ind = i;
                        check = true;

                        break;
                    }
                }

                if (check)
                {
                    
                    FileData[ind][1] = $"{float.Parse(FileData[ind][1].Split()[0]) + float.Parse(amount.Split()[0])} {FileData[ind][1].Split()[1]}";
                    FileData[ind][2] = $"{price}";
                    FileData[ind][3] = $"{Convert.ToDecimal(FileData[ind][1].Split()[0]) * price}";

                    File.WriteAllText(PathtoWarehouse, $"{FileData[0][0]}|{FileData[0][1]}|{FileData[0][2]}|{FileData[0][3]}\n");

                    for (int i = 1; i < FileData.Count; i++)
                    {
                        File.AppendAllText(PathtoWarehouse, $"{FileData[i][0]}|{FileData[i][1]}|{FileData[i][2]}|{FileData[i][3]}\n");

                    }

                }
                else
                {
                    amount += $" {unit}";
                    File.AppendAllText(PathtoWarehouse, $"{name}|{amount}|{price}|{price * Convert.ToDecimal(amount.Split()[0])}\n");
                    AddToCatalog(name, unit);
                }


                File.AppendAllText(PathToHistory, $"{name}|+{amount}|{price}|{price * Convert.ToDecimal(amount.Split()[0])}|{receipt.Time}\n");

            });
        }

        public void AddToCatalog(string name, string unit)
        {

            File.AppendAllText(PathToCatalog, $"{name}|{unit}\n");
        }

        public List<Product> GetAllProductData()
        {

            try
            {
                var file = File.ReadAllLines(PathToCatalog).ToList();


                List<Product> result = new List<Product>();

                file.ForEach(p =>
                {

                    //Console.WriteLine(p);

                    string[] l = p.Split('|');

                    Product product = new Product(l[0], l[1]);

                    result.Add(product);
                }
                );
                return result;
            }
            catch
            {

                return new List<Product>();
            }


        }

        public List<List<string>> GetInfoFromFile()
        {

            try
            {
                var file = File.ReadAllText(PathtoWarehouse).Split("\n");

                List<List<string>> result = new List<List<string>>();

                for (int i = 0; i < file.Length - 1; i++)
                {
                    var line = file[i].Split("|");

                    result.Add(new List<string>() { line[0], line[1], line[2], line[3] });

                }

                return result;

            }
            catch
            {
                //Console.WriteLine("File not found");

                return new List<List<string>>();
            }

        }

        public List<Product> GetAllProductsFromFile()
        {

            try
            {
                var file = File.ReadAllText(PathtoWarehouse).Split("\n");

                List<Product> result = new List<Product>();

                for (int i = 0; i < file.Length - 1; i++)
                {
                    var line = file[i].Split("|");

                    string name = line[0][..1].ToUpper() + line[0][1..].ToLower();

                    Product product = new Product(name, line[1].Split()[1], Convert.ToDecimal(line[2]), Convert.ToDecimal(line[1].Split()[0]));

                    result.Add(product);

                }

                return result;

            }
            catch
            {
                File.WriteAllText(PathtoWarehouse, "");

                return new List<Product>();
            }

        }

        public void GetProductFromFile(OperationModel sale)
        {

            List<List<string>> FileData = GetInfoFromFile();

            sale.Products.ForEach(p =>
            {
                string name = p.Name;
                string amount = $"{p.Quantity}";


                bool check = false;
                int ind = 0;

                for (int i = 0; i < FileData.Count; i++)
                {
                    if (FileData[i][0].ToLower() == name.ToLower())
                    {
                        ind = i;
                        check = true;

                        break;
                    }
                }

                if (check)
                {
                    if (float.Parse(FileData[ind][1].Split()[0]) > float.Parse(amount.Split()[0]))
                    {
                        DateTime dateTime1 = sale.Time;

                        File.AppendAllText(PathToHistory, $"{name}|-{amount.Split()[0]} {FileData[ind][1].Split()[1]}|{FileData[ind][2]}|{Convert.ToDecimal(FileData[ind][2]) * Convert.ToDecimal(amount.Split()[0])}|{dateTime1}\n");

                        FileData[ind][1] = $"{float.Parse(FileData[ind][1].Split()[0]) - float.Parse(amount.Split()[0])} {FileData[ind][1].Split()[1]}";
                        FileData[ind][3] = $"{Convert.ToDecimal(FileData[ind][1].Split()[0]) * Convert.ToDecimal(FileData[ind][2])}";

                        File.WriteAllText(PathtoWarehouse, $"{FileData[0][0]}|{FileData[0][1]}|{FileData[0][2]}|{FileData[0][3]}\n");

                        for (int i = 1; i < FileData.Count; i++)
                        {
                            File.AppendAllText(PathtoWarehouse, $"{FileData[i][0]}|{FileData[i][1]}|{FileData[i][2]}|{FileData[i][3]}\n");

                        }

                        DateTime dateTime2 = sale.Time;


                        //return true;
                    }
                    else if (float.Parse(FileData[ind][1].Split()[0]) == float.Parse(amount.Split()[0]))
                    {


                        File.AppendAllText(PathToHistory, $"{name}|-{amount.Split()[0]} {FileData[ind][1].Split()[1]}|{FileData[ind][2]}|{Convert.ToDecimal(FileData[ind][2]) * Convert.ToDecimal(amount.Split()[0])}|{sale.Time}\n");

                        FileData.RemoveAt(ind);

                        if (FileData.Count > 0)
                        {
                            File.WriteAllText(PathtoWarehouse, $"{FileData[0][0]}|{FileData[0][1]}|{FileData[0][2]}|{FileData[0][3]}\n");

                            for (int i = 1; i < FileData.Count; i++)
                            {
                                File.AppendAllText(PathtoWarehouse, $"{FileData[i][0]}|{FileData[i][1]}|{FileData[i][2]}|{FileData[i][3]}\n");

                            }

                        }
                        else
                        {
                            File.WriteAllText(PathtoWarehouse, "");
                        }

                        //return true;
                    }

                }


                //return false;
            });
        }

        public List<List<string>> GetIstoryOfWarehouse()
        {

            try
            {
                var file = File.ReadAllText(PathToHistory).Split("\n");

                List<List<string>> result = new List<List<string>>();

                for (int i = 0; i < file.Length - 1; i++)
                {

                    result.Add(new List<string>() { file[i] });

                }

                return result;

            }
            catch
            {

                return new List<List<string>>();
            }

        }


    }
}

