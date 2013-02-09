using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSE445_Project2
{
    class TravelAgency
    {
        /*
         * Each travel agency is a thread instantiated from the same class (or the same method) in a class.
         * The TravelAgency's actions are event driven.
         * Each travel agency contains a call-back method (event handler) for the HotelSupplier to call when a price-cut event occurs.
         * The travel agency will calculate the number of rooms to order, for example, based on the need and the difference between the previous price and the current price.
         * The thread will terminate after the HotelSupplier thread has terminated.
         * Each order is an OrderClass object. 
         * The object is sent to the Encoder for encoding. 
         * The encoded string is sent back to the travel agency.
         * Then, the travel agency will send the order in String format to the MultiCellBuffer.
         * Before sending the order to the MultiCellBuffer, a time stamp must be saved.
         * When the confirmation of order completion is received, the time of the order will be calculated and saved (or printed).
         */
    }
}
