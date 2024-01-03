using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;


namespace Graphic
{
    public partial class PaintForm 
    {
        double x, y;           //XAxis YAxis
        LineItem line;        //라인
        PointPairList Speed;  //그래프 점.

        
        private void Graph_init()
        {
            //Zed_Timer.Start(); // Timer Start

            /**********그래프 설정**********/
            GraphPane MyPane = zedGraphControl1.GraphPane;
            MyPane.Chart.Fill = new Fill(Color.FromArgb(3,8,15), Color.FromArgb(3, 8, 15), 180.0f);
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
            MyPane.XAxis.Scale.MinorStep = 5;    //작은 눈금 
            MyPane.XAxis.Scale.MajorStep = 10;   //큰 눈금
            MyPane.XAxis.Scale.Min = 0;
            MyPane.XAxis.Scale.Max = 100;
            MyPane.YAxis.Scale.MinorStep = 5;
            MyPane.YAxis.Scale.MajorStep = 10;
            MyPane.YAxis.Scale.Min = 0;
            MyPane.YAxis.Scale.Max = 100;

            /**********눈금 색**********/
            MyPane.XAxis.MajorTic.Color = Color.Orange;
            MyPane.YAxis.MajorTic.Color = Color.Orange;

            Speed = new PointPairList();
            line = MyPane.AddCurve("Speed", Speed, Color.Orange, SymbolType.Circle);//라인

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void UP_btn_Click(object sender, EventArgs e)
        {
            y += 10;
        }

        private void DOWN_btn_Click(object sender, EventArgs e)
        {
            y -= 10;
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
            Speed.Add(x, y);

            x += 10;
            if (x > 100)
            {
                x = 0;
                Speed.Clear();
            }
            zedGraphControl1.Refresh();
        }
    }
}