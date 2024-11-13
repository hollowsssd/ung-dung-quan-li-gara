using Guna.UI2.WinForms;
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
    public partial class capnhatkhachhang : Form
    {
        SqlConnection connec;
        string chuoi;
       
        string makh;
        public capnhatkhachhang(string maKH, string hoTen, string soDienThoai, string biensoxe,SqlConnection connect,string chuoi)
        {
            InitializeComponent();
            this.connec=connect;
            this.chuoi = chuoi;
            this.makh = maKH;

            txtmakh.Text = maKH;
            txthoten.Text = hoTen;  
            txtsdt.Text = soDienThoai;  
            txtbiensoxe.Text = biensoxe;    

        }

        private void bttxoa_Click(object sender, EventArgs e)
        {
                    using(  connec =new SqlConnection(chuoi))
                    { 
                        connec.Open();
                        SqlCommand cmd = new SqlCommand("delete from khach_hang where ma_kh=@ma_kh",connec);
                        cmd.Parameters.AddWithValue("@ma_kh",txtmakh.Text);
                        cmd.ExecuteNonQuery(); // được dùng cho các câu lệnh UPDATE, INSERT, hoặc DELETE
                         MessageBox.Show("da xoa thanh cong");
                        connec.Close();
                        this.Hide();    
                    }
        }

        private void bttluu_Click(object sender, EventArgs e)
        {

            using (connec = new SqlConnection(chuoi))
            {
                connec.Open();
                SqlCommand cmd = new SqlCommand("update khach_hang set ho_ten=@ho_ten,so_dien_thoai=@so_dien_thoai,bien_so_xe=@bien_so_xe where ma_kh=@ma_kh", connec);
                cmd.Parameters.AddWithValue("@ma_kh",makh);
                cmd.Parameters.AddWithValue("@ho_ten", txthoten.Text); 
                cmd.Parameters.AddWithValue("@so_dien_thoai", txtsdt.Text);
                cmd.Parameters.AddWithValue("@bien_so_xe", txtbiensoxe.Text);
                cmd.ExecuteNonQuery ();
                MessageBox.Show("Khách hàng đã được cập nhật thành công!");
                connec.Close();
                this.Close();


       
            }
        }
    }
}
