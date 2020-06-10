using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace StudentAPI.Models
{
    public class Student
    {

        //Define all the properties that are in the database table Students. 
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public string BirthDate { get; set; }
        public string Score { get; set; }
        public int DepartmentID { get; set; }

    }

}