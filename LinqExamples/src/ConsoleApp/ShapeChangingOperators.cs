using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples {
    public class ShapeChangingOperators {
        public static void FromFlatToHierarcalSelectSubquery(){
            string[] names = GetArrayOfNames();
            var q = from n in names
                    orderby n
                    select new { 
                        Initial = n.Substring(0,1), 
                        Names = from m in names
                                where m.Substring(0, 1) == n.Substring(0,1)
                                select m 
                    };
            foreach (var item in q) {
                Console.WriteLine(item.Initial);
                foreach (var name in item.Names) {
                    Console.WriteLine("\t" + name);
                }
            }
        }

        

        public static void FromFlatToHierarcalSelectSubquery2() {
            string[] names = GetArrayOfNames();
            var q1 = (from n in names
                      orderby n
                      select n.Substring(0, 1)).Distinct();

            var q = from i in q1
                    select new {
                        Initial = i,
                        Names = from m in names
                                where m.Substring(0, 1) == i.ToString()
                                select m
                    };
            foreach (var item in q) {
                Console.WriteLine(item.Initial);
                foreach (var name in item.Names) {
                    Console.WriteLine("\t" + name);
                }
            }
        }

        public static void FromFlatToHierarcalGroupBy() {
            string[] names = GetArrayOfNames();

            var q = from n in names
                    orderby names
                    group n by n.Substring(0, 1);
                    

            foreach (var grouping in q) {
                Console.WriteLine(grouping.Key);
                foreach (var name in grouping) {
                    Console.WriteLine("\t" + name);
                }
            }
        }

        public static void FromRelationalToFlatSelectMany() {
            List<Car> cars = Car.GetCars();

            List<Driver> drivers = Driver.GetDrivers();

            var q = from c in cars
                    from d in drivers
                    where c.Id == d.CarId
                    select new { c.Brand, c.Model, d.Name, d.Surname};

            foreach (var item in q) {
                Console.WriteLine(item.Brand + " " + item.Model + " " + item.Name + " " + item.Surname);
            }
        }

        public static void FromRelationalToFlatJoin() {
            List<Car> cars = Car.GetCars();

            List<Driver> drivers = Driver.GetDrivers();

            var q = from c in cars
                    join d in drivers
                    on c.Id equals d.Id
                    select new { c.Brand, c.Model, d.Name, d.Surname };

            foreach (var item in q) {
                Console.WriteLine(item.Brand + " " + item.Model + " " + item.Name + " " + item.Surname);
            }
        }


        public static void FromRelationalToHierarcalSelectSubquery() {
            List<Car> cars = Car.GetCars();

            List<Driver> drivers = Driver.GetDrivers();

            var q = from c in cars
                    orderby c.Brand, c.Model
                    select new {
                        Car = c, 
                        Drivers = from d in drivers 
                                  orderby d.Surname, d.Name
                                  where d.CarId == c.Id 
                                  select d
                    };

            foreach (var item in q) {
                Console.WriteLine(item.Car.Brand + " " + item.Car.Model);
                foreach (var d in item.Drivers) {
                    Console.WriteLine("\t" + d.Name + " " + d.Surname);
                }
            }
        }

        public static void FromRelationalToHierarcalGroupJoin() {
            List<Car> cars = Car.GetCars();

            List<Driver> drivers = Driver.GetDrivers();

            var q1 = from c in cars
                     join d in drivers
                     on c.Id equals d.CarId
                     into cardrivers
                     select new { Car = c, Drivers = cardrivers };

            foreach (var item in q1) {
                Console.WriteLine(item.Car.Brand + " " + item.Car.Model);
                foreach (var d in item.Drivers) {
                    Console.WriteLine("\t" + d.Name + " " + d.Surname);
                }
            }
        }

        public static void FromHierarchicalToFlatSelectMany() {
            List<Musician> beatles = Musician.GetMusicians();

            var q = from m in beatles
                    from i in m.Instruments
                    select new { m.Name, Instrument = i };
            foreach (var item in q) {
                Console.WriteLine(item.Name + " plays " + item.Instrument);
            }
        }



        private static string[] GetArrayOfNames() {
            string[] names = { "Alice", "Candice", "Jack", "Janis", "Bob", "Clara", "David", "Frank", "Daniel", "Armando", "Andrea", "John", "Simona", "Marco", "Mario", "Samantha" };
            return names;
        }

    }
}
