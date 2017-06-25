﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BaseCommon.Basic;
using BaseCommon.Data;
using BusinessLogic.BasicData;
using BaseCommon.Models;


namespace BusinessLogic.BasicData.Models.StoreSite
{
    public class ListModel : ListViewModel
    {
  
        [AppDisplayNameAttribute("StoreSiteNo")]
        public string StoreSiteNo { get; set; }
        [AppDisplayNameAttribute("StoreSiteName")]
        public string StoreSiteName { get; set; }
        [AppDisplayNameAttribute("StoreSiteParentId")]
        public string ParentId { get; set; }

    }
}