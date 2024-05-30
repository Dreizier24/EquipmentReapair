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
using EquipmentRepair.ViewModel;

namespace EquipmentRepair.View
{
    /// <summary>
    /// Логика взаимодействия для UsersList.xaml
    /// </summary>
    public partial class UsersList : Window
    {
        private AuthVM _authVM;
        public UsersList(AuthVM authVM)
        {
            InitializeComponent();
            _authVM = authVM;
            _authVM.LoadUsers();
            this.DataContext = _authVM;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            UserAdd userAdd = new UserAdd(null);
            userAdd.ShowDialog();
        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            UserAdd changeUser = new UserAdd((DataContext as AuthVM).SelectedEmployee);
            changeUser.ShowDialog();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as AuthVM).DeleteSelectedEmployee();
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            MainForm mainForm = new MainForm(_authVM);
            mainForm.Show();
            this.Close();
        }

        public void RefreshData()
        {
             (DataContext as AuthVM).LoadUsers();
        }
    }
}
