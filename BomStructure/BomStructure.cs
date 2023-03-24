using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BomStructure
{
   
    public class BOM_HEADER
    {
        
        [Description("type:'integer', title:'Object ID Primary Key BOM ID || Colorway ID',default: 0, examples: [ 14924645924633 ]")]
        public  long  OBJECT_ID{get;set;}
        
        [Description("type:'integer', title:'Unique identifier for the BOM',default: 0, examples: [ 1492464 ]")]
        public int   BOM_ID{get;set;}
       
        [Description("type:'string', title:'Season of the BOM',default: '', examples: [ 'FA2018' ]")]
        public string   CYCLE_YEAR{get;set;}
        
        [Description("type:'string', title:'Season of the BOM',default: '', examples: [ 'FA' ]")]
        public string   SEASON_CD{get;set;}
      
        [Description("type:'integer', title:'Year of the BOM',default: 0, examples: [ 2018 ]")]
        public int   SEASON_YR{get;set;}
     
        [Description("type:'string', title:'Factory responsible for Development of Style',default: '', examples: [ 'NIG' ]")]
        public string   FACTORY{get;set;}
        
        [Description("type:'string', title:'Style Number',default: '', examples: [ 'CI8616' ]")]
        public string   STYLE_NBR{get;set;}
      
        [Description("type:'integer', title:'Seasonal Style Name',default: '', examples: [ 'PCC_UAT_TEST_STYLE_43' ]")]
        public string   STYLE_NM{get;set;}
      
        [Description("type:'integer', title:'MMX Product ID for Style',default: 0, examples: [ 3714838 ]")]
        public int   PRODUCT_ID{get;set;}
     
        [Description("type:'string', title:'Colorway code associated to Style Colorway',default: 'Colorway code associated to Style Colorway', examples: [ '043' ]")]
        public string   COLORWAY_CD{get;set;}
   
        [Description("type:'integer', title:'Unique identifier of the Style Colorway',default: 0, examples: [ 5924633 ]")]
        public int   COLORWAY_ID{get;set;}
       
        [Description("type:'string', title:'Plug Colorway Code from MMX (Placeholder)',default: '', examples: [ 'PR7' ]")]
        public string   PLUG_COLORWAY_CD{get;set;}
     
        [Description("type:'string', title:'Status of the BOM',default: '', examples: [ 'T1' ]")]
        public string   BOM_STATUS{get;set;}
       
        [Description("type:'string', title:'Timestamp of the last update to the BOM',default: '', examples: [ '8/1/18 1:35' ]")]
        public string   BOM_UPDATE_TIMESTAMP{get;set;}
      
        [Description("type:'string', title:'System User ID who last updated the BOM',default: '', examples: [ 'MLOW2' ]")]
        public string BOM_UPDATE_USERID { get; set; }
    }

    public class BOM_HEADER_AUXILIARY
    {
      

        [Description("type:'string', title:'Parent Supplier of assigned Factory',default: '', examples: [ 'NSK' ]")]
        public string PARENT_FCTY{get;set;}

        [Description("type:'string', title:'Primary Development Region of the Season Style',default: '', examples: [ '01' ]")]
        public string PRIM_DEV_REG_CODE{get;set;}
       
        [Description("type:'string', title:'Primary Development Region Abbreviation',default: '', examples: [ 'USA' ]")]
        public string PRIM_DEV_REG_ABRV{get;set;}

        [Description("type:'string', title:'Design Region of the Style',default: '', examples: [ '01' ]")]
        public string DESIGN_REG_CODE{get;set;}
   
        [Description("type:'string', title:'Design Region Abbreviation',default: '', examples: [ 'USA' ]")]
        public string DESIGN_REG_ABRV{get;set;}
     
        [Description("type:'integer', title:'Marketing Structure ID unique to the MSC Combination',default: 0, examples: [ 5483 ]")]
        public int MSC_IDENTIFIER{get;set;}

        
        [Description("type:'string', title:'The Marketing Structure Code assigned by the Primary Development Region',default: '', examples: [ 'WGSG' ]")]
        public string MSC_CODE{get;set;}

     
        [Description("type:'string', title:'Marketing Structure Description Level 1',default: '', examples: [ 'WOMEN'S' ]")]
        public string MSC_LEVEL_1{get;set;}

 
        [Description("type:'string', title:'Marketing Structure Description Level 2',default: '', examples: [ 'GOLF' ]")]
        public string MSC_LEVEL_2{get;set;}
      
        [Description("type:'string', title:'Marketing Structure Description Level 3',default: '', examples: [ 'SPORT GOLF' ]")]
        public string MSC_LEVEL_3{get;set;}

      
        [Description("type:'integer', title:'Silhouette Code assigned to the Style',default: 0, examples: [ 80 ]")]
        public int SILHOUETTE_CODE{get;set;}

        [Description("type:'string', title:'Silhouette description of the Style',default: '', examples: [ 'GLOVES' ]")]
        public string SILHOUETTE_DESC{get;set;}

        [Description("type:'string', title:'Addendum Color from MMX',default: '', examples: [ '' ]")]
        public string ADDENDUM{get;set;}

        [Description("type:'string', title:'The First and Last Name of the Product Developer assigned to the Style',default: '', examples: [  'LOW, MAY' ]")]
        public string DEVELOPER{get;set;}

        [Description("type:'string', title:'The system User ID of the Product Developer assigned to the Style',default: '', examples: [  'MLOW2' ]")]
        public  string DEVELOPER_USER_ID{get;set;}

        [Description("type:'integer', title:'Unique identifier for the Primary Color in the Colorway',default: 0, examples: [  1 ]")]
        public  int PRMRY_COLOR_ID{get;set;}

        [Description("type:'string', title:'Code of the Primary Color in the Colorway',default: '', examples: [ '00A' ]")]
        public  string PRMRY_COLOR_CD{get;set;}

        [Description("type:'string', title:'Abbreviation of the Primary Color in the Colorway',default: '', examples: [ 'BLACK' ]")]
        public  string PRMRY_ABRV{get;set;}

        [Description("type:'string', title:'Description of the Primary Color in the Colorway',default: '', examples: [ 'BLACK' ]")]
        public  string PRMRY_DESC{get;set;}

        [Description("type:'string', title:'Unique identifier for the Secondary Color in the Colorway',default: '', examples: [ '' ]")]
        public  string SCNDY_COLOR_ID{get;set;}

        [Description("type:'string', title:'Code of the Secondary Color in the Colorway',default: '', examples: [ '' ]")]
        public  string SCNDY_COLOR_CD{get;set;}

        [Description("type:'string', title:'Abbreviation of the Secondary Color in the Colorway',default: '', examples: [ '' ]")]
        public  string SCNDY_ABRV{get;set;}

        [Description("type:'string', title:'Description of the Secondary Color in the Colorway',default: '', examples: [ '' ]")]
        public  string SCNDY_DESC{get;set;}

        [Description("type:'string', title:'Unique identifier for the Tertiary Color in the Colorway',default: '', examples: [ '' ]")]
        public  string TRTRY_COLOR_ID{get;set;}

        [Description("type:'string', title:'Code assigned to the Tertiary Color in the Colorway',default: '', examples: [ '' ]")]
        public  string TRTRY_COLOR_CD{get;set;}

        [Description("type:'string', title:'Abbreviation assigned to the Tertiary Color in the Colorway',default: '', examples: [ '' ]")]
        public  string TRTRY_ABRV{get;set;}

        [Description("type:'string', title:'Description of the Tertiary Color in the Colorway',default: '', examples: [ '' ]")]
        public  string TRTRY_DESC{get;set;}

        [Description("type:'string', title:'Unique identifier for the Logo Color in the Colorway',default: '', examples: [ '' ]")]
        public  string LOGO_COLOR_ID{get;set;}

        [Description("type:'string', title:'Code of the Logo Color in the Colorway',default: '', examples: [ '' ]")]
        public  string LOGO_COLOR_CD{get;set;}

        [Description("type:'string', title:'Abbreviation of the Logo Color in the Colorway',default: '', examples: [ '' ]")]
        public  string LOGO_ABRV{get;set;}

        [Description("type:'string', title:'Description of the Logo Color in the Colorway',default: '', examples: [ '' ]")]
        public  string LOGO_DESC{get;set;}

    }

    public class BOM_LINE_ITEMS
    {

        [Description("type:'integer', title:'Row Number of Display on the BOM',default: 0, examples: [ 1 ]")]
        public  int  BOM_ROW_NBR{get;set;}

        [Description("type:'integer', title:'Unique ID associated to the BOM Item',default: 0, examples: [ 44910016 ]")]
        public int BOM_ITM_ID {get;set;}

        [Description("type:'integer', title:'Item Number of the Material',default: 0, examples: [ 342169 ]")]
        public int ITEM_NBR {get;set;}

        [Description("type:'integer', title:'The PCX Supplied Material Identifier',default: 0, examples: [ 0 ]")]
        public int PCX_SUPPLIED_MATL_ID {get;set;}

        [Description("type:'string', title:'Item Type of the Material',default: '', examples: [ 'I' ]")]
        public  string  IT{get;set;}

        [Description("type:'string', title:'Item Status of the Material',default: '', examples: [ 'A' ]")]
        public  string  IS{get;set;}

        [Description("type:'string', title:'Item Type Level 1 of the Material',default: '', examples: [ 'STATEMENT' ]")]
        public  string  ITEM_TYPE_1{get;set;}

        [Description("type:'string', title:'Item Type Level 2 of the Material',default: '', examples: [ '' ]")]
        public  string  ITEM_TYPE_2{get;set;}

        [Description("type:'string', title:'Item Type Level 3 of the Material',default: '', examples: [ '' ]")]
        public  string  ITEM_TYPE_3{get;set;}

        [Description("type:'string', title:'Item Type Level 4 of the Material',default: '', examples: [ '' ]")]
        public  string  ITEM_TYPE_4{get;set;}

        [Description("type:'integer', title:'Unique Identifier of the Vendor of the Item',default: 0, examples: [ 0 ]")]
        public  int  VEND_ID{get;set;}

        [Description("type:'string', title:'Code of the Vendor of the Item',default: '', examples: [ 'CONT' ]")]
        public  string  VEND_CD{get;set;}

        [Description("type:'string', title:'Name of the Vendor of the Item',default: '', examples: [ 'CONTRACTOR' ]")]
        public  string  VEND_NM{get;set;}

        [Description("type:'string', title:'he Liaison Office or PCC assigned to the Factory',default: '', examples: [ 'XX' ]")]
        public  string  VEND_LO{get;set;}

        [Description("type:'integer', title:'Display Sort order of the Components for the BOM Line Item',default: 0, examples: [ 0 ]")]
        public  int  COMPONENT_ORD{get;set;}

        [Description("type:'integer', title:'Unique Identifier of the Component of the Bom Line Item',default: 0, examples: [ 44474125 ]")]
        public int BOM_COMPONENT_ID {get;set;}

        [Description("type:'string', title:'Short Description of how the item is being applied on the Style',default: '', examples: [ '' ]")]
        public  string  USE{get;set;}

        [Description("type:'string', title:'Description of how the item is being applied on the Style',default: '', examples: [ 'PM#342158__ STATEMENT__ THIS IS A PROMO STYLE.__' ]")]
        public  string  DESCRIPTION{get;set;}

        [Description("type:'string', title:'Unit of Measure for the Item',default: '', examples: [ 'YDS' ]")]
        public  string  UOM{get;set;}

        [Description("type:'string', title:'Quantity of the Item used for the BOM Line Item',default: '', examples: [ '' ]")]
        public  string  QTY{get;set;}

        [Description("type:'string', title:'Timestamp of when the item was added to the BOM',default: '', examples: [ '8/1/18 1:27' ]")]
        public  string  BOM_ITM_SETUP_TIMESTAMP{get;set;}

        [Description("type:'string', title:'Timestamp of the last update to the BOM',default: '', examples: [ '8/1/18 1:27' ]")]
        public  string  BOM_ITM_UPDATE_TIMESTAMP{get;set;}

        [Description("type:'string', title:'Unique Identifier of a color associated to the component of the Bom Line Item',default: '', examples: [ '' ]")]
        public  string  BOM_CMPCLR_ID{get;set;}

        [Description("type:'string', title:'Display sort order of the Color associated to the component of the Bom Line Item',default: '', examples: [ '' ]")]
        public  string  CMPCOLR_ORD_NBR{get;set;}

        [Description("type:'string', title:'Unique Identifier of the color assinged to the Item for the Colorway',default: '', examples: [ '' ]")]
        public  string  ITEM_COLOR_ID{get;set;}

        [Description("type:'string', title:'Code of the color assigned to the Item for the Colorway',default: '', examples: [ '' ]")]
        public  string  ITEM_COLOR_CD{get;set;}

        [Description("type:'string', title:'Abbreviation of the color assigned to the Item for the Colorway',default: '', examples: [ '' ]")]
        public  string  ITEM_COLOR_ABRV{get;set;}

        [Description("type:'string', title:'Name of the color assigned to the Item for the Colorway',default: '', examples: [ '' ]")]
        public  string  ITEM_COLOR_NM{get;set;}

        [Description("type:'string', title:'Number of the Graphic Colorway',default: '', examples: [ '' ]")]
        public  string  GCW{get;set;}

        [Description("type:'string', title:'Unique Identifier of the Graphic Color in the Graphic Colorway',default: '', examples: [ '' ]")]
        public  string  GCW_CMPCOLR_ID{get;set;}

        [Description("type:'string', title:'Display sort order of the Graphic Colorway Component',default: '', examples: [ '' ]")]
        public  string  ART_ITEM_ORD_NBR{get;set;}

        [Description("type:'string', title:'Description of the Graphic Colorway Component',default: '', examples: [ '' ]")]
        public  string  GCW_ART_DESCRIPTION{get;set;}
    }
}
