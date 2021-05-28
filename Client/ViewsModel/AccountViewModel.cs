using Client.Commands;
using Client.Model;
using Client.Services;
using Client.Store;
using Client.Stores;
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
    public class AccountViewModel : ViewModelBase
    {
        ObservableCollection<Message> messages;
        public ObservableCollection<Message> Messages { get { return messages == null ? messages = new ObservableCollection<Message>() : messages; } set { messages = value; } }

        private static List<KeyValuePair<string, byte[]>> messageFiles;
        public static List<KeyValuePair<string, byte[]>> MessageFiles { get { return messageFiles == null ? messageFiles = new List<KeyValuePair<string, byte[]>>() : messageFiles; } set { messageFiles = value; } }

        public string MessageContent { get { return AccountModel.MessageContent; } set { AccountModel.MessageContent = value; OnPropertyChanged("MessageContent"); } }

        public ICommand NavigateLogOutCommand { get; }
        protected ICommand _sendMessageCommand;
        protected ICommand _selectFileCommand;


        public ICommand SendMessageCommand { get { return _sendMessageCommand ?? (_sendMessageCommand = new RelayCommand(parameter => 
        {
            Message msg = AccountModel.SendMessage(MessageContent, MessageFiles);
            Messages.Add(msg); 
        })); } }

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

        public AccountViewModel( NavigationStore navigationStore)
        {

            NavigateLogOutCommand = new NavigateCommand<LoginViewModel>(new NavigationService<LoginViewModel>(
                navigationStore, () => new LoginViewModel(navigationStore)));


            //SendTextMessageCommand = new 
        }
    }
}
