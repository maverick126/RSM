using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using SQSAdmin_WpfCustomControlLibrary.SQSAdminWCFService;

namespace SQSAdmin_WpfCustomControlLibrary.Common
{
    class BulkConfigureResource : ViewModelBase
    {
        private SQSAdminServiceClient client;
        public int loginstateid;
        private ObservableCollection<CommonResource.Brand> _brand = new ObservableCollection<CommonResource.Brand>();
        private ObservableCollection<SupplierBrandResource.SupplierBrand> _supplierbrand = new ObservableCollection<SupplierBrandResource.SupplierBrand>();
        private ObservableCollection<Product> _selectedproduct = new ObservableCollection<Product>();
        private ObservableCollection<Product> _tempselectedproduct = new ObservableCollection<Product>();
        private ObservableCollection<Product> _availableproduct = new ObservableCollection<Product>();
        private ObservableCollection<StudioMResource.Question> _question = new ObservableCollection<StudioMResource.Question>();
        private ObservableCollection<StudioMResource.Question> _selectedquestion = new ObservableCollection<StudioMResource.Question>();
        private ObservableCollection<StudioMResource.Question> _tempselectedquestion = new ObservableCollection<StudioMResource.Question>();
        private ObservableCollection<StudioMResource.Answer> _answer = new ObservableCollection<StudioMResource.Answer>();
        private ObservableCollection<StudioMResource.Answer> _selectedanswer = new ObservableCollection<StudioMResource.Answer>();
        private ObservableCollection<StudioMResource.Answer> _tempselectedanswer = new ObservableCollection<StudioMResource.Answer>();
        public BulkConfigureResource(int stateid)
        {
            loginstateid=stateid;
            LoadSupplierBrand("", stateid, 1);
            //LoadBrand(loginstateid);
        }
        #region methods
        public void LoadBrand(int stateid)
        {
            //SQSBrand.Clear();
            //client = new SQSAdminServiceClient();
            //client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            //DataSet ds = client.SQSAdmin_Generic_GetBrandByState(stateid);
            //client.Close();
            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    CommonResource.Brand b = new CommonResource.Brand();
            //    b.BrandID = int.Parse(dr["brandid"].ToString());
            //    b.BrandName = dr["brandname"].ToString();
            //    SQSBrand.Add(b);
            //}
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

        public void LoadQuestion(int stateid, string searchtext)
        {
            StudioMQuestion.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StudioM_SearchActiveQuestions(stateid, searchtext);
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                StudioMResource.Question b = new StudioMResource.Question();
                b.QuestionID = int.Parse(dr["idtemplatequestion"].ToString());
                b.QuestionText = dr["question"].ToString();
                b.AnswerTypeID = int.Parse(dr["fkidanswertype"].ToString());
                b.AnswerType = dr["answertype"].ToString();
                b.QuestionAndType = dr["questionandtype"].ToString();
                b.Mandatory = bool.Parse(dr["mandatory"].ToString());
                
                bool exists = false;
                foreach (StudioMResource.Question prod in SelectedQuestion)
                {
                    if (b.QuestionID == prod.QuestionID)
                    {
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    StudioMQuestion.Add(b);
                }
            }

        }
        public void SearchAvailableProducts(int stateid, string productid, string productname)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StudioM_GetStudioMProduct(stateid, productid, productname);
            AvailableProduct.Clear();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Product p = new Product();
                p.ProductID = dr["productid"].ToString();
                p.ProductName = dr["productname"].ToString();
                //p.ProductDescription = dr["productdescription"].ToString();
                bool exists = false;
                foreach (Product prod in SelectedProduct)
                {
                    if (p.ProductID == prod.ProductID)
                    {
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    AvailableProduct.Add(p);
                }
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
                StudioMResource.Answer a = new StudioMResource.Answer();
                a.AnswerID = int.Parse(dr["idtemplateanswer"].ToString());
                a.AnswerText = dr["answer"].ToString();

                bool exists = false;
                foreach (StudioMResource.Answer prod in SelectedAnswer)
                {
                    if (a.AnswerID == prod.AnswerID)
                    {
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    StudioMAnswer.Add(a);
                }
            }
        }

        public bool BulkConfigureStudioMQandA(string supplierbrandid, string productidstring, string questionidstring, string answeridstring, string usercode)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            bool result = client.SQSAdmin_StudioM_BulkConfigureStudiomQandA(supplierbrandid,productidstring,questionidstring,answeridstring, usercode);
            client.Close();

            return result;
 
        }        
        
        #endregion

        #region data contract
        public ObservableCollection<CommonResource.Brand> SQSBrand
        {
            get
            {
                return _brand;
            }
            set
            {
                _brand = value;
                OnPropertyChanged("SQSBrand");
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
        public ObservableCollection<Product> SelectedProduct
        {
            get
            {
                return _selectedproduct;
            }
            set
            {
                _selectedproduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }

        public ObservableCollection<Product> TempSelectedProduct
        {
            get
            {
                return _tempselectedproduct;
            }
            set
            {
                _tempselectedproduct = value;
                OnPropertyChanged("TempSelectedProduct");
            }
        }
        public ObservableCollection<Product> AvailableProduct
        {
            get
            {
                return _availableproduct;
            }
            set
            {
                _availableproduct = value;
                OnPropertyChanged("AvailableProduct");
            }
        }

        public ObservableCollection<StudioMResource.Question> StudioMQuestion
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
        public ObservableCollection<StudioMResource.Question> SelectedQuestion
        {
            get
            {
                return _selectedquestion;
            }
            set
            {
                _selectedquestion = value;
                OnPropertyChanged("SelectedQuestion");
            }
        }

        public ObservableCollection<StudioMResource.Question> TempSelectedQuestion
        {
            get
            {
                return _tempselectedquestion;
            }
            set
            {
                _tempselectedquestion = value;
                OnPropertyChanged("TempSelectedQuestion");
            }
        }

        public ObservableCollection<StudioMResource.Answer> StudioMAnswer
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
        public ObservableCollection<StudioMResource.Answer> SelectedAnswer
        {
            get
            {
                return _selectedanswer;
            }
            set
            {
                _selectedanswer = value;
                OnPropertyChanged("SelectedAnswer");
            }
        }
        public ObservableCollection<StudioMResource.Answer> TempSelectedAnswer
        {
            get
            {
                return _tempselectedanswer;
            }
            set
            {
                _tempselectedanswer = value;
                OnPropertyChanged("TempSelectedAnswer");
            }
        }
        #endregion

    }
}
