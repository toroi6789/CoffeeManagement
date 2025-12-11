namespace CoffeeManagement.GUI.ThongKe
{
    partial class NguyenLieuDashboardGUI
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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblTongNL = new System.Windows.Forms.Label();
            this.chartNguyenLieu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartNguyenLieu)).BeginInit();
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
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "THỐNG KÊ SẢN PHẨM";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.lblTongNL);
            this.panel1.Controls.Add(this.chartNguyenLieu);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(814, 492);
            this.panel1.TabIndex = 4;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Image = global::CoffeeManagement.Properties.Resources.refresh;
            this.btnRefresh.Location = new System.Drawing.Point(627, 11);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(33, 30);
            this.btnRefresh.TabIndex = 42;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblTongNL
            // 
            this.lblTongNL.BackColor = System.Drawing.SystemColors.Control;
            this.lblTongNL.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongNL.ForeColor = System.Drawing.Color.Black;
            this.lblTongNL.Location = new System.Drawing.Point(49, 9);
            this.lblTongNL.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTongNL.Name = "lblTongNL";
            this.lblTongNL.Size = new System.Drawing.Size(169, 31);
            this.lblTongNL.TabIndex = 40;
            this.lblTongNL.Text = "Tong";
            this.lblTongNL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chartNguyenLieu
            // 
            this.chartNguyenLieu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chartNguyenLieu.BackColor = System.Drawing.SystemColors.Control;
            chartArea3.Name = "ChartArea1";
            this.chartNguyenLieu.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chartNguyenLieu.Legends.Add(legend3);
            this.chartNguyenLieu.Location = new System.Drawing.Point(37, 43);
            this.chartNguyenLieu.Name = "chartNguyenLieu";
            this.chartNguyenLieu.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Chocolate;
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            series3.YValuesPerPoint = 2;
            this.chartNguyenLieu.Series.Add(series3);
            this.chartNguyenLieu.Size = new System.Drawing.Size(743, 432);
            this.chartNguyenLieu.TabIndex = 0;
            this.chartNguyenLieu.Text = "chart1";
            // 
            // NguyenLieuDashboardGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblTitle);
            this.Name = "NguyenLieuDashboardGUI";
            this.Size = new System.Drawing.Size(814, 532);
            this.Load += new System.EventHandler(this.NguyenLieuDashboardGUI_Load);
            this.SizeChanged += new System.EventHandler(this.NguyenLieuDashboardGUI_SizeChanged);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartNguyenLieu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblTongNL;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartNguyenLieu;
    }
}
