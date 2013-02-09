using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleAppPriceCut
{
    public delegate void priceCutEvent(Int32 pr);
    class ChickenFarm
    {
        static Random rng = new Random();
        public static event priceCutEvent priceCut;
        private static Int32 chickenPrice = 10;
        public Int32 getPrice() { return chickenPrice; }
        public static void changePrice(Int32 price)
        {
            if (price < chickenPrice)
            {
                if (priceCut != null)
                    priceCut(price);
            }
            chickenPrice = price;
        }
        public void farmerFunc()
        {
            for (Int32 i = 0; i < 50; i++)
            {
                Thread.Sleep(500);
                Int32 p = rng.Next(5, 10);
                ChickenFarm.changePrice(p);
            }
        }
        public class Retailer
        {
            public void retailerFunc()
            {
                ChickenFarm chicken = new ChickenFarm();
                for (Int32 i = 0; i < 10; i++)
                {
                    Thread.Sleep(1000);
                    Int32 p = chicken.getPrice();
                    Console.WriteLine("Store{0} has everyday low price: ${1} each", Thread.CurrentThread.Name, p);
                }
            }
            public void chickenOnSale(Int32 p)
            {
                Console.WriteLine("Store{0} chickens are on sale: as low as ${1} each", Thread.CurrentThread.Name, p);
            }
        }
        public class myApplication
        {
            static void Main(String[] args)
            {
                ChickenFarm chicken = new ChickenFarm();
                Thread farmer = new Thread(new ThreadStart(chicken.farmerFunc));
                farmer.Start();
                Retailer chickenstore = new Retailer();
                ChickenFarm.priceCut += new priceCutEvent(chickenstore.chickenOnSale);
                Thread[] retailers = new Thread[5];
                for (int i = 0; i < 3; i++)
                {
                    retailers[i] = new Thread(new ThreadStart(chickenstore.retailerFunc));
                    retailers[i].Name = (i + 1).ToString();
                    retailers[i].Start();
                }
            }
        }
    }
}
