Imports DevExpress.XtraEditors
Imports System.Windows.Forms
Imports System.Drawing
Imports DevExpress.XtraBars.Docking2010.Views.WindowsUI
Imports DevExpress.XtraBars.Docking2010.Views

Public Class wQCAccessoryInspec

    Private CalcPopup As wPopupCalc
    Private WCPopup As wPopupQCFabricBarcode
    Private WCPopupEdit As wPopupQCFabricBarcodeList

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

        WCPopup = New wPopupQCFabricBarcode
        HI.TL.HandlerControl.AddHandlerObj(WCPopup)

        Dim oSysLang As New ST.SysLanguage
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, WCPopup.Name.ToString.Trim, WCPopup)
        Catch ex As Exception
        Finally
        End Try

        WCPopupEdit = New wPopupQCFabricBarcodeList
        HI.TL.HandlerControl.AddHandlerObj(WCPopupEdit)

        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, WCPopupEdit.Name.ToString.Trim, WCPopupEdit)
        Catch ex As Exception
        Finally
        End Try

        AddHandler TileControl.ItemClick, AddressOf SubTileItem_Click

        _StateEnter = False
    End Sub


    Private _TileGroup As DevExpress.XtraEditors.TileGroup


    Private Sub subCreateLayout(BarcodeKey As String)

        TileControl.Groups.Clear()

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

        _Qry &= vbCrLf & ",CASE WHEN ISNULL(B.FTBarcodeNo,'')<>'' THEN '1' ELSE '' END AS FTStateCheck,A.FNQCFabricType"

        _Qry &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMQCFabric AS A  WITH(NOLOCK) "
        _Qry &= vbCrLf & " LEFT OUTER JOIN (Select FTBarcodeNo,FNHSysQCFabricDetailId "
        _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabricBarcode_Defect  WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
        _Qry &= vbCrLf & " GROUP BY  FTBarcodeNo,FNHSysQCFabricDetailId "

        _Qry &= vbCrLf & " ) AS B ON A.FNHSysQCFabricDetailId=B.FNHSysQCFabricDetailId  "
        _Qry &= vbCrLf & " WHERE  A.FTStateActive = '1'"

        _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

        TileControl.ShowText = False
        TileControl.Text = ""
        TileControl.ItemSize = 70
        TileControl.ScrollMode = TileControlScrollMode.ScrollBar
        TileControl.ShowGroupText = True

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


                TileControl.Groups.Add(_TileGroup)

            Next
        End If





        dtgrp.Dispose()
        _oDt.Dispose()

    End Sub


    Private Function SaveDataSubDetail(_QADetailId As Integer, YardNo As Decimal, DSize As Decimal, DPoint As Decimal, DRemark As String) As Boolean
        '    Try
        '        Dim _Cmd As String = ""
        '        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
        '        HI.Conn.SQLConn.SqlConnectionOpen()
        '        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        '        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        '        _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabricBarcode_Defect"
        '        _Cmd &= " (FTInsUser, FDInsDate, FTInsTime ,  FTBarcodeNo, FNHSysQCFabricDetailId, FNSeq, FNYardNo, FNSize, FNPoint, FTRemark)"
        '        _Cmd &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
        '        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
        '        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text.Trim()) & "'"
        '        _Cmd &= vbCrLf & "," & Val(_QADetailId) & " "
        '        _Cmd &= vbCrLf & ",ISNULL((SELECT MAX(FNSeq) AS FNSeq FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabricBarcode_Defect WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text.Trim()) & "' AND FNHSysQCFabricDetailId=" & Val(_QADetailId) & " ),0) +1 AS FNSeq "
        '        _Cmd &= vbCrLf & "," & Val(YardNo) & " "
        '        _Cmd &= vbCrLf & "," & Val(DSize) & " "
        '        _Cmd &= vbCrLf & "," & Val(DPoint) & " "
        '        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(DRemark) & "'"

        '        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
        '            HI.Conn.SQLConn.Tran.Rollback()
        '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        '            Return False
        '        End If

        '        HI.Conn.SQLConn.Tran.Commit()
        '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        '        Call SumPoint()
        '        Return True
        '    Catch ex As Exception
        '        HI.Conn.SQLConn.Tran.Rollback()
        '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        '        Return False
        '    End Try
    End Function


    Private Function DeleteDataSubDetail(_QADetailId As Integer, Seq As Integer) As Integer
        'Try
        '    Dim _Cmd As String = ""

        '    HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
        '    HI.Conn.SQLConn.SqlConnectionOpen()
        '    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        '    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        '    _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo. TINVENQCFabricBarcode_Defect"
        '    _Cmd &= vbCrLf & " WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text.Trim) & "'"
        '    _Cmd &= vbCrLf & "AND FNHSysQCFabricDetailId=" & CInt("0" & _QADetailId)
        '    _Cmd &= vbCrLf & " AND FNSeq='" & Seq & "'"

        '    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
        '        HI.Conn.SQLConn.Tran.Rollback()
        '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        '        Return -1
        '    End If


        '    _Cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo. TINVENQCFabricBarcode_Defect SET FNSeq=FNSeq-1"
        '    _Cmd &= vbCrLf & " WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text.Trim) & "'"
        '    _Cmd &= vbCrLf & "AND FNHSysQCFabricDetailId=" & CInt("0" & _QADetailId)
        '    _Cmd &= vbCrLf & " AND FNSeq>'" & Seq & "'"
        '    HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


        '    HI.Conn.SQLConn.Tran.Commit()
        '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        '    Dim DCount As Integer = 0


        '    _Cmd = "SELECT SUM(1) AS DCount From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabricBarcode_Defect"
        '    _Cmd &= vbCrLf & " WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text.Trim) & "'"
        '    _Cmd &= vbCrLf & "AND FNHSysQCFabricDetailId=" & CInt("0" & _QADetailId)
        '    DCount = Val(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_INVEN, "0"))

        '    Call SumPoint()

        '    Return DCount

        'Catch ex As Exception
        '    HI.Conn.SQLConn.Tran.Rollback()
        '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        '    Return -1
        'End Try
    End Function

    Private Sub ValidateSave()


    End Sub

    Private Sub SaveData(State As String)


        'Dim cmd As String = ""

        '        cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabricBarcode SET "
        '        cmd &= vbCrLf & " FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '        cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
        '        cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
        '        cmd &= vbCrLf & ",FTBatchNo='" & HI.UL.ULF.rpQuoted(FTBatchNo.Text) & "'"
        '        cmd &= vbCrLf & ", FTRollNo='" & HI.UL.ULF.rpQuoted(FTRollNo.Text) & "'"
        '        cmd &= vbCrLf & ", FNQuantity=" & FNQuantity.Value & ""
        '        cmd &= vbCrLf & ", FNActQuantity=" & FNActQuantity.Value & ""
        '        cmd &= vbCrLf & ", FTFabricFrontSize='" & HI.UL.ULF.rpQuoted(FTFabricFrontSize.Text) & "'"
        '        cmd &= vbCrLf & ", FTActFabricFrontSize=" & FTActFabricFrontSize.Value & ""
        '        cmd &= vbCrLf & ", FTActFabricFrontSizeMid=" & FTActFabricFrontSizeMid.Value & ""
        '        cmd &= vbCrLf & ", FTActFabricFrontSizeEnd=" & FTActFabricFrontSizeEnd.Value & ""
        '        cmd &= vbCrLf & ",FTStateReject='" & State & "'"
        '        cmd &= vbCrLf & ", FTShades='" & HI.UL.ULF.rpQuoted(FTShades.Text) & "'"
        '        cmd &= vbCrLf & "WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(FTBarcodeNo.Text.Trim()) & "'"

        '        If HI.Conn.SQLConn.ExecuteNonQuery(cmd, Conn.DB.DataBaseName.DB_INVEN) = True Then
        '            Dim dtsize As New DataTable
        '            dtsize.Columns.Add("FrontSize", GetType(Decimal))
        '            dtsize.Rows.Add(FTActFabricFrontSize.Value)
        '            dtsize.Rows.Add(FTActFabricFrontSizeMid.Value)
        '            dtsize.Rows.Add(FTActFabricFrontSizeEnd.Value)

        '            Dim FrontSize As String = ""

        '            For Each Rx As DataRow In dtsize.Select("FrontSize>0", "FrontSize ASC")
        '                FrontSize = Rx!FrontSize.ToString
        '                Exit For
        '            Next

        '            dtsize.Dispose()

        '            cmd = "  UPDATE A SET FTFabricFrontSize='" & HI.UL.ULF.rpQuoted(FrontSize) & "'"

        '            If State = "0" Then
        '                cmd &= vbCrLf & " ,FTStateQCAccept='1'"
        '                cmd &= vbCrLf & " ,FTStateQCReject='0'"
        '            Else
        '                cmd &= vbCrLf & " ,FTStateQCAccept='0'"
        '                cmd &= vbCrLf & " ,FTStateQCReject='1'"
        '            End If

        '            cmd &= vbCrLf & "   From   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As A  INNER Join"
        '            cmd &= vbCrLf & "   (Select A.FNHSysRawMatId, A.FTDocumentNo, A.FTBatchNo, A.FTPurchaseNo,A.FTRollNo"
        '            cmd &= vbCrLf & "     From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As A With(NOLOCK)  INNER Join [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As IM With(NOLOCK) On A.FNHSysRawMatId =IM.FNHSysRawMatId "
        '            cmd &= vbCrLf & "       INNER Join   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat As MM With(NOLOCK) On IM.FTRawMatCode  =MM.FTMainMatCode "
        '            cmd &= vbCrLf & "    WHERE(A.FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(FTBarcodeNo.Text.Trim) & "')"
        '            cmd &= vbCrLf & "   And (MM.FNMerMatType = 0)"
        '            cmd &= vbCrLf & " 	 ) As B On A.FTDocumentNo = B.FTDocumentNo And A.FTBatchNo = B.FTBatchNo And A.FTPurchaseNo = B.FTPurchaseNo And "
        '            cmd &= vbCrLf & "     A.FNHSysRawMatId = B.FNHSysRawMatId AND A.FTRollNo=B.FTRollNo "

        '            HI.Conn.SQLConn.ExecuteNonQuery(cmd, Conn.DB.DataBaseName.DB_INVEN)

        '            FNQCFabricRollStatus.SelectedIndex = Val(State)
        '        End If



    End Sub


    Private Sub SubTileItem_Click(sender As Object, e As TileItemEventArgs)
        Try


            Call ValidateSave()



            Static DfectId As Integer = 0
            If DfectId = 0 Then
                DfectId = Val(e.Item.Id.ToString)
                If ocmqc.Tag.ToString = "2" Then
                    If e.Item.Checked Then

                        Dim DCount As Integer = 0

                        Dim dtData As DataTable
                        Dim cmd As String = ""

                        cmd = "  Select  FNSeq, FNYardNo, FNSize, FNPoint, FTRemark"
                        cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabricBarcode_Defect  AS X WITH(NOLOCK)"
                        cmd &= vbCrLf & "   Where (FTBarcodeNo = N'" & HI.UL.ULF.rpQuoted("") & "') "
                        cmd &= vbCrLf & " And (FNHSysQCFabricDetailId = " & Val(DfectId) & ") "
                        dtData = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_INVEN)


                        With WCPopupEdit
                            .StateEnter = False
                            .ogc.DataSource = dtData.Copy
                            .ShowDialog()


                            If .StateEnter Then

                                Dim DataSeq As Integer = 0
                                DataSeq = .RowDataSeq


                                DCount = Me.DeleteDataSubDetail(DfectId, DataSeq)

                                e.Item.Checked = (DCount > 0)

                            End If

                        End With

                        dtData.Dispose()


                    End If
                Else
                    With WCPopup
                        .StateEnter = False
                        .FNYardNo.Value = 0
                        .FNSize.Value = 0
                        .FNPoint.Value = 0
                        .FTRemark.Text = ""
                        .ShowDialog()


                        If .StateEnter Then
                            Dim YardNo As Decimal = .FNYardNo.Value
                            Dim DSize As Decimal = .FNSize.Value
                            Dim DPoint As Decimal = .FNPoint.Value
                            Dim DNote As String = .FTRemark.Text.Trim

                            e.Item.Checked = SaveDataSubDetail(DfectId, YardNo, DSize, DPoint, DNote)

                        End If

                    End With

                End If
                DfectId = 0
            End If
            Exit Sub
        Catch ex As Exception
        End Try

    End Sub

    Private Sub wQAPreFinalCheckPoint_Load(sender As Object, e As EventArgs) Handles Me.Load
        TileControl.Groups.Clear()

        ocmqc.Appearance.BackColor = Color.FromArgb(192, 255, 192)
        ocmqc.Text = "QC"
        ocmqc.Tag = "1"



        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized

        ocmclose.Enabled = True
        ocmqc.Enabled = True
        ocmpass.Enabled = True
        ocmreject.Enabled = True

    End Sub

    Private Sub ocmclose_Click(sender As Object, e As EventArgs) Handles ocmclose.Click

        If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการปิดหน้าจอการทำงานการตราวจสอบคุณภาพผ้าใช่หรือไม่ ?", 151600421) = True Then
            Me.Close()
        End If

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



    Private Sub ocmqc_Click(sender As Object, e As EventArgs) Handles ocmqc.Click


        Call ValidateSave()




        If ocmqc.Tag.ToString = "2" Then
            ocmqc.Appearance.BackColor = Color.FromArgb(192, 255, 192)
            ocmqc.Text = "QC"
            ocmqc.Tag = "1"
        Else
            ocmqc.Appearance.BackColor = Color.FromArgb(255, 192, 192)
            ocmqc.Text = "EDIT QC"
            ocmqc.Tag = "2"
        End If
    End Sub



    Private Sub cleardataroll()


        FNQuantity.Value = 0
        FNActQuantity.Value = 0

    End Sub

    Private Function VerifyData() As Boolean
        Dim StatePass As Boolean = False



        If FNActQuantity.Value > 0 Then


            StatePass = True


        Else

                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNActQuantity_lbl.Text)
                FNActQuantity.Focus()
                FNActQuantity.SelectAll()


            End If

        'If StatePass = False Then
        '    HI.MG.ShowMsg.mInfo("กรุณาทำการระบุข้อมูล", 1812210547, Me.Text,, MessageBoxIcon.Warning)
        'End If


        Return StatePass
    End Function
    Private Sub ocmpass_Click(sender As Object, e As EventArgs) Handles ocmpass.Click


        Call ValidateSave()



        If VerifyData() = False Then
            Exit Sub
        End If
        SaveData("0")

    End Sub

    Private Sub ocmreject_Click(sender As Object, e As EventArgs) Handles ocmreject.Click

        Call ValidateSave()



            If VerifyData() = False Then
            Exit Sub
        End If

        SaveData("1")


    End Sub


    Private Sub SumPoint()


    End Sub
    Private Sub wQCBarcodeFabricInspec_Activated(sender As Object, e As EventArgs) Handles Me.Activated

    End Sub

End Class