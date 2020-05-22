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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ISQSAdminService
    {

        [OperationContract]
        DataSet SQSAdmin_Generic_GetState();

        [OperationContract]
        DataSet SQSAdmin_Generic_GetRegionList(int stateid);

        [OperationContract]
        DataSet SQSAdmin_Generic_GetAllAreas();

        [OperationContract]
        DataSet SQSAdmin_Generic_GetAllGroups();

        [OperationContract]
        DataSet SQSAdmin_Generic_GetGroupsFromArea(int areaid);

        [OperationContract]
        DataSet SQSAdmin_Generic_GetBrandByState(int stateid);

        [OperationContract]
        DataSet SQSAdmin_Generic_GetHomeNameBySearch(int stateid, int brandid, string homename);

        [OperationContract]
        DataSet SQSAdmin_Generic_GetSQSConfiguration(string configCode, string codeValue);

        [OperationContract]
        DataSet SQSAdmin_Generic_GetSpecificationByStateRegionsBrands(int stateid, string regionids, string brandids, DateTime effdate, string systemid, string attachmentid, string homestring);

        [OperationContract]
        DataSet SQSAdmin_Generic_GetSpecificationByStateRegionBrand(int stateid, int regionid, int brandid, DateTime effdate, string systemid, string attachmentid, string homestring);

        [OperationContract]
        void SQSAdmin_Generic_SaveSpecificationByStateRegionBrand(int id, string filename, int stateid, int regionid, int brandid, int homeid, DateTime effdate, int systemformid, int attachmenttypeid, int active);

        [OperationContract]
        DataSet SQSAdmin_Generic_GetAvailabeAreasByHome(int homeid);

        [OperationContract]
        DataSet SQSAdmin_Generic_GetConfiguredAreasByHome(int homeid);

        [OperationContract]
        void SQSAdmin_Generic_AddMinimumAreasToHome(int homeid, string selectedareaid, string usercode);

        [OperationContract]
        void SQSAdmin_Generic_RemoveMinimumAreasFromHome(int homeid, string selectedareaid, string usercode);

        [OperationContract]
        DataSet SQSAdmin_Generic_GetAvailablePAGByState(string stateid, string areaid, string groupid, string keyword, string pagid);

        [OperationContract]
        DataSet SQSAdmin_Generic_GetDisplayHomeByPAGAndState(string stateid, string brandid, string pagid, string displayhomename, string suburb);

        [OperationContract]
        string SQSAdmin_Generic_ReplaceDisplayHomePAG(string existingpagid, string newpagid, string displayhomeid, string usercode, string qty, string changeqty, string changeprice, string allowdesc, string desc, string newextradesc);

        [OperationContract]
        DataSet SQSAdmin_Generic_GetProductUOM();

        [OperationContract]
        DataSet SQSAdmin_Generic_GetProductCategory();

        [OperationContract]
        DataSet SQSAdmin_Generic_GetProductCode();

        [OperationContract]
        DataSet SQSAdmin_Generic_GetProductDisplayCode(string forpromo);

        [OperationContract]
        DataSet SQSAdmin_Generic_GetProductCostCenterCode();

        [OperationContract]
        DataSet SQSAdmin_Generic_ImportProduct(DataTable dt, string usercode);

        [OperationContract]
        DataSet SQSAdmin_Generic_ImportPriceVerticalFormat(DataTable dt, string usercode);

        [OperationContract]
        DataSet SQSAdmin_Generic_ImportPriceHorizontalFormat(DataTable dt, string usercode, string stateid);

        [OperationContract]
        DataSet SQSAdmin_Generic_ImportPriceHorizontalFormatNSW(DataTable dt, string usercode);

        [OperationContract]
        DataSet SQSAdmin_Generic_ImportQuantity(DataTable dt, string usercode);

        [OperationContract]
        DataSet SQSAdmin_Generic_CheckExistingProducts(DataTable dt);

        [OperationContract]
        DataSet SQSAdmin_Generic_ValidatePriceVerticalFormat(DataTable dt, string usercode);

        [OperationContract]
        DataSet SQSAdmin_Generic_ValidatePriceHorizontalFormat(DataTable dt, string usercode, string stateid);

        [OperationContract]
        DataSet SQSAdmin_Generic_ValidatePriceHorizontalFormatNSW(DataTable dt, string usercode);

        [OperationContract]
        DataSet SQSAdmin_Generic_ValidateQuantity(DataTable dt, string usercode);

        [OperationContract]
        DataSet SQSAdmin_Promotion_GetPromotionList(int stateid, int brandid, int storey, int status);

        [OperationContract]
        DataSet SQSAdmin_Promotion_GetPromotionTypeList();

        [OperationContract]
        bool SQSAdmin_Promotion_UpdateMultiplePromotionName(string promotionid, string promotionname);

        [OperationContract]
        bool SQSAdmin_Promotion_CheckMasterPromotionProductExist(int stateid, string productid);

        [OperationContract]
        bool SQSAdmin_Promotion_CheckMultiplePromotionExist(int brandid, int stories, string productid);

        [OperationContract]
        void SQSAdmin_Promotion_SavePromotion(int stateid, int brandid, int storey, int promotiontypeid, string productid, string promotionname, decimal promotioncost, decimal capevalue, int forregional, int active, int displaycodeid);

        [OperationContract]
        DataSet SQSAdmin_Promotion_GetPromotionDetails(int multiplepromotionid);

        [OperationContract]
        DataSet SQSAdmin_Promotion_GetAvailableProductsForPromotion(int multiplepromotionid, int areaid, int groupid, string productstring, string pagid);

        [OperationContract]
        void SQSAdmin_Promotion_AddProductsToPromotion(int multiplepromotionid, string selectedpagid);

        [OperationContract]
        DataSet SQSAdmin_Promotion_GetAreaInPromotion(int multiplepromotionid);

        [OperationContract]
        DataSet SQSAdmin_Promotion_GetGroupInPromotion(int multiplepromotionid);

        [OperationContract]
        DataSet SQSAdmin_Promotion_GetPromotionForProduct(int pagid, string productid);

        [OperationContract]
        DataSet SQSAdmin_Promotion_GetExistingProductsForPromotion(int multiplepromotionid, int areaid, int groupid, string productstring, string pagid);

        [OperationContract]
        DataSet SQSAdmin_Promotion_GetPromotionForCopy(int multiplepromotionid);

        [OperationContract]
        bool SQSAdmin_Promotion_CopyPromotionProductsFromSourceToTarget(int targetpromotionid, int sourcepromotionid);

        [OperationContract]
        void SQSAdmin_Promotion_RemoveProductsFromPromotion(int multiplepromotionid, string selectedpagid);

        [OperationContract]
        void SQSAdmin_Promotion_UpdateProductForPromotion(int multiplepromotionid, int pagid, int defaultselected, decimal discount);

        [OperationContract]
        void SQSAdmin_Promotion_UpdatePromotion(int multiplepromotionid, string baseproduct, decimal promotioncost, decimal capevalue, int forregional, int active, int displaycodeid, string promotionname);

        [OperationContract]
        DataSet SQSAdmin_StudioM_GetSuppliers(int stateid, string suppliername, int active);

        [OperationContract]
        DataSet SQSAdmin_StudioM_GetActiveQuestions(int stateid);

        [OperationContract]
        DataSet SQSAdmin_StudioM_SearchActiveQuestions(int stateid, string searchtext);

        [OperationContract]
        DataSet SQSAdmin_StudioM_GetAnswerForQuestion(int questionid);

        [OperationContract]
        DataSet SQSAdmin_StudioM_GetAvailableGroupListForRetailCluster(int retailclusterid, string groupname);

        [OperationContract]
        DataSet SQSAdmin_StudioM_GetExistingGroupListForRetailCluster(int retailclusterid);

        [OperationContract]
        DataSet SQSAdmin_StudioM_GetRetailCluster(int stateid, string clustername);

        [OperationContract]
        void SQSAdmin_StudioM_UpdateProductStudioMAttribute(string productid, string attribute);

        [OperationContract]
        string SQSAdmin_StudioM_GetProductStudioMAttribute(string productid);

        [OperationContract]
        bool SQSAdmin_StudioM_IsSupplierExists(int stateid, string suppliername);

        [OperationContract]
        bool SQSAdmin_StudioM_IsRetailClusterExists(int stateid, string clustername);

        [OperationContract]
        void SQSAdmin_StudioM_AddEditRetailCluster(int retailclusterid, string clustername, int stateid, int active, string loginuser);

        [OperationContract]
        void SQSAdmin_StudioM_SaveSelectedGroupToRetailCluster(int retailclusterid, string selectedgroupid, string usercode);

        [OperationContract]
        void SQSAdmin_StudioM_RemoveSelectedGroupFromRetailCluster(int retailclusterid, string selectedgroupid);

        [OperationContract]
        void SQSAdmin_StudioM_AddEditSupplier(int supplierid, int stateid, string suppliername, int active);

        [OperationContract]
        DataSet SQSAdmin_StudioM_GetAnswerType();

        [OperationContract]
        DataSet SQSAdmin_StudioM_GetQuestionList(string question, int active, int stateid);

        [OperationContract]
        bool SQSAdmin_StudioM_IsQuestionExists(int answertypeid, string questiontext, int stateid);

        [OperationContract]
        void SQSAdmin_StudioM_AddEditQuestion(int questionid, int answertypeid, string questiontext, int active, int mandatory, int stateid);

        [OperationContract]
        DataSet SQSAdmin_StudioM_GetAnswerList(string questiontext, string answertext, int active, int stateid);

        [OperationContract]
        bool SQSAdmin_StudioM_IsAnswerExists(int questionid, string answertext);

        [OperationContract]
        void SQSAdmin_StudioM_AddEditAnswer(int questionid, int answerid, string answertext, int active);

        [OperationContract]
        DataSet SQSAdmin_StudioM_GetProductPrice(int stateid, int regionid, string productid, string productname, string productdescription, int active, int studiomproduct);

        [OperationContract]
        DataSet SQSAdmin_StudioM_GetProductImages(string productid, int supplierid);

        [OperationContract]
        void SQSAdmin_StudioM_SaveProductImage(string productid, string imagename, int supplierid, byte[] imagestream);

        [OperationContract]
        void SQSAdmin_StudioM_RemoveProductImage(int imageid);

        [OperationContract]
        DataSet SQSAdmin_StudioM_GetProductByCategoryCount(int stateid);

        [OperationContract]
        DataSet SQSAdmin_StandardInclusion_GetStandardInclusionProducts(string productid, string proudctname, int stateid, string brandids);

        [OperationContract]
        DataSet SQSAdmin_StandardInclusion_GetUpgradeOptionByStandardInclusion(string productid, string brandids);

        [OperationContract]
        DataSet SQSAdmin_StandardInclusion_GetAvailableUpgradeOptionProducts(string productid, string proudctname, string brandids, int stateid, string stdinclusionproductid);

        [OperationContract]
        void SQSAdmin_StandardInclusion_RemoveValidationRule(int validationruleid, bool update, bool active);

        [OperationContract]
        void SQSAdmin_StandardInclusion_SaveValidationRule(string standardinclusionproductid, string upgradeoptionproductids, string brandids, string usercode, bool promotion, DateTime effectivedate);

        [OperationContract]
        void SQSAdmin_StudioM_UpdateStudioMAttributeForClusterOrGroup(string id, string xml, string type, string usercode, string supplieridstring, string questionidstring, string answeridstring, string mandatorystring, string stateid);

        [OperationContract]
        DataSet SQSAdmin_StudioM_LoadStudioMAttributeForClusterOrGroup(string id, string type, string stateid);

        [OperationContract]
        DataSet SQSAdmin_StudioM_LoadSQSGroupList(string groupname);

        [OperationContract]
        DataSet SQSAdmin_StandardInclusion_GetPAGFromProduct(string productid);

        [OperationContract]
        DataSet SQSAdmin_StandardInclusion_GetRegionGroupByState(int pstateid);

        [OperationContract]
        DataSet SQSAdmin_StandardInclusion_GetStandardInclusions(int pbrandid, int pregiongroupid, int pagid, int pstateid, string productid, string productname);

        [OperationContract]
        DataSet SQSAdmin_StudioM_GetSupplierBrand(string brandname, int pstateid, int active);

        [OperationContract]
        void SQSAdmin_StudioM_AddEditSupplierBrand(int supplierbrandid, string brandname, int pstateid, int active, string usercode);

        [OperationContract]
        bool SQSAdmin_StudioM_CheckSupplierBrandExists(string brandname, int pstateid);

        [OperationContract]
        bool SQSAdmin_StandardInclusion_IsStandardInclusionExists(int pagid, int pbrandid, int pregiongroupid, int standardinclusionid);

        [OperationContract]
        void SQSAdmin_StandardInclusion_AddEditStandardInclusion(int standardinclusionid, int pagid, int pbrandid, int pregiongroupid, decimal quantity, int active, string usercode);

        [OperationContract]
        void SQSAdmin_StandardInclusion_RemoveStandardInclusion(int standardinclusionid);

        [OperationContract]
        DataSet SQSAdmin_StandardInclusion_GetAvailableStandardInclusionPAGFromMasterList(int areaid, int groupid, string productid, string productname, int stateid);

        [OperationContract]
        DataSet SQSAdmin_StandardInclusion_GetAvailableStandardInclusionPAG(int areaid, int groupid, string productid, string productname, int pbrandid, int pregiongroupid);

        [OperationContract]
        void SQSAdmin_StandardInclusion_SaveStandardInclusionToBrand(string selectedids, string brandids, int regiongroupid, string usercode, DateTime effectivedate);

        [OperationContract]
        DataSet SQSAdmin_HomeMinimumArea_GetSourceHomeByBrand(int brandid, string homename);

        [OperationContract]
        DataSet SQSAdmin_HomeMinimumArea_GetTargetHomeByBrand(int brandid, string homename);

        [OperationContract]
        void SQSAdmin_HomeMinimumArea_CopyMinimumAreasFromSourceHomeToTargetHome(string sourcehomeid, string targethomeidstring, string usercode);

        [OperationContract]
        void SQSAdmin_StudioM_UploadImageToSharepointImageLibrary(string productid, int supplierid);

        [OperationContract]
        DataSet SQSAdmin_RelatedArea_LoadProduct(string productid, string keyword, int stateid);

        [OperationContract]
        DataSet SQSAdmin_RelatedArea_LoadExistingAreasForProduct(string productid, int active);

        [OperationContract]
        DataSet SQSAdmin_RelatedArea_LoadAvailableAreasForProduct(string productid, string keyword, string callfrom);

        [OperationContract]
        void SQSAdmin_RelatedArea_AddSelectedAreasToProduct(string productid, string areaidstring, string usercode, string callfrom);

        [OperationContract]
        void SQSAdmin_RelatedArea_RemooveSelectedAreasFromProduct(string productid, string areaidstring, string callfrom);

        [OperationContract]
        DataSet SQSAdmin_StudioM_GetStudioMProduct(int stateid, string productid, string productname);

        [OperationContract]
        bool SQSAdmin_StudioM_BulkConfigureStudiomQandA(string supplierbrandid, string productidstring, string questionidstring, string answeridstring, string usercode);

        [OperationContract]
        DataSet SQSAdmin_Home_GetHomeList(int stateid, int brandid, string homename, int active);

        [OperationContract]
        DataSet SQSAdmin_Home_GetHomePlanByStateAndBrands(int stateid, string brandids);

        [OperationContract]
        DataSet SQSAdmin_Generic_GetAreas(int stateid);

        [OperationContract]
        DataSet SQSAdmin_Generic_GetGroups(int stateid);

        [OperationContract]
        DataSet SQSAdmin_HomeConfiguration_GetAvailablePAGForHome(int homeid, int areaid, int groupid, string keyword);

        [OperationContract]
        DataSet SQSAdmin_HomeConfiguration_GetExistingPAGForHome(int homeid, int areaid, int groupid, string keyword);

        [OperationContract]
        bool SQSAdmin_HomeConfiguration_AddProductToHome(int homeid, string selectedpagid, string qtystring, string changeqtystring, string changepricestring, string extradescstring, string usercode);

        [OperationContract]
        bool SQSAdmin_HomeConfiguration_UpdatePagForHome(int homeid, int pagid, string quantity, int changeqty, int changeprice, int extradesc, string usercode);

        [OperationContract]
        bool SQSAdmin_HomeConfiguration_RemoveProductsFromHome(int homeid, string selectedpagids, string usercode);

        [OperationContract]
        DataSet SQSAdmin_HomeConfiguration_GetProducts(int stateid, string productid, string keyword);

        [OperationContract]
        string SQSAdmin_HomeConfiguration_CopyProductConfiguration(string sourceproductid, string targetproductid, string usercode);

        [OperationContract]
        DataSet SQSAdmin_QuantityManagement_GetHomeModelQuantity(string stateid, string areaid, string groupid, string brandid);

        [OperationContract]
        DataSet SQSAdmin_QuantityManagement_GetHomeFacadeQuantity(string stateid, string areaid, string groupid, string brandid);

        [OperationContract]
        bool SQSAdmin_QuantityManagement_UpdateQuantityForAreaGroupAtHomeModel(string stateid, string areaid, string groupid, string brandid, string homemodel, string quantity, string usercode);

        [OperationContract]
        bool SQSAdmin_QuantityManagement_UpdateQuantityForAreaGroupAtHomeFacade(string areaid, string groupid, string homeid, string quantity, string usercode);

        [OperationContract]
        bool SQSAdmin_QuantityManagement_RefreshQuantity(string stateid, string areaid, string groupid, string usercode);

        [OperationContract]
        bool SQSAdmin_HomeConfiguration_BulkUpdateQuantity(string homeid, string pagidstring, string quantity, string usercode);

        [OperationContract]
        bool SQSAdmin_HomeConfiguration_BulkUpdateHomeModelQuantity(string brandid, string areaid, string groupid, string homestring, string quantity, string usercode);

        [OperationContract]
        DataSet SQSAdmin_SearchDisclaimers(int typeid, int stateid, int regionid, int brandid);

        [OperationContract]
        string SQSAdmin_SearchDisclaimer(string type, string state, int regionid, int brandid, string brandname, DateTime depositDate);

        [OperationContract]
        DataSet SQSAdmin_Generic_BasePriceHoldDays(string stateid, string regionids, string brandids, DateTime effectivedate, string active);

        [OperationContract]
        void SQSAdmin_Generic_UpdateBasePriceHoldDays(string id, string daysfrom, string daysto, DateTime effectivedate, string active, string depositamount, string usercode, decimal cmapercent, decimal surchargepercent, decimal btpsinglestorey, decimal btpdoublestorey, decimal btpSingleStoryDiscount, decimal btpDoubleStoryDiscount, decimal btpSingleStoreyCostSiteOther, decimal btpDoubleStoreyCostSiteOther);

        [OperationContract]
        void SQSAdmin_Generic_NeweBasePriceHoldDays(int stateid, string regionids, string brandids, string daysfrom, string daysto, DateTime effectivedate, string active, string depositamount, string usercode, string cmapercent, string surchargepercent, string regionalsurchargeSSpercent, string regionalsurchargeSDpercent, string btpSingleStoryDiscount, string btpDoubleStoryDiscount, string btpSingleStoreyCostSiteOther, string btpDoubleStoreyCostSiteOther);

        [OperationContract]
        string SQSAdmin_SaveDisclaimer(string type, string state, string regionids, string brandids, string disclaimerText, string internalNotes, string effectiveDate, int status, string usercode);
    }



}
