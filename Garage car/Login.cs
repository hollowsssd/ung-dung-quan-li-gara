using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Garage_car
{
    public partial class Login : Form
    {
        chuoiconnect chuoiconnect= new chuoiconnect();
        SqlConnection connection = null;
        String chuoi =chuoiconnect.chuoi() ;
        public Login()
        {
            InitializeComponent();
            // Đảm bảo mật khẩu luôn luôn được che giấu
            txtPassword.UseSystemPasswordChar = true;

            // Đặt trạng thái của CheckBox là tích sẵn khi ứng dụng khởi chạy
            checkPS.Checked = false;
        }

            private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

                private void checkPS_CheckedChanged(object sender, EventArgs e)
        {

            if (checkPS.Checked)
            {
                // Hiển thị mật khẩu
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                // Che mật khẩu
                txtPassword.UseSystemPasswordChar = true;

            }
        }

        private void guna2CircleButton1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            using (connection = new SqlConnection(chuoi))
            {
                connection.Open();
                string username = txtUserName.Text;
                string password = txtPassword.Text;

                string sql="select chuc_vu, ma_nv, ho_ten from nhan_vien where ten_dang_nhap=@username and mat_khau=@password";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                SqlDataReader dt = cmd.ExecuteReader();

                if (dt.Read()==true)
                {
                    string chucVu = dt["chuc_vu"].ToString().ToLower(); // Chuyển chức vụ về chữ thường để dễ so sánh
                    String manv= dt["ma_nv"].ToString();
                    string hoten = dt["ho_ten"].ToString();
                       labelError.Visible = false;
                    if (chucVu == "quản lý" || chucVu == "quan ly")
                    {
                       
                        Form1 ds = new Form1(manv,hoten);
                        this.Hide();
                        ds.Show();
                    } else if (chucVu =="nhân viên" ||chucVu=="nhan vien")
                    {
                             Form2 ds = new Form2(manv, hoten);
                            this.Hide();
                             ds.Show() ;
                    }
                }else
                {
                    // Đăng nhập không thành công
                    labelError.Visible = true;
                    // Xóa thông tin đăng nhập
                    txtUserName.Clear();
                    txtPassword.Clear();
                    txtUserName.Focus();
                }
                connection.Close();
            }
        }

        private void guna2CircleButton1_Click_2(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkPS_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkPS.Checked)
            {
                // Hiển thị mật khẩu
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                // Che mật khẩu
                txtPassword.UseSystemPasswordChar = true;

            }
        }

       
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr one, int two, int three, int four);
        private void MoveLogin(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter)) 
            {

                txtPassword.Focus();


                // Ngăn chặn hành động mặc định của phím Enter
                e.SuppressKeyPress = true;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter))
            {
                btnLoginaaaa.Focus();
                e.SuppressKeyPress = true;
            }
        }
    }
}
