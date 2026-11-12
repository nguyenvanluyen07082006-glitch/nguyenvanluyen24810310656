
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Bai5_3_QuanLySanPham.Models;

namespace Bai5_3_QuanLySanPham
{
    public partial class Form1 : Form
    {
        // Danh sách sản phẩm
        List<Product> danhSach = new List<Product>();

        // BindingSource
        BindingSource bindingSource = new BindingSource();

        // Constructor
        public Form1()
        {
            InitializeComponent();

            // Liên kết sự kiện
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnSearch.Click += btnSearch_Click;

            dgvProducts.CellClick += dgvProducts_CellClick;

            // Hiển thị danh sách
            HienThiDanhSach();
        }

        // =====================================
        // 1. HIỂN THỊ DANH SÁCH SẢN PHẨM
        // =====================================

        void HienThiDanhSach()
        {
            bindingSource.DataSource = null;
            bindingSource.DataSource = danhSach;

            dgvProducts.DataSource = bindingSource;
        }

        // =====================================
        // 2. KIỂM TRA DỮ LIỆU
        // =====================================

        bool KiemTraDuLieu(out decimal donGia, out int soLuong)
        {
            donGia = 0;
            soLuong = 0;

            if (txtProductId.Text.Trim() == "" ||
                txtProductName.Text.Trim() == "" ||
                txtUnitPrice.Text.Trim() == "" ||
                txtQuantity.Text.Trim() == "" ||
                txtCategory.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return false;
            }

            if (!decimal.TryParse(txtUnitPrice.Text, out donGia))
            {
                MessageBox.Show("Đơn giá không hợp lệ!");
                return false;
            }

            if (!int.TryParse(txtQuantity.Text, out soLuong))
            {
                MessageBox.Show("Số lượng không hợp lệ!");
                return false;
            }

            if (donGia < 0 || soLuong < 0)
            {
                MessageBox.Show("Đơn giá và số lượng không được âm!");
                return false;
            }

            return true;
        }

        // =====================================
        // 3. CHỨC NĂNG THÊM SẢN PHẨM
        // =====================================

        private void btnAdd_Click(object sender, EventArgs e)
        {
            decimal donGia;
            int soLuong;

            if (!KiemTraDuLieu(out donGia, out soLuong))
            {
                return;
            }

            string maSP = txtProductId.Text.Trim();

            foreach (Product sp in danhSach)
            {
                if (sp.ProductId.Equals(
                    maSP,
                    StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Mã sản phẩm đã tồn tại!");
                    return;
                }
            }

            Product sanPham = new Product();

            sanPham.ProductId = maSP;
            sanPham.ProductName = txtProductName.Text.Trim();
            sanPham.UnitPrice = donGia;
            sanPham.Quantity = soLuong;
            sanPham.Category = txtCategory.Text.Trim();

            danhSach.Add(sanPham);

            HienThiDanhSach();

            MessageBox.Show("Thêm sản phẩm thành công!");

            XoaTrang();
        }

        // =====================================
        // 4. CLICK DÒNG DATAGRIDVIEW
        // =====================================

        private void dgvProducts_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            Product sp = dgvProducts.Rows[e.RowIndex]
                .DataBoundItem as Product;

            if (sp == null)
            {
                return;
            }

            txtProductId.Text = sp.ProductId;
            txtProductName.Text = sp.ProductName;
            txtUnitPrice.Text = sp.UnitPrice.ToString();
            txtQuantity.Text = sp.Quantity.ToString();
            txtCategory.Text = sp.Category;
        }

        // =====================================
        // 5. CHỨC NĂNG SỬA SẢN PHẨM
        // =====================================

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!");
                return;
            }

            Product sp = dgvProducts.CurrentRow.DataBoundItem as Product;

            if (sp == null)
            {
                return;
            }

            decimal donGia;
            int soLuong;

            if (!KiemTraDuLieu(out donGia, out soLuong))
            {
                return;
            }

            string maSP = txtProductId.Text.Trim();

            foreach (Product item in danhSach)
            {
                if (item.ProductId.Equals(
                    maSP,
                    StringComparison.OrdinalIgnoreCase)
                    && item != sp)
                {
                    MessageBox.Show("Mã sản phẩm đã tồn tại!");
                    return;
                }
            }

            sp.ProductId = maSP;
            sp.ProductName = txtProductName.Text.Trim();
            sp.UnitPrice = donGia;
            sp.Quantity = soLuong;
            sp.Category = txtCategory.Text.Trim();

            HienThiDanhSach();

            MessageBox.Show("Sửa sản phẩm thành công!");
        }

        // =====================================
        // 6. CHỨC NĂNG XÓA SẢN PHẨM
        // =====================================

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!");
                return;
            }

            Product sp = dgvProducts.CurrentRow.DataBoundItem as Product;

            if (sp == null)
            {
                return;
            }

            DialogResult ketQua = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa sản phẩm này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (ketQua == DialogResult.Yes)
            {
                danhSach.Remove(sp);

                HienThiDanhSach();

                XoaTrang();

                MessageBox.Show("Xóa sản phẩm thành công!");
            }
        }

        // =====================================
        // 7. CHỨC NĂNG TÌM KIẾM SẢN PHẨM
        // =====================================

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtSearch.Text.Trim();

            if (tuKhoa == "")
            {
                HienThiDanhSach();
                return;
            }

            List<Product> ketQua = danhSach
                .Where(sp => sp.ProductName.Contains(
                    tuKhoa,
                    StringComparison.OrdinalIgnoreCase))
                .ToList();

            bindingSource.DataSource = null;
            bindingSource.DataSource = ketQua;

            dgvProducts.DataSource = bindingSource;

            if (ketQua.Count == 0)
            {
                MessageBox.Show("Không tìm thấy sản phẩm!");
            }
        }

        // =====================================
        // 8. XÓA TRẮNG CÁC Ô NHẬP LIỆU
        // =====================================

        void XoaTrang()
        {
            txtProductId.Clear();
            txtProductName.Clear();
            txtUnitPrice.Clear();
            txtQuantity.Clear();
            txtCategory.Clear();

            txtProductId.Focus();
        }
    }
}

