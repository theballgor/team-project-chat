using Client.Commands;
using Client.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ClientServerLibrary;
using System.Net;

namespace Client.ViewsModel
{
    /// <summary>
    /// MVVM
    /// </summary>
    partial class DataManageVM : INotifyPropertyChanged
    {
        ClientModel client;
        private string email;
        private string username;
        private string password;


        public void Load()
        {
            client = new ClientModel(new IPEndPoint(GlobalVariables.LocalIP, 5001));
            client.Connect(new IPEndPoint(GlobalVariables.LocalIP, GlobalVariables.ServerPort));
            client.StartListening();
        }


        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
    }

}

