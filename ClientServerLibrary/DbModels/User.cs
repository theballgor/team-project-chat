using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class User : INotifyPropertyChanged
    {
        public int Id { get; set; }
        [Required]
        public string Username { get { return username; } set { username = value; OnPropertyChanged("Username"); } }
        [NotMapped]
        private string username;
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get { return email; } set { email = value; OnPropertyChanged("Email"); } }
        [NotMapped]
        private string email;
        [DataType(DataType.Password)]
        [Required]
        public string Password { get { return password; } set { password = value; OnPropertyChanged("Password"); } }
        [NotMapped]
        private string password;
        public string Avatar { get { return avatar; } set { avatar = value; OnPropertyChanged("Avatar"); } }
        [NotMapped]
        private string avatar;
        [StringLength(256)]
        public string Description { get { return description; } set { description = value; OnPropertyChanged("Description"); } }
        [NotMapped]
        private string description;
        [DataType(DataType.PhoneNumber)]
        [StringLength(32)]
        public string PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; OnPropertyChanged("PhoneNumber"); } }
        [NotMapped]
        private string phoneNumber;
        public UserStatus Status { get { return status; } set { status = value; OnPropertyChanged("Status"); } }
        [NotMapped]
        private UserStatus status;

        public virtual ICollection<ConversationConnection> ConversationConnections { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}