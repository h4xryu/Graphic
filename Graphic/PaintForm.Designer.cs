using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using ZedGraph;

namespace Graphic
{
    partial class PaintForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            glControl1 = new GLControl();
            closeBtn = new Button();
            label1 = new System.Windows.Forms.Label();
            zedGraphControl1 = new ZedGraphControl();
            SuspendLayout();
            // 
            // glControl1
            // 
            glControl1.AutoValidate = AutoValidate.EnablePreventFocusChange;
            glControl1.BackColor = Color.FromArgb(3, 8, 15);
            glControl1.BackgroundImageLayout = ImageLayout.None;
            glControl1.ForeColor = Color.FromArgb(3, 8, 15);
            glControl1.Location = new Point(1016, 407);
            glControl1.Margin = new Padding(3, 4, 3, 4);
            glControl1.Name = "glControl1";
            glControl1.Size = new Size(430, 291);
            glControl1.TabIndex = 1;
            glControl1.TabStop = false;
            glControl1.VSync = false;
            glControl1.Load += glControl1_Load_1;
            // 
            // closeBtn
            // 
            closeBtn.BackColor = Color.FromArgb(15, 30, 45);
            closeBtn.FlatStyle = FlatStyle.Popup;
            closeBtn.Location = new Point(1358, 750);
            closeBtn.Margin = new Padding(0);
            closeBtn.Name = "closeBtn";
            closeBtn.Size = new Size(88, 23);
            closeBtn.TabIndex = 0;
            closeBtn.TabStop = false;
            closeBtn.Text = "close";
            closeBtn.UseVisualStyleBackColor = false;
            closeBtn.Click += closeBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(72, 201);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(52, 15);
            label1.TabIndex = 2;
            label1.Text = "Matrix : ";
            label1.Click += label1_Click;
            // 
            // zedGraphControl1
            // 
            zedGraphControl1.ForeColor = SystemColors.ControlLightLight;
            zedGraphControl1.Location = new Point(72, 335);
            zedGraphControl1.Margin = new Padding(3, 4, 3, 4);
            zedGraphControl1.Name = "zedGraphControl1";
            zedGraphControl1.ScrollGrace = 0D;
            zedGraphControl1.ScrollMaxX = 0D;
            zedGraphControl1.ScrollMaxY = 0D;
            zedGraphControl1.ScrollMaxY2 = 0D;
            zedGraphControl1.ScrollMinX = 0D;
            zedGraphControl1.ScrollMinY = 0D;
            zedGraphControl1.ScrollMinY2 = 0D;
            zedGraphControl1.Size = new Size(695, 363);
            zedGraphControl1.TabIndex = 3;
            zedGraphControl1.UseExtendedPrintDialog = true;
            zedGraphControl1.Load += zedGraphControl1_Load;
            // 
            // PaintForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(3, 8, 15);
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(1493, 796);
            Controls.Add(label1);
            Controls.Add(glControl1);
            Controls.Add(zedGraphControl1);
            Controls.Add(closeBtn);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "PaintForm";
            Text = "PaintForm";
            Load += PaintForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private GLControl glControl1;
        private Button closeBtn;
        private ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.Label label1;
        
    }
}
