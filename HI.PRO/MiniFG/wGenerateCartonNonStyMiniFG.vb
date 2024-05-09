Imports System.Drawing

Public Class wGenerateCartonNonStyMiniFG


    Private _ListDataPackOrg As New List(Of DataTable)
    Private _ListDataSubOrderPack As New List(Of DataTable)
    Private _ListDataSubOrderBal As New List(Of DataTable)
    Private _ListDataSubOrderBlank As New List(Of DataTable)

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call InitGrid()
    End Sub


#Region "Property"

    Private _FTPackNo As String = ""
    Property FTPackNo As String
        Get
            Return _FTPackNo
        End Get
        Set(value As String)
            _FTPackNo = value
        End Set
    End Property

    Private _FNHSysStyleId As Integer = 0
    Property FNHSysStyleId As Integer
        Get
            Return _FNHSysStyleId
        End Get
        Set(value As Integer)
            _FNHSysStyleId = value
        End Set
    End Property

    Private _FNPackSetValue As Integer = 0
    Property FNPackSetValue As Integer
        Get
            Return _FNPackSetValue
        End Get
        Set(value As Integer)
            _FNPackSetValue = value
        End Set
    End Property

    Private _Process As Boolean = False
    Property Process As Boolean
        Get
            Return _Process
        End Get
        Set(value As Boolean)
            _Process = value
        End Set
    End Property

    Property ListDataPackOrg As DataTable
        Get
            Return _ListDataPackOrg(0)
        End Get
        Set(value As DataTable)
            _ListDataPackOrg.Clear()
            _ListDataPackOrg.Add(value.Copy)
        End Set
    End Property

    Private _ObjectParent As Object = Nothing
    Public Property ObjectParent As Object
        Get
            Return _ObjectParent
        End Get
        Set(value As Object)
            _ObjectParent = value
        End Set
    End Property
#End Region

#Region "Procedure"
    Private Sub InitGrid()


    End Sub

    Private Sub LoadPackDefault()

    End Sub

    Private Sub CreateBreakDownForPack(dt As DataTable, _dtpackmerge As DataTable)
        Dim _colcount As Integer = 0



        With dt
            For Each R As DataRow In .Rows
                For Each Col As DataColumn In .Columns
                    Select Case Col.ColumnName.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper
                        Case Else
                            R.Item(Col) = 0
                    End Select
                Next
            Next
        End With

        With _dtpackmerge
            For Each R As DataRow In .Rows
                For Each Col As DataColumn In .Columns
                    Select Case Col.ColumnName.ToUpper
                        Case "FTColorway".ToUpper, "FTPOLine".ToUpper
                        Case Else
                            R.Item(Col) = 0
                    End Select
                Next
            Next
        End With


    End Sub


    Private Sub SetDefaultSetPack()

        Try

            Dim _Qry As String = ""
            Dim _PackPerCarton As Integer
            Dim _SubOrder As String = ""
            Dim _dt As DataTable
            Dim _dt2 As DataTable

            _Qry = "SELECT TOP 1 FTSubOrderNo "
            _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Detail AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "  WHERE  (A.FTPackNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPackNo) & "')"
            _SubOrder = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "")


            _Qry = "SELECT TOP 1 B.FNPackPerCarton,B.FNPackCartonSubType"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS B  WITH(NOLOCK)  "
            _Qry &= vbCrLf & "  WHERE  (B.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_SubOrder) & "')"

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
            _PackPerCarton = -1
            For Each R As DataRow In _dt.Rows

                FNPackCartonSubType.SelectedIndex = Integer.Parse(Val(R!FNPackCartonSubType.ToString))
                FNPackCartonSubType_SelectedIndexChanged(FNPackCartonSubType, New System.EventArgs)
                _PackPerCarton = Integer.Parse(Val(R!FNPackPerCarton.ToString))
                Me.FNPackPerCaton.Value = _PackPerCarton

            Next

        Catch ex As Exception
        End Try
    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False
        If Me.FNHSysCartonId.Text <> "" And Me.FNHSysCartonId.Properties.Tag.ToString <> "" Then
            If FNPackPerCaton.Value > 0 Then

                Dim _PackValue As Integer = FNPackPerCaton.Value
                _Pass = True


            Else
                HI.MG.ShowMsg.mInfo("กรุณมทำการระบุจำนวน Pack ต่อ กล่อง !!!", 1411150129, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNHSysCartonId_lbl.Text)
            FNHSysCartonId.Focus()
        End If
        Return _Pass
    End Function

    Private Function CreateCarton() As Boolean

        Dim _Spls As New HI.TL.SplashScreen("Creating...Carton Please Wait....")
        Try
            Dim _LastCartonNo As Integer = 0
            Dim _Qry As String = ""
            Dim _dt As DataTable
            Dim _SizeQty As Integer = 0
            Dim _PackQty As Integer = Me.FNPackPerCaton.Value
            Dim _FTOrderNo As String = ""
            Dim _FTSubOrderNo As String = ""
            Dim _FTColorway As String = ""
            Dim _POLine As String = ""
            Dim _dtMerge As DataTable




            _Qry = " SELECT MAX(FNCartonNo) AS FNCartonNo "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS T WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo) & "'"
            _LastCartonNo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD)))


            '_Qry = "Insert Into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail "
            '_Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTPackNo"
            '_Qry &= vbCrLf & " , FNCartonNo, FTOrderNo, FTSubOrderNo"
            '_Qry &= vbCrLf & "  , FTColorway, FTSizeBreakDown, FNQuantity,FNHSysCartonId,FNPackCartonSubType,FNPackPerCarton , FTPOLine)"
            '_Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            '_Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
            '_Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
            '_Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPackNo) & "' "
            '_Qry &= vbCrLf & "," & _LastCartonNo & " "
            '_Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "' "
            '_Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTSubOrderNo) & "' "
            '_Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTColorway) & "' "
            '_Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' "
            '_Qry &= vbCrLf & "," & _PackBal & " "
            '_Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysCartonId.Properties.Tag.ToString)) & " "
            '_Qry &= vbCrLf & "," & Integer.Parse(Val(FNPackCartonSubType.SelectedIndex)) & " "
            '_Qry &= vbCrLf & "," & _PackQty & " "
            '_Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_POLine) & "' "
            'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)


        Catch ex As Exception
            _Spls.Close()
            Return False
        End Try
        _Spls.Close()
        Return True
    End Function

    Private Function CreateCartonSolidmultiSubOrder() As Boolean
        Dim _Spls As New HI.TL.SplashScreen("Creating...Carton Please Wait....")
        Try

            Dim _LastCartonNo As Integer = 0
            Dim _Qry As String = ""
            Dim _dt As DataTable
            Dim _SizeQty As Integer = 0
            Dim _PackQty As Integer = Me.FNPackPerCaton.Value
            Dim _FTOrderNo As String = ""
            Dim _FTSubOrderNo As String = ""
            Dim _FTColorway As String = ""
            Dim _POLine As String = ""
            Dim _dtMerge As DataTable



            _Qry = " SELECT MAX(FNCartonNo) AS FNCartonNo "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKMiniFGOrderPack_Carton_Detail AS T WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo) & "'"
            _LastCartonNo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD)))

            Dim _ColorWay As String = ""
            Dim _SizePackQty As Integer = 0
            Dim _SizeBalQty As Integer = 0

            Dim dtpackassort As New DataTable
            dtpackassort.Columns.Add("FTSizeCode", GetType(String))
            dtpackassort.Columns.Add("FNQuantity", GetType(Integer))


            If _SizePackQty > 0 Then
                _SizeQty = _SizePackQty

                _Qry = "Insert Into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail "
                _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTPackNo"
                _Qry &= vbCrLf & " , FNCartonNo, FTOrderNo, FTSubOrderNo"
                _Qry &= vbCrLf & "  , FTColorway, FTSizeBreakDown, FNQuantity,FNHSysCartonId,FNPackCartonSubType,FNPackPerCarton,FTPOLine)"
                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPackNo) & "' "
                _Qry &= vbCrLf & "," & _LastCartonNo & " "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "' "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTSubOrderNo) & "' "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTColorway) & "' "
                '_Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(ColP.ColumnName.ToString) & "' "
                '_Qry &= vbCrLf & "," & _SizePackQty & " "
                '_Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysCartonId.Properties.Tag.ToString)) & " "
                '_Qry &= vbCrLf & "," & Integer.Parse(Val(FNPackCartonSubType.SelectedIndex)) & " "
                '_Qry &= vbCrLf & "," & Me.FNPackPerCaton.Value & " "
                '_Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_POLine) & "' "


                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)


                'If Val(_SizePackQty) > 0 Then
                '    '_SizePackQty = _SizeBalQty
                '    GoTo T1
                'End If





            End If






            _Spls.Close()
            Return True
        Catch ex As Exception
            _Spls.Close()
            Return False
        End Try
    End Function



#End Region

    Private Sub wGenerateCarton_Load(sender As Object, e As EventArgs) Handles Me.Load

        Call InitGrid()
        HI.TL.HandlerControl.ClearControl(Me.ogbcarton)
        Call SetDefaultSetPack()
    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.Process = False
        Me.Close()
    End Sub

    Private Sub FNPackCartonSubType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNPackCartonSubType.SelectedIndexChanged
        Try
            Dim _StateEdit As Boolean = False

            Select Case FNPackCartonSubType.SelectedIndex
                Case 0
                    _StateEdit = False
                    FNPackCartonScrapType.SelectedIndex = 0
                    'If (Me.FTStateMerge.Checked) Then
                    '    FNPackCartonScrapType.Enabled = True
                    'End If
                    FNPackCartonScrapType.Enabled = True

                Case Else
                    _StateEdit = True
                    FNPackCartonScrapType.SelectedIndex = 2
                    FNPackCartonScrapType.Enabled = False
            End Select



        Catch ex As Exception
        End Try

    End Sub

    Private Sub ocmcreate_Click(sender As Object, e As EventArgs) Handles ocmcreate.Click
        If Me.VerifyData Then
            If Me.CreateCartonSolidmultiSubOrder Then
                Me.Process = True
                Call CallLoadCarton()

            End If
        End If
    End Sub

    Private Sub CallLoadCarton()
        Try
            GenBarcodeEN13(FTPackNo)

            Call CallByName(_ObjectParent, "CreateTreeCarton", CallType.Method, Nothing)
        Catch ex As Exception
        End Try
    End Sub

    Private _StateSumGrid As Boolean


    Private Function GenerateBarcodeSSCC(_SBarcodeNo As Integer, _EBarcodeNo As Integer, _BeginCarton As Integer) As String
        Try
            Dim _Cmd As String = "" : Dim _Seq As Integer = 0
            Dim _BarCodeSSS As String = "" : Dim _O, _M, _T As Integer : Dim _DemoBarcode As String = "" : Dim _BarCode As String = ""

            _Cmd = "SELECT TOP (1) FTCfgData  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "]..TSESystemConfig WITH(NOLOCK) WHERE (FTCfgName = N'CfManufacturerNo')"
            Dim _FacNo As String = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SECURITY, "")

            '_Cmd = "Select Max(FNCartonNo) AS FNCartonNo From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode WITH(NOLOCK) "
            '_Cmd &= vbCrLf & "Where FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
            '_Cmd &= vbCrLf & "and isnull(FTBarCodeEAN13,'') <> ''"
            '_Seq = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0")
            _Seq = _BeginCarton



            For I As Integer = _SBarcodeNo To _EBarcodeNo
                '   _Cmd = "Select Top 1 From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode Where FTCartonNo='" & I & "'"
                ' If HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "") = "" Then

                _BarCodeSSS = _FacNo & Microsoft.VisualBasic.Right("000000000" & CStr(I), 9)

                _O = 0 : _M = 0 : _T = 0
                For x As Integer = 1 To 16
                    _DemoBarcode = _BarCodeSSS
                    If (x Mod 2) = 0 Then
                        _M += +CInt(_DemoBarcode.Substring(x - 1, 1))
                    Else
                        _O += +CInt(_DemoBarcode.Substring(x - 1, 1))
                    End If
                Next
                _M = _M * 3 : _T = _M + _O : _T = _T Mod 10
                If _T > 0 Then
                    _T = 10 - _T
                End If
                _BarCode = "000" & _BarCodeSSS & CStr(_T)


                ' End If
                _Seq += +1
            Next
            Return _BarCode

        Catch ex As Exception
            Return ""

        End Try
    End Function

    Private Function GenBarcodeEN13(PackNo As String) As Boolean
        Try
            Dim _Cmd As String = "" : Dim _EN13 As String = "" : Dim _CartonNO As String = ""
            Dim _CustomerPO As DataTable
            _Cmd = "Select    FTCustomerPO from " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "..TPACKOrderPack  where FTPackNo  = '" & PackNo & "' "
            _CustomerPO = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            Dim _oDt As System.Data.DataTable

            For Each Rz As DataRow In _CustomerPO.Rows
                _Cmd = "SELECT   C.FTColorway, C.FTSizeBreakDown, C.FTOrderNo, C.FTPackNo,  C.FTPOLine  , C.FTSubOrderNo, PK.FTCustomerPO, C.FNCartonNo, D.FTSerialFrom , D.FTSerialTo  , D.FNFrom , D.FNTo ,C.FNQuantity"
                _Cmd &= vbCrLf & " , convert(nvarchar(30) , convert(int ,D.FTSerialFrom ) + ROW_NUMBER() Over (partition by C.FTOrderNo , C.FTSubOrderNo, C.FTPOLine ,C.FTPackNo,C.FTColorway, C.FTSizeBreakDown ,PK.FTCustomerPO ,C.FNQuantity ORder by  C.FTPackNo,C.FNCartonNo) -1 )AS FNCartonSeq  "
                _Cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Detail AS C  LEFT OUTER JOIN "
                _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack AS  PK WITH(NOLOCK)    ON C.FTPackNo = PK.FTPackNo INNER JOIN    "
                _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].. TEXPTPackPlan_D as D  ON PK.FTCustomerPO = D.FTPORef and C.FTPOLine  = convert(nvarchar(30), convert(int, D.FTPOLineNo)) "
                _Cmd &= vbCrLf & " and C.FTSizeBreakDown = D.FTSizeBreakDown and    C.FTColorway= replace(replace(D.FTShortDescription,D.FTStyleCode,''),'-','')  and C.FNQuantity = D.FNQtyPerPack "

                _Cmd &= vbCrLf & "  LEFT OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKCarton AS  T WITH(NOLOCK)    ON  C.FTPackNo = T.FTPackNo AND C.FNCartonNo = T.FNCartonNo     "
                _Cmd &= vbCrLf & "WHERE  (PK.FTCustomerPO = N'" & Rz!FTCustomerPO.ToString & "')" 'ISNULL(T.FTState,'0') = '0' and 
                _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)


                For Each R As DataRow In _oDt.Rows
                    _EN13 = HI.UL.ULF.rpQuoted(GenerateBarcodeSSCC(R!FNCartonSeq.ToString, R!FNCartonSeq.ToString, R!FNCartonNo.ToString))
                    _Cmd = " Select  FTBarCodeEAN13 From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Barcode  "
                    _Cmd &= vbCrLf & " where  FTPackNo='" & HI.UL.ULF.rpQuoted(R!FTPackNo.ToString) & "'"
                    _Cmd &= vbCrLf & " and   FNCartonNo='" & HI.UL.ULF.rpQuoted(R!FNCartonNo.ToString) & "'"
                    _Cmd &= vbCrLf & " and isnull(FTBarCodeEAN13,'') <>'' "
                    If HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT).Rows.Count <= 0 Then
                        _Cmd = "Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Barcode"
                        _Cmd &= vbCrLf & "Set  FTUpdUser= '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & ",FTCartonNo='" & HI.UL.ULF.rpQuoted(R!FNCartonSeq.ToString) & "'"
                        _Cmd &= vbCrLf & ",FTBarCodeEAN13='" & HI.UL.ULF.rpQuoted(_EN13) & "'"
                        _Cmd &= vbCrLf & ",FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(_EN13) & "'"
                        _Cmd &= vbCrLf & " where  FTPackNo='" & HI.UL.ULF.rpQuoted(R!FTPackNo.ToString) & "'"
                        _Cmd &= vbCrLf & " and   FNCartonNo='" & HI.UL.ULF.rpQuoted(R!FNCartonNo.ToString) & "'"
                        If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD) = False Then
                            _Cmd = "INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Barcode (FTInsUser,FDInsDate,FTUpdTime,FTCartonNo,FTBarCodeEAN13,FTBarCodeCarton,FTPackNo,FNCartonNo)"
                            _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FNCartonSeq.ToString) & "'"
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_EN13) & "'"
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_EN13) & "'"
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPackNo.ToString) & "'"
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FNCartonNo.ToString) & "'"
                            HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                        End If
                    End If
                Next
            Next
            _Cmd = "exec  dbo.sp_updatebarcodeucc '" & PackNo & "'"
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)


            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


End Class