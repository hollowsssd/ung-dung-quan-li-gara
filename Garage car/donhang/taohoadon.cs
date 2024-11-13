using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Garage_car
{
    public partial class taohoadon : Form
    {

            public string manv { get; private set; }//{ get; set; }:
                                                    //  get: Cho phép đọc giá trị của thuộc tính.
                                                    // set: Cho phép gán giá trị mới cho thuộc tính.
            public string hoten { get; private set; }

            chuoiconnect chuoiconnect = new chuoiconnect();
            SqlConnection connection = null;
            String chuoi = chuoiconnect.chuoi();
        public taohoadon(string manv, string hoten, string taomahodon)
        {
            InitializeComponent();

            this.manv = manv;
            this.hoten = hoten;
            txtsohoadon.Text = taomahodon;
            txtthoigian.Text = DateTime.Now.ToString();

            txtmatennv.Text = $"{this.manv}|{this.hoten}";




        }

        //loadcomboboxmakh|tenkh
        private void comboBoxmatenkh_DropDown(object sender, EventArgs e)

        {
            using (connection = new SqlConnection(chuoi))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("select ma_kh, ho_ten from khach_hang", connection);
                SqlDataReader reader = command.ExecuteReader();

                List<ComboBoxItem> items = new List<ComboBoxItem>();

                while (reader.Read())
                {
                    // Thêm các item vào ComboBox
                    items.Add(new ComboBoxItem { Ma = reader["ma_kh"].ToString(), Ten = reader["ho_ten"].ToString() });
                }
                reader.Close();

                // Cập nhật DataSource của ComboBox
                comboBoxmatenkh.DataSource = items;
                comboBoxmatenkh.DisplayMember = "ToString"; // Hiển thị kết quả của phương thức ToString
                connection.Close();
            }
        }

        //load combobox tenphutung va so luong trong kho
        private void comboBoxphutonkho_DropDown(object sender, EventArgs e)
        {
            using (connection = new SqlConnection(chuoi))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("select ten_pt, so_luong from phu_tung", connection);
                SqlDataReader reader = command.ExecuteReader();

                List<ComboBoxItem> items = new List<ComboBoxItem>();

                while (reader.Read())
                {
                    // Thêm các item vào ComboBox
                    items.Add(new ComboBoxItem { Ma = reader["ten_pt"].ToString(), Ten = reader["so_luong"].ToString() });
                }
                reader.Close();

                // Cập nhật DataSource của ComboBox
                comboBoxphutonkho.DataSource = items;
                comboBoxphutonkho.DisplayMember = "ToString"; // Hiển thị kết quả của phương thức ToString
                connection.Close();
            }
        }
        private void comboBoxphutonkho_SelectedValueChanged(object sender, EventArgs e)
        {
            using (connection = new SqlConnection(chuoi))
            {
                connection.Open();
                string tenpt = ((ComboBoxItem)comboBoxphutonkho.SelectedItem).Ma;

                SqlCommand command = new SqlCommand("select gia from phu_tung where ten_pt=@ten_pt", connection);
                command.Parameters.AddWithValue("@ten_pt", tenpt);
                // Sử dụng ExecuteScalar để lấy giá trị từ kết quả truy vấn
                decimal gia = (decimal)command.ExecuteScalar();
                txtdongia.Text = gia.ToString();
                connection.Close();

            }
        }

        //load dichvu
        private void comboBoxtendichvu_DropDown(object sender, EventArgs e)
        {
            using (connection = new SqlConnection(chuoi))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("select ten_dv from dich_vu", connection);
                SqlDataReader reader = command.ExecuteReader();

                List<ComboBoxItem> items = new List<ComboBoxItem>();

                while (reader.Read())
                {
                    // Thêm các item vào ComboBox
                    items.Add(new ComboBoxItem { Ma = reader["ten_dv"].ToString() });
                }
                reader.Close();

                // Cập nhật DataSource của ComboBox
                comboBoxtendichvu.DataSource = items;

                connection.Close();
            }
        }

        private void comboBoxtendichvu_SelectedValueChanged(object sender, EventArgs e)
        {
            using (connection = new SqlConnection(chuoi))
            {
                connection.Open();
                string tendv = ((ComboBoxItem)comboBoxtendichvu.SelectedItem).Ma;

                SqlCommand command = new SqlCommand("select gia from dich_vu where ten_dv=@ten_dv", connection);
                command.Parameters.AddWithValue("@ten_dv", tendv);
                // Sử dụng ExecuteScalar để lấy giá trị từ kết quả truy vấn
                decimal gia = (decimal)command.ExecuteScalar();
                txtdongiadichvu.Text = gia.ToString();
                connection.Close();

            }

        }

        private void bttthempt_Click(object sender, EventArgs e)
        {
            using (connection = new SqlConnection(chuoi))
            {
                connection.Open();
                string ten_pt = ((ComboBoxItem)comboBoxphutonkho.SelectedItem).Ma;

                decimal dongia = decimal.Parse(txtdongia.Text);
                int soluong = int.Parse(txtsoluong.Text);
                decimal thanhtien = (decimal)soluong * dongia;
                SqlCommand cmd = new SqlCommand("select ma_pt from phu_tung where ten_pt=@ten_pt and gia=@dongia", connection);
                cmd.Parameters.AddWithValue("@ten_pt", ten_pt);
                cmd.Parameters.AddWithValue("@dongia", dongia);
                string mapt = (string)cmd.ExecuteScalar();
                connection.Close();

                dataphutung.Rows.Add(mapt, ten_pt, soluong, thanhtien);

                txtsoluong.Text = null;
                UpdateTongThanhTien(thanhtien);

            }
        }

        private void bttthemdv_Click(object sender, EventArgs e)
        {
            using (connection = new SqlConnection(chuoi))
            {
                connection.Open();
                string ten_dv = ((ComboBoxItem)comboBoxtendichvu.SelectedItem).Ma;

                decimal thanhtien = decimal.Parse(txtdongiadichvu.Text);

                SqlCommand cmd = new SqlCommand("select ma_dv from dich_vu where ten_dv=@ten_dv ", connection);
                cmd.Parameters.AddWithValue("@ten_dv", ten_dv);
                string madv = (string)cmd.ExecuteScalar();
                connection.Close();

                datadichvu.Rows.Add(madv, ten_dv, thanhtien);

                UpdateTongThanhTien(thanhtien);

            }

        }
        private void UpdateTongThanhTien(decimal thanhtien)
        {
            if (decimal.TryParse(txttongthanhtien.Text, out decimal tongThanhTien))
            {
                // Cộng thêm thành tiền vừa nhập vào tổng thành tiền hiện tại
                tongThanhTien += thanhtien;
            }
            else
            {
                // Nếu tổng thành tiền ban đầu không hợp lệ (ví dụ rỗng hoặc không phải số), khởi tạo bằng thành tiền mới
                tongThanhTien = thanhtien;
            }

            // Cập nhật tổng thành tiền mới vào TextBox txtTongThanhTien
            txttongthanhtien.Text = tongThanhTien.ToString();
        }

        private void bttxoa_Click(object sender, EventArgs e)
        {
            if (dataphutung.SelectedRows.Count > 0)
            {
                // Xóa tất cả các dòng được chọn
                foreach (DataGridViewRow row in dataphutung.SelectedRows)
                {
                    // Xóa dòng
                    dataphutung.Rows.Remove(row);
                }
            }
            else if (datadichvu.SelectedRows.Count > 0)
            {
                // Xóa tất cả các dòng được chọn
                foreach (DataGridViewRow row in datadichvu.SelectedRows)
                {
                    // Xóa dòng
                    datadichvu.Rows.Remove(row);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void bttthanhtoan_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("ban co muon thanh toan?", "xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(chuoi))
                    {
                        connection.Open();

                        string mahd = txtsohoadon.Text;
                        string makh = ((ComboBoxItem)comboBoxmatenkh.SelectedItem).Ma;
                        string manv = this.manv;
                        DateTime da = DateTime.Parse(txtthoigian.Text);
                        decimal tongtien = decimal.Parse(txttongthanhtien.Text);

                        SqlCommand Command = new SqlCommand("INSERT INTO hoa_don (ma_hd, ma_kh, ma_nv,ngay_hd,tong_tien) VALUES (@mahd, @makh, @manv, @ngayhd,@tongtien)", connection);
                        Command.Parameters.AddWithValue("@mahd", mahd);
                        Command.Parameters.AddWithValue("@makh", makh);
                        Command.Parameters.AddWithValue("@manv", manv);
                        Command.Parameters.AddWithValue("@ngayhd", da);
                        Command.Parameters.AddWithValue("@tongtien", tongtien);
                        Command.ExecuteNonQuery();

                        // Lặp qua từng dòng của Dataphutung
                        foreach (DataGridViewRow row in dataphutung.Rows)
                        {
                            // Kiểm tra nếu dòng không phải là dòng mới
                            if (!row.IsNewRow)
                            {
                                // Lấy dữ liệu từ các cột trong DataGridView

                                string maPt = row.Cells["ma_pt"].Value.ToString();
                                int soLuong = Convert.ToInt32(row.Cells["so_luong"].Value);
                                decimal thanhtien = Convert.ToDecimal(row.Cells["gia"].Value);

                                // Câu lệnh SQL để chèn dữ liệu vào bảng chi tiết hóa đơn

                                using (SqlCommand command = new SqlCommand("INSERT INTO chi_tiet_hoa_don (ma_hd, ma_pt, so_luong, don_gia) VALUES (@mahd, @maPt, @soLuong, @donGia)", connection))
                                {
                                    // Thêm tham số vào câu lệnh SQL
                                    command.Parameters.AddWithValue("@mahd", mahd);
                                    command.Parameters.AddWithValue("@maPt", maPt);
                                    command.Parameters.AddWithValue("@soLuong", soLuong);
                                    command.Parameters.AddWithValue("@donGia", thanhtien);

                                    // Thực thi câu lệnh SQL
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                        // Lặp qua từng dòng của Datadichvu
                        foreach (DataGridViewRow row in datadichvu.Rows)
                        {
                            // Kiểm tra nếu dòng không phải là dòng mới
                            if (!row.IsNewRow)
                            {
                                // Lấy dữ liệu từ các cột trong DataGridView
                                string madv = row.Cells["ma_dv"].Value.ToString();
                                decimal thanhtien = Convert.ToDecimal(row.Cells["thanhtien"].Value);

                                // Câu lệnh SQL để chèn dữ liệu vào bảng chi tiết hóa đơn

                                using (SqlCommand command = new SqlCommand("INSERT INTO chi_tiet_hoa_don (ma_hd, ma_dv, so_luong, don_gia) VALUES (@maHd, @ma_dv, 1, @donGia)", connection))
                                {
                                    // Thêm tham số vào câu lệnh SQL
                                    command.Parameters.AddWithValue("@maHd", mahd);
                                    command.Parameters.AddWithValue("@ma_dv", madv);
                                    command.Parameters.AddWithValue("@donGia", thanhtien);

                                    // Thực thi câu lệnh SQL
                                    command.ExecuteNonQuery();
                                }
                            }
                        }

                        connection.Close();
                        if (MessageBox.Show("Bạn có muốn in hóa đơn?", "Xác nhận in", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            PrintDialog printDialog = new PrintDialog();
                            PrintDocument printDocument = new PrintDocument();
                            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);

                            printDialog.Document = printDocument;

                            if (printDialog.ShowDialog() == DialogResult.OK)
                            {
                                printDocument.Print();
                            }
                        }
                        MessageBox.Show("thanh toan thanh cong");
                        this.Hide();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {

                MessageBox.Show("bạn đã hủy.");
            }


        }
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Tạo nội dung hóa đơn cần in
            string content = "HÓA ĐƠN\n";
            content += $"Mã hóa đơn: {txtsohoadon.Text}\n";
            content += $"Ngày: {DateTime.Now}\n";
            content += $"Tổng tiền: {txttongthanhtien.Text}\n\n";

            // Thêm thông tin các mặt hàng từ DataGridView
            foreach (DataGridViewRow row in dataphutung.Rows)
            {
                if (!row.IsNewRow)
                {
                    content += $"Phụ tùng: {row.Cells["tenPt"].Value} - SL: {row.Cells["so_luong"].Value} - Giá: {row.Cells["gia"].Value}\n";
                }
            }

            foreach (DataGridViewRow row in datadichvu.Rows)
            {
                if (!row.IsNewRow)
                {
                    content += $"Dịch vụ: {row.Cells["tenDv"].Value} - Giá: {row.Cells["thanhtien"].Value}\n";
                }
            }

            // In nội dung lên trang giấy
            e.Graphics.DrawString(content, new Font("Arial", 12), Brushes.Black, 100, 100);


        }

        private void datadichvu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBoxmatenkh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
