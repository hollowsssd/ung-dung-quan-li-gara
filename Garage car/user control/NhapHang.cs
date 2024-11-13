using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Garage_car
{
    public partial class NhapHang : UserControl
    {

        chuoiconnect chuoiconnect = new chuoiconnect();
        SqlConnection connection = null;
        String chuoi = chuoiconnect.chuoi();

        public string MaNV { get;private set; }
        public string HoTen { get;private set; }

        public NhapHang()
        {
            InitializeComponent();
            loaddata();
        }
        public void SetNhanVienInfo(string maNV, string hoTen)
        {
            this.MaNV = maNV;
            this.HoTen = hoTen;
            txtmanvtennv.Text = $"{maNV}:{hoTen}"; //  hiển thị tên nhân viên trên giao diện
        }
        private void loaddata()
        {
            using(connection = new SqlConnection(chuoi))
            {
                connection.Open();
                SqlDataAdapter dt = new SqlDataAdapter("select * from nhap_hang", connection);
                DataTable tb =new DataTable();
                dt.Fill(tb);
                guna2DataGridView1.DataSource = tb;
                connection.Close();
            }

        }
        private void comboBox1_DropDown(object sender, EventArgs e)
        {
               
            using (SqlConnection connection = new SqlConnection(chuoi))
            {
                connection.Open();
                comboBox1.Items.Clear();
                SqlCommand command = new SqlCommand("select ma_pt, ten_pt from phu_tung", connection);
                SqlDataReader reader = command.ExecuteReader();

                List<ComboBoxItem> items = new List<ComboBoxItem>();

                while (reader.Read())
                {
                    // Thêm các item vào ComboBox
                    items.Add(new ComboBoxItem {Ma = reader["ma_pt"].ToString(),Ten = reader["ten_pt"].ToString()});
                }
                reader.Close();

                // Cập nhật DataSource của ComboBox
                comboBox1.DataSource = items;
                comboBox1.DisplayMember = "ToString"; // Hiển thị kết quả của phương thức ToString
                connection.Close();
            }
        } 
        public void ClearComboBox()
        {
            comboBox1.Items.Clear();
            comboBox1.DataSource = null; // Đặt lại DataSource 
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (txtgianhap.Text == "" || txtsoluongnhap.Text == "")
            {
                MessageBox.Show("không có dữ liệu để thêm");
            }
            else
            {
                using (connection = new SqlConnection(chuoi))
                {
                    connection.Open();
                    // Lấy giá trị từ ComboBox
                    int soluong = int.Parse(txtsoluongnhap.Text);
                    string mapt = ((ComboBoxItem)comboBox1.SelectedItem).Ma;
                    SqlCommand cmd = new SqlCommand(@" 
                                                    BEGIN TRANSACTION;
                                                    INSERT INTO nhap_hang (ma_pt, ma_nv, so_luong_nhap, gia_nhap, ngay_nhap) 
                                                    VALUES (@ma_pt, @ma_nv, @so_luong_nhap, @gia_nhap, GETDATE());
                                                    UPDATE phu_tung 
                                                    SET so_luong = so_luong + @so_luong_nhap 
                                                    WHERE ma_pt = @ma_pt;
                                                    COMMIT TRANSACTION;", connection);
                   /* @ trong chuỗi "verbatim" Điều này có nghĩa là chuỗi sẽ được giữ nguyên mọi ký tự bên trong, 
                    bao gồm cả ký tự xuống dòng, dấu gạch chéo ngược(\), và các ký tự đặc biệt khác mà không cần phải thoát chúng.

                    BEGIN TRANSACTION: Bắt đầu một giao dịch.Sau khi bắt đầu, mọi câu lệnh SQL tiếp theo sẽ được coi là một phần của giao dịch đó.
                    COMMIT TRANSACTION: Kết thúc giao dịch và lưu tất cả các thay đổi vào cơ sở dữ liệu.
                    ROLLBACK TRANSACTION: Hủy bỏ giao dịch và hoàn tác tất cả các thay đổi đã thực hiện trong giao dịch đó nếu có lỗi xảy ra.*/


                    cmd.Parameters.AddWithValue("@ma_pt",mapt);
                    cmd.Parameters.AddWithValue("@ma_nv", MaNV);
                    cmd.Parameters.AddWithValue("@so_luong_nhap",soluong);
                    cmd.Parameters.AddWithValue("@gia_nhap", decimal.Parse(txtgianhap.Text)); 
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("đã được thêm thành công!");
                    connection.Close();
                    loaddata();
                    
                    dichvuvaPhuTung dichvuvaphutung=new dichvuvaPhuTung();
                    dichvuvaphutung.loaddata();
                    
       
                }
            }

        }

        
    }
}
