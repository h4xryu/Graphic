using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
namespace Graphic
{
    public partial class PaintForm : Form
    {
        ushort uigrp = 0;
        Matrix4 modelview, projection;
        ObjMesh mesh1 = null;
        const bool HighQuality = true;

        public PaintForm()
        {
            this.Paint += new System.Windows.Forms.PaintEventHandler(PaintForm_Paint);
            InitializeComponent();
        }

        private void PaintForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString("Paint Event!!", new Font("����", 20), Brushes.Black, 50, 50);

        }

        private void PaintForm_Load(object sender, EventArgs e)
        {


        }

        private void btnPaint_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            g.DrawString("Create Graphics!!", new Font("����", 20), Brushes.Black, 50, 80);
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

            mesh1 = new ObjMesh("C:/object/Aircraft.obj");
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
            GL.Clear(ClearBufferMask.ColorBufferBit |
                     ClearBufferMask.DepthBufferBit |
                     ClearBufferMask.StencilBufferBit);

            if (HighQuality)
            {
                GL.PointParameter(PointParameterName.PointDistanceAttenuation,
                    new float[] { 0, 0, (float)Math.Pow(1 / (projection.M11 * Width / 2), 2) });
            }

            modelview = Matrix4.LookAt(0f, 20f, zoomFactor, 0, 0, 0, 0.0f, 1.0f, 0.0f);
            var aspect_ratio = Width / (float)Height;
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver6, aspect_ratio, 1, 256);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);
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
        private float panX = 0;
        private float panY = 0;

        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                angleX += (e.X - _mouseStartX);
                angleY -= (e.Y - _mouseStartY);

                this.Cursor = Cursors.Cross;

                glControl1.Invalidate();
            }
            if (e.Button == MouseButtons.Left)
            {
                panX += (e.X - _mouseStartX);
                panY -= (e.Y - _mouseStartY);
                GL.Viewport((int)panX, (int)panY, glControl1.Width, glControl1.Height); // Use all of the glControl painting area
                this.Cursor = Cursors.Hand;
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
        }

        private void glControl1_Load_1(object sender, EventArgs e)
        {
            glControl1.MouseMove += glControl_MouseMove;
            glControl1.MouseWheel += glControl_MouseWheel;
            glControl1.MouseUp += glControl1_MouseUp;
            glControl1.Paint += glControl1_Paint;

            GL.ClearColor(Color.DarkSlateGray);
            GL.Color3(1f, 1f, 1f); // Points Color
            SetupViewport();
        }
    }
}
