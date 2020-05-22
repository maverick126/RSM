using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using SQSAdmin_WpfCustomControlLibrary.SQSAdminWCFService;
using SQSAdmin_WpfCustomControlLibrary.Common;

namespace SQSAdmin_WpfCustomControlLibrary
{
    /// <summary>
    /// Interaction logic for frmAreaGroupByHomePlan.xaml
    /// </summary>
    public partial class frmAreaGroupByHomePlan : Window
    {
        private SQSAdminServiceClient client;
        private int loginstateid;
        private string usercode;
        private int currentselectedhomeid=0;
        HomeAreaResource hr;
        public frmAreaGroupByHomePlan(int pstate, string pusercode)
        {
            loginstateid = pstate;
            usercode = pusercode;
            hr = new HomeAreaResource(pstate);
                      
            InitializeComponent();
            this.DataContext = hr;
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnsearch_Click(null,null);   

        }
        private void btnsearch_Click(object sender, RoutedEventArgs e)
        {
            hr.LoadHome(int.Parse(cmbState.SelectedValue.ToString()), int.Parse(cmbBrand.SelectedValue.ToString()), txthomename.Text);
            dataGrid2.SelectedIndex = -1;
        }

        private void btnConfigure_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmbBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbBrand.SelectedValue != null)
            {
                btnsearch_Click(null, null);
            }
        }

        private void cmbState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbState.SelectedValue != null)
            {
                hr.LoadBrand(int.Parse(cmbState.SelectedValue.ToString()), "cmbState");
                cmbBrand.SelectedIndex = 0;
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            cmbState.SelectedValue = loginstateid;
            txthomename.Text = "";
            hr.LoadBrand(loginstateid,"cmbState");
            cmbBrand.SelectedIndex = 0;
            btnsearch_Click(null, null);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string selectedareaids = "";
            var itemsSource = dataGridAvailable.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {

                foreach (var item in itemsSource)
                {
                    var row = dataGridAvailable.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = dataGridAvailable.Columns[0].GetCellContent(item) as ContentPresenter;
                    if (contentPresenter != null)
                    {
                        DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                        CheckBox chk = editingTemplate.FindName("chkSelected", contentPresenter) as CheckBox;
                        TextBlock tx = dataGridAvailable.Columns[1].GetCellContent(row) as TextBlock;
                        if ((bool)chk.IsChecked)
                        {
                            if (selectedareaids.Length == 0)
                            {
                                selectedareaids = tx.Text;
                            }
                            else
                            {
                                selectedareaids = selectedareaids + "," + tx.Text;
                            }
                        }
                    }
                }
            }

            if (selectedareaids.Length != 0)
            {
                hr.AddSelectedAreasToHome(currentselectedhomeid, selectedareaids, usercode);
                RefreshAreaLists(currentselectedhomeid);
            }
            else
            {
                MessageBox.Show("Please select at least one area to configure!");
            }

        }

        private void RefreshAreaLists(int homeid)
        {
            hr.LoadAvailableAreasForHome(homeid);
            hr.LoadConfiguredAreasForHome(homeid);
        }
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            string selectedareaids = "";
            var itemsSource = dataGridConfigured.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {

                foreach (var item in itemsSource)
                {
                    var row = dataGridConfigured.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = dataGridConfigured.Columns[0].GetCellContent(item) as ContentPresenter;
                    if (contentPresenter != null)
                    {
                        DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                        CheckBox chk = editingTemplate.FindName("chkSelected2", contentPresenter) as CheckBox;
                        TextBlock tx = dataGridConfigured.Columns[1].GetCellContent(row) as TextBlock;
                        if ((bool)chk.IsChecked)
                        {
                            if (selectedareaids.Length == 0)
                            {
                                selectedareaids = tx.Text;
                            }
                            else
                            {
                                selectedareaids = selectedareaids + "," + tx.Text;
                            }
                        }
                    }
                }
            }

            if (selectedareaids.Length != 0)
            {
                hr.RemoveMinimumAreasToHome(currentselectedhomeid, selectedareaids, usercode);
                RefreshAreaLists(currentselectedhomeid);
            }
            else
            {
                MessageBox.Show("Please select at least one area to reomve!");
            }
        }

        private void dataGrid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HomeAreaResource.Home p = (HomeAreaResource.Home)dataGrid2.SelectedItem;
            if (p != null)
            {
                groupBox2.Header = "Minimum Area Configuration for " + p.HomeName;
                RefreshAreaLists(p.HomeID);
                currentselectedhomeid = p.HomeID;
            }
            else
            {
                groupBox2.Header = "Minimum Area Configuration";
                hr.AvailableAreas.Clear();
                hr.ConfiguredAreas.Clear();
                currentselectedhomeid=0;
            }
        }


        private void cmbState2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbState2.SelectedValue != null)
            {
                hr.LoadBrand(int.Parse(cmbState2.SelectedValue.ToString()), "cmbState2");
                cmbBrand2.SelectedIndex = 0;
            }
        }

        private void cmbState3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbState3.SelectedValue != null)
            {
                hr.LoadBrand(int.Parse(cmbState3.SelectedValue.ToString()), "cmbState3");
                cmbBrand3.SelectedIndex = 0;
            }
        }

        private void cmbBrand2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbBrand2.SelectedValue != null)
            {
                btnsearch2_Click(null, null);
            }
        }

        private void cmbBrand3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbBrand3.SelectedValue != null)
            {
                btnsearch3_Click(null, null);
            }
        }

        private void btnsearch2_Click(object sender, RoutedEventArgs e)
        {
            hr.LoadSourceHome(int.Parse(cmbBrand2.SelectedValue.ToString()), txthomename2.Text);
        }

        private void btnReset2_Click(object sender, RoutedEventArgs e)
        {
            cmbState2.SelectedValue = loginstateid;
            txthomename2.Text = "";
            hr.LoadBrand(loginstateid, "cmbState2");
            cmbBrand2.SelectedIndex = 0;
            btnsearch2_Click(null, null);
        }

        private void btnsearch3_Click(object sender, RoutedEventArgs e)
        {
            hr.LoadTargetHome(int.Parse(cmbBrand3.SelectedValue.ToString()), txthomename3.Text);
        }
        public bool ContainsUnicodeCharacter(string input)
        {
            const int MaxAnsiCode = 255;

            return input.ToCharArray().Any(c => c > MaxAnsiCode);
        }
        private void btnReset3_Click(object sender, RoutedEventArgs e)
        {
            cmbState3.SelectedValue = loginstateid;
            txthomename3.Text = "";
            hr.LoadBrand(loginstateid, "cmbState3");
            cmbBrand3.SelectedIndex = 0;
            btnsearch3_Click(null, null);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string sourcehomeid="";
            string targethomeidstring = "";

            var itemsSource = dataGridSourceHome.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {

                foreach (var item in itemsSource)
                {
                    var row = dataGridSourceHome.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = dataGridSourceHome.Columns[0].GetCellContent(item) as ContentPresenter;
                    if (contentPresenter != null)
                    {
                        DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                        CheckBox chk = editingTemplate.FindName("chkSelected3", contentPresenter) as CheckBox;
                        TextBlock tx = dataGridSourceHome.Columns[1].GetCellContent(row) as TextBlock;
                        if ((bool)chk.IsChecked)
                        {
                            sourcehomeid = tx.Text;
                            break;
                        }
                    }
                }
            }

            var itemsSource2 = dataGridTargetHome.ItemsSource as IEnumerable;
            if (itemsSource2 != null)
            {

                foreach (var item in itemsSource2)
                {
                    var row = dataGridTargetHome.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = dataGridTargetHome.Columns[0].GetCellContent(item) as ContentPresenter;
                    if (contentPresenter != null)
                    {
                        DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                        CheckBox chk = editingTemplate.FindName("chkSelected4", contentPresenter) as CheckBox;
                        TextBlock tx = dataGridTargetHome.Columns[1].GetCellContent(row) as TextBlock;
                        if ((bool)chk.IsChecked)
                        {
                            if (targethomeidstring == "")
                            {
                                targethomeidstring = tx.Text;
                            }
                            else
                            {
                                targethomeidstring = targethomeidstring + "," + tx.Text;
                            }
                        }
                    }
                }
            }
            if (sourcehomeid == "")
            {
                MessageBox.Show("Please select source home");
            }
            else if (targethomeidstring == "")
            {
                MessageBox.Show("Please select at least one target home");
            }
            else
            {
                hr.CopyMinimumAreasFromSourceToTargetHomes(sourcehomeid, targethomeidstring, usercode);
                txthomename2.Text = "";
                hr.LoadSourceHome(int.Parse(cmbBrand2.SelectedValue.ToString()), "");
                txthomename3.Text = "";
                hr.LoadTargetHome(int.Parse(cmbBrand3.SelectedValue.ToString()), "");
            }
        }



        private void chkSelected3_Checked(object sender, RoutedEventArgs e)
        {
            var itemsSource = dataGridSourceHome.ItemsSource as IEnumerable;
            HomeAreaResource.Home home = (HomeAreaResource.Home)(((CheckBox)e.OriginalSource).DataContext);
            if (itemsSource != null)
            {

                foreach (var item in itemsSource)
                {
                    var row = dataGridSourceHome.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = dataGridSourceHome.Columns[0].GetCellContent(item) as ContentPresenter;
                    if (contentPresenter != null)
                    {
                        DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                        CheckBox chk = editingTemplate.FindName("chkSelected3", contentPresenter) as CheckBox;
                        TextBlock tx = dataGridConfigured.Columns[1].GetCellContent(row) as TextBlock;
                        if ( home.HomeID.ToString()!=tx.Text && (bool)chk.IsChecked)
                        {
                            chk.IsChecked = false;
                        }
                    }
                }
            }
        }


    }
}
