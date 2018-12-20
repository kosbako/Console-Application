using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationProject
{
    class Program
    {
        static void Main(string[] args)
        {            
            LoginScreen login = new LoginScreen();
            string input;
            do
            {
                Console.WriteLine("Press 0 to exit the program");
                Console.WriteLine("Press 1 to open the Login Screen");
                input = Console.ReadLine();
                Console.Clear();
                if (input == "1")
                {
                    login.Login();
                }
            } while (input != "0");            
        }
    }
}
