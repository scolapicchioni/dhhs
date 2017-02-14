using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples {
    public class Car {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public override string ToString() {
            return this.Id + " " + this.Brand + " " + this.Model + " " + this.Price;
        }


        public static List<Car> GetCars() {
            List<Car> cars = new List<Car> { 
                new Car{Id = 1, Brand="Alfa Romeo", Model = "147", Price = 15000},
                new Car{Id = 2, Brand="Alfa Romeo", Model = "MiTo", Price = 16000},
                new Car{Id = 8, Brand="Alfa Romeo", Model = "Giulietta", Price = 11000},
                new Car{Id = 3, Brand="Audi", Model = "A3", Price = 22000},
                new Car{Id = 4, Brand="Audi", Model = "A2", Price = 23000},
                new Car{Id = 9, Brand="Audi", Model = "A1", Price = 20000},
                new Car{Id = 5, Brand="Citroen", Model = "C1", Price = 11000},
                new Car{Id = 6, Brand="Citroen", Model = "C2", Price = 13000},
                new Car{Id = 7, Brand="FIAT", Model = "500", Price = 14000},
                new Car{Id = 10, Brand="FIAT", Model = "Panda", Price = 9000}
            };
            return cars;
        }
    }
}
