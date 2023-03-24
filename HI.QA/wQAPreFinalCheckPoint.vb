Imports DevExpress.XtraEditors
Imports System.Windows.Forms
Imports System.Drawing
Imports DevExpress.XtraBars.Docking2010.Views.WindowsUI
Imports DevExpress.XtraBars.Docking2010.Views

Public Class wQAPreFinalCheckPoint
    Private _FNHSysStyleId As Integer = 0
    Public Property FNHSysStyleId As Integer
        Get
            Return _FNHSysStyleId
        End Get
        Set(value As Integer)
            _FNHSysStyleId = value
        End Set
    End Property

    Private _FNHSysUnitSectId As Integer = 0
    Public Property FNHSysUnitSectId As Integer
        Get
            Return _FNHSysUnitSectId
        End Get
        Set(value As Integer)
            _FNHSysUnitSectId = value
        End Set
    End Property

    Private _FTOrderNo As String = ""
    Public Property FTOrderNo As String
        Get
            Return _FTOrderNo
        End Get
        Set(value As String)
            _FTOrderNo = value
        End Set
    End Property

    Private _FDQADate As String = ""
    Public Property FDQADate As String
        Get
            Return _FDQADate
        End Get
        Set(value As String)
            _FDQADate = value
        End Set
    End Property

    Private _FNHourNo As String = ""
    Public Property FNHourNo As String
        Get
            Return _FNHourNo
        End Get
        Set(value As String)
            _FNHourNo = value
        End Set
    End Property

    Private _FTBarcodeCartonNo As String = ""
    Public Property FTBarcodeCartonNo As String
        Get
            Return _FTBarcodeCartonNo
        End Get
        Set(value As String)
            _FTBarcodeCartonNo = value
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

        dataSource = New SampleDataSource()
        groupsItemDetailPage = New Dictionary(Of SampleDataGroup, PageGroup)()

        _StateEnter = False
    End Sub

    Private dataSource As SampleDataSource
    Private groupsItemDetailPage As Dictionary(Of SampleDataGroup, PageGroup)
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
        Dim _oDt As DataTable

        _Qry = "SELECT    A.FNHSysQAFinalCheckPointId,A.FTQAFinalCheckPointCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ", A.FTQAFinalCheckPointNameTH as FTQAFinalCheckPointName"
        Else
            _Qry &= vbCrLf & ", A.FTQAFinalCheckPointNameEN as FTQAFinalCheckPointName"
        End If

        _Qry &= vbCrLf & ",CASE WHEN ISNULL(B.FTBarcodeCartonNo,'')<>'' THEN '1' ELSE '' END AS FTStateCheck"

        _Qry &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQAFinalCheckPoint AS A  WITH(NOLOCK) "
        _Qry &= vbCrLf & " LEFT OUTER JOIN (Select FTBarcodeCartonNo,FNHSysQAFinalCheckPointId "
        _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPreFinal_CheckPoint  WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FNHSysStyleId=" & CInt("0" & Me.FNHSysStyleId)
        _Qry &= vbCrLf & " AND FNHSysUnitSectId=" & CInt("0" & Me.FNHSysUnitSectId)
        _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo) & "'"
        _Qry &= vbCrLf & " AND FDQADate='" & Me.FDQADate & "'"
        _Qry &= vbCrLf & " AND FTBarcodeCartonNo='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeCartonNo) & "'"
        _Qry &= vbCrLf & " AND FNHourNo='" & Me.FNHourNo & "'"
        _Qry &= vbCrLf & " ) AS B ON A.FNHSysQAFinalCheckPointId=B.FNHSysQAFinalCheckPointId  "

        _Qry &= vbCrLf & " WHERE  A.FTStateActive = '1'"
        _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

        SubTileControl1.ShowText = False
        SubTileControl1.Text = ""
        SubTileControl1.ItemSize = 60

        SubTileControl1.ScrollMode = TileControlScrollMode.ScrollBar

        _TileGroup = New DevExpress.XtraEditors.TileGroup
        _TileGroup.Text = ""

        SubTileControl1.Groups.Add(_TileGroup)
        For Each R As DataRow In _oDt.Rows

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

            _i.Name = R!FTQAFinalCheckPointCode.ToString
            _i.Text = R!FTQAFinalCheckPointName.ToString
            _i.Id = CInt(R!FNHSysQAFinalCheckPointId)
            Dim Elmt As DevExpress.XtraEditors.TileItemElement
            Elmt = New DevExpress.XtraEditors.TileItemElement
            Elmt.Text = R!FTQAFinalCheckPointCode.ToString
            Elmt.TextAlignment = TileItemContentAlignment.BottomLeft

            _i.Elements.Add(Elmt)
            _TileGroup.Items.Add(_i)

        Next

        AddHandler SubTileControl1.ItemClick, AddressOf SubTileItem_Click

    End Sub


    Private Function SaveDataSubDetail(_QADetailId As Integer) As Boolean
        Try
            Dim _Cmd As String = ""
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPreFinal_CheckPoint"
            _Cmd &= " (FTInsUser, FDInsDate, FTInsTime , FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate, FNHourNo, FTBarcodeCartonNo, FNHSysQAFinalCheckPointId)"
            _Cmd &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & "," & CInt("0" & Me.FNHSysStyleId)
            _Cmd &= vbCrLf & "," & CInt("0" & Me.FNHSysUnitSectId)
            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo) & "'"
            _Cmd &= vbCrLf & ",'" & Me.FDQADate & "'"
            _Cmd &= vbCrLf & ",'" & Me.FNHourNo & "'"
            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTBarcodeCartonNo) & "'"
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

            _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPreFinal_CheckPoint"
            _Cmd &= vbCrLf & "WHERE FNHSysStyleId=" & CInt("0" & Me.FNHSysStyleId)
            _Cmd &= vbCrLf & "AND FNHSysUnitSectId=" & CInt("0" & Me.FNHSysUnitSectId)
            _Cmd &= vbCrLf & "AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo) & "'"
            _Cmd &= vbCrLf & "AND FDQADate='" & Me.FDQADate & "'"
            _Cmd &= vbCrLf & " AND FTBarcodeCartonNo='" & HI.UL.ULF.rpQuoted(FTBarcodeCartonNo) & "'"
            _Cmd &= vbCrLf & "AND FNHourNo='" & Me.FNHourNo & "'"
            _Cmd &= vbCrLf & "AND FNHSysQAFinalCheckPointId=" & CInt("0" & _QADetailId)

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
                e.Item.Checked = True


                SaveDataSubDetail(e.Item.Id)


            Else
                e.Item.Checked = False

                Me.DeleteDataSubDetail(e.Item.Id)

            End If

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
End Class