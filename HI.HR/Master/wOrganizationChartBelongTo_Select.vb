Public Class wOrganizationChartBelongTo_Select
    'Private _wOrganizationChartBelongToPopup As wOrganizationChartBelongToPopup

#Region " Procedure "

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        '_wOrganizationChartBelongToPopup = New wOrganizationChartBelongToPopup

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub wOrganizationChartBelongTo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call LoadCon()
    End Sub


    Private Sub LoadCon()
        Dim _Qry As String = ""
        Dim _Dt As DataTable

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry = " SELECT 
                         FNHSysOrgId
                        , 'HIGROUP' AS FNHGroup
                        , FTCLevelCode, FTCLevelNameTH AS FTCLevelName
                        , FTCountryCode, FTCountryNameTH AS FTCountryName
                        ,  FTCmpCode, FTCmpNameTH AS FTCmpName

                        , FTDivisonCode, FTDivisonNameTH AS FTDivisonName
                        , FTDeptCode, FTDeptDescTH AS FTDeptDesc
                        , FTSectCode, FTSectNameTH AS FTSectName
                        , FTUnitSectCode, FTUnitSectNameTH AS FTUnitSectName
                        , FTPositCode, FTPositNameTH AS FTPositName
                        , FTStateActive, FTStateLeader
                        FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.HRDV_THRMOrganizationChart 
                        WHERE FTStateActive = 1 "
        Else
            _Qry = " SELECT 
                         FNHSysOrgId
                        , 'HIGROUP' AS FNHGroup
                        ,FTCLevelCode, FTCLevelNameEN AS FTCLevelName
                        , FTCountryCode, FTCountryNameEN AS FTCountryName
                        ,  FTCmpCode, FTCmpNameEN AS FTCmpName

                        , FTDivisonCode, FTDivisonNameEN AS FTDivisonName
                        , FTDeptCode, FTDeptDescEN AS FTDeptDesc
                        , FTSectCode, FTSectNameEN AS FTSectName
                        , FTUnitSectCode, FTUnitSectNameEN AS FTUnitSectName
                        , FTPositCode, FTPositNameEN AS FTPositName
                        , FTStateActive, FTStateLeader
                        FROM    " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.HRDV_THRMOrganizationChart 
                        WHERE FTStateActive = 1"
        End If

        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        ogcorganize.DataSource = _Dt


    End Sub
#End Region

#Region "MAIN PROC"


    Private Sub ProcessClose(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

#End Region

#Region "General"

    Private Sub Ogvorganize_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles ogvorganize.RowCellClick
        Try

            Dim FNHSysOrgId As String = ogvorganize.GetFocusedRowCellValue("FNHSysOrgId").ToString()
            Dim FNHGroup As String = ogvorganize.GetFocusedRowCellValue("FNHGroup").ToString()
            Dim FTCLevelCode As String = ogvorganize.GetFocusedRowCellValue("FTCLevelCode").ToString()
            Dim FTCLevelName As String = ogvorganize.GetFocusedRowCellValue("FTCLevelName").ToString()
            Dim FTCountryCode As String = ogvorganize.GetFocusedRowCellValue("FTCountryCode").ToString()
            Dim FTCountryName As String = ogvorganize.GetFocusedRowCellValue("FTCountryName").ToString()
            Dim FTCmpCode As String = ogvorganize.GetFocusedRowCellValue("FTCmpCode").ToString()
            Dim FTCmpName As String = ogvorganize.GetFocusedRowCellValue("FTCmpName").ToString()
            Dim FTDivisonCode As String = ogvorganize.GetFocusedRowCellValue("FTDivisonCode").ToString()
            Dim FTDivisonName As String = ogvorganize.GetFocusedRowCellValue("FTDivisonName").ToString()
            Dim FTDeptCode As String = ogvorganize.GetFocusedRowCellValue("FTDeptCode").ToString()
            Dim FTDeptDesc As String = ogvorganize.GetFocusedRowCellValue("FTDeptDesc").ToString()
            Dim FTSectCode As String = ogvorganize.GetFocusedRowCellValue("FTSectCode").ToString()
            Dim FTSectName As String = ogvorganize.GetFocusedRowCellValue("FTSectName").ToString()
            Dim FTUnitSectCode As String = ogvorganize.GetFocusedRowCellValue("FTUnitSectCode").ToString()
            Dim FTUnitSectName As String = ogvorganize.GetFocusedRowCellValue("FTUnitSectName").ToString()
            Dim FTPositCode As String = ogvorganize.GetFocusedRowCellValue("FTPositCode").ToString()
            Dim FTPositName As String = ogvorganize.GetFocusedRowCellValue("FTPositName").ToString()

            _FNHSysOrgId_O = FNHSysOrgId
            _FNHGroup_O = FNHGroup
            _FTCLevelCode_O = FTCLevelCode
            _FTCLevelName_O = FTCLevelName
            _FTCountryCode_O = FTCountryCode
            _FTCountryName_O = FTCountryName
            _FTCmpCode_O = FTCmpCode
            _FTCmpName_O = FTCmpName
            _FTDivisonCode_O = FTDivisonCode
            _FTDivisonName_O = FTDivisonName
            _FTDeptCode_O = FTDeptCode
            _FTDeptDesc_O = FTDeptDesc
            _FTSectCode_O = FTSectCode
            _FTSectName_O = FTSectName
            _FTUnitSectCode_O = FTUnitSectCode
            _FTUnitSectName_O = FTUnitSectName
            _FTPositCode_O = FTPositCode
            _FTPositName_O = FTPositName

            Me.Close()

        Catch ex As Exception
        End Try
    End Sub

    Private _Type As String
    Public Property Type As String
        Get
            Return _Type
        End Get
        Set(value As String)
            _Type = value
        End Set
    End Property

    Private _FNHSysOrgId_O As String
    Public Property FNHSysOrgId_O As String
        Get
            Return _FNHSysOrgId_O
        End Get
        Set(value As String)
            _FNHSysOrgId_O = value
        End Set
    End Property

    Private _FNHGroup_O As String
    Public Property FNHGroup_O As String
        Get
            Return _FNHGroup_O
        End Get
        Set(value As String)
            _FNHGroup_O = value
        End Set
    End Property

    Private _FTCLevelCode_O As String
    Public Property FTCLevelCode_O As String
        Get
            Return _FTCLevelCode_O
        End Get
        Set(value As String)
            _FTCLevelCode_O = value
        End Set
    End Property

    Private _FTCLevelName_O As String
    Public Property FTCLevelName_O As String
        Get
            Return _FTCLevelName_O
        End Get
        Set(value As String)
            _FTCLevelName_O = value
        End Set
    End Property




    Private _FTCountryCode_O As String
    Public Property FTCountryCode_O As String
        Get
            Return _FTCountryCode_O
        End Get
        Set(value As String)
            _FTCountryCode_O = value
        End Set
    End Property

    Private _FTCountryName_O As String
    Public Property FTCountryName_O As String
        Get
            Return _FTCountryName_O
        End Get
        Set(value As String)
            _FTCountryName_O = value
        End Set
    End Property

    Private _FTCmpCode_O As String
    Public Property FTCmpCode_O As String
        Get
            Return _FTCmpCode_O
        End Get
        Set(value As String)
            _FTCmpCode_O = value
        End Set
    End Property

    Private _FTCmpName_O As String
    Public Property FTCmpName_O As String
        Get
            Return _FTCmpName_O
        End Get
        Set(value As String)
            _FTCmpName_O = value
        End Set
    End Property

    Private _FTDivisonCode_O As String
    Public Property FTDivisonCode_O As String
        Get
            Return _FTDivisonCode_O
        End Get
        Set(value As String)
            _FTDivisonCode_O = value
        End Set
    End Property

    Private _FTDivisonName_O As String
    Public Property FTDivisonName_O As String
        Get
            Return _FTDivisonName_O
        End Get
        Set(value As String)
            _FTDivisonName_O = value
        End Set
    End Property

    Private _FTDeptCode_O As String
    Public Property FTDeptCode_O As String
        Get
            Return _FTDeptCode_O
        End Get
        Set(value As String)
            _FTDeptCode_O = value
        End Set
    End Property

    Private _FTDeptDesc_O As String
    Public Property FTDeptDesc_O As String
        Get
            Return _FTDeptDesc_O
        End Get
        Set(value As String)
            _FTDeptDesc_O = value
        End Set
    End Property

    Private _FTSectCode_O As String
    Public Property FTSectCode_O As String
        Get
            Return _FTSectCode_O
        End Get
        Set(value As String)
            _FTSectCode_O = value
        End Set
    End Property

    Private _FTSectName_O As String
    Public Property FTSectName_O As String
        Get
            Return _FTSectName_O
        End Get
        Set(value As String)
            _FTSectName_O = value
        End Set
    End Property

    Private _FTUnitSectCode_O As String
    Public Property FTUnitSectCode_O As String
        Get
            Return _FTUnitSectCode_O
        End Get
        Set(value As String)
            _FTUnitSectCode_O = value
        End Set
    End Property

    Private _FTUnitSectName_O As String
    Public Property FTUnitSectName_O As String
        Get
            Return _FTUnitSectName_O
        End Get
        Set(value As String)
            _FTUnitSectName_O = value
        End Set
    End Property

    Private _FTPositCode_O As String
    Public Property FTPositCode_O As String
        Get
            Return _FTPositCode_O
        End Get
        Set(value As String)
            _FTPositCode_O = value
        End Set
    End Property

    Private _FTPositName_O As String
    Public Property FTPositName_O As String
        Get
            Return _FTPositName_O
        End Get
        Set(value As String)
            _FTPositName_O = value
        End Set
    End Property






#End Region

End Class