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
                    AccountModel.Conversations[conversationMessage.Conversation.Id].Value.Add(conversationMessage);
                    break;

                //
                case ActionType.SendFriendRequest:
                    break;

                ////
                case ActionType.RegisterUser:
                    break;

                case ActionType.LogInUserByEmail:
                    LoginModel.Notify(Login(message));
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

        //static ObservableCollection<KeyValuePair<Conversation, ObservableCollection<Message>>> GetUserConversations(ClientServerMessage message)
        //{
        //    if (message.Content != null)
        //        return message.Content as ObservableCollection<KeyValuePair<Conversation, ObservableCollection<Message>>>;
        //    else
        //        return null;
        //}

        static ObservableCollection<KeyValuePair<Conversation, ObservableCollection<Message>>> GetUserConversations(ClientServerMessage message)
        {
            ObservableCollection<KeyValuePair<Conversation, ObservableCollection<Message>>> collection = new ObservableCollection<KeyValuePair<Conversation, ObservableCollection<Message>>>();

            for (int i = 0; i < 5; i++)
            {
                ObservableCollection<Message> messages = new ObservableCollection<Message>();
                Conversation conversation = new Conversation();
                conversation.Id = i;

                conversation.Name = "Conversation #" + i;
                for (int j = 0; j < 20; j++)
                    messages.Add(new Message { Conversation = conversation, Sender = new User { Username = "User #" + (j % 2) }, Content = "Message #" + j });
                
                collection.Add(new KeyValuePair<Conversation, ObservableCollection<Message>>(conversation, messages));
            }

            return collection;
        }


        static Message SendConversationMessage(ClientServerMessage message)
        {
            if (message.Content != null)
                return message.Content as Message;
            else
                return null;
        }

        static KeyValuePair<Conversation, ObservableCollection<Message>> CreateConversation(ClientServerMessage message)
        {
            return (KeyValuePair<Conversation, ObservableCollection<Message>>)message.Content;
        }

        static KeyValuePair<Conversation, ObservableCollection<Message>> JoinConversation(ClientServerMessage message)
        {
            return (KeyValuePair<Conversation, ObservableCollection<Message>>)message.Content;
        }



    }
}
