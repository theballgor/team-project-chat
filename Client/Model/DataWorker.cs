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
                case ActionType.SendConversationMessage:
                    break;
                case ActionType.SendFriendRequest:
                    break;

                case ActionType.RegisterUser:
                    RegistrationModel.Notify(Register(message));
                    break;

                case ActionType.LogInUserByEmail:
                    LoginModel.Notify(Login(message));
                    break;
                case ActionType.CreateConversation:
                    break;
                case ActionType.JoinConversation:
                    break;
                case ActionType.GetConversationMessages:
                    break;
                case ActionType.GetUserConversations:
                    break;
                case ActionType.GetConversationUsers:
                    break;
                case ActionType.GetUserFriendships:
                    break;
                case ActionType.GetUserInfo:
                    break;
                case ActionType.FatalError:
                    break;
                case ActionType.GetFriendsFromUserFriendships:
                    AccountModel.Notify(GetContactsList(message));
                    break;
                default:
                    break;
            }
        }

        private static object Register(ClientServerMessage message)
        {
            if (message.Content != null)
                return message.Content;
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

        static User[] GetContactsList(ClientServerMessage message)
        {
            if (message.Content != null)
            { 
                return message.Content as User[];
            }
            else
                return null;
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
        {
            return new ObservableCollection<T>(enumerable);
        }
    }
}
