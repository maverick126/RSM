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
    /// Interaction logic for frmConfigureSupplierQAToCluster.xaml
    /// </summary>
    public partial class frmConfigureSupplierQAToCluster : Window
    {
        private int loginstateid = 0;
        private int clusterid = 0;
        private int groupid = 0;
        private RetailClusterSource rs;
        private bool currentquestionmandatory = false;
        private RetailClusterSource.RetailCluster rcluster;
        private RetailClusterSource.Group group;
        private string configType = "";
        private string id = "";
        public frmConfigureSupplierQAToCluster(int pstateid, object source, string pusercode, string configuretype)
        {
            loginstateid = pstateid;
            configType = configuretype;
            if (configuretype.ToUpper() == "RETAILCLUSTER")
            {
                rcluster = (RetailClusterSource.RetailCluster)source;
                clusterid = rcluster.RetailClusterID;
                id = clusterid.ToString();
                
            }
            else if (configuretype.ToUpper() == "GROUP")
            {
                group = (RetailClusterSource.Group)source;
                groupid = group.GroupID;
                id = groupid.ToString();
                
            }
            
            rs = new RetailClusterSource(loginstateid, pusercode);
            InitializeComponent();
            if (configuretype.ToUpper() == "RETAILCLUSTER")
            {
                txtclustername.Text = txtclustername.Text + "Retail Clusters -  " + rcluster.RetailClusterName;
                btnConfig.Content = "Save Template To Cluster";
            }
            else
            {
                txtclustername.Text = txtclustername.Text + "Group -  " + group.GroupName;
                btnConfig.Content = "Save Template To Groups";
            }
            this.DataContext = rs;
            rs.LoadSuppliers(loginstateid);
            rs.LoadQuestions(loginstateid);
            rs.LoadQAndA(id, configType);

            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;

        }

        private void cmbQuestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rs.LoadAnswerForQuestions(int.Parse(cmbQuestion.SelectedValue.ToString()));
            cmbAnswer.SelectedIndex = 0;
            currentquestionmandatory = rs.StudioMQuestion[cmbQuestion.SelectedIndex].Mandatory;

        }

        private void cmbSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int supplierid = 0;
            //if ((bool)chkFilter.IsChecked)
            //    supplierid = int.Parse(cmbSupplier.SelectedValue.ToString());

            //rs.GetProductImage(productID, supplierid);
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            int idx = dataGrid2.SelectedIndex;
            if ((cmbAnswer.Text.ToUpper().Contains("SINGLE SELECTION") || cmbAnswer.Text.ToUpper().Contains("MULTIPLE SELECTION")))
            {
                if (cmbAnswer.SelectedValue == null)
                {
                    MessageBox.Show("Please select an answer for the question.");
                }
                return;
            }

            StudioMResource.QandAForSupplier qa = new StudioMResource.QandAForSupplier();
            qa.SupplierBrandID = int.Parse(cmbSupplier.SelectedValue.ToString());
            qa.SupplierBrandName = cmbSupplier.Text;
            qa.QuestionID = int.Parse(cmbQuestion.SelectedValue.ToString());
            qa.QuestionText = cmbQuestion.Text;
            qa.Mandatory = currentquestionmandatory;
            if (cmbAnswer.SelectedValue != null)
            {
                qa.AnswerID = int.Parse(cmbAnswer.SelectedValue.ToString());
                qa.AnswerText = cmbAnswer.Text;
            }
            else
            {
                qa.AnswerID = 0;
                qa.AnswerText = "";
            }


            foreach (var item in rs.StudioMQuestion)
            {
                if (item.QuestionID == int.Parse(cmbQuestion.SelectedValue.ToString()))
                {
                    qa.AnswerType = item.AnswerType;
                    break;
                }
            }

            rs.AddStudioMAttributes(qa, idx);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            StudioMResource.QandAForSupplier qa = ((FrameworkElement)sender).DataContext as StudioMResource.QandAForSupplier;
            rs.RemoveStudioMAttributes(qa);
        }

        private void ButtonSaveToCluster_Click(object sender, RoutedEventArgs e)
        {
            rs.SaveStudioMAttributesToClusterOrGroup(id, configType);
        }
    }

}
