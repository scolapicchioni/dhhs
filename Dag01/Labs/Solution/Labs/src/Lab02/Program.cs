using DataHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Lab02
{
    public class Program
    {
        private static IEnumerable<Employee> Employees = DataSource.Employees;
        private static IEnumerable<Product> Products = DataSource.Products;
        private static IEnumerable<ProductVendor> ProductVendors = DataSource.ProductVendors;
        private static IEnumerable<Vendor> Vendors = DataSource.Vendors;

        static void Main(string[] args)
        {
            Console.WriteLine("Lab 02 - LINQ");
            Console.WriteLine("=============\n");

            Exercise01();
            Exercise02();
            Exercise03();
            Exercise04();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }

        static void Exercise01()
        {
            Console.WriteLine("Exercise 01 - Start");

            Console.WriteLine("\nQuestion 1");
            Console.WriteLine("Are there any employees with less than 21 sick leave hours?: " +
                (Employees.Any(employee => employee.SickLeaveHours < 21) ? "Yes" : "No"));

            Console.WriteLine("\nQuestion 2");
            Console.WriteLine("Number of employees with less than 21 sick leave hours: " +
                Employees.Count(employee => employee.SickLeaveHours < 21));

            Console.WriteLine("\nQuestion 3");
            IEnumerable<Employee> healthyEmployees = Employees.Where(employee => employee.SickLeaveHours < 21);
            foreach (Employee employee in healthyEmployees)
            {
                Console.WriteLine(string.Format("{0}, {1}, {2}", employee.Name, employee.Gender, employee.SickLeaveHours));
            }

            Console.WriteLine("\nQuestion 4");
            IEnumerable<Employee> orderedHealthyEmployees = healthyEmployees.OrderBy(employee => employee.Gender).ThenBy(employee => employee.Name);
            foreach (Employee employee in orderedHealthyEmployees)
            {
                Console.WriteLine(string.Format("{0}, {1}, {2}", employee.Name, employee.Gender, employee.SickLeaveHours));
            }

            Console.WriteLine("\nExercise 01 - End\n");
        }

        static void Exercise02()
        {
            Console.WriteLine("Exercise 02 - Start");

            Console.WriteLine("\nQuestion 1");
            Console.WriteLine("Are there any employees with less than 21 sick leave hours?: " +
                ((from employee in Employees where employee.SickLeaveHours < 21 select employee).Count() > 0 ? "Yes" : "No"));

            Console.WriteLine("\nQuestion 2");
            Console.WriteLine("Number of employees with less than 21 sick leave hours: " +
                (from employee in Employees where employee.SickLeaveHours < 21 select employee).Count());

            Console.WriteLine("\nQuestion 3");
            IEnumerable<Employee> healthyEmployees = from employee in Employees where employee.SickLeaveHours < 21 select employee;
            foreach (Employee employee in healthyEmployees)
            {
                Console.WriteLine(string.Format("{0}, {1}, {2}", employee.Name, employee.Gender, employee.SickLeaveHours));
            }

            Console.WriteLine("\nQuestion 4");
            IEnumerable<Employee> orderedHealthyEmployees = from employee in healthyEmployees orderby employee.Gender, employee.Name select employee;
            foreach (Employee employee in orderedHealthyEmployees)
            {
                Console.WriteLine(string.Format("{0}, {1}, {2}", employee.Name, employee.Gender, employee.SickLeaveHours));
            }

            Console.WriteLine("\nExercise 02 - End\n");
        }

        static void Exercise03()
        {
            Console.WriteLine("Exercise 03 - Start");

            Console.WriteLine("\nQuestion 1");

            var productOfferings = from product in Products
                                   join productVendor in ProductVendors
                                   on product.ID equals productVendor.ProductID
                                   select new { ProductName = product.Name, Price = productVendor.Price };

            foreach (var productOffering in productOfferings)
            {
                Console.WriteLine("Product: {0}: {1}", productOffering.ProductName, productOffering.Price);
            }

            Console.WriteLine("\nQuestion 2");

            var productVendorOfferings = from product in Products
                                         join productVendor in ProductVendors
                                         on product.ID equals productVendor.ProductID
                                         join vendor in Vendors
                                         on productVendor.VendorID equals vendor.ID
                                         select new
                                         {
                                             ProductName = product.Name,
                                             Price = productVendor.Price,
                                             VendorName = vendor.Name
                                         };

            foreach (var productOffering in productVendorOfferings)
            {
                Console.WriteLine("Product: {0}: {1} by {2}", productOffering.ProductName, productOffering.Price, productOffering.VendorName);
            }

            Console.WriteLine("\nQuestion 3");

            var productOfferingGroups = from product in productVendorOfferings
                                        group product by product.ProductName;

            foreach (var productOfferingGroup in productOfferingGroups)
            {
                Console.WriteLine("\nProduct: {0}", productOfferingGroup.Key);
                foreach (var productOffering in productOfferingGroup)
                {
                    Console.WriteLine("Offering: {0} by {1}", productOffering.Price, productOffering.VendorName);
                }
            }

            Console.WriteLine("\nQuestion 4");

            var cheapestProductOfferings = from product in productVendorOfferings
                                           group product by product.ProductName into allProductOfferings
                                           from productOffering in allProductOfferings
                                           where productOffering.Price == allProductOfferings.Min(offering => offering.Price)
                                           select productOffering;

            foreach (var productOffering in cheapestProductOfferings)
            {
                Console.WriteLine("{0} by {1} for {2}", productOffering.ProductName, productOffering.VendorName, productOffering.Price);
            }

            Console.WriteLine("\nExercise 03 - End\n");
        }

        private static void Exercise04()
        {
            Console.WriteLine("Exercise 04 - Start");
            Exercise04a();
            Exercise04b();
            Console.WriteLine("\nExercise 04 - End\n");
        }

        static void Exercise04a()
        {

            Stopwatch stopwatch = new Stopwatch();
            var healthInfo = from employee in Employees
                             orderby employee.SickLeaveHours * 1000 / (DateTime.Now - employee.BirthDate).TotalHours
                             select new { employee.Name, SickLeaveAgeRatio = employee.SickLeaveHours * 1000 / (DateTime.Now - employee.BirthDate).TotalHours };

            stopwatch.Start();

            healthInfo.ToArray();

            long time = stopwatch.ElapsedMilliseconds;

            stopwatch.Stop();

            Console.WriteLine("Duration no let  : {0} ms.", time);

        }

        static void Exercise04b()
        {
            Stopwatch stopwatch = new Stopwatch();
            var healthInfo2 = from employee in Employees
                              let sickLeaveRatio = employee.SickLeaveHours * 1000 / (DateTime.Now - employee.BirthDate).TotalHours
                              orderby sickLeaveRatio
                              select new { employee.Name, SickLeaveAgeRatio = sickLeaveRatio };
            stopwatch.Start();

            healthInfo2.ToArray();

            long time = stopwatch.ElapsedMilliseconds;

            stopwatch.Stop();

            Console.WriteLine("Duration with let: {0} ms.", time);
        }
    }
}
