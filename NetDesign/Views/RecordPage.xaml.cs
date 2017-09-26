using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace NetDesign.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class RecordPage : Page
    {
        //初始化显示记录 
        List<Models.Record> list = App.conn.Table<Models.Record>().ToList();
        ObservableCollection<Models.Record> query = new ObservableCollection<Models.Record>();
        
        public RecordPage()
        {
            this.InitializeComponent();
            query.Clear();
            foreach(var t in list)
            {
                query.Add(t);
            }
        }
        //清除所有记录按钮反应代码
        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            App.conn.DeleteAll<Models.Record>();
            query.Clear();

        }
        //点击记录时做出的反应（将选中的记录内容填充到IEEE_Page的相应文本框内）
        private void rec_ItemClick(object sender, ItemClickEventArgs e)
        {
            var record = (Models.Record)e.ClickedItem;
            ShellPage.ieee.setDesMac(record.DesMac1, 0);
            ShellPage.ieee.setDesMac(record.DesMac2, 1);
            ShellPage.ieee.setDesMac(record.DesMac3, 2);
            ShellPage.ieee.setDesMac(record.DesMac4, 3);
            ShellPage.ieee.setDesMac(record.DesMac5, 4);
            ShellPage.ieee.setDesMac(record.DesMac6, 5);

            ShellPage.ieee.setSourceMac(record.SourceMac1, 0);
            ShellPage.ieee.setSourceMac(record.SourceMac2, 1);
            ShellPage.ieee.setSourceMac(record.SourceMac3, 2);
            ShellPage.ieee.setSourceMac(record.SourceMac4, 3);
            ShellPage.ieee.setSourceMac(record.SourceMac5, 4);
            ShellPage.ieee.setSourceMac(record.SourceMac6, 5);

            ShellPage.ieee.setDataPart(record.DataPart);


        }
    }
}
