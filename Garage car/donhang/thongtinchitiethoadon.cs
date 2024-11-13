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
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Garage_car.user_control
{
    public partial class thongtinchitiethoadon : Form
    {
        SqlConnection conn;
        string chuoiketnoi;
        string shd;
        public thongtinchitiethoadon(string shd,SqlConnection connection, string chuoi)
        {
            InitializeComponent();
            this.conn = connection;
            this.chuoiketnoi = chuoi;
            this.shd = shd;

            txtsohoadon.Text = shd;
            loadchitiet();
            loaddata();
            
        }
        private void loaddata()
        {
            using (conn = new SqlConnection(chuoiketnoi))
            {
                conn.Open();
                SqlDataAdapter pt = new SqlDataAdapter("SELECT chi_tiet_hoa_don.ma_pt, chi_tiet_hoa_don.so_luong, don_gia FROM chi_tiet_hoa_don JOIN phu_tung ON chi_tiet_hoa_don.ma_pt = phu_tung.ma_pt WHERE chi_tiet_hoa_don.ma_hd = @shd", chuoiketnoi);
                pt.SelectCommand.Parameters.AddWithValue("@shd", shd); 
                //thêm một tham số vào lệnh truy vấn SQL để truyền giá trị shd vào câu truy vấn. Việc này giúp ngăn chặn SQL Injection và đảm bảo rằng giá trị shd được truyền chính xác vào câu truy vấn SQL.
                DataTable dt = new DataTable();
                pt.Fill(dt);
                guna2DataGridView1.DataSource = dt;

                SqlDataAdapter dv = new SqlDataAdapter("SELECT chi_tiet_hoa_don.ma_dv, ten_dv, mo_ta, gia  FROM chi_tiet_hoa_don JOIN dich_vu ON chi_tiet_hoa_don.ma_dv = dich_vu.ma_dv WHERE chi_tiet_hoa_don.ma_hd = @shd", chuoiketnoi);
                dv.SelectCommand.Parameters.AddWithValue("@shd", shd);
                //thêm một tham số vào lệnh truy vấn SQL để truyền giá trị shd vào câu truy vấn. Việc này giúp ngăn chặn SQL Injection và đảm bảo rằng giá trị shd được truyền chính xác vào câu truy vấn SQL.
                DataTable dtt = new DataTable();
                dv.Fill(dtt);
                guna2DataGridView2.DataSource = dtt;

                conn.Close();

            }
        }
        private void loadchitiet()
        {
            using (conn= new SqlConnection(chuoiketnoi))
            {
                conn.Open();
                SqlCommand dl = new SqlCommand("select tong_tien, ngay_hd,hoa_don.ma_nv,nhan_vien.ho_ten as tennv,hoa_don.ma_kh,khach_hang.ho_ten as tenkh  from hoa_don JOIN nhan_vien ON hoa_don.ma_nv = nhan_vien.ma_nv JOIN khach_hang ON hoa_don.ma_kh = khach_hang.ma_kh where hoa_don.ma_hd=@shd  ", conn);
                dl.Parameters.AddWithValue("@shd",shd);
                using (SqlDataReader dr = dl.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        string tennv=dr["tennv"].ToString();
                        string manv = dr["ma_nv"].ToString();
                        string makh = dr["ma_kh"].ToString();
                        string tenkh = dr["tenkh"].ToString();
                        string ngay = dr["ngay_hd"].ToString();
                        decimal tongtien = dr["tong_tien"] != DBNull.Value ? Convert.ToDecimal(dr["tong_tien"]) : 0;// Đọc giá trị decimal

                        txtmatennv.Text = $"{manv} | {tennv}";

                        txtmatenkh.Text = $"{makh} | {tenkh}";

                        txtngayhd.Text = ngay;
                        txttongtien.Text = tongtien.ToString("N2"); // Định dạng decimal với 2 chữ số thập phân
                    }
                }
                
                conn.Close();

            }
        }
    }
}
