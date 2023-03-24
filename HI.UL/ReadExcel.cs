using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Collections;
using System.Data;
using System.Diagnostics;

namespace HI.UL
{
    public static class ReadExcel
    {

        public static DataTable Read(string InputFile, string SheetName, int SelectSheetind =-1)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US", true);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US", true);
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss";

            DataTable dt = null;
            int Sheetind = -1;
            DevExpress.XtraSpreadsheet.SpreadsheetControl _SpreadsheetControl = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
            DevExpress.Spreadsheet.Worksheet worksheet = null;
            //  _SpreadsheetControl.LoadDocument(  InputFile);

                switch (System.IO.Path.GetExtension(InputFile).ToLower()) {
                case ".xls":
                    _SpreadsheetControl.LoadDocument(System.IO.File.ReadAllBytes(InputFile), DevExpress.Spreadsheet.DocumentFormat.Xls);
                    break;
                case ".xlsx":
                    _SpreadsheetControl.LoadDocument(System.IO.File.ReadAllBytes(InputFile), DevExpress.Spreadsheet.DocumentFormat.Xlsx);
                    break;
                case ".xlsm":
                    _SpreadsheetControl.LoadDocument(System.IO.File.ReadAllBytes(InputFile), DevExpress.Spreadsheet.DocumentFormat.Xlsm);
                    break;
                default:
                    _SpreadsheetControl.LoadDocument(System.IO.File.ReadAllBytes(InputFile), DevExpress.Spreadsheet.DocumentFormat.Xlsx);
                    break;
            }
           
          
            string ColName = "";

            if (SelectSheetind == -1)
            {
                foreach (DevExpress.Spreadsheet.Worksheet wk in _SpreadsheetControl.Document.Worksheets)
                {
                    if (wk.Name.ToUpper() == SheetName.ToUpper())
                    {
                        Sheetind = wk.Index;
                        worksheet = wk;
                        break;
                    }
                }
            }
            else
            {
                int I = -1;
                foreach (DevExpress.Spreadsheet.Worksheet wk in _SpreadsheetControl.Document.Worksheets)
                {
                    if (I == -1)
                    {
                        I = 0;
                    }

                    if (I == SelectSheetind)
                    {
                        Sheetind = wk.Index;
                        worksheet = wk;
                        break;
                    }
                    I = I + 1;

                }
            };

            if (Sheetind >= 0)
            {
                dt = new DataTable();

                for (int c = 0; c <= worksheet.GetUsedRange().ColumnCount - 1; c++)
                {
                    try
                    {
                        ColName = "F" + (c + 1).ToString();
                    }
                    catch 
                    {
                        ColName = c.ToString();
                    }

                    dt.Columns.Add(ColName, typeof(string));

                }

                for (int r = 0; r <= worksheet.GetUsedRange().RowCount - 1; r++)
                {
                    DataRow Rx = dt.NewRow();
                    for (int c = 0; c <= worksheet.GetUsedRange().ColumnCount - 1; c++)
                    {

                        if (worksheet.Cells[r, c].Value.Type == DevExpress.Spreadsheet.CellValueType.DateTime)
                        {

                            Rx[c] = HI.UL.ULDate.ConvertEN(worksheet.Cells[r, c].Value.DateTimeValue);

                        }
                        else
                        {
                            Rx[c] = worksheet.Cells[r, c].DisplayText;
                        }

                    }
                    dt.Rows.Add(Rx);
                }

            }
            _SpreadsheetControl = null;
            return dt;
        }

        public static DataTable PDFConvertRead(string InputFile, string SheetName, int SelectSheetind = -1)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US", true);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US", true);
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss";

            DataTable dt = null;
            int Sheetind = -1;
            DevExpress.XtraSpreadsheet.SpreadsheetControl _SpreadsheetControl = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
            DevExpress.Spreadsheet.Worksheet worksheet = null;
            _SpreadsheetControl.LoadDocument(InputFile);

            string ColName = "";

            if (SelectSheetind == -1)
            {
                foreach (DevExpress.Spreadsheet.Worksheet wk in _SpreadsheetControl.Document.Worksheets)
                {
                    if (wk.Name.ToUpper() == SheetName.ToUpper())
                    {
                        Sheetind = wk.Index;
                        worksheet = wk;
                        break;
                    }
                }
            }
            else
            {
                int I = -1;
                foreach (DevExpress.Spreadsheet.Worksheet wk in _SpreadsheetControl.Document.Worksheets)
                {
                    if (I == -1)
                    {
                        I = 0;
                    }

                    if (I == SelectSheetind)
                    {
                        Sheetind = wk.Index;
                        worksheet = wk;
                        break;
                    }
                    I = I + 1;

                }
            };

            if (Sheetind >= 0)
            {
                dt = new DataTable();

                for (int c = 0; c <= worksheet.GetUsedRange().ColumnCount - 1; c++)
                {
                    try
                    {
                        ColName = "F" + (c + 1).ToString();
                    }
                    catch (Exception ex)
                    {
                        ColName = c.ToString();
                    }

                    dt.Columns.Add(ColName, typeof(string));

                }

                for (int r = 0; r <= worksheet.GetUsedRange().RowCount - 1; r++)
                {
                    DataRow Rx = dt.NewRow();
                    for (int c = 0; c <= worksheet.GetUsedRange().ColumnCount - 1; c++)
                    {

                        if (worksheet.Cells[r, c].Value.Type == DevExpress.Spreadsheet.CellValueType.DateTime)
                        {

                            Rx[c] = HI.UL.ULDate.ConvertEN(worksheet.Cells[r, c].Value.DateTimeValue);

                        }
                        else
                        {
                            Rx[c] = worksheet.Cells[r, c].Value.ToString() ;
                        }

                    }
                    dt.Rows.Add(Rx);
                }

            }
            _SpreadsheetControl = null;
            return dt;
        }
    }
}