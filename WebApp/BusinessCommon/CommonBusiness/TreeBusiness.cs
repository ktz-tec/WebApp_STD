using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BaseCommon.Data;

namespace BusinessCommon.CommonBusiness
{
    public class TreeBusiness
    {

        public static DataTable GetSearchDataTable(string searchText, DataTable dtSource)
        {
            DataTable dt = new DataTable();
            searchText = DataConvert.ToString(searchText);
            if (searchText != "")
            {
                DataRow[] drs = dtSource.Select(" name like '%" + searchText.ToUpper() + "%'");
                List<DataRow> resultList = new List<DataRow>();
                resultList.AddRange(drs);
                var parentIds = drs.Select(m => m.Field<string>("parentId")).Distinct().ToList();
                if (parentIds.Count > 0)
                {
                    var parentList = DistinctParent(parentIds, dtSource);
                    resultList.AddRange(parentList);
                    resultList = resultList.Distinct().ToList();
                }
                if (resultList.Count > 0)
                {
                    dt = resultList.CopyToDataTable();
                }
            }
            else
            {
                return dtSource;
            }
            return dt;
        }

        private static List<DataRow> DistinctParent(List<string> parentIds, DataTable dtAll)
        {
            string insql = "";
            foreach (string pid in parentIds)
            {
                insql += "'" + pid + "',";
            }
            insql = insql.Substring(0, insql.Length - 1);
            var parentDrs = dtAll.Select(string.Format(" id in ({0})", insql));
            List<DataRow> retList = new List<DataRow>();
            retList.AddRange(parentDrs);
            var pIds = parentDrs.Select(m => m.Field<string>("parentId")).Distinct().ToList();
            if (pIds.Count > 0)
            {
                var pDrs = DistinctParent(pIds, dtAll);
                retList.AddRange(pDrs);
            }
            return retList;
            //return retList.Distinct().ToList();
        }
    }
}
