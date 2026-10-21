

using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using Bai1_QuanLyThietBi.Models;

namespace Bai1_QuanLyThietBi
{
    public partial class Form1 : Form
    {
        BindingList<Device> danhSach = new BindingList<Device>();
        BindingSource nguonDuLieu = new BindingSource();
        ErrorProvider loi = new ErrorProvider();

        TextBox txtMa = new TextBox();
        TextBox txtTen = new TextBox();
        ComboBox cboLoai = new ComboBox();
        TextBox txtGia = new TextBox();
        NumericUpDown numSoLuong = new NumericUpDown();

        Button btnThem = new Button();
        Button btnCapNhat = new Button();
        Button btnXoa = new Button();
        Button btnLamMoi = new Button();

        DataGridView dgv = new DataGridView();

        public Form1()
        {
            InitializeComponent();

            TaoGiaoDien();

            nguonDuLieu.DataSource = danhSach;
            dgv.DataSource = nguonDuLieu;

            txtMa.Text = TaoMaMoi();
        }

        void TaoGiaoDien()
        {
            Text = "Quản lý danh mục thiết bị điện tử";
            Width = 1000;
            Height = 650;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            TaoNhan("Mã thiết bị:", 25, 25);
            txtMa.SetBounds(140, 25, 220, 28);
            txtMa.ReadOnly = true;
            txtMa.TabStop = false;
            Controls.Add(txtMa);

            TaoNhan("Tên thiết bị:", 25, 70);
            txtTen.SetBounds(140, 70, 220, 28);
            Controls.Add(txtTen);

            TaoNhan("Loại thiết bị:", 25, 115);
            cboLoai.SetBounds(140, 115, 220, 28);
            cboLoai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoai.Items.AddRange(new object[]
            {
                "Máy tính",
                "Điện thoại",
                "Phụ kiện",
                "Khác"
            });
            Controls.Add(cboLoai);

            TaoNhan("Đơn giá:", 410, 25);
            txtGia.SetBounds(510, 25, 220, 28);
            Controls.Add(txtGia);

            TaoNhan("Số lượng:", 410, 70);
            numSoLuong.SetBounds(510, 70, 220, 28);
            numSoLuong.Minimum = 0;
            numSoLuong.Maximum = 1000000;
            numSoLuong.DecimalPlaces = 0;
            numSoLuong.ThousandsSeparator = true;
            Controls.Add(numSoLuong);

            TaoNut(btnThem, "Thêm", 25, BtnThem_Click);
            TaoNut(btnCapNhat, "Cập nhật", 140, BtnCapNhat_Click);
            TaoNut(btnXoa, "Xóa", 255, BtnXoa_Click);
            TaoNut(btnLamMoi, "Làm mới", 370, BtnLamMoi_Click);

            dgv.SetBounds(25, 215, 930, 370);
            dgv.AutoGenerateColumns = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.MultiSelect = false;
            dgv.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible = false;
            dgv.BackgroundColor = System.Drawing.Color.White;

            ThemCot("Ma", "Mã thiết bị");
            ThemCot("Ten", "Tên thiết bị");
            ThemCot("Loai", "Loại thiết bị");
            ThemCot("Gia", "Đơn giá");
            ThemCot("SoLuong", "Số lượng");

            dgv.CellClick += Dgv_CellClick;
            Controls.Add(dgv);

            loi.ContainerControl = this;
        }

        void TaoNhan(string noiDung, int x, int y)
        {
            Label nhan = new Label();
            nhan.Text = noiDung;
            nhan.SetBounds(x, y, 110, 28);
            nhan.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            Controls.Add(nhan);
        }

        void TaoNut(
            Button nut,
            string noiDung,
            int x,
            EventHandler suKien)
        {
            nut.Text = noiDung;
            nut.SetBounds(x, 160, 100, 35);
            nut.Click += suKien;
            Controls.Add(nut);
        }

        void ThemCot(string thuocTinh, string tieuDe)
        {
            DataGridViewTextBoxColumn cot =
                new DataGridViewTextBoxColumn();

            cot.Name = thuocTinh;
            cot.DataPropertyName = thuocTinh;
            cot.HeaderText = tieuDe;
            cot.SortMode = DataGridViewColumnSortMode.NotSortable;

            if (thuocTinh == "Gia")
            {
                cot.DefaultCellStyle.Format = "#,##0 '₫'";
                cot.DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }

            if (thuocTinh == "SoLuong")
            {
                cot.DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;
            }

            dgv.Columns.Add(cot);
        }

        string TaoMaMoi()
        {
            int soLonNhat = 0;

            foreach (Device thietBi in danhSach)
            {
                string ma = thietBi.Ma;

                if (ma.StartsWith("TB", StringComparison.OrdinalIgnoreCase)
                    && int.TryParse(ma.Substring(2), out int so))
                {
                    if (so > soLonNhat)
                    {
                        soLonNhat = so;
                    }
                }
            }

            return "TB" + (soLonNhat + 1).ToString("D3");
        }

        bool KiemTraDuLieu(out decimal gia)
        {
            loi.Clear();
            gia = 0;

            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                loi.SetError(txtTen, "Vui lòng nhập tên thiết bị.");
                txtTen.Focus();
                return false;
            }

            if (cboLoai.SelectedIndex == -1)
            {
                loi.SetError(cboLoai, "Vui lòng chọn loại thiết bị.");
                cboLoai.Focus();
                return false;
            }

            bool giaHopLe =
                decimal.TryParse(
                    txtGia.Text,
                    NumberStyles.Number,
                    CultureInfo.CurrentCulture,
                    out gia)
                ||
                decimal.TryParse(
                    txtGia.Text,
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out gia);

            if (!giaHopLe || gia < 0)
            {
                loi.SetError(
                    txtGia,
                    "Đơn giá phải là số hợp lệ và không âm.");

                txtGia.Focus();
                return false;
            }

            return true;
        }

        void BtnThem_Click(object? sender, EventArgs e)
        {
            if (!KiemTraDuLieu(out decimal gia))
            {
                return;
            }

            Device moi = new Device();

            moi.Ma = TaoMaMoi();
            moi.Ten = txtTen.Text.Trim();
            moi.Loai = cboLoai.Text;
            moi.Gia = gia;
            moi.SoLuong = (int)numSoLuong.Value;

            danhSach.Add(moi);

            nguonDuLieu.ResetBindings(false);

            XoaTrang();

            MessageBox.Show(
                "Thêm thiết bị thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        void BtnCapNhat_Click(object? sender, EventArgs e)
        {
            if (dgv.CurrentRow == null ||
                dgv.CurrentRow.DataBoundItem is not Device thietBi)
            {
                MessageBox.Show(
                    "Vui lòng chọn thiết bị cần cập nhật.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!KiemTraDuLieu(out decimal gia))
            {
                return;
            }

            thietBi.Ten = txtTen.Text.Trim();
            thietBi.Loai = cboLoai.Text;
            thietBi.Gia = gia;
            thietBi.SoLuong = (int)numSoLuong.Value;

            nguonDuLieu.ResetBindings(false);

            MessageBox.Show(
                "Cập nhật thiết bị thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        void BtnXoa_Click(object? sender, EventArgs e)
        {
            if (dgv.CurrentRow == null ||
                dgv.CurrentRow.DataBoundItem is not Device thietBi)
            {
                MessageBox.Show(
                    "Vui lòng chọn thiết bị cần xóa.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult ketQua = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa thiết bị "
                    + thietBi.Ten + " không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (ketQua == DialogResult.Yes)
            {
                danhSach.Remove(thietBi);

                nguonDuLieu.ResetBindings(false);

                XoaTrang();

                MessageBox.Show(
                    "Xóa thiết bị thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            XoaTrang();
        }

        void Dgv_CellClick(
            object? sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (dgv.Rows[e.RowIndex].DataBoundItem is not Device thietBi)
            {
                return;
            }

            txtMa.Text = thietBi.Ma;
            txtTen.Text = thietBi.Ten;
            cboLoai.SelectedItem = thietBi.Loai;
            txtGia.Text = thietBi.Gia.ToString(
                CultureInfo.CurrentCulture);
            numSoLuong.Value = thietBi.SoLuong;

            loi.Clear();
        }

        void XoaTrang()
        {
            txtMa.Text = TaoMaMoi();
            txtTen.Clear();
            cboLoai.SelectedIndex = -1;
            txtGia.Clear();
            numSoLuong.Value = 0;

            loi.Clear();
            dgv.ClearSelection();
            dgv.CurrentCell = null;

            txtTen.Focus();
        }
    }
}



