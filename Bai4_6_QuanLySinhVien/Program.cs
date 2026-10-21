
namespace Bai4_6_QuanLySinhVien;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        Application.Run(new FormStudent());
    }
}
