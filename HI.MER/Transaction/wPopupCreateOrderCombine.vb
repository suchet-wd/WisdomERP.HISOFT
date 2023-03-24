Imports System.Drawing
Imports System.IO

Public Class wPopupCreateOrderCombine
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_MERCHAN
    Private _ProcLoad As Boolean = False

    Private _OrderNo As DataTable
    Public Property OrderNo As DataTable
        Get
            Return _OrderNo
        End Get
        Set(value As DataTable)
            _OrderNo = value
        End Set
    End Property

    Private _SubOrderNo As DataTable
    Public Property SubOrderNo As DataTable
        Get
            Return _SubOrderNo
        End Get
        Set(value As DataTable)
            _SubOrderNo = value
        End Set
    End Property

    Private _State As Boolean = False
    Public Property State As Boolean
        Get
            Return _State
        End Get
        Set(value As Boolean)
            _State = value
        End Set
    End Property

    Private Sub wPopupCreateOrderCombine_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            'Me.ogbImageOrder.Visible = Not (_OrderNo Is Nothing)
            Call _LoadMainOrderInfo(_OrderNo)
            Call _LoadSubOrderInfo(_SubOrderNo)
        Catch ex As Exception
        End Try
    End Sub

    Private Function _LoadMainOrderInfo(ByVal pObj As Object) As Boolean
        Try
            _ProcLoad = True
            Dim _Dt As DataTable
            Dim _Cmd As String

            '_Cmd = "Select  FTOrderNo, FDOrderDate, FTOrderBy, FNOrderType, FNHSysCmpId, FNHSysCmpRunId, FNHSysStyleId, FTPORef, "
            '_Cmd &= vbCrLf & "FNHSysCustId, FNHSysAgencyId, FNHSysProdTypeId, FNHSysBuyerId, FTMainMaterial, FTCombination, FTRemark, FTStateOrderApp, FTAppBy, FDAppDate, FTAppTime, FNJobState, FTStateBy, FDStateDate, "
            '_Cmd &= vbCrLf & " FTStateTime, FTImage1, FTImage2, FTImage3, FTImage4, FNHSysBrandId, FNHSysBuyId, FTCancelAppBy, FDCancelAppDate, FDCancelAppTime, FTCancelAppRemark, FTPOTradingCo, FTPOItem, "
            '_Cmd &= vbCrLf & " FTPOCreateDate, FNHSysMerTeamId, FNHSysPlantId, FNHSysBuyGrpId, FNHSysMainCategoryId, FNHSysVenderPramId, FTOrderCreateStatus, FTImportUser, FDImportDate, FTImportTime, FPOrderImage1, "
            '_Cmd &= vbCrLf & "  FPOrderImage2, FPOrderImage3, FPOrderImage4, FNHSysSeasonId, FDDateChangeOrderImage1, FTTimeChangeOrderImage1, FTUserChangeOrderImage1, FDDateChangeOrderImage2, "
            '_Cmd &= vbCrLf & "  FTTimeChangeOrderImage2, FTUserChangeOrderImage2, FDDateChangeOrderImage3, FTTimeChangeOrderImage3, FTUserChangeOrderImage3, FDDateChangeOrderImage4, FTTimeChangeOrderImage4, "
            '_Cmd &= vbCrLf & " FTUserChangeOrderImage4, FTOrderNoRef, FTStateSendDirectorApp, FTStateSendDirectorBy, FDStateSendDirectorDate, FTStateSendDirectorTime, FTStateDirectorApp, FTStateDirectorAppBy, "
            '_Cmd &= vbCrLf & " FDStateDirectorAppDate, FTStateDirectorAppTime, FTStateDirectorReject, FTStateDirectorRejectBy, FDStateDirectorRejectDate, FTStateDirectorRejectTime, FTStateFactoryApp, FTStateFactoryAppBy, "
            '_Cmd &= vbCrLf & "  FDStateFactoryAppDate, FTStateFactoryAppTime, FTStateFactoryReject, FTStateFactoryRejectBy, FDStateFactoryRejectDate, FTStateFactoryRejectTime, FTChangeCmpBy, FDChangeCmpDate, FTChangeCmpTime,"
            '_Cmd &= vbCrLf & "FNHSysStyleIdPull "
            '_Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder WITH(NOLOCK) "
            '_Cmd &= vbCrLf & " WHERE FTOrderNo='" & pObj & "' "
            _Dt = pObj

            Dim _FieldName As String = ""
            For Each R As DataRow In _Dt.Rows
                For Each Col As DataColumn In _Dt.Columns
                    _FieldName = Col.ColumnName.ToString
                    For Each Obj As Object In Me.Controls.Find(_FieldName, True)

                        Select Case Obj.GetType.FullName.ToString.ToUpper
                            Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                    'If _FieldName.ToUpper <> FTOrderNo.Name.ToString.ToUpper Then
                                    .Text = R.Item(Col).ToString()
                                    'End If
                                End With
                            Case "DevExpress.XtraEditors.CalcEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                    .Value = Val(R.Item(Col).ToString)
                                End With
                            Case "DevExpress.XtraEditors.ComboBoxEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                    Try
                                        .SelectedIndex = Val(R.Item(Col).ToString)
                                    Catch ex As Exception
                                        .SelectedIndex = -1
                                    End Try
                                End With
                            Case "DevExpress.XtraEditors.CheckEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                    .EditValue = (Integer.Parse(Val(R.Item(Col).ToString))).ToString
                                End With
                            Case "DevExpress.XtraEditors.MemoEdit".ToUpper, "DevExpress.XtraEditors.TextEdit".ToUpper
                                If _FieldName.ToUpper() = "FDUPDDATE" Then
                                    Obj.Text = HI.UL.ULDate.ConvertEN(R.Item(Col).ToString())
                                ElseIf _FieldName.ToUpper() = "FTMAINMATERIAL" Or _FieldName.ToUpper() = "FTCOMBINATION" Then
                                    Obj.Text = ""
                                Else
                                    Obj.Text = R.Item(Col).ToString
                                End If
                            Case "DevExpress.XtraEditors.PictureEdit".ToUpper
                                REM 2014/12/09
                                'With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                '    Try
                                '        REM 2014/03/14 .Image = HI.UL.ULImage.LoadImage("" & .Properties.Tag.ToString & R.Item(Col).ToString)
                                '        .Image = HI.UL.ULImage.LoadImage("" & _SystemFilePath & "\" & R.Item(Col).ToString)
                                '    Catch ex As Exception
                                '        .Image = Nothing
                                '    End Try
                                'End With

                            Case "DevExpress.XtraEditors.DateEdit".ToUpper
                                Try
                                    With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                        .DateTime = HI.UL.ULDate.ConvertEnDB(R.Item(Col).ToString)
                                    End With
                                Catch ex As Exception
                                    With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                        .Text = ""
                                    End With
                                End Try
                            Case Else
                                Obj.Text = R.Item(Col).ToString
                        End Select
                    Next
                Next
                Exit For
            Next

            '_Cmd = "SELECT TOP 1 A.FPOrderImage1, A.FPOrderImage2, A.FPOrderImage3, A.FPOrderImage4"
            '_Cmd &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS A (NOLOCK)"
            '_Cmd &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(pObj) & "';"

            Dim tmpDTFPOrderImage As System.Data.DataTable
            tmpDTFPOrderImage = pObj 'HI.Conn.SQLConn.GetDataTable(_Cmd, HI.Conn.DB.DataBaseName.DB_MERCHAN)
            If Not DBNull.Value.Equals(tmpDTFPOrderImage) AndAlso tmpDTFPOrderImage.Rows.Count > 0 Then
                For Each oDataRowOrderImage As System.Data.DataRow In tmpDTFPOrderImage.Rows
                    For Each oDataColumnOrderImage As System.Data.DataColumn In tmpDTFPOrderImage.Columns
                        Select Case True
                            Case oDataColumnOrderImage.ColumnName.ToString.ToUpper = "FPORDERIMAGE1"
                                For Each Obj As Object In Me.Controls.Find("FTImage1", True)

                                    If Not DBNull.Value.Equals(oDataRowOrderImage!FPOrderImage1) Then
                                        Dim oArrayOrderImage() As Byte = CType(oDataRowOrderImage!FPOrderImage1, Byte())

                                        If Not oArrayOrderImage Is Nothing And oArrayOrderImage.Length > 0 Then
                                            Dim ms As New MemoryStream(oArrayOrderImage)
                                            With Obj
                                                .Image = Image.FromStream(ms)
                                            End With
                                        Else
                                            Obj.Image = Nothing
                                        End If
                                    Else
                                        Obj.Image = Nothing
                                    End If
                                Next
                            Case oDataColumnOrderImage.ColumnName.ToString.ToUpper = "FPORDERIMAGE2"
                                For Each Obj As Object In Me.Controls.Find("FTImage2", True)

                                    If Not DBNull.Value.Equals(oDataRowOrderImage!FPOrderImage2) Then
                                        Dim oArrayOrderImage() As Byte = CType(oDataRowOrderImage!FPOrderImage2, Byte())
                                        If Not oArrayOrderImage Is Nothing And oArrayOrderImage.Length > 0 Then
                                            Dim ms As New MemoryStream(oArrayOrderImage)
                                            With Obj
                                                .Image = Image.FromStream(ms)
                                            End With
                                        Else
                                            Obj.Image = Nothing
                                        End If
                                    Else
                                        Obj.Image = Nothing
                                    End If
                                Next
                            Case oDataColumnOrderImage.ColumnName.ToString.ToUpper = "FPORDERIMAGE3"
                                For Each Obj As Object In Me.Controls.Find("FTImage3", True)
                                    If Not DBNull.Value.Equals(oDataRowOrderImage!FPOrderImage3) Then
                                        Dim oArrayOrderImage() As Byte = CType(oDataRowOrderImage!FPOrderImage3, Byte())
                                        If Not oArrayOrderImage Is Nothing And oArrayOrderImage.Length > 0 Then
                                            Dim ms As New MemoryStream(oArrayOrderImage)
                                            With Obj
                                                .Image = Image.FromStream(ms)
                                            End With
                                        Else
                                            Obj.Image = Nothing
                                        End If
                                    Else
                                        Obj.Image = Nothing
                                    End If
                                Next
                            Case oDataColumnOrderImage.ColumnName.ToString.ToUpper = "FPORDERIMAGE4"
                                For Each Obj As Object In Me.Controls.Find("FTImage4", True)
                                    If Not DBNull.Value.Equals(oDataRowOrderImage!FPOrderImage4) Then
                                        Dim oArrayOrderImage() As Byte = CType(oDataRowOrderImage!FPOrderImage4, Byte())
                                        If Not oArrayOrderImage Is Nothing And oArrayOrderImage.Length > 0 Then
                                            Dim ms As New MemoryStream(oArrayOrderImage)
                                            With Obj
                                                .Image = Image.FromStream(ms)
                                            End With
                                        Else
                                            Obj.Image = Nothing
                                        End If
                                    Else
                                        Obj.Image = Nothing
                                    End If
                                Next
                            Case Else
                                '...Nothing
                        End Select
                    Next
                    Exit For
                Next
            Else
                Me.FTImage1.Image = Nothing
                Me.FTImage2.Image = Nothing
                Me.FTImage3.Image = Nothing
                Me.FTImage4.Image = Nothing
            End If
            If Not tmpDTFPOrderImage Is Nothing Then tmpDTFPOrderImage.Dispose()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function _LoadSubOrderInfo(_oDt As DataTable) As Boolean
        Try
            _ProcLoad = True
            Dim _Dt As DataTable
            _Dt = _oDt
            Dim _FieldName As String = ""
            For Each R As DataRow In _Dt.Rows
                For Each Col As DataColumn In _Dt.Columns
                    _FieldName = Col.ColumnName.ToString
                    For Each Obj As Object In Me.Controls.Find(_FieldName, True)

                        Select Case Obj.GetType.FullName.ToString.ToUpper
                            Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                    If _FieldName.ToUpper <> FTOrderNo.Name.ToString.ToUpper Then
                                        .Text = R.Item(Col).ToString()
                                    End If
                                End With
                            Case "DevExpress.XtraEditors.CalcEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                    .Value = Val(R.Item(Col).ToString)
                                End With
                            Case "DevExpress.XtraEditors.ComboBoxEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                    Try
                                        .SelectedIndex = Val(R.Item(Col).ToString)
                                    Catch ex As Exception
                                        .SelectedIndex = -1
                                    End Try
                                End With
                            Case "DevExpress.XtraEditors.CheckEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                    .EditValue = (Integer.Parse(Val(R.Item(Col).ToString))).ToString
                                End With
                            Case "DevExpress.XtraEditors.MemoEdit".ToUpper, "DevExpress.XtraEditors.TextEdit".ToUpper
                                If _FieldName.ToUpper() = "FDUPDDATE" Then
                                    Obj.Text = HI.UL.ULDate.ConvertEN(R.Item(Col).ToString())
                                ElseIf _FieldName.ToUpper() = "FTMAINMATERIAL" Or _FieldName.ToUpper() = "FTCOMBINATION" Then
                                    Obj.Text = ""
                                ElseIf _FieldName.ToString = "FTPOREF" Then
                                    If R.Item(Col).ToString <> "" Then
                                        Obj.Text = R.Item(Col).ToString
                                    End If
                                Else
                                    Obj.Text = R.Item(Col).ToString
                                End If
                            Case "DevExpress.XtraEditors.PictureEdit".ToUpper
                                REM 2014/12/09
                                'With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                '    Try
                                '        REM 2014/03/14 .Image = HI.UL.ULImage.LoadImage("" & .Properties.Tag.ToString & R.Item(Col).ToString)
                                '        .Image = HI.UL.ULImage.LoadImage("" & _SystemFilePath & "\" & R.Item(Col).ToString)
                                '    Catch ex As Exception
                                '        .Image = Nothing
                                '    End Try
                                'End With

                            Case "DevExpress.XtraEditors.DateEdit".ToUpper
                                Try
                                    With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                        .DateTime = HI.UL.ULDate.ConvertEnDB(R.Item(Col).ToString)
                                    End With
                                Catch ex As Exception
                                    With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                        .Text = ""
                                    End With
                                End Try
                            Case Else
                                Obj.Text = R.Item(Col).ToString
                        End Select
                    Next
                Next
                Exit For
            Next


            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ValidateSubOrderNoInfo() As Boolean
        Dim bValidate As Boolean = False

        Try
            If Me.FDSubOrderDate.Text <> "" Then
                If Me.FDProDate.Text <> "" Then
                    If Me.FDShipDate.Text <> "" Then
                        If Me.FNHSysContinentId.Text <> "" Then
                            If Me.FNHSysCountryId.Text <> "" Then

                                If Me.FNHSysShipModeId.Text <> "" Then
                                    If Me.FNHSysShipPortId.Text <> "" Then
                                        If Me.FNHSysCurId.Text <> "" Then
                                            If Me.FNHSysGenderId.Text <> "" Then

                                                If Me.FNHSysPlantId.Text <> "" Then
                                                    If Me.FNHSysBuyGrpId.Text <> "" Then
                                                        If Me.FNHSysUnitId.Text <> "" Then

                                                            '...validate breakdown confirm divert xxx
                                                            '==============================================================================================================================================================================================
                                                            Dim numSumQuantityDivert As Integer

                                                            numSumQuantityDivert = 0




                                                            bValidate = True


                                                            '==============================================================================================================================================================================================

                                                        Else
                                                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysUnitId_lbl.Text)
                                                            Me.FNHSysUnitId.Focus()
                                                        End If
                                                    Else
                                                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysBuyGrpId_lbl.Text)
                                                        Me.FNHSysBuyGrpId.Focus()
                                                    End If
                                                Else
                                                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysPlantId_lbl.Text)
                                                    Me.FNHSysPlantId.Focus()
                                                End If


                                            Else
                                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysGenderId_lbl.Text)
                                                Me.FNHSysGenderId.Focus()
                                            End If
                                        Else
                                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysCurId_lbl.Text)
                                            Me.FNHSysCurId.Focus()
                                        End If
                                    Else
                                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysShipPortId_lbl.Text)
                                        Me.FNHSysShipPortId.Focus()
                                    End If
                                Else
                                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysShipModeId_lbl.Text)
                                    Me.FNHSysShipModeId.Focus()
                                End If

                            Else
                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysCountryId_lbl.Text)
                                Me.FNHSysCountryId.Focus()
                            End If
                        Else
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysContinentId_lbl.Text)
                            Me.FNHSysContinentId.Focus()
                        End If


                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FDShipDate_lbl.Text)
                        Me.FDShipDate.Focus()
                    End If

                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FDProDate_lbl.Text)
                    Me.FDProDate.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FDSubOrderDate_lbl.Text)
                Me.FDSubOrderDate.Focus()
            End If


        Catch ex As Exception

        End Try

        Return bValidate

    End Function


    Private Sub osave_Click(sender As Object, e As EventArgs) Handles osave.Click
        Try
            If Me.ogrpOrderFactory.Visible = True Then
                If Me.FTPORef.Text.Trim() = "" Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPORef_lbl.Text)
                    FTPORef.Focus()
                    Exit Sub
                End If
            End If

            If ValidateSubOrderNoInfo() Then
                State = True
                Me.Close()
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub oclose_Click(sender As Object, e As EventArgs) Handles oclose.Click
        Try
            _State = False
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNHSysPlantId_lbl_Click(sender As Object, e As EventArgs) Handles FNHSysPlantId_lbl.Click

    End Sub
End Class