using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using ZedGraph;
using System.IO.Ports;
using System.Threading;
using System.Runtime.Intrinsics.X86;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.LinkLabel;
using ZedGraph;

namespace Graphic
{
    public partial class PaintForm : Form
    {
        private System.Text.ASCIIEncoding asciiEncodingSealed1 = new System.Text.ASCIIEncoding();
        private System.Text.DecoderReplacementFallback decoderReplacementFallback1 = new System.Text.DecoderReplacementFallback();
        private System.Text.EncoderReplacementFallback encoderReplacementFallback1 = new System.Text.EncoderReplacementFallback();
        ushort uigrp = 0;
        Matrix4 modelview, projection;
        ObjMesh mesh1 = null;
        const bool HighQuality = true;
        private int command_bufsize = 0;
        private Thread term;
        private List<string?> acc;
        private List<string?> gyro = new List<string?>();
        private List<string?> mag;
        private List<string?> ypr;
        private float old_angleX = 0;
        private float old_angleY = 0;
        private float res = 10;
        private float diffX = 0;
        private float diffY = 0;
        private string[] values;
        private bool ang_flag = false;
        private SWSleep utimer;
        private bool isRunning = false;
        int centerX = 1050;
        int centerY = 710;
        /*************************************Graph************************************/
        double x, y_pitch, y_roll, y_yaw, t;           //XAxis YAxis
        LineItem line_pitch;        //라인
        LineItem line_roll;
        LineItem line_yaw;
        PointPairList Pitch;  //그래프 점. pitch
        PointPairList Roll;
        PointPairList Yaw;
        System.Windows.Forms.Timer Zed_Timer = new System.Windows.Forms.Timer();
        GraphPane MyPane;
        Graphics g;
        Graphics g1;
        float arrowEndX, arrowEndY;
        public PaintForm()
        {

            InitializeComponent();
            Graph_init();
            g = this.CreateGraphics();
            g1 = this.CreateGraphics();
        }


        private void PaintForm_Load(object sender, EventArgs e)
        {
            comboBox_port.DataSource = SerialPort.GetPortNames();
            richTextBox_received.Text = "연결하려면 connect버튼을 누르세요.";
        }


       

        private void SetupViewport()
        {
            if (this.WindowState == FormWindowState.Minimized) return;
            GL.MatrixMode(MatrixMode.Projection);
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

        private void PaintForm_MouseMove(object sender, MouseEventArgs e)
        {
            pointDebug.Text = "x,y = ( " + (e.X) + " , " + (e.Y) + " )";
        }

        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Right)
            {
                angleX -= (e.X - _mouseStartX);
                angleY -= (e.Y - _mouseStartY);

                this.Cursor = Cursors.Cross;

                glControl1.Invalidate();
            }
            if (e.Button == MouseButtons.Left)
            {


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

            this.Cursor = Cursors.Default;
            angleX = 0.0f;
            angleY = 0.0f;
            angleZ = 0.0f;
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
            //button_send.Enabled = true;

            if (!serialPort1.IsOpen)  //시리얼포트가 열려 있지 않으면
            {

                serialPort1.PortName = comboBox_port.Text;
                try
                {
                    serialPort1.Open();  //시리얼포트 열기
                    richTextBox_received.Text = string.Empty;
                    Zed_Timer.Start(); // Timer Start
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
        private void MySerialReceived(object? s, EventArgs e)  //여기에서 수신 데이타를 사용자의 용도에 따라 처리한다.
        {


            // 이미 실행 중인 스레드가 있으면 다시 시작하지 않음
            if (term == null || term.ThreadState != ThreadState.Running)
            {
                term = new Thread(Terminal_thread);
                term.IsBackground = true;

                // lock이 성공했을 때만 스레드 시작
                if (Monitor.TryEnter(term))
                {
                    lock (term) { term.Start(); }
                }
            }


            serialPort1.DiscardInBuffer();
            serialPort1.DiscardOutBuffer();

        }
        private void Button_send_Click(object sender, EventArgs e)  //보내기 버튼을 클릭하면
        {
            // serialPort1.Write(textBox_send.Text);  //텍스트박스의 텍스트를 시리얼통신으로 송신
            //textBox_send.Text = string.Empty;
        }

        private void button_disconnect_Click_1(object sender, EventArgs e)
        {
            angleX = 0;
            angleY = 0;
            // button_send.Enabled = false;
            if (serialPort1.IsOpen)  //시리얼포트가 열려 있으면
            {
                Zed_Timer.Stop();
                label_status.Text = "포트가 닫혔습니다.";
                comboBox_port.Enabled = true;  //COM포트설정 콤보박스 활성화
                Pitch.Clear();
                Roll.Clear();
                Yaw.Clear();
                x = 0;
                t = 0;
                serialPort1.Close();  //시리얼포트 닫기

            }
            else  //시리얼포트가 닫혀 있으면
            {
                label_status.Text = "포트가 이미 닫혀 있습니다.";
            }
        }

        private void Terminal_thread()
        {
            utimer = new SWSleep();
           

            string? ReceiveData = string.Empty;



            try
            {
                ReceiveData = (string?)serialPort1.ReadLine(); //시리얼 포트에 수신된 데이타를 ReceiveData 읽어오기
                if (ReceiveData == string.Empty)
                {
                    if (richTextBox_received.InvokeRequired) richTextBox_received.Invoke(new MethodInvoker(() => { richTextBox_received.Text = "데이터를 불러오는 중입니다."; }));
                    else richTextBox_received.Text = "데이터를 불러오는 중입니다.";
                    return;
                }
                if ((ReceiveData.StartsWith("@", StringComparison.OrdinalIgnoreCase)) || ReceiveData.EndsWith("#", StringComparison.OrdinalIgnoreCase))
                {
                    ReceiveData = ReceiveData.Split(new string[] { "@", "#" }, StringSplitOptions.RemoveEmptyEntries)[0];
                }
                else
                {
                    if (richTextBox_received.InvokeRequired) richTextBox_received.Invoke(new MethodInvoker(() => { richTextBox_received.Text = "데이터를 불러오는 중입니다."; }));
                    else richTextBox_received.Text = "데이터를 불러오는 중입니다.";
                    return;
                }



                if ((ReceiveData.StartsWith("(", StringComparison.OrdinalIgnoreCase)) && ReceiveData.EndsWith(")", StringComparison.OrdinalIgnoreCase))
                {
                    int idx_start = ReceiveData.IndexOf('(');
                    int idx_end = ReceiveData.IndexOf(')');

                    this.Invoke(() =>
                        {

                            values = ReceiveData.Remove(0, idx_start + 1).Remove(idx_end - 1, 1).Split(',');

                            if (values.Length < 1)
                            {
                                return;
                            }
                            try
                            {
                                values[0] = RemoveNonNumericCharacters(values[0]);
                                values[1] = RemoveNonNumericCharacters(values[1]);
                                values[2] = RemoveNonNumericCharacters(values[2]);

                                angleX = -((int.Parse(values[0])));
                                angleY = -((int.Parse(values[1])));

                                richTextBox_received.Text = "X축 각도 : " + angleX + "\n" + "Y축 각도 : " + angleY + "\n" + "Z축 각도 : " + -((int.Parse(values[2]))) +
                                "\n시리얼 포트 : " + comboBox_port.Text + "\n" +  "통신속도(Baud rate) : 115200";


                                glControl1.Invalidate();

                                
                                
                                float arrowLength = 100.0f;
                                float arrowWidth = 10.0f;

                                // 화살표의 끝점 좌표 계산


                                
                                // 화살표 그리기
                                

                            }
                            catch (IndexOutOfRangeException e) { }

                            catch (Exception e)
                            {


                            }

                        });


                }
                else
                {
                }



            }

            //MessageBox.Show(ReceiveData);

            // string[] lines = ReceiveData.Split(new string[] { "\n\r", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);



            //  foreach (string cmdLine in lines)
            //{



            //           }

            catch (OperationCanceledException e)
            {
                return;
            }
            catch (Exception err) { }
        }

        private void UpdateAngle(ref float oldAngle, float targetAngle, float diff, ref float currentAngle)
        {
            if (targetAngle > oldAngle)
            {
                oldAngle += diff;
                currentAngle = oldAngle;

            }
            else
            {
                oldAngle -= diff;
                currentAngle = oldAngle;
            }
            glControl1.Invalidate();
            utimer.USleep(1);

        }

        private void richTextBox_received_TextChanged(object sender, EventArgs e)
        {
            richTextBox_received.SelectionStart = richTextBox_received.TextLength;
            richTextBox_received.ScrollToCaret();


        }

        static string RemoveNonNumericCharacters(string input)
        {
            StringBuilder result = new StringBuilder();

            foreach (char c in input)
            {
                if (char.IsDigit(c) || c == '-')
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }


        /*************************************Graph************************************/
        private void Graph_init()
        {

            Zed_Timer.Interval = 100;
            Zed_Timer.Tick += Zed_Timer_Tick;
            MyPane = zedGraphControl1.GraphPane;

            /**********그래프 설정**********/

            MyPane.Chart.Fill = new Fill(Color.FromArgb(3, 8, 15), Color.FromArgb(3, 8, 15), 180.0f);
            //zedGraphControl1.MasterPane.Fill.Color = Color.FromArgb(3, 8, 15);
            foreach (GraphPane pane in zedGraphControl1.MasterPane.PaneList)
            {
                pane.Fill.Color = Color.FromArgb(3, 8, 15); ;
            }
            /**********x축 y축 설정**********/
            MyPane.XAxis.Scale.FontSpec.FontColor = Color.White;
            MyPane.YAxis.Scale.FontSpec.FontColor = Color.White;
            MyPane.XAxis.Title.FontSpec.FontColor = Color.White;
            MyPane.YAxis.Title.FontSpec.FontColor = Color.White;
            MyPane.Title.FontSpec.FontColor = Color.White;
            MyPane.Border.Color = Color.FromArgb(3, 8, 15);
            MyPane.Title.Text = "";
            MyPane.XAxis.Title.Text = "";
            MyPane.YAxis.Title.Text = "";
            MyPane.XAxis.Scale.MinorStep = -1;    //작은 눈금 
            MyPane.XAxis.Scale.MajorStep = 1;   //큰 눈금
            MyPane.XAxis.Scale.Min = 0;
            MyPane.XAxis.Scale.Max = 20;
            MyPane.YAxis.Scale.MinorStep = -5;
            MyPane.YAxis.Scale.MajorStep = 10;
            MyPane.YAxis.Scale.Min = -180;
            MyPane.YAxis.Scale.Max = 180;

            /**********눈금 색**********/
            MyPane.XAxis.MajorTic.Color = Color.FromArgb(23, 28, 35);
            MyPane.YAxis.MajorTic.Color = Color.FromArgb(23, 28, 35);

            Pitch = new PointPairList();
            Roll = new PointPairList();
            Yaw = new PointPairList();
            line_pitch = MyPane.AddCurve("Pitch", Pitch, Color.Orange, SymbolType.None);//라인
            line_roll = MyPane.AddCurve("Roll", Roll, Color.Lime, SymbolType.None);//라인
            line_yaw = MyPane.AddCurve("Yaw", Yaw, Color.Red, SymbolType.None);//라인

        }





        private void STOP_btn_Click(object sender, EventArgs e)
        {
            //Zed_Timer.Stop();
        }

        private void START_btn_Click(object sender, EventArgs e)
        {
            //Zed_Timer.Start();
        }

        private void Zed_Timer_Tick(object sender, EventArgs e)
        {
            Pitch.Add(x, y_pitch);
            Roll.Add(x, y_roll);
            Yaw.Add(x, y_yaw);

            x += 0.1;
            y_pitch = angleX;
            y_roll = angleY;
            try { y_yaw = -((int.Parse(RemoveNonNumericCharacters(values[2])))); }
            catch (System.IndexOutOfRangeException eout) { }
            catch (System.NullReferenceException enull) { }
            catch (System.OverflowException eover) { }
            catch (System.FormatException eform) { }
            catch (Exception defe) { }


            
            if (x >= 20)
            {
                t += 0.1;
                MyPane.XAxis.Scale.Min = t;
                MyPane.XAxis.Scale.Max = x;
            }
            zedGraphControl1.Refresh();
            Invalidate();
           
        }

        
    }
}