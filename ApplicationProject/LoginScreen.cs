using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using ApplicationProject.Controllers;

namespace ApplicationProject
{
    class LoginScreen
    {
        public void Login()
        {
            Console.WriteLine("Give username to login or press 0 to exit");
            string inputUserName = Console.ReadLine();

            if (inputUserName == "0")
            {
                Console.Clear();
                return;
            }
            Console.Clear();

            Console.WriteLine("Give me the Password or press 0 to exit");
            string inputPassword = Console.ReadLine();
            if (inputPassword == "0")
            {
                Console.Clear();
                return;
            }

            Console.Clear();

            LoginScreen login = new LoginScreen();
            login.Validation(inputUserName, inputPassword);
        }

        public void Validation(string userName, string password)
        {
            LoginScreen login = new LoginScreen();
            
            UserController userController = new UserController();

            List<User> users = userController.RetrieveUsers();

            bool userNameFound = false;
            User userVerified = null;

            foreach (var user in users)
            {
                if (userName == user.UserName)
                {
                    if (password == user.PassWord)
                    {
                        userNameFound = true;
                        userVerified = user;
                    }
                }
            }

            if (!userNameFound)
            {
                Console.WriteLine("Incorrect username or password");
                Thread.Sleep(2000);
                Console.Clear();
                login.Login();
            }
            else
            {
                Console.WriteLine("Correct Credentials");
                Thread.Sleep(2000);
                Actions action = new Actions();
                action.ShowActions(userVerified);
            }
        }
    }
}
