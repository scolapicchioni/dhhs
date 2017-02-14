using DataHelper;
using System;
using System.Collections.Generic;
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

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }

        static void Exercise01()
        {
            Console.WriteLine("Exercise 01 - Start");

            //TODO: Add code here

            Console.WriteLine("Exercise 01 - End\n");
        }
    }
}
