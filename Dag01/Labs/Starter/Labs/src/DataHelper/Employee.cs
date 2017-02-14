using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataHelper
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? Manager { get; set; }
        public DateTime BirthDate { get; set; }
        public char Gender { get; set; }
        public int SickLeaveHours { get; set; }
    }
}
