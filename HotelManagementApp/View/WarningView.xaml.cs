﻿using HotelManagementApp.ViewModel;
using System.Windows;

namespace HotelManagementApp.View
{
    /// <summary>
    /// Interaction logic for WarningView.xaml
    /// </summary>
    public partial class WarningView : Window
    {
        public WarningView(Window backView)
        {
            InitializeComponent();
            DataContext = new WarningViewModel(this, backView);
        }
        public void Show(string message)
        {
            lblText.Content = message;
            this.Show();
        }
    }
}
