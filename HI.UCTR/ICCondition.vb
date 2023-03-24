Imports System.Windows.Forms
Public Class ICCondition

#Region "WareHouse"

    Private m_DbDtWHNo As New DataTable
    ReadOnly Property DbDtWHNo As DataTable
        Get
            Return m_DbDtWHNo
        End Get
    End Property

    Property ShowWHNo As Boolean
        Get
            Return otpWhNo.PageVisible
        End Get

        Set(ByVal value As Boolean)
            otpWhNo.PageVisible = value
        End Set

    End Property

    Private Sub FNWHCon_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNWHCon.SelectedIndexChanged
        FNHSysWHId.Properties.ReadOnly = (FNWHCon.SelectedIndex = 0)
        FNHSysWHIdTo.Properties.ReadOnly = Not (FNWHCon.SelectedIndex = 1)

        FNHSysWHId.Properties.Buttons(0).Enabled = Not (FNHSysWHId.Properties.ReadOnly)
        FNHSysWHIdTo.Properties.Buttons(0).Enabled = Not (FNHSysWHIdTo.Properties.ReadOnly)

        FNHSysWHId.Text = ""
        FNHSysWHIdTo.Text = ""

        m_DbDtWHNo.Rows.Clear()
        m_DbDtWHNo.AcceptChanges()
    End Sub


    Private Sub FNHSysWHId_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FNHSysWHId.KeyDown

        Try
            Select Case Me.FNWHCon.SelectedIndex
                Case 2
                    Select Case e.KeyCode
                        Case Keys.Enter

                            If FNHSysWHId.Text = "" Then Exit Sub
                            If FNHSysWHId.Properties.Tag.ToString = "" Then Exit Sub

                            Dim NewRow As DataRow = m_DbDtWHNo.NewRow
                            NewRow("FTCode") = FNHSysWHId.Text
                            NewRow("FTName") = FNHSysWHId_None.Text

                            m_DbDtWHNo.Rows.Add(NewRow)
                            m_DbDtWHNo.AcceptChanges()

                    End Select
                Case Else
            End Select
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ogvWHNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ogvWHNo.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    Call ogvWHNo_DoubleClick(ogvWHNo, New System.EventArgs)
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvWHNo_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ogvWHNo.DoubleClick
        Try
            With ogvWHNo
                If .FocusedRowHandle < 0 Then Exit Sub
                .DeleteRow(.FocusedRowHandle)
                m_DbDtWHNo.AcceptChanges()
            End With
        Catch ex As Exception

        End Try
    End Sub


#End Region

#Region "Supl"

    Private m_DbDtSupl As New DataTable
    ReadOnly Property DbDtSupl As DataTable
        Get
            Return m_DbDtSupl
        End Get
    End Property

    Property ShowmSupl As Boolean
        Get
            Return otpSupl.PageVisible
        End Get

        Set(ByVal value As Boolean)
            otpSupl.PageVisible = value
        End Set

    End Property


    Private Sub FNSuplCon_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles FNSuplCon.SelectedIndexChanged

        FNHSysSuplId.Properties.ReadOnly = (FNSuplCon.SelectedIndex = 0)
        FNHSysSuplIdTo.Properties.ReadOnly = Not (FNSuplCon.SelectedIndex = 1)

        FNHSysSuplId.Properties.Buttons(0).Enabled = Not (FNHSysSuplId.Properties.ReadOnly)
        FNHSysSuplIdTo.Properties.Buttons(0).Enabled = Not (FNHSysSuplIdTo.Properties.ReadOnly)

        FNHSysSuplId.Text = ""
        FNHSysSuplIdTo.Text = ""

        m_DbDtSupl.Rows.Clear()
        m_DbDtSupl.AcceptChanges()

    End Sub


    Private Sub ocbemp3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FNHSysSuplId.KeyDown

        Try
            Select Case Me.FNSuplCon.SelectedIndex
                Case 2
                    Select Case e.KeyCode
                        Case Keys.Enter

                            If FNHSysSuplId.Text = "" Then Exit Sub
                            If FNHSysSuplId.Properties.Tag.ToString = "" Then Exit Sub

                            Dim NewRow As DataRow = m_DbDtSupl.NewRow
                            NewRow("FTCode") = FNHSysSuplId.Text
                            NewRow("FTName") = FNHSysSuplId_None.Text

                            m_DbDtSupl.Rows.Add(NewRow)
                            m_DbDtSupl.AcceptChanges()

                    End Select
                Case Else
            End Select
        Catch ex As Exception

        End Try

    End Sub
    Private Sub ogvSupl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ogvSupl.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    Call ogvSupl_DoubleClick(ogvSupl, New System.EventArgs)
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvSupl_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ogvSupl.DoubleClick
        Try
            With ogvSupl
                If .FocusedRowHandle < 0 Then Exit Sub
                .DeleteRow(.FocusedRowHandle)
                m_DbDtSupl.AcceptChanges()
            End With
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "ItemCode"

    Private m_DbDtItemCode As New DataTable
    ReadOnly Property DbDtItemCode As DataTable
        Get
            Return m_DbDtItemCode
        End Get
    End Property

    Property ShowmItemCode As Boolean
        Get
            Return otpItemCode.PageVisible
        End Get

        Set(ByVal value As Boolean)
            otpItemCode.PageVisible = value
        End Set

    End Property


    Private Sub FNItemCodeCon_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles FNItemCon.SelectedIndexChanged

        FNHSysRawMatId.Properties.ReadOnly = (FNItemCon.SelectedIndex = 0)
        FNHSysRawMatIdTo.Properties.ReadOnly = Not (FNItemCon.SelectedIndex = 1)

        FNHSysRawMatId.Properties.Buttons(0).Enabled = Not (FNHSysRawMatId.Properties.ReadOnly)
        FNHSysRawMatIdTo.Properties.Buttons(0).Enabled = Not (FNHSysRawMatIdTo.Properties.ReadOnly)

        FNHSysRawMatId.Text = ""
        FNHSysRawMatIdTo.Text = ""

        m_DbDtItemCode.Rows.Clear()
        m_DbDtItemCode.AcceptChanges()

    End Sub


    Private Sub otpItemCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FNHSysRawMatId.KeyDown

        Try
            Select Case Me.FNItemCon.SelectedIndex
                Case 2
                    Select Case e.KeyCode
                        Case Keys.Enter

                            If FNHSysRawMatId.Text = "" Then Exit Sub
                            If FNHSysRawMatId.Properties.Tag.ToString = "" Then Exit Sub

                            Dim NewRow As DataRow = m_DbDtItemCode.NewRow
                            NewRow("FTCode") = FNHSysRawMatId.Text
                            NewRow("FTName") = FNHSysRawMatId_None.Text

                            m_DbDtItemCode.Rows.Add(NewRow)
                            m_DbDtItemCode.AcceptChanges()

                    End Select
                Case Else
            End Select
        Catch ex As Exception

        End Try

    End Sub
    Private Sub ogvItemCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ogvItemCode.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    Call ogvItemCode_DoubleClick(ogvItemCode, New System.EventArgs)
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvItemCode_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ogvItemCode.DoubleClick
        Try
            With ogvItemCode
                If .FocusedRowHandle < 0 Then Exit Sub
                .DeleteRow(.FocusedRowHandle)
                m_DbDtItemCode.AcceptChanges()
            End With
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "ColorCode"

    Private m_DbDtColorCode As New DataTable
    ReadOnly Property DbDtColorCode As DataTable
        Get
            Return m_DbDtColorCode
        End Get
    End Property

    Property ShowmColorCode As Boolean
        Get
            Return otpColorCode.PageVisible
        End Get

        Set(ByVal value As Boolean)
            otpColorCode.PageVisible = value
        End Set

    End Property


    Private Sub FNItemCon_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles FNColorCon.SelectedIndexChanged

        FNHSysRawMatColorId.Properties.ReadOnly = (FNColorCon.SelectedIndex = 0)
        FNHSysRawMatColorIdTo.Properties.ReadOnly = Not (FNColorCon.SelectedIndex = 1)

        FNHSysRawMatColorId.Properties.Buttons(0).Enabled = Not (FNHSysRawMatColorId.Properties.ReadOnly)
        FNHSysRawMatColorIdTo.Properties.Buttons(0).Enabled = Not (FNHSysRawMatColorIdTo.Properties.ReadOnly)

        FNHSysRawMatColorId.Text = ""
        FNHSysRawMatColorIdTo.Text = ""

        m_DbDtColorCode.Rows.Clear()
        m_DbDtColorCode.AcceptChanges()

    End Sub


    Private Sub otpColorCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FNHSysRawMatColorId.KeyDown

        Try
            Select Case Me.FNColorCon.SelectedIndex
                Case 2
                    Select Case e.KeyCode
                        Case Keys.Enter

                            If FNHSysRawMatColorId.Text = "" Then Exit Sub
                            If FNHSysRawMatColorId.Properties.Tag.ToString = "" Then Exit Sub

                            Dim NewRow As DataRow = m_DbDtColorCode.NewRow
                            NewRow("FTCode") = FNHSysRawMatColorId.Text
                            NewRow("FTName") = FNHSysRawMatColorId_None.Text

                            m_DbDtColorCode.Rows.Add(NewRow)
                            m_DbDtColorCode.AcceptChanges()

                    End Select
                Case Else
            End Select
        Catch ex As Exception

        End Try

    End Sub
    Private Sub ogvColorCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ogvColorCode.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    Call ogvColorCode_DoubleClick(ogvColorCode, New System.EventArgs)
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvColorCode_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ogvColorCode.DoubleClick
        Try
            With ogvColorCode
                If .FocusedRowHandle < 0 Then Exit Sub
                .DeleteRow(.FocusedRowHandle)
                m_DbDtColorCode.AcceptChanges()
            End With
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "SizeCode"

    Private m_DbDtSizeCode As New DataTable
    ReadOnly Property DbDtSizeCode As DataTable
        Get
            Return m_DbDtSizeCode
        End Get
    End Property

    Property ShowmSizeCode As Boolean
        Get
            Return otpSizeCode.PageVisible
        End Get

        Set(ByVal value As Boolean)
            otpSizeCode.PageVisible = value
        End Set

    End Property


    Private Sub FNSizeCon_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles FNSizeCon.SelectedIndexChanged

        FNHSysRawMatSizeId.Properties.ReadOnly = (FNSizeCon.SelectedIndex = 0)
        FNHSysRawMatSizeIdTo.Properties.ReadOnly = Not (FNSizeCon.SelectedIndex = 1)

        FNHSysRawMatSizeId.Properties.Buttons(0).Enabled = Not (FNHSysRawMatSizeId.Properties.ReadOnly)
        FNHSysRawMatSizeIdTo.Properties.Buttons(0).Enabled = Not (FNHSysRawMatSizeIdTo.Properties.ReadOnly)

        FNHSysRawMatSizeId.Text = ""
        FNHSysRawMatSizeIdTo.Text = ""

        m_DbDtSizeCode.Rows.Clear()
        m_DbDtSizeCode.AcceptChanges()

    End Sub


    Private Sub otpSizeCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FNHSysRawMatSizeId.KeyDown

        Try
            Select Case Me.FNSizeCon.SelectedIndex
                Case 2
                    Select Case e.KeyCode
                        Case Keys.Enter

                            If FNHSysRawMatSizeId.Text = "" Then Exit Sub
                            If FNHSysRawMatSizeId.Properties.Tag.ToString = "" Then Exit Sub

                            Dim NewRow As DataRow = m_DbDtSizeCode.NewRow
                            NewRow("FTCode") = FNHSysRawMatSizeId.Text
                            NewRow("FTName") = FNHSysRawMatSizeId_None.Text

                            m_DbDtSizeCode.Rows.Add(NewRow)
                            m_DbDtSizeCode.AcceptChanges()

                    End Select
                Case Else
            End Select
        Catch ex As Exception

        End Try

    End Sub
    Private Sub ogvSizeCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ogvSizeCode.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    Call ogvSizerCode_DoubleClick(ogvSizeCode, New System.EventArgs)
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvSizerCode_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ogvSizeCode.DoubleClick
        Try
            With ogvSizeCode
                If .FocusedRowHandle < 0 Then Exit Sub
                .DeleteRow(.FocusedRowHandle)
                m_DbDtSizeCode.AcceptChanges()
            End With
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "User"

    Private m_DbDtUser As New DataTable
    ReadOnly Property DbDtUser As DataTable
        Get
            Return m_DbDtUser
        End Get
    End Property

    Property ShowmUser As Boolean
        Get
            Return otpUser.PageVisible
        End Get

        Set(ByVal value As Boolean)
            otpUser.PageVisible = value
        End Set

    End Property


    Private Sub FNUserCon_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles FNUserCon.SelectedIndexChanged

        FTUser.Properties.ReadOnly = (FNUserCon.SelectedIndex = 0)
        FTUserTo.Properties.ReadOnly = Not (FNUserCon.SelectedIndex = 1)

        FTUser.Properties.Buttons(0).Enabled = Not (FTUser.Properties.ReadOnly)
        FTUserTo.Properties.Buttons(0).Enabled = Not (FTUserTo.Properties.ReadOnly)

        FTUser.Text = ""
        FTUserTo.Text = ""

        m_DbDtUser.Rows.Clear()
        m_DbDtUser.AcceptChanges()

    End Sub


    Private Sub FTUser_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FTUser.KeyDown

        Try
            Select Case Me.FNUserCon.SelectedIndex
                Case 2
                    Select Case e.KeyCode
                        Case Keys.Enter

                            If FTUser.Text = "" Then Exit Sub
                            If FTUser.Properties.Tag.ToString = "" Then Exit Sub

                            Dim NewRow As DataRow = m_DbDtUser.NewRow
                            NewRow("FTCode") = FTUser.Text
                            NewRow("FTName") = FTUser_None.Text

                            m_DbDtUser.Rows.Add(NewRow)
                            m_DbDtUser.AcceptChanges()

                    End Select
                Case Else
            End Select
        Catch ex As Exception

        End Try

    End Sub
    Private Sub ogvemp_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ogvUserBy.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    Call ogvemp_DoubleClick(ogvUserBy, New System.EventArgs)
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvemp_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ogvUserBy.DoubleClick
        Try
            With ogvUserBy
                If .FocusedRowHandle < 0 Then Exit Sub
                .DeleteRow(.FocusedRowHandle)
                m_DbDtUser.AcceptChanges()
            End With
        Catch ex As Exception

        End Try
    End Sub

#End Region

    Public Sub PrePareData()

        Dim _Qry As String = ""
        Try
            ' Dim oDt As DataTable

            If otpItemCode.PageVisible Then
                _Qry = "  SELECT FTCode,FTName  FROM (SELECT  TOP 0  '' AS FTCode  ,'' AS FTName  )AS M ORDER BY FTCode "
                m_DbDtItemCode = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER).Copy
                ogcItemCode.DataSource = m_DbDtItemCode
            End If

            If otpColorCode.PageVisible Then
                _Qry = "  SELECT FTCode,FTName  FROM (SELECT  TOP 0  '' AS FTCode  ,'' AS FTName  )AS M ORDER BY FTCode "
                m_DbDtColorCode = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER).Copy
                ogcColorCode.DataSource = m_DbDtColorCode
            End If

            If otpSupl.PageVisible Then
                _Qry = "  SELECT FTCode ,FTName FROM (SELECT  TOP 0  '' AS FTCode  ,'' AS FTName  )AS M ORDER BY FTCode "
                m_DbDtSupl = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER).Copy
                ogcSupl.DataSource = m_DbDtSupl
            End If

            If otpSizeCode.PageVisible Then
                _Qry = "  SELECT FTCode,FTName  FROM (SELECT  TOP 0  '' AS FTCode  ,'' AS FTName  )AS M ORDER BY FTCode "
                m_DbDtSizeCode = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER).Copy
                ogcSizeCode.DataSource = m_DbDtSizeCode
            End If

            If otpWhNo.PageVisible Then
                _Qry = "  SELECT FTCode,FTName  FROM (SELECT  TOP 0  '' AS FTCode  ,'' AS FTName  )AS M ORDER BY FTCode "
                m_DbDtWHNo = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER).Copy
                ogcWHNo.DataSource = m_DbDtWHNo
            End If


            If otpUser.PageVisible Then
                _Qry = "  SELECT FTCode,FTName  FROM (SELECT  TOP 0  '' AS FTCode ,'' AS FTName  )AS M ORDER BY FTCode "
                m_DbDtUser = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER).Copy
                ogcUserBy.DataSource = m_DbDtUser
            End If

        Catch ex As Exception

        End Try

    End Sub

    Public Function GetCriteria() As String
        Dim _Criteria As String = ""
        Dim tText As String = ""
        '*********Ware House*********
        If (Me.otpWhNo.PageVisible) Then
            Select Case Me.FNWHCon.SelectedIndex
                Case 1
                    If Me.FNHSysWHId.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {TCNMWarehouse.FTWHCode} >='" & HI.UL.ULF.rpQuoted(Me.FNHSysWHId.Text) & "' "
                    End If

                    If Me.FNHSysWHIdTo.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {TCNMWarehouse.FTWHCode} <='" & HI.UL.ULF.rpQuoted(Me.FNHSysWHIdTo.Text) & "' "
                    End If

                Case 2

                    tText = ""

                    For Each oRow As DataRow In Me.DbDtWHNo.Rows
                        tText &= oRow("FTCode") & "|"
                    Next

                    If tText.Trim <> "" Then
                        tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {TCNMWarehouse.FTWHCode} IN['" & tText.Replace("|", "','") & "'] "
                    End If
            End Select

        End If

        '********Supl*********
        If (Me.otpSupl.PageVisible) Then
            Select Case Me.FNSuplCon.SelectedIndex
                Case 1
                    If Me.FNHSysSuplId.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {TCNMSupplier.FTSuplCode} >='" & HI.UL.ULF.rpQuoted(Me.FNHSysSuplId.Text) & "' "
                    End If

                    If Me.FNHSysSuplIdTo.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {TCNMSupplier.FTSuplCode} <='" & HI.UL.ULF.rpQuoted(Me.FNHSysSuplIdTo.Text) & "' "
                    End If
                Case 2
                    tText = ""

                    For Each oRow As DataRow In Me.DbDtSupl.Rows
                        tText &= oRow("FTCode") & "|"
                    Next

                    If tText.Trim <> "" Then
                        tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {TCNMSupplier.FTSuplCode} IN['" & tText.Replace("|", "','") & "'] "
                    End If
            End Select
        End If

        '*******Item********
        If (Me.otpItemCode.PageVisible) Then
            Select Case Me.FNItemCon.SelectedIndex
                Case 1
                    If Me.FNHSysRawMatId.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {TINVENMMaterial.FTRawMatCode} >='" & HI.UL.ULF.rpQuoted(Me.FNHSysRawMatId.Text) & "' "
                    End If

                    If Me.FNHSysRawMatIdTo.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {TINVENMMaterial.FTRawMatCode} <='" & HI.UL.ULF.rpQuoted(Me.FNHSysRawMatIdTo.Text) & "' "
                    End If

                Case 2
                    tText = ""

                    For Each oRow As DataRow In Me.DbDtItemCode.Rows
                        tText &= oRow("FTCode") & "|"
                    Next

                    If tText.Trim <> "" Then
                        tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {TINVENMMaterial.FTRawMatCode} IN['" & tText.Replace("|", "','") & "'] "
                    End If
            End Select
        End If

        '******Color*********
        If (Me.otpColorCode.PageVisible) Then
            Select Case Me.FNColorCon.SelectedIndex
                Case 1
                    If Me.FNHSysRawMatColorId.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {TINVENMMatColor.FTRawMatColorCode} >='" & HI.UL.ULF.rpQuoted(Me.FNHSysRawMatColorId.Text) & "' "
                    End If

                    If Me.FNHSysRawMatColorIdTo.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {TINVENMMatColor.FTRawMatColorCode} <='" & HI.UL.ULF.rpQuoted(Me.FNHSysRawMatColorIdTo.Text) & "' "
                    End If
                Case 2
                    tText = ""

                    For Each oRow As DataRow In Me.DbDtColorCode.Rows
                        tText &= oRow("FTCode") & "|"
                    Next

                    If tText.Trim <> "" Then
                        tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {TINVENMMatColor.FTRawMatColorCode} IN['" & tText.Replace("|", "','") & "'] "
                    End If
            End Select
        End If

        '******Size***********
        If (Me.otpSizeCode.PageVisible) Then
            Select Case Me.FNSizeCon.SelectedIndex
                Case 1
                    If Me.FNHSysRawMatSizeId.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {TINVENMMatSize.FTRawMatSizeCode} >='" & HI.UL.ULF.rpQuoted(Me.FNHSysRawMatSizeId.Text) & "' "
                    End If

                    If Me.FNHSysRawMatSizeIdTo.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {TINVENMMatSize.FTRawMatSizeCode} <='" & HI.UL.ULF.rpQuoted(Me.FNHSysRawMatSizeIdTo.Text) & "' "
                    End If
                Case 2
                    tText = ""

                    For Each oRow As DataRow In Me.DbDtSizeCode.Rows
                        tText &= oRow("FTCode") & "|"
                    Next

                    If tText.Trim <> "" Then
                        tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {TINVENMMatSize.FTRawMatSizeCode} IN['" & tText.Replace("|", "','") & "'] "
                    End If

            End Select
        End If

        '******User******
        'If (Me.otpUser.PageVisible) Then
        '    Select Case Me.FNUserCon.SelectedIndex
        '        Case 1

        '        Case 2
        '            tText = ""

        '            For Each oRow As DataRow In Me.DbDtUser.Rows
        '                tText &= oRow("FTCode") & "|"
        '            Next

        '            If tText.Trim <> "" Then
        '                tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
        '                _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
        '                _Criteria &= "  {TINVENMMatSize.FTRawMatSizeCode} IN['" & tText.Replace("|", "','") & "'] "
        '            End If
        '    End Select
        'End If


        Return _Criteria
    End Function

End Class
