using System;
using System.Collections.Generic;
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
    public partial class AddCountryPage : Window
    {
        public event Action CountryAdded; 

        public AddCountryPage()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string countryName = CountryNameTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(countryName))
            {
                MessageBox.Show("Введите название страны", "Ошибка",
                               MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new task9Entities1())
                {
                    // Проверяем, существует ли уже такая страна
                    if (context.Countries.Any(c => c.CountryName == countryName))
                    {
                        MessageBox.Show("Такая страна уже существует", "Ошибка",
                                      MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    var newCountry = new Countries { CountryName = countryName };
                    context.Countries.Add(newCountry);
                    context.SaveChanges();

                    MessageBox.Show("Страна успешно добавлена", "Успех",
                                  MessageBoxButton.OK, MessageBoxImage.Information);

                    CountryAdded?.Invoke(); 
                    this.Close(); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении страны: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
