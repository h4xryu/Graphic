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
            btnPaint = new Button();
            SuspendLayout();
            // 
            // glControl1
            // 
            glControl1.AutoValidate = AutoValidate.EnablePreventFocusChange;
            glControl1.BackColor = Color.Black;
            glControl1.Location = new Point(374, 120);
            glControl1.Margin = new Padding(4, 5, 4, 5);
            glControl1.Name = "glControl1";
            glControl1.Size = new Size(182, 106);
            glControl1.TabIndex = 1;
            glControl1.TabStop = false;
            glControl1.VSync = false;
            glControl1.Load += glControl1_Load_1;
            // 
            // btnPaint
            // 
            btnPaint.Location = new Point(107, 160);
            btnPaint.Name = "btnPaint";
            btnPaint.Size = new Size(94, 29);
            btnPaint.TabIndex = 0;
            btnPaint.Text = "Paint";
            btnPaint.UseVisualStyleBackColor = true;
            btnPaint.Click += btnPaint_Click;
            // 
            // PaintForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1920, 1080);
            Controls.Add(btnPaint);
            Controls.Add(glControl1);
            Name = "PaintForm";
            Text = "PaintForm";
            Load += PaintForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnPaint;
        private GLControl glControl1;
    }
}
