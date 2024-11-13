using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Garage_car
{
    public partial class xoasualichhen : Form
    {

        SqlConnection Connection = null;
        string chuoiketnoi;

        public xoasualichhen(string ma_lich_hen, string ma_nv,string ngay_hen,string mo_ta, string ten_khach_hang, string so_dien_thoai, string trang_thai, SqlConnection connection, string chuoi)
        {
            InitializeComponent();
            txtmalh.Text = ma_lich_hen;
            this.chuoiketnoi= chuoi;
            this.Connection = connection;


            txtmota.Text = mo_ta;
            da.Value = DateTime.Parse(ngay_hen);

            txttenkhachhang.Text = ten_khach_hang;
            txtsodienthoai.Text = so_dien_thoai;
            comboBox1trangthai.SelectedItem = trang_thai;
            // Nạp dữ liệu vào ComboBox
            loadcombobox();

            // Chọn phần tử trong ComboBox dựa trên maNv
            SetSelectedNhanVien(ma_nv);

            // Thiết lập định dạng cho DateTimePicker
            da.Format = DateTimePickerFormat.Custom;
            da.CustomFormat = "dd/MM/yyyy HH:mm";
        }
        private void SelectComboBoxItemByMaNv(string maNv)
        {
            foreach (var item in comboBox1.Items)
            {
                string itemText = item.ToString(); // Lấy chuỗi hiện tại của item trong ComboBox
                string[] parts = itemText.Split('|'); // Tách chuỗi theo ký tự '|'

                if (parts.Length == 2 && parts[0] == maNv) // So sánh mã nhân viên
                {
                    comboBox1.SelectedItem = item;
                    break;
                }
            }
        }
        public void SetSelectedNhanVien(string maNv)
        {
            SelectComboBoxItemByMaNv(maNv);
        }

        private void bttCapNhat_Click(object sender, EventArgs e)
        {
            using (Connection = new SqlConnection(chuoiketnoi))
            {
                Connection.Open();


                
                string manv = ((ComboBoxItem)comboBox1.SelectedItem).Ma; 

             
                string malh = txtmalh.Text;
                DateTime dt = da.Value;
                string trangthai = comboBox1trangthai.SelectedItem.ToString();

                SqlCommand cmd = new SqlCommand("UPDATE lich_hen SET ma_nv = @ma_nv, ngay_hen = @ngay_hen, mo_ta = @mo_ta, trang_thai = @trang_thai, ten_khach_hang = @ten_khach_hang, so_dien_thoai = @so_dien_thoai WHERE ma_lich_hen = @ma_lich_hen", Connection);

                cmd.Parameters.AddWithValue("@ma_lich_hen", malh);
                cmd.Parameters.AddWithValue("@ma_nv", manv); 
                cmd.Parameters.AddWithValue("@ngay_hen", dt);
                cmd.Parameters.AddWithValue("@mo_ta", txtmota.Text);
                cmd.Parameters.AddWithValue("@trang_thai", trangthai);
                cmd.Parameters.AddWithValue("@ten_khach_hang", txttenkhachhang.Text);
                cmd.Parameters.AddWithValue("@so_dien_thoai", txtsodienthoai.Text);

                // Thực thi câu lệnh SQL
                cmd.ExecuteNonQuery();

                // Hiển thị thông báo thành công
                MessageBox.Show("Lịch hẹn đã được cập nhật thành công!");

                // Đóng kết nối và ẩn form
                Connection.Close();
                this.Hide();
            }
        }

        private void bttXoa_Click(object sender, EventArgs e)
        {
            using (Connection = new SqlConnection(chuoiketnoi))
            {
                Connection.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM lich_hen WHERE ma_lich_hen = @ma_lich_hen", Connection);
                cmd.Parameters.AddWithValue("@ma_lich_hen", txtmalh.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Lịch hẹn đã được xóa thành công!");

                Connection.Close();
                this.Hide();
            }
        }

        private void loadcombobox()
        {
            using (SqlConnection connection = new SqlConnection(chuoiketnoi))
            {
                connection.Open();
                comboBox1.Items.Clear();
                SqlCommand command = new SqlCommand("SELECT ma_nv, ho_ten FROM nhan_vien", connection);
                SqlDataReader reader = command.ExecuteReader();

                List<ComboBoxItem> items = new List<ComboBoxItem>();

                while (reader.Read())
                {
                    items.Add(new ComboBoxItem { Ma = reader["ma_nv"].ToString(), Ten = reader["ho_ten"].ToString() });
                }
                reader.Close();

                comboBox1.DataSource = items;
                comboBox1.DisplayMember = "ToString";

                connection.Close();
            }
        }
    }
}
