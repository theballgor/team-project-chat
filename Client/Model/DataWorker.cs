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
                    Message conversationMessage = SendConversationMessage(message);
                    AccountModel.Messages[conversationMessage.Conversation.Id].Add(conversationMessage);
                    break;

                case ActionType.SendFriendRequest:
                    break;

                case ActionType.LogInUserByEmail:
                    LoginModel.Notify(Login(message));
                    break;

                // ???
                case ActionType.RegisterUser:
                    break;


                case ActionType.CreateConversation:
                    AccountModel.Conversations.Add(CreateConversation(message));
                    break;

                case ActionType.JoinConversation:
                    AccountModel.Conversations.Add(JoinConversation(message));
                    break;

                //
                case ActionType.GetConversationMessages:
                    break;

                case ActionType.GetUserConversations:

                    AccountModel.Conversations = GetUserConversations(message);

                    break;

                //
                case ActionType.GetConversationUsers:
                    break;

                //
                case ActionType.GetUserFriendships:
                    break;

                //
                case ActionType.GetUserInfo:
                    break;

                //
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

        static ObservableCollection<Conversation> GetUserConversations(ClientServerMessage message)
        {
            if (message.Content != null)
            {
                List<Conversation> messageList = message.Content as List<Conversation>;

                ObservableCollection<Conversation> conversations = new ObservableCollection<Conversation>();

                foreach (var item in messageList)
                    conversations.Add(item);

                return conversations;
            }
            else
                return null;
        }


        ///// HARDCODE
        //static ObservableCollection<Conversation> GetUserConversations(ClientServerMessage message)
        //{
        //    ObservableCollection<Conversation> collection = new ObservableCollection<Conversation>();
        //    for (int i = 0; i < 5; i++)
        //    {
        //        Conversation conversation = new Conversation();
        //        conversation.Id = i;
        //        conversation.Name = "Conversation #" + i;

        //        collection.Add(conversation);
        //    }

        //    return collection;
        //}


        static Message SendConversationMessage(ClientServerMessage message)
        {
            if (message.Content != null)
                return message.Content as Message;
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
