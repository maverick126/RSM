using System;
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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Xml;
using SQSAdmin_WpfCustomControlLibrary.SQSAdminWCFService;
using SQSAdmin_WpfCustomControlLibrary.Common;

namespace SQSAdmin_WpfCustomControlLibrary
{
    /// <summary>
    /// Interaction logic for frmStudioMAttributes.xaml
    /// </summary>
    public partial class frmStudioMAttributes : Window
    {
        private string productID = "";
        private string productNameString = "";
        private StudioMResource sr;
        private string selectedanswertype="";
        public frmStudioMAttributes(string pproductid, string productname, int pstateid)
        {
            sr = new StudioMResource(pproductid, pstateid);
            InitializeComponent();
            productID= pproductid;
            productNameString = productname;
            this.DataContext = sr;
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = "Studio M Attributes";
            textBlock1.Text = @"Configure StudioM attributes -- " + productID + " -- " + productNameString;

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            StudioMResource.QandAForSupplier  qa = ((FrameworkElement)sender).DataContext as StudioMResource.QandAForSupplier;
            sr.RemoveStudioMAttributes(qa);
        }

        private void cmbQuestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sr.LoadAnswerForQuestions(int.Parse(cmbQuestion.SelectedValue.ToString()));
            cmbAnswer.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (cmbAnswer.SelectedValue != null)
            {
                StudioMResource.QandAForSupplier qa = new StudioMResource.QandAForSupplier();
                qa.SupplierBrandID = int.Parse(cmbSupplier.SelectedValue.ToString());
                qa.SupplierBrandName = cmbSupplier.Text;
                qa.QuestionID = int.Parse(cmbQuestion.SelectedValue.ToString());
                qa.QuestionText = cmbQuestion.Text;
                qa.AnswerID = int.Parse(cmbAnswer.SelectedValue.ToString());
                qa.AnswerText = cmbAnswer.Text;

                foreach (var item in sr.StudioMQuestion)
                {
                    if (item.QuestionID == int.Parse(cmbQuestion.SelectedValue.ToString()))
                    {
                        qa.AnswerType = item.AnswerType;
                        break;
                    }
                }

                sr.AddStudioMAttributes(qa);
            }
            else
            {
                MessageBox.Show("Please select an answer for the question.");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            sr.SaveStudioMAttributes(productID);
        }
    }
}
