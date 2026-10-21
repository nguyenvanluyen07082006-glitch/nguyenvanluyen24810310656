
using System.ComponentModel;

namespace Bai4_7_MDI
{
    public class Student : INotifyPropertyChanged
    {
        private string studentId = "";
        private string fullName = "";
        private int age;
        private string gender = "Nam";

        public string StudentId
        {
            get { return studentId; }
            set
            {
                studentId = value;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(StudentId))
                );
            }
        }

        public string FullName
        {
            get { return fullName; }
            set
            {
                fullName = value;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(FullName))
                );
            }
        }

        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(Age))
                );
            }
        }

        public string Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(Gender))
                );
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
