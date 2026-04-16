using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MasterPol.Data;

namespace MasterPol.Views
{
    public partial class ProductionWindow : Page
    {
        public ProductionWindow()
        {
            InitializeComponent();
            LoadProductionOrders();
        }

        private void LoadProductionOrders()
        {
            try
            {
                using (var context = new CompanySalesContext())
                {
                    var orders = from po in context.ProductionOrders
                                 join p in context.Products on po.ProductId equals p.Id
                                 select new
                                 {
                                     po.Id,
                                     ProductName = p.ProductName,
                                     po.Quantity,
                                     po.StartDate,
                                     po.EndDate,
                                     po.Status
                                 };
                    dgProduction.ItemsSource = orders.OrderByDescending(o => o.StartDate).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки производственных заказов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e) => LoadProductionOrders();
    }
}