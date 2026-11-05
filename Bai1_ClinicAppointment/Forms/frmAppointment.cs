
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Bai1_ClinicAppointment.Models;

namespace Bai1_ClinicAppointment.Forms
{
    public class frmAppointment : Form
    {
        BindingList<Appointment> danhSach = new BindingList<Appointment>();
        BindingSource nguonDuLieu = new BindingSource();

        ErrorProvider loi = new ErrorProvider();

        TextBox txtMa = new TextBox();
        TextBox txtTen = new TextBox();
        TextBox txtSDT = new TextBox();
        TextBox txtLyDo = new TextBox();

        DateTimePicker dtpNgayHen = new DateTimePicker();

        ComboBox cboBacSi = new ComboBox();
        ComboBox cboTrangThai = new ComboBox();

        TextBox txtTimKiem = new TextBox();
        ComboBox cboLocTrangThai = new ComboBox();
        DateTimePicker dtpLocNgay = new DateTimePicker();

        DataGridView dgv = new DataGridView();

        ToolStripStatusLabel lblSoLuong = new ToolStripStatusLabel();

        bool dangNap = false;

       public frmAppointment()
{
    KhoiTaoGiaoDien();
    TaoDanhSachBacSi();
    LamMoi();
    HienThiDuLieu();
}

        void KhoiTaoGiaoDien()
        {
            Text = "Clinic Appointment Manager";
            Size = new Size(1100, 750);
            MinimumSize = new Size(850, 600);
            StartPosition = FormStartPosition.CenterScreen;

            MenuStrip menu = new MenuStrip();

            ToolStripMenuItem menuFile =
                new ToolStripMenuItem("Tệp");

            ToolStripMenuItem menuXuat =
                new ToolStripMenuItem("Xuất CSV");

            ToolStripMenuItem menuThoat =
                new ToolStripMenuItem("Thoát");

            menuXuat.Click += (s, e) => XuatCSV();
            menuThoat.Click += (s, e) => Close();

            menuFile.DropDownItems.Add(menuXuat);
            menuFile.DropDownItems.Add(menuThoat);

            menu.Items.Add(menuFile);

            Controls.Add(menu);
            MainMenuStrip = menu;

            StatusStrip thanhTrangThai = new StatusStrip();

            lblSoLuong.Text = "Số lịch hẹn: 0";
            thanhTrangThai.Items.Add(lblSoLuong);

            Controls.Add(thanhTrangThai);

            TableLayoutPanel bangChinh = new TableLayoutPanel();

            bangChinh.Dock = DockStyle.Fill;
            bangChinh.ColumnCount = 1;
            bangChinh.RowCount = 3;

            bangChinh.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 280));

            bangChinh.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 55));

            bangChinh.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            Controls.Add(bangChinh);

            bangChinh.BringToFront();
            thanhTrangThai.BringToFront();
            menu.BringToFront();

            TaoKhuVucNhap(bangChinh);
            TaoKhuVucTimKiem(bangChinh);
            TaoKhuVucBang(bangChinh);

            loi.ContainerControl = this;
            loi.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        void TaoKhuVucNhap(TableLayoutPanel bangChinh)
        {
            GroupBox nhomNhap = new GroupBox();

            nhomNhap.Text = "THÔNG TIN LỊCH HẸN";
            nhomNhap.Dock = DockStyle.Fill;
            nhomNhap.Padding = new Padding(10);

            bangChinh.Controls.Add(nhomNhap, 0, 0);

            TableLayoutPanel bangNhap = new TableLayoutPanel();

            bangNhap.Dock = DockStyle.Fill;
            bangNhap.ColumnCount = 4;
            bangNhap.RowCount = 5;

            bangNhap.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 110));

            bangNhap.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));

            bangNhap.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 110));

            bangNhap.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));

            for (int i = 0; i < 4; i++)
            {
                bangNhap.RowStyles.Add(
                    new RowStyle(SizeType.Absolute, 38));
            }

            bangNhap.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            nhomNhap.Controls.Add(bangNhap);

            txtMa.ReadOnly = true;
            txtMa.BackColor = Color.LightGray;

            ThemTruong(bangNhap, "Mã lịch hẹn:", txtMa, 0, 0);
            ThemTruong(bangNhap, "Họ tên:", txtTen, 2, 0);

            ThemTruong(bangNhap, "Số điện thoại:", txtSDT, 0, 1);

            dtpNgayHen.Format = DateTimePickerFormat.Custom;
            dtpNgayHen.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpNgayHen.ShowUpDown = true;

            ThemTruong(bangNhap, "Ngày giờ hẹn:", dtpNgayHen, 2, 1);

            cboBacSi.DropDownStyle = ComboBoxStyle.DropDownList;
            ThemTruong(bangNhap, "Bác sĩ:", cboBacSi, 0, 2);

            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;

            ThemTruong(bangNhap, "Trạng thái:", cboTrangThai, 2, 2);

            cboTrangThai.Items.AddRange(new object[]
            {
                "Chờ khám",
                "Đang khám",
                "Hoàn tất",
                "Đã hủy"
            });

            cboTrangThai.SelectedIndex = 0;

            Label lblLyDo = new Label();

            lblLyDo.Text = "Lý do khám:";
            lblLyDo.Dock = DockStyle.Fill;
            lblLyDo.TextAlign = ContentAlignment.MiddleLeft;

            txtLyDo.Multiline = true;
            txtLyDo.Dock = DockStyle.Fill;
            txtLyDo.ScrollBars = ScrollBars.Vertical;

            bangNhap.Controls.Add(lblLyDo, 0, 3);
            bangNhap.Controls.Add(txtLyDo, 1, 3);
            bangNhap.SetColumnSpan(txtLyDo, 3);

            FlowLayoutPanel bangNut = new FlowLayoutPanel();

            bangNut.Dock = DockStyle.Fill;
            bangNut.FlowDirection = FlowDirection.RightToLeft;

            Button btnThem = TaoNut("Thêm");
            Button btnSua = TaoNut("Cập nhật");
            Button btnXoa = TaoNut("Xóa");
            Button btnLamMoi = TaoNut("Làm mới");

            btnThem.Click += (s, e) => ThemLichHen();
            btnSua.Click += (s, e) => CapNhatLichHen();
            btnXoa.Click += (s, e) => XoaLichHen();
            btnLamMoi.Click += (s, e) => LamMoi();

            bangNut.Controls.Add(btnLamMoi);
            bangNut.Controls.Add(btnXoa);
            bangNut.Controls.Add(btnSua);
            bangNut.Controls.Add(btnThem);

            bangNhap.Controls.Add(bangNut, 0, 4);
            bangNhap.SetColumnSpan(bangNut, 4);
        }

        void ThemTruong(
            TableLayoutPanel bang,
            string ten,
            Control dieuKhien,
            int cot,
            int hang)
        {
            Label lbl = new Label();

            lbl.Text = ten;
            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = ContentAlignment.MiddleLeft;

            dieuKhien.Dock = DockStyle.Fill;
            dieuKhien.Margin = new Padding(3, 5, 8, 5);

            bang.Controls.Add(lbl, cot, hang);
            bang.Controls.Add(dieuKhien, cot + 1, hang);
        }

        Button TaoNut(string ten)
        {
            Button nut = new Button();

            nut.Text = ten;
            nut.Size = new Size(105, 32);
            nut.Margin = new Padding(5);

            return nut;
        }

        void TaoDanhSachBacSi()
        {
            cboBacSi.Items.AddRange(new object[]
            {
                "Nguyễn Văn An",
                "Trần Thị Bình",
                "Lê Văn Cường",
                "Phạm Thị Dung"
            });

            cboBacSi.SelectedIndex = 0;
        }

        void TaoKhuVucTimKiem(TableLayoutPanel bangChinh)
        {
            FlowLayoutPanel khuVuc = new FlowLayoutPanel();

            khuVuc.Dock = DockStyle.Fill;
            khuVuc.Padding = new Padding(5);
            khuVuc.WrapContents = false;

            Label lblTim = new Label();

            lblTim.Text = "Tìm kiếm:";
            lblTim.AutoSize = true;
            lblTim.Margin = new Padding(5, 10, 5, 5);

            txtTimKiem.Width = 210;
            txtTimKiem.Margin = new Padding(5, 5, 15, 5);

            Label lblNgay = new Label();

            lblNgay.Text = "Lọc ngày:";
            lblNgay.AutoSize = true;
            lblNgay.Margin = new Padding(5, 10, 5, 5);

            dtpLocNgay.Width = 135;
            dtpLocNgay.Format = DateTimePickerFormat.Short;
            dtpLocNgay.ShowCheckBox = true;
            dtpLocNgay.Checked = false;

            Label lblTrangThai = new Label();

            lblTrangThai.Text = "Trạng thái:";
            lblTrangThai.AutoSize = true;
            lblTrangThai.Margin = new Padding(5, 10, 5, 5);

            cboLocTrangThai.Width = 135;
            cboLocTrangThai.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cboLocTrangThai.Items.AddRange(new object[]
            {
                "Tất cả",
                "Chờ khám",
                "Đang khám",
                "Hoàn tất",
                "Đã hủy"
            });

            cboLocTrangThai.SelectedIndex = 0;

            khuVuc.Controls.Add(lblTim);
            khuVuc.Controls.Add(txtTimKiem);
            khuVuc.Controls.Add(lblNgay);
            khuVuc.Controls.Add(dtpLocNgay);
            khuVuc.Controls.Add(lblTrangThai);
            khuVuc.Controls.Add(cboLocTrangThai);

            bangChinh.Controls.Add(khuVuc, 0, 1);

            txtTimKiem.TextChanged += (s, e) => HienThiDuLieu();

            dtpLocNgay.ValueChanged +=
                (s, e) => HienThiDuLieu();

            dtpLocNgay.CloseUp +=
                (s, e) => HienThiDuLieu();

            cboLocTrangThai.SelectedIndexChanged +=
                (s, e) => HienThiDuLieu();
        }

        void TaoKhuVucBang(TableLayoutPanel bangChinh)
        {
            GroupBox nhomBang = new GroupBox();

            nhomBang.Text = "DANH SÁCH LỊCH HẸN";
            nhomBang.Dock = DockStyle.Fill;

            bangChinh.Controls.Add(nhomBang, 0, 2);

            dgv.Dock = DockStyle.Fill;
            dgv.AutoGenerateColumns = false;

            dgv.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;

            dgv.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgv.RowHeadersVisible = false;

            ThemCot("MaLichHen", "Mã lịch", 85);
            ThemCot("HoTen", "Họ tên bệnh nhân", 150);
            ThemCot("SoDienThoai", "Số điện thoại", 110);
            ThemCot("NgayGioHen", "Ngày giờ hẹn", 140);
            ThemCot("BacSi", "Bác sĩ", 130);
            ThemCot("LyDoKham", "Lý do khám", 160);
            ThemCot("TrangThai", "Trạng thái", 100);

            dgv.Columns["NgayGioHen"].DefaultCellStyle.Format =
                "dd/MM/yyyy HH:mm";

            dgv.DataSource = nguonDuLieu;

            dgv.SelectionChanged += (s, e) =>
            {
                if (!dangNap && dgv.SelectedRows.Count > 0)
                {
                    NapDuLieuLenForm();
                }
            };

            nhomBang.Controls.Add(dgv);
        }

        void ThemCot(string tenCot, string tieuDe, int doRong)
        {
            DataGridViewTextBoxColumn cot =
                new DataGridViewTextBoxColumn();

            cot.DataPropertyName = tenCot;
            cot.Name = tenCot;
            cot.HeaderText = tieuDe;
            cot.Width = doRong;

            dgv.Columns.Add(cot);
        }

        void HienThiDuLieu()
        {
            string tuKhoa = txtTimKiem.Text.Trim().ToLower();

            var ketQua = danhSach.Where(x =>
                (x.HoTen.ToLower().Contains(tuKhoa) ||
                 x.SoDienThoai.Contains(tuKhoa)) &&

                (!dtpLocNgay.Checked ||
                 x.NgayGioHen.Date == dtpLocNgay.Value.Date) &&

                (cboLocTrangThai.SelectedIndex <= 0 ||
                 x.TrangThai == cboLocTrangThai.Text)
            ).ToList();

            dangNap = true;

            nguonDuLieu.DataSource =
                new BindingList<Appointment>(ketQua);

            dangNap = false;

            lblSoLuong.Text =
                "Số lịch hẹn sau khi lọc: " + ketQua.Count;

            if (dgv.Rows.Count > 0)
            {
                dgv.ClearSelection();
            }
        }

        void LamMoi()
        {
            dangNap = true;

            txtMa.Text = TaoMaLichHen();
            txtTen.Clear();
            txtSDT.Clear();
            txtLyDo.Clear();

            dtpNgayHen.Value = DateTime.Now;

            if (cboBacSi.Items.Count > 0)
                cboBacSi.SelectedIndex = 0;

            cboTrangThai.SelectedIndex = 0;

            loi.Clear();

            dgv.ClearSelection();

            dangNap = false;

            txtTen.Focus();
        }

        string TaoMaLichHen()
        {
            return "LH" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }

        bool KiemTraDuLieu()
        {
            loi.Clear();

            bool hopLe = true;

            // KIỂM TRA MÃ LỊCH HẸN DUY NHẤT

            Appointment? lichDangChon = LayLichDangChon();

            bool maBiTrung = danhSach.Any(x =>
                x.MaLichHen == txtMa.Text &&
                x != lichDangChon);

            if (string.IsNullOrWhiteSpace(txtMa.Text) || maBiTrung)
            {
                loi.SetError(
                    txtMa,
                    "Mã lịch hẹn không hợp lệ hoặc bị trùng.");

                hopLe = false;
            }

            // KIỂM TRA HỌ TÊN

            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                loi.SetError(txtTen, "Vui lòng nhập họ tên.");
                hopLe = false;
            }

            // KIỂM TRA SỐ ĐIỆN THOẠI

            if (!Regex.IsMatch(txtSDT.Text.Trim(), @"^0\d{9}$"))
            {
                loi.SetError(
                    txtSDT,
                    "Số điện thoại phải có 10 chữ số và bắt đầu bằng 0.");

                hopLe = false;
            }

            // KIỂM TRA LÝ DO KHÁM

            if (string.IsNullOrWhiteSpace(txtLyDo.Text))
            {
                loi.SetError(txtLyDo, "Vui lòng nhập lý do khám.");
                hopLe = false;
            }

            // KIỂM TRA NGÀY GIỜ HẸN

            DateTime hienTai = DateTime.Now;

            DateTime thoiDiemHienTai = new DateTime(
                hienTai.Year,
                hienTai.Month,
                hienTai.Day,
                hienTai.Hour,
                hienTai.Minute,
                0);

            if (dtpNgayHen.Value < thoiDiemHienTai)
            {
                loi.SetError(
                    dtpNgayHen,
                    "Ngày hẹn không được nằm trong quá khứ.");

                hopLe = false;
            }

            // KIỂM TRA BÁC SĨ

            if (cboBacSi.SelectedIndex < 0)
            {
                loi.SetError(cboBacSi, "Vui lòng chọn bác sĩ.");
                hopLe = false;
            }

            return hopLe;
        }

        void ThemLichHen()
        {
            if (!KiemTraDuLieu())
                return;

            Appointment moi = new Appointment();

            moi.MaLichHen = txtMa.Text;
            moi.HoTen = txtTen.Text.Trim();
            moi.SoDienThoai = txtSDT.Text.Trim();
            moi.NgayGioHen = dtpNgayHen.Value;
            moi.BacSi = cboBacSi.Text;
            moi.LyDoKham = txtLyDo.Text.Trim();
            moi.TrangThai = cboTrangThai.Text;

            danhSach.Add(moi);

            HienThiDuLieu();
            LamMoi();

            MessageBox.Show(
                "Thêm lịch hẹn thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        Appointment? LayLichDangChon()
        {
            if (dgv.SelectedRows.Count == 0)
                return null;

            return dgv.SelectedRows[0].DataBoundItem as Appointment;
        }

        void CapNhatLichHen()
        {
            Appointment? lich = LayLichDangChon();

            if (lich == null)
            {
                MessageBox.Show("Vui lòng chọn lịch hẹn cần sửa.");
                return;
            }

            if (!KiemTraDuLieu())
                return;

            if (MessageBox.Show(
                "Bạn có chắc muốn cập nhật lịch hẹn này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            lich.HoTen = txtTen.Text.Trim();
            lich.SoDienThoai = txtSDT.Text.Trim();
            lich.NgayGioHen = dtpNgayHen.Value;
            lich.BacSi = cboBacSi.Text;
            lich.LyDoKham = txtLyDo.Text.Trim();
            lich.TrangThai = cboTrangThai.Text;

            HienThiDuLieu();
            LamMoi();

            MessageBox.Show("Cập nhật thành công!");
        }

        void XoaLichHen()
        {
            Appointment? lich = LayLichDangChon();

            if (lich == null)
            {
                MessageBox.Show("Vui lòng chọn lịch hẹn cần xóa.");
                return;
            }

            if (MessageBox.Show(
                "Bạn có chắc muốn xóa lịch hẹn " +
                lich.MaLichHen + "?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            danhSach.Remove(lich);

            HienThiDuLieu();
            LamMoi();

            MessageBox.Show("Đã xóa lịch hẹn!");
        }

        void NapDuLieuLenForm()
        {
            Appointment? lich = LayLichDangChon();

            if (lich == null)
                return;

            dangNap = true;

            txtMa.Text = lich.MaLichHen;
            txtTen.Text = lich.HoTen;
            txtSDT.Text = lich.SoDienThoai;
            txtLyDo.Text = lich.LyDoKham;

            dtpNgayHen.Value = lich.NgayGioHen;

            cboBacSi.Text = lich.BacSi;
            cboTrangThai.Text = lich.TrangThai;

            loi.Clear();

            dangNap = false;
        }

        void XuatCSV()
        {
            using (SaveFileDialog hopThoai = new SaveFileDialog())
            {
                hopThoai.Filter = "CSV files (*.csv)|*.csv";
                hopThoai.FileName = "DanhSachLichHen.csv";

                if (hopThoai.ShowDialog() != DialogResult.OK)
                    return;

                var danhSachHienThi =
                    nguonDuLieu.List.Cast<Appointment>().ToList();

                using (StreamWriter ghi = new StreamWriter(
                    hopThoai.FileName,
                    false,
                    new UTF8Encoding(true)))
                {
                    ghi.WriteLine(
                        "MaLichHen,HoTen,SoDienThoai,NgayGioHen,BacSi,LyDoKham,TrangThai");

                    foreach (Appointment lich in danhSachHienThi)
                    {
                        string dong = string.Join(",",
                            ChuanHoaCSV(lich.MaLichHen),
                            ChuanHoaCSV(lich.HoTen),
                            ChuanHoaCSV(lich.SoDienThoai),
                            ChuanHoaCSV(
                                lich.NgayGioHen.ToString("dd/MM/yyyy HH:mm")),
                            ChuanHoaCSV(lich.BacSi),
                            ChuanHoaCSV(lich.LyDoKham),
                            ChuanHoaCSV(lich.TrangThai));

                        ghi.WriteLine(dong);
                    }
                }

                MessageBox.Show(
                    "Xuất CSV thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        string ChuanHoaCSV(string noiDung)
        {
            return "\"" + noiDung.Replace("\"", "\"\"") + "\"";
        }
    }
}

