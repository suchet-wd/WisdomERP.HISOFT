Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls

Public Class wBOMListingListOrderNo

    Public BOMSysID As Integer = 0
    Public StyleID As Integer = 0
    Public SeasonID As Integer = 0
    Public StateChange As Boolean = False

    Private Sub wBOMListingListOrderNo_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub RepositoryItemCheckEdit4_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCheckEdit4.EditValueChanging
        Try
            Dim State As String = ""

            Dim OrderNo As String = Me.ogvorder.GetFocusedRowCellValue("FTOrderNo").ToString
            Dim cmdstring As String = ""

            If e.NewValue.ToString = "1" Then

                cmdstring = "  declare @Rec int =0     "
                cmdstring &= vbCrLf & "    insert into   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SpecialOrder ( FTInsUser, FDInsDate, FTInsTime, FNHSysBomId, FTOrderNo) "
                cmdstring &= vbCrLf & "  select "
                cmdstring &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                cmdstring &= vbCrLf & ", " & HI.UL.ULDate.FormatDateDB & ""
                cmdstring &= vbCrLf & ", " & HI.UL.ULDate.FormatTimeDB & ""
                cmdstring &= vbCrLf & ", " & BOMSysID & ""
                cmdstring &= vbCrLf & " , '" & HI.UL.ULF.rpQuoted(OrderNo) & "'"
                cmdstring &= vbCrLf & "   set @Rec =  @@ROWCOUNT  "
                cmdstring &= vbCrLf & " SELECT @Rec AS FNState"

            Else


                cmdstring = " declare @Order nvarchar(30) =''   "
                cmdstring &= vbCrLf & "  declare @Rec int =0   "
                cmdstring &= vbCrLf & " set @Order = ISNULL((select top 1  FTOrderNo  from  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Order WITH(NOLOCK)  where FNHSysBomId =" & BOMSysID & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'),'') "
                cmdstring &= vbCrLf & " IF  @Order ='' "
                cmdstring &= vbCrLf & " BEGIN "
                cmdstring &= vbCrLf & "     delete from   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SpecialOrder   where FNHSysBomId =" & BOMSysID & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                cmdstring &= vbCrLf & "     set @Rec =  @@ROWCOUNT  "
                cmdstring &= vbCrLf & " END "
                cmdstring &= vbCrLf & " SELECT @Rec AS FNState"


            End If

            If Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN, "")) = 0 Then

                e.Cancel = True

            Else
                StateChange = True

                e.Cancel = False
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvorder_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvorder.KeyDown
        Try


            If (ogdorder.DataSource Is Nothing) Then

                Exit Sub
            End If


            With Me.ogvorder
                If .RowCount <= 0 Then Exit Sub
                Select Case e.KeyCode
                    Case Keys.F1, Keys.F2

                        Dim State As String = "0"

                        Select Case e.KeyCode
                            Case Keys.F1
                                State = "1"
                            Case Keys.F2
                                State = "0"
                        End Select
                        Dim OrderNo As String = ""
                        Dim cmdstring As String = ""
                        For I As Integer = 0 To .RowCount - 1

                            If .GetRowCellValue(I, "FTSelect").ToString <> State Then

                                .FocusedRowHandle = I

                                OrderNo = .GetRowCellValue(I, "FTOrderNo").ToString



                                If State = "1" Then

                                    cmdstring = "  declare @Rec int =0     "
                                    cmdstring &= vbCrLf & "    insert into   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SpecialOrder ( FTInsUser, FDInsDate, FTInsTime, FNHSysBomId, FTOrderNo) "
                                    cmdstring &= vbCrLf & "  select "
                                    cmdstring &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                    cmdstring &= vbCrLf & ", " & HI.UL.ULDate.FormatDateDB & ""
                                    cmdstring &= vbCrLf & ", " & HI.UL.ULDate.FormatTimeDB & ""
                                    cmdstring &= vbCrLf & ", " & BOMSysID & ""
                                    cmdstring &= vbCrLf & " , '" & HI.UL.ULF.rpQuoted(OrderNo) & "'"
                                    cmdstring &= vbCrLf & "   set @Rec =  @@ROWCOUNT  "
                                    cmdstring &= vbCrLf & " SELECT @Rec AS FNState"

                                Else


                                    cmdstring = " declare @Order nvarchar(30) =''   "
                                    cmdstring &= vbCrLf & "  declare @Rec int =0   "
                                    cmdstring &= vbCrLf & " set @Order = ISNULL((select top 1  FTOrderNo  from  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Order WITH(NOLOCK)  where FNHSysBomId =" & BOMSysID & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'),'') "
                                    cmdstring &= vbCrLf & " IF  @Order ='' "
                                    cmdstring &= vbCrLf & " BEGIN "
                                    cmdstring &= vbCrLf & "     delete from   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SpecialOrder   where FNHSysBomId =" & BOMSysID & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                                    cmdstring &= vbCrLf & "     set @Rec =  @@ROWCOUNT  "
                                    cmdstring &= vbCrLf & " END "
                                    cmdstring &= vbCrLf & " SELECT @Rec AS FNState"


                                End If



                                If Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN, "0")) > 0 Then
                                    StateChange = True
                                    .SetRowCellValue(I, "FTSelect", State)

                                End If

                            End If

                        Next

                End Select
            End With
        Catch ex As Exception
        End Try
    End Sub
End Class