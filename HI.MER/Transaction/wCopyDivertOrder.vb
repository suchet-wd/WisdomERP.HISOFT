Public Class wCopyDivertOrder

    Private _TabSelected As Integer
    Public _DDt As DataTable
    Public _ComDt As DataTable
    Public _SewDt As DataTable
    Public _PacDt As DataTable

    Private _wDivert As wDivertOrderDistination
    Private _wListCompleteCopyOrder As wListCompleteCopyOrder

    Public Sub New(ByRef ptDt As DataTable, ByVal ptTabSelected As Integer)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        '_DDt = ptDt
        _TabSelected = ptTabSelected

    End Sub

    Private Sub ocmok_Click(sender As Object, e As EventArgs) Handles ocmok.Click
        Dim _Qry As String = ""
        Dim _Dt As DataTable

        'Select Case _TabSelected
        '    Case 1
        If Me.ockcomponent.Checked Then
            If (FNDivertSeq.Text <> "") Then
                _Qry = "SELECT CONVERT(INT, A.FNSeq) AS FNSeq, A.FNSeq AS FNSeqOrg, A.FNPart, B.FTMainMatCode,"
                Select Case HI.ST.Lang.Language
                    Case HI.ST.Lang.eLang.TH
                        _Qry &= vbCrLf & "       A.FTOrderNo, A.FTSubOrderNo, A.FNHSysMerMatId, (B.FTMainMatNameTH) AS FTMainMatDesc"
                    Case Else
                        _Qry &= vbCrLf & "       A.FTOrderNo, A.FTSubOrderNo, A.FNHSysMerMatId, (B.FTMainMatNameEN) AS FTMainMatDesc"
                End Select
                _Qry &= vbCrLf & "       , A.FNConSmp"
                _Qry &= vbCrLf & "       , ISNULL(A.FTComponent, '') AS FTComponent, ISNULL(A.FTRemark, '') AS FTRemark"
                _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Component As A With(NOLOCK) "
                _Qry &= vbCrLf & "LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.[TMERMMainMat] As B With(NOLOCK) On A.FNHSysMerMatId = B.FNHSysMainMatId"
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' AND FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "' AND FNDivertSeq =" & (FNDivertSeq.Text)
                _Qry &= vbCrLf & "ORDER BY A.FNSeq ASC;"
            Else
                _Qry = "SELECT CONVERT(INT, A.FNSeq) AS FNSeq, A.FNSeq AS FNSeqOrg, A.FNPart, B.FTMainMatCode,"
                Select Case HI.ST.Lang.Language
                    Case HI.ST.Lang.eLang.TH
                        _Qry &= vbCrLf & "       A.FTOrderNo, A.FTSubOrderNo, A.FNHSysMerMatId, (B.FTMainMatNameTH) AS FTMainMatDesc"
                    Case Else
                        _Qry &= vbCrLf & "       A.FTOrderNo, A.FTSubOrderNo, A.FNHSysMerMatId, (B.FTMainMatNameEN) AS FTMainMatDesc"
                End Select
                _Qry &= vbCrLf & "       , A.FNConSmp"
                _Qry &= vbCrLf & "       , ISNULL(A.FTComponent, '') AS FTComponent, ISNULL(A.FTRemark, '') AS FTRemark"
                _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Component As A With(NOLOCK) "
                _Qry &= vbCrLf & "LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.[TMERMMainMat] As B With(NOLOCK) On A.FNHSysMerMatId = B.FNHSysMainMatId"
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' AND FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "'"
                _Qry &= vbCrLf & "ORDER BY A.FNSeq ASC;"
            End If
            _ComDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
        End If

        If Me.ockSewing.Checked Then
            If (FNDivertSeq.Text <> "") Then
                _Qry = "  Select FNSewSeq, FTSewDescription, FTSewNote, FTImage"
                _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Sew With (NOLOCK)"
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' AND FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "' AND FNDivertSeq =" & (FNDivertSeq.Text)
                _Qry &= vbCrLf & "ORDER BY FNSewSeq ASC;"
            Else
                _Qry = "  Select FNSewSeq, FTSewDescription, FTSewNote, FTImage"
                _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Sew With (NOLOCK)"
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' AND FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "'"
                _Qry &= vbCrLf & "ORDER BY FNSewSeq ASC;"
            End If
            _SewDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
        End If

        'Case 2
        If Me.ockPacking.Checked Then
            If (FNDivertSeq.Text <> "") Then
                _Qry = "  Select FNPackSeq, FTPackDescription, FTPackNote, FTImage, FBImage"
                _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Pack With (NOLOCK)"
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' AND FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "' AND FNDivertSeq =" & (FNDivertSeq.Text)
                _Qry &= vbCrLf & "ORDER BY FNPackSeq ASC;"
            Else
                _Qry = "  Select FNPackSeq, FTPackDescription, FTPackNote, FTImage, FBImage"
                _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Pack With (NOLOCK)"
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' AND FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "'"
                _Qry &= vbCrLf & "ORDER BY FNPackSeq ASC;"
            End If
            _PacDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
        End If
        'Case 3

        'If (FNDivertSeq.Text <> "") Then
        '        _Qry = "  Select FTColorway, FTSizeBreakDown, FNQuantity"
        '        _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Bundle With (NOLOCK)"
        '        _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' AND FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "' AND FNDivertSeq =" & (FNDivertSeq.Text)
        '    Else
        '        _Qry = "  Select FTColorway, FTSizeBreakDown, FNQuantity"
        '        _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Bundle With (NOLOCK)"
        '        _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' AND FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "'"
        '    End If

        'Case 4
        If Me.ockSizeSpec.Checked Then
            If (FNDivertSeq.Text <> "") Then
                _Qry = "  Select FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension, FTTolerant, FNHSysMeasId"
                _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_SizeSpec With (NOLOCK)"
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' AND FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "' AND FNDivertSeq =" & (FNDivertSeq.Text)
            Else
                _Qry = "  Select FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension, FTTolerant, FNHSysMeasId"
                _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_SizeSpec With (NOLOCK)"
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' AND FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "'"
            End If
            _DDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
        End If
        'Case 5

        'End Select
        '_DDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
        '_DDt = _Dt
        Me.Close()
    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.Close()
    End Sub
End Class