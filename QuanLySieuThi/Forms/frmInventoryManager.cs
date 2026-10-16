
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuanLySieuThi.Models;

namespace QuanLySieuThi.Forms
{
    public class frmInventoryManager : Form
    {
        BindingList<Product> danhSach = new BindingList<Product>();
        BindingSource nguonDuLieu = new BindingSource();
        ErrorProvider baoLoi = new ErrorProvider();
        System.Windows.Forms.Timer dongHo =
    new System.Windows.Forms.Timer();

        TextBox txtMa = new TextBox();
        TextBox txtTen = new TextBox();
        TextBox txtGia = new TextBox();
        ComboBox cboLoai = new ComboBox();
        NumericUpDown numSoLuong = new NumericUpDown();

        PictureBox picAnh = new PictureBox();
        DataGridView dgvSanPham = new DataGridView();

        Button btnThem = new Button();
        Button btnSua = new Button();
        Button btnXoa = new Button();
        Button btnLamMoi = new Button();
        Button btnChonAnh = new Button();

        ToolStripStatusLabel lblTongSoLuong =
            new ToolStripStatusLabel();

        ToolStripStatusLabel lblTongGiaTri =
            new ToolStripStatusLabel();

        ToolStripStatusLabel lblThoiGian =
            new ToolStripStatusLabel();

        string duongDanAnh = "";

        public frmInventoryManager()
        {
            TaoGiaoDien();
            GanDuLieu();
            GanSuKien();

            txtMa.Text = TaoMaMoi();
            CapNhatThongKe();

            dongHo.Interval = 1000;
            dongHo.Tick += (s, e) =>
            {
                lblThoiGian.Text =
                    "Thời gian: " +
                    DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            };

            dongHo.Start();

            FormClosing += FrmInventoryManager_FormClosing;
        }

        private void TaoGiaoDien()
        {
            Text = "Quản lý kho hàng siêu thị";
            Size = new Size(1100, 700);
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(900, 600);

            // MENU
            MenuStrip menu = new MenuStrip();

            ToolStripMenuItem heThong =
                new ToolStripMenuItem("Hệ thống");

            ToolStripMenuItem moFile =
                new ToolStripMenuItem("Mở file CSV");

            ToolStripMenuItem luuBaoCao =
                new ToolStripMenuItem("Lưu báo cáo");

            ToolStripMenuItem thoat =
                new ToolStripMenuItem("Thoát");

            moFile.Click += (s, e) => NhapCSV();
            luuBaoCao.Click += (s, e) => XuatCSV();
            thoat.Click += (s, e) => Close();

            heThong.DropDownItems.Add(moFile);
            heThong.DropDownItems.Add(luuBaoCao);
            heThong.DropDownItems.Add(new ToolStripSeparator());
            heThong.DropDownItems.Add(thoat);

            ToolStripMenuItem congCu =
                new ToolStripMenuItem("Công cụ");

            ToolStripMenuItem nhapCSV =
                new ToolStripMenuItem("Nhập CSV");

            ToolStripMenuItem xuatCSV =
                new ToolStripMenuItem("Xuất CSV");

            nhapCSV.Click += (s, e) => NhapCSV();
            xuatCSV.Click += (s, e) => XuatCSV();

            congCu.DropDownItems.Add(nhapCSV);
            congCu.DropDownItems.Add(xuatCSV);

            menu.Items.Add(heThong);
            menu.Items.Add(congCu);

            // TOOLSTRIP
            ToolStrip congCuNhanh = new ToolStrip();

            ToolStripButton nutThemNhanh =
                new ToolStripButton("Thêm sản phẩm");

            ToolStripButton nutNhapNhanh =
                new ToolStripButton("Nhập CSV");

            ToolStripButton nutXuatNhanh =
                new ToolStripButton("Xuất CSV");

            ToolStripButton nutThoatNhanh =
                new ToolStripButton("Thoát");

            nutThemNhanh.Click += (s, e) => ThemSanPham();
            nutNhapNhanh.Click += (s, e) => NhapCSV();
            nutXuatNhanh.Click += (s, e) => XuatCSV();
            nutThoatNhanh.Click += (s, e) => Close();

            congCuNhanh.Items.Add(nutThemNhanh);
            congCuNhanh.Items.Add(nutNhapNhanh);
            congCuNhanh.Items.Add(nutXuatNhanh);
            congCuNhanh.Items.Add(nutThoatNhanh);

            // STATUSSTRIP
            StatusStrip trangThai = new StatusStrip();

            lblTongSoLuong.Text = "Tổng số lượng: 0";
            lblTongGiaTri.Text = "Tổng giá trị: 0";
            lblThoiGian.Text = "Thời gian:";

            lblTongSoLuong.Spring = true;
            lblTongSoLuong.TextAlign = ContentAlignment.MiddleLeft;

            lblTongGiaTri.Spring = true;
            lblTongGiaTri.TextAlign = ContentAlignment.MiddleLeft;

            lblThoiGian.Spring = true;
            lblThoiGian.TextAlign = ContentAlignment.MiddleRight;

            trangThai.Items.Add(lblTongSoLuong);
            trangThai.Items.Add(lblTongGiaTri);
            trangThai.Items.Add(lblThoiGian);

            // KHU VỰC CHÍNH
            Panel khuVucChinh = new Panel();
            khuVucChinh.Dock = DockStyle.Fill;

            // KHU VỰC NHẬP THÔNG TIN
            GroupBox nhomThongTin = new GroupBox();
            nhomThongTin.Text = "Thông tin sản phẩm";
            nhomThongTin.Dock = DockStyle.Top;
            nhomThongTin.Height = 235;

            TaoNhan(nhomThongTin, "Mã sản phẩm:", 20, 30);
            TaoNhan(nhomThongTin, "Tên sản phẩm:", 20, 75);
            TaoNhan(nhomThongTin, "Loại sản phẩm:", 20, 120);
            TaoNhan(nhomThongTin, "Giá:", 390, 30);
            TaoNhan(nhomThongTin, "Số lượng:", 390, 75);
            TaoNhan(nhomThongTin, "Ảnh sản phẩm:", 700, 30);

            txtMa.Location = new Point(135, 28);
            txtMa.Size = new Size(220, 25);
            txtMa.ReadOnly = true;

            txtTen.Location = new Point(135, 73);
            txtTen.Size = new Size(220, 25);

            cboLoai.Location = new Point(135, 118);
            cboLoai.Size = new Size(220, 25);
            cboLoai.DropDownStyle = ComboBoxStyle.DropDownList;

            cboLoai.Items.AddRange(new object[]
            {
                "Điện thoại",
                "Máy tính",
                "Phụ kiện",
                "Đồ gia dụng",
                "Khác"
            });

            txtGia.Location = new Point(470, 28);
            txtGia.Size = new Size(170, 25);

            numSoLuong.Location = new Point(470, 73);
            numSoLuong.Size = new Size(170, 25);
            numSoLuong.Minimum = 0;
            numSoLuong.Maximum = 1000000;

            picAnh.Location = new Point(810, 25);
            picAnh.Size = new Size(140, 105);
            picAnh.BorderStyle = BorderStyle.FixedSingle;
            picAnh.SizeMode = PictureBoxSizeMode.Zoom;

            btnChonAnh.Text = "Chọn ảnh";
            btnChonAnh.Location = new Point(810, 135);
            btnChonAnh.Size = new Size(140, 30);

            btnThem = TaoNut("Thêm", 20, 175);
            btnSua = TaoNut("Sửa", 120, 175);
            btnXoa = TaoNut("Xóa", 220, 175);
            btnLamMoi = TaoNut("Làm mới", 320, 175);

            nhomThongTin.Controls.Add(txtMa);
            nhomThongTin.Controls.Add(txtTen);
            nhomThongTin.Controls.Add(cboLoai);
            nhomThongTin.Controls.Add(txtGia);
            nhomThongTin.Controls.Add(numSoLuong);
            nhomThongTin.Controls.Add(picAnh);
            nhomThongTin.Controls.Add(btnChonAnh);
            nhomThongTin.Controls.Add(btnThem);
            nhomThongTin.Controls.Add(btnSua);
            nhomThongTin.Controls.Add(btnXoa);
            nhomThongTin.Controls.Add(btnLamMoi);

            // BẢNG SẢN PHẨM
            dgvSanPham.Dock = DockStyle.Fill;
            dgvSanPham.AutoGenerateColumns = false;
            dgvSanPham.ReadOnly = true;
            dgvSanPham.AllowUserToAddRows = false;
            dgvSanPham.AllowUserToDeleteRows = false;
            dgvSanPham.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvSanPham.MultiSelect = false;
            dgvSanPham.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvSanPham.RowHeadersVisible = false;

            ThemCot("Ma", "Mã sản phẩm");
            ThemCot("Ten", "Tên sản phẩm");
            ThemCot("Loai", "Loại sản phẩm");

            DataGridViewTextBoxColumn cotGia =
                new DataGridViewTextBoxColumn();

            cotGia.Name = "Gia";
            cotGia.HeaderText = "Giá";
            cotGia.DataPropertyName = "Gia";
            cotGia.DefaultCellStyle.Format = "N0";
            cotGia.FillWeight = 80;
            dgvSanPham.Columns.Add(cotGia);

            ThemCot("SoLuong", "Số lượng");
            ThemCot("DuongDanAnh", "Đường dẫn ảnh");

            khuVucChinh.Controls.Add(dgvSanPham);
            khuVucChinh.Controls.Add(nhomThongTin);

            // THÊM VÀO FORM
            Controls.Add(khuVucChinh);
            Controls.Add(congCuNhanh);
            Controls.Add(menu);
            Controls.Add(trangThai);

            MainMenuStrip = menu;

            baoLoi.ContainerControl = this;
        }

        private void TaoNhan(
            Control cha, string noiDung, int x, int y)
        {
            Label nhan = new Label();

            nhan.Text = noiDung;
            nhan.Location = new Point(x, y + 3);
            nhan.AutoSize = true;

            cha.Controls.Add(nhan);
        }

        private Button TaoNut(string noiDung, int x, int y)
        {
            Button nut = new Button();

            nut.Text = noiDung;
            nut.Location = new Point(x, y);
            nut.Size = new Size(90, 32);

            return nut;
        }

        private void ThemCot(string tenCot, string tieuDe)
        {
            DataGridViewTextBoxColumn cot =
                new DataGridViewTextBoxColumn();

            cot.Name = tenCot;
            cot.HeaderText = tieuDe;
            cot.DataPropertyName = tenCot;

            dgvSanPham.Columns.Add(cot);
        }

        private void GanDuLieu()
        {
            nguonDuLieu.DataSource = danhSach;
            dgvSanPham.DataSource = nguonDuLieu;
        }

        private void GanSuKien()
        {
            btnThem.Click += (s, e) => ThemSanPham();
            btnSua.Click += (s, e) => SuaSanPham();
            btnXoa.Click += (s, e) => XoaSanPham();
            btnLamMoi.Click += (s, e) => LamMoi();
            btnChonAnh.Click += (s, e) => ChonAnh();

            dgvSanPham.SelectionChanged += (s, e) =>
            {
                if (dgvSanPham.CurrentRow == null)
                    return;

                if (dgvSanPham.CurrentRow.DataBoundItem is Product sp)
                {
                    txtMa.Text = sp.Ma;
                    txtTen.Text = sp.Ten;
                    cboLoai.SelectedItem = sp.Loai;
                    txtGia.Text = sp.Gia.ToString(
                        CultureInfo.InvariantCulture);
                    numSoLuong.Value = sp.SoLuong;

                    duongDanAnh = sp.DuongDanAnh;
                    HienThiAnh(duongDanAnh);
                }
            };
        }

        private string TaoMaMoi()
        {
            int soLonNhat = 0;

            foreach (Product sp in danhSach)
            {
                if (sp.Ma.StartsWith("SP"))
                {
                    string phanSo = sp.Ma.Substring(2);

                    if (int.TryParse(phanSo, out int so))
                    {
                        if (so > soLonNhat)
                            soLonNhat = so;
                    }
                }
            }

            return "SP" + (soLonNhat + 1).ToString("D3");
        }

        private bool KiemTra()
        {
            baoLoi.Clear();
            bool hopLe = true;

            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                baoLoi.SetError(txtTen, "Vui lòng nhập tên sản phẩm");
                hopLe = false;
            }

            if (cboLoai.SelectedIndex < 0)
            {
                baoLoi.SetError(cboLoai, "Vui lòng chọn loại sản phẩm");
                hopLe = false;
            }

            if (!decimal.TryParse(
                txtGia.Text.Trim(),
                NumberStyles.Number,
                CultureInfo.CurrentCulture,
                out decimal gia))
            {
                if (!decimal.TryParse(
                    txtGia.Text.Trim(),
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out gia))
                {
                    baoLoi.SetError(txtGia, "Giá không hợp lệ");
                    hopLe = false;
                }
            }

            if (decimal.TryParse(
                txtGia.Text.Trim(),
                NumberStyles.Number,
                CultureInfo.CurrentCulture,
                out decimal giaKiemTra)
                && giaKiemTra < 0)
            {
                baoLoi.SetError(txtGia, "Giá không được âm");
                hopLe = false;
            }

            if (decimal.TryParse(
                txtGia.Text.Trim(),
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out decimal giaKiemTra2)
                && giaKiemTra2 < 0)
            {
                baoLoi.SetError(txtGia, "Giá không được âm");
                hopLe = false;
            }

            return hopLe;
        }

        private bool LayGia(out decimal gia)
        {
            if (decimal.TryParse(
                txtGia.Text.Trim(),
                NumberStyles.Number,
                CultureInfo.CurrentCulture,
                out gia))
                return gia >= 0;

            if (decimal.TryParse(
                txtGia.Text.Trim(),
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out gia))
                return gia >= 0;

            return false;
        }

        private void ThemSanPham()
        {
            if (!KiemTra())
                return;

            if (!LayGia(out decimal gia))
            {
                MessageBox.Show("Giá sản phẩm không hợp lệ.");
                return;
            }

            Product sp = new Product();

            sp.Ma = TaoMaMoi();
            sp.Ten = txtTen.Text.Trim();
            sp.Loai = cboLoai.Text;
            sp.Gia = gia;
            sp.SoLuong = (int)numSoLuong.Value;
            sp.DuongDanAnh = duongDanAnh;

            danhSach.Add(sp);

            CapNhatThongKe();
            LamMoi();
        }

        private void SuaSanPham()
        {
            if (dgvSanPham.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa.");
                return;
            }

            if (!KiemTra())
                return;

            if (!LayGia(out decimal gia))
            {
                MessageBox.Show("Giá sản phẩm không hợp lệ.");
                return;
            }

            if (dgvSanPham.CurrentRow.DataBoundItem is not Product sp)
                return;

            sp.Ten = txtTen.Text.Trim();
            sp.Loai = cboLoai.Text;
            sp.Gia = gia;
            sp.SoLuong = (int)numSoLuong.Value;
            sp.DuongDanAnh = duongDanAnh;

            nguonDuLieu.ResetBindings(false);
            CapNhatThongKe();
        }

        private void XoaSanPham()
        {
            if (dgvSanPham.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa.");
                return;
            }

            DialogResult ketQua = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa sản phẩm này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (ketQua != DialogResult.Yes)
                return;

            if (dgvSanPham.CurrentRow.DataBoundItem is Product sp)
            {
                danhSach.Remove(sp);
            }

            CapNhatThongKe();
            LamMoi();
        }

        private void LamMoi()
        {
            txtMa.Text = TaoMaMoi();
            txtTen.Clear();
            txtGia.Clear();
            cboLoai.SelectedIndex = -1;
            numSoLuong.Value = 0;

            duongDanAnh = "";
            picAnh.Image = null;

            baoLoi.Clear();
            dgvSanPham.ClearSelection();
        }

        private void ChonAnh()
        {
            using (OpenFileDialog hopThoai = new OpenFileDialog())
            {
                hopThoai.Title = "Chọn ảnh sản phẩm";
                hopThoai.Filter =
                    "Tệp ảnh|*.jpg;*.jpeg;*.png;*.bmp";

                if (hopThoai.ShowDialog() == DialogResult.OK)
                {
                    duongDanAnh = hopThoai.FileName;
                    HienThiAnh(duongDanAnh);
                }
            }
        }

        private void HienThiAnh(string duongDan)
        {
            if (picAnh.Image != null)
            {
                Image anhCu = picAnh.Image;
                picAnh.Image = null;
                anhCu.Dispose();
            }

            if (string.IsNullOrWhiteSpace(duongDan)
                || !File.Exists(duongDan))
                return;

            try
            {
                using (Image anhGoc = Image.FromFile(duongDan))
                {
                    picAnh.Image = new Bitmap(anhGoc);
                }
            }
            catch
            {
                picAnh.Image = null;
            }
        }

        private void XuatCSV()
        {
            using (SaveFileDialog hopThoai = new SaveFileDialog())
            {
                hopThoai.Title = "Lưu báo cáo kho hàng";
                hopThoai.Filter = "Tệp CSV|*.csv";
                hopThoai.FileName = "BaoCaoKhoHang.csv";

                if (hopThoai.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    using (StreamWriter ghi = new StreamWriter(
                        hopThoai.FileName, false, Encoding.UTF8))
                    {
                        ghi.WriteLine(
                            "Ma;Ten;Loai;Gia;SoLuong;DuongDanAnh");

                        foreach (Product sp in danhSach)
                        {
                            ghi.WriteLine(
                                Csv(sp.Ma) + ";" +
                                Csv(sp.Ten) + ";" +
                                Csv(sp.Loai) + ";" +
                                sp.Gia.ToString(CultureInfo.InvariantCulture) + ";" +
                                sp.SoLuong + ";" +
                                Csv(sp.DuongDanAnh));
                        }
                    }

                    MessageBox.Show("Xuất CSV thành công!");
                }
                catch (Exception loi)
                {
                    MessageBox.Show(
                        "Không thể xuất file: " + loi.Message);
                }
            }
        }

        private string Csv(string noiDung)
        {
            noiDung ??= "";

            return "\"" + noiDung.Replace("\"", "\"\"") + "\"";
        }

        private void NhapCSV()
        {
            using (OpenFileDialog hopThoai = new OpenFileDialog())
            {
                hopThoai.Title = "Chọn file CSV";
                hopThoai.Filter = "Tệp CSV|*.csv";

                if (hopThoai.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    string[] cacDong = File.ReadAllLines(
                        hopThoai.FileName, Encoding.UTF8);

                    if (cacDong.Length <= 1)
                    {
                        MessageBox.Show("File CSV không có dữ liệu.");
                        return;
                    }

                    BindingList<Product> duLieuMoi =
                        new BindingList<Product>();

                    for (int i = 1; i < cacDong.Length; i++)
                    {
                        if (string.IsNullOrWhiteSpace(cacDong[i]))
                            continue;

                        string[] cot = TachCSV(cacDong[i]);

                        if (cot.Length < 6)
                            continue;

                        if (!decimal.TryParse(
                            cot[3],
                            NumberStyles.Number,
                            CultureInfo.InvariantCulture,
                            out decimal gia))
                            continue;

                        if (!int.TryParse(cot[4], out int soLuong))
                            continue;

                        Product sp = new Product();

                        sp.Ma = cot[0];
                        sp.Ten = cot[1];
                        sp.Loai = cot[2];
                        sp.Gia = gia;
                        sp.SoLuong = soLuong;
                        sp.DuongDanAnh = cot[5];

                        duLieuMoi.Add(sp);
                    }

                    danhSach.Clear();

                    foreach (Product sp in duLieuMoi)
                    {
                        danhSach.Add(sp);
                    }

                    CapNhatThongKe();
                    LamMoi();

                    MessageBox.Show(
                        "Đã nhập " + duLieuMoi.Count +
                        " sản phẩm từ CSV.");
                }
                catch (Exception loi)
                {
                    MessageBox.Show(
                        "Không thể nhập file: " + loi.Message);
                }
            }
        }

        private string[] TachCSV(string dong)
        {
            var ketQua = new System.Collections.Generic.List<string>();
            StringBuilder noiDung = new StringBuilder();
            bool dangTrongNgoacKep = false;

            for (int i = 0; i < dong.Length; i++)
            {
                char kyTu = dong[i];

                if (kyTu == '"')
                {
                    if (dangTrongNgoacKep
                        && i + 1 < dong.Length
                        && dong[i + 1] == '"')
                    {
                        noiDung.Append('"');
                        i++;
                    }
                    else
                    {
                        dangTrongNgoacKep = !dangTrongNgoacKep;
                    }
                }
                else if (kyTu == ';' && !dangTrongNgoacKep)
                {
                    ketQua.Add(noiDung.ToString());
                    noiDung.Clear();
                }
                else
                {
                    noiDung.Append(kyTu);
                }
            }

            ketQua.Add(noiDung.ToString());

            return ketQua.ToArray();
        }

        private void CapNhatThongKe()
        {
            int tongSoLuong = danhSach.Sum(sp => sp.SoLuong);

            decimal tongGiaTri = danhSach.Sum(
                sp => sp.Gia * sp.SoLuong);

            lblTongSoLuong.Text =
                "Tổng số lượng: " + tongSoLuong;

            lblTongGiaTri.Text =
                "Tổng giá trị: " + tongGiaTri.ToString("N0");
        }

        private void FrmInventoryManager_FormClosing(
            object? sender, FormClosingEventArgs e)
        {
            DialogResult ketQua = MessageBox.Show(
                "Bạn có muốn thoát chương trình không?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (ketQua != DialogResult.Yes)
            {
                e.Cancel = true;
            }
            else
            {
                dongHo.Stop();
            }
        }
    }
}
