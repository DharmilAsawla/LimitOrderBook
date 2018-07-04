using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook
{
    interface ILimitOrder
    {
        string PlaceOrder(string id, string side, int quantity, int price);
        string CancelOrder(string id);
    }
}
