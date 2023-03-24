Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns

Public Class wEfkaCycleTimeTracking

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#Region "Initial Grid"

#End Region

#Region "Custom summaries"

    Private totalSum As Integer = 0
    Private GrpSum As Integer = 0

    Private Sub InitStartValue()
        totalSum = 0
        GrpSum = 0
    End Sub

    'Private Sub ogv_CustomDrawGroupRow(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs) Handles ogv.CustomDrawGroupRow

    '    Dim info As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo = e.Info
    '    Dim Handle As Integer = ogv.GetDataRowHandleByGroupRowHandle(e.RowHandle)

    '    'Select Case info.Column.FieldName.ToString.ToUpper
    '    '    Case "FNWorkingDay"

    '    Dim GrpDisplayText As String = ogv.GetGroupSummaryText(e.RowHandle)  'ogv.GetGroupRowValue(e.RowHandle, info.Column)
    '    Dim GrpDisplayTextReplace As String = Nothing
    '    Dim GrpDisplayTextReplaceNew As String = Nothing
    '    GrpDisplayTextReplace = GrpDisplayText.Split(")")(1)

    '    If GrpDisplayTextReplace <> "" Then
    '        If GrpDisplayTextReplace.Split("=").Length >= 2 Then
    '            Dim Title1 As String = GrpDisplayTextReplace.Split("=")(0)
    '            Dim Title2 As String = GrpDisplayTextReplace.Split("=")(1)

    '            If IsNumeric(Title2) = False Then
    '                Title2 = "0"
    '            End If
    '            Dim _Sum As Integer = CDbl(Title2)
    '            Dim NetDisplay As String = ""
    '            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
    '                NetDisplay = Format((_Sum \ 480), "00") & " วัน : " & Format(((_Sum Mod 480) \ 60), "00") & " ชั่วโมง : " & Format(((_Sum Mod 480) Mod 60), "00") & " นาที"
    '            Else
    '                NetDisplay = Format((_Sum \ 480), "00") & " Day : " & Format(((_Sum Mod 480) \ 60), "00") & " Hour : " & Format(((_Sum Mod 480) Mod 60), "00") & " Minute"
    '            End If

    '            GrpDisplayTextReplaceNew = Title1 & "=" & NetDisplay
    '            GrpDisplayText = GrpDisplayText.Replace(GrpDisplayTextReplace, GrpDisplayTextReplaceNew)
    '        End If


    '    info.GroupText = info.Column.Caption + ":" + info.GroupValueText + ""
    '    info.GroupText += "" + GrpDisplayText + ""

    '    'End Select

    'End Sub

#End Region

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

        ogdtime.DataSource = Nothing

        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _dt As DataTable
        Dim _TotalRow As Integer = 0
        Dim _Rx As Integer = 0

        StateCal = False

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Try


            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_DATA_EFKA_DETAIL '" & HI.UL.ULDate.ConvertEnDB(FTStartEfkaDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTStartEfkaDate.Text) & "' "


            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            Me.ogdtime.DataSource = _dt
            ogvtime.BestFitColumns()
        Catch ex As Exception
        End Try

        _Spls.Close()
        _RowDataChange = False

    End Sub


    Private Sub InitialGridMergCell()

        'For Each c As GridColumn In ogvtime.Columns

        '    Select Case c.FieldName.ToString
        '        Case "FNRcvQuantity", "FTPositionPartName", "FTMatColorCode", "FTRawMatColorName"
        '            c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
        '        Case Else
        '            c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
        '            c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        '    End Select

        'Next

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False


        If Me.FTStartEfkaDate.Text <> "" Then
            _Pass = True
        End If

        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function
#End Region

#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try




            For Each col As DevExpress.XtraGrid.Columns.GridColumn In ogvtime.Columns
                col.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
            Next

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



    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ogvtime_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvtime.CellMerge
        Try
            With Me.ogvtime


                Select Case e.Column.FieldName
                    Case "FTAssetCode", "FTAssetName", "FNTotalTimeAVG"

                        If "" & .GetRowCellValue(e.RowHandle1, "FTUnitSectCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTUnitSectCode").ToString _
                            And "" & .GetRowCellValue(e.RowHandle1, "FTStyleCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTStyleCode").ToString _
                             And "" & .GetRowCellValue(e.RowHandle1, "FNSeq").ToString = "" & .GetRowCellValue(e.RowHandle2, "FNSeq").ToString _
                            And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap

                        Else

                            e.Merge = False
                            e.Handled = True

                        End If
                    Case "FNSeq", "FTOperationName"

                        If "" & .GetRowCellValue(e.RowHandle1, "FTUnitSectCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTUnitSectCode").ToString _
                          And "" & .GetRowCellValue(e.RowHandle1, "FTStyleCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTStyleCode").ToString _
                          And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap

                        Else

                            e.Merge = False
                            e.Handled = True

                        End If

                    Case "FTUnitSectCode", "FTStyleCode"
                        e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                        e.Handled = True
                        e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                    Case Else
                        e.Merge = False
                        e.Handled = True
                End Select

            End With

        Catch ex As Exception

        End Try
    End Sub



    'Private Sub ogvtime_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvtime.RowCellStyle
    '    Try
    '        With Me.ogvtime
    '            Select Case e.Column.FieldName
    '                Case "FNCutQuantity"
    '                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNCutQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNGrandQuantity")) Then
    '                        e.Appearance.BackColor = Drawing.Color.OrangeRed
    '                    Else
    '                        e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                    End If
    '                Case "FNRcvSuplQuantity"
    '                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSendSuplQuantity")) > 0 Then
    '                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNRcvSuplQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNSendSuplQuantity")) Then
    '                            e.Appearance.BackColor = Drawing.Color.OrangeRed
    '                        Else
    '                            e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                        End If
    '                    End If

    '                Case "FNSPMKQuantity"
    '                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNCutQuantity")) > 0 Then
    '                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSPMKQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNCutQuantity")) Then
    '                            e.Appearance.BackColor = Drawing.Color.OrangeRed
    '                        Else
    '                            e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                        End If
    '                    End If
    '                Case "FNSewQuantity"
    '                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSPMKQuantity")) > 0 Then
    '                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNSPMKQuantity")) Then
    '                            e.Appearance.BackColor = Drawing.Color.OrangeRed
    '                        Else
    '                            e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                        End If
    '                    End If

    '                Case "FNSewOutQuantity"
    '                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewQuantity")) > 0 Then
    '                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewOutQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewQuantity")) Then
    '                            e.Appearance.BackColor = Drawing.Color.OrangeRed
    '                        Else
    '                            e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                        End If
    '                    End If

    '                Case "FNPackQuantity"
    '                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewOutQuantity")) > 0 Then
    '                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNPackQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewOutQuantity")) Then
    '                            e.Appearance.BackColor = Drawing.Color.OrangeRed

    '                        Else
    '                            e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                        End If
    '                    End If
    '                Case "FNBalCutQuantity", "FNBalSuplQuantity", "FNBalSewQuantity", "FNBalPackQuantity", "FNCutBalQuantity"
    '                    If Double.Parse(.GetRowCellValue(e.RowHandle, e.Column.FieldName)) > 0 Then
    '                        e.Appearance.BackColor = Drawing.Color.OrangeRed
    '                    Else

    '                        Select Case e.Column.FieldName
    '                            Case "FNBalCutQuantity"
    '                                If Double.Parse(.GetRowCellValue(e.RowHandle, "FNCutQuantity")) > 0 Then
    '                                    e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                                End If
    '                            Case "FNBalSuplQuantity"
    '                                If Double.Parse(.GetRowCellValue(e.RowHandle, "FNRcvSuplQuantity")) > 0 Then
    '                                    e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                                End If
    '                            Case "FNBalSewQuantity"
    '                                If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewQuantity")) > 0 Then
    '                                    e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                                End If
    '                            Case "FNBalPackQuantity"
    '                                If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewOutQuantity")) > 0 Then
    '                                    e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                                End If
    '                            Case "FNCutBalQuantity"
    '                                If Double.Parse(.GetRowCellValue(e.RowHandle, "FNGrandQuantity")) > 0 Then
    '                                    e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                                End If
    '                        End Select

    '                    End If

    '            End Select
    '        End With
    '    Catch ex As Exception

    '    End Try
    'End Sub



End Class