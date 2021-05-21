using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Client.Stores;
using ClientServerLibrary;
using ClientServerLibrary.DbClasses;

namespace Client.Model
{
    public static class DataWorker
    {
        public static User _user;
        public static void Handle(ClientServerMessage message)
        {
            switch (message.ActionType)
            {
                case ActionType.SendConversationMessage:
                    break;
                case ActionType.SendFriendRequest:
                    break;
                case ActionType.RegisterUser:
                    break;
                case ActionType.LogInUser:

                    _user = Login(message);

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
                case ActionType.GetUserFriendshhips:
                    break;
                case ActionType.GetUserInfo:
                    break;
                case ActionType.FatalError:
                    break;
                default:
                    break;
            }
        }

        static User Login(ClientServerMessage message)
        {
            if (message.Content != null)
                return message.Content as User;
            else
                return null;
        }

    }
}
