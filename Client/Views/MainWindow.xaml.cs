using Client.CustomerControls;
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

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // ToDo send and resive message 
            // Listenet server

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void ButtonMaximaze_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else WindowState = WindowState.Normal;
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //private void OpenContactInfoWindow_Click(object sender, RoutedEventArgs e)
        //{
        //    ContactInfoScreen.Visibility = Visibility.Visible;

        //    //Window window = new Window
        //    //{
        //    //    Content = new ContactInfo(),
        //    //    SizeToContent = SizeToContent.WidthAndHeight,
        //    //    ResizeMode = ResizeMode.NoResize
        //    //};

        //    //window.ShowDialog();
        //}

        //private void PopupBox_Opened(object sender, RoutedEventArgs e)
        //{
        //    PopupBox.IsPopupOpen = true;
        //}
        //private void PopupBox_Closed(object sender, RoutedEventArgs e)
        //{
        //    PopupBox.IsPopupOpen = false;
        //}

        //private void ButtonOpenMenu_Checked(object sender, RoutedEventArgs e)
        //{
        //    var animation = (Storyboard)FindResource("OpenMenu");
        //    animation.Begin(this);

        //    var border = (Border)FindName("GridMain");
        //    border.BorderBrush = new SolidColorBrush(Color.FromArgb(100, 60, 60, 60));
        //    border.CornerRadius = new CornerRadius(6);
        //    border.BorderThickness = new Thickness(2);


        //    /////////////////////
        //    //Button okButton = (Button)this.FindName("PART_OK")

        //    //myBorder1 = new Border();
        //    //myBorder1.BorderBrush = Brushes.SlateBlue;
        //    //myBorder1.BorderThickness = new Thickness(5, 10, 15, 20);
        //    //myBorder1.Background = Brushes.AliceBlue;
        //    //myBorder1.Padding = new Thickness(5);
        //    //myBorder1.CornerRadius = new CornerRadius(15);
        //}

        //private void ButtonOpenMenu_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    var animation = (Storyboard)FindResource("CloseMenu");
        //    animation.Begin(this);

        //    var border = (Border)FindName("GridMain");
        //    border.BorderBrush = Brushes.Transparent;
        //}
    }
}