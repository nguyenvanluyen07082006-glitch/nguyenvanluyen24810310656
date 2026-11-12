
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Bai5_4_QuanLyNhanVien.Models;

namespace Bai5_4_QuanLyNhanVien
{
    public partial class Form1 : Form
    {
        // Danh sách nhân viên
        List<Employee> danhSachNhanVien = new List<Employee>();

        public Form1()
        {
            InitializeComponent();

            // Gắn sự kiện
            tvDepartments.AfterSelect += tvDepartments_AfterSelect;

            cboViewMode.SelectedIndexChanged += cboViewMode_SelectedIndexChanged;

            // Khởi tạo dữ liệu
            TaoDuLieuNhanVien();

            TaoCayPhongBan();

            TaoCotListView();

            // Chọn chế độ Details mặc định
            cboViewMode.SelectedIndex = 0;

            // Mở rộng cây
            tvDepartments.ExpandAll();
        }

        // ==================================
        // 1. TẠO DỮ LIỆU NHÂN VIÊN
        // ==================================

        void TaoDuLieuNhanVien()
        {
            danhSachNhanVien.Add(new Employee
            {
                EmployeeId = "NV001",
                FullName = "Nguyễn Văn An",
                Position = "Lập trình viên",
                StartDate = new DateTime(2024, 1, 10)
            });

            danhSachNhanVien.Add(new Employee
            {
                EmployeeId = "NV002",
                FullName = "Trần Văn Bình",
                Position = "Trưởng nhóm",
                StartDate = new DateTime(2023, 3, 15)
            });

            danhSachNhanVien.Add(new Employee
            {
                EmployeeId = "NV003",
                FullName = "Lê Thị Hoa",
                Position = "Kiểm thử viên",
                StartDate = new DateTime(2024, 5, 20)
            });

            danhSachNhanVien.Add(new Employee
            {
                EmployeeId = "NV004",
                FullName = "Phạm Văn Nam",
                Position = "Nhân viên bán hàng",
                StartDate = new DateTime(2022, 8, 12)
            });

            danhSachNhanVien.Add(new Employee
            {
                EmployeeId = "NV005",
                FullName = "Hoàng Thị Lan",
                Position = "Chăm sóc khách hàng",
                StartDate = new DateTime(2023, 6, 1)
            });

            danhSachNhanVien.Add(new Employee
            {
                EmployeeId = "NV006",
                FullName = "Đỗ Văn Hùng",
                Position = "Nhân viên nhân sự",
                StartDate = new DateTime(2021, 9, 5)
            });
        }

        // ==================================
        // 2. TẠO CÂY PHÒNG BAN
        // ==================================

        void TaoCayPhongBan()
        {
            tvDepartments.Nodes.Clear();

            TreeNode congTy = new TreeNode("Công ty");

            // Phòng kỹ thuật
            TreeNode kyThuat = new TreeNode("Phòng Kỹ thuật");

            kyThuat.Nodes.Add(new TreeNode("Nhóm Phát triển"));
            kyThuat.Nodes.Add(new TreeNode("Nhóm Kiểm thử"));

            // Phòng kinh doanh
            TreeNode kinhDoanh = new TreeNode("Phòng Kinh doanh");

            kinhDoanh.Nodes.Add(new TreeNode("Nhóm Bán hàng"));
            kinhDoanh.Nodes.Add(new TreeNode("Nhóm Chăm sóc khách hàng"));

            // Phòng nhân sự
            TreeNode nhanSu = new TreeNode("Phòng Nhân sự");

            nhanSu.Nodes.Add(new TreeNode("Nhóm Tuyển dụng"));

            // Thêm phòng ban vào công ty
            congTy.Nodes.Add(kyThuat);
            congTy.Nodes.Add(kinhDoanh);
            congTy.Nodes.Add(nhanSu);

            // Thêm công ty vào TreeView
            tvDepartments.Nodes.Add(congTy);
        }

        // ==================================
        // 3. TẠO CÁC CỘT LISTVIEW
        // ==================================

        void TaoCotListView()
        {
            lsvEmployees.Columns.Clear();

            lsvEmployees.Columns.Add("Mã NV", 100);
            lsvEmployees.Columns.Add("Họ Tên", 200);
            lsvEmployees.Columns.Add("Chức vụ", 180);
            lsvEmployees.Columns.Add("Ngày vào làm", 150);
        }

        // ==================================
        // 4. XỬ LÝ KHI CHỌN NODE
        // ==================================

        private void tvDepartments_AfterSelect(
            object sender,
            TreeViewEventArgs e)
        {
            string tenNode = e.Node.Text;

            HienThiNhanVien(tenNode);
        }

        // ==================================
        // 5. HIỂN THỊ NHÂN VIÊN
        // ==================================

        void HienThiNhanVien(string tenNode)
        {
            lsvEmployees.Items.Clear();

            foreach (Employee nv in danhSachNhanVien)
            {
                bool hienThi = false;

                // Công ty hiển thị toàn bộ
                if (tenNode == "Công ty")
                {
                    hienThi = true;
                }

                // Phòng kỹ thuật
                else if (tenNode == "Phòng Kỹ thuật")
                {
                    if (nv.EmployeeId == "NV001" ||
                        nv.EmployeeId == "NV002" ||
                        nv.EmployeeId == "NV003")
                    {
                        hienThi = true;
                    }
                }

                // Nhóm phát triển
                else if (tenNode == "Nhóm Phát triển")
                {
                    if (nv.EmployeeId == "NV001" ||
                        nv.EmployeeId == "NV002")
                    {
                        hienThi = true;
                    }
                }

                // Nhóm kiểm thử
                else if (tenNode == "Nhóm Kiểm thử")
                {
                    if (nv.EmployeeId == "NV003")
                    {
                        hienThi = true;
                    }
                }

                // Phòng kinh doanh
                else if (tenNode == "Phòng Kinh doanh")
                {
                    if (nv.EmployeeId == "NV004" ||
                        nv.EmployeeId == "NV005")
                    {
                        hienThi = true;
                    }
                }

                // Nhóm bán hàng
                else if (tenNode == "Nhóm Bán hàng")
                {
                    if (nv.EmployeeId == "NV004")
                    {
                        hienThi = true;
                    }
                }

                // Nhóm chăm sóc khách hàng
                else if (tenNode == "Nhóm Chăm sóc khách hàng")
                {
                    if (nv.EmployeeId == "NV005")
                    {
                        hienThi = true;
                    }
                }

                // Phòng nhân sự
                else if (tenNode == "Phòng Nhân sự" ||
                         tenNode == "Nhóm Tuyển dụng")
                {
                    if (nv.EmployeeId == "NV006")
                    {
                        hienThi = true;
                    }
                }

                if (hienThi)
                {
                    ListViewItem item = new ListViewItem(nv.EmployeeId);

                    item.SubItems.Add(nv.FullName);
                    item.SubItems.Add(nv.Position);
                    item.SubItems.Add(nv.StartDate.ToString("dd/MM/yyyy"));

                    lsvEmployees.Items.Add(item);
                }
            }
        }

        // ==================================
        // 6. CHUYỂN ĐỔI CHẾ ĐỘ HIỂN THỊ
        // ==================================

        private void cboViewMode_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (cboViewMode.SelectedIndex == 0)
            {
                lsvEmployees.View = View.Details;
            }
            else if (cboViewMode.SelectedIndex == 1)
            {
                lsvEmployees.View = View.SmallIcon;
            }
            else if (cboViewMode.SelectedIndex == 2)
            {
                lsvEmployees.View = View.LargeIcon;
            }
            else if (cboViewMode.SelectedIndex == 3)
            {
                lsvEmployees.View = View.Tile;
            }
        }
    }
}
