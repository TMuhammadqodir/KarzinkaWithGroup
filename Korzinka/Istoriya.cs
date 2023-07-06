using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korzinka
{
    public class Istoriya
    {
        public void IstoriyaInfo(List<List<string>> str)
        {
            Output outp = new Output();

            List<string> name_in = new List<string>();
            List<int> count_in = new List<int>();
            List<int> price_in = new List<int>();
            List<string> data_in = new List<string>();

            List<string> name_out = new List<string>();
            List<int> count_out = new List<int>();
            List<int> price_out = new List<int>();
            List<string> data_out = new List<string>();

            foreach (List<string> item in str)
            {
                foreach (string s in item)
                {
                    string[] info = s.Split("|");

                    if (info[1].Contains('-'))
                    {
                        int n1 = int.Parse(info[2]);
                        int n2 = int.Parse(info[1].Split()[0]);

                        name_out.Add(info[0]);
                        count_out.Add(n2);
                        price_out.Add(n1);
                        data_out.Add(info[4]);
                    }
                    else
                    {
                        int n1 = int.Parse(info[2]);
                        int n2 = int.Parse(info[1].Split()[0]);

                        name_in.Add(info[0]);
                        count_in.Add(n2);
                        price_in.Add(n1);
                        data_in.Add(info[4]);

                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("                                           OUTPUT PRODUCT                                               ");
            Console.WriteLine("+----+----------------------+----------+-------------+--------------+----------------------------------+ ");
            Console.WriteLine("| N  |     Product Name     |   Count  |    Price    |    Summa     |               Date               | ");
            Console.WriteLine("+----+----------------------+----------+-------------+--------------+----------------------------------+ ");

            int index = 0;
            int summa1 = 0;
            for (int i = data_out.Count - 1; i >= 0; i--)
            {
                index++;

                int sum = Math.Abs(count_out[i] * price_out[i]);
                summa1 += sum;

                Console.WriteLine($@"|{index,4}|{name_out[i],-22}|{Math.Abs(count_out[i]),10}|{price_out[i],13}|{sum,14}|{data_out[i],32}||");

                Console.WriteLine("+----+----------------------+----------+-------------+--------------+----------------------------------+");
            }
            string l6 = "                                                  ";
            l6 = l6[..(49 - summa1.ToString().Length)];
            Console.WriteLine($"|                  TOTAL AMOUNT                      |{l6}{summa1}|");
            Console.WriteLine("+----------------------------------------------------+-------------------------------------------------+");


            //Console.ForegroundColor = ConsoleColor.Green;
            Output.SetCustomConsoleColor(150, 255, 150);

            Console.WriteLine("\n\n");
            Console.WriteLine("                                            INPUT PRODUCT                                               ");
            Console.WriteLine("+----+----------------------+----------+-------------+--------------+----------------------------------+");
            Console.WriteLine("| N  |     Product Name     |   Count  |    Price    |    Summa     |               Date               |");
            Console.WriteLine("+----+----------------------+----------+-------------+--------------+----------------------------------+");

            index = 0;
            int summa2 = 0;

            for (int i = data_in.Count - 1; i >= 0; i--)
            {
                index++;
                string l1 = "                       ";
                string l2 = "          ";
                string l3 = "              ";
                string l4 = "               ";
                string l5 = "                                     ";

                int c = name_in[i].Length;
                string c1 = count_in[i].ToString();
                string c2 = price_in[i].ToString();

                l1 = l1[..(22 - c)];
                l2 = l2[..(10 - c1.Length)];
                l3 = l3[..(13 - c2.Length)];

                int sum = count_in[i] * price_in[i];
                summa2 += sum;
                string c3 = sum.ToString();

                l4 = l4[..(14 - c3.Length)];
                l5 = l5[..(34 - data_in[i].Length)];

                Console.WriteLine($@"|{index,4}|{name_in[i]}{l1}|{l2}{count_in[i]}|{l3}{price_in[i]}|{l4}{sum}|{l5}{data_in[i]}|");

                Console.WriteLine("+----+----------------------+----------+-------------+--------------+----------------------------------+");


            }
            l6 = "                                                  ";
            l6 = l6[..(49 - summa2.ToString().Length)];
            Console.WriteLine($"|                    TOTAL AMOUNT                    |{l6}{summa2}|");
            Console.WriteLine("+----------------------------------------------------+-------------------------------------------------+");

            int l23 = int.Parse(Console.ReadLine());
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n");












        }


        /*public void SearchHistory(List<List<string>> str)
        {
            string sana, vaqt;

            Console.Write("Qidirilayotgan kunni kiriting (format 23-May): ");
            sana=Console.ReadLine();
            Console.Write("Qidirilayotgan vaqtni kiritning (format-00:00:00): ");
            vaqt=Console.ReadLine();

            foreach(var s in str)
            {
                foreach (var v in s)
                {
                    if (sana.Contains(v) && vaqt.Contains(v))
                    {
                        Console.WriteLine();
                    }
                }
            }


        }*/
    }
}
