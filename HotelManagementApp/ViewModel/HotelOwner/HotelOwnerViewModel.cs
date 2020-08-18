using HotelManagementApp.Command;
using HotelManagementApp.Model;
using HotelManagementApp.View.HotelOwner;
using System;
using System.Windows;
using System.Windows.Input;

namespace HotelManagementApp.ViewModel.HotelOwner
{
    class HotelOwnerViewModel : LogoutViewModel
    {
        #region Fields
        private readonly HotelOwnerView hotelOwnerView;
        private readonly DataAccess db = new DataAccess();
        #endregion
        #region Constructors
        public HotelOwnerViewModel(HotelOwnerView hotelOwnerView)
        {
            this.hotelOwnerView = hotelOwnerView;
        }
        #endregion
        #region Methods
        protected override void ExitExecute()
        {
            MainWindow loginWindow = new MainWindow();
            hotelOwnerView.Close();
            loginWindow.Show();
        }
        #endregion
        #region Commands

        //adding new emloyee

        private ICommand addNewEmployee;
        public ICommand AddNewEmployee
        {
            get
            {
                if (addNewEmployee == null)
                {
                    addNewEmployee = new RelayCommand(param => AddNewEmployeeExecute(), param => CanAddNewEmployee());
                }
                return addNewEmployee;
            }
        }

        private void AddNewEmployeeExecute()
        {
            try
            {
                AddNewEmployeeView addNewEmployeeView = new AddNewEmployeeView();
                addNewEmployeeView.ShowDialog();
                hotelOwnerView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanAddNewEmployee()
        {
            return true;
        }

        //adding new manager

        private ICommand addNewManager;
        public ICommand AddNewManager
        {
            get
            {
                if (addNewManager == null)
                {
                    addNewManager = new RelayCommand(param => AddNewManagerExecute(), param => CanAddNewManager());
                }
                return addNewManager;
            }
        }

        private void AddNewManagerExecute()
        {
            try
            {
                AddNewManagerView addNewManagerView = new AddNewManagerView();
                addNewManagerView.ShowDialog();
                hotelOwnerView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanAddNewManager()
        {
            return true;
        }
        #endregion
    }
}
