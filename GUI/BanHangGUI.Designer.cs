using System.Windows;
using System.Windows.Forms;

namespace GUI
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
            this.components = new System.ComponentModel.Container();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.orderGUI1 = new OrderGUI();
            this.txt_Sreach = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnSearch = new FontAwesome.Sharp.IconButton();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(24, 145);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(660, 378);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // orderGUI1
            // 
            this.orderGUI1.Location = new System.Drawing.Point(690, 3);
            this.orderGUI1.Name = "orderGUI1";
            this.orderGUI1.Size = new System.Drawing.Size(387, 606);
            this.orderGUI1.TabIndex = 1;
            this.orderGUI1.Load += new System.EventHandler(this.orderGUI1_Load);
            // 
            // txt_Sreach
            // 
            this.txt_Sreach.Location = new System.Drawing.Point(24, 74);
            this.txt_Sreach.Name = "txt_Sreach";
            this.txt_Sreach.Size = new System.Drawing.Size(264, 22);
            this.txt_Sreach.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
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
            this.btnSearch.Location = new System.Drawing.Point(320, 71);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(29, 29);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // BanHangGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txt_Sreach);
            this.Controls.Add(this.orderGUI1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "BanHangGUI";
            this.Size = new System.Drawing.Size(1080, 656);
            this.Load += new System.EventHandler(this.BanHang_Load);
            this.SizeChanged += new System.EventHandler(this.BanHangGUI_SizeChanged);
            this.ParentChanged += new System.EventHandler(this.BanHangGUI_ParentChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private OrderGUI orderGUI1;
        private TextBox txt_Sreach;
        private ContextMenuStrip contextMenuStrip1;
        private FontAwesome.Sharp.IconButton btnSearch;
    }
}
