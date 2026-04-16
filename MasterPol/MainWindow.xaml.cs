using System.Windows;
using MasterPol.Models;
using MasterPol.Views;

namespace MasterPol
{
    public partial class MainWindow : Window
    {
        private User _currentUser;

        public MainWindow(User user)
        {
            InitializeComponent();
            _currentUser = user;

            string displayName = string.IsNullOrEmpty(user.Username) ? user.Role : $"{user.Username} ({user.Role})";
            tbUserInfo.Text = $"👤 {displayName}";

            ContentFrame.Content = new PartnersListWindow();
        }

        private void BtnPartners_Click(object sender, RoutedEventArgs e)
            => ContentFrame.Content = new PartnersListWindow();

        private void BtnProducts_Click(object sender, RoutedEventArgs e)
            => ContentFrame.Content = new ProductsWindow();

        private void BtnMaterials_Click(object sender, RoutedEventArgs e)
            => ContentFrame.Content = new MaterialsWindow();

        private void BtnEmployees_Click(object sender, RoutedEventArgs e)
            => ContentFrame.Content = new EmployeesWindow();

        private void BtnWarehouse_Click(object sender, RoutedEventArgs e)
            => ContentFrame.Content = new WarehouseWindow();

        private void BtnProduction_Click(object sender, RoutedEventArgs e)
            => ContentFrame.Content = new ProductionWindow();

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            var login = new LoginWindow();
            login.Show();
            Close();
        }
    }
} 