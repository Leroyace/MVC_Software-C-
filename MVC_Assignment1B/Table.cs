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
    class Table : AnyItem
    {
        // constructor
        public Table(string itemName, int x_at, int y_at,string type)
			: base(itemName, x_at, y_at, type)
		{
        }

        // override method to display item as text
        public override string ToString()
        {
            return "A " + type.ToString() + " Table is placed at " + this.Position();
        }

        // override method to display item as graphics
        public override void Display(Graphics g)
        {
            if (g != null)
            {
                
                // Create pen.
                Pen pen = new Pen(Color.Black, 3);

                // Create array of points that define lines to draw.
                Point[] points =
                         {
                 new Point(x,  y),
                 new Point(x + 25, y + 10),
                 new Point(x + 13, y + 22),
                 new Point(x - 12, y + 10),
                 new Point(x,y)
             };
                // Create points that define line.
                Point point1 = new Point(x - 12, y + 10);
                Point point2 = new Point(x - 12, y + 30);
                Point point3 = new Point(x + 13, y + 22);
                Point point4 = new Point(x + 13, y + 37);
                Point point5 = new Point(x + 25, y + 10);
                Point point6 = new Point(x + 25, y + 30);


                //Draw lines to screen.
                g.DrawLines(pen, points);
                g.DrawLine(pen, point1, point2);
                g.DrawLine(pen, point3, point4);
                g.DrawLine(pen, point5, point6);
                System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 6);
                System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                if (type.ToString() == "Single")
                {
                    g.DrawString("Single", drawFont, drawBrush, x -5, y +4 );
                }
                else if (type.ToString() == "Double")
                {
                    g.DrawString("Double", drawFont, drawBrush, x-5, y+4 );
                }
                
            }

            // if shape needs border to be drawn
            if (Highlight)
            {
                // add in border if shape selected
                // to define point and size
                Point pt = new Point(x - 15, y-2); // to avoid shadow

                int borderSide = 43; // make slightly smaller than shape to avoid shadow
                int borderTop = 42;
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
            pth.AddRectangle(new Rectangle(x - 15, y-3, 43, 42));
            bool retval = pth.IsVisible(p);
            pth.Dispose();
            return retval;
        }
    }
}
