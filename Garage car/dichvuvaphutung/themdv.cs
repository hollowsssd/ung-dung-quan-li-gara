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
    public partial class themdv : Form
    {

        SqlConnection conn = null;
        string chuoiketnoi;
        string ma_dv;
        public themdv(SqlConnection connection, string chuoi, string ma_dv)
        {
            InitializeComponent();
            this.chuoiketnoi = chuoi;
            this.conn = connection;
            this.ma_dv = ma_dv;

            txtmadv.Text = ma_dv;

           
        }

        private void bttthem_Click(object sender, EventArgs e)
        {
           using(conn=new SqlConnection(this.chuoiketnoi))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO dich_vu (ma_dv,ten_dv,gia,mo_ta) VALUES (@ma_dv, @ten_dv, @gia, @mo_ta)", conn);
                cmd.Parameters.AddWithValue("@ma_dv", ma_dv);
                cmd.Parameters.AddWithValue("@ten_dv", txttendv.Text);
                cmd.Parameters.AddWithValue("@gia", txtgia.Text);
                cmd.Parameters.AddWithValue("@mo_ta", txtmota.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("dịch vụ đã được thêm thành công!");
                conn.Close();
                this.Hide();
            }
        }
    }
}
