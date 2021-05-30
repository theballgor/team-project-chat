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
                AccountModel.MessageContent = value;
                OnPropertyChanged("MessageContent");
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
        protected ICommand _selectFileCommand;
        protected ICommand _getConversationMessagesCommand;
        protected ICommand _sendMessageCommand;
        protected ICommand _getSelectedChatCommand;

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
        public ICommand GetSelectedChatCommand
        {
            get
            {
                return _getSelectedChatCommand ?? (_getSelectedChatCommand = new RelayCommand(parameter =>
                {
                    if (parameter is Conversation currentConversation)
                    {
                        LoadConversationMessages(currentConversation);
                    }
                }));
            }
        }

        private void LoadConversationMessages(Conversation currentConversation)
        {
            AccountModel.GetConversationMessages(currentConversation.Id);
        }


        // Server-commands
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

        public ICommand GetConversationMessagesCommand
        {
            get
            {
                return _getConversationMessagesCommand ?? (_getConversationMessagesCommand = new RelayCommand(parameter =>
                {
                    //AccountModel.GetConversationMessages((int)parameter);
                    AccountModel.GetConversationMessages(0);
                }));
            }
        }
    }
}
