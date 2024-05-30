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
using EquipmentRepair.DBStorage;
using EquipmentRepair.View;
using EquipmentRepair.ViewModel;

namespace EquipmentRepair.View
{
    /// <summary>
    /// Логика взаимодействия для AllRequests.xaml
    /// </summary>
    public partial class AllRequests : Window
    {
        private Request _request;
        private AuthVM _authVM;

        public AllRequests(AuthVM authVM)
        {
            InitializeComponent();
            _authVM = authVM;
            this.DataContext = new RequestVM();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            NewRequest newRequest = new NewRequest(null);
            newRequest.ShowDialog();
        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            NewRequest newRequest = new NewRequest((DataContext as RequestVM).SelectedRequest);
            newRequest.ShowDialog();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as RequestVM).DeleteSelectedData();
        }

        public void RefreshData()
        {
            (DataContext as RequestVM).LoadData();
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            if (_authVM.Post != 4)
            {
                MainForm mainForm = new MainForm(_authVM);
                mainForm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Недостаточно прав","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}
