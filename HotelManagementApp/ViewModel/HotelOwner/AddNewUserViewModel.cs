using HotelManagementApp.Command;
using System.Windows.Input;

namespace HotelManagementApp.ViewModel.HotelOwner
{
    class AddNewUserViewModel : ViewModelBase
    {
        
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

        protected virtual bool CanSaveExecute()
        {
            return true;
        }
        protected virtual void SaveExecute()
        {
           
        }

        //Escaping action
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
        protected virtual bool CanExitExecute()
        {
            return true;
        }

        protected virtual void ExitExecute()
        {          
        }
        #endregion
    }
}
