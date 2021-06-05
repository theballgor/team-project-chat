﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
                    Task.Run(() => ConversationHandler(currentClient));
                }
            });
        }
        private void ConversationHandler(TcpClient client)
        {
            try
            {
                User currentUser = null;
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
                        case ActionType.LogInUserByEmail:
                            LoginUserByEmail((User)clientServerMessage.Content);
                            break;
                        //case ActionType.LogInUserByUsername:
                        //    LoginUserByUsername((User)clientServerMessage.Content);
                        //    break;
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
                        case ActionType.GetFriendsFromUserFriendships:
                            GetFriendsFromUserFriendships();
                            break;
                        case ActionType.GetUserInfo:
                            GetUserInfo();
                            break;
                        //case ActionType.UpdateUserInfo:
                        //    UpdateUserInfo((User)clientServerMessage.Content);
                        //    break;
                        //case ActionType.UpdateConversationInfo:
                        //    UpdateConversationInfo((Conversation)clientServerMessage.Content);
                        //    break;
                        case ActionType.FatalError:

                            break;
                        case ActionType.FriendRequestResult:
                            break;
                        case ActionType.GetUserFriendRequests:
                            GetUserFriendRequests();
                            break;
                        case ActionType.GetUsersByUsername:
                            GetUsersByUsername((string)clientServerMessage.Content);
                            break;
                        case ActionType.Error:
                            break;
                    }
                    /// <summary>
                    /// returns Content=User[]
                    /// </summary>
                    void GetUserFriendRequests()
                    {
                        Friendship[] friendships= dbManager.GetAllUserFriendShips(currentUser);
                        if(friendships!=null&&friendships.Length!=0)
                            friendships= friendships.Where(item => item.FriendshipStatus == FriendshipStatus.Pending).ToArray();
                        SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = friendships });
                    }
                        /// <summary>
                        /// returns Content=User[]
                        /// </summary>
                        void GetUsersByUsername(string  userName)
                    {
                        User[] users = dbManager.GetAllUsersByUserName(userName);
                        SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = users });
                    }
                        /// <summary>
                        /// returns Content=RegistrationResult
                        /// </summary>
                        void RegisterUser(User user)
                    {
                        RegistrationResult registrationResult = RegistrationResult.Success;
                        User[] users = dbManager.GetAllUsers();
                        if (users != null)
                        {
                            if (users.Where(item => item.Email == user.Email).Count() != 0)
                                registrationResult = RegistrationResult.EmailAlreadyExists;
                            else if (users.Where(item => item.Username == user.Username).Count() != 0)
                                registrationResult = RegistrationResult.UserNameAlreadyExists;
                            //else if (users.Where(item => item.PhoneNumber == user.PhoneNumber).Count() != 0)
                            //    registrationResult = RegistrationResult.PhoneNumberAlreadyExists;
                        }
                        if (registrationResult == RegistrationResult.Success)
                        {
                            if (!dbManager.CreateUser(user))
                            {
                                registrationResult = RegistrationResult.CreationError;

                            }
                        }
                        SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = registrationResult });
                    }

                    /// <summary>
                    /// returns Content=User or null
                    /// </summary>
                    void LoginUserByEmail(User user)
                    {
                        user = dbManager.CheckLoginByEmail(user);
                        if (user != null)
                            connectedClients.Add(new KeyValuePair<int, TcpClient>(user.Id, client));
                        currentUser = user;
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
                        currentUser = user;
                        SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = user });
                    }

                    /// <summary>
                    /// returns Content=Conversation or null
                    /// </summary>
                    void CreateConversation(Conversation conversation)
                    {
                        //not ended 
                        if (!dbManager.CreateConversation(conversation))
                            conversation = null;
                        SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = conversation });
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

                            if (!dbManager.CreateConversationConnection(new ConversationConnection() { Conversation = conversation, User = user }))
                                conversationConnection = null;
                        }
                        SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = conversationConnection });
                    }

                    /// <summary>
                    /// returns Content=friendship or null
                    /// </summary>
                    void AddFriend(int friendId)
                    {
                        Friendship friendship = new Friendship() { Inviter = currentUser, Requester = dbManager.GetUserById(friendId), FriendshipStatus = FriendshipStatus.Pending };
                        SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = friendship });
                    }

                    /// <summary>
                    /// returns to user Content=bool and Content=Message to other users in group
                    /// </summary>
                    void SendConversationMessage(Message message)
                    {
                        object content = null;
                        try
                        {
                            if (clientServerMessage.AdditionalContent != null)
                            {
                                MessageFile[] dbFiles = (MessageFile[])clientServerMessage.AdditionalContent;
                                foreach (var file in dbFiles)
                                {
                                    string newFilePath;
                                    if (file.FileExtenction == ".wav")
                                    {
                                        file.FileType = FileType.Audio;
                                        newFilePath = ConfigurationManager.AppSettings["AudioPath"];
                                    }
                                    else if (file.ImageCheck())
                                    {
                                        file.FileType = FileType.Image;
                                        newFilePath = ConfigurationManager.AppSettings["ImagePath"];
                                    }
                                    else
                                    {
                                        file.FileType = FileType.File;
                                        newFilePath = ConfigurationManager.AppSettings["FilePath"];
                                    }
                                    newFilePath = newFilePath + "\\" + Path.GetRandomFileName() + file.FileExtenction;
                                    file.FilePath = newFilePath;
                                    if (!file.SaveFileByPath())
                                        throw new Exception();
                                    dbManager.CreateFile(file);
                                }
                            }
                            
                            Message tmp = message;
                            tmp.Conversation = dbManager.GetConversationById(message.Conversation.Id);
                            dbManager.CreateMessage(tmp);

                            List<User> users = dbManager.GetAllUsersFromConversation(tmp.Conversation).ToList();
                            users.Remove(currentUser);
                            List<TcpClient> clients = GetClientsByUsers(users.ToArray());
                            SendMessage(clients, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = content });
                            SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = true });
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = false });
                        }



                    }

                    /// <summary>
                    /// returns Content=List<KeyValuePair<Conversation, Message[]>>
                    /// </summary>
                    void GetUserConversations()
                    {
                        while (currentUser == null) { Thread.Sleep(200); } //wait for initialization currentUser
                        Conversation[] conversations = dbManager.GetAllUserConversations(currentUser.Id);
                        if (conversations != null)
                        {
                            List<KeyValuePair<Conversation, Message>> ConversationMessagesValuePairs = new List<KeyValuePair<Conversation, Message>>();
                            foreach (var conversation in conversations)
                            {
                                Message message = null;
                                Message[] messages = dbManager.GetAllConversationMessages(conversation);
                                if (messages!=null&&messages.Length != 0)
                                    message = messages.OrderBy(item => item.SendTime).Last();
                                ConversationMessagesValuePairs.Add(new KeyValuePair<Conversation, Message>(conversation, message));
                            }
                            SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = ConversationMessagesValuePairs.ToArray() });
                        }
                        else
                        {
                            SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = null });
                        }
                    }

                    /// <summary>
                    /// returns List<KeyValuePair<Message, DbFile[]>>
                    /// </summary>
                    void GetConversationMessages(Conversation conversation)
                    {
                        Message[] messages = dbManager.GetAllConversationMessages(conversation);

                        List<KeyValuePair<Message, MessageFile[]>> messageFilePair = null;
                        if (messages != null)
                        {
                            messageFilePair = new List<KeyValuePair<Message, MessageFile[]>>();
                            foreach (Message message in messages)
                            {
                                MessageFile[] dbFiles = dbManager.GetAllMessageFiles(message);
                                foreach (var dbFile in dbFiles)
                                {
                                    if (dbFile.GetFileFromPath())
                                    {
                                        Console.WriteLine("File read");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Faile to read file");
                                    }
                                }
                                messageFilePair.Add(new KeyValuePair<Message, MessageFile[]>(message, dbFiles));
                            }
                        }
                        SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = messageFilePair.ToArray() });
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
                    /// returns Content=User[]
                    /// </summary>
                    void GetFriendsFromUserFriendships()
                    {
                        Friendship[] friendships = dbManager.GetAllUserFriendShips(currentUser);
                        User[] users = null;
                        if (friendships != null)
                        {
                            users = new User[friendships.Length];
                            for (int i = 0; i < friendships.Length; i++)
                            {
                                if (friendships[i].Requester == currentUser)
                                    users[i] = friendships[i].Inviter;
                                else
                                    users[i] = friendships[i].Requester;
                            }
                        }
                        SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = users });
                    }

                    /// <summary>
                    /// returns Content=user
                    /// </summary>
                    void GetUserInfo()
                    {
                        User user = dbManager.GetUserById(currentUser.Id);
                        currentUser = user;
                        SendMessage(client, new ClientServerMessage() { ActionType = clientServerMessage.ActionType, Content = user });
                    }

                    void UpdateUserInfo(User user)
                    {

                    }

                    void UpdateConversationInfo(Conversation conversation)
                    {

                    }
                }
            }
            catch (Exception e)
            {
                try
                {
                    SendMessage(client, new ClientServerMessage() { ActionType = ActionType.FatalError, AdditionalContent = "Something went wrong" });
                }
                catch (Exception)
                {
                    Console.WriteLine("Can't find user to send message");
                }
                AbortConnection(client);
                Console.WriteLine(client.Client.RemoteEndPoint + "\t disconnected");
            }
        }
    }
}