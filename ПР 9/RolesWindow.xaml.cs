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
    /// Логика взаимодействия для RolesWindow.xaml
    /// </summary>
    public partial class RolesWindow : Window
    {
        public RolesWindow()
        {
            InitializeComponent();
            LoadRoles();
        }
        private void LoadRoles()
        {
            using (var context = new task9Entities1())
            {
                RolesDataGrid.ItemsSource = context.Roles.ToList();
            }
        }
    }
}
