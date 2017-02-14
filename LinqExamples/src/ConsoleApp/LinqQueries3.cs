using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples
{
    public static class LinqQueries3
    {
        public static void Ex01() {
            //inner join, we flatten the results (by duplicating the customer for each supplier)
            var q1 = from c in Customer.GetCustomers()
                     join s in Supplier.GetSuppliers()
                     on c.City equals s.City
                     select new { CustomerName = c.Name, SupplierName = s.Name, City = s.City };
            foreach (var item in q1)
            {
                Console.WriteLine($"{item.CustomerName} {item.SupplierName} {item.City}");
            }
        }

        public static void Ex02()
        {
            //left outer join, hierarchycal.
            //we first project the suppliers with the same city as the customer into a new collection,
            //then we select the customer and the collection of suppliers together,
            //where the collection is empty if there is no related supplier 
            var q1 = from c in Customer.GetCustomers()
                     join s in Supplier.GetSuppliers()
                     on c.City equals s.City
                     into SuppliersInSameCityAsCustomer
                     select new { Customer = c, SuppliersInSameCityAsCustomer};
            foreach (var item in q1)
            {
                Console.WriteLine(item.Customer);
                foreach (var sup in item.SuppliersInSameCityAsCustomer)
                {
                    Console.WriteLine($"\t{sup}");
                }
            }
        }

        public static void Ex03() {
            var q1 = from c in Customer.GetCustomers()
                     select new
                     {
                         Customer = c,
                         Products = (from o in c.Orders
                                     from p in Product.GetProducts()
                                     where o.ProductId == p.Id
                                     select p),
                         CustomerSuppliers = (from s in Supplier.GetSuppliers()
                                              where c.City == s.City
                                              select s)
                     };
            foreach (var item in q1)
            {
                Console.WriteLine(item.Customer);
                foreach (var P in item.Products)
                {
                    Console.WriteLine($"\tProduct: {P}");
                }
                foreach (var S in item.CustomerSuppliers)
                {
                    Console.WriteLine($"\tSupplier: {S}");
                }
            }
        }


        public static void Ex04() {
            var q1 = from c in Customer.GetCustomers()
                     group c by c.Country
                     into CustomersByCountry
                     select new { Country = CustomersByCountry.Key, Count = CustomersByCountry.Count() };
            foreach (var item in q1)
            {
                Console.WriteLine($"{item.Country} {item.Count}");
            }
        }

        public static void Ex05() {
            var q1 = from c in Category.GetCategories()
                     join p in Product.GetProducts()
                     on c.Id equals p.CategoryId
                     select new { c, p };

            foreach (var item in q1)
            {
                Console.WriteLine($"{item.c} --- {item.p}");
            }
        }

        public static void Ex06()
        {
            var q1 = from c in Category.GetCategories()
                     join p in Product.GetProducts()
                     on c.Id equals p.CategoryId
                     into pbyc
                     select new { c, pbyc };

            foreach (var item in q1)
            {
                Console.WriteLine($"{item.c}");
                foreach (var p in item.pbyc)
                {
                    Console.WriteLine($"\t{p}");
                }
            }
        }

        public static void Ex07()
        {
            var q1 = from c in Category.GetCategories()
                     join p in Product.GetProducts()
                     on c.Id equals p.CategoryId
                     into pbyc
                     from p in pbyc.DefaultIfEmpty(new Product() { Id = 0, Price = 0, CategoryId = 0})
                     select new { c, p};

            foreach (var item in q1)
            {
                Console.WriteLine($"{item.c} - {item.p} ");
            }
        }
    }
}
