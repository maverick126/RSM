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
    public class HomeAreaResource : ViewModelBase
    {
        private int loginstate;
        private int loginstate2;
        private int loginstate3;
        private ObservableCollection<CommonResource.Brand> _brand = new ObservableCollection<CommonResource.Brand>();
        private ObservableCollection<CommonResource.Brand> _brand2 = new ObservableCollection<CommonResource.Brand>();
        private ObservableCollection<CommonResource.Brand> _brand3 = new ObservableCollection<CommonResource.Brand>();
        private ObservableCollection<CommonResource.State> _state = new ObservableCollection<CommonResource.State>();
        private ObservableCollection<CommonResource.State> _state2 = new ObservableCollection<CommonResource.State>();
        private ObservableCollection<CommonResource.State> _state3= new ObservableCollection<CommonResource.State>();
        private ObservableCollection<CommonResource.Area> _availablearea = new ObservableCollection<CommonResource.Area>();
        private ObservableCollection<CommonResource.Area> _configuredarea = new ObservableCollection<CommonResource.Area>();
        private ObservableCollection<Home> _sqshome = new ObservableCollection<Home>();
        private ObservableCollection<Home> _sourcehome = new ObservableCollection<Home>();
        private ObservableCollection<Home> _targethome = new ObservableCollection<Home>();
        private SQSAdminServiceClient client;

        public HomeAreaResource(int stateid)
        {
            loginstate = stateid;
            LoginState = stateid;
            LoginState2 = stateid;
            LoginState3 = stateid;

            LoadState();
            LoadBrand(loginstate, "cmbState");
            LoadBrand(loginstate, "cmbState2");
            LoadBrand(loginstate, "cmbState3");

        }


        public void LoadState()
        {
            SQSState.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetState();
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                CommonResource.State s = new CommonResource.State();
                s.StateID = int.Parse(dr["stateid"].ToString());
                s.StateAbbreviation = dr["stateAbbreviation"].ToString();
                SQSState.Add(s);
                SQSState2.Add(s);
                SQSState3.Add(s);

            }
        }
        public void LoadBrand(int stateid, string dropdownname)
        {
            if (dropdownname.ToUpper() == "CMBSTATE")
            {
                SQSBrand.Clear();
            }
            else if (dropdownname.ToUpper() == "CMBSTATE2")
            {
                SQSBrand2.Clear();
            }
            else if (dropdownname.ToUpper() == "CMBSTATE3")
            {
                SQSBrand3.Clear();
            }
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetBrandByState(stateid);
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                CommonResource.Brand b = new CommonResource.Brand();
                b.BrandID = int.Parse(dr["brandid"].ToString());
                b.BrandName = dr["brandname"].ToString();
                if (dropdownname.ToUpper() == "CMBSTATE")
                {
                    SQSBrand.Add(b);
                }
                else if (dropdownname.ToUpper() == "CMBSTATE2")
                {
                    SQSBrand2.Add(b);
                }
                else if (dropdownname.ToUpper() == "CMBSTATE3")
                {
                    SQSBrand3.Add(b);
                }
            }
        }

        public void LoadHome(int pstateid, int brandid, string homename)
        {
            SQSHome.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetHomeNameBySearch(pstateid, brandid, homename);
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Home b = new Home();
                b.HomeID = int.Parse(dr["homeid"].ToString());
                b.HomeName = dr["homename"].ToString();
                b.Configured = bool.Parse(dr["min_area"].ToString());
                SQSHome.Add(b);
            }

        }
        public void LoadSourceHome(int brandid, string homename)
        {
            SQSSourceHome.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_HomeMinimumArea_GetSourceHomeByBrand(brandid, homename);
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Home b = new Home();
                b.HomeID = int.Parse(dr["homeid"].ToString());
                b.HomeName = dr["homename"].ToString();
                b.Configured = true;
                SQSSourceHome.Add(b);
            }

        }
        public void LoadTargetHome(int brandid, string homename)
        {
            SQSTargetHome.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_HomeMinimumArea_GetTargetHomeByBrand(brandid, homename);
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Home b = new Home();
                b.HomeID = int.Parse(dr["homeid"].ToString());
                b.HomeName = dr["homename"].ToString();
                b.Configured = false;
                SQSTargetHome.Add(b);
            }

        }
        public void LoadAvailableAreasForHome(int homeid)
        {
            AvailableAreas.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetAvailabeAreasByHome(homeid);
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                CommonResource.Area b = new CommonResource.Area();
                b.AreaID = int.Parse(dr["areaid"].ToString());
                b.AreaName = dr["areaname"].ToString();
                AvailableAreas.Add(b);
            }

        }
        public void LoadConfiguredAreasForHome(int homeid)
        {
            ConfiguredAreas.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetConfiguredAreasByHome(homeid);
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                CommonResource.Area b = new CommonResource.Area();
                b.AreaID = int.Parse(dr["areaid"].ToString());
                b.AreaName = dr["areaname"].ToString();
                ConfiguredAreas.Add(b);
            }

        }

        public void AddSelectedAreasToHome(int homeid, string selectedareaid, string usercode)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            client.SQSAdmin_Generic_AddMinimumAreasToHome(homeid, selectedareaid,usercode);
            client.Close();
        }
        public void RemoveMinimumAreasToHome(int homeid, string selectedareaid, string usercode)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            client.SQSAdmin_Generic_RemoveMinimumAreasFromHome(homeid, selectedareaid, usercode);
            client.Close();
        }
        public void CopyMinimumAreasFromSourceToTargetHomes(string sourcehomeid, string targethomeidstring, string usercode)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            client.SQSAdmin_HomeMinimumArea_CopyMinimumAreasFromSourceHomeToTargetHome(sourcehomeid, targethomeidstring, usercode);
            client.Close();


        }
        #region data contract
        public class Home
        {
            public int HomeID { get; set; }
            public string HomeName { get; set; }
            public bool Configured { get; set; }
        }
        #endregion

        #region public properties
        public int LoginState
        {
            get
            {
                return loginstate;
            }
            set
            {
                loginstate = value;
                OnPropertyChanged("LoginState");
            }
        }
        public int LoginState2
        {
            get
            {
                return loginstate2;
            }
            set
            {
                loginstate2 = value;
                OnPropertyChanged("LoginState2");
            }
        }
        public int LoginState3
        {
            get
            {
                return loginstate3;
            }
            set
            {
                loginstate3 = value;
                OnPropertyChanged("LoginState3");
            }
        }
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
        public ObservableCollection<CommonResource.Brand> SQSBrand2
        {
            get
            {
                return _brand2;
            }
            set
            {
                _brand2 = value;
                OnPropertyChanged("SQSBrand2");
            }
        }
        public ObservableCollection<CommonResource.Brand> SQSBrand3
        {
            get
            {
                return _brand3;
            }
            set
            {
                _brand3 = value;
                OnPropertyChanged("SQSBrand3");
            }
        }
        public ObservableCollection<CommonResource.State> SQSState
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
        public ObservableCollection<CommonResource.State> SQSState2
        {
            get
            {
                return _state2;
            }
            set
            {
                _state2 = value;
                OnPropertyChanged("SQSState2");
            }
        }
        public ObservableCollection<CommonResource.State> SQSState3
        {
            get
            {
                return _state3;
            }
            set
            {
                _state3 = value;
                OnPropertyChanged("SQSState3");
            }
        }
        public ObservableCollection<Home> SQSHome
        {
            get
            {
                return _sqshome;
            }
            set
            {
                _sqshome = value;
                OnPropertyChanged("SQSHome");
            }
        }

        public ObservableCollection<Home> SQSSourceHome
        {
            get
            {
                return _sourcehome;
            }
            set
            {
                _sourcehome = value;
                OnPropertyChanged("SQSSourceHome");
            }
        }
        public ObservableCollection<Home> SQSTargetHome
        {
            get
            {
                return _targethome;
            }
            set
            {
                _targethome = value;
                OnPropertyChanged("SQSTargetHome");
            }
        }

        public ObservableCollection<CommonResource.Area> AvailableAreas
        {
            get
            {
                return _availablearea;
            }
            set
            {
                _availablearea = value;
                OnPropertyChanged("AvailableAreas");
            }
        }

        public ObservableCollection<CommonResource.Area> ConfiguredAreas
        {
            get
            {
                return _configuredarea;
            }
            set
            {
                _configuredarea = value;
                OnPropertyChanged("ConfiguredAreas");
            }
        }
        #endregion

    }
}
