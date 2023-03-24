Imports DevExpress.XtraGrid.Views.Grid
Imports System.Drawing

Public Class wTrackingDoc

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub wTrackingDoc_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
          
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Call LoadData()
    End Sub

    Private Sub LoadData()
        Dim _Spls As New HI.TL.SplashScreen("Please Waiting loading data....")
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "SELECT         H.FTDocumentNo, CASE WHEN isdate(H.FDDocumentDate) = 1 THEN CONVERT(varchar(10), CONVERT(datetime, H.FDDocumentDate), 103) ELSE '' END AS FDDocumentDate, H.FTDocumentBy, "
            _Cmd &= vbCrLf & " H.FNHSysCmpId, H.FNDocType, H.FTDocumentTitle, H.FBDocument, H.FNFileType, H.FTNote, H.FTSandApprove, H.FTSandApproveBy, H.FDSandApproveDate, H.FTSendApproveTime, H.FTBenefit, "
            _Cmd &= vbCrLf & " H.FTOperActivityName, H.FNHSysDocNameId, H.FTDocRefCode, H.FTStateManagerApp, H.FDManagerAppDate, H.FTManagerAppTime, H.FTManagerAppBy, C.FTCmpCode, "
            _Cmd &= vbCrLf & " D.FTDocNameCode, H.FTOwnerStateApprove, H.FTOwnerApproveBy, Isnull(T.FTStateManagerApp,'0') AS FTStateManagerApp "
            _Cmd &= vbCrLf & "  ,H.FDOwnerApproveDate, H.FTOwnerApproveTime, Isnull(H.FT91StateApprove,'0') AS FT91StateApprove , H.FT91ApproveBy, H.FD91ApproveDate, H.FT91ApproveTime, Isnull(H.FT70StateApprove,'0') AS FT70StateApprove, H.FT70ApproveBy, H.FD70ApproveDate,"
            _Cmd &= vbCrLf & "  H.FT70ApproveTime, Isnull(H.FTC1StateApprove,'0') AS FTC1StateApprove , H.FTC1ApproveBy, H.FDC1ApproveDate, H.FTC1ApproveTime,Isnull(H.FTC2StateApprove,'0') AS FTC2StateApprove, H.FTC2ApproveBy, H.FDC2ApproveDate, H.FTC2ApproveTime, Isnull(H.FTC3StateApprove,'0') AS FTC3StateApprove,"
            _Cmd &= vbCrLf & " H.FTC3ApproveBy, H.FDC3ApproveDate, H.FTC3ApproveTime, Isnull(H.FTSRStateApprove,'0') AS FTSRStateApprove, H.FTSRApproveBy, H.FDSRApproveDate, H.FTSRApproveTime, Isnull(H.FTSPStateApprove,'0') AS FTSPStateApprove, H.FTSPApproveBy, H.FDSPApproveDate,"
            _Cmd &= vbCrLf & " H.FTSPApproveTime, Isnull(H.FTCDStateApprove,'0') AS FTCDStateApprove , H.FTCDApproveBy, H.FDCDApproveDate, H.FTCDApproveTime, Isnull(H.FTVNStateApprove,'0') AS FTVNStateApprove , H.FTVNApproveBy, H.FDVNApproveDate, H.FTVNApproveTime,"
            _Cmd &= vbCrLf & " Isnull(H.FTFGStateApprove,'0') AS FTFGStateApprove , H.FTFGApproveBy, H.FDFGApproveDate, H.FTFGApproveTime"
            _Cmd &= vbCrLf & " , Isnull(H.FTStateMNGDepApp,'0') AS FTStateMNGDepApp "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", L1.FTNameTH AS FTDocTypeName, L2.FTNameTH AS FTFileTypeName, D.FTDocNameTH AS FTDocName, L3.FTNameTH AS FNOperActivity , C.FTCmpNameTH AS FTCmpName "
                _Cmd &= vbCrLf & ", [dbo].[FN_GET_YMD](T.FDManagerAppDate,convert(varchar(10),getdate(),111),'TH') AS FDDocAge  "
                _Cmd &= vbCrLf & " , S.FTUnitSectNameTH  as FTUnitSectName "
                _Cmd &= vbCrLf & " ,DC.FTUnitSectNameTH as FTUnitSectNameCreate "
            Else
                _Cmd &= vbCrLf & ", L1.FTNameEN AS FTDocTypeName, L2.FTNameEN AS FTFileTypeName, D.FTDocNameEN AS FTDocName, L3.FTNameEN AS FNOperActivity , C.FTCmpNameEN AS FTCmpName"
                _Cmd &= vbCrLf & ", [dbo].[FN_GET_YMD](T.FDManagerAppDate,convert(varchar(10),getdate(),111),'EN') AS FDDocAge  "
                _Cmd &= vbCrLf & " , S.FTUnitSectNameEN  as FTUnitSectName "
                _Cmd &= vbCrLf & " ,DC.FTUnitSectNameEN as FTUnitSectNameCreate "
            End If
            _Cmd &= vbCrLf & " ,isnull(H.FNReviseNo,'0') as FNReviseNo ,convert(varchar(10) , convert(date,isnull(H.FDUpdDate,H.FDInsDate)),103) as FDUpdDate "
            _Cmd &= vbCrLf & " ,FTOperActivityName  , S.FTUnitSectCode "
            _Cmd &= vbCrLf & " ,DC.FTUnitSectCode as FTUnitSectCodeCreate "
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument AS H WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH (NOLOCK) ON H.FNHSysCmpId = C.FNHSysCmpId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TDOCMDocumentTitle AS D WITH (NOLOCK) ON H.FNHSysDocNameId = D.FNHSysDocNameId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "(SELECT        FTListName, FNListIndex, FTNameTH, FTNameEN"
            _Cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH (NOLOCK)"
            _Cmd &= vbCrLf & " WHERE        (FTListName = 'FTDocType')) AS L1 ON H.FNDocType = L1.FNListIndex LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "(SELECT        FTListName, FNListIndex, FTNameTH, FTNameEN"
            _Cmd &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS HSysListData_1"
            _Cmd &= vbCrLf & "  WHERE        (FTListName = 'FTFileType')) AS L2 ON H.FNFileType = L2.FNListIndex LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " (SELECT        FTListName, FNListIndex, FTNameTH, FTNameEN"
            _Cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS HSysListData_1"
            _Cmd &= vbCrLf & "WHERE        (FTListName = 'FNOperActivity')) AS L3 ON H.FNOperActivity = L3.FNListIndex"
            _Cmd &= vbCrLf & "INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TDOCMDocumentTitle AS T WITH(NOLOCK) ON H.FNHSysDocNameId = T.FNHSysDocNameId"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS S WITH(NOLOCK) ON H.FNHSysUnitSectId = S.FNHSysUnitSectId "
            _Cmd &= vbCrLf & "  LEFT OUTER JOIN (SELECT L.FTUserName, L.FNHSysEmpID, S.FTUnitSectCode, S.FTUnitSectNameTH, S.FTUnitSectNameEN "
            _Cmd &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].. TSEUserLogin AS L WITH (NOLOCK) LEFT OUTER JOIN "
            _Cmd &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH (NOLOCK) ON L.FNHSysEmpID = E.FNHSysEmpID LEFT OUTER JOIN "
            _Cmd &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS S WITH (NOLOCK) ON E.FNHSysUnitSectId = S.FNHSysUnitSectId) AS DC  ON H.FTDocumentBy = DC.FTUserName "


            _Cmd &= vbCrLf & "Where Isnull(H.FTStateManagerApp,'0') = '1'  "
            If Me.FNHSysCmpId.Text <> "" Then
                _Cmd &= vbCrLf & " And C.FTCmpCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysCmpId.Text) & "'"
            End If
            If Me.FNHSysCmpIdTo.Text <> "" Then
                _Cmd &= vbCrLf & " And C.FTCmpCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysCmpIdTo.Text) & "'"
            End If
            If Me.FNDocType.SelectedIndex > 0 Then
                _Cmd &= vbCrLf & " AND  H.FNDocType=" & Integer.Parse("0" & Me.FNDocType.SelectedIndex) - 1
            End If
            If Me.FNFileType.SelectedIndex > 0 Then
                _Cmd &= vbCrLf & " AND  H.FNFileType=" & Integer.Parse("0" & Me.FNFileType.SelectedIndex) - 1
            End If
            If Me.FTStartDate.Text <> "" Then
                _Cmd &= vbCrLf & " And T.FDManagerAppDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
            End If
            If Me.FTEndDate.Text <> "" Then
                _Cmd &= vbCrLf & " And T.FDManagerAppDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
            End If


            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_DOC)

            _Spls.Close()
            Me.ogcdocument.DataSource = _oDt
        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub

    Private Sub ogvdocument_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles ogvdocument.RowStyle
        Try
            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then
                Dim _FNDocType As Integer = View.GetRowCellDisplayText(e.RowHandle, View.Columns("FNDocType"))
                Dim _FTMngApp As String = View.GetRowCellDisplayText(e.RowHandle, View.Columns("FTStateManagerApp"))
                If _FNDocType = 0 Then
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                Else
                    e.Appearance.BackColor = Color.SkyBlue
                    e.Appearance.BackColor2 = Color.LightSkyBlue
                End If

                If _FTMngApp = "1" Then
                    e.Appearance.BackColor = Color.Green
                    e.Appearance.BackColor2 = Color.LightGreen
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

 
End Class