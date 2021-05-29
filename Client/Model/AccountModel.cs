using ClientLibrary;
using ClientServerLibrary.DbClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    static class AccountModel
    {
        static AccountModel() 
        {
            conversations = new ObservableCollection<KeyValuePair<Conversation, ObservableCollection<Message>>>();
        }


        static private User user;
        static public User User
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

        private static ObservableCollection<KeyValuePair<Conversation, ObservableCollection<Message>>> conversations;
        public static ObservableCollection<KeyValuePair<Conversation, ObservableCollection<Message>>> Conversations
        {
            get
            {
                return conversations;
            }
            set
            {
                conversations = value;
                LoadDataNotify();
            }
        }

        public static event EventHandler LoadData;

        // Notyfier
        public static void LoadDataNotify()
        {
            LoadData(null, new ViewModelEventArgs { Content = null });
        }
    }
}
