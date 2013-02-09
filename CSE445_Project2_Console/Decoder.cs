using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSE445_Project2_Console.EncryptionService;

namespace CSE445_Project2_Console
{
    class Decoder
    {
        /*
         * Decoder will first call ASU decryption service then convert the string back into the OrderObject.
         * Make a change.
         */
        private OrderClass s;
        public Decoder()
        {
            s = new OrderClass();
        }
        public Decoder(String str)
        {
            s = new OrderClass();
            setOrder(str);
        }
        public OrderClass getOrder()
        {
            return s;
        }
        public void setOrder(String str)
        {
            EncryptionService.ServiceClient serviceClient = new EncryptionService.ServiceClient();
            string temp = serviceClient.Decrypt(str); //Decrypts string
            /*
             * OrderClass.set() //All of OrderClass' attributes
             * OrderClass.set()....etc
             */
            char[] chStr = temp.ToCharArray();  //Creates an array of char from string
            StringBuilder temp2 = new StringBuilder();
            StringBuilder temp3 = new StringBuilder();
            StringBuilder temp4 = new StringBuilder();
            int j = 0;
            for (int i = 0; i < chStr.Length; ++i)
            {
                switch(j){
                    case 0:
                    {
                        while (chStr[i] != ' ')
                        {
                            temp2.Append(chStr[i]); //builds a string based on input up to first space
                            ++i;
                        }
                        break;
                    }
                    case 1:
                    {
                        while (chStr[i] != ' ')
                        {
                            temp3.Append(chStr[i]); //builds a string based on input up to second space
                            ++i;
                        }
                        break;
                    }
                    case 2:
                    {
                        while (chStr[i] != ' ')
                        {
                            temp4.Append(chStr[i]); //builds a string based on input up to third space
                            ++i;
                        }
                        break;
                    }
                }
                ++j;
            }

            int amt = Convert.ToInt32(temp2.ToString()); //Converts StringBuilder type to Int
            int cardNo = Convert.ToInt32(temp3.ToString());
            
            string senderID =  temp4.ToString();
            s.setID(senderID);
            
            s.setAmt(amt); //OrderClass object set() methods
            s.setCardNo(cardNo);
        }
    }
}
