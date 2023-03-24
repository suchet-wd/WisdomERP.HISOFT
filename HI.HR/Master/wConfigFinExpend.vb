Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Base
Imports System.IO
Imports System.Drawing
Imports System
Imports System.Data

Public Class wConfigFinExpend
    Private _wConfigFinExpendPopup As wConfigFinExpendPopup

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _wConfigFinExpendPopup = New wConfigFinExpendPopup
        HI.TL.HandlerControl.AddHandlerObj(_wConfigFinExpendPopup)
        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wConfigFinExpendPopup.Name.ToString.Trim, _wConfigFinExpendPopup)
        Catch ex As Exception
        Finally
        End Try


    End Sub


    Private _FormPopup As String = ""
    Public Property FormPopup As String
        Get
            Return _FormPopup
        End Get
        Set(ByVal value As String)
            _FormPopup = value
        End Set
    End Property


    Public Sub LoadData()

        Dim _Qry As String = ""
        Dim _Dt As DataTable
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry = "SELECT FNHSysFinExpendID, FNHSysEmpID, FNHSysEmpTypeId
                    , FTEmpTypeCode, FTEmpTypeNameTH AS FTEmpTypeName
                    , FTEmpCode
                    , FTEmpNameTH + ' ' + FTEmpSurnameTH AS [FTEmpName]
                    , FTFinCode, FTFinDescTH  AS [FTFinDesc]
                    , FTPayYearBegin, FTPayTermBegin
                    , FTPayYearEnd, FTPayTermEnd
                    , FCFinAmt
                    , FCFinAmtTotal, FCFinAmtTotalSysExpend, FTStaCompletedPayment
                    FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.V_Finance_Expend "
        Else
            _Qry = "SELECT FNHSysFinExpendID, FNHSysEmpID, FNHSysEmpTypeId
                    , FTEmpTypeCode, FTEmpTypeNameEN AS FTEmpTypeName
                    , FTEmpCode
                    , FTEmpNameEN + ' ' + FTEmpSurnameEN AS [FTEmpName]
                    , FTFinCode, FTFinDescEN  AS [FTFinDesc]
                    , FTPayYearBegin, FTPayTermBegin
                    , FTPayYearEnd, FTPayTermEnd
                    , FCFinAmt
                    , FCFinAmtTotal, FCFinAmtTotalSysExpend, FTStaCompletedPayment
                    FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.V_Finance_Expend "
        End If

        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        ogcdetail.DataSource = _Dt

    End Sub

#Region "MAIN PROC"

    Private Sub Proc_AddNew(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmaddnew.Click
        Try

            With _wConfigFinExpendPopup
                .FNHSysFinExpendID = ""
                .ShowDialog()
                'LoadData()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Proc_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.LoadData()
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message, Me.Text, System.Windows.Forms.MessageBoxIcon.Error)
        End Try
    End Sub

    Private Shared Sub Gridview_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvdetail.RowCellStyle
        Try
            With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                If e.RowHandle <> .FocusedRowHandle OrElse e.Column.AbsoluteIndex = .FocusedColumn.AbsoluteIndex Then
                    If (e.Column.OptionsColumn.ReadOnly) Then
                        e.Appearance.BackColor = System.Drawing.Color.LemonChiffon
                    Else
                        e.Appearance.BackColor = System.Drawing.Color.White
                    End If
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub


    Private Sub Ogvdetail_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles ogvdetail.RowCellClick
        Try

            Dim FNHSysFinExpendID As String = ogvdetail.GetFocusedRowCellValue("FNHSysFinExpendID").ToString()
            Dim FNHSysEmpID As String = ogvdetail.GetFocusedRowCellValue("FNHSysEmpID").ToString()
            Dim FNHSysEmpTypeId As String = ogvdetail.GetFocusedRowCellValue("FNHSysEmpTypeId").ToString()
            Dim FTEmpTypeCode As String = ogvdetail.GetFocusedRowCellValue("FTEmpTypeCode").ToString()
            Dim FTEmpTypeName As String = ogvdetail.GetFocusedRowCellValue("FTEmpTypeName").ToString()
            Dim FTEmpCode As String = ogvdetail.GetFocusedRowCellValue("FTEmpCode").ToString()
            Dim FTEmpName As String = ogvdetail.GetFocusedRowCellValue("FTEmpName").ToString()
            Dim FTFinCode As String = ogvdetail.GetFocusedRowCellValue("FTFinCode").ToString()
            Dim FTFinDesc As String = ogvdetail.GetFocusedRowCellValue("FTFinDesc").ToString()
            Dim FTPayYearBegin As String = ogvdetail.GetFocusedRowCellValue("FTPayYearBegin").ToString()
            Dim FTPayTermBegin As String = ogvdetail.GetFocusedRowCellValue("FTPayTermBegin").ToString()
            Dim FTPayYearEnd As String = ogvdetail.GetFocusedRowCellValue("FTPayYearEnd").ToString()
            Dim FTPayTermEnd As String = ogvdetail.GetFocusedRowCellValue("FTPayTermEnd").ToString()
            Dim FCFinAmt As String = ogvdetail.GetFocusedRowCellValue("FCFinAmt").ToString()

            Dim FCFinAmtTotal As String = ogvdetail.GetFocusedRowCellValue("FCFinAmtTotal").ToString()
            Dim FCFinAmtTotalSysExpend As String = ogvdetail.GetFocusedRowCellValue("FCFinAmtTotalSysExpend").ToString()
            Dim FTStaCompletedPayment As String = ogvdetail.GetFocusedRowCellValue("FTStaCompletedPayment").ToString()


            With _wConfigFinExpendPopup

                .FNHSysFinExpendID = FNHSysFinExpendID
                .FNHSysEmpID.Properties.Tag = FNHSysEmpID
                .FNHSysEmpID.Text = FTEmpCode
                .FNHSysEmpTypeId.Text = FNHSysEmpTypeId
                .FTEmpName.Text = FTEmpName
                .FTFinCode.Properties.Tag = FTFinCode
                .FTFinCode.Text = FTFinCode
                .FTFinDesc.Text = FTFinDesc
                .FTPayYearBegin.Properties.Tag = FTPayYearBegin
                .FTPayYearBegin.Text = FTPayYearBegin
                .FTPayTermBegin.Properties.Tag = FTPayTermBegin
                .FTPayTermBegin.Text = FTPayTermBegin
                .FTPayYearEnd.Properties.Tag = FTPayYearEnd
                .FTPayYearEnd.Text = FTPayYearEnd
                .FTPayTermEnd.Properties.Tag = FTPayTermEnd
                .FTPayTermEnd.Text = FTPayTermEnd
                .FCFinAmt.Text = FCFinAmt

                .FCFinAmtTotal.Text = FCFinAmtTotal
                .FCFinAmtTotalSysExpend.Text = FCFinAmtTotalSysExpend

                If FTStaCompletedPayment = True Then
                    .FTStaCompletedPayment.Checked = True
                Else
                    .FTStaCompletedPayment.Checked = False

                End If

                .ShowDialog()

                LoadData()
                ''FNFundRate.Value = .FundRate

            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        LoadData()
    End Sub
End Class