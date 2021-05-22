using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ClientServerLibrary;
using ClientServerLibrary.DbClasses;

namespace Server
{
    public partial class ServerClass
    {
        private DbManager dbManager;
        private TcpListener server;
        private List<KeyValuePair<string, TcpClient>> connectedClients;
        private readonly object locker;

        public ServerClass(IPEndPoint serverIEP)
        {
            server = new TcpListener(serverIEP);
            locker = new object();
            lock (locker)
                connectedClients = new List<KeyValuePair<string, TcpClient>>();
            dbManager = new DbManager();

        }

        public void Connect()
        {
            server.Start();
            Console.WriteLine($"Server started\t\t{server.LocalEndpoint}");

            Task.Run(() =>
            {
                while (true)
                {
                    TcpClient currentClient = server.AcceptTcpClient();
                    Console.WriteLine("Connnected\t\t" + currentClient.Client.RemoteEndPoint);
                    Task.Run(() =>
                    {
                        ConversationHandler(currentClient);
                    });
                }
            });
        }
        private void ConversationHandler(TcpClient client)
        {
            try
            {
                User currentUser = dbManager.GetUserById(GetUserIdByClient(client));
                while (true)
                {
                    byte[] data = ClientServerDataManager.TcpClientDataReader(client);
                    ClientServerMessage message = ClientServerDataManager.Deserialize(data);
                    switch (message.ActionType)
                    {
                        case ActionType.SendConversationMessage:

                            break;
                        case ActionType.RegisterUser:
                            //RegisterUser((User)message.Content);


                            //(message.Content as User).Username = "This is register!";
                            //(message.Content as User).Id = 1;
                            //SendMessage(client, ClientServerDataManager.Serialize(message));
                            //Console.WriteLine((message.Content as User).Username + "\t" + (message.Content as User).Email);


                            break;
                        case ActionType.LogInUserByEmail:
                            LoginUserByEmail((User)clientServerMessage.Content);

                            break;
                        case ActionType.LogInUserByUsername:
                            LoginUserByUsername((User)clientServerMessage.Content);
                            break;
                        case ActionType.CreateConversation:
                            CreateConversation((Conversation)message.Content);
                            break;
                        case ActionType.JoinConversation:
                            JoinConversation((int)clientServerMessage.Content);
                            break;
                        case ActionType.SendFriendRequest:
                            AddFriend((int)clientServerMessage.Content);
                            break;
                        case ActionType.GetConversationMessages:
                            GetConversationMessages((Conversation)clientServerMessage.Content);
                            break;
                        case ActionType.GetUserConversations:
                            GetUserConversations();
                            break;
                        case ActionType.GetConversationUsers:
                            GetConversationUsers((int)clientServerMessage.Content);
                            break;
                        case ActionType.GetUserFriendships:
                            GetUserFriendShips();
                            break;
                        case ActionType.GetUserInfo:
                            GetUserInfo();
                            break;
                        case ActionType.FatalError:

                            break;
                    }
                    /// <summary>
                    /// returns Content=bool
                    /// </summary>
                    void RegisterUser(User user)
                    {
                        bool registerResult = dbManager.CreateUser(user);
                        SendMessage(client, new ClientServerMessage() { ActionType = message.ActionType, Content = registerResult });
                    }

                    /// <summary>
                    /// returns Content=User or null
                    /// </summary>
                    void LoginUserByEmail(User user)
                    {
                        user = dbManager.CheckLoginByEmail(user);
                        if (user != null)
                            connectedClients.Add(new KeyValuePair<int, TcpClient>(user.Id, client));
                        SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = user });
                    }

                    /// <summary>
                    /// returns Content=User or null
                    /// </summary>
                    void LoginUserByUsername(User user)
                    {
                        user = dbManager.CheckLoginByUsername(user);
                        if (user != null)
                            connectedClients.Add(new KeyValuePair<int, TcpClient>(user.Id, client));
                        SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = user });
                    }

                    /// <summary>
                    /// returns Content=Conversation or null
                    /// </summary>
                    void CreateConversation(Conversation conversation)
                    {
                        Conversation dbConversation = dbManager.CreateConversation(conversation);
                        SendMessage(client, new ClientServerMessage() { ActionType = message.ActionType, Content = dbConversation });
                    }

                    /// <summary>
                    /// returns Content=conversationConnection or null
                    /// </summary>
                    void JoinConversation(int conversationId)
                    {
                        Conversation conversation = dbManager.GetConversationById(conversationId);
                        ConversationConnection conversationConnection = null;
                        if (conversation != null)
                        {
                            User user = dbManager.GetUserById(GetUserIdByClient(client));
                            conversationConnection = dbManager.CreateConversationConnection(new ConversationConnection() { Conversation = conversation, User = user });
                        }
                        SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = conversationConnection });
                    }

                    /// <summary>
                    /// returns Content=friendship or null
                    /// </summary>
                    void AddFriend(int friendId)
                    {
                        Friendship friendship=new Friendship() {Inviter= currentUser, Requester=dbManager.GetUserById(friendId),InviteTime=DateTime.Now,FriendshipStatus= FriendshipStatus.Pending};
                        SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = friendship });
                    }

                    /// <summary>
                    /// returns to user Content=bool and Content=Message to other users in group
                    /// </summary>
                    void SendConversationMessage(Message message)
                    {
                        message = dbManager.CreateMessage(message);
                        if (message != null)
                        {
                            List<User> users = dbManager.GetAllUsersFromConversation(message.Conversation).ToList();
                            object content=null;
                            switch (message.MessageType)
                            {
                                case MessageType.Text:
                                    {
                                        content = message.Content;
                                        message = dbManager.CreateMessage(message);
                                        break;
                                    }
                           
                                case MessageType.Audio:

                                    break;
                                case MessageType.File:
                                    {
                                        string filePath = ConfigurationManager.AppSettings.Get("FilePath");

                                        byte[] file = (byte[])clientServerMessage.Content;
                                        string fileName = clientServerMessage.AdditionalContent.ToString();
                                        string newFilePath = filePath + "\\" + $@"{Guid.NewGuid()}" + Path.GetExtension(fileName);
                                        File.WriteAllBytes(newFilePath, file);
                                        message.Content = newFilePath;
                                        message = dbManager.CreateMessage(message);
                                        break;
                                    }                  
                                case MessageType.Image:
                                    {
                                        string imagePath = ConfigurationManager.AppSettings.Get("ImagePath");

                                        byte[] file = (byte[])clientServerMessage.Content;
                                        string fileName = clientServerMessage.AdditionalContent.ToString();
                                        string newFilePath = imagePath + "\\" + $@"{Guid.NewGuid()}" + Path.GetExtension(fileName);
                                        File.WriteAllBytes(newFilePath, file);
                                        message.Content = newFilePath;
                                        message = dbManager.CreateMessage(message);
                                        break;
                                    }
                            }
                            users.Remove(currentUser);
                            List<TcpClient> clients = GetClientsByUsers(users.ToArray());
                            SendMessage(clients, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = content });

                            SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = true });
                        }
                        else
                        {
                            SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = false });
                        }
                    }

                    /// <summary>
                    /// returns Content=List<KeyValuePair<Conversation, Message[]>>
                    /// </summary>
                    void GetUserConversations()
                    {
                     
                        Conversation[] conversations = dbManager.GetAllUserConversations(currentUser.Id);
                        if (conversations != null)
                        {
                            List<KeyValuePair<Conversation, Message[]>> ConversationMessagesValuePairs = new List<KeyValuePair<Conversation, Message[]>>();
                            foreach (var conversation in conversations)
                            {
                                Message[] messages = dbManager.GetAllConversationMessages(conversation);
                                ConversationMessagesValuePairs.Add(new KeyValuePair<Conversation, Message[]>(conversation, messages));
                            }
                            SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = ConversationMessagesValuePairs });
                        }
                        else
                        {
                            SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = null });
                        }
                    }

                    /// <summary>
                    /// returns Content=Message[]
                    /// </summary>
                    void GetConversationMessages(Conversation conversation)
                    {
                        Message[] messages = dbManager.GetAllConversationMessages(conversation);
                        if (messages != null)
                        {
                            //work with files
                        }
                        SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = messages });
                    }

                    /// <summary>
                    /// returns Content=User[]
                    /// </summary>
                    void GetConversationUsers(int conversationId)
                    {
                        Conversation conversation = dbManager.GetConversationById(conversationId);
                        User[] users = null;
                        if (conversation != null)
                            users = dbManager.GetAllUsersFromConversation(conversation);
                        SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = users });
                    }

                    /// <summary>
                    /// returns Content=Friendship[]
                    /// </summary>
                    void GetUserFriendShips()
                    {
                        Friendship[] friendships = dbManager.GetAllUserFriendShips(currentUser);
                        SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = friendships });
                    }

                    /// <summary>
                    /// returns Content=user
                    /// </summary>
                    void GetUserInfo()
                    {
                       User user= dbManager.GetUserById(currentUser.Id);
                        currentUser = user;
                        SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = user });
                    }
                }
            }
            catch (Exception)
            {
                try
                {
                    SendMessage(client, new ClientServerMessage() { ActionType = ActionType.FatalError, AdditionalContent = "Something went wrong" });
                }
                catch (Exception)
                {
                    Console.WriteLine("Send message error");
                }
                AbortConnection(client);
                Console.WriteLine(client.Client.RemoteEndPoint + "\t disconnected");
            }
        }
    }
}