using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korzinka
{

    public class Security
    {
        string Fayl = "D:\\Korzinka\\Files\\Security.bin";
        int tries = 3;

        public UserRole? Role()
        {
            UserModel user = new UserModel();
            user.Role = UserRole.Admin;
            bool red = false;

            while (!red && tries > 0)
            {
                if (File.Exists(Fayl))
                {
                    using (FileStream fs = File.OpenRead(Fayl))
                    {
                        BinaryReader br = new BinaryReader(fs);
                        user.Login = br.ReadString();
                        user.Password = br.ReadString();
                    }
                }
                else
                {
                    this.AddNewUser();
                }

                red = this.CheckUser(user);
                tries--;

            }
            if (tries == 0)
            {
                Console.WriteLine("Urinishlaringiz muvaffaqqiyatsiz ketdi.");
                return null;
            }
            return user.Role;
        }

        public void AddNewUser()
        {
            UserModel user = new UserModel();
            Console.Clear();
            Output.SetCustomConsoleColor(255, 255, 255);
            Console.WriteLine("***********************************");
            Console.WriteLine("* Yangi login va parol o'rnating: *");
            Console.WriteLine("***********************************");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.Write("Login: "); user.Login = Console.ReadLine();
            Console.Write("Parol: "); user.Password = Console.ReadLine();
            Console.WriteLine("Rol: (admin, menejer, kassir; default = kassir)"); string role = Console.ReadLine();
            user.Role = role[..1].ToLower() == "a" ? UserRole.Admin : role[..1].ToLower() == "m"? UserRole.Manager : UserRole.Cashier;

            Console.ResetColor();

            using (FileStream fs = File.OpenWrite(Fayl))
            {
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(user.Login);
                bw.Write(user.Password);
                bw.Write(user.Role.ToString());
                Console.WriteLine();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;


                Console.WriteLine("+--------------------------------------------+");
                Console.WriteLine("| Yangi login va parolingiz faylga saqlandi! |");
                Console.WriteLine("+--------------------------------------------+");

                Console.WriteLine();
                Console.ResetColor();
            }
            
        }

        public bool CheckUser(UserModel user)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();
            //Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++");
            //Console.WriteLine("| Kirish uchun Login va parolingizni kiriting: |");
            //Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++");
            Output.SetCustomConsoleColor(150,255,150);
            Console.Write("Login: "); string userInputLogin = Console.ReadLine();
            Console.Write("Parol: "); 

            if (this.CheckPassword(user.Password) && userInputLogin == user.Login)
            {
                Console.Clear();
                Console.WriteLine("===================");
                Console.WriteLine("|  Xush kelibsiz  |");
                Console.WriteLine("===================");
                Thread.Sleep(1500);
                return true;
            }
            Console.Clear();
            Output.SetCustomConsoleColor(255, 70, 70);
            if (tries > 1)
            {
                Console.WriteLine("::::::::::::::::::::::::::::::::::::::::::::::::::::");
                Console.WriteLine("| Login yoki parol noto'g'ri! Qayta urinib ko'ring |");
                Console.WriteLine("::::::::::::::::::::::::::::::::::::::::::::::::::::\n");
                Console.ResetColor();
                Thread.Sleep(1500);
            }
            return false;
        }

        public bool CheckPassword(string userPassword)
        {

            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Enter)
                {
                    if (key.Key == ConsoleKey.Backspace)
                    {
                        if (password.Length > 1)
                        {
                            password = password[..(password.Length - 1)];
                        }
                    }
                    else
                    {
                        password += key.KeyChar;
                    }
                    
                }
                else
                {
                    break;
                }
            }
            while (true);

            return userPassword == password;

        }
    }
}