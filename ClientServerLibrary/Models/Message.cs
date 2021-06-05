using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace ClientServerLibrary.DbClasses
{
    [Table("Messages")]
    [Serializable]
    public class Message : INotifyPropertyChanged
    {
        public int Id { get; set; }
        [StringLength(4000)]
        [Required]
        public string Content { get { return content; } set { content = value; OnPropertyChanged("Content"); } }
        [NotMapped]
        private string content;
        public DateTime SendTime { get { return sendTime; } set { sendTime = value; OnPropertyChanged("SendTime"); } }
        [NotMapped]
        private DateTime sendTime;
        public bool IsRead { get { return isRead; } set { isRead = value; OnPropertyChanged("IsRead"); } }
        [NotMapped]
        private bool isRead;

        [NotMapped]
        [field: NonSerialized]
        public bool IsSend { get; set; }

        [Column("sender_id")]
        public virtual User Sender { get; set; }
        [Column("conversation_id")]
        public virtual ConversationModel Conversation { get; set; }
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}