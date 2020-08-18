using HotelManagementApp.Command;
using HotelManagementApp.Model;
using HotelManagementApp.View.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace HotelManagementApp.ViewModel.Manager
{
    class ManagerViewModel : LogoutViewModel
    {
        #region Fields
        private List<vwEmployee> employees;
        private vwEmployee employee;
        private readonly ManagerView managerView;
        private readonly DataAccess db = new DataAccess();
        private string floor;
        private decimal salary;
        int managerId;
        private Array positions = Enum.GetValues(typeof(PositionsEnum));
        #endregion
        #region Constructors
        public ManagerViewModel(ManagerView managerView, int managerId)
        {
            this.managerView = managerView;
            this.managerId = managerId;
            Employees = LoadEmployees();
           
        }

        private List<vwEmployee> LoadEmployees()
        {
            if(db.LoadManagerById(managerId) != null)
            {
                floor = db.LoadManagerById(managerId).FloorNumber;
            }
            return db.LoadEmployees(floor);
        }
        #endregion
        #region Methods
        protected override void ExitExecute()
        {
            MainWindow loginWindow = new MainWindow();
            managerView.Close();
            loginWindow.Show();
        }
        #endregion
        #region Properies
        public vwEmployee Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
                OnPropertyChanged(nameof(Employee));
            }
        }
        public List<vwEmployee> Employees
        {
            get
            {
                return employees;
            }
            set
            {
                employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }
        #endregion

        //Generate Salary
        #region Commands

        //adding new emloyee

        private ICommand generateSalary;
        public ICommand GenerateSalary
        {
            get
            {
                if (generateSalary == null)
                {
                    generateSalary = new RelayCommand(param => GenerateSalaryExecute(), param => CanGenerateSalary());
                }
                return generateSalary;
            }
        }
        private void GenerateSalaryExecute()
        {
            try
            {
                if (Employee != null)
                {
                    if(Employee.Position == positions.GetValue(2).ToString()
                        || Employee.Position == positions.GetValue(3).ToString())
                    {
                        GetNumberFromManagerView getNumberView = new GetNumberFromManagerView(Employee.EmployeeID, managerId);
                        getNumberView.Show();
                        managerView.Close();
                    }
                    else
                    {
                        GetNumberFromManagerView getNumberView = new GetNumberFromManagerView(Employee.EmployeeID, managerId);
                        getNumberView.Show();
                        managerView.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanGenerateSalary()
        {
            return true;
        }
        #endregion
    }
}
