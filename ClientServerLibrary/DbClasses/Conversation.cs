using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ClientServerLibrary.DbClasses
{
    [Serializable]
    public enum ConversationAccessibility
    {
        Public,
        Private
    }
    [Table("Conversations")]
    [Serializable]
    public class Conversation : INotifyPropertyChanged
    {

        public int Id { get; set; }
        [Required]
        [StringLength(32)]
        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }
        [NotMapped]
        private string name;
        [StringLength(256)]
        public string Description { get { return description; } set { description = value; OnPropertyChanged("Description"); } }
        [NotMapped]
        private string description;

        public string Avatar { get { return avatar; } set { avatar = value; OnPropertyChanged("Avatar"); } }
        [NotMapped]
        private string avatar;

        public ConversationAccessibility ConversationAccessibility { get { return conversationAccessibility; } set { conversationAccessibility = value; OnPropertyChanged("ConversationAccessibility"); } }
        [NotMapped]
        private ConversationAccessibility conversationAccessibility;

        [NotMapped]
        public string StreamingPort { get { return streamingPort; } set { streamingPort = value; OnPropertyChanged("StreamingPort"); } }
        [NotMapped]
        private string streamingPort;
        [Column("userCreator_id")]
        [ForeignKey("Creator")]
        public virtual User Creator { get; set; }
        public virtual ICollection<ConversationConnection> ConversationConnections { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}