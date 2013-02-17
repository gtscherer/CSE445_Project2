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
        public delegate void priceCutEvent(Int32 newPrice, Int32 price);
        public delegate void printTimesEvent();
        public delegate void printOrdersEvent();
        public static event priceCutEvent priceCut;
        public static event printTimesEvent printAgencyTimes;
        public static event printOrdersEvent printOrders;
        private EventWaitHandle[] waitHandle;
        private int[] numRooms;
        private int[] numPriceCuts;
        private DateTime now;
        private MultiCellBuffer buffer;
        private int counter = 0;
        private int totalAgencies;
        private int priceCuts = 10;

        public HotelSupplier(MultiCellBuffer buffer, int numOfAgencies)
        {
            
            this.buffer = buffer;
            price_model = new PricingModel();
            numRooms = new int[7];
            numPriceCuts = new int[7];
            // get start time
            now = DateTime.Now;
            Console.WriteLine(now);
            totalAgencies = numOfAgencies;
            for (int i = 0; i < 7; ++i)
            {
                numRooms[i] = 50;
                numPriceCuts[i] = 0;
            }
            waitHandle = new EventWaitHandle[1];
            waitHandle[0] = new EventWaitHandle(false, EventResetMode.AutoReset);
            MultiCellBuffer mcp = new MultiCellBuffer();
        }
        public void hotelStarter()
        {
            Console.WriteLine("Hotel thread created");
            //Connect event handler to let the hotel know when there are new orders
            MultiCellBuffer.notifyHotelOfOrder += new MultiCellBuffer.notifyHotelOfOrderEvent(this.notifyHotelOfOrder);

            int randomNumber = price_model.getPrice();
            // the HotelSupplier will be active until 10 price cuts have been reached
            for (Int32 i = 0; i < priceCuts; )
            {
                Int32 newPrice = price_model.cutPrice(randomNumber);
                price = randomNumber;
                Console.WriteLine("Old Price = {0}", randomNumber);
                Console.WriteLine("New Price is {0}", newPrice);
                // is there a lower price available?
                if (newPrice < price)
                {
                    // there is at least a subscriber
                    if (priceCut != null)
                    {
                        // emit event to subscribers
                        priceCut(newPrice, price);
                        //price is cut, wait for at least one order until moving forward
                        AutoResetEvent.WaitAny(waitHandle, 30000);
                    }
                    // increase the count of price cuts
                    i++;
                }
                price = newPrice;
                Thread.Sleep(1000);
                randomNumber = price_model.changePrice();
            }

            //Wait for all the Travel Agencys'  orders to finish
            while (counter < totalAgencies * priceCuts)
                Thread.Sleep(10);

            //print order times and total running time
            DateTime after = DateTime.Now;
            Console.WriteLine(after);
            long ticks = after.Ticks - now.Ticks;
            double seconds = TimeSpan.FromTicks(ticks).TotalSeconds;
            Console.WriteLine("Total runtime {0} seconds ", seconds);
            //option to print orders
            Console.WriteLine("PRESS ENTER to print ORDER information...");
            Console.ReadLine();
            printOrders();
            Console.WriteLine("PRESS ENTER AGAIN to print agency order times...");
            Console.ReadLine();
            printAgencyTimes();
            Console.ReadLine();
        }

        // Need an event handler to catch a buffer event for new order
        public void notifyHotelOfOrder(bool cellsOccupied)
        {
            String[] orderString = new String[1]; 
            //get an order from the buffer, and then release the semaphore
            orderString[0] = buffer.getOneCell();
            MultiCellBuffer._cells.Release();
            Decoder dec = new Decoder(orderString[0]);

            price_model.scalePrice(Convert.ToInt32(dec.getOrder().getPrice()));
            this.setOrder(orderString);
            
            // trace events caught
            Console.WriteLine("({0}) Received By Hotel Supplier", dec.getOrder().ToString());
            waitHandle[0].Set();
        }

        //Process a recieved order
        public void setOrder(String[] orderString)
        {

            // get encoder - did we want to use a singleton here?
            Decoder    d = new Decoder();
            OrderClass order;
            Thread[] thread = new Thread[orderString.Length];

            // decode all orders
            for (int i = 0; i < orderString.Length; ++i)
            {

            // set the order an decrypt
            d.setOrder(orderString[i]);
            
            // get the decrypeted order
            order = d.getOrder();

            // need to call OrderProcessing
            thread[i] = new Thread(new ParameterizedThreadStart(Worker));
            thread[i].Start(order);

            //Increment order counter
            counter++;

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
