using System;
using System.IO;
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
using System.Xml;
using SQSAdmin_WpfCustomControlLibrary.SQSAdminWCFService;
using SQSAdmin_WpfCustomControlLibrary.Common;
using System.Configuration;



namespace SQSAdmin_WpfCustomControlLibrary
{
    /// <summary>
    /// Interaction logic for ctrlStudioMAttributes.xaml
    /// </summary>
    public partial class ctrlStudioMAttributes : UserControl
    {
        private string productID = "";
        private string productname = "";
        private StudioMResource sr;
        private bool IsStudioMProduct = false;
        private bool currentquestionmandatory = false;

        public ctrlStudioMAttributes(string pproductid, string pproductname, int pstateid, bool isstudioM)
        {
            sr = new StudioMResource(pproductid, pstateid);
            InitializeComponent();
            productID = pproductid;
            productname = pproductname;
            IsStudioMProduct = isstudioM;
            this.DataContext = sr;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsStudioMProduct)
            {
                textBlock1.Text = @"Configure StudioM Attributes -- " + productID + " -- " + productname;
                producttab.SelectedIndex = 0;
                
            }
            else
            {
                textBlock1.Text = @"Upload Image -- " + productID + " -- " + productname;
                producttab.SelectedIndex = 1;
                TabItem tb1= (TabItem)producttab.FindName("tab1");
                if (tb1 != null)
                {
                    tb1.IsEnabled = false;
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            StudioMResource.QandAForSupplier qa = ((FrameworkElement)sender).DataContext as StudioMResource.QandAForSupplier;
            sr.RemoveStudioMAttributes(qa);
        }

        private void cmbQuestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sr.LoadAnswerForQuestions(int.Parse(cmbQuestion.SelectedValue.ToString()));
            currentquestionmandatory = sr.StudioMQuestion[cmbQuestion.SelectedIndex].Mandatory;
            cmbAnswer.SelectedIndex = 0;
            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            sr.SaveStudioMAttributes(productID);
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog open = new  System.Windows.Forms.OpenFileDialog();
            open.Multiselect = false;
            open.Filter = "Image files (*.bmp, *.jpg, *png, *.gif, *.tiff)|*.bmp;*.jpg*;*.png;*.gif; *.tiff";
            open.AddExtension = true;

            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                UploadImage(open.FileName, open.OpenFile());
            }
        }
        private void UploadImage(string filename, Stream stream)
        {
            int imgwidth=800;
            int imgheight=600;
            //bool needresize = false;
            //int nWidth, nHeight;
            //System.Drawing.Image resizedimage;

            // get the target width and height from config
            XmlDocument doc = new XmlDocument();
            doc.Load(@"http://sqsadmin/sqsadminconfig.xml");
            XmlNodeList nodeList = doc.SelectNodes("connectionStrings/ResizeImageWidth");
            foreach (XmlNode node in nodeList)
            {
                imgwidth =  int.Parse(node.SelectSingleNode("value").InnerText);
            }

            nodeList = doc.SelectNodes("connectionStrings/ResizeImageHeight");
            foreach (XmlNode node in nodeList)
            {
                imgheight = int.Parse(node.SelectSingleNode("value").InnerText);
            }


            //get byte[] image
            byte[] byteData = new byte[stream.Length];
            stream.Read(byteData, 0, byteData.Length);

            byte[] resizedbyteData = sr.ResizeImages(byteData, imgwidth, imgheight);

            /*
            // convert byte[] image to system.drawing.image
            MemoryStream ms = new MemoryStream(byteData);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);

            // resize based on parameter
            if (returnImage.Width > returnImage.Height)//is landscape
            {
                if (returnImage.Width > imgwidth)
                {
                    needresize = true;
                    nWidth = (int)imgwidth;
                    nHeight = (int)((imgwidth * returnImage.Height) / returnImage.Width);
                }
                else
                {
                    needresize = false;
                }

            }
            else //portrait
            {
                if (returnImage.Height > imgheight)
                {
                    needresize = true;
                    nWidth = (int)((imgheight * returnImage.Width) / returnImage.Height);
                    nHeight = (int)imgheight;

                }
                else
                {
                    needresize = false;
                }
            }
            if (needresize)
            {
                resizedimage = resizeImage(returnImage, imgwidth, imgheight);
            }
            else
            {
                resizedimage = returnImage;
            }

            //convert system.drawing.image to byte[] image


            ms = new MemoryStream();
            System.Drawing.Imaging.ImageFormat format;

            if (resizedimage.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
                format = System.Drawing.Imaging.ImageFormat.Jpeg;
            else if (resizedimage.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
                format = System.Drawing.Imaging.ImageFormat.Bmp;
            else if (resizedimage.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Emf))
                format = System.Drawing.Imaging.ImageFormat.Emf;
            else if (resizedimage.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Exif))
                format = System.Drawing.Imaging.ImageFormat.Exif;
            else if (resizedimage.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
                format = System.Drawing.Imaging.ImageFormat.Gif;
            else if (resizedimage.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Icon))
                format = System.Drawing.Imaging.ImageFormat.Icon;
            else if (resizedimage.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                format = System.Drawing.Imaging.ImageFormat.Png;
            else if (resizedimage.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff))
                format = System.Drawing.Imaging.ImageFormat.Tiff;
            else if (resizedimage.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Wmf))
                format = System.Drawing.Imaging.ImageFormat.Wmf;
            else
                format = System.Drawing.Imaging.ImageFormat.Jpeg;

            resizedimage.Save(ms, format);
            byte[] rsByte = ms.ToArray();
            ms.Dispose();
            */
            string[] tempname = filename.Split('\\');
            string name=tempname[tempname.Length-1];

            //save image
            //SaveImageData(rsByte, "c:\\temp\\" + name);
            sr.SaveProductImage(productID, name, int.Parse(cmbSupplier.SelectedValue.ToString()), resizedbyteData);


        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            StudioMResource.ProductImage pi = ((FrameworkElement)sender).DataContext as StudioMResource.ProductImage;
            frmShowImage si = new frmShowImage(pi);
            si.ShowDialog();
        }

        private void btnDeleteImage_Click(object sender, RoutedEventArgs e)
        {
            MyDialog dialog = new MyDialog();
            int supplierid = 0;
            if (dialog.ShowDialog() == false)
            {
                if (dialog.ResponseText == "Y")
                {
                    StudioMResource.ProductImage pi = ((FrameworkElement)sender).DataContext as StudioMResource.ProductImage;
                    //if ((bool)chkFilter.IsChecked)
                    //    supplierid = int.Parse(cmbSupplier.SelectedValue.ToString());
                    sr.RemoveProductImage(pi.ImageID, supplierid);
                }
            }
        }



        private static void SaveImageData(byte[] imageData, string filePath)
        {

            FileStream fs = new FileStream(filePath, FileMode.Create,FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(imageData);
            bw.Close();
            fs.Close();

        }

        private void cmbSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int supplierid = 0;
            //if ((bool)chkFilter.IsChecked)
            //    supplierid = int.Parse(cmbSupplier.SelectedValue.ToString());

            sr.GetProductImage(productID, supplierid);
        }

        private void chkFilter_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            int supplierid = 0;
            //if ((bool)chkFilter.IsChecked)
            //    supplierid = int.Parse(cmbSupplier.SelectedValue.ToString());

            sr.GetProductImage(productID, supplierid);
        }

        //private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, int width, int height)
        //{
        //    int sourceWidth = (int)imgToResize.Width;
        //    int sourceHeight = (int)imgToResize.Height;

        //    float nPercent = 0;
        //    float nPercentW = 0;
        //    float nPercentH = 0;

        //    nPercentW = ((float)width / (float)sourceWidth);
        //    nPercentH = ((float)height / (float)sourceHeight);

        //    if (nPercentH < nPercentW)
        //        nPercent = nPercentH;
        //    else
        //        nPercent = nPercentW;

        //    int destWidth = (int)(sourceWidth * nPercent);
        //    int destHeight = (int)(sourceHeight * nPercent);

        //    System.Drawing.Bitmap b = new System.Drawing.Bitmap(destWidth, destHeight);
        //    System.Drawing.Graphics g = System.Drawing.Graphics.FromImage((System.Drawing.Image)b);
        //    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

        //    g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
        //    g.Dispose();

        //    return (System.Drawing.Image)b;
        //}

    }
}
