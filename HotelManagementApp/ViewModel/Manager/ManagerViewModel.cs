using HotelManagementApp.Model;
using HotelManagementApp.View.Manager;

namespace HotelManagementApp.ViewModel.Manager
{
    class ManagerViewModel : ViewModelBase
    {
        #region Fields
        private readonly ManagerView managerView;
        private readonly DataAccess db = new DataAccess();
        #endregion
        #region Constructors
        public ManagerViewModel(ManagerView managerView, tblManager manager)
        {
            this.managerView = managerView;
        }
        #endregion
    }
}
