﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WPFUIKitProfessional.Authorization
{
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginBtn_MouseEnter(object sender, MouseEventArgs e) => Cursor = Cursors.Hand;
        private void LoginBtn_MouseLeave(object sender, MouseEventArgs e) => Cursor = Cursors.Arrow;
        private void TextBlock_MouseEnter(object sender, MouseEventArgs e) => Cursor = Cursors.IBeam;

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var account = Window.GetWindow(this);
            (account as Account).Registration();
            account.Title = "Registration";
        }
    }
}