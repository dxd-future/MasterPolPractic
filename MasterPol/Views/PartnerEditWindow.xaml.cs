using System;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using MasterPol.Data;
using MasterPol.Models;

namespace MasterPol.Views
{
    public partial class PartnerEditWindow : Window
    {
        private Partner _partner;
        private CompanySalesContext _context;

        public PartnerEditWindow(Partner partner)
        {
            InitializeComponent();
            _context = new CompanySalesContext();
            _partner = partner ?? new Partner();
            LoadData();
        }

        private void LoadData()
        {
            if (_partner.Id > 0)
            {
                cbPartnerType.Text = _partner.PartnerType;
                txtName.Text = _partner.Name;
                txtLastName.Text = _partner.LastName;
                txtFirstName.Text = _partner.FirstName;
                txtMiddleName.Text = _partner.MiddleName;
                txtEmail.Text = _partner.Email;
                txtPhoneCountry.Text = _partner.PhoneCountryCode;
                txtPhoneCity.Text = _partner.PhoneCityCode;
                txtPhoneNumber.Text = _partner.PhoneNumber;
                txtPostalCode.Text = _partner.AddressPostalCode;
                txtRegion.Text = _partner.AddressRegion;
                txtCity.Text = _partner.AddressCity;
                txtStreet.Text = _partner.AddressStreet;
                txtBuilding.Text = _partner.AddressBuilding;
                txtInn.Text = _partner.Inn;
                txtRating.Text = _partner.Rating?.ToString();
            }
        }

        private bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            { MessageBox.Show("Введите название компании.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning); return false; }
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            { MessageBox.Show("Введите фамилию директора.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning); return false; }
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            { MessageBox.Show("Введите имя директора.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning); return false; }
            if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            { MessageBox.Show("Введите номер телефона.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning); return false; }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(txtEmail.Text);
                    if (addr.Address != txtEmail.Text.Trim())
                        throw new Exception();
                }
                catch
                {
                    MessageBox.Show("Введите корректный email.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }

            string inn = txtInn.Text.Trim();
            if (!string.IsNullOrEmpty(inn) && !Regex.IsMatch(inn, @"^\d{10}$|^\d{12}$"))
            {
                MessageBox.Show("ИНН должен содержать 10 или 12 цифр.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtRating.Text))
            {
                if (!int.TryParse(txtRating.Text, out int rating) || rating < 0 || rating > 100)
                {
                    MessageBox.Show("Рейтинг должен быть целым числом от 0 до 100.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }

            return true;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateData()) return;

            try
            {
                _partner.PartnerType = (cbPartnerType.SelectedItem as ComboBoxItem)?.Content.ToString();
                _partner.Name = txtName.Text.Trim();
                _partner.LastName = txtLastName.Text.Trim();
                _partner.FirstName = txtFirstName.Text.Trim();
                _partner.MiddleName = txtMiddleName.Text.Trim();
                _partner.Email = txtEmail.Text.Trim();
                _partner.PhoneCountryCode = txtPhoneCountry.Text.Trim();
                _partner.PhoneCityCode = txtPhoneCity.Text.Trim();
                _partner.PhoneNumber = txtPhoneNumber.Text.Trim();
                _partner.AddressPostalCode = txtPostalCode.Text.Trim();
                _partner.AddressRegion = txtRegion.Text.Trim();
                _partner.AddressCity = txtCity.Text.Trim();
                _partner.AddressStreet = txtStreet.Text.Trim();
                _partner.AddressBuilding = txtBuilding.Text.Trim();
                _partner.Inn = txtInn.Text.Trim();
                _partner.Rating = string.IsNullOrWhiteSpace(txtRating.Text) ? (int?)null : int.Parse(txtRating.Text);

                if (_partner.Id == 0)
                    _context.Partners.Add(_partner);
                else
                    _context.Entry(_partner).State = EntityState.Modified;

                _context.SaveChanges();
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            _context.Dispose();
            base.OnClosed(e);
        }
    }
}