using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ClientServerLibrary;
using ClientServerLibrary.DbClasses;

namespace Client.Model
{
    public static class DataWorker
    {
        public static void Handle(ClientServerMessage message)
        {
            switch (message.ActionType)
            {
                case ActionType.LogInUserByEmail:
                    LoginModel.Notify(Login(message));
                    break;

                case ActionType.CreateConversation:
                    AccountModel.Conversations.Add(CreateConversation(message));
                    break;

                case ActionType.JoinConversation:
                    AccountModel.Conversations.Add(JoinConversation(message));
                    break;

                case ActionType.GetConversationMessages:
                    AccountModel.Messages.Add(GetConversationMessages(message));
                    break;

                case ActionType.GetUserConversations:
                    AccountModel.Conversations = GetUserConversations(message);
                    break;

                case ActionType.GetUserFriendships:
                    //AccountModel.Contacts.Add(SendFriendRequest(message));
                    break;

                case ActionType.GetUserInfo:
                    AccountModel.User = GetUserInfo(message);
                    break;

                // ???
                case ActionType.RegisterUser:
                    break;

                // ???
                case ActionType.GetConversationUsers:
                    break;

                case ActionType.GetFriendsFromUserFriendships:
                    AccountModel.Contacts = GetFriendsFromUserFriendships(message);
                    break;


                case ActionType.FatalError:
                    Console.WriteLine("FATAL ERROR");
                    break;

                default:
                    break;
            }
        }

        static User GetUserInfo(ClientServerMessage message)
        {
            if (message.Content != null)
                return message.Content as User;
            else
                return null;
        }

        static User Login(ClientServerMessage message)
        {
            if (message.Content != null)
                return message.Content as User;
            else
                return null;
        }

        static ObservableCollection<User> GetFriendsFromUserFriendships(ClientServerMessage message)
        {
            if (message.Content != null)
            {
                User[] userList = message.Content as User[];

                ObservableCollection<User> contacts = new ObservableCollection<User>();

                foreach (var item in userList)
                    contacts.Add(item);

                return contacts;
            }
            else
                return null;
        }

        static ObservableCollection<Conversation> GetUserConversations(ClientServerMessage message)
        {
            if (message.Content != null)
            {
                Conversation[] messageList = message.Content as Conversation[];
                ObservableCollection<Conversation> conversations = new ObservableCollection<Conversation>();

                if(messageList.Length != 0)
                    foreach (var item in messageList)
                        conversations.Add(item);

                return conversations;
            }
            else
                return null;
        }

        static ObservableCollection<Message> GetConversationMessages(ClientServerMessage message)
        {
            if (message.Content != null)
            {
                Message[] messageList = message.Content as Message[];

                ObservableCollection<Message> messages = new ObservableCollection<Message>();

                foreach (var item in messageList)
                    messages.Add(item);

                return messages;
            }
            else
                return null;
        }

        static Conversation CreateConversation(ClientServerMessage message)
        {
            if (message.Content != null)
                return message.Content as Conversation;
            else
                return null;
        }

        static Conversation JoinConversation(ClientServerMessage message)
        {
            if (message.Content != null)
                return message.Content as Conversation;
            else
                return null;
        }

        static User SendFriendRequest(ClientServerMessage message)
        {
            if (message.Content != null)
                return message.Content as User;
            else
                return null;
        }


    }
}
