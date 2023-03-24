Option Explicit On
Option Strict Off

Imports System
Imports System.Data
Imports System.Collections.Generic
Imports Microsoft.VisualBasic

Public Class wMapSizeImportOrder

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

    Public Sub New(ByRef paramDTMapSizeNotExists As System.Data.DataTable)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        If Not paramDTMapSizeNotExists Is Nothing Then
            _DTMapSizeNotExists = paramDTMapSizeNotExists.Copy()
        End If

        'Call PROC_GETbInitMapSizeData()

    End Sub

    Private Function PROC_GETbInitGridview() As Boolean
        Dim bRet As Boolean = False
        Try

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
            End If
        End Try

        Return bRet

    End Function

    Private Function PROC_GETbInitRepositoryMapsizeExtend() As Boolean
        Dim bRet As Boolean = False

        Try

            'sSQL = ""
            'sSQL = "SELECT A.FTMapSizeExtension AS FTMapSizeExtend"
            'sSQL &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERMMapSize AS A (NOLOCK)"
            'sSQL &= Environment.NewLine & "ORDER BY A.FTMapSizeExtension;"


            sSQL = ""
            sSQL = "SELECT A.FTMatSizeCode AS FTMapSizeExtend"
            sSQL &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMatSize AS A (NOLOCK)"
            sSQL &= Environment.NewLine & " WHERE FTStateActive='1'"
            sSQL &= Environment.NewLine & "ORDER BY A.FNMatSizeSeq;"

            Dim tmpMapSizeExtend As System.Data.DataTable

            tmpMapSizeExtend = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            Me.RepositoryItemFTMapSizeExtend.Items.Clear()

            For Each oDataRow As System.Data.DataRow In tmpMapSizeExtend.Rows
                Me.RepositoryItemFTMapSizeExtend.Items.Add(oDataRow!FTMapSizeExtend.ToString)
            Next

            tmpMapSizeExtend.Dispose()

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
            End If
        End Try

        Return bRet

    End Function

    Private Function PROC_GETbInitMapSizeData() As Boolean
        Dim bRet As Boolean = False
        Dim DTMapSize As System.Data.DataTable
        Try
            sSQL = ""

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
            End If
        End Try

        Return bRet

    End Function

    Private Function PROC_GETbShowBrowseData() As Boolean
        Dim bRet As Boolean = False
        Try
            Call PROC_GETbInitRepositoryMapsizeExtend()

            'Me.ogdMapSizeImport.DataSource = _DTMapSizeNotExists
            'Me.ogvMapSizeImport.BestFitColumns()
            'Me.ogdMapSizeImport.Refresh()

            Me.ogdMapSizeImport.DataSource = _DTMapSizeNotExists
            ogvMapSizeImport.OptionsView.ColumnAutoWidth = True
            ogvMapSizeImport.BestFitColumns()
            ogdMapSizeImport.Refresh()
            ogvMapSizeImport.RefreshData()

            bRet = True

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
            End If
        End Try

        Return bRet

    End Function

    Private Function PROC_GETbMapSizeImportOrder() As Boolean
        Dim bRet As Boolean = False
        Try
            If Me.ogvMapSizeImport.RowCount > 0 Then
                Me.TopMost = False
                Me.Refresh()

                If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave, Me.Text, "") = True Then
                    '...ถ้ารายการของแถวใน gridview คอลัมน์ map size code มีการเลือกข้อมูล Map Size Extend จริง ให้ทำการ เก็บรายการ size code ที่ยังไม่มีอยู่ในระบบ และ รหัสรายการ ไซส์ โค้ด ที่ต้องการ Map Size 
                    '...รายการที่ Map ส่งค่ากลับ / ส่วนรายการที่ ไม่มีการ Map ให้ทำการ Add new record to system master file TMERMMatSize

                    Dim tmpConfirmMapSize As System.Data.DataTable
                    With CType(Me.ogdMapSizeImport.DataSource, System.Data.DataTable)
                        .AcceptChanges()
                        tmpConfirmMapSize = .Copy
                    End With

                    Me._DTUserImportMapSize = tmpConfirmMapSize.Clone()

                    For Each oDataRow As System.Data.DataRow In tmpConfirmMapSize.Rows
                        '...FTSizeCodeNotExists, FTMapSizeExtend
                        If oDataRow!FTMapSizeExtend.ToString <> "" Then
                            'Me._DTMapSizeNotExists.ImportRow(oDataRow)

                            sSQL = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERMMapSize(FTInsUser, FDInsDate, FTInsTime,FTMapSize, FTMapSizeExtension)"
                            sSQL &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            sSQL &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            sSQL &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                            sSQL &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(oDataRow!FTSizeCodeNotExists.ToString) & "'"
                            sSQL &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(oDataRow!FTMapSizeExtend.ToString) & "'"

                            HI.Conn.SQLConn.ExecuteNonQuery(sSQL, HI.Conn.DB.DataBaseName.DB_MERCHAN)
                            Me.DTUserImportMapSize.ImportRow(oDataRow)
                        Else
                            If oDataRow!FTSizeCodeNotExists.ToString <> "" Then
                                '...add new FNHSysMatSizeId:FTMatSizeCode in System TMERMMatSize [HITECH_MASTER]
                                Dim nFNHSysMatSizeId As Integer

                                nFNHSysMatSizeId = Val(HI.TL.RunID.GetRunNoID("TMERMMatSize", "FNHSysMatSizeId", HI.Conn.DB.DataBaseName.DB_MASTER).ToString())

                                sSQL = ""
                                sSQL = "DECLARE @FNMatSizeSeqMax AS numeric(18,2);"
                                sSQL &= Environment.NewLine & "SELECT @FNMatSizeSeqMax = MAX(A.FNMatSizeSeq)"
                                sSQL &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS A WITH(NOLOCK)"
                                sSQL &= Environment.NewLine & "GROUP BY A.FNHSysMatSizeId;"
                                'sSQL &= Environment.NewLine & "--PRINT 'FNMatSizeSeqMax Current : ' + CONVERT(VARCHAR(10),ISNULL(@FNMatSizeSeqMax, 1));"
                                'sSQL &= Environment.NewLine & "--PRINT 'FNMatSizeSeq Max Next : ' + CONVERT(VARCHAR(10),(ISNULL(@FNMatSizeSeqMax, 1) + 1));"
                                sSQL &= Environment.NewLine & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] ([FTInsUser],[FDInsDate],[FTInsTime]"
                                sSQL &= Environment.NewLine & "                                                                                                    ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                                sSQL &= Environment.NewLine & "                                                                                                    ,[FNHSysMatSizeId],[FTMatSizeCode],[FNMatSizeSeq]"
                                sSQL &= Environment.NewLine & "							                                                                           ,[FTMatSizeNameTH],[FTMatSizeNameEN],[FTRemark],[FTStateActive])"
                                sSQL &= Environment.NewLine & "VALUES(NULL, NULL, NULL"
                                sSQL &= ",NULL, NULL, NULL"
                                sSQL &= ", " & nFNHSysMatSizeId & ", N'" & HI.UL.ULF.rpQuoted(oDataRow!FTSizeCodeNotExists.ToString) & "', (ISNULL(@FNMatSizeSeqMax, 0) + 1)"
                                sSQL &= ", N'" & HI.UL.ULF.rpQuoted(oDataRow!FTSizeCodeNotExists.ToString) & "', N'" & HI.UL.ULF.rpQuoted(oDataRow!FTSizeCodeNotExists.ToString) & "', '', '1');"

                                If HI.Conn.SQLConn.ExecuteNonQuery(sSQL, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then
                                    System.Diagnostics.Debug.Write("Append new size code to system data base TMERMMatSize [HITECH_MASTER] " & oDataRow!FTSizeCodeNotExists.ToString & " " & Environment.NewLine)
                                End If

                            End If

                        End If

                    Next

                    If Not tmpConfirmMapSize Is Nothing Then tmpConfirmMapSize.Dispose()

                    bRet = True

                Else
                    '...Nothing
                End If

                Me.TopMost = True
                Me.Refresh()

            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
            End If
        End Try

        Return bRet

    End Function

#End Region

#Region "Event Handle"

    Private Sub ocmok_Click(sender As Object, e As EventArgs) Handles ocmok.Click
        If PROC_GETbMapSizeImportOrder() = True Then
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
        Call PROC_GETbShowBrowseData()
    End Sub

#End Region

End Class