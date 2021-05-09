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
    public enum FriendshipStatus
    {
        Pending,
        Confirmed,
        Blocked
    }
    [Table("FriendShips")]
    [Serializable]
    public class FriendshipModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Requester")]
        public int RequesterId { get; set; }
        public virtual UserModel Requester { get; set; }
        [ForeignKey("Inviter")]
        public int InviterId { get; set; }
        public virtual UserModel Inviter { get; set; }
        public DateTime InviteTime { get; set; }
        public FriendshipStatus FriendshipStatus { get; set; }
    }
}