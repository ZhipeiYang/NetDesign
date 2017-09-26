using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDesign.Models
{
    public class Record
    {
        
        [AutoIncrement,PrimaryKey]
        public int Id { get; set; }
       
        public string DesMac1 { get; set; }
        public string DesMac2 { get; set; }
        public string DesMac3 { get; set; }
        public string DesMac4 { get; set; }
        public string DesMac5 { get; set; }
        public string DesMac6 { get; set; }

        public string SourceMac1 { get; set; }
        public string SourceMac2 { get; set; }
        public string SourceMac3 { get; set; }
        public string SourceMac4 { get; set; }
        public string SourceMac5 { get; set; }
        public string SourceMac6 { get; set; }

        public string DataPart { get; set; }


    }
}
