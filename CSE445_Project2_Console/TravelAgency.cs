using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CSE445_Project2_Console
{
    class TravelAgency
    {
        /*
         * (Instantiated from other Classes)Each travel agency is a thread instantiated from the same class (or the same method) in a class.
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

        //thread id
        private int senderId;
        //actual roomDemand
        private int roomDemand;
        //room demand variable used for scaling
        private double tempDemand;
        //int to keep track of order number
        private int orderNumber;
        //keep track of start times
        private int startNumber;


        //to generate random demand to scale with price
        private Random randDemand;
        //keep track of start and finish times for each order up to 10
        private DateTime[] orderStart;
        private DateTime[] orderFinish;
        //keep track of queued orders, send orders, and confirmed orders
        private List<OrderClass> queuedOrders;
        private List<OrderClass> sentOrders;
        private List<OrderClass> confirmedOrders;
        //currentOrder object to be used for constructing orders to add to the lists
        private OrderClass currentOrder;
        //encoder to encode each order
        private Encoder orderEncoder;
        private MultiCellBuffer mcb;


        public TravelAgency()
        {
            //instantiating necessary objects
            randDemand = new Random();
            queuedOrders = new List<OrderClass>();
            sentOrders = new List<OrderClass>();
            confirmedOrders = new List<OrderClass>();
            currentOrder = new OrderClass();
            orderEncoder = new Encoder();
            orderStart = new DateTime[10];
            orderFinish = new DateTime[10];
            orderNumber = 0;
            startNumber = 0;
            roomDemand = randDemand.Next(50, 100);
            mcb = new MultiCellBuffer();
        }

        //public mehtod to set ID
        public void setId(int a)
        {
            senderId = a;
        }

        //This is the actual thread where it attempts to add orders tot he buffer
        public void agencyFunc()
        {
            Console.WriteLine("Agency function thread created, my ID is: {0}", senderId);

            //using a while(true) because to continuously poll the queued orders so
            //whenever one is added it attempts to add it to the buffer
            while (true)
            {
                //demand always changing
                roomDemand = randDemand.Next(50, 100);

                //commented out sleep because all threads were syncing up and generating the same random demand
                //Thread.Sleep(50);

                

                //Console.WriteLine("test");
                //check to see if any orders have been added to the queue
                if (queuedOrders.Count != 0 && startNumber < 10)
                {
                    //encode the first order in line
                    orderEncoder.setOrder(queuedOrders.First());
                    Console.WriteLine("Sending ({0}) To Hotel Supplier", queuedOrders.First().ToString());
                    //move the order from queued to sent
                    sentOrders.Add(queuedOrders.First());
                    queuedOrders.RemoveAt(0);

                    // this is where i need the multicelled buffer to add the order to the buffer 
                    MultiCellBuffer._cells.WaitOne();
                    orderStart[startNumber] = DateTime.Now;
                    mcb.setOneCell(orderEncoder.getOrder());

                    //start time stamp for order
                    

                    //Console.WriteLine("Order Sent! Thread: {0}, Rooms: {1}, Card: {2}", queuedOrders.First().getID(), queuedOrders.First().getnoRooms(), queuedOrders.First().getCardNo());


                    
                    //increment orderNumber for startTime of next order
                    startNumber++;

                }
            }
        }


        //this class is an event listener that creates a new order and
        //adds it to the list of queued orders to be added to the buffer in the agencyFunc
        //THIS CALLBACK EVENT HANDLER NEEDS TO CHANGE SO I RECEIVE PREVIOUS PRICE AND CURRENT
        // Joern on 1/15/13 - previous price added
        public void priceCutEvent(int newPrice, int prevPrice)
        {
            

            if (orderNumber < 10)
            {
                //generate random room demand
                roomDemand = randDemand.Next(50, 100);


                //generate scaled demand for first order without previous price
                //assuming its a price drop of 10%           
                   
                tempDemand = ((double)prevPrice / (double)newPrice) * roomDemand;                    
                roomDemand = (int)tempDemand;
                    
                




                //create new order;
                currentOrder.setnoRooms(roomDemand);
                currentOrder.setID(senderId.ToString());
                currentOrder.setCardNo(senderId + 5000);
                currentOrder.setPrice((double)newPrice);
                currentOrder.setOrderID(senderId + "-" + orderNumber);
                orderNumber++;

                //add new order to pending list
                queuedOrders.Add(currentOrder);

                //reset current order
                currentOrder = new OrderClass();

                //Console.WriteLine("Received : {0}", newPrice);
                //Console.WriteLine("Sender id = {0}", senderId);
            }


        }

        //Event handler for order confirmation
        public void orderConfirmationEvent(OrderClass confirmedOrder)
        {

            

            //only attempt to move order from sent to confirmed after making sure
            //the order sender id matches the thread sender id
            //otherwise it is someonelse's order
            if (confirmedOrder.getID() == senderId.ToString())
            {
                
               
                //search for the confirmed order
                for (int s = 0; s < sentOrders.Count; s++)
                {

                    if (sentOrders.ElementAt(s).getOrderId() == confirmedOrder.getOrderId())
                    {
                        Console.WriteLine("Order ({0}) Confirmation Received", confirmedOrder.ToString());
                        orderFinish[s] = DateTime.Now;
                        
                        //move from sent orders to confirmed orders
                        confirmedOrders.Add(confirmedOrder);
                        // trace events caught

                        //sentOrders.RemoveAt(s); i will not remove the confirmed order from s in order to
                        //maintain the correct order in which the orders were submitted so i can use
                        //that index for the finish time stamp
                        //TimeStamp for end of order s
                    }

                }
            }

        }

        public void printTimes()
        {
            //Thread.SpinWait(2000);
            Console.WriteLine("\nThread Number: {0}", senderId);
            for (int i = 0; i < confirmedOrders.Count; i++)
            {
                long ticks = orderFinish[i].Ticks - orderStart[i].Ticks;
                double seconds = TimeSpan.FromTicks(ticks).TotalSeconds;
                Console.WriteLine("Order {0}:       Total Time: {1} Seconds", confirmedOrders[i].getOrderId(), seconds);
            }
            long ticks2;
            ticks2 = orderFinish[confirmedOrders.Count - 0].Ticks - orderStart[0].Ticks;
            double seconds2 = TimeSpan.FromTicks(ticks2).TotalSeconds;
            Console.WriteLine("Total time from submission of first order to last confirmation: {0}", seconds2);

        }

        public void printOrders()
        {
            
            Console.WriteLine("\nTHREAD NUMBER: {0}", senderId);
            Console.WriteLine("\nQueued Orders:");
            if (queuedOrders.Count == 0)
            {
                Console.WriteLine("No Queued Orders...");
            }
            else
            {
                for (int i = 0; i < queuedOrders.Count; i++)
                {
                    Console.WriteLine("{0}", queuedOrders[i].ToString2());
                }
            }

            Console.WriteLine("\nSent Orders:");
            if (sentOrders.Count == 0)
            {
                Console.WriteLine("No Sent Orders...");
            }
            else
            {
                for (int i = 0; i < sentOrders.Count; i++)
                {
                    Console.WriteLine("{0}", sentOrders[i].ToString2());
                }
            }

            Console.WriteLine("\nConfirmed Orders:");
            if (confirmedOrders.Count == 0)
            {
                Console.WriteLine("No Confirmed Orders...");
            }
            else
            {
                for (int i = 0; i < confirmedOrders.Count; i++)
                {
                    Console.WriteLine("{0}", confirmedOrders[i].ToString2());
                }
            }
            

        }
    }
}