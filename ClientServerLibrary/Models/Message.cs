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
    public enum MessageType
    {
        Text,
        Audio,
        File,
        Image
    }
    [Table("Messages")]
    [Serializable]
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [StringLength(4000)]
        [Required]
        public string Content { get; set; }
        public DateTime SendTime { get; set; }
        public bool IsRead { get; set; }

        [ForeignKey("Sender")]
        public int SenderId { get; set; }
        public virtual User Sender { get; set; }
        [ForeignKey("Conversation")]
        public int ConversationId { get; set; }
        public virtual Conversation Conversation { get; set; }
        public MessageType MessageType { get; set; }
    }
}