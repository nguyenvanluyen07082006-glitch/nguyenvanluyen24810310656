
using System.ComponentModel;

namespace Bai4_7_MDI
{
    public class FormStudent : Form
    {
        public event Action<string>? BaoTrangThai;

        private BindingList<Student> danhSach =
            new BindingList<Student>();

        private BindingSource nguonDuLieu = new BindingSource();

        private DataGridView dgv = new DataGridView();

        private TextBox txtMa = new TextBox();
        private TextBox txtTen = new TextBox();
        private TextBox txtTuoi = new TextBox();

        private ComboBox cboGioiTinh = new ComboBox();

        public FormStudent()
        {
            Text = "Quản lý sinh viên";
            Width = 850;
            Height = 550;

            TaoGiaoDien();
            CauHinhDuLieu();
        }

        private void TaoGiaoDien()
        {
            Label lblMa = new Label();
            lblMa.Text = "Mã sinh viên:";
            lblMa.SetBounds(20, 20, 100, 25);

            txtMa.SetBounds(125, 20, 180, 25);

            Label lblTen = new Label();
            lblTen.Text = "Họ tên:";
            lblTen.SetBounds(20, 60, 100, 25);

            txtTen.SetBounds(125, 60, 180, 25);

            Label lblTuoi = new Label();
            lblTuoi.Text = "Tuổi:";
            lblTuoi.SetBounds(20, 100, 100, 25);

            txtTuoi.SetBounds(125, 100, 180, 25);

            Label lblGioiTinh = new Label();
            lblGioiTinh.Text = "Giới tính:";
            lblGioiTinh.SetBounds(20, 140, 100, 25);

            cboGioiTinh.SetBounds(125, 140, 180, 25);
            cboGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ" });
            cboGioiTinh.SelectedIndex = 0;

            Button btnThem = new Button();
            btnThem.Text = "Thêm";
            btnThem.SetBounds(340, 20, 100, 35);
            btnThem.Click += BtnThem_Click;

            Button btnCapNhat = new Button();
            btnCapNhat.Text = "Cập nhật";
            btnCapNhat.SetBounds(450, 20, 100, 35);
            btnCapNhat.Click += BtnCapNhat_Click;

            Button btnXoa = new Button();
            btnXoa.Text = "Xóa";
            btnXoa.SetBounds(560, 20, 100, 35);
            btnXoa.Click += BtnXoa_Click;

            dgv.SetBounds(20, 200, 790, 290);
            dgv.AutoGenerateColumns = true;
            dgv.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;

            ContextMenuStrip menuChuDot = new ContextMenuStrip();

            ToolStripMenuItem xoaDong =
                new ToolStripMenuItem("Xóa dòng");

            ToolStripMenuItem lamMoi =
                new ToolStripMenuItem("Làm mới dữ liệu");

            ToolStripMenuItem xemChiTiet =
                new ToolStripMenuItem("Xem chi tiết");

            xoaDong.Click += (s, e) => XoaSinhVien();
            lamMoi.Click += (s, e) =>
            {
                dgv.Refresh();
                BaoTrangThai?.Invoke("Đã làm mới bảng sinh viên");
            };
            xemChiTiet.Click += (s, e) => XemChiTiet();

            menuChuDot.Items.Add(xoaDong);
            menuChuDot.Items.Add(lamMoi);
            menuChuDot.Items.Add(xemChiTiet);

            dgv.ContextMenuStrip = menuChuDot;

            Controls.AddRange(new Control[]
            {
                lblMa, txtMa,
                lblTen, txtTen,
                lblTuoi, txtTuoi,
                lblGioiTinh, cboGioiTinh,
                btnThem, btnCapNhat, btnXoa,
                dgv
            });
        }

        private void CauHinhDuLieu()
        {
            nguonDuLieu.DataSource = danhSach;
            dgv.DataSource = nguonDuLieu;

            txtMa.DataBindings.Add(
                "Text", nguonDuLieu, "StudentId",
                true, DataSourceUpdateMode.OnPropertyChanged
            );

            txtTen.DataBindings.Add(
                "Text", nguonDuLieu, "FullName",
                true, DataSourceUpdateMode.OnPropertyChanged
            );

            txtTuoi.DataBindings.Add(
                "Text", nguonDuLieu, "Age",
                true, DataSourceUpdateMode.Never
            );

            cboGioiTinh.DataBindings.Add(
                "SelectedItem", nguonDuLieu, "Gender",
                true, DataSourceUpdateMode.OnPropertyChanged
            );
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMa.Text) ||
                string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Nhập mã sinh viên và họ tên trước.");
                return;
            }

            if (!int.TryParse(txtTuoi.Text, out int tuoi) ||
                tuoi < 1 || tuoi > 150)
            {
                MessageBox.Show("Tuổi phải từ 1 đến 150.");
                return;
            }

            string ma = txtMa.Text.Trim();

            foreach (Student sv in danhSach)
            {
                if (sv.StudentId.Equals(
                    ma, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Mã sinh viên đã tồn tại.");
                    return;
                }
            }

            Student sinhVienMoi = new Student
            {
                StudentId = ma,
                FullName = txtTen.Text.Trim(),
                Age = tuoi,
                Gender = cboGioiTinh.SelectedItem?.ToString() ?? "Nam"
            };

            danhSach.Add(sinhVienMoi);
            nguonDuLieu.Position = danhSach.Count - 1;

            BaoTrangThai?.Invoke(
                "Thêm sinh viên thành công: " + sinhVienMoi.FullName
            );
        }

        private void BtnCapNhat_Click(object? sender, EventArgs e)
        {
            if (nguonDuLieu.Current is not Student sv)
            {
                MessageBox.Show("Chọn sinh viên cần cập nhật.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMa.Text) ||
                string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Mã sinh viên và họ tên không được để trống.");
                return;
            }

            if (!int.TryParse(txtTuoi.Text, out int tuoi) ||
                tuoi < 1 || tuoi > 150)
            {
                MessageBox.Show("Tuổi phải từ 1 đến 150.");
                return;
            }

            string ma = txtMa.Text.Trim();

            foreach (Student item in danhSach)
            {
                if (item != sv &&
                    item.StudentId.Equals(
                        ma, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Mã sinh viên đã tồn tại.");
                    return;
                }
            }

            sv.StudentId = ma;
            sv.FullName = txtTen.Text.Trim();
            sv.Age = tuoi;
            sv.Gender = cboGioiTinh.SelectedItem?.ToString() ?? "Nam";

            nguonDuLieu.ResetCurrentItem();

            BaoTrangThai?.Invoke(
                "Cập nhật sinh viên thành công: " + sv.FullName
            );
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            XoaSinhVien();
        }

        private void XoaSinhVien()
        {
            if (nguonDuLieu.Current is not Student sv)
            {
                MessageBox.Show("Chọn sinh viên cần xóa.");
                return;
            }

            string ten = sv.FullName;
            nguonDuLieu.RemoveCurrent();

            BaoTrangThai?.Invoke("Đã xóa sinh viên: " + ten);
        }

        private void XemChiTiet()
        {
            if (nguonDuLieu.Current is not Student sv)
            {
                MessageBox.Show("Chọn một sinh viên.");
                return;
            }

            MessageBox.Show(
                "Mã sinh viên: " + sv.StudentId + "\n" +
                "Họ tên: " + sv.FullName + "\n" +
                "Tuổi: " + sv.Age + "\n" +
                "Giới tính: " + sv.Gender,
                "Chi tiết sinh viên"
            );
        }
    }
}
