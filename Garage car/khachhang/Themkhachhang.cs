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
    public partial class Themkhachhang : Form
    {   
        SqlConnection connection;
        string chuoiketnoi;
        string makh;
        
        public Themkhachhang(SqlConnection connect,string chuoi,string taomakhachhang)
        {
            InitializeComponent();
            connection = connect;
            chuoiketnoi=chuoi;
            makh = taomakhachhang;
            txtmakh.Text=makh;

        }
       
       

        private void guna2Button1Save_Click(object sender, EventArgs e)
        {
            
            using (connection = new SqlConnection(chuoiketnoi))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO khach_hang (ma_kh, ho_ten, so_dien_thoai, bien_so_xe) VALUES (@ma_kh, @ho_ten, @so_dien_thoai, @bien_so_xe)", connection);
                cmd.Parameters.AddWithValue("@ma_kh", makh);
                cmd.Parameters.AddWithValue("@ho_ten", txthoten.Text);
                cmd.Parameters.AddWithValue("@so_dien_thoai", txtsdt.Text);
                cmd.Parameters.AddWithValue("@bien_so_xe", txtbiensoxe.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Khách hàng đã được thêm thành công!");
                connection.Close();
                this.Hide();
            }

        }
    }
}
