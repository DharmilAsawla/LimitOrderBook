using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook
{
    class Order
    {
        public string Id { get; set; }
        public string Side { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
