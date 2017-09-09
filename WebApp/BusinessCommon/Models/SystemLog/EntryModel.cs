using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseCommon.Basic;
using BaseCommon.Models;
using System.ComponentModel;

namespace BusinessCommon.Models.SystemLog
{
    public class EntryModel : QueryEntryViewModel
    {

        [AppDisplayNameAttribute("UserName")]
        public string UserName { get; set; }

        [AppDisplayNameAttribute("Message")]
        public string LogMessage { get; set; }

        [DisplayName("日志类型")]
        public string LogType { get; set; }

        [AppDisplayNameAttribute("LogDate1")]
        public string LogDate1 { get; set; }

        [AppDisplayNameAttribute("LogDate2")]
        public string LogDate2 { get; set; }



    }
}
