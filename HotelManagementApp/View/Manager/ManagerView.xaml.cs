using HotelManagementApp.Model;
using HotelManagementApp.ViewModel.Manager;
using System.Windows;

namespace HotelManagementApp.View.Manager
{
    /// <summary>
    /// Interaction logic for ManagerView.xaml
    /// </summary>
    public partial class ManagerView : Window
    {
        public ManagerView(tblManager manager)
        {
            InitializeComponent();
            this.DataContext = new ManagerViewModel(this, manager);
        }
    }
}
