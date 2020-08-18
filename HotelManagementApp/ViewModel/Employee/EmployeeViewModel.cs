using HotelManagementApp.Model;
using HotelManagementApp.View.Employee;

namespace HotelManagementApp.ViewModel.Employee
{
    class EmployeeViewModel : ViewModelBase
    {
        #region Fields
        private readonly EmployeeView employeeView;
        private readonly DataAccess db = new DataAccess();
        #endregion
        #region Constructors
        public EmployeeViewModel(EmployeeView employeeView, tblEmployee employee)
        {
            this.employeeView = employeeView;
        }
        #endregion
    }
}
