Imports DevExpress.Data
Imports System.IO

Public Class wMEDStockOnhand

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'Dim oSysLang As New ST.SysLanguage
        'Try
        '    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _ListBarcodeOnhand.Name.ToString.Trim, _ListBarcodeOnhand)
        'Catch ex As Exception
        'Finally
        'End Try
        'HI.TL.HandlerControl.AddHandlerObj(_ListBarcodeOnhand)
        Call InitGrid()
    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "FNQuantity"

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = "FNQuantity"

        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

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
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit.ToString & "}"
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
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n" & HI.ST.Config.QtyDigit.ToString & "})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

            .ExpandAllGroups()
            .RefreshData()


        End With
        '------End Add Summary Grid-------------
    End Sub
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
 
        Dim _Cmd As String = ""
        Dim _dt As DataTable
       
        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

        _Cmd = "Select  T.FNHSysDrugId, Sum(T.FNQuantity) AS  FNQuantity   , D.FTDrugCode  , U.FTDrugUnitCode"
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Cmd &= vbCrLf & ", D.FTDrugNameTH  AS FTDrugName"
        Else
            _Cmd &= vbCrLf & ", D.FTDrugNameEN  AS FTDrugName"
        End If
        _Cmd &= vbCrLf & " From (SELECT     FTMEDRcvNo, FNHSysDrugId, FNQuantity"
        _Cmd &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTRecieve_Detail WITH (NOLOCK)"
        _Cmd &= vbCrLf & "WHERE LEFT(FTMEDRcvNo,2) = '" & Microsoft.VisualBasic.Left(HI.ST.SysInfo.CmpRunID, 2) & "'"
        _Cmd &= vbCrLf & "       UNION ALL"
        _Cmd &= vbCrLf & "SELECT     FTDocumentRefNo, FNHSysDrugId,-FNQuantity"
        _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTDrugPay WITH (NOLOCK)   "
        _Cmd &= vbCrLf & "WHERE LEFT(FTDocumentRefNo,2) = '" & Microsoft.VisualBasic.Left(HI.ST.SysInfo.CmpRunID, 2) & "'"
        _Cmd &= vbCrLf & ") AS T LEFT OUTER JOIN "
        _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMECMDrug AS D WITH(NOLOCK) ON T.FNHSysDrugId = D.FNHSysDrugId"
        _Cmd &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMECMDrugUnit AS U WITH(NOLOCK) ON D.FNHSysDrugUnitId_Rcv = U.FNHSysDrugUnitId"
        _Cmd &= vbCrLf & "WHERE D.FTDrugCode <>''"
        If Me.FNHSysDrugId.Text <> "" Then
            _Cmd &= vbCrLf & "AND D.FTDrugCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysDrugId.Text) & "'"
        End If
        If Me.FNHSysDrugIdTo.Text <> "" Then
            _Cmd &= vbCrLf & "AND D.FTDrugCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysDrugIdTo.Text) & "'"
        End If
        _Cmd &= vbCrLf & "Group by  T.FNHSysDrugId,  D.FTDrugCode , D.FTDrugNameEN , D.FTDrugNameTH , U.FTDrugUnitCode"
        _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MEDC)

        Me.ogdtime.DataSource = _dt
        _Spls.Close()

        _RowDataChange = False

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False
 
        If Me.FNHSysDrugId.Text <> "" Then
            _Pass = True
        End If
        If Me.FNHSysDrugIdTo.Text <> "" Then
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
            
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvtime)
            StateCal = False
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
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

 

End Class