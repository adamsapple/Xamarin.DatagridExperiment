using System;
using System.Collections.Generic;
using System.Text;

namespace ListViewTest01
{
    public class ListViewItem
    {
        //string id = string.Empty;
        [System.ComponentModel.DefaultValue(0)]
        public int Id { get; set; }

        //string data01 = string.Empty;
        [System.ComponentModel.DefaultValue("")]
        public string Data01 { get; set; }

        //string data02 = string.Empty;
        [System.ComponentModel.DefaultValue("")]
        public string Data02 { get; set; }

        //string data03 = string.Empty;
        [System.ComponentModel.DefaultValue("")]
        public string Data03 { get; set; }

        //string data04 = string.Empty;
        [System.ComponentModel.DefaultValue("")]
        public string Data04 { get; set; }
        
        //string data05 = string.Empty;
        [System.ComponentModel.DefaultValue("")]
        public string Data05 { get; set; }
    }
}
