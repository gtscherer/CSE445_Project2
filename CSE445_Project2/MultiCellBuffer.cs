using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSE445_Project2
{
    class MultiCellBuffer
    {
        /*
         * This class is used for communication between travel agencies (clients) and the HotelSupplier (server): 
         *      this class has n data sells (n=3 for group project).
         * The number of cells available must be less than the max number of travel agencies in your experiment. 
         * A setOneCell and getOneCell methods can be defined to write data into and read data from one of the available cells.
         * You can use a semaphore of value n to manage the cells.
         * You cannot use a queue for the buffer, which is a different data structure. 
         * Note: the semaphore will allow an agency to gain the right to write into the buffer.
         * But, the HotelSupplier can still read at the same time. Synchronization is also required.
         * 
         * thanks for adding the comments!
         * 
         */
    }
}
