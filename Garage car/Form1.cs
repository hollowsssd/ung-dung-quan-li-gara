using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Garage_car
{
    public partial class Form1 : Form
    {
        public string MaNV { get; set; }//{ get; set; }:
                                          //  get: Cho phép đọc giá trị của thuộc tính.
                                           // set: Cho phép gán giá trị mới cho thuộc tính.
        public string HoTen { get; set; }

        public Form1()
        {
            InitializeComponent();
        }
        public Form1(string maNV, string hoTen) : this()
        {
            this.MaNV = maNV;
            this.HoTen = hoTen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Khi Form1 load, truyền thông tin nhân viên cho UserControl
           nhapHang2.SetNhanVienInfo(MaNV, HoTen);
            donHang1.SetNhanVienInfo(MaNV,HoTen);
        }
        private void guna2CircleButton1CloseGD_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

               [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr one, int two, int three, int four);

        private void DiChuyenFrom(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }


        private void guna2CircleButton1Minize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;

        }

        

        private void AnHien(object sender, EventArgs e)
        {
            if (panel2.Visible == true)
            // neu panel dang hien thi se doi thnah ta (true sang false)
            {
                panel2.Visible = false;

            }
            else if (panel2.Visible == false)
            // neu panel dang hien thi se doi thnah ta (true sang false)
            {
                panel2.Visible = true;

            }
            
        }

        private void ClickLogOut(object sender, EventArgs e)
        {
            Login lg = new Login();
            this.Hide();
            lg.Show();
        }
        
        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            trangChu2.BringToFront();
        }

        private void btnlLichHen_Click(object sender, EventArgs e)
        {
            lichHen1.BringToFront();
        }


        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            khachHang1.BringToFront();

        }
        
        private void btnNhanVien_Click(object sender, EventArgs e)
        {
         
            nhanVien2.BringToFront();

        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {
           
            nhapHang2.BringToFront();
        }

        private void BtnDonHang_Click(object sender, EventArgs e)
        {
            donHang1.BringToFront();
        }

        private void btnPhuTung_Click(object sender, EventArgs e)
        {
             dichvuvaPhuTung2.BringToFront();
        }
    }
}
