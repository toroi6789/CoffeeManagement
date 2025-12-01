namespace CoffeeManagement.GUI
{
    partial class BanGUI
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
            this.error = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnThem = new System.Windows.Forms.Button();
            this.txtTenBan = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvBan = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.pnContainer = new System.Windows.Forms.Panel();
            this.pnChucnang = new System.Windows.Forms.Panel();
            this.pnTitle = new System.Windows.Forms.Panel();
            this.updownSucchua = new CoffeeManagement.DTO.NumericUpDownEditingControl();
            ((System.ComponentModel.ISupportInitialize)(this.error)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBan)).BeginInit();
            this.panelInfo.SuspendLayout();
            this.pnContainer.SuspendLayout();
            this.pnChucnang.SuspendLayout();
            this.pnTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updownSucchua)).BeginInit();
            this.SuspendLayout();
            // 
            // error
            // 
            this.error.ContainerControl = this;
            // 
            // btnThem
            // 
            this.btnThem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThem.Location = new System.Drawing.Point(576, 13);
            this.btnThem.Margin = new System.Windows.Forms.Padding(2);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(69, 38);
            this.btnThem.TabIndex = 3;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // txtTenBan
            // 
            this.txtTenBan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTenBan.Location = new System.Drawing.Point(141, 61);
            this.txtTenBan.Margin = new System.Windows.Forms.Padding(2);
            this.txtTenBan.Name = "txtTenBan";
            this.txtTenBan.ReadOnly = true;
            this.txtTenBan.Size = new System.Drawing.Size(106, 20);
            this.txtTenBan.TabIndex = 7;
            // 
            // txtID
            // 
            this.txtID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtID.Location = new System.Drawing.Point(141, 17);
            this.txtID.Margin = new System.Windows.Forms.Padding(2);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(106, 20);
            this.txtID.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 31);
            this.label2.TabIndex = 2;
            this.label2.Text = "Mã Bàn";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(2, 54);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 31);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tên Bàn";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(162, 23);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(300, 20);
            this.txtSearch.TabIndex = 0;
            // 
            // btnSua
            // 
            this.btnSua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSua.Location = new System.Drawing.Point(723, 13);
            this.btnSua.Margin = new System.Windows.Forms.Padding(2);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(71, 38);
            this.btnSua.TabIndex = 5;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXoa.Location = new System.Drawing.Point(650, 13);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(2);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(69, 38);
            this.btnXoa.TabIndex = 4;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTimKiem.Location = new System.Drawing.Point(466, 18);
            this.btnTimKiem.Margin = new System.Windows.Forms.Padding(2);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(62, 28);
            this.btnTimKiem.TabIndex = 1;
            this.btnTimKiem.Text = "Tìm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(2, 151);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 31);
            this.label5.TabIndex = 2;
            this.label5.Text = "Sức Chứa";
            // 
            // dgvBan
            // 
            this.dgvBan.AllowUserToAddRows = false;
            this.dgvBan.AllowUserToDeleteRows = false;
            this.dgvBan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBan.Location = new System.Drawing.Point(4, 124);
            this.dgvBan.Margin = new System.Windows.Forms.Padding(2);
            this.dgvBan.Name = "dgvBan";
            this.dgvBan.ReadOnly = true;
            this.dgvBan.RowHeadersWidth = 51;
            this.dgvBan.RowTemplate.Height = 24;
            this.dgvBan.Size = new System.Drawing.Size(526, 351);
            this.dgvBan.TabIndex = 1;
            this.dgvBan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBan_CellClick);
            this.dgvBan.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvBan_CellFormatting);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(2, 102);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 31);
            this.label4.TabIndex = 2;
            this.label4.Text = "Trạng Thái";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Quản Lý Bàn";
            // 
            // panelInfo
            // 
            this.panelInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInfo.Controls.Add(this.cboStatus);
            this.panelInfo.Controls.Add(this.updownSucchua);
            this.panelInfo.Controls.Add(this.txtTenBan);
            this.panelInfo.Controls.Add(this.txtID);
            this.panelInfo.Controls.Add(this.label2);
            this.panelInfo.Controls.Add(this.label3);
            this.panelInfo.Controls.Add(this.label5);
            this.panelInfo.Controls.Add(this.label4);
            this.panelInfo.Location = new System.Drawing.Point(536, 124);
            this.panelInfo.Margin = new System.Windows.Forms.Padding(2);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(274, 418);
            this.panelInfo.TabIndex = 3;
            this.panelInfo.Paint += new System.Windows.Forms.PaintEventHandler(this.panelInfo_Paint);
            // 
            // cboStatus
            // 
            this.cboStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboStatus.DisplayMember = "ds";
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Location = new System.Drawing.Point(141, 108);
            this.cboStatus.Margin = new System.Windows.Forms.Padding(2);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(106, 21);
            this.cboStatus.TabIndex = 8;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(79, 13);
            this.btnExport.Margin = new System.Windows.Forms.Padding(2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(69, 38);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(6, 13);
            this.btnImport.Margin = new System.Windows.Forms.Padding(2);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(69, 38);
            this.btnImport.TabIndex = 3;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // pnContainer
            // 
            this.pnContainer.Controls.Add(this.panelInfo);
            this.pnContainer.Controls.Add(this.dgvBan);
            this.pnContainer.Controls.Add(this.pnChucnang);
            this.pnContainer.Controls.Add(this.pnTitle);
            this.pnContainer.Location = new System.Drawing.Point(2, 2);
            this.pnContainer.Margin = new System.Windows.Forms.Padding(2);
            this.pnContainer.Name = "pnContainer";
            this.pnContainer.Size = new System.Drawing.Size(812, 545);
            this.pnContainer.TabIndex = 1;
            this.pnContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // pnChucnang
            // 
            this.pnChucnang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnChucnang.Controls.Add(this.txtSearch);
            this.pnChucnang.Controls.Add(this.btnSua);
            this.pnChucnang.Controls.Add(this.btnXoa);
            this.pnChucnang.Controls.Add(this.btnTimKiem);
            this.pnChucnang.Controls.Add(this.btnExport);
            this.pnChucnang.Controls.Add(this.btnThem);
            this.pnChucnang.Controls.Add(this.btnImport);
            this.pnChucnang.Location = new System.Drawing.Point(2, 55);
            this.pnChucnang.Margin = new System.Windows.Forms.Padding(2);
            this.pnChucnang.Name = "pnChucnang";
            this.pnChucnang.Size = new System.Drawing.Size(808, 63);
            this.pnChucnang.TabIndex = 0;
            this.pnChucnang.SizeChanged += new System.EventHandler(this.BanGUI_SizeChanged);
            // 
            // pnTitle
            // 
            this.pnTitle.Controls.Add(this.label1);
            this.pnTitle.Location = new System.Drawing.Point(2, 2);
            this.pnTitle.Margin = new System.Windows.Forms.Padding(2);
            this.pnTitle.Name = "pnTitle";
            this.pnTitle.Size = new System.Drawing.Size(808, 48);
            this.pnTitle.TabIndex = 0;
            // 
            // updownSucchua
            // 
            this.updownSucchua.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.updownSucchua.EditingControlDataGridView = null;
            this.updownSucchua.EditingControlFormattedValue = "4";
            this.updownSucchua.EditingControlRowIndex = 0;
            this.updownSucchua.EditingControlValueChanged = true;
            this.updownSucchua.Location = new System.Drawing.Point(141, 158);
            this.updownSucchua.Margin = new System.Windows.Forms.Padding(2);
            this.updownSucchua.Name = "updownSucchua";
            this.updownSucchua.Size = new System.Drawing.Size(104, 20);
            this.updownSucchua.TabIndex = 9;
            this.updownSucchua.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // BanGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnContainer);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "BanGUI";
            this.Size = new System.Drawing.Size(818, 552);
            this.Load += new System.EventHandler(this.BanGUI_Load);
            this.SizeChanged += new System.EventHandler(this.BanGUI_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.error)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBan)).EndInit();
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.pnContainer.ResumeLayout(false);
            this.pnChucnang.ResumeLayout(false);
            this.pnChucnang.PerformLayout();
            this.pnTitle.ResumeLayout(false);
            this.pnTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updownSucchua)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ErrorProvider error;
        private System.Windows.Forms.Panel pnContainer;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.TextBox txtTenBan;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvBan;
        private System.Windows.Forms.Panel pnChucnang;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Panel pnTitle;
        private System.Windows.Forms.Label label1;
        private DTO.NumericUpDownEditingControl updownSucchua;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnImport;
    }
}
