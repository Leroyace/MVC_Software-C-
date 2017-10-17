using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVC_Assignment1B
{
    public partial class ViewForm3 : Form, ITemView
    {
        private ItemModel myModel;
       

        public ViewForm3()
        {
            InitializeComponent();
        }
        private void clearPanel()
        {
            panel1.CreateGraphics().Clear(panel1.BackColor);
        }
        public void RefreshView()
        {
            // clear panel
            clearPanel();
            // create arraylist of shapes from model and convery
            // to array of shapes
            ArrayList theItemList = myModel.ItemList;
            AnyItem[] theItems = (AnyItem[])theItemList.ToArray(typeof(AnyItem));
            // graphics object to draw shapes
            Graphics g = this.panel1.CreateGraphics();
            // draw all shapes in array
            foreach (AnyItem sh in theItems)
            {
                sh.Display(g);
            }
        }
        public void DisplayPrinters()
        {
            // clear panel
            clearPanel();
            // create arraylist of shapes from model and convery
            // to array of shapes
            ArrayList theItemList = myModel.ItemList;
            AnyItem[] theItems = (AnyItem[])theItemList.ToArray(typeof(AnyItem));
            // graphics object to draw shapes
            Graphics g = this.panel1.CreateGraphics();
            // draw all shapes in array
            foreach (AnyItem sh in theItems)
            {
                // only draw circles & squares
                if (sh.name.Equals("Printer"))
                    sh.Display(g);
            }
        }
        public void DisplayComputers()
        {
            // clear panel
            clearPanel();
            // create arraylist of shapes from model and convery
            // to array of shapes
            ArrayList theItemList = myModel.ItemList;
            AnyItem[] theItems = (AnyItem[])theItemList.ToArray(typeof(AnyItem));
            // graphics object to draw shapes
            Graphics g = this.panel1.CreateGraphics();
            // draw all shapes in array
            foreach (AnyItem sh in theItems)
            {
                // only draw circles & squares
                if (sh.name.Equals("Computer"))
                    sh.Display(g);
            }
        }
        public void DisplayTables()
        {
            // clear panel
            clearPanel();
            // create arraylist of shapes from model and convery
            // to array of shapes
            ArrayList theItemList = myModel.ItemList;
            AnyItem[] theItems = (AnyItem[])theItemList.ToArray(typeof(AnyItem));
            // graphics object to draw shapes
            Graphics g = this.panel1.CreateGraphics();
            // draw all shapes in array
            foreach (AnyItem sh in theItems)
            {
                // only draw circles & squares
                if (sh.name.Equals("Table"))
                    sh.Display(g);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilter.Text == "All Items")
                RefreshView();
            else if (cmbFilter.Text == "Tables only")
                DisplayTables();
            else if (cmbFilter.Text == "Computers only")
                DisplayComputers();
            else if (cmbFilter.Text == "Printers only")
                DisplayPrinters();
        }

        private void ViewForm3_Load(object sender, EventArgs e)
        {
            cmbFilter.SelectedIndex = 0;
        }
        public ItemModel MyModel
        {
            set
            {
                myModel = value;
            }
        }
    }
}
