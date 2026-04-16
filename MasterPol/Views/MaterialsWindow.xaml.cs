using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MasterPol.Data;

namespace MasterPol.Views
{
    public partial class MaterialsWindow : Page
    {
        public MaterialsWindow()
        {
            InitializeComponent();
            LoadMaterials();
        }

        private void LoadMaterials()
        {
            try
            {
                using (var context = new CompanySalesContext())
                {
                    dgMaterials.ItemsSource = context.MaterialTypes.OrderBy(m => m.MaterialName).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки материалов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функция добавления типа материала будет реализована позже.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgMaterials.SelectedItem == null)
            {
                MessageBox.Show("Выберите материал для редактирования.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MessageBox.Show("Функция редактирования материала будет реализована позже.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e) => LoadMaterials();
    }
}