using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MasterPol.Data;

namespace MasterPol.Views
{
    public partial class WarehouseWindow : Page
    {
        public WarehouseWindow()
        {
            InitializeComponent();
            LoadWarehouse();
        }

        private void LoadWarehouse()
        {
            try
            {
                using (var context = new CompanySalesContext())
                {
                    var stocks = from ws in context.WarehouseStocks
                                 join p in context.Products on ws.ProductId equals p.Id
                                 select new
                                 {
                                     ws.Id,
                                     ProductName = p.ProductName,
                                     ws.Quantity,
                                     ws.LastUpdated
                                 };
                    dgWarehouse.ItemsSource = stocks.OrderBy(s => s.ProductName).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки складских остатков: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e) => LoadWarehouse();
    }
}