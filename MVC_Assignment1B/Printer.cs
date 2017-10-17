using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Assignment1B
{
    [Serializable()]
    class Printer : AnyItem
    {
        // constructor
        public Printer(string itemName, int x_at, int y_at, string type)
			: base(itemName, x_at, y_at, type)
		{
        }

        // override method to display item as text
        public override string ToString()
        {
            return "A " + type.ToString() + " Printer is placed at " + this.Position();
        }

        // override method to display item as graphics
        public override void Display(Graphics g)
        {
            if (g != null)
            {
                // Create pen.
                Pen blackPen = new Pen(Color.Black, 3);
                Pen pen = new Pen(Color.Black, 2);

                // Create points that define polygon.
                Point point1 = new Point(x+40, y+25);
                Point point2 = new Point(x+45, y+25);
                Point point3 = new Point(x + 45, y + 55);
                Point point4 = new Point(x + 38, y + 55);
                Point point5 = new Point(x + 38, y + 59);
                Point point6 = new Point(x + 30, y + 59);
                Point point7 = new Point(x + 30, y + 65);
                Point point22 = new Point(x + 30, y + 65);
                Point point23 = new Point(x + 30, y + 70);
                Point point24 = new Point(x + 10, y + 70);
                Point point25 = new Point(x + 10, y + 65);
                Point point8 = new Point(x + 10, y + 65);
                Point point9 = new Point(x + 10, y + 59);
                Point point10 = new Point(x + 2, y + 59);
                Point point11 = new Point(x + 2, y + 55);
                Point point12 = new Point(x - 5, y + 55);
                Point point13 = new Point(x - 5, y + 25);
                //Point point14 = new Point(x - 5, y + 25);
                Point[] curvePoints =
                         {
                 point1,
                 point2,
                 point3,
                 point4,
                 point5,
                 point6,
                 point7,
                 point22,
                 point23,
                 point24,
                 point25,
                 point8,
                 point9,
                 point10,
                 point11,
                 point12,
                 point13
             };
                // Create points that define line.
                Point point14 = new Point(x, y+35);
                Point point15 = new Point(x+40, y+35);
                Point point16 = new Point(x+2, y + 59);
                Point point17 = new Point(x + 2, y + 47);
                Point point18 = new Point(x +2, y + 47);
                Point point19 = new Point(x + 38, y + 47);
                Point point20 = new Point(x + 38, y + 47);
                Point point21 = new Point(x + 38, y + 59);

                Point point26 = new Point(x + 10, y + 65);
                Point point27 = new Point(x + 10, y + 47);
                Point point28 = new Point(x + 30, y + 65);
                Point point29 = new Point(x + 30, y + 47);

                Point point30 = new Point(x + 27, y + 53);
                Point point31 = new Point(x + 13, y + 53);
                Point point32 = new Point(x + 27, y + 59);
                Point point33 = new Point(x + 13, y + 59);
                Point point34 = new Point(x + 27, y + 65);
                Point point35 = new Point(x + 13, y + 65);

                System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 7);
                System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                if (type.ToString() == "Ink")
                {
                    g.DrawString("Ink", drawFont, drawBrush, x + 9, y + 10);
                }
                else if (type.ToString() == "Laser")
                {
                    g.DrawString("Laser", drawFont, drawBrush, x + 8, y + 10);
                }
                g.DrawRectangle(pen, x+5, y+10, 30, 15);
              
                g.DrawLine(blackPen, point14, point15);
                g.DrawLine(blackPen, point16, point17);
                g.DrawLine(blackPen, point18, point19);
                g.DrawLine(blackPen, point20, point21);
                g.DrawLine(blackPen, point26, point27);
                g.DrawLine(blackPen, point28, point29);
                g.DrawLine(pen, point30, point31);
                g.DrawLine(pen, point32, point33);
                g.DrawLine(pen, point34, point35);
                g.DrawEllipse(pen,x+35,y+38,5,5);
                g.DrawEllipse(pen, x + 27, y+38, 5, 5);
                // Draw polygon to screen.
                g.DrawPolygon(blackPen, curvePoints);
            }

            // if shape needs border to be drawn
            if (Highlight)
            {
                // add in border if shape selected
                // to define point and size
                Point pt = new Point(x -10, y+6); // to avoid shadow

                int borderSide = 60; // make slightly smaller than shape to avoid shadow
                int borderTop = 70;
                Size size = new Size(borderSide, borderTop);
                // draw border
                Pen p = new Pen(Color.Red, 2);
                p.DashStyle = DashStyle.Solid;
                g.DrawRectangle(p, new Rectangle(pt, size));
                p.Dispose();
            }
        }

        public override int x_pos //non abstract property
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public override int y_pos //non abstract property
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public override string type //non abstract property
        {
            get
            {
                return Type;
            }
            set
            {
                Type = value;
            }
        }

        public override string name //non abstract property
        {
            get
            {
                return ItemName;
            }
            set
            {
                ItemName = value;
            }
        }


        public override bool HitTest(Point p)
        {
            GraphicsPath pth = new GraphicsPath();
            pth.AddRectangle(new Rectangle(x, y, 47, 67));
            bool retval = pth.IsVisible(p);
            pth.Dispose();
            return retval;
        }
    }
}
