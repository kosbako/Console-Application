using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationProject.Options;

namespace ApplicationProject
{
    class Actions
    {
        public void ShowActions(User userRegistered)
        {            
            string input = string.Empty;            

            do
            {
                MessageOption messageOption = new MessageOption();
                UserOption userOption = new UserOption();
                Console.Clear();
                Console.WriteLine($"Wellcome {userRegistered.UserName}");
                Console.WriteLine("Press 0 to exit");
                Console.WriteLine("Press 1 for create a message");
                Console.WriteLine("Press 2 for view messages");                

                switch (userRegistered.Privilege)
                {                    
                    case Role.SuperAdmin:                                               
                        Console.WriteLine("Press 3 for edit messages");
                        Console.WriteLine("Press 4 for delete messages");
                        Console.WriteLine("Press 5 for create a user");
                        Console.WriteLine("Press 6 for view a user");
                        Console.WriteLine("Press 7 for delete a user");
                        Console.WriteLine("Press 8 for update a user");

                        input = Console.ReadLine();
                        if (input == "1")
                        {
                            messageOption.CreateMessage(userRegistered);
                        }
                        if (input == "2")
                        {                            
                            messageOption.ViewMessage();                            
                        }
                        if (input == "3")
                        {
                            messageOption.EditMessage();
                        }
                        if (input =="4")
                        {
                            messageOption.DeleteMessage(userRegistered);
                        }
                        if (input == "5")
                        {
                            userOption.CreateUser();
                        }
                        if (input == "6")
                        {
                            userOption.ViewUser();
                        }
                        if (input == "7")
                        {
                            userOption.DeleteUser();
                        }
                        if (input == "8")
                        {
                            userOption.UpdateUser();
                        }                        
                        break;
                    case Role.View:
                        input = Console.ReadLine();
                        if (input == "1")
                        {
                            messageOption.CreateMessage(userRegistered);
                        }
                        if (input == "2")
                        {
                            messageOption.ViewMessage();
                        }
                        break;
                    case Role.ViewEdit:
                        Console.WriteLine("Press 3 for edit messages");
                        input = Console.ReadLine();
                        if (input == "1")
                        {
                            messageOption.CreateMessage(userRegistered);
                        }
                        if (input == "2")
                        {
                            messageOption.ViewMessage();
                        }
                        if (input == "3")
                        {
                            messageOption.EditMessage();
                        }
                        break;
                    case Role.ViewEditDelete:
                        Console.WriteLine("Press 3 for edit messages");
                        Console.WriteLine("Press 4 for delete messages");
                        input = Console.ReadLine();
                        if (input == "1")
                        {
                            messageOption.CreateMessage(userRegistered);
                        }
                        if (input == "2")
                        {
                            messageOption.ViewMessage();
                        }
                        if (input == "3")
                        {
                            messageOption.EditMessage();
                        }
                        if (input == "4")
                        {
                            messageOption.DeleteMessage(userRegistered);
                        }                        
                        break;
                    default:
                        break;
                }
            } while (input != "0");

            Console.Clear();
        }
    }
}
