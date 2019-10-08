﻿using System;
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

namespace MoneyLover.UI.Views
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }
        private void ImgShowHide_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            HidePassword();
        }

        private void ImgShowHide_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowPassword();
        }
        private void ImgShowHide_MouseLeave(object sender, MouseEventArgs e)
        {
            HidePassword();
        }
        private void psdPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //if (psdPassword.Password.Length > 0)
            //    psdPassword.Visibility = Visibility.Visible;
            //else
            //    psdPassword.Visibility = Visibility.Hidden;
        }
        public void ShowPassword()
        {
            txtVisiblePasswordbox.Visibility = Visibility.Visible;
            psdPassword.Visibility = Visibility.Hidden;
            txtVisiblePasswordbox.Text = psdPassword.Password;
        }
        public void HidePassword()
        {
            txtVisiblePasswordbox.Visibility = Visibility.Hidden;
            psdPassword.Visibility = Visibility.Visible;
            psdPassword.Focus();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
