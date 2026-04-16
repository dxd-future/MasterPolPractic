using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MasterPol.Data;

namespace MasterPol.Views
{
    public partial class ProductsWindow : Page
    {
        public ProductsWindow()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            try
            {
                using (var context = new CompanySalesContext())
                {
                    var products = from p in context.Products
                                   join pt in context.ProductTypes on p.ProductTypeId equals pt.Id into ptJoin
                                   from pt in ptJoin.DefaultIfEmpty()
                                   join mt in context.MaterialTypes on p.MaterialTypeId equals mt.Id into mtJoin
                                   from mt in mtJoin.DefaultIfEmpty()
                                   select new
                                   {
                                       p.Id,
                                       p.Article,
                                       p.ProductName,
                                       ProductTypeName = pt != null ? pt.TypeName : "Не указан",
                                       MaterialTypeName = mt != null ? mt.MaterialName : "Не указан",
                                       p.MinCostForPartner
                                   };
                    dgProducts.ItemsSource = products.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки продукции: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функция добавления продукции будет реализована позже.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgProducts.SelectedItem == null)
            {
                MessageBox.Show("Выберите продукт для редактирования.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MessageBox.Show("Функция редактирования продукции будет реализована позже.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e) => LoadProducts();
    }
}