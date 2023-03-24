Imports DevExpress.XtraEditors
Imports System.Windows.Forms
Imports System.Drawing
Imports DevExpress.XtraBars.Docking2010.Views.WindowsUI
Imports DevExpress.XtraBars.Docking2010.Views

Public Class wQCFabricInspecRoll

    Private CalcPopup As wPopupCalc


    Private _DetailEdit As Boolean = False
    Public Property DetailEdit As Boolean
        Get
            Return _DetailEdit
        End Get
        Set(value As Boolean)
            _DetailEdit = value
        End Set
    End Property


    Private _FTQCFabNo As String = ""
    Public Property FTQCFabNo As String
        Get
            Return _FTQCFabNo
        End Get
        Set(value As String)
            _FTQCFabNo = value
        End Set
    End Property

    Private _FNHSysRawMatId As Integer = 0
    Public Property FNHSysRawMatId As Integer
        Get
            Return _FNHSysRawMatId
        End Get
        Set(value As Integer)
            _FNHSysRawMatId = value
        End Set
    End Property

    Private _FTBatchNo As String = ""
    Public Property FTBatchNo As String
        Get
            Return _FTBatchNo
        End Get
        Set(value As String)
            _FTBatchNo = value
        End Set
    End Property

    Private _RollNo As String = ""
    Public Property RollNo As String
        Get
            Return _RollNo
        End Get
        Set(value As String)
            _RollNo = value
        End Set
    End Property



    Private _CalcValue As Integer = 0
    Public Property CalcValue As Integer
        Get
            Return _CalcValue
        End Get
        Set(value As Integer)
            _CalcValue = value
        End Set
    End Property

    Private _StateEnter As Boolean = False
    Public Property StateEnter As Boolean
        Get
            Return _StateEnter
        End Get
        Set(value As Boolean)
            _StateEnter = value
        End Set
    End Property

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.



        _StateEnter = False
    End Sub


    Private _TileGroup As DevExpress.XtraEditors.TileGroup
    Private SubTileControl1 As DevExpress.XtraEditors.TileControl

    Private Sub subCreateLayout()


        TileControl.Visible = False
        SubTileControl1 = New DevExpress.XtraEditors.TileControl

        ogrpSubmenu.Controls.Add(SubTileControl1)

        SubTileControl1.Dock = DockStyle.Fill
        SubTileControl1.BackColor = Color.LightGray
        SubTileControl1.Orientation = Orientation.Vertical

        'SubTileControl.Visible = True
        Dim _Qry As String = ""
        Dim _oDt As New DataTable
        Dim dtgrp As New DataTable

        _Qry = "SELECT    A.FNHSysQCFabricDetailId,A.FTQCFabricDetailCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ", A.FTQCFabricDetailNameTH as FTQCFabricDetailName"
        Else
            _Qry &= vbCrLf & ", A.FTQCFabricDetailNameEN as FTQCFabricDetailName"
        End If

        _Qry &= vbCrLf & ",CASE WHEN ISNULL(B.FTQCFabNo,'')<>'' THEN '1' ELSE '' END AS FTStateCheck,A.FNQCFabricType"

        _Qry &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMQCFabric AS A  WITH(NOLOCK) "
        _Qry &= vbCrLf & " LEFT OUTER JOIN (Select FTQCFabNo,FNHSysQCFabricDetailId,FTBatchNo,FNHSysRawMatId,FTRollNo "
        _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat_Defect  WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTQCFabNo='" & HI.UL.ULF.rpQuoted(Me.FTQCFabNo) & "'"
        _Qry &= vbCrLf & " AND FNHSysRawMatId=" & Me.FNHSysRawMatId.ToString
        _Qry &= vbCrLf & " AND FTBatchNo='" & HI.UL.ULF.rpQuoted(Me.FTBatchNo) & "'"
        _Qry &= vbCrLf & " AND FTRollNo='" & HI.UL.ULF.rpQuoted(Me.RollNo) & "'"

        _Qry &= vbCrLf & " ) AS B ON A.FNHSysQCFabricDetailId=B.FNHSysQCFabricDetailId  "
        _Qry &= vbCrLf & " WHERE  A.FTStateActive = '1'"

        _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

        SubTileControl1.ShowText = False
        SubTileControl1.Text = ""
        SubTileControl1.ItemSize = 60
        SubTileControl1.ScrollMode = TileControlScrollMode.ScrollBar

        If _oDt.Rows.Count > 0 Then


            _Qry = "  Select   FNListIndex "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " , FTNameTH AS  FTName "
            Else
                _Qry &= vbCrLf & " , FTNameEN AS  FTName "

            End If
            _Qry &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData As X With(NOLOCK) "
            _Qry &= vbCrLf & " Where (FTListName = N'FNQCFabricType') "
            _Qry &= vbCrLf & "  Order By FNListIndex "

            dtgrp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)

            For Each Rgrp As DataRow In dtgrp.Rows

                _TileGroup = New DevExpress.XtraEditors.TileGroup
                _TileGroup.Text = Rgrp!FTName.ToString

                For Each R As DataRow In _oDt.Select("FNQCFabricType=" & Val(Rgrp!FNListIndex) & "", "FTQCFabricDetailCode")

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

                    _i.Checked = (R!FTStateCheck.ToString = "1")

                    _i.Name = R!FTQCFabricDetailCode.ToString
                    _i.Text = R!FTQCFabricDetailCode.ToString & " " & R!FTQCFabricDetailName.ToString
                    _i.Id = CInt(R!FNHSysQCFabricDetailId)
                    'Dim Elmt As DevExpress.XtraEditors.TileItemElement
                    'Elmt = New DevExpress.XtraEditors.TileItemElement
                    'Elmt.Text = R!FTQCFabricDetailCode.ToString
                    'Elmt.TextAlignment = TileItemContentAlignment.BottomLeft

                    ' _i.Elements.Add(Elmt)
                    _TileGroup.Items.Add(_i)

                Next


                SubTileControl1.Groups.Add(_TileGroup)

            Next
        End If



        AddHandler SubTileControl1.ItemClick, AddressOf SubTileItem_Click

        dtgrp.Dispose()
        _oDt.Dispose()

    End Sub


    Private Function SaveDataSubDetail(_QADetailId As Integer) As Boolean
        Try
            Dim _Cmd As String = ""
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat_Defect"
            _Cmd &= " (FTInsUser, FDInsDate, FTInsTime , FTQCFabNo, FNHSysRawMatId, FTBatchNo, FTRollNo, FNHSysQCFabricDetailId)"
            _Cmd &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTQCFabNo) & "'"
            _Cmd &= vbCrLf & "," & Val(Me.FNHSysRawMatId)
            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTBatchNo) & "'"
            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.RollNo) & "'"
            _Cmd &= vbCrLf & "," & CInt("0" & _QADetailId)


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

            _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat_Defect"
            _Cmd &= vbCrLf & " WHERE FTQCFabNo='" & HI.UL.ULF.rpQuoted(Me.FTQCFabNo) & "'"
            _Cmd &= vbCrLf & " AND FNHSysRawMatId=" & Me.FNHSysRawMatId.ToString
            _Cmd &= vbCrLf & " AND FTBatchNo='" & HI.UL.ULF.rpQuoted(Me.FTBatchNo) & "'"
            _Cmd &= vbCrLf & " AND FTRollNo='" & HI.UL.ULF.rpQuoted(Me.RollNo) & "'"
            _Cmd &= vbCrLf & "AND FNHSysQCFabricDetailId=" & CInt("0" & _QADetailId)

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


    Private Sub SubTileItem_Click(sender As Object, e As TileItemEventArgs)
        Try

            If e.Item.Checked = False Then
                e.Item.Checked = SaveDataSubDetail(e.Item.Id)
            Else
                e.Item.Checked = Not (Me.DeleteDataSubDetail(e.Item.Id))
            End If

            DetailEdit = True
        Catch ex As Exception
        End Try

        'SubTileControl.Groups.Item(0).Items.Remove(sender)
    End Sub

    Private Sub wQAPreFinalCheckPoint_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call subCreateLayout()
    End Sub

    Private Sub ocmclose_Click(sender As Object, e As EventArgs) Handles ocmclose.Click
        Me.Close()
    End Sub

    Private Sub FNActQuantity_MouseDown(sender As Object, e As MouseEventArgs) Handles FNActQuantity.MouseDown
        Try
            CalcPopup = New wPopupCalc
            With CalcPopup
                .ShowDialog()
                If .StateEnter = True Then
                    Me.FNActQuantity.Value = .CalcValue
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FTActFabricFrontSize_MouseDown(sender As Object, e As MouseEventArgs) Handles FTActFabricFrontSize.MouseDown
        Try
            CalcPopup = New wPopupCalc
            With CalcPopup
                .ShowDialog()
                If .StateEnter = True Then
                    Me.FTActFabricFrontSize.Value = .CalcValue
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub
End Class