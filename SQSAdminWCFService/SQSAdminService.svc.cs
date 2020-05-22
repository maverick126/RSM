using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;


namespace SQSAdminWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class SQSAdminService : ISQSAdminService
    {
        SQLDataAccess dataAccess = new SQLDataAccess();
        public DataSet SQSAdmin_Generic_GetState()
        {
            return dataAccess.GetStateList();
        }

        public DataSet SQSAdmin_Generic_GetAvailableGroup()
        {
            return dataAccess.GetStateList();
        }

        public DataSet SQSAdmin_Generic_GetRegionList(int stateid)
        {
            return dataAccess.GetRegionList(stateid);
        }

        public DataSet SQSAdmin_Generic_GetBrandByState(int stateid)
        {
            return dataAccess.GetBrandList(stateid);
        }

        public DataSet SQSAdmin_Generic_GetAllAreas()
        {
            return dataAccess.GetArea();
        }

        public DataSet SQSAdmin_Generic_GetAllGroups()
        {
            return dataAccess.GetGroup();
        }
        public DataSet SQSAdmin_Generic_GetGroupsFromArea(int areaid)
        {
            return dataAccess.GetGroupFromArea(areaid);
        }

        public DataSet SQSAdmin_Generic_GetHomeNameBySearch(int stateid, int brandid, string homename)
        {
            return dataAccess.GetHomeNameBySearch(stateid, brandid, homename);
        }

        public DataSet SQSAdmin_Generic_GetSQSConfiguration(string configCode, string codeValue)
        {
            return dataAccess.GetSQSConfiguration(configCode, codeValue);
        }
        public DataSet SQSAdmin_Generic_GetSpecificationByStateRegionsBrands(int stateid, string regionids, string brandids, DateTime effdate, string systemid, string attachmentid, string homestring)
        {
            return dataAccess.GetSpecificationByStateRegionsBrands(stateid, regionids, brandids, effdate, systemid, attachmentid, homestring);
        }
        public DataSet SQSAdmin_Generic_GetSpecificationByStateRegionBrand(int stateid, int regionid, int brandid, DateTime effdate, string systemid, string attachmentid, string homestring)
        {
            return dataAccess.GetSpecificationByStateRegionBrand(stateid, regionid, brandid, effdate, systemid, attachmentid, homestring);
        }
        public void SQSAdmin_Generic_SaveSpecificationByStateRegionBrand(int id, string filename, int stateid, int regionid, int brandid, int homeid, DateTime effdate, int systemformid, int attachmenttypeid, int active)
        {
            dataAccess.SaveSpecificationByStateRegionBrand(id, filename, stateid, regionid, brandid, homeid, effdate, systemformid, attachmenttypeid, active);
        }
        public DataSet SQSAdmin_Generic_GetAvailabeAreasByHome(int homeid)
        {
            return dataAccess.GetAvailableAreasByHomeID(homeid);
        }

        public DataSet SQSAdmin_Generic_GetConfiguredAreasByHome(int homeid)
        {
            return dataAccess.GetConfiguredAreasByHomeID(homeid);
        }

        public void SQSAdmin_Generic_AddMinimumAreasToHome(int homeid, string selectedareaid, string usercode)
        {
            dataAccess.AddMinimumAreasToHome(homeid, selectedareaid, usercode);
        }

        public void SQSAdmin_Generic_RemoveMinimumAreasFromHome(int homeid, string selectedareaid, string usercode)
        {
            dataAccess.RemoveMinimumAreasFromHome(homeid, selectedareaid, usercode);
        }

        public DataSet SQSAdmin_Generic_GetAvailablePAGByState(string stateid, string areaid, string groupid, string keyword, string pagid)
        {
            return dataAccess.GetAvailablePAGByState(stateid, areaid, groupid, keyword, pagid);
        }

        public DataSet SQSAdmin_Generic_GetDisplayHomeByPAGAndState(string stateid, string brandid, string pagid, string displayhomename, string suburb)
        {
            return dataAccess.GetDisplayHomeByPAGAndState(stateid, brandid, pagid, displayhomename, suburb);
        }

        public string SQSAdmin_Generic_ReplaceDisplayHomePAG(string existingpagid, string newpagid, string displayhomeid, string usercode, string qty, string changeqty, string changeprice, string allowdesc, string desc, string newextradesc)
        {
            return dataAccess.ReplaceDisplayHomePAG(existingpagid, newpagid, displayhomeid, usercode, qty, changeqty, changeprice, allowdesc, desc, newextradesc);
        }


        public DataSet SQSAdmin_Generic_GetProductUOM()
        {
            return dataAccess.SQSAdmin_Generic_GetProductUOM();
        }

        public DataSet SQSAdmin_Generic_GetProductCategory()
        {
            return dataAccess.SQSAdmin_Generic_GetProductCategory();
        }


        public DataSet SQSAdmin_Generic_GetProductCode()
        {
            return dataAccess.SQSAdmin_Generic_GetProductCode();
        }


        public DataSet SQSAdmin_Generic_GetProductDisplayCode(string forpromo)
        {
            return dataAccess.SQSAdmin_Generic_GetProductDisplayCode(forpromo);
        }


        public DataSet SQSAdmin_Generic_GetProductCostCenterCode()
        {
            return dataAccess.SQSAdmin_Generic_GetProductCostCenterCode();
        }

        public DataSet SQSAdmin_Generic_ImportProduct(DataTable dt, string usercode)
        {
            return dataAccess.SQSAdmin_Generic_ImportProduct(dt, usercode);
        }

        public DataSet SQSAdmin_Generic_ImportPriceVerticalFormat(DataTable dt, string usercode)
        {
            return dataAccess.SQSAdmin_Generic_ImportPriceVerticalFormat(dt, usercode);
        }

        public DataSet SQSAdmin_Generic_ImportPriceHorizontalFormat(DataTable dt, string usercode, string stateid)
        {
            return dataAccess.SQSAdmin_Generic_ImportPriceHorizontalFormat(dt, usercode, stateid);
        }

        public DataSet SQSAdmin_Generic_ImportPriceHorizontalFormatNSW(DataTable dt, string usercode)
        {
            return dataAccess.SQSAdmin_Generic_ImportPriceHorizontalFormatNSW(dt, usercode);
        }

        public DataSet SQSAdmin_Generic_CheckExistingProducts(DataTable dt)
        {
            return dataAccess.SQSAdmin_Generic_CheckExistingProducts(dt);
        }

        public DataSet SQSAdmin_Generic_ValidatePriceVerticalFormat(DataTable dt, string usercode)
        {
            return dataAccess.SQSAdmin_Generic_ValidatePriceVerticalFormat(dt, usercode);
        }

        public DataSet SQSAdmin_Generic_ValidatePriceHorizontalFormat(DataTable dt, string usercode, string stateid)
        {
            return dataAccess.SQSAdmin_Generic_ValidatePriceHorizontalFormat(dt, usercode, stateid);
        }

        public DataSet SQSAdmin_Generic_ValidatePriceHorizontalFormatNSW(DataTable dt, string usercode)
        {
            return dataAccess.SQSAdmin_Generic_ValidatePriceHorizontalFormatNSW(dt, usercode);
        }

        public DataSet SQSAdmin_Generic_ValidateQuantity(DataTable dt, string usercode)
        {
            return dataAccess.SQSAdmin_Generic_ValidateQuantity(dt, usercode);
        }
        public DataSet SQSAdmin_Generic_ImportQuantity(DataTable dt, string usercode)
        {
            return dataAccess.SQSAdmin_Generic_ImportQuantity(dt, usercode);
        }

        public DataSet SQSAdmin_Promotion_GetPromotionList(int stateid, int brandid, int storey, int status)
        {
            return dataAccess.GetPromotionList(stateid, brandid, storey, status);
        }
        public DataSet SQSAdmin_Promotion_GetPromotionTypeList()
        {
            return dataAccess.GetPromotionTypeList();
        }

        public bool SQSAdmin_Promotion_CheckMasterPromotionProductExist(int stateid, string productid)
        {
            return dataAccess.CheckMasterPromotionProductExist(stateid, productid);
        }

        public bool SQSAdmin_Promotion_CheckMultiplePromotionExist(int brandid, int stories, string productid)
        {
            return dataAccess.CheckMultiplePromotionExist(brandid, stories, productid);
        }
        public void SQSAdmin_Promotion_SavePromotion(int stateid, int brandid, int storey, int promotiontypeid, string productid, string promotionname, decimal promotioncost, decimal capevalue, int forregional, int active, int displaycodeid)
        {
            dataAccess.SavePromotion(stateid, brandid, storey, promotiontypeid, productid, promotionname, promotioncost, capevalue, forregional, active, displaycodeid);
        }

        public DataSet SQSAdmin_Promotion_GetPromotionDetails(int multiplepromotionid)
        {
            return dataAccess.GetMultiplePromotionDetails(multiplepromotionid);
        }
        public DataSet SQSAdmin_Promotion_GetAvailableProductsForPromotion(int multiplepromotionid, int areaid, int groupid, string productstring, string pagid)
        {
            return dataAccess.GetAvailableProductsForPromotion(multiplepromotionid, areaid, groupid, productstring, pagid);
        }

        public void SQSAdmin_Promotion_AddProductsToPromotion(int multiplepromotionid, string selectedpagid)
        {
            dataAccess.AddProductsToPromotion(multiplepromotionid, selectedpagid);
        }
        public DataSet SQSAdmin_Promotion_GetAreaInPromotion(int multiplepromotionid)
        {
            return dataAccess.GetPromotionProductArea(multiplepromotionid);
        }
        public DataSet SQSAdmin_Promotion_GetGroupInPromotion(int multiplepromotionid)
        {
            return dataAccess.GetPromotionProductGroup(multiplepromotionid);
        }

        public DataSet SQSAdmin_Promotion_GetExistingProductsForPromotion(int multiplepromotionid, int areaid, int groupid, string productstring, string pagid)
        {
            return dataAccess.GetExistingProductsForPromotion(multiplepromotionid, areaid, groupid, productstring, pagid);
        }

        public void SQSAdmin_Promotion_RemoveProductsFromPromotion(int multiplepromotionid, string selectedpagid)
        {
            dataAccess.RemoveProductsFromPromotion(multiplepromotionid, selectedpagid);
        }

        public void SQSAdmin_Promotion_UpdateProductForPromotion(int multiplepromotionid, int pagid, int defaultselected, decimal discount)
        {
            dataAccess.UpdatePromotionProduct(multiplepromotionid, pagid, defaultselected, discount);
        }

        public void SQSAdmin_Promotion_UpdatePromotion(int multiplepromotionid, string baseproduct, decimal promotioncost, decimal capevalue, int forregional, int active, int displaycodeid, string promotionname)
        {
            dataAccess.UpdatePromotion(multiplepromotionid, baseproduct, promotioncost, capevalue, forregional, active, displaycodeid, promotionname);
        }

        public DataSet SQSAdmin_Promotion_GetPromotionForProduct(int pagid, string productid)
        {
            return dataAccess.GetPromotionForProduct(pagid, productid);
        }

        public DataSet SQSAdmin_Promotion_GetPromotionForCopy(int multiplepromotionid)
        {
            return dataAccess.GetSourcePromotionForCopy(multiplepromotionid);
        }

        public bool SQSAdmin_Promotion_CopyPromotionProductsFromSourceToTarget(int targetpromotionid, int sourcepromotionid)
        {
            return dataAccess.CopyPromotionProductFromSourceToTarget(targetpromotionid, sourcepromotionid);
        }

        public bool SQSAdmin_Promotion_UpdateMultiplePromotionName(string promotionid, string promotionname)
        {
            return dataAccess.SQSAdmin_Promotion_UpdateMultiplePromotionName(promotionid, promotionname);
        }

        public DataSet SQSAdmin_StudioM_GetSuppliers(int stateid, string suppliername, int active)
        {
            return dataAccess.GetSupplier(stateid, suppliername, active);
        }

        public DataSet SQSAdmin_StudioM_GetActiveQuestions(int stateid)
        {
            return dataAccess.GetActiveQuestion(stateid);
        }

        public DataSet SQSAdmin_StudioM_SearchActiveQuestions(int stateid, string searchtext)
        {
            return dataAccess.SearchActiveQuestion(stateid, searchtext);
        }

        public DataSet SQSAdmin_StudioM_GetAvailableGroupListForRetailCluster(int retailclusterid, string groupname)
        {
            return dataAccess.GetAvailableGroupList(retailclusterid, groupname);
        }

        public DataSet SQSAdmin_StudioM_GetExistingGroupListForRetailCluster(int retailclusterid)
        {
            return dataAccess.GetExistingGroupList(retailclusterid);
        }

        public DataSet SQSAdmin_StudioM_GetRetailCluster(int stateid, string clustername)
        {
            return dataAccess.GetRetailCluster(stateid, clustername);
        }

        public bool SQSAdmin_StudioM_IsRetailClusterExists(int stateid, string clustername)
        {
            return dataAccess.CheckRetailClusterExists(stateid, clustername);
        }
        public DataSet SQSAdmin_StudioM_GetAnswerForQuestion(int questionid)
        {
            return dataAccess.GetAnswerForQuestion(questionid);
        }

        public void SQSAdmin_StudioM_UpdateProductStudioMAttribute(string productid, string attribute)
        {
            dataAccess.UpdateStudioMAttribute(productid, attribute);
        }
        public string SQSAdmin_StudioM_GetProductStudioMAttribute(string productid)
        {
            return dataAccess.GetProductStudioMAttribute(productid);
        }

        public bool SQSAdmin_StudioM_IsSupplierExists(int stateid, string suppliername)
        {
            return dataAccess.CheckSupplierExists(stateid, suppliername);
        }

        public void SQSAdmin_StudioM_AddEditSupplier(int supplierid, int stateid, string suppliername, int active)
        {
            dataAccess.SaveSupplier(supplierid, stateid, suppliername, active);
        }


        public DataSet SQSAdmin_StudioM_GetAnswerType()
        {
            return dataAccess.GetAnserType();
        }

        public DataSet SQSAdmin_StudioM_GetQuestionList(string question, int active, int stateid)
        {
            return dataAccess.GetQuestionList(question, active, stateid);

        }

        public bool SQSAdmin_StudioM_IsQuestionExists(int answertypeid, string questiontext, int stateid)
        {
            return dataAccess.CheckQuestionExists(answertypeid, questiontext, stateid);
        }

        public void SQSAdmin_StudioM_AddEditQuestion(int questionid, int answertypeid, string questiontext, int active, int mandatory, int stateid)
        {
            dataAccess.SaveQuestion(questionid, answertypeid, questiontext, active, mandatory, stateid);
        }

        public DataSet SQSAdmin_StudioM_GetAnswerList(string questiontext, string answertext, int active, int stateid)
        {
            return dataAccess.GetAnswerList(questiontext, answertext, active, stateid);
        }
        public bool SQSAdmin_StudioM_IsAnswerExists(int questionid, string answertext)
        {
            return dataAccess.CheckAnswerExists(questionid, answertext);
        }

        public void SQSAdmin_StudioM_AddEditAnswer(int questionid, int answerid, string answertext, int active)
        {
            dataAccess.SaveAnswer(questionid, answerid, answertext, active);
        }

        public DataSet SQSAdmin_StudioM_GetProductPrice(int stateid, int regionid, string productid, string productname, string productdescription, int active, int studiomproduct)
        {
            return dataAccess.GetProductPriceList(stateid, regionid, active, productid, productname, productdescription, studiomproduct);
        }

        public DataSet SQSAdmin_StudioM_GetProductImages(string productid, int supplierid)
        {
            return dataAccess.GetProductImages(productid, supplierid);
        }

        public void SQSAdmin_StudioM_SaveProductImage(string productid, string imagename, int supplierid, byte[] imagestream)
        {
            dataAccess.SaveProductImage(productid, imagename, supplierid, imagestream);
        }

        public void SQSAdmin_StudioM_RemoveProductImage(int imageid)
        {
            dataAccess.DeleteProductImage(imageid);
        }

        public DataSet SQSAdmin_StudioM_GetProductByCategoryCount(int stateid)
        {
            return dataAccess.GetProductByCategoryCount(stateid);
        }
        public DataSet SQSAdmin_StandardInclusion_GetStandardInclusionProducts(string productid, string productname, int stateid, string brandids)
        {
            return dataAccess.GetStandardInclusion(productid, productname, stateid, brandids);
        }

        public DataSet SQSAdmin_StandardInclusion_GetUpgradeOptionByStandardInclusion(string productid, string brandids)
        {
            return dataAccess.GetUpgradeOptionByStandardInlcusion(productid, brandids);
        }

        public DataSet SQSAdmin_StandardInclusion_GetAvailableUpgradeOptionProducts(string productid, string productname, string brandids, int stateid, string stdinclusionproductid)
        {
            return dataAccess.GetAvailableUpgradeOption(productid, productname, brandids, stateid, stdinclusionproductid);
        }

        public void SQSAdmin_StandardInclusion_RemoveValidationRule(int validationruleid, bool update = false, bool active = true)
        {
            dataAccess.RemoveValidationRule(validationruleid, update, active);
        }

        public void SQSAdmin_StandardInclusion_SaveValidationRule(string standardinclusionproductid, string upgradeoptionproductids, string brandids, string usercode, bool promotion, DateTime effectivedate)
        {
            dataAccess.SaveValidationRule(standardinclusionproductid, upgradeoptionproductids, brandids, usercode, promotion, effectivedate);
        }

        public void SQSAdmin_StudioM_AddEditRetailCluster(int retailclusterid, string clustername, int stateid, int active, string loginuser)
        {
            dataAccess.AddEditRetailCluster(retailclusterid, clustername, stateid, active, loginuser);
        }

        public void SQSAdmin_StudioM_SaveSelectedGroupToRetailCluster(int retailclusterid, string selectedgroupid, string usercode)
        {
            dataAccess.SaveSelectedGroupToRetailCluster(retailclusterid, selectedgroupid, usercode);
        }

        public void SQSAdmin_StudioM_RemoveSelectedGroupFromRetailCluster(int retailclusterid, string selectedgroupid)
        {
            dataAccess.RemoveSelectedGroupFromRetailCluster(retailclusterid, selectedgroupid);
        }

        public void SQSAdmin_StudioM_UpdateStudioMAttributeForClusterOrGroup(string id, string xml, string type, string usercode, string supplieridstring, string questionidstring, string answeridstring, string mandatorystring, string stateid)
        {
            dataAccess.UpdateStudioMAttributeForClusterOrGroup(id, xml, type, usercode, supplieridstring, questionidstring, answeridstring, mandatorystring, stateid);
        }

        public DataSet SQSAdmin_StudioM_LoadStudioMAttributeForClusterOrGroup(string id, string type, string stateid)
        {
            return dataAccess.LoadStudioMAttributeForClusterOrGroup(int.Parse(id), type, int.Parse(stateid));
        }

        public DataSet SQSAdmin_StudioM_LoadSQSGroupList(string groupname)
        {
            return dataAccess.LoadSQSGroupList(groupname);
        }

        public DataSet SQSAdmin_StandardInclusion_GetPAGFromProduct(string productid)
        {
            return dataAccess.GetPAGForProduct(productid);
        }

        public DataSet SQSAdmin_StandardInclusion_GetRegionGroupByState(int pstateid)
        {
            return dataAccess.GetRegiongroupNameByState(pstateid);
        }

        public DataSet SQSAdmin_StandardInclusion_GetStandardInclusions(int pbrandid, int pregiongroupid, int pagid, int pstateid, string productid, string productname)
        {
            return dataAccess.GetStandardInclusion(pbrandid, pregiongroupid, pagid, pstateid, productid, productname);
        }

        public DataSet SQSAdmin_StudioM_GetSupplierBrand(string brandname, int pstateid, int active)
        {
            return dataAccess.GetSupplierBrand(brandname, pstateid, active);
        }

        public void SQSAdmin_StudioM_AddEditSupplierBrand(int supplierbrandid, string brandname, int pstateid, int active, string usercode)
        {
            dataAccess.AddEditSupplierBrand(supplierbrandid, brandname, pstateid, active, usercode);
        }

        public bool SQSAdmin_StudioM_CheckSupplierBrandExists(string brandname, int pstateid)
        {
            return dataAccess.CheckSupplierBrandExists(brandname, pstateid);
        }

        public bool SQSAdmin_StandardInclusion_IsStandardInclusionExists(int pagid, int pbrandid, int pregiongroupid, int standardinclusionid)
        {
            return dataAccess.CheckStandardInclusionExists(pagid, pbrandid, pregiongroupid, standardinclusionid);
        }

        public void SQSAdmin_StandardInclusion_AddEditStandardInclusion(int standardinclusionid, int pagid, int pbrandid, int pregiongroupid, decimal quantity, int active, string usercode)
        {
            dataAccess.AddEditStandardInclusion(standardinclusionid, pagid, pbrandid, pregiongroupid, quantity, active, usercode);
        }

        public void SQSAdmin_StandardInclusion_RemoveStandardInclusion(int standardinclusionid)
        {
            dataAccess.RemoveStandardInclusion(standardinclusionid);
        }

        public DataSet SQSAdmin_StandardInclusion_GetAvailableStandardInclusionPAGFromMasterList(int areaid, int groupid, string productid, string productname, int stateid)
        {
            return dataAccess.GetAvailableStandardInclusionPAGFromMasterList(areaid, groupid, productid, productname, stateid);
        }

        public DataSet SQSAdmin_StandardInclusion_GetAvailableStandardInclusionPAG(int areaid, int groupid, string productid, string productname, int pbrandid, int pregiongroupid)
        {
            return dataAccess.GetAvailableStandardInclusionPAG(areaid, groupid, productid, productname, pbrandid, pregiongroupid);
        }


        public void SQSAdmin_StandardInclusion_SaveStandardInclusionToBrand(string selectedids, string brandids, int regiongroupid, string usercode, DateTime effectivedate)
        {
            dataAccess.SaveStandardInclusionToBrand(selectedids, brandids, regiongroupid, usercode, effectivedate);
        }


        public DataSet SQSAdmin_HomeMinimumArea_GetSourceHomeByBrand(int brandid, string homename)
        {
            return dataAccess.GetSourceHomeByBrand(brandid, homename);
        }

        public DataSet SQSAdmin_HomeMinimumArea_GetTargetHomeByBrand(int brandid, string homename)
        {
            return dataAccess.GetTargetHomeByBrand(brandid, homename);
        }

        public void SQSAdmin_HomeMinimumArea_CopyMinimumAreasFromSourceHomeToTargetHome(string sourcehomeid, string targethomeidstring, string usercode)
        {
            dataAccess.CopyMinimumAreasFromSourceHomeToTargetHome(sourcehomeid, targethomeidstring, usercode);
        }

        public void SQSAdmin_StudioM_UploadImageToSharepointImageLibrary(string productid, int supplierid)
        {
            SharepointImagingWebService.Imaging imgws = new SharepointImagingWebService.Imaging();
            imgws.Credentials = System.Net.CredentialCache.DefaultCredentials;
            imgws.Url = "http://vm-sharedev01/_vti_bin/imaging.asmx";

            System.Xml.XmlNode resnode = imgws.GetListItems("PictLib", "0");
            //System.Xml.XmlNode resnode = imgws.ListPictureLibrary();

            //DataSet ds = dataAccess.GetProductImages(productid, supplierid);

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        System.Xml.XmlNode resnode;
            //        Byte[] b = (Byte[])dr["image"];
            //        imgws.Upload("PictLib", "", b, dr["fkidproduct"].ToString() + "_" + dr["imagename"].ToString(), true);
            //    }
            //}


        }

        public DataSet SQSAdmin_RelatedArea_LoadProduct(string productid, string keyword, int stateid)
        {
            return dataAccess.LoadProduct(stateid, productid, keyword);
        }

        public DataSet SQSAdmin_RelatedArea_LoadExistingAreasForProduct(string productid, int active)
        {
            return dataAccess.LoadExistingAreasForProduct(productid, active);
        }

        public DataSet SQSAdmin_RelatedArea_LoadAvailableAreasForProduct(string productid, string keyword, string callfrom)
        {
            return dataAccess.LoadAvailableAreasForProduct(productid, keyword, callfrom);
        }

        public void SQSAdmin_RelatedArea_AddSelectedAreasToProduct(string productid, string areaidstring, string usercode, string callfrom)
        {
            dataAccess.AddSelectedAreasToProduct(productid, areaidstring, usercode, callfrom);
        }

        public void SQSAdmin_RelatedArea_RemooveSelectedAreasFromProduct(string productid, string areaidstring, string callfrom)
        {
            dataAccess.RemoveSelectedAreasFromProduct(productid, areaidstring, callfrom);
        }

        public DataSet SQSAdmin_StudioM_GetStudioMProduct(int stateid, string productid, string productname)
        {
            return dataAccess.GetStudioMProduct(stateid, productid, productname);
        }

        public bool SQSAdmin_StudioM_BulkConfigureStudiomQandA(string supplierbrandid, string productidstring, string questionidstring, string answeridstring, string usercode)
        {
            return dataAccess.BulkConfigureStudiomQandA(supplierbrandid, productidstring, questionidstring, answeridstring, usercode);
        }

        public DataSet SQSAdmin_Home_GetHomeList(int stateid, int brandid, string homename, int active)
        {
            return dataAccess.SQSAdmin_Home_GetHomeList(stateid, brandid, homename, active);
        }

        public DataSet SQSAdmin_Home_GetHomePlanByStateAndBrands(int stateid, string brandids)
        {
            return dataAccess.SQSAdmin_Home_GetHomePlanByStateAndBrands(stateid, brandids);
        }

        public DataSet SQSAdmin_Generic_GetAreas(int stateid)
        {
            return dataAccess.SQSAdmin_Generic_GetAreas(stateid);
        }


        public DataSet SQSAdmin_Generic_GetGroups(int stateid)
        {
            return dataAccess.SQSAdmin_Generic_GetGroups(stateid);
        }

        public DataSet SQSAdmin_HomeConfiguration_GetAvailablePAGForHome(int homeid, int areaid, int groupid, string keyword)
        {
            return dataAccess.SQSAdmin_HomeConfiguration_GetAvailablePAGForHome(homeid, areaid, groupid, keyword);
        }

        public DataSet SQSAdmin_HomeConfiguration_GetExistingPAGForHome(int homeid, int areaid, int groupid, string keyword)
        {
            return dataAccess.SQSAdmin_HomeConfiguration_GetExistingPAGForHome(homeid, areaid, groupid, keyword);
        }
        public bool SQSAdmin_HomeConfiguration_AddProductToHome(int homeid, string selectedpagid, string qtystring, string changeqtystring, string changepricestring, string extradescstring, string usercode)
        {
            return dataAccess.SQSAdmin_HomeConfiguration_AddProductToHome(homeid, selectedpagid, qtystring, changeqtystring, changepricestring, extradescstring, usercode);
        }
        public bool SQSAdmin_HomeConfiguration_UpdatePagForHome(int homeid, int pagid, string quantity, int changeqty, int changeprice, int extradesc, string usercode)
        {
            return dataAccess.SQSAdmin_HomeConfiguration_UpdatePagForHome(homeid, pagid, quantity, changeqty, changeprice, extradesc, usercode);
        }

        public bool SQSAdmin_HomeConfiguration_RemoveProductsFromHome(int homeid, string selectedpagids, string usercode)
        {
            return dataAccess.SQSAdmin_HomeConfiguration_RemoveProductForHome(homeid, selectedpagids, usercode);
        }

        public DataSet SQSAdmin_HomeConfiguration_GetProducts(int stateid, string productid, string keyword)
        {
            return dataAccess.SQSAdmin_HomeConfiguration_GetProducts(stateid, productid, keyword);
        }
        public string SQSAdmin_HomeConfiguration_CopyProductConfiguration(string sourceproductid, string targetproductid, string usercode)
        {
            return dataAccess.SQSAdmin_HomeConfiguration_CopyProductConfiguration(sourceproductid, targetproductid, usercode);
        }
        public DataSet SQSAdmin_QuantityManagement_GetHomeModelQuantity(string stateid, string areaid, string groupid, string brandid)
        {
            return dataAccess.SQSAdmin_QuantityManagement_GetHomeModelQuantity(stateid, areaid, groupid, brandid);
        }

        public DataSet SQSAdmin_QuantityManagement_GetHomeFacadeQuantity(string stateid, string areaid, string groupid, string brandid)
        {
            return dataAccess.SQSAdmin_QuantityManagement_GetHomeFacadeQuantity(stateid, areaid, groupid, brandid);
        }

        public bool SQSAdmin_QuantityManagement_UpdateQuantityForAreaGroupAtHomeModel(string stateid, string areaid, string groupid, string brandid, string homemodel, string quantity, string usercode)
        {
            return dataAccess.SQSAdmin_QuantityManagement_UpdateQuantityForAreaGroupAtHomeModel(stateid, areaid, groupid, brandid, homemodel, quantity, usercode);
        }

        public bool SQSAdmin_QuantityManagement_UpdateQuantityForAreaGroupAtHomeFacade(string areaid, string groupid, string homeid, string quantity, string usercode)
        {
            return dataAccess.SQSAdmin_QuantityManagement_UpdateQuantityForAreaGroupAtHomeFacade(areaid, groupid, homeid, quantity, usercode);
        }

        public bool SQSAdmin_QuantityManagement_RefreshQuantity(string stateid, string areaid, string groupid, string usercode)
        {
            return dataAccess.SQSAdmin_QuantityManagement_RefreshQuantity(stateid, areaid, groupid, usercode);
        }

        public bool SQSAdmin_HomeConfiguration_BulkUpdateQuantity(string homeid, string pagidstring, string quantity, string usercode)
        {
            return dataAccess.SQSAdmin_HomeConfiguration_BulkUpdateQuantity(homeid, pagidstring, quantity, usercode);
        }
        public bool SQSAdmin_HomeConfiguration_BulkUpdateHomeModelQuantity(string brandid, string areaid, string groupid, string homestring, string quantity, string usercode)
        {
            return dataAccess.SQSAdmin_HomeConfiguration_BulkUpdateHomeModelQuantity(brandid, areaid, groupid, homestring, quantity, usercode);
        }

        public DataSet SQSAdmin_SearchDisclaimers(int typeid, int stateid, int regionid, int brandid)
        {
            return dataAccess.SearchDisclaimers(typeid, stateid, regionid, brandid);
        }

        public string SQSAdmin_SearchDisclaimer(string type, string state, int regionid, int brandid, string brandname, DateTime depositDate)
        {
            return dataAccess.SearchDisclaimer(type, state, regionid, brandid, brandname, depositDate);
        }

        public string SQSAdmin_SaveDisclaimer(string type, string state, string regionids, string brandids, string disclaimerText, string internalNotes, string effectiveDate, int status, string usercode)
        {
            return dataAccess.SaveDisclaimer(type, state, regionids, brandids, disclaimerText, internalNotes, effectiveDate, status, usercode);
        }

        public DataSet SQSAdmin_Generic_BasePriceHoldDays(string stateid, string regionids, string brandids, DateTime effectivedate, string active)
        {
            return dataAccess.GetBasePriceHoldingDays(stateid, regionids, brandids, effectivedate, active);
        }

        public void SQSAdmin_Generic_UpdateBasePriceHoldDays(string id, string daysfrom, string daysto, DateTime effectivedate, string active, string depositamount, string usercode, decimal cmapercent, decimal surchargepercent, decimal btpsinglestorey, decimal btpdoublestorey, decimal btpSingleStoryDiscount, decimal btpDoubleStoryDiscount, decimal btpSingleStoreyCostSiteOther, decimal btpDoubleStoreyCostSiteOther)
        {
            dataAccess.UpdateBasePriceHoldingDays(id, daysfrom, daysto, effectivedate, active, depositamount, usercode, cmapercent, surchargepercent, btpsinglestorey, btpdoublestorey, btpSingleStoryDiscount, btpDoubleStoryDiscount, btpSingleStoreyCostSiteOther, btpDoubleStoreyCostSiteOther);
        }

        public void SQSAdmin_Generic_NeweBasePriceHoldDays(int stateid, string regionids, string brandids, string daysfrom, string daysto, DateTime effectivedate, string active, string depositamount, string usercode, string cmapercent, string surchargepercent, string regionalsurchargeSSpercent, string regionalsurchargeSDpercent, string btpSingleStoryDiscount, string btpDoubleStoryDiscount, string btpSingleStoreyCostSiteOther, string btpDoubleStoreyCostSiteOther)
        {
            dataAccess.NewBasePriceHoldingDays(stateid, regionids, brandids, daysfrom, daysto, effectivedate, active, depositamount, usercode, cmapercent, surchargepercent, regionalsurchargeSSpercent, regionalsurchargeSDpercent, btpSingleStoryDiscount, btpDoubleStoryDiscount, btpSingleStoreyCostSiteOther, btpDoubleStoreyCostSiteOther);
        }
    }
}
