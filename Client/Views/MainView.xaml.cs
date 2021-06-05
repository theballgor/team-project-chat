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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void ButtonOpenMenu_Checked(object sender, RoutedEventArgs e)
        {
            var animation = (Storyboard)FindResource("OpenMenu");
            animation.Begin(this);

            var border = (Border)FindName("GridMain");
            border.BorderBrush = new SolidColorBrush(Color.FromArgb(100, 60, 60, 60));
            border.CornerRadius = new CornerRadius(6);
            border.BorderThickness = new Thickness(2);
        }

        private void ButtonOpenMenu_Unchecked(object sender, RoutedEventArgs e)
        {
            var animation = (Storyboard)FindResource("CloseMenu");
            animation.Begin(this);

            var border = (Border)FindName("GridMain");
            border.BorderBrush = Brushes.Transparent;
        }

        private void PopupBox_Opened(object sender, RoutedEventArgs e)
        {
            PopupBox.IsPopupOpen = true;
        }

        private void PopupBox_Closed(object sender, RoutedEventArgs e)
        {
            PopupBox.IsPopupOpen = false;
        }

        private void buttonOpenContactList_Checked(object sender, RoutedEventArgs e)
        {
            var ContactList = (Border)FindName("ContactList");
            ContactList.Visibility = Visibility.Visible;
        }

        private void buttonOpenContactList_Unchecked(object sender, RoutedEventArgs e)
        {
            var gridContactList = (Border)FindName("ContactList");
            gridContactList.Visibility = Visibility.Hidden;
        }

        private void Conversation_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
