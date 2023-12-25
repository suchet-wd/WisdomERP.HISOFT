Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid

Public Class wProdSendSuplTracking

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean
    Private Totalsum As Integer
    Private _RowHandleHoldChk As Integer = 0
    Private _RowHandleHold As Integer = 0

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'InitGrid()



    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()

        'For Each oGridCol As DevExpress.XtraGrid.Columns.GridColumn In ogvtime.Columns
        '    oGridCol.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        'Next

        ''------Start Add Summary Grid-------------
        'Dim sFieldCount As String = ""
        'Dim sFieldSum As String = "" '"FNQuantity|FNSendQuantity|FNBalRcvSupl|FNRcvQuantity"

        'Dim sFieldGrpCount As String = ""
        'Dim sFieldGrpSum As String = "" '"FNQuantity|FNSendQuantity|FNBalRcvSupl|FNRcvQuantity"

        Dim sFieldCustomSum As String = "FNSendQuantity|"
        'Dim sFieldCustomGrpSum As String = ""

        With ogvtime
            .ClearGrouping()
            .ClearDocument()
            '.Columns("FTDateTrans").Group()

            'For Each Str As String In sFieldCount.Split("|")
            '    If Str <> "" Then
            '        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
            '        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
            '    End If
            'Next

            For Each Str As String In sFieldCustomSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            'For Each Str As String In sFieldSum.Split("|")
            '    If Str <> "" Then
            '        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
            '        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
            '    End If
            'Next

            'For Each Str As String In sFieldGrpCount.Split("|")
            '    If Str <> "" Then
            '        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
            '    End If
            'Next

            'For Each Str As String In sFieldCustomGrpSum.Split("|")
            '    If Str <> "" Then
            '        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
            '    End If
            'Next

            'For Each Str As String In sFieldGrpSum.Split("|")
            '    If Str <> "" Then
            '        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
            '    End If
            'Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

            .ExpandAllGroups()
            .RefreshData()

        End With

        '------End Add Summary Grid-------------
    End Sub
#End Region


#Region "Custom summaries"



    'Private totalSum As Integer = 0
    Private GrpSum As Integer = 0

    Private Sub InitStartValue()
        Totalsum = 0
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

    Private Sub LoadCompany()
        Dim _Str As String

        _Str = " SELECT   '0' AS FTSelect "
        _Str &= vbCrLf & ",M.FNHSysCmpId"
        _Str &= vbCrLf & ",M.FTCmpCode,ISNULL(IPP.FTIPServer,'') AS FTIPServer"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

            _Str &= vbCrLf & " , M.FTCmpNameTH AS FTCmpName "

        Else
            _Str &= vbCrLf & " , M.FTCmpNameEN AS FTCmpName "
        End If

        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS M WITH(NOLOCK) "
        _Str &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSECompanyIPServer AS IPP WITH(NOLOCK) ON M.FNHSysCmpId = IPP.FNHSysCmpId "
        _Str &= vbCrLf & " WHERE ISNULL(M.FTStateActive,'') ='1' AND ISNULL(IPP.FTIPServer,'') <>'' "
        _Str &= vbCrLf & " ORDER BY M.FTCmpCode"

        Me.ogccmp.DataSource = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)


        _Str = " SELECT   '0' AS FTSelect "
        _Str &= vbCrLf & ",M.FNHSysSuplId"
        _Str &= vbCrLf & ",M.FTSuplCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

            _Str &= vbCrLf & " , M.FTSuplNameTH AS FTSuplName "

        Else
            _Str &= vbCrLf & " , M.FTSuplNameEN AS FTSuplName "
        End If

        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS M WITH(NOLOCK) "

        _Str &= vbCrLf & " WHERE ISNULL(M.FTStateActive,'') ='1'  AND ISNULL(M.FTStateSubContact,'')='1'"
        _Str &= vbCrLf & " ORDER BY M.FTSuplCode"

        Me.ogcsupl.DataSource = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)


    End Sub

    Private Sub LoadData(Spls As HI.TL.SplashScreen)

        ogdtime.DataSource = Nothing


        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _dt As DataTable
        Dim _dtall As DataTable = Nothing
        Dim _TotalRow As Integer = 0
        Dim _Rx As Integer = 0

        StateCal = False

        Spls.UpdateInformation("Loading.... Data Company   Please wait....")

        Dim _dtcmp As DataTable
        With CType(Me.ogccmp.DataSource, DataTable)
            .AcceptChanges()
            _dtcmp = .Copy
        End With

        Dim StrSupl As String = ""

        With CType(Me.ogcsupl.DataSource, DataTable)
            .AcceptChanges()

            For Each R As DataRow In .Select("FTSelect='1'")

                If R!FTSuplCode.ToString <> "" Then

                    If StrSupl = "" Then
                        StrSupl = R!FTSuplCode.ToString
                    Else
                        StrSupl = StrSupl & "','" & R!FTSuplCode.ToString
                    End If

                End If
            Next

        End With

        '  Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Try


            Dim _ServerName, _UID, _PWS, _DBName As String
            Dim _ConnectString As String = ""
            Dim _FNHSysCmpId As Integer = 0
            Dim CmpCode As String = ""
            For Each R As DataRow In _dtcmp.Select("FTSelect='1'")

                _FNHSysCmpId = Val(R!FNHSysCmpId.ToString)
                CmpCode = R!FTCmpCode.ToString()

                If HI.Conn.DB.UsedDB(Conn.DB.DataBaseName.DB_PROD) Then

                    _ServerName = R!FTIPServer.ToString
                    _UID = HI.Conn.DB.UIDName
                    _PWS = HI.Conn.DB.PWDName
                    _DBName = HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD)

                    _ConnectString = "SERVER=" & _ServerName & ";UID=" & _UID & ";PWD=" & _PWS & ";Initial Catalog=" & _DBName

                    Spls.UpdateInformation("Loading.... Data Company " & R!FTCmpCode.ToString & "   Please wait....")

                    Try

                        'NEW OF the Day 2016/09/10 14.16// LAST UPDATE Modify 2016/09/12 11.15 By JOKER  //LAST UPDATE 2016/11/04 11.38 by joker// Last UPDATE 2016/11/11 11.06// Last update 2016/11/24 10.12 ********

                        _Qry = "select A.FTStyleCode,A.FTSendSuplNo,A.FTOrderNo,A.FDSendSuplDate,A.FTColorway, A.FTSizeBreakDown"
                        _Qry &= vbCrLf & ",SSm.FNSendSuplState,SST.FTSenSuplTypeName,B.FDRcvSuplDate,B.FTRcvSuplNo ,C.FTCmpCode,S.FTSuplCode  , B.FTRcvSuplBy ,  A.FTSendSuplBy"
                        If ST.Lang.Language = ST.Lang.eLang.TH Then
                            _Qry &= vbCrLf & ",C.FTCmpNameTH as FTCmpName,S.FTSuplNameTH as FTSuplName,Part.FTPartNameTH as FTPartName"
                        Else
                            _Qry &= vbCrLf & ",C.FTCmpNameEN as FTCmpName,S.FTSuplNameEN as FTSuplName ,Part.FTPartNameEN as FTPartName"
                        End If
                        _Qry &= vbCrLf & ",MAX(NKPO.FTPOref) AS FTPOref,MAX(NKLine.FTNikePOLineItem) AS FTNikePOLineItem "
                        _Qry &= vbCrLf & ",sum(A.FNSendQuantity) AS FNSendQuantity"
                        _Qry &= vbCrLf & ",(ISNULL(sum(B.FNRcvQuantity),0)) AS FNRcvQuantity"
                        _Qry &= vbCrLf & ",sum(A.FNSendQuantity)-(ISNULL(sum(B.FNRcvQuantity),0)) as FNBalRcvSupl"

                        _Qry &= vbCrLf & "from"
                        _Qry &= vbCrLf & "(select AA.FTSendSuplNo,K.FTOrderNo,K.FTBarcodeSendSuplNo,BD.FNQuantity as FNSendQuantity ,  FTSendSuplBy"
                        _Qry &= vbCrLf & ",convert(varchar(10),convert(datetime,AA.FDSendSuplDate),103) AS FDSendSuplDate"
                        '_Qry &= vbCrLf & ",convert(varchar(10),convert(datetime,RR.FDRcvSuplDate),103) AS FDRcvSuplDate"
                        _Qry &= vbCrLf & ",BD.FTColorway,BD.FTSizeBreakDown,BD.FNBunbleSeq"
                        _Qry &= vbCrLf & ",K.FNHSysPartId,AA.FNSendSuplState,K.FNSendSuplType,AA.FTSuplCode,AA.FNHSysSuplId"
                        _Qry &= vbCrLf & ",BD.FTBarcodeBundleNo,K.FTStyleCode FROM"
                        _Qry &= vbCrLf & "(select SB.FTBarcodeSendSuplNo,S.FDSendSuplDate,Supl.FTSuplCode,S.FNHSysSuplId,S.FTSendSuplNo , S.FTSendSuplBy ,S.FNSendSuplState from"
                        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode AS SB WITH(NOLOCK)    INNER JOIN"
                        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl AS S WITH(NOLOCK) ON SB.FTSendSuplNo=S.FTSendSuplNo"
                        _Qry &= vbCrLf & "LEFT OUtER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS Supl WITH(NOLOCK) ON S.FNHSysSuplId=Supl.FNHSysSuplId "

                        '  _Qry &= vbCrLf & "  WHERE  S.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & ""

                        _Qry &= vbCrLf & " ) AS AA LEFT OUTER JOIN"
                        _Qry &= vbCrLf & "(select p.FTOrderNo,BS.FTBarcodeSendSuplNo,BS.FTBarcodeBundleNo,BS.FNHSysPartId,BS.FNSendSuplType,Sty.FTStyleCode from "
                        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) INNER JOIN"
                        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS BS WITH(NOLOCK) ON P.FTOrderProdNo=BS.FTOrderProdNo LEFT OUTER JOIN"
                        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON P.FTOrderNo=O.FTOrderNo LEFT OUTER JOIN"
                        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle as Sty WITH(NOLOCK) ON O.FNHSysStyleId=Sty.FNHSysStyleId LEFT OUtER JOIN"
                        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy AS Buy WItH(NOLOCK) ON O.FNHSysBuyId=buy.FNHSysBuyId "
                        _Qry &= vbCrLf & " where O.FNHSysCmpId=" & Val(_FNHSysCmpId) & ""
                        _Qry &= vbCrLf & ") AS K ON AA.FTBarcodeSendSuplNo=K.FTBarcodeSendSuplNo"
                        If Me.cFDRcvSuplDate.Text <> "" Or Me.FDEndRcvSuplDate.Text <> "" Then
                            '_Qry &= vbCrLf & "LEFT OUtER jOIN (select distinct R.FDRcvSuplDate,SB.FTSendSuplNo,RB.FTBarcodeSendSuplNo from"
                            '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode AS RB WITH(NOLOCK) INNER jOIN"
                            '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl AS R WITH(NOLOCK) ON RB.FTRcvSuplNo=R.FTRcvSuplNo LEFT OUTER JOIN"
                            '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode AS SB WITH(NOLOCK) ON RB.FTBarcodeSendSuplNo=SB.FTBarcodeSendSuplNo"

                            ''  _Qry &= vbCrLf & "  WHERE  R.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & ""

                            '_Qry &= vbCrLf & ") AS RR ON AA.FTSendSuplNo=RR.FTSendSuplNo AND K.FTBarcodeSendSuplNo=RR.FTBarcodeSendSuplNo"


                            _Qry &= vbCrLf & " INNER jOIN (select distinct SB.FTSendSuplNo from"
                            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode AS RB WITH(NOLOCK) INNER jOIN"
                            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl AS R WITH(NOLOCK) ON RB.FTRcvSuplNo=R.FTRcvSuplNo LEFT OUTER JOIN"
                            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode AS SB WITH(NOLOCK) ON RB.FTBarcodeSendSuplNo=SB.FTBarcodeSendSuplNo "

                            '  _Qry &= vbCrLf & "  WHERE  R.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & ""


                            _Qry &= vbCrLf & "where R.FDRcvSuplDate<>''"

                            ' _Qry &= vbCrLf & "  AND  R.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & ""

                            If Me.cFDRcvSuplDate.Text <> "" Then
                                _Qry &= vbCrLf & "and R.FDRcvSuplDate>='" & UL.ULDate.ConvertEnDB(Me.cFDRcvSuplDate.Text) & "'"
                            End If
                            If Me.FDEndRcvSuplDate.Text <> "" Then
                                _Qry &= vbCrLf & "and R.FDRcvSuplDate<='" & UL.ULDate.ConvertEnDB(Me.FDEndRcvSuplDate.Text) & "'"
                            End If


                            _Qry &= vbCrLf & ") AS RR ON AA.FTSendSuplNo=RR.FTSendSuplNo "

                        End If
                        _Qry &= vbCrLf & "LEFT OUTER jOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS BD WITH(NOLOCK) ON K.FTBarcodeBundleNo=BD.FTBarcodeBundleNo"
                        _Qry &= vbCrLf & "where AA.FTSendSuplNo <>''"


                        If Me.FNHSysBuyId.Text <> "" Then
                            _Qry &= vbCrLf & "and Buy.FTBuyCode='" & Me.FNHSysBuyId.Text & "'"
                        End If
                        If Me.FNHSysStyleId.Text <> "" Then
                            _Qry &= vbCrLf & "and Sty.FTStyleCode='" & Me.FNHSysStyleId.Text & "'"
                        End If
                        If Me.FTOrderNo.Text <> "" Then
                            _Qry &= vbCrLf & "and K.FTOrderNo>='" & Me.FTOrderNo.Text & "'"
                        End If
                        If Me.FTOrderNoTo.Text <> "" Then
                            _Qry &= vbCrLf & "and K.FTOrderNo<='" & Me.FTOrderNoTo.Text & "'"
                        End If
                        If Me.FTStartShipment.Text <> "" Or Me.FTEndShipment.Text <> "" Then
                            _Qry &= vbCrLf & " AND  K.FTOrderNo In ( "
                            _Qry &= vbCrLf & " SELECT DISTINCT  SS.FTOrderNo "
                            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS SS WITH(NOLOCK) "
                            _Qry &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS Oxx WITH(NOLOCK) ON SS.FTOrderNo=Oxx.FTOrderNo "
                            _Qry &= vbCrLf & " WHERE SS.FTOrderNo <>'' "
                            _Qry &= vbCrLf & " AND Oxx.FNHSysCmpId=" & Val(_FNHSysCmpId) & ""
                            If FTStartShipment.Text <> "" Then
                                _Qry &= vbCrLf & " AND SS.FDShipDate >='" & UL.ULDate.ConvertEnDB(FTStartShipment.Text) & "'  "
                            End If

                            If FTEndShipment.Text <> "" Then
                                _Qry &= vbCrLf & " AND SS.FDShipDate <='" & UL.ULDate.ConvertEnDB(FTEndShipment.Text) & "'  "
                            End If

                            _Qry &= vbCrLf & ") "
                        End If
                        If Me.cFDSendSuplDate.Text <> "" Then
                            _Qry &= vbCrLf & "and AA.FDSendSuplDate>='" & UL.ULDate.ConvertEnDB(Me.cFDSendSuplDate.Text) & "'"
                        End If
                        If Me.FDEndSendSuplDate.Text <> "" Then
                            _Qry &= vbCrLf & "and AA.FDSendSuplDate<='" & UL.ULDate.ConvertEnDB(Me.FDEndSendSuplDate.Text) & "'"
                        End If
                        'If Me.cFDRcvSuplDate.Text <> "" Then
                        '    _Qry &= vbCrLf & "and RR.FDRcvSuplDate>='" & UL.ULDate.ConvertEnDB(Me.cFDRcvSuplDate.Text) & "'"
                        'End If
                        'If Me.FDEndRcvSuplDate.Text <> "" Then
                        '    _Qry &= vbCrLf & "and RR.FDRcvSuplDate<='" & UL.ULDate.ConvertEnDB(Me.FDEndRcvSuplDate.Text) & "'"
                        'End If
                        If Me.FNHSysSuplId.Text <> "" Then
                            _Qry &= vbCrLf & "and AA.FTSuplCode='" & Me.FNHSysSuplId.Text & "'"
                        End If


                        If StrSupl <> "" Then
                            _Qry &= vbCrLf & "and AA.FTSuplCode IN ('" & StrSupl & "')"
                        End If

                        _Qry &= vbCrLf & ") AS A LEFT OUTER JOIN"

                        _Qry &= vbCrLf & "(select RB.FTBarcodeSendSuplNo,BD.FNQuantity as FNRcvQuantity,R.FTRcvSuplNo,R.FTRcvSuplBy"
                        _Qry &= vbCrLf & ",convert(varchar(10),convert(datetime,FDRcvSuplDate),103) as FDRcvSuplDate"

                        _Qry &= vbCrLf & "from"
                        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode AS RB WITH(NOLOCK) INNER jOIN"
                        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl AS R WITH(NOLOCK) ON RB.FTRcvSuplNo=R.FTRcvSuplNo LEFT OUTER JOIN"
                        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS BS WITH(NOLOcK) ON RB.FTBarcodeSendSuplNo=BS.FTBarcodeSendSuplNo LEFT OUTER jOIN"
                        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS BD WITH(NOLOCK) ON BS.FTBarcodeBundleNo=BD.FTBarcodeBundleNo"
                        _Qry &= vbCrLf & "where R.FDRcvSuplDate<>''"

                        ' _Qry &= vbCrLf & "  AND  R.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & ""

                        If Me.cFDRcvSuplDate.Text <> "" Then
                            _Qry &= vbCrLf & "and R.FDRcvSuplDate>='" & UL.ULDate.ConvertEnDB(Me.cFDRcvSuplDate.Text) & "'"
                        End If

                        If Me.FDEndRcvSuplDate.Text <> "" Then
                            _Qry &= vbCrLf & "and R.FDRcvSuplDate<='" & UL.ULDate.ConvertEnDB(Me.FDEndRcvSuplDate.Text) & "'"
                        End If

                        _Qry &= vbCrLf & ") AS B ON A.FTBarcodeSendSuplNo=B.FTBarcodeSendSuplNo "

                        _Qry &= vbCrLf & "LEFT OUtER jOIN"
                        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo=O.FTOrderNo LEFT OUTER JOIN"
                        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH(NOLOCK) ON O.FNHSysCmpId=C.FNHSysCmpId LEFT OUTER JOIN"
                        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS Part WITH(NOLOCK) ON A.FNHSysPartId=Part.FNHSysPartId LEFT OUtER JOIN"
                        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH(NOLOCK) ON A.FNHSysSuplId=S.FNHSysSuplId"
                        _Qry &= vbCrLf & "LeFT OUtER JOIN ("
                        _Qry &= vbCrLf & "Select FNListIndex"
                        _Qry &= vbCrLf & ",FTNameTH AS FNSendSuplState "
                        _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH (NOLOCK)"
                        _Qry &= vbCrLf & "WHERE  (FTListName = N'FNSendSuplState')"
                        _Qry &= vbCrLf & ") AS SSM ON A.FNSendSuplState=SSM.FNListIndex "
                        _Qry &= vbCrLf & "LeFT OUtER join"
                        _Qry &= vbCrLf & "("
                        _Qry &= vbCrLf & "Select FNListIndex"
                        _Qry &= vbCrLf & ",FTNameTH AS FTSenSuplTypeName"
                        _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH (NOLOCK)"
                        _Qry &= vbCrLf & "WHERE  (FTListName = N'FNSendSuplType')"
                        _Qry &= vbCrLf & ") AS SST  ON A.FNSendSuplType=SST.FNListIndex "

                        '_Qry &= vbCrLf & " Outer Apply  (select top 1 OBSX.FTPOref,OBSX.FTNikePOLineItem "
                        '_Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS OBSX  "
                        '_Qry &= vbCrLf & "  WHERE OBSX.FTOrderNo= A.FTOrderNo   AND OBSX.FTColorway=A.FTColorway AND OBSX.FTSizeBreakDown=A.FTSizeBreakDown "
                        '_Qry &= vbCrLf & "  ) OBSX "

                        _Qry &= vbCrLf & "  OUTER APPLY(select  STUFF((SELECT  ',' + FTPOref  "
                        _Qry &= vbCrLf & "	From( SELECT DISTINCT OBSX.FTPOref  "
                        _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS OBSX  "
                        _Qry &= vbCrLf & "  WHERE OBSX.FTOrderNo= A.FTOrderNo   AND OBSX.FTColorway=A.FTColorway AND OBSX.FTSizeBreakDown=A.FTSizeBreakDown "
                        _Qry &= vbCrLf & ") As T  "
                        _Qry &= vbCrLf & " For Xml PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,1,'')  AS FTPOref ) AS NKPO "


                        _Qry &= vbCrLf & "  OUTER APPLY(select  STUFF((SELECT  ',' + FTNikePOLineItem  "
                        _Qry &= vbCrLf & "	From( SELECT DISTINCT OBSX.FTNikePOLineItem  "
                        _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS OBSX  "
                        _Qry &= vbCrLf & "  WHERE OBSX.FTOrderNo= A.FTOrderNo   AND OBSX.FTColorway=A.FTColorway AND OBSX.FTSizeBreakDown=A.FTSizeBreakDown "
                        _Qry &= vbCrLf & ") As T  "
                        _Qry &= vbCrLf & " For Xml PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,1,'')  AS FTNikePOLineItem ) AS NKLine "


                        _Qry &= vbCrLf & " where O.FNHSysCmpId=" & Val(_FNHSysCmpId) & ""

                        _Qry &= vbCrLf & "group by A.FTSendSuplNo,A.FTOrderNo,A.FDSendSuplDate,A.FTColorway, A.FTSizeBreakDown"
                        _Qry &= vbCrLf & ",SSm.FNSendSuplState,SST.FTSenSuplTypeName"
                        _Qry &= vbCrLf & ",B.FDRcvSuplDate,B.FTRcvSuplNo,C.FTCmpCode,S.FTSuplCode,A.FTStyleCode , B.FTRcvSuplBy  , A.FTSendSuplBy"

                        If ST.Lang.Language = ST.Lang.eLang.TH Then
                            _Qry &= vbCrLf & ",C.FTCmpNameTH,S.FTSuplNameTH,Part.FTPartNameTH"
                        Else
                            _Qry &= vbCrLf & ",C.FTCmpNameEN,S.FTSuplNameEN,Part.FTPartNameEN"
                        End If

                        _dt = HI.Conn.SQLConn.GetDataTableConectstring(_Qry, _ConnectString)

                        If _dtall Is Nothing Then
                            _dtall = _dt.Copy
                        Else
                            _dtall.Merge(_dt.Copy)
                        End If

                    Catch ex22 As Exception

                    End Try

                End If

            Next



            Me.ogdtime.DataSource = _dtall
            Call InitialGridMergCell()
        Catch ex As Exception
        End Try

        Call LoadDataBundle(Spls)

        ' _Spls.Close()
        _RowDataChange = False

    End Sub

    Private Sub LoadDataBundle(Spls As HI.TL.SplashScreen)

        ogcbybundle.DataSource = Nothing


        Spls.UpdateInformation("Loading.... Data Company   Please wait....")

        Dim _dtcmp As DataTable
        With CType(Me.ogccmp.DataSource, DataTable)
            .AcceptChanges()
            _dtcmp = .Copy
        End With


        Dim StrSupl As String = ""

        With CType(Me.ogcsupl.DataSource, DataTable)
            .AcceptChanges()

            For Each R As DataRow In .Select("FTSelect='1'")

                If R!FTSuplCode.ToString <> "" Then

                    If StrSupl = "" Then
                        StrSupl = R!FTSuplCode.ToString
                    Else
                        StrSupl = StrSupl & "','" & R!FTSuplCode.ToString
                    End If

                End If
            Next

        End With

        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _dt As DataTable
        Dim _TotalRow As Integer = 0
        Dim _Rx As Integer = 0
        Dim _dtall As DataTable = Nothing

        Dim _ServerName, _UID, _PWS, _DBName As String
        Dim _ConnectString As String = ""
        Dim _FNHSysCmpId As Integer = 0
        Dim CmpCode As String = ""
        For Each R As DataRow In _dtcmp.Select("FTSelect='1'")

            _FNHSysCmpId = Val(R!FNHSysCmpId.ToString)
            CmpCode = R!FTCmpCode.ToString()

            If HI.Conn.DB.UsedDB(Conn.DB.DataBaseName.DB_PROD) Then

                _ServerName = R!FTIPServer.ToString
                _UID = HI.Conn.DB.UIDName
                _PWS = HI.Conn.DB.PWDName
                _DBName = HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD)

                _ConnectString = "SERVER=" & _ServerName & ";UID=" & _UID & ";PWD=" & _PWS & ";Initial Catalog=" & _DBName

                Spls.UpdateInformation("Loading.... Data Company " & R!FTCmpCode.ToString & "   Please wait....")

                Try

                    _Qry = "select A.FTStyleCode,A.FTSendSuplNo,A.FTOrderNo,A.FTBarcodeSendSuplNo,A.FDSendSuplDate,A.FTColorway, A.FTSizeBreakDown , FTSendSuplBy ,FTRcvSuplBy"
                    _Qry &= vbCrLf & ",A.FTBarcodeBundleNo,A.FNBunbleSeq,SSm.FNSendSuplState,SST.FTSenSuplTypeName"
                    _Qry &= vbCrLf & ",B.FDRcvSuplDate,B.FTRcvSuplNo,O.FTPORef as FTPORef,C.FTCmpCode,S.FTSuplCode"

                    If ST.Lang.Language = ST.Lang.eLang.TH Then
                        _Qry &= vbCrLf & ",C.FTCmpNameTH as FTCmpName,S.FTSuplNameTH as FTSuplName,Part.FTPartNameTH as FTPartName"
                    Else
                        _Qry &= vbCrLf & ",C.FTCmpNameEN as FTCmpName,S.FTSuplNameEN as FTSuplName,Part.FTPartNameEN as FTPartName"
                    End If

                    _Qry &= vbCrLf & " ,A.FNSendQuantity AS FNSendQuantity "
                    _Qry &= vbCrLf & " ,B.FNRcvQuantity AS FNRcvQuantity, "


                    '_Qry &= vbCrLf & " (A.FNSendQuantity)-(ISNULL(B.FNRcvQuantity,0)) as FNBalRcvSupl, "
                    ' ----- Edit By Chet  10 Jun 2023 -----
                    _Qry &= vbCrLf & " CASE WHEN (A.FNSendQuantity = ISNULL(B.FNRcvQuantity,0)) THEN "
                    _Qry &= vbCrLf & " '0' ELSE A.FNSendQuantity - ISNULL(B.FNRcvQuantity,0) END as FNBalRcvSupl, "
                    ' ----- End Edit By Chet  10 Jun 2023 -----


                    _Qry &= vbCrLf & " A.FTPOLineItemNo, A.FTOrderProdNo, "

                    ' ----- Add By Chet  29 Mar 2023 -----
                    _Qry &= vbCrLf & " SS2B_BC.FTStateBranchAcceptBy as FTSendApproveBy, "

                    _Qry &= vbCrLf & " CASE WHEN SS2B_BC.FTStateBranchAcceptBy is null THEN null ELSE "
                    _Qry &= vbCrLf & " CASE WHEN ISNULL( SS2B_BC.FNBalQuantity, 0 ) = ISNULL( RS2B_BC.FNBalQuantity, 0 ) THEN "
                    _Qry &= vbCrLf & " A.FNSendQuantity ELSE ISNULL( SS2B_BC.FNBalQuantity, 0 ) - ISNULL( RS2B_BC.FNBalQuantity, 0 ) "
                    _Qry &= vbCrLf & " END END AS FTSendApproveQty, "

                    _Qry &= vbCrLf & " CONVERT(VARCHAR(10), Convert (DateTime, SS2B_BC.FTStateBranchAcceptDate ), 103 ) AS FTSendApproveDate, "
                    ' ----- Edit By Chet  10 Jun 2023 -----
                    _Qry &= vbCrLf & " CASE WHEN A.FNSendQuantity >= (ISNULL(SS2B_BC.FNBalQuantity,0)) THEN "
                    _Qry &= vbCrLf & " A.FNSendQuantity - ISNULL(SS2B_BC.FNBalQuantity,0) "
                    _Qry &= vbCrLf & " ELSE '0' END as FTSendApproveBal, "
                    ' ----- End Edit By Chet  10 Jun 2023 -----

                    _Qry &= vbCrLf & " RS2B_BC.FTStateBranchAcceptBy as FTRcvApproveBy, "
                    _Qry &= vbCrLf & " CASE WHEN RS2B_BC.FTStateBranchAcceptBy is not null THEN CASE WHEN RS2B_BC.FNBalQuantity >= A.FNSendQuantity THEN "
                    _Qry &= vbCrLf & " A.FNSendQuantity ELSE RS2B_BC.FNBalQuantity END END AS FTRcvApproveQty, "
                    _Qry &= vbCrLf & "  Convert(VARCHAR(10), Convert(DateTime, RS2B_BC.FTStateBranchAcceptDate), 103) As FTRcvApproveDate, "

                    'SS2B_BC.FTStateBranchAcceptBy IS NOT NULL THEN CASE WHEN 
                    _Qry &= vbCrLf & " CASE WHEN RS2B_BC.FTStateBranchAcceptBy IS NULL THEN "
                    _Qry &= vbCrLf & " B.FNRcvQuantity ELSE ISNULL(RS2B_BC.FNBalQuantity,0) - ISNULL(SS2B_BC.FNBalQuantity,0)  END AS FTRcvApproveBal "
                    ' ----- End Add By Chet 29 Mar 2023  / 25 Dec 2023 ----- 
                    _Qry &= vbCrLf & " ,ISNULL( SPLN.FTNote, '' ) AS FTNote"


                    '' best add  unisect receive 20231211 
                    If ST.Lang.Language = ST.Lang.eLang.TH Then
                        _Qry &= vbCrLf & ",RS2B_BC.FNHSysUnitSectId  ,US.FTUnitSectCode ,US.FTUnitSectNameTH AS FTUnitSectName"
                    Else
                        _Qry &= vbCrLf & ",RS2B_BC.FNHSysUnitSectId  ,US.FTUnitSectCode ,US.FTUnitSectNameEN AS FTUnitSectName "
                    End If

                    '' best end add 20231211



                    _Qry &= vbCrLf & "from"
                    _Qry &= vbCrLf & "(Select AA.FTSendSuplNo,K.FTOrderNo,K.FTBarcodeSendSuplNo,BD.FNQuantity As FNSendQuantity"
                    _Qry &= vbCrLf & ",convert(varchar(10),convert(datetime,AA.FDSendSuplDate),103) As FDSendSuplDate"
                    '_Qry &= vbCrLf & ",convert(varchar(10),convert(datetime,RR.FDRcvSuplDate),103) As FDRcvSuplDate"
                    _Qry &= vbCrLf & ",BD.FTColorway,BD.FTSizeBreakDown,BD.FNBunbleSeq,BD.FTPOLineItemNo"
                    _Qry &= vbCrLf & ",K.FNHSysPartId,AA.FNSendSuplState,K.FNSendSuplType,AA.FTSuplCode,AA.FNHSysSuplId,K.FTOrderProdNo"
                    _Qry &= vbCrLf & ",BD.FTBarcodeBundleNo,K.FTStyleCode , AA.FTSendSuplBy FROM"

                    _Qry &= vbCrLf & "(Select SB.FTBarcodeSendSuplNo,S.FDSendSuplDate,Supl.FTSuplCode,S.FNHSysSuplId,S.FTSendSuplNo,S.FNSendSuplState , S.FTSendSuplBy from"
                    _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode As SB With(NOLOCK)    INNER JOIN"
                    _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl As S With(NOLOCK) On SB.FTSendSuplNo=S.FTSendSuplNo"
                    _Qry &= vbCrLf & "LEFT OUtER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier As Supl With(NOLOCK) On S.FNHSysSuplId=Supl.FNHSysSuplId "
                    ' _Qry &= vbCrLf & "  WHERE  S.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & ""


                    _Qry &= vbCrLf & " ) As AA LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "(Select p.FTOrderNo,BS.FTBarcodeSendSuplNo,BS.FTBarcodeBundleNo,BS.FNHSysPartId,BS.FNSendSuplType,P.FTOrderProdNo,STy.FTStyleCode from "
                    _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd As P With(NOLOCK) INNER JOIN"
                    _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl As BS With(NOLOCK) On P.FTOrderProdNo=BS.FTOrderProdNo LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder As O With(NOLOCK) On P.FTOrderNo=O.FTOrderNo LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As Sty With(NOLOCK) On O.FNHSysStyleId=Sty.FNHSysStyleId LEFT OUtER JOIN"
                    _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy As Buy With(NOLOCK) On O.FNHSysBuyId=buy.FNHSysBuyId "

                    _Qry &= vbCrLf & " where O.FNHSysCmpId=" & Val(_FNHSysCmpId) & ""

                    _Qry &= vbCrLf & ") As K On AA.FTBarcodeSendSuplNo=K.FTBarcodeSendSuplNo"

                    If Me.cFDRcvSuplDate.Text <> "" Or Me.FDEndRcvSuplDate.Text <> "" Then
                        '_Qry &= vbCrLf & "LEFT OUtER jOIN (Select distinct R.FDRcvSuplDate,SB.FTSendSuplNo,RB.FTBarcodeSendSuplNo from"
                        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode As RB With(NOLOCK) INNER jOIN"
                        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl As R With(NOLOCK) On RB.FTRcvSuplNo=R.FTRcvSuplNo LEFT OUtER jOIN"
                        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode As SB With(NOLOCK) On RB.FTBarcodeSendSuplNo=SB.FTBarcodeSendSuplNo"

                        '' _Qry &= vbCrLf & "  WHERE  R.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & ""

                        '_Qry &= vbCrLf & ") As RR On AA.FTSendSuplNo=RR.FTSendSuplNo And K.FTBarcodeSendSuplNo=RR.FTBarcodeSendSuplNo "


                        _Qry &= vbCrLf & " INNER jOIN (Select distinct  SB.FTSendSuplNo  from"
                        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode As RB With(NOLOCK) INNER jOIN"
                        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl As R With(NOLOCK) On RB.FTRcvSuplNo=R.FTRcvSuplNo LEFT OUtER jOIN"
                        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode As SB With(NOLOCK) On RB.FTBarcodeSendSuplNo=SB.FTBarcodeSendSuplNo"

                        ' _Qry &= vbCrLf & "  WHERE  R.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & ""
                        _Qry &= vbCrLf & "where R.FDRcvSuplDate<>''"
                        If Me.cFDRcvSuplDate.Text <> "" Then
                            _Qry &= vbCrLf & "and R.FDRcvSuplDate>='" & UL.ULDate.ConvertEnDB(Me.cFDRcvSuplDate.Text) & "'"
                        End If
                        If Me.FDEndRcvSuplDate.Text <> "" Then
                            _Qry &= vbCrLf & "and R.FDRcvSuplDate<='" & UL.ULDate.ConvertEnDB(Me.FDEndRcvSuplDate.Text) & "'"
                        End If

                        _Qry &= vbCrLf & ") As RR On AA.FTSendSuplNo=RR.FTSendSuplNo  "

                    End If

                    _Qry &= vbCrLf & "LEFT OUTER jOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode As BD With(NOLOCK) On K.FTBarcodeBundleNo=BD.FTBarcodeBundleNo"
                    _Qry &= vbCrLf & "where AA.FTSendSuplNo <>''"

                    If Me.FNHSysBuyId.Text <> "" Then
                        _Qry &= vbCrLf & "and Buy.FTBuyCode='" & Me.FNHSysBuyId.Text & "'"
                    End If

                    If Me.FNHSysStyleId.Text <> "" Then
                        _Qry &= vbCrLf & "and K.FTStyleCode='" & Me.FNHSysStyleId.Text & "'"
                    End If

                    If Me.FTOrderNo.Text <> "" Then
                        _Qry &= vbCrLf & "and K.FTOrderNo>='" & Me.FTOrderNo.Text & "'"
                    End If

                    If Me.FTOrderNoTo.Text <> "" Then
                        _Qry &= vbCrLf & "and K.FTOrderNo<='" & Me.FTOrderNoTo.Text & "'"
                    End If

                    If Me.FTStartShipment.Text <> "" Or Me.FTEndShipment.Text <> "" Then

                        _Qry &= vbCrLf & " AND  K.FTOrderNo In ( "
                        _Qry &= vbCrLf & " SELECT DISTINCT  SS.FTOrderNo "
                        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS SS WITH(NOLOCK) "
                        _Qry &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS Oxx WITH(NOLOCK) ON SS.FTOrderNo=Oxx.FTOrderNo "
                        _Qry &= vbCrLf & "  WHERE SS.FTOrderNo <>'' "

                        _Qry &= vbCrLf & " AND Oxx.FNHSysCmpId=" & Val(_FNHSysCmpId) & ""

                        If FTStartShipment.Text <> "" Then
                            _Qry &= vbCrLf & " AND SS.FDShipDate >='" & UL.ULDate.ConvertEnDB(FTStartShipment.Text) & "'  "
                        End If

                        If FTEndShipment.Text <> "" Then
                            _Qry &= vbCrLf & " AND SS.FDShipDate <='" & UL.ULDate.ConvertEnDB(FTEndShipment.Text) & "'  "
                        End If

                        _Qry &= vbCrLf & ") "

                    End If

                    If Me.cFDSendSuplDate.Text <> "" Then
                        _Qry &= vbCrLf & "and AA.FDSendSuplDate>='" & UL.ULDate.ConvertEnDB(Me.cFDSendSuplDate.Text) & "'"
                    End If

                    If Me.FDEndSendSuplDate.Text <> "" Then
                        _Qry &= vbCrLf & "and AA.FDSendSuplDate<='" & UL.ULDate.ConvertEnDB(Me.FDEndSendSuplDate.Text) & "'"
                    End If
                    'If Me.cFDRcvSuplDate.Text <> "" Then
                    '    _Qry &= vbCrLf & "and RR.FDRcvSuplDate>='" & UL.ULDate.ConvertEnDB(Me.cFDRcvSuplDate.Text) & "'"
                    'End If
                    'If Me.FDEndRcvSuplDate.Text <> "" Then
                    '    _Qry &= vbCrLf & "and RR.FDRcvSuplDate<='" & UL.ULDate.ConvertEnDB(Me.FDEndRcvSuplDate.Text) & "'"
                    'End If
                    If Me.FNHSysSuplId.Text <> "" Then
                        _Qry &= vbCrLf & "and AA.FTSuplCode='" & Me.FNHSysSuplId.Text & "'"
                    End If

                    If StrSupl <> "" Then
                        _Qry &= vbCrLf & "and AA.FTSuplCode IN ('" & StrSupl & "')"
                    End If

                    _Qry &= vbCrLf & ") AS A LEFT OUTER JOIN"

                    _Qry &= vbCrLf & "(select RB.FTBarcodeSendSuplNo,BD.FNQuantity as FNRcvQuantity,R.FTRcvSuplNo,R.FTRcvSuplBy"
                    _Qry &= vbCrLf & ",convert(varchar(10),convert(datetime,FDRcvSuplDate),103) as FDRcvSuplDate"

                    ' Add by Chet 13 Jun 2023
                    _Qry &= vbCrLf & " ,bd.FTBarcodeBundleNo , bd.FTPOLineItemNo "
                    ' End Add by Chet 13 Jun 2023

                    _Qry &= vbCrLf & "from"
                    _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode AS RB WITH(NOLOCK) INNER jOIN"
                    _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl AS R WITH(NOLOCK) ON RB.FTRcvSuplNo=R.FTRcvSuplNo LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS BS WITH(NOLOcK) ON RB.FTBarcodeSendSuplNo=BS.FTBarcodeSendSuplNo LEFT OUTER jOIN"
                    _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS BD WITH(NOLOCK) ON BS.FTBarcodeBundleNo=BD.FTBarcodeBundleNo"
                    _Qry &= vbCrLf & "where R.FDRcvSuplDate<>''"
                    If Me.cFDRcvSuplDate.Text <> "" Then
                        _Qry &= vbCrLf & "and R.FDRcvSuplDate>='" & UL.ULDate.ConvertEnDB(Me.cFDRcvSuplDate.Text) & "'"
                    End If
                    If Me.FDEndRcvSuplDate.Text <> "" Then
                        _Qry &= vbCrLf & "and R.FDRcvSuplDate<='" & UL.ULDate.ConvertEnDB(Me.FDEndRcvSuplDate.Text) & "'"
                    End If

                    'Modify by Chet 13 Jun 2023 + "and a.FTBarcodeBundleNo = b.FTBarcodeBundleNo and a.FTPOLineItemNo   = b.FTPOLineItemNo"
                    '_Qry &= vbCrLf & ") AS B ON A.FTBarcodeSendSuplNo=B.FTBarcodeSendSuplNo"
                    _Qry &= vbCrLf & ") AS B ON A.FTBarcodeSendSuplNo=B.FTBarcodeSendSuplNo and a.FTBarcodeBundleNo = b.FTBarcodeBundleNo and a.FTPOLineItemNo   = b.FTPOLineItemNo"
                    'End Modify by Chet 13 Jun 2023

                    _Qry &= vbCrLf & "LEFT OUtER jOIN"
                    _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo=O.FTOrderNo LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH(NOLOCK) ON O.FNHSysCmpId=C.FNHSysCmpId LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS Part WITH(NOLOCK) ON A.FNHSysPartId=Part.FNHSysPartId LEFT OUtER JOIN"
                    _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH(NOLOCK) ON A.FNHSysSuplId=S.FNHSysSuplId "
                    _Qry &= vbCrLf & "LeFT OUtER JOIN ("
                    _Qry &= vbCrLf & "Select FNListIndex"
                    _Qry &= vbCrLf & ",FTNameTH AS FNSendSuplState "
                    _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH (NOLOCK)"
                    _Qry &= vbCrLf & "WHERE  (FTListName = N'FNSendSuplState')"
                    _Qry &= vbCrLf & ") AS SSM ON A.FNSendSuplState=SSM.FNListIndex "
                    _Qry &= vbCrLf & "LeFT OUtER join"
                    _Qry &= vbCrLf & "("
                    _Qry &= vbCrLf & "Select FNListIndex"
                    _Qry &= vbCrLf & ",FTNameTH AS FTSenSuplTypeName"
                    _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH (NOLOCK)"
                    _Qry &= vbCrLf & "WHERE  (FTListName = N'FNSendSuplType')"
                    _Qry &= vbCrLf & ") AS SST  ON A.FNSendSuplType=SST.FNListIndex "


                    ' ----- Add By Chet 29 Mar 2023 ----- 
                    _Qry &= vbCrLf & " Left OUTER JOIN (select FTSendSuplNo, FTStateBranchAcceptBy, FTStateBranchAcceptDate, FNBalQuantity, FTBarcodeSendSuplNo, FNQuantity "
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch_Barcode) as SS2B_BC "
                    _Qry &= vbCrLf & " ON SS2B_BC.FTSendSuplNo = A.FTSendSuplNo AND SS2B_BC.FTBarcodeSendSuplNo = A.FTBarcodeSendSuplNo "

                    _Qry &= vbCrLf & " Left OUTER JOIN (select FTRcvSuplNo, FTStateBranchAcceptBy, FTStateBranchAcceptDate, FNBalQuantity, FTBarcodeSendSuplNo , FNHSysUnitSectId "
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSuplToBranch_Barcode) as RS2B_BC "
                    _Qry &= vbCrLf & " ON RS2B_BC.FTRcvSuplNo = B.FTRcvSuplNo AND RS2B_BC.FTBarcodeSendSuplNo = B.FTBarcodeSendSuplNo "

                    _Qry &= vbCrLf & " OUTER APPLY (SELECT top 1  FNHSysUnitSectId, FTUnitSectCode, FTUnitSectNameTH, FTUnitSectNameEN FROM  HITECH_MASTER.dbo.TCNMUnitSect WHERE FNHSysUnitSectId = RS2B_BC.FNHSysUnitSectId) US "

                    _Qry &= vbCrLf & " OUTER APPLY ( SELECT top 1  FTNote FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.FN_Get_SendSuplDesc (    A.FTBarcodeSendSuplNo ,A.FTBarcodeBundleNo  ) A  "
                    _Qry &= vbCrLf & "  ) AS SPLN"
                    ' ----- End Add By Chet 29 Mar 2023 ----- 


                    _Qry &= vbCrLf & " where O.FNHSysCmpId=" & Val(_FNHSysCmpId) & ""

                    _dt = HI.Conn.SQLConn.GetDataTableConectstring(_Qry, _ConnectString)


                    If _dtall Is Nothing Then
                        _dtall = _dt.Copy
                    Else
                        _dtall.Merge(_dt.Copy)

                    End If

                Catch ex22 As Exception
                    ' System.Windows.Forms.MessageBox.Show(ex22.Message())
                End Try

            End If

        Next

        Me.ogcbybundle.DataSource = _dtall
        Call InitialGridMergCell()


    End Sub


    Private Sub InitialGridMergCell()

        For Each c As GridColumn In ogvtime.Columns
            'If FNReportGridMergeFormat.SelectedIndex = 1 Then
            '    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            'Else
            Select Case c.FieldName.ToString
                Case "FNRcvQuantity", "FTPositionPartName", "FTMatColorCode", "FTRawMatColorName"
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
                Case Else
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
            End Select

            'End If

        Next

    End Sub

    Private Sub InitialGridBundleMergCell()

        For Each c As GridColumn In ogvbybundle.Columns
            'If FNReportGridMergeFormat.SelectedIndex = 1 Then
            '    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            'Else
            Select Case c.FieldName.ToString
                Case "FTStyleCode", "FTOrderNo", "FTCmpCode", "FTCmpName", "FTColorway", "FTSizeBreakDown", "FTSenSuplTypeName" _
                    , "FTPartName", "FTSuplCode", "FTSuplName", "FTSendSuplNo", "FDSendSuplDate", "FNSendSuplState", "FTRcvSuplNo", "FDRcvSuplDate", "FTUnitSectCode"
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                Case Else
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End Select
            'End If

        Next

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FNHSysBuyId.Text <> "" Then
            _Pass = True
        End If

        If Me.FNHSysStyleId.Text <> "" Then
            _Pass = True
        End If

        If Me.FTOrderNo.Text <> "" Then
            _Pass = True
        End If

        If Me.FTOrderNoTo.Text <> "" Then
            _Pass = True
        End If

        If Me.cFDSendSuplDate.Text <> "" Then
            _Pass = True
        End If

        If Me.FDEndSendSuplDate.Text <> "" Then
            _Pass = True
        End If

        If Me.cFDRcvSuplDate.Text <> "" Then
            _Pass = True
        End If
        If Me.FDEndRcvSuplDate.Text <> "" Then
            _Pass = True
        End If
        'If Me.FNHSysSuplId.Text <> "" Then
        '    _Pass = True
        'End If
        If Me.FTStartShipment.Text <> "" Then
            _Pass = True
        End If
        If Me.FTEndShipment.Text <> "" Then
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
            'Call InitGrid()
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvtime)

            Call LoadCompany()

            StateCal = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If VerifyData() Then


            Dim _dtcmp As DataTable
            With CType(Me.ogccmp.DataSource, DataTable)
                .AcceptChanges()
                _dtcmp = .Copy
            End With

            If _dtcmp.Select("FTSelect='1'").Length > 0 Then
                _dtcmp.Dispose()

                Dim _Spls As New HI.TL.SplashScreen("Loading data...   Please Wait  ")

                Try
                    Call LoadData(_Spls)
                    Me.ogcdetailcolorsizeline.SelectedTabPageIndex = 0
                Catch ex As Exception
                    ' System.Windows.Forms.MessageBox.Show(ex.Message())
                End Try

                _Spls.Close()

            Else
                _dtcmp.Dispose()

                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Company !!!", 15120508456, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)

            End If

        End If
        Me.otbx.SelectedTabPageIndex = 0
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)

        Call LoadCompany()


        Me.otbx.SelectedTabPageIndex = 0

    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvtime)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ogvtime_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvtime.CellMerge
        Try
            With Me.ogvtime

                If FNReportGridMergeFormat.SelectedIndex = 1 Then
                    e.Merge = False
                    e.Handled = True
                Else
                    Select Case e.Column.FieldName
                        Case "FTColorway", "FTPOref"
                            If "" & .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString _
                                And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then
                                e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                                e.Handled = True
                                e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                            Else
                                e.Merge = False
                                e.Handled = True
                            End If

                        Case "FTSizeBreakDown", "FTPORef", "FTNikePOLineItem"
                            If ("" & .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString) _
                                And ("" & .GetRowCellValue(e.RowHandle1, "FTColorway").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTColorway").ToString) _
                                And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then
                                e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                                e.Handled = True
                                e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                            Else
                                e.Merge = False
                                e.Handled = True
                            End If

                        Case "FNQuantity", "FTPORef"

                            If ("" & .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString) _
                               And ("" & .GetRowCellValue(e.RowHandle1, "FTColorway").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTColorway").ToString) _
                               And ("" & .GetRowCellValue(e.RowHandle1, "FTSizeBreakDown").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTSizeBreakDown").ToString) _
                               And ("" & .GetRowCellValue(e.RowHandle1, "FTSenSuplTypeName").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTSenSuplTypeName").ToString) _
                               And ("" & .GetRowCellValue(e.RowHandle1, "FTPartName").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTPartName").ToString) _
                                And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                                e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                                e.Handled = True
                                e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                            Else
                                e.Merge = False
                                e.Handled = True
                            End If


                        Case "FNSendQuantity", "FNBalRcvSupl", "FTPORef"

                            If ("" & .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString) _
                              And ("" & .GetRowCellValue(e.RowHandle1, "FTColorway").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTColorway").ToString) _
                              And ("" & .GetRowCellValue(e.RowHandle1, "FTSizeBreakDown").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTSizeBreakDown").ToString) _
                              And ("" & .GetRowCellValue(e.RowHandle1, "FTSenSuplTypeName").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTSenSuplTypeName").ToString) _
                              And ("" & .GetRowCellValue(e.RowHandle1, "FTPartName").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTPartName").ToString) _
                              And ("" & .GetRowCellValue(e.RowHandle1, "FTSendSuplNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTSendSuplNo").ToString) _
                              And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                                e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                                e.Handled = True
                                e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap

                            Else

                                e.Merge = False
                                e.Handled = True

                            End If

                        Case "FTStyleCode", "FTSuplCode", "FTSuplName", "FTOrderNo", "FTPORef", "FTCmpCode", "FTCmpName", "FTSenSuplTypeName", "FTPartName", "FTSendSuplNo", "FDSendSuplDate", "FTRcvSuplNo", "FDRcvSuplDate", "FNSendSuplState"

                            If "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                                e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                                e.Handled = True
                                e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap

                            Else

                                e.Merge = False
                                e.Handled = True

                            End If
                        Case "FTSendSuplBy"

                            If ("" & .GetRowCellValue(e.RowHandle1, "FTSendSuplNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTSendSuplNo").ToString) Then

                                e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                                e.Handled = True
                                e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap

                            Else

                                e.Merge = False
                                e.Handled = True

                            End If

                        Case "FTRcvSuplBy"

                            If ("" & .GetRowCellValue(e.RowHandle1, "FTRcvSuplNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTRcvSuplNo").ToString) Then

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
                End If


            End With

        Catch ex As Exception
        End Try

    End Sub
    'Private Sub ogvtime_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvtime.RowCellStyle
    '    Try
    '        With Me.ogvtime
    '            Select Case e.Column.FieldName
    '                Case "FNCutQuantity"
    '                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNCutQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNGrandQuantity")) Then
    '                        e.Appearance.BackColor = Drawing.Color.OrangeRed
    '                    Else
    '                        e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                    End If
    '                Case "FNRcvSuplQuantity"
    '                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSendSuplQuantity")) > 0 Then
    '                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNRcvSuplQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNSendSuplQuantity")) Then
    '                            e.Appearance.BackColor = Drawing.Color.OrangeRed
    '                        Else
    '                            e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                        End If
    '                    End If

    '                Case "FNSPMKQuantity"
    '                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNCutQuantity")) > 0 Then
    '                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSPMKQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNCutQuantity")) Then
    '                            e.Appearance.BackColor = Drawing.Color.OrangeRed
    '                        Else
    '                            e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                        End If
    '                    End If
    '                Case "FNSewQuantity"
    '                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSPMKQuantity")) > 0 Then
    '                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNSPMKQuantity")) Then
    '                            e.Appearance.BackColor = Drawing.Color.OrangeRed
    '                        Else
    '                            e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                        End If
    '                    End If

    '                Case "FNSewOutQuantity"
    '                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewQuantity")) > 0 Then
    '                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewOutQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewQuantity")) Then
    '                            e.Appearance.BackColor = Drawing.Color.OrangeRed
    '                        Else
    '                            e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                        End If
    '                    End If

    '                Case "FNPackQuantity"
    '                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewOutQuantity")) > 0 Then
    '                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNPackQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewOutQuantity")) Then
    '                            e.Appearance.BackColor = Drawing.Color.OrangeRed

    '                        Else
    '                            e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                        End If
    '                    End If
    '                Case "FNBalCutQuantity", "FNBalSuplQuantity", "FNBalSewQuantity", "FNBalPackQuantity", "FNCutBalQuantity"
    '                    If Double.Parse(.GetRowCellValue(e.RowHandle, e.Column.FieldName)) > 0 Then
    '                        e.Appearance.BackColor = Drawing.Color.OrangeRed
    '                    Else

    '                        Select Case e.Column.FieldName
    '                            Case "FNBalCutQuantity"
    '                                If Double.Parse(.GetRowCellValue(e.RowHandle, "FNCutQuantity")) > 0 Then
    '                                    e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                                End If
    '                            Case "FNBalSuplQuantity"
    '                                If Double.Parse(.GetRowCellValue(e.RowHandle, "FNRcvSuplQuantity")) > 0 Then
    '                                    e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                                End If
    '                            Case "FNBalSewQuantity"
    '                                If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewQuantity")) > 0 Then
    '                                    e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                                End If
    '                            Case "FNBalPackQuantity"
    '                                If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewOutQuantity")) > 0 Then
    '                                    e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                                End If
    '                            Case "FNCutBalQuantity"
    '                                If Double.Parse(.GetRowCellValue(e.RowHandle, "FNGrandQuantity")) > 0 Then
    '                                    e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                                End If
    '                        End Select

    '                    End If

    '            End Select
    '        End With
    '    Catch ex As Exception

    '    End Try
    'End Sub


    Private Sub ogvbybundle_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvbybundle.CellMerge
        Try
            With Me.ogvbybundle

                If FNReportGridMergeFormat.SelectedIndex = 1 Then
                    e.Merge = False
                    e.Handled = True
                Else
                    Select Case e.Column.FieldName
                        Case "FTColorway"
                            If "" & .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString _
                                And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then
                                e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                                e.Handled = True
                                e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                            Else
                                e.Merge = False
                                e.Handled = True
                            End If

                        Case "FTSizeBreakDown", "FTPORef1 "

                            If ("" & .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString) _
                                And ("" & .GetRowCellValue(e.RowHandle1, "FTColorway").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTColorway").ToString) _
                                And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                                e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                                e.Handled = True
                                e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                            Else
                                e.Merge = False
                                e.Handled = True
                            End If

                        Case "FTStyleCode", "FTSuplCode", "FTSuplName", "FTOrderNo", "FTCmpCode", "FTCmpName", "FTSenSuplTypeName", "FTPartName", "FTSendSuplNo", "FDSendSuplDate", "FTRcvSuplNo", "FDRcvSuplDate", "FNSendSuplState", "FTPORef1", "FTPOLineItemNo"

                            If "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then
                                e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                                e.Handled = True
                                e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                            Else
                                e.Merge = False
                                e.Handled = True
                            End If
                        Case "FTSendSuplBy"

                            If ("" & .GetRowCellValue(e.RowHandle1, "FTSendSuplNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTSendSuplNo").ToString) Then

                                e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                                e.Handled = True
                                e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap

                            Else

                                e.Merge = False
                                e.Handled = True

                            End If

                        Case "FTRcvSuplBy"

                            If ("" & .GetRowCellValue(e.RowHandle1, "FTRcvSuplNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTRcvSuplNo").ToString) Then

                                e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                                e.Handled = True
                                e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap

                            Else

                                e.Merge = False
                                e.Handled = True

                            End If
                        Case "FTUnitSectCode"

                            If ("" & .GetRowCellValue(e.RowHandle1, "FTUnitSectCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTUnitSectCode").ToString) Then

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
                End If



            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        If VerifyData() Then

            Dim _FM As String = ""

            Dim strFoumalar As String = ""
            If Val(FNHSysBuyId.Properties.Tag.ToString()) > 0 Then
                If strFoumalar <> "" Then strFoumalar += " AND "
                strFoumalar += "{V_Send_RCVSupp_Report.FNHSysBuyId} = " & FNHSysBuyId.Properties.Tag.ToString() & ""
            End If

            If Val(FNHSysStyleId.Properties.Tag.ToString()) > 0 Then
                If strFoumalar <> "" Then strFoumalar += " AND "
                strFoumalar += "{V_Send_RCVSupp_Report.FNHSysStyleId} = " & Val(FNHSysStyleId.Properties.Tag.ToString()) & ""
            End If

            If Me.FTOrderNo.Text <> "" Then
                If strFoumalar <> "" Then strFoumalar += " AND "
                strFoumalar += " {V_Send_RCVSupp_Report.FTOrderNo} >= '" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            End If

            If Me.FTOrderNoTo.Text <> "" Then
                If strFoumalar <> "" Then strFoumalar += " AND "
                strFoumalar += " {V_Send_RCVSupp_Report.FTOrderNo} <= '" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
            End If

            If Me.cFDSendSuplDate.Text <> "" Then
                If strFoumalar <> "" Then strFoumalar += " AND "
                strFoumalar += " {V_Send_RCVSupp_Report.FDShipDate} >= '" & HI.UL.ULDate.ConvertEnDB(Me.cFDSendSuplDate.Text) & "'"
            End If

            If Me.FDEndSendSuplDate.Text <> "" Then
                If strFoumalar <> "" Then strFoumalar += " AND "
                strFoumalar += " {V_Send_RCVSupp_Report.FDShipDate} <= '" & HI.UL.ULDate.ConvertEnDB(Me.FDEndSendSuplDate.Text) & "'"
            End If

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Production\"
                .ReportName = "SendSuplReportTracking.rpt"
                .Formular = strFoumalar
                .Preview()
            End With

        End If
    End Sub

    Private Sub InitSummaryStartValue()
        Totalsum = 0
        GrpSum = 0
        _RowHandleHold = 0
        _RowHandleHoldChk = -1
    End Sub

    'Private Sub ogvtime_CustomSummaryCalculate(sender As Object, e As CustomSummaryEventArgs) Handles ogvtime.CustomSummaryCalculate

    '    Try
    '        If e.SummaryProcess = CustomSummaryProcess.Start Then
    '            InitSummaryStartValue()
    '        End If
    '        With ogvtime
    '            Select Case CType(e.Item, GridSummaryItem).FieldName.ToString
    '                Case "FNSendQuantity"
    '                    If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
    '                        If e.IsTotalSummary Then
    '                            If e.RowHandle <> _RowHandleHold Or e.RowHandle = 0 Then
    '                                If (.GetRowCellValue(e.RowHandle, "FTSizeBreakDown").ToString <> .GetRowCellValue(_RowHandleHold, "FTSizeBreakDown").ToString) Or (e.RowHandle = _RowHandleHold And e.RowHandle <> _RowHandleHoldChk) Then
    '                                    Totalsum = Totalsum + Integer.Parse(Val(e.FieldValue.ToString))
    '                                End If
    '                                _RowHandleHold = e.RowHandle
    '                                _RowHandleHoldChk = e.RowHandle
    '                            End If
    '                        End If
    '                        e.TotalValue = Totalsum
    '                    End If
    '            End Select
    '        End With
    '    Catch ex As Exception

    '    End Try
    'End Sub
End Class