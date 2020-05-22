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
using System.Collections.Specialized;
using System.Windows.Threading;
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;


namespace SQSAdmin_WpfCustomControlLibrary.Common
{
    public class CommonResource : ViewModelBase
    {

        private ObservableCollection<Brand> _brand = new ObservableCollection<Brand>();
        private ObservableCollection<Brand> _brandwithall = new ObservableCollection<Brand>();
        private ObservableCollection<Brand> _brandwithdefault = new ObservableCollection<Brand>();
        private ObservableCollection<State> _state = new ObservableCollection<State>();
        private ObservableCollection<State> _state2 = new ObservableCollection<State>();
        private ObservableCollection<Storey> _storey = new ObservableCollection<Storey>();
        private ObservableCollection<Status> _status = new ObservableCollection<Status>();
        private ObservableCollection<PromotionType> _promotype = new ObservableCollection<PromotionType>();
        private ObservableCollection<Home> _home = new ObservableCollection<Home>();
        private ObservableCollection<Product> _sourceproduct = new ObservableCollection<Product>();
        private ObservableCollection<Product> _targetproduct = new ObservableCollection<Product>();
        private ObservableCollection<FullMultiplePromotion> _multipromo = new ObservableCollection<FullMultiplePromotion>();
        private ObservableCollection<DisplayHome> _displayhome = new ObservableCollection<DisplayHome>();

        private ObservableCollection<Area> _area1 = new ObservableCollection<Area>();
        private ObservableCollection<Area> _area2 = new ObservableCollection<Area>();
        private ObservableCollection<Area> _allareas = new ObservableCollection<Area>();
        private ObservableCollection<Area> _existingproductarea = new ObservableCollection<Area>();
        private ObservableCollection<Area> _availableproductarea = new ObservableCollection<Area>();
        private ObservableCollection<Group> _group1 = new ObservableCollection<Group>();
        private ObservableCollection<Group> _group2 = new ObservableCollection<Group>();
        private ObservableCollection<Group> _group3 = new ObservableCollection<Group>();
        private ObservableCollection<Group> _group4 = new ObservableCollection<Group>();
        private ObservableCollection<Group> _allgroups = new ObservableCollection<Group>();
        private ObservableCollection<Group> _existingproductgroup = new ObservableCollection<Group>();
        private ObservableCollection<Group> _availableproductgroup = new ObservableCollection<Group>();
        private ObservableCollection<Region> _region = new ObservableCollection<Region>();
        private ObservableCollection<Region> _regionwithdefault = new ObservableCollection<Region>();
        private ObservableCollection<Region> _regionwithall = new ObservableCollection<Region>();
        private ObservableCollection<Home> _homewithdefault = new ObservableCollection<Home>();
        private ObservableCollection<Specification> _specifications = new ObservableCollection<Specification>();
        private ObservableCollection<DisplayCode> _attachmenttypes = new ObservableCollection<DisplayCode>();
        private ObservableCollection<PromotionProduct> _promotionproduct = new ObservableCollection<PromotionProduct>();
        private ObservableCollection<MultiplePromotion> _multiplepromotion = new ObservableCollection<MultiplePromotion>();
        private ObservableCollection<HomeConfigurationItem> _existingpag = new ObservableCollection<HomeConfigurationItem>();
        private ObservableCollection<HomeConfigurationItem> _availablepag = new ObservableCollection<HomeConfigurationItem>();
        private ObservableCollection<HomeModelAndQuantity> _homemodelquantity = new ObservableCollection<HomeModelAndQuantity>();
        private ObservableCollection<DisplayCode> _pricedisplaycode = new ObservableCollection<DisplayCode>();
        private ObservableCollection<BasePriceHoldingDays> _basepriceholddays = new ObservableCollection<BasePriceHoldingDays>();

        private SQSAdminServiceClient client;
        public int loginstateid, multiplepromotionid, defaultstate;

        #region methods
        public CommonResource(int stateid, int promotionid)
        {
            DefaultStateID = stateid;
            LoginState = stateid;
            multiplepromotionid = promotionid;
            //client = new SQSAdminServiceClient();
            LoadState();
            LoadRegion(defaultstate);
            LoadBrand(defaultstate);
            loadStorey();
            loadStatus();
            //LoadSystemDocumentTypes();
            //LoadAttachmentTypes();
            //LoadPromotionType();
            //LoadAllAreaForAvailableProduct();
            //LoadAllAreaForExistingPromotion();
            //LoadAllGroupForExistingPromotion();

        }
        public void LoadState()
        {
            SQSState.Clear();
            SQSState2.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetState();
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                State s = new State();
                s.StateID = int.Parse(dr["stateid"].ToString());
                s.StateAbbreviation = dr["stateAbbreviation"].ToString();
                SQSState.Add(s);
                SQSState2.Add(s);
            }
        }
        #region price holding days
        public void LoadBasePriceHoldingDays(string stateid, string regionids, string brandids, DateTime effectivedate, string active)
        {

            BasePriceHoldDays.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_BasePriceHoldDays(stateid, regionids, brandids, effectivedate, active);
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                BasePriceHoldingDays bh = new BasePriceHoldingDays();
                bh.ID = int.Parse(dr["ID"].ToString());
                bh.RegionID = int.Parse(dr["regionid"].ToString());
                bh.RegionName = dr["regionname"].ToString();
                bh.BrandID = int.Parse(dr["brandid"].ToString());
                bh.BrandName = dr["brandname"].ToString();
                bh.DepositAmount = dr["amount"].ToString();
                bh.DaysFrom = int.Parse(dr["daysfrom"].ToString());
                bh.DaysTo = int.Parse(dr["daysto"].ToString());
                bh.EffectiveDate = DateTime.Parse(dr["effectivedate"].ToString()).AddHours(1);
                bh.CMAPercent = decimal.Parse(dr["CMApercent"].ToString());
                bh.SurchargePercent = decimal.Parse(dr["SurchargePercent"].ToString());
                bh.BTPSingleStoreyPercent = decimal.Parse(dr["BTPSingleStoreyPercent"].ToString());
                bh.BTPDoubleStoreyPercent = decimal.Parse(dr["BTPDoubleStoreyPercent"].ToString());
                bh.BTPSingleStoreyDiscount = decimal.Parse(dr["btpSingleStoreyDiscount"].ToString());
                bh.BTPDoubleStoreyDiscount = decimal.Parse(dr["btpDoubleStoreyDiscount"].ToString());
                bh.BTPSingleStoreySiteOtherCosts = decimal.Parse(dr["BTPSingleStoreySiteOtherCosts"].ToString());
                bh.BTPDoubleStoreySiteOtherCosts = decimal.Parse(dr["BTPDoubleStoreySiteOtherCosts"].ToString());

                if (dr["active"].ToString() == "1" || dr["active"].ToString().ToLower() == "true")
                    bh.Active = true;
                else
                    bh.Active = false;
                bh.State = dr["state"].ToString();
                BasePriceHoldDays.Add(bh);
            }
        }

        public void UpdateBasePriceHoldingDays(string id, string daysfrom, string daysto, DateTime effectivedate, string active, string depositamount, string usercode, decimal cmapercent, decimal surchargepercent, decimal btpsinglestorey, decimal btpdoublestorey, decimal btpSingleStoryDiscount, decimal btpDoubleStoryDiscount, decimal btpSingleStoreyCostSiteOther, decimal btpDoubleStoreyCostSiteOther)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            client.SQSAdmin_Generic_UpdateBasePriceHoldDays(id, daysfrom, daysto, effectivedate, active, depositamount, usercode, cmapercent, surchargepercent, btpsinglestorey, btpdoublestorey, btpSingleStoryDiscount, btpDoubleStoryDiscount, btpSingleStoreyCostSiteOther, btpDoubleStoreyCostSiteOther);
            client.Close();
        }

        public void NewBasePriceHoldingDays(int stateid, string regionids, string brandids, string daysfrom, string daysto, DateTime effectivedate, string active, string depositamount, string usercode, string cmapercent, string surchargepercent, string regionalsurchargeSSpercent, string regionalsurchargeSDpercent, string btpSingleStoryDiscount, string btpDoubleStoryDiscount, string btpSingleStoreyCostSiteOther, string btpDoubleStoreyCostSiteOther)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            client.SQSAdmin_Generic_NeweBasePriceHoldDays(stateid, regionids, brandids, daysfrom, daysto, effectivedate, active, depositamount, usercode, cmapercent, surchargepercent, regionalsurchargeSSpercent, regionalsurchargeSDpercent, btpSingleStoryDiscount, btpDoubleStoryDiscount, btpSingleStoreyCostSiteOther, btpDoubleStoreyCostSiteOther);
            client.Close();
        }
        #endregion
        public void LoadRegion(int stateid)
        {
            LoadRegionData(stateid, ref _region);
        }
        public void LoadBrand(int stateid)
        {
            SQSBrand.Clear();
            SQSBrandWithAll.Clear();
            ItemsBrand.Clear();
            SelectedItemsBrand.Clear();
            Dictionary<string, object> _items = new Dictionary<string, object>();
            _items.Add("Default", "0");

            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetBrandByState(stateid);
            client.Close();

            Brand a = new Brand();
            a.BrandID = 0;
            a.BrandName = "ALL";
            SQSBrandWithAll.Add(a);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Brand b = new Brand();
                b.BrandID = int.Parse(dr["brandid"].ToString());
                b.BrandName = dr["brandname"].ToString();
                SQSBrand.Add(b);
                SQSBrandWithAll.Add(b);
                _items.Add(b.BrandName, b.BrandID);
            }
            ItemsBrand = _items;
        }
        public void LoadBrandWithDefault(int stateid)
        {
            SQSBrandWithDefault.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetBrandByState(stateid);
            client.Close();

            Brand a = new Brand();
            a.BrandID = 0;
            a.BrandName = "Default";
            SQSBrandWithDefault.Add(a);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Brand b = new Brand();
                b.BrandID = int.Parse(dr["brandid"].ToString());
                b.BrandName = dr["brandname"].ToString();
                SQSBrand.Add(b);
                SQSBrandWithDefault.Add(b);
            }
        }

        public void LoadRegionWithAll(int stateid)
        {
            LoadRegionData(stateid, ref _regionwithall);

            Region a = new Region();
            a.RegionID = 0;
            a.RegionName = "Default";
            SQSRegionWithAll.Insert(0, a);
        }

        public void LoadRegionWithDefault(int stateid)
        {

            LoadRegionData(stateid, ref _regionwithdefault);

            Region a = new Region();
            a.RegionID = 0;
            a.RegionName = "Default";
            SQSRegionWithDefault.Insert(0, a);
        }

        public void LoadRegionData(int stateid, ref ObservableCollection<Region> region)
        {
            SQSRegion.Clear();
            ItemsRegion.Clear();
            SelectedItemsRegion.Clear();
            Dictionary<string, object> _items = new Dictionary<string, object>();
            _items.Add("Default", "0");

            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetRegionList(stateid);
            client.Close();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Region s = new Region();
                s.RegionID = int.Parse(dr["regionid"].ToString());
                s.RegionName = dr["regionname"].ToString();
                s.RegionCode = dr["bccode"].ToString();
                SQSRegion.Add(s);
                _items.Add(s.RegionName, s.RegionID);
                //region.Add(s);
            }
            ItemsRegion = _items;
        }

        public void LoadHomeWithDefault(int stateid, int brandid, string homename, int active)
        {
            SQSHomeWithDefault.Clear();

            Home h = new Home();
            h.HomeID = 0;
            h.HomeName = "Default";
            SQSHomeWithDefault.Add(h);

            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Home_GetHomeList(stateid, brandid, homename, active);
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Home s = new Home();
                s.HomeID = int.Parse(dr["homeid"].ToString());
                s.HomeName = dr["homename"].ToString();
                s.ProductID = dr["productid"].ToString();
                s.BrandID = int.Parse(dr["brandid"].ToString());
                s.Stories = dr["storey"].ToString();
                s.BrandName = dr["brandname"].ToString();

                if (dr["active"].ToString().ToUpper() == "TRUE" || dr["active"].ToString().ToUpper() == "1")
                {
                    s.Active = true;
                }
                else
                {
                    s.Active = false;
                }
                SQSHomeWithDefault.Add(s);
            }
        }

        public void LoadSystemDocumentTypes()
        {
            ItemsSystemDocumentTypes = new Dictionary<string, object>();
            SelectedItemsSystemDocumentTypes = new Dictionary<string, object>();

            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetSQSConfiguration("SystemDocumentType", "");
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DisplayCode s = new DisplayCode();
                s.DisplayCodeID = int.Parse(dr["CodeValue"].ToString());
                s.DisplayCodeName = dr["CodeText"].ToString();
                ItemsSystemDocumentTypes.Add(s.DisplayCodeName, s.DisplayCodeID);
            }
        }

        public void LoadAttachmentTypes()
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetSQSConfiguration("AttachmentType", "");
            client.Close();

            AttachmentTypes.Clear();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DisplayCode s = new DisplayCode();
                s.DisplayCodeID = int.Parse(dr["CodeValue"].ToString());
                s.DisplayCodeName = dr["CodeText"].ToString();
                AttachmentTypes.Add(s);
            }
        }

        public void GetSpecificationByStateRegionsBrands(int stateid, string regionids, string brandids, System.DateTime effdate, string systemid, string attachmentid, string homestring)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetSpecificationByStateRegionsBrands(stateid, regionids, brandids, effdate, systemid, attachmentid, homestring);
            client.Close();

            Specifications.Clear();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Specification s = new Specification();

                if (dr.Table.Columns["idSpecification"] != null)
                    s.ID = int.Parse(dr["idSpecification"].ToString());
                if (dr.Table.Columns["SpecificationFileName"] != null)
                    s.FileName = dr["SpecificationFileName"].ToString();
                s.StateName = dr["StateName"].ToString();
                s.RegionName = dr["RegionName"].ToString();
                s.BrandName = dr["BrandName"].ToString();
                s.HomeName = dr["HomeName"].ToString();
                DateTime dateEffectiveDate = DateTime.Now;
                DateTime.TryParse(dr["EffectiveDate"].ToString(), out dateEffectiveDate);
                //s.EffectiveDate = dateEffectiveDate.ToString("yyyy/MM/dd hh:mm tt");
                s.EffectiveDate = dateEffectiveDate.AddHours(1);
                s.SystemDocument = dr["SystemDocument"].ToString();
                s.AttachmentType = dr["AttachmentType"].ToString();
                s.ContractType = dr["ContractType"].ToString();
                if (dr["active"].ToString().ToUpper() == "TRUE" || dr["active"].ToString().ToUpper() == "1")
                {
                    s.Active = true;
                }
                else
                {
                    s.Active = false;
                }
                Specifications.Add(s);
            }
        }

        public void GetSpecificationByStateRegionBrand(int stateid, int regionid, int brandid, System.DateTime effdate, string systemid, string attachmentid, string homestring)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetSpecificationByStateRegionBrand(stateid, regionid, brandid, effdate, systemid, attachmentid, homestring);
            client.Close();

            Specifications.Clear();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Specification s = new Specification();

                if (dr.Table.Columns["idSpecification"] != null)
                    s.ID = int.Parse(dr["idSpecification"].ToString());
                if (dr.Table.Columns["SpecificationFileName"] != null)
                    s.FileName = dr["SpecificationFileName"].ToString();
                s.StateName = dr["StateName"].ToString();
                s.RegionName = dr["RegionName"].ToString();
                s.BrandName = dr["BrandName"].ToString();
                s.HomeName = dr["HomeName"].ToString();
                DateTime dateEffectiveDate = DateTime.Now;
                DateTime.TryParse(dr["EffectiveDate"].ToString(), out dateEffectiveDate);
                s.EffectiveDate = dateEffectiveDate.AddHours(1);
                s.SystemDocument = dr["SystemDocument"].ToString();
                s.AttachmentType = dr["AttachmentType"].ToString();
                s.ContractType = dr["ContractType"].ToString();
                Specifications.Add(s);
            }
        }
        public void SaveSpecificationByStateRegionBrand(int id, string filename, int stateid, int regionid, int brandid, int homeid, DateTime effdate, int systemformid, int attachmenttypeid, int active)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            client.SQSAdmin_Generic_SaveSpecificationByStateRegionBrand(id, filename, stateid, regionid, brandid, homeid, effdate, systemformid, attachmenttypeid, active);
            client.Close();
        }

        public DataSet LoadProductUOM()
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            return client.SQSAdmin_Generic_GetProductUOM();

        }
        public DataSet LoadProductCategory()
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            return client.SQSAdmin_Generic_GetProductCategory();

        }
        public DataSet LoadProductCode()
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            return client.SQSAdmin_Generic_GetProductCode();

        }

        public DataSet LoadProductDisplayCode()
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            return client.SQSAdmin_Generic_GetProductDisplayCode("0");
        }

        public void LoadProductDisplayCodeForPromo()
        {
            PriceDisplayCode.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetProductDisplayCode("1");
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DisplayCode dp = new DisplayCode();
                dp.DisplayCodeID = int.Parse(dr["pricedisplaycodeid"].ToString());
                dp.DisplayCodeName = dr["pricedisplaydesc"].ToString();
                PriceDisplayCode.Add(dp);
            }
        }

        public DataSet LoadProductCostCenterCode()
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            return client.SQSAdmin_Generic_GetProductCostCenterCode();

        }
        public void LoadPromotionType()
        {
            SQSPromotionType.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Promotion_GetPromotionTypeList();
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                PromotionType s = new PromotionType();
                s.PromotionTypeid = int.Parse(dr["promotiontypeid"].ToString());
                s.PromotionTypeName = dr["promotiondescription"].ToString();
                SQSPromotionType.Add(s);
            }
        }

        public void LoadDisplayHome(string stateid, string brandid, string pagid, string displayhomename, string suburb)
        {
            SQSDisplayHome.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetDisplayHomeByPAGAndState(stateid, brandid, pagid, displayhomename, suburb);
            client.Close();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DisplayHome s = new DisplayHome();
                s.DisplayHomeID = int.Parse(dr["homeid"].ToString());
                s.BrandID = int.Parse(dr["brandid"].ToString());
                s.BrandName = dr["brandname"].ToString();
                s.DisplayHomeName = dr["homename"].ToString();
                s.Suburb = dr["suburb"].ToString();
                s.Address = dr["lotaddress"].ToString();
                SQSDisplayHome.Add(s);
            }
        }

        public string ReplacePAGInDisplayhomes(string existingpag, string newpag, string displayhomeid, string usercode, string qty, string changeqty, string changeprice, string allowdesc, string desc, string newextradesc)
        {
            string result = "";
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            result = client.SQSAdmin_Generic_ReplaceDisplayHomePAG(existingpag, newpag, displayhomeid, usercode, qty, changeqty, changeprice, allowdesc, desc, newextradesc);
            client.Close();

            return result;

        }

        public void UpdateMultilpePromotionName(string promotionid, string promotionname)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            client.SQSAdmin_Promotion_UpdateMultiplePromotionName(promotionid, promotionname);
            client.Close();
        }

        public DataSet ImportProduct(System.Data.DataTable dt, string usercode)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_ImportProduct(dt, usercode);
            client.Close();

            return ds;
        }

        public DataSet ImportPrice(System.Data.DataTable dt, string usercode, string direction)
        {
            DataSet ds;
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);

            if (direction.ToUpper() == "VERTICAL")
            {
                ds = client.SQSAdmin_Generic_ImportPriceVerticalFormat(dt, usercode);
            }
            else
            {
                //if (loginstateid == 3) // QLD horizontal import
                //{
                System.Data.DataTable newformat = ConvertToImportFormatTable(dt);
                ds = client.SQSAdmin_Generic_ImportPriceHorizontalFormat(newformat, usercode, loginstateid.ToString());
                //}
                //else  // NSW horizontal import
                //{
                //    ds = client.SQSAdmin_Generic_ImportPriceHorizontalFormatNSW(dt, usercode);
                //}
            }
            client.Close();

            return ds;
        }

        public System.Data.DataTable ConvertToImportFormatTable(System.Data.DataTable dt)
        {
            System.Data.DataTable tempTable = new System.Data.DataTable();
            tempTable.TableName = "newtable";
            tempTable.Columns.Add("productid");
            tempTable.Columns.Add("HouseType");
            tempTable.Columns.Add("effectivedate");
            tempTable.Columns.Add("values");
            tempTable.Columns.Add("derivedcost");

            foreach (DataRow dr in dt.Rows)
            {
                string values = "";

                for (int idx = 3; idx < dr.ItemArray.Count() - 1; idx++)
                {
                    if (values == "")
                    {
                        values = dr[idx].ToString();
                    }
                    else
                    {
                        values = values + "|" + dr[idx].ToString();
                    }
                }

                string tempdate = DateTime.Parse(dr["EffectiveDate"].ToString()).ToString("dd/MMM/yyyy");

                DataRow newrow = tempTable.NewRow();
                newrow["productid"] = dr["productid"];
                newrow["housetype"] = dr["housetype"];
                newrow["EffectiveDate"] = tempdate;
                newrow["values"] = values;
                newrow["derivedcost"] = dr["derivedcost"];

                tempTable.Rows.Add(newrow);
            }


            return tempTable;
        }
        public DataSet ValidatePrice(System.Data.DataTable dt, string usercode, string direction)
        {
            DataSet ds;
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            if (direction.ToUpper() == "VERTICAL")
            {
                ds = client.SQSAdmin_Generic_ValidatePriceVerticalFormat(dt, usercode);
            }
            else
            {
                //if (loginstateid == 3) //qld horizontal format
                //{
                    ds = client.SQSAdmin_Generic_ValidatePriceHorizontalFormat(dt, usercode, loginstateid.ToString());
                //}
                //else
                //{
                //    ds = client.SQSAdmin_Generic_ValidatePriceHorizontalFormatNSW(dt, usercode);
                //}
            }
            client.Close();

            return ds;
        }

        public DataSet ValidateProduct(System.Data.DataTable dt)
        {
            DataSet ds;
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);

            ds = client.SQSAdmin_Generic_CheckExistingProducts(dt);

            client.Close();

            return ds;
        }
        public DataSet ValidateQuantity(System.Data.DataTable dt, string usercode)
        {
            DataSet ds;
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);

            ds = client.SQSAdmin_Generic_ValidateQuantity(dt, usercode);
            client.Close();

            return ds;
        }
        public DataSet ImportQuantity(System.Data.DataTable dt, string usercode)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_ImportQuantity(dt, usercode);
            client.Close();

            return ds;
        }

        public void LoadAllAreaForAvailableProduct()
        {
            Area s;
            SQSAllAreaForAvailableProduct.Clear();
            s = new Area();
            s.AreaID = 0;
            s.AreaName = "All";
            SQSAllAreaForAvailableProduct.Add(s);
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetAllAreas();
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                s = new Area();
                s.AreaID = int.Parse(dr["areaid"].ToString());
                s.AreaName = dr["areaname"].ToString();
                SQSAllAreaForAvailableProduct.Add(s);
            }

        }

        public void LoadAllAreaForExistingPromotion()
        {
            Area s;
            SQSAreaForPromotionProduct.Clear();

            s = new Area();
            s.AreaID = 0;
            s.AreaName = "All";
            SQSAreaForPromotionProduct.Add(s);
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Promotion_GetAreaInPromotion(multiplepromotionid);
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                s = new Area();
                s.AreaID = int.Parse(dr["areaid"].ToString());
                s.AreaName = dr["areaname"].ToString();
                SQSAreaForPromotionProduct.Add(s);
            }

        }
        public void LoadAllGroupForExistingPromotion()
        {
            Group s;
            SQSGroupForPromotionProduct.Clear();

            s = new Group();
            s.GroupID = 0;
            s.GroupName = "All";
            SQSGroupForPromotionProduct.Add(s);
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Promotion_GetGroupInPromotion(multiplepromotionid);
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                s = new Group();
                s.GroupID = int.Parse(dr["groupid"].ToString());
                s.GroupName = dr["groupname"].ToString();
                SQSGroupForPromotionProduct.Add(s);
            }

        }

        public void LoadAllGroupForAvailableProduct()
        {
            SQSAllGroupForAvailableProduct.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetAllGroups();
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Group s = new Group();
                s.GroupID = int.Parse(dr["groupid"].ToString());
                s.GroupName = dr["groupname"].ToString();
                SQSAllGroupForAvailableProduct.Add(s);
            }
        }

        public void LoadFilteredGroupForAvailableProduct(int areaid)
        {
            Group s;
            SQSFilteredGroupForAvailableProduct.Clear();
            s = new Group();
            s.GroupID = 0;
            s.GroupName = "All";
            SQSFilteredGroupForAvailableProduct.Add(s);
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetGroupsFromArea(areaid);
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                s = new Group();
                s.GroupID = int.Parse(dr["groupid"].ToString());
                s.GroupName = dr["groupname"].ToString();
                SQSFilteredGroupForAvailableProduct.Add(s);
            }

        }

        public void LoadFilteredGroupForAvailableProduct2(int areaid)
        {
            Group s;
            SQSFilteredGroupForAvailableProduct2.Clear();
            s = new Group();
            s.GroupID = 0;
            s.GroupName = "All";
            SQSFilteredGroupForAvailableProduct2.Add(s);
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetGroupsFromArea(areaid);
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                s = new Group();
                s.GroupID = int.Parse(dr["groupid"].ToString());
                s.GroupName = dr["groupname"].ToString();
                SQSFilteredGroupForAvailableProduct2.Add(s);
            }

        }

        public void LoadFilteredGroupWithoutAllForAvailableProduct(int areaid)
        {
            Group s;
            SQSFilteredGroupForAvailableProduct.Clear();

            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetGroupsFromArea(areaid);
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                s = new Group();
                s.GroupID = int.Parse(dr["groupid"].ToString());
                s.GroupName = dr["groupname"].ToString();
                SQSFilteredGroupForAvailableProduct.Add(s);
            }

        }
        private void loadStorey()
        {
            SQSStorey.Clear();
            Storey st = new Storey();
            st.id = 0;
            st.StoreyName = "All";
            SQSStorey.Add(st);


            st = new Storey();
            st.id = 1;
            st.StoreyName = "1";
            SQSStorey.Add(st);

            st = new Storey();
            st.id = 2;
            st.StoreyName = "2";
            SQSStorey.Add(st);

        }

        private void loadStatus()
        {
            SQSStatus.Clear();
            Status st = new Status();
            st.id = 0;
            st.StatusName = "All";
            SQSStatus.Add(st);

            st = new Status();
            st.id = 1;
            st.StatusName = "Active";
            SQSStatus.Add(st);

            st = new Status();
            st.id = 2;
            st.StatusName = "Inactive";
            SQSStatus.Add(st);

        }

        public void LoadPromotionProduct(int pagid, string productid)
        {
            SQSPromotionProduct.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Promotion_GetPromotionForProduct(pagid, productid);
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                PromotionProduct s = new PromotionProduct();
                s.PromotionID = int.Parse(dr["idmultiplepromotion"].ToString());
                s.PromotionBaseProductID = dr["baseproductid"].ToString();
                s.PromotionName = dr["promotionname"].ToString();
                s.HomeBrand = dr["brandname"].ToString();
                s.Storey = dr["storey"].ToString();
                s.PAGID = int.Parse(dr["pagid"].ToString());
                s.ProductID = dr["productid"].ToString();
                s.AreaName = dr["areaname"].ToString();
                s.GroupName = dr["groupname"].ToString();
                SQSPromotionProduct.Add(s);
            }
        }

        public void LoadSourceProducts(int stateid, string productid, string keyword)
        {
            SourceProduct.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_HomeConfiguration_GetProducts(stateid, productid, keyword);
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Product s = new Product();
                s.ProductID = dr["productid"].ToString();
                s.ProductName = dr["productname"].ToString();
                s.ProductDescription = dr["productdescription"].ToString();

                SourceProduct.Add(s);
            }
        }
        public void LoadTargetProducts(int stateid, string productid, string keyword)
        {
            TargetProduct.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_HomeConfiguration_GetProducts(stateid, productid, keyword);
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Product s = new Product();
                s.ProductID = dr["productid"].ToString();
                s.ProductName = dr["productname"].ToString();
                s.ProductDescription = dr["productdescription"].ToString();
                TargetProduct.Add(s);
            }
        }
        public void LoadHomes(int stateid, int brandid, string homename, int active)
        {
            SQSHome.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Home_GetHomeList(stateid, brandid, homename, active);
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Home s = new Home();
                s.HomeID = int.Parse(dr["homeid"].ToString());
                s.HomeName = dr["homename"].ToString();
                s.ProductID = dr["productid"].ToString();
                s.BrandID = int.Parse(dr["brandid"].ToString());
                s.Stories = dr["storey"].ToString();
                s.BrandName = dr["brandname"].ToString();

                if (dr["active"].ToString().ToUpper() == "TRUE" || dr["active"].ToString().ToUpper() == "1")
                {
                    s.Active = true;
                }
                else
                {
                    s.Active = false;
                }
                SQSHome.Add(s);
            }
        }
        public void LoadHomesPlans(int stateid, List<int> brandids, string homename, int active)
        {
            int brandid = 0;

            SQSHome.Clear();
            ItemsHome.Clear();
            SelectedItemsHome.Clear();
            Dictionary<string, object> _items = new Dictionary<string, object>();

            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Home_GetHomePlanByStateAndBrands(stateid, getIDsSelected(brandids));
            client.Close();
            if (ds != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Home s = new Home();
                    s.HomeID = int.Parse(dr["homeid"].ToString());
                    s.HomeName = dr["homename"].ToString();
                    SQSHome.Add(s);
                    if (!_items.ContainsKey(s.HomeName))
                    {
                        _items.Add(s.HomeName, s.HomeID);
                    }
                }
            }
            ItemsHome = _items;
        }
        public void LoadFullPromotion(DataSet ds)
        {
            SQSFullMultiplePromotion.Clear();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                FullMultiplePromotion s = new FullMultiplePromotion();
                s.PromotionID = int.Parse(dr["promotionid"].ToString());
                s.MultiplePromotionID = int.Parse(dr["idmultiplepromotion"].ToString());
                s.PromotionName = dr["promotionname"].ToString();
                s.PromotionType = dr["promotiontype"].ToString();
                s.Storey = int.Parse(dr["stories"].ToString());
                s.BaseProduct = dr["baseproductid"].ToString();
                s.BrandName = dr["brandname"].ToString();
                s.PromotionCost = double.Parse(dr["promotioncost"].ToString());
                if (dr["promotioncape"].ToString() != "")
                {
                    s.CapeVale = double.Parse(dr["promotioncape"].ToString());
                }
                else
                {
                    s.CapeVale = 0.0;
                }
                if (dr["forregional"].ToString() == "1" || dr["forregional"].ToString().ToUpper() == "TRUE")
                {
                    s.ForRegional = true;
                }
                else
                {
                    s.ForRegional = false;
                }

                if (dr["active"].ToString().ToUpper() == "TRUE" || dr["active"].ToString().ToUpper() == "1")
                {
                    s.Active = true;
                }
                else
                {
                    s.Active = false;
                }
                s.DisplayCodeName = dr["pricedisplaydesc"].ToString();
                s.DisplayCodeID = int.Parse(dr["fkidpricedisplaycode"].ToString());
                SQSFullMultiplePromotion.Add(s);
            }
        }
        public void LoadHomeConfigurationAreas(int stateid)
        {
            ExistingProductArea.Clear();
            AvailableProductArea.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetAreas(stateid);
            client.Close();
            Area a = new Area();
            a.AreaID = 0;
            a.AreaName = "ALL";
            ExistingProductArea.Add(a);
            AvailableProductArea.Add(a);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Area s = new Area();
                s.AreaID = int.Parse(dr["areaid"].ToString());
                s.AreaName = dr["areaname"].ToString();
                ExistingProductArea.Add(s);
                AvailableProductArea.Add(s);
            }
        }

        public void LoadHomeConfigurationGroups(int stateid)
        {
            ExistingProductGroup.Clear();
            AvailableProductGroup.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Generic_GetGroups(stateid);
            client.Close();
            Group g = new Group();
            g.GroupID = 0;
            g.GroupName = "ALL";
            ExistingProductGroup.Add(g);
            AvailableProductGroup.Add(g);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Group s = new Group();
                s.GroupID = int.Parse(dr["groupid"].ToString());
                s.GroupName = dr["groupname"].ToString();
                ExistingProductGroup.Add(s);
                AvailableProductGroup.Add(s);
            }
        }

        public DataSet LoadHomeExistingPAGAsDataSet(int homeid, int areaid, int groupid, string keyword)
        {
            HomeExistingPAG.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_HomeConfiguration_GetExistingPAGForHome(homeid, areaid, groupid, keyword);
            client.Close();

            return ds;
        }


        public void LoadHomeExistingPAG(int homeid, int areaid, int groupid, string keyword)
        {
            HomeExistingPAG.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_HomeConfiguration_GetExistingPAGForHome(homeid, areaid, groupid, keyword);
            client.Close();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                HomeConfigurationItem s = new HomeConfigurationItem();
                s.HomeID = homeid;
                s.PagID = int.Parse(dr["productareagroupid"].ToString());
                s.AreaID = int.Parse(dr["areaid"].ToString());
                s.AreaName = dr["areaname"].ToString();
                s.GroupID = int.Parse(dr["groupid"].ToString());
                s.GroupName = dr["groupname"].ToString();
                s.ProductID = dr["productid"].ToString();
                s.ProductName = dr["productname"].ToString();
                s.ProductDescription = dr["productdescription"].ToString();
                s.Quantity = decimal.Parse(dr["quantity"].ToString());
                s.UnitPrice = decimal.Parse(dr["unitprice"].ToString());
                if (dr["ChangePrice"].ToString().ToUpper() == "TRUE" || dr["ChangePrice"].ToString() == "1")
                {
                    s.ChangePrice = true;
                }
                else
                {
                    s.ChangePrice = false;
                }
                if (dr["ChangeQty"].ToString().ToUpper() == "TRUE" || dr["ChangeQty"].ToString() == "1")
                {
                    s.ChangeQuantity = true;
                }
                else
                {
                    s.ChangeQuantity = false;
                }
                if (dr["HomeDisplayID"].ToString() == "0")
                {
                    s.IsDisplayHomeItem = false;
                }
                else
                {
                    s.IsDisplayHomeItem = true;
                }
                if (dr["addextradesc"].ToString().ToUpper() == "TRUE" || dr["addextradesc"].ToString() == "1")
                {
                    s.AddExtraDesc = true;
                }
                else
                {
                    s.AddExtraDesc = false;
                }
                HomeExistingPAG.Add(s);
            }
        }

        public void LoadHomeAvailablePAG(int homeid, int areaid, int groupid, string keyword)
        {
            HomeAvailablePAG.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_HomeConfiguration_GetAvailablePAGForHome(homeid, areaid, groupid, keyword);
            client.Close();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                HomeConfigurationItem s = new HomeConfigurationItem();
                s.HomeID = homeid;
                s.PagID = int.Parse(dr["productareagroupid"].ToString());
                s.AreaID = int.Parse(dr["areaid"].ToString());
                s.AreaName = dr["areaname"].ToString();
                s.GroupID = int.Parse(dr["groupid"].ToString());
                s.GroupName = dr["groupname"].ToString();
                s.ProductID = dr["productid"].ToString();
                s.ProductName = dr["productname"].ToString();
                s.ProductDescription = dr["productdescription"].ToString();
                s.Quantity = decimal.Parse(dr["quantity"].ToString());
                s.UnitPrice = decimal.Parse(dr["unitprice"].ToString());
                s.IsDisplayHomeItem = false;
                if (dr["ChangePrice"].ToString().ToUpper() == "TRUE" || dr["ChangePrice"].ToString() == "1")
                {
                    s.ChangePrice = true;
                }
                else
                {
                    s.ChangePrice = false;
                }
                if (dr["ChangeQty"].ToString().ToUpper() == "TRUE" || dr["ChangeQty"].ToString() == "1")
                {
                    s.ChangeQuantity = true;
                }
                else
                {
                    s.ChangeQuantity = false;
                }
                HomeAvailablePAG.Add(s);
            }
        }
        public bool AddProductsToHome(int homeid, string selectedpagid, string qtystring, string changeqtystring, string changepricestring, string extradescstring, string usercode)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            bool ds = client.SQSAdmin_HomeConfiguration_AddProductToHome(homeid, selectedpagid, qtystring, changeqtystring, changepricestring, extradescstring, usercode);
            client.Close();
            return ds;

        }
        public bool UpdatePagForHome(int homeid, int pagid, decimal quantity, int changeqty, int changeprice, int extradesc, string usercode)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            bool ds = client.SQSAdmin_HomeConfiguration_UpdatePagForHome(homeid, pagid, quantity.ToString(), changeqty, changeprice, extradesc, usercode);
            client.Close();
            return ds;

        }

        public bool RemoveProductsFromHome(int homeid, string selectedpagids, string usercode)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            bool ds = client.SQSAdmin_HomeConfiguration_RemoveProductsFromHome(homeid, selectedpagids, usercode);
            client.Close();
            return ds;

        }

        public string CopyProductConfiguration(string sourceproductid, string targetproductid, string usercode)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            string ds = client.SQSAdmin_HomeConfiguration_CopyProductConfiguration(sourceproductid, targetproductid, usercode);
            client.Close();
            return ds;

        }
        //public void LoadAvailableProductArea(int stateid, int homeid)
        //{
        //    AvailableProductArea.Clear();
        //    client = new SQSAdminServiceClient();
        //    client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
        //    DataSet ds = client.SQSAdmin_HomeConfiguration_GetAvailableAreaForHome(stateid, homeid);
        //    client.Close();
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        Area s = new Area();
        //        s.AreaID = int.Parse(dr["areaid"].ToString());
        //        s.AreaName = dr["areaname"].ToString();
        //        AvailableProductArea.Add(s);
        //    }
        //}
        public void LoadSourcePromotionForCopy(int targetpromotionid)
        {
            SQSMultiplePromotion.Clear();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Promotion_GetPromotionForCopy(targetpromotionid);
            client.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                MultiplePromotion s = new MultiplePromotion();
                s.MultiplePromotionID = int.Parse(dr["idmultiplepromotion"].ToString());
                s.PromotionName = "(" + dr["baseproductid"].ToString() + ") " + dr["promotionname"].ToString();
                SQSMultiplePromotion.Add(s);
            }
        }

        public bool CopyPromotionProductFromSourceToTarget(int targetpromotionid, int sourcepromotionid)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            bool ds = client.SQSAdmin_Promotion_CopyPromotionProductsFromSourceToTarget(targetpromotionid, sourcepromotionid);
            client.Close();
            return ds;

        }


        public void LoadHomeModelQuantity(string stateid, string areaid, string groupid, string brandid)
        {
            HomeModelQty.Clear();

            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_QuantityManagement_GetHomeModelQuantity(stateid, areaid, groupid, brandid);
            DataSet facadeds = client.SQSAdmin_QuantityManagement_GetHomeFacadeQuantity(stateid, areaid, groupid, brandid);
            client.Close();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                List<HomeAndQuantity> hl = new List<HomeAndQuantity>();
                HomeModelAndQuantity hq = new HomeModelAndQuantity();
                hq.HomeModel = dr["homemodel"].ToString();
                hq.Quantity = decimal.Parse(dr["quantity"].ToString());
                hq.UpdatedQuantity = decimal.Parse(dr["quantity"].ToString());
                if ((dr["multipleqty"].ToString().ToUpper() == "1" || dr["multipleqty"].ToString().ToUpper() == "TRUE"))
                {
                    hq.MultipleQuantity = true;
                }
                else
                {
                    hq.MultipleQuantity = false;
                }

                hq.Display1 = dr["Display1"].ToString();
                hq.Display2 = dr["Display2"].ToString();
                hq.Display3 = dr["Display3"].ToString();
                hq.Display4 = dr["Display4"].ToString();
                hq.Display5 = dr["Display5"].ToString();
                hq.Display6 = dr["Display6"].ToString();
                hq.BMPImage = dr["warningimage"].ToString();

                foreach (DataRow dr2 in facadeds.Tables[0].Rows)
                {
                    if (dr2["homemodel"].ToString() == hq.HomeModel)
                    {
                        HomeAndQuantity hh = new HomeAndQuantity();
                        hh.HomeID = int.Parse(dr2["homeid"].ToString());
                        hh.HomeName = dr2["homemodel"].ToString() + " " + dr2["facade"].ToString();
                        hh.Quantity = decimal.Parse(dr2["quantity"].ToString());
                        hh.UpdatedQuantity = decimal.Parse(dr2["quantity"].ToString());
                        //if (dr2["allowedit"].ToString() == "1" || dr2["allowedit"].ToString().ToUpper() == "TURE")
                        //{
                        //    hh.Enabled = true;
                        //}
                        //else
                        //{
                        //    hh.Enabled = false;
                        //}

                        hl.Add(hh);
                    }
                }
                hq.HomeList = hl;
                HomeModelQty.Add(hq);

            }
        }
        public retrunValue LoadHomeModelQuantity_Multithread(string stateid, string areaid, string groupid, string brandid)
        {
            //HomeModelQty.Clear();
            retrunValue rv = new retrunValue();
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_QuantityManagement_GetHomeModelQuantity(stateid, areaid, groupid, brandid);
            DataSet facadeds = client.SQSAdmin_QuantityManagement_GetHomeFacadeQuantity(stateid, areaid, groupid, brandid);
            client.Close();
            rv.HomeModelSet = ds;
            rv.FacadeSet = facadeds;

            return rv;
            /*
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                List<HomeAndQuantity> hl = new List<HomeAndQuantity>();
                HomeModelAndQuantity hq = new HomeModelAndQuantity();
                hq.HomeModel = dr["homemodel"].ToString();
                hq.Quantity = decimal.Parse(dr["quantity"].ToString());
                hq.UpdatedQuantity = decimal.Parse(dr["quantity"].ToString());
                if ((dr["multipleqty"].ToString().ToUpper() == "1" || dr["multipleqty"].ToString().ToUpper() == "TRUE"))
                {
                    hq.MultipleQuantity = true;
                }
                else
                {
                    hq.MultipleQuantity = false;
                }

                hq.Display1 = dr["Display1"].ToString();
                hq.Display2 = dr["Display2"].ToString();
                hq.Display3 = dr["Display3"].ToString();
                hq.Display4 = dr["Display4"].ToString();
                hq.Display5 = dr["Display5"].ToString();
                hq.Display6 = dr["Display6"].ToString();
                hq.BMPImage = dr["warningimage"].ToString();

                foreach (DataRow dr2 in facadeds.Tables[0].Rows)
                {
                    if (dr2["homemodel"].ToString() == hq.HomeModel)
                    {
                        HomeAndQuantity hh = new HomeAndQuantity();
                        hh.HomeID = int.Parse(dr2["homeid"].ToString());
                        hh.HomeName = dr2["homemodel"].ToString() + " " + dr2["facade"].ToString();
                        hh.Quantity = decimal.Parse(dr2["quantity"].ToString());
                        hh.UpdatedQuantity = decimal.Parse(dr2["quantity"].ToString());
                        //if (dr2["allowedit"].ToString() == "1" || dr2["allowedit"].ToString().ToUpper() == "TURE")
                        //{
                        //    hh.Enabled = true;
                        //}
                        //else
                        //{
                        //    hh.Enabled = false;
                        //}

                        hl.Add(hh);
                    }
                }
                hq.HomeList = hl;
                HomeModelQty.Add(hq);
            
            }
             * */
        }
        public bool UpdateQuantityAtHomeModel(string stateid, string areaid, string groupid, string brandid, string homemodel, string quantity, string usercode)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            bool ds = client.SQSAdmin_QuantityManagement_UpdateQuantityForAreaGroupAtHomeModel(stateid, areaid, groupid, brandid, homemodel, quantity, usercode);
            client.Close();
            return ds;

        }

        public bool UpdateQuantityAtHomeFacade(string areaid, string groupid, string homeid, string quantity, string usercode)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            bool ds = client.SQSAdmin_QuantityManagement_UpdateQuantityForAreaGroupAtHomeFacade(areaid, groupid, homeid, quantity, usercode);
            client.Close();
            return ds;

        }

        public bool RefreshQuantity(string stateid, string areaid, string groupid, string usercode)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            bool ds = client.SQSAdmin_QuantityManagement_RefreshQuantity(stateid, areaid, groupid, usercode);
            client.Close();
            return ds;

        }

        public bool BulkUpdateQuantity(string homeid, string pagidstring, string quantity, string usercode)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            bool ds = client.SQSAdmin_HomeConfiguration_BulkUpdateQuantity(homeid, pagidstring, quantity, usercode);
            client.Close();
            return ds;

        }
        public bool BulkUpdateHomeModelQuantity(string brandid, string areaid, string groupid, string homestring, string quantity, string usercode)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            bool ds = client.SQSAdmin_HomeConfiguration_BulkUpdateHomeModelQuantity(brandid, areaid, groupid, homestring, quantity, usercode);
            client.Close();
            return ds;

        }


        public System.Data.DataTable readExcelFileToDataTable(string filename, string SheetsName)
        {

            System.Data.DataTable ResultTable = new System.Data.DataTable();
            try
            {
                string connectionString = string.Empty;

                if (filename.Contains(".xlsx"))
                {
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended Properties=Excel 12.0;";
                }
                else
                {
                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename + ";Extended Properties=\"Excel 8.0;HDR=Yes;TypeGuessRows=0;ImportMixedTypes=Text;IMEX=1\"";
                }

                OleDbCommand selectCommand = new OleDbCommand();
                OleDbConnection connection = new OleDbConnection();
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                connection.ConnectionString = connectionString;

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                System.Data.DataTable dtSchema = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                selectCommand.CommandText = "SELECT * FROM [" + SheetsName + "]";
                selectCommand.Connection = connection;
                adapter.SelectCommand = selectCommand;

                ResultTable.TableName = SheetsName.Replace("$", "").Replace("'", "");
                adapter.Fill(ResultTable);

                connection.Close();

                return ResultTable;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void GenerateExcel(string formname, string formdirection)
        {
            string flatlistYN = "Y,N";
            string flatliststate = "";
            string flatlistproductcategory = "";
            string flatlistproductcode = "";
            string flatlistdisplaycode = "";
            string flatlistcostcenter = "";
            string flatlistuom = "";
            string flatregion = "";
            string tempname = formname.Substring(0, 1).ToUpper() + formname.Substring(1, formname.Length - 1).ToLower() + "Sheet";
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            try
            {
                Application excel = new Application();
                Workbook oWB = excel.Application.Workbooks.Add(true);
                Worksheet oSheet = oWB.ActiveSheet as Worksheet;
                oSheet.Name = tempname;

                //Worksheet oSheet2 = oWB.Sheets.Add(Type.Missing, Type.Missing, 1, Type.Missing) as Worksheet;
                //oSheet2.Name = "sheet2";
                //oSheet2.Select();
                int idxuom = 2;
                int idxpcat = 2;
                int idxpcode = 2;
                int idxdcode = 2;
                int idxccode = 2;
                #region product sample sheet
                if (formname.ToUpper() == "PRODUCT")
                {

                    // get state 
                    DataSet dsTemp = client.SQSAdmin_Generic_GetState();
                    foreach (DataRow dr in dsTemp.Tables[0].Rows)
                    {
                        if (flatliststate == "")
                        {
                            flatliststate = dr["stateabbreviation"].ToString();
                        }
                        else
                        {
                            flatliststate = flatliststate + "," + dr["stateabbreviation"].ToString();
                        }
                    }

                    // get product category
                    dsTemp = client.SQSAdmin_Generic_GetProductCategory();

                    foreach (DataRow dr in dsTemp.Tables[0].Rows)
                    {
                        if (flatlistproductcategory == "")
                        {
                            flatlistproductcategory = dr["dropdown"].ToString();
                        }
                        else
                        {
                            flatlistproductcategory = flatlistproductcategory + "," + dr["dropdown"].ToString();
                        }
                        oSheet.Cells[idxpcat, 27] = dr["dropdown"].ToString();
                        idxpcat++;

                    }

                    // get product code
                    dsTemp = client.SQSAdmin_Generic_GetProductCode();

                    foreach (DataRow dr in dsTemp.Tables[0].Rows)
                    {
                        if (flatlistproductcode == "")
                        {
                            flatlistproductcode = dr["dropdown"].ToString();
                        }
                        else
                        {
                            flatlistproductcode = flatlistproductcode + "," + dr["dropdown"].ToString();
                        }
                        oSheet.Cells[idxpcode, 28] = dr["dropdown"].ToString();
                        idxpcode++;
                    }

                    // get price display code
                    dsTemp = client.SQSAdmin_Generic_GetProductDisplayCode("0");
                    foreach (DataRow dr in dsTemp.Tables[0].Rows)
                    {
                        if (flatlistdisplaycode == "")
                        {
                            flatlistdisplaycode = dr["dropdown"].ToString();
                        }
                        else
                        {
                            flatlistdisplaycode = flatlistdisplaycode + "," + dr["dropdown"].ToString();
                        }

                        oSheet.Cells[idxdcode, 29] = dr["dropdown"].ToString();
                        idxdcode++;
                    }

                    // get cost center
                    dsTemp = client.SQSAdmin_Generic_GetProductCostCenterCode();

                    foreach (DataRow dr in dsTemp.Tables[0].Rows)
                    {
                        if (flatlistcostcenter == "")
                        {
                            flatlistcostcenter = dr["dropdown"].ToString();
                        }
                        else
                        {
                            flatlistcostcenter = flatlistcostcenter + "," + dr["dropdown"].ToString();
                        }
                        oSheet.Cells[idxccode, 30] = dr["dropdown"].ToString();
                        idxccode++;
                    }


                    // get UOM
                    dsTemp = client.SQSAdmin_Generic_GetProductUOM();

                    foreach (DataRow dr in dsTemp.Tables[0].Rows)
                    {
                        if (flatlistuom == "")
                        {
                            flatlistuom = dr["dropdown"].ToString();
                        }
                        else
                        {
                            flatlistuom = flatlistuom + "," + dr["dropdown"].ToString();
                        }
                        oSheet.Cells[idxuom, 31] = dr["dropdown"].ToString();
                        idxuom++;
                    }

                    oSheet.Select();

                    int ColumnIndex = 1;
                    Range rng;

                    // build the column header text;

                    excel.Cells[1, ColumnIndex] = "Product ID";
                    rng = (Range)excel.Cells[1, 1];
                    rng.Font.Bold = true;
                    ColumnIndex++;

                    excel.Cells[1, ColumnIndex] = "Product Name";
                    rng = (Range)excel.Cells[1, 2];
                    rng.Font.Bold = true;
                    ColumnIndex++;

                    excel.Cells[1, ColumnIndex] = "Product Description";
                    rng = (Range)excel.Cells[1, ColumnIndex];
                    rng.Font.Bold = true;
                    ColumnIndex++;


                    excel.Cells[1, ColumnIndex] = "UOM";
                    rng = (Range)excel.Cells[1, ColumnIndex];
                    rng.Font.Bold = true;
                    ColumnIndex++;


                    excel.Cells[1, ColumnIndex] = "Sort Order";
                    rng = (Range)excel.Cells[1, ColumnIndex];
                    rng.Font.Bold = true;
                    ColumnIndex++;

                    excel.Cells[1, ColumnIndex] = "Active";
                    rng = (Range)excel.Cells[1, ColumnIndex];
                    rng.Font.Bold = true;
                    ColumnIndex++;

                    excel.Cells[1, ColumnIndex] = "BC Active";
                    rng = (Range)excel.Cells[1, ColumnIndex];
                    rng.Font.Bold = true;
                    ColumnIndex++;

                    excel.Cells[1, ColumnIndex] = "Product Category";
                    rng = (Range)excel.Cells[1, ColumnIndex];
                    rng.Font.Bold = true;
                    ColumnIndex++;

                    excel.Cells[1, ColumnIndex] = "Product Code";
                    rng = (Range)excel.Cells[1, ColumnIndex];
                    rng.Font.Bold = true;
                    ColumnIndex++;

                    excel.Cells[1, ColumnIndex] = "Price Display Code";
                    rng = (Range)excel.Cells[1, ColumnIndex];
                    rng.Font.Bold = true;
                    ColumnIndex++;

                    excel.Cells[1, ColumnIndex] = "Cost Centre Code";
                    rng = (Range)excel.Cells[1, ColumnIndex];
                    rng.Font.Bold = true;
                    ColumnIndex++;

                    excel.Cells[1, ColumnIndex] = "Mini Bill Completed";
                    rng = (Range)excel.Cells[1, ColumnIndex];
                    rng.Font.Bold = true;
                    ColumnIndex++;

                    excel.Cells[1, ColumnIndex] = "State";
                    rng = (Range)excel.Cells[1, ColumnIndex];
                    rng.Font.Bold = true;
                    ColumnIndex++;

                    excel.Cells[1, ColumnIndex] = "IsStudioMProduct";
                    rng = (Range)excel.Cells[1, ColumnIndex];
                    rng.Font.Bold = true;
                    ColumnIndex++;

                    excel.Cells[1, ColumnIndex] = "Internal Description";
                    rng = (Range)excel.Cells[1, ColumnIndex];
                    rng.Font.Bold = true;
                    ColumnIndex++;

                    excel.Cells[1, ColumnIndex] = "Additional Notes";
                    rng = (Range)excel.Cells[1, ColumnIndex];
                    rng.Font.Bold = true;
                    ColumnIndex++;

                    excel.Cells[1, ColumnIndex] = "Operation Only";
                    rng = (Range)excel.Cells[1, ColumnIndex];
                    rng.Font.Bold = true;
                    ColumnIndex++;

                    int rowIndex = 2;



                    string forumula = "='" + tempname + "'!AE1:AE" + (idxuom - 1).ToString();

                    var cell6 = (Range)excel.Columns[4];
                    cell6.Validation.Delete();
                    //cell6.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatlistuom, Type.Missing);
                    cell6.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, forumula, Type.Missing);
                    cell6.Validation.IgnoreBlank = true;
                    cell6.Validation.InCellDropdown = true;

                    ((Range)excel.Cells[1, 4]).Validation.Delete();


                    var cell7 = (Range)excel.Columns[6];
                    cell7.Validation.Delete();
                    cell7.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatlistYN, Type.Missing);
                    cell7.Validation.IgnoreBlank = true;
                    cell7.Validation.InCellDropdown = true;
                    ((Range)excel.Cells[1, 6]).Validation.Delete();

                    var cell8 = (Range)excel.Columns[7];
                    cell8.Validation.Delete();
                    cell8.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatlistYN, Type.Missing);
                    cell8.Validation.IgnoreBlank = true;
                    cell8.Validation.InCellDropdown = true;
                    ((Range)excel.Cells[1, 7]).Validation.Delete();


                    forumula = "='" + tempname + "'!AA1:AA" + (idxpcat - 1).ToString();
                    var cell2 = (Range)excel.Columns[8];
                    cell2.Validation.Delete();
                    //cell2.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatlistproductcategory, Type.Missing);
                    cell2.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, forumula, Type.Missing);
                    cell2.Validation.IgnoreBlank = true;
                    cell2.Validation.InCellDropdown = true;
                    ((Range)excel.Cells[1, 8]).Validation.Delete();

                    forumula = "='" + tempname + "'!AB1:AB" + (idxpcode - 1).ToString();
                    var cell3 = (Range)excel.Columns[9];
                    cell3.Validation.Delete();
                    //cell3.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatlistproductcode, Type.Missing);
                    cell3.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, forumula, Type.Missing);
                    cell3.Validation.IgnoreBlank = true;
                    cell3.Validation.InCellDropdown = true;
                    ((Range)excel.Cells[1, 9]).Validation.Delete();

                    forumula = "='" + tempname + "'!AC1:AC" + (idxdcode - 1).ToString();
                    var cell4 = (Range)excel.Columns[10];
                    cell4.Validation.Delete();
                    //cell4.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatlistdisplaycode, Type.Missing);
                    cell4.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, forumula, Type.Missing);
                    cell4.Validation.IgnoreBlank = true;
                    cell4.Validation.InCellDropdown = true;
                    ((Range)excel.Cells[1, 10]).Validation.Delete();

                    forumula = "='" + tempname + "'!AD1:AD" + (idxccode - 1).ToString();
                    var cell5 = (Range)excel.Columns[11];
                    cell5.Validation.Delete();
                    //cell5.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatlistcostcenter, Type.Missing);
                    cell5.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, forumula, Type.Missing);
                    cell5.Validation.IgnoreBlank = true;
                    cell5.Validation.InCellDropdown = true;

                    ((Range)excel.Cells[1, 11]).Validation.Delete();

                    var cell9 = (Range)excel.Columns[12];
                    cell9.Validation.Delete();
                    cell9.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatlistYN, Type.Missing);
                    cell9.Validation.IgnoreBlank = true;
                    cell9.Validation.InCellDropdown = true;
                    ((Range)excel.Cells[1, 12]).Validation.Delete();

                    var cell = (Range)excel.Columns[13];
                    cell.Validation.Delete();
                    cell.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatliststate, Type.Missing);
                    cell.Validation.IgnoreBlank = true;
                    cell.Validation.InCellDropdown = true;
                    ((Range)excel.Cells[1, 13]).Validation.Delete();

                    var cell10 = (Range)excel.Columns[14];
                    cell10.Validation.Delete();
                    cell10.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatlistYN, Type.Missing);
                    cell10.Validation.IgnoreBlank = true;
                    cell10.Validation.InCellDropdown = true;
                    ((Range)excel.Cells[1, 14]).Validation.Delete();

                    var cell11 = (Range)excel.Columns[17];
                    cell11.Validation.Delete();
                    cell11.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatlistYN, Type.Missing);
                    cell11.Validation.IgnoreBlank = true;
                    cell11.Validation.InCellDropdown = true;
                    ((Range)excel.Cells[1, 17]).Validation.Delete();

                }
                #endregion


                #region price sample sheet vertical format
                if (formname.ToUpper() == "PRICE" && formdirection.ToUpper() == "VERTICAL")
                {
                    DataSet dsTemp = client.SQSAdmin_Generic_GetRegionList(LoginState);

                    foreach (DataRow dr in dsTemp.Tables[0].Rows)
                    {
                        if (flatregion == "")
                        {
                            flatregion = dr["regionname"].ToString();
                        }
                        else
                        {
                            flatregion = flatregion + "," + dr["regionname"].ToString();
                        }
                    }

                    int ColumnIndex2 = 1;
                    Range rng;

                    // build the column header text;

                    excel.Cells[1, ColumnIndex2] = "ProductID";
                    rng = (Range)excel.Cells[1, ColumnIndex2];
                    rng.Font.Bold = true;
                    ColumnIndex2++;

                    excel.Cells[1, ColumnIndex2] = "RegionName";
                    rng = (Range)excel.Cells[1, ColumnIndex2];
                    rng.Font.Bold = true;
                    ColumnIndex2++;

                    excel.Cells[1, ColumnIndex2] = "EffectiveDate";
                    rng = (Range)excel.Cells[1, ColumnIndex2];
                    rng.Font.Bold = true;
                    ColumnIndex2++;

                    excel.Cells[1, ColumnIndex2] = "DirectBuildCost";
                    rng = (Range)excel.Cells[1, ColumnIndex2];
                    rng.Font.Bold = true;
                    ColumnIndex2++;

                    excel.Cells[1, ColumnIndex2] = "BuildingTransferCost";
                    rng = (Range)excel.Cells[1, ColumnIndex2];
                    rng.Font.Bold = true;
                    ColumnIndex2++;


                    excel.Cells[1, ColumnIndex2] = "SellPrice";
                    rng = (Range)excel.Cells[1, ColumnIndex2];
                    rng.Font.Bold = true;
                    ColumnIndex2++;

                    excel.Cells[1, ColumnIndex2] = "TargetMarginPercent";
                    rng = (Range)excel.Cells[1, ColumnIndex2];
                    rng.Font.Bold = true;
                    ColumnIndex2++;

                    excel.Cells[1, ColumnIndex2] = "DerivedCost";
                    rng = (Range)excel.Cells[1, ColumnIndex2];
                    rng.Font.Bold = true;
                    ColumnIndex2++;



                    int rowIndex2 = 2;

                    //excel.Cells[rowIndex2, 1] = "TT-XXX-YYY-000";


                    //var cell2 = (Range)excel.Cells[rowIndex2, 2];
                    var cell2 = (Range)excel.Columns[2];
                    cell2.Validation.Delete();
                    cell2.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatregion, Type.Missing);
                    cell2.Validation.IgnoreBlank = true;
                    cell2.Validation.InCellDropdown = true;
                    ((Range)excel.Cells[1, 2]).Validation.Delete();

                    //excel.Cells[rowIndex2, 3] = "13/05/2015";
                    //excel.Cells[rowIndex2, 4] = "130.05";
                    //excel.Cells[rowIndex2, 5] = "195.45";

                    //var cell7 = (Range)excel.Cells[rowIndex2, 6];
                    //var cell7 = (Range)excel.Columns[7];
                    //cell7.Validation.Delete();
                    //cell7.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatlistYN, Type.Missing);
                    //cell7.Validation.IgnoreBlank = true;
                    //cell7.Validation.InCellDropdown = true;
                    //((Range)excel.Cells[1, 8]).Validation.Delete();


                    var cell8 = (Range)excel.Columns[8];
                    cell8.Validation.Delete();
                    cell8.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatlistYN, Type.Missing);
                    cell8.Validation.IgnoreBlank = true;
                    cell8.Validation.InCellDropdown = true;
                    ((Range)excel.Cells[1, 8]).Validation.Delete();
                    //excel.Cells[rowIndex2, 7] = "Please delete this sample row which contains the validation of region after complete the form.";
                }
                #endregion

                #region horizontal format for price
                if (formname.ToUpper() == "PRICE" && formdirection.ToUpper() == "HORIZONTAL")
                {
                    DataSet dsTemp = client.SQSAdmin_Generic_GetRegionList(LoginState);


                    int ColumnIndex2 = 1;
                    Range rng;

                    // build the column header text;

                    excel.Cells[1, ColumnIndex2] = "ProductID";
                    rng = (Range)excel.Cells[1, ColumnIndex2];
                    rng.Font.Bold = true;
                    ColumnIndex2++;

                    excel.Cells[1, ColumnIndex2] = "HouseType";
                    rng = (Range)excel.Cells[1, ColumnIndex2];
                    rng.Font.Bold = true;
                    ColumnIndex2++;

                    excel.Cells[1, ColumnIndex2] = "EffectiveDate";
                    rng = (Range)excel.Cells[1, ColumnIndex2];
                    rng.Font.Bold = true;
                    ColumnIndex2++;


                    foreach (DataRow dr in dsTemp.Tables[0].Rows)
                    {
                        excel.Cells[1, ColumnIndex2] = dr["BCCode"].ToString().TrimStart().TrimEnd() + "_DirectBuildCost";
                        rng = (Range)excel.Cells[1, ColumnIndex2];
                        rng.Font.Bold = true;
                        ColumnIndex2++;
                    }

                    foreach (DataRow dr in dsTemp.Tables[0].Rows)
                    {
                        excel.Cells[1, ColumnIndex2] = dr["BCCode"].ToString().TrimStart().TrimEnd() + "_BuildingTransferCost";
                        rng = (Range)excel.Cells[1, ColumnIndex2];
                        rng.Font.Bold = true;
                        ColumnIndex2++;
                    }


                    foreach (DataRow dr in dsTemp.Tables[0].Rows)
                    {
                        excel.Cells[1, ColumnIndex2] = dr["BCCode"].ToString().TrimStart().TrimEnd() + "_SellPrice";
                        rng = (Range)excel.Cells[1, ColumnIndex2];
                        rng.Font.Bold = true;
                        ColumnIndex2++;
                    }

                    foreach (DataRow dr in dsTemp.Tables[0].Rows)
                    {
                        excel.Cells[1, ColumnIndex2] = dr["BCCode"].ToString().TrimStart().TrimEnd() + "_TargetMarginPercent";
                        rng = (Range)excel.Cells[1, ColumnIndex2];
                        rng.Font.Bold = true;
                        ColumnIndex2++;
                    }

                    excel.Cells[1, ColumnIndex2] = "DerivedCost";
                    rng = (Range)excel.Cells[1, ColumnIndex2];
                    rng.Font.Bold = true;
                    ColumnIndex2++;

                    int rowIndex2 = 2;
                    excel.Cells[rowIndex2, 1] = "TT-XXX-YYY-000";
                    excel.Cells[rowIndex2, 2] = "Leave blank if no code";
                    excel.Cells[rowIndex2, 3] = "13/05/2015";

                    ColumnIndex2 = 4;
                    foreach (DataRow dr in dsTemp.Tables[0].Rows)
                    {
                        excel.Cells[rowIndex2, ColumnIndex2] = "12345.67";
                        ColumnIndex2++;
                    }
                    foreach (DataRow dr in dsTemp.Tables[0].Rows)
                    {
                        excel.Cells[rowIndex2, ColumnIndex2] = "34567.89";
                        ColumnIndex2++;
                    }

                    foreach (DataRow dr in dsTemp.Tables[0].Rows)
                    {
                        excel.Cells[rowIndex2, ColumnIndex2] = "234567.99";
                        ColumnIndex2++;
                    }
                    foreach (DataRow dr in dsTemp.Tables[0].Rows)
                    {
                        excel.Cells[rowIndex2, ColumnIndex2] = "27.5";
                        ColumnIndex2++;
                    }
                    var cell7 = (Range)excel.Cells[rowIndex2, ColumnIndex2];
                    cell7.Validation.Delete();
                    cell7.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatlistYN, Type.Missing);
                    cell7.Validation.IgnoreBlank = true;
                    cell7.Validation.InCellDropdown = true;
                    ColumnIndex2++;

                    excel.Cells[rowIndex2, ColumnIndex2] = "Please delete this sample row after complete the form.";
                }
                #endregion


                #region quantity sample sheet
                if (formname.ToUpper() == "QUANTITY")
                {

                    Range rng;

                    // build the column header text;

                    excel.Cells[1, 1] = "HomeProductID";
                    rng = (Range)excel.Cells[1, 1];
                    rng.Font.Bold = true;

                    excel.Cells[1, 2] = "ProductAreaGroupID";
                    rng = (Range)excel.Cells[1, 2];
                    rng.Font.Bold = true;

                    excel.Cells[1, 3] = "Quantity";
                    rng = (Range)excel.Cells[1, 3];
                    rng.Font.Bold = true;

                    int rowIndex = 2;

                    excel.Cells[rowIndex, 1] = "QM9ADE33TRAS1";
                    excel.Cells[rowIndex, 2] = "12345";
                    excel.Cells[rowIndex, 3] = "108.50";
                    excel.Cells[rowIndex, 4] = "Sample data row. please delete this row after complete the excel.";
                    rng = (Range)excel.Cells[rowIndex, 3];
                    rng.NumberFormat = "0.00";

                    rowIndex++;

                    excel.Cells[rowIndex, 1] = "QM9ADE33TRAS1";
                    excel.Cells[rowIndex, 2] = "67890";
                    excel.Cells[rowIndex, 3] = "45.00";
                    excel.Cells[rowIndex, 4] = "Sample data row. please delete this row after complete the excel.";
                    rng = (Range)excel.Cells[rowIndex, 3];
                    rng.NumberFormat = "0";



                }
                #endregion


                excel.Visible = true;
                oSheet.Columns.AutoFit();
                oSheet.Activate();
                oSheet.get_Range("AA:AA", Type.Missing).EntireColumn.Hidden = true;
                oSheet.get_Range("AB:AB", Type.Missing).EntireColumn.Hidden = true;
                oSheet.get_Range("AC:AC", Type.Missing).EntireColumn.Hidden = true;
                oSheet.get_Range("AD:AD", Type.Missing).EntireColumn.Hidden = true;
                oSheet.get_Range("AE:AE", Type.Missing).EntireColumn.Hidden = true;
                //worksheet.Protect(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);


            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region properties
        public int LoginState
        {
            get
            {
                return loginstateid;
            }
            set
            {
                loginstateid = value;
                OnPropertyChanged("LoginState");
            }
        }

        public ObservableCollection<PromotionProduct> SQSPromotionProduct
        {
            get
            {
                return _promotionproduct;
            }
            set
            {
                _promotionproduct = value;
                OnPropertyChanged("SQSPromotionProduct");
            }
        }

        public ObservableCollection<FullMultiplePromotion> SQSFullMultiplePromotion
        {
            get
            {
                return _multipromo;
            }
            set
            {
                _multipromo = value;
                OnPropertyChanged("SQSFullMultiplePromotion");
            }
        }
        public ObservableCollection<Product> SourceProduct
        {
            get
            {
                return _sourceproduct;
            }
            set
            {
                _sourceproduct = value;
                OnPropertyChanged("SourceProduct");
            }
        }
        public ObservableCollection<Product> TargetProduct
        {
            get
            {
                return _targetproduct;
            }
            set
            {
                _targetproduct = value;
                OnPropertyChanged("TargetProduct");
            }
        }

        public ObservableCollection<MultiplePromotion> SQSMultiplePromotion
        {
            get
            {
                return _multiplepromotion;
            }
            set
            {
                _multiplepromotion = value;
                OnPropertyChanged("SQSMultiplePromotion");
            }
        }
        #region Brands
        private Dictionary<string, object> _itemsBrand = new Dictionary<string, object>();
        private Dictionary<string, object> _selectedItemsBrand = new Dictionary<string, object>();

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
        public ObservableCollection<Brand> SQSBrandWithDefault
        {
            get
            {
                return _brandwithdefault;
            }
            set
            {
                _brandwithdefault = value;
                OnPropertyChanged("SQSBrandWithDefault");
            }
        }
        #endregion
        public ObservableCollection<Storey> SQSStorey
        {
            get
            {
                return _storey;
            }
            set
            {
                _storey = value;
                OnPropertyChanged("SQSStorey");
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
        public ObservableCollection<State> SQSState2
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

        #region Regions
        private Dictionary<string, object> _itemsRegion = new Dictionary<string, object>();
        private Dictionary<string, object> _selectedItemsRegion = new Dictionary<string, object>();
        public Dictionary<string, object> ItemsRegion
        {
            get
            {
                return _itemsRegion;
            }
            set
            {
                _itemsRegion = value;
                OnPropertyChanged("ItemsRegion");
            }
        }
        public Dictionary<string, object> SelectedItemsRegion
        {
            get
            {
                return _selectedItemsRegion;
            }
            set
            {
                _selectedItemsRegion = value;
                OnPropertyChanged("SelectedItemsRegion");
            }
        }
        public ObservableCollection<Region> SQSRegion
        {
            get
            {
                return _region;
            }
            set
            {
                _region = value;
                OnPropertyChanged("SQSRegion");
            }
        }
        public ObservableCollection<Region> SQSRegionWithAll
        {
            get
            {
                return _regionwithall;
            }
            set
            {
                _regionwithall = value;
                OnPropertyChanged("SQSRegionWithAll");
            }
        }
        public ObservableCollection<Region> SQSRegionWithDefault
        {
            get
            {
                return _regionwithdefault;
            }
            set
            {
                _regionwithdefault = value;
                OnPropertyChanged("SQSRegionWithDefault");
            }
        }
        #endregion

        #region Home
        private Dictionary<string, object> _itemsHome = new Dictionary<string, object>();
        private Dictionary<string, object> _selectedItemsHome = new Dictionary<string, object>();
        public Dictionary<string, object> ItemsHome
        {
            get
            {
                return _itemsHome;
            }
            set
            {
                _itemsHome = value;
                OnPropertyChanged("ItemsHome");
            }
        }
        public Dictionary<string, object> SelectedItemsHome
        {
            get
            {
                return _selectedItemsHome;
            }
            set
            {
                _selectedItemsHome = value;
                OnPropertyChanged("SelectedItemsHome");
            }
        }
        public ObservableCollection<Home> SQSHomeWithDefault
        {
            get
            {
                return _homewithdefault;
            }
            set
            {
                _homewithdefault = value;
                OnPropertyChanged("SQSHomeWithDefault");
            }
        }
        #endregion

        public ObservableCollection<Specification> Specifications
        {
            get
            {
                return _specifications;
            }
            set
            {
                _specifications = value;
                OnPropertyChanged("Specifications");
            }
        }

        private Dictionary<string, object> _itemsSystemDocumentTypes;
        private Dictionary<string, object> _selectedItemsSystemDocumentTypes;
        public Dictionary<string, object> ItemsSystemDocumentTypes
        {
            get
            {
                return _itemsSystemDocumentTypes;
            }
            set
            {
                _itemsSystemDocumentTypes = value;
                OnPropertyChanged("ItemsSystemDocumentTypes");
            }
        }
        public Dictionary<string, object> SelectedItemsSystemDocumentTypes
        {
            get
            {
                return _selectedItemsSystemDocumentTypes;
            }
            set
            {
                _selectedItemsSystemDocumentTypes = value;
                OnPropertyChanged("SelectedItemsSystemDocumentTypes");
            }
        }

        public ObservableCollection<DisplayCode> AttachmentTypes
        {
            get
            {
                return _attachmenttypes;
            }
            set
            {
                _attachmenttypes = value;
                OnPropertyChanged("AttachmentTypes");
            }
        }
        public ObservableCollection<Status> SQSStatus
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                OnPropertyChanged("SQSStatus");
            }
        }

        public ObservableCollection<PromotionType> SQSPromotionType
        {
            get
            {
                return _promotype;
            }
            set
            {
                _promotype = value;
                OnPropertyChanged("SQSPromotionType");
            }
        }

        public ObservableCollection<Area> SQSAllAreaForAvailableProduct
        {
            get
            {
                return _area1;
            }
            set
            {
                _area1 = value;
                OnPropertyChanged("SQSAllAreaForAvailableProduct");
            }
        }

        public ObservableCollection<Group> SQSAllGroupForAvailableProduct
        {
            get
            {
                return _group1;
            }
            set
            {
                _group1 = value;
                OnPropertyChanged("SQSAllGroupForAvailableProduct");
            }
        }

        public ObservableCollection<Area> SQSAreaForPromotionProduct
        {
            get
            {
                return _area2;
            }
            set
            {
                _area2 = value;
                OnPropertyChanged("SQSAreaForPromotionProduct");
            }
        }

        public ObservableCollection<Group> SQSGroupForPromotionProduct
        {
            get
            {
                return _group2;
            }
            set
            {
                _group2 = value;
                OnPropertyChanged("SQSAllGroupForAvailableProduct");
            }
        }

        public ObservableCollection<Group> SQSFilteredGroupForAvailableProduct
        {
            get
            {
                return _group3;
            }
            set
            {
                _group3 = value;
                OnPropertyChanged("SQSFilteredGroupForAvailableProduct");
            }
        }

        public ObservableCollection<Group> SQSFilteredGroupForAvailableProduct2
        {
            get
            {
                return _group4;
            }
            set
            {
                _group4 = value;
                OnPropertyChanged("SQSFilteredGroupForAvailableProduct2");
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
        public ObservableCollection<DisplayCode> PriceDisplayCode
        {
            get
            {
                return _pricedisplaycode;
            }
            set
            {
                _pricedisplaycode = value;
                OnPropertyChanged("PriceDisplayCode");
            }
        }

        public ObservableCollection<Home> SQSHome
        {
            get
            {
                return _home;
            }
            set
            {
                _home = value;
                OnPropertyChanged("SQSHome");
            }
        }


        public ObservableCollection<Area> ExistingProductArea
        {
            get
            {
                return _existingproductarea;
            }
            set
            {
                _existingproductarea = value;
                OnPropertyChanged("ExistingProductArea");
            }
        }
        public ObservableCollection<Area> AvailableProductArea
        {
            get
            {
                return _availableproductarea;
            }
            set
            {
                _availableproductarea = value;
                OnPropertyChanged("AvailableProductArea");
            }
        }

        public ObservableCollection<Group> AvailableProductGroup
        {
            get
            {
                return _availableproductgroup;
            }
            set
            {
                _availableproductgroup = value;
                OnPropertyChanged("AvailableProductGroup");
            }
        }

        public ObservableCollection<Area> AllAreas
        {
            get
            {
                return _allareas;
            }
            set
            {
                _allareas = value;
                OnPropertyChanged("AllAreas");
            }
        }
        public ObservableCollection<Group> AllGroups
        {
            get
            {
                return _allgroups;
            }
            set
            {
                _allgroups = value;
                OnPropertyChanged("AllGroups");
            }
        }
        public ObservableCollection<Group> ExistingProductGroup
        {
            get
            {
                return _existingproductgroup;
            }
            set
            {
                _existingproductgroup = value;
                OnPropertyChanged("ExistingProductGroup");
            }
        }

        public ObservableCollection<HomeConfigurationItem> HomeExistingPAG
        {
            get
            {
                return _existingpag;
            }
            set
            {
                _existingpag = value;
                OnPropertyChanged("HomeExistingPAG");
            }
        }

        public ObservableCollection<DisplayHome> SQSDisplayHome
        {
            get
            {
                return _displayhome;
            }
            set
            {
                _displayhome = value;
                OnPropertyChanged("SQSDisplayHome");
            }
        }
        public ObservableCollection<HomeConfigurationItem> HomeAvailablePAG
        {
            get
            {
                return _availablepag;
            }
            set
            {
                _availablepag = value;
                OnPropertyChanged("HomeAvailablePAG");
            }
        }

        public ObservableCollection<HomeModelAndQuantity> HomeModelQty
        {
            get
            {
                return _homemodelquantity;
            }
            set
            {
                _homemodelquantity = value;
                OnPropertyChanged("HomeModelQty");
            }
        }


        public ObservableCollection<BasePriceHoldingDays> BasePriceHoldDays
        {
            get
            {
                return _basepriceholddays;
            }
            set
            {
                _basepriceholddays = value;
                OnPropertyChanged("BasePriceHoldDays");
            }
        }
        #endregion

        #region data contract

        public class Brand
        {
            public int BrandID { get; set; }
            public string BrandName { get; set; }
        }

        public class DisplayCode
        {
            public int DisplayCodeID { get; set; }
            public string DisplayCodeName { get; set; }
        }
        public class State
        {
            public int StateID { get; set; }
            public string StateAbbreviation { get; set; }
        }
        public class Region
        {
            public int RegionID { get; set; }
            public string RegionName { get; set; }
            public string RegionCode { get; set; }
        }
        public class Specification
        {
            public int ID { set; get; }
            public string FileName { set; get; }
            public string StateName { set; get; }
            public string RegionName { set; get; }
            public string BrandName { set; get; }
            public string HomeName { set; get; }
            public DateTime EffectiveDate { set; get; }
            public string SystemDocument { set; get; }
            public string AttachmentType { set; get; }
            public string ContractType { set; get; }
            public bool Active { set; get; }
        }
        public class MultiplePromotion
        {
            public int MultiplePromotionID { get; set; }
            public string PromotionName { get; set; }
        }
        public class Storey
        {
            public int id { get; set; }
            public string StoreyName { get; set; }
        }

        public class Status
        {
            public int id { get; set; }
            public string StatusName { get; set; }
        }

        public class PromotionType
        {
            public int PromotionTypeid { get; set; }
            public string PromotionTypeName { get; set; }
        }
        public class Area
        {
            public int AreaID { get; set; }
            public string AreaName { get; set; }
        }
        public class Group
        {
            public int GroupID { get; set; }
            public string GroupName { get; set; }
        }


        public class PromotionProduct
        {
            public int PAGID { get; set; }
            public string ProductID { get; set; }
            public string AreaName { get; set; }
            public string GroupName { get; set; }
            public int PromotionID { get; set; }
            public string PromotionBaseProductID { get; set; }
            public string PromotionName { get; set; }
            public string HomeBrand { get; set; }
            public string Storey { get; set; }
        }
        public class Home
        {
            public int HomeID { get; set; }
            public string ProductID { get; set; }
            public string HomeName { get; set; }
            public string Stories { get; set; }
            public int BrandID { get; set; }
            public string BrandName { get; set; }
            public bool Active { get; set; }
        }

        public class HomeConfigurationItem
        {
            public int HomeID { get; set; }
            public int PagID { get; set; }
            public string ProductID { get; set; }
            public string ProductName { get; set; }
            public string ProductDescription { get; set; }
            public int AreaID { get; set; }
            public string AreaName { get; set; }
            public int GroupID { get; set; }
            public string GroupName { get; set; }
            public bool ChangePrice { get; set; }
            public bool ChangeQuantity { get; set; }
            public bool AddExtraDesc { get; set; }
            public bool IsDisplayHomeItem { get; set; }
            public decimal Quantity { get; set; }
            public decimal UnitPrice { get; set; }
        }

        public class HomeModelAndQuantity
        {
            public string HomeModel { get; set; }
            public decimal Quantity { get; set; }
            public decimal UpdatedQuantity { get; set; }
            public List<HomeAndQuantity> HomeList { get; set; }
            public bool MultipleQuantity { get; set; }
            public string Display1 { get; set; }
            public string Display2 { get; set; }
            public string Display3 { get; set; }
            public string Display4 { get; set; }
            public string Display5 { get; set; }
            public string Display6 { get; set; }
            public string BMPImage { get; set; }
            public int BrandID { get; set; }
            public string BrandName { get; set; }
        }


        public class HomeAndQuantity
        {
            public int HomeID { get; set; }
            public string HomeName { get; set; }
            public decimal Quantity { get; set; }
            public decimal UpdatedQuantity { get; set; }
            // public bool Enabled { get; set; }
        }

        public class retrunValue
        {
            public DataSet HomeModelSet { get; set; }
            public DataSet FacadeSet { get; set; }
        }

        public class FullMultiplePromotion
        {
            public int PromotionID { get; set; }
            public int MultiplePromotionID { get; set; }
            public string PromotionName { get; set; }
            public string PromotionType { get; set; }
            public string BrandName { get; set; }
            public int Storey { get; set; }
            public string BaseProduct { get; set; }
            public double PromotionCost { get; set; }
            public double CapeVale { get; set; }
            public bool ForRegional { get; set; }
            public bool Active { get; set; }
            public string DisplayCodeName { get; set; }
            public int DisplayCodeID { get; set; }
        }

        public class DisplayHome
        {
            public int DisplayHomeID { get; set; }
            public int BrandID { get; set; }
            public string DisplayHomeName { get; set; }
            public string BrandName { get; set; }
            public string Suburb { get; set; }
            public string Address { get; set; }
        }

        public class BasePriceHoldingDays
        {
            public int ID { get; set; }
            public int BrandID { get; set; }
            public string BrandName { get; set; }
            public int RegionID { get; set; }
            public string RegionName { get; set; }
            public string DepositAmount { get; set; }
            public int DaysFrom { get; set; }
            public int DaysTo { get; set; }
            public DateTime EffectiveDate { get; set; }
            public decimal CMAPercent { get; set; }
            public decimal SurchargePercent { get; set; }
            public decimal BTPDoubleStoreyPercent { get; set; }
            public decimal BTPSingleStoreyPercent { get; set; }
            public decimal BTPSingleStoreyDiscount { get; set; }
            public decimal BTPDoubleStoreyDiscount { get; set; }
            public decimal BTPSingleStoreySiteOtherCosts { get; set; }
            public decimal BTPDoubleStoreySiteOtherCosts { get; set; }

            public string State { get; set; }
            public bool Active { get; set; }
        }
        #endregion

        public string getIDsSelected(List<int> ids)
        {
            string return_value = string.Empty;
            List<string> items = new List<string>();

            if (ids != null)
            {
                foreach (int item in ids)
                {
                    items.Add(item.ToString());
                }
            }

            return string.Join(",", items);
        }

        public string getIDsSelected(Dictionary<string, object> selectedItems)
        {
            string return_value = string.Empty;

            List<string> items = new List<string>();
            foreach (KeyValuePair<string, object> item in selectedItems)
            {
                items.Add(item.Value.ToString());
            }

            return string.Join(",", items);
        }

        public string getTextSelectedAsString(Dictionary<string, object> selectedItems)
        {
            string return_value = string.Empty;

            List<string> items = new List<string>();
            foreach (KeyValuePair<string, object> item in selectedItems)
            {
                items.Add(item.Key.ToString());
            }

            return string.Join(",", items);
        }
    }
}
