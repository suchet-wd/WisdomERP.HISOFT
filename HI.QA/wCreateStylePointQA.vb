Imports System
Imports System.Windows.Forms
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing.Image
Imports System.Math

Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports System.Diagnostics
Imports System.Data.SqlClient

Imports System.Reflection
 



Public Class wCreateStylePointQA
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()

    Private _oSeq As Integer

    Private _Dragging As Boolean = False

    Private _PointX As Double
    Private _PointY As Double
    Private _PointH As Double
    Private _PointW As Double

    Private _DX As Double
    Private _DY As Double
    Private _UX As Double
    Private _UY As Double

    Private _StartPoint, _FirstPoint, _LastPoint, _EndPoint As Point
    Private _StateObj As Boolean = False
    Private _StateObjMove As Boolean = False
    Private _Seq As Integer
    Private _PointLocation As Point
    Private _PointSize As Size
    Private _LastNameObj As ZBobb.AlphaBlendTextBox
    Private _SeqHold As Integer = 0
    Private textBox1 As ZBobb.AlphaBlendTextBox
    Private textBox12 As ZBobb.AlphaBlendTextBox

#Region "Property"
    Private _DefaultCmpCode As String = ""
    Public Property DefaultCmpCode As String
        Get
            Return _DefaultCmpCode
        End Get
        Set(ByVal value As String)
            _DefaultCmpCode = value
        End Set
    End Property

    Private _FmtRun As String = ""
    Public ReadOnly Property FmtRun As String
        Get
            Return ""
        End Get
    End Property

    Private _AssPath As String = ""
    Public Property AssPath As String
        Get
            Return _AssPath
        End Get
        Set(ByVal value As String)
            _AssPath = value
        End Set
    End Property

    Private _FormName As String = ""
    Public Property FormName As String
        Get
            Return _FormName
        End Get
        Set(ByVal value As String)
            _FormName = value
        End Set
    End Property

    Private _FormObjID As Integer = 0
    Public Property FormObjID As Integer
        Get
            Return _FormObjID
        End Get
        Set(ByVal value As Integer)
            _FormObjID = value
        End Set
    End Property

    Private _FormPopup As String = ""
    Public Property FormPopup As String
        Get
            Return _FormPopup
        End Get
        Set(ByVal value As String)
            _FormPopup = value
        End Set
    End Property

    Private _ObjectFocus As Object = Nothing
    Public Property ObjectFocus As Object
        Get
            Return _ObjectFocus
        End Get
        Set(ByVal value As Object)
            _ObjectFocus = value
        End Set
    End Property

    Private _SysDBName As String = ""
    Public Property SysDBName As String
        Get
            Return _SysDBName
        End Get
        Set(ByVal value As String)
            _SysDBName = value
        End Set
    End Property

    Private _SysTableName As String = ""
    Public Property SysTableName As String
        Get
            Return _SysTableName
        End Get
        Set(ByVal value As String)
            _SysTableName = value
        End Set
    End Property

    Private _SysDocType As String = ""
    Public Property SysDocType As String
        Get
            Return _SysDocType
        End Get
        Set(ByVal value As String)
            _SysDocType = value
        End Set
    End Property

    Private _TableName As String = ""
    Public Property TableName As String
        Get
            Return _TableName
        End Get
        Set(ByVal value As String)
            _TableName = value
        End Set
    End Property

    Private _MainKeyID As String = ""
    Public Property MainKeyID As String
        Get
            Return _MainKeyID
        End Get
        Set(ByVal value As String)
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
        Set(ByVal value As String)
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
        Set(ByVal value As Boolean)
            _ProcComplete = value
        End Set
    End Property

    Private _Parent_Form As Object
    Public Property Parent_Form As Object
        Get
            Return _Parent_Form
        End Get
        Set(ByVal value As Object)
            _Parent_Form = value
        End Set
    End Property

#End Region

#Region "command"

    Private Sub ocmSavePoint_Click(sender As Object, e As EventArgs) Handles ocmSavePoint.Click
        If Me.FTPointName.Text <> "" Then
            If Me.SaveData Then

            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPointName_lbl.Text)
            Me.FTPointName.Focus()

        End If
    End Sub

    Private Sub ocmDelPoint_Click(sender As Object, e As EventArgs) Handles ocmDelPoint.Click
        If Me.FTPointName.Text <> "" Then
            If Me.DeletingData(Me.FNHSysStyleId.Properties.Tag, Me.FTPointName.Text, _oSeq) Then
                Me.LoadData()
                HI.TL.HandlerControl.ClearControl(Me)
            End If
        End If
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

    Private Sub FNHSysStyleId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysStyleId.EditValueChanged
        Try
            If FNHSysStyleId.Text.Trim() <> "" Then
                Dim _Str As String = "SELECT TOP 1 FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle WITH(NOLOCK) WHERE FTStyleCode ='" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "' "
                FNHSysStyleId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "")
                Call LoadImangeStyle(Integer.Parse(Val(FNHSysStyleId.Properties.Tag)))
                Me.LoadData()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
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

    Private Sub FTImage_MouseDown(sender As Object, e As MouseEventArgs) Handles FTImage.MouseDown
        Try
            If textBox1 Is Nothing Then
                HI.TL.HandlerControl.ClearControl(Me)
                textBox1 = New ZBobb.AlphaBlendTextBox
                textBox1.Name = "ZBobbAlphaBlendTextBox"
                textBox1.BorderStyle = BorderStyle.Fixed3D
                textBox1.Multiline = True
                textBox1.BackColor = Color.LightGreen
                textBox1.ReadOnly = True
                textBox1.ForeColor = Color.Transparent
                textBox1.ShortcutsEnabled = False
                AddHandler textBox1.MouseDown, AddressOf ObjClick
                AddHandler textBox1.MouseMove, AddressOf Obj_MouseMove
            End If
            HI.TL.HandlerControl.ClearControl(Me)
            textBox1.Name = "ZBobbAlphaBlendTextBox"
            textBox1.BorderStyle = BorderStyle.Fixed3D
            textBox1.Multiline = True
            textBox1.BackColor = Color.LightGreen
            textBox1.ReadOnly = True
            textBox1.ForeColor = Color.Transparent
            textBox1.ShortcutsEnabled = False
            If e.Button = System.Windows.Forms.MouseButtons.Right Then
                _Seq = GetSeq()
                If _StateObj Then
                    textBox1.Location = New Point(e.X + _PointLocation.X, e.Y + _PointLocation.Y)
                    textBox1.Size = New Size(5, 5)
                Else
                    _StartPoint = New Point(e.X, e.Y)
                    Me.FTImage.Controls.Add(textBox1)
                    textBox1.BringToFront()
                    _LastNameObj = textBox1
                End If
            End If
            If e.Button = System.Windows.Forms.MouseButtons.Left Then
            End If
            _StateObj = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private _StatePoner As Boolean = False
    Private Sub ObjClick(sender As Object, e As MouseEventArgs)
        Try
            If e.Button = System.Windows.Forms.MouseButtons.Right Then
                _Seq = GetSeq()
                If textBox1 Is Nothing Then
                    HI.TL.HandlerControl.ClearControl(Me)
                    textBox1 = New ZBobb.AlphaBlendTextBox
                    textBox1.Name = "ZBobbAlphaBlendTextBox"
                    textBox1.BorderStyle = BorderStyle.Fixed3D
                    textBox1.Multiline = True
                    textBox1.BackColor = Color.LightGreen
                    textBox1.ReadOnly = True
                    textBox1.ForeColor = Color.Transparent
                    textBox1.ShortcutsEnabled = False
                    AddHandler textBox1.MouseDown, AddressOf ObjClick
                    AddHandler textBox1.MouseMove, AddressOf Obj_MouseMove
                End If
                HI.TL.HandlerControl.ClearControl(Me)
                textBox1.Name = "ZBobbAlphaBlendTextBox"
                textBox1.BorderStyle = BorderStyle.Fixed3D
                textBox1.Multiline = True
                textBox1.BackColor = Color.LightGreen
                textBox1.ReadOnly = True
                textBox1.ForeColor = Color.Transparent
                textBox1.ShortcutsEnabled = False
                FTImage.Controls.Remove(textBox1)
                FTImage.Controls.Add(textBox1)
                textBox1.BringToFront()
                Select Case True
                    Case sender.GetType.FullName.ToString = "ZBobb.AlphaBlendTextBox"
                        If textBox1.Name.ToString = sender.name.ToString Then
                            _PointLocation.X = textBox1.Location.X
                            _PointLocation.Y = textBox1.Location.Y
                        Else
                            _PointLocation.X = CType(sender, ZBobb.AlphaBlendTextBox).Location.X
                            _PointLocation.Y = CType(sender, ZBobb.AlphaBlendTextBox).Location.Y
                        End If
                        Dim RectX As Integer = e.X
                        Dim RectY As Integer = e.Y

                        textBox1.Size = New Size(5, 5)
                        textBox1.Location = New Point(_PointLocation.X + RectX, _PointLocation.Y + RectY)
                        _StartPoint = New Point(_PointLocation.X + RectX, _PointLocation.Y + RectY)
                End Select
            End If

            If e.Button = System.Windows.Forms.MouseButtons.Left Then
                If Not (textBox1 Is Nothing) Then
                    textBox1.SendToBack()
                    textBox1.BorderStyle = BorderStyle.None
                    textBox1.Multiline = True
                    textBox1.ReadOnly = True
                    textBox1.ForeColor = Color.Transparent
                    textBox1.ShortcutsEnabled = False
                    textBox1.Size = New Size(2, 2)
                End If
                If CType(sender, ZBobb.AlphaBlendTextBox).Name.ToString <> "ZBobbAlphaBlendTextBox" Then
                    Dim _Qry As String = ""
                    Dim _oDt As DataTable
                    _Qry = "SELECT top 1   A.FTPointName, A.FNSeq, A.FNPointX, A.FNPointY, A.FTPicType, A.FNPicHeight, A.FNPicWidth,A.FTRemark,A.FTPicName"
                    _Qry &= vbCrLf & "FROM   TPRODTStylePoint AS A WITH(NOLOCK) LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B WITH(NOLOCK) ON A.FNHSysStyleId = B.FNHSysStyleId  "
                    _Qry &= vbCrLf & "Where  B.FTStyleCode ='" & Me.FNHSysStyleId.Text & "'"
                    _Qry &= vbCrLf & "AND A.FTPicType=" & CInt(_Step)
                    _Qry &= vbCrLf & "AND A.FNSeq=" & CInt(CType(sender, ZBobb.AlphaBlendTextBox).Name.ToString)

                    _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
                    For Each R As DataRow In _oDt.Rows
                        Me.FTPointName.Text = "" & R!FTPointName.ToString
                        Me.FTRemark.Text = "" & R!FTRemark.ToString
                        Me.FTImagePoint.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\POINTMODEL\" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "\" & R!FTPicName.ToString)
                        Me._Seq = CInt("0" & R!FNSeq.ToString)
                        Me._PointLocation.X = CDbl("0" & R!FNPointX.ToString)
                        Me._PointLocation.Y = CDbl("0" & R!FNPointY.ToString)
                        Me._PointSize.Height = CDbl("0" & R!FNPicHeight.ToString)
                        Me._PointSize.Width = CDbl("0" & R!FNPicWidth.ToString)
                    Next
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Function LoadImage(ImagePath As String) As Image
        Try
            Return Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(ImagePath)))
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function _ResizeImage(ByVal image As Image, ByVal _Img As Image) As Image
        Dim newWidth As Integer
        Dim newHeight As Integer
        newWidth = _Img.Width
        newHeight = _Img.Height
        Dim newImage As Image = New Bitmap(newWidth, newHeight)
        Using graphicsHandle As Graphics = Graphics.FromImage(newImage)
            graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic
            graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight)
        End Using
        Return newImage
    End Function

    Private Function GetSeq() As Integer
        Try
            Dim _Seq As Integer = 0
            Dim _Qry As String = ""
            _Qry = "Select max(FNSeq)  From TPRODTStylePoint with(nolock) "
            _Seq = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "0")
            Return _Seq + 1
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private _Path As String = System.Windows.Forms.Application.StartupPath.ToString
    Private Function SaveData() As Boolean
        Try
            Dim _Qry As String = ""

            If (My.Computer.FileSystem.DirectoryExists(_Path + "\Images\POINTMODEL\" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "")) = False) Then   ' & Microsoft.VisualBasic.Left(Me.FNHSysStyleId.Text, 6).ToString) 
                My.Computer.FileSystem.CreateDirectory(_Path + "\Images\POINTMODEL\" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", ""))
            End If
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTStylePoint "
            _Qry &= vbCrLf & "SET FTUpdUser=N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & ",FDUpdDate =" & HI.UL.ULDate.FormatDateDB
            _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
            '*********************
            _Qry &= vbCrLf & ",FNPointX=" & CDbl(_PointLocation.X.ToString)
            _Qry &= vbCrLf & ",FNPointY=" & CDbl(_PointLocation.Y.ToString)
            _Qry &= vbCrLf & ",FNPicHeight=" & CDbl(_PointSize.Height.ToString)
            _Qry &= vbCrLf & ",FNPicWidth=" & CDbl(_PointSize.Width.ToString)
            '*************
            _Qry &= vbCrLf & " , FTPointName='" & HI.UL.ULF.rpQuoted(Me.FTPointName.Text) & "'"
            _Qry &= vbCrLf & ",FTPicName='" & HI.UL.ULImage.SaveImage(HI.UL.ULImage.ResizeImage(FTImagePoint.Image, New Size(700, 450)), Me.FTPointName.Text, _Path & "\Images\POINTMODEL\" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "")) & "'"
            _Qry &= vbCrLf & ",FTRemark='" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "'"
            _Qry &= vbCrLf & "WHERE  FTStyleCode='" & HI.UL.ULF.rpQuoted(Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "")) & "'"
            _Qry &= vbCrLf & " AND FTPicType=" & CInt(_Step)
            _Qry &= vbCrLf & " AND FNSeq=" & CInt(_Seq)

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                _Seq = HI.Conn.SQLConn.GetField("Select Max(FNSeq) AS FNSeq From TPRODTStylePoint WHERE FNHSysStyleId=" & CInt(Me.FNHSysStyleId.Properties.Tag), Conn.DB.DataBaseName.DB_PROD, "0")
                _Seq += +1
                _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTStylePoint( FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId, FTPointName, FNSeq, FNPointX, FNPointY, FTPicType, FNPicHeight, FNPicWidth, FTPicName,FTRemark,FTStyleCode)"
                _Qry &= vbCrLf & "SELECT  N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & "," & CInt(Me.FNHSysStyleId.Properties.Tag)
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPointName.Text) & "'"
                _Qry &= vbCrLf & "," & CInt(_Seq)
                '*******************
                _Qry &= vbCrLf & "," & CDbl(_PointLocation.X.ToString)
                _Qry &= vbCrLf & "," & CDbl(_PointLocation.Y.ToString)
                '*******************
                _Qry &= vbCrLf & "," & CInt(_Step)
                _Qry &= vbCrLf & "," & CDbl(_PointSize.Height.ToString)
                _Qry &= vbCrLf & "," & CDbl(_PointSize.Width.ToString)
                _Qry &= vbCrLf & ",'" & HI.UL.ULImage.SaveImage(HI.UL.ULImage.ResizeImage(FTImagePoint.Image, New Size(700, 450)), Me.FTPointName.Text, _Path & "\Images\POINTMODEL\" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "")) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "")) & "'"

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If
            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            LoadData()
            LoadDrawRectangleRectangle(_Step)
            Call LoadImangeStyle(Integer.Parse(Val(FNHSysStyleId.Properties.Tag)))
            HI.TL.HandlerControl.ClearControl(Me)
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Sub LoadData()
        Dim _Qry As String = ""
        _Qry = "Select A.FNHSysStyleId, A.FTPointName, A.FNSeq, A.FNPointX, A.FNPointY, A.FTPicType, A.FNPicHeight, A.FNPicWidth, A.FTPicName ,A.FTRemark "
        _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTStylePoint AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B WITH(NOLOCK) ON A.FNHSysStyleId = B.FNHSysStyleId  "
        _Qry &= vbCrLf & "WHERE  A.FTStyleCode ='" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "'"
        Me.ogcdetail.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
    End Sub

    Private Function DeletingData(ByVal _FNStylyNo As Integer, ByVal _FTPointName As String, ByVal Seq As Integer) As Boolean
        Try
            Dim _Qry As String = ""
            _Qry = "Delete From TPRODTStylePoint"
            _Qry &= vbCrLf & "WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "")) & "'"
            _Qry &= vbCrLf & " AND FTPointName='" & HI.UL.ULF.rpQuoted(_FTPointName) & "'"
            _Qry &= vbCrLf & " AND FNSeq=" & CInt(Seq)
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)
            LoadData()
            LoadDrawRectangleRectangle(_Step)
            Call LoadImangeStyle(Integer.Parse(Val(FNHSysStyleId.Properties.Tag)))
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Sub FTImage_MouseMove(sender As Object, e As MouseEventArgs) Handles FTImage.MouseMove
        Try
            If e.Button = System.Windows.Forms.MouseButtons.Right Then
                _EndPoint = New Point(e.X, e.Y)
                Dim rect As New Rectangle(_StartPoint.X, _StartPoint.Y, _EndPoint.X - _StartPoint.X + 1, _EndPoint.Y - _StartPoint.Y + 1)
                Dim rect2 As New Rectangle(_EndPoint.X, _EndPoint.Y, _StartPoint.X - _EndPoint.X + 1, _StartPoint.Y - _EndPoint.Y + 1)
                If Abs(rect.Width) < 5 Then
                    rect.Width = 5
                End If
                If Abs(rect.Height) < 5 Then
                    rect.Height = 5
                End If
                If Abs(rect2.Width) < 5 Then
                    rect2.Width = 5
                End If
                If Abs(rect2.Height) < 5 Then
                    rect2.Height = 5
                End If
                If rect.Height <= -1 And rect.Width <= -1 Then
                    textBox1.Location = New Drawing.Point(rect2.X, rect2.Y)
                    textBox1.Size = New Drawing.Size(Abs(rect2.Width), Abs(rect2.Height))
                Else
                    If rect.Height <= -1 Then
                        textBox1.Location = New Drawing.Point(rect.X, rect2.Y)
                        textBox1.Size = New Drawing.Size(Abs(rect.Width), Abs(rect.Height))
                    ElseIf rect.Width <= -1 Then
                        textBox1.Location = New Drawing.Point(rect2.X, rect.Y)
                        textBox1.Size = New Drawing.Size(Abs(rect.Width), Abs(rect.Height))
                    Else
                        textBox1.Location = New Drawing.Point(rect.X, rect.Y)
                        textBox1.Size = New Drawing.Size(Abs(rect.Width), Abs(rect.Height))
                    End If
                End If
                _PointLocation = textBox1.Location
                _PointSize = textBox1.Size
            End If
            _StateObjMove = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Obj_MouseMove(sender As Object, e As MouseEventArgs)
        Try
            If e.Button = System.Windows.Forms.MouseButtons.Right Then
                _EndPoint = New Point(CType(sender, ZBobb.AlphaBlendTextBox).Location.X + e.X, CType(sender, ZBobb.AlphaBlendTextBox).Location.Y + e.Y)

                Dim rect As New Rectangle(_StartPoint.X, _StartPoint.Y, _EndPoint.X - _StartPoint.X + 1, _EndPoint.Y - _StartPoint.Y + 1)
                Dim rect2 As New Rectangle(_EndPoint.X, _EndPoint.Y, _StartPoint.X - _EndPoint.X + 1, _StartPoint.Y - _EndPoint.Y + 1)

                If Abs(rect.Width) < 5 Then
                    rect.Width = 5
                End If
                If Abs(rect.Height) < 5 Then
                    rect.Height = 5
                End If
                If Abs(rect2.Width) < 5 Then
                    rect2.Width = 5
                End If
                If Abs(rect2.Height) < 5 Then
                    rect2.Height = 5
                End If

                If rect.Height <= -1 And rect.Width <= -1 Then
                    textBox1.Location = New Drawing.Point(rect2.X, rect2.Y)
                    textBox1.Size = New Drawing.Size(Abs(rect2.Width), Abs(rect2.Height))
                Else
                    If rect.Height <= -1 Then
                        textBox1.Location = New Drawing.Point(rect.X, rect2.Y)
                        textBox1.Size = New Drawing.Size(Abs(rect.Width), Abs(rect.Height))
                    ElseIf rect.Width <= -1 Then
                        textBox1.Location = New Drawing.Point(rect2.X, rect.Y)
                        textBox1.Size = New Drawing.Size(Abs(rect.Width), Abs(rect.Height))
                    Else
                        textBox1.Location = New Drawing.Point(rect.X, rect.Y)
                        textBox1.Size = New Drawing.Size(Abs(rect.Width), Abs(rect.Height))
                    End If
                End If
                _PointLocation = textBox1.Location
                _PointSize = textBox1.Size
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTImage_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles FTImage.MouseUp
        Try
            If e.Button = System.Windows.Forms.MouseButtons.Right Then
                'CType(Me.FTImagePoint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
                Me.FTImage.Controls.Add(textBox1)
                'CType(Me.FTImagePoint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
                textBox1.BringToFront()
            ElseIf e.Button = System.Windows.Forms.MouseButtons.Left Then
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadDrawRectangleRectangle(ByVal _Type As Integer)
        Dim _Qry As String = ""
        Dim _oDt As DataTable

        _Qry = "SELECT     A.FNHSysStyleId, A.FNSeq, A.FNPointX, A.FNPointY, A.FTPicType, A.FNPicHeight, A.FNPicWidth"
        _Qry &= vbCrLf & "FROM   TPRODTStylePoint AS A WITH(NOLOCK) LEFT OUTER JOIN "
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B WITH(NOLOCK) ON A.FNHSysStyleId = B.FNHSysStyleId "
        _Qry &= vbCrLf & "Where  A.FTStyleCode ='" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "'"
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
            textBox12.Location = New Drawing.Point(CInt(Abs(R!FNPointX)), CInt(Abs(R!FNPointY)))
            textBox12.Size = New Drawing.Size(CInt(Abs(R!FNPicWidth)), CInt(Abs(R!FNPicHeight)))
            textBox12.Name = "" & R!FNSeq.ToString
            Me.FTImage.Controls.Add(textBox12)
            textBox12.BringToFront()
            AddHandler textBox12.MouseDown, AddressOf ObjClick
            AddHandler textBox12.MouseMove, AddressOf Obj_MouseMove
        Next
    End Sub

    Private Sub ogvdetail_DoubleClick(sender As Object, e As EventArgs) Handles ogvdetail.DoubleClick
        Try
            With ogvdetail
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                Me.FTPointName.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTPointName").ToString
                Me.FTRemark.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTRemark").ToString
                Me.FTImagePoint.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\POINTMODEL\" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "\" & .GetRowCellValue(.FocusedRowHandle, "FTPicName").ToString)
                Me._oSeq = CInt("" & .GetRowCellValue(.FocusedRowHandle, "FNSeq"))
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private textBoxS As ZBobb.AlphaBlendTextBox
    Private Sub FTImagePoint_EditValueChanged(sender As Object, e As EventArgs)
        Try
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
                    Me.FTImagePoint.SendToBack()
                    textBoxS.Location = New Drawing.Point(50 * y, 50 * x)
                    textBoxS.Size = New Drawing.Size(50, 50)
                    i += +1
                    textBoxS.Name = "" & i.ToString
                    Me.FTImagePoint.Controls.Add(textBoxS)
                    textBoxS.BringToFront()
                    AddHandler textBoxS.MouseDown, AddressOf ObjFocus
                Next
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ObjFocus(sender As Object, e As MouseEventArgs)
        Try
            If CType(sender, ZBobb.AlphaBlendTextBox).BackColor = Color.LightPink Then
                CType(sender, ZBobb.AlphaBlendTextBox).BackColor = Color.Orange
            Else
                CType(sender, ZBobb.AlphaBlendTextBox).BackColor = Color.LightPink
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmNewImg_Click(sender As Object, e As EventArgs) Handles ocmAddNewImg.Click
        Try
            Dim _Img As New DevExpress.XtraEditors.PictureEdit
            Dim _NPath As String = _Path & "\Images\MODEL\" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "")
            Dim openFileDialog1 As New OpenFileDialog
            openFileDialog1.Multiselect = True
            openFileDialog1.ShowDialog()
            If openFileDialog1.FileName <> "" Then
                Dim sPath As String = "Folder path here"
                If (My.Computer.FileSystem.DirectoryExists(_NPath) = False) Then
                    My.Computer.FileSystem.CreateDirectory(_NPath)
                End If
                _Img.Image = HI.UL.ULImage.LoadImage(openFileDialog1.FileName)
                HI.UL.ULImage.SaveImage(_Img, Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "_" & _Step.ToString, _NPath)
                Me.FTImage.Image = HI.UL.ULImage.LoadImage(_NPath & "\" & Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "") & "_" & _Step.ToString & ".JPG")
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmcopy_Click(sender As Object, e As EventArgs) Handles ocmcopy.Click
        Try
            If Me.FNHSysStyleId.Text <> "" Then
                Call CopyStylePoint()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CopyStylePoint()
        Try
            Dim _FromCopyStly As New wCopyStylePoint
            HI.TL.HandlerControl.AddHandlerObj(_FromCopyStly)
            With _FromCopyStly
                .StyleId = Me.FNHSysStyleId.Properties.Tag
                .StyleCode = Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "")
                .FTImageOld = Me.FTImage
                .FNStep = _Step
                .Show()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvdetail_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvdetail.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Call ocmDelPoint_Click(ocmDelPoint, New System.EventArgs)
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class