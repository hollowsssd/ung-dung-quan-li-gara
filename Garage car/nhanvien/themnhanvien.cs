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

namespace Garage_car.Resources
{
    public partial class themnhanvien : Form
    {
        SqlConnection Connection;
        string chuoiketnoi;
        string manv;
        public themnhanvien(SqlConnection connection, string chuoi, string taomanhanvien)
        {
            InitializeComponent();
            this.Connection = connection;
            this.chuoiketnoi = chuoi;
            this.manv = taomanhanvien;

            txtmanv.Text=this.manv;

        }

        private void bttthem_Click(object sender, EventArgs e)
        {
            bool gioitinh = comboboxgioitinh.SelectedIndex == 0; // 0 cho Nam, 1 cho Nữ
            using (Connection = new SqlConnection(chuoiketnoi))
            {
                Connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO nhan_vien (ma_nv,ho_ten,so_dien_thoai,chuc_vu,email,diachi,gioitinh,ten_dang_nhap,mat_khau) VALUES (@ma_nv,@ho_ten,@so_dien_thoai,@chuc_vu,@email,@diachi,@gioitinh,@ten_dang_nhap,@mat_khau)", Connection);
                cmd.Parameters.AddWithValue("@ma_nv", manv);
                cmd.Parameters.AddWithValue("@ho_ten", txthoten.Text);
                cmd.Parameters.AddWithValue("@so_dien_thoai", txtsodienthoai.Text);
                cmd.Parameters.AddWithValue("@chuc_vu", txtchucvu.Text);
                cmd.Parameters.AddWithValue("@email", txtemal.Text);
                cmd.Parameters.AddWithValue("@diachi", txtdiachi.Text);
                cmd.Parameters.AddWithValue("@gioitinh", gioitinh);
                cmd.Parameters.AddWithValue("@ten_dang_nhap", txttendangnhap.Text);
                cmd.Parameters.AddWithValue("@mat_khau", txtmatkhaui.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Khách hàng đã được thêm thành công!");
                Connection.Close();
                this.Hide();
            }
        }
    }
}
