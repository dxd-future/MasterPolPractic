using System;
using System.Linq;
using System.Windows;
using MasterPol.Data;
using MasterPol.Models;

namespace MasterPol.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = txtUsername.Text.Trim();
            string password = txtPassword.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                ShowError("Введите логин и пароль");
                return;
            }

            try
            {
                using (var context = new CompanySalesContext())
                {
                    var user = context.Users.FirstOrDefault(u => u.Username == login && u.PasswordHash == password);
                    if (user != null)
                    {
                        MainWindow main = new MainWindow(user);
                        main.Show();
                        Close();
                    }
                    else
                    {
                        ShowError("Неверный логин или пароль");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.Visibility = Visibility.Visible;
        }
    }
}