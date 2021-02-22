using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XC.LogControl
{
    /*--------------------
    *编写人：蔡名洋
    *编写时间：2017-06-30
    *修改时间：2017-06-30
    *代码功能：系统日志类
    --------------------*/

    /// <summary>
    /// 日志类
    /// </summary>
    public class SystemRunLog
    {
        private static SystemRunLog m_ApplicationRunLog = null;
        /// <summary>
        /// 系统运行目录
        /// </summary>
        public static SystemRunLog ApplicationRunLog
        {
            get
            {
                if (m_ApplicationRunLog == null)
                {
                    string strLogFoderPath = System.IO.Path.Combine(Application.StartupPath, "Log", "SystemRun", DateTime.Now.ToString("yyyy年MM月dd日") + ".log");
                    m_ApplicationRunLog = new SystemRunLog(strLogFoderPath);
                }
                return m_ApplicationRunLog;
            }
        }

        /// <summary>
        /// 系统日志路径
        /// </summary>
        public string LogFilePath = "";

        /// <summary>
        /// 重置日志操作信息
        /// </summary>
        public SystemRunLog(string strLogFilePath)
        {
            string strFolderPath = System.IO.Path.GetDirectoryName(strLogFilePath);
            if (!System.IO.Directory.Exists(strFolderPath))
            {
                System.IO.Directory.CreateDirectory(strFolderPath);
            }
            LogFilePath = strLogFilePath;
        }

        /// <summary>
        /// 将错误日志编写到系统日志中
        /// </summary>
        /// <param name="logTxt">系统日志信息</param>
        public void WriteLogForError(string logTxt)
        {
            List<string> Listlog = GetLogTxt(logTxt, SystemLogType.Error);
            WriteLogFromListLog(Listlog);
        }

        /// <summary>
        /// 将警告日志编写到系统日志中
        /// </summary>
        /// <param name="logTxt">系统日志信息</param>
        public void WriteLogForWarning(string logTxt)
        {
            List<string> Listlog = GetLogTxt(logTxt, SystemLogType.Warning);
            WriteLogFromListLog(Listlog);
        }

        /// <summary>
        /// 将错误日志编写到系统日志中
        /// </summary>
        /// <param name="logTxt">系统日志信息</param>
        public void WriteLogForException(string logTxt, Exception ex)
        {
            List<string> Listlog = GetLogForException(logTxt, ex);
            WriteLogFromListLog(Listlog);
        }

        /// <summary>
        /// 将提示日志编写到系统日志中
        /// </summary>
        /// <param name="logTxt">系统日志信息</param>
        public void WriteLogForInformation(string logTxt)
        {
            List<string> Listlog = GetLogTxt(logTxt, SystemLogType.Information);
            WriteLogFromListLog(Listlog);
        }

        /// <summary>
        /// 将成功日志编写到系统日志中
        /// </summary>
        /// <param name="logTxt">系统日志信息</param>
        public void WriteLogForSuccess(string logTxt)
        {
            List<string> Listlog = GetLogTxt(logTxt, SystemLogType.Success);
            WriteLogFromListLog(Listlog);
        }

        private void WriteLogFromListLog(List<string> Listlog)
        {
            StreamWriter fileWriter = new StreamWriter(LogFilePath, true);
            string strWriteLine = "";
            for (int i = 0; i < Listlog.Count; i++)
            {
                if (i == 0)
                {
                    strWriteLine = string.Format("{0}:{1}", Listlog[i], GetDateTxt());
                }
                else
                {
                    strWriteLine = Listlog[i];
                }
                fileWriter.WriteLine(strWriteLine);
                Console.WriteLine(strWriteLine);
            }
            fileWriter.Close();
            WriteLine();
        }

        /// <summary>
        /// 获取异常错误日志
        /// </summary>
        /// <param name="strLogInfo">日志信息</param>
        /// <param name="ErrorEx">错误异常</param>
        /// <returns></returns>
        private List<string> GetLogForException(string strLogInfo, Exception ErrorEx)
        {
            List<string> Listlog = new List<string>();
            Listlog.Add(ReturnLogType(SystemLogType.Error));
            Listlog.Add(strLogInfo);
            Listlog.Add(string.Format("问题描述：{0}", ErrorEx.Message));
            Listlog.Add(string.Format("程序错误地点：{0}", ErrorEx.StackTrace));
            return Listlog;
        }


        private void WriteLine()
        {
            StreamWriter fileWriter = new StreamWriter(LogFilePath, true);
            string strEndLine = "-----------************************************-----------";
            fileWriter.WriteLine(strEndLine);
            Console.WriteLine(strEndLine);

            for (int i = 0; i < 3; i++)
            {
                strEndLine = "";
                fileWriter.WriteLine(strEndLine);
                Console.WriteLine(strEndLine);
            }
            fileWriter.Close();
        }

        private string GetDateTxt()
        {
            string strDateTime = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");
            return strDateTime;
        }

        private List<string> GetLogTxt(string logTxt, SystemLogType logType)
        {
            List<string> Listlog = new List<string>();
            Listlog = logTxt.Split('\n').ToList();
            string strlog = ReturnLogType(logType);
            Listlog.Insert(0, strlog);
            return Listlog;
        }

        private string ReturnLogType(SystemLogType logType)
        {
            string strlog = "";
            switch (logType)
            {
                case SystemLogType.Success:
                    strlog = string.Format("成功");
                    break;
                case SystemLogType.Error:
                    strlog = string.Format("错误");
                    break;
                case SystemLogType.Warning:
                    strlog = string.Format("警告");
                    break;
                case SystemLogType.Information:
                    strlog = string.Format("提示");
                    break;
            }
            return strlog;
        }

        public void ClearLog()
        {
            System.IO.File.Delete(LogFilePath);
        }

        public void ShowLogFile()
        {
            System.Diagnostics.Process.Start(LogFilePath);
        }

        public void WriteLogByLogUI(string lopStr, string datetime, LogUITypeEnum logType)
        {
            List<string> ListLog = new List<string>();
            switch (logType)
            {
                case LogUITypeEnum.Information:
                    ListLog.Add(string.Format("提示:{0}    {1}", lopStr, datetime));
                    break;
                case LogUITypeEnum.Warning:
                    ListLog.Add(string.Format("警告:{0}    {1}", lopStr, datetime));
                    break;
                case LogUITypeEnum.Success:
                    ListLog.Add(string.Format("成功:{0}    {1}", lopStr, datetime));
                    break;
                case LogUITypeEnum.Error:
                    ListLog.Add(string.Format("错误:{0}    {1}", lopStr, datetime));
                    break;
                case LogUITypeEnum.Line:
                    ListLog.Add(string.Format(""));
                    break;
                case LogUITypeEnum.Unit:
                    ListLog.Add(string.Format(""));
                    ListLog.Add(string.Format(""));
                    ListLog.Add(string.Format(""));
                    ListLog.Add(string.Format("{0}    {1}", lopStr, datetime));
                    break;
            }
            WriteLogByListLog(ListLog);
        }

        private void WriteLogByListLog(List<string> ListLog)
        {
            StreamWriter fileWriter = new StreamWriter(LogFilePath, true);
            string strWriteLine = "";
            for (int i = 0; i < ListLog.Count; i++)
            {
                strWriteLine = ListLog[i];
                fileWriter.WriteLine(strWriteLine);
            }
            fileWriter.Close();
        }
    }
}