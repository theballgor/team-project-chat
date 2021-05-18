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
                    ClientServerMessage message = ClientServerDataManager.Deserialize(data);
                    switch (message.ActionType)
                    {
                        case ActionType.SendText:

                            break;
                        case ActionType.SendAudio:

                            break;
                        case ActionType.SendFile:

                            break;
                        case ActionType.RegisterUser:
                            RegisterUser((User)message.Content);
                            break;
                        case ActionType.LogInUser:
                            LoginUser((User)message.Content);
                            break;
                        case ActionType.CreateConversation:
                            CreateConversation((Conversation)message.Content);
                            break;
                        case ActionType.JoinConversation:
                            JoinConversation((int)message.Content);
                            break;
                        case ActionType.AddFriend:
                            AddFriend((int)message.Content);
                            break;
                        case ActionType.GetConversationMessages:

                            break;
                        case ActionType.FatalError:

                            break;
                    }
                    void RegisterUser(User user)
                    {
                        bool registerResult = dbManager.CreateUser(user);
                        SendMessage(client, new ClientServerMessage() { ActionType = message.ActionType, Content = registerResult });
                    }
                    void LoginUser(User user)
                    {
                        User dbUser = dbManager.CheckLogin(user);
                        if (dbUser != null)
                            connectedClients.Add(new KeyValuePair<int, TcpClient>(dbUser.Id, client));
                        SendMessage(client, new ClientServerMessage() { ActionType = message.ActionType, Content = dbUser });
                    }
                    void CreateConversation(Conversation conversation)
                    {
                        Conversation dbConversation = dbManager.CreateConversation(conversation);
                        SendMessage(client, new ClientServerMessage() { ActionType = message.ActionType, Content = dbConversation });
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
                        SendMessage(client, new ClientServerMessage() { ActionType = message.ActionType, Content = dbConversation });
                    }
                    void AddFriend(int friendId)
                    {
                        User currentUser = dbManager.GetUserById(GetUserIdByClient(client));
                        Friendship friendship=new Friendship() {Inviter= currentUser, Requester=dbManager.GetUserById(friendId),InviteTime=DateTime.Now,FriendshipStatus= FriendshipStatus.Pending};
                        SendMessage(client, new ClientServerMessage() { ActionType = message.ActionType, Content = friendship });
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