using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientServerLibrary.DbClasses
{
    /*[Serializable]
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
    }*/

    public class Friendship : INotifyPropertyChanged
    {
        public int Id { get; set; }

        [Column("inviter_id")]
        public virtual User User1 { get; set; }
        [Column("friend_id")]
        public virtual User User2 { get; set; }

        public DateTime InviteTime { get { return inviteTime; } set { inviteTime = value; OnPropertyChanged("InviteTime"); } }
        [NotMapped]
        private DateTime inviteTime;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}