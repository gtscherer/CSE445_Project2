using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSE445_Project2_Console
{
    class OrderClass
    {
        /*
         * Contains at least the following private data members and public methods:
         * senderId: the identity of the sender, you can use thread name or thread id.
         * cardNo: an integer that represents a credit card number.
         * amount: an integer that represents the number of rooms to order.
         * setID and getID: methods allow the users to write and read senderId member.
         * setCardNo and getCardNo: methods allow the users to write and read cardNo member.
         * setAmt and getAmt: methods allow the users to write and read Amount member.
         * 
         * You must decide if these methods need to be synchronized. 
         * The instances creaded from this class are of the OrderObject.
         */
        private int cardNo;
        private double amount;
        private int noRooms;
        private double price;
        private string senderId;

        public string getID(){
            return senderId;            //Possibly needs synchronization
        }
        
        public void setID(string a){
            senderId = a;            //Possibly needs synchronization
        }
        public void setCardNo(int cardNumber)
        {
            cardNo = cardNumber;           //Possibly needs synchronization
        }
        public int getCardNo()
        {
            return cardNo;           //Possibly needs synchronization
        }
        public void setnoRooms(int noRooms)
        {
            this.noRooms = noRooms;           //Possibly needs synchronization
        }
        public int getnoRooms()
        {
            return noRooms;           //Possibly needs synchronization
        }
        public void setPrice(double price)
        {
            this.price = price;           //Possibly needs synchronization
        }
        public double getPrice()
        {
            return price;           //Possibly needs synchronization
        }
        public void setAmt(double amt)
        {
            amount = amt;           //Possibly needs synchronization
        }
        public double getAmt()
        {
            return amount;           //Possibly needs synchronization
        }

        public override string ToString()
        {
            return "SenderId: " + getID() + " Card Number: " + getCardNo() + " Amount: " + getAmt();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            OrderClass o = obj as OrderClass;

            if ((System.Object)o == null)
                return false;


            return (amount == o.getAmt()) && (cardNo == o.getCardNo()) && senderId.Equals(o.getID());
        }

        public static bool operator ==(OrderClass a, OrderClass b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return (a.getAmt() == b.getAmt()) && (a.getCardNo() == b.getCardNo()) && a.getID().Equals(b.getID());
        }

        public static bool operator !=(OrderClass a, OrderClass b)
        {
            return !(a == b);
        }


    }
}
