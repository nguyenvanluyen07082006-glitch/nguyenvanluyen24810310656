
namespace Bai5_4_QuanLyNhanVien
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvDepartments;
        private System.Windows.Forms.ListView lsvEmployees;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ComboBox cboViewMode;
        private System.Windows.Forms.Label lblViewMode;
        private System.Windows.Forms.Panel panelTop;

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
            components = new System.ComponentModel.Container();

            splitContainer1 = new System.Windows.Forms.SplitContainer();
            tvDepartments = new System.Windows.Forms.TreeView();
            lsvEmployees = new System.Windows.Forms.ListView();
            imageList1 = new System.Windows.Forms.ImageList(components);
            cboViewMode = new System.Windows.Forms.ComboBox();
            lblViewMode = new System.Windows.Forms.Label();
            panelTop = new System.Windows.Forms.Panel();

            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panelTop.SuspendLayout();
            SuspendLayout();

            // Form
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1100, 650);
            Text = "QUẢN LÝ NHÂN VIÊN";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            // SplitContainer
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.SplitterDistance = 280;

            // TreeView
            tvDepartments.Dock = System.Windows.Forms.DockStyle.Fill;
            tvDepartments.Name = "tvDepartments";
            tvDepartments.Font = new System.Drawing.Font("Segoe UI", 10F);
            splitContainer1.Panel1.Controls.Add(tvDepartments);

            // Panel phía trên ListView
            panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            panelTop.Height = 55;

            // Label
            lblViewMode.AutoSize = true;
            lblViewMode.Text = "Chế độ hiển thị:";
            lblViewMode.Location = new System.Drawing.Point(15, 17);

            // ComboBox
            cboViewMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboViewMode.Items.AddRange(new object[]
            {
                "Details",
                "SmallIcon",
                "LargeIcon",
                "Tile"
            });

            cboViewMode.Location = new System.Drawing.Point(150, 12);
            cboViewMode.Size = new System.Drawing.Size(160, 28);
            cboViewMode.Name = "cboViewMode";

            panelTop.Controls.Add(lblViewMode);
            panelTop.Controls.Add(cboViewMode);

            // ListView
            lsvEmployees.Dock = System.Windows.Forms.DockStyle.Fill;
            lsvEmployees.Name = "lsvEmployees";
            lsvEmployees.Font = new System.Drawing.Font("Segoe UI", 10F);
            lsvEmployees.FullRowSelect = true;
            lsvEmployees.GridLines = true;
            lsvEmployees.View = System.Windows.Forms.View.Details;
            lsvEmployees.HideSelection = false;

            // ImageList
            imageList1.ImageSize = new System.Drawing.Size(32, 32);
            imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;

            // Thêm điều khiển vào Panel2
            splitContainer1.Panel2.Controls.Add(lsvEmployees);
            splitContainer1.Panel2.Controls.Add(panelTop);

            // Thêm SplitContainer vào Form
            Controls.Add(splitContainer1);

            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ResumeLayout(false);
        }
    }
}
