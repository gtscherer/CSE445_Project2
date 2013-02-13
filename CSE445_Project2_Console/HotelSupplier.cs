using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CSE445_Project2_Console
{
    class HotelSupplier
    {
        /* 
         * Uses a PricingModel to determine the room prices.
         * Defines a price-cut event that can emit an event and call the event handlers in TravelAgencys if there is a price-cut according to the PricingModel.
         * Receives the orders (as a string) from the MultiCellBuffer.
         * Calls Decoder to convert the string into the order object.
         * For each order, starts a new thread (resulting in multiple threads for processing multiple orders) from OrderProcessing class to process the order based on current price.
         * There is a counter p -> and after p price cuts, the HotelSupplier thread will terminate.
         * Before generating the first price, a time stamp must be saved.
         * Before thread terminates, the total time used will be calculated and saved.
         */

        private PricingModel price_model;
        private Int32 price;
        public delegate void priceCutEvent(Int32 price);
        public static event priceCutEvent priceCut;
        private int[] numRooms;
        private int[] numPriceCuts;
        private DateTime now;


        public HotelSupplier()
        {
            // let's see
            price_model = new PricingModel();
            numRooms = new int[7];
            numPriceCuts = new int[7];
            // get start time
            now = DateTime.Now;
            Console.WriteLine(now);
            for (int i = 0; i < 7; ++i)
            {
                numRooms[i] = 50;
                numPriceCuts[i] = 0;
            }

            // the HotelSupplier will be active until 10 price cuts have been reached
            

        }
        public void hotelStarter()
        {
            for (Int32 i = 0; i < 11; )
            {
                //Console.WriteLine("For loop start: {0}", i);
                // get random number to scale price
                Random rand = new Random();
                int randomNumber = rand.Next(0, 100); //Will replaced with PriceModel.getPrice();


                // not sure whether we really would like to keep it this way
                Int32 newPrice = price_model.scalePrice(randomNumber);
                Thread.Sleep(50);
                Console.WriteLine("New Price is {0}", newPrice);

                // is there a lower price available?
                if (newPrice < price)
                {
                    // there is at least a subscriber
                    if (priceCut != null)
                    {
                        // emit / raise event to subscribers
                        priceCut(newPrice);
                    }
                    // increase the number of price cuts
                    i++;
                }
                price = newPrice;


                // not clear how orchestration should look like...

            }

            // get start time
            DateTime after = DateTime.Now;
            Console.WriteLine(after);
            long ticks = after.Ticks - now.Ticks;
            double seconds = TimeSpan.FromTicks(ticks).TotalSeconds;
            Console.WriteLine("Total runtime {0} seconds ", seconds);

            Console.ReadLine();
        }
        
        public void setOrder(OrderClass[] orderTable) {

            // get encoder - did we want to use a singleton here?
            Decoder    d = new Decoder();
            OrderClass order;
            Thread[] thread = new Thread[orderTable.Length];

            // decode all orders
            for (int i = 0; i < orderTable.Length; ++i)
            {

            // set the order an decrypt
            d.setOrder(orderTable[i].ToString());
            
            // get the decrypeted order as string
            order = d.getOrder();

            // need to call OrderProcessing
            thread[i] = new Thread(new ParameterizedThreadStart(Worker));
            thread[i].Start(order);

            }

        }

        private static void Worker(object order) {

            OrderProcessing p = new OrderProcessing((OrderClass)order);


        }


        public static bool checkCard(int cardNo)
        { 
            return true;
        }
    }
}
