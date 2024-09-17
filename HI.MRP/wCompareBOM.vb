Imports System.Data.SqlClient

Public Class wCompareBOM

    Private _FNHSysStyleDevId As String = ""
    'Private _ProcComplete As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()

    'Public Property ProcComplete As Boolean
    '    Get
    '        Return _ProcComplete
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _ProcComplete = value
    '    End Set
    'End Property

    Public Property FNHSysStyleDevId As String
        Get
            Return _FNHSysStyleDevId
        End Get
        Set(value As String)
            _FNHSysStyleDevId = value
        End Set
    End Property

    Sub New()
        InitializeComponent()
    End Sub

#Region "Form Load"
    Private Sub wCompareBOM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'HI.TL.HandlerControl.ClearControl(Me)
        LoadData()
    End Sub
#End Region

    Public Sub LoadData()
        Dim _Qry As String = ""
        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[Get_BOM_Dev_Compare_Tracking] "
        _Qry &= vbCrLf & "@BomId = '" & Val(_FNHSysStyleDevId) & "'"
        gcCompare.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
    End Sub

    Private Sub ocmclose_Click(sender As Object, e As EventArgs) Handles ocmclose.Click
        Me.Close()
    End Sub
End Class

'_Qry = "SELECT FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysStyleDevId "
'_Qry = "FNHSysStyleDevId, FNHSysStyleDevIdOriginal, FNSeq, FTCompareType, FTCompareState "
'_Qry &= vbCrLf & ", FTCompareCode, FTCompareDesc, FTRemark "
'_Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_Compare AS b WITH(NOLOCK) "
'_Qry &= vbCrLf & "WHERE  (FNHSysStyleDevId  =" & Val(_FNHSysStyleDevId) & ")"