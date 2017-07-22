using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseCommon.Data
{
    public enum Language
    {
        CN,   //中文
        EN    //英文
    }

    public enum DBMS
    {
        SqlServer,
        Access,
        Oracle
    }

    public enum TextType
    {
        /// <summary>
        /// 文本框
        /// </summary>
        Text,
        /// <summary>
        /// 密码框
        /// </summary>
        Password,
        /// <summary>
        /// 隐藏框
        /// </summary>
        Hidden 
    }

    public enum CondtionType
    {
        StringEqual,
        StringLike
    }

    public enum CacheCreateType
    {
        Immediately,
        IfNoThenGenerated
    }

    public enum LogType
    {
        Info,
        Warning,
        Error,
        Debug  
    }

    public enum UsePeopleControlLevel
    {
        High,
        Low
    }
}
