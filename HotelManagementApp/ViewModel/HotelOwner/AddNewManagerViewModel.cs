using HotelManagementApp.DataValidations;
using HotelManagementApp.Interfaces;
using HotelManagementApp.Model;
using HotelManagementApp.View.HotelOwner;
using System;
using System.ComponentModel;
using System.Windows;

namespace HotelManagementApp.ViewModel.HotelOwner
{
    class AddNewManagerViewModel : AddNewUserViewModel, IDataErrorInfo
    {
        #region Fields
        private readonly AddNewManagerView addNewManagerView;
        private tblManager manager;
        private IValidatedUserData userData;
        private DataAccess db = new DataAccess();
        private string professionalQualificationsLevel;
        private string floorNumber;
        private string workExperience;
        private int workExperienceValue;
        private string[] professionalQualificationsLevels = new string[] { "I", "II", "III", "IV", "V", "VI", "VII" };
        #endregion

        #region Properties
    
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
        public bool IsAddedNewManager { get; internal set; }
    
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
        public string ProfessionalQualificationsLevel
        {
            get
            {
                return professionalQualificationsLevel;
            }
            set
            {
                if (professionalQualificationsLevel == value) return;
                professionalQualificationsLevel = value;
                OnPropertyChanged(nameof(ProfessionalQualificationsLevel));
            }
        }
        public string WorkExperience
        {
            get
            {
                return workExperience;
            }
            set
            {
                if (workExperience == value) return;
                workExperience = value;
                OnPropertyChanged(nameof(WorkExperience));
            }
        }
        public string[] ProfessionalQualificationsLevels
        {
            get
            {
                return professionalQualificationsLevels;
            }
            set
            {
                if (professionalQualificationsLevels == value) return;
                professionalQualificationsLevels = value;
                OnPropertyChanged(nameof(ProfessionalQualificationsLevels));
            }
        }

        #endregion
        #region Constructors
        public AddNewManagerViewModel(AddNewManagerView addNewManagerView, IValidatedUserData userData)
        {
            this.addNewManagerView = addNewManagerView;
            ProfessionalQualificationsLevel = string.Empty;
            FloorNumber = string.Empty;
            WorkExperience = string.Empty;
            manager = new tblManager();
            this.userData = userData;
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
                UserData.CanSave = true;
                var db = new DataAccess();
                string validationMessage = string.Empty;
                if (name == nameof(WorkExperience))
                {
                    if (!int.TryParse(WorkExperience, out workExperienceValue) || 
                        workExperienceValue < 0)
                    {
                        validationMessage = "Invalid format! Number must be positive.";
                        UserData.CanSave = false;
                    }
                }
                if (string.IsNullOrEmpty(validationMessage))
                    UserData.CanSave = true;
                return validationMessage;
            }
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
                || string.IsNullOrWhiteSpace(WorkExperience)
                || string.IsNullOrWhiteSpace(ProfessionalQualificationsLevel)
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
                //adding new manager to database 
                db.TryAddNewUserData(UserData.UserData);

                var userId = db.GetUserDataId(UserData.Username);
                if (userId != 0)
                {
                    manager.UserDataID = userId;
                    manager.ProfessionalQualificationsLevel = ProfessionalQualificationsLevel;
                    manager.FloorNumber = FloorNumber;
                    manager.WorkExperience = workExperienceValue;
                    IsAddedNewManager = db.TryAddNewManager(manager);
                    if (!IsAddedNewManager)
                    {
                        MessageBox.Show("Something went wrong. The new manager is not created.");
                        db.TryRemoveUserData(userId);
                    }
                    else
                    {
                        MessageBox.Show("The new manager is sucessfully created.");
                    }
                }
                else
                {
                    MessageBox.Show("Something went wrong. The new manager is not created.");
                }
                var ownerView = new HotelOwnerView();
                ownerView.Show();
                addNewManagerView.Close();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        protected override void ExitExecute()
        {
            IsAddedNewManager = false;
            var ownerView = new HotelOwnerView();
            ownerView.Show();
            addNewManagerView.Close();
        }
        #endregion
    }
}
