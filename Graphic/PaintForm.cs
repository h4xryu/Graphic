using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using ZedGraph;
using System.IO.Ports;
using System.Threading;
using System.Runtime.Intrinsics.X86;

namespace Graphic
{

    public partial class PaintForm : Form
    {
        ushort uigrp = 0;
        Matrix4 modelview, projection;
        ObjMesh mesh1 = null;
        const bool HighQuality = true;
        private int command_bufsize = 0;
        private Thread term;
        public PaintForm()
        {

            this.Paint += new System.Windows.Forms.PaintEventHandler(PaintForm_Paint);
            InitializeComponent();
            Graph_init();
        }

        private void PaintForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString("Paint Event!!", new Font("굴림", 20), Brushes.Black, 50, 50);

        }

        private void PaintForm_Load(object sender, EventArgs e)
        {
            comboBox_port.DataSource = SerialPort.GetPortNames();
            button_send.Enabled = false;
        }

        private void btnPaint_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            g.DrawString("Create Graphics!!", new Font("굴림", 20), Brushes.Black, 50, 80);
        }


        private void SetupViewport()
        {
            if (this.WindowState == FormWindowState.Minimized) return;
            //glControl1.Width = this.Width - 64;
            //glControl1.Height = this.Height - 128;
            GL.MatrixMode(MatrixMode.Projection);
            //GL.LoadIdentity();
            GL.Ortho(0, glControl1.Width, 0, glControl1.Height, -1, 1);
            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);
            GL.Enable(EnableCap.DepthTest);

            // Improve visual quality at the expense of performance
            if (HighQuality)
            {
                int max_size;
                GL.GetInteger(GetPName.PointSizeMax, out max_size);
                GL.Enable(EnableCap.PointSmooth);
            }

            //mesh1 = new ObjMesh("C:/object/AircraftTest.obj");
            mesh1 = new ObjMesh(Application.StartupPath + "\\Aircraft.obj");
            mesh1.Prepare();

            float aspect_ratio = this.Width / (float)this.Height;
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, 1, 1024);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            //Set Lighting
            float[] mat_specular = { 1.0f, 1.0f, 1.0f, 1.0f };
            float[] mat_shininess = { 30.0f };
            float[] light_position = { 1.0f, 1.0f, 1.0f, 0.0f };
            float[] light_ambient = { 0.5f, 0.5f, 0.5f, 1.0f };

            GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            GL.ShadeModel(ShadingModel.Smooth);

            GL.Material(MaterialFace.Front, MaterialParameter.Specular, mat_specular);
            GL.Material(MaterialFace.Front, MaterialParameter.Shininess, mat_shininess);
            GL.Light(LightName.Light0, LightParameter.Position, light_position);
            GL.Light(LightName.Light0, LightParameter.Ambient, light_ambient);
            GL.Light(LightName.Light0, LightParameter.Diffuse, mat_specular);

            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.ColorMaterial);
            GL.Enable(EnableCap.CullFace);
        }


        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            GL.ClearColor(Color.FromArgb(3, 8, 15));
            GL.Clear(ClearBufferMask.ColorBufferBit |
                     ClearBufferMask.DepthBufferBit
                     | ClearBufferMask.StencilBufferBit);

            if (HighQuality)
            {
                GL.PointParameter(PointParameterName.PointDistanceAttenuation,
                    new float[] { 0, 0, (float)Math.Pow(1 / (projection.M11 * Width / 2), 2) });
            }

            modelview = Matrix4.LookAt(0.0f, 16f, zoomFactor, 0, 0, 0, 0.0f, 1.0f, 0.0f);
            var aspect_ratio = Width / (float)Height;
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver6, aspect_ratio, 1, 256);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);
            GL.Rotate(angleZ, 0, 0, 1.0f);
            GL.Rotate(angleY, 1.0f, 0, 0);
            GL.Rotate(angleX, 0, 1.0f, 0);

            // draw a VBO:
            mesh1.Render();

            glControl1.SwapBuffers();

        }

        #region GLControl. Mouse event handlers
        private int _mouseStartX = 0;
        private int _mouseStartY = 0;
        private float angleX = 0;
        private float angleY = 0;
        private float angleZ = 0;
        private float panX = 0;
        private float panY = 0;
        private int job = 0;
        private int left_flag = 0;
        private int right_flag = 0;

        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                right_flag = 1;
                angleX -= (e.X - _mouseStartX);
                angleY -= (e.Y - _mouseStartY);

                this.Cursor = Cursors.Cross;

                glControl1.Invalidate();
            }
            if (e.Button == MouseButtons.Left)
            {

                left_flag = 1;
                //panX += (e.X - _mouseStartX);
                //panY -= (e.Y - _mouseStartY);
                //GL.Viewport((int)panX, (int)panY, glControl1.Width, glControl1.Height); // Use all of the glControl painting area
                //this.Cursor = Cursors.Hand;


                // angleY -= (e.Y - _mouseStartY);

                angleZ += (e.X - _mouseStartX);




                this.Cursor = Cursors.Cross;
                glControl1.Invalidate();
            }
            _mouseStartX = e.X;
            _mouseStartY = e.Y;
        }

        float zoomFactor = 1;
        private void glControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0) zoomFactor += 7f;
            else zoomFactor -= 7f;
            glControl1.Invalidate();
        }
        #endregion    

        private void glControl1_MouseUp(object sender, MouseEventArgs e)
        {
            left_flag = 0;
            right_flag = 0;
            this.Cursor = Cursors.Default;
            angleX = 0.0f;
            angleY = 0.0f;
            angleZ = 0.0f;
            job = 0;
            GL.LoadIdentity();
            glControl1.Invalidate();

        }

        private void glControl1_Load_1(object sender, EventArgs e)
        {
            glControl1.MouseMove += glControl_MouseMove;
            glControl1.MouseWheel += glControl_MouseWheel;
            glControl1.MouseUp += glControl1_MouseUp;
            glControl1.Paint += glControl1_Paint;

            GL.ClearColor(Color.FromArgb(3, 8, 15));
            GL.Color3(1f, 1f, 1f); // Points Color
            SetupViewport();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)  //시리얼포트가 열려 있으면
            {
                serialPort1.Close();  //시리얼포트 닫기
            }
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSerial_Click(object sender, EventArgs e)
        {
            button_send.Enabled = true;

            if (!serialPort1.IsOpen)  //시리얼포트가 열려 있지 않으면
            {

                serialPort1.PortName = comboBox_port.Text;
                try
                {
                    serialPort1.Open();  //시리얼포트 열기
                    
                    serialPort1.Write("$");
                }
                catch (Exception err)
                {
                    label_status.Text = err.Message;
                    return;
                }
                label_status.Text = "포트가 열렸습니다.";
                comboBox_port.Enabled = false;  //COM포트설정 콤보박스 비활성화
            }
            else  //시리얼포트가 열려 있으면
            {
                label_status.Text = "포트가 이미 열려 있습니다.";
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            this.Invoke(new EventHandler(MySerialReceived));
        }
        private void MySerialReceived(object s, EventArgs e)  //여기에서 수신 데이타를 사용자의 용도에 따라 처리한다.
        {
            term = new Thread(Terminal_thread);
            term.Start();
            term.IsBackground = true;
        }
        private void Button_send_Click(object sender, EventArgs e)  //보내기 버튼을 클릭하면
        {
            serialPort1.Write(textBox_send.Text);  //텍스트박스의 텍스트를 시리얼통신으로 송신
            textBox_send.Text = string.Empty;
        }

        private void button_disconnect_Click_1(object sender, EventArgs e)
        {
            button_send.Enabled = false;
            if (serialPort1.IsOpen)  //시리얼포트가 열려 있으면
            {
                serialPort1.Write("^");
                serialPort1.Close();  //시리얼포트 닫기

                label_status.Text = "포트가 닫혔습니다.";
                comboBox_port.Enabled = true;  //COM포트설정 콤보박스 활성화
            }
            else  //시리얼포트가 닫혀 있으면
            {
                label_status.Text = "포트가 이미 닫혀 있습니다.";
            }
        }

        private void Terminal_thread()
        {
            string? ReceiveData = string.Empty;
            try 
            { 
                ReceiveData = (string)serialPort1.ReadLine();//시리얼 버터에 수신된 데이타를 ReceiveData 읽어오기
                string[] lines = ReceiveData.Split(new string[] { "\n\r", "\r", "\n" }, StringSplitOptions.None);

                foreach (string cmdLine in lines)
                {
                    if (cmdLine.Equals("Waiting for devices..", StringComparison.OrdinalIgnoreCase)) // 데이터 시작 부분
                    {
                        //MessageBox.Show("test1");
                    }
                    else if (cmdLine.Equals("Data Received", StringComparison.OrdinalIgnoreCase)) // 데이터 시작 부분
                    {
                        //MessageBox.Show("test1");
                    }

                    else if (cmdLine.StartsWith("acc : (", StringComparison.OrdinalIgnoreCase)) // acc 데이터 받는 부분
                    {
                        //MessageBox.Show("test2"); //데이터 버퍼에 저장
                        if (cmdLine.EndsWith(")", StringComparison.OrdinalIgnoreCase)) //버퍼저장 종료
                    }
                }

            }  
            catch (Exception err)
            {
                
            }

            

            if (command_bufsize < 16) // 터미널 박스 과부화 방지
            {

                if (richTextBox_received.InvokeRequired)
                {
                    richTextBox_received.Invoke(new MethodInvoker(() => { richTextBox_received.Text += ReceiveData; }));
                }
                else richTextBox_received.Text = richTextBox_received.Text + ReceiveData;


                command_bufsize++;
            }
            else
            {

                if (richTextBox_received.InvokeRequired)
                {
                    richTextBox_received.Invoke(new MethodInvoker(() =>
                    {
                        int startIndex = richTextBox_received.GetFirstCharIndexFromLine(0);
                        int endIndex = richTextBox_received.GetFirstCharIndexFromLine(1) - 1;
                        richTextBox_received.Select(startIndex, endIndex - startIndex + 1);
                        richTextBox_received.SelectedText = string.Empty;
                        richTextBox_received.Invoke(new MethodInvoker(() => { richTextBox_received.Text += ReceiveData; }));
                    }));
                }
                else richTextBox_received.Text = richTextBox_received.Text + ReceiveData;
            }
    
        }
    }
}
