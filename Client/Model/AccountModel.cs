using ClientLibrary;
using ClientServerLibrary;
using ClientServerLibrary.DbClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    static class AccountModel
    {

        public static event EventHandler GetContactsList;

        //Request to servet, get contacts list
        public static void RequestContacts()
        private static string messageContent;
        public static string MessageContent { get { return messageContent; } set { messageContent = value; } }
        public static Message SendMessage(string messageContent, List<KeyValuePair<string, byte[]>> files)
        {
            Message message = new Message();
            Task.Run(() =>
            {
                message.Content = messageContent;
                //message.Conversation = ?
                message.IsRead = false;
                message.MessageType = MessageType.Text;
                //message.Sender = ?
                message.SendTime = DateTime.Now;
                ClientServerMessage csMessage = new ClientServerMessage { Content = message };
                csMessage.AdditionalContent = files;
                ClientModel.GetInstance().SendMessage(csMessage);
                try
                {
                    ClientServerMessage message = new ClientServerMessage {};
                    message.ActionType = ActionType.GetFriendsFromUserFriendships;

                    ClientModel.GetInstance().SendMessage(message);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            });
        }

            return message;
        public static void Notify(object contacts)
        {
            GetContactsList(null, new ViewModelEventArgs {Content = contacts});
        }

    }
}
