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
    public class ManagementResource : ViewModelBase
    {
        public int stateid = 1;
        public int defaultstate;
        public string maxlengthstring;
        private ObservableCollection<Supplier> _supplier = new ObservableCollection<Supplier>();
        private List<Supplier> _tempsupplier = new List<Supplier>();
        private ObservableCollection<State> _state = new ObservableCollection<State>();
        private ObservableCollection<State> _state2 = new ObservableCollection<State>();
        private ObservableCollection<Question> _question = new ObservableCollection<Question>();
        private ObservableCollection<AnswerType> _answertype = new ObservableCollection<AnswerType>();
        private ObservableCollection<Answer> _answer = new ObservableCollection<Answer>();
        private SQSAdminServiceClient client;
        public PagingCollectionView _supplier2;
        public int pagesize = 36;

        public ManagementResource(int pstateid)
        {
            DefaultStateID = pstateid;
            stateid = pstateid;
            //client = new SQSAdminServiceClient();
            LoadState();
            LoadAnswerType();
            maxlengthstring = "Max length 1000 charaters.";
        }

        public void LoadSuppliers(int stateid, string suppliername, int active)
        {
            int tempsize;
            StudioMSupplier.Clear();
            _tempsupplier.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StudioM_GetSuppliers(stateid,suppliername, active);
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Supplier b = new Supplier();
                b.SupplierID = int.Parse(dr["idstudiomsupplier"].ToString());
                b.SupplierName = dr["suppliername"].ToString();
                b.Active = bool.Parse(dr["active"].ToString());
                b.StateID = int.Parse(dr["fkidstate"].ToString());
                b.StateName = dr["statename"].ToString();
                StudioMSupplier.Add(b);
                _tempsupplier.Add(b);
            }
            if (ds.Tables[0].Rows.Count > pagesize)
            {
                tempsize = pagesize;
            }
            else
            {
                tempsize = ds.Tables[0].Rows.Count;
            }
            StudioMSupplier2 = new PagingCollectionView(_tempsupplier, tempsize);
        }

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

        public void LoadAnswerType()
        {
            AnswerType s;
            StudioMAnswerType.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StudioM_GetAnswerType();
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                s = new AnswerType();
                s.AnswerTypeID = int.Parse(dr["idanswertype"].ToString());
                s.AnswerTypeText = dr["answertype"].ToString();
                StudioMAnswerType.Add(s);
            }
        }


        public void LoadAnswer(string questiontext, string answertext, int active, int pstateid)
        {

            Answer s;
            StudioMAnswer.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StudioM_GetAnswerList(questiontext, answertext,active, pstateid);
            client.Close();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                s = new Answer();
                s.AnswerID = int.Parse(dr["idtemplateanswer"].ToString());
                s.AnswerText = dr["answer"].ToString();
                s.QuestionID = int.Parse(dr["fkidtemplatequestion"].ToString());
                s.QuestionText = dr["question"].ToString();
                s.AnswerTypeText = dr["answertype"].ToString();
                s.Active = bool.Parse(dr["active"].ToString());
                StudioMAnswer.Add(s);
            }

            LoadQuestions("", 1, pstateid);
        }


        public void LoadQuestions(string question, int active, int pstateid)
        {
            Question s;
            StudioMQuestion.Clear();

            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StudioM_GetQuestionList(question, active, pstateid);
            client.Close();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                s = new Question();
                s.QuestionID = int.Parse(dr["idtemplatequestion"].ToString());
                s.QuestionText = dr["question"].ToString();
                s.AnswerTypeID = int.Parse(dr["fkidanswertype"].ToString());
                s.AnswertypeText = dr["answerType"].ToString();
                s.Active = bool.Parse(dr["active"].ToString());
                s.QuestionAndType = dr["questionandtype"].ToString();
                s.QuestionStateID = int.Parse(dr["fkidstate"].ToString());
                s.Mandatory = bool.Parse(dr["Mandatory"].ToString());
                StudioMQuestion.Add(s);
            }
        }

        public bool SupplierExists(int pstateid, string suppliername)
        {
            bool result;
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            result= client.SQSAdmin_StudioM_IsSupplierExists(pstateid, suppliername);
            client.Close();
            return result;
        }

        public bool QuestionExists(int panswertypeid, string questiontext)
        {
            bool result;
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            result= client.SQSAdmin_StudioM_IsQuestionExists(panswertypeid, questiontext, stateid);
            client.Close();
            return result;
        }

        public bool AnswerExists(int questionid, string answertext)
        {
            bool result;
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            result= client.SQSAdmin_StudioM_IsAnswerExists(questionid, answertext);
            client.Close();
            return result;
        }


        public void SaveSupplier(Supplier s)
        {
            int active = 0;
            if (s.Active) active = 1;
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            client.SQSAdmin_StudioM_AddEditSupplier(s.SupplierID, s.StateID, s.SupplierName, active);
            client.Close();

        }

        public void SaveQuestion(Question q)
        {
            int active = 0;
            int mandatory = 0;
            if (q.Active) active = 1;
            if (q.Mandatory) mandatory = 1;
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            client.SQSAdmin_StudioM_AddEditQuestion(q.QuestionID, q.AnswerTypeID, q.QuestionText, active, mandatory, q.QuestionStateID);
            client.Close();

        }

        public void SaveAnswer(Answer s)
        {
            int active = 0;
            if (s.Active) active = 1;
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            client.SQSAdmin_StudioM_AddEditAnswer(s.QuestionID, s.AnswerID, s.AnswerText, active);
            client.Close();

        }

        #region data contract

        public class Supplier
        {
            public int SupplierID { get; set; }
            public string SupplierName { get; set; }
            public int StateID { get; set; }
            public string StateName { get; set; }
            public bool Active { get; set; }
        }

        public class State
        {
            public int StateID { get; set; }
            public string StateAbbreviation { get; set; }
        }
        public class Question
        {
            public int QuestionID { get; set; }
            public string QuestionText { get; set; }
            public int AnswerTypeID { get; set; }
            public string AnswertypeText { get; set; }
            public bool Active { get; set; }
            public string QuestionAndType { get; set; }
            public int QuestionStateID { get; set; }
            public bool Mandatory { get; set; }
        }
        public class AnswerType
        {
            public int AnswerTypeID { get; set; }
            public string AnswerTypeText { get; set; }
        }
        public class Answer
        {
            public int AnswerID { get; set; }
            public string AnswerText { get; set; }
            public int QuestionID { get; set; }
            public string QuestionText { get; set; }
            public string AnswerTypeText { get; set; }
            public bool Active { get; set; }
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

        public PagingCollectionView StudioMSupplier2
        {
            get
            {
                return _supplier2;
            }
            set
            {
                _supplier2 = value;
                OnPropertyChanged("StudioMSupplier2");
            }
        }

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

        public ObservableCollection<AnswerType> StudioMAnswerType
        {
            get
            {
                return _answertype;
            }
            set
            {
                _answertype = value;
                OnPropertyChanged("StudioMAnswerType");
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
        public string MaxLength
        {
            get
            {
                return maxlengthstring;
            }
            set
            {
                maxlengthstring = value;
                OnPropertyChanged("MaxLength");
            }
        }

        #endregion
    }
}
