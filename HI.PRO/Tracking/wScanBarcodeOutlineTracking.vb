
Imports System.Windows.Forms
Imports System.Drawing
Imports DevExpress.XtraGrid.Views.Grid

Public Class wScanBarcodeOutlineTracking
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

        Me.InitFormControl()

        Call InitGrid()

    End Sub

#Region "Initial Grid"

    Private Sub InitGrid()

        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "FNQuantity|FNQuantityOut|FNQtyBal"
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

            '
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


        _ProcLoad = False
    End Sub

    Private Sub LoadDucumentDetail()
        Dim _Spls As New HI.TL.SplashScreen("Please wait Loading Data...")
        Try
            Dim _Qry As String = ""
            Dim dt As DataTable
            _Qry = " declare  @Tmp table ("
            _Qry &= vbCrLf & " FTBarcodeNo nvarchar(30)"
            _Qry &= vbCrLf & ", FNHSysUnitSectId int , "
            _Qry &= vbCrLf & " FNQuantity int "
            _Qry &= vbCrLf & " UNIQUE CLUSTERED (FTBarcodeNo,FNHSysUnitSectId)  )  "

            _Qry &= vbCrLf & " INSERT INTO @Tmp (FTBarcodeNo ,FNHSysUnitSectId,FNQuantity )"


            _Qry &= vbCrLf & "SELECT    FTBarcodeNo, A.FNHSysUnitSectId, sum(FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline A WITH (NOLOCK)"
            _Qry &= vbCrLf & " inner join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON A.FNHSysUnitSectId = US.FNHSysUnitSectId "
            _Qry &= vbCrLf & "where FDDate >= '" & HI.UL.ULDate.ConvertEnDB(Me.FDDateTrans.Text) & "'"
            _Qry &= vbCrLf & "  and us.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID)
            _Qry &= vbCrLf & "group by FTBarcodeNo, A.FNHSysUnitSectId"
            _Qry &= vbCrLf & ""

            _Qry &= vbCrLf & "  INSERT INTO @Tmp (FTBarcodeNo ,FNHSysUnitSectId,FNQuantity ) "
            _Qry &= vbCrLf & "SELECT        FTBarcodeNo, A.FNHSysUnitSectId, sum(FNScanQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail A WITH (NOLOCK)"
            _Qry &= vbCrLf & " inner join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON A.FNHSysUnitSectId = US.FNHSysUnitSectId "

            _Qry &= vbCrLf & "where FDScanDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FDDateTrans.Text) & "'"
            _Qry &= vbCrLf & "and not exists (SELECT        FTBarcodeNo "
            _Qry &= vbCrLf & "FROM   @Tmp z where z.FTBarcodeNo  = a.FTBarcodeNo  "
            _Qry &= vbCrLf & " )"
            _Qry &= vbCrLf & "  and us.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID)
            _Qry &= vbCrLf & "group by FTBarcodeNo, A.FNHSysUnitSectId"



            _Qry &= vbCrLf & "  Select  A.FNHSysUnitSectId, A.FTBarcodeNo, P.FTOrderNo, US.FTUnitSectCode , B.FTColorway, B.FTSizeBreakDown "
            _Qry &= vbCrLf & ", B.FNBunbleSeq ,Isnull( B.FTPOLineItemNo , '')  AS FTNikePOLineItem ,B.FNQuantity  "

            _Qry &= vbCrLf & ",Case when isdate(HA.FDDocScanDate)= 1 Then convert(varchar(10),convert(datetime,HA.FDDocScanDate),103) Else '' END AS FDDate "
            _Qry &= vbCrLf & " into #Tmptable"

            _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS A WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON A.FNHSysUnitSectId = US.FNHSysUnitSectId INNER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH (NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH (NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo INNER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan AS HA WITH (NOLOCK) ON A.FTDocScanNo = HA.FTDocScanNo  "
            _Qry &= vbCrLf & "WHERE     (US.FTStateSew = '1') "
            _Qry &= vbCrLf & "  and us.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID)
            If Me.FDDateTrans.Text <> "" Then
                _Qry &= vbCrLf & "and  HA.FDDocScanDate  >='" & HI.UL.ULDate.ConvertEnDB(Me.FDDateTrans.Text) & "'"
            End If
            If Me.FDDateTransTo.Text <> "" Then
                _Qry &= vbCrLf & "and  HA.FDDocScanDate  <='" & HI.UL.ULDate.ConvertEnDB(Me.FDDateTransTo.Text) & "'"
            End If

            If Me.FNHSysUnitSectId.Text <> "" Then
                _Qry &= vbCrLf & "And US.FTUnitSectCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "'"
            End If
            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & "And US.FTUnitSectCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "'"
            End If

            If Me.FTOrderNo.Text <> "" Then
                _Qry &= vbCrLf & "And P.FTOrderNo >='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            End If
            If Me.FTOrderNoTo.Text <> "" Then
                _Qry &= vbCrLf & "And P.FTOrderNo <='" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
            End If






            '_Qry &= vbCrLf & "  SELECT    A.FNHSysUnitSectId, A.FTBarcodeNo, P.FTOrderNo, US.FTUnitSectCode , B.FTColorway, B.FTSizeBreakDown, SUM(B.FNQuantity) AS FNQuantity"
            '_Qry &= vbCrLf & ",sum(isnull(BO.FNQuantity,0)) AS FNQuantityOut , O.FTPORef , B.FNBunbleSeq"

            '_Qry &= vbCrLf & ",Isnull( B.FTPOLineItemNo , '')  AS FTNikePOLineItem"
            '_Qry &= vbCrLf & ",SUM(B.FNQuantity) -  sum(isnull(BO.FNQuantity,0))  AS FNQtyBal"
            '_Qry &= vbCrLf & ",Case when isdate(HA.FDDocScanDate)= 1 Then convert(varchar(10),convert(datetime,HA.FDDocScanDate),103) Else '' END AS FDDate "

            _Qry &= vbCrLf & "  SELECT    A.FNHSysUnitSectId, A.FTBarcodeNo,  A.FTOrderNo,  A.FTUnitSectCode ,  A.FTColorway,  A.FTSizeBreakDown, SUM( A.FNQuantity) AS FNQuantity "
            _Qry &= vbCrLf & " ,sum(isnull(BO.FNQuantity,0)) AS FNQuantityOut , O.FTPORef ,  A.FNBunbleSeq"
            _Qry &= vbCrLf & " , A. FTNikePOLineItem"
            _Qry &= vbCrLf & " ,SUM( A.FNQuantity) -  sum(isnull(BO.FNQuantity,0))  AS FNQtyBal"
            _Qry &= vbCrLf & " ,a.FDDate "
            _Qry &= vbCrLf & "  FROM   #Tmptable as A LEFT OUTER JOIN  "
            '_Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS A WITH (NOLOCK) INNER JOIN"
            '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON A.FNHSysUnitSectId = US.FNHSysUnitSectId INNER JOIN"
            '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH (NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo INNER JOIN"
            ''_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode_Detail AS BD WITH (NOLOCK) ON B.FTBarcodeBundleNo = BD.FTBarcodeBundleNo INNER JOIN"
            '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH (NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo INNER JOIN"
            '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan AS HA WITH (NOLOCK) ON A.FTDocScanNo = HA.FTDocScanNo LEFT OUTER JOIN "


            _Qry &= vbCrLf & "@Tmp AS BO   ON A.FTBarcodeNo = BO.FTBarcodeNo "

            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo"

            _Qry &= vbCrLf & "WHERE 1=1 "

            If Me.FTCustomerPO.Text <> "" Then
                _Qry &= vbCrLf & "And O.FTPORef >='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'"
            End If
            If Me.FTCustomerPOTo.Text <> "" Then
                _Qry &= vbCrLf & "And O.FTPORef <='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPOTo.Text) & "'"
            End If

            _Qry &= vbCrLf & "GROUP BY  A.FDDate, A.FNHSysUnitSectId ,A.FTBarcodeNo,  A.FTOrderNo,  A.FTUnitSectCode,  A.FTColorway, A.FTSizeBreakDown  , O.FTPORef, A.FNBunbleSeq , A.FTNikePOLineItem  "
            _Qry &= vbCrLf & "having (SUM(A.FNQuantity) - sum(isnull(BO.FNQuantity,0)) ) > 0"

            _Qry &= vbCrLf & " drop table #Tmptable "



            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
            Me.ogcdetail.DataSource = dt.Copy
            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try
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

    End Sub

    Private Sub LoadData(HSysId As String)
        Dim _Str As String = Me.Query & "  WHERE " & Me.MainKey & "='" & HSysId & "' "
        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)
        Dim _FieldName As String = ""
        For Each R As DataRow In _dt.Rows
            For Each Col As DataColumn In _dt.Columns
                _FieldName = Col.ColumnName.ToString

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)

                                .Text = R.Item(Col).ToString
                                Call HI.TL.HandlerControl.DynamicButtonedit_Leave(Obj, New System.EventArgs)
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
                            With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                Try
                                    .DateTime = HI.UL.ULDate.ConvertEN(R.Item(Col).ToString)
                                Catch ex As Exception
                                    .Text = ""
                                End Try
                            End With

                        Case Else
                            Obj.Text = R.Item(Col).ToString
                    End Select
                Next
            Next

            Exit For
        Next

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

#End Region

#Region "MAIN PROC"

    Private Sub Proc_Clear(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        Me.FormRefresh()
    End Sub

    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

    Private Sub ogvdetail_RowCountChanged(sender As Object, e As EventArgs) Handles ogvdetail.RowCountChanged
        Try
            Dim dt As New DataTable
            Try
                dt = CType(ogcdetail.DataSource, DataTable).Copy
            Catch ex As Exception
            End Try
            dt.Dispose()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub wScanBarcodeSendSupl_Load(sender As Object, e As EventArgs) Handles Me.Load
        'FTBarcodeNo.EnterMoveNextControl = False
        'FNHSysUnitSectId.TabStop = False
        'Me.FDDateTrans.Text = HI.Conn.SQLConn.GetField("Select  convert(nvarchar(10),Convert(varchar(10),Getdate(),103),103) ", Conn.DB.DataBaseName.DB_PROD, Date.Now)
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        If VerrifyData() Then
            Call LoadDucumentDetail()
        End If
    End Sub

    Private Function VerrifyData() As Boolean
        Try
            If Me.FDDateTrans.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FDDateTrans_lbl.Text)
                Me.FDDateTrans.Focus()
                Return False
            End If
            If Me.FDDateTransTo.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FDDateTransTo_lbl.Text)
                Me.FDDateTransTo.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Return True
        End Try
    End Function
    'Private Sub FTBarcodeNo_KeyDown(sender As Object, e As KeyEventArgs)
    '    Try
    '        Select Case e.KeyCode
    '            Case Keys.Enter
    '                If ChkBarCode(Me.FTBarcodeNo.Text) Then
    '                    If ScansBarcode(Me.FTBarcodeNo.Text) Then
    '                        Call LoadDucumentDetail()
    '                    End If
    '                    FTBarcodeNo.Focus()
    '                    FTBarcodeNo.SelectAll()
    '                Else
    '                    HI.MG.ShowMsg.mInfo("Barcode ยังไม่มีการแสกนเข้าไลน์......", 1506181749, Me.Text, "", MessageBoxIcon.Stop)
    '                    FTBarcodeNo.Focus()
    '                    FTBarcodeNo.SelectAll()
    '                End If
    '        End Select

    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Private Function ChkBarCode(_BarcodeKey As String) As Boolean
    '    Try

    '        Dim _Cmd As String = ""
    '        Dim _oDt As DataTable
    '        _Cmd = "SELECT   Top 1  A.FTDocScanNo, A.FTBarcodeNo, A.FNHSysUnitSectId, US.FTUnitSectCode, US.FTUnitSectNameTH, US.FTUnitSectNameEN, B.FTBarcodeBundleNo, B.FNBunbleSeq, B.FTColorway, "
    '        _Cmd &= vbCrLf & "  B.FTSizeBreakDown, B.FTOrderProdNo, BD.FTLayCutNo, BD.FNLayCutSeq, BD.FNQuantity, T.FTStyleCode ,O.FTOrderNo , O.FTPORef  "
    '        _Cmd &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS A WITH (NOLOCK) INNER JOIN"
    '        _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON A.FNHSysUnitSectId = US.FNHSysUnitSectId INNER JOIN"
    '        _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH (NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo INNER JOIN"
    '        _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS BD WITH (NOLOCK) ON B.FTBarcodeBundleNo = BD.FTBarcodeBundleNo INNER JOIN "
    '        _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH (NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo  "
    '        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON  P.FTOrderNo = O.FTOrderNo"  'LEFT(B.FTOrderProdNo,CHARINDEX('-',B.FTOrderProdNo,1)-1)
    '        _Cmd &= vbCrLf & "   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS T WITH(NOLOCK) ON O.FNHSysStyleId = T.FNHSysStyleId"
    '        _Cmd &= vbCrLf & "WHERE  A.FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarcodeKey) & "' and    (US.FTStateSew = '1')"
    '        _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
    '        For Each R As DataRow In _oDt.Rows
    '            Me.FNHSysUnitSectId.Text = HI.UL.ULF.rpQuoted(R!FTUnitSectCode.ToString)
    '            Me.FTColorway.Text = HI.UL.ULF.rpQuoted(R!FTColorway.ToString)
    '            Me.FTSizeBreakDown.Text = HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString)
    '            Me.FTSubOrderNo.Text = HI.UL.ULF.rpQuoted(R!FTOrderProdNo.ToString)
    '            Me.FNBunbleSeq.Text = HI.UL.ULF.rpQuoted(R!FNBunbleSeq.ToString)
    '            Me.FTStyleCode.Text = HI.UL.ULF.rpQuoted(R!FTStyleCode.ToString)
    '            Me.FTOrderNo.Text = HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString)
    '            Me.FTPORef.Text = HI.UL.ULF.rpQuoted(R!FTPORef.ToString)
    '            Return True
    '        Next
    '        Return False
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function

    'Private Function ScansBarcode(BarcodeKey As String) As Boolean
    '    Try
    '        If Not (Me.FTStateDeleteBarcode.Checked) Then
    '            If Not (ChkBarcodeInlineBal(BarcodeKey, Me.FTOrderNo.Text, FNHSysUnitSectId.Properties.Tag)) Then
    '                HI.MG.ShowMsg.mInfo("BarCode ออกไลน์หมดแล้ว...", 1506191040, Me.Text, "", MessageBoxIcon.Stop)
    '                Return False
    '            End If
    '        End If

    '        Dim _Cmd As String = ""

    '        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
    '        HI.Conn.SQLConn.SqlConnectionOpen()
    '        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

    '        If Not (Me.FTStateDeleteBarcode.Checked) Then

    '            _Cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline"
    '            _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '            _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
    '            _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
    '            _Cmd &= vbCrLf & ",FNQuantity=FNQuantity+1"
    '            _Cmd &= vbCrLf & "WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
    '            _Cmd &= vbCrLf & "And FNHSysUnitSectId=" & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
    '            _Cmd &= vbCrLf & "And FDDate =" & HI.UL.ULDate.FormatDateDB
    '            _Cmd &= vbCrLf & "And FTTime = Convert(varchar(5),Getdate(),114)"

    '            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline"
    '                _Cmd &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime , FTBarcodeNo, FNHSysUnitSectId, FDDate, FTTime, FNQuantity)"
    '                _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
    '                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
    '                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
    '                _Cmd &= vbCrLf & "," & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
    '                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
    '                _Cmd &= vbCrLf & ",Convert(varchar(5),Getdate(),114)"
    '                _Cmd &= vbCrLf & ",1"

    '                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                    HI.Conn.SQLConn.Tran.Rollback()
    '                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                    Return False
    '                End If

    '            End If
    '        Else
    '            _Cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline"
    '            _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '            _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
    '            _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
    '            _Cmd &= vbCrLf & ",FNQuantity=FNQuantity-1"
    '            _Cmd &= vbCrLf & "WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
    '            _Cmd &= vbCrLf & "And FNHSysUnitSectId=" & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
    '            _Cmd &= vbCrLf & "And FDDate +'|'+FTTime in ("
    '            _Cmd &= vbCrLf & "Select top 1 FDDate +'|'+FTTime  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline "
    '            _Cmd &= vbCrLf & "WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
    '            _Cmd &= vbCrLf & "And FNHSysUnitSectId=" & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
    '            _Cmd &= vbCrLf & "ORder by FDDate Desc,FTTime Desc )"

    '            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                HI.Conn.SQLConn.Tran.Rollback()
    '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                Return False
    '            End If

    '            _Cmd = "Select FNQuantity From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline"
    '            _Cmd &= vbCrLf & "WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
    '            _Cmd &= vbCrLf & "And FNHSysUnitSectId=" & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
    '            _Cmd &= vbCrLf & "And FDDate +'|'+FTTime in ("
    '            _Cmd &= vbCrLf & "Select top 1 FDDate +'|'+FTTime  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline "
    '            _Cmd &= vbCrLf & "WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
    '            _Cmd &= vbCrLf & "And FNHSysUnitSectId=" & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
    '            _Cmd &= vbCrLf & "ORder by FDDate Desc,FTTime Desc )"

    '            If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0") <= 0 Then

    '                _Cmd = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline"
    '                _Cmd &= vbCrLf & "WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
    '                _Cmd &= vbCrLf & "And FNHSysUnitSectId=" & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
    '                _Cmd &= vbCrLf & "And FDDate +'|'+FTTime in ("
    '                _Cmd &= vbCrLf & "Select top 1 FDDate +'|'+FTTime  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline "
    '                _Cmd &= vbCrLf & "WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
    '                _Cmd &= vbCrLf & "And FNHSysUnitSectId=" & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
    '                _Cmd &= vbCrLf & "ORder by FDDate Desc,FTTime Desc )"

    '                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                    HI.Conn.SQLConn.Tran.Rollback()
    '                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                    Return False
    '                End If

    '            End If

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


    Private Function ChkBarcodeInlineBal(BarCode As String, OrderNo As String, UnitSectId As Integer) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _QtyIn As Double = 0 : Dim _QtyOut As Double = 0
            _Cmd = "SELECT     A.FTBarcodeNo, P.FTOrderNo, US.FTUnitSectCode AS FTUnitSectCodeSew, B.FTColorway, B.FTSizeBreakDown, SUM(BD.FNQuantity) AS FNQuantity"
            _Cmd &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS A WITH (NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON A.FNHSysUnitSectId = US.FNHSysUnitSectId INNER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B WITH (NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo INNER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode_Detail AS BD WITH (NOLOCK) ON B.FTBarcodeBundleNo = BD.FTBarcodeBundleNo INNER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH (NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo INNER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan AS HA WITH (NOLOCK) ON A.FTDocScanNo = HA.FTDocScanNo"
            _Cmd &= vbCrLf & "WHERE     (US.FTStateSew = '1') "
            _Cmd &= vbCrLf & "AND (A.FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(BarCode) & "')"
            _Cmd &= vbCrLf & "AND (P.FTOrderNo = '" & HI.UL.ULF.rpQuoted(OrderNo) & "') "
            _Cmd &= vbCrLf & "AND (A.FNHSysUnitSectId=" & Integer.Parse(UnitSectId) & ")"
            _Cmd &= vbCrLf & "GROUP BY A.FTBarcodeNo, P.FTOrderNo, US.FTUnitSectCode, B.FTColorway, B.FTSizeBreakDown"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            For Each R As DataRow In _oDt.Rows
                _QtyIn = Double.Parse(R!FNQuantity.ToString)
                Exit For
            Next

            _Cmd = "SELECT     A.FTBarcodeNo, A.FNHSysUnitSectId , P.FTOrderNo, SUM(A.FNQuantity) AS FNQuantity"
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS A LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B ON A.FTBarcodeNo = B.FTBarcodeBundleNo INNER JOIN "
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH (NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo"
            _Cmd &= vbCrLf & "WHERE (A.FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(BarCode) & "')"
            _Cmd &= vbCrLf & "AND (P.FTOrderNo = '" & HI.UL.ULF.rpQuoted(OrderNo) & "') "
            _Cmd &= vbCrLf & "AND (A.FNHSysUnitSectId=" & Integer.Parse(UnitSectId) & ")"
            _Cmd &= vbCrLf & "GROUP BY A.FTBarcodeNo, A.FNHSysUnitSectId, P.FTOrderNo "
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            For Each R As DataRow In _oDt.Rows
                _QtyOut = Double.Parse(R!FNQuantity.ToString)
                Exit For
            Next
            Return _QtyIn > _QtyOut
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ogvdetail_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles ogvdetail.RowStyle
        Try
            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then 
                'Dim _Qtyin As Integer = Integer.Parse("0" & View.GetRowCellValue(e.RowHandle, "FNQuantity").ToString)
                Dim _QtyOut As Integer = Integer.Parse("0" & View.GetRowCellValue(e.RowHandle, "FNQuantityOut").ToString)
                If _QtyOut > 0 Then
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class