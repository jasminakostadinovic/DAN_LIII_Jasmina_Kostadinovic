using HotelManagementApp.Model;
using HotelManagementApp.ViewModel.Manager;
using System.Windows;

namespace HotelManagementApp.View.Manager
{
    /// <summary>
    /// Interaction logic for GetNumberFromManagerView.xaml
    /// </summary>
    public partial class GetNumberFromManagerView : Window
    {
        public GetNumberFromManagerView(int employeeId, int managerId)
        {
            InitializeComponent();
            this.DataContext = new GetNumberFromManagerViewModel(this, employeeId, managerId);
        }
    }
}
