namespace GUI.ThongKe
{
    partial class PhieuNhapDashboardGUI
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbDate = new System.Windows.Forms.ComboBox();
            this.lblTongGiaNhap = new System.Windows.Forms.Label();
            this.dtpNgay = new System.Windows.Forms.DateTimePicker();
            this.chartPhieuNhapCot = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartPhieuNhapDuong = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblTongPhieu = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartPhieuNhapCot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPhieuNhapDuong)).BeginInit();
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
            this.lblTitle.Text = "THỐNG KÊ PHIẾU NHẬP";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.cbDate);
            this.panel1.Controls.Add(this.lblTongGiaNhap);
            this.panel1.Controls.Add(this.lblTongPhieu);
            this.panel1.Controls.Add(this.dtpNgay);
            this.panel1.Controls.Add(this.chartPhieuNhapCot);
            this.panel1.Controls.Add(this.chartPhieuNhapDuong);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(814, 492);
            this.panel1.TabIndex = 3;
            // 
            // cbDate
            // 
            this.cbDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDate.FormattingEnabled = true;
            this.cbDate.Items.AddRange(new object[] {
            "Ngày",
            "Tháng",
            "Năm"});
            this.cbDate.Location = new System.Drawing.Point(500, 17);
            this.cbDate.Name = "cbDate";
            this.cbDate.Size = new System.Drawing.Size(121, 21);
            this.cbDate.TabIndex = 41;
            this.cbDate.SelectedIndexChanged += new System.EventHandler(this.cbDate_SelectedIndexChanged);
            // 
            // lblTongGiaNhap
            // 
            this.lblTongGiaNhap.BackColor = System.Drawing.SystemColors.Control;
            this.lblTongGiaNhap.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongGiaNhap.ForeColor = System.Drawing.Color.Black;
            this.lblTongGiaNhap.Location = new System.Drawing.Point(136, 7);
            this.lblTongGiaNhap.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTongGiaNhap.Name = "lblTongGiaNhap";
            this.lblTongGiaNhap.Size = new System.Drawing.Size(118, 31);
            this.lblTongGiaNhap.TabIndex = 40;
            this.lblTongGiaNhap.Text = "Phiếu nhập";
            this.lblTongGiaNhap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpNgay
            // 
            this.dtpNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgay.Location = new System.Drawing.Point(318, 13);
            this.dtpNgay.MinimumSize = new System.Drawing.Size(4, 28);
            this.dtpNgay.Name = "dtpNgay";
            this.dtpNgay.Size = new System.Drawing.Size(176, 28);
            this.dtpNgay.TabIndex = 39;
            this.dtpNgay.ValueChanged += new System.EventHandler(this.dtpNgay_ValueChanged);
            // 
            // chartPhieuNhapCot
            // 
            this.chartPhieuNhapCot.BackColor = System.Drawing.SystemColors.Control;
            chartArea1.Name = "ChartArea1";
            this.chartPhieuNhapCot.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartPhieuNhapCot.Legends.Add(legend1);
            this.chartPhieuNhapCot.Location = new System.Drawing.Point(3, 44);
            this.chartPhieuNhapCot.Name = "chartPhieuNhapCot";
            this.chartPhieuNhapCot.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Chocolate;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartPhieuNhapCot.Series.Add(series1);
            this.chartPhieuNhapCot.Size = new System.Drawing.Size(757, 212);
            this.chartPhieuNhapCot.TabIndex = 0;
            this.chartPhieuNhapCot.Text = "chart1";
            // 
            // chartPhieuNhapDuong
            // 
            this.chartPhieuNhapDuong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chartPhieuNhapDuong.BackColor = System.Drawing.SystemColors.Control;
            chartArea2.Name = "ChartArea1";
            this.chartPhieuNhapDuong.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartPhieuNhapDuong.Legends.Add(legend2);
            this.chartPhieuNhapDuong.Location = new System.Drawing.Point(3, 277);
            this.chartPhieuNhapDuong.Name = "chartPhieuNhapDuong";
            this.chartPhieuNhapDuong.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Chocolate;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            series2.YValuesPerPoint = 2;
            this.chartPhieuNhapDuong.Series.Add(series2);
            this.chartPhieuNhapDuong.Size = new System.Drawing.Size(757, 212);
            this.chartPhieuNhapDuong.TabIndex = 43;
            this.chartPhieuNhapDuong.Text = "chart1";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Image = global::GUI.Properties.Resources.refresh;
            this.btnRefresh.Location = new System.Drawing.Point(627, 11);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(33, 30);
            this.btnRefresh.TabIndex = 42;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblTongPhieu
            // 
            this.lblTongPhieu.BackColor = System.Drawing.SystemColors.Control;
            this.lblTongPhieu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongPhieu.ForeColor = System.Drawing.Color.Black;
            this.lblTongPhieu.Location = new System.Drawing.Point(14, 7);
            this.lblTongPhieu.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTongPhieu.Name = "lblTongPhieu";
            this.lblTongPhieu.Size = new System.Drawing.Size(118, 31);
            this.lblTongPhieu.TabIndex = 44;
            this.lblTongPhieu.Text = "Phiếu nhập";
            this.lblTongPhieu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PhieuNhapDashboardGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblTitle);
            this.Name = "PhieuNhapDashboardGUI";
            this.Size = new System.Drawing.Size(814, 532);
            this.Load += new System.EventHandler(this.PhieuNhapDashboardGUI_Load);
            this.SizeChanged += new System.EventHandler(this.PhieuNhapDashboardGUI_SizeChanged);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartPhieuNhapCot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPhieuNhapDuong)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox cbDate;
        private System.Windows.Forms.Label lblTongGiaNhap;
        private System.Windows.Forms.DateTimePicker dtpNgay;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPhieuNhapCot;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPhieuNhapDuong;
        private System.Windows.Forms.Label lblTongPhieu;
    }
}
