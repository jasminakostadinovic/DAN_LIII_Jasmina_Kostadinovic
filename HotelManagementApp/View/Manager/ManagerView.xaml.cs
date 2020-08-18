using HotelManagementApp.Model;
using HotelManagementApp.ViewModel.Manager;
using System.Windows;
using System.Windows.Controls;

namespace HotelManagementApp.View.Manager
{
    /// <summary>
    /// Interaction logic for ManagerView.xaml
    /// </summary>
    public partial class ManagerView : Window
    {
        public ManagerView(int managerId)
        {
            InitializeComponent();
            this.DataContext = new ManagerViewModel(this, managerId);
        }
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //hiding id columns
            if (e.Column.Header.ToString() == "EmployeeID"
                 || e.Column.Header.ToString() == "UserDataID"
                 || e.Column.Header.ToString() == "Date_Of_Birth")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }
}
