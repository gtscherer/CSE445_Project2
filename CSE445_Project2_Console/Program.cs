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
            const int N = 3;
            HotelSupplier hotel = new HotelSupplier();
            /*
            Thread hotelThread = new Thread(new ThreadStart(hotel.hotelFunc));
            hotelThread.Start();
            TravelAgency orbitz = new TravelAgency();
            HotelSupplier.priceCut += new priceCutEvent(orbitz.roomSale);
            Thread[] agencies = new Thread[N];
            for(int i = 0; i < 3; i++)
            {
                agencies[i] = new Thread(new ThreadStart(orbitz.agencyFunc))
                agencies[i].Name = (i + 1).ToString();
                agencies[i].Start();
            }
            */
        }
    }
}
