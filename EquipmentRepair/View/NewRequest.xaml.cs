using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.Http.Headers;
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

namespace EquipmentRepair.View
{
    /// <summary>
    /// Логика взаимодействия для NewRequest.xaml
    /// </summary>
    public partial class NewRequest : Window
    {
        private Request _request;
        private List<Fault> _faults;

        public NewRequest(Request request)
        {
            InitializeComponent();

            foreach (var item in App.Current.Windows)
            {
                if (item is AllRequests)
                    this.Owner = item as Window;
            }

            if (request == null)
            {
                _request = new Request();
            }
            else
            {
                _request = request;
            }

            _request.date_added = DateTime.Now;
            _request.status_id = 1;

            this.DataContext = _request;

            LoadFaults();
        }

        private void LoadFaults()
        {
            using (var db = new EquipmentRepairDBEntities())
            {
                _faults = db.Fault.ToList();
                FaultTypeComboBox.ItemsSource = _faults;
            }
        }

        private void saveNewRequest_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new EquipmentRepairDBEntities())
            {
                try
                {
                    db.Request.AddOrUpdate(_request);
                    db.SaveChanges();
                    MessageBox.Show("Данные успешно сохранены!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    (Owner as AllRequests)?.RefreshData();
                    Owner.Focus();
                    this.Close();
                }
                catch (Exception ex)
                {
                    /*string requestDetails = $"Request Details:\n" +
                                            $"Date Added: {_request.date_added}\n" +
                                            $"Equipment: {_request.equipment}\n" +
                                            $"Fault Type: {_request.fault_type}\n" +
                                            $"Problem Description: {_request.problem_description}\n" +
                                            $"Client Name: {_request.client_fullname}\n" +
                                            $"Client Name: {_request.client_phoneNumber}\n" +
                                            $"Status ID: {_request.status_id}\n";*/

                    MessageBox.Show($"Произошла ошибка\nПодробности:\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
