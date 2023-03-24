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
Imports DevExpress.Utils.TouchHelpers

Public Class wQA
    Private dataSource As SampleDataSource
    Private groupsItemDetailPage As Dictionary(Of SampleDataGroup, PageGroup)
    Private oGcDetail As DevExpress.XtraGrid.GridControl
    Private Ui As uCQA
    Private _PInsTime As String = ""


    Private _FTStateReject As Integer = 0  ' 0 = Pass , 1 = Major , 2 = Minor
    Private _Static As Boolean
    Private Pointlist As New List(Of String)
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        FTPointName.Text = ""
        'PointName_lbl.Visible = False

        dataSource = New SampleDataSource()
        groupsItemDetailPage = New Dictionary(Of SampleDataGroup, PageGroup)()
        CreateLayout()

        RemoveHandler FNHSysUnitSectId.ButtonClick, AddressOf HI.TL.HandlerControl.DynamicButtone_ButtonClick
        RemoveHandler FNHSysStyleId.ButtonClick, AddressOf HI.TL.HandlerControl.DynamicButtone_ButtonClick
        RemoveHandler FTOrderNo.ButtonClick, AddressOf HI.TL.HandlerControl.DynamicButtone_ButtonClick

    End Sub


    Private Sub CreateLayout()
        For Each group As SampleDataGroup In dataSource.Data.Groups

            'TileContainer.Buttons.Add(New DevExpress.XtraBars.Docking2010.WindowsUIButton(group.Title, Nothing, -1, DevExpress.XtraBars.Docking2010.ImageLocation.AboveText, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, Nothing, True, -1, True, Nothing, False, False, True, Nothing, group, -1, False, False))

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

        'SubTileControl.Visible = True
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

        'SubTileControl.Groups.Remove(_TileGroup.Name(0).ToString)
        _TileGroup = New DevExpress.XtraEditors.TileGroup
        'SubTileControl.Groups.Remove(_TileGroup)
        _TileGroup.Text = "" & name.ToString


        SubTileControl1.Groups.Add(_TileGroup)
        For Each R As DataRow In _oDt.Rows

            Dim _i As New DevExpress.XtraEditors.TileItem

            _i.AllowAnimation = True
            '_i.Checked = True
            _i.BackgroundImageScaleMode = TileItemImageScaleMode.ZoomInside
            _i.AppearanceItem.Normal.BackColor = Color.DodgerBlue
            ''_i.AppearanceItem.Normal.BackColor2 = Color.LightBlue
            _i.AppearanceItem.Normal.BorderColor = Color.LightBlue
            _i.AppearanceItem.Normal.ForeColor = Color.Black
            '_i.AppearanceItem.Normal.Font = New Font("Tahoma", AutoSizeMode, FontStyle.Bold)


            '_i.ItemSize = TileItemSize.Wide

            _i.ItemSize = TileItemSize.Wide
            _i.ContentAnimation = TileItemContentAnimationType.ScrollLeft
            '_i.TextShowMode = TileItemContentShowMode.Hover
            '_i.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True
            '_i.Appearance.BorderColor = System.Drawing.Color.Gray
            '_i.Appearance.BackColor2 = System.Drawing.Color.LightGray
            '_i.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal

            If CInt("0" & R!FNHSysQADetailId) <> 0 And FTPointName.Text <> "" Then

                '_Qry = "select top 1  * From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_DTH  WITH(NOLOCK) "
                '_Qry &= vbCrLf & " WHERE Isnull(FNSeqQCQty,0) = " & CInt(Me.FNQCActualQty.Value)
                '_Qry &= vbCrLf & " AND FNHSysStyleId=" & CInt(Me.FNHSysStyleId.Properties.Tag)
                '_Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
                ''_Qry &= vbCrLf & " AND FTPointName='" & _PointName.ToString & "'"
                '_Qry &= vbCrLf & " AND FTPointSubName='" & HI.UL.ULF.rpQuoted(FTPointName.Text) & "'"
                '_Qry &= vbCrLf & " AND FNHSysQADetailId=" & CInt(R!FNHSysQADetailId)
                '_Qry &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)
                '_Qry &= vbCrLf & " AND FDQADate=" & HI.UL.ULDate.FormatDateDB
                ''_Qry &= vbCrLf & " AND FNSeq=" & CInt(Me.FNQCActualQty.Value)  + 1
                '_Qry &= vbCrLf & " AND FNHourQty=" & CInt("0" & Me.FNHour.Value)



                'new
                _Qry = "Select Top 1 * From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_SubDetail  WITH(NOLOCK) "
                _Qry &= vbCrLf & "WHERE FNHSysStyleId=" & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
                _Qry &= vbCrLf & "AND FNHSysUnitSectId=" & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
                _Qry &= vbCrLf & "AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
                _Qry &= vbCrLf & "AND FDQADate=" & HI.UL.ULDate.FormatDateDB
                '_Qry &= vbCrLf & "AND FNHourNo=" & CInt("0" & Me.FNHour.Value)
                _Qry &= vbCrLf & "AND FNHourNo='" & _PInsTime & "'"
                _Qry &= vbCrLf & "AND FNSeq=" & CInt("0" & Me.FNQCActualQty.Value) + 1
                _Qry &= vbCrLf & "AND FTPointSubName='" & HI.UL.ULF.rpQuoted(Me.FTPointName.Text) & "'"
                _Qry &= vbCrLf & "AND FNHSysQADetailId=" & CInt(R!FNHSysQADetailId)
                'new
                If HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD).Rows.Count > 0 Then
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
            '   Elmt.Text = R!FTQADetailCode.ToString
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
        'SubTileControl.Visible = False
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
                    If Me.FNQCActualQty.Value >= Me.FNQtyQA.Value Then

                        If HI.MG.ShowMsg.mConfirmProcess("ทำการ QA ครบตามจำนวนการสุ่มตรวจแล้ว... ต้องการทำ QA เพิ่ม ใช่หรือไม่ ?", 1411150017) = False Then
                            e.Item.Checked = False
                            ClearOnSucc()
                            Exit Sub
                        Else
                            CalcPopup = New wPopupCalc
                            With CalcPopup
                                .ShowDialog()
                                If .StateEnter = True Then
                                    If .CalcValue <= Me.FNQtyIn.Value Then
                                        Me.FNQtyQA.Value = .CalcValue
                                    Else
                                        Me.FNQtyQA.Value = Me.FNQtyIn.Value
                                    End If

                                End If
                            End With
                        End If
                    End If

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
            _Cmd &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_SubDetail AS D WITH(NOLOCK)  LEFT OUTER JOIN  "
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQADetail AS Q WITH(NOLOCK)   ON D.FNHSysQADetailId = Q.FNHSysQADetailId "
            _Cmd &= vbCrLf & "WHERE FNHSysStyleId=" & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
            _Cmd &= vbCrLf & "And FNHSysUnitSectId=" & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
            _Cmd &= vbCrLf & "And FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            _Cmd &= vbCrLf & "AND FDQADate=" & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & "AND FNHourNo='" & _PInsTime & "'"
            _Cmd &= vbCrLf & "AND FNSeq=" & CInt("0" & Me.FNQCActualQty.Value) + 1
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Me.FNMinorQty.Value = Integer.Parse("0" & _oDt.Compute("Count(FTStateCtitical)", "FTStateCtitical = '0'"))
            Me.FNMajorQty.Value = Integer.Parse("0" & _oDt.Compute("Count(FTStateCtitical)", "FTStateCtitical = '1'"))
            Me.FNCtiticalQty.Value = Integer.Parse("0" & _oDt.Compute("Count(FTStateCtitical)", "FTStateCtitical = '2'"))

        Catch ex As Exception

        End Try
    End Sub


    Private _PicType As Integer = 0
    Private _FNSeq As Integer = 0
    Private _State As Integer = 0

    'Private Sub Delete_Detail(ByVal _FNHSysQADetailId As Integer, ByVal State As Boolean)
    '    Try
    '        Dim _Qry As String = ""
    '        _Qry = " Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_D"
    '        _Qry &= vbCrLf & " WHERE FNHSysStyleId=" & CInt(Me.FNHSysStyleId.Properties.Tag)
    '        _Qry &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)
    '        _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
    '        _Qry &= vbCrLf & " AND FDQADate=" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & " and FNHSysQADetailId=" & CInt(_FNHSysQADetailId)

    '        '_Qry &= vbCrLf & " AND FTPointName='" & _PointName.ToString & "'"
    '        _Qry &= vbCrLf & " AND FTPointSubName='" & HI.UL.ULF.rpQuoted(FTPointName.Text) & "'"
    '        _Qry &= vbCrLf & " AND FNDefectQty = 0"

    '        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)



    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Function SaveSubDetail(ByVal _FNHSysQADetailId As Integer, ByVal State As Boolean) As Boolean

    '    Try


    '        Dim _Qry As String = ""

    '        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
    '        HI.Conn.SQLConn.SqlConnectionOpen()
    '        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

    '        _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_D"
    '        _Qry &= vbCrLf & "SET FTUpdUser=N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '        _Qry &= vbCrLf & ",FDUpdDate =" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB

    '        If State Then
    '            _Qry &= vbCrLf & ",FNDefectQty=FNDefectQty+1"
    '        Else
    '            _Qry &= vbCrLf & ",FNDefectQty=FNDefectQty-1"
    '        End If

    '        _Qry &= vbCrLf & ",FTState='" & HI.UL.ULF.rpQuoted(IIf(State, "1", "0")) & "'"

    '        _Qry &= vbCrLf & " WHERE FNHSysStyleId=" & CInt(Me.FNHSysStyleId.Properties.Tag)
    '        _Qry &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)
    '        _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
    '        _Qry &= vbCrLf & " AND FDQADate=" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & " and FNHSysQADetailId=" & CInt(_FNHSysQADetailId)

    '        '_Qry &= vbCrLf & " AND FTPointName='" & _PointName.ToString & "'"
    '        _Qry &= vbCrLf & " AND FTPointSubName='" & HI.UL.ULF.rpQuoted(FTPointName.Text) & "'"



    '        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

    '            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_D "
    '            _Qry &= "(FTInsUser, FDInsDate, FTInsTime,FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate, FNHSysQADetailId, FTPointSubName, FNDefectQty,FTState )"
    '            _Qry &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
    '            _Qry &= vbCrLf & "," & CInt(Me.FNHSysStyleId.Properties.Tag)
    '            _Qry &= vbCrLf & "," & CInt(Me.FNHSysUnitSectId.Properties.Tag)
    '            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
    '            _Qry &= vbCrLf & "," & CInt(_FNHSysQADetailId)
    '            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPointName.Text) & "'"
    '            _Qry &= vbCrLf & ",1"
    '            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(IIf(State, "1", "0")) & "'"


    '            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                HI.Conn.SQLConn.Tran.Rollback()
    '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                Return False
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

    Private Function _TotalDefect() As Double
        Try

        Catch ex As Exception

        End Try
    End Function

    'Private Function SaveDetail_TW() As Boolean
    '    Try
    '        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
    '        HI.Conn.SQLConn.SqlConnectionOpen()
    '        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

    '        Dim _Qry As String = ""

    '        _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_DTW"
    '        _Qry &= vbCrLf & "SET FTUpdUser=N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '        _Qry &= vbCrLf & ",FDUpdDate =" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
    '        _Qry &= vbCrLf & ",FNQtyIn=" & CInt(Me.FNQtyIn.Value)
    '        _Qry &= vbCrLf & ",FNQtyQA=" & CInt(Me.FNQtyQA.Value)
    '        _Qry &= vbCrLf & ",FNQCActualQty=" & CInt(Me.FNQCActualQty.Value)

    '        _Qry &= vbCrLf & " WHERE FNHSysStyleId=" & CInt(FNHSysStyleId.Properties.Tag)
    '        _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
    '        _Qry &= vbCrLf & " AND FDQADate=" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & " AND FNSeq=" & CInt(Me.FNHour.Value)
    '        _Qry &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)

    '        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

    '            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_DTW (FTInsUser,FDInsDate,FTInsTime,FNHSysStyleId,FTOrderNo,FDQADate,FNSeq,FNHSysUnitSectId,FNQtyIn"
    '            _Qry &= ",FNQtyQA,FNQCActualQty)"
    '            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
    '            _Qry &= vbCrLf & "," & CInt(Me.FNHSysStyleId.Properties.Tag)
    '            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
    '            _Qry &= vbCrLf & "," & CInt(Me.FNHour.Value)
    '            _Qry &= vbCrLf & "," & CInt(Me.FNHSysUnitSectId.Properties.Tag)
    '            _Qry &= vbCrLf & "," & CInt(Me.FNQtyIn.Value)
    '            _Qry &= vbCrLf & "," & CInt(Me.FNQtyQA.Value)
    '            _Qry &= vbCrLf & "," & CInt(Me.FNQCActualQty.Value)

    '            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                HI.Conn.SQLConn.Tran.Rollback()
    '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                Return False
    '            End If
    '        End If

    '        HI.Conn.SQLConn.Tran.Commit()
    '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

    '        _CountQAQty += +1

    '        Return True
    '    Catch ex As Exception
    '        HI.Conn.SQLConn.Tran.Rollback()
    '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '        Return False
    '    End Try
    'End Function

    'Private Function GetTotalDefect() As Integer
    '    Try
    '        Dim _Qry As String = ""
    '        _Qry = "Select  sum(FNTotalDefect) AS FNTotalDefect"
    '        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_DTH WITH(NOLOCK) "
    '        _Qry &= vbCrLf & " WHERE FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)
    '        _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
    '        _Qry &= vbCrLf & " AND FDQADate=" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & " AND FTPointSubName='" & HI.UL.ULF.rpQuoted(Me.FTPointName.Text) & "'"
    '        _Qry &= vbCrLf & " AND FNHourQty=" & CInt(Me.FNHour.Value)
    '        _Qry &= vbCrLf & " AND FNHSysStyleId=" & CInt(Me.FNHSysStyleId.Properties.Tag)



    '        Return HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "0")
    '    Catch ex As Exception
    '        Return 0
    '    End Try
    'End Function


    'Private Function GetSubTotalDefect(ByVal _QADetailId As Integer) As Integer
    '    Try
    '        Dim _Qry As String = ""
    '        _Qry = "Select  sum(FNTotalDefect) AS FNTotalDefect"
    '        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_DTH WITH(NOLOCK) "
    '        _Qry &= vbCrLf & " WHERE FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)
    '        _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
    '        _Qry &= vbCrLf & " AND FDQADate=" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & " AND FTPointSubName='" & HI.UL.ULF.rpQuoted(Me.FTPointName.Text) & "'"
    '        _Qry &= vbCrLf & " AND FNHourQty=" & CInt(Me.FNHour.Value)
    '        _Qry &= vbCrLf & " AND FNHSysStyleId=" & CInt(Me.FNHSysStyleId.Properties.Tag)
    '        '_Qry &= vbCrLf & " AND FNHSysQADetailId=" & CInt(_QADetailId)



    '        Return HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "0")
    '    Catch ex As Exception
    '        Return 0
    '    End Try
    'End Function

    'Private Function DeleteDetail_Point(ByVal _QADetailId As Integer) As Boolean
    '    Try

    '        Dim _Qry As String = ""
    '        _Qry = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPoint_Detail "
    '        '_Qry &= vbCrLf & "SET FNTotalDefect=" & CInt(GetSubTotalDefect(CInt(_QADetailId))) + 1 'CInt(GetSubTotalDefect(CInt(_QADetailId)))
    '        ''_Qry &= vbCrLf & ",FTStateMSG=0"
    '        '_Qry &= vbCrLf & ",FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '        '_Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
    '        '_Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
    '        _Qry &= vbCrLf & "WHERE FNHSysStyleId=" & CInt(Me.FNHSysStyleId.Properties.Tag)
    '        _Qry &= vbCrLf & "AND FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)
    '        _Qry &= vbCrLf & "AND FDQADate=" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & " AND FTPointSubName='" & HI.UL.ULF.rpQuoted(Me.FTPointName.Text) & "'"
    '        _Qry &= vbCrLf & " AND FNTotalDefect=0"

    '        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)
    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function

    'Private Function SaveDetail_Point(ByVal _QADetailId As Integer, Optional _State As Boolean = True) As Boolean
    '    Try
    '        Dim _Qry As String = ""
    '        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
    '        HI.Conn.SQLConn.SqlConnectionOpen()
    '        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


    '        _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPoint_Detail "
    '        If _State Then
    '            _Qry &= vbCrLf & "SET FNTotalDefect=" & CInt(GetSubTotalDefect(CInt(_QADetailId))) + 1 'CInt(GetSubTotalDefect(CInt(_QADetailId)))
    '        Else
    '            _Qry &= vbCrLf & "SET FNTotalDefect=" & CInt(GetSubTotalDefect(CInt(_QADetailId))) - 1
    '        End If

    '        '_Qry &= vbCrLf & ",FTStateMSG=0"
    '        _Qry &= vbCrLf & ",FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '        _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
    '        _Qry &= vbCrLf & "WHERE FNHSysStyleId=" & CInt(Me.FNHSysStyleId.Properties.Tag)
    '        _Qry &= vbCrLf & "AND FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)
    '        _Qry &= vbCrLf & "AND FDQADate=" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & " AND FTPointSubName='" & HI.UL.ULF.rpQuoted(Me.FTPointName.Text) & "'"


    '        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

    '            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPoint_Detail"
    '            _Qry &= "(FTInsUser, FDInsDate, FTInsTime , FNHSysStyleId, FNHSysUnitSectId, FDQADate, FTPointSubName, FNTotalDefect )" 'FTStateMSG
    '            _Qry &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
    '            _Qry &= vbCrLf & "," & CInt(Me.FNHSysStyleId.Properties.Tag)
    '            _Qry &= vbCrLf & "," & CInt(Me.FNHSysUnitSectId.Properties.Tag)
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
    '            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPointName.Text) & "'"
    '            _Qry &= vbCrLf & "," & CInt(GetSubTotalDefect(CInt(_QADetailId))) + 1
    '            '_Qry &= vbCrLf & ",0"
    '            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                HI.Conn.SQLConn.Tran.Rollback()
    '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                Return False
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

    'Private Sub Delete_DetailTH(ByVal _FNHSysQADetailId As Integer)
    '    Try
    '        Dim _Qry As String = ""
    '        _Qry = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_DTH"


    '        _Qry &= vbCrLf & "WHERE FNHSysStyleId=" & CInt(Me.FNHSysStyleId.Properties.Tag)
    '        _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
    '        _Qry &= vbCrLf & " AND FDQADate=" & HI.UL.ULDate.FormatDateDB
    '        '_Qry &= vbCrLf & " AND FNSeq=1" ' & CInt(Me.FNQCActualQty.Value)
    '        _Qry &= vbCrLf & " AND FNSeq=" & CInt(Me.FNQCActualQty.Value) + 1
    '        _Qry &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)
    '        _Qry &= vbCrLf & " AND FNHourQty=" & CInt(Me.FNHour.Value)
    '        _Qry &= vbCrLf & "AND  FTPointSubName='" & HI.UL.ULF.rpQuoted(Me.FTPointName.Text) & "'"
    '        _Qry &= vbCrLf & " AND FNHSysQADetailId=" & CInt(_FNHSysQADetailId)

    '        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Function SaveDetail_TH(ByVal _FNHSysQADetailId As Integer) As Boolean
    '    Try
    '        '           SELECT  FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime
    '        ', FNHSysStyleId, FNHSysUnitSectId, FNHSysQADetailId, FTOrderNo, FDQADate, FNHourQty, FNSeq, FTPointSubName, 
    '        '                         FNTotalDefect, FTStateApp, FTStateMSG, FTStateReject
    '        'FROM            TPRODTQA_DTH
    '        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
    '        HI.Conn.SQLConn.SqlConnectionOpen()
    '        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

    '        Dim _Qry As String = ""
    '        _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_DTH"
    '        _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '        _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
    '        _Qry &= vbCrLf & ",FNTotalDefect=1"
    '        '_Qry &= vbCrLf & ",FTStateApp=1"
    '        '_Qry &= vbCrLf & ",FTStateMSG=0"
    '        _Qry &= vbCrLf & ",FTStateReject=" & CInt(_FTStateReject)

    '        _Qry &= vbCrLf & "WHERE FNHSysStyleId=" & CInt(Me.FNHSysStyleId.Properties.Tag)
    '        _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
    '        _Qry &= vbCrLf & " AND FDQADate=" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & " AND FNSeq=" & CInt(Me.FNQCActualQty.Value) + 1
    '        _Qry &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)
    '        _Qry &= vbCrLf & " AND FNHourQty=" & CInt(Me.FNHour.Value)
    '        _Qry &= vbCrLf & "AND  FTPointSubName='" & HI.UL.ULF.rpQuoted(Me.FTPointName.Text) & "'"
    '        _Qry &= vbCrLf & " AND FNHSysQADetailId=" & CInt(_FNHSysQADetailId)


    '        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_DTH"
    '            _Qry &= "(FTInsUser, FDInsDate, FTInsTime,FNHSysStyleId, FNHSysUnitSectId, FNHSysQADetailId, FTOrderNo, FDQADate, FNHourQty, FNSeq, FTPointSubName,FNTotalDefect,  FTStateReject,FNSeqQCQty)" 'FTStateApp, FTStateMSG,
    '            _Qry &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
    '            _Qry &= vbCrLf & "," & CInt(Me.FNHSysStyleId.Properties.Tag)
    '            _Qry &= vbCrLf & "," & CInt(Me.FNHSysUnitSectId.Properties.Tag)
    '            _Qry &= vbCrLf & "," & CInt(_FNHSysQADetailId)
    '            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
    '            _Qry &= vbCrLf & "," & CInt(Me.FNHour.Value)
    '            _Qry &= vbCrLf & "," & CInt(Me.FNQCActualQty.Value) + 1
    '            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPointName.Text) & "'"
    '            _Qry &= vbCrLf & ",1"
    '            '_Qry &= vbCrLf & ",0"
    '            '_Qry &= vbCrLf & ",0"
    '            _Qry &= vbCrLf & "," & CInt(_FTStateReject)
    '            _Qry &= vbCrLf & "," & CInt(Me.FNQCActualQty.Value)


    '            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                HI.Conn.SQLConn.Tran.Rollback()
    '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                Return False
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


    'Private Function SaveQA_H(ByVal _FNHSysQADetailId As Integer) As Boolean
    '    Try
    '        '          SELECT        TOP (200) FTInsUser, FDInsDate, FTInsTime,  FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate, FNQAQty, FNAcceptAbleQty, FNMainReject, FNSubReject, 
    '        '                         FTStateApp
    '        'FROM            TPRODTQA_H


    '        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
    '        HI.Conn.SQLConn.SqlConnectionOpen()
    '        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

    '        Dim _Qry As String = ""
    '        _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_H"
    '        _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '        _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
    '        _Qry &= vbCrLf & ",FNQAQty=" & CInt(Me.FNQtyQA.Value)
    '        '_Qry &= vbCrLf & ",FNMainReject=" & CInt(Me.fn
    '        '_Qry &= vbCrLf & ",FTStateMSG=0"
    '        '_Qry &= vbCrLf & ",FTStateReject=0"

    '        _Qry &= vbCrLf & "WHERE FNHSysStyleId=" & CInt(Me.FNHSysStyleId.Properties.Tag)
    '        _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
    '        _Qry &= vbCrLf & " AND FDQADate=" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)



    '        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_H"
    '            _Qry &= "(FTInsUser, FDInsDate, FTInsTime,  FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate, FNQAQty)"
    '            _Qry &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
    '            _Qry &= vbCrLf & "," & CInt(Me.FNHSysStyleId.Properties.Tag)
    '            _Qry &= vbCrLf & "," & CInt(Me.FNHSysUnitSectId.Properties.Tag)
    '            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
    '            _Qry &= vbCrLf & "," & CInt(Me.FNQtyQA.Value)



    '            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                HI.Conn.SQLConn.Tran.Rollback()
    '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                Return False
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


    'Private Function CreateGroupItemDetailPage(ByVal group As SampleDataGroup, ByVal child As PageGroup) As PageGroup
    '    Dim page As New GroupDetailPage(group, child)
    '    Dim pageGroup As PageGroup = page.PageGroup
    '    Dim document As BaseDocument = WindowsUIView.AddDocument(page)
    '    'pageGroup.Parent = TileContainer
    '    pageGroup.Items.Add(TryCast(document, Document))
    '    WindowsUIView.ContentContainers.Add(pageGroup)
    '    WindowsUIView.ActivateContainer(pageGroup)
    '    Return pageGroup
    'End Function

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

    'Private Function CreateTile(ByVal document As Document, ByVal item As SampleDataItem) As Tile
    '    Dim tile As New Tile()
    '    tile.Document = document
    '    tile.Group = item.GroupName
    '    tile.Tag = item


    '    tile.Elements.Add(CreateTileItemElement(item.Subtitle, TileItemContentAlignment.BottomLeft, Point.Empty, 9))
    '    tile.Elements.Add(CreateTileItemElement(item.Subtitle, TileItemContentAlignment.Manual, New Point(0, 100), 12))
    '    tile.Appearances.Normal.BackColor = Color.FromArgb(140, 140, 140)
    '    tile.Appearances.Hovered.BackColor = tile.Appearances.Normal.BackColor
    '    tile.Appearances.Selected.BackColor = tile.Appearances.Hovered.BackColor
    '    tile.Appearances.Normal.BorderColor = Color.FromArgb(140, 140, 140)
    '    tile.Appearances.Hovered.BorderColor = tile.Appearances.Normal.BorderColor
    '    tile.Appearances.Selected.BorderColor = tile.Appearances.Hovered.BorderColor
    '    AddHandler tile.Click, AddressOf tile_Click
    '    WindowsUIView.Tiles.Add(tile)
    '    TileContainer.Items.Add(tile)

    '    Return tile
    'End Function

    'Private Sub tile_Click(ByVal sender As Object, ByVal e As TileClickEventArgs)
    '    Dim page As PageGroup = (TryCast((TryCast(e.Tile, Tile)).ActivationTarget, PageGroup))
    '    If page IsNot Nothing Then
    '        page.Parent = TileContainer
    '        page.SetSelected((TryCast(e.Tile, Tile)).Document)
    '    End If
    'End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub FNHSysStyleId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysStyleId.EditValueChanged
        Try
            Me.FTOrderNo.Text = ""
            Me.FTOrderNo.Properties.Tag = 0

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

                'Me.LoadData()
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
                            Me.FTImage.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\MODEL\" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "\" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "_" & _Step.ToString & ".JPG")

                            If Me.FTImage.Image Is Nothing Then
                                Me.FTImage.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage1))
                            End If
                        Catch ex As Exception
                            Me.FTImage.Image = Nothing
                        End Try

                    Case 1
                        Try
                            Me.FTImage.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\MODEL\" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "\" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "_" & _Step.ToString & ".JPG")
                            If Me.FTImage.Image Is Nothing Then
                                Me.FTImage.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage2))
                            End If

                        Catch ex As Exception
                            Me.FTImage.Image = Nothing
                        End Try
                    Case 2
                        Try
                            Me.FTImage.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\MODEL\" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "\" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "_" & _Step.ToString & ".JPG")
                            If Me.FTImage.Image Is Nothing Then
                                Me.FTImage.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage3))
                            End If

                        Catch ex As Exception
                            Me.FTImage.Image = Nothing
                        End Try
                    Case 3
                        Try
                            Me.FTImage.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\MODEL\" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "\" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "_" & _Step.ToString & ".JPG")
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
        _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTStylePoint AS A WITH(NOLOCK) LEFT OUTER JOIN "
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B WITH(NOLOCK) ON A.FNHSysStyleId = B.FNHSysStyleId "
        _Qry &= vbCrLf & "Where A.FTStyleCode='" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "'"
        _Qry &= vbCrLf & "AND A.FTPicType=" & CInt(_Type)
        _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
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

            If e.Button = System.Windows.Forms.MouseButtons.Left Then
                Dim _Qry As String = ""
                Dim _oDt As DataTable
                _Qry = "SELECT top 1   FTPointName,   FNSeq, FNPointX, FNPointY, FTPicType, FNPicHeight, FNPicWidth,FTRemark,FTPicName"
                _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTStylePoint"
                _Qry &= vbCrLf & "Where FTStyleCode='" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "'"
                _Qry &= vbCrLf & "AND FTPicType=" & CInt(_Step)
                _Qry &= vbCrLf & "AND FNSeq=" & CInt(CType(sender, ZBobb.AlphaBlendTextBox).Name.ToString)

                _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
                For Each R As DataRow In _oDt.Rows

                    Me.FTImage.Image = _ResizeImage(HI.UL.ULImage.LoadImage(_Path & "\Images\POINTMODEL\" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "\" & R!FTPicName.ToString))
                    'Me.FTImage.Image = _ResizeImage(HI.UL.ULImage.LoadImage(_Path & "\Images\POINTMODEL\642321SP15\" & R!FTPicName.ToString))
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



            'FTImage.Controls.Clear()


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
                  




                    '_Qry = "select top 1  * From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_DTH  WITH(NOLOCK) "
                    '_Qry &= vbCrLf & " WHERE Isnull(FNSeqQCQty,0) = " & CInt(Me.FNQCActualQty.Value)
                    '_Qry &= vbCrLf & " AND FNHSysStyleId=" & CInt(Me.FNHSysStyleId.Properties.Tag)
                    '_Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
                    ''_Qry &= vbCrLf & " AND FTPointName='" & _PointName.ToString & "'"
                    '_Qry &= vbCrLf & " AND FTPointSubName='" & HI.UL.ULF.rpQuoted(textBoxS.Name.ToString) & "'"
                    ' ''_Qry &= vbCrLf & " AND D.FNHSysQADetailId=" & CInt(R!FNHSysQADetailId)
                    '_Qry &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)
                    '_Qry &= vbCrLf & " AND FDQADate=" & HI.UL.ULDate.FormatDateDB
                    '_Qry &= vbCrLf & " AND FNSeq=" & CInt(Me.FNQCActualQty.Value) + 1
                    '_Qry &= vbCrLf & " AND FNHourQty=" & CInt("0" & Me.FNHour.Value)


                    _Qry = "Select Top 1 * From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_SubDetail WITH(NOLOCK) "
                    _Qry &= vbCrLf & "WHERE FNHSysStyleId=" & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
                    _Qry &= vbCrLf & "AND FNHSysUnitSectId=" & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
                    _Qry &= vbCrLf & "AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
                    _Qry &= vbCrLf & "AND FDQADate=" & HI.UL.ULDate.FormatDateDB
                    '_Qry &= vbCrLf & "AND FNHourNo=" & CInt("0" & Me.FNHour.Value)
                    _Qry &= vbCrLf & "AND FNHourNo='" & _PInsTime & "'"
                    _Qry &= vbCrLf & "AND FNSeq=" & CInt("0" & Me.FNQCActualQty.Value) + 1
                    _Qry &= vbCrLf & "AND FTPointSubName='" & HI.UL.ULF.rpQuoted(textBoxS.Name.ToString) & "'"


                    If HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD).Rows.Count > 0 Then
                        _PState = "1"
                    Else
                        _PState = "0"
                    End If

                    textBoxS.BackColor = IIf(_PState = "1", Color.Blue, Color.White)

                    AddHandler textBoxS.MouseDown, AddressOf ObjFocus
                Next
                'Me.FTImagePoint.Controls.Add(textBoxS)
            Next
            'End If

        Catch ex As Exception

        End Try
    End Sub


    Private Sub ObjFocus(sender As Object, e As MouseEventArgs)
        Try
            'textBoxS.BackColor = Color.

            If e.Button = System.Windows.Forms.MouseButtons.Left Then

                Dim _Qry As String = ""


                FTPointName.Text = sender.name.ToString



                '_Qry = "select top 1  * From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_DTH  WITH(NOLOCK) "

                '_Qry &= vbCrLf & " WHERE Isnull(FNSeqQCQty,0) = " & CInt(Me.FNQCActualQty.Value)

                '_Qry &= vbCrLf & " AND FNHSysStyleId=" & CInt(Me.FNHSysStyleId.Properties.Tag)
                '_Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
                ''_Qry &= vbCrLf & " AND FTPointName='" & _PointName.ToString & "'"
                '_Qry &= vbCrLf & " AND FTPointSubName='" & HI.UL.ULF.rpQuoted(FTPointName.Text) & "'"
                ' ''_Qry &= vbCrLf & " AND D.FNHSysQADetailId=" & CInt(R!FNHSysQADetailId)
                '_Qry &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)
                '_Qry &= vbCrLf & " AND FDQADate=" & HI.UL.ULDate.FormatDateDB
                '_Qry &= vbCrLf & " AND FNSeq=" & CInt(Me.FNQCActualQty.Value) + 1
                '_Qry &= vbCrLf & " AND FNHourQty=" & CInt("0" & Me.FNHour.Value)

                _Qry = "Select Top 1 * From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_SubDetail WITH(NOLOCK) "
                _Qry &= vbCrLf & "WHERE FNHSysStyleId=" & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
                _Qry &= vbCrLf & "AND FNHSysUnitSectId=" & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
                _Qry &= vbCrLf & "AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
                _Qry &= vbCrLf & "AND FDQADate=" & HI.UL.ULDate.FormatDateDB
                '_Qry &= vbCrLf & "AND FNHourNo=" & CInt("0" & Me.FNHour.Value)
                _Qry &= vbCrLf & "AND FNHourNo='" & _PInsTime & "'"
                _Qry &= vbCrLf & "AND FNSeq=" & CInt("0" & Me.FNQCActualQty.Value) + 1
                _Qry &= vbCrLf & "AND FTPointSubName='" & HI.UL.ULF.rpQuoted(Me.FTPointName.Text) & "'"


                If HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD).Rows.Count > 0 Then
                    _PState = "1"
                Else
                    _PState = "0"
                End If

                '_PState = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "0")

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
                        CalcPopup = New wPopupCalc
                        With CalcPopup
                            .ShowDialog()
                            If .StateEnter = True Then
                                If .CalcValue <= Me.FNQtyIn.Value Then
                                    Me.FNQtyQA.Value = .CalcValue
                                Else
                                    Me.FNQtyQA.Value = Me.FNQtyIn.Value
                                End If

                            End If
                        End With
                    End If
                End If

                _FTStateReject = 0
                _SetActualCheck()


                'SaveNew
                Me.SaveDataHeader()
                If (Me.FNMajorQty.Value + Me.FNMinorQty.Value + Me.FNCtiticalQty.Value) > 0 Then
                    Me.SaveDataDetail("1")
                Else
                    Me.SaveDataDetail("0")
                End If

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
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Function Verify() As Boolean

        If Me.FNHSysStyleId.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FNHSysStyleId_lbl.Text)
            FNHSysStyleId.Focus()
            Return False
        End If

        If Me.FNHSysUnitSectId.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FNHSysUnitSectId_lbl.Text)
            FNHSysUnitSectId.Focus()
            Return False
        End If

        If Me.FTOrderNo.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FTOrderNo_lbl.Text)
            FTOrderNo.Focus()
            Return False
        End If
        'If Me.FNHour.Value = 0 Then
        '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FNHour_lbl.Text)
        '    FNHour.Focus()
        '    Return False
        'End If

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








        Return True
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

    'Private Function _GetRowState(ByVal _Style As Integer, ByVal _OrderNo As String, ByVal _Hour As Integer, ByVal _UnitSectId As Integer) As Boolean
    '    Try
    '        Dim _Qry As String = ""
    '        _Qry = "Select Top 1 *  From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_DTW  WITH(NOLOCK)"
    '        _Qry &= vbCrLf & " WHERE FNHSysStyleId=" & CInt(_Style)
    '        _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
    '        _Qry &= vbCrLf & " AND FDQADate=" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & " AND FNSeq=1" ' & CInt(_Hour)
    '        _Qry &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(_UnitSectId)
    '        If HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD, "").Rows.Count > 0 Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function


    Private Function _GetRowState_Detail(ByVal _Style As Integer, ByVal _OrderNo As String, ByVal _Hour As Integer, ByVal _UnitSectId As Integer) As Boolean
        Try
            Dim _Qry As String = ""
            Dim _State As String = ""
            '_Qry = "Select Top 1  *  From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_DTH  WITH(NOLOCK)"
            '_Qry &= vbCrLf & " WHERE FNHSysStyleId=" & CInt(_Style)
            '_Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
            '_Qry &= vbCrLf & " AND FDQADate=" & HI.UL.ULDate.FormatDateDB
            '_Qry &= vbCrLf & " AND FNSeq=" & CInt(Me.FNQCActualQty.Value) + 1
            '_Qry &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(_UnitSectId)
            '_Qry &= vbCrLf & " AND FNHourQty=" & CInt(Me.FNHour.Value)

            _Qry = "Select Top 1 * From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_SubDetail WITH(NOLOCK) "
            _Qry &= vbCrLf & "WHERE FNHSysStyleId=" & CInt("0" & _Style)
            _Qry &= vbCrLf & "AND FNHSysUnitSectId=" & CInt("0" & _UnitSectId)
            _Qry &= vbCrLf & "AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
            _Qry &= vbCrLf & "AND FDQADate=" & HI.UL.ULDate.FormatDateDB
            '_Qry &= vbCrLf & "AND FNHourNo=" & CInt("0" & Me.FNHour.Value)
            _Qry &= vbCrLf & "AND FNHourNo='" & _PInsTime & "'"
            _Qry &= vbCrLf & "AND FNSeq=" & CInt("0" & Me.FNQCActualQty.Value) + 1
            '_Qry &= vbCrLf & "AND FTPointSubName='" & HI.UL.ULF.rpQuoted(Me.FTPointName.Text) & "'"

            If HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD).Rows.Count > 0 Then
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
        If _GetRowState_Detail(CInt("0" & Me.FNHSysStyleId.Properties.Tag), Me.FTOrderNo.Text, Me.FNHour.Value, CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)) Then
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
            '**************************

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
        If _GetRowState_Detail(CInt("0" & Me.FNHSysStyleId.Properties.Tag), Me.FTOrderNo.Text, Me.FNHour.Value, CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)) Then
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


    'Private Function UpdateMajor() As Boolean
    '    Try
    '        Dim _Qtr As String = ""

    '        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
    '        HI.Conn.SQLConn.SqlConnectionOpen()
    '        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

    '        Dim _Qry As String = ""

    '        _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_DTW"
    '        _Qry &= vbCrLf & "SET FTUpdUser=N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '        _Qry &= vbCrLf & ",FDUpdDate =" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
    '        '_Qry &= vbCrLf & ",FNQtyIn=" & CInt(Me.FNQtyIn.Value)
    '        '_Qry &= vbCrLf & ",FNQtyQA=" & CInt(Me.FNQtyQA.Value)
    '        _Qry &= vbCrLf & ",FNQCActualQty=" & CInt(Me.FNQCActualQty.Value)

    '        _Qry &= vbCrLf & ",FNMainReject=" & CInt(Me.FNMajorQty.Value)

    '        _Qry &= vbCrLf & " WHERE FNHSysStyleId=" & CInt(FNHSysStyleId.Properties.Tag)
    '        _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
    '        _Qry &= vbCrLf & " AND FDQADate=" & HI.UL.ULDate.FormatDateDB

    '        _Qry &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)



    '        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

    '            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_DTW (FTInsUser,FDInsDate,FTInsTime,FNHSysStyleId,FTOrderNo,FDQADate,FNSeq,FNHSysUnitSectId,FNQtyIn"
    '            _Qry &= ",FNQtyQA,FNQCActualQty,FNMainReject)"
    '            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
    '            _Qry &= vbCrLf & "," & CInt(Me.FNHSysStyleId.Properties.Tag)
    '            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
    '            _Qry &= vbCrLf & ",1" ' & CInt(Me.FNHour.Value)
    '            _Qry &= vbCrLf & "," & CInt(Me.FNHSysUnitSectId.Properties.Tag)
    '            _Qry &= vbCrLf & "," & CInt(Me.FNQtyIn.Value)
    '            _Qry &= vbCrLf & "," & CInt(Me.FNQtyQA.Value)
    '            _Qry &= vbCrLf & "," & CInt(Me.FNQCActualQty.Value)
    '            _Qry &= vbCrLf & "," & CInt(Me.FNMajorQty.Value)

    '            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                HI.Conn.SQLConn.Tran.Rollback()
    '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                Return False
    '            End If
    '        End If



    '        _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_DTH"
    '        _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '        _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
    '        '_Qry &= vbCrLf & ",FNTotalDefect=1"
    '        '_Qry &= vbCrLf & ",FTStateApp=1"
    '        '_Qry &= vbCrLf & ",FTStateMSG=0"
    '        _Qry &= vbCrLf & ",FTStateReject=" & CInt(_FTStateReject)

    '        _Qry &= vbCrLf & "WHERE FNHSysStyleId=" & CInt(Me.FNHSysStyleId.Properties.Tag)
    '        _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
    '        _Qry &= vbCrLf & " AND FDQADate=" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & " AND FNSeq=" & CInt(Me.FNQCActualQty.Value)
    '        _Qry &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)
    '        _Qry &= vbCrLf & " AND FNHourQty=" & CInt(Me.FNHour.Value)
    '        '_Qry &= vbCrLf & "AND  FTPointSubName='" & HI.UL.ULF.rpQuoted(Me.PointName_lbl.Text) & "'"
    '        '_Qry &= vbCrLf & " AND FNHSysQADetailId=" & CInt(_FNHSysQADetailId)
    '        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '            HI.Conn.SQLConn.Tran.Rollback()
    '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '            Return False
    '        End If



    '        _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_H"
    '        _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '        _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
    '        _Qry &= vbCrLf & ",FNQAQty=" & CInt(Me.FNQtyQA.Value)

    '        _Qry &= vbCrLf & ",FNMainReject=" & CInt(Me.FNMajorQty.Value)

    '        _Qry &= vbCrLf & "WHERE FNHSysStyleId=" & CInt(Me.FNHSysStyleId.Properties.Tag)
    '        _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
    '        _Qry &= vbCrLf & " AND FDQADate=" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)
    '        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
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

    'Private Function UpdateMinor() As Boolean
    '    Try
    '        Dim _Qtr As String = ""

    '        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
    '        HI.Conn.SQLConn.SqlConnectionOpen()
    '        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

    '        Dim _Qry As String = ""

    '        _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_DTW"
    '        _Qry &= vbCrLf & "SET FTUpdUser=N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '        _Qry &= vbCrLf & ",FDUpdDate =" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
    '        '_Qry &= vbCrLf & ",FNQtyIn=" & CInt(Me.FNQtyIn.Value)
    '        '_Qry &= vbCrLf & ",FNQtyQA=" & CInt(Me.FNQtyQA.Value)
    '        _Qry &= vbCrLf & ",FNQCActualQty=" & CInt(Me.FNQCActualQty.Value)

    '        _Qry &= vbCrLf & ",FNSubReject=" & CInt(Me.FNMinorQty.Value)

    '        _Qry &= vbCrLf & " WHERE FNHSysStyleId=" & CInt(FNHSysStyleId.Properties.Tag)
    '        _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
    '        _Qry &= vbCrLf & " AND FDQADate=" & HI.UL.ULDate.FormatDateDB

    '        _Qry &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)



    '        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

    '            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_DTW (FTInsUser,FDInsDate,FTInsTime,FNHSysStyleId,FTOrderNo,FDQADate,FNSeq,FNHSysUnitSectId,FNQtyIn"
    '            _Qry &= ",FNQtyQA,FNQCActualQty,FNSubReject)"
    '            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
    '            _Qry &= vbCrLf & "," & CInt(Me.FNHSysStyleId.Properties.Tag)
    '            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
    '            _Qry &= vbCrLf & ",1" ' & CInt(Me.FNHour.Value)
    '            _Qry &= vbCrLf & "," & CInt(Me.FNHSysUnitSectId.Properties.Tag)
    '            _Qry &= vbCrLf & "," & CInt(Me.FNQtyIn.Value)
    '            _Qry &= vbCrLf & "," & CInt(Me.FNQtyQA.Value)
    '            _Qry &= vbCrLf & "," & CInt(Me.FNQCActualQty.Value)
    '            _Qry &= vbCrLf & "," & CInt(Me.FNMinorQty.Value)

    '            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                HI.Conn.SQLConn.Tran.Rollback()
    '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                Return False
    '            End If
    '        End If


    '        _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_DTH"
    '        _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '        _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
    '        '_Qry &= vbCrLf & ",FNTotalDefect=1"
    '        '_Qry &= vbCrLf & ",FTStateApp=1"
    '        '_Qry &= vbCrLf & ",FTStateMSG=0"
    '        _Qry &= vbCrLf & ",FTStateReject=" & CInt(_FTStateReject)

    '        _Qry &= vbCrLf & "WHERE FNHSysStyleId=" & CInt(Me.FNHSysStyleId.Properties.Tag)
    '        _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
    '        _Qry &= vbCrLf & " AND FDQADate=" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & " AND FNSeq=" & CInt(Me.FNQCActualQty.Value)
    '        _Qry &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)
    '        _Qry &= vbCrLf & " AND FNHourQty=" & CInt(Me.FNHour.Value)
    '        '_Qry &= vbCrLf & "AND  FTPointSubName='" & HI.UL.ULF.rpQuoted(Me.PointName_lbl.Text) & "'"
    '        '_Qry &= vbCrLf & " AND FNHSysQADetailId=" & CInt(_FNHSysQADetailId)
    '        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '            HI.Conn.SQLConn.Tran.Rollback()
    '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '            Return False
    '        End If




    '        _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_H"
    '        _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '        _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
    '        _Qry &= vbCrLf & ",FNQAQty=" & CInt(Me.FNQtyQA.Value)

    '        _Qry &= vbCrLf & ",FNSubReject=" & CInt(Me.FNMinorQty.Value)

    '        _Qry &= vbCrLf & "WHERE FNHSysStyleId=" & CInt(Me.FNHSysStyleId.Properties.Tag)
    '        _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
    '        _Qry &= vbCrLf & " AND FDQADate=" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)
    '        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
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

    'Private Function UpdateAndon() As Boolean
    '    Try
    '        Dim _Qtr As String = ""

    '        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
    '        HI.Conn.SQLConn.SqlConnectionOpen()
    '        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

    '        Dim _Qry As String = ""

    '        _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_DTW"
    '        _Qry &= vbCrLf & "SET FTUpdUser=N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '        _Qry &= vbCrLf & ",FDUpdDate =" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
    '        '_Qry &= vbCrLf & ",FNQtyIn=" & CInt(Me.FNQtyIn.Value)
    '        '_Qry &= vbCrLf & ",FNQtyQA=" & CInt(Me.FNQtyQA.Value)
    '        '_Qry &= vbCrLf & ",FNQCActualQty=" & CInt(Me.FNQCActualQty.Value)

    '        _Qry &= vbCrLf & ",FNAndon=" & CInt(Me.FNAndon.Value)

    '        _Qry &= vbCrLf & " WHERE FNHSysStyleId=" & CInt(FNHSysStyleId.Properties.Tag)
    '        _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
    '        _Qry &= vbCrLf & " AND FDQADate=" & HI.UL.ULDate.FormatDateDB

    '        _Qry &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)



    '        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

    '            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_DTW (FTInsUser,FDInsDate,FTInsTime,FNHSysStyleId,FTOrderNo,FDQADate,FNSeq,FNHSysUnitSectId,FNQtyIn"
    '            _Qry &= ",FNQtyQA,FNQCActualQty,FNAndon)"
    '            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
    '            _Qry &= vbCrLf & "," & CInt(Me.FNHSysStyleId.Properties.Tag)
    '            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
    '            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
    '            _Qry &= vbCrLf & ",1" ' & CInt(Me.FNHour.Value)
    '            _Qry &= vbCrLf & "," & CInt(Me.FNHSysUnitSectId.Properties.Tag)
    '            _Qry &= vbCrLf & "," & CInt(Me.FNQtyIn.Value)
    '            _Qry &= vbCrLf & "," & CInt(Me.FNQtyQA.Value)
    '            _Qry &= vbCrLf & "," & CInt(Me.FNQCActualQty.Value)
    '            _Qry &= vbCrLf & "," & CInt(Me.FNAndon.Value)

    '            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                HI.Conn.SQLConn.Tran.Rollback()
    '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                Return False
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


    'Private Function UpdateSubMinor() As Boolean
    '    Try
    '        Dim _Qtr As String = ""

    '        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
    '        HI.Conn.SQLConn.SqlConnectionOpen()
    '        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

    '        Dim _Qry As String = ""



    '        _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_DTH"
    '        _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '        _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
    '        '_Qry &= vbCrLf & ",FNTotalDefect=1"
    '        '_Qry &= vbCrLf & ",FTStateApp=1"
    '        '_Qry &= vbCrLf & ",FTStateMSG=0"
    '        _Qry &= vbCrLf & ",FTStateReject=" & CInt(_FTStateReject)

    '        _Qry &= vbCrLf & "WHERE FNHSysStyleId=" & CInt(Me.FNHSysStyleId.Properties.Tag)
    '        _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
    '        _Qry &= vbCrLf & " AND FDQADate=" & HI.UL.ULDate.FormatDateDB
    '        _Qry &= vbCrLf & " AND FNSeq=" & CInt(Me.FNQCActualQty.Value)
    '        _Qry &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)
    '        _Qry &= vbCrLf & " AND FNHourQty=" & CInt(Me.FNHour.Value)
    '        '_Qry &= vbCrLf & "AND  FTPointSubName='" & HI.UL.ULF.rpQuoted(Me.PointName_lbl.Text) & "'"
    '        '_Qry &= vbCrLf & " AND FNHSysQADetailId=" & CInt(_FNHSysQADetailId)
    '        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
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
                Call GetMn()
                'new
            End If

            Me.FTPointName.Text = ""
            Un_State = False

        Catch ex As Exception

        End Try
    End Sub

    Private Function _GetConfigSample() As Double
        Try
            Dim _Qry As String = ""
            _Qry = "Select Top 1 FNSampleQty "
            _Qry &= vbCrLf & "From TPRODMConfigQA WITH(NOLOCK)"
            _Qry &= vbCrLf & "WHERE FNStartQty <=" & CDbl(Me.FNQtyIn.Value)
            _Qry &= vbCrLf & " and FNEndQty >=" & CDbl(Me.FNQtyIn.Value)
            Return HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private _OverDefectQty As Double = 0
    Private Function _GetConfigAccept() As Double
        Try
            Dim _Qry As String = ""
            _Qry = "Select Top 1 FNAcceptQty "
            _Qry &= vbCrLf & "From TPRODMConfigQA WITH(NOLOCK)"
            _Qry &= vbCrLf & "WHERE FNStartQty <=" & CDbl(Me.FNQtyIn.Value)
            _Qry &= vbCrLf & " and FNEndQty >=" & CDbl(Me.FNQtyIn.Value)
            Return HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Sub FNQtyIn_EditValueChanged(sender As Object, e As EventArgs) Handles FNQtyIn.EditValueChanged
        Try
            If FNQtyIn.Value > 0 Then
                _PInsTime = HI.Conn.SQLConn.GetField(" Select " & HI.UL.ULDate.FormatTimeDB, Conn.DB.DataBaseName.DB_PROD, "")
                _PInsTime = Microsoft.VisualBasic.Left(Replace(_PInsTime, ":", ""), 4)
            End If
            Me.FNHour_EditValueChanged()
            Me.FNQtyQA.Value = _GetConfigSample()
            _OverDefectQty = _GetConfigAccept()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNHour_EditValueChanged()
        Try
            LoadImangeStyle(CInt("0" & FNHSysStyleId.Properties.Tag), 0)

            Dim _Qry As String = ""
            Dim _oDt As DataTable

            _Qry = "Select Top 1   FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate, FNHourNo, FNQAInQty, FNQAAqlQty, FNQAActualQty, FNMajorQty, FNMinorQty, Isnull(FNAndon,0) AS FNAndon"
            _Qry &= vbCrLf & "FROM   TPRODTQA WITH(NOLOCK) "
            _Qry &= vbCrLf & "WHERE FDQADate=" & HI.UL.ULDate.FormatDateDB
            _Qry &= vbCrLf & "AND FNHSysStyleId=" & CInt(Me.FNHSysStyleId.Properties.Tag)
            _Qry &= vbCrLf & "AND FNHSysUnitSectId=" & CInt(Me.FNHSysUnitSectId.Properties.Tag)
            _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            '_Qry &= vbCrLf & " AND FNHourNo=" & CInt(Me.FNHour.Value)
            _Qry &= vbCrLf & " AND FNHourNo='" & _PInsTime & "'"


            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            If _oDt.Rows.Count > 0 Then
                For Each R As DataRow In _oDt.Rows
                    'Me.FNQtyIn.Value = CInt(R!FNQAInQty)
                    Me.FNQtyQA.Value = CInt(R!FNQAAqlQty)
                    Me.FNQCActualQty.Value = CInt(R!FNQAActualQty)
                    Me.FNMajorQty.Value = CInt(R!FNMajorQty)
                    Me.FNMinorQty.Value = CInt(R!FNMinorQty)
                    Me.FNAndon.Value = CInt(R!FNAndon)

                Next
            Else
                'Me.FNQtyIn.Value = 0
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
        Try
            Me.FNHSysStyleId.Text = ""
            Me.FNHSysStyleId.Properties.Tag = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTOrderNo.EditValueChanged
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

    Private CalcPopup As wPopupCalc
 
    Private Sub FNHour_MouseClick(sender As Object, e As MouseEventArgs) Handles FNHour.MouseDown
        Try
            CalcPopup = New wPopupCalc
            With CalcPopup
                .ShowDialog()
                If .StateEnter = True Then
                    Me.FNHour.Value = .CalcValue
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNQtyIn_MouseClick(sender As Object, e As MouseEventArgs) Handles FNQtyIn.MouseDown
        Try
            CalcPopup = New wPopupCalc
            With CalcPopup
                .ShowDialog()
                If .StateEnter = True Then
                    Me.FNQtyIn.Value = .CalcValue
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    'Private _FEditQA As wEditQA
    Private _FQADailyReport As wDailyQAReport
    Private Sub ocmEditQA_Click(sender As Object, e As EventArgs) Handles ocmEditQA.Click
        Try
            For Each f As Form In Application.OpenForms
                If TypeOf f Is wDailyQAReport Then
                    f.Activate()
                    Exit Sub
                End If
            Next


            Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
            HI.ST.SysInfo.MenuName = "MnuQADailyReport"
            'Dim _WShow As New wShowData(_WformPo, _PurchaseNo)
            'HI.ST.SysInfo.MenuName = _TmpMenu


            _FQADailyReport = New wDailyQAReport
            HI.TL.HandlerControl.AddHandlerObj(_FQADailyReport)
            Dim oSysLang As New ST.SysLanguage
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _FQADailyReport.Name.ToString.Trim, _FQADailyReport)

            With _FQADailyReport
                .FNHSysUnitSectId.Text = Me.FNHSysUnitSectId.Text
                .FNHSysStyleId.Text = Me.FNHSysStyleId.Text
                .FTOrderNo.Text = Me.FTOrderNo.Text

                .FDSDate.DateTime = HI.Conn.SQLConn.GetField("select  " & HI.UL.ULDate.FormatDateDB, Conn.DB.DataBaseName.DB_PROD, "")
                .FDEDate.DateTime = HI.Conn.SQLConn.GetField("select   " & HI.UL.ULDate.FormatDateDB, Conn.DB.DataBaseName.DB_PROD, "")
                .MdiParent = Me.ParentForm
                .WindowState = FormWindowState.Maximized
                .StateLoad = True
                .Show()
            End With
        Catch ex As Exception
        End Try
    End Sub



    Private Function SaveDataHeader() As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable


            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA"
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
            _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            _Cmd &= vbCrLf & " AND FDQADate =" & HI.UL.ULDate.FormatDateDB
            '_Cmd &= vbCrLf & " AND FNHourNo=" & CInt("0" & Me.FNHour.Value)
            _Cmd &= vbCrLf & " AND FNHourNo='" & _PInsTime & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA  "
                _Cmd &= "(FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate, FNHourNo, FNQAInQty, FNQAAqlQty, FNQAActualQty, FNMajorQty, FNMinorQty)" ', FNAndon
                _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & "," & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
                _Cmd &= vbCrLf & "," & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ",'" & _PInsTime & "'"
                _Cmd &= vbCrLf & "," & CInt("0" & Me.FNQtyIn.Value)
                _Cmd &= vbCrLf & "," & CInt("0" & Me.FNQtyQA.Value)
                _Cmd &= vbCrLf & "," & CInt("0" & Me.FNQCActualQty.Value)
                _Cmd &= vbCrLf & "," & CInt("0" & Me.FNMajorQty.Value)
                _Cmd &= vbCrLf & "," & CInt("0" & Me.FNMinorQty.Value)
                '_Cmd &= vbCrLf & "," & CInt("0" & Me.FNAndon.Value)
                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If
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

    Private Function SaveDataDetail(ByVal _StateReject As String) As Boolean
        Try
            Dim _Cmd As String = ""
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_Detail"
            _Cmd &= vbCrLf & "Set FTInsUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & ",FDInsDate =" & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & ",FTInsTime =" & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & ",FTStateReject ='" & _StateReject & "'"
            _Cmd &= vbCrLf & "WHERE FNHSysStyleId=" & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
            _Cmd &= vbCrLf & "AND FNHSysUnitSectId=" & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
            _Cmd &= vbCrLf & "AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            _Cmd &= vbCrLf & "AND FDQADate=" & HI.UL.ULDate.FormatDateDB
            '_Cmd &= vbCrLf & "AND FNHourNo=" & CInt("0" & Me.FNHour.Value)
            _Cmd &= vbCrLf & "AND FNHourNo='" & _PInsTime & "'"
            _Cmd &= vbCrLf & "AND FNSeq=" & CInt("0" & Me.FNQCActualQty.Value)
            '_Cmd &= vbCrLf & "AND FTPointSubName='" & HI.UL.ULF.rpQuoted(Me.FTPointName.Text) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_Detail"
                _Cmd &= " (FTInsUser, FDInsDate, FTInsTime , FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate, FNHourNo, FNSeq,  FTStateReject)"
                _Cmd &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & "," & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
                _Cmd &= vbCrLf & "," & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ",'" & _PInsTime & "'"
                _Cmd &= vbCrLf & "," & CInt("0" & Me.FNQCActualQty.Value)
                '_Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPointName.Text) & "'"
                _Cmd &= vbCrLf & ",'" & _StateReject & "'"


                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            _Cmd = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_CREATE_DATA_QualityAssurance " & Val(HI.ST.SysInfo.CmpID) & "," & Val(Me.FNHSysStyleId.Properties.Tag.ToString) & "," & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString) & ",'" & HI.UL.ULF.rpQuoted(FNHSysUnitSectId.Text.Trim()) & "','" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "','" & _PInsTime & "'," & Val(Me.FNQCActualQty.Value) & ",0"
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN)

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
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA"
            _Cmd &= vbCrLf & "Set FTInsUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & ",FDInsDate =" & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & ",FTInsTime =" & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & ",FNAndon = " & CInt("0" & Me.FNAndon.Value)
            _Cmd &= vbCrLf & "WHERE FNHSysStyleId=" & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
            _Cmd &= vbCrLf & " AND FNHSysUnitSectId =" & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
            _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            _Cmd &= vbCrLf & " AND FDQADate =" & HI.UL.ULDate.FormatDateDB
            '_Cmd &= vbCrLf & " AND FNHourNo=" & CInt("0" & Me.FNHour.Value)
            _Cmd &= vbCrLf & " AND FNHourNo='" & _PInsTime & "'"
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

    Private Function UndoReject() As Boolean
        Try
            Dim _Cmd As String = ""
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            _Cmd = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_Detail"
            _Cmd &= vbCrLf & "WHERE FNHSysStyleId=" & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
            _Cmd &= vbCrLf & "AND FNHSysUnitSectId=" & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
            _Cmd &= vbCrLf & "AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            _Cmd &= vbCrLf & "AND FDQADate=" & HI.UL.ULDate.FormatDateDB
            '_Cmd &= vbCrLf & "AND FNHourNo=" & CInt("0" & Me.FNHour.Value)
            _Cmd &= vbCrLf & "AND FNHourNo='" & _PInsTime & "'"
            _Cmd &= vbCrLf & "AND FNSeq=" & CInt("0" & Me.FNQCActualQty.Value)
            '_Cmd &= vbCrLf & "AND FTPointSubName='" & HI.UL.ULF.rpQuoted(Me.FTPointName.Text) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            _Cmd = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_CREATE_DATA_QualityAssurance " & Val(HI.ST.SysInfo.CmpID) & "," & Val(Me.FNHSysStyleId.Properties.Tag.ToString) & "," & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString) & ",'" & HI.UL.ULF.rpQuoted(FNHSysUnitSectId.Text.Trim()) & "','" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "','" & _PInsTime & "'," & Val(Me.FNQCActualQty.Value) & ",1"
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN)

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
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each str As String In Pointlist.ToList
                _Cmd &= vbCrLf & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_SubDetail"
                _Cmd &= " (FTInsUser, FDInsDate, FTInsTime , FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate, FNHourNo, FNSeq, FTPointSubName,FNHSysQADetailId)"
                _Cmd &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & "," & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
                _Cmd &= vbCrLf & "," & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                '_Cmd &= vbCrLf & "," & CInt("0" & Me.FNHour.Value)
                _Cmd &= vbCrLf & ",'" & _PInsTime & "'"
                _Cmd &= vbCrLf & "," & CInt("0" & Me.FNQCActualQty.Value) + 1
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(str) & "'"
                _Cmd &= vbCrLf & "," & CInt("0" & _QADetailId)
            Next


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

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            For Each str As String In Pointlist.ToList
                _Cmd &= vbCrLf & "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_SubDetail"

                _Cmd &= vbCrLf & "WHERE FNHSysStyleId=" & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
                _Cmd &= vbCrLf & "AND FNHSysUnitSectId=" & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
                _Cmd &= vbCrLf & "AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
                _Cmd &= vbCrLf & "AND FDQADate=" & HI.UL.ULDate.FormatDateDB
                '_Cmd &= vbCrLf & "AND FNHourNo=" & CInt("0" & Me.FNHour.Value)
                _Cmd &= vbCrLf & "AND FNHourNo='" & _PInsTime & "'"
                _Cmd &= vbCrLf & "AND FNSeq=" & CInt("0" & Me.FNQCActualQty.Value) + 1
                _Cmd &= vbCrLf & "AND FTPointSubName='" & HI.UL.ULF.rpQuoted(str) & "'"
                _Cmd &= vbCrLf & "AND FNHSysQADetailId=" & CInt("0" & _QADetailId)
            Next




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

    Private Sub CheckExe()
        Try
          
            Dim p() As System.Diagnostics.Process
            p = System.Diagnostics.Process.GetProcessesByName("HI SOFT")
            If p.Count > 0 Then
                p = System.Diagnostics.Process.GetProcessesByName("HI SOFT")
                Dim L As String = p(0).MainWindowTitle
                AppActivate(L)
            End If
           
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SimpleButton1_Click_1(sender As Object, e As EventArgs) Handles ocmtoscanpackorder.Click
        CheckExe()
    End Sub

    Private Sub ocmreject_Click(sender As Object, e As EventArgs) Handles ocmreject.Click
        Try
            If HI.MG.ShowMsg.mConfirmProcess(" Your Want Reject Quality Control  Yes or No. ?", 1502170001) = True Then
                Me.UpdateReject()
                Me.FTOrderNo.Text = ""
                Me.FTOrderNo.Properties.Tag = 0
            Else
                ClearOnSucc()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function UpdateReject() As Boolean
        Try
            Dim _Cmd As String = ""
          
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA"
            _Cmd &= vbCrLf & "Set FTStateReject ='1'"

            _Cmd &= vbCrLf & "WHERE FNHSysStyleId=" & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
            _Cmd &= vbCrLf & " AND FNHSysUnitSectId =" & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
            _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            _Cmd &= vbCrLf & " AND FDQADate =" & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & " AND FNHourNo='" & _PInsTime & "'"

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

    Private Sub FNMajorQty_EditValueChanged(sender As Object, e As EventArgs) 'Handles FNMajorQty.EditValueChanged, FNMinorQty.EditValueChanged, FNCtiticalQty.EditValueChanged
        Try
            If (FNMajorQty.Value + FNMinorQty.Value) > _OverDefectQty Then
                MG.ShowMsg.mInfo("Defect Over AQL. / ยอดเสียเกินจำนวนที่กำหนด.. (กรุณาตรวจสอบ)", 1504250099, Me.Text, "", MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
        End Try
    End Sub


End Class

