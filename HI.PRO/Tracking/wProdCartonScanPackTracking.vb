﻿Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns

Public Class wProdCartonScanPackTracking

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "FNTotalFullCartonQty|FNTotalScarpCartonQty|FNTotalScanQuantity|FNTotalCarton|FNTotalFullCarton|FNTotalScarpCarton"

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = "FNTotalFullCartonQty|FNTotalScarpCartonQty|FNTotalScanQuantity|FNTotalCarton|FNTotalFullCarton|FNTotalScarpCarton"


        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        Try
            With ogvtime
                .ClearGrouping()
                .ClearDocument()
                '.Columns("FTDateTrans").Group()

                For Each Str As String In sFieldCount.Split("|")
                    If Str <> "" Then
                        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                    End If
                Next

                For Each Str As String In sFieldCustomSum.Split("|")
                    If Str <> "" Then
                        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
                        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                    End If
                Next

                For Each Str As String In sFieldSum.Split("|")
                    If Str <> "" Then
                        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                    End If
                Next

                For Each Str As String In sFieldGrpCount.Split("|")
                    If Str <> "" Then
                        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                    End If
                Next

                For Each Str As String In sFieldCustomGrpSum.Split("|")
                    If Str <> "" Then
                        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                    End If
                Next

                For Each Str As String In sFieldGrpSum.Split("|")
                    If Str <> "" Then
                        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                    End If
                Next

                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                .OptionsView.ShowFooter = True
                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

                .ExpandAllGroups()
                .RefreshData()

            End With

        Catch ex As Exception
        End Try

        sFieldCount = ""
        sFieldSum = "FNScanQuantity"

        sFieldGrpCount = ""
        sFieldGrpSum = "FNScanQuantity"

        Try
            With ogvdetailcolorsize
                .ClearGrouping()
                .ClearDocument()
                '.Columns("FTDateTrans").Group()

                For Each Str As String In sFieldCount.Split("|")
                    If Str <> "" Then
                        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                    End If
                Next

                For Each Str As String In sFieldCustomSum.Split("|")
                    If Str <> "" Then
                        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
                        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                    End If
                Next

                For Each Str As String In sFieldSum.Split("|")
                    If Str <> "" Then
                        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                    End If
                Next

                For Each Str As String In sFieldGrpCount.Split("|")
                    If Str <> "" Then
                        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                    End If
                Next

                For Each Str As String In sFieldCustomGrpSum.Split("|")
                    If Str <> "" Then
                        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                    End If
                Next

                For Each Str As String In sFieldGrpSum.Split("|")
                    If Str <> "" Then
                        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                    End If
                Next

                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                .OptionsView.ShowFooter = True
                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

                .ExpandAllGroups()
                .RefreshData()

            End With
        Catch ex As Exception

        End Try

        '------End Add Summary Grid-------------
    End Sub
#End Region

#Region "Custom summaries"

    Private totalSum As Integer = 0
    Private GrpSum As Integer = 0

    Private Sub InitStartValue()
        totalSum = 0
        GrpSum = 0
    End Sub

    'Private Sub ogv_CustomDrawGroupRow(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs) Handles ogv.CustomDrawGroupRow

    '    Dim info As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo = e.Info
    '    Dim Handle As Integer = ogv.GetDataRowHandleByGroupRowHandle(e.RowHandle)

    '    'Select Case info.Column.FieldName.ToString.ToUpper
    '    '    Case "FNWorkingDay"

    '    Dim GrpDisplayText As String = ogv.GetGroupSummaryText(e.RowHandle)  'ogv.GetGroupRowValue(e.RowHandle, info.Column)
    '    Dim GrpDisplayTextReplace As String = Nothing
    '    Dim GrpDisplayTextReplaceNew As String = Nothing
    '    GrpDisplayTextReplace = GrpDisplayText.Split(")")(1)

    '    If GrpDisplayTextReplace <> "" Then
    '        If GrpDisplayTextReplace.Split("=").Length >= 2 Then
    '            Dim Title1 As String = GrpDisplayTextReplace.Split("=")(0)
    '            Dim Title2 As String = GrpDisplayTextReplace.Split("=")(1)

    '            If IsNumeric(Title2) = False Then
    '                Title2 = "0"
    '            End If
    '            Dim _Sum As Integer = CDbl(Title2)
    '            Dim NetDisplay As String = ""
    '            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
    '                NetDisplay = Format((_Sum \ 480), "00") & " วัน : " & Format(((_Sum Mod 480) \ 60), "00") & " ชั่วโมง : " & Format(((_Sum Mod 480) Mod 60), "00") & " นาที"
    '            Else
    '                NetDisplay = Format((_Sum \ 480), "00") & " Day : " & Format(((_Sum Mod 480) \ 60), "00") & " Hour : " & Format(((_Sum Mod 480) Mod 60), "00") & " Minute"
    '            End If

    '            GrpDisplayTextReplaceNew = Title1 & "=" & NetDisplay
    '            GrpDisplayText = GrpDisplayText.Replace(GrpDisplayTextReplace, GrpDisplayTextReplaceNew)
    '        End If


    '    info.GroupText = info.Column.Caption + ":" + info.GroupValueText + ""
    '    info.GroupText += "" + GrpDisplayText + ""

    '    'End Select

    'End Sub

#End Region

#Region "Property"

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


#End Region

#Region "Procedure"

    Private Sub LoadData()

        ogdtime.DataSource = Nothing
        ogcdetailcolorsize.DataSource = Nothing

        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _dt As DataTable
        Dim _TotalRow As Integer = 0
        Dim _Rx As Integer = 0

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Try

            _Qry = "SELECT   op.FDInsDate AS 'FDInsDate'"
            _Qry &= vbCrLf & ", op.FTCustomerPO AS 'FTCustomerPO'"
            _Qry &= vbCrLf & ", S.FTStyleCode AS 'FTStyleCode'"
            _Qry &= vbCrLf & ", opd.FTOrderNo AS 'FTOrderNo'"
            _Qry &= vbCrLf & ", SUM(1) AS 'FNTotalCarton'"
            _Qry &= vbCrLf & ", SUM(Case When (opd.FNPackPerCarton = opd.FNQuantity) Then 1 Else 0 End) As 'FNTotalFullCarton'"
            _Qry &= vbCrLf & ", SUM(CASE WHEN (opd.FNPackPerCarton <> opd.FNQuantity) THEN 1 ELSE 0 END) AS 'FNTotalScarpCarton'"
            _Qry &= vbCrLf & ", SUM(Case When (opd.FNPackPerCarton = opd.FNQuantity) Then opd.FNQuantity Else 0 End) As 'FNTotalFullCartonQty'"
            _Qry &= vbCrLf & ", SUM(Case When (opd.FNPackPerCarton <> opd.FNQuantity) Then opd.FNQuantity Else 0 End) As 'FNTotalScarpCartonQty'"
            _Qry &= vbCrLf & ", SUM(opd.FNQuantity) As FNTotalScanQuantity"

            _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack AS op WITH (NOLOCK) "

            _Qry &= vbCrLf & "OUTER APPLY(SELECT * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS opd  WITH (NOLOCK)"
            _Qry &= vbCrLf & "WHERE opd.FTPackNo = op.FTPackNo) AS opd"

            _Qry &= vbCrLf & "OUTER APPLY(SELECT TOP 1 S.FTStyleCode, S.FTStyleNameEN, S.FTStyleNameTH "
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S  WITH (NOLOCK)"
            _Qry &= vbCrLf & "WHERE S.FNHSysStyleId = op.FNHSysStyleId) AS S"

            _Qry &= vbCrLf & "WHERE "
            _Qry &= vbCrLf & "(op.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & ") "
            '_Qry &= vbCrLf & "AND (op.FTPackNo BETWEEN 'C2PAC-1807250002' AND 'C2PAC-1807250008') "

            If FTOrderNo.Text <> "" And FTOrderNoTo.Text <> "" Then
                _Qry &= vbCrLf & "AND (opd.FTOrderNo BETWEEN '" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' AND '" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "') "
            End If

            If FTSubOrderNo.Text <> "" And FTSubOrderNoTo.Text <> "" Then
                _Qry &= vbCrLf & "AND (opd.FTSubOrderNo BETWEEN '" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "' AND '" & HI.UL.ULF.rpQuoted(FTSubOrderNoTo.Text) & "') "
            End If

            If FDDate.Text <> "" And FDDateTo.Text <> "" Then
                _Qry &= vbCrLf & "AND (op.FDPackDate BETWEEN '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "' AND '" & HI.UL.ULDate.ConvertEnDB(FDDateTo.Text) & "') "
            End If

            If FNHSysStyleId.Text <> "" And FNHSysStyleIdTo.Text <> "" Then
                _Qry &= vbCrLf & "AND (op.FNHSysStyleId BETWEEN '" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "' AND '" & HI.UL.ULF.rpQuoted(FNHSysStyleIdTo.Text) & "') "
            End If

            If FNHSysPOID.Text <> "" And FNHSysPOIDTo.Text <> "" Then
                _Qry &= vbCrLf & "AND(op.FTCustomerPO BETWEEN '" & HI.UL.ULF.rpQuoted(FNHSysPOID.Text) & "' AND '" & HI.UL.ULF.rpQuoted(FNHSysPOIDTo.Text) & "') "
            End If

            _Qry &= vbCrLf & "GROUP BY op.FDInsDate, op.FTCustomerPO, S.FTStyleCode, opd.FTOrderNo "
            _Qry &= vbCrLf & "ORDER BY op.FDInsDate DESC"

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)


            Me.ogdtime.DataSource = _dt
            Me.ogvtime.BestFitColumns()
            Call LoaddataDetailColorSize()

        Catch ex As Exception
        End Try

        _Spls.Close()


    End Sub

    Private Sub LoaddataDetailColorSize()
        Dim _Qry As String
        Dim _dt As DataTable
        ogcdetailcolorsize.DataSource = Nothing
        Try
            _Qry = "Select ISNULL(pc.FTUpdUser, pc.FTInsUser) AS 'FTScanByClose' "
            _Qry &= vbCrLf & ", CASE WHEN ISDATE(pc.FDInsDate) = 1 Then Convert(Date,pc.FDInsDate)  ELSE NULL END AS 'FTScanDateClose' "
            _Qry &= vbCrLf & ", pc.FTInsTime AS 'FTScanTimeClose' "
            _Qry &= vbCrLf & ", ISNULL(csd.FTInsUser, '') AS 'FTScanByStart' "
            _Qry &= vbCrLf & ", CASE WHEN ISDATE(csd.MnIns) = 1 Then Convert(Date,csd.MnIns) ELSE NULL END AS 'FTScanDateStart' "
            _Qry &= vbCrLf & ", CASE WHEN ISDATE(csd.MnIns) = 1 Then Convert(Time,csd.MnIns) ELSE NULL END AS 'FTScanTimeStart' "
            _Qry &= vbCrLf & ", csd2.MXUser AS 'FTScanByEnd' "
            _Qry &= vbCrLf & ", CASE WHEN ISDATE(ISNULL(csd.MxUpd,csd.MxIns)) = 1 Then Convert(Date,ISNULL(csd.MxUpd,csd.MxIns)) ELSE NULL END AS 'FTScanDateEnd' "
            _Qry &= vbCrLf & ", CASE WHEN ISDATE(ISNULL(csd.MxUpd,csd.MxIns)) = 1 Then Convert(Time,ISNULL(csd.MxUpd,csd.MxIns)) ELSE NULL END AS 'FTScanTimeEnd' "
            _Qry &= vbCrLf & ", cs.FTPackNo AS 'FTPackNo' "
            _Qry &= vbCrLf & ", cs.FNCartonNo AS 'FNCartonNo' "
            _Qry &= vbCrLf & ", op.FTCustomerPO AS 'FTCustomerPO' "
            _Qry &= vbCrLf & ", cs.FTOrderNo AS 'FTOrderNo' "
            _Qry &= vbCrLf & ", cs.FTSubOrderNo AS 'FTSubOrderNo' "
            _Qry &= vbCrLf & ", cs.Qty AS 'FNScanQuantity' "

            _Qry &= vbCrLf
            _Qry &= vbCrLf & "From [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) + "].dbo.TPACKOrderPack AS op WITH (NOLOCK) "
            _Qry &= vbCrLf
            _Qry &= vbCrLf & "OUTER APPLY(SELECT cs.FTOrderNo, cs.FTSubOrderNo "
            _Qry &= vbCrLf & ", cs.FTPackNo, cs.FNCartonNo, SUM(cs.FNScanQuantity) AS Qty "

            _Qry &= vbCrLf & "FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) + "].dbo.TPACKOrderPack_Carton_Scan AS cs  With (NOLOCK) "
            _Qry &= vbCrLf & "WHERE cs.FTPackNo = op.FTPackNo "
            _Qry &= vbCrLf & "GROUP BY cs.FTOrderNo, cs.FTSubOrderNo , cs.FTPackNo, cs.FNCartonNo "
            _Qry &= vbCrLf & ") AS cs "
            _Qry &= vbCrLf
            _Qry &= vbCrLf & "OUTER APPLY(SELECT TOP 1 pc.FDInsDate, pc.FTInsTime, pc.FTInsUser, pc.FTUpdUser "
            _Qry &= vbCrLf & "FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) + "].dbo.TPACKCarton AS pc  With (NOLOCK)"
            _Qry &= vbCrLf & "WHERE(pc.FTPackNo = cs.FTPackNo) And (pc.FNCartonNo = cs.FNCartonNo) And pc.FTState = '1') AS pc "
            _Qry &= vbCrLf
            _Qry &= vbCrLf & "OUTER APPLY(SELECT DISTINCT csd.FTInsUser "
            _Qry &= vbCrLf & ", MIN(csd.FDInsDate + ' ' + csd.FTInsTime) AS MnIns "
            _Qry &= vbCrLf & ", MAX(csd.FDInsDate + ' ' + csd.FTInsTime) AS MxIns "
            _Qry &= vbCrLf & ", MIN(csd.FDUpdDate + ' ' + csd.FTUpdTime) AS MnUpd "
            _Qry &= vbCrLf & ", MAX(csd.FDUpdDate + ' ' + csd.FTUpdTime) AS MxUpd "
            _Qry &= vbCrLf & "From [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) + "].dbo.TPACKOrderPack_Carton_Scan_Detail AS csd  With (NOLOCK) "
            _Qry &= vbCrLf & "Where csd.FTPackNo = cs.FTPackNo And csd.FNCartonNo = cs.FNCartonNo "
            _Qry &= vbCrLf & " GROUP BY csd.FTInsUser  ) AS csd "
            _Qry &= vbCrLf
            _Qry &= vbCrLf & "OUTER APPLY(SELECT DISTINCT ISNULL(csd2.FTUpdUser,csd2.FTInsUser)  AS MXUser "
            _Qry &= vbCrLf & "From [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) + "].dbo.TPACKOrderPack_Carton_Scan_Detail AS csd2  With (NOLOCK) "
            _Qry &= vbCrLf & "Where csd2.FTPackNo = cs.FTPackNo And csd2.FNCartonNo = cs.FNCartonNo "
            _Qry &= vbCrLf & "And (ISNULL(csd.MxUpd, csd.MnIns) = ISNULL((csd2.FDUpdDate + ' ' + csd2.FTUpdTime), (csd2.FDInsDate + ' ' + csd2.FTInsTime))) ) AS csd2 "
            '_Qry &= vbCrLf & "OUTER APPLY(SELECT  MAX(ISNULL(csd.FDUpdDate + ' '+ csd.FTUpdTime, csd.FDInsDate + ' '+ csd.FTInsTime)) AS 'Mx' "
            '_Qry &= vbCrLf & ", MIN(csd.FDInsDate + ' ' + csd.FTInsTime) AS 'Mn' "
            '_Qry &= vbCrLf & ", csd.FTPackNo, csd.FNCartonNo "
            '_Qry &= vbCrLf & "From [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) + "].dbo.TPACKOrderPack_Carton_Scan_Detail AS csd  With (NOLOCK) "
            '_Qry &= vbCrLf & "Where csd.FTPackNo = cs.FTPackNo And csd.FNCartonNo = cs.FNCartonNo "
            '_Qry &= vbCrLf
            '_Qry &= vbCrLf & "GROUP BY csd.FTPackNo, csd.FNCartonNo"
            '_Qry &= vbCrLf & ") AS csd  "

            _Qry &= vbCrLf
            _Qry &= vbCrLf & "WHERE (op.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & ") AND (pc.FTInsUser <> '') "

            'If chkShow.Checked = True Then            '    _Qry &= vbCrLf & ""            'End If

            If FTOrderNo.Text <> "" And FTOrderNoTo.Text <> "" Then
                _Qry &= vbCrLf & "AND (cs.FTOrderNo BETWEEN '" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' AND '" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "') "
            End If

            If FTSubOrderNo.Text <> "" And FTSubOrderNoTo.Text <> "" Then
                _Qry &= vbCrLf & "AND (cs.FTSubOrderNo BETWEEN '" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "' AND '" & HI.UL.ULF.rpQuoted(FTSubOrderNoTo.Text) & "') "
            End If

            If FDDate.Text <> "" And FDDateTo.Text <> "" Then
                _Qry &= vbCrLf & "AND (pc.FDInsDate BETWEEN '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "' AND '" & HI.UL.ULDate.ConvertEnDB(FDDateTo.Text) & "') "
            End If

            'If FNHSysStyleId.Text <> "" And FNHSysStyleIdTo.Text <> "" Then
            '    _Qry &= vbCrLf & "AND (S.FTStyleCode BETWEEN '" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "' AND '" & HI.UL.ULF.rpQuoted(FNHSysStyleIdTo.Text) & "') "
            'End If

            If FNHSysPOID.Text <> "" And FNHSysPOIDTo.Text <> "" Then
                _Qry &= vbCrLf & "AND(op.FTCustomerPO BETWEEN '" & HI.UL.ULF.rpQuoted(FNHSysPOID.Text) & "' AND '" & HI.UL.ULF.rpQuoted(FNHSysPOIDTo.Text) & "') "
            End If

            '_Qry &= vbCrLf & "GROUP BY pc.FTInsUser, pc.FTUpdUser, pc.FDInsDate, pc.FTInsTime, cs.FTInsUser "
            '_Qry &= vbCrLf & ", cs.FTUpdUser, cs.FTPackNo, cs.FNCartonNo, op.FTCustomerPO "
            '_Qry &= vbCrLf & ", cs.FTOrderNo, cs.FTSubOrderNo, csd.Mn, csd.Mx "
            _Qry &= vbCrLf
            _Qry &= vbCrLf & "ORDER BY cs.FTOrderNo, cs.FTSubOrderNo, cs.FTPackNo, cs.FNCartonNo "

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            Me.ogcdetailcolorsize.DataSource = _dt
            Me.ogvdetailcolorsize.BestFitColumns()
        Catch ex As Exception
        End Try

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FDDate.Text <> "" And Me.FDDateTo.Text <> "" Then
            _Pass = True
        End If

        If (Me.FNHSysStyleId.Text <> "" And FNHSysStyleId.Properties.Tag.ToString <> "") _
            And (Me.FNHSysStyleIdTo.Text <> "" And FNHSysStyleIdTo.Properties.Tag.ToString <> "") Then
            _Pass = True
        End If

        If (Me.FTOrderNo.Text <> "" And FTOrderNo.Properties.Tag.ToString <> "") _
            And (Me.FTOrderNoTo.Text <> "" And FTOrderNoTo.Properties.Tag.ToString <> "") Then
            _Pass = True
        End If

        If (Me.FTSubOrderNo.Text <> "" And FTSubOrderNo.Properties.Tag.ToString <> "") _
            And (Me.FTSubOrderNoTo.Text <> "" And FTSubOrderNoTo.Properties.Tag.ToString <> "") Then
            _Pass = True
        End If

        If (Me.FNHSysPOID.Text <> "" And FNHSysPOID.Properties.Tag.ToString <> "") _
            And (Me.FNHSysPOIDTo.Text <> "" And FNHSysPOIDTo.Properties.Tag.ToString <> "") Then
            _Pass = True
        End If

        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function

#End Region


#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Call InitGrid()

            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvtime)
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvdetailcolorsize)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If VerifyData() Then
            Call LoadData()
        End If
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvtime)
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvdetailcolorsize)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

End Class