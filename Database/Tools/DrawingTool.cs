using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCY_Database.Tools
{
    public static class DrawingTool
    {
        public static void DrawRoundedRectangle(Graphics g, RectangleF r, float radius, Pen p)
        {
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            float d = 1.0f * radius;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d,r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            gp.CloseFigure();

            //g.FillPath(System.Drawing.Brushes.LightBlue, gp);
            using (System.Drawing.Drawing2D.LinearGradientBrush brush =
                new System.Drawing.Drawing2D.LinearGradientBrush(r, Color.AliceBlue,
                Color.LightBlue, System.Drawing.Drawing2D.LinearGradientMode.Vertical))
            {
                g.FillPath(brush, gp);
            }
            g.DrawPath(p, gp);
            

        }

    }
}
