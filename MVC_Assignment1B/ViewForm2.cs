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
    public partial class ViewForm2 : Form, ITemView
    {
        private ItemModel myModel;
        AnyItem editItem; // variable for Item to edit
        AnyItem topItem; //  variable for selected Item
        Dictionary<string, string[]> TypeOfItem = new Dictionary<string, string[]>();
        BinaryFormatter binFor;
        int max_X = 425;
        int max_Y = 325;
        int max_width = 200;
        int max_height = 200;
        int i = 0;
        public ViewForm2()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        }
      
        public void RefreshView()
        {
            // clear listBox
            listBox1.Items.Clear();
            // create arrayList from shapes in model
            ArrayList theItemsList = myModel.ItemList;
            //Convert arrayList to array of shapes
            AnyItem[] theItems = (AnyItem[])theItemsList.ToArray(typeof(AnyItem));
            
            // add each shape in the array to the listBox
            foreach (AnyItem sh in theItems)
            {
                listBox1.Items.Add(sh);
            }
        }
        public ItemModel MyModel
        {
            set
            {
                myModel = value;
            }
        }
        private bool InputValid(string txtXpos, string txtYpos)
        {

            int X, Y;
            int Height = 0;
            bool validInput = false;
          

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
            // variables required for anyShape object
            int posRow, posCol;
            AnyItem aItem;
            string type = "";

          
            bool validInput = false;

            try
            {
                
                if (
                    (txtPosCol.Text.ToString() == "") ||
                    (txtPosRow.Text.ToString() == "") || (cmbItemSelect.SelectedIndex == 0) || (cmbTypeSelect.SelectedIndex == 0))

                    // display error message
                    MessageBox.Show
                        ("Please enter fill all the information", "Required Data Missing",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    // fields populated, if valid input create shape
                    validInput = InputValid(txtPosCol.Text.ToString(), txtPosRow.Text.ToString());


                    if (validInput)
                    {
                        posRow = Convert.ToInt32(txtPosRow.Text);
                        posCol = Convert.ToInt32(txtPosCol.Text);
                        type = cmbTypeSelect.Text;

                        // if table selected create table
                        if (cmbItemSelect.Text == "Table")
                        {
                            aItem = new Table("Table", posRow, posCol, type);
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

        private void button2_Click(object sender, EventArgs e)
        {
          
            txtPosCol.Text = "";
            txtPosRow.Text = "";
      
            cmbItemSelect.SelectedIndex = 0;
        }

        private void ViewForm2_Load(object sender, EventArgs e)
        {
            TypeOfItem["Select Item"] = new string[] { "Select Type" };
            TypeOfItem["Table"] = new string[] { "Select Type", "Single", "Double" };
            TypeOfItem["Computer"] = new string[] { "Select Type", "Windows", "Mac" };
            TypeOfItem["Printer"] = new string[] { "Select Type", "Ink", "Laser" };
            foreach (string Item in TypeOfItem.Keys)
                cmbItemSelect.Items.Add(Item);

            cmbItemSelect.SelectedIndex = 0;
            binFor = new BinaryFormatter();
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

        private void button4_Click(object sender, EventArgs e)
        {
            pnlUpdate.Hide();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            

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

        private void btnEdit_Click(object sender, EventArgs e)
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            topItem = null;
            i = 0;
            // create arrayList of shaapes from myModel
            ArrayList theItemList = myModel.ItemList;
            // create array of Items from array list
            AnyItem[] theItems = (AnyItem[])theItemList.ToArray(typeof(AnyItem));
           

            // loop through array checking if mouse is over Item
            foreach (AnyItem s in theItems)
            {
                i++;
                
                // check if mouse is over Item
                if (listBox1.GetItemText(listBox1.SelectedItem).Contains(s.name.ToString()) && listBox1.SelectedIndex+1 == i)
                {
                    // if so make Item topItem

                    topItem = s;
                }
                
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            // check is topItem has a value, if so delete
            if (topItem != null)
            {
                // delete selected Item and redisplay remaining Items
                myModel.DeleteItem(topItem);
            }
            myModel.UpdateViews();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
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
