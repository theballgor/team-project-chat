using Client.Model;
using Client.Store;
using Client.Stores;
using Client.ViewsModel;
using ClientServerLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Startup();
            NavigationStore navigationStore = new NavigationStore();
            navigationStore.CurrentViewModel = new LoginViewModel(navigationStore);
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
        private void Startup()
        {
            ClientModel.CreateClientEndpoint(GlobalVariables.LocalIP, ClientModel.GetFreeTcpPort());
            ClientModel.Connect(GlobalVariables.LocalIP, GlobalVariables.ServerPort);
            ClientModel.StartListening();
        }

    }
}
