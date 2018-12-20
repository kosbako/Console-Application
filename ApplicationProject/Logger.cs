using ApplicationProject.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationProject
{
    public class Logger
    {
        public void LogCreate(Message message)
        {
            try
            {
                string curFile = "log.txt";
                if (!File.Exists(curFile)) File.Create(curFile).Dispose();
                UserController userController = new UserController();
                User sender = userController.RetrieveUserById(message.SenderId);
                User receiver = userController.RetrieveUserById(message.ReceiverId);

                List<string> mylist = new List<string>(new string[] 
                {
                    $"MessageId: {message.MessageId.ToString()}",
                    $"DateOfSubmission: {message.DateOfSubmission.ToString()}",
                    $"Sender Username: { sender.UserName}",
                    $"Receiver Username: { receiver.UserName}",
                    $"MessageData: { message.MessageData}",
                    Environment.NewLine
                });

                File.AppendAllLines(curFile, mylist);
            }
            catch (Exception)
            {
                //do nothing
                //we don't want to catch a message if crash the logger
            }
        }

        public void LogUpdate(Message message, string oldMessageData)
        {
            try
            {
                string curFile = "log.txt";
                if (!File.Exists(curFile)) File.Create(curFile).Dispose();
                UserController userController = new UserController();
                User sender = userController.RetrieveUserById(message.SenderId);
                User receiver = userController.RetrieveUserById(message.ReceiverId);

                List<string> mylist = new List<string>(new string[]
                {
                    $"MessageId: {message.MessageId.ToString()}",
                    $"DateOfChange: {DateTime.Now.ToString()}",
                    $"Sender Username: { sender.UserName}",
                    $"Receiver Username: { receiver.UserName}",
                    $"OldMessageData: {oldMessageData}",
                    $"NewMessageData: { message.MessageData}",
                    Environment.NewLine
                });

                File.AppendAllLines(curFile, mylist);
            }
            catch (Exception)
            {
                //do nothing
                //we don't want to catch a message if crash the logger
            }
        }
    }
}
