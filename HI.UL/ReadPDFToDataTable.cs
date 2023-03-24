using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Collections;
using System.Data;
using System.Diagnostics;
using Bytescout.PDFExtractor;
using System.IO;
using SautinSoft;

namespace HI.UL
{
     public static class ReadPDFToDataTable
    {

         public static List<DataTable> BytescoutReadPdfFile(string InputFile, String UserNameLogin)
         {
            List<DataTable> PDFToData = new List<DataTable>();

            CSVExtractor extractor = new CSVExtractor();
            extractor.RegistrationName = "demo";
            extractor.RegistrationKey = "demo";
           // extractor.TextEncodingCodePage = 65001;
            // Load sample PDF document
            extractor.LoadDocumentFromFile(InputFile);
            //extractor.CSVSeparatorSymbol = "," // you can change CSV separator symbol (if needed) from "," symbol to another if needed for non-US locales

            extractor.DetectNewColumnBySpacesRatio = 1;
            extractor.PreserveFormattingOnTextExtraction = true;
            extractor.TrimSpaces = true;
            extractor.AutoAlignColumnsToHeader = true;
            extractor.OCRMode = OCRMode.Off;
            extractor.OCRResolution = 300;
            //extractor.ColumnDetectionMode = ColumnDetectionMode.ContentGroupsAndBorders;
            extractor.LineGroupingMode = LineGroupingMode.None;

             DataTable _dttemp =null;
            //Get page count
            int pageCount = extractor.GetPageCount();
            String _PathSaveFileName  = Path.GetDirectoryName(InputFile);
            int _TotalPage  = pageCount;
            String fileName="";
     
            for (int i =0;i <= pageCount-1;i++){
                         
                        fileName  = _PathSaveFileName  + "\\" + UserNameLogin + "page" + (i + 1).ToString() + ".csv";
                      
                         // Save extracted page text to file  
          
                         extractor.SavePageCSVToFile(i, fileName);
                        
                         _dttemp = HI.UL.ReadExcel.PDFConvertRead(fileName, UserNameLogin + "page" + (i + 1).ToString(), -1);

                         if (_dttemp != null) {
                             PDFToData.Add(_dttemp.Copy());     
                         }

                         try { System.IO.File.Delete(fileName); }
                         catch { }

             };
           
            return PDFToData;

         }

         public static List<DataTable> SautinReadPdfFile(string InputFile, String UserNameLogin)
         {
             List<DataTable> PDFToData = new List<DataTable>();

             
            SautinSoft.PdfFocus _Sautin = new SautinSoft.PdfFocus();
            _Sautin.OpenPdf(InputFile);


    
             DataTable _dttemp = null;
             //Get page count
             int pageCount = _Sautin.PageCount;
             String _PathSaveFileName = Path.GetDirectoryName(InputFile);
             int _TotalPage = pageCount;
             String fileName = "";

             _Sautin.ExcelOptions.ConvertNonTabularDataToSpreadsheet = true;
             _Sautin.ExcelOptions.SpacingBetweenTables = 1;

             for (int i = 0; i <= pageCount - 1; i++)
             {

                 fileName = _PathSaveFileName + "\\" + UserNameLogin + "page" + (i + 1).ToString() + ".xls";
                
                 // Save extracted page text to file  
                 _Sautin.ToExcel(fileName, i, i);
               
                 _dttemp = HI.UL.ReadExcel.PDFConvertRead(fileName, UserNameLogin + "page" + (i + 1).ToString(),0);

                 if (_dttemp != null)
                 {
                     PDFToData.Add(_dttemp.Copy());
                 }

                 try { System.IO.File.Delete(fileName); }
                 catch { }

             };

             return PDFToData;

         }

    }

}
