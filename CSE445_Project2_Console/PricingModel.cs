using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSE445_Project2_Console
{
    class PricingModel
    {
        /* 
         * Decides price of rooms. It can increase price or cut the price.
         * You must define a mathematical model (formula) to determine the price based on the order received within a given time period and the number of rooms the HotelSupplier available in the same time period.
         * You can use a hard-coded table of the price in each week day, however you must make sure your model will allow the price to go up for some period of time and go down some other time.
         */
        private int[] priceTable;
        public PricingModel()
        {
            priceTable = new int[7];
            for (int i = 0; i < priceTable.Length; ++i)
            {
                priceTable[i] = (i + (i * 3 - 2)) + 107;
            }
        }
        public int scalePrice(int num)
        {
            return num + 50;
        }

        /*
         * Scale price based on two things:
         * 1) Get time of last order and determine if the price needs to go up based on how close in time it was
         * 2) Based on how many hotels are available.
         */
    }
}
