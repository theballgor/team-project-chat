using ClientLibrary;
using ClientServerLibrary;
using ClientServerLibrary.DbClasses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Model
{
    static class AccountModel
    {
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
                //message.Sender = ?
                message.SendTime = DateTime.Now;
                ClientServerMessage csMessage = new ClientServerMessage { Content = message };
                csMessage.AdditionalContent = files;
                ClientModel.GetInstance().SendMessage(csMessage);
            });

            return message;
        }


        #region Contacts
        public static event EventHandler GetContactsList;

        //Request to servet, get contacts list
        public static void RequestContacts()
        {
            Task.Run(() =>
            {
                try
                {
                    ClientServerMessage message = new ClientServerMessage { };
                    message.ActionType = ActionType.GetFriendsFromUserFriendships;

                    ClientModel.GetInstance().SendMessage(message);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            });
        }
        public static void Notify(object contacts)
        {
            GetContactsList(null, new ViewModelEventArgs {Content = contacts});
        }
        #endregion
    }
}
