using ApplicationProject.Controllers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ApplicationProject.Options
{
    public class MessageOption
    {
        public void ViewMessage()
        {
            Console.Clear();
            Message message = new Message();
            MessageController messageController = new MessageController();
            var getAllMessages = messageController.RetrieveAllMessages();

            foreach (var item in getAllMessages)
            {
                Console.WriteLine(
                    $"messageId: {item.MessageId}, " +
                    $"dateOfSubmission: {item.DateOfSubmission.ToString(CultureInfo.GetCultureInfo("el-GR"))}, " +
                    $"sender: {item.Sender.UserName}, " +
                    $"receiver: {item.Receiver.UserName}, messageData: {item.MessageData}");
            }

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Press  everything from the keyboard to return to the previous menu");
            Console.ReadKey();
        }

        public void EditMessage()
        {
            Console.Clear();
            MessageController messageController = new MessageController();
            List<Message> messages = messageController.RetrieveAllMessages();

            if (messages.Count == 0)
            {
                Console.WriteLine("There are no messages to edit");
                Console.WriteLine("Press something to return");
                Console.ReadKey();
                return;
            }
            foreach (var item in messages)
            {
                Console.WriteLine(
                    $"messageId: {item.MessageId}, " +
                    $"dateOfSubmission: {item.DateOfSubmission.ToString(CultureInfo.GetCultureInfo("el-GR"))}, " +
                    $"sender: {item.Sender.UserName}, " +
                    $"receiver: {item.Receiver.UserName}, " +
                    $"messageData: {item.MessageData}");
            }

            List<int> messagesId = messages.Select(p => p.MessageId).ToList();
            int checkId;
            bool value = true;

            do
            {                
                Console.WriteLine("Give the messageId of the message you want to edit or press 0 to return back");
                bool success = int.TryParse(Console.ReadLine(), out checkId);
                if (success)
                {
                    if (checkId == 0)
                    {
                        return;
                    }
                    if (messagesId.Contains(checkId))
                    {
                        value = false;
                    }
                }
                
            } while (value);

            Console.WriteLine("Give the new messageData");
            string newMessageData = Console.ReadLine();

            Message messageById = messages.Where(x => x.MessageId == checkId).FirstOrDefault();
            messageById.MessageData = newMessageData;

            try
            {
                messageController.UpdateMessage(messageById);
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

        public void DeleteMessage(User user)
        {
            Console.Clear();
            Console.WriteLine("You can delete messages that only you have sent");
            Console.WriteLine("");
            MessageController messageController = new MessageController();
            var messagesSended = messageController.RetrieveMessagesBySender(user.Id);

            if (messagesSended.Count == 0)
            {
                Console.WriteLine("You do not have messages sent");
                Console.WriteLine("Press something to return");
                Console.ReadKey();
                return;
            }

            foreach (var item in messagesSended)
            {
                Console.WriteLine(
                    $"MessageId: {item.MessageId}, " +
                    $"DateOfSubmission: {item.DateOfSubmission.ToString(CultureInfo.GetCultureInfo("el-GR"))}, " +
                    $"messageData: {item.MessageData}, " +
                    $"receiver: {item.Receiver.UserName},");
            }

            bool value = true;
            int checkId;
            List<int> messagesId = messagesSended.Select(x => x.MessageId).ToList();

            do
            {
                Console.WriteLine("");
                Console.WriteLine("Choose the messageId of the message you want to delete or press 0 to return back");
                bool success = int.TryParse(Console.ReadLine(), out checkId);
                if (success)
                {
                    if (checkId == 0)
                    {
                        return;
                    }
                    if (messagesId.Contains(checkId))
                    {
                        value = false;
                    }
                }                

            } while (value);

            try
            {
                messageController.DeleteMessage(checkId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Clear();
            Console.WriteLine("Message Deleted");
            Console.WriteLine("Press something to exit");
            Console.ReadKey();
        }

        

        public void CreateMessage(User user)
        {
            Console.Clear();
            UserController userController = new UserController();
            List<User> listOfReceivers = userController.RetrieveUsers().Where(x => x.Id != user.Id).ToList();

            foreach (var item in listOfReceivers)
            {
                Console.WriteLine($"Receiver: {item.UserName}, ReceiverId: {item.Id}");
            }

            bool value = true;
            int checkId;
            var listOfIds = listOfReceivers.Select(x => x.Id).ToList();

            do
            {
                Console.WriteLine("Give the Id of the receiver or press  0 to return back");
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

            string messageData;
            do
            {
                Console.WriteLine("Give the messageData");
                messageData = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(messageData));
            
            Message message = new Message();
            MessageController messageController = new MessageController();

            message.MessageData = messageData;
            message.ReceiverId = checkId;
            message.SenderId = user.Id;

            try
            {
                messageController.CreateMessage(message);
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
    }
}
