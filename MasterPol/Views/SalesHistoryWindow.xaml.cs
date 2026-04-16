using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MasterPol.Data;

namespace MasterPol.Views
{
    public partial class SalesHistoryWindow : Window
    {
        private int _partnerId;
        private string _partnerName;
        private int? _selectedTypeId;

        public SalesHistoryWindow(int partnerId, string partnerName)
        {
            InitializeComponent();
            _partnerId = partnerId;
            _partnerName = partnerName;
            tbPartnerName.Text = $"История продаж партнёра: {_partnerName}";
            LoadTypes();
            LoadSales();
        }

        private void LoadTypes()
        {
            try
            {
                using (var context = new CompanySalesContext())
                {
                    var types = context.ProductTypes.OrderBy(t => t.TypeName).ToList();
                    types.Insert(0, new Models.ProductType { Id = 0, TypeName = "Все" });
                    cbType.ItemsSource = types;
                    cbType.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки типов продукции: {ex.Message}", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadSales()
        {
            try
            {
                using (var context = new CompanySalesContext())
                {
                    var baseQuery = from s in context.Sales
                                    join p in context.Products on s.ProductId equals p.Id
                                    where s.PartnerId == _partnerId
                                    select new
                                    {
                                        s.Id,
                                        ProductName = p.ProductName,
                                        ProductTypeId = p.ProductTypeId,
                                        s.Quantity,
                                        s.SaleDate
                                    };

                    if (dpStartDate.SelectedDate.HasValue)
                        baseQuery = baseQuery.Where(s => s.SaleDate >= dpStartDate.SelectedDate.Value);
                    if (dpEndDate.SelectedDate.HasValue)
                        baseQuery = baseQuery.Where(s => s.SaleDate <= dpEndDate.SelectedDate.Value);

                    if (_selectedTypeId.HasValue)
                        baseQuery = baseQuery.Where(s => s.ProductTypeId == _selectedTypeId.Value);

                    var sales = baseQuery.OrderByDescending(s => s.SaleDate).ToList();
                    dgSales.ItemsSource = sales;

                    int totalQty = sales.Sum(s => s.Quantity);
                    tbTotalQuantity.Text = totalQty.ToString("N0");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки истории продаж: {ex.Message}", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DateFilter_Changed(object sender, SelectionChangedEventArgs e) => LoadSales();
        private void CbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbType.SelectedItem == null) return;
            var selected = cbType.SelectedItem as Models.ProductType;
            if (selected != null && selected.Id == 0)
                _selectedTypeId = null;
            else
                _selectedTypeId = selected?.Id;

            LoadSales();
        }
        private void BtnClearDates_Click(object sender, RoutedEventArgs e)
        {
            dpStartDate.SelectedDate = null;
            dpEndDate.SelectedDate = null;
            LoadSales();
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e) => Close();
    }
}