using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples
{
    public class Order
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public bool  Shipped { get; set; }
        public string Month { get; set; }
        public int ProductId { get; set; }
        public override string ToString()
        {
            return $"Order Id: {Id} - ProductId: {ProductId} - Quantity: {Quantity} - Shipped: {Shipped} - Month: {Month}";
        }
    }
}
