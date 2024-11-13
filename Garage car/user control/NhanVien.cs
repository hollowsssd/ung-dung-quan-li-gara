using Garage_car.Resources;
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

namespace Garage_car
{
    public partial class NhanVien : UserControl
    {
        chuoiconnect chuoiconnect = new chuoiconnect();
        SqlConnection connection = null;
        String chuoi = chuoiconnect.chuoi();
        public NhanVien()
        {
            InitializeComponent();
            loaddata();

        }
        public void loaddata()
        {
            using (connection = new SqlConnection(chuoi))
            {
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT ma_nv, ho_ten , so_dien_thoai , chuc_vu , email ,diachi, CASE WHEN gioitinh = 1 THEN 'Nam' ELSE 'Nu' END AS gioitinh FROM nhan_vien", connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                guna2DataGridView1.DataSource = dt;
                connection.Close();
            }
        }

        private string taomanhanvien()
        {
            int tammanv = 0;
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.Cells["ma_nv"].Value != null)
                {
                    string manv = row.Cells["ma_nv"].Value.ToString();
                    if (manv.Length > 2 && int.TryParse(manv.Substring(2), out int currentmanv))
                    {
                        if (currentmanv > tammanv)
                        {
                            tammanv = currentmanv;
                        }
                    }
                }
            }
            return "NV" + (tammanv + 1).ToString("D2"); // Tạo mã khách hàng mới
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            themnhanvien themnhanvien = new themnhanvien(connection, chuoi, taomanhanvien());
            themnhanvien.ShowDialog();
            loaddata();

        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];
                string manv = row.Cells["ma_nv"].Value.ToString();
                string hoTen = row.Cells["ho_ten"].Value.ToString();
                string soDienThoai = row.Cells["so_dien_thoai"].Value.ToString();
                string chucvu = row.Cells["chuc_vu"].Value.ToString();
                string diachi = row.Cells["diachi"].Value.ToString();
                string email = row.Cells["email"].Value.ToString();
                 bool gioitinh = row.Cells["gioitinh"].Value.ToString() == "Nam";
                string ten_dang_nhap = row.Cells["ten_dang_nhap"].Value.ToString();
                string mat_khau = row.Cells["mat_khau"].Value.ToString();

                capnhatnhanvien C = new capnhatnhanvien(manv, hoTen, soDienThoai, chucvu, email, diachi, gioitinh, ten_dang_nhap, mat_khau, connection, chuoi);
                C.ShowDialog();
                loaddata();
            }

        }
    }
}