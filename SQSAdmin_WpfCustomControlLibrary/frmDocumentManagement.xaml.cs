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
using SQSAdmin_WpfCustomControlLibrary.SQSAdminWCFService;
using SQSAdmin_WpfCustomControlLibrary.Common;
using System.Xml;
using System.Diagnostics;
using System.Data;

namespace SQSAdmin_WpfCustomControlLibrary
{
    /// <summary>
    /// Interaction logic for frmDocumentManagement.xaml
    /// </summary>
    public partial class frmDocumentManagement : Window
    {
        private SQSAdminServiceClient client;
        private int loginstateid;
        private CommonResource cr;
        private string documentAttachmentsPath = string.Empty;
        public frmDocumentManagement(int stateid, string usercode)
        {
            loginstateid = stateid;
            cr = new CommonResource(stateid, 0);
            InitializeComponent();
            this.DataContext = cr;
            dteEffectiveDate.SelectedDate = DateTime.Now;
            //cmbSystemDocumentType.SelectedIndex = 0;
            cr.LoadSystemDocumentTypes();
            cr.LoadAttachmentTypes();
            cmbAttachmentType.SelectedIndex = 0;
            cmbBrand.OnSelectedItemsChanged2 += new EventHandler(OnSelectedItemsChanged);
        }

        private void cmbState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //LoadBrandDropDown();
            int pstateid;
            if (cmbState.SelectedValue != null)
            {
                pstateid = int.Parse(cmbState.SelectedValue.ToString());
            }
            else
            {
                pstateid = loginstateid;
            }
            cr.LoadRegion(pstateid);
            cr.LoadBrand(pstateid);
            cr.LoadHomesPlans(pstateid, null, "", 1); // return all homes in the state
            cmbBrand.Text = string.Empty;
            cmbRegion.Text = string.Empty;
            cmbHome.Text = string.Empty;
            cmbHome.IsEnabled = false;
            radioButtonBrand.IsChecked = true;
        }
        private void bthSearch_Click(object sender, RoutedEventArgs e)
        {
            int pstateid = 0;
            string pregionids = string.Empty;
            string pbrandids = string.Empty;
            string psystemid= string.Empty;
            string attachmenttypeid = string.Empty;
            string selectedhomes = string.Empty;
            if (cmbState.SelectedValue != null)
            {
                pstateid = int.Parse(cmbState.SelectedValue.ToString());
            }
            else
            {
                pstateid = loginstateid;
            }
            if (cmbRegion.SelectedItems != null)
            {
                pregionids = cr.getIDsSelected(cmbRegion.SelectedItems);
            }
            if (cmbBrand.SelectedItems != null)
            {
                pbrandids = cr.getIDsSelected(cmbBrand.SelectedItems);
            }
            if (cmbSystemDocumentType.SelectedItems != null)
            {
                psystemid = cr.getIDsSelected(cmbSystemDocumentType.SelectedItems);
            }
            if (cmbAttachmentType.SelectedItem != null)
            {
                attachmenttypeid = cmbAttachmentType.SelectedValue.ToString();
            }
            if (cmbHome.SelectedItems != null)
            {
                selectedhomes = cr.getTextSelectedAsString(cmbHome.SelectedItems);
            }
            cr.GetSpecificationByStateRegionsBrands(pstateid, pregionids, pbrandids, dteEffectiveDate.SelectedDate ?? DateTime.Now, psystemid, attachmenttypeid,selectedhomes);
        }

        private static void OnSelectedItemsChanged(object sender, EventArgs e)
        {
        }

        private void bthAttachmentAdd_Click(object sender, RoutedEventArgs e)
        {
            // validation
            if (string.IsNullOrWhiteSpace(cmbRegion.Text))
            {
                MessageBox.Show("Please select 1 or more regions");
            }
            else if (string.IsNullOrWhiteSpace(cmbBrand.Text))
            {
                MessageBox.Show("Please select 1 or more brands");
            }
            else if ((radioButtonHome.IsChecked ?? false) && (string.IsNullOrWhiteSpace(cmbHome.Text)))
            {
                MessageBox.Show("Please select 1 or more homes");
            }
            else if (string.IsNullOrWhiteSpace(cmbSystemDocumentType.Text))
            {
                MessageBox.Show("Please select 1 or more system documents");
            }
            else
            {
                frmDocumentManagementSaveConfirm saveConfirm = new frmDocumentManagementSaveConfirm(cmbState.Text, cmbRegion.Text, cmbBrand.Text, dteEffectiveDate.Text, cmbSystemDocumentType.Text, cmbAttachmentType.Text, cmbHome.Text);
                if (saveConfirm.ShowDialog()??false)
                {
                System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
                openFileDialog1.Filter = "Pdf Files|*.pdf;*.xlsx;*.xlsm";
                openFileDialog1.Multiselect = false;

                System.Windows.Forms.DialogResult result = openFileDialog1.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    string filename = openFileDialog1.FileName;
                    if (filename != "")
                    {
                        //this.loadingimage.Image = global::SQSAdmin_WpfCustomControlLibrary.Properties.Resources.processingImage;

                        try
                        {
                            int pstateid = 0;
                            int pregionid = 0;
                            int pbrandid = 0;
                            int phomeid = 0;
                            int psystemformid = 0;
                            int pattachmenttypeid = 0;
                            string filenameonlynopath = "";
                            DateTime effectiveDate = dteEffectiveDate.SelectedDate ?? DateTime.Now;


                            #region  upload file, only need once.
                            string[] tempname = filename.Split('\\');
                            filenameonlynopath = tempname[tempname.Length-1];

                            if (string.IsNullOrWhiteSpace(documentAttachmentsPath))
                            {
                                XmlDocument doc = new XmlDocument();
                                doc.Load(@"http://sqsadmin/sqsadminconfig.xml");
                                XmlNodeList nodeList = doc.SelectNodes("connectionStrings/AttachmentsPath/" + CommonVariables.Environment);
                                foreach (XmlNode node in nodeList)
                                {
                                    documentAttachmentsPath = @"" + node.SelectSingleNode("value").InnerText;
                                }
                            }
                            if (string.IsNullOrWhiteSpace(documentAttachmentsPath))
                            {
                                MessageBox.Show("Attachments cannot be uploaded due to missing server path.");
                                return;
                            }
                            byte[] bytesAttachment = System.IO.File.ReadAllBytes(filename);
                            System.IO.File.WriteAllBytes(documentAttachmentsPath + filenameonlynopath, bytesAttachment);
                            #endregion


                            foreach (KeyValuePair<string, object> region in cmbRegion.SelectedItems)
                            {
                                foreach (KeyValuePair<string, object> brand in cmbBrand.SelectedItems)
                                {
                                    foreach (KeyValuePair<string, object> system in cmbSystemDocumentType.SelectedItems)
                                    {
                                        if (cmbHome.SelectedItems.Count < 1)
                                        {
                                            cmbHome.SelectedItems.Add("0", 0);
                                        }
                                        foreach (KeyValuePair<string, object> home in cmbHome.SelectedItems)
                                        {
                                            phomeid = int.Parse(home.Value.ToString());
                                            if (phomeid > 0)
                                            {
                                                // filenameonlynopath += "_" + home.Key;
                                            }

                                            pattachmenttypeid = int.Parse(cmbAttachmentType.SelectedValue.ToString());
 

                                            if (cmbState.SelectedValue != null)
                                            {
                                                pstateid = int.Parse(cmbState.SelectedValue.ToString());
                                            }
                                            else
                                            {
                                                pstateid = loginstateid;
                                            }

                                            pregionid = int.Parse(region.Value.ToString());
                                            pbrandid = int.Parse(brand.Value.ToString());

                                            psystemformid = int.Parse(system.Value.ToString());
                                            cr.SaveSpecificationByStateRegionBrand(0, filenameonlynopath, pstateid, pregionid, pbrandid, phomeid, effectiveDate, psystemformid, pattachmenttypeid, 1);

                                        }
                                    }
                                }
                            }

                            bthSearch_Click(sender, e);

                                MessageBox.Show("Successfully uploaded the attachment.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }
        }

        private void cmbBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int pstateid = 0;
            int pbrandid = 0;
            if (cmbState.SelectedValue != null)
            {
                pstateid = int.Parse(cmbState.SelectedValue.ToString());
            }
            else
            {
                pstateid = loginstateid;
            }
            if (cmbBrand.SelectedItems != null)
            {
                pbrandid = int.Parse(cmbBrand.SelectedItems.ToString());
            }
            cr.LoadHomes(pstateid, pbrandid, "", 1);
        }

        private void btnAttachment_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(documentAttachmentsPath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@"http://sqsadmin/sqsadminconfig.xml");
                XmlNodeList nodeList = doc.SelectNodes("connectionStrings/AttachmentsPath/" + CommonVariables.Environment);
                foreach (XmlNode node in nodeList)
                {
                    documentAttachmentsPath = @"" + node.SelectSingleNode("value").InnerText;
                }
            }
            if (!string.IsNullOrWhiteSpace(documentAttachmentsPath))
            {
                Process.Start(documentAttachmentsPath + ((CommonResource.Specification)((FrameworkElement)sender).DataContext).FileName);
            }
        }

        private void btnShowHomesForSelectedBrands_Click(object sender, RoutedEventArgs e)
        {
            LoadHomes();
        }

        private void LoadHomes()
        {
            int pstateid;
            List<int> brandids = new List<int>();
            int brandid = 0;

            if (cmbState.SelectedValue != null)
            {
                pstateid = int.Parse(cmbState.SelectedValue.ToString());
            }
            else
            {
                pstateid = loginstateid;
            }

            foreach (KeyValuePair<string, object> item in cr.SelectedItemsBrand)
            {
                int.TryParse(item.Value.ToString(), out brandid);
                brandids.Add(brandid);
            }
            cr.LoadHomesPlans(pstateid, brandids, "", 1);
        }

        private void radioButtonBrand_Checked(object sender, RoutedEventArgs e)
        {
            cmbBrand.IsEnabled = true;
            cmbHome.IsEnabled = false;
            cmbHome.Text = string.Empty;
            cr.ItemsHome.Clear();
        }

        private void radioButtonHome_Checked(object sender, RoutedEventArgs e)
        {
            cmbBrand.IsEnabled = false;
            cmbHome.IsEnabled = true;
            LoadHomes();
        }

        private void checkBoxActive_Click(object sender, RoutedEventArgs e)
        {
            CommonResource.Specification itemSelected = (CommonResource.Specification)((System.Windows.FrameworkElement)sender).DataContext;

            try
            {
                if (itemSelected != null)
                {
                    var confirmResult = MessageBox.Show("Are you sure to mark this item " + (itemSelected.Active ? "active" : "inactive") + " ?",
                                                         "Confirm Change!!", MessageBoxButton.YesNo);
                    if (confirmResult == MessageBoxResult.Yes)
                    {
                        // save changes
                        cr.SaveSpecificationByStateRegionBrand(itemSelected.ID, string.Empty, 0, 0, 0, 0, DateTime.Now, 0, 0, itemSelected.Active?1:0);
                    }
                    else
                    {
                        itemSelected.Active = !itemSelected.Active;
                        ((System.Windows.Controls.Primitives.ToggleButton)sender).IsChecked = itemSelected.Active;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
