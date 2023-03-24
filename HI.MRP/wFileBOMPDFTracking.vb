Imports DevExpress.Data
Imports DevExpress.Utils
Imports System.ComponentModel
Imports System.IO
Imports System.Windows.Forms

Public Class wFileBOMPDFTracking


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

    Private Sub LoadData()
        Dim _Qry As String = ""
        Dim _dt As DataTable




        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")


        _Qry = " SELECT A.*,B.* "




        _Qry &= vbCrLf & "  FROM (  SELECT        ORDBUY.FNHSysStyleId "
        _Qry &= vbCrLf & "  , ORDBUY.FNHSysSeasonId "
        _Qry &= vbCrLf & ",  ST.FTStyleCode, SS.FTSeasonCode "
        _Qry &= vbCrLf & "	, ST.FTStyleNameEN AS FTStyleName "

        _Qry &= vbCrLf & " From   ( "
        _Qry &= vbCrLf & " SELECT DISTINCT ORDBUY.FNHSysStyleId,ORDBUY.FNHSysSeasonId "
        _Qry &= vbCrLf & " FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrder AS ORDBUY WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE ORDBUY.FNHSysBuyId=" & Val(FNHSysBuyId.Properties.Tag.ToString) & ""
        _Qry &= vbCrLf & "  )  ORDBUY "

        _Qry &= vbCrLf & "  INNER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle AS ST WITH(NOLOCK) ON ORDBUY.FNHSysStyleId = ST.FNHSysStyleId INNER JOIN "
        _Qry &= vbCrLf & "  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMSeason As SS With(NOLOCK) On ORDBUY.FNHSysSeasonId = SS.FNHSysSeasonId "



        _Qry &= vbCrLf & "  GROUP BY "
        _Qry &= vbCrLf & "	 ORDBUY.FNHSysStyleId "
        _Qry &= vbCrLf & "  , ORDBUY.FNHSysSeasonId "
        _Qry &= vbCrLf & ",  ST.FTStyleCode, SS.FTSeasonCode "
        _Qry &= vbCrLf & "	, ST.FTStyleNameEN "

        _Qry &= vbCrLf & " ) AS A  "

        _Qry &= vbCrLf & "    OUTER APPLY (SELECT  TOP 1 *  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTBOMFilePDF AS STM WITH(NOLOCK) WHERE STM.FNHSysStyleId=A.FNHSysStyleId AND  STM.FNHSysSeasonId=A.FNHSysSeasonId  ) AS B  "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
        ogc.DataSource = _dt.Copy


        _dt.Dispose()
        _Spls.Close()

        _RowDataChange = False

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = True

        If FNHSysBuyId.Text.Trim = "" Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If

        Return _Pass
    End Function
#End Region

#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogv)


            ogv.Columns("Attach").Visible = ocmuserattachfilepdf.Enabled
            ogv.Columns("View").Visible = ocmuserviewfilepdf.Enabled

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

    'Private Sub ogv_Click(sender As Object, e As EventArgs) Handles ogv.Click

    '    Select Case ogv.FocusedColumn.Name
    '        Case "Attach"

    '            With New System.Windows.Forms.OpenFileDialog
    '                .Filter = "PDF Files(*.pdf)|*.pdf|All Files(*.*)|*.*"
    '                If .ShowDialog() = DialogResult.OK Then

    '                End If
    '            End With


    '        Case "View"

    '            If ogv.GetFocusedRowCellValue("FTStatePDF").ToString = "1" Then

    '            End If
    '    End Select
    'End Sub

    Private Sub ogv_ShowingEditor(sender As Object, e As CancelEventArgs) Handles ogv.ShowingEditor
        Select Case ogv.FocusedColumn.Name
            Case "Attach"

                With New System.Windows.Forms.OpenFileDialog
                    .Filter = "PDF Files(*.pdf)|*.pdf|All Files(*.*)|*.*"
                    If .ShowDialog() = DialogResult.OK Then

                        Dim PDFV As New DevExpress.XtraPdfViewer.PdfViewer
                    End If
                End With


            Case "View"

                If ogv.GetFocusedRowCellValue("FTStatePDF").ToString = "1" Then

                End If
        End Select

        e.Cancel = True
    End Sub

    Private Sub RepositoryItemMemoExEditNote_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemMemoExEditNote.EditValueChanged
        Try


            Dim pValue As String = ""
            Dim pOldValue As String = ""


            With CType(sender, DevExpress.XtraEditors.MemoExEdit)
                pValue = .EditValue.ToString
                pOldValue = .OldEditValue.ToString
            End With
            '   Dim pSeq As Integer = Val(Me.ogvmat.GetFocusedRowCellValue("FNSeq").ToString)

            'If (pOldValue) <> pValue And pSeq > 0 Then



            'Else

            '    '   Me.ogvmat.SetFocusedRowCellValue(Me.ogvmat.FocusedColumn.FieldName, (pOldValue))
            'End If



        Catch ex As Exception


        End Try
    End Sub
End Class