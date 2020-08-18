using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementApp.Model
{
    public partial class vwEmployee
    {
        public string DateOfBirth
        {
            get
            {
                return Date_Of_Birth.ToShortDateString();
            }
        }
    }
}
