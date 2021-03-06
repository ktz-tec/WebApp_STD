﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BaseCommon.Basic;
using BaseCommon.Data;
using WebApp.BaseWeb.Common;
using BusinessLogic.BasicData;

namespace WebApp.Areas.BasicData.Models.AssetsType
{
    public class ListModel : ListViewModel
    {
        public ListModel()
        {
            Repository = new AssetsTypeRepository();
        }

        [AppDisplayNameAttribute("AssetsTypeNo")]
        public string AssetsTypeNo { get; set; }
        [AppDisplayNameAttribute("AssetsTypeName")]
        public string AssetsTypeName { get; set; }

    }
}