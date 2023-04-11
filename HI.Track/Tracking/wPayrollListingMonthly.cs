using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;

namespace HI.Track
{
    public partial class wPayrollListingMonthly : DevExpress.XtraEditors.XtraForm
    {
        public wPayrollListingMonthly()
        {
            InitializeComponent();
        }


        private void ocmload_Click(object sender, EventArgs e)
        {
            if (FTPayYear.Text != "" && FTPayYear.Text.Length == 4)
            {
                if (FNMonth.Text != "")
                {
                    if (FNHSysEmpTypeId.Text != "")
                    {
                        var _Spls = new HI.TL.SplashScreen("Loading...Data Please wait.");
                        LoadDataInfo();
                        _Spls.Close();
                    }
                    else
                    {
                        FNHSysEmpTypeId.Focus();
                    }
                }
                else
                {
                    FNMonth.Focus();
                    FNMonth.SelectAll();
                }
            }
            else
            {
                FTPayYear.Focus();
                FTPayYear.SelectAll();
            }
        }

        private void LoadDataInfo()
        {
             String _Qry = " SELECT M.FTEmpCode ";

            if (HI.ST.Lang.Language.Equals(HI.ST.SysInfo.LanguageLocal))
            {
                _Qry += "  ,P1.FTPreNameNameTH + ' ' + M.FTEmpNameTH + '  ' + M.FTEmpSurnameTH AS FTEmpName";
                _Qry += "  ,ES.FTNameTH  AS FTEmpStatusName ";
                _Qry += "  ,ET.FTEmpTypeNameTH  AS FTEmpTypeName ";
                _Qry += "  ,Dept.FTDeptDescTH  AS FTDeptName ";
                _Qry += "  ,OrgDiv.FTDivisonNameTH  AS FTDivisonName ";
                _Qry += "  ,OrgSect.FTSectNameTH  AS FTSectName ";
                _Qry += "  ,OrgUnitSect.FTUnitSectNameTH  AS FTUnitSectName ";
                _Qry += "  ,OrgPosit.FTPositNameTH  AS FTPositName ";
                _Qry += " ,ACG.FTAccountGroupCode, ACG.FTAccountGroupNameTH AS [FTAccountGroupName] ";
            }
            else
            {
                _Qry += "  ,P1.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' + M.FTEmpSurnameEN AS FTEmpName";
                _Qry += "  ,ES.FTNameEN  AS FTEmpStatusName ";
                _Qry += "  ,ET.FTEmpTypeNameEN  AS FTEmpTypeName ";
                _Qry += "  ,Dept.FTDeptDescEN  AS FTDeptName ";
                _Qry += "  ,OrgDiv.FTDivisonNameEN  AS FTDivisonName ";
                _Qry += "  ,OrgSect.FTSectNameEN  AS FTSectName ";
                _Qry += "  ,OrgUnitSect.FTUnitSectNameEN  AS FTUnitSectName ";
                _Qry += "  ,OrgPosit.FTPositNameEN  AS FTPositName ";
                _Qry += " ,ACG.FTAccountGroupCode, ACG.FTAccountGroupNameEN AS [FTAccountGroupName] ";
            }

            _Qry += "  ,ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode ";
            _Qry += "  ,ISNULL(Dept.FTDeptCode,'') AS FTDeptCode, ISNULL(OrgDiv.FTDivisonCode,'') AS FTDivisonCode";
            _Qry += "  ,ISNULL(OrgSect.FTSectCode,'') AS FTSectCode,ISNULL(OrgUnitSect.FTUnitSectCode,'') AS FTUnitSectCode, OrgPosit.FTPositCode, M.FNEmpStatus";
            _Qry += "  ,P.FTPayYear, MAX(P.FTPayTerm) AS FTPayTerm, P.FNHSysEmpID, P.FTEmpIdNo";

            _Qry += "  ,SUM(P.FNHoliday) AS FNHoliday, MAX(P.FNSalary) AS FNSalary, SUM(P.FNWorkingHour) AS FNWorkingHour";
            _Qry += "  ,SUM(P.FNOt1) AS  FNOt1";
            _Qry += "  ,SUM(P.FNOt15) AS  FNOt15";
            _Qry += "  ,SUM(P.FNOt2) AS  FNOt2";
            _Qry += "  ,SUM(P.FNOt3) AS  FNOt3";
            _Qry += "  ,SUM(P.FNOt4) AS  FNOt4";
            _Qry += "  , SUM(P.FNTotalLeavePay) AS FNTotalLeavePay, SUM(P.FCBaht) AS FCBaht, SUM(P.FCOt1_Baht) AS FCOt1_Baht, " +
                "SUM(P.FCOt15_Baht) AS FCOt15_Baht, SUM(P.FCOt2_Baht) AS FCOt2_Baht";
            _Qry += "  ,SUM(P.FCOt3_Baht) AS FCOt3_Baht, SUM(P.FCOt4_Baht) AS FCOt4_Baht, SUM(P.FCNetBaht) AS FCNetBaht, " +
                "SUM(P.FNPayLeaveVacationBaht) AS FNPayLeaveVacationBaht, SUM(P.FNPayLeaveOtherBaht) AS FNPayLeaveOtherBaht";
            _Qry += "  , SUM(P.FNTotalRecalSSO) AS FNTotalRecalSSO, SUM(P.FNTotalRecalTAX) AS FNTotalRecalTAX, " +
                "SUM(P.FNTotalAdd) AS FNTotalAdd, SUM(P.FNTotalAddOther) AS FNTotalAddOther, SUM(P.FNTotalExpense) AS FNTotalExpense";
            _Qry += "  ,SUM(P.FNTotalExpenseOther) AS FNTotalExpenseOther, SUM(P.FNTotalIncome) AS FNTotalIncome, " +
                "SUM(P.FNSocial) AS FNSocial, SUM(P.FNTax) AS FNTax, SUM(P.FHolidayBaht) AS FHolidayBaht";
            _Qry += "  ,SUM(P.FNNetpay) AS FNNetpay, SUM(P.FNPayLeaveSSo) AS FNPayLeaveSSo";

            _Qry += ", SUM(P.FNTotalWKNMin) AS FNTotalWKNMin , SUM(TS.FCHour) AS FCHour";

            _Qry += "  , SUM(ISNULL([001],0)) [001] ";
            _Qry += "  , SUM(ISNULL([009],0)) [009] ";
            _Qry += "  , SUM(ISNULL([012],0)) [012] ";
            _Qry += "  , SUM(ISNULL([016],0)) [016] ";
            _Qry += "  , SUM(ISNULL([032],0)) [032] ";
            _Qry += "  , SUM(ISNULL([033],0)) [033] ";
            _Qry += "  , SUM(ISNULL([034],0)) [034] ";
            _Qry += "  , SUM(ISNULL([035],0)) [035] ";
            _Qry += "  , SUM(ISNULL([036],0)) [036] ";
            _Qry += "  , SUM(ISNULL([043],0)) [043] ";
            _Qry += "  , SUM(ISNULL([106],0)) [106] ";
            _Qry += "  , SUM(ISNULL([108],0)) [108] ";
            _Qry += "  , SUM(ISNULL([109],0)) [109] ";
            _Qry += "  , SUM(ISNULL([110],0)) [110] ";
            _Qry += "  , SUM(ISNULL([111],0)) [111] ";
            _Qry += "  , SUM(ISNULL([112],0)) [112] ";
            _Qry += "  , SUM(ISNULL([113],0)) [113] ";

            _Qry += " , SUM(ISNULL([002],0)) [002] ";
            _Qry += " , SUM(ISNULL([017],0)) [017] "; 
            _Qry += " , SUM(ISNULL([008],0)) [008] "; 
            _Qry += " , SUM(ISNULL([014],0)) [014] "; 
            _Qry += " , SUM(ISNULL([050],0)) [050] "; 
            _Qry += " , SUM(ISNULL([011],0)) AS FNIncentiveAmt ";

            _Qry += " , (Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(SUM(FNTotalWKNMin) / (FCHour * 60)))),2)" +
                "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((SUM(FNTotalWKNMin) % (FCHour * 60)) / 60.00))),2)" +
                "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((SUM(FNTotalWKNMin) % (FCHour * 60)) % 60.00))),2))  AS FNWorkingDay";

            /*
            _Qry += " , ISNULL(FTStateHRSent,'0') as 'FTStateHRSent'  ";
            _Qry += " , ISNULL(FTUserHRSent,'') as 'FTUserHRSent'  ";
            _Qry += " , ISNULL(FTDateHRSent,'') as 'FTDateHRSent'  ";
            _Qry += " , ISNULL(FTTimeHRSent,'') as 'FTTimeHRSent'  ";

            _Qry += " , ISNULL(FTStateEmployeeAccept,'0') as 'FTStateEmployeeAccept'  ";
            _Qry += " , ISNULL(FTUserEmployeeAccept,'') as 'FTUserEmployeeAccept'  ";
            _Qry += " , ISNULL(FTDateEmployeeAccept,'') as 'FTDateEmployeeAccept'  ";
            _Qry += " , ISNULL(FTTimeEmployeeAccept,'') as 'FTTimeEmployeeAccept'  ";

            _Qry += " , ISNULL(FTStateHRAccept,'0') as 'FTStateHRAccept'  ";
            _Qry += " , ISNULL(FTUseHRAccept,'') as 'FTUseHRAccept'  ";
            _Qry += " , ISNULL(FTDateHRAccept,'') as 'FTDateHRAccept'  ";
            _Qry += " , ISNULL(FTTimeHRAccept,'') as 'FTTimeHRAccept'  ";
            */

            _Qry += "  FROM [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) + "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN";
            _Qry += "  [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) + "].dbo.THRTPayRoll AS P WITH (NOLOCK) ON M.FNHSysEmpID=P.FNHSysEmpID INNER JOIN";
            _Qry += "  [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.THRMPrename AS P1 WITH (NOLOCK) ON M.FNHSysPreNameId = P1.FNHSysPreNameId  INNER JOIN";
            _Qry += "  [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON P.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN";
            _Qry += "  [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON P.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN";
            _Qry += "  [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMDivision AS OrgDiv WITH (NOLOCK) ON P.FNHSysDivisonId = OrgDiv.FNHSysDivisonId LEFT OUTER JOIN";
            _Qry += "  [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMSect AS OrgSect WITH (NOLOCK) ON P.FNHSysSectId = OrgSect.FNHSysSectId LEFT OUTER JOIN";
            _Qry += "  [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMUnitSect AS OrgUnitSect WITH (NOLOCK) ON P.FNHSysUnitSectId = OrgUnitSect.FNHSysUnitSectId LEFT OUTER JOIN";
            _Qry += "  [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMPosition AS OrgPosit WITH (NOLOCK) ON P.FNHSysPositId = OrgPosit.FNHSysPositId LEFT OUTER JOIN";
            _Qry += "  [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) + "].dbo.THRMTimeShift AS TS WITH (NOLOCK) ON M.FNHSysShiftID=TS.FNHSysShiftID LEFT OUTER JOIN";
            _Qry += "  [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) + "].dbo.V_MEmpStatus AS ES ON M.FNEmpStatus = ES.FNListIndex";

            _Qry += "  LEFT OUTER JOIN [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMAccountGroup AS ACG WITH(NOLOCK) ON OrgUnitSect.FNHSysAccountGroupId = ACG.FNHSysAccountGroupId ";

            _Qry += " LEFT JOIN ( SELECT FTPayYear, FTPayTerm,FNHSysEmpID,[001], [009],[012], [016],[032],[033],[034],[035],[036], [043],[106],[108],[109],[110],[111],[112],[113] ,[002],[017],[008],[014] ,[050],[011] ";
            _Qry += " FROM ";
            _Qry += " ( ";
            _Qry += " SELECT  FTPayYear, FTPayTerm,FNHSysEmpID,FTFinCode,  FCTotalFinAmt ";
            _Qry += " FROM [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) + "].dbo.THRTPayRollFin ";
            _Qry += " WHERE ";
            _Qry += "    FTPayYear ='" + HI.UL.ULF.rpQuoted(FTPayYear.Text) + "'  ";
            _Qry += " ) D ";
            _Qry += " PIVOT ";
            _Qry += " ( MAX(FCTotalFinAmt) ";
            _Qry += " For FTFinCode   in ([001], [009],[012], [016],[032],[033],[034],[035],[036],[043],[106],[108],[109],[110],[111],[112],[113],[002],[017],[008],[014] ,[050],[011]  ) ";
            _Qry += " ) piv) AS V_Fin ON V_Fin.FNHSysEmpID=M.FNHSysEmpID AND V_Fin.FTPayYear=P.FTPayYear AND V_Fin.FTPayTerm=P.FTPayTerm ";


            _Qry += " WHERE (M.FTEmpCode <> '')";
            _Qry += " AND  M.FNHSysCmpId =" + HI.ST.SysInfo.CmpID + "  ";

            _Qry += " AND  P.FTPayYear ='" + HI.UL.ULF.rpQuoted(FTPayYear.Text) + "'  ";

            if (FNHSysEmpTypeId.Text != "")
            {
                _Qry += " AND ET.FTEmpTypeCode ='" + HI.UL.ULF.rpQuoted(FNHSysEmpTypeId.Text) + "' ";
            }

            //------Criteria By Employeee Code;
            if (FNHSysEmpId.Text != "")
            {
                _Qry += " AND M.FTEmpCode >= '" + HI.UL.ULF.rpQuoted(FNHSysEmpId.Text) + "' ";
            }

            if (FNHSysEmpIdTo.Text != "")
            {
                _Qry += " AND M.FTEmpCode <= '" + HI.UL.ULF.rpQuoted(FNHSysEmpIdTo.Text) + "' ";
            }

            //------Criteria By Department
            if (FNHSysDeptId.Text != "")
            {
                _Qry += " AND  Dept.FTDeptCode>='" + HI.UL.ULF.rpQuoted(FNHSysDeptId.Text) + "' ";
            }

            if (FNHSysDeptIdTo.Text != "")
            {
                _Qry += " AND  Dept.FTDeptCode<='" + HI.UL.ULF.rpQuoted(FNHSysDeptIdTo.Text) + "' ";
            }

            //------Criteria By Division
            if (FNHSysDivisonId.Text != "")
            {
                _Qry += " AND  OrgDiv.FTDivisonCode>='" + HI.UL.ULF.rpQuoted(FNHSysDivisonId.Text) + "' ";
            }

            if (FNHSysDivisonIdTo.Text != "")
            {
                _Qry += " AND  OrgDiv.FTDivisonCode<='" + HI.UL.ULF.rpQuoted(FNHSysDivisonIdTo.Text) + "' ";
            }

            //------Criteria By Sect
            if (FNHSysSectId.Text != "")
            {
                _Qry += " AND  OrgSect.FTSectCode>='" + HI.UL.ULF.rpQuoted(FNHSysSectId.Text) + "' ";
            }

            if (FNHSysSectIdTo.Text != "")
            {
                _Qry += " AND  OrgSect.FTSectCode<='" + HI.UL.ULF.rpQuoted(FNHSysSectIdTo.Text) + "' ";
            }

            //------Criteria Unit Sect
            if (FNHSysUnitSectId.Text != "")
            {
                _Qry += " AND   OrgUnitSect.FTUnitSectCode>='" + HI.UL.ULF.rpQuoted(FNHSysUnitSectId.Text) + "' ";
            }

            if (FNHSysUnitSectIdTo.Text != "")
            {
                _Qry += " AND   OrgUnitSect.FTUnitSectCode<='" + HI.UL.ULF.rpQuoted(FNHSysUnitSectIdTo.Text) + "' ";
            }

            _Qry = _Qry + HI.ST.Security.PermissionFilterEmployeeSalary();


            _Qry += " AND P.FTPayTerm in (SELECT DISTINCT PDT.FTPayTerm as PayTerm from HITECH_HR.dbo.THRMCfgPayDT AS PDT " +
                " JOIN HITECH_MASTER.dbo.THRMEmpType AS ET WITH(NOLOCK) ON PDT.FNHSysEmpTypeId = ET.FNHSysEmpTypeId " +
                " where FTPayYear = '" + HI.UL.ULF.rpQuoted(FTPayYear.Text) + "' AND PDT.FNMonth = " + (FNMonth.SelectedIndex + 1) + ")";

            _Qry += "GROUP BY FTEmpCode, FTEmpNameEN, FTEmpSurnameEN, FTPreNameNameEN,ES.FTNameEN,ET.FTEmpTypeNameEN,Dept.FTDeptDescEN," +
                "OrgDiv.FTDivisonNameEN,OrgSect.FTSectNameEN,OrgUnitSect.FTUnitSectNameEN,OrgPosit.FTPositNameEN,ACG.FTAccountGroupCode," +
                "ACG.FTAccountGroupNameEN,ET.FTEmpTypeCode,Dept.FTDeptCode,OrgDiv.FTDivisonCode,OrgSect.FTSectCode,OrgUnitSect.FTUnitSectCode," +
                "OrgPosit.FTPositCode,M.FNEmpStatus,P.FTPayYear,P.FNHSysEmpID,P.FTEmpIdNo,FCHour ";

            _Qry += " ORDER BY FTEmpCode ";
            ogc.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR);
            ogv.ExpandAllGroups();
            ogv.RefreshData();
            ogv.BestFitColumns();
        }

        private void wPayrollListingMonthly_Load(object sender, EventArgs e)
        {
            FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode;
            FTPayYear.Text = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTPayYear FROM  THRMCfgPayHD WITH(NOLOCK) Order BY FTPayYear Desc", Conn.DB.DataBaseName.DB_HR, "");
        }

        private void InitGrid()
        {
            //------Start Add Summary Grid-------------
            string sFieldCount = "FTEmpCode";
            string sFieldSum = "FCBaht|FCOt1_Baht|FCOt15_Baht|FCOt2_Baht|FCOt3_Baht|FCOt4_Baht|FNIncentiveAmt|FCNetBaht|FNPayLeaveVacationBaht|FNPayLeaveOtherBaht|FNTotalAdd|FNTotalAddOther|FNTotalExpense|FNTotalExpenseOther|FNTotalIncome|FNTotalRecalSSO|FNSocial|FNTax|FHolidayBaht|FNNetpay|FNTotalRecalTAX|008|009|014|016|017|032|043|050|112|113";

            string sFieldGrpCount = "FTEmpCode";
            string sFieldGrpSum = "FCBaht|FNTotalIncome|FNTotalRecalSSO|FNSocial|FNTax|FNNetpay|FNTotalRecalTAX";

            string sFieldCustomSum = "FNWorkingDay|FNOt1|FNOt15|FNOt2|FNOt3|FNOt4";
            string sFieldCustomGrpSum = "FNWorkingDay";

            ogv.ClearGrouping();
            ogv.ClearDocument();

            foreach (string Str in sFieldCount.Split('|'))
            {
                if (Str != "")
                {
                    ogv.Columns.ColumnByName(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str);
                    ogv.Columns.ColumnByName(Str).SummaryItem.DisplayFormat = "{0:n0}";
                }
            }

            foreach (string Str in sFieldCustomSum.Split('|'))
            {
                if (Str != "")
                {
                    ogv.Columns.ColumnByName(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str);
                    ogv.Columns.ColumnByName(Str).SummaryItem.DisplayFormat = "{0:n0}";
                }
            }

            foreach (string Str in sFieldSum.Split('|'))
            {
                if (Str != "")
                {
                    ogv.Columns.ColumnByName(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str);
                    ogv.Columns.ColumnByName(Str).SummaryItem.DisplayFormat = "{0:n2}";
                }
            }

            foreach (string Str in sFieldGrpCount.Split('|'))
            {
                if (Str != "")
                {
                    ogv.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, null, "(Count by " + ogv.Columns.ColumnByFieldName(Str).Caption + "={0:n0})");
                }
            }

            foreach (string Str in sFieldCustomGrpSum.Split('|'))
            {
                if (Str != "")
                {
                    ogv.GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, null, "(Sum by " + ogv.Columns.ColumnByFieldName(Str).Caption + "={0:n0})");

                }
            }

            foreach (string Str in sFieldGrpSum.Split('|'))
            {
                if (Str != "")
                {
                    ogv.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, null, "(Sum by " + ogv.Columns.ColumnByFieldName(Str).Caption + "={0:n2})");

                }
            }
            ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            ogv.OptionsView.ShowFooter = true;
            ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded;
            ogv.OptionsView.ShowGroupPanel = true;
            ogv.ExpandAllGroups();
            ogv.RefreshData();

            //------End Add Summary Grid------------ -
        }


        private void ocmexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ocmclear_Click(object sender, EventArgs e)
        {
            FNHSysEmpTypeId.Text = "";
            FNHSysDeptId.Text = "";
            FNHSysDivisonId.Text = "";
            FNHSysSectId.Text = "";
            FNHSysUnitSectId.Text = "";
            FNHSysEmpId.Text = "";
            FNHSysDeptIdTo.Text = "";
            FNHSysDivisonIdTo.Text = "";
            FNHSysSectIdTo.Text = "";
            FNHSysUnitSectIdTo.Text = "";
            FNHSysEmpIdTo.Text = "";
            HI.TL.HandlerControl.ClearControl(this);
        }
    }
}