﻿using HotelManagementApp.ViewModel.HotelOwner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelManagementApp.View.HotelOwner
{
    /// <summary>
    /// Interaction logic for AddNewEmployeeView.xaml
    /// </summary>
    public partial class AddNewEmployeeView : Window
    {
        public AddNewEmployeeView()
        {
            InitializeComponent();
            this.DataContext = new AddNewEmployeeViewModel(this, new ValidatedUserData());
        }
    }
}
