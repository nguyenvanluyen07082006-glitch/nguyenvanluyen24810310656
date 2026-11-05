
using System;
using System.Windows.Forms;
using Bai2_LabEquipment.Forms;

namespace Bai2_LabEquipment
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
