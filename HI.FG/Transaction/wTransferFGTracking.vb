Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Drawing


Public Class wTransferFGTracking
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_INVEN
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private StateCal As Boolean = False
    Private _ProcLoad As Boolean = False
    Private _ListCarton As wListCarton

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Call InitFormControl()

        _ListCarton = New wListCarton
        HI.TL.HandlerControl.AddHandlerObj(_ListCarton)
        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.InsertObjectLanguage(HI.ST.SysInfo.ModuleName, _ListCarton.Name.ToString.Trim, _ListCarton)
        Catch ex As Exception
        End Try
    End Sub
#Region "Property"

    Private _WHID As Integer
    Public Property WH As Integer
        Get
            Return _WHID
        End Get
        Set(value As Integer)
            _WHID = value
        End Set
    End Property

    Private _WHIDTo As Integer
    Public Property WHTo As Integer
        Get
            Return _WHIDTo
        End Get
        Set(value As Integer)
            _WHIDTo = value
        End Set
    End Property

    Private _OrderNo As String = ""
    Public Property OrderNo As String
        Get
            Return _OrderNo
        End Get
        Set(value As String)
            _OrderNo = value
        End Set
    End Property

    Private _OrderNoTo As String = ""
    Public Property OrderNoTo As String
        Get
            Return _OrderNoTo
        End Get
        Set(value As String)
            _OrderNoTo = value
        End Set
    End Property

    Private _FNPriceTrans As Double = -1
    Public Property FNPriceTrans As Double
        Get
            Return _FNPriceTrans
        End Get
        Set(value As Double)
            _FNPriceTrans = value
        End Set
    End Property

    Private _DocRefNo As String = ""
    Public Property DocRefNo As String
        Get
            Return _DocRefNo
        End Get
        Set(value As String)
            _DocRefNo = value
        End Set
    End Property

    Private _AssPath As String = ""
    Public Property AssPath As String
        Get
            Return _AssPath
        End Get
        Set(value As String)
            _AssPath = value
        End Set
    End Property

    Private _FormName As String = ""
    Public Property FormName As String
        Get
            Return _FormName
        End Get
        Set(value As String)
            _FormName = value
        End Set
    End Property

    Private _FormObjID As Integer = 0
    Public Property FormObjID As Integer
        Get
            Return _FormObjID
        End Get
        Set(value As Integer)
            _FormObjID = value
        End Set
    End Property

    Private _FormPopup As String = ""
    Public Property FormPopup As String
        Get
            Return _FormPopup
        End Get
        Set(value As String)
            _FormPopup = value
        End Set
    End Property

    Private _ObjectFocus As Object = Nothing
    Public Property ObjectFocus As Object
        Get
            Return _ObjectFocus
        End Get
        Set(value As Object)
            _ObjectFocus = value
        End Set
    End Property

    Private _SysDBName As String = ""
    Public Property SysDBName As String
        Get
            Return _SysDBName
        End Get
        Set(value As String)
            _SysDBName = value
        End Set
    End Property

    Private _SysTableName As String = ""
    Public Property SysTableName As String
        Get
            Return _SysTableName
        End Get
        Set(value As String)
            _SysTableName = value
        End Set
    End Property

    Private _SysDocType As String = ""
    Public Property SysDocType As String
        Get
            Return _SysDocType
        End Get
        Set(value As String)
            _SysDocType = value
        End Set
    End Property

    Private _TableName As String = ""
    Public Property TableName As String
        Get
            Return _TableName
        End Get
        Set(value As String)
            _TableName = value
        End Set
    End Property

    Private _MainKeyID As String = ""
    Public Property MainKeyID As String
        Get
            Return _MainKeyID
        End Get
        Set(value As String)
            _MainKeyID = value
        End Set
    End Property

    Public ReadOnly Property MainKey As String
        Get
            Return _FormHeader(0).MainKey
        End Get
    End Property

    Private _RequireField As String = ""
    Public Property RequireField As String
        Get
            Return _RequireField
        End Get
        Set(value As String)
            _RequireField = value
        End Set
    End Property

    Public ReadOnly Property Query As String
        Get
            Return _FormHeader(0).Query
        End Get
    End Property

    Private _ProcComplete As Boolean = False
    Public Property ProcComplete As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(value As Boolean)
            _ProcComplete = value
        End Set
    End Property

    Private _Parent_Form As Object
    Public Property Parent_Form As Object
        Get
            Return _Parent_Form
        End Get
        Set(value As Object)
            _Parent_Form = value
        End Set
    End Property

#End Region

#Region "Procedure"

    Private Sub InitFormControl()

        Dim _Str As String = ""
        Dim _objId As Integer
        Dim _dt As DataTable
        Dim _StrQuery As String = ""
        Dim _SortField As String = ""
        Dim _ColCount As Integer = 0
        Dim _StartX As Double = 0
        Dim _StartY As Double = 0
        Dim _CtrLv As Double = -1
        Dim _CtrHeight As Double = 0
        Dim _dtgrpobj As New DataTable


        _Str = "SELECT TOP 1 FTBaseName,FTTableName AS FHSysTableName,FNFormObjID,FTBaseName + '.' + FTPrefix + '.' + FTTableName AS FTTableName,FTSortField,FNFormPopUpWidth,FNFormPopUpHeight  "
        _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTableObjForm WITH(NOLOCK) "
        _Str &= vbCrLf & " WHERE FTDynamicFormName='" & HI.UL.ULF.rpQuoted(Me.Name) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

        If _dt.Rows.Count > 0 Then

            _objId = Integer.Parse(_dt.Rows(0)!FNFormObjID.ToString)
            Me.SysDBName = _dt.Rows(0)!FTBaseName.ToString
            Me.SysTableName = _dt.Rows(0)!FHSysTableName.ToString
            Me.TableName = _dt.Rows(0)!FTTableName.ToString

            _SortField = _dt.Rows(0)!FTSortField.ToString

            _Str = "   SELECT       FTBaseName, FTPrefix, FTTableName, FNGrpObjID, FNGrpObjSeq, FNFormObjID, FNGenFormObj, FNGenFormObjSeq, FTDynamicFormName, FTSortField, "
            _Str &= vbCrLf & "  FNFormWidth, FNFormHeight, FNFormPopUpWidth, FNFormPopUpHeight, FTAssemBlyName, FTAssFormName, FTPropertyInfo"
            _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTableObjForm  WITH(NOLOCK)  "
            _Str &= vbCrLf & " WHERE        (FNGrpObjID =" & _objId & ")"
            _Str &= vbCrLf & " ORDER BY  CASE WHEN FNFormObjID=" & _objId & " THEN 0 ELSE 1 END,FNGrpObjSeq"
            _dtgrpobj = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)


            _Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.SP_GET_DYNAMIC_OBJECT_CONTROL " & _objId & ""
            _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)
            If _dt.Rows.Count > 0 Then

                For Each Row As DataRow In _dtgrpobj.Rows
                    Select Case Row!FNGenFormObj.ToString
                        Case "H"
                            Dim _DMF As New HI.TL.DynamicForm(_objId, Val(Row!FNFormObjID.ToString), _dt, Me)
                            _DMF.SysObjID = Val(Row!FNFormObjID.ToString)
                            _DMF.SysTableName = Row!FTTableName.ToString
                            _DMF.SysDBName = Row!FTBaseName.ToString
                            _FormHeader.Add(_DMF)

                    End Select
                Next

            End If

        End If

        _dt.Dispose()
        _dtgrpobj.Dispose()

    End Sub

    Private Sub FormRefresh()
        HI.TL.HandlerControl.ClearControl(Me)

        For Each Obj As Object In Me.Controls.Find(Me.MainKey, True)
            Select Case HI.ENM.Control.GeTypeControl(Obj)
                Case ENM.Control.ControlType.ButtonEdit
                    With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                        .Focus()
                    End With
            End Select
        Next

    End Sub

    Private Sub LoadWH(key As String)
        Dim _Qry As String = ""
        Dim _dt As DataTable

        _Qry = "SELECT W.FTWHFGCode FROM"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "]..TFGTransferFG as F WITH(NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMWarehouseFG AS W WITH(NOLOCK) ON F.FNHSysWHIdFG=W.FNHSysWHFGId"
        _Qry &= vbCrLf & "WHERE F.FTTransferFGNo='" & key.ToString & "'"
        _Qry &= vbCrLf & "AND F.FNHSysWHIdFG=W.FNHSysWHFGId"
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_FG)

        For Each R As DataRow In _dt.Rows
            FNHSysWHIdFG.Text = R!FTWHFGCode.ToString
        Next

        _Qry = "SELECT W.FTWHFGCode FROM"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "]..TFGTransferFG as F WITH(NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMWarehouseFG AS W WITH(NOLOCK) ON F.FNHSysWHIdFGTo=W.FNHSysWHFGId"
        _Qry &= vbCrLf & "WHERE F.FTTransferFGNo='" & key.ToString & "'"
        _Qry &= vbCrLf & "AND F.FNHSysWHIdFGTo=W.FNHSysWHFGId"
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_FG)

        For Each R As DataRow In _dt.Rows
            FNHSysWHIdFGTo.Text = R!FTWHFGCode.ToString
        Next

    End Sub


#End Region

    Private Sub wTransferFG_Load(sender As Object, e As EventArgs) Handles Me.Load


    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadGridDetail()
        Dim _Spls As New HI.TL.SplashScreen("Please Waiting Loading Data..!!!")
        Try

            Dim _Qry As String = ""
            Dim _dt As DataTable


            _Qry = "SELECT   F.FDDateTransferFG AS FDDate , F.FTTransferFGNo,  F.FNHSysWHIdFG, F.FNHSysWHIdFGTo, F.FTStateApprove, F.FTApproveBy, F.FDApproveDate, F.FTCancelBy, F.FDCancelDate, F.FTCancelTime, D.FTBarCodeCarton, "
            _Qry &= vbCrLf & " D.FNQuantity, D.FTPackNo, D.FNCartonNo, W.FTWHFGCode, X.FTWHFGCode AS FTWHFGCodeTo, C.FTCmpCode,  P.FTCmpCode AS FTCmpCodeTo"
            _Qry &= vbCrLf & " ,B.FTOrderNo , B.FTColorWay as FTColorway , B.FTSizeBreakDown ,B.FNCartonNo , B.FTPackNo , O.FTPORef , S.FTStyleCode "
            _Qry &= vbCrLf & ",Case When Isdate(F.FDDateTransferFG)= 1 Then convert(varchar(10),convert(date,F.FDDateTransferFG),103) Else '' End AS FDDateTransferFG "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ", W.FTWHFGNameTH as FTWHFGName , X.FTWHFGNameTH AS FTWHFGNameTo , C.FTCmpNameTH AS FTCmpName , P.FTCmpNameTH AS FTCmpNameTo "
            Else
                _Qry &= vbCrLf & ", W.FTWHFGNameEN as FTWHFGName , X.FTWHFGNameEN AS FTWHFGNameTo , C.FTCmpNameEN AS FTCmpName , P.FTCmpNameEN AS FTCmpNameTo "
            End If
            _Qry &= vbCrLf & ", isnull(F.FTStateApprove,'') AS  FTStateApprove  , Case when  Isnull(F.FTStateApprove,'') ='1' then  F.FTApproveBy When Isnull(F.FTStateApprove,'') ='0' Then F.FTCancelBy Else ''   End AS FTActiveBy "
            _Qry &= vbCrLf & ",Case when  Isnull(F.FTStateApprove,'') ='1' then  convert(varchar(10),convert(date,F.FDApproveDate),103) When Isnull(F.FTStateApprove,'') ='0' Then convert(varchar(10),convert(date,F.FDCancelDate),103) Else ''   End AS FTActiveDate"
            _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "]..TFGTransferFG AS F WITH (NOLOCK) LEFT OUTER JOIN"
            _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "]..TFGTransferFG_Detail AS D WITH (NOLOCK) ON F.FTTransferFGNo = D.FTTransferFGNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseFG AS W WITH (NOLOCK) ON F.FNHSysWHIdFG = W.FNHSysWHFGId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseFG AS X WITH (NOLOCK) ON F.FNHSysWHIdFGTo = X.FNHSysWHFGId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH (NOLOCK) ON W.FNHSysCmpId = C.FNHSysCmpId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS P WITH (NOLOCK) ON X.FNHSysCmpId = P.FNHSysCmpId"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBarcodeScanFG AS B WITH(NOLOCK) ON D.FTBarCodeCarton = B.FTBarCodeCarton and D.FTPackNo = B.FTPackNo and D.FNCartonNo = B.FNCartonNo"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS O WITH(NOLOCK) ON B.FTOrderNo = O.FTOrderNo "
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle AS S WITH(NOLOCK) ON O.FNHSysStyleId = S.FNHSysStyleId"
            _Qry &= vbCrLf & "Where F.FTTransferFGNo <> ''"
            If Me.FNHSysCmpId.Text <> "" Then
                _Qry &= vbCrLf & " And C.FTCmpCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysCmpId.Text) & "'"
            End If
            If Me.FNHSysCmpIdTo.Text <> "" Then
                _Qry &= vbCrLf & " And P.FTCmpCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysCmpIdTo.Text) & "'"
            End If
            If Me.FNHSysWHIdFG.Text <> "" Then
                _Qry &= vbCrLf & " And C.FTCmpCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysWHIdFG.Text) & "'"
            End If
            If Me.FNHSysWHIdFGTo.Text <> "" Then
                _Qry &= vbCrLf & " And C.FTCmpCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysWHIdFGTo.Text) & "'"
            End If
            If Me.FTStartDate.Text <> "" Then
                _Qry &= vbCrLf & " And  F.FDDateTransferFG >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
            End If
            If Me.FTEndDate.Text <> "" Then
                _Qry &= vbCrLf & " And  F.FDDateTransferFG <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
            End If
            _Qry &= vbCrLf & "Order by  FDDate  ASC , F.FTTransferFGNo ASC ,C.FTCmpCode ASC , P.FTCmpCode ASC  "
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_FG)
            ogcDetail.DataSource = _dt
            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        If VerrifyData() Then
            Call LoadGridDetail()
        End If
    End Sub

    Private Function VerrifyData() As Boolean
        Try
            Dim _State As Boolean = False
            Dim _F As String = "FNHSysCmpId|FNHSysCmpIdTo|FNHSysWHIdFG|FNHSysWHIdFGTo|FTStartDate|FTEndDate"
            For Each x As String In _F.Split("|")
                For Each _obj As Object In oCriteria.Controls.Find(x.ToString, True)
                    Select Case _obj.GetType.ToString.ToUpper
                        Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                            With CType(_obj, DevExpress.XtraEditors.ButtonEdit)
                                If .Text <> "" Then
                                    _State = True
                                End If
                            End With
                        Case "DevExpress.XtraEditors.DateEdit".ToUpper
                            With CType(_obj, DevExpress.XtraEditors.DateEdit)
                                If .Text <> "" Then
                                    _State = True
                                End If
                            End With
                    End Select
                Next
            Next
            If Not (_State) Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysCmpId_lbl.Text)
                Me.FNHSysCmpId.Focus()
            End If
            Return _State
        Catch ex As Exception
        End Try
    End Function

    Private Sub ogvDetail_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) ' Handles ogvDetail.CellMerge
        With ogvDetail
            Select Case e.Column.FieldName
                Case "FTColorway"
                    If "" & .GetRowCellValue(e.RowHandle1, "FTTransferFGNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTTransferFGNo").ToString _
                        And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then
                        e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                        e.Handled = True
                        e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                    Else
                        e.Merge = False
                        e.Handled = True
                    End If
                Case "FTSizeBreakDown"

                    If ("" & .GetRowCellValue(e.RowHandle1, "FTTransferFGNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTTransferFGNo").ToString) _
                        And ("" & .GetRowCellValue(e.RowHandle1, "FTColorway").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTColorway").ToString) _
                        And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                        e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                        e.Handled = True
                        e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                    Else
                        e.Merge = False
                        e.Handled = True
                    End If
            End Select
        End With

    End Sub

    Private Sub ogvDetail_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles ogvDetail.RowStyle
        Try
            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then
                Dim _State As String = View.GetRowCellValue(e.RowHandle, View.Columns("FTStateApprove"))
                Dim _By As String = View.GetRowCellValue(e.RowHandle, View.Columns("FTActiveBy"))
                Dim _Date As String = View.GetRowCellValue(e.RowHandle, View.Columns("FTActiveDate"))
                Select Case True
                    Case _State = "1" And _By <> "" And _Date <> ""
                        e.Appearance.BackColor = Color.LightGreen
                        e.Appearance.BackColor2 = Color.White
                    Case _State = "0" And _By <> "" And _Date <> ""
                        e.Appearance.BackColor = Color.Salmon
                        e.Appearance.BackColor2 = Color.SeaShell
                    Case _State = "" And _By = "" And _Date = ""
                        e.Appearance.BackColor = Color.LightGray
                        e.Appearance.BackColor2 = Color.White
                End Select
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class