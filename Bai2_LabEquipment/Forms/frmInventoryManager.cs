
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bai2_LabEquipment.Models;

namespace Bai2_LabEquipment.Forms
{
    public class frmInventoryManager : Form
    {
        private BindingList<Product> danhSach = new BindingList<Product>();
        private BindingSource nguonDuLieu = new BindingSource();

        private TextBox txtMa = new TextBox();
        private TextBox txtTen = new TextBox();
        private TextBox txtDonGia = new TextBox();

        private ComboBox cboLoai = new ComboBox();

        private NumericUpDown numSoLuong = new NumericUpDown();

        private PictureBox picAnh = new PictureBox();

        private DataGridView dgv = new DataGridView();

        private Button btnChonAnh = new Button();
        private Button btnThem = new Button();
        private Button btnSua = new Button();
        private Button btnXoa = new Button();
        private Button btnLamMoi = new Button();

        private Label lblThongKe = new Label();

        private ErrorProvider loi = new ErrorProvider();

        private string duongDanAnh = "";

        public frmInventoryManager()
        {
            Text = "QUẢN LÝ KHO SẢN PHẨM";
            Size = new Size(1100, 700);
            StartPosition = FormStartPosition.CenterScreen;

            KhoiTaoGiaoDien();
            TaoBangDuLieu();
            HienThiDuLieu();
        }

        private void KhoiTaoGiaoDien()
        {
            TableLayoutPanel bangChinh = new TableLayoutPanel();

            bangChinh.Dock = DockStyle.Fill;
            bangChinh.RowCount = 3;
            bangChinh.ColumnCount = 1;

            bangChinh.RowStyles.Add(new RowStyle(SizeType.Absolute, 270));
            bangChinh.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            bangChinh.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            Controls.Add(bangChinh);

            Panel bangNhap = new Panel();
            bangNhap.Dock = DockStyle.Fill;

            bangChinh.Controls.Add(bangNhap, 0, 0);

            TaoOThongTin(bangNhap);

            Panel bangBang = new Panel();
            bangBang.Dock = DockStyle.Fill;

            bangChinh.Controls.Add(bangBang, 0, 1);

            dgv.Dock = DockStyle.Fill;
            dgv.AutoGenerateColumns = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = false;

            bangBang.Controls.Add(dgv);

            lblThongKe.Dock = DockStyle.Fill;
            lblThongKe.TextAlign = ContentAlignment.MiddleLeft;
            lblThongKe.Font = new Font("Arial", 11, FontStyle.Bold);

            bangChinh.Controls.Add(lblThongKe, 0, 2);
        }

        private void TaoOThongTin(Panel bangNhap)
        {
            TaoNhan(bangNhap, "Mã sản phẩm:", 20, 20);
            txtMa.Location = new Point(150, 20);
            txtMa.Width = 200;
            txtMa.ReadOnly = true;
            bangNhap.Controls.Add(txtMa);

            TaoNhan(bangNhap, "Tên sản phẩm:", 20, 60);
            txtTen.Location = new Point(150, 60);
            txtTen.Width = 200;
            bangNhap.Controls.Add(txtTen);

            TaoNhan(bangNhap, "Loại sản phẩm:", 20, 100);
            cboLoai.Location = new Point(150, 100);
            cboLoai.Width = 200;
            cboLoai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoai.Items.AddRange(new object[]
            {
                "Điện tử",
                "Gia dụng",
                "Văn phòng phẩm",
                "Khác"
            });
            bangNhap.Controls.Add(cboLoai);

            TaoNhan(bangNhap, "Đơn giá:", 20, 140);
            txtDonGia.Location = new Point(150, 140);
            txtDonGia.Width = 200;
            bangNhap.Controls.Add(txtDonGia);

            TaoNhan(bangNhap, "Số lượng:", 20, 180);
            numSoLuong.Location = new Point(150, 180);
            numSoLuong.Width = 200;
            numSoLuong.Maximum = 100000;
            bangNhap.Controls.Add(numSoLuong);

            picAnh.Location = new Point(400, 20);
            picAnh.Size = new Size(150, 150);
            picAnh.BorderStyle = BorderStyle.FixedSingle;
            picAnh.SizeMode = PictureBoxSizeMode.Zoom;
            bangNhap.Controls.Add(picAnh);

            btnChonAnh.Text = "Chọn ảnh";
            btnChonAnh.Location = new Point(400, 180);
            btnChonAnh.Click += BtnChonAnh_Click;
            bangNhap.Controls.Add(btnChonAnh);

            TaoNut(bangNhap, btnThem, "Thêm", 600, 30);
            TaoNut(bangNhap, btnSua, "Sửa", 600, 80);
            TaoNut(bangNhap, btnXoa, "Xóa", 600, 130);
            TaoNut(bangNhap, btnLamMoi, "Làm mới", 600, 180);

            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += BtnLamMoi_Click;
        }

        private void TaoNhan(Panel bang, string noiDung, int x, int y)
        {
            Label lbl = new Label();

            lbl.Text = noiDung;
            lbl.Location = new Point(x, y);
            lbl.Size = new Size(120, 25);

            bang.Controls.Add(lbl);
        }

        private void TaoNut(Panel bang, Button nut, string noiDung, int x, int y)
        {
            nut.Text = noiDung;
            nut.Location = new Point(x, y);
            nut.Size = new Size(110, 35);

            bang.Controls.Add(nut);
        }

        private void TaoBangDuLieu()
        {
            dgv.Columns.Add("MaSP", "Mã sản phẩm");
            dgv.Columns.Add("TenSP", "Tên sản phẩm");
            dgv.Columns.Add("Loai", "Loại");
            dgv.Columns.Add("DonGia", "Đơn giá");
            dgv.Columns.Add("SoLuong", "Số lượng");
            dgv.Columns.Add("TotalValue", "Tổng giá trị");

            dgv.Columns["MaSP"]!.DataPropertyName = "MaSP";
            dgv.Columns["TenSP"]!.DataPropertyName = "TenSP";
            dgv.Columns["Loai"]!.DataPropertyName = "Loai";
            dgv.Columns["DonGia"]!.DataPropertyName = "DonGia";
            dgv.Columns["SoLuong"]!.DataPropertyName = "SoLuong";
            dgv.Columns["TotalValue"]!.DataPropertyName = "TotalValue";

            dgv.DataSource = nguonDuLieu;

            dgv.SelectionChanged += Dgv_SelectionChanged;
        }

        private void HienThiDuLieu()
        {
            nguonDuLieu.DataSource = danhSach;

            lblThongKe.Text =
                "Tổng số sản phẩm: " + danhSach.Count +
                " | Tổng số lượng: " + danhSach.Sum(x => x.SoLuong) +
                " | Tổng giá trị kho: " +
                danhSach.Sum(x => x.TotalValue).ToString("N0") + " VNĐ";
        }

        private void BtnChonAnh_Click(object? sender, EventArgs e)
        {
            OpenFileDialog hopThoai = new OpenFileDialog();

            hopThoai.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (hopThoai.ShowDialog() == DialogResult.OK)
            {
                duongDanAnh = hopThoai.FileName;

                using (Image anh = Image.FromFile(duongDanAnh))
                {
                    picAnh.Image = new Bitmap(anh);
                }
            }
        }

        private bool KiemTraDuLieu()
        {
            loi.Clear();

            if (txtTen.Text.Trim() == "")
            {
                loi.SetError(txtTen, "Vui lòng nhập tên sản phẩm");
                return false;
            }

            if (cboLoai.SelectedIndex == -1)
            {
                loi.SetError(cboLoai, "Vui lòng chọn loại sản phẩm");
                return false;
            }

            if (!decimal.TryParse(txtDonGia.Text, out decimal donGia)
                || donGia <= 0)
            {
                loi.SetError(txtDonGia, "Đơn giá phải lớn hơn 0");
                return false;
            }

            return true;
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            if (!KiemTraDuLieu())
            {
                return;
            }

            Product sanPham = new Product();

            sanPham.MaSP = "SP" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            sanPham.TenSP = txtTen.Text.Trim();
            sanPham.Loai = cboLoai.Text;
            sanPham.DonGia = decimal.Parse(txtDonGia.Text);
            sanPham.SoLuong = (int)numSoLuong.Value;
            sanPham.ImagePath = duongDanAnh;

            danhSach.Add(sanPham);

            HienThiDuLieu();
            LamMoi();
            
            MessageBox.Show("Thêm sản phẩm thành công!");
        }

        private void BtnSua_Click(object? sender, EventArgs e)
        {
            Product? sanPham = LaySanPhamDangChon();

            if (sanPham == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa");
                return;
            }

            if (!KiemTraDuLieu())
            {
                return;
            }

            sanPham.TenSP = txtTen.Text.Trim();
            sanPham.Loai = cboLoai.Text;
            sanPham.DonGia = decimal.Parse(txtDonGia.Text);
            sanPham.SoLuong = (int)numSoLuong.Value;
            sanPham.ImagePath = duongDanAnh;

            dgv.Refresh();
            HienThiDuLieu();

            MessageBox.Show("Cập nhật sản phẩm thành công!");
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            Product? sanPham = LaySanPhamDangChon();

            if (sanPham == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa");
                return;
            }

            DialogResult ketQua = MessageBox.Show(
                "Bạn có chắc muốn xóa sản phẩm này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (ketQua == DialogResult.Yes)
            {
                danhSach.Remove(sanPham);
                HienThiDuLieu();
                LamMoi();
            }
        }

        private void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            LamMoi();
        }

        private void LamMoi()
        {
            txtMa.Clear();
            txtTen.Clear();
            txtDonGia.Clear();

            cboLoai.SelectedIndex = -1;

            numSoLuong.Value = 0;

            picAnh.Image = null;
            duongDanAnh = "";

            dgv.ClearSelection();
            loi.Clear();
        }

        private Product? LaySanPhamDangChon()
        {
            if (dgv.CurrentRow == null)
            {
                return null;
            }

            return dgv.CurrentRow.DataBoundItem as Product;
        }

        private void Dgv_SelectionChanged(object? sender, EventArgs e)
        {
            Product? sanPham = LaySanPhamDangChon();

            if (sanPham == null)
            {
                return;
            }

            txtMa.Text = sanPham.MaSP;
            txtTen.Text = sanPham.TenSP;
            cboLoai.Text = sanPham.Loai;
            txtDonGia.Text = sanPham.DonGia.ToString();
            numSoLuong.Value = sanPham.SoLuong;

            duongDanAnh = sanPham.ImagePath;

            if (File.Exists(duongDanAnh))
            {
                using (Image anh = Image.FromFile(duongDanAnh))
                {
                    picAnh.Image = new Bitmap(anh);
                }
            }
            else
            {
                picAnh.Image = null;
            }
        }
    }
}
