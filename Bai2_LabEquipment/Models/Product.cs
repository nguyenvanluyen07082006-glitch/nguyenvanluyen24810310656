
using System.ComponentModel;

namespace Bai2_LabEquipment.Models
{
    public class Product : INotifyPropertyChanged
    {
        private string maSP = "";
        private string tenSP = "";
        private string loai = "";
        private decimal donGia;
        private int soLuong;
        private string imagePath = "";

        public string MaSP
        {
            get { return maSP; }
            set
            {
                maSP = value;
                ThongBaoThayDoi("MaSP");
            }
        }

        public string TenSP
        {
            get { return tenSP; }
            set
            {
                tenSP = value;
                ThongBaoThayDoi("TenSP");
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

        public decimal DonGia
        {
            get { return donGia; }
            set
            {
                donGia = value;
                ThongBaoThayDoi("DonGia");
                ThongBaoThayDoi("TotalValue");
            }
        }

        public int SoLuong
        {
            get { return soLuong; }
            set
            {
                soLuong = value;
                ThongBaoThayDoi("SoLuong");
                ThongBaoThayDoi("TotalValue");
            }
        }

        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                imagePath = value;
                ThongBaoThayDoi("ImagePath");
            }
        }

        public decimal TotalValue
        {
            get { return DonGia * SoLuong; }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void ThongBaoThayDoi(string tenThuocTinh)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(tenThuocTinh)
            );
        }
    }
}
