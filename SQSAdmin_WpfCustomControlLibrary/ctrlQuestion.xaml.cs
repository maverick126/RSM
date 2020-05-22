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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using SQSAdmin_WpfCustomControlLibrary.SQSAdminWCFService;
using SQSAdmin_WpfCustomControlLibrary.Common;
using System.Windows.Controls.Primitives;

namespace SQSAdmin_WpfCustomControlLibrary
{
    /// <summary>
    /// Interaction logic for ctrlQuestion.xaml
    /// </summary>
    public partial class ctrlQuestion : UserControl
    {
        private ManagementResource mr;
        private int loginstate;
        public ctrlQuestion(int pstateid)
        {
            loginstate = pstateid;
            mr = new ManagementResource(loginstate);
            InitializeComponent();
            this.DataContext = mr;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            mr.LoadQuestions(txtQuestion.Text, int.Parse(cmbActive.SelectedValue.ToString()), loginstate);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Windows.Controls.DataGridRow row = (Microsoft.Windows.Controls.DataGridRow)(dataGrid1.ItemContainerGenerator.ContainerFromItem(dataGrid1.SelectedItem));
            ManagementResource.Question s = (ManagementResource.Question)row.Item;
            bool exists = false;
            s.QuestionText = RemoveExtraCarriageReturn(s.QuestionText);

            if (s.QuestionText != null && s.QuestionText.Trim() != "")
            {
                if (s.QuestionID == 0)
                {
                    if (!QuestionExists(s))
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
                    MessageBox.Show("This question already exists.");
                }
                else
                {
                    try
                    {
                        mr.SaveQuestion(s);
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
                MessageBox.Show("Please enter a question text.");
            }
        }

        private string RemoveExtraCarriageReturn(string inputtext)
        {
            string outputtext = inputtext;
            char[] temp = inputtext.Reverse<char>().ToArray();
            int index = 0;
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] >= 32 && temp[i] <= 255)
                {
                    index = i;
                    break;
                }
            }
            if (index > 0)
            {
                outputtext = inputtext.Substring(0, inputtext.Length - index);
            }
            return outputtext;

        }
        private bool QuestionExists(ManagementResource.Question s)
        {
            return mr.QuestionExists(s.AnswerTypeID, s.QuestionText);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ManagementResource.Question s = new ManagementResource.Question();
            s.QuestionStateID = loginstate;
            s.AnswerTypeID = 1;
            mr.StudioMQuestion.Add(s);
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
           // mr.LoadSuppliers(int.Parse(cmbState.SelectedValue.ToString()), txtSupplierName.Text, int.Parse(cmbActive.SelectedValue.ToString()));
            mr.LoadQuestions(txtQuestion.Text, int.Parse(cmbActive.SelectedValue.ToString()), int.Parse(cmbState.SelectedValue.ToString()));
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

        private void txtQName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tx = (TextBox)e.OriginalSource;
            mr.MaxLength = "Max length 1000 characters. " + (1000 - tx.Text.Length).ToString() + " left.";
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtQuestion.Text = "";
            cmbActive.SelectedValue = 1;
            cmbState.SelectedValue = loginstate;
            btnSearch_Click(null, null);
        }

    }
}
