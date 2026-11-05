namespace Bai1_ClinicAppointment.Models
{
    public class Appointment
    {
        public string MaLichHen { get; set; } = "";
        public string HoTen { get; set; } = "";
        public string SoDienThoai { get; set; } = "";
        public DateTime NgayGioHen { get; set; }
        public string BacSi { get; set; } = "";
        public string LyDoKham { get; set; } = "";
        public string TrangThai { get; set; } = "Chờ khám";
    }
}