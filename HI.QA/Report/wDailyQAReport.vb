Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports  System.Windows.Forms
Imports System.Math
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Data
Imports System.Data.SqlClient
Imports DevExpress.XtraCharts

Public Class wDailyQAReport

    Private _PointQty As Integer = 0
    Private _Path As String =  System.Windows.Forms.Application.StartupPath.ToString
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private _StateLoad As Boolean = False
    Public Property StateLoad As Boolean
        Get
            Return _StateLoad
        End Get
        Set(value As Boolean)
            _StateLoad = value
        End Set
    End Property
    Private Sub InitGrid()
        Try
            With ogvDetailDaily
                .OptionsView.ShowAutoFilterRow = False
                .OptionsSelection.MultiSelect = False
                .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadImageStyle()
        Try
            Dim _Qry As String = ""
            Dim dt As DataTable
            _Qry = "SELECT  TOP 1   FNHSysStyleId,  FPStyleImage1,FPStyleImage2, FPStyleImage3, FPStyleImage4"
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTStyleCode='" & Me.FNHSysStyleId.Text & "'"
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

            For Each Rx As DataRow In dt.Rows

                Try
                    Me.FTImage.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\MODEL\" & Me.FNHSysStyleId.Text.ToString() & "\" & Me.FNHSysStyleId.Text.ToString() & "_0.JPG")
                    If Me.FTImage.Image Is Nothing Then
                        Me.FTImage.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage1))
                    End If
                Catch ex As Exception
                    Me.FTImage.Image = Nothing
                End Try



                Try
                    Me.FTImage2.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\MODEL\" & Me.FNHSysStyleId.Text.ToString() & "\" & Me.FNHSysStyleId.Text.ToString() & "_1.JPG")
                    If Me.FTImage2.Image Is Nothing Then
                        Me.FTImage2.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage2))
                    End If
                Catch ex As Exception
                    Me.FTImage2.Image = Nothing
                End Try

                'If Rx!FPStyleImage3.ToString <> "" Then
                '    Try
                '        Me.FTImage3.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\MODEL\" &  Me.FNHSysStyleId.Text .ToString() & "\" &  Me.FNHSysStyleId.Text .ToString() & "_2.JPG")
                '        If Me.FTImage3.Image Is Nothing Then
                '            Me.FTImage3.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage3))
                '        End If
                '    Catch ex As Exception
                '        Me.FTImage3.Image = Nothing
                '    End Try
                'End If

            Next
            If dt.Rows.Count <= 0 Then
                Try
                    Me.FTImage.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\MODEL\" & Me.FNHSysStyleId.Text.ToString() & "\" & Me.FNHSysStyleId.Text.ToString() & "_0.JPG")
                Catch ex As Exception
                    Me.FTImage.Image = Nothing
                End Try

                Try
                    Me.FTImage2.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\MODEL\" & Me.FNHSysStyleId.Text.ToString() & "\" & Me.FNHSysStyleId.Text.ToString() & "_1.JPG")
                Catch ex As Exception
                    Me.FTImage2.Image = Nothing
                End Try
            End If
            Call LoadDrawRectangleRectangle(0)
            Call LoadDrawRectangleRectangle2(1)
            'Call LoadDrawRectangleRectangle3(2)
        Catch ex As Exception

        End Try
    End Sub



    Private Sub LoadImageStyle(_StyleCode As String, ByRef img As DevExpress.XtraEditors.PictureEdit, ByRef img2 As DevExpress.XtraEditors.PictureEdit)
        Try
            Dim Images As DevExpress.XtraEditors.PictureEdit = Me.FTImage
            Dim Images2 As DevExpress.XtraEditors.PictureEdit = Me.FTImage2

            Dim _Qry As String = ""
            Dim dt As DataTable
            _Qry = "SELECT  TOP 1   FNHSysStyleId,  FPStyleImage1,FPStyleImage2, FPStyleImage3, FPStyleImage4"
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTStyleCode='" & _StyleCode & "'"
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

            For Each Rx As DataRow In dt.Rows
                If Rx!FPStyleImage1.ToString <> "" Then
                    Try
                        Images.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\MODEL\" & _StyleCode.ToString() & "\" & _StyleCode.ToString() & "_0.JPG")
                        If Images.Image Is Nothing Then
                            Images.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage1))
                        End If
                    Catch ex As Exception
                        Images.Image = Nothing
                    End Try

                End If
                If Rx!FPStyleImage2.ToString <> "" Then
                    Try
                        Images2.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\MODEL\" & _StyleCode.ToString() & "\" & _StyleCode.ToString() & "_1.JPG")
                        If Images2.Image Is Nothing Then
                            Images2.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage2))
                        End If
                    Catch ex As Exception
                        Images2.Image = Nothing
                    End Try
                End If
                'If Rx!FPStyleImage3.ToString <> "" Then
                '    Try
                '        Me.FTImage3.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\MODEL\" &  Me.FNHSysStyleId.Text .ToString() & "\" &  Me.FNHSysStyleId.Text .ToString() & "_2.JPG")
                '        If Me.FTImage3.Image Is Nothing Then
                '            Me.FTImage3.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage3))
                '        End If
                '    Catch ex As Exception
                '        Me.FTImage3.Image = Nothing
                '    End Try
                'End If

            Next
            If dt.Rows.Count <= 0 Then
                Try
                    Images.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\MODEL\" & Me.FNHSysStyleId.Text.ToString() & "\" & _StyleCode.ToString() & "_0.JPG")
                Catch ex As Exception
                    Images.Image = Nothing
                End Try

                Try
                    Images2.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\MODEL\" & Me.FNHSysStyleId.Text.ToString() & "\" & _StyleCode.ToString() & "_1.JPG")
                Catch ex As Exception
                    Images2.Image = Nothing
                End Try
            End If
            Call LoadDrawRectangleRectangle(0, Images, _StyleCode)
            Call LoadDrawRectangleRectangle2(1, Images2, _StyleCode)

            img = Images
            img2 = Images2
            'Call LoadDrawRectangleRectangle3(2)
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

    Private textBox12 As ZBobb.AlphaBlendTextBox

    Private Sub LoadDrawRectangleRectangle(ByVal _Type As Integer)

        Dim _Qry As String = ""
        Dim _oDt As DataTable

        _Qry = "SELECT    A.FNHSysStyleId, A.FNSeq, (((A.FNPointX /480)*100)*380/100) AS FNPointX,(((A.FNPointY /480)*100)*380/100) AS FNPointY, A.FTPicType, (((A.FNPicHeight /480)*100)*380/100)  AS FNPicHeight ,(((A.FNPicWidth /480)*100)*380/100) AS FNPicWidth"
        _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTStylePoint AS A WITH(NOLOCK) LEFT OUTER JOIN "
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B WITH(NOLOCK) ON A.FNHSysStyleId = B.FNHSysStyleId "
        _Qry &= vbCrLf & "Where B.FTStyleCode='" & Me.FNHSysStyleId.Text & "'"
        _Qry &= vbCrLf & "AND A.FTPicType=" & CInt(_Type)
        _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        For Each R As DataRow In _oDt.Rows

            textBox12 = New ZBobb.AlphaBlendTextBox
            textBox12.BorderStyle = BorderStyle.Fixed3D

            textBox12.BackColor = Color.Orange
            textBox12.Multiline = True
            textBox12.ReadOnly = True
            'textBox12.ForeColor = Color.Transparent
            textBox12.Font = New Font("Tahoma", 10, FontStyle.Bold)
            textBox12.ShortcutsEnabled = False
            Me.FTImage.SendToBack()
            textBox12.Location = New System.Drawing.Point(CInt(Abs(R!FNPointX)), CInt(Abs(R!FNPointY)))
            textBox12.Size = New System.Drawing.Size(CInt(Abs(R!FNPicWidth)), CInt(Abs(R!FNPicHeight)))
            textBox12.Name = "" & R!FNSeq.ToString
            textBox12.Text = GetDefectQty(CInt("0" & R!FNSeq.ToString))
            'textBox12.ForeColor = Color.Red
            Me.FTImage.Controls.Add(textBox12)
            textBox12.BringToFront()
            'AddHandler textBox12.MouseDown, AddressOf ObjClick
            'AddHandler textBox12.MouseMove, AddressOf Obj_MouseMove
        Next

    End Sub

    Private Sub LoadDrawRectangleRectangle(ByVal _Type As Integer, ByVal img As DevExpress.XtraEditors.PictureEdit, ByVal _StyleCode As String)

        Dim _Qry As String = ""
        Dim _oDt As DataTable

        _Qry = "SELECT    A.FNHSysStyleId, A.FNSeq, (((A.FNPointX /480)*100)*380/100) AS FNPointX,(((A.FNPointY /480)*100)*380/100) AS FNPointY, A.FTPicType, (((A.FNPicHeight /480)*100)*380/100)  AS FNPicHeight ,(((A.FNPicWidth /480)*100)*380/100) AS FNPicWidth"
        _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTStylePoint AS A WITH(NOLOCK) LEFT OUTER JOIN "
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B WITH(NOLOCK) ON A.FNHSysStyleId = B.FNHSysStyleId "
        _Qry &= vbCrLf & "Where B.FTStyleCode='" & _StyleCode & "' and ( A.FNPointX > 0  and A.FNPointY > 0 and A.FNPicHeight > 0  and A.FNPicWidth > 0 ) "
        _Qry &= vbCrLf & "AND A.FTPicType=" & CInt(_Type)
        _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        For Each R As DataRow In _oDt.Rows

            textBox12 = New ZBobb.AlphaBlendTextBox
            textBox12.BorderStyle = BorderStyle.Fixed3D

            textBox12.BackColor = Color.Orange
            textBox12.Multiline = True
            textBox12.ReadOnly = True
            'textBox12.ForeColor = Color.Transparent
            textBox12.Font = New Font("Tahoma", 10, FontStyle.Bold)
            textBox12.ShortcutsEnabled = False
            img.SendToBack()
            textBox12.Location = New System.Drawing.Point(CInt(Abs(R!FNPointX)), CInt(Abs(R!FNPointY)))
            textBox12.Size = New System.Drawing.Size(CInt(Abs(R!FNPicWidth)), CInt(Abs(R!FNPicHeight)))
            textBox12.Name = "" & R!FNSeq.ToString
            textBox12.Text = GetDefectQty(CInt("0" & R!FNSeq.ToString))
            'textBox12.ForeColor = Color.Red
            img.Controls.Add(textBox12)
            textBox12.BringToFront()
            'AddHandler textBox12.MouseDown, AddressOf ObjClick
            'AddHandler textBox12.MouseMove, AddressOf Obj_MouseMove
        Next

    End Sub



    Private Function GetDefectQty(ByVal _Seq As Integer) As String
        Try
            Dim _Cmd As String = ""
            Dim _QtyDefect As String = ""
            _Cmd = " SELECT   COUNT(*) AS Qty,B.FTPointName, B.FNSeq, QD.FTStateCtitical , List.FTNameEN"
            _Cmd &= vbCrLf & "FROM      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA_SubDetail AS A  "
            _Cmd &= vbCrLf & "INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S ON A.FNHSysStyleId = S.FNHSysStyleId"
            _Cmd &= vbCrLf & "   LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTStylePoint AS B ON LEFT (A.FTPointSubName,CHARINDEX('-',A.FTPointSubName)-1) = B.FTPointName"
            _Cmd &= vbCrLf & "and S.FTStyleCode   = B.FTStyleCode"
            _Cmd &= vbCrLf & "   LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQADetail AS QD WITH(NOLOCK)  ON A.FNHSysQADetailId = QD.FNHSysQADetailId"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN ( Select  FNListIndex, FTNameTH, FTNameEN  From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData  WITH(NOLOCK)  "
            _Cmd &= vbCrLf & "  where FTListName = 'FTStateCtitical'  ) AS List ON QD.FTStateCtitical = List.FNListIndex  "
            _Cmd &= vbCrLf & ""
            _Cmd &= vbCrLf & ""

            _Cmd &= vbCrLf & "WHERE     (A.FDQADate >= '" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "') AND (A.FDQADate <= '" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "') "
            _Cmd &= vbCrLf & "AND (A.FNHSysUnitSectId = " & Me.FNHSysUnitSectId.Properties.Tag & ") AND (S.FTStyleCode = '" & Me.FNHSysStyleId.Text & "') AND (A.FTOrderNo = '" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "')"
            _Cmd &= vbCrLf & " AND B.FNSeq =" & _Seq
            _Cmd &= vbCrLf & "GROUP BY B.FTPointName , B.FNSeq, QD.FTStateCtitical , List.FTNameEN"
            Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            For Each R As DataRow In _oDt.Rows
                _QtyDefect = CInt("0" & R!Qty)
            Next

            Return _QtyDefect
        Catch ex As Exception

        End Try
    End Function



    Private textBox122 As ZBobb.AlphaBlendTextBox

    Private Label As DevExpress.XtraEditors.LabelControl

    Private Sub LoadDrawRectangleRectangle2(ByVal _Type As Integer)

        Dim _Qry As String = ""
        Dim _oDt As DataTable

        _Qry = "SELECT    A.FNHSysStyleId, A.FNSeq, (((A.FNPointX /480)*100)*380/100) AS FNPointX,(((A.FNPointY /480)*100)*380/100) AS FNPointY, A.FTPicType, (((A.FNPicHeight /480)*100)*380/100)  AS FNPicHeight ,(((A.FNPicWidth /480)*100)*380/100) AS FNPicWidth"
        _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTStylePoint AS A WITH(NOLOCK) LEFT OUTER JOIN "
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B WITH(NOLOCK) ON A.FNHSysStyleId = B.FNHSysStyleId "
        _Qry &= vbCrLf & "Where B.FTStyleCode='" & Me.FNHSysStyleId.Text & "'"
        _Qry &= vbCrLf & "AND A.FTPicType=" & CInt(_Type)
        _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        For Each R As DataRow In _oDt.Rows

            textBox122 = New ZBobb.AlphaBlendTextBox
            textBox122.BorderStyle = BorderStyle.Fixed3D

            textBox122.BackColor = Color.Orange
            textBox122.Multiline = True
            textBox122.ReadOnly = True
            ' textBox122.ForeColor = Color.Transparent
            textBox122.Font = New Font("Tahoma", 10, FontStyle.Bold)
            textBox122.ForeColor = Color.Black
            textBox122.ShortcutsEnabled = False

            Me.FTImage2.SendToBack()
            textBox122.Location = New System.Drawing.Point(CInt(Abs(R!FNPointX)), CInt(Abs(R!FNPointY)))
            textBox122.Size = New System.Drawing.Size(CInt(Abs(R!FNPicWidth)), CInt(Abs(R!FNPicHeight)))
            textBox122.Name = "" & R!FNSeq.ToString
            textBox122.Text = GetDefectQty(CInt("0" & R!FNSeq.ToString))
            'textBox122.ForeColor = Color.Red
            Me.FTImage2.Controls.Add(textBox122)
            textBox122.BringToFront()
            'AddHandler textBox12.MouseDown, AddressOf ObjClick
            'AddHandler textBox12.MouseMove, AddressOf Obj_MouseMove
        Next

    End Sub


    Private Sub LoadDrawRectangleRectangle2(ByVal _Type As Integer, ByVal img As DevExpress.XtraEditors.PictureEdit, ByVal _StyleCode As String)

        Dim _Qry As String = ""
        Dim _oDt As DataTable

        _Qry = "SELECT    A.FNHSysStyleId, A.FNSeq, (((A.FNPointX /480)*100)*380/100) AS FNPointX,(((A.FNPointY /480)*100)*380/100) AS FNPointY, A.FTPicType, (((A.FNPicHeight /480)*100)*380/100)  AS FNPicHeight ,(((A.FNPicWidth /480)*100)*380/100) AS FNPicWidth"
        _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTStylePoint AS A WITH(NOLOCK) LEFT OUTER JOIN "
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B WITH(NOLOCK) ON A.FNHSysStyleId = B.FNHSysStyleId "
        _Qry &= vbCrLf & "Where B.FTStyleCode='" & _StyleCode & "' and ( A.FNPointX > 0  and A.FNPointY > 0 and A.FNPicHeight > 0  and A.FNPicWidth > 0 )"
        _Qry &= vbCrLf & "AND A.FTPicType=" & CInt(_Type)
        _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        For Each R As DataRow In _oDt.Rows

            textBox122 = New ZBobb.AlphaBlendTextBox
            textBox122.BorderStyle = BorderStyle.Fixed3D

            textBox122.BackColor = Color.Orange
            textBox122.Multiline = True
            textBox122.ReadOnly = True
            ' textBox122.ForeColor = Color.Transparent
            textBox122.Font = New Font("Tahoma", 10, FontStyle.Bold)
            textBox122.ForeColor = Color.Black
            textBox122.ShortcutsEnabled = False

            img.SendToBack()
            textBox122.Location = New System.Drawing.Point(CInt(Abs(R!FNPointX)), CInt(Abs(R!FNPointY)))
            textBox122.Size = New System.Drawing.Size(CInt(Abs(R!FNPicWidth)), CInt(Abs(R!FNPicHeight)))
            textBox122.Name = "" & R!FNSeq.ToString
            textBox122.Text = GetDefectQty(CInt("0" & R!FNSeq.ToString))
            'textBox122.ForeColor = Color.Red
            img.Controls.Add(textBox122)
            textBox122.BringToFront()
            'AddHandler textBox12.MouseDown, AddressOf ObjClick
            'AddHandler textBox12.MouseMove, AddressOf Obj_MouseMove
        Next

    End Sub



    Private textBox1223 As ZBobb.AlphaBlendTextBox

    Private Label1 As DevExpress.XtraEditors.LabelControl
    'Private Sub LoadDrawRectangleRectangle3(ByVal _Type As Integer)

    '    Dim _Qry As String = ""
    '    Dim _oDt As DataTable

    '    _Qry = "SELECT    A.FNHSysStyleId, A.FNSeq, (((A.FNPointX /480)*100)*380/100) AS FNPointX,(((A.FNPointY /480)*100)*380/100) AS FNPointY, A.FTPicType, (((A.FNPicHeight /480)*100)*380/100)  AS FNPicHeight ,(((A.FNPicWidth /480)*100)*380/100) AS FNPicWidth"
    '    _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTStylePoint AS A WITH(NOLOCK) LEFT OUTER JOIN "
    '    _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B WITH(NOLOCK) ON A.FNHSysStyleId = B.FNHSysStyleId "
    '    _Qry &= vbCrLf & "Where LEFT(B.FTStyleCode,6)='" &  Me.FNHSysStyleId.Text  & "'"
    '    _Qry &= vbCrLf & "AND A.FTPicType=" & CInt(_Type)
    '    _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
    '    For Each R As DataRow In _oDt.Rows

    '        textBox1223 = New ZBobb.AlphaBlendTextBox
    '        textBox1223.BorderStyle = BorderStyle.Fixed3D

    '        textBox1223.BackColor = Color.Orange
    '        textBox1223.Multiline = True
    '        textBox1223.ReadOnly = True
    '        ' textBox122.ForeColor = Color.Transparent
    '        textBox1223.Font = New Font("Tahoma", 10, FontStyle.Bold)
    '        textBox1223.ForeColor = Color.Black
    '        textBox1223.ShortcutsEnabled = False

    '        Me.FTImage3.SendToBack()
    '        textBox1223.Location = New System.Drawing.Point(CInt(Abs(R!FNPointX)), CInt(Abs(R!FNPointY)))
    '        textBox1223.Size = New System.Drawing.Size(CInt(Abs(R!FNPicWidth)), CInt(Abs(R!FNPicHeight)))
    '        textBox1223.Name = "" & R!FNSeq.ToString
    '        textBox1223.Text = GetDefectQty(CInt("0" & R!FNSeq.ToString))
    '        'textBox1223.ForeColor = Color.Red
    '        Me.FTImage3.Controls.Add(textBox122)
    '        textBox1223.BringToFront()
    '        'AddHandler textBox12.MouseDown, AddressOf ObjClick
    '        'AddHandler textBox12.MouseMove, AddressOf Obj_MouseMove
    '    Next

    'End Sub


    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If Me.VerifyData() = False Then
                Exit Sub
            End If
            Me.LoadImageStyle()
            Me.LoadData()
            LoadGrid()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadData()
        Try
            Dim _Cmd As String = ""
            Dim _FTColorway As String = ""
            Dim _FTOrderNo As String = ""
            Dim _FTStyleCode As String = ""
            Dim _oDt As DataTable
            'Dim _oDt As DataTable



            _Cmd = " SELECT     FNHSysUnitSectId,  FDQADate  "
            _Cmd &= vbCrLf & ",        (SELECT TOP 1 STUFF"
            _Cmd &= vbCrLf & "                              ((SELECT ', ' + t2.FTOrderNo"
            _Cmd &= vbCrLf & "                              FROM      (SELECT   FTOrderNo  ,FNHSysUnitSectId,  FDQADate"
            _Cmd &= vbCrLf & "                                                      FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA AS Z with(NOLOCK) "
            _Cmd &= vbCrLf & "                                                      GROUP BY FTOrderNo ,FNHSysUnitSectId,  FDQADate  ) t2"
            _Cmd &= vbCrLf & "                                   WHERE   t2.FNHSysUnitSectId = V.FNHSysUnitSectId AND t2.FDQADate = V.FDQADate   FOR XML PATH('')), 1, 2, '') AS FTOrderNo)  as FTOrderNo"
            _Cmd &= vbCrLf & ",        (SELECT TOP 1 STUFF"
            _Cmd &= vbCrLf & "                               ((SELECT ', ' + t2.FTStyleCode"
            _Cmd &= vbCrLf & "                                FROM      (SELECT   Z.FNHSysStyleId  ,FNHSysUnitSectId,  FDQADate , S.FTStyleCode"
            _Cmd &= vbCrLf & "                                                   FROM      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA AS Z with(NOLOCK) "
            _Cmd &= vbCrLf & "												  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S WITH(NOLOCK) ON Z.FNHSysStyleId = S.FNHSysStyleId"
            _Cmd &= vbCrLf & "                                                  GROUP BY Z.FNHSysStyleId  ,FNHSysUnitSectId,  FDQADate , S.FTStyleCode ) t2"
            _Cmd &= vbCrLf & "                              WHERE   t2.FNHSysUnitSectId = V.FNHSysUnitSectId AND t2.FDQADate = V.FDQADate   FOR XML PATH('')), 1, 2, '') AS FTStyleCode)  as FTStyleCode"
            _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQA as V with(nolock)"
            _Cmd &= vbCrLf & "where FNHSysUnitSectId = " & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag.ToString)
            _Cmd &= vbCrLf & "And  FDQADate >= '" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'  and  FDQADate <= '" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "'"
            _Cmd &= vbCrLf & "group by FNHSysUnitSectId,  FDQADate "
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            _FTOrderNo = _oDt.Rows(0).Item("FTOrderNo").ToString
            _FTStyleCode = _oDt.Rows(0).Item("FTStyleCode").ToString

            If Me.FTOrderNo.Text <> "" Then
                _Cmd = "Select Top 1  b.FTColorway  From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline a  WITH(NOLOCK) "
                _Cmd &= vbCrLf & " left join  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo. TPRODTBundle AS b ON a.FTBarcodeNo = b.FTBarcodeBundleNo"
                _Cmd &= vbCrLf & " Where a.FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
                _Cmd &= vbCrLf & " and  a.FDDate between '" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'  and    '" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "'"
                _Cmd &= vbCrLf & "and  FNHSysUnitSectId = " & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag.ToString)

                _FTColorway = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN, "")
            Else
                _FTColorway = ""
                For Each str As String In _FTOrderNo.Split(",")
                    str = Microsoft.VisualBasic.Replace(str, " ", "")
                    '_Cmd = "Select Top 1  FTColorway  From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown WITH(NOLOCK) "
                    '_Cmd &= vbCrLf & " Where FTOrderNo='" & HI.UL.ULF.rpQuoted(str) & "'"
                    _Cmd = "Select  distinct   b.FTColorway  From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline a  WITH(NOLOCK) "
                    _Cmd &= vbCrLf & " left join  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo. TPRODTBundle AS b ON a.FTBarcodeNo = b.FTBarcodeBundleNo"
                    _Cmd &= vbCrLf & " Where a.FTOrderNo='" & HI.UL.ULF.rpQuoted(str) & "'"
                    _Cmd &= vbCrLf & " and  a.FDDate between '" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'  and    '" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "'"
                    _Cmd &= vbCrLf & "and  a.FNHSysUnitSectId = " & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag.ToString)
                    For Each a As DataRow In HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows
                        If _FTColorway <> "" Then _FTColorway &= ","
                        _FTColorway &= a!FTColorway.ToString  ' HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN, "")
                    Next

                Next

            End If



            Me.oFTColorway.Text = _FTColorway
            Me.oFNHSysUnitSectId.Text = Me.FNHSysUnitSectId.Text
            Me.oFNHSysStyleId.Text = _FTStyleCode
            Me.oFTOrderNo.Text = _FTOrderNo
            Me.oFNHSysUnitSectId_None.Text = Me.FNHSysUnitSectId_None.Text
            Me.oFNHSysStyleId_None.Text = Me.FNHSysStyleId_None.Text
            Me.oFDQADate.Text = Me.FDSDate.Text
            Me.oFDQADateE.Text = Me.FDEDate.Text

            If Me.FNHSysStyleId.Text = "" Then
                For Each str As String In _FTStyleCode.Split(",")
                    Me.FNHSysStyleId.Text = str
                    Exit For
                Next

                Call LoadImageStyle()

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadGrid()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable


            '_Cmd = "     DECLARE @DynamicPivotQuery AS NVARCHAR(MAX)"
            '_Cmd &= vbCrLf & " DECLARE @ColumnName varchar(max)"
            '_Cmd &= vbCrLf & "  DECLARE @ColumnNameMax varchar(max)            "
            '_Cmd &= vbCrLf & "   SELECT @ColumnName= ISNULL(@ColumnName + ',','') "
            '_Cmd &= vbCrLf & "      +  QUOTENAME(FNHourNo)"
            '_Cmd &= vbCrLf & "       FROM ("
            '_Cmd &= vbCrLf & "       SELECT  LEFT(A.FNHourNo,2) AS FNHourNo"
            '_Cmd &= vbCrLf & "       FROM         TPRODTQA_SubDetail AS A LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & " 							  HITECH_MASTER.dbo.TQAMQADetail AS B ON A.FNHSysQADetailId = B.FNHSysQADetailId"
            '_Cmd &= vbCrLf & " 		WHERE     (A.FDQADate >= '" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "' and A.FDQADate <= '" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "')"
            '_Cmd &= vbCrLf & " 		group by    LEFT(A.FNHourNo,2)"
            '_Cmd &= vbCrLf & "          ) AS Courses                "

            '_Cmd &= vbCrLf & " DECLARE @colNames varchar(max)  "

            '_Cmd &= vbCrLf & " select @colNames = STUFF((SELECT  '+ isnull(' + QUOTENAME(FNHourNo)+', 0) ' "
            '_Cmd &= vbCrLf & "         from ("
            '_Cmd &= vbCrLf & " SELECT  LEFT(A.FNHourNo,2) AS FNHourNo "
            '_Cmd &= vbCrLf & "  FROM         TPRODTQA_SubDetail AS A LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & "						  HITECH_MASTER.dbo.TQAMQADetail AS B ON A.FNHSysQADetailId = B.FNHSysQADetailId"
            '_Cmd &= vbCrLf & "	WHERE     (A.FDQADate >= '" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "' and A.FDQADate <= '" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "')"
            '_Cmd &= vbCrLf & "	group by    LEFT(A.FNHourNo,2)"
            '_Cmd &= vbCrLf & "      ) AS Courses  "
            '_Cmd &= vbCrLf & "        FOR XML PATH(''), TYPE"
            '_Cmd &= vbCrLf & "         ).value('.', 'NVARCHAR(MAX)') "
            '_Cmd &= vbCrLf & "      ,1,1,'')"

            '_Cmd &= vbCrLf & " Declare @ColumnNameSum nvarchar(max) "
            '_Cmd &= vbCrLf & "   select @ColumnNameSum = STUFF((SELECT  ', Sum(isnull(' + QUOTENAME(FNHourNo)+', 0)) AS ' +QUOTENAME(FNHourNo)+' '"
            '_Cmd &= vbCrLf & "   from ("
            '_Cmd &= vbCrLf & " SELECT  LEFT(A.FNHourNo,2) AS FNHourNo "
            '_Cmd &= vbCrLf & "  FROM         TPRODTQA_SubDetail AS A LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & " 						  HITECH_MASTER.dbo.TQAMQADetail AS B ON A.FNHSysQADetailId = B.FNHSysQADetailId"
            '_Cmd &= vbCrLf & " 	WHERE     (A.FDQADate >= '" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "' and A.FDQADate <= '" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "')"
            '_Cmd &= vbCrLf & " 	group by    LEFT(A.FNHourNo,2)"
            '_Cmd &= vbCrLf & "      ) AS Courses  "
            '_Cmd &= vbCrLf & "        FOR XML PATH(''), TYPE"
            '_Cmd &= vbCrLf & "         ).value('.', 'NVARCHAR(MAX)') "
            '_Cmd &= vbCrLf & "      ,1,1,'')"


            '_Cmd &= vbCrLf & "        SET @DynamicPivotQuery =   N'SELECT FNHSysStyleId, FNHSysUnitSectId, FTOrderNo,    FNHSysQADetailId, ' + @ColumnName + '"
            '_Cmd &= vbCrLf & " 		INTO #Tmp "
            '_Cmd &= vbCrLf & " 		From ("
            '_Cmd &= vbCrLf & " 		SELECT COUNT(*) AS Qty, A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo, A.FDQADate,   A.FNHSysQADetailId, LEFT(A.FNHourNo,2) AS FNHourNo "
            '_Cmd &= vbCrLf & " 		FROM         TPRODTQA_SubDetail AS A LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & " 							  HITECH_MASTER.dbo.TQAMQADetail AS B ON A.FNHSysQADetailId = B.FNHSysQADetailId"
            '_Cmd &= vbCrLf & " 		WHERE     (A.FDQADate >= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'' and A.FDQADate <= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "'')"
            '_Cmd &= vbCrLf & " 		group by     A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo,  A.FDQADate,  A.FNHSysQADetailId, B.FTQADetailCode, B.FTQADetailNameTH, "
            '_Cmd &= vbCrLf & "                   B.FTQADetailNameEN ,  LEFT(A.FNHourNo,2)) AS A       "
            '_Cmd &= vbCrLf & "      PIVOT(sum(Qty) FOR   FNHourNo IN (' + @ColumnName + ')) AS PVTTable"


            '_Cmd &= vbCrLf & "  Select '''' AS FNSeq , FNHSysStyleId, FNHSysUnitSectId, FTOrderNo,  '''' as FNHSysQADetailId, '''' as  FTQADetailCode "
            'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            '    _Cmd &= vbCrLf & ", ''จำนวนงานออก'' as FTQADetailName"
            'Else
            '    _Cmd &= vbCrLf & ",''Output'' as FTQADetailName"
            'End If
            '_Cmd &= vbCrLf & " , ' + @ColumnName + ', ( '+ @colNames +')  AS Total"
            '_Cmd &= vbCrLf & " From ( SELECT     FNHSysStyleId, FNHSysUnitSectId, FTOrderNo,  FNQAInQty, LEFT(FNHourNo,2) AS FNHourNo"
            '_Cmd &= vbCrLf & "           FROM TPRODTQA"
            '_Cmd &= vbCrLf & " where FDQADate >= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'' and FDQADate <= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "''"
            '_Cmd &= vbCrLf & "   and  FNHSysUnitSectId=" & Me.FNHSysUnitSectId.Properties.Tag & " and  FNHSysStyleId = " & Me.FNHSysStyleId.Properties.Tag & "  and FTOrderNo=''" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'') AS A"
            '_Cmd &= vbCrLf & " pivot (Sum(FNQAInQty) for FNHourNo in(' + @ColumnName + ')) AS T0     "
            '_Cmd &= vbCrLf & "             UNION"
            '_Cmd &= vbCrLf & " Select '''' AS FNSeq , FNHSysStyleId, FNHSysUnitSectId, FTOrderNo,  '''' as FNHSysQADetailId, '''' as  FTQADetailCode "
            'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            '    _Cmd &= vbCrLf & ",''จำนวนสุ่มตรวจ'' as FTQADetailName"
            'Else
            '    _Cmd &= vbCrLf & ",''Rendom'' as FTQADetailName"
            'End If
            '_Cmd &= vbCrLf & " ,' + @ColumnName + ', ( '+ @colNames +')  AS Total"
            '_Cmd &= vbCrLf & " From (SELECT     FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, LEFT(FNHourNo,2) AS FNHourNo,   FNQAActualQty  "
            '_Cmd &= vbCrLf & "            FROM TPRODTQA"
            '_Cmd &= vbCrLf & " WHERE FDQADate >= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'' and FDQADate <= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "''"
            '_Cmd &= vbCrLf & "   and  FNHSysUnitSectId=" & Me.FNHSysUnitSectId.Properties.Tag & " and  FNHSysStyleId = " & Me.FNHSysStyleId.Properties.Tag & " and FTOrderNo=''" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'') AS B"
            '_Cmd &= vbCrLf & " Pivot (sum(FNQAActualQty) for FNHourNo in (' + @ColumnName + ')) AS T1"
            '_Cmd &= vbCrLf & "          UNION"
            '_Cmd &= vbCrLf & " Select '''' AS FNSeq , FNHSysStyleId, FNHSysUnitSectId, FTOrderNo,'''' as FNHSysQADetailId, '''' as  FTQADetailCode "
            'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            '    _Cmd &= vbCrLf & ", ''อันดง'' as FTQADetailName"
            'Else
            '    _Cmd &= vbCrLf & ",''Andon'' as FTQADetailName"
            'End If
            '_Cmd &= vbCrLf & " , ' + @ColumnName + ', ( '+ @colNames +')  AS Total"
            '_Cmd &= vbCrLf & "  From ( SELECT     FNHSysStyleId, FNHSysUnitSectId, FTOrderNo,  Isnull(FNAndon,0) AS FNAndon , LEFT(FNHourNo,2) AS FNHourNo"
            '_Cmd &= vbCrLf & "            FROM TPRODTQA"
            '_Cmd &= vbCrLf & " where FDQADate >= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'' and FDQADate <= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "''"
            '_Cmd &= vbCrLf & "   and  FNHSysUnitSectId=" & Me.FNHSysUnitSectId.Properties.Tag & " and  FNHSysStyleId = " & Me.FNHSysStyleId.Properties.Tag & " and FTOrderNo=''" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'') AS C "
            '_Cmd &= vbCrLf & " pivot (Sum(FNAndon) for FNHourNo in(' + @ColumnName + ')) AS T2"
            '_Cmd &= vbCrLf & "             UNION"
            '_Cmd &= vbCrLf & "  SELECT  convert(nvarchar(3), ROW_NUMBER() Over (Order By A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo,    A.FNHSysQADetailId)) AS FNSeq"

            '_Cmd &= vbCrLf & "     ,  A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo,    A.FNHSysQADetailId, B.FTQADetailCode "
            'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            '    _Cmd &= vbCrLf & ", B.FTQADetailNameTH as FTQADetailName"
            'Else
            '    _Cmd &= vbCrLf & ", B.FTQADetailNameEN as FTQADetailName"
            'End If
            '_Cmd &= vbCrLf & "                   ,' + @ColumnNameSum + ', sum( '+ @colNames +')  AS Total"
            '_Cmd &= vbCrLf & " FROM         TPRODTQA_SubDetail AS A LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & "                     HITECH_MASTER.dbo.TQAMQADetail AS B ON A.FNHSysQADetailId = B.FNHSysQADetailId"
            '_Cmd &= vbCrLf & "                      LEFT OUTER JOIN #Tmp AS T ON A.FNHSysQADetailId = T.FNHSysQADetailId and A.FNHSysStyleId = T.FNHSysStyleId"
            '_Cmd &= vbCrLf & " 					and A.FNHSysUnitSectId = T.FNHSysUnitSectId  "
            '_Cmd &= vbCrLf & " WHERE     (A.FDQADate >= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'' and A.FDQADate <= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "''  )"
            '_Cmd &= vbCrLf & "   and  A.FNHSysUnitSectId=" & Me.FNHSysUnitSectId.Properties.Tag & " "
            '_Cmd &= vbCrLf & " and A.FNHSysStyleId=" & Me.FNHSysStyleId.Properties.Tag & " "
            '_Cmd &= vbCrLf & " and A.FTOrderNo=''" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "''"
            '_Cmd &= vbCrLf & " group by A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo,    A.FNHSysQADetailId, B.FTQADetailCode, B.FTQADetailNameTH, "
            '_Cmd &= vbCrLf & "                    B.FTQADetailNameEN  '"

            '_Cmd &= vbCrLf & "      EXEC sp_executesql @DynamicPivotQuery    "




            '_Cmd = "    DECLARE @DynamicPivotQuery AS NVARCHAR(MAX)"
            '_Cmd &= vbCrLf & "DECLARE @ColumnName varchar(max)"
            '_Cmd &= vbCrLf & " DECLARE @ColumnNameMax varchar(max) ,@ColumnDefault varchar(max)"
            '_Cmd &= vbCrLf & " Set @ColumnDefault = '[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12]'"
            '_Cmd &= vbCrLf & " Set @ColumnName = @ColumnDefault"

            '_Cmd &= vbCrLf & "DECLARE @colNames varchar(max)  "
            '_Cmd &= vbCrLf & " Declare @ColumnNameSum nvarchar(max) "
            '_Cmd &= vbCrLf & " SET @ColumnNameSum =  'sum(Isnull([1],0)) AS [1],sum(Isnull([2],0)) AS [2],sum(Isnull([3],0)) AS [3],sum(Isnull([4],0)) AS [4],sum(Isnull([5],0)) AS [5],sum(Isnull([6],0)) AS [6],sum(Isnull([7],0)) AS [7]"
            '_Cmd &= vbCrLf & "		,sum(Isnull([8],0)) AS [8],sum(Isnull([9],0)) AS [9],sum(Isnull([10],0)) AS [10],sum(Isnull([11],0)) AS [11],sum(Isnull([12],0)) AS [12]'"
            '_Cmd &= vbCrLf & "SET @colNames =  'Isnull([1],0) +Isnull([2],0) +Isnull([3],0) +Isnull([4],0) +Isnull([5],0) +Isnull([6],0) +Isnull([7],0) +"
            '_Cmd &= vbCrLf & "				Isnull([8],0) +Isnull([9],0) +Isnull([10],0) +Isnull([11],0) +Isnull([12],0) '"



            '_Cmd &= vbCrLf & " Delete From TmpTPRODTQA_Report_Detail Where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '_Cmd &= vbCrLf & "  SET @DynamicPivotQuery =   N'SELECT FNHSysStyleId, FNHSysUnitSectId, FTOrderNo,    FNHSysQADetailId, ' + @ColumnName + '"
            '_Cmd &= vbCrLf & "	INTO #Tmp "
            '_Cmd &= vbCrLf & "	From ("
            '_Cmd &= vbCrLf & "	SELECT COUNT(*) AS Qty, A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo, A.FDQADate,   A.FNHSysQADetailId"
            '_Cmd &= vbCrLf & "	,  CASE  "
            '_Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''0800'' and A.FNHourNo < ''0900'' Then ''1''"
            '_Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''0900'' and A.FNHourNo < ''1000'' Then ''2''"
            '_Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1000'' and A.FNHourNo < ''1100'' Then ''3''"
            '_Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1100'' and A.FNHourNo <= ''1200'' Then ''4''"
            '_Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1300'' and A.FNHourNo < ''1400'' Then ''5''"
            '_Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1400'' and A.FNHourNo < ''1500'' Then ''6''"
            '_Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1500'' and A.FNHourNo < ''1600'' Then ''7''"
            '_Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1600'' and A.FNHourNo < ''1700'' Then ''8''"
            '_Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1730'' and A.FNHourNo < ''1830'' Then ''9''"
            '_Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1830'' and A.FNHourNo < ''1930'' Then ''10''"
            '_Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1930'' and A.FNHourNo < ''2030'' Then ''11''"
            '_Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''2030'' and A.FNHourNo < ''2130'' Then ''12''"
            '_Cmd &= vbCrLf & "	ELSE ''''"
            '_Cmd &= vbCrLf & "	END FNHourNo"
            '_Cmd &= vbCrLf & "	FROM         TPRODTQA_SubDetail AS A LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & "						  HITECH_MASTER.dbo.TQAMQADetail AS B ON A.FNHSysQADetailId = B.FNHSysQADetailId"

            '_Cmd &= vbCrLf & " 		WHERE     (A.FDQADate >= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'' and A.FDQADate <= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "'')"
            '_Cmd &= vbCrLf & "   and  A.FNHSysUnitSectId=" & Me.FNHSysUnitSectId.Properties.Tag & " " 'and  A.FNHSysStyleId = " & Me.FNHSysStyleId.Properties.Tag & "  and A.FTOrderNo=''" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "''
            '_Cmd &= vbCrLf & "	group by     A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo,  A.FDQADate,  A.FNHSysQADetailId, B.FTQADetailCode, B.FTQADetailNameTH, "
            '_Cmd &= vbCrLf & "              B.FTQADetailNameEN ,  A.FNHourNo) AS A       "
            '_Cmd &= vbCrLf & "   PIVOT(sum(Qty) FOR   FNHourNo IN (' + @ColumnName + ')) AS PVTTable"

            '_Cmd &= vbCrLf & " INSERT INTO TmpTPRODTQA_Report_Detail( FTUserLogIn,FNSeq,  FNHSysStyleId, FNHSysUnitSectId, FTOrderNo,FNHSysQADetailId , FTQADetailCode, FTDefectTH, FTDefectEN, [1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12], FNTotal)"

            '_Cmd &= vbCrLf & "Select ''" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'',* From ("
            '_Cmd &= vbCrLf & " Select '''' AS FNSeq , FNHSysStyleId, FNHSysUnitSectId, FTOrderNo,  '''' as FNHSysQADetailId, ''0.1'' as  FTQADetailCode "

            ''If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            '_Cmd &= vbCrLf & ", ''จำนวนงานออก'' as FTQADetailNameTH"
            ''Else
            '_Cmd &= vbCrLf & ",''Output'' as FTQADetailNameEN"
            ''End If
            '_Cmd &= vbCrLf & ", ' + @ColumnName + ', ( '+ @colNames +')  AS Total"
            '_Cmd &= vbCrLf & " From ( SELECT     FNHSysStyleId, FNHSysUnitSectId, FTOrderNo,  FNQAInQty, "
            '_Cmd &= vbCrLf & "  CASE  "
            '_Cmd &= vbCrLf & "		WHEN FNHourNo >= ''0800'' and FNHourNo < ''0900'' Then ''1''"
            '_Cmd &= vbCrLf & "		WHEN FNHourNo >= ''0900'' and FNHourNo < ''1000'' Then ''2''"
            '_Cmd &= vbCrLf & "		WHEN FNHourNo >= ''1000'' and FNHourNo < ''1100'' Then ''3''"
            '_Cmd &= vbCrLf & "		WHEN FNHourNo >= ''1100'' and FNHourNo <= ''1200'' Then ''4''"
            '_Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1300'' and FNHourNo < ''1400'' Then ''5''"
            '_Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1400'' and FNHourNo < ''1500'' Then ''6''"
            '_Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1500'' and FNHourNo < ''1600'' Then ''7''"
            '_Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1600'' and FNHourNo < ''1700'' Then ''8''"
            '_Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1730'' and FNHourNo < ''1830'' Then ''9''"
            '_Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1830'' and FNHourNo < ''1930'' Then ''10''"
            '_Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1930'' and FNHourNo < ''2030'' Then ''11''"
            '_Cmd &= vbCrLf & "	WHEN FNHourNo >= ''2030'' and FNHourNo < ''2130'' Then ''12''"
            '_Cmd &= vbCrLf & "	ELSE ''''"
            '_Cmd &= vbCrLf & "	END FNHourNo"
            '_Cmd &= vbCrLf & "         FROM TPRODTQA"

            '_Cmd &= vbCrLf & " where FDQADate >= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'' and FDQADate <= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "''"
            '_Cmd &= vbCrLf & "   and  FNHSysUnitSectId=" & Me.FNHSysUnitSectId.Properties.Tag & " ) AS A" 'and  FNHSysStyleId = " & Me.FNHSysStyleId.Properties.Tag & "  and FTOrderNo=''" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "''

            '_Cmd &= vbCrLf & "pivot (Sum(FNQAInQty) for FNHourNo in(' + @ColumnName + ')) AS T0     "
            '_Cmd &= vbCrLf & "           UNION"
            '_Cmd &= vbCrLf & "Select '''' AS FNSeq , FNHSysStyleId, FNHSysUnitSectId, FTOrderNo,  '''' as FNHSysQADetailId, ''0.2'' as  FTQADetailCode "

            ''If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            '_Cmd &= vbCrLf & ",''จำนวนสุ่มตรวจ'' as FTQADetailNameTH"
            ''Else
            '_Cmd &= vbCrLf & ",''Random'' as FTQADetailNameEN"
            ''End If
            '_Cmd &= vbCrLf & " ,' + @ColumnName + ', ( '+ @colNames +')  AS Total"
            '_Cmd &= vbCrLf & " From (SELECT     FNHSysStyleId, FNHSysUnitSectId, FTOrderNo,"
            '_Cmd &= vbCrLf & " CASE  "
            '_Cmd &= vbCrLf & "	WHEN FNHourNo >= ''0800'' and FNHourNo < ''0900'' Then ''1''"
            '_Cmd &= vbCrLf & "	WHEN FNHourNo >= ''0900'' and FNHourNo < ''1000'' Then ''2''"
            '_Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1000'' and FNHourNo < ''1100'' Then ''3''"
            '_Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1100'' and FNHourNo <= ''1200'' Then ''4''"
            '_Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1300'' and FNHourNo < ''1400'' Then ''5''"
            '_Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1400'' and FNHourNo < ''1500'' Then ''6''"
            '_Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1500'' and FNHourNo < ''1600'' Then ''7''"
            '_Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1600'' and FNHourNo < ''1700'' Then ''8''"
            '_Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1730'' and FNHourNo < ''1830'' Then ''9''"
            '_Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1830'' and FNHourNo < ''1930'' Then ''10''"
            '_Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1930'' and FNHourNo < ''2030'' Then ''11''"
            '_Cmd &= vbCrLf & "WHEN FNHourNo >= ''2030'' and FNHourNo < ''2130'' Then ''12''"
            '_Cmd &= vbCrLf & "ELSE ''''"
            '_Cmd &= vbCrLf & "	END FNHourNo"

            '_Cmd &= vbCrLf & ",   FNQAActualQty  "
            '_Cmd &= vbCrLf & "          FROM TPRODTQA"

            '_Cmd &= vbCrLf & " WHERE FDQADate >= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'' and FDQADate <= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "''"
            '_Cmd &= vbCrLf & "   and  FNHSysUnitSectId=" & Me.FNHSysUnitSectId.Properties.Tag & ") AS B" ' and  FNHSysStyleId = " & Me.FNHSysStyleId.Properties.Tag & " and FTOrderNo=''" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "''

            '_Cmd &= vbCrLf & "Pivot (sum(FNQAActualQty) for FNHourNo in (' + @ColumnName + ')) AS T1"
            '_Cmd &= vbCrLf & "           UNION"
            '_Cmd &= vbCrLf & "Select '''' AS FNSeq , FNHSysStyleId, FNHSysUnitSectId, FTOrderNo,'''' as FNHSysQADetailId, ''0.3'' as  FTQADetailCode "

            ''If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            '_Cmd &= vbCrLf & ", ''จำนวนเสีย/ตัว'' as FTQADetailNameTH"
            ''Else
            '_Cmd &= vbCrLf & ",''Defect /Pcs.'' as FTQADetailNameEN"
            ''End If
            '_Cmd &= vbCrLf & ", ' + @ColumnName + ', ( '+ @colNames +')  AS Total"
            '_Cmd &= vbCrLf & " From ( SELECT     FNHSysStyleId, FNHSysUnitSectId, FTOrderNo,  Isnull(FNSeq,0) AS FNSeq ,"
            '_Cmd &= vbCrLf & " CASE  "
            '_Cmd &= vbCrLf & "	WHEN FNHourNo >= ''0800'' and FNHourNo < ''0900'' Then ''1''"
            '_Cmd &= vbCrLf & "	WHEN FNHourNo >= ''0900'' and FNHourNo < ''1000'' Then ''2''"
            '_Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1000'' and FNHourNo < ''1100'' Then ''3''"
            '_Cmd &= vbCrLf & "WHEN FNHourNo >= ''1100'' and FNHourNo <= ''1200'' Then ''4''"
            '_Cmd &= vbCrLf & "WHEN FNHourNo >= ''1300'' and FNHourNo < ''1400'' Then ''5''"
            '_Cmd &= vbCrLf & "WHEN FNHourNo >= ''1400'' and FNHourNo < ''1500'' Then ''6''"
            '_Cmd &= vbCrLf & "WHEN FNHourNo >= ''1500'' and FNHourNo < ''1600'' Then ''7''"
            '_Cmd &= vbCrLf & "WHEN FNHourNo >= ''1600'' and FNHourNo < ''1700'' Then ''8''"
            '_Cmd &= vbCrLf & "WHEN FNHourNo >= ''1730'' and FNHourNo < ''1830'' Then ''9''"
            '_Cmd &= vbCrLf & "WHEN FNHourNo >= ''1830'' and FNHourNo < ''1930'' Then ''10''"
            '_Cmd &= vbCrLf & "WHEN FNHourNo >= ''1930'' and FNHourNo < ''2030'' Then ''11''"
            '_Cmd &= vbCrLf & "WHEN FNHourNo >= ''2030'' and FNHourNo < ''2130'' Then ''12''"
            '_Cmd &= vbCrLf & "ELSE ''''"
            '_Cmd &= vbCrLf & "END FNHourNo"
            '_Cmd &= vbCrLf & "    FROM TPRODTQA_Detail"


            '_Cmd &= vbCrLf & " where FDQADate >= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'' and FDQADate <= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "'' and   FTStateReject = ''1''"
            '_Cmd &= vbCrLf & "   and  FNHSysUnitSectId=" & Me.FNHSysUnitSectId.Properties.Tag & ") AS C " ' and  FNHSysStyleId = " & Me.FNHSysStyleId.Properties.Tag & " and FTOrderNo=''" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "''

            '_Cmd &= vbCrLf & "pivot (count(FNSeq) for FNHourNo in(' + @ColumnName + ')) AS T2"
            '_Cmd &= vbCrLf & "        UNION"
            '_Cmd &= vbCrLf & "SELECT  convert(nvarchar(3), ROW_NUMBER() Over (Order By A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo,    A.FNHSysQADetailId)) AS FNSeq"
            '_Cmd &= vbCrLf & "   ,  A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo,    A.FNHSysQADetailId, B.FTQADetailCode "

            ''If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            '_Cmd &= vbCrLf & ", B.FTQADetailNameTH as FTQADetailNameTH"
            ''Else
            '_Cmd &= vbCrLf & ", B.FTQADetailNameEN as FTQADetailNameEN"
            ''End If
            '_Cmd &= vbCrLf & "  ,' + @ColumnNameSum + ', sum( '+ @colNames +')  AS Total"
            '_Cmd &= vbCrLf & "FROM (SELECT distinct   FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate,  FNHSysQADetailId" 'FTPointSubName, 
            '_Cmd &= vbCrLf & "FROM            TPRODTQA_SubDetail With(NOLOCK) ) AS A LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & "                    HITECH_MASTER.dbo.TQAMQADetail AS B WITH(NOLOCK) ON A.FNHSysQADetailId = B.FNHSysQADetailId"
            '_Cmd &= vbCrLf & "           LEFT OUTER JOIN #Tmp AS T ON A.FNHSysQADetailId = T.FNHSysQADetailId and A.FNHSysStyleId = T.FNHSysStyleId"
            '_Cmd &= vbCrLf & "				and A.FNHSysUnitSectId = T.FNHSysUnitSectId  "

            '_Cmd &= vbCrLf & " WHERE     (A.FDQADate >= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'' and A.FDQADate <= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "''  )"
            '_Cmd &= vbCrLf & "   and  A.FNHSysUnitSectId=" & Me.FNHSysUnitSectId.Properties.Tag & " "
            ''_Cmd &= vbCrLf & " and A.FNHSysStyleId=" & Me.FNHSysStyleId.Properties.Tag & " "
            ''_Cmd &= vbCrLf & " and A.FTOrderNo=''" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "''"
            '_Cmd &= vbCrLf & " group by A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo,    A.FNHSysQADetailId, B.FTQADetailCode, B.FTQADetailNameTH, "
            '_Cmd &= vbCrLf & "B.FTQADetailNameEN  ) AS T' "

            '_Cmd &= vbCrLf & "     EXEC sp_executesql @DynamicPivotQuery    "

            '_Cmd &= vbCrLf & " Select FTUserLogIn, FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate, FNSeq,  [1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12], FNTotal as Total , FTQADetailCode, "
            '_Cmd &= vbCrLf & "  FNHSysQADetailId "
            'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            '    _Cmd &= vbCrLf & ", FTDefectTH as FTQADetailName"
            'Else
            '    _Cmd &= vbCrLf & ", FTDefectEN as FTQADetailName"
            'End If

            '_Cmd &= vbCrLf & "  From TmpTPRODTQA_Report_Detail Where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '_Cmd &= vbCrLf & "  Order by case when isnull(FNSeq,'') = '' then convert(numeric(18,2),FTQADetailCode)   else convert(numeric(18,2),FNSeq) end asc "




            _Cmd = "    DECLARE @DynamicPivotQuery AS NVARCHAR(MAX)"
            _Cmd &= vbCrLf & "DECLARE @ColumnName varchar(max)"
            _Cmd &= vbCrLf & " DECLARE @ColumnNameMax varchar(max) ,@ColumnDefault varchar(max)"
            _Cmd &= vbCrLf & " Set @ColumnDefault = '[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12]'"
            _Cmd &= vbCrLf & " Set @ColumnName = @ColumnDefault"

            _Cmd &= vbCrLf & "DECLARE @colNames varchar(max)  "
            _Cmd &= vbCrLf & " Declare @ColumnNameSum nvarchar(max) "
            _Cmd &= vbCrLf & " SET @ColumnNameSum =  'sum(Isnull([1],0)) AS [1],sum(Isnull([2],0)) AS [2],sum(Isnull([3],0)) AS [3],sum(Isnull([4],0)) AS [4],sum(Isnull([5],0)) AS [5],sum(Isnull([6],0)) AS [6],sum(Isnull([7],0)) AS [7]"
            _Cmd &= vbCrLf & "		,sum(Isnull([8],0)) AS [8],sum(Isnull([9],0)) AS [9],sum(Isnull([10],0)) AS [10],sum(Isnull([11],0)) AS [11],sum(Isnull([12],0)) AS [12]'"
            _Cmd &= vbCrLf & "SET @colNames =  'Isnull([1],0) +Isnull([2],0) +Isnull([3],0) +Isnull([4],0) +Isnull([5],0) +Isnull([6],0) +Isnull([7],0) +"
            _Cmd &= vbCrLf & "				Isnull([8],0) +Isnull([9],0) +Isnull([10],0) +Isnull([11],0) +Isnull([12],0) '"



            _Cmd &= vbCrLf & " Delete From TmpTPRODTQA_Report_Detail Where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & "  SET @DynamicPivotQuery =   N'SELECT  FNHSysUnitSectId,    FNHSysQADetailId, ' + @ColumnName + '"
            _Cmd &= vbCrLf & "	INTO #Tmp "
            _Cmd &= vbCrLf & "	From ("
            _Cmd &= vbCrLf & "	SELECT COUNT(*) AS Qty, A.FNHSysUnitSectId,  A.FDQADate,   A.FNHSysQADetailId"
            _Cmd &= vbCrLf & "	,  CASE  "
            _Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''0800'' and A.FNHourNo < ''0900'' Then ''1''"
            _Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''0900'' and A.FNHourNo < ''1000'' Then ''2''"
            _Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1000'' and A.FNHourNo < ''1100'' Then ''3''"
            _Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1100'' and A.FNHourNo <= ''1200'' Then ''4''"
            _Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1300'' and A.FNHourNo < ''1400'' Then ''5''"
            _Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1400'' and A.FNHourNo < ''1500'' Then ''6''"
            _Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1500'' and A.FNHourNo < ''1600'' Then ''7''"
            _Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1600'' and A.FNHourNo < ''1700'' Then ''8''"
            _Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1730'' and A.FNHourNo < ''1830'' Then ''9''"
            _Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1830'' and A.FNHourNo < ''1930'' Then ''10''"
            _Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1930'' and A.FNHourNo < ''2030'' Then ''11''"
            _Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''2030'' and A.FNHourNo < ''2130'' Then ''12''"
            _Cmd &= vbCrLf & "	ELSE ''''"
            _Cmd &= vbCrLf & "	END FNHourNo"
            _Cmd &= vbCrLf & "	FROM         TPRODTQA_SubDetail AS A LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "						  HITECH_MASTER.dbo.TQAMQADetail AS B ON A.FNHSysQADetailId = B.FNHSysQADetailId"

            _Cmd &= vbCrLf & " 		WHERE     (A.FDQADate >= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'' and A.FDQADate <= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "'')"
            _Cmd &= vbCrLf & "   and  A.FNHSysUnitSectId=" & Me.FNHSysUnitSectId.Properties.Tag & " " 'and  A.FNHSysStyleId = " & Me.FNHSysStyleId.Properties.Tag & "  and A.FTOrderNo=''" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "''
            _Cmd &= vbCrLf & "	group by     A.FNHSysUnitSectId, A.FDQADate,  A.FNHSysQADetailId, B.FTQADetailCode, B.FTQADetailNameTH, "
            _Cmd &= vbCrLf & "              B.FTQADetailNameEN ,  A.FNHourNo) AS A       "
            _Cmd &= vbCrLf & "   PIVOT(sum(Qty) FOR   FNHourNo IN (' + @ColumnName + ')) AS PVTTable"

            _Cmd &= vbCrLf & " INSERT INTO TmpTPRODTQA_Report_Detail(FNSeqMain, FTUserLogIn,FNSeq,  FNHSysStyleId, FNHSysUnitSectId, FTOrderNo,FNHSysQADetailId , FTQADetailCode, FTDefectTH, FTDefectEN, [1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12], FNTotal)"

            _Cmd &= vbCrLf & "Select 1, ''" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'',* From ("
            _Cmd &= vbCrLf & " Select '''' AS FNSeq , 0 as  FNHSysStyleId, FNHSysUnitSectId,'''' as  FTOrderNo,  '''' as FNHSysQADetailId, ''0.1'' as  FTQADetailCode "

            'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Cmd &= vbCrLf & ", ''จำนวนงานออก'' as FTQADetailNameTH"
            'Else
            _Cmd &= vbCrLf & ",''Output'' as FTQADetailNameEN"
            'End If
            _Cmd &= vbCrLf & ", ' + @ColumnName + ', ( '+ @colNames +')  AS Total"
            _Cmd &= vbCrLf & " From ( SELECT    FNHSysUnitSectId,   FNQAInQty, "
            _Cmd &= vbCrLf & "  CASE  "
            _Cmd &= vbCrLf & "		WHEN FNHourNo >= ''0800'' and FNHourNo < ''0900'' Then ''1''"
            _Cmd &= vbCrLf & "		WHEN FNHourNo >= ''0900'' and FNHourNo < ''1000'' Then ''2''"
            _Cmd &= vbCrLf & "		WHEN FNHourNo >= ''1000'' and FNHourNo < ''1100'' Then ''3''"
            _Cmd &= vbCrLf & "		WHEN FNHourNo >= ''1100'' and FNHourNo <= ''1200'' Then ''4''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1300'' and FNHourNo < ''1400'' Then ''5''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1400'' and FNHourNo < ''1500'' Then ''6''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1500'' and FNHourNo < ''1600'' Then ''7''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1600'' and FNHourNo < ''1700'' Then ''8''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1730'' and FNHourNo < ''1830'' Then ''9''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1830'' and FNHourNo < ''1930'' Then ''10''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1930'' and FNHourNo < ''2030'' Then ''11''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''2030'' and FNHourNo < ''2130'' Then ''12''"
            _Cmd &= vbCrLf & "	ELSE ''''"
            _Cmd &= vbCrLf & "	END FNHourNo"
            _Cmd &= vbCrLf & "         FROM TPRODTQA"

            _Cmd &= vbCrLf & " where FDQADate >= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'' and FDQADate <= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "''"
            _Cmd &= vbCrLf & "   and  FNHSysUnitSectId=" & Me.FNHSysUnitSectId.Properties.Tag & " ) AS A" 'and  FNHSysStyleId = " & Me.FNHSysStyleId.Properties.Tag & "  and FTOrderNo=''" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "''

            _Cmd &= vbCrLf & "pivot (Sum(FNQAInQty) for FNHourNo in(' + @ColumnName + ')) AS T0     "
            _Cmd &= vbCrLf & "           UNION"
            _Cmd &= vbCrLf & "Select '''' AS FNSeq , 0 as FNHSysStyleId, FNHSysUnitSectId, '''' as FTOrderNo,  '''' as FNHSysQADetailId, ''0.2'' as  FTQADetailCode "

            'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Cmd &= vbCrLf & ",''จำนวนสุ่มตรวจ'' as FTQADetailNameTH"
            'Else
            _Cmd &= vbCrLf & ",''Random'' as FTQADetailNameEN"
            'End If
            _Cmd &= vbCrLf & " ,' + @ColumnName + ', ( '+ @colNames +')  AS Total"
            _Cmd &= vbCrLf & " From (SELECT    FNHSysUnitSectId, "
            _Cmd &= vbCrLf & " CASE  "
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''0800'' and FNHourNo < ''0900'' Then ''1''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''0900'' and FNHourNo < ''1000'' Then ''2''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1000'' and FNHourNo < ''1100'' Then ''3''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1100'' and FNHourNo <= ''1200'' Then ''4''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1300'' and FNHourNo < ''1400'' Then ''5''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1400'' and FNHourNo < ''1500'' Then ''6''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1500'' and FNHourNo < ''1600'' Then ''7''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1600'' and FNHourNo < ''1700'' Then ''8''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1730'' and FNHourNo < ''1830'' Then ''9''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1830'' and FNHourNo < ''1930'' Then ''10''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1930'' and FNHourNo < ''2030'' Then ''11''"
            _Cmd &= vbCrLf & "WHEN FNHourNo >= ''2030'' and FNHourNo < ''2130'' Then ''12''"
            _Cmd &= vbCrLf & "ELSE ''''"
            _Cmd &= vbCrLf & "	END FNHourNo"

            _Cmd &= vbCrLf & ",   FNQAActualQty  "
            _Cmd &= vbCrLf & "          FROM TPRODTQA"

            _Cmd &= vbCrLf & " WHERE FDQADate >= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'' and FDQADate <= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "''"
            _Cmd &= vbCrLf & "   and  FNHSysUnitSectId=" & Me.FNHSysUnitSectId.Properties.Tag & ") AS B" ' and  FNHSysStyleId = " & Me.FNHSysStyleId.Properties.Tag & " and FTOrderNo=''" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "''

            _Cmd &= vbCrLf & "Pivot (sum(FNQAActualQty) for FNHourNo in (' + @ColumnName + ')) AS T1"
            _Cmd &= vbCrLf & "           UNION"
            _Cmd &= vbCrLf & "Select '''' AS FNSeq , 0 as  FNHSysStyleId, FNHSysUnitSectId, '''' as FTOrderNo,'''' as FNHSysQADetailId, ''0.3'' as  FTQADetailCode "

            'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Cmd &= vbCrLf & ", ''จำนวนเสีย/ตัว'' as FTQADetailNameTH"
            'Else
            _Cmd &= vbCrLf & ",''Defect /Pcs.'' as FTQADetailNameEN"
            'End If
            _Cmd &= vbCrLf & ", ' + @ColumnName + ', ( '+ @colNames +')  AS Total"
            _Cmd &= vbCrLf & " From ( SELECT   FNHSysUnitSectId,    Isnull(FNSeq,0) AS FNSeq ,"
            _Cmd &= vbCrLf & " CASE  "
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''0800'' and FNHourNo < ''0900'' Then ''1''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''0900'' and FNHourNo < ''1000'' Then ''2''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1000'' and FNHourNo < ''1100'' Then ''3''"
            _Cmd &= vbCrLf & "WHEN FNHourNo >= ''1100'' and FNHourNo <= ''1200'' Then ''4''"
            _Cmd &= vbCrLf & "WHEN FNHourNo >= ''1300'' and FNHourNo < ''1400'' Then ''5''"
            _Cmd &= vbCrLf & "WHEN FNHourNo >= ''1400'' and FNHourNo < ''1500'' Then ''6''"
            _Cmd &= vbCrLf & "WHEN FNHourNo >= ''1500'' and FNHourNo < ''1600'' Then ''7''"
            _Cmd &= vbCrLf & "WHEN FNHourNo >= ''1600'' and FNHourNo < ''1700'' Then ''8''"
            _Cmd &= vbCrLf & "WHEN FNHourNo >= ''1730'' and FNHourNo < ''1830'' Then ''9''"
            _Cmd &= vbCrLf & "WHEN FNHourNo >= ''1830'' and FNHourNo < ''1930'' Then ''10''"
            _Cmd &= vbCrLf & "WHEN FNHourNo >= ''1930'' and FNHourNo < ''2030'' Then ''11''"
            _Cmd &= vbCrLf & "WHEN FNHourNo >= ''2030'' and FNHourNo < ''2130'' Then ''12''"
            _Cmd &= vbCrLf & "ELSE ''''"
            _Cmd &= vbCrLf & "END FNHourNo"
            _Cmd &= vbCrLf & "    FROM TPRODTQA_Detail"


            _Cmd &= vbCrLf & " where FDQADate >= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'' and FDQADate <= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "'' and   FTStateReject = ''1''"
            _Cmd &= vbCrLf & "   and  FNHSysUnitSectId=" & Me.FNHSysUnitSectId.Properties.Tag & ") AS C " ' and  FNHSysStyleId = " & Me.FNHSysStyleId.Properties.Tag & " and FTOrderNo=''" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "''

            _Cmd &= vbCrLf & "pivot (count(FNSeq) for FNHourNo in(' + @ColumnName + ')) AS T2"
            _Cmd &= vbCrLf & "        UNION"
            _Cmd &= vbCrLf & "SELECT  convert(nvarchar(3), ROW_NUMBER() Over (Order By  A.FNHSysUnitSectId,  A.FNHSysQADetailId)) AS FNSeq"
            _Cmd &= vbCrLf & "   ,   0 as FNHSysStyleId, A.FNHSysUnitSectId,'''' as FTOrderNo,    A.FNHSysQADetailId, B.FTQADetailCode "

            'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Cmd &= vbCrLf & ", B.FTQADetailNameTH as FTQADetailNameTH"
            'Else
            _Cmd &= vbCrLf & ", B.FTQADetailNameEN as FTQADetailNameEN"
            'End If
            _Cmd &= vbCrLf & "  ,' + @ColumnNameSum + ', sum( '+ @colNames +')  AS Total"
            _Cmd &= vbCrLf & "FROM (SELECT distinct     FNHSysUnitSectId,   FDQADate,  FNHSysQADetailId" 'FTPointSubName, 
            _Cmd &= vbCrLf & "FROM            TPRODTQA_SubDetail With(NOLOCK) ) AS A LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "                    HITECH_MASTER.dbo.TQAMQADetail AS B WITH(NOLOCK) ON A.FNHSysQADetailId = B.FNHSysQADetailId"
            _Cmd &= vbCrLf & "           LEFT OUTER JOIN #Tmp AS T ON A.FNHSysQADetailId = T.FNHSysQADetailId  "
            _Cmd &= vbCrLf & "				and A.FNHSysUnitSectId = T.FNHSysUnitSectId  "

            _Cmd &= vbCrLf & " WHERE     (A.FDQADate >= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'' and A.FDQADate <= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "''  )"
            _Cmd &= vbCrLf & "   and  A.FNHSysUnitSectId=" & Me.FNHSysUnitSectId.Properties.Tag & " "
            '_Cmd &= vbCrLf & " and A.FNHSysStyleId=" & Me.FNHSysStyleId.Properties.Tag & " "
            '_Cmd &= vbCrLf & " and A.FTOrderNo=''" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "''"
            _Cmd &= vbCrLf & " group by   A.FNHSysUnitSectId,   A.FNHSysQADetailId, B.FTQADetailCode, B.FTQADetailNameTH, "
            _Cmd &= vbCrLf & "B.FTQADetailNameEN  ) AS T' "

            _Cmd &= vbCrLf & "     EXEC sp_executesql @DynamicPivotQuery    "

            _Cmd &= vbCrLf & " Select FTUserLogIn,   FNHSysUnitSectId, FTOrderNo,  FNSeq,  [1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12], FNTotal as Total , FTQADetailCode, "
            _Cmd &= vbCrLf & "  FNHSysQADetailId "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", FTDefectTH as FTQADetailName"
            Else
                _Cmd &= vbCrLf & ", FTDefectEN as FTQADetailName"
            End If

            _Cmd &= vbCrLf & "  From TmpTPRODTQA_Report_Detail Where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & "  Order by case when isnull(FNSeq,'') = '' then convert(numeric(18,2),FTQADetailCode)   else convert(numeric(18,2),FNSeq) end asc "


            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Me.ogcDetailDaily.DataSource = _oDt
            Call SetValueDefect(_oDt)
            '  Call LoadChartDoughnut()

        Catch ex As Exception
        End Try
    End Sub

    Private oChartPie As DevExpress.XtraCharts.ChartControl
    Private Sub LoadChartDoughnut(ByVal _oDt As DataTable)
        Try
            'oChart.ClearSelection()


            oChartPie = New DevExpress.XtraCharts.ChartControl

            ' Create the first side-by-side bar series and add points to it.
            Dim series1 As New Series(Me.FTSeriesTopName.Text, ViewType.Pie)
            For Each R As DataRow In _oDt.Rows
                series1.Points.Add(New SeriesPoint(R!FTQADetailName.ToString, New Double() {CDbl("0" & R!FNQty.ToString)}))

            Next
            ' Add the series to the chart.


            oChartPie.Series.Add(series1)
            'oChartPie.AllowDrop = False
            'oChartPie.DoDragDrop(oChartPie, DragDropEffects.Move)

            series1.Label.PointOptions.PointView = PointView.ArgumentAndValues
            series1.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent
            series1.Label.PointOptions.ValueNumericOptions.Precision = 2
            Try

                If TypeOf oChartPie.Diagram Is Diagram3D Then
                    Dim diagram As Diagram3D = CType(oChartPie.Diagram, Diagram3D)
                    diagram.RuntimeRotation = True
                    diagram.RuntimeZooming = True
                    diagram.RuntimeScrolling = True
                End If

            Catch ex As Exception
            End Try


            'CType(oChartPie.Diagram, SimpleDiagram3D).RotationAngleX = -35
            'CType(oChartPie.Diagram, SimpleDiagram3D).Dimension = 2
            ' Add a title to the chart (if necessary).
            'Dim chartTitle1 As New ChartTitle()

            'chartTitle1.Text = Me.FTTitleTopChart.Text
            'oChartPie.Titles.Add(chartTitle1)
            oChartPie.Dock = DockStyle.Fill

            Me.oChartDefaul.Controls.Clear()
            Me.oChartDefaul.Controls.Add(oChartPie)
        Catch ex As Exception
        End Try
    End Sub



    Private Sub wDailyQAReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvDetailDaily)
            Me.InitGrid()
            Me.InitGrid2()

            If _StateLoad Then
                Call ocmload_Click(sender, e)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvDetailDaily_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles ogvDetailDaily.RowStyle
        Try
            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then
                Dim col As String = View.GetRowCellDisplayText(e.RowHandle, View.Columns("FNSeq"))
                If col = "" Then
                    e.Appearance.BackColor = Color.Gray
                    e.Appearance.BackColor2 = Color.LightGray
                    e.Appearance.BorderColor = Color.LightGray

                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SetValueDefect(ByVal _oDt As DataTable)
        Try
            Dim _DeQty As Double = 0
            Dim _ReQty As Double = 0
            Dim _Point As Integer = GetPoint()
            For Each R As DataRow In _oDt.Rows
                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    If R!FTQADetailName.ToString = "จำนวนเสีย/ตัว" Then
                        _DeQty += +CDbl("0" & R!Total)
                    End If
                Else
                    If R!FTQADetailName.ToString = "Defect /Pcs." Then
                        _DeQty += +CDbl("0" & R!Total)
                    End If
                End If

            Next

            'If _oDt.Rows.Count > 0 Then
            '    _ReQty = CDbl("0" & _oDt.Rows(1) !Total)
            'End If
            If _oDt.Rows.Count > 0 Then
                _ReQty = CDbl("0" & _oDt.Rows(1)!Total)
            End If

            'If _DeQty > 0 And _ReQty > 0 And _Point > 0 Then
            '    Me.FNDefectQty.Text = Format(CDbl("0" & _DeQty), "#,#")
            '    Me.FNPercentDefect.Text = Format((CDbl("0" & _DeQty) / (CDbl("0" & _ReQty) * _Point)) * 100, "0.00") & "%"
            '    Me.FNQualityRate.Text = Format((((CDbl("0" & _ReQty) * _Point) - CDbl("0" & _DeQty)) / (CDbl("0" & _ReQty) * _Point)) * 100, "0.00") & "%"
            'Else
            '    Me.FNDefectQty.Text = "0"
            '    Me.FNPercentDefect.Text = "0.00" & "%"
            '    Me.FNQualityRate.Text = "0.00" & "%"
            'End If
            Dim QARet As Double = 0
            If _DeQty > 0 And _ReQty > 0 Then
                Me.FNDefectQty.Text = Format(CDbl("0" & _DeQty), "#,#")
                Me.FNPercentDefect.Text = Format((CDbl("0" & _DeQty) / (CDbl("0" & _ReQty))) * 100, "0.00") & "%"
                Me.FNQualityRate.Text = Format((((CDbl("0" & _ReQty)) - CDbl("0" & _DeQty)) / (CDbl("0" & _ReQty))) * 100, "0.00") & "%"
                QARet = Format((((CDbl("0" & _ReQty)) - CDbl("0" & _DeQty)) / (CDbl("0" & _ReQty))) * 100, "0.00")
            Else
                Me.FNDefectQty.Text = "0"
                Me.FNPercentDefect.Text = "0.00" & "%"
                Me.FNQualityRate.Text = "100.00" & "%"
                QARet = "100.00"
            End If


            Dim _dt As New DataTable
            With _dt
                .Columns.Add("FNQty", GetType(Double))
                .Columns.Add("FTQADetailCode", GetType(String))
                .Columns.Add("FTQADetailName", GetType(String))
                .Columns.Add("FNHSysQADetailId", GetType(Integer))
            End With

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _dt.Rows.Add(Double.Parse(QARet), "0", "คุณภาพงานดี ", 0)
            Else

                _dt.Rows.Add(Double.Parse(QARet), "0", "Quality Rate ", 0)
            End If


            Dim _DDCodeQty As Integer = Integer.Parse("0" & _oDt.Compute("sum(Total)   ", "FNHSysQADetailId > 0"))

            For Each R As DataRow In _oDt.Select("FNHSysQADetailId > 0")
                Dim PerDecode As Double = 0
                Dim defectc As Double = 0
                defectc = (Format((CDbl("0" & _DeQty) / (CDbl("0" & _ReQty))) * 100, "0.00000") / _DDCodeQty)
                PerDecode = Format(defectc * CDbl("0" & R!Total.ToString), "0.00000")
                _dt.Rows.Add(PerDecode, R!FTQADetailCode.ToString, R!FTQADetailName.ToString, Integer.Parse(R!FNHSysQADetailId.ToString))
            Next

            Call LoadChartDoughnut(_dt)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub InitGrid2()
        Try
            Dim sFieldSum As String = "Total"

            With ogvDetailDaily
                For Each Str As String In sFieldSum.Split("|")
                    If Str <> "" Then
                        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
                        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                    End If
                Next
            End With
        Catch ex As Exception
        End Try
    End Sub
    Private totalSum As Integer = 0
    Private GrpSum As Integer = 0
    Private _RowHandleHold As Integer = 0

    Private Sub InitStartValue()
        totalSum = 0
        GrpSum = 0
        _RowHandleHold = 0
    End Sub

    Private Sub ogvDetailDaily_CustomSummaryCalculate(sender As Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles ogvDetailDaily.CustomSummaryCalculate
        If e.SummaryProcess = CustomSummaryProcess.Start Then
            InitStartValue()
        End If
        Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
            Case "Total"
                If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                    If e.IsTotalSummary Then
                        If _RowHandleHold <> e.RowHandle Then
                            If ogvDetailDaily.GetRowCellValue(e.RowHandle, "FNSeq").ToString <> "" Then
                                totalSum = totalSum + Integer.Parse(Val(e.FieldValue.ToString))
                            End If
                            _RowHandleHold = e.RowHandle
                        End If
                    End If
                    e.TotalValue = totalSum
                End If
        End Select
    End Sub

    Private Sub PreviewReport(_OrderNo As String, _StyleCode As String, _StyleId As Integer)
        Try
            Dim Img As New DevExpress.XtraEditors.PictureEdit
            Dim Img2 As New DevExpress.XtraEditors.PictureEdit
            Img.Size = Me.FTImage.Size
            Img2.Size = Me.FTImage2.Size

            LoadImageStyle(_StyleCode, Img, Img2)

            imageRpt = Img
            imageRpt2 = Img2


            'HI.UL.ULImage.
            Dim _Cmd As String = ""

            '_Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TmpTPRODTQA_Report WHERE FTInsUser='" & HI.ST.UserInfo.UserName & "'"
            '_Cmd &= vbCrLf & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TmpTPRODTQA_Report "
            '_Cmd &= "(FTInsUser, FDInsDate, FTInsTime,   FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate )"
            '_Cmd &= vbCrLf & "Select '" & HI.ST.UserInfo.UserName & "'"
            '_Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            '_Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            '_Cmd &= vbCrLf & "," & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
            '_Cmd &= vbCrLf & "," & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
            '_Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            '_Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'"
            'HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)



            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()

            _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TmpTPRODTQA_Report (FTInsUser, FDInsDate, FTInsTime,   FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate , FTImage , FTImage2)" '
            _Cmd &= vbCrLf & "Select @FTUserLogIn"
            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & ",@FNHSysStyleId"
            _Cmd &= vbCrLf & ",@FNHSysUnitSectId"
            _Cmd &= vbCrLf & ",@FTOrderNo"
            _Cmd &= vbCrLf & ",@FDQADate"
            _Cmd &= vbCrLf & ",@FTImage"
            _Cmd &= vbCrLf & ",@FTImage2"
            '    _Cmd &= vbCrLf & ",@FTImage3"

            '_Cmd = " Update  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TmpTPRODTQA_Report "
            '_Cmd &= vbCrLf & "  SET FTImage=@FTImage"
            '_Cmd &= vbCrLf & " ,FTImage2=@FTImage2"
            '_Cmd &= vbCrLf & " WHERE FNHSysStyleId=@FNHSysStyleId "
            '_Cmd &= vbCrLf & " And FNHSysUnitSectId=@FNHSysUnitSectId"
            '_Cmd &= vbCrLf & " AND FTOrderNo=@FTOrderNo"
            '_Cmd &= vbCrLf & " AND FDQADate=@FDQADate"

            Dim cmd As New SqlCommand(_Cmd, HI.Conn.SQLConn.Cnn)
            cmd.Parameters.Add("@FTUserLogIn", SqlDbType.NVarChar, 100)
            'cmd.Parameters.Add("@FDDate", SqlDbType.NVarChar, 100)
            'cmd.Parameters.Add("@FTTime", SqlDbType.NVarChar, 100)
            cmd.Parameters.Add("@FNHSysStyleId", SqlDbType.Int, 18)
            cmd.Parameters.Add("@FNHSysUnitSectId", SqlDbType.Int, 18)
            cmd.Parameters.Add("@FTOrderNo", SqlDbType.NVarChar, 30)
            cmd.Parameters.Add("@FDQADate", SqlDbType.NVarChar, 30)
            cmd.Parameters.Add("@FTImage", SqlDbType.Image)
            cmd.Parameters.Add("@FTImage2", SqlDbType.Image)
            'cmd.Parameters.Add("@FTImage3", SqlDbType.Image)

            cmd.Parameters("@FTUserLogIn").Value = HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)
            'cmd.Parameters("@FDDate").Value = HI.UL.ULDate.FormatDateDB
            'cmd.Parameters("@FTTime").Value = HI.UL.ULDate.FormatTimeDB
            cmd.Parameters("@FNHSysStyleId").Value = Integer.Parse(Val(_StyleId))
            cmd.Parameters("@FNHSysUnitSectId").Value = Integer.Parse(Val(Me.FNHSysUnitSectId.Properties.Tag))
            cmd.Parameters("@FTOrderNo").Value = HI.UL.ULF.rpQuoted(_OrderNo)
            cmd.Parameters("@FDQADate").Value = HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text)

            Dim data As Byte() = Nothing
            data = HI.UL.ULImage.ConvertImageToByteArray(TakeScreenShot(imageRpt), UL.ULImage.PicType.Employee)
            cmd.Parameters("@FTImage").Value = data

            Dim data2 As Byte() = Nothing
            data2 = HI.UL.ULImage.ConvertImageToByteArray(TakeScreenShot(imageRpt2), UL.ULImage.PicType.Employee)
            cmd.Parameters("@FTImage2").Value = data2


            'Dim data3 As Byte() = Nothing
            'data3 = HI.UL.ULImage.ConvertImageToByteArray(TakeScreenShot(FTImage3), UL.ULImage.PicType.Employee)
            'cmd.Parameters("@FTImage3").Value = data3


            'cmd.Parameters.AddWithValue("@FNHSysStyleId", Integer.Parse(Val(Me.FNHSysStyleId.Properties.Tag)))
            'cmd.Parameters.AddWithValue("@FNHSysUnitSectId", Integer.Parse(Val(Me.FNHSysUnitSectId.Properties.Tag)))
            'cmd.Parameters.AddWithValue("@FTOrderNo", HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text))
            'cmd.Parameters.AddWithValue("@FDQADate", HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text))
            'Dim data As Byte() = Nothing


            'data = HI.UL.ULImage.ConvertImageToByteArray(TakeScreenShot(Me.FTImage), UL.ULImage.PicType.Employee)


            'Dim p As New SqlParameter("@FTImage", SqlDbType.Image)
            'p.Value = data

            'cmd.Parameters.Add(p)

            'Dim data2 As Byte() = Nothing


            'data2 = HI.UL.ULImage.ConvertImageToByteArray(TakeScreenShot(Me.FTImage2), UL.ULImage.PicType.Employee)


            'Dim p2 As New SqlParameter("@FTImage2", SqlDbType.Image)
            'p2.Value = data2

            'cmd.Parameters.Add(p2)

            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)

        Catch ex As Exception
            'MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        Try
            If Me.VerifyData() = False Then
                Exit Sub
            End If

            Dim _spls As New HI.TL.SplashScreen("Loading... Report.Please Wait.", "Preview Report")

            Dim _Order As String = Me.oFTOrderNo.Text
            Dim _StyleId As Integer = 0 : Dim _Seq As Integer = 0
            Dim _Cmd As String = "" : Dim _StyleCode As String = ""
            _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TmpTPRODTQA_Report WHERE FTInsUser='" & HI.ST.UserInfo.UserName & "'"

            '_Cmd &= vbCrLf & " and FTOrderNo='" & HI.UL.ULF.rpQuoted(str) & "'"
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            _Cmd = " Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TmpTPRODTQA_Report_Detail Where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)


            For Each str As String In _Order.Split(",")
                str = Microsoft.VisualBasic.Replace(str, " ", "")
                _StyleId = 0 : _StyleCode = ""
                _Seq += +1



                _Cmd = "Select Top 1 FNHSysStyleId  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder WITH(NOLOCK)  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(str) & "'"
                _StyleId = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN, 0)

                _Cmd = "Select Top 1 FTStyleCode  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK)  WHERE FNHSysStyleId=" & Integer.Parse(_StyleId)
                _StyleCode = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MASTER, "")

                Me.PreviewReport(str, _StyleCode, _StyleId)
                Call GetDataReport(_StyleId, str, _Seq)
                Dim _Formular As String = ""

                If Me.FNHSysUnitSectId.Properties.Tag <> "" Then
                    _Formular = "{TmpTPRODTQA_Report_Detail.FNHSysUnitSectId} =" & CInt("0" & Me.FNHSysUnitSectId.Properties.Tag)
                End If

                If _StyleId > 0 Then
                    _Formular &= " and {TmpTPRODTQA_Report_Detail.FNHSysStyleId} =" & CInt("0" & _StyleId)
                End If

                If str <> "" Then
                    _Formular &= " and {TmpTPRODTQA_Report_Detail.FTOrderNo} ='" & HI.UL.ULF.rpQuoted(str) & "'"
                End If


                _Formular &= " and {TmpTPRODTQA_Report_Detail.FTUserLogIn}  ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"


                With New HI.RP.Report

                    .FormTitle = Me.Text
                    .ReportFolderName = "Production\"
                    .Formular = _Formular
                    .AddParameter("FDSDate", HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text))
                    .AddParameter("FDEDate", HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text))
                    .ReportName = "ReportQADaily.rpt"
                    _spls.Close()
                    .Preview()
                End With

            Next

        Catch ex As Exception
        End Try
    End Sub

    Private Function TakeScreenShot(ByVal Control As Control) As Bitmap
        Dim Screenshot As New Bitmap(Control.Width, Control.Height)
        Control.DrawToBitmap(Screenshot, New Rectangle(0, 0, Control.Width, Control.Height))
        Return Screenshot
    End Function

    Private Sub GetDataReport(StyleId As Integer, OrderNo As String, _Seq As Integer)
        Try
            Dim _Cmd As String = ""


            _Cmd = "    DECLARE @DynamicPivotQuery AS NVARCHAR(MAX)"
            _Cmd &= vbCrLf & "DECLARE @ColumnName varchar(max)"
            _Cmd &= vbCrLf & " DECLARE @ColumnNameMax varchar(max) ,@ColumnDefault varchar(max)"
            _Cmd &= vbCrLf & " Set @ColumnDefault = '[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12]'"
            _Cmd &= vbCrLf & " Set @ColumnName = @ColumnDefault"

            _Cmd &= vbCrLf & "DECLARE @colNames varchar(max)  "
            _Cmd &= vbCrLf & " Declare @ColumnNameSum nvarchar(max) "
            _Cmd &= vbCrLf & " SET @ColumnNameSum =  'sum(Isnull([1],0)) AS [1],sum(Isnull([2],0)) AS [2],sum(Isnull([3],0)) AS [3],sum(Isnull([4],0)) AS [4],sum(Isnull([5],0)) AS [5],sum(Isnull([6],0)) AS [6],sum(Isnull([7],0)) AS [7]"
            _Cmd &= vbCrLf & "		,sum(Isnull([8],0)) AS [8],sum(Isnull([9],0)) AS [9],sum(Isnull([10],0)) AS [10],sum(Isnull([11],0)) AS [11],sum(Isnull([12],0)) AS [12]'"
            _Cmd &= vbCrLf & "SET @colNames =  'Isnull([1],0) +Isnull([2],0) +Isnull([3],0) +Isnull([4],0) +Isnull([5],0) +Isnull([6],0) +Isnull([7],0) +"
            _Cmd &= vbCrLf & "				Isnull([8],0) +Isnull([9],0) +Isnull([10],0) +Isnull([11],0) +Isnull([12],0) '"



            '   _Cmd &= vbCrLf & " Delete From TmpTPRODTQA_Report_Detail Where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & "  SET @DynamicPivotQuery =   N'SELECT FNHSysStyleId, FNHSysUnitSectId, FTOrderNo,    FNHSysQADetailId, ' + @ColumnName + '"
            _Cmd &= vbCrLf & "	INTO #Tmp "
            _Cmd &= vbCrLf & "	From ("
            _Cmd &= vbCrLf & "	SELECT COUNT(*) AS Qty, A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo, A.FDQADate,   A.FNHSysQADetailId"
            _Cmd &= vbCrLf & "	,  CASE  "
            _Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''0800'' and A.FNHourNo < ''0900'' Then ''1''"
            _Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''0900'' and A.FNHourNo < ''1000'' Then ''2''"
            _Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1000'' and A.FNHourNo < ''1100'' Then ''3''"
            _Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1100'' and A.FNHourNo <= ''1200'' Then ''4''"
            _Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1300'' and A.FNHourNo < ''1400'' Then ''5''"
            _Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1400'' and A.FNHourNo < ''1500'' Then ''6''"
            _Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1500'' and A.FNHourNo < ''1600'' Then ''7''"
            _Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1600'' and A.FNHourNo < ''1700'' Then ''8''"
            _Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1730'' and A.FNHourNo < ''1830'' Then ''9''"
            _Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1830'' and A.FNHourNo < ''1930'' Then ''10''"
            _Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''1930'' and A.FNHourNo < ''2030'' Then ''11''"
            _Cmd &= vbCrLf & "	WHEN A.FNHourNo >= ''2030'' and A.FNHourNo < ''2130'' Then ''12''"
            _Cmd &= vbCrLf & "	ELSE ''''"
            _Cmd &= vbCrLf & "	END FNHourNo"
            _Cmd &= vbCrLf & "	FROM         TPRODTQA_SubDetail AS A LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "						  HITECH_MASTER.dbo.TQAMQADetail AS B ON A.FNHSysQADetailId = B.FNHSysQADetailId"

            _Cmd &= vbCrLf & " 		WHERE     (A.FDQADate >= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'' and A.FDQADate <= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "'')"
            _Cmd &= vbCrLf & "   and  A.FNHSysUnitSectId=" & Me.FNHSysUnitSectId.Properties.Tag & " and  A.FNHSysStyleId = " & StyleId & "  and A.FTOrderNo=''" & HI.UL.ULF.rpQuoted(OrderNo) & "''  " '
            _Cmd &= vbCrLf & "	group by     A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo,  A.FDQADate,  A.FNHSysQADetailId, B.FTQADetailCode, B.FTQADetailNameTH, "
            _Cmd &= vbCrLf & "              B.FTQADetailNameEN ,  A.FNHourNo) AS A       "
            _Cmd &= vbCrLf & "   PIVOT(sum(Qty) FOR   FNHourNo IN (' + @ColumnName + ')) AS PVTTable"

            _Cmd &= vbCrLf & " INSERT INTO TmpTPRODTQA_Report_Detail(FNSeqMain,  FTUserLogIn,FNSeq,  FNHSysStyleId, FNHSysUnitSectId, FTOrderNo,FNHSysQADetailId , FTQADetailCode, FTDefectTH, FTDefectEN, [1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12], FNTotal)"

            _Cmd &= vbCrLf & "Select   ''" & (_Seq) & "'', ''" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'',* From ("
            _Cmd &= vbCrLf & " Select '''' AS FNSeq , FNHSysStyleId, FNHSysUnitSectId, FTOrderNo,  '''' as FNHSysQADetailId, ''0.1'' as  FTQADetailCode "

            'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Cmd &= vbCrLf & ", ''จำนวนงานออก'' as FTQADetailNameTH"
            'Else
            _Cmd &= vbCrLf & ",''Output'' as FTQADetailNameEN"
            'End If
            _Cmd &= vbCrLf & ", ' + @ColumnName + ', ( '+ @colNames +')  AS Total"
            _Cmd &= vbCrLf & " From ( SELECT     FNHSysStyleId, FNHSysUnitSectId, FTOrderNo,  FNQAInQty, "
            _Cmd &= vbCrLf & "  CASE  "
            _Cmd &= vbCrLf & "		WHEN FNHourNo >= ''0800'' and FNHourNo < ''0900'' Then ''1''"
            _Cmd &= vbCrLf & "		WHEN FNHourNo >= ''0900'' and FNHourNo < ''1000'' Then ''2''"
            _Cmd &= vbCrLf & "		WHEN FNHourNo >= ''1000'' and FNHourNo < ''1100'' Then ''3''"
            _Cmd &= vbCrLf & "		WHEN FNHourNo >= ''1100'' and FNHourNo <= ''1200'' Then ''4''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1300'' and FNHourNo < ''1400'' Then ''5''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1400'' and FNHourNo < ''1500'' Then ''6''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1500'' and FNHourNo < ''1600'' Then ''7''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1600'' and FNHourNo < ''1700'' Then ''8''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1730'' and FNHourNo < ''1830'' Then ''9''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1830'' and FNHourNo < ''1930'' Then ''10''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1930'' and FNHourNo < ''2030'' Then ''11''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''2030'' and FNHourNo < ''2130'' Then ''12''"
            _Cmd &= vbCrLf & "	ELSE ''''"
            _Cmd &= vbCrLf & "	END FNHourNo"
            _Cmd &= vbCrLf & "         FROM TPRODTQA"

            _Cmd &= vbCrLf & " where FDQADate >= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'' and FDQADate <= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "''"
            _Cmd &= vbCrLf & "   and  FNHSysUnitSectId=" & Me.FNHSysUnitSectId.Properties.Tag & "  and  FNHSysStyleId = " & StyleId & "  and FTOrderNo=''" & HI.UL.ULF.rpQuoted(OrderNo) & "'' ) AS A" '

            _Cmd &= vbCrLf & "pivot (Sum(FNQAInQty) for FNHourNo in(' + @ColumnName + ')) AS T0     "
            _Cmd &= vbCrLf & "           UNION"
            _Cmd &= vbCrLf & "Select '''' AS FNSeq , FNHSysStyleId, FNHSysUnitSectId, FTOrderNo,  '''' as FNHSysQADetailId, ''0.2'' as  FTQADetailCode "

            'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Cmd &= vbCrLf & ",''จำนวนสุ่มตรวจ'' as FTQADetailNameTH"
            'Else
            _Cmd &= vbCrLf & ",''Random'' as FTQADetailNameEN"
            'End If
            _Cmd &= vbCrLf & " ,' + @ColumnName + ', ( '+ @colNames +')  AS Total"
            _Cmd &= vbCrLf & " From (SELECT     FNHSysStyleId, FNHSysUnitSectId, FTOrderNo,"
            _Cmd &= vbCrLf & " CASE  "
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''0800'' and FNHourNo < ''0900'' Then ''1''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''0900'' and FNHourNo < ''1000'' Then ''2''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1000'' and FNHourNo < ''1100'' Then ''3''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1100'' and FNHourNo <= ''1200'' Then ''4''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1300'' and FNHourNo < ''1400'' Then ''5''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1400'' and FNHourNo < ''1500'' Then ''6''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1500'' and FNHourNo < ''1600'' Then ''7''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1600'' and FNHourNo < ''1700'' Then ''8''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1730'' and FNHourNo < ''1830'' Then ''9''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1830'' and FNHourNo < ''1930'' Then ''10''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1930'' and FNHourNo < ''2030'' Then ''11''"
            _Cmd &= vbCrLf & "WHEN FNHourNo >= ''2030'' and FNHourNo < ''2130'' Then ''12''"
            _Cmd &= vbCrLf & "ELSE ''''"
            _Cmd &= vbCrLf & "	END FNHourNo"

            _Cmd &= vbCrLf & ",   FNQAActualQty  "
            _Cmd &= vbCrLf & "          FROM TPRODTQA"

            _Cmd &= vbCrLf & " WHERE FDQADate >= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'' and FDQADate <= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "''"
            _Cmd &= vbCrLf & "   and  FNHSysUnitSectId=" & Me.FNHSysUnitSectId.Properties.Tag & "   and  FNHSysStyleId = " & StyleId & " and FTOrderNo=''" & HI.UL.ULF.rpQuoted(OrderNo) & "'' ) AS B" '

            _Cmd &= vbCrLf & "Pivot (sum(FNQAActualQty) for FNHourNo in (' + @ColumnName + ')) AS T1"
            _Cmd &= vbCrLf & "           UNION"
            _Cmd &= vbCrLf & "Select '''' AS FNSeq , FNHSysStyleId, FNHSysUnitSectId, FTOrderNo,'''' as FNHSysQADetailId, ''0.3'' as  FTQADetailCode "

            'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Cmd &= vbCrLf & ", ''จำนวนเสีย/ตัว'' as FTQADetailNameTH"
            'Else
            _Cmd &= vbCrLf & ",''Defect /Pcs.'' as FTQADetailNameEN"
            'End If
            _Cmd &= vbCrLf & ", ' + @ColumnName + ', ( '+ @colNames +')  AS Total"
            _Cmd &= vbCrLf & " From ( SELECT     FNHSysStyleId, FNHSysUnitSectId, FTOrderNo,  Isnull(FNSeq,0) AS FNSeq ,"
            _Cmd &= vbCrLf & " CASE  "
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''0800'' and FNHourNo < ''0900'' Then ''1''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''0900'' and FNHourNo < ''1000'' Then ''2''"
            _Cmd &= vbCrLf & "	WHEN FNHourNo >= ''1000'' and FNHourNo < ''1100'' Then ''3''"
            _Cmd &= vbCrLf & "WHEN FNHourNo >= ''1100'' and FNHourNo <= ''1200'' Then ''4''"
            _Cmd &= vbCrLf & "WHEN FNHourNo >= ''1300'' and FNHourNo < ''1400'' Then ''5''"
            _Cmd &= vbCrLf & "WHEN FNHourNo >= ''1400'' and FNHourNo < ''1500'' Then ''6''"
            _Cmd &= vbCrLf & "WHEN FNHourNo >= ''1500'' and FNHourNo < ''1600'' Then ''7''"
            _Cmd &= vbCrLf & "WHEN FNHourNo >= ''1600'' and FNHourNo < ''1700'' Then ''8''"
            _Cmd &= vbCrLf & "WHEN FNHourNo >= ''1730'' and FNHourNo < ''1830'' Then ''9''"
            _Cmd &= vbCrLf & "WHEN FNHourNo >= ''1830'' and FNHourNo < ''1930'' Then ''10''"
            _Cmd &= vbCrLf & "WHEN FNHourNo >= ''1930'' and FNHourNo < ''2030'' Then ''11''"
            _Cmd &= vbCrLf & "WHEN FNHourNo >= ''2030'' and FNHourNo < ''2130'' Then ''12''"
            _Cmd &= vbCrLf & "ELSE ''''"
            _Cmd &= vbCrLf & "END FNHourNo"
            _Cmd &= vbCrLf & "    FROM TPRODTQA_Detail"


            _Cmd &= vbCrLf & " where FDQADate >= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'' and FDQADate <= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "'' and   FTStateReject = ''1''"
            _Cmd &= vbCrLf & "   and  FNHSysUnitSectId=" & Me.FNHSysUnitSectId.Properties.Tag & "  and  FNHSysStyleId = " & StyleId & " and FTOrderNo=''" & HI.UL.ULF.rpQuoted(OrderNo) & "'' ) AS C " ' 

            _Cmd &= vbCrLf & "pivot (count(FNSeq) for FNHourNo in(' + @ColumnName + ')) AS T2"
            _Cmd &= vbCrLf & "        UNION"
            _Cmd &= vbCrLf & "SELECT  convert(nvarchar(3), ROW_NUMBER() Over (Order By A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo,    A.FNHSysQADetailId)) AS FNSeq"
            _Cmd &= vbCrLf & "   ,  A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo,    A.FNHSysQADetailId, B.FTQADetailCode "

            'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Cmd &= vbCrLf & ", B.FTQADetailNameTH as FTQADetailNameTH"
            'Else
            _Cmd &= vbCrLf & ", B.FTQADetailNameEN as FTQADetailNameEN"
            'End If
            _Cmd &= vbCrLf & "  ,' + @ColumnNameSum + ', sum( '+ @colNames +')  AS Total"
            _Cmd &= vbCrLf & "FROM (SELECT distinct   FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate,  FNHSysQADetailId" 'FTPointSubName, 
            _Cmd &= vbCrLf & "FROM            TPRODTQA_SubDetail With(NOLOCK) ) AS A LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "                    HITECH_MASTER.dbo.TQAMQADetail AS B WITH(NOLOCK) ON A.FNHSysQADetailId = B.FNHSysQADetailId"
            _Cmd &= vbCrLf & "           LEFT OUTER JOIN #Tmp AS T ON A.FNHSysQADetailId = T.FNHSysQADetailId and A.FNHSysStyleId = T.FNHSysStyleId"
            _Cmd &= vbCrLf & "				and A.FNHSysUnitSectId = T.FNHSysUnitSectId  "

            _Cmd &= vbCrLf & " WHERE     (A.FDQADate >= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'' and A.FDQADate <= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "''  )"
            _Cmd &= vbCrLf & "   and  A.FNHSysUnitSectId=" & Me.FNHSysUnitSectId.Properties.Tag & " "
            _Cmd &= vbCrLf & " and A.FNHSysStyleId=" & StyleId & " "
            _Cmd &= vbCrLf & " and A.FTOrderNo=''" & HI.UL.ULF.rpQuoted(OrderNo) & "''"
            _Cmd &= vbCrLf & " group by A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo,    A.FNHSysQADetailId, B.FTQADetailCode, B.FTQADetailNameTH, "
            _Cmd &= vbCrLf & "B.FTQADetailNameEN  ) AS T' "

            _Cmd &= vbCrLf & "     EXEC sp_executesql @DynamicPivotQuery    "

            _Cmd &= vbCrLf & " Select FTUserLogIn, FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate, FNSeq,  [1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12], FNTotal as Total , FTQADetailCode, "
            _Cmd &= vbCrLf & "  FNHSysQADetailId "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", FTDefectTH as FTQADetailName"
            Else
                _Cmd &= vbCrLf & ", FTDefectEN as FTQADetailName"
            End If

            _Cmd &= vbCrLf & "  From TmpTPRODTQA_Report_Detail Where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & "  Order by case when isnull(FNSeq,'') = '' then convert(numeric(18,2),FTQADetailCode)   else convert(numeric(18,2),FNSeq) end asc "


            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
        Catch ex As Exception

        End Try
    End Sub


    Private Function VerifyData() As Boolean
        Try
            If Me.FNHSysUnitSectId.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysUnitSectId_lbl.Text)
                Me.FNHSysUnitSectId.Focus()
                Return False
            End If

            'If Me.FNHSysStyleId.Text = "" Then
            '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysStyleId_lbl.Text)
            '    Me.FNHSysStyleId.Focus()
            '    Return False
            'End If
            'If Me.FTOrderNo.Text = "" Then
            '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTOrderNo_lbl.Text)
            '    Me.FTOrderNo.Focus()
            '    Return False
            'End If


            If Me.FDSDate.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FDSDate_lbl.Text)
                Me.FDSDate.Focus()
                Return False
            End If
            If Me.FDEDate.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FDEDate_lbl.Text)
                Me.FDEDate.Focus()
                Return False
            End If


            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function GetPoint() As Integer
        Try
            Dim _Cmd As String = ""
            '_Cmd = "Select COUNT(*) AS Qty From TPRODTStylePoint"
            '_Cmd &= vbCrLf & " where FNHSysStyleId = " & Integer.Parse(Val(Me.FNHSysStyleId.Properties.Tag))

            _Cmd = "  Select Top 1  Qty "
            _Cmd &= vbCrLf & "From (Select  COUNT(*) AS Qty ,T.FNHSysStyleId From TPRODTStylePoint AS T WITH(NOLOCK ) LEFT OUTER JOIN [HITECH_MASTER]..TMERMStyle AS M WITH(NOLOCK) "
            _Cmd &= vbCrLf & "ON T.FNHSysStyleId = M.FNHSysStyleId "
            _Cmd &= vbCrLf & "Where M.FTStyleCode like '" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "%'"
            _Cmd &= vbCrLf & "group by T.FNHSysStyleId ) AS X"
            _Cmd &= vbCrLf & "Order by  Qty desc"

            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0")
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub
End Class