using Client.Commands;
using Client.Model;
using Client.Services;
using Client.Store;
using ClientLibrary;
using ClientServerLibrary.DbClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewsModel
{
    public class AccountViewModel : ViewModelBase
    {

        #region Contacts
        public ObservableCollection<User> _contacts;
        public ObservableCollection<User> Contacts
        {
            get => _contacts;
            set
            {
                //To change the list
                if (_contacts == value) return;

                //To update the list
                _contacts = value;

                OnPropertyChanged("Contacts");
            }
        }
        #endregion



        public ICommand NavigateLogOutCommand { get; }

        //Constructor
        public AccountViewModel( NavigationStore navigationStore)
        {
            AccountModel.RequestContacts();
            AccountModel.GetContactsList += AccountModel_GetContactsList;

            NavigateLogOutCommand = new NavigateCommand<LoginViewModel>(new NavigationService<LoginViewModel>(
                navigationStore, () => new LoginViewModel(navigationStore)));
        }

        private void AccountModel_GetContactsList(object sender, EventArgs e)
        {
            User[] users = ((e as ViewModelEventArgs).Content as User[]);
            if (users != null)
            {
                Contacts = new ObservableCollection<User>();

                foreach (var user in users)
                {
                    Contacts.Add(user);
                }

                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("Failed to contacts list");
            }
        }
    }
}
