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
using SQSAdmin_WpfCustomControlLibrary.Common;

namespace SQSAdmin_WpfCustomControlLibrary
{
    /// <summary>
    /// Interaction logic for frmNewPromotion.xaml
    /// </summary>
    public partial class frmDocumentManagementSaveConfirm : Window
    {
        private SQSAdmin_WpfCustomControlLibrary.SQSAdminWCFService.SQSAdminServiceClient client;

        public frmDocumentManagementSaveConfirm(string stateName, string regionName, string brandName, string effectiveDate, string systemForm, string type, string homeName)
        {
            InitializeComponent();

            txtState.Text = stateName;
            txtRegion.Text = regionName;
            txtBrand.Text = brandName;
            txtEffectiveDate.Text = effectiveDate;
            txtSystemForm.Text = systemForm;
            txtDocumentType.Text = type;
            txtHome.Text = homeName;

            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
