namespace CoffeeManagement.GUI
{
    partial class BanHangGUI
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.orderGUI1 = new CoffeeManagement.GUI.OrderGUI();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(681, 547);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // orderGUI1
            // 
            this.orderGUI1.Location = new System.Drawing.Point(690, 3);
            this.orderGUI1.Name = "orderGUI1";
            this.orderGUI1.Size = new System.Drawing.Size(387, 606);
            this.orderGUI1.TabIndex = 1;
            this.orderGUI1.RequestChangeToThanhToan += OnOrderRequestPnlBodyChangedToThanhToan;
            // 
            // BanHangGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.orderGUI1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "BanHangGUI";
            this.Size = new System.Drawing.Size(1080, 656);
            this.Load += new System.EventHandler(this.BanHang_Load);
            this.SizeChanged += new System.EventHandler(this.BanHangGUI_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private OrderGUI orderGUI1;
    }
}
