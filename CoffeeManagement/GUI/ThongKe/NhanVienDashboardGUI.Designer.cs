namespace CoffeeManagement.GUI.ThongKe
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblTong = new System.Windows.Forms.Label();
            this.chartNhanVien = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblTongDangLam = new System.Windows.Forms.Label();
            this.lblTongTrongLich = new System.Windows.Forms.Label();
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
            this.lblTitle.Size = new System.Drawing.Size(814, 40);
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
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(814, 492);
            this.panel1.TabIndex = 3;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::CoffeeManagement.Properties.Resources.refresh;
            this.btnRefresh.Location = new System.Drawing.Point(649, 116);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(33, 30);
            this.btnRefresh.TabIndex = 42;
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // lblTong
            // 
            this.lblTong.BackColor = System.Drawing.SystemColors.Control;
            this.lblTong.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTong.ForeColor = System.Drawing.Color.Black;
            this.lblTong.Location = new System.Drawing.Point(48, 88);
            this.lblTong.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTong.Name = "lblTong";
            this.lblTong.Size = new System.Drawing.Size(62, 31);
            this.lblTong.TabIndex = 40;
            this.lblTong.Text = "Tổng";
            this.lblTong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chartNhanVien
            // 
            this.chartNhanVien.BackColor = System.Drawing.SystemColors.Control;
            chartArea1.Name = "ChartArea1";
            this.chartNhanVien.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartNhanVien.Legends.Add(legend1);
            this.chartNhanVien.Location = new System.Drawing.Point(15, 65);
            this.chartNhanVien.Name = "chartNhanVien";
            this.chartNhanVien.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartNhanVien.Series.Add(series1);
            this.chartNhanVien.Size = new System.Drawing.Size(767, 384);
            this.chartNhanVien.TabIndex = 0;
            this.chartNhanVien.Text = "chart1";
            // 
            // lblTongDangLam
            // 
            this.lblTongDangLam.BackColor = System.Drawing.SystemColors.Control;
            this.lblTongDangLam.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongDangLam.ForeColor = System.Drawing.Color.Black;
            this.lblTongDangLam.Location = new System.Drawing.Point(48, 130);
            this.lblTongDangLam.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTongDangLam.Name = "lblTongDangLam";
            this.lblTongDangLam.Size = new System.Drawing.Size(100, 31);
            this.lblTongDangLam.TabIndex = 43;
            this.lblTongDangLam.Text = "Tổng";
            this.lblTongDangLam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTongTrongLich
            // 
            this.lblTongTrongLich.BackColor = System.Drawing.SystemColors.Control;
            this.lblTongTrongLich.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTrongLich.ForeColor = System.Drawing.Color.Black;
            this.lblTongTrongLich.Location = new System.Drawing.Point(48, 171);
            this.lblTongTrongLich.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTongTrongLich.Name = "lblTongTrongLich";
            this.lblTongTrongLich.Size = new System.Drawing.Size(100, 31);
            this.lblTongTrongLich.TabIndex = 44;
            this.lblTongTrongLich.Text = "Tổng";
            this.lblTongTrongLich.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NhanVienDashboardGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblTitle);
            this.Name = "NhanVienDashboardGUI";
            this.Size = new System.Drawing.Size(814, 532);
            this.Load += new System.EventHandler(this.NhanVienDashboardGUI_Load);
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
