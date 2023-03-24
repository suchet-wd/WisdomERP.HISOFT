Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Drawing

Public Class wPOSSale
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_ACCOUNT
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _ProcLoad As Boolean = False
    Private _PDtDetail As DataTable = Nothing
    Private _FormLoad As Boolean = True
    Private _PopUp As wPOSSalePopup
    Private _PayPopUp As wPOSPayPopup
    Private _SetServerPopUp As wPOSPopUpSetServerName
    Private _ServerName As String = "HISOFT_SVR"

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        Call PrepareForm()
        ' Add any initialization after the InitializeComponent() call.
        _PopUp = New wPOSSalePopup
        HI.TL.HandlerControl.AddHandlerObj(_PopUp)
        _PayPopUp = New wPOSPayPopup
        HI.TL.HandlerControl.AddHandlerObj(_PayPopUp)

        _SetServerPopUp = New wPOSPopUpSetServerName
        HI.TL.HandlerControl.AddHandlerObj(_SetServerPopUp)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.InsertObjectLanguage(HI.ST.SysInfo.ModuleName, _PopUp.Name.ToString.Trim, _PopUp)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _PopUp.Name.ToString.Trim, _PopUp)

            Call oSysLang.InsertObjectLanguage(HI.ST.SysInfo.ModuleName, _PayPopUp.Name.ToString.Trim, _PayPopUp)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _PayPopUp.Name.ToString.Trim, _PayPopUp)

            Call oSysLang.InsertObjectLanguage(HI.ST.SysInfo.ModuleName, _SetServerPopUp.Name.ToString.Trim, _SetServerPopUp)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _SetServerPopUp.Name.ToString.Trim, _SetServerPopUp)
        Catch ex As Exception
        End Try
        Call NewDataTable()
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpID

    End Sub

    Private Sub wScanBarcodeFGToWH_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.FTProductBarcodeCustNo.EnterMoveNextControl = False
            HI.Conn.DB.UsedDB(Conn.DB.DataBaseName.DB_ACCOUNT)

            Dim _Cmd As String = ""
            _Cmd = "SELECT  Top 1 FTCfgData"
            _Cmd &= vbCrLf & " FROM TSESystemConfig WITH(NOLOCK) "
            _Cmd &= vbCrLf & " WHERE  (FTCfgName = N'CfgPos')"
            _ServerName = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SECURITY)
            _SysDocType = IIf(HI.Conn.DB.SerVerName.ToUpper = _ServerName, "1", "2")

            _Cmd = "SELECT   FNHSysWHFGId, FTWHFGCode "
            _Cmd &= vbCrLf & " FROM   TCNMWarehouseFG With(NOLOCK) "
            _Cmd &= vbCrLf & " WHERE FNHSysCmpId=" & HI.ST.SysInfo.CmpID
            _Cmd &= vbCrLf & " And FTStateSale='1' "
            Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MASTER)
            For Each R As DataRow In _oDt.Rows
                Me.FNHSysWHFGId.Properties.Tag = R!FNHSysWHFGId.ToString
                Me.FNHSysWHFGId.Text = R!FTWHFGCode.ToString
                Exit For
            Next
            Me.FNHSysWHFGId.Properties.ReadOnly = _oDt.Rows.Count = 1
            Me.FNHSysWHFGId.Enabled = _oDt.Rows.Count <> 1

            If HI.Conn.DB.SerVerName.ToUpper = _ServerName.ToUpper Then
                Me.FNHSysWHFGId.Visible = True
                Me.FNHSysWHFGId_lbl.Visible = True
            Else
                Me.FNHSysWHFGId.Visible = False
                Me.FNHSysWHFGId_lbl.Visible = False
            End If

            Me.FNSaleQtyBarcode.Value = 1
            'RemoveHandler Me.FTInvoiceNo.Leave, AddressOf HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly
        Catch ex As Exception
        End Try
    End Sub

#Region "Object Factory"
    Private Sub NewDataTable()
        Try
            _PDtDetail = New DataTable
            With _PDtDetail
                .Columns.Add("FTBarcodeCustNo", GetType(String))
                .Columns.Add("FTProdTypeName", GetType(String))
                .Columns.Add("FNQuantity", GetType(Integer))
                .Columns.Add("FNPrice", GetType(Double))
                .Columns.Add("FNAmount", GetType(Double))
                .Columns.Add("FNHSysWHFGId", GetType(Integer))
                .Columns.Add("FTOrderNo", GetType(String))
                .Columns.Add("FTColorway", GetType(String))
                .Columns.Add("FTSizeBreakDown", GetType(String))
                .Columns.Add("FNQuantityBal", GetType(Integer))
                .Columns.Add("FTDocumentRefNo", GetType(String))
            End With
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Command Button"
    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Try
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            HI.TL.HandlerControl.ClearControl(Me)
            Me.ogcdetail.DataSource = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmSave_Click(sender As Object, e As EventArgs) Handles ocmSave.Click
        If Me.FTInvoiceNo.Text <> "" Then
            If Me.FDInvoiceDate.Text <> "" Then
                If SaveData() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FDInvoiceDate_lbl.Text)
                Me.FDInvoiceDate.Focus()
                Exit Sub
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTInvoiceNo_lbl.Text)
            Me.FTInvoiceNo.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try
            Dim _Cmd As String = ""
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_ACCOUNT)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Exit Sub
            End If

            _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            HI.TL.HandlerControl.ClearControl(Me)
            Me.ogcdetail.DataSource = Nothing

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Exit Sub
        End Try
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        Try
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Account\"
                .ReportName = "ReportSaleSlipIE.rpt"
                .Formular = "{V_Rpt_SaleInvoice.FTInvoiceNo} ='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text) & "' "
                .Preview()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmsyncdata_Click(sender As Object, e As EventArgs)
        Try
            Dim _ServerName As String = ""
            With _SetServerPopUp
                .Text = .Text
                .ShowDialog()
                If (.Proc) Then
                    _ServerName = .FTComputerName.Text
                Else
                    Exit Sub
                End If
            End With
            Try
                If Not My.Computer.Network.Ping(_ServerName.ToString) Then
                    HI.MG.ShowMsg.mInfo("connecting Server Problems..", 1508031711, Me.Text)
                    Exit Sub
                End If
            Catch ex As Exception
            End Try

            If SyncDataToServer(_ServerName) Then
                HI.MG.ShowMsg.mInfo("synced data succussfuly...", 1508031702, Me.Text)
            Else
                HI.MG.ShowMsg.mInfo("synced data failed...", 1508031703, Me.Text)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmpay_Click(sender As Object, e As EventArgs) Handles ocmpay.Click
        Try
            HI.TL.HandlerControl.ClearControl(_PayPopUp)
            With _PayPopUp
                .Proc = False
                .FNInvGrandAmt.Value = Me.FNInvGrandAmt.Value
                .ShowDialog()
                If (.Proc) Then
                    Me.FNCashPay.Value = .FNCashAmt.Value
                    If SaveData() Then
                        With New HI.RP.Report
                            .FormTitle = Me.Text
                            .ReportFolderName = "Account\"
                            .ReportName = "ReportSaleSlipIEPos.rpt"
                            .Formular = "{V_Rpt_SaleInvoice.FTInvoiceNo} ='" & HI.UL.ULF.rpQuoted(FTInvoiceNo.Text) & "' "
                            .Print = True
                            .PrevieNoSplash()
                        End With
                    End If
                    Me.FTInvoiceNo.Text = ""
                    Me.FTProductBarcodeCustNo.Text = ""
                    Me.FTProductBarcodeCustNo.Focus()
                    Me.FNSaleQtyBarcode.Value = 1
                    Me.FNNetTotal.Value = 0
                    Me.FNInvAmt.Value = 0
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub
#End Region

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

#Region "Behavioral"
    Private Sub RepositoryFNQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryFNQuantity.EditValueChanging
        Try
            Dim nValue As Double = 0
            With ogvdetail
                'If e.NewValue > CInt("0" & .GetRowCellValue(.FocusedRowHandle, "FNQuantityBal").ToString) Then
                '    HI.MG.ShowMsg.mInfo("Sale Quantity Over Onhand.... Pls Chk Product!!", 1509030001, Me.Text)
                '    Dim _oDt As DataTable = CType(ogcdetail.DataSource, DataTable)
                '    With _oDt
                '        .AcceptChanges()
                '        For Each r As DataRow In .Select("FTBarcodeCustNo ='" & ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FTBarcodeCustNo") & "'")
                '            r!FNQuantity = e.OldValue
                '        Next
                '        .AcceptChanges()
                '    End With
                '    ogcdetail.DataSource = _oDt
                '    e.Cancel = True
                '    Exit Sub
                'End If
                'e.Cancel = False
                nValue = e.NewValue * CDbl("0" & .GetRowCellValue(.FocusedRowHandle, "FNPrice").ToString())
                .SetRowCellValue(.FocusedRowHandle, "FNAmount", nValue)
            End With
            Call SetPOAmt()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNInvGrandAmt_EditValueChanged(sender As Object, e As EventArgs) Handles FNInvGrandAmt.EditValueChanged
        Try
            If Not (_ProcLoad) Then
                Me.FTInvGrandAmtEN.Text = HI.UL.ULF.Convert_Bath_EN(FNInvGrandAmt.Value)
                Me.FTInvGrandAmtTH.Text = HI.UL.ULF.Convert_Bath_TH(FNInvGrandAmt.Value)
                Me.FNNetTotal.Value = Me.FNInvGrandAmt.Value
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNCashAmt_EditValueChanged(sender As Object, e As EventArgs) Handles FNCashPay.EditValueChanged
        Try
            If FNInvGrandAmt.Value > 0 And FNCashPay.Value > 0 Then
                Me.FNChangeAmt.Value = FNCashPay.Value - FNInvGrandAmt.Value
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTProductBarcodeCustNo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FTProductBarcodeCustNo.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If Me.FNSaleQtyBarcode.Value <= 0 Then
                    HI.MG.ShowMsg.mInfo("โปรดตรวจสอบจำนวนสินค้า", 1611191456, Me.Text)
                    Me.FNSaleQtyBarcode.Focus()
                    Exit Sub
                End If
                If Me.FTInvoiceNo.Text = "" Then
                    Dim _CmpH As String = ""
                    For Each ctrl As Object In Me.Controls.Find("FNHSysCmpId", True)

                        Select Case HI.ENM.Control.GeTypeControl(ctrl)
                            Case ENM.Control.ControlType.ButtonEdit
                                With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                    _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                End With

                                Exit For
                            Case ENM.Control.ControlType.TextEdit
                                With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                    If .Text = "" Then
                                        _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                    Else
                                        _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                    End If

                                End With

                                Exit For
                        End Select

                    Next


                    Me.FTInvoiceNo.Text = HI.TL.Document.GetDocumentNo(SysDBName, SysTableName, "", True, _CmpH)
                    DefaultsData()
                End If
                If Me.FTInvoiceNo.Text <> "" Then
                    'If SaveData() Then
                    'Call _CheckDoc()
                    Call GetDetail(HI.UL.ULF.rpQuoted(Me.FTProductBarcodeCustNo.Text))
                    Me.FTProductBarcodeCustNo.Focus()
                    Me.FTProductBarcodeCustNo.SelectAll()
                    'End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTInvoiceNo_lbl.Text)
                    Me.FTInvoiceNo.Focus()
                    Exit Sub
                End If

                Me.FTProductBarcodeCustNo.Focus()
                Me.FTProductBarcodeCustNo.SelectAll()
            End If


            If e.KeyCode = Keys.F1 Then
                Me.FNSaleQtyBarcode.Focus()
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvdetail_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvdetail.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                With ogvdetail
                    .DeleteRow(.FocusedRowHandle)
                End With
                SetPOAmt()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvdetail_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles ogvdetail.RowStyle
        Try
            Dim View As GridView = sender
            If ((e.RowHandle Mod 2) = 0) Then
                e.Appearance.BackColor = Color.Salmon
                e.Appearance.BackColor2 = Color.SeaShell
            End If
        Catch ex As Exception
        End Try
    End Sub


#End Region

#Region "Processing"


    Private Sub PrepareForm()

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

            _objId = Integer.Parse(_dt.Rows(0) !FNFormObjID.ToString)
            Me.SysDBName = _dt.Rows(0) !FTBaseName.ToString
            Me.SysTableName = _dt.Rows(0) !FHSysTableName.ToString
            Me.TableName = _dt.Rows(0) !FTTableName.ToString

            _SortField = _dt.Rows(0) !FTSortField.ToString

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

    Public Sub DefaultsData()
        _FormLoad = True
        Dim _FieldName As String
        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For I As Integer = 0 To _FormHeader(cind).DefaultsData.ToArray.Count - 1
                _FieldName = _FormHeader(cind).DefaultsData(I).FiledName.ToString

                Dim Pass As Boolean = True

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                .Text = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString

                                HI.TL.HandlerControl.DynamicButtonedit_Leave(Obj, New System.EventArgs)

                            End With
                        Case ENM.Control.ControlType.CalcEdit
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                .Value = Val(_FormHeader(cind).DefaultsData(I).DataDefaults.ToString)

                            End With
                        Case ENM.Control.ControlType.ComboBoxEdit
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                .SelectedIndex = Val(_FormHeader(cind).DefaultsData(I).DataDefaults.ToString)
                            End With
                        Case ENM.Control.ControlType.CheckEdit
                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                .Checked = (_FormHeader(cind).DefaultsData(I).DataDefaults.ToString = "1")
                            End With
                        Case ENM.Control.ControlType.DateEdit
                            With CType(Obj, DevExpress.XtraEditors.DateEdit)

                                Try
                                    .DateTime = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString
                                Catch ex As Exception
                                    .Text = ""
                                End Try

                            End With
                        Case ENM.Control.ControlType.MemoEdit
                            With CType(Obj, DevExpress.XtraEditors.MemoEdit)
                                .Text = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString
                            End With
                        Case ENM.Control.ControlType.TextEdit
                            With CType(Obj, DevExpress.XtraEditors.TextEdit)
                                .Text = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString
                            End With
                        Case Else
                    End Select
                Next
            Next
        Next

        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
        Me.ogcdetail.DataSource = Nothing
        'If Me.FTInvoiceNo.Properties.Tag <> "" Then
        '    If ogvdetail.RowCount > 0 Then
        '        Me.FTDocRefNo.Enabled = False
        '    End If
        'Else
        '    Me.FTDocRefNo.Enabled = True
        '    Me.FTDocRefNo.Properties.Buttons.Item(0).Enabled = True
        'End If
        Call NewDataTable()
        _FormLoad = False
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpID
    End Sub

    Public Sub LoadDataInfo(Key As Object)

        _FormLoad = True
        _ProcLoad = True

        Dim _Dt As DataTable
        Dim _Str As String = Me.Query & "  WHERE  " & Me.MainKey & "='" & Key.ToString & "' "

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
                                    .Image = HI.UL.ULImage.LoadImage("" & .Properties.Tag.ToString & R.Item(Col).ToString)
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

        Call LoadDetail()
        Me.FNNetTotal.Value = Me.FNInvGrandAmt.Value
        _ProcLoad = False
        _FormLoad = False
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpID
    End Sub

    Private Sub LoadDetail()
        Try
            Dim _Cmd As String = ""
            _Cmd = "SELECT  distinct   D.FTBarcodeCustNo , D.FTColorway , D.FTSizeBreakDown , D.FTDocumentRefNo , D.FTOrderNo"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                '_Cmd &= vbCrLf & ", T.FTProdTypeNameTH as FTProdTypeName "
                _Cmd &= vbCrLf & ",S.FTStyleNameTH as FTProdTypeName "
            Else
                _Cmd &= vbCrLf & ", S.FTStyleNameEN as FTProdTypeName "
                '_Cmd &= vbCrLf & ", T.FTProdTypeNameEN as FTProdTypeName "
            End If
            _Cmd &= vbCrLf & ",D.FNQuantity  , D.FNPrice ,convert(numeric(18,2),Isnull(D.FNQuantity,0) * Isnull(D.FNPrice,0)) AS FNAmount "

            _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMProductType AS T WITH (NOLOCK) ON O.FNHSysProdTypeId = T.FNHSysProdTypeId"
            _Cmd &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode AS B WITH(NOLOCK) ON O.FTOrderNo = B.FTOrderNo"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTStylePrice AS P WITH(NOLOCK) ON O.FNHSysStyleId = P.FNHSysStyleId and B.FTColorway = P.FTColorway "
            _Cmd &= vbCrLf & " and B.FTSizeBreakDown = P.FTSizeBreakDown "
            _Cmd &= vbCrLf & " RIGHT OUTER  JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail AS D WITH(NOLOCK)  ON B.FTCustBarcodeNo = D.FTBarcodeCustNo and O.FTOrderNo = D.FTOrderNo"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S WITH(NOLOCK) ON O.FNHSysStyleId = S.FNHSysStyleId"
            _Cmd &= vbCrLf & "where D.FTInvoiceNo ='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            ogcdetail.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Calculate(sender As System.Object, e As System.EventArgs) Handles FNDisCountPer.EditValueChanged,
                                                                                  FNInvAmt.EditValueChanged,
                                                                                  FNDisCountAmt.EditValueChanged,
                                                                                  FNVatPer.EditValueChanged,
                                                                                  FNVatAmt.EditValueChanged,
                                                                                  FNSurcharge.EditValueChanged

        Static _Proc As Boolean

        If Not (_Proc) And Not (_ProcLoad) Then
            _Proc = True
            Dim _POAmt As Double = FNInvAmt.Value

            If _POAmt = 0 Then
                FNDisCountAmt.Value = 0
                FNVatAmt.Value = 0
            End If

            Dim _DisPer As Double = FNDisCountPer.Value
            Dim _DisAmt As Double = FNDisCountAmt.Value
            Dim _VatPer As Double = FNVatPer.Value
            Dim _VatAmt As Double = FNVatAmt.Value
            Dim _SurAmt As Double = FNSurcharge.Value

            Select Case sender.Name.ToString.ToUpper
                Case "FNDisCountPer".ToUpper
                    _DisPer = FNDisCountPer.Value
                    _DisAmt = Format((_POAmt * _DisPer) / 100, HI.ST.Config.AmtFormat)
                    FNDisCountAmt.Value = _DisAmt
                Case "FNDisCountAmt".ToUpper
                    _DisAmt = FNDisCountAmt.Value

                    If _POAmt > 0 Then
                        _DisPer = Format((_DisAmt * 100) / _POAmt, HI.ST.Config.PercentFormat)
                    Else
                        _DisPer = 0
                    End If
                    FNDisCountPer.Value = _DisPer
                Case "FNVatPer".ToUpper
                    _VatPer = FNVatPer.Value
                    _VatAmt = Format(((_POAmt - _DisAmt) * _VatPer) / 100, HI.ST.Config.AmtFormat)
                    FNVatAmt.Value = _VatAmt
                Case "FNVatAmt".ToUpper
                    _VatAmt = FNVatAmt.Value

                    If (_POAmt - _DisAmt) > 0 Then
                        _VatPer = Format((_VatAmt * 100) / (_POAmt - _DisAmt), HI.ST.Config.PercentFormat)
                    Else
                        _VatPer = 0
                    End If
                    FNVatPer.Value = _VatPer
                Case Else
                    _DisAmt = Format((_POAmt * _DisPer) / 100, HI.ST.Config.AmtFormat)
                    FNDisCountAmt.Value = _DisAmt

                    _VatPer = FNVatPer.Value
                    _VatAmt = Format(((_POAmt - _DisAmt) * _VatPer) / 100, HI.ST.Config.AmtFormat)
                    FNVatAmt.Value = _VatAmt
            End Select

            Me.FNInvNetAmt.Value = (_POAmt - _DisAmt)

            Select Case sender.Name.ToString.ToUpper
                Case "FNDisCountPer".ToUpper, "FNDisCountAmt".ToUpper
                    _VatPer = FNVatPer.Value
                    _VatAmt = Format(((_POAmt - _DisAmt) * _VatPer) / 100, HI.ST.Config.AmtFormat)
                    FNVatAmt.Value = _VatAmt
            End Select

            FNInvGrandAmt.Value = Format(Me.FNInvNetAmt.Value + FNVatAmt.Value + _SurAmt, HI.ST.Config.AmtFormat)
            Me.FNNetTotal.Value = Me.FNInvGrandAmt.Value

            _Proc = False
        End If
    End Sub

    Private Function _CheckDoc() As Boolean
        Try
            Dim _Str As String = ""
            _Str = _FormHeader(0).Query & "  WHERE " & _FormHeader(0).MainKey & "='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "' "
            Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)
            If _dt.Rows.Count <= 0 Then
                Call SaveData()
            End If
        Catch ex As Exception

        End Try
    End Function

    Private Sub GetDetail(_Barcode As String)
        Try
            With CType(ogcdetail.DataSource, DataTable)
                If Not (CType(ogcdetail.DataSource, DataTable) Is Nothing) Then
                    If .Rows.Count > 0 Then
                        _PDtDetail = .Copy
                    End If
                End If
            End With
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _SelectSize As String = ""

            'If HI.ST.SysInfo.Admin Then
            '    _ServerName = HI.Conn.DB.SerVerName.ToString
            'End If
            Dim _WHFGId As Integer = 0 '= HI.Conn.SQLConn.GetField("SELECT TOP (1) FNHSysWHFGId  FROM  TCNMWarehouseFG WITH(NOLOCK) WHERE Isnull(FTStateSale,'') = '1'", Conn.DB.DataBaseName.DB_MASTER, "0")
            If HI.Conn.DB.SerVerName.ToUpper = _ServerName.ToUpper Then
                If Me.FNHSysWHFGId.Text = "" Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysWHFGId_lbl.Text)
                    Me.FNHSysWHFGId.Focus()
                    Exit Sub
                End If
                _WHFGId = Me.FNHSysWHFGId.Properties.Tag

                _Cmd = "SELECT Top 1  '' as FTIssueFGNo , F.FNHSysWHFGId,  O.FNHSysProdTypeId, O.FTOrderNo, T.FTProdTypeCode , Isnull(P.FNPrice,0) AS FNPrice ,SUM(F.FNQuantity) AS FNQuantity , F.FTColorway , F.FTSizeBreakDown "
                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    _Cmd &= vbCrLf & ", max(TE.FTStyleNameTH) as FTProdTypeName "
                Else
                    _Cmd &= vbCrLf & ", max(TE.FTStyleNameEN) as FTProdTypeName "
                End If
                _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) INNER JOIN"
                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMProductType AS T WITH (NOLOCK) ON O.FNHSysProdTypeId = T.FNHSysProdTypeId"
                _Cmd &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS TE WITH (NOLOCK) ON O.FNHSysStyleId = TE.FNHSysStyleId"
                _Cmd &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode AS B WITH(NOLOCK) ON O.FTOrderNo = B.FTOrderNo"
                _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTStylePrice AS P WITH(NOLOCK) ON O.FNHSysStyleId = P.FNHSysStyleId and B.FTColorway = P.FTColorway "
                _Cmd &= vbCrLf & " and B.FTSizeBreakDown = P.FTSizeBreakDown "
                _Cmd &= vbCrLf & " INNER JOIN ("

                _Cmd &= vbCrLf & " SELECT   FNHSysWHFGId,  FTColorway, FTSizeBreakDown, FTOrderNo, SUM(FNQuantity) AS FNQuantity"
                _Cmd &= vbCrLf & "     FROM          (SELECT     F.FNHSysWHFGId, B.FTColorway, B.FTSizeBreakDown, B.FTOrderNo, F.FNQuantity"
                _Cmd &= vbCrLf & "     FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode AS B WITH (NOLOCK) INNER JOIN"
                _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS F WITH (NOLOCK) ON B.FTOrderNo = F.FTOrderNo AND B.FTColorway = F.FTColorWay AND B.FTSizeBreakDown = F.FTSizeBreakDown) "
                _Cmd &= vbCrLf & "    AS T"
                _Cmd &= vbCrLf & "   GROUP BY FNHSysWHFGId, FTColorway, FTSizeBreakDown, FTOrderNo"

                _Cmd &= vbCrLf & " UNION ALL"
                _Cmd &= vbCrLf & "SELECT      HJ.FNHSysWHFGId, VA.FTColorway,VA.FTSizeBreakDown, AJ.FTOrderNo,  sum(AJ.FNQuantity) AS FNQuantity "
                _Cmd &= vbCrLf & "FROM        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGAdjustFG AS HJ WITH (NOLOCK) INNER JOIN"
                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGAdjustFG_Detail AS AJ WITH (NOLOCK) ON HJ.FTAdjustFGNo = AJ.FTAdjustFGNo LEFT OUTER JOIN"
                _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.V_GetDetailAdj AS VA WITH (NOLOCK) ON AJ.FTCustBarcodeNo = VA.FTCustBarcodeNo and AJ.FTOrderNo = VA.FTOrderNo"
                _Cmd &= vbCrLf & " GROUP BY   HJ.FNHSysWHFGId, AJ.FTOrderNo, VA.FTColorway, VA.FTSizeBreakDown "

                _Cmd &= vbCrLf & " UNION ALL"
                _Cmd &= vbCrLf & "SELECT    T.FNHSysWHIdFGTo,  FG.FTColorWay,  FG.FTSizeBreakDown, FG.FTOrderNo,   SUM(D.FNQuantity) AS FNQuantity "
                _Cmd &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG AS T WITH (NOLOCK) LEFT OUTER JOIN"
                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo. TFGTransferFG_Detail AS D WITH (NOLOCK) ON T.FTTransferFGNo = D.FTTransferFGNo INNER JOIN"
                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS FG WITH (NOLOCK) ON D.FTBarCodeCarton = FG.FTBarCodeCarton"
                _Cmd &= vbCrLf & "GROUP BY  T.FNHSysWHIdFGTo,  FG.FTColorWay,  FG.FTSizeBreakDown, FG.FTOrderNo "

                _Cmd &= vbCrLf & "  UNION ALL"
                _Cmd &= vbCrLf & " SELECT      HJ.FNHSysWHFGId, VA.FTColorway,VA.FTSizeBreakDown, AJ.FTOrderNo,  sum(AJ.FNQuantity) AS FNQuantity "
                _Cmd &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGReturnFG AS HJ WITH (NOLOCK) INNER JOIN"
                _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGReturnFG_Detail AS AJ WITH (NOLOCK) ON HJ.FTReturnFGNo = AJ.FTReturnFGNo LEFT OUTER JOIN"
                _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.V_GetDetailAdj AS VA WITH (NOLOCK) ON AJ.FTColorway = VA.FTColorway and AJ.FTOrderNo = VA.FTOrderNo and AJ.FTSizeBreakDown = VA.FTSizeBreakDown"
                _Cmd &= vbCrLf & " GROUP BY   HJ.FNHSysWHFGId, AJ.FTOrderNo, VA.FTColorway, VA.FTSizeBreakDown"


                _Cmd &= vbCrLf & "  UNION ALL "
                _Cmd &= vbCrLf & "  SELECT   D.FNHSysWHFGId,  D.FTColorway, D.FTSizeBreakDown, D.FTOrderNo, - sum(D.FNQuantity) AS FNQuantity "
                _Cmd &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale AS H WITH(NOLOCK) LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail AS D  WITH(NOLOCK) ON H.FTInvoiceNo = D.FTInvoiceNo "
                _Cmd &= vbCrLf & "  group by   D.FNHSysWHFGId,  D.FTColorway, D.FTSizeBreakDown, D.FTOrderNo  "
                _Cmd &= vbCrLf & " UNION ALL"
                _Cmd &= vbCrLf & "SELECT    T.FNHSysWHIdFG,  FG.FTColorWay,  FG.FTSizeBreakDown, FG.FTOrderNo,  - SUM(D.FNQuantity) AS FNQuantity "
                _Cmd &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG AS T WITH (NOLOCK) LEFT OUTER JOIN"
                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo. TFGTransferFG_Detail AS D WITH (NOLOCK) ON T.FTTransferFGNo = D.FTTransferFGNo INNER JOIN"
                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS FG WITH (NOLOCK) ON D.FTBarCodeCarton = FG.FTBarCodeCarton"
                _Cmd &= vbCrLf & "GROUP BY  T.FNHSysWHIdFG,  FG.FTColorWay,  FG.FTSizeBreakDown, FG.FTOrderNo "

                _Cmd &= vbCrLf & " UNION ALL"
                _Cmd &= vbCrLf & "SELECT         H.FNHSysWHFGId ,V.FTColorway,V.FTSizeBreakDown, V.FTOrderNo,  - SUM(I.FNQuantity) AS FNQuantity  "
                _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGIssueFG AS H WITH(NOLOCK) LEFT OUTER JOIN "
                _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGIssueFG_Detail AS I WITH (NOLOCK) ON H.FTIssueFGNo = I.FTIssueFGNo LEFT OUTER JOIN"
                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.V_GetDetailAdj AS V ON I.FTOrderNo = V.FTOrderNo AND I.FTColorway = V.FTColorway AND I.FTSizeBreakDown = V.FTSizeBreakDown"
                _Cmd &= vbCrLf & "Group by  H.FNHSysWHFGId ,V.FTColorway,V.FTSizeBreakDown, V.FTOrderNo "

                '_Cmd &= vbCrLf & ") AS S ON Z.FTColorway = S.FTColorway AND Z.FTSizeBreakDown = S.FTSizeBreakDown AND "
                '_Cmd &= vbCrLf & "   Z.FTSizeBreakDown = S.FTSizeBreakDown"
                _Cmd &= vbCrLf & " ) AS F  ON B.FTOrderNo = F.FTOrderNo    and B.FTColorway = F.FTColorway  and B.FTSizeBreakDown = F.FTSizeBreakDown "


                _Cmd &= vbCrLf & "where B.FTCustBarcodeNo ='" & _Barcode & "'"
                _Cmd &= vbCrLf & "and F.FNHSysWHFGId=" & Integer.Parse(_WHFGId)
                _Cmd &= vbCrLf & "group by F.FNHSysWHFGId , O.FNHSysProdTypeId, O.FTOrderNo, T.FTProdTypeCode, ISNULL(P.FNPrice, 0)  ,B.FTCustBarcodeNo ,"
                _Cmd &= vbCrLf & "    T.FTProdTypeNameTH ,T.FTProdTypeNameEN    ,F.FTColorway , F.FTSizeBreakDown "
                _Cmd &= vbCrLf & " having SUM(F.FNQuantity) > 0  "

            Else
                _Cmd = "SELECT   T.FNHSysWHFGId,T.FTIssueFGNo,T.FTCustBarcodeNo,T.FTOrderNo,T.FTColorway, T.FTSizeBreakDown, T.FTProdTypeCode,Isnull(P.FNPrice,0) AS FNPrice, T.FNHSysStyleId, T.FTStyleCode ,sum(T.FNQuantity) AS FNQuantity"
                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    _Cmd &= vbCrLf & ", T.FTProdTypeNameTH AS FTProdTypeName"
                Else
                    _Cmd &= vbCrLf & ", T.FTProdTypeNameEN AS FTProdTypeName"
                End If
                _Cmd &= vbCrLf & "From ("
                _Cmd &= vbCrLf & "SELECT   H.FNHSysWHFGId,   H.FTIssueFGNo,  V.FTCustBarcodeNo, V.FTOrderNo, V.FTColorway, V.FTSizeBreakDown, V.FTProdTypeCode, V.FTProdTypeNameTH, V.FTProdTypeNameEN, V.FNHSysStyleId, V.FTStyleCode ,I.FNQuantity"
                _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGIssueFG AS H WITH(NOLOCK) LEFT OUTER JOIN "
                _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGIssueFG_Detail AS I WITH (NOLOCK) ON H.FTIssueFGNo = I.FTIssueFGNo LEFT OUTER JOIN"
                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.V_GetDetailAdj AS V ON I.FNHSysStyleId = V.FNHSysStyleId and I.FTColorway = V.FTColorway and I.FTSizeBreakDown = V.FTSizeBreakDown"
                _Cmd &= vbCrLf & " and   I.FTOrderNo = V.FTOrderNo "
                _Cmd &= vbCrLf & "UNION ALL"
                _Cmd &= vbCrLf & "SELECT  H.FNHSysWHFGId, I.FTDocumentRefNo,  V.FTCustBarcodeNo, V.FTOrderNo, V.FTColorway, V.FTSizeBreakDown, V.FTProdTypeCode, V.FTProdTypeNameTH, V.FTProdTypeNameEN, V.FNHSysStyleId, V.FTStyleCode , - I.FNQuantity"
                _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGReturnFG AS H WITH(NOLOCK) INNER JOIN "
                _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGReturnFG_Detail AS I WITH (NOLOCK) ON H.FTReturnFGNo = I.FTReturnFGNo LEFT OUTER JOIN"
                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.V_GetDetailAdj AS V ON I.FNHSysStyleId = V.FNHSysStyleId and I.FTColorway = V.FTColorway and I.FTSizeBreakDown = V.FTSizeBreakDown"
                _Cmd &= vbCrLf & " and   I.FTOrderNo = V.FTOrderNo "
                _Cmd &= vbCrLf & "UNION ALL"
                _Cmd &= vbCrLf & " SELECT D.FNHSysWHFGId, D.FTDocumentRefNo, D.FTBarcodeCustNo , D.FTOrderNo ,  D.FTColorway,  D.FTSizeBreakDown, V.FTProdTypeCode, V.FTProdTypeNameTH, V.FTProdTypeNameEN, V.FNHSysStyleId, V.FTStyleCode  , - D.FNQuantity "
                _Cmd &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale AS H WITH(NOLOCK)"
                _Cmd &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail AS D  WITH(NOLOCK) ON H.FTInvoiceNo = D.FTInvoiceNo "
                _Cmd &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.V_GetDetailAdj AS V ON D.FTBarcodeCustNo = V.FTCustBarcodeNo"
                _Cmd &= vbCrLf & " and  D.FTOrderNo = V.FTOrderNo "
                _Cmd &= vbCrLf & " Where H.FTInvoiceNo <> '" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
                _Cmd &= vbCrLf & " and H.FTInvoiceNo like '%INVO%'"
                _Cmd &= vbCrLf & " ) AS T"
                _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTStylePrice AS P WITH(NOLOCK) ON T.FNHSysStyleId = P.FNHSysStyleId and T.FTColorway = P.FTColorway "
                _Cmd &= vbCrLf & " and T.FTSizeBreakDown = P.FTSizeBreakDown "

                _Cmd &= vbCrLf & "WHERE T.FTCustBarcodeNo='" & _Barcode & "'"
                '_Cmd &= vbCrLf & "and T.FNHSysWHFGId=" & Integer.Parse(_WHFGId)
                _Cmd &= vbCrLf & "Group by T.FNHSysWHFGId, T.FTIssueFGNo,  T.FTCustBarcodeNo, T.FTOrderNo, T.FTColorway, T.FTSizeBreakDown, T.FTProdTypeCode,P.FNPrice, T.FTProdTypeNameTH, T.FTProdTypeNameEN, T.FNHSysStyleId, T.FTStyleCode"
                _Cmd &= vbCrLf & "having sum(T.FNQuantity) > 0 "
            End If

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            If _oDt.Rows.Count > 0 Then

                If _oDt.Rows.Count > 1 Then
                    'check Color &  Size 
                    With _PopUp
                        .SSize = ""
                        .ogcDetail.DataSource = _oDt.Copy
                        .ShowDialog()
                        _SelectSize = .SSize
                    End With
                Else
                    _SelectSize = _oDt.Rows(0).Item("FTSizeBreakDown").ToString
                End If
                Dim _QtySale As Integer = 0
                For Each R As DataRow In _oDt.Select("FTSizeBreakDown='" & _SelectSize & "'")
                    With _PDtDetail
                        .AcceptChanges()


                        If .Select("FTBarcodeCustNo='" & _Barcode & "'").Length <= 0 Then
                            .Rows.Add(_Barcode, R!FTProdTypeName.ToString, Me.FNSaleQtyBarcode.Value, R!FNPrice.ToString, Me.FNSaleQtyBarcode.Value * Double.Parse("0" & R!FNPrice.ToString),
                                      R!FNHSysWHFGId.ToString, R!FTOrderNo.ToString, R!FTColorWay.ToString, R!FTSizeBreakDown.ToString, R!FNQuantity.ToString, R!FTIssueFGNo.ToString)
                        Else

                            For Each x As DataRow In .Select("FTBarcodeCustNo='" & _Barcode & "'")
                                If CInt(x!FNQuantity.ToString) >= CInt(x!FNQuantityBal.ToString) Then
                                    HI.MG.ShowMsg.mInfo("Sale Quantity Over Onhand.... Pls Chk Product!!", 1509030001, Me.Text)
                                    Exit Sub
                                End If
                                _QtySale = CInt("0" & x!FNQuantity.ToString) + Me.FNSaleQtyBarcode.Value
                                x!FNQuantity = _QtySale
                                x!FNAmount = Format(CDbl((_QtySale) * x!FNPrice.ToString), "#.00")
                            Next
                        End If
                        .AcceptChanges()
                    End With

                    Exit For
                Next

            Else
                If HI.Conn.SQLConn.GetDataTable("Select Top 1 * From  TPRODTOrder_CustBarcode WITH(NOLOCK) where FTCustBarcodeNo = '" & _Barcode & "' ", Conn.DB.DataBaseName.DB_PROD).Rows.Count > 0 Then
                    HI.MG.ShowMsg.mInfo("ยังไม่มีการแสกนของเข้าคลังสำเร็จรูป หรือ ของในคลังหมด....", 1508011007, Me.Text)
                Else
                    HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลบาร์โค๊ดลูกค้า.....กรุณาตรวจสอบอีกครั้ง", 1507271320, Me.Text)
                End If

                'With _PDtDetail
                '    .AcceptChanges()
                '    If .Select("FTBarcodeCustNo='" & _Barcode & "'").Length <= 0 Then
                '        .Rows.Add(_Barcode, _Barcode, 1, 0, 0)
                '    Else
                '        For Each x As DataRow In .Select("FTBarcodeCustNo='" & _Barcode & "'")
                '            x!FNQuantity = (CInt("0" & x!FNQuantity.ToString) + 1)
                '            x!FNAmount = Format(CDbl((CInt("0" & x!FNQuantity.ToString)) * x!FNPrice.ToString), "#.00")
                '        Next
                '    End If
                '    .AcceptChanges()
                'End With
            End If
            Me.ogcdetail.DataSource = _PDtDetail
            Call SetPOAmt()
            Me.FNSaleQtyBarcode.Value = 1
            'Call SaveData()
        Catch ex As Exception
        End Try
    End Sub

    'Private Sub RepositoryFNPrice_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryFNPrice.EditValueChanging
    '    Try
    '        Dim nValue As Double = 0
    '        With ogvdetail
    '            If e.NewValue > CInt("0" & .GetRowCellValue(.FocusedRowHandle, "FNQuantityBal").ToString) Then
    '                HI.MG.ShowMsg.mInfo("Sale Quantity Over Onhand.... Pls Chk Product!!", 1509030001, Me.Text)
    '                Exit Sub
    '            End If
    '            nValue = e.NewValue * CDbl("0" & .GetRowCellValue(.FocusedRowHandle, "FNQuantity").ToString())
    '            .SetRowCellValue(.FocusedRowHandle, "FNAmount", nValue)
    '        End With
    '        Call SetPOAmt()
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Sub SetPOAmt()
        Try
            Me.FNInvAmt.Value = 0
            With CType(ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    Me.FNInvAmt.Value += +CDbl("0" & R!FNAmount.ToString)
                Next
            End With
        Catch ex As Exception
        End Try
    End Sub
    'Private Function SaveData() As Boolean
    '    HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_ACCOUNT)
    '    HI.Conn.SQLConn.SqlConnectionOpen()
    '    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
    '    Try
    '        Dim _Cmd As String = ""



    '        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '            HI.Conn.SQLConn.Tran.Rollback()
    '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '            Return False
    '        End If


    '        HI.Conn.SQLConn.Tran.Commit()
    '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '        Return True
    '    Catch ex As Exception
    '        HI.Conn.SQLConn.Tran.Rollback()
    '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '        Return False
    '    End Try
    'End Function

    Private Overloads Function SaveData(Optional ByVal State As Boolean = True) As Boolean

        Dim _FieldName As String
        Dim _Fields As String = ""
        Dim _Values As String = ""
        Dim _Str As String
        Dim _Key As String = ""
        Dim _Val As String = ""
        Dim _StateNew As Boolean = False
        Dim _CmpH As String = ""
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
                                If SysDocType <> "0" Then
                                    If .Text = HI.TL.Document.GetDocumentNo(_FormHeader(cind).SysDBName, _FormHeader(cind).SysTableName, SysDocType, True, _CmpH).ToString() Then
                                        _StateNew = True
                                    Else

                                        _Key = .Text

                                        _Str = _FormHeader(cind).Query & "  WHERE " & _FormHeader(cind).MainKey & "='" & HI.UL.ULF.rpQuoted(_Key) & "' "
                                        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

                                        If _dt.Rows.Count <= 0 Then
                                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                            Obj.Focus()
                                            Return False
                                        End If
                                    End If
                                Else
                                    _Key = .Text

                                    _Str = _FormHeader(cind).Query & "  WHERE " & _FormHeader(cind).MainKey & "='" & HI.UL.ULF.rpQuoted(_Key) & "' "
                                    Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)
                                    If _dt.Rows.Count <= 0 Then
                                        _StateNew = True
                                    Else
                                        _StateNew = False
                                    End If

                                End If

                            End If
                        End With

                End Select
            Next
        Next

        If (_StateNew) Then
            If SysDocType <> "0" Then
                _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, SysDocType, False, _CmpH).ToString
            End If

        End If

        Try

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            Dim _FoundControl As Boolean
            For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
                For I As Integer = 0 To _FormHeader(cind).BaseFiled.ToArray.Count - 1
                    _FieldName = _FormHeader(cind).BaseFiled(I).FiledName.ToString
                    _FoundControl = False
                    If (_StateNew) Then

                        '------Update -------------
                        For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                            _FoundControl = True
                            If UCase(_FieldName) = _FormHeader(cind).MainKey.ToUpper Then
                                _Val = _Key
                            Else
                                Select Case HI.ENM.Control.GeTypeControl(Obj)
                                    Case ENM.Control.ControlType.ButtonEdit
                                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                            _Val = "" & .Properties.Tag.ToString
                                        End With
                                    Case ENM.Control.ControlType.CalcEdit
                                        With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                            _Val = .Value.ToString
                                        End With
                                    Case ENM.Control.ControlType.ComboBoxEdit
                                        With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                            _Val = .SelectedIndex.ToString
                                        End With
                                    Case ENM.Control.ControlType.CheckEdit
                                        With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                            _Val = .EditValue.ToString
                                        End With
                                    Case ENM.Control.ControlType.PictureEdit
                                        With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                            _Val = HI.UL.ULImage.SaveImage(CType(Obj, DevExpress.XtraEditors.PictureEdit), _Key.ToString & "_" & .Name.ToString, "" & .Properties.Tag.ToString)
                                        End With
                                    Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit, ENM.Control.ControlType.DateEdit
                                        _Val = Obj.Text
                                    Case Else
                                        _Val = Obj.Text
                                End Select
                            End If
                        Next

                        If Not (_FoundControl) Then
                            Select Case UCase(_FieldName)
                                Case UCase("FDUpdDate"), UCase("FDUpdDate"), UCase("FTUpdTime"), UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser"), UCase("FTUpdUser")
                                    _FoundControl = True
                            End Select
                        End If

                        If _FoundControl Then
                            If _Values <> "" Then _Values &= ","
                            If _Fields <> "" Then _Fields &= ","

                            _Fields &= _FieldName

                            Select Case UCase(_FieldName)
                                Case UCase("FDInsDate"), UCase("FTInsDate")
                                    _Values &= HI.UL.ULDate.FormatDateDB & ""
                                Case UCase("FDUpdDate"), UCase("FDUpdDate"), UCase("FTUpdTime"), UCase("FTUpdUser")
                                    _Values &= "''"
                                Case UCase("FTInsTime")
                                    _Values &= HI.UL.ULDate.FormatTimeDB & ""
                                Case UCase("FTInsUser")
                                    _Values &= "'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                Case Else
                                    Select Case UCase(Microsoft.VisualBasic.Left(_FieldName, 2))
                                        Case "FC", "FN"
                                            _Values &= HI.UL.ULF.ChkNumeric(_Val) & ""
                                        Case "FD"
                                            _Values &= "'" & HI.UL.ULDate.ConvertEnDB(_Val) & "'"
                                        Case Else
                                            _Values &= "'" & HI.UL.ULF.rpQuoted(_Val) & "'"
                                    End Select
                            End Select
                        End If


                    Else

                        '------Update -------------
                        For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                            _FoundControl = True
                            If UCase(_FieldName) = _FormHeader(cind).MainKey.ToUpper Then
                                _Val = _Key
                            Else

                                Select Case HI.ENM.Control.GeTypeControl(Obj)
                                    Case ENM.Control.ControlType.ButtonEdit
                                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                            _Val = "" & .Properties.Tag.ToString
                                        End With
                                    Case ENM.Control.ControlType.CalcEdit
                                        With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                            _Val = .Value.ToString
                                        End With
                                    Case ENM.Control.ControlType.ComboBoxEdit
                                        With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                            _Val = .SelectedIndex.ToString
                                        End With
                                    Case ENM.Control.ControlType.CheckEdit
                                        With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                            _Val = .EditValue.ToString
                                        End With
                                    Case ENM.Control.ControlType.PictureEdit
                                        With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                            _Val = HI.UL.ULImage.SaveImage(CType(Obj, DevExpress.XtraEditors.PictureEdit), _Key.ToString & "_" & .Name.ToString, "" & .Properties.Tag.ToString)
                                        End With
                                    Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit, ENM.Control.ControlType.DateEdit
                                        _Val = Obj.Text
                                    Case Else
                                        _Val = Obj.Text
                                End Select

                            End If

                        Next

                        If Not (_FoundControl) Then
                            Select Case UCase(_FieldName)
                                Case UCase("FDUpdDate"), UCase("FDUpdDate"), UCase("FTUpdTime"), UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser"), UCase("FTUpdUser")
                                    _FoundControl = True
                            End Select
                        End If

                        If _FoundControl Then
                            Select Case UCase(_FieldName)
                                Case UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser")
                                Case Else
                                    If _Values <> "" Then _Values &= ","
                            End Select

                            Select Case UCase(_FieldName)
                                Case UCase("FDUpdDate"), UCase("FDUpdDate")
                                    _Values &= _FieldName & "=" & HI.UL.ULDate.FormatDateDB & ""
                                Case UCase("FTUpdTime")
                                    _Values &= _FieldName & "=" & HI.UL.ULDate.FormatTimeDB & ""
                                Case UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser")
                                Case UCase("FTUpdUser")
                                    _Values &= _FieldName & "='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                Case Else
                                    Select Case UCase(Microsoft.VisualBasic.Left(_FieldName, 2))
                                        Case "FC", "FN"
                                            _Values &= _FieldName & "=" & HI.UL.ULF.ChkNumeric(_Val) & ""
                                        Case "FD"
                                            _Values &= _FieldName & "='" & HI.UL.ULDate.ConvertEnDB(_Val) & "'"
                                        Case Else
                                            _Values &= _FieldName & "='" & HI.UL.ULF.rpQuoted(_Val) & "'"
                                    End Select
                            End Select
                        End If

                    End If

                Next
                If (_StateNew) Then
                    _Str = " INSERT INTO   " & _FormHeader(cind).TableName & "(" & _Fields & ") VALUES (" & _Values & ")"
                Else
                    _Str = " Update  " & _FormHeader(cind).TableName & " Set " & _Values & " WHERE  " & _FormHeader(cind).MainKey & "='" & _Key.ToString & "' "
                End If

                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            Next

            If Not (SaveDetail(_Key)) Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If
            For Each Obj As Object In Me.Controls.Find(_FormHeader(0).MainKey, True)

                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.ButtonEdit
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            .Properties.Tag = _Key
                            .Text = _Key
                        End With
                End Select
            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

    Private Function SaveDetail(_Key As String) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            With CType(ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                _oDt = .Copy
            End With

            _Cmd = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail Where  FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
            For Each R As DataRow In _oDt.Rows
                _Cmd = "Update   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail"
                _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ",FNQuantity=" & CDbl("0" & R!FNQuantity.ToString)
                _Cmd &= vbCrLf & ",FNPrice=" & CDbl("0" & R!FNPrice.ToString)
                _Cmd &= vbCrLf & ",FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(R!FTDocumentRefNo.ToString) & "'"
                _Cmd &= vbCrLf & "WHERE FTInvoiceNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
                _Cmd &= vbCrLf & "And FTBarcodeCustNo='" & R!FTBarcodeCustNo.ToString & "'"
                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail ( FTInsUser, FDInsDate, FTInsTime,  FTInvoiceNo, FTBarcodeCustNo"
                    _Cmd &= ", FNQuantity, FNPrice , FNHSysWHFGId,FTOrderNo , FTColorWay , FTSizeBreakDown,FTDocumentRefNo)"
                    _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ", " & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ", " & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                    _Cmd &= vbCrLf & ",'" & R!FTBarcodeCustNo.ToString & "'"
                    _Cmd &= vbCrLf & "," & CDbl("0" & R!FNQuantity.ToString)
                    _Cmd &= vbCrLf & "," & CDbl("0" & R!FNPrice.ToString)
                    _Cmd &= vbCrLf & "," & CInt("0" & Me.FNHSysWHFGId.Properties.Tag.ToString)
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTColorWay.ToString) & "'"
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTDocumentRefNo.ToString) & "'"
                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If
                End If
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function SyncDataToServer(_ServerName As String) As Boolean
        Dim _Spls As New HI.TL.SplashScreen("being synced sysnc Data.... Please Wait")
        Try
            Dim _Cmd As String = ""
            'Header
            HI.Conn.DB.UsedDB(Conn.DB.DataBaseName.DB_ACCOUNT)

            _Cmd = "INSERT INTO  OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "').[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale"
            _Cmd &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTInvoiceNo, FDInvoiceDate, FTInvoiceBy, FTRemark, FNInvAmt, FNDisCountPer, FNDisCountAmt, FNInvNetAmt, FNVatPer, "
            _Cmd &= vbCrLf & " FNVatAmt, FNSurcharge, FNInvGrandAmt, FTInvGrandAmtTH, FTInvGrandAmtEN, FNHSysCmpId, FTCustomerName, FTCustAddr)"
            _Cmd &= vbCrLf & "SELECT     S.FTInsUser, S.FDInsDate, S.FTInsTime, S.FTUpdUser, S.FDUpdDate, S.FTUpdTime, S.FTInvoiceNo, S.FDInvoiceDate, S.FTInvoiceBy, S.FTRemark, S.FNInvAmt, S.FNDisCountPer, S.FNDisCountAmt, "
            _Cmd &= vbCrLf & "S.FNInvNetAmt, S.FNVatPer, S.FNVatAmt, S.FNSurcharge, S.FNInvGrandAmt, S.FTInvGrandAmtTH, S.FTInvGrandAmtEN, S.FNHSysCmpId, S.FTCustomerName, S.FTCustAddr"
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale AS S LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "').[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale AS T ON S.FTInvoiceNo = T.FTInvoiceNo"
            _Cmd &= vbCrLf & " WHERE(T.FTInvoiceNo Is NULL)"
            HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

            'Detail
            _Cmd = "INSERT INTO OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "').[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail"
            _Cmd &= vbCrLf & "( FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTInvoiceNo, FTBarcodeCustNo, FNQuantity, FNPrice)"
            _Cmd &= vbCrLf & "SELECT     S.FTInsUser, S.FDInsDate, S.FTInsTime, S.FTUpdUser, S.FDUpdDate, S.FTUpdTime, S.FTInvoiceNo, S.FTBarcodeCustNo, S.FNQuantity, S.FNPrice"
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail AS S LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "').[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail AS T ON S.FTBarcodeCustNo = T.FTBarcodeCustNo AND "
            _Cmd &= vbCrLf & " S.FTInvoiceNo = T.FTInvoiceNo"
            _Cmd &= vbCrLf & "WHERE     (T.FTInvoiceNo IS NULL) AND (T.FTBarcodeCustNo IS NULL)"
            HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
            _Spls.Close()
            Return True
        Catch ex As Exception
            _Spls.Close()
            Return False
        End Try
    End Function

    Private Function SyncDataFromServer(_ServerName As String) As Boolean
        Dim _Spls As New HI.TL.SplashScreen("being synced sysnc Data.... Please Wait")
        Try
            Dim _Cmd As String = ""
            HI.Conn.DB.UsedDB(Conn.DB.DataBaseName.DB_ACCOUNT)

            _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG (FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTBarCodeCarton, FNHSysWHFGId, FTColorWay, FTSizeBreakDown, FTOrderNo, FNQuantity)"
            _Cmd &= vbCrLf & "SELECT  F.FTInsUser, F.FDInsDate, F.FTInsTime, F.FTUpdUser, F.FDUpdDate, F.FTUpdTime, F.FTBarCodeCarton, F.FNHSysWHFGId, F.FTColorWay, F.FTSizeBreakDown, F.FTOrderNo, F.FNQuantity"
            _Cmd &= vbCrLf & "FROM         OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "' ).[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS F LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS G ON F.FTOrderNo = G.FTOrderNo AND F.FTSizeBreakDown = G.FTSizeBreakDown AND F.FTColorWay = G.FTColorWay AND "
            _Cmd &= vbCrLf & "F.FNHSysWHFGId = G.FNHSysWHFGId And F.FTBarCodeCarton = G.FTBarCodeCarton"
            _Cmd &= vbCrLf & " WHERE G.FTBarCodeCarton Is null"
            HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            _Cmd = "INSERT INTO HITECH_PRODUCTION.dbo.TPRODTOrder_CustBarcode "
            _Cmd &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FTCustBarcodeNo)"
            _Cmd &= vbCrLf & "SELECT     B.FTInsUser, B.FDInsDate, B.FTInsTime, B.FTUpdUser, B.FDUpdDate, B.FTUpdTime, B.FTOrderNo, B.FTSubOrderNo, B.FTColorway, B.FTSizeBreakDown, B.FTCustBarcodeNo"
            _Cmd &= vbCrLf & "FROM         OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "' ).HITECH_PRODUCTION.dbo.TPRODTOrder_CustBarcode AS B LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode AS C ON B.FTOrderNo = C.FTOrderNo AND B.FTSubOrderNo = C.FTSubOrderNo AND B.FTColorway = C.FTColorway AND "
            _Cmd &= vbCrLf & "B.FTSizeBreakDown = C.FTSizeBreakDown And B.FTCustBarcodeNo = C.FTCustBarcodeNo"
            _Cmd &= vbCrLf & "Where C.FTCustBarcodeNo Is null"
            HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTStylePrice"
            _Cmd &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysStyleId, FNHSysCmpId, FNPrice, FTColorway, FTSizeBreakDown)"
            _Cmd &= vbCrLf & "SELECT     P.FTInsUser, P.FDInsDate, P.FTInsTime, P.FTUpdUser, P.FDUpdDate, P.FTUpdTime, P.FNHSysStyleId, P.FNHSysCmpId, P.FNPrice, P.FTColorway, P.FTSizeBreakDown"
            _Cmd &= vbCrLf & "FROM         OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "' ).[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTStylePrice AS P LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTStylePrice AS S ON P.FTSizeBreakDown = S.FTSizeBreakDown AND P.FTColorway = S.FTColorway AND P.FNHSysCmpId = S.FNHSysCmpId AND P.FNHSysStyleId = S.FNHSysStyleId"
            _Cmd &= vbCrLf & "WHERE S.FNHSysStyleId Is null"
            HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

            _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMProductType"
            _Cmd &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysProdTypeId, FTProdTypeCode, FTProdTypeNameTH, FTProdTypeNameEN, FTRemark, FTStateActive)"
            _Cmd &= vbCrLf & "SELECT     T.FTInsUser, T.FDInsDate, T.FTInsTime, T.FTUpdUser, T.FDUpdDate, T.FTUpdTime, T.FNHSysProdTypeId, T.FTProdTypeCode, T.FTProdTypeNameTH, T.FTProdTypeNameEN, T.FTRemark, "
            _Cmd &= vbCrLf & "T.FTStateActive  "
            _Cmd &= vbCrLf & "FROM         OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "' ).[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMProductType AS T LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMProductType AS P ON T.FNHSysProdTypeId = P.FNHSysProdTypeId"
            _Cmd &= vbCrLf & " WHERE P.FNHSysProdTypeId Is null"
            HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_MASTER)

            _Cmd = " INSERT INTO      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder( FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FDOrderDate, FTOrderBy, FNOrderType, FNHSysCmpId, FNHSysCmpRunId, FNHSysStyleId, FTPORef, "
            _Cmd &= vbCrLf & "FNHSysCustId, FNHSysAgencyId, FNHSysProdTypeId, FNHSysBuyerId, FTMainMaterial, FTCombination, FTRemark, FTStateOrderApp, FTAppBy, FDAppDate, FTAppTime, FNJobState, FTStateBy, "
            _Cmd &= vbCrLf & "FDStateDate, FTStateTime, FTImage1, FTImage2, FTImage3, FTImage4, FNHSysBrandId, FNHSysBuyId, FTCancelAppBy, FDCancelAppDate, FDCancelAppTime, FTCancelAppRemark, "
            _Cmd &= vbCrLf & " FTPOTradingCo, FTPOItem, FTPOCreateDate, FNHSysMerTeamId, FNHSysPlantId, FNHSysBuyGrpId, FNHSysMainCategoryId, FNHSysVenderPramId, FTOrderCreateStatus, FTImportUser, "
            _Cmd &= vbCrLf & " FDImportDate, FTImportTime, FPOrderImage1, FPOrderImage2, FPOrderImage3, FPOrderImage4, FNHSysSeasonId, FDDateChangeOrderImage1, FTTimeChangeOrderImage1, "
            _Cmd &= vbCrLf & " FTUserChangeOrderImage1, FDDateChangeOrderImage2, FTTimeChangeOrderImage2, FTUserChangeOrderImage2, FDDateChangeOrderImage3, FTTimeChangeOrderImage3, "
            _Cmd &= vbCrLf & " FTUserChangeOrderImage3, FDDateChangeOrderImage4, FTTimeChangeOrderImage4, FTUserChangeOrderImage4, FTOrderNoRef, FTStateSendDirectorApp, FTStateSendDirectorBy, "
            _Cmd &= vbCrLf & " FDStateSendDirectorDate, FTStateSendDirectorTime, FTStateDirectorApp, FTStateDirectorAppBy, FDStateDirectorAppDate, FTStateDirectorAppTime, FTStateDirectorReject, FTStateDirectorRejectBy, "
            _Cmd &= vbCrLf & " FDStateDirectorRejectDate, FTStateDirectorRejectTime, FTStateFactoryApp, FTStateFactoryAppBy, FDStateFactoryAppDate, FTStateFactoryAppTime, FTStateFactoryReject, FTStateFactoryRejectBy, "
            _Cmd &= vbCrLf & " FDStateFactoryRejectDate, FTStateFactoryRejectTime, FTChangeCmpBy, FDChangeCmpDate, FTChangeCmpTime)"

            _Cmd &= vbCrLf & "SELECT     O.FTInsUser, O.FDInsDate, O.FTInsTime, O.FTUpdUser, O.FDUpdDate, O.FTUpdTime, O.FTOrderNo, O.FDOrderDate, O.FTOrderBy, O.FNOrderType, O.FNHSysCmpId, O.FNHSysCmpRunId, "
            _Cmd &= vbCrLf & " O.FNHSysStyleId, O.FTPORef, O.FNHSysCustId, O.FNHSysAgencyId, O.FNHSysProdTypeId, O.FNHSysBuyerId, O.FTMainMaterial, O.FTCombination, O.FTRemark, O.FTStateOrderApp, O.FTAppBy,"
            _Cmd &= vbCrLf & " O.FDAppDate, O.FTAppTime, O.FNJobState, O.FTStateBy, O.FDStateDate, O.FTStateTime, O.FTImage1, O.FTImage2, O.FTImage3, O.FTImage4, O.FNHSysBrandId, O.FNHSysBuyId,"
            _Cmd &= vbCrLf & " O.FTCancelAppBy, O.FDCancelAppDate, O.FDCancelAppTime, O.FTCancelAppRemark, O.FTPOTradingCo, O.FTPOItem, O.FTPOCreateDate, O.FNHSysMerTeamId, O.FNHSysPlantId,"
            _Cmd &= vbCrLf & " O.FNHSysBuyGrpId, O.FNHSysMainCategoryId, O.FNHSysVenderPramId, O.FTOrderCreateStatus, O.FTImportUser, O.FDImportDate, O.FTImportTime, O.FPOrderImage1, O.FPOrderImage2,"
            _Cmd &= vbCrLf & " O.FPOrderImage3, O.FPOrderImage4, O.FNHSysSeasonId, O.FDDateChangeOrderImage1, O.FTTimeChangeOrderImage1, O.FTUserChangeOrderImage1, O.FDDateChangeOrderImage2,"
            _Cmd &= vbCrLf & "O.FTTimeChangeOrderImage2, O.FTUserChangeOrderImage2, O.FDDateChangeOrderImage3, O.FTTimeChangeOrderImage3, O.FTUserChangeOrderImage3, O.FDDateChangeOrderImage4,"
            _Cmd &= vbCrLf & "O.FTTimeChangeOrderImage4, O.FTUserChangeOrderImage4, O.FTOrderNoRef, O.rowguid, O.FTStateSendDirectorApp, O.FTStateSendDirectorBy, O.FDStateSendDirectorDate,"
            _Cmd &= vbCrLf & " O.FTStateSendDirectorTime, O.FTStateDirectorApp, O.FTStateDirectorAppBy, O.FDStateDirectorAppDate, O.FTStateDirectorAppTime, O.FTStateDirectorReject, O.FTStateDirectorRejectBy,"
            _Cmd &= vbCrLf & " O.FDStateDirectorRejectDate, O.FTStateDirectorRejectTime, O.FTStateFactoryApp, O.FTStateFactoryAppBy, O.FDStateFactoryAppDate, O.FTStateFactoryAppTime, O.FTStateFactoryReject,"
            _Cmd &= vbCrLf & " O.FTStateFactoryRejectBy, O.FDStateFactoryRejectDate, O.FTStateFactoryRejectTime, O.FTChangeCmpBy, O.FDChangeCmpDate, O.FTChangeCmpTime"
            _Cmd &= vbCrLf & "FROM         OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "' ). [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS T ON O.FTOrderNo = T.FTOrderNo"
            _Cmd &= vbCrLf & " where T.FTOrderNo Is null"
            HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN)
            _Spls.Close()
            Return True
        Catch ex As Exception
            _Spls.Close()
            Return False
        End Try
    End Function

    Private Sub ocmsyncdatafromserver_Click(sender As Object, e As EventArgs)
        Try
            Dim _ServerName As String = ""
            Try
                With _SetServerPopUp
                    .Text = .Text
                    .ShowDialog()
                    If (.Proc) Then
                        _ServerName = .FTComputerName.Text
                        If _ServerName = HI.Conn.DB.SerVerName Then
                            HI.MG.ShowMsg.mInfo("ชื่อเซิฟเวอร์ตรงกัน ไม่สามารถถ่ายโอนนข้อมูลได้.....", 1508261338, Me.Text)
                            Exit Sub
                        End If
                    Else
                        Exit Sub
                    End If
                End With
                If Not My.Computer.Network.Ping(_ServerName.ToString) Then
                    HI.MG.ShowMsg.mInfo("connecting Server Problems..", 1508031711, Me.Text)
                    Exit Sub
                End If
            Catch ex As Exception
            End Try

            If SyncDataFromServer(_ServerName) Then
                HI.MG.ShowMsg.mInfo("synced data succussfuly...", 1508031702, Me.Text)
            Else
                HI.MG.ShowMsg.mInfo("synced data failed...", 1508031703, Me.Text)
            End If
        Catch ex As Exception
        End Try
    End Sub
#End Region

    Private Sub FTInvoiceNo_EditValueChanged(sender As Object, e As EventArgs) 'Handles FTInvoiceNo.EditValueChanged
        Try
            Me.FTProductBarcodeCustNo.Focus()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTInvoiceNo_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles FTInvoiceNo.EditValueChanging
        Try
            Me.FTProductBarcodeCustNo.Focus()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub wPOSSale_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                Me.FNSaleQtyBarcode.Focus()

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNSaleQtyBarcode_KeyDown(sender As Object, e As KeyEventArgs) Handles FNSaleQtyBarcode.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Call FTProductBarcodeCustNo_KeyDown(sender, e)
            End If
        Catch ex As Exception
        End Try
    End Sub


End Class