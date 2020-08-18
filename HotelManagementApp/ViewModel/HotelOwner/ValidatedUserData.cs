using HotelManagementApp.DataValidations;
using HotelManagementApp.Interfaces;
using HotelManagementApp.Model;
using System;
using System.Globalization;

namespace HotelManagementApp.ViewModel.HotelOwner
{
    class ValidatedUserData : ViewModelBase, IValidatedUserData
    {
        #region Fields
        protected tblUserData userData;
        protected string surname;
        protected string givenName;
        protected string dateOfBirth;
        protected string email;
        protected string username;
        protected string password;
        protected DateTime dateOfBirthValue;
        #endregion
        #region Properties
        public bool CanSave { get; set; }
        public tblUserData UserData
        {
            get
            {
                return userData;
            }
            set
            {
                if (userData == value) return;
                userData = value;
            }
        }
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                if (surname == value) return;
                surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }
        public string GivenName
        {
            get
            {
                return givenName;
            }
            set
            {
                if (givenName == value) return;
                givenName = value;
                OnPropertyChanged(nameof(GivenName));
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (password == value) return;
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                if (username == value) return;
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (email == value) return;
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string DateOfBirth
        {
            get
            {
                return dateOfBirth;
            }
            set
            {
                if (dateOfBirth == value) return;
                dateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));
            }
        }
        public DateTime DateOfBirthValue
        {
            get
            {
                return dateOfBirthValue;
            }
            set
            {
                if (dateOfBirthValue == value) return;
                dateOfBirthValue = value;
            }
        }
        #endregion
        #region Constructors
        public ValidatedUserData()
        {
            GivenName = string.Empty;
            Surname = string.Empty;
            DateOfBirth = string.Empty;
            Email = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            userData = new tblUserData();
            CanSave = true;
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
                var db = new DataAccess();
                string validationMessage = string.Empty;
                var validate = new DataValidation();
                var culture = CultureInfo.InvariantCulture;
                var styles = DateTimeStyles.None;

                if (name == nameof(Username))
                {
                    if (!db.IsUniqueUsername(Username))
                    {
                        validationMessage = "Username number must be unique!";
                        CanSave = false;
                    }
                }
                else if (name == nameof(Email))
                {
                    if (!validate.IsValidEmail(Email))
                    {
                        validationMessage = "Invalid email format!";
                        CanSave = false;
                    }

                }
                if (name == nameof(DateOfBirth))
                {
                    if (!DateTime.TryParse(DateOfBirth, culture, styles, out dateOfBirthValue))
                    {
                        validationMessage = "Invalid date format! use: [4/10/2008]";
                        CanSave = false;
                    }
                    //to avoid SqlDateTime overflow exception
                    if (DateOfBirthValue == default(DateTime) || DateOfBirthValue.Year < 1900)
                    {
                        validationMessage = "Invalid date of birth!";
                        CanSave = false;
                    }
                }
                if (string.IsNullOrEmpty(validationMessage))
                    CanSave = true;

                return validationMessage;
            }
        }
        #endregion

    }
}
