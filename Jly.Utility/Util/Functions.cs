using Jly.Utility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Jly.Utility.Util
{
    /// <summary>
    /// 特定功能方法
    /// </summary>
    public class Functions
    {
        /// <summary>
        /// 当前非漫游用户使用的应用程序特定数据的公共储存库的目录
        /// </summary>
        public static string SpecialFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        /// <summary>
        /// 获取指定文件的路径
        /// </summary>
        /// <returns></returns>
        public static string GetFilePath()
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "所有文件(*.*)|*.*|CSV文件(*.csv)|*.csv|Excel文件|*.xlsx;*.xls)"
            };
            if (ofd.ShowDialog() == true)
                return ofd.FileName;
            else
                return null;
        }

        /// <summary>
        /// 设置保存文件的路径
        /// </summary>
        /// <returns></returns>
        public static string SetFilePath(string fileName = "document")
        {
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog()
            {
                FileName = fileName,
                Filter = "所有文件(*.*)|*.*|CSV文件(*.csv)|*.csv|文本文件|*.txt|Excel文件|*.xlsx;xls"
            };
            if (sfd.ShowDialog() == true)
            {
                return sfd.FileName;
            }
            else
            {
                return null;
            }
        }


    }
}
