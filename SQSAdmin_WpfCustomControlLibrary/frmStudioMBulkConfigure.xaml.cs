using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
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
    /// Interaction logic for frmStudioMBulkConfigure.xaml
    /// </summary>
    public partial class frmStudioMBulkConfigure : Window
    {
        private SQSAdminServiceClient client;
        private int loginstateid;
        private BulkConfigureResource br;
        private string usercode;
        public frmStudioMBulkConfigure(int stateid, string pusercode)
        {
            loginstateid = stateid;
            usercode = pusercode;
            br = new BulkConfigureResource(loginstateid);
            InitializeComponent();
            this.DataContext = br;
            btnSearchProduct_Click(null, null);
            btnSearchQuestion_Click(null, null);
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
        }

        private void btnSearchProduct_Click(object sender, RoutedEventArgs e)
        {
            br.SearchAvailableProducts(loginstateid, txtProductID.Text, txtProductName.Text);
        }


        private void btnProductAdd_Click(object sender, RoutedEventArgs e)
        {
            var itemsSource = ProductGrid1.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {

                foreach (var item in itemsSource)
                {
                    var row = ProductGrid1.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = ProductGrid1.Columns[0].GetCellContent(row) as ContentPresenter;
                    DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                    CheckBox chk = editingTemplate.FindName("chkSelected", contentPresenter) as CheckBox;
                    TextBlock tx = ProductGrid1.Columns[1].GetCellContent(row) as TextBlock;

                    ContentPresenter contentPresenter2 = ProductGrid1.Columns[2].GetCellContent(row) as ContentPresenter;
                    DataTemplate editingTemplate2 = contentPresenter2.ContentTemplate;
                    TextBlock tx2 = editingTemplate2.FindName("txtproductname", contentPresenter2) as TextBlock;

                    if ((bool)chk.IsChecked)
                    {
 
                        Product p = new Product();
                        p.ProductID = tx.Text;
                        p.ProductName = tx2.Text;
                        br.SelectedProduct.Add(p);
                        br.TempSelectedProduct.Add(p);

                    }
                }
            }

            br.SearchAvailableProducts(loginstateid, txtProductID.Text, txtProductName.Text);
            chkAllProduct.IsChecked = false;
            chkAllProduct2.IsChecked = false;
        }

        private void btnProductRemove_Click(object sender, RoutedEventArgs e)
        {
            var itemsSource = ProductGrid2.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {

                foreach (var item in itemsSource)
                {
                    var row = ProductGrid2.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = ProductGrid2.Columns[0].GetCellContent(row) as ContentPresenter;
                    DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                    CheckBox chk = editingTemplate.FindName("chkSelected", contentPresenter) as CheckBox;
                    TextBlock tx = ProductGrid2.Columns[1].GetCellContent(row) as TextBlock;
 
                    if ((bool)chk.IsChecked)
                    {
                        Product prod = (Product)item;
                        br.TempSelectedProduct.Remove(prod);
                    }
                }
            }
            br.SelectedProduct.Clear();
            foreach (Product p in br.TempSelectedProduct)
            {
                br.SelectedProduct.Add(p);
            }
            br.SearchAvailableProducts(loginstateid, txtProductID.Text, txtProductName.Text);
            chkAllProduct2.IsChecked = false;
        }

        private void btnSearchQuestion_Click(object sender, RoutedEventArgs e)
        {
            br.LoadQuestion(loginstateid, txtQuestion.Text);
        }

        private void btnQuestionAdd_Click(object sender, RoutedEventArgs e)
        {
            var itemsSource = QuestionGrid1.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {

                foreach (var item in itemsSource)
                {
                    var row = QuestionGrid1.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = QuestionGrid1.Columns[0].GetCellContent(row) as ContentPresenter;
                    DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                    CheckBox chk = editingTemplate.FindName("chkSelected", contentPresenter) as CheckBox;
                    TextBlock tx = QuestionGrid1.Columns[1].GetCellContent(row) as TextBlock;
                    CheckBox chk2 = QuestionGrid1.Columns[4].GetCellContent(row) as CheckBox;

                    ContentPresenter contentPresenter2 = QuestionGrid1.Columns[2].GetCellContent(row) as ContentPresenter;
                    DataTemplate editingTemplate2 = contentPresenter2.ContentTemplate;
                    TextBlock tx2 = editingTemplate2.FindName("txtquestionname", contentPresenter2) as TextBlock;

                    TextBlock tx3 = QuestionGrid1.Columns[3].GetCellContent(row) as TextBlock;
                    if ((bool)chk.IsChecked)
                    {

                        StudioMResource.Question q = new StudioMResource.Question();
                        q.QuestionID = int.Parse(tx.Text);
                        q.QuestionText = tx2.Text;
                        q.AnswerType = tx3.Text;
                        q.Mandatory = (bool)chk2.IsChecked;

                        br.SelectedQuestion.Add(q);
                        br.TempSelectedQuestion.Add(q);

                    }
                }
            }
            br.LoadQuestion(loginstateid, txtQuestion.Text);
            if (br.SelectedQuestion.Count == 1)
            {
                cmbQuestion.SelectedIndex = 0;
                cmbQuestion_SelectionChanged(null, null);
            }
        }

        private void btnQuestionRemove_Click(object sender, RoutedEventArgs e)
        {
            var itemsSource = QuestionGrid2.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {

                foreach (var item in itemsSource)
                {
                    var row = QuestionGrid2.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = QuestionGrid2.Columns[0].GetCellContent(row) as ContentPresenter;
                    DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                    CheckBox chk = editingTemplate.FindName("chkSelected", contentPresenter) as CheckBox;
                    TextBlock tx = QuestionGrid2.Columns[1].GetCellContent(row) as TextBlock;
                    CheckBox chk2 = QuestionGrid2.Columns[3].GetCellContent(row) as CheckBox;

                    ContentPresenter contentPresenter2 = QuestionGrid2.Columns[2].GetCellContent(row) as ContentPresenter;
                    DataTemplate editingTemplate2 = contentPresenter2.ContentTemplate;
                    TextBlock tx2 = editingTemplate2.FindName("txtquestionname", contentPresenter2) as TextBlock;

                    if ((bool)chk.IsChecked)
                    {
                        StudioMResource.Question q = (StudioMResource.Question)item;
                        br.TempSelectedQuestion.Remove(q);
                    }
                }
            }
            br.SelectedQuestion.Clear();
            foreach (StudioMResource.Question q in br.TempSelectedQuestion)
            {
                br.SelectedQuestion.Add(q);
            }
            br.LoadQuestion(loginstateid, txtQuestion.Text);
            if (br.SelectedQuestion.Count == 1)
            {
                cmbQuestion.SelectedIndex = 0;
                cmbQuestion_SelectionChanged(null, null);
            }

            if (br.SelectedQuestion.Count==0)
            {
                br.SelectedAnswer.Clear();
                br.TempSelectedAnswer.Clear();
            }
        }

        private void cmbQuestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbQuestion.Text.Trim() == "")
            {
                br.StudioMAnswer.Clear();
            }
            else
            {
                if (cmbQuestion.SelectedValue != null)
                {
                    br.LoadAnswerForQuestions(int.Parse(cmbQuestion.SelectedValue.ToString()));
                }
                else
                {
                    br.StudioMAnswer.Clear();
                }
            }
        }

        private void btnAnswerAdd_Click(object sender, RoutedEventArgs e)
        {
            var itemsSource = AnswerGrid1.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {

                foreach (var item in itemsSource)
                {
                    var row = AnswerGrid1.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = AnswerGrid1.Columns[0].GetCellContent(row) as ContentPresenter;
                    DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                    CheckBox chk = editingTemplate.FindName("chkSelected", contentPresenter) as CheckBox;
                    TextBlock tx = AnswerGrid1.Columns[1].GetCellContent(row) as TextBlock;

                    ContentPresenter contentPresenter2 = AnswerGrid1.Columns[2].GetCellContent(row) as ContentPresenter;
                    DataTemplate editingTemplate2 = contentPresenter2.ContentTemplate;
                    TextBlock tx2 = editingTemplate2.FindName("txtanswername", contentPresenter2) as TextBlock;

                    if ((bool)chk.IsChecked)
                    {

                        StudioMResource.Answer q = new StudioMResource.Answer();
                        q.AnswerID = int.Parse(tx.Text);
                        q.AnswerText = tx2.Text;
                        br.SelectedAnswer.Add(q);
                        br.TempSelectedAnswer.Add(q);

                    }
                }
            }
            if (cmbQuestion.Text.Trim() != "")
            {
                br.LoadAnswerForQuestions(int.Parse(cmbQuestion.SelectedValue.ToString()));
            }
            else
            {
                br.StudioMAnswer.Clear();
            }
            chkAll2.IsChecked = false;
            chkAll.IsChecked = false;
        }

        private void btnAnswerRemove_Click(object sender, RoutedEventArgs e)
        {
            var itemsSource = AnswerGrid2.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {

                foreach (var item in itemsSource)
                {
                    var row = AnswerGrid2.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = AnswerGrid2.Columns[0].GetCellContent(row) as ContentPresenter;
                    DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                    CheckBox chk = editingTemplate.FindName("chkSelected", contentPresenter) as CheckBox;
                    TextBlock tx = AnswerGrid2.Columns[1].GetCellContent(row) as TextBlock;

                    ContentPresenter contentPresenter2 = AnswerGrid2.Columns[2].GetCellContent(row) as ContentPresenter;
                    DataTemplate editingTemplate2 = contentPresenter2.ContentTemplate;
                    TextBlock tx2 = editingTemplate2.FindName("txtanswername", contentPresenter2) as TextBlock;

                    if ((bool)chk.IsChecked)
                    {
                        StudioMResource.Answer q = (StudioMResource.Answer)item;
                        br.TempSelectedAnswer.Remove(q);
                    }
                }
            }
            br.SelectedAnswer.Clear();
            foreach (StudioMResource.Answer q in br.TempSelectedAnswer)
            {
                br.SelectedAnswer.Add(q);
            }
            if (cmbQuestion.Text.Trim() != "" || cmbQuestion.SelectedValue != null)
            {
                br.LoadAnswerForQuestions(int.Parse(cmbQuestion.SelectedValue.ToString()));
            }
            chkAll2.IsChecked = false;
            chkAll.IsChecked = false;
        }

        private void chkAll_Click(object sender, RoutedEventArgs e)
        {
            var itemsSource = AnswerGrid1.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {

                foreach (var item in itemsSource)
                {
                    var row = AnswerGrid1.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = AnswerGrid1.Columns[0].GetCellContent(row) as ContentPresenter;
                    DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                    CheckBox chk = editingTemplate.FindName("chkSelected", contentPresenter) as CheckBox;

                    chk.IsChecked = chkAll.IsChecked;
                }
            }          
        }

        private void chkAll2_Click(object sender, RoutedEventArgs e)
        {
            var itemsSource = AnswerGrid2.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {

                foreach (var item in itemsSource)
                {
                    var row = AnswerGrid2.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = AnswerGrid1.Columns[0].GetCellContent(row) as ContentPresenter;
                    DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                    CheckBox chk = editingTemplate.FindName("chkSelected", contentPresenter) as CheckBox;

                    chk.IsChecked = chkAll2.IsChecked;
                }
            }   
        }

        private void btnConfigure_Click(object sender, RoutedEventArgs e)
        {
            if (cmbSupplier.Text.Trim() == "")
            {
                MessageBox.Show("Please select a supplier brand.");
                return;
            }
            if (cmbQuestion.Text.Trim() == "")
            {
                MessageBox.Show("Please select a question.");
                return;
            }
            if (br.SelectedProduct.Count < 1)
            {
                MessageBox.Show("No product has been selected.");
                return;
            }
            if (br.SelectedQuestion.Count < 1)
            {
                MessageBox.Show("No question has been selected.");
                return;
            }
            if (br.SelectedAnswer.Count < 1)
            {
                MessageBox.Show("No answer has been selected.");
                return;
            }

            string selectedproductidstring = "";
            string selectedquestionidsstring = "";
            string selectedansweridstring = "";

            foreach (Product p in br.SelectedProduct)
            {
                if (selectedproductidstring == "")
                {
                    selectedproductidstring = p.ProductID;
                }
                else
                {
                    selectedproductidstring = selectedproductidstring+","+ p.ProductID;
                }
            }

            foreach (StudioMResource.Question q in br.SelectedQuestion)
            {
                if (selectedquestionidsstring == "")
                {
                    selectedquestionidsstring = q.QuestionID.ToString();
                }
                else
                {
                    selectedquestionidsstring = selectedquestionidsstring + "," + q.QuestionID.ToString();
                }
            }
            foreach (StudioMResource.Answer q in br.SelectedAnswer)
            {
                if (selectedansweridstring == "")
                {
                    selectedansweridstring = q.AnswerID.ToString();
                }
                else
                {
                    selectedansweridstring = selectedansweridstring + "," + q.AnswerID.ToString();
                }
            }

            if (br.BulkConfigureStudioMQandA(cmbSupplier.SelectedValue.ToString(), selectedproductidstring, selectedquestionidsstring, selectedansweridstring, usercode))
            {
                MessageBox.Show("The configuration has been saved!");
            }
            else
            {
                MessageBox.Show("Exception occurred when save the configuration.");
            }

        }

        private void btnResetProduct_Click(object sender, RoutedEventArgs e)
        {
            txtProductID.Text = "";
            txtProductName.Text = "";
            btnSearchProduct_Click(null, null);
        }

        private void btnResetQuestion_Click(object sender, RoutedEventArgs e)
        {
            txtQuestion.Text = "";
            btnSearchQuestion_Click(null, null);
        }

        private void btnResetAll_Click(object sender, RoutedEventArgs e)
        {
            br.SelectedAnswer.Clear();
            br.SelectedQuestion.Clear();
            br.SelectedProduct.Clear();
            br.StudioMAnswer.Clear();
        }

        private void chkAllProduct_Click(object sender, RoutedEventArgs e)
        {
            var itemsSource = ProductGrid1.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {

                foreach (var item in itemsSource)
                {
                    var row = ProductGrid1.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = ProductGrid1.Columns[0].GetCellContent(row) as ContentPresenter;
                    DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                    CheckBox chk = editingTemplate.FindName("chkSelected", contentPresenter) as CheckBox;

                    chk.IsChecked = chkAllProduct.IsChecked;
                }
            }
        }

        private void chkAllProduct2_Click(object sender, RoutedEventArgs e)
        {
            var itemsSource = ProductGrid2.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {

                foreach (var item in itemsSource)
                {
                    var row = ProductGrid2.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = ProductGrid2.Columns[0].GetCellContent(row) as ContentPresenter;
                    DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                    CheckBox chk = editingTemplate.FindName("chkSelected", contentPresenter) as CheckBox;

                    chk.IsChecked = chkAllProduct2.IsChecked;
                }
            }
        }


    }
}
