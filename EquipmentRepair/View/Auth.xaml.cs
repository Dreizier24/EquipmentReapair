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
using System.Windows.Navigation;
using System.Windows.Shapes;
using EquipmentRepair.View;
using EquipmentRepair.ViewModel;

namespace EquipmentRepair
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AuthVM authVM = new AuthVM();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = authVM;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            authVM.AuthInApp();
            if (authVM.IsAuthenticated)
            {
                this.Close();
            }
        }
    }
}
