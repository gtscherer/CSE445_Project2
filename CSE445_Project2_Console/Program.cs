using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CSE445_Project2_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 
             * Start Hotel Supplier Threads
             * Create buffer classes
             * Instantiate objects
             * Create Threads
             * Start Threads
             */

            MultiCellBuffer buffer = new MultiCellBuffer();

            //Get number of travel agencies from the user
            Console.WriteLine("Program started.");
            Console.WriteLine("Please enter the number of threads you would like to test:");
            int N = 0;
            while (N <= 0)
            {
                try         
                {
                    N = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException a)
                {
                    Console.WriteLine("Try entering a different number. Needs to be an integer.");
                }
            }
            //Instantiate our hotel supplier
            HotelSupplier hotel = new HotelSupplier(buffer, N);

            //Build our travel agencies         
            const int idNum = 100;
            TravelAgency[] obitz = new TravelAgency[N];
            Thread[] agencies = new Thread[N];

            //Instantiate our travel agencies, attach the event handlers and start the threads
            for(int i = 0; i < N; ++i)
            {
                obitz[i] = new TravelAgency();
                obitz[i].setId(idNum + i);
                HotelSupplier.priceCut += new HotelSupplier.priceCutEvent(obitz[i].priceCutEvent);
                HotelSupplier.printAgencyTimes += new HotelSupplier.printTimesEvent(obitz[i].printTimes);
                HotelSupplier.printOrders += new HotelSupplier.printOrdersEvent(obitz[i].printOrders);
                agencies[i] = new Thread(new ThreadStart(obitz[i].agencyFunc));
                OrderProcessing.orderConfirmation += new OrderProcessing.orderConfirmationEvent(obitz[i].orderConfirmationEvent);
                agencies[i].Name = (i + 1).ToString();
                agencies[i].Start();
            }

            //Begin the Hotel Supplier thread which will control our main program flow
            Thread hotelThread = new Thread(new ThreadStart(hotel.hotelStarter));
            hotelThread.Start();
        }

        //Test method to verify encode/decode service with orderclass
        static Boolean testOrder()
        {
            OrderClass order = new OrderClass();
            order.setAmt(123);
            order.setID("test");
            order.setCardNo(456418);

            Encoder enc = new Encoder(order);
            String encoded = enc.getOrder();
            Decoder dec = new Decoder(encoded);

            return order == dec.getOrder();

        }
    }
}
