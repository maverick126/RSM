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
    /// Interaction logic for ctrlAnswer.xaml
    /// </summary>
    public partial class ctrlAnswer : UserControl
    {
        private ManagementResource mr;
        private int loginstate;
        public ctrlAnswer(int pstateid)
        {
            loginstate = pstateid;
            mr = new ManagementResource(loginstate);
            InitializeComponent();
            this.DataContext = mr;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            mr.LoadAnswer(txtQuestion.Text, txtAnswer.Text, int.Parse(cmbActive.SelectedValue.ToString()), loginstate);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Windows.Controls.DataGridRow row = (Microsoft.Windows.Controls.DataGridRow)(dataGrid1.ItemContainerGenerator.ContainerFromItem(dataGrid1.SelectedItem));
            ManagementResource.Answer s = (ManagementResource.Answer)row.Item;
            bool exists = false;

            if (s.QuestionID == null || s.QuestionID < 1)
            {
                MessageBox.Show("Please select a question for the answer.");
            }
            else
            {
                if (s.AnswerText != null && s.AnswerText.Trim() != "")
                {
                    if (s.AnswerID == 0)
                    {
                        if (!AnswerExists(s))
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
                        MessageBox.Show("This answer already exists.");
                    }
                    else
                    {
                        try
                        {
                            mr.SaveAnswer(s);
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
                    MessageBox.Show("Please enter an answer text.");
                }
            }
        }

        private bool AnswerExists(ManagementResource.Answer s)
        {
            return mr.AnswerExists(s.QuestionID, s.AnswerText);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ManagementResource.Answer s = new ManagementResource.Answer();
            s.AnswerID = 0;
            mr.StudioMAnswer.Add(s);
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            mr.LoadAnswer(txtQuestion.Text, txtAnswer.Text, int.Parse(cmbActive.SelectedValue.ToString()), loginstate);
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

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtQuestion.Text = "";
            txtAnswer.Text = "";
            cmbActive.SelectedValue = 1;
            btnSearch_Click(null, null);
        }

    }
}
