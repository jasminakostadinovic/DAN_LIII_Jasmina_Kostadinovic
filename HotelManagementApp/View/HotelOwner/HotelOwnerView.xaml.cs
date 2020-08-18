using HotelManagementApp.ViewModel.HotelOwner;
using System.Windows;

namespace HotelManagementApp.View.HotelOwner
{
    /// <summary>
    /// Interaction logic for HotelOwnerView.xaml
    /// </summary>
    public partial class HotelOwnerView : Window
    {
        public HotelOwnerView()
        {
            InitializeComponent();
            this.DataContext = new HotelOwnerViewModel(this);
        }
    }
}
