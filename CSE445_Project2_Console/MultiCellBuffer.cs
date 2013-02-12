using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CSE445_Project2_Console
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
         * thanks
         * 
         * 
         */

        /*
         * Starting point for the multicell buffer.
         * Might need changes when we integrate to meet the synchronization requirement
         * Cycle method for the getOneCell should prevent any threads from starving
         * Needs testing once we have a travel agency class put together
         */

        static int numberOfCells = 5;

        //Semaphore allows travel agency to get access, hotel supplier will release when order is processed
        public static Semaphore _cells = new Semaphore(numberOfCells , numberOfCells);


        static string[] cells = new string[numberOfCells];
        static int counter = 0;

        public void setOneCell(String order)
        {           
                bool success = false;
                
                //Cycle through the cells
                for (int i = 0; i < cells.Length && !success; ++i)
                {
                    //Find the first empty cell
                    if (cells[i] == null)
                    {
                        //Fill it with the order
                        cells[i] = order;
                        success = true;
                    }
                }
            
                //Should only happen if we fail to use the semaphore
                if (!success)
                {
                    Console.WriteLine("Cells are full, Could not write to buffer");
                }            
        }

        //Test to make certain the cells aren't empty
        public bool cellsEmpty()
        {
            bool empty = true;
            for (int i = 0; i < cells.Length; ++i)
            {
                if (cells[i] != null)
                    empty = false;
            }
            return empty;
        }

        //Grabs the contents of one cell, ensuring to cycle through all the cells
        public string getOneCell()
        {
            string order = null;
            //Ensure there's an order to get
            if (!cellsEmpty())
            {

                //cycle until we find an occupied cell
                while (cells[counter] == null)
                {
                    counter = (counter + 1) % cells.Length;
                }

                //get the cell contents
                order = cells[counter];

                //Delete the cell contents
                cells[counter] = null;

                //Move the counter to the next slot
                counter = (counter + 1) % cells.Length;

            }

            //Return the found order
            return order;
        }

    }
}
