using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NetDesign.Views
{
    public sealed partial class IEEEPage : Page, INotifyPropertyChanged
    {
        //生成一个模拟的以太网帧
        public static Ethernet ethernet = new Ethernet();
       
        public IEEEPage()
        {
            InitializeComponent();
            //设置用于输出的文本框的属性为只读
            result.IsReadOnly = true;
            FCS_TextBox.IsReadOnly = true;
            //当ShellPage.cs中定义的ieee中的Mac不为空时，将其值赋给当前页面的Mac文本框
            if (ShellPage.ieee.getSourceMac(0)!=null)
            {
                Source1_TextBox.Text = ShellPage.ieee.getSourceMac(0);
                Source2_TextBox.Text = ShellPage.ieee.getSourceMac(1);
                Source3_TextBox.Text = ShellPage.ieee.getSourceMac(2);
                Source4_TextBox.Text = ShellPage.ieee.getSourceMac(3);
                Source5_TextBox.Text = ShellPage.ieee.getSourceMac(4);
                Source6_TextBox.Text = ShellPage.ieee.getSourceMac(5);
            }
            if(ShellPage.ieee.getDesMac(0)!=null)
            {
                Target1_TextBox.Text = ShellPage.ieee.getDesMac(0);
                Target2_TextBox.Text = ShellPage.ieee.getDesMac(1);
                Target3_TextBox.Text = ShellPage.ieee.getDesMac(2);
                Target4_TextBox.Text = ShellPage.ieee.getDesMac(3);
                Target5_TextBox.Text = ShellPage.ieee.getDesMac(4);
                Target6_TextBox.Text = ShellPage.ieee.getDesMac(5);
            }
            //读取ShellPage.cs中定义的DataPart赋值给Data_TextBox
            Data_TextBox.Text = ShellPage.ieee.getDataPart();           
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

        //按下ok键时所执行的操作
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string s = "";
            //判断输入是否有错，如有错误，则将错误信息存储在字符串s中准备输出
            if (Source1_TextBox.Text.Length != 2 || Regex.IsMatch(Source1_TextBox.Text, "^[0-9A-Fa-f]+$") == false ||
                Source2_TextBox.Text.Length != 2 || Regex.IsMatch(Source2_TextBox.Text, "^[0-9A-Fa-f]+$") == false ||
                Source3_TextBox.Text.Length != 2 || Regex.IsMatch(Source3_TextBox.Text, "^[0-9A-Fa-f]+$") == false ||
                Source4_TextBox.Text.Length != 2 || Regex.IsMatch(Source4_TextBox.Text, "^[0-9A-Fa-f]+$") == false ||
                Source5_TextBox.Text.Length != 2 || Regex.IsMatch(Source5_TextBox.Text, "^[0-9A-Fa-f]+$") == false ||
                Source6_TextBox.Text.Length != 2 || Regex.IsMatch(Source6_TextBox.Text, "^[0-9A-Fa-f]+$") == false
                )
                s += "源MAC地址输入不合法，请重新输入（MAC地址用六组两位十六进制数表示）" + Environment.NewLine;
            if (
                    Target1_TextBox.Text.Length != 2 || Regex.IsMatch(Target1_TextBox.Text, "^[0-9A-Fa-f]+$") == false ||
                    Target2_TextBox.Text.Length != 2 || Regex.IsMatch(Target2_TextBox.Text, "^[0-9A-Fa-f]+$") == false ||
                    Target3_TextBox.Text.Length != 2 || Regex.IsMatch(Target3_TextBox.Text, "^[0-9A-Fa-f]+$") == false ||
                    Target4_TextBox.Text.Length != 2 || Regex.IsMatch(Target4_TextBox.Text, "^[0-9A-Fa-f]+$") == false ||
                    Target5_TextBox.Text.Length != 2 || Regex.IsMatch(Target5_TextBox.Text, "^[0-9A-Fa-f]+$") == false ||
                    Target6_TextBox.Text.Length != 2 || Regex.IsMatch(Target6_TextBox.Text, "^[0-9A-Fa-f]+$") == false
                   )
                s += "目的MAC地址输入不合法，请重新输入（MAC地址用六组两位十六进制数表示）" + Environment.NewLine;           
            if (Data_TextBox.Text.Length == 0 )
                s += "数据部分不合法，请重新输入（长度应大于0）";
            //将用于保存当前界面输入（只保存正确的信息）
            ShellPage.ieee.setDesMac(Target1_TextBox.Text, 0);
            ShellPage.ieee.setDesMac(Target2_TextBox.Text, 1);
            ShellPage.ieee.setDesMac(Target3_TextBox.Text, 2);
            ShellPage.ieee.setDesMac(Target4_TextBox.Text, 3);
            ShellPage.ieee.setDesMac(Target5_TextBox.Text, 4);
            ShellPage.ieee.setDesMac(Target6_TextBox.Text, 5);
            ShellPage.ieee.setSourceMac(Source1_TextBox.Text, 0);
            ShellPage.ieee.setSourceMac(Source2_TextBox.Text, 1);
            ShellPage.ieee.setSourceMac(Source3_TextBox.Text, 2);
            ShellPage.ieee.setSourceMac(Source4_TextBox.Text, 3);
            ShellPage.ieee.setSourceMac(Source5_TextBox.Text, 4);
            ShellPage.ieee.setSourceMac(Source6_TextBox.Text, 5);
            ShellPage.ieee.setDataPart(Data_TextBox.Text);
            //当字符串s为0（即没有发生错误）时，输出正确的结果
        
            App.conn.Insert(new Models.Record()
            {
                DesMac1 = ShellPage.ieee.getDesMac(0),
                DesMac2 = ShellPage.ieee.getDesMac(1),
                DesMac3 = ShellPage.ieee.getDesMac(2),
                DesMac4 = ShellPage.ieee.getDesMac(3),
                DesMac5 = ShellPage.ieee.getDesMac(4),
                DesMac6 = ShellPage.ieee.getDesMac(5),
                SourceMac1=ShellPage.ieee.getSourceMac(0),
                SourceMac2 = ShellPage.ieee.getSourceMac(1),
                SourceMac3 = ShellPage.ieee.getSourceMac(2),
                SourceMac4 = ShellPage.ieee.getSourceMac(3),
                SourceMac5 = ShellPage.ieee.getSourceMac(4),
                SourceMac6 = ShellPage.ieee.getSourceMac(5),
                DataPart=ShellPage.ieee.getDataPart()
            });
            if (s.Length == 0)
            {
                //设置模拟以太网帧内容
                ethernet.setDesMac(ShellPage.ieee.DesMac);
                ethernet.setSourceMac(ShellPage.ieee.SourceMac);
                ethernet.setData(ShellPage.ieee.getDataPart());
                ethernet.setFCS(ShellPage.ieee.getCheck());

                s = "前导码: " + ethernet.getLeadCode() + Environment.NewLine
                     + "帧前定界符: " + ethernet.getDelimiter() + Environment.NewLine
                     + "目的MAC地址: " + ethernet.getDesMac() + Environment.NewLine
                     + "源MAC地址:" + ethernet.getSourceMac() + Environment.NewLine
                     + "长度字段:" + ethernet.getDataLength() + Environment.NewLine
                     + "数据字段:" + ethernet.getDataPart() + Environment.NewLine
                     + "校验字段:" + ethernet.getFCS().PadLeft(32, '0');
                //FCS_TextBox.Text = ethernet.getFCS();
                string temp = ethernet.getFCS();
                int t= Convert.ToInt32(temp, 2);
              
                FCS_TextBox.Text = Convert.ToString(t, 16);
            }
            //输出结果，当存在错误时，输出错误信息，没有错误时输出模拟以太网帧的封装结果
            result.Text = s;
        }

        //按下reset按钮时执行的操作
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Source1_TextBox.Text = "";
            Source2_TextBox.Text = "";
            Source3_TextBox.Text = "";
            Source4_TextBox.Text = "";
            Source5_TextBox.Text = "";
            Source6_TextBox.Text = "";
            Target1_TextBox.Text = "";
            Target2_TextBox.Text = "";
            Target3_TextBox.Text = "";
            Target4_TextBox.Text = "";
            Target5_TextBox.Text = "";
            Target6_TextBox.Text = "";
            Data_TextBox.Text = "";
            FCS_TextBox.Text = "";
            result.Text = "";
        }
    }
}

