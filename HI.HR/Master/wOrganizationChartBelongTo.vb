'Imports DevExpress.Data
Imports DevExpress.XtraGrid.Columns

Public Class wOrganizationChartBelongTo
    Private _wOrganizationChartBelongToPopup As wOrganizationChartBelongToPopup

#Region " Procedure "

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        InitGrid()
        _wOrganizationChartBelongToPopup = New wOrganizationChartBelongToPopup
        HI.TL.HandlerControl.AddHandlerObj(_wOrganizationChartBelongToPopup)

        Dim oSysLang As New ST.SysLanguage
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wOrganizationChartBelongToPopup.Name.ToString.Trim, _wOrganizationChartBelongToPopup)
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



    End Sub

    Private Sub LoadData()
        Dim _Qry As String = ""
        Dim _Dt As DataTable
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry = " SELECT FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime
                        , FNHSysOrgBelongToId, FNHSysOrgId, FNHOrgBelongId
                        , 'HIGROUP' AS FNHGroup
	                    , FTCLevelCode,FTCLevelNameTH AS [FTCLevelName]
                        ,FTCountryCode, FTCountryNameTH AS [FTCountryName]
                        , FTCmpCode, FTCmpNameTH AS [FTCmpName]
                        , FTDivisonCode, FTDivisonNameTH AS [FTDivisonName]
                        , FTDeptCode, FTDeptDescTH AS [FTDeptDesc]
                        , FTSectCode, FTSectNameTH AS  [FTSectName]
                        , FTUnitSectCode, FTUnitSectNameTH AS [FTUnitSectName]
                        , FTPositCode, FTPositNameTH AS [FTPositName]
                        , 'HIGROUP' AS FNHGroup_B
	                    , FTCLevelCode_B,FTCLevelNameTH_B AS [FTCLevelName_B]
, FTCountryCode_B
                        , FTCountryNameTH_B AS [FTCountryName_B]
                        , FTCmpCode_B, FTCmpNameTH_B AS [FTCmpName_B]
                        , FTDivisonCode_B, FTDivisonNameTH_B AS [FTDivisonName_B]
                        , FTDeptCode_B, FTDeptDescTH_B AS [FTDeptDesc_B]
                        , FTSectCode_B, FTSectNameTH_B AS [FTSectName_B]
                        , FTUnitSectCode_B, FTUnitSectNameTH_B AS [FTUnitSectName_B]
                        , FTPositCode_B, FTPositNameTH_B AS [FTPositName_B]
                        , FNHSysEmpID, FTEmpCode
                        , FTEmpNameTH +' '+ FTEmpSurnameTH AS [FTEmpName]
                        , FTStateActive, FNTypeId, FTTypeName, FNPercentage, FTRemark
                        FROM    " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.HRDV_THRMOrganizationChartBelongTo  "
        Else
            _Qry = " SELECT FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime
                        , FNHSysOrgBelongToId, FNHSysOrgId, FNHOrgBelongId
                        , 'HIGROUP' AS FNHGroup
	                    , FTCLevelCode,FTCLevelNameEN AS [FTCLevelName]
                        ,FTCountryCode, FTCountryNameEN AS [FTCountryName]
                        , FTCmpCode, FTCmpNameEN AS [FTCmpName]
                        , FTDivisonCode, FTDivisonNameEN AS [FTDivisonName]
                        , FTDeptCode, FTDeptDescEN AS [FTDeptDesc]
                        , FTSectCode, FTSectNameEN AS  [FTSectName]
                        , FTUnitSectCode, FTUnitSectNameEN AS [FTUnitSectName]
                        , FTPositCode, FTPositNameEN AS [FTPositName]
                        , 'HIGROUP' AS FNHGroup_B
	                    , FTCLevelCode_B,FTCLevelNameEN_B AS [FTCLevelName_B]
,FTCountryCode_B
                        , FTCountryNameEN_B AS [FTCountryName_B]
                        , FTCmpCode_B, FTCmpNameEN_B AS [FTCmpName_B]
                        , FTDivisonCode_B, FTDivisonNameEN_B AS [FTDivisonName_B]
                        , FTDeptCode_B, FTDeptDescEN_B AS [FTDeptDesc_B]
                        , FTSectCode_B, FTSectNameEN_B AS [FTSectName_B]
                        , FTUnitSectCode_B, FTUnitSectNameEN_B AS [FTUnitSectName_B]
                        , FTPositCode_B, FTPositNameEN_B AS [FTPositName_B]
                        , FNHSysEmpID, FTEmpCode
                        , FTEmpNameEN +' '+ FTEmpSurnameEN AS [FTEmpName]
                        , FTStateActive, FNTypeId, FTTypeName, FNPercentage, FTRemark
                        FROM   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.HRDV_THRMOrganizationChartBelongTo "
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

            With _wOrganizationChartBelongToPopup
                .ShowDialog()
                LoadData()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Ogvorganize_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles ogvorganize.RowCellClick
        Try
            Dim FNHSysOrgBelongToId As String = ogvorganize.GetFocusedRowCellValue("FNHSysOrgBelongToId").ToString()
            Dim FNHSysOrgId As String = ogvorganize.GetFocusedRowCellValue("FNHSysOrgId").ToString()
            Dim FNHOrgBelongId As String = ogvorganize.GetFocusedRowCellValue("FNHOrgBelongId").ToString()
            Dim FNHGroup As String = ogvorganize.GetFocusedRowCellValue("FNHGroup").ToString()
            Dim FTCLevelCode As String = ogvorganize.GetFocusedRowCellValue("FTCLevelCode").ToString()
            Dim FTCLevelName As String = ogvorganize.GetFocusedRowCellValue("FTCLevelName").ToString()
            Dim FTCountryCode As String = ogvorganize.GetFocusedRowCellValue("FTCountryCode").ToString()
            Dim FTCountryName As String = ogvorganize.GetFocusedRowCellValue("FTCountryName").ToString()
            Dim FTCmpCode As String = ogvorganize.GetFocusedRowCellValue("FTCmpCode").ToString()
            Dim FTCmpName As String = ogvorganize.GetFocusedRowCellValue("FTCmpName").ToString()
            Dim FTDivisonCode As String = ogvorganize.GetFocusedRowCellValue("FTDivisonCode").ToString()
            Dim FTDivisonName As String = ogvorganize.GetFocusedRowCellValue("FTDivisonName").ToString()
            Dim FTDeptCode As String = ogvorganize.GetFocusedRowCellValue("FTDeptCode").ToString()
            Dim FTDeptDesc As String = ogvorganize.GetFocusedRowCellValue("FTDeptDesc").ToString()
            Dim FTSectCode As String = ogvorganize.GetFocusedRowCellValue("FTSectCode").ToString()
            Dim FTSectName As String = ogvorganize.GetFocusedRowCellValue("FTSectName").ToString()
            Dim FTUnitSectCode As String = ogvorganize.GetFocusedRowCellValue("FTUnitSectCode").ToString()
            Dim FTUnitSectName As String = ogvorganize.GetFocusedRowCellValue("FTUnitSectName").ToString()
            Dim FTPositCode As String = ogvorganize.GetFocusedRowCellValue("FTPositCode").ToString()
            Dim FTPositName As String = ogvorganize.GetFocusedRowCellValue("FTPositName").ToString()

            Dim FNHGroup_B As String = ogvorganize.GetFocusedRowCellValue("FNHGroup_B").ToString()
            Dim FTCLevelCode_B As String = ogvorganize.GetFocusedRowCellValue("FTCLevelCode_B").ToString()
            Dim FTCLevelName_B As String = ogvorganize.GetFocusedRowCellValue("FTCLevelName_B").ToString()
            Dim FTCountryCode_B As String = ogvorganize.GetFocusedRowCellValue("FTCountryCode_B").ToString()
            Dim FTCountryName_B As String = ogvorganize.GetFocusedRowCellValue("FTCountryName_B").ToString()
            Dim FTCmpCode_B As String = ogvorganize.GetFocusedRowCellValue("FTCmpCode_B").ToString()
            Dim FTCmpName_B As String = ogvorganize.GetFocusedRowCellValue("FTCmpName_B").ToString()
            Dim FTDivisonCode_B As String = ogvorganize.GetFocusedRowCellValue("FTDivisonCode_B").ToString()
            Dim FTDivisonName_B As String = ogvorganize.GetFocusedRowCellValue("FTDivisonName_B").ToString()
            Dim FTDeptCode_B As String = ogvorganize.GetFocusedRowCellValue("FTDeptCode_B").ToString()
            Dim FTDeptDesc_B As String = ogvorganize.GetFocusedRowCellValue("FTDeptDesc_B").ToString()
            Dim FTSectCode_B As String = ogvorganize.GetFocusedRowCellValue("FTSectCode_B").ToString()
            Dim FTSectName_B As String = ogvorganize.GetFocusedRowCellValue("FTSectName_B").ToString()
            Dim FTUnitSectCode_B As String = ogvorganize.GetFocusedRowCellValue("FTUnitSectCode_B").ToString()
            Dim FTUnitSectName_B As String = ogvorganize.GetFocusedRowCellValue("FTUnitSectName_B").ToString()
            Dim FTPositCode_B As String = ogvorganize.GetFocusedRowCellValue("FTPositCode_B").ToString()
            Dim FTPositName_B As String = ogvorganize.GetFocusedRowCellValue("FTPositName_B").ToString()

            Dim FNHSysEmpID As String = ogvorganize.GetFocusedRowCellValue("FNHSysEmpID").ToString()
            Dim FTEmpCode As String = ogvorganize.GetFocusedRowCellValue("FTEmpCode").ToString()
            Dim FTEmpName As String = ogvorganize.GetFocusedRowCellValue("FTEmpName").ToString()
            'Dim FTEmpSurname As String = ogvorganize.GetFocusedRowCellValue("FTEmpSurname").ToString()
            Dim FTStateActive As String = ogvorganize.GetFocusedRowCellValue("FTStateActive").ToString()
            Dim FNTypeId As String = ogvorganize.GetFocusedRowCellValue("FNTypeId").ToString()
            Dim FTTypeName As String = ogvorganize.GetFocusedRowCellValue("FTTypeName").ToString()
            Dim FNPercentage As String = ogvorganize.GetFocusedRowCellValue("FNPercentage").ToString()
            Dim FTRemark As String = ogvorganize.GetFocusedRowCellValue("FTRemark").ToString()




            With _wOrganizationChartBelongToPopup

                .FNHSysOrgBelongToId = ""
                .btnOrganize.Text = ""
                .btnOrganize.Properties.Tag = ""
                .btnOrganizeBelongTo.Text = ""
                .btnOrganizeBelongTo.Properties.Tag = ""
                .FNHGroup.Text = ""
                .FTCLevelCode.Text = ""
                .FTCLevelName.Text = ""
                .FTCountryCode.Text = ""
                .FTCountryName.Text = ""
                .FTCmpCode.Text = ""
                .FTCmpName.Text = ""
                .FTDivisonCode.Text = ""
                .FTDivisonName.Text = ""
                .FTDeptCode.Text = ""
                .FTDeptDesc.Text = ""
                .FTSectCode.Text = ""
                .FTSectName.Text = ""
                .FTUnitSectCode.Text = ""
                .FTUnitSectName.Text = ""
                .FTPositCode.Text = ""
                .FTPositName.Text = ""

                .FNHGroup_B.Text = ""
                .FTCLevelCode_B.Text = ""
                .FTCLevelName_B.Text = ""
                .FTCountryCode_B.Text = ""
                .FTCountryName_B.Text = ""
                .FTCmpCode_B.Text = ""
                .FTCmpName_B.Text = ""
                .FTDivisonCode_B.Text = ""
                .FTDivisonName_B.Text = ""
                .FTDeptCode_B.Text = ""
                .FTDeptDesc_B.Text = ""
                .FTSectCode_B.Text = ""
                .FTSectName_B.Text = ""
                .FTUnitSectCode_B.Text = ""
                .FTUnitSectName_B.Text = ""
                .FTPositCode_B.Text = ""
                .FTPositName_B.Text = ""

                .FNHSysEmpID.Properties.Tag = ""
                .FNHSysEmpID.Text = ""
                .FTEmpName.Text = ""

                .FTStateActive.Checked = True
                '.FNType.SelectedItem = "0"
                .FNPercentage.Text = ""
                .FTRemark.Text = ""


                .FNHSysOrgBelongToId = FNHSysOrgBelongToId
                .btnOrganize.Text = FNHSysOrgId
                .btnOrganize.Properties.Tag = FNHSysOrgId
                .btnOrganizeBelongTo.Text = FNHOrgBelongId
                .btnOrganizeBelongTo.Properties.Tag = FNHOrgBelongId
                .FNHGroup.Text = FNHGroup
                .FTCLevelCode.Text = FTCLevelCode
                .FTCLevelName.Text = FTCLevelName
                .FTCountryCode.Text = FTCountryCode
                .FTCountryName.Text = FTCountryName
                .FTCmpCode.Text = FTCmpCode
                .FTCmpName.Text = FTCmpName
                .FTDivisonCode.Text = FTDivisonCode
                .FTDivisonName.Text = FTDivisonName
                .FTDeptCode.Text = FTDeptCode
                .FTDeptDesc.Text = FTDeptDesc
                .FTSectCode.Text = FTSectCode
                .FTSectName.Text = FTSectName
                .FTUnitSectCode.Text = FTUnitSectCode
                .FTUnitSectName.Text = FTUnitSectName
                .FTPositCode.Text = FTPositCode
                .FTPositName.Text = FTPositName

                .FNHGroup_B.Text = FNHGroup_B
                .FTCLevelCode_B.Text = FTCLevelCode_B
                .FTCLevelName_B.Text = FTCLevelName_B
                .FTCountryCode_B.Text = FTCountryCode_B
                .FTCountryName_B.Text = FTCountryName_B
                .FTCmpCode_B.Text = FTCmpCode_B
                .FTCmpName_B.Text = FTCmpName_B
                .FTDivisonCode_B.Text = FTDivisonCode_B
                .FTDivisonName_B.Text = FTDivisonName_B
                .FTDeptCode_B.Text = FTDeptCode_B
                .FTDeptDesc_B.Text = FTDeptDesc_B
                .FTSectCode_B.Text = FTSectCode_B
                .FTSectName_B.Text = FTSectName_B
                .FTUnitSectCode_B.Text = FTUnitSectCode_B
                .FTUnitSectName_B.Text = FTUnitSectName_B
                .FTPositCode_B.Text = FTPositCode_B
                .FTPositName_B.Text = FTPositName_B

                .FNHSysEmpID.Properties.Tag = FNHSysEmpID
                .FNHSysEmpID.Text = FTEmpCode
                .FTEmpName.Text = FTEmpName
                If FTStateActive = True Then
                    .FTStateActive.Checked = True
                Else
                    .FTStateActive.Checked = False

                End If

                .FNType.SelectedItem = FTTypeName
                .FNPercentage.Text = FNPercentage
                .FTRemark.Text = FTRemark


                .ShowDialog()

                LoadData()
                ''FNFundRate.Value = .FundRate

            End With

        Catch ex As Exception
        End Try
    End Sub



    Private Sub Ogvorganize_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvorganize.CellMerge
        Try
            With Me.ogvorganize
                Select Case e.Column.FieldName
                    Case "FNHSysOrgId", "FNHGroup", "FTCLevelCode", "FTCLevelName", "FTCountryCode", "FTCountryName", "FTCmpCode", "FTCmpName", "FTDivisonCode", "FTDivisonName", "FTDeptCode", "FTDeptDesc", "FTSectCode", "FTSectName", "FTUnitSectCode", "FTUnitSectName", "FTPositCode", "FTPositName"

                        If ("" & .GetRowCellValue(e.RowHandle1, "FNHSysOrgId").ToString = "" & .GetRowCellValue(e.RowHandle2, "FNHSysOrgId").ToString) _
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
                Case "FNHSysOrgId", "FNHGroup", "FTCLevelCode", "FTCLevelName", "FTCountryCode", "FTCountryName", "FTCmpCode", "FTCmpName", "FTDivisonCode", "FTDivisonName", "FTDeptCode", "FTDeptDesc", "FTSectCode", "FTSectName", "FTUnitSectCode", "FTUnitSectName", "FTPositCode", "FTPositName"
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

#End Region

End Class