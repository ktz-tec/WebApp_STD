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
        public DataTable GetEntryGridDataTable(Dictionary<string, object> paras)
        {
            DataTable dtScrap = GetScrapDataTable(paras);
            DataTable dtPurchase = GetPurchaseDataTable(paras);
            DataTable dtTransfer = GetTransferDataTable(paras);

            DataTable dtGrid = dtScrap.Copy();
            foreach (DataRow dr in dtPurchase.Rows)
            {
                dtGrid.ImportRow(dr);
            }
            foreach (DataRow dr in dtTransfer.Rows)
            {
                dtGrid.ImportRow(dr);
            }
            return dtGrid;
        }

        private DataTable GetScrapDataTable(Dictionary<string, object> paras)
        {
            string sql = string.Format(@"select '报废申请' ApplyType,
            S.assetsScrapId RefId,
            (select userName from AppUser where S.createId=AppUser.userId) ApplyUser,
            S.createTime ApplyTime ,
            (select top 1 codename from CodeTable where codetype='ApproveState' and  S.approveState=codeno) ApproveState,
S.assetsScrapNo No,
           (case when A.approveState='O' then '审批' else  '再申请' end)  ApproveClass,
(select userName from AppUser where AppUser.userId=@approver) Approver ,
A.createTime ApproveCreateTime 
            from AssetsScrap S,AppApprove A
            where A.tableName='AssetsScrap' and A.approveState  in ('O','B')
            and S.assetsScrapId=A.refId and A.isValid='Y'
            and A.approver=@approver ");
//            sql += " union all " + string.Format(@"select '报废申请' ApplyType,
//            S.assetsScrapId RefId,
//            (select userName from AppUser where S.createId=AppUser.userId) ApplyUser,
//            S.createTime ApplyTime ,
//            (select top 1 codename from CodeTable where codetype='ApproveState' and  S.approveState=codeno) ApproveState
//            from AssetsScrap S 
//            where S.createId=@approver and S.approveState='R'  ");
            DataTable dtGrid = AppMember.DbHelper.GetDataSet(sql, paras).Tables[0];
            return dtGrid;
        }

        public DataTable GetTransferDataTable(Dictionary<string, object> paras)
        {

            string sql = string.Format(@"select '调拨申请' ApplyType,
S.assetsTransferId RefId,
(select userName from AppUser where S.createId=AppUser.userId) ApplyUser,
S.createTime ApplyTime ,
(select top 1 codename from CodeTable where codetype='ApproveState' and  S.approveState=codeno) ApproveState,
S.assetsTransferNo No,
(case when A.approveState='O' then '审批' else  '再申请' end)  ApproveClass,
(select top 1 userName from AppUser where AppUser.userId=@approver) Approver ,
A.createTime ApproveCreateTime 
from AssetsTransfer S,AppApprove A
where A.tableName='AssetsTransfer' and A.approveState  in ('O','B')
and S.assetsTransferId=A.refId and A.isValid='Y'
and A.approver=@approver ");
//            sql += " union all " + string.Format(@"select '调拨申请' ApplyType,
//S.assetsTransferId RefId,
//(select userName from AppUser where S.createId=AppUser.userId) ApplyUser,
//S.createTime ApplyTime ,
//(select top 1 codename from CodeTable where codetype='ApproveState' and  S.approveState=codeno) ApproveState
//from AssetsTransfer S
//where S.createId=@approver and S.approveState='R'");
            DataTable dtGrid = AppMember.DbHelper.GetDataSet(sql, paras).Tables[0];
            return dtGrid;
        }

        public DataTable GetPurchaseDataTable(Dictionary<string, object> paras)
        {
            string sql = string.Format(@"select '采购申请' ApplyType,
S.assetsPurchaseId RefId,
(select userName from AppUser where S.createId=AppUser.userId) ApplyUser,
S.createTime ApplyTime ,
(select top 1 codename from CodeTable where codetype='ApproveState' and  S.approveState=codeno) ApproveState,
S.assetsPurchaseNo No,
(case when A.approveState='O' then '审批' else  '再申请' end)  ApproveClass,
(select top 1 userName from AppUser where AppUser.userId=@approver) Approver ,
A.createTime ApproveCreateTime 
from AssetsPurchase S,AppApprove A
where A.tableName='AssetsPurchase' and A.approveState  in ('O','B')
and S.assetsPurchaseId=A.refId and A.isValid='Y'
and A.approver=@approver ");
    //            sql += " union all " + string.Format(@"select '采购申请' ApplyType,
    //S.assetsPurchaseId RefId,
    //(select userName from AppUser where S.createId=AppUser.userId) ApplyUser,
    //S.createTime ApplyTime ,
    //(select top 1 codename from CodeTable where codetype='ApproveState' and  S.approveState=codeno) ApproveState
    //from AssetsPurchase S
    //where S.createId=@approver and S.approveState='R'");
            DataTable dtGrid = AppMember.DbHelper.GetDataSet(sql, paras).Tables[0];
            return dtGrid;
        }
    }
}
