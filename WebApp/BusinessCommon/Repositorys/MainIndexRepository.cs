using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BaseCommon.Basic;
using BaseCommon.Data;
using System.Collections;

namespace BusinessCommon.Repositorys
{
    public class MainIndexRepository
    {
        public  DataTable GetEntryGridDataTable(Dictionary<string, object> paras)
        {
                string sql = string.Format(@"select '报废申请' ApplyType,
S.assetsScrapId RefId,
(select userName from AppUser where S.createId=AppUser.userId) ApplyUser,
S.createTime ApplyTime ,
(select top 1 codename from CodeTable where codetype='ApproveState' and  S.approveState=codeno) ApproveState
from AssetsScrap S,AppApprove A
where A.tableName='AssetsScrap' and A.approveState='O'
and S.assetsScrapId=A.refId and A.isValid='Y'
and A.approver=@approver ");
                sql += " union all " + string.Format(@"select '报废申请' ApplyType,
S.assetsScrapId RefId,
(select userName from AppUser where S.createId=AppUser.userId) ApplyUser,
S.createTime ApplyTime ,
(select top 1 codename from CodeTable where codetype='ApproveState' and  S.approveState=codeno) ApproveState
from AssetsScrap S 
where S.createId=@approver and S.approveState='R'  ");

                sql +=" union all "+ string.Format(@"select '调拨申请' ApplyType,
S.assetsTransferId RefId,
(select userName from AppUser where S.createId=AppUser.userId) ApplyUser,
S.createTime ApplyTime ,
(select top 1 codename from CodeTable where codetype='ApproveState' and  S.approveState=codeno) ApproveState
from AssetsTransfer S,AppApprove A
where A.tableName='AssetsTransfer' and A.approveState='O'
and S.assetsTransferId=A.refId and A.isValid='Y'
and A.approver=@approver ");
                sql += " union all " + string.Format(@"select '调拨申请' ApplyType,
S.assetsTransferId RefId,
(select userName from AppUser where S.createId=AppUser.userId) ApplyUser,
S.createTime ApplyTime ,
(select top 1 codename from CodeTable where codetype='ApproveState' and  S.approveState=codeno) ApproveState
from AssetsTransfer S
where S.createId=@approver and S.approveState='R'");

                sql += " union all " + string.Format(@"select '采购申请' ApplyType,
S.assetsPurchaseId RefId,
(select userName from AppUser where S.createId=AppUser.userId) ApplyUser,
S.createTime ApplyTime ,
(select top 1 codename from CodeTable where codetype='ApproveState' and  S.approveState=codeno) ApproveState
from AssetsPurchase S,AppApprove A
where A.tableName='AssetsPurchase' and A.approveState='O'
and S.assetsPurchaseId=A.refId and A.isValid='Y'
and A.approver=@approver ");
                sql += " union all " + string.Format(@"select '采购申请' ApplyType,
S.assetsPurchaseId RefId,
(select userName from AppUser where S.createId=AppUser.userId) ApplyUser,
S.createTime ApplyTime ,
(select top 1 codename from CodeTable where codetype='ApproveState' and  S.approveState=codeno) ApproveState
from AssetsPurchase S
where S.createId=@approver and S.approveState='R'");
                DataTable dtGrid = AppMember.DbHelper.GetDataSet(sql, paras).Tables[0];
                return dtGrid;
        }
    }
}
