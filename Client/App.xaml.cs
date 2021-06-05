﻿using Client.Model;
using Client.Store;
using Client.ViewsModel;
using ClientServerLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
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
            Thread.Sleep(1500);
            try
            {
                Connecting();
                NavigationStore navigationStore = new NavigationStore();
                navigationStore.CurrentViewModel = new LoginViewModel(navigationStore);
                MainWindow = new MainWindow()
                {
                    DataContext = new MainViewModel(navigationStore)
                };
                MainWindow.Show();
            }
            catch (Exception ex)
            {
                File.WriteAllText("errorlog.txt", $"[{DateTime.Now.ToString("T")}]" + ex.Message);
                throw;
            }

            base.OnStartup(e);
        }
        private void Connecting()
        {
            ClientModel.GetInstance().CreateClientEndpoint(GlobalVariables.LocalIP, ClientModel.GetInstance().GetFreeTcpPort());
            ClientModel.GetInstance().Connect(GlobalVariables.LocalIP, GlobalVariables.ServerPort);
            ClientModel.GetInstance().StartListening();
        }
    }
}
