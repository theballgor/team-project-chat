using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientServerLibrary.DbClasses
{
    public class Conversation : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public string Theme { get { return theme; } set { theme = value; OnPropertyChanged("Theme"); } }
        [NotMapped]
        private string theme;

        public string Avatar { get { return avatar; } set { avatar = value; OnPropertyChanged("Avatar"); } }
        [NotMapped]
        private string avatar;

        public string Status { get { return status; } set { status = value; OnPropertyChanged("Status"); } }
        [NotMapped]
        private string status;

        public string StreamingPort { get { return streamingPort; } set { streamingPort = value; OnPropertyChanged("StreamingPort"); } }
        [NotMapped]
        private string streamingPort;

        [Column("streamingUser_id")]
        public virtual User User { get; set; }

        public virtual ICollection<UserConversation> UserConversation { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}