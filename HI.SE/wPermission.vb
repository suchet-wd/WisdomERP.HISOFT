Imports System.Windows.Forms
Imports System.Drawing
Imports System.IO
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors.Controls

Public Class wPermission

    Private _SysPathImageSystem As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images\System"
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"

    Private _FoundImageForm As Boolean = False
    Private _FoundImageObject As Boolean = False
    Private _StateNew As Boolean

    Sub New()
        _StateNew = True
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call InitTreeModule()
        Call InitTreeSubModule()
        Call InitTreeSub()

        AddHandler _lstpmdl.Click, AddressOf Module_Click
        _StateNew = False

        otppermissionhr.PageVisible = (HI.ST.SysInfo.Admin)

    End Sub

    Private _HSysID As Integer = 0
    Public Property HSysID() As Integer
        Get
            Return _HSysID
        End Get
        Set(value As Integer)
            _HSysID = value
        End Set
    End Property

    Private _ProcComplete As Boolean = False
    Public Property ProcComplete() As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(value As Boolean)
            _ProcComplete = value
        End Set
    End Property

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.ProcComplete = False
        Me.Close()
    End Sub

    Private TNode As TreeNode
    Private Frm_Load As Boolean
    Private imagemodule As New System.Windows.Forms.ImageList
    Private imageSub As New System.Windows.Forms.ImageList
    Private imageSubmodule As New System.Windows.Forms.ImageList

    Private Sub wRole_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try

            If Me.HSysID > 0 Then
                Dim _Str As String = "SELECT * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermission WHERE FNHSysPermissionID=" & Me.HSysID & " "
                Dim _DT As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)

                For Each R As DataRow In _DT.Rows
                    Me.FTPermissionCode.Text = R!FTPermissionCode.ToString
                    Me.FTPermissionNameTH.Text = R!FTPermissionNameTH.ToString
                    Me.FTPermissionNameEN.Text = R!FTPermissionNameEN.ToString
                    FTStateStaff.Checked = (R!FTStateStaff.ToString = "1")
                    FTStateStyleRiskCritical.Checked = (R!FTStateStyleRiskCritical.ToString = "1")
                    Exit For
                Next

            End If

            Call LoadModule(Me.HSysID)

            TBPermission.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Enum ListType As Integer

        _Node = 0
        _Module = 1
        _Menu = 2
        _Form = 3
        _Object = 4
        _User = 5

    End Enum


    Private Function GetListType(Value As String) As ListType
        Try
            Return CType([Enum].Parse(GetType(ListType), Value), ListType)
        Catch ex As Exception
            Return ListType._Menu
        End Try
    End Function
#Region "Module"

    Private Sub LoadModule(_PermissionID As Integer)
        Try
            Dim oDT As New DataTable
            imagemodule.Images.Clear()

            _LstModule.ClearNodes()
            _LstModule.StateImageList = imagemodule
            Dim _lstBlenSub As New DevExpress.XtraTreeList.Blending.XtraTreeListBlending
            _lstBlenSub.TreeListControl = _LstModule

            oDT = LoadSysModule(_PermissionID)

            _LstModule.BeginUnboundLoad()

            Call InitNodeModule(Nothing, oDT, _PermissionID, ListType._Module)

            _LstModule.EndUnboundLoad()

            oDT.Dispose()

        Catch ex As Exception
        End Try

    End Sub

    Private Sub InitTreeModule()
        With _LstModule

            .Columns.Add() : .Columns.Add()
            .Columns.Add() : .Columns.Add()
            .Columns.Add()
            .Columns.Add() : .Columns.Add()

            With .Columns.Item(0)
                .Name = "ColKey"
                .Caption = "Menu Name"
                .FieldName = "FTMnuName"
                .Visible = False
            End With

            With .Columns.Item(1)
                .Name = "FTCode"
                .Caption = "FTCode"
                .FieldName = "FTCode"
                .Visible = True
            End With

            With .Columns.Item(2)
                .Name = "FTNameTH"
                .Caption = "FTNameTH"
                .FieldName = "FTNameTH"
                .Visible = True
            End With

            With .Columns.Item(3)
                .Name = "FTNameEN"
                .Caption = "FTNameEN"
                .FieldName = "FTNameEN"
                .Visible = True
            End With

            With .Columns.Item(4)
                .Name = "Object"
                .Caption = "Object"
                .FieldName = "Object"
                .Visible = False
            End With

            With .Columns.Item(5)
                .Name = "SubKey"
                .Caption = "SubKey"
                .FieldName = "SubKey"
                .Visible = False
            End With

            With .Columns.Item(6)
                .Name = "FTType"
                .Caption = "FTType"
                .FieldName = "FTType"
                .Visible = False
            End With

            With .OptionsView
                .ShowColumns = True
                .ShowHorzLines = False
                .ShowFocusedFrame = False
                .ShowIndicator = False
                .ShowVertLines = False
                .ShowCheckBoxes = True
            End With

            With .OptionsPrint
                .PrintHorzLines = False
                .PrintVertLines = False
                .UsePrintStyles = True
            End With

            With .OptionsMenu
                .EnableFooterMenu = False
            End With

            With .OptionsBehavior
                .AutoNodeHeight = False
                .Editable = False
                .DragNodes = False
                .ResizeNodes = False
                .AllowExpandOnDblClick = True
            End With

            With .OptionsSelection
                .EnableAppearanceFocusedCell = False
                .EnableAppearanceFocusedRow = True
            End With

            With .Appearance
                With .SelectedRow
                    .BackColor = Color.GreenYellow
                    .ForeColor = Color.Blue
                End With
            End With

            .TreeLineStyle = DevExpress.XtraTreeList.LineStyle.None

        End With
    End Sub

    Private Function LoadSysModule(_PermissionID As Integer) As DataTable
        Try
            Dim tSql As String

            tSql = "SELECT  M.*,ISNULL(PM.FTStateSelect,'') AS FTStateSelect  "
            tSql &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysModule AS M With(NOLOCK) "
            tSql &= vbCrLf & " LEFT OUTER JOIN  (SELECT '1' AS FTStateSelect,FNHSysModuleID "
            tSql &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionModule AS PM With(NOLOCK) "
            tSql &= vbCrLf & "  WHERE PM.FNHSysPermissionID=" & Val(_PermissionID) & " "
            tSql &= vbCrLf & ") AS PM"
            tSql &= vbCrLf & " ON M.FNHSysModuleID=PM.FNHSysModuleID"

            If Not (HI.ST.SysInfo.Admin) Or (HI.ST.SysInfo.Admin And Not (HI.ST.SysInfo.AdminAllModule)) Then

                tSql &= vbCrLf & " INNER JOIN ("
                tSql &= vbCrLf & "  SELECT     DISTINCT  B.FNHSysModuleID"
                tSql &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS A  With(NOLOCK) INNER JOIN"
                tSql &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionModule AS B  With(NOLOCK) ON A.FNHSysPermissionID = B.FNHSysPermissionID"
                tSql &= vbCrLf & " WHERE  A.FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                tSql &= vbCrLf & "   ) AS Bx"
                tSql &= vbCrLf & " ON M.FNHSysModuleID = Bx.FNHSysModuleID"

            End If

            tSql &= vbCrLf & " WHERE ISNULL(M.FTStaActive,'0')='1'  "
            tSql &= vbCrLf & " ORDER BY M.FNSeq"

            Return HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_SYSTEM)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub InitNodeModule(ByVal _Node As DevExpress.XtraTreeList.Nodes.TreeListNode, ByVal oDbdt As System.Data.DataTable, _PermissionID As String, Optional _lstType As ListType = ListType._Module)
        Dim ModuleCode As String
        Dim ModuleID As Integer
        Dim _ModuleName As String = ""
        Dim tPathImgDis As String

        For Each R2 As DataRow In oDbdt.Rows

            ModuleCode = R2!FTModuleCode.ToString
            ModuleID = Integer.Parse(Val(R2!FNHSysModuleID.ToString))

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _ModuleName = R2!FTModuleNameTH.ToString
            Else
                _ModuleName = R2!FTModuleNameEN.ToString
            End If

            Dim snode As DevExpress.XtraTreeList.Nodes.TreeListNode
            snode = AddModuleTreeListNode(_Node, ModuleID, ModuleCode, R2!FTModuleNameTH.ToString, R2!FTModuleNameEN.ToString, ModuleCode, _PermissionID, _lstType)

            If R2!FTImg.ToString.Trim <> "" Then
                tPathImgDis = _SystemFilePath & "\Module\" & R2!FTImg.ToString.Trim

                If IO.File.Exists(tPathImgDis) Then
                    imagemodule.Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
                    snode.StateImageIndex = imagemodule.Images.Count - 1
                End If

            End If

            If R2!FTStateSelect.ToString = "1" Then
                snode.Checked = True
            Else
                snode.Checked = False
            End If

            snode.HasChildren = False

        Next

    End Sub

    Private Function AddModuleTreeListNode(ByVal _Node As DevExpress.XtraTreeList.Nodes.TreeListNode,
                             Value1 As String, Value2 As String, Value3 As String, Value4 As String, Value5 As String, Value6 As String, Value7 As ListType) As DevExpress.XtraTreeList.Nodes.TreeListNode

        Return _LstModule.AppendNode(New Object() {Value1, Value2, Value3, Value4, Value5, Value6, Value7}, _Node)

    End Function
#End Region

#Region "Permission Modile"

    Private Sub LoadPermissionModule(_PermissionID As Integer)
        Try
            Dim oDT As New DataTable
            imageSubmodule.Images.Clear()

            _lstpmdl.ClearNodes()
            _lstpmdl.StateImageList = imageSubmodule
            Dim _lstBlenSub As New DevExpress.XtraTreeList.Blending.XtraTreeListBlending
            _lstBlenSub.TreeListControl = _lstpmdl

            oDT = LoadSysModulePermission(_PermissionID)
            _lstpmdl.BeginUnboundLoad()
            Call InitNodeModulePermission(Nothing, oDT, _PermissionID, ListType._Module)
            _lstpmdl.EndUnboundLoad()

            oDT.Dispose()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub InitTreeSubModule()
        With _lstpmdl

            .Columns.Add() : .Columns.Add()
            .Columns.Add() : .Columns.Add()

            With .Columns.Item(0)
                .Name = "GColKey"
                .Caption = "Menu Name"
                .FieldName = "FTMnuName"
                .Visible = False
            End With

            With .Columns.Item(1)
                .Name = "GFTCode"
                .Caption = "FTCode"
                .FieldName = "FTCode"
                .Visible = False
            End With

            With .Columns.Item(2)
                .Name = "GFTName"
                .Caption = "FTName"
                .FieldName = "FTName"
                .Visible = True
            End With

            With .Columns.Item(3)
                .Name = "FFTType"
                .Caption = "FTType"
                .FieldName = "FTType"
                .Visible = False
            End With

            With .OptionsView
                .ShowColumns = True
                .ShowHorzLines = False
                .ShowFocusedFrame = False
                .ShowIndicator = False
                .ShowVertLines = False
                .ShowCheckBoxes = False
            End With

            With .OptionsPrint
                .PrintHorzLines = False
                .PrintVertLines = False
                .UsePrintStyles = True
            End With

            With .OptionsMenu
                .EnableFooterMenu = False
            End With

            With .OptionsBehavior
                .AutoNodeHeight = False
                .Editable = False
                .DragNodes = False
                .ResizeNodes = False
                .AllowExpandOnDblClick = True
            End With

            With .OptionsSelection
                .EnableAppearanceFocusedCell = False
                .EnableAppearanceFocusedRow = True
            End With

            With .Appearance
                With .SelectedRow
                    .BackColor = Color.GreenYellow
                    .ForeColor = Color.Blue
                End With
            End With

            .TreeLineStyle = DevExpress.XtraTreeList.LineStyle.None

        End With
    End Sub

    Private Function AddSubTreeModuleListNode(ByVal _Node As DevExpress.XtraTreeList.Nodes.TreeListNode,
                             Value1 As String, Value2 As String, Value3 As String, Value4 As ListType) As DevExpress.XtraTreeList.Nodes.TreeListNode

        Return _lstpmdl.AppendNode(New Object() {Value1, Value2, Value3, Value4}, _Node)

    End Function

    Private Function LoadSysModulePermission(_PermissionID As Integer) As DataTable
        Try
            Dim tSql As String

            tSql = "SELECT M.*  "
            tSql &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysModule AS M With(NOLOCK) "
            tSql &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionModule AS PM With(NOLOCK) "
            tSql &= vbCrLf & " ON M.FNHSysModuleID=PM.FNHSysModuleID"


            If Not (HI.ST.SysInfo.Admin) Or (HI.ST.SysInfo.Admin And Not (HI.ST.SysInfo.AdminAllModule)) Then

                tSql &= vbCrLf & " INNER JOIN ("
                tSql &= vbCrLf & "  SELECT     DISTINCT  B.FNHSysModuleID"
                tSql &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS A With(NOLOCK)  INNER JOIN"
                tSql &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionModule AS B With(NOLOCK)  ON A.FNHSysPermissionID = B.FNHSysPermissionID"
                tSql &= vbCrLf & " WHERE  A.FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                tSql &= vbCrLf & "   ) AS Bx"
                tSql &= vbCrLf & " ON M.FNHSysModuleID = Bx.FNHSysModuleID"

            End If

            tSql &= vbCrLf & " WHERE ISNULL(M.FTStaActive,'0')='1'  AND PM.FNHSysPermissionID=" & Val(_PermissionID) & " "
            tSql &= vbCrLf & " ORDER BY M.FNSeq"

            Return HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_SYSTEM)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Private Sub InitNodeModulePermission(ByVal _Node As DevExpress.XtraTreeList.Nodes.TreeListNode, ByVal oDbdt As System.Data.DataTable, _PermissionID As String, Optional _lstType As ListType = ListType._Module)
        Dim ModuleCode As String
        Dim ModuleID As Integer
        Dim _ModuleName As String = ""
        Dim tPathImgDis As String

        For Each R2 As DataRow In oDbdt.Rows

            ModuleCode = R2!FTModuleCode.ToString
            ModuleID = Integer.Parse(Val(R2!FNHSysModuleID.ToString))

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _ModuleName = R2!FTModuleNameTH.ToString
            Else
                _ModuleName = R2!FTModuleNameEN.ToString
            End If

            Dim snode As DevExpress.XtraTreeList.Nodes.TreeListNode
            snode = AddSubTreeModuleListNode(_Node, ModuleID, ModuleCode, _ModuleName, _lstType)

            If R2!FTImg.ToString.Trim <> "" Then
                tPathImgDis = _SystemFilePath & "\Module\" & R2!FTImg.ToString.Trim

                If IO.File.Exists(tPathImgDis) Then
                    imageSubmodule.Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
                    snode.StateImageIndex = imageSubmodule.Images.Count - 1
                End If

            End If
            snode.HasChildren = False


        Next

    End Sub


#End Region

#Region "Permission Menu"

    Private Sub LoadPermissionMenu(_PermissionID As Integer, _ModuleID As Integer)
        Try
            Dim oDT As New DataTable
            imageSub.Images.Clear()

            _FoundImageForm = False
            _FoundImageObject = False

            _LstDetail.ClearNodes()
            _LstDetail.StateImageList = imageSub

            Dim tPathImgDis As String = _SystemFilePath & "\System\Form.png"
            If IO.File.Exists(tPathImgDis) Then
                _FoundImageForm = True
                imageSub.Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
            End If

            tPathImgDis = _SystemFilePath & "\System\Object.png"
            If IO.File.Exists(tPathImgDis) Then
                _FoundImageObject = True
                imageSub.Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
            End If

            Dim _lstBlenSub As New DevExpress.XtraTreeList.Blending.XtraTreeListBlending
            _lstBlenSub.TreeListControl = _LstDetail

            oDT = LoadSysMenuPermission(_ModuleID, _PermissionID)
            _LstDetail.BeginUnboundLoad()
            Call InitNodePermissionMenu(Nothing, -1, oDT, _ModuleID, _PermissionID)
            _LstDetail.EndUnboundLoad()

            oDT.Dispose()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub InitTreeSub()
        With _LstDetail

            .Columns.Add() : .Columns.Add()
            .Columns.Add() : .Columns.Add()
            .Columns.Add() : .Columns.Add()

            With .Columns.Item(0)
                .Name = "SColKey"
                .Caption = "Menu Name"
                .FieldName = "FTMnuName"
                .Visible = False
            End With

            With .Columns.Item(1)
                .Name = "SFTCode"
                .Caption = "FTCode"
                .FieldName = "FTCode"
                .Visible = False
            End With

            With .Columns.Item(2)
                .Name = "SFTName"
                .Caption = "FTNameTH"
                .FieldName = "FTNameTH"
                .Visible = True
            End With

            With .Columns.Item(3)
                .Name = "SObject"
                .Caption = "Object"
                .FieldName = "Object"
                .Visible = False
            End With

            With .Columns.Item(4)
                .Name = "SSubKey"
                .Caption = "SubKey"
                .FieldName = "SubKey"
                .Visible = False
            End With

            With .Columns.Item(5)
                .Name = "SFTType"
                .Caption = "FTType"
                .FieldName = "FTType"
                .Visible = False
            End With

            With .OptionsView
                .ShowColumns = True
                .ShowHorzLines = False
                .ShowFocusedFrame = False
                .ShowIndicator = False
                .ShowVertLines = False
                .ShowCheckBoxes = True
            End With

            With .OptionsPrint
                .PrintHorzLines = False
                .PrintVertLines = False
                .UsePrintStyles = True
            End With

            With .OptionsMenu
                .EnableFooterMenu = False
            End With

            With .OptionsBehavior
                .AutoNodeHeight = False
                .Editable = False
                .DragNodes = False
                .ResizeNodes = False
                .AllowExpandOnDblClick = True
            End With

            With .OptionsSelection
                .EnableAppearanceFocusedCell = False
                .EnableAppearanceFocusedRow = True
            End With

            With .Appearance
                With .SelectedRow
                    .BackColor = Color.GreenYellow
                    .ForeColor = Color.Blue
                End With
            End With

            .TreeLineStyle = DevExpress.XtraTreeList.LineStyle.None

        End With
    End Sub

    Private Sub InitNodePermissionModule(ByVal _Node As DevExpress.XtraTreeList.Nodes.TreeListNode, ByVal oDbdt As System.Data.DataTable, _PermissionID As String, Optional _lstType As ListType = ListType._Module)
        Dim ModuleCode As String
        Dim ModuleID As Integer
        Dim _ModuleName As String = ""
        Dim tPathImgDis As String

        For Each R2 As DataRow In oDbdt.Rows

            ModuleCode = R2!FTModuleCode.ToString
            ModuleID = Integer.Parse(Val(R2!FNHSysModuleID.ToString))

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _ModuleName = R2!FTModuleNameTH.ToString
            Else
                _ModuleName = R2!FTModuleNameEN.ToString
            End If

            Dim snode As DevExpress.XtraTreeList.Nodes.TreeListNode
            snode = AddSubTreeListNode(_Node, ModuleID, ModuleCode, _ModuleName, ModuleCode, _PermissionID, _lstType)

            If R2!FTImg.ToString.Trim <> "" Then
                tPathImgDis = _SystemFilePath & "\Module\" & R2!FTImg.ToString.Trim

                If IO.File.Exists(tPathImgDis) Then
                    imageSub.Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
                    snode.StateImageIndex = imageSub.Images.Count - 1
                End If

            End If

            snode.Checked = True

            oDbdt = LoadSysMenuPermission(ModuleID, _PermissionID)

            If IsNothing(oDbdt) = False Then
                Dim aDbRow() As DataRow
                aDbRow = oDbdt.Select("FNMnuIDParent=-1", "FNSeq")

                If aDbRow.Length > 0 Then
                    Try
                        snode.HasChildren = True
                        snode.Tag = True
                    Catch ex As Exception
                    End Try

                    Call InitNodePermissionMenu(snode, -1, oDbdt, ModuleCode, _PermissionID)
                Else
                    snode.HasChildren = False
                End If
            Else
                snode.HasChildren = False
            End If

        Next

    End Sub

    Private Sub InitNodePermissionMenu(ByVal _Node As DevExpress.XtraTreeList.Nodes.TreeListNode, ByVal MnuParent As Double, ByVal oDbdt As System.Data.DataTable, ByVal _ModuleID As String, ByVal _PermissionID As String)
        Dim aDbRow() As DataRow
        Dim _DtForm As DataTable

        aDbRow = oDbdt.Select("FNMnuIDParent=" & MnuParent & "", "FNSeq")
        If aDbRow.Length = 0 Then Exit Sub

        Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode
        Try

            Dim _StateChild As Boolean = False
            For Each R2 As DataRow In aDbRow
                Try

                    Dim _MenuName As String = ""

                    If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                        _MenuName = R2!FTCaptionTH.ToString()
                    Else
                        _MenuName = R2!FTCaptionEN.ToString()
                    End If

                    _StateChild = False

                    node = AddSubTreeListNode(_Node, R2!FTMnuName.ToString, R2!FTMnuName.ToString, _MenuName, R2!FTMnuName.ToString, _PermissionID, ListType._Menu)

                    If R2!FTImg.ToString.Trim <> "" Then
                        Dim tPathImgDis As String = _SystemFilePath & "\Menu\" & R2!FTImg.ToString.Trim
                        If IO.File.Exists(tPathImgDis) Then
                            imageSub.Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
                            node.StateImageIndex = imageSub.Images.Count - 1
                        End If
                    End If

                    If R2!FTStateSelect.ToString = "1" Then
                        node.Checked = True
                    Else
                        node.Checked = False
                    End If

                    _DtForm = Me.LoadSyFormPermission(_PermissionID, R2!FTMnuName.ToString)

                    If _DtForm.Rows.Count > 0 Then
                        _StateChild = True

                        node.HasChildren = True
                        node.Tag = True

                        InitNodePermissionForm(node, _DtForm, _ModuleID, _PermissionID, ListType._Form)
                    End If

                    If CheckExistsSubMenu(R2!FNMnuID.ToString, oDbdt) Then

                        Try
                            If Not (_StateChild) Then
                                node.HasChildren = True
                                node.Tag = True
                            End If

                        Catch ex As Exception
                        End Try

                        InitNodePermissionMenu(node, R2!FNMnuID.ToString, oDbdt, _ModuleID, _PermissionID)
                    Else
                        If Not (_StateChild) Then
                            node.HasChildren = False
                        End If
                    End If

                Catch
                End Try
            Next
        Catch
        End Try
        '_Lst.EndUnboundLoad()
    End Sub

    Private Sub InitNodePermissionForm(ByVal _Node As DevExpress.XtraTreeList.Nodes.TreeListNode, ByVal oDbdt As System.Data.DataTable, ModuleID As Integer, _PermissionID As String, Optional _lstType As ListType = ListType._Form)
        Dim _Name As String = ""
        For Each R2 As DataRow In oDbdt.Rows

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Name = R2!FTLangTH.ToString
            Else
                _Name = R2!FTLangEN.ToString
            End If

            If (HI.ST.SysInfo.Admin) Then
                _Name = _Name & "( " & R2!FTFormName.ToString & " )"
            End If

            Dim snode As DevExpress.XtraTreeList.Nodes.TreeListNode
            snode = AddSubTreeListNode(_Node, R2!FTFormName.ToString, R2!FTFormName.ToString, _Name, R2!FTMnuName.ToString, ModuleID, ListType._Form)

            If (_FoundImageForm) Then
                snode.StateImageIndex = 0
            End If

            If R2!FTStateSelect.ToString = "1" Then
                snode.Checked = True
            Else
                snode.Checked = False
            End If

            oDbdt = LoadSysObjectPermission(_PermissionID, R2!FTMnuName.ToString, R2!FTFormName.ToString)

            If IsNothing(oDbdt) = False Then

                If oDbdt.Rows.Count > 0 Then
                    Try
                        snode.HasChildren = True
                        snode.Tag = True
                    Catch ex As Exception
                    End Try

                    Call InitNodePermissionObject(snode, oDbdt, _PermissionID, R2!FTMnuName.ToString, R2!FTFormName.ToString, ListType._Object)
                Else
                    snode.HasChildren = False
                End If
            Else
                snode.HasChildren = False
            End If
        Next
    End Sub

    Private Sub InitNodePermissionObject(ByVal _Node As DevExpress.XtraTreeList.Nodes.TreeListNode, ByVal oDbdt As System.Data.DataTable, _PermissionID As String, _MenuName As String, _FormName As String, Optional _lstType As ListType = ListType._Object)
        Dim _Name As String = ""
        For Each R2 As DataRow In oDbdt.Rows

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Name = R2!FTLangTH.ToString
            Else
                _Name = R2!FTLangEN.ToString
            End If

            If (HI.ST.SysInfo.Admin) Then
                _Name = _Name & "( " & R2!FTObjectName.ToString & " )"
            End If

            Dim snode As DevExpress.XtraTreeList.Nodes.TreeListNode
            snode = AddSubTreeListNode(_Node, R2!FTObjectName.ToString, _MenuName, _Name, _FormName, _PermissionID, _lstType)

            If (_FoundImageObject) Then
                If (_FoundImageForm) Then
                    snode.StateImageIndex = 1
                Else
                    snode.StateImageIndex = 0
                End If
            End If

            If R2!FTStateSelect.ToString = "1" Then
                snode.Checked = True
            Else
                snode.Checked = False
            End If

            snode.HasChildren = False
        Next
    End Sub

    Private Function AddSubTreeListNode(ByVal _Node As DevExpress.XtraTreeList.Nodes.TreeListNode,
                               Value1 As String, Value2 As String, Value3 As String, Value4 As String, Value5 As String, Value6 As ListType) As DevExpress.XtraTreeList.Nodes.TreeListNode

        Return _LstDetail.AppendNode(New Object() {Value1, Value2, Value3, Value4, Value5, Value6}, _Node)

    End Function

    Private Function LoadSysMenuPermission(ByVal ModuleID As Integer, _PermissionID As Integer) As DataTable
        Try
            Dim tSql As String
            tSql = "SELECT DISTINCT M.* ,ISNULL(PM.FTStateSelect,'') AS FTStateSelect "
            tSql &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysMenu AS M With(NOLOCK) "
            tSql &= vbCrLf & " LEFT OUTER JOIN   (  SELECT '1' AS FTStateSelect,FTMnuName "
            tSql &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu AS PM With(NOLOCK)"
            tSql &= vbCrLf & "   WHERE PM.FNHSysPermissionID=" & Val(_PermissionID) & "  "
            tSql &= vbCrLf & " ) AS PM "
            tSql &= vbCrLf & " ON M.FTMnuName=PM.FTMnuName"

            If Not (HI.ST.SysInfo.Admin) Or (HI.ST.SysInfo.Admin And Not (HI.ST.SysInfo.AdminAllModule)) Then

                tSql &= vbCrLf & " INNER JOIN ("
                tSql &= vbCrLf & "  SELECT      DISTINCT  B.FTMnuName"
                tSql &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS A  With(NOLOCK) INNER JOIN"
                tSql &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu AS B  With(NOLOCK) ON A.FNHSysPermissionID = B.FNHSysPermissionID"
                tSql &= vbCrLf & " WHERE  A.FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                tSql &= vbCrLf & "   ) AS Bx"
                tSql &= vbCrLf & " ON M.FTMnuName = Bx.FTMnuName"
            Else

            End If

            tSql &= vbCrLf & " WHERE ISNULL(M.FTStaActive,'0')='1'"
            tSql &= vbCrLf & " AND M.FNHSysModuleID=" & ModuleID & " AND M.FNMnuID <> M.FNMnuIDParent "

            Return HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_SYSTEM)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Function LoadSyFormPermission(_PermissionID As Integer, MemuName As String) As DataTable
        Try
            Dim tSql As String
            tSql = "SELECT DISTINCT  M.* ,ISNULL(PM.FTStateSelect,'') AS FTStateSelect "
            tSql &= vbCrLf & " FROM ("
            tSql &= vbCrLf & " SELECT        M.*, B.FTLangEN, B.FTLangTH"
            tSql &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysObjectForm AS M WITH (NOLOCK) LEFT OUTER JOIN"
            tSql &= vbCrLf & "    (SELECT        FTFormName, FTObjectName, FTLangEN, FTLangTH, FTLangVT, FTLangKM"
            tSql &= vbCrLf & "   FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) & "].dbo.HSysLanguage WITH (NOLOCK)"
            tSql &= vbCrLf & "   WHERE        (FTFormName = FTObjectName)) AS B ON M.FTFormName = B.FTFormName"
            tSql &= vbCrLf & " WHERE  M.FTMnuName='" & HI.UL.ULF.rpQuoted(MemuName) & "' "
            tSql &= vbCrLf & " ) AS M "
            tSql &= vbCrLf & " LEFT OUTER JOIN   (  SELECT '1' AS FTStateSelect,FTMnuName,FTFormName "
            tSql &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionObjectControl AS PM With(NOLOCK)"
            tSql &= vbCrLf & "   WHERE PM.FNHSysPermissionID=" & Val(_PermissionID) & " "
            tSql &= vbCrLf & "   AND PM.FTMnuName='" & HI.UL.ULF.rpQuoted(MemuName) & "' "
            tSql &= vbCrLf & "    GROUP BY FTMnuName,FTFormName "
            tSql &= vbCrLf & " ) AS PM "
            tSql &= vbCrLf & " ON M.FTMnuName=PM.FTMnuName"

            Return HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_SYSTEM)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Function LoadSysObjectPermission(_PermissionID As Integer, MemuName As String, FormName As String) As DataTable
        Try
            Dim tSql As String
            tSql = "SELECT DISTINCT  M.* ,ISNULL(PM.FTStateSelect,'') AS FTStateSelect "
            tSql &= vbCrLf & " FROM ("
            tSql &= vbCrLf & " SELECT       M.*, B.FTLangEN, B.FTLangTH"
            tSql &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysObjectControl AS M WITH (NOLOCK) LEFT OUTER JOIN"
            tSql &= vbCrLf & "    (SELECT      FTFormName, FTObjectName, FTLangEN, FTLangTH, FTLangVT, FTLangKM"
            tSql &= vbCrLf & "   FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) & "].dbo.HSysLanguage WITH (NOLOCK)"
            tSql &= vbCrLf & "   WHERE        (FTFormName <> FTObjectName)) AS B ON M.FTFormName = B.FTFormName AND M.FTObjectName = B.FTObjectName"
            tSql &= vbCrLf & " WHERE  M.FTMnuName='" & HI.UL.ULF.rpQuoted(MemuName) & "' AND  M.FTFormName='" & HI.UL.ULF.rpQuoted(FormName) & "' "
            tSql &= vbCrLf & " ) AS M "
            tSql &= vbCrLf & " LEFT OUTER JOIN   (  SELECT '1' AS FTStateSelect,FTMnuName,FTFormName,FTObjName "
            tSql &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionObjectControl AS PM With(NOLOCK)"
            tSql &= vbCrLf & "   WHERE PM.FNHSysPermissionID=" & Val(_PermissionID) & " "
            tSql &= vbCrLf & "   AND PM.FTMnuName='" & HI.UL.ULF.rpQuoted(MemuName) & "' AND FTFormName='" & HI.UL.ULF.rpQuoted(FormName) & "'  "
            tSql &= vbCrLf & "    GROUP BY FTMnuName,FTFormName,FTObjName "
            tSql &= vbCrLf & " ) AS PM "
            tSql &= vbCrLf & " ON M.FTMnuName=PM.FTMnuName AND M.FTObjectName=PM.FTObjName"

            Return HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_SYSTEM)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Function CheckExistsSubMenu(ByVal MnuParent As Double, ByVal oDbdt As System.Data.DataTable) As Boolean
        Try
            Dim aDbRow() As DataRow
            aDbRow = oDbdt.Select("FNMnuIDParent=" & MnuParent & "", "FNSeq")
            Return (aDbRow.Length > 0)
        Catch ex As Exception
            ' MsgBox(ex.Message)
            Throw New Exception(ex.Message)
        End Try
    End Function

#End Region

    Private Sub ocmsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsave.Click

        Select Case TBPermission.SelectedTabPage.Name
            Case otp.Name
                If VerifyData() Then
                    If Me.Saveata() Then

                        Call LoadModule(Me.HSysID)

                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    Else
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    End If
                End If
            Case otppermission.Name

                Dim _Str As String = ""
                Dim _Spls As New HI.TL.SplashScreen("Saving... Permission Please wait.")
                For Each _Node As DevExpress.XtraTreeList.Nodes.TreeListNode In _LstDetail.Nodes

                    Select Case GetListType(_Node.GetValue(5).ToString)
                        Case ListType._Menu
                            _Str = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu  "
                            _Str &= vbCrLf & " WHERE FNHSysPermissionID = " & Val(Me.HSysID) & " "
                            _Str &= vbCrLf & " AND FTMnuName='" & HI.UL.ULF.rpQuoted(_Node.GetValue(0).ToString) & "' "

                            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY)
                            'Case ListType._Object
                            '    _Str = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionObjectControl  "
                            '    _Str &= vbCrLf & " WHERE FNHSysPermissionID = " & Val(Me.HSysID) & " "
                            '    _Str &= vbCrLf & "  AND FTMnuName='" & HI.UL.ULF.rpQuoted(_Node.GetValue(1).ToString) & "' "
                            '    _Str &= vbCrLf & "  AND FTFormName='" & HI.UL.ULF.rpQuoted(_Node.GetValue(3).ToString) & "' "
                            '    _Str &= vbCrLf & "  AND FTObjName='" & HI.UL.ULF.rpQuoted(_Node.GetValue(0).ToString) & "' "

                            '    HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY)
                    End Select

                    If _Node.Checked Then

                        Select Case GetListType(_Node.GetValue(5).ToString)
                            Case ListType._Menu
                                _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu(FTInsUser, FDInsDate, FTInsTime,  FNHSysPermissionID,  FTMnuName)"
                                _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',Convert(varchar(10),GetDate(),111),Convert(varchar(8),GetDate(),114)"
                                _Str &= vbCrLf & " ," & Val(Me.HSysID) & ",'" & HI.UL.ULF.rpQuoted(_Node.GetValue(0).ToString) & "' "
                                HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY)
                            Case ListType._Form

                            Case ListType._Object

                        End Select
                    End If

                    Call SavePermissionMenu(_Node)

                Next

                _Spls.Close()

                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

            Case otppermissionhr.Name
                Call SavePermissionHR()
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Case otppermissioncmp.Name
                Call SavePermissionCmp()
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Case Else


        End Select
    End Sub

    Private Sub SavePermissionMenu(ByVal Node As DevExpress.XtraTreeList.Nodes.TreeListNode)
        Try
            Dim _Str As String
            For Each _Node As DevExpress.XtraTreeList.Nodes.TreeListNode In Node.Nodes

                Select Case GetListType(_Node.GetValue(5).ToString)
                    Case ListType._Menu

                        _Str = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu "
                        _Str &= vbCrLf & " WHERE FNHSysPermissionID = " & Val(Me.HSysID) & " "
                        _Str &= vbCrLf & " AND FTMnuName='" & HI.UL.ULF.rpQuoted(_Node.GetValue(0).ToString) & "' "

                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY)
                    Case ListType._Object

                        _Str = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionObjectControl  "
                        _Str &= vbCrLf & " WHERE FNHSysPermissionID = " & Val(Me.HSysID) & " "
                        _Str &= vbCrLf & "  AND FTMnuName='" & HI.UL.ULF.rpQuoted(_Node.GetValue(1).ToString) & "' "
                        _Str &= vbCrLf & "  AND FTFormName='" & HI.UL.ULF.rpQuoted(_Node.GetValue(3).ToString) & "' "
                        _Str &= vbCrLf & "  AND FTObjName='" & HI.UL.ULF.rpQuoted(_Node.GetValue(0).ToString) & "' "

                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY)
                End Select

                If _Node.Checked = True Then

                    Select Case GetListType(_Node.GetValue(5).ToString)
                        Case ListType._Menu

                            _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu(FTInsUser, FDInsDate, FTInsTime,  FNHSysPermissionID,  FTMnuName)"
                            _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',Convert(varchar(10),GetDate(),111),Convert(varchar(8),GetDate(),114)"
                            _Str &= vbCrLf & " ," & Val(Me.HSysID) & ",'" & HI.UL.ULF.rpQuoted(_Node.GetValue(0).ToString) & "' "

                            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY)

                        Case ListType._Form
                        Case ListType._Object

                            _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionObjectControl(FTInsUser, FDInsDate, FTInsTime"
                            _Str &= vbCrLf & ",FNHSysPermissionID,FTMnuName,FTFormName,FTObjName)"
                            _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',Convert(varchar(10),GetDate(),111),Convert(varchar(8),GetDate(),114)"
                            _Str &= vbCrLf & " ," & Val(Me.HSysID) & ",'" & HI.UL.ULF.rpQuoted(_Node.GetValue(1).ToString) & "' "
                            _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_Node.GetValue(3).ToString) & "'"
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Node.GetValue(0).ToString) & "' "

                            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY)

                    End Select

                End If

                Call SavePermissionMenu(_Node)
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SavePermissionHR()
        Dim Strsql As String

        Strsql = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType  WHERE  FNHSysPermissionID=" & Val(Me.HSysID) & " "
        HI.Conn.SQLConn.ExecuteNonQuery(Strsql, Conn.DB.DataBaseName.DB_SECURITY)

        Strsql = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeTypeSect  WHERE  FNHSysPermissionID=" & Val(Me.HSysID) & " "
        HI.Conn.SQLConn.ExecuteNonQuery(Strsql, Conn.DB.DataBaseName.DB_SECURITY)

        Strsql = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeTypeUnitSect  WHERE  FNHSysPermissionID=" & Val(Me.HSysID) & " "
        HI.Conn.SQLConn.ExecuteNonQuery(Strsql, Conn.DB.DataBaseName.DB_SECURITY)

        ogv.FocusedRowHandle = 0
        ogv.FocusedColumn = ogv.Columns.ColumnByName("FTEmpTypeCode")

        CType(ogc.DataSource, DataTable).AcceptChanges()

        For Each R As DataRow In CType(ogc.DataSource, DataTable).Rows

            If R!FTSelect.ToString = "1" Then

                Strsql = "Insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType(FTInsUser, FDInsDate, FTInsTime, FNHSysPermissionID, FNHSysEmpTypeId,FTStateSalary,FTStateAll,FTStateAllUnit) "
                Strsql &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',Convert(varchar(10),GetDate(),111),Convert(varchar(8),GetDate(),114)"
                Strsql &= vbCrLf & ", " & Val(Me.HSysID) & "," & Val(R!FNHSysEmpTypeId.ToString) & ",'" & R!FTStateSalary.ToString & "','" & R!FTStateAll.ToString & "','" & R!FTStateAllUnit.ToString & "' "
                HI.Conn.SQLConn.ExecuteNonQuery(Strsql, Conn.DB.DataBaseName.DB_SECURITY)

            End If

        Next
        CType(ogcsect.DataSource, DataTable).AcceptChanges()
        For Each R As DataRow In CType(ogcsect.DataSource, DataTable).Rows

            If R!FTSelect.ToString = "1" Then
                Strsql = "Insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeTypeSect(FTInsUser, FDInsDate, FTInsTime, FNHSysPermissionID, FNHSysSectId,FTStateAll) "
                Strsql &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',Convert(varchar(10),GetDate(),111),Convert(varchar(8),GetDate(),114)"
                Strsql &= vbCrLf & ", " & Val(Me.HSysID) & "," & Val(R!FNHSysSectId.ToString) & " ,'" & R!FTStateAll.ToString & "'"
                HI.Conn.SQLConn.ExecuteNonQuery(Strsql, Conn.DB.DataBaseName.DB_SECURITY)
            End If
        Next

        CType(ogcunitsect.DataSource, DataTable).AcceptChanges()
        For Each R As DataRow In CType(ogcunitsect.DataSource, DataTable).Rows

            If R!FTSelect.ToString = "1" Then
                Strsql = "Insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeTypeUnitSect(FTInsUser, FDInsDate, FTInsTime, FNHSysPermissionID,FNHSysUnitSectId) "
                Strsql &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',Convert(varchar(10),GetDate(),111),Convert(varchar(8),GetDate(),114)"
                Strsql &= vbCrLf & ", " & Val(Me.HSysID) & "," & Val(R!FNHSysUnitSectId.ToString) & ""
                HI.Conn.SQLConn.ExecuteNonQuery(Strsql, Conn.DB.DataBaseName.DB_SECURITY)

            End If

        Next

    End Sub

    Private Sub SavePermissionCmp()
        Dim Strsql As String

        Strsql = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionCmp  WHERE  FNHSysPermissionID=" & Val(Me.HSysID) & " "
        HI.Conn.SQLConn.ExecuteNonQuery(Strsql, Conn.DB.DataBaseName.DB_SECURITY)

        CType(ogccmp.DataSource, DataTable).AcceptChanges()

        For Each R As DataRow In CType(ogccmp.DataSource, DataTable).Rows

            If R!FTSelect.ToString = "1" Then
                Strsql = "Insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionCmp(FTInsUser, FDInsDate, FTInsTime, FNHSysPermissionID, FNHSysCmpId) "
                Strsql &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',Convert(varchar(10),GetDate(),111),Convert(varchar(8),GetDate(),114)"
                Strsql &= vbCrLf & ", " & Val(Me.HSysID) & "," & Val(R!FNHSysCmpId.ToString) & " "
                HI.Conn.SQLConn.ExecuteNonQuery(Strsql, Conn.DB.DataBaseName.DB_SECURITY)

            End If

        Next

    End Sub


    Private Function Saveata() As Boolean
        Try

            If Me.HSysID = 0 Then
                Me.HSysID = HI.SE.RunID.GetRunNoID("TSEPermission", "FNHSysPermissionID", Conn.DB.DataBaseName.DB_SECURITY)
            End If

            Dim _Str As String = ""

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SECURITY)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try
                _Str = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermission SET "
                _Str &= vbCrLf & " FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Str &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                _Str &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                _Str &= vbCrLf & " ,FTPermissionCode='" & HI.UL.ULF.rpQuoted(FTPermissionCode.Text.Trim) & "' "
                _Str &= vbCrLf & " ,FTPermissionNameTH='" & HI.UL.ULF.rpQuoted(FTPermissionNameTH.Text.Trim) & "'"
                _Str &= vbCrLf & " ,FTPermissionNameEN='" & HI.UL.ULF.rpQuoted(FTPermissionNameEN.Text.Trim) & "'"
                _Str &= vbCrLf & " ,FTStateStaff='" & FTStateStaff.EditValue.ToString() & "'"
                _Str &= vbCrLf & " ,FTStateStyleRiskCritical='" & FTStateStyleRiskCritical.EditValue.ToString() & "'"
                _Str &= vbCrLf & " WHERE FNHSysPermissionID=" & Me.HSysID & " "

                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermission(FTInsUser, FDInsDate, FTInsTime,  FNHSysPermissionID, FTPermissionCode, FTPermissionNameTH, FTPermissionNameEN,FTStateStaff,FTStateStyleRiskCritical) "
                    _Str &= vbCrLf & " SELECT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                    _Str &= vbCrLf & " ," & Me.HSysID
                    _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTPermissionCode.Text.Trim) & "' "
                    _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTPermissionNameTH.Text.Trim) & "'"
                    _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTPermissionNameEN.Text.Trim) & "'"
                    _Str &= vbCrLf & " ,'" & FTStateStaff.EditValue.ToString() & "'"
                    _Str &= vbCrLf & " ,'" & FTStateStyleRiskCritical.EditValue.ToString() & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If

                _Str = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionModule WHERE  FNHSysPermissionID=" & Me.HSysID & "  "
                HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                For Each _Node As DevExpress.XtraTreeList.Nodes.TreeListNode In _LstModule.Nodes

                    Dim _ModuleID As String = _Node.GetValue(0)

                    If _Node.Checked Then
                        _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionModule(FTInsUser, FDInsDate, FTInsTime, FNHSysPermissionID, FNHSysModuleID) "
                        _Str &= vbCrLf & " SELECT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                        _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                        _Str &= vbCrLf & " ," & Me.HSysID
                        _Str &= vbCrLf & " ," & Val(_ModuleID)
                        HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    End If
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
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False
        If Me.FTPermissionCode.Text.Trim <> "" Then
            If Me.FTPermissionNameTH.Text.Trim <> "" Then
                If Me.FTPermissionNameEN.Text.Trim <> "" Then

                    Dim _Str As String = "SELECT TOP 1  FTPermissionCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermission WHERE FTPermissionCode='" & HI.UL.ULF.rpQuoted(Me.FTPermissionCode.Text.Trim) & "'  AND  FNHSysPermissionID<> " & Me.HSysID & " "
                    If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SECURITY, "") = "" Then
                        _Pass = True
                    Else
                        HI.MG.ShowMsg.mInvalidData("พบข้อมูล Role นี้ในระบบแล้ว .....", 1306190004, Me.Text)
                        FTPermissionCode.Focus()
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPermissionNameEN_lbl.Text)
                    FTPermissionNameEN.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPermissionNameTH_lbl.Text)
                FTPermissionNameTH.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPermissionCode_lbl.Text)
            FTPermissionCode.Focus()
        End If

        Return _Pass

    End Function

    Private Sub TBRole_SelectedPageChanged(ByVal sender As Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles TBPermission.SelectedPageChanged
        Select Case e.Page.Name.ToString
            Case otppermission.Name.ToString

                LoadPermissionModule(Me.HSysID)

            Case otppermissionhr.Name.ToString

                Call LoadPermissionHR()
            Case otppermissioncmp.Name.ToString
                Call LoadPermissionCmp()

        End Select
    End Sub

    Private Sub TBRole_SelectedPageChanging(ByVal sender As Object, ByVal e As DevExpress.XtraTab.TabPageChangingEventArgs) Handles TBPermission.SelectedPageChanging

        If (_StateNew) Then Exit Sub
        Select Case e.Page.Name.ToString
            Case otppermission.Name.ToString, otppermissionhr.Name.ToString
                Dim _Str As String

                _Str = "SELECT TOP 1  FNHSysPermissionID  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermission WHERE FNHSysPermissionID=" & Me.HSysID & ""
                If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SECURITY, "") = "" Then

                    HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลการกำหนด Permission กรุณาทำการตรวจสอบ !!!", 1306190002, Me.Text)
                    e.Cancel = True

                Else

                    _Str = "SELECT TOP 1  FNHSysPermissionID  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionModule WHERE FNHSysPermissionID=" & Me.HSysID & ""
                    If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SECURITY, "") = "" Then
                        HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลการกำหนด Module สำหรับ Permission กรุณาทำการตรวจสอบ !!!", 1306190003, Me.Text)
                        e.Cancel = True
                    Else
                        e.Cancel = False
                    End If

                End If

            Case Else
                e.Cancel = False
        End Select
    End Sub

    Private Sub LoadPermissionHR()

        Dim _Str As String

        _Str = " SELECT    "
        _Str &= vbCrLf & " CASE WHEN S.FNHSysEmpTypeId IS NULL THEN '0' ELSE '1' END AS FTSelect "
        _Str &= vbCrLf & " ,  M.FNHSysEmpTypeId, M.FTEmpTypeCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & " , M.FTEmpTypeNameTH AS FTEmpTypeDesc "
        Else
            _Str &= vbCrLf & " , M.FTEmpTypeNameEN AS FTEmpTypeDesc "
        End If
        _Str &= vbCrLf & " ,ISNULL(S.FTStateSalary,'0') AS FTStateSalary "
        _Str &= vbCrLf & " ,ISNULL(S.FTStateAll,'0') AS FTStateAll "
        _Str &= vbCrLf & " ,ISNULL(S.FTStateAllUnit,'0') AS FTStateAllUnit  , C.FNHSysCmpId"
        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS M WITH(NOLOCK) "
        _Str &= vbCrLf & " LEFT OUTER JOIN ("
        _Str &= vbCrLf & " SELECT FNHSysPermissionID, FNHSysEmpTypeId, FTStateSalary, FTStateAll, FTStateAllUnit   "
        _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType WITH(NOLOCK)"
        _Str &= vbCrLf & "   WHERE FNHSysPermissionID = " & Val(Me.HSysID) & ""
        _Str &= vbCrLf & " ) AS S ON M.FNHSysEmpTypeId = S.FNHSysEmpTypeId"
        _Str &= vbCrLf & " Outer Apply ( Select Top 1     FTCmpCode as  FNHSysCmpId  "
        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C "
        _Str &= vbCrLf & " where C.FNHSysCmpId =M.FNHSysCmpId"
        _Str &= vbCrLf & ""
        _Str &= vbCrLf & " ) C"

        _Str &= vbCrLf & " WHERE ISNULL(M.FTStateActive,'') ='1' "
        _Str &= vbCrLf & "  ORDER BY M.FTEmpTypeCode "

        Me.ogc.DataSource = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)

        _Str = " SELECT  CASE WHEN S.FNHSysPermissionID IS NULL THEN '0' ELSE '1' END  AS FTSelect "
        _Str &= vbCrLf & ",M.FNHSysSectId"
        _Str &= vbCrLf & ",M.FTSectCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & " , M.FTSectNameTH AS FTSectName "
        Else
            _Str &= vbCrLf & " , M.FTSectNameEN AS FTSectName "
        End If
        _Str &= vbCrLf & " ,ISNULL(S.FTStateAll,'0') AS FTStateAll , C.FNHSysCmpId "

        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS M WITH(NOLOCK) "
        _Str &= vbCrLf & " LEFT OUTER JOIN ("
        _Str &= vbCrLf & " SELECT FNHSysPermissionID,FNHSysSectId,FTStateAll  "
        _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeTypeSect WITH(NOLOCK)"
        _Str &= vbCrLf & "   WHERE FNHSysPermissionID = " & Val(Me.HSysID) & ""
        _Str &= vbCrLf & " ) AS S ON M.FNHSysSectId = S.FNHSysSectId"
        _Str &= vbCrLf & " Outer Apply ( Select Top 1     FTCmpCode as  FNHSysCmpId  "
        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C "
        _Str &= vbCrLf & " where C.FNHSysCmpId =M.FNHSysCmpId"
        _Str &= vbCrLf & ""
        _Str &= vbCrLf & " ) C"
        _Str &= vbCrLf & " WHERE ISNULL(M.FTStateActive,'') ='1' "

        Me.ogcsect.DataSource = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)

        _Str = " SELECT  CASE WHEN S.FNHSysPermissionID IS NULL THEN '0' ELSE '1' END  AS FTSelect, C.FNHSysCmpId  "
        _Str &= vbCrLf & ",M.FNHSysUnitSectId"
        _Str &= vbCrLf & ",M.FTUnitSectCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & " , M.FTUnitSectNameTH AS FTUnitSectName "
        Else
            _Str &= vbCrLf & " , M.FTUnitSectNameEN AS FTUnitSectName "
        End If

        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS M WITH(NOLOCK) "
        _Str &= vbCrLf & " LEFT OUTER JOIN ("
        _Str &= vbCrLf & " SELECT FNHSysPermissionID,FNHSysUnitSectId  "
        _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeTypeUnitSect WITH(NOLOCK)"
        _Str &= vbCrLf & "   WHERE FNHSysPermissionID = " & Val(Me.HSysID) & ""
        _Str &= vbCrLf & " ) AS S ON M.FNHSysUnitSectId = S.FNHSysUnitSectId"
        _Str &= vbCrLf & " Outer Apply ( Select Top 1     FTCmpCode as  FNHSysCmpId  "
        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C "
        _Str &= vbCrLf & " where C.FNHSysCmpId =M.FNHSysCmpId"
        _Str &= vbCrLf & ""
        _Str &= vbCrLf & " ) C"
        _Str &= vbCrLf & " WHERE ISNULL(M.FTStateActive,'') ='1' "

        Me.ogcunitsect.DataSource = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)

    End Sub

    Private Sub LoadPermissionCmp()
        Dim _Str As String
        _Str = " SELECT  CASE WHEN S.FNHSysPermissionID IS NULL THEN '0' ELSE '1' END  AS FTSelect "
        _Str &= vbCrLf & ",M.FNHSysCmpId"
        _Str &= vbCrLf & ",M.FTCmpCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & " , M.FTCmpNameTH AS FTCmpName "
        Else
            _Str &= vbCrLf & " , M.FTCmpNameEN AS FTCmpName "
        End If

        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS M WITH(NOLOCK) "
        _Str &= vbCrLf & " LEFT OUTER JOIN ("
        _Str &= vbCrLf & " SELECT FNHSysPermissionID,FNHSysCmpId  "
        _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionCmp WITH(NOLOCK)"
        _Str &= vbCrLf & "   WHERE FNHSysPermissionID = " & Val(Me.HSysID) & ""
        _Str &= vbCrLf & " ) AS S ON M.FNHSysCmpId = S.FNHSysCmpId"
        _Str &= vbCrLf & " WHERE ISNULL(M.FTStateActive,'') ='1' "

        Me.ogccmp.DataSource = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)
    End Sub

    Private Sub Module_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            With CType(sender, DevExpress.XtraTreeList.TreeList)
                Dim _hifo As TreeListHitInfo = .CalcHitInfo(.PointToClient(Control.MousePosition))
                If (_hifo.Node IsNot Nothing) Then
                    With _hifo.Node

                        Dim Value1 As String = .GetValue(0).ToString
                        Dim Value2 As String = .GetValue(1).ToString
                        Dim Value3 As String = .GetValue(2).ToString
                        Dim Value4 As String = .GetValue(3).ToString
                        _LstDetail.Nodes.Clear()

                        Try
                            Dim _Spls As New HI.TL.SplashScreen("Loading...  Please Wait.")
                            Call LoadPermissionMenu(Me.HSysID, Val(Value1))
                            _Spls.Close()
                        Catch ex As Exception
                        End Try

                    End With
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub StateNodeChild(Node As DevExpress.XtraTreeList.Nodes.TreeListNode, _State As Boolean)
        If Node.HasChildren Then
            For Each _Node As DevExpress.XtraTreeList.Nodes.TreeListNode In Node.Nodes
                _Node.Checked = _State
                Call StateNodeChild(_Node, _State)
            Next
        End If
    End Sub

    Private Function CheckStateNodeChild(Node As DevExpress.XtraTreeList.Nodes.TreeListNode) As Boolean
        If Node.HasChildren Then
            For Each _Node As DevExpress.XtraTreeList.Nodes.TreeListNode In Node.Nodes
                If _Node.Checked Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function

    Private Sub StateNodeParent(Node As DevExpress.XtraTreeList.Nodes.TreeListNode, _State As Boolean)

        If Not (Node.ParentNode Is Nothing) Then
            Dim PNode As DevExpress.XtraTreeList.Nodes.TreeListNode = Node.ParentNode

            If _State = False Then
                'If CheckStateNodeChild(PNode) = False Then
                '    PNode.Checked = _State
                'End If
                Exit Sub
            Else
                PNode.Checked = _State
            End If

            Call StateNodeParent(PNode, _State)

        End If
    End Sub

    Private Sub _LstDetail_AfterCheckNode(sender As Object, e As DevExpress.XtraTreeList.NodeEventArgs) Handles _LstDetail.AfterCheckNode
        Static _Proc As Boolean

        If Not (_Proc) Then
            _Proc = True

            Call StateNodeChild(e.Node, e.Node.Checked)
            Call StateNodeParent(e.Node, e.Node.Checked)

            _Proc = False
        End If
    End Sub

    'Private Sub ogv_KeyDown(sender As Object, e As KeyEventArgs) Handles ogv.KeyDown, ogvsect.KeyDown, ogvunitsect.KeyDown
    '    Try
    '        Dim view As ColumnView = ogv
    '        view.ActiveFilter.Add(view.Columns("FNHSysCmpId"), New ColumnFilterInfo("[Company] Like '%" & e.KeyValue & "%'", ""))

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub RepositoryItemFNHSysCmpId_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemFNHSysCmpId.EditValueChanging
    '    Try
    '        If e.NewValue Then


    '        End If
    '        Dim view As ColumnView = ogvsect
    '        'view.FilteredColumnsCount
    '        Dim columnFilter As ColumnFilterInfo
    '        columnFilter = New ColumnFilterInfo(view.Columns("FNHSysCmpId").Caption & " LIKE  %" & HI.UL.ULF.rpQuoted(e.NewValue) & "%")
    '        view.Columns("FNHSysCmpId").FilterInfo = columnFilter
    '        Dim view2 As ColumnView = ogvunitsect
    '        'view2.ClearColumnsFilter()
    '        columnFilter = New ColumnFilterInfo(view2.Columns("FNHSysCmpId").Caption & " LIKE  %" & HI.UL.ULF.rpQuoted(e.NewValue) & "%")

    '        'view2.Columns("FNHSysCmpId").FilterInfo = columnFilter
    '        ogvunitsect.ClearColumnsFilter()
    '        ogvunitsect.Columns("FNHSysCmpId").FilterInfo = columnFilter
    '    Catch ex As Exception

    '    End Try
    'End Sub
End Class