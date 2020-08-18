using HotelManagementApp.Command;
using HotelManagementApp.Model;
using HotelManagementApp.View.Manager;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace HotelManagementApp.ViewModel.Manager
{
    class GetNumberFromManagerViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Fields
        private GetNumberFromManagerView getNumberView;
        private string number;
        private int numberValue;
        private int employeeId;
        private double salary;
        private int managerId;
        DataAccess db = new DataAccess();
        #endregion

        #region Properties
        public string Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
                OnPropertyChanged(nameof(Number));
            }
        }
        public bool CanSave { get; set; }
        #endregion

        #region Constructors
        public GetNumberFromManagerViewModel(GetNumberFromManagerView getNumberView, int employeeId, int managerId)
        {
            this.getNumberView = getNumberView;
            Number = string.Empty;
            this.employeeId = employeeId;
            this.managerId = managerId;
        }
        #endregion

        #region IDataErrorInfoImplementation
        //validations

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string name]
        {
            get
            {
                CanSave = true;
                string validationMessage = string.Empty;
                if (name == nameof(Number))
                {
                    if (!int.TryParse(Number, out numberValue) ||
                        numberValue < 2 || numberValue > 999)
                    {
                        validationMessage = "Invalid format! Number must be between 2-999.";
                        CanSave = false;
                    }
                }
                if (string.IsNullOrEmpty(validationMessage))
                    CanSave = true;
                return validationMessage;
            }
        }
        #endregion
        #region Commands
        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        private bool CanSaveExecute()
        {
            if (string.IsNullOrWhiteSpace(Number))
                return false;
            return true;
        }
        private void SaveExecute()
        {
            try
            {
                var db = new DataAccess();
                salary = GenerateSatary();

                var isUpdated = db.TryUpdateEmloyeeSalary(employeeId, salary.ToString());
                if (isUpdated)
                    MessageBox.Show("Salary is succesfully generated!");
                else
                    MessageBox.Show("Something went wrong. Salary is not generated.");

                ManagerView managerView = new ManagerView(managerId);
                managerView.Show();
                getNumberView.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private double GenerateSatary()
        {
            var manager = db.LoadManagerById(managerId);
            var i = 0.75 * manager.WorkExperience;
            var s = 0.15 * GetLevelValue(manager.ProfessionalQualificationsLevel);
            var p = GetPValue();
            var x = numberValue;
            return (1000 * i * s * p + x);
        }

        private double GetPValue()
        {
            string sex = db.LoadEmployeeSex(employeeId);
            if (sex == "m")
                return 1.12;
            else
                return 1.15;
        }

        private int GetLevelValue(string p)
        {
            if (p == "I")
                return 1;
            if (p == "II")
                return 2;
            if (p == "III")
                return 3;
            if (p == "IV")
                return 4;
            if (p == "V")
                return 5;
            if (p == "VI")
                return 6;
            if (p == "VII")
                return 7;
            return 0;
        }

        private ICommand exit;
        public ICommand Exit
        {
            get
            {
                if (exit == null)
                {
                    exit = new RelayCommand(param => ExitExecute(), param => CanExitExecute());
                }
                return exit;
            }
        }

        private bool CanExitExecute()
        {
            return true;
        }

        private void ExitExecute()
        {
            ManagerView managerView = new ManagerView(managerId);
            managerView.Show();
            getNumberView.Close();
        }
        #endregion
    }
}
