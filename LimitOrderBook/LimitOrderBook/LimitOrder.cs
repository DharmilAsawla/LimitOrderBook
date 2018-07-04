using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook
{
    public class LimitOrder : ILimitOrder
    {
        public string CancelOrder(string id)
        {
            throw new NotImplementedException();
        }

        public string PlaceOrder(string id, string side, int quantity , int price)
        {
            throw new NotImplementedException();
        }
    }
}
