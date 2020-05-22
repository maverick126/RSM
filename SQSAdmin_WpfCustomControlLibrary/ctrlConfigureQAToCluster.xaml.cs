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
    /// Interaction logic for ctrlConfigureQAToCluster.xaml
    /// </summary>
    public partial class ctrlConfigureQAToCluster : UserControl
    {
        private RetailClusterSource rs;
        private int loginstateid;
        private string loginuser;
        private bool currentquestionmandatory = false;
        private string configType = "";
        public ctrlConfigureQAToCluster(int pstateid, string pusercode, string configuretype)
        {
            loginstateid = pstateid;
            loginuser = pusercode;
            configType = configuretype;
            rs = new RetailClusterSource(loginstateid, pusercode);
            InitializeComponent();
            this.DataContext = rs;
            rs.LoadSuppliers(loginstateid);
            rs.LoadQuestions(loginstateid);

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            cmbState.SelectedValue = loginstateid;
            txtretailclustername.Text = "";
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            rs.LoadExistingRetailCluster(int.Parse(cmbState.SelectedValue.ToString()), txtretailclustername.Text);
        }
        private void cmbSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //int supplierid = 0;
            ////if ((bool)chkFilter.IsChecked)
            //ComboBox cmbSupplier = (ComboBox)sender;
            //supplierid = int.Parse(cmbSupplier.SelectedValue.ToString());

            //sr.GetProductImage(productID, supplierid);
        }

        private void cmbQuestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ComboBox cmbQuestion = (ComboBox)sender;
            //rs.LoadAnswerForQuestions(int.Parse(cmbQuestion.SelectedValue.ToString()));

            //Grid gr = (Grid)cmbQuestion.Parent;
            //ComboBox cmbAnswer = (ComboBox)gr.FindName("cmbAnswer");
   
            //cmbAnswer.SelectedIndex = 0;
            //currentquestionmandatory = rs.StudioMQuestion[cmbQuestion.SelectedIndex].Mandatory;


        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Button btnadd = (Button)sender;
            //Grid gr = (Grid)btnadd.Parent;

            //ComboBox cmbQuestion = (ComboBox)gr.FindName("cmbQuestion");
            ////rs.LoadAnswerForQuestions(int.Parse(cmbQuestion.SelectedValue.ToString()));
            //ComboBox cmbAnswer = (ComboBox)gr.FindName("cmbAnswer");
            //ComboBox cmbSupplier = (ComboBox)gr.FindName("cmbSupplier");

            //if ((cmbAnswer.Text.ToUpper().Contains("SINGLE SELECTION") || cmbAnswer.Text.ToUpper().Contains("MULTIPLE SELECTION")))
            //{
            //    if (cmbAnswer.SelectedValue == null)
            //    {
            //        MessageBox.Show("Please select an answer for the question.");
            //    }
            //    return;
            //}

            //StudioMResource.QandAForSupplier qa = new StudioMResource.QandAForSupplier();
            //qa.SupplierID = int.Parse(cmbSupplier.SelectedValue.ToString());
            //qa.SupplierName = cmbSupplier.Text;
            //qa.QuestionID = int.Parse(cmbQuestion.SelectedValue.ToString());
            //qa.QuestionText = cmbQuestion.Text;
            //qa.Mandatory = currentquestionmandatory;
            //if (cmbAnswer.SelectedValue != null)
            //{
            //    qa.AnswerID = int.Parse(cmbAnswer.SelectedValue.ToString());
            //    qa.AnswerText = cmbAnswer.Text;
            //}
            //else
            //{
            //    qa.AnswerID = 0;
            //    qa.AnswerText = "";
            //}


            //foreach (var item in rs.StudioMQuestion)
            //{
            //    if (item.QuestionID == int.Parse(cmbQuestion.SelectedValue.ToString()))
            //    {
            //        qa.AnswerType = item.AnswerType;
            //        break;
            //    }
            //}

            //rs.AddStudioMAttributes(qa);
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //StudioMResource.QandAForSupplier qa = ((FrameworkElement)sender).DataContext as StudioMResource.QandAForSupplier;
            //rs.RemoveStudioMAttributes(qa);
        }

        private void dataGrid1_RowDetailsVisibilityChanged(object sender, Microsoft.Windows.Controls.DataGridRowDetailsEventArgs e)
        {
            //Grid gr=(Grid)e.DetailsElement;
            //Microsoft.Windows.Controls.DataGrid dg2 = (Microsoft.Windows.Controls.DataGrid)gr.FindName("dataGrid2");
            //dg2.ItemsSource = rs.StudioMQandA;

        }
        private void Config_Click(object sender, RoutedEventArgs e)
        {
            RetailClusterSource.RetailCluster rcluster = (RetailClusterSource.RetailCluster)(((Button)e.OriginalSource).DataContext);
            //int clusterid = int.Parse(((Button)e.OriginalSource).CommandParameter.ToString());
            frmConfigureSupplierQAToCluster win = new frmConfigureSupplierQAToCluster(loginstateid, rcluster, loginuser, configType);
            win.ShowDialog();

        }
    }
}
