using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using BaseCommon.Data;

namespace BaseCommon.Basic
{
    public class AppMember
    {
        public static string ConnectionString { get; set; }
        public static DbProviderFactory Provider { get; set; }
        public static Dictionary<string, string> AppText { get; set; }
        public static Dictionary<string, string> TablePrefix { get; set; }
        public static DatabaseHelper DbHelper { get; set; }
        public static Language AppLanguage { get; set; }
        public static DBMS AppDBMS { get; set; }
        public static string GridPath { get; set; }
        public static int BodyHeight { get; set; }
        public const string HideString = "String";
        public const string DeletePK = "_DeletePK";
        public const string IntReg = @"^[0-9]*[1-9][0-9]*$";
        public const string DoubleReg = @"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$";
        /// <summary>
        /// 是否启用自动月次更新
        /// </summary>
        public static string AutoMonthUpdate { get; set; }
        /// <summary>
        /// 是否启用折旧
        /// </summary>
        public static string LaunchDepreciation { get; set; }
        /// <summary>
        /// 是否开启自动折旧
        /// </summary>
        public static string AutoDepreciation { get; set; }
        /// <summary>
        /// 打印机名称
        /// </summary>
        public static string PrinterName { get; set; }
        public static string SqllitePath { get; set; }
        public static UsePeopleControlLevel UsePeopleControlLevel { get; set; }
        public static bool DepreciationRuleOpen { get; set; }
        public static string ViewVersion { get; set; }  
    }
}
