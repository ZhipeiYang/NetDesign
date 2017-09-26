using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace NetDesign.Views
{
    public sealed partial class CRCPage : Page, INotifyPropertyChanged
    {
        public static List<string> Show_List = new List<string>();
        public static int counter = 0;
        public CRCPage()
        {
            InitializeComponent();
            //设置用于输出的文本框的属性为只读，并读取由在ShellPage.cs中定义的ieee对象的需要校验的部分的值，填充到Data_TextBox中
            Show_TextBox.IsReadOnly = true;
            Result_TextBox.IsReadOnly = true;
            Check_TextBox.Text = ShellPage.ieee.getCheck();
            Data_TextBox.Text = IEEEPage.ethernet.getDesMac()
                             + IEEEPage.ethernet.getSourceMac()
                             + IEEEPage.ethernet.getDataLength()
                             + IEEEPage.ethernet.getDataPart();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //重置按钮
        private void Reset_Button_Click(object sender, RoutedEventArgs e)
        {
            Data_TextBox.Text = "";
            Show_TextBox.Text = "";
            Result_TextBox.Text = "";
            Check_TextBox.Text = ShellPage.ieee.getCheck();
            counter = 0;
            Show_List.Clear();
        }

        //CRC算法，参数为校验方程（check），数据部分（data）以及用于保存中间结果的List show（引用传参），返回值为计算所得的fcs
        public static string CRC(string check, string data,ref List<string> show)
        {
            string data2 = data + "00000000000000000000000000000000";
            char[] c = check.ToArray();
            char[] d = data2.ToArray();
            d = trim0(d);
            string t = "";
            int j = 0;
            show.Clear();
            show.Add(new string(d));
            if ( Regex.IsMatch(data2, "^[0-1]+$") == false)
            {
                show.Clear();
                return "input error";
            }
            else if(Regex.IsMatch(check, "^[0-1]+$") == false )
            {
                show.Clear();
                return "check error";
            }
            else
            {
                while (d.Length >= c.Length)
                {
                    j++;
                    for (int i = 0; i < c.Length; i++)
                    {
                        if (d[i] == c[i])
                            d[i] = '0';
                        else
                            d[i] = '1';
                    }
                    d = trim0(d);
                    show.Add( new string(d));
                }
                t = new string(d);
                return t;
            }
        }
        //CRC方法的辅助函数，用于清除左侧的‘0’
        public static char[] trim0(char[] c)
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
        //按下OK键所执行的操作，若数据部分输入合法，则输出CRC结果，否则输出错误信息
        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(Data_TextBox.Text, "^[0-1]+$") == false || Data_TextBox.Text.Length == 0)
                Result_TextBox.Text = "数据部分不合法，请重新输入（长度应大于0,并且为01序列）";
            else
                Result_TextBox.Text = CRC(Check_TextBox.Text,Data_TextBox.Text , ref Show_List);
        }
        //逐步演示，依赖于已定义的全局变量Show_List和counter，演示完成后清除Show_List
        private void Next_Button_Click(object sender, RoutedEventArgs e)
        {
            if (counter < Show_List.Count()-1&&Show_List[counter+1].Length>33)
            {
                Show_TextBox.Text = Show_List[counter].Substring(0,33)+"..." + Environment.NewLine
                    + Check_TextBox.Text + Environment.NewLine
                    + "-".PadLeft(45, '-') + Environment.NewLine
                    + Show_List[counter + 1].PadLeft(Show_List[counter].Length, '0').Substring(0,33)+"...";
                counter++;
            }
            else if(counter < Show_List.Count() - 1 && Show_List[counter + 1].Length <= 33)
            {
                Show_TextBox.Text = Show_List[counter] + Environment.NewLine
                    + Check_TextBox.Text + Environment.NewLine
                    + "-".PadLeft(Show_List[counter].Length + 3, '-') + Environment.NewLine
                    + Show_List[counter + 1].PadLeft(Show_List[counter].Length, '0');
                counter++;
            }
            else if (counter == Show_List.Count()-1)
            {
                Show_TextBox.Text = Show_TextBox.Text + Environment.NewLine + "演示完成";
                Show_List.Clear();
            }
            
        }

    }
}
