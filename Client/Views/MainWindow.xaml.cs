﻿using Client.CustomerControls;
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
        private void messageBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
        private void buttonMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void buttonMaximaze_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else WindowState = WindowState.Normal;
        }
        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void OpenContactInfoWindow_Click(object sender, RoutedEventArgs e)
        {
            ContactInfoScreen.Visibility = Visibility.Visible;

            //Window window = new Window
            //{
            //    Content = new ContactInfo(),
            //    SizeToContent = SizeToContent.WidthAndHeight,
            //    ResizeMode = ResizeMode.NoResize
            //};

            //window.ShowDialog();
        }

        private void sendMessage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PopupBox_Opened(object sender, RoutedEventArgs e)
        {
            PopupBox.IsPopupOpen = true;
        }
        private void PopupBox_Closed(object sender, RoutedEventArgs e)
        {
            PopupBox.IsPopupOpen = false;
        }
    }
}