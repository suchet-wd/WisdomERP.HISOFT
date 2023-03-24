Option Explicit On
Option Strict Off

Imports System
Imports System.Data
Imports System.Collections.Generic
Imports Microsoft.VisualBasic

Public Class wMapColorAndSizeImportOrderTarget

#Region "Variable Declaration"
    Private sSQL As String
    Private _DTMapSizeNotExists As System.Data.DataTable
#End Region

#Region "Property"

    Private _StateProcess As Boolean = False
    Public Property StateProcess As Boolean
        Get
            Return _StateProcess
        End Get
        Set(value As Boolean)
            _StateProcess = value
        End Set
    End Property

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



    Private Function PROC_GETbShowBrowseData() As Boolean
        Dim bRet As Boolean = False
        Try

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
                    Dim _FNHSysMatMappingId As Integer = Val(HI.TL.RunID.GetRunNoID("TMERMMapColorSize", "FNHSysMapColorSizetId", Conn.DB.DataBaseName.DB_MASTER).ToString())
                    Dim tmpConfirmMapSize As System.Data.DataTable
                    With CType(Me.ogdMapSizeImport.DataSource, System.Data.DataTable)
                        .AcceptChanges()
                        tmpConfirmMapSize = .Copy
                    End With


                    For Each R As DataRow In tmpConfirmMapSize.Rows

                        sSQL = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMapColorSize("
                        sSQL &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FNHSysMapColorSizetId, FTMapColorSizeCode"
                        sSQL &= vbCrLf & ", FNHSysMatColorId, FNHSysMatSizeId)"
                        sSQL &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        sSQL &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                        sSQL &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                        sSQL &= vbCrLf & "," & _FNHSysMatMappingId & ""
                        sSQL &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTColor.ToString) & "'"
                        sSQL &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysMatColorId_Hide.ToString)) & ""
                        sSQL &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysMatSizeId_Hide.ToString)) & ""

                        HI.Conn.SQLConn.ExecuteNonQuery(sSQL, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                        _FNHSysMatMappingId = _FNHSysMatMappingId + 1
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
           
        End Try

        Return bRet

    End Function

#End Region

#Region "Event Handle"

    Private Sub ocmok_Click(sender As Object, e As EventArgs) Handles ocmok.Click

        With CType(Me.ogdMapSizeImport.DataSource, System.Data.DataTable)
            .AcceptChanges()

            If .Select("FNHSysMatSizeId='' OR FNHSysMatColorId=''").Length > 0 Then
                HI.MG.ShowMsg.mInfo("กรุณาทำการระบุข้อมูลให้ครบ !!!", 1505250148, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                Exit Sub
            End If
        End With

        If PROC_GETbMapSizeImportOrder() = True Then
            'HI.MG.ShowMsg.mProcessComplete(HI.MG.ShowMsg.ProcessType.mSave, Me.Text)
            Me.StateProcess = True
            Me.Close()
        Else
            Me.StateProcess = False
        End If
    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.StateProcess = False
        Me.Close()
    End Sub

    Private Sub wMapSizeImportOrder_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call PROC_GETbShowBrowseData()
    End Sub

#End Region

End Class