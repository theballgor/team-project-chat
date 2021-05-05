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
    public class ConversationModel
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
        [StringLength(256)]
        public string Description { get; set; }
        public string Avatar { get; set; }
        public ConversationAccessibility ConversationAccessibility { get; set; }
        [ForeignKey("Creator")]
        public int CreatorId { get; set; }
        public virtual UserModel Creator { get; set; }
        public virtual ObservableListSource<ConversationConnection> ConversationConnections { get; set; }
        public virtual ObservableListSource<MessageModel> Messages { get; set; }
    }
}