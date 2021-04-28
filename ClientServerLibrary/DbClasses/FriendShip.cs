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
        pending,
        confirmed,
        blocked
    }
    [Table("FriendShips")]
    [Serializable]
    class FriendShip
    {
        public int FirstUser_id;
        public string SecondUser_id;
        public FriendshipStatus FriendshipStatus;
    }
}