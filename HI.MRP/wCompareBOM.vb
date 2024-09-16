Imports System.Data.SqlClient

Public Class wCompareBOM

    Private _FNHSysStyleDevId As String = ""
    Private _ProcComplete As Boolean = False
    Public Property ProcComplete As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(ByVal value As Boolean)
            _ProcComplete = value
        End Set
    End Property

    Public Property FNHSysStyleDevId As String
        Get
            Return _FNHSysStyleDevId
        End Get
        Set(value As String)
            _FNHSysStyleDevId = value
        End Set
    End Property


#Region "Form Load"
    Private Sub wCompareBOM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub
#End Region

    Private Sub ocmok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmok.Click
        Me.ProcComplete = False
        Me.Close()
    End Sub

    Private Sub ocmcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmcancel.Click
        Me.ProcComplete = False
        Me.Close()
    End Sub

    Public Sub LoadData()
        Dim _Qry As String = ""
        '_Qry = "SELECT FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysStyleDevId "
        _Qry = "FNHSysStyleDevId, FNHSysStyleDevIdOriginal, FNSeq, FTCompareType, FTCompareState "
        _Qry &= vbCrLf & ", FTCompareCode, FTCompareDesc, FTRemark "
        _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_Compare AS b WITH(NOLOCK) "
        _Qry &= vbCrLf & "WHERE  (FNHSysStyleDevId  =" & Val(_FNHSysStyleDevId) & ")"
        'ogcCompare.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
    End Sub

End Class