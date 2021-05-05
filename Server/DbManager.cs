using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class DbManager
    {
        ChatContext chatContext;
        Random random = new Random();
        public DbManager()
        {
            chatContext = new ChatContext();
        }
    }
}