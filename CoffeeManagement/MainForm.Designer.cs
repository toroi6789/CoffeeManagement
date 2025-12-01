namespace CoffeeManagement
{
    partial class MainForm
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
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUserInfo = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnMaximize = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.banHangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bánHàngDanhSáchSảnPhẩmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quanLyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hoaDonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.danhSáchHóaĐơnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nhanVienToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nhapKhoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sanPhamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.danhMụcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bànToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nhàCungCấpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.đặtBànToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.nguyênLiệuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nguyênLiệuToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlTitle.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackColor = System.Drawing.Color.DarkCyan;
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Controls.Add(this.lblUserInfo);
            this.pnlTitle.Controls.Add(this.btnLogout);
            this.pnlTitle.Controls.Add(this.btnMinimize);
            this.pnlTitle.Controls.Add(this.btnMaximize);
            this.pnlTitle.Controls.Add(this.btnClose);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(1080, 45);
            this.pnlTitle.TabIndex = 0;
            this.pnlTitle.Resize += new System.EventHandler(this.pnlTitle_Resize);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(15, 11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(346, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ CỬA HÀNG CÀ PHÊ";
            // 
            // lblUserInfo
            // 
            this.lblUserInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUserInfo.ForeColor = System.Drawing.Color.White;
            this.lblUserInfo.Location = new System.Drawing.Point(365, 12);
            this.lblUserInfo.Name = "lblUserInfo";
            this.lblUserInfo.Size = new System.Drawing.Size(512, 28);
            this.lblUserInfo.TabIndex = 4;
            this.lblUserInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.BackColor = System.Drawing.Color.DarkCyan;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnLogout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(854, 5);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(156, 35);
            this.btnLogout.TabIndex = 5;
            this.btnLogout.Text = "Đăng xuất";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.BackColor = System.Drawing.Color.DarkCyan;
            this.btnMinimize.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.btnMinimize.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.btnMinimize.Location = new System.Drawing.Point(921, 0);
            this.btnMinimize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(53, 45);
            this.btnMinimize.TabIndex = 3;
            this.btnMinimize.Text = "—";
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnMaximize
            // 
            this.btnMaximize.BackColor = System.Drawing.Color.DarkCyan;
            this.btnMaximize.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMaximize.FlatAppearance.BorderSize = 0;
            this.btnMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaximize.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.btnMaximize.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.btnMaximize.Location = new System.Drawing.Point(974, 0);
            this.btnMaximize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMaximize.Name = "btnMaximize";
            this.btnMaximize.Size = new System.Drawing.Size(53, 45);
            this.btnMaximize.TabIndex = 2;
            this.btnMaximize.Text = "▢";
            this.btnMaximize.UseVisualStyleBackColor = false;
            this.btnMaximize.Click += new System.EventHandler(this.btnMaximize_Click);
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.BackColor = System.Drawing.Color.DarkCyan;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.btnClose.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.btnClose.Location = new System.Drawing.Point(1027, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(53, 45);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "x";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Silver;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.banHangToolStripMenuItem,
            this.bánHàngDanhSáchSảnPhẩmToolStripMenuItem,
            this.quanLyToolStripMenuItem,
            this.đặtBànToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 45);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1080, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // banHangToolStripMenuItem
            // 
            this.banHangToolStripMenuItem.Name = "banHangToolStripMenuItem";
            this.banHangToolStripMenuItem.Size = new System.Drawing.Size(85, 24);
            this.banHangToolStripMenuItem.Text = "Bán hàng";
            this.banHangToolStripMenuItem.Click += new System.EventHandler(this.banHangToolStripMenuItem_Click);
            // 
            // bánHàngDanhSáchSảnPhẩmToolStripMenuItem
            // 
            this.bánHàngDanhSáchSảnPhẩmToolStripMenuItem.Name = "bánHàngDanhSáchSảnPhẩmToolStripMenuItem";
            this.bánHàngDanhSáchSảnPhẩmToolStripMenuItem.Size = new System.Drawing.Size(191, 29);
            this.bánHàngDanhSáchSảnPhẩmToolStripMenuItem.Text = "Danh sách sản phẩm";
            this.bánHàngDanhSáchSảnPhẩmToolStripMenuItem.Click += new System.EventHandler(this.bánHàngDanhSáchSảnPhẩmToolStripMenuItem_Click);
            // 
            // quanLyToolStripMenuItem
            // 
            this.quanLyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hoaDonToolStripMenuItem,
            this.nhanVienToolStripMenuItem,
            this.nhapKhoToolStripMenuItem,
            this.sanPhamToolStripMenuItem,
            this.danhMụcToolStripMenuItem,
            this.bànToolStripMenuItem,
            this.nhàCungCấpToolStripMenuItem,
            this.nguyênLiệuToolStripMenuItem1});
            this.quanLyToolStripMenuItem.Name = "quanLyToolStripMenuItem";
            this.quanLyToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.quanLyToolStripMenuItem.Text = "Quản lý";
            this.quanLyToolStripMenuItem.Click += new System.EventHandler(this.quanLyToolStripMenuItem_Click);
            // 
            // hoaDonToolStripMenuItem
            // 
            this.hoaDonToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.danhSáchHóaĐơnToolStripMenuItem});
            this.hoaDonToolStripMenuItem.Name = "hoaDonToolStripMenuItem";
            this.hoaDonToolStripMenuItem.Size = new System.Drawing.Size(158, 26);
            this.hoaDonToolStripMenuItem.Text = "Hóa đơn";
            this.hoaDonToolStripMenuItem.Click += new System.EventHandler(this.hoaDonToolStripMenuItem_Click);
            // 
            // danhSáchHóaĐơnToolStripMenuItem
            // 
            this.danhSáchHóaĐơnToolStripMenuItem.Name = "danhSáchHóaĐơnToolStripMenuItem";
            this.danhSáchHóaĐơnToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.danhSáchHóaĐơnToolStripMenuItem.Text = "Danh sách hóa đơn";
            this.danhSáchHóaĐơnToolStripMenuItem.Click += new System.EventHandler(this.DSHoaDonToolStripMenuItem_Click);
            // 
            // nhanVienToolStripMenuItem
            // 
            this.nhanVienToolStripMenuItem.Name = "nhanVienToolStripMenuItem";
            this.nhanVienToolStripMenuItem.Size = new System.Drawing.Size(158, 26);
            this.nhanVienToolStripMenuItem.Text = "Nhân viên";
            this.nhanVienToolStripMenuItem.Click += new System.EventHandler(this.nhanVienToolStripMenuItem_Click);
            // 
            // nhapKhoToolStripMenuItem
            // 
            this.nhapKhoToolStripMenuItem.Name = "nhapKhoToolStripMenuItem";
            this.nhapKhoToolStripMenuItem.Size = new System.Drawing.Size(158, 26);
            this.nhapKhoToolStripMenuItem.Text = "Nhập kho";
            this.nhapKhoToolStripMenuItem.Click += new System.EventHandler(this.nhapKhoToolStripMenuItem_Click);
            // 
            // sanPhamToolStripMenuItem
            // 
            this.sanPhamToolStripMenuItem.Name = "sanPhamToolStripMenuItem";
            this.sanPhamToolStripMenuItem.Size = new System.Drawing.Size(158, 26);
            this.sanPhamToolStripMenuItem.Text = "Sản phẩm";
            this.sanPhamToolStripMenuItem.Click += new System.EventHandler(this.sanPhamToolStripMenuItem_Click);
            // 
            // danhMụcToolStripMenuItem
            // 
            this.danhMụcToolStripMenuItem.Name = "danhMụcToolStripMenuItem";
            this.danhMụcToolStripMenuItem.Size = new System.Drawing.Size(224, 30);
            this.danhMụcToolStripMenuItem.Text = "Danh Mục";
            this.danhMụcToolStripMenuItem.Click += new System.EventHandler(this.danhMụcToolStripMenuItem_Click);
            // 
            // bànToolStripMenuItem
            // 
            this.bànToolStripMenuItem.Name = "bànToolStripMenuItem";
            this.bànToolStripMenuItem.Size = new System.Drawing.Size(224, 30);
            this.bànToolStripMenuItem.Text = "Bàn";
            this.bànToolStripMenuItem.Click += new System.EventHandler(this.bànToolStripMenuItem_Click);
            // 
            // nhàCungCấpToolStripMenuItem
            // 
            this.nhàCungCấpToolStripMenuItem.Name = "nhàCungCấpToolStripMenuItem";
            this.nhàCungCấpToolStripMenuItem.Size = new System.Drawing.Size(224, 30);
            this.nhàCungCấpToolStripMenuItem.Text = "Nhà Cung Cấp";
            this.nhàCungCấpToolStripMenuItem.Click += new System.EventHandler(this.nhàCungCấpToolStripMenuItem_Click);
            // 
            // đặtBànToolStripMenuItem
            // 
            this.đặtBànToolStripMenuItem.Name = "đặtBànToolStripMenuItem";
            this.đặtBànToolStripMenuItem.Size = new System.Drawing.Size(76, 24);
            this.đặtBànToolStripMenuItem.Text = "Đặt bàn";
            this.đặtBànToolStripMenuItem.Click += new System.EventHandler(this.datBanToolStripMenuItem_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.AutoSize = true;
            this.pnlHeader.Controls.Add(this.menuStrip1);
            this.pnlHeader.Controls.Add(this.pnlTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1080, 78);
            this.pnlHeader.TabIndex = 2;
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.flowLayoutPanel1);
            this.pnlBody.Location = new System.Drawing.Point(0, 67);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(1080, 653);
            this.pnlBody.TabIndex = 4;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1080, 653);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.SizeChanged += new System.EventHandler(this.flowLayoutPanel1_SizeChanged);
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // nguyênLiệuToolStripMenuItem
            // 
            this.nguyênLiệuToolStripMenuItem.Name = "nguyênLiệuToolStripMenuItem";
            this.nguyênLiệuToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // nguyênLiệuToolStripMenuItem1
            // 
            this.nguyênLiệuToolStripMenuItem1.Name = "nguyênLiệuToolStripMenuItem1";
            this.nguyênLiệuToolStripMenuItem1.Size = new System.Drawing.Size(224, 30);
            this.nguyênLiệuToolStripMenuItem1.Text = "Nguyên Liệu";
            this.nguyênLiệuToolStripMenuItem1.Click += new System.EventHandler(this.nguyênLiệuToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1080, 720);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlBody);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnMaximize;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem banHangToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quanLyToolStripMenuItem;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ToolStripMenuItem hoaDonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nhanVienToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nhapKhoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem đặtBànToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sanPhamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem danhSáchHóaĐơnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bánHàngDanhSáchSảnPhẩmToolStripMenuItem;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblUserInfo;
        private System.Windows.Forms.ToolStripMenuItem danhMụcToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bànToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nhàCungCấpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nguyênLiệuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nguyênLiệuToolStripMenuItem1;
    }
}

