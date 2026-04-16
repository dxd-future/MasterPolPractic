using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MasterPol.Data;
using MasterPol.Models;

namespace MasterPol.Views
{
    public partial class PartnersListWindow : Page
    {
        private List<Partner> _allPartners;

        public PartnersListWindow()
        {
            InitializeComponent();
            cbFilterType.SelectedIndex = 0;
            LoadPartners();
        }

        private void LoadPartners()
        {
            try
            {
                using (var context = new CompanySalesContext())
                {
                    _allPartners = context.Partners.OrderBy(p => p.Name).ToList();
                    ApplyFilterAndSearch();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки партнёров: {ex.Message}", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ApplyFilterAndSearch()
        {
            var filtered = _allPartners.AsEnumerable();

            string selectedType = (cbFilterType.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (selectedType != "Все")
                filtered = filtered.Where(p => p.PartnerType == selectedType);

            string searchText = txtSearch.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                filtered = filtered.Where(p => p.Name.ToLower().Contains(searchText) ||
                                               $"{p.LastName} {p.FirstName} {p.MiddleName}".ToLower().Contains(searchText));
            }

            dgPartners.ItemsSource = filtered.OrderBy(p => p.Name).ToList();
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e) => ApplyFilterAndSearch();
        private void BtnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            cbFilterType.SelectedIndex = 0;
            txtSearch.Text = "";
            ApplyFilterAndSearch();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var editWin = new PartnerEditWindow(null);
            if (editWin.ShowDialog() == true)
                LoadPartners();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgPartners.SelectedItem is Partner selected)
            {
                var editWin = new PartnerEditWindow(selected);
                if (editWin.ShowDialog() == true)
                    LoadPartners();
            }
            else
                MessageBox.Show("Выберите партнёра для редактирования.", "Внимание",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void BtnHistory_Click(object sender, RoutedEventArgs e)
        {
            if (dgPartners.SelectedItem is Partner selected)
            {
                var historyWin = new SalesHistoryWindow(selected.Id, selected.Name);
                historyWin.ShowDialog();
            }
            else
                MessageBox.Show("Выберите партнёра для просмотра истории продаж.", "Внимание",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e) => LoadPartners();
    }
}