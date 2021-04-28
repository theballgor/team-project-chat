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
    class Message
    {
        [Key]
        public int Id { get; }
        public string Content { get; set; }
        public DateTime SendTime { get; }
        public bool IsRead { get; set; }
        public string Sender_id { get; set; }
        public string Conversation_id { get; set; }
        public MessageType MessageType { get; set; }
    }
}