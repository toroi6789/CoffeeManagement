namespace CoffeeManagement.GUI
{
    partial class DatBanGUI
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
            this.pnTitle = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnContainer = new System.Windows.Forms.Panel();
            this.dgvDatban = new System.Windows.Forms.DataGridView();
            this.dgvBan = new System.Windows.Forms.DataGridView();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.dtpGioBD = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpNgay = new System.Windows.Forms.DateTimePicker();
            this.btnDatBan = new System.Windows.Forms.Button();
            this.error = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnTitle.SuspendLayout();
            this.pnContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatban)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBan)).BeginInit();
            this.panelInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.error)).BeginInit();
            this.SuspendLayout();
            // 
            // pnTitle
            // 
            this.pnTitle.Controls.Add(this.panel4);
            this.pnTitle.Controls.Add(this.label1);
            this.pnTitle.Location = new System.Drawing.Point(3, 4);
            this.pnTitle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnTitle.Name = "pnTitle";
            this.pnTitle.Size = new System.Drawing.Size(1077, 59);
            this.pnTitle.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Location = new System.Drawing.Point(635, 62);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(443, 351);
            this.panel4.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Đặt Bàn";
            // 
            // pnContainer
            // 
            this.pnContainer.Controls.Add(this.dgvDatban);
            this.pnContainer.Controls.Add(this.dgvBan);
            this.pnContainer.Controls.Add(this.panelInfo);
            this.pnContainer.Controls.Add(this.pnTitle);
            this.pnContainer.Location = new System.Drawing.Point(3, 2);
            this.pnContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnContainer.Name = "pnContainer";
            this.pnContainer.Size = new System.Drawing.Size(1083, 516);
            this.pnContainer.TabIndex = 2;
            // 
            // dgvDatban
            // 
            this.dgvDatban.AllowUserToAddRows = false;
            this.dgvDatban.AllowUserToDeleteRows = false;
            this.dgvDatban.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDatban.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatban.Location = new System.Drawing.Point(3, 308);
            this.dgvDatban.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvDatban.Name = "dgvDatban";
            this.dgvDatban.ReadOnly = true;
            this.dgvDatban.RowHeadersWidth = 51;
            this.dgvDatban.RowTemplate.Height = 24;
            this.dgvDatban.Size = new System.Drawing.Size(1077, 206);
            this.dgvDatban.TabIndex = 1;
            this.dgvDatban.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBan_CellClick);
            this.dgvDatban.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDatban_CellFormatting);
            // 
            // dgvBan
            // 
            this.dgvBan.AllowUserToAddRows = false;
            this.dgvBan.AllowUserToDeleteRows = false;
            this.dgvBan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBan.Location = new System.Drawing.Point(0, 65);
            this.dgvBan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvBan.Name = "dgvBan";
            this.dgvBan.ReadOnly = true;
            this.dgvBan.RowHeadersWidth = 51;
            this.dgvBan.RowTemplate.Height = 24;
            this.dgvBan.Size = new System.Drawing.Size(632, 238);
            this.dgvBan.TabIndex = 1;
            this.dgvBan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBan_CellClick);
            this.dgvBan.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBan_CellClick);
            this.dgvBan.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvBan_CellFormatting);
            // 
            // panelInfo
            // 
            this.panelInfo.Controls.Add(this.dtpGioBD);
            this.panelInfo.Controls.Add(this.label2);
            this.panelInfo.Controls.Add(this.label3);
            this.panelInfo.Controls.Add(this.dtpNgay);
            this.panelInfo.Controls.Add(this.btnDatBan);
            this.panelInfo.Location = new System.Drawing.Point(641, 68);
            this.panelInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(436, 235);
            this.panelInfo.TabIndex = 0;
            // 
            // dtpGioBD
            // 
            this.dtpGioBD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpGioBD.Location = new System.Drawing.Point(256, 54);
            this.dtpGioBD.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpGioBD.Name = "dtpGioBD";
            this.dtpGioBD.Size = new System.Drawing.Size(120, 22);
            this.dtpGioBD.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 29);
            this.label2.TabIndex = 0;
            this.label2.Text = "Giờ Đặt Bàn";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 29);
            this.label3.TabIndex = 0;
            this.label3.Text = "Ngày Đặt Bàn";
            // 
            // dtpNgay
            // 
            this.dtpNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgay.Location = new System.Drawing.Point(256, 18);
            this.dtpNgay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpNgay.MinDate = new System.DateTime(2025, 11, 22, 0, 0, 0, 0);
            this.dtpNgay.Name = "dtpNgay";
            this.dtpNgay.Size = new System.Drawing.Size(120, 22);
            this.dtpNgay.TabIndex = 6;
            // 
            // btnDatBan
            // 
            this.btnDatBan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDatBan.Location = new System.Drawing.Point(171, 113);
            this.btnDatBan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDatBan.Name = "btnDatBan";
            this.btnDatBan.Size = new System.Drawing.Size(92, 47);
            this.btnDatBan.TabIndex = 3;
            this.btnDatBan.Text = "Đặt bàn";
            this.btnDatBan.UseVisualStyleBackColor = true;
            this.btnDatBan.Click += new System.EventHandler(this.btnDatBan_Click);
            // 
            // error
            // 
            this.error.ContainerControl = this;
            // 
            // DatBanGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnContainer);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "DatBanGUI";
            this.Size = new System.Drawing.Size(1091, 678);
            this.Load += new System.EventHandler(this.DatBanGUI_Load);
            this.SizeChanged += new System.EventHandler(this.DatBanGUI_SizeChanged);
            this.pnTitle.ResumeLayout(false);
            this.pnTitle.PerformLayout();
            this.pnContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatban)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBan)).EndInit();
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.error)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnContainer;
        private System.Windows.Forms.DataGridView dgvBan;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Button btnDatBan;
        private System.Windows.Forms.ErrorProvider error;
        private System.Windows.Forms.DateTimePicker dtpNgay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpGioBD;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dgvDatban;
    }
}
