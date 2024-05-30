using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EquipmentRepair.DBStorage;
using EquipmentRepair.View;

namespace EquipmentRepair.ViewModel
{
    public class AuthVM : BaseViewModel
    {
        private Employee _employee;

        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }

        private int _id;
        public int Id
        {
            get => _id;
            set {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private int _post;
        public int Post
        {
            get => _post;
            set
            {
                _post = value;
                OnPropertyChanged(nameof(Post));
            }
        }

        private bool _isAuthenticated;
        public bool IsAuthenticated
        {
            get => _isAuthenticated;
            set
            {
                _isAuthenticated = value;
                OnPropertyChanged(nameof(IsAuthenticated));
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _surname;
        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        private string _lastname;
        public string Lastname
        {
            get => _lastname;
            set
            {
                _lastname = value;
                OnPropertyChanged(nameof(Lastname));
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        private ObservableCollection<Post> _posts;
        public ObservableCollection<Post> Posts
        {
            get => _posts;
            set
            {
                _posts = value;
                OnPropertyChanged(nameof(Posts));
            }
        }

        public AuthVM()
        {
            Employees = new ObservableCollection<Employee>();
            Posts = new ObservableCollection<Post>();
            LoadUsers();
        }

        public void LoadUsers()
        {
            if (Employees.Count > 0)
                Employees.Clear();
            var res = DBStorage.DBStorage.DB_s.Employee.ToList();
            res.ForEach(elem => Employees?.Add(elem));
        }

        public bool Authorization(string login, string password)
        {
            using (var db = new EquipmentRepairDBEntities())
            {
                var res = db.Employee.FirstOrDefault(employee => employee.login == login && employee.password == password);
                _employee = res;
                if (res != null)
                {
                    IsAuthenticated = true;
                    _post = res.post;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void AuthInApp()
        {
            IsAuthenticated = Authorization(Login, Password);
            if (IsAuthenticated)
            {
                    if (Post == 1)
                    {
                        MainForm mainForm = new MainForm(this);
                        mainForm.Show();
                    }
                    else if (Post == 2)
                    {
                        AllRequests allRequests = new AllRequests(this);
                        allRequests.Show();
                    }
                    else if (Post == 3)
                    {
                        AllRequests allRequests = new AllRequests(this);
                        allRequests.Show();
                    }
                    else if (Post == 4)
                    {
                        AllRequests allRequests = new AllRequests(this);
                        allRequests.Show();
                    }
                    else
                    {
                        MessageBox.Show($"Произошла ошибка авторизации\nПожалуйста, обратитесь к сис. адм.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль\n Или такого пользователя не существует",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteSelectedEmployee()
        {
            if (!(SelectedEmployee is null)) 
            {
                using (var db = new EquipmentRepairDBEntities())
                {
                    var res = MessageBox.Show("Вы уверены, что хотите удалить пользователя?\nЭто действие невозможно отменить", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (res == MessageBoxResult.Yes)
                    {
                        try
                        {
                            var forDelete = db.Employee.Where(elem => elem.id == SelectedEmployee.id).FirstOrDefault();
                            db.Employee.Remove(forDelete);
                            db.SaveChanges();
                            LoadUsers();
                            MessageBox.Show("Данные успешно удалены!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Произошла ошибка\nПодробности:\n{ex}","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
                        }
                    }
                }
            }
        }
    }
}  
