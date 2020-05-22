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
namespace SQSAdmin_WpfCustomControlLibrary.Common
{
    
    class StandardInclusionResource: ViewModelBase
    {
        private ObservableCollection<GenericProduct> _availableproduct = new ObservableCollection<GenericProduct>();
        private ObservableCollection<GenericProduct> _upgradeproduct = new ObservableCollection<GenericProduct>();
        private ObservableCollection<GenericProduct> _promotionproduct = new ObservableCollection<GenericProduct>();
        private ObservableCollection<GenericProduct> _standardinclusion = new ObservableCollection<GenericProduct>();
        private ObservableCollection<Brand> _brand = new ObservableCollection<Brand>();
        private ObservableCollection<Brand> _brandwithall = new ObservableCollection<Brand>();
        private ObservableCollection<RegionGroup> _regiongorup = new ObservableCollection<RegionGroup>();
        private ObservableCollection<RegionGroup> _regiongorupwithall = new ObservableCollection<RegionGroup>();
        private ObservableCollection<ProductAreaGroup> _pag = new ObservableCollection<ProductAreaGroup>();
        private ProductAreaGroup _selectedpag = new ProductAreaGroup();
        private ObservableCollection<StandardInclusionPAG> _si = new ObservableCollection<StandardInclusionPAG>();
        private StandardInclusionPAG _selectedstandardinclusion = new StandardInclusionPAG();
        private ObservableCollection<StandardInclusionPAG> _tempselectedsi = new ObservableCollection<StandardInclusionPAG>();
        private ObservableCollection<Group> _group = new ObservableCollection<Group>();
        private ObservableCollection<Area> _area = new ObservableCollection<Area>();

        public int loginstateid;
        public SQSAdminServiceClient client;
        public StandardInclusionResource(int stateid)
        {
            loginstateid = stateid;
            //client = new SQSAdminServiceClient();
            LoadBrand(loginstateid);
        }

        public void LoadBrand(int stateid)
        {
            SQSBrand.Clear();
            SQSBrandWithAll.Clear();
            ItemsBrand = new Dictionary<string, object>();
            SelectedItemsBrand = new Dictionary<string, object>();
            Brand b = new Brand();
            b.BrandID = 0;
            b.BrandName = "All";
            SQSBrandWithAll.Add(b);

            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetBrandByState(stateid);
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                b = new Brand();
                b.BrandID = int.Parse(dr["brandid"].ToString());
                b.BrandName = dr["brandname"].ToString();
                SQSBrand.Add(b);
                SQSBrandWithAll.Add(b);
                ItemsBrand.Add(b.BrandName, b.BrandID);
            }
        }
        public void GetStandardInclusion(string productid, string productname, int pstateid, string brandids)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StandardInclusion_GetStandardInclusionProducts(productid, productname, pstateid, brandids);
            client.Close();
            StandarInclusionProducts.Clear();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                GenericProduct p = new GenericProduct();
                p.BrandID = int.Parse(dr["BrandID"].ToString());
                p.BrandName = dr["BrandName"].ToString();
                p.ProductID = dr["productid"].ToString();
                p.ProductName = dr["productname"].ToString();
                p.ProductDescription = dr["productdescription"].ToString();
                p.validationruleID = 0;

                StandarInclusionProducts.Add(p);
            }
            UpgradeOptionProducts.Clear();
            PromotionProducts.Clear();
        }

        public void GetUpgradeForStandardInclusion(string productid, string pbrandids)
        {
            DateTime dateValue = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StandardInclusion_GetUpgradeOptionByStandardInclusion(productid, pbrandids);
            client.Close();
            UpgradeOptionProducts.Clear();
            PromotionProducts.Clear();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                GenericProduct p = new GenericProduct();
                p.BrandID = int.Parse(dr["brandid"].ToString());
                p.BrandName = dr["brandname"].ToString();
                p.ProductID = dr["productid"].ToString();
                p.ProductName = dr["productname"].ToString();
                p.ProductDescription = dr["productdescription"].ToString();
                p.validationruleID = int.Parse(dr["idStudioM_Inclusionvalidationrule"].ToString());
                p.Promotion = bool.Parse(dr["Promotion"].ToString());
                DateTime.TryParse(dr["EffectiveDate"].ToString(), out dateValue);
                if (dateValue != (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue)
                {
                    p.EffectiveDate = dateValue.AddHours(3);
                }
                p.Active = bool.Parse(dr["active"].ToString());
                if (p.Promotion)
                {
                    PromotionProducts.Add(p);
                }
                else if (p.Active)
                { 
                    // upgrade products show only the active items
                    UpgradeOptionProducts.Add(p);
                }
            }
        }

        public void GetAvailableUpgradeOptionProducts(string productid, string productname, string brandids, int pstateid, string stdinclusionproductid)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StandardInclusion_GetAvailableUpgradeOptionProducts(productid, productname, brandids, pstateid, stdinclusionproductid);
            client.Close();
            AvailableOptionProducts.Clear();
            if (ds != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    GenericProduct p = new GenericProduct();
                    p.BrandID = 0;
                    p.BrandName = "";
                    p.ProductID = dr["productid"].ToString();
                    p.ProductName = dr["productname"].ToString();
                    p.ProductDescription = dr["productdescription"].ToString();
                    p.validationruleID = 0;

                    AvailableOptionProducts.Add(p);
                }
            }
        }
        public void RemoveValidationRule(GenericProduct p, bool promotion, bool update = false, bool active = true)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            client.SQSAdmin_StandardInclusion_RemoveValidationRule(p.validationruleID, update, active);
            client.Close();
            if (promotion && !update)
            {
                PromotionProducts.Remove(p);
            }
            else
            { 
                UpgradeOptionProducts.Remove(p);
            }
        }

        public void SaveValidationRule(string standardinclusionproductid, string upgradeoptionproductids, string brandids, string usercode, bool promotion, DateTime effectivedate)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            client.SQSAdmin_StandardInclusion_SaveValidationRule(standardinclusionproductid, upgradeoptionproductids, brandids, usercode, promotion, effectivedate);
            client.Close();
        }

        //public void LoadPAGsFromProduct(string productid)
        //{
        //    client = new SQSAdminServiceClient();
        //    client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
        //    DataSet ds= client.SQSAdmin_StandardInclusion_GetPAGFromProduct(productid);
        //    client.Close();

        //    SQSProductAreaGroup.Clear();
        //    ProductAreaGroup pg = new ProductAreaGroup();
        //    pg.ProductAreaGroupID = 0;
        //    pg.ProductAreaGroupName = "Please Select...";
        //    SQSProductAreaGroup.Add(pg);

        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        pg = new ProductAreaGroup();
        //        pg.ProductAreaGroupID = int.Parse(dr["productareagroupid"].ToString());
        //        pg.ProductAreaGroupName = dr["productareagroupid"].ToString();
        //        SQSProductAreaGroup.Add(pg);
        //    }

        //}
        public void LoadAvailableStandardInclusionPAGsFromMasterList(string productid, string productname, int areaid, int groupid, int stateid)
        {
            DateTime dateSelected = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;

            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StandardInclusion_GetAvailableStandardInclusionPAGFromMasterList(areaid, groupid, productid, productname, stateid);
            client.Close();

            SQSProductAreaGroup.Clear();
            ProductAreaGroup pg;

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            { 
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    pg = new ProductAreaGroup();
                    pg.ProductAreaGroupID = int.Parse(dr["productareagroupid"].ToString());
                    pg.AreaID = int.Parse(dr["areaid"].ToString());
                    pg.GroupID = int.Parse(dr["groupid"].ToString());
                    pg.AreaName = dr["areaname"].ToString();
                    pg.GroupName = dr["groupname"].ToString();
                    pg.ProductID = dr["productid"].ToString();
                    pg.ProductName = dr["productname"].ToString();
                    pg.ProductDescription = dr["productdescription"].ToString();
                    pg.ProductIDName = dr["productid"].ToString() + " - " + dr["productname"].ToString();
                    SQSProductAreaGroup.Add(pg);
                }
            }
        }

        public void LoadAvailableStandardInclusionPAGs(string productid, string productname, int areaid, int groupid, int pbrandid, int pregiongroupid)
        {
            DateTime dateSelected = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;

            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StandardInclusion_GetAvailableStandardInclusionPAG(areaid, groupid, productid, productname, pbrandid, pregiongroupid);
            client.Close();

            SQSProductAreaGroup.Clear();
            ProductAreaGroup pg;

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    pg = new ProductAreaGroup();
                    pg.ProductAreaGroupID = int.Parse(dr["productareagroupid"].ToString());
                    pg.AreaID = int.Parse(dr["areaid"].ToString());
                    pg.GroupID = int.Parse(dr["groupid"].ToString());
                    pg.AreaName = dr["areaname"].ToString();
                    pg.GroupName = dr["groupname"].ToString();
                    pg.ProductID = dr["productid"].ToString();
                    pg.ProductName = dr["productname"].ToString();
                    pg.ProductDescription = dr["productdescription"].ToString();
                    pg.ProductIDName = dr["productid"].ToString() + " - " + dr["productname"].ToString();
                    DateTime.TryParse(dr["EffectiveDate"].ToString(), out dateSelected);
                    if (dateSelected != (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue)
                    {
                        pg.EffectiveDate = dateSelected.AddHours(3).ToString("dd/MM/yyyy");
                    }
                    SQSProductAreaGroup.Add(pg);
                }
            }
        }

        public void LoadArea()
        {
            SQSArea.Clear();
            Area b = new Area();
            b.AreaID = 0;
            b.AreaName = "All";
            SQSArea.Add(b);

            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetAllAreas();
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                b = new Area();
                b.AreaID = int.Parse(dr["areaid"].ToString());
                b.AreaName = dr["areaname"].ToString();
                SQSArea.Add(b);

            }
        }
        public void LoadGroup()
        {
            SQSGroup.Clear();
            Group b = new Group();
            b.GroupID = 0;
            b.GroupName = "All";
            SQSGroup.Add(b);

            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetAllGroups();
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                b = new Group();
                b.GroupID = int.Parse(dr["groupid"].ToString());
                b.GroupName = dr["groupname"].ToString();
                SQSGroup.Add(b);

            }
        }
        public void LoadRegionGroupFromState(int pstateid, bool includeRegional = false)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StandardInclusion_GetRegionGroupByState(pstateid);
            client.Close();

            SQSRegionGroup.Clear();
            SQSRegionGroupWithAll.Clear();
            RegionGroup rg = null;
            if (pstateid == 1)
            {
                rg = new RegionGroup();
                rg.RegionGroupID = 0;
                rg.RegionGroupName = "All Regions";
                SQSRegionGroupWithAll.Add(rg);
            }
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                rg = new RegionGroup();
                rg.RegionGroupID = int.Parse(dr["idregiongroup"].ToString());
                rg.RegionGroupName = dr["regiongroupname"].ToString();
                SQSRegionGroup.Add(rg);
                SQSRegionGroupWithAll.Add(rg);
            }
            if (pstateid == 1 && includeRegional)
            {
                rg = new RegionGroup();
                rg.RegionGroupID = 12;
                rg.RegionGroupName = "Regional";
                SQSRegionGroupWithAll.Insert(2, rg);
            }
        }

        public void LoadAllStandardInclusions(int pbrandid, int pregiongroupid, int pagid, string productid, string productname)
        {
            DateTime dateSelected = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;

            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StandardInclusion_GetStandardInclusions(pbrandid, pregiongroupid, pagid, loginstateid, productid, productname);
            client.Close();

            SQSAllStandardInclusions.Clear();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                StandardInclusionPAG p = new StandardInclusionPAG();
                p.SIBrandID = int.Parse(dr["brandid"].ToString());
                p.SIBrandName = dr["brandname"].ToString();
                p.PAGID = int.Parse(dr["productareagroupid"].ToString());
                p.ProductID = dr["productid"].ToString();
                p.ProductName = dr["productname"].ToString();
                p.ProductDescription = dr["productdescription"].ToString();
                p.Quantity = decimal.Parse(dr["quantity"].ToString());
                p.Active = bool.Parse(dr["active"].ToString());
                p.StandardInclusionID = int.Parse(dr["idstandardinclusions"].ToString());
                p.AreaName = dr["areaname"].ToString();
                p.GroupName = dr["groupname"].ToString();
                p.SIRegionGroupID = int.Parse(dr["regiongroupid"].ToString());
                p.SIRegionGroupName = dr["regiongroupname"].ToString();
                DateTime.TryParse(dr["EffectiveDate"].ToString(), out dateSelected);
                if (dateSelected != (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue)
                {
                    p.EffectiveDate = dateSelected.AddHours(3).ToString("dd/MM/yyyy");
                }
                SQSAllStandardInclusions.Add(p);
            }

        }

        public bool StandardInclusionExists(int pagid, int pbrandid, int pregiongroupid, int standardinclusionid)
        {
            bool result;
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            result = client.SQSAdmin_StandardInclusion_IsStandardInclusionExists(pagid, pbrandid, pregiongroupid, standardinclusionid);
            client.Close();
            return result;
        }

        public void RemoveStandardInclusion(StandardInclusionResource.StandardInclusionPAG s)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            client.SQSAdmin_StandardInclusion_RemoveStandardInclusion(s.StandardInclusionID);
            client.Close();

        }
        
        public void SaveStandardInclusions(StandardInclusionResource.StandardInclusionPAG s)
        {
            int active = 0;
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            if (s.Active)
            {
                active = 1;
            }

            client.SQSAdmin_StandardInclusion_AddEditStandardInclusion(s.StandardInclusionID,s.PAGID, s.SIBrandID, s.SIRegionGroupID, s.Quantity, active, CommonVariables.UserCode);
            client.Close();

        }

        public void SaveStandardInclusionToBrand(string selectedids, string brandids, int regiongroupid, string usercode, DateTime effectivedate)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            client.SQSAdmin_StandardInclusion_SaveStandardInclusionToBrand(selectedids, brandids, regiongroupid, usercode, effectivedate);
            client.Close();

        }

        #region properties
            public ObservableCollection<GenericProduct> StandarInclusionProducts
            {
                get
                {
                    return _standardinclusion;
                }
                set
                {
                    _standardinclusion = value;
                    OnPropertyChanged("StandarInclusionProducts");
                }
            }
            public ObservableCollection<GenericProduct> UpgradeOptionProducts
            {
                get
                {
                    return _upgradeproduct;
                }
                set
                {
                    _upgradeproduct = value;
                    OnPropertyChanged("UpgradeOptionProducts");
                }
            }

            public ObservableCollection<GenericProduct> PromotionProducts
            {
                get
                {
                    return _promotionproduct;
                }
                set
                {
                    _promotionproduct = value;
                    OnPropertyChanged("PromotionProducts");
                }
            }

            public ObservableCollection<GenericProduct> AvailableOptionProducts
            {
                get
                {
                    return _availableproduct;
                }
                set
                {
                    _availableproduct = value;
                    OnPropertyChanged("AvailableOptionProducts");
                }
            }
            public ObservableCollection<Brand> SQSBrand
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
            public ObservableCollection<Brand> SQSBrandWithAll
            {
                get
                {
                    return _brandwithall;
                }
                set
                {
                    _brandwithall = value;
                    OnPropertyChanged("SQSBrandWithAll");
                }
            }
            public ObservableCollection<RegionGroup> SQSRegionGroup
            {
                get
                {
                    return _regiongorup;
                }
                set
                {
                    _regiongorup = value;
                    OnPropertyChanged("SQSRegionGroup");
                }
            }
            public ObservableCollection<Area> SQSArea
            {
                get
                {
                    return _area;
                }
                set
                {
                    _area = value;
                    OnPropertyChanged("SQSArea");
                }
            }
            public ObservableCollection<Group> SQSGroup
            {
                get
                {
                    return _group;
                }
                set
                {
                    _group = value;
                    OnPropertyChanged("SQSGroup");
                }
            }
            public ObservableCollection<RegionGroup> SQSRegionGroupWithAll
            {
                get
                {
                    return _regiongorupwithall;
                }
                set
                {
                    _regiongorupwithall = value;
                    OnPropertyChanged("SQSRegionGroupWithAll");
                }
            }
            public ObservableCollection<ProductAreaGroup> SQSProductAreaGroup
            {
                get
                {
                    return _pag;
                }
                set
                {
                    _pag = value;
                    OnPropertyChanged("SQSProductAreaGroup");
                }
            }

            public StandardInclusionPAG SelectedSI
            {
                get
                {
                    return _selectedstandardinclusion;
                }
                set
                {
                    _selectedstandardinclusion = value;
                    OnPropertyChanged("SelectedSI");
                }
            }
            public ProductAreaGroup SelectedPAG
            {
                get
                {
                    return _selectedpag;
                }
                set
                {
                    _selectedpag = value;
                    OnPropertyChanged("SelectedPAG");
                }
            }

            public ObservableCollection<StandardInclusionPAG> SQSAllStandardInclusions
            {
                get
                {
                    return _si;
                }
                set
                {
                    _si = value;
                    OnPropertyChanged("SQSAllStandardInclusions");
                }
            }
        #endregion

        #region Brands
        private Dictionary<string, object> _itemsBrand;
        private Dictionary<string, object> _selectedItemsBrand;    

        public Dictionary<string, object> ItemsBrand
        {
            get
            {
                return _itemsBrand;
            }
            set
            {
                _itemsBrand = value;
                OnPropertyChanged("ItemsBrand");
            }
        }

        public Dictionary<string, object> SelectedItemsBrand
        {
            get
            {
                return _selectedItemsBrand;
            }
            set
            {
                _selectedItemsBrand = value;
                OnPropertyChanged("SelectedItemsBrand");
            }
        }
        #endregion

        #region class
        public class GenericProduct
            {
                public int validationruleID { get; set; }
                public int BrandID { get; set; }
                public string ProductID { get; set; }
                public string ProductName { get; set; }
                public string ProductDescription { get; set; }
                public string BrandName { get; set; }
                public bool Promotion { get; set; }
                public DateTime EffectiveDate { get; set; } 
                public bool Active { get; set; }
        }

        public class ProductAreaGroup
            {
                public int ProductAreaGroupID { get; set; }
                public int AreaID { get; set; }
                public int GroupID { get; set; }
                public string AreaName { get; set; }
                public string GroupName { get; set; }
                public string ProductID { get; set; }
                public string ProductName { get; set; }
                public string ProductDescription { get; set; }
                public string ProductIDName { get; set; }
                public string EffectiveDate { get; set; }
            }
            public class Brand
            {
                public int BrandID { get; set; }
                public string BrandName { get; set; }
            }
            public class RegionGroup
            {
                public int RegionGroupID { get; set; }
                public string RegionGroupName { get; set; }
            }
            public class Group
            {
                public int GroupID { get; set; }
                public string GroupName { get; set; }
            }
            public class Area
            {
                public int AreaID { get; set; }
                public string AreaName { get; set; }
            }
            public class StandardInclusionPAG
            {
                public int StandardInclusionID { get; set; }
                public int SIBrandID { get; set; }
                public int PAGID { get; set; }
                public string ProductID { get; set; }
                public string ProductName { get; set; }
                public string ProductDescription { get; set; }
                public string SIBrandName { get; set; }
                public int SIRegionGroupID { get; set; }
                public string SIRegionGroupName { get; set; }
                public bool Active { get; set; }
                public decimal Quantity { get; set; }
                public string AreaName { get; set; }
                public string GroupName { get; set; }
                public string EffectiveDate { get; set; }
        }
        #endregion

    }


}
