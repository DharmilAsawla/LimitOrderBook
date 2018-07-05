using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LimitOrderBook;

namespace LimitOrderBook_Tests
{
    [TestClass]
    public class Test_LOB
    {
        LimitOrder book = new LimitOrder();

        

        [TestMethod]
        public void PlaceOrders()
        {
            

            //Place orders
            Assert.AreEqual("OK", book.PlaceOrder("AAA", "Buy", 10, 10));


            Assert.AreEqual("OK", book.PlaceOrder("BBB", "Buy", 12, 12));
            Assert.AreEqual("OK", book.PlaceOrder("CCC", "Buy", 14, 14));



            //Cancel Order
            Assert.AreEqual("OK", book.CancelOrder("CCC"));

            Assert.AreEqual("OK", book.PlaceOrder("DDD", "Sell", 10, 15));

            Assert.AreEqual("Fully matched with BBB(2@12)", book.PlaceOrder("EEE", "Sell", 2, 12));


            Assert.AreEqual("Fully matched with BBB(4@12)", book.PlaceOrder("FFF", "Sell", 4, 12));

            Assert.AreEqual("Partially matched with BBB(6@12)", book.PlaceOrder("GGG", "Sell", 10, 12));

            Assert.AreEqual("Failed - already fully filled", book.CancelOrder("BBB"));


            Assert.AreEqual("Fully matched with GGG(4@12)", book.PlaceOrder("HHH", "Buy", 14, 12));


            //struglling on this test case
            Assert.AreEqual("Fully matched with HHH(10@12) and AAA(10@10)", book.PlaceOrder("KKK", "Sell", 20, 12));

            Assert.AreEqual("OK", book.CancelOrder("DDD"));
            Assert.AreEqual("Failed - no such active order", book.CancelOrder("DDD"));


            //book should now be empty
            //check if true



        }
    }

}
