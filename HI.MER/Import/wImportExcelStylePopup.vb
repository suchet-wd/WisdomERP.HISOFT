Option Explicit On
Option Strict Off

Imports System
Imports System.Data
Imports System.Collections.Generic
Imports Microsoft.VisualBasic

Public Class wImportExcelStylePopup

#Region "Variable Declaration"
    Private sSQL As String
    Private _DTMapSizeNotExists As System.Data.DataTable
#End Region

#Region "Property"
    Private _DTUserImportMapSize As System.Data.DataTable = Nothing
    Public Property DTUserImportMapSize As System.Data.DataTable
        Get
            Return _DTUserImportMapSize
        End Get
        Set(ByVal value As System.Data.DataTable)
            _DTUserImportMapSize = value
        End Set
    End Property
#End Region

#Region "PROC AND FUNCTION"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

    End Sub


    Private Function StyleImportOrder() As Boolean


        Dim cmdstring As String = ""
        Dim nFNHSysStyleId As Integer = 0


        With CType(Me.ogcstyle.DataSource, DataTable)
            .AcceptChanges()

            For Each R As DataRow In .Rows

                nFNHSysStyleId = Val(HI.TL.RunID.GetRunNoID("TMERMStyle", "FNHSysStyleId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                cmdstring = "  DECLARE @CountData int = 0 "
                cmdstring &= vbCrLf & " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle "
                cmdstring &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                cmdstring &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                cmdstring &= vbCrLf & ",FTStateGameDays='" & HI.UL.ULF.rpQuoted(R!FTStateGameDays.ToString) & "' "
                cmdstring &= vbCrLf & " WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(R!FTStyleCode.ToString) & "' "
                cmdstring &= vbCrLf & " SET @CountData = @@ROWCOUNT  "
                cmdstring &= vbCrLf & " IF @CountData <=0 "
                cmdstring &= vbCrLf & "   BEGIN "
                cmdstring &= vbCrLf & "     INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle(FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId, FTStyleCode, FTStyleNameTH, FTStyleNameEN, FTRemark, FTStateActive, FNHSysCustId, FTStateGameDays)  "
                cmdstring &= vbCrLf & "     SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & "          ," & HI.UL.ULDate.FormatDateDB & " "
                cmdstring &= vbCrLf & "          ," & HI.UL.ULDate.FormatTimeDB & " "
                cmdstring &= vbCrLf & "          ," & nFNHSysStyleId & " "
                cmdstring &= vbCrLf & "          ,'" & HI.UL.ULF.rpQuoted(R!FTStyleCode.ToString) & "' "
                cmdstring &= vbCrLf & "          ,'" & HI.UL.ULF.rpQuoted(R!FTStyleName.ToString) & "' "
                cmdstring &= vbCrLf & "          ,'" & HI.UL.ULF.rpQuoted(R!FTStyleName.ToString) & "' "
                cmdstring &= vbCrLf & "          ,'' "
                cmdstring &= vbCrLf & "          ,'1',0"
                cmdstring &= vbCrLf & "          ,'" & HI.UL.ULF.rpQuoted(R!FTStateGameDays.ToString) & "'"
                cmdstring &= vbCrLf & "    SET @CountData = @@ROWCOUNT  "
                cmdstring &= vbCrLf & "   END "
                cmdstring &= vbCrLf & " SELECT  @CountData"


                If Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_MASTER, "0")) > 0 Then

                End If

            Next

        End With

        Return True

    End Function

#End Region

#Region "Event Handle"

    Private Sub ocmok_Click(sender As Object, e As EventArgs) Handles ocmok.Click
        If StyleImportOrder() = True Then
            'HI.MG.ShowMsg.mProcessComplete(HI.MG.ShowMsg.ProcessType.mSave, Me.Text)
            DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        Else
            DialogResult = System.Windows.Forms.DialogResult.Cancel
        End If
    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub wMapSizeImportOrder_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

#End Region

End Class