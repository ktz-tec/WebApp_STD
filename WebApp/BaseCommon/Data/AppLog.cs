using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseCommon.Basic;

namespace BaseCommon.Data
{
    public class AppLog
    {
        public static int WriteLog(string userId, LogType logType, string logger, string message)
        {
            Dictionary<string, object> paras = new Dictionary<string, object>();
            paras.Add("LogDate", IdGenerator.GetServerDate().ToString("yyyy-MM-dd HH:mm:ss"));
            paras.Add("userId", userId);
            paras.Add("logType", logType.ToString());
            paras.Add("logger", logger);
            paras.Add("message", message.Length<4000?message:message.Substring(0,3999));
            string sql = "insert into AppLog(LogDate,UserId,LogLevel,Logger,Message)" +
                     "values(@LogDate,@userId,@logType,@logger,@message)";
            return AppMember.DbHelper.ExecuteSql(sql,paras);
        }

        /// <summary>
        /// 删除60天前的debug信息
        /// </summary>
        /// <returns></returns>
        public static int DeleteLog()
        {
            string sql = "delete from  AppLog where LogLevel='Debug' and LogDate<GETDATE()-60 ";
            return AppMember.DbHelper.ExecuteSql(sql);
        }
    }
}
