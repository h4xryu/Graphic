using OpenTK;
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
            glControl1.Location = new Point(1306, 619);
            glControl1.Margin = new Padding(4, 5, 4, 5);
            glControl1.Name = "glControl1";
            glControl1.Size = new Size(553, 388);
            glControl1.TabIndex = 1;
            glControl1.TabStop = false;
            glControl1.VSync = false;
            glControl1.Load += glControl1_Load_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(93, 268);
            label1.Name = "label1";
            label1.Size = new Size(65, 20);
            label1.TabIndex = 2;
            label1.Text = "Matrix : ";
            label1.Click += label1_Click;
            // 
            // zedGraphControl1
            // 
            zedGraphControl1.Location = new Point(93, 523);
            zedGraphControl1.Margin = new Padding(4, 5, 4, 5);
            zedGraphControl1.Name = "zedGraphControl1";
            zedGraphControl1.ScrollGrace = 0D;
            zedGraphControl1.ScrollMaxX = 0D;
            zedGraphControl1.ScrollMaxY = 0D;
            zedGraphControl1.ScrollMaxY2 = 0D;
            zedGraphControl1.ScrollMinX = 0D;
            zedGraphControl1.ScrollMinY = 0D;
            zedGraphControl1.ScrollMinY2 = 0D;
            zedGraphControl1.Size = new Size(894, 484);
            zedGraphControl1.TabIndex = 3;
            zedGraphControl1.UseExtendedPrintDialog = true;
            // 
            // PaintForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(3, 8, 15);
            ClientSize = new Size(1920, 1080);
            Controls.Add(label1);
            Controls.Add(glControl1);
            Controls.Add(zedGraphControl1);
            Name = "PaintForm";
            Text = "PaintForm";
            Load += PaintForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private GLControl glControl1;
        private ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.Label label1;
        
    }
}
