using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XC.LogControl
{
    public interface ILogShow
    {
        /// <summary>
        /// 输出日志到UI
        /// </summary>
        /// <param name="lopStr">日志内容</param>
        /// <param name="logType">日志类型</param>
        void ShowLog(string lopStr, LogUITypeEnum logType);

        /// <summary>
        /// 清空
        /// </summary>
        void Clear();

        /// <summary>
        /// 输出
        /// </summary>
        void ExportTxt();

        /// <summary>
        /// 输出日志到文件
        /// </summary>
        /// <param name="outPath">文件路径</param>
        void Output(string outPath);
    }
}
