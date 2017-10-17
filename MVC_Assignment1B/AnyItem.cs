using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Assignment1B
{   
    [Serializable()]
    public abstract class AnyItem
    {
        protected string ItemName;
        protected int x;
        protected int y;
        protected string Type;
        // added for hittest and highlight
        bool highlight;

        // constructor
        public AnyItem(string item, int x_at, int y_at, string type)
        {
            ItemName = item;
            x = x_at;
            y = y_at;
            Type = type;
        }

        public abstract void Display(Graphics g); // abstract method

        public bool Highlight
        {
            get
            {
                return highlight;
            }
            set
            {
                highlight = value;
            }
        }

        public string Position()  //non abstract method
        {
            return "(" + x.ToString() + "," + y.ToString() + ")";
        }

        public abstract int x_pos //abstract property
        {
            get;
            set;
        }

        public abstract int y_pos //abstract property
        {
            get;
            set;
        }
        public abstract string type //abstract property
        {
            get;
            set;
        }

        public abstract string name //abstract property
        {
            get;
            set;
        }

        // virtual method
        public virtual bool HitTest(Point p)
        {
            Point pt = new Point(x, y);
            Size size = new Size(100, 100);
            //default behaviour
            return new Rectangle(pt, size).Contains(p);
        }
    }
}
