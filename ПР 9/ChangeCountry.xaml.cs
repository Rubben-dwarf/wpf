using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using System.Xml.Linq;
using ПР_9.Data;

namespace ПР_9
{
    /// <summary>
    /// Логика взаимодействия для ChangeCountry.xaml
    /// </summary>
    public partial class ChangeCountry : Window
    {
        public event Action CountryChanged;
        public ChangeCountry()
        {
            InitializeComponent();
        }


        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ChangeBox.Text))
            {
                MessageBox.Show("Введите название страны", "Ошибка",
                               MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new test9EntitiesL())
                {
                    // Проверяем, существует ли уже такая страна
                    if (context.Countries.Any(c => c.CountryName == Name))
                    {
                        MessageBox.Show("Такая страна уже существует", "Ошибка",
                                      MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    var rowToUpdate = context.Countries.Find(Convert.ToInt32(label1.Content)); // YourTable - ваша таблица
                    rowToUpdate.CountryName = ChangeBox.Text;
                    context.SaveChanges();

                    MessageBox.Show("Страна успешно изменена", "Успех",
                                  MessageBoxButton.OK, MessageBoxImage.Information);

                    CountriesWindow countriesWindow = new CountriesWindow();
                    countriesWindow.Show(); 
                    CountryChanged?.Invoke();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении страны: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        
        }

        private void ChangeBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
