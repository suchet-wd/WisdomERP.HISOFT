'Imports DevExpress.Data
Imports DevExpress.XtraGrid.Columns

Public Class wConfigIncentiveHeader
    Private _wConfigIncentiveHeaderPopup As wConfigIncentiveHeaderPopup

#Region " Procedure "

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        InitGrid()
        _wConfigIncentiveHeaderPopup = New wConfigIncentiveHeaderPopup
        HI.TL.HandlerControl.AddHandlerObj(_wConfigIncentiveHeaderPopup)

        Dim oSysLang As New ST.SysLanguage
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wConfigIncentiveHeaderPopup.Name.ToString.Trim, _wConfigIncentiveHeaderPopup)
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


        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry = " SELECT H.FNHSysPositId,  P.FTPositCode  "
            _Qry &= vbCrLf & " ,P.FTPositNameTH  ,P.FTPositNameEN "
            _Qry &= vbCrLf & " , H.FNHSysCmpId, FDDateFrom, FDDateTo, FNINcentiveMultiple ,H.FTStateActive "

            _Qry &= vbCrLf & "     FROM     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo. TPRODMIncentiveHeader H "
            _Qry &= vbCrLf & " LEFT JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMPosition P ON H.FNHSysPositId=P.FNHSysPositId  AND  H.FNHSysCmpId= P.FNHSysCmpId "
            _Qry &= vbCrLf & " "

        Else
            _Qry = " SELECT H.FNHSysPositId,  P.FTPositCode  "
            _Qry &= vbCrLf & " ,P.FTPositNameEN ,P.FTPositNameEN  "
            _Qry &= vbCrLf & " , H.FNHSysCmpId, FDDateFrom, FDDateTo, FNINcentiveMultiple ,H.FTStateActive "

            _Qry &= vbCrLf & "     FROM     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo. TPRODMIncentiveHeader H "
            _Qry &= vbCrLf & " LEFT JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMPosition P ON H.FNHSysPositId=P.FNHSysPositId  AND  H.FNHSysCmpId= P.FNHSysCmpId "
            _Qry &= vbCrLf & " "
        End If

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

            With _wConfigIncentiveHeaderPopup

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

    Private Sub ogvorganize_DoubleClick(sender As Object, e As EventArgs) Handles ogv.DoubleClick
        Try
            Dim FNHSysPositId As String = ogv.GetFocusedRowCellValue("FNHSysPositId").ToString()
            Dim FTPositCode As String = ogv.GetFocusedRowCellValue("FTPositCode").ToString()
            Dim FTPositNameTH As String = ogv.GetFocusedRowCellValue("FTPositNameTH").ToString()
            Dim FTPositNameEN As String = ogv.GetFocusedRowCellValue("FTPositNameEN").ToString()


            Dim FTStateActive As String = ogv.GetFocusedRowCellValue("FTStateActive").ToString()

            Dim FNHSysCmpId As String = ogv.GetFocusedRowCellValue("FNHSysCmpId").ToString()
            Dim FDDateFrom As String = ogv.GetFocusedRowCellValue("FDDateFrom").ToString()
            Dim FDDateTo As String = ogv.GetFocusedRowCellValue("FDDateTo").ToString()
            Dim FNINcentiveMultiple As String = ogv.GetFocusedRowCellValue("FNINcentiveMultiple").ToString()



            With _wConfigIncentiveHeaderPopup

                ''.FNHSysAccountId = ""
                '.FTAccountCode.Text = ""
                '.FTAccountNameTH.Text = ""
                '.FTAccountNameEN.Text = ""

                .FNHSysPositId.Properties.Tag = ""
                .FNHSysPositId.Text = ""
                .FNHSysPositId.Text = ""


                .FTStateActive.Checked = True
                '.FNHSysAccountId = FNHSysAccountId
                '.FTAccountCode.Text = FTAccountCode
                '.FTAccountNameTH.Text = FTAccountNameTH
                '.FTAccountNameEN.Text = FTAccountNameEN

                If FTStateActive = True Then
                    .FTStateActive.Checked = True
                Else
                    .FTStateActive.Checked = False

                End If


                .FNHSysPositId.Properties.Tag = FNHSysPositId
                .FNHSysPositId.Text = FTPositCode
                .FNHSysPositId_None.Text = FTPositNameTH

                '' .FNSeq.Text = FNSeq

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