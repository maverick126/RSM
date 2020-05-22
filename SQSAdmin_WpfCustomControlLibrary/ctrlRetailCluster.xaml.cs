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
    /// Interaction logic for ctrlRetailCluster.xaml
    /// </summary>
    public partial class ctrlRetailCluster : UserControl
    {
        private  RetailClusterSource rs;
        private int loginstateid;
        private string loginuser;
        private int currentselectedclusterid = 0;
        private string currentselectedclustername="";
        private string selectedgroupidstring="";
        private string removedgroupidstring = "";
        public ctrlRetailCluster(int pstateid, string pusercode)
        {
            loginstateid = pstateid;
            loginuser = pusercode;
            rs = new RetailClusterSource(loginstateid, pusercode);
            InitializeComponent();
            this.DataContext = rs;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            cmbState.SelectedValue = loginstateid;
            txtretailclustername.Text = "";
        }

        private void btnResetAvailable_Click(object sender, RoutedEventArgs e)
        {
            txtAvailableGroupName.Text = "";
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            rs.LoadExistingRetailCluster(int.Parse(cmbState.SelectedValue.ToString()), txtretailclustername.Text);
        }

        private void btnSearchAvailable_Click(object sender, RoutedEventArgs e)
        {
            rs.LoadAvailableGroups(currentselectedclusterid,txtAvailableGroupName.Text);
        }

        private void btnSaveSelectedGroup_Click(object sender, RoutedEventArgs e)
        {
            selectedgroupidstring = "";
            if (currentselectedclusterid == 0)
            {
                MessageBox.Show("Please select a cluster from left grid.");
            }
            else
            {
                var itemsSource = dataGrid3.ItemsSource as IEnumerable;
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
                                if (selectedgroupidstring.Length == 0)
                                {
                                    selectedgroupidstring = tx.Text;
                                }
                                else
                                {
                                    selectedgroupidstring = selectedgroupidstring + "," + tx.Text;
                                }
                            }
                        }
                    }
                    if (selectedgroupidstring != "")
                    {
                        SaveData(currentselectedclusterid, selectedgroupidstring, loginuser);
                    }
                    else
                    {
                        MessageBox.Show("Please select at least one group.");
                    }
                }
            }
        }

        private void SaveData(int clusterid, string selectedgroupid, string usercode)
        {
            rs.SaveSelectedGroupToRetailCluster(clusterid, selectedgroupid, usercode);
            btnSearchAvailable_Click(null, null);
            rs.LoadExistingGroups(clusterid);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Windows.Controls.DataGridRow row = (Microsoft.Windows.Controls.DataGridRow)(dataGrid1.ItemContainerGenerator.ContainerFromItem(dataGrid1.SelectedItem));
            RetailClusterSource.RetailCluster s = (RetailClusterSource.RetailCluster)row.Item;
            bool exists = false;

            if (s.RetailClusterName != null && s.RetailClusterName.Trim() != "")
            {
                if (s.RetailClusterID == 0)
                {
                    if (!RetailClusterExists(s))
                    {
                        exists = false;
                    }
                    else
                    {
                        exists = true;
                    }
                }

                if (exists)
                {
                    MessageBox.Show("This cluster already exists.");
                }
                else
                {
                    try
                    {
                        rs.SaveRetailCluster(s);
                        if (row.DetailsVisibility == Visibility.Visible)
                        {
                            row.DetailsVisibility = Visibility.Collapsed;
                        }
                        btnSearch_Click(null, null);
                    }
                    catch (Exception ex)
                    {
                    }

                }
            }
            else
            {
                MessageBox.Show("Please enter a cluster name.");
            }
        }

        private bool RetailClusterExists(RetailClusterSource.RetailCluster s)
        {
            return rs.RetailClusterExists(s.StateID, s.RetailClusterName);
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var itemsSource = dataGrid2.ItemsSource as IEnumerable;
            removedgroupidstring = "";
            if (itemsSource != null)
            {
                foreach (var item in itemsSource)
                {
                    var row = dataGrid2.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;

                    ContentPresenter contentPresenter = dataGrid2.Columns[0].GetCellContent(item) as ContentPresenter;
                    if (contentPresenter != null)
                    {
                        DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                        CheckBox chk = editingTemplate.FindName("chkSelected3", contentPresenter) as CheckBox;
                        TextBlock tx = dataGrid2.Columns[1].GetCellContent(row) as TextBlock;
                        if ((bool)chk.IsChecked)
                        {
                            if (removedgroupidstring.Length == 0)
                            {
                                removedgroupidstring = tx.Text;
                            }
                            else
                            {
                                removedgroupidstring = removedgroupidstring + "," + tx.Text;
                            }
                        }
                    }
                }
                if (removedgroupidstring != "")
                {
                    RemoveGroup(currentselectedclusterid, removedgroupidstring);
                }
                else
                {
                    MessageBox.Show("Please select at least one group to remove.");
                }
            }
        }

        private void RemoveGroup(int clusterid, string selectedgroupid)
        {
            rs.RemoveSelectedGroupFromRetailCluster(clusterid, selectedgroupid);
            btnSearchAvailable_Click(null, null);
            rs.LoadExistingGroups(clusterid);
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            RetailClusterSource.RetailCluster s = new RetailClusterSource.RetailCluster();
            rs.ExistingRetailClusters.Add(s);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RetailClusterSource.RetailCluster p = (RetailClusterSource.RetailCluster)dataGrid1.SelectedItem;
            if (p != null && p.RetailClusterID != 0)
            {
                rs.SelectedRetailClusterID = p.RetailClusterID;
                btnRemove.IsEnabled = true;
                btnSaveSelectedGroup.IsEnabled = true;
                currentselectedclusterid = p.RetailClusterID;
                currentselectedclustername = p.RetailClusterName;
                rs.LoadExistingGroups(currentselectedclusterid);
                btnSearchAvailable_Click(null, null);
                groupBox3.Header = "Available Groups for Cluster " + currentselectedclustername;
                gboxupgrade.Header = "Configured Groups of Cluster " + currentselectedclustername;
            }
            else
            {
                groupBox3.Header = "Available Groups";
                gboxupgrade.Header = "Configured Groups";
                btnRemove.IsEnabled = false;
                btnSaveSelectedGroup.IsEnabled = false;
                currentselectedclusterid = 0;
                rs.LoadExistingGroups(currentselectedclusterid);
                btnSearchAvailable_Click(null, null);
            }
            //gboxupgrade.Header = "Upgrade Option of " + p.ProductID;
            //txtSaveAlert.Text = "Configure selected products as the upgrade option of proudct " + p.ProductID;
            //sr.GetUpgradeForStandardInclusion(p.ProductID, int.Parse(cmbBrand.SelectedValue.ToString()));
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Windows.Controls.DataGridRow row = (Microsoft.Windows.Controls.DataGridRow)(dataGrid1.ItemContainerGenerator.ContainerFromItem(dataGrid1.SelectedItem));

                // change the details visibility
                if (row.DetailsVisibility == Visibility.Collapsed)
                {
                    row.DetailsVisibility = Visibility.Visible;
                }
                else
                {
                    row.DetailsVisibility = Visibility.Collapsed;
                }
                btnSearch_Click(null, null);

            }
            catch (System.Exception)
            {
            }

        }
    
    }
}
