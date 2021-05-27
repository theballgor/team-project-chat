using Client.Commands;
using Client.Services;
using Client.Store;
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

        // 
        public ObservableCollection<User> mContacts;
        public ObservableCollection<User> Contacts
        {
            get => mContacts;
            set
            {
                //To change the list
                if (mContacts == value) return;

                //To update the list
                mContacts = value;

                OnPropertyChanged("Contacts");
            }
        }



        public ICommand NavigateLogOutCommand { get; }

        //Constructor
        public AccountViewModel( NavigationStore navigationStore)
        {
            NavigateLogOutCommand = new NavigateCommand<LoginViewModel>(new NavigationService<LoginViewModel>(
                navigationStore, () => new LoginViewModel(navigationStore)));
        }
    }
}
