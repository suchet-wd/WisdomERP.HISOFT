Imports DevExpress.XtraEditors.Controls

Public Class wSMPManageOrderBomItem

    Public Enum RcvType As Integer
        RcvNormal = 0
        RcvRepire = 1
        RcvFree = 2
    End Enum

    Private _PurchaseNo As String = ""
    Public Property PurchaseNo() As String
        Get
            Return _PurchaseNo
        End Get
        Set(value As String)
            _PurchaseNo = value
        End Set
    End Property

    Private _ReceiveNo As String = ""
    Public Property ReceiveNo() As String
        Get
            Return _ReceiveNo
        End Get
        Set(value As String)
            _ReceiveNo = value
        End Set
    End Property

    Private _ReceiveType As RcvType = RcvType.RcvNormal
    Public Property ReceiveType() As RcvType
        Get
            Return _ReceiveType
        End Get
        Set(value As RcvType)
            _ReceiveType = value
        End Set
    End Property


    Private _ItemColumn As String = ""
    Public Property ItemColumn() As String
        Get
            Return _ItemColumn
        End Get
        Set(value As String)
            _ItemColumn = value
        End Set
    End Property


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


        Dim cmd As String = " SELECT * FROM (SELECT  ' ' AS FTRawMatColorCode, ' '  AS FTRawMatColorName UNION   SELECT  C.FTRawMatColorCode, C.FTRawMatColorNameEN AS FTRawMatColorName FROM         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH(NOLOCK)  WHERE C.FTStateActive='1' ) AS A ORDER BY A.FTRawMatColorCode"
        Dim dtcolor As New DataTable
        dtcolor = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_MASTER)

        Me.RepositoryItemGridLookUpEdit.DataSource = dtcolor.Copy

    End Sub


    Public Sub SetNewColumn(dt As DataTable)
        Try
            Dim StrCol As String = dt.Rows(0)!FTColumn.ToString

            ItemColumn = StrCol
            With Me.ogvrcv
                .BeginInit()



                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    Select Case Microsoft.VisualBasic.Left(.Columns(I).Name.ToString, 3).ToUpper
                        Case "FIX".ToUpper

                        Case Else

                            Dim FName As String = .Columns(I).FieldName

                            .Columns.Remove(.Columns(I))
                    End Select

                Next


                If StrCol <> "" Then

                    For Each R As String In StrCol.Split(",")


                        Dim FieldName As String = R
                        Dim FieldNameCTH As String = R & "_EN"
                        Dim FieldNameCEN As String = R & "_QTY"
                        Dim FieldNameCh As String = R & "_CH"

                        Dim CaptionName As String = R.Replace("_", "-")

                        Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                        With ColG


                            .FieldName = FieldNameCh
                            .Name = "CH" & FieldNameCh
                            .Caption = " "
                            .Visible = True
                            .ColumnEdit = ResFTStateSelect

                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                            .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                            .AppearanceCell.BackColor = System.Drawing.Color.LightCyan

                            With .OptionsColumn
                                .AllowMove = False
                                .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                .AllowSort = DevExpress.Utils.DefaultBoolean.False

                                .AllowEdit = True
                                .ReadOnly = False
                            End With


                            .Width = 40

                        End With

                        .Columns.Add(ColG)


                        Dim ColG2 As New DevExpress.XtraGrid.Columns.GridColumn
                        With ColG2


                            .FieldName = FieldName
                            .Name = "CX" & FieldName
                            .Caption = CaptionName
                            .Visible = False
                            .ColumnEdit = RepositoryItemGridLookUpEdit

                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                            .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                            .AppearanceCell.BackColor = System.Drawing.Color.LightCyan

                            With .OptionsColumn
                                .AllowMove = False
                                .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                .AllowSort = DevExpress.Utils.DefaultBoolean.False

                                .AllowEdit = True
                                .ReadOnly = False
                            End With



                            .Width = 120


                        End With

                        .Columns.Add(ColG2)

                        Dim ColG3 As New DevExpress.XtraGrid.Columns.GridColumn
                        With ColG3


                            .FieldName = FieldNameCTH
                            .Name = "CX" & FieldNameCTH
                            .Caption = CaptionName ' "Color Name"
                            .Visible = True
                            .ColumnEdit = RepositoryItemTextColorname

                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                            .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                            .AppearanceCell.BackColor = System.Drawing.Color.LightCyan


                            With .OptionsColumn
                                .AllowMove = False
                                .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                .AllowSort = DevExpress.Utils.DefaultBoolean.False

                                .AllowEdit = True
                                .ReadOnly = False
                            End With

                            .Width = 150


                        End With

                        .Columns.Add(ColG3)


                        Dim ColG4 As New DevExpress.XtraGrid.Columns.GridColumn
                        With ColG4


                            .FieldName = FieldNameCEN
                            .Name = "CX" & FieldNameCEN
                            .Caption = "Used"
                            .Visible = True
                            .ColumnEdit = RepositoryItemUsed

                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                            .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                            .AppearanceCell.BackColor = System.Drawing.Color.LightCyan
                            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric


                            .DisplayFormat.FormatString = "{0:n4}"


                            With .OptionsColumn
                                .AllowMove = False
                                .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                .AllowSort = DevExpress.Utils.DefaultBoolean.False

                                .AllowEdit = True
                                .ReadOnly = False
                            End With

                            .Width = 80

                        End With

                        .Columns.Add(ColG4)

                    Next

                End If

                .EndInit()
            End With

        Catch ex As Exception
        End Try

    End Sub

    Private _ProcessProc As Boolean = False
    Public Property ProcessProc As Boolean
        Get
            Return _ProcessProc
        End Get
        Set(value As Boolean)
            _ProcessProc = value
        End Set
    End Property

    Private Sub ocmreceive_Click(sender As System.Object, e As System.EventArgs) Handles ocmreceive.Click

        With CType(ogcrcv.DataSource, DataTable)
            .AcceptChanges()

            If .Select("FTSelect='1'").Length > 0 Then
                Me.ProcessProc = True
                Me.Close()

            Else
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกข้อมูลรายการ !!!", 2010224517, Me.Text,, System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        End With

    End Sub

    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmcancel.Click
        Me.ProcessProc = False
        Me.Close()
    End Sub

    Private Sub FTStaReceiveAll_CheckedChanged(sender As Object, e As EventArgs) Handles FTStaReceiveAll.CheckedChanged
        Try
            Dim State As String = "0"

            If FTStaReceiveAll.Checked Then
                State = "1"
            End If


            With ogcrcv
                If Not (.DataSource Is Nothing) And ogvrcv.RowCount > 0 Then

                    With ogvrcv
                        For I As Integer = 0 To .RowCount - 1


                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), State)
                            Dim ColorCode As String = .GetRowCellValue(I, .Columns.ColumnByFieldName("FTRawMatColorCode")).ToString
                            Dim ColorName As String = .GetRowCellValue(I, .Columns.ColumnByFieldName("FTRawMatColorName")).ToString

                            For Each R As String In ItemColumn.Split(",")


                                Dim FieldName As String = R
                                Dim FieldNameCTH As String = R & "_EN"
                                Dim FieldNameCEN As String = R & "_QTY"
                                Dim FieldNameCh As String = R & "_CH"



                                .SetRowCellValue(I, .Columns.ColumnByFieldName(FieldNameCh), State)

                                If State = "1" Then


                                    .SetRowCellValue(I, .Columns.ColumnByFieldName(FieldName), ColorCode)
                                    .SetRowCellValue(I, .Columns.ColumnByFieldName(FieldNameCTH), ColorName)
                                    .SetRowCellValue(I, .Columns.ColumnByFieldName(FieldNameCEN), 0)

                                Else

                                    .SetRowCellValue(I, .Columns.ColumnByFieldName(FieldName), "")
                                    .SetRowCellValue(I, .Columns.ColumnByFieldName(FieldNameCTH), "")
                                    .SetRowCellValue(I, .Columns.ColumnByFieldName(FieldNameCEN), 0)

                                End If

                            Next


                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With


        Catch ex As Exception

        End Try
    End Sub

    Private Sub ResFNRcvQty_EditValueChanging(sender As Object, e As ChangingEventArgs)

    End Sub

    Private Sub RepositoryItemGridLookUpEdit_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemGridLookUpEdit.EditValueChanged
        Try

            With ogvrcv
                Dim FieldName As String = .FocusedColumn.FieldName.ToString.Replace("_CH", "")
                Dim FieldNameCTH As String = FieldName & "_EN"
                Dim FieldNameCEN As String = FieldName & "_QTY"
                Dim FieldNameCh As String = FieldName & "_CH"


                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)

                .SetFocusedRowCellValue(FieldName, obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorCode").ToString())
                .SetFocusedRowCellValue(FieldNameCTH, obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorName").ToString())



            End With


        Catch ex As Exception

        End Try
    End Sub

    Private Sub ResFTStateSelect_EditValueChanged(sender As Object, e As EventArgs) Handles ResFTStateSelect.EditValueChanged

    End Sub

    Private Sub ResFTStateSelect_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles ResFTStateSelect.EditValueChanging
        Try

            Dim State As String = "0"

            If e.NewValue.ToString = "1" Then
                State = "1"

            Else
                State = "0"

            End If

            Select Case ogvrcv.FocusedColumn.FieldName.ToString
                Case "FTSelect"


                    Dim ColorCode As String = ogvrcv.GetRowCellValue(ogvrcv.FocusedRowHandle, ogvrcv.Columns.ColumnByFieldName("FTRawMatColorCode")).ToString
                    Dim ColorName As String = ogvrcv.GetRowCellValue(ogvrcv.FocusedRowHandle, ogvrcv.Columns.ColumnByFieldName("FTRawMatColorName")).ToString

                    For Each R As String In ItemColumn.Split(",")


                        Dim FieldName As String = R
                        Dim FieldNameCTH As String = R & "_EN"
                        Dim FieldNameCEN As String = R & "_QTY"
                        Dim FieldNameCh As String = R & "_CH"


                        ogvrcv.SetRowCellValue(ogvrcv.FocusedRowHandle, ogvrcv.Columns.ColumnByFieldName(FieldNameCh), State)

                        If State = "1" Then


                                ogvrcv.SetRowCellValue(ogvrcv.FocusedRowHandle, ogvrcv.Columns.ColumnByFieldName(FieldName), ColorCode)
                                ogvrcv.SetRowCellValue(ogvrcv.FocusedRowHandle, ogvrcv.Columns.ColumnByFieldName(FieldNameCTH), ColorName)
                                ogvrcv.SetRowCellValue(ogvrcv.FocusedRowHandle, ogvrcv.Columns.ColumnByFieldName(FieldNameCEN), 0)

                            Else

                                ogvrcv.SetRowCellValue(ogvrcv.FocusedRowHandle, ogvrcv.Columns.ColumnByFieldName(FieldName), "")
                                ogvrcv.SetRowCellValue(ogvrcv.FocusedRowHandle, ogvrcv.Columns.ColumnByFieldName(FieldNameCTH), "")
                                ogvrcv.SetRowCellValue(ogvrcv.FocusedRowHandle, ogvrcv.Columns.ColumnByFieldName(FieldNameCEN), 0)

                            End If

                        Next
                        e.Cancel = False



                Case Else

                    Dim ColorCode As String = ogvrcv.GetRowCellValue(ogvrcv.FocusedRowHandle, ogvrcv.Columns.ColumnByFieldName("FTRawMatColorCode")).ToString
                    Dim ColorName As String = ogvrcv.GetRowCellValue(ogvrcv.FocusedRowHandle, ogvrcv.Columns.ColumnByFieldName("FTRawMatColorName")).ToString


                    Dim FieldName As String = ogvrcv.FocusedColumn.FieldName.ToString.Replace("_CH", "")
                    Dim FieldNameCTH As String = FieldName & "_EN"
                    Dim FieldNameCEN As String = FieldName & "_QTY"
                    Dim FieldNameCh As String = FieldName & "_CH"

                    ogvrcv.SetRowCellValue(ogvrcv.FocusedRowHandle, ogvrcv.Columns.ColumnByFieldName(FieldNameCh), State)

                    If State = "1" Then


                        ogvrcv.SetRowCellValue(ogvrcv.FocusedRowHandle, ogvrcv.Columns.ColumnByFieldName(FieldName), ColorCode)
                        ogvrcv.SetRowCellValue(ogvrcv.FocusedRowHandle, ogvrcv.Columns.ColumnByFieldName(FieldNameCTH), ColorName)
                        ogvrcv.SetRowCellValue(ogvrcv.FocusedRowHandle, ogvrcv.Columns.ColumnByFieldName(FieldNameCEN), 0)

                    Else

                        ogvrcv.SetRowCellValue(ogvrcv.FocusedRowHandle, ogvrcv.Columns.ColumnByFieldName(FieldName), "")
                        ogvrcv.SetRowCellValue(ogvrcv.FocusedRowHandle, ogvrcv.Columns.ColumnByFieldName(FieldNameCTH), "")
                        ogvrcv.SetRowCellValue(ogvrcv.FocusedRowHandle, ogvrcv.Columns.ColumnByFieldName(FieldNameCEN), 0)

                    End If

                    e.Cancel = False

            End Select

        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepositoryItemTextColorname_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemTextColorname.EditValueChanged

    End Sub
End Class