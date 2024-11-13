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
using static TheArtOfDevHtmlRenderer.Adapters.RGraphicsPath;

namespace Garage_car
{
    public partial class xoasuadv : Form
    {
        SqlConnection conn;
        string chuoiketnoi;
        string madv;
        public xoasuadv(string ma_dv,string ten_dv, string mo_ta,decimal gia,SqlConnection connection,string chuoi)
        {
            InitializeComponent();
            conn = connection;
            chuoiketnoi = chuoi;
            madv = ma_dv;

            txtmadv.Text = madv;
            txtmota.Text = mo_ta;
            txttendv.Text = mo_ta;
            txtgia.Text=gia.ToString();
        }
        private void bttsua_Click(object sender, EventArgs e)
        {
            using (conn = new SqlConnection(chuoiketnoi))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update dich_vu set ten_dv=@ten_dv,mo_ta=@mo_ta, gia=@gia where ma_dv=@ma_dv", conn);
                cmd.Parameters.AddWithValue("@ma_dv", madv);
                cmd.Parameters.AddWithValue("@ten_dv", txttendv.Text);
                cmd.Parameters.AddWithValue("@mo_ta", txtmota.Text);
                cmd.Parameters.AddWithValue("@gia", txtgia.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Khách hàng đã được cập nhật thành công!");
                conn.Close();
                this.Close();



            }
        }

        private void bttxoa_Click(object sender, EventArgs e)
        {
            using (conn = new SqlConnection(chuoiketnoi))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete from dich_vu where ma_dv=@ma_dv", conn);
                cmd.Parameters.AddWithValue("@ma_dv", txtmadv.Text);
                cmd.ExecuteNonQuery(); // được dùng cho các câu lệnh UPDATE, INSERT, hoặc DELETE
                MessageBox.Show("da xoa thanh cong");
                conn.Close();
                this.Hide();
            }
        }
    }
}
