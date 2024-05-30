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
    /// Логика взаимодействия для MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {
        private AuthVM _authVM;
        public MainForm(AuthVM authVM)
        {
            InitializeComponent();
            _authVM = authVM;
        }

        private void checkRequests_Click(object sender, RoutedEventArgs e)
        {

        }

        private void listRequests_Click(object sender, RoutedEventArgs e)
        {
            RequestsList requestsList = new RequestsList(_authVM);
            requestsList.Show();
            this.Close();
        }

        private void newRequest_Click(object sender, RoutedEventArgs e)
        {
            AllRequests allRequests = new AllRequests(_authVM);
            allRequests.ShowDialog();
            this.Close();
        }

        private void userManage_Click(object sender, RoutedEventArgs e)
        {
            UsersList usersList = new UsersList(_authVM);
            usersList.Show();
            this.Close();
        }
    }
}
