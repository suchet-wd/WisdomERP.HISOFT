'Imports DevExpress.Data
Imports DevExpress.XtraGrid.Columns

Public Class wAccount
    Private _wAccountPopup As wAccountPopup

#Region " Procedure "

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        InitGrid()
        _wAccountPopup = New wAccountPopup
        HI.TL.HandlerControl.AddHandlerObj(_wAccountPopup)

        Dim oSysLang As New ST.SysLanguage
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wAccountPopup.Name.ToString.Trim, _wAccountPopup)
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
            _Qry = " SELECT FNHSysAccountId, MA.FTAccountCode, MA.FTAccountNameTH , MA.FTAccountNameEN  "
            _Qry &= vbCrLf & " , FTStateActive, FTPOStateActive, FTOverHeadStateActive,  FTParentState,  FTPIStateActive "
            _Qry &= vbCrLf & " , MA.FNHSysAccountGroupId "
            _Qry &= vbCrLf & ", A.FTAccountCode AS FTAccountGroupCode "
            _Qry &= vbCrLf & " , A.FTAccountNameTH AS FTAccountGroupName "
            _Qry &= vbCrLf & "     FROM     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMAccount MA "
            _Qry &= vbCrLf & " OUTER APPLY (SELECT TOP 1 FTAccountCode, FTAccountNameTH FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMAccount A WHERE MA.FNHSysAccountGroupId=A.FNHSysAccountId AND FTParentState = '1') A"
            _Qry &= vbCrLf & " "

        Else
            _Qry = " SELECT FNHSysAccountId, MA.FTAccountCode,  MA.FTAccountNameEN  ,MA.FTAccountNameEN  "
            _Qry &= vbCrLf & " , FTStateActive, FTPOStateActive, FTOverHeadStateActive,  FTParentState,  FTPIStateActive "
            _Qry &= vbCrLf & " , MA.FNHSysAccountGroupId "
            _Qry &= vbCrLf & ", A.FTAccountCode AS FTAccountGroupCode "
            _Qry &= vbCrLf & " , A.FTAccountNameEN AS FTAccountGroupName "
            _Qry &= vbCrLf & "     FROM     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMAccount MA "
            _Qry &= vbCrLf & " OUTER APPLY (SELECT TOP 1 FTAccountCode, FTAccountNameEN FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMAccount A WHERE MA.FNHSysAccountGroupId=A.FNHSysAccountId AND FTParentState = '1') A"
            _Qry &= vbCrLf & " "
        End If

        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        ogc.DataSource = _Dt
        Call InitialGridOrganizeMergCell()
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

            With _wAccountPopup
                .ShowDialog()
                LoadData()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Ogvorganize_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles ogv.RowCellClick

    End Sub



    Private Sub Ogvorganize_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogv.CellMerge
        Try
            With Me.ogv
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

        For Each c As GridColumn In ogv.Columns

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

    Private Sub ogvorganize_DoubleClick(sender As Object, e As EventArgs) Handles ogv.DoubleClick
        Try
            Dim FNHSysAccountId As String = ogv.GetFocusedRowCellValue("FNHSysAccountId").ToString()
            Dim FTAccountCode As String = ogv.GetFocusedRowCellValue("FTAccountCode").ToString()
            Dim FTAccountNameTH As String = ogv.GetFocusedRowCellValue("FTAccountNameTH").ToString()
            Dim FTAccountNameEN As String = ogv.GetFocusedRowCellValue("FTAccountNameEN").ToString()


            Dim FTStateActive As String = ogv.GetFocusedRowCellValue("FTStateActive").ToString()
            Dim FTPOStateActive As String = ogv.GetFocusedRowCellValue("FTPOStateActive").ToString()
            Dim FTOverHeadStateActive As String = ogv.GetFocusedRowCellValue("FTOverHeadStateActive").ToString()
            Dim FTParentState As String = ogv.GetFocusedRowCellValue("FTParentState").ToString()
            Dim FTPIStateActive As String = ogv.GetFocusedRowCellValue("FTPIStateActive").ToString()


            Dim FTAccountGroupCode As String = ogv.GetFocusedRowCellValue("FTAccountGroupCode").ToString()
            Dim FNHSysAccountGroupId As String = ogv.GetFocusedRowCellValue("FNHSysAccountGroupId").ToString()
            Dim FTAccountGroupName As String = ogv.GetFocusedRowCellValue("FTAccountGroupName").ToString()

            ''  Dim FNSeq As Double = Format(Val(ogv.GetFocusedRowCellValue("FNSeq").ToString()), "###0.00")

            With _wAccountPopup

                .FNHSysAccountId = ""
                .FTAccountCode.Text = ""
                .FTAccountNameTH.Text = ""
                .FTAccountNameEN.Text = ""

                .FNHSysAccountGroupId.Properties.Tag = ""
                .FNHSysAccountGroupId.Text = ""
                .FNHSysAccountGroupId.Text = ""


                .FTStateActive.Checked = True
                .FNHSysAccountId = FNHSysAccountId
                .FTAccountCode.Text = FTAccountCode
                .FTAccountNameTH.Text = FTAccountNameTH
                .FTAccountNameEN.Text = FTAccountNameEN

                If FTStateActive = True Then
                    .FTStateActive.Checked = True
                Else
                    .FTStateActive.Checked = False

                End If

                If FTPOStateActive = True Then
                    .FTPOStateActive.Checked = True
                Else
                    .FTPOStateActive.Checked = False

                End If
                If FTOverHeadStateActive = True Then
                    .FTOverHeadStateActive.Checked = True
                Else
                    .FTOverHeadStateActive.Checked = False

                End If
                If FTParentState = True Then
                    .FTParentState.Checked = True
                Else
                    .FTParentState.Checked = False

                End If
                If FTPIStateActive = True Then
                    .FTPIStateActive.Checked = True
                Else
                    .FTPIStateActive.Checked = False

                End If

                .FNHSysAccountGroupId.Properties.Tag = FNHSysAccountGroupId
                .FNHSysAccountGroupId.Text = FTAccountGroupCode
                .FNHSysAccountGroupId_None.Text = FTAccountGroupName

                '' .FNSeq.Text = FNSeq

                .ShowDialog()

                LoadData()

            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub wAccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call LoadData()
    End Sub

#End Region

End Class