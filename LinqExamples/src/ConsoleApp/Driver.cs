using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples {
    public class Driver {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int CarId { get; set; }
        public override string ToString()
        {
            return $"{Id} - {Name} {Surname}";
        }
        public static List<Driver> GetDrivers() {
            List<Driver> drivers = new List<Driver> { 
                new Driver {Id = 1, Name = "Alice", Surname = "Anderson", CarId = 5},
                new Driver {Id = 2, Name = "Bob", Surname = "Builders", CarId = 2},
                new Driver {Id = 3, Name = "Candice", Surname = "Clarkson", CarId = 9},
                new Driver {Id = 4, Name = "Marco", Surname = "Danielson", CarId = 4},
                new Driver {Id = 5, Name = "Giulia", Surname = "Conte", CarId = 4},
                new Driver {Id = 6, Name = "Frank", Surname = "Funnel", CarId = 2},
                new Driver {Id = 7, Name = "Donald", Surname = "Trump", CarId = 2},
                new Driver {Id = 8, Name = "Stan", Surname = "Lee", CarId = 5},
                new Driver {Id = 9, Name = "Kyle", Surname = "Korelson", CarId = 4}
            };
            return drivers;
        }
    }
}
