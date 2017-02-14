using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Name}";
        }

        public static List<Category> GetCategories() {
            return new List<Category>() {
                new Category() { Id = 1, Name = "Pasta" },
                new Category() { Id = 2, Name = "Beverages"},
                new Category() { Id = 3, Name = "Other"}
            };
        }
    }
}
