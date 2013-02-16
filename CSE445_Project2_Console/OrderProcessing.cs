using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSE445_Project2_Console
{
    class OrderProcessing
    {
        /*
         * Whenever an order needs to be processed, a new thread is instantiated from this class (or method) to process the order.
         * It will check the validity of the credit card number.
         * You can define your credit card format, for example, the credit card number from the travel agencies must be a number registered to the HotelSupplier, or a number between two given numbers (e.g. 5000<x<7000).
         * Each OrderProcessing thread will calculate the total amount of charge, e.g., unitPrice*NoOfRooms + Tax + LocationCharge. 
         * It will send a confirmation to the travel agency when an order is completed.
         * You can implement the confirmation in different ways: you can use another buffer for the confirmation (with a buffer cell for each thread) so you don't have to consider the conflict among threads. 
         * However, you still need to coordinate the write and read between the producer and the consumer.
         */
        private OrderClass order;
 
        public delegate void orderConfirmationEvent(OrderClass confirmedOrder);
        public static event orderConfirmationEvent orderConfirmation;

        public OrderProcessing(OrderClass order)
        { 
        this.order = order;

            if (checkCardValidity() == true)
            {
                calculateTotal();
            }
            else
            {
                Console.WriteLine("Card Number {0} invalid ", order.getCardNo());
            }

        }

        private bool checkCardValidity()
        {

            int cardNo = order.getCardNo();

            if ((cardNo > 4999 && cardNo < 7001) ||
               ( HotelSupplier.checkCard(cardNo) == true ))
            {
                return true;
            }
            else 
            {
                return false;
            }
            
        }

        private void calculateTotal()
        { 
            
            double amount;
            double tax = 0.09 * (double)order.getnoRooms() * order.getPrice();
            double locationCharge = 19;

            amount = (double)order.getnoRooms() * order.getPrice() + tax + locationCharge;
            order.setAmt(amount);

            // inform agency here - what approach will we choose?
            orderConfirmation(order);


        }

        public OrderClass getOrder()
        {
            return order;
        }
    }
}
