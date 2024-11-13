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
using System.Xml.Linq;

namespace Garage_car
{
    public partial class KhachHang : UserControl
    {
        public KhachHang()
        {
            InitializeComponent();
            loaddata();
            txttimkiem.Click += new EventHandler(txttimkiem_TextChanged);
        }

        chuoiconnect chuoiconnect = new chuoiconnect();
        SqlConnection connection = null;
        String chuoi = chuoiconnect.chuoi();
        public void loaddata()
        {
            using (connection = new SqlConnection(chuoi))
            {
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter("select * from khach_hang", connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                guna2DataGridView1.DataSource = dt;
                connection.Close();
            }
        }
        // Phương thức tạo mã khách hàng tự động
        private string taomakhachhang()
        {
            int maxMaKH = 0;
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.Cells["Ma_KH"].Value != null)
                {
                    string maKH = row.Cells["ma_kh"].Value.ToString();
                    if (maKH.Length > 2 && int.TryParse(maKH.Substring(2), out int currentMaKH))
                    {
                        if (currentMaKH > maxMaKH)
                        {
                            maxMaKH = currentMaKH;
                        }
                    }
                }
            }
            return "KH" + (maxMaKH + 1).ToString("D2"); // Tạo mã khách hàng mới
        }


        private void guna2Button1Save_Click(object sender, EventArgs e)
        {
            Themkhachhang themkhachhang = new Themkhachhang(connection, chuoi, taomakhachhang());
            themkhachhang.ShowDialog();
            loaddata();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];
                string maKH = row.Cells["ma_kh"].Value.ToString();
                string hoTen = row.Cells["ho_ten"].Value.ToString();
                string soDienThoai = row.Cells["so_dien_thoai"].Value.ToString();
                string biensoxe = row.Cells["bien_so_xe"].Value.ToString();
                capnhatkhachhang suaXoaKhachHang = new capnhatkhachhang(maKH, hoTen, soDienThoai, biensoxe, connection, chuoi);
                suaXoaKhachHang.ShowDialog();
                loaddata();
            }
        }
        private void txttimkiem_TextChanged(object sender, EventArgs e)
        {
            
            String tenkh = txttimkiem.Text;
            timkiem(tenkh);
            
        }
        private void timkiem(string name)
        {
            using (connection = new SqlConnection(chuoi))
            {
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM khach_hang WHERE ho_ten LIKE @name", connection);
                da.SelectCommand.Parameters.AddWithValue("@name", "%" + name + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);
                guna2DataGridView1.DataSource = dt;
                connection.Close();
            }
        }

        
    }
}
