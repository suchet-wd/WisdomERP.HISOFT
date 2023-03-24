Imports DevExpress.Data
Imports DevExpress.Utils
Imports System.ComponentModel
Imports System.IO
Imports System.Windows.Forms

Public Class wBOMListing

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean

    Private _wAddBOMNew As wBOMListingAddNew
    Private _wAddBOM As wBOMListingAdd

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _wAddBOMNew = New wBOMListingAddNew
        HI.TL.HandlerControl.AddHandlerObj(_wAddBOMNew)
        Dim oSysLang As New HI.ST.SysLanguage
        'Call HI.ST.Lang.InsertLanguage(_CopyStyle)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _wAddBOMNew.Name.ToString.Trim, _wAddBOMNew)
        Catch ex As Exception
        Finally
        End Try

        Call HI.ST.Lang.SP_SETxLanguage(_wAddBOMNew)



    End Sub



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

    Private Sub LoadData(Optional BomId As Integer = 0)
        Dim _Qry As String = ""
        Dim _dt As DataTable


        StateCal = False

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

        _Qry = "SELECT           A.FTInsUser, A.FDInsDate, A.FTInsTime "
        _Qry &= vbCrLf & "    , A.FTUpdUser, A.FDUpdDate, A.FTUpdTime "
        _Qry &= vbCrLf & "   , A.FNHSysBomId "
        _Qry &= vbCrLf & "	, A.FNHSysStyleId "
        _Qry &= vbCrLf & "  , A.FNHSysSeasonId "
        _Qry &= vbCrLf & ", A.FNHSysCustId "
        _Qry &= vbCrLf & "  , A.FNBomType "
        _Qry &= vbCrLf & "	, BT.FNBomTypeName "
        _Qry &= vbCrLf & "  , A.FNBomVersion "
        _Qry &= vbCrLf & "	, A.FTDevelopName, A.FTNote, A.FTStateActive, A.FTSatetConfirm "
        _Qry &= vbCrLf & "  , A.FTSatetConfirmBy, A.FDSatetConfirmDate, A.FTSatetConfirmTime "
        _Qry &= vbCrLf & " , A.FTSatetApprove, A.FTSatetApproveBy, A.FDSatetApproveDate "
        _Qry &= vbCrLf & "  , A.FTSatetApproveTime, ST.FTStyleCode, SS.FTSeasonCode "
        _Qry &= vbCrLf & "	, ST.FTStyleNameEN AS FTStyleName, ST.FTStateGameDays, ST.FTStateStyleSet "
        _Qry &= vbCrLf & "  ,BM1.FTBomMatUpdUser  "
        _Qry &= vbCrLf & "	,BM1.FTBomMatUpdTime "
        _Qry &= vbCrLf & "	,BM2.FTBomMatColorUpdUser "
        _Qry &= vbCrLf & "	,BM2.FTBomMatColorUpdTime "
        _Qry &= vbCrLf & "	,BM3.FTBomMatSizeUpdUser "
        _Qry &= vbCrLf & "	,BM3.FTBomMatSizeUpdTime "
        _Qry &= vbCrLf & "	,BMEDIT.FTBomEditUser "
        _Qry &= vbCrLf & "	,BMEDIT.FTBomEditTime "


        _Qry &= vbCrLf & " From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM AS A WITH(NOLOCK) INNER Join "
        _Qry &= vbCrLf & " " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle AS ST WITH(NOLOCK) ON A.FNHSysStyleId = ST.FNHSysStyleId INNER JOIN "
        _Qry &= vbCrLf & "  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMSeason As SS With(NOLOCK) On A.FNHSysSeasonId = SS.FNHSysSeasonId "


        If FNHSysBuyId.Text.Trim <> "" Then

            _Qry &= vbCrLf & "  INNER JOIN ( "
            _Qry &= vbCrLf & " SELECT DISTINCT ORDBUY.FNHSysStyleId,ORDBUY.FNHSysSeasonId "
            _Qry &= vbCrLf & " FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrder AS ORDBUY WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE ORDBUY.FNHSysBuyId=" & Val(FNHSysBuyId.Properties.Tag.ToString) & ""

            _Qry &= vbCrLf & "  )  ORDBUY ON A.FNHSysStyleId = ORDBUY.FNHSysStyleId AND A.FNHSysSeasonId = ORDBUY.FNHSysSeasonId"

        End If

        _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 L.FTNameTH AS  FNBomTypeName FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & ".dbo.HSysListData AS L WITH(NOLOCK) WHERE L.FTListName ='FNBomDevType' AND L.FNListIndex = A.FNBomType )   AS BT    "

        _Qry &= vbCrLf & "  OUTER APPLY(SELECT TOP 1 CASE WHEN ISNULL(BM1.FDUpdDate,'') ='' THEN BM1.FTInsUser ELSE BM1.FTUpdUser END AS  FTBomMatUpdUser "
        _Qry &= vbCrLf & "           ,CASE WHEN ISNULL(BM1.FDUpdDate,'') ='' THEN BM1.FDInsDate  + ' ' + BM1.FTInsTime ELSE ISNULL(BM1.FDUpdDate,'') + ' ' + BM1.FTUpdTime END AS FTBomMatUpdTime "
        _Qry &= vbCrLf & "     From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Mat AS BM1 WITH(NOLOCK) "
        _Qry &= vbCrLf & "  	Where BM1.FNHSysBomId = A.FNHSysBomId "
        _Qry &= vbCrLf & "     Order By CASE WHEN ISNULL(BM1.FDUpdDate,'') ='' THEN BM1.FDInsDate  + ' ' + BM1.FTInsTime ELSE ISNULL(BM1.FDUpdDate,'') + ' ' + BM1.FTUpdTime END DESC "

        _Qry &= vbCrLf & "   )   AS BM1       "
        _Qry &= vbCrLf & "      OUTER APPLY(SELECT TOP 1 CASE WHEN ISNULL(BM2.FDUpdDate,'') ='' THEN BM2.FTInsUser ELSE BM2.FTUpdUser END AS  FTBomMatColorUpdUser "
        _Qry &= vbCrLf & "     ,CASE WHEN ISNULL(BM2.FDUpdDate,'') ='' THEN BM2.FDInsDate  + ' ' + BM2.FTInsTime ELSE ISNULL(BM2.FDUpdDate,'') + ' ' + BM2.FTUpdTime END AS FTBomMatColorUpdTime "
        _Qry &= vbCrLf & "     From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Colorway AS BM2 WITH(NOLOCK) "
        _Qry &= vbCrLf & "   Where BM2.FNHSysBomId = A.FNHSysBomId "
        _Qry &= vbCrLf & "    Order By CASE WHEN ISNULL(BM2.FDUpdDate,'') ='' THEN BM2.FDInsDate  + ' ' + BM2.FTInsTime ELSE ISNULL(BM2.FDUpdDate,'') + ' ' + BM2.FTUpdTime END DESC "
        _Qry &= vbCrLf & "     )   AS BM2       "
        _Qry &= vbCrLf & "     OUTER APPLY(SELECT TOP 1 CASE WHEN ISNULL(BM3.FDUpdDate,'') ='' THEN BM3.FTInsUser ELSE BM3.FTUpdUser END AS  FTBomMatSizeUpdUser  "
        _Qry &= vbCrLf & "       ,CASE WHEN ISNULL(BM3.FDUpdDate,'') ='' THEN BM3.FDInsDate  + ' ' + BM3.FTInsTime ELSE ISNULL(BM3.FDUpdDate,'') + ' ' + BM3.FTUpdTime END As FTBomMatSizeUpdTime "
        _Qry &= vbCrLf & "     From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Sizebreakdown AS BM3 WITH(NOLOCK)  "
        _Qry &= vbCrLf & "  	Where BM3.FNHSysBomId = A.FNHSysBomId "
        _Qry &= vbCrLf & "     Order By CASE WHEN ISNULL(BM3.FDUpdDate,'') ='' THEN BM3.FDInsDate  + ' ' + BM3.FTInsTime ELSE ISNULL(BM3.FDUpdDate,'') + ' ' + BM3.FTUpdTime END DESC "
        _Qry &= vbCrLf & "     )   AS BM3       "

        _Qry &= vbCrLf & "  OUTER APPLY(SELECT TOP 1 BMEDIT.FTUser  AS  FTBomEditUser "
        _Qry &= vbCrLf & "           ,BMEDIT.FDEditDate  + ' ' + BMEDIT.FTEditTime  AS FTBomEditTime "
        _Qry &= vbCrLf & "     From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_UserEidt AS BMEDIT WITH(NOLOCK) "
        _Qry &= vbCrLf & "  	Where BMEDIT.FNHSysBomId = A.FNHSysBomId "
        _Qry &= vbCrLf & "    "

        _Qry &= vbCrLf & "   )   AS BMEDIT       "

        _Qry &= vbCrLf & "   WHERE A.FNHSysBomId > 0 "

        If BomId > 0 Then
            _Qry &= vbCrLf & "   AND A.FNHSysBomId = " & BomId & " "
        End If


        If FNHSysSeasonId.Text.Trim <> "" Then

            _Qry &= vbCrLf & "   AND A.FNHSysSeasonId = " & Val(FNHSysSeasonId.Properties.Tag.ToString) & " "

        End If


        _Qry &= vbCrLf & " ORDER BY ST.FTStyleCode, SS.FTSeasonCode,BT.FNBomTypeName,A.FNBomVersion "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)


        If BomId > 0 Then

            Try

                If Me.ogclisting.DataSource Is Nothing Then
                    Me.ogclisting.DataSource = _dt.Copy
                Else
                    With CType(Me.ogclisting.DataSource, DataTable)

                        .Merge(_dt.Copy)

                        .AcceptChanges()
                    End With
                End If

            Catch ex As Exception

            End Try

        Else
            Me.ogclisting.DataSource = _dt.Copy
        End If

        _dt.Dispose()
        _Spls.Close()

        _RowDataChange = False

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = True

        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function
#End Region

#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvlisting)


            Dim strBuy As String = ""

            Try

                strBuy = (HI.UL.AppRegistry.ReadRegistry("BOMLoadDataBuy" & Me.Name))


            Catch ex As Exception

            End Try


            Try
                If strBuy.Split("|").Length = 2 Then
                    FNHSysBuyId.Text = strBuy.Split("|")(0)
                    FNHSysSeasonId.Text = strBuy.Split("|")(1)
                Else
                    FNHSysBuyId.Text = strBuy
                    FNHSysSeasonId.Text = ""
                End If
            Catch ex As Exception

            End Try

            Call LoadData()
            StateCal = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If VerifyData() Then


            Call LoadData()
            Call HI.UL.AppRegistry.WriteRegistry("BOMLoadDataBuy" & Me.Name, FNHSysBuyId.Text.Trim() & "|" & FNHSysSeasonId.Text.Trim())
        End If
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvlisting)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub


    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


    Private Sub ocmaddnew_Click(sender As Object, e As EventArgs) Handles ocmaddnew.Click
        Dim BomID As Integer = 0

        With _wAddBOMNew
            .FNBomDevType.SelectedIndex = 0
            .FNHSysStyleId2.Text = ""
            .FNHSysSeasonId.Text = ""
            .ProcComplete = False
            .ShowDialog()

            If .ProcComplete Then

                Dim StyleId As Integer = Val(.FNHSysStyleId2.Properties.Tag.ToString)
                Dim SeasonId As Integer = Val(.FNHSysSeasonId.Properties.Tag.ToString)
                Dim BonType As Integer = .FNBomDevType.SelectedIndex


                If StyleId > 0 And SeasonId > 0 Then

                    Dim cmdstring As String = ""

                    BomID = HI.SE.RunID.GetRunNoID("TMERTBOM", "FNHSysBomId", Conn.DB.DataBaseName.DB_MERCHAN)

                    cmdstring = "insert into " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM (FTInsUser, FDInsDate, FTInsTime, FNHSysBomId, FNHSysStyleId, FNHSysSeasonId, FNBomType, FNBomVersion, FTStateActive)"
                    cmdstring &= vbCrLf & " Select  N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & ", " & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & ", " & HI.UL.ULDate.FormatTimeDB & ""
                    cmdstring &= vbCrLf & ", " & BomID & ""
                    cmdstring &= vbCrLf & ", " & StyleId & ""
                    cmdstring &= vbCrLf & ", " & SeasonId & ""
                    cmdstring &= vbCrLf & ", " & BonType & ""
                    cmdstring &= vbCrLf & ",ISNULL((select TOP 1 FNBomVersion FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM  WHERE FNHSysStyleId =" & StyleId & " AND  FNHSysSeasonId=" & SeasonId & " AND FNBomType =" & BonType & " ),0) + 1 "
                    cmdstring &= vbCrLf & ",'1' "

                    cmdstring &= vbCrLf & " insert into " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTStyle (FNHSysStyleId, FTStyleCode, FTStyleNameTH, FTStyleNameEN, FNHSysCustId, FNHSysSeasonId, FTStateActive, FTUserDevelop) "
                    cmdstring &= vbCrLf & " Select  A.FNHSysStyleId,ST.FTStyleCode, ST.FTStyleNameTH, ST.FTStyleNameEN,0 FNHSysCustId,0 FNHSysSeasonId,'1' FTStateActive,'' FTUserDevelop"
                    cmdstring &= vbCrLf & " FROM( SELECT   " & StyleId & " AS   FNHSysStyleId"
                    cmdstring &= vbCrLf & "  ) AS A INNER Join " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle AS ST WITH(NOLOCK) ON A.FNHSysStyleId = ST.FNHSysStyleId"
                    cmdstring &= vbCrLf & " Left OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTStyle AS C ON A.FNHSysStyleId =C.FNHSysStyleId"
                    cmdstring &= vbCrLf & " WHERE C.FNHSysStyleId Is NULL "

                    If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN) = True Then

                    Else
                        BomID = 0
                    End If


                End If

            End If

        End With

        If BomID > 0 Then
            Me.LoadData(BomID)
            ShowBOMGarment(BomID)
        End If
    End Sub

    Private Sub ogvlisting_ColumnPositionChanged(sender As Object, e As EventArgs) Handles ogvlisting.ColumnPositionChanged

    End Sub


    Private Sub ShowBOMGarment(BomID As Integer, Optional StateView As Boolean = False)


        If StateView = False Then

            Dim EditUser As String = ""
            Dim cmdstring As String = "select top 1 FTUser from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTBOM_UserEidt AS X WITH(NOLOCK) WHERE FNHSysBomId=" & BomID & " AND FTUser<>'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

            EditUser = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN, "")

            If EditUser <> "" Then

                HI.MG.ShowMsg.mInfo("This Bom was editing by another user. you can view only. !!!", 789456, Me.Text, " User : " & EditUser, MessageBoxIcon.Warning)

                StateView = True

            Else

                cmdstring = " Declare @Rec int = 0 "
                cmdstring &= vbCrLf & " UPDATE  BOM   SET  "
                cmdstring &= vbCrLf & "    FNHSysBomId =" & BomID & " "
                cmdstring &= vbCrLf & " , FDEditDate=" & HI.UL.ULDate.FormatDateDB & ""
                cmdstring &= vbCrLf & ",   FTEditTime=" & HI.UL.ULDate.FormatTimeDB & ""
                cmdstring &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_UserEidt  AS BOM  "
                cmdstring &= vbCrLf & " WHERE BOM.FTUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "

                cmdstring &= vbCrLf & " SET @Rec = @@ROWCOUNT  "
                cmdstring &= vbCrLf & "    IF @Rec = 0  "
                cmdstring &= vbCrLf & "        BEGIN  "
                cmdstring &= vbCrLf & "             INSERT INTO  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_UserEidt (FTUser,FNHSysBomId,FDEditDate,FTEditTime) "
                cmdstring &= vbCrLf & "             select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & BomID & " ," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & " "
                cmdstring &= vbCrLf & "             SET @Rec = @@ROWCOUNT  "
                cmdstring &= vbCrLf & "        END  "
                cmdstring &= vbCrLf & "  SELECT  @Rec As FNState "

                Dim mdt As DataTable
                mdt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

            End If

        End If


        Dim _Spls As New HI.TL.SplashScreen("Loading BOM...   Please Wait   ")
        Try

            If _wAddBOM Is Nothing Then
                _wAddBOM = New wBOMListingAdd
                HI.TL.HandlerControl.AddHandlerObj(_wAddBOM)
                Dim oSysLang As New HI.ST.SysLanguage

                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _wAddBOM.Name.ToString.Trim, _wAddBOM)
                Catch ex As Exception
                Finally
                End Try

                Call HI.ST.Lang.SP_SETxLanguage(_wAddBOM)


            End If

            Try
                With _wAddBOM

                    .UIButtonPanel.Buttons(0).Properties.Enabled = Not (StateView)
                    .UIButtonPanel.Buttons(0).Properties.Visible = Not (StateView)
                    .UIButtonPanel.Buttons(1).Properties.Visible = Not (StateView)
                    .FNHSysCustId.ReadOnly = StateView
                    .FTDevelopName.ReadOnly = StateView
                    .FTRemark.Properties.ReadOnly = StateView
                    .FNStateBomOrder.Properties.ReadOnly = StateView
                    .StateEdit = Not (StateView)
                    .LoadBom(BomID)
                    .otb.SelectedTabPageIndex = 2
                    .otb.SelectedTabPageIndex = 0
                    .Show()

                    .ogvmat.ClearColumnsFilter()
                    .ogvmatcolor.ClearColumnsFilter()
                    .ogvmatcolornote.ClearColumnsFilter()
                    .ogvmatsize.ClearColumnsFilter()
                    .ogvmatsilkcolor.ClearColumnsFilter()
                    .ogvorder.ClearColumnsFilter()

                    .StateClose = False
                    .WindowState = FormWindowState.Maximized
                    .BringToFront()
                End With
            Catch ex As Exception


                _wAddBOM = New wBOMListingAdd
                HI.TL.HandlerControl.AddHandlerObj(_wAddBOM)
                Dim oSysLang As New HI.ST.SysLanguage

                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _wAddBOM.Name.ToString.Trim, _wAddBOM)
                Catch ex2 As Exception
                Finally
                End Try

                Call HI.ST.Lang.SP_SETxLanguage(_wAddBOM)


                With _wAddBOM

                    .UIButtonPanel.Buttons(0).Properties.Enabled = Not (StateView)
                    .UIButtonPanel.Buttons(0).Properties.Visible = Not (StateView)
                    .UIButtonPanel.Buttons(1).Properties.Visible = Not (StateView)
                    .FNHSysCustId.ReadOnly = StateView
                    .FTDevelopName.ReadOnly = StateView
                    .FTRemark.Properties.ReadOnly = StateView
                    .FNStateBomOrder.Properties.ReadOnly = StateView
                    .StateEdit = Not (StateView)
                    .LoadBom(BomID)
                    .otb.SelectedTabPageIndex = 2
                    .otb.SelectedTabPageIndex = 0
                    .Show()
                    .ogvmat.ClearColumnsFilter()
                    .ogvmatcolor.ClearColumnsFilter()
                    .ogvmatcolornote.ClearColumnsFilter()
                    .ogvmatsize.ClearColumnsFilter()
                    .ogvmatsilkcolor.ClearColumnsFilter()
                    .ogvorder.ClearColumnsFilter()
                    .StateClose = False
                    .WindowState = FormWindowState.Maximized
                    .BringToFront()
                End With

            End Try


        Catch ex As Exception

        End Try


        _Spls.Close()

    End Sub

    Private Sub ocmedit_Click(sender As Object, e As EventArgs) Handles ocmedit.Click
        Try
            With Me.ogvlisting
                If .FocusedRowHandle >= 0 Then
                    Dim BomID As Integer = Val(.GetFocusedRowCellValue("FNHSysBomId").ToString)


                    If BomID > 0 Then
                        ShowBOMGarment(BomID)
                    End If

                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmview_Click(sender As Object, e As EventArgs) Handles ocmview.Click
        Try
            With Me.ogvlisting
                If .FocusedRowHandle >= 0 Then
                    Dim BomID As Integer = Val(.GetFocusedRowCellValue("FNHSysBomId").ToString)


                    If BomID > 0 Then
                        ShowBOMGarment(BomID, True)
                    End If

                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmcopytonewversion_Click(sender As Object, e As EventArgs) Handles ocmcopytonewversion.Click
        Try
            With Me.ogvlisting
                If .FocusedRowHandle >= 0 Then
                    Dim OrgBomID As Integer = Val(.GetFocusedRowCellValue("FNHSysBomId").ToString)
                    Dim Style As String = .GetFocusedRowCellValue("FTStyleCode").ToString
                    Dim Season As String = .GetFocusedRowCellValue("FTSeasonCode").ToString
                    Dim BomDevType As Integer = Val(.GetFocusedRowCellValue("FNBomType").ToString)

                    Dim BomID As Integer = 0

                    With _wAddBOMNew
                        .FNBomDevType.SelectedIndex = BomDevType
                        .FNHSysStyleId2.Text = Style
                        .FNHSysSeasonId.Text = Season
                        .ProcComplete = False
                        .ShowDialog()

                        If .ProcComplete Then

                            Dim StyleId As Integer = Val(.FNHSysStyleId2.Properties.Tag.ToString)
                            Dim SeasonId As Integer = Val(.FNHSysSeasonId.Properties.Tag.ToString)
                            Dim BonType As Integer = .FNBomDevType.SelectedIndex


                            If StyleId > 0 And SeasonId > 0 Then

                                Dim cmdstring As String = ""

                                BomID = HI.SE.RunID.GetRunNoID("TMERTBOM", "FNHSysBomId", Conn.DB.DataBaseName.DB_MERCHAN)

                                cmdstring = "insert into " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM (FTInsUser, FDInsDate, FTInsTime, FNHSysBomId, FNHSysStyleId, FNHSysSeasonId, FNBomType, FNBomVersion, FTStateActive,FTDevelopName, FTNote,FNHSysCustId,FNStateBomOrder)"
                                cmdstring &= vbCrLf & " Select  N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                cmdstring &= vbCrLf & ", " & HI.UL.ULDate.FormatDateDB & ""
                                cmdstring &= vbCrLf & ", " & HI.UL.ULDate.FormatTimeDB & ""
                                cmdstring &= vbCrLf & ", " & BomID & ""
                                cmdstring &= vbCrLf & ", " & StyleId & ""
                                cmdstring &= vbCrLf & ", " & SeasonId & ""
                                cmdstring &= vbCrLf & ", " & BonType & ""
                                cmdstring &= vbCrLf & ",ISNULL((select TOP 1 MAX(B1.FNBomVersion) AS FNBomVersion FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM AS B1  WHERE B1.FNHSysStyleId =" & StyleId & " AND  B1.FNHSysSeasonId=" & SeasonId & " AND B1.FNBomType =" & BonType & " ),0) + 1 "
                                cmdstring &= vbCrLf & ",'1',X.FTDevelopName, X.FTNote,X.FNHSysCustId,X.FNStateBomOrder "
                                cmdstring &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM AS X WITH(NOLOCK) "
                                cmdstring &= vbCrLf & " WHERE X.FNHSysBomId =" & OrgBomID & ""

                                cmdstring &= vbCrLf & " insert into " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTStyle (FNHSysStyleId, FTStyleCode, FTStyleNameTH, FTStyleNameEN, FNHSysCustId, FNHSysSeasonId, FTStateActive, FTUserDevelop) "
                                cmdstring &= vbCrLf & " Select  A.FNHSysStyleId,ST.FTStyleCode, ST.FTStyleNameTH, ST.FTStyleNameEN,0 FNHSysCustId,0 FNHSysSeasonId,'1' FTStateActive,'' FTUserDevelop"
                                cmdstring &= vbCrLf & " FROM( SELECT   " & StyleId & " AS   FNHSysStyleId"
                                cmdstring &= vbCrLf & "  ) AS A INNER Join " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle AS ST WITH(NOLOCK) ON A.FNHSysStyleId = ST.FNHSysStyleId"
                                cmdstring &= vbCrLf & " Left OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTStyle AS C ON A.FNHSysStyleId =C.FNHSysStyleId"
                                cmdstring &= vbCrLf & " WHERE C.FNHSysStyleId Is NULL "

                                If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN) = True Then

                                    cmdstring = " EXEC " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_GETBOMGARMENT_COPYTONEW '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & OrgBomID & "," & BomID & "  "
                                    HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                                Else
                                    BomID = 0
                                End If


                            End If

                        End If

                    End With

                    If BomID > 0 Then
                        Me.LoadData(BomID)
                        ShowBOMGarment(BomID)
                    End If

                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvlisting_DoubleClick(sender As Object, e As EventArgs) Handles ogvlisting.DoubleClick
        Try
            With Me.ogvlisting
                If .FocusedRowHandle >= 0 Then
                    Dim BomID As Integer = Val(.GetFocusedRowCellValue("FNHSysBomId").ToString)


                    If BomID > 0 Then

                        If ocmedit.Enabled = False Then
                            ShowBOMGarment(BomID, True)
                        Else
                            ShowBOMGarment(BomID)
                        End If

                    End If

                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogclisting_Click(sender As Object, e As EventArgs) Handles ogclisting.Click

    End Sub

    Private Sub wBOMListing_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Try
            Dim cmdstring As String = ""

            cmdstring = " Delete BOM "
            cmdstring &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_UserEidt  AS BOM  "
            cmdstring &= vbCrLf & " WHERE BOM.FTUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "

            HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

        Catch ex As Exception

        End Try
    End Sub
End Class