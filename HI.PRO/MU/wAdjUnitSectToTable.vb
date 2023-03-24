Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid

Public Class wAdjUnitSectToTable
    Private sFTOrderNo As String
    Private sFTOrderProdNo As String

    Dim dtStyleDetail As DataTable
    Private _PartId As Integer = 0
    Public Property PartId As Integer
        Get
            Return _PartId
        End Get
        Set(value As Integer)
            _PartId = value
        End Set
    End Property

    Private _UpdateState As Boolean = False
    Public Property UpdateState As Boolean
        Get
            Return _UpdateState
        End Get
        Set(value As Boolean)
            _UpdateState = value
        End Set
    End Property
    Private _FTOrderProdNo As String = ""
    Public Property FTOrderProdNo As String
        Get
            Return _FTOrderProdNo
        End Get
        Set(value As String)
            _FTOrderProdNo = value
        End Set
    End Property

    Private _ODTRatio As DataTable
    Public Property ODTRatio As DataTable
        Get
            Return _ODTRatio
        End Get
        Set(value As DataTable)
            _ODTRatio = value
        End Set
    End Property
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private _odtConfig As DataTable
    Public Property odtConfig As DataTable
        Get
            Return _odtConfig
        End Get
        Set(value As DataTable)
            _odtConfig = value
        End Set
    End Property
    Private Sub wAddCutItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    End Sub

    Private Sub InitNewRow(ByVal dataTable As System.Data.DataTable) 'add new row in gridview
        Try

        Catch ex As Exception
        End Try
    End Sub

    Private _StateSumGrid As Boolean
    Private Sub SumGrid()
        _StateSumGrid = True
        CType(ogcpart.DataSource, DataTable).AcceptChanges()
        Try
            Dim _Total As Double = 0
            _Total = 0
            With Me.ogvpart

                For I As Integer = 0 To .RowCount - 1
                    _Total = 0
                    For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns

                        Select Case GridCol.FieldName.ToString.ToUpper
                            Case "FTOrderNo".ToUpper, "FTOrderProdNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysPartId".ToUpper, "FTPartCode".ToUpper, "FTPartName".ToUpper, "Total".ToUpper, "FNSeq".ToUpper
                            Case Else
                                If IsNumeric(.GetFocusedRowCellValue(GridCol)) Then
                                    _Total = _Total + CDbl(.GetRowCellValue(I, GridCol))
                                Else
                                    _Total = _Total + 0
                                End If
                        End Select

                    Next

                    .SetRowCellValue(I, "Total", _Total)
                Next

            End With

        Catch ex As Exception
        End Try

        CType(ogcpart.DataSource, DataTable).AcceptChanges()

        _StateSumGrid = False
    End Sub


    Private Function PROC_SAVECutOrder() As Boolean
        Dim _Qry As String = ""
        Dim _QryCheckSeq As String = ""
        Dim bRet As Boolean = False
        Dim Maxleng As String = ""

        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            '_Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord "
            '_Qry &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '_Qry &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            '_Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB

            '_Qry &= vbCrLf & " WHERE  FTOrderNo='" & FTOrderNo.Text & "' AND FTOrderProdNo ='" & sFTOrderProdNo & "' "
            '_Qry &= vbCrLf & "AND FNHSysUnitSectId = " & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString)

            'If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            '    _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord "
            '    _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTOrderProdNo, FNHSysUnitSectId, FNHSysCmpId) "
            '    _Qry &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            '    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            '    _Qry &= vbCrLf & ",'" & FTOrderNo.Text & "'"
            '    '_Qry &= vbCrLf & ",'" & _FTSubOrderNo & "'"
            '    _Qry &= vbCrLf & ",'" & sFTOrderProdNo & "' "
            '    '_Qry &= vbCrLf & ",'" & _FTColorway & "'"
            '    _Qry &= vbCrLf & "," & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString)
            '    _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID)

            '    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            '        HI.Conn.SQLConn.Tran.Rollback()
            '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '        Return False
            '    End If
            'End If

            'Call SaveDetail(FTOrderNo.Text)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function





    Private Sub ocmok_Click(sender As Object, e As EventArgs) Handles ocmok.Click

        Try

            Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")
            If Me.FNHSysUnitSectId.Text = "" Then
                HI.MG.ShowMsg.mInfo("กรุณาเลือกโต๊ะ !!", 202204201832, Me.Text)
                Exit Sub
            End If

            With DirectCast(Me.ogcpart.DataSource, DataTable)
                .AcceptChanges()

                If .Select("FTSelect ='1'").Length <= 0 Then
                    HI.MG.ShowMsg.mInfo("กรุณาเลือกโต๊ะ !!", 202204201832, Me.Text)
                    Exit Sub
                End If

            End With

            _Spls.Close()

            UpdateState = True
            Me.Close()

            'If CreateNewJobProducttion() Then
            '    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

            'Else
            '    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            'End If

        Catch ex As Exception

        End Try

        Me.Close()
    End Sub









    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        UpdateState = False
        Me.Close()
    End Sub

    Private Sub ReposCaleditWeight_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposCaleditWeight.EditValueChanging
        Try
            Dim _NewValue As Double = e.NewValue
            Dim _OrgValue As Double = 0
            Dim _Size As String = ""
            Dim _FTColorway As String = ""
            Dim _Dt As DataTable = ogcpart.DataSource
            Dim _PartId As String
            Dim _Seq As Integer = 0

            If e.NewValue < 0 Then
                e.Cancel = True
            Else
                With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                    _Size = .FocusedColumn.FieldName.ToString()
                    _FTColorway = .GetFocusedRowCellValue("FTColorway")
                    _PartId = .GetFocusedRowCellValue("FNHSysPartId")
                    _Seq = Integer.Parse("0" & .GetFocusedRowCellValue("FNSeq"))

                    If Not (_StateSumGrid) Then
                        Dim _ColName As String = .FocusedColumn.FieldName.ToString
                        With CType(ogcpart.DataSource, DataTable)
                            .AcceptChanges()

                            For Each R As DataRow In .Select("FNSeq=" & _Seq)
                                If (R!FNHSysPartId = _PartId) Then
                                    R.Item(_ColName) = _NewValue
                                    Exit For
                                End If
                            Next

                        End With

                        ' ogvpackdetailWeight.SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, (_NewValue))

                        If Not (ogcpart.DataSource Is Nothing) Then
                            Call SumGrid()
                        End If

                    End If

                End With
            End If
        Catch ex As Exception
            e.Cancel = True
        End Try
    End Sub

    Private Sub ReposPartCode_EditValueChanged(sender As Object, e As EventArgs) Handles ReposPartCode.EditValueChanged
        CType(ogcpart.DataSource, DataTable).AcceptChanges()
        Call InitNewRow(CType(ogcpart.DataSource, DataTable))
    End Sub

    Private Sub ogvpart_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvpart.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                        .DeleteRow(.FocusedRowHandle)
                        If (.RowCount = 0) Then
                            Call InitNewRow(CType(ogcpart.DataSource, DataTable))
                        End If
                        'With CType(ogcfabric.DataSource, DataTable)
                        '    .AcceptChanges()
                        '    Dim x As Integer = 0
                        '    For Each r As DataRow In .Select("FNSeq<>0", "FNSeq")
                        '        x += +1
                        '        r!FNSeq = x
                        '    Next
                        '    .AcceptChanges()
                        'End With
                    End With
                    'SumAmt()
            End Select
        Catch ex As Exception
        End Try
    End Sub



    Private Sub ocmRowDel_Click(sender As Object, e As EventArgs)
        Try
            'If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการลบการบันทึกงานตัด ใช่หรือไม่ !!!!!", 1905061707, Me.Text) = False Then Exit Sub
            'With Me.ogvpart
            '    If .FocusedRowHandle < -1 Or .RowCount < 0 Then Exit Sub
            '    Dim _Cmd As String = ""

            '    Dim _PartIds As Integer = Integer.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNHSysPartId").ToString)
            '    Dim _Seq As Integer = Integer.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNSeq").ToString)

            '    _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord_Detail"
            '    _Cmd &= vbCrLf & "  WHERE FTOrderProdNo = N'" & sFTOrderProdNo & "'"
            '    _Cmd &= vbCrLf & "  AND FDSaveDate = N'" & HI.UL.ULDate.ConvertEnDB(Me.FDSaveDate.Text) & "'"
            '    _Cmd &= vbCrLf & "  AND FNHSysUnitSectId =" & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString) & ""
            '    _Cmd &= vbCrLf & "  AND FNHSysPartId =" & Val(_PartIds) & ""
            '    _Cmd &= vbCrLf & "  AND FNSeq=" & _Seq
            '    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            '    .DeleteRow(.FocusedRowHandle)
            'End With
            'LoadOrderCutItemUnitSect()
        Catch ex As Exception

        End Try
    End Sub

    Private _LayerPerTable As Integer = 0




    Private _StateCheck As Boolean = False


    Private Sub RepositoryItemCheckEditFTSelect_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCheckEditFTSelect.EditValueChanging
        Try
            Try
                With Me.ogvpart
                    If .RowCount < 0 And .FocusedRowHandle < -1 Then Exit Sub
                    If e.NewValue = "1" Then
                        .SetRowCellValue(.FocusedRowHandle, "FNHSysUnitSectId", Me.FNHSysUnitSectId.Properties.Tag)
                        .SetRowCellValue(.FocusedRowHandle, "FTUnitSectCode", Me.FNHSysUnitSectId.Text)

                    Else
                        .SetRowCellValue(.FocusedRowHandle, "FNHSysUnitSectId", 0)
                        .SetRowCellValue(.FocusedRowHandle, "FTUnitSectCode", "")


                    End If

                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Catch ex As Exception

        End Try
    End Sub
End Class