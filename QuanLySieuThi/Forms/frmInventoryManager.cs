
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
    System.Windows.Forms.Timer dongHo = new System.Windows.Forms.Timer();

        TextBox txtMa = new TextBox();
        TextBox txtTen = new TextBox();
        TextBox txtGia = new TextBox();
        ComboBox cboLoai = new ComboBox();
        NumericUpDown numSoLuong = new NumericUpDown();
        PictureBox picProductImage = new PictureBox();
        DataGridView dgvSanPham = new DataGridView();

        Button btnThem = new Button();
        Button btnSua = new Button();
        Button btnXoa = new Button();
        Button btnLamMoi = new Button();
        Button btnChonAnh = new Button();

        Label lblTongSoLuong = new Label();
        Label lblTongGiaTri = new Label();
        Label lblThoiGian = new Label();

        string duongDanAnh = "";

        public frmInventoryManager()
        {
            TaoGiaoDien();
            GanDuLieu();
            GanSuKien();
            CapNhatThongKe();
            txtMa.Text = TaoMaMoi();

            dongHo.Interval = 1000;
            dongHo.Tick += (s, e) =>
            {
                lblThoiGian.Text = "Thoi gian: " +
                    DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            };
            dongHo.Start();

            FormClosing += FrmInventoryManager_FormClosing;
        }

        void TaoGiaoDien()
        {
            Text = "QUAN LY KHO VA BAO CAO";
            Size = new Size(1100, 750);
            StartPosition = FormStartPosition.CenterScreen;

            MenuStrip menu = new MenuStrip();

            ToolStripMenuItem menuHeThong =
                new ToolStripMenuItem("System");
            ToolStripMenuItem menuMo =
                new ToolStripMenuItem("Open File");
            ToolStripMenuItem menuLuu =
                new ToolStripMenuItem("Save Report");
            ToolStripMenuItem menuThoat =
                new ToolStripMenuItem("Exit");

            menuMo.Click += (s, e) => NhapCSV();
            menuLuu.Click += (s, e) => XuatCSV();
            menuThoat.Click += (s, e) => Close();

            menuHeThong.DropDownItems.Add(menuMo);
            menuHeThong.DropDownItems.Add(menuLuu);
            menuHeThong.DropDownItems.Add(new ToolStripSeparator());
            menuHeThong.DropDownItems.Add(menuThoat);

            ToolStripMenuItem menuTools =
                new ToolStripMenuItem("Tools");
            ToolStripMenuItem menuNhap =
                new ToolStripMenuItem("Import CSV");
            ToolStripMenuItem menuXuat =
                new ToolStripMenuItem("Export CSV");

            menuNhap.Click += (s, e) => NhapCSV();
            menuXuat.Click += (s, e) => XuatCSV();

            menuTools.DropDownItems.Add(menuNhap);
            menuTools.DropDownItems.Add(menuXuat);

            menu.Items.Add(menuHeThong);
            menu.Items.Add(menuTools);
            MainMenuStrip = menu;
            Controls.Add(menu);

            ToolStrip congCu = new ToolStrip();

            ToolStripButton nutThemNhanh =
                new ToolStripButton("Them");
            ToolStripButton nutNhapNhanh =
                new ToolStripButton("Import CSV");
            ToolStripButton nutXuatNhanh =
                new ToolStripButton("Export CSV");

            nutThemNhanh.Click += (s, e) => ThemSanPham();
            nutNhapNhanh.Click += (s, e) => NhapCSV();
            nutXuatNhanh.Click += (s, e) => XuatCSV();

            congCu.Items.Add(nutThemNhanh);
            congCu.Items.Add(nutNhapNhanh);
            congCu.Items.Add(nutXuatNhanh);
            congCu.Dock = DockStyle.Top;
            Controls.Add(congCu);

            Label tieuDe = new Label();
            tieuDe.Text = "QUAN LY KHO SAN PHAM";
            tieuDe.Font = new Font("Arial", 18, FontStyle.Bold);
            tieuDe.AutoSize = true;
            tieuDe.Location = new Point(350, 65);
            Controls.Add(tieuDe);

            GroupBox nhomNhap = new GroupBox();
            nhomNhap.Text = "Thong tin san pham";
            nhomNhap.Location = new Point(20, 105);
            nhomNhap.Size = new Size(700, 260);
            Controls.Add(nhomNhap);

            TaoNhan(nhomNhap, "Ma san pham:", 20, 30);
            TaoNhan(nhomNhap, "Ten san pham:", 20, 70);
            TaoNhan(nhomNhap, "Loai:", 20, 110);
            TaoNhan(nhomNhap, "Don gia:", 350, 30);
            TaoNhan(nhomNhap, "So luong:", 350, 70);

            TaoO(nhomNhap, txtMa, 130, 30);
            txtMa.ReadOnly = true;
            TaoO(nhomNhap, txtTen, 130, 70);
            TaoO(nhomNhap, txtGia, 440, 30);

            cboLoai.Location = new Point(130, 110);
            cboLoai.Size = new Size(190, 25);
            cboLoai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoai.Items.AddRange(new object[]
            {
                "May tinh",
                "Dien thoai",
                "May tinh bang",
                "Man hinh",
                "Phu kien",
                "Khac"
            });
            nhomNhap.Controls.Add(cboLoai);

            numSoLuong.Location = new Point(440, 70);
            numSoLuong.Size = new Size(190, 25);
            numSoLuong.Minimum = 0;
            numSoLuong.Maximum = 1000000;
            nhomNhap.Controls.Add(numSoLuong);

            TaoNut(nhomNhap, btnThem, "Them moi", 20, 155);
            TaoNut(nhomNhap, btnSua, "Cap nhat", 130, 155);
            TaoNut(nhomNhap, btnXoa, "Xoa", 240, 155);
            TaoNut(nhomNhap, btnLamMoi, "Lam moi", 350, 155);

            btnChonAnh.Text = "Chon anh";
            btnChonAnh.Location = new Point(20, 205);
            btnChonAnh.Size = new Size(100, 30);
            nhomNhap.Controls.Add(btnChonAnh);

            picProductImage.Location = new Point(750, 120);
            picProductImage.Size = new Size(250, 200);
            picProductImage.BorderStyle = BorderStyle.FixedSingle;
            picProductImage.SizeMode = PictureBoxSizeMode.Zoom;
            Controls.Add(picProductImage);

            dgvSanPham.Location = new Point(20, 380);
            dgvSanPham.Size = new Size(1040, 260);
            dgvSanPham.ReadOnly = true;
            dgvSanPham.AllowUserToAddRows = false;
            dgvSanPham.AutoGenerateColumns = false;
            dgvSanPham.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvSanPham.MultiSelect = false;
            dgvSanPham.RowHeadersVisible = false;
            dgvSanPham.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            ThemCot("Ma", "Ma SP", 12);
            ThemCot("Ten", "Ten san pham", 23);
            ThemCot("Loai", "Loai", 15);
            ThemCot("Gia", "Don gia", 15);
            ThemCot("SoLuong", "So luong", 12);
            ThemCot("DuongDanAnh", "Ten file anh", 23);

            dgvSanPham.Columns["Gia"].DefaultCellStyle.Format = "N0";
            dgvSanPham.Columns["DuongDanAnh"].DataPropertyName =
                "DuongDanAnh";

            Controls.Add(dgvSanPham);

            StatusStrip trangThai = new StatusStrip();

            lblTongSoLuong.Text = "Tong so luong: 0";
            lblTongGiaTri.Text = "Tong gia tri kho: 0 VNĐ";
            lblThoiGian.Text = "Thoi gian:";

            trangThai.Items.Add(new ToolStripStatusLabel(lblTongSoLuong.Text));
trangThai.Items.Add(new ToolStripStatusLabel(" | "));
trangThai.Items.Add(new ToolStripStatusLabel(lblTongGiaTri.Text));
trangThai.Items.Add(new ToolStripStatusLabel(" | "));
trangThai.Items.Add(new ToolStripStatusLabel(lblThoiGian.Text));

            Controls.Add(trangThai);
            trangThai.Dock = DockStyle.Bottom;

            baoLoi.ContainerControl = this;
        }

        void TaoNhan(Control cha, string chu, int x, int y)
        {
            Label nhan = new Label();
            nhan.Text = chu;
            nhan.Location = new Point(x, y);
            nhan.Size = new Size(110, 25);
            cha.Controls.Add(nhan);
        }

        void TaoO(Control cha, TextBox o, int x, int y)
        {
            o.Location = new Point(x, y);
            o.Size = new Size(190, 25);
            cha.Controls.Add(o);
        }

        void TaoNut(
            Control cha, Button nut, string chu, int x, int y)
        {
            nut.Text = chu;
            nut.Location = new Point(x, y);
            nut.Size = new Size(100, 32);
            cha.Controls.Add(nut);
        }

        void ThemCot(string ten, string tieuDe, float tiLe)
        {
            dgvSanPham.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = ten,
                HeaderText = tieuDe,
                DataPropertyName = ten,
                FillWeight = tiLe
            });
        }

        void GanDuLieu()
        {
            nguonDuLieu.DataSource = danhSach;
            dgvSanPham.DataSource = nguonDuLieu;
        }

        void GanSuKien()
        {
            btnThem.Click += (s, e) => ThemSanPham();
            btnSua.Click += (s, e) => SuaSanPham();
            btnXoa.Click += (s, e) => XoaSanPham();
            btnLamMoi.Click += (s, e) => LamMoi();
            btnChonAnh.Click += (s, e) => ChonAnh();

            dgvSanPham.SelectionChanged += (s, e) =>
            {
                Product p = dgvSanPham.CurrentRow?.DataBoundItem
                    as Product;

                if (p == null)
                    return;

                txtMa.Text = p.Ma;
                txtTen.Text = p.Ten;
                cboLoai.SelectedItem = p.Loai;
                txtGia.Text = p.Gia.ToString();
                numSoLuong.Value = p.SoLuong;
                duongDanAnh = p.DuongDanAnh;
                HienThiAnh(duongDanAnh);
            };

            danhSach.ListChanged += (s, e) => CapNhatThongKe();
        }

        string TaoMaMoi()
        {
            int so = 1;
            string ma;

            do
            {
                ma = "SP" + so.ToString("D3");
                so++;
            }
            while (danhSach.Any(p => p.Ma == ma));

            return ma;
        }

        bool KiemTra()
        {
            baoLoi.Clear();
            bool hopLe = true;
            decimal gia;

            if (txtTen.Text.Trim() == "")
            {
                baoLoi.SetError(txtTen, "Hay nhap ten san pham");
                hopLe = false;
            }

            if (cboLoai.SelectedIndex == -1)
            {
                baoLoi.SetError(cboLoai, "Hay chon loai san pham");
                hopLe = false;
            }

            if (!decimal.TryParse(txtGia.Text.Trim(), out gia)
                || gia < 0)
            {
                baoLoi.SetError(txtGia, "Don gia khong hop le");
                hopLe = false;
            }

            return hopLe;
        }

        void ThemSanPham()
        {
            if (!KiemTra())
                return;

            danhSach.Add(new Product
            {
                Ma = TaoMaMoi(),
                Ten = txtTen.Text.Trim(),
                Loai = cboLoai.Text,
                Gia = decimal.Parse(txtGia.Text.Trim()),
                SoLuong = (int)numSoLuong.Value,
                DuongDanAnh = duongDanAnh
            });

            LamMoi();
            CapNhatThongKe();
            MessageBox.Show("Them san pham thanh cong!");
        }

        void SuaSanPham()
        {
            Product p = dgvSanPham.CurrentRow?.DataBoundItem as Product;

            if (p == null)
            {
                MessageBox.Show("Hay chon san pham can sua!");
                return;
            }

            if (!KiemTra())
                return;

            p.Ten = txtTen.Text.Trim();
            p.Loai = cboLoai.Text;
            p.Gia = decimal.Parse(txtGia.Text.Trim());
            p.SoLuong = (int)numSoLuong.Value;
            p.DuongDanAnh = duongDanAnh;

            nguonDuLieu.ResetCurrentItem();
            CapNhatThongKe();
            MessageBox.Show("Cap nhat thanh cong!");
        }

        void XoaSanPham()
        {
            Product p = dgvSanPham.CurrentRow?.DataBoundItem as Product;

            if (p == null)
            {
                MessageBox.Show("Hay chon san pham can xoa!");
                return;
            }

            if (MessageBox.Show(
                "Ban co chac muon xoa san pham nay?",
                "Xac nhan",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                danhSach.Remove(p);
                LamMoi();
                CapNhatThongKe();
            }
        }

        void LamMoi()
        {
            txtMa.Text = TaoMaMoi();
            txtTen.Clear();
            txtGia.Clear();
            cboLoai.SelectedIndex = -1;
            numSoLuong.Value = 0;
            duongDanAnh = "";
            picProductImage.Image = null;
            baoLoi.Clear();
            dgvSanPham.ClearSelection();
            dgvSanPham.CurrentCell = null;
        }

        void ChonAnh()
        {
            using (OpenFileDialog hopThoai = new OpenFileDialog())
            {
                hopThoai.Title = "Chon anh san pham";
                hopThoai.Filter =
                    "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (hopThoai.ShowDialog() == DialogResult.OK)
                {
                    duongDanAnh = hopThoai.FileName;
                    HienThiAnh(duongDanAnh);
                }
            }
        }

        void HienThiAnh(string duongDan)
        {
            if (picProductImage.Image != null)
            {
                Image anhCu = picProductImage.Image;
                picProductImage.Image = null;
                anhCu.Dispose();
            }

            if (File.Exists(duongDan))
            {
                using (Image anhGoc = Image.FromFile(duongDan))
                {
                    picProductImage.Image = new Bitmap(anhGoc);
                }
            }
        }

        void XuatCSV()
        {
            using (SaveFileDialog hopThoai = new SaveFileDialog())
            {
                hopThoai.Title = "Luu bao cao kho";
                hopThoai.Filter = "CSV Files|*.csv";
                hopThoai.FileName = "BaoCaoKho.csv";

                if (hopThoai.ShowDialog() != DialogResult.OK)
                    return;

                using (StreamWriter ghi = new StreamWriter(
                    hopThoai.FileName, false, Encoding.UTF8))
                {
                    ghi.WriteLine(
                        "Ma;Ten;Loai;Gia;SoLuong;DuongDanAnh");

                    foreach (Product p in danhSach)
                    {
                        ghi.WriteLine(
                            $"{Csv(p.Ma)};{Csv(p.Ten)};" +
                            $"{Csv(p.Loai)};{p.Gia};" +
                            $"{p.SoLuong};{Csv(p.DuongDanAnh)}");
                    }
                }

                MessageBox.Show("Xuat CSV thanh cong!");
            }
        }

        string Csv(string chu)
        {
            return "\"" + (chu ?? "").Replace("\"", "\"\"") + "\"";
        }

        void NhapCSV()
        {
            using (OpenFileDialog hopThoai = new OpenFileDialog())
            {
                hopThoai.Title = "Chon file CSV";
                hopThoai.Filter = "CSV Files|*.csv";

                if (hopThoai.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    string[] dong = File.ReadAllLines(
                        hopThoai.FileName, Encoding.UTF8);

                    danhSach.Clear();

                    for (int i = 1; i < dong.Length; i++)
                    {
                        if (string.IsNullOrWhiteSpace(dong[i]))
                            continue;

                        string[] cot = TachCSV(dong[i]);

                        if (cot.Length < 6)
                            continue;

                        decimal gia;
                        int soLuong;

                        if (!decimal.TryParse(
                            cot[3], NumberStyles.Number,
                            CultureInfo.InvariantCulture, out gia))
                        {
                            if (!decimal.TryParse(cot[3], out gia))
                                continue;
                        }

                        if (!int.TryParse(cot[4], out soLuong))
                            continue;

                        danhSach.Add(new Product
                        {
                            Ma = cot[0],
                            Ten = cot[1],
                            Loai = cot[2],
                            Gia = gia,
                            SoLuong = soLuong,
                            DuongDanAnh = cot[5]
                        });
                    }

                    LamMoi();
                    CapNhatThongKe();
                    MessageBox.Show("Nhap CSV thanh cong!");
                }
                catch (Exception loi)
                {
                    MessageBox.Show(
                        "Khong doc duoc file: " + loi.Message);
                }
            }
        }

        string[] TachCSV(string dong)
        {
            var ketQua = new System.Collections.Generic.List<string>();
            var o = new StringBuilder();
            bool trongNgoac = false;

            for (int i = 0; i < dong.Length; i++)
            {
                char c = dong[i];

                if (c == '"')
                {
                    if (trongNgoac && i + 1 < dong.Length
                        && dong[i + 1] == '"')
                    {
                        o.Append('"');
                        i++;
                    }
                    else
                    {
                        trongNgoac = !trongNgoac;
                    }
                }
                else if (c == ';' && !trongNgoac)
                {
                    ketQua.Add(o.ToString());
                    o.Clear();
                }
                else
                {
                    o.Append(c);
                }
            }

            ketQua.Add(o.ToString());
            return ketQua.ToArray();
        }

        void CapNhatThongKe()
        {
            int tongSoLuong = danhSach.Sum(p => p.SoLuong);
            decimal tongGiaTri = danhSach.Sum(
                p => p.Gia * p.SoLuong);

            lblTongSoLuong.Text =
                "Tong so luong: " + tongSoLuong;

            lblTongGiaTri.Text =
                "Tong gia tri kho: " + tongGiaTri.ToString("N0")
                + " VNĐ";
        }

        void FrmInventoryManager_FormClosing(
            object sender, FormClosingEventArgs e)
        {
            DialogResult ketQua = MessageBox.Show(
                "Ban co chac chan muon thoat?",
                "Xac nhan thoat",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (ketQua != DialogResult.Yes)
                e.Cancel = true;
            else
                dongHo.Stop();
        }
    }
}

