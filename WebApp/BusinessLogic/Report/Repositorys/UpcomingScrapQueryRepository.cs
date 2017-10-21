﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseCommon.Basic;
using System.Data;
using BaseCommon.Data;
using BusinessLogic.Report.Models.UpcomingScrapQuery;

namespace BusinessLogic.Report.Repositorys
{
    public class UpcomingScrapQueryRepository : IQuery
    {

        public virtual DataTable GetReportGridDataTable(ListCondition condition, bool needPaging)
        {
           
            string sql = string.Format(@"select  row_number() over(order by  Assets.assetsNo) as rownumber,
Assets.assetsId assetsId,
                        Assets.assetsNo assetsNo,
                        Assets.assetsName assetsName,
                        (select assetsClassName from AssetsClass where Assets.assetsClassId=AssetsClass.assetsClassId) assetsClassId,
	                (select departmentName from AppDepartment where Assets.departmentId=AppDepartment.departmentId) departmentId,
                   (select storeSiteName from StoreSite where Assets.storeSiteId=StoreSite.storeSiteId) storeSiteId,
	                  Assets.durableYears durableYears,
	                convert(nvarchar(100),  Assets.purchaseDate ,23) purchaseDate,
                    convert(nvarchar(100),  DATEADD(yy,Assets.durableYears,Assets.purchaseDate) ,23)  endDate,
	                (select CodeTable.codeName from CodeTable where Assets.assetsState=CodeTable.codeNo and CodeTable.codeType='AssetsState' and CodeTable.languageVer='{0}' ) assetsState
                from Assets where 1=1 {1}  ", AppMember.AppLanguage.ToString(), ListWhereSql(condition).Sql);
            if (needPaging)
            {
                sql = string.Format("select top {0} *  from ({1}) A where rownumber > {2} order by   assetsNo ", condition.PageRowNum, sql, (condition.PageIndex - 1) * condition.PageRowNum);
            }
            else
            {
                sql += " order by   Assets.assetsNo";
            }
            DataTable dtGrid = AppMember.DbHelper.GetDataSet(sql, ListWhereSql(condition).DBPara).Tables[0];
            return dtGrid;
        }

        protected  WhereConditon ListWhereSql(ListCondition condition)
        {
            WhereConditon wcd = new WhereConditon();
            wcd.DBPara = new Dictionary<string, object>();
            EntryModel model = JsonHelper.Deserialize<EntryModel>(condition.ListModelString);
            if (model == null) return wcd;
            if (DataConvert.ToString(model.AssetsNo) != "")
            {
                wcd.Sql += @" and Assets.assetsNo  like '%'+@assetsNo+'%'";
                wcd.DBPara.Add("assetsNo", model.AssetsNo);
            }
            if (DataConvert.ToString(model.AssetsName) != "")
            {
                wcd.Sql += @" and Assets.assetsName  like '%'+@assetsName+'%'";
                wcd.DBPara.Add("assetsName", model.AssetsName);
            }
            if (DataConvert.ToString(model.AssetsClassId) != "")
            {
                wcd.Sql += @" and  Assets.assetsClassId=@assetsClassId";
                wcd.DBPara.Add("assetsClassId", model.AssetsClassId);
            }
            if (DataConvert.ToString(model.DepartmentId) != "")
            {
                wcd.Sql += @" and Assets.departmentId=@departmentId";
                wcd.DBPara.Add("departmentId", model.DepartmentId);
            }
            if (DataConvert.ToString(model.StoreSiteId) != "")
            {
                wcd.Sql += @" and Assets.storeSiteId=@storeSiteId";
                wcd.DBPara.Add("storeSiteId", model.StoreSiteId);
            }
            if (DataConvert.ToString(model.AssetsState) != "")
            {
                wcd.Sql += @" and Assets.assetsState=@assetsState";
                wcd.DBPara.Add("assetsState", model.AssetsState);
            }
            if (DataConvert.ToString(model.DepreciationEndDate1) != "")
            {
                wcd.Sql += @" and DATEADD(yy,Assets.durableYears,Assets.purchaseDate)>=@depreciationEndDate1 ";
                wcd.DBPara.Add("depreciationEndDate1", DataConvert.ToString(model.DepreciationEndDate1)+" 00:00:00");
            }
            if (DataConvert.ToString(model.DepreciationEndDate2) != "")
            {
                wcd.Sql += @" and DATEADD(yy,Assets.durableYears,Assets.purchaseDate)<=@depreciationEndDate2 ";
                wcd.DBPara.Add("depreciationEndDate2", DataConvert.ToString(model.DepreciationEndDate2) + " 23:59:59");
            }
            return wcd;
        }



    }
}
