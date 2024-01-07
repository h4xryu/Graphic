using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.IO.Ports;
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
            System.Text.ASCIIEncoding asciiEncodingSealed1 = new System.Text.ASCIIEncoding();
            System.Text.DecoderReplacementFallback decoderReplacementFallback1 = new System.Text.DecoderReplacementFallback();
            System.Text.EncoderReplacementFallback encoderReplacementFallback1 = new System.Text.EncoderReplacementFallback();
            glControl1 = new GLControl();
            closeBtn = new Button();
            zedGraphControl1 = new ZedGraphControl();
            comboBox_port = new ComboBox();
            btnSerial = new Button();
            serialPort1 = new SerialPort(components);
            label_status = new System.Windows.Forms.Label();
            richTextBox_received = new RichTextBox();
            textBox_send = new TextBox();
            button_send = new Button();
            button_disconnect = new Button();
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
            // zedGraphControl1
            // 
            zedGraphControl1.ForeColor = SystemColors.ControlLightLight;
            zedGraphControl1.Location = new Point(62, 382);
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
            // comboBox_port
            // 
            comboBox_port.BackColor = SystemColors.MenuBar;
            comboBox_port.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_port.FormattingEnabled = true;
            comboBox_port.Location = new Point(5, 2);
            comboBox_port.Margin = new Padding(2);
            comboBox_port.Name = "comboBox_port";
            comboBox_port.Size = new Size(118, 23);
            comboBox_port.TabIndex = 4;
            comboBox_port.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // btnSerial
            // 
            btnSerial.FlatStyle = FlatStyle.System;
            btnSerial.Location = new Point(135, 2);
            btnSerial.Margin = new Padding(2);
            btnSerial.Name = "btnSerial";
            btnSerial.Size = new Size(73, 21);
            btnSerial.TabIndex = 5;
            btnSerial.Text = "connect";
            btnSerial.UseVisualStyleBackColor = true;
            btnSerial.Click += btnSerial_Click;
            // 
            // serialPort1
            // 
            serialPort1.BaudRate = 115200;
            serialPort1.DataBits = 8;
            serialPort1.DiscardNull = false;
            serialPort1.DtrEnable = false;
            serialPort1.Encoding = asciiEncodingSealed1;
            serialPort1.Handshake = Handshake.None;
            serialPort1.NewLine = "\n";
            serialPort1.Parity = Parity.None;
            serialPort1.ParityReplace = 63;
            serialPort1.PortName = "COM1";
            serialPort1.ReadBufferSize = 4096;
            serialPort1.ReadTimeout = -1;
            serialPort1.ReceivedBytesThreshold = 1;
            serialPort1.RtsEnable = false;
            serialPort1.StopBits = StopBits.One;
            serialPort1.WriteBufferSize = 2048;
            serialPort1.WriteTimeout = -1;
            serialPort1.DataReceived += serialPort1_DataReceived;
            // 
            // label_status
            // 
            label_status.AutoSize = true;
            label_status.ForeColor = SystemColors.ControlLightLight;
            label_status.Location = new Point(5, 27);
            label_status.Margin = new Padding(2, 0, 2, 0);
            label_status.Name = "label_status";
            label_status.Size = new Size(107, 15);
            label_status.TabIndex = 6;
            label_status.Text = "포트를 연결하세요";
            // 
            // richTextBox_received
            // 
            richTextBox_received.BackColor = Color.FromArgb(3, 8, 15);
            richTextBox_received.ForeColor = Color.Lime;
            richTextBox_received.Location = new Point(62, 52);
            richTextBox_received.Margin = new Padding(2);
            richTextBox_received.Name = "richTextBox_received";
            richTextBox_received.Size = new Size(695, 310);
            richTextBox_received.TabIndex = 7;
            richTextBox_received.Text = "";
            // 
            // textBox_send
            // 
            textBox_send.BackColor = Color.FromArgb(20, 28, 35);
            textBox_send.ForeColor = Color.Lime;
            textBox_send.Location = new Point(320, 2);
            textBox_send.Margin = new Padding(2);
            textBox_send.Name = "textBox_send";
            textBox_send.Size = new Size(330, 23);
            textBox_send.TabIndex = 8;
            // 
            // button_send
            // 
            button_send.FlatStyle = FlatStyle.System;
            button_send.Location = new Point(684, 2);
            button_send.Margin = new Padding(2);
            button_send.Name = "button_send";
            button_send.Size = new Size(73, 22);
            button_send.TabIndex = 9;
            button_send.Text = "send";
            button_send.UseVisualStyleBackColor = true;
            button_send.Click += Button_send_Click;
            // 
            // button_disconnect
            // 
            button_disconnect.FlatStyle = FlatStyle.System;
            button_disconnect.Location = new Point(212, 2);
            button_disconnect.Margin = new Padding(2);
            button_disconnect.Name = "button_disconnect";
            button_disconnect.Size = new Size(73, 21);
            button_disconnect.TabIndex = 10;
            button_disconnect.Text = "disconnect";
            button_disconnect.UseVisualStyleBackColor = true;
            button_disconnect.Click += button_disconnect_Click_1;
            // 
            // PaintForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(3, 8, 15);
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(1493, 796);
            Controls.Add(button_disconnect);
            Controls.Add(button_send);
            Controls.Add(textBox_send);
            Controls.Add(richTextBox_received);
            Controls.Add(label_status);
            Controls.Add(btnSerial);
            Controls.Add(comboBox_port);
            Controls.Add(glControl1);
            Controls.Add(zedGraphControl1);
            Controls.Add(closeBtn);
            FormBorderStyle = FormBorderStyle.None;
            Location = new Point(200, 100);
            Margin = new Padding(2);
            Name = "PaintForm";
            StartPosition = FormStartPosition.Manual;
            Text = "PaintForm";
            Load += PaintForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private GLControl glControl1;
        private Button closeBtn;
        private ZedGraphControl zedGraphControl1;
        private ComboBox comboBox_port;
        private Button btnSerial;
        private SerialPort serialPort1;
        private System.Windows.Forms.Label label_status;
        private RichTextBox richTextBox_received;
        private TextBox textBox_send;
        private Button button_send;
        private Button button_disconnect;
    }
}
