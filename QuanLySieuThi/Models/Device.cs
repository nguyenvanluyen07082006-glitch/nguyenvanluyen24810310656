using System.ComponentModel;

namespace QuanLySieuThi.Models
{
    public class Device : INotifyPropertyChanged
    {
        private string ma = "";
        private string ten = "";
        private string loai = "";
        private decimal gia;
        private int soLuong;

        public string Ma
        {
            get { return ma; }
            set
            {
                ma = value;
                ThongBaoThayDoi("Ma");
            }
        }

        public string Ten
        {
            get { return ten; }
            set
            {
                ten = value;
                ThongBaoThayDoi("Ten");
            }
        }

        public string Loai
        {
            get { return loai; }
            set
            {
                loai = value;
                ThongBaoThayDoi("Loai");
            }
        }

        public decimal Gia
        {
            get { return gia; }
            set
            {
                gia = value;
                ThongBaoThayDoi("Gia");
            }
        }

        public int SoLuong
        {
            get { return soLuong; }
            set
            {
                soLuong = value;
                ThongBaoThayDoi("SoLuong");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void ThongBaoThayDoi(string tenThuocTinh)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(tenThuocTinh));
        }
    }
}