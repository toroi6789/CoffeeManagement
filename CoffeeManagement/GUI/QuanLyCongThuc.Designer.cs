namespace CoffeeManagement.GUI
{
    partial class QuanLyCongThuc
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.NguyenLieuSP = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            this.btnXoa = new System.Windows.Forms.Button();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtSearch_ID = new System.Windows.Forms.TextBox();
            this.btnSearch = new FontAwesome.Sharp.IconButton();
            this.btnThem = new System.Windows.Forms.Button();
            this.AllNguyenLieu = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.txtID_NL = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtSoLuongSuDung = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NguyenLieuSP)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AllNguyenLieu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnHuy);
            this.panel1.Controls.Add(this.btnSua);
            this.panel1.Controls.Add(this.txtSoLuongSuDung);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.NguyenLieuSP);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.btnXoa);
            this.panel1.Controls.Add(this.txtID);
            this.panel1.Location = new System.Drawing.Point(27, 63);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(294, 355);
            this.panel1.TabIndex = 3;
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(178, 114);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(81, 29);
            this.btnHuy.TabIndex = 11;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(90, 114);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(81, 29);
            this.btnSua.TabIndex = 10;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "Số lượng sử dụng";
            // 
            // NguyenLieuSP
            // 
            this.NguyenLieuSP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.NguyenLieuSP.Location = new System.Drawing.Point(3, 148);
            this.NguyenLieuSP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NguyenLieuSP.MultiSelect = false;
            this.NguyenLieuSP.Name = "NguyenLieuSP";
            this.NguyenLieuSP.ReadOnly = true;
            this.NguyenLieuSP.RowHeadersWidth = 51;
            this.NguyenLieuSP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.NguyenLieuSP.Size = new System.Drawing.Size(286, 194);
            this.NguyenLieuSP.TabIndex = 0;
            this.NguyenLieuSP.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.NguyenLieuSP_CellClick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(7, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(114, 23);
            this.label10.TabIndex = 1;
            this.label10.Text = "ID sản phẩm";
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(3, 114);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(81, 29);
            this.btnXoa.TabIndex = 4;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(127, 24);
            this.txtID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(153, 22);
            this.txtID.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(199, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(344, 38);
            this.label1.TabIndex = 4;
            this.label1.Text = "Công thức sản phẩm";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtSearch_ID);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Controls.Add(this.btnThem);
            this.panel2.Controls.Add(this.AllNguyenLieu);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtID_NL);
            this.panel2.Location = new System.Drawing.Point(408, 63);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(294, 355);
            this.panel2.TabIndex = 5;
            // 
            // txtSearch_ID
            // 
            this.txtSearch_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch_ID.Location = new System.Drawing.Point(96, 112);
            this.txtSearch_ID.Name = "txtSearch_ID";
            this.txtSearch_ID.Size = new System.Drawing.Size(148, 27);
            this.txtSearch_ID.TabIndex = 7;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.MediumAquamarine;
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.GhostWhite;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Teal;
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkCyan;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearch.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnSearch.IconColor = System.Drawing.Color.GhostWhite;
            this.btnSearch.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnSearch.IconSize = 20;
            this.btnSearch.Location = new System.Drawing.Point(251, 110);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(29, 29);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(3, 112);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(87, 31);
            this.btnThem.TabIndex = 3;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // AllNguyenLieu
            // 
            this.AllNguyenLieu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AllNguyenLieu.Location = new System.Drawing.Point(3, 148);
            this.AllNguyenLieu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AllNguyenLieu.MultiSelect = false;
            this.AllNguyenLieu.Name = "AllNguyenLieu";
            this.AllNguyenLieu.ReadOnly = true;
            this.AllNguyenLieu.RowHeadersWidth = 51;
            this.AllNguyenLieu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AllNguyenLieu.Size = new System.Drawing.Size(286, 194);
            this.AllNguyenLieu.TabIndex = 0;
            this.AllNguyenLieu.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AllNguyenLieu_CellClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "ID";
            // 
            // txtID_NL
            // 
            this.txtID_NL.Location = new System.Drawing.Point(127, 24);
            this.txtID_NL.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtID_NL.Name = "txtID_NL";
            this.txtID_NL.ReadOnly = true;
            this.txtID_NL.Size = new System.Drawing.Size(153, 22);
            this.txtID_NL.TabIndex = 2;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtSoLuongSuDung
            // 
            this.txtSoLuongSuDung.Location = new System.Drawing.Point(127, 57);
            this.txtSoLuongSuDung.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSoLuongSuDung.Name = "txtSoLuongSuDung";
            this.txtSoLuongSuDung.ReadOnly = true;
            this.txtSoLuongSuDung.Size = new System.Drawing.Size(153, 22);
            this.txtSoLuongSuDung.TabIndex = 8;
            // 
            // QuanLyCongThuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "QuanLyCongThuc";
            this.Size = new System.Drawing.Size(734, 467);
            this.SizeChanged += new System.EventHandler(this.QuanLyCongThuc_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NguyenLieuSP)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AllNguyenLieu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView NguyenLieuSP;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.DataGridView AllNguyenLieu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtID_NL;
        private FontAwesome.Sharp.IconButton btnSearch;
        private System.Windows.Forms.TextBox txtSearch_ID;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.TextBox txtSoLuongSuDung;
    }
}
