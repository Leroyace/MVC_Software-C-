using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Assignment1B
{
    public class ItemModel
    {
        private ArrayList itemList;
        private ItemController theController;

        public ItemModel(ItemController aController)
        {
            itemList = new ArrayList();
            theController = aController;
        }

        public ArrayList ItemList
        {
            get
            {
                return itemList;
            }
        }

        
        public void AddItem(AnyItem aItem)
        {
            itemList.Add(aItem);
            UpdateViews();
        }

      
        public void UpdateItem(AnyItem aItem)
        {
            UpdateViews();
        }

       
        public void DeleteItem(AnyItem aItem)
        {
            itemList.Remove(aItem);
            UpdateViews();
        }

       
        public void SendToBack(AnyItem aItem)
        {
            // first shape drawn is at the back			
            // temp arrayList to resort shapes so selected shape is drawn first
            ArrayList sortList = new ArrayList();
            // find index of shape to be drawn first
            int max = itemList.IndexOf(aItem);
            // first shape i.e. shape to send to back
            sortList.Add(aItem);
            // copy to sortList in correct sequence
            for (int i = 0; i < max; i++)
            {
                sortList.Add(itemList[i]);
            }

            // copy sortList back to shapeList
            for (int i = 0; i < sortList.Count; i++)
            {
                itemList[i] = sortList[i];
            }
            UpdateViews();
        }

     
        public void BringToFront(AnyItem aItem)
        {
            // last shape drawn is at the front			
            // temp arrayList to resort shapes so selected shape is drawn last
            ArrayList sortList = new ArrayList(itemList);
            // find index of shape to be drawn last
            int max = itemList.IndexOf(aItem);
            // find length of shapeList array
            int length = itemList.Count;
            // copy shapeList to sortList excluding selected shape
            for (int i = max + 1; i < length; i++)
            {
                sortList[i - 1] = itemList[i];
            }
            // last shape i.e. shape to bring to front
            sortList[length - 1] = itemList[max];
            // copy sortList back to shapeList
            for (int i = 0; i < sortList.Count; i++)
            {
                itemList[i] = sortList[i];
            }
            UpdateViews();
        }

      
        public void UpdateViews()
        {
            theController.UpdateViews();
        }
    }
}
