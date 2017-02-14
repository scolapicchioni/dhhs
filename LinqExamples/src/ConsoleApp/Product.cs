using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples
{
    public class Product
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        public override string ToString()
        {
            return $"ProductId: {Id} - Price: {Price} - CategoryId: {CategoryId}";
        }
        public static List<Product> GetProducts() {
            return new List<Product>() {
                new Product() { Id=1, Price=10, CategoryId = 1},
                new Product() { Id=2, Price=20, CategoryId = 1},
                new Product() { Id=3, Price=30, CategoryId = 2},
                new Product() { Id=4, Price=40, CategoryId = 2},
                new Product() { Id=5, Price=50, CategoryId = 2},
                new Product() { Id=6, Price=60, CategoryId = 2}
            };
        }
    }
}
