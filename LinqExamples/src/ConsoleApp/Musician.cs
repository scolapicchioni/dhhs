using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples {
    public class Musician {
        public string Name { get; set; }
        public string City { get; set; }
        public List<string> Instruments { get; set; }

        public static List<Musician> GetMusicians() {
            List<Musician> beatles = new List<Musician>{ 
	            new Musician{Name="Paul", City="Liverpool", Instruments =new List<string>{"Bass", "Guitar", "Vocals"}}, 
	            new Musician{Name="John", City="New York", Instruments =new List<string>{"Guitar", "Piano", "Vocals"}}, 
	            new Musician{Name="George", City="Liverpool", Instruments =new List<string>{"Guitar", "Vocals"}}, 
	            new Musician{Name="Ringo", City="Los Angeles", Instruments = new List<string>{"Drums", "Vocals" }}
            };
            return beatles;
        }
    }
}
