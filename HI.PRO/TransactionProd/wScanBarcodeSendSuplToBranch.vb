Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid

Public Class wScanBarcodeSendSuplToBranch

    Private _DBEnum As HI.Conn.DB.DataBaseName = Conn.DB.DataBaseName.DB_PROD
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()

    Private _DataInfo As DataTable
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Private _ProcLoad As Boolean = False

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.



        Call InitGrid()

    End Sub

#Region "Initial Grid"

    Private Sub InitGrid()

        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = "FTBarcodeSendSuplNo"
        Dim sFieldSum As String = "FNQuantity"
        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""

        With ogvdetail
            .ClearGrouping()
            .ClearDocument()

            For Each Str As String In sFieldCount.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
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

            For Each Str As String In sFieldGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True

        End With
        '------End Add Summary Grid-------------
    End Sub

#End Region

#Region "Property"


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

    Public Sub LoadDataInfo(Key As Object)
        _ProcLoad = True

        Dim _Dt As DataTable
        Dim _Str As String = ""


        _Str = "  SELECT  TOP 1   FTSendSuplNo, FDSendSuplDate, FTSendSuplBy"
        _Str &= vbCrLf & " , ISNULL(("
        _Str &= vbCrLf & " SELECT TOP 1 FTSuplCode "
        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS X WITH(NOLOCK)"
        _Str &= vbCrLf & " WHERE X.FNHSysSuplId = A.FNHSysSuplId "
        _Str &= vbCrLf & " ),'') AS  FNHSysSuplId"
        _Str &= vbCrLf & " , FNSendSuplState, FTRemark, "
        _Str &= vbCrLf & " FTStateBranchAccept, FTStateBranchAcceptBy, FTStateBranchAcceptDate, FTStateBranchAcceptTime"
        _Str &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch AS A  WITH(NOLOCK)"
        _Str &= vbCrLf & "  WHERE FTSendSuplNo='" & HI.UL.ULF.rpQuoted(Key) & "'"

        _Dt = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

        Dim _FieldName As String = ""
        For Each R As DataRow In _Dt.Rows
            For Each Col As DataColumn In _Dt.Columns
                _FieldName = Col.ColumnName.ToString

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                .Text = R.Item(Col).ToString
                            End With

                        Case ENM.Control.ControlType.CalcEdit
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                .Value = Val(R.Item(Col).ToString)
                            End With
                        Case ENM.Control.ControlType.ComboBoxEdit
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                Try
                                    .SelectedIndex = Val(R.Item(Col).ToString)
                                Catch ex As Exception
                                    .SelectedIndex = -1
                                End Try
                            End With
                        Case ENM.Control.ControlType.CheckEdit
                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                .EditValue = (Integer.Parse(Val(R.Item(Col).ToString))).ToString
                            End With
                        Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit
                            Obj.Text = R.Item(Col).ToString
                        Case ENM.Control.ControlType.PictureEdit
                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                Try
                                    .Image = HI.UL.ULImage.LoadImage("" & .Properties.Tag.ToString & R.Item(Col).ToString) ' hImage ' ' Image.FromFile("" & .Properties.Tag.ToString & R.Item(Col).ToString)
                                Catch ex As Exception
                                    .Image = Nothing
                                End Try
                            End With
                        Case ENM.Control.ControlType.DateEdit
                            Try

                                With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                    .Text = HI.UL.ULDate.ConvertEN(R.Item(Col).ToString)
                                End With
                            Catch ex As Exception
                            End Try
                        Case Else
                            Obj.Text = R.Item(Col).ToString
                    End Select
                Next
            Next

            Exit For
        Next

        Call LoadDucumentDetail(Key.ToString)

        olberror.Text = ""
        _ProcLoad = False
    End Sub

    Private Sub LoadDucumentDetail(Key As String)

        Try
            Dim _Qry As String = ""
            Dim dt As DataTable

            _Qry = " SELECT  case when isnull(B.FNBalQuantity,0) > 0 then '1' else '0' end  AS FTSelect , B.FTBarcodeSendSuplNo,B.FNBunbleSeq"
            _Qry &= vbCrLf & " 	, B.FNHSysPartId"
            _Qry &= vbCrLf & " 	, B.FNSendSuplType"
            _Qry &= vbCrLf & " 	, A.FNHSysSuplId"
            _Qry &= vbCrLf & " 	, B.FTBarcodeBundleNo"
            _Qry &= vbCrLf & " 	, O.FTOrderNo "
            _Qry &= vbCrLf & " 	, B.FTOrderProdNo"
            _Qry &= vbCrLf & " 	, B.FTSendSuplRef"
            _Qry &= vbCrLf & " 	, A.FNHSysCmpId"
            _Qry &= vbCrLf & " 	, S.FTSuplCode"
            _Qry &= vbCrLf & " 	, B.FTSendSuplNo"
            _Qry &= vbCrLf & " 	, B.FNBunbleSeq"
            _Qry &= vbCrLf & " 	, B.FTColorway"
            _Qry &= vbCrLf & " 	, B.FTSizeBreakDown"
            _Qry &= vbCrLf & " 	, B.FNQuantity"
            _Qry &= vbCrLf & " 	, ST.FTStyleCode"
            _Qry &= vbCrLf & " 	, MP.FTPartCode "
            _Qry &= vbCrLf & " 	, B.FNHSysOperationId"
            _Qry &= vbCrLf & " 	, B.FNSeq"
            _Qry &= vbCrLf & " 	, B.FNHSysOperationIdTo"
            _Qry &= vbCrLf & " 	, Mpp.FTOperationCode "
            _Qry &= vbCrLf & " , case when isnull(B.FNBalQuantity,0) > 0 then B.FNBalQuantity else 0 end  AS FNQuantityBal "
            _Qry &= vbCrLf & " , isnull(B.FNBalQuantity,0)  AS FNBalOrig"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " 	,MP.FTPartNameTH AS FTPartName "
                _Qry &= vbCrLf & " 	,MPP.FTOperationNameTH  AS FTOperationName"
            Else
                _Qry &= vbCrLf & " 	,MP.FTPartNameEN AS FTPartName"
                _Qry &= vbCrLf & " 	,MPP.FTOperationNameEN AS FTOperationName"
            End If

            _Qry &= vbCrLf & " , B.FNHSysMarkId  "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ",  CMK.FTMarkNameTH AS FTMarkName  "
            Else
                _Qry &= vbCrLf & ",  CMK.FTMarkNameEN AS FTMarkName  "
            End If
            _Qry &= vbCrLf & " ,convert(varchar(10) , convert( date , B.FDInsDate) , 103 ) as FDInsDate "
            _Qry &= vbCrLf & " ,B.FTInsTime  "

            _Qry &= vbCrLf & " 	 FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch AS A WITH (NOLOCK)   INNER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch_Barcode AS B  WITH (NOLOCK) ON A.FTSendSuplNo = B.FTSendSuplNo INNER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON B.FTOrderNo=O.FTOrderNo INNER JOIN "
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH (NOLOCK)  ON O.FNHSysStyleId = ST.FNHSysStyleId LEFT OUTER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH (NOLOCK) ON A.FNHSysSuplId = S.FNHSysSuplId"

            _Qry &= vbCrLf & " 		    LEFT OUTER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart  AS MP WITH (NOLOCK) ON B.FNHSysPartId = MP.FNHSysPartId"
            _Qry &= vbCrLf & " 	 LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd AS OPP WITH (NOLOCK)  ON B.FTOrderProdNo = OPP.FTOrderProdNo AND B.FNHSysOperationId = OPP.FNHSysOperationId "
            _Qry &= vbCrLf & " 	 LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS MPP WITH (NOLOCK)  ON  B.FNHSysOperationId  = MPP.FNHSysOperationId"

            _Qry &= vbCrLf & "     LEFT OUTER JOIN        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS CMK  WITH (NOLOCK)  ON B.FNHSysMarkId = CMK.FNHSysMarkId"

            _Qry &= vbCrLf & "   WHERE B.FTSendSuplNo='" & HI.UL.ULF.rpQuoted(Key) & "' --and isnull(B.FTStateBranchAccept,'0') = '1' "
            _Qry &= vbCrLf & "  ORDER BY B.FTBarcodeSendSuplNo "


            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            Me.ogcdetail.DataSource = dt.Copy

        Catch ex As Exception

        End Try


    End Sub


    Private Function CheckNotUsed(Key As String) As Boolean
        Dim _Str As String = ""
        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For I As Integer = 0 To _FormHeader(cind).CheckDelFiled.ToArray.Count - 1
                _Str = _FormHeader(cind).CheckDelFiled.Item(I).Query & "'" & HI.UL.ULF.rpQuoted(Key) & "' "

                If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SYSTEM, "") <> "" Then
                    HI.MG.ShowMsg.mProcessError(1301180001, "", Me.Text, MessageBoxIcon.Warning)
                    Return False
                End If

            Next
        Next
        Return True
    End Function

    Private Function VerrifyData() As Boolean
        Dim _FieldName As String
        Dim _Val As String = ""
        Dim _Caption As String = ""
        Dim _Str As String = ""
        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For I As Integer = 0 To _FormHeader(cind).CheckFiled.ToArray.Count - 1
                _FieldName = _FormHeader(cind).CheckFiled(I).FiledName.ToString
                _Caption = ""

                For Each ObjCaption As Object In Me.Controls.Find(_FieldName & "_lbl", True)
                    If HI.ENM.Control.GeTypeControl(ObjCaption) = ENM.Control.ControlType.LabelControl Then
                        _Caption = ObjCaption.Text
                        Exit For
                    End If
                Next

                Dim Pass As Boolean = True

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                If .Properties.Buttons.Count <= 1 Then
                                    If .Text.Trim() = "" Or "" & .Properties.Tag.ToString = "" Then
                                        Pass = False
                                    End If
                                End If
                            End With
                        Case ENM.Control.ControlType.CalcEdit
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                If Val(.Value.ToString) <= 0 Then
                                    Pass = False
                                End If
                            End With
                        Case ENM.Control.ControlType.ComboBoxEdit
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                If .SelectedIndex < 0 Then Pass = False
                            End With
                        Case ENM.Control.ControlType.CheckEdit

                        Case ENM.Control.ControlType.DateEdit
                            With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                If HI.UL.ULDate.CheckDate(.Text) = "" Then
                                    Pass = False
                                End If
                            End With
                        Case ENM.Control.ControlType.PictureEdit
                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                If .Image Is Nothing Then
                                    Pass = False
                                End If
                            End With
                        Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit
                            If Obj.Text = "" Then
                                Pass = False
                            End If
                        Case Else
                            Pass = False
                    End Select

                    If Pass = False Then
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, _Caption)
                        Obj.Focus()
                        Return False
                    End If
                Next
            Next
        Next


        '---------- Validate Document ---------------------
        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For Each Obj As Object In Me.Controls.Find(_FormHeader(cind).MainKey, True)
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.ButtonEdit
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            If .Text.Trim() = "" Then
                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                Obj.Focus()
                                Return False
                            Else
                                Dim _CmpH
                                For Each ctrl As Object In Me.Controls.Find("FNHSysCmpId", True)

                                    Select Case HI.ENM.Control.GeTypeControl(ctrl)
                                        Case ENM.Control.ControlType.ButtonEdit
                                            With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End With

                                            Exit For
                                        Case ENM.Control.ControlType.TextEdit
                                            With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End With

                                            Exit For
                                    End Select

                                Next

                                If .Text <> HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", True, _CmpH).ToString() Then
                                    _Str = _FormHeader(cind).Query & "  WHERE " & _FormHeader(cind).MainKey & "='" & HI.UL.ULF.rpQuoted(.Text) & "' "
                                    Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

                                    If _dt.Rows.Count <= 0 Then
                                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                        Obj.Focus()
                                        Return False
                                    End If
                                End If
                            End If
                        End With

                End Select
            Next
        Next


        Return True
    End Function


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

    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTStateBranchAcceptBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Or FTStateBranchAcceptBy.Text.Trim = "" Then
            Return True
        Else
            HI.MG.ShowMsg.mProcessError(1508110911, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If
    End Function


    Private Function CheckTransferToBranchAccept() As Boolean

        Dim _Qry As String

        _Qry = "SELECT TOP 1  FTStateBranchAccept "
        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTSendSuplNo='" & HI.UL.ULF.rpQuoted(FTSendSuplNo.Text) & ""

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "1" Then
            Return True
        Else
            HI.MG.ShowMsg.mProcessError(1508119111, "สบข้อมูลสาขาทำการรับแล้ว ไม่สามารถเพิ่มหรือแก้ไขได้ !!! ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If

    End Function

#End Region

#Region "MAIN PROC"



    Private Sub Proc_Clear(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        Me.FormRefresh()
        olberror.Text = ""
        Me.FNHSysCmpIdTo.Text = HI.ST.SysInfo.CmpCode
        Me.FNHSysCmpIdTo.Properties.Tag = HI.ST.SysInfo.CmpID
    End Sub

    Private Sub Proc_Preview(sender As System.Object, e As System.EventArgs)
        If Me.FTSendSuplNo.Text <> "" Then
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Production\"
                .ReportName = "SendSuplSlip.rpt"
                .Formular = "{TPRODTSendSupl.FTSendSuplNo}='" & HI.UL.ULF.rpQuoted(FTSendSuplNo.Text) & "' "
                .Preview()
            End With
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTSendSuplNo_lbl.Text)
            FTSendSuplNo.Focus()
        End If
    End Sub

    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Proc "

#End Region

    Private Sub FTBarcodeNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTBarcodeNo.EditValueChanged

    End Sub

    Private Sub ogcdetail_Click(sender As Object, e As EventArgs) Handles ogcdetail.Click

    End Sub

    Private Sub ogvdetail_RowCountChanged(sender As Object, e As EventArgs) Handles ogvdetail.RowCountChanged
        Try

            Dim dt As New DataTable

            Try
                dt = CType(ogcdetail.DataSource, DataTable).Copy
            Catch ex As Exception
            End Try

            Me.FNHSysSuplId.Properties.ReadOnly = (dt.Rows.Count > 0)
            FNHSysSuplId.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)

            Me.FNSendSuplState.Properties.ReadOnly = (dt.Rows.Count > 0)
            dt.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub wScanBarcodeSendSupl_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FNHSysCmpIdTo.Text = HI.ST.SysInfo.CmpCode
        Me.FNHSysCmpIdTo.Properties.Tag = HI.ST.SysInfo.CmpID
        FTBarcodeNo.EnterMoveNextControl = False
        TextEdit10.EnterMoveNextControl = False
    End Sub


    Private Sub FTSendSuplNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTSendSuplNo.EditValueChanged
        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTSendSuplNo_EditValueChanged), New Object() {sender, e})
        Else
            Call LoadDataInfo(FTSendSuplNo.Text)
        End If
    End Sub

    Private Sub ocmapprove_Click(sender As Object, e As EventArgs) Handles ocmapprove.Click
        If Me.FTStateBranchAccept.Checked Then
            'HI.MG.ShowMsg.mInfo("มีการอนุมัติแล้ว ไม่สามารถทำรายการได้ !!!! ", 1701100821, Me.Text, "", MessageBoxIcon.Warning)
            'Exit Sub
            If HI.MG.ShowMsg.mConfirmProcess("มีการอนุมัติแล้ว ต้องการอนุมัติ  !!!!", 1703130925, "") = False Then
                Exit Sub
            End If
        End If
        If CheckOwner() = False Then Exit Sub

        Dim _Strsql As String
        With DirectCast(Me.ogcdetail.DataSource, DataTable)
            .AcceptChanges()
            If .Select("FTSelect='1'").Length <= 0 Then
                HI.MG.ShowMsg.mInfo("ไม่สามารถทำการอนุมัติแล้วได้ กรุณายิงบาร์โค้ดมัดงาน !!!! ", 1703021042, Me.Text, "", MessageBoxIcon.Warning)
                Exit Sub
            End If

            For Each R As DataRow In .Select("FTSelect='1'")
                _Strsql = "UPDate  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch_Barcode "
                _Strsql &= vbCrLf & " SET FTStateBranchAccept='1'"
                _Strsql &= vbCrLf & " ,FTStateBranchAcceptBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Strsql &= vbCrLf & " ,FTStateBranchAcceptDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Strsql &= vbCrLf & " ,FTStateBranchAcceptTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Strsql &= vbCrLf & " , FNBalQuantity=" & Double.Parse("0" & R!FNQuantityBal.ToString)
                _Strsql &= vbCrLf & " WHERE    FTSendSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTSendSuplNo.Text) & "' "
                _Strsql &= vbCrLf & "and FTBarcodeSendSuplNo = '" & R!FTBarcodeSendSuplNo.ToString & "'"
                HI.Conn.SQLConn.ExecuteOnly(_Strsql, Conn.DB.DataBaseName.DB_PROD)
            Next
        End With

        _Strsql = "UPDate  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch "
        _Strsql &= vbCrLf & " SET FTStateBranchAccept='1'"
        _Strsql &= vbCrLf & " ,FTStateBranchAcceptBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        _Strsql &= vbCrLf & " ,FTStateBranchAcceptDate=" & HI.UL.ULDate.FormatDateDB & " "
        _Strsql &= vbCrLf & " ,FTStateBranchAcceptTime=" & HI.UL.ULDate.FormatTimeDB & " "
        _Strsql &= vbCrLf & " WHERE    FTSendSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTSendSuplNo.Text) & "'  AND ISNULL(FTStateBranchAccept,'0')<>'1' "
        HI.Conn.SQLConn.ExecuteOnly(_Strsql, Conn.DB.DataBaseName.DB_PROD)

        _Strsql = "Select top 1  * From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl With(nolock) where FTSendSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTSendSuplNo.Text) & "'   "
        If HI.Conn.SQLConn.GetDataTable(_Strsql, Conn.DB.DataBaseName.DB_PROD).Rows.Count <= 0 Then
            _Strsql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl "
            _Strsql &= vbCrLf & "( FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTSendSuplNo, FDSendSuplDate, FTSendSuplBy, FNHSysSuplId, FNSendSuplState, FTRemark, FNHSysCmpId, FTStateSendExcel)"
            _Strsql &= vbCrLf & "SELECT FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTSendSuplNo, FDSendSuplDate, FTSendSuplBy, FNHSysSuplId, FNSendSuplState, FTRemark, FNHSysCmpId,  FTStateSendExcel"
            _Strsql &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch  "
            _Strsql &= vbCrLf & " WHERE    FTSendSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTSendSuplNo.Text) & "'  AND ISNULL(FTStateBranchAccept,'0')='1' and ( FNHSysCmpId <> FNHSysCmpIdTo ) "
            HI.Conn.SQLConn.ExecuteOnly(_Strsql, Conn.DB.DataBaseName.DB_PROD)
        End If
        _Strsql = "Select top 1  * From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode With(nolock) where FTSendSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTSendSuplNo.Text) & "'   "
        If HI.Conn.SQLConn.GetDataTable(_Strsql, Conn.DB.DataBaseName.DB_PROD).Rows.Count <= 0 Then
            _Strsql = "insert into [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode (FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTSendSuplNo, FTBarcodeSendSuplNo)"
            _Strsql &= vbCrLf & "Select   FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTSendSuplNo, FTBarcodeSendSuplNo"
            _Strsql &= vbCrLf & "From TPRODTSendSuplToBranch_Barcode"
            _Strsql &= vbCrLf & "where FTSendSuplNo in (SELECT FTSendSuplNo "
            _Strsql &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch  "
            _Strsql &= vbCrLf & " WHERE    FTSendSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTSendSuplNo.Text) & "'  AND ISNULL(FTStateBranchAccept,'0')='1' and ( FNHSysCmpId <> FNHSysCmpIdTo ))  "
            HI.Conn.SQLConn.ExecuteOnly(_Strsql, Conn.DB.DataBaseName.DB_PROD)
        End If

        Me.LoadDataInfo(Me.FTSendSuplNo.Text)
    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        If CheckOwner() = False Then Exit Sub


        Dim _Strsql As String
        _Strsql = "UPDate  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch "
        _Strsql &= vbCrLf & " SET FTStateBranchAccept='0'"
        _Strsql &= vbCrLf & " ,FTStateBranchAcceptBy='' "
        _Strsql &= vbCrLf & " ,FTStateBranchAcceptDate='' "
        _Strsql &= vbCrLf & " ,FTStateBranchCancelAccept='1'"
        _Strsql &= vbCrLf & " ,FTStateBranchCancelAcceptBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        _Strsql &= vbCrLf & " ,FTStateBranchCancelAcceptDate=" & HI.UL.ULDate.FormatDateDB & " "
        _Strsql &= vbCrLf & " ,FTStateBranchCancelAcceptTime=" & HI.UL.ULDate.FormatTimeDB & " "
        _Strsql &= vbCrLf & " WHERE    FTSendSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTSendSuplNo.Text) & "'  AND ISNULL(FTStateBranchAccept,'')='1' "
        HI.Conn.SQLConn.ExecuteOnly(_Strsql, Conn.DB.DataBaseName.DB_PROD)

        _Strsql = "UPDate  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch_Barcode "
        _Strsql &= vbCrLf & " SET FTStateBranchAccept='0'"
        _Strsql &= vbCrLf & " ,FTStateBranchAcceptBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        _Strsql &= vbCrLf & " ,FTStateBranchAcceptDate=" & HI.UL.ULDate.FormatDateDB & " "
        _Strsql &= vbCrLf & " ,FTStateBranchAcceptTime=" & HI.UL.ULDate.FormatTimeDB & " "
        _Strsql &= vbCrLf & " , FNBalQuantity=0"
        _Strsql &= vbCrLf & " WHERE    FTSendSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTSendSuplNo.Text) & "'  AND ISNULL(FTStateBranchAccept,'0')='1' "
        HI.Conn.SQLConn.ExecuteOnly(_Strsql, Conn.DB.DataBaseName.DB_PROD)

        Me.LoadDataInfo(Me.FTSendSuplNo.Text)
    End Sub

    Private Sub TextEdit10_KeyDown(sender As Object, e As KeyEventArgs) Handles TextEdit10.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Call LoadBarcodeInfo(TextEdit10.Text)
                Call DefalutScanChecked()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadBarcodeInfo(Key As String)

        Try
            Dim _Qry As String = ""
            Dim dt As DataTable
            Dim _StatePass As Boolean = False

            Me.FTSendSuplNo.Text = HI.Conn.SQLConn.GetField("Select Top 1 FTSendSuplNo From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch_Barcode Where FTBarcodeSendSuplNo='" & Key.ToString & "'", Conn.DB.DataBaseName.DB_PROD, "")
            'LoadDucumentDetail(Me.FTSendSuplNo.Text)



            If Me.FTSendSuplNo.Text = "" Then
                MG.ShowMsg.mInfo("กรุณาตรวจสอบ เลขที่บาร์โค้ดมัดงาน !!!! ", 1703021100, Me.Text)
                Exit Sub
            End If

            _Qry = " SELECT  Top 1  B.FTBarcodeSendSuplNo,B.FNBunbleSeq"
            _Qry &= vbCrLf & " 	, B.FNHSysPartId"
            _Qry &= vbCrLf & " 	, B.FNSendSuplType"
            _Qry &= vbCrLf & " 	, A.FNHSysSuplId"
            _Qry &= vbCrLf & " 	, B.FTBarcodeBundleNo"
            _Qry &= vbCrLf & " 	, O.FTOrderNo "
            _Qry &= vbCrLf & " 	, B.FTOrderProdNo"
            _Qry &= vbCrLf & " 	, B.FTSendSuplRef"
            _Qry &= vbCrLf & " 	, A.FNHSysCmpId"
            _Qry &= vbCrLf & " 	, S.FTSuplCode"
            _Qry &= vbCrLf & " 	, B.FTSendSuplNo"
            _Qry &= vbCrLf & " 	, B.FNBunbleSeq"
            _Qry &= vbCrLf & " 	, B.FTColorway"
            _Qry &= vbCrLf & " 	, B.FTSizeBreakDown"
            _Qry &= vbCrLf & " 	, B.FNQuantity"
            _Qry &= vbCrLf & " 	, ST.FTStyleCode"
            _Qry &= vbCrLf & " 	, MP.FTPartCode "
            _Qry &= vbCrLf & " 	, B.FNHSysOperationId"
            _Qry &= vbCrLf & " 	, B.FNSeq"
            _Qry &= vbCrLf & " 	, B.FNHSysOperationIdTo"
            _Qry &= vbCrLf & " 	, Mpp.FTOperationCode "
            _Qry &= vbCrLf & " 	,  B.FNQuantity AS FNQuantityBal "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " 	,MP.FTPartNameTH AS FTPartName "
                _Qry &= vbCrLf & " 	,MPP.FTOperationNameTH  AS FTOperationName"
            Else
                _Qry &= vbCrLf & " 	,MP.FTPartNameEN AS FTPartName"
                _Qry &= vbCrLf & " 	,MPP.FTOperationNameEN AS FTOperationName"
            End If

            _Qry &= vbCrLf & " , B.FNHSysMarkId  "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ",  CMK.FTMarkNameTH AS FTMarkName  "
            Else
                _Qry &= vbCrLf & ",  CMK.FTMarkNameEN AS FTMarkName  "
            End If

            _Qry &= vbCrLf & " 	 FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch AS A WITH (NOLOCK)   INNER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch_Barcode AS B  WITH (NOLOCK) ON A.FTSendSuplNo = B.FTSendSuplNo INNER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON B.FTOrderNo=O.FTOrderNo INNER JOIN "
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH (NOLOCK)  ON O.FNHSysStyleId = ST.FNHSysStyleId LEFT OUTER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH (NOLOCK) ON A.FNHSysSuplId = S.FNHSysSuplId"

            _Qry &= vbCrLf & " 	 LEFT OUTER JOIN"
            _Qry &= vbCrLf & " 	 [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart  AS MP WITH (NOLOCK) ON B.FNHSysPartId = MP.FNHSysPartId"
            _Qry &= vbCrLf & " 	 LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd AS OPP WITH (NOLOCK)  ON B.FTOrderProdNo = OPP.FTOrderProdNo AND B.FNHSysOperationId = OPP.FNHSysOperationId "
            _Qry &= vbCrLf & " 	 LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS MPP WITH (NOLOCK)  ON  B.FNHSysOperationId  = MPP.FNHSysOperationId"

            _Qry &= vbCrLf & "   LEFT OUTER JOIN        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS CMK  WITH (NOLOCK)  ON B.FNHSysMarkId = CMK.FNHSysMarkId"

            _Qry &= vbCrLf & "    WHERE B.FTSendSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTSendSuplNo.Text) & "' "
            _Qry &= vbCrLf & " and B.FTBarcodeSendSuplNo  = '" & HI.UL.ULF.rpQuoted(Key) & "' "
            _Qry &= vbCrLf & "  ORDER BY B.FTBarcodeSendSuplNo "

            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            With DirectCast(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                ' _oDt = .Copy
                If .Select("FTBarcodeSendSuplNo = '" & HI.UL.ULF.rpQuoted(Key) & "' ").Length <= 0 Then
                    For Each R As DataRow In dt.Rows
                        .ImportRow(R)
                    Next
                    Me.olberror.Text = HI.MG.ShowMsg.GetMessage("แสกนรับเรียบร้อยแล้ว Barcode !!!", 1702161204)
                Else
                    olberror.Text = HI.MG.ShowMsg.GetMessage("มีการแสกนรับไปแล้ว Barcode !!!", 1702161203)
                End If
                .AcceptChanges()
            End With
            ' Me.ogcdetail.DataSource = dt.Copy
            oFTStyleCode.Text = ""
            oFTOrderNo.Text = ""
            oFTOrderProdNo.Text = ""
            oFTColorway.Text = ""
            oFTSizeBreakDown.Text = ""
            oFNHSysOperationId.Text = ""
            oFNHSysMarkId.Text = ""
            oFNHSysPartId.Text = ""
            oFTBarcodeBundleNo.Text = ""
            olberror.Text = ""
            olberror.ForeColor = Color.Red
            If dt.Rows.Count > 0 Then
                For Each R As DataRow In dt.Rows
                    oFTStyleCode.Text = R!FTStyleCode.ToString
                    oFTOrderNo.Text = R!FTOrderNo.ToString
                    oFTOrderProdNo.Text = R!FTOrderProdNo.ToString
                    oFTColorway.Text = R!FTColorway.ToString
                    oFTSizeBreakDown.Text = R!FTSizeBreakDown.ToString
                    oFNHSysOperationId.Text = R!FTOperationName.ToString
                    oFNHSysMarkId.Text = R!FNHSysMarkId.ToString
                    oFNHSysPartId.Text = R!FTPartName.ToString
                    oFTBarcodeBundleNo.Text = R!FNBunbleSeq.ToString

                    If Me.FNHSysSuplId.Text = "" Then
                        Me.FNHSysSuplId.Text = R!FTSuplCode.ToString
                    End If
                    Exit For
                Next
            End If
            Dim _Strsql As String = ""
            If Me.FTStateBranchAccept.Checked Then
                With DirectCast(Me.ogcdetail.DataSource, DataTable)
                    .AcceptChanges()
                    If .Rows.Count <= 0 Then
                        HI.MG.ShowMsg.mInfo("ไม่สามารถทำการอนุมัติแล้วได้ กรุณายิงบาร์โค้ดมัดงาน !!!! ", 1703021042, Me.Text, "", MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                    For Each R As DataRow In .Rows
                        _Strsql = "UPDate  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch_Barcode "
                        _Strsql &= vbCrLf & " SET FTStateBranchAccept='1'"
                        _Strsql &= vbCrLf & " ,FTStateBranchAcceptBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Strsql &= vbCrLf & " ,FTStateBranchAcceptDate=" & HI.UL.ULDate.FormatDateDB & " "
                        _Strsql &= vbCrLf & " ,FTStateBranchAcceptTime=" & HI.UL.ULDate.FormatTimeDB & " "
                        _Strsql &= vbCrLf & " , FNBalQuantity=" & Double.Parse("0" & R!FNQuantityBal.ToString)
                        _Strsql &= vbCrLf & " WHERE    FTSendSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTSendSuplNo.Text) & "'  AND ISNULL(FTStateBranchAccept,'')<>'1' "
                        _Strsql &= vbCrLf & "and FTBarcodeSendSuplNo = '" & R!FTBarcodeSendSuplNo.ToString & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Strsql, Conn.DB.DataBaseName.DB_PROD)
                    Next
                End With
            End If
            'FTBarcodeNo.Focus()
            'FTBarcodeNo.SelectAll()
            TextEdit10.Focus()
            TextEdit10.SelectAll()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DefalutScanChecked()
        Try
            With ogvdetail
                For i As Integer = 0 To .RowCount - 1
                    If .GetRowCellValue(i, "FTBarcodeSendSuplNo").ToString = Me.TextEdit10.Text.Trim Then
                        .SetRowCellValue(i, "FTSelect", "1")
                        .SetRowCellValue(i, "FNQuantityBal", .GetRowCellValue(i, "FNQuantity"))
                        CType(ogcdetail.DataSource, DataTable).AcceptChanges()
                    End If
                Next
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private _NewValue As Integer
    Private _Edit As Boolean = False
    Private Sub RepositoryItemFNQuantityBal_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemFNQuantityBal.EditValueChanging
        Try
            With Me.ogvdetail
                If e.NewValue <= .GetRowCellValue(.FocusedRowHandle, "FNQuantity") Then
                    _NewValue = e.NewValue
                    _Edit = True
                Else
                    MG.ShowMsg.mInfo("ใส่เกินจำนวนที่กำหนด กรุณาใส่ใหม่อีกครั้ง !", 1704201628, Me.Text)
                    e.Cancel = True
                    _Edit = True
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepositoryItemFNQuantityBal_KeyDown(sender As Object, e As KeyEventArgs) Handles RepositoryItemFNQuantityBal.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim _Strsql As String = ""
                With ogvdetail
                    If _NewValue > 0 And _Edit = True Then
                        .SetRowCellValue(.FocusedRowHandle, "FTSelect", "1")
                        _Strsql = "UPDate  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch_Barcode "
                        _Strsql &= vbCrLf & " SET FTStateBranchAccept='1'"
                        _Strsql &= vbCrLf & " ,FTStateBranchAcceptBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Strsql &= vbCrLf & " ,FTStateBranchAcceptDate=" & HI.UL.ULDate.FormatDateDB & " "
                        _Strsql &= vbCrLf & " ,FTStateBranchAcceptTime=" & HI.UL.ULDate.FormatTimeDB & " "
                        _Strsql &= vbCrLf & " , FNBalQuantity=" & _NewValue
                        _Strsql &= vbCrLf & " WHERE    FTSendSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTSendSuplNo.Text) & "' -- AND ISNULL(FTStateBranchAccept,'0')<>'1' "
                        _Strsql &= vbCrLf & " and FTBarcodeSendSuplNo = '" & .GetRowCellValue(.FocusedRowHandle, "FTBarcodeSendSuplNo").ToString & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Strsql, Conn.DB.DataBaseName.DB_PROD)
                        _Edit = False
                    End If
                    .FocusedRowHandle = .FocusedRowHandle + 1
                    .FocusedColumn = .VisibleColumns(11)
                    .UnselectRow(.FocusedRowHandle - 1)
                    .SelectRow(.FocusedRowHandle)
                End With
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvdetail_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogvdetail.RowStyle
        Try
            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then
                Dim category As String = View.GetRowCellValue(e.RowHandle, View.Columns("FTSelect"))
                If category = "1" Then
                    e.Appearance.BackColor = Color.LightGreen
                    '  e.Appearance.BackColor2 = Color.SeaShell
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ockselectall_CheckedChanged(sender As Object, e As EventArgs) Handles ockselectall.CheckedChanged
        Try
            Dim Row As Integer = 0
            Call LoadDucumentDetail(Me.FTSendSuplNo.Text)
            With DirectCast(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    If Me.ockselectall.Checked Then
                        If ogvdetail.GetRowCellValue(Row, "FTSelect").ToString <> "1" Then
                            R!FTSelect = "1"
                            ogvdetail.SetRowCellValue(Row, "FNQuantityBal", ogvdetail.GetRowCellValue(Row, "FNQuantity"))
                        End If
                    Else
                        R!FTSelect = "0"
                        ogvdetail.SetRowCellValue(Row, "FNQuantityBal", 0)
                        Call LoadDucumentDetail(Me.FTSendSuplNo.Text)
                    End If
                    Row += 1
                Next
                .AcceptChanges()
            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepositoryItemCheckFTSelect_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCheckFTSelect.EditValueChanging
        Try
            Dim _Qtys As Double = 0
            Dim _Barcode As String = ""
            With ogvdetail
                _Barcode = .GetRowCellValue(.FocusedRowHandle, "FTBarcodeSendSuplNo")
                If e.NewValue = "1" Then
                    _Qtys = .GetRowCellValue(.FocusedRowHandle, "FNQuantity")
                Else
                    _Qtys = .GetRowCellValue(.FocusedRowHandle, "FNBalOrig")
                End If
            End With
            With DirectCast(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Select("FTBarcodeSendSuplNo='" & _Barcode & "'")
                    R!FNQuantityBal = _Qtys
                    Exit For
                Next
                .AcceptChanges()
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextEdit10_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit10.EditValueChanged

    End Sub
End Class