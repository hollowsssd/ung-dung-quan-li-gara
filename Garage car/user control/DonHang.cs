using Garage_car.user_control;
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
    public partial class DonHang : UserControl
    {

        chuoiconnect chuoiconnect = new chuoiconnect();
        SqlConnection connection = null;
        String chuoi = chuoiconnect.chuoi();
        public string MaNV { get;  set; }//{ get; set; }:
                                        //  get: Cho phép đọc giá trị của thuộc tính.
                                        // set: Cho phép gán giá trị mới cho thuộc tính.
        public string HoTen { get;  set; }
        public DonHang()
        {
            InitializeComponent();
            loaddata();
            txttimkien.Click += new EventHandler(textBox1_TextChanged);

        }
        public void SetNhanVienInfo(string maNV, string hoTen)
        {
            this.MaNV = maNV;
            this.HoTen = hoTen;
            
        }
        private string taomahoadon()
        {
            int maxMahd = 0;
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.Cells["ma_hd"].Value != null)
                {
                    string mahd = row.Cells["ma_hd"].Value.ToString();
                    if (mahd.Length > 2 && int.TryParse(mahd.Substring(2), out int currentMahd))
                    {
                        if (currentMahd > maxMahd)
                        {
                            maxMahd = currentMahd;
                        }
                    }
                }
            }
            return "HD" + (maxMahd + 1).ToString("D2"); // Tạo mã khách hàng mới
        }
        public void loaddata()
        {
            using (connection = new SqlConnection(chuoi))
            {
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT hoa_don.ma_hd,ho_ten,ma_nv,tong_tien FROM hoa_don join khach_hang on hoa_don.ma_kh=khach_hang.ma_kh ", connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                guna2DataGridView1.DataSource = dt;
                connection.Close();
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)

            {
                DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];
                string shd= row.Cells["ma_hd"].Value.ToString();
                thongtinchitiethoadon thongtinchitiet= new thongtinchitiethoadon(shd, connection, chuoi);
                thongtinchitiet.Show();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
                {
                    String sohd = txttimkien.Text;
                    timkiem(sohd);
                }
        }
 
        private void timkiem(string name)
        {
            using (connection = new SqlConnection(chuoi))
            {
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM hoa_don WHERE ma_hd LIKE @name", connection);
                da.SelectCommand.Parameters.AddWithValue("@name", "%" + name + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);
                guna2DataGridView1.DataSource = dt;
                connection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            taohoadon taohoadon = new taohoadon(MaNV,HoTen,taomahoadon());  
            taohoadon.ShowDialog();
            loaddata();
        }
    }
}
