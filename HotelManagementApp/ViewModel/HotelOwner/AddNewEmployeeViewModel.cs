using HotelManagementApp.DataValidations;
using HotelManagementApp.Interfaces;
using HotelManagementApp.Model;
using HotelManagementApp.View.HotelOwner;
using System;
using System.Collections.Generic;
using System.Windows;

namespace HotelManagementApp.ViewModel.HotelOwner
{
    class AddNewEmployeeViewModel : AddNewUserViewModel
    {
        #region Fields
        private readonly AddNewEmployeeView addNewEmployeeView;
        private tblEmployee employee;
        private IValidatedUserData userData;
        private DataAccess db = new DataAccess();
        private string position;
        private string floorNumber;
        private string citizenship;
        private string sex;
        private string[] sexTypes = new string[] { "m", "f", "x" };
        private string[] positions = new string[] {"cleaning",
"cooking", "monitoring", "reporting" };
        private List<string> floorNumbers;
        #endregion

        #region Properties
        public List<string> FloorNumbers
        {
            get
            {
                return floorNumbers;
            }
            set
            {
                floorNumbers = value;
                OnPropertyChanged(nameof(FloorNumbers));
            }
        }
        public IValidatedUserData UserData
        {
            get
            {
                return userData;
            }
            set
            {
                userData = value;
                OnPropertyChanged(nameof(UserData));
            }
        }
        public bool IsAddedNewEmployee { get; internal set; }
        public string Position
        {
            get
            {
                return position;
            }
            set
            {
                if (position == value) return;
                position = value;
                OnPropertyChanged(nameof(Position));
            }
        }
        public string FloorNumber
        {
            get
            {
                return floorNumber;
            }
            set
            {
                if (floorNumber == value) return;
                floorNumber = value;
                OnPropertyChanged(nameof(FloorNumber));
            }
        }

        public string Citizenship
        {
            get
            {
                return citizenship;
            }
            set
            {
                if (citizenship == value) return;
                citizenship = value;
                OnPropertyChanged(nameof(Citizenship));
            }
        }

        public string Sex
        {
            get
            {
                return sex;
            }
            set
            {
                if (sex == value) return;
                sex = value;
                OnPropertyChanged(nameof(Sex));
            }
        }

        public string[] SexTypes
        {
            get
            {
                return sexTypes;
            }
            set
            {
                if (sexTypes == value) return;
                sexTypes = value;
                OnPropertyChanged(nameof(SexTypes));
            }
        }
        public string[] Positions
        {
            get
            {
                return positions;
            }
            set
            {
                if (positions == value) return;
                positions = value;
                OnPropertyChanged(nameof(Positions));
            }
        }
        #endregion
        #region Constructors
        public AddNewEmployeeViewModel(AddNewEmployeeView addNewEmployeeView, IValidatedUserData userData)
        {
            this.addNewEmployeeView = addNewEmployeeView;
            Position = string.Empty;
            FloorNumber = string.Empty;
            Citizenship = string.Empty;
            Sex = string.Empty;
            employee = new tblEmployee();
            this.userData = userData;
            FloorNumbers = LoadFloorNumbers();
        }

        private List<string> LoadFloorNumbers()
        {
            return db.LoadFloorNumbers();
        }

        #endregion

        #region Methods
        protected override bool CanSaveExecute()
        {
            if (
                string.IsNullOrWhiteSpace(UserData.GivenName)
                || string.IsNullOrWhiteSpace(UserData.Surname)
                || string.IsNullOrWhiteSpace(UserData.Email)
                || string.IsNullOrWhiteSpace(UserData.DateOfBirth)
                || string.IsNullOrWhiteSpace(UserData.Username)
                || string.IsNullOrWhiteSpace(UserData.Password)
                || string.IsNullOrWhiteSpace(FloorNumber)
                || string.IsNullOrWhiteSpace(Citizenship)
                || string.IsNullOrWhiteSpace(Sex)
                || string.IsNullOrWhiteSpace(Position)
                || UserData.CanSave == false)
                return false;
            return true;
        }
        protected override void SaveExecute()
        {
            try
            {
                UserData.UserData.GivenName = UserData.GivenName;
                UserData.UserData.Surname = UserData.Surname;
                UserData.UserData.Email = UserData.Email;
                UserData.UserData.Username = UserData.Username;
                UserData.UserData.Password = SecurePasswordHasher.Hash(UserData.Password);
                UserData.UserData.DateOfBirth = UserData.DateOfBirthValue; 
                //adding new employee to database 
                db.TryAddNewUserData(UserData.UserData);

                var userId = db.GetUserDataId(UserData.Username);
                if (userId != 0)
                {
                    employee.UserDataID = userId;
                    employee.Position = Position;
                    employee.FloorNumber = FloorNumber;
                    employee.Sex = Sex;
                    IsAddedNewEmployee = db.TryAddNewEmployee(employee);
                    if (!IsAddedNewEmployee)
                    {
                        MessageBox.Show("Something went wrong. The new employee is not created.");
                        db.TryRemoveUserData(userId);
                    }
                    else
                    {
                        MessageBox.Show("The new employee is sucessfully created.");
                    }
                }
                else
                {
                    MessageBox.Show("Something went wrong. The new employee is not created.");
                }
                var ownerView = new HotelOwnerView();
                ownerView.Show();
                addNewEmployeeView.Close();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        protected override void ExitExecute()
        {
            IsAddedNewEmployee = false;
            var ownerView = new HotelOwnerView();
            ownerView.Show();
            addNewEmployeeView.Close();
        }
        #endregion
    }
}
