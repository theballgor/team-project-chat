using Client.Commands;
using Client.Services;
using Client.Store;
using Client.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewsModel
{
    public class AccountViewModel : ViewModelBase
    {
        public ICommand NavigateLogOutCommand { get; }

        public AccountViewModel( NavigationStore navigationStore)
        {

            NavigateLogOutCommand = new NavigateCommand<LoginViewModel>(new NavigationService<LoginViewModel>(
                navigationStore, () => new LoginViewModel(navigationStore)));
        }



        //}
    }
}
