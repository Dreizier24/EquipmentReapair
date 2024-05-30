using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using EquipmentRepair.DBStorage;

namespace EquipmentRepair.ViewModel
{
    public class RequestVM : BaseViewModel
    {
        private ObservableCollection<Request> _request;
        public ObservableCollection<Request> Request
        {
            get => _request;
            set 
            {
                _request = value;
                OnPropertyChanged(nameof(Request));
            }
        }

        private Request _selectedRequest;
        public Request SelectedRequest
        {
            get => _selectedRequest;
            set
            {
                _selectedRequest = value;
                OnPropertyChanged(nameof(SelectedRequest));
            }
        }


        private int _id;
        public int RequestId 
        { 
            get => _id;
            set 
            { 
                _id = value;
                OnPropertyChanged(nameof(RequestId));
            }
        }

        private System.DateTime _date_added;
        public System.DateTime date_added 
        { 
            get => _date_added; 
            set 
            { 
                _date_added = value;
                OnPropertyChanged(nameof(date_added)); 
                    } 
        }

        private string _equipment;
        public string equipment
        {
            get => _equipment;
            set
            {
                _equipment = value;
                OnPropertyChanged(nameof(equipment));
            }
        }

        private int _fault_type;
        public int fault_type
        {
            get => _fault_type;
            set
            {
                _fault_type = value;
                OnPropertyChanged(nameof(fault_type));
            }
        }

        private string _problem_description;
        public string problem_description
        {
            get => _problem_description;
            set
            {
                _problem_description = value;
                OnPropertyChanged(nameof(problem_description));
            }
        }

        private string _client_fullname;
        public string client_fullname
        {
            get => _client_fullname;
            set
            {
                _client_fullname = value;
                OnPropertyChanged(nameof(client_fullname));
            }
        }

        private string _client_phoneNumber;
        public string client_phoneNumber
        {
            get => _client_phoneNumber;
            set
            {
                _client_phoneNumber = value;
                OnPropertyChanged(nameof(client_phoneNumber));
            }
        }

        private int _status_id;
        public int status_id
        {
            get => _status_id;
            set
            {
                _status_id = value;
                OnPropertyChanged(nameof(status_id));
            }
        }

        private Nullable<int> _priority;
        public Nullable<int> priority
        {
            get => _priority;
            set
            {
                _priority = value;
                OnPropertyChanged(nameof(priority));
            }
        }

        private Nullable<int> _executor_id;
        public Nullable<int> executor_id
        {
            get => _executor_id;
            set
            {
                _executor_id = value;
                OnPropertyChanged(nameof(executor_id));
            }
        }
        private Nullable<System.DateTime> _date_start;
        public Nullable<System.DateTime> date_start
        {
            get => _date_start;
            set
            {
                _date_start = value;
                OnPropertyChanged(nameof(date_start));
            }
        }

        private Nullable<System.DateTime> _date_end;
        public Nullable<System.DateTime> date_end
        {
            get => _date_end;
            set
            {
                _date_end = value;
                OnPropertyChanged(nameof(date_end));
            }
        }
        private string _materials;
        public string materials
        {
            get => _materials;
            set
            {
                _materials = value;
                OnPropertyChanged(nameof(materials));
            }
        }
        private Nullable<decimal> _price;
        public Nullable<decimal> price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(price));
            }
        }

        private string _fault_reason;
        public string fault_reason
        {
            get => _fault_reason;
            set
            {
                _fault_reason = value;
                OnPropertyChanged(nameof(fault_reason));
            }
        }
        private string _provided_help;
        public string provided_help
        {
            get => _provided_help;
            set
            {
                _provided_help = value;
                OnPropertyChanged(nameof(provided_help));
            }
        }





        private List<Fault> _faults;
        public List<Fault> Faults
        {
            get => _faults;
            set
            {
                _faults = value;
                OnPropertyChanged(nameof(Faults));
            }
        }

        private void LoadFaults()
        {
            using (var db = new EquipmentRepairDBEntities())
            {
                Faults = db.Fault.ToList();
            }
        }

         private List<Status> _statuses;
         public List<Status> Statuses
         {
             get => _statuses;
             set
             {
                 _statuses = value;
                 OnPropertyChanged(nameof(Statuses));
             }
         }

         private void LoadStatus()
         {
             using (var db = new EquipmentRepairDBEntities())
             {
                 Statuses = db.Status.ToList();
             }
         }

        private int _selectedFaultId;
        public int SelectedFaultId
        {
            get => _selectedFaultId;
            set
            {
                _selectedFaultId = value;
                OnPropertyChanged(nameof(SelectedFaultId));
            }
        }

        private int _selectedStatusId;
        public int SelectedStatusId
        {
            get => _selectedStatusId;
            set
            {
                _selectedStatusId = value;
                OnPropertyChanged(nameof(SelectedStatusId));
            }
        }

        public void LoadData()
        {
            if (Request.Count > 0)
                Request.Clear();
            var res = DBStorage.DBStorage.DB_s.Request.ToList();
            res.ForEach(elem => Request?.Add(elem));
        }

        public RequestVM()
        {
            Request = new ObservableCollection<Request>();
            LoadData();
            LoadFaults();
            LoadStatus();
        }


        public void DeleteSelectedData()
        {
            if (!(SelectedRequest is null))
            {
                using (var db = new EquipmentRepairDBEntities())
                {
                    var res = MessageBox.Show("Вы уверены, что хотите удалить выбранный элемент?\nЭто действие невозможно отменить", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (res == MessageBoxResult.Yes)
                    {
                        try
                        {
                            var forDelete = db.Request.Where(elem => elem.id == SelectedRequest.id).FirstOrDefault();
                            db.Request.Remove(forDelete);
                            db.SaveChanges();
                            LoadData();
                            MessageBox.Show("Данные успешно удалены!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Произошла ошибка\nПодробности:\n{ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }
    }
}
