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
    public partial class themphuttungcs : Form
    {
        SqlConnection conn=null;
        string chuoi;
        string ma_pt;
        public themphuttungcs(SqlConnection connection , string chuoi,string ma_pt)
        {
            InitializeComponent();
            this.chuoi = chuoi;
            this.conn = connection;
            this.ma_pt = ma_pt;

            txtmapt.Text = ma_pt;
        }

        private void bttthem_Click(object sender, EventArgs e)
        {
            using (conn = new SqlConnection(chuoi))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO phu_tung (ma_pt,ten_pt,gia,mo_ta,so_luong) VALUES (@ma_pt, @ten_pt, @gia, @mo_ta,@so_luong)", conn);
                cmd.Parameters.AddWithValue("@ma_pt", ma_pt);
                cmd.Parameters.AddWithValue("@ten_pt", txttenpt.Text);
                cmd.Parameters.AddWithValue("@gia", txtgia.Text);
                cmd.Parameters.AddWithValue("@mo_ta", txtmota.Text);
                int soluong = 0;
                cmd.Parameters.AddWithValue("@so_luong",soluong);
                cmd.ExecuteNonQuery();
                MessageBox.Show("phụ tùng đã được thêm thành công!");
                conn.Close();
                NhapHang nhaphang = new NhapHang();
                nhaphang.ClearComboBox();
                this.Hide();
            }
        }
    }
}
