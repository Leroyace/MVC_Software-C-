using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVC_Assignment1B
{
    public partial class ViewForm1 : Form, ITemView
    {   
        private ItemModel myModel;

        bool dragging;
        AnyItem topItem; //  variable for selected Item
        AnyItem editItem; // variable for Item to edit
        BinaryFormatter binFor;
        // variables for mouse position
        Point lastPosition = new Point(0, 0);
        Point currentPosition = new Point(0, 0);

        Dictionary<string, string[]> TypeOfItem = new Dictionary<string, string[]>();

        int max_X = 425;
        int max_Y = 325;
        int max_width = 200;
        int max_height = 200;

        public ViewForm1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        }
        private void clearPanel()
        {
            panel1.CreateGraphics().Clear(panel1.BackColor);
        }
        public void RefreshView()
        {
            // clear panel
            clearPanel();
            // create arraylist of Items from model and convery
            // to array of Items
            ArrayList theItemList = myModel.ItemList;
            AnyItem[] theItems = (AnyItem[])theItemList.ToArray(typeof(AnyItem));
            // graphics object to draw Items
            Graphics g = this.panel1.CreateGraphics();
            // draw all Items in array
            foreach (AnyItem sh in theItems)
            {
                sh.Display(g);
            }
        }
        public ItemModel MyModel
        {
            set
            {
                myModel = value;
            }
        }

        private void cmbItemSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbTypeSelect.Items.Clear();

            if (cmbItemSelect.SelectedIndex > -1)
            {
                string type = TypeOfItem.Keys.ElementAt(cmbItemSelect.SelectedIndex);
                cmbTypeSelect.Items.AddRange(TypeOfItem[type]);
                cmbTypeSelect.SelectedIndex = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            txtPosCol.Text = "";
            txtPosRow.Text = "";
        
            cmbItemSelect.SelectedIndex = 0;
        }

        private void ViewForm1_Load(object sender, EventArgs e)
        {
            TypeOfItem["Select Item"] = new string[] { "Select Type"};
            TypeOfItem["Table"] = new string[] { "Select Type", "Single", "Double"};
            TypeOfItem["Computer"] = new string[] { "Select Type", "Windows", "Mac" };
            TypeOfItem["Printer"] = new string[] { "Select Type", "Ink", "Laser" };
            foreach (string Item in TypeOfItem.Keys)
                cmbItemSelect.Items.Add(Item);
            
            cmbItemSelect.SelectedIndex = 0;
            binFor = new BinaryFormatter();
        }
        private bool InputValid(string txtXpos, string txtYpos)
        {

            int X, Y, Width;
            int Height = 0;
            bool validInput = false;
            // required fields populated 
            

            // convert user inputs to variables				
            X = Convert.ToInt32(txtXpos);
            Y = Convert.ToInt32(txtYpos);
        

            // validate data entry is within limits
            if ((X > max_X) || (Y > max_Y) ||
                (Height > max_height))
                MessageBox.Show("Maximum value for X is " + max_X
                    + "\r\n" + "Maximum value for Y is " + max_Y + "\r\n"
                    + "\r\n" + "Maximum value for Width is " + max_width
                    + "\r\n" + "Maximum value for Height is " + max_height,
                    "Please Check the Values Entered",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                validInput = true;
            return validInput;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // variables required for anyItem object
            int row, col, posRow,posCol;
            AnyItem aItem;
            string type = "";

            bool tableRequired = true; // variable to aid validation of txtHeight field
            bool validInput = false;

            try
            {
                // first work if height is required based on Item selected
                //if (rbCircle.Checked || rbSquare.Checked)
                   // tableRequired = false;

                // if heightRequired check all fields populated
               // if (heightRequired)
               // {
                    if (
                        (txtPosCol.Text.ToString() == "") ||
                        (txtPosRow.Text.ToString() == "") || (cmbItemSelect.SelectedIndex == 0) || (cmbTypeSelect.SelectedIndex == 0))

                        // display error message
                        MessageBox.Show
                            ("Please enter fill all the information", "Required Data Missing",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        // fields populated, if valid input create Item
                        validInput = InputValid(txtPosCol.Text.ToString(), txtPosRow.Text.ToString());


                        if (validInput)
                        {
                            posRow = Convert.ToInt32(txtPosRow.Text);
                            posCol = Convert.ToInt32(txtPosCol.Text);
                            type = cmbTypeSelect.Text;

                            // if table selected create table
                            if (cmbItemSelect.Text == "Table")
                            {
                                aItem = new Table("Table", posRow, posCol,type );
                                myModel.AddItem(aItem);
                            }

                            // if computer selected create computer
                            else if (cmbItemSelect.Text == "Computer")
                            {
                                aItem = new Computer("Computer", posRow, posCol, type);
                                myModel.AddItem(aItem);
                            }

                            // if printer selected create printer
                            else if (cmbItemSelect.Text == "Printer")
                            {
                                aItem = new Printer("Printer", posRow, posCol, type);
                                myModel.AddItem(aItem);
                            }
                            
                        }
                    }
                }

                

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + "\r\n" + ex.ToString(),
                    "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (topItem != null)
                dragging = true;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            // set last position to current position
            lastPosition = currentPosition;
            // set current position to mouse position
            currentPosition = new Point(e.X, e.Y);
            // calculate how far mouse has moved 
            int xMove = currentPosition.X - lastPosition.X;
            int yMove = currentPosition.Y - lastPosition.Y;

            if (!dragging) // mouse not down 
            {
                // reset variables 
                topItem = null;
                bool needsDisplay = false;

                // create arrayList of shaapes from myModel
                ArrayList theItemList = myModel.ItemList;
                // create array of Items from array list
                AnyItem[] theItems = (AnyItem[])theItemList.ToArray(typeof(AnyItem));
                // graphics object to draw Items when required
                Graphics g = this.panel1.CreateGraphics();

                // loop through array checking if mouse is over Item
                foreach (AnyItem s in theItems)
                {
                    // check if mouse is over Item
                    if (s.HitTest(new Point(e.X, e.Y)))
                    {
                        // if so make Item topItem
                        topItem = s; 
                    }

                    if (s.Highlight == true)
                    {
                        // Item to be redrawn
                        needsDisplay = true;
                        // redraw Item
                        s.Display(g);
                        s.Highlight = false;
                    }
                    // 30 Oct moved this piece up to before highlight test
                    //					if (s.HitTest(new Point(e.X, e.Y)))// check if mouse is over Item
                    //					{
                    //						topItem = s; // make Item topItem
                    //					}
                }

                if (topItem != null) // if there is a topItem
                {
                    needsDisplay = true; // need to redisplay
                    topItem.Display(g); // redisplay topItem					
                    topItem.Highlight = true;
                }

                if (needsDisplay)
                {
                    // redisplay model
                    myModel.UpdateViews();
                }
            }
            else // mouse is down
            {
                // reset position of selected Item by value of mouse move 
                topItem.x_pos = topItem.x_pos + xMove;
                topItem.y_pos = topItem.y_pos + yMove;

                myModel.UpdateViews();
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
            // clear drawOn panel
            clearPanel();
            // create arrayList of Items from model and convert to array of Items
            ArrayList theItemList = myModel.ItemList;
            AnyItem[] theItems = (AnyItem[])theItemList.ToArray(typeof(AnyItem));
            // graphics object to draw selected Item
            Graphics g = this.panel1.CreateGraphics();

            // check if Item selected and if so display
            if (topItem != null)
            {
                theItems[0] = topItem;
                topItem.Display(g);
            }
            myModel.UpdateViews();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (topItem != null)
            {
                // message to user to enter new size and colour
                MessageBox.Show("You May Update Item type Only" + "\r\n",
                    "Select a new type and click the Update button",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // make editItem the current topItem 
                editItem = topItem;

                // update values in update panel to selected Item
                lblSelectedItem.Text =
                    topItem.name.ToString();
                
                cmbUpdateType.Items.Clear();
                cmbUpdateType.Items.AddRange(TypeOfItem[topItem.name.ToString()]);
                cmbUpdateType.SelectedIndex = 0;

                  
                // show update panel
                pnlUpdate.Show();
              
            }
            myModel.UpdateViews();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // check is topItem has a value, if so delete
            if (topItem != null)
            {
                // clear panel
                clearPanel();
                // delete selected Item and redisplay remaining Items
                myModel.DeleteItem(topItem);
            }
            myModel.UpdateViews();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pnlUpdate.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // clear draw on panel
            clearPanel();

            // local variables for updates to selected item
         
            string Type;

            // get updated attributes for selected item						
            Type = cmbUpdateType.Text;
            
                // update shape attributes
                editItem.type = Type;
               
                // redisplay
                myModel.UpdateViews();
                pnlUpdate.Hide();
            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileInfo finfo = new FileInfo(saveFileDialog1.FileName);
                Stream strm;
                strm = finfo.Open(FileMode.Create, FileAccess.ReadWrite);
                ArrayList theItemList = myModel.ItemList;
                AnyItem[] theItems = (AnyItem[])theItemList.ToArray(typeof(AnyItem));
                foreach (AnyItem sh in theItems)
                {
                    binFor.Serialize(strm, sh);
                }
                strm.Close();
            }
        }
    }
}
