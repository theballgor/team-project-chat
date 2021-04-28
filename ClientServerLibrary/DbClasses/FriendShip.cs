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
    class Friendship
    {
        public int Inviter_id { get; }
        public string Friend_id { get; }
        public DateTime InviteTime { get; }
        public FriendshipStatus FriendshipStatus { get; set; }
    }
}