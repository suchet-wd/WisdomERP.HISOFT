Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.Math

Public Class wDailyQAReport_bak


    Private _Path As String = System.Windows.Forms.Application.StartupPath.ToString
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

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
                If Rx!FPStyleImage1.ToString <> "" Then
                    Try
                        Me.FTImage.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\MODEL\" & Microsoft.VisualBasic.Left(Me.FNHSysStyleId.Text, 6).ToString() & "\" & Microsoft.VisualBasic.Left(Me.FNHSysStyleId.Text, 6).ToString() & "_1.JPG")

                        If Me.FTImage.Image Is Nothing Then
                            Me.FTImage.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage1))
                        End If
                    Catch ex As Exception
                        Me.FTImage.Image = Nothing
                    End Try
                End If
                If Rx!FPStyleImage2.ToString <> "" Then
                    Try
                        Me.FTImage2.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\MODEL\" & Microsoft.VisualBasic.Left(Me.FNHSysStyleId.Text, 6).ToString() & "\" & Microsoft.VisualBasic.Left(Me.FNHSysStyleId.Text, 6).ToString() & "_2.JPG")

                        If Me.FTImage2.Image Is Nothing Then
                            Me.FTImage2.Image = _ResizeImage(HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage2))
                        End If
                    Catch ex As Exception
                        Me.FTImage2.Image = Nothing
                    End Try
                End If

            Next
            Call LoadDrawRectangleRectangle(0)
            Call LoadDrawRectangleRectangle2(1)
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
        _Qry &= vbCrLf & "Where LEFT(B.FTStyleCode,6)='" & Microsoft.VisualBasic.Left(Me.FNHSysStyleId.Text, 6) & "'"
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
            'AddHandler textBox12.MouseDown, AddressOf ObjClick
            'AddHandler textBox12.MouseMove, AddressOf Obj_MouseMove
        Next

    End Sub

    Private textBox122 As ZBobb.AlphaBlendTextBox
    Private Sub LoadDrawRectangleRectangle2(ByVal _Type As Integer)

        Dim _Qry As String = ""
        Dim _oDt As DataTable

        _Qry = "SELECT    A.FNHSysStyleId, A.FNSeq, (((A.FNPointX /480)*100)*380/100) AS FNPointX,(((A.FNPointY /480)*100)*380/100) AS FNPointY, A.FTPicType, (((A.FNPicHeight /480)*100)*380/100)  AS FNPicHeight ,(((A.FNPicWidth /480)*100)*380/100) AS FNPicWidth"
        _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTStylePoint AS A WITH(NOLOCK) LEFT OUTER JOIN "
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B WITH(NOLOCK) ON A.FNHSysStyleId = B.FNHSysStyleId "
        _Qry &= vbCrLf & "Where LEFT(B.FTStyleCode,6)='" & Microsoft.VisualBasic.Left(Me.FNHSysStyleId.Text, 6) & "'"
        _Qry &= vbCrLf & "AND A.FTPicType=" & CInt(_Type)
        _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        For Each R As DataRow In _oDt.Rows

            textBox122 = New ZBobb.AlphaBlendTextBox
            textBox122.BorderStyle = BorderStyle.Fixed3D

            textBox122.BackColor = Color.LightPink
            textBox122.Multiline = True
            textBox122.ReadOnly = True
            textBox122.ForeColor = Color.Transparent
            textBox122.ShortcutsEnabled = False
            Me.FTImage2.SendToBack()
            textBox122.Location = New System.Drawing.Point(CInt(Abs(R!FNPointX)), CInt(Abs(R!FNPointY)))
            textBox122.Size = New System.Drawing.Size(CInt(Abs(R!FNPicWidth)), CInt(Abs(R!FNPicHeight)))
            textBox122.Name = "" & R!FNSeq.ToString
            Me.FTImage2.Controls.Add(textBox122)
            textBox122.BringToFront()
            'AddHandler textBox12.MouseDown, AddressOf ObjClick
            'AddHandler textBox12.MouseMove, AddressOf Obj_MouseMove
        Next

    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
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
            Dim _oDt As DataTable
            _Cmd = "Select Top 1  FTColorway  From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown WITH(NOLOCK) "
            _Cmd &= vbCrLf & " Where FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            _FTColorway = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN, "")

            Me.oFTColorway.Text = _FTColorway
            Me.oFNHSysUnitSectId.Text = Me.FNHSysUnitSectId.Text
            Me.oFNHSysStyleId.Text = Me.FNHSysStyleId.Text
            Me.oFTOrderNo.Text = Me.FTOrderNo.Text
            Me.oFNHSysUnitSectId_None.Text = Me.FNHSysUnitSectId_None.Text
            Me.oFNHSysStyleId_None.Text = Me.FNHSysStyleId_None.Text



        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadGrid()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable


            _Cmd = "     DECLARE @DynamicPivotQuery AS NVARCHAR(MAX)"
            _Cmd &= vbCrLf & " DECLARE @ColumnName varchar(max)"
            _Cmd &= vbCrLf & "  DECLARE @ColumnNameMax varchar(max)            "
            _Cmd &= vbCrLf & "   SELECT @ColumnName= ISNULL(@ColumnName + ',','') "
            _Cmd &= vbCrLf & "      +  QUOTENAME(FNHourNo)"
            _Cmd &= vbCrLf & "       FROM ("
            _Cmd &= vbCrLf & "       SELECT  LEFT(A.FNHourNo,2) AS FNHourNo"
            _Cmd &= vbCrLf & "       FROM         TPRODTQA_SubDetail AS A LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " 							  HITECH_MASTER.dbo.TQAMQADetail AS B ON A.FNHSysQADetailId = B.FNHSysQADetailId"
            _Cmd &= vbCrLf & " 		WHERE     (A.FDQADate = '2015/02/18')"
            _Cmd &= vbCrLf & " 		group by    LEFT(A.FNHourNo,2)"
            _Cmd &= vbCrLf & "          ) AS Courses                "
            _Cmd &= vbCrLf & "        SET @DynamicPivotQuery =   N'SELECT FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate,   FNHSysQADetailId, ' + @ColumnName + '"
            _Cmd &= vbCrLf & " 		INTO #Tmp "
            _Cmd &= vbCrLf & " 		From ("
            _Cmd &= vbCrLf & " 		SELECT COUNT(*) AS Qty, A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo, A.FDQADate,   A.FNHSysQADetailId, LEFT(A.FNHourNo,2) AS FNHourNo "
            _Cmd &= vbCrLf & " 		FROM         TPRODTQA_SubDetail AS A LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " 							  HITECH_MASTER.dbo.TQAMQADetail AS B ON A.FNHSysQADetailId = B.FNHSysQADetailId"
            _Cmd &= vbCrLf & " 		WHERE     (A.FDQADate = ''2015/02/18'')"
            _Cmd &= vbCrLf & " 		group by     A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo, A.FDQADate,   A.FNHSysQADetailId, B.FTQADetailCode, B.FTQADetailNameTH, "
            _Cmd &= vbCrLf & "                   B.FTQADetailNameEN ,  LEFT(A.FNHourNo,2)) AS A       "
            _Cmd &= vbCrLf & "      PIVOT(sum(Qty) FOR   FNHourNo IN (' + @ColumnName + ')) AS PVTTable"

            _Cmd &= vbCrLf & "  Select '''' AS FNSeq , FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate, '''' as FNHSysQADetailId, '''' as  FTQADetailCode "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", ''จำนวนงานออก'' as FTQADetailName"
            Else
                _Cmd &= vbCrLf & ",''Output'' as FTQADetailName"
            End If
            _Cmd &= vbCrLf & " , ' + @ColumnName + '"
            _Cmd &= vbCrLf & " From ( SELECT     FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate, FNQAInQty, LEFT(FNHourNo,2) AS FNHourNo"
            _Cmd &= vbCrLf & "           FROM TPRODTQA"
            _Cmd &= vbCrLf & " where FDQADate = ''2015/02/18'') AS A"
            _Cmd &= vbCrLf & " pivot (Sum(FNQAInQty) for FNHourNo in(' + @ColumnName + ')) AS T0     "
            _Cmd &= vbCrLf & "             UNION"
            _Cmd &= vbCrLf & " Select '''' AS FNSeq , FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate, '''' as FNHSysQADetailId, '''' as  FTQADetailCode "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ",''จำนวนสุ่มตรวจ'' as FTQADetailName"
            Else
                _Cmd &= vbCrLf & ",''Rendom'' as FTQADetailName"
            End If
            _Cmd &= vbCrLf & " ,' + @ColumnName + '"
            _Cmd &= vbCrLf & " From (SELECT     FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate, LEFT(FNHourNo,2) AS FNHourNo,   FNQAActualQty  "
            _Cmd &= vbCrLf & "            FROM TPRODTQA"
            _Cmd &= vbCrLf & " WHERE FDQADate = ''2015/02/18'' ) AS B"
            _Cmd &= vbCrLf & " Pivot (sum(FNQAActualQty) for FNHourNo in (' + @ColumnName + ')) AS T1"
            _Cmd &= vbCrLf & "          UNION"
            _Cmd &= vbCrLf & " Select '''' AS FNSeq , FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate, '''' as FNHSysQADetailId, '''' as  FTQADetailCode "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", ''อันดง'' as FTQADetailName"
            Else
                _Cmd &= vbCrLf & ",''Andon'' as FTQADetailName"
            End If
            _Cmd &= vbCrLf & " , ' + @ColumnName + '"
            _Cmd &= vbCrLf & "  From ( SELECT     FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate,   Isnull(FNAndon,0) AS FNAndon , LEFT(FNHourNo,2) AS FNHourNo"
            _Cmd &= vbCrLf & "            FROM TPRODTQA"
            _Cmd &= vbCrLf & " where FDQADate = ''2015/02/18'') AS C "
            _Cmd &= vbCrLf & " pivot (Sum(FNAndon) for FNHourNo in(' + @ColumnName + ')) AS T2"
            _Cmd &= vbCrLf & "             UNION"
            _Cmd &= vbCrLf & "  SELECT  convert(nvarchar(3), ROW_NUMBER() Over (Order By A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo, A.FDQADate,    A.FNHSysQADetailId)) AS FNSeq"

            _Cmd &= vbCrLf & "     ,  A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo, A.FDQADate,    A.FNHSysQADetailId, B.FTQADetailCode "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", B.FTQADetailNameTH as FTQADetailName"
            Else
                _Cmd &= vbCrLf & ", B.FTQADetailNameEN as FTQADetailName"
            End If
            _Cmd &= vbCrLf & "                   ,' + @ColumnName + '"
            _Cmd &= vbCrLf & " FROM         TPRODTQA_SubDetail AS A LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "                     HITECH_MASTER.dbo.TQAMQADetail AS B ON A.FNHSysQADetailId = B.FNHSysQADetailId"
            _Cmd &= vbCrLf & "                      LEFT OUTER JOIN #Tmp AS T ON A.FNHSysQADetailId = T.FNHSysQADetailId and A.FNHSysStyleId = T.FNHSysStyleId"
            _Cmd &= vbCrLf & " 					and A.FNHSysUnitSectId = T.FNHSysUnitSectId  "
            _Cmd &= vbCrLf & " WHERE     (A.FDQADate = ''2015/02/18'')"
            _Cmd &= vbCrLf & " group by A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo, A.FDQADate,    A.FNHSysQADetailId, B.FTQADetailCode, B.FTQADetailNameTH, "
            _Cmd &= vbCrLf & "                    B.FTQADetailNameEN ,' + @ColumnName + '  '"

            _Cmd &= vbCrLf & "      EXEC sp_executesql @DynamicPivotQuery    "
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Me.ogcDetailDaily.DataSource = _oDt

        Catch ex As Exception

        End Try
    End Sub

    Private Sub wDailyQAReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.InitGrid()
    End Sub
End Class