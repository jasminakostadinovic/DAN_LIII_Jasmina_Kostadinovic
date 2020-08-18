using HotelManagementApp.Model;
using HotelManagementApp.ViewModel.Employee;
using System.Windows;

namespace HotelManagementApp.View.Employee
{
    /// <summary>
    /// Interaction logic for EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : Window
    {
        public EmployeeView(tblEmployee employee)
        {
            InitializeComponent();
            this.DataContext = new EmployeeViewModel(this, employee);
        }
    }
}
