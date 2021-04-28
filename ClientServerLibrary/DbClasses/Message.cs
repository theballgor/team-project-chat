using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientServerLibrary.DbClasses
{
    [Table("Messages")]
    [Serializable]
    class Message
    {
        [Key]
        public int Id;
        public string Body;
        public string Time;
        public string IsRead;
        // user_id in tz
        public string Sender_id;

        public string conversation_id;
    }
}