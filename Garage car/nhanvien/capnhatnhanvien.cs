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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static TheArtOfDevHtmlRenderer.Adapters.RGraphicsPath;

namespace Garage_car
{
    public partial class capnhatnhanvien : Form
    {   
        SqlConnection conn;
        string chuoiketnoi;
        string manv;
        public capnhatnhanvien(string manv, string hoTen, string soDienThoai, string chucvu, string email,string diachi, bool gioitinh,string  ten_dang_nhap,string mat_khau, SqlConnection connection, string chuoi)
        {
            InitializeComponent();
            conn = connection;
            chuoiketnoi = chuoi;
            this.manv = manv;

            txtmanv.Text = manv;
            txthoten.Text = hoTen;
            txtsodienthoai.Text = soDienThoai;
            txtchucvu.Text = chucvu;
            txtemal.Text = email;
            txtdiachi.Text = diachi;
            txttendangnhap.Text= ten_dang_nhap;
            txtmatkhaui.Text = mat_khau;

            // Giả sử bạn có ComboBox với các giá trị như "Nam" và "Nữ"
            comboboxgioitinh.Items.Clear();
            comboboxgioitinh.Items.Add("Nam");
            comboboxgioitinh.Items.Add("Nữ");
            comboboxgioitinh.SelectedIndex = gioitinh ? 0 : 1;
        }
       

        private void bttxoa_Click(object sender, EventArgs e)   
        {
            using (conn = new SqlConnection(chuoiketnoi))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete from nhan_vien where ma_nv=@ma_nv", conn);
                cmd.Parameters.AddWithValue("@ma_nv", txtmanv.Text);
                cmd.ExecuteNonQuery(); // được dùng cho các câu lệnh UPDATE, INSERT, hoặc DELETE
                MessageBox.Show("da xoa thanh cong");
                conn.Close();
                this.Hide();
            }
        }

        private void bttluu_Click(object sender, EventArgs e)
        {
            bool gioitinh = comboboxgioitinh.SelectedIndex == 0; // 0 cho Nam, 1 cho Nữ

            using (conn = new SqlConnection(chuoiketnoi))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE nhan_vien SET ho_ten = @ho_ten, so_dien_thoai = @so_dien_thoai, chuc_vu = @chuc_vu, email = @email, gioitinh = @gioitinh,diachi=@diachi,ten_dang_nhap=@ten_dang_nhap, mat_khau =@mat_khau WHERE ma_nv = @ma_nv", conn);
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
                MessageBox.Show("Cập nhật nhân viên thành công!");
                conn.Close();
                this.Hide();
            }
        }
    }
}
