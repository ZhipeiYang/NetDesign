using System.Text.RegularExpressions;

namespace NetDesign
{
    //用于界面缓存和页面之间的值传递，set函数均包含正确性检验
   public class View_IEEE
    {
        public string[] SourceMac;
        public string[] DesMac;
        private string DataPart;
        private string Check;
        public View_IEEE()
        {
            SourceMac = new string[6];
            DesMac = new string[6];
            DataPart = "";

           
            Check = "100000100110000010001110110110111";
        }
        public bool setSourceMac(string t,int i)
        {
            if(i>=0&&i<6&&t.Length==2&& Regex.IsMatch(t, "^[0-9A-Fa-f]+$")==true)
            {
                SourceMac[i] = t;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool setDesMac(string t, int i)
        {
            if (i >= 0 && i < 6 && t.Length == 2 && Regex.IsMatch(t, "^[0-9A-Fa-f]+$") == true)
            {
                DesMac[i] = t;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool setDataPart(string t)
        {
            if(t.Length!=0)
            {
                DataPart = t;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool setCheck(string t)
        {
            if (Regex.IsMatch(t, "^[0-1]+$") == true)
            {
                Check = t;
                return true;
            }
            else
            {
                return false;
            }
        }
        public string getSourceMac(int i)
        {
            if(i>=0&&i<6)
            {
                return SourceMac[i];
            }
            else
            {
                return "error";
            }
        }
        public string getDesMac(int i)
        {
            if (i >= 0 && i < 6)
            {
                return DesMac[i];
            }
            else
            {
                return "error";
            }
        }
        public string getDataPart()
        {
            return DataPart;
        }
        public string getCheck()
        {
            return Check;
        }
    }
}
