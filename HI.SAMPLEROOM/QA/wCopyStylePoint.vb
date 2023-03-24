Imports System.Windows.Forms
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class wCopyStylePoint
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Call LoadInitia()
    End Sub
#Region "Property"
    Private m_DbDtStyleCode As New DataTable
    ReadOnly Property DbDtStyleCode As DataTable
        Get
            Return m_DbDtStyleCode
        End Get
    End Property

    Private _StyleId As Integer
    Public Property StyleId As Integer
        Get
            Return _StyleId
        End Get
        Set(value As Integer)
            _StyleId = value
        End Set
    End Property

    Private _StyleCode As String
    Public Property StyleCode As String
        Get
            Return _StyleCode
        End Get
        Set(value As String)
            _StyleCode = value
        End Set
    End Property

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


    Private _FTImageOld As DevExpress.XtraEditors.PictureEdit
    Public Property FTImageOld As DevExpress.XtraEditors.PictureEdit
        Get
            Return _FTImageOld
        End Get
        Set(value As DevExpress.XtraEditors.PictureEdit)
            _FTImageOld = value
        End Set
    End Property

    Private _FNStep As Integer = 0
    Public Property FNStep As Integer
        Get
            Return _FNStep
        End Get
        Set(value As Integer)
            _FNStep = value
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

    Private Sub LoadInitia()
        Try

            Me.cFTStyleCode.OptionsColumn.AllowEdit = False
            Me.cFTStyleName.OptionsColumn.AllowEdit = False

            Dim _Cmd As String = ""
            _Cmd = " SELECT Top  0     FNHSysStyleId, FTStyleCode, '' as  FTStyleName "
            _Cmd &= vbCrLf & " FROM    [HITECH_MASTER].dbo.TMERMStyle WITH(NOLOCK) "
            m_DbDtStyleCode = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MASTER).Copy
            Me.ogcdetail.DataSource = m_DbDtStyleCode

        Catch ex As Exception

        End Try
    End Sub

    Private Sub obtClose_Click(sender As Object, e As EventArgs) Handles obtClose.Click
        Me.Close()
    End Sub

    Private Sub FNHSysStyleId_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FNHSysStyleId.KeyDown
        Try


            Select Case e.KeyCode
                Case Keys.Enter

                    If FNHSysStyleId.Text = "" Then Exit Sub
                    If FNHSysStyleId.Properties.Tag.ToString = "" Then Exit Sub

                    Dim NewRow As DataRow = m_DbDtStyleCode.NewRow
                    NewRow("FNHSysStyleId") = CInt("0" & FNHSysStyleId.Properties.Tag.ToString)
                    NewRow("FTStyleCode") = Replace(Replace(Me.FNHSysStyleId.Text, "/", ""), "*", "")
                    NewRow("FTStyleName") = FNHSysStyleId_None.Text

                    m_DbDtStyleCode.Rows.Add(NewRow)
                    m_DbDtStyleCode.AcceptChanges()

            End Select

        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub

    Private Sub ogvdetail_DoubleClick(sender As Object, e As EventArgs) Handles ogvdetail.DoubleClick
        Try
            With ogvdetail
                If .FocusedRowHandle < 0 Then Exit Sub
                .DeleteRow(.FocusedRowHandle)
                m_DbDtStyleCode.AcceptChanges()
            End With
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub

    Private Sub ogvdetail_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvdetail.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    Call ogvdetail_DoubleClick(ogvdetail, New System.EventArgs)
            End Select
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub


#Region "Copy"
    Private Sub obtCopy_Click(sender As Object, e As EventArgs) Handles obtCopy.Click
        Try
            'Call CopyPoint() 
            'Call CopyImg()
            If Me.ogvdetail.RowCount <= 0 Then
                HI.MG.ShowMsg.mInfo("Please Select Style No. !!!", 14121700001, Me.FNHSysStyleId_lbl.Text, "", MessageBoxIcon.Hand)
                Exit Sub
            End If

            If Me.CopyPoint And Me.CopyImg Then
                HI.MG.ShowMsg.mInfo("Copy Point Style Success....", 14121100001, Me.Text, "", MessageBoxIcon.Information)

            Else
                HI.MG.ShowMsg.mInfo("Copy Point Style Fail....", 14121100002, Me.Text, "", MessageBoxIcon.Error)
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private _Path As String = System.Windows.Forms.Application.StartupPath.ToString
    Private Function CopyPoint() As Boolean
        Try
            Dim _Cmd As String = ""

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            For Each R As DataRow In m_DbDtStyleCode.Rows

                _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTStylePoint  WHERE FTStyleCode ='" & Microsoft.VisualBasic.Left(R!FTStyleCode.ToString, 6) & "'"
                HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)



                _Cmd = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTStylePoint (FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId, FTPointName, FTPicType, FNSeq, FNPointX, FNPointY, FNPicHeight, FNPicWidth, FTPicName,FTRemark,FTStyleCode) "
                _Cmd &= vbCrLf & "SELECT distinct '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & " ," & CInt("0" & R!FNHSysStyleId.ToString) & ", FTPointName, FTPicType, FNSeq, FNPointX, FNPointY, FNPicHeight, FNPicWidth, FTPicName, FTRemark,'" & Microsoft.VisualBasic.Left(R!FTStyleCode.ToString, 6) & "'"
                _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTStylePoint WITH(NOLOCK) "
                _Cmd &= vbCrLf & " WHERE FTStyleCode ='" & Microsoft.VisualBasic.Left(_StyleCode, 6) & "'"

                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    'HI.Conn.SQLConn.Tran.Rollback()
                    'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    'Return False
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
    End Function



    Private Function CopyImg() As Boolean
        Dim _SteCopyMol As Boolean = False
        Dim _SteCopyPnt As Boolean = True

        Try
            Dim _Str As String = "SELECT Top 1   FPStyleImage1,FPStyleImage2, FPStyleImage3, FPStyleImage4 FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle WITH(NOLOCK) WHERE FTStyleCode ='" & HI.UL.ULF.rpQuoted(_StyleCode.ToString) & "' "
            Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MASTER)
            Dim Row As Integer = 1

            Dim _Formpath As String = _Path & "\Images\MODEL\" & Microsoft.VisualBasic.Left(Me._StyleCode.ToString, 6).ToString
            Dim _FormpathPoint As String = _Path & "\Images\POINTMODEL\" & Microsoft.VisualBasic.Left(Me._StyleCode.ToString, 6).ToString
            For Each R As DataRow In m_DbDtStyleCode.Rows
                For Each Col As DataRow In _oDt.Rows
                    Dim _Img As New DevExpress.XtraEditors.PictureEdit
                    Dim _NPath As String = _Path & "\Images\MODEL\" & Microsoft.VisualBasic.Left(R!FTStyleCode.ToString, 6).ToString
                    If (My.Computer.FileSystem.DirectoryExists(_NPath) = False) Then
                        My.Computer.FileSystem.CreateDirectory(_NPath)
                    End If
                    Dim _PPath As String = _Path & "\Images\POINTMODEL\" & Microsoft.VisualBasic.Left(R!FTStyleCode.ToString, 6).ToString
                    If (My.Computer.FileSystem.DirectoryExists(_PPath) = False) Then
                        My.Computer.FileSystem.CreateDirectory(_PPath)
                    End If

                    For i As Integer = 1 To 4 Step 1
                        If (My.Computer.FileSystem.DirectoryExists(_Formpath) = True) Then
                            Dim fileEntries As String() = Directory.GetFiles(_Formpath)

                            Dim fileName As String
                            For Each fileName In fileEntries

                                If Path.GetFileName(fileName).ToString = Microsoft.VisualBasic.Left(Me._StyleCode.ToString, 6).ToString & "_" & (i - 1).ToString & ".JPG" Then
                                    _Img.Image = HI.UL.ULImage.LoadImage(fileName)
                                    Dim Seq As String = Mid(Microsoft.VisualBasic.Right(fileName, 5).ToString, 1, 1)
                                    HI.UL.ULImage.SaveImage(CType(_Img, DevExpress.XtraEditors.PictureEdit), Microsoft.VisualBasic.Left(R!FTStyleCode.ToString, 6).ToString & "_" & Seq.ToString, _NPath)
                                Else
                                    Select Case i - 1
                                        Case 1
                                            Try
                                                _Img.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Col!FPStyleImage1))
                                            Catch ex As Exception
                                                _Img.Image = Nothing
                                            End Try
                                        Case 2
                                            Try
                                                _Img.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Col!FPStyleImage2))
                                            Catch ex As Exception
                                                _Img.Image = Nothing
                                            End Try
                                        Case 3
                                            Try
                                                _Img.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Col!FPStyleImage3))
                                            Catch ex As Exception
                                                _Img.Image = Nothing
                                            End Try
                                        Case 4
                                            Try
                                                _Img.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Col!FPStyleImage4))
                                            Catch ex As Exception
                                                _Img.Image = Nothing
                                            End Try
                                    End Select

                                    HI.UL.ULImage.SaveImage(CType(_Img, DevExpress.XtraEditors.PictureEdit), Microsoft.VisualBasic.Left(R!FTStyleCode.ToString, 6).ToString & "_" & (i - 1).ToString, _NPath)
                                End If
                                i += +1
                            Next fileName

                            _SteCopyMol = True
                        Else

                            Select Case i
                                Case 1
                                    Try
                                        _Img.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Col!FPStyleImage1))
                                    Catch ex As Exception
                                        _Img.Image = Nothing
                                    End Try
                                Case 2
                                    Try
                                        _Img.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Col!FPStyleImage2))
                                    Catch ex As Exception
                                        _Img.Image = Nothing
                                    End Try
                                Case 3
                                    Try
                                        _Img.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Col!FPStyleImage3))
                                    Catch ex As Exception
                                        _Img.Image = Nothing
                                    End Try
                                Case 4
                                    Try
                                        _Img.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Col!FPStyleImage4))
                                    Catch ex As Exception
                                        _Img.Image = Nothing
                                    End Try
                            End Select

                            HI.UL.ULImage.SaveImage(CType(_Img, DevExpress.XtraEditors.PictureEdit), Microsoft.VisualBasic.Left(R!FTStyleCode.ToString, 6).ToString & "_" & (i - 1).ToString, _NPath)


                            'HI.UL.ULImage.SaveImage(_FTImageOld, R!FTStyleCode.ToString & "_" & _FNStep.ToString, _NPath)
                            _SteCopyMol = True
                        End If
                    Next

                    If (My.Computer.FileSystem.DirectoryExists(_FormpathPoint) = True) Then
                        Dim _SubFile As String() = Directory.GetFiles(_FormpathPoint)
                        Dim _FileName As String
                        For Each _FileName In _SubFile
                            Dim _Fiename As String = Path.GetFileName(_FileName)
                            File.Copy(_FileName, _PPath & "\" & _Fiename, True)
                        Next
                        _SteCopyPnt = True
                    End If
                    Row += +1
                Next


            Next

            If (_SteCopyMol = True And _SteCopyPnt = True) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region



    Public Function _ResizeImage(ByVal image As Image) As Image


        Dim newWidth As Integer
        Dim newHeight As Integer

        newWidth = 520
        newHeight = 520


        Dim newImage As Image = New Bitmap(newWidth, newHeight)
        Using graphicsHandle As Graphics = Graphics.FromImage(newImage)
            graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic
            graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight)
        End Using
        Return newImage
    End Function
End Class