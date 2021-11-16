using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyWin11
{
    public partial class ClockWindow : Form
    {
        public ClockWindow()
        {
            InitializeComponent();

            SetStyle();

            ShowTime();

            SetPosition();

            StartTime();
        }

        private void SetStyle()
        {
            FormBorderStyle = FormBorderStyle.None;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AutoSize = true;
            TopMost = true;
        }

        private void ShowTime()
        {
            Controls.Add(new Label() { Text = System.DateTime.Now.ToString("ddd HH:mm:ss"), AutoSize = true });
        }

        private void StartTime()
        {
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += (o, e) =>
            {
                Controls[0].Text = System.DateTime.Now.ToString("ddd HH:mm:ss");
            };

            timer.Start();
        }

        private void SetPosition()
        {
            CenterToScreen();

            Top = 0;
        }


        /// <summary>
        /// 设置窗体的Region
        /// </summary>
        public void SetWindowRegion()
        {
            GraphicsPath FormPath;
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            FormPath = GetRoundedRectPath(rect, 10);
            Region = new Region(FormPath);

        }
        /// <summary>
        /// 绘制圆角路径
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();

            // 左上角
            path.AddArc(arcRect, 180, 90);

            // 右上角
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);

            // 右下角
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);

            // 左下角
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();//闭合曲线
            return path;
        }

        /// <summary>
        /// 窗体size发生改变时重新设置Region属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(object sender, EventArgs e)
        {
            SetWindowRegion();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetWindowRegion();
        }
    }
}
