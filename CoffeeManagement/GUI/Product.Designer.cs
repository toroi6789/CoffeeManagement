namespace CoffeeManagement.GUI
{
    partial class Product
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
            this.HinhSP = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.lblGiaBan = new System.Windows.Forms.Label();
            this.lblTenSP = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.HinhSP)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // HinhSP
            // 
            this.HinhSP.BackColor = System.Drawing.Color.White;
            this.HinhSP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HinhSP.Dock = System.Windows.Forms.DockStyle.Top;
            this.HinhSP.Location = new System.Drawing.Point(0, 0);
            this.HinhSP.Name = "HinhSP";
            this.HinhSP.Size = new System.Drawing.Size(185, 169);
            this.HinhSP.TabIndex = 0;
            this.HinhSP.TabStop = false;
            this.HinhSP.Click += new System.EventHandler(this.HinhSP_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.lblGiaBan);
            this.panel1.Controls.Add(this.lblTenSP);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 172);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(185, 94);
            this.panel1.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(129, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(57, 35);
            this.button1.TabIndex = 3;
            this.button1.Text = "Mua";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // lblGiaBan
            // 
            this.lblGiaBan.BackColor = System.Drawing.Color.White;
            this.lblGiaBan.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblGiaBan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiaBan.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.lblGiaBan.Location = new System.Drawing.Point(0, 31);
            this.lblGiaBan.Name = "lblGiaBan";
            this.lblGiaBan.Size = new System.Drawing.Size(185, 63);
            this.lblGiaBan.TabIndex = 2;
            this.lblGiaBan.Text = "label3";
            this.lblGiaBan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTenSP
            // 
            this.lblTenSP.BackColor = System.Drawing.Color.White;
            this.lblTenSP.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTenSP.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenSP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTenSP.Location = new System.Drawing.Point(0, 0);
            this.lblTenSP.Name = "lblTenSP";
            this.lblTenSP.Size = new System.Drawing.Size(185, 31);
            this.lblTenSP.TabIndex = 0;
            this.lblTenSP.Text = "label1";
            this.lblTenSP.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(147, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 23);
            this.label1.TabIndex = 5;
            this.label1.Text = "Hot";
            // 
            // Product
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.HinhSP);
            this.Name = "Product";
            this.Size = new System.Drawing.Size(185, 266);
            ((System.ComponentModel.ISupportInitialize)(this.HinhSP)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox HinhSP;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblGiaBan;
        private System.Windows.Forms.Label lblTenSP;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}
