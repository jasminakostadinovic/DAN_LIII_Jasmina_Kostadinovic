using HotelManagementApp.Model;
using System;
using System.ComponentModel;

namespace HotelManagementApp.Interfaces
{
    interface IValidatedUserData : IDataErrorInfo
    {
        bool CanSave { get; set; }
        string Surname { get; set; }
        string GivenName { get; set; }
        string Password { get; set; }
        string Username { get; set; }
        string Email { get; set; }
        string DateOfBirth { get; set; }
        DateTime DateOfBirthValue { get; set; }
        tblUserData UserData { get; set; }
    }
}
