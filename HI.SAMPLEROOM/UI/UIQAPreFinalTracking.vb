Imports System.Windows.Forms
Imports System.Drawing

Public Class UIQAPreFinalTracking

    Sub New(oFNHSysEmpID As Integer, odt As DataTable, MonthYear As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.EmpData = odt
        Me.FNHSysEmpID = oFNHSysEmpID
        Me.FTMonthYear = MonthYear

        Call CreateDatatable(odt)
        Call GenerateGridBand()

        Dim _Qry As String = ""

        _Qry = " SELECT TOP 1 FNTheQuality"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPreFinalTheQuality AS WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE FTMonthYear='" & HI.UL.ULDate.ConvertEnDB("01/" & Me.FTMonthYear) & "'"
        _Qry &= vbCrLf & " AND FNHSysEmpID=" & Integer.Parse(Val(Me.FNHSysEmpID)) & ""

        With Me.FNTheQuality
            .Properties.AppearanceFocused.ForeColor = Color.Blue
            .Properties.AppearanceFocused.BackColor = Color.GreenYellow

            .Properties.AppearanceReadOnly.BackColor = Color.LightCyan
            .Properties.AppearanceReadOnly.ForeColor = Color.Blue

            .Properties.AppearanceDisabled.BackColor = Color.LightCyan
            .Properties.AppearanceDisabled.ForeColor = Color.Blue



            .Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            .Properties.DisplayFormat.FormatString = "N" & .Properties.Precision.ToString

            AddHandler .Leave, AddressOf HI.TL.HandlerControl.CalEdit_Leave
            AddHandler .Spin, AddressOf HI.TL.HandlerControl.Caledit_Spin

            .EnterMoveNextControl = True
        End With


        FNTheQuality.Value = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "0"))

    End Sub

#Region "Procedure"

    Private _EmpData As DataTable = Nothing
    Private Property EmpData As DataTable
        Get
            Return _EmpData
        End Get
        Set(value As DataTable)
            _EmpData = value
        End Set
    End Property

    Private _GridData As DataTable = Nothing
    Private Property GridData As DataTable
        Get
            Return _GridData
        End Get
        Set(value As DataTable)
            _GridData = value
        End Set
    End Property

    Private _FNHSysEmpID As Integer = 0
    Private Property FNHSysEmpID As Integer
        Get
            Return _FNHSysEmpID
        End Get
        Set(value As Integer)
            _FNHSysEmpID = value
        End Set
    End Property

    Private _FTMonthYear As String = ""
    Private Property FTMonthYear As String
        Get
            Return _FTMonthYear
        End Get
        Set(value As String)
            _FTMonthYear = value
        End Set
    End Property

    Private Sub CreateDatatable(dt As DataTable)
        Dim _dt As New DataTable
        With _dt
            _dt.Columns.Add("FTQADate", GetType(String))
            _dt.Columns.Add("FTPORef", GetType(String))
            _dt.Columns.Add("FNQAInQty", GetType(Integer))
            _dt.Columns.Add("FNQAActualQty", GetType(Integer))
            _dt.Columns.Add("FNDefect", GetType(Integer))
            _dt.Columns.Add("FTStyleCode", GetType(String))
            _dt.Columns.Add("FTColorway", GetType(String))
            _dt.Columns.Add("FTCustRef", GetType(String))
            _dt.Columns.Add("FTSubOrderNo", GetType(String))
            _dt.Columns.Add("FTDestination", GetType(String))
            _dt.Columns.Add("FTDefectDetail", GetType(String))
            _dt.Columns.Add("FTStatePreFinal", GetType(String))
        End With

        Dim _StrFilter As String = ""

        If Not (dt Is Nothing) Then
            For Each R As DataRow In dt.Select("FNHSysEmpID=" & Me.FNHSysEmpID & "", "FTQATypeCode,FTQADetailCode")
                _StrFilter = "FTQADate='" & R!FTQADate.ToString & "' AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"

                If _dt.Select(_StrFilter).Length <= 0 Then
                    _dt.Rows.Add(R!FTQADate.ToString, R!FTPORef.ToString, R!FNQAInQty.ToString, R!FNQAActualQty.ToString, R!FNDefect.ToString, R!FTStyleCode.ToString, R!FTColorway.ToString, R!FTCustRef.ToString, R!FTSubOrderNo.ToString, R!FTDestination.ToString, R!FTDefectDetail.ToString, R!FTStatePreFinal.ToString)
                End If

                If _dt.Columns.IndexOf("C" & R!FNHSysQADetailId.ToString) < 0 Then
                    _dt.Columns.Add("C" & R!FNHSysQADetailId.ToString, GetType(Integer))
                End If

                If Val(R!FNStateQA.ToString) > 0 Then
                    For Each Rx As DataRow In _dt.Select(_StrFilter)
                        Rx.Item("C" & R!FNHSysQADetailId.ToString) = Val(Rx.Item("C" & R!FNHSysQADetailId.ToString).ToString) + Val(R!FNStateQA.ToString)
                    Next
                End If

            Next
        End If


        With Me.ogvdetail

            For Each Col As DataColumn In _dt.Columns

                Dim _BanCol As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
                With _BanCol
                    .Caption = Col.ColumnName.ToString
                    .FieldName = Col.ColumnName.ToString
                    .Name = FNHSysEmpID.ToString & "_" & Col.ColumnName.ToString
                    .OptionsColumn.AllowEdit = False
                    .OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
                    .OptionsColumn.ReadOnly = True
                    .Visible = True

                    Select Case Col.ColumnName.ToString
                        Case "FTQADate", "FTPORef", "FTStyleCode", "FTColorway"
                            .Width = 80
                        Case "FNQAInQty", "FNQAActualQty", "FNDefect"
                            .Width = 50
                        Case Else
                            If Microsoft.VisualBasic.Left(Col.ColumnName.ToString, 1) = "C" Then
                                .Width = 50
                            Else
                                If Col.ColumnName.ToString = "FTDefectDetail" Then
                                    .Width = 150
                                Else
                                    .Width = 80
                                End If
                            End If

                    End Select
                End With

                .Columns.Add(_BanCol)

            Next

        End With

        Me.GridData = _dt
    End Sub

    Private Sub GenerateGridBand()
        Try
            Dim _Qry As String = ""
            Dim _GbandIndex As Integer = 0
            With Me.ogvdetail

                For Each Str As String In "FTQADate|FTPORef|FNQAInQty|FNQAActualQty|FNDefect|FTStyleCode|FTColorway|FTCustRef|FTSubOrderNo|FTDestination".Split("|")
                    Dim _gBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                    With _gBand
                        .AppearanceHeader.Options.UseTextOptions = True
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        .Caption = Str
                        .Columns.Add(Me.ogvdetail.Columns.ColumnByFieldName(Str))
                        .Name = "gb" & FNHSysEmpID.ToString & "_" & Str
                        .RowCount = 4
                        .Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left


                    End With

                    .Bands.Add(_gBand)
                    _GbandIndex = _GbandIndex + 1
                Next

                If Not (EmpData Is Nothing) Then
                    Dim grp As List(Of String) = (EmpData.Select("FTQATypeCode<>''", "FTQATypeCode").CopyToDataTable).AsEnumerable() _
                                                      .Select(Function(r) r.Field(Of String)("FTQATypeCode")) _
                                                      .Distinct() _
                                                      .ToList()


                    Dim _StateCreateBand As Boolean = False
                    Dim _FTQAData As String = EmpData.Rows(0)!FTQADate.ToString
                    Dim _SubOrderNo As String = EmpData.Rows(0)!FTSubOrderNo.ToString
                    For Each Ind As String In grp

                        _StateCreateBand = False
                        Dim _GrbandType As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                        Dim _GrbandType2 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand

                        For Each R As DataRow In EmpData.Select("FTQADate='" & HI.UL.ULF.rpQuoted(_FTQAData) & "' AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "' AND FTQATypeCode='" & HI.UL.ULF.rpQuoted(Ind) & "'", "FTQADetailCode")

                            If _StateCreateBand = False Then

                                With _GrbandType
                                    .AppearanceHeader.Options.UseTextOptions = True
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .Caption = R!FNStateQAByType.ToString
                                    .Name = "gbt" & FNHSysEmpID.ToString & "_" & R!FNHSysQATypeId.ToString
                                    .RowCount = 1


                                End With


                                .Bands.Add(_GrbandType)

                                With _GrbandType2
                                    .AppearanceHeader.Options.UseTextOptions = True
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .Caption = R!FTQATypeCode.ToString
                                    .Name = "gbt2" & FNHSysEmpID.ToString & "_" & R!FNHSysQATypeId.ToString
                                    .RowCount = 1
                                End With

                                _GrbandType.Children.Add(_GrbandType2)

                                _StateCreateBand = True
                            End If

                            Dim _GrbandCol1 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                            Dim _GrbandCol2 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand



                            With _GrbandCol1
                                .AppearanceHeader.Options.UseTextOptions = True
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .Caption = R!FNStateQAByDetail.ToString
                                .Name = "gbcol1" & FNHSysEmpID.ToString & "_" & R!FNHSysQADetailId.ToString
                                .RowCount = 1


                            End With

                            _GrbandType2.Children.Add(_GrbandCol1)

                            With _GrbandCol2
                                .AppearanceHeader.Options.UseTextOptions = True
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .Columns.Add(Me.ogvdetail.Columns.ColumnByFieldName("C" & R!FNHSysQADetailId.ToString))
                                .Caption = R!FTQADetailCode.ToString
                                .Name = "gbcol2" & FNHSysEmpID.ToString & "_" & R!FNHSysQADetailId.ToString
                                .RowCount = 1
                                .Width = 50

                            End With

                            _GrbandCol1.Children.Add(_GrbandCol2)

                        Next

                    Next
                End If

                For Each Str As String In "FTDefectDetail|FTStatePreFinal".Split("|")
                    Dim _gBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                    With _gBand
                        .AppearanceHeader.Options.UseTextOptions = True
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        .Caption = Str
                        .Columns.Add(Me.ogvdetail.Columns.ColumnByFieldName(Str))
                        .Name = "gbl" & FNHSysEmpID.ToString & "_" & Str
                        .RowCount = 4

                        If Str = "FTDefectDetail" Then
                            .Width = 150
                        Else
                            .Width = 80
                        End If
                    End With

                    .Bands.Add(_gBand)

                Next

            End With

            Me.ogcdetail.DataSource = Me.GridData

        Catch ex As Exception

        End Try


    End Sub

#End Region

    Private Sub FNTheQuality_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles FNTheQuality.EditValueChanging
        Try
            If Me.FTMonthYear <> "" And FNHSysEmpID > 0 Then
            Else
                e.Cancel = True
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub FNTheQuality_KeyDown(sender As Object, e As KeyEventArgs) Handles FNTheQuality.KeyDown
        Select Case e.KeyCode

            Case Keys.Enter
                If Me.FTMonthYear <> "" And FNHSysEmpID > 0 Then
                    Dim _Qry As String = ""
                    _Qry = "  UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPreFinalTheQuality SET  "
                    _Qry &= vbCrLf & " FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " ,FNTheQuality=" & Me.FNTheQuality.Value & ""
                    _Qry &= vbCrLf & "  WHERE FTMonthYear='" & HI.UL.ULDate.ConvertEnDB("01/" & Me.FTMonthYear) & "'"
                    _Qry &= vbCrLf & " AND FNHSysEmpID=" & Integer.Parse(Val(Me.FNHSysEmpID)) & ""


                    If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD) = False Then
                        _Qry = "  INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPreFinalTheQuality (  "
                        _Qry &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime,  FTMonthYear, FNHSysEmpID, FNTheQuality"
                        _Qry &= vbCrLf & ") "
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & " , " & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & " , " & HI.UL.ULDate.FormatTimeDB & ""
                        _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB("01/" & Me.FTMonthYear) & "'"
                        _Qry &= vbCrLf & "," & Integer.Parse(Val(Me.FNHSysEmpID)) & ""
                        _Qry &= vbCrLf & " ," & Me.FNTheQuality.Value & ""

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)

                    End If


                End If
        End Select
    End Sub

    Private Sub FNTheQuality_KeyPress(sender As Object, e As KeyPressEventArgs) Handles FNTheQuality.KeyPress

    End Sub
End Class
