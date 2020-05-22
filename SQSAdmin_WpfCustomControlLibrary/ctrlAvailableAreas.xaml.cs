using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using SQSAdmin_WpfCustomControlLibrary.SQSAdminWCFService;
using SQSAdmin_WpfCustomControlLibrary.Common;
using System.ComponentModel;
using System.Xml;

namespace SQSAdmin_WpfCustomControlLibrary
{
    /// <summary>
    /// Interaction logic for ctrlAvailableAreas.xaml
    /// </summary>
    public partial class ctrlAvailableAreas : Window
    {
        private RelatedAreasSource rs;
        public int stateid = 0;
        public string loginuser = "";
        public string currentselectedproductid="";
        public string callfrom = "";
        public string selectedareaidstring = "";
        public ctrlAvailableAreas(int pstateid, string pusercode, string pproductid, string pcallfrom)
        {
            loginuser = pusercode;
            stateid = pstateid;
            callfrom = pcallfrom;
            currentselectedproductid = pproductid;
            rs = new RelatedAreasSource(stateid, loginuser);
            InitializeComponent();
            this.DataContext = rs;
            rs.LoadAvailableAreasForProduct(currentselectedproductid, txtAvailableAreaName.Text, callfrom);
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
        }

        private void btnResetAvailable_Click(object sender, RoutedEventArgs e)
        {
            txtAvailableAreaName.Text = "";
            rs.LoadAvailableAreasForProduct(currentselectedproductid, txtAvailableAreaName.Text.Trim(), callfrom);
        }

        private void btnSearchAvailable_Click(object sender, RoutedEventArgs e)
        {
            rs.LoadAvailableAreasForProduct(currentselectedproductid, txtAvailableAreaName.Text.Trim(), callfrom);
        }

        private void btnAddArea_Click(object sender, RoutedEventArgs e)
        {
            var itemsSource = dataGrid3.ItemsSource as IEnumerable;
            selectedareaidstring = "";
            if (itemsSource != null)
            {
                foreach (var item in itemsSource)
                {
                    var row = dataGrid3.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;

                    ContentPresenter contentPresenter = dataGrid3.Columns[0].GetCellContent(item) as ContentPresenter;
                    if (contentPresenter != null)
                    {
                        DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                        CheckBox chk = editingTemplate.FindName("chkSelected2", contentPresenter) as CheckBox;
                        TextBlock tx = dataGrid3.Columns[1].GetCellContent(row) as TextBlock;
                        if ((bool)chk.IsChecked)
                        {
                            if (selectedareaidstring.Length == 0)
                            {
                                selectedareaidstring = tx.Text;
                            }
                            else
                            {
                                selectedareaidstring = selectedareaidstring + "," + tx.Text;
                            }
                        }
                    }
                }
                if (selectedareaidstring != "")
                {
                    SaveArea(currentselectedproductid, selectedareaidstring);
                }
                else
                {
                    MessageBox.Show("Please select area to configure!");
                }
            }
        }

        private void SaveArea(string productid, string selectedareaid)
        {
            rs.SaveRelatedAreaToProduct(productid, selectedareaid, loginuser, callfrom);
            rs.LoadAvailableAreasForProduct(currentselectedproductid, txtAvailableAreaName.Text, callfrom);
            var parentwin = this.Owner as ctrlRelatedAreas;
            parentwin.RefreshGrids();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
