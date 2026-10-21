
using System;
using System.Windows.Forms;
using QuanLySieuThi.Forms;

namespace QuanLySieuThi
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new frmInventoryManager());
        }
    }
}


