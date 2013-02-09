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
        private int amount;
        //private (thread name type or thread id type or string/int) senderId;
        /*
        This is commented out because I haven't determined what senderID's type is. 
        public (thread-type) getID(){
            return senderID;            //Possibly needs synchronization
        }
        
        public void setID((thread-type) a){
            senderID = a;            //Possibly needs synchronization
        }
        
        */
        public void setCardNo(int cardNumber)
        {
            cardNo = cardNumber;           //Possibly needs synchronization
        }
        public int getCardNo()
        {
            return cardNo;           //Possibly needs synchronization
        }
        public void setAmt(int amt)
        {
            amount = amt;           //Possibly needs synchronization
        }
        public int getAmt()
        {
            return amount;           //Possibly needs synchronization
        }
    }
}
