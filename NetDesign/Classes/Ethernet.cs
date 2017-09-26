using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NetDesign.Views
{
    //模拟以太网帧
     public  class Ethernet 
    {
        private string LeadCode;
        private string Delimiter;
        private string SourceMac;
        private string DesMac;
        private string DataPart;
        private string DataLength;
        private string FCS;
        public Ethernet()
        {
            LeadCode = "10101010101010101010101010101010101010101010101010101010";
            Delimiter = "10101011";
            SourceMac = "";
            DesMac = "";
            DataPart = "";
            DataLength = "";
            FCS = "";
        }
        //检查输入正确性并根据传入的一组长度为6的2位十六进制表示的Mac计算用二进制表示的Mac并返回
        public bool setSourceMac(string[] t)
        {
            SourceMac = "";
           if (t.Length != 6)
                return false;
           for(int i=0;i<6;i++)
            {
                SourceMac += Hex2Bit(t[i]);
            }
            return true;
        }
        public bool setDesMac(string[] t)
        {
            DesMac = "";
            if (t.Length != 6) return false;
            for (int i = 0; i < 6; i++)
            {
                DesMac +=  Hex2Bit(t[i]);
            }
            return true;
        }
       //检查输入正确性并设置DataPart和DataLength字段
        public bool setData(string t)
        {
            
            if (t.Length != 0)
            {
                string temp="";
                DataPart = "";
                int length = 0;
                byte[] by = Encoding.ASCII.GetBytes(t); 
                for (int i = 0; i < by.Length; i++)
                {
                    temp = Convert.ToString(by[i], 2);
                    length = temp.Length;
                    for (int j = 0; j < 8 - length; j++)
                    {
                        temp = "0" + temp;
                    }
                    DataPart =DataPart+ temp;              
                }
                DataPart=DataPart.PadRight(46 * 8, '0');
                DataLength = (Convert.ToString((DataPart.Length / 8) + 6 + 6 + 2 + 4, 2)).PadLeft(16, '0');
                return true;
            }
                return false;
        }
        //根据传入的校验方程t，利用DataPart属性进行CRC校验，设置FCS字段
        public bool setFCS(string t)
        {
            if (Regex.IsMatch(t, "^[0-1]+$") == false)
                return false;
           /*
            else if (DataPart == "0110100001100101011011000110110001101111001000000111011101101111011100100110110001100100")
                FCS = "‭10100100010010000100111011111111‬";
           */
            else
            FCS = CRC(t, DesMac + SourceMac + DataLength + DataPart);
            return true;
        }

        //获取各个字段并返回
        public string getLeadCode()
        {
            return LeadCode;
        }
        public string getDelimiter()
        {
            return Delimiter;
        }
        public string getSourceMac()
        {
            return SourceMac;
        }
        public string getDesMac()
        {
            return DesMac;
        }
        public string getDataPart()
        {
            return DataPart;
        }
        public string getDataLength()
        {
            return DataLength;
        }
        public string getFCS()
        {
            return FCS;
        }

        public string CRC(string check, string data)
        {
            
             string data2= data+ "00000000000000000000000000000000";
             char[] c = check.ToArray();
             char[] d = data2.ToArray(); 
             d = trim0(d); 
             if (Regex.IsMatch(data2, "^[0-1]+$") == false || Regex.IsMatch(check, "^[0-1]+$") == false)
                 return "error";
             while (d.Length >= c.Length)
             {
                 for (int i = 0; i < c.Length; i++)
                 { 
                         if (d[i] == c[i])
                             d[i] = '0';
                         else
                             d[i] = '1';
                 }
                 d = trim0(d);
             }
             return new string(d);
             
          
        }
        public char[] trim0(char[] c)
        {
            int i = -1;

            while (i < c.Length - 1 && c[i + 1] == '0')
            {
                i++;
            }
            char[] temp = new char[c.Length - i - 1];
            if (i == -1) return c;
            else
            {
                for (int j = 0; j + i + 1 < c.Length; j++)
                {
                    temp[j] = c[j + i + 1];
                }
                return temp;
            }

        }
        public int Hex2Dec(char h)
        {
            switch (h)
            {
                case '0': return 0;
                case '1': return 1;
                case '2': return 2;
                case '3': return 3;
                case '4': return 4;
                case '5': return 5;
                case '6': return 6;
                case '7': return 7;
                case '8': return 8;
                case '9': return 9;
                case 'A': return 10;
                case 'a': return 10;
                case 'B': return 11;
                case 'b': return 11;
                case 'C': return 12;
                case 'c': return 12;
                case 'D': return 13;
                case 'd': return 13;
                case 'E': return 14;
                case 'e': return 14;
                case 'F': return 15;
                case 'f': return 15;
                default: return -1;
            }
        }
        public string Hex2Bit(string hex)
        {
            string bit = "";
            char[] h = hex.ToArray();
            int num = 0;
            if (h.Length == 2)
                if (Hex2Dec(h[0]) != -1 && Hex2Dec(h[1]) != -1)
                {
                    num += Hex2Dec(h[1]) + Hex2Dec(h[0]) * 16;
                    bit = Convert.ToString(num, 2);
                    bit = bit.PadLeft(8, '0');
                }
                else
                {
                    bit = "error";
                }
            return bit;
        }

    }
}
