using System.Linq;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using QuanLySieuThi.Models;

namespace QuanLySieuThi
{
    public partial class Form1 : Form
    {
        BindingList<Device> danhSach = new BindingList<Device>();
        BindingSource nguonDuLieu = new BindingSource();
        ErrorProvider baoLoi = new ErrorProvider();

        TextBox txtMa = new TextBox();
        TextBox txtTen = new TextBox();
        TextBox txtGia = new TextBox();

        ComboBox cboCategory = new ComboBox();
        NumericUpDown numQuantity = new NumericUpDown();
        DataGridView dgvDevices = new DataGridView();

        Button btnAdd = new Button();
        Button btnUpdate = new Button();
        Button btnDelete = new Button();
        Button btnClear = new Button();

        bool dangNapDuLieu = false;

        public Form1()
        {
            InitializeComponent();

            TaoGiaoDien();
            GanDuLieu();
            GanSuKien();

            txtMa.Text = TaoMaMoi();
        }

        void TaoGiaoDien()
        {
            Text = "Quan ly danh muc thiet bi dien tu";
            Size = new Size(1000, 650);
            StartPosition = FormStartPosition.CenterScreen;

            Label tieuDe = new Label();
            tieuDe.Text = "QUAN LY THIET BI DIEN TU";
            tieuDe.Font = new Font("Arial", 18, FontStyle.Bold);
            tieuDe.AutoSize = true;
            tieuDe.Location = new Point(300, 15);
            Controls.Add(tieuDe);

            GroupBox nhomNhap = new GroupBox();
            nhomNhap.Text = "Thong tin thiet bi";
            nhomNhap.Location = new Point(25, 55);
            nhomNhap.Size = new Size(930, 235);
            Controls.Add(nhomNhap);

            TaoNhan(nhomNhap, "Ma thiet bi:", 25, 35);
            TaoNhan(nhomNhap, "Ten thiet bi:", 25, 80);
            TaoNhan(nhomNhap, "Loai thiet bi:", 25, 125);
            TaoNhan(nhomNhap, "Don gia (VNĐ):", 490, 35);
            TaoNhan(nhomNhap, "So luong ton:", 490, 80);

            TaoO(nhomNhap, txtMa, 150, 35);
            txtMa.ReadOnly = true;

            TaoO(nhomNhap, txtTen, 150, 80);
            TaoO(nhomNhap, txtGia, 620, 35);

            cboCategory.Location = new Point(150, 125);
            cboCategory.Size = new Size(220, 25);
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategory.Items.AddRange(new object[]
            {
                "May tinh",
                "Dien thoai",
                "May tinh bang",
                "Man hinh",
                "Phu kien",
                "Thiet bi khac"
            });
            nhomNhap.Controls.Add(cboCategory);

            numQuantity.Location = new Point(620, 80);
            numQuantity.Size = new Size(220, 25);
            numQuantity.Minimum = 0;
            numQuantity.Maximum = 1000000;
            nhomNhap.Controls.Add(numQuantity);

            TaoNut(nhomNhap, btnAdd, "Them moi", 25, 175);
            TaoNut(nhomNhap, btnUpdate, "Cap nhat", 140, 175);
            TaoNut(nhomNhap, btnDelete, "Xoa", 255, 175);
            TaoNut(nhomNhap, btnClear, "Lam moi / Huy", 370, 175);

            dgvDevices.Location = new Point(25, 310);
            dgvDevices.Size = new Size(930, 280);
            dgvDevices.ReadOnly = true;
            dgvDevices.AllowUserToAddRows = false;
            dgvDevices.AllowUserToDeleteRows = false;
            dgvDevices.AutoGenerateColumns = false;
            dgvDevices.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvDevices.MultiSelect = false;
            dgvDevices.RowHeadersVisible = false;
            dgvDevices.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvDevices.SelectionChanged += DgvDevices_SelectionChanged;

            dgvDevices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMa",
                HeaderText = "Ma thiet bi",
                DataPropertyName = "Ma",
                FillWeight = 15
            });

            dgvDevices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTen",
                HeaderText = "Ten thiet bi",
                DataPropertyName = "Ten",
                FillWeight = 25
            });

            dgvDevices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colLoai",
                HeaderText = "Loai thiet bi",
                DataPropertyName = "Loai",
                FillWeight = 20
            });

            dgvDevices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colGia",
                HeaderText = "Don gia (VNĐ)",
                DataPropertyName = "Gia",
                FillWeight = 20,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0"
                }
            });

            dgvDevices.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSoLuong",
                HeaderText = "So luong ton",
                DataPropertyName = "SoLuong",
                FillWeight = 15
            });

            Controls.Add(dgvDevices);

            baoLoi.ContainerControl = this;
        }

        void TaoNhan(Control cha, string noiDung, int x, int y)
        {
            Label nhan = new Label();
            nhan.Text = noiDung;
            nhan.Location = new Point(x, y);
            nhan.Size = new Size(120, 25);
            cha.Controls.Add(nhan);
        }

        void TaoO(Control cha, TextBox o, int x, int y)
        {
            o.Location = new Point(x, y);
            o.Size = new Size(220, 25);
            cha.Controls.Add(o);
        }

        void TaoNut(
            Control cha, Button nut, string noiDung, int x, int y)
        {
            nut.Text = noiDung;
            nut.Location = new Point(x, y);
            nut.Size = new Size(105, 32);
            cha.Controls.Add(nut);
        }

        void GanDuLieu()
        {
            nguonDuLieu.DataSource = danhSach;
            dgvDevices.DataSource = nguonDuLieu;
        }

        void GanSuKien()
        {
            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnClear.Click += BtnClear_Click;
        }

        string TaoMaMoi()
        {
            int so = 1;
            string ma;

            do
            {
                ma = "TB" + so.ToString("D3");
                so++;
            }
            while (danhSach.Any(
                thietBi => thietBi.Ma == ma));

            return ma;
        }

        bool KiemTraDuLieu()
        {
            baoLoi.Clear();
            bool hopLe = true;

            if (txtTen.Text.Trim() == "")
            {
                baoLoi.SetError(
                    txtTen, "Ten thiet bi khong duoc de trong");
                hopLe = false;
            }

            if (cboCategory.SelectedIndex == -1)
            {
                baoLoi.SetError(
                    cboCategory, "Hay chon loai thiet bi");
                hopLe = false;
            }

            decimal gia;

            if (!decimal.TryParse(txtGia.Text.Trim(), out gia)
                || gia < 0)
            {
                baoLoi.SetError(
                    txtGia, "Don gia phai la so hop le va khong am");
                hopLe = false;
            }

            return hopLe;
        }

        void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!KiemTraDuLieu())
                return;

            Device thietBi = new Device
            {
                Ma = TaoMaMoi(),
                Ten = txtTen.Text.Trim(),
                Loai = cboCategory.Text,
                Gia = decimal.Parse(txtGia.Text.Trim()),
                SoLuong = (int)numQuantity.Value
            };

            danhSach.Add(thietBi);

            LamMoi();
            MessageBox.Show("Them thiet bi thanh cong!");
        }

        void BtnUpdate_Click(object sender, EventArgs e)
        {
            Device thietBi = dgvDevices.CurrentRow?.DataBoundItem
                as Device;

            if (thietBi == null)
            {
                MessageBox.Show("Hay chon thiet bi can cap nhat!");
                return;
            }

            if (!KiemTraDuLieu())
                return;

            thietBi.Ten = txtTen.Text.Trim();
            thietBi.Loai = cboCategory.Text;
            thietBi.Gia = decimal.Parse(txtGia.Text.Trim());
            thietBi.SoLuong = (int)numQuantity.Value;

            nguonDuLieu.ResetCurrentItem();

            MessageBox.Show("Cap nhat thiet bi thanh cong!");
        }

        void BtnDelete_Click(object sender, EventArgs e)
        {
            Device thietBi = dgvDevices.CurrentRow?.DataBoundItem
                as Device;

            if (thietBi == null)
            {
                MessageBox.Show("Hay chon thiet bi can xoa!");
                return;
            }

            DialogResult ketQua = MessageBox.Show(
                "Ban co chac muon xoa thiet bi nay?",
                "Xac nhan xoa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (ketQua == DialogResult.Yes)
            {
                danhSach.Remove(thietBi);
                LamMoi();
            }
        }

        void BtnClear_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        void LamMoi()
        {
            dangNapDuLieu = true;

            txtMa.Text = TaoMaMoi();
            txtTen.Clear();
            txtGia.Clear();
            cboCategory.SelectedIndex = -1;
            numQuantity.Value = 0;

            dgvDevices.ClearSelection();
            dgvDevices.CurrentCell = null;
            baoLoi.Clear();

            dangNapDuLieu = false;
        }

        void DgvDevices_SelectionChanged(object sender, EventArgs e)
        {
            if (dangNapDuLieu)
                return;

            Device thietBi = dgvDevices.CurrentRow?.DataBoundItem
                as Device;

            if (thietBi == null)
                return;

            txtMa.Text = thietBi.Ma;
            txtTen.Text = thietBi.Ten;
            txtGia.Text = thietBi.Gia.ToString();
            cboCategory.SelectedItem = thietBi.Loai;
            numQuantity.Value = thietBi.SoLuong;
            baoLoi.Clear();
        }
    }
}
