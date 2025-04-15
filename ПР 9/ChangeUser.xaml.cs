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
    /// <summary>
    /// Логика взаимодействия для ChangeUser.xaml
    /// </summary>
    public partial class ChangeUser : Window
    {
        public event Action UserChanged;
        public ChangeUser()
        {
            InitializeComponent();
            RolesBox.ItemsSource = Data.test9EntitiesL.GetContext().Roles.ToList();
            CountryBox.ItemsSource = Data.test9EntitiesL.GetContext().Countries.ToList();
            GenderBox.ItemsSource = Data.test9EntitiesL.GetContext().Gender.ToList();
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FirstNameBox.Text))
            {
                MessageBox.Show("Введите имя пользователя", "Ошибка",
                               MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new test9EntitiesL())
                {
                    var rowToUpdate = context.Users.Find(Convert.ToInt32(label1.Content));
                    int countryId = ((Countries)CountryBox.SelectedItem).Id;
                    int roleId = ((Roles)RolesBox.SelectedItem).Id;
                    int genderId = ((Gender)GenderBox.SelectedItem).Id;
                    rowToUpdate.FirstName = FirstNameBox.Text;
                    rowToUpdate.IdCountry = countryId;
                    rowToUpdate.IdRole = roleId;
                    rowToUpdate.IdGender = genderId;
                    context.SaveChanges();

                    MessageBox.Show("Данные пользователя успешно изменены", "Успех",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                    UserChanged?.Invoke();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка изменении данных: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
