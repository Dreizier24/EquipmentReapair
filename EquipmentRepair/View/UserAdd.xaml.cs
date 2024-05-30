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
using EquipmentRepair.DBStorage;
using System.Data.Entity.Migrations;

namespace EquipmentRepair.View
{
    /// <summary>
    /// Логика взаимодействия для UserAdd.xaml
    /// </summary>
    public partial class UserAdd : Window
    {
        private Employee _employee;
        private List<Post> _posts;
        public UserAdd(Employee employee)
        {
            InitializeComponent();

            foreach (var item in App.Current.Windows) 
            {
                if (item is UsersList)
                    this.Owner = item as Window;
            }

            if (employee == null)
            {
                _employee = new Employee();
            }
            else
            {
                _employee = employee;
            }


            LoadPosts();
            this.DataContext = _employee;
        }

        private void LoadPosts()
        {
            using (var db = new EquipmentRepairDBEntities())
            {
                _posts = db.Post.ToList();
                PostsTypeComboBox.ItemsSource = _posts;
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new EquipmentRepairDBEntities())
            {
                try
                {
                    db.Employee.AddOrUpdate(_employee);
                    db.SaveChanges();
                    MessageBox.Show("Данные успешно сохранены!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    (Owner as UsersList)?.RefreshData();
                    Owner.Focus();
                    this.Close();
                }
                catch (Exception ex)
                {
                    /*string msg = $"name {_employee.name}\nsurname {_employee.surname}\nlastname {_employee.lastname}\n" +
                        $"number {_employee.phoneNumber}\nlogin {_employee.login}\npassword {_employee.password}\npost {_employee.post}";*/

                    MessageBox.Show($"Произошла ошибка\nПодробности\n{ex}","Ошибка",MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
