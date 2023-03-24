'Imports DevExpress.Data
Imports DevExpress.XtraGrid.Columns

Public Class wOrganizationChartAccountCost
    Private _wOrganizationChartAccountCostPopup As wOrganizationChartAccountCostPopup

#Region " Procedure "

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        InitGrid()
        _wOrganizationChartAccountCostPopup = New wOrganizationChartAccountCostPopup
        HI.TL.HandlerControl.AddHandlerObj(_wOrganizationChartAccountCostPopup)

        Dim oSysLang As New ST.SysLanguage
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wOrganizationChartAccountCostPopup.Name.ToString.Trim, _wOrganizationChartAccountCostPopup)
        Catch ex As Exception
        Finally
        End Try
        ' Add any initialization after the InitializeComponent() call.


    End Sub

    Private Sub InitGrid()
        With ogvorganize
            .ClearGrouping()
            .ClearDocument()
            .ExpandAllGroups()
            .RefreshData()

        End With
    End Sub

    Private Sub InitGridClearSort()
        For Each c As GridColumn In ogvorganize.Columns
            c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        Next

        ogvorganize.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click

    End Sub

    Private Sub LoadData()
        Dim _Qry As String = ""
        Dim _Dt As DataTable


        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry = " SELECT FNHSysCostId, FTCostCode, FTCostNameTH, FTCostNameEN
                    , C.FNHSysDeptId, C.FNHSysDivisonId
                    , C.FNHSysSectId, C.FNHSysUnitSectId
                    , C.FNHSysAccountCostId,  C.FTRemark, C.FTStateActive, C.FNHSysCmpId

                    , D.FTDeptCode, D.FTDeptDescTH AS FTDeptDesc
                    , Di.FTDivisonCode, Di.FTDivisonNameTH AS FTDivisonName
                    , S.FTSectCode, S.FTSectNameTH AS FTSectName
                    , US.FTUnitSectCode, US.FTUnitSectNameTH AS FTUnitSectName
                    , AC.FTAccountCostCode, AC.FTAccountCostNameTH AS FTAccountCostName
                    , C.FNSeq

                    FROM     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMPayroll_Cost C
                    LEFT OUTER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMUnitSect US WITH (nolock) ON C.FNHSysUnitSectId = US.FNHSysUnitSectId 
                    LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMSect  S WITH (nolock) ON C.FNHSysSectId = S.FNHSysSectId 
                    LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMDepartment D WITH (nolock) ON C.FNHSysDeptId = D.FNHSysDeptId 
                    LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMDivision Di WITH (nolock) ON C.FNHSysDivisonId = Di.FNHSysDivisonId 
                    LEFT JOIN   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMAccountCost AC WITH (nolock) ON C.FNHSysAccountCostId = AC.FNHSysAccountCostId 
                    WHERE C.FNHSysCmpId = " & HI.ST.SysInfo.CmpID


        Else
            _Qry = " SELECT FNHSysCostId, FTCostCode, FTCostNameTH, FTCostNameEN
                    , C.FNHSysDeptId, C.FNHSysDivisonId
                    , C.FNHSysSectId, C.FNHSysUnitSectId
                    , C.FNHSysAccountCostId,  C.FTRemark, C.FTStateActive, C.FNHSysCmpId

                    , D.FTDeptCode, D.FTDeptDescEN AS FTDeptDesc
                    , Di.FTDivisonCode, Di.FTDivisonNameEN AS FTDivisonName
                    , S.FTSectCode, S.FTSectNameEN AS FTSectName
                    , US.FTUnitSectCode, US.FTUnitSectNameEN AS FTUnitSectName
                    , AC.FTAccountCostCode, AC.FTAccountCostNameEN AS FTAccountCostName
                    , C.FNSeq

                    FROM     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMPayroll_Cost C
                    LEFT OUTER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMUnitSect US WITH (nolock) ON C.FNHSysUnitSectId = US.FNHSysUnitSectId 
                    LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMSect  S WITH (nolock) ON C.FNHSysSectId = S.FNHSysSectId 
                    LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMDepartment D WITH (nolock) ON C.FNHSysDeptId = D.FNHSysDeptId 
                    LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMDivision Di WITH (nolock) ON C.FNHSysDivisonId = Di.FNHSysDivisonId 
                    LEFT JOIN   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMAccountCost AC WITH (nolock) ON C.FNHSysAccountCostId = AC.FNHSysAccountCostId 
            WHERE C.FNHSysCmpId = " & HI.ST.SysInfo.CmpID
        End If

        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        ogcorganize.DataSource = _Dt
        Call InitialGridOrganizeMergCell()
    End Sub
#End Region

#Region "MAIN PROC"


    Private Sub ProcessClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region "General"

    Private Sub wOrganizationChartBelongTo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'HI.TL.HandlerControl.AddHandlerObj(_wOrganizationChartBelongToPopup)

        Call LoadData()
    End Sub

    Private Sub Ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click
        Try

            With _wOrganizationChartAccountCostPopup
                .ShowDialog()
                LoadData()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Ogvorganize_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles ogvorganize.RowCellClick

    End Sub



    Private Sub Ogvorganize_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvorganize.CellMerge
        Try
            With Me.ogvorganize
                Select Case e.Column.FieldName
                    Case "FNHSysCostId", "FNHGroup", "FTDivisonCode", "FTDivisonName", "FTDeptCode", "FTDeptDesc", "FTSectCode", "FTSectName", "FTUnitSectCode", "FTUnitSectName", "FTPositCode", "FTPositName"

                        If ("" & .GetRowCellValue(e.RowHandle1, "FNHSysCostId").ToString = "" & .GetRowCellValue(e.RowHandle2, "FNHSysCostId").ToString) _
                            And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case Else
                        e.Merge = False
                        e.Handled = True
                End Select

            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub InitialGridOrganizeMergCell()

        For Each c As GridColumn In ogvorganize.Columns

            Select Case c.FieldName.ToString
                Case "FNHSysCostId", "FNHGroup", "FTCLevelCode", "FTCLevelName", "FTCountryCode", "FTCountryName", "FTCmpCode", "FTCmpName", "FTDivisonCode", "FTDivisonName", "FTDeptCode", "FTDeptDesc", "FTSectCode", "FTSectName", "FTUnitSectCode", "FTUnitSectName", "FTPositCode", "FTPositName"
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                Case Else
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End Select

        Next

    End Sub

    Private Sub Ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        
        Call LoadData()
    End Sub

    Private Sub ogvorganize_DoubleClick(sender As Object, e As EventArgs) Handles ogvorganize.DoubleClick
        Try
            Dim FNHSysCostId As String = ogvorganize.GetFocusedRowCellValue("FNHSysCostId").ToString()
            Dim FTCostCode As String = ogvorganize.GetFocusedRowCellValue("FTCostCode").ToString()
            Dim FTCostNameTH As String = ogvorganize.GetFocusedRowCellValue("FTCostNameTH").ToString()
            Dim FTCostNameEN As String = ogvorganize.GetFocusedRowCellValue("FTCostNameEN").ToString()
            'Dim FTCountryCode As String = ogvorganize.GetFocusedRowCellValue("FTCountryCode").ToString()
            'Dim FTCountryName As String = ogvorganize.GetFocusedRowCellValue("FTCountryName").ToString()
            'Dim FTCmpCode As String = ogvorganize.GetFocusedRowCellValue("FTCmpCode").ToString()
            'Dim FTCmpName As String = ogvorganize.GetFocusedRowCellValue("FTCmpName").ToString()

            Dim FTDivisonCode As String = ogvorganize.GetFocusedRowCellValue("FTDivisonCode").ToString()
            Dim FTDivisonName As String = ogvorganize.GetFocusedRowCellValue("FTDivisonName").ToString()
            Dim FTDeptCode As String = ogvorganize.GetFocusedRowCellValue("FTDeptCode").ToString()
            Dim FTDeptDesc As String = ogvorganize.GetFocusedRowCellValue("FTDeptDesc").ToString()
            Dim FTSectCode As String = ogvorganize.GetFocusedRowCellValue("FTSectCode").ToString()
            Dim FTSectName As String = ogvorganize.GetFocusedRowCellValue("FTSectName").ToString()
            Dim FTUnitSectCode As String = ogvorganize.GetFocusedRowCellValue("FTUnitSectCode").ToString()
            Dim FTUnitSectName As String = ogvorganize.GetFocusedRowCellValue("FTUnitSectName").ToString()

            Dim FNHSysDeptId As String = ogvorganize.GetFocusedRowCellValue("FNHSysDeptId").ToString()
            Dim FNHSysDivisonId As String = ogvorganize.GetFocusedRowCellValue("FNHSysDivisonId").ToString()
            Dim FNHSysSectId As String = ogvorganize.GetFocusedRowCellValue("FNHSysSectId").ToString()
            Dim FNHSysUnitSectId As String = ogvorganize.GetFocusedRowCellValue("FNHSysUnitSectId").ToString()

            Dim FNHSysAccountCostId As String = ogvorganize.GetFocusedRowCellValue("FNHSysAccountCostId").ToString()
            Dim FTAccountCostCode As String = ogvorganize.GetFocusedRowCellValue("FTAccountCostCode").ToString()
            Dim FTAccountCostName As String = ogvorganize.GetFocusedRowCellValue("FTAccountCostName").ToString()

            Dim FTStateActive As String = ogvorganize.GetFocusedRowCellValue("FTStateActive").ToString()

            Dim FNSeq As Double = Format(Val(ogvorganize.GetFocusedRowCellValue("FNSeq").ToString()), "###0.00")

            With _wOrganizationChartAccountCostPopup

                .FNHSysCostId = ""
                .FTCostCode.Text = ""
                .FTCostNameTH.Text = ""
                .FTCostNameEN.Text = ""

                .FNHSysDivisonId.Properties.Tag = ""
                .FNHSysDivisonId.Text = ""
                .FNHSysDivisonId_None.Text = ""

                .FNHSysDeptId.Properties.Tag = ""
                .FNHSysDeptId.Text = ""
                .FNHSysDeptId_None.Text = ""

                .FNHSysSectId.Properties.Tag = ""
                .FNHSysSectId.Text = ""
                .FNHSysSectId_None.Text = ""

                .FNHSysUnitSectId.Properties.Tag = ""
                .FNHSysUnitSectId.Text = ""
                .FNHSysUnitSectId_None.Text = ""

                .FNHSysAccountCostId.Properties.Tag = ""
                .FNHSysAccountCostId.Text = ""
                .FNHSysAccountCostId_None.Text = ""


                .FTStateActive.Checked = True
                .FNHSysCostId = FNHSysCostId
                .FTCostCode.Text = FTCostCode
                .FTCostNameTH.Text = FTCostNameTH
                .FTCostNameEN.Text = FTCostNameEN

                .FNHSysDivisonId.Properties.Tag = FNHSysDivisonId
                .FNHSysDivisonId.Text = FTDivisonCode
                .FNHSysDivisonId_None.Text = FTDivisonName

                .FNHSysDeptId.Properties.Tag = FNHSysDeptId
                .FNHSysDeptId.Text = FTDeptCode
                .FNHSysDeptId_None.Text = FTDeptDesc

                .FNHSysSectId.Properties.Tag = FNHSysSectId
                .FNHSysSectId.Text = FTSectCode
                .FNHSysSectId_None.Text = FTSectName

                .FNHSysUnitSectId.Properties.Tag = FNHSysUnitSectId
                .FNHSysUnitSectId.Text = FTUnitSectCode
                .FNHSysUnitSectId_None.Text = FTUnitSectName

                .FNHSysAccountCostId.Properties.Tag = FNHSysAccountCostId
                .FNHSysAccountCostId.Text = FTAccountCostCode
                .FNHSysAccountCostId_None.Text = FTAccountCostName

                If FTStateActive = True Then
                    .FTStateActive.Checked = True
                Else
                    .FTStateActive.Checked = False

                End If

                .FNSeq.Text = FNSeq


                .ShowDialog()

                LoadData()
                ''FNFundRate.Value = .FundRate

            End With

        Catch ex As Exception
        End Try
    End Sub

#End Region

End Class