
namespace Bai4_7_MDI
{
    public class FormMain : Form
    {
        private MenuStrip menu = new MenuStrip();
        private ToolStrip thanhCongCu = new ToolStrip();
        private StatusStrip thanhTrangThai = new StatusStrip();

        private ToolStripStatusLabel lblNguoiDung =
            new ToolStripStatusLabel("Người dùng: Admin");

        private ToolStripStatusLabel lblThongBao =
            new ToolStripStatusLabel("Sẵn sàng");

        private ToolStripStatusLabel lblThoiGian =
            new ToolStripStatusLabel();

        private System.Windows.Forms.Timer dongHo =
            new System.Windows.Forms.Timer();

        public FormMain()
        {
            Text = "Hệ thống quản lý - Bài 4.7";
            Width = 1100;
            Height = 700;
            IsMdiContainer = true;
            StartPosition = FormStartPosition.CenterScreen;

            TaoMenu();
            TaoThanhCongCu();
            TaoThanhTrangThai();

            dongHo.Interval = 1000;
            dongHo.Tick += (s, e) =>
            {
                lblThoiGian.Text = DateTime.Now.ToString(
                    "dd/MM/yyyy HH:mm:ss"
                );
            };
            dongHo.Start();
        }

        private void TaoMenu()
        {
            ToolStripMenuItem heThong = new ToolStripMenuItem("Hệ thống");
            ToolStripMenuItem dangXuat = new ToolStripMenuItem("Đăng xuất");
            ToolStripMenuItem thoat = new ToolStripMenuItem("Thoát");

            dangXuat.Click += (s, e) =>
            {
                foreach (Form form in MdiChildren)
                {
                    form.Close();
                }

                lblNguoiDung.Text = "Người dùng: Chưa đăng nhập";
                CapNhatTrangThai("Đã đăng xuất");
            };

            thoat.Click += (s, e) => Close();

            heThong.DropDownItems.Add(dangXuat);
            heThong.DropDownItems.Add(thoat);

            ToolStripMenuItem danhMuc = new ToolStripMenuItem("Danh mục");
            ToolStripMenuItem sinhVien = new ToolStripMenuItem("Quản lý sinh viên");
            ToolStripMenuItem sanPham = new ToolStripMenuItem("Quản lý sản phẩm");

            sinhVien.Click += (s, e) => MoForm<FormStudent>();
            sanPham.Click += (s, e) => MoForm<FormProduct>();

            danhMuc.DropDownItems.Add(sinhVien);
            danhMuc.DropDownItems.Add(sanPham);

            ToolStripMenuItem cuaSo = new ToolStripMenuItem("Cửa sổ");

            ToolStripMenuItem cascade = new ToolStripMenuItem("Cascade");
            ToolStripMenuItem ngang = new ToolStripMenuItem("Tile Horizontal");
            ToolStripMenuItem doc = new ToolStripMenuItem("Tile Vertical");

            cascade.Click += (s, e) =>
                LayoutMdi(MdiLayout.Cascade);

            ngang.Click += (s, e) =>
                LayoutMdi(MdiLayout.TileHorizontal);

            doc.Click += (s, e) =>
                LayoutMdi(MdiLayout.TileVertical);

            cuaSo.DropDownItems.Add(cascade);
            cuaSo.DropDownItems.Add(ngang);
            cuaSo.DropDownItems.Add(doc);

            menu.Items.Add(heThong);
            menu.Items.Add(danhMuc);
            menu.Items.Add(cuaSo);

            MainMenuStrip = menu;
            Controls.Add(menu);
        }

        private void TaoThanhCongCu()
        {
            ToolStripButton btnSinhVien =
                new ToolStripButton("Quản lý sinh viên");

            ToolStripButton btnSanPham =
                new ToolStripButton("Quản lý sản phẩm");

            btnSinhVien.Click += (s, e) => MoForm<FormStudent>();
            btnSanPham.Click += (s, e) => MoForm<FormProduct>();

            thanhCongCu.Items.Add(btnSinhVien);
            thanhCongCu.Items.Add(btnSanPham);

            thanhCongCu.Dock = DockStyle.Top;
            Controls.Add(thanhCongCu);
        }

        private void TaoThanhTrangThai()
        {
            thanhTrangThai.Items.Add(lblNguoiDung);
            thanhTrangThai.Items.Add(new ToolStripStatusLabel(" | "));
            thanhTrangThai.Items.Add(lblThongBao);
            thanhTrangThai.Items.Add(new ToolStripStatusLabel(" | "));
            thanhTrangThai.Items.Add(lblThoiGian);

            thanhTrangThai.Dock = DockStyle.Bottom;
            Controls.Add(thanhTrangThai);
        }

        private void MoForm<T>() where T : Form, new()
        {
            foreach (Form form in MdiChildren)
            {
                if (form is T)
                {
                    form.Activate();
                    CapNhatTrangThai("Đã kích hoạt: " + form.Text);
                    return;
                }
            }

            T formMoi = new T();
            formMoi.MdiParent = this;

            if (formMoi is FormStudent sv)
            {
                sv.BaoTrangThai += CapNhatTrangThai;
            }

            if (formMoi is FormProduct sp)
            {
                sp.BaoTrangThai += CapNhatTrangThai;
            }

            formMoi.FormClosed += (s, e) =>
                CapNhatTrangThai("Đã đóng cửa sổ");

            formMoi.Show();
            CapNhatTrangThai("Đã mở: " + formMoi.Text);
        }

        public void CapNhatTrangThai(string thongBao)
        {
            lblThongBao.Text = thongBao;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            dongHo.Stop();
            dongHo.Dispose();
            base.OnFormClosed(e);
        }
    }
}
