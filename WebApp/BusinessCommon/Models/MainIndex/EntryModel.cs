using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BaseCommon.Basic;
using System.Data;
using BusinessCommon.Repositorys;
using BaseCommon.Models;
using BaseCommon.Data;

namespace BusinessCommon.Models.MainIndex
{
    public class EntryModel : EntryViewModel
    {
        public string CssMergeUrl { get; set; } 

        public ToolsRepository Repository = new ToolsRepository();

        public string EntryGridId { get; set; }
        public GridLayout EntryGridLayout { get; set; }
    }
}