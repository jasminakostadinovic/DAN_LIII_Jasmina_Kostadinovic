using HotelManagementApp.Command;
using HotelManagementApp.Model;
using HotelManagementApp.View;
using HotelManagementApp.View.Employee;
using HotelManagementApp.View.HotelOwner;
using HotelManagementApp.View.Manager;
using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;

namespace HotelManagementApp.ViewModel
{
	class MainWindowViewModel : ViewModelBase
    {
		#region Fields
		private string userName;
		readonly MainWindow loginView;
		private static readonly string ownerAccessPath = @"..\OwnerAccess.txt";
		private string ownerUserName;
		private string ownerPassword;
		#endregion

		#region Constructor
		internal MainWindowViewModel(MainWindow view)
		{
			this.loginView = view;
		}
		static MainWindowViewModel()
		{
			if (!File.Exists(ownerAccessPath))
			{
				File.WriteAllLines(ownerAccessPath, new string[]
				{
					"x",
					"x"
				});
			}
		}
		#endregion
		#region Meethods

		private string ReadOwnerUsername()
		{
			try
			{
				return File.ReadAllLines(ownerAccessPath)[0];
			}
			catch (Exception)
			{
				return "x";
			}
		}
		private string ReadOwnerPasword()
		{
			try
			{
				return File.ReadAllLines(ownerAccessPath)[1];
			}
			catch (Exception)
			{
				return "x";
			}
		}
		#endregion
		#region Properties
		public string UserName
		{
			get
			{
				return userName;
			}
			set
			{
				userName = value;
				OnPropertyChanged(nameof(UserName));
			}
		}
		#endregion

		private ICommand submitCommand;
		public ICommand SubmitCommand
		{
			get
			{
				if (submitCommand == null)
				{
					submitCommand = new RelayCommand(Submit);
					return submitCommand;
				}
				return submitCommand;
			}
		}

		void Submit(object obj)
		{
			string password = (obj as PasswordBox).Password;
			ownerUserName = ReadOwnerUsername();
			ownerPassword = ReadOwnerPasword();
			DataAccess dataAccess = new DataAccess();
			if (UserName == ownerUserName && password == ownerPassword)
			{
				HotelOwnerView hotelOwnerView = new HotelOwnerView();
				loginView.Close();
				hotelOwnerView.Show();
				return;
			}		
			else if (dataAccess.IsCorrectUser(userName, password))
			{
				int userDataId = dataAccess.GetUserDataId(userName);
				if (userDataId != 0)
				{
					if(dataAccess.GetTypeOfUser(userDataId) == nameof(tblManager))
					{
						var manager = dataAccess.LoadManagerByUsername(UserName);
						int managerId = manager.ManagerID;
						ManagerView managerView = new ManagerView(managerId);
						loginView.Close();
						managerView.Show();
						return;
					}
					if(dataAccess.GetTypeOfUser(userDataId) == nameof(tblEmployee))
					{
						var employee = dataAccess.LoadEmployeeByUsername(UserName);
						EmployeeView employeeView = new EmployeeView(employee);
						loginView.Close();
						employeeView.Show();
						return;
					}
					return;
				}
			}
			else if (!dataAccess.IsCorrectUser(userName, password))
			{
				WarningView warning = new WarningView(loginView);
				warning.Show("User name or password are not correct!");
				UserName = null;
				(obj as PasswordBox).Password = null;
				return;
			}

		}
	}
}
