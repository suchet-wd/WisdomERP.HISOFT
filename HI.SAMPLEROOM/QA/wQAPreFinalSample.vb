Imports DevExpress.XtraBars.Docking2010.Views.WindowsUI
Imports DevExpress.XtraBars.Docking2010.Views
Imports DevExpress.XtraEditors
Imports System.Windows.Forms
Imports System.Drawing
Imports System
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Windows.Forms.MouseButtons
Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Math
Imports DevExpress.Utils.Win

Public Class wQAPreFinalSample
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private dataSource As SampleDataSource
    Private groupsItemDetailPage As Dictionary(Of SampleDataGroup, PageGroup)
    Private oGcDetail As DevExpress.XtraGrid.GridControl
    Private Ui As uCQA
    Private _PInsTime As String = ""
    Private _QACheckPointPopup As wQAPreFinalCheckPoint
    Private _FTStateReject As Integer = 0  ' 0 = Pass , 1 = Major , 2 = Minor
    Private _Static As Boolean
    Private _SaveProc As Boolean = False
    Private Pointlist As New List(Of String)
    Private _QAPreFinalSamplePopup As wQAPreFinalSamplePopup
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Call InitFormControl()
        FTPointName.Text = ""
        _QACheckPointPopup = New wQAPreFinalCheckPoint
        _QAPreFinalSamplePopup = New wQAPreFinalSamplePopup
        Dim oSysLang As New ST.SysLanguage
        Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _QACheckPointPopup.Name.ToString.Trim, _QACheckPointPopup)
        Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _QAPreFinalSamplePopup.Name.ToString.Trim, _QAPreFinalSamplePopup)

        dataSource = New SampleDataSource()
        groupsItemDetailPage = New Dictionary(Of SampleDataGroup, PageGroup)()
        CreateLayout()
    End Sub

#Region "Property"
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
#End Region
#Region "Proceder"
    Private Sub InitFormControl()

        Me.SysDBName = "HITECH_SAMPLEROOM"
        Me.SysTableName = "TSMPTQAPreFinal"
        Me.TableName = "HITECH_SAMPLEROOM.dbo.TSMPTQAPreFinal"


    End Sub

    Private Sub FormRefresh()
        HI.TL.HandlerControl.ClearControl(Me)

        For Each Obj As Object In Me.Controls.Find("FTBarcodeRef", True)
            Select Case HI.ENM.Control.GeTypeControl(Obj)
                Case ENM.Control.ControlType.ButtonEdit
                    With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                        .Focus()
                    End With
            End Select
        Next
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
    End Sub

    Public Sub DefaultsData()
        Dim _FieldName As String
        'HI.TL.HandlerControl.ClearControl(Me)
        Me.FNHSysStyleId.Text = ""
        Me.FTSMPOrderNo.Text = ""
        Me.EmployeeInfo.Text = ""
        Me.FNQtyIn.Value = 0
        Me.FNQtyQA.Value = 0
        Me.FNQCActualQty.Value = 0
        Me.FNCtiticalQty.Value = 0
        Me.FNAndon.Value = 0

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
        Me.FDInsDate.Text = HI.Conn.SQLConn.GetField(" Select  Convert(varchar(10),convert(date," & HI.UL.ULDate.FormatDateDB & "),103) AS FDDate", Conn.DB.DataBaseName.DB_SAMPLE, "")
        Me.FTInsUser.Text = HI.ST.UserInfo.UserName
    End Sub

#End Region

    Private _FDQADate As String = ""
    Public Property FDQADate As String
        Get
            Return _FDQADate
        End Get
        Set(value As String)
            _FDQADate = value
        End Set
    End Property

    Private Sub CreateLayout()
        For Each group As SampleDataGroup In dataSource.Data.Groups
            Dim _TileGroup As New DevExpress.XtraEditors.TileGroup
            _TileGroup.Text = "222222"
            TileControl.Groups.Add(_TileGroup)
            For Each item As SampleDataItem In group.Items
                Dim _i As New DevExpress.XtraEditors.TileItem
                _i.AllowAnimation = True
                _i.BackgroundImageScaleMode = TileItemImageScaleMode.ZoomInside
                _i.AppearanceItem.Normal.BackColor = Color.SeaShell
                _i.AppearanceItem.Normal.BackColor2 = Color.LightSalmon
                _i.AppearanceItem.Normal.BorderColor = Color.White
                _i.AppearanceItem.Normal.ForeColor = Color.Black
                _i.AppearanceItem.Normal.Font = New Font("Tahoma", 8, FontStyle.Bold)
                _i.ItemSize = TileItemSize.Wide
                _i.ContentAnimation = TileItemContentAnimationType.ScrollLeft
                _i.Name = item.Description
                _i.Text = item.Content
                _i.Id = item.Id
                _TileGroup.Items.Add(_i)
            Next
        Next group
        AddHandler TileControl.ItemClick, AddressOf TileItem1_ItemClick
    End Sub

    Private Sub TileItem1_ItemClick(sender As Object, e As TileItemEventArgs)
        If Verify() Then
            If Me.FTPointName.Text <> "" Then
                subCreateLayout(e.Item.Id, e.Item.Text)
            Else
                HI.MG.ShowMsg.mInfo("Please Select Point Check !", 1411100002, Me.Text, "", MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub Back(sender As Object, e As EventArgs)
        TileControl.Controls.Remove(Ui)
    End Sub

    Private _TileGroup As DevExpress.XtraEditors.TileGroup
    Private SubTileControl1 As DevExpress.XtraEditors.TileControl

    Private Sub subCreateLayout(ByVal _Code As Integer, name As String)
        If Not (_Static) Then
            Exit Sub
        End If
        TileControl.Visible = False
        SubTileControl1 = New DevExpress.XtraEditors.TileControl
        ogrpSubmenu.Controls.Add(SubTileControl1)
        SubTileControl1.Dock = DockStyle.Fill
        SubTileControl1.BackColor = Color.LightGray
        SubTileControl1.Orientation = Orientation.Vertical

        Dim _Qry As String = ""
        Dim _oDt As DataTable
        _Qry = "SELECT     FNHSysQADetailId, FNHSysQATypeId, FTQADetailCode, FTStateActive"
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ", FTQADetailNameTH as FTQADetailName"
        Else
            _Qry &= vbCrLf & ", FTQADetailNameEN as FTQADetailName"
        End If

        _Qry &= vbCrLf & "FROM         TQAMQADetail WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FNHSysQATypeId=" & CInt(_Code)
        _Qry &= vbCrLf & " and FTStateActive = '1'"
        _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

        SubTileControl1.ShowText = True
        SubTileControl1.Text = "" & name.ToString
        SubTileControl1.ItemSize = 70
        SubTileControl1.ScrollMode = TileControlScrollMode.ScrollBar

        _TileGroup = New DevExpress.XtraEditors.TileGroup
        _TileGroup.Text = "" & name.ToString

        SubTileControl1.Groups.Add(_TileGroup)
        For Each R As DataRow In _oDt.Rows
            Dim _i As New DevExpress.XtraEditors.TileItem
            _i.AllowAnimation = True
            _i.BackgroundImageScaleMode = TileItemImageScaleMode.ZoomInside
            _i.AppearanceItem.Normal.BackColor = Color.DodgerBlue
            _i.AppearanceItem.Normal.BorderColor = Color.LightBlue
            _i.AppearanceItem.Normal.ForeColor = Color.Black
            _i.ItemSize = TileItemSize.Wide
            _i.ContentAnimation = TileItemContentAnimationType.ScrollLeft

            If CInt("0" & R!FNHSysQADetailId) <> 0 And FTPointName.Text <> "" Then
                'new
                If Me.FDQADate = "" Then Call GetBarcodeDADate()

                _Qry = "Select Top 1 * From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal_SubDetail  WITH(NOLOCK) "
                _Qry &= vbCrLf & "WHERE FNHSysStyleId=" & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
                _Qry &= vbCrLf & "AND FNHSysUnitSectId=" & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
                _Qry &= vbCrLf & "AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                _Qry &= vbCrLf & "AND FDQADate='" & Me.FDQADate & "'"
                _Qry &= vbCrLf & "AND FTBarcodeRef='" & HI.UL.ULF.rpQuoted(FTBarcodeRef.Text) & "'"
                '_Qry &= vbCrLf & "AND FNHourNo='" & _PInsTime & "'"
                _Qry &= vbCrLf & "AND FNSeq=" & CInt("0" & Me.FNQCActualQty.Value) + 1
                _Qry &= vbCrLf & "AND FTPointSubName='" & HI.UL.ULF.rpQuoted(Me.FTPointName.Text) & "'"
                _Qry &= vbCrLf & "AND FNHSysQADetailId=" & CInt(R!FNHSysQADetailId)
                'new
                If HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE).Rows.Count > 0 Then
                    _i.Checked = True
                Else
                    _i.Checked = False
                End If

            End If

            _i.Name = R!FTQADetailCode.ToString
            '_i.Text = R!FTQADetailName.ToString
            _i.Text = R!FTQADetailCode.ToString
            _i.Id = CInt(R!FNHSysQADetailId)
            Dim Elmt As DevExpress.XtraEditors.TileItemElement
            Elmt = New DevExpress.XtraEditors.TileItemElement
            '  Elmt.Text = R!FTQADetailCode.ToString
            Elmt.Text = R!FTQADetailName.ToString
            Elmt.TextAlignment = TileItemContentAlignment.BottomLeft

            _i.Elements.Add(Elmt)
            _TileGroup.Items.Add(_i)
        Next

        AddHandler SubTileControl1.RightItemClick, AddressOf SubTileItem_Click
        AddHandler SubTileControl1.ItemClick, AddressOf SubTileItem_RightItemClick
    End Sub

    Private Sub SubTileItem_Click(sender As Object, e As TileItemEventArgs)
        ogrpSubmenu.Controls.Remove(SubTileControl1)
        TileControl.Visible = True
    End Sub

    Private Sub SubTileItem_RightItemClick(sender As Object, e As TileItemEventArgs)
        Try
            If Not (Verify()) Then
                Exit Sub
            End If
            If e.Item.Checked = False Then
                e.Item.Checked = True
                If Me.FTPointName.Text <> "" Then
                    'If Me.FNQCActualQty.Value >= Me.FNQtyQA.Value Then
                    '    If HI.MG.ShowMsg.mConfirmProcess("ทำการ QA ครบตามจำนวนการสุ่มตรวจแล้ว... ต้องการทำ QA เพิ่ม ใช่หรือไม่ ?", 1411150017) = False Then
                    '        ' e.Item.Checked = False
                    '        ' ClearOnSucc()
                    '        ' Exit Sub
                    '    Else
                    '        'CalcPopup = New wPopupCalc
                    '        'With CalcPopup
                    '        '    .ShowDialog()
                    '        '    If .StateEnter = True Then
                    '        '        If .CalcValue <= Me.FNQtyIn.Value Then
                    '        '            Me.FNQtyQA.Value = .CalcValue
                    '        '        Else
                    '        '            Me.FNQtyQA.Value = Me.FNQtyIn.Value
                    '        '        End If

                    '        '    End If
                    '        'End With
                    '    End If
                    'End If

                    'SaveSubDetail(e.Item.Id, e.Item.Checked)
                    'SaveDetail_Point(e.Item.Id)
                    'SaveDetail_TH(e.Item.Id)
                    'SaveQA_H(e.Item.Id)
                    'new

                    SaveDataSubDetail(e.Item.Id)
                    'new
                End If
            Else
                e.Item.Checked = False
                If Me.FTPointName.Text <> "" Then
                    'SaveSubDetail(e.Item.Id, e.Item.Checked)
                    'Delete_Detail(e.Item.Id, e.Item.Checked)
                    'SaveDetail_Point(e.Item.Id, False)
                    'DeleteDetail_Point(e.Item.Id)
                    ''SaveDetail_TH(e.Item.Id)
                    'Delete_DetailTH(e.Item.Id)
                    'SaveQA_H(e.Item.Id)

                    'new 
                    Me.DeleteDataSubDetail(e.Item.Id)
                    'new
                End If
            End If
            Call GetMn()
        Catch ex As Exception

        End Try
        'SubTileControl.Groups.Item(0).Items.Remove(sender)
    End Sub

    Private Sub GetMn()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "Select  FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate, FNHourNo, FNSeq, FTPointSubName, D.FNHSysQADetailId , Q.FTStateCtitical , Q.FNHSysQATypeId  "
            _Cmd &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal_SubDetail AS D WITH(NOLOCK)  LEFT OUTER JOIN  "
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQADetail AS Q WITH(NOLOCK)   ON D.FNHSysQADetailId = Q.FNHSysQADetailId "
            _Cmd &= vbCrLf & "WHERE FNHSysStyleId=" & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
            _Cmd &= vbCrLf & "AND FNHSysUnitSectId=" & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
            _Cmd &= vbCrLf & "AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
            _Cmd &= vbCrLf & "AND FDQADate='" & Me.FDQADate & "'"
            _Cmd &= vbCrLf & "AND FNSeq=" & CInt("0" & Me.FNQCActualQty.Value) + 1
            _Cmd &= vbCrLf & "and FTBarcodeRef ='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeRef.Text) & "'"

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)
            Me.FNMinorQty.Value = Integer.Parse("0" & _oDt.Compute("Count(FTStateCtitical)", "FTStateCtitical = '0'"))
            Me.FNMajorQty.Value = Integer.Parse("0" & _oDt.Compute("Count(FTStateCtitical)", "FTStateCtitical = '1'"))
            Me.FNCtiticalQty.Value = Integer.Parse("0" & _oDt.Compute("Count(FTStateCtitical)", "FTStateCtitical = '2'"))

        Catch ex As Exception

        End Try
    End Sub

    Private _PicType As Integer = 0
    Private _FNSeq As Integer = 0
    Private _State As Integer = 0


    Private Function CreateTileItemElement(ByVal text As String, ByVal alignment As TileItemContentAlignment, ByVal location As Point, ByVal fontSize As Single) As TileItemElement
        Dim element As New TileItemElement()
        element.TextAlignment = alignment
        If Not location.IsEmpty Then
            element.TextLocation = location
        End If
        element.Appearance.Normal.Options.UseFont = True
        element.Appearance.Normal.Font = New Font(New FontFamily("Segoe UI Symbol"), fontSize)
        element.Text = text
        Return element
    End Function

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub FNHSysStyleId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysStyleId.EditValueChanged
        Try

            If FNHSysStyleId.Text.Trim() <> "" Then
                ' If FNHSysStyleId.Properties.Tag.ToString = "" Then
                Dim _Str As String = "SELECT TOP 1 FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK) WHERE FTStyleCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "' "
                FNHSysStyleId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "")
                'End If

                Me.LoadImangeStyle(Integer.Parse(Val(FNHSysStyleId.Properties.Tag)))
                Me.LoadDrawRectangleRectangle(_Step)

                '**************************
                ogrpSubmenu.Controls.Remove(SubTileControl1)
                TileControl.Visible = True
                '**************************
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    Private _Step As Integer = 0

    Private Sub LoadImangeStyle(_FNHSysStyleId As Integer, Optional _NextStep As Integer = 0)
        Try
            _Step = _Step + _NextStep
            If _Step <= 0 Then
                _Step = 0

            ElseIf _Step >= 4 Then
                _Step = 4
            End If

            Dim _Qry As String = ""
            Dim dt As DataTable
            _Qry = "SELECT  TOP 1   FNHSysStyleId,  FPStyleImage1,FPStyleImage2, FPStyleImage3, FPStyleImage4"
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTStyleCode='" & Me.FNHSysStyleId.Text & "'"
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)


            For Each Rx As DataRow In dt.Rows
                Me.FTImage.Controls.Clear()
                Select Case _Step
                    Case 0
                        Try
                            Me.FTImage.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\MODELSAMPLE\" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "\" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "_" & _Step.ToString & ".JPG")

                            If Me.FTImage.Image Is Nothing Then
                                Me.FTImage.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage1))
                            End If
                        Catch ex As Exception
                            Me.FTImage.Image = Nothing
                        End Try

                    Case 1
                        Try
                            Me.FTImage.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\MODELSAMPLE\" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "\" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "_" & _Step.ToString & ".JPG")
                            If Me.FTImage.Image Is Nothing Then
                                Me.FTImage.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage2))
                            End If

                        Catch ex As Exception
                            Me.FTImage.Image = Nothing
                        End Try
                    Case 2
                        Try
                            Me.FTImage.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\MODELSAMPLE\" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "\" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "_" & _Step.ToString & ".JPG")
                            If Me.FTImage.Image Is Nothing Then
                                Me.FTImage.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage3))
                            End If

                        Catch ex As Exception
                            Me.FTImage.Image = Nothing
                        End Try
                    Case 3
                        Try

                            Me.FTImage.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\MODELSAMPLE\" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "\" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "_" & _Step.ToString & ".JPG")
                            If Me.FTImage.Image Is Nothing Then
                                Me.FTImage.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage4))
                            End If

                        Catch ex As Exception
                            Me.FTImage.Image = Nothing
                        End Try
                End Select

                LoadDrawRectangleRectangle(_Step)
            Next
            dt.Dispose()
        Catch ex As Exception
        End Try
    End Sub

    Private textBox12 As ZBobb.AlphaBlendTextBox
    Private _Path As String = System.Windows.Forms.Application.StartupPath.ToString

    Private Sub LoadDrawRectangleRectangle(ByVal _Type As Integer)
        Dim _Qry As String = ""
        Dim _oDt As DataTable
        _Qry = "SELECT    A.FNHSysStyleId, A.FNSeq, A.FNPointX, A.FNPointY, A.FTPicType, A.FNPicHeight, A.FNPicWidth"
        _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTStylePoint AS A WITH(NOLOCK) LEFT OUTER JOIN "
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B WITH(NOLOCK) ON A.FNHSysStyleId = B.FNHSysStyleId "
        _Qry &= vbCrLf & "Where A.FTStyleCode='" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "'"
        _Qry &= vbCrLf & "AND A.FTPicType=" & CInt(_Type)
        _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
        For Each R As DataRow In _oDt.Rows

            textBox12 = New ZBobb.AlphaBlendTextBox
            textBox12.BorderStyle = BorderStyle.Fixed3D
            textBox12.BackColor = Color.LightPink
            textBox12.Multiline = True
            textBox12.ReadOnly = True
            textBox12.ForeColor = Color.Transparent
            textBox12.ShortcutsEnabled = False
            Me.FTImage.SendToBack()
            textBox12.Location = New System.Drawing.Point(CInt(Abs(R!FNPointX)), CInt(Abs(R!FNPointY)))
            textBox12.Size = New System.Drawing.Size(CInt(Abs(R!FNPicWidth)), CInt(Abs(R!FNPicHeight)))
            textBox12.Name = "" & R!FNSeq.ToString
            Me.FTImage.Controls.Add(textBox12)
            textBox12.BringToFront()
            AddHandler textBox12.MouseDown, AddressOf ObjClick
            'AddHandler textBox12.MouseMove, AddressOf Obj_MouseMove
        Next
    End Sub

    Public Function _ResizeImage(ByVal image As Image) As Image
        Dim newWidth As Integer
        Dim newHeight As Integer
        newWidth = FTImage.Width
        newHeight = FTImage.Height
        Dim newImage As Image = New Bitmap(newWidth, newHeight)
        Using graphicsHandle As Graphics = Graphics.FromImage(newImage)
            graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic
            graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight)
        End Using
        Return newImage
    End Function

    Private _CountQAQty As Integer = 0
    Private Function GetCountQAActualQty() As Integer
        Try
            If _CountQAQty > 0 Then
                _CountQAQty -= -1
            Else
                _CountQAQty = 0
            End If
            Return _CountQAQty
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private _PointName As String = ""

    Private Sub ObjClick(sender As Object, e As MouseEventArgs)
        Try
            Call GetBarcodeDADate()
            If e.Button = System.Windows.Forms.MouseButtons.Left Then
                Dim _Qry As String = ""
                Dim _oDt As DataTable
                _Qry = "SELECT top 1   FTPointName,   FNSeq, FNPointX, FNPointY, FTPicType, FNPicHeight, FNPicWidth,FTRemark,FTPicName"
                _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTStylePoint"
                _Qry &= vbCrLf & "Where FTStyleCode='" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "'"
                _Qry &= vbCrLf & "AND FTPicType=" & CInt(_Step)
                _Qry &= vbCrLf & "AND FNSeq=" & CInt(CType(sender, ZBobb.AlphaBlendTextBox).Name.ToString)

                _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
                For Each R As DataRow In _oDt.Rows
                    Me.FTImage.Image = _ResizeImage(HI.UL.ULImage.LoadImage(_Path & "\Images\POINTMODELSAMPLE\" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "\" & R!FTPicName.ToString))
                    _PointName = "" & R!FTPointName.ToString
                    _PicType = "" & R!FTPicType.ToString
                    _FNSeq = CInt(R!FNSeq)
                Next

                ocmNext.Visible = False
                ocmPrev.Visible = False
                ocmBack.Visible = True
                _Static = True
                FTImagePoint_EditValueChanged()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmPrev_Click(sender As Object, e As EventArgs) Handles ocmPrev.Click
        Try
            If Me.FNHSysStyleId.Properties.Tag.ToString <> "" Then
                LoadImangeStyle(Val(Me.FNHSysStyleId.Properties.Tag), -1)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ocmNext_Click(sender As Object, e As EventArgs) Handles ocmNext.Click
        Try
            If Me.FNHSysStyleId.Properties.Tag.ToString <> "" Then
                LoadImangeStyle(Val(Me.FNHSysStyleId.Properties.Tag), 1)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private textBoxS As ZBobb.AlphaBlendTextBox
    Private _PState As String
    Private Sub FTImagePoint_EditValueChanged()
        Try
            'If FTImage.EditValue <> Nothing Then
            _Static = True
            Dim i As Integer = 0
            For y As Integer = 0 To 3
                For x As Integer = 0 To 3
                    textBoxS = New ZBobb.AlphaBlendTextBox
                    textBoxS.BorderStyle = BorderStyle.Fixed3D
                    textBoxS.BackColor = Color.LightPink
                    textBoxS.Multiline = True
                    textBoxS.ReadOnly = True
                    textBoxS.ForeColor = Color.Transparent
                    textBoxS.ShortcutsEnabled = False
                    Me.FTImage.SendToBack()
                    textBoxS.Location = New System.Drawing.Point(120 * x, 120 * y)
                    textBoxS.Size = New System.Drawing.Size(120, 120)
                    i += +1
                    If _PointName <> "" Then
                        textBoxS.Name = "" & _PointName & "-" & i.ToString
                    Else
                        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                            Select Case _Step
                                Case 0
                                    textBoxS.Name = "" & "ชิ้นหน้า" & "-" & i.ToString
                                Case 1
                                    textBoxS.Name = "" & "ชิ้นหลัง" & "-" & i.ToString
                                Case 2
                                    textBoxS.Name = "" & "ชิ้นซ้าย" & "-" & i.ToString
                                Case Else
                                    textBoxS.Name = "" & "ชิ้นขวา" & "-" & i.ToString
                            End Select
                        Else
                            Select Case _Step
                                Case 0
                                    textBoxS.Name = "" & "Front" & "-" & i.ToString
                                Case 1
                                    textBoxS.Name = "" & "Back" & "-" & i.ToString
                                Case 2
                                    textBoxS.Name = "" & "Left" & "-" & i.ToString
                                Case Else
                                    textBoxS.Name = "" & "Right" & "-" & i.ToString
                            End Select
                        End If
                    End If
                    Me.FTImage.Controls.Add(textBoxS)
                    textBoxS.BringToFront()
                    Dim _Qry As String = ""

                    _Qry = "Select Top 1 * From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal_SubDetail WITH(NOLOCK) "
                    _Qry &= vbCrLf & "WHERE FNHSysStyleId=" & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
                    _Qry &= vbCrLf & "AND FNHSysUnitSectId=" & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
                    _Qry &= vbCrLf & "AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                    _Qry &= vbCrLf & "AND FDQADate='" & Me.FDQADate & "'"
                    _Qry &= vbCrLf & "AND FTBarcodeRef='" & HI.UL.ULF.rpQuoted(FTBarcodeRef.Text) & "'"
                    '_Qry &= vbCrLf & "AND FNHourNo='" & _PInsTime & "'"
                    _Qry &= vbCrLf & "AND FNSeq=" & CInt("0" & Me.FNQCActualQty.Value) + 1
                    _Qry &= vbCrLf & "AND FTPointSubName='" & HI.UL.ULF.rpQuoted(textBoxS.Name.ToString) & "'"

                    If HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE).Rows.Count > 0 Then
                        _PState = "1"
                    Else
                        _PState = "0"
                    End If
                    textBoxS.BackColor = IIf(_PState = "1", Color.Blue, Color.White)
                    AddHandler textBoxS.MouseDown, AddressOf ObjFocus
                Next
            Next
            'End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ObjFocus(sender As Object, e As MouseEventArgs)
        Try
            If e.Button = System.Windows.Forms.MouseButtons.Left Then
                Dim _Qry As String = ""
                FTPointName.Text = sender.name.ToString
                _Qry = "Select Top 1 * From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal_SubDetail WITH(NOLOCK) "
                _Qry &= vbCrLf & "WHERE FNHSysStyleId=" & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
                _Qry &= vbCrLf & "AND FNHSysUnitSectId=" & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
                _Qry &= vbCrLf & "AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                _Qry &= vbCrLf & "AND FDQADate='" & Me.FDQADate & "'"
                _Qry &= vbCrLf & "AND FTBarcodeRef='" & HI.UL.ULF.rpQuoted(FTBarcodeRef.Text) & "'"
                '_Qry &= vbCrLf & "AND FNHourNo='" & _PInsTime & "'"
                _Qry &= vbCrLf & "AND FNSeq=" & CInt("0" & Me.FNQCActualQty.Value) + 1
                _Qry &= vbCrLf & "AND FTPointSubName='" & HI.UL.ULF.rpQuoted(Me.FTPointName.Text) & "'"

                If HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE).Rows.Count > 0 Then
                    _PState = "1"
                Else
                    _PState = "0"
                End If

                If _PState <> "1" Then
                    If CType(sender, ZBobb.AlphaBlendTextBox).BackColor.Name.ToString = "ff0000ff" Then
                        CType(sender, ZBobb.AlphaBlendTextBox).BackColor = Color.White
                        FTPointName.Text = ""
                        Pointlist.Remove(sender.name.ToString)
                    Else
                        CType(sender, ZBobb.AlphaBlendTextBox).BackColor = Color.Blue
                        FTPointName.Text = "" & sender.name.ToString
                        Pointlist.Add(sender.name.ToString)
                    End If
                Else
                    FTPointName.Text = "" & sender.name.ToString
                End If

                '**************************
                ogrpSubmenu.Controls.Remove(SubTileControl1)
                TileControl.Visible = True
                '**************************
            Else
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub obtSubRefesh_ItemClick(sender As Object, e As EventArgs) Handles ocmBack.Click
        _PointName = ""
        FTPointName.Text = ""
        LoadImangeStyle(CInt("0" & FNHSysStyleId.Properties.Tag), 0)
        ocmNext.Visible = True
        ocmPrev.Visible = True
        ocmBack.Visible = False
        _Static = False
        '**************************
        ogrpSubmenu.Controls.Remove(SubTileControl1)
        TileControl.Visible = True
        '**************************
    End Sub

    Private Sub PointName_lbl_EnabledChanged(sender As Object, e As EventArgs)
        Try
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTImage_Click(sender As Object, e As EventArgs) Handles FTImage.Click
        Try
            If Not (Me.FTImage.Image Is Nothing) Then
                ocmNext.Visible = False
                ocmPrev.Visible = False
                ocmBack.Visible = True
                Me.FTImagePoint_EditValueChanged()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles ocmPass.Click
        Try
            If Verify() Then
                If Me.FNQCActualQty.Value >= Me.FNQtyQA.Value Then
                    If HI.MG.ShowMsg.mConfirmProcess("ทำการ QA ครบตามจำนวนการสุ่มตรวจแล้ว... ต้องการทำ QA เพิ่ม ใช่หรือไม่ ?", 1411150017) = False Then
                        ClearOnSucc()
                        Exit Sub
                    Else
                        'CalcPopup = New wPopupCalc
                        'With CalcPopup
                        '    .ShowDialog()
                        '    If .StateEnter = True Then
                        '        If .CalcValue <= Me.FNQtyIn.Value Then
                        '            Me.FNQtyQA.Value = .CalcValue
                        '        Else
                        '            Me.FNQtyQA.Value = Me.FNQtyIn.Value
                        '        End If

                        '    End If
                        'End With
                    End If
                End If

                _FTStateReject = 0
                _SetActualCheck()

                'SaveNew
                Me.SaveDataHeader()

                Me.SaveDataDetail("0")

                'If (Me.FNMajorQty.Value + Me.FNMinorQty.Value + Me.FNCtiticalQty.Value) > 0 Then
                '    Me.SaveDataDetail("1")
                'Else
                '    Me.SaveDataDetail("0")
                'End If

                'SaveNew
                'SaveDetail_TW()
                'UpdateSubMinor()
                '**************************

                LoadImangeStyle(CInt("0" & FNHSysStyleId.Properties.Tag), 0)
                ocmNext.Visible = True
                ocmPrev.Visible = True
                ocmBack.Visible = False
                _Static = False
                ogrpSubmenu.Controls.Remove(SubTileControl1)
                TileControl.Visible = True

                '**************************
                'End If
                Pointlist.Clear()
                Me.FTPointName.Text = ""
                Me.FNMajorQty.Value = 0
                Me.FNMinorQty.Value = 0
                Me.FNCtiticalQty.Value = 0
                Un_State = True

                nextpcs()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub nextpcs()
        Try

            Me.FDQADate = ""
            If Me.FTBarcodeRef.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTBarcodeRef_lbl.Text)
                Me.FTBarcodeRef.Focus()
                Exit Sub
            End If

            If Me.FTBarcodeNo.Text <> "" Then
                GetPackNo(Me.FTBarcodeNo.Text)
            End If
            ' SetNewCarton()
            Me.FTBarcodeNo.Focus()
            Me.FTBarcodeNo.SelectAll()
        Catch ex As Exception

        End Try
    End Sub

    Private Function Verify() As Boolean
        Try
            If Me.FTBarcodeRef.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FTBarcodeRef_lbl.Text)
                FTBarcodeRef.Focus()
                Return False
            End If

            If Me.FTBarcodeNo.Text = "" And DirectCast(Me.ogcbarcode.DataSource, DataTable).Rows.Count <= 0 Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FTBarcodeNo_lbl.Text)
                FTBarcodeNo.Focus()
                Return False
            End If
            If Me.FNHSysStyleId.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FNHSysStyleId_lbl.Text)
                FNHSysStyleId.Focus()
                Return False
            End If
            'If Me.FNHSysUnitSectId.Text = "" Then
            '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FNHSysUnitSectId_lbl.Text)
            '    FNHSysUnitSectId.Focus()
            '    Return False
            'End If
            If Me.FTSMPOrderNo.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FTOrderNo_lbl.Text)
                FTSMPOrderNo.Focus()
                Return False
            End If
            If Me.FNQtyIn.Value = 0 Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FNQtyIn_lbl.Text)
                FNQtyIn.Focus()
                Return False
            End If
            If Me.FNQtyQA.Value = 0 Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FNQtyQA_lbl.Text)
                FNQtyQA.Focus()
                Return False
            End If
            'Call GetBarcodeDADate()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub SimpleButton7_Click(sender As Object, e As EventArgs) Handles ocmAndon.Click
        Try
            If Me.FNQCActualQty.Value > 0 Then
                Me.FNAndon.Value += +1
                'UpdateAndon()
                'New
                UpdateUnDon()
                'New
                Me.FTPointName.Text = ""
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GetBarcodeDADate()
        Try
            Me.FDQADate = ""
            Me.FDQADate = HI.Conn.SQLConn.GetField(" Select TOP 1 FDQADate FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal AS A WITH(NOLOCK)  WHERE  (FTBarcodeRef = N'" & HI.UL.ULF.rpQuoted(Me.FTBarcodeRef.Text) & "') ", Conn.DB.DataBaseName.DB_SAMPLE, "")

            If Me.FDQADate = "" Then
                Me.FDQADate = HI.Conn.SQLConn.GetField(" Select " & HI.UL.ULDate.FormatDateDB, Conn.DB.DataBaseName.DB_SAMPLE, "")
            End If
            _PInsTime = ""
            _PInsTime = HI.Conn.SQLConn.GetField(" Select TOP 1 FNHourNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal AS A WITH(NOLOCK) WHERE  (FTBarcodeRef = N'" & HI.UL.ULF.rpQuoted(Me.FTBarcodeRef.Text) & "') ", Conn.DB.DataBaseName.DB_SAMPLE, "")
            'If _PInsTime = "" Then
            _PInsTime = HI.Conn.SQLConn.GetField(" Select " & HI.UL.ULDate.FormatTimeDB, Conn.DB.DataBaseName.DB_SAMPLE, "")
            _PInsTime = Microsoft.VisualBasic.Left(Replace(_PInsTime, ":", ""), 4)
            'End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SimpleButton8_Click(sender As Object, e As EventArgs) Handles ocmReAndon.Click
        Try
            If Me.FNQCActualQty.Value > 0 Then
                If Me.FNAndon.Value > 0 Then
                    Me.FNAndon.Value += -1
                End If
                'New
                UpdateUnDon()
                'New
                Me.FTPointName.Text = ""
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function _GetRowState_Detail(ByVal _Style As Integer, ByVal _OrderNo As String, ByVal _Hour As Integer, ByVal _UnitSectId As Integer) As Boolean
        Try
            Dim _Qry As String = ""
            Dim _State As String = ""

            _Qry = "Select Top 1 * From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal_SubDetail WITH(NOLOCK) "
            _Qry &= vbCrLf & "WHERE FNHSysStyleId=" & CInt("0" & _Style)
            _Qry &= vbCrLf & "AND FNHSysUnitSectId=" & CInt("0" & _UnitSectId)
            _Qry &= vbCrLf & "AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
            _Qry &= vbCrLf & "AND FDQADate=" & HI.UL.ULDate.FormatDateDB
            _Qry &= vbCrLf & "AND FTBarcodeRef='" & HI.UL.ULF.rpQuoted(FTBarcodeRef.Text) & "'"
            '_Qry &= vbCrLf & "AND FNHourNo='" & _PInsTime & "'"
            _Qry &= vbCrLf & "AND FNSeq=" & CInt("0" & Me.FNQCActualQty.Value) + 1

            If HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub _SetActualCheck()
        Try
            Me.FNQCActualQty.Value += +1
        Catch ex As Exception
        End Try
    End Sub

    Private Sub _SetMajor()
        Try
            Me.FNQCActualQty.Value += +1
            Me.FNMajorQty.Value += +1
        Catch ex As Exception
        End Try
    End Sub

    Private Sub _SetMinor()
        Try
            Me.FNQCActualQty.Value += +1
            Me.FNMinorQty.Value += +1
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmMajor_Click(sender As Object, e As EventArgs) Handles ocmMajor.Click
        If Not (Verify()) Then
            Exit Sub
        End If
        If _GetRowState_Detail(CInt("0" & Me.FNHSysStyleId.Properties.Tag), Me.FTSMPOrderNo.Text, Me.FNHour.Value, CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)) Then
            _FTStateReject = 1
            _SetMajor()
            'SaveNew
            Me.SaveDataHeader()
            Me.SaveDataDetail("1")
            'SaveNew
            'UpdateMajor()
            '**************************
            LoadImangeStyle(CInt("0" & FNHSysStyleId.Properties.Tag), 0)
            ocmNext.Visible = True
            ocmPrev.Visible = True
            ocmBack.Visible = False
            _Static = False
            ogrpSubmenu.Controls.Remove(SubTileControl1)
            TileControl.Visible = True
            '*************************
            Un_StateMnM = 1
            Me.FTPointName.Text = ""
            Un_State = True
        Else
            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล บกพร่องหรือเสียหาย", 1411100001, Me.Text, "", MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub ocmMinor_Click(sender As Object, e As EventArgs) Handles ocmMinor.Click
        If Not (Verify()) Then
            Exit Sub
        End If
        If _GetRowState_Detail(CInt("0" & Me.FNHSysStyleId.Properties.Tag), Me.FTSMPOrderNo.Text, Me.FNHour.Value, CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)) Then
            _FTStateReject = 2
            _SetMinor()
            'SaveNew
            Me.SaveDataHeader()
            Me.SaveDataDetail("2")
            'SaveNew

            'UpdateMinor()
            '**************************
            Me.LoadImangeStyle(CInt("0" & FNHSysStyleId.Properties.Tag), 0)
            ocmNext.Visible = True
            ocmPrev.Visible = True
            ocmBack.Visible = False
            _Static = False
            ogrpSubmenu.Controls.Remove(SubTileControl1)
            TileControl.Visible = True
            '**************************

            Un_StateMnM = 2
            Me.FTPointName.Text = ""
            Un_State = True
        Else
            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล บกพร่องหรือเสียหาย", 1411100001, Me.Text, "", MessageBoxIcon.Error)
        End If
    End Sub

    Private Un_StateMnM As Integer = 0  '1 Major , 2 Minor
    Private Un_State As Boolean = False
    Private Sub ocmUndoM_Click(sender As Object, e As EventArgs) Handles ocmUndoM.Click
        Try
            If Not (Verify()) Then
                Exit Sub
            End If
            If Un_State Then
                Me.FNQCActualQty.Value = Me.FNQCActualQty.Value - 1
                'If Un_StateMnM = 1 Then
                '    If Me.FNMajorQty.Value > 0 Then
                '        Me.FNMajorQty.Value = Me.FNMajorQty.Value - 1
                '    End If
                'ElseIf Un_StateMnM = 2 Then
                '    If Me.FNMinorQty.Value > 0 Then
                '        Me.FNMinorQty.Value = Me.FNMinorQty.Value - 1
                '    End If
                'End If

                'new
                Me.SaveDataHeader()
                Me.UndoReject()
                'new
                Call LoadImangeStyle(CInt("0" & FNHSysStyleId.Properties.Tag), 0)
            End If

            Me.FTPointName.Text = ""
            Un_State = False

        Catch ex As Exception
        End Try

    End Sub

    Private Function _GetConfigSample() As Double
        Try
            'Dim _Qry As String = ""
            '_Qry = "Select Top 1 FNSampleQty "
            '_Qry &= vbCrLf & "From TPRODMConfigQA WITH(NOLOCK)"
            '_Qry &= vbCrLf & "WHERE FNStartQty <=" & CDbl(Me.FNQtyIn.Value)
            '_Qry &= vbCrLf & " and FNEndQty >=" & CDbl(Me.FNQtyIn.Value)
            'Return HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")
            Return Me.FNQtyIn.Value
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Sub FNQtyIn_EditValueChanged(sender As Object, e As EventArgs) Handles FNQtyIn.EditValueChanged
        Try
            If FNQtyIn.Value > 0 Then
            End If
            Me.FNHour_EditValueChanged()
            Me.FNQtyQA.Value = _GetConfigSample()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNHour_EditValueChanged()
        Try
            LoadImangeStyle(CInt("0" & FNHSysStyleId.Properties.Tag), 0)

            Dim _Qry As String = ""
            Dim _oDt As DataTable

            _Qry = "Select Top 1   FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate, FNHourNo, FNQAInQty, FNQAAqlQty, FNQAActualQty, FNMajorQty, FNMinorQty, Isnull(FNAndon,0) AS FNAndon"
            _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FDQADate='" & Me.FDQADate & "'"
            _Qry &= vbCrLf & " AND FNHSysStyleId=" & CInt(Me.FNHSysStyleId.Properties.Tag)
            _Qry &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)
            _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
            _Qry &= vbCrLf & " AND FTBarcodeCartonNo='" & HI.UL.ULF.rpQuoted(FTBarcodeNo.Text) & "'"
            _Qry &= vbCrLf & " AND FNHourNo='" & _PInsTime & "'"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
            If _oDt.Rows.Count > 0 Then
                For Each R As DataRow In _oDt.Rows
                    Me.FNQtyQA.Value = CInt(R!FNQAAqlQty)
                    Me.FNQCActualQty.Value = CInt(R!FNQAActualQty)
                    Me.FNMajorQty.Value = CInt(R!FNMajorQty)
                    Me.FNMinorQty.Value = CInt(R!FNMinorQty)
                    Me.FNAndon.Value = CInt(R!FNAndon)
                Next
            Else
                Me.FNQtyQA.Value = 0
                Me.FNQCActualQty.Value = 0
                Me.FNMajorQty.Value = 0
                Me.FNMinorQty.Value = 0
                Me.FNAndon.Value = 0
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNHSysUnitSectId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysUnitSectId.EditValueChanged
        'Try
        '    Me.FNHSysStyleId.Text = ""
        '    Me.FNHSysStyleId.Properties.Tag = 0
        'Catch ex As Exception
        'End Try
    End Sub

    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTSMPOrderNo.EditValueChanged
        Try
            Me.FNQtyIn.Value = 0
            Me.FNQtyQA.Value = 0
            Me.FNQCActualQty.Value = 0
            Me.FNMajorQty.Value = 0
            Me.FNMinorQty.Value = 0
            Me.FNAndon.Value = 0
            Me.FNHour.Value = 0
        Catch ex As Exception
        End Try
    End Sub

    Private Function SaveDocumentNo() As String
        Try
            Dim _Cmd As String = "" : Dim _CmpH As String = ""
            Dim _RefNo As String = HI.UL.ULF.rpQuoted(FTBarcodeRef.Text)
            _Cmd = "Select Top 1 FTBarcodeRef From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal WITH(NOLOCK) "
            _Cmd &= vbCrLf & " Where  FTBarcodeRef='" & HI.UL.ULF.rpQuoted(FTBarcodeRef.Text) & "'"
            If HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE, "") = "" Then
                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & FNHSysCmpId.Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                _RefNo = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH).ToString
            End If
            Return _RefNo
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function SaveDataHeader() As Boolean
        Try
            Dim _Key As String = SaveDocumentNo()

            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            With DirectCast(Me.ogcbarcode.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal"
                    _Cmd &= vbCrLf & "Set FTInsUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ",FDInsDate =" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ",FTInsTime =" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",FNQAInQty=" & CInt("0" & Me.FNQtyIn.Value)
                    _Cmd &= vbCrLf & ",FNQAAqlQty=" & CInt("0" & Me.FNQtyQA.Value)
                    _Cmd &= vbCrLf & ",FNQAActualQty = " & CInt("0" & Me.FNQCActualQty.Value)
                    _Cmd &= vbCrLf & ",FNMajorQty = " & CInt("0" & Me.FNMajorQty.Value)
                    _Cmd &= vbCrLf & ",FNMinorQty = " & CInt("0" & Me.FNMinorQty.Value)
                    '_Cmd &= vbCrLf & ",FNAndon = " & CInt("0" & Me.FNAndon.Value)
                    _Cmd &= vbCrLf & "WHERE FNHSysStyleId=" & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
                    _Cmd &= vbCrLf & " AND FNHSysUnitSectId =" & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
                    _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                    _Cmd &= vbCrLf & " AND FDQADate ='" & Me.FDQADate & "'"
                    _Cmd &= vbCrLf & " AND FTBarcodeCartonNo='" & HI.UL.ULF.rpQuoted(R!FTBarCodeCarton.ToString) & "'"
                    _Cmd &= vbCrLf & " AND FNHourNo='" & _PInsTime & "'"
                    _Cmd &= vbCrLf & " AND FTBarcodeRef='" & HI.UL.ULF.rpQuoted(_Key) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal  "
                        _Cmd &= "(FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate, FNHourNo, FNQAInQty, FNQAAqlQty, FNQAActualQty, FNMajorQty, FNMinorQty,FTBarcodeCartonNo, FTBarcodeRef)" ', FNAndon
                        _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & "," & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
                        _Cmd &= vbCrLf & "," & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                        _Cmd &= vbCrLf & ",'" & Me.FDQADate & "'"
                        _Cmd &= vbCrLf & ",'" & _PInsTime & "'"
                        _Cmd &= vbCrLf & "," & CInt("0" & Me.FNQtyIn.Value)
                        _Cmd &= vbCrLf & "," & CInt("0" & Me.FNQtyQA.Value)
                        _Cmd &= vbCrLf & "," & CInt("0" & Me.FNQCActualQty.Value)
                        _Cmd &= vbCrLf & "," & CInt("0" & Me.FNMajorQty.Value)
                        _Cmd &= vbCrLf & "," & CInt("0" & Me.FNMinorQty.Value)
                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTBarCodeCarton.ToString) & "'"
                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If
                    End If
                Next
            End With

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Me.FTBarcodeRef.Text = _Key

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Function SaveDataDetail(ByVal _StateReject As String) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _Key As String = SaveDocumentNo()
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            With DirectCast(Me.ogcbarcode.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal_Detail"
                    _Cmd &= vbCrLf & "Set FTInsUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ",FDInsDate =" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ",FTInsTime =" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",FTStateReject ='" & _StateReject & "'"
                    _Cmd &= vbCrLf & "WHERE FNHSysStyleId=" & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
                    _Cmd &= vbCrLf & "AND FNHSysUnitSectId=" & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
                    _Cmd &= vbCrLf & "AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                    _Cmd &= vbCrLf & "AND FDQADate='" & Me.FDQADate & "'"
                    _Cmd &= vbCrLf & " AND FTBarcodeCartonNo='" & HI.UL.ULF.rpQuoted(R!FTBarCodeCarton.ToString) & "'"
                    _Cmd &= vbCrLf & "AND FNHourNo='" & _PInsTime & "'"
                    _Cmd &= vbCrLf & "AND FNSeq=" & CInt("0" & Me.FNQCActualQty.Value)
                    _Cmd &= vbCrLf & "and FTBarcodeRef ='" & HI.UL.ULF.rpQuoted(_Key) & "'"
                    '_Cmd &= vbCrLf & "AND FTPointSubName='" & HI.UL.ULF.rpQuoted(Me.FTPointName.Text) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal_Detail"
                        _Cmd &= " (FTInsUser, FDInsDate, FTInsTime , FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate, FNHourNo, FNSeq,  FTStateReject,FTBarcodeCartonNo,FTBarcodeRef)"
                        _Cmd &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & "," & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
                        _Cmd &= vbCrLf & "," & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                        _Cmd &= vbCrLf & ",'" & Me.FDQADate & "'"
                        _Cmd &= vbCrLf & ",'" & _PInsTime & "'"
                        _Cmd &= vbCrLf & "," & CInt("0" & Me.FNQCActualQty.Value)
                        '_Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPointName.Text) & "'"
                        _Cmd &= vbCrLf & ",'" & _StateReject & "'"
                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTBarCodeCarton.ToString) & "'"
                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        Else
                            _Cmd = "Exec  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.SP_UpdateQCSampleRoomInc '" & Me.FTBarcodeRef.Text & "'"
                            HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                        End If
                    Else
                        _Cmd = "Exec  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.SP_UpdateQCSampleRoomInc '" & Me.FTBarcodeRef.Text & "'"
                        HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                    End If
                Next
            End With

            _Cmd = "update a"
            _Cmd &= vbCrLf & " set a.FTRemark ='" & Me.FTRemark.Text & "'"
            _Cmd &= vbCrLf & " ,  a.FNHourNo ='" & _PInsTime & "'"
            _Cmd &= vbCrLf & " from    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal_Barcode     a  "
            _Cmd &= vbCrLf & " where  a.FTBarcodeRef='" & Me.FTBarcodeRef.Text & "'"
            _Cmd &= vbCrLf & " and FNBarcodeSeq=" & Me.FNBarcodeSeq.Value

            _Cmd &= vbCrLf & " and a.fnseq in  (select  max( z.fnseq  ) "
            _Cmd &= vbCrLf & " from    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal_Barcode     z  "
            _Cmd &= vbCrLf & " where  z.FTBarcodeRef='" & Me.FTBarcodeRef.Text & "'   ) "

            HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            '_Cmd = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.SP_CREATE_DATA_QualityAssuranceFinal " & Val(HI.ST.SysInfo.CmpID) & "," & Val(Me.FNHSysStyleId.Properties.Tag.ToString) & "," & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString) & ",'" & HI.UL.ULF.rpQuoted(FNHSysUnitSectId.Text.Trim()) & "','" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "','" & _PInsTime & "'," & Val(Me.FNQCActualQty.Value) & ",0"
            'HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)


            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Function UpdateUnDon() As Boolean
        Try
            Dim _Cmd As String = ""
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            With DirectCast(Me.ogcbarcode.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal"
                    _Cmd &= vbCrLf & "Set FTInsUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ",FDInsDate =" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ",FTInsTime =" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",FNAndon = " & CInt("0" & Me.FNAndon.Value)
                    _Cmd &= vbCrLf & "WHERE FNHSysStyleId=" & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
                    _Cmd &= vbCrLf & " AND FNHSysUnitSectId =" & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
                    _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                    _Cmd &= vbCrLf & " AND FDQADate ='" & Me.FDQADate & "'"
                    _Cmd &= vbCrLf & " AND FTBarcodeCartonNo='" & HI.UL.ULF.rpQuoted(R!FTBarCodeCarton.ToString) & "'"
                    _Cmd &= vbCrLf & " AND FNHourNo='" & _PInsTime & "'"
                    _Cmd &= vbCrLf & " and FTBarcodeRef ='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeRef.Text) & "'"
                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If
                Next
            End With

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

    Private Function UndoReject() As Boolean
        Try
            Dim _Cmd As String = ""
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            With DirectCast(Me.ogcbarcode.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    _Cmd = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal_Detail"
                    _Cmd &= vbCrLf & " WHERE FNHSysStyleId=" & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
                    _Cmd &= vbCrLf & " AND FNHSysUnitSectId=" & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
                    _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                    _Cmd &= vbCrLf & " AND FDQADate='" & Me.FDQADate & "'"
                    _Cmd &= vbCrLf & " AND FTBarcodeCartonNo='" & HI.UL.ULF.rpQuoted(R!FTBarCodeCarton.ToString) & "'"
                    _Cmd &= vbCrLf & " AND FNHourNo='" & _PInsTime & "'"
                    _Cmd &= vbCrLf & " AND FNSeq=" & CInt("0" & Me.FNQCActualQty.Value)
                    _Cmd &= vbCrLf & " and FTBarcodeRef ='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeRef.Text) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If
                Next
            End With

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            '_Cmd = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.SP_CREATE_DATA_QualityAssuranceFinal " & Val(HI.ST.SysInfo.CmpID) & "," & Val(Me.FNHSysStyleId.Properties.Tag.ToString) & "," & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString) & ",'" & HI.UL.ULF.rpQuoted(FNHSysUnitSectId.Text.Trim()) & "','" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "','" & _PInsTime & "'," & Val(Me.FNQCActualQty.Value) & ",1"
            'HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Function SaveDataSubDetail(_QADetailId As Integer) As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select Top 1 FTBarcodeRef From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "]..TSMPTQAPreFinal Where FTBarcodeRef='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeRef.Text) & "'"
            If HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE, "") = "" Then
                Call SaveDataHeader()
            End If

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            With DirectCast(Me.ogcbarcode.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    For Each str As String In Pointlist.ToList
                        _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal_SubDetail"
                        _Cmd &= " (FTInsUser, FDInsDate, FTInsTime , FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate, FNHourNo, FNSeq, FTPointSubName,FNHSysQADetailId,FTBarcodeCartonNo,FTBarcodeRef)"
                        _Cmd &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & "," & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
                        _Cmd &= vbCrLf & "," & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                        _Cmd &= vbCrLf & ",'" & Me.FDQADate & "'"
                        _Cmd &= vbCrLf & ",'" & _PInsTime & "'"
                        _Cmd &= vbCrLf & "," & CInt("0" & Me.FNQCActualQty.Value) + 1
                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(str) & "'"
                        _Cmd &= vbCrLf & "," & CInt("0" & _QADetailId)
                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTBarCodeCarton.ToString) & "'"
                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTBarcodeRef.Text) & "'"
                    Next

                Next
            End With

            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
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
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Function DeleteDataSubDetail(_QADetailId As Integer) As Boolean
        Try
            Dim _Cmd As String = ""

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            With DirectCast(Me.ogcbarcode.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    For Each str As String In Pointlist.ToList
                        _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal_SubDetail"
                        _Cmd &= vbCrLf & "WHERE FNHSysStyleId=" & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
                        _Cmd &= vbCrLf & "AND FNHSysUnitSectId=" & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
                        _Cmd &= vbCrLf & "AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                        _Cmd &= vbCrLf & "AND FDQADate='" & Me.FDQADate & "'"
                        '_Cmd &= vbCrLf & " AND FTBarcodeCartonNo='" & HI.UL.ULF.rpQuoted(R!FTBarCodeCarton.ToString) & "'"
                        '_Cmd &= vbCrLf & "AND FNHourNo='" & _PInsTime & "'"
                        _Cmd &= vbCrLf & "AND FNSeq=" & CInt("0" & Me.FNQCActualQty.Value) + 1
                        _Cmd &= vbCrLf & "AND FTPointSubName='" & HI.UL.ULF.rpQuoted(str) & "'"
                        _Cmd &= vbCrLf & "AND FNHSysQADetailId=" & CInt("0" & _QADetailId)
                        _Cmd &= vbCrLf & "and FTBarcodeRef ='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeRef.Text) & "'"
                    Next


                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        'HI.Conn.SQLConn.Tran.Rollback()
                        'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        'Return False
                    End If
                Next
            End With

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

    Private Sub ocmreject_Click(sender As Object, e As EventArgs) Handles ocmrejectqc.Click
        Try
            'If HI.MG.ShowMsg.mConfirmProcess(" Your Want Reject Quality Control  Yes or No. ?", 1502170001) = True Then
            '    Me.UpdateReject()
            '    nextpcs()
            '    'Me.FTSMPOrderNo.Text = ""
            '    'Me.FTSMPOrderNo.Properties.Tag = 0
            'Else
            '    ClearOnSucc()
            'End If

            If Not (Verify()) Then
                Exit Sub
            End If
            'If _GetRowState_Detail(CInt("0" & Me.FNHSysStyleId.Properties.Tag), Me.FTSMPOrderNo.Text, Me.FNHour.Value, CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)) Then
            _FTStateReject = 1
            _SetMajor()
            'SaveNew
            Me.SaveDataHeader()
            Me.SaveDataDetail("1")
            'SaveNew
            'UpdateMajor()
            '**************************
            LoadImangeStyle(CInt("0" & FNHSysStyleId.Properties.Tag), 0)
            ocmNext.Visible = True
            ocmPrev.Visible = True
            ocmBack.Visible = False
            _Static = False
            ogrpSubmenu.Controls.Remove(SubTileControl1)
            TileControl.Visible = True
            '*************************
            Un_StateMnM = 1
            Me.FTPointName.Text = ""
            Un_State = True
            nextpcs()
            'Else
            '    HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล บกพร่องหรือเสียหาย", 1411100001, Me.Text, "", MessageBoxIcon.Error)
            'End If

        Catch ex As Exception
        End Try
    End Sub

    Private Function UpdateReject() As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            With DirectCast(Me.ogcbarcode.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal"
                    _Cmd &= vbCrLf & "Set FTStateReject ='1'"
                    _Cmd &= vbCrLf & "WHERE FNHSysStyleId=" & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
                    _Cmd &= vbCrLf & " AND FNHSysUnitSectId =" & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
                    _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                    _Cmd &= vbCrLf & " AND FDQADate ='" & Me.FDQADate & "'"
                    _Cmd &= vbCrLf & " AND FNHourNo='" & _PInsTime & "'"
                    _Cmd &= vbCrLf & " AND FTBarcodeCartonNo='" & HI.UL.ULF.rpQuoted(R!FTBarCodeCarton.ToString) & "'"
                    _Cmd &= vbCrLf & " and FTBarcodeRef ='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeRef.Text) & "'"
                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    Else
                        _Cmd = "Exec  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.SP_UpdateQCSampleRoomInc '" & Me.FTBarcodeRef.Text & "'"
                        HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                    End If
                Next
            End With



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

    Private Function ClearOnSucc() As Boolean
        Try

            Me.FNQtyIn.Value = 0
            Me.FNQtyQA.Value = 0
            Me.FNQCActualQty.Value = 0
            Me.FNMajorQty.Value = 0
            Me.FNMinorQty.Value = 0
            Me.FNCtiticalQty.Value = 0
            Me.FTPointName.Text = ""
            Me.FNAndon.Value = 0
            Me.FNHour.Value = 0

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Function ValidateBarcode(_Barcode As String, ByRef _BarcodeRef As String) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "Select Top  1 FTBarcodeRef  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "]..TSMPTQAPreFinal_SubDetail WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE FTBarcodeCartonNo ='" & _Barcode & "'"
            _BarcodeRef = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE, "")
            Return _BarcodeRef = ""
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ValidateBarcodeRef(_Barcode As String) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "Select Top  1 FTBarcodeRef  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "]..TSMPTQAPreFinal WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE FTBarcodeRef ='" & _Barcode & "'"
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE, "") = ""
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub GetPackNo(_FTBarcodeNo As String)
        Try
            Call GetBarcodeDADate()
            Dim _Cmd As String = ""
            Dim _oDt As DataTable

            Dim _dtcartonbar As DataTable
            Dim _oDtBarcode As DataTable = New DataTable

            'If Me.FTBarcodeRef.Text <> "" Then
            '    If Not (ValidateBarcodeRef(Me.FTBarcodeRef.Text)) Then
            '        '  HI.MG.ShowMsg.mInfo("มีการตรวจบาร์โค้ดชุดก่อนหน้าแล้ว กรุณากดปุ่มล้างข้อมูล แล้วตรวจใหม่....", 1602181653, Me.Text)
            '        If Not (HI.MG.ShowMsg.mConfirmProcess("มีการตรวจบาร์โค้ดชุดก่อนหน้าแล้ว ต้องการตรวจซ้ำหรือไม่.....", 1812251017, "")) Then
            '            Me.FTBarcodeNo.Focus()
            '            Me.FTBarcodeNo.SelectAll()
            '            Exit Sub
            '        End If
            '    End If
            'End If
            'Dim _BarcodeRef As String = ""
            'If Not (ValidateBarcode(Me.FTBarcodeNo.Text, _BarcodeRef)) Then
            '    '  HI.MG.ShowMsg.mInfo("บาร์โค๊ด มีการทำ QA Final ไปแล้วกรุณาตรวจสอบ !!!!", 1602181434, Me.Text)
            '    If Not (HI.MG.ShowMsg.mConfirmProcess("บาร์โค๊ด มีการทำ QA Final ไปแล้ว   ต้องการตรวจซ้ำหรือไม่..... ", 1812251020, "")) Then
            '        Me.FTBarcodeRef.Text = _BarcodeRef
            '        Exit Sub
            '    End If

            'End If



            _Cmd = " SELECT Top 1 (A.FNHSysUnitSectId) AS FNHSysUnitSectId"
            _Cmd &= vbCrLf & ",(A.FNScanQuantity) AS FNScanQuantity"
            _Cmd &= vbCrLf & ",(A.FTUnitSectCode) AS FTUnitSectCode"
            _Cmd &= vbCrLf & ",(A.FTOrderNo) AS FTOrderNo"
            _Cmd &= vbCrLf & ",(ST.FTStyleCode) AS FTStyleCode "
            _Cmd &= vbCrLf & ",A.FNBundleQty"
            _Cmd &= vbCrLf & " FROM (SELECT S.FNHSysUnitSectId, S.FTBarcodeNo, SUM(S.FNQuantity) AS FNScanQuantity, S.FTOrderNo, '' FTUnitSectCode , convert(numeric(18,0),D.FNQuantity - isnull(BS.FNQuantity,0) ) as FNBundleQty"
            _Cmd &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScanOutline AS S  WITH(NOLOCK)   "
            _Cmd &= vbCrLf & "INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle AS D WITH (NOLOCK) ON S.FTBarcodeNo = D.FTBarcodeBundleNo "
            _Cmd &= vbCrLf & " outer apply ( select sum ( BS.FNQuantity) as FNQuantity  "
            _Cmd &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle AS BS WITH(NOLOCK)"
            _Cmd &= vbCrLf & "  where  BS.FTMainBarcodeBundleNo   = D.FTBarcodeBundleNo and BS.FTBarcodeBundleNo <>   D.FTBarcodeBundleNo"
            _Cmd &= vbCrLf & " ) as BS "


            _Cmd &= vbCrLf & "WHERE S.FTBarcodeNo ='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
            _Cmd &= vbCrLf & " GROUP BY S.FNHSysUnitSectId, S.FTBarcodeNo, S.FTOrderNo,   D.FNQuantity ,isnull(BS.FNQuantity,0)"
            _Cmd &= vbCrLf & " ) AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS O  WITH(NOLOCK)  ON A.FTOrderNo= O.FTSMPOrderNo INNER JOIN "
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST  WITH(NOLOCK)  ON O.FNHSysStyleId = ST.FNHSysStyleId "



            _Cmd &= vbCrLf & " Where 1=1 --A.FNHSysUnitSectId is not null "
            'If Me.FNHSysUnitSectId.Text <> "" Then
            '    _Cmd &= vbCrLf & " and A.FNHSysUnitSectId =" & Integer.Parse("0" & Me.FNHSysUnitSectId.Properties.Tag)
            'End If
            If Me.FNHSysStyleId.Text <> "" Then
                _Cmd &= vbCrLf & " and ST.FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'"
            End If
            If Me.FTSMPOrderNo.Text <> "" Then
                _Cmd &= vbCrLf & " and A.FTOrderNo ='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
            End If
            'End If
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)
            Dim _oPDt As DataTable = _oDt



            If _oDt.Rows.Count <= 0 Then
                HI.MG.ShowMsg.mInfo("Pls Check Barcode , Unit , Style ,Order !!", 1604191456, Me.Text, "", MessageBoxIcon.Stop)
                Exit Sub
            Else

                For Each R As DataRow In _oDt.Rows
                    If Integer.Parse("0" & R!FNScanQuantity.ToString) > 0 Then
                        'If Integer.Parse("0" & R!FNScanQuantity.ToString) < Integer.Parse("0" & R!FNBundleQty.ToString) Then
                        '    HI.MG.ShowMsg.mInfo("สแกนยังไม่เต็มกล่อง !!!", 1604201004, Me.Text, "", MessageBoxIcon.Stop)
                        '    Exit Sub
                        'End If
                    Else
                        HI.MG.ShowMsg.mInfo("ยังไม่มีการสแกนออกไลน์ !!!", 1604201005, Me.Text, "", MessageBoxIcon.Stop)
                        Exit Sub
                    End If
                    Exit For
                Next
                'End If
            End If

            If Me.ogcbarcode.DataSource Is Nothing Then
                With _oDtBarcode
                    .BeginInit()
                    .Columns.Add("FTBarCodeCarton", GetType(String))
                    .Rows.Add(Me.FTBarcodeNo.Text)
                    .EndInit()
                End With
            Else
                With DirectCast(Me.ogcbarcode.DataSource, DataTable)
                    .AcceptChanges()
                    _oDtBarcode = .Copy
                    'If _oDtBarcode.Rows.Count >= 1 Then
                    '    If Not (verryfydata()) Then
                    '        Me.FTBarcodeNo.Focus()
                    '        Me.FTBarcodeNo.SelectAll()
                    '        Exit Sub
                    '    End If
                    'End If
                    If _oDtBarcode.Select("FTBarCodeCarton='" & Me.FTBarcodeNo.Text & "'").Length <= 0 Then
                        _oDtBarcode.Rows.Add(Me.FTBarcodeNo.Text)
                    End If
                End With
            End If

            Me.ogcbarcode.DataSource = _oDtBarcode



            Call LoadDataInfo()


            _FTBarcodeNo = ""
            For Each r As DataRow In _oDtBarcode.Rows
                If _FTBarcodeNo <> "" Then _FTBarcodeNo &= ","
                _FTBarcodeNo &= HI.UL.ULF.rpQuoted(r!FTBarCodeCarton.ToString)
            Next

            _Cmd = " SELECT MAX(A.FNHSysUnitSectId) AS FNHSysUnitSectId"
            _Cmd &= vbCrLf & ",SUM(A.FNScanQuantity) AS FNScanQuantity"
            _Cmd &= vbCrLf & ",MAX(A.FTUnitSectCode) AS FTUnitSectCode"
            _Cmd &= vbCrLf & ",MAX(A.FTOrderNo) AS FTOrderNo"
            _Cmd &= vbCrLf & ",MAX(ST.FTStyleCode) AS FTStyleCode "
            _Cmd &= vbCrLf & ", max(isnull(FTEmpName,'') ) as FTEmpName"
            _Cmd &= vbCrLf & " FROM (SELECT 0 FNHSysUnitSectId, S.FTBarcodeNo, SUM(isnull(S.FNQuantity,0)) - max(isnull(qc.FNQAInQty,0)) AS FNScanQuantity, S.FTOrderNo, '' FTUnitSectCode"
            _Cmd &= vbCrLf & " , Emp.FTEmpName"

            _Cmd &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScanOutline AS S  WITH(NOLOCK)  "
            _Cmd &= vbCrLf & "INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle AS D WITH (NOLOCK) ON S.FTBarcodeNo = D.FTBarcodeBundleNo "
            _Cmd &= vbCrLf & "   OUTER APPLY("
            _Cmd &= vbCrLf & "  Select  STUFF((Select  ', ' + FTEmpName "
            _Cmd &= vbCrLf & " 	From(Select Convert(nvarchar(10),Row_number() Over(Order By  b.FNHSysEmpId)) + '.'  +  FTEmpNameTH + ' ' + FTEmpSurnameTH   AS FTEmpName"
            _Cmd &= vbCrLf & " from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScan_Emp b with(nolock) "
            _Cmd &= vbCrLf & " left join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee emp with(nolock) on b.FNHSysEmpId = emp.FNHSysEmpID  "
            _Cmd &= vbCrLf & " where b.FTBarcodeNo =  S.FTBarcodeNo "
            _Cmd &= vbCrLf & "  ) As TEmp For XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,1,'') AS FTEmpName  ) as Emp"
            _Cmd &= vbCrLf & "  outer apply (Select top 1   0  FNQAInQty  --SUM(FNQAInQty) as  FNQAInQty  "
            _Cmd &= vbCrLf & "  from TSMPTQAPreFinal qc with(nolock) "
            _Cmd &= vbCrLf & "  where  qc.FTBarcodeCartonNo = s.FTBarcodeNo "
            _Cmd &= vbCrLf & "  ) qc "
            _Cmd &= vbCrLf & ""
            _Cmd &= vbCrLf & ""

            _Cmd &= vbCrLf & "WHERE S.FTBarcodeNo in ('" & Replace(_FTBarcodeNo, ",", "','") & "')"


            _Cmd &= vbCrLf & " GROUP BY S.FNHSysUnitSectId, S.FTBarcodeNo, S.FTOrderNo , Emp.FTEmpName "
            _Cmd &= vbCrLf & " ) AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS O  WITH(NOLOCK)  ON A.FTOrderNo= O.FTSMPOrderNo INNER JOIN "
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST  WITH(NOLOCK)  ON O.FNHSysStyleId = ST.FNHSysStyleId "

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)
            For Each R As DataRow In _oDt.Rows
                ' FNHSysUnitSectId.Text = R!FTUnitSectCode.ToString
                EmployeeInfo.Text = R!FTEmpName.ToString
                FNHSysStyleId.Text = R!FTStyleCode.ToString
                FTSMPOrderNo.Text = R!FTOrderNo.ToString
                FNQtyIn.Value = Val(R!FNScanQuantity.ToString)

                Exit For
            Next


            If ValidateBarcodeRef(Me.FTBarcodeRef.Text) Then
                If Me.ogcbarcode.DataSource Is Nothing Then

                Else
                    If Not SaveDataHeader() Then
                        Exit Sub
                    End If
                End If


            End If



            Call FNQtyIn_EditValueChanged(FNQtyIn, New System.EventArgs)

            HI.TL.HandlerControl.ClearControl(_QAPreFinalSamplePopup)
            With _QAPreFinalSamplePopup
                .OrderNo = Me.FTSMPOrderNo.Text
                .StyleId = Val(Me.FNHSysStyleId.Properties.Tag)
                .UnitSectId = Val(Me.FNHSysUnitSectId.Properties.Tag)
                .Seq = CInt("0" & Me.FNQCActualQty.Value) + 1
                .Dates = HI.Conn.SQLConn.GetField("Select " & HI.UL.ULDate.FormatDateDB, Conn.DB.DataBaseName.DB_SAMPLE)
                .Times = _PInsTime
                .DocNo = Me.FTBarcodeRef.Text
                .Barcode = Me.FTBarcodeNo.Text

                .loadinfo(Val(_oPDt.Rows(0).Item("FNBundleQty")), 1)
                .ShowDialog()
                If Not (.Proc) Then
                    Exit Sub

                End If

                Me.FNBarcodeSeq.Value = .FNBarcodeSeq
            End With



        Catch ex As Exception
        End Try
    End Sub


    Private Function getTeamEmp() As String
        Try
            Dim _Return As String = ""

            Dim _Cmd As String = ""
            _Cmd = ""

        Catch ex As Exception
            Return ""
        End Try
    End Function


    Private Sub FTBarcodeNo_KeyDown(sender As Object, e As KeyEventArgs) Handles FTBarcodeNo.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Me.FDQADate = ""
                If Me.FTBarcodeRef.Text = "" Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTBarcodeRef_lbl.Text)
                    Me.FTBarcodeRef.Focus()
                    Exit Sub
                End If

                If Me.FTBarcodeNo.Text <> "" Then
                    GetPackNo(Me.FTBarcodeNo.Text)
                End If
                ' SetNewCarton()
                Me.FTBarcodeNo.Focus()
                Me.FTBarcodeNo.SelectAll()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmcheckpointfinal_Click(sender As Object, e As EventArgs) Handles ocmcheckpointfinal.Click
        If Me.Verify = False Then
            Exit Sub
        End If
        Call FNQtyIn_EditValueChanged(FNQtyIn, New System.EventArgs)
        Me.SaveDataHeader()
        Try
            '**************************
            LoadImangeStyle(CInt("0" & FNHSysStyleId.Properties.Tag), 0)
            ocmNext.Visible = True
            ocmPrev.Visible = True
            ocmBack.Visible = False
            FTPointName.Text = ""
            _Static = False
            ogrpSubmenu.Controls.Remove(SubTileControl1)
            TileControl.Visible = True
            '**************************
        Catch ex As Exception
        End Try
        With _QACheckPointPopup
            .FDQADate = Me.FDQADate
            .FTBarcodeCartonNo = Me.FTBarcodeNo.Text
            .FTOrderNo = Me.FTSMPOrderNo.Text
            .FNHSysStyleId = Integer.Parse(Val(Me.FNHSysStyleId.Properties.Tag.ToString))
            .FNHSysUnitSectId = Integer.Parse(Val(Me.FNHSysUnitSectId.Properties.Tag.ToString))
            .FNHourNo = _PInsTime
            .ShowDialog()
        End With
    End Sub

    Private Sub wQAPreFinal_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ogvbarcode.OptionsView.ShowAutoFilterRow = False
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Sub ogvbarcode_DoubleClick(sender As Object, e As EventArgs) Handles ogvbarcode.DoubleClick
        Try
            With ogvbarcode
                If .FocusedRowHandle < 0 Then Exit Sub
                If Me.FNQCActualQty.Value > 0 Then
                    HI.MG.ShowMsg.mInfo("ไม่สามารถ ลบบาร์โค้ดได้ เนื่องจากมีการตรวจไปแล้ว !!!", 1604201027, Me.Text)
                    Exit Sub
                End If
                .DeleteRow(.FocusedRowHandle)
                With CType(Me.ogcbarcode.DataSource, DataTable)
                    .AcceptChanges()
                End With
                Call LoadDataDelbarcode()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadDataDelbarcode()
        Try
            Dim _Cmd As String = ""
            Dim _FTBarcodeNo As String = ""
            Dim _oDtBarcode As DataTable

            With DirectCast(Me.ogcbarcode.DataSource, DataTable)
                .AcceptChanges()
                _oDtBarcode = .Copy

            End With

            _FTBarcodeNo = ""
            For Each r As DataRow In _oDtBarcode.Rows
                If _FTBarcodeNo <> "" Then _FTBarcodeNo &= ","
                _FTBarcodeNo &= HI.UL.ULF.rpQuoted(r!FTBarCodeCarton.ToString)
            Next


            _Cmd = " SELECT MAX(A.FNHSysUnitSectId) AS FNHSysUnitSectId"
            _Cmd &= vbCrLf & ",SUM(A.FNScanQuantity) AS FNScanQuantity"
            _Cmd &= vbCrLf & ",MAX(A.FTUnitSectCode) AS FTUnitSectCode"
            _Cmd &= vbCrLf & ",MAX(A.FTOrderNo) AS FTOrderNo"
            _Cmd &= vbCrLf & ",MAX(ST.FTStyleCode) AS FTStyleCode "
            _Cmd &= vbCrLf & " FROM (SELECT 0 FNHSysUnitSectId, S.FTBarcodeNo, SUM(S.FNQuantity) AS FNScanQuantity, S.FTOrderNo, '' FTUnitSectCode"
            _Cmd &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScanOutline AS S  WITH(NOLOCK)   "
            _Cmd &= vbCrLf & "INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle AS D WITH (NOLOCK) ON S.FTBarcodeNo = D.FTBarcodeBundleNo "
            _Cmd &= vbCrLf & "WHERE S.FTBarcodeNo in ('" & Replace(_FTBarcodeNo, ",", "','") & "')"
            _Cmd &= vbCrLf & " GROUP BY S.FNHSysUnitSectId, S.FTBarcodeNo, s.FTOrderNo "
            _Cmd &= vbCrLf & " ) AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS O  WITH(NOLOCK)  ON A.FTOrderNo= O.FTSMPOrderNo INNER JOIN "
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST  WITH(NOLOCK)  ON O.FNHSysStyleId = ST.FNHSysStyleId "

            Dim _oDt As DataTable
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)
            For Each R As DataRow In _oDt.Rows
                FNQtyIn.Value = Val(R!FNScanQuantity.ToString)
                Exit For
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try
            If Me.FTBarcodeRef.Text <> "" Then
                If Not VerifyDelete() Then
                    HI.MG.ShowMsg.mInfo("ไม่สามารถลบข้อมูล ตรวจสอบคูณภาพได้ เนื่องจากมีการอนุมัติจบงานไปแล้ว กรุณาตรวจสอบ !!!", 2303241414, Me.Text)
                    Exit Sub
                End If
                If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete) Then

                    If DeleteData() Then
                        Me.FTBarcodeRef.Text = ""
                        FormRefresh()
                    End If
                End If
            Else
                Me.FTBarcodeRef.Focus()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub checkstateapp()
        Try
            Me.FTStateApp.Checked = VerifyDeletecheck()


        Catch ex As Exception
        End Try
    End Sub


    Private Function VerifyDeletecheck() As Boolean
        Try
            Dim _cmd As String = ""
            Dim _odt As DataTable
            With DirectCast(Me.ogcbarcode.DataSource, DataTable)
                .AcceptChanges()
                _odt = .Copy


            End With

            For Each R As DataRow In _odt.Rows
                _cmd = "select top 1   *  from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam with(nolock)  "
                _cmd &= vbCrLf & " where  isnull(FTStateFinish,'0') = 1  and  FTTeam ='" & R!FTBarCodeCarton.ToString & "'"
                If HI.Conn.SQLConn.GetDataTable(_cmd, Conn.DB.DataBaseName.DB_SAMPLE).Rows.Count > 0 Then

                    Return True
                End If
            Next

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Function VerifyDelete() As Boolean
        Try
            Dim _cmd As String = ""
            Dim _odt As DataTable
            With DirectCast(Me.ogcbarcode.DataSource, DataTable)
                .AcceptChanges()
                _odt = .Copy


            End With

            For Each R As DataRow In _odt.Rows
                _cmd = "select top 1   *  from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam with(nolock)  "
                _cmd &= vbCrLf & " where  isnull(FTStateFinish,'0') = 1  and  FTTeam ='" & R!FTBarCodeCarton.ToString & "'"
                If HI.Conn.SQLConn.GetDataTable(_cmd, Conn.DB.DataBaseName.DB_SAMPLE).Rows.Count > 0 Then

                    Return False
                End If
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal WHERE FTBarcodeRef='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeRef.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal_CheckPoint WHERE FTBarcodeRef='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeRef.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Str = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal_Detail WHERE FTBarcodeRef='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeRef.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Str = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal_SubDetail WHERE FTBarcodeRef='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeRef.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            _Str = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal_Barcode  Where FTBarcodeRef ='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeRef.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)



            With DirectCast(Me.ogcbarcode.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    _Str = " delete from  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleQC  Where   FTTeam ='" & HI.UL.ULF.rpQuoted(R!FTBarCodeCarton.ToString) & "'"
                    HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                Next
            End With


            With DirectCast(Me.ogcbarcode.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows

                    _Str = "Exec  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.SP_UpdateQCSampleRoomInc_delete '" & HI.UL.ULF.rpQuoted(R!FTBarCodeCarton.ToString) & "'"
                    HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                Next
            End With







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

    Private Sub LoadDataInfo()
        Try
            If (_SaveProc) Then Exit Sub
            _SaveProc = True
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _oDtt As DataTable
            _Cmd = "SELECT  distinct FTBarcodeCartonNo  as FTBarCodeCarton"
            _Cmd &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal WITH(NOLOCK) "
            _Cmd &= vbCrLf & "Where FTBarcodeRef='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeRef.Text) & "'"
            _oDtt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)


            _Cmd = "SELECT Top 1 FTBarcodeRef, B.FTStyleCode as FNHSysStyleId , C.FTUnitSectCode as  FNHSysUnitSectId, FTOrderNo as FTSMPOrderNo, FDQADate, FNHourNo, FNQAInQty as FNQtyIn , FNQAAqlQty as FNQtyQA , FNQAActualQty as FNQCActualQty, FNMajorQty, FNMinorQty, FNAndon "
            _Cmd &= vbCrLf & "    , Emp.FTEmpName as EmployeeInfo , isnull(a.FTStateApp,'0') as FTStateApp  , isnull(a.FDAppDate  , '') as FDAppDate  , isnull(a.FTAppBy ,'') as FTAppBy   "
            _Cmd &= vbCrLf & " , isnull( a.FDInsDate ,'') as FDInsDate  , isnull(a.FTInsTime ,'') as FTInsTime  "
            _Cmd &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTQAPreFinal AS A WITH(NOLOCK)  "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle  AS B WITH(NOLOCK) ON A.FNHSysStyleId = B.FNHSysStyleId "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS C WITH(NOLOCK) ON A.FNHSysUnitSectId = C.FNHSysUnitSectId "
            _Cmd &= vbCrLf & "   OUTER APPLY("
            _Cmd &= vbCrLf & "  Select  STUFF((Select  ', ' + FTEmpName "
            _Cmd &= vbCrLf & " 	From(Select Convert(nvarchar(10),Row_number() Over(Order By  b.FNHSysEmpId)) + '.'  +  FTEmpNameTH + ' ' + FTEmpSurnameTH   AS FTEmpName"
            _Cmd &= vbCrLf & " from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScan_Emp b with(nolock) "
            _Cmd &= vbCrLf & " left join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee emp with(nolock) on b.FNHSysEmpId = emp.FNHSysEmpID  "
            _Cmd &= vbCrLf & " where b.FTBarcodeNo =   a.FTBarcodeCartonNo  "
            _Cmd &= vbCrLf & "  ) As TEmp For XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,1,'') AS FTEmpName  ) as Emp"

            _Cmd &= vbCrLf & "Where FTBarcodeRef='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeRef.Text) & "'"
            _Cmd &= vbCrLf & " ORDER BY  FNQAActualQty desc "
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)
            Dim _doc As String = Me.FTBarcodeRef.Text
            If _oDt.Rows.Count > 0 Then
                HI.TL.HandlerControl.ClearControl(Me)
                Me.FTBarcodeRef.Text = _doc
                Me.ogcbarcode.DataSource = _oDtt
            Else
                Me.FNHSysStyleId.Text = ""
                Me.FTSMPOrderNo.Text = ""
                Me.EmployeeInfo.Text = ""
                Me.FNQtyIn.Value = 0
                Me.FNQtyQA.Value = 0
                Me.FNQCActualQty.Value = 0
                Me.FNCtiticalQty.Value = 0
                Me.FNAndon.Value = 0
            End If


            Dim _FieldName As String = ""
            For Each R As DataRow In _oDt.Rows
                For Each Col As DataColumn In _oDt.Columns
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
            _SaveProc = False
        Catch ex As Exception
            _SaveProc = False
        End Try
    End Sub

    Private Sub FTBarcodeRef_EditValueChanged(sender As Object, e As EventArgs) Handles FTBarcodeRef.EditValueChanged
        If Me.FTBarcodeRef.Text = "" Then Exit Sub
        If _SaveProc = False Then
            '_SaveProc = True
            Call LoadDataInfo()
            checkstateapp()
            ' _SaveProc = False
        End If

    End Sub




    Private Sub updateToQCProcess()

        'CType(Me.ogcoperation.DataSource, DataTable).AcceptChanges()
        'If Not (otbjobprod.SelectedTabPage Is Nothing) And FTSMPOrderNo.Text <> "" Then


        '    Dim key As String = otbjobprod.SelectedTabPage.Name.ToString
        '    If (CheckProcess(key)) Then
        '        Exit Sub
        '    End If


        '    With CType(Me.ogcoperation.DataSource, DataTable)
        '        .AcceptChanges()

        '        Dim _FNSeq As Integer = 0
        '        Dim _Spls As New HI.TL.SplashScreen("Saving...Data Please Wait.", Me.Text)

        '        Dim _Qry As String = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleQC WHERE FTTeam ='" & HI.UL.ULF.rpQuoted(key.ToString) & "'  "
        '        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
        '        HI.Conn.SQLConn.SqlConnectionOpen()
        '        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        '        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        '        Try
        '            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

        '            End If

        '            For Each R As DataRow In .Select("FNQuantity>0", "FNSeq")

        '                _FNSeq = _FNSeq + 1

        '                _Qry = "Insert INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleQC"
        '                _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTSMPOrderNo, FTTeam, FTDate, FNSeq,FTSizeBreakDown,FTColorway, FNQuantity, FNPass,FNNotPass, FTRemark)"
        '                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        '                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
        '                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
        '                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "'"
        '                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(key.ToString) & "'"
        '                _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(R!FTDate.ToString) & "'"
        '                _Qry &= vbCrLf & " ," & _FNSeq & " "
        '                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
        '                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
        '                _Qry &= vbCrLf & " ," & Val(R!FNQuantity.ToString) & " "
        '                _Qry &= vbCrLf & " ," & Val(R!FNPass.ToString) & " "
        '                _Qry &= vbCrLf & " ," & Val(R!FNNotPass.ToString) & " "
        '                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTRemark.ToString) & "'"

        '                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

        '                    HI.Conn.SQLConn.Tran.Rollback()
        '                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        '                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        '                    _Spls.Close()

        '                    Exit Sub

        '                End If

        '            Next

        '            HI.Conn.SQLConn.Tran.Commit()
        '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        '            Try
        '                Call ocmrefresh_Click(ocmrefresh, New System.EventArgs)
        '            Catch ex As Exception
        '            End Try

        '            _Spls.Close()
        '            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        '        Catch ex As Exception
        '            HI.Conn.SQLConn.Tran.Rollback()
        '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        '            _Spls.Close()
        '            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        '        End Try

        '    End With
        'Else
        '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTSMPOrderNo_lbl.Text)
        '    FTSMPOrderNo.Focus()
        'End If

    End Sub

    Private Sub ocmrevokeapproval_Click(sender As Object, e As EventArgs) Handles ocmrevokeapproval.Click
        Try
            If Me.FTBarcodeRef.Text <> "" Then
                Dim _Cmd As String = ""
                Dim _odt As DataTable
                _Cmd = "exec  dbo.SP_ApprovedRevoke_QASample '" & HI.UL.ULF.rpQuoted(Me.FTBarcodeRef.Text) & "','" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)
                'HI.MG.ShowMsg.mInfo(2210211200, "ยกเลิกอนุมัติเรียบร้อย", Me.Text)
                FTStateApp.Checked = False

                'For Each R As DataRow In _odt.Rows
                '    HI.MG.ShowMsg.mInfo(2210211200, "ยกเลิกอนุมัติเรียบร้อย", Me.Text, R!FTMsg.ToString)
                '    If R!FTState.ToString = "1" Then
                '        FTStateApp.Checked = False
                '    End If
                '    Exit Sub
                'Next
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmapprove_Click(sender As Object, e As EventArgs) Handles ocmapprovepay.Click
        Try
            If Me.FTBarcodeRef.Text <> "" Then
                Dim _Cmd As String = ""
                Dim _odt As DataTable
                _Cmd = "exec   dbo.SP_Approved_QASample '" & HI.UL.ULF.rpQuoted(Me.FTBarcodeRef.Text) & "','" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)
                'HI.MG.ShowMsg.mInfo(2210211200, "อนุุมัติเรียบร้อย...", Me.Text)

                FTStateApp.Checked = True

                'For Each R As DataRow In _odt.Rows
                '    HI.MG.ShowMsg.mInfo(2210211200, "อนุุมัติเรียบร้อย...", Me.Text, R!FTMsg.ToString)
                '    If R!FTState.ToString = "1" Then
                '        FTStateApp.Checked = True
                '    End If
                '    Exit Sub
                'Next
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNBarcodeSeq_Click(sender As Object, e As EventArgs) Handles FNBarcodeSeq.Click
        Try
            Me.FDQADate = ""
            If Me.FTBarcodeRef.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTBarcodeRef_lbl.Text)
                Me.FTBarcodeRef.Focus()
                Exit Sub
            End If

            If Me.FTBarcodeNo.Text <> "" Then
                GetPackNo(Me.FTBarcodeNo.Text)
            End If
            ' SetNewCarton()
            Me.FTBarcodeNo.Focus()
            Me.FTBarcodeNo.SelectAll()
        Catch ex As Exception

        End Try
    End Sub
End Class

