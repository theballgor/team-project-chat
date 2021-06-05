using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientServerLibrary.DbClasses
{
    [Table("ConversationConnections")]
    [Serializable]
    public class ConversationConnection
    {
        public int Id { get; set; }
        public virtual Conversation Conversation { get; set; }
        public virtual User User { get; set; }
    }
}