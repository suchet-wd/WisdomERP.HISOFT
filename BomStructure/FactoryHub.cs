using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryHub
{
    public  class FactoryHubXML
    {

        string header1 = "http://schemas.xmlsoap.org/soap/envelope/";
        string header2 = "http://tempuri.org/";
        string header3 = "http://schemas.datacontract.org/2004/07/OLLIeOrders";

        public  string OrderDataGetXMLRequest(string BuyGroup, string FactoryCode, string GACDateBegin, string GACDateEnd, string OrderDocDateBegin, string OrderDocDateEnd, string LastWhqDownloadDate = "", string Material = "", string MaxOrderItemsReturned = "", string OrderItemNumber = "", string OrderNumber = "", string Plant = "", string ShipTo = "") {


            StringBuilder  XMLRet =new StringBuilder() ;
       
            string header = "<soapenv:Envelope xmlns:soapenv=" + (char)34 + header1 + (char)34 + " xmlns:tem=" + (char)34 + header2 + (char)34 + " xmlns:oll=" + (char)34 + header3 + (char)34 + ">";

            XMLRet.AppendLine(header);
            XMLRet.AppendLine("<soapenv:Header/>");
            XMLRet.AppendLine(" <soapenv:Body>");
            XMLRet.AppendLine(" <tem:OrdersDataGet>");
            // < !--Optional:-->
            XMLRet.AppendLine(" <tem:input>");

            //< !--Optional:-->
            if (BuyGroup != "") {
                XMLRet.AppendLine(" <oll:BuyGroup>" + BuyGroup + "</oll:BuyGroup>");
            };
          

            //< !--Optional:-->
            if (FactoryCode != "") {
                XMLRet.AppendLine(" <oll:FactoryCode>" + FactoryCode + "</oll:FactoryCode>");
            };
         
            //< !--Optional:-->
            if (GACDateBegin != "") {
                XMLRet.AppendLine(" <oll:GACDateBegin>" + GACDateBegin + "</oll:GACDateBegin>");
            };
          
            // < !--Optional:-->
            if (GACDateEnd != "") { XMLRet.AppendLine(" <oll:GACDateEnd>" + GACDateEnd + "</oll:GACDateEnd>"); };
                 

            //< !--Optional:-->
            if (OrderDocDateBegin != "") {
                XMLRet.AppendLine("  <oll:OrderDocDateBegin>" + OrderDocDateBegin + "</oll:OrderDocDateBegin>");
            };
           
            // < !--Optional:-->
            if (OrderDocDateEnd != "") {
                XMLRet.AppendLine("  <oll:OrderDocDateEnd>" + OrderDocDateEnd + "</oll:OrderDocDateEnd>");
            };
           
            //< !--Optional:-->
            if (LastWhqDownloadDate != "") {
                XMLRet.AppendLine("  <oll:LastWhqDownloadDate>" + LastWhqDownloadDate + "</oll:LastWhqDownloadDate>");
            };
            
            // < !--Optional:-->
            if (Material != "") {
                XMLRet.AppendLine("  <oll:Material>" + Material + "</oll:Material>");
            };
            
            // < !--Optional:-->
            if (MaxOrderItemsReturned != "") {
                XMLRet.AppendLine("  <oll:MaxOrderItemsReturned>" + MaxOrderItemsReturned + "</oll:MaxOrderItemsReturned>");
            };
            
            // < !--Optional:-->
            if (OrderItemNumber != "") {
                XMLRet.AppendLine("  <oll:OrderItemNumber>" + OrderItemNumber + "</oll:OrderItemNumber>");
            };
            
            // < !--Optional:-->
            if (OrderNumber != "") {
                XMLRet.AppendLine("  <oll:OrderNumber>" + OrderNumber + "</oll:OrderNumber>");
            };
            
            //< !--Optional:-->
            if (Plant != "") {
                XMLRet.AppendLine("  <oll:Plant>" + Plant + "</oll:Plant>");
            };
            
            //< !--Optional:-->
            if (ShipTo != "") {
                XMLRet.AppendLine("  <oll:ShipTo>" + ShipTo + "</oll:ShipTo>");
            };
            
                            
            XMLRet.AppendLine(" </tem:input>");
            XMLRet.AppendLine(" </tem:OrdersDataGet>");
            XMLRet.AppendLine(" </soapenv:Body>");
            XMLRet.AppendLine("</soapenv:Envelope>");

            return XMLRet.ToString() ;

        }


        public enum FSHSetFlags : int
        {
            Order_Flag_Y = 0,
            Order_Flag_N_and_Order_Item_Flag_Y = 1,
            Order_Flag_N_and_Order_Item_Flag_N = 2
        }
        public string OrdersDataGetSetFlagsXMLRequest(string FactoryCode, string OrderNumber ,string OrderItemNumber, FSHSetFlags SetFlags)
        {


            //1.If Order Flag = Y:
            //      a.Update Order Number New Flag = Y
            //      b.Update Order Item New Flag = Y
            //      c.Update Order Item Acknowledge Flag = N
            //2.If Order Flag = N and Order Item Flag = Y
            //      a.Update Order Number New Flag = N
            //      b.Update Order Item New Flag = Y
            //      c.Update Order Item Acknowledge Flag = N
            //3.If Order Flag = N and Order Item Flag = N
            //      a.Update Order Number New Flag = N
            //      b.Update Order Item New Flag = N
            //      c.Update Order Item Acknowledge Flag = (Input parameter)

            string OrderNumberNewFlag = "";
            string OrderItemNewFlag = "";
                string OrderItemAcknowledgedFlag ="";

            switch (SetFlags) {
                case FSHSetFlags.Order_Flag_Y:
                    OrderNumberNewFlag = "Y";
                    OrderItemNewFlag = "Y";
                    OrderItemAcknowledgedFlag = "N";
                    break;
                case FSHSetFlags.Order_Flag_N_and_Order_Item_Flag_Y:
                    OrderNumberNewFlag = "N";
                    OrderItemNewFlag = "Y";
                    OrderItemAcknowledgedFlag = "N";

                    break;
                case FSHSetFlags.Order_Flag_N_and_Order_Item_Flag_N:
                    OrderNumberNewFlag = "N";
                    OrderItemNewFlag = "N";
                    OrderItemAcknowledgedFlag = "N";
                    break;
                default:
                    OrderNumberNewFlag = "Y";
                    OrderItemNewFlag = "Y";
                    OrderItemAcknowledgedFlag = "N";
                    break; 


            }

            StringBuilder XMLRet = new StringBuilder();

            string header = "<soapenv:Envelope xmlns:soapenv=" + (char)34 + header1 + (char)34 + " xmlns:tem=" + (char)34 + header2 + (char)34 + " xmlns:oll=" + (char)34 + header3 + (char)34 + ">";

            XMLRet.Append(header);
            XMLRet.Append("<soapenv:Header/>");
            XMLRet.Append(" <soapenv:Body>");
            XMLRet.Append(" <tem:OrdersDataGetSetFlags>");
            // < !--Optional:-->
            XMLRet.Append(" <tem:input>");

            XMLRet.Append(" <oll:FactoryCode>" + FactoryCode + "</oll:FactoryCode>");
            XMLRet.Append("  <oll:OrderNumber>" + OrderNumber + "</oll:OrderNumber>");
            XMLRet.Append("  <oll:OrderItemNumber>" + OrderItemNumber + "</oll:OrderItemNumber>");
            XMLRet.Append("  <oll:OrderNumberNewFlag>" + OrderNumberNewFlag + "</oll:OrderNumberNewFlag>");
            XMLRet.Append("  <oll:OrderItemNewFlag>" + OrderItemNewFlag + "</oll:OrderItemNewFlag>");
            XMLRet.Append("  <oll:OrderItemAcknowledgedFlag>" + OrderItemAcknowledgedFlag + "</oll:OrderItemAcknowledgedFlag>");

            XMLRet.Append(" </tem:input>");
            XMLRet.Append(" </tem:OrdersDataGetSetFlags>");
            XMLRet.Append(" </soapenv:Body>");
            XMLRet.Append("</soapenv:Envelope>");

            return XMLRet.ToString();

        }

        public string OrdersDataGetUnAcknowledgedXMLRequest(string FactoryCode, string MaxOrderItemsReturned)
        {

            StringBuilder XMLRet = new StringBuilder();

            string header = "<soapenv:Envelope xmlns:soapenv=" + (char)34 + header1 + (char)34 + " xmlns:tem=" + (char)34 + header2 + (char)34 + " xmlns:oll=" + (char)34 + header3 + (char)34 + ">";

            XMLRet.Append(header);
            XMLRet.AppendLine("<soapenv:Header/>");
            XMLRet.AppendLine(" <soapenv:Body>");
            XMLRet.AppendLine(" <tem:OrdersDataGetUnAcknowledged>");
            // < !--Optional:-->
            XMLRet.AppendLine(" <tem:input>");

            XMLRet.AppendLine(" <oll:FactoryCode>" + FactoryCode + "</oll:FactoryCode>");
            XMLRet.AppendLine("  <oll:MaxOrderItemsReturned>" + MaxOrderItemsReturned + "</oll:MaxOrderItemsReturned>");
          
            XMLRet.AppendLine(" </tem:input>");
            XMLRet.AppendLine(" </tem:OrdersDataGetUnAcknowledged>");
            XMLRet.AppendLine(" </soapenv:Body>");
            XMLRet.AppendLine("</soapenv:Envelope>");

            return XMLRet.ToString();

        }

        public string OrdersEventSubmitXMLRequest(string FactoryCode, string OrderNumber,List<EventDetails>  EvtDetails )
        {

            StringBuilder XMLRet = new StringBuilder();

            string header = "<soapenv:Envelope xmlns:soapenv=" + (char)34 + header1 + (char)34 + " xmlns:tem=" + (char)34 + header2 + (char)34 + " xmlns:oll=" + (char)34 + header3 + (char)34 + ">";

            XMLRet.Append(header);
            XMLRet.Append("<soapenv:Header/>");
            XMLRet.Append(" <soapenv:Body>");
            XMLRet.Append(" <tem:OrdersEventSubmit>");
            // < !--Optional:-->
            XMLRet.Append(" <tem:input>");

            XMLRet.Append(" <oll:FactoryCode>" + FactoryCode + "</oll:FactoryCode>");
            XMLRet.Append("  <oll:OrderNumber>" + OrderNumber + "</oll:OrderNumber>");

            XMLRet.Append("     <oll:OrderEventDetails>");
            //  <!--Zero or more repetitions:-->
            if (EvtDetails.Count > 0) {

                for (int i = 0; i <= EvtDetails.Count - 1; i++) {
                    XMLRet.Append("     <oll:EventDetails>");

                    XMLRet.Append("         <oll:OrderItem>" + EvtDetails[i].OrderItem  + "</oll:OrderItem>");
                    XMLRet.Append("         <oll:OrderEventCd>" + EvtDetails[i].OrderEventCd + "</oll:OrderEventCd>");
                    XMLRet.Append("         <oll:EventDate>" + EvtDetails[i].EventDate + "</oll:EventDate>");
                    // < !--Optional:-->
                    XMLRet.Append("         <oll:EventValue1>" + EvtDetails[i].EventValue1 + "</oll:EventValue1>");
                    // < !--Optional:-->
                    XMLRet.Append("         <oll:EventValue2>" + EvtDetails[i].EventValue2 + "</oll:EventValue2>");
                    // < !--Optional:-->
                    XMLRet.Append("         <oll:EventValue3>" + EvtDetails[i].EventValue3 + "</oll:EventValue3>");

                    XMLRet.Append("     </oll:EventDetails>");

                };
                
            };

            XMLRet.Append("     </oll:OrderEventDetails>");

            XMLRet.Append(" </tem:input>");
            XMLRet.Append(" </tem:OrdersEventSubmit>");
            XMLRet.Append(" </soapenv:Body>");
            XMLRet.Append("</soapenv:Envelope>");

            return XMLRet.ToString();

        }

        public string OrdersGetPDFXMLRequest(string FactoryCode, string OrderNumber)
        {

            StringBuilder XMLRet = new StringBuilder();

            string header = "<soapenv:Envelope xmlns:soapenv=" + (char)34 + header1 + (char)34 + " xmlns:tem=" + (char)34 + header2 + (char)34 + " xmlns:oll=" + (char)34 + header3 + (char)34 + ">";

            XMLRet.Append(header);
            XMLRet.Append("<soapenv:Header/>");
            XMLRet.Append(" <soapenv:Body>");
            XMLRet.Append(" <tem:OrdersGetPDF>");
            // < !--Optional:-->
            XMLRet.Append(" <tem:input>");

            XMLRet.Append(" <oll:FactoryCode>" + FactoryCode + "</oll:FactoryCode>");
            XMLRet.Append("  <oll:OrderNumber>" + OrderNumber + "</oll:OrderNumber>");

            XMLRet.Append(" </tem:input>");
            XMLRet.Append(" </tem:OrdersGetPDF>");
            XMLRet.Append(" </soapenv:Body>");
            XMLRet.Append("</soapenv:Envelope>");

            return XMLRet.ToString();

        }

    }

  

    public class EventDetails
    {
        public string OrderItem { get; set; }
        public string OrderEventCd { get; set; }
        public string EventDate { get; set; }
        public string EventValue1 { get; set; }
        public string EventValue2 { get; set; }
        public string EventValue3 { get; set; }

    }

}
