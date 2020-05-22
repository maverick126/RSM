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
    public class RelatedAreasSource : ViewModelBase
    {
        public int stateid;
        public int defaultstate;
        public string loginuser = "";
        private ObservableCollection<RetailClusterSource.State> _state = new ObservableCollection<RetailClusterSource.State>();
        private ObservableCollection<CommonResource.Area> _area = new ObservableCollection<CommonResource.Area>();
        private ObservableCollection<CommonResource.Area> _area2 = new ObservableCollection<CommonResource.Area>();
        private ObservableCollection<CommonResource.Area> _excludearea = new ObservableCollection<CommonResource.Area>();
        private ObservableCollection<CommonResource.Area> _existingarea = new ObservableCollection<CommonResource.Area>();
        private ObservableCollection<Product> _product = new ObservableCollection<Product>();
        private SQSAdminServiceClient client;
        private string _selectedproductid="";
        public RelatedAreasSource(int pstate, string pusercode)
        {
            stateid = pstate;
            DefaultStateID = pstate;
            loginuser = pusercode;
            LoadState();
        }
        #region methods
        public void LoadState()
        {
            RetailClusterSource.State s;
            SQSState.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetState();
            client.Close();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                s = new RetailClusterSource.State();
                s.StateID = int.Parse(dr["stateid"].ToString());
                s.StateAbbreviation = dr["stateAbbreviation"].ToString();
                SQSState.Add(s);
            }
        }

        public void LoadProduct(int stateid, string productid, string keyword)
        {
            Product s;
            SQSProduct.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_RelatedArea_LoadProduct(productid,keyword,stateid);
            client.Close();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                s = new Product();
                s.ProductID = dr["productid"].ToString();
                s.ProductName = dr["productname"].ToString();
                s.ProductDescription = dr["productdescription"].ToString();
                SQSProduct.Add(s);
            }
        }

        public void LoadAvailableAreasForProduct(string productid, string keyword, string callfrom)
        {
            CommonResource.Area s;
            AvailableAreas.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_RelatedArea_LoadAvailableAreasForProduct(productid,keyword, callfrom);
            client.Close();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                s = new CommonResource.Area();
                s.AreaID = int.Parse(dr["areaid"].ToString());
                s.AreaName = dr["areaname"].ToString();
                AvailableAreas.Add(s);
            }
        }
        public void LoadExistingAreasForProduct(string productid, int active)
        {
            CommonResource.Area s;
            ExistingAreas.Clear();
            ExcludedAreas.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_RelatedArea_LoadExistingAreasForProduct(productid,active);
            client.Close();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                s = new CommonResource.Area();
                if (dr["validateinstudiom"] != null && (dr["validateinstudiom"].ToString() == "1" || dr["validateinstudiom"].ToString().ToUpper() == "TRUE"))
                {
                    s.AreaID = int.Parse(dr["areaid"].ToString());
                    s.AreaName = dr["areaname"].ToString();
                    ExistingAreas.Add(s);
                }
                else
                {
                    s.AreaID = int.Parse(dr["areaid"].ToString());
                    s.AreaName = dr["areaname"].ToString();
                    ExcludedAreas.Add(s);
                }
            }
        }

        public void SaveRelatedAreaToProduct(string productid, string areaidstring, string usercode, string callfrom)
        {

            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            client.SQSAdmin_RelatedArea_AddSelectedAreasToProduct(productid,areaidstring,usercode, callfrom);
            client.Close();

        }

        public void RemoveRelatedAreaFromProduct(string productid, string areaidstring, string callfrom)
        {

            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            client.SQSAdmin_RelatedArea_RemooveSelectedAreasFromProduct(productid, areaidstring, callfrom);
            client.Close();

        }
        #endregion
        #region properties
        public ObservableCollection<CommonResource.Area> AvailableAreas
        {
            get
            {
                return _area;
            }
            set
            {
                _area = value;
                OnPropertyChanged("AvailableAreas");
            }
        }

        public ObservableCollection<CommonResource.Area> SQSAreas
        {
            get
            {
                return _area2;
            }
            set
            {
                _area2 = value;
                OnPropertyChanged("SQSAreas");
            }
        }
        public ObservableCollection<CommonResource.Area> ExistingAreas
        {
            get
            {
                return _existingarea;
            }
            set
            {
                _existingarea = value;
                OnPropertyChanged("ExistingAreas");
            }
        }
        public ObservableCollection<CommonResource.Area> ExcludedAreas
        {
            get
            {
                return _excludearea;
            }
            set
            {
                _excludearea = value;
                OnPropertyChanged("ExcludedAreas");
            }
        }
        public ObservableCollection<Product> SQSProduct
        {
            get
            {
                return _product;
            }
            set
            {
                _product = value;
                OnPropertyChanged("SQSProduct");
            }
        }
        public ObservableCollection<RetailClusterSource.State> SQSState
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

        public string SelectedProductID
        {
            get
            {
                return _selectedproductid;
            }
            set
            {
                _selectedproductid = value;
                OnPropertyChanged("SelectedProductID");
            }
        }
        #endregion
    }

    #region
    public class Product
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
    }
    #endregion


}
