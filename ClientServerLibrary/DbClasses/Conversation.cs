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
    public enum ConversationAccessibility
    {
        Public,
        Private
    }

    [Table("Conversations")]
    [Serializable]
    public class Conversation
    {
        [Key]
        public int Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Avatar { get; private set; }
        public ConversationAccessibility ConversationAccessibility { get; set; }
        public string Creator_id { get; set; }
    }
}