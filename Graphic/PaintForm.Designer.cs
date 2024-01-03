using OpenTK;
using OpenTK.Graphics.OpenGL;
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
            glControl1 = new GLControl();
            label1 = new Label();
            SuspendLayout();
            // 
            // glControl1
            // 
            glControl1.AutoValidate = AutoValidate.EnablePreventFocusChange;
            glControl1.BackColor = Color.FromArgb(3, 8, 15);
            glControl1.Location = new Point(1283, 152);
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
            // PaintForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(3, 8, 15);
            ClientSize = new Size(1920, 1080);
            Controls.Add(label1);
            Controls.Add(glControl1);
            Name = "PaintForm";
            Text = "PaintForm";
            Load += PaintForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private GLControl glControl1;
        private Label label1;
    }
}
