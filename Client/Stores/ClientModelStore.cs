using Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Stores
{
    public class ClientModelStore
    {
        private ClientModel _currentClientModel;
        public ClientModel CurrentClientModel
        {
            get => _currentClientModel;
            set 
            {
                _currentClientModel = value; 
            }
        }

    }
}
