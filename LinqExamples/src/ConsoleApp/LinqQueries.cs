using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples {
    public class LinqQueries {
        public static void First() {
            string[] names = GetArrayOfNames();

            IEnumerable<string> orderednames = Enumerable.OrderBy(names, n => n);
            //names[4] = "Zack";
            foreach (var item in orderednames) {
                Console.WriteLine(item);
            }
        }

        public static void Second(){
            string[] names = GetArrayOfNames();
            IEnumerable<string> orderednames = names.OrderBy(n => n);
            foreach (var item in orderednames) {
                Console.WriteLine(item);
            }
        }

        public static void Third(){
            string[] names = GetArrayOfNames();

            var orderednames = from n in names
                               orderby n
                               select n;

            foreach (var item in orderednames) {
                Console.WriteLine(item);
            }
        }

        public static void Fourth() {
            string[] names = GetArrayOfNames();
            var orderednames = names
                .Where(n => n.StartsWith("A"))
                .OrderBy(n => n)
                .Select(n => n.ToUpper());
            foreach (var item in orderednames) {
                Console.WriteLine(item);
            }
        }

        public static void Fifth() {
            string[] names = GetArrayOfNames();
            var orderednames = from n in names
                               select n.ToUpper();

            foreach (var item in orderednames) {
                Console.WriteLine(item);
            }
        }

        public static void Sixth() {
            string[] names = GetArrayOfNames();
            var orderednames = from n in names
                               group n by n.Substring(0,1);

            foreach (var grouping in orderednames) {
                Console.WriteLine(grouping.Key);
                foreach (var item in grouping) {
                    Console.WriteLine("\t" + item);
                }
            }
        }

        public static void Seventh() {
            string[] names = GetArrayOfNames();
            var query = from n in names
                               where n.Length > 4
                               select n;
            foreach (var item in query) {
                Console.WriteLine(item);
            }
        }

       

        public static void Eighth() {
            string[] names = GetArrayOfNames();
            //I want to select the names with more than 3 consonants

            var q1 = from n in names
                     where (n.Replace("a", "").Replace("e", "").Replace("i", "").Replace("o", "").Replace("u", "")).Length > 3
                     select n.Replace("a", "").Replace("e", "").Replace("i", "").Replace("o", "").Replace("u", "");

            var q2 = from n in names
                     let novowel = n.Replace("a", "").Replace("e", "").Replace("i", "").Replace("o", "").Replace("u", "")
                     where novowel.Length > 3
                     select novowel;

            var q3 = from n in names
                     let novowel = n.Replace("a", "").Replace("e", "").Replace("i", "").Replace("o", "").Replace("u", "")
                     where novowel.Length > 3
                     select novowel + " comes from " + n;

            foreach (var item in q3) {
                Console.WriteLine(item);
            }

        }

        public static void Nineth() {
            List<Car> cars = Car.GetCars();

            List<Driver> drivers = Driver.GetDrivers();

            var q1 = from c in cars
                     join d in drivers
                     on c.Id equals d.CarId
                     select new {d.Name, c.Brand, c.Model };

            foreach (var item in q1) {
                Console.WriteLine(item.Name + " drives " + item.Brand + " " +  item.Model );
            }
        }

        public static void Tenth() {
            List<Car> cars = Car.GetCars();

            List<Driver> drivers = Driver.GetDrivers();

            var q1 = from c in cars
                     join d in drivers
                     on c.Id equals d.CarId
                     into cardrivers
                     orderby c.Brand, c.Model
                     where cardrivers.Count()>0
                     select new { Car = c, Drivers = cardrivers};

            foreach (var item in q1) {
                Console.WriteLine(item.Car.Brand + " " + item.Car.Model);
                foreach (var d in item.Drivers) {
                    Console.WriteLine("\t" + d.Name + " " + d.Surname);
                }
            }
        }


        public static void Eleventh() {
            List<Car> cars = Car.GetCars();

            List<Driver> drivers = Driver.GetDrivers();

            var q1 = from d in drivers
                     join c in cars
                     on d.CarId equals c.Id
                     into cardrivers
                     select new { Driver = d, Cars = cardrivers };

            foreach (var item in q1) {
                Console.WriteLine(item.Driver.Name + " " + item.Driver.Surname);
                foreach (var c in item.Cars) {
                    Console.WriteLine("\t" + c.Model + " " + c.Price);
                }
            }
        }

        

        public static void Tenth_Weird() {
            List<Car> cars = Car.GetCars();

            List<Driver> drivers = Driver.GetDrivers();

            var q1 = from c in cars
                     let Drivers = from dd in drivers where dd.CarId == c.Id select dd
                     orderby c.Brand, c.Model
                     where Drivers.Count()>0
                     select new {Car =  c, Drivers  };

            foreach (var item in q1) {
                Console.WriteLine(item.Car.Brand + " " + item.Car.Model);
                foreach (var d in item.Drivers) {
                    Console.WriteLine("\t" + d.Name + " " + d.Surname);
                }
            }
        }

        



        public static void HowManyPeopleDriveByBrand() {
            List<Car> cars = Car.GetCars();

            List<Driver> drivers = Driver.GetDrivers();

            var q1 = (from c in cars
                     join d in drivers
                     on c.Id equals d.CarId
                     group c by c.Brand
                         into bzz
                         from c in bzz
                         select new { c.Brand, Count = bzz.Count() }).Distinct();

            foreach (var item in q1) {
                Console.WriteLine(item);
            }
        }

        public static void FromFrom1() {
            List<Musician> beatles = Musician.GetMusicians();

            var musiciansandinstrumentsflattened = from m in beatles
                                                   from i in m.Instruments
                                                   select new { m.Name, Instrument = i };
            foreach (var musician in musiciansandinstrumentsflattened) {
                 Console.WriteLine(musician.Name + " plays " + musician.Instrument); 
            }
        }

        

        public static void FromFrom2() {
            List<Musician> beatles = Musician.GetMusicians();

            var musiciansandinstrumentsflattened =
                from m in beatles
                from instrument in m.Instruments
                where instrument == "Guitar" || instrument == "Vocals"
                select new { m.Name, Instrument = instrument }; 
            
            foreach (var musician in musiciansandinstrumentsflattened) {
                Console.WriteLine(musician.Name + " plays " + musician.Instrument);
            }
        }

        public static void Let01() {
            List<Musician> beatles = Musician.GetMusicians();
            var collection =
                from musician in beatles
                let numberOfInstruments = musician.Instruments.Count
                where numberOfInstruments > 2
                select new {
                    musician.Name,
                    NumberOfInstruments = numberOfInstruments
                };

            foreach (var m in collection) {
                Console.WriteLine(m.Name + " plays " + m.NumberOfInstruments + " instruments");
            }
        }

        public static void Let02() {
            List<Musician> beatles = Musician.GetMusicians();

            var collection =
                from musician in beatles
                let stringInstruments =
                    from instrument in musician.Instruments
                    where instrument == "Guitar" || instrument == "Bass" || instrument == "Piano"
                    select instrument
                select new { musician.Name, stringInstruments };

            foreach (var m in collection) {
                Console.WriteLine(m.Name + " plays ");
                foreach (var instrument in m.stringInstruments) {
                    Console.WriteLine("\t" + instrument);
                }

            }
        }

        public static void GroupBy1() {
            List<Car> cars = Car.GetCars();

            var q1 = from c in cars
                     orderby c.Price
                     group c by c.Brand;

            foreach (var grouping in q1) {
                Console.WriteLine(grouping.Key);
                foreach (var item in grouping) {
                    Console.WriteLine("\t" + item);
                }
            }
        }


        public static void Join1() {
            List<Car> cars = Car.GetCars();

            List<Driver> drivers = Driver.GetDrivers();

            var q1 = from d in drivers
                     join c in cars
                     on d.CarId equals c.Id
                     select new { d.Name, d.Surname, c.Brand, c.Model };

            foreach (var item in q1) {
                Console.WriteLine(item.Name + " " + item.Surname + " drives a " + item.Brand + " " + item.Model);
            }
        }

        public static void Join2() {
            List<Car> cars = Car.GetCars();

            List<Driver> drivers = Driver.GetDrivers();

            var q1 = from c in cars
                     join d in drivers
                     on c.Id equals d.CarId
                     into cardrivers
                     select new { c.Brand, c.Model, Drivers = cardrivers };

            foreach (var item in q1) {
                Console.WriteLine(item.Brand + " " + item.Model );
                foreach (var d in item.Drivers) {
                    Console.WriteLine("\t" + d.Name + d.Surname);
                }
            }
        }

        public static void Join3() {
            List<Car> cars = Car.GetCars();

            List<Driver> drivers = Driver.GetDrivers();

            var q1 = from c in cars
                     join d in drivers
                     on c.Id equals d.CarId
                     into cardrivers
                     from cd in cardrivers.DefaultIfEmpty()
                     select new { Driver = cd == null ? "" : cd.Name + " " + cd.Surname, c.Brand, c.Model };

            foreach (var item in q1) {
                Console.WriteLine(item.Brand + " " + item.Model + " " + item.Driver);
            }
        }
        public static void DeferredExecution1() {
            List<string> original = GetListOfNames();
            var q1 = original.Where(w => w.StartsWith("A")).Select(w => w);
            original.Add("Andrea");
            foreach (var item in q1) {
                Console.WriteLine(item);
            }
        }

        public static void DeferredExecution2() {
            List<string> original = GetListOfNames();
            var q1 = original.Where(w => w.StartsWith("A")).Select(w => w).ToList();
            original.Add("Andrea");
            foreach (var item in q1) {
                Console.WriteLine(item);
            }
        }

        public static void IntoKeyword1() {
            List<string> original = GetListOfNames();
            var q1 = original
                .Select(w => w.Replace("a", "").Replace("e", "").Replace("i", "").Replace("o", "").Replace("u", ""))
                .Where(n=>n.Length>2)
                .OrderBy(n=>n);
            foreach (var item in q1) {
                Console.WriteLine(item);
            }
        }

        public static void IntoKeyword2() {
            List<string> original = GetListOfNames();

            //wrong!
            var q1 = from w in original
                     where w.Length > 2
                     orderby w
                     select w.Replace("a", "").Replace("e", "").Replace("i", "").Replace("o", "").Replace("u", "");
                    
            foreach (var item in q1) {
                Console.WriteLine(item);
            }
        }

        public static void IntoKeyword3() {
            List<string> original = GetListOfNames();

            var q1 = from w in original
                     select w.Replace("a", "").Replace("e", "").Replace("i", "").Replace("o", "").Replace("u", "");
            var q2 = from w in q1
                     where w.Length > 2
                     orderby w
                     select w;
                     

            foreach (var item in q2) {
                Console.WriteLine(item);
            }
        }

        public static void IntoKeyword4() {
            List<string> original = GetListOfNames();

            
            var q1 = from w in 
                     
                     from w in original
                     select w.Replace("a", "").Replace("e", "").Replace("i", "").Replace("o", "").Replace("u", "")
                     
                     where w.Length > 2
                     orderby w
                     select w;


            foreach (var item in q1) {
                Console.WriteLine(item);
            }
        }

        public static void IntoKeyword5() {
            List<string> original = GetListOfNames();


            var q1 = from w in original
                     select w.Replace("a", "").Replace("e", "").Replace("i", "").Replace("o", "").Replace("u", "")
                     into noVowels
                     where noVowels.Length > 2
                     orderby noVowels
                     select noVowels;

            foreach (var item in q1) {
                Console.WriteLine(item);
            }
        }

        public static void LetKeyword1() {
            List<string> original = GetListOfNames();

            var q = from w in original
                    select new { Original = w, Vowelless = w.Replace("a", "").Replace("e", "").Replace("i", "").Replace("o", "").Replace("u", "") }
                        into temp
                        where temp.Vowelless.Length > 2
                        orderby temp.Original
                        select temp;
            foreach (var item in q) {
                Console.WriteLine(item.Original + " " + item.Vowelless);
            }
        }

        public static void LetKeyword2() {
            List<string> original = GetListOfNames();

            var q = from w in original
                    let vowelless = w.Replace("a", "").Replace("e", "").Replace("i", "").Replace("o", "").Replace("u", "")
                    where vowelless.Length > 2
                    orderby vowelless
                    select new { Original = w, Vowelless = vowelless };
                        
            foreach (var item in q) {
                Console.WriteLine(item.Original + " " + item.Vowelless);
            }
        }

        private static List<string> GetListOfNames() {
            List<string> original = new List<string> { "Alice", "Candice", "Jack", "Janis", "Bob", "Clara", "David", "Frank", "Daniel", "Armando", "Andrea", "John", "Simona", "Marco", "Mario", "Samantha" };
            return original;
        }



        private static string[] GetArrayOfNames() {
            string[] names = { "Alice", "Candice", "Jack", "Janis", "Bob", "Clara", "David", "Frank", "Daniel", "Armando", "Andrea", "John", "Simona", "Marco", "Mario", "Samantha" };
            return names;
        }
    }


}
