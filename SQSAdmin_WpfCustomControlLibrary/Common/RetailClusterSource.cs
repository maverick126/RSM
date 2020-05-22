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
    public class RetailClusterSource : ViewModelBase
    {
        public int stateid = 1;
        public int defaultstate;
        public string loginuser="";
        public string maxlengthstring;
        public int selectedretailclusterid = 0;

        private ObservableCollection<StudioMResource.Supplier> _supplier = new ObservableCollection<StudioMResource.Supplier>();
        private ObservableCollection<RetailCluster> _retailcluster = new ObservableCollection<RetailCluster>();
        private ObservableCollection<Group> _group = new ObservableCollection<Group>();
        private ObservableCollection<Group> _group2 = new ObservableCollection<Group>();
        private ObservableCollection<Group> _existinggroup = new ObservableCollection<Group>();
        private ObservableCollection<State> _state = new ObservableCollection<State>();
        private ObservableCollection<State> _state2 = new ObservableCollection<State>();
        private ObservableCollection<StudioMResource.Question> _question = new ObservableCollection<StudioMResource.Question>();
        private ObservableCollection<StudioMResource.Answer> _answer = new ObservableCollection<StudioMResource.Answer>();
        private SQSAdminServiceClient client;
        public PagingCollectionView _supplier2;
        private ObservableCollection<StudioMResource.QandAForSupplier> _qanda = new ObservableCollection<StudioMResource.QandAForSupplier>();
        private ObservableCollection<SupplierBrandResource.SupplierBrand> _supplierbrand = new ObservableCollection<SupplierBrandResource.SupplierBrand>();
        public int pagesize = 36;

        public RetailClusterSource(int pstateid, string pusercode)
        {
            DefaultStateID = pstateid;
            stateid = pstateid;
            loginuser = pusercode;
            LoadState();
            //LoadAvailableGroups("");
            //LoadExistingGroups();
            LoadExistingRetailCluster(pstateid, "");
            LoadSupplierBrand("", stateid, 1);
        }

        #region methods used

        public void LoadState()
        {
            State s;
            SQSState.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetState();
            client.Close();
            s = new State();
            s.StateID = 0;
            s.StateAbbreviation = "All";
            SQSState.Add(s);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                s = new State();
                s.StateID = int.Parse(dr["stateid"].ToString());
                s.StateAbbreviation = dr["stateAbbreviation"].ToString();
                SQSState.Add(s);
                SQSStateWithoutAll.Add(s);
            }
        }
        public void LoadQAndA(string id, string type)
        {
            StudioMResource.QandAForSupplier qa;
            StudioMQandA.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StudioM_LoadStudioMAttributeForClusterOrGroup(id, type, DefaultStateID.ToString());
            client.Close();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                qa = new StudioMResource.QandAForSupplier();
                qa.SupplierBrandID = int.Parse(dr["idStudioMSupplier"].ToString());
                qa.SupplierBrandName = dr["SupplierName"].ToString();
                qa.QuestionID = int.Parse(dr["idTemplateQuestion"].ToString());
                qa.QuestionText = dr["Question"].ToString();
                if (bool.Parse(dr["mandatory"].ToString()))
                {
                    qa.Mandatory = true;
                }
                else
                {
                    qa.Mandatory = false;
                }
                qa.AnswerType = dr["AnswerType"].ToString();
                qa.AnswerID = int.Parse(dr["idTemplateAnswer"].ToString());
                qa.AnswerText = dr["Answer"].ToString();
                StudioMQandA.Add(qa);
            }
            
        }

        public void LoadAvailableGroups(int selectedretailclusterid,string groupname)
        {
            Group s;
            AvailableGroups.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StudioM_GetAvailableGroupListForRetailCluster(selectedretailclusterid, groupname);
            client.Close();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                s = new Group();
                s.GroupID = int.Parse(dr["groupid"].ToString());
                s.GroupName = dr["groupname"].ToString();
                AvailableGroups.Add(s);
            }
        }

        public void LoadSQSGroups(string groupname)
        {
            Group s;
            SQSGroups.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StudioM_LoadSQSGroupList(groupname);
            client.Close();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                s = new Group();
                s.GroupID = int.Parse(dr["groupid"].ToString());
                s.GroupName = dr["groupname"].ToString();
                SQSGroups.Add(s);
            }
        }
        public void LoadExistingGroups(int selectedretailclusterid)
        {
            Group s;
            ExistingGroups.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StudioM_GetExistingGroupListForRetailCluster(selectedretailclusterid);
            client.Close();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                s = new Group();
                s.GroupID = int.Parse(dr["groupid"].ToString());
                s.GroupName = dr["groupname"].ToString();
                ExistingGroups.Add(s);
            }
        }

        public void LoadExistingRetailCluster(int pstateid, string clustername)
        {
            RetailCluster s;
            ExistingRetailClusters.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StudioM_GetRetailCluster(pstateid, clustername);
            client.Close();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                s = new RetailCluster();
                s.RetailClusterID = int.Parse(dr["id_StudioM_RetailCluster"].ToString());
                s.RetailClusterName = dr["retailclustername"].ToString();
                s.StateID = int.Parse(dr["stateid"].ToString());
                s.StateName = dr["stateabbreviation"].ToString();
                s.Active = bool.Parse(dr["active"].ToString());
                ExistingRetailClusters.Add(s);
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
        public void LoadSuppliers(int pstateid)
        {
            StudioMResource.Supplier b;
            StudioMSupplier.Clear();

            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StudioM_GetSuppliers(pstateid, "", 1);
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                b = new StudioMResource.Supplier();
                b.SupplierID = int.Parse(dr["idstudiomsupplier"].ToString());
                b.SupplierName = dr["suppliername"].ToString();
                StudioMSupplier.Add(b);
            }
        }

        public void LoadQuestions(int pstateid)
        {
            StudioMQuestion.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StudioM_GetActiveQuestions(pstateid);
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
                StudioMResource.Answer b = new StudioMResource.Answer();
                b.AnswerID = int.Parse(dr["idtemplateanswer"].ToString());
                b.AnswerText = dr["answer"].ToString();
                StudioMAnswer.Add(b);
            }
        }
        public bool RetailClusterExists(int stateid, string clustername)
        {
            bool result;
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            result = client.SQSAdmin_StudioM_IsRetailClusterExists(stateid, clustername);
            client.Close();
            return result;
        }

        public void SaveRetailCluster(RetailCluster q)
        {
            int active = 0;

            if (q.Active) active = 1;
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            client.SQSAdmin_StudioM_AddEditRetailCluster(q.RetailClusterID, q.RetailClusterName, q.StateID, active, loginuser);
            client.Close();

        }

        public void SaveSelectedGroupToRetailCluster(int retailclusterid, string selectedgroupuid, string usercode)
        {

            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            client.SQSAdmin_StudioM_SaveSelectedGroupToRetailCluster(retailclusterid, selectedgroupuid, usercode);
            client.Close();
        }

        public void RemoveSelectedGroupFromRetailCluster(int retailclusterid, string selectedgroupuid)
        {

            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            client.SQSAdmin_StudioM_RemoveSelectedGroupFromRetailCluster(retailclusterid, selectedgroupuid);
            client.Close();
        }

        #endregion

        public void AddStudioMAttributes(StudioMResource.QandAForSupplier qa, int index)
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
                if (index == -1) // add to the bottom
                {
                    StudioMQandA.Add(qa);
                }
                else
                {
                    StudioMQandA.Insert(index, qa);
                }
            }
        }

        public void RemoveStudioMAttributes(StudioMResource.QandAForSupplier qa)
        {
            StudioMQandA.Remove(qa);
        }


        public void SaveStudioMAttributesToClusterOrGroup(string id, string type)
        {
            int oldsupplierid = 0;
            int oldquestionid = 0;
            int oldanswerid = 0;
            string xml = "";
            string mandatory = "";
            string supplierstring="";
            string questionstring="";
            string answerstring="";
            string mandatorystring="";
            StudioMResource.QandAForSupplier qa;

            if (StudioMQandA.Count > 0)
            {
                xml = "<Brands>";

                var sortedQANDA = from item in StudioMQandA
                                  //orderby item.SupplierBrandID, item.QuestionID, item.AnswerID
                                  select item;

                for (int i = 0; i < sortedQANDA.Count<StudioMResource.QandAForSupplier>(); i++)
                {

                    qa = sortedQANDA.ElementAt<StudioMResource.QandAForSupplier>(i);

                    if (qa.Mandatory)
                    {
                        mandatory = "1";
                    }
                    else
                    {
                        mandatory = "0";
                    }

                    if (supplierstring == "")
                    {
                        supplierstring = qa.SupplierBrandID.ToString();
                        questionstring = qa.QuestionID.ToString();
                        answerstring = qa.AnswerID.ToString();
                        mandatorystring = mandatory;
                    }
                    else
                    {
                        supplierstring = supplierstring + "," + qa.SupplierBrandID.ToString();
                        questionstring = questionstring + "," + qa.QuestionID.ToString();
                        answerstring = answerstring + "," + qa.AnswerID.ToString();
                        mandatorystring = mandatorystring + "," + mandatory;
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
                            xml = xml + @"<Answer id=""" + qa.AnswerID + @""" text=""" +System.Security.SecurityElement.Escape(qa.AnswerText) + @"""/>";
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
            xml = xml.Replace("(Single Selection)","");
            xml = xml.Replace("(Multiple Selection)", "");
            xml = xml.Replace("(Free Text)", "");
            xml = xml.Replace("(Integer)", "");
            xml = xml.Replace("(Decimal)", "");

            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            client.SQSAdmin_StudioM_UpdateStudioMAttributeForClusterOrGroup(id, xml, type, loginuser, supplierstring, questionstring, answerstring, mandatorystring, DefaultStateID.ToString());
            client.Close();

        }
        #region data contract

        public class RetailCluster
        {
            public int RetailClusterID { get; set; }
            public string RetailClusterName { get; set; }
            public int StateID { get; set; }
            public string StateName { get; set; }
            public bool Active { get; set; }
        }

        public class State
        {
            public int StateID { get; set; }
            public string StateAbbreviation { get; set; }
        }

        public class Group
        {
            public int GroupID { get; set; }
            public string GroupName { get; set; }
        }


#endregion


        #region public propeties
        public ObservableCollection<State> SQSState
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                OnPropertyChanged("SQSState");
            }
        }
        public ObservableCollection<StudioMResource.Supplier> StudioMSupplier
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

        public ObservableCollection<StudioMResource.Answer> StudioMAnswer
        {
            get
            {
                return _answer;
            }
            set
            {
                _answer = value;
                OnPropertyChanged("StudioMQuestion");
            }
        }

        public ObservableCollection<Group> AvailableGroups
        {
            get
            {
                return _group;
            }
            set
            {
                _group = value;
                OnPropertyChanged("AvailableGroups");
            }
        }

        public ObservableCollection<Group> SQSGroups
        {
            get
            {
                return _group2;
            }
            set
            {
                _group2 = value;
                OnPropertyChanged("SQSGroups");
            }
        }
        public ObservableCollection<Group> ExistingGroups
        {
            get
            {
                return _existinggroup;
            }
            set
            {
                _existinggroup = value;
                OnPropertyChanged("ExistingGroups");
            }
        }

        public ObservableCollection<RetailCluster> ExistingRetailClusters
        {
            get
            {
                return _retailcluster;
            }
            set
            {
                _retailcluster = value;
                OnPropertyChanged("ExistingRetailClusters");
            }
        }

        public ObservableCollection<State> SQSStateWithoutAll
        {
            get
            {
                return _state2;
            }
            set
            {
                _state2 = value;
                OnPropertyChanged("SQSStateWithoutAll");
            }
        }
        public int DefaultStateID
        {
            get
            {
                return defaultstate;
            }
            set
            {
                defaultstate = value;
                OnPropertyChanged("DefaultStateID");
            }
        }

        public int SelectedRetailClusterID
        {
            get
            {
                return selectedretailclusterid;
            }
            set
            {
                selectedretailclusterid = value;
                OnPropertyChanged("SelectedRetailClusterID");
            }
        }

        public ObservableCollection<StudioMResource.QandAForSupplier> StudioMQandA
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
