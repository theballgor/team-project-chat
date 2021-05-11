using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientServerLibrary.DbClasses
{
    /*[Serializable]
    public enum UserStatus
    {
        Online,
        Ofline,
        DoNotDisturb
    }
    [Table("Users")]
    [Serializable]
    class User
    {
        public int Id { get;}
        public string Username { get;private set; }
        public string Email { get; private set;}
        public string Password { get; private set; }
        public string Avatar { get; private set; }
        public string Description { get;  set; }
        public string PhoneNumber { get; set; }
        public UserStatus UserStatus { get; set; }
    }*/

    public class User : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public string Username { get { return username; } set { username = value; OnPropertyChanged("Username"); } }
        [NotMapped]
        private string username;

        public string Email { get { return email; } set { email = value; OnPropertyChanged("Email"); } }
        [NotMapped]
        private string email;

        public string Password { get { return password; } set { password = value; OnPropertyChanged("Password"); } }
        [NotMapped]
        private string password;

        public string Avatar { get { return avatar; } set { avatar = value; OnPropertyChanged("Avatar"); } }
        [NotMapped]
        private string avatar;

        public int Status { get { return status; } set { status = value; OnPropertyChanged("Status"); } }
        [NotMapped]
        private int status;

        public virtual ICollection<UserConversation> UserConversation { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}