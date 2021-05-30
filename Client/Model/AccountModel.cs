using ClientLibrary;
using ClientServerLibrary;
using ClientServerLibrary.DbClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Client.Model
{
    static class AccountModel
    {
        // Constructor
        static AccountModel()
        {
            conversations = new ObservableCollection<Conversation>();
            contacts = new ObservableCollection<User>();
            messages = new ObservableCollection<ObservableCollection<Message>>();

            RequestContacts();
            GetUserConversations();
        }


        /// Message from textbox
        private static string messageContent;
        public static string MessageContent
        {
            get => messageContent;
            set { messageContent = value; }
        }

        /// Current logged user
        private static User user;
        public static User User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
            }
        }

        /// All conversations
        private static ObservableCollection<Conversation> conversations;
        public static ObservableCollection<Conversation> Conversations
        {
            get
            {
                return conversations;
            }
            set
            {
                conversations = value;
                LoadConversationsNotify();
            }
        }

        /// All messages
        private static ObservableCollection<ObservableCollection<Message>> messages;
        public static ObservableCollection<ObservableCollection<Message>> Messages
        {
            get
            {
                return messages;
            }
            set
            {
                messages = value;
                LoadMessagesNotify();
            }
        }

        /// Contact list
        private static ObservableCollection<User> contacts;
        public static ObservableCollection<User> Contacts
        {
            get
            {
                return contacts;
            }
            set
            {
                contacts = value;
                GetContactsListNotify();
            }
        }

        /// Files 
        // ???
        private static List<KeyValuePair<string, byte[]>> messageFiles;
        public static List<KeyValuePair<string, byte[]>> MessageFiles
        {
            get => messageFiles == null ? messageFiles = new List<KeyValuePair<string, byte[]>>() : messageFiles;
            set => messageFiles = value;
        }


        // Event handlers
        public static event EventHandler LoadConversations;
        public static event EventHandler LoadMessages;
        public static event EventHandler GetContactsList;


        // Notyfiers
        public static void LoadConversationsNotify()
        {
            LoadConversations(null, new ViewModelEventArgs());
        }
        public static void LoadMessagesNotify()
        {
            LoadMessages(null, new ViewModelEventArgs());
        }
        public static void GetContactsListNotify()
        {
            GetContactsList(null, new ViewModelEventArgs());
        }


        // Methods
        public static void LogOut()
        {
            MessageContent = null;
            User = null;
            Conversations = null;
            Messages = null;
            Contacts = null;
            MessageFiles = null;
        }
        public static void SendMessage()
        {
            Task.Run(() =>
            {
                Message message = new Message();
                message.Content = messageContent;
                //message.Conversation = ?
                message.IsRead = false;
                message.Sender = User;
                message.SendTime = DateTime.Now;

                ClientServerMessage csMessage = new ClientServerMessage { Content = message };
                csMessage.AdditionalContent = messageFiles;
                ClientModel.GetInstance().SendMessage(csMessage);

                Messages[message.Conversation.Id].Add(message);
            });
        }
        public static void RequestContacts()
        {
            Task.Run(() =>
            {
                try
                {
                    ClientServerMessage message = new ClientServerMessage { Content = User };
                    message.ActionType = ActionType.GetFriendsFromUserFriendships;

                    ClientModel.GetInstance().SendMessage(message);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            });
        }
        public static void GetConversationMessages(int conversationId)
        {
            Task.Run(() =>
            {
                ClientModel.GetInstance().SendMessage(new ClientServerMessage { Content = new Conversation { Id = conversationId }, ActionType = ActionType.GetConversationMessages });
            });
        }
        public static void GetUserConversations()
        {
            Task.Run(() =>
            {
                ClientModel.GetInstance().SendMessage(new ClientServerMessage { Content = User, ActionType = ActionType.GetUserConversations });
            });
        }

    }
}
