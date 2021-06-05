using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServerLibrary
{
    [Serializable]
    public class InvalidDataError
    {
        private string message;
        public string Message
        {
            get => message;
            set => message = value;
        }
    }
}
