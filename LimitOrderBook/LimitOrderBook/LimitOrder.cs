using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook
{
    public class LimitOrder : ILimitOrder
    {

        Dictionary<string, Order> OrderDict = new Dictionary<string, Order>();
        
        Dictionary<string, int> OrderBook_Buy = new Dictionary<string, int>();
        Dictionary<string, int> OrderBook_Sell = new Dictionary<string, int>();

        public List<string> FilledOrders = new List<string>();

        public string CancelOrder(string id)
        {

            if (OrderDict.ContainsKey(id))
            {
                    OrderDict.Remove(id);
                    return "OK";
                
            }

            if (FilledOrders.Contains(id))
            {
                return "Failed - already fully filled";
            }


            else { return "Failed - no such active order"; }
        }
            
        
        public string PlaceOrder(string id, string side, int quantity, int price)
        {
            if (side == "Sell") //Sell
            {
                if (OrderBook_Buy.Count != 0)
                {
                    foreach (KeyValuePair<string, int> pair in OrderBook_Buy)
                    {
                        if (pair.Value >= price) //check if Buy price >= sell price
                        {
                            //subtract quantity from buy side:
                            int new_quantity = OrderDict[pair.Key].Quantity - quantity;
                            int taken = OrderDict[pair.Key].Quantity - new_quantity;
                            

                            if (new_quantity == 0)
                            {                                
                                string output = "Fully matched with" + pair.Key + "(" + OrderDict[pair.Key].Quantity + " @" + pair.Value + ")";
                                
                                
                                //fully matched remove from either/both sides: (dont add new buy order)
                                OrderBook_Buy.Remove(pair.Key);
                                OrderDict.Remove(pair.Key); // remove from orderdict too

                                FilledOrders.Add(pair.Key);
                                FilledOrders.Add(id);
                                return output;
                            }
                            else if (new_quantity < 0)
                            {
                                //partially matched
                                int remainder = Math.Abs(new_quantity);


                                string output = "Partially matched with " + pair.Key + "(" + OrderDict[pair.Key].Quantity + "@" + pair.Value + ")";

                                OrderDict.Remove(pair.Key); //remove the buy order because no quantity

                                OrderBook_Sell.Add(id, price); //add the sell order
                                OrderDict.Add(id, new Order() { Id = id, Price = price, Quantity = remainder, Side = side });

                                FilledOrders.Add(pair.Key);
                                FilledOrders.Add(id);
                                return output;
                            }
                            else
                            {
                                OrderDict[pair.Key].Quantity = new_quantity;  //remove the quantity from the ID on sell side

                                FilledOrders.Add(pair.Key);
                                FilledOrders.Add(id);
                                string output = "Fully matched with " + pair.Key + "(" + taken + "@" + pair.Value + ")";
                                return output;
                            }

                        }

                    }

                }
                
                OrderBook_Sell.Add(id, price);
                OrderDict.Add(id, new Order() { Id = id, Price = price, Quantity = quantity, Side = side });
                return "OK";
            }


            else if (side == "Buy") //Buy
            {
                if (OrderBook_Sell.Count != 0)
                {
                    foreach (KeyValuePair<string, int> pair in OrderBook_Sell)
                    {
                        if (price >= pair.Value) //check if Buy price >= sell price
                        {
                          
                            //subtract quantity from sell side:
                            int new_quantity = OrderDict[pair.Key].Quantity - quantity;
                            int taken = OrderDict[pair.Key].Quantity - new_quantity;
                       

                            if (new_quantity == 0)
                            {

                                string output = "Fully matched with " + pair.Key + "(" + OrderDict[pair.Key].Quantity + "@" + pair.Value + ")";

                                //fully matched remove from both sides: (dont add new buy order)
                                OrderBook_Sell.Remove(pair.Key);
                                OrderDict.Remove(pair.Key); // remove from orderdict too
                                
                                

                                FilledOrders.Add(pair.Key);
                                FilledOrders.Add(id);
                                return output;
                            }
                            else if (new_quantity < 0)
                            {
                                //partially matched
                                int remainder = Math.Abs(new_quantity);
                               
                                string output = "Fully matched with " + pair.Key + "(" + OrderDict[pair.Key].Quantity + "@" + pair.Value + ")";

                                OrderDict.Remove(pair.Key); //remove the sell order because no quantity

                                OrderBook_Buy.Add(id, price); //add the buy order
                                OrderDict.Add(id, new Order() { Id = id, Price = price, Quantity = remainder, Side = side });

                                FilledOrders.Add(pair.Key);
                                FilledOrders.Add(id);
                                return output;
                            }
                            else
                            {
                                OrderDict[pair.Key].Quantity = new_quantity;  //remove the quantity from the ID on sell side

                                FilledOrders.Add(pair.Key);
                                FilledOrders.Add(id);

                                string output = "Fully matched with " + pair.Key + "(" + OrderDict[pair.Key].Quantity + "@" + pair.Value + ")";
                                return output;
                            }

                        }

                    }
                }
                else
                {
                    OrderBook_Buy.Add(id, price);
                    OrderDict.Add(id, new Order() { Id = id, Price = price, Quantity = quantity, Side = side });

                }
                return "OK";
            }

            else { return "Error!"; }
        }      
    }
}
