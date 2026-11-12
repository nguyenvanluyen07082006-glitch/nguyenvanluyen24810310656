
namespace Bai5_3_QuanLySanPham
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.GroupBox gbThongTin;
        private System.Windows.Forms.GroupBox gbChucNang;

        private System.Windows.Forms.Label lblProductId;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label lblUnitPrice;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label lblCategory;

        private System.Windows.Forms.TextBox txtProductId;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.TextBox txtUnitPrice;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.TextBox txtSearch;

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSearch;

        private System.Windows.Forms.DataGridView dgvProducts;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            gbThongTin = new System.Windows.Forms.GroupBox();
            gbChucNang = new System.Windows.Forms.GroupBox();

            lblProductId = new System.Windows.Forms.Label();
            lblProductName = new System.Windows.Forms.Label();
            lblUnitPrice = new System.Windows.Forms.Label();
            lblQuantity = new System.Windows.Forms.Label();
            lblCategory = new System.Windows.Forms.Label();

            txtProductId = new System.Windows.Forms.TextBox();
            txtProductName = new System.Windows.Forms.TextBox();
            txtUnitPrice = new System.Windows.Forms.TextBox();
            txtQuantity = new System.Windows.Forms.TextBox();
            txtCategory = new System.Windows.Forms.TextBox();
            txtSearch = new System.Windows.Forms.TextBox();

            btnAdd = new System.Windows.Forms.Button();
            btnEdit = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();
            btnSearch = new System.Windows.Forms.Button();

            dgvProducts = new System.Windows.Forms.DataGridView();

            gbThongTin.SuspendLayout();
            gbChucNang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            SuspendLayout();

            // Form
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(900, 650);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "QUẢN LÝ DANH SÁCH SẢN PHẨM";

            // GroupBox thông tin
            gbThongTin.Text = "Thông tin sản phẩm";
            gbThongTin.Location = new System.Drawing.Point(20, 20);
            gbThongTin.Size = new System.Drawing.Size(850, 200);

            // Label
            lblProductId.Text = "Mã SP:";
            lblProductId.Location = new System.Drawing.Point(30, 40);
            lblProductId.AutoSize = true;

            lblProductName.Text = "Tên SP:";
            lblProductName.Location = new System.Drawing.Point(30, 85);
            lblProductName.AutoSize = true;

            lblUnitPrice.Text = "Đơn giá:";
            lblUnitPrice.Location = new System.Drawing.Point(430, 40);
            lblUnitPrice.AutoSize = true;

            lblQuantity.Text = "Số lượng:";
            lblQuantity.Location = new System.Drawing.Point(430, 85);
            lblQuantity.AutoSize = true;

            lblCategory.Text = "Danh mục:";
            lblCategory.Location = new System.Drawing.Point(30, 135);
            lblCategory.AutoSize = true;

            // TextBox
            txtProductId.Location = new System.Drawing.Point(120, 35);
            txtProductId.Size = new System.Drawing.Size(250, 27);

            txtProductName.Location = new System.Drawing.Point(120, 80);
            txtProductName.Size = new System.Drawing.Size(250, 27);

            txtUnitPrice.Location = new System.Drawing.Point(530, 35);
            txtUnitPrice.Size = new System.Drawing.Size(250, 27);

            txtQuantity.Location = new System.Drawing.Point(530, 80);
            txtQuantity.Size = new System.Drawing.Size(250, 27);

            txtCategory.Location = new System.Drawing.Point(120, 130);
            txtCategory.Size = new System.Drawing.Size(250, 27);

            gbThongTin.Controls.Add(lblProductId);
            gbThongTin.Controls.Add(lblProductName);
            gbThongTin.Controls.Add(lblUnitPrice);
            gbThongTin.Controls.Add(lblQuantity);
            gbThongTin.Controls.Add(lblCategory);

            gbThongTin.Controls.Add(txtProductId);
            gbThongTin.Controls.Add(txtProductName);
            gbThongTin.Controls.Add(txtUnitPrice);
            gbThongTin.Controls.Add(txtQuantity);
            gbThongTin.Controls.Add(txtCategory);

            // GroupBox chức năng
            gbChucNang.Text = "Chức năng";
            gbChucNang.Location = new System.Drawing.Point(20, 235);
            gbChucNang.Size = new System.Drawing.Size(850, 100);

            // Button
            btnAdd.Text = "Thêm";
            btnAdd.Location = new System.Drawing.Point(20, 35);
            btnAdd.Size = new System.Drawing.Size(90, 35);

            btnEdit.Text = "Sửa";
            btnEdit.Location = new System.Drawing.Point(130, 35);
            btnEdit.Size = new System.Drawing.Size(90, 35);

            btnDelete.Text = "Xóa";
            btnDelete.Location = new System.Drawing.Point(240, 35);
            btnDelete.Size = new System.Drawing.Size(90, 35);

            // Tìm kiếm
            txtSearch.Location = new System.Drawing.Point(400, 37);
            txtSearch.Size = new System.Drawing.Size(200, 27);

            btnSearch.Text = "Tìm kiếm";
            btnSearch.Location = new System.Drawing.Point(620, 35);
            btnSearch.Size = new System.Drawing.Size(100, 35);

            gbChucNang.Controls.Add(btnAdd);
            gbChucNang.Controls.Add(btnEdit);
            gbChucNang.Controls.Add(btnDelete);
            gbChucNang.Controls.Add(txtSearch);
            gbChucNang.Controls.Add(btnSearch);

            // DataGridView
            dgvProducts.Location = new System.Drawing.Point(20, 350);
            dgvProducts.Size = new System.Drawing.Size(850, 260);

            dgvProducts.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            dgvProducts.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            dgvProducts.MultiSelect = false;
            dgvProducts.ReadOnly = true;
            dgvProducts.AllowUserToAddRows = false;

            // Thêm vào Form
            Controls.Add(gbThongTin);
            Controls.Add(gbChucNang);
            Controls.Add(dgvProducts);

            gbThongTin.ResumeLayout(false);
            gbThongTin.PerformLayout();

            gbChucNang.ResumeLayout(false);
            gbChucNang.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ResumeLayout(false);
        }
    }
}
