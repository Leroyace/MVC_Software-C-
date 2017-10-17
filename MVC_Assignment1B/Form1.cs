using System;
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
    public partial class Form1 : Form
    {
        private ItemModel theModel;
        private ItemController theController;
        private ViewForm1 myViewForm1;
        private ViewForm2 myViewForm2;
        private ViewForm3 myViewForm3;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            theController = new ItemController();
        }
        private void completeSetUp()
        {
            // make controller
            theController = new ItemController();
            // make model
            theModel = new ItemModel(theController);
            // make views
            myViewForm1 = new ViewForm1();
            myViewForm2 = new ViewForm2();
            myViewForm3 = new ViewForm3();
            myViewForm1.MyModel = theModel;
            myViewForm2.MyModel = theModel;
            myViewForm3.MyModel = theModel;

            theController.AddView(myViewForm1);
            theController.AddView(myViewForm2);
            theController.AddView(myViewForm3);

            //show views
            myViewForm3.Show();
            myViewForm2.Show();
            myViewForm1.Show();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.completeSetUp();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            theModel = new ItemModel(theController);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            myViewForm1 = new ViewForm1();
            myViewForm2 = new ViewForm2();
            myViewForm3 = new ViewForm3();
            myViewForm1.MyModel = theModel;
            myViewForm2.MyModel = theModel;
            myViewForm3.MyModel = theModel;
            theController.AddView(myViewForm1);
            theController.AddView(myViewForm2);
            theController.AddView(myViewForm3);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            myViewForm1.Show();
            myViewForm2.Show();
            myViewForm3.Show();
        }
    }
}
