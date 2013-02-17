using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSE445_Project2_Console.EncryptionService;

namespace CSE445_Project2_Console
{
    class Encoder
    {
        /*
         * The encoder class will convert an OrderObject into a string.
         * You can choose any way to encode the values into a string, as long as you can decode the string to the original order object.
         * You can use a class or method to implement the Encoder. 
         * For the group project, after data format conversion, the encoder calls encryption service in ASU repository to encrypt the string. If you use the .svc service, you should use "Add Service Reference" and if you use the .asmx service, you should use "Add Web Reference." (.svc is recommended).
         */
        private String order;
        public Encoder()
        {
            order = "";
        }
        public Encoder(OrderClass s)
        {
            setOrder(s);  //constructs an encoder with a class
        }
        public String getOrder(){
            return order;   //returns encrypted string
        }
        public void setOrder(OrderClass s)
        {
            StringBuilder temp = new StringBuilder();
            temp.Append(s.getAmt().ToString());  //Appends amount to string
            temp.Append(" ");
            temp.Append(s.getCardNo().ToString()); //Appends card number to string
            temp.Append(" ");
            temp.Append(s.getID().ToString()); //Appends ID to string
            temp.Append(" ");
            temp.Append(s.getOrderId().ToString()); //Appends orderId to string
            temp.Append(" ");
            temp.Append(s.getnoRooms().ToString()); //Appends number of rooms to string
            temp.Append(" ");
            temp.Append(s.getPrice().ToString());  //Appends price per room to string
            temp.Append(" ");
            
             
            order = temp.ToString(); //builds string
            EncryptionService.ServiceClient serviceClient = new EncryptionService.ServiceClient(); //Instantiates encryption service
            order = serviceClient.Encrypt(order);  //encrypts string
        }
    }

}
