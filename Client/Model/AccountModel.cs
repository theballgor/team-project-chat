﻿using ClientLibrary;
using ClientServerLibrary;
using ClientServerLibrary.DbClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Model
{
    static class AccountModel
    {
        // Constructor
        static AccountModel()
        {
            conversations = new ObservableCollection<KeyValuePair<Conversation, Message>>();
            contacts = new ObservableCollection<User>();
            activeMessages = new ObservableCollection<Message>();

            Task.Run(() =>
            {
                RequestContacts();
                GetUserConversations();
            });
        }
            
        // Fields

        /// Message from textbox
        private static string messageContent;
        public static string MessageContent
        {
            get => messageContent;
            set { messageContent = value; }
        }

        /// Current logged user
        private static User user;
        public static User User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
            }
        }

        /// All conversations
        private static ObservableCollection<KeyValuePair<Conversation, Message>> conversations;
        public static ObservableCollection<KeyValuePair<Conversation, Message>> Conversations
        {
            get
            {
                return conversations;
            }
            set
            {
                conversations = value;
            }
        }

        /// All messages

        public static ObservableCollection<Message> activeMessages;
        public static ObservableCollection<Message> ActiveMessages
        {
            get
            {
                return activeMessages;
            }
            set
            {
                activeMessages = value;
            }
        }

        /// Contact list
        private static ObservableCollection<User> contacts;
        public static ObservableCollection<User> Contacts
        {
            get
            {
                return contacts;
            }
            set
            {
                contacts = value;
            }
        }

        /// Files 
        // ???
        private static ObservableCollection<KeyValuePair<string, byte[]>> messageFiles;
        public static ObservableCollection<KeyValuePair<string, byte[]>> MessageFiles
        {
            get => messageFiles == null ? messageFiles = new ObservableCollection<KeyValuePair<string, byte[]>>() : messageFiles;
            set => messageFiles = value;
        }


        // Methods
        public static void LogOut()
        {
            MessageContent = null;
            User = null;
            Conversations = null;
            Contacts = null;
            MessageFiles = null;
        }
        public static void SendMessage()
        {
            Message message = new Message();
            message.Content = messageContent;
            //message.Conversation = ?
            message.IsRead = false;
            message.Sender = User;
            message.SendTime = DateTime.Now;

            ClientServerMessage csMessage = new ClientServerMessage { Content = message };
            csMessage.AdditionalContent = messageFiles;
            ClientModel.GetInstance().SendMessageSync(csMessage);

            ActiveMessages.Add(message);
        }
        public static void RequestContacts()
        {
            try
            {
               ClientServerMessage message = new ClientServerMessage { Content = User };
                message.ActionType = ActionType.GetFriendsFromUserFriendships;

                ClientModel.GetInstance().SendMessageSync(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void GetConversationMessages(int conversationId)
        {
            ClientModel.GetInstance().SendMessageSync(new ClientServerMessage { Content = new Conversation { Id = conversationId }, ActionType = ActionType.GetConversationMessages });
        }
        public static void GetUserConversations()
        {
            ClientModel.GetInstance().SendMessageSync(new ClientServerMessage { Content = User, ActionType = ActionType.GetUserConversations });
        }
    }
}
