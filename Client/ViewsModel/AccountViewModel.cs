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
using ClientServerLibrary.DbClasses;
using System.Collections.ObjectModel;
using Client.Model;

namespace Client.ViewsModel
{
    public class AccountViewModel : ViewModelBase
    {
        public ICommand NavigateLogOutCommand { get; }

        public AccountViewModel( NavigationStore navigationStore)
        {
            NavigateLogOutCommand = new NavigateCommand<LoginViewModel>(new NavigationService<LoginViewModel>(
                navigationStore, () => new LoginViewModel(navigationStore)));


            // Events

            AccountModel.LoadData += AccountModel_LoadData;
            foreach (var item in Converstaions)
                item.Value.CollectionChanged += Messages_CollectionsChanged;
            Converstaions.CollectionChanged += Converstaions_CollectionChanged;
        }

        private void AccountModel_LoadData(object sender, EventArgs e)
        {
            Console.WriteLine("Data recieved");

            foreach (var conversation in AccountModel.Conversations)
            {
                Console.WriteLine("Conversation:\t" + conversation.Key.Name);

                foreach (var message in conversation.Value)
                {
                    Console.WriteLine(message.Content);
                }
            }
        }

        private void Converstaions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

        }

        private void Messages_CollectionsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

        }

        public ObservableCollection<KeyValuePair<Conversation, ObservableCollection<Message>>> Converstaions
        {
            get
            {
                return AccountModel.Conversations;
            }
        }


    }
}
