using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples
{
    public class LinqQueriesFinal
    {
        public static void Ex01()
        {
            var singersAndGuitarPlayers =
                from Musician in Musician.GetMusicians()
                from instrument in Musician.Instruments
                    //where instrument == "Guitar" || instrument == "Vocals"
                select new { Musician.Name, instrument };
            foreach (var item in singersAndGuitarPlayers)
            {
                Console.WriteLine($"{item.Name} {item.instrument}");
            }
            Console.WriteLine("-----------------------");
            foreach (var x in Musician.GetMusicians().SelectMany(m => m.Instruments).Distinct().OrderBy(i => i))
            {
                Console.WriteLine(x);
            }
        }

        public static void Ex02()
        {
            var singersAndGuitarPlayers =
                from Musician in Musician.GetMusicians()
                where (
                    from i in Musician.Instruments
                    where i == "Guitar"
                    select i
                ).Any()
                select Musician;
            foreach (var item in singersAndGuitarPlayers)
            {
                Console.WriteLine(item.Name);
            }

        }

        public static void Ex03()
        {
            //linq is available as extension methods of the Enumerable class
            IEnumerable<Car> cars = Enumerable.Where(Car.GetCars(), c => c.Price > 10000);
            foreach (var item in cars)
            {
                Console.WriteLine(item);
            }
        }
        public static void Ex04()
        {
            List<Car> list = Car.GetCars();
            //this line will be compiled as the previous example
            IEnumerable<Car> cars = list.Where(c => c.Price > 10000);
            foreach (var item in cars)
            {
                Console.WriteLine(item);
            }
        }

        public static void Ex05()
        {
            //there are more ore less 40 methods.
            //some of them accept no input and generate an output sequence:
            IEnumerable<int> numbers = Enumerable.Range(0, 10);
            foreach (var n in numbers)
            {
                Console.WriteLine(n);
            }

            IEnumerable<string> song = Enumerable.Repeat("NA", 16);
            foreach (var na in song)
            {
                Console.Write(na);
            }
            Console.WriteLine("BATMAAAAN!");
        }
        public static void Ex06()
        {
            //some accept an input sequence and output a scalar value:
            int[] numbers = { 5, 1, 6, 2, 8, 3, 9, 4, 0, 5 };
            int max = numbers.Max();
            int min = numbers.Min();
            double avg = numbers.Average();
            int count = numbers.Count();
            bool doesTheCollectionContainAnything = numbers.Any();


            //some can also accept predicates
            bool IsThereAnyEvenNumber = numbers.Any(n => n % 2 == 0);
            bool AreTheyAllEvenNumbers = numbers.All(n => n % 2 == 0);
            int multiply = numbers.Aggregate((n1, n2) => n1 * n2);

        }
        public static void Ex07()
        {
            //some accept an input sequence and output one element:
            int[] numbers = { 5, 1, 6, 2, 8, 3, 9, 4, 0, 5 };
            int first = numbers.First();
            int firstEven = numbers.First(n => n % 2 == 0);
            int last = numbers.Last();
            int lastEven = numbers.Last(n => n % 2 == 0);
            int zero = numbers.Single(n => n == 0);

        }

        public static void Ex08()
        {
            //some accept two input sequences and output one sequence of non repeated elements
            int[] numbers = { 1, 2, 3, 4, 3 };
            int[] numbers2 = { 5, 6, 1, 3, 6 };
            IEnumerable<int> union = numbers.Union(numbers2);
            IEnumerable<int> intersection = numbers.Intersect(numbers2);
            IEnumerable<int> except = numbers.Except(numbers2);
            Console.WriteLine("UNION");
            foreach (var n in union)
            {
                Console.WriteLine(n);
            }

            Console.WriteLine("INTERSECTION");
            foreach (var n in intersection)
            {
                Console.WriteLine(n);
            }

            Console.WriteLine("EXCEPT");
            foreach (var n in except)
            {
                Console.WriteLine(n);
            }
        }
        public static void Ex09()
        {
            //the methods returning a scalar values are immediately executed,
            //but the ones returning IEnumerables are delayed until the collection
            //is consumed by a foreach
            List<int> numbers = new List<int> { 1, 2, 3 };

            IEnumerable<int> result = numbers.Where(n =>
            {
                Console.WriteLine($"\tWHERE {n}");
                return n % 2 == 0;
            });

            //without the foreach, we don't see anything on screen

            foreach (var n in result)
            {
                Console.WriteLine(n);
            }

            //if we add a new item in the list and then we execute the foreach,
            //we will find the new item in the result 

            //numbers.Add(4);
            //foreach (var n in result)
            //{
            //    Console.WriteLine(n);
            //}
        }

        public static void Ex10()
        {
            //because many linq methods return an IEnumerable,
            //we can chain them together

            List<string> names = new List<string> { "Tom", "Dick", "Harry", "Mary", "Jay" };
            IEnumerable<string> query =
                names.Where(n => n.Contains("a"))
                     .OrderBy(n => n.Length)
                     .Select(n => n.ToUpper());

            names.Add("Ada");

            foreach (string name in query)
            {
                Console.WriteLine(name);
            }
        }

        public static void Ex11()
        {
            //let's print something, to see when and how each methods get executed

            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };
            IEnumerable<string> query =
                names.Where(n =>
                {
                    Console.WriteLine($"\tWHERE {n}");
                    return n.Contains("a");
                }).OrderBy(n =>
                {
                    Console.WriteLine($"\t\tORDERBY {n}");
                    return n.Length;
                }).Select(n =>
                {
                    Console.WriteLine($"\t\t\tSELECT {n}");
                    return n.ToUpper();
                });

            foreach (string name in query)
            {
                Console.WriteLine(name);
            }
        }

        public static void Ex12()
        {
            //some of the extension methods (but not all)
            //are also available as linq keywords (query syntax)
            //They will be translated as extension methods

            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };

            IEnumerable<string> query = from n in names
                                        where n.Contains("a")
                                        orderby n.Length
                                        select n.ToUpper();

            foreach (string name in query)
            {
                Console.WriteLine(name);
            }
        }

        public static void Ex13()
        {
            //so let's explore the syntax of the linq queries
            //each query starts with a from
            //in order to declare a range variable.
            //it ends either with a select or a group by

            /*
                                                        select
                                                       /      \
            from -------------------------------------<        >
                                                      \        /
                                                       group by
            */

            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };

            IEnumerable<string> query = from n in names //n is a range variable
                                        select n.ToUpper(); //you can use n in the rest of the query

            foreach (var name in query)
            {
                Console.WriteLine(name);
            }
        }

        public static void Ex14()
        {
            //a select will always output as many items as the input, 
            //but they may be different shaped items

            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };

            var query = from n in names
                        select new { Name = n.ToUpper(), n.Length }; //anonymous class, that's why we use var

            foreach (var item in query)
            {
                Console.WriteLine($"The name {item.Name} is {item.Length} letters long");
            }
        }

        public static void Ex15()
        {
            //if you want a hyerarchical output,
            //you can use group by instead

            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay", "Tamara", "Tim", "Dante", "Martha", "Marco", "John", "James" };
            var query = from n in names
                        group n by n.Substring(0, 1);

            foreach (var grouping in query)
            {
                Console.WriteLine(grouping.Key);
                foreach (var item in grouping)
                {
                    Console.WriteLine("\t" + item);
                }
            }
        }

        public static void Ex16()
        {
            //you can use any key to group by 
            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay", "Tamara", "Tim", "Dante", "Martha", "Marco", "Mitch", "Tod", "John", "James" };

            var query = from n in names
                        group n by new { Initial = n.Substring(0, 1), n.Length };

            foreach (var grouping in query)
            {
                Console.WriteLine($"{grouping.Key.Initial} - {grouping.Key.Length}");
                foreach (var item in grouping)
                {
                    Console.WriteLine("\t" + item);
                }
            }
        }

        public static void Ex17()
        {
            //you can group anything you need 
            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay", "Tamara", "Tim", "Dante", "Martha", "Marco", "Mitch", "Tod", "John", "James" };

            var query = from n in names
                        group new { Name = n.ToUpper(), n.Length } by n.Substring(0, 1);

            foreach (var grouping in query)
            {
                Console.WriteLine($"{grouping.Key}");
                foreach (var item in grouping)
                {
                    Console.WriteLine($"\t{item.Name} - {item.Length}");
                }
            }
        }

        public static void Ex18()
        {
            //between a from and a select (or group by)
            //there are many keywords you can use : where

            /*
                        where
                       /     \                          select
                      /       \                        /      \
            from ----<--------->----------------------<        >
                                                      \        /
                                                       group by
            */

            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay", "Tamara", "Tim", "Dante", "Martha", "Marco", "Mitch", "Tod", "John", "James" };

            var query = from n in names
                        where n.StartsWith("T")
                        select n;

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
            //or
            var query2 = from n in names
                         where n.Length > 3
                         group n by n.Substring(0, 1);

            foreach (var item in query2)
            {
                Console.WriteLine(item.Key);
                foreach (var name in item)
                {
                    Console.WriteLine($"\t{name}");
                }
            }
        }

        public static void Ex19()
        {
            //between a from and a select (or group by)
            //there are many keywords you can use : orderby

            /*
                        where
                       /     \                          select
                      /orderby\                        /      \
            from ----<--------->----------------------<        >
                                                      \        /
                                                       group by
            */

            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay", "Tamara", "Tim", "Dante", "Martha", "Marco", "Mitch", "Tod", "John", "James" };

            var query = from n in names
                        orderby n
                        select n;

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }

            //or
            var query2 = from n in names
                         orderby n
                         group n by n.Substring(0, 1);

            Console.WriteLine("------------");
            foreach (var item in query2)
            {
                Console.WriteLine(item.Key);
                foreach (var name in item)
                {
                    Console.WriteLine($"\t{name}");
                }
            }

            //or
            var query3 = from n in names
                         orderby n, n.Length //descending
                         select n;
            Console.WriteLine("------------");
            foreach (var item in query3)
            {
                Console.WriteLine(item);
            }
            //names.OrderBy(n=>n).ThenBy(n=>n.Length)
        }


        public static void Ex20()
        {
            //between a from and a select (or group by)
            //there are many keywords you can use : let

            /*
                        where
                       /     \                          select
                      /orderby\                        /      \
            from ----<--------->----------------------<        >
                      \  let  /                       \        /
                                                       group by
            */

            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay", "Tamara", "Tim", "Dante", "Martha", "Marco", "Mitch", "Tod", "John", "James" };

            //let can increase speed and / or readability

            //Example: I want to select the names with more than 3 consonants.
            //it means I have to remove all the vowels aeiou
            //I could do it like this

            var q1 = from n in names
                     where (n.Replace("a", "").Replace("e", "").Replace("i", "").Replace("o", "").Replace("u", "")).Length > 3
                     select n.Replace("a", "").Replace("e", "").Replace("i", "").Replace("o", "").Replace("u", "");

            //but this is not readable and also not performant
            //with the let keyword we can introduce a new range variable (novowel, in this case)
            var q2 = from n in names
                     let novowel = n.Replace("a", "").Replace("e", "").Replace("i", "").Replace("o", "").Replace("u", "")
                     where novowel.Length > 3
                     select novowel;

            //the previous range variable is still in scope, so we can still use it
            var q3 = from n in names
                     let novowel = n.Replace("a", "").Replace("e", "").Replace("i", "").Replace("o", "").Replace("u", "")
                     where novowel.Length > 3
                     select novowel + " comes from " + n;

            foreach (var item in q1)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-----------------------");
            foreach (var item in q2)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-----------------------");
            foreach (var item in q3)
            {
                Console.WriteLine(item);
            }

        }

        public static void Ex21()
        {
            //you can combine multiple techniques.
            //"progressive building" is a technique where you build multiple queries after each other
            //continuing from where you just left (q2 uses initials as input in this example)
            //You can use hybrid queries if you miss the linq operators (Distinct in this case)
            //You can use let for speed and readability
            //You can use subqueries for grouping into a hyerarchical structure

            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay", "Tamara", "Tim", "Dante", "Martha", "Marco", "Mitch", "Tod", "John", "James" };

            var initials = (from n in names
                            let initial = n.Substring(0, 1).ToUpper()
                            orderby initial
                            select initial).Distinct();
            var q2 = from initial in initials
                     select new
                     {
                         Initial = initial,
                         Names = from n in names where n.Substring(0, 1).ToUpper() == initial select n
                     };

            foreach (var item in q2)
            {
                Console.WriteLine(item.Initial);
                foreach (var name in item.Names)
                {
                    Console.WriteLine("\t" + name);
                }

            }
        }

        public static void Ex22()
        {
            //so far we've seen how to project one list into another list
            //with select
            //or how to project a list into a hierarchycal structure
            //with group by or subqueries.

            //now we have two lists and we want to make one

            /*
            
            --------    --------      -----------
            |      |    |      |      |         |
            |      |----|      | ---> |         |
            |      |    |      |      |         |
            |      |    |      |      |         |
            --------    --------      -----------
            
            */
            //we can use the join keyword

            /*
                        where
                       /     \                          select
                      /orderby\                        /      \
            from ----<--------->----------------------<        >
                      \  let  /                       \        /
                       \     /                         group by
                         join
            */
            //the join keyword produces an inner equi join
            //in this example it means
            //that cars with no drivers
            //and drivers with no cars
            //will not show up in the result

            List<Car> cars = Car.GetCars();

            List<Driver> drivers = Driver.GetDrivers();

            var q1 = from c in cars
                     join d in drivers
                     on c.Id equals d.CarId
                     select new { d.Name, c.Brand, c.Model };

            foreach (var item in q1)
            {
                Console.WriteLine(item.Name + " drives " + item.Brand + " " + item.Model);
            }

        }

        
        public static void Ex23a()
        {
            //the join keyword also has an optional into keyword
            //with wich we can define a new range variable
            //using this technique we can achieve
            //a left outer join that will produce a hyerarchical output

            //now we have two lists and we want to make one

            /*
            
            --------    --------      --------
            |      |    |      |      |      |
            |      |----|      | ---> --------
            |      |    |      |          |     ----------
            |      |    |      |          ------|        |
            --------    --------                ----------
            
            */

            /*
                        where
                       /     \                          select
                      /orderby\                        /      \
            from ----<--------->----------------------<        >
                      \  let  /                       \        /
                       \     /                         group by
                     join [into]
            */

            List<Car> cars = Car.GetCars();

            List<Driver> drivers = Driver.GetDrivers();

            var q1 = from c in cars
                     //--------------------//
                     join d in drivers
                     on c.Id equals d.CarId
                     into cardrivers       //new range variable
                     //--------------------//
                     orderby c.Brand, c.Model
                     //we selected all the cars up above
                     //so we will also get the cars with no drivers this time
                     //while the cardrivers collection could be null
                     select new { Car = c, Drivers = cardrivers};

            foreach (var item in q1)
            {
                Console.WriteLine(item.Car.Brand + " " + item.Car.Model);
                foreach (var d in item.Drivers)
                {
                    Console.WriteLine("\t" + d.Name + " " + d.Surname);
                }
            }
        }

        public static void Ex23b()
        {
            //the join keyword also has an optional into keyword
            //with wich we can define a new range variable
            //using this technique we can achieve
            //a left outer join that will produce a hyerarchical output

            //now we have two lists and we want to make one

            /*
            
            --------    --------      --------
            |      |    |      |      |      |
            |      |----|      | ---> --------
            |      |    |      |          |     ----------
            |      |    |      |          ------|        |
            --------    --------                ----------
            
            */

            /*
                        where
                       /     \                          select
                      /orderby\                        /      \
            from ----<--------->----------------------<        >
                      \  let  /                       \        /
                       \     /                         group by
                     join [into]
            */

            List<Car> cars = Car.GetCars();

            List<Driver> drivers = Driver.GetDrivers();

            var q1 = from c in cars
                     //--------------------//
                     join d in drivers
                     on c.Id equals d.CarId
                     into cardrivers       //new range variable
                     //--------------------//
                     orderby c.Brand, c.Model
                     //we selected all the cars up above
                     //we will get also the cars with no drivers this time
                     //but we can create a default driver if cardrivers is empty
                     select new { Car = c, Drivers = cardrivers.DefaultIfEmpty(new Driver() { Name = "No", Surname = "Driver" }) };

            foreach (var item in q1)
            {
                Console.WriteLine(item.Car.Brand + " " + item.Car.Model);
                foreach (var d in item.Drivers)
                {
                    Console.WriteLine("\t" + d.Name + " " + d.Surname);
                }
            }
        }


        public static void Ex23c()
        {
            //the join keyword also has an optional into keyword
            //with wich we can define a new range variable
            //using this technique we can achieve
            //a left outer join that will produce a hyerarchical output

            //now we have two lists and we want to make one

            /*
            
            --------    --------      --------
            |      |    |      |      |      |
            |      |----|      | ---> --------
            |      |    |      |          |     ----------
            |      |    |      |          ------|        |
            --------    --------                ----------
            
            */

            /*
                        where
                       /     \                          select
                      /orderby\                        /      \
            from ----<--------->----------------------<        >
                      \  let  /                       \        /
                       \     /                         group by
                     join [into]
            */

            List<Car> cars = Car.GetCars();

            List<Driver> drivers = Driver.GetDrivers();

            var q1 = from c in cars
                     //-------------------//
                     join d in drivers
                     on c.Id equals d.CarId
                     into cardrivers         //new range variable
                     //--------------------//
                     orderby c.Brand, c.Model
                     //we selected all the cars up above
                     //but we can filter
                     //so that we take only the cars with drivers,
                     //effectively going back to an inner join
                     where cardrivers.Any()
                     select new { Car = c, Drivers = cardrivers };

            foreach (var item in q1)
            {
                Console.WriteLine(item.Car.Brand + " " + item.Car.Model);
                foreach (var d in item.Drivers)
                {
                    Console.WriteLine("\t" + d.Name + " " + d.Surname);
                }
            }
        }

        public static void Ex24()
        {
            //another left outer join that will produce a hyerarchical output

            /*
            
            --------    --------      --------
            |      |    |      |      |      |
            |      |----|      | ---> --------
            |      |    |      |          |     ----------
            |      |    |      |          ------|        |
            --------    --------                ----------
            
            */

            /*
                        where
                       /     \                          select
                      /orderby\                        /      \
            from ----<--------->----------------------<        >
                      \  let  /                       \        /
                       \     /                         group by
                     join [into]
            */

            List<Car> cars = Car.GetCars();

            List<Driver> drivers = Driver.GetDrivers();

            var q1 = from d in drivers
                     join c in cars
                     on d.CarId equals c.Id
                     into carsfordriver
                     select new { Driver = d, Cars = carsfordriver };

            foreach (var item in q1)
            {
                Console.WriteLine(item.Driver.Name + " " + item.Driver.Surname);
                foreach (var c in item.Cars)
                {
                    Console.WriteLine("\t" + c.Model + " " + c.Price);
                }
            }
        }

        public static void Ex25()
        {
            /*
             * An additional from keyword can be use to get a cross join 
             */

            /*
                        where
                       /     \                          select
                      /orderby\                        /      \
            from ----<--------->----------------------<        >
                      \  let  /                       \        /
                       \     /                         group by
                     join [into]
                         \ /
                         from
            
            */


            List<Car> cars = Car.GetCars();

            List<Driver> drivers = Driver.GetDrivers();

            var query = from d in drivers
                        from c in cars //every driver connected to every car (cross join)
                        select new { c, d };
            foreach (var item in query)
            {
                Console.WriteLine($"{item.c.Brand} {item.c.Model} {item.d.Name} {item.d.Surname}");
            }
        }

        public static void Ex26()
        {
            /*
             * With the correct filters we can go from a the cross join to an outer join
             */

            /*
                        where
                       /     \                          select
                      /orderby\                        /      \
            from ----<--------->----------------------<        >
                      \  let  /                       \        /
                       \     /                         group by
                     join [into]
                         \ /
                         from
            
            */


            List<Car> cars = Car.GetCars();

            List<Driver> drivers = Driver.GetDrivers();

            var query = from d in drivers
                        from c in cars
                        where d.CarId != c.Id //we get the drivers connected to the cars they DON'T drive
                        select new { c, d };
            foreach (var item in query)
            {
                Console.WriteLine($"{item.d.Name} {item.d.Surname} DOES NOT DRIVE {item.c.Brand} {item.c.Model}");
            }
        }



        public static void Ex27()
        {
            //if you have a hyerarchical input
            //and you want to flatten it
            //you can use the from keyword again

            /*
            
             --------                    --------
             |      |                    |      |
             --------             --->   |      |
                 |     ----------        |      |
                 ------|        |        |      |
                       ----------        --------
            
            */

            /*
                        where
                       /     \                          select
                      /orderby\                        /      \
            from ----<--------->----------------------<        >
                      \  let  /                       \        /
                       \     /                         group by
                     join [into]
                         \ /
                         from
            
            */

            List<Musician> beatles = Musician.GetMusicians();

            var musiciansandinstrumentsflattened = from m in beatles
                                                   from i in m.Instruments
                                                   select new { m.Name, Instrument = i };
            foreach (var musician in musiciansandinstrumentsflattened)
            {
                Console.WriteLine(musician.Name + " plays " + musician.Instrument);
            }
        }

        public static void Ex28()
        {
            //another example of the from - from
            //to flatten a hyerarchical input
            
            /*
            
             --------                    --------
             |      |                    |      |
             --------             --->   |      |
                 |     ----------        |      |
                 ------|        |        |      |
                       ----------        --------
            
            */

            /*
                        where
                       /     \                          select
                      /orderby\                        /      \
            from ----<--------->----------------------<        > - into -
                      \  let  /                       \        /
                       \     /                         group by
                     join [into]
                         \ /
                         from
            
            */

            string[] names = { "John Mark", "Frank Steve Fanny", "Phillis James Mary Dave" };

            var q = from n in names
                    from name in n.Split()
                    select name;
            foreach (var item in q)
            {
                Console.WriteLine(item);
            }
        }

        public static void Ex29() {
            //in this last example, combining many different techniques,
            //we get a flat left join.
            //the DefaultIfEmpty method will return a list with a default value
            //should the list be empty 
             
            List<Car> cars = Car.GetCars();

            List<Driver> drivers = Driver.GetDrivers();

            var q1 = from c in cars
                     join d in drivers
                     on c.Id equals d.CarId
                     into driversforcar
                     from driverforcar in driversforcar.DefaultIfEmpty(new Driver() { Name = "No" , Surname = "Driver"})
                     select new { c, driverforcar };

            foreach (var item in q1)
            {
                Console.WriteLine(item.c + " " + item.driverforcar);
            }
        }
    }
}