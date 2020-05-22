using System;
using System.IO;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using SQSAdmin_WpfCustomControlLibrary.SQSAdminWCFService;
using System.Xml;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Input;
using System.Runtime.Serialization;
using System.Web;

namespace SQSAdmin_WpfCustomControlLibrary.Common
{
    public class StudioMResource : ViewModelBase
    {
        private string productid = "";
        private int stateid = 1;       
        private ObservableCollection<Supplier> _supplier = new ObservableCollection<Supplier>();
        private ObservableCollection<Question> _question = new ObservableCollection<Question>();
        private ObservableCollection<Answer> _answer = new ObservableCollection<Answer>();
        private ObservableCollection<QandAForSupplier> _qanda = new ObservableCollection<QandAForSupplier>();
        private ObservableCollection<ProductImage> _image = new ObservableCollection<ProductImage>();
        private ObservableCollection<SupplierBrandResource.SupplierBrand> _supplierbrand = new ObservableCollection<SupplierBrandResource.SupplierBrand>();
        public int ThumbnailWidth;
        public int ThumbnailHeight;
        private SQSAdminServiceClient client;
        
        public StudioMResource(string pproductid, int pstateid)
        {
            //client = new SQSAdminServiceClient();
            productid = pproductid;
            stateid = pstateid;
            //LoadSuppliers();
            LoadQuestions();
            LoadStudioMAttributes();
            GetThumbnailSize();
            //GetProductImage(productid, );
            LoadSupplierBrand("", stateid, 1);
            
            
        }

        #region methods
        private void GetThumbnailSize()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"http://sqsadmin/sqsadminconfig.xml");
            XmlNodeList nodeList = doc.SelectNodes("connectionStrings/ThumbnailImageWidth");
            foreach (XmlNode node in nodeList)
            {
                ThumbnailWidth = int.Parse(node.SelectSingleNode("value").InnerText);
            }

            nodeList = doc.SelectNodes("connectionStrings/ThumbnailImageHeight");
            foreach (XmlNode node in nodeList)
            {
                ThumbnailHeight = int.Parse(node.SelectSingleNode("value").InnerText);
            }
        }
        public void LoadSupplierBrand(string brandname, int pstateid, int active)
        {
            SupplierBrandResource.SupplierBrand s;
            SQSSupplierBrand.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StudioM_GetSupplierBrand(brandname, pstateid, active);
            client.Close();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                s = new SupplierBrandResource.SupplierBrand();
                s.SupplierBrandID = int.Parse(dr["id_studiom_supplierbrand"].ToString());
                s.SupplierBrandName = dr["supplierbrandname"].ToString();
                s.Active = bool.Parse(dr["active"].ToString());
                s.BrandStateID = int.Parse(dr["fkidstate"].ToString());
                s.BrandStateName = dr["stateAbbreviation"].ToString();
                SQSSupplierBrand.Add(s);
            }
        }

        public void LoadSuppliers()
        {
            StudioMSupplier.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StudioM_GetSuppliers(stateid, "", 1);
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Supplier b = new Supplier();
                b.SupplierID = int.Parse(dr["idstudiomsupplier"].ToString());
                b.SupplierName = dr["suppliername"].ToString();
                StudioMSupplier.Add(b);
            }
        }
        public void LoadQuestions()
        {
            StudioMQuestion.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StudioM_GetActiveQuestions(stateid);
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Question b = new Question();
                b.QuestionID = int.Parse(dr["idtemplatequestion"].ToString());
                b.QuestionText = dr["question"].ToString();
                b.AnswerTypeID = int.Parse(dr["fkidanswertype"].ToString());
                b.AnswerType = dr["answertype"].ToString();
                b.QuestionAndType = dr["questionandtype"].ToString();
                b.Mandatory = bool.Parse(dr["mandatory"].ToString());
                StudioMQuestion.Add(b);
            }
        }
        public void LoadAnswerForQuestions(int questionid)
        {
            StudioMAnswer.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StudioM_GetAnswerForQuestion(questionid);
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Answer b = new Answer();
                b.AnswerID = int.Parse(dr["idtemplateanswer"].ToString());
                b.AnswerText = dr["answer"].ToString();
                StudioMAnswer.Add(b);
            }
        }



        public void AddStudioMAttributes(QandAForSupplier qa)
        {
            bool dul = false;
            foreach (var item in StudioMQandA)
            {
                if (item.SupplierBrandID == qa.SupplierBrandID && item.QuestionID == qa.QuestionID && item.AnswerID == qa.AnswerID)
                {
                    dul = true;
                    break;
                }
            }
            if (!dul)
            {
                StudioMQandA.Add(qa);
            }
        }


        public void GetProductImage(string productid, int supplierid)
        {
            StudioMProductImage.Clear();
            BitmapImage bi;
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StudioM_GetProductImages(productid, supplierid);
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ProductImage b = new ProductImage();
                b.ImageID = int.Parse(dr["id_StudioM_ProductImage"].ToString());
                b.ImageName = dr["imagename"].ToString();
                b.SupplierBrandName = dr["supplierbrandname"].ToString();
                b.ImageStream = (byte[])dr["image"];

                bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = new MemoryStream(ResizeImages((byte[])dr["image"],ThumbnailWidth, ThumbnailHeight));
                bi.EndInit();
                b.BMPImage = bi;
                StudioMProductImage.Add(b);
            }
        }

        public void RemoveProductImage(int imageid, int supplierid)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            client.SQSAdmin_StudioM_RemoveProductImage(imageid);
            client.Close();
            GetProductImage(productid, supplierid);
        }

        public void SaveProductImage(string productid, string imagename, int supplierid, byte[] imagestream)
        {
            int psupplierid = 0;
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            client.SQSAdmin_StudioM_SaveProductImage(productid, imagename, supplierid, imagestream);
            client.Close();
            //if (filter)
            //    psupplierid = supplierid;
            GetProductImage(productid, psupplierid);
        }

        public void RemoveStudioMAttributes(QandAForSupplier qa)
        {
            StudioMQandA.Remove(qa);
            //foreach (var item in StudioMQandA)
            //{
            //    if (item.SupplierID == qa.SupplierID && item.QuestionID == qa.QuestionID && item.AnswerID == qa.AnswerID)
            //    {
            //        StudioMQandA.Remove(qa);
            //    }
            //}
        }

        public void SaveStudioMAttributes(string productid)
        {
            int oldsupplierid = 0;
            int oldquestionid = 0;
            int oldanswerid = 0;
            string xml = "";
            string mandatory = "";
            QandAForSupplier qa;

            if (StudioMQandA.Count > 0)
            {
                xml = "<Brands>";

                var sortedQANDA = from item in StudioMQandA
                                  orderby item.SupplierBrandID, item.QuestionID, item.AnswerID
                                  select item;

                for (int i = 0; i < sortedQANDA.Count<QandAForSupplier>(); i++)
                {
                    qa = sortedQANDA.ElementAt<QandAForSupplier>(i);
                    if (qa.Mandatory)
                    {
                        mandatory = "1";
                    }
                    else
                    {
                        mandatory = "0";
                    }
                    if (qa.SupplierBrandID == oldsupplierid)
                    {
                        if (qa.QuestionID == oldquestionid)
                        {
                            if (qa.AnswerID == oldanswerid)
                            {
                            }
                            else
                            {
                                oldanswerid = qa.AnswerID;
                                xml = xml + @"<Answer id=""" + qa.AnswerID + @""" text=""" + System.Security.SecurityElement.Escape(qa.AnswerText) + @"""/>";
                            }
                        }
                        else
                        {
                            oldquestionid = qa.QuestionID;
                            oldanswerid = qa.AnswerID;
                            xml = xml + @"</Answers></Question>";
                            xml = xml + @"<Question id=""" + qa.QuestionID + @""" text=""" + System.Security.SecurityElement.Escape(qa.QuestionText) + @""" type=""" + qa.AnswerType + @""" mandatory=""" + mandatory + @""">";
                            xml = xml + @"<Answers>";
                            xml = xml + @"<Answer id=""" + qa.AnswerID + @""" text=""" + System.Security.SecurityElement.Escape(qa.AnswerText) + @"""/>";
                        }
                    }
                    else
                    {

                        oldsupplierid = qa.SupplierBrandID;
                        oldquestionid = qa.QuestionID;
                        oldanswerid = qa.AnswerID;

                        if (i > 0)
                        {
                            xml = xml + @"</Answers></Question></Questions></Brand>";
                        }

                        xml = xml + @"<Brand id=""" + qa.SupplierBrandID + @""" name=""" + System.Security.SecurityElement.Escape(qa.SupplierBrandName) + @""">";
                        xml = xml + @"<Questions>";
                        xml = xml + @"<Question id=""" + qa.QuestionID + @""" text=""" + System.Security.SecurityElement.Escape(qa.QuestionText) + @""" type=""" + qa.AnswerType + @""" mandatory=""" + mandatory + @""">";
                        xml = xml + @"<Answers>";
                        xml = xml + @"<Answer id=""" + qa.AnswerID + @""" text=""" + System.Security.SecurityElement.Escape(qa.AnswerText) + @"""/>";
                    }
                }
                xml = xml + @"</Answers></Question></Questions></Brand>";
                xml = xml + "</Brands>";
            }
            else
            {
                xml = "";
            }
            
            xml = xml.Replace("(Single Selection)", "");
            xml = xml.Replace("(Multiple Selection)", "");
            xml = xml.Replace("(Free Text)", "");
            xml = xml.Replace("(Integer)", "");
            xml = xml.Replace("(Decimal)", "");

            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            client.SQSAdmin_StudioM_UpdateProductStudioMAttribute(productid, xml);
            client.Close();

        }
        public void LoadStudioMAttributes()
        {
            QandAForSupplier qa;
            StudioMQandA.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
        
            string xml = client.SQSAdmin_StudioM_GetProductStudioMAttribute(productid);
            client.Close();

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                XmlNodeList supplierList = doc.SelectNodes("/Brands/Brand");
                foreach (XmlNode xd in supplierList)
                {
                    XmlNode idnode = xd.Attributes["id"];
                    XmlNode namenode = xd.Attributes["name"];
                    if (idnode != null && idnode.Value != "")
                    {
                        XmlNodeList questionList = doc.SelectNodes("/Brands/Brand[@id='" + idnode.Value + "']/Questions/Question");
                        foreach (XmlNode xd2 in questionList)
                        {
                            XmlNode idnode2 = xd2.Attributes["id"];
                            XmlNode textnode2 = xd2.Attributes["text"];
                            XmlNode atypenode = xd2.Attributes["type"];
                            XmlNode mandatory = xd2.Attributes["mandatory"];
                            if (idnode2 != null && idnode2.Value != "")
                            {
                                XmlNodeList answerList = doc.SelectNodes("/Brands/Brand[@id='" + idnode.Value + "']/Questions/Question[@id='" + idnode2.Value + "']/Answers/Answer");
                                foreach (XmlNode xd3 in answerList)
                                {
                                    XmlNode idnode3 = xd3.Attributes["id"];
                                    XmlNode textnode3 = xd3.Attributes["text"];
                                    bool m=true;
                                    qa = new QandAForSupplier();
                                    qa.AnswerID = int.Parse(idnode3.Value);
                                    qa.AnswerText = textnode3.Value;
                                    qa.AnswerType = atypenode.Value;
                                    qa.QuestionID = int.Parse(idnode2.Value.ToString());
                                    qa.QuestionText = textnode2.Value;
                                    qa.SupplierBrandID = int.Parse(idnode.Value.ToString());
                                    qa.SupplierBrandName = namenode.Value;
                                    if (mandatory!=null && mandatory.Value.ToString() == "1")
                                    {
                                        m = true;
                                    }
                                    else
                                    {
                                        if (mandatory == null)
                                        {
                                            m = true;
                                        }
                                        else
                                        {
                                            m = false;
                                        }
                                    }
                                    qa.Mandatory = m;

                                    StudioMQandA.Add(qa);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        public byte[] ResizeImages(byte[] byteData, int imgwidth, int imgheight)
        {
            // convert byte[] image to system.drawing.image
            bool needresize;
            int nWidth, nHeight;
            System.Drawing.Image resizedimage;

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
            return rsByte;
        }

        private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, int width, int height)
        {
            int sourceWidth = (int)imgToResize.Width;
            int sourceHeight = (int)imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)width / (float)sourceWidth);
            nPercentH = ((float)height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            System.Drawing.Bitmap b = new System.Drawing.Bitmap(destWidth, destHeight);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (System.Drawing.Image)b;
        }
        #endregion

        #region data contract

        public class Supplier
        {
            public int SupplierID { get; set; }
            public string SupplierName { get; set; }
        }
        public class Question
        {
            public int QuestionID { get; set; }
            public string QuestionText { get; set; }
            public int AnswerTypeID { get; set; }
            public string AnswerType { get; set; }
            public string QuestionAndType { get; set; }
            public bool Mandatory { get; set; }
        }
        public class Answer
        {
            public int AnswerID { get; set; }
            public string AnswerText { get; set; }
        }


        public class QandAForSupplier
        {
            public int SupplierBrandID { get; set; }
            public string SupplierBrandName { get; set; }

            public int QuestionID { get; set; }
            public string QuestionText { get; set; }

            public int AnswerID { get; set; }
            public string AnswerText { get; set; }

            public string AnswerType { get; set; }
            public bool Mandatory { get; set; }
        }

        public class ProductImage
        {
            public int ImageID { get; set; }
            public string SupplierBrandName { get; set; }
            public string ImageName { get; set; }
            public byte[] ImageStream { get; set; }
            public BitmapImage BMPImage { get; set; }
        }

        #endregion

        #region properties
        public ObservableCollection<Supplier> StudioMSupplier
        {
            get
            {
                return _supplier;
            }
            set
            {
                _supplier = value;
                OnPropertyChanged("StudioMSupplier");
            }
        }
        public ObservableCollection<Question> StudioMQuestion
        {
            get
            {
                return _question;
            }
            set
            {
                _question = value;
                OnPropertyChanged("StudioMQuestion");
            }
        }

        public ObservableCollection<Answer> StudioMAnswer
        {
            get
            {
                return _answer;
            }
            set
            {
                _answer = value;
                OnPropertyChanged("StudioMAnswer");
            }
        }

        public ObservableCollection<QandAForSupplier> StudioMQandA
        {
            get
            {
                return _qanda;
            }
            set
            {
                _qanda = value;
                OnPropertyChanged("StudioMQandA");
            }
        }
        public ObservableCollection<ProductImage> StudioMProductImage
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged("StudioMProductImage");
            }
        }
        public ObservableCollection<SupplierBrandResource.SupplierBrand> SQSSupplierBrand
        {
            get
            {
                return _supplierbrand;
            }
            set
            {
                _supplierbrand = value;
                OnPropertyChanged("SQSSupplierBrand");
            }
        }
        #endregion
    }
}
