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
    /// Логика взаимодействия для StaffWindow.xaml
    /// </summary>
    public partial class StaffWindow : Window
    {
        public event Action UsersUpdated;
        public StaffWindow()
        {
            InitializeComponent();
            RolesComboBox.ItemsSource = Data.task9Entities1.GetContext().Roles.ToList();
            LoadUsers();
        }
        public List<UserViewModel> UsersContext()
        {
            using (task9Entities1 context2 = new task9Entities1())
            {
                return context2.Users.Select(u => new UserViewModel
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        CountryName = u.Countries.CountryName,
                        RoleName = u.Roles.RoleName,         
                        GenderName = u.Gender.Gender1
                    }
                ).ToList();

            }
        }
        private void LoadUsers()
        {
            UserDataGrid.ItemsSource = UsersContext();
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            AddUser addUser = new AddUser();
            addUser.UserAdded += LoadUsers;
            addUser.ShowDialog();
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            UserViewModel selectedUser = UserDataGrid.SelectedItem as UserViewModel; 
            if (selectedUser == null)
            {
                MessageBox.Show("Выберите пользователя для удаления", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new task9Entities1())
                {
                    int userIdToDelete = selectedUser.Id;
                    var userToDelete = context.Users.Find(userIdToDelete);

                    if (userToDelete != null)
                    {

                        context.Users.Remove(userToDelete);
                        context.SaveChanges();

                        LoadUsers();
                        UsersUpdated?.Invoke();

                        MessageBox.Show("Пользователь успешно удален", "Успех",
                                      MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении пользователя: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            UserViewModel selectedUser = UserDataGrid.SelectedItem as UserViewModel;
            if (selectedUser == null)
            {
                MessageBox.Show("Выберите пользователя для изменения", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new task9Entities1())
                {

                    var UserToChange = context.Countries.Find(selectedUser.Id);

                    if (UserToChange != null)
                    {

                        if (UserToChange.Users != null && UserToChange.Users.Any())
                        {
                            var result = MessageBox.Show("Изменить данные пользователя?",
                                                       "Подтверждение",
                                                       MessageBoxButton.YesNo,
                                                       MessageBoxImage.Question);

                            //if (result != MessageBoxResult.Yes) return;
                        }

                        ChangeUser changeUser = new ChangeUser();
                        changeUser.FirstNameBox.Text = selectedUser.FirstName;
                        changeUser.RolesBox.Text = selectedUser.RoleName;
                        changeUser.CountryBox.Text = selectedUser.CountryName;
                        changeUser.GenderBox.Text = selectedUser.GenderName;
                        changeUser.label1.Content = selectedUser.Id;
                        changeUser.Show();
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

        private void RolesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (task9Entities1 context = new task9Entities1())
            {
                if (RolesComboBox.SelectedItem == null)
                {
                    LoadUsers(); 
                }
                else
                {
                    int selectedRoleId = ((Roles)RolesComboBox.SelectedItem).Id;

                    UserDataGrid.ItemsSource = context.Users
                        .Where(u => u.IdRole == selectedRoleId)
                        .AsEnumerable() // Добавлено AsEnumerable()
                        .Select(u => new UserViewModel // Создание UserViewModel без конструктора
                        {
                            Id = u.Id,
                            FirstName = u.FirstName,
                            CountryName = context.Countries.Find(u.IdCountry)?.CountryName, // Используем context для доступа к связанным сущностям
                            RoleName = context.Roles.Find(u.IdRole)?.RoleName,
                            GenderName = context.Gender.Find(u.IdGender)?.Gender1

                        })
                        .ToList();
                }
            }
        }
    }
}

