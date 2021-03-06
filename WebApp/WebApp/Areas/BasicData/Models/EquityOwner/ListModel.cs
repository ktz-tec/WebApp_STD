﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BaseCommon.Basic;
using BaseCommon.Data;
using WebApp.BaseWeb.Common;
using BusinessLogic.BasicData;

namespace WebApp.Areas.BasicData.Models.EquityOwner
{
    public class ListModel : ListViewModel
    {
        public ListModel()
        {
            Repository = new EquityOwnerRepository();
        }

        [AppDisplayNameAttribute("EquityOwnerNo")]
        public string EquityOwnerNo { get; set; }
        [AppDisplayNameAttribute("EquityOwnerName")]
        public string EquityOwnerName { get; set; }

    }
}