using System.Collections;
using System.Windows.Forms;

namespace SQSAdmin
{
  /// <summary>
  /// created by:    Frank Zhao
  /// created date:  31/05/2007
  /// functionality: this class was used to add sort function for a listview of windows form.
  /// how to use:
  ///         step1. set listview "sorting" property to asending or desending
  ///         step2: add evnt handler to listview "Columnclick" e.g. listView1_ColumnClick
  ///         step3: create procedure listView1_ColumnClick to call this class 
  ///                detail see listView1_ColumnClick() of frmPrice.cs
  /// </summary>
    class ListViewItemComparer: IComparer 
    {
        private int col;
        private SortOrder order;
        public ListViewItemComparer()
        {
            col = 0;
            order = SortOrder.Ascending;
        }
        public ListViewItemComparer(int column,SortOrder order)
        {
            col = column;
            this.order = order;
        }
        public int Compare(object x, object y)
        {
            int returnVal= -1;
            returnVal = string.Compare(((ListViewItem)x).SubItems[col].Text,
                                    ((ListViewItem)y).SubItems[col].Text);
            // Determine whether the sort order is descending.

            if (order == SortOrder.Descending)
                // Invert the value returned by String.Compare.
                returnVal *= -1;

            return returnVal;

        }

    }
}
