using System;
using System.Collections.Generic;
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

namespace WpfLogControlApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            xcLog.ShowLog("第一阶段", XC.LogControl.LogUITypeEnum.Unit);
            xcLog.ShowLog("落霞与孤鹜齐飞", XC.LogControl.LogUITypeEnum.Information);
            xcLog.ShowLog("秋水共长天一色", XC.LogControl.LogUITypeEnum.Success);
            xcLog.ShowLog("李秋水", XC.LogControl.LogUITypeEnum.Warning);
            xcLog.ShowLog("王博", XC.LogControl.LogUITypeEnum.Error);

            xcLog.ShowLog("第二阶段", XC.LogControl.LogUITypeEnum.Unit);
            xcLog.ShowLog("第三阶段", XC.LogControl.LogUITypeEnum.Unit);
            xcLog.ShowLog("第四阶段", XC.LogControl.LogUITypeEnum.Unit);
            xcLog.ShowLog("第五阶段", XC.LogControl.LogUITypeEnum.Unit);
        }
    }
}
