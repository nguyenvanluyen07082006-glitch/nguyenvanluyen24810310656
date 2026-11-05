using System;
using System.Windows.Forms;
using Bai1_ClinicAppointment.Forms;

namespace Bai1_ClinicAppointment
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            Application.Run(new frmAppointment());
        }
    }
}