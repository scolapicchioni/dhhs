using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples {
    public class LinqQueries2 {
        public static void Ex01FromSelect01() {
            List<Car> cars = Car.GetCars();

            IEnumerable<Car> q = from c in cars //c is a Range Variable, cars is a Collection (IEnumerable)
                    select c;
            
            foreach (var car in q) {
                Console.WriteLine(car.Brand + " " + car.Model);
            }
        }

        public static void Ex02FromSelect02() {
            List<Car> cars = Car.GetCars();

            IEnumerable<string> q = from c in cars 
                                    select c.Model;

            foreach (var model in q) {
                Console.WriteLine(model);
            }
        }

        public static void Ex03FromSelect03() {
            List<Car> cars = Car.GetCars();

            var q = from c in cars 
                    select new { c.Brand, c.Model };

            foreach (var item in q) {
                Console.WriteLine(item.Brand + " " + item.Model);
            }
        }

        public static void Ex04FromGroupBy01() {
            List<Car> cars = Car.GetCars();

            var q = from c in cars 
                    group c by c.Brand; //hierarchical

            foreach (var grouping in q) {
                Console.WriteLine(grouping.Key);
                foreach (var item in grouping) {
                    Console.WriteLine("\t" + item.Model);
                }
            }
        }

        public static void Ex04FromGroupBy02() {
            List<Car> cars = Car.GetCars();

            var q = from c in cars
                    group new { c.Model, c.Price } by c.Brand; //hierarchical

            foreach (var grouping in q) {
                Console.WriteLine(grouping.Key);
                foreach (var item in grouping) {
                    Console.WriteLine("\t" + item.Model);
                }
            }
        }

        public static void Ex04FromGroupBy03() {
            List<Car> cars = Car.GetCars();

            var q = from c in cars
                    group new { c.Model, c.Price } by new {c.Brand, Initial =  c.Model.Substring(0,1)}; //hierarchical

            foreach (var grouping in q) {
                Console.WriteLine(grouping.Key.Brand + " " + grouping.Key.Initial);
                foreach (var item in grouping) {
                    Console.WriteLine("\t" + item.Model);
                }
            }
        }

        public static void Ex05Where01() {
            List<Car> cars = Car.GetCars();

            var q = from c in cars
                    where c.Brand == "FIAT"
                    select c;

            foreach (var item in q) {
                Console.WriteLine(item.Brand + " " + item.Model);
            }
        }

        public static void Ex06OrderBy01() {
            List<Car> cars = Car.GetCars();

            var q = from c in cars
                    orderby c.Price ascending, c.Model descending
                    select c;

            foreach (var item in q) {
                Console.WriteLine(item.Brand + " " + item.Model + " " + item.Price);
            }
        }

        public static void Ex07WhereOrderBy01() {
            List<Car> cars = Car.GetCars();

            var q = from c in cars
                    where c.Brand == "FIAT"
                    orderby c.Price ascending, c.Model descending
                    select c;

            foreach (var item in q) {
                Console.WriteLine(item.Brand + " " + item.Model + " " + item.Price);
            }

        }

        public static void Ex08Join01() {
            List<Car> cars = Car.GetCars();
            List<Driver> drivers = Driver.GetDrivers();

            var q = from c in cars
                    join d in drivers
                    on c.Id equals d.CarId //equi join
                    select new { c.Brand, c.Model, d.Name, d.Surname }; //flat

            foreach (var item in q) {
                Console.WriteLine(item.Brand + " " + item.Model + " " + item.Name + " " + item.Surname);
            }
        }

        public static void Ex09SelectMany01() {
            List<Car> cars = Car.GetCars();
            List<Driver> drivers = Driver.GetDrivers();

            var q = from c in cars
                    from d in drivers //cross join
                    select new { c.Brand, c.Model, d.Name, d.Surname }; //flat

            foreach (var item in q) {
                Console.WriteLine(item.Brand + " " + item.Model + " " + item.Name + " " + item.Surname);
            }
        }

        public static void Ex10SelectMany02() {
            List<Car> cars = Car.GetCars();
            List<Driver> drivers = Driver.GetDrivers();

            var q = from c in cars
                    from d in drivers
                    where c.Id == d.CarId //equi join, inner
                    select new { c.Brand, c.Model, d.Name, d.Surname }; //flat

            foreach (var item in q) {
                Console.WriteLine(item.Brand + " " + item.Model + " " + item.Name + " " + item.Surname);
            }
        }

        public static void Ex11SelectMany03() {
            List<Car> cars = Car.GetCars();
            List<Driver> drivers = Driver.GetDrivers();

            var q1 = from c in cars
                    from d in drivers
                    where c.Id != d.CarId //non equi join
                    select new { c.Brand, c.Model, d.Name, d.Surname }; //flat

            foreach (var item in q1) {
                Console.WriteLine(item.Brand + " " + item.Model + " is NOT driven by " + item.Name + " " + item.Surname);
            }

        }

        public static void Ex12GroupJoin01() {
            List<Car> cars = Car.GetCars();
            List<Driver> drivers = Driver.GetDrivers();

            var q = from c in cars
                    join d in drivers
                    on c.Id equals d.CarId
                    into cardrivers //left join , group join
                    select new { Car = c, Drivers = cardrivers }; //hierarchical

            foreach (var item in q) {
                Console.WriteLine(item.Car.Brand + " " + item.Car.Model);
                foreach (var driver in item.Drivers) {
                    Console.WriteLine("\t" + driver.Name + " " + driver.Surname);
                }
            }
        }

        public static void Ex13GroupJoin02() {
            List<Car> cars = Car.GetCars();
            List<Driver> drivers = Driver.GetDrivers();

            var q = from c in cars
                    join d in drivers
                    on c.Id equals d.CarId
                    into cardrivers //inner join , group join
                    where cardrivers.Any()
                    select new { Car = c, Drivers = cardrivers }; //hierarchical

            foreach (var item in q) {
                Console.WriteLine(item.Car.Brand + " " + item.Car.Model);
                foreach (var driver in item.Drivers) {
                    Console.WriteLine("\t" + driver.Name + " " + driver.Surname);
                }
            }
        }

        public static void Ex14FlatJoin01() {
            List<Car> cars = Car.GetCars();
            List<Driver> drivers = Driver.GetDrivers();

            var q = from c in cars
                    join d in drivers
                    on c.Id equals d.CarId
                    into cardrivers //till here it would be hierarchical
                    from cd in cardrivers.DefaultIfEmpty() //but now it's flat again, with a left join
                    select new { Car = c, Driver = cd };

            foreach (var item in q) {
                Console.WriteLine(item.Car.Brand + " " + item.Car.Model + (item.Driver == null ? " no driver" : " " + item.Driver.Name + " " + item.Driver.Surname));
            }

        }

        public static void Ex15FromHierarchicalToFlatSelectMany01(){
            List<Musician> musicians = Musician.GetMusicians();

            var q1 = from m in musicians
                     select m.Instruments;
            foreach (var item in q1)
            {
                Console.WriteLine(item); //it's a collection
                foreach (var i in item)
                {
                    Console.WriteLine(i);
                }
            }

            //equivalent to
            var q2 = Musician.GetMusicians().Select(m => m.Instruments);

            foreach (var item in q2)
            {
                Console.WriteLine(item); //it's a collection
                foreach (var i in item)
                {
                    Console.WriteLine(i);
                }
            }

            var q3 = from m in Musician.GetMusicians()
                    from i in m.Instruments
                    select i;
            foreach (var item in q3)
            {
                Console.WriteLine(item); //not a collection anymore
            }

            //equivalent to
            var q4 = Musician.GetMusicians().SelectMany(m => m.Instruments);

            foreach (var item in q4)
            {
                Console.WriteLine(item); //not a collection anymore
            }
        }

        public static void Ex15FromHierarchicalToFlatSelectMany02() {
            List<Musician> musicians = Musician.GetMusicians();

            var q = from m in musicians
                    from i in m.Instruments //flat selectmany
                    select new { Musician = m.Name, Instrument = i };

            foreach (var item in q) {
                Console.WriteLine(item.Musician + " plays " + item.Instrument);
            }
        }

        public static void Ex16Let01() {
            List<Musician> musicians = Musician.GetMusicians();

            //var q = from m in musicians
            //        where m.Instruments.Count() > 2
            //        select new { m.Name, HowManyInstruments = m.Instruments.Count() };

            var q = from m in musicians
                    let HowManyInstruments = m.Instruments.Count()
                    where HowManyInstruments > 2
                    select new { m.Name, HowManyInstruments };

            foreach (var item in q) {
                Console.WriteLine(item.Name + " plays " + item.HowManyInstruments + " instruments");
            }
        }

        public static void Ex17Let02() {
            List<Musician> musicians = Musician.GetMusicians();

            //var q = from m in musicians
            //        where (from i in m.Instruments
            //               where i == "Bass" || i == "Drums"
            //               select i).Any()
            //        select new {
            //            Musician = m.Name,
            //            Instruments = from i in m.Instruments
            //                          where i == "Bass" || i == "Drums"
            //                          select i
            //        };

            var q = from m in musicians
                    let bassOrDrums = from i in m.Instruments
                           where i == "Bass" || i == "Drums"
                           select i
                    where bassOrDrums.Any()
                    select new {
                        Musician = m.Name,
                        Instruments = bassOrDrums
                    };

            foreach (var item in q) {
                Console.WriteLine(item.Musician);
                foreach (var instr in item.Instruments) {
                    Console.WriteLine("\t" + instr);
                }
            }
        }

        public static void Ex05FromGroupJoin() {
            List<Car> cars = Car.GetCars();

            var q = from c in cars
                    group c by c.Brand
                        into carbrands
                        select new { 
                            Brand = carbrands.Key, 
                            Cars = from x in carbrands 
                                   select new { x.Model, x.Price } 
                        };

            foreach (var item in q) {
                Console.WriteLine(item.Brand);
                foreach (var car in item.Cars) {
                    Console.WriteLine("\t" + car.Model);
                }
            }
        }

        public static void CallOrder() {
            var cars = Car.GetCars();

            var q = cars.Where(c=>{Console.WriteLine("calling where " + c);return c.Brand=="FIAT";})
                .OrderBy(c=>{Console.WriteLine("calling orderby " + c );return c.Price;})
                .Select(c => { Console.WriteLine("calling select" + c); return c.Id; })
                .ToList();

            //foreach (var item in q) {
            //    Console.WriteLine("FOREACH " + item);
            //}
        }
    }
}
