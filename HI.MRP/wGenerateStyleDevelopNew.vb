Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Columns
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Reflection
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraEditors.Controls

Public Class wGenerateStyleDevelopNew

    Private _dtpart As New List(Of DataTable)

    Dim View As GridView
    Dim RowsIndex As Double
    Dim TopVisibleIndex As Int32
    Private sFNHSysStyleDevId As String
    Private ViewColor As GridView
    Private ViewSize As GridView
    Private _wNewColorway As wNewColorwayDevelop
    Private _wChangeColorway As wNewChangeColorwayDevelop
    Private _wNewSize As wNewSize
    Private _wChangeDesc As wChangeColorDesc
    Private _wGenNewMaterial As wGenerateNewItem
    Private _CopyStyle As wCopyDevStyle

    Dim oleDbDataAdapter1 As DbDataAdapter
    Dim oleDbDataAdapter2 As DbDataAdapter
    Dim dtStyleDetail As DataTable
    Dim dtBefore As DataTable
    Dim dtAfter As DataTable

    Private CmSysUnitId As Integer = 0
    Private Enum TabIndexs As Integer
        StyleDetail = 0
        Colorway = 1
        SizeBreakdown = 2
    End Enum



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

#End Region

#Region "Handler Control"

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.


        Dim oSysLang As New HI.ST.SysLanguage
        'Call HI.ST.Lang.InsertLanguage(_CopyStyle)


        _wNewColorway = New wNewColorwayDevelop
        HI.TL.HandlerControl.AddHandlerObj(_wNewColorway)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wNewColorway.Name.ToString.Trim, _wNewColorway)
        Catch ex As Exception
        Finally
        End Try
        Call HI.ST.Lang.SP_SETxLanguage(_wNewColorway)



        _CopyStyle = New wCopyDevStyle
        HI.TL.HandlerControl.AddHandlerObj(_CopyStyle)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _CopyStyle.Name.ToString.Trim, _CopyStyle)
        Catch ex As Exception
        Finally
        End Try


        _wChangeColorway = New wNewChangeColorwayDevelop
        HI.TL.HandlerControl.AddHandlerObj(_wChangeColorway)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wChangeColorway.Name.ToString.Trim, _wChangeColorway)
        Catch ex As Exception
        Finally
        End Try
        Call HI.ST.Lang.SP_SETxLanguage(_wChangeColorway)


        _wNewSize = New wNewSize
        HI.TL.HandlerControl.AddHandlerObj(_wNewSize)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wNewSize.Name.ToString.Trim, _wNewSize)
        Catch ex As Exception
        Finally
        End Try
        Call HI.ST.Lang.SP_SETxLanguage(_wNewSize)

        _wChangeDesc = New wChangeColorDesc
        HI.TL.HandlerControl.AddHandlerObj(_wChangeDesc)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wChangeDesc.Name.ToString.Trim, _wChangeDesc)
        Catch ex As Exception
        Finally
        End Try
        Call HI.ST.Lang.SP_SETxLanguage(_wChangeDesc)

        _wGenNewMaterial = New wGenerateNewItem
        HI.TL.HandlerControl.AddHandlerObj(_wGenNewMaterial)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wGenNewMaterial.Name.ToString.Trim, _wGenNewMaterial)
        Catch ex As Exception
        Finally
        End Try
        Call HI.ST.Lang.SP_SETxLanguage(_wNewColorway)

        With RepositoryFTUnitCode
            AddHandler .Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
            AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
            AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave
        End With

        With RepositoryFNHSysSuplId
            AddHandler .Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
            AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
            AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave
        End With

        With RepositoryFNHSysCurId
            AddHandler .Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
            AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
            AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave
        End With


        Try
            Call LoadPartMaster()

            Me.ogcpart.DataSource = _dtpart(0).Copy

            Call LoadSetPart()
            Call LoadItemMaster()
        Catch ex As Exception

        End Try

    End Sub
#End Region

#Region "Form Load"

    Private _ProcComplete As Boolean = False
    Public Property ProcComplete As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(value As Boolean)
            _ProcComplete = value
        End Set
    End Property

    Private Sub wGenerateStyle_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            ' View = New GridView()
            View = ogcmat.Views(0)
            View.FocusedRowHandle = 0


            View.OptionsView.ShowAutoFilterRow = False
            View.OptionsView.NewItemRowPosition = NewItemRowPosition.None
            View.OptionsNavigation.AutoFocusNewRow = True
            View.OptionsBehavior.AllowAddRows = True
            View.OptionsBehavior.AllowDeleteRows = True
            View.OptionsBehavior.Editable = True
            View.OptionsView.ShowAutoFilterRow = True
            View.BestFitColumns()

            ogvmat.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 300
            ogvcolor.Columns.ColumnByFieldName("FTMainMatName").Width = 200
            ogvsize.Columns.ColumnByFieldName("FTMainMatName").Width = 200
            ogvmat.Columns.ColumnByFieldName("FTPartNameEN").Width = 150
            ogvmat.Columns.ColumnByFieldName("FTPartNameTH").Width = 150
            ogvcolor.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            ogvsize.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            ogvmat.Columns.ColumnByFieldName("FTComponent").Width = 100

            FTUpdUser.Text = HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)
            FDUpdDate.Text = "??/??/????"
            FTUpdTime.Text = "??:??:??"

            sFNHSysStyleDevId = ""

            ViewColor = Me.ogccolor.Views(0)
            ViewSize = Me.ogcsize.Views(0)

            ogvpart.Columns.ColumnByFieldName("FTSelect").Width = 40
            ogvpart.Columns.ColumnByFieldName("FTPartName").Width = 150

            RemoveHandler FNHSysStyleDevId.EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged
            RemoveHandler FNHSysStyleDevId.Leave, AddressOf HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly


            Dim cmdstring As String = "select top 1 FNHSysUnitId from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS X With(NOLOCK) WHERE X.FTUnitCode='CM' "
            CmSysUnitId = Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_MASTER, "0"))
            If CmSysUnitId = 0 Then
                CmSysUnitId = 1311090002
            End If

            Call TabChange()
            Call CreateMergeEditControl()

        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "MAIN PROC"

    Private Function CheckOwner() As Boolean

        If (HI.ST.UserInfo.UserName.ToUpper = FTUpdUser.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Or FTUpdUser.Text.ToUpper = "" Then
            Return True
        Else
            Dim _Qry As String = ""
            Dim _Qry2 As String = ""
            _Qry = "SELECT TOP 1  FNHSysMerTeamId  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(FTUpdUser.Text) & "' "

            _Qry2 = "SELECT TOP 1  FNHSysMerTeamId  "
            _Qry2 &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry2 &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "") = HI.Conn.SQLConn.GetField(_Qry2, Conn.DB.DataBaseName.DB_SECURITY, "") Then
                Return True
            Else
                HI.MG.ShowMsg.mProcessError(1411200101, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข Style นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                Return False
            End If
        End If

    End Function

    Private Sub Proc_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub FNHSysStyleDevId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysStyleDevId.EditValueChanged
        Dim _FNHSysStyleDevId As Integer = Integer.Parse(Val(FNHSysStyleDevId.Properties.Tag.ToString))

        Try
            If Me.InvokeRequired Then
                Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FNHSysStyleDevId_EditValueChanged), New Object() {sender, e})
            Else
                'If _FNHSysStyleDevId <> 0 Then
                '    Dim _Str As String = "SELECT TOP 1 FNHSysStyleDevId FROM [" &
                '        HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle  WITH(NOLOCK) WHERE FTStyleDevCode ='" &
                '        _FNHSysStyleDevId & "' AND FTSeason='" &
                '        HI.UL.ULF.rpQuoted(FTSeason.Text) & "' AND ISNULL(FNVersion,0)=" &
                '        FNVersion.Text & " "
                '    FNHSysStyleDevId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")

                If _FNHSysStyleDevId <> 0 Then

                    Call LoadStyleInfo(_FNHSysStyleDevId, True)
                    ' Call LoadStyleDetail(FNHSysStyleDevId.Properties.Tag.ToString)
                    Call LoadStylePostInfo()

                Else
                    FNHSysCustId.Text = Nothing
                    FNHSysCustId_None.Text = Nothing
                    FTUpdUser.Text = Nothing
                    FDUpdDate.Text = Nothing
                    FTUpdTime.Text = Nothing
                    ogcmat.DataSource = Nothing
                    ogcmat.Refresh()
                    ogccolor.DataSource = Nothing
                    ogccolor.Refresh()
                    ogcsize.DataSource = Nothing
                    ogcsize.Refresh()
                End If

                sFNHSysStyleDevId = FNHSysStyleDevId.Text
                'Else
                '    ogcmat.DataSource = Nothing
                '    ogcmat.Refresh()
                '    ogccolor.DataSource = Nothing
                '    ogccolor.Refresh()
                '    ogcsize.DataSource = Nothing
                '    ogcsize.Refresh()
                'End If
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Function CheckPostDataToBomSheet() As Boolean
        'If FTStatePost.Checked Then
        '    HI.MG.ShowMsg.mInfo("มีการ Post To BOM แล้ว ไม่สามารถทำการแก้ไขหรือเปลี่ยนแปลงใดๆ !!!", 1506167781, Me.Text, , MessageBoxIcon.Warning)
        '    Return False
        'Else
        '    Return True
        'End If

        Return True

    End Function

    Private Function VerifyMasterData() As Boolean

        Dim _Qry As String = ""
        Dim _FNHSysStyleDevId As Integer = Integer.Parse(Val(FNHSysStyleDevId.Properties.Tag.ToString))

        _Qry = "SELECT TOP 1  A.FNHSysStyleDevId"
        _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_Mat AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & "  WHERE  (A.FNHSysStyleDevId =" & Val(_FNHSysStyleDevId) & ") "
        _Qry &= vbCrLf & "         AND (A.FTUnitCode = N'')   And ISNULL(A.FTStateNotShowBomSheet,'0') <>'1' "

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "") <> "" Then
            HI.MG.ShowMsg.mInfo("ที่ยังไม่ได้ทำการระบุ ข้อมูล Unit ไม่สามารถทำการ Post ได้กรุณาทำการตรวจสอบ !!!", 1506308723, Me.Text, , MessageBoxIcon.Warning)
            Return False
        End If

        _Qry = "SELECT TOP 1  A.FNHSysStyleDevId"
        _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_Mat AS A WITH(NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH(NOLOCK)  ON A.FTSuplCode = S.FTSuplCode"
        _Qry &= vbCrLf & "  WHERE  (A.FNHSysStyleDevId =" & Val(_FNHSysStyleDevId) & ") "
        _Qry &= vbCrLf & "  AND (A.FTSuplCode <> N'') "
        _Qry &= vbCrLf & "  AND (S.FTSuplCode IS NULL)"

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "") <> "" Then
            HI.MG.ShowMsg.mInfo("ข้อมูล Supplier ไม่ถูกต้อง ตาม Master File ไม่สามารถทำการ Post ได้กรุณาทำการตรวจสอบ !!!", 1506308724, Me.Text, , MessageBoxIcon.Warning)
            Return False
        End If

        _Qry = "SELECT TOP 1  A.FNHSysStyleDevId"
        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_Mat AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS S WITH(NOLOCK)  ON A.FTUnitCode = S.FTUnitCode"
        _Qry &= vbCrLf & "WHERE  (A.FNHSysStyleDevId =" & Val(_FNHSysStyleDevId) & ")  And ISNULL(A.FTStateNotShowBomSheet,'0') <>'1'  "
        _Qry &= vbCrLf & "AND (A.FTUnitCode <> N'') "
        _Qry &= vbCrLf & "AND (S.FTUnitCode IS NULL)"

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "") <> "" Then
            HI.MG.ShowMsg.mInfo("ข้อมูล Unit ไม่ถูกต้อง ตาม Master File ไม่สามารถทำการ Post ได้กรุณาทำการตรวจสอบ !!!", 1506308725, Me.Text, , MessageBoxIcon.Warning)
            Return False
        End If

        _Qry = "SELECT TOP 1 A.FNHSysStyleDevId"
        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_Mat AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & "WHERE  (A.FNHSysStyleDevId =" & Val(_FNHSysStyleDevId) & ") "
        _Qry &= vbCrLf & "AND (A.FTSuplCode = N'') "
        _Qry &= vbCrLf & "AND (A.FTStateFree ='1')"

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "") <> "" Then
            HI.MG.ShowMsg.mInfo("พบข้อมูล Item Free ที่ยังไม่ได้ทำการระบุ ข้อมูล Supplier ไม่สามารถทำการ Post ได้กรุณาทำการตรวจสอบ !!!", 1506308726, Me.Text, , MessageBoxIcon.Warning)
            Return False
        End If

        _Qry = "UPDATE A SET A.FTItemREfNo=MM.FTMainMatCode"
        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_Mat As A "
        _Qry &= vbCrLf & "INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat  As MM With(NOLOCK)  On A.FTItemNo = MM.FTCusItemCodeRef "
        _Qry &= vbCrLf & "  WHERE  (A.FNHSysStyleDevId =" & Val(_FNHSysStyleDevId) & ") And ISNULL(A.FTStateNotShowBomSheet,'0') <>'1' AND ISNULL(A.FTItemREfNo,'')='' AND MM.FTStateActive='1' "
        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        _Qry = "SELECT A.FTItemNo"
        _Qry &= vbCrLf & ", A.FTItemDesc"
        _Qry &= vbCrLf & ", B.FTSuplCode AS FNHSysSuplId"
        _Qry &= vbCrLf & ", B.FNHSysSuplId AS FNHSysSuplId_Hide"
        _Qry &= vbCrLf & ", C.FTUnitCode AS FNHSysUnitId"
        _Qry &= vbCrLf & ", C.FNHSysUnitId AS FNHSysUnitId_Hide"
        _Qry &= vbCrLf & ", CUR.FTCurCode AS FNHSysCurId"
        _Qry &= vbCrLf & ", A.FNHSysCurId AS FNHSysCurId_Hide"
        _Qry &= vbCrLf & ", '' AS FNMerMatType,0 AS FNMerMatType_Hide"
        _Qry &= vbCrLf & ", '' AS FNHSysMatGrpId,0 AS FNHSysMatGrpId_Hide"
        _Qry &= vbCrLf & ", '' AS FNHSysMatTypeId,0 AS FNHSysMatTypeId_Hide"
        _Qry &= vbCrLf & ", '' AS FNHSysCustId,0 AS FNHSysCustId_Hide"
        _Qry &= vbCrLf & ", '' AS FTFabricFrontSize"
        _Qry &= vbCrLf & ", 0.00 AS FNPrice"
        _Qry &= vbCrLf & ", '0'AS FTStateMainMaterial"
        _Qry &= vbCrLf & ", '0'AS FTStateNotCheckResuorce"
        _Qry &= vbCrLf & ", '0'AS FTStateHanger"
        _Qry &= vbCrLf & ", '0'AS FTStateOpenPR"
        _Qry &= vbCrLf & ", '0'AS FTStateSplitPO,A.FTStateNominate"
        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_Mat AS A WITH(NOLOCK) "
        _Qry &= vbCrLf
        _Qry &= vbCrLf & "OUTER APPLY (SELECT TOP 1 MM.FNHSysMainMatId  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat  AS MM WITH(NOLOCK) WHERE MM.FTCusItemCodeRef = A.FTItemNo AND MM.FTStateActive ='1' ) AS MM "
        _Qry &= vbCrLf
        _Qry &= vbCrLf & "OUTER APPLY (SELECT TOP 1 MM.FNHSysMainMatId  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat  AS MM WITH(NOLOCK) WHERE MM.FTItemComboRef = A.FTItemNo AND MM.FTStateActive ='1' ) AS MMCBO "
        _Qry &= vbCrLf
        _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS C WITH(NOLOCK) ON A.FTUnitCode = C.FTUnitCode "
        _Qry &= vbCrLf
        _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS B WITH(NOLOCK) ON A.FTSuplCode = B.FTSuplCode"
        _Qry &= vbCrLf
        _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS CUR WITH(NOLOCK) ON A.FNHSysCurId = CUR.FNHSysCurId "
        _Qry &= vbCrLf & "  WHERE  (A.FNHSysStyleDevId =" & Val(_FNHSysStyleDevId) & ") AND ISNULL(A.FTStateNotShowBomSheet,'0') <>'1' AND ISNULL(A.FTStateLabel,'0') <>'1' AND ISNULL(A.FTItemREfNo,'')='' "
        _Qry &= vbCrLf
        _Qry &= vbCrLf & "AND (MM.FNHSysMainMatId IS NULL) "
        _Qry &= vbCrLf & "AND (MMCBO.FNHSysMainMatId IS NULL) "

        _Qry &= vbCrLf & "ORDER BY A.FTItemNo "
        Dim _dtNewItem As DataTable
        _dtNewItem = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        If _dtNewItem.Rows.Count > 0 Then
            With _wGenNewMaterial
                HI.ST.Lang.SP_SETxLanguage(_wGenNewMaterial)
                .StateGenItem = False
                .ogvdetail.OptionsView.ShowAutoFilterRow = False
                .ogcdetail.DataSource = _dtNewItem.Copy
                .ogcdetail.Refresh()
                .ShowDialog()

                If .StateGenItem Then
                    LoadItemMaster()
                End If
                '
            End With
        End If

        _dtNewItem.Dispose()


        _Qry = "SELECT TOP 1  A.FNHSysStyleDevId"
        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_Mat AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat  AS MM WITH(NOLOCK)  ON A.FTItemNo = MM.FTCusItemCodeRef"
        _Qry &= vbCrLf & "WHERE  (A.FNHSysStyleDevId =" & Val(_FNHSysStyleDevId) & ") AND ISNULL(A.FTStateNotShowBomSheet,'0') <>'1' AND ISNULL(A.FTItemREfNo,'')=''  "
        _Qry &= vbCrLf & "AND (MM.FTCusItemCodeRef IS NULL)"

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "") <> "" Then
            HI.MG.ShowMsg.mInfo("ข้อมูล Item ไม่ถูกต้อง ตาม Master File ไม่สามารถทำการ Post ได้กรุณาทำการตรวจสอบ !!!", 1506308727, Me.Text, , MessageBoxIcon.Warning)
            Return False
        End If

        Return True
    End Function

    Private Function CheckColorWay() As Boolean
        Dim _FNHSysStyleDevId As Integer = Integer.Parse(Val(FNHSysStyleDevId.Properties.Tag.ToString))

        Try
            Dim _Qry As String = ""
            Dim _FNHSysMatColorId As Integer = Val(HI.TL.RunID.GetRunNoID("TMERMMatColor", "FNHSysMatColorId", Conn.DB.DataBaseName.DB_MASTER).ToString())
            _FNHSysMatColorId = _FNHSysMatColorId - 1

            Dim FNMatColorSeq As Decimal = Decimal.Parse(Format(Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNMatColorSeq FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatColor AS X WITH(NOLOCK) ORDER BY FNMatColorSeq DESC", Conn.DB.DataBaseName.DB_MASTER, "0")), "0.00"))

            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatColor ("
            _Qry &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime, FNHSysMatColorId"
            _Qry &= vbCrLf & ", FTMatColorCode, FNMatColorSeq, FTMatColorNameTH, FTMatColorNameEN"
            _Qry &= vbCrLf & ", FTRemark, FTStateActive)"
            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & ", " & HI.UL.ULDate.FormatDateDB
            _Qry &= vbCrLf & ", " & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & ", " & _FNHSysMatColorId & "  + Row_Number() Over (Order By A.FTColorWay ) AS FNHSysMatColorId"
            _Qry &= vbCrLf & ", A.FTColorWay"
            _Qry &= vbCrLf & ", " & FNMatColorSeq & "  + Row_Number() Over (Order By A.FTColorWay ) AS FNMatColorSeq"
            _Qry &= vbCrLf & ", A.FTColorNameTH AS FTMatColorNameTH"
            _Qry &= vbCrLf & ", A.FTColorNameEN AS FTMatColorNameEN"
            _Qry &= vbCrLf & ", '' AS FTRemark"
            _Qry &= vbCrLf & ", '1' AS FTStateActive"
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_ColorWay AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatColor AS MC WITH(NOLOCK)  ON A.FTColorWay = MC.FTMatColorCode"
            _Qry &= vbCrLf & "WHERE (A.FNHSysStyleDevId =" & _FNHSysStyleDevId & ") AND  (MC.FTMatColorCode IS NULL)"
            _Qry &= vbCrLf & "GROUP BY A.FTColorWay"
            _Qry &= vbCrLf & "ORDER BY A.FTColorWay"

            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        Catch ex As Exception
            Return False
        End Try
        Return True

    End Function

    Private Function CheckRawMatColor() As Boolean
        Dim _FNHSysStyleDevId As Integer = Integer.Parse(Val(FNHSysStyleDevId.Properties.Tag.ToString))

        Try
            Dim _Qry As String = ""
            Dim _FNHSysMatColorId As Integer = Val(HI.TL.RunID.GetRunNoID("TINVENMMatColor", "FNHSysRawMatColorId", Conn.DB.DataBaseName.DB_MASTER).ToString())
            _FNHSysMatColorId = _FNHSysMatColorId - 1

            Dim FNMatColorSeq As Decimal = Decimal.Parse(Format(Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNRawMatColorSeq FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS X WITH(NOLOCK) ORDER BY FNRawMatColorSeq DESC", Conn.DB.DataBaseName.DB_MASTER, "0")), "0.00"))

            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor("
            _Qry &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FNHSysRawMatColorId"
            _Qry &= vbCrLf & ", FTRawMatColorCode, FNRawMatColorSeq, FTRawMatColorNameTH,FTRawMatColorNameEN "
            _Qry &= vbCrLf & ", FTRemark, FTStateActive, FTGCWNo )"
            _Qry &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & ", " & HI.UL.ULDate.FormatDateDB
            _Qry &= vbCrLf & ", " & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & ", " & _FNHSysMatColorId & " + Row_Number() Over (Order By A.FTColorCode) AS FNHSysMatColorId"
            _Qry &= vbCrLf & ", A.FTColorCode "
            _Qry &= vbCrLf & ", " & FNMatColorSeq & "  + Row_Number() Over (Order By A.FTColorCode ) AS FNMatColorSeq"
            _Qry &= vbCrLf & ", MAX(A.FTColorNameTH) AS FTMatColorNameTH"
            _Qry &= vbCrLf & ", MAX(A.FTColorNameEN) AS FTMatColorNameEN"
            _Qry &= vbCrLf & ", '' AS FTRemark"
            _Qry &= vbCrLf & ", '1' AS FTStateActive,''"
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_ColorWay AS A WITH(NOLOCK) "
            _Qry &= vbCrLf
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS MC WITH(NOLOCK)  ON  A.FTColorCode = MC.FTRawMatColorCode "
            _Qry &= vbCrLf & "WHERE (A.FNHSysStyleDevId =" & _FNHSysStyleDevId & ")  AND ISNULL(A.FTColorCode,'') <> ''  AND  (MC.FTRawMatColorCode IS NULL)"
            _Qry &= vbCrLf
            _Qry &= vbCrLf & "GROUP BY A.FTColorCode"
            _Qry &= vbCrLf & "ORDER BY A.FTColorCode"
            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Private Function CheckSizeBeakDown() As Boolean
        Dim _FNHSysStyleDevId As Integer = Integer.Parse(Val(FNHSysStyleDevId.Properties.Tag.ToString))

        Try
            Dim _Qry As String = ""
            Dim _FNHSysMatColorId As Integer = Val(HI.TL.RunID.GetRunNoID("TMERMMatSize", "FNHSysMatSizeId", Conn.DB.DataBaseName.DB_MASTER).ToString())
            _FNHSysMatColorId = _FNHSysMatColorId - 1

            Dim FNMatColorSeq As Decimal = Decimal.Parse(Format(Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNMatSizeSeq FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS X WITH(NOLOCK) ORDER BY FNMatColorSeq DESC", Conn.DB.DataBaseName.DB_MASTER, "0")), "0.00"))

            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize("
            _Qry &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FNHSysMatSizeId"
            _Qry &= vbCrLf & ", FTMatSizeCode, FNMatSizeSeq, FTMatSizeNameTH, FTMatSizeNameEN"
            _Qry &= vbCrLf & ", FTRemark, FTStateActive)"
            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & "," & _FNHSysMatColorId & "  + Row_Number() Over (Order By A.FTSizeBreakDown ) AS FNHSysMatColorId"
            _Qry &= vbCrLf & ",A.FTSizeBreakDown"
            _Qry &= vbCrLf & "," & FNMatColorSeq & "  + Row_Number() Over (Order By A.FTSizeBreakDown ) AS FNMatColorSeq"
            _Qry &= vbCrLf & ",A.FTSizeBreakDown AS FTMatColorNameTH"
            _Qry &= vbCrLf & ",A.FTSizeBreakDown AS FTMatColorNameEN"
            _Qry &= vbCrLf & ",'' AS FTRemark"
            _Qry &= vbCrLf & ",'1' AS FTStateActive"
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_SizeBreakDown AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS MC WITH(NOLOCK)  ON A.FTSizeBreakDown = MC.FTMatSizeCode"
            _Qry &= vbCrLf & "WHERE   (A.FNHSysStyleDevId =" & _FNHSysStyleDevId & ") AND  (MC.FTMatSizeCode IS NULL)"
            _Qry &= vbCrLf & "GROUP BY A.FTSizeBreakDown"
            _Qry &= vbCrLf & "ORDER BY A.FTSizeBreakDown"

            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        Catch ex As Exception
            Return False
        End Try
        Return True

    End Function

    Private Function CheckRawMatSize() As Boolean
        Dim _FNHSysStyleDevId As Integer = Integer.Parse(Val(FNHSysStyleDevId.Properties.Tag.ToString))

        Try
            Dim _Qry As String = ""
            Dim _FNHSysMatColorId As Integer = Val(HI.TL.RunID.GetRunNoID("TINVENMMatSize", "FNHSysRawMatSizeId", Conn.DB.DataBaseName.DB_MASTER).ToString())
            _FNHSysMatColorId = _FNHSysMatColorId - 1

            Dim FNMatColorSeq As Decimal = Decimal.Parse(Format(Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNRawMatSizeSeq FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS X WITH(NOLOCK) ORDER BY FNRawMatSizeSeq DESC", Conn.DB.DataBaseName.DB_MASTER, "0")), "0.00"))

            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize("
            _Qry &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FNHSysRawMatSizeId"
            _Qry &= vbCrLf & ", FTRawMatSizeCode, FNRawMatSizeSeq, FTRawMatSizeNameTH, FTRawMatSizeNameEN"
            _Qry &= vbCrLf & ", FTRemark, FTStateActive)"
            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & "," & _FNHSysMatColorId & "  + Row_Number() Over (Order By A.FTSizeCode ) AS FNHSysMatColorId"
            _Qry &= vbCrLf & ",A.FTSizeCode"
            _Qry &= vbCrLf & "," & FNMatColorSeq & "  + Row_Number() Over (Order By A.FTSizeCode ) AS FNMatColorSeq"
            _Qry &= vbCrLf & ",A.FTSizeCode AS FTMatColorNameTH"
            _Qry &= vbCrLf & ",A.FTSizeCode AS FTMatColorNameEN"
            _Qry &= vbCrLf & ",'' AS FTRemark"
            _Qry &= vbCrLf & ",'1' AS FTStateActive"
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_SizeBreakDown AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS MC WITH(NOLOCK)  ON A.FTSizeCode = MC.FTRawMatSizeCode"
            _Qry &= vbCrLf & "WHERE (A.FNHSysStyleDevId =" & _FNHSysStyleDevId & ") AND ISNULL(A.FTSizeCode,'') <> '' AND  (MC.FTRawMatSizeCode IS NULL)"
            _Qry &= vbCrLf & "GROUP BY A.FTSizeCode"
            _Qry &= vbCrLf & "ORDER BY A.FTSizeCode"

            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        Catch ex As Exception
            Return False
        End Try
        Return True

    End Function

    Private Function PostDataToBomSheet() As Boolean
        Dim _Spls As New HI.TL.SplashScreen("Posting Data to BOM Sheet.... ,Please Wait.")

        Dim _dt As DataTable
        Dim _Qry As String = ""
        Dim _FNHSysStyleDevId As Integer = Integer.Parse(Val(FNHSysStyleDevId.Properties.Tag.ToString))
        Dim _FTStyleDevCode As String = ""
        Dim _FTSeason As String = ""
        Dim _FTStyleCode As String = ""
        Dim _FNHSysStyleId As Integer = 0
        Dim _FNHSysSeasonId As Integer = 0
        Dim _MaxFNSeq As Integer = 0
        Dim _MaxFNMerMatSeq As Integer = 0

        Try
            _Qry = "SELECT FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysStyleDevId, FTStyleDevCode, FTStyleDevNameTH, FTStyleDevNameEN, FTSeason, FTNote, FNHSysCustId, FTStatePost, FTPostBy, FTPostDate, FTPostTime"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle AS MS WITH(NOLOCK)  "
            _Qry &= vbCrLf & " WHERE  (FNHSysStyleDevId  =" & Val(_FNHSysStyleDevId) & ")"

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            For Each R As DataRow In _dt.Rows
                _FTStyleDevCode = R!FTStyleDevCode.ToString
                _FTSeason = R!FTSeason.ToString
                _FTStyleCode = R!FTStyleDevCode.ToString '& R!FTSeason.ToString
            Next

            If _FTSeason <> "" Then

                _Qry = " SELECT TOP 1 FNHSysSeasonId "
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS MS WITH(NOLOCK)  "
                _Qry &= vbCrLf & " WHERE  (FTSeasonCode  ='" & HI.UL.ULF.rpQuoted(_FTSeason) & "')"

                _FNHSysSeasonId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")))

                If _FNHSysSeasonId <= 0 Then
                    _FNHSysSeasonId = HI.SE.RunID.GetRunNoID("TMERMSeason", "FNHSysSeasonId", Conn.DB.DataBaseName.DB_MASTER)

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason( "
                    _Qry &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime"
                    _Qry &= vbCrLf & ",FNHSysSeasonId, FTSeasonCode, FTSeasonNameTH, FTSeasonNameEN, FTRemark, FTStateActive)"
                    _Qry &= vbCrLf & "SELECT "
                    _Qry &= vbCrLf & "'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & ", " & _FNHSysSeasonId & ", '" & HI.UL.ULF.rpQuoted(_FTSeason) & "', '" & HI.UL.ULF.rpQuoted(_FTSeason) & "', '" & HI.UL.ULF.rpQuoted(_FTSeason) & "',  '','1' "
                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                End If
            End If

            If _FTStyleCode <> "" Then
                _Qry = " SELECT TOP 1 FNHSysStyleId "
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS MS WITH(NOLOCK)  "
                _Qry &= vbCrLf & " WHERE  (FTStyleCode  ='" & HI.UL.ULF.rpQuoted(_FTStyleCode) & "')"

                _FNHSysStyleId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")))

                If _FNHSysStyleId <= 0 Then
                    _FNHSysStyleId = HI.SE.RunID.GetRunNoID("TMERMStyle", "FNHSysStyleId", Conn.DB.DataBaseName.DB_MASTER)

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle( "
                    _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime"
                    _Qry &= vbCrLf & " ,FNHSysStyleId, FTStyleCode, FTStyleNameTH, FTStyleNameEN, FTRemark, FTStateActive, FNHSysCustId)"
                    _Qry &= vbCrLf & "  SELECT "
                    _Qry &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " , " & _FNHSysStyleId & ", '" & HI.UL.ULF.rpQuoted(_FTStyleCode) & "', FTStyleDevNameTH, FTStyleDevNameEN,  FTNote,'1', FNHSysCustId "
                    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle AS MS WITH(NOLOCK)  "
                    _Qry &= vbCrLf & " WHERE  (FNHSysStyleDevId  =" & Val(_FNHSysStyleDevId) & ")"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle( "
                    _Qry &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime"
                    _Qry &= vbCrLf & ", FNHSysStyleId, FTStyleCode, FTStyleNameTH, FTStyleNameEN, FNHSysCustId, FNHSysSeasonId, FTStateActive)"
                    _Qry &= vbCrLf & "SELECT "
                    _Qry &= vbCrLf & "'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & ", " & _FNHSysStyleId & ", '" & HI.UL.ULF.rpQuoted(_FTStyleCode) & "', FTStyleDevNameTH, FTStyleDevNameEN,FNHSysCustId," & _FNHSysSeasonId & ",'1' "
                    _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle AS MS WITH(NOLOCK)  "
                    _Qry &= vbCrLf & "WHERE  (FNHSysStyleDevId  =" & Val(_FNHSysStyleDevId) & ")"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                Else

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle( "
                    _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime"
                    _Qry &= vbCrLf & " ,  FNHSysStyleId, FTStyleCode, FTStyleNameTH, FTStyleNameEN, FNHSysCustId, FNHSysSeasonId, FTStateActive)"
                    _Qry &= vbCrLf & "  SELECT "
                    _Qry &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " , " & _FNHSysStyleId & ", '" & HI.UL.ULF.rpQuoted(_FTStyleCode) & "', FTStyleDevNameTH, FTStyleDevNameEN,FNHSysCustId,  0,'1' "
                    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle AS MS WITH(NOLOCK)  "
                    _Qry &= vbCrLf & " WHERE  (FNHSysStyleDevId  =" & Val(_FNHSysStyleDevId) & ")"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                End If

                If _FNHSysStyleId > 0 And _FNHSysSeasonId > 0 Then

                    If CheckColorWay() Then
                    End If

                    If CheckRawMatColor() Then
                    End If

                    If CheckSizeBeakDown() Then
                    End If

                    If CheckRawMatSize() Then
                    End If

                    'Dim dtpart As DataTable

                Else

                    _Spls.Close()

                    Return False

                End If
                Dim _dtmat As DataTable

                'Dim _RowData As Double
                Dim _FNSeq As Double = 0
                Dim _DataFNSeq As Double = 0
                Dim _FNMerMatSeq As Double = 0
                Dim _FNHSysSuplId As Integer = 0
                Dim _FNHSysUnitId As Integer = 0
                Dim _FNConSmp As Double = 0
                Dim _FNConSmpPlus As Double = 0
                Dim _FNHSysMerMatId As Integer = 0
                Dim _FTItemNo As String = ""
                Dim _FNPart As Integer = 0

                '_Qry = "    SELECT A.FNHSysStyleDevId,A.FNSeq,A.FNMerMatSeq,S.FNHSysSuplId, U.FNHSysUnitId,A.FNConSmp,A.FNConSmpPlus,A.FTItemNo,A.FNPart "
                '_Qry &= vbCrLf & " ,ISNULL(("
                '_Qry &= vbCrLf & " SELECT TOP 1 FNHSysMainMatId "
                '_Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS XY WITH(NOLOCK)"
                '_Qry &= vbCrLf & " WHERE XY.FTCusItemCodeRef =A.FTItemREfNo"
                '_Qry &= vbCrLf & " ),0) AS FNHSysMerMatId"
                '_Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_Mat AS A WITH(NOLOCK)"
                '_Qry &= vbCrLf & "        LEFT OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U  WITH(NOLOCK)   ON A.FTUnitCode = U.FTUnitCode LEFT OUTER JOIN"
                '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S  WITH(NOLOCK)   ON A.FTSuplCode = S.FTSuplCode"
                '_Qry &= vbCrLf & " WHERE  (A.FNHSysStyleDevId  =" & Val(_FNHSysStyleDevId) & ")  AND ISNULL(A.FTItemREfNo,'') <> ''  "
                '_Qry &= vbCrLf & " AND Isnull(A.FTStateNotShowBomSheet,'0') = '0'"
                '_Qry &= vbCrLf & " ORDER BY A.FNSeq,A.FNMerMatSeq "



                _Qry = "EXEC  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_GETBOM_DEVFORPOST " & Val(_FNHSysStyleDevId) & " "
                _dtmat = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                For Each R As DataRow In _dtmat.Rows

                    _FNSeq = Double.Parse(Val(R!FNSeq.ToString))
                    _FNMerMatSeq = Double.Parse(Val(R!FNMerMatSeq.ToString))
                    _FNHSysSuplId = Integer.Parse(Val(R!FNHSysSuplId.ToString))
                    _FNHSysUnitId = Integer.Parse(Val(R!FNHSysUnitId.ToString))
                    _FNConSmp = Double.Parse(Val(R!FNConSmp.ToString))
                    _FNConSmpPlus = Double.Parse(Val(R!FNConSmpPlus.ToString))
                    _FNHSysMerMatId = Integer.Parse(Val(R!FNHSysMerMatId.ToString))
                    _FTItemNo = R!FTItemNo.ToString
                    _FNPart = Integer.Parse(Val(R!FNPart.ToString))

                    _Qry = " SELECT TOP 1 FNSeq,FNMerMatSeq  "
                    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Mat AS A WITH(NOLOCK)"
                    _Qry &= vbCrLf & " WHERE FNHSysStyleId=" & _FNHSysStyleId & " "
                    _Qry &= vbCrLf & " AND FNHSysMerMatId=" & _FNHSysMerMatId & " "
                    _Qry &= vbCrLf & " AND FNHSysSuplId=" & _FNHSysSuplId & " "
                    _Qry &= vbCrLf & " AND FNHSysUnitId=" & _FNHSysUnitId & " "
                    _Qry &= vbCrLf & " AND FNConSmp=" & _FNConSmp & " "
                    _Qry &= vbCrLf & " AND FNConSmpPlus=" & _FNConSmpPlus & " "
                    _Qry &= vbCrLf & " AND (FNHSysSeasonId=" & _FNHSysSeasonId & "  OR FNHSysSeasonId <=0) "
                    ' _Qry &= vbCrLf & " AND  FTOrderNo='-1' AND  FTSubOrderNo='-1'"

                    If _FNPart > 1 Then
                        _Qry &= vbCrLf & " AND FNPart=" & _FNPart & " "
                    End If

                    _Qry &= vbCrLf & " ORDER BY FNHSysSeasonId DESC "

                    Dim _dtcheckMat As DataTable
                    _dtcheckMat = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                    _MaxFNSeq = -1
                    _MaxFNMerMatSeq = -1

                    For Each Rx As DataRow In _dtcheckMat.Rows
                        _MaxFNSeq = Double.Parse(Val(R!FNSeq.ToString))
                        _MaxFNMerMatSeq = Double.Parse(Val(R!FNMerMatSeq.ToString))

                        _DataFNSeq = _MaxFNSeq
                    Next

                    _dtcheckMat.Dispose()

                    If _MaxFNSeq < 0 Then
                        _Qry = " SELECT Max( FNSeq)  AS FNSeq  "
                        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Mat AS A WITH(NOLOCK)"
                        _Qry &= vbCrLf & " WHERE FNHSysStyleId=" & _FNHSysStyleId & " "
                        '     _Qry &= vbCrLf & " AND FNHSysSeasonId=" & _FNHSysSeasonId & " "
                        _MaxFNSeq = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "0")))

                        '  If _MaxFNSeq >= 0 Then
                        _MaxFNSeq = _MaxFNSeq + 1
                        _DataFNSeq = _MaxFNSeq
                        'Else
                        '    _DataFNSeq = _FNHSysSeasonId + _FNSeq
                        'End If

                        '_Qry = " SELECT Max( FNMerMatSeq)  AS FNMerMatSeq  "
                        '_Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Mat AS A WITH(NOLOCK)"
                        '_Qry &= vbCrLf & " WHERE FNHSysStyleId=" & _FNHSysStyleId & " "
                        '_Qry &= vbCrLf & " AND FNHSysSeasonId=" & _FNHSysSeasonId & " "
                        '_MaxFNMerMatSeq = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "0")))
                        '_MaxFNMerMatSeq = _MaxFNMerMatSeq + 1
                        _MaxFNMerMatSeq = _FNMerMatSeq

                        _Qry = "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Mat"
                        _Qry &= vbCrLf & " ("
                        _Qry &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime"
                        _Qry &= vbCrLf & " , FNHSysStyleId, FNSeq, FNMerMatSeq, FNHSysMerMatId, FNPart, FTPositionPartName"
                        _Qry &= vbCrLf & " , FNHSysSuplId, FTStateNominate, FNHSysUnitId, FNPrice, FNHSysCurId, FNConSmp, FNConSmpPlus"
                        _Qry &= vbCrLf & " , FTOrderNo, FTSubOrderNo, FTStateActive, FTStateCombination"
                        _Qry &= vbCrLf & " , FTStateMainMaterial, FTPositionPartId, FTPart, FTComponent, FTStateFree,FNHSysSeasonId,FNSeqRef "
                        _Qry &= vbCrLf & " )"
                        _Qry &= vbCrLf & "SELECT "
                        _Qry &= vbCrLf & "'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                        _Qry &= vbCrLf & "," & _FNHSysStyleId & " AS  FNHSysStyleDevId"
                        _Qry &= vbCrLf & "," & (_DataFNSeq) & "  "
                        _Qry &= vbCrLf & "," & _MaxFNMerMatSeq & "  "
                        _Qry &= vbCrLf & "," & _FNHSysMerMatId & "  "
                        _Qry &= vbCrLf & ",A.FNPart "
                        _Qry &= vbCrLf & ",A.FTPartNameEN "
                        _Qry &= vbCrLf & ",S.FNHSysSuplId"
                        _Qry &= vbCrLf & ",A.FTStateNominate "
                        _Qry &= vbCrLf & ",U.FNHSysUnitId "
                        _Qry &= vbCrLf & ",A.FNPrice"
                        _Qry &= vbCrLf & ",A.FNHSysCurId "
                        _Qry &= vbCrLf & ",A.FNConSmp"
                        _Qry &= vbCrLf & ",A.FNConSmpPlus "
                        _Qry &= vbCrLf & ",'-1' AS FTOrderNo"
                        _Qry &= vbCrLf & ",'-1'FTSubOrderNo"
                        _Qry &= vbCrLf & ", A.FTStateActive"
                        _Qry &= vbCrLf & ", A.FTStateCombination"
                        _Qry &= vbCrLf & ", A.FTStateMainMaterial"
                        _Qry &= vbCrLf & ", A.FTPositionPartId"
                        _Qry &= vbCrLf & ", ISNULL(A.FTPart,'') AS FTPart"
                        _Qry &= vbCrLf & ", A.FTComponent"
                        _Qry &= vbCrLf & ", A.FTStateFree ," & _FNHSysSeasonId & "," & _FNSeq & ""
                        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_Mat AS A WITH(NOLOCK) "
                        _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U  WITH(NOLOCK)   ON A.FTUnitCode = U.FTUnitCode "
                        _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S  WITH(NOLOCK)   ON A.FTSuplCode = S.FTSuplCode"
                        _Qry &= vbCrLf & " WHERE  (A.FNHSysStyleDevId  =" & Val(_FNHSysStyleDevId) & ")"
                        '_Qry &= vbCrLf & " AND A.FTItemNo='" & HI.UL.ULF.rpQuoted(_FTItemNo) & "'"
                        _Qry &= vbCrLf & " AND  FNSeq =" & _FNSeq & ""
                        _Qry &= vbCrLf & " AND  FNMerMatSeq =" & _FNMerMatSeq & ""


                        If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN) = False Then
                            _Spls.Close()
                            Return False
                        End If

                    End If

                    _Qry = "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_ColorWay"
                    _Qry &= vbCrLf & "("
                    _Qry &= vbCrLf & "   FTInsUser, FDInsDate, FTInsTime,  FNHSysStyleId, FNSeq, FNMerMatSeq"
                    _Qry &= vbCrLf & "   , FNColorWaySeq, FTRunColor, FNHSysRawMatColorId, FNHSysMatColorId "
                    _Qry &= vbCrLf & "   , FTRawMatColorNameTH, FTRawMatColorNameEN"
                    _Qry &= vbCrLf & ") "
                    _Qry &= vbCrLf & "SELECT "
                    _Qry &= vbCrLf & "   '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "   ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "   ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " 	 ," & _FNHSysStyleId & " AS  FNHSysStyleDevId"
                    _Qry &= vbCrLf & "   ," & (_DataFNSeq)
                    _Qry &= vbCrLf & "   ," & _MaxFNMerMatSeq
                    _Qry &= vbCrLf & "   , A.FNColorWaySeq"
                    _Qry &= vbCrLf & "   , A.FTRunColor"
                    _Qry &= vbCrLf & "   , ISNULL(MRC.FNHSysRawMatColorId,0) AS FNHSysRawMatColorId"
                    _Qry &= vbCrLf & "   , MC.FNHSysMatColorId"
                    _Qry &= vbCrLf & "   , A.FTColorNameTH"
                    _Qry &= vbCrLf & "   , A.FTColorNameEN"
                    _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_ColorWay AS A WITH(NOLOCK) "
                    _Qry &= vbCrLf & "INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatColor AS MC WITH(NOLOCK) ON A.FTColorWay = MC.FTMatColorCode "
                    _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS MRC WITH(NOLOCK) ON A.FTColorCode = MRC.FTRawMatColorCode"
                    _Qry &= vbCrLf & " WHERE  (A.FNHSysStyleDevId  =" & Val(_FNHSysStyleDevId) & ")"
                    _Qry &= vbCrLf & " AND  A.FNSeq =" & _FNSeq & ""
                    _Qry &= vbCrLf & " AND  A.FNMerMatSeq =" & _FNMerMatSeq & ""
                    _Qry &= vbCrLf & " AND  ISNULL(MC.FNHSysMatColorId,0) > 0 "
                    _Qry &= vbCrLf & " AND  ISNULL(MC.FNHSysMatColorId,0) NOT IN "
                    _Qry &= vbCrLf & " ("
                    _Qry &= vbCrLf & " SELECT FNHSysMatColorId "
                    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_ColorWay AS AC WITH(NOLOCK)"
                    _Qry &= vbCrLf & " WHERE  (AC.FNHSysStyleId  =" & Val(_FNHSysStyleId) & ")"
                    _Qry &= vbCrLf & " AND  FNSeq =" & (_DataFNSeq) & ""
                    _Qry &= vbCrLf & " AND  FNMerMatSeq =" & _MaxFNMerMatSeq & ""
                    _Qry &= vbCrLf & " )"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                    _Qry = "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_SizeBreakDown"
                    _Qry &= vbCrLf & " ( FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId, FNSeq, FNMerMatSeq"
                    _Qry &= vbCrLf & "  , FNSieBreakDownSeq, FTSizeBreakDown, FTRunSize, FNHSysRawMatSizeId, FNHSysMatSizeId )"
                    _Qry &= vbCrLf
                    _Qry &= vbCrLf & "SELECT "
                    _Qry &= vbCrLf & "' " & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ", " & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ", " & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & ", " & _FNHSysStyleId & " AS  FNHSysStyleDevId"
                    _Qry &= vbCrLf & ", " & (_DataFNSeq)
                    _Qry &= vbCrLf & ", " & _MaxFNMerMatSeq
                    _Qry &= vbCrLf & ", A.FNSieBreakDownSeq"
                    _Qry &= vbCrLf & ", A.FTSizeBreakDown, '1' AS FTRunSize, MRZ.FNHSysRawMatSizeId, MS.FNHSysMatSizeId"
                    _Qry &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_SizeBreakDown AS A  WITH(NOLOCK) "
                    _Qry &= vbCrLf & "INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS MS  WITH(NOLOCK)  ON A.FTSizeBreakDown = MS.FTMatSizeCode "
                    _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS MRZ  WITH(NOLOCK) ON A.FTSizeCode = MRZ.FTRawMatSizeCode"
                    _Qry &= vbCrLf & " WHERE  (A.FNHSysStyleDevId  =" & Val(_FNHSysStyleDevId) & ")"
                    _Qry &= vbCrLf & " AND  A.FNSeq =" & _FNSeq & ""
                    _Qry &= vbCrLf & " AND  A.FNMerMatSeq =" & _FNMerMatSeq & ""
                    _Qry &= vbCrLf & " AND  ISNULL(MS.FNHSysMatSizeId,0) > 0 "
                    _Qry &= vbCrLf & " AND  ISNULL(MS.FNHSysMatSizeId,0) NOT IN "
                    _Qry &= vbCrLf & " ("
                    _Qry &= vbCrLf & " SELECT FNHSysMatSizeId "
                    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_SizeBreakDown AS AC WITH(NOLOCK)"
                    _Qry &= vbCrLf & " WHERE  (AC.FNHSysStyleId  =" & Val(_FNHSysStyleId) & ")"
                    _Qry &= vbCrLf & " AND  FNSeq =" & (_DataFNSeq) & ""
                    _Qry &= vbCrLf & " AND  FNMerMatSeq =" & _MaxFNMerMatSeq & ""
                    _Qry &= vbCrLf & " )"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                Next
                _dtmat.Dispose()
            End If
        Catch ex As Exception
            _Spls.Close()
            Return False
        End Try

        _Spls.Close()
        Return True

    End Function


    Private Function PostDataToNewBomSheet() As Boolean
        Dim _Spls As New HI.TL.SplashScreen("Posting Data to BOM Sheet.... ,Please Wait.")

        Dim _dt As DataTable
        Dim _Qry As String = ""
        Dim _FNHSysStyleDevId As Integer = Integer.Parse(Val(FNHSysStyleDevId.Properties.Tag.ToString))
        Dim _FTStyleDevCode As String = ""
        Dim _FTSeason As String = ""
        Dim _FTStyleCode As String = ""
        Dim _FNHSysStyleId As Integer = 0
        Dim _FNHSysSeasonId As Integer = 0
        Dim _MaxFNSeq As Integer = 0
        Dim _MaxFNMerMatSeq As Integer = 0

        Try
            _Qry = "SELECT FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysStyleDevId, FTStyleDevCode, FTStyleDevNameTH, FTStyleDevNameEN, FTSeason, FTNote, FNHSysCustId, FTStatePost, FTPostBy, FTPostDate, FTPostTime"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle AS MS WITH(NOLOCK)  "
            _Qry &= vbCrLf & " WHERE  (FNHSysStyleDevId  =" & Val(_FNHSysStyleDevId) & ")"

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            For Each R As DataRow In _dt.Rows
                _FTStyleDevCode = R!FTStyleDevCode.ToString
                _FTSeason = R!FTSeason.ToString
                _FTStyleCode = R!FTStyleDevCode.ToString '& R!FTSeason.ToString
            Next

            If _FTSeason <> "" Then

                _Qry = " SELECT TOP 1 FNHSysSeasonId "
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS MS WITH(NOLOCK)  "
                _Qry &= vbCrLf & " WHERE  (FTSeasonCode  ='" & HI.UL.ULF.rpQuoted(_FTSeason) & "')"

                _FNHSysSeasonId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")))

                If _FNHSysSeasonId <= 0 Then
                    _FNHSysSeasonId = HI.SE.RunID.GetRunNoID("TMERMSeason", "FNHSysSeasonId", Conn.DB.DataBaseName.DB_MASTER)

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason( "
                    _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime"
                    _Qry &= vbCrLf & " ,FNHSysSeasonId, FTSeasonCode, FTSeasonNameTH, FTSeasonNameEN, FTRemark, FTStateActive)"
                    _Qry &= vbCrLf & "  SELECT "
                    _Qry &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " , " & _FNHSysSeasonId & ", '" & HI.UL.ULF.rpQuoted(_FTSeason) & "', '" & HI.UL.ULF.rpQuoted(_FTSeason) & "', '" & HI.UL.ULF.rpQuoted(_FTSeason) & "',  '','1' "

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                End If

            End If

            If _FTStyleCode <> "" Then
                _Qry = " SELECT TOP 1 FNHSysStyleId "
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS MS WITH(NOLOCK)  "
                _Qry &= vbCrLf & " WHERE  (FTStyleCode  ='" & HI.UL.ULF.rpQuoted(_FTStyleCode) & "')"

                _FNHSysStyleId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")))

                If _FNHSysStyleId <= 0 Then
                    _FNHSysStyleId = HI.SE.RunID.GetRunNoID("TMERMStyle", "FNHSysStyleId", Conn.DB.DataBaseName.DB_MASTER)

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle( "
                    _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime"
                    _Qry &= vbCrLf & " ,FNHSysStyleId, FTStyleCode, FTStyleNameTH, FTStyleNameEN, FTRemark, FTStateActive, FNHSysCustId)"
                    _Qry &= vbCrLf & "  SELECT "
                    _Qry &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " , " & _FNHSysStyleId & ", '" & HI.UL.ULF.rpQuoted(_FTStyleCode) & "', FTStyleDevNameTH, FTStyleDevNameEN,  FTNote,'1', FNHSysCustId "
                    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle AS MS WITH(NOLOCK)  "
                    _Qry &= vbCrLf & " WHERE  (FNHSysStyleDevId  =" & Val(_FNHSysStyleDevId) & ")"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle( "
                    _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime"
                    _Qry &= vbCrLf & " ,  FNHSysStyleId, FTStyleCode, FTStyleNameTH, FTStyleNameEN, FNHSysCustId, FNHSysSeasonId, FTStateActive)"
                    _Qry &= vbCrLf & "  SELECT "
                    _Qry &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " , " & _FNHSysStyleId & ", '" & HI.UL.ULF.rpQuoted(_FTStyleCode) & "', FTStyleDevNameTH, FTStyleDevNameEN,FNHSysCustId," & _FNHSysSeasonId & ",'1' "
                    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle AS MS WITH(NOLOCK)  "
                    _Qry &= vbCrLf & " WHERE  (FNHSysStyleDevId  =" & Val(_FNHSysStyleDevId) & ")"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                Else

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle( "
                    _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime"
                    _Qry &= vbCrLf & " ,  FNHSysStyleId, FTStyleCode, FTStyleNameTH, FTStyleNameEN, FNHSysCustId, FNHSysSeasonId, FTStateActive)"
                    _Qry &= vbCrLf & "  SELECT "
                    _Qry &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " , " & _FNHSysStyleId & ", '" & HI.UL.ULF.rpQuoted(_FTStyleCode) & "', FTStyleDevNameTH, FTStyleDevNameEN,FNHSysCustId,  0,'1' "
                    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle AS MS WITH(NOLOCK)  "
                    _Qry &= vbCrLf & " WHERE  (FNHSysStyleDevId  =" & Val(_FNHSysStyleDevId) & ")"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                End If

                If _FNHSysStyleId > 0 And _FNHSysSeasonId > 0 Then
                    If CheckColorWay() Then
                    End If
                    If CheckRawMatColor() Then
                    End If
                    If CheckSizeBeakDown() Then
                    End If
                    If CheckRawMatSize() Then
                    End If
                    'Dim dtpart As DataTable

                Else
                    _Spls.Close()
                    Return False

                End If
                Dim _dtmat As DataTable
                Dim BomID As Integer = 0
                BomID = HI.SE.RunID.GetRunNoID("TMERTBOM", "FNHSysBomId", Conn.DB.DataBaseName.DB_MERCHAN)
                _Qry = "EXEC  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_GETBOMGARMENT_FROMBOMDEVPOST '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Val(_FNHSysStyleDevId) & "," & Val(_FNHSysStyleId) & "," & Val(_FNHSysSeasonId) & "," & Val(FNBomDevType.SelectedIndex) & "," & Val(BomID) & " "
                _dtmat = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
                _dtmat.Dispose()
            End If
        Catch ex As Exception
            _Spls.Close()
            Return False
        End Try

        _Spls.Close()
        Return True

    End Function

    Private Function CheckPart(PartEN As String, PartTH As String) As String
        Dim _Qry As String = ""
        Dim PartID As String = ""
        Dim PartCode As String = ""
        Dim PartCodeNew As String = ""

        _Qry = "SELECT TOP 1 FNHSysPartId "
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTPartNameEN='" & HI.UL.ULF.rpQuoted(PartEN) & "'"

        PartID = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")

        If PartID = "" Then
            PartID = (HI.SE.RunID.GetRunNoID("TMERMPart", "FNHSysPartId", Conn.DB.DataBaseName.DB_MASTER)).ToString
            If PartID <> "" Then
                PartCode = "P" & Me.FNHSysStyleDevId.Text

                _Qry = " SELECT TOP 1 RIGHT(FTPartCode,4) AS FTPartCode "
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart WITH(NOLOCK)"
                _Qry &= vbCrLf & " WHERE LEFT(FTPartCode,Len('" & HI.UL.ULF.rpQuoted(PartCode) & "'))='" & HI.UL.ULF.rpQuoted(PartCode) & "'"
                _Qry &= vbCrLf & "  AND LEN(FTPartCode) = Len('" & HI.UL.ULF.rpQuoted(PartCode) & "' + '0000')"
                _Qry &= vbCrLf & "  ORDER BY RIGHT(FTPartCode,4) DESC"
                PartCodeNew = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0000")
                PartCodeNew = (Integer.Parse(Val(PartCodeNew)) + 1).ToString("0000")
                PartCode = PartCode & PartCodeNew

                _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart"
                _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime,  FNHSysPartId, FTPartCode,FTPartNameEN,FTPartNameTH, FTRemark, FTStateActive)"
                _Qry &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & "," & Integer.Parse(Val(PartID)) & ""
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(PartCode) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(PartEN) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(PartTH) & "'"
                _Qry &= vbCrLf & ",''"
                _Qry &= vbCrLf & ",'1'"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MASTER)

            End If

        End If

        Return PartID

    End Function

    Private Sub LoadStylePostInfo()
        Dim _FNHSysStyleDevId As Integer = Integer.Parse(Val(FNHSysStyleDevId.Properties.Tag.ToString))
        FTStatePost.Checked = False
        FTPostBy.Text = ""
        FTPostDate.Text = ""
        FTPostTime.Text = ""

        Dim _dt As DataTable
        Dim _Str As String = ""
        _Str = "SELECT TOP 1  FTStatePost,FTPostBy, FTPostDate, FTPostTime"
        _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle AS MS WITH(NOLOCK)  "
        _Str &= vbCrLf & "    WHERE MS.FNHSysStyleDevId = '" & _FNHSysStyleDevId & "' AND FTSeason='" & HI.UL.ULF.rpQuoted(FTSeason.Text) & "'   AND ISNULL(FNVersion,0)=" & FNVersion.Text & " AND FNBomDevType =" & FNBomDevType.SelectedIndex & "  "

        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

        If _dt.Rows.Count > 0 Then
            For Each R As DataRow In _dt.Rows

                FTStatePost.Checked = (R!FTStatePost.ToString = "1")
                FTPostBy.Text = R!FTPostBy.ToString
                FTPostDate.Text = R!FTPostDate.ToString
                FTPostTime.Text = R!FTPostTime.ToString

            Next
        End If

    End Sub

    Private Sub LoadStyleInfo(ByVal _FNHSysStyleDevId As String, Optional _DefaultTab As Boolean = False)

        Call ClearMergeCtrlData()

        FTNikeDeveloperName.Text = ""
        Dim _dt As DataTable
        Dim _Str As String = ""
        _Str = "SELECT MS.FNHSysStyleDevId, MS.FTStyleDevCode, MS.FTStyleDevNameTH, MS.FTStyleDevNameEN "
        _Str &= vbCrLf & ", MS.FNHSysCustId,  MS.FTUpdUser, CONVERT(VARCHAR(10)"
        _Str &= vbCrLf & ", CONVERT(DATETIME, MS.FDUpdDate, 120), 103) AS FDUpdDate, MS.FTUpdTime,msc.FTMSCCode "
        _Str &= vbCrLf & ", T8.FTCustCode, T8.FTCustNameEN, MS.FTDevelopDate, MS.FTNote, MS.FTNikeDeveloperName "
        _Str &= vbCrLf & ", MS.FNVersion, MS.FTMSCLevel1, MS.FTMSCLevel2, MS.FTMSCLevel3, MS.FTSilhouette, MS.FNBomDevType"
        _Str &= vbCrLf & ", CONVERT(VARCHAR(10), CONVERT(DATETIME, MS.FDInsDate, 120), 103) AS FTUploadDate"
        _Str &= vbCrLf & ", MS.FTInsUser AS FTOwner, FTVenderPramCode, MS.FTSeason, m.FTMerTeamCode "
        _Str &= vbCrLf & ", MS.FTStatePost, MS.FTPostBy, MS.FTPostDate, MS.FTPostTime "
        _Str &= vbCrLf
        _Str &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle AS MS WITH(NOLOCK)  "
        _Str &= vbCrLf
        _Str &= vbCrLf & "LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer T8 WITH(NOLOCK) ON MS.FNHSysCustId = T8.FNHSysCustId"
        _Str &= vbCrLf
        _Str &= vbCrLf & "OUTER APPLY (SELECT TOP 1 u.FNHSysMerTeamId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS u WHERE u.FTUserName = MS.FTInsUser) AS u "
        _Str &= vbCrLf
        _Str &= vbCrLf & "OUTER APPLY (SELECT TOP 1 m.FTMerTeamCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMerTeam AS m WHERE m.FNHSysMerTeamId = u.FNHSysMerTeamId) AS m "
        _Str &= vbCrLf
        _Str &= vbCrLf & "OUTER APPLY (SELECT TOP 1 v.FTVenderPramCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMVenderPram AS v WHERE v.FNHSysVenderPramId = MS.FNHSysVenderPramId) AS v "
        _Str &= vbCrLf
        _Str &= vbCrLf & "OUTER APPLY (select top 1 FTMSCCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMSC X where X.FNHSysMSCId = MS.FNHSysMSCId ) As msc "
        _Str &= vbCrLf & "WHERE(MS.FNHSysStyleDevId  =" & Val(_FNHSysStyleDevId) & ")"
        _Str &= vbCrLf
        _Str &= vbCrLf & "ORDER BY MS.FNHSysStyleDevId"
        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

        If _dt.Rows.Count > 0 Then
            For Each R As DataRow In _dt.Rows

                'FNHSysStyleDevId_None.Text = R!FTStyleNameTH.ToString
                FTStyleDevNameTH.Text = R!FTStyleDevNameTH.ToString
                FTStyleDevNameEN.Text = R!FTStyleDevNameEN.ToString
                FNHSysCustId.Text = R!FTCustCode.ToString
                FNHSysCustId_None.Text = R!FTCustNameEN.ToString
                FTUpdUser.Text = R!FTUpdUser.ToString
                FDUpdDate.Text = R!FDUpdDate.ToString
                FTUpdTime.Text = R!FTUpdTime.ToString
                FNHSysMSCId.Text = R!FTMSCCode.ToString
                FNVersion.Text = R!FNVersion.ToString
                FTUploadDate.Text = R!FTUploadDate.ToString
                FTOwner.Text = R!FTOwner.ToString
                FTFty.Text = R!FTVenderPramCode.ToString
                FNHSysVenderPramId.Text = R!FTVenderPramCode.ToString
                FTSeason.Text = R!FTSeason.ToString
                FNVersion.Text = R!FNVersion.ToString

                Try
                    FTDevelopDate.DateTime = R!FTDevelopDate.ToString
                    FTDevelopDate.Text = HI.UL.ULDate.ConvertEN(R!FTDevelopDate.ToString)
                Catch ex As Exception
                    FTDevelopDate.Text = ""
                End Try

                FTNote.Text = R!FTNote.ToString
                FNBomDevType.SelectedIndex = Val(R!FNBomDevType.ToString)
                FTMSCLevel1.Text = R!FTMSCLevel1.ToString
                FTMSCLevel2.Text = R!FTMSCLevel2.ToString
                FTMSCLevel3.Text = R!FTMSCLevel3.ToString
                FTSilhouette.Text = R!FTSilhouette.ToString
                FTNikeDeveloperName.Text = R!FTNikeDeveloperName.ToString
            Next

        Else

            _Str = "SELECT TOP 1 MS.FNHSysStyleDevId, MS.FTStyleDevCode, MS.FTStyleDevNameTH, MS.FTStyleDevNameEN "
            _Str &= vbCrLf & ", MS.FNHSysCustId,  MS.FTUpdUser, CONVERT(VARCHAR(10) "
            _Str &= vbCrLf & ", CONVERT(DATETIME, MS.FDUpdDate, 120), 103) AS FDUpdDate, MS.FTUpdTime, msc.FTMSCCode"
            _Str &= vbCrLf & ", T8.FTCustCode, T8.FTCustNameEN,MS.FTNote,MS.FTNikeDeveloperName,MS.FNVersion,MS.FTMSCLevel1,MS.FTMSCLevel2,MS.FTMSCLevel3,MS.FTSilhouette,MS.FNBomDevType"
            _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle AS MS WITH(NOLOCK)  "
            _Str &= vbCrLf
            _Str &= vbCrLf & " LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer T8 WITH(NOLOCK) ON MS.FNHSysCustId = T8.FNHSysCustId"
            _Str &= vbCrLf
            _Str &= vbCrLf & " outer apply (select top 1  FTMSCCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMSC X where X.FNHSysMSCId = MS.FNHSysMSCId ) As msc "
            _Str &= vbCrLf & " WHERE( MS.FTStyleDevCode  ='" & HI.UL.ULF.rpQuoted(FNHSysStyleDevId.Text.Trim()) & "')"
            _Str &= vbCrLf
            _Str &= vbCrLf & " ORDER BY MS.FNHSysStyleDevId"

            _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

            If _dt.Rows.Count > 0 Then

                For Each R As DataRow In _dt.Rows

                    'FNHSysStyleDevId_None.Text = R!FTStyleNameTH.ToString
                    FTStyleDevNameTH.Text = R!FTStyleDevNameTH.ToString
                    FTStyleDevNameEN.Text = R!FTStyleDevNameEN.ToString
                    FNHSysCustId.Text = R!FTCustCode.ToString
                    FNHSysCustId_None.Text = R!FTCustNameEN.ToString
                    FNHSysMSCId.Text = R!FTMSCCode.ToString
                    FNVersion.Text = 1 'Val(R!FNVersion.ToString)
                    FTUpdUser.Text = ""
                    FDUpdDate.Text = ""
                    FTUpdTime.Text = ""
                    FTDevelopDate.Text = ""
                    FTNote.Text = R!FTNote.ToString
                    FTNikeDeveloperName.Text = R!FTNikeDeveloperName.ToString
                    'FNBomDevType.SelectedIndex = Val(R!FNBomDevType.ToString)
                    FTMSCLevel1.Text = R!FTMSCLevel1.ToString
                    FTMSCLevel2.Text = R!FTMSCLevel2.ToString
                    FTMSCLevel3.Text = R!FTMSCLevel3.ToString
                    FTSilhouette.Text = R!FTSilhouette.ToString
                Next
            Else
                'FNHSysStyleDevId_None.Text = ""
                FTStyleDevNameTH.Text = ""
                FTStyleDevNameEN.Text = ""
                FNHSysCustId.Text = ""
                FNHSysCustId_None.Text = ""
                FNHSysMSCId.Text = ""
                FTUpdUser.Text = ""
                FDUpdDate.Text = ""
                FTUpdTime.Text = ""
                FTDevelopDate.Text = ""
                FTNote.Text = ""
                FTNikeDeveloperName.Text = ""
                FNVersion.Text = 1
                FTMSCLevel1.Text = ""
                FTMSCLevel2.Text = ""
                FTMSCLevel3.Text = ""
                FTSilhouette.Text = ""

            End If
        End If

        'Me.otb.SelectedTabPageIndex = 0
        If (_FNHSysStyleDevId <> "") Then
            Call LoadStyleDetail(_FNHSysStyleDevId)
            Call LoadColorwaySize(_FNHSysStyleDevId)
        End If

        If (_DefaultTab) Then
            Me.otb.SelectedTabPageIndex = 0
        End If

        Call LoadStylePostInfo()
    End Sub

    Private Sub LoadStyleDetail(ByVal FNHSysStyleDevId As String)
        'If FNHSysStyleDevId = "" Then Return
        Dim _Str As String = ""

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str = "SELECT T2.FNHSysStyleDevId, T2.FNSeq, T2.FNMerMatSeq, T2.FTItemNo, T2.FTItemDesc AS FNHSysMerMatId_None,  T2.FTUnitCode , T2.FTStateNominate,  T2.FTSuplCode, T2.FNHSysCurId, "
            _Str &= vbCrLf & " T6.FTCurCode, T2.FNPrice, T2.FNPart, T2.FTPartNameEN, T2.FTPartNameTH "
        Else
            _Str = "SELECT T2.FNHSysStyleDevId, T2.FNSeq, T2.FNMerMatSeq, T2.FTItemNo, T2.FTItemDesc AS FNHSysMerMatId_None,  T2.FTUnitCode , T2.FTStateNominate,  T2.FTSuplCode, T2.FNHSysCurId, "
            _Str &= vbCrLf & " T6.FTCurCode, T2.FNPrice, T2.FNPart, T2.FTPartNameEN, T2.FTPartNameTH "
        End If

        _Str &= vbCrLf & ", T2.FNConSmp, T2.FNConSmpPlus, T2.FNHSysCurId AS   FNHSysCurId_Hide "
        _Str &= vbCrLf & ", T2.FTStateCombination  AS FTStateCombination "
        _Str &= vbCrLf & ", T2.FTStateActive, T2.FTStateMainMaterial"
        _Str &= vbCrLf & ", T2.FTComponent , T2.FTPositionPartId "
        _Str &= vbCrLf & ", ISNULL(T2.FTStateFree,'0') AS FTStateFree,ISNULL(T2.FTPart,'') AS FTPart"
        _Str &= vbCrLf & ", ISNULL(T2.FTStateNotShowBomSheet,'0') AS FTStateNotShowBomSheet,ISNULL(T2.FTStateLabel,'0') AS FTStateLabel,ISNULL(T2.FTUsed,'') As FTUsed ,ISNULL(T2.FNOrderSetType,0) As FNOrderSetType,T2.FTItemREfNo"
        _Str &= vbCrLf & ", ISNULL(T2.FTStateDTM,'0') AS FTStateDTM, T2.FTDTMNote, ISNULL( T2.FTStateHemNotOptiplan,'0') AS FTStateHemNotOptiplan, ISNULL(T2.FNRepeatLengthCM,0) AS FNRepeatLengthCM "
        _Str &= vbCrLf & ", ISNULL(T2.FNRepeatConvert,0) AS FNRepeatConvert, ISNULL(T2.FNPackPerCarton,0) AS FNPackPerCarton, ISNULL(T2.FNConSmpSplit,0) AS FNConSmpSplit, T2.FTBOMExcelSuplName "
        _Str &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTDevelopStyle] AS T1 WITH(NOLOCK) "
        _Str &= vbCrLf & "INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTDevelopStyle_Mat] AS T2 WITH(NOLOCK) ON T1.FNHSysStyleDevId = T2.FNHSysStyleDevId "
        _Str &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS T6 WITH(NOLOCK) ON T2.FNHSysCurId = T6.FNHSysCurId "
        _Str &= vbCrLf & "WHERE (T1.FNHSysStyleDevId =" & Val(FNHSysStyleDevId) & ")"
        _Str &= vbCrLf & "ORDER BY T1.FNHSysStyleDevId, T2.FNMerMatSeq, T2.FNPart  "

        dtStyleDetail = New DataTable()
        dtStyleDetail = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

        Me.ogcmat.DataSource = dtStyleDetail
        Me.ogcmat.Refresh()

        Dim View As GridView = Me.ogcmat.Views(0)
        InitialGridMergCell()
        View.BestFitColumns()
        ogvmat.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 300
        ogvcolor.Columns.ColumnByFieldName("FTMainMatName").Width = 200
        ogvsize.Columns.ColumnByFieldName("FTMainMatName").Width = 200
        ogvmat.Columns.ColumnByFieldName("FTPartNameEN").Width = 150
        ogvmat.Columns.ColumnByFieldName("FTPartNameTH").Width = 150
        ogvcolor.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
        ogvsize.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
        ogvmat.Columns.ColumnByFieldName("FTComponent").Width = 100
        ogvmat.Columns.ColumnByFieldName("FTItemREfNo").Width = 150
        ogvmat.Columns.ColumnByFieldName("FTUsed").Width = 300

    End Sub

    Private Sub LoadColorwaySize(ByVal FNHSysStyleDevId As String)
        Try
            'Dim StrSql As String = ""
            Call ImportColor()
            Call ImportSize()

        Catch ex As Exception
        End Try
    End Sub

    Private Function InitialGrid(ByVal InitGrid As GridView) As GridView
        Dim xCol As Integer = 0
        Dim Idx As Integer = 0
        Try
            xCol = InitGrid.Columns.Count - 1
            For Idx = xCol To 0 Step -1

                Select Case InitGrid.Columns(Idx).FieldName.ToString.ToLower
                    Case "FNHSysStyleDevId".ToLower, "FNSeq".ToLower, "FNMerMatSeq".ToLower, "FTItemNo".ToLower _
                        , "FTMainMatName".ToLower, "FNPart".ToLower, "FTPositionPartName".ToLower, "FNConSmp".ToLower _
                        , "FNConSmpPlus".ToLower, "FNColorWaySeq".ToLower, "FTRunColor".ToLower, "FTItemREfNo".ToLower
                    Case Else
                        Dim Col As GridColumn = InitGrid.Columns(Idx)
                        View.Columns(Col.Name).Dispose()
                End Select
            Next
        Catch ex As Exception
        End Try

        Return InitGrid
    End Function

    Private Sub ocmbomaddnew_Click(sender As System.Object, e As System.EventArgs) Handles ocmbomaddnew.Click
        If CheckOwner() = False Then
            Exit Sub
        End If
        If sFNHSysStyleDevId = "" Then Return
        Call LoadStylePostInfo()
        If CheckPostDataToBomSheet() = False Then Exit Sub

        If ogcmat.DataSource Is Nothing Then
            Call LoadStyleDetail(-99)
            Call LoadColorwaySize(-99)
        End If
        CType(ogcmat.DataSource, DataTable).AcceptChanges()
        InitNewRow(CType(ogcmat.DataSource, DataTable), TabIndexs.StyleDetail)
    End Sub

    Private Sub ocmbominsertrow_Click(sender As System.Object, e As System.EventArgs) Handles ocmbominsertrow.Click
        If CheckOwner() = False Then
            Exit Sub
        End If
        Dim crRow As Double, nxRow As String, nwRow As String
        Dim RowCount As Integer = 0
        View = Me.ogcmat.Views(0)
        If sFNHSysStyleDevId = "" Then Return
        Call LoadStylePostInfo()
        If CheckPostDataToBomSheet() = False Then Exit Sub
        If (Not IsDBNull(View)) Then
            RowsIndex = View.FocusedRowHandle
            TopVisibleIndex = View.TopRowIndex
            RowCount = View.RowCount
        End If

        crRow = Val(View.GetRowCellValue(RowsIndex, "FNMerMatSeq").ToString)
        nxRow = Val(View.GetRowCellValue(RowsIndex + 1, "FNMerMatSeq").ToString)

        nwRow = Str(crRow + 0.01).Trim

        If nxRow = nwRow Then
            Return
        End If

        Dim MaxSeq As Double = 1.0
        Dim LastSeq As Double = 0
        CType(ogcmat.DataSource, DataTable).AcceptChanges()
        Dim dt As DataTable = CType(ogcmat.DataSource, DataTable)

        For Each r As DataRow In dt.Rows
            LastSeq = r.Item("FNSeq")
            If LastSeq >= MaxSeq Then
                MaxSeq = LastSeq + 1
            End If
        Next

        If LastSeq = MaxSeq Then MaxSeq = LastSeq + 1

        Dim dr As DataRow = dt.NewRow()
        dr.Item("FNHSysStyleDevId") = Val(FNHSysStyleDevId.Properties.Tag.ToString)
        dr.Item("FNSeq") = MaxSeq
        dr.Item("FNMerMatSeq") = nwRow
        dr.Item("FNPart") = "1"
        dr.Item("FTStateNominate") = "1"
        dr.Item("FTStateCombination") = "0"
        dr.Item("FTStateFree") = "0"
        dr.Item("FTStateActive") = "1"
        dr.Item("FTStateMainMaterial") = "0"

        dt.Rows.InsertAt(dr, RowsIndex + 1)

        'Call DoGrouping()

    End Sub

    Private Sub ocmbomnewcolorway_Click(sender As System.Object, e As System.EventArgs) Handles ocmbomnewcolorway.Click

        If CheckOwner() = False Then
            Exit Sub
        End If

        If sFNHSysStyleDevId = "" Then Return
        'InitNewRow(CType(ogcstylecolor.DataSource, DataTable), TabIndexs.Colorway)
        Call LoadStylePostInfo()
        If CheckPostDataToBomSheet() = False Then Exit Sub
        HI.ST.Lang.SP_SETxLanguage(_wNewColorway)
        With _wNewColorway
            .FTColorway.Text = ""
            .ProcNew = False
            .ShowDialog()

            If .ProcNew Then

                Dim _NewColorWay As String = .FTColorway.Text
                Dim _NewColorID As String = .FTColorway.Properties.Tag.ToString
                Dim _Qry As String = ""

                _Qry = "SELECT  FTColorWay "
                _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_ColorWay AS A WITH (NOLOCK)"
                _Qry &= vbCrLf & "WHERE FNHSysStyleDevId=" & Integer.Parse(Val(FNHSysStyleDevId.Properties.Tag.ToString)) & ""
                _Qry &= vbCrLf & "And  (FTColorWay = N'" & HI.UL.ULF.rpQuoted(_NewColorWay) & "')"
                If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "") <> "" Then
                    Exit Sub
                End If

                _Qry = "SELECT Max( FNColorWaySeq) AS FNColorWaySeq"
                _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_ColorWay AS A WITH(NOLOCK)"
                _Qry &= vbCrLf & "WHERE FNHSysStyleDevId=" & Integer.Parse(Val(FNHSysStyleDevId.Properties.Tag.ToString)) & ""

                _NewColorID = (Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "0"))) + 1).ToString
                '_Qry = " SELECT TOP 1 FTMatColorNameEN FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TMERMMatColor] AS C WITH(NOLOCK) WHERE FNHSysMatColorId =" & Integer.Parse(Val(_NewColorID)) & " "
                '_NewColorWay = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "")

                Dim InitDt As DataTable = CType(Me.ogccolor.DataSource, DataTable)
                Dim dc As DataColumn
                Dim dc1 As DataColumn
                Dim dc3 As DataColumn
                Dim dc4 As DataColumn

                If Me.ogvcolor.Columns.ColumnByFieldName("FTRawMatColorCode" & _NewColorWay) Is Nothing Then

                    dc = New DataColumn("FTRawMatColorCode" & _NewColorWay, System.Type.GetType("System.String"))
                    dc1 = New DataColumn("FNHSysRawMatColorId" & "FTRawMatColorCode" & _NewColorWay, System.Type.GetType("System.String"))
                    dc3 = New DataColumn("FTRawMatColorNameTH" & _NewColorWay, System.Type.GetType("System.String"))
                    dc4 = New DataColumn("FTRawMatColorNameEN" & _NewColorWay, System.Type.GetType("System.String"))

                    dc.Caption = _NewColorWay
                    dc1.Caption = "FNHSysRawMatColorId"

                    If ogvcolor.Columns(dc.ColumnName) Is Nothing Then
                        Try
                            ogvcolor.Columns.Item(dc.ColumnName).FieldName = dc.ColumnName
                            ogvcolor.Columns.Item(dc1.ColumnName).FieldName = dc1.ColumnName
                            ogvcolor.Columns.Item(dc3.ColumnName).FieldName = dc3.ColumnName
                            ogvcolor.Columns.Item(dc4.ColumnName).FieldName = dc4.ColumnName
                            InitDt.Columns.Add(dc.ColumnName)
                            InitDt.Columns.Add(dc1.ColumnName)
                            InitDt.Columns.Add(dc3.ColumnName)
                            InitDt.Columns.Add(dc4.ColumnName)
                        Catch ex As Exception
                            ogvcolor.Columns.AddField(dc.ColumnName)
                            ogvcolor.Columns(dc.ColumnName).FieldName = dc.ColumnName
                            ogvcolor.Columns(dc.ColumnName).Name = dc.ColumnName
                            ogvcolor.Columns(dc.ColumnName).Caption = dc.Caption
                            ogvcolor.Columns(dc.ColumnName).Visible = True
                            ogvcolor.Columns(dc.ColumnName).Width = 70
                            ogvcolor.Columns(dc.ColumnName).OptionsColumn.AllowShowHide = False
                            ogvcolor.Columns(dc.ColumnName).OptionsColumn.ShowInCustomizationForm = False

                            ogvcolor.Columns.AddField(dc1.ColumnName)
                            ogvcolor.Columns(dc1.ColumnName).FieldName = dc1.ColumnName
                            ogvcolor.Columns(dc1.ColumnName).Name = dc1.ColumnName
                            ogvcolor.Columns(dc1.ColumnName).Caption = dc1.Caption
                            ogvcolor.Columns(dc1.ColumnName).Tag = _NewColorID
                            ogvcolor.Columns(dc1.ColumnName).Visible = False
                            ogvcolor.Columns(dc1.ColumnName).OptionsColumn.AllowShowHide = False
                            ogvcolor.Columns(dc1.ColumnName).OptionsColumn.ShowInCustomizationForm = False

                            ogvcolor.Columns.AddField(dc3.ColumnName)
                            ogvcolor.Columns(dc3.ColumnName).FieldName = dc3.ColumnName
                            ogvcolor.Columns(dc3.ColumnName).Name = dc3.ColumnName
                            ogvcolor.Columns(dc3.ColumnName).Caption = dc3.Caption
                            ogvcolor.Columns(dc3.ColumnName).Tag = _NewColorID
                            ogvcolor.Columns(dc3.ColumnName).Visible = False
                            ogvcolor.Columns(dc3.ColumnName).OptionsColumn.AllowShowHide = False
                            ogvcolor.Columns(dc3.ColumnName).OptionsColumn.ShowInCustomizationForm = False

                            ogvcolor.Columns.AddField(dc4.ColumnName)
                            ogvcolor.Columns(dc4.ColumnName).FieldName = dc4.ColumnName
                            ogvcolor.Columns(dc4.ColumnName).Name = dc4.ColumnName
                            ogvcolor.Columns(dc4.ColumnName).Caption = dc4.Caption
                            ogvcolor.Columns(dc4.ColumnName).Tag = _NewColorID
                            ogvcolor.Columns(dc4.ColumnName).Visible = False
                            ogvcolor.Columns(dc4.ColumnName).OptionsColumn.AllowShowHide = False
                            ogvcolor.Columns(dc4.ColumnName).OptionsColumn.ShowInCustomizationForm = False

                            Dim repos As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
                            repos.Buttons.Item(0).Tag = "169"
                            ogvcolor.Columns(dc.ColumnName).ColumnEdit = repos
                            ogvcolor.Columns(dc1.ColumnName).ColumnEdit = RepRawMatIDCaledit

                            With repos
                                .CharacterCasing = CharacterCasing.Upper
                                AddHandler .Click, AddressOf DynamicResponButtoneColor_Gotocus
                                AddHandler .ButtonClick, AddressOf DynamicResponButtoneSysHideColor_ButtonClick
                                AddHandler .KeyDown, AddressOf DynamicButtoneditColor_KeyDown
                            End With

                            InitDt.Columns.Add(dc.ColumnName)
                            InitDt.Columns.Add(dc1.ColumnName)
                            InitDt.Columns.Add(dc3.ColumnName)
                            InitDt.Columns.Add(dc4.ColumnName)
                            'dtStyleDetail.Columns.Add(dc)
                            'dtStyleDetail.Columns(dc.ColumnName) = dtStyleDetail.Columns(FTColorWay)
                        End Try
                    End If

                    CType(Me.ogccolor.DataSource, DataTable).AcceptChanges()

                End If
            End If

        End With

    End Sub

    Private Sub GridView1_CellValueChanging(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles ogvmat.CellValueChanging
        Dim vw2 As GridView = New GridView()
        Dim vw3 As GridView = New GridView()

        vw2 = ogccolor.Views(0)
        vw2.SetRowCellValue(e.RowHandle, e.Column.FieldName, e.Value)
        ogccolor = vw2.GridControl
        ogccolor.Refresh()

        vw3 = ogcsize.Views(0)
        vw3.SetRowCellValue(e.RowHandle, e.Column.FieldName, e.Value)
        ogcsize = vw3.GridControl
        ogcsize.Refresh()

    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvmat.KeyDown
        Dim vw1 As GridView = New GridView()
        Dim SelectedRow As Integer, FocusedColumn As Integer
        vw1 = ogcmat.Views(0)
        If (Not IsDBNull(vw1)) Then
            SelectedRow = vw1.FocusedRowHandle
            FocusedColumn = vw1.FocusedColumn.AbsoluteIndex
        End If

        Select Case e.KeyCode
            Case Keys.Return, Keys.Tab
                If FocusedColumn = ogvmat.Columns.Count - 1 Then
                    If SelectedRow = vw1.RowCount - 1 Then
                        CType(ogcmat.DataSource, DataTable).AcceptChanges()
                        InitNewRow(CType(ogcmat.DataSource, DataTable), TabIndexs.StyleDetail)
                    End If
                End If
            Case Keys.Down
                If SelectedRow = vw1.RowCount - 1 Then
                    CType(ogcmat.DataSource, DataTable).AcceptChanges()
                    InitNewRow(CType(ogcmat.DataSource, DataTable), TabIndexs.StyleDetail)
                End If
            Case Keys.Delete

                If ogvmat.FocusedColumn.FieldName.ToString.ToLower = "FTItemREfNo".ToLower Then
                    ogvmat.SetFocusedRowCellValue("FTItemREfNo", "")
                    CType(ogcmat.DataSource, DataTable).AcceptChanges()
                End If

        End Select
    End Sub

    Private Sub ClearMergeCtrlData()
        Try
            ogcmat.Controls.Remove(m_mergedCellEditorSupl)
            ogcmat.Controls.Remove(m_mergedCellEditorMainMat)
            ogcmat.Controls.Remove(m_mergedCellEditorUnit)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclearclsr.Click
        Call ClearMergeCtrlData()
        HI.TL.HandlerControl.ClearControl(Me)
        Me.ogcmat.DataSource = Nothing
        Me.ogccolor.DataSource = Nothing
        Me.ogcsize.DataSource = Nothing

        FNHSysMSCId.Text = ""
        FNVersion.Text = ""
        FNBomDevType.Text = ""
        FNHSysVenderPramId.Text = ""
        FTStyleDevNameTH.Text = ""
        FTStyleDevNameEN.Text = ""
        FTOwner.Text = ""
        FTNikeDeveloperName.Text = ""
        FTSeason.Text = ""
        FTUploadDate.Text = ""

        Dim xCol As Integer = 0
        Dim Idx As Integer = 0
        Try
            Dim View1 As GridView
            Dim View2 As GridView

            View1 = Me.ogccolor.Views(0)
            View1 = InitialGrid(View1)
            View1.BestFitColumns()

            View2 = Me.ogcsize.Views(0)
            View2 = InitialGrid(View2)
            View2.BestFitColumns()

            ogvmat.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 300
            ogvcolor.Columns.ColumnByFieldName("FTMainMatName").Width = 200
            ogvsize.Columns.ColumnByFieldName("FTMainMatName").Width = 200
            ogvmat.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            ogvcolor.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            ogvsize.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            ogvmat.Columns.ColumnByFieldName("FTComponent").Width = 100

        Catch ex As Exception

        End Try
        Call LoadItemMaster()

    End Sub

    Private Sub ocmrefresh_Click(sender As System.Object, e As System.EventArgs) Handles ocmrefresh.Click
        Try
            Call ClearMergeCtrlData()

            Try
                Me.ogcmat.DataSource = Nothing
            Catch ex As Exception
            End Try

            Try
                Me.ogccolor.DataSource = Nothing
            Catch ex As Exception
            End Try

            Try
                Me.ogcsize.DataSource = Nothing
            Catch ex As Exception
            End Try

            If FNHSysStyleDevId.Text <> "" Then

                Dim _Str As String = "SELECT TOP 1 FNHSysStyleDevId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle  WITH(NOLOCK) WHERE FTStyleDevCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleDevId.Text) & "' "
                FNHSysStyleDevId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")

                If (FNHSysStyleDevId.Properties.Tag.ToString <> "") Then
                    Call LoadStyleDetail(FNHSysStyleDevId.Properties.Tag.ToString)
                    Call LoadColorwaySize(FNHSysStyleDevId.Properties.Tag.ToString)
                End If

            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub InitNewRow(ByVal dataTable As System.Data.DataTable, ByVal TableIndexx As Integer)
        Try
            Dim dr As DataRow
            dtStyleDetail = dataTable

            dr = dtStyleDetail.NewRow()

            Dim MaxSeq As Double = 1.0
            Dim LastSeq As Double = 0

            Dim MaxMatSeq As Double = 1
            Dim LastMatSeq As Integer = 0
            Dim MaxRowIndex As Integer = 0

            For Each r As DataRow In dtStyleDetail.Rows

                LastSeq = r.Item("FNSeq")

                If LastSeq > MaxSeq Then
                    MaxSeq = LastSeq + 1
                    MaxRowIndex = dtStyleDetail.Rows.IndexOf(r)
                End If

                LastMatSeq = Int(r.Item("FNMerMatSeq"))

                If LastMatSeq > MaxMatSeq Then
                    MaxMatSeq = LastMatSeq + 1
                End If

            Next

            If LastSeq = MaxSeq Then MaxSeq = LastSeq + 1
            If LastMatSeq = MaxMatSeq Then MaxMatSeq = LastMatSeq + 1

            'LastMatSeq = Int(dtStyleDetail.Rows(MaxRowIndex).Item("FNMerMatSeq").ToString) + 1

            If TableIndexx = 0 Then

                dr.Item("FNHSysStyleDevId") = Val(FNHSysStyleDevId.Properties.Tag.ToString)
                dr.Item("FNSeq") = MaxSeq
                dr.Item("FNMerMatSeq") = MaxMatSeq
                dr.Item("FNPart") = "1"
                dr.Item("FNConSmp") = 0
                dr.Item("FNConSmpPlus") = 0
                dr.Item("FTStateNominate") = "1"
                dr.Item("FTStateCombination") = "0"
                dr.Item("FTStateFree") = "0"
                dr.Item("FTStateActive") = "1"
                dr.Item("FTStateMainMaterial") = "0"
                dr.Item("FTStateNotShowBomSheet") = "0"
                dr.Item("FTStateLabel") = "0"
                dr.Item("FTUsed") = ""
                dr.Item("FTItemREfNo") = ""


                dtStyleDetail.Rows.Add(dr)

                ogcmat.DataSource = dtStyleDetail
                ogcmat.Refresh()

            ElseIf TableIndexx = 1 Then

                dr.Item("FNHSysStyleDevId") = Val(FNHSysStyleDevId.Properties.Tag.ToString)
                dr.Item("FNSeq") = MaxSeq
                dr.Item("FNColorWaySeq") = MaxSeq
                dr.Item("FTRunColor") = "1"
                dtStyleDetail.Rows.Add(dr)

                ogccolor.DataSource = dtStyleDetail
                ogccolor.Refresh()

            Else

                dr.Item("FNHSysStyleDevId") = Val(FNHSysStyleDevId.Properties.Tag.ToString)
                dr.Item("FNSeq") = MaxSeq
                dr.Item("FNSieBreakDownSeq") = MaxSeq
                dr.Item("FTRunSize") = "1"
                dtStyleDetail.Rows.Add(dr)

                ogcsize.DataSource = dtStyleDetail
                ogcsize.Refresh()

            End If

            'Call DoGrouping()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ocmbomdeleterow_Click(sender As System.Object, e As System.EventArgs) Handles ocmbomdeleterow.Click
        If CheckOwner() = False Then
            Exit Sub
        End If

        With ogvmat

            If .FocusedRowHandle < 0 Then Exit Sub

            RowsIndex = .FocusedRowHandle
            TopVisibleIndex = .TopRowIndex

            If RowsIndex < 0 Then Return
            Call LoadStylePostInfo()
            If CheckPostDataToBomSheet() = False Then Exit Sub
            If Not (.PostEditor() And .UpdateCurrentRow()) Then Return

            Dim _Seq As String = .GetRowCellValue(RowsIndex, "FNMerMatSeq").ToString
            Dim _ItemCode As String = .GetRowCellValue(RowsIndex, "FTItemNo").ToString
            Dim _Part As String = .GetRowCellValue(RowsIndex, "FNPart").ToString
            Dim _FTOrderNo As String = "" '& .GetRowCellValue(RowsIndex, "FTOrderNo").ToString

            Dim _Msg As String = " Item Code : " & _ItemCode & "     Sequence No : " & _Seq & "     Part No : " & _Part & " "

            Dim _Qry As String = ""



            'Delete row of Style Detail.
            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, _Msg) = True Then
                CType(ogcmat.DataSource, DataTable).AcceptChanges()
                Dim dt As DataTable = CType(ogcmat.DataSource, DataTable)
                Dim ItemID As String = ("" & .GetRowCellValue(RowsIndex, "FTItemNo").ToString)

                Dim CurrentPart As Integer = Integer.Parse(Val(.GetRowCellValue(RowsIndex, "FNPart").ToString))

                For Each r As DataRow In dt.Select("FTItemNo='" & HI.UL.ULF.rpQuoted(ItemID) & "'", "FNPart")
                    If Integer.Parse(Val(r.Item("FNPart"))) = CurrentPart Then

                    Else
                        If Integer.Parse(Val(r.Item("FNPart"))) > CurrentPart Then
                            r.Item("FNPart") = Integer.Parse(Val(r.Item("FNPart"))) - 1
                        End If
                    End If

                Next

                If DoDeleteSource(oleDbDataAdapter2, CType(ogcmat.DataSource, DataTable), RowsIndex, TabIndexs.StyleDetail) = False Then Return
                DoDeleteSource(oleDbDataAdapter2, CType(ogccolor.DataSource, DataTable), RowsIndex, TabIndexs.Colorway)
                DoDeleteSource(oleDbDataAdapter2, CType(ogcsize.DataSource, DataTable), RowsIndex, TabIndexs.SizeBreakdown)

                UpdateDatasource()
                CType(ogcmat.DataSource, DataTable).AcceptChanges()


                'View1.SelectRow(RowsIndex - 1)
                If RowsIndex >= .RowCount Then RowsIndex = .RowCount - 1
                .FocusedRowHandle = RowsIndex ' 1

            End If
        End With

    End Sub

    Private Sub ocmdeletecolorway_Click(sender As System.Object, e As System.EventArgs) Handles ocmdeletecolorway.Click

        If CheckOwner() = False Then
            Exit Sub
        End If

        If sFNHSysStyleDevId = "" Then Return
        Call LoadStylePostInfo()
        If CheckPostDataToBomSheet() = False Then Exit Sub
        With Me.ogvcolor
            If .FocusedRowHandle < 0 Then Exit Sub
            If Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = "FTRawMatColorCode".ToUpper Then


                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการลบ Colorway ใช่หรือไม่ ?", 1406030001, .FocusedColumn.Caption) Then
                    Dim Col1 As String = .FocusedColumn.FieldName.ToString
                    Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)
                    Dim Col3 As String = "FTRawMatColorNameEN" & .FocusedColumn.FieldName.ToString.Replace("FTRawMatColorCode", "")
                    Dim Col4 As String = "FTRawMatColorNameTH" & .FocusedColumn.FieldName.ToString.Replace("FTRawMatColorCode", "")
                    Try
                        ogvcolor.Columns.Remove(ogvcolor.Columns.ColumnByFieldName(Col1))
                        CType(Me.ogccolor.DataSource, DataTable).Columns.Remove(Col1)
                    Catch ex As Exception
                    End Try

                    Try
                        ogvcolor.Columns.Remove(ogvcolor.Columns.ColumnByFieldName(Col2))
                        CType(Me.ogccolor.DataSource, DataTable).Columns.Remove(Col2)
                    Catch ex As Exception
                    End Try
                    Try
                        ogvcolor.Columns.Remove(ogvcolor.Columns.ColumnByFieldName(Col3))
                        CType(Me.ogccolor.DataSource, DataTable).Columns.Remove(Col3)
                    Catch ex As Exception
                    End Try
                    Try
                        ogvcolor.Columns.Remove(ogvcolor.Columns.ColumnByFieldName(Col4))
                        CType(Me.ogccolor.DataSource, DataTable).Columns.Remove(Col4)
                    Catch ex As Exception
                    End Try

                    CType(Me.ogccolor.DataSource, DataTable).AcceptChanges()
                End If
            End If

        End With
    End Sub

    Private Sub ImportColor()

        Dim dataAdapter As SqlDataAdapter
        Dim dt As DataTable = New DataTable()

        Dim dc As DataColumn
        Dim dc1 As DataColumn
        Dim dc3 As DataColumn
        Dim dc4 As DataColumn

        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

        Try
            dataAdapter = CreateAdapterImportColor(HI.Conn.SQLConn.Cnn)

            Dim SelectCmd As SqlCommand = New SqlCommand(dataAdapter.SelectCommand.CommandText, HI.Conn.SQLConn.Cnn)
            SelectCmd.Parameters.AddWithValue("@FNHSysStyleDevId", Val(FNHSysStyleDevId.Properties.Tag.ToString))
            SelectCmd.ExecuteNonQuery()

            dataAdapter.SelectCommand = SelectCmd
            dataAdapter.Fill(dt)

            Dim StrSql As String = ""

            Dim sqlCmd As New SqlCommand
            sqlCmd.Connection = HI.Conn.SQLConn.Cnn
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GET_STYLE_COLORWAY_DEVELOP]"
            sqlCmd.Parameters.AddWithValue("@FNHSysStyleDevId", Val(FNHSysStyleDevId.Properties.Tag.ToString))
            sqlCmd.Parameters.AddWithValue("@LANGID", HI.ST.Lang.Language.ToString())

            oleDbDataAdapter2 = New SqlClient.SqlDataAdapter(sqlCmd.CommandText, HI.Conn.SQLConn._ConnString)
            oleDbDataAdapter2.SelectCommand = sqlCmd

            dtStyleDetail = New DataTable()
            oleDbDataAdapter2.Fill(dtStyleDetail)

            '' Initial data to dynamic column
            Dim InitDt As DataTable = New DataTable()
            For Each c As DataColumn In dtStyleDetail.Columns
                InitDt.Columns.Add(c.ColumnName, c.DataType)
            Next

            '' Insert Dynamic Grid Column Header
            View = Me.ogccolor.Views(0)
            View = InitialGrid(View)
            View.BestFitColumns()

            ogvmat.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 300
            ogvcolor.Columns.ColumnByFieldName("FTMainMatName").Width = 200
            ogvsize.Columns.ColumnByFieldName("FTMainMatName").Width = 200

            ogvcolor.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            ogvsize.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            ogvmat.Columns.ColumnByFieldName("FTComponent").Width = 100

            Dim FNSeqCurr As Integer = 0
            Dim FNSeqLast As Integer = 0

            For Each r As DataRow In dt.Rows
                'FNSeqCurr = Val(r!FNMatColorSeq.ToString)
                'If FNSeqLast = 0 Then FNSeqLast = FNSeqCurr
                'If FNSeqCurr > 0 And FNSeqLast < FNSeqCurr Then

                dc = New DataColumn("FTRawMatColorCode" & r!FTColorWay.ToString, System.Type.GetType("System.String"))
                dc1 = New DataColumn("FNHSysRawMatColorId" & "FTRawMatColorCode" & r!FTColorWay.ToString, System.Type.GetType("System.String"))
                dc3 = New DataColumn("FTRawMatColorNameTH" & r!FTColorWay.ToString, System.Type.GetType("System.String"))
                dc4 = New DataColumn("FTRawMatColorNameEN" & r!FTColorWay.ToString, System.Type.GetType("System.String"))

                dc.Caption = r!FTColorWay.ToString
                dc1.Caption = "FNHSysRawMatColorId"

                If View.Columns(dc.ColumnName) Is Nothing Then

                    Dim ExistCol As Boolean = False
                    For Each x As GridColumn In View.Columns
                        If x.Tag = r!FNHSysMatColorId.ToString Then
                            ExistCol = True
                        End If
                    Next

                    If ExistCol = False Then

                        Try

                            View.Columns.Item(dc.ColumnName).FieldName = dc.ColumnName
                            View.Columns.Item(dc1.ColumnName).FieldName = dc1.ColumnName
                            View.Columns.Item(dc3.ColumnName).FieldName = dc3.ColumnName
                            View.Columns.Item(dc4.ColumnName).FieldName = dc4.ColumnName
                            InitDt.Columns.Add(dc.ColumnName)
                            InitDt.Columns.Add(dc1.ColumnName)
                            InitDt.Columns.Add(dc3.ColumnName)
                            InitDt.Columns.Add(dc4.ColumnName)

                        Catch ex As Exception

                            View.Columns.AddField(dc.ColumnName)
                            View.Columns(dc.ColumnName).FieldName = dc.ColumnName
                            View.Columns(dc.ColumnName).Name = dc.ColumnName
                            View.Columns(dc.ColumnName).Caption = dc.Caption
                            View.Columns(dc.ColumnName).Visible = True
                            View.Columns(dc.ColumnName).Width = 70
                            View.Columns(dc.ColumnName).OptionsColumn.AllowShowHide = False
                            View.Columns(dc.ColumnName).OptionsColumn.ShowInCustomizationForm = False

                            View.Columns.AddField(dc1.ColumnName)
                            View.Columns(dc1.ColumnName).FieldName = dc1.ColumnName
                            View.Columns(dc1.ColumnName).Name = dc1.ColumnName
                            View.Columns(dc1.ColumnName).Caption = dc1.Caption
                            View.Columns(dc1.ColumnName).Tag = r!FNHSysMatColorId.ToString
                            View.Columns(dc1.ColumnName).Visible = False
                            View.Columns(dc1.ColumnName).OptionsColumn.AllowShowHide = False
                            View.Columns(dc1.ColumnName).OptionsColumn.ShowInCustomizationForm = False

                            View.Columns.AddField(dc3.ColumnName)
                            View.Columns(dc3.ColumnName).FieldName = dc3.ColumnName
                            View.Columns(dc3.ColumnName).Name = dc3.ColumnName
                            View.Columns(dc3.ColumnName).Caption = dc3.Caption
                            View.Columns(dc3.ColumnName).Tag = r!FNHSysMatColorId.ToString
                            View.Columns(dc3.ColumnName).Visible = False
                            View.Columns(dc3.ColumnName).OptionsColumn.AllowShowHide = False
                            View.Columns(dc3.ColumnName).OptionsColumn.ShowInCustomizationForm = False

                            View.Columns.AddField(dc4.ColumnName)
                            View.Columns(dc4.ColumnName).FieldName = dc4.ColumnName
                            View.Columns(dc4.ColumnName).Name = dc4.ColumnName
                            View.Columns(dc4.ColumnName).Caption = dc4.Caption
                            View.Columns(dc4.ColumnName).Tag = r!FNHSysMatColorId.ToString
                            View.Columns(dc4.ColumnName).Visible = False
                            View.Columns(dc4.ColumnName).OptionsColumn.AllowShowHide = False
                            View.Columns(dc4.ColumnName).OptionsColumn.ShowInCustomizationForm = False
                            View.Columns(dc4.ColumnName).OptionsColumn.AllowSort = DefaultBoolean.False

                            Dim repos As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
                            repos.Buttons.Item(0).Tag = "169"
                            View.Columns(dc.ColumnName).ColumnEdit = repos
                            View.Columns(dc1.ColumnName).ColumnEdit = RepRawMatIDCaledit

                            With repos
                                .CharacterCasing = CharacterCasing.Upper

                                AddHandler .Click, AddressOf DynamicResponButtoneColor_Gotocus
                                AddHandler .ButtonClick, AddressOf DynamicResponButtoneSysHideColor_ButtonClick
                                AddHandler .KeyDown, AddressOf DynamicButtoneditColor_KeyDown

                            End With

                            InitDt.Columns.Add(dc.ColumnName)
                            InitDt.Columns.Add(dc1.ColumnName)
                            InitDt.Columns.Add(dc3.ColumnName)
                            InitDt.Columns.Add(dc4.ColumnName)

                        End Try
                    End If

                End If


            Next

            '' Get new datasource colorway
            sqlCmd = New SqlCommand
            sqlCmd.Connection = HI.Conn.SQLConn.Cnn
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GET_STYLE_COLORWAY_DEVELOP_INFO]"
            sqlCmd.Parameters.AddWithValue("@FNHSysStyleDevId", Val(FNHSysStyleDevId.Properties.Tag.ToString))
            sqlCmd.Parameters.AddWithValue("@LANGID", HI.ST.Lang.Language.ToString())

            oleDbDataAdapter2 = New SqlClient.SqlDataAdapter(sqlCmd.CommandText, HI.Conn.SQLConn._ConnString)
            oleDbDataAdapter2.SelectCommand = sqlCmd

            Dim dtColor As DataTable = New DataTable()
            oleDbDataAdapter2.Fill(dtColor)

            '' Fill data to new datatable
            View = Me.ogccolor.Views(0)
            Dim FNSeqSame As Boolean = False
            For Each r As DataRow In dtColor.Rows
                Dim n As DataRow

                FNSeqSame = False
                For Each x As DataRow In InitDt.Select("FNSeq=" & Val(r.Item("FNSeq")) & "")
                    If x.Item("FNSeq") = r.Item("FNSeq") Then
                        FNSeqSame = True

                        For Each c As GridColumn In View.Columns
                            If c.Tag = "1406060024" Then
                                Beep()
                            End If

                            If c.Tag = "1406060024" Then
                                Beep()
                            End If

                            If c.Tag = r.Item("FNHSysMatColorId").ToString() Then
                                'If c.Caption = r.Item("FTMatColorName").ToString() And r.Item("FNHSysMatColorId").ToString() <> "" And c.FieldName.ToString.Replace("FNHSysRawMatColorId" & "FTRawMatColorCode", "") = r.Item("FTMatColorName").ToString() Then
                                x.Item("FTRawMatColorCode" & r!FTMatColorName.ToString) = r.Item("FTColorWay")
                                x.Item(c.Name) = r.Item("FNHSysRawMatColorId")
                                x.Item("FTRawMatColorNameTH" & r!FTMatColorName.ToString) = r.Item("FTRawMatColorNameTH")
                                x.Item("FTRawMatColorNameEN" & r!FTMatColorName.ToString) = r.Item("FTRawMatColorNameEN")

                                Exit For
                            End If
                        Next

                    End If
                Next

                If FNSeqSame = False Then
                    n = InitDt.NewRow()
                    n.Item("FNHSysStyleDevId") = r.Item("FNHSysStyleDevId")
                    n.Item("FNSeq") = r.Item("FNSeq")
                    n.Item("FNMerMatSeq") = r.Item("FNMerMatSeq")
                    n.Item("FTItemNo") = r.Item("FTItemNo")
                    n.Item("FTMainMatName") = r.Item("FTMainMatName")
                    n.Item("FNPart") = r.Item("FNPart")
                    n.Item("FTPositionPartName") = r.Item("FTPositionPartName")
                    n.Item("FNConSmp") = r.Item("FNConSmp")
                    n.Item("FNConSmpPlus") = r.Item("FNConSmpPlus")
                    n.Item("FTRunColor") = r.Item("FTRunColor")
                    n.Item("FNOrderSetType") = r.Item("FNOrderSetType")

                    For Each c As GridColumn In View.Columns
                        If c.Tag = "1406060024" Then
                            Beep()
                        End If
                        If c.Tag = r.Item("FNHSysMatColorId").ToString() And r.Item("FNHSysMatColorId").ToString() <> "" Then
                            'If c.Caption = r.Item("FTMatColorName").ToString() And r.Item("FNHSysMatColorId").ToString() <> "" And c.FieldName.ToString.Replace("FNHSysRawMatColorId" & "FTRawMatColorCode", "") = r.Item("FTMatColorName").ToString() Then

                            Try
                                n.Item("FNColorWaySeq") = r.Item("FNColorWaySeq")
                                '    n.Item("FTRunColor") = IIf(r.Item("FTRunColor").ToString = "", "0", r.Item("FTRunColor").ToString)
                                n.Item("FTRawMatColorCode" & r!FTMatColorName.ToString) = r.Item("FTColorWay")
                                n.Item("FTRawMatColorNameEN" & r!FTMatColorName.ToString) = r.Item("FTRawMatColorNameEN")
                                n.Item("FTRawMatColorNameTH" & r!FTMatColorName.ToString) = r.Item("FTRawMatColorNameTH")
                                n.Item(c.Name) = r.Item("FNHSysRawMatColorId")
                                ' ,SC.FTRawMatColorNameEN,SC.FTRawMatColorNameTH 
                            Catch ex As Exception

                            End Try

                            Exit For
                        End If
                    Next

                    InitDt.Rows.Add(n)
                End If

            Next

            InitDt.AcceptChanges()

            'View.Columns("FTColorWay").Visible = False
            View.BestFitColumns()
            View.OptionsView.ShowAutoFilterRow = True

            ogccolor.DataSource = InitDt
            ogccolor = View.GridControl
            ogvmat.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 300
            ogvcolor.Columns.ColumnByFieldName("FTMainMatName").Width = 200
            ogvsize.Columns.ColumnByFieldName("FTMainMatName").Width = 200
            ogvcolor.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            ogvsize.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            ogvmat.Columns.ColumnByFieldName("FTComponent").Width = 100

            ogccolor.Refresh()

            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        Catch ex As Exception
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        End Try

    End Sub

    Private Sub ImportSize()

        Dim dataAdapter As SqlDataAdapter
        Dim dt As DataTable = New DataTable()
        Dim dc As DataColumn
        Dim dc1 As DataColumn

        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

        Try
            dataAdapter = CreateAdapterImportSize(HI.Conn.SQLConn.Cnn)

            Dim SelectCmd As SqlCommand = New SqlCommand(dataAdapter.SelectCommand.CommandText, HI.Conn.SQLConn.Cnn)
            SelectCmd.Parameters.AddWithValue("@FNHSysStyleDevId", Val(FNHSysStyleDevId.Properties.Tag.ToString))
            SelectCmd.ExecuteNonQuery()

            dataAdapter.SelectCommand = SelectCmd
            dataAdapter.Fill(dt)

            Dim StrSql As String = ""

            StrSql = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GET_STYLE_SIZEBREAKDOWN_DEVELOP] " & Val(FNHSysStyleDevId.Properties.Tag.ToString) & ",'" & HI.ST.Lang.Language.ToString() & "' "
            dtStyleDetail = HI.Conn.SQLConn.GetDataTable(StrSql, Conn.DB.DataBaseName.DB_MERCHAN)

            '' Initial data to dynamic column
            Dim InitDt As DataTable = New DataTable()
            For Each c As DataColumn In dtStyleDetail.Columns
                InitDt.Columns.Add(c.ColumnName, c.DataType)
            Next

            '' Insert Dynamic Grid Column Header
            View = Me.ogcsize.Views(0)
            View = InitialGrid(View)
            View.BestFitColumns()
            ogvmat.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 300
            ogvcolor.Columns.ColumnByFieldName("FTMainMatName").Width = 200
            ogvsize.Columns.ColumnByFieldName("FTMainMatName").Width = 200

            ogvcolor.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            ogvsize.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            ogvmat.Columns.ColumnByFieldName("FTComponent").Width = 100

            'Dim FNSeqCurr As Integer = 0
            'Dim FNSeqLast As Integer = 0

            For Each r As DataRow In dt.Rows
                'FNSeqCurr = Val(r!FNMatSizeSeq.ToString)
                'If FNSeqLast = 0 Then FNSeqLast = FNSeqCurr
                'If FNSeqCurr > 0 And FNSeqLast < FNSeqCurr Then

                dc = New DataColumn("FTRawMatSizeCode" & r!FTSizeBreakDown.ToString, System.Type.GetType("System.String"))
                dc1 = New DataColumn("FNHSysRawMatSizeId" & "FTRawMatSizeCode" & r!FTSizeBreakDown.ToString, System.Type.GetType("System.String"))
                dc.Caption = r!FTSizeBreakDown.ToString
                dc1.Caption = "FNHSysRawMatSizeId"

                If View.Columns(dc.ColumnName) Is Nothing Then

                    Dim ExistCol As Boolean = False
                    For Each x As GridColumn In View.Columns
                        If x.Tag = r!FNHSysMatSizeId.ToString Then
                            ExistCol = True
                        End If
                    Next

                    If ExistCol = False Then
                        Try
                            View.Columns.Item(dc.ColumnName).FieldName = dc.ColumnName
                            View.Columns.Item(dc1.ColumnName).FieldName = dc1.ColumnName
                            InitDt.Columns.Add(dc.ColumnName)
                            InitDt.Columns.Add(dc1.ColumnName)
                        Catch ex As Exception
                            View.Columns.AddField(dc.ColumnName)
                            View.Columns(dc.ColumnName).FieldName = dc.ColumnName
                            View.Columns(dc.ColumnName).Name = dc.ColumnName
                            View.Columns(dc.ColumnName).Caption = dc.Caption
                            View.Columns(dc.ColumnName).Visible = True
                            View.Columns(dc.ColumnName).Width = 70
                            View.Columns(dc.ColumnName).OptionsColumn.AllowShowHide = False
                            View.Columns(dc.ColumnName).OptionsColumn.ShowInCustomizationForm = False

                            View.Columns.AddField(dc1.ColumnName)
                            View.Columns(dc1.ColumnName).FieldName = dc1.ColumnName
                            View.Columns(dc1.ColumnName).Name = dc1.ColumnName
                            View.Columns(dc1.ColumnName).Caption = dc1.Caption
                            View.Columns(dc1.ColumnName).Tag = r!FNHSysMatSizeId.ToString
                            View.Columns(dc1.ColumnName).Visible = False
                            View.Columns(dc1.ColumnName).OptionsColumn.AllowShowHide = False
                            View.Columns(dc1.ColumnName).OptionsColumn.ShowInCustomizationForm = False
                            View.Columns(dc1.ColumnName).OptionsColumn.AllowSort = DefaultBoolean.False

                            Dim repos As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
                            repos.Buttons.Item(0).Tag = "170"
                            View.Columns(dc.ColumnName).ColumnEdit = repos

                            With repos
                                .CharacterCasing = CharacterCasing.Upper
                                AddHandler .Click, AddressOf DynamicResponButtone_Gotocus
                                AddHandler .ButtonClick, AddressOf DynamicResponButtoneSysHide_ButtonClick

                                AddHandler .KeyDown, AddressOf DynamicButtoneditSize_KeyDown
                            End With

                            InitDt.Columns.Add(dc.ColumnName)
                            InitDt.Columns.Add(dc1.ColumnName)

                        End Try

                    End If

                    '' Initialize
                    'FNSeqLast = FNSeqCurr

                End If
            Next


            Dim dtSize As DataTable = New DataTable()
            'oleDbDataAdapter2.Fill(dtSize)
            StrSql = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GET_STYLE_SIZEBREAKDOWNINFO_DEVELOP] " & Val(FNHSysStyleDevId.Properties.Tag.ToString) & ",'" & HI.ST.Lang.Language.ToString() & "' "
            dtSize = HI.Conn.SQLConn.GetDataTable(StrSql, Conn.DB.DataBaseName.DB_MERCHAN)

            '' Fill data to new datatable
            Dim FNSeqSame As Boolean = False
            For Each r As DataRow In dtSize.Rows
                Dim n As DataRow

                FNSeqSame = False
                For Each x As DataRow In InitDt.Rows
                    If x.Item("FNSeq") = r.Item("FNSeq") Then
                        FNSeqSame = True

                        For Each c As GridColumn In View.Columns
                            If c.Tag = r.Item("FNHSysMatSizeId").ToString() Then
                                x.Item("FTRawMatSizeCode" & r!FTMatSizeName.ToString) = r.Item("FTSizeBreakDown")
                                x.Item(c.Name) = r.Item("FNHSysRawMatSizeId")

                                Exit For
                            End If
                        Next

                    End If
                Next

                If FNSeqSame = False Then
                    n = InitDt.NewRow()
                    n.Item("FNHSysStyleDevId") = r.Item("FNHSysStyleDevId")
                    n.Item("FNSeq") = r.Item("FNSeq")
                    n.Item("FNMerMatSeq") = r.Item("FNMerMatSeq")
                    n.Item("FTItemNo") = r.Item("FTItemNo")
                    n.Item("FTMainMatName") = r.Item("FTMainMatName")
                    n.Item("FNPart") = r.Item("FNPart")
                    n.Item("FTPositionPartName") = r.Item("FTPositionPartName")
                    n.Item("FNConSmp") = r.Item("FNConSmp")
                    n.Item("FNConSmpPlus") = r.Item("FNConSmpPlus")
                    n.Item("FTRunSize") = r.Item("FTRunSize")
                    n.Item("FNOrderSetType") = r.Item("FNOrderSetType")

                    For Each c As GridColumn In View.Columns
                        If c.Tag = r.Item("FNHSysMatSizeId").ToString() And r.Item("FNHSysMatSizeId").ToString() <> "" Then
                            n.Item("FNSieBreakDownSeq") = r.Item("FNSieBreakDownSeq")
                            ' n.Item("FTRunSize") = IIf(r.Item("FTRunSize").ToString = "", "0", r.Item("FTRunSize").ToString)
                            n.Item("FTRawMatSizeCode" & r!FTMatSizeName.ToString) = r.Item("FTSizeBreakDown")
                            n.Item(c.Name) = r.Item("FNHSysRawMatSizeId")

                            Exit For
                        End If
                    Next

                    InitDt.Rows.Add(n)
                End If

            Next

            InitDt.AcceptChanges()

            'View.Columns("FTColorWay").Visible = False
            View.BestFitColumns()
            View.OptionsView.ShowAutoFilterRow = True
            ogvmat.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 300
            ogvcolor.Columns.ColumnByFieldName("FTMainMatName").Width = 200
            ogvsize.Columns.ColumnByFieldName("FTMainMatName").Width = 200
            ogvcolor.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            ogvsize.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            ogvmat.Columns.ColumnByFieldName("FTComponent").Width = 100

            ogcsize.DataSource = InitDt
            ogcsize = View.GridControl
            ogcsize.Refresh()

            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        Catch ex As Exception
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            'MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Function Update_OrderCombinationInfo(ByVal _FNHSysStyleDevId As Integer) As Boolean
        Dim ret As Boolean = True
        Return ret
    End Function

    Private Function Update_OrderMainmaterialInfo(ByVal _FNHSysStyleDevId As Integer) As Boolean
        Dim ret As Boolean = True
        Return ret
    End Function

    Private Sub PostSave(ByVal TableName As String, Optional ByVal refDocKey As String = Nothing)
        Try

        Catch ex As Exception
            '' To do something
        End Try
    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        Try
            If Me.FNHSysStyleDevId.Text <> "" Then
                If Me.FTSeason.Text <> "" Then
                    If Me.FTStyleDevNameTH.Text <> "" Then
                        If Me.FTStyleDevNameEN.Text <> "" Then
                            If Me.FNHSysCustId.Text <> "" And Me.FNHSysCustId.Properties.Tag.ToString <> "" Then

                                If Me.FTDevelopDate.Text <> "" Then
                                    _Pass = True
                                Else
                                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTDevelopDate_lbl.Text)
                                    FTDevelopDate.Focus()
                                End If

                            Else
                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysCustId_lbl.Text)
                                FNHSysCustId.Focus()
                            End If
                        Else
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTStyleDevNameEN_lbl.Text)
                            FTStyleDevNameEN.Focus()
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTStyleDevNameTH_lbl.Text)
                        FTStyleDevNameTH.Focus()
                    End If

                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTSeason_lbl.Text)
                    FTSeason.Focus()
                End If

            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysStyleDevId_lbl.Text)
                FNHSysStyleDevId.Focus()
            End If
        Catch ex As Exception

        End Try
        Return _Pass
    End Function
    Private Sub ocmsave_Click(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click

        If CheckOwner() = False Then
            Exit Sub
        End If

        If Me.VerifyData() Then

            If ogcmat.DataSource Is Nothing Then
                Dim _Str As String = ""
                _Str = "SELECT TOP 1 FNHSysStyleDevId FROM "
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTDevelopStyle]  WITH(NOLOCK) "
                _Str &= vbCrLf & "    WHERE FTStyleDevCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleDevId.Text) & "' AND FTSeason='" & HI.UL.ULF.rpQuoted(FTSeason.Text) & "'  AND ISNULL(FNVersion,0)=" & FNVersion.Text & " AND FNBomDevType =" & FNBomDevType.SelectedIndex & "  "

                If Integer.Parse(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "0")) <= 0 Then
                    Call LoadStyleDetail(-99)
                    Call LoadColorwaySize(-99)

                    If ogcmat.DataSource Is Nothing Then
                        Exit Sub
                    End If

                Else
                    Exit Sub
                End If

            End If

            Call LoadStylePostInfo()
            If CheckPostDataToBomSheet() = False Then Exit Sub

            With CType(ogcmat.DataSource, DataTable)
                .AcceptChanges()

                If .Select("FTUnitCode='' AND FTStateNotShowBomSheet='0'").Length > 0 Then
                    HI.MG.ShowMsg.mInfo("กรุณาทำการระบุ  หน่วยวัตถุดิบให้ครบ !!!", 1505210874, Me.Text, , MessageBoxIcon.Warning)
                    Exit Sub
                End If

                If .Select("FTStateFree='1' AND FTSuplCode=''").Length > 0 Then
                    HI.MG.ShowMsg.mInfo("พบข้อมูลวัตถุดิบ Free กรุณาทำการระบุ Supplier !!!", 1505210875, Me.Text, , MessageBoxIcon.Warning)
                    Exit Sub
                End If

            End With

            Dim _Spls As New HI.TL.SplashScreen("Saving.... ,Please Wait.")
            If Me.SaveData Then
                '' Update order information.
                Me.FNHSysStyleDevId.Focus()
                Call LoadItemMaster()
                _Spls.Close()
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Me.ProcComplete = True
                LoadStyleInfo(FNHSysStyleDevId.Properties.Tag.ToString)

                ' InitNewRow(CType(ogcmatcode.DataSource, DataTable), TabIndexs.StyleDetail)
            Else
                _Spls.Close()
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub

    Private Sub ocmdelete_Click(sender As System.Object, e As System.EventArgs) Handles ocmdelete.Click

        If CheckOwner() = False Then
            Exit Sub
        End If
        '  If CType(ogcmat.DataSource, DataTable).Rows.Count > 0 Then
        Call LoadStylePostInfo()
        If CheckPostDataToBomSheet() = False Then Exit Sub

        If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, Me.Text) = True Then
            Dim _Spls As New HI.TL.SplashScreen("Deleting...   Please Wait   ")
            If Me.DeleteAllData(_Spls) Then
                _Spls.Close()
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                HI.TL.HandlerControl.ClearControl(Me)
                Me.otb.SelectedTabPageIndex = 0
            Else
                _Spls.Close()
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            End If
        End If

        '  End If
    End Sub

    Private Sub ocmbomdiffpart_Click(sender As System.Object, e As System.EventArgs) Handles ocmbomdiffpart.Click
        If CheckOwner() = False Then
            Exit Sub
        End If
        Try
            CType(ogcmat.DataSource, DataTable).AcceptChanges()
        Catch ex As Exception
        End Try

        Dim crRow As Double, nxRow As String, nwRow As String
        Dim RowCount As Integer = 0
        View = Me.ogcmat.Views(0)
        If sFNHSysStyleDevId = "" Then Return
        Call LoadStylePostInfo()
        If CheckPostDataToBomSheet() = False Then Exit Sub
        If (Not IsDBNull(View)) Then
            RowsIndex = View.FocusedRowHandle
            TopVisibleIndex = View.TopRowIndex
            RowCount = View.RowCount
        End If
        nxRow = 0

        crRow = Val(View.GetRowCellValue(RowsIndex, "FNMerMatSeq").ToString)

        Dim dt As DataTable = CType(ogcmat.DataSource, DataTable)

        Try
            ' nxRow = Val(View.GetRowCellValue(RowsIndex + 1, "FNMerMatSeq").ToString)

            For Each R As DataRow In dt.Select("FNMerMatSeq>" & crRow & "", "FNMerMatSeq")
                nxRow = Double.Parse(Val(R!FNMerMatSeq.ToString))
                Exit For
            Next
        Catch ex As Exception
            nxRow = 0
        End Try


        nwRow = Str(crRow + 0.001).Trim

        'If nxRow = nwRow Then
        '    Return
        'End If

        Dim MaxSeq As Double = 1.0
        Dim LastSeq As Double = 0

        For Each r As DataRow In dt.Rows
            LastSeq = r.Item("FNSeq")
            If LastSeq >= MaxSeq Then
                MaxSeq = LastSeq + 1
            End If
        Next

        If LastSeq = MaxSeq Then MaxSeq = LastSeq + 1

        Dim ItemID As Long = 0 'Val(View.GetRowCellValue(RowsIndex, "FNHSysMerMatId").ToString)
        Dim ItemCode As String = View.GetRowCellValue(RowsIndex, "FTItemNo").ToString
        Dim CurrentPart As Integer = Integer.Parse(Val(View.GetRowCellValue(RowsIndex, "FNPart").ToString))
        Dim MaxPart As Integer = 0
        Dim LastPart As Integer = 0
        Dim CurentSeq As Integer = Integer.Parse(Val(View.GetRowCellValue(RowsIndex, "FNSeq").ToString))
        Dim dtP As DataTable = CType(ogcmat.DataSource, DataTable)

        'For Each r As DataRow In dt.Rows

        '    If ItemID = Val(r.Item("FNHSysMerMatId")) Then

        '        LastPart = CInt(r.Item("FNPart"))

        '        If LastPart >= MaxPart Then
        '            MaxPart = LastPart + 1
        '        End If

        '    End If

        'Next

        dt = CType(ogcmat.DataSource, DataTable)
        Dim StateAdd As Boolean = False
        For Each r As DataRow In dt.Select("FTItemNo='" & ItemCode & "'  ", "FNPart")

            If Integer.Parse(Val(r.Item("FNPart"))) = CurrentPart Then
                If StateAdd = False Then
                    Dim dr As DataRow = dt.NewRow()

                    For Each c As DataColumn In dt.Columns

                        Select Case c.ColumnName
                            Case "FNSeq"
                                dr.Item(c) = MaxSeq
                            Case "FNPart"
                                dr.Item(c) = CurrentPart + 1
                            Case Else

                                Try
                                    dr.Item(c) = View.GetRowCellValue(RowsIndex, c.ColumnName) '.ToString
                                Catch ex As Exception
                                End Try

                        End Select
                    Next

                    dt.Rows.InsertAt(dr, RowsIndex + 1)
                    StateAdd = True
                End If

            Else
                If Integer.Parse(Val(r.Item("FNPart"))) > CurrentPart Then
                    r.Item("FNPart") = Integer.Parse(Val(r.Item("FNPart"))) + 1
                End If
            End If
        Next

        ogcmat.DataSource = dt.Copy
        ogvmat.FocusedRowHandle = RowsIndex + 1

        'Try
        '    CType(ogcmat.DataSource, DataTable).AcceptChanges()
        'Catch ex As Exception
        'End Try

        ' If LastPart = MaxPart Then MaxPart = LastPart + 1

        Call DiffpartColor()
        Call DiffpartSize()
        Call InitialGridMergCell()

    End Sub

    Private Sub DiffpartColor()
        Try
            Try
                CType(ogccolor.DataSource, DataTable).AcceptChanges()
            Catch ex As Exception
            End Try

            Dim crRow As Double, nxRow As String, nwRow As String
            Dim RowCount As Integer = 0
            View = Me.ogccolor.Views(0)
            'If FNHSysStyleId.Properties.Tag.ToString = "" Or FNHSysSeasonId.Properties.Tag.ToString = "" Then Return

            If (Not IsDBNull(View)) Then
                RowsIndex = (DirectCast(Me.ogcmat.Views(0), DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle) - 1
                TopVisibleIndex = View.TopRowIndex
                RowCount = View.RowCount
            End If
            nxRow = 0
            crRow = Val(View.GetRowCellValue(RowsIndex, "FNMerMatSeq").ToString)
            Dim dt As DataTable = CType(ogccolor.DataSource, DataTable)

            Try
                For Each R As DataRow In dt.Select("FNMerMatSeq>" & crRow & "", "FNMerMatSeq")
                    nxRow = Double.Parse(Val(R!FNMerMatSeq.ToString))
                    Exit For
                Next
            Catch ex As Exception
                nxRow = 0
            End Try

            nwRow = Str(crRow + 0.001).Trim
            Dim MaxSeq As Double = 1.0
            Dim LastSeq As Double = 0

            For Each r As DataRow In dt.Rows
                LastSeq = r.Item("FNSeq")
                If LastSeq >= MaxSeq Then
                    MaxSeq = LastSeq + 1
                End If
            Next

            If LastSeq = MaxSeq Then MaxSeq = LastSeq + 1

            Dim ItemID As Long = 0 'Val(View.GetRowCellValue(RowsIndex, "FNHSysMerMatId").ToString)
            Dim ItemCode As String = View.GetRowCellValue(RowsIndex, "FTItemNo").ToString
            Dim CurrentPart As Integer = Integer.Parse(Val(View.GetRowCellValue(RowsIndex, "FNPart").ToString))
            Dim MaxPart As Integer = 0
            Dim LastPart As Integer = 0
            Dim CurentSeq As Integer = Integer.Parse(Val(View.GetRowCellValue(RowsIndex, "FNSeq").ToString))
            Dim dtP As DataTable = CType(ogccolor.DataSource, DataTable)

            dt = CType(ogccolor.DataSource, DataTable)
            Dim StateAdd As Boolean = False
            For Each r As DataRow In dt.Select("FTItemNo='" & ItemCode & "' ", "FNPart")

                If Integer.Parse(Val(r.Item("FNPart"))) = CurrentPart Then
                    If StateAdd = False Then
                        Dim dr As DataRow = dt.NewRow()
                        For Each c As DataColumn In dt.Columns
                            Select Case c.ColumnName
                                Case "FNSeq"
                                    dr.Item(c) = MaxSeq
                                Case "FNPart"
                                    dr.Item(c) = CurrentPart + 1
                                Case Else
                                    Try
                                        dr.Item(c) = View.GetRowCellValue(RowsIndex, c.ColumnName) '.ToString
                                    Catch ex As Exception
                                    End Try
                            End Select
                        Next

                        dt.Rows.InsertAt(dr, RowsIndex + 1)
                        StateAdd = True
                    End If
                Else
                    If Integer.Parse(Val(r.Item("FNPart"))) > CurrentPart Then
                        r.Item("FNPart") = Integer.Parse(Val(r.Item("FNPart"))) + 1
                    End If
                End If
            Next

            Try
                CType(ogccolor.DataSource, DataTable).AcceptChanges()
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DiffpartSize()
        Try
            Try
                CType(ogcsize.DataSource, DataTable).AcceptChanges()
            Catch ex As Exception
            End Try

            Dim crRow As Double, nxRow As String, nwRow As String
            Dim RowCount As Integer = 0
            View = Me.ogcsize.Views(0)
            ' If FNHSysStyleId.Properties.Tag.ToString = "" Or FNHSysSeasonId.Properties.Tag.ToString = "" Then Return

            If (Not IsDBNull(View)) Then
                RowsIndex = (DirectCast(Me.ogcmat.Views(0), DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle) - 1
                TopVisibleIndex = View.TopRowIndex
                RowCount = View.RowCount
            End If
            nxRow = 0
            crRow = Val(View.GetRowCellValue(RowsIndex, "FNMerMatSeq").ToString)
            Dim dt As DataTable = CType(ogcsize.DataSource, DataTable)

            Try
                For Each R As DataRow In dt.Select("FNMerMatSeq>" & crRow & "", "FNMerMatSeq")
                    nxRow = Double.Parse(Val(R!FNMerMatSeq.ToString))
                    Exit For
                Next
            Catch ex As Exception
                nxRow = 0
            End Try

            nwRow = Str(crRow + 0.001).Trim
            Dim MaxSeq As Double = 1.0
            Dim LastSeq As Double = 0

            For Each r As DataRow In dt.Rows
                LastSeq = r.Item("FNSeq")
                If LastSeq >= MaxSeq Then
                    MaxSeq = LastSeq + 1
                End If
            Next

            If LastSeq = MaxSeq Then MaxSeq = LastSeq + 1

            Dim ItemID As Long = 0 'Val(View.GetRowCellValue(RowsIndex, "FNHSysMerMatId").ToString)
            Dim ItemCode As String = View.GetRowCellValue(RowsIndex, "FTItemNo").ToString
            Dim CurrentPart As Integer = Integer.Parse(Val(View.GetRowCellValue(RowsIndex, "FNPart").ToString))
            Dim MaxPart As Integer = 0
            Dim LastPart As Integer = 0
            Dim CurentSeq As Integer = Integer.Parse(Val(View.GetRowCellValue(RowsIndex, "FNSeq").ToString))
            Dim dtP As DataTable = CType(ogcsize.DataSource, DataTable)

            dt = CType(ogcsize.DataSource, DataTable)
            Dim StateAdd As Boolean = False
            For Each r As DataRow In dt.Select("FTItemNo='" & ItemCode & "' ", "FNPart")

                If Integer.Parse(Val(r.Item("FNPart"))) = CurrentPart Then
                    If StateAdd = False Then
                        Dim dr As DataRow = dt.NewRow()
                        For Each c As DataColumn In dt.Columns
                            Select Case c.ColumnName
                                Case "FNSeq"
                                    dr.Item(c) = MaxSeq
                                Case "FNPart"
                                    dr.Item(c) = CurrentPart + 1
                                Case Else
                                    Try
                                        dr.Item(c) = View.GetRowCellValue(RowsIndex, c.ColumnName) '.ToString
                                    Catch ex As Exception
                                    End Try
                            End Select
                        Next

                        dt.Rows.InsertAt(dr, RowsIndex + 1)
                        StateAdd = True
                    End If
                Else
                    If Integer.Parse(Val(r.Item("FNPart"))) > CurrentPart Then
                        r.Item("FNPart") = Integer.Parse(Val(r.Item("FNPart"))) + 1
                    End If
                End If
            Next

            Try
                CType(ogcsize.DataSource, DataTable).AcceptChanges()
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Private Function SaveData() As Boolean
        Dim ret As Boolean
        Dim _Str As String = ""
        Dim _FNHSysStyleDevId As Integer = 0
        Dim _StateNew As Boolean = False
        Try
            _Str = "SELECT TOP 1 FNHSysStyleDevId FROM "
            _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTDevelopStyle]  WITH(NOLOCK) "
            _Str &= vbCrLf & "    WHERE FTStyleDevCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleDevId.Text) & "' AND FTSeason='" & HI.UL.ULF.rpQuoted(FTSeason.Text) & "'  AND ISNULL(FNVersion,0)=" & FNVersion.Text & " AND FNBomDevType =" & FNBomDevType.SelectedIndex & "  "

            _FNHSysStyleDevId = Integer.Parse(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "0"))
            If _FNHSysStyleDevId <= 0 Then
                _FNHSysStyleDevId = HI.SE.RunID.GetRunNoID("TMERTDevelopStyle", "FNHSysStyleDevId", Conn.DB.DataBaseName.DB_MERCHAN)

                _StateNew = True
            End If

            Me.FNHSysStyleDevId.Properties.Tag = _FNHSysStyleDevId

            Dim mVersion As Integer = 0
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try
                _Str = "SELECT TOP 1 FNHSysStyleDevId FROM " &
                    "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTDevelopStyle] WHERE FNHSysStyleDevId = " & Val(Me.FNHSysStyleDevId.Properties.Tag.ToString) & " "
                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    ''Insert Header
                    Dim SelectCMD As SqlCommand = New SqlCommand(_Str, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                    'SelectCMD.Parameters.AddWithValue("@FNHSysStyleDevId", (Val(Me.FNHSysStyleDevId.Properties.Tag.ToString))

                    Dim cnt As Integer
                    cnt = SelectCMD.ExecuteScalar

                    If cnt = 0 Then

                        mVersion = 0
                        _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTDevelopStyle] "
                        _Str &= vbCrLf & " (FNHSysStyleDevId, FTStyleDevCode, FTStyleDevNameTH, FTStyleDevNameEN, FTSeason, FTNote, FNHSysCustId, "
                        _Str &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime,FTDevelopDate,FNHSysMSCId,FNVersion"
                        _Str &= vbCrLf & "  ,FNBomDevType,FTMSCCode,FTMSCLevel1,FTMSCLevel2,FTMSCLevel3,FTSilhouette) "

                        _Str &= vbCrLf & " SELECT " & _FNHSysStyleDevId & ", '" & HI.UL.ULF.rpQuoted(FNHSysStyleDevId.Text) & "', '" & HI.UL.ULF.rpQuoted(FTStyleDevNameTH.Text) & "' "
                        _Str &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(FTStyleDevNameEN.Text) & "', '" & HI.UL.ULF.rpQuoted(FTSeason.Text) & "', '" & HI.UL.ULF.rpQuoted(FTNote.Text) & "', " & Val(FNHSysCustId.Properties.Tag.ToString) & " "
                        _Str &= vbCrLf & ", N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Str &= vbCrLf & ", " & HI.UL.ULDate.FormatDateDB & ""
                        _Str &= vbCrLf & ", " & HI.UL.ULDate.FormatTimeDB & ""
                        _Str &= vbCrLf & ", N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Str &= vbCrLf & ", " & HI.UL.ULDate.FormatDateDB & ""
                        _Str &= vbCrLf & ", " & HI.UL.ULDate.FormatTimeDB
                        _Str &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(FTDevelopDate.Text) & "'"
                        _Str &= vbCrLf & "," & Val(FNHSysMSCId.Properties.Tag.ToString()) & ""
                        _Str &= vbCrLf & "," & mVersion & ""
                        _Str &= vbCrLf & "," & FNBomDevType.SelectedIndex & ""
                        _Str &= vbCrLf & ", N'" & HI.UL.ULF.rpQuoted(FNHSysMSCId.Text) & "'"
                        _Str &= vbCrLf & ", N'" & HI.UL.ULF.rpQuoted(FTMSCLevel1.Text.Trim) & "'"
                        _Str &= vbCrLf & ", N'" & HI.UL.ULF.rpQuoted(FTMSCLevel2.Text.Trim) & "'"
                        _Str &= vbCrLf & ", N'" & HI.UL.ULF.rpQuoted(FTMSCLevel3.Text.Trim) & "'"
                        _Str &= vbCrLf & ", N'" & HI.UL.ULF.rpQuoted(FTSilhouette.Text.Trim) & "'"

                        If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If

                    Else

                        mVersion = FNVersion.Text

                        '' Create Data Table before update new value

                        _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTDevelopStyle] SET " & vbCrLf &
                        "FTStyleDevCode = '" & HI.UL.ULF.rpQuoted(FNHSysStyleDevId.Text) & "', " & vbCrLf &
                        "FTNote = '" & HI.UL.ULF.rpQuoted(FTNote.Text) & "', " & vbCrLf &
                        "FTStyleDevNameTH = '" & HI.UL.ULF.rpQuoted(FTStyleDevNameTH.Text) & "', " & vbCrLf &
                        "FTStyleDevNameEN = '" & HI.UL.ULF.rpQuoted(FTStyleDevNameEN.Text) & "', " & vbCrLf &
                        "FNHSysCustId = '" & Val(FNHSysCustId.Properties.Tag.ToString) & "', " & vbCrLf &
                        "FTSeason = '" & HI.UL.ULF.rpQuoted(FTSeason.Text) & "', " & vbCrLf &
                        "FTUpdUser = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', " & vbCrLf &
                        "FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ", " & vbCrLf &
                        "FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & "," & vbCrLf &
                        "FTDevelopDate ='" & HI.UL.ULDate.ConvertEnDB(FTDevelopDate.Text) & "'" & vbCrLf &
                        ",FNHSysMSCId =" & Val(FNHSysMSCId.Properties.Tag.ToString()) & "" & vbCrLf &
                        ",FNVersion =" & mVersion & "" & vbCrLf &
                        ",FTMSCCode = '" & HI.UL.ULF.rpQuoted(FNHSysMSCId.Text) & "'" & vbCrLf &
                        ",FTMSCLevel1 = '" & HI.UL.ULF.rpQuoted(FTStyleDevNameTH.Text) & "' " & vbCrLf &
                        ",FTMSCLevel2 = '" & HI.UL.ULF.rpQuoted(FTStyleDevNameTH.Text) & "' " & vbCrLf &
                        ",FTMSCLevel3 = '" & HI.UL.ULF.rpQuoted(FTStyleDevNameTH.Text) & "' " & vbCrLf &
                        ",FTSilhouette = '" & HI.UL.ULF.rpQuoted(FTSilhouette.Text.Trim) & "' " & vbCrLf &
                        "WHERE FNHSysStyleDevId = " & _FNHSysStyleDevId

                        If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If

                    End If

                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    '' Create Data Table after update new value

                    ''Insert & Update Detail
                    ret = UpdateDatasource(_StateNew)

                End If

                Return ret

            Catch ex As Exception

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False

            End Try

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "Insert & Update Process"

    Private Function UpdateDatasource(Optional _StateNew As Boolean = False) As Boolean
        Dim ret As Boolean
        'Save the latest changes to the bound DataTable 
        View = Me.ogcmat.Views(0)
        View.ClearSorting()
        View.Columns("FNMerMatSeq").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
        View.Columns("FNPart").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

        CType(ogcmat.DataSource, DataTable).AcceptChanges()
        CType(ogccolor.DataSource, DataTable).AcceptChanges()
        CType(ogcsize.DataSource, DataTable).AcceptChanges()

        Dim dt1 As DataTable = CType(ogcmat.DataSource, DataTable)
        Dim dt2 As DataTable = CType(ogccolor.DataSource, DataTable)
        Dim dt3 As DataTable = CType(ogcsize.DataSource, DataTable)
        'If Not (View.PostEditor() And View.UpdateCurrentRow()) Then Return False
        Select Case Me.otb.SelectedTabPage.Name
            Case otpmatcode.Name
                ' Update Style detail.
                If IsNothing(dt1) Then
                    ret = True
                Else
                    ret = DoUpdateTable(oleDbDataAdapter2, dt1)
                    If ret = False Then Return ret
                End If

                Dim _Qry As String = ""

                _Qry = " DELETE FROM  C"
                _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_Mat AS A RIGHT OUTER JOIN"
                _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_ColorWay AS C ON A.FNHSysStyleDevId = C.FNHSysStyleDevId AND A.FNSeq = C.FNSeq"
                _Qry &= vbCrLf & "  WHERE   C.FNHSysStyleDevId=" & Integer.Parse(Val(FNHSysStyleDevId.Properties.Tag.ToString)) & " AND   (A.FNHSysStyleDevId Is NULL)"

                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                _Qry = " DELETE FROM  C"
                _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_Mat AS A RIGHT OUTER JOIN"
                _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_SizeBreakDown AS C ON A.FNHSysStyleDevId = C.FNHSysStyleDevId AND A.FNSeq = C.FNSeq"
                _Qry &= vbCrLf & "  WHERE   C.FNHSysStyleDevId=" & Integer.Parse(Val(FNHSysStyleDevId.Properties.Tag.ToString)) & " AND   (A.FNHSysStyleDevId Is NULL)"

                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                '_Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_Mat "
                '_Qry &= vbCrLf & "  SET FTPositionPartId = ''"
                '_Qry &= vbCrLf & " WHERE  (FNHSysStyleDevId  =" & Integer.Parse(Val(FNHSysStyleDevId.Properties.Tag.ToString)) & ")"
                'HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                'ไม่รีเฟรช หลังมีการปรับ diffpart เพื่อ save data row copy......20160503 
                'If _StateNew = False Then
                '    Call LoadColorwaySize(FNHSysStyleDevId.Properties.Tag)
                'End If

                dt2 = CType(ogccolor.DataSource, DataTable)
                dt3 = CType(ogcsize.DataSource, DataTable)

                ' Update Color way
                If IsNothing(dt2) Then
                    ret = True
                Else
                    ret = DoUpdateColor(oleDbDataAdapter2, dt2)
                    If ret = False Then Return ret
                End If

                ' Update Size Breakdown
                If IsNothing(dt3) Then
                    ret = True
                Else
                    ret = DoUpdateSize(oleDbDataAdapter2, dt3)
                    If ret = False Then Return ret
                End If

            Case otpmatcolor.Name
                ' Update Color way
                If IsNothing(dt2) Then
                    ret = True
                Else
                    ret = DoUpdateColor(oleDbDataAdapter2, dt2)
                    If ret = False Then Return ret
                End If

                Call ImportSize()

                dt3 = CType(ogcsize.DataSource, DataTable)

                ' Update Size Breakdown
                If IsNothing(dt3) Then
                    ret = True
                Else
                    ret = DoUpdateSize(oleDbDataAdapter2, dt3)
                    If ret = False Then Return ret
                End If
            Case otpmatsize.Name
                ' Update Size Breakdown
                If IsNothing(dt3) Then
                    ret = True
                Else
                    ret = DoUpdateSize(oleDbDataAdapter2, dt3)
                    If ret = False Then Return ret
                End If
        End Select

        If (FNHSysStyleDevId.Properties.Tag.ToString() <> "") Then
            Try
                Select Case Me.otb.SelectedTabPage.Name
                    Case otpmatcode.Name

                        Call LoadStyleDetail(FNHSysStyleDevId.Properties.Tag.ToString)
                        Call LoadColorwaySize(FNHSysStyleDevId.Properties.Tag.ToString)
                    Case otpmatcode.Name
                End Select

            Catch ex As Exception
            End Try
        End If

        Return ret

    End Function

    Private Function DoUpdateTable(ByVal dataAdapter As SqlDataAdapter,
        ByVal dataTable As System.Data.DataTable) As Boolean
        Dim InsertSql As String = ""
        Dim _Cmd As String = ""
        Dim dt As DataTable
        dt = dataTable 'CType(ogcmatcode.DataSource, DataTable)

        Try
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            dataAdapter = CreateAdapter(HI.Conn.SQLConn.Cnn)
            InsertSql = dataAdapter.InsertCommand.CommandText
            Dim InsertCMD As SqlCommand = New SqlCommand(dataAdapter.InsertCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
            Dim UpdateCmd As SqlCommand = New SqlCommand(dataAdapter.UpdateCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)

            Dim UpdateDate As String = "", UpdateTime As String = ""
            Dim StrSql As String = "SELECT " & HI.UL.ULDate.FormatDateDB & " AS UpdateDate, " & HI.UL.ULDate.FormatTimeDB & " AS UpdateTime"
            Dim dtx As DataTable
            dtx = HI.Conn.SQLConn.GetDataTable(StrSql, Conn.DB.DataBaseName.DB_MERCHAN)

            If dtx.Rows.Count > 0 Then
                For Each Rx As DataRow In dtx.Rows
                    UpdateDate = Rx!UpdateDate.ToString()
                    UpdateTime = Rx!UpdateTime.ToString()
                Next
            End If

            '' Create Data Table after update new value

            ' Add parameters and set values.
            If dt.Rows.Count = 0 Then Return True

            Dim _FNHSysStyleDevId As Integer = Integer.Parse(Val(FNHSysStyleDevId.Properties.Tag.ToString()))
            For Each r As DataRow In dt.Rows
                Dim SelectCMD As SqlCommand = New SqlCommand(dataAdapter.SelectCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                SelectCMD.Parameters.AddWithValue("@FNHSysStyleDevId", _FNHSysStyleDevId)
                SelectCMD.Parameters.AddWithValue("@FNSeq", (r.Item("FNSeq").ToString))
                Dim cnt As Integer
                cnt = SelectCMD.ExecuteScalar
                If cnt = 0 Then
                    If r.Item("FTItemNo").ToString <> "" Then

                        'InsertCMD.Parameters.AddWithValue("@FNHSysStyleDevId", _FNHSysStyleDevId)
                        'InsertCMD.Parameters.AddWithValue("@FNSeq", Val(r.Item("FNSeq").ToString))
                        'InsertCMD.Parameters.AddWithValue("@FNMerMatSeq", Val(r.Item("FNMerMatSeq").ToString))
                        'InsertCMD.Parameters.AddWithValue("@FTItemNo", HI.UL.ULF.rpQuoted(r.Item("FTItemNo").ToString))
                        'InsertCMD.Parameters.AddWithValue("@FNPart", Val(r.Item("FNPart").ToString))
                        'InsertCMD.Parameters.AddWithValue("@FTItemDesc", HI.UL.ULF.rpQuoted(r.Item("FNHSysMerMatId_None").ToString))
                        'InsertCMD.Parameters.AddWithValue("@FTPartNameEN", HI.UL.ULF.rpQuoted(r.Item("FTPartNameEN").ToString))
                        'InsertCMD.Parameters.AddWithValue("@FTPartNameTH", HI.UL.ULF.rpQuoted(r.Item("FTPartNameTH").ToString))
                        'InsertCMD.Parameters.AddWithValue("@FTSuplCode", HI.UL.ULF.rpQuoted(r.Item("FTSuplCode").ToString))
                        'InsertCMD.Parameters.AddWithValue("@FTStateNominate", r.Item("FTStateNominate").ToString)
                        'InsertCMD.Parameters.AddWithValue("@FTUnitCode", HI.UL.ULF.rpQuoted(r.Item("FTUnitCode").ToString))
                        'InsertCMD.Parameters.AddWithValue("@FNPrice", Val(r.Item("FNPrice").ToString))
                        'InsertCMD.Parameters.AddWithValue("@FNHSysCurId", Val(r.Item("FNHSysCurId").ToString))
                        'InsertCMD.Parameters.AddWithValue("@FNConSmp", Val(r.Item("FNConSmp").ToString))
                        'InsertCMD.Parameters.AddWithValue("@FNConSmpPlus", Val(r.Item("FNConSmpPlus").ToString))
                        'InsertCMD.Parameters.AddWithValue("@FTStateActive", r.Item("FTStateActive").ToString)
                        'InsertCMD.Parameters.AddWithValue("@FTStateCombination", r.Item("FTStateCombination").ToString)
                        'InsertCMD.Parameters.AddWithValue("@FTStateMainMaterial", r.Item("FTStateMainMaterial").ToString)
                        'InsertCMD.Parameters.AddWithValue("@FTInsUser", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))
                        'InsertCMD.Parameters.AddWithValue("@FDInsDate", UpdateDate)
                        'InsertCMD.Parameters.AddWithValue("@FTInsTime", UpdateTime)
                        'InsertCMD.Parameters.AddWithValue("@FTUpdUser", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))
                        'InsertCMD.Parameters.AddWithValue("@FDUpdDate", UpdateDate)
                        'InsertCMD.Parameters.AddWithValue("@FTUpdTime", UpdateTime)
                        'InsertCMD.Parameters.AddWithValue("@FTComponent", r.Item("FTComponent").ToString)
                        'InsertCMD.Parameters.AddWithValue("@FTStateFree", r.Item("FTStateFree").ToString)
                        'InsertCMD.Parameters.AddWithValue("@FTPart", r.Item("FTPart").ToString)

                        InsertCMD.Parameters.AddWithValue("@FNHSysStyleDevId", _FNHSysStyleDevId)
                        InsertCMD.Parameters.AddWithValue("@FNSeq", Val(r.Item("FNSeq").ToString))
                        InsertCMD.Parameters.AddWithValue("@FNMerMatSeq", Val(r.Item("FNMerMatSeq").ToString))
                        InsertCMD.Parameters.AddWithValue("@FTItemNo", HI.UL.ULF.rpQuoted(r.Item("FTItemNo").ToString))
                        InsertCMD.Parameters.AddWithValue("@FNPart", Val(r.Item("FNPart").ToString))
                        InsertCMD.Parameters.AddWithValue("@FTItemDesc", (r.Item("FNHSysMerMatId_None").ToString))
                        InsertCMD.Parameters.AddWithValue("@FTPartNameEN", (r.Item("FTPartNameEN").ToString))
                        InsertCMD.Parameters.AddWithValue("@FTPartNameTH", (r.Item("FTPartNameTH").ToString))
                        InsertCMD.Parameters.AddWithValue("@FTSuplCode", (r.Item("FTSuplCode").ToString))
                        InsertCMD.Parameters.AddWithValue("@FTStateNominate", r.Item("FTStateNominate").ToString)
                        InsertCMD.Parameters.AddWithValue("@FTUnitCode", (r.Item("FTUnitCode").ToString))
                        InsertCMD.Parameters.AddWithValue("@FNPrice", Val(r.Item("FNPrice").ToString))
                        InsertCMD.Parameters.AddWithValue("@FNHSysCurId", Val(r.Item("FNHSysCurId").ToString))
                        InsertCMD.Parameters.AddWithValue("@FNConSmp", Val(r.Item("FNConSmp").ToString))
                        InsertCMD.Parameters.AddWithValue("@FNConSmpPlus", Val(r.Item("FNConSmpPlus").ToString))
                        InsertCMD.Parameters.AddWithValue("@FTStateActive", r.Item("FTStateActive").ToString)
                        InsertCMD.Parameters.AddWithValue("@FTStateCombination", r.Item("FTStateCombination").ToString)
                        InsertCMD.Parameters.AddWithValue("@FTStateMainMaterial", r.Item("FTStateMainMaterial").ToString)
                        InsertCMD.Parameters.AddWithValue("@FTInsUser", (HI.ST.UserInfo.UserName))
                        InsertCMD.Parameters.AddWithValue("@FDInsDate", UpdateDate)
                        InsertCMD.Parameters.AddWithValue("@FTInsTime", UpdateTime)
                        InsertCMD.Parameters.AddWithValue("@FTUpdUser", (HI.ST.UserInfo.UserName))
                        InsertCMD.Parameters.AddWithValue("@FDUpdDate", UpdateDate)
                        InsertCMD.Parameters.AddWithValue("@FTUpdTime", UpdateTime)
                        InsertCMD.Parameters.AddWithValue("@FTComponent", r.Item("FTComponent").ToString)
                        InsertCMD.Parameters.AddWithValue("@FTStateFree", r.Item("FTStateFree").ToString)
                        InsertCMD.Parameters.AddWithValue("@FTPart", r.Item("FTPart").ToString)
                        InsertCMD.Parameters.AddWithValue("@FTStateNotShowBomSheet", r.Item("FTStateNotShowBomSheet").ToString)
                        InsertCMD.Parameters.AddWithValue("@FTStateLabel", r.Item("FTStateLabel").ToString)
                        InsertCMD.Parameters.AddWithValue("@FNOrderSetType", Val(r.Item("FNOrderSetType").ToString))
                        InsertCMD.Parameters.AddWithValue("@FTItemREfNo", r.Item("FTItemREfNo").ToString)
                        InsertCMD.Parameters.AddWithValue("@FTPositionPartId", r.Item("FTPositionPartId").ToString)
                        InsertCMD.Parameters.AddWithValue("@FTStateHemNotOptiplan", r.Item("FTStateHemNotOptiplan").ToString)
                        InsertCMD.Parameters.AddWithValue("@FNRepeatLengthCM", Val(r.Item("FNRepeatLengthCM").ToString))

                        If Val((((r.Item("FNRepeatLengthCM").ToString))).ToString) And CmSysUnitId <> 0 Then


                            If Integer.Parse(CmSysUnitId) <> Integer.Parse((Val(r.Item("FNHSysUnitId").ToString))) Then

                                Dim FNRepeatConvert As Decimal = 0.0

                                _Cmd = " SELECT      TOP 1   Convert(numeric(18,10),CASE WHEN FNRateFrom > 1 THEN FNRateTo/FNRateFrom  ELSE  FNRateFrom * FNRateTo END)  As  FNConvRatio "
                                _Cmd &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitConvert  WITH (NOLOCK) "
                                _Cmd &= vbCrLf & "  WHERE FNHSysUnitId =" & Integer.Parse((Val(r.Item("FNHSysUnitId").ToString))) & " "
                                _Cmd &= vbCrLf & "  And FNHSysUnitIdTo =" & Integer.Parse(CmSysUnitId) & " "

                                FNRepeatConvert = Val(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MASTER, "0"))

                                InsertCMD.Parameters.AddWithValue("@FNRepeatConvert", FNRepeatConvert)

                            Else

                                InsertCMD.Parameters.AddWithValue("@FNRepeatConvert", 1)
                            End If

                        Else
                            InsertCMD.Parameters.AddWithValue("@FNRepeatConvert", 0)

                        End If

                        InsertCMD.Parameters.AddWithValue("@FNPackPerCarton", Val((((r.Item("FNPackPerCarton").ToString))).ToString))


                        If Val((((r.Item("FNOrderSetType").ToString))).ToString) = 3 Then

                            InsertCMD.Parameters.AddWithValue("@FNConSmpSplit", Decimal.Parse(Format(Val(r.Item("FNConSmp").ToString) / 2.0, "0.0000")))

                        Else

                            InsertCMD.Parameters.AddWithValue("@FNConSmpSplit", Val((((r.Item("FNConSmp").ToString))).ToString))


                        End If



                        InsertCMD.CommandType = CommandType.Text
                        InsertCMD.ExecuteNonQuery()
                        InsertCMD.Parameters.Clear()

                    End If

                Else

                    'UpdateCmd.Parameters.AddWithValue("@FNHSysStyleDevId", _FNHSysStyleDevId)
                    'UpdateCmd.Parameters.AddWithValue("@FNSeq", Val(r.Item("FNSeq").ToString))
                    'UpdateCmd.Parameters.AddWithValue("@FNMerMatSeq", Val(r.Item("FNMerMatSeq").ToString))
                    'UpdateCmd.Parameters.AddWithValue("@FTItemNo", HI.UL.ULF.rpQuoted(r.Item("FTItemNo").ToString))
                    'UpdateCmd.Parameters.AddWithValue("@FNPart", Val(r.Item("FNPart").ToString))
                    'UpdateCmd.Parameters.AddWithValue("@FTItemDesc", HI.UL.ULF.rpQuoted(r.Item("FNHSysMerMatId_None").ToString))
                    'UpdateCmd.Parameters.AddWithValue("@FTPartNameEN", HI.UL.ULF.rpQuoted(r.Item("FTPartNameEN").ToString))
                    'UpdateCmd.Parameters.AddWithValue("@FTPartNameTH", HI.UL.ULF.rpQuoted(r.Item("FTPartNameTH").ToString))
                    'UpdateCmd.Parameters.AddWithValue("@FTSuplCode", HI.UL.ULF.rpQuoted(r.Item("FTSuplCode").ToString))
                    'UpdateCmd.Parameters.AddWithValue("@FTStateNominate", r.Item("FTStateNominate").ToString)
                    'UpdateCmd.Parameters.AddWithValue("@FTUnitCode", HI.UL.ULF.rpQuoted(r.Item("FTUnitCode").ToString))
                    'UpdateCmd.Parameters.AddWithValue("@FNPrice", Val(r.Item("FNPrice").ToString))
                    'UpdateCmd.Parameters.AddWithValue("@FNHSysCurId", Val(r.Item("FNHSysCurId").ToString))
                    'UpdateCmd.Parameters.AddWithValue("@FNConSmp", Val(r.Item("FNConSmp").ToString))
                    'UpdateCmd.Parameters.AddWithValue("@FNConSmpPlus", Val(r.Item("FNConSmpPlus").ToString))
                    'UpdateCmd.Parameters.AddWithValue("@FTStateActive", r.Item("FTStateActive").ToString)
                    'UpdateCmd.Parameters.AddWithValue("@FTStateCombination", r.Item("FTStateCombination").ToString)
                    'UpdateCmd.Parameters.AddWithValue("@FTStateMainMaterial", r.Item("FTStateMainMaterial").ToString)
                    'UpdateCmd.Parameters.AddWithValue("@FTInsUser", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))
                    'UpdateCmd.Parameters.AddWithValue("@FDInsDate", UpdateDate)
                    'UpdateCmd.Parameters.AddWithValue("@FTInsTime", UpdateTime)
                    'UpdateCmd.Parameters.AddWithValue("@FTUpdUser", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))
                    'UpdateCmd.Parameters.AddWithValue("@FDUpdDate", UpdateDate)
                    'UpdateCmd.Parameters.AddWithValue("@FTUpdTime", UpdateTime)
                    'UpdateCmd.Parameters.AddWithValue("@FTComponent", r.Item("FTComponent").ToString)
                    'UpdateCmd.Parameters.AddWithValue("@FTStateFree", r.Item("FTStateFree").ToString)
                    'UpdateCmd.Parameters.AddWithValue("@FTPart", r.Item("FTPart").ToString)
                    UpdateCmd.Parameters.AddWithValue("@FNHSysStyleDevId", _FNHSysStyleDevId)
                    UpdateCmd.Parameters.AddWithValue("@FNSeq", Val(r.Item("FNSeq").ToString))
                    UpdateCmd.Parameters.AddWithValue("@FNMerMatSeq", Val(r.Item("FNMerMatSeq").ToString))
                    UpdateCmd.Parameters.AddWithValue("@FTItemNo", HI.UL.ULF.rpQuoted(r.Item("FTItemNo").ToString))
                    UpdateCmd.Parameters.AddWithValue("@FNPart", Val(r.Item("FNPart").ToString))
                    UpdateCmd.Parameters.AddWithValue("@FTItemDesc", (r.Item("FNHSysMerMatId_None").ToString))
                    UpdateCmd.Parameters.AddWithValue("@FTPartNameEN", (r.Item("FTPartNameEN").ToString))
                    UpdateCmd.Parameters.AddWithValue("@FTPartNameTH", (r.Item("FTPartNameTH").ToString))
                    UpdateCmd.Parameters.AddWithValue("@FTSuplCode", (r.Item("FTSuplCode").ToString))
                    UpdateCmd.Parameters.AddWithValue("@FTStateNominate", r.Item("FTStateNominate").ToString)
                    UpdateCmd.Parameters.AddWithValue("@FTUnitCode", (r.Item("FTUnitCode").ToString))
                    UpdateCmd.Parameters.AddWithValue("@FNPrice", Val(r.Item("FNPrice").ToString))
                    UpdateCmd.Parameters.AddWithValue("@FNHSysCurId", Val(r.Item("FNHSysCurId").ToString))
                    UpdateCmd.Parameters.AddWithValue("@FNConSmp", Val(r.Item("FNConSmp").ToString))
                    UpdateCmd.Parameters.AddWithValue("@FNConSmpPlus", Val(r.Item("FNConSmpPlus").ToString))
                    UpdateCmd.Parameters.AddWithValue("@FTStateActive", r.Item("FTStateActive").ToString)
                    UpdateCmd.Parameters.AddWithValue("@FTStateCombination", r.Item("FTStateCombination").ToString)
                    UpdateCmd.Parameters.AddWithValue("@FTStateMainMaterial", r.Item("FTStateMainMaterial").ToString)
                    UpdateCmd.Parameters.AddWithValue("@FTInsUser", (HI.ST.UserInfo.UserName))
                    UpdateCmd.Parameters.AddWithValue("@FDInsDate", UpdateDate)
                    UpdateCmd.Parameters.AddWithValue("@FTInsTime", UpdateTime)
                    UpdateCmd.Parameters.AddWithValue("@FTUpdUser", (HI.ST.UserInfo.UserName))
                    UpdateCmd.Parameters.AddWithValue("@FDUpdDate", UpdateDate)
                    UpdateCmd.Parameters.AddWithValue("@FTUpdTime", UpdateTime)
                    UpdateCmd.Parameters.AddWithValue("@FTComponent", r.Item("FTComponent").ToString)
                    UpdateCmd.Parameters.AddWithValue("@FTStateFree", r.Item("FTStateFree").ToString)
                    UpdateCmd.Parameters.AddWithValue("@FTPart", r.Item("FTPart").ToString)
                    UpdateCmd.Parameters.AddWithValue("@FTStateNotShowBomSheet", r.Item("FTStateNotShowBomSheet").ToString)
                    UpdateCmd.Parameters.AddWithValue("@FTStateLabel", r.Item("FTStateLabel").ToString)
                    UpdateCmd.Parameters.AddWithValue("@FNOrderSetType", Val(r.Item("FNOrderSetType").ToString))
                    UpdateCmd.Parameters.AddWithValue("@FTItemREfNo", r.Item("FTItemREfNo").ToString)
                    UpdateCmd.Parameters.AddWithValue("@FTPositionPartId", r.Item("FTPositionPartId").ToString)

                    UpdateCmd.Parameters.AddWithValue("@FTStateHemNotOptiplan", r.Item("FTStateHemNotOptiplan").ToString)
                    UpdateCmd.Parameters.AddWithValue("@FNRepeatLengthCM", Val(r.Item("FNRepeatLengthCM").ToString))

                    If Val((((r.Item("FNRepeatLengthCM").ToString))).ToString) And CmSysUnitId <> 0 Then


                        If Integer.Parse(CmSysUnitId) <> Integer.Parse((Val(r.Item("FNHSysUnitId").ToString))) Then

                            Dim pRepeatConvert As Decimal = 0.0

                            _Cmd = " SELECT      TOP 1   Convert(numeric(18,10),CASE WHEN FNRateFrom > 1 THEN FNRateTo/FNRateFrom  ELSE  FNRateFrom * FNRateTo END)  As  FNConvRatio "
                            _Cmd &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitConvert  WITH (NOLOCK) "
                            _Cmd &= vbCrLf & "  WHERE FNHSysUnitId =" & Integer.Parse((Val(r.Item("FNHSysUnitId").ToString))) & " "
                            _Cmd &= vbCrLf & "  And FNHSysUnitIdTo =" & Integer.Parse(CmSysUnitId) & " "

                            pRepeatConvert = Val(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MASTER, "0"))

                            UpdateCmd.Parameters.AddWithValue("@FNRepeatConvert", pRepeatConvert)

                        Else

                            UpdateCmd.Parameters.AddWithValue("@FNRepeatConvert", 1)
                        End If

                    Else
                        UpdateCmd.Parameters.AddWithValue("@FNRepeatConvert", 0)

                    End If

                    UpdateCmd.Parameters.AddWithValue("@FNPackPerCarton", Val((((r.Item("FNPackPerCarton").ToString))).ToString))


                    If Val((((r.Item("FNOrderSetType").ToString))).ToString) = 3 Then

                        UpdateCmd.Parameters.AddWithValue("@FNConSmpSplit", Decimal.Parse(Format(Val(r.Item("FNConSmp").ToString) / 2.0, "0.0000")))

                    Else

                        UpdateCmd.Parameters.AddWithValue("@FNConSmpSplit", Val((((r.Item("FNConSmp").ToString))).ToString))


                    End If


                    UpdateCmd.CommandType = CommandType.Text
                    UpdateCmd.ExecuteNonQuery()
                    UpdateCmd.Parameters.Clear()

                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            '' Create Data Table after update new value

            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            MessageBox.Show(ex.Message)
            Return False
        End Try
    End Function

    Private Function DoUpdateColor(ByVal dataAdapter As SqlDataAdapter,
        ByVal dataTable As System.Data.DataTable) As Boolean
        Dim InsertSql As String = ""
        Dim _FNHSysStyleDevId As Integer = Integer.Parse(Val(FNHSysStyleDevId.Properties.Tag.ToString()))
        Try
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim dt As DataTable
            dt = dataTable 'CType(ogcstylecolor.DataSource, DataTable)

            dataAdapter = CreateAdapterColor(HI.Conn.SQLConn.Cnn)
            InsertSql = dataAdapter.InsertCommand.CommandText
            Dim InsertCMD As SqlCommand = New SqlCommand(dataAdapter.InsertCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
            Dim UpdateCmd As SqlCommand = New SqlCommand(dataAdapter.UpdateCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)

            Dim UpdateDate As String = "", UpdateTime As String = ""
            Dim StrSql As String = "SELECT " & HI.UL.ULDate.FormatDateDB & " AS UpdateDate, " & HI.UL.ULDate.FormatTimeDB & " AS UpdateTime"
            Dim dtx As DataTable
            dtx = HI.Conn.SQLConn.GetDataTableOnbeginTrans(StrSql, Conn.DB.DataBaseName.DB_MERCHAN)

            If dtx.Rows.Count > 0 Then
                For Each Rx As DataRow In dtx.Rows
                    UpdateDate = Rx!UpdateDate.ToString()
                    UpdateTime = Rx!UpdateTime.ToString()
                Next
            End If

            Dim Views As GridView = New GridView()
            Views = ogccolor.Views(0)

            '' Create Data Table after update new value

            ' Add parameters and set values.
            Dim dttmpColorway As New DataTable
            dttmpColorway.Columns.Add("FNHSysMatColorId", GetType(String))
            dttmpColorway.Columns.Add("FNColorWaySeq", GetType(Integer))

            If dt.Rows.Count = 0 Then Return True
            For Each r As DataRow In dt.Rows
                Dim SelectCMD As SqlCommand = New SqlCommand(dataAdapter.SelectCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                Dim CIndex As Integer = 0
                Dim Cols As Integer = dt.Columns.Count
                Dim Colx As Integer = 0
                Dim ColorWay As String = ""
                For Each c As DataColumn In dt.Columns
                    Colx += 1


                    Select Case c.ColumnName.ToString.ToLower
                        Case "FNHSysStyleDevId".ToLower, "FNSeq".ToLower, "FNMerMatSeq".ToLower, "FTItemNo".ToLower,
                            "FTMainMatName".ToLower, "FNPart".ToLower, "FTPositionPartName".ToLower, "FNConSmp".ToLower, "FNConSmpPlus".ToLower,
                             "FNColorWaySeq".ToLower, "FTRunColor".ToLower, "FNOrderSetType".ToLower
                        Case Else
                            If (Microsoft.VisualBasic.Left(c.ColumnName.ToString, "FTRawMatColorCode".Length)).ToUpper = "FTRawMatColorCode".ToUpper Then

                                If (Microsoft.VisualBasic.Left(c.ColumnName.ToString, ("FTRawMatColorCode").Length)).ToUpper = ("FTRawMatColorCode").ToUpper Then

                                    CIndex += 1
                                    SelectCMD.Parameters.AddWithValue("@FNHSysStyleDevId", _FNHSysStyleDevId)
                                    SelectCMD.Parameters.AddWithValue("@FNSeq", r.Item("FNSeq").ToString)
                                    SelectCMD.Parameters.AddWithValue("@FNColorWaySeq", CIndex)

                                    ColorWay = c.ColumnName.ToString.Replace("FTRawMatColorCode", "")
                                    Dim cnt As Integer
                                    cnt = SelectCMD.ExecuteScalar
                                    If cnt = 0 Then

                                        If dttmpColorway.Select("FNHSysMatColorId='" & HI.UL.ULF.rpQuoted(ColorWay) & "'").Length <= 0 Then
                                            dttmpColorway.Rows.Add(ColorWay, CIndex)
                                        End If

                                        InsertCMD.Parameters.AddWithValue("@FNHSysStyleDevId", _FNHSysStyleDevId)
                                        InsertCMD.Parameters.AddWithValue("@FNSeq", r.Item("FNSeq").ToString)
                                        InsertCMD.Parameters.AddWithValue("@FNMerMatSeq", CDbl(r.Item("FNMerMatSeq").ToString))
                                        InsertCMD.Parameters.AddWithValue("@FTRunColor", r.Item("FTRunColor").ToString)
                                        InsertCMD.Parameters.AddWithValue("@FNColorWaySeq", CIndex)
                                        InsertCMD.Parameters.AddWithValue("@FTColorCode", r.Item(c.ColumnName.ToString).ToString)
                                        InsertCMD.Parameters.AddWithValue("@FTColorWay", ColorWay)
                                        InsertCMD.Parameters.AddWithValue("@FTColorNameTH", r.Item("FTRawMatColorNameTH" & ColorWay).ToString)
                                        InsertCMD.Parameters.AddWithValue("@FTColorNameEN", r.Item("FTRawMatColorNameEN" & ColorWay).ToString)
                                        InsertCMD.Parameters.AddWithValue("@FTInsUser", (HI.ST.UserInfo.UserName))
                                        InsertCMD.Parameters.AddWithValue("@FDInsDate", UpdateDate)
                                        InsertCMD.Parameters.AddWithValue("@FTInsTime", UpdateTime)
                                        InsertCMD.Parameters.AddWithValue("@FTUpdUser", (HI.ST.UserInfo.UserName))
                                        InsertCMD.Parameters.AddWithValue("@FDUpdDate", UpdateDate)
                                        InsertCMD.Parameters.AddWithValue("@FTUpdTime", UpdateTime)

                                        InsertCMD.CommandType = CommandType.Text
                                        InsertCMD.ExecuteNonQuery()
                                        InsertCMD.Parameters.Clear()
                                    Else

                                        If dttmpColorway.Select("FNHSysMatColorId='" & HI.UL.ULF.rpQuoted(ColorWay) & "'").Length <= 0 Then
                                            dttmpColorway.Rows.Add(ColorWay, CIndex)
                                        End If

                                        UpdateCmd.Parameters.AddWithValue("@FNHSysStyleDevId", _FNHSysStyleDevId)
                                        UpdateCmd.Parameters.AddWithValue("@FNSeq", r.Item("FNSeq").ToString)
                                        UpdateCmd.Parameters.AddWithValue("@FNMerMatSeq", CDbl(r.Item("FNMerMatSeq").ToString))
                                        UpdateCmd.Parameters.AddWithValue("@FTRunColor", r.Item("FTRunColor").ToString)
                                        UpdateCmd.Parameters.AddWithValue("@FTColorNameTH", r.Item("FTRawMatColorNameTH" & ColorWay).ToString)
                                        UpdateCmd.Parameters.AddWithValue("@FTColorNameEN", r.Item("FTRawMatColorNameEN" & ColorWay).ToString)
                                        UpdateCmd.Parameters.AddWithValue("@FNColorWaySeq", CIndex)
                                        UpdateCmd.Parameters.AddWithValue("@FTColorCode", r.Item(c.ColumnName.ToString).ToString)
                                        UpdateCmd.Parameters.AddWithValue("@FTColorWay", ColorWay)
                                        UpdateCmd.Parameters.AddWithValue("@FTUpdUser", (HI.ST.UserInfo.UserName))
                                        UpdateCmd.Parameters.AddWithValue("@FDUpdDate", UpdateDate)
                                        UpdateCmd.Parameters.AddWithValue("@FTUpdTime", UpdateTime)

                                        UpdateCmd.CommandType = CommandType.Text
                                        UpdateCmd.ExecuteNonQuery()
                                        UpdateCmd.Parameters.Clear()
                                    End If

                                    SelectCMD.Parameters.Clear()

                                End If
                            End If
                    End Select


                Next

            Next

            Dim _Qry As String = ""
            If dttmpColorway.Rows.Count > 0 Then

                _Qry = " DELETE  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_ColorWay "
                _Qry &= vbCrLf & " WHERE FNHSysStyleDevId=" & _FNHSysStyleDevId & " "
                _Qry &= vbCrLf & " AND NOT( Convert(varchar(30),FTColorWay)+'|'+ Convert(varchar(30),FNColorWaySeq) IN (''"

                For Each R As DataRow In dttmpColorway.Rows
                    _Qry &= vbCrLf & ",'" & R!FNHSysMatColorId.ToString & "|" & R!FNColorWaySeq.ToString & "'"
                Next

                _Qry &= vbCrLf & " ))"
                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            Else
                _Qry = " DELETE  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_ColorWay "
                _Qry &= vbCrLf & " WHERE FNHSysStyleDevId=" & _FNHSysStyleDevId & " "

                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
            End If

            dttmpColorway.Dispose()
            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            '' Create Data Table after update new value

            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            MessageBox.Show(ex.Message)
            Return False
        End Try
    End Function

    Private Function DoUpdateSize(ByVal dataAdapter As SqlDataAdapter,
        ByVal dataTable As System.Data.DataTable) As Boolean
        Dim InsertSql As String = ""
        Dim Views As GridView = New GridView()

        Dim _FNHSysStyleDevId As Integer = Integer.Parse(Val(FNHSysStyleDevId.Properties.Tag.ToString()))

        Try
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim dt As DataTable
            dt = dataTable 'CType(ogcstylecolor.DataSource, DataTable)

            dataAdapter = CreateAdapterSize(HI.Conn.SQLConn.Cnn)
            InsertSql = dataAdapter.InsertCommand.CommandText
            Dim InsertCMD As SqlCommand = New SqlCommand(dataAdapter.InsertCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
            Dim UpdateCmd As SqlCommand = New SqlCommand(dataAdapter.UpdateCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)

            Dim UpdateDate As String = "", UpdateTime As String = ""
            Dim StrSql As String = "SELECT " & HI.UL.ULDate.FormatDateDB & " AS UpdateDate, " & HI.UL.ULDate.FormatTimeDB & " AS UpdateTime"
            Dim dtx As DataTable
            dtx = HI.Conn.SQLConn.GetDataTableOnbeginTrans(StrSql, Conn.DB.DataBaseName.DB_MERCHAN)

            If dtx.Rows.Count > 0 Then
                For Each Rx As DataRow In dtx.Rows
                    UpdateDate = Rx!UpdateDate.ToString()
                    UpdateTime = Rx!UpdateTime.ToString()
                Next
            End If

            Views = ogcsize.Views(0)

            '' Create Data Table after update new value

            Dim dttmpColorway As New DataTable
            dttmpColorway.Columns.Add("FNHSysMatSizeId", GetType(String))
            dttmpColorway.Columns.Add("FNSieBreakDownSeq", GetType(Integer))

            Dim _SizeBreakdown As String = ""
            ' Add parameters and set values.
            If dt.Rows.Count = 0 Then Return True
            For Each r As DataRow In dt.Rows
                Dim SelectCMD As SqlCommand = New SqlCommand(dataAdapter.SelectCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                Dim CIndex As Integer = 0
                Dim Cols As Integer = dt.Columns.Count
                Dim Colx As Integer = 0
                For Each c As DataColumn In dt.Columns
                    Colx += 1
                    Select Case c.ColumnName.ToString.ToLower
                        Case "FNHSysStyleDevId".ToLower, "FNSeq".ToLower, "FNMerMatSeq".ToLower, "FTItemNo".ToLower,
                            "FTMainMatName".ToLower, "FNPart".ToLower, "FTPositionPartName".ToLower, "FNConSmp".ToLower, "FNConSmpPlus".ToLower,
                             "FNColorWaySeq".ToLower, "FTRunColor".ToLower, "FNOrderSetType".ToLower
                        Case Else

                            If (Microsoft.VisualBasic.Left(c.ColumnName.ToString, "FTRawMatSizeCode".Length)).ToUpper = "FTRawMatSizeCode".ToUpper Then
                                CIndex += 1
                                SelectCMD.Parameters.AddWithValue("@FNHSysStyleDevId", _FNHSysStyleDevId)
                                SelectCMD.Parameters.AddWithValue("@FNSeq", r.Item("FNSeq").ToString)
                                SelectCMD.Parameters.AddWithValue("@FNSieBreakDownSeq", CIndex)

                                _SizeBreakdown = c.ColumnName.ToString.Replace("FTRawMatSizeCode", "")

                                Dim cnt As Integer
                                cnt = SelectCMD.ExecuteScalar
                                If cnt = 0 Then

                                    If dttmpColorway.Select("FNHSysMatSizeId='" & HI.UL.ULF.rpQuoted(_SizeBreakdown) & "'").Length <= 0 Then
                                        dttmpColorway.Rows.Add(_SizeBreakdown, CIndex)
                                    End If

                                    InsertCMD.Parameters.AddWithValue("@FNHSysStyleDevId", _FNHSysStyleDevId)
                                    InsertCMD.Parameters.AddWithValue("@FNSeq", r.Item("FNSeq").ToString)
                                    InsertCMD.Parameters.AddWithValue("@FNMerMatSeq", CDbl(r.Item("FNMerMatSeq").ToString))

                                    InsertCMD.Parameters.AddWithValue("@FTRunSize", r.Item("FTRunSize").ToString)
                                    InsertCMD.Parameters.AddWithValue("@FNSieBreakDownSeq", CIndex)
                                    InsertCMD.Parameters.AddWithValue("@FTSizeCode", r.Item(c.ColumnName.ToString).ToString)
                                    InsertCMD.Parameters.AddWithValue("@FTSizeBreakDown", _SizeBreakdown)

                                    InsertCMD.Parameters.AddWithValue("@FTInsUser", (HI.ST.UserInfo.UserName))
                                    InsertCMD.Parameters.AddWithValue("@FDInsDate", UpdateDate)
                                    InsertCMD.Parameters.AddWithValue("@FTInsTime", UpdateTime)
                                    InsertCMD.Parameters.AddWithValue("@FTUpdUser", (HI.ST.UserInfo.UserName))
                                    InsertCMD.Parameters.AddWithValue("@FDUpdDate", UpdateDate)
                                    InsertCMD.Parameters.AddWithValue("@FTUpdTime", UpdateTime)

                                    InsertCMD.CommandType = CommandType.Text
                                    InsertCMD.ExecuteNonQuery()
                                    InsertCMD.Parameters.Clear()
                                Else


                                    If dttmpColorway.Select("FNHSysMatSizeId='" & HI.UL.ULF.rpQuoted(_SizeBreakdown) & "'").Length <= 0 Then
                                        dttmpColorway.Rows.Add(_SizeBreakdown, CIndex)
                                    End If

                                    UpdateCmd.Parameters.AddWithValue("@FNHSysStyleDevId", _FNHSysStyleDevId)
                                    UpdateCmd.Parameters.AddWithValue("@FNSeq", r.Item("FNSeq").ToString)
                                    UpdateCmd.Parameters.AddWithValue("@FNMerMatSeq", CDbl(r.Item("FNMerMatSeq").ToString))

                                    UpdateCmd.Parameters.AddWithValue("@FTRunSize", r.Item("FTRunSize").ToString)
                                    UpdateCmd.Parameters.AddWithValue("@FNSieBreakDownSeq", CIndex)
                                    UpdateCmd.Parameters.AddWithValue("@FTSizeCode", r.Item(c.ColumnName.ToString).ToString)
                                    UpdateCmd.Parameters.AddWithValue("@FTSizeBreakDown", _SizeBreakdown)

                                    UpdateCmd.Parameters.AddWithValue("@FTUpdUser", (HI.ST.UserInfo.UserName))
                                    UpdateCmd.Parameters.AddWithValue("@FDUpdDate", UpdateDate)
                                    UpdateCmd.Parameters.AddWithValue("@FTUpdTime", UpdateTime)

                                    UpdateCmd.CommandType = CommandType.Text
                                    UpdateCmd.ExecuteNonQuery()
                                    UpdateCmd.Parameters.Clear()
                                End If

                                SelectCMD.Parameters.Clear()

                            End If
                    End Select

                Next
            Next


            Dim _Qry As String = ""
            If dttmpColorway.Rows.Count > 0 Then

                _Qry = " DELETE  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_SizeBreakDown "
                _Qry &= vbCrLf & " WHERE FNHSysStyleDevId=" & _FNHSysStyleDevId & " "

                _Qry &= vbCrLf & " AND NOT( Convert(nvarchar(30),FTSizeBreakDown)+'|'+Convert(nvarchar(30),FNSieBreakDownSeq) IN (''"

                For Each R As DataRow In dttmpColorway.Rows
                    _Qry &= vbCrLf & ",'" & R!FNHSysMatSizeId.ToString & "|" & R!FNSieBreakDownSeq.ToString & "'"
                Next

                _Qry &= vbCrLf & " ))"
                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            Else
                _Qry = " DELETE  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_SizeBreakDown "
                _Qry &= vbCrLf & " WHERE FNHSysStyleDevId=" & _FNHSysStyleDevId & " "

                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
            End If

            dttmpColorway.Dispose()

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            '' Create Data Table after update new value

            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            MessageBox.Show(ex.Message)
            Return False
        End Try
    End Function

#End Region

#Region "Delete Process"

    Private Function DeleteAllData(ByVal objspl As HI.TL.SplashScreen) As Boolean
        Try

            Dim _Dt As DataTable = CType(ogcmat.DataSource, DataTable)
            Dim _Str As String = ""
            Dim tEDateExec As String = ""
            Dim tDateNext As String = ""


            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            objspl.UpdateInformation("Deleting.... Style No " & FNHSysStyleDevId.Text)

            '' Delete Color
            _Str = " DELETE  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_ColorWay "
            _Str &= vbCrLf & " WHERE FNHSysStyleDevId =" & Val(FNHSysStyleDevId.Properties.Tag.ToString())
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            '' Delete Size
            _Str = " DELETE  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_SizeBreakDown "
            _Str &= vbCrLf & " WHERE FNHSysStyleDevId =" & Val(FNHSysStyleDevId.Properties.Tag.ToString())
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            '' Delete detail  
            _Str = " DELETE  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_Mat "
            _Str &= vbCrLf & " WHERE FNHSysStyleDevId =" & Val(FNHSysStyleDevId.Properties.Tag.ToString())
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            '' Delete Header
            _Str = " DELETE  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle "
            _Str &= vbCrLf & " WHERE FNHSysStyleDevId =" & Val(FNHSysStyleDevId.Properties.Tag.ToString()) & " "

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

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

    Private Function DoDeleteSource(ByVal dataAdapter As SqlDataAdapter,
    ByVal dataTable As System.Data.DataTable, r As Integer, ByVal TableIndexx As Integer) As Boolean

        If dataTable.Rows.Count = 0 Then Return True

        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
        Try
            If TableIndexx = 0 Then
                dataAdapter = CreateAdapter(HI.Conn.SQLConn.Cnn)
            ElseIf TableIndexx = 1 Then
                dataAdapter = CreateAdapterColor(HI.Conn.SQLConn.Cnn)
            Else
                dataAdapter = CreateAdapterSize(HI.Conn.SQLConn.Cnn)
            End If

            Dim SelectCMD As SqlCommand = New SqlCommand(dataAdapter.SelectCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
            SelectCMD.Parameters.AddWithValue("@FNHSysStyleDevId", Val(dataTable.Rows(r).Item("FNHSysStyleDevId").ToString))
            SelectCMD.Parameters.AddWithValue("@FNSeq", dataTable.Rows(r).Item("FNSeq").ToString)

            Dim cnt As Integer
            cnt = SelectCMD.ExecuteScalar
            If cnt = 0 Then
                If dataTable.Rows(r).RowState <> DataRowState.Deleted Then
                    dataTable.Rows(r).Delete()
                End If

                dataTable.AcceptChanges()

                If TableIndexx = 0 Then
                    ogcmat.DataSource = dataTable
                    ogcmat.Refresh()
                ElseIf TableIndexx = 1 Then
                    ogccolor.DataSource = dataTable
                    ogccolor.Refresh()
                Else
                    ogcsize.DataSource = dataTable
                    ogcsize.Refresh()
                End If

                Return True
            End If

            Dim DeleteCmd As SqlCommand = New SqlCommand(dataAdapter.DeleteCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)

            Try
                DeleteCmd.Parameters.AddWithValue("@FNHSysStyleDevId", dataTable.Rows(r).Item("FNHSysStyleDevId").ToString)
                DeleteCmd.Parameters.AddWithValue("@FNSeq", dataTable.Rows(r).Item("FNSeq").ToString)

                DeleteCmd.CommandType = CommandType.Text
                DeleteCmd.ExecuteNonQuery()
                DeleteCmd.Parameters.Clear()

                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                If dataTable.Rows(r).RowState <> DataRowState.Deleted Then
                    dataTable.Rows(r).Delete()
                End If

                dataTable.AcceptChanges()

                If TableIndexx = 0 Then
                    ogcmat.DataSource = dataTable
                    ogcmat.Refresh()
                ElseIf TableIndexx = 1 Then
                    ogccolor.DataSource = dataTable
                    ogccolor.Refresh()
                Else
                    ogcsize.DataSource = dataTable
                    ogcsize.Refresh()
                End If

                Return True

            Catch ex As Exception
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                MessageBox.Show(ex.Message)
                Return False
            End Try
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

#End Region

#Region "Create Adapter"
    Public Function CreateAdapter(
    ByVal connection As SqlConnection) As SqlDataAdapter

        Dim adapter As SqlDataAdapter = New SqlDataAdapter()

        ' Create the SelectCommand. 
        Dim command As SqlCommand = New SqlCommand(
            "SELECT FNHSysStyleDevId, FNSeq, FNMerMatSeq, FTItemNo, FNPart,FTItemDesc FROM " &
            "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTDevelopStyle_Mat] " &
            "WHERE FNHSysStyleDevId = @FNHSysStyleDevId AND FNSeq = @FNSeq", connection)

        ' Add the parameters for the SelectCommand.
        command.Parameters.Add("@FNHSysStyleDevId", SqlDbType.Int)
        command.Parameters.Add("@FNSeq", SqlDbType.Int)

        adapter.SelectCommand = command

        ' Create the InsertCommand.
        command = New SqlCommand(
            "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTDevelopStyle_Mat] (" &
            " FNHSysStyleDevId, FNSeq, FNMerMatSeq " &
            ", FTItemNo, FTItemDesc, FNPart, FTPartNameEN, FTPartNameTH, FTSuplCode, FTStateNominate " &
            ", FTUnitCode, FNPrice, FNHSysCurId, FNConSmp, FNConSmpPlus, FTComponent " &
            ", FTStateActive, FTStateCombination, FTStateMainMaterial, FTStateFree,FTPart " &
            ",[FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser],[FDUpdDate],[FTUpdTime],[FTStateNotShowBomSheet],[FTStateLabel],[FNOrderSetType],[FTItemREfNo],[FTPositionPartId],FTStateHemNotOptiplan,FNRepeatLengthCM,FNRepeatConvert,FNPackPerCarton,FNConSmpSplit) " &
            "VALUES ( " &
            " @FNHSysStyleDevId, @FNSeq, @FNMerMatSeq " &
            ", @FTItemNo, @FTItemDesc, @FNPart, @FTPartNameEN, @FTPartNameTH, @FTSuplCode, @FTStateNominate " &
            ", @FTUnitCode, @FNPrice, @FNHSysCurId, @FNConSmp, @FNConSmpPlus, @FTComponent " &
            ", @FTStateActive, @FTStateCombination, @FTStateMainMaterial, @FTStateFree,@FTPart " &
            ",@FTInsUser, @FDInsDate, @FTInsTime, @FTUpdUser, @FDUpdDate, @FTUpdTime,@FTStateNotShowBomSheet,@FTStateLabel,@FNOrderSetType,@FTItemREfNo,@FTPositionPartId,@FTStateHemNotOptiplan,@FNRepeatLengthCM,@FNRepeatConvert,@FNPackPerCarton,@FNConSmpSplit)", connection)

        ' Add the parameters for the InsertCommand.
        command.Parameters.Add("@FNHSysStyleDevId", SqlDbType.Int, 8, "FNHSysStyleDevId")
        command.Parameters.Add("@FNSeq", SqlDbType.Int, 8, "FNSeq")
        command.Parameters.Add("@FNMerMatSeq", SqlDbType.Decimal, 5, "FNMerMatSeq")
        command.Parameters.Add("@FTItemNo", SqlDbType.NChar, 30, "FTItemNo")
        command.Parameters.Add("@FNPart", SqlDbType.Int, 8, "FNPart")
        command.Parameters.Add("@FTItemDesc", SqlDbType.NChar, 2000, "FTItemDesc")
        command.Parameters.Add("@FTPartNameEN", SqlDbType.NChar, 200, "FTPartNameEN")
        command.Parameters.Add("@FTPartNameTH", SqlDbType.NChar, 200, "FTPartNameTH")
        command.Parameters.Add("@FTSuplCode", SqlDbType.NChar, 30, "FTSuplCode")
        command.Parameters.Add("@FTStateNominate", SqlDbType.VarChar, 1, "FTStateNominate")
        command.Parameters.Add("@FTUnitCode", SqlDbType.NChar, 30, "FTUnitCode")
        command.Parameters.Add("@FNPrice", SqlDbType.Decimal, 5, "FNPrice")
        command.Parameters.Add("@FNHSysCurId", SqlDbType.Int, 8, "FNHSysCurId")
        command.Parameters.Add("@FNConSmp", SqlDbType.Decimal, 5, "FNConSmp")
        command.Parameters.Add("@FNConSmpPlus", SqlDbType.Decimal, 5, "FNConSmpPlus")
        command.Parameters.Add("@FTStateActive", SqlDbType.VarChar, 1, "FTStateActive")
        command.Parameters.Add("@FTStateCombination", SqlDbType.VarChar, 1, "FTStateCombination")
        command.Parameters.Add("@FTStateMainMaterial", SqlDbType.VarChar, 1, "FTStateMainMaterial")
        command.Parameters.Add("@FTInsUser", SqlDbType.NChar, 50, "FTInsUser")
        command.Parameters.Add("@FDInsDate", SqlDbType.VarChar, 10, "FDInsDate")
        command.Parameters.Add("@FTInsTime", SqlDbType.VarChar, 8, "FTInsTime")
        command.Parameters.Add("@FTUpdUser", SqlDbType.NChar, 50, "FTUpdUser")
        command.Parameters.Add("@FDUpdDate", SqlDbType.VarChar, 10, "FDUpdDate")
        command.Parameters.Add("@FTUpdTime", SqlDbType.VarChar, 8, "FTUpdTime")
        command.Parameters.Add("@FTComponent", SqlDbType.NChar, 500, "FTComponent")
        command.Parameters.Add("@FTStateFree", SqlDbType.VarChar, 1, "FTStateFree")
        command.Parameters.Add("@FTPart", SqlDbType.NChar, 200, "FTPart")
        command.Parameters.Add("@FTStateNotShowBomSheet", SqlDbType.VarBinary, 1, "FTStateNotShowBomSheet")
        command.Parameters.Add("@FTStateLabel", SqlDbType.VarBinary, 1, "FTStateLabel")
        command.Parameters.Add("@FNOrderSetType", SqlDbType.Int, 8, "FNOrderSetType")
        command.Parameters.Add("@FTItemREfNo", SqlDbType.NChar, 30, "FTItemREfNo")
        command.Parameters.Add("@FTPositionPartId", SqlDbType.NChar, 500, "FTPositionPartId")
        command.Parameters.Add("@FTStateHemNotOptiplan", SqlDbType.VarChar, 1, "FTStateHemNotOptiplan")
        command.Parameters.Add("@FNRepeatLengthCM", SqlDbType.Decimal, 5, "FNRepeatLengthCM")
        command.Parameters.Add("@FNRepeatConvert", SqlDbType.Decimal, 10, "FNRepeatConvert")
        command.Parameters.Add("@FNPackPerCarton", SqlDbType.Int, 8, "FNPackPerCarton")
        command.Parameters.Add("@FNConSmpSplit", SqlDbType.Decimal, 5, "FNConSmpSplit")


        adapter.InsertCommand = command

        ' Create the UpdateCommand.
        command = New SqlCommand(
            "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTDevelopStyle_Mat SET " &
            " FNHSysStyleDevId=@FNHSysStyleDevId " &
            ", FNSeq=@FNSeq " &
            ", FNMerMatSeq=@FNMerMatSeq " &
            ", FTItemNo=@FTItemNo " &
            ", FTItemDesc=@FTItemDesc " &
            ", FNPart=@FNPart " &
            ", FTPartNameEN=@FTPartNameEN " &
            ", FTPartNameTH=@FTPartNameTH " &
            ", FTSuplCode=@FTSuplCode " &
            ", FTStateNominate=@FTStateNominate " &
            ", FTUnitCode=@FTUnitCode " &
            ", FNPrice=@FNPrice " &
            ", FNHSysCurId=@FNHSysCurId " &
            ", FNConSmp=@FNConSmp " &
            ", FNConSmpPlus=@FNConSmpPlus " &
            ", FTComponent=@FTComponent " &
            ", FTStateActive=@FTStateActive " &
            ", FTStateCombination=@FTStateCombination " &
            ", FTStateMainMaterial=@FTStateMainMaterial " &
            ", FTStateFree=@FTStateFree " &
            ", FTPart=@FTPart " &
            ",FTUpdUser=@FTUpdUser " &
            ", FDUpdDate=@FDUpdDate " &
            ", FTUpdTime=@FTUpdTime " &
            ",FTStateNotShowBomSheet =@FTStateNotShowBomSheet " &
            ",FTStateLabel =@FTStateLabel " &
            ",FNOrderSetType =@FNOrderSetType " &
            ",FTItemREfNo =@FTItemREfNo " &
            ",FTPositionPartId =@FTPositionPartId " &
            ",FTStateHemNotOptiplan =@FTStateHemNotOptiplan " &
            ",FNRepeatLengthCM =@FNRepeatLengthCM " &
            ",FNRepeatConvert =@FNRepeatConvert " &
            ",FNPackPerCarton =@FNPackPerCarton " &
            ",FNConSmpSplit =@FNConSmpSplit " &
            "WHERE FNHSysStyleDevId = @FNHSysStyleDevId AND FNSeq = @FNSeq", connection)

        ' Add the parameters for the UpdateCommand.
        command.Parameters.Add("@FNHSysStyleDevId", SqlDbType.Int, 8, "FNHSysStyleDevId")
        command.Parameters.Add("@FNSeq", SqlDbType.Int, 8, "FNSeq")
        command.Parameters.Add("@FNMerMatSeq", SqlDbType.Decimal, 5, "FNMerMatSeq")
        command.Parameters.Add("@FTItemNo", SqlDbType.NChar, 30, "FTItemNo")
        command.Parameters.Add("@FNPart", SqlDbType.Int, 8, "FNPart")
        command.Parameters.Add("@FTItemDesc", SqlDbType.NChar, 2000, "FTItemDesc")
        command.Parameters.Add("@FTPartNameEN", SqlDbType.NChar, 200, "FTPartNameEN")
        command.Parameters.Add("@FTPartNameTH", SqlDbType.NChar, 200, "FTPartNameTH")
        command.Parameters.Add("@FTSuplCode", SqlDbType.NChar, 30, "FTSuplCode")
        command.Parameters.Add("@FTStateNominate", SqlDbType.VarChar, 1, "FTStateNominate")
        command.Parameters.Add("@FTUnitCode", SqlDbType.NChar, 30, "FTUnitCode")
        command.Parameters.Add("@FNPrice", SqlDbType.Decimal, 5, "FNPrice")
        command.Parameters.Add("@FNHSysCurId", SqlDbType.Int, 8, "FNHSysCurId")
        command.Parameters.Add("@FNConSmp", SqlDbType.Decimal, 5, "FNConSmp")
        command.Parameters.Add("@FNConSmpPlus", SqlDbType.Decimal, 5, "FNConSmpPlus")
        command.Parameters.Add("@FTStateActive", SqlDbType.VarChar, 1, "FTStateActive")
        command.Parameters.Add("@FTStateCombination", SqlDbType.VarChar, 1, "FTStateCombination")
        command.Parameters.Add("@FTStateMainMaterial", SqlDbType.VarChar, 1, "FTStateMainMaterial")
        command.Parameters.Add("@FTInsUser", SqlDbType.NChar, 50, "FTInsUser")
        command.Parameters.Add("@FDInsDate", SqlDbType.VarChar, 10, "FDInsDate")
        command.Parameters.Add("@FTInsTime", SqlDbType.VarChar, 8, "FTInsTime")
        command.Parameters.Add("@FTUpdUser", SqlDbType.NChar, 50, "FTUpdUser")
        command.Parameters.Add("@FDUpdDate", SqlDbType.VarChar, 10, "FDUpdDate")
        command.Parameters.Add("@FTUpdTime", SqlDbType.VarChar, 8, "FTUpdTime")
        command.Parameters.Add("@FTComponent", SqlDbType.NChar, 500, "FTComponent")
        command.Parameters.Add("@FTStateFree", SqlDbType.VarChar, 1, "FTStateFree")
        command.Parameters.Add("@FTPart", SqlDbType.NChar, 200, "FTPart")
        command.Parameters.Add("@FTStateNotShowBomSheet", SqlDbType.VarChar, 1, "FTStateNotShowBomSheet")
        command.Parameters.Add("@FTStateLabel", SqlDbType.VarChar, 1, "FTStateLabel")
        command.Parameters.Add("@FNOrderSetType", SqlDbType.Int, 8, "FNOrderSetType")
        command.Parameters.Add("@FTItemREfNo", SqlDbType.NChar, 30, "FTItemREfNo")
        command.Parameters.Add("@FTPositionPartId", SqlDbType.NChar, 500, "FTPositionPartId")
        command.Parameters.Add("@FTStateHemNotOptiplan", SqlDbType.VarChar, 1, "FTStateHemNotOptiplan")
        command.Parameters.Add("@FNRepeatLengthCM", SqlDbType.Decimal, 5, "FNRepeatLengthCM")
        command.Parameters.Add("@FNRepeatConvert", SqlDbType.Decimal, 10, "FNRepeatConvert")
        command.Parameters.Add("@FNPackPerCarton", SqlDbType.Int, 8, "FNPackPerCarton")
        command.Parameters.Add("@FNConSmpSplit", SqlDbType.Decimal, 5, "FNConSmpSplit")

        Dim parameter As SqlParameter = command.Parameters.Add("@FNHSysStyleDevId", SqlDbType.Int, 64, "FNHSysStyleDevId") 'old id
        parameter.SourceVersion = DataRowVersion.Original

        adapter.UpdateCommand = command

        ' Create the DeleteCommand.
        command = New SqlCommand(
            "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTDevelopStyle_Mat] " &
            "WHERE FNHSysStyleDevId = @FNHSysStyleDevId AND FNSeq = @FNSeq", connection)

        ' Add the parameters for the DeleteCommand.
        command.Parameters.Add("@FNHSysStyleDevId", SqlDbType.Int, 8, "FNHSysStyleDevId")
        command.Parameters.Add("@FNSeq", SqlDbType.Int, 8, "FNSeq")
        parameter.SourceVersion = DataRowVersion.Original

        adapter.DeleteCommand = command

        Return adapter
    End Function

    Public Function CreateAdapterColor(ByVal connection As SqlConnection) As SqlDataAdapter

        Dim adapter As SqlDataAdapter = New SqlDataAdapter()

        ' Create the SelectCommand. 
        Dim command As SqlCommand = New SqlCommand(
            "SELECT FNHSysStyleDevId, FNSeq, FNMerMatSeq, FTColorWay, FNColorWaySeq, FTRunColor, FTColorCode, FTColorNameTH,FTColorNameEN, FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime FROM " &
            "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTDevelopStyle_ColorWay] " &
            "WHERE (FNHSysStyleDevId = @FNHSysStyleDevId AND FNSeq = @FNSeq " &
            "AND FNColorWaySeq = @FNColorWaySeq " &
            ") " &
            "OR (FNHSysStyleDevId = @FNHSysStyleDevId AND FNSeq = -1)", connection)

        ' Add the parameters for the SelectCommand.
        command.Parameters.Add("@FNHSysStyleDevId", SqlDbType.Int)
        command.Parameters.Add("@FNSeq", SqlDbType.Int)
        command.Parameters.Add("@FNColorWaySeq", SqlDbType.Int)

        adapter.SelectCommand = command

        ' Create the InsertCommand.
        command = New SqlCommand(
            "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTDevelopStyle_ColorWay] " &
            "(FNHSysStyleDevId, FNSeq, FNMerMatSeq, FTColorWay, FNColorWaySeq, FTRunColor, FTColorCode, FTColorNameTH,FTColorNameEN ,FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime) " &
            "VALUES (@FNHSysStyleDevId, @FNSeq, @FNMerMatSeq, @FTColorWay, @FNColorWaySeq, @FTRunColor, @FTColorCode, @FTColorNameTH,@FTColorNameEN, " &
            "@FTInsUser, @FDInsDate, @FTInsTime, @FTUpdUser, @FDUpdDate, @FTUpdTime)", connection)

        ' Add the parameters for the InsertCommand.
        ' FNHSysStyleDevId, FNSeq, FNMerMatSeq, FTColorWay, FNColorWaySeq, FTRunColor, FTColorCode, FTColorNameTH,FTColorNameEN

        command.Parameters.Add("@FNHSysStyleDevId", SqlDbType.Int, 8, "FNHSysStyleDevId")
        command.Parameters.Add("@FNSeq", SqlDbType.Int, 8, "FNSeq")
        command.Parameters.Add("@FNMerMatSeq", SqlDbType.Decimal, 5, "FNMerMatSeq")
        command.Parameters.Add("@FTColorWay", SqlDbType.NChar, 30, "FTColorWay")
        command.Parameters.Add("@FNColorWaySeq", SqlDbType.Int, 8, "FNColorWaySeq")
        command.Parameters.Add("@FTRunColor", SqlDbType.NChar, 1, "FTRunColor")
        command.Parameters.Add("@FTColorCode", SqlDbType.NChar, 30, "FTColorCode")
        command.Parameters.Add("@FTColorNameTH", SqlDbType.NChar, 200, "FTColorNameTH")
        command.Parameters.Add("@FTColorNameEN", SqlDbType.NChar, 200, "FTColorNameEN")
        command.Parameters.Add("@FTInsUser", SqlDbType.NChar, 50, "FTInsUser")
        command.Parameters.Add("@FDInsDate", SqlDbType.VarChar, 10, "FDInsDate")
        command.Parameters.Add("@FTInsTime", SqlDbType.VarChar, 8, "FTInsTime")
        command.Parameters.Add("@FTUpdUser", SqlDbType.NChar, 50, "FTUpdUser")
        command.Parameters.Add("@FDUpdDate", SqlDbType.VarChar, 10, "FDUpdDate")
        command.Parameters.Add("@FTUpdTime", SqlDbType.VarChar, 8, "FTUpdTime")

        adapter.InsertCommand = command

        ' Create the UpdateCommand.
        command = New SqlCommand(
            "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTDevelopStyle_ColorWay] SET " &
            "FNHSysStyleDevId          = @FNHSysStyleDevId, " &
            "FNSeq                  = @FNSeq, " &
            "FNMerMatSeq            = @FNMerMatSeq, " &
            "FTColorWay          = @FTColorWay, " &
            "FNColorWaySeq             = @FNColorWaySeq, " &
            "FTRunColor    = @FTRunColor, " &
            "FTColorCode       = @FTColorCode, " &
            "FTUpdUser              = @FTUpdUser, " &
            "FDUpdDate              = @FDUpdDate, " &
            "FTUpdTime              = @FTUpdTime, " &
            "FTColorNameTH    = @FTColorNameTH, " &
            "FTColorNameEN    = @FTColorNameEN " &
            "WHERE FNHSysStyleDevId    = @FNHSysStyleDevId AND FNSeq = @FNSeq AND FNColorWaySeq = @FNColorWaySeq", connection)

        '"FTColorWay             = @FTColorWay, " & _

        ' Add the parameters for the UpdateCommand.
        command.Parameters.Add("@FNHSysStyleDevId", SqlDbType.Int, 8, "FNHSysStyleDevId")
        command.Parameters.Add("@FNSeq", SqlDbType.Int, 8, "FNSeq")
        command.Parameters.Add("@FNMerMatSeq", SqlDbType.Decimal, 5, "FNMerMatSeq")
        command.Parameters.Add("@FTColorWay", SqlDbType.NChar, 30, "FTColorWay")
        command.Parameters.Add("@FNColorWaySeq", SqlDbType.Int, 8, "FNColorWaySeq")
        command.Parameters.Add("@FTRunColor", SqlDbType.NChar, 1, "FTRunColor")
        command.Parameters.Add("@FTColorCode", SqlDbType.NChar, 30, "FTColorCode")
        command.Parameters.Add("@FTColorNameTH", SqlDbType.NChar, 200, "FTColorNameTH")
        command.Parameters.Add("@FTColorNameEN", SqlDbType.NChar, 200, "FTColorNameEN")
        command.Parameters.Add("@FTUpdUser", SqlDbType.NChar, 50, "FTUpdUser")
        command.Parameters.Add("@FDUpdDate", SqlDbType.VarChar, 10, "FDUpdDate")
        command.Parameters.Add("@FTUpdTime", SqlDbType.VarChar, 8, "FTUpdTime")

        Dim parameter As SqlParameter
        parameter = command.Parameters.Add("@FNHSysStyleDevId", SqlDbType.Int, 8, "FNHSysStyleDevId")
        parameter = command.Parameters.Add("@FNSeq", SqlDbType.Int, 8, "FNSeq")
        parameter = command.Parameters.Add("@FNColorWaySeq", SqlDbType.Int, 8, "FNColorWaySeq")
        parameter.SourceVersion = DataRowVersion.Original

        adapter.UpdateCommand = command

        ' Create the DeleteCommand.
        command = New SqlCommand(
            "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTDevelopStyle_ColorWay] " &
            "WHERE FNHSysStyleDevId = @FNHSysStyleDevId AND FNSeq = @FNSeq", connection)

        ' Add the parameters for the DeleteCommand.
        command.Parameters.Add("@FNHSysStyleDevId", SqlDbType.Int, 8, "FNHSysStyleDevId")
        command.Parameters.Add("@FNSeq", SqlDbType.Int, 8, "FNSeq")
        parameter.SourceVersion = DataRowVersion.Original

        adapter.DeleteCommand = command

        Return adapter
    End Function

    Public Function CreateAdapterSize(ByVal connection As SqlConnection) As SqlDataAdapter

        Dim adapter As SqlDataAdapter = New SqlDataAdapter()

        ' Create the SelectCommand. 
        Dim command As SqlCommand = New SqlCommand(
            "SELECT FNHSysStyleDevId, FNSeq, FNMerMatSeq, FTSizeBreakDown, FNSieBreakDownSeq, FTRunSize, FTSizeCode, FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime FROM " &
            "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTDevelopStyle_SizeBreakDown] " &
            "WHERE (FNHSysStyleDevId = @FNHSysStyleDevId AND FNSeq = @FNSeq " &
            "AND FNSieBreakDownSeq = @FNSieBreakDownSeq " &
            ") " &
            "OR (FNHSysStyleDevId = @FNHSysStyleDevId AND FNSeq = -1)", connection)

        ' Add the parameters for the SelectCommand.
        command.Parameters.Add("@FNHSysStyleDevId", SqlDbType.Int)
        command.Parameters.Add("@FNSeq", SqlDbType.Int)
        command.Parameters.Add("@FNColorWaySeq", SqlDbType.Int)

        adapter.SelectCommand = command

        ' Create the InsertCommand.
        command = New SqlCommand(
            "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTDevelopStyle_SizeBreakDown] " &
            "(FNHSysStyleDevId, FNSeq, FNMerMatSeq, FTSizeBreakDown, FNSieBreakDownSeq, FTRunSize, FTSizeCode, " &
            "[FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser],[FDUpdDate],[FTUpdTime]) " &
            "VALUES (@FNHSysStyleDevId, @FNSeq, @FNMerMatSeq,@FTSizeBreakDown, @FNSieBreakDownSeq, @FTRunSize, @FTSizeCode, " &
            "@FTInsUser, @FDInsDate, @FTInsTime, @FTUpdUser, @FDUpdDate, @FTUpdTime)", connection)

        ' Add the parameters for the InsertCommand.
        command.Parameters.Add("@FNHSysStyleDevId", SqlDbType.Int, 8, "FNHSysStyleDevId")
        command.Parameters.Add("@FNSeq", SqlDbType.Int, 8, "FNSeq")
        command.Parameters.Add("@FNMerMatSeq", SqlDbType.Decimal, 5, "FNMerMatSeq")
        command.Parameters.Add("@FNSieBreakDownSeq", SqlDbType.Int, 8, "FNSieBreakDownSeq")
        command.Parameters.Add("@FTRunSize", SqlDbType.NChar, 30, "FTRunSize")
        command.Parameters.Add("@FTSizeBreakDown", SqlDbType.NChar, 30, "FTSizeBreakDown")
        command.Parameters.Add("@FTSizeCode", SqlDbType.NChar, 30, "FTSizeCode")
        command.Parameters.Add("@FTInsUser", SqlDbType.NChar, 50, "FTInsUser")
        command.Parameters.Add("@FDInsDate", SqlDbType.VarChar, 10, "FDInsDate")
        command.Parameters.Add("@FTInsTime", SqlDbType.VarChar, 8, "FTInsTime")
        command.Parameters.Add("@FTUpdUser", SqlDbType.NChar, 50, "FTUpdUser")
        command.Parameters.Add("@FDUpdDate", SqlDbType.VarChar, 10, "FDUpdDate")
        command.Parameters.Add("@FTUpdTime", SqlDbType.VarChar, 8, "FTUpdTime")

        adapter.InsertCommand = command

        ' Create the UpdateCommand.
        command = New SqlCommand(
            "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTDevelopStyle_SizeBreakDown] SET " &
            "FNHSysStyleDevId       = @FNHSysStyleDevId, " &
            "FNSeq                  = @FNSeq, " &
            "FNMerMatSeq            = @FNMerMatSeq, " &
            "FNSieBreakDownSeq      = @FNSieBreakDownSeq, " &
            "FTRunSize              = @FTRunSize, " &
            "FTSizeBreakDown        = @FTSizeBreakDown, " &
            "FTSizeCode             = @FTSizeCode, " &
            "FTUpdUser              = @FTUpdUser, " &
            "FDUpdDate              = @FDUpdDate, " &
            "FTUpdTime              = @FTUpdTime " &
            "WHERE FNHSysStyleDevId    = @FNHSysStyleDevId AND FNSeq = @FNSeq AND FNSieBreakDownSeq = @FNSieBreakDownSeq", connection)

        ' Add the parameters for the UpdateCommand.
        command.Parameters.Add("@FNHSysStyleDevId", SqlDbType.Int, 8, "FNHSysStyleDevId")
        command.Parameters.Add("@FNSeq", SqlDbType.Int, 8, "FNSeq")
        command.Parameters.Add("@FNMerMatSeq", SqlDbType.Decimal, 5, "FNMerMatSeq")
        command.Parameters.Add("@FNSieBreakDownSeq", SqlDbType.Int, 8, "FNSieBreakDownSeq")
        command.Parameters.Add("@FTRunSize", SqlDbType.NChar, 30, "FTRunSize")
        command.Parameters.Add("@FTSizeBreakDown", SqlDbType.NChar, 30, "FTSizeBreakDown")
        command.Parameters.Add("@FTSizeCode", SqlDbType.NChar, 30, "FTSizeCode")

        command.Parameters.Add("@FTUpdUser", SqlDbType.NChar, 50, "FTUpdUser")
        command.Parameters.Add("@FDUpdDate", SqlDbType.VarChar, 10, "FDUpdDate")
        command.Parameters.Add("@FTUpdTime", SqlDbType.VarChar, 8, "FTUpdTime")

        Dim parameter As SqlParameter
        parameter = command.Parameters.Add("@FNHSysStyleDevId", SqlDbType.Int, 8, "FNHSysStyleDevId") 'old id
        parameter = command.Parameters.Add("@FNSeq", SqlDbType.Int, 8, "FNSeq")
        parameter = command.Parameters.Add("@FNSieBreakDownSeq", SqlDbType.Int, 8, "FNSieBreakDownSeq")
        parameter.SourceVersion = DataRowVersion.Original

        adapter.UpdateCommand = command

        ' Create the DeleteCommand.
        command = New SqlCommand(
            "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTDevelopStyle_SizeBreakDown] " &
            "WHERE FNHSysStyleDevId = @FNHSysStyleDevId AND FNSeq = @FNSeq", connection)

        ' Add the parameters for the DeleteCommand.
        command.Parameters.Add("@FNHSysStyleDevId", SqlDbType.Int, 8, "FNHSysStyleDevId")
        command.Parameters.Add("@FNSeq", SqlDbType.Int, 8, "FNSeq")
        parameter.SourceVersion = DataRowVersion.Original

        adapter.DeleteCommand = command

        Return adapter
    End Function

    Public Function CreateAdapterImportColor(ByVal connection As SqlConnection) As SqlDataAdapter
        Dim adapter As SqlDataAdapter = New SqlDataAdapter()

        Dim _Qry As String = ""



        _Qry = " SELECT  FNHSysStyleDevId,FNMatColorSeq,FTColorway,FNMatColorSeq AS FNHSysMatColorId "
        _Qry &= vbCrLf & "  FROM ("
        _Qry &= vbCrLf & "  SELECT        A.FNHSysStyleDevId, MAX(A.FNColorWaySeq) AS FNMatColorSeq, A.FTColorWay AS FTColorway"
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTDevelopStyle_ColorWay AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE (A.FNHSysStyleDevId = @FNHSysStyleDevId) "
        _Qry &= vbCrLf & "  GROUP BY  A.FNHSysStyleDevId, A.FTColorWay "
        _Qry &= vbCrLf & "   ) AS A"
        _Qry &= vbCrLf & "  GROUP BY  FNHSysStyleDevId, FNMatColorSeq, FTColorway  "
        _Qry &= vbCrLf & "  ORDER BY FNHSysStyleDevId, FNMatColorSeq "

        ' Create the SelectCommand. 
        Dim command As SqlCommand = New SqlCommand(_Qry, connection)

        ' Add the parameters for the SelectCommand.
        command.Parameters.Add("@FNHSysStyleDevId", SqlDbType.Int)

        adapter.SelectCommand = command

        Return adapter
    End Function

    Public Function CreateAdapterImportSize(ByVal connection As SqlConnection) As SqlDataAdapter
        Dim adapter As SqlDataAdapter = New SqlDataAdapter()
        Dim _Qry As String = ""

        _Qry = " SELECT  FNHSysStyleDevId,FNMatSizeSeq,FTSizeBreakDown,FNHSysMatSizeId "
        _Qry &= vbCrLf & " FROM "
        _Qry &= vbCrLf & " (  SELECT        A.FNHSysStyleDevId, A.FNSieBreakDownSeq AS FNMatSizeSeq, A.FTSizeBreakDown AS  FTSizeBreakDown, A.FNSieBreakDownSeq AS FNHSysMatSizeId"
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTDevelopStyle_SizeBreakDown AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & "  WHERE (A.FNHSysStyleDevId = @FNHSysStyleDevId) "
        _Qry &= vbCrLf & "  GROUP BY   A.FNHSysStyleDevId, A.FNSieBreakDownSeq, A.FTSizeBreakDown    "
        _Qry &= vbCrLf & "   ) AS A"
        _Qry &= vbCrLf & "  GROUP BY  FNHSysStyleDevId,FNMatSizeSeq,FTSizeBreakDown,FNHSysMatSizeId "
        _Qry &= vbCrLf & " ORDER BY FNHSysStyleDevId,FNMatSizeSeq,FTSizeBreakDown,FNHSysMatSizeId"

        ' Create the SelectCommand. 

        Dim command As SqlCommand = New SqlCommand(_Qry, connection)

        ' Add the parameters for the SelectCommand.
        command.Parameters.Add("@FNHSysStyleDevId", SqlDbType.Int)

        adapter.SelectCommand = command

        Return adapter
    End Function

#End Region

#Region "Merge Grid cell"

    Private Sub InitialGridMergCell()
        Dim MergGrid1 As GridView = ogcmat.Views(0)
        Dim MergGrid2 As GridView = ogccolor.Views(0)
        Dim MergGrid3 As GridView = ogcsize.Views(0)

        'Style Detail
        For Each c As GridColumn In MergGrid1.Columns
            If c.AbsoluteIndex < 8 Then
                If c.FieldName.ToString.ToLower() = "FTItemREfNo".ToLower() Then
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
                Else
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                End If


            Else
                c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End If
        Next

        'Color Way
        For Each c As GridColumn In MergGrid2.Columns
            If c.AbsoluteIndex < 5 Then
                c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
            Else
                c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End If
        Next

        'Size Breakdown
        For Each c As GridColumn In MergGrid3.Columns
            If c.AbsoluteIndex < 5 Then
                c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
            Else
                c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End If
        Next

    End Sub

    Private Sub GridView1_CellMerge(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvmat.CellMerge
        Dim view As GridView = TryCast(sender, GridView)
        Try
            Dim ItemCode1 As String = view.GetRowCellValue(e.RowHandle1, "FTItemNo").ToString
            Dim ItemCode2 As String = view.GetRowCellValue(e.RowHandle2, "FTItemNo").ToString

            If (ItemCode1 = ItemCode2 And e.Column.AbsoluteIndex < 13) Then
                Dim value1 = view.GetRowCellValue(e.RowHandle1, e.Column)
                Dim value2 = view.GetRowCellValue(e.RowHandle2, e.Column)

                e.Merge = (value1 = value2)
                e.Handled = True
                Return
            Else
                e.Merge = False
                e.Handled = True
                Return
            End If

        Catch ex As Exception
            Return
        End Try
    End Sub

    Private Sub GridView2_CellMerge(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvcolor.CellMerge
        Dim view As GridView = TryCast(sender, GridView)
        Try
            Dim ItemCode1 As String = view.GetRowCellValue(e.RowHandle1, "FTItemNo").ToString
            Dim ItemCode2 As String = view.GetRowCellValue(e.RowHandle2, "FTItemNo").ToString

            If (ItemCode1 = ItemCode2 And e.Column.AbsoluteIndex < 5) Then
                Dim value1 = view.GetRowCellValue(e.RowHandle1, e.Column)
                Dim value2 = view.GetRowCellValue(e.RowHandle2, e.Column)

                e.Merge = (value1 = value2)
                e.Handled = True
                Return
            Else
                e.Merge = False
                e.Handled = True
                Return
            End If

        Catch ex As Exception
            Return
        End Try
    End Sub

    Private Sub GridView3_CellMerge(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvsize.CellMerge
        Dim view As GridView = TryCast(sender, GridView)
        Try
            Dim ItemCode1 As String = view.GetRowCellValue(e.RowHandle1, "FTItemNo").ToString
            Dim ItemCode2 As String = view.GetRowCellValue(e.RowHandle2, "FTItemNo").ToString

            If (ItemCode1 = ItemCode2 And e.Column.AbsoluteIndex < 5) Then
                Dim value1 = view.GetRowCellValue(e.RowHandle1, e.Column)
                Dim value2 = view.GetRowCellValue(e.RowHandle2, e.Column)

                e.Merge = (value1 = value2)
                e.Handled = True
                Return
            Else
                e.Merge = False
                e.Handled = True
                Return
            End If

        Catch ex As Exception
            Return
        End Try
    End Sub

#End Region

    Private Sub TabChange()

        ocmbomaddnew.Visible = (otb.SelectedTabPage.Name = otpmatcode.Name)
        ocmbomnewcolorway.Visible = (otb.SelectedTabPage.Name = otpmatcode.Name)
        ocmbomdeleterow.Visible = (otb.SelectedTabPage.Name = otpmatcode.Name)
        ' ocmbomdiffpart.Visible = (otb.SelectedTabPage.Name = otpmatcode.Name)
        ocmbominsertrow.Visible = (otb.SelectedTabPage.Name = otpmatcode.Name)

        ocmdeletecolorway.Visible = (otb.SelectedTabPage.Name = otpmatcolor.Name)
        ocmbomnewcolorway.Visible = (otb.SelectedTabPage.Name = otpmatcolor.Name)
        ocmbomchangecolorway.Visible = (otb.SelectedTabPage.Name = otpmatcolor.Name)
        ocmchangematcolordes.Visible = (otb.SelectedTabPage.Name = otpmatcolor.Name)

        ocmbomnewsize.Visible = (otb.SelectedTabPage.Name = otpmatsize.Name)
        ocmbomdeletesize.Visible = (otb.SelectedTabPage.Name = otpmatsize.Name)

        HI.TL.METHOD.CallActiveToolBarFunction(Me)

    End Sub

    Private Sub otb_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otb.SelectedPageChanged
        Call TabChange()
    End Sub

    Private Sub ocmbomnewsize_Click(sender As Object, e As EventArgs) Handles ocmbomnewsize.Click
        If CheckOwner() = False Then
            Exit Sub
        End If

        If sFNHSysStyleDevId = "" Then Return
        'InitNewRow(CType(ogcstylecolor.DataSource, DataTable), TabIndexs.Colorway)
        Call LoadStylePostInfo()
        If CheckPostDataToBomSheet() = False Then Exit Sub
        HI.ST.Lang.SP_SETxLanguage(_wNewSize)
        With _wNewSize
            .FTSizeBreakDown.Text = ""
            .ProcNew = False
            .ShowDialog()

            If .ProcNew Then
                Dim _Qry As String = ""
                Dim _NewSize As String = .FTSizeBreakDown.Text
                Dim _NewSizeID As String = .FTSizeBreakDown.Properties.Tag.ToString

                Dim InitDt As DataTable = CType(Me.ogcsize.DataSource, DataTable)
                Dim dc As DataColumn
                Dim dc1 As DataColumn

                If Me.ogvcolor.Columns.ColumnByFieldName("FTRawMatColorCode" & _NewSize) Is Nothing Then

                    dc = New DataColumn("FTRawMatSizeCode" & _NewSize, System.Type.GetType("System.String"))
                    dc1 = New DataColumn("FNHSysRawMatSizeId" & "FTRawMatSizeCode" & _NewSize, System.Type.GetType("System.String"))

                    dc.Caption = _NewSize
                    dc1.Caption = "FNHSysRawMatColorId"

                    If ogvcolor.Columns(dc.ColumnName) Is Nothing Then

                        Try
                            ogvsize.Columns.Item(dc.ColumnName).FieldName = dc.ColumnName
                            ogvsize.Columns.Item(dc1.ColumnName).FieldName = dc1.ColumnName
                            InitDt.Columns.Add(dc.ColumnName)
                            InitDt.Columns.Add(dc1.ColumnName)
                        Catch ex As Exception
                            ogvsize.Columns.AddField(dc.ColumnName)
                            ogvsize.Columns(dc.ColumnName).FieldName = dc.ColumnName
                            ogvsize.Columns(dc.ColumnName).Name = dc.ColumnName
                            ogvsize.Columns(dc.ColumnName).Caption = dc.Caption
                            ogvsize.Columns(dc.ColumnName).Visible = True
                            ogvsize.Columns(dc.ColumnName).Width = 70
                            ogvsize.Columns(dc.ColumnName).OptionsColumn.AllowShowHide = False
                            ogvsize.Columns(dc.ColumnName).OptionsColumn.ShowInCustomizationForm = False

                            ogvsize.Columns.AddField(dc1.ColumnName)
                            ogvsize.Columns(dc1.ColumnName).FieldName = dc1.ColumnName
                            ogvsize.Columns(dc1.ColumnName).Name = dc1.ColumnName
                            ogvsize.Columns(dc1.ColumnName).Caption = dc1.Caption
                            ogvsize.Columns(dc1.ColumnName).Tag = _NewSizeID
                            ogvsize.Columns(dc1.ColumnName).Visible = False
                            ogvsize.Columns(dc1.ColumnName).OptionsColumn.AllowShowHide = False
                            ogvsize.Columns(dc1.ColumnName).OptionsColumn.ShowInCustomizationForm = False

                            Dim repos As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
                            repos.Buttons.Item(0).Tag = "170"
                            ogvsize.Columns(dc.ColumnName).ColumnEdit = repos

                            With repos
                                .CharacterCasing = CharacterCasing.Upper
                                AddHandler .Click, AddressOf DynamicResponButtone_Gotocus
                                AddHandler .ButtonClick, AddressOf DynamicResponButtoneSysHide_ButtonClick
                                AddHandler .Leave, AddressOf DynamicResponButtoneditSysHide_Leave
                            End With

                            InitDt.Columns.Add(dc.ColumnName)
                            InitDt.Columns.Add(dc1.ColumnName)

                        End Try

                    End If

                    CType(Me.ogcsize.DataSource, DataTable).AcceptChanges()

                End If
            End If

        End With

    End Sub

    Private Sub ocmbomdeletesize_Click(sender As Object, e As EventArgs) Handles ocmbomdeletesize.Click
        If CheckOwner() = False Then
            Exit Sub
        End If

        If sFNHSysStyleDevId = "" Then Return
        Call LoadStylePostInfo()
        If CheckPostDataToBomSheet() = False Then Exit Sub
        With Me.ogvsize
            If .FocusedRowHandle < 0 Then Exit Sub
            If Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatSizeCode".Length) = "FTRawMatSizeCode".ToUpper Then
                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการลบ Size ใช่หรือไม่ ?", 1406030077, .FocusedColumn.Caption) Then
                    Dim Col1 As String = .FocusedColumn.FieldName.ToString
                    Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)
                    Dim dt As DataTable = CType(Me.ogcsize.DataSource, DataTable)
                    Try
                        ogvsize.Columns.Remove(ogvsize.Columns.ColumnByFieldName(Col1))
                        dt.Columns.Remove(Col1)
                    Catch ex As Exception
                    End Try

                    Try
                        ogvsize.Columns.Remove(ogvsize.Columns.ColumnByFieldName(Col2))
                        dt.Columns.Remove(Col2)
                    Catch ex As Exception
                    End Try
                    CType(Me.ogcsize.DataSource, DataTable).AcceptChanges()
                End If
            End If

        End With
    End Sub

#Region "Handle Control"

    Private Sub DynamicButtoneditColor_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.F7

                With CType(sender, DevExpress.XtraEditors.ButtonEdit)

                    If .Properties.Buttons.Count >= 1 Then
                        If (.Properties.Buttons.Item(0).Visible) Then
                            .PerformClick(.Properties.Buttons.Item(0))
                        End If
                    End If
                End With
            Case Keys.F1
                Call ocmchangematcolordes_Click(ocmchangematcolordes, New System.EventArgs)
            Case Keys.F10, Keys.F11, Keys.F12
                Try
                    With Me.ogvcolor
                        If .FocusedRowHandle < 0 Then Exit Sub
                        Select Case e.KeyCode
                            Case Keys.F10
                                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                                    If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatColorCode")) = "FTRawMatColorCode".ToUpper Then

                                        Dim Col1 As String = GridCol.FieldName.ToString
                                        Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)
                                        Dim Col3 As String = "FTRawMatColorNameTH" & GridCol.FieldName.ToString.Replace("FTRawMatColorCode", "")
                                        Dim Col4 As String = "FTRawMatColorNameEN" & GridCol.FieldName.ToString.Replace("FTRawMatColorCode", "")

                                        .SetFocusedRowCellValue(GridCol.FieldName, "N/R")
                                        .SetFocusedRowCellValue(Col2, "-1")
                                        .SetFocusedRowCellValue(Col3, "")
                                        .SetFocusedRowCellValue(Col4, "")

                                    End If
                                Next

                                CType(ogccolor.DataSource, DataTable).AcceptChanges()
                                ogccolor.RefreshDataSource()

                            Case Keys.F11
                                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                                    If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatColorCode")) = "FTRawMatColorCode".ToUpper Then

                                        Dim Col1 As String = GridCol.FieldName.ToString
                                        Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)

                                        Dim Col3 As String = "FTRawMatColorNameTH" & GridCol.FieldName.ToString.Replace("FTRawMatColorCode", "")
                                        Dim Col4 As String = "FTRawMatColorNameEN" & GridCol.FieldName.ToString.Replace("FTRawMatColorCode", "")


                                        .SetFocusedRowCellValue(GridCol.FieldName, "")
                                        .SetFocusedRowCellValue(Col2, "0")
                                        .SetFocusedRowCellValue(Col3, "")
                                        .SetFocusedRowCellValue(Col4, "")
                                    End If
                                Next

                                CType(ogccolor.DataSource, DataTable).AcceptChanges()
                                ogccolor.RefreshDataSource()
                            Case Keys.F12
                                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                                    If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatColorCode")) = "FTRawMatColorCode".ToUpper Then

                                        Dim Code As String = GridCol.Caption
                                        Dim _ColorInt As Integer = 99 ' Integer.Parse(Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysRawMatColorId FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor WITH(NOLOCK) WHERE FTRawMatColorCode='" & HI.UL.ULF.rpQuoted(Code) & "'  ", Conn.DB.DataBaseName.DB_MASTER, "0")))

                                        If _ColorInt > 0 Then
                                            Dim Col1 As String = GridCol.FieldName.ToString
                                            Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)
                                            Dim Col3 As String = "FTRawMatColorNameTH" & GridCol.FieldName.ToString.Replace("FTRawMatColorCode", "")
                                            Dim Col4 As String = "FTRawMatColorNameEN" & GridCol.FieldName.ToString.Replace("FTRawMatColorCode", "")

                                            .SetFocusedRowCellValue(GridCol.FieldName, Code)
                                            .SetFocusedRowCellValue(Col2, _ColorInt.ToString)
                                            .SetFocusedRowCellValue(Col3, Code)
                                            .SetFocusedRowCellValue(Col4, Code)

                                        End If


                                    End If
                                Next

                                CType(ogccolor.DataSource, DataTable).AcceptChanges()
                                ogccolor.RefreshDataSource()
                        End Select
                    End With
                Catch ex As Exception
                End Try
            Case Keys.F9
                Try
                    With Me.ogvcolor
                        If .FocusedRowHandle < 0 Then Exit Sub
                        Dim _VisibleIndex As Integer = .FocusedColumn.VisibleIndex

                        Dim mCol1 As String = .FocusedColumn.FieldName.ToString
                        Dim mCol2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(mCol1, mCol1.Length - "FTRawMatColorCode".Length)
                        Dim mCol3 As String = "FTRawMatColorNameTH" & .FocusedColumn.FieldName.ToString.Replace("FTRawMatColorCode", "")
                        Dim mCol4 As String = "FTRawMatColorNameEN" & .FocusedColumn.FieldName.ToString.Replace("FTRawMatColorCode", "")

                        Dim Code As String = ""
                        Dim CodeTH As String = ""
                        Dim CodeEN As String = ""

                        With CType(sender, DevExpress.XtraEditors.ButtonEdit)
                            Code = .Text
                        End With

                        CodeTH = .GetFocusedRowCellValue(mCol3).ToString
                        CodeEN = .GetFocusedRowCellValue(mCol4).ToString

                        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                            If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatColorCode")) = "FTRawMatColorCode".ToUpper Then
                                If GridCol.VisibleIndex > _VisibleIndex Then


                                    Dim _ColorInt As Integer = 99 'Integer.Parse(Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysRawMatColorId FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor WITH(NOLOCK) WHERE FTRawMatColorCode='" & HI.UL.ULF.rpQuoted(Code) & "'  ", Conn.DB.DataBaseName.DB_MASTER, "0")))

                                    If _ColorInt > 0 Then
                                        Dim Col1 As String = GridCol.FieldName.ToString
                                        Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)
                                        Dim Col3 As String = "FTRawMatColorNameTH" & GridCol.FieldName.ToString.Replace("FTRawMatColorCode", "")
                                        Dim Col4 As String = "FTRawMatColorNameEN" & GridCol.FieldName.ToString.Replace("FTRawMatColorCode", "")

                                        .SetFocusedRowCellValue(GridCol.FieldName, Code)
                                        .SetFocusedRowCellValue(Col2, _ColorInt.ToString)
                                        .SetFocusedRowCellValue(Col3, CodeTH)
                                        .SetFocusedRowCellValue(Col4, CodeEN)

                                    End If
                                End If

                            End If
                        Next

                        CType(ogccolor.DataSource, DataTable).AcceptChanges()
                        ogccolor.RefreshDataSource()

                    End With
                Catch ex As Exception
                End Try
            Case Else

        End Select
    End Sub

    Private Sub DynamicButtoneditSize_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.F7

                With CType(sender, DevExpress.XtraEditors.ButtonEdit)

                    If .Properties.Buttons.Count >= 1 Then
                        If (.Properties.Buttons.Item(0).Visible) Then
                            .PerformClick(.Properties.Buttons.Item(0))
                        End If
                    End If
                End With
            Case Keys.F10, Keys.F11, Keys.F12
                Try
                    With Me.ogvsize
                        If .FocusedRowHandle < 0 Then Exit Sub
                        Select Case e.KeyCode
                            Case Keys.F10

                                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                                    If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatSizeCode")) = "FTRawMatSizeCode".ToUpper Then

                                        Dim Col1 As String = GridCol.FieldName.ToString
                                        Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                                        .SetFocusedRowCellValue(GridCol.FieldName, "N/R")
                                        .SetFocusedRowCellValue(Col2, "-1")
                                    End If
                                Next

                                CType(ogcsize.DataSource, DataTable).AcceptChanges()
                                ogcsize.RefreshDataSource()

                            Case Keys.F11
                                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                                    If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatSizeCode")) = "FTRawMatSizeCode".ToUpper Then
                                        Dim Col1 As String = GridCol.FieldName.ToString
                                        Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                                        .SetFocusedRowCellValue(GridCol.FieldName, "")
                                        .SetFocusedRowCellValue(Col2, "0")
                                    End If
                                Next

                                CType(ogcsize.DataSource, DataTable).AcceptChanges()
                                ogcsize.RefreshDataSource()
                            Case Keys.F12
                                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                                    If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatSizeCode")) = "FTRawMatSizeCode".ToUpper Then

                                        Dim Code As String = GridCol.Caption
                                        Dim _SizeInt As Integer = Integer.Parse(Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysRawMatSizeId FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize WITH(NOLOCK) WHERE FTRawMatSizeCode='" & HI.UL.ULF.rpQuoted(Code) & "'  ", Conn.DB.DataBaseName.DB_MASTER, "0")))

                                        If _SizeInt > 0 Then
                                            Dim Col1 As String = GridCol.FieldName.ToString
                                            Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                                            .SetFocusedRowCellValue(GridCol.FieldName, Code)
                                            .SetFocusedRowCellValue(Col2, _SizeInt.ToString)
                                        End If


                                    End If
                                Next

                                CType(ogcsize.DataSource, DataTable).AcceptChanges()
                                ogcsize.RefreshDataSource()
                        End Select
                    End With
                Catch ex As Exception

                End Try
            Case Keys.F9
                Try
                    With Me.ogvsize
                        If .FocusedRowHandle < 0 Then Exit Sub
                        Dim _VisibleIndex As Integer = .FocusedColumn.VisibleIndex

                        Dim Code As String = ""
                        With CType(sender, DevExpress.XtraEditors.ButtonEdit)
                            Code = .Text
                        End With

                        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                            If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatSizeCode")) = "FTRawMatSizeCode".ToUpper Then

                                If GridCol.VisibleIndex > _VisibleIndex Then

                                    Dim _SizeInt As Integer = Integer.Parse(Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysRawMatSizeId FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize WITH(NOLOCK) WHERE FTRawMatSizeCode='" & HI.UL.ULF.rpQuoted(Code) & "'  ", Conn.DB.DataBaseName.DB_MASTER, "0")))

                                    If _SizeInt > 0 Then
                                        Dim Col1 As String = GridCol.FieldName.ToString
                                        Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                                        .SetFocusedRowCellValue(GridCol.FieldName, Code)
                                        .SetFocusedRowCellValue(Col2, _SizeInt.ToString)
                                    End If
                                End If

                            End If
                        Next

                        CType(ogcsize.DataSource, DataTable).AcceptChanges()
                        ogcsize.RefreshDataSource()

                    End With
                Catch ex As Exception
                End Try
            Case Else

        End Select
    End Sub

    Private Sub DynamicResponButtoneSysHide_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Select Case e.Button.Index
            Case 0

                If Val(e.Button.Tag.ToString) <= 0 Then Exit Sub

                If Not (_StateProc) Then
                    _StateProc = True
                    Dim _form As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm
                    With New HI.TL.wDynamicBrowseInfo(Val(e.Button.Tag.ToString), _form)
                        .BrowseID = Val(e.Button.Tag.ToString)

                        .X = MousePosition.X
                        .Y = MousePosition.Y

                        Dim _Qrysql As String = ""
                        Dim _dtbrw As New DataTable
                        Dim _dtret As New DataTable
                        Dim _BrowseCmd As String = ""
                        Dim _BrowseSortCmd As String = ""
                        Dim _BrowseWhereCmd As String = ""
                        Dim _FTBrwCmdField As String = ""
                        Dim _FTBrwCmdFieldOptional As String = ""
                        Dim FTBrwCmdGroupBy As String = ""
                        Dim _Where As String = ""
                        Dim _ConFiledName As String = ""
                        _Where = HI.TL.HandlerControl.GridViewGetBrowseRespon(sender, CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView), .BrowseID, _dtret, _BrowseCmd, _BrowseSortCmd, _BrowseWhereCmd, _FTBrwCmdField, _FTBrwCmdFieldOptional, FTBrwCmdGroupBy, _Qrysql, "", "")

                        If _Qrysql = "" Then
                            _StateProc = False
                            Exit Sub
                        End If

                        _dtbrw = New DataTable
                        _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql & "  " & _BrowseWhereCmd & "  " & _Where & "  " & FTBrwCmdGroupBy & "  " & _BrowseSortCmd, Conn.DB.DataBaseName.DB_SYSTEM)

                        .Data = _dtbrw.Copy
                        .DataRetField = _dtret.Copy

                        _dtbrw.Dispose()
                        _dtret.Dispose()

                        .ShowDialog()

                        If Not (.ValuesReturn Is Nothing) Then

                            For Each Row As DataRow In .DataRetField.Select("NOT(FTRetField IS NULL)")
                                With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                                    Dim _ColName As String = Row!FTRetField.ToString

                                    If Not (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                    Else
                                        _ColName = _ColName & "" & .FocusedColumn.Name.ToString

                                        If (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                            If Microsoft.VisualBasic.Left(Row!FTRetField, Row!FTRetField.ToString.Length) = Microsoft.VisualBasic.Left(.FocusedColumn.Name.ToString, Row!FTRetField.ToString.Length) Then
                                                _ColName = .FocusedColumn.Name.ToString
                                            End If
                                        End If
                                    End If

                                    If Not (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                        Dim ctrl As Object = .Columns.ColumnByFieldName(_ColName).ColumnEdit

                                        If ctrl Is Nothing Then

                                            Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                Case "System.Int32".ToUpper
                                                    .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                Case "System.String".ToUpper
                                                    .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                Case Else
                                                    .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                            End Select
                                        Else
                                            If Row!FTStatePropertyTag.ToString = "Y" Then
                                                Try
                                                    If ctrl Is Nothing Then
                                                        Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                            Case "System.Int32".ToUpper
                                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                            Case "System.String".ToUpper
                                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                            Case Else
                                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                        End Select
                                                    Else

                                                        Try

                                                            If Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatSizeCode".Length) = "FTRawMatSizeCode".ToUpper Then
                                                                Dim Col1 As String = .FocusedColumn.FieldName.ToString
                                                                Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                                                                Select Case .Columns.ColumnByFieldName(Col2).ColumnType.FullName.ToString.ToUpper
                                                                    Case "System.Int32".ToUpper
                                                                        .SetRowCellValue(.FocusedRowHandle, Col2, Val(Row!ValuesRet.ToString))

                                                                    Case "System.String".ToUpper
                                                                        .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                                    Case Else
                                                                        .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                                End Select
                                                            ElseIf Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = "FTRawMatColorCode".ToUpper Then
                                                                Dim Col1 As String = .FocusedColumn.FieldName.ToString
                                                                Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)

                                                                Select Case .Columns.ColumnByFieldName(Col2).ColumnType.FullName.ToString.ToUpper
                                                                    Case "System.Int32".ToUpper
                                                                        .SetRowCellValue(.FocusedRowHandle, Col2, Val(Row!ValuesRet.ToString))

                                                                    Case "System.String".ToUpper
                                                                        .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                                    Case Else
                                                                        .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                                End Select
                                                            Else
                                                                Select Case .Columns.ColumnByFieldName(_ColName & "_Hide").ColumnType.FullName.ToString.ToUpper
                                                                    Case "System.Int32".ToUpper
                                                                        .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Val(Row!ValuesRet.ToString))
                                                                    Case "System.String".ToUpper
                                                                        .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Row!ValuesRet.ToString)
                                                                    Case Else
                                                                        .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Row!ValuesRet.ToString)
                                                                End Select
                                                            End If
                                                        Catch ex As Exception
                                                        End Try

                                                        'Try
                                                        '    With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
                                                        '        .Text = Row!ValuesRet.ToString
                                                        '    End With
                                                        'Catch ex As Exception
                                                        'End Try

                                                        Try
                                                            With CType(ctrl, DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit)
                                                                .Tag = Row!ValuesRet.ToString
                                                            End With
                                                        Catch ex As Exception
                                                        End Try

                                                    End If
                                                Catch ex As Exception
                                                End Try
                                            Else


                                                Try
                                                    With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
                                                        .Text = Row!ValuesRet.ToString
                                                    End With
                                                Catch ex As Exception
                                                End Try

                                                Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                    Case "System.Int32".ToUpper
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                    Case "System.String".ToUpper
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                    Case Else
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                End Select
                                            End If
                                        End If
                                    End If
                                End With
                            Next
                        End If

                        .Data.Dispose()
                        .DataRetField.Dispose()

                    End With

                    _StateProc = False

                    With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                        Try
                            Dim IC As Integer = 1
                            Do While .Columns(.FocusedColumn.AbsoluteIndex + IC).Visible = False
                                IC = IC + 1
                            Loop
                            .FocusedColumn = .Columns(.FocusedColumn.AbsoluteIndex + IC)
                        Catch ex As Exception
                        End Try
                    End With

                End If

        End Select
    End Sub

    Private _StateProc As Boolean
    Private Sub DynamicResponButtoneditSysHide_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        With CType(sender, DevExpress.XtraEditors.ButtonEdit)

            If Not (_StateProc) Then
                _StateProc = True

                Dim _BrowseID As Integer
                Dim _Name As String
                Dim _Data As String
                Dim _BrowseCmd As String = ""
                Dim _BrowseSortCmd As String = ""
                Dim _BrowseWhereCmd As String = ""
                Dim _FTBrwCmdField As String = ""
                Dim _FTBrwCmdFieldOptional As String = ""
                Dim FTBrwCmdGroupBy As String = ""
                Dim _ConFiledName As String = ""

                Dim _form As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm
                If IsNothing(_form) Then _form = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent

                With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)

                    _Name = .Name.ToString.Replace("Res_", "")

                    _Data = .Text
                    If .Properties.Buttons.Count > 1 Then
                        If UCase("" & .Properties.Buttons.Item(1).Tag) = UCase("d") Then
                            Dim T As System.Type = _form.GetType()

                            Dim _pdbnameinfo As PropertyInfo
                            Dim _ptablenameinfo As PropertyInfo
                            Dim _pdoctypeinfo As PropertyInfo

                            Dim _minfo As MethodInfo

                            _pdbnameinfo = T.GetProperty("SysDBName")
                            _ptablenameinfo = T.GetProperty("SysTableName")
                            _pdoctypeinfo = T.GetProperty("SysDocType")
                            _minfo = T.GetMethod("InitData")

                            If Not (_pdbnameinfo Is Nothing) AndAlso Not (_ptablenameinfo Is Nothing) AndAlso Not (_pdoctypeinfo Is Nothing) Then

                                If .Text = HI.TL.Document.GetDocumentNo(_pdbnameinfo.GetValue(_form, Nothing).ToString, _ptablenameinfo.GetValue(_form, Nothing).ToString, _pdoctypeinfo.GetValue(_form, Nothing).ToString, True) Then
                                    .Properties.Tag = ""
                                    _StateProc = False
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If
                    .Properties.Tag = ""

                    _BrowseID = Val("" & .Properties.Buttons.Item(0).Tag)

                    Dim _Qrysql As String = ""
                    Dim _dtbrw As New DataTable
                    Dim _dtret As New DataTable
                    Dim _Where As String = ""

                    _Where = HI.TL.HandlerControl.GridViewGetBrowseRespon(sender, CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView), _BrowseID, _dtret, _BrowseCmd, _BrowseSortCmd, _BrowseWhereCmd, _FTBrwCmdField, _FTBrwCmdFieldOptional, FTBrwCmdGroupBy, _Qrysql, _ConFiledName, _Data, True)

                    If _Qrysql = "" Then
                        _StateProc = False
                        Exit Sub
                    End If

                    _Qrysql = _BrowseCmd.ToUpper.Replace("SELECT", " SELECT  ") & " " & _BrowseWhereCmd & _Where & "  " & FTBrwCmdGroupBy & " " & _BrowseSortCmd

                    If _Where <> "" AndAlso _ConFiledName <> "" Then
                        _dtbrw = New DataTable
                        _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)

                        If _dtbrw.Rows.Count > 0 Then
                            .Properties.Tag = _Data
                        Else
                            .Properties.Tag = ""
                        End If

                        With _dtbrw
                            If .Rows.Count > 0 Then
                                For Each Row As DataRow In _dtret.Rows
                                    If .Columns.IndexOf(Row!FTBrwField.ToString) >= 0 Then

                                        Select Case Microsoft.VisualBasic.Left(Row!FTBrwField.ToString.ToUpper, 2)
                                            Case "FN", "FC", "FS"
                                                If IsNumeric(.Rows(0).Item(Row!FTBrwField.ToString).ToString) Then
                                                    Row!ValuesRet = CDbl(.Rows(0).Item(Row!FTBrwField.ToString).ToString)
                                                Else
                                                    Row!ValuesRet = "0"
                                                End If
                                            Case Else
                                                Row!ValuesRet = .Rows(0).Item(Row!FTBrwField.ToString).ToString
                                        End Select
                                    Else

                                        Select Case Microsoft.VisualBasic.Left(Row!FTBrwField.ToString.ToUpper, 2)
                                            Case "FN", "FC", "FS"
                                                Row!ValuesRet = "0"
                                            Case Else
                                                Row!ValuesRet = ""
                                        End Select
                                    End If
                                Next
                            Else
                                For Each Row As DataRow In _dtret.Rows

                                    Select Case Microsoft.VisualBasic.Left(Row!FTBrwField.ToString.ToUpper, 2)
                                        Case "FN", "FC", "FS"
                                            Row!ValuesRet = "0"
                                        Case Else
                                            Row!ValuesRet = ""
                                    End Select

                                Next
                            End If
                        End With

                        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                            For Each Row As DataRow In _dtret.Select("NOT(FTRetField IS NULL)")
                                If (Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatSizeCode".Length) = Row!FTRetField.ToString.ToUpper Or Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = Row!FTRetField.ToString.ToUpper) And Row!FTStatePropertyTag.ToString <> "Y" Then
                                    Continue For
                                End If

                                Dim _ColName As String = Row!FTRetField.ToString
                                If Not (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                Else
                                    _ColName = _ColName & "" & .FocusedColumn.Name.ToString

                                    If (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                        If Microsoft.VisualBasic.Left(Row!FTRetField, Row!FTRetField.ToString.Length) = Microsoft.VisualBasic.Left(.FocusedColumn.Name.ToString, Row!FTRetField.ToString.Length) Then
                                            _ColName = .FocusedColumn.Name.ToString
                                        End If
                                    End If
                                End If


                                If Not (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then

                                    Dim ctrl As Object = .Columns.ColumnByFieldName(_ColName).ColumnEdit

                                    If ctrl Is Nothing Then

                                        Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                            Case "System.Int32".ToUpper
                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                            Case "System.String".ToUpper
                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                            Case Else
                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                        End Select
                                    Else
                                        If Row!FTStatePropertyTag.ToString = "Y" Then
                                            Try
                                                If ctrl Is Nothing Then
                                                    Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                        Case "System.Int32".ToUpper
                                                            .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                        Case "System.String".ToUpper
                                                            .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                        Case Else
                                                            .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                    End Select
                                                Else
                                                    Try



                                                        If Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatSizeCode".Length) = "FTRawMatSizeCode".ToUpper Then
                                                            Dim Col1 As String = .FocusedColumn.FieldName.ToString
                                                            Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                                                            Select Case .Columns.ColumnByFieldName(Col2).ColumnType.FullName.ToString.ToUpper
                                                                Case "System.Int32".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, Col2, Val(Row!ValuesRet.ToString))

                                                                Case "System.String".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                                Case Else
                                                                    .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                            End Select
                                                        ElseIf Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = "FTRawMatColorCode".ToUpper Then
                                                            Dim Col1 As String = .FocusedColumn.FieldName.ToString
                                                            Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)

                                                            Select Case .Columns.ColumnByFieldName(Col2).ColumnType.FullName.ToString.ToUpper
                                                                Case "System.Int32".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, Col2, Val(Row!ValuesRet.ToString))

                                                                Case "System.String".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                                Case Else
                                                                    .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                            End Select
                                                        Else
                                                            Select Case .Columns.ColumnByFieldName(_ColName & "_Hide").ColumnType.FullName.ToString.ToUpper
                                                                Case "System.Int32".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Val(Row!ValuesRet.ToString))
                                                                Case "System.String".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Row!ValuesRet.ToString)
                                                                Case Else
                                                                    .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Row!ValuesRet.ToString)
                                                            End Select
                                                        End If

                                                    Catch ex As Exception
                                                    End Try

                                                    'Try
                                                    '    With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
                                                    '        .Text = Row!ValuesRet.ToString
                                                    '    End With
                                                    'Catch ex As Exception
                                                    'End Try

                                                    Try
                                                        With CType(ctrl, DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit)
                                                            .Tag = Row!ValuesRet.ToString
                                                        End With
                                                    Catch ex As Exception

                                                    End Try

                                                End If
                                            Catch ex As Exception
                                            End Try

                                        Else


                                            If Row!FTRetField.ToString.ToUpper = _Name.ToUpper Then
                                            Else
                                                Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                    Case "System.Int32".ToUpper
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                    Case "System.String".ToUpper
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                    Case Else
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                End Select
                                            End If

                                        End If
                                    End If



                                End If
                            Next
                        End With


                    End If

                    _dtbrw.Dispose()
                    _dtret.Dispose()

                End With

                _StateProc = False
            End If

        End With
    End Sub

    Private Sub DynamicResponButtoneditSysHide_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)

        With CType(sender, DevExpress.XtraEditors.ButtonEdit)
            If .Text <> "" Then

                Dim _value As String = ""
                With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                    If Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatSizeCode".Length) = "FTRawMatSizeCode".ToUpper Then
                        Dim Col1 As String = .FocusedColumn.FieldName.ToString
                        Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                        Select Case .Columns.ColumnByFieldName(Col2).ColumnType.FullName.ToString.ToUpper
                            Case "System.Int32".ToUpper
                                _value = "" & .GetRowCellValue(.FocusedRowHandle, Col2).ToString
                            Case "System.String".ToUpper
                                _value = "" & .GetRowCellValue(.FocusedRowHandle, Col2).ToString()
                            Case Else
                                _value = "" & .GetRowCellValue(.FocusedRowHandle, Col2).ToString()
                        End Select
                    ElseIf Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = "FTRawMatColorCode".ToUpper Then
                        Dim Col1 As String = .FocusedColumn.FieldName.ToString
                        Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)

                        Select Case .Columns.ColumnByFieldName(Col2).ColumnType.FullName.ToString.ToUpper
                            Case "System.Int32".ToUpper
                                _value = "" & .GetRowCellValue(.FocusedRowHandle, Col2).ToString
                            Case "System.String".ToUpper
                                _value = "" & .GetRowCellValue(.FocusedRowHandle, Col2).ToString()
                            Case Else
                                _value = "" & .GetRowCellValue(.FocusedRowHandle, Col2).ToString()
                        End Select
                    End If


                    If _value = "0" Or _value = "" Then
                        .SetFocusedRowCellValue(.FocusedColumn, "")
                    End If

                End With
            End If
        End With

    End Sub

    Private Sub DynamicResponButtone_Gotocus(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim _Data As String

        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                _Data = "" & .GetRowCellValue(.FocusedRowHandle, .FocusedColumn).ToString
            End With
        Catch ex As Exception
            _Data = ""
        End Try

        Try
            With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
                .Text = _Data
                .Properties.Tag = .Text
            End With
        Catch ex As Exception
        End Try

    End Sub

    Private Sub DynamicResponButtoneSysHideColor_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Select Case e.Button.Index
            Case 0

                If Val(e.Button.Tag.ToString) <= 0 Then Exit Sub

                If Not (_StateProc) Then
                    _StateProcColor = True
                    Dim _form As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm
                    With New HI.TL.wDynamicBrowseInfo(Val(e.Button.Tag.ToString), _form)
                        .BrowseID = Val(e.Button.Tag.ToString)

                        .X = MousePosition.X
                        .Y = MousePosition.Y

                        Dim _Qrysql As String = ""
                        Dim _dtbrw As New DataTable
                        Dim _dtret As New DataTable
                        Dim _BrowseCmd As String = ""
                        Dim _BrowseSortCmd As String = ""
                        Dim _BrowseWhereCmd As String = ""
                        Dim _FTBrwCmdField As String = ""
                        Dim _FTBrwCmdFieldOptional As String = ""
                        Dim FTBrwCmdGroupBy As String = ""
                        Dim _Where As String = ""
                        Dim _ConFiledName As String = ""
                        _Where = HI.TL.HandlerControl.GridViewGetBrowseRespon(sender, CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView), .BrowseID, _dtret, _BrowseCmd, _BrowseSortCmd, _BrowseWhereCmd, _FTBrwCmdField, _FTBrwCmdFieldOptional, FTBrwCmdGroupBy, _Qrysql, "", "")

                        If _Qrysql = "" Then
                            _StateProcColor = False
                            Exit Sub
                        End If

                        _dtbrw = New DataTable
                        _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql & "  " & _BrowseWhereCmd & "  " & _Where & "  " & FTBrwCmdGroupBy & "  " & _BrowseSortCmd, Conn.DB.DataBaseName.DB_SYSTEM)

                        .Data = _dtbrw.Copy
                        .DataRetField = _dtret.Copy

                        _dtbrw.Dispose()
                        _dtret.Dispose()

                        .ShowDialog()

                        If Not (.ValuesReturn Is Nothing) Then

                            For Each Row As DataRow In .DataRetField.Select("NOT(FTRetField IS NULL)")
                                With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                                    Dim _ColName As String = Row!FTRetField.ToString

                                    If Not (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                    Else
                                        _ColName = _ColName & "" & .FocusedColumn.Name.ToString

                                        If (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                            If Microsoft.VisualBasic.Left(Row!FTRetField, Row!FTRetField.ToString.Length) = Microsoft.VisualBasic.Left(.FocusedColumn.Name.ToString, Row!FTRetField.ToString.Length) Then
                                                _ColName = .FocusedColumn.Name.ToString
                                            End If
                                        End If
                                    End If

                                    If Not (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                        Dim ctrl As Object = .Columns.ColumnByFieldName(_ColName).ColumnEdit

                                        If ctrl Is Nothing Then

                                            Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                Case "System.Int32".ToUpper
                                                    .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                Case "System.String".ToUpper
                                                    .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                Case Else
                                                    .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                            End Select
                                        Else
                                            If Row!FTStatePropertyTag.ToString = "Y" Then
                                                Try
                                                    If ctrl Is Nothing Then
                                                        Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                            Case "System.Int32".ToUpper
                                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                            Case "System.String".ToUpper
                                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                            Case Else
                                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                        End Select
                                                    Else

                                                        Try

                                                            If Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatSizeCode".Length) = "FTRawMatSizeCode".ToUpper Then
                                                                Dim Col1 As String = .FocusedColumn.FieldName.ToString
                                                                Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                                                                Select Case .Columns.ColumnByFieldName(Col2).ColumnType.FullName.ToString.ToUpper
                                                                    Case "System.Int32".ToUpper
                                                                        .SetRowCellValue(.FocusedRowHandle, Col2, Val(Row!ValuesRet.ToString))

                                                                    Case "System.String".ToUpper
                                                                        .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                                    Case Else
                                                                        .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                                End Select
                                                            ElseIf Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = "FTRawMatColorCode".ToUpper Then
                                                                Dim Col1 As String = .FocusedColumn.FieldName.ToString
                                                                Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)

                                                                Select Case .Columns.ColumnByFieldName(Col2).ColumnType.FullName.ToString.ToUpper
                                                                    Case "System.Int32".ToUpper
                                                                        .SetRowCellValue(.FocusedRowHandle, Col2, Val(Row!ValuesRet.ToString))

                                                                    Case "System.String".ToUpper
                                                                        .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                                    Case Else
                                                                        .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                                End Select
                                                            Else
                                                                Select Case .Columns.ColumnByFieldName(_ColName & "_Hide").ColumnType.FullName.ToString.ToUpper
                                                                    Case "System.Int32".ToUpper
                                                                        .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Val(Row!ValuesRet.ToString))
                                                                    Case "System.String".ToUpper
                                                                        .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Row!ValuesRet.ToString)
                                                                    Case Else
                                                                        .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Row!ValuesRet.ToString)
                                                                End Select
                                                            End If
                                                        Catch ex As Exception
                                                        End Try

                                                        'Try
                                                        '    With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
                                                        '        .Text = Row!ValuesRet.ToString
                                                        '    End With
                                                        'Catch ex As Exception
                                                        'End Try

                                                        Try
                                                            With CType(ctrl, DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit)
                                                                .Tag = Row!ValuesRet.ToString
                                                            End With
                                                        Catch ex As Exception
                                                        End Try

                                                    End If
                                                Catch ex As Exception
                                                End Try
                                            Else


                                                Try
                                                    With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
                                                        .Text = Row!ValuesRet.ToString
                                                    End With
                                                Catch ex As Exception
                                                End Try

                                                Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                    Case "System.Int32".ToUpper
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                    Case "System.String".ToUpper
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                    Case Else
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                End Select
                                            End If
                                        End If
                                    End If
                                End With
                            Next
                        End If

                        .Data.Dispose()
                        .DataRetField.Dispose()

                        Call SetColorDescription(CType(sender, DevExpress.XtraEditors.ButtonEdit).Text)

                    End With

                    _StateProcColor = False

                    With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                        Try
                            Dim IC As Integer = 1
                            Do While .Columns(.FocusedColumn.AbsoluteIndex + IC).Visible = False
                                IC = IC + 1
                            Loop
                            .FocusedColumn = .Columns(.FocusedColumn.AbsoluteIndex + IC)
                        Catch ex As Exception
                        End Try
                    End With

                End If

        End Select
    End Sub

    Private _StateProcColor As Boolean
    Private Sub DynamicResponButtoneditSysHideColor_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        With CType(sender, DevExpress.XtraEditors.ButtonEdit)

            If Not (_StateProcColor) Then
                _StateProcColor = True

                Dim _BrowseID As Integer
                Dim _Name As String
                Dim _Data As String
                Dim _BrowseCmd As String = ""
                Dim _BrowseSortCmd As String = ""
                Dim _BrowseWhereCmd As String = ""
                Dim _FTBrwCmdField As String = ""
                Dim _FTBrwCmdFieldOptional As String = ""
                Dim FTBrwCmdGroupBy As String = ""
                Dim _ConFiledName As String = ""

                Dim _form As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm
                If IsNothing(_form) Then _form = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent

                With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)

                    _Name = .Name.ToString.Replace("Res_", "")

                    _Data = .Text
                    If .Properties.Buttons.Count > 1 Then
                        If UCase("" & .Properties.Buttons.Item(1).Tag) = UCase("d") Then
                            Dim T As System.Type = _form.GetType()

                            Dim _pdbnameinfo As PropertyInfo
                            Dim _ptablenameinfo As PropertyInfo
                            Dim _pdoctypeinfo As PropertyInfo

                            Dim _minfo As MethodInfo

                            _pdbnameinfo = T.GetProperty("SysDBName")
                            _ptablenameinfo = T.GetProperty("SysTableName")
                            _pdoctypeinfo = T.GetProperty("SysDocType")
                            _minfo = T.GetMethod("InitData")

                            If Not (_pdbnameinfo Is Nothing) AndAlso Not (_ptablenameinfo Is Nothing) AndAlso Not (_pdoctypeinfo Is Nothing) Then

                                If .Text = HI.TL.Document.GetDocumentNo(_pdbnameinfo.GetValue(_form, Nothing).ToString, _ptablenameinfo.GetValue(_form, Nothing).ToString, _pdoctypeinfo.GetValue(_form, Nothing).ToString, True) Then
                                    .Properties.Tag = ""
                                    _StateProcColor = False
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If
                    .Properties.Tag = ""

                    _BrowseID = Val("" & .Properties.Buttons.Item(0).Tag)

                    Dim _Qrysql As String = ""
                    Dim _dtbrw As New DataTable
                    Dim _dtret As New DataTable
                    Dim _Where As String = ""

                    _Where = HI.TL.HandlerControl.GridViewGetBrowseRespon(sender, CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView), _BrowseID, _dtret, _BrowseCmd, _BrowseSortCmd, _BrowseWhereCmd, _FTBrwCmdField, _FTBrwCmdFieldOptional, FTBrwCmdGroupBy, _Qrysql, _ConFiledName, _Data, True)

                    If _Qrysql = "" Then
                        _StateProcColor = False
                        Exit Sub
                    End If

                    _Qrysql = _BrowseCmd.ToUpper.Replace("SELECT", " SELECT  ") & " " & _BrowseWhereCmd & _Where & "  " & FTBrwCmdGroupBy & " " & _BrowseSortCmd

                    If _Where <> "" AndAlso _ConFiledName <> "" Then
                        _dtbrw = New DataTable
                        _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)

                        If _dtbrw.Rows.Count > 0 Then
                            .Properties.Tag = _Data
                        Else
                            .Properties.Tag = ""
                        End If

                        With _dtbrw
                            If .Rows.Count > 0 Then
                                For Each Row As DataRow In _dtret.Rows
                                    If .Columns.IndexOf(Row!FTBrwField.ToString) >= 0 Then

                                        Select Case Microsoft.VisualBasic.Left(Row!FTBrwField.ToString.ToUpper, 2)
                                            Case "FN", "FC", "FS"
                                                If IsNumeric(.Rows(0).Item(Row!FTBrwField.ToString).ToString) Then
                                                    Row!ValuesRet = CDbl(.Rows(0).Item(Row!FTBrwField.ToString).ToString)
                                                Else
                                                    Row!ValuesRet = "0"
                                                End If
                                            Case Else
                                                Row!ValuesRet = .Rows(0).Item(Row!FTBrwField.ToString).ToString
                                        End Select
                                    Else

                                        Select Case Microsoft.VisualBasic.Left(Row!FTBrwField.ToString.ToUpper, 2)
                                            Case "FN", "FC", "FS"
                                                Row!ValuesRet = "0"
                                            Case Else
                                                Row!ValuesRet = ""
                                        End Select
                                    End If
                                Next
                            Else
                                For Each Row As DataRow In _dtret.Rows

                                    Select Case Microsoft.VisualBasic.Left(Row!FTBrwField.ToString.ToUpper, 2)
                                        Case "FN", "FC", "FS"
                                            Row!ValuesRet = "0"
                                        Case Else
                                            Row!ValuesRet = ""
                                    End Select

                                Next
                            End If
                        End With

                        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                            For Each Row As DataRow In _dtret.Select("NOT(FTRetField IS NULL)")
                                If (Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatSizeCode".Length) = Row!FTRetField.ToString.ToUpper Or Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = Row!FTRetField.ToString.ToUpper) And Row!FTStatePropertyTag.ToString <> "Y" Then
                                    Continue For
                                End If

                                Dim _ColName As String = Row!FTRetField.ToString
                                If Not (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                Else
                                    _ColName = _ColName & "" & .FocusedColumn.Name.ToString

                                    If (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                        If Microsoft.VisualBasic.Left(Row!FTRetField, Row!FTRetField.ToString.Length) = Microsoft.VisualBasic.Left(.FocusedColumn.Name.ToString, Row!FTRetField.ToString.Length) Then
                                            _ColName = .FocusedColumn.Name.ToString
                                        End If
                                    End If
                                End If

                                If Not (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then

                                    Dim ctrl As Object = .Columns.ColumnByFieldName(_ColName).ColumnEdit

                                    If ctrl Is Nothing Then

                                        Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                            Case "System.Int32".ToUpper
                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                            Case "System.String".ToUpper
                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                            Case Else
                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                        End Select
                                    Else
                                        If Row!FTStatePropertyTag.ToString = "Y" Then
                                            Try
                                                If ctrl Is Nothing Then

                                                    Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                        Case "System.Int32".ToUpper
                                                            .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                        Case "System.String".ToUpper
                                                            .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                        Case Else
                                                            .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                    End Select

                                                Else

                                                    Try
                                                        If Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatSizeCode".Length) = "FTRawMatSizeCode".ToUpper Then
                                                            Dim Col1 As String = .FocusedColumn.FieldName.ToString
                                                            Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                                                            Select Case .Columns.ColumnByFieldName(Col2).ColumnType.FullName.ToString.ToUpper
                                                                Case "System.Int32".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, Col2, Val(Row!ValuesRet.ToString))

                                                                Case "System.String".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                                Case Else
                                                                    .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                            End Select
                                                        ElseIf Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = "FTRawMatColorCode".ToUpper Then
                                                            Dim Col1 As String = .FocusedColumn.FieldName.ToString
                                                            Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)

                                                            Select Case .Columns.ColumnByFieldName(Col2).ColumnType.FullName.ToString.ToUpper
                                                                Case "System.Int32".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, Col2, Val(Row!ValuesRet.ToString))

                                                                Case "System.String".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                                Case Else
                                                                    .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                            End Select
                                                        Else
                                                            Select Case .Columns.ColumnByFieldName(_ColName & "_Hide").ColumnType.FullName.ToString.ToUpper
                                                                Case "System.Int32".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Val(Row!ValuesRet.ToString))
                                                                Case "System.String".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Row!ValuesRet.ToString)
                                                                Case Else
                                                                    .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Row!ValuesRet.ToString)
                                                            End Select
                                                        End If

                                                    Catch ex As Exception
                                                    End Try

                                                    Try
                                                        With CType(ctrl, DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit)
                                                            .Tag = Row!ValuesRet.ToString
                                                        End With
                                                    Catch ex As Exception
                                                    End Try
                                                End If

                                            Catch ex As Exception
                                            End Try

                                        Else

                                            If Row!FTRetField.ToString.ToUpper = _Name.ToUpper Then
                                            Else
                                                Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                    Case "System.Int32".ToUpper
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                    Case "System.String".ToUpper
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                    Case Else
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                End Select
                                            End If

                                        End If
                                    End If
                                End If
                            Next


                        End With
                    End If

                    _dtbrw.Dispose()
                    _dtret.Dispose()

                    Call SetColorDescription(.Text)

                End With

                _StateProcColor = False
            End If

        End With
    End Sub

    Private Sub SetColorDescription(_ColorCode As String)
        If _ColorCode <> "" Then
            Try
                Dim _Found As Boolean = False
                With ogvcolor
                    If Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = "FTRawMatColorCode".ToUpper Then

                        Dim Col1 As String = .FocusedColumn.FieldName.ToString
                        Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)
                        Dim Col3 As String = "FTRawMatColorNameTH" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)
                        Dim Col4 As String = "FTRawMatColorNameEN" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)
                        Dim SysMatCode As String = .GetRowCellValue(.FocusedRowHandle, "FTItemNo")
                        Dim SysStyleID As String = .GetRowCellValue(.FocusedRowHandle, "FNHSysStyleDevId")
                        Dim SysColorID As String = .GetRowCellValue(.FocusedRowHandle, Col2)

                        Dim _DescTH As String = ""
                        Dim _DescEN As String = ""

                        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                            If GridCol.FieldName <> .FocusedColumn.FieldName Then
                                If Microsoft.VisualBasic.Left(GridCol.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = "FTRawMatColorCode".ToUpper Then
                                    If .GetFocusedRowCellValue(GridCol.FieldName).ToString = _ColorCode Then
                                        Dim Col5 As String = "FTRawMatColorNameTH" & Microsoft.VisualBasic.Right(GridCol.FieldName.ToString, GridCol.FieldName.ToString.Length - "FTRawMatColorCode".Length)
                                        Dim Col6 As String = "FTRawMatColorNameEN" & Microsoft.VisualBasic.Right(GridCol.FieldName.ToString, GridCol.FieldName.ToString.Length - "FTRawMatColorCode".Length)

                                        Try
                                            _DescTH = .GetRowCellValue(.FocusedRowHandle, Col5)
                                            _DescEN = .GetRowCellValue(.FocusedRowHandle, Col6)
                                        Catch ex As Exception
                                        End Try

                                        _Found = True
                                        Exit For

                                    End If
                                End If
                            End If
                        Next

                        If Not (_Found) Then
                            Dim _Qry As String = ""
                            Dim dt As DataTable

                            _Qry = " SELECT  TOP 1 C.FTRawMatColorNameTH, C.FTRawMatColorNameEN  "
                            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_ColorWay AS C WITH(NOLOCK) INNER JOIN"
                            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Mat AS B WITH(NOLOCK)  ON C.FNHSysStyleDevId = B.FNHSysStyleDevId AND C.FNSeq = B.FNSeq INNER JOIN"
                            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS A WITH(NOLOCK)  ON B.FNHSysMerMatId = A.FNHSysMainMatId"
                            _Qry &= vbCrLf & "  WHERE  A.FTItemNo='" & HI.UL.ULF.rpQuoted(SysMatCode) & "' "
                            _Qry &= vbCrLf & "  AND   C.FNHSysStyleDevId = " & Integer.Parse(Val(FNHSysStyleDevId.Properties.Tag.ToString)) & " "
                            _Qry &= vbCrLf & "  AND   C.FNHSysRawMatColorId = " & Integer.Parse(Val(SysColorID)) & " "

                            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            If dt.Rows.Count <= 0 Then

                                _Qry = " SELECT  TOP 1   FNHSysRawMatColorId, FTRawMatColorNameTH, FTRawMatColorNameEN"
                                _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C  WITH(NOLOCK) "
                                _Qry &= vbCrLf & "  WHERE FNHSysRawMatColorId = " & Integer.Parse(Val(SysColorID)) & " "
                                dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            End If

                            For Each R As DataRow In dt.Rows
                                _DescTH = R!FTRawMatColorNameTH.ToString
                                _DescEN = R!FTRawMatColorNameEN.ToString
                                Exit For
                            Next
                            dt.Dispose()
                        End If

                        .SetRowCellValue(.FocusedRowHandle, Col3, _DescTH)
                        .SetRowCellValue(.FocusedRowHandle, Col4, _DescEN)

                    End If
                End With
            Catch ex As Exception

            End Try


        Else
            Try
                With ogvcolor
                    If Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = "FTRawMatColorCode".ToUpper Then

                        Dim Col1 As String = .FocusedColumn.FieldName.ToString
                        Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)
                        Dim Col3 As String = "FTRawMatColorNameTH" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)
                        Dim Col4 As String = "FTRawMatColorNameEN" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)

                        .SetRowCellValue(.FocusedRowHandle, Col3, "")
                        .SetRowCellValue(.FocusedRowHandle, Col4, "")

                    End If

                End With
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub DynamicResponButtoneditSysHideColor_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)

        With CType(sender, DevExpress.XtraEditors.ButtonEdit)
            If .Text <> "" Then

                Dim _value As String = ""
                With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                    If Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatSizeCode".Length) = "FTRawMatSizeCode".ToUpper Then
                        Dim Col1 As String = .FocusedColumn.FieldName.ToString
                        Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                        Select Case .Columns.ColumnByFieldName(Col2).ColumnType.FullName.ToString.ToUpper
                            Case "System.Int32".ToUpper
                                _value = "" & .GetRowCellValue(.FocusedRowHandle, Col2).ToString
                            Case "System.String".ToUpper
                                _value = "" & .GetRowCellValue(.FocusedRowHandle, Col2).ToString()
                            Case Else
                                _value = "" & .GetRowCellValue(.FocusedRowHandle, Col2).ToString()
                        End Select
                    ElseIf Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = "FTRawMatColorCode".ToUpper Then
                        Dim Col1 As String = .FocusedColumn.FieldName.ToString
                        Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)

                        Select Case .Columns.ColumnByFieldName(Col2).ColumnType.FullName.ToString.ToUpper
                            Case "System.Int32".ToUpper
                                _value = "" & .GetRowCellValue(.FocusedRowHandle, Col2).ToString
                            Case "System.String".ToUpper
                                _value = "" & .GetRowCellValue(.FocusedRowHandle, Col2).ToString()
                            Case Else
                                _value = "" & .GetRowCellValue(.FocusedRowHandle, Col2).ToString()
                        End Select

                        If _value = "0" Or _value = "" Then

                            Dim Col3 As String = "FTRawMatColorNameTH" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)
                            Dim Col4 As String = "FTRawMatColorNameEN" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)

                            .SetRowCellValue(.FocusedRowHandle, Col3, "")
                            .SetRowCellValue(.FocusedRowHandle, Col4, "")

                        End If
                    End If


                    If _value = "0" Or _value = "" Then
                        .SetFocusedRowCellValue(.FocusedColumn, "")

                    End If

                End With
            End If
        End With

    End Sub

    Private Sub DynamicResponButtoneColor_Gotocus(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim _Data As String

        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                _Data = "" & .GetRowCellValue(.FocusedRowHandle, .FocusedColumn).ToString
            End With
        Catch ex As Exception
            _Data = ""
        End Try

        Try
            With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
                .Text = _Data
                .Properties.Tag = .Text
            End With
        Catch ex As Exception
        End Try

    End Sub
#End Region

    Private Sub RepRawMatIDCaledit_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepRawMatIDCaledit.EditValueChanging
        With Me.ogvcolor
            If .FocusedRowHandle < 0 Then Exit Sub
            Dim _ColName As String = .FocusedColumn.FieldName.ToString

            If Not (e.NewValue = e.OldValue) Then
                If e.NewValue = 0 Or e.NewValue = -1 Then
                    .SetFocusedRowCellValue("" & _ColName, "")
                End If
            End If

        End With
    End Sub

    Private Sub GridView2_CellValueChanged(sender As Object, e As Views.Base.CellValueChangedEventArgs) Handles ogvcolor.CellValueChanged
        With Me.ogvcolor
            If .FocusedRowHandle < 0 Then Exit Sub
            Static Proc As Boolean
            If Not (Proc) Then
                Proc = True
                Try
                    Dim _ColName As String = Microsoft.VisualBasic.Left(e.Column.FieldName.ToString.ToUpper, ("FNHSysRawMatColorIdFTRawMatColorCode").Length)

                    If _ColName.ToUpper = ("FNHSysRawMatColorIdFTRawMatColorCode").ToUpper Then
                        Dim ColorWay As String = Microsoft.VisualBasic.Right(e.Column.FieldName.ToString, e.Column.FieldName.ToString.Length - ("FNHSysRawMatColorIdFTRawMatColorCode").Length)

                        If Integer.Parse(Val(e.Value)) = 0 Then
                            .SetRowCellValue(e.RowHandle, "FTRawMatColorNameTH" & ColorWay, "")
                            .SetRowCellValue(e.RowHandle, "FTRawMatColorNameEN" & ColorWay, "")
                        Else
                            Dim _ItemCode As String = "" & .GetRowCellValue(e.RowHandle, "FTItemNo").ToString
                            Dim _Qry As String = ""
                            Dim dt As DataTable

                            _Qry = " SELECT  TOP 1  A.FNHSysRawMatColorId "
                            _Qry &= vbCrLf & "  ,ISNULL(B.FTRawMatColorNameTH,A.FTRawMatColorNameTH) AS FTRawMatColorNameTH"
                            _Qry &= vbCrLf & "  ,ISNULL(B.FTRawMatColorNameEN,A.FTRawMatColorNameEN) AS FTRawMatColorNameEN"
                            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS A WITH(NOLOCK)  LEFT JOIN"
                            _Qry &= vbCrLf & "  (SELECT        B.FNHSysRawMatColorId, B.FTRawMatColorNameTH, B.FTRawMatColorNameEN"
                            _Qry &= vbCrLf & "   FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Mat AS A WITH(NOLOCK)  INNER JOIN"
                            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_ColorWay AS B WITH(NOLOCK)  ON A.FNHSysStyleDevId = B.FNHSysStyleDevId AND A.FNSeq = B.FNSeq INNER JOIN"
                            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS M WITH(NOLOCK) ON A.FNHSysMerMatId = M.FNHSysMainMatId"
                            _Qry &= vbCrLf & "   WHERE        (M.FTItemNo = N'" & HI.UL.ULF.rpQuoted(_ItemCode) & "')"
                            _Qry &= vbCrLf & "  AND  B.FNHSysRawMatColorId=" & Integer.Parse(Val(e.Value)) & " "
                            _Qry &= vbCrLf & "  ) AS B "
                            _Qry &= vbCrLf & "  ON A.FNHSysRawMatColorId = B.FNHSysRawMatColorId"
                            _Qry &= vbCrLf & "  WHERE  A.FNHSysRawMatColorId=" & Integer.Parse(Val(e.Value)) & " "

                            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            For Each R As DataRow In dt.Rows
                                .SetRowCellValue(e.RowHandle, "FTRawMatColorNameTH" & ColorWay, R!FTRawMatColorNameTH.ToString)
                                .SetRowCellValue(e.RowHandle, "FTRawMatColorNameEN" & ColorWay, R!FTRawMatColorNameEN.ToString)
                                Exit For
                            Next

                            dt.Dispose()

                        End If
                    End If
                Catch ex As Exception

                End Try
                Proc = False
            End If

        End With
    End Sub

    Private Sub ocmchangematcolordes_Click(sender As Object, e As EventArgs) Handles ocmchangematcolordes.Click
        Try

            If CheckOwner() = False Then
                Exit Sub
            End If

            If sFNHSysStyleDevId = "" Then Return
            Call LoadStylePostInfo()
            If CheckPostDataToBomSheet() = False Then Exit Sub
            With Me.ogvcolor
                If .FocusedRowHandle < 0 Then Exit Sub
                If Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = "FTRawMatColorCode".ToUpper Then

                    CType(ogccolor.DataSource, DataTable).AcceptChanges()

                    Dim Col1 As String = .FocusedColumn.FieldName.ToString
                    Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)
                    Dim Col3 As String = "FTRawMatColorNameTH" & .FocusedColumn.FieldName.ToString.Replace("FTRawMatColorCode", "")
                    Dim Col4 As String = "FTRawMatColorNameEN" & .FocusedColumn.FieldName.ToString.Replace("FTRawMatColorCode", "")

                    If .GetRowCellValue(.FocusedRowHandle, Col2).ToString <> "" Then
                        Dim _Qry As String = ""
                        Dim _ItemCode As String = "" & .GetRowCellValue(ogvcolor.FocusedRowHandle, "FTItemNo").ToString
                        Dim _State As Boolean = False
                        Dim _FTOrderNo As String = ""

                        _State = False

                        With _wChangeDesc

                            .FTRawMatColorNameTH.Text = "" & ogvcolor.GetRowCellValue(ogvcolor.FocusedRowHandle, Col3).ToString
                            .FTRawMatColorNameEN.Text = "" & ogvcolor.GetRowCellValue(ogvcolor.FocusedRowHandle, Col4).ToString
                            .FTRawMatColorNameTH.Properties.ReadOnly = _State
                            .FTRawMatColorNameEN.Properties.ReadOnly = _State

                            .ProcNew = False
                            .ShowDialog()

                            If .ProcNew Then

                                Dim _DescTH As String = .FTRawMatColorNameTH.Text.Trim
                                Dim _DescEN As String = .FTRawMatColorNameEN.Text.Trim

                                With ogvcolor
                                    ogvcolor.SetRowCellValue(ogvcolor.FocusedRowHandle, Col3, _DescTH) '.SetFocusedRowCellValue(Col3, _DescTH)
                                    ogvcolor.SetRowCellValue(ogvcolor.FocusedRowHandle, Col4, _DescEN) '.SetFocusedRowCellValue(Col4, _DescEN)

                                    Dim _ColorCode As String = "" & ogvcolor.GetRowCellValue(ogvcolor.FocusedRowHandle, Col1).ToString

                                    For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                                        If GridCol.FieldName <> .FocusedColumn.FieldName Then
                                            If Microsoft.VisualBasic.Left(GridCol.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = "FTRawMatColorCode".ToUpper Then
                                                If .GetFocusedRowCellValue(GridCol.FieldName).ToString = _ColorCode Then
                                                    Dim Col5 As String = "FTRawMatColorNameTH" & Microsoft.VisualBasic.Right(GridCol.FieldName.ToString, GridCol.FieldName.ToString.Length - "FTRawMatColorCode".Length)
                                                    Dim Col6 As String = "FTRawMatColorNameEN" & Microsoft.VisualBasic.Right(GridCol.FieldName.ToString, GridCol.FieldName.ToString.Length - "FTRawMatColorCode".Length)

                                                    Try

                                                        ogvcolor.SetRowCellValue(ogvcolor.FocusedRowHandle, Col5, _DescTH) '.SetFocusedRowCellValue(Col5, _DescTH)
                                                        ogvcolor.SetRowCellValue(ogvcolor.FocusedRowHandle, Col6, _DescEN) '.SetFocusedRowCellValue(Col6, _DescEN)

                                                    Catch ex As Exception
                                                    End Try

                                                End If
                                            End If
                                        End If
                                    Next
                                End With

                                CType(Me.ogccolor.DataSource, DataTable).AcceptChanges()
                            End If
                        End With

                    End If

                End If

            End With
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ReposFNMerMatSeq_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposFNMerMatSeq.EditValueChanging
        Try
            CType(ogcmat.DataSource, DataTable).AcceptChanges()
            With ogvmat
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim MaxSeq As Integer = CType(ogcmat.DataSource, DataTable).Select("FNMerMatSeq=" & e.NewValue & "").Length + 1
                .SetFocusedRowCellValue("FNPart", MaxSeq)
                .SetFocusedRowCellValue("FNMerMatSeq", e.NewValue)
                CType(ogcmat.DataSource, DataTable).AcceptChanges()

            End With
        Catch ex As Exception
        End Try

    End Sub

    Private Sub GridView2_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvcolor.KeyDown
        Try
            With Me.ogvcolor
                If .FocusedRowHandle < 0 Then Exit Sub
                Select Case e.KeyCode
                    Case Keys.F10
                        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                            If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatColorCode")) = "FTRawMatColorCode".ToUpper Then

                                Dim Col1 As String = GridCol.FieldName.ToString
                                Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)

                                .SetFocusedRowCellValue(GridCol.FieldName, "N/R")
                                .SetFocusedRowCellValue(Col2, "-1")

                            End If
                        Next

                        CType(ogccolor.DataSource, DataTable).AcceptChanges()
                        ogccolor.RefreshDataSource()

                    Case Keys.F11
                        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                            If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatColorCode")) = "FTRawMatColorCode".ToUpper Then

                                Dim Col1 As String = GridCol.FieldName.ToString
                                Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)
                                .SetFocusedRowCellValue(GridCol.FieldName, "")
                                .SetFocusedRowCellValue(Col2, "0")
                            End If
                        Next

                        CType(ogccolor.DataSource, DataTable).AcceptChanges()
                        ogccolor.RefreshDataSource()
                    Case Keys.F12
                        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                            If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatColorCode")) = "FTRawMatColorCode".ToUpper Then

                                Dim Code As String = GridCol.Caption
                                Dim _ColorInt As Integer = 99 ' Integer.Parse(Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysRawMatColorId FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor WITH(NOLOCK) WHERE FTRawMatColorCode='" & HI.UL.ULF.rpQuoted(Code) & "'  ", Conn.DB.DataBaseName.DB_MASTER, "0")))

                                If _ColorInt > 0 Then
                                    Dim Col1 As String = GridCol.FieldName.ToString
                                    Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)

                                    .SetFocusedRowCellValue(GridCol.FieldName, Code)
                                    .SetFocusedRowCellValue(Col2, _ColorInt.ToString)
                                End If

                            End If
                        Next

                        CType(ogccolor.DataSource, DataTable).AcceptChanges()
                        ogccolor.RefreshDataSource()
                End Select
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GridView3_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvsize.KeyDown
        Try
            With Me.ogvsize
                If .FocusedRowHandle < 0 Then Exit Sub
                Select Case e.KeyCode
                    Case Keys.F10

                        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                            If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatSizeCode")) = "FTRawMatSizeCode".ToUpper Then

                                Dim Col1 As String = GridCol.FieldName.ToString
                                Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                                .SetFocusedRowCellValue(GridCol.FieldName, "N/R")
                                .SetFocusedRowCellValue(Col2, "-1")
                            End If
                        Next

                        CType(ogcsize.DataSource, DataTable).AcceptChanges()
                        ogcsize.RefreshDataSource()

                    Case Keys.F11
                        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                            If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatSizeCode")) = "FTRawMatSizeCode".ToUpper Then
                                Dim Col1 As String = GridCol.FieldName.ToString
                                Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                                .SetFocusedRowCellValue(GridCol.FieldName, "")
                                .SetFocusedRowCellValue(Col2, "0")
                            End If
                        Next

                        CType(ogcsize.DataSource, DataTable).AcceptChanges()
                        ogcsize.RefreshDataSource()
                    Case Keys.F12
                        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                            If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatSizeCode")) = "FTRawMatSizeCode".ToUpper Then

                                Dim Code As String = GridCol.Caption
                                Dim _SizeInt As Integer = Integer.Parse(Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysRawMatSizeId FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize WITH(NOLOCK) WHERE FTRawMatSizeCode='" & HI.UL.ULF.rpQuoted(Code) & "'  ", Conn.DB.DataBaseName.DB_MASTER, "0")))

                                If _SizeInt > 0 Then
                                    Dim Col1 As String = GridCol.FieldName.ToString
                                    Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                                    .SetFocusedRowCellValue(GridCol.FieldName, Code)
                                    .SetFocusedRowCellValue(Col2, _SizeInt.ToString)
                                End If


                            End If
                        Next

                        CType(ogcsize.DataSource, DataTable).AcceptChanges()
                        ogcsize.RefreshDataSource()
                End Select
            End With
        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridView2_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles ogvcolor.RowCellStyle
        Try
            With ogvcolor
                If Microsoft.VisualBasic.Left(e.Column.FieldName.ToUpper, Len("FTRawMatColorCode")) = "FTRawMatColorCode".ToUpper Then
                    If .GetRowCellValue(e.RowHandle, e.Column) = "N/R" Then

                        e.Appearance.BackColor = System.Drawing.Color.LightCyan
                        e.Appearance.ForeColor = System.Drawing.Color.Blue


                    Else
                        Dim _ColorCode As String = "" & .GetRowCellValue(e.RowHandle, e.Column).ToString()

                        If _ColorCode <> "" Then

                            Dim Col5 As String = "FTRawMatColorNameTH" & Microsoft.VisualBasic.Right(e.Column.FieldName.ToString, e.Column.FieldName.ToString.Length - "FTRawMatColorCode".Length)
                            Dim Col6 As String = "FTRawMatColorNameEN" & Microsoft.VisualBasic.Right(e.Column.FieldName.ToString, e.Column.FieldName.ToString.Length - "FTRawMatColorCode".Length)
                            Dim _Qry As String = ""

                            Dim _TextEN As String = ""
                            Dim _TextTH As String = ""

                            Try

                                _TextTH = "" & .GetRowCellValue(e.RowHandle, Col5).ToString
                                _TextEN = "" & .GetRowCellValue(e.RowHandle, Col6).ToString

                            Catch ex As Exception
                            End Try

                            If _TextEN = "" Then
                                e.Appearance.ForeColor = System.Drawing.Color.Red
                            Else

                                _Qry = " SELECT TOP 1 FTRawMatColorCode "
                                _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS XC WITH(NOLOCK)"
                                _Qry &= vbCrLf & " WHERE FTRawMatColorCode='" & HI.UL.ULF.rpQuoted(_ColorCode.Trim()) & "'"
                                _Qry &= vbCrLf & " AND FTRawMatColorNameEN ='" & HI.UL.ULF.rpQuoted(_TextEN.Trim()) & "' "

                                If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                                    e.Appearance.ForeColor = System.Drawing.Color.Purple
                                End If

                            End If

                        End If

                    End If

                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private lastRowHandle As Integer = GridControl.InvalidRowHandle
    Private lastColumn As GridColumn = Nothing
    Private ttInfo As ToolTipControlInfo = Nothing

    Private Sub objToolTipController_GetActiveObjectInfo(sender As Object, e As ToolTipControllerGetActiveObjectInfoEventArgs) Handles objToolTipController.GetActiveObjectInfo
        Try
            If e.Info Is Nothing AndAlso e.SelectedControl Is ogccolor Then
                Dim view As GridView = TryCast(ogccolor.FocusedView, GridView)
                Dim info As GridHitInfo = view.CalcHitInfo(e.ControlMousePosition)

                If info.InRowCell AndAlso (info.RowHandle <> lastRowHandle OrElse info.Column IsNot lastColumn) Then
                    lastRowHandle = info.RowHandle
                    lastColumn = info.Column
                    Dim text As String = view.GetRowCellDisplayText(info.RowHandle, info.Column)
                    Dim cellKey As String = info.RowHandle.ToString() & " - " & info.Column.ToString()

                    If Microsoft.VisualBasic.Left(info.Column.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = "FTRawMatColorCode".ToUpper And text <> "" Then

                        Dim Col5 As String = "FTRawMatColorNameTH" & Microsoft.VisualBasic.Right(info.Column.FieldName.ToString, info.Column.FieldName.ToString.Length - "FTRawMatColorCode".Length)
                        Dim Col6 As String = "FTRawMatColorNameEN" & Microsoft.VisualBasic.Right(info.Column.FieldName.ToString, info.Column.FieldName.ToString.Length - "FTRawMatColorCode".Length)

                        Try
                            text &= vbCrLf & "" & view.GetRowCellValue(info.RowHandle, Col5)
                            text &= vbCrLf & "" & view.GetRowCellValue(info.RowHandle, Col6)
                        Catch ex As Exception
                        End Try

                    End If

                    ttInfo = New DevExpress.Utils.ToolTipControlInfo(cellKey, text)

                End If

                If ttInfo IsNot Nothing Then
                    e.Info = ttInfo
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Enum EditMergeCellData As Integer
        ItemSeq = 0
        ItemCode = 1
        SuplCode = 2
        UNitCode = 3
        ItemDesc = 4
    End Enum

    Private _StateEditMergeCell As EditMergeCellData = EditMergeCellData.ItemSeq
    Property StateEditMergeCell As EditMergeCellData
        Get
            Return _StateEditMergeCell
        End Get
        Set(value As EditMergeCellData)
            _StateEditMergeCell = value
        End Set
    End Property

    Private m_mergedCellEditorSupl As DevExpress.XtraEditors.ButtonEdit
    Private m_mergedCellEditorMainMat As DevExpress.XtraEditors.ButtonEdit
    Private m_mergedCellEditorUnit As DevExpress.XtraEditors.ButtonEdit
    Private m_mergedCellEditorDesc As DevExpress.XtraEditors.MemoEdit
    Private m_mergedCellsEdited As GridCellInfoCollection

    Private Sub CreateMergeEditControl()
        m_mergedCellEditorSupl = New DevExpress.XtraEditors.ButtonEdit
        m_mergedCellEditorMainMat = New DevExpress.XtraEditors.ButtonEdit
        m_mergedCellEditorUnit = New DevExpress.XtraEditors.ButtonEdit
        m_mergedCellEditorDesc = New DevExpress.XtraEditors.MemoEdit

        With m_mergedCellEditorSupl
            .Name = "FNHSysSuplIdTo"
            .Properties.Buttons(0).Tag = 175

            AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicButtone_ButtonClick
            AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly
        End With

        With m_mergedCellEditorMainMat
            .Name = "FNHSysMainMatIdTo"
            .Properties.Buttons(0).Tag = 176

            AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicButtone_ButtonClick
            AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly
        End With

        With m_mergedCellEditorUnit
            .Name = "FNHSysUnitIdTo"
            .Properties.Buttons(0).Tag = 246

            AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicButtone_ButtonClick
            AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly
        End With

        With m_mergedCellEditorDesc
            .Name = "FNHSysMerMatId_None"

        End With

    End Sub

    Private Sub GridView1_MouseDown(sender As Object, e As MouseEventArgs) Handles ogvmat.MouseDown
        Dim tmpview As DevExpress.XtraGrid.Views.Grid.GridView = sender
        Dim hInfo As GridHitInfo = tmpview.CalcHitInfo(e.X, e.Y)

        If (hInfo.InRowCell) Then
            If Not (m_mergedCellsEdited Is Nothing) Then
                Select Case StateEditMergeCell
                    Case EditMergeCellData.SuplCode
                        If (ogcmat.Contains(m_mergedCellEditorSupl)) Then
                            ogcmat.Controls.Remove(m_mergedCellEditorSupl)
                            For Each cellInfo As GridCellInfo In m_mergedCellsEdited
                                With ogvmat
                                    .SetRowCellValue(cellInfo.RowHandle, "FTSuplCode", m_mergedCellEditorSupl.Text)
                                    .SetRowCellValue(cellInfo.RowHandle, "FNHSysSuplId", Integer.Parse(Val(m_mergedCellEditorSupl.Properties.Tag.ToString())))
                                End With
                            Next

                        End If
                    Case EditMergeCellData.ItemCode
                        If (ogcmat.Contains(m_mergedCellEditorMainMat)) Then
                            ogcmat.Controls.Remove(m_mergedCellEditorMainMat)
                            Dim _FNHSysMerMatId_None As String = ""
                            Dim _Qry As String = ""

                            _Qry = "SELECT TOP 1 "

                            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                                _Qry &= vbCrLf & " FTMainMatNameEN AS FTMainMatName "
                            Else
                                _Qry &= vbCrLf & " FTMainMatNameTH AS FTMainMatName "
                            End If

                            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS A WITH(NOLOCK) "
                            _Qry &= vbCrLf & " WHERE FNHSysMainMatId=" & Integer.Parse(Val(m_mergedCellEditorMainMat.Properties.Tag.ToString())) & ""
                            _FNHSysMerMatId_None = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")

                            For Each cellInfo As GridCellInfo In m_mergedCellsEdited
                                With ogvmat
                                    .SetRowCellValue(cellInfo.RowHandle, "FTItemNo", m_mergedCellEditorMainMat.Text)
                                    .SetRowCellValue(cellInfo.RowHandle, "FNHSysMerMatId", Integer.Parse(Val(m_mergedCellEditorMainMat.Properties.Tag.ToString())))
                                    .SetRowCellValue(cellInfo.RowHandle, "FNHSysMerMatId_None", _FNHSysMerMatId_None)
                                End With
                            Next

                        End If
                    Case EditMergeCellData.UNitCode
                        If (ogcmat.Contains(m_mergedCellEditorUnit)) Then
                            ogcmat.Controls.Remove(m_mergedCellEditorUnit)
                            For Each cellInfo As GridCellInfo In m_mergedCellsEdited
                                With ogvmat
                                    .SetRowCellValue(cellInfo.RowHandle, "FTUnitCode", m_mergedCellEditorUnit.Text)
                                    .SetRowCellValue(cellInfo.RowHandle, "FNHSysUnitId", Integer.Parse(Val(m_mergedCellEditorUnit.Properties.Tag.ToString())))
                                End With
                            Next

                        End If
                    Case EditMergeCellData.ItemDesc
                        If (ogcmat.Contains(m_mergedCellEditorDesc)) Then
                            ogcmat.Controls.Remove(m_mergedCellEditorDesc)
                            For Each cellInfo As GridCellInfo In m_mergedCellsEdited
                                With ogvmat
                                    .SetRowCellValue(cellInfo.RowHandle, "FNHSysMerMatId_None", m_mergedCellEditorDesc.Text)
                                End With
                            Next
                        End If


                End Select

                m_mergedCellsEdited = Nothing
            End If

            Dim vInfo As GridViewInfo = tmpview.GetViewInfo()
            Dim cInfo As GridCellInfo = vInfo.GetGridCellInfo(hInfo)
            Select Case cInfo.Column.FieldName.ToString
                Case "FTSuplCode"

                    If (TypeOf (cInfo) Is DevExpress.XtraGrid.Views.Grid.ViewInfo.GridMergedCellInfo) Then
                        If (m_mergedCellsEdited IsNot Nothing) Then
                            ogcmat.Controls.Remove(m_mergedCellEditorSupl)
                        End If

                        ogcmat.Controls.Add(m_mergedCellEditorSupl)
                        m_mergedCellEditorSupl.Bounds = cInfo.Bounds
                        ' ''m_mergedCellEditorSupl.Text = cInfo.CellValue.ToString()
                        m_mergedCellsEdited = (CType(cInfo, GridMergedCellInfo)).MergedCells
                        StateEditMergeCell = EditMergeCellData.SuplCode

                    End If

                Case "FTItemNo"

                    If (TypeOf (cInfo) Is DevExpress.XtraGrid.Views.Grid.ViewInfo.GridMergedCellInfo) Then

                        If (m_mergedCellsEdited IsNot Nothing) Then

                            ogcmat.Controls.Remove(m_mergedCellEditorMainMat)

                        End If

                        ogcmat.Controls.Add(m_mergedCellEditorMainMat)
                        m_mergedCellEditorMainMat.Bounds = cInfo.Bounds
                        m_mergedCellEditorMainMat.Text = cInfo.CellValue.ToString()
                        ''m_mergedCellsEdited = (CType(cInfo, GridMergedCellInfo)).MergedCells
                        m_mergedCellsEdited = (CType(cInfo, GridMergedCellInfo)).MergedCells
                        StateEditMergeCell = EditMergeCellData.ItemCode

                        '' ''End If

                    End If

                Case "FTUnitCode"
                    If (TypeOf (cInfo) Is DevExpress.XtraGrid.Views.Grid.ViewInfo.GridMergedCellInfo) Then
                        If (m_mergedCellsEdited IsNot Nothing) Then
                            ogcmat.Controls.Remove(m_mergedCellEditorUnit)
                        End If

                        ogcmat.Controls.Add(m_mergedCellEditorUnit)
                        m_mergedCellEditorUnit.Bounds = cInfo.Bounds
                        m_mergedCellEditorUnit.Text = cInfo.CellValue.ToString()
                        m_mergedCellsEdited = (CType(cInfo, GridMergedCellInfo)).MergedCells
                        StateEditMergeCell = EditMergeCellData.UNitCode
                    End If

                Case "FNHSysMerMatId_None"
                    If (TypeOf (cInfo) Is DevExpress.XtraGrid.Views.Grid.ViewInfo.GridMergedCellInfo) Then
                        If (m_mergedCellsEdited IsNot Nothing) Then
                        End If
                        ogcmat.Controls.Remove(m_mergedCellEditorDesc)

                        ogcmat.Controls.Add(m_mergedCellEditorDesc)
                        m_mergedCellEditorDesc.Bounds = cInfo.Bounds
                        m_mergedCellEditorDesc.Text = HI.UL.ULF.rpQuoted(cInfo.CellValue.ToString)
                        Try
                            m_mergedCellsEdited = (CType(cInfo, GridMergedCellInfo)).MergedCells
                        Catch ex As Exception
                        End Try
                        StateEditMergeCell = EditMergeCellData.ItemDesc

                    End If

            End Select
        End If
    End Sub

    Private Sub GridView3_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles ogvsize.RowCellStyle
        Try
            With ogvsize
                If Microsoft.VisualBasic.Left(e.Column.FieldName.ToUpper, Len("FTRawMatSizeCode")) = "FTRawMatSizeCode".ToUpper Then

                    If .GetRowCellValue(e.RowHandle, e.Column) = "N/R" Then

                        e.Appearance.BackColor = System.Drawing.Color.LightCyan
                        e.Appearance.ForeColor = System.Drawing.Color.Blue

                    End If

                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTSeason_KeyPress(sender As Object, e As KeyPressEventArgs) Handles FTSeason.KeyPress
        Dim CharInt As Integer = 0
        ' CharInt = Asc(e.KeyChar)
        Select Case Asc(e.KeyChar)
            Case 32, 39, 34, 37
                e.Handled = True
            Case Else
                e.Handled = False
        End Select
    End Sub

    Private Sub FNHSysStyleDevId_KeyPress(sender As Object, e As KeyPressEventArgs) Handles FNHSysStyleDevId.KeyPress
        Dim CharInt As Integer = 0
        ' CharInt = Asc(e.KeyChar)
        Select Case Asc(e.KeyChar)
            Case 32, 39, 34, 37
                e.Handled = True
            Case Else
                e.Handled = False
        End Select
    End Sub

    Private Sub FTSeason_EditValueChanged(sender As Object, e As EventArgs) Handles FTSeason.EditValueChanged

        Try
            If Me.InvokeRequired Then
                Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FNHSysStyleDevId_EditValueChanged), New Object() {sender, e})
            Else

                Dim _Str As String = "SELECT TOP 1 FNHSysStyleDevId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle  WITH(NOLOCK) WHERE FTStyleDevCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleDevId.Text) & "' AND FTSeason='" & HI.UL.ULF.rpQuoted(FTSeason.Text) & "'  AND ISNULL(FNVersion,0)=" & FNVersion.Text & "  AND FNBomDevType =" & FNBomDevType.SelectedIndex & " "
                FNHSysStyleDevId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")
                Call LoadStyleInfo(FNHSysStyleDevId.Properties.Tag.ToString, True)

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ocmposttobomsheet_Click(sender As Object, e As EventArgs) Handles ocmposttobomsheet.Click
        If Me.VerifyData() Then
            If CheckOwner() = False Then
                Exit Sub
            End If
            If ogcmat.DataSource Is Nothing Then
                Exit Sub
            End If

            Call LoadStylePostInfo()
            If CheckPostDataToBomSheet() = False Then Exit Sub
            If VerifyMasterData() = False Then Exit Sub

            Dim _dt As DataTable
            Dim _Qry As String = ""
            Dim _FNHSysStyleDevId As Integer = Integer.Parse(Val(FNHSysStyleDevId.Properties.Tag.ToString))
            Dim _FTStyleDevCode As String = ""
            Dim _FTSeason As String = ""
            Dim _FTStyleCode As String = ""
            Dim _FNHSysStyleId As Integer = 0
            Dim _FNHSysSeasonId As Integer = 0

            _Qry = "SELECT FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysStyleDevId, FTStyleDevCode, FTStyleDevNameTH, FTStyleDevNameEN, FTSeason, FTNote, FNHSysCustId, FTStatePost, FTPostBy, FTPostDate, FTPostTime"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle AS MS WITH(NOLOCK)  "
            _Qry &= vbCrLf & " WHERE  (FNHSysStyleDevId  =" & Val(_FNHSysStyleDevId) & ")"

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            For Each R As DataRow In _dt.Rows

                _FTStyleDevCode = R!FTStyleDevCode.ToString
                _FTSeason = R!FTSeason.ToString
                _FTStyleCode = R!FTStyleDevCode.ToString '& R!FTSeason.ToString

            Next

            If _FTSeason <> "" Then

                _Qry = " SELECT TOP 1 FNHSysSeasonId "
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS MS WITH(NOLOCK)  "
                _Qry &= vbCrLf & " WHERE  (FTSeasonCode  ='" & HI.UL.ULF.rpQuoted(_FTSeason) & "')"

                _FNHSysSeasonId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")))

            End If

            _Qry = " SELECT TOP 1 FNHSysStyleId "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS MS WITH(NOLOCK)  "
            _Qry &= vbCrLf & " WHERE  (FTStyleCode  ='" & HI.UL.ULF.rpQuoted(_FTStyleCode) & "')"

            _FNHSysStyleId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")))

            If _FNHSysStyleId > 0 And _FNHSysSeasonId > 0 Then

                '_Qry = " SELECT TOP 1 FNHSysStyleId "
                '' _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Mat AS A WITH(NOLOCK)"
                '_Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK)"
                '_Qry &= vbCrLf & " WHERE FNHSysStyleId=" & _FNHSysStyleId & " AND FNHSysSeasonId=" & _FNHSysSeasonId & ""

                'If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "") <> "" Then
                '    'HI.MG.ShowMsg.mInfo("พบข้อมูลการสร้าง BOM Sheet ของ Style และ Season นี้แล้ว ไม่สามารถทำการ Post ได้ กรุณาทำการตรวจสอบ !!!", 1506309954, Me.Text, , MessageBoxIcon.Warning)
                '    HI.MG.ShowMsg.mInfo("พบข้อมูลการสร้าง Order Production ของ Style และ Season นี้แล้ว ไม่สามารถทำการ Post ได้ กรุณาทำการตรวจสอบ !!!", 1506389954, Me.Text, , MessageBoxIcon.Warning)
                '    Exit Sub
                'End If

            End If

            If PostDataToNewBomSheet() Then

                _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle"
                _Qry &= vbCrLf & " SET FTStatePost = '1'"
                _Qry &= vbCrLf & ",FTPostBy = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ",FTPostDate = " & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ",FTPostTime = " & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & " WHERE FNHSysStyleDevId=" & Integer.Parse(Val(FNHSysStyleDevId.Properties.Tag.ToString())) & ""

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                Call LoadStylePostInfo()

                HI.MG.ShowMsg.mInfo("Post data Complete !!!", 1506305874, Me.Text, , MessageBoxIcon.Information)

            End If

        End If
    End Sub


    Private Sub ocmbomchangecolorway_Click(sender As Object, e As EventArgs) Handles ocmbomchangecolorway.Click
        Try

            If CheckOwner() = False Then
                Exit Sub
            End If

            If sFNHSysStyleDevId = "" Then Return
            Call LoadStylePostInfo()
            If CheckPostDataToBomSheet() = False Then Exit Sub

            With Me.ogvcolor

                If .FocusedRowHandle < 0 Then Exit Sub

                If Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = "FTRawMatColorCode".ToUpper Then

                    CType(ogccolor.DataSource, DataTable).AcceptChanges()
                    Dim dt2 As DataTable = CType(ogccolor.DataSource, DataTable)

                    Dim _FTColorway As String = .FocusedColumn.Caption

                    Dim Col1 As String = .FocusedColumn.FieldName.ToString
                    Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)
                    Dim Col3 As String = "FTRawMatColorNameTH" & .FocusedColumn.FieldName.ToString.Replace("FTRawMatColorCode", "")
                    Dim Col4 As String = "FTRawMatColorNameEN" & .FocusedColumn.FieldName.ToString.Replace("FTRawMatColorCode", "")

                    With _wChangeColorway
                        .FTFromColorway.Text = _FTColorway
                        .FTColorway.Text = ""
                        .ProcNew = False
                        .ShowDialog()

                        If .ProcNew Then

                            Dim _NewColorWay As String = .FTColorway.Text
                            Dim _Qry As String = ""

                            _Qry = "   SELECT  FTColorWay "
                            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_ColorWay AS A WITH(NOLOCK)"
                            _Qry &= vbCrLf & "  WHERE FNHSysStyleDevId=" & Integer.Parse(Val(FNHSysStyleDevId.Properties.Tag.ToString)) & ""
                            _Qry &= vbCrLf & "        AND  (FTColorWay = N'" & HI.UL.ULF.rpQuoted(_NewColorWay) & "')"

                            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "") <> "" Then
                                Exit Sub
                            End If

                            Dim dc As String = ""
                            Dim dc1 As String = ""
                            Dim dc3 As String = ""
                            Dim dc4 As String = ""

                            dc = "FTRawMatColorCode" & _NewColorWay
                            dc1 = "FNHSysRawMatColorId" & "FTRawMatColorCode" & _NewColorWay
                            dc3 = "FTRawMatColorNameTH" & _NewColorWay
                            dc4 = "FTRawMatColorNameEN" & _NewColorWay

                            ogvcolor.FocusedColumn.Caption = _NewColorWay
                            ogvcolor.FocusedColumn.FieldName = dc

                            dt2.BeginInit()
                            dt2.Columns(Col1).ColumnName = dc
                            dt2.Columns(Col2).ColumnName = dc1
                            dt2.Columns(Col3).ColumnName = dc3
                            dt2.Columns(Col4).ColumnName = dc4
                            dt2.EndInit()
                            dt2.AcceptChanges()

                            Me.ogccolor.DataSource = dt2.Copy
                            Me.ogccolor.Refresh()

                            CType(Me.ogccolor.DataSource, DataTable).AcceptChanges()

                        End If

                    End With

                End If

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepFTPositionPartName_QueryCloseUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RepFTPositionPartName.QueryCloseUp
        Try
            With Me.ogvmat
                If .FocusedRowHandle < 0 Then
                    Exit Sub
                End If

                Me.TextEdit1.Focus()
                Me.TextEdit1.SelectAll()

                .SetFocusedRowCellValue("FTPart", Me.TextEdit1.Text)

                Dim _PartName As String = ""
                Dim _PartIDKey As String = ""

                With CType(Me.ogcpart.DataSource, DataTable)
                    .AcceptChanges()
                    For Each R As DataRow In .Select("FTSelect='1'")

                        If _PartName = "" Then
                            _PartName = R!FTPartName.ToString
                            _PartIDKey = R!FNHSysPartId.ToString
                        Else
                            _PartName = _PartName & "," & R!FTPartName.ToString
                            _PartIDKey = _PartIDKey & "|" & R!FNHSysPartId.ToString
                        End If

                    Next

                End With

                If Me.TextEdit1.Text <> "" Then
                    _PartName = Me.TextEdit1.Text & ":" & _PartName
                End If

                .SetFocusedRowCellValue("FTPartNameEN", _PartName)
                .SetFocusedRowCellValue("FTPositionPartId", _PartIDKey)

                Try
                    ' If "" & .GetFocusedRowCellValue("FTComponent").ToString = "" Or _PartName <> "" Then
                    If "" & .GetFocusedRowCellValue("FTComponent").ToString.Trim() = "" Then
                        .SetFocusedRowCellValue("FTComponent", _PartName)
                    End If
                Catch ex As Exception
                End Try

                'Try
                '    ' If "" & .GetFocusedRowCellValue("FTComponent").ToString = "" Or _PartName <> "" Then
                '    If "" & .GetFocusedRowCellValue("FTComponent").ToString.Trim() = "" Then
                '        .SetFocusedRowCellValue("FTComponent", _PartName)
                '    End If
                'Catch ex As Exception
                'End Try

                'CType(Me.ogcpart.DataSource, DataTable).AcceptChanges()

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepFTPositionPartName_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RepFTPositionPartName.QueryPopUp
        Try

            Call LoadPartMaster()

            Dim _PartKey As String = ""
            Dim _PartIDKey As String = ""
            With Me.ogvmat
                If .FocusedRowHandle < 0 Then
                    Exit Sub
                End If
                _PartKey = "" & .GetFocusedRowCellValue("FTPart").ToString
                _PartIDKey = "" & .GetFocusedRowCellValue("FTPositionPartId").ToString
            End With

            ogvpart.ClearColumnsFilter()
            ogvpart.ActiveFilter.Clear()

            Me.ogcpart.DataSource = _dtpart(0).Copy
            ogvpart.Columns.ColumnByFieldName("FTSelect").Width = 40
            ogvpart.Columns.ColumnByFieldName("FTPartName").Width = 150

            Me.TextEdit1.Text = _PartKey

            With CType(Me.ogcpart.DataSource, DataTable)

                For Each Str As String In _PartIDKey.Split("|")
                    For Each R As DataRow In .Select("FNHSysPartId=" & Integer.Parse(Val(Str)) & "")
                        R!FTSelect = "1"
                        Exit For
                    Next
                Next

                .AcceptChanges()

            End With

        Catch ex As Exception
        End Try

    End Sub

    Private Sub LoadPartMaster()
        _dtpart.Clear()

        Dim dt As DataTable
        Dim _Qry As String = ""
        Dim _RowIndex As Integer = 0
        _Qry = "Select '0' AS FTSelect ,FNHSysPartId,FTPartCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ",FTPartNameTH AS FTPartName"
        Else
            _Qry &= vbCrLf & ",FTPartNameEN AS FTPartName"
        End If

        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS A With(NOLOCK) "
        _Qry &= vbCrLf & " WHERE ISNULL(FTStateActive,'')='1'  "
        _Qry &= vbCrLf & " ORDER BY FTPartCode "

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

        _dtpart.Add(dt.Copy)

        dt.Dispose()
    End Sub


    Private Sub LoadSetPart()
        Dim _Qry As String
        _Qry = "SELECT        FNListIndex AS FNIndex"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " ,FTNameTH  AS FTName "
        Else
            _Qry &= vbCrLf & " , FTNameEN AS FTName "
        End If

        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE  (FTListName = N'FNOrderSetTypeBOM')"
        _Qry &= vbCrLf & " ORDER BY FNListIndex  "

        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)
        Me.RepositoryItemFNOrderSetType.DataSource = dt.Copy
        Me.RepositoryItemFNOrderSetType4.DataSource = dt.Copy
        Me.RepositoryFNOrderSetType3.DataSource = dt.Copy

        dt.Dispose()

    End Sub

    Private Sub LoadItemMaster()
        Dim _Qry As String
        _Qry = "SELECT X.FTMainMatCode AS FTItemREfNo,Sup.FTSuplCode ,Ux.FTUnitCode ,X.FNHSysCurId,Cur.FTCurCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ", X.FTMainMatNameTH  AS FTItemREfNoName "
        Else
            _Qry &= vbCrLf & ", X.FTMainMatNameEN AS FTItemREfNoName "
        End If

        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS X WITH(NOLOCK)"
        _Qry &= vbCrLf
        _Qry &= vbCrLf & "Left OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS Sup with(nolock) on X.FNHSysSuplId = Sup.FNHSysSuplId "
        _Qry &= vbCrLf
        _Qry &= vbCrLf & "Left OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit  As Ux With(nolock) On X.FNHSysUnitId  = Ux.FNHSysUnitId "
        _Qry &= vbCrLf
        _Qry &= vbCrLf & "Left OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency  As Cur With(nolock) On X.FNHSysCurId  = Cur.FNHSysCurId "
        _Qry &= vbCrLf
        '  _Qry &= vbCrLf & " WHERE  (FTStateActive ='1')"
        _Qry &= vbCrLf & " ORDER BY X.FTMainMatCode  "

        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

        RepositoryIteFTItemREfNo.DataSource = dt
    End Sub

    Private Sub RepositoryIteFTItemREfNo_KeyDown(sender As Object, e As KeyEventArgs) Handles RepositoryIteFTItemREfNo.KeyDown

        Select Case e.KeyCode
            Case Keys.Delete

                If ogvmat.FocusedColumn.FieldName.ToString.ToLower = "FTItemREfNo".ToLower Then

                    ogvmat.SetFocusedRowCellValue("FTItemREfNo", "")
                    CType(ogcmat.DataSource, DataTable).AcceptChanges()
                End If

        End Select

    End Sub

    Private Sub RepositoryIteFTItemREfNo_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryIteFTItemREfNo.EditValueChanged
        Try

            With Me.ogvmat
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)

                .SetFocusedRowCellValue("FTSuplCode", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTSuplCode").ToString)
                .SetFocusedRowCellValue("FTUnitCode", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTUnitCode").ToString)
                .SetFocusedRowCellValue("FTCurCode", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTCurCode").ToString)

            End With

            CType(Me.ogcmat.DataSource, DataTable).AcceptChanges()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNVersion_EditValueChanged(sender As Object, e As EventArgs)
        FNVersion.Visible = (FNVersion.Text > 0)
    End Sub

    Private Sub ocmcopy_Click(sender As Object, e As EventArgs) Handles ocmcopy.Click

        If Me.FNHSysStyleDevId.Text <> "" Then
            If "" & Me.FNHSysStyleDevId.Properties.Tag.ToString <> "" Then
                If FTSeason.Text <> "" Then

                    Call HI.ST.Lang.SP_SETxLanguage(_CopyStyle)

                    With _CopyStyle
                        .FNHSysStyleIdF.Text = Me.FNHSysStyleDevId.Text
                        .FNHSysSeasonIdF.Text = FTSeason.Text.Trim
                        .FNHSysStyleIdF_None.Text = FTStyleDevNameEN.Text
                        .FNHSysStyleIdF.Properties.Tag = FNHSysStyleDevId.Properties.Tag.ToString
                        .FNHSysStyleDevId.Text = ""
                        .FTSeason.Text = ""
                        .FNVersion.Value = 0
                        .ProcComplete = False
                        .ShowDialog()

                        If (.ProcComplete) Then
                            Me.otb.SelectedTabPage = otpmatcode

                        End If

                    End With

                End If

            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysStyleDevId_lbl.Text)
                FNHSysStyleDevId.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysStyleDevId_lbl.Text)
            FNHSysStyleDevId.Focus()
        End If

    End Sub


    Private Sub FNBomDevType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNBomDevType.SelectedIndexChanged
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New HI.Delegate.Dele.ComboBoxEdit_SelectedIndexChange(AddressOf FNBomDevType_SelectedIndexChanged), New Object() {sender, e})
            Else

                Dim _Str As String = "SELECT TOP 1 FNHSysStyleDevId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle  WITH(NOLOCK) WHERE FTStyleDevCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleDevId.Text) & "' AND FTSeason='" & HI.UL.ULF.rpQuoted(FTSeason.Text) & "'  AND ISNULL(FNVersion,0)=" & FNVersion.Text & " AND FNBomDevType =" & FNBomDevType.SelectedIndex & " "
                FNHSysStyleDevId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")
                Call LoadStyleInfo(FNHSysStyleDevId.Properties.Tag.ToString, True)

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ogvmat_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs) Handles ogvmat.CustomColumnDisplayText
        Try

            Select Case e.Column.FieldName
                Case "FNRepeatLengthCM", "FNPackPerCarton"
                    If Val(e.Value) = 0 Then
                        e.DisplayText = ""
                    End If


            End Select

        Catch ex As Exception

        End Try
    End Sub


    Private Sub RepositoryItemCalFNPackPerCarton_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCalFNPackPerCarton.EditValueChanging
        Try
            If Val(e.NewValue.ToString) < 0 Then
                e.Cancel = True
            Else
                e.Cancel = False
                Dim NewValue As Decimal = Val(e.NewValue.ToString)
                Dim ConSmp As Decimal = 0
                NewValue = CDbl(Format((1.0 / NewValue), "0.00000"))
                Me.GridView1.SetFocusedRowCellValue("FNConSmp", NewValue)

            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryConsPlusCalcEdit_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryConsPlusCalcEdit.EditValueChanging
        Try
            If Val(e.NewValue.ToString) < 0 Then
                e.Cancel = True
            Else
                e.Cancel = False
                Dim NewValue As Decimal = Val(e.NewValue.ToString)
                If NewValue > 0 Then
                    Me.GridView1.SetFocusedRowCellValue("FTStateHemNotOptiplan", "1")
                Else
                    Me.GridView1.SetFocusedRowCellValue("FTStateHemNotOptiplan", "0")
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class