using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientServerLibrary.DbClasses
{
    [Table("ConversationConnections")]
    [Serializable]
    public class ConversationConnection
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("Conversation")]
        public int ConversationId { get; set; }
        public virtual Conversation Conversation { get; set; }
    }
}