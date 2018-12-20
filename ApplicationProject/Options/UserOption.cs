using ApplicationProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationProject.Options
{
    public class UserOption
    {
        public void CreateUser()
        {
            string userName;
            bool value1 = true;
            do
            {
                Console.Clear();
                do
                {
                    Console.WriteLine("Give the username or press 0 to exit");
                    userName = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(userName));
                
                if (userName == "0") return;
                

                UserController userController = new UserController();
                List<User> user = null;
                List<string> usernames = null;
                user = userController.RetrieveUsers();
                usernames = user.Select(x => x.UserName).ToList();
                if (usernames.Contains(userName))
                {
                    Console.WriteLine("The username that you choose already exists, please give another one");
                    Thread.Sleep(3000);
                }
                else
                {
                    value1 = false;
                }
                
            } while (value1);

            string passWord;
            do
            {
                Console.WriteLine("Give the passWord or press 0 to exit");
                passWord = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(passWord));
            
            if (passWord == "0") return;
            bool value = true;
            Role role;

            do
            {
                int number2;
                string input;
                
                
                Console.WriteLine("Give the privilege: Press 1 for view, 2 for viewEdit, 3 for viewEditDelete or press 0 to exit");
                input = Console.ReadLine();
                

                bool success = int.TryParse(input, out number2);
                role = (Role)number2;
                if (success)
                {
                    if (number2 == 0)
                    {
                        return;
                    }
                    
                    if ((int)role == 1 || (int)role == 2 || (int)role == 3)
                    {
                        value = false;
                    }
                }
                
            } while (value);

            value = true;
            int number;

            do
            {
                Console.WriteLine($"The following information  will be stored:\n userName = {userName}, password = {passWord}, role = {role}," +
                $" \n Are you sure?\n Press 1 if you accept, 2 if you want to give the above again or 0 if you want to return back");
                bool success = int.TryParse(Console.ReadLine(), out number);
                if (success)
                {
                    if (number == 0)
                    {
                        return;
                    }
                    if (number == 1 || number == 2)
                    {
                        value = false;
                    }
                }                
            } while (value);

            if (number == 1)
            {
                UserController userController = new UserController();
                User user = new User() { UserName = userName, PassWord = passWord, Privilege = role };

                try
                {
                    userController.CreateUser(user);
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine("The User created");
                    Console.WriteLine("Press something to return");
                    Console.ReadKey();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.EntityValidationErrors.FirstOrDefault().ValidationErrors.FirstOrDefault().ErrorMessage);
                    Console.WriteLine("Press Something to continue");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Press Something to continue");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.Clear();
                CreateUser();
            }
        }

        public void DeleteUser()
        {
            Console.Clear();
            UserController userController = new UserController();
            
            List<User> users = null;
            try
            {
                users = userController.RetrieveUsers();
                users = users.Where(p => p.Privilege != Role.SuperAdmin).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Application Exception {ex.Message}");
                Console.WriteLine("Press something to return");
                Console.ReadKey();
                return;
            }            

            if (users.Count == 0)
            {
                Console.WriteLine("There are no users to delete");
                Console.WriteLine("Press something to return");
                Console.ReadKey();
                return;
            }

            foreach (var user in users)
            {
                Console.WriteLine($"Id: {user.Id}, username: {user.UserName}");
            }

            Console.WriteLine(Environment.NewLine);
            List<int> listOfIds = users.Select(p => p.Id).ToList();
            bool value = true;
            
            do
            {
                Console.WriteLine("Give the Id for the user you want to be deleted or Press 0 to return back");
                int number;
                bool success = int.TryParse(Console.ReadLine(), out number);
                if (success)
                {
                    if (number == 0)
                    {
                        return;
                    }
                    if (listOfIds.Contains(number))
                    {
                        bool value1 = true;
                        int number1;
                        do
                        {
                            Console.WriteLine($"You choose to delete the user with Id: {number}, are you sure ? Press 1 for yes or 0 for no and return back");
                            bool succeed = int.TryParse(Console.ReadLine(), out number1);
                            if (succeed)
                            {
                                if (number1 == 0)
                                {
                                    return;
                                }
                                if (number1 == 1)
                                {
                                    value1 = false;
                                }
                            }
                        } while (value1);

                        try
                        {
                            userController.DeleteUser(number);
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine($"Application Exception {ex.Message}");
                            Console.WriteLine("Press something to return");
                            Console.ReadKey();
                            return;
                        }

                        value = false;
                    }
                }                
            } while (value);
        }

        public void UpdateUser()
        {
            Console.Clear();
            UserController userController = new UserController();
            List<User> users = null;
            try
            {
                users = userController.RetrieveUsers();
                users = users.Where(p => p.Privilege != Role.SuperAdmin).ToList();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Application Exception {ex.Message}");
                Console.WriteLine("Press something to return");
                Console.ReadKey();
                return;
            }            

            if (users.Count == 0)
            {
                Console.WriteLine("There are no users to update");
                Console.WriteLine("Press something to return");
                Console.ReadKey();
                return;
            }

            foreach (var user in users)
            {
                Console.WriteLine($"Id: {user.Id}, username: {user.UserName}, passWord: {user.PassWord}, Role: {user.Privilege}");
            }

            bool value = true;
            int checkId;
            Console.WriteLine(Environment.NewLine);
            List<int> listOfIds = users.Select(p => p.Id).ToList();

            do
            {
                Console.WriteLine("Choose the Id of the user you want to update or press 0 to return back");
                bool success = int.TryParse(Console.ReadLine(), out checkId);
                if (success)
                {
                    if (checkId == 0)
                    {
                        return;
                    }
                    if (listOfIds.Contains(checkId))
                    {
                        value = false;
                    }
                }                
            } while (value);

            User userToUpdate = users.Where(p => p.Id == checkId).FirstOrDefault();
            Console.Clear();
            Console.WriteLine($"You choose to update the following user:");
            Console.WriteLine($"Username : {userToUpdate.UserName}");
            Console.WriteLine($"Password: {userToUpdate.PassWord}");
            Console.WriteLine($"Role: {userToUpdate.Privilege}");
            Console.WriteLine(Environment.NewLine);
            int number;
            value = true;

            do
            {                
                Console.WriteLine("Press 1 for update username, press 2 for update password, press 3 for update Role or press 0 to return back");
                bool success = int.TryParse(Console.ReadLine(), out number);
                if (success)
                {
                    if (number == 0)
                    {
                        return;
                    }
                    if (number == 1 || number == 2 || number == 3)
                    {
                        value = false;
                    }
                }                
            } while (value);

            Console.Clear();
            switch (number)
            {
                case 1:
                    Console.WriteLine($"The old value is {userToUpdate.UserName}, give the new value or press 0 to exit ");
                    string input = Console.ReadLine();
                    if (input == "0") return;

                    userToUpdate.UserName = input;
                    try
                    {
                        userController.UpdateUser(userToUpdate);
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                    {

                        Console.Clear();
                        Console.WriteLine(ex.EntityValidationErrors.FirstOrDefault().ValidationErrors.FirstOrDefault().ErrorMessage);
                        Console.WriteLine("Press Something to continue");
                        Console.ReadKey();
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine($"Application Exception {ex.Message}");
                        Console.WriteLine("Press something to return");
                        Console.ReadKey();
                        return;
                    }
                    
                    break;
                case 2:
                    Console.WriteLine($"The old value is {userToUpdate.PassWord}, give the new value or press 0 to exit ");
                    string input1 = Console.ReadLine();
                    if (input1 == "0") return;

                    userToUpdate.PassWord = input1;
                    try
                    {
                        userController.UpdateUser(userToUpdate);
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                    {

                        Console.Clear();
                        Console.WriteLine(ex.EntityValidationErrors.FirstOrDefault().ValidationErrors.FirstOrDefault().ErrorMessage);
                        Console.WriteLine("Press Something to continue");
                        Console.ReadKey();
                        return;
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine($"Application Exception {ex.Message}");
                        Console.WriteLine("Press something to return");
                        Console.ReadKey();
                        return;
                    }
                    break;
                case 3:
                    int number1;
                    value = true;

                    do
                    {
                        Console.WriteLine($"The old value is {userToUpdate.Privilege}, give the new value \nPress 1 for view, 2 for viewEdit, 3 for viewEditDelete or 0 to exit ");
                        bool success = int.TryParse(Console.ReadLine(), out number1);
                        if (number1 == 0) return;
                        if (number1 == 1 || number1 == 2 || number1 == 3)
                        {
                            value = false;
                        }
                    } while (value);
                    Role input2 = (Role)number1;
                    userToUpdate.Privilege = input2;
                    try
                    {
                        userController.UpdateUser(userToUpdate);
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.EntityValidationErrors.FirstOrDefault().ValidationErrors.FirstOrDefault().ErrorMessage);
                        Console.WriteLine("Press Something to continue");
                        Console.ReadKey();
                        return;
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine($"Application Exception {ex.Message}");
                        Console.WriteLine("Press something to return");
                        Console.ReadKey();
                        return;
                    }
                    break;
            }
        }

        public void ViewUser()
        {
            Console.Clear();
            UserController userController = new UserController();
            List<User> allUsers = null;
            try
            {
                allUsers = userController.RetrieveUsers().Where(x => x.Privilege != Role.SuperAdmin).ToList();             
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Application Exception {ex.Message}");
                Console.WriteLine("Press something to return");
                Console.ReadKey();
                return;
            }

            if(allUsers.Count == 0)
            {
                Console.WriteLine("There are no Users in the Database");
            }

            foreach (var user in allUsers)
            {
                Console.WriteLine($"username: {user.UserName}, password: {user.PassWord}, Role: {user.Privilege}");
            }

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Press something from keyboard to exit");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
