using Client.Commands;
using Client.Model;
using Client.Services;
using Client.Store;
using ClientServerLibrary.DbClasses;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace Client.ViewsModel
{
    public partial class AccountViewModel : ViewModelBase
    {
        private readonly NavigationStore navigationStore;
        //Constructor
        public AccountViewModel(NavigationStore navigationStore)
        {
            this.navigationStore = navigationStore;
        }


        /// Залогінений користувач
        /// Прив"язка до форми
        public User User
        {
            get => AccountModel.User;
        }

        /// Список контаків
        /// Прив"язка до форми
        public ObservableCollection<User> Contacts
        {
            get => AccountModel.Contacts;
        }

        /// Список чатів та останніх повідомлень в цих чатах
        /// Прив"язка до форми
        public ObservableCollection<KeyValuePair<ConversationModel, Message>> Conversations
        {
            get => AccountModel.Conversations;
            /// Список контаків
        }

        /// Активний чат
        public ConversationModel ActiveConversation
        {
            get => AccountModel.ActiveConversation;
        }

        /// Список повідомлень в активному чаті та булівське яке показує чи повідолення наше, чи чиєсь
        /// Прив"язка до форми
        public ObservableCollection<KeyValuePair<Message, ObservableCollection<MessageFile>>> ActiveMessages
        {
            get => AccountModel.ActiveMessages;
        }

        /// Повідомлення яке ми вписуємо в чаті
        /// Відправка по кліку
        /// Двостороння прив"язка між формою та моделлю
        public string MessageContent
        {
            get => AccountModel.MessageContent;
            set
            {
                AccountModel.MessageContent = value;
                OnPropertyChanged("MessageContent");
            }
        }

        /// Колекція файлових повідомлень, 1-назва(шлях), 2-байтовий масив
        /// Відправка по кліку разом з текстовим повідомелнням
        /// Двостороння прив"язка між формою та моделлю
        public ObservableCollection<MessageFile> MessageFiles
        {
            get => AccountModel.MessageFiles;
            set => MessageFiles = value;
        }

        /// Ім"я активного чату
        /// Прив"язка до форми
        public string ContactName { get => ActiveConversation.Name; set { } }

        /// Текстобокс в якому ми вводимо ім"я користувача якого ми шукаємо для додавання в друзі
        /// Відправка по кліку
        /// Двостороння прив"язка між формою та моделлю
        public string FindFriendByUsername
        {
            get => AccountModel.FindFriendByUsername;
            set { AccountModel.FindFriendByUsername = value; OnPropertyChanged("FindFriendByUsername"); }
        }

        /// Список людей який сервер підшукав нам за пошуком по нікнейму
        /// Прив"язка до форми
        ObservableCollection<User> FindFriends
        {
            get => AccountModel.FindFriends;
        }

        /// Запити на дружбу які відправили нам
        /// Прив"язка до форми
        public ObservableCollection<User> FriendRequests
        {
            get => AccountModel.FriendRequests;
        }



        /// Кнопка розлогінитись
        protected ICommand _navigateLogOutCommand;
        public ICommand NavigateLogOutCommand
        {
            get
            {
                return _navigateLogOutCommand ?? (_navigateLogOutCommand = new RelayCommand(parameter =>
                {
                    new NavigationService<LoginViewModel>(navigationStore, () => new LoginViewModel(navigationStore)).Navigate();
                }));
            }
        }

        /// Кнопка вибрати файл
        protected ICommand _selectFileCommand;
        public ICommand SelectFileCommand
        {
            get
            {
                return _selectFileCommand ?? (_selectFileCommand = new RelayCommand(parameter =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Multiselect = true;
                    if (openFileDialog.ShowDialog() == true)
                    {
                        foreach (string path in openFileDialog.FileNames)
                            MessageFiles.Add(new MessageFile(Path.GetFileName(path), File.ReadAllBytes(path)));
                    }
                }));
            }
        }

        /// Зміна активного чату
        /// Потрібно передавати параметром чат на який було натиснуто
        protected ICommand _onConversationChanged;
        public ICommand OnConversationChanged
        {
            get
            {
                return _onConversationChanged ?? (_onConversationChanged = new RelayCommand(parameter =>
                {

                    if (parameter is ConversationModel currentConversation && currentConversation != AccountModel.ActiveConversation)
                    {
                        AccountModel.ActiveMessages.Clear();
                        AccountModel.ActiveConversation = currentConversation;
                        AccountModel.GetConversationMessages();
                    }
                }));
            }
        }

        /// Кнопка створення нового чату
        /// TODO
        protected ICommand _createConversation;
        public ICommand CreateConversation
        {
            get
            {
                return _createConversation ?? (_createConversation = new RelayCommand(parameter =>
                {
                    // HARDCODE
                    Console.WriteLine("CRFEATE");
                    AccountModel.CreateConversation();
                }));
            }
        }

        /// Кнопка відправлення повідомлення
        /// TODO
        protected ICommand _sendMessageCommand;
        public ICommand SendMessageCommand
        {
            get
            {
                return _sendMessageCommand ?? (_sendMessageCommand = new RelayCommand(parameter =>
                {
                    AccountModel.SendMessage();

                    //if (FindFriends.Count == 0)
                    //    AccountModel.GetUsersByUsername();
                    //else
                    //{
                    //    AccountModel.SendFriendRequest(FindFriends[0]);
                    //}

                    //if (FriendRequests.Count > 0)
                    //    AccountModel.ConfirmFriendRequestCommand(FriendRequests[0]);

                }));
            }
        }

        public ICommand GetConversationMessagesCommand
        {
            get
            {
                return _sendMessageCommand ?? (_sendMessageCommand = new RelayCommand(parameter =>
                {
                    if (parameter is User userToRequest)
                    {
                        AccountModel.SendFriendRequest(userToRequest);
                    }
                }));
            }
        }

        /// Кнопка прийняти запит на дружбу
        /// Потрібно передавати параметром на формі юзера біля якого була натиснута кнопка
        protected ICommand _confirmFriendRequestCommand;
        public ICommand ConfirmFriendRequestCommand
        {
            get
            {
                return _confirmFriendRequestCommand ?? (_confirmFriendRequestCommand = new RelayCommand(parameter =>
                {
                    if (parameter is User userToRequest)
                    {
                        AccountModel.ConfirmFriendRequestCommand(userToRequest);
                    }
                }));
            }
        }


        /// Кнопка відхилити запит на дружбу
        /// Потрібно передавати параметром на формі юзера біля якого була натиснута кнопка
        protected ICommand _declineFriendRequestCommand;
        public ICommand DeclineFriendRequestCommand
        {
            get
            {
                return _declineFriendRequestCommand ?? (_declineFriendRequestCommand = new RelayCommand(parameter =>
                {
                    if (parameter is User userToRequest)
                    {
                        AccountModel.DeclineFriendRequestCommand(userToRequest);
                        AccountModel.FriendRequests.Remove(userToRequest);
                    }
                }));
            }
        }

        protected ICommand _startConversation;
        public ICommand StartConversation
        {
            get
            {
                return _startConversation ?? (_startConversation = new RelayCommand(parameter =>
                {
                    if (parameter is User userToChat)
                    {
                        AccountModel.ActiveConversation = new ConversationModel { Name = userToChat.Username, Creator = AccountModel.User };
                    }
                }));
            }
        }
        

    }
}
