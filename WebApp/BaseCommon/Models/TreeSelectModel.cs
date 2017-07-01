using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using BaseCommon.Basic;
using BaseCommon.Models;

namespace BaseCommon.Models
{
    public class TreeSelectModel : EntryViewModel
    {
        public string Tree_HideString { get; set; }
        public string TreeId { get; set; }
        public DataTable DataTree { get; set; } 

        public bool ShowCheckBox { get; set; }
        public string SelectId { get; set; }

        public string SearchText { get; set; }
        public string SearchUrl { get; set; }   

    }
}