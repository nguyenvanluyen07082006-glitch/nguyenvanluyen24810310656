
namespace Bai4_7_MDI
{
    public class FormProduct : Form
    {
        public event Action<string>? BaoTrangThai;

        private DataGridView dgv = new DataGridView();
        private TextBox txtMa = new TextBox();
        private TextBox txtTen = new TextBox();
        private TextBox txtGia = new TextBox();

        public FormProduct()
        {
            Text = "Quản lý sản phẩm";
            Width = 700;
            Height = 450;

            TaoGiaoDien();
        }

        private void TaoGiaoDien()
        {
            Label lblMa = new Label();
            lblMa.Text = "Mã sản phẩm:";
            lblMa.SetBounds(20, 20, 100, 25);

            txtMa.SetBounds(125, 20, 180, 25);

            Label lblTen = new Label();
            lblTen.Text = "Tên sản phẩm:";
            lblTen.SetBounds(20, 60, 100, 25);

            txtTen.SetBounds(125, 60, 180, 25);

            Label lblGia = new Label();
            lblGia.Text = "Giá:";
            lblGia.SetBounds(20, 100, 100, 25);

            txtGia.SetBounds(125, 100, 180, 25);

            Button btnThem = new Button();
            btnThem.Text = "Thêm sản phẩm";
            btnThem.SetBounds(340, 20, 130, 35);
            btnThem.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtMa.Text) ||
                    string.IsNullOrWhiteSpace(txtTen.Text) ||
                    !decimal.TryParse(txtGia.Text, out decimal gia) ||
                    gia < 0)
                {
                    MessageBox.Show("Nhập mã, tên và giá hợp lệ.");
                    return;
                }

                foreach (DataGridViewRow dong in dgv.Rows)
                {
                    if (dong.Cells[0].Value?.ToString() ==
                        txtMa.Text.Trim())
                    {
                        MessageBox.Show("Mã sản phẩm đã tồn tại.");
                        return;
                    }
                }

                dgv.Rows.Add(
                    txtMa.Text.Trim(),
                    txtTen.Text.Trim(),
                    gia
                );

                BaoTrangThai?.Invoke(
                    "Thêm sản phẩm thành công: " + txtTen.Text.Trim()
                );
            };

            dgv.SetBounds(20, 160, 640, 230);
            dgv.AllowUserToAddRows = false;
            dgv.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            dgv.Columns.Add("Ma", "Mã sản phẩm");
            dgv.Columns.Add("Ten", "Tên sản phẩm");
            dgv.Columns.Add("Gia", "Giá");

            Controls.AddRange(new Control[]
            {
                lblMa, txtMa,
                lblTen, txtTen,
                lblGia, txtGia,
                btnThem, dgv
            });
        }
    }
}
