namespace Garage_car
{
    partial class Loading
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Loading));
            this.label1NameShop = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1PT = new System.Windows.Forms.Label();
            this.Run = new Guna.UI2.WinForms.Guna2ProgressBar();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.guna2CircleButton1 = new Guna.UI2.WinForms.Guna2CircleButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1NameShop
            // 
            this.label1NameShop.AutoSize = true;
            this.label1NameShop.BackColor = System.Drawing.Color.DarkSalmon;
            this.label1NameShop.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1NameShop.ForeColor = System.Drawing.Color.Black;
            this.label1NameShop.Location = new System.Drawing.Point(193, 176);
            this.label1NameShop.Name = "label1NameShop";
            this.label1NameShop.Size = new System.Drawing.Size(108, 23);
            this.label1NameShop.TabIndex = 5;
            this.label1NameShop.Text = "Garage Auto";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.DarkSalmon;
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(38, 210);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = "Loading...";
            // 
            // label1PT
            // 
            this.label1PT.AutoSize = true;
            this.label1PT.BackColor = System.Drawing.Color.DarkSalmon;
            this.label1PT.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1PT.ForeColor = System.Drawing.Color.Black;
            this.label1PT.Location = new System.Drawing.Point(224, 210);
            this.label1PT.Name = "label1PT";
            this.label1PT.Size = new System.Drawing.Size(43, 23);
            this.label1PT.TabIndex = 8;
            this.label1PT.Text = "% %";
            // 
            // Run
            // 
            this.Run.BorderRadius = 12;
            this.Run.Location = new System.Drawing.Point(31, 236);
            this.Run.Name = "Run";
            this.Run.ProgressColor = System.Drawing.Color.White;
            this.Run.ProgressColor2 = System.Drawing.Color.DarkSalmon;
            this.Run.Size = new System.Drawing.Size(424, 30);
            this.Run.TabIndex = 9;
            this.Run.Text = "guna2ProgressBar1";
            this.Run.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.Run.ValueChanged += new System.EventHandler(this.Run_ValueChanged);
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 35;
            this.bunifuElipse1.TargetControl = this;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // guna2CircleButton1
            // 
            this.guna2CircleButton1.BackColor = System.Drawing.Color.DarkSalmon;
            this.guna2CircleButton1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2CircleButton1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2CircleButton1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2CircleButton1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2CircleButton1.FillColor = System.Drawing.Color.DarkSalmon;
            this.guna2CircleButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2CircleButton1.ForeColor = System.Drawing.Color.White;
            this.guna2CircleButton1.Image = global::Garage_car.Properties.Resources.cancel_50px1;
            this.guna2CircleButton1.ImageSize = new System.Drawing.Size(30, 30);
            this.guna2CircleButton1.Location = new System.Drawing.Point(525, 0);
            this.guna2CircleButton1.Name = "guna2CircleButton1";
            this.guna2CircleButton1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CircleButton1.Size = new System.Drawing.Size(81, 70);
            this.guna2CircleButton1.TabIndex = 18;
            this.guna2CircleButton1.Click += new System.EventHandler(this.guna2CircleButton1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.pictureBox2.Image = global::Garage_car.Properties.Resources.Designer1;
            this.pictureBox2.Location = new System.Drawing.Point(150, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(186, 161);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // Loading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSalmon;
            this.ClientSize = new System.Drawing.Size(480, 293);
            this.Controls.Add(this.guna2CircleButton1);
            this.Controls.Add(this.Run);
            this.Controls.Add(this.label1PT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1NameShop);
            this.Controls.Add(this.pictureBox2);
            this.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Loading";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "  ";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1NameShop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1PT;
        private Guna.UI2.WinForms.Guna2ProgressBar Run;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.Timer timer1;
        private Guna.UI2.WinForms.Guna2CircleButton guna2CircleButton1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}