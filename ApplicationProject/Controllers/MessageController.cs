using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationProject.Controllers
{
    public class MessageController
    {

        public List<Message> RetrieveAllMessages()
        {
            List<Message> messageTable;
            using (var db = new AppContext())
            {
                messageTable = db.Messages.Include("Sender").Include("Receiver").ToList();
            }

            return messageTable;
        }

        public List<Message> RetrieveMessagesBySender(int senderId)
        {
            List<Message> messagesBySender;
            using (var db = new AppContext())
            {
                messagesBySender = db.Messages.Include("Sender").Include("Receiver").Where(p => p.SenderId == senderId).ToList();
            }

            return messagesBySender;
        }

        public List<Message> RetrieveMessagesByReceiver(int receiverId)
        {
            List<Message> messagesByReceiver;
            using (var db = new AppContext())
            {
                messagesByReceiver = db.Messages.Include("Sender").Include("Receiver").Where(p => p.ReceiverId == receiverId).ToList();
            }

            return messagesByReceiver;
        }

        public Message RetrieveMessageById(int id)
        {
            List<Message> messageById;
            using (var db = new AppContext())
            {
                messageById = db.Messages.Include("Sender").Include("Receiver").Where(p => p.MessageId == id).ToList();
            }

            return messageById.FirstOrDefault();
        }

        public void CreateMessage(Message message)
        {
            using (var db = new AppContext())
            {
                db.Messages.Add(message);
                db.SaveChanges();
                Logger logger = new Logger();
                logger.LogCreate(message);
            }
        }

        public void DeleteMessage(int id)
        {
            using (var db = new AppContext())
            {
                var idToDelete = db.Messages.Find(id);
                db.Messages.Remove(idToDelete);
                db.SaveChanges();
            }
        }
        

        public void UpdateMessage(Message message)
        {
            using (var db = new AppContext())
            {                
                var messageUpdate = db.Messages.Find(message.MessageId);
                string oldMessageData = messageUpdate.MessageData;
                messageUpdate.MessageData = message.MessageData;
                db.SaveChanges();
                Logger logger = new Logger();
                logger.LogUpdate(message, oldMessageData);
            }
        }
    }
}
