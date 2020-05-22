using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SQSAdminWCFService
{
    public class SQLDataAccess
    {
        private SqlConnection connection = null;
        private int timeout = 2400000;
        private SqlDataAdapter Adapter;
        private DataSet ds;
        private string connectionstring = ConfigurationManager.ConnectionStrings["SqlConnection"].ToString();

        public DataSet GetStateList()
        {

            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_AdminGetState");
                //SqlCmd.Parameters["@revisionid"].Value = revisonid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public DataSet GetBrandList(int stateid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spw_GetBrandByState");
                SqlCmd.Parameters["@stateid"].Value = stateid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet GetHomeNameBySearch(int stateid, int brandid, string homename)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_AdminFindHome");
                SqlCmd.Parameters["@stateid"].Value = stateid;
                SqlCmd.Parameters["@brandid"].Value = brandid;
                SqlCmd.Parameters["@active"].Value = 1;
                SqlCmd.Parameters["@homename"].Value = homename;
                SqlCmd.Parameters["@showSummary"].Value = 0;
                SqlCmd.Parameters["@viewmode"].Value = "homefacade";
                SqlCmd.Parameters["@showonpricelist"].Value = 2;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet GetSQSConfiguration(string configCode, string codeValue)
        {
            DataSet Sdr;
            try
            {
                SqlCommand SqlCmd = ConstructStoredProcedure("sp_GetSQSConfiguration");
                SqlCmd.Parameters["@configCode"].Value = configCode;
                SqlCmd.Parameters["@codeValue"].Value = codeValue;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public DataSet GetSpecificationByStateRegionsBrands(int stateid, string regionids, string brandids, DateTime effdate, string systemid, string attachmentid, string homestring)
        {
            DataSet Sdr;
            try
            {
                SqlCommand SqlCmd = ConstructStoredProcedure("spw_GetSpecificationByStateRegionsBrands");
                SqlCmd.Parameters["@stateid"].Value = stateid;
                SqlCmd.Parameters["@regionids"].Value = regionids;
                SqlCmd.Parameters["@brandids"].Value = brandids;
                SqlCmd.Parameters["@effdate"].Value = effdate;
                SqlCmd.Parameters["@multipleset"].Value = 1;
                SqlCmd.Parameters["@systemid"].Value = systemid;
                SqlCmd.Parameters["@homestring"].Value = homestring;
                SqlCmd.Parameters["@attachmenttypeid"].Value = attachmentid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public DataSet GetSpecificationByStateRegionBrand(int stateid, int regionid, int brandid, DateTime effdate, string systemid, string attachmentid, string homestring)
        {
            DataSet Sdr;
            try
            {
                SqlCommand SqlCmd = ConstructStoredProcedure("spw_GetSpecificationByStateRegionBrand");
                SqlCmd.Parameters["@stateid"].Value = stateid;
                SqlCmd.Parameters["@regionid"].Value = regionid;
                SqlCmd.Parameters["@brandid"].Value = brandid;
                SqlCmd.Parameters["@effdate"].Value = effdate;
                SqlCmd.Parameters["@multipleset"].Value = 1;
                SqlCmd.Parameters["@systemid"].Value = systemid;
                SqlCmd.Parameters["@homestring"].Value = homestring;
                SqlCmd.Parameters["@attachmenttypeid"].Value = attachmentid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet SaveSpecificationByStateRegionBrand(int id, string filename, int stateid, int regionid, int brandid, int homeid, DateTime effdate, int systemformid, int attachmenttypeid, int active)
        {
            DataSet Sdr;
            try
            {
                SqlCommand SqlCmd = ConstructStoredProcedure("spw_SaveSpecificationByStateRegionBrand");
                SqlCmd.Parameters["@id"].Value = id;
                SqlCmd.Parameters["@filename"].Value = filename;
                SqlCmd.Parameters["@stateid"].Value = stateid;
                SqlCmd.Parameters["@regionid"].Value = regionid;
                SqlCmd.Parameters["@brandid"].Value = brandid;
                SqlCmd.Parameters["@homeid"].Value = homeid;
                SqlCmd.Parameters["@effdate"].Value = effdate;
                SqlCmd.Parameters["@systemformid"].Value = systemformid;
                SqlCmd.Parameters["@attachmenttypeid"].Value = attachmenttypeid;
                SqlCmd.Parameters["@active"].Value = active;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet GetBasePriceHoldingDays(string stateid, string regionids, string brandids, DateTime effectivedate, string active)
        {
            DataSet Sdr;
            try
            {
                SqlCommand SqlCmd = ConstructStoredProcedure("spw_GetDisclaimerBasePriceHoldingDays");
                SqlCmd.Parameters["@brandids"].Value = brandids;
                SqlCmd.Parameters["@regionids"].Value = regionids;
                SqlCmd.Parameters["@effectivedate"].Value = effectivedate;
                SqlCmd.Parameters["@active"].Value = active;
                SqlCmd.Parameters["@stateid"].Value = stateid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public void UpdateBasePriceHoldingDays(string id, string daysfrom, string daysto, DateTime effectivedate, string active, string depositamount, string usercode, decimal cmapercent, decimal surchargepercent, decimal btpsinglestorey, decimal btpdoublestorey, decimal btpSingleStoryDiscount, decimal btpDoubleStoryDiscount, decimal btpSingleStoreyCostSiteOther, decimal btpDoubleStoreyCostSiteOther)
        {
            DataSet Sdr;
            try
            {
                SqlCommand SqlCmd = ConstructStoredProcedure("spw_UpdateDisclaimerBasePriceHoldingDays");
                SqlCmd.Parameters["@id"].Value = id;
                SqlCmd.Parameters["@daysfrom"].Value = daysfrom;
                SqlCmd.Parameters["@daysto"].Value = daysto;
                SqlCmd.Parameters["@effectivedate"].Value = effectivedate;
                SqlCmd.Parameters["@active"].Value = active;
                SqlCmd.Parameters["@depositamount"].Value = depositamount;
                SqlCmd.Parameters["@usercode"].Value = usercode;
                SqlCmd.Parameters["@cmapercent"].Value = cmapercent;
                SqlCmd.Parameters["@surchargepercent"].Value = surchargepercent;
                SqlCmd.Parameters["@btpsinglestorey"].Value = btpsinglestorey;
                SqlCmd.Parameters["@btpdoublestorey"].Value = btpdoublestorey;
                SqlCmd.Parameters["@btpSingleStoreyDiscount"].Value = btpSingleStoryDiscount;
                SqlCmd.Parameters["@btpDoubleStoreyDiscount"].Value = btpDoubleStoryDiscount;
                SqlCmd.Parameters["@btpSingleStoreySiteOtherCosts"].Value = btpSingleStoreyCostSiteOther;
                SqlCmd.Parameters["@btpDoubleStoreySiteOtherCosts"].Value = btpDoubleStoreyCostSiteOther;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {
            }

        }
        public void NewBasePriceHoldingDays(int stateid, string regionids, string brandids, string daysfrom, string daysto, DateTime effectivedate, string active, string depositamount, string usercode, string cmapercent, string surchargepercent, string regionalsurchargeSSpercent, string regionalsurchargeSDpercent, string btpSingleStoryDiscount, string btpDoubleStoryDiscount, string btpSingleStoreyCostSiteOther, string btpDoubleStoreyCostSiteOther)
        {
            DataSet Sdr;
            try
            {
                SqlCommand SqlCmd = ConstructStoredProcedure("spw_NewDisclaimerBasePriceHoldingDays");
                SqlCmd.Parameters["@stateid"].Value = stateid;
                SqlCmd.Parameters["@regionids"].Value = regionids;
                SqlCmd.Parameters["@brandids"].Value = brandids;
                SqlCmd.Parameters["@daysfrom"].Value = daysfrom;
                SqlCmd.Parameters["@daysto"].Value = daysto;
                SqlCmd.Parameters["@effectivedate"].Value = effectivedate;
                SqlCmd.Parameters["@active"].Value = active;
                SqlCmd.Parameters["@depositamount"].Value = depositamount;
                SqlCmd.Parameters["@usercode"].Value = usercode;
                SqlCmd.Parameters["@cmapercent"].Value = cmapercent;
                SqlCmd.Parameters["@surchargepercent"].Value = surchargepercent;
                SqlCmd.Parameters["@regionalsurchargeSSpercent"].Value = regionalsurchargeSSpercent;
                SqlCmd.Parameters["@regionalsurchargeSDpercent"].Value = regionalsurchargeSDpercent;
                SqlCmd.Parameters["@btpSingleStoreyDiscount"].Value = btpSingleStoryDiscount;
                SqlCmd.Parameters["@btpDoubleStoreyDiscount"].Value = btpDoubleStoryDiscount;
                SqlCmd.Parameters["@btpSingleStoreySiteOtherCosts"].Value = btpSingleStoreyCostSiteOther;
                SqlCmd.Parameters["@btpDoubleStoreySiteOtherCosts"].Value = btpDoubleStoreyCostSiteOther;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
            }
            catch (Exception ex)
            {

            }
        }

        public string SearchDisclaimer(string type, string state, int regionid, int brandid, string brandname, DateTime depositDate)
        {
            string return_value = string.Empty;
            DataSet Sdr;
            try
            {
                DataSet ds;
                if (type == "1")
                {
                    SqlCommand Cmd = ConstructStoredProcedure("spw_getDisclaimer");
                    Cmd.Parameters["@state"].Value = state;
                    Cmd.Parameters["@regionid"].Value = regionid;
                    Cmd.Parameters["@brandid"].Value = brandid;
                    Cmd.Parameters["@depositdate"].Value = depositDate;
                    Cmd.Parameters["@replacedaystoken"].Value = 1;

                    Sdr = ExcuteSqlStoredProcedure(Cmd);
                }
                else if (type == "2")
                {
                    SqlCommand Cmd = ConstructStoredProcedure("spw_getOrderFormDisclaimer");
                    Cmd.Parameters["@state"].Value = state;
                    Cmd.Parameters["@regionid"].Value = regionid;
                    Cmd.Parameters["@brandid"].Value = brandid;
                    Cmd.Parameters["@depositdate"].Value = depositDate;
                    Cmd.Parameters["@replacedaystoken"].Value = 1;

                    Sdr = ExcuteSqlStoredProcedure(Cmd);
                }
                else
                {
                    SqlCommand Cmd = ConstructStoredProcedure("sp_SalesEstimate_GetMRSDisclaimerForView");
                    Cmd.Parameters["@state"].Value = state;
                    Cmd.Parameters["@regionid"].Value = regionid;
                    Cmd.Parameters["@brandid"].Value = brandid;
                    Cmd.Parameters["@printtype"].Value = type;
                    Cmd.Parameters["@beforedate"].Value = 0;

                    Sdr = ExcuteSqlStoredProcedure(Cmd);
                }

                if (Sdr.Tables[0].Rows.Count > 0)
                    return_value = Sdr.Tables[0].Rows[0]["agreement"].ToString();
            }
            catch (Exception ex)
            {
                return_value = ex.Message;
            }
            return return_value;
        }

        public string SaveDisclaimer(string type, string state, string regionids, string brandids, string disclaimerText, string internalNotes, string effectiveDate, int status, string usercode)
        {
            string return_value = string.Empty;
            DataSet Sdr;
            try
            {
                SqlCommand Cmd = ConstructStoredProcedure("sp_SalesEstimate_SaveDisclaimer");
                Cmd.Parameters["@type"].Value = type;
                Cmd.Parameters["@state"].Value = state;
                Cmd.Parameters["@regionids"].Value = regionids;
                Cmd.Parameters["@brandids"].Value = brandids;
                Cmd.Parameters["@disclaimer"].Value = disclaimerText;
                Cmd.Parameters["@internalNotes"].Value = internalNotes;
                Cmd.Parameters["@effectivedate"].Value = effectiveDate;
                Cmd.Parameters["@status"].Value = status;
                Cmd.Parameters["@createdby"].Value = usercode;

                Sdr = ExcuteSqlStoredProcedure(Cmd);

                return_value = Sdr.Tables[0].Rows[0]["agreement"].ToString();
            }
            catch (Exception ex)
            {
                return_value = ex.Message;
            }
            return return_value;
        }

        public DataSet SearchDisclaimers(int typeid, int stateid, int regionid, int brandid)
        {
            DataSet Sdr = null;
            try
            {
                SqlCommand Cmd = ConstructStoredProcedure("sp_SalesEstimate_GetDisclaimerHeader");
                Cmd.Parameters["@typeid"].Value = typeid;
                Cmd.Parameters["@stateid"].Value = stateid;
                Cmd.Parameters["@regionid"].Value = regionid;
                Cmd.Parameters["@brandid"].Value = brandid;

                Sdr = ExcuteSqlStoredProcedure(Cmd);
            }
            catch (Exception ex)
            {
                // return_value = ex.Message;
            }
            return Sdr;
        }

        public DataSet GetAvailableAreasByHomeID(int homeid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_Generic_GetAvailabeAreasByHomeID");
                SqlCmd.Parameters["@homeid"].Value = homeid;

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet GetConfiguredAreasByHomeID(int homeid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_Generic_GetConfiguredAreasByHomeID");
                SqlCmd.Parameters["@homeid"].Value = homeid;

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public void AddMinimumAreasToHome(int homeid, string selectedareaid, string usercode)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_Generic_AddMinimumAreasToHome");
                SqlCmd.Parameters["@homeid"].Value = homeid;
                SqlCmd.Parameters["@selectedareaid"].Value = selectedareaid;
                SqlCmd.Parameters["@usercode"].Value = usercode;

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {

            }

        }

        public void RemoveMinimumAreasFromHome(int homeid, string selectedareaid, string usercode)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_Generic_RemoveMinimumAreasFromHome");
                SqlCmd.Parameters["@homeid"].Value = homeid;
                SqlCmd.Parameters["@selectedareaid"].Value = selectedareaid;
                SqlCmd.Parameters["@usercode"].Value = usercode;

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {

            }

        }

        public DataSet GetAvailablePAGByState(string stateid, string areaid, string groupid, string keyword, string pagid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_Generic_GetAvailablePAGByState");
                SqlCmd.Parameters["@stateid"].Value = stateid;
                SqlCmd.Parameters["@areaid"].Value = areaid;
                SqlCmd.Parameters["@groupid"].Value = groupid;
                SqlCmd.Parameters["@keyword"].Value = keyword;
                SqlCmd.Parameters["@pagid"].Value = pagid;

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet GetDisplayHomeByPAGAndState(string stateid, string brandid, string pagid, string displayhomename, string suburb)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_Generic_GetDisplayHomeByPAG");
                SqlCmd.Parameters["@stateid"].Value = stateid;
                SqlCmd.Parameters["@brandid"].Value = brandid;
                SqlCmd.Parameters["@pagid"].Value = pagid;
                SqlCmd.Parameters["@displayhomename"].Value = displayhomename;
                SqlCmd.Parameters["@suburb"].Value = suburb;

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public string ReplaceDisplayHomePAG(string existingpag, string newpag, string displayhomeid, string usercode, string qty, string changeqty, string changeprice, string allowdesc, string desc, string newextradesc)
        {
            DataSet Sdr;
            string result = "";
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_Generic_ReplaceDisplayHomePAG");
                SqlCmd.Parameters["@existingpag"].Value = existingpag;
                SqlCmd.Parameters["@newpag"].Value = newpag;
                SqlCmd.Parameters["@displayhomeid"].Value = displayhomeid;
                SqlCmd.Parameters["@usercode"].Value = usercode;
                SqlCmd.Parameters["@qty"].Value = qty;
                SqlCmd.Parameters["@changeqty"].Value = changeqty;
                SqlCmd.Parameters["@changeprice"].Value = changeprice;
                SqlCmd.Parameters["@allowdesc"].Value = allowdesc;
                SqlCmd.Parameters["@desc"].Value = desc;
                SqlCmd.Parameters["@extradesc"].Value = newextradesc;

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                result = Sdr.Tables[0].Rows[0]["result"].ToString();
            }
            catch (Exception ex)
            {
            }

            return result;

        }


        public DataSet GetSourceHomeByBrand(int brandid, string homename)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_HomeMinimumArea_GetSourceHomeByBrand");
                SqlCmd.Parameters["@brandid"].Value = brandid;
                SqlCmd.Parameters["@homename"].Value = homename;

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet GetTargetHomeByBrand(int brandid, string homename)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_HomeMinimumArea_GetTargetHomeByBrand");
                SqlCmd.Parameters["@brandid"].Value = brandid;
                SqlCmd.Parameters["@homename"].Value = homename;


                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public void CopyMinimumAreasFromSourceHomeToTargetHome(string sourcehomeid, string targethomeidstring, string usercode)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_HomeMinimumArea_CopyMinimumAreasFromSourceHomeToTargetHomes");
                SqlCmd.Parameters["@sourcehomeid"].Value = sourcehomeid;
                SqlCmd.Parameters["@targethomeidstring"].Value = targethomeidstring;
                SqlCmd.Parameters["@usercode"].Value = usercode;

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {

            }

        }
        public DataSet GetAvailableGroupList(int retailclusterid, string groupname)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_GetAvailableGroupForRetailCluster");
                SqlCmd.Parameters["@retailclusterid"].Value = retailclusterid;
                SqlCmd.Parameters["@groupname"].Value = groupname;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public DataSet LoadSQSGroupList(string groupname)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_GetSQSGroups");
                SqlCmd.Parameters["@groupname"].Value = groupname;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        public DataSet GetExistingGroupList(int retailclusterid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_GetExistingGroupForRetailCluster");
                SqlCmd.Parameters["@retailclusterid"].Value = retailclusterid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet GetRetailCluster(int stateid, string clustername)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_GetRetailCluster");
                SqlCmd.Parameters["@stateid"].Value = stateid;
                SqlCmd.Parameters["@clustername"].Value = clustername;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public void SaveSelectedGroupToRetailCluster(int retailclusterid, string selectedgroupid, string usercode)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_SaveSelectedGroupToRetailCluster");
                SqlCmd.Parameters["@retailclusterid"].Value = retailclusterid;
                SqlCmd.Parameters["@selectedgroupid"].Value = selectedgroupid;
                SqlCmd.Parameters["@usercode"].Value = usercode;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {

            }

        }

        public void RemoveSelectedGroupFromRetailCluster(int retailclusterid, string selectedgroupid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_RemoveSelectedGroupFromRetailCluster");
                SqlCmd.Parameters["@retailclusterid"].Value = retailclusterid;
                SqlCmd.Parameters["@selectedgroupid"].Value = selectedgroupid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {

            }

        }

        public DataSet GetRegionList(int pstateid)
        {

            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_GetRegionForPriceImport");
                SqlCmd.Parameters["@stateID"].Value = pstateid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet GetPromotionList(int stateid, int brandid, int storey, int status)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_Promotion_GetPromotion");
                SqlCmd.Parameters["@stateID"].Value = stateid;
                SqlCmd.Parameters["@brandid"].Value = brandid;
                SqlCmd.Parameters["@storey"].Value = storey;
                SqlCmd.Parameters["@active"].Value = status;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public bool CheckMasterPromotionProductExist(int stateid, string productid)
        {
            bool result = true;
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_Promotion_CheckProductExists");
                SqlCmd.Parameters["@stateID"].Value = stateid;
                SqlCmd.Parameters["@productid"].Value = productid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

                if (Sdr.Tables[0].Rows[0]["result"].ToString() == "0")
                {
                    result = false;
                }

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool CheckMultiplePromotionExist(int brandid, int stories, string productid)
        {
            bool result = false;
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_Promotion_CheckMultiplePromotionExists");
                SqlCmd.Parameters["@brandid"].Value = brandid;
                SqlCmd.Parameters["@stories"].Value = stories;
                SqlCmd.Parameters["@baseproductid"].Value = productid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

                if (Sdr.Tables[0].Rows[0]["result"].ToString().ToUpper() == "TRUE")
                {
                    result = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public DataSet GetArea()
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_AdminGetAllAreas");
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet GetPromotionProductArea(int multiplepromotionid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_Promotion_GetPromotionProductArea");
                SqlCmd.Parameters["@multiplepromotionid"].Value = multiplepromotionid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet GetPromotionProductGroup(int multiplepromotionid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_Promotion_GetPromotionProductGroup");
                SqlCmd.Parameters["@multiplepromotionid"].Value = multiplepromotionid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet GetGroup()
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_AdminGetAllGroups");
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet GetGroupFromArea(int areaid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_AdminGetGroupFromArea");
                SqlCmd.Parameters["@areaid"].Value = areaid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        public void SavePromotion(
            int stateid,
            int brandid,
            int storey,
            int promotiontypeid,
            string productid,
            string promotionname,
            decimal promotioncost,
            decimal capevalue,
            int forregional,
            int active,
            int displaycodeid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_Promotion_SavePromotion");
                SqlCmd.Parameters["@stateID"].Value = stateid;
                SqlCmd.Parameters["@brandid"].Value = brandid;
                SqlCmd.Parameters["@storey"].Value = storey;
                SqlCmd.Parameters["@promotiontypeid"].Value = promotiontypeid;
                SqlCmd.Parameters["@productid"].Value = productid;
                SqlCmd.Parameters["@promotionname"].Value = promotionname;
                SqlCmd.Parameters["@promotioncost"].Value = promotioncost;
                SqlCmd.Parameters["@capevalue"].Value = capevalue;
                SqlCmd.Parameters["@forregional"].Value = forregional;
                SqlCmd.Parameters["@active"].Value = active;
                SqlCmd.Parameters["@displaycodeid"].Value = displaycodeid;

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {

            }

        }

        public DataSet GetPromotionTypeList()
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_Promotion_GetPromotionType");
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        //public DataSet GetProductPriceList(int pstateid,
        //                                   int pregionid,
        //                                   int pstatus,
        //                                   string pproductid,
        //                                   string pproductname,
        //                                   string pproductdesc
        //                                  )
        //{

        //    DataSet Sdr;
        //    try
        //    {

        //        SqlCommand SqlCmd = ConstructStoredProcedure("AdminSearchProduct");
        //        SqlCmd.Parameters["@ProductID"].Value = pproductid;
        //        SqlCmd.Parameters["@ProductName"].Value = pproductname;
        //        SqlCmd.Parameters["@ProductDescription"].Value = pproductdesc;
        //        SqlCmd.Parameters["@regionID"].Value = pregionid;
        //        SqlCmd.Parameters["@active"].Value = pstatus;
        //        SqlCmd.Parameters["@minibillstart"].Value = 0;
        //        SqlCmd.Parameters["@minibillcomplete"].Value = 0;
        //        SqlCmd.Parameters["@stateID"].Value = pstateid;
        //        Sdr = ExcuteSqlStoredProcedure(SqlCmd);
        //        return Sdr;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }

        //}

        public DataSet GetMultiplePromotionDetails(int multiplepromotionid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_Promotion_GetMultimplePromotionDetails");
                SqlCmd.Parameters["@multiplepromotionid"].Value = multiplepromotionid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public DataSet GetAvailableProductsForPromotion(int multiplepromotionid, int areaid, int groupid, string proudctstring, string pagid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_Generic_GetAllAvailableProductsForPromotion");
                SqlCmd.Parameters["@multiplepromotionid"].Value = multiplepromotionid;
                SqlCmd.Parameters["@areaid"].Value = areaid;
                SqlCmd.Parameters["@groupid"].Value = groupid;
                SqlCmd.Parameters["@proudctstring"].Value = proudctstring;
                SqlCmd.Parameters["@pagid"].Value = pagid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet GetExistingProductsForPromotion(int multiplepromotionid, int areaid, int groupid, string proudctstring, string pagid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_Promotion_GetExistingProductsForPromotion");
                SqlCmd.Parameters["@multiplepromotionid"].Value = multiplepromotionid;
                SqlCmd.Parameters["@areaid"].Value = areaid;
                SqlCmd.Parameters["@groupid"].Value = groupid;
                SqlCmd.Parameters["@proudctstring"].Value = proudctstring;
                SqlCmd.Parameters["@pagid"].Value = pagid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public void AddProductsToPromotion(int multiplepromotionid, string selelctedpagid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_Promotion_AddProductsToPromotion");
                SqlCmd.Parameters["@multiplepromotionid"].Value = multiplepromotionid;
                SqlCmd.Parameters["@selelctedpagid"].Value = selelctedpagid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
            }
            catch (Exception ex)
            {
            }

        }

        public void UpdatePromotionProduct(int multiplepromotionid, int pagid, int defaultselected, decimal discount)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_Promotion_UpdatePromotionProduct");
                SqlCmd.Parameters["@multiplepromotionid"].Value = multiplepromotionid;
                SqlCmd.Parameters["@pagid"].Value = pagid;
                SqlCmd.Parameters["@defaultselected"].Value = defaultselected;
                SqlCmd.Parameters["@discount"].Value = discount;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
            }
            catch (Exception ex)
            {
            }

        }

        public void UpdatePromotion(int multiplepromotionid, string baseproduct, decimal promotioncost, decimal capevalue, int forregional, int active, int displaycodeid, string promotionname)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_Promotion_UpdatePromotion");
                SqlCmd.Parameters["@multiplepromotionid"].Value = multiplepromotionid;
                SqlCmd.Parameters["@baseproduct"].Value = baseproduct;
                SqlCmd.Parameters["@promotioncost"].Value = promotioncost;
                SqlCmd.Parameters["@capevalue"].Value = capevalue;
                SqlCmd.Parameters["@forregional"].Value = forregional;
                SqlCmd.Parameters["@active"].Value = active;
                SqlCmd.Parameters["@displaycodeid"].Value = displaycodeid;
                SqlCmd.Parameters["@promotionname"].Value = promotionname;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
            }
            catch (Exception ex)
            {
            }

        }

        public void RemoveProductsFromPromotion(int multiplepromotionid, string selelctedpagid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_Promotion_RemoveProductsFromPromotion");
                SqlCmd.Parameters["@multiplepromotionid"].Value = multiplepromotionid;
                SqlCmd.Parameters["@selelctedpagid"].Value = selelctedpagid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
            }
            catch (Exception ex)
            {
            }

        }


        public DataSet GetSupplier(int stateid, string suppliername, int active)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_GetSuppliers");
                SqlCmd.Parameters["@stateid"].Value = stateid;
                SqlCmd.Parameters["@suppliername"].Value = suppliername;
                SqlCmd.Parameters["@active"].Value = active;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet GetActiveQuestion(int stateid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_GetQuestion");
                SqlCmd.Parameters["@question"].Value = "";
                SqlCmd.Parameters["@active"].Value = 1;
                SqlCmd.Parameters["@stateid"].Value = stateid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet SearchActiveQuestion(int stateid, string searchtext)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_GetQuestion");
                SqlCmd.Parameters["@question"].Value = searchtext;
                SqlCmd.Parameters["@active"].Value = 1;
                SqlCmd.Parameters["@stateid"].Value = stateid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet GetAnswerForQuestion(int questionid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_GetAnswerForQuestion");
                SqlCmd.Parameters["@questionid"].Value = questionid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public DataSet LoadStudioMAttributeForClusterOrGroup(int id, string type, int stateid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_LoadStudioMAttributeForClusterOrGroup");
                SqlCmd.Parameters["@id"].Value = id;
                SqlCmd.Parameters["@type"].Value = type;
                SqlCmd.Parameters["@stateid"].Value = stateid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public DataSet UpdateStudioMAttribute(string productid, string attribute)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_UpdateStudioMAttribute");
                SqlCmd.Parameters["@productid"].Value = productid;
                SqlCmd.Parameters["@attribute"].Value = attribute;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet UpdateStudioMAttributeForClusterOrGroup(string id, string attribute, string type, string usercode, string supplieridstring, string questionidstring, string answeridstring, string mandatorystring, string stateid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_UpdateStudioMAttributeForClusterOrGroup");
                SqlCmd.Parameters["@id"].Value = id;
                SqlCmd.Parameters["@attribute"].Value = attribute;
                SqlCmd.Parameters["@type"].Value = type;
                SqlCmd.Parameters["@usercode"].Value = usercode;
                SqlCmd.Parameters["@supplieridstring"].Value = supplieridstring;
                SqlCmd.Parameters["@questionidstring"].Value = questionidstring;
                SqlCmd.Parameters["@answeridstring"].Value = answeridstring;
                SqlCmd.Parameters["@mandatorystring"].Value = mandatorystring;
                SqlCmd.Parameters["@stateid"].Value = stateid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public string GetProductStudioMAttribute(string productid)
        {
            DataSet Sdr;
            string result = "";
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_GetStudioMAttribute");
                SqlCmd.Parameters["@productid"].Value = productid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

                if (Sdr.Tables[0].Rows.Count > 0)
                {
                    result = Sdr.Tables[0].Rows[0]["studiomqanda"].ToString();
                }
                return result;

            }
            catch (Exception ex)
            {
                return result;
            }

        }

        public bool CheckSupplierExists(int stateid, string suppliername)
        {
            DataSet Sdr;
            bool result = false;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_CheckSupplierExists");
                SqlCmd.Parameters["@stateid"].Value = stateid;
                SqlCmd.Parameters["@suppliername"].Value = suppliername;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

                if (Sdr.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }
                return result;

            }
            catch (Exception ex)
            {
                return result;
            }

        }

        public bool CheckQuestionExists(int answertypeid, string questiontext, int stateid)
        {
            DataSet Sdr;
            bool result = false;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_CheckQuestionExists");
                SqlCmd.Parameters["@answertypeid"].Value = answertypeid;
                SqlCmd.Parameters["@questiontext"].Value = questiontext;
                SqlCmd.Parameters["@stateid"].Value = stateid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

                if (Sdr.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }
                return result;

            }
            catch (Exception ex)
            {
                return result;
            }

        }

        public bool CheckRetailClusterExists(int stateid, string clustername)
        {
            DataSet Sdr;
            bool result = false;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_CheckRetailClusterExists");
                SqlCmd.Parameters["@stateid"].Value = stateid;
                SqlCmd.Parameters["@clustername"].Value = clustername;

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

                if (Sdr.Tables[0].Rows[0]["result"].ToString() == "1")
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                return result;

            }
            catch (Exception ex)
            {
                return result;
            }

        }

        public void AddEditRetailCluster(int retailclusterid, string clustername, int stateid, int active, string loginuser)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_AddEditRetailCluster");
                SqlCmd.Parameters["@retailclusterid"].Value = retailclusterid;
                SqlCmd.Parameters["@clustername"].Value = clustername;
                SqlCmd.Parameters["@stateid"].Value = stateid;
                SqlCmd.Parameters["@active"].Value = active;
                SqlCmd.Parameters["@usercode"].Value = loginuser;

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {
            }

        }
        public bool CheckAnswerExists(int questionid, string answertext)
        {
            DataSet Sdr;
            bool result = false;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_CheckAnswerExists");
                SqlCmd.Parameters["@questionid"].Value = questionid;
                SqlCmd.Parameters["@answertext"].Value = answertext;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

                if (Sdr.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }
                return result;

            }
            catch (Exception ex)
            {
                return result;
            }

        }

        public void SaveSupplier(int supplierid, int stateid, string suppliername, int active)
        {
            DataSet Sdr;

            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_AddEditSupplier");
                SqlCmd.Parameters["@supplierid"].Value = supplierid;
                SqlCmd.Parameters["@stateid"].Value = stateid;
                SqlCmd.Parameters["@suppliername"].Value = suppliername;
                SqlCmd.Parameters["@active"].Value = active;

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {

            }

        }

        public void SaveQuestion(int questionid, int answertypeid, string questiontext, int active, int mandatory, int stateid)
        {
            DataSet Sdr;

            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_AddEditQuestion");
                SqlCmd.Parameters["@questionid"].Value = questionid;
                SqlCmd.Parameters["@answertypeid"].Value = answertypeid;
                SqlCmd.Parameters["@questiontext"].Value = questiontext;
                SqlCmd.Parameters["@active"].Value = active;
                SqlCmd.Parameters["@mandatory"].Value = mandatory;
                SqlCmd.Parameters["@stateid"].Value = stateid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {

            }

        }

        public void SaveAnswer(int questionid, int answerid, string answertext, int active)
        {
            DataSet Sdr;

            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_AddEditAnswer");
                SqlCmd.Parameters["@questionid"].Value = questionid;
                SqlCmd.Parameters["@answerid"].Value = answerid;
                SqlCmd.Parameters["@answertext"].Value = answertext;
                SqlCmd.Parameters["@active"].Value = active;

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {

            }

        }
        public DataSet GetAnserType()
        {
            DataSet Sdr;

            try
            {
                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_GetAnswerType");
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
            }
            catch (Exception ex)
            {
                return null;
            }
            return Sdr;
        }

        public DataSet GetQuestionList(string question, int active, int stateid)
        {
            DataSet Sdr;

            try
            {
                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_GetQuestion");
                SqlCmd.Parameters["@question"].Value = question;
                SqlCmd.Parameters["@active"].Value = active;
                SqlCmd.Parameters["@stateid"].Value = stateid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
            }
            catch (Exception ex)
            {
                return null;
            }
            return Sdr;
        }

        public DataSet GetAnswerList(string questiontext, string answertext, int active, int stateid)
        {
            DataSet Sdr;

            try
            {
                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_GetAnswer");
                SqlCmd.Parameters["@questiontext"].Value = questiontext;
                SqlCmd.Parameters["@answertext"].Value = answertext;
                SqlCmd.Parameters["@active"].Value = active;
                SqlCmd.Parameters["@stateid"].Value = stateid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
            }
            catch (Exception ex)
            {
                return null;
            }
            return Sdr;
        }

        public DataSet GetProductPriceList(int pstateid,
                                           int pregionid,
                                           int pstatus,
                                           string pproductid,
                                           string pproductname,
                                           string pproductdesc,
                                           int studioMproduct
                                          )
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("AdminSearchProduct");
                SqlCmd.Parameters["@ProductID"].Value = pproductid;
                SqlCmd.Parameters["@ProductName"].Value = pproductname;
                SqlCmd.Parameters["@ProductDescription"].Value = pproductdesc;
                SqlCmd.Parameters["@regionID"].Value = pregionid;
                SqlCmd.Parameters["@active"].Value = pstatus;
                SqlCmd.Parameters["@minibillstart"].Value = 0;
                SqlCmd.Parameters["@minibillcomplete"].Value = 0;
                SqlCmd.Parameters["@stateID"].Value = pstateid;
                SqlCmd.Parameters["@studioMproduct"].Value = studioMproduct;
                SqlCmd.Parameters["@standardinclusion"].Value = 0;
                SqlCmd.Parameters["@additionalnote"].Value = "";
                SqlCmd.Parameters["@operationsonly"].Value = "0";
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet GetProductImages(string pproductid, int supplierid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_GetImageForProduct");
                SqlCmd.Parameters["@ProductID"].Value = pproductid;
                SqlCmd.Parameters["@supplierid"].Value = supplierid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet DeleteProductImage(int imageid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_RemoveImage");
                SqlCmd.Parameters["@imageid"].Value = imageid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet GetProductByCategoryCount(int stateid)
        {

            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_GetProductByCategoryCount");
                SqlCmd.Parameters["@stateid"].Value = stateid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public DataSet SaveProductImage(string productid, string imagename, int supplierid, byte[] imagestream)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_SaveProductImage");
                SqlCmd.Parameters["@productid"].Value = productid;
                SqlCmd.Parameters["@imagename"].Value = imagename;
                SqlCmd.Parameters["@supplierid"].Value = supplierid;
                SqlCmd.Parameters["@image"].Value = imagestream;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet GetStandardInclusion(string productid, string productname, int stateid, string brandids)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StandardInclusion_GetStandardInclusionProducts");
                SqlCmd.Parameters["@stateid"].Value = stateid;
                SqlCmd.Parameters["@productid"].Value = productid;
                SqlCmd.Parameters["@productname"].Value = productname;
                SqlCmd.Parameters["@brandids"].Value = brandids;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet GetUpgradeOptionByStandardInlcusion(string productid, string brandids)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StandardInclusion_GetUpgradeOptionForStandardInclusion");
                SqlCmd.Parameters["@productid"].Value = productid;
                SqlCmd.Parameters["@brandids"].Value = brandids;

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet GetAvailableUpgradeOption(string productid, string productname, string brandids, int stateid, string stdinclusionproductid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StandardInclusion_GetAvailableUpgradeOptionProducts");
                SqlCmd.Parameters["@productid"].Value = productid;
                SqlCmd.Parameters["@productname"].Value = productname;
                SqlCmd.Parameters["@brandids"].Value = brandids;
                SqlCmd.Parameters["@stateid"].Value = stateid;
                SqlCmd.Parameters["@stdinclusionproductid"].Value = stdinclusionproductid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public void RemoveValidationRule(int validationruleid, bool update, bool active)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StandardInclusion_RemoveValidationRule");
                SqlCmd.Parameters["@validationruleid"].Value = validationruleid;
                SqlCmd.Parameters["@update"].Value = update;
                SqlCmd.Parameters["@active"].Value = active;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {

            }

        }

        public void SaveValidationRule(string standardinclusionproductid, string upgradeoptionproductids, string brandids, string usercode, bool promotion, DateTime effectivedate)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StandardInclusion_SaveValidationRule");
                SqlCmd.Parameters["@standardinclusionproductid"].Value = standardinclusionproductid;
                SqlCmd.Parameters["@upgradeoptionproductids"].Value = upgradeoptionproductids;
                SqlCmd.Parameters["@brandids"].Value = brandids;
                SqlCmd.Parameters["@usercode"].Value = usercode;
                SqlCmd.Parameters["@promotion"].Value = promotion;
                SqlCmd.Parameters["@effectivedate"].Value = @effectivedate;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {

            }

        }

        public DataSet GetPAGForProduct(string productid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StandardInclusion_GetPAGForProduct");
                SqlCmd.Parameters["@productid"].Value = productid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet GetRegiongroupNameByState(int pstateid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StandardInclusion_GetRegionGroupByState");
                SqlCmd.Parameters["@stateId"].Value = pstateid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet GetStandardInclusion(int pbrandid, int pregiongroupid, int pagid, int pstateid, string productid, string productname)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StandardInclusion_GetStandardInclusionDetails");
                SqlCmd.Parameters["@brandId"].Value = pbrandid;
                SqlCmd.Parameters["@regiongroupid"].Value = pregiongroupid;
                SqlCmd.Parameters["@pagid"].Value = pagid;
                SqlCmd.Parameters["@stateid"].Value = pstateid;
                SqlCmd.Parameters["@productid"].Value = productid;
                SqlCmd.Parameters["@productname"].Value = productname;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet GetSupplierBrand(string brandname, int pstateid, int active)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_GetSupplierBrands");
                SqlCmd.Parameters["@stateid"].Value = pstateid;
                SqlCmd.Parameters["@brandname"].Value = brandname;
                SqlCmd.Parameters["@active"].Value = active;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public void AddEditSupplierBrand(int supplierbrandid, string brandname, int pstateid, int active, string usercode)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_AddEditSupplierBrand");
                SqlCmd.Parameters["@supplierbrandid"].Value = supplierbrandid;
                SqlCmd.Parameters["@brandname"].Value = brandname;
                SqlCmd.Parameters["@active"].Value = active;
                SqlCmd.Parameters["@stateid"].Value = pstateid;
                SqlCmd.Parameters["@userid"].Value = usercode;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {

            }
        }

        public bool CheckSupplierBrandExists(string brandname, int pstateid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_CheckSupplierBrandExists");
                SqlCmd.Parameters["@brandname"].Value = brandname;
                SqlCmd.Parameters["@stateid"].Value = pstateid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                if (Sdr.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool CheckStandardInclusionExists(int pagid, int pbrandid, int pregiongroupid, int standardinclusionid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StandardInclusion_CheckStandardInclusionExists");
                SqlCmd.Parameters["@pagid"].Value = pagid;
                SqlCmd.Parameters["@brandid"].Value = pbrandid;
                SqlCmd.Parameters["@regiongroupid"].Value = pregiongroupid;
                SqlCmd.Parameters["@standardinclusionid"].Value = standardinclusionid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                if (Sdr.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void AddEditStandardInclusion(int standardinclusionid, int pagid, int pbrandid, int pregiongroupid, decimal quantity, int active, string usercode)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StandardInclusion_AddEditStandardInclusion");
                SqlCmd.Parameters["@standardinclusionid"].Value = standardinclusionid;
                SqlCmd.Parameters["@pagid"].Value = pagid;
                SqlCmd.Parameters["@brandid"].Value = pbrandid;
                SqlCmd.Parameters["@regiongroupid"].Value = pregiongroupid;
                SqlCmd.Parameters["@quantity"].Value = quantity;
                SqlCmd.Parameters["@active"].Value = active;
                SqlCmd.Parameters["@usercode"].Value = usercode;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {

            }
        }

        public void RemoveStandardInclusion(int standardinclusionid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StandardInclusion_RemoveStandardInclusion");
                SqlCmd.Parameters["@standardinclusionid"].Value = standardinclusionid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {

            }
        }

        public DataSet GetAvailableStandardInclusionPAGFromMasterList(int areaid, int groupid, string productid, string productname, int stateid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StandardInclusion_GetAvailableStandardInclusion_FromProductMasterList");
                SqlCmd.Parameters["@stateid"].Value = stateid;
                SqlCmd.Parameters["@areaid"].Value = areaid;
                SqlCmd.Parameters["@groupid"].Value = groupid;
                SqlCmd.Parameters["@productid"].Value = productid;
                SqlCmd.Parameters["@productname"].Value = productname;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet GetAvailableStandardInclusionPAG(int areaid, int groupid, string productid, string productname, int pbrandid, int pregiongroupid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StandardInclusion_GetAvailableStandardInclusion");
                SqlCmd.Parameters["@brandid"].Value = pbrandid;
                SqlCmd.Parameters["@regiongroupid"].Value = pregiongroupid;
                SqlCmd.Parameters["@areaid"].Value = areaid;
                SqlCmd.Parameters["@groupid"].Value = groupid;
                SqlCmd.Parameters["@productid"].Value = productid;
                SqlCmd.Parameters["@productname"].Value = productname;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void SaveStandardInclusionToBrand(string selectedids, string brandids, int regiongroupid, string usercode, DateTime effectivedate)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StandardInclusion_SaveStandardInclusionToBrand");
                SqlCmd.Parameters["@brandids"].Value = brandids;
                SqlCmd.Parameters["@regiongroupid"].Value = regiongroupid;
                SqlCmd.Parameters["@selectedids"].Value = selectedids;
                SqlCmd.Parameters["@usercode"].Value = usercode;
                SqlCmd.Parameters["@effectivedate"].Value = effectivedate;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {

            }
        }

        public DataSet GetPromotionForProduct(int pagid, string productid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_Promotion_GetPromotionForProduct");
                SqlCmd.Parameters["@pagid"].Value = pagid;
                SqlCmd.Parameters["@productid"].Value = productid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet GetSourcePromotionForCopy(int targetpromotionid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_Promotion_GetSourcePromotionForCopy");
                SqlCmd.Parameters["@targetpromotionid"].Value = targetpromotionid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool CopyPromotionProductFromSourceToTarget(int targetpromotionid, int sourcepromotionid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_Promotion_CopyPromotionProductFromSourceToTarget");
                SqlCmd.Parameters["@targetpromotionid"].Value = targetpromotionid;
                SqlCmd.Parameters["@sourcepromotionid"].Value = sourcepromotionid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataSet LoadProduct(int stateid, string productid, string keyword)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_RelatedArea_LoadProducts");
                SqlCmd.Parameters["@productid"].Value = productid;
                SqlCmd.Parameters["@keyword"].Value = keyword;
                SqlCmd.Parameters["@stateid"].Value = stateid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
            }
            catch (Exception ex)
            {
                return null;
            }
            return Sdr;
        }

        public DataSet LoadExistingAreasForProduct(string productid, int active)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_RelatedArea_GetRelatedAreaForProduct");
                SqlCmd.Parameters["@productid"].Value = productid;
                SqlCmd.Parameters["@active"].Value = active;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
            }
            catch (Exception ex)
            {
                return null;
            }
            return Sdr;
        }

        public DataSet LoadAvailableAreasForProduct(string productid, string keyword, string callfrom)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_RelatedArea_GetAvailableAreasForProduct");
                SqlCmd.Parameters["@productid"].Value = productid;
                SqlCmd.Parameters["@keyword"].Value = keyword;
                SqlCmd.Parameters["@callfrom"].Value = callfrom;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
            }
            catch (Exception ex)
            {
                return null;
            }
            return Sdr;
        }

        public void AddSelectedAreasToProduct(string productid, string areaidstring, string usercode, string callfrom)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_RelatedArea_AddRelatedAreasToProducts");
                SqlCmd.Parameters["@productidstr"].Value = productid;
                SqlCmd.Parameters["@areaidstr"].Value = areaidstring;
                SqlCmd.Parameters["@usercode"].Value = usercode;
                SqlCmd.Parameters["@callfrom"].Value = callfrom;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
            }
            catch (Exception ex)
            {

            }
        }

        public void RemoveSelectedAreasFromProduct(string productid, string areaidstring, string callfrom)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_RelatedArea_RemoveRelatedAreasfromProduct");
                SqlCmd.Parameters["@productid"].Value = productid;
                SqlCmd.Parameters["@idstring"].Value = areaidstring;
                SqlCmd.Parameters["@callfrom"].Value = callfrom;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
            }
            catch (Exception ex)
            {

            }
        }

        public DataSet GetStudioMProduct(int stateid, string productid, string productname)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_GetStudioMProduct");
                SqlCmd.Parameters["@stateid"].Value = stateid;
                SqlCmd.Parameters["@productid"].Value = productid;
                SqlCmd.Parameters["@productname"].Value = productname;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool BulkConfigureStudiomQandA(string supplierbrandid, string productidstring, string questionidstring, string answeridstring, string usercode)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_StudioM_BulkConfigureStudioMQAndA");
                SqlCmd.CommandTimeout = 240000;
                SqlCmd.Parameters["@idsupplierbrand"].Value = supplierbrandid;
                SqlCmd.Parameters["@productidstring"].Value = productidstring;
                SqlCmd.Parameters["@questionidstring"].Value = questionidstring;
                SqlCmd.Parameters["@answeridstring"].Value = answeridstring;
                SqlCmd.Parameters["@usercode"].Value = usercode;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

                return true;
            }
            catch (Exception ex)
            {
                //return false;
                throw ex;
            }

        }


        public DataSet SQSAdmin_Home_GetHomeList(int stateid, int brandid, string homename, int active)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_AdminFindHome");
                SqlCmd.Parameters["@stateID"].Value = stateid;
                SqlCmd.Parameters["@brandID"].Value = brandid;
                SqlCmd.Parameters["@active"].Value = active;
                SqlCmd.Parameters["@homename"].Value = homename;
                SqlCmd.Parameters["@showSummary"].Value = 0;
                SqlCmd.Parameters["@viewmode"].Value = "homefacade";
                SqlCmd.Parameters["@showonpricelist"].Value = 2;

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {
                return null;
            }
            return Sdr;
        }

        public DataSet SQSAdmin_Home_GetHomePlanByStateAndBrands(int stateid, string brandids)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("GetHomePlanByStateAndBrands");
                SqlCmd.Parameters["@stateID"].Value = stateid;
                SqlCmd.Parameters["@brandIDs"].Value = brandids;

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {
                return null;
            }
            return Sdr;
        }

        public DataSet SQSAdmin_Generic_GetAreas(int stateid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_AdminGetAllAreas");
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {
                return null;
            }
            return Sdr;
        }

        public DataSet SQSAdmin_Generic_GetGroups(int stateid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_AdminGetAllGroups");
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {
                return null;
            }
            return Sdr;
        }

        public DataSet SQSAdmin_HomeConfiguration_GetAvailablePAGForHome(int homeid, int areaid, int groupid, string keyword)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_GetAvailablePagForHome");
                SqlCmd.Parameters["@homeID"].Value = homeid;
                SqlCmd.Parameters["@areaID"].Value = areaid;
                SqlCmd.Parameters["@groupID"].Value = groupid;
                SqlCmd.Parameters["@keyword"].Value = keyword;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {
                return null;
            }
            return Sdr;
        }

        public DataSet SQSAdmin_HomeConfiguration_GetExistingPAGForHome(int homeid, int areaid, int groupid, string keyword)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_GetExistingPagForHome");
                SqlCmd.Parameters["@homeID"].Value = homeid;
                SqlCmd.Parameters["@areaID"].Value = areaid;
                SqlCmd.Parameters["@groupID"].Value = groupid;
                SqlCmd.Parameters["@keyword"].Value = keyword;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {
                return null;
            }
            return Sdr;
        }

        public bool SQSAdmin_HomeConfiguration_AddProductToHome(int homeid, string selectedpagid, string qtystring, string changeqtystring, string changepricestring, string extradescstring, string usercode)
        {
            bool result = true;
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_ConfigureProductsToHome");
                SqlCmd.Parameters["@homeID"].Value = homeid;
                SqlCmd.Parameters["@pagidstring"].Value = selectedpagid;
                SqlCmd.Parameters["@qtystring"].Value = qtystring;
                SqlCmd.Parameters["@changeqtystring"].Value = changeqtystring;
                SqlCmd.Parameters["@changepricestring"].Value = changepricestring;
                SqlCmd.Parameters["@extradescstring"].Value = extradescstring;
                SqlCmd.Parameters["@usercode"].Value = usercode;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {
                return false;
            }
            return result;
        }

        public bool SQSAdmin_HomeConfiguration_UpdatePagForHome(int homeid, int pagid, string quantity, int changeqty, int changeprice, int extradesc, string usercode)
        {
            bool result = true;
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_UpdatePagForHome");
                SqlCmd.Parameters["@homeID"].Value = homeid;
                SqlCmd.Parameters["@pagid"].Value = pagid;
                SqlCmd.Parameters["@quantity"].Value = quantity;
                SqlCmd.Parameters["@changeqty"].Value = changeqty;
                SqlCmd.Parameters["@changeprice"].Value = changeprice;
                SqlCmd.Parameters["@extradesc"].Value = extradesc;
                SqlCmd.Parameters["@usercode"].Value = usercode;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {
                return false;
            }
            return result;
        }
        public bool SQSAdmin_HomeConfiguration_RemoveProductForHome(int homeid, string selectedpagids, string usercode)
        {
            bool result = true;
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_RemovePagFromHome");
                SqlCmd.Parameters["@homeID"].Value = homeid;
                SqlCmd.Parameters["@pagidstring"].Value = selectedpagids;
                SqlCmd.Parameters["@usercode"].Value = usercode;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {
                return false;
            }
            return result;
        }

        public DataSet SQSAdmin_HomeConfiguration_GetProducts(int stateid, string productid, string keyword)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_GetProducts");
                SqlCmd.Parameters["@stateID"].Value = stateid;
                SqlCmd.Parameters["@productID"].Value = productid;
                SqlCmd.Parameters["@keyword"].Value = keyword;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

            }
            catch (Exception ex)
            {
                return null;
            }
            return Sdr;
        }

        public string SQSAdmin_HomeConfiguration_CopyProductConfiguration(string sourceproductid, string targetproductid, string usercode)
        {
            DataSet Sdr;
            string result = "";
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_CopyProductConfiguration");
                SqlCmd.Parameters["@sourceproductID"].Value = sourceproductid;
                SqlCmd.Parameters["@targetproductID"].Value = targetproductid;
                SqlCmd.Parameters["@usercode"].Value = usercode;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

                result = Sdr.Tables[0].Rows[0]["result"].ToString();

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return result;
        }

        public DataSet SQSAdmin_QuantityManagement_GetHomeModelQuantity(string stateid, string areaid, string groupid, string brandid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_GetQuantityByHomeModel");
                SqlCmd.Parameters["@stateID"].Value = stateid;
                SqlCmd.Parameters["@areaID"].Value = areaid;
                SqlCmd.Parameters["@groupID"].Value = groupid;
                SqlCmd.Parameters["@brandid"].Value = brandid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

                return Sdr;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet SQSAdmin_QuantityManagement_GetHomeFacadeQuantity(string stateid, string areaid, string groupid, string brandid)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_GetQuantityByHome");
                SqlCmd.Parameters["@stateID"].Value = stateid;
                SqlCmd.Parameters["@areaID"].Value = areaid;
                SqlCmd.Parameters["@groupID"].Value = groupid;
                SqlCmd.Parameters["@brandid"].Value = brandid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

                return Sdr;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public bool SQSAdmin_Promotion_UpdateMultiplePromotionName(string promotionid, string promotionname)
        {
            DataSet Sdr;
            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_UpdateMultiplePromotionName");
                SqlCmd.Parameters["@promotionid"].Value = promotionid;
                SqlCmd.Parameters["@promotionname"].Value = promotionname;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool SQSAdmin_QuantityManagement_UpdateQuantityForAreaGroupAtHomeModel(string stateid, string areaid, string groupid, string brandid, string homemodel, string quantity, string usercode)
        {
            DataSet Sdr;

            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("AdminChangeQtyOfAreaGroupForAllHome");
                SqlCmd.Parameters["@homemodel"].Value = homemodel;
                SqlCmd.Parameters["@stateID"].Value = stateid;
                SqlCmd.Parameters["@areaID"].Value = areaid;
                SqlCmd.Parameters["@groupID"].Value = groupid;
                SqlCmd.Parameters["@qty"].Value = quantity;
                SqlCmd.Parameters["@createdBy"].Value = usercode;
                SqlCmd.Parameters["@brandid"].Value = brandid;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool SQSAdmin_QuantityManagement_UpdateQuantityForAreaGroupAtHomeFacade(string areaid, string groupid, string homeid, string quantity, string usercode)
        {
            DataSet Sdr;

            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_UpdateQtyOfAreaGroupForFacade");
                SqlCmd.Parameters["@homeid"].Value = homeid;
                SqlCmd.Parameters["@areaID"].Value = areaid;
                SqlCmd.Parameters["@groupID"].Value = groupid;
                SqlCmd.Parameters["@qty"].Value = quantity;
                SqlCmd.Parameters["@usercode"].Value = usercode;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool SQSAdmin_QuantityManagement_RefreshQuantity(string stateid, string areaid, string groupid, string usercode)
        {
            DataSet Sdr;

            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_AdminRefreshAndUpdateQuantity");
                SqlCmd.Parameters["@stateid"].Value = stateid;
                SqlCmd.Parameters["@areaID"].Value = areaid;
                SqlCmd.Parameters["@groupID"].Value = groupid;
                SqlCmd.Parameters["@usercode"].Value = usercode;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }


        public bool SQSAdmin_HomeConfiguration_BulkUpdateQuantity(string homeid, string pagidstring, string quantity, string usercode)
        {
            DataSet Sdr;

            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_BulkUpdateQuantityForHome");
                SqlCmd.Parameters["@homeid"].Value = homeid;
                SqlCmd.Parameters["@pagidstring"].Value = pagidstring;
                SqlCmd.Parameters["@quantity"].Value = quantity;
                SqlCmd.Parameters["@usercode"].Value = usercode;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool SQSAdmin_HomeConfiguration_BulkUpdateHomeModelQuantity(string brandid, string areaid, string groupid, string homestring, string quantity, string usercode)
        {
            DataSet Sdr;

            try
            {

                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_BulkUpdateQuantityForHomeModel");
                SqlCmd.Parameters["@brandid"].Value = brandid;
                SqlCmd.Parameters["@homestring"].Value = homestring;
                SqlCmd.Parameters["@areaid"].Value = areaid;
                SqlCmd.Parameters["@groupid"].Value = groupid;
                SqlCmd.Parameters["@quantity"].Value = quantity;
                SqlCmd.Parameters["@usercode"].Value = usercode;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }


        public DataSet SQSAdmin_Generic_GetProductUOM()
        {
            DataSet Sdr;

            try
            {
                SqlCommand SqlCmd = ConstructStoredProcedure("spa_AdminGetUOMCode");
                SqlCmd.Parameters["@code"].Value = "";
                SqlCmd.Parameters["@desc"].Value = "";
                SqlCmd.Parameters["@active"].Value = 1;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet SQSAdmin_Generic_GetProductCategory()
        {
            DataSet Sdr;

            try
            {
                SqlCommand SqlCmd = ConstructStoredProcedure("spa_AdminGetProductCategory");
                SqlCmd.Parameters["@code"].Value = "";
                SqlCmd.Parameters["@desc"].Value = "";
                SqlCmd.Parameters["@active"].Value = 1;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet SQSAdmin_Generic_GetProductCode()
        {
            DataSet Sdr;

            try
            {
                SqlCommand SqlCmd = ConstructStoredProcedure("spa_AdminGetProductCode");
                SqlCmd.Parameters["@code"].Value = "";
                SqlCmd.Parameters["@desc"].Value = "";
                SqlCmd.Parameters["@active"].Value = 1;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet SQSAdmin_Generic_GetProductDisplayCode(string forpromo)
        {
            DataSet Sdr;

            try
            {
                SqlCommand SqlCmd = ConstructStoredProcedure("spa_AdminGetPriceDisplayCode");
                SqlCmd.Parameters["@code"].Value = "";
                SqlCmd.Parameters["@desc"].Value = "";
                SqlCmd.Parameters["@active"].Value = 1;
                SqlCmd.Parameters["@forpromotion"].Value = forpromo;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet SQSAdmin_Generic_GetProductCostCenterCode()
        {
            DataSet Sdr;

            try
            {
                SqlCommand SqlCmd = ConstructStoredProcedure("spa_AdminGetProductCostCentreCode");
                SqlCmd.Parameters["@code"].Value = "";
                SqlCmd.Parameters["@desc"].Value = "";
                SqlCmd.Parameters["@active"].Value = 1;
                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet SQSAdmin_Generic_ImportProduct(DataTable tb, string usercode)
        {
            DataSet Sdr;

            try
            {
                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_ImportProduct");
                SqlCmd.Parameters["@dtable"].Value = tb;
                SqlCmd.Parameters["@usercode"].Value = usercode;

                foreach (SqlParameter parameter in SqlCmd.Parameters)
                {
                    if (parameter.SqlDbType != SqlDbType.Structured)
                    {
                        continue;
                    }
                    string name = parameter.TypeName;
                    int index = name.IndexOf(".");
                    if (index == -1)
                    {
                        continue;
                    }
                    name = name.Substring(index + 1);
                    if (name.Contains("."))
                    {
                        parameter.TypeName = name;
                    }
                }

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet SQSAdmin_Generic_ImportPriceVerticalFormat(DataTable tb, string usercode)
        {
            DataSet Sdr;
            try
            {
                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_ImportPriceVerticalFormat");
                SqlCmd.Parameters["@dtable"].Value = tb;
                SqlCmd.Parameters["@usercode"].Value = usercode;

                foreach (SqlParameter parameter in SqlCmd.Parameters)
                {
                    if (parameter.SqlDbType != SqlDbType.Structured)
                    {
                        continue;
                    }
                    string name = parameter.TypeName;
                    int index = name.IndexOf(".");
                    if (index == -1)
                    {
                        continue;
                    }
                    name = name.Substring(index + 1);
                    if (name.Contains("."))
                    {
                        parameter.TypeName = name;
                    }
                }

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet SQSAdmin_Generic_ImportPriceHorizontalFormat(DataTable tb, string usercode, string stateid)
        {
            DataSet Sdr;
            try
            {
                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_ImportPriceHorizontalFormat");
                SqlCmd.Parameters["@dtable"].Value = tb;
                SqlCmd.Parameters["@usercode"].Value = usercode;
                SqlCmd.Parameters["@stateid"].Value = stateid;

                foreach (SqlParameter parameter in SqlCmd.Parameters)
                {
                    if (parameter.SqlDbType != SqlDbType.Structured)
                    {
                        continue;
                    }
                    string name = parameter.TypeName;
                    int index = name.IndexOf(".");
                    if (index == -1)
                    {
                        continue;
                    }
                    name = name.Substring(index + 1);
                    if (name.Contains("."))
                    {
                        parameter.TypeName = name;
                    }
                }

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet SQSAdmin_Generic_ImportPriceHorizontalFormatNSW(DataTable tb, string usercode)
        {
            DataSet Sdr;
            try
            {
                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_ImportPriceHorizontalFormatNSW");
                SqlCmd.Parameters["@dtable"].Value = tb;
                SqlCmd.Parameters["@usercode"].Value = usercode;

                foreach (SqlParameter parameter in SqlCmd.Parameters)
                {
                    if (parameter.SqlDbType != SqlDbType.Structured)
                    {
                        continue;
                    }
                    string name = parameter.TypeName;
                    int index = name.IndexOf(".");
                    if (index == -1)
                    {
                        continue;
                    }
                    name = name.Substring(index + 1);
                    if (name.Contains("."))
                    {
                        parameter.TypeName = name;
                    }
                }

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet SQSAdmin_Generic_CheckExistingProducts(DataTable tb)
        {
            DataSet Sdr;
            try
            {
                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_CheckExistingProducts");
                SqlCmd.Parameters["@dtable"].Value = tb;

                foreach (SqlParameter parameter in SqlCmd.Parameters)
                {
                    if (parameter.SqlDbType != SqlDbType.Structured)
                    {
                        continue;
                    }
                    string name = parameter.TypeName;
                    int index = name.IndexOf(".");
                    if (index == -1)
                    {
                        continue;
                    }
                    name = name.Substring(index + 1);
                    if (name.Contains("."))
                    {
                        parameter.TypeName = name;
                    }
                }

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet SQSAdmin_Generic_ValidatePriceVerticalFormat(DataTable tb, string usercode)
        {
            DataSet Sdr;
            try
            {
                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_ValidatePriceVerticalFormat");
                SqlCmd.Parameters["@dtable"].Value = tb;
                SqlCmd.Parameters["@usercode"].Value = usercode;

                foreach (SqlParameter parameter in SqlCmd.Parameters)
                {
                    if (parameter.SqlDbType != SqlDbType.Structured)
                    {
                        continue;
                    }
                    string name = parameter.TypeName;
                    int index = name.IndexOf(".");
                    if (index == -1)
                    {
                        continue;
                    }
                    name = name.Substring(index + 1);
                    if (name.Contains("."))
                    {
                        parameter.TypeName = name;
                    }
                }

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet SQSAdmin_Generic_ValidatePriceHorizontalFormat(DataTable tb, string usercode, string stateid)
        {
            DataSet Sdr;
            try
            {
                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_ValidatePriceHorizontalFormat");
                SqlCmd.Parameters["@dtable"].Value = tb;
                SqlCmd.Parameters["@usercode"].Value = usercode;
                SqlCmd.Parameters["@stateid"].Value = stateid;

                foreach (SqlParameter parameter in SqlCmd.Parameters)
                {
                    if (parameter.SqlDbType != SqlDbType.Structured)
                    {
                        continue;
                    }
                    string name = parameter.TypeName;
                    int index = name.IndexOf(".");
                    if (index == -1)
                    {
                        continue;
                    }
                    name = name.Substring(index + 1);
                    if (name.Contains("."))
                    {
                        parameter.TypeName = name;
                    }
                }

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet SQSAdmin_Generic_ValidatePriceHorizontalFormatNSW(DataTable tb, string usercode)
        {
            DataSet Sdr;
            try
            {
                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_ValidatePriceHorizontalFormatNSW");
                SqlCmd.Parameters["@dtable"].Value = tb;
                SqlCmd.Parameters["@usercode"].Value = usercode;

                foreach (SqlParameter parameter in SqlCmd.Parameters)
                {
                    if (parameter.SqlDbType != SqlDbType.Structured)
                    {
                        continue;
                    }
                    string name = parameter.TypeName;
                    int index = name.IndexOf(".");
                    if (index == -1)
                    {
                        continue;
                    }
                    name = name.Substring(index + 1);
                    if (name.Contains("."))
                    {
                        parameter.TypeName = name;
                    }
                }

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet SQSAdmin_Generic_ValidateQuantity(DataTable tb, string usercode)
        {
            DataSet Sdr;
            try
            {
                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_ValidateQuantity");
                SqlCmd.Parameters["@dtable"].Value = tb;
                SqlCmd.Parameters["@usercode"].Value = usercode;

                foreach (SqlParameter parameter in SqlCmd.Parameters)
                {
                    if (parameter.SqlDbType != SqlDbType.Structured)
                    {
                        continue;
                    }
                    string name = parameter.TypeName;
                    int index = name.IndexOf(".");
                    if (index == -1)
                    {
                        continue;
                    }
                    name = name.Substring(index + 1);
                    if (name.Contains("."))
                    {
                        parameter.TypeName = name;
                    }
                }

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet SQSAdmin_Generic_ImportQuantity(DataTable tb, string usercode)
        {
            DataSet Sdr;
            try
            {
                SqlCommand SqlCmd = ConstructStoredProcedure("spa_Admin_ImportQuantity");
                SqlCmd.Parameters["@dtable"].Value = tb;
                SqlCmd.Parameters["@usercode"].Value = usercode;

                foreach (SqlParameter parameter in SqlCmd.Parameters)
                {
                    if (parameter.SqlDbType != SqlDbType.Structured)
                    {
                        continue;
                    }
                    string name = parameter.TypeName;
                    int index = name.IndexOf(".");
                    if (index == -1)
                    {
                        continue;
                    }
                    name = name.Substring(index + 1);
                    if (name.Contains("."))
                    {
                        parameter.TypeName = name;
                    }
                }

                Sdr = ExcuteSqlStoredProcedure(SqlCmd);
                return Sdr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #region help function to connect and run sp

        public SqlCommand ConstructStoredProcedure(string SP)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["SqlConnection"].ToString();
            SqlCommand Cmd = null;

            try
            {
                if (connectionstring != null)
                {
                    connection = new SqlConnection(connectionstring);
                    connection.Open();
                }
                Cmd = new SqlCommand(SP, connection);
                Cmd.CommandTimeout = 240000; //timeout;
                SqlDataAdapter Adapter = new SqlDataAdapter(Cmd);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(Cmd);
            }
            catch (Exception)
            {
                if (connection != null)
                    connection.Dispose();
                if (Cmd != null)
                    Cmd.Dispose();

                throw;
            }
            return Cmd;
        }

        public DataSet ExcuteSqlStoredProcedure(SqlCommand SelectCommand)
        {
            try
            {
                Adapter = new SqlDataAdapter(SelectCommand);
                SelectCommand.CommandTimeout = timeout;
                ds = new DataSet();
                Adapter.Fill(ds);

                connection.Close();

                return ds;
            }

            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }
        #endregion
    }
}