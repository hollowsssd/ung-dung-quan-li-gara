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

namespace Garage_car
{
    public partial class xoavacapnhatphutung : Form
    {
        SqlConnection connec=null;
        string chuoi;
        int soluong;
        string mapt;
        public xoavacapnhatphutung(string mapt, string tenpt,string mota ,decimal gia , int soluong,SqlConnection connection, string chuoi)
        {
            InitializeComponent();
            this.chuoi = chuoi;
            this.mapt = mapt;
            this.soluong = soluong;
            connec=connection;

            txtmapt.Text=mapt;
            txttenpt.Text=tenpt;
            txtgia.Text=gia.ToString();
            txtsoluong.Text=soluong.ToString();
            txtmota.Text=mota;
        }

        private void bttxoa_Click(object sender, EventArgs e)
        {
            using (connec = new SqlConnection(chuoi))
            {
                connec.Open();
                SqlCommand cmd = new SqlCommand("delete from phu_tung where ma_pt=@ma_pt", connec);
                cmd.Parameters.AddWithValue("@ma_pt", txtmapt.Text);
                cmd.ExecuteNonQuery(); // được dùng cho các câu lệnh UPDATE, INSERT, hoặc DELETE
                MessageBox.Show("da xoa thanh cong");
                connec.Close();
                this.Hide();
            }
        }

        private void bttsua_Click(object sender, EventArgs e)
        {
            using (connec = new SqlConnection(chuoi))
            {
                connec.Open();
                SqlCommand cmd = new SqlCommand("update phu_tung set ten_pt=@ten_pt,mo_ta=@mo_ta, gia=@gia  where ma_pt=@ma_pt", connec);
                cmd.Parameters.AddWithValue("@ma_pt", mapt);
                cmd.Parameters.AddWithValue("@ten_pt", txttenpt.Text);
                cmd.Parameters.AddWithValue("@mo_ta", txtmota.Text);
                cmd.Parameters.AddWithValue("@gia", txtgia.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Khách hàng đã được cập nhật thành công!");
                connec.Close();
                this.Close();



            }
        }
    }
}
