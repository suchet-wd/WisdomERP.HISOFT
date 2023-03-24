Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls

Public Class wQATrimCard
    Private _tmpPg As UIQAPreFinalTrackingList
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _tmpPg = New UIQAPreFinalTrackingList(Nothing)

        page0.Controls.Add(_tmpPg)
        _tmpPg.Dock = System.Windows.Forms.DockStyle.Fill
    End Sub


#Region "Procedure"
    Private Sub LoadData()
        Dim _Qry As String = ""
        Dim dt As New DataTable

        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")

        Try
            '_Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_QAPreFinal_Tracking '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "' "

            _Qry = "select  *  "
            _Qry &= vbCrLf & " from HITECH_PRODUCTION.dbo.getOrderForTrimCard( " & Val(Me.FNHSysStyleId.Properties.Tag) & "," & Val(Me.FNHSysSeasonId.Properties.Tag) & " ) "
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
            Me.ogcOrderNo.DataSource = dt


            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try

        dt.Dispose()

    End Sub

#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If Me.FNHSysStyleId.Text <> "" And Me.FNHSysSeasonId.Text <> "" Then
                Call LoadData()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            'PSizevideo.URL = "D:\Download\T429908\Video\big_buck_bunny.mp4"
            InitControl()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub pivotGridControl_CustomCellDisplayText(sender As Object, e As DevExpress.XtraPivotGrid.PivotCellDisplayTextEventArgs)
        Try
            If (e.Value = 0) Then e.DisplayText = ""

        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepositoryFTSelect_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryFTSelect.EditValueChanging
        Try
            Dim _Qry As String = "" : Dim _oDt As DataTable

            If e.NewValue = "1" Then
                With Me.ogvOrderNo
                    If .RowCount <= 0 Or .FocusedRowHandle < 0 Then Exit Sub
                    _Qry = "Select *  from  dbo.getInfoTrimcard('" & .GetRowCellValue(.FocusedRowHandle, "FTOrderNo").ToString & "')"
                    _Qry &= vbCrLf & " Order by FNTrimCardType asc "
                    _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

                    For Each R As DataRow In _oDt.Select("", "FNTrimCardType asc")

                        Select Case Val(R!FNTrimCardType.ToString)
                            Case 0
                                'BACK NECK LABEL
                                Me.TrimCard10.Text = R!FTComponent.ToString
                            Case 1
                                'ID
                                Me.FTTrimCard5.Text = R!FTComponent.ToString

                            Case 2
                                'JOCK TAG
                                Me.TrimCard6.Text = R!FTComponent.ToString

                            Case 3, 5
                                'PRINT C&C LABEL
                                Me.FTTrimCard3.Text = R!FTComponent.ToString
                            Case 4
                                'VAS
                                Me.FTTrimCard4.Text = R!FTComponent.ToString
                            Case 6
                                'ตราไซร์
                                Me.FTTrimCard1.Text = R!FTComponent.ToString
                            Case 7
                                'ถุง
                                Me.TrimCard9.Text = R!FTComponent.ToString
                            Case 8
                                'วิธียิงป้าย
                                Me.TrimCard7.Text = R!FTComponent.ToString
                            Case 9
                                'สติ๊กเกอร์
                                Me.TrimCard8.Text = R!FTComponent.ToString

                            Case Else
                                Me.TrimCard10.Text = R!FTComponent.ToString

                        End Select
                        LoadImgObjet(Val(R!FNTrimCardType.ToString), R!FTImagePath.ToString)

                    Next


                    _Qry = "Select * from dbo.getOrderPackInfo('" & .GetRowCellValue(.FocusedRowHandle, "FTOrderNo").ToString & "','" & .GetRowCellValue(.FocusedRowHandle, "FTSubOrderNo").ToString & "')"
                    _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
                    Dim _packInfo As String = ""
                    For Each x As DataRow In _oDt.Rows
                        If _packInfo <> "" Then
                            _packInfo &= vbCrLf & x!FTPackDescription.ToString
                        Else
                            _packInfo &= x!FTPackDescription.ToString
                        End If
                    Next


                    Me.TrimCard7.Text = _packInfo
                    _Qry = "Select  FTImagePath from TPRODTQATrimcard  where FTSubOrderNo =   '" & .GetRowCellValue(.FocusedRowHandle, "FTSubOrderNo").ToString & "' and FNOperId = 8"
                    _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
                    For Each Rx As DataRow In _oDt.Rows
                        LoadImgObjet(8, Rx!FTImagePath.ToString)
                    Next


                End With



            Else




            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function loadImag(ByVal image As DevExpress.XtraEditors.PictureEdit, _PathImage As String) As Image
        Try
            Try
                Dim _folder As String = Microsoft.VisualBasic.Left(_PathImage, 6)
                image.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\TrimCard\" & _folder & "\" & _PathImage & ".JPG")
                'If image.Image Is Nothing Then
                '    image.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage1))
                'End If
            Catch ex As Exception
                image.Image = Nothing
            End Try
            Return image.Image
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function loadvideo() As String
        Try
            Dim _Path As String = ""




            Return _Path
        Catch ex As Exception

        End Try
    End Function



    'Private Sub LoadImangeStyle(_FNHSysStyleId As Integer, _SeaSonId As Integer, _OperId As Integer)
    '    Try

    '        Dim _Qry As String = ""
    '        Dim dt As DataTable
    '        _Qry = "Select FNHSysCountryId,  FNSeq, FTOrderNo, FTSubOrderNo, FTOperInfo, FTImagePath   "
    '        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo. TPRODTQATrimcard  "
    '        _Qry &= vbCrLf & " WHERE FNHSysStyleId=" & Val(_FNHSysStyleId) & ""
    '        _Qry &= vbCrLf & " And FNHSysSeasonId=" & Val(_SeaSonId) & ""
    '        _Qry &= vbCrLf & " And FNOperId=" & Val(_OperId) & ""
    '        _Qry &= vbCrLf & " Order by FNSeq asc"


    '        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

    '        For Each Rx As DataRow In dt.Rows
    '            Me.FTImage.Controls.Clear()
    '            Select Case _Step
    '                Case 0
    '                    Try
    '                        Me.FTImage.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\TrimCard\" & Microsoft.VisualBasic.Left(Me.FNHSysStyleId.Text, 6).ToString & "\" & Microsoft.VisualBasic.Left(Me.FNHSysStyleId.Text, 6).ToString & "_" & _Step.ToString & ".JPG")
    '                        If Me.FTImage.Image Is Nothing Then
    '                            Me.FTImage.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage1))
    '                        End If
    '                    Catch ex As Exception
    '                        Me.FTImage.Image = Nothing
    '                    End Try
    '                Case 1
    '                    Try
    '                        Me.FTImage.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\MODEL\" & Microsoft.VisualBasic.Left(Me.FNHSysStyleId.Text, 6).ToString & "\" & Microsoft.VisualBasic.Left(Me.FNHSysStyleId.Text, 6).ToString & "_" & _Step.ToString & ".JPG")
    '                        If Me.FTImage.Image Is Nothing Then
    '                            Me.FTImage.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage2))
    '                        End If
    '                    Catch ex As Exception
    '                        Me.FTImage.Image = Nothing
    '                    End Try
    '                Case 2
    '                    Try
    '                        Me.FTImage.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\MODEL\" & Microsoft.VisualBasic.Left(Me.FNHSysStyleId.Text, 6).ToString & "\" & Microsoft.VisualBasic.Left(Me.FNHSysStyleId.Text, 6).ToString & "_" & _Step.ToString & ".JPG")
    '                        If Me.FTImage.Image Is Nothing Then
    '                            Me.FTImage.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage3))
    '                        End If
    '                    Catch ex As Exception
    '                        Me.FTImage.Image = Nothing
    '                    End Try
    '                Case 3
    '                    Try
    '                        Me.FTImage.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\MODEL\" & Microsoft.VisualBasic.Left(Me.FNHSysStyleId.Text, 6).ToString & "\" & Microsoft.VisualBasic.Left(Me.FNHSysStyleId.Text, 6).ToString & "_" & _Step.ToString & ".JPG")
    '                        If Me.FTImage.Image Is Nothing Then
    '                            Me.FTImage.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage4))
    '                        End If
    '                    Catch ex As Exception
    '                        Me.FTImage.Image = Nothing
    '                    End Try
    '            End Select

    '        Next
    '        dt.Dispose()
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Public Function _ResizeImage(ByVal image As Image) As Image
        Dim newWidth As Integer
        Dim newHeight As Integer
        newWidth = image.Width
        newHeight = image.Height
        Dim newImage As Image = New Bitmap(newWidth, newHeight)
        Using graphicsHandle As Graphics = Graphics.FromImage(newImage)
            graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic
            graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight)
        End Using
        Return newImage
    End Function

    Private _Path As String = System.Windows.Forms.Application.StartupPath.ToString
    Private Sub SaveImage(img As Image, Name As String, _OperId As Integer, _OperName As String)
        Try
            Dim _Img As New DevExpress.XtraEditors.PictureEdit
            Dim _NPath As String = _Path & "\Images\TrimCard\" & Microsoft.VisualBasic.Left(Me.FNHSysStyleId.Text, 6).ToString()
            If (My.Computer.FileSystem.DirectoryExists(_NPath) = False) Then
                My.Computer.FileSystem.CreateDirectory(_NPath)
            End If
            _Img.Image = img
            Dim _NewNameImag As String = ""
            _NewNameImag = Microsoft.VisualBasic.Left(Me.FNHSysStyleId.Text, 6).ToString & "_" & Me.FNHSysSeasonId.Text & "_" & Name
            Dim _Seq As Integer = IIf(IsNumeric(Microsoft.VisualBasic.Right(_NewNameImag, 1)), Microsoft.VisualBasic.Right(_NewNameImag, 1), 0)

            HI.UL.ULImage.SaveImage(_Img, Microsoft.VisualBasic.Left(Me.FNHSysStyleId.Text, 6).ToString & "_" & Me.FNHSysSeasonId.Text & "_" & Name, _NPath)
            SaveData(_NewNameImag, _OperName, _OperId, _Seq)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SaveVideo(img As String, Name As String, _OperId As Integer, _OperName As String)
        Dim _NewNameImag As String = ""
        Dim typename As String = ""
        Dim _Seq As Integer = 0
        Try

            Dim _NPath As String = _Path & "\video\TrimCard\" & Microsoft.VisualBasic.Left(Me.FNHSysStyleId.Text, 6).ToString()
            If (My.Computer.FileSystem.DirectoryExists(_NPath) = False) Then
                My.Computer.FileSystem.CreateDirectory(_NPath)
            End If


            _NewNameImag = Microsoft.VisualBasic.Left(Me.FNHSysStyleId.Text, 6).ToString & "_" & Me.FNHSysSeasonId.Text & "_" & Name
            _Seq = IIf(IsNumeric(Microsoft.VisualBasic.Right(_NewNameImag, 1)), Microsoft.VisualBasic.Right(_NewNameImag, 1), 0)
            Dim Inx As Integer = img.IndexOf(".")
            typename = Microsoft.VisualBasic.Right(img, Len(img) - Inx)

            Try
                'System.IO.File.Copy(_PathCopy & "\" & foundFile.Name.ToString, _Path & "\" & foundFile.Name.ToString)
                System.IO.File.Copy(img, _NPath & "\" & _NewNameImag & typename)
            Catch ex As Exception
                'My.Computer.FileSystem.CopyFile(_PathCopy & "\" & foundFile.Name.ToString, _Path & "\" & foundFile.Name.ToString)
                My.Computer.FileSystem.CopyFile(img, _NPath & "\" & _NewNameImag & typename)
            Finally
                '  MsgBox(_PathCopy & "\" & foundFile.Name.ToString & " TO " & _Path & "\" & foundFile.Name.ToString & " succsess", MsgBoxStyle.OkOnly)
            End Try


            SaveData(_NewNameImag & typename, _OperName, _OperId, _Seq, "1")
        Catch ex As Exception
            SaveData(_NewNameImag & typename, _OperName, _OperId, _Seq, "1")
        End Try
    End Sub



    Private Function getOperInfo(OperId As Integer) As String
        Try



            Dim _Oper As String = ""
            Select Case Val(OperId)
                Case 0
                    'BACK NECK LABEL
                    _Oper = Me.TrimCard10.Text
                Case 1
                    'ป้ายไลเซ็นท์
                    _Oper = Me.Trimcard5.Text

                Case 2
                    'ตราแคร์ 3 ภาษา (30C)
                    _Oper = Me.TrimCard6.Text

                Case 3, 5
                    'ป้าย Elite
                    _Oper = Me.FTTrimCard3.Text
                Case 4
                    'ป้าย NFC ซิพ
                    _Oper = Me.FTTrimCard4.Text
                Case 6
                    'ID
                    _Oper = Me.FTTrimCard1.Text
                Case 7
                    'ป้ายแชมป์
                    _Oper = Me.TrimCard9.Text
                Case 8
                    'JOCK TAG
                    _Oper = Me.TrimCard7.Text
                Case 9
                    'วิธียิงป้าย
                    _Oper = Me.TrimCard8.Text

                Case Else
                    _Oper = Me.TrimCard10.Text

            End Select
            Return _Oper
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function GetOperName(obj As Object) As Integer
        Try
            Select Case DirectCast(obj, DevExpress.XtraTab.XtraTabPage).Name
                Case "page0"
                    Return 0
                Case "page1"
                    Return 1
                Case "page2"
                    Return 2
                Case "page3"
                    Return 4
                Case "page4"
                    Return 5
                Case "page5"
                    Return 6
                Case "page6"
                    Return 7
                Case "page7"
                    Return 8
                Case "page8"
                    Return 9
                Case "page9"
                    Return 10
                Case "page11"
                    Return 3
                Case Else
                    Return 0
            End Select
            Return 0
        Catch ex As Exception
            Return 0
        End Try
    End Function


    Private Function InitControl() As Boolean
        Try
            Dim _OperId As Integer = 0 : Dim _OperInfo As String = ""
            For Each Obj As Object In Me.otb.Controls
                For Each Objx As Object In Obj.Controls
                    _OperId = GetOperName(Obj)

                    _OperInfo = getOperInfo(_OperId)
                    If HI.ENM.Control.GeTypeControl(Objx) = ENM.Control.ControlType.PictureEdit Then
                        With CType(Objx, DevExpress.XtraEditors.PictureEdit)

                            .Properties.SizeMode = PictureSizeMode.Stretch
                        End With
                        Continue For
                    End If
                    For Each Objxs As Object In Objx.Controls
                        If HI.ENM.Control.GeTypeControl(Objxs) = ENM.Control.ControlType.PictureEdit Then

                            With CType(Objxs, DevExpress.XtraEditors.PictureEdit)
                                .Properties.SizeMode = PictureSizeMode.Stretch
                            End With
                            Continue For
                        End If




                        For Each Objxss As Object In Objxs.Controls
                            If HI.ENM.Control.GeTypeControl(Objxss) = ENM.Control.ControlType.PictureEdit Then

                                With CType(Objxss, DevExpress.XtraEditors.PictureEdit)
                                    .Properties.SizeMode = PictureSizeMode.Stretch
                                End With
                                Continue For
                            End If
                            For Each Objx1 As Object In Objxss.Controls
                                If HI.ENM.Control.GeTypeControl(Objx1) = ENM.Control.ControlType.PictureEdit Then

                                    With CType(Objx1, DevExpress.XtraEditors.PictureEdit)
                                        .Properties.SizeMode = PictureSizeMode.Stretch
                                    End With
                                    Continue For
                                End If

                            Next
                        Next

                    Next



                Next



            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function



    Private Function Save() As Boolean
        Try
            Dim _OperId As Integer = 0 : Dim _OperInfo As String = ""
            For Each Obj As Object In Me.otb.Controls
                For Each Objx As Object In Obj.Controls
                    _OperId = GetOperName(Obj)
                    'If IsNumeric(DirectCast(Obj, System.Windows.Forms.Control).Tag.ToString(0)) Then
                    '    _OperId = Val("0" & DirectCast(Obj, System.Windows.Forms.Control).Tag.ToString(0))
                    'Else
                    '    _OperId = Val("0")
                    'End If
                    _OperInfo = getOperInfo(_OperId)
                    If HI.ENM.Control.GeTypeControl(Objx) = ENM.Control.ControlType.PictureEdit Then
                        With CType(Objx, DevExpress.XtraEditors.PictureEdit)
                            SaveImage(.Image, .Name.ToString, _OperId, _OperInfo)
                        End With
                        Continue For
                    End If
                    For Each Objxs As Object In Objx.Controls
                        If HI.ENM.Control.GeTypeControl(Objxs) = ENM.Control.ControlType.PictureEdit Then

                            With CType(Objxs, DevExpress.XtraEditors.PictureEdit)
                                SaveImage(.Image, .Name.ToString, _OperId, _OperInfo)
                            End With
                            Continue For
                        End If
                        If HI.ENM.Control.GeTypeControl(Objxs) = ENM.Control.ControlType.ButtonEdit Then

                            With CType(Objxs, DevExpress.XtraEditors.ButtonEdit)
                                SaveVideo(.Text, .Name.ToString, _OperId, _OperInfo)


                            End With
                            Continue For
                        End If




                        For Each Objxss As Object In Objxs.Controls
                            If HI.ENM.Control.GeTypeControl(Objxss) = ENM.Control.ControlType.PictureEdit Then

                                With CType(Objxss, DevExpress.XtraEditors.PictureEdit)
                                    SaveImage(.Image, .Name.ToString, _OperId, _OperInfo)
                                End With
                                Continue For
                            End If
                            For Each Objx1 As Object In Objxss.Controls
                                If HI.ENM.Control.GeTypeControl(Objx1) = ENM.Control.ControlType.PictureEdit Then

                                    With CType(Objx1, DevExpress.XtraEditors.PictureEdit)
                                        SaveImage(.Image, .Name.ToString, _OperId, _OperInfo)
                                    End With
                                    Continue For
                                End If

                            Next
                        Next

                    Next



                Next



            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function loadvideo(OPerId As Integer, PathImage As String) As Boolean
        Try



            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function LoadImgObjet(OPerId As Integer, PathImage As String) As Boolean
        Try
            Dim _ObjName As String = PathImage.Split("_")(2)
            Dim _OperId As Integer = 0 : Dim _OperInfo As String = ""
            For Each Obj As Object In Me.otb.Controls
                For Each Objx As Object In Obj.Controls
                    _OperId = GetOperName(Obj)
                    If _OperId = OPerId Then
                        If HI.ENM.Control.GeTypeControl(Objx) = ENM.Control.ControlType.PictureEdit Then
                            If CType(Objx, DevExpress.XtraEditors.PictureEdit).Name = _ObjName Then
                                CType(Objx, DevExpress.XtraEditors.PictureEdit).Image = loadImag(CType(Objx, DevExpress.XtraEditors.PictureEdit), PathImage)
                                Continue For
                            End If

                        End If
                        If HI.ENM.Control.GeTypeControl(Objx) = ENM.Control.ControlType.ButtonEdit Then
                            'Dim _ObjNames As String = Microsoft.VisualBasic.Left(_ObjName, _ObjName.IndexOf("."))
                            Dim _ObjNames As String = ""
                            Try
                                _ObjNames = Microsoft.VisualBasic.Left(_ObjName, _ObjName.IndexOf("."))
                            Catch ex As Exception
                                _ObjNames = _ObjName
                            End Try
                            If CType(Objx, DevExpress.XtraEditors.ButtonEdit).Name = _ObjNames Then
                                'CType(Objxs, DevExpress.XtraEditors.PictureEdit).Image = loadImag(CType(Objxs, DevExpress.XtraEditors.PictureEdit), PathImage)
                                Continue For
                            End If

                        End If

                        For Each Objxs As Object In Objx.Controls
                            If HI.ENM.Control.GeTypeControl(Objxs) = ENM.Control.ControlType.PictureEdit Then
                                If CType(Objxs, DevExpress.XtraEditors.PictureEdit).Name = _ObjName Then
                                    CType(Objxs, DevExpress.XtraEditors.PictureEdit).Image = loadImag(CType(Objxs, DevExpress.XtraEditors.PictureEdit), PathImage)
                                    Continue For
                                End If

                            End If
                            If HI.ENM.Control.GeTypeControl(Objxs) = ENM.Control.ControlType.ButtonEdit Then
                                Dim _ObjNames As String = ""
                                Try
                                    _ObjNames = Microsoft.VisualBasic.Left(_ObjName, _ObjName.IndexOf("."))
                                Catch ex As Exception
                                    _ObjNames = _ObjName
                                End Try
                                If CType(Objxs, DevExpress.XtraEditors.ButtonEdit).Name = _ObjNames Then
                                    With CType(Objxs, DevExpress.XtraEditors.ButtonEdit)
                                        .Text = _Path & "\video\TrimCard\" & "" & Microsoft.VisualBasic.Left(Me.FNHSysStyleId.Text, 6).ToString() & "\" & PathImage
                                    End With


                                    Continue For
                                End If

                            End If

                            For Each Objxss As Object In Objxs.Controls
                                If HI.ENM.Control.GeTypeControl(Objxss) = ENM.Control.ControlType.PictureEdit Then
                                    If CType(Objxs, DevExpress.XtraEditors.PictureEdit).Name = _ObjName Then
                                        CType(Objxs, DevExpress.XtraEditors.PictureEdit).Image = loadImag(CType(Objxss, DevExpress.XtraEditors.PictureEdit), PathImage)
                                        Continue For
                                    End If

                                End If

                                If HI.ENM.Control.GeTypeControl(Objxss) = ENM.Control.ControlType.ButtonEdit Then
                                    ' Dim _ObjNames As String = Microsoft.VisualBasic.Left(_ObjName, _ObjName.IndexOf("."))
                                    Dim _ObjNames As String = ""
                                    Try
                                        _ObjNames = Microsoft.VisualBasic.Left(_ObjName, _ObjName.IndexOf("."))
                                    Catch ex As Exception
                                        _ObjNames = _ObjName
                                    End Try
                                    If CType(Objxss, DevExpress.XtraEditors.ButtonEdit).Name = _ObjNames Then
                                        'CType(Objxs, DevExpress.XtraEditors.PictureEdit).Image = loadImag(CType(Objxs, DevExpress.XtraEditors.PictureEdit), PathImage)
                                        Continue For
                                    End If

                                End If

                                For Each Objx1 As Object In Objxss.Controls
                                    If HI.ENM.Control.GeTypeControl(Objx1) = ENM.Control.ControlType.PictureEdit Then
                                        If CType(Objx1, DevExpress.XtraEditors.PictureEdit).Name = _ObjName Then
                                            CType(Objx1, DevExpress.XtraEditors.PictureEdit).Image = loadImag(CType(Objx1, DevExpress.XtraEditors.PictureEdit), PathImage)
                                            Continue For
                                        End If
                                    End If

                                    If HI.ENM.Control.GeTypeControl(Objx1) = ENM.Control.ControlType.ButtonEdit Then
                                        'Dim _ObjNames As String = Microsoft.VisualBasic.Left(_ObjName, _ObjName.IndexOf("."))
                                        Dim _ObjNames As String = ""
                                        Try
                                            _ObjNames = Microsoft.VisualBasic.Left(_ObjName, _ObjName.IndexOf("."))
                                        Catch ex As Exception
                                            _ObjNames = _ObjName
                                        End Try
                                        If CType(Objx1, DevExpress.XtraEditors.ButtonEdit).Name = _ObjNames Then
                                            'CType(Objxs, DevExpress.XtraEditors.PictureEdit).Image = loadImag(CType(Objxs, DevExpress.XtraEditors.PictureEdit), PathImage)
                                            Continue For
                                        End If


                                    End If

                                Next



                            Next

                        Next


                    End If
                Next
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function



    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        If Save() Then
            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        End If
    End Sub

    Private Function SaveData(_ImgName As String, OperInfo As String, OperId As Integer, _Seq As Integer, Optional ByVal _ImageType As String = "0") As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            With DirectCast(Me.ogcOrderNo.DataSource, DataTable)
                .AcceptChanges()

                _oDt = .Copy
            End With
            For Each R As DataRow In _oDt.Select("FTSelect ='1'")

                _Cmd = " Update a "
                _Cmd &= vbCrLf & " set a.FTImagePath='" & _ImgName & "'"
                _Cmd &= vbCrLf & " , a.FTOperInfo='" & OperInfo & "'"


                _Cmd &= vbCrLf & " ,a.FTRemark='" & Me.FTRemark.Text & "'"
                _Cmd &= vbCrLf & " From  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "..TPRODTQATrimcard a "
                _Cmd &= vbCrLf & " where FNHSysStyleId = " & Val(Me.FNHSysStyleId.Properties.Tag)
                _Cmd &= vbCrLf & " and FNHSysSeasonId= " & Val(Me.FNHSysSeasonId.Properties.Tag)
                _Cmd &= vbCrLf & " and FTOrderNo='" & R!FTOrderNo.ToString & "'"
                _Cmd &= vbCrLf & " and FTSubOrderNo='" & R!FTSubOrderNo.ToString & "'"
                _Cmd &= vbCrLf & " and FNOperId=" & Val(OperId)
                _Cmd &= vbCrLf & " and FNSeq=" & Val(_Seq)
                If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD) = False Then

                    _Cmd = " insert into   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "..TPRODTQATrimcard (FTInsUser, FDInsDate, FTInsTime,"
                    _Cmd &= vbCrLf & "FNHSysStyleId, FNHSysSeasonId, FNHSysCountryId, FNOperId, FNSeq, FTOrderNo, FTSubOrderNo, FTOperInfo, FTImagePath,FTRemark,ImageType)"
                    _Cmd &= vbCrLf & " Select '" & HI.ST.UserInfo.UserName & "'"
                    _Cmd &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & " ," & Val(Me.FNHSysStyleId.Properties.Tag)
                    _Cmd &= vbCrLf & " ," & Val(Me.FNHSysSeasonId.Properties.Tag)
                    _Cmd &= vbCrLf & "  ," & Val(R!FNHSysCountryId.ToString)
                    _Cmd &= vbCrLf & " ," & Val(OperId)
                    _Cmd &= vbCrLf & " ," & Val(_Seq)
                    _Cmd &= vbCrLf & " ,'" & R!FTOrderNo.ToString & "'"
                    _Cmd &= vbCrLf & " ,'" & R!FTSubOrderNo.ToString & "'"
                    _Cmd &= vbCrLf & " ,'" & OperInfo & "'"
                    _Cmd &= vbCrLf & " ,'" & _ImgName & "'"
                    _Cmd &= vbCrLf & " ,'" & Me.FTRemark.Text & "'"
                    _Cmd &= vbCrLf & " ,'" & _ImageType & "'"
                    If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD) = False Then
                        Return False
                    End If

                End If
                'Exit For
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmapprove_Click(sender As Object, e As EventArgs) Handles ocmapprove.Click
        Try
            Dim _Cmd As String = ""
            Dim _scount As Integer = 0
            With DirectCast(Me.ogcOrderNo.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Select("FTSelect = '1'")
                    _Cmd = "Update  t "
                    _Cmd &= vbCrLf & " set  t.FTStateApprove='1'  , t.FTStateReject='0'"
                    _Cmd &= vbCrLf & " , t.FTAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & " ,t.FTAppTime=" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & " ,t.FDAppDate=" & HI.UL.ULDate.FormatDateDB

                    _Cmd &= vbCrLf & " from " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "..TPRODTQATrimcard t "
                    _Cmd &= vbCrLf & " where FTSubOrderNo='" & R!FTSubOrderNo.ToString & "'"

                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                    _scount += +1
                Next

            End With


            If _scount > 0 Then
                HI.MG.ShowMsg.mInfo("อนุมัติเรียบร้อย ....", 2001161658, Me.Text)
            Else
                HI.MG.ShowMsg.mInfo("กรุณาเลือก รายการที่อนุมัติ !!!!", 2001161659, Me.Text)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmreject_Click(sender As Object, e As EventArgs) Handles ocmreject.Click
        Try
            Dim _Cmd As String = ""
            Dim _scount As Integer = 0
            With DirectCast(Me.ogcOrderNo.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Select("FTSelect = '1'")
                    _Cmd = "Update  t "
                    _Cmd &= vbCrLf & " set  t.FTStateApprove='0'  , t.FTStateReject='1' "
                    _Cmd &= vbCrLf & " , t.FTRejectBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & " ,t.FTRejectTime=" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & " ,t.FDRejectDate=" & HI.UL.ULDate.FormatDateDB

                    _Cmd &= vbCrLf & " from " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "..TPRODTQATrimcard t "
                    _Cmd &= vbCrLf & " where FTSubOrderNo='" & R!FTSubOrderNo.ToString & "'"

                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                    _scount += +1
                Next


            End With

            If _scount > 0 Then
                HI.MG.ShowMsg.mInfo("ยกเลิกอนุมัติเรียบร้อย ....", 2001161660, Me.Text)
            Else
                HI.MG.ShowMsg.mInfo("กรุณาเลือก รายการที่ไม่อนุมัติ !!!!", 2001161662, Me.Text)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub PSize5_EditValueChanged(sender As Object, e As EventArgs) Handles PSize5.EditValueChanged
        Try
            'PSizevideo.URL = Me.PSize5.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PLicesed5_EditValueChanged(sender As Object, e As EventArgs) Handles PLicesed5.EditValueChanged
        Try
            ' PLicesedvideo.URL = Me.PLicesed5.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PCare5_EditValueChanged(sender As Object, e As EventArgs) Handles PCare5.EditValueChanged
        Try
            ' Me.PCarevideo.URL = Me.PCare5.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PNFC5_EditValueChanged(sender As Object, e As EventArgs) Handles PNFC5.EditValueChanged
        Try
            ' Me.PNFCvideo.URL = Me.PNFC5.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PID5_EditValueChanged(sender As Object, e As EventArgs) Handles PID5.EditValueChanged
        Try
            ' Me.PIDvideo.URL = Me.PID5.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PCham5_EditValueChanged(sender As Object, e As EventArgs) Handles PCham5.EditValueChanged
        Try
            ' Me.PChamvideo.URL = Me.PCham5.Text
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PJOCKTAG5_EditValueChanged(sender As Object, e As EventArgs) Handles PJOCKTAG5.EditValueChanged
        Try
            ' Me.PJOCKTAGvideo.URL = Me.PJOCKTAG5.Text
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PSLabel9_EditValueChanged(sender As Object, e As EventArgs) Handles PSLabel9.EditValueChanged
        Try
            ' Me.PSLabelvideo.URL = Me.PSLabel9.Text
        Catch ex As Exception
        End Try
    End Sub

    Private Sub psticker5_EditValueChanged(sender As Object, e As EventArgs) Handles psticker5.EditValueChanged
        Try
            'Me.pstickervideo.URL = Me.psticker5.Text
        Catch ex As Exception
        End Try
    End Sub

    Private Sub pbag5_EditValueChanged(sender As Object, e As EventArgs) Handles pbag5.EditValueChanged
        Try
            ' Me.pbagvideo.URL = Me.pbag5.Text
        Catch ex As Exception
        End Try
    End Sub
    Private Sub pelite5_EditValueChanged(sender As Object, e As EventArgs) Handles pelite5.EditValueChanged
        Try
            ' Me.pelitevedio.URL = Me.pelite5.Text
        Catch ex As Exception
        End Try
    End Sub
    Private Sub PLicesed5_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles PLicesed5.ButtonClick
        Try
            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg
                '.InitialDirectory = "D:\"
                '.InitialDirectory = "" 
                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = False
                If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    Me.PLicesed5.Text = .FileName
                Else
                    Me.PLicesed5.Text = ""
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PSize5_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles PSize5.ButtonClick
        Try
            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg
                '.InitialDirectory = "D:\"
                '.InitialDirectory = "" 
                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = False
                If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    Me.PSize5.Text = .FileName
                Else
                    Me.PSize5.Text = ""
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PCare5_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles PCare5.ButtonClick
        Try
            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg
                '.InitialDirectory = "D:\"
                '.InitialDirectory = "" 
                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = False
                If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    Me.PCare5.Text = .FileName
                Else
                    Me.PCare5.Text = ""
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PNFC5_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles PNFC5.ButtonClick
        Try
            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg
                '.InitialDirectory = "D:\"
                '.InitialDirectory = "" 
                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = False
                If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    Me.PNFC5.Text = .FileName
                Else
                    Me.PNFC5.Text = ""
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PID5_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles PID5.ButtonClick
        Try
            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg
                '.InitialDirectory = "D:\"
                '.InitialDirectory = "" 
                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = False
                If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    Me.PID5.Text = .FileName
                Else
                    Me.PID5.Text = ""
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PCham5_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles PCham5.ButtonClick
        Try
            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg
                '.InitialDirectory = "D:\"
                '.InitialDirectory = "" 
                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = False
                If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    Me.PCham5.Text = .FileName
                Else
                    Me.PCham5.Text = ""
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PJOCKTAG5_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles PJOCKTAG5.ButtonClick
        Try
            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg
                '.InitialDirectory = "D:\"
                '.InitialDirectory = "" 
                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = False
                If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    Me.PJOCKTAG5.Text = .FileName
                Else
                    Me.PJOCKTAG5.Text = ""
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PSLabel9_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles PSLabel9.ButtonClick
        Try
            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg
                '.InitialDirectory = "D:\"
                '.InitialDirectory = "" 
                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = False
                If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    Me.PSLabel9.Text = .FileName
                Else
                    Me.PSLabel9.Text = ""
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub psticker5_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles psticker5.ButtonClick
        Try
            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg
                '.InitialDirectory = "D:\"
                '.InitialDirectory = "" 
                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = False
                If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    Me.psticker5.Text = .FileName
                Else
                    Me.psticker5.Text = ""
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub pbag5_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles pbag5.ButtonClick
        Try
            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg
                '.InitialDirectory = "D:\"
                '.InitialDirectory = "" 
                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = False
                If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    Me.pbag5.Text = .FileName
                Else
                    Me.pbag5.Text = ""
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub pelite5_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles pelite5.ButtonClick
        Try
            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg
                '.InitialDirectory = "D:\"
                '.InitialDirectory = "" 
                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = False
                If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    Me.pbag5.Text = .FileName
                Else
                    Me.pbag5.Text = ""
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub


End Class