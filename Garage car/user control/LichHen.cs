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
using System.Web.UI.Design.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Garage_car
{
    public partial class LichHen : UserControl
    {
        chuoiconnect chuoiconnect = new chuoiconnect();
        SqlConnection connection = null;
        String chuoi = chuoiconnect.chuoi();
        public LichHen()
        {
            InitializeComponent();
            loaddata();
        }


        public void loaddata()
        {
            using (connection = new SqlConnection(chuoi))
            {
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter("select * from lich_hen", connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                guna2DataGridView1.DataSource = dt;
                connection.Close();
            }
        }
        private string taomalichhen()
        {
            int maxMalh = 0;
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.Cells["ma_lich_hen"].Value != null)
                {
                    string malh = row.Cells["ma_lich_hen"].Value.ToString();
                    if (malh.Length > 2 && int.TryParse(malh.Substring(2), out int currentMalh))
                    {
                        if (currentMalh > maxMalh)
                        {
                            maxMalh = currentMalh;
                        }
                    }
                }
            }
            return "LH" + (maxMalh + 1).ToString("D2"); // Tạo mã lich hẹn  mới
        }

        private void bttthem_Click(object sender, EventArgs e)
        {
            themlichhen themlichhen = new themlichhen(connection,chuoi,taomalichhen());
            themlichhen.ShowDialog();
            loaddata();
        }


        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];
                    
                string ma_lich_hen = row.Cells["ma_lich_hen"].Value.ToString();
                string ma_nv = row.Cells["ma_nv"].Value.ToString();
                string ngay_hen = row.Cells["ngay_hen"].Value.ToString();

                string mo_ta = row.Cells["mo_ta"].Value.ToString();
                string trang_thai = row.Cells["trang_thai"].Value.ToString();
                string ten_khach_hang = row.Cells["ten_khach_hang"].Value.ToString();
                string so_dien_thoai = row.Cells["so_dien_thoai"].Value.ToString();



                xoasualichhen dv = new xoasualichhen(ma_lich_hen, ma_nv, ngay_hen, mo_ta,ten_khach_hang,so_dien_thoai, trang_thai,connection, chuoi);
                dv.ShowDialog();
                loaddata();


            }


        }
    }
}
