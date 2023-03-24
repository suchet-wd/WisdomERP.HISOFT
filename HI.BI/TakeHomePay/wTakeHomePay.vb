
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Grid

Public Class wTakeHomePay



    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _ActualDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),GETDATE(),111)", Conn.DB.DataBaseName.DB_HR, "")
        _ActualNextDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),DateAdd(day,1,GETDATE()),111)", Conn.DB.DataBaseName.DB_HR, "")

        Call InitGrid()

    End Sub

#Region "Initial Grid"

    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        'Dim sFieldCount As String = "FTEmpCode"
        'Dim sFieldSum As String = ""
        'Dim sFieldGrpCount As String = ""
        'Dim sFieldGrpSum As String = ""

        'With ogv
        '    .ClearGrouping()
        '    .ClearDocument()

        '    For Each Str As String In sFieldCount.Split("|")
        '        If Str <> "" Then
        '            .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
        '            .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
        '        End If
        '    Next

        '    For Each Str As String In sFieldSum.Split("|")
        '        If Str <> "" Then
        '            .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
        '            .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
        '        End If
        '    Next

        '    For Each Str As String In sFieldGrpCount.Split("|")
        '        If Str <> "" Then
        '            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
        '        End If
        '    Next

        '    For Each Str As String In sFieldGrpSum.Split("|")
        '        If Str <> "" Then
        '            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
        '        End If
        '    Next

        '    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        '    .OptionsView.ShowFooter = True

        'End With
        ''------End Add Summary Grid-------------
    End Sub
#End Region

#Region "Property"

    Private _ActualDate As String = ""
    ReadOnly Property ActualDate As String
        Get
            Return _ActualDate
        End Get
    End Property

    Private _ActualNextDate As String = ""
    ReadOnly Property ActualNextDate As String
        Get
            Return _ActualNextDate
        End Get
    End Property

    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(value As String)
            _CallMethodParm = value
        End Set
    End Property

#End Region

#Region "MAIN PROC"

    Private Sub ProcessSave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsave.Click


    End Sub

    Private Sub ProcessClear(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclear.Click

        HI.TL.HandlerControl.ClearControl(Me)
        Me.FTStartDate.Focus()

    End Sub

    Private Sub ProcessClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click

        ' C1FlexGrid1.SaveExcel("Text.xls", C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells, C1.Win.C1FlexGrid.FileFlags.VisibleOnly)
        Me.Close()
    End Sub

#End Region

#Region " Procedure "

    Private Function SaveData(Spls As HI.TL.SplashScreen) As Boolean



    End Function


    Private Function VerrifyData() As Boolean
        Dim _Pass As Boolean = False
        'If HI.UL.ULDate.CheckDate(FTDateRequest.Text) <> "" Then
        '    If Not (ogc.DataSource Is Nothing) Then
        '        CType(ogc.DataSource, DataTable).AcceptChanges()
        '        If CType(ogc.DataSource, DataTable).Rows.Count > 0 Then
        '            If CType(ogc.DataSource, DataTable).Select("FTSelect='1'").Length > 0 Then



        '                If Me.otba1starttime.Text = Me.otba1endtime.Text Then
        '                    Me.otba1starttime.Text = ""
        '                    Me.otba1endtime.Text = ""
        '                End If

        '                If Me.otba2starttime.Text = Me.otba2endtime.Text Then
        '                    Me.otba2starttime.Text = ""
        '                    Me.otba2endtime.Text = ""
        '                End If

        '                If (Me.otba1starttime.Text <> "" And Me.otba1endtime.Text <> "") Or
        '                        (Me.otba2starttime.Text <> "" And Me.otba2endtime.Text <> "") Or (Me.FTStateDaily.Checked) Then

        '                    _Pass = True

        '                Else

        '                    HI.MG.ShowMsg.mInvalidData("กรุณาทำการเวลาขอโอที", 1304030002, Me.Text)
        '                End If

        '            Else
        '                HI.MG.ShowMsg.mInvalidData("กรุณาทำการเลือกพนักงาน", 1304030001, Me.Text)
        '                FTStartDate.Focus()
        '            End If
        '        Else
        '            HI.MG.ShowMsg.mInvalidData("กรุณาทำการเลือกพนักงาน", 1304030001, Me.Text)
        '            FTStartDate.Focus()
        '        End If
        '    Else
        '        HI.MG.ShowMsg.mInvalidData("กรุณาทำการเลือกพนักงาน", 1304030001, Me.Text)
        '        FTStartDate.Focus()
        '    End If
        'Else
        '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTDateRequest_lbl.Text)
        '    FTStartDate.Focus()
        'End If

        Return _Pass
    End Function

    Private Sub LoadDataInfo()
        Dim _Qry As String = ""
        Dim dt As New DataTable
        Dim dtline As New DataTable
        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")
        Me.otb.TabPages.Clear()
        Dim _TotalLine As Integer = 0
        Dim _PLine As Integer = 0
        Dim _DisplayLang As String = "TH"


        If HI.ST.Lang.Language <> ST.Lang.eLang.TH Then
            _DisplayLang = "EN"
        End If

        _Qry = "SELECT FNHSysUnitSectId, FTUnitSectCode"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS X WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE  (FTStateSew = '1') AND (FTStateActive = '1')"

        If FNHSysUnitSectId.Text <> "" Then
            _Qry &= vbCrLf & "  AND FTUnitSectCode >='" & HI.UL.ULF.rpQuoted(FNHSysUnitSectId.Text) & "'"
        End If

        If FNHSysUnitSectIdTo.Text <> "" Then
            _Qry &= vbCrLf & "  AND FTUnitSectCode <='" & HI.UL.ULF.rpQuoted(FNHSysUnitSectIdTo.Text) & "'"
        End If

        _Qry &= vbCrLf & "  ORDER BY FTUnitSectCode"
        dtline = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)
        _TotalLine = dtline.Rows.Count

        For Each Rx As DataRow In dtline.Rows
            _PLine = _PLine + 1

            _Spls.UpdateInformation("Loading Data of Line " & Rx!FTUnitSectCode.ToString & "  ( " & _PLine.ToString & " of  " & _TotalLine.ToString & " )")

            _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_GET_CALCULATE_TAKE_HOME_PAY " & Val(Rx!FNHSysUnitSectId.ToString) & ",'" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) & "','" & HI.UL.ULF.rpQuoted(_DisplayLang) & "' "
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)


            Dim Otp As New DevExpress.XtraTab.XtraTabPage()
            With Otp
                .Name = "T" & Rx!FNHSysUnitSectId.ToString
                .Text = Rx!FTUnitSectCode.ToString
                .Tag = "2|"
            End With
            Dim oPg As New UITakeHomePayRawData(dt.Copy, Val(Rx!FNHSysUnitSectId.ToString), Rx!FTUnitSectCode.ToString)


            Otp.Controls.Add(oPg)
            oPg.Dock = System.Windows.Forms.DockStyle.Fill
            otb.TabPages.Add(Otp)
            oPg.ogvdetail.OptionsView.ShowAutoFilterRow = True

        Next

        _Spls.Close()

        dtline.Dispose()
        dt.Dispose()
    End Sub
#End Region

#Region "General"

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If HI.UL.ULDate.CheckDate(FTStartDate.Text) <> "" And HI.UL.ULDate.CheckDate(FTEndDate.Text) <> "" Then
            Call LoadDataInfo()

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTStartDate_lbl.Text)
            FTStartDate.Focus()
        End If
    End Sub

    Private Sub wOTRequest_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
    End Sub

#End Region

    Private Sub ocmexporttoexcel_Click(sender As Object, e As EventArgs) Handles ocmexporttoexcel.Click

    End Sub
End Class