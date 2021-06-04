using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
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
                case ActionType.RegisterUser:
                    RegistrationModel.Notify(Register(message));
                    break;
                case ActionType.GetUserInfo:
                    AccountModel.User = GetUserInfo(message);
                    break;
                case ActionType.GetUserConversations:
                    GetUserConversations(message);
                    break;
                case ActionType.GetFriendsFromUserFriendships:
                    GetUserFriendships(message);
                    break;
                case ActionType.GetConversationMessages:
                    GetConversationMessages(message);
                    break;
                    AccountModel.Conversations.Add(new KeyValuePair<Conversation, Message>(CreateConversation(message), null));
                case ActionType.CreateConversation:
                    break;

                case ActionType.GetUsersByUsername:
                    GetUsersByUsername(message);
                    break;

                case ActionType.GetUserFriendRequests:
                    GetUserFriendRequests(message);
                    break;

                //case ActionType.JoinConversation:
                //    AccountModel.Conversations.Add(JoinConversation(message));
                //    break;

                //case ActionType.GetConversationMessages:
                //    AccountModel.Messages.Add(GetConversationMessages(message));
                //    break;


                //case ActionType.GetUserFriendships:
                //    //AccountModel.Contacts.Add(SendFriendRequest(message));
                //    break;


                case ActionType.GetConversationUsers:
                    break;

                case ActionType.Error:
                    MessageBox.Show(Error(message).Message);
                    break;

                case ActionType.FatalError:
                    //ClientModel.GetInstance().Disconnect();
                    break;

                default:
                    break;
            }
        }

        static InvalidDataError Error(ClientServerMessage message)
        {
            if (message.Content != null)
                return message.Content as InvalidDataError;
            else return null;
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

        static bool Register(ClientServerMessage message)
        {
            return (bool)message.Content;
        }

        static void GetUserFriendships(ClientServerMessage message)
        {
            User[] userList = message.Content as User[];

            App.Current.Dispatcher.Invoke(() =>
            {
            if (userList != null)
                foreach (var item in userList)
                    AccountModel.Contacts.Add(item);
            });
        }

        static void GetUserConversations(ClientServerMessage message)
        {
            KeyValuePair<Conversation, Message>[] messageList = message.Content as KeyValuePair<Conversation, Message>[];

            App.Current.Dispatcher.Invoke(() =>
            {
                if (messageList != null)
                    foreach (var item in messageList)
                    AccountModel.Conversations.Add(item);
            });
        }

        static void GetConversationMessages(ClientServerMessage message)
        {
            Message[] messageList = message.Content as Message[];

            App.Current.Dispatcher.Invoke(() =>
            {
                if (messageList != null)
                    foreach (var item in messageList)
                {
                    AccountModel.ActiveMessages.Add(new KeyValuePair<Message, bool>(item, item.Sender.Id == AccountModel.User.Id));
                }
            });
        }

        static void GetUsersByUsername(ClientServerMessage message)
        {
            User[] userList = message.Content as User[];

            App.Current.Dispatcher.Invoke(() =>
            {
                foreach (var item in userList)
                {
                    AccountModel.FindFriends.Add(item);
                    System.Console.WriteLine(item.Username);
                }
            });
        }

        static Conversation CreateConversation(ClientServerMessage message)
        {
            if (message.Content != null)
                return message.Content as Conversation;
            else
                return null;
        }

        static void GetUserFriendRequests(ClientServerMessage message)
        {
            Console.WriteLine("FRIEND REQUESTS");
            User[] userList = message.Content as User[];

            App.Current.Dispatcher.Invoke(() =>
            {
                foreach (var item in userList)
                {
                    AccountModel.FriendRequests.Add(item);
                    Console.WriteLine(item.Username);
                }
            });
        }
    }
}
