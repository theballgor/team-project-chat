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
    public enum UserStatus
    {
        Online,
        Ofline,
        DoNotDisturb
    }
    [Table("Users")]
    [Serializable]
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        public string Avatar { get; set; }
        [StringLength(256)]
        public string Description { get;  set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public UserStatus UserStatus { get; set; }

        public virtual ObservableListSource<MessageModel> Messages { get; set; }
        public virtual ObservableListSource<Friendship> Friendships { get; set; }
        public virtual ObservableListSource<ConversationConnection> ConversationConnections { get; set; }
    }
}