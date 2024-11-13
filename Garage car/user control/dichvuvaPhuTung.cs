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
    public partial class dichvuvaPhuTung : UserControl
    {
        chuoiconnect chuoiconnect = new chuoiconnect();
        SqlConnection connection = null;
        String chuoi = chuoiconnect.chuoi();
        public dichvuvaPhuTung()
        {
            InitializeComponent();
            loaddata();

            txttimphutung.Click += new EventHandler(txttimphutung_TextChanged);
            txttimtendichvu.Click += new EventHandler(txttimtendichvu_TextChanged);

        }

        public void loaddata()
        {
            using (connection = new SqlConnection(chuoi))
            {
                connection.Open();
                SqlDataAdapter pt =new SqlDataAdapter("select * from phu_tung",chuoi);
                DataTable da= new DataTable();
                pt.Fill(da);
                guna2DataGridView1.DataSource = da;


                SqlDataAdapter dv = new SqlDataAdapter("select * from dich_vu", chuoi);
                DataTable da1 = new DataTable();
                dv.Fill(da1);
                guna2DataGridView2.DataSource = da1;


                connection.Close();
            }
        }
        private string taomaphutung()
        {
            int temp = 0;
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.Cells["ma_pt"].Value != null)
                {
                    string ma_pt = row.Cells["ma_pt"].Value.ToString();
                    if (ma_pt.Length > 2 && int.TryParse(ma_pt.Substring(2), out int currentmapt))
                    {
                        if (currentmapt > temp)
                        {
                            temp = currentmapt;
                        }
                    }
                }
            }
            return "PT" + (temp + 1).ToString("D2"); // Tạo mã khách hàng mới
        }

        private string taomadv()
        {
            int temp = 0;
            foreach (DataGridViewRow row in guna2DataGridView2.Rows)
            {
                if (row.Cells["ma_dv"].Value != null)
                {
                    string ma_dv = row.Cells["ma_dv"].Value.ToString();
                    if (ma_dv.Length > 2 && int.TryParse(ma_dv.Substring(2), out int currentmadv))
                    {
                        if (currentmadv > temp)
                        {
                            temp = currentmadv;
                        }
                    }
                }
            }
            return "DV" + (temp + 1).ToString("D2"); // Tạo mã khách hàng mới
        }


        private void bttthemphutung_Click(object sender, EventArgs e)
        {
            themphuttungcs pt= new themphuttungcs(connection,chuoi,taomaphutung());
            pt.ShowDialog();
            loaddata();
        

        }

        private void bttthemDV_Click(object sender, EventArgs e)
        {
            themdv dv = new themdv(connection,chuoi,taomadv());
            dv.ShowDialog();
            loaddata();
          
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];
                string mapt = row.Cells["ma_pt"].Value.ToString();
                string tenpt = row.Cells["ten_pt"].Value.ToString();
                string mota = row.Cells["mo_ta"].Value.ToString();
                decimal gia =(decimal)row.Cells["gia"].Value;
                int soluong = (int) row.Cells["so_luong"].Value;   
                 xoavacapnhatphutung pt= new xoavacapnhatphutung(mapt, tenpt, mota, gia, soluong, connection, chuoi);
                pt.ShowDialog();
                loaddata();
            }
        }

        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = guna2DataGridView2.Rows[e.RowIndex];
                string ma_dv = row.Cells["ma_dv"].Value.ToString();
                string ten_dv = row.Cells["ten_dv"].Value.ToString();
                string mo_ta = row.Cells["mo_ta"].Value.ToString();
                decimal gia = (decimal)row.Cells["gia"].Value;
                xoasuadv dv = new xoasuadv(ma_dv, ten_dv, mo_ta, gia, connection, chuoi);
                dv.ShowDialog();
                loaddata();
            }
        }

        private void txttimphutung_TextChanged(object sender, EventArgs e)
        {  
            String tenpt = txttimphutung.Text;
            timkiemphutung(tenpt);
        }

        private void txttimtendichvu_TextChanged(object sender, EventArgs e)
        {
            String tendv = txttimtendichvu.Text;
            timkiemdichvu(tendv);

        }
        private void timkiemphutung(string name)
        {
            using (connection = new SqlConnection(chuoi))
            {
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM phu_tung WHERE ten_pt LIKE @name", connection);
                da.SelectCommand.Parameters.AddWithValue("@name", "%" + name + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);
                guna2DataGridView1.DataSource = dt;
                connection.Close();
            }
        }
        private void timkiemdichvu(string name)
        {
            using (connection = new SqlConnection(chuoi))
            {
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM dich_vu WHERE ten_dv LIKE @name", connection);
                da.SelectCommand.Parameters.AddWithValue("@name", "%" + name + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);
                guna2DataGridView2.DataSource = dt;
                connection.Close();
            }
        }
    }
}
