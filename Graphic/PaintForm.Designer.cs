using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;
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
            ASCIIEncoding asciiEncoding1 = new ASCIIEncoding();
            DecoderReplacementFallback decoderReplacementFallback1 = new DecoderReplacementFallback();
            EncoderReplacementFallback encoderReplacementFallback1 = new EncoderReplacementFallback();
            glControl1 = new GLControl();
            closeBtn = new Button();
            zedGraphControl1 = new ZedGraphControl();
            comboBox_port = new ComboBox();
            btnSerial = new Button();
            serialPort1 = new SerialPort(components);
            label_status = new System.Windows.Forms.Label();
            richTextBox_received = new RichTextBox();
            button_disconnect = new Button();
            myTimer = new System.Windows.Forms.Timer(components);
            pointDebug = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // glControl1
            // 
            glControl1.AutoValidate = AutoValidate.EnablePreventFocusChange;
            glControl1.BackColor = Color.FromArgb(3, 8, 15);
            glControl1.BackgroundImageLayout = ImageLayout.None;
            glControl1.ForeColor = Color.FromArgb(3, 8, 15);
            glControl1.Location = new Point(1306, 543);
            glControl1.Margin = new Padding(4, 5, 4, 5);
            glControl1.Name = "glControl1";
            glControl1.Size = new Size(553, 388);
            glControl1.TabIndex = 1;
            glControl1.TabStop = false;
            glControl1.VSync = false;
            glControl1.Load += glControl1_Load_1;
            // 
            // closeBtn
            // 
            closeBtn.BackColor = Color.FromArgb(15, 30, 45);
            closeBtn.FlatStyle = FlatStyle.Popup;
            closeBtn.Location = new Point(1746, 985);
            closeBtn.Margin = new Padding(0);
            closeBtn.Name = "closeBtn";
            closeBtn.Size = new Size(113, 31);
            closeBtn.TabIndex = 0;
            closeBtn.TabStop = false;
            closeBtn.Text = "close";
            closeBtn.UseVisualStyleBackColor = false;
            closeBtn.Click += closeBtn_Click;
            // 
            // zedGraphControl1
            // 
            zedGraphControl1.ForeColor = SystemColors.ControlLightLight;
            zedGraphControl1.Location = new Point(80, 509);
            zedGraphControl1.Margin = new Padding(4, 5, 4, 5);
            zedGraphControl1.Name = "zedGraphControl1";
            zedGraphControl1.ScrollGrace = 0D;
            zedGraphControl1.ScrollMaxX = 0D;
            zedGraphControl1.ScrollMaxY = 0D;
            zedGraphControl1.ScrollMaxY2 = 0D;
            zedGraphControl1.ScrollMinX = 0D;
            zedGraphControl1.ScrollMinY = 0D;
            zedGraphControl1.ScrollMinY2 = 0D;
            zedGraphControl1.Size = new Size(698, 443);
            zedGraphControl1.TabIndex = 3;
            zedGraphControl1.UseExtendedPrintDialog = true;
            zedGraphControl1.Load += zedGraphControl1_Load;
            // 
            // comboBox_port
            // 
            comboBox_port.BackColor = Color.FromArgb(3, 8, 15);
            comboBox_port.FlatStyle = FlatStyle.Popup;
            comboBox_port.ForeColor = Color.Lime;
            comboBox_port.FormattingEnabled = true;
            comboBox_port.ImeMode = ImeMode.NoControl;
            comboBox_port.Location = new Point(1306, 985);
            comboBox_port.Name = "comboBox_port";
            comboBox_port.Size = new Size(151, 28);
            comboBox_port.TabIndex = 4;
            comboBox_port.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // btnSerial
            // 
            btnSerial.BackColor = Color.FromArgb(15, 30, 45);
            btnSerial.FlatStyle = FlatStyle.Popup;
            btnSerial.Location = new Point(1491, 985);
            btnSerial.Name = "btnSerial";
            btnSerial.Size = new Size(108, 31);
            btnSerial.TabIndex = 5;
            btnSerial.Text = "connect";
            btnSerial.UseVisualStyleBackColor = false;
            btnSerial.Click += btnSerial_Click;
            // 
            // serialPort1
            // 
            serialPort1.BaudRate = 115200;
            serialPort1.DataBits = 8;
            serialPort1.DiscardNull = false;
            serialPort1.DtrEnable = false;
            //asciiEncoding1.DecoderFallback = decoderReplacementFallback1;
            //asciiEncoding1.EncoderFallback = encoderReplacementFallback1;
            serialPort1.Encoding = asciiEncoding1;
            serialPort1.Handshake = Handshake.None;
            serialPort1.NewLine = "\n";
            serialPort1.Parity = Parity.None;
            serialPort1.ParityReplace = 63;
            serialPort1.PortName = "COM3";
            serialPort1.ReadBufferSize = 2048;
            serialPort1.ReadTimeout = 500;
            serialPort1.ReceivedBytesThreshold = 1;
            serialPort1.RtsEnable = false;
            serialPort1.StopBits = StopBits.One;
            serialPort1.WriteBufferSize = 1024;
            serialPort1.WriteTimeout = 500;
            serialPort1.DataReceived += serialPort1_DataReceived;
            // 
            // label_status
            // 
            label_status.AutoSize = true;
            label_status.ForeColor = SystemColors.ControlLightLight;
            label_status.Location = new Point(1306, 963);
            label_status.Name = "label_status";
            label_status.Size = new Size(134, 20);
            label_status.TabIndex = 6;
            label_status.Text = "포트를 연결하세요";
            // 
            // richTextBox_received
            // 
            richTextBox_received.BackColor = Color.FromArgb(3, 8, 15);
            richTextBox_received.BorderStyle = BorderStyle.None;
            richTextBox_received.ForeColor = Color.Lime;
            richTextBox_received.Location = new Point(80, 22);
            richTextBox_received.Name = "richTextBox_received";
            richTextBox_received.Size = new Size(367, 183);
            richTextBox_received.TabIndex = 7;
            richTextBox_received.Text = "";
            richTextBox_received.TextChanged += richTextBox_received_TextChanged;
            // 
            // button_disconnect
            // 
            button_disconnect.BackColor = Color.FromArgb(15, 30, 45);
            button_disconnect.FlatStyle = FlatStyle.Popup;
            button_disconnect.Location = new Point(1620, 985);
            button_disconnect.Name = "button_disconnect";
            button_disconnect.Size = new Size(108, 31);
            button_disconnect.TabIndex = 10;
            button_disconnect.Text = "disconnect";
            button_disconnect.UseVisualStyleBackColor = false;
            button_disconnect.Click += button_disconnect_Click_1;
            // 
            // pointDebug
            // 
            pointDebug.AutoSize = true;
            pointDebug.ForeColor = Color.Lime;
            pointDebug.Location = new Point(1700, 36);
            pointDebug.Name = "pointDebug";
            pointDebug.Size = new Size(26, 20);
            pointDebug.TabIndex = 11;
            pointDebug.Text = "x,y";
     
            // 
            // PaintForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(3, 8, 15);
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(1920, 1061);
            Controls.Add(pointDebug);
            Controls.Add(button_disconnect);
            Controls.Add(richTextBox_received);
            Controls.Add(label_status);
            Controls.Add(btnSerial);
            Controls.Add(comboBox_port);
            Controls.Add(glControl1);
            Controls.Add(zedGraphControl1);
            Controls.Add(closeBtn);
            FormBorderStyle = FormBorderStyle.None;
            Location = new Point(200, 100);
            Name = "PaintForm";
            StartPosition = FormStartPosition.Manual;
            Text = "PaintForm";
            Load += PaintForm_Load;
            MouseMove += PaintForm_MouseMove;
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
        private Button button_disconnect;
        private System.Windows.Forms.Timer myTimer;
        private System.Windows.Forms.Label pointDebug;
    }
}
