Imports DevExpress.Data
Imports DevExpress.Utils
Imports System.IO
Imports System.Windows.Forms

Public Class wStyleSendSuplTracking


    Private _RowDataChange As Boolean



    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.





    End Sub



#Region "Property"

    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(value As String)
            _CallMethodParm = value
        End Set
    End Property

    Private _ActualDate As String = ""
    ReadOnly Property ActualDate As String
        Get
            Return _ActualDate
        End Get
    End Property

    Private _ActualNextDate As String = ""
    ReadOnly Property ActualNextDate As String
        Get
            Return _ActualNextDate
        End Get
    End Property


#End Region

#Region "Procedure"

    Private Sub LoadData(Optional BomId As Integer = 0)
        Dim _Qry As String = ""
        Dim _dt As DataTable




        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")


        _Qry = " SELECT A.*,B.*,Case When ISNULL(B.FTUpdUser,'') <> '' THEN B.FTUpdUser ELSE B.FTInsUser END AS FTUpdateUser "
        _Qry &= vbCrLf & " ,Case When ISNULL(B.FTUpdUser,'') <> '' THEN B.FDUpdDate ELSE B.FDInsDate END AS FTUpdateDate "
        _Qry &= vbCrLf & " ,Case When ISNULL(B.FTUpdUser,'') <> '' THEN B.FTUpdTime ELSE B.FTInsTime END AS FTUpdateTime "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " ,PMT.FTPartNameTH AS FTPartName"
        Else
            _Qry &= vbCrLf & " ,PMT.FTPartNameEN As FTPartName "
        End If


        _Qry &= vbCrLf & "  FROM (  SELECT        A.FNHSysStyleId "
        _Qry &= vbCrLf & "  , A.FNHSysSeasonId "
        _Qry &= vbCrLf & ",  ST.FTStyleCode, SS.FTSeasonCode "
        _Qry &= vbCrLf & "	, ST.FTStyleNameEN AS FTStyleName "
        _Qry &= vbCrLf & "	 ,A3.FNHSysPartId  "
        _Qry &= vbCrLf & " From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM AS A WITH(NOLOCK) INNER Join "
        _Qry &= vbCrLf & " " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle AS ST WITH(NOLOCK) ON A.FNHSysStyleId = ST.FNHSysStyleId INNER JOIN "
        _Qry &= vbCrLf & "  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMSeason As SS With(NOLOCK) On A.FNHSysSeasonId = SS.FNHSysSeasonId "
        _Qry &= vbCrLf & "  INNER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Mat AS A2 WITH(NOLOCK) ON A.FNHSysBomId = A2.FNHSysBomId "
        _Qry &= vbCrLf & "  INNER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Part AS A3 WITH(NOLOCK) ON A2.FNHSysBomId = A3.FNHSysBomId AND A2.FNSeq = A3.FNSeq "

        If FNHSysBuyId.Text.Trim <> "" Then

            _Qry &= vbCrLf & "  INNER JOIN ( "
            _Qry &= vbCrLf & " SELECT DISTINCT ORDBUY.FNHSysStyleId,ORDBUY.FNHSysSeasonId "
            _Qry &= vbCrLf & " FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrder AS ORDBUY WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE ORDBUY.FNHSysBuyId=" & Val(FNHSysBuyId.Properties.Tag.ToString) & ""

            _Qry &= vbCrLf & "  )  ORDBUY ON A.FNHSysStyleId = ORDBUY.FNHSysStyleId AND A.FNHSysSeasonId = ORDBUY.FNHSysSeasonId"

        End If


        _Qry &= vbCrLf & "   WHERE A.FNHSysBomId > 0  AND A.FTStateActive='1' AND A2.FTStateMatConfirm ='1'"

        If BomId > 0 Then
            _Qry &= vbCrLf & "   AND A.FNHSysBomId = " & BomId & " "
        End If


        If FNHSysStyleId.Text.Trim <> "" Then

            _Qry &= vbCrLf & "   AND A.FNHSysStyleId = " & Val(FNHSysStyleId.Properties.Tag.ToString) & " "

        End If

        If FNHSysSeasonId.Text.Trim <> "" Then

            _Qry &= vbCrLf & "   AND A.FNHSysSeasonId = " & Val(FNHSysSeasonId.Properties.Tag.ToString) & " "

        End If

        _Qry &= vbCrLf & "  GROUP BY "
        _Qry &= vbCrLf & "	 A.FNHSysStyleId "
        _Qry &= vbCrLf & "  , A.FNHSysSeasonId "
        _Qry &= vbCrLf & ",  ST.FTStyleCode, SS.FTSeasonCode "
        _Qry &= vbCrLf & "	, ST.FTStyleNameEN "
        _Qry &= vbCrLf & "	 ,A3.FNHSysPartId  "
        _Qry &= vbCrLf & " ) AS A  "
        _Qry &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS PMT WITH(NOLOCK) ON A.FNHSysPartId = PMT.FNHSysPartId "
        _Qry &= vbCrLf & "    OUTER APPLY (SELECT  TOP 1 *  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part AS STM WITH(NOLOCK) WHERE STM.FNHSysStyleId=A.FNHSysStyleId AND  STM.FNHSysSeasonId=A.FNHSysSeasonId AND STM.FNHSysPartId = A.FNHSysPartId ) AS B  "


        Select Case FNDataType.SelectedIndex
            Case 1, 2
                _Qry &= vbCrLf & "    OUTER APPLY (SELECT  TOP 1 '1'  As FTState  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part AS STM2 WITH(NOLOCK) WHERE STM2.FNHSysStyleId=A.FNHSysStyleId AND  STM2.FNHSysSeasonId=A.FNHSysSeasonId ) AS B2  "

        End Select

        Select Case FNDataType.SelectedIndex
            Case 1
                _Qry &= vbCrLf & "  AND  B2.FTState ='1'   "
            Case 2
                _Qry &= vbCrLf & "  AND  ISNULL(B2.FTState,'') =''   "
        End Select



        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
        ogc.DataSource = _dt.Copy


        _dt.Dispose()
        _Spls.Close()

        _RowDataChange = False

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = True



        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function
#End Region

#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogv)



        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If VerifyData() Then


            Call LoadData()

        End If
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogv)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub


    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

End Class