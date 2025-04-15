using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ПР_9.Data;

namespace ПР_9
{
    public partial class CountriesWindow : Window
    {
        public event Action CountriesUpdated; // Событие для уведомления об изменениях

        public CountriesWindow()
        {
            InitializeComponent();
            LoadCountries();
        }

        private void LoadCountries()
        {
            using (var context = new test9EntitiesL())
            {
                CountriesDataGrid.ItemsSource = context.Countries.ToList();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCountry = CountriesDataGrid.SelectedItem as Countries;
            if (selectedCountry == null)
            {
                MessageBox.Show("Выберите страну для удаления", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new test9EntitiesL())
                {
                   
                    var countryToDelete = context.Countries.Find(selectedCountry.Id);

                    if (countryToDelete != null)
                    {
                        
                        if (countryToDelete.Users != null && countryToDelete.Users.Any())
                        {
                            var result = MessageBox.Show("Эта страна имеет связанных пользователей. Удалить её?",
                                                       "Подтверждение",
                                                       MessageBoxButton.YesNo,
                                                       MessageBoxImage.Question);

                            if (result != MessageBoxResult.Yes) return;

                            
                            context.Users.RemoveRange(countryToDelete.Users);
                        }

                        context.Countries.Remove(countryToDelete);
                        context.SaveChanges();

                        LoadCountries(); 
                        CountriesUpdated?.Invoke(); 

                        MessageBox.Show("Страна успешно удалена", "Успех",
                                      MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении страны: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCountry = CountriesDataGrid.SelectedItem as Countries;
            if (selectedCountry == null)
            {
                MessageBox.Show("Выберите страну для изменения", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new test9EntitiesL())
                {

                    var countryToChange = context.Countries.Find(selectedCountry.Id);

                    if (countryToChange != null)
                    {

                        if (countryToChange.Users != null && countryToChange.Users.Any())
                        {
                            var result = MessageBox.Show("Изменить данные о стране?",
                                                       "Подтверждение",
                                                       MessageBoxButton.YesNo,
                                                       MessageBoxImage.Question);

                            //if (result != MessageBoxResult.Yes) return;
                        }

                        ChangeCountry changeCountry = new ChangeCountry();
                        changeCountry.ChangeBox.Text = selectedCountry.CountryName;
                        changeCountry.label1.Content = selectedCountry.Id;
                        changeCountry.Show();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выборе страны: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
