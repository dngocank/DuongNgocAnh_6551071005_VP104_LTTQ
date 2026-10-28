using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace KiemTraGiuaKy
{
    public partial class Form1 : Form
    {
        private static readonly string[] CandidateConnectionStrings = new[]
        {
            "Server=localhost;Database=DuLieu_NhanSu;Integrated Security=True;TrustServerCertificate=True",
            "Server=.;Database=DuLieu_NhanSu;Integrated Security=True;TrustServerCertificate=True",
            "Server=.\\SQLEXPRESS;Database=DuLieu_NhanSu;Integrated Security=True;TrustServerCertificate=True",
            "Server=(localdb)\\MSSQLLocalDB;Database=DuLieu_NhanSu;Integrated Security=True;TrustServerCertificate=True"
        };

        private string connectionString = CandidateConnectionStrings[0];

        private string maNVChonDeSua = "";

        public Form1()
        {
            InitializeComponent();
            XacDinhConnectionString();
            KhoiTaoGiaoDienVaIcon();
        }

        private void XacDinhConnectionString()
        {
            foreach (var str in CandidateConnectionStrings)
            {
                try
                {
                    using (SqlConnection testConn = new SqlConnection(str))
                    {
                        testConn.Open();
                        connectionString = str;
                        return;
                    }
                }
                catch
                {
                }
            }
        }

        private void KhoiTaoGiaoDienVaIcon()
        {
            try
            {
                Bitmap bmpThem = new Bitmap(18, 18);
                using (Graphics g = Graphics.FromImage(bmpThem))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    using (Brush b = new SolidBrush(Color.FromArgb(46, 125, 50)))
                    {
                        g.FillEllipse(b, 1, 1, 15, 15);
                    }
                    using (Pen p = new Pen(Color.White, 2f))
                    {
                        g.DrawLine(p, 8.5f, 4.5f, 8.5f, 12.5f);
                        g.DrawLine(p, 4.5f, 8.5f, 12.5f, 8.5f);
                    }
                }
                btnThem.Image = bmpThem;

                Bitmap bmpThoat = new Bitmap(18, 18);
                using (Graphics g = Graphics.FromImage(bmpThoat))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    using (Brush b = new SolidBrush(Color.FromArgb(211, 47, 47)))
                    {
                        g.FillRectangle(b, 2, 2, 14, 14);
                    }
                    using (Pen p = new Pen(Color.White, 2f))
                    {
                        g.DrawLine(p, 5, 5, 13, 13);
                        g.DrawLine(p, 13, 5, 5, 13);
                    }
                }
                btnThoat.Image = bmpThoat;

                Bitmap bmpSua = new Bitmap(18, 18);
                using (Graphics g = Graphics.FromImage(bmpSua))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    using (Pen p = new Pen(Color.FromArgb(100, 100, 100), 2.5f))
                    {
                        g.DrawLine(p, 4, 13, 12, 5);
                    }
                    using (Brush b = new SolidBrush(Color.FromArgb(255, 179, 0)))
                    {
                        g.FillPolygon(b, new PointF[] { new PointF(12, 3), new PointF(15, 6), new PointF(13, 8), new PointF(10, 5) });
                    }
                    using (Brush b2 = new SolidBrush(Color.FromArgb(33, 33, 33)))
                    {
                        g.FillPolygon(b2, new PointF[] { new PointF(3, 14), new PointF(6, 14), new PointF(3, 11) });
                    }
                }
                btnSua.Image = bmpSua;

                Bitmap bmpXoa = new Bitmap(18, 18);
                using (Graphics g = Graphics.FromImage(bmpXoa))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    using (Brush b = new SolidBrush(Color.FromArgb(120, 120, 120)))
                    {
                        g.FillRectangle(b, 4, 3, 10, 2);
                        g.FillRectangle(b, 5, 6, 8, 9);
                    }
                }
                btnXoa.Image = bmpXoa;
            }
            catch
            {
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            btnThem.Enabled = true;
            btnThoat.Enabled = true;

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MaNhanVien, TenNhanVien, LuongCoBan FROM NhanVien";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvNhanVien.DataSource = dt;
                    }
                }

                if (dgvNhanVien.Columns["MaNhanVien"] != null)
                    dgvNhanVien.Columns["MaNhanVien"]!.HeaderText = "Mã Nhân Viên";

                if (dgvNhanVien.Columns["TenNhanVien"] != null)
                    dgvNhanVien.Columns["TenNhanVien"]!.HeaderText = "Tên Nhân Viên";

                if (dgvNhanVien.Columns["LuongCoBan"] != null)
                    dgvNhanVien.Columns["LuongCoBan"]!.HeaderText = "Lương Cơ Bản";

                dgvNhanVien.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtLuongCB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if ((e.KeyChar == '.' || e.KeyChar == ',') &&
                !txtLuongCB.Text.Contains('.') &&
                !txtLuongCB.Text.Contains(','))
            {
                return;
            }

            e.Handled = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có muốn thoát không",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNV.Text.Trim();
            string tenNV = txtTenNV.Text.Trim();
            string luongCB = txtLuongCB.Text.Trim();

            if (string.IsNullOrEmpty(maNV) || string.IsNullOrEmpty(tenNV) || string.IsNullOrEmpty(luongCB))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ dữ liệu vào các ô TextBox!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (string.IsNullOrEmpty(maNV)) txtMaNV.Focus();
                else if (string.IsNullOrEmpty(tenNV)) txtTenNV.Focus();
                else txtLuongCB.Focus();
                return;
            }

            if (KiemTraTrungMaNV(maNV))
            {
                MessageBox.Show("Mã nhân viên đã tồn tại, vui lòng nhập lại!", "Trùng mã nhân viên", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaNV.Focus();
                txtMaNV.SelectAll();
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string insertSql = "INSERT INTO NhanVien (MaNhanVien, TenNhanVien, LuongCoBan) VALUES (@MaNV, @TenNV, @LuongCB)";
                    using (SqlCommand cmd = new SqlCommand(insertSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        cmd.Parameters.AddWithValue("@TenNV", tenNV);
                        cmd.Parameters.AddWithValue("@LuongCB", luongCB);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadData();

                XoaRongTextBox();

                btnThem.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool KiemTraTrungMaNV(string maNV)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT COUNT(*) FROM NhanVien WHERE MaNhanVien = @MaNV";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        object? result = cmd.ExecuteScalar();
                        int count = result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra mã nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvNhanVien.Rows.Count)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
                if (row.Cells["MaNhanVien"]?.Value != null)
                {
                    maNVChonDeSua = row.Cells["MaNhanVien"].Value!.ToString() ?? "";

                    txtMaNV.Text = maNVChonDeSua;
                    txtTenNV.Text = row.Cells["TenNhanVien"].Value?.ToString() ?? "";
                    txtLuongCB.Text = row.Cells["LuongCoBan"].Value?.ToString() ?? "";

                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;

                    txtMaNV.ReadOnly = true;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNV.Text.Trim();
            if (string.IsNullOrEmpty(maNV))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có muốn xóa nhân viên này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string deleteSql = "DELETE FROM NhanVien WHERE MaNhanVien = @MaNV";
                        using (SqlCommand cmd = new SqlCommand(deleteSql, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaNV", maNV);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            XoaRongTextBox();
            txtMaNV.ReadOnly = false;
            maNVChonDeSua = "";
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThem.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maNV = string.IsNullOrEmpty(maNVChonDeSua) ? txtMaNV.Text.Trim() : maNVChonDeSua;
            string tenNV = txtTenNV.Text.Trim();
            string luongCB = txtLuongCB.Text.Trim();

            if (string.IsNullOrEmpty(maNV))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(tenNV) || string.IsNullOrEmpty(luongCB))
            {
                MessageBox.Show("Vui lòng không để trống thông tin Tên nhân viên và Lương cơ bản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string updateSql = "UPDATE NhanVien SET TenNhanVien = @TenNV, LuongCoBan = @LuongCB WHERE MaNhanVien = @MaNV";
                    using (SqlCommand cmd = new SqlCommand(updateSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenNV", tenNV);
                        cmd.Parameters.AddWithValue("@LuongCB", luongCB);
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Cập nhật thông tin nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            XoaRongTextBox();
            txtMaNV.ReadOnly = false;
            maNVChonDeSua = "";
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThem.Enabled = true;
        }

        private void XoaRongTextBox()
        {
            txtMaNV.Clear();
            txtTenNV.Clear();
            txtLuongCB.Clear();
        }

        private void txtMaNV_TextChanged(object sender, EventArgs e)
        {
            if (!btnSua.Enabled && !btnXoa.Enabled && !btnThem.Enabled && !string.IsNullOrWhiteSpace(txtMaNV.Text))
            {
                btnThem.Enabled = true;
            }
        }
    }
}
