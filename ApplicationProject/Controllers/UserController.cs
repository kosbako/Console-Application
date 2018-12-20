using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationProject.Controllers
{
    public class UserController
    {
        public void CreateUser(User user)
        {
            using (var db = new AppContext())
            {       
                db.Users.Add(user);
                db.SaveChanges();                
            }
        }

        public List<User> RetrieveUsers()
        {
            List<User> userTable;
            using (var db = new AppContext())
            {
                userTable = db.Users.ToList();
            }

            return userTable;
        }

        public void DeleteUser(int id)
        {
            using (var db = new AppContext())
            {
                var messages = db.Messages.Where(x => x.Sender.Id == id || x.Receiver.Id == id);
                foreach (var item in messages)
                {
                    db.Messages.Remove(item);
                }
                db.SaveChanges();
                var idToDelete = db.Users.Find(id);
                db.Users.Remove(idToDelete);
                db.SaveChanges();
            }
        }


        public void UpdateUser(User user)
        {
            using (var db = new AppContext())
            {
                var userUpdate = db.Users.Find(user.Id);
                userUpdate.UserName = user.UserName;
                userUpdate.PassWord = user.PassWord;
                userUpdate.Privilege = user.Privilege;
                db.SaveChanges();
            }
        }

        public User RetrieveUserById(int? id)
        {
            using (var db = new AppContext())
            {
                return db.Users.Find(id);
            }
        }
    }
}
