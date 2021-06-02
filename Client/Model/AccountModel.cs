using ClientLibrary;
using ClientServerLibrary;
using ClientServerLibrary.DbClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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

            Task.Run(() =>
            {
                RequestContacts();
                GetUserConversations();
            });
        }

        // Fields

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
            }
        }

        /// Files 
        // ???
        private static List<MessageFile> messageFiles;
        public static List<MessageFile> MessageFiles
        {
            get => messageFiles == null ? messageFiles = new List<MessageFile>() : messageFiles;
            set => messageFiles = value;
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
            Message message = new Message();
            message.Content = messageContent;
            //message.Conversation = !!!!!; 
            message.IsRead = false;
            message.Sender = User;
            message.SendTime = DateTime.Now;

            ClientServerMessage csMessage = new ClientServerMessage { Content = message };
            csMessage.AdditionalContent = messageFiles.ToArray();
            ClientModel.GetInstance().SendMessageSync(csMessage);

            //Messages[message.Conversation.Id].Add(message);
        }

        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);
        public static void StartRecordingVoiceMessage()
        {
            mciSendString("open new Type waveaudio Alias recsound", "", 0, 0);
            mciSendString("record recsound", "", 0, 0);

            /*ClientServerMessage csMessage = new ClientServerMessage { Content = message };
            csMessage.AdditionalContent = messageFiles.ToArray();
            ClientModel.GetInstance().SendMessageSync(csMessage);*/

            //Messages[message.Conversation.Id].Add(message);

        }

        public static void StopRecordingVoiceMessage()
        {
            string fileName = "lastVoiceMessage.wav";
            mciSendString($"save recsound {fileName}", "", 0, 0);
            mciSendString("close recsound ", "", 0, 0);


            MessageFiles.Add(new DbFile(Path.GetFileName(fileName), File.ReadAllBytes(fileName)));
            /*ClientServerMessage csMessage = new ClientServerMessage { Content = message };
            csMessage.AdditionalContent = messageFiles.ToArray();
            ClientModel.GetInstance().SendMessageSync(csMessage);*/

            //Messages[message.Conversation.Id].Add(message);

        }

        public static void RequestContacts()
        {
            try
            {
               ClientServerMessage message = new ClientServerMessage { Content = User };
                message.ActionType = ActionType.GetFriendsFromUserFriendships;

                ClientModel.GetInstance().SendMessageSync(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void GetConversationMessages(int conversationId)
        {
            ClientModel.GetInstance().SendMessageSync(new ClientServerMessage { Content = new Conversation { Id = conversationId }, ActionType = ActionType.GetConversationMessages });
        }
        public static void GetUserConversations()
        {
            ClientModel.GetInstance().SendMessageSync(new ClientServerMessage { Content = User, ActionType = ActionType.GetUserConversations });
        }

    }

}
