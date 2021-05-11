using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientServerLibrary.DbClasses
{
    public class Message : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public string Content { get { return content; } set { content = value; OnPropertyChanged("Content"); } }
        [NotMapped]
        private string content;

        public DateTime SendTime { get { return sendTime; } set { sendTime = value; OnPropertyChanged("SendTime"); } }
        [NotMapped]
        private DateTime sendTime;

        public bool IsRead { get { return isRead; } set { isRead = value; OnPropertyChanged("IsRead"); } }
        [NotMapped]
        private bool isRead;

        [Column("sender_id")]
        public virtual User User { get; set; }
        [Column("conversation_id")]
        public virtual Conversation Conversation { get; set; }

        public int MessageType { get { return messageType; } set { messageType = value; OnPropertyChanged("MessageType"); } }
        [NotMapped]
        private int messageType;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}