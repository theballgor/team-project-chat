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
    public class Friendship
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Requester")]
        public int RequesterId { get; set; }
        public virtual User Requester { get; set; }
        [ForeignKey("Inviter")]
        public int InviterId { get; set; }
        public virtual User Inviter { get; set; }
        public DateTime InviteTime { get; set; }
        public FriendshipStatus FriendshipStatus { get; set; }
    }
}