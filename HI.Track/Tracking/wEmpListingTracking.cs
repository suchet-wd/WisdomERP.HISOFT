using System;
using System.Collections;
using System.Data;
using System.Linq;

namespace HI.Track
{
    public partial class wEmpListingTracking : DevExpress.XtraEditors.XtraForm
    {
        public wEmpListingTracking()
        {
            InitializeComponent();
        }

        private void ocmExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string CallMethodName
        {
            get; set;
        }

        public string CallMenuName
        {
            get; set;
        }

        public string CallMethodParm
        {
            get;set;
        }

        private void ocmLoadData_Click(object sender, EventArgs e)
        {
            if (VerifyField())
            {
                String _Qry = "";
                var _Spls = new HI.TL.SplashScreen("Loading...Data Please wait.");
                try
                {
                    ogcDetail.DataSource = null;
                    _Qry = "SELECT EM.FNHSysEmpID AS FNHSysEmpID, EM.FTEmpCode,  EM.FNHSysCmpId , Div.FTDivisonCode, Dept.FTDeptCode, ";
                    _Qry += "row_number ( ) OVER ( partition BY EM.FNHSysCmpId ORDER BY EM.FNHSysCmpId, Div.FTDivisonCode, Dept.FTDeptCode  DESC ) AS Seq, ";
                    if ((HI.ST.Lang.Language).ToString() == "TH")
                    {
                        _Qry += " (EM.FTEmpNameTH + ' ' + EM.FTEmpSurnameTH) AS EmpName, ";
                        _Qry += " (Dept.FTDeptDescTH) AS FTDeptDesc, ";
                        _Qry += " (Div.FTDivisonNameTH) AS FTDivisonName, ";
                    }
                    else
                    {
                        _Qry += " (EM.FTEmpNameEN + ' ' + EM.FTEmpSurnameEN) AS EmpName, ";
                        _Qry += " (Dept.FTDeptDescEN) AS FTDeptDesc, ";
                        _Qry += " (Div.FTDivisonNameEN) AS FTDivisonName, ";
                    }

                    _Qry += " Case When ISDATE(EM.FDDateStart) = 1 Then convert(Datetime, EM.FDDateStart) Else NULL END AS FDDateStart ,";
                    _Qry += " Case When ISDATE(EM.FDDateEnd) = 1 Then convert(Datetime, EM.FDDateEnd) Else NULL END AS FDDateEnd ,";
                    _Qry += " EM.FNHSysDeptId, EM.FNHSysDivisonId ";
                    _Qry += " , ISNULL(U.FTUserName, '') AS FTUserName ";
                    _Qry += " , CASE WHEN U.FTStateActive = 0 THEN 'INACTIVE' WHEN U.FTStateActive = 1 THEN 'ACTIVE' ELSE ISNULL(U.FTStateActive, '') END AS FTStateActive ";

                    //_Qry += " , CASE WHEN EM.FNEmpStatus = 0 THEN 'INACTIVE' WHEN EM.FNEmpStatus = 1 THEN 'ACTIVE' WHEN EM.FNEmpStatus = 2 THEN 'ACTIVE' ELSE ISNULL(EM.FNEmpStatus, '') END AS FNEmpStatus ";

                    _Qry += (chkPermission.Checked == true) ? ", ISNULL(( SELECT CMP.FTCmpCode FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TCNMCmp AS CMP WITH ( NOLOCK ) WHERE CMP.FNHSysCmpId = EM.FNHSysCmpId ), " +
                        "(SELECT CMP.FTCmpCode FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TCNMCmp AS CMP WITH(NOLOCK) WHERE CMP.FNHSysCmpId = PC.FNHSysCmpId)) AS FTCmpCode "
                    : " , (SELECT FTCmpCode  FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TCNMCmp AS CMP WITH(NOLOCK)  WHERE CMP.FNHSysCmpId = EM.FNHSysCmpId) AS FTCmpCode ";

                    _Qry += " , (SELECT FTNameTH FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) + ".dbo.HSysListData AS L WITH (NOLOCK) WHERE L.FTListName = 'FNEmpStatus' AND L.FNListIndex = EM.FNEmpStatus) AS FNEmpStatus";

                    _Qry += " FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + ".dbo.TSEUserLogin AS U WITH(NOLOCK) ";


                    _Qry += " OUTER APPLY(SELECT EM.* FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) + ".dbo.THRMEmployee AS EM WITH(NOLOCK) WHERE(U.FNHSysEmpID = em.FNHSysEmpID)) AS EM ";

                    _Qry += " OUTER APPLY( select FTDeptCode, FTDeptDescEN, FTDeptDescTH from " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TCNMDepartment AS Dept WITH (NOLOCK) " +
                        "WHERE Dept.FNHSysDeptId = EM.FNHSysDeptId) AS Dept";

                    _Qry += " OUTER APPLY( select FTDivisonCode, FTDivisonNameEN, FTDivisonNameTH from " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TCNMDivision AS Div WITH (NOLOCK) " +
                        "WHERE Div.FNHSysDivisonId = EM.FNHSysDivisonId) AS Div ";

                    _Qry += (chkPermission.Checked == true) ? " OUTER APPLY (SELECT UP.FNHSysPermissionID FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + ".dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) " +
                        " WHERE UP.FTUserName = U.FTUserName) AS UP" : "";

                    _Qry += (chkPermission.Checked == true) ? " OUTER APPLY(SELECT PC.FNHSysCmpId FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + ".dbo.TSEPermissionCmp AS PC WITH (NOLOCK) " +
                        " WHERE PC.FNHSysPermissionID = UP.FNHSysPermissionID) AS PC" : "";

                    _Qry += " WHERE U.FTUserName <> ''";
                    _Qry += (chkShowActive.Checked == true) ? " AND U.FTStateActive = 1 " : " ";

                    _Qry += " ORDER BY EM.FNHSysCmpId, Seq";

                    // ----- Remove Rows -----

                    DataTable dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT);

                    ogcDetail.DataSource = dt;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                _Spls.Close();
            }
            else
            {
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อนไข อย่างน้อย 1 รายการ !!!", 1406170001, this.Text, "", System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        private void ocmClear_Click(object sender, EventArgs e)
        {
            HI.TL.HandlerControl.ClearControl(this);
        }

        private bool VerifyField()
        {
            bool checkfield = true;
            //if (FNHSysStyleId.Text != "" && FNHSysStyleIdTo.Text != "")
            //{
            //    checkfield = true;
            //}
            //if (FNHSysSeasonId.Text != "" && FNHSysSeasonIdTo.Text != "")
            //{
            //    checkfield = true;
            //}
            //if (FNHSysStyleId.Text == "" && FNHSysStyleIdTo.Text != "")
            //{
            //    checkfield = false;
            //}
            //if (FNHSysStyleId.Text != "" && FNHSysStyleIdTo.Text == "")
            //{
            //    checkfield = false;
            //}
            //if (FNHSysSeasonId.Text == "" && FNHSysSeasonIdTo.Text != "")
            //{
            //    checkfield = false;
            //}
            //if (FNHSysSeasonId.Text != "" && FNHSysSeasonIdTo.Text == "")
            //{
            //    checkfield = false;
            //}
            return checkfield;
        }

    }
}