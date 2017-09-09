using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BaseCommon.Data;
using BaseCommon.Basic;

namespace BusinessCommon.CommonBusiness
{
   public class ConfigHelper
    {
       public static string GetConfig(string value)
       {
           string sql = string.Format(@"select configKey,configValue from AppConfig where configKey='{0}' ", value);
           DataTable dt = AppMember.DbHelper.GetDataSet(sql).Tables[0];
           if (dt.Rows.Count > 0)
               return DataConvert.ToString(dt.Rows[0]["configValue"]);
           else
               return "";
       }

    }
}
