using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public override string ToString()
        {
            return $"{Id} - {Name} - {City}";
        }

        public static List<Supplier> GetSuppliers() {
            return new List<Supplier>() {
                new Supplier() { Id=1, Name="Dallas Cowboys", City="Dallas" },
                new Supplier() { Id=2, Name="Dallas Movers", City="Dallas" },
                new Supplier() { Id=3, Name="Torino Traslochi", City="Torino" },
                new Supplier() { Id=4, Name="Seattle FastMovers", City="Seattle" }
            };
        }
    }
}
