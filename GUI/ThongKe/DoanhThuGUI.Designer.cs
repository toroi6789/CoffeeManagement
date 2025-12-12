namespace GUI.ThongKe
{
    partial class DoanhThuGUI
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cbDate = new System.Windows.Forms.ComboBox();
            this.lblTongDoanhThu = new System.Windows.Forms.Label();
            this.dtpNgay = new System.Windows.Forms.DateTimePicker();
            this.chartDoanhThu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartBieuDoDuong = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBieuDoDuong)).BeginInit();
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
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "THỐNG KÊ DOANH THU";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.cbDate);
            this.panel1.Controls.Add(this.lblTongDoanhThu);
            this.panel1.Controls.Add(this.dtpNgay);
            this.panel1.Controls.Add(this.chartDoanhThu);
            this.panel1.Controls.Add(this.chartBieuDoDuong);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 49);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1085, 606);
            this.panel1.TabIndex = 2;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Image = global::GUI.Properties.Resources.refresh;
            this.btnRefresh.Location = new System.Drawing.Point(836, 14);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(44, 37);
            this.btnRefresh.TabIndex = 42;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cbDate
            // 
            this.cbDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDate.FormattingEnabled = true;
            this.cbDate.Items.AddRange(new object[] {
            "Ngày",
            "Tháng",
            "Năm"});
            this.cbDate.Location = new System.Drawing.Point(667, 21);
            this.cbDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbDate.Name = "cbDate";
            this.cbDate.Size = new System.Drawing.Size(160, 24);
            this.cbDate.TabIndex = 41;
            this.cbDate.SelectedIndexChanged += new System.EventHandler(this.cbDate_SelectedIndexChanged);
            // 
            // lblTongDoanhThu
            // 
            this.lblTongDoanhThu.BackColor = System.Drawing.SystemColors.Control;
            this.lblTongDoanhThu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongDoanhThu.ForeColor = System.Drawing.Color.Black;
            this.lblTongDoanhThu.Location = new System.Drawing.Point(352, 9);
            this.lblTongDoanhThu.Name = "lblTongDoanhThu";
            this.lblTongDoanhThu.Size = new System.Drawing.Size(157, 38);
            this.lblTongDoanhThu.TabIndex = 40;
            this.lblTongDoanhThu.Text = "Doanh thu";
            this.lblTongDoanhThu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpNgay
            // 
            this.dtpNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgay.Location = new System.Drawing.Point(92, 12);
            this.dtpNgay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpNgay.MinimumSize = new System.Drawing.Size(4, 28);
            this.dtpNgay.Name = "dtpNgay";
            this.dtpNgay.Size = new System.Drawing.Size(233, 28);
            this.dtpNgay.TabIndex = 39;
            this.dtpNgay.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // chartDoanhThu
            // 
            this.chartDoanhThu.BackColor = System.Drawing.SystemColors.Control;
            chartArea3.Name = "ChartArea1";
            this.chartDoanhThu.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chartDoanhThu.Legends.Add(legend3);
            this.chartDoanhThu.Location = new System.Drawing.Point(4, 54);
            this.chartDoanhThu.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chartDoanhThu.Name = "chartDoanhThu";
            this.chartDoanhThu.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Chocolate;
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chartDoanhThu.Series.Add(series3);
            this.chartDoanhThu.Size = new System.Drawing.Size(1009, 261);
            this.chartDoanhThu.TabIndex = 0;
            this.chartDoanhThu.Text = "chart1";
            // 
            // chartBieuDoDuong
            // 
            this.chartBieuDoDuong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chartBieuDoDuong.BackColor = System.Drawing.SystemColors.Control;
            chartArea4.Name = "ChartArea1";
            this.chartBieuDoDuong.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chartBieuDoDuong.Legends.Add(legend4);
            this.chartBieuDoDuong.Location = new System.Drawing.Point(4, 341);
            this.chartBieuDoDuong.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chartBieuDoDuong.Name = "chartBieuDoDuong";
            this.chartBieuDoDuong.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Chocolate;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            series4.YValuesPerPoint = 2;
            this.chartBieuDoDuong.Series.Add(series4);
            this.chartBieuDoDuong.Size = new System.Drawing.Size(1009, 261);
            this.chartBieuDoDuong.TabIndex = 43;
            this.chartBieuDoDuong.Text = "chart1";
            // 
            // DoanhThuGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblTitle);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DoanhThuGUI";
            this.Size = new System.Drawing.Size(1085, 655);
            this.Load += new System.EventHandler(this.DoanhThuGUI_Load);
            this.SizeChanged += new System.EventHandler(this.DoanhThuGUI_SizeChanged);
            this.ParentChanged += new System.EventHandler(this.DoanhThuGUI_SizeChanged);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBieuDoDuong)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhThu;
        private System.Windows.Forms.DateTimePicker dtpNgay;
        private System.Windows.Forms.Label lblTongDoanhThu;
        private System.Windows.Forms.ComboBox cbDate;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartBieuDoDuong;
    }
}
