using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
