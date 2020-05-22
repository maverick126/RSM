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

    public class SupplierBrandResource: ViewModelBase
    {
        public int stateid = 1;
        public int defaultstate;
        public string maxlengthstring;
        private ObservableCollection<SupplierBrand> _supplierbrand = new ObservableCollection<SupplierBrand>();

        private ObservableCollection<State> _state = new ObservableCollection<State>();
        private ObservableCollection<State> _state2 = new ObservableCollection<State>();

        private SQSAdminServiceClient client;
        public PagingCollectionView _supplier2;
        public int pagesize = 36;

        public SupplierBrandResource(int pstateid)
        {
            defaultstate = pstateid;
            stateid = pstateid;
            LoadState();
            
        }

        public void LoadState()
        {
            State s;
            SQSState.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetState();
            client.Close();
            //s = new State();
            //s.StateID = 0;
            //s.StateAbbreviation = "All";
            //SQSState.Add(s);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                s = new State();
                s.StateID = int.Parse(dr["stateid"].ToString());
                s.StateAbbreviation = dr["stateAbbreviation"].ToString();
                SQSState.Add(s);
                SQSStateWithoutAll.Add(s);
            }
        }

        public void LoadSupplierBrand(string brandname, int pstateid, int active)
        {
            SupplierBrand s;
            SQSSupplierBrand.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StudioM_GetSupplierBrand(brandname, pstateid, active);
            client.Close();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                s = new SupplierBrand();
                s.SupplierBrandID = int.Parse(dr["id_studiom_supplierbrand"].ToString());
                s.SupplierBrandName = dr["supplierbrandname"].ToString();
                s.Active = bool.Parse(dr["active"].ToString());
                s.BrandStateID = int.Parse(dr["fkidstate"].ToString());
                s.BrandStateName = dr["stateAbbreviation"].ToString();
                SQSSupplierBrand.Add(s);
            }
        }

        public bool SupplierBrandExists(string brandname, int pstateid)
        {
            bool result;

            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            result = client.SQSAdmin_StudioM_CheckSupplierBrandExists(brandname,pstateid);
            client.Close();
            return result;
        }
        public void SaveSupplierBrand(SupplierBrandResource.SupplierBrand s)
        {
            int active = 0;
            if (s.Active)
            {
                active = 1;
            }
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            client.SQSAdmin_StudioM_AddEditSupplierBrand(s.SupplierBrandID, s.SupplierBrandName, s.BrandStateID, active, CommonVariables.UserCode);
            client.Close();
        }

        #region properties
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

        public ObservableCollection<SupplierBrand> SQSSupplierBrand
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
        #endregion

        #region data contract

        public class SupplierBrand
        {
            public int SupplierBrandID { get; set; }
            public string SupplierBrandName { get; set; }
            public int BrandStateID { get; set; }
            public string BrandStateName { get; set; }
            public bool Active { get; set; }
        }

        public class State
        {
            public int StateID { get; set; }
            public string StateAbbreviation { get; set; }
        }


        #endregion
    }




}
