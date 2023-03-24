

Public Class wSendDocToAccApprove

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_INVEN
    Private _AddItemPopup As wRcvToAccPopup
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private _oDtCen As DataTable
    Private _ProcLoad As Boolean = False

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Call InitFormControl()

        _AddItemPopup = New wRcvToAccPopup
        HI.TL.HandlerControl.AddHandlerObj(_AddItemPopup)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddItemPopup.Name.ToString.Trim, _AddItemPopup)
        Catch ex As Exception
        Finally
        End Try


    End Sub

#Region "Property"
    Public _oDtPopup As DataTable
    Private Property oDtPopup As DataTable
        Get
            Return _oDtPopup
        End Get
        Set(value As DataTable)
            _oDtPopup = value
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

    Public ReadOnly Property Query As String
        Get
            Return _FormHeader(0).Query
        End Get
    End Property

#End Region

#Region "Proc"
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

    Public Sub DefaultsData()
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
        Call LoadDataDoc()
    End Sub
#End Region

    Private Sub ocmloaddocument_Click(sender As Object, e As EventArgs) Handles ocmadddocument.Click
        Try

            If CheckOwner() = False Then Exit Sub

            Dim _StateSave As Boolean = False
            If Me.FTStateMailToStock.Checked = True Or Me.FTStateMailToStockReject.Checked = True Then

                HI.MG.ShowMsg.mInfo("ไม่สามารถเพิ่มเอกสารได้ เนื่องจากบัญชีได้ อนุมัติรับแล้ว...", 1504270002, Me.Text, "", System.Windows.Forms.MessageBoxIcon.Warning)
                Exit Sub
            ElseIf Me.FTStateMailToAccount.Checked = True Then
                Dim _Qry As String = ""
                _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENRcvToAcc "
                _Qry &= vbCrLf & "  SET FTStateMailToAccount='0' "
                _Qry &= vbCrLf & " , FTMailToAccountBy=''"
                _Qry &= vbCrLf & " , FTMailToAccountDate=''"
                _Qry &= vbCrLf & "  ,FTMailToAccountTime=''"
                _Qry &= vbCrLf & " WHERE FTRcvToAccNo='" & HI.UL.ULF.rpQuoted(Me.FTRcvToAccNo.Text) & "'"

                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PUR)
                Me.FTStateMailToAccount.Checked = False
            End If

            If Me.FTRcvToAccNo.Text = "" Then
                Me.FTRcvToAccNo.Focus()
                Exit Sub
            End If
            If Not (SaveData()) Then
                Exit Sub
            End If

            With _AddItemPopup
                .ogclistDoc.DataSource = Nothing
                .oSelectAll.Checked = False
                .ogvlistDoc.ActiveFilter.Clear()
                ._FNHSysCmpId = Integer.Parse(HI.ST.SysInfo.CmpID.ToString)
                .ShowDialog()
                _StateSave = ._StateSave
                oDtPopup = ._oDtSelect.Copy
            End With

            'Call LoadRcvDocument()
            If _StateSave Then
                Call SaveDataDetail()
            End If
            Call LoadDataDoc()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadRcvDocument()
        Try

            Dim _Cmd As String = ""
            Dim _Where As String = ""

            For Each R As DataRow In oDtPopup.Rows
                If _Where <> "" Then _Where &= ","
                _Where &= "'" & R!FTReceiveNo.ToString & "'"
            Next


            Dim _oDt As DataTable
            _Cmd = "SELECT    '0' AS FTStateAccApp, RCV.FTReceiveNo, RCV.FDReceiveDate, RCV.FTReceiveBy, RCV.FTInvoiceNo, RCV.FDInvoiceDate, Spl.FTSuplCode,Spl.FNHSysSuplId"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", Spl.FTSuplNameTH AS FTSuplName "
            Else
                _Cmd &= vbCrLf & ", Spl.FTSuplNameEN AS FTSuplName"
            End If
            _Cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS RCV LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS PO ON RCV.FTPurchaseNo = PO.FTPurchaseNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS Spl ON PO.FNHSysSuplId = Spl.FNHSysSuplId"
            _Cmd &= vbCrLf & "WHERE RCV.FTReceiveNo in (" & _Where & ")"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_INVEN)
            _oDtCen = _oDt.Copy


        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadDataDoc(Optional _Proc As Boolean = False, Optional State As Boolean = False)
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable

            _Cmd = "SELECT     H.FTRcvToAccNo, H.FDRcvToAccDate, D.FTReceiveNo, CASE WHEN IsDate(D.FDReceiveDate) = 1 Then convert(varchar(10),convert(datetime,D.FDReceiveDate),103) Else '' END AS FDReceiveDate "
            _Cmd &= vbCrLf & ", D.FTReceiveBy, D.FTInvoiceNo,CASE WHEN IsDate( D.FDInvoiceDate) = 1 Then convert(varchar(10),convert(datetime, D.FDInvoiceDate),103) Else '' END  FDInvoiceDate  "
            _Cmd &= vbCrLf & ", D.FNHSysSuplId, D.FTAccAppBy, D.FDAccAppDate, "
            _Cmd &= vbCrLf & "        D.FTAccAppTime, S.FTSuplCode ,Isnull(D.FTStateAccAppOrg,'0') AS  FTStateAccAppOrg "
            _Cmd &= vbCrLf & ", Isnull(D.FTStateAppPOOrg,'0') AS FTStateAppPOOrg, Isnull(D.FTStateAppPackingOrg,'0') AS FTStateAppPackingOrg"
            _Cmd &= vbCrLf & ", Isnull(D.FTPackNo,'') AS FTPackNo   ,Isnull(D.FTPurchaseNo,'') AS FTPurchaseNo"
            If _Proc Then

                If (State) Then
                    _Cmd &= vbCrLf & " , '1' AS FTSelectAll ,'1' AS FTStateAppPO , '1' AS FTStateAccApp  , '1' AS FTStateAppPacking "
                Else
                    _Cmd &= vbCrLf & " , '0' AS FTSelectAll ,'0' AS FTStateAppPO , '0' AS FTStateAccApp  , '0' AS FTStateAppPacking "
                End If

            Else
                'Org
                _Cmd &= vbCrLf & " , '0' AS FTSelectAll ,Isnull(D.FTStateAppPO,'0') AS FTStateAppPO , Isnull(D.FTStateAccApp,'0') AS FTStateAccApp  , Isnull(D.FTStateAppPacking,'0') AS FTStateAppPacking "
            End If

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", S.FTSuplNameTH AS FTSuplName "
            Else
                _Cmd &= vbCrLf & ", S.FTSuplNameEN AS FTSuplName"
            End If

            _Cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENRcvToAcc AS H WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENRcvToAcc_Detail AS D WITH (NOLOCK) ON H.FTRcvToAccNo = D.FTRcvToAccNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH (NOLOCK) ON D.FNHSysSuplId = S.FNHSysSuplId"
            _Cmd &= vbCrLf & "WHERE  H.FTRcvToAccNo='" & HI.UL.ULF.rpQuoted(Me.FTRcvToAccNo.Text) & "'"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_INVEN)

            Me.ogcdetail.DataSource = _oDt

        Catch ex As Exception

        End Try
    End Sub


    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Try
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs)
        Try
            HI.TL.HandlerControl.ClearControl(Me)

        Catch ex As Exception

        End Try
    End Sub

    'Private Function SaveData() As Boolean
    '    Try
    '        Dim _Cmd As String = ""


    '        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_INVEN)
    '        HI.Conn.SQLConn.SqlConnectionOpen()
    '        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

    '        _Cmd = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENRcvToAcc "
    '        _Cmd &= vbCrLf & "Set FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '        _Cmd &= vbCrLf & ",FDUpdDate =" & HI.UL.ULDate.FormatDateDB
    '        _Cmd &= vbCrLf & ",FTUpdTime =" & HI.UL.ULDate.FormatTimeDB
    '        _Cmd &= vbCrLf & ",FTRemark ='" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "'"
    '        _Cmd &= vbCrLf & "WHERE FTRcvToAccNo='" & HI.UL.ULF.rpQuoted(Me.FTRcvToAccNo.Text) & "'"
    '        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '            _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENRcvToAcc "
    '            _Cmd &= "(FTInsUser, FDInsDate, FTInsTime,  FTRcvToAccNo, FDRcvToAccDate, FTRcvToAccBy, FTRemark, FNHSysCmpId)"
    '            _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
    '            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
    '            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTRcvToAccNo.Text) & "'"
    '            _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FDRcvToAccDate.Text) & "'"
    '            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTRcvToAccBy.Text) & "'"
    '            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "'"
    '            _Cmd &= vbCrLf & "," & Integer.Parse(Me.FNHSysCmpId.Properties.Tag)

    '            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                HI.Conn.SQLConn.Tran.Rollback()
    '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                Return False
    '            End If
    '        End If


    '        HI.Conn.SQLConn.Tran.Commit()
    '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '        Call SaveDataDetail()
    '        Return True
    '    Catch ex As Exception
    '        HI.Conn.SQLConn.Tran.Rollback()
    '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '        Return False
    '    End Try
    'End Function

    Private Function SaveData() As Boolean

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

                                If .Text = HI.TL.Document.GetDocumentNo(_FormHeader(cind).SysDBName, _FormHeader(cind).SysTableName, "", True, _CmpH).ToString() Then
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
                            End If
                        End With

                End Select
            Next
        Next

        If (_StateNew) Then
            _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH).ToString

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

    Private Function SaveDataDetail() As Boolean
        Dim _Spls As New HI.TL.SplashScreen("Prepre Save Data.. Please Wait ")
        Try
            Dim _Cmd As String = ""
            'Dim _oDt As DataTable = _oDtCen.Copy


            'Dim _Where As String = ""

            'For Each R As DataRow In oDtPopup.Rows
            '    If _Where <> "" Then _Where &= ","
            '    _Where &= "'" & R!FTReceiveNo.ToString & "'"
            'Next



            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_INVEN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            '_Cmd = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENRcvToAcc_Detail "
            '_Cmd &= vbCrLf & "Set FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '_Cmd &= vbCrLf & ",FDUpdDate =" & HI.UL.ULDate.FormatDateDB
            '_Cmd &= vbCrLf & ",FTUpdTime =" & HI.UL.ULDate.FormatTimeDB
            '_Cmd &= vbCrLf & ",FTInvoiceNo='" & HI.UL.ULF.rpQuoted(R!FTInvoiceNo.ToString) & "'"
            '_Cmd &= vbCrLf & ",FDInvoiceDate='" & HI.UL.ULDate.ConvertEnDB(R!FDInvoiceDate.ToString) & "'"
            '_Cmd &= vbCrLf & ",FNHSysSuplId=" & Integer.Parse(R!FNHSysSuplId.ToString)
            '_Cmd &= vbCrLf & ",FTStateAccApp='" & HI.UL.ULF.rpQuoted(R!FTStateAccApp.ToString) & "'"
            '_Cmd &= vbCrLf & ",FTAccAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '_Cmd &= vbCrLf & ",FDAccAppDate=" & HI.UL.ULDate.FormatDateDB
            '_Cmd &= vbCrLf & ",FTAccAppTime=" & HI.UL.ULDate.FormatTimeDB
            '_Cmd &= vbCrLf & "WHERE FTRcvToAccNo='" & HI.UL.ULF.rpQuoted(Me.FTRcvToAccNo.Text) & "'"
            '_Cmd &= vbCrLf & "and FTReceiveNo='" & HI.UL.ULF.rpQuoted(R!FTReceiveNo.ToString) & "'"

            'If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then


            For Each R As DataRow In oDtPopup.Rows


                Select Case Val(R!FNDocState.ToString)
                    Case 2 'sample room

                        _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENRcvToAcc_Detail"
                        _Cmd &= "(FTInsUser, FDInsDate, FTInsTime, FTRcvToAccNo, FTReceiveNo, FDReceiveDate, FTReceiveBy, FTInvoiceNo, FDInvoiceDate, FNHSysSuplId"
                        _Cmd &= ",FTPurchaseNo,FTPackNo)"
                        _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTRcvToAccNo.Text) & "'"
                        _Cmd &= vbCrLf & ",RCV.FTReceiveNo, RCV.FDReceiveDate, RCV.FTReceiveBy, RCV.FTInvoiceNo, RCV.FDInvoiceDate,Spl.FNHSysSuplId"
                        _Cmd &= vbCrLf & ",RCV.FTPurchaseNo ,'" & HI.UL.ULF.rpQuoted(R!FTPackNo.ToString) & "'"
                        _Cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPReceive AS RCV WITH(NOLOCK) LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase AS PO WITH(NOLOCK)   ON RCV.FTPurchaseNo = PO.FTPurchaseNo LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS Spl WITH(NOLOCK) ON PO.FNHSysSuplId = Spl.FNHSysSuplId"
                        _Cmd &= vbCrLf & "WHERE RCV.FTReceiveNo ='" & HI.UL.ULF.rpQuoted(R!FTReceiveNo.ToString) & "'"
                        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            _Spls.Close()
                            Return False
                        End If

                    Case 3 'po service

                        _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENRcvToAcc_Detail"
                        _Cmd &= "(FTInsUser, FDInsDate, FTInsTime, FTRcvToAccNo, FTReceiveNo, FDReceiveDate, FTReceiveBy, FTInvoiceNo, FDInvoiceDate, FNHSysSuplId"
                        _Cmd &= ",FTPurchaseNo,FTPackNo)"
                        _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTRcvToAccNo.Text) & "'"
                        _Cmd &= vbCrLf & ",  FTPurchaseNo, FDPurchaseDate, FTPurchaseBy, FTInvoiceNo, FDInvoiceDate, FNHSysSuplId,  FTPurchaseNo"
                        _Cmd &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(R!FTPackNo.ToString) & "'"
                        _Cmd &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchaseService WITH(NOLOCK)"
                        _Cmd &= vbCrLf & "WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(R!FTReceiveNo.ToString) & "'"

                        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            _Spls.Close()
                            Return False
                        End If

                    Case Else

                        _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENRcvToAcc_Detail"
                        _Cmd &= "(FTInsUser, FDInsDate, FTInsTime, FTRcvToAccNo, FTReceiveNo, FDReceiveDate, FTReceiveBy, FTInvoiceNo, FDInvoiceDate, FNHSysSuplId"
                        _Cmd &= ",FTPurchaseNo,FTPackNo)"
                        _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTRcvToAccNo.Text) & "'"
                        _Cmd &= vbCrLf & ",RCV.FTReceiveNo, RCV.FDReceiveDate, RCV.FTReceiveBy, RCV.FTInvoiceNo, RCV.FDInvoiceDate,Spl.FNHSysSuplId"
                        _Cmd &= vbCrLf & ",RCV.FTPurchaseNo ,'" & HI.UL.ULF.rpQuoted(R!FTPackNo.ToString) & "'"
                        _Cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS RCV WITH(NOLOCK) LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS PO WITH(NOLOCK)   ON RCV.FTPurchaseNo = PO.FTPurchaseNo LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS Spl WITH(NOLOCK) ON PO.FNHSysSuplId = Spl.FNHSysSuplId"
                        _Cmd &= vbCrLf & "WHERE RCV.FTReceiveNo ='" & HI.UL.ULF.rpQuoted(R!FTReceiveNo.ToString) & "'"

                        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            _Spls.Close()
                            Return False
                        End If

                End Select


            Next
            'End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Spls.Close()
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Spls.Close()
            Return False
        End Try
    End Function

    Private Function Approve() As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _Proc As Boolean = False

            'With CType(Me.ogcdetail.DataSource, DataTable)
            '    .AcceptChanges()
            '    Dim _oDt As DataTable = .Copy
            'End With

            Dim _oDt As DataTable = CType(Me.ogcdetail.DataSource, DataTable)
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_INVEN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _oDt.Select("(FTStateAccApp <> FTStateAccAppOrg) Or (FTStateAppPO <> FTStateAppPOOrg) Or (FTStateAppPacking <> FTStateAppPackingOrg) ")
                If HI.UL.ULF.rpQuoted(R!FTStateAccApp.ToString) <> HI.UL.ULF.rpQuoted(R!FTStateAccAppOrg.ToString) Then
                    _Cmd = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENRcvToAcc_Detail "
                    _Cmd &= vbCrLf & "Set FTStateAccApp='" & HI.UL.ULF.rpQuoted(R!FTStateAccApp.ToString) & "'"
                    _Cmd &= vbCrLf & ",FTAccAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ",FDAccAppDate=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ",FTAccAppTime=" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",FTStateAccAppOrg='" & HI.UL.ULF.rpQuoted(R!FTStateAccApp.ToString) & "'"
                    _Cmd &= vbCrLf & "WHERE FTRcvToAccNo='" & HI.UL.ULF.rpQuoted(Me.FTRcvToAccNo.Text) & "'"
                    _Cmd &= vbCrLf & "and FTReceiveNo='" & HI.UL.ULF.rpQuoted(R!FTReceiveNo.ToString) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If
                End If

                If HI.UL.ULF.rpQuoted(R!FTStateAppPO.ToString) <> HI.UL.ULF.rpQuoted(R!FTStateAppPOOrg.ToString) Then
                    _Cmd = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENRcvToAcc_Detail "
                    _Cmd &= vbCrLf & "Set  FTAccAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ",FDAccAppDate=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ",FTAccAppTime=" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",FTStateAccAppOrg='" & HI.UL.ULF.rpQuoted(R!FTStateAccApp.ToString) & "'"
                    _Cmd &= vbCrLf & ",FTStateAppPO='" & HI.UL.ULF.rpQuoted(R!FTStateAppPO.ToString) & "'"
                    _Cmd &= vbCrLf & ",FTStateAppPOOrg='" & HI.UL.ULF.rpQuoted(R!FTStateAppPO.ToString) & "'"
                    _Cmd &= vbCrLf & "WHERE FTRcvToAccNo='" & HI.UL.ULF.rpQuoted(Me.FTRcvToAccNo.Text) & "'"
                    _Cmd &= vbCrLf & "and FTReceiveNo='" & HI.UL.ULF.rpQuoted(R!FTReceiveNo.ToString) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If
                End If

                If HI.UL.ULF.rpQuoted(R!FTStateAppPacking.ToString) <> HI.UL.ULF.rpQuoted(R!FTStateAppPackingOrg.ToString) Then
                    _Cmd = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENRcvToAcc_Detail "
                    _Cmd &= vbCrLf & "Set FTAccAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ",FDAccAppDate=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ",FTAccAppTime=" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",FTStateAppPacking='" & HI.UL.ULF.rpQuoted(R!FTStateAppPacking.ToString) & "'"
                    _Cmd &= vbCrLf & ",FTStateAppPackingOrg='" & HI.UL.ULF.rpQuoted(R!FTStateAppPacking.ToString) & "'"
                    _Cmd &= vbCrLf & "WHERE FTRcvToAccNo='" & HI.UL.ULF.rpQuoted(Me.FTRcvToAccNo.Text) & "'"
                    _Cmd &= vbCrLf & "and FTReceiveNo='" & HI.UL.ULF.rpQuoted(R!FTReceiveNo.ToString) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If
                End If
                _Proc = True
            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return _Proc
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try

            If CheckOwner() = False Then Exit Sub

            Dim _StateSave As Boolean = True
            If VerifyData() Then
                Dim _oDt As DataTable = CType(Me.ogcdetail.DataSource, DataTable)
                For Each R As DataRow In _oDt.Select("(FTStateAccApp <> FTStateAccAppOrg) Or (FTStateAppPO <> FTStateAppPOOrg) Or (FTStateAppPacking <> FTStateAppPackingOrg) ")
                    _StateSave = False
                Next

                If Not (_StateSave) Then
                    If HI.MG.ShowMsg.mConfirmProcess("การบันทึกเอกสาร ระบบจะล้างข้อมูลที่มีการเลือกอนุมัติไว้ก่อนหน้า คุณต้องการบันทึกเอกสารใช่หรือไม่", 1504272201) = False Then
                        Exit Sub
                    End If
                End If
                If SaveData() Then

                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    Call LoadDataDoc()
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Function VerifyData() As Boolean
        Try
            If Me.FTRcvToAccNo.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTRcvToAccNo_lbl.Text)
                Me.FTRcvToAccNo.Focus()
                Return False
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmapprove_Click(sender As Object, e As EventArgs) Handles ocmapprove.Click
        Try
            If VerifyData() Then
                If Approve() Then
                    HI.MG.ShowMsg.mInfo("Approve Success", 15042300011, Me.Text, "", System.Windows.Forms.MessageBoxIcon.Information)
                    Call LoadDataDoc()
                Else
                    HI.MG.ShowMsg.mInfo("Approve failed", 15042300012, Me.Text, "", System.Windows.Forms.MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub



    Private Function DeleteData() As Boolean
        Try
            Dim _Cmd As String = ""


            _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENRcvToAcc"
            _Cmd &= vbCrLf & "WHERE FTRcvToAccNo='" & HI.UL.ULF.rpQuoted(Me.FTRcvToAccNo.Text) & "'"
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_INVEN)

            _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENRcvToAcc_Detail"
            _Cmd &= vbCrLf & "WHERE FTRcvToAccNo='" & HI.UL.ULF.rpQuoted(Me.FTRcvToAccNo.Text) & "'"
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_INVEN)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTRcvToAccBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then

            Return True

        Else

            Dim _Qry As String = ""
            Dim _Qry2 As String = ""
            Dim _FNHSysTeamGrpId As Integer = 0
            Dim _FNHSysTeamGrpIdTo As Integer = 0

            _Qry = "SELECT TOP 1  FNHSysTeamGrpId  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(FTRcvToAccBy.Text) & "' "
            _FNHSysTeamGrpId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")))

            _Qry2 = "SELECT TOP 1  FNHSysTeamGrpId  "
            _Qry2 &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry2 &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "
            _FNHSysTeamGrpIdTo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry2, Conn.DB.DataBaseName.DB_SECURITY, "")))

            If _FNHSysTeamGrpId > 0 Then

                If _FNHSysTeamGrpId = _FNHSysTeamGrpIdTo Then

                    Return True

                Else

                    HI.MG.ShowMsg.mProcessError(1405280901, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                    Return False

                End If

            Else

                HI.MG.ShowMsg.mProcessError(1405280901, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                Return False

            End If

        End If

    End Function

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try
            If CheckOwner() = False Then Exit Sub
            If VerifyData() Then
                If StateApprove() Then
                    If DeleteData() Then
                        MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                        HI.TL.HandlerControl.ClearControl(Me)
                        Call LoadDataDoc()
                    Else
                        MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    End If
                Else
                    MG.ShowMsg.mInfo("ไม่สามารถลบเอกสารใบนี้ได้ เนื่องจากมีการ Approve รับไปแล้ว..", 1504250001, Me.Text, "", System.Windows.Forms.MessageBoxIcon.Warning)
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            HI.TL.HandlerControl.ClearControl(Me)
            FNHSysCmpId.Text = HI.ST.SysInfo.CmpID
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Try
            'Call LoadDataDoc()
            Call LoadDataInfo(Me.FTRcvToAccNo.Text)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTRcvToAccNo_TextChanged(sender As Object, e As EventArgs) Handles FTRcvToAccNo.TextChanged
        'Try
        '    Call LoadDataDoc()
        'Catch ex As Exception
        'End Try
    End Sub

    Private Sub ogvdetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvdetail.KeyDown
        Try
            Dim _Cmd As String = ""

            If Me.ocmdelete.Enabled = False Then Exit Sub
            If e.KeyCode = System.Windows.Forms.Keys.Delete Then
                With ogvdetail
                    If .RowCount <= 0 Then Exit Sub
                    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                    For Each i As Integer In .GetSelectedRows()
                        If .GetRowCellValue(i, "FTStateAccApp").ToString = "0" And .GetRowCellValue(i, "FTStateAppPO").ToString = "0" And .GetRowCellValue(i, "FTStateAppPacking").ToString = "0" Then
                            Dim _RcvNo As String = "" & .GetRowCellValue(i, "FTReceiveNo").ToString

                            _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENRcvToAcc_Detail"
                            _Cmd &= vbCrLf & " WHERE FTRcvToAccNo='" & HI.UL.ULF.rpQuoted(Me.FTRcvToAccNo.Text) & "'"
                            _Cmd &= vbCrLf & " And FTReceiveNo='" & HI.UL.ULF.rpQuoted(_RcvNo) & "'"
                            _Cmd &= vbCrLf & " And ISNULL(FTStateAccApp,'')<>'1'"
                            _Cmd &= vbCrLf & " And ISNULL(FTStateAppPO,'')<>'1'"
                            _Cmd &= vbCrLf & " And ISNULL(FTStateAppPacking,'')<>'1'"

                            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_INVEN)

                        End If
                    Next

                End With
                Call LoadDataDoc()

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepositoryFTStateAccApp_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryFTStateAccApp.EditValueChanging
        Try


            With ogvdetail
                If .RowCount <= 0 Then Exit Sub
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                If ocmapprove.Enabled = False Then
                    e.Cancel = True
                Else
                    If e.NewValue = "1" Then
                        If .GetRowCellValue(.FocusedRowHandle, "FTStateAppPO").ToString = "1" And .GetRowCellValue(.FocusedRowHandle, "FTStateAppPacking").ToString = "1" Then
                            .SetRowCellValue(.FocusedRowHandle, "FTSelectAll", "1")
                        End If
                    Else
                        .SetRowCellValue(.FocusedRowHandle, "FTSelectAll", "0")
                    End If
                End If

            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryFTSelectAll_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryFTSelectAll.EditValueChanging
        Try

            With ogvdetail

                If .RowCount <= 0 Then Exit Sub
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                If ocmapprove.Enabled = False Then
                    e.Cancel = True
                Else
                    If e.NewValue = "1" Then
                        .SetRowCellValue(.FocusedRowHandle, "FTStateAccApp", "1")
                        .SetRowCellValue(.FocusedRowHandle, "FTStateAppPO", "1")
                        .SetRowCellValue(.FocusedRowHandle, "FTStateAppPacking", "1")
                    Else
                        .SetRowCellValue(.FocusedRowHandle, "FTStateAccApp", "0")
                        .SetRowCellValue(.FocusedRowHandle, "FTStateAppPO", "0")
                        .SetRowCellValue(.FocusedRowHandle, "FTStateAppPacking", "0")
                    End If
                End If

            End With
        Catch ex As Exception
        End Try
    End Sub


    Private Sub RepositoryFTStateAppPacking_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryFTStateAppPacking.EditValueChanging
        Try
            With ogvdetail

                If .RowCount <= 0 Then Exit Sub
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                If ocmapprove.Enabled = False Then
                    e.Cancel = True
                Else
                    If e.NewValue = "1" Then
                        If .GetRowCellValue(.FocusedRowHandle, "FTStateAccApp").ToString = "1" And .GetRowCellValue(.FocusedRowHandle, "FTStateAppPO").ToString = "1" Then
                            .SetRowCellValue(.FocusedRowHandle, "FTSelectAll", "1")
                        End If
                    Else
                        .SetRowCellValue(.FocusedRowHandle, "FTSelectAll", "0")
                    End If
                End If

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryFTStateAppPO_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryFTStateAppPO.EditValueChanging
        Try
            With ogvdetail


                If .RowCount <= 0 Then Exit Sub
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                If ocmapprove.Enabled = False Then
                    e.Cancel = True
                Else
                    If e.NewValue = "1" Then
                        If .GetRowCellValue(.FocusedRowHandle, "FTStateAccApp").ToString = "1" And .GetRowCellValue(.FocusedRowHandle, "FTStateAppPacking").ToString = "1" Then
                            .SetRowCellValue(.FocusedRowHandle, "FTSelectAll", "1")
                        End If
                    Else
                        .SetRowCellValue(.FocusedRowHandle, "FTSelectAll", "0")
                    End If
                End If

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Function StateApprove() As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable

            _Cmd = "SELECT  Top 1    FTStateAccApp, FTStateAppPO, FTStateAppPacking"
            _Cmd &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENRcvToAcc_Detail"
            _Cmd &= vbCrLf & " Where FTRcvToAccNo = '" & HI.UL.ULF.rpQuoted(Me.FTRcvToAccNo.Text) & "'"
            _Cmd &= vbCrLf & " AND (Isnull(FTStateAccApp,'0') ='1' Or Isnull(FTStateAppPO,'0') = '1' Or Isnull(FTStateAppPacking,'0')  ='1')"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_INVEN)

            Return _oDt.Rows.Count <= 0

        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmmailtoaccount_Click(sender As Object, e As EventArgs) Handles ocmmailtoaccount.Click
        Try
            If CheckOwner() = False Then Exit Sub

            If VerifyData() Then


                Dim tmpsubject As String = ""
                Dim tmpmessage As String = ""
                Dim _Qry As String = ""
                Dim _oDt As DataTable

                _Qry = "    SELECT DISTINCT U.FTUserName"
                _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS U WITH (NOLOCK) INNER JOIN"
                _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp AS T WITH (NOLOCK) ON U.FNHSysTeamGrpId = T.FNHSysTeamGrpId INNER JOIN"
                _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS P WITH (NOLOCK) ON U.FTUserName = P.FTUserName INNER JOIN"
                _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionCmp AS C WITH (NOLOCK) ON P.FNHSysPermissionID = C.FNHSysPermissionID"
                _Qry &= vbCrLf & "WHERE     (Isnull(T.FTStateAccount,'0') = '1') AND (C.FNHSysCmpId = " & Integer.Parse(HI.ST.SysInfo.CmpID.ToString) & ")"

                _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SECURITY)

                If _oDt.Rows.Count <= 0 Then
                    HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลกลุ่ม User Accont กรุณาทำการติดต่อผู้ดูแลระบบ !!!", 1504290017, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                    Exit Sub
                End If

                tmpsubject = "Send Document No." & FTRcvToAccNo.Text & "   "

                tmpmessage = "Send Document No. " & FTRcvToAccNo.Text
                tmpmessage &= vbCrLf & " To Account Team."
                tmpmessage &= vbCrLf & "Date :" & FDRcvToAccDate.Text
                tmpmessage &= vbCrLf & "By :" & FTRcvToAccBy.Text
                tmpmessage &= vbCrLf & "Note :" & FTRemark.Text

                For Each R As DataRow In _oDt.Rows

                    If HI.Mail.ClsSendMail.SendMail(HI.ST.UserInfo.UserName, HI.UL.ULF.rpQuoted(R!FTUserName.ToString), tmpsubject, tmpmessage, 6, Me.FTRcvToAccNo.Text) Then

                        _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENRcvToAcc "
                        _Qry &= vbCrLf & "  SET FTStateMailToAccount='1' "
                        _Qry &= vbCrLf & " , FTMailToAccountBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & " , FTMailToAccountDate=" & HI.UL.ULDate.FormatDateDB & " "
                        _Qry &= vbCrLf & "  ,FTMailToAccountTime=" & HI.UL.ULDate.FormatTimeDB & " "
                        _Qry &= vbCrLf & "  ,FTStateMailToStockReject='0' "
                        _Qry &= vbCrLf & "  ,FTStateMailToStock='0' "
                        _Qry &= vbCrLf & "  ,FTMailToStockBy=''"
                        _Qry &= vbCrLf & " WHERE FTRcvToAccNo='" & HI.UL.ULF.rpQuoted(Me.FTRcvToAccNo.Text) & "'"

                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PUR)

                    End If

                Next

                HI.MG.ShowMsg.mInfo("Send Mail To Account Complete !!!", 1504250109, Me.Text, Me.FTRcvToAccNo.Text, System.Windows.Forms.MessageBoxIcon.Information)

                ' End If

                Me.FTStateMailToAccount.Checked = True
                Me.FTStateMailToStockReject.Checked = False

            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub LoadDataInfo(Key As Object)
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

        Me.oSelectAll.Checked = False
        Call LoadDataDoc()

        _ProcLoad = False
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        Try
            If VerifyData() Then
                With New HI.RP.Report
                    .FormTitle = Me.Text
                    .ReportFolderName = "Inventrory\"
                    .ReportName = "ReportSendDoctoAccount.rpt"
                    .Formular = "{TINVENRcvToAcc.FTRcvToAccNo} ='" & HI.UL.ULF.rpQuoted(FTRcvToAccNo.Text) & "' "
                    .Preview()
                End With
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmmailtostock_Click(sender As Object, e As EventArgs) Handles ocmmailtostock.Click
        Try
            If VerifyData() Then
                If Me.FTStateMailToAccount.Checked = False Then
                    MG.ShowMsg.mInfo("Is not send mail from stock.  You cannot send mail.", 1505200012, Me.Text, "", System.Windows.Forms.MessageBoxIcon.Stop)
                    Exit Sub
                End If

                Dim tmpsubject As String = ""
                Dim tmpmessage As String = ""
                Dim _Qry As String = ""
                'Dim _oDt As DataTable
                '_Qry = "    SELECT DISTINCT U.FTUserName"
                '_Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS U WITH (NOLOCK) INNER JOIN"
                '_Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp AS T WITH (NOLOCK) ON U.FNHSysTeamGrpId = T.FNHSysTeamGrpId INNER JOIN"
                '_Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS P WITH (NOLOCK) ON U.FTUserName = P.FTUserName INNER JOIN"
                '_Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionCmp AS C WITH (NOLOCK) ON P.FNHSysPermissionID = C.FNHSysPermissionID"
                '_Qry &= vbCrLf & "WHERE     (Isnull(T.FTStateStock,'0') = '1') AND (C.FNHSysCmpId = " & Integer.Parse(HI.ST.SysInfo.CmpID.ToString) & ")"
                '_oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SECURITY)

                tmpsubject = " Document No." & FTRcvToAccNo.Text & " Successfully Received...  "

                tmpmessage = " Document No. " & FTRcvToAccNo.Text
                tmpmessage &= vbCrLf & " To Stock Team."
                tmpmessage &= vbCrLf & " Thank you very much.."



                If HI.Mail.ClsSendMail.SendMail(HI.ST.UserInfo.UserName, HI.UL.ULF.rpQuoted(Me.FTRcvToAccBy.Text), tmpsubject, tmpmessage, 6, Me.FTRcvToAccNo.Text) Then
                    _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENRcvToAcc "
                    _Qry &= vbCrLf & "  SET FTStateMailToStock='1' "
                    _Qry &= vbCrLf & " , FTMailToStockBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " , FTMailToStockDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Qry &= vbCrLf & "  ,FTMailToStockTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    _Qry &= vbCrLf & " WHERE FTRcvToAccNo='" & HI.UL.ULF.rpQuoted(Me.FTRcvToAccNo.Text) & "'"
                 

                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PUR)


                     

                End If


                HI.MG.ShowMsg.mInfo("Send Mail To Stock Complete !!!", 1504250129, Me.Text, Me.FTRcvToAccNo.Text, System.Windows.Forms.MessageBoxIcon.Information)

                ' End If

                Me.FTStateMailToStock.Checked = True

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles ocmmailtostockReject.Click
        Try
            If VerifyData() Then
                If Me.FTStateMailToAccount.Checked = False Then
                    MG.ShowMsg.mInfo("Is not send mail from stock.  You cannot send mail.", 1505200012, Me.Text, "", System.Windows.Forms.MessageBoxIcon.Stop)
                    Exit Sub
                End If
                Dim tmpsubject As String = ""
                Dim tmpmessage As String = ""
                Dim _Qry As String = ""
                'Dim _oDt As DataTable
                '_Qry = "    SELECT DISTINCT U.FTUserName"
                '_Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS U WITH (NOLOCK) INNER JOIN"
                '_Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp AS T WITH (NOLOCK) ON U.FNHSysTeamGrpId = T.FNHSysTeamGrpId INNER JOIN"
                '_Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS P WITH (NOLOCK) ON U.FTUserName = P.FTUserName INNER JOIN"
                '_Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionCmp AS C WITH (NOLOCK) ON P.FNHSysPermissionID = C.FNHSysPermissionID"
                '_Qry &= vbCrLf & "WHERE     (Isnull(T.FTStateStock,'0') = '1') AND (C.FNHSysCmpId = " & Integer.Parse(HI.ST.SysInfo.CmpID.ToString) & ")"
                '_oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SECURITY)

                tmpsubject = " Document No." & FTRcvToAccNo.Text & " failed Received...  "

                tmpmessage = " Document No. " & FTRcvToAccNo.Text
                tmpmessage &= vbCrLf & " To Stock Team."
                tmpmessage &= vbCrLf & " please check sending document. "
                tmpmessage &= vbCrLf & " Thank you."



                If HI.Mail.ClsSendMail.SendMail(HI.ST.UserInfo.UserName, HI.UL.ULF.rpQuoted(Me.FTRcvToAccBy.Text), tmpsubject, tmpmessage, 6, Me.FTRcvToAccNo.Text) Then
                    _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENRcvToAcc "
                    _Qry &= vbCrLf & "  SET FTStateMailToStockReject='1' "
                    _Qry &= vbCrLf & " , FTMailToStockBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " , FTMailToStockDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Qry &= vbCrLf & "  ,FTMailToStockTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    _Qry &= vbCrLf & " WHERE FTRcvToAccNo='" & HI.UL.ULF.rpQuoted(Me.FTRcvToAccNo.Text) & "'"
                    _Qry &= vbCrLf & " "
                    _Qry &= vbCrLf & "   UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENRcvToAcc "
                    _Qry &= vbCrLf & "  SET FTStateMailToAccount='0' "
                    _Qry &= vbCrLf & " , FTMailToStockBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " , FTMailToStockDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Qry &= vbCrLf & "  ,FTMailToStockTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    _Qry &= vbCrLf & " WHERE FTRcvToAccNo='" & HI.UL.ULF.rpQuoted(Me.FTRcvToAccNo.Text) & "'"
                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PUR)



                End If

                HI.MG.ShowMsg.mInfo("Send Mail To Stock Complete !!!", 1504250129, Me.Text, Me.FTRcvToAccNo.Text, System.Windows.Forms.MessageBoxIcon.Information)


                ' End If

                Me.FTStateMailToStockReject.Checked = True
                Me.FTStateMailToAccount.Checked = False

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FTRcvToAccNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTRcvToAccNo.EditValueChanged

    End Sub

    Private Sub oSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles oSelectAll.CheckedChanged
        Try
            If Me.oSelectAll.Checked = True Then
                Call LoadDataDoc(True, True)
            Else
                Call LoadDataDoc(True, False)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub wSendDocToAccApprove_Load(sender As Object, e As EventArgs) Handles Me.Load
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID
    End Sub
End Class