using Client.Commands;
using Client.Model;
using Client.Services;
using Client.Store;
using ClientLibrary;
using ClientServerLibrary.DbClasses;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.ViewsModel
{
    public partial class AccountViewModel : ViewModelBase
    {
        //Constructor
        public AccountViewModel(NavigationStore navigationStore)
        {
            NavigateLogOutCommand = new NavigateCommand<LoginViewModel>(new NavigationService<LoginViewModel>(
                navigationStore, () => new LoginViewModel(navigationStore)));

            // Events
            // ОПИСАНІ В ПАРТІАЛ КЛАСІ НИЖЧЕ
            AccountModel.LoadConversations += AccountModel_LoadConversations;
            AccountModel.GetContactsList += AccountModel_GetContactsList1;
            AccountModel.LoadMessages += AccountModel_LoadMessages;
            AccountModel.Conversations.CollectionChanged += Conversations_CollectionChanged;
            AccountModel.Messages.CollectionChanged += AllMessages_CollectionChanged;

            foreach (var messages in Messages)
                messages.CollectionChanged += Messages_CollectionChanged;
        }

        // Fields

                /// Static fields
        public ObservableCollection<User> Contacts
        {
            get => AccountModel.Contacts;
        }
        public ObservableCollection<Conversation> Conversations
        {
            get => AccountModel.Conversations;
        }
        public ObservableCollection<ObservableCollection<Message>> Messages
        {
            get => AccountModel.Messages;
        }
        public User User
        {
            get => AccountModel.User;
        }


        public string MessageContent
        {
            get => AccountModel.MessageContent;
            set
            {
                AccountModel.MessageContent = value; OnPropertyChanged("MessageContent");
            }
        }
        
        // ???
        public List<KeyValuePair<string, byte[]>> MessageFiles
        {
            get => AccountModel.MessageFiles;
            set => MessageFiles = value;
        }

        // Commands

        public ICommand NavigateLogOutCommand { get; }
        protected ICommand _sendMessageCommand;
        protected ICommand _selectFileCommand;
        // TODO
        public ICommand SendMessageCommand
        {
            get
            {
                return _sendMessageCommand ?? (_sendMessageCommand = new RelayCommand(parameter =>
                {
                    AccountModel.SendMessage();
                }));
            }
        }
        public ICommand SelectFileCommand
        {
            get
            {
                return _selectFileCommand ?? (_selectFileCommand = new RelayCommand(parameter =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == true)
                    {
                        foreach (string path in openFileDialog.FileNames)
                            MessageFiles.Add(new KeyValuePair<string, byte[]>(Path.GetFileName(path), File.ReadAllBytes(path)));
                    }
                }));
            }
        }

    }


    public partial class AccountViewModel : ViewModelBase
    {

        private void Messages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AllMessages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Conversations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AccountModel_LoadMessages(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AccountModel_GetContactsList1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AccountModel_LoadConversations(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
