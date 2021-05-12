using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client.Views
{
    /// <summary>
    /// Логика взаимодействия для RegistrationForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void buttonRegistration_Click(object sender, RoutedEventArgs e)
        {
            RegistrationForm registrationForm = new RegistrationForm();
            this.Close();
            registrationForm.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
