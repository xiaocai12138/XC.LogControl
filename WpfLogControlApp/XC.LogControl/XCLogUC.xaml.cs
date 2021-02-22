using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XC.LogControl
{
    /// <summary>
    /// XCLogUC.xaml 的交互逻辑
    /// </summary>
    public partial class XCLogUC : UserControl
    {
        public XCLogUC()
        {
            InitializeComponent();
        }

        public int UnitIndex
        {
            get;
            private set;
        }

        private Paragraph m_TextBoxParagraph = null;
        public Paragraph TextBoxParagraph
        {
            get
            {
                if (m_TextBoxParagraph == null)
                {
                    m_TextBoxParagraph = new Paragraph();
                }
                return m_TextBoxParagraph;
            }
        }

        public void ShowLog(string lopStr, LogUITypeEnum logType)
        {
            String datetime = string.Format("时间：{0}", DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss"));//时间
            String errorStr = lopStr;//日志内容
            TextBox.Document.Blocks.Add(TextBoxParagraph);
            if (TextBoxParagraph.Inlines.Count == 500)
            {
                this.Clear();
            }
            SystemRunLog.ApplicationRunLog.WriteLogByLogUI(lopStr, datetime, logType);
            switch (logType)//根据日志类型区分颜色
            {
                case LogUITypeEnum.Unit:
                    if (UnitIndex > 0)
                    {
                        TextBoxParagraph.Inlines.Add(new Run("\r\n") { Foreground = Brushes.Black });
                    }
                    TextBoxParagraph.Inlines.Add(new Run(string.Format("{0}):{1}    {2}\r\n", ++UnitIndex, errorStr, datetime)) { FontFamily = new System.Windows.Media.FontFamily("微软雅黑"), Foreground = Brushes.Blue });
                    break;
                case LogUITypeEnum.Success://成功
                    TextBoxParagraph.Inlines.Add(new Run(string.Format("成功：{0}    {1}\r\n", errorStr, datetime)) { FontFamily = new System.Windows.Media.FontFamily("微软雅黑"), Foreground = Brushes.Black });
                    break;
                case LogUITypeEnum.Error://错误
                    TextBoxParagraph.Inlines.Add(new Run(string.Format("错误：{0}    {1}\r\n", errorStr, datetime)) { FontFamily = new System.Windows.Media.FontFamily("微软雅黑"), Foreground = Brushes.Red });
                    break;
                case LogUITypeEnum.Information://提示
                    TextBoxParagraph.Inlines.Add(new Run(string.Format("提示：{0}    {1}\r\n", errorStr, datetime)) { FontFamily = new System.Windows.Media.FontFamily("微软雅黑"), Foreground = Brushes.Aqua });
                    break;
                case LogUITypeEnum.Warning://警告
                    TextBoxParagraph.Inlines.Add(new Run(string.Format("警告：{0}    {1}\r\n", errorStr, datetime)) { FontFamily = new System.Windows.Media.FontFamily("微软雅黑"), Foreground = Brushes.Green });
                    break;
                case LogUITypeEnum.Line:
                    TextBoxParagraph.Inlines.Add(new Run("\r\n") { FontFamily = new System.Windows.Media.FontFamily("微软雅黑"), Foreground = Brushes.Black });
                    break;
                default:
                    TextBoxParagraph.Inlines.Add(new Run(datetime + "\r\n") { FontFamily = new System.Windows.Media.FontFamily("微软雅黑"), Foreground = Brushes.Black });
                    TextBoxParagraph.Inlines.Add(new Run(errorStr + "\r\n") { FontFamily = new System.Windows.Media.FontFamily("微软雅黑"), Foreground = Brushes.Black });
                    break;
            };
            this.TextBox.ScrollToEnd();
            System.Windows.Forms.Application.DoEvents();
        }

        /// <summary>
        /// 输出日志到文件
        /// </summary>
        /// <param name="outPath">文件路径</param>
        public void Output(string outPath)
        {
            DateTime dt = DateTime.Now;
            //导出日志文件，以日期时间命名
            StreamWriter sw = File.CreateText(outPath);
            TextRange range = new TextRange(TextBox.Document.ContentStart, TextBox.Document.ContentEnd);
            sw.WriteLine(range.Text);
            sw.Close();
        }


        public void Clear()
        {
            UnitIndex = 0;
            m_TextBoxParagraph = null;
            System.Windows.Documents.FlowDocument doc = this.TextBox.Document;
            doc.Blocks.Clear();
        }

        public void ExportTxt()
        {
            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            sfd.Filter = "文本文件|*.txt";
            sfd.RestoreDirectory = true;
            sfd.AddExtension = true;

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (StreamWriter s = new System.IO.StreamWriter(sfd.FileName, false))
                {
                    TextRange range = new TextRange(TextBox.Document.ContentStart, TextBox.Document.ContentEnd);
                    s.WriteLine(range.Text);
                }
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            ExportTxt();
        }
    }
}
