Imports System.Windows.Forms
Public Class UIQAPreFinalTrackingList

    Sub New(odt As DataTable)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.EmpData = odt
        Call CreateDatatable(odt)
        Call GenerateGridBand()
    End Sub

#Region "Procedure"

    Private _EmpData As DataTable = Nothing
    Public Property EmpData As DataTable
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

    Public Sub RefreshData()
        Call CreateDatatable(Me.EmpData)
        Call GenerateGridBand()
    End Sub

    Private Sub CreateDatatable(dt As DataTable)
        Dim _dt As New DataTable
        With _dt
            _dt.Columns.Add("FTUnitSectCode", GetType(String))
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
            _dt.Columns.Add("FNSeq", GetType(Integer))
            _dt.Columns.Add("FTSizeBreakDown", GetType(String))
        End With

        Dim _StrFilter As String = ""

        If Not (dt Is Nothing) Then

            For Each R As DataRow In dt.Select("FTQADate<>''", "FTQATypeCode,FTQADetailCode")
                _StrFilter = "FTUnitSectCode='" & HI.UL.ULF.rpQuoted(R!FTUnitSectCode) & "' AND FTQADate='" & R!FTQADate.ToString & "' AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' and FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "' and FNSeq=" & Val(R!FNSeq.ToString)
                ' _StrFilter &= "FTUnitSectCode='" & HI.UL.ULF.rpQuoted(R!FTUnitSectCode) & "' "
                If _dt.Select(_StrFilter).Length <= 0 Then
                    _dt.Rows.Add(R!FTUnitSectCode.ToString, R!FTQADate.ToString, R!FTPORef.ToString, R!FNQAInQty.ToString, R!FNQAActualQty.ToString, R!FNDefect.ToString, R!FTStyleCode.ToString, R!FTColorway.ToString, R!FTCustRef.ToString, R!FTSubOrderNo.ToString, R!FTDestination.ToString, R!FTDefectDetail.ToString, R!FTStatePreFinal.ToString, R!FNSeq.ToString, R!FTSizeBreakDown.ToString)
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
            .BeginInit()
            .Columns.Clear()
            .Bands.Clear()

            For Each Col As DataColumn In _dt.Columns

                Dim _BanCol As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
                With _BanCol
                    .Caption = Col.ColumnName.ToString
                    .FieldName = Col.ColumnName.ToString
                    .Name = Col.ColumnName.ToString
                    .OptionsColumn.AllowEdit = False
                    .OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
                    .OptionsColumn.ReadOnly = True
                    .Visible = True

                    Select Case Col.ColumnName.ToString
                        Case "FTUnitSectCode", "FTQADate", "FTPORef", "FTStyleCode", "FTColorway", "FNSeq", "FTSizeBreakDown"
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
            .EndInit()
        End With

        Me.GridData = _dt
    End Sub

    Private Sub GenerateGridBand()
        Try
            Dim _Qry As String = ""
            Dim _GbandIndex As Integer = 0
            With Me.ogvdetail
                .BeginInit()

                For Each Str As String In "FTUnitSectCode|FTQADate|FTPORef|FNSeq|FNQAInQty|FNQAActualQty|FNDefect|FTStyleCode|FTColorway|FTSizeBreakDown|FTCustRef|FTSubOrderNo".Split("|")

                    Dim _gBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand

                    With _gBand
                        .AppearanceHeader.Options.UseTextOptions = True
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        .Caption = Str
                        .Columns.Add(Me.ogvdetail.Columns.ColumnByFieldName(Str))
                        .Name = "gb" & Str
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
                    Dim _UnitSectCode As String = EmpData.Rows(0)!FTUnitSectCode.ToString

                    Dim _FTQAData As String = EmpData.Rows(0)!FTQADate.ToString
                    Dim _SubOrderNo As String = EmpData.Rows(0)!FTSubOrderNo.ToString

                    For Each Ind As String In grp

                        _StateCreateBand = False
                        Dim _GrbandType As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                        Dim _GrbandType2 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                        Dim _empdata As New DataTable
                        _empdata = EmpData.Select("FTQADate='" & HI.UL.ULF.rpQuoted(_FTQAData) & "' AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'  AND FTUnitSectCode='" & HI.UL.ULF.rpQuoted(_UnitSectCode) & "' AND FTQATypeCode='" & HI.UL.ULF.rpQuoted(Ind) & "'", "FTQATypeCode").CopyToDataTable()
                        _empdata = DirectCast(_empdata.DefaultView.ToTable(True, "FNStateQAByType", "FNHSysQATypeId", "FTQATypeCode", "FTQADetailCode", "FNStateQAByDetail", "FNHSysQADetailId"), DataTable)
                        For Each R As DataRow In _empdata.Rows ' EmpData.Select("FTQADate='" & HI.UL.ULF.rpQuoted(_FTQAData) & "' AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'  AND FTUnitSectCode='" & HI.UL.ULF.rpQuoted(_UnitSectCode) & "' AND FTQATypeCode='" & HI.UL.ULF.rpQuoted(Ind) & "'", "FTQADetailCode")

                            If _StateCreateBand = False Then

                                With _GrbandType
                                    .AppearanceHeader.Options.UseTextOptions = True
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .Caption = R!FNStateQAByType.ToString
                                    .Name = "gbt" & R!FNHSysQATypeId.ToString
                                    .RowCount = 1

                                End With

                                .Bands.Add(_GrbandType)

                                With _GrbandType2
                                    .AppearanceHeader.Options.UseTextOptions = True
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .Caption = R!FTQATypeCode.ToString
                                    .Name = "gbt2" & R!FNHSysQATypeId.ToString
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
                                .Name = "gbcol1" & R!FNHSysQADetailId.ToString
                                .RowCount = 1

                            End With

                            _GrbandType2.Children.Add(_GrbandCol1)

                            With _GrbandCol2
                                .AppearanceHeader.Options.UseTextOptions = True
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .Columns.Add(Me.ogvdetail.Columns.ColumnByFieldName("C" & R!FNHSysQADetailId.ToString))
                                .Caption = R!FTQADetailCode.ToString
                                .Name = "gbcol2" & R!FNHSysQADetailId.ToString
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
                        .Name = "gb" & Str
                        .RowCount = 4

                        If Str = "FTDefectDetail" Then
                            .Width = 150
                        Else
                            .Width = 80
                        End If
                    End With

                    .Bands.Add(_gBand)

                Next
                .EndInit()
            End With

            Me.ogcdetail.DataSource = Me.GridData

        Catch ex As Exception

        End Try


    End Sub

#End Region
End Class
