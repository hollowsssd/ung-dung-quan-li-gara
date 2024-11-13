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

namespace Garage_car
{
    public partial class themlichhen : Form
    {
        
        SqlConnection connection = null;
        String chuoi;
        string malh;
        public themlichhen(SqlConnection connection,string chuoi,String taomalh)
        {
            InitializeComponent();
            this.connection = connection;
            this.chuoi = chuoi;
            this.malh = taomalh;

            da.Format = DateTimePickerFormat.Custom;
            da.CustomFormat = "dd/MM/yyyy";
            da.MinDate = DateTime.Now;
            

            txtmalh.Text = taomalh;
            loaddata();
            

        }

        public themlichhen() { }


        private void bttthem_Click(object sender, EventArgs e)
        {
            using (connection = new SqlConnection(chuoi))
            {
                connection.Open(); 
                        
                        string manv = ((ComboBoxItem)comboBox1.SelectedItem).Ma;
                        DateTime dt = da.Value;
                        string trangthai = comboBox1trangthai.SelectedItem.ToString();

                        SqlCommand cmd = new SqlCommand("INSERT INTO lich_hen (ma_lich_hen, ma_nv,ngay_hen,mo_ta,trang_thai,ten_khach_hang,so_dien_thoai) VALUES (@ma_lich_hen, @ma_nv,@ngay_hen,@mo_ta,@trang_thai,@ten_khach_hang,@so_dien_thoai)", connection);
                        cmd.Parameters.AddWithValue("@ma_lich_hen", malh);
                        cmd.Parameters.AddWithValue("@ten_khach_hang", txttenkhachhang.Text);
                        cmd.Parameters.AddWithValue("@so_dien_thoai", txtsodienthoai.Text);
                        cmd.Parameters.AddWithValue("@mo_ta", txtmota.Text);
                        cmd.Parameters.AddWithValue("@trang_thai", trangthai);
                        
                        cmd.Parameters.AddWithValue("@ngay_hen", dt);
                        cmd.Parameters.AddWithValue("@ma_nv",manv);

                       

                cmd.ExecuteNonQuery();
                        MessageBox.Show("Khách hàng đã được thêm thành công!");
                        connection.Close();
                        this.Hide();
                

                connection.Close();
            }
        }

        private void loaddata()
        {
            using (SqlConnection connection = new SqlConnection(chuoi))
            {
                connection.Open();
                
                SqlCommand command = new SqlCommand("select ma_nv, ho_ten from nhan_vien", connection);
                SqlDataReader reader = command.ExecuteReader();

                List<ComboBoxItem> items = new List<ComboBoxItem>();

                while (reader.Read())
                {
                    // Thêm các item vào ComboBox
                    items.Add(new ComboBoxItem { Ma = reader["ma_nv"].ToString(), Ten = reader["ho_ten"].ToString() });
                }
                reader.Close();

                // Cập nhật DataSource của ComboBox
                comboBox1.DataSource = items;
                comboBox1.DisplayMember = "ToString"; // Hiển thị kết quả của phương thức ToString
                connection.Close();
            }
        }

    }
}
