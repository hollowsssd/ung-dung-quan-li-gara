using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Garage_car
{
    public partial class Form2 : Form
    {
        public string MaNV { get; set; }//{ get; set; }:
                                        //  get: Cho phép đọc giá trị của thuộc tính.
                                        // set: Cho phép gán giá trị mới cho thuộc tính.
        public string HoTen { get; set; }
        public Form2()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Khi Form1 load, truyền thông tin nhân viên cho UserControl
            donHang1.SetNhanVienInfo(MaNV, HoTen);
        }
        public Form2(string maNV, string hoTen) : this()
        {
            this.MaNV = maNV;
            this.HoTen = hoTen;
        }

        private void BtnDonHang_Click(object sender, EventArgs e)
        {
            donHang1.BringToFront();
        }

        private void trangChu1_Load(object sender, EventArgs e)
        {
            trangChu1.BringToFront();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            khachHang1.BringToFront();
        }
    }
}
