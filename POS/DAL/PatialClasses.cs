using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public partial class Instructor
    {       
        public string FullName
        { 
            get
            {
                return Name + " " + Surname; 
            }            
        }
    }

    public partial class User
    {
        public string FullName
        {
            get
            {
                return Name + " " + Surname;
            }
        }
    }

    public partial class Customer
    {
        public string FullName
        {
            get
            {
                return FirstName + " " + Surname;
            }
        }

    }

    public partial class DriverSchedule
    {
        public string DateStr { get; set; }

    }
}
