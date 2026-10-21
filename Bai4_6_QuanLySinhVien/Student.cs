
using System.ComponentModel;

namespace Bai4_6_QuanLySinhVien
{
    public class Student : INotifyPropertyChanged
    {
        private string studentId = "";
        private string fullName = "";
        private int age;
        private string gender = "Nam";
        private string avatarPath = "";

        public string StudentId
        {
            get { return studentId; }
            set
            {
                if (studentId != value)
                {
                    studentId = value;
                    ThongBaoThayDoi("StudentId");
                }
            }
        }

        public string FullName
        {
            get { return fullName; }
            set
            {
                if (fullName != value)
                {
                    fullName = value;
                    ThongBaoThayDoi("FullName");
                }
            }
        }

        public int Age
        {
            get { return age; }
            set
            {
                if (age != value)
                {
                    age = value;
                    ThongBaoThayDoi("Age");
                }
            }
        }

        public string Gender
        {
            get { return gender; }
            set
            {
                if (gender != value)
                {
                    gender = value;
                    ThongBaoThayDoi("Gender");
                }
            }
        }

        public string AvatarPath
        {
            get { return avatarPath; }
            set
            {
                if (avatarPath != value)
                {
                    avatarPath = value;
                    ThongBaoThayDoi("AvatarPath");
                }
            }
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
