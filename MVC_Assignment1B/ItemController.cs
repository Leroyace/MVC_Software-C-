using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Assignment1B
{
    public interface ITemView
    {
        void RefreshView();
    }
    public class ItemController
    {
        private ArrayList ViewList;

        // constructor
        public ItemController()
        {
            ViewList = new ArrayList();
        }

        /// <summary>method: AddView
        /// add view to viewlist
        /// </summary>
        /// <param name="aView"></param>
        public void AddView(ITemView aView)
        {
            ViewList.Add(aView);
        }

        /// <summary>method: UpdateViews
        /// update each view 
        /// </summary>
        public void UpdateViews()
        {
            ITemView[] theViews = (ITemView[])ViewList.ToArray(typeof(ITemView));
            foreach (ITemView v in theViews)
            {
                v.RefreshView();
            }
        }
    }
}
