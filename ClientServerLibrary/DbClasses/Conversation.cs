using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientServerLibrary.DbClasses
{
    [Serializable]
    public enum ConversationStatus
    {
        pending,
        confirmed,
        blocked
    }
    [Table("Conversations")]
    [Serializable]
    class Conversation
    {
        [Key]
        public int Id;
        public string Theme;
        public string Avatart;
        public ConversationStatus ConversationStatus;
        public string StreamingPort;
        // user_id in tz
        public string host_id;
    }
}