using System;
using System.ComponentModel;
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
    public class Friendship : INotifyPropertyChanged
    {
        public int Id { get; set; }

        [Column("inviter_id")]
        public virtual User Inviter { get; set; }
        [Column("requester_id")]
        public virtual User Requester { get; set; }

        public DateTime InviteTime { get { return inviteTime; } set { inviteTime = value; OnPropertyChanged("InviteTime"); } }
        [NotMapped]
        private DateTime inviteTime;
        public FriendshipStatus FriendshipStatus { get { return friendshipStatus; } set { friendshipStatus = value; OnPropertyChanged("FriendshipStatus"); } }
        [NotMapped]
        private FriendshipStatus friendshipStatus;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}