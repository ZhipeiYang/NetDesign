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
        //����һ��ģ�����̫��֡
        public static Ethernet ethernet = new Ethernet();
       
        public IEEEPage()
        {
            InitializeComponent();
            //��������������ı��������Ϊֻ��
            result.IsReadOnly = true;
            FCS_TextBox.IsReadOnly = true;
            //��ShellPage.cs�ж����ieee�е�Mac��Ϊ��ʱ������ֵ������ǰҳ���Mac�ı���
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
            //��ȡShellPage.cs�ж����DataPart��ֵ��Data_TextBox
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

        //����ok��ʱ��ִ�еĲ���
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string s = "";
            //�ж������Ƿ��д����д����򽫴�����Ϣ�洢���ַ���s��׼�����
            if (Source1_TextBox.Text.Length != 2 || Regex.IsMatch(Source1_TextBox.Text, "^[0-9A-Fa-f]+$") == false ||
                Source2_TextBox.Text.Length != 2 || Regex.IsMatch(Source2_TextBox.Text, "^[0-9A-Fa-f]+$") == false ||
                Source3_TextBox.Text.Length != 2 || Regex.IsMatch(Source3_TextBox.Text, "^[0-9A-Fa-f]+$") == false ||
                Source4_TextBox.Text.Length != 2 || Regex.IsMatch(Source4_TextBox.Text, "^[0-9A-Fa-f]+$") == false ||
                Source5_TextBox.Text.Length != 2 || Regex.IsMatch(Source5_TextBox.Text, "^[0-9A-Fa-f]+$") == false ||
                Source6_TextBox.Text.Length != 2 || Regex.IsMatch(Source6_TextBox.Text, "^[0-9A-Fa-f]+$") == false
                )
                s += "ԴMAC��ַ���벻�Ϸ������������루MAC��ַ��������λʮ����������ʾ��" + Environment.NewLine;
            if (
                    Target1_TextBox.Text.Length != 2 || Regex.IsMatch(Target1_TextBox.Text, "^[0-9A-Fa-f]+$") == false ||
                    Target2_TextBox.Text.Length != 2 || Regex.IsMatch(Target2_TextBox.Text, "^[0-9A-Fa-f]+$") == false ||
                    Target3_TextBox.Text.Length != 2 || Regex.IsMatch(Target3_TextBox.Text, "^[0-9A-Fa-f]+$") == false ||
                    Target4_TextBox.Text.Length != 2 || Regex.IsMatch(Target4_TextBox.Text, "^[0-9A-Fa-f]+$") == false ||
                    Target5_TextBox.Text.Length != 2 || Regex.IsMatch(Target5_TextBox.Text, "^[0-9A-Fa-f]+$") == false ||
                    Target6_TextBox.Text.Length != 2 || Regex.IsMatch(Target6_TextBox.Text, "^[0-9A-Fa-f]+$") == false
                   )
                s += "Ŀ��MAC��ַ���벻�Ϸ������������루MAC��ַ��������λʮ����������ʾ��" + Environment.NewLine;           
            if (Data_TextBox.Text.Length == 0 )
                s += "���ݲ��ֲ��Ϸ������������루����Ӧ����0��";
            //�����ڱ��浱ǰ�������루ֻ������ȷ����Ϣ��
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
            //���ַ���sΪ0����û�з�������ʱ�������ȷ�Ľ��
        
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
                //����ģ����̫��֡����
                ethernet.setDesMac(ShellPage.ieee.DesMac);
                ethernet.setSourceMac(ShellPage.ieee.SourceMac);
                ethernet.setData(ShellPage.ieee.getDataPart());
                ethernet.setFCS(ShellPage.ieee.getCheck());

                s = "ǰ����: " + ethernet.getLeadCode() + Environment.NewLine
                     + "֡ǰ�����: " + ethernet.getDelimiter() + Environment.NewLine
                     + "Ŀ��MAC��ַ: " + ethernet.getDesMac() + Environment.NewLine
                     + "ԴMAC��ַ:" + ethernet.getSourceMac() + Environment.NewLine
                     + "�����ֶ�:" + ethernet.getDataLength() + Environment.NewLine
                     + "�����ֶ�:" + ethernet.getDataPart() + Environment.NewLine
                     + "У���ֶ�:" + ethernet.getFCS().PadLeft(32, '0');
                //FCS_TextBox.Text = ethernet.getFCS();
                string temp = ethernet.getFCS();
                int t= Convert.ToInt32(temp, 2);
              
                FCS_TextBox.Text = Convert.ToString(t, 16);
            }
            //�������������ڴ���ʱ�����������Ϣ��û�д���ʱ���ģ����̫��֡�ķ�װ���
            result.Text = s;
        }

        //����reset��ťʱִ�еĲ���
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

