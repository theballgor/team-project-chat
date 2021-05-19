using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
        private List<KeyValuePair<int, TcpClient>> connectedClients;
        private readonly object locker;
        public ServerClass(IPEndPoint serverIEP)
        {
            server = new TcpListener(serverIEP);
            locker = new object();
            lock (locker)
                connectedClients = new List<KeyValuePair<int, TcpClient>>();
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
                while (true)
                {
                    byte[] data = ClientServerDataManager.TcpClientDataReader(client);
                    ClientServerMessage clientServerMessage = ClientServerDataManager.Deserialize(data);
                    switch (clientServerMessage.ActionType)
                    {
                        case ActionType.SendConversationMessage:
                            SendConversationMessage((Message)clientServerMessage.Content);
                            break;
                        case ActionType.RegisterUser:
                            RegisterUser((User)clientServerMessage.Content);
                            break;
                        case ActionType.LogInUser:
                            LoginUser((User)clientServerMessage.Content);
                            break;
                        case ActionType.CreateConversation:
                            CreateConversation((Conversation)clientServerMessage.Content);
                            break;
                        case ActionType.JoinConversation:
                            JoinConversation((int)clientServerMessage.Content);
                            break;
                        case ActionType.SendFriendRequest:
                            AddFriend((int)clientServerMessage.Content);
                            break;
                        case ActionType.GetConversationMessages:

                            break;
                        case ActionType.FatalError:

                            break;
                    }
                    void RegisterUser(User user)
                    {
                        bool registerResult = dbManager.CreateUser(user);
                        SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = registerResult });
                    }
                    void LoginUser(User user)
                    {
                        user = dbManager.CheckLogin(user);
                        if (user != null)
                            connectedClients.Add(new KeyValuePair<int, TcpClient>(user.Id, client));
                        SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = user });
                    }
                    void CreateConversation(Conversation conversation)
                    {
                        conversation = dbManager.CreateConversation(conversation);
                        SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = conversation });
                    }
                    void JoinConversation(int conversationId)
                    {
                        Conversation conversation = dbManager.GetConversationById(conversationId);
                        ConversationConnection dbConversation = null;
                        if (conversation != null)
                        {
                            User user = dbManager.GetUserById(GetUserIdByClient(client));
                            dbConversation = dbManager.CreateConversationConnection(new ConversationConnection() { Conversation = conversation, User = user });
                        }
                        SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = dbConversation });
                    }
                    void AddFriend(int friendId)
                    {
                        User currentUser = dbManager.GetUserById(GetUserIdByClient(client));
                        Friendship friendship=new Friendship() {Inviter= currentUser, Requester=dbManager.GetUserById(friendId),InviteTime=DateTime.Now,FriendshipStatus= FriendshipStatus.Pending};
                        SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = friendship });
                    }
                    void SendConversationMessage(Message message)
                    {
                        message = dbManager.CreateMessage(message);
                        if (message != null)
                        {
                            User[] users = dbManager.GetAllUsersFromConversation(message.Conversation);
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
                            List<TcpClient> clients = GetClientsByUsers(users);
                            SendMessage(clients, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = content });
                        }
                        else
                        {
                            SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = null });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                AbortConnection(client);
                Console.WriteLine(e.Message);
            }
        }
    }
}