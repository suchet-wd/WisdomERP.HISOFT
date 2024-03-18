'Imports DevExpress.Data
Imports DevExpress.XtraGrid.Columns

Public Class wConfigPayrollMonthly_LA
    Private _wConfigPayRollMonthly_LA_Popup As wConfigPayRollMonthly_LA_Popup

#Region " Procedure "

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        InitGrid()
        _wConfigPayRollMonthly_LA_Popup = New wConfigPayRollMonthly_LA_Popup
        HI.TL.HandlerControl.AddHandlerObj(_wConfigPayRollMonthly_LA_Popup)

        Dim oSysLang As New ST.SysLanguage
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wConfigPayRollMonthly_LA_Popup.Name.ToString.Trim, _wConfigPayRollMonthly_LA_Popup)
        Catch ex As Exception
        Finally
        End Try
        ' Add any initialization after the InitializeComponent() call.


    End Sub

    Private Sub InitGrid()
        With ogv
            .ClearGrouping()
            .ClearDocument()
            .ExpandAllGroups()
            .RefreshData()

        End With
    End Sub

    Private Sub InitGridClearSort()
        For Each c As GridColumn In ogv.Columns
            c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        Next

        ogv.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click

    End Sub

    Private Sub LoadData()
        Dim _Qry As String = ""
        Dim _Dt As DataTable



        _Qry = " SELECT M.FNHSysEmpID , E.FTEmpCode "
        _Qry &= vbCrLf & " , E.FTEmpNameTH + ' ' + E.FTEmpSurnameTH AS 'FTEmpNameLocal'  "
        _Qry &= vbCrLf & " , E.FTEmpNameEN + ' ' + E.FTEmpSurnameEN AS 'FTEmpNameEN'  "
        _Qry &= vbCrLf & " , M.FNSalary, FCOt1_Baht, FNTotalIncome "
        _Qry &= vbCrLf & " , FNTotalRecalSSO, FNSocial, FNSocialCmp "
        _Qry &= vbCrLf & " , FNTotalRecalTAX, FNTax, FNTax5, FNTax10, FNTax15 "
        _Qry &= vbCrLf & " , FNTax20, FNTax25 "
        _Qry &= vbCrLf & "  , FNNetpay "
        _Qry &= vbCrLf & "   , M.FNHSysCmpId , FTStateActive "
        _Qry &= vbCrLf & "   , M.FTTaxNo_TH, M.FTSocialNo_TH "


        _Qry &= vbCrLf & "  , M.FTAddrNo, M.FTAddrHome, M.FTAddrMoo, M.FTAddrSoi, M.FTAddrRoad, M.FTAddrTumbol, M.FTAddrAmphur, M.FTAddrProvince, M.FTAddrPostCode, M.FTAddrTel "
        _Qry &= vbCrLf & "  , M.FTAddrNo1, M.FTAddrHome1, M.FTAddrMoo1, M.FTAddrSoi1, M.FTAddrRoad1, M.FTAddrTumbol1, M.FTAddrAmphur1, M.FTAddrProvince1, M.FTAddrPostCode1, M.FTAddrTel1 "
        _Qry &= vbCrLf & "  "

        _Qry &= vbCrLf & "  "
        _Qry &= vbCrLf & "  , M.FTEmpNameTH  , M.FTEmpSurnameTH "



        _Qry &= vbCrLf & "     FROM     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMPayroll_Monthly M "
        _Qry &= vbCrLf & " LEFT JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee E ON E.FNHSysEmpID = M.FNHSysEmpID AND  E.FNHSysCmpId = M.FNHSysCmpId "
        _Qry &= vbCrLf & " "


        'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
        '    _Qry = " SELECT H.FNHSysPositId,  P.FTPositCode  "
        '    _Qry &= vbCrLf & " ,P.FTPositNameTH  ,P.FTPositNameEN "
        '    _Qry &= vbCrLf & " , H.FNHSysCmpId, FDDateFrom, FDDateTo, FNINcentiveMultiple ,H.FTStateActive "

        '    _Qry &= vbCrLf & "     FROM     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo. TPRODMIncentiveHeader H "
        '    _Qry &= vbCrLf & " LEFT JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMPosition P ON H.FNHSysPositId=P.FNHSysPositId  AND  H.FNHSysCmpId= P.FNHSysCmpId "
        '    _Qry &= vbCrLf & " "

        'Else
        '    _Qry = " SELECT H.FNHSysPositId,  P.FTPositCode  "
        '    _Qry &= vbCrLf & " ,P.FTPositNameEN ,P.FTPositNameEN  "
        '    _Qry &= vbCrLf & " , H.FNHSysCmpId, FDDateFrom, FDDateTo, FNINcentiveMultiple ,H.FTStateActive "

        '    _Qry &= vbCrLf & "     FROM     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo. TPRODMIncentiveHeader H "
        '    _Qry &= vbCrLf & " LEFT JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.TCNMPosition P ON H.FNHSysPositId=P.FNHSysPositId  AND  H.FNHSysCmpId= P.FNHSysCmpId "
        '    _Qry &= vbCrLf & " "
        'End If

        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        ogc.DataSource = _Dt
        ''  Call InitialGridOrganizeMergCell()
    End Sub
#End Region

#Region "MAIN PROC"


    Private Sub ProcessClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region "General"


    Private Sub Ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click
        Try

            With _wConfigPayRollMonthly_LA_Popup

                .ShowDialog()
                LoadData()
            End With
        Catch ex As Exception
        End Try
    End Sub





    'Private Sub Ogvorganize_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogv.CellMerge
    '    Try
    '        With Me.ogv
    '            Select Case e.Column.FieldName
    '                Case "FNHSysCostId", "FNHGroup", "FTDivisonCode", "FTDivisonName", "FTDeptCode", "FTDeptDesc", "FTSectCode", "FTSectName", "FTUnitSectCode", "FTUnitSectName", "FTPositCode", "FTPositName"

    '                    If ("" & .GetRowCellValue(e.RowHandle1, "FNHSysCostId").ToString = "" & .GetRowCellValue(e.RowHandle2, "FNHSysCostId").ToString) _
    '                        And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then
    '                        e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
    '                        e.Handled = True
    '                        e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    '                    Else
    '                        e.Merge = False
    '                        e.Handled = True
    '                    End If
    '                Case Else
    '                    e.Merge = False
    '                    e.Handled = True
    '            End Select

    '        End With

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub InitialGridOrganizeMergCell()

    '    For Each c As GridColumn In ogv.Columns

    '        Select Case c.FieldName.ToString
    '            Case "FNHSysCostId", "FNHGroup", "FTCLevelCode", "FTCLevelName", "FTCountryCode", "FTCountryName", "FTCmpCode", "FTCmpName", "FTDivisonCode", "FTDivisonName", "FTDeptCode", "FTDeptDesc", "FTSectCode", "FTSectName", "FTUnitSectCode", "FTUnitSectName", "FTPositCode", "FTPositName"
    '                c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
    '                c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    '            Case Else
    '                c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
    '        End Select

    '    Next

    'End Sub

    Private Sub Ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click

        Call LoadData()
    End Sub

    Private Sub ogv_DoubleClick(sender As Object, e As EventArgs) Handles ogv.DoubleClick
        Try
            Dim _FNHSysEmpID As String = ogv.GetFocusedRowCellValue("FNHSysEmpID").ToString()
            Dim _FTEmpCode As String = ogv.GetFocusedRowCellValue("FTEmpCode").ToString()
            Dim _FTEmpNameLocal As String = ogv.GetFocusedRowCellValue("FTEmpNameLocal").ToString()
            Dim _FTEmpNameEN As String = ogv.GetFocusedRowCellValue("FTEmpNameEN").ToString()


            Dim _FNSalary As String = ogv.GetFocusedRowCellValue("FNSalary").ToString()

            Dim _FCOt1_Baht As String = ogv.GetFocusedRowCellValue("FCOt1_Baht").ToString()
            Dim _FNTotalIncome As String = ogv.GetFocusedRowCellValue("FNTotalIncome").ToString()
            Dim _FNTotalRecalSSO As String = ogv.GetFocusedRowCellValue("FNTotalRecalSSO").ToString()
            Dim _FNSocial As String = ogv.GetFocusedRowCellValue("FNSocial").ToString()
            Dim _FNSocialCmp As String = ogv.GetFocusedRowCellValue("FNSocialCmp").ToString()
            Dim _FNTotalRecalTAX As String = ogv.GetFocusedRowCellValue("FNTotalRecalTAX").ToString()
            Dim _FNTax As String = ogv.GetFocusedRowCellValue("FNTax").ToString()
            Dim _FNNetpay As String = ogv.GetFocusedRowCellValue("FNNetpay").ToString()
            Dim _FNHSysCmpId As String = ogv.GetFocusedRowCellValue("FNHSysCmpId").ToString()

            Dim _FTStateActive As String = ogv.GetFocusedRowCellValue("FTStateActive").ToString()


            Dim _FTTaxNo_TH As String = ogv.GetFocusedRowCellValue("FTTaxNo_TH").ToString()
            Dim _FTSocialNo_TH As String = ogv.GetFocusedRowCellValue("FTSocialNo_TH").ToString()

            Dim _FTAddrNo As String = ogv.GetFocusedRowCellValue("FTAddrNo").ToString()
            Dim _FTAddrHome As String = ogv.GetFocusedRowCellValue("FTAddrHome").ToString()
            Dim _FTAddrMoo As String = ogv.GetFocusedRowCellValue("FTAddrMoo").ToString()
            Dim _FTAddrSoi As String = ogv.GetFocusedRowCellValue("FTAddrSoi").ToString()
            Dim _FTAddrRoad As String = ogv.GetFocusedRowCellValue("FTAddrRoad").ToString()
            Dim _FTAddrTumbol As String = ogv.GetFocusedRowCellValue("FTAddrTumbol").ToString()
            Dim _FTAddrAmphur As String = ogv.GetFocusedRowCellValue("FTAddrAmphur").ToString()
            Dim _FTAddrProvince As String = ogv.GetFocusedRowCellValue("FTAddrProvince").ToString()
            Dim _FTAddrPostCode As String = ogv.GetFocusedRowCellValue("FTAddrPostCode").ToString()
            Dim _FTAddrTel As String = ogv.GetFocusedRowCellValue("FTAddrTel").ToString()

            Dim _FTAddrNo1 As String = ogv.GetFocusedRowCellValue("FTAddrNo1").ToString()
            Dim _FTAddrHome1 As String = ogv.GetFocusedRowCellValue("FTAddrHome1").ToString()
            Dim _FTAddrMoo1 As String = ogv.GetFocusedRowCellValue("FTAddrMoo1").ToString()
            Dim _FTAddrSoi1 As String = ogv.GetFocusedRowCellValue("FTAddrSoi1").ToString()
            Dim _FTAddrRoad1 As String = ogv.GetFocusedRowCellValue("FTAddrRoad1").ToString()
            Dim _FTAddrTumbol1 As String = ogv.GetFocusedRowCellValue("FTAddrTumbol1").ToString()
            Dim _FTAddrAmphur1 As String = ogv.GetFocusedRowCellValue("FTAddrAmphur1").ToString()
            Dim _FTAddrProvince1 As String = ogv.GetFocusedRowCellValue("FTAddrProvince1").ToString()
            Dim _FTAddrPostCode1 As String = ogv.GetFocusedRowCellValue("FTAddrPostCode1").ToString()
            Dim _FTAddrTel1 As String = ogv.GetFocusedRowCellValue("FTAddrTel1").ToString()


            Dim _FTEmpNameTH As String = ogv.GetFocusedRowCellValue("FTEmpNameTH").ToString()
            Dim _FTEmpSurnameTH As String = ogv.GetFocusedRowCellValue("FTEmpSurnameTH").ToString()










            With _wConfigPayRollMonthly_LA_Popup

                ''.FNHSysAccountId = ""
                '.FTAccountCode.Text = ""
                '.FTAccountNameTH.Text = ""
                '.FTAccountNameEN.Text = ""

                .FNHSysEmpID.Properties.Tag = ""
                .FNHSysEmpID.Text = ""
                .FNHSysEmpID.Text = ""


                .FTStateActive.Checked = True
                '.FNHSysAccountId = FNHSysAccountId
                '.FTAccountCode.Text = FTAccountCode
                '.FTAccountNameTH.Text = FTAccountNameTH
                '.FTAccountNameEN.Text = FTAccountNameEN

                If _FTStateActive = True Then
                    .FTStateActive.Checked = True
                Else
                    .FTStateActive.Checked = False

                End If


                .FNHSysEmpID_edit = _FNHSysEmpID

                .FNHSysEmpID.Properties.Tag = _FNHSysEmpID
                .FNHSysEmpID.Text = _FTEmpCode
                .FNHSysEmpID_None.Text = _FTEmpNameLocal

                .FNSalary.Text = Val(_FNSalary)
                .FCOt1_Baht.Text = Val(_FCOt1_Baht)
                .FNTotalIncome.Text = Val(_FNTotalIncome)
                .FNTotalRecalSSO.Text = Val(_FNTotalRecalSSO)
                .FNSocial.Text = Val(_FNSocial)
                .FNSocialCmp.Text = Val(_FNSocialCmp)
                .FNTotalRecalTAX.Text = Val(_FNTotalRecalTAX)
                .FNTax.Text = Val(_FNTax)
                .FNNetpay.Text = Val(_FNNetpay)

                .FTSocialNo_TH.Text = _FTSocialNo_TH
                .FTTaxNo_TH.Text = _FTTaxNo_TH

                .FTAddrNo.Text = _FTAddrNo
                .FTAddrHome.Text = _FTAddrHome
                .FTAddrMoo.Text = _FTAddrMoo
                .FTAddrSoi.Text = _FTAddrSoi
                .FTAddrRoad.Text = _FTAddrRoad
                .FTAddrTumbol.Text = _FTAddrTumbol
                .FTAddrAmphur.Text = _FTAddrAmphur
                .FTAddrProvince.Text = _FTAddrProvince
                .FTAddrPostCode.Text = _FTAddrPostCode
                .FTAddrTel.Text = _FTAddrTel

                .FTAddrNo1.Text = _FTAddrNo1
                .FTAddrHome1.Text = _FTAddrHome1
                .FTAddrMoo1.Text = _FTAddrMoo1
                .FTAddrSoi1.Text = _FTAddrSoi1
                .FTAddrRoad1.Text = _FTAddrRoad1
                .FTAddrTumbol1.Text = _FTAddrTumbol1
                .FTAddrAmphur1.Text = _FTAddrAmphur1
                .FTAddrProvince1.Text = _FTAddrProvince1
                .FTAddrPostCode1.Text = _FTAddrPostCode1
                .FTAddrTel1.Text = _FTAddrTel1

                .FTEmpnameTH.Text = _FTEmpNameTH
                .FTEmpSurnameTH.Text = _FTEmpSurnameTH


                .ShowDialog()

                LoadData()

            End With

        Catch ex As Exception
        End Try
    End Sub



    Private Sub wConfigIncentiveHeader_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub

#End Region

End Class