
using System.ComponentModel;
using System.Text;

namespace Bai4_6_QuanLySinhVien
{
    public class FormStudent : Form
    {
        private BindingList<Student> danhSach = new BindingList<Student>();
        private BindingSource nguonDuLieu = new BindingSource();

        private DataGridView dgvSinhVien = new DataGridView();

        private TextBox txtMaSV = new TextBox();
        private TextBox txtHoTen = new TextBox();
        private TextBox txtTuoi = new TextBox();

        private ComboBox cboGioiTinh = new ComboBox();
        private PictureBox picAvatar = new PictureBox();

        private Button btnChonAnh = new Button();
        private Button btnThem = new Button();
        private Button btnXoa = new Button();
        private Button btnCapNhat = new Button();
        private Button btnXuatCSV = new Button();

        public FormStudent()
        {
            Text = "Quản lý sinh viên - Bài 4.6";
            Width = 1000;
            Height = 650;
            StartPosition = FormStartPosition.CenterScreen;

            TaoGiaoDien();
            CauHinhBinding();
        }

        private void TaoGiaoDien()
        {
            Label lblMaSV = new Label();
            lblMaSV.Text = "Mã sinh viên:";
            lblMaSV.SetBounds(20, 20, 100, 25);

            txtMaSV.SetBounds(125, 20, 200, 25);

            Label lblHoTen = new Label();
            lblHoTen.Text = "Họ tên:";
            lblHoTen.SetBounds(20, 60, 100, 25);

            txtHoTen.SetBounds(125, 60, 200, 25);

            Label lblTuoi = new Label();
            lblTuoi.Text = "Tuổi:";
            lblTuoi.SetBounds(20, 100, 100, 25);

            txtTuoi.SetBounds(125, 100, 200, 25);

            Label lblGioiTinh = new Label();
            lblGioiTinh.Text = "Giới tính:";
            lblGioiTinh.SetBounds(20, 140, 100, 25);

            cboGioiTinh.SetBounds(125, 140, 200, 25);
            cboGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGioiTinh.Items.Add("Nam");
            cboGioiTinh.Items.Add("Nữ");

            picAvatar.SetBounds(370, 20, 150, 150);
            picAvatar.BorderStyle = BorderStyle.FixedSingle;
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;

            btnChonAnh.Text = "Chọn ảnh";
            btnChonAnh.SetBounds(370, 180, 150, 30);
            btnChonAnh.Click += BtnChonAnh_Click;

            btnThem.Text = "Thêm sinh viên";
            btnThem.SetBounds(20, 220, 130, 35);
            btnThem.Click += BtnThem_Click;

            btnXoa.Text = "Xóa sinh viên";
            btnXoa.SetBounds(160, 220, 130, 35);
            btnXoa.Click += BtnXoa_Click;

            btnCapNhat.Text = "Cập nhật";
            btnCapNhat.SetBounds(300, 220, 100, 35);
            btnCapNhat.Click += BtnCapNhat_Click;

            btnXuatCSV.Text = "Xuất CSV";
            btnXuatCSV.SetBounds(410, 220, 110, 35);
            btnXuatCSV.Click += BtnXuatCSV_Click;

            dgvSinhVien.SetBounds(20, 275, 940, 320);
            dgvSinhVien.AutoGenerateColumns = true;
            dgvSinhVien.AllowUserToAddRows = false;
            dgvSinhVien.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvSinhVien.MultiSelect = false;
            dgvSinhVien.ReadOnly = true;
            dgvSinhVien.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            Controls.AddRange(new Control[]
            {
                lblMaSV,
                txtMaSV,
                lblHoTen,
                txtHoTen,
                lblTuoi,
                txtTuoi,
                lblGioiTinh,
                cboGioiTinh,
                picAvatar,
                btnChonAnh,
                btnThem,
                btnXoa,
                btnCapNhat,
                btnXuatCSV,
                dgvSinhVien
            });
        }

        private void CauHinhBinding()
        {
            nguonDuLieu.DataSource = danhSach;
            dgvSinhVien.DataSource = nguonDuLieu;

            txtMaSV.DataBindings.Add(
                "Text",
                nguonDuLieu,
                "StudentId",
                true,
                DataSourceUpdateMode.OnPropertyChanged
            );

            txtHoTen.DataBindings.Add(
                "Text",
                nguonDuLieu,
                "FullName",
                true,
                DataSourceUpdateMode.OnPropertyChanged
            );

            txtTuoi.DataBindings.Add(
                "Text",
                nguonDuLieu,
                "Age",
                true,
                DataSourceUpdateMode.Never
            );

            cboGioiTinh.DataBindings.Add(
                "SelectedItem",
                nguonDuLieu,
                "Gender",
                true,
                DataSourceUpdateMode.OnPropertyChanged
            );

            dgvSinhVien.DataBindingComplete += (sender, e) =>
            {
                if (dgvSinhVien.Columns["AvatarPath"] != null)
                {
                    dgvSinhVien.Columns["AvatarPath"].Visible = false;
                }
            };

            nguonDuLieu.PositionChanged += (sender, e) =>
            {
                HienThiAvatar();
            };
        }

        private void HienThiAvatar()
        {
            if (nguonDuLieu.Current is Student sv)
            {
                HienThiAnh(sv.AvatarPath);
            }
            else
            {
                HienThiAnh("");
            }
        }

        private void HienThiAnh(string duongDan)
        {
            Image? anhCu = picAvatar.Image;
            picAvatar.Image = null;
            anhCu?.Dispose();

            if (string.IsNullOrWhiteSpace(duongDan))
            {
                return;
            }

            if (!File.Exists(duongDan))
            {
                return;
            }

            try
            {
                using (Image anhGoc = Image.FromFile(duongDan))
                {
                    picAvatar.Image = new Bitmap(anhGoc);
                }
            }
            catch
            {
                MessageBox.Show(
                    "Không thể hiển thị ảnh đã chọn.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            Student sv = new Student();

            danhSach.Add(sv);
            nguonDuLieu.Position = danhSach.Count - 1;

            txtMaSV.Focus();
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            if (nguonDuLieu.Current == null)
            {
                MessageBox.Show("Chưa có sinh viên để xóa.");
                return;
            }

            DialogResult ketQua = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa sinh viên này không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (ketQua == DialogResult.Yes)
            {
                nguonDuLieu.RemoveCurrent();
                HienThiAvatar();
            }
        }

        private void BtnCapNhat_Click(object? sender, EventArgs e)
        {
            if (nguonDuLieu.Current is not Student sv)
            {
                MessageBox.Show("Vui lòng thêm sinh viên trước.");
                return;
            }

            int tuoi;

            if (string.IsNullOrWhiteSpace(txtMaSV.Text))
            {
                MessageBox.Show("Mã sinh viên không được để trống.");
                txtMaSV.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Họ tên không được để trống.");
                txtHoTen.Focus();
                return;
            }

            if (!int.TryParse(txtTuoi.Text, out tuoi)
                || tuoi < 1
                || tuoi > 150)
            {
                MessageBox.Show("Tuổi phải là số nguyên từ 1 đến 150.");
                txtTuoi.Focus();
                return;
            }

            bool trungMa = danhSach.Any(
                x => x != sv &&
                x.StudentId.Equals(
                    txtMaSV.Text.Trim(),
                    StringComparison.OrdinalIgnoreCase
                )
            );

            if (trungMa)
            {
                MessageBox.Show("Mã sinh viên đã tồn tại.");
                txtMaSV.Focus();
                return;
            }

            sv.StudentId = txtMaSV.Text.Trim();
            sv.FullName = txtHoTen.Text.Trim();
            sv.Age = tuoi;

            if (cboGioiTinh.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn giới tính.");
                cboGioiTinh.Focus();
                return;
            }

            sv.Gender = cboGioiTinh.SelectedItem.ToString() ?? "Nam";

            nguonDuLieu.ResetCurrentItem();
            dgvSinhVien.Refresh();

            MessageBox.Show(
                "Cập nhật sinh viên thành công.",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void BtnChonAnh_Click(object? sender, EventArgs e)
        {
            if (nguonDuLieu.Current is not Student sv)
            {
                MessageBox.Show("Vui lòng thêm sinh viên trước.");
                return;
            }

            using OpenFileDialog hopThoai = new OpenFileDialog();

            hopThoai.Title = "Chọn ảnh sinh viên";
            hopThoai.Filter =
                "Tệp hình ảnh|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (hopThoai.ShowDialog() == DialogResult.OK)
            {
                sv.AvatarPath = hopThoai.FileName;
                HienThiAvatar();
            }
        }

        private void BtnXuatCSV_Click(object? sender, EventArgs e)
        {
            using SaveFileDialog hopThoai = new SaveFileDialog();

            hopThoai.Title = "Xuất danh sách sinh viên";
            hopThoai.Filter = "CSV (*.csv)|*.csv";
            hopThoai.FileName = "DanhSachSinhVien.csv";

            if (hopThoai.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                using StreamWriter tep = new StreamWriter(
                    hopThoai.FileName,
                    false,
                    new UTF8Encoding(true)
                );

                tep.WriteLine(
                    "StudentId,FullName,Age,Gender,AvatarPath"
                );

                foreach (Student sv in danhSach)
                {
                    tep.WriteLine(
                        Csv(sv.StudentId) + "," +
                        Csv(sv.FullName) + "," +
                        Csv(sv.Age.ToString()) + "," +
                        Csv(sv.Gender) + "," +
                        Csv(sv.AvatarPath)
                    );
                }

                MessageBox.Show(
                    "Xuất file CSV thành công.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất CSV: " + ex.Message);
            }
        }

        private string Csv(string giaTri)
        {
            giaTri = giaTri.Replace("\"", "\"\"");
            return "\"" + giaTri + "\"";
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            HienThiAnh("");
            base.OnFormClosed(e);
        }
    }
}
