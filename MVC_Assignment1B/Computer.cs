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
    class Computer : AnyItem
    {
        // constructor
        public Computer(string itemName, int x_at, int y_at, string type)
			: base(itemName, x_at, y_at, type)
		{
        }

        // override method to display item as text
        public override string ToString()
        {
            return "A " + type.ToString() + " Computer is placed at " + this.Position();
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
                Point point1 = new Point(x, y);
                Point point2 = new Point(x + 50, y);
                Point point3 = new Point(x + 50, y + 30);
                Point point4 = new Point(x + 35, y + 30);
                Point point5 = new Point(x + 35, y + 32);
                Point point6 = new Point(x + 40, y + 37);
                Point point7 = new Point(x + 40, y + 40);
                Point point8 = new Point(x + 10, y + 40);
                Point point9 = new Point(x + 10, y + 37);
                Point point10 = new Point(x + 15, y + 32);
                Point point11 = new Point(x + 15, y + 30);
                Point point12 = new Point(x, y + 30);
                Point[] curvePoints =
                         {
                 point1,
                 point2,
                 point3,
                 point4,
                 point5,
                 point6,
                 point7,
                 point8,
                 point9,
                 point10,
                 point11,
                 point12
             };
                System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 7);
                System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                if (type.ToString() == "Mac")
                {
                    g.DrawString("MAC", drawFont, drawBrush, x + 12, y + 10);
                }
                else if (type.ToString() == "Windows")
                {
                    g.DrawString("WINS", drawFont, drawBrush, x + 12, y + 10);
                }
                g.DrawRectangle(pen, x + 4, y + 4, 42, 22);
                // Draw polygon to screen.
                g.DrawPolygon(blackPen, curvePoints);

            }

            // if shape needs border to be drawn
            if (Highlight)
            {
                // add in border if shape selected
                // to define point and size
                Point pt = new Point(x - 3, y -3); // to avoid shadow

                int borderSide = 55; // make slightly smaller than shape to avoid shadow
                int borderTop = 45;
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
            pth.AddRectangle(new Rectangle(x-3, y-3, 55, 45));
            bool retval = pth.IsVisible(p);
            pth.Dispose();
            return retval;
        }
    }
}

