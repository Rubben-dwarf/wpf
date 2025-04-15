using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public event Action UserAdded;
        public AddUser()
        {
            InitializeComponent();
            LoadComboBoxes();
        }
        private void LoadComboBoxes()
        {
            RolesBox.ItemsSource = Data.test9EntitiesL.GetContext().Roles.ToList();
            CountryBox.ItemsSource = Data.test9EntitiesL.GetContext().Countries.ToList();
            GenderBox.ItemsSource = Data.test9EntitiesL.GetContext().Gender.ToList();
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            string UserName = FirstNameBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(UserName))
            {
                MessageBox.Show("Введите название страны", "Ошибка",
                               MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new test9EntitiesL())
                {
                    if (context.Users.Any(c => c.FirstName == UserName))
                    {
                        MessageBox.Show("Такой человек уже существует", "Ошибка",
                                      MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if (CountryBox.SelectedItem != null && RolesBox.SelectedItem != null && GenderBox.SelectedItem != null)
                    {
                        int countryId = ((Countries)CountryBox.SelectedItem).Id;
                        int roleId = ((Roles)RolesBox.SelectedItem).Id;
                        int genderId = ((Gender)GenderBox.SelectedItem).Id;
                        var newUser = new Users { FirstName = UserName, IdRole = roleId, IdCountry = countryId, IdGender = genderId };
                        context.Users.Add(newUser);
                        try
                        {
                            context.SaveChanges();
                            MessageBox.Show("Пользователь успешно добавлен!");
                            UserAdded?.Invoke();
                            this.Close();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка: {ex.Message}");
                        }

                        
                    }
                    else
                    {
                        MessageBox.Show("Выберите все поля!");
                    }
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

