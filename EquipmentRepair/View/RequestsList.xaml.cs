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
    /// Логика взаимодействия для RequestsList.xaml
    /// </summary>
    public partial class RequestsList : Window
    {
        private AuthVM _authVM;
        public RequestsList(AuthVM authVM)
        {
            InitializeComponent();
            _authVM = authVM;
            this.DataContext = new RequestVM();
        }

        private void home_Click(object sender, RoutedEventArgs e)
        {
            MainForm mainForm = new MainForm(_authVM);
            mainForm.Show();
            this.Close();
        }
    }
}
