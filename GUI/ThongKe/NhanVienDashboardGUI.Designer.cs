namespace GUI.ThongKe
{
    partial class NhanVienDashboardGUI
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTongTrongLich = new System.Windows.Forms.Label();
            this.lblTongDangLam = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblTong = new System.Windows.Forms.Label();
            this.chartNhanVien = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartNhanVien)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.DarkCyan;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1085, 49);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "THỐNG KÊ NHÂN VIÊN";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTongTrongLich);
            this.panel1.Controls.Add(this.lblTongDangLam);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.lblTong);
            this.panel1.Controls.Add(this.chartNhanVien);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 49);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1085, 606);
            this.panel1.TabIndex = 3;
            // 
            // lblTongTrongLich
            // 
            this.lblTongTrongLich.BackColor = System.Drawing.SystemColors.Control;
            this.lblTongTrongLich.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTrongLich.ForeColor = System.Drawing.Color.Black;
            this.lblTongTrongLich.Location = new System.Drawing.Point(64, 208);
            this.lblTongTrongLich.Name = "lblTongTrongLich";
            this.lblTongTrongLich.Size = new System.Drawing.Size(133, 38);
            this.lblTongTrongLich.TabIndex = 44;
            this.lblTongTrongLich.Text = "Tổng";
            this.lblTongTrongLich.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTongDangLam
            // 
            this.lblTongDangLam.BackColor = System.Drawing.SystemColors.Control;
            this.lblTongDangLam.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongDangLam.ForeColor = System.Drawing.Color.Black;
            this.lblTongDangLam.Location = new System.Drawing.Point(64, 158);
            this.lblTongDangLam.Name = "lblTongDangLam";
            this.lblTongDangLam.Size = new System.Drawing.Size(133, 38);
            this.lblTongDangLam.TabIndex = 43;
            this.lblTongDangLam.Text = "Tổng";
            this.lblTongDangLam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::GUI.Properties.Resources.refresh;
            this.btnRefresh.Location = new System.Drawing.Point(855, 36);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(44, 37);
            this.btnRefresh.TabIndex = 42;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblTong
            // 
            this.lblTong.BackColor = System.Drawing.SystemColors.Control;
            this.lblTong.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTong.ForeColor = System.Drawing.Color.Black;
            this.lblTong.Location = new System.Drawing.Point(64, 106);
            this.lblTong.Name = "lblTong";
            this.lblTong.Size = new System.Drawing.Size(83, 38);
            this.lblTong.TabIndex = 40;
            this.lblTong.Text = "Tổng";
            this.lblTong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chartNhanVien
            // 
            this.chartNhanVien.BackColor = System.Drawing.SystemColors.Control;
            chartArea3.Name = "ChartArea1";
            this.chartNhanVien.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chartNhanVien.Legends.Add(legend3);
            this.chartNhanVien.Location = new System.Drawing.Point(20, 80);
            this.chartNhanVien.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chartNhanVien.Name = "chartNhanVien";
            this.chartNhanVien.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chartNhanVien.Series.Add(series3);
            this.chartNhanVien.Size = new System.Drawing.Size(1023, 473);
            this.chartNhanVien.TabIndex = 0;
            this.chartNhanVien.Text = "chart1";
            // 
            // NhanVienDashboardGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblTitle);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "NhanVienDashboardGUI";
            this.Size = new System.Drawing.Size(1085, 655);
            this.Load += new System.EventHandler(this.NhanVienDashboardGUI_Load);
            this.SizeChanged += new System.EventHandler(this.NhanVienDashboardGUI_SizeChanged);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartNhanVien)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblTong;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartNhanVien;
        private System.Windows.Forms.Label lblTongTrongLich;
        private System.Windows.Forms.Label lblTongDangLam;
    }
}
