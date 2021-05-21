using ClientServerLibrary.DbClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Stores
{
    public class UserStore
    {
        private User _currentUserModel;
        public User CurrentUserModel
        {
            get => _currentUserModel;
            set
            {
                _currentUserModel = value;
            }
        }
    }
}
