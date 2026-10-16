
using System;
using System.ComponentModel;
using System.Windows.Forms;
using Bai1_QuanLyThietBi.Models;

namespace Bai1_QuanLyThietBi
{
    public partial class Form1 : Form
    {
        BindingList<Device> danhSach = new BindingList<Device>();
        BindingSource nguonDuLieu = new BindingSource();

        TextBox txtMa = new TextBox();
        TextBox txtTen = new TextBox();
        ComboBox cboLoai = new ComboBox();
        TextBox txtGia = new TextBox();
        TextBox txtSoLuong = new TextBox();

        Button btnThem = new Button();
        Button btnSua = new Button();
        Button btnXoa = new Button();
        Button btnLamMoi = new Button();

        DataGridView dgv = new DataGridView();
        ErrorProvider loi = new ErrorProvider();

        public Form1()
        {
            InitializeComponent();
            TaoGiaoDien();

            nguonDuLieu.DataSource = danhSach;
            dgv.DataSource = nguonDuLieu;
        }

        void TaoGiaoDien()
        {
            Text = "Quản lý danh mục thiết bị điện tử";
            Width = 1000;
            Height = 650;
            StartPosition = FormStartPosition.CenterScreen;

            TaoNhan("Mã thiết bị", 20, 20);
            txtMa.SetBounds(130, 20, 200, 25);
            Controls.Add(txtMa);

            TaoNhan("Tên thiết bị", 20, 60);
            txtTen.SetBounds(130, 60, 200, 25);
            Controls.Add(txtTen);

            TaoNhan("Loại thiết bị", 20, 100);
            cboLoai.SetBounds(130, 100, 200, 25);
            cboLoai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoai.Items.AddRange(new object[]
            {
                "Máy tính", "Điện thoại", "Phụ kiện", "Khác"
            });
            Controls.Add(cboLoai);

            TaoNhan("Giá", 400, 20);
            txtGia.SetBounds(500, 20, 200, 25);
            Controls.Add(txtGia);

            TaoNhan("Số lượng", 400, 60);
            txtSoLuong.SetBounds(500, 60, 200, 25);
            Controls.Add(txtSoLuong);

            TaoNut(btnThem, "Thêm", 20, BtnThem_Click);
            TaoNut(btnSua, "Sửa", 130, BtnSua_Click);
            TaoNut(btnXoa, "Xóa", 240, BtnXoa_Click);
            TaoNut(btnLamMoi, "Làm mới", 350, BtnLamMoi_Click);

            dgv.SetBounds(20, 210, 940, 380);
            dgv.AutoGenerateColumns = false;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            ThemCot("Ma", "Mã thiết bị");
            ThemCot("Ten", "Tên thiết bị");
            ThemCot("Loai", "Loại thiết bị");
            ThemCot("Gia", "Giá");
            ThemCot("SoLuong", "Số lượng");

            dgv.CellClick += Dgv_CellClick;
            Controls.Add(dgv);

            loi.ContainerControl = this;
        }

        void TaoNhan(string noiDung, int x, int y)
        {
            Label nhan = new Label();
            nhan.Text = noiDung;
            nhan.SetBounds(x, y, 100, 25);
            Controls.Add(nhan);
        }

        void TaoNut(
            Button nut,
            string noiDung,
            int x,
            EventHandler suKien)
        {
            nut.Text = noiDung;
            nut.SetBounds(x, 150, 100, 35);
            nut.Click += suKien;
            Controls.Add(nut);
        }

        void ThemCot(string thuocTinh, string tieuDe)
        {
            DataGridViewTextBoxColumn cot =
                new DataGridViewTextBoxColumn();

            cot.DataPropertyName = thuocTinh;
            cot.HeaderText = tieuDe;
            cot.Name = thuocTinh;

            if (thuocTinh == "Gia")
                cot.DefaultCellStyle.Format = "N0";

            dgv.Columns.Add(cot);
        }

        bool KiemTraDuLieu()
        {
            loi.Clear();

            if (string.IsNullOrWhiteSpace(txtMa.Text))
            {
                loi.SetError(txtMa, "Hãy nhập mã thiết bị");
                txtMa.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                loi.SetError(txtTen, "Hãy nhập tên thiết bị");
                txtTen.Focus();
                return false;
            }

            if (cboLoai.SelectedIndex == -1)
            {
                loi.SetError(cboLoai, "Hãy chọn loại thiết bị");
                cboLoai.Focus();
                return false;
            }

            decimal gia;
            if (!decimal.TryParse(txtGia.Text, out gia) || gia < 0)
            {
                loi.SetError(txtGia, "Giá phải là số không âm");
                txtGia.Focus();
                return false;
            }

            int soLuong;
            if (!int.TryParse(txtSoLuong.Text, out soLuong)
                || soLuong < 0)
            {
                loi.SetError(txtSoLuong,
                    "Số lượng phải là số nguyên không âm");
                txtSoLuong.Focus();
                return false;
            }

            return true;
        }

        void BtnThem_Click(object? sender, EventArgs e)
        {
            if (!KiemTraDuLieu())
                return;

            foreach (Device thietBi in danhSach)
            {
                if (thietBi.Ma == txtMa.Text.Trim())
                {
                    MessageBox.Show("Mã thiết bị đã tồn tại!");
                    return;
                }
            }

            Device moi = new Device();
            moi.Ma = txtMa.Text.Trim();
            moi.Ten = txtTen.Text.Trim();
            moi.Loai = cboLoai.Text;
            moi.Gia = decimal.Parse(txtGia.Text);
            moi.SoLuong = int.Parse(txtSoLuong.Text);

            danhSach.Add(moi);
            XoaTrang();

            MessageBox.Show("Thêm thiết bị thành công!");
        }

        void BtnSua_Click(object? sender, EventArgs e)
        {
            if (dgv.CurrentRow == null ||
                dgv.CurrentRow.DataBoundItem is not Device thietBi)
            {
                MessageBox.Show("Hãy chọn thiết bị cần sửa!");
                return;
            }

            if (!KiemTraDuLieu())
                return;

            foreach (Device khac in danhSach)
            {
                if (khac != thietBi &&
                    khac.Ma == txtMa.Text.Trim())
                {
                    MessageBox.Show("Mã thiết bị đã tồn tại!");
                    return;
                }
            }

            thietBi.Ma = txtMa.Text.Trim();
            thietBi.Ten = txtTen.Text.Trim();
            thietBi.Loai = cboLoai.Text;
            thietBi.Gia = decimal.Parse(txtGia.Text);
            thietBi.SoLuong = int.Parse(txtSoLuong.Text);

            nguonDuLieu.ResetBindings(false);
            MessageBox.Show("Sửa thiết bị thành công!");
        }

        void BtnXoa_Click(object? sender, EventArgs e)
        {
            if (dgv.CurrentRow == null ||
                dgv.CurrentRow.DataBoundItem is not Device thietBi)
            {
                MessageBox.Show("Hãy chọn thiết bị cần xóa!");
                return;
            }

            DialogResult ketQua = MessageBox.Show(
                "Bạn có chắc muốn xóa thiết bị này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (ketQua == DialogResult.Yes)
            {
                danhSach.Remove(thietBi);
                XoaTrang();
            }
        }

        void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            XoaTrang();
        }

        void Dgv_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            Device thietBi =
                (Device)dgv.Rows[e.RowIndex].DataBoundItem;

            txtMa.Text = thietBi.Ma;
            txtTen.Text = thietBi.Ten;
            cboLoai.Text = thietBi.Loai;
            txtGia.Text = thietBi.Gia.ToString();
            txtSoLuong.Text = thietBi.SoLuong.ToString();
        }

        void XoaTrang()
        {
            txtMa.Clear();
            txtTen.Clear();
            cboLoai.SelectedIndex = -1;
            txtGia.Clear();
            txtSoLuong.Clear();
            loi.Clear();
            dgv.ClearSelection();
            txtMa.Focus();
        }
    }
}
S
