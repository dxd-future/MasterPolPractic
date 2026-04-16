using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MasterPol.Data;

namespace MasterPol.Views
{
    public partial class EmployeesWindow : Page
    {
        public EmployeesWindow()
        {
            InitializeComponent();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            try
            {
                using (var context = new CompanySalesContext())
                {
                    dgEmployees.ItemsSource = context.Employees.OrderBy(e => e.LastName).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки сотрудников: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функция добавления сотрудника будет реализована позже.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmployees.SelectedItem == null)
            {
                MessageBox.Show("Выберите сотрудника для редактирования.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MessageBox.Show("Функция редактирования сотрудника будет реализована позже.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e) => LoadEmployees();
    }
}