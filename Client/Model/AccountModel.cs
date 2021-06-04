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
    /// Fields
    static partial class AccountModel
    {
        /// Constructor
        static AccountModel()
        {
            conversations = new ObservableCollection<KeyValuePair<Conversation, Message>>();
            contacts = new ObservableCollection<User>();
            activeMessages = new ObservableCollection<KeyValuePair<Message, bool>>();
            activeConversation = new Conversation();

            Task.Run(() =>
            {
                RequestContacts();
                Thread.Sleep(100);
                GetUserConversations();
                Thread.Sleep(100);
                GetUserFriendRequests();
            });
        }

        /// Message from textbox
        /// Текстбокс повідомлення
        private static string messageContent;
        public static string MessageContent
        {
            get => messageContent;
            set { messageContent = value; }
        }

        /// Current logged user
        /// Користувач який зараз залогінений
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
        /// Колекція KeyValuePair з усіма чатами та останнім повідомленням у них
        private static ObservableCollection<KeyValuePair<Conversation, Message>> conversations;
        public static ObservableCollection<KeyValuePair<Conversation, Message>> Conversations
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
        /// KeyValuePair з вибраним чатом та усі повідомлення у ньому
        private static Conversation activeConversation;
        public static Conversation ActiveConversation
        {
            get
            {
                return activeConversation;
            }
            set
            {
                activeConversation = value;
            }
        }


        private static ObservableCollection<KeyValuePair<Message, bool>> activeMessages;
        public static ObservableCollection<KeyValuePair<Message, bool>> ActiveMessages
        {
            get
            {
                return activeMessages;
            }
            set
            {
                activeMessages = value;
            }
        }


        /// Contact list
        /// Колекція контактів
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
        /// Колекція KeyValuePair з назвами файлів та їх байтовим масивом(вмістом)
        private static ObservableCollection<MessageFile> messageFiles;
        public static ObservableCollection<MessageFile> MessageFiles
        {
            get => messageFiles == null ? messageFiles = new ObservableCollection<MessageFile>() : messageFiles;
            set => messageFiles = value;
        }




        /// Список наших запитів на дружбу
        private static ObservableCollection<User> friendRequests;
        public static ObservableCollection<User> FriendRequests
        {
            get => friendRequests == null ? friendRequests = new ObservableCollection<User>() : friendRequests;
            set => friendRequests = value;
        }

        /// Список який повретається нам коли ми шукаємо нових друзів
        private static ObservableCollection<User> findFriends;
        public static ObservableCollection<User> FindFriends
        {
            get => findFriends == null ? findFriends = new ObservableCollection<User>() : findFriends;
            set => findFriends = value;
        }

        /// Значення текстбоксу де ми шукаємо друга
        private static string findFriendByUsername;
        public static string FindFriendByUsername
        {
            get => findFriendByUsername;
            set { findFriendByUsername = value; }
        }


    }

    /// Methods
    static partial class AccountModel
    {
        /// Надсилання повідомлень
        public static void SendMessage()
        {
            Message message = new Message();
            message.Content = messageContent;
            message.Conversation = ActiveConversation;
            message.Sender = User;
            message.IsRead = false;
            message.SendTime = DateTime.Now;

            ClientServerMessage clientMessage = new ClientServerMessage { Content = message, ActionType = ActionType.SendConversationMessage };


            if (messageFiles != null)
                clientMessage.AdditionalContent = messageFiles.ToArray();
            messageFiles = null;

            ActiveMessages.Add(new KeyValuePair<Message,bool>(message,false));
            ClientModel.GetInstance().SendMessageAsync(clientMessage);
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


            MessageFiles.Add(new MessageFile(Path.GetFileName(fileName), File.ReadAllBytes(fileName)));
            /*ClientServerMessage csMessage = new ClientServerMessage { Content = message };
            csMessage.AdditionalContent = messageFiles.ToArray();
            ClientModel.GetInstance().SendMessageSync(csMessage);*/

            //Messages[message.Conversation.Id].Add(message);

        }


        /// Запит на контакти
        /// ObservableCollection<User>
        /// 
        /// Contacts
        public static void RequestContacts()
        {
            ClientModel.GetInstance().SendMessageSync(new ClientServerMessage {  ActionType = ActionType.GetFriendsFromUserFriendships });
            Console.WriteLine("requested contacts");
        }


        /// Запит на колекцію повідомлень у вибраному чаті
        /// KeyValuePair<Conversation, ObservableCollection<Message>>
        /// 
        /// ActiveMessages
        public static void GetConversationMessages()
        {
            ClientModel.GetInstance().SendMessageAsync(new ClientServerMessage { Content = activeConversation, ActionType = ActionType.GetConversationMessages });
        }


        /// Запит на колекцію усіх чатів
        /// ObservableCollection<KeyValuePair<Conversation, Message>>
        /// 
        /// Conversations
        public static void GetUserConversations()
        {
            ClientModel.GetInstance().SendMessageSync(new ClientServerMessage { ActionType = ActionType.GetUserConversations });
            Console.WriteLine("conversations requested");
        }


        public static void CreateConversation()
        {
            Conversation conversation = new Conversation
            {
                Creator = User,
                Name = "TEST CONVERSATION",
                ConversationAccessibility = ConversationAccessibility.Public,
            };

            ClientModel.GetInstance().SendMessageAsync(new ClientServerMessage { Content = conversation, ActionType = ActionType.CreateConversation });
        }





        public static void GetUserFriendRequests()
        {
            ClientModel.GetInstance().SendMessageSync(new ClientServerMessage { Content = User, ActionType = ActionType.GetUserFriendRequests });
            Console.WriteLine("requested FriendsRequest");
        }

        public static void GetUsersByUsername()
        {
            ClientModel.GetInstance().SendMessageAsync(new ClientServerMessage { Content = new User { Username = findFriendByUsername }, ActionType = ActionType.GetUsersByUsername });
            Console.WriteLine("GET USERS WITH [ " + findFriendByUsername + " ] IN USERNAME");
        }

        public static void SendFriendRequest(User userToRequest)
        {
            Friendship friendship = new Friendship { Inviter = User, Requester = new User { Id = userToRequest.Id }, InviteTime = DateTime.Now };

            ClientModel.GetInstance().SendMessageSync(new ClientServerMessage { Content = friendship, ActionType = ActionType.SendFriendRequest });
            Console.WriteLine("FRIEND REQUEST SENT FOR " + userToRequest.Username);
        }

        public static void ConfirmFriendRequestCommand(User userToConfirm)
        {
            Friendship friendship = new Friendship { Requester = User, Inviter = new User { Id = userToConfirm.Id }, FriendshipStatus = FriendshipStatus.Confirmed };

            ClientModel.GetInstance().SendMessageSync(new ClientServerMessage { Content = friendship, ActionType = ActionType.FriendRequestResult });
            Console.WriteLine("FRIEND REQUEST CONFIRMED FOR " + userToConfirm.Username);
        }


        public static void DeclineFriendRequestCommand(User userToDecline)
        {
            Friendship friendship = new Friendship { Requester = User, Inviter = new User { Id = userToDecline.Id }, FriendshipStatus = FriendshipStatus.Declined };

            ClientModel.GetInstance().SendMessageSync(new ClientServerMessage { Content = friendship, ActionType = ActionType.FriendRequestResult });
            Console.WriteLine("FRIEND REQUEST DECLINED FOR " + userToDecline.Username);
        }
    }

}
