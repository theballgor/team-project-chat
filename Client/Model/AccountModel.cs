using ClientLibrary;
using ClientServerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    static class AccountModel
    {

        public static event EventHandler GetContactsList;

        //Request to servet, get contacts list
        public static void RequestContacts()
        {
            Task.Run(() =>
            {
                try
                {
                    ClientServerMessage message = new ClientServerMessage {};
                    message.ActionType = ActionType.GetFriendsFromUserFriendships;

                    ClientModel.GetInstance().SendMessage(message);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            });
        }

        public static void Notify(object contacts)
        {
            GetContactsList(null, new ViewModelEventArgs {Content = contacts});
        }

    }
}
