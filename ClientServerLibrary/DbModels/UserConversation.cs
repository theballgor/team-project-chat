using System.ComponentModel;

namespace ClientServerLibrary.DbClasses
{
    public class UserConversation : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public virtual Conversation Conversation { get; set; }
        public virtual User User { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}