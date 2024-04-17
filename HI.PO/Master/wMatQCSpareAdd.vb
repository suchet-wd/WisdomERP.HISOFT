Imports System.Windows.Forms

Public Class wMatQCSpareAdd

    Public StateSave As Boolean = False

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub WConfigFinExpendPopup_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            Me.ogvDetail.OptionsView.ShowAutoFilterRow = False
            Me.ogvDetail.ClearColumnsFilter()
            Me.ogvDetail.ClearSorting()


            For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In ogvDetail.Columns
                GridCol.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
            Next


        Catch ex As Exception
        End Try

    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        StateSave = False
        Me.Close()
    End Sub



    Private Function VerifyData() As Boolean
        If FNHSysMatTypeId.Text.Trim = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysMatTypeId_lbl.Text)
            Return False
        End If

        With CType(Me.ogcDetail.DataSource, DataTable)
            .AcceptChanges()

            If .Select("FNEndQty>0").Length <= 0 Then
                Return False
            End If
        End With
        Return True
    End Function

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If VerifyData() Then

            Dim dt As New DataTable
            With CType(Me.ogcDetail.DataSource, DataTable)
                .AcceptChanges()

                dt = .Copy

            End With

            Dim cmdstring As String = ""

            cmdstring = " DECLARE @CountData int = 0 "
            cmdstring &= vbCrLf & " DELETE FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & ".dbo.TMATQCSpare_Detail   WHERE FNHSysMatTypeId= " & Val(FNHSysMatTypeId.Properties.Tag.ToString) & " "
            cmdstring &= vbCrLf & " UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & ".dbo.TMATQCSpare  SET "
            cmdstring &= vbCrLf & "                   FTUpdUser= '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            cmdstring &= vbCrLf & "                   ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
            cmdstring &= vbCrLf & "                   ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
            cmdstring &= vbCrLf & "                   ,FNSpareConditionType= " & FNSpareConditionType.SelectedIndex & " "
            cmdstring &= vbCrLf & "                   ,FNSpareType= " & FNSpareType.SelectedIndex & " "
            cmdstring &= vbCrLf & "                   ,FTStateActive= '" & FTStateActive.EditValue.ToString() & "' "
            cmdstring &= vbCrLf & "                   ,FTRemark= '" & HI.UL.ULF.rpQuoted(FTRemark.Text.Trim) & "' "
            cmdstring &= vbCrLf & "     WHERE FNHSysMatTypeId= " & Val(FNHSysMatTypeId.Properties.Tag.ToString) & " "
            cmdstring &= vbCrLf & "  SET @CountData  = @@ROWCOUNT  "
            cmdstring &= vbCrLf & "  IF  @CountData <= 0  "
            cmdstring &= vbCrLf & "       BEGIN  "
            cmdstring &= vbCrLf & "  insert into  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & ".dbo.TMATQCSpare  ("
            cmdstring &= vbCrLf & "   FTInsUser, FDInsDate, FTInsTime,FNHSysMatTypeId, FNSpareConditionType, FNSpareType, FTRemark, FTStateActive"
            cmdstring &= vbCrLf & "  )"
            cmdstring &= vbCrLf & "    select  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            cmdstring &= vbCrLf & "                   ," & HI.UL.ULDate.FormatDateDB & " "
            cmdstring &= vbCrLf & "                   ," & HI.UL.ULDate.FormatTimeDB & " "
            cmdstring &= vbCrLf & "               ," & Val(FNHSysMatTypeId.Properties.Tag.ToString) & " "
            cmdstring &= vbCrLf & "                   , " & FNSpareConditionType.SelectedIndex & " "
            cmdstring &= vbCrLf & "                   ," & FNSpareType.SelectedIndex & " "
            cmdstring &= vbCrLf & "                   ,'" & HI.UL.ULF.rpQuoted(FTRemark.Text.Trim) & "' "
            cmdstring &= vbCrLf & "                   ,'" & FTStateActive.EditValue.ToString() & "' "
            cmdstring &= vbCrLf & "  SET @CountData  = @@ROWCOUNT  "
            cmdstring &= vbCrLf & "      END  "
            cmdstring &= vbCrLf & "  IF  @CountData > 0  "
            cmdstring &= vbCrLf & "       BEGIN  "

            For Each R As DataRow In dt.Select("FNSeq>0", "FNSeq")
                cmdstring &= vbCrLf & "  insert into   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & ".dbo.TMATQCSpare_Detail  ("
                cmdstring &= vbCrLf & "   FTInsUser, FDInsDate, FTInsTime,FNHSysMatTypeId, FNSeq, FNStartQty, FNEndQty, FNSpare"
                cmdstring &= vbCrLf & "  )"


                cmdstring &= vbCrLf & "    select  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & "                   ," & HI.UL.ULDate.FormatDateDB & " "
                cmdstring &= vbCrLf & "                   ," & HI.UL.ULDate.FormatTimeDB & " "
                cmdstring &= vbCrLf & "               ," & Val(FNHSysMatTypeId.Properties.Tag.ToString) & " "
                cmdstring &= vbCrLf & "                   , " & Val(R!FNSeq.ToString) & " "
                cmdstring &= vbCrLf & "                   ," & Val(R!FNStartQty.ToString) & " "
                cmdstring &= vbCrLf & "                   ,'" & Val(R!FNEndQty.ToString) & "' "
                cmdstring &= vbCrLf & "                   ,'" & Val(R!FNSpare.ToString) & "' "

            Next

            cmdstring &= vbCrLf & "      END  "


            If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR) Then

                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                StateSave = True
                Me.Close()


            End If

        End If
    End Sub



    Private Function SaveData() As Boolean

        Dim _Qry As String
        Dim _ChkActive As Integer
        _ChkActive = 0

        Try


            If (HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception

            Return False

        End Try
    End Function

    Private Sub FNHSysMerMatId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysMatTypeId.EditValueChanged

    End Sub


    Public Sub LoadDataSpare(MatId As Integer)

        Dim cmdstrig As String = ""
        cmdstrig = "select FNSeq,FNStartQty,FNEndQty,FNSpare "
        cmdstrig &= vbCrLf & "  FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & ".dbo.TMATQCSpare_Detail AS X WITH(NOLOCK) "
        cmdstrig &= vbCrLf & "  WHERE FNHSysMatTypeId=" & Val(MatId) & ""
        cmdstrig &= vbCrLf & "  ORDER BY FNSeq "


        ogcDetail.DataSource = HI.Conn.SQLConn.GetDataTable(cmdstrig, Conn.DB.DataBaseName.DB_PUR)


    End Sub

    Private Sub ogcdetailmat_EmbeddedNavigator_ButtonClick(sender As Object, e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles ogcDetail.EmbeddedNavigator.ButtonClick
        Select Case e.Button.ButtonType
            Case DevExpress.XtraEditors.NavigatorButtonType.Append

                AddNewRow()
            Case DevExpress.XtraEditors.NavigatorButtonType.Remove
                DeleteRow()
        End Select

        e.Handled = True
    End Sub

    Private Sub AddNewRow()

        Try
            ogvDetail.FocusedColumn = ogvDetail.Columns.ColumnByFieldName("FNSeq")
            With CType(Me.ogcDetail.DataSource, DataTable)

                .AcceptChanges()


                If .Select("FNEndQty<=0").Length > 0 Then
                    Exit Sub
                End If



                Dim MaxSeq As Decimal = 0
                For Each RxMax As DataRow In .Select("FNSeq>0", "FNSeq DESC")
                    MaxSeq = Val(RxMax!FNSeq.ToString) + 1.0
                    Exit For
                Next

                If MaxSeq = 0 Then
                    MaxSeq = 1
                End If

                Dim dr As DataRow = .NewRow()

                For Each c As DataColumn In .Columns

                    Select Case c.ColumnName
                        Case "FNSeq"
                            dr.Item(c) = MaxSeq


                        Case Else
                            Try


                                Select Case c.DataType.ToString
                                    Case "System.String"
                                        dr.Item(c) = ""
                                    Case Else
                                        dr.Item(c) = 0
                                End Select


                            Catch ex As Exception
                            End Try
                    End Select

                Next

                .Rows.Add(dr)
                .AcceptChanges()

            End With

            ogvDetail.LeftCoord = 0
            ogvDetail.ClearSelection()
            ogvDetail.FocusedRowHandle = ogvDetail.RowCount - 1
            ogvDetail.SelectRow(ogvDetail.FocusedRowHandle)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub DeleteRow()
        ogvDetail.DeleteSelectedRows()

        With CType(Me.ogcDetail.DataSource, DataTable)
            Dim I As Integer = 1
            For Each RxMax As DataRow In .Select("FNSeq>0", "FNSeq")
                RxMax!FNSeq = I
                I = I + 1

            Next
        End With
    End Sub
    Private Sub ogvdetailmat_KeyDown(sender As Object, e As KeyEventArgs)

        Try

            Select Case e.KeyCode
                Case Keys.Down
                    AddNewRow()
                Case Keys.Delete
                    DeleteRow()
            End Select

        Catch ex As Exception
        End Try

    End Sub

End Class