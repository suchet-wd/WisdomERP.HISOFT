'Imports DevExpress.Data
Imports DevExpress.XtraGrid.Columns

Public Class wOrganizationChartMapping
    Private _wOrganizationChartMappingPopup As wOrganizationChartMappingPopup

#Region " Procedure "

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        InitGrid()
        _wOrganizationChartMappingPopup = New wOrganizationChartMappingPopup
        HI.TL.HandlerControl.AddHandlerObj(_wOrganizationChartMappingPopup)

        Dim oSysLang As New ST.SysLanguage
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wOrganizationChartMappingPopup.Name.ToString.Trim, _wOrganizationChartMappingPopup)
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
            _Qry = " SELECT  
                       FNHSysOrgMapId, FNHSysOrgMapId
                        ,FNHSysEmpID, FNHSysOrgId, FNPercentageQty, FTStateActive
                        , FTEmpCode
                        , FTPreNameNameTH +' '+ FTEmpNameTH +' '+ FTEmpSurnameTH AS 'FTEmpName'
                        , FNHSysCLevelId, FTCLevelCode, FTCLevelNameTH AS 'FTCLevelName'
                        , FNHSysCountryId, FTCountryCode, FTCountryNameTH AS 'FTCountryName'
                        , FNHSysCmpId, FTCmpCode, FTCmpNameTH AS 'FTCmpName'
                        , FNHSysDivisonId, FTDivisonCode, FTDivisonNameTH AS 'FTDivisonName'
                        , FNHSysDeptId, FTDeptCode, FTDeptDescTH AS 'FTDeptDesc'
                        , FNHSysSectId, FTSectCode, FTSectNameTH AS 'FTSectName'
                        , FNHSysUnitSectId, FTUnitSectCode, FTUnitSectNameTH AS 'FTUnitSectName'
                        , FNHSysPositId, FTPositCode, FTPositNameTH AS 'FTPositName'
                        ,FTRemark
                        FROM    " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.V_TCNMOrgmapping  "
        Else
            _Qry = " SELECT
                        FNHSysOrgMapId,FNHSysOrgMapId  
                        ,FNHSysEmpID, FNHSysOrgId, FNPercentageQty, FTStateActive
                        , FTEmpCode
                        , FTPreNameNameEN + ' ' + FTEmpNameEN + ' ' + FTEmpSurnameEN AS 'FTEmpName'
                        , FNHSysCLevelId, FTCLevelCode, FTCLevelNameEN AS 'FTCLevelName'
                        , FNHSysCountryId, FTCountryCode, FTCountryNameEN AS 'FTCountryName'
                        , FNHSysCmpId, FTCmpCode, FTCmpNameEN AS 'FTCmpName'
                        , FNHSysDivisonId, FTDivisonCode, FTDivisonNameEN AS 'FTDivisonName'
                        , FNHSysDeptId, FTDeptCode, FTDeptDescEN AS 'FTDeptDesc'
                        , FNHSysSectId, FTSectCode, FTSectNameEN AS 'FTSectName'
                        , FNHSysUnitSectId, FTUnitSectCode, FTUnitSectNameEN AS 'FTUnitSectName'
                        , FNHSysPositId, FTPositCode, FTPositNameEN AS 'FTPositName'
                        , FTRemark
                        FROM   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.V_TCNMOrgmapping "
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

    Private Sub wOrganizationChartMapping_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'HI.TL.HandlerControl.AddHandlerObj(_wOrganizationChartBelongToPopup)

        Call LoadData()
    End Sub

    Private Sub Ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click
        Try

            With _wOrganizationChartMappingPopup
                .ShowDialog()
                LoadData()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Ogvorganize_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles ogvorganize.RowCellClick
        Try
            Dim FNHSysOrgMapId As String = ogvorganize.GetFocusedRowCellValue("FNHSysOrgMapId").ToString()
            Dim FNHSysOrgId As String = ogvorganize.GetFocusedRowCellValue("FNHSysOrgId").ToString()

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

            Dim FNHSysEmpID As String = ogvorganize.GetFocusedRowCellValue("FNHSysEmpID").ToString()
            Dim FTEmpCode As String = ogvorganize.GetFocusedRowCellValue("FTEmpCode").ToString()
            Dim FTEmpName As String = ogvorganize.GetFocusedRowCellValue("FTEmpName").ToString()
            'Dim FTEmpSurname As String = ogvorganize.GetFocusedRowCellValue("FTEmpSurname").ToString()
            Dim FTStateActive As String = ogvorganize.GetFocusedRowCellValue("FTStateActive").ToString()
            Dim FNPercentageQty As String = ogvorganize.GetFocusedRowCellValue("FNPercentageQty").ToString()
            Dim FTRemark As String = ogvorganize.GetFocusedRowCellValue("FTRemark").ToString()


            With _wOrganizationChartMappingPopup

                .FNHSysOrgMapId = ""
                .btnOrganize.Text = ""
                .btnOrganize.Properties.Tag = ""
                '.FNHGroup.Text = ""
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

                .FNHSysEmpID.Properties.Tag = ""
                .FNHSysEmpID.Text = ""
                .FTEmpName.Text = ""

                .FTStateActive.Checked = True
                .FNPercentageQty.Text = ""
                .FTRemark.Text = ""


                .FNHSysOrgMapId = FNHSysOrgMapId
                .btnOrganize.Text = FNHSysOrgId
                .btnOrganize.Properties.Tag = FNHSysOrgId

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



                .FNHSysEmpID.Properties.Tag = FNHSysEmpID
                .FNHSysEmpID.Text = FTEmpCode
                .FTEmpName.Text = FTEmpName
                If FTStateActive = True Then
                    .FTStateActive.Checked = True
                Else
                    .FTStateActive.Checked = False

                End If

                .FNPercentageQty.Text = FNPercentageQty
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