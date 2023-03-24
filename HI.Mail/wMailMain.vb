Imports DevExpress.XtraTreeList
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns

Imports System.IO
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.Utils
Imports DevExpress.XtraRichEdit.API.Native
Imports DevExpress.Office.Utils

Imports HI.SE.RunID
Imports System.Text

Public Class wMailMain

    Private _SysPathImageSystem As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images\System"
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"


    Private _PathRFT As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images\Mail\Doc\"
    ' Private _PathRFT As String = "E:\HI SOFT SOURCE CODE_PEE\HI SOFT\HI\bin\Debug\Images\Mail\Doc\"

    Private imagemenuList As New System.Windows.Forms.ImageList
    Private imageListSub As New System.Windows.Forms.ImageList
    Private FoundImgPermission As Boolean = False

    Private _CountInbox_Read As String = String.Empty
    Private _CountInbox_UnRead As String = String.Empty
    Private _CountSent As String = String.Empty
    Private _CountDelete As String = String.Empty



    Private _MailType As String = MailType.Inbox

    Private _DTRefresh As New DataTable

    Private _OldRow As Integer = 0

    Private Shared _FTMailTo As String = String.Empty
    Private Shared _FTMailFrom As String = String.Empty

    Private Shared _FTMailSubject As String = String.Empty
    Private Shared _FNMailStateAttach As Integer = 0
    Private Shared _FNMailStatePriority As Integer = 0
    Private Shared _FNMailFileAttach As String = String.Empty
    Private Shared _FTMailText2 As String = String.Empty

    Private Shared _FTInsTime As String = String.Empty
    Private Shared _FDInsDate As String = String.Empty



    Private _sFilePath As String = String.Empty
    Private _sFileName As String = String.Empty
    Private _sPathFileName As String = String.Empty


    Private sPathServer As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images\Mail\Doc\"

    ' Private sPathServer As String = "\\Chainiwest\hi soft project\HI SOFT\HI SOFT\Images\Mail\Doc\"
    Private _nMailStateAttach As Integer = 0

    Private _sUsersystem As String = String.Empty


    Private _DTAttach As DataTable

    Private _DTAttach9 As DataTable

    Private _FTMailId_Old As Long
    Private _AppRcv As wApproveReceiveItem
    ' Public _FormState As statusform


    'Private _User As wUser
    'Private _Permission As wPermission


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call ConutTMAILMessages()

        Call CreateMain()

        Call LoadPic()

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name.ToString.Trim, Me)
        Catch ex As Exception
        Finally
        End Try

        _AppRcv = New wApproveReceiveItem
        HI.TL.HandlerControl.AddHandlerObj(_AppRcv)

        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AppRcv.Name.ToString.Trim, _AppRcv)
        Catch ex As Exception
        Finally
        End Try

        HI.TL.HandlerControl.AddHandlerObj(Me)
        HI.ST.Lang.SP_SETxLanguage(Me)



    End Sub


    Private Shared _FTMailId As Long
    Public Shared Property Data_FTMailId As Long
        Get
            Return _FTMailId
        End Get
        Set(value As Long)
            _FTMailId = value
        End Set
    End Property

    Private Shared _FormState As statusform
    Public Shared Property Data_FormState As statusform
        Get
            Return _FormState
        End Get
        Set(value As statusform)
            _FormState = value
        End Set
    End Property


    Public Shared Property Data_FTMailFrom As String
        Get
            Return _FTMailFrom
        End Get
        Set(value As String)
            _FTMailFrom = value
        End Set
    End Property

    Public Shared Property Data_FTMailTo As String
        Get
            Return _FTMailTo
        End Get
        Set(value As String)
            _FTMailTo = value
        End Set
    End Property


    Public Shared Property Data_FTMailSubject As String
        Get
            Return _FTMailSubject
        End Get
        Set(value As String)
            _FTMailSubject = value
        End Set
    End Property

    Public Shared Property Data_FNMailStateAttach As String
        Get
            Return _FNMailStateAttach
        End Get
        Set(value As String)
            _FNMailStateAttach = value
        End Set
    End Property

    Public Shared Property Data_FNMailFileAttach As String
        Get
            Return _FNMailFileAttach
        End Get
        Set(value As String)
            _FNMailFileAttach = value
        End Set
    End Property

    Public Shared Property Data_FNMailStatePriority As String
        Get
            Return _FNMailStatePriority
        End Get
        Set(value As String)
            _FNMailStatePriority = value
        End Set
    End Property

    Public Shared Property Data_FTMailText2 As String
        Get
            Return _FTMailText2
        End Get
        Set(value As String)
            _FTMailText2 = value
        End Set
    End Property

    Public Shared Property Data_FTInsTime As String
        Get
            Return _FTInsTime
        End Get
        Set(value As String)
            _FTInsTime = value
        End Set
    End Property


    Public Shared Property Data_FDInsDate As String
        Get
            Return _FDInsDate
        End Get
        Set(value As String)
            _FDInsDate = value
        End Set
    End Property

    Private _MailTypeInfo As Integer = -1
    Public Property MailTypeInfo As Integer
        Get
            Return _MailTypeInfo
        End Get
        Set(value As Integer)
            _MailTypeInfo = value
        End Set
    End Property

    Public Enum statusform
        Fnew  '0
        Fedit '1
        FLoad  '2
        FReply  '3
        FReplyALL '4
        FForword  '5
    End Enum

    Public Enum MailType
        Inbox
        Sent
        Delete
    End Enum

    Enum ListType As Integer
        _Node = 0
        _Module = 1
        _Menu = 2
        _Form = 3
        _Object = 4
        _User = 5
        _UserPermission = 6
        _UserPermissionModule = 7
        _Permission = 8
        _PermissionModule = 9
        _PermissionModuleMenu = 10
    End Enum


    Private Sub wMailMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        _FormState = statusform.FLoad

        PclShowMail.Visible = False

        Call CreateTabel_Attach9()

        ocmexit2.Text = ocmexit.Text

        Timer1.Enabled = True
        ' OtmCheckLanguage.Enabled = True


        Call LoadogcTMAILMessages(MailType.Inbox)

        If _CountInbox_Read <> 0 Then
            Call FirstLoad()
        Else
            Exit Sub
        End If

        recMessage.ReadOnly = True

        With ogvTMAILFileAttach
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        With ogvTMAILMessages
            .OptionsView.ShowAutoFilterRow = True
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        With ogvTMAILFileAttach9
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With


    End Sub


#Region "Menu"

    Public Function CreateMain() As Boolean
        Try


            Dim _NodeID As Long = 1

            Dim _Str As String = ""
            Dim _Dt As DataTable
            Dim tPathImgDis As String

            Call InitTree()
            _Lst.ClearNodes()


            Dim _lstBlen As New DevExpress.XtraTreeList.Blending.XtraTreeListBlending
            _lstBlen.TreeListControl = _Lst

            _Str = " SELECT  FNSeq, FNListName"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Str &= vbCrLf & " , FTNameTH AS  FTName"
            Else
                _Str &= vbCrLf & " , FTNameEN AS  FTName"
            End If

            _Str &= vbCrLf & " , FTImmageName"
            _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].dbo.TMAILListData AS A WITH(NOLOCK)"
            _Str &= vbCrLf & " Order BY FNSeq"

            _Dt = HI.Conn.SQLConn.GetDataTable(_Str, HI.Conn.DB.DataBaseName.DB_MAIL)
            _Lst.BeginUnboundLoad()

            Dim TempCount As String = String.Empty

            For Each R As DataRow In _Dt.Rows

                Dim mnode As DevExpress.XtraTreeList.Nodes.TreeListNode

                Select Case R!FNSeq.ToString
                    Case "1"


                        If _CountInbox_UnRead = 0 Then
                            TempCount = "(" & _CountInbox_Read & ")"
                        Else
                            TempCount = "(" & _CountInbox_Read & ")(" & _CountInbox_UnRead & ")"
                        End If

                    Case "2" : TempCount = "(" & _CountSent & ")"
                    Case "3" : TempCount = "(" & _CountDelete & ")"
                End Select

                mnode = AddTreeListNode(Nothing, R!FNSeq.ToString, R!FTName.ToString & TempCount, "", ListType._Node, R!FTName.ToString)

                If R!FTImmageName.ToString.Trim <> "" Then
                    tPathImgDis = _SystemFilePath & "\Mail\" & R!FTImmageName.ToString.Trim
                    If IO.File.Exists(tPathImgDis) Then
                        imagemenuList.Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
                        mnode.StateImageIndex = imagemenuList.Images.Count - 1
                    End If
                End If


                FTMailSubject_Lbl_2.Text = "XXXX"
                FTMailDate_Lbl_2.Text = "DD/MM/YYYY"
                FTMailTo_Lbl_2.Text = "XXXX"

                Select Case R!FNSeq
                    Case 1
                        mnode.HasChildren = False  ' แสดงทีแรกไม่โชว์ SubTree แตก ( + )
                    Case 2
                        mnode.HasChildren = False
                    Case 3
                        mnode.HasChildren = False
                End Select

            Next
            _Lst.EndUnboundLoad()

            AddHandler _Lst.Click, AddressOf Menu_Click

            _Dt.Dispose()
            Return True

        Catch ex As Exception
            ' MsgBox(ex.Message)
            Return True
        End Try
    End Function

#End Region

#Region "Innit Treelist"


    Private Function AddTreeListNode(ByVal _Node As DevExpress.XtraTreeList.Nodes.TreeListNode, _
                                Value1 As String, Value2 As String, Value3 As String, Value4 As ListType, Value5 As String) As DevExpress.XtraTreeList.Nodes.TreeListNode

        Return _Lst.AppendNode(New Object() {Value1, Value2, Value3, Value4, Value5}, _Node)

    End Function


    Private Sub InitTree()
        With _Lst

            .Columns.Add() : .Columns.Add()
            .Columns.Add() : .Columns.Add()
            .Columns.Add()
            With .Columns.Item(0)
                .Name = "ColKey"
                .Caption = "Menu Name"
                .FieldName = "FTMnuName"
                .Visible = False
            End With

            With .Columns.Item(1)
                .Name = "FTName"
                .Caption = "FTName"
                .FieldName = "FTName"
                .Visible = True
            End With

            With .Columns.Item(2)
                .Name = "Object"
                .Caption = "Object"
                .FieldName = "Object"
                .Visible = False
            End With

            With .Columns.Item(3)
                .Name = "FTType"
                .Caption = "FTType"
                .FieldName = "FTType"
                .Visible = False
            End With

            With .Columns.Item(4)
                .Name = "ObjectDesc"
                .Caption = "ObjectDesc"
                .FieldName = "ObjectDesc"
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


#End Region

#Region "Function "

    Private Sub Menu_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            With CType(sender, DevExpress.XtraTreeList.TreeList)
                Dim _hifo As TreeListHitInfo = .CalcHitInfo(.PointToClient(Control.MousePosition))
                If (_hifo.Node IsNot Nothing) Then
                    With _hifo.Node

                        Dim Value1 As String = .GetValue(0).ToString
                        Dim Value2 As String = .GetValue(1).ToString
                        Dim Value3 As String = .GetValue(2).ToString
                        Dim Value4 As String = .GetValue(3).ToString
                        Dim Value5 As String = .GetValue(4).ToString


                        Select Case Value1.ToUpper
                            Case "1".ToUpper  '0
                                '        Call SetButton(MenuType.Permission)
                                '        Call LoadPermission()

                                Call LoadogcTMAILMessages(MailType.Inbox)
                                If _CountInbox_Read = "0" Then
                                    recMessage.Text = ""
                                    Me.FTMailSubject_Lbl_2.Text = "XXXX"
                                    Me.FTMailDate_Lbl_2.Text = "DD/MM/YYYY"
                                    Me.FTMailTo_Lbl_2.Text = "XXXX"
                                    ogcTMAILFileAttach.DataSource = Nothing
                                End If

                                _MailType = MailType.Inbox


                            Case "2".ToUpper  '1
                                '        Call SetButton(MenuType.User)
                                '        Call LoadUser()
                                Call LoadogcTMAILMessages(MailType.Sent)

                                If _CountSent = "0" Then
                                    recMessage.Text = ""
                                    Me.FTMailSubject_Lbl_2.Text = "XXXX"
                                    Me.FTMailDate_Lbl_2.Text = "DD/MM/YYYY"
                                    Me.FTMailTo_Lbl_2.Text = "XXXX"
                                    ogcTMAILFileAttach.DataSource = Nothing
                                End If
                                _MailType = MailType.Sent

                            Case "3".ToUpper  '2
                                Call LoadogcTMAILMessages(MailType.Delete)

                                If _CountDelete = "0" Then
                                    recMessage.Text = ""
                                    Me.FTMailSubject_Lbl_2.Text = "XXXX"
                                    Me.FTMailDate_Lbl_2.Text = "DD/MM/YYYY"
                                    Me.FTMailTo_Lbl_2.Text = "XXXX"
                                    ogcTMAILFileAttach.DataSource = Nothing
                                End If
                                _MailType = MailType.Delete



                        End Select
                    End With
                End If
            End With

            ogvTMAILMessages_Click(sender, e)

            'ogvTMAILMessages_MouseDown(sender, e)

        Catch ex As Exception

        End Try
    End Sub


    Private Sub ClearText()
        recMessage.Text = ""
        Me.FTMailSubject_Lbl_2.Text = "XXXX"
        Me.FTMailDate_Lbl_2.Text = "DD/MM/YYYY"
        Me.FTMailTo_Lbl_2.Text = "XXXX"
        ' ogcTMAILFileAttach.DataSource = Nothing

    End Sub

    Private Sub LoadPic()
        Dim tPathImgDis As String = ""
        With ImageList1
            .Images.Clear()

            tPathImgDis = _SystemFilePath & "\Mail\Read.png"  '0
            If IO.File.Exists(tPathImgDis) Then
                .Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
            End If

            tPathImgDis = _SystemFilePath & "\Mail\UnRead.png" '1
            If IO.File.Exists(tPathImgDis) Then
                .Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
            End If

            tPathImgDis = _SystemFilePath & "\Mail\priority2.png" '2
            If IO.File.Exists(tPathImgDis) Then
                .Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
            Else
                tPathImgDis = _SystemFilePath & "\Mail\Bank.png" ' 6
                If IO.File.Exists(tPathImgDis) Then
                    .Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
                End If
            End If

            tPathImgDis = _SystemFilePath & "\Mail\priority3.png" '3
            If IO.File.Exists(tPathImgDis) Then
                .Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
            Else
                tPathImgDis = _SystemFilePath & "\Mail\Bank.png" ' 6
                If IO.File.Exists(tPathImgDis) Then
                    .Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
                End If
            End If

            tPathImgDis = _SystemFilePath & "\Mail\priority3.png" '4
            If IO.File.Exists(tPathImgDis) Then
                .Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
            Else
                tPathImgDis = _SystemFilePath & "\Mail\Bank.png" ' 6
                If IO.File.Exists(tPathImgDis) Then
                    .Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
                End If
            End If

            tPathImgDis = _SystemFilePath & "\Mail\Att2.png" '5
            If IO.File.Exists(tPathImgDis) Then
                .Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
            End If
            tPathImgDis = _SystemFilePath & "\Mail\Close1.png" ' 6 "CloseMail2.png"
            If IO.File.Exists(tPathImgDis) Then
                .Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
            End If
            tPathImgDis = _SystemFilePath & "\Mail\Open1.png" ' 7 OpenMail2.png"
            If IO.File.Exists(tPathImgDis) Then
                .Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
            End If

            tPathImgDis = _SystemFilePath & "\Mail\Bank.png" ' 8
            If IO.File.Exists(tPathImgDis) Then
                .Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
            End If


            tPathImgDis = _SystemFilePath & "\Mail\Unknown-user.png" ' 9
            If IO.File.Exists(tPathImgDis) Then
                PicMail.Image = HI.UL.ULImage.LoadImage(_SystemFilePath & "\Mail\Unknown-user.png")
            End If

            tPathImgDis = _SystemFilePath & "\Mail\FuncSendMail.png" ' 10
            If IO.File.Exists(tPathImgDis) Then
                PicMail_2.Image = HI.UL.ULImage.LoadImage(_SystemFilePath & "\Mail\FuncSendMail.png")
            End If

        End With

    End Sub

    Private Sub SetDataGrid()
        ogvTMAILMessages.Columns.ColumnByFieldName("FTSelect").Width = 30
        ogvTMAILMessages.Columns.ColumnByName("ColImageAttach").Width = 30
        ogvTMAILMessages.Columns.ColumnByName("ColImagePriority").Width = 30
        ogvTMAILMessages.Columns.ColumnByName("ColImageOpen").Width = 30
        ogvTMAILMessages.Columns.ColumnByName("ColFTMailFrom").Width = 80
        ogvTMAILMessages.Columns.ColumnByName("ColFTMailDate").Width = 80
        ogvTMAILMessages.Columns.ColumnByName("ColFTMailSubject").Width = 266

    End Sub


    Private Sub SetDataGrid2()

        ogvTMAILFileAttach.Columns.ColumnByName("FTMailId").Width = 30
        ogvTMAILFileAttach.Columns.ColumnByName("FNMailFileAttach").Width = 80


    End Sub

    Private Sub LoadogcTMAILMessages(ByVal TempData As String, Optional _FTMailId As String = "", Optional ReloadMail As Boolean = True)
        Try
            Dim _str As String = String.Empty
            Dim _dt As New DataTable
            Dim _condition As String = String.Empty

            Dim _FocusRowInDex As Integer = -1
            Dim _dtOld As DataTable

            If Not (ReloadMail) Then
                If Not (Me.ogcTMAILMessages.DataSource Is Nothing) Then
                    With CType(Me.ogcTMAILMessages.DataSource, DataTable)
                        .AcceptChanges()
                        _dtOld = .Copy
                    End With
                Else
                    Me.FTChkStateSelect.Checked = False
                    _dtOld = Nothing
                End If
            Else
                Me.FTChkStateSelect.Checked = False
                _dtOld = Nothing
            End If

            Select Case TempData
                Case 0

                    _condition = " WHERE (FTMailTo = '" & HI.ST.UserInfo.UserName & "'  AND (isnull(FNMailStateSend,0) = 1 ) AND isnull(FNMailStateDelete,0) <> 1) AND FNMailStateType = 1"

                    _str = " SELECT '0' AS FTSelect,FTInsUser,FTUpdUser,FDUpdDate,FTUpdTime,FTMailId,"
                    ' _str &= "isnull(FTMailDate,'') as FTMailDate," FDInsDate  FTInsTime
                    '_str &= "SUBSTRING(FTMailDate,9,2) + '/'+ SUBSTRING(FTMailDate,6,2) + '/' + SUBSTRING(FTMailDate,1,4) as FTMailDate,"
                    _str &= "  CASE WHEN ISDATE(FTMailDate) = 1 THEN  Convert(datetime,FTMailDate)  ELSE NULL END AS FTMailDate,"
                    _str &= "isnull(FTMailFrom,'') as FTMailFrom,"
                    _str &= "isnull(FTMailTo,'') as FTMailTo,"
                    _str &= "isnull(FTMailSubject,'') as FTMailSubject,"
                    _str &= "FTMailText,"
                    _str &= "isnull(FTMailStateOpen,0) as FTMailStateOpen,"
                    _str &= "isnull(FDMailOpenDate,'') as FDMailOpenDate,"
                    _str &= "isnull(FTMailOpenTime,'') as FTMailOpenTime,"
                    _str &= "isnull(FNMailStateAttach,0) as FNMailStateAttach,"
                    _str &= "isnull(FNMailStatePriority,0) as FNMailStatePriority,"
                    _str &= "isnull(FNMailStateSend,0) as FNMailStateSend,"
                    _str &= "isnull(FNMailStateJobStatus,0) as FNMailStateJobStatus,"
                    _str &= "isnull(FNMailFileAttach,'') as FNMailFileAttach,"
                    _str &= "isnull(FDInsDate,'') as FDInsDate,"
                    _str &= "isnull(FTInsTime,'') as FTInsTime,"
                    _str &= "isnull(FTMailFromTemp,'') as FTMailFromTemp,"
                    _str &= "isnull(FNMailStateDelete,0) as FNMailStateDelete"
                    _str &= vbCrLf & ",ISNULL(FNMailTypeInfo,-1) AS FNMailTypeInfo "
                    _str &= vbCrLf & ",ISNULL(FTMailInfoRef,'') AS FTMailInfoRef "
                    _str &= vbCrLf & ",ISNULL(Z.FTCallMnuName,'') AS FTCallMnuName "
                    _str &= vbCrLf & ",ISNULL(Z.FTCallMethodName,'') AS FTCallMethodName "
                    _str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].dbo.TMAILMessages AS A WITH(NOLOCK)"
                    _str &= vbCrLf & " LEFT OUTER JOIN (SELECT FNListIndex, FTCallMnuName, FTCallMethodName FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)  WHERE  (FTListName = N'FNMailTypeInfo')) AS Z "
                    _str &= vbCrLf & " ON A.FNMailTypeInfo = Z.FNListIndex"
                    _str &= vbCrLf & _condition
                    _str &= vbCrLf & " Order BY FDInsDate desc,FTMailId desc"

                Case 1

                    _condition = " WHERE (FTMailTo = '" & HI.ST.UserInfo.UserName & "' AND FTMailFromTemp =  '" & HI.ST.UserInfo.UserName & "' " & _
                                 " and isnull(FNMailStateSend,0) = 0  AND isnull(FNMailStateDelete,0) <> 1 ) " & _
                                 " OR ( FTMailFromTemp = '" & HI.ST.UserInfo.UserName & "' AND FTMailTo <> '" & HI.ST.UserInfo.UserName & "' " & _
                                 " and isnull(FNMailStateSend,0) = 1  AND isnull(FNMailStateDelete,0) <> 1 ) AND FNMailStateType = 0 "

                    _str = " SELECT '0' AS FTSelect,FTInsUser,FTUpdUser,FDUpdDate,FTUpdTime,FTMailId,"
                    ' _str &= "isnull(FTMailDate,'') as FTMailDate," FDInsDate  FTInsTime
                    '_str &= "SUBSTRING(FTMailDate,9,2) + '/'+ SUBSTRING(FTMailDate,6,2) + '/' + SUBSTRING(FTMailDate,1,4) as FTMailDate,"
                    _str &= "  CASE WHEN ISDATE(FTMailDate) = 1 THEN  Convert(datetime,FTMailDate)  ELSE NULL END AS FTMailDate,"
                    _str &= "isnull(FTMailTo,'') as FTMailFrom,"
                    _str &= "isnull(FTMailTo,'') as FTMailTo,"
                    _str &= "isnull(FTMailSubject,'') as FTMailSubject,"
                    _str &= "FTMailText,"
                    _str &= "isnull(FTMailStateOpen,0) as FTMailStateOpen,"
                    _str &= "isnull(FDMailOpenDate,'') as FDMailOpenDate,"
                    _str &= "isnull(FTMailOpenTime,'') as FTMailOpenTime,"
                    _str &= "isnull(FNMailStateAttach,0) as FNMailStateAttach,"
                    _str &= "isnull(FNMailStatePriority,0) as FNMailStatePriority,"
                    _str &= "isnull(FNMailStateSend,0) as FNMailStateSend,"
                    _str &= "isnull(FNMailStateJobStatus,0) as FNMailStateJobStatus,"
                    _str &= "isnull(FNMailFileAttach,'') as FNMailFileAttach,"
                    _str &= "isnull(FTInsTime,'') as FTInsTime,"
                    _str &= "isnull(FDInsDate,'') as FDInsDate,"
                    _str &= "isnull(FTMailFromTemp,'') as FTMailFromTemp,"
                    _str &= "isnull(FNMailStateDelete,0) as FNMailStateDelete"
                    _str &= vbCrLf & ",ISNULL(FNMailTypeInfo,-1) AS FNMailTypeInfo "
                    _str &= vbCrLf & ",ISNULL(FTMailInfoRef,'') AS FTMailInfoRef "
                    _str &= vbCrLf & ",ISNULL(Z.FTCallMnuName,'') AS FTCallMnuName "
                    _str &= vbCrLf & ",ISNULL(Z.FTCallMethodName,'') AS FTCallMethodName "
                    _str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].dbo.TMAILMessages AS A WITH(NOLOCK)"
                    _str &= vbCrLf & " LEFT OUTER JOIN (SELECT FNListIndex, FTCallMnuName, FTCallMethodName FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)  WHERE  (FTListName = N'FNMailTypeInfo')) AS Z "
                    _str &= vbCrLf & " ON A.FNMailTypeInfo = Z.FNListIndex"
                    _str &= vbCrLf & _condition
                    _str &= vbCrLf & " Order BY FDInsDate desc,FTMailId desc"

                Case 2
                    _condition = " WHERE (FTMailTo = '" & HI.ST.UserInfo.UserName & "' AND isnull(FNMailStateDelete,0) = 1 AND FNMailStateType = 1) " & _
                                   " OR (FTMailFromTemp = '" & HI.ST.UserInfo.UserName & "' AND isnull(FNMailStateDelete,0) = 1 AND FNMailStateType = 0)"

                    _str = " SELECT '0' AS FTSelect,FTInsUser,FTUpdUser,FDUpdDate,FTUpdTime,FTMailId,"
                    ' _str &= "isnull(FTMailDate,'') as FTMailDate," FDInsDate FTInsTime
                    '_str &= "SUBSTRING(FTMailDate,9,2) + '/'+ SUBSTRING(FTMailDate,6,2) + '/' + SUBSTRING(FTMailDate,1,4) as FTMailDate,"
                    _str &= "  CASE WHEN ISDATE(FTMailDate) = 1 THEN  Convert(datetime,FTMailDate)  ELSE NULL END AS FTMailDate,"
                    _str &= "isnull(FTMailTo,'') as FTMailFrom,"
                    _str &= "isnull(FTMailTo,'') as FTMailTo,"
                    _str &= "isnull(FTMailSubject,'') as FTMailSubject,"
                    _str &= "FTMailText,"
                    _str &= "isnull(FTMailStateOpen,0) as FTMailStateOpen,"
                    _str &= "isnull(FDMailOpenDate,'') as FDMailOpenDate,"
                    _str &= "isnull(FTMailOpenTime,'') as FTMailOpenTime,"
                    _str &= "isnull(FNMailStateAttach,0) as FNMailStateAttach,"
                    _str &= "isnull(FNMailStatePriority,0) as FNMailStatePriority,"
                    _str &= "isnull(FNMailStateSend,0) as FNMailStateSend,"
                    _str &= "isnull(FNMailStateJobStatus,0) as FNMailStateJobStatus,"
                    _str &= "isnull(FNMailFileAttach,'') as FNMailFileAttach,"
                    _str &= "isnull(FTInsTime,'') as FTInsTime,"
                    _str &= "isnull(FDInsDate,'') as FDInsDate,"
                    _str &= "isnull(FTMailFromTemp,'') as FTMailFromTemp,"
                    _str &= "isnull(FNMailStateDelete,0) as FNMailStateDelete"
                    _str &= vbCrLf & ",ISNULL(FNMailTypeInfo,-1) AS FNMailTypeInfo "
                    _str &= vbCrLf & ",ISNULL(FTMailInfoRef,'') AS FTMailInfoRef "
                    _str &= vbCrLf & ",ISNULL(Z.FTCallMnuName,'') AS FTCallMnuName "
                    _str &= vbCrLf & ",ISNULL(Z.FTCallMethodName,'') AS FTCallMethodName "
                    _str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].dbo.TMAILMessages AS A WITH(NOLOCK)"
                    _str &= vbCrLf & " LEFT OUTER JOIN (SELECT FNListIndex, FTCallMnuName, FTCallMethodName FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)  WHERE  (FTListName = N'FNMailTypeInfo')) AS Z "
                    _str &= vbCrLf & " ON A.FNMailTypeInfo = Z.FNListIndex"
                    _str &= vbCrLf & _condition
                    _str &= vbCrLf & " Order BY FDInsDate desc,FTMailId desc"


            End Select

            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_MAIL)

            _dt.Columns.Add("FTImageStatus", GetType(Object))
            _dt.Columns.Add("FTImageOpen", GetType(Object))
            _dt.Columns.Add("FTImagePriority", GetType(Object))
            _dt.Columns.Add("FTImageAttach", GetType(Object))

            For Each R As DataRow In _dt.Rows

                If Not (_dtOld Is Nothing) Then
                    If _dtOld.Select("FTMailId=" & Long.Parse(Val(R!FTMailId.ToString)) & " AND FTSelect='1'").Length > 0 Then
                        R!FTSelect = "1"
                    End If
                End If
                Select Case Val(R!FNMailStateSend)  'FNMailStatePriority
                    Case 0
                        R!FTImageStatus = ImageList1.Images(7)
                    Case 1
                        R!FTImageStatus = ImageList1.Images(6)
                End Select

                If TempData <> 0 Then
                    R!FTImageOpen = ImageList1.Images(0)
                Else
                    Select Case Val(R!FTMailStateOpen)
                        Case 0 ' UnRead
                            R!FTImageOpen = ImageList1.Images(1)
                        Case 1  ' Read
                            R!FTImageOpen = ImageList1.Images(0)
                    End Select

                End If


                Select Case Val(R!FNMailStatePriority)
                    Case 0
                        R!FTImagePriority = ImageList1.Images(2)
                    Case 1
                        R!FTImagePriority = ImageList1.Images(3)
                    Case 2 ' 
                        R!FTImagePriority = ImageList1.Images(4)
                End Select


                Select Case Val(R!FNMailStateAttach)
                    Case 0
                        R!FTImageAttach = ImageList1.Images(8)
                    Case 1
                        R!FTImageAttach = ImageList1.Images(5)
                End Select


            Next

            Me.ogcTMAILMessages.DataSource = _dt
            Dim view As GridView
            view = ogcTMAILMessages.Views(0)
            view.OptionsView.ShowAutoFilterRow = True
            view.BestFitColumns()

            Me.ogcTMAILMessages = view.GridControl
            Me.ogcTMAILMessages.Refresh()

            Call SetDataGrid()

            _dt.Dispose()


        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub LoadogcTMAILFileAttach(ByVal TempData As String)
        Try
            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            _str = " SELECT FTMailId,"
            _str &= "isnull(FNMailFileAttach,'') as FNMailFileAttach "
            _str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].dbo.TMAILFileAttach AS A WITH(NOLOCK)"
            _str &= " Where FTMailId = " & TempData

            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_MAIL)

            Me.ogcTMAILFileAttach.DataSource = _dt.Copy
            ogvTMAILFileAttach.OptionsView.ShowAutoFilterRow = False
            ogvTMAILFileAttach.BestFitColumns()

            ' If _dt.Rows.Count > 0 Then
            'Dim view As GridView
            'view = ogcTMAILFileAttach.Views(0)

            'ogcTMAILFileAttach.Visible = True
            'LabelControl1.Visible = True
            'Me.ogcTMAILFileAttach = view.GridControl
            'Me.ogcTMAILFileAttach.Refresh()

            '_dt.Dispose()
            ' Else
            ' Me.ogcTMAILFileAttach.DataSource = Nothing
            'Dim view As GridView
            'view = ogcTMAILFileAttach.Views(0)
            'LabelControl1.Visible = False
            'ogcTMAILFileAttach.Visible = False
            'Me.ogcTMAILFileAttach = view.GridControl
            'Me.ogcTMAILFileAttach.Refresh()

            '  _dt.Dispose()

            ' End If
            _dt.Dispose()
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub LoadogcTMAILFileAttach_Sent(ByVal TempData As String)
        Try
            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            _str = " SELECT FTMailId,"
            _str &= "isnull(FNMailFileAttach,'') as FNMailFileAttach "
            _str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].dbo.TMAILFileAttach AS A WITH(NOLOCK)"
            _str &= " Where FTMailId = " & TempData

            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_MAIL)

            Me.ogcTMAILFileAttach9.DataSource = _dt

            If _dt.Rows.Count > 0 Then
                Dim view As GridView
                view = ogcTMAILFileAttach9.Views(0)
                view.OptionsView.ShowAutoFilterRow = True
                view.BestFitColumns()

                Me.ogcTMAILFileAttach9 = view.GridControl
                Me.ogcTMAILFileAttach9.Refresh()

                _dt.Dispose()
            Else
                Me.ogcTMAILFileAttach9.DataSource = Nothing
                Dim view As GridView
                view = ogcTMAILFileAttach9.Views(0)
                view.OptionsView.ShowAutoFilterRow = True
                view.BestFitColumns()

                Me.ogcTMAILFileAttach9 = view.GridControl
                Me.ogcTMAILFileAttach9.Refresh()

                _dt.Dispose()


            End If

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub ConutTMAILMessages()

        Try

            Dim _str As String = String.Empty
            Dim _dt As New DataTable


            _CountInbox_Read = 0
            _CountInbox_UnRead = 0
            _CountSent = 0
            _CountDelete = 0

            '_str = "Select  SUM(CASE WHEN (FTMailTo =  '" & HI.ST.UserInfo.UserName & "' AND FTMailFromTemp =  '" & HI.ST.UserInfo.UserName & "'"
            '_str &= Environment.NewLine & " AND (isnull(FNMailStateSend,0) = 1 ) AND isnull(FTMailStateOpen,0) = 1 AND isnull(FNMailStateDelete,0) <> 1 AND FNMailStateType = 1 )"
            '_str &= Environment.NewLine & " OR (FTMailTo =  '" & HI.ST.UserInfo.UserName & "' AND FTMailFromTemp <>  '" & HI.ST.UserInfo.UserName & "'"
            '_str &= Environment.NewLine & "  AND (isnull(FNMailStateSend,0) = 1 ) AND isnull(FTMailStateOpen,0) = 1 AND isnull(FNMailStateDelete,0) <> 1 AND FNMailStateType = 1 ) THEN 1 ELSe 0 END) AS CountInbox_Read,"
            '_str &= Environment.NewLine & " SUM(CASE WHEN (FTMailTo =  '" & HI.ST.UserInfo.UserName & "' AND FTMailFromTemp =  '" & HI.ST.UserInfo.UserName & "' "
            '_str &= Environment.NewLine & " AND (isnull(FNMailStateSend,0) = 1 ) AND isnull(FTMailStateOpen,0) = 0 AND isnull(FNMailStateDelete,0) <> 1 AND FNMailStateType = 1) "
            '_str &= Environment.NewLine & "  OR (FTMailTo =  '" & HI.ST.UserInfo.UserName & "' AND FTMailFromTemp <>  '" & HI.ST.UserInfo.UserName & "'"
            '_str &= Environment.NewLine & " AND (isnull(FNMailStateSend,0) = 1 ) AND isnull(FTMailStateOpen,0) = 0 ) AND FNMailStateType = 1 THEN 1 ELSe 0 END) AS CountInbox_UnRead, "
            '_str &= Environment.NewLine & " SUM(CASE WHEN (FTMailTo =   '" & HI.ST.UserInfo.UserName & "' AND FTMailFromTemp =  '" & HI.ST.UserInfo.UserName & "'"
            '_str &= Environment.NewLine & " and isnull(FNMailStateSend,0) = 0  AND isnull(FNMailStateDelete,0) <> 1 AND FNMailStateType = 0 )"
            '_str &= Environment.NewLine & " OR ( FTMailFromTemp =  '" & HI.ST.UserInfo.UserName & "' AND FTMailTo <>   '" & HI.ST.UserInfo.UserName & "'"
            '_str &= Environment.NewLine & " and isnull(FNMailStateSend,0) = 1  AND isnull(FNMailStateDelete,0) <> 1  )  THEN 1 ELSe 0 END) AS CountSent, "
            '_str &= Environment.NewLine & " SUM(CASE WHEN  isnull(FNMailStateDelete,0) = 1 " 'AND FNMailStateType = 0 
            '_str &= Environment.NewLine & " AND (FTMailTo = '" & HI.ST.UserInfo.UserName & "' or FTMailFromTemp = '" & HI.ST.UserInfo.UserName & "')"
            '_str &= Environment.NewLine & " THEN 1 ELSe 0 END) AS CountDelete "
            '' _str &= Environment.NewLine & " FROM [HITECH_MAIL].dbo.TMAILMessages AS A WITH(NOLOCK)"
            '_str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MAIL) & "].dbo.TMAILMessages AS A WITH(NOLOCK)"
            '' _str &= Environment.NewLine &  WHERE (FTMailTo = '" & HI.ST.UserInfo.UserName & "' or FTMailFromTemp = '" & HI.ST.UserInfo.UserName & "')" 


            _str = "Select  SUM(CASE WHEN (FTMailTo =  '" & HI.ST.UserInfo.UserName & "' AND FTMailFromTemp =  '" & HI.ST.UserInfo.UserName & "'"
            _str &= Environment.NewLine & " AND (isnull(FNMailStateSend,0) = 1 ) AND isnull(FTMailStateOpen,0) = 1 AND isnull(FNMailStateDelete,0) <> 1 AND FNMailStateType = 1 )"
            _str &= Environment.NewLine & " OR (FTMailTo =  '" & HI.ST.UserInfo.UserName & "' AND FTMailFromTemp <>  '" & HI.ST.UserInfo.UserName & "'"
            _str &= Environment.NewLine & "  AND (isnull(FNMailStateSend,0) = 1 ) AND isnull(FTMailStateOpen,0) = 1 AND isnull(FNMailStateDelete,0) <> 1 AND FNMailStateType = 1 ) THEN 1 ELSe 0 END) AS CountInbox_Read,"
            _str &= Environment.NewLine & " SUM(CASE WHEN (FTMailTo =  '" & HI.ST.UserInfo.UserName & "' AND FTMailFromTemp =  '" & HI.ST.UserInfo.UserName & "' "
            _str &= Environment.NewLine & " AND (isnull(FNMailStateSend,0) = 1 ) AND isnull(FTMailStateOpen,0) = 0 AND isnull(FNMailStateDelete,0) <> 1 AND FNMailStateType = 1) "
            _str &= Environment.NewLine & "  OR (FTMailTo =  '" & HI.ST.UserInfo.UserName & "' AND FTMailFromTemp <>  '" & HI.ST.UserInfo.UserName & "'"
            _str &= Environment.NewLine & " AND (isnull(FNMailStateSend,0) = 1 ) AND isnull(FTMailStateOpen,0) = 0 ) AND FNMailStateType = 1 THEN 1 ELSe 0 END) AS CountInbox_UnRead, "
            _str &= Environment.NewLine & " SUM(CASE WHEN (FTMailTo =   '" & HI.ST.UserInfo.UserName & "' AND FTMailFromTemp = '" & HI.ST.UserInfo.UserName & "'"
            _str &= Environment.NewLine & " and isnull(FNMailStateSend,0) = 0  AND isnull(FNMailStateDelete,0) <> 1 AND FNMailStateType = 0 )"
            _str &= Environment.NewLine & " OR ( FTMailFromTemp =  '" & HI.ST.UserInfo.UserName & "' AND FTMailTo <>   '" & HI.ST.UserInfo.UserName & "'"
            _str &= Environment.NewLine & " and isnull(FNMailStateSend,0) = 1  AND isnull(FNMailStateDelete,0) <> 1 AND FNMailStateType = 0 ) "
            _str &= Environment.NewLine & " OR ( FTMailFromTemp =  '" & HI.ST.UserInfo.UserName & "' AND FTMailTo = '" & HI.ST.UserInfo.UserName & "'"
            _str &= Environment.NewLine & " and isnull(FNMailStateSend,0) = 0  AND isnull(FNMailStateDelete,0) <> 1  ) "
            _str &= Environment.NewLine & " THEN 1 ELSe 0 END) AS CountSent, "
            _str &= Environment.NewLine & " SUM(CASE WHEN  (isnull(FNMailStateDelete,0) = 1 AND FTMailTo = '" & HI.ST.UserInfo.UserName & "' AND FNMailStateType = 1 ) "
            _str &= Environment.NewLine & " OR (isnull(FNMailStateDelete,0) = 1 AND FTMailFromTemp = '" & HI.ST.UserInfo.UserName & "' AND FNMailStateType = 0 ) "
            _str &= Environment.NewLine & " THEN 1 ELSe 0 END) AS CountDelete "
            ' _str &= Environment.NewLine & " FROM [HITECH_MAIL].dbo.TMAILMessages AS A WITH(NOLOCK)"
            _str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].dbo.TMAILMessages AS A WITH(NOLOCK)"
            ' _str &= Environment.NewLine &  WHERE (FTMailTo = '" & HI.ST.UserInfo.UserName & "' or FTMailFromTemp = '" & HI.ST.UserInfo.UserName & "')" 


            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_MAIL)
            For Each R As DataRow In _dt.Rows

                If R!CountInbox_Read.ToString() = "0" Or R!CountInbox_Read.ToString() = "" Then
                    _CountInbox_Read = "0"
                Else
                    _CountInbox_Read = R!CountInbox_Read.ToString()
                End If

                If R!CountInbox_UnRead.ToString() = "0" Or R!CountInbox_UnRead.ToString() = "" Then
                    _CountInbox_UnRead = "0"
                Else
                    _CountInbox_UnRead = R!CountInbox_UnRead.ToString()
                End If

                If R!CountSent.ToString() = "0" Or R!CountSent.ToString() = "" Then
                    _CountSent = "0"
                Else
                    _CountSent = R!CountSent.ToString()
                End If

                If R!CountDelete.ToString() = "0" Or R!CountDelete.ToString() = "" Then
                    _CountDelete = "0"
                Else
                    _CountDelete = R!CountDelete.ToString()
                End If


                Exit For
            Next

            ' Me.ogcTMAILMessages.DataSource = _dt

            _dt.Dispose()

            ' HI.ST.Lang.SP_SETxLanguage(Me)

        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub FirstLoad()

        Try

            With Me.ogvTMAILMessages
                Me.FTMailSubject_Lbl_2.Text = .GetRowCellValue(.FocusedRowHandle, "FTMailSubject").ToString()
                Me.FTMailDate_Lbl_2.Text = CDate(.GetRowCellValue(.FocusedRowHandle, "FTMailDate")).ToString("dd/MM/yyyy") & " " & .GetRowCellValue(.FocusedRowHandle, "FTInsTime").ToString()
                Me.FTMailTo_Lbl_2.Text = .GetRowCellValue(.FocusedRowHandle, "FTMailTo").ToString()
                ' Me.FTMailFrom_Lbl.Text = "From : " & .GetRowCellValue(.FocusedRowHandle, "FTMailFrom").ToString()

                _FTMailTo = .GetRowCellValue(.FocusedRowHandle, "FTMailTo").ToString()
                _FTMailSubject = .GetRowCellValue(.FocusedRowHandle, "FTMailSubject").ToString()
                _FNMailStateAttach = .GetRowCellValue(.FocusedRowHandle, "FNMailStateAttach").ToString()
                _FNMailStatePriority = .GetRowCellValue(.FocusedRowHandle, "FNMailStatePriority").ToString()
                _FNMailFileAttach = .GetRowCellValue(.FocusedRowHandle, "FNMailFileAttach").ToString()
                _FTMailText2 = .GetRowCellValue(.FocusedRowHandle, "FTMailText").ToString()
                _FTMailFrom = .GetRowCellValue(.FocusedRowHandle, "FTMailFrom").ToString()
                _FTMailId = .GetRowCellValue(.FocusedRowHandle, "FTMailId").ToString()


                Call LoadogcTMAILFileAttach(.GetRowCellValue(.FocusedRowHandle, "FTMailId").ToString())

            End With

        Catch ex As Exception

            '  Me.FTMailTo_Lbl.Text = "To : "
        End Try

    End Sub

    Public Shared Function StrToByteArray(strValue As String) As Byte()  'Function แปลงเป็น byte
        Dim encoding As New System.Text.ASCIIEncoding()
        Return encoding.GetBytes(strValue)
    End Function


    Private Function InsertTMAILMessages() As Boolean

        Dim _Str As String
        Dim _FTMailId As Long
        Dim _sMailTo As String = String.Empty

        Try

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MAIL)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            'Dim sTempDate As String
            'sTempDate = Now.Date.ToString("yyyy/MM/dd") & " " & Format(Date.Now, "HH:mm:ss")

            'Update เลขที่เก่า เป็น สถานะ Reply

            'If _FormState = statusform.FReply Or _FormState = statusform.FReplyALL Or _FormState = statusform.FForword Then

            '    If _MailType <> MailType.Delete Then



            '        '_Str = ""
            '        '_Str = "UPDATE TMAILMessages "
            '        '_Str &= Environment.NewLine & "SET  [FNMailStateSend] = 3 "
            '        '_Str &= Environment.NewLine & ", [FTUpdUser] = '" & HI.ST.UserInfo.UserName & "'"
            '        '_Str &= Environment.NewLine & ", [FDUpdDate] = " & HI.UL.ULDate.FormatDateDB
            '        '_Str &= Environment.NewLine & ", [FTUpdTime] = " & HI.UL.ULDate.FormatTimeDB
            '        '_Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
            '        '_Str &= Environment.NewLine & " WHERE FTMailId = " & wMailNew2.Data_FTMailId_Old



            '        _Str = "Delete  "
            '        _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
            '        _Str &= Environment.NewLine & " WHERE FTMailId IN (" & wMailNew2.Data_FTMailId_Old & ")"
            '        _Str &= Environment.NewLine & "Delete "
            '        _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILFileAttach] "
            '        _Str &= Environment.NewLine & " WHERE FTMailId IN (" & wMailNew2.Data_FTMailId_Old & ")"


            '        If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            '            HI.Conn.SQLConn.Tran.Rollback()
            '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '            Return False
            '        End If

            '        Try
            '            Dim Path_Delete = sPathServer & wMailNew2.Data_FTMailId_Old

            '            If Directory.Exists(Path_Delete) Then

            '                System.IO.Directory.Delete(Path_Delete, True)

            '            End If
            '        Catch ex As Exception

            '        End Try


            '    End If

            'End If


            Dim splitMailto() As String = _FTMailTo.Split(";")

            _sUsersystem = String.Empty

            For i = 0 To splitMailto.GetUpperBound(0)

                If splitMailto(i).ToString = String.Empty Then Exit For


                ''ตรวจสอบ ว่ามีรายชื่ออยู่ในระบบหรือไม่
                If Find_UserSystem(splitMailto(i).ToString.Trim) = True Then

                    If _sMailTo = String.Empty Then
                        _sMailTo = splitMailto(i)
                    Else
                        _sMailTo &= " ; " & splitMailto(i)
                    End If



                    '***************************************************************************
                    ' ส่งเมลขาเข้า FNMailStateType = 1
                    '  _FTMailId = GetRunNoID("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                    _Str = ""
                    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
                    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & splitMailto(i) & "'"
                    _Str &= ",'" & _FTMailSubject & "',0,1,"
                    _Str &= _nMailStateAttach & "," & _FNMailStatePriority & ",0,'" & _FNMailFileAttach & "',"
                    _Str &= "@FTMailText,'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',1)"

                    HI.Conn.SQLConn.Cmd.Parameters.Add("@FTMailText", SqlDbType.Text).Value = _FTMailText2

                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    If Not (_DTAttach9 Is Nothing) Then
                        For j = 0 To _DTAttach9.Rows.Count - 1
                            _Str = ""
                            _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILFileAttach]"
                            _Str &= ControlChars.CrLf & "([FTMailId],[FNMailFileAttach])"
                            _Str &= " VALUES (" & _FTMailId & ",'" & _DTAttach9.Rows(j).Item("FNMailFileAttach").ToString() & "')"

                            If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Return False
                            End If

                            Try

                                Dim sSource = sPathServer & _FTMailId

                                If Not Directory.Exists(sSource) Then
                                    Directory.CreateDirectory(sSource)
                                End If

                                sSource = sSource & "\" & _DTAttach9.Rows(j).Item("FNMailFileAttach").ToString()

                                File.Copy(_sPathFileName, sSource, True)

                            Catch ex As Exception
                                'MessageBox.Show(ex.Message)
                            End Try

                        Next

                    End If



                    If splitMailto(i) = HI.ST.UserInfo.UserName Then  'กรณีที่ส่งเมลหาตัวเอง  open = 0,  send = 1

                        '  _FTMailId = GetRunNoID("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                        _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                        _Str = ""
                        _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                        _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                        _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
                        _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                        _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp])"
                        _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & splitMailto(i) & "'"
                        _Str &= ",'" & _FTMailSubject & "',0,0,"
                        _Str &= _nMailStateAttach & "," & _FNMailStatePriority & ",0,'" & _FNMailFileAttach & "',"
                        _Str &= "@FTMailText,'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "')"

                        HI.Conn.SQLConn.Cmd.Parameters.Add("@FTMailText", SqlDbType.Text).Value = _FTMailText2

                        If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If


                        If Not (_DTAttach9 Is Nothing) Then

                            For j = 0 To _DTAttach9.Rows.Count - 1

                                _Str = ""
                                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILFileAttach]"
                                _Str &= ControlChars.CrLf & "([FTMailId],[FNMailFileAttach])"
                                _Str &= " VALUES (" & _FTMailId & ",'" & _DTAttach9.Rows(j).Item("FNMailFileAttach").ToString() & "')"

                                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                    Return False
                                End If

                                Try

                                    Dim sSource = sPathServer & _FTMailId

                                    If Not Directory.Exists(sSource) Then
                                        Directory.CreateDirectory(sSource)
                                    End If

                                    sSource = sSource & "\" & _DTAttach9.Rows(j).Item("FNMailFileAttach").ToString()

                                    File.Copy(_sPathFileName, sSource, True)

                                Catch ex As Exception
                                    'MessageBox.Show(ex.Message)
                                End Try

                            Next

                        End If


                    End If


                Else
                    ' 
                    If _sUsersystem = String.Empty Then
                        _sUsersystem = splitMailto(i)
                    Else
                        _sUsersystem &= " ; " & splitMailto(i)
                    End If
                End If
            Next


            ' ส่งเมลขาออก FNMailStateType = 0
            '  _FTMailId = GetRunNoID("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
            _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
            _Str = ""
            _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
            _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
            _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
            _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
            _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
            _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _sMailTo & "'"
            _Str &= ",'" & _FTMailSubject & "',0,1,"
            _Str &= _nMailStateAttach & "," & _FNMailStatePriority & ",0,'" & _FNMailFileAttach & "',"
            _Str &= "@FTMailText,'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',0)"

            HI.Conn.SQLConn.Cmd.Parameters.Add("@FTMailText", SqlDbType.Text).Value = _FTMailText2

            If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If


            If Not (_DTAttach9 Is Nothing) Then
                ' For Each R As DataRow In _DTAttach.Rows
                For j = 0 To _DTAttach9.Rows.Count - 1

                    _Str = ""
                    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILFileAttach]"
                    _Str &= ControlChars.CrLf & "([FTMailId],[FNMailFileAttach])"
                    _Str &= " VALUES (" & _FTMailId & ",'" & _DTAttach9.Rows(j).Item("FNMailFileAttach").ToString() & "')"
                    ' _Str &= " VALUES (" & _FTMailId & ",'" & R!FNMailFileAttach.ToString & "')"

                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    Try
                        ' Dim sSource = sPathServer & R!FNMailFileAttach.ToString
                        Dim sSource = sPathServer & _FTMailId

                        If Not Directory.Exists(sSource) Then
                            Directory.CreateDirectory(sSource)
                        End If

                        sSource = sSource & "\" & _DTAttach9.Rows(j).Item("FNMailFileAttach").ToString()

                        File.Copy(_sPathFileName, sSource, True)

                    Catch ex As Exception
                        'MessageBox.Show(ex.Message)
                    End Try

                Next

            End If




            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)


            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub CreateTabel_Attach()
        _DTAttach = New DataTable
        _DTAttach.Columns.Add("FTMailId", GetType(Object))
        _DTAttach.Columns.Add("FNMailFileAttach", GetType(Object))
    End Sub


    Private Function Find_UserSystem(ByVal TempUserName As String) As Boolean
        Dim _dt As DataTable
        Dim _Sql As String

        Try

            _Sql = ""
            _Sql = "SELECT FTUserName "
            _Sql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].[dbo].[TSEUserLogin]"
            _Sql &= Environment.NewLine & "WHERE FTUserName = '" & TempUserName.Trim & "'"


            _dt = HI.Conn.SQLConn.GetDataTable(_Sql, HI.Conn.DB.DataBaseName.DB_SECURITY)

            If _dt.Rows.Count > 0 Then

                Return True
            Else
                Return False
            End If


        Catch ex As Exception

        End Try
        Return True
    End Function



#End Region




    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefreshmail.Click
        Dim _Spls As New HI.TL.SplashScreen("Deleting... Please wait....")
        Me.Timer1.Enabled = False
        Try
            Call RefreshInfo_Time(True)

            _Spls.Close()

        Catch ex As Exception
            _Spls.Close()
        End Try
        Me.Timer1.Enabled = True

    End Sub





    Private Sub Show_OldData(ByVal OldRow As Long)



        With Me.ogvTMAILMessages

            If OldRow < 0 Or OldRow > .RowCount Then Exit Sub


            ' ถ้าสถานะ FTMailStateOpen = 0  ยังไม่เปิด   >> เปลี่ยนเป็น 1 เปิดอ่าน

            Me.FTMailSubject_Lbl_2.Text = .GetRowCellValue(OldRow, "FTMailSubject").ToString()
            Me.FTMailDate_Lbl_2.Text = CDate(.GetRowCellValue(OldRow, "FTMailDate")).ToString("dd/MM/yyyy")
            Me.FTMailTo_Lbl_2.Text = .GetRowCellValue(OldRow, "FTMailTo").ToString()
            '  Me.FTMailFrom_Lbl.Text = "From : " & .GetRowCellValue(.FocusedRowHandle, "FTMailFrom").ToString()

            'If .GetRowCellValue(OldRow, "FTMailText").ToString() <> String.Empty Then
            '    recMessage.LoadDocument(_PathRFT & .GetRowCellValue(OldRow, "FTMailText").ToString(), DocumentFormat.Rtf)
            'Else
            '    recMessage.Text = ""
            'End If

            ' recMessage.Document.Text = .GetRowCellValue(.FocusedRowHandle, "FTMailText").ToString()

            If .GetRowCellValue(OldRow, "FTMailStateOpen").ToString() = 0 And _MailType = 0 Then  ' ยังไม่ได้อ่าน +  Inbox
                Call UpdateTMAILMessages_StateOpen(.GetRowCellValue(OldRow, "FTMailId").ToString())
                Call RefreshInfo()
                Call LoadogcTMAILMessages(MailType.Inbox)
                ' _Lst.FocusedNode = _Lst.Nodes(0)
            End If
        End With


    End Sub

    Private Sub Show_RowData()

        With Me.ogvTMAILMessages

            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub


            ' ถ้าสถานะ FTMailStateOpen = 0  ยังไม่เปิด   >> เปลี่ยนเป็น 1 เปิดอ่าน

            Me.FTMailSubject_Lbl_2.Text = .GetRowCellValue(.FocusedRowHandle, "FTMailSubject").ToString()
            Me.FTMailDate_Lbl_2.Text = CDate(.GetRowCellValue(.FocusedRowHandle, "FTMailDate")).ToString("dd/MM/yyyy") & " " & .GetRowCellValue(0, "FTInsTime").ToString()
            Me.FTMailTo_Lbl_2.Text = .GetRowCellValue(.FocusedRowHandle, "FTMailTo").ToString()
            '  Me.FTMailFrom_Lbl.Text = "From : " & .GetRowCellValue(.FocusedRowHandle, "FTMailFrom").ToString()

            'If .GetRowCellValue(0, "FTMailText").ToString() <> String.Empty Then
            '    recMessage.LoadDocument(_PathRFT & .GetRowCellValue(0, "FTMailText").ToString(), DocumentFormat.Rtf)
            'Else
            '    recMessage.Text = ""
            'End If

            'recMessage.Document.Text = .GetRowCellValue(.FocusedRowHandle, "FTMailText").ToString()

            If .GetRowCellValue(0, "FTMailStateOpen").ToString() = 0 And _MailType = 0 Then  ' ยังไม่ได้อ่าน +  Inbox
                'Call UpdateTMAILMessages_StateOpen(.GetRowCellValue(0, "FTMailId").ToString())
                'Call RefreshInfo()
                'Call LoadogcTMAILMessages(MailType.Inbox)
                ' _Lst.FocusedNode = _Lst.Nodes(0)
            End If
        End With


    End Sub

    Private Sub ocmaddnew_Click(sender As Object, e As EventArgs) Handles ocmaddnewMail.Click

        Try

            ' Call Set_Button(False)

            Call Set_ShowLabel()

            Call CreateTabel_Attach9()

            _FormState = statusform.Fnew
            FTMailTo9.Text = String.Empty
            FTMailSubject9.Text = String.Empty
            FNMailStatePriority9.SelectedIndex = 0
            FNMailStatePriority9.Enabled = True
            FTMailText29.Text = String.Empty
            ogcTMAILFileAttach9.DataSource = Nothing

            PclShowMail.Visible = True
            SpcShow.Visible = False

            FTMailTo9.Focus()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Set_Button(ByVal Temp_Bollen As Boolean)
        ocmrefreshmail.Enabled = Temp_Bollen
        ocmdeletemail.Enabled = Temp_Bollen
        ocmForwordMail.Enabled = Temp_Bollen
        '  ocmmail.Enabled = Temp_Bollen
        ocmaddnewMail.Enabled = Temp_Bollen
        ocmReplyMail.Enabled = Temp_Bollen
        PclShowMail.Visible = Not Temp_Bollen
    End Sub

    Private Function UpdateTMAILMessages_StateOpen(ByVal DataId As Long) As Boolean

        Dim _Str As String
        ' Dim tFTMailOpenTime As String = HI.UL.ULDate.FormatTimeDB

        Try

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MAIL)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Str = ""
            _Str = "UPDATE TMAILMessages "
            _Str &= Environment.NewLine & "SET  [FTMailStateOpen] = 1"
            _Str &= Environment.NewLine & ", [FDMailOpenDate] = " & HI.UL.ULDate.FormatDateDB
            _Str &= Environment.NewLine & ", [FTMailOpenTime] = " & HI.UL.ULDate.FormatTimeDB
            _Str &= Environment.NewLine & ", [FTUpdUser] = '" & HI.ST.UserInfo.UserName & "'"
            _Str &= Environment.NewLine & ", [FDUpdDate] = " & HI.UL.ULDate.FormatDateDB
            _Str &= Environment.NewLine & ", [FTUpdTime] = " & HI.UL.ULDate.FormatTimeDB
            _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
            _Str &= Environment.NewLine & " WHERE FTMailId = " & DataId
            _Str &= Environment.NewLine & " AND FTMailTo = '" & HI.ST.UserInfo.UserName & "'"

            If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Function UpdateTMAILMessages_StateDelete(ByVal DataId As Long, ByVal Temp_Status As String) As Boolean

        Dim _Str As String
        ' Dim tFTMailOpenTime As String = HI.UL.ULDate.FormatTimeDB

        Try

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MAIL)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Str = ""
            Select Case Temp_Status
                Case 0  ' ย้ายไป Re
                    _Str = "UPDATE TMAILMessages "
                    _Str &= Environment.NewLine & "SET  [FNMailStateDelete] = 1"
                    _Str &= Environment.NewLine & ", [FTUpdUser] = '" & HI.ST.UserInfo.UserName & "'"
                    _Str &= Environment.NewLine & ", [FDUpdDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTUpdTime] = " & HI.UL.ULDate.FormatTimeDB
                    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
                    _Str &= Environment.NewLine & " WHERE FTMailId = " & DataId
                Case 1  ' ลบจริง
                    _Str = "Delete  "
                    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
                    _Str &= Environment.NewLine & " WHERE FTMailId IN (" & DataId & ")"
                    _Str &= Environment.NewLine & "Delete "
                    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILFileAttach] "
                    _Str &= Environment.NewLine & " WHERE FTMailId IN (" & DataId & ")"


            End Select

            If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            _FTMailTo = String.Empty

            If Temp_Status = 1 Then

                Try
                    Dim Path_Delete = sPathServer & _FTMailId

                    If Directory.Exists(Path_Delete) Then

                        System.IO.Directory.Delete(Path_Delete, True)

                    End If
                Catch ex As Exception

                End Try

            End If

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Function Delete_TMAILFileAttach(ByVal DataId As Long, ByVal DataFile As String) As Boolean
        Dim _Str As String

        Try
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MAIL)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Str = ""
            _Str = " Delete FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILFileAttach] "
            _Str &= Environment.NewLine & " WHERE FTMailId = '" & DataId & "' AND FNMailFileAttach ='" & DataFile & "'"

            If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        Catch ex As Exception

        End Try

        Return True

    End Function

    Private Function UpdateTMAILMessages_State_UnDelete(ByVal DataId As Long) As Boolean

        Dim _Str As String

        Try

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MAIL)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Str = "UPDATE TMAILMessages "
            _Str &= Environment.NewLine & "SET  [FNMailStateDelete] = 0"
            _Str &= Environment.NewLine & ", [FTUpdUser] = '" & HI.ST.UserInfo.UserName & "'"
            _Str &= Environment.NewLine & ", [FDUpdDate] = " & HI.UL.ULDate.FormatDateDB
            _Str &= Environment.NewLine & ", [FTUpdTime] = " & HI.UL.ULDate.FormatTimeDB
            _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
            _Str &= Environment.NewLine & " WHERE FTMailId = " & DataId


            If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            _FTMailTo = String.Empty

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    'Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click

    '    ' ถ้าสถานะ  FNMailStateSend = 2   ลบข้อมูลจริงใน Database
    '    'ถ้าสถานะ  FNMailStateSend = 1     update เป็น 2

    '    With ogvTMAILMessages
    '        If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub
    '        UpdateTMAILMessages_StateDelete(Me.ogvTMAILMessages.GetRowCellValue(.FocusedRowHandle, "FNMailStateSend").ToString(), Me.ogvTMAILMessages.GetRowCellValue(.FocusedRowHandle, "FTMailId").ToString())
    '    End With

    '    ' UpdateTMAILMessages_StateDelete
    '    'If File.Exists(Path) Then
    '    '    File.Delete(Path)
    '    'End If

    '    ' ถ้าสถานะ FTMailStateOpen = 0  ยังไม่เปิด   >> เปลี่ยนเป็น 1 เปิดอ่าน

    'End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Call RefreshInfo_Time(False)

    End Sub

    Private Sub RefreshInfo()  '  หาจำนวน จดหมาย Refresh

        Call ConutTMAILMessages()

        For Each N As DevExpress.XtraTreeList.Nodes.TreeListNode In _Lst.Nodes
            Select Case N.GetValue(0).ToString
                Case "1" '"0"
                    ' N.SetValue(1).text = N.GetValue(4).ToString & " ( " & _CountInbox.ToString() & ") "
                    ' N.GetValue(0).ToString = N.GetValue(4).ToString & " ( " & _CountInbox.ToString() & ") "
                    ' N.SetValue(1).text = N.GetValue(4).ToString & " ( " & _CountInbox.ToString() & ") "
                    ' N.SetValue(1, N.GetValue(4).ToString & " ( " & _CountInbox.ToString() & ") ")


                    'If _CountInbox_UnRead = 0 Then
                    '    N.SetValue(1, "Inbox (" & _CountInbox_Read.ToString() & ")")
                    'Else
                    '    N.SetValue(1, "Inbox (" & _CountInbox_Read.ToString() & ") (" & _CountInbox_UnRead & ")")
                    'End If

                    'If _CountInbox_UnRead = 0 Then
                    '    N.SetValue(1, Trim(N.GetValue(1).ToString.Split("(")(0)) & "(" & _CountInbox_Read.ToString() & ")")
                    'Else
                    '    N.SetValue(1, Trim(N.GetValue(1).ToString.Split("(")(0)) & "(" & _CountInbox_Read.ToString() & ") (" & _CountInbox_UnRead & ")")
                    'End If


                    If _CountInbox_UnRead = 0 Then
                        'N.SetValue(1, Trim(N.GetValue(1).ToString.Split("(")(0)) & "(" & " (" & _CountInbox_Read.ToString() & ")")
                        N.SetValue(1, Trim(N.GetValue(1).ToString.Split("(")(0)) & "(" & _CountInbox_Read.ToString() & ")")
                    Else
                        ' N.SetValue(1, Trim(N.GetValue(1).ToString.Split("(")(0)) & "(" & " (" & _CountInbox_Read.ToString() & ") (" & _CountInbox_UnRead & ")")
                        N.SetValue(1, Trim(N.GetValue(1).ToString.Split("(")(0)) & "(" & _CountInbox_Read.ToString() & ") (" & _CountInbox_UnRead & ")")
                    End If

                Case "2" '"1"

                    ' N.SetValue(1, "Sent (" & _CountSent.ToString() & ")")

                    N.SetValue(1, Trim(N.GetValue(1).ToString.Split("(")(0)) & "(" & _CountSent.ToString() & ")")

                Case "3" '"2"
                    'N.SetValue(3, N.GetValue(4).ToString & " ( " & _CountDelete.ToString() & ") ")

                    ' N.SetValue(1, "Recycle Bin (" & _CountDelete.ToString() & ")")

                    N.SetValue(1, Trim(N.GetValue(1).ToString.Split("(")(0)) & "(" & _CountDelete.ToString() & ")")
            End Select
        Next

    End Sub

    Private Sub RefreshInfo_Time(Optional ReloadMail As Boolean = True)  '  หาจำนวน จดหมาย Refresh
        Try

            Dim _FocusRowInDex As Long = -1
            Dim _FTMailId As String = ""

            Try

                If ogvTMAILMessages.FocusedRowHandle > 0 Then
                    _FocusRowInDex = ogvTMAILMessages.FocusedRowHandle
                    _FTMailId = ogvTMAILMessages.GetRowCellValue(ogvTMAILMessages.FocusedRowHandle, "FTMailId").ToString
                End If

            Catch ex As Exception
            End Try

            Call ConutTMAILMessages()

            For Each N As DevExpress.XtraTreeList.Nodes.TreeListNode In _Lst.Nodes

                Select Case N.GetValue(0).ToString
                    Case "1" '"0"
                        ' N.SetValue(1, "Inbox (" & _CountInbox.ToString() & ")")

                        If _CountInbox_UnRead = 0 Then
                            'N.SetValue(1, Trim(N.GetValue(1).ToString.Split("(")(0)) & "(" & "(" & _CountInbox_Read.ToString() & ")")
                            N.SetValue(1, Trim(N.GetValue(1).ToString.Split("(")(0)) & "(" & _CountInbox_Read.ToString() & ")")
                        Else
                            ' N.SetValue(1, Trim(N.GetValue(1).ToString.Split("(")(0)) & "(" & "(" & _CountInbox_Read.ToString() & ") (" & _CountInbox_UnRead & ")")
                            N.SetValue(1, Trim(N.GetValue(1).ToString.Split("(")(0)) & "(" & _CountInbox_Read.ToString() & ") (" & _CountInbox_UnRead & ")")
                        End If

                        ' Exit For

                    Case "2"

                        N.SetValue(1, Trim(N.GetValue(1).ToString.Split("(")(0)) & "(" & _CountSent.ToString() & ")")

                    Case "3"

                        N.SetValue(1, Trim(N.GetValue(1).ToString.Split("(")(0)) & "(" & _CountDelete.ToString() & ")")

                End Select

            Next

            Select Case _MailType
                Case 0
                    Call LoadogcTMAILMessages(MailType.Inbox, _FTMailId, ReloadMail)
                    _Lst.FocusedNode = _Lst.Nodes(0)

                Case 1
                    Call LoadogcTMAILMessages(MailType.Sent, _FTMailId, ReloadMail)
                    _Lst.FocusedNode = _Lst.Nodes(1)
                Case 2
                    Call LoadogcTMAILMessages(MailType.Delete, _FTMailId, ReloadMail)
                    _Lst.FocusedNode = _Lst.Nodes(2)
            End Select

            Me.ogcTMAILMessages.BeginInit()
            Me.ogcTMAILMessages.RefreshDataSource()
            Me.ogcTMAILMessages.EndInit()
            CType(Me.ogcTMAILMessages.DataSource, DataTable).AcceptChanges()
            Dim _Found As Boolean = False
            Dim tmpdt As DataTable = CType(Me.ogcTMAILMessages.DataSource, DataTable)
            '_FocusRowInDex = -1
            With Me.ogvTMAILMessages
                If .RowCount > 0 And _FTMailId <> "" Then

                    Try
                        _FocusRowInDex = -1
                        For Each R As DataRow In tmpdt.Rows
                            _FocusRowInDex = _FocusRowInDex + 1
                            If R!FTMailId.ToString = _FTMailId Then
                                _Found = True
                                Exit For
                            End If
                        Next
                        '  _FocusRowInDex = ogvTMAILMessages.LocateByValue("FTMailId", _FTMailId)

                    Catch ex As Exception
                    End Try
                    If Not (_Found) Then
                        _FocusRowInDex = -1
                    End If
                    Try
                        If _FocusRowInDex <> -1 Then
                            .FocusedRowHandle = _FocusRowInDex
                        End If
                    Catch ex As Exception
                    End Try

                End If
            End With

            Call Show_RowData()
            '   HI.ST.Lang.SP_SETxLanguage(xForm)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmmail_Click(sender As Object, e As EventArgs) Handles ocmmail.Click
        Try

            If _FormState = statusform.FLoad Then
                ' MessageBox.Show("กรุณาเลือกเมลล์", "Message 60", MessageBoxButtons.OK, MessageBoxIcon.Error)

                ' HI.MG.ShowMsg.mProcessError(1407180001, "กรุณาเลือกเมลล์ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Information)

                HI.MG.ShowMsg.mProcessError(1407180001, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Information)

                Exit Sub
            End If

            If (FTMailTo9.Text.Trim = String.Empty) Then
                'MessageBox.Show("กรุณาระบุ ชื่อผู้รับเมลล์", "Message 60", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ' HI.MG.ShowMsg.mProcessError(1407180002, "ระบุ ชื่อผู้รับเมลล์ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Information)
                HI.MG.ShowMsg.mProcessError(1407180002, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Information)
                FTMailTo9.Focus() : Exit Sub
            End If

            ' Call Set_Button(True)

            _FTMailTo = FTMailTo9.Text
            _FTMailSubject = FTMailSubject9.Text
            If ogvTMAILFileAttach9.RowCount > 0 Then
                _nMailStateAttach = 1
            Else
                _nMailStateAttach = 0
            End If

            _FNMailStatePriority = FNMailStatePriority9.SelectedIndex
            '  _sFileName = wMailNew2.Data_sFileName
            ' _sPathFileName = wMailNew2.Data_sPathFileName
            _FTMailText2 = FTMailText29.Document.Text
            Call CreateTabel_Attach9()
            _DTAttach9 = CType(ogcTMAILFileAttach9.DataSource, DataTable)


            If InsertTMAILMessages() = True Then

                If _sUsersystem <> String.Empty Then
                    ' MessageBox.Show("ชื่อผู้รับเมลล์ไม่ถูกต้อง " & _sUsersystem, "Message 60", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    'HI.MG.ShowMsg.mProcessError(1407180003, "ชื่อผู้รับเมลล์ไม่ถูกต้อง " & _sUsersystem, Me.Text, System.Windows.Forms.MessageBoxIcon.Information)
                    HI.MG.ShowMsg.mProcessError(1407180003, "ชื่อผู้รับเมลล์ไม่ถูกต้อง ", Me.Text, System.Windows.Forms.MessageBoxIcon.Error, _sUsersystem)
                End If

                _FormState = statusform.FLoad

                Call RefreshInfo()
                Call LoadogcTMAILMessages(MailType.Inbox)
                _Lst.FocusedNode = _Lst.Nodes(0)

                PclShowMail.Visible = False
                SpcShow.Visible = True


            End If


        Catch ex As Exception

        End Try

    End Sub


    Private Sub Open_AttachFile()

        Dim sSlash As Integer

        With OpenFileDialog
            .Title = "Attach File"
            .FileName = ""
            .Multiselect = False
            .FilterIndex = 0
            .Filter = "All File|*.*"   '"Word File|*.doc|Text File|*.txt|All File|*.*"
        End With

        If OpenFileDialog.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            sSlash = InStrRev(OpenFileDialog.FileName, "\")
            _sFilePath = Mid(OpenFileDialog.FileName, 1, sSlash)
            _sFileName = Mid(OpenFileDialog.FileName, sSlash + 1, Len(OpenFileDialog.FileName))

            _sPathFileName = OpenFileDialog.FileName


        Else

            _sPathFileName = String.Empty
            _sFileName = String.Empty
        End If

    End Sub


    Private Sub Set_ShowLabel()
        'FTMailSubject_Lbl9.Text = FTMailSubject_Lbl.Text
        ' FTMailTo_Lbl9.Text = FTMailTo_Lbl.Text
        ' FNMailFileAttach_lbl9.Text = LabelControl1.Text
        ' ocmexit2.Text = ocmexit.Text
    End Sub

    Private Sub ocmForwordMail_Click(sender As Object, e As EventArgs) Handles ocmForwordMail.Click


        Try
            If _FTMailTo = String.Empty Then Exit Sub

            _FormState = statusform.FForword


            If _FTMailFrom = String.Empty Then
                Exit Sub
            End If

            '  Call Set_Button(False)

            Call Set_ShowLabel()

            PclShowMail.Visible = True
            SpcShow.Visible = False

            FTMailText29.CreateNewDocument()  ' สร้าง เคลียร์ข้อความ

            FTMailTo9.Text = String.Empty
            FTMailSubject9.Text = "FW : " & _FTMailSubject
            FNMailStatePriority9.SelectedIndex = _FNMailStatePriority
            FNMailStatePriority9.Enabled = False
            FNMailStatePriority9.BackColor = Color.White


            _FTMailId_Old = _FTMailId
            ' Call LoadogcTMAILFileAttach(wMailMain.Data_FTMailId)

            Call LoadogcTMAILFileAttach_Sent(wMailMain.Data_FTMailId)


            FTMailText29.CreateNewDocument()  ' สร้าง เคลียร์ข้อความ
            FTMailText29.Text = vbCrLf & vbCrLf & _
                               "____________________________________________________" & vbCrLf & _
                               TFrom_Lbl.Text & "  " & _FTMailFrom & vbCrLf & _
                              FTMailDate_Lbl.Text & "  " & _FDInsDate & " " & _FTInsTime & vbCrLf & _
                              FTMailTo_Lbl.Text & "  " & _FTMailTo & vbCrLf & _
                              FTMailSubject_Lbl.Text & "  " & _FTMailSubject & vbCrLf & _
                              vbCrLf & _FTMailText2 & vbCrLf


            FTMailTo9.Focus()


        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdeletemail.Click

        ' ถ้าสถานะ  FNMailStateSend = 2   ลบข้อมูลจริงใน Database
        ' ถ้าสถานะ  FNMailStateSend = 1     update เป็น 2

        With ogvTMAILMessages
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub
            Dim _dt As DataTable
            With CType(ogcTMAILMessages.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy

            End With

            Dim _Spls As New HI.TL.SplashScreen("Deleting... Please wait....")
            Me.Timer1.Enabled = False
            Try
                For Each R As DataRow In _dt.Select("FTSelect='1'")
                    UpdateTMAILMessages_StateDelete(Long.Parse(Val(R!FTMailId.ToString())), Integer.Parse(Val(R!FNMailStateDelete.ToString())))
                    'UpdateTMAILMessages_StateDelete(Me.ogvTMAILMessages.GetRowCellValue(.FocusedRowHandle, "FTMailId").ToString(), Me.ogvTMAILMessages.GetRowCellValue(.FocusedRowHandle, "FNMailStateDelete").ToString())
                Next

                _Spls.Close()

            Catch ex As Exception
                _Spls.Close()
            End Try

            Me.Timer1.Enabled = True
           
            _dt.Dispose()

        End With

        Try
            Me.FTChkStateSelect.Checked = False
        Catch ex As Exception

        End Try

        Call RefreshInfo()

        Call LoadogcTMAILMessages(_MailType)

        Select Case _MailType
            Case MailType.Inbox
                _Lst.FocusedNode = _Lst.Nodes(0)
                If _CountInbox_Read = "0" Then
                    recMessage.Text = ""
                    'Me.FTMailSubject_Lbl.Text = Me.FTMailSubject_Lbl.Text
                    'Me.FTMailDate_Lbl.Text = Me.FTMailDate_Lbl.Text
                    'Me.FTMailTo_Lbl.Text = Me.FTMailTo_Lbl.Text
                    Call Set_ShowLabel()

                    ogcTMAILFileAttach.DataSource = Nothing
                End If
            Case MailType.Sent
                _Lst.FocusedNode = _Lst.Nodes(1)
                If _CountSent = "0" Then
                    recMessage.Text = ""
                    'Me.FTMailSubject_Lbl.Text = Me.FTMailSubject_Lbl.Text
                    'Me.FTMailDate_Lbl.Text = Me.FTMailDate_Lbl.Text
                    'Me.FTMailTo_Lbl.Text = Me.FTMailTo_Lbl.Text

                    Call Set_ShowLabel()

                    ogcTMAILFileAttach.DataSource = Nothing
                End If
            Case MailType.Delete
                _Lst.FocusedNode = _Lst.Nodes(2)
                If _CountDelete = "0" Then
                    recMessage.Text = ""
                    'Me.FTMailSubject_Lbl.Text = Me.FTMailSubject_Lbl.Text
                    'Me.FTMailDate_Lbl.Text = Me.FTMailDate_Lbl.Text
                    'Me.FTMailTo_Lbl.Text = Me.FTMailTo_Lbl.Text

                    Call Set_ShowLabel()

                    ogcTMAILFileAttach.DataSource = Nothing
                End If

        End Select


    End Sub


    Private Sub ocmReplyMail_Click(sender As Object, e As EventArgs) Handles ocmReplyMail.Click
        ' ตรวจสอบว่ามีการเลือกยัง

        Try

            If _FTMailTo = String.Empty Then Exit Sub

            _FormState = statusform.FReply

            If _FTMailFrom = String.Empty Then
                Exit Sub
            End If

            '  Call Set_Button(False)

            ' FTMailTo.Text = wMailMain.Data_FTMailTo
            Call Set_ShowLabel()

            PclShowMail.Visible = True
            SpcShow.Visible = False

            FTMailTo9.Text = _FTMailFrom

            ' FTMailText29.CreateNewDocument()  ' สร้าง เคลียร์ข้อความ


            FTMailSubject9.Text = "RE : " & _FTMailSubject
            FNMailStatePriority9.SelectedIndex = _FNMailStatePriority
            FNMailStatePriority9.Enabled = False
            FNMailStatePriority9.BackColor = Color.White

            _FTMailId_Old = _FTMailId

            ' Call LoadogcTMAILFileAttach(_FTMailId)
            Call LoadogcTMAILFileAttach_Sent(_FTMailId)


            ' Dim pos As DocumentPosition = FTMailText29.Document.CaretPosition
            FTMailText29.CreateNewDocument()  ' สร้าง เคลียร์ข้อความ
            ' FTMailText29.Text = vbCrLf & vbCrLf & _FTMailText2 & vbCrLf


            FTMailText29.Text = vbCrLf & vbCrLf & _
                             "____________________________________________________" & vbCrLf & _
                           TFrom_Lbl.Text & "  " & _FTMailFrom & vbCrLf & _
                            FTMailDate_Lbl.Text & "  " & _FDInsDate & " " & _FTInsTime & vbCrLf & _
                            FTMailTo_Lbl9.Text & "  " & _FTMailTo & vbCrLf & _
                            FTMailSubject_Lbl9.Text & "  " & _FTMailSubject & vbCrLf & _
                            vbCrLf & _FTMailText2 & vbCrLf

            FTMailText29.Focus()




        Catch ex As Exception

        End Try




    End Sub

    Private Sub ogcTMAILFileAttach_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub SbntAttachFile9_Click(sender As Object, e As EventArgs) Handles SbntAttachFile9.Click
        Dim StrFileOutput As String = String.Empty

        Call Open_AttachFile()


        If _sFileName <> String.Empty Then
            Call NewRowFileAttach()

        End If

    End Sub

    Private Sub CreateTabel_Attach9()
        _DTAttach9 = New DataTable
        _DTAttach9.Columns.Add("FTMailId", GetType(Object))
        _DTAttach9.Columns.Add("FNMailFileAttach", GetType(Object))
    End Sub

    Private Sub NewRowFileAttach()
        Try

            Dim oDataRow As DataRow

            oDataRow = _DTAttach9.NewRow()

            oDataRow.Item("FTMailId") = ""
            oDataRow.Item("FNMailFileAttach") = _sFileName

            _DTAttach9.Rows.Add(oDataRow)

            ogcTMAILFileAttach9.DataSource = _DTAttach9
            ogcTMAILFileAttach9.Refresh()
            ogvTMAILFileAttach9.RefreshData()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ocmexit2_Click(sender As Object, e As EventArgs) Handles ocmexit2.Click

        PclShowMail.Visible = False
        SpcShow.Visible = True

        ' Call Set_Button(True)
    End Sub

    Private Sub FNMailStatePriority9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNMailStatePriority9.SelectedIndexChanged

    End Sub

    Private Sub FNMailStatePriority_lbl_Click(sender As Object, e As EventArgs) Handles FNMailStatePriority_lbl.Click

    End Sub



    ' Private Sub OtmCheckLanguage_Tick(sender As Object, e As EventArgs) Handles OtmCheckLanguage.Tick

    'Dim oSysLang As New ST.SysLanguage
    'Try
    '    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name.ToString.Trim, Me)
    'Catch ex As Exception
    'Finally
    'End Try

    'HI.TL.HandlerControl.AddHandlerObj(Me)
    'HI.ST.Lang.SP_SETxLanguage(_Lst)
    'Call SetDataGrid()

    ' End Sub


    Private Sub ocmUndoMail_Click(sender As Object, e As EventArgs) Handles ocmUndoMail.Click

        If _MailType = MailType.Delete Then
            'ทำการ โอน สถานะ Delete กลับเป็นปกติ

            With ogvTMAILMessages
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub
                UpdateTMAILMessages_State_UnDelete(Me.ogvTMAILMessages.GetRowCellValue(.FocusedRowHandle, "FTMailId").ToString())
            End With
            Call RefreshInfo()

            Call LoadogcTMAILMessages(_MailType)

            Select Case _MailType
                Case MailType.Inbox
                    _Lst.FocusedNode = _Lst.Nodes(0)
                    If _CountInbox_Read = "0" Then
                        recMessage.Text = ""
                        Call Set_ShowLabel()
                        ogcTMAILFileAttach.DataSource = Nothing
                    End If
                Case MailType.Sent
                    _Lst.FocusedNode = _Lst.Nodes(1)
                    If _CountSent = "0" Then
                        recMessage.Text = ""
                        Call Set_ShowLabel()
                        ogcTMAILFileAttach.DataSource = Nothing
                    End If
                Case MailType.Delete
                    _Lst.FocusedNode = _Lst.Nodes(2)
                    If _CountDelete = "0" Then
                        recMessage.Text = ""
                        Call Set_ShowLabel()
                        ogcTMAILFileAttach.DataSource = Nothing
                    End If

            End Select

        End If
    End Sub


    Private Sub ogvTMAILMessages_Click(sender As Object, e As EventArgs) Handles ogvTMAILMessages.Click

        Try

            With Me.ogvTMAILMessages
                Dim pt As Point = ogvTMAILMessages.GridControl.PointToClient(Control.MousePosition)
                Dim info As GridHitInfo = ogvTMAILMessages.CalcHitInfo(pt)

                If (info.InRow Or info.InRowCell) Then
                    ' If Me.ogvTMAILMessages.FocusedColumn.Name = "ColImagePriority" Or Me.ogvTMAILMessages.FocusedColumn.Name = "ColImageOpen" Or Me.ogvTMAILMessages.FocusedColumn.Name = "ColImageAttach" Then Exit Sub

                    '  If Me.ogvTMAILMessages.FocusedColumn.ColumnEditName = "RepositoryItemPictureEdit7" Or Me.ogvTMAILMessages.FocusedColumn.ColumnEditName = "RepositoryItemPictureEdit4" Or Me.ogvTMAILMessages.FocusedColumn.ColumnEditName = "RepositoryItemPictureEdit6" Then Exit Sub
                    ' MessageBox.Show(.FocusedColumn.ColumnEditName)

                    ' MessageBox.Show(.FocusedRowHandle & vbCrLf & Me.ogvTMAILMessages.FocusedColumn.Name & vbCrLf & Me.ogvTMAILMessages.FocusedColumn.ColumnEditName)

                    If Me.ogvTMAILMessages.FocusedRowHandle < 0 Or Me.ogvTMAILMessages.FocusedRowHandle > .RowCount Then Exit Sub

                    _OldRow = .FocusedRowHandle
                    ' ถ้าสถานะ FTMailStateOpen = 0  ยังไม่เปิด   >> เปลี่ยนเป็น 1 เปิดอ่าน

                    Me.FTMailSubject_Lbl_2.Text = .GetRowCellValue(.FocusedRowHandle, "FTMailSubject").ToString()
                    _FTInsTime = .GetRowCellValue(.FocusedRowHandle, "FTInsTime").ToString()

                    Me.FTMailDate_Lbl_2.Text = CDate(.GetRowCellValue(.FocusedRowHandle, "FTMailDate")).ToString("dd/MM/yyyy") & " " & _FTInsTime
                    Me.FTMailTo_Lbl_2.Text = .GetRowCellValue(.FocusedRowHandle, "FTMailTo").ToString()

                    '  Me.FTMailFrom_Lbl.Text = "From : " & .GetRowCellValue(.FocusedRowHandle, "FTMailFrom").ToString()
                    ' Me.FNMailFileAttach_Lbl.Text = .GetRowCellValue(.FocusedRowHandle, "FNMailFileAttach").ToString()
                    'If .GetRowCellValue(.FocusedRowHandle, "FTMailText").ToString() <> String.Empty Then
                    '    recMessage.LoadDocument(_PathRFT & .GetRowCellValue(.FocusedRowHandle, "FTMailText").ToString(), DocumentFormat.Rtf)
                    'Else
                    '    recMessage.Text = ""
                    'End If
                    ' Dim text = System.Text.Encoding.ASCII.GetString(Data)
                    ' แปลง byte to string
                    ' recMessage.Document.Text = System.Text.Encoding.ASCII.GetString(.GetRowCellValue(.FocusedRowHandle, "FTMailText"))

                    recMessage.Document.Text = .GetRowCellValue(.FocusedRowHandle, "FTMailText").ToString()
                    Try
                        Dim _AppIndex As Integer = recMessage.Document.Text.IndexOf("Approved")
                        Dim _RejectIndex As Integer = recMessage.Document.Text.IndexOf("Rejected")

                        If _AppIndex >= 0 Or _RejectIndex >= 0 Then

                            'Dim charProperties As DevExpress.XtraRichEdit.API.Native.CharacterProperties = recMessage.Document.BeginUpdateCharacters(recMessage.Document.Range)
                            'charProperties.FontName = "Tahoma"
                            'charProperties.FontSize = 18.25F
                            'recMessage.Document.EndUpdateCharacters(charProperties)
                            Dim myStart As DevExpress.XtraRichEdit.API.Native.DocumentPosition = Nothing
                            Select Case True
                                Case (_AppIndex >= 0)
                                    myStart = recMessage.Document.CreatePosition(_AppIndex)
                                Case (_RejectIndex >= 0)
                                    myStart = recMessage.Document.CreatePosition(_RejectIndex)
                            End Select

                            If Not (myStart Is Nothing) Then

                                Dim myRange As DevExpress.XtraRichEdit.API.Native.DocumentRange = recMessage.Document.CreateRange(myStart, 60)

                                Select Case True
                                    Case (_AppIndex >= 0)
                                        myRange = recMessage.Document.CreateRange(myStart, "Approved".Length)
                                    Case (_RejectIndex >= 0)
                                        myRange = recMessage.Document.CreateRange(myStart, "Rejected".Length)
                                End Select

                                recMessage.Document.Selection = myRange

                                Dim doc As Document = recMessage.Document
                                Dim range As DocumentRange = doc.Selection
                                Dim cp As CharacterProperties = doc.BeginUpdateCharacters(range)

                                cp.FontSize = 22

                                Select Case True
                                    Case (_AppIndex >= 0)
                                        cp.ForeColor = Color.Green
                                    Case (_RejectIndex >= 0)
                                        cp.ForeColor = Color.Red
                                End Select

                                cp.Bold = True
                                ' cp.BackColor = Color.Blue
                                ' cp.Underline = UnderlineType.DoubleWave
                                ' cp.UnderlineColor = Color.White
                                doc.EndUpdateCharacters(cp)
                                recMessage.Document.Selection = recMessage.Document.CreateRange(0, 0)
                            End If

                        End If
                    Catch ex As Exception
                    End Try

                    Dim _FocusRowInDex As Integer = -1
                    Dim _FTMailId As String = ""

                    Try

                        If ogvTMAILMessages.FocusedRowHandle > 0 Then
                            _FocusRowInDex = ogvTMAILMessages.FocusedRowHandle
                            _FTMailId = ogvTMAILMessages.GetRowCellValue(ogvTMAILMessages.FocusedRowHandle, "FTMailId").ToString
                        End If

                    Catch ex As Exception
                    End Try

                    If .GetRowCellValue(.FocusedRowHandle, "FTMailStateOpen").ToString() = 0 And _MailType = 0 Then  ' ยังไม่ได้อ่าน +  Inbox
                        Call UpdateTMAILMessages_StateOpen(.GetRowCellValue(.FocusedRowHandle, "FTMailId").ToString())
                        Call RefreshInfo()
                        Call LoadogcTMAILMessages(MailType.Inbox)
                        ' _Lst.FocusedNode = _Lst.Nodes(0)

                        Dim _Found As Boolean = False
                        Dim tmpdt As DataTable = CType(Me.ogcTMAILMessages.DataSource, DataTable)
                        '_FocusRowInDex = -1
                        With Me.ogvTMAILMessages
                            If .RowCount > 0 And _FTMailId <> "" Then

                                Try
                                    _FocusRowInDex = -1
                                    For Each R As DataRow In tmpdt.Rows
                                        _FocusRowInDex = _FocusRowInDex + 1
                                        If R!FTMailId.ToString = _FTMailId Then
                                            _Found = True
                                            Exit For
                                        End If
                                    Next
                                    '  _FocusRowInDex = ogvTMAILMessages.LocateByValue("FTMailId", _FTMailId)

                                Catch ex As Exception
                                End Try
                                If Not (_Found) Then
                                    _FocusRowInDex = -1
                                End If
                                Try
                                    If _FocusRowInDex <> -1 Then
                                        .FocusedRowHandle = _FocusRowInDex
                                    End If
                                Catch ex As Exception
                                End Try

                            End If
                        End With

                    End If

                    _FTMailTo = .GetRowCellValue(.FocusedRowHandle, "FTMailTo").ToString()
                    _FTMailSubject = .GetRowCellValue(.FocusedRowHandle, "FTMailSubject").ToString()
                    _FNMailStateAttach = .GetRowCellValue(.FocusedRowHandle, "FNMailStateAttach").ToString()
                    _FNMailStatePriority = .GetRowCellValue(.FocusedRowHandle, "FNMailStatePriority").ToString()
                    _FNMailFileAttach = .GetRowCellValue(.FocusedRowHandle, "FNMailFileAttach").ToString()
                    _FTMailText2 = .GetRowCellValue(.FocusedRowHandle, "FTMailText").ToString()

                    If _MailType = MailType.Inbox Then
                        _FTMailFrom = .GetRowCellValue(.FocusedRowHandle, "FTMailFrom").ToString()
                    Else
                        _FTMailFrom = .GetRowCellValue(.FocusedRowHandle, "FTMailFromTemp").ToString()
                    End If

                    _FTMailId = .GetRowCellValue(.FocusedRowHandle, "FTMailId").ToString()

                    If .GetRowCellValue(.FocusedRowHandle, "FTMailFrom").ToString() <> String.Empty Then
                        _FDInsDate = CDate(.GetRowCellValue(.FocusedRowHandle, "FDInsDate").ToString()).ToString("dd/MM/yyyy").ToString
                    Else
                        _FDInsDate = String.Empty
                    End If

                    olblink.ForeColor = Color.Blue
                    Me.olblink.Text = .GetRowCellValue(.FocusedRowHandle, "FTMailInfoRef").ToString()
                    Me.olblink.Visible = (.GetRowCellValue(.FocusedRowHandle, "FTMailInfoRef").ToString() <> "")
                    Me.olbFTCallMnuName.Text = .GetRowCellValue(.FocusedRowHandle, "FTCallMnuName").ToString()
                    Me.olbFTCallMethodName.Text = .GetRowCellValue(.FocusedRowHandle, "FTCallMethodName").ToString()
                    Me.MailTypeInfo = Integer.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNMailTypeInfo").ToString()))

                    Select Case MailTypeInfo
                        Case 4

                            Dim _Qry As String

                            _Qry = "SELECT TOP 1 M.FTReceiveNo"
                            _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENMailSendAppRcvOver AS M  WITH (NOLOCK)"
                            _Qry &= vbCrLf & " WHERE  (M.FTReceiveNo = N'" & HI.UL.ULF.rpQuoted(olblink.Text) & "') "
                            _Qry &= vbCrLf & " AND (ISNULL(M.FTStateApprove, N'') = N'')"

                            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "") = "" Then
                                olblink.ForeColor = Color.Red
                            End If

                        Case Else

                    End Select

                    Try

                        Dim _Sql As String
                        _Sql = "SELECT TOP 1  FPUserImage   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin WITH (NOLOCK) "
                        _Sql &= vbCrLf & " WHERE FTUserName='" & HI.UL.ULF.rpQuoted(_FTMailFrom) & "'"

                        Try
                            Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Sql, Conn.DB.DataBaseName.DB_SECURITY)
                            PicMail.Image = Nothing
                            For Each R As DataRow In dt.Rows
                                PicMail.Image = HI.UL.ULImage.ConvertByteArrayToImmage(R!FPUserImage)
                                Exit For
                            Next

                        Catch ex As Exception
                            PicMail.Image = Nothing
                        End Try

                    Catch ex As Exception
                        PicMail.Image = Nothing
                    End Try

                    Call LoadogcTMAILFileAttach(.GetRowCellValue(.FocusedRowHandle, "FTMailId").ToString())

                    '  FTMailTo_Lbl.Text = ogvTMAILMessages.GetDataRow(ogvTMAILMessages.FocusedRowHandle)("FTMailSubject").ToString()
                    '  Convert.ToDateTime(DATESTART).ToString("yyyy-MM-dd") & "' , " & _
                    ' Convert.ToDateTime(DATESTART).ToString("yyyy-MM-dd") & " " & Now.ToString("HH:mm:ss") & "' , " & _

                End If
                
            End With

        Catch ex As Exception
            ' MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub ogvTMAILFileAttach_DoubleClick(sender As Object, e As EventArgs) Handles ogvTMAILFileAttach.DoubleClick

        With Me.ogvTMAILFileAttach
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub
            Try
                Dim PathAttach As String = String.Empty
                PathAttach = sPathServer & .GetRowCellValue(.FocusedRowHandle, "FTMailId").ToString() & "\" & .GetRowCellValue(.FocusedRowHandle, "FNMailFileAttach").ToString()
                If File.Exists(PathAttach) Then   ' ถ้ามีอยู่แล้วว ลบ File ทิ้งก่อน แล้วค่อยเพิ่ม
                    'File.Delete(PathAttach)
                    Process.Start(PathAttach)

                End If

            Catch ex As Exception

            End Try

        End With
    End Sub

    Private Sub ogvTMAILFileAttach_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvTMAILFileAttach.KeyDown
        With Me.ogvTMAILFileAttach
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub

            Select Case e.KeyCode

                'Case System.Windows.Forms.Keys.Delete

                '    Try
                '        With ogvTMAILFileAttach
                '            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub
                '        End With

                '        ' ลบ โฟเดอร์ที่ Server
                '        Dim PathAttach As String = String.Empty
                '        PathAttach = sPathServer & .GetRowCellValue(.FocusedRowHandle, "FTMailId").ToString() & "\" & .GetRowCellValue(.FocusedRowHandle, "FNMailFileAttach").ToString()
                '        If File.Exists(PathAttach) Then   ' ถ้ามีอยู่แล้วว ลบ File ทิ้งก่อน แล้วค่อยเพิ่ม
                '            File.Delete(PathAttach)
                '        End If

                '        ' ลบข้อมูลที่ Attfile
                '        Call Delete_TMAILFileAttach(.GetRowCellValue(.FocusedRowHandle, "FTMailId").ToString(), .GetRowCellValue(.FocusedRowHandle, "FNMailFileAttach").ToString())

                '        'ลบที่ DataGrid
                '        Call ogvTMAILFileAttach.DeleteRow(ogvTMAILFileAttach.FocusedRowHandle)
                '        CType(Me.ogcTMAILFileAttach.DataSource, DataTable).AcceptChanges()




                '    Catch ex As Exception

                '    End Try


                Case System.Windows.Forms.Keys.Enter

                    Try
                        Dim PathAttach As String = String.Empty
                        PathAttach = sPathServer & .GetRowCellValue(.FocusedRowHandle, "FTMailId").ToString() & "\" & .GetRowCellValue(.FocusedRowHandle, "FNMailFileAttach").ToString()
                        If File.Exists(PathAttach) Then   ' ถ้ามีอยู่แล้วว ลบ File ทิ้งก่อน แล้วค่อยเพิ่ม
                            'File.Delete(PathAttach)
                            Process.Start(PathAttach)

                        End If

                    Catch ex As Exception

                    End Try


            End Select

        End With

    End Sub

    Private Sub ogvTMAILFileAttach9_DoubleClick(sender As Object, e As EventArgs) Handles ogvTMAILFileAttach9.DoubleClick
        With Me.ogvTMAILFileAttach9
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub
            Try
                Dim PathAttach As String = String.Empty
                PathAttach = sPathServer & .GetRowCellValue(.FocusedRowHandle, "FTMailId").ToString() & "\" & .GetRowCellValue(.FocusedRowHandle, "FNMailFileAttach").ToString()
                If File.Exists(PathAttach) Then   ' ถ้ามีอยู่แล้วว ลบ File ทิ้งก่อน แล้วค่อยเพิ่ม
                    'File.Delete(PathAttach)
                    Process.Start(PathAttach)

                End If

            Catch ex As Exception

            End Try

        End With
    End Sub

    Private Sub ogvTMAILFileAttach9_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvTMAILFileAttach9.KeyDown
        With Me.ogvTMAILFileAttach9
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub

            Select Case e.KeyCode

                Case System.Windows.Forms.Keys.Delete

                    Try
                        'With ogvTMAILFileAttach9
                        '    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub
                        'End With

                        ' สร้างเมลใหม่ ไม่ต้องลบ เป็นการส่งต่อ ตอบกลับ จะแนบไฟล์หรือลบทิ้งก็ได้
                        'Dim PathAttach As String = String.Empty
                        'PathAttach = sPathServer & .GetRowCellValue(.FocusedRowHandle, "FTMailId").ToString() & "\" & .GetRowCellValue(.FocusedRowHandle, "FNMailFileAttach").ToString()
                        'If File.Exists(PathAttach) Then   ' ถ้ามีอยู่แล้วว ลบ File ทิ้งก่อน แล้วค่อยเพิ่ม
                        '    File.Delete(PathAttach)
                        'End If

                        Call ogvTMAILFileAttach9.DeleteRow(ogvTMAILFileAttach9.FocusedRowHandle)
                        CType(Me.ogcTMAILFileAttach9.DataSource, DataTable).AcceptChanges()


                    Catch ex As Exception

                    End Try


                Case System.Windows.Forms.Keys.Enter

                    Try


                        Dim PathAttach As String = String.Empty
                        PathAttach = sPathServer & .GetRowCellValue(.FocusedRowHandle, "FTMailId").ToString() & "\" & .GetRowCellValue(.FocusedRowHandle, "FNMailFileAttach").ToString()
                        If File.Exists(PathAttach) Then   ' ถ้ามีอยู่แล้วว ลบ File ทิ้งก่อน แล้วค่อยเพิ่ม
                            'File.Delete(PathAttach)
                            Process.Start(PathAttach)

                        End If

                    Catch ex As Exception

                    End Try


            End Select

        End With
    End Sub


    Private Sub ogcTMAILMessages_Click(sender As Object, e As EventArgs)
        Try

        Catch ex As Exception
            ' MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ogcTMAILMessages_Click_1(sender As Object, e As EventArgs) Handles ogcTMAILMessages.Click

    End Sub

    Private Sub olblink_Click(sender As Object, e As EventArgs) Handles olblink.Click
        Try
            Select Case MailTypeInfo
                Case 4

                    Dim _Qry As String
                    Dim _dt As New DataTable
                    _Qry = "SELECT '0' AS FTSelect"
                    _Qry &= vbCrLf & " , M.FTReceiveNo"
                    _Qry &= vbCrLf & "  , CASE WHEN ISDATE(R.FDReceiveDate) = 1 THEN  CONVERT(nvarchar(10),Convert(datetime,R.FDReceiveDate),103) ELSE '' END AS FDReceiveDate"
                    _Qry &= vbCrLf & " , R.FTReceiveBy"
                    _Qry &= vbCrLf & " , R.FTPurchaseNo"
                    _Qry &= vbCrLf & " , M.FNHSysRawMatId"
                    _Qry &= vbCrLf & " , IM.FTRawMatCode"

                    If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                        _Qry &= vbCrLf & " , IM.FTRawMatNameTH AS FTRawMatName"
                    Else
                        _Qry &= vbCrLf & " , IM.FTRawMatNameEN AS FTRawMatName"
                    End If

                    _Qry &= vbCrLf & " , ISNULL(IMC.FTRawMatColorCode,'') AS FTRawMatColorCode"
                    _Qry &= vbCrLf & " , ISNULL(IMS.FTRawMatSizeCode,'') AS FTRawMatSizeCode"
                    _Qry &= vbCrLf & " , ISNULL(RD.FTFabricFrontSize,'') AS FTFabricFrontSize"
                    _Qry &= vbCrLf & " , U.FTUnitCode "
                    _Qry &= vbCrLf & " , M.FNPOQuantity"
                    _Qry &= vbCrLf & " , M.FNRcvHisQuantity"
                    _Qry &= vbCrLf & " , M.FNRcvQtyPass"
                    _Qry &= vbCrLf & " , M.FNRcvQtyOver"
                    _Qry &= vbCrLf & " , M.FNTotalRcvQty"
                    _Qry &= vbCrLf & " , 0.0000 AS FNApproveRcvQty"
                    _Qry &= vbCrLf & " , M.FTStateApprove,M.FNHSysMailAppId,R.FTPurchaseNo,R.FNHSysWHId,ISNULL(R.FTStateImport,'0') AS FTStateImport  "
                    _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENMailSendAppRcvOver AS M INNER JOIN"
                    _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R ON M.FTReceiveNo = R.FTReceiveNo INNER JOIN"
                    _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail AS RD WITH (NOLOCK) ON R.FTReceiveNo = RD.FTReceiveNo AND M.FNHSysRawMatId=RD.FNHSysRawMatId"
                    _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM ON M.FNHSysRawMatId = IM.FNHSysRawMatId LEFT OUTER JOIN"
                    _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS IMS ON IM.FNHSysRawMatSizeId = IMS.FNHSysRawMatSizeId LEFT OUTER JOIN"
                    _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS IMC ON IM.FNHSysRawMatColorId = IMC.FNHSysRawMatColorId"
                    _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK)  ON RD.FNHSysUnitId = U.FNHSysUnitId "
                    _Qry &= vbCrLf & " WHERE  (M.FTReceiveNo = N'" & HI.UL.ULF.rpQuoted(olblink.Text) & "') "
                    _Qry &= vbCrLf & " AND (ISNULL(M.FTStateApprove, N'') = N'')"
                    _Qry &= vbCrLf & " ORDER BY IM.FTRawMatCode,ISNULL(IMC.FTRawMatColorCode,''),ISNULL(IMS.FTRawMatSizeCode,'')"

                    _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

                    If _dt.Rows.Count > 0 Then

                        With _AppRcv

                            .ocmapprove.Enabled = True
                            .ocmreject.Enabled = True
                            .ocmcancel.Enabled = True
                            .ogcrcv.DataSource = _dt.Copy
                            .ShowDialog()

                        End With

                    End If

                    _dt.Dispose()

                Case 9

                    Dim _OrderNoKey As String = ""
                    Dim _SubOrderNoKey As String = ""

                    Try
                        _OrderNoKey = olblink.Text.Split("|")(0)
                    Catch ex As Exception
                    End Try

                    Try
                        _SubOrderNoKey = olblink.Text.Split("|")(1)
                    Catch ex As Exception
                    End Try

                    Call CallByName(Me.Parent.Parent, "CallWindowForm", CallType.Method, {Me.olbFTCallMnuName.Text, Me.olbFTCallMethodName.Text, ({_OrderNoKey, _SubOrderNoKey})})

                Case Else

                    Call CallByName(Me.Parent.Parent, "CallWindowForm", CallType.Method, {Me.olbFTCallMnuName.Text, Me.olbFTCallMethodName.Text, ({olblink.Text})})

            End Select

        Catch ex As Exception
        End Try
    End Sub

    Private Sub recMessage_Click(sender As Object, e As EventArgs) Handles recMessage.Click

    End Sub

    Private Sub recMessage_TextChanged(sender As Object, e As EventArgs) Handles recMessage.TextChanged

    End Sub

    Private Sub FTChkStateSelect_CheckedChanged(sender As Object, e As EventArgs) Handles FTChkStateSelect.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.FTChkStateSelect.Checked Then
                _State = "1"
            End If

            With ogcTMAILMessages
                If Not (.DataSource Is Nothing) And ogvTMAILMessages.RowCount > 0 Then

                    With ogvTMAILMessages
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub olbFTCallMethodName_Click(sender As Object, e As EventArgs) Handles olbFTCallMethodName.Click

    End Sub

    Private Sub olbFTCallMnuName_Click(sender As Object, e As EventArgs) Handles olbFTCallMnuName.Click

    End Sub
End Class


