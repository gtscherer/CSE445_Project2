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
            setOrder(s);
        }
        public String getOrder(){
            return order;   
        }
        public void setOrder(OrderClass s)
        {
            StringBuilder temp = new StringBuilder();
            temp.Append(s.getAmt().ToString());
            temp.Append(" ");
            temp.Append(s.getCardNo().ToString());
            temp.Append(" ");
            /* This is commented out because I haven't determined what senderID's type is. 
             temp.Append(s.getID().toString()
             temp.Append(" ");
             */
            order = temp.ToString();
            EncryptionService.ServiceClient serviceClient = new EncryptionService.ServiceClient();
            order = serviceClient.Encrypt(order);
        }
    }

}
