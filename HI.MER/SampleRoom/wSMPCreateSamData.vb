Imports System.ComponentModel
Imports DevExpress.XtraEditors.Controls

Public Class wSMPCreateSamData

    Private SamCostPrice As Decimal = 0
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


        Dim cmdstring As String = "Select TOp 1 FTCfgData FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS X WITH(NOLOCK) WHERE FTCfgName='SMPSamCostPrice'"

        SamCostPrice = Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SECURITY, "0.0"))

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

#End Region

#Region "Procedure"

    Public Sub LoadOrderDataInfo(ByVal Key As Object)

        Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")

        Try
            ogcoperation.DataSource = Nothing

            Dim _Qry As String = ""
            Dim _dtprod As DataTable

            _Qry = "SELECT  P.FNSeq, P.FNMockUpType AS FNMockUpType_Hide, P.FNSam , P.FNCostPerMin , P.FNPrice   "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

                _Qry &= vbCrLf & "  ,X.FTNameTH As FNMockUpType"

            Else
                _Qry &= vbCrLf & "  , X.FTNameEN As FNMockUpType"

            End If

            _Qry &= vbCrLf & " ,P.FTRemark"
            _Qry &= vbCrLf & " ,P.FTRemark"
            _Qry &= vbCrLf & " , Case When ISDATE(P.FDInsDate) = 1 Then  Convert(varchar(10),convert(Datetime,P.FDInsDate) ,103) Else '' END AS  FDInsDate"
            _Qry &= vbCrLf & " ,P.FTInsUser,P.FTInsTime"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSam AS P With(Nolock)"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN (  "
            _Qry &= vbCrLf & "  SELECT FNListIndex,FTNameTH,FTNameEN "
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WITH (NOLOCK) "
            _Qry &= vbCrLf & "  WHERE  (FTListName = N'FNMockUpType')  "
            _Qry &= vbCrLf & "  ) AS X ON  P.FNMockUpType =X.FNListIndex "
            _Qry &= vbCrLf & "  WHERE P.FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(Key.ToString) & "'  "
            _Qry &= vbCrLf & "  Order By  P.FNSeq  "

            _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

            ogcoperation.DataSource = _dtprod.Copy


        Catch ex As Exception

        End Try

        _Spls.Close()
    End Sub

    Private Sub InitNewFirstRow()


        Dim _Qry As String = ""
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry = "   SELECT       TOP 1  FTNameTH"
            _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WITH (NOLOCK)"
            _Qry &= vbCrLf & "  WHERE        (FTListName = N'FNMockUpType' AND FNListIndex=0) ORDER BY  FNListIndex "
        Else
            _Qry = "   SELECT       TOP 1  FTNameEN"
            _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WITH (NOLOCK)"
            _Qry &= vbCrLf & "  WHERE        (FTListName = N'FNMockUpType' AND FNListIndex=0) ORDER BY  FNListIndex "

        End If

        With CType(Me.ogcoperation.DataSource, DataTable)
            .Rows.Add()
            .Rows(.Rows.Count - 1)!FNSeq = .Rows.Count
            .Rows(.Rows.Count - 1)!FNMockUpType_Hide = 0
            .Rows(.Rows.Count - 1)!FNMockUpType = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")
            .Rows(.Rows.Count - 1)!FNSam = 0
            .Rows(.Rows.Count - 1)!FNCostPerMin = SamCostPrice
            .Rows(.Rows.Count - 1)!FNPrice = 0
            .Rows(.Rows.Count - 1)!FTRemark = ""
            .AcceptChanges()

        End With
    End Sub

    Private Sub InitNewRow()



        Dim _Qry As String = ""
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry = "   SELECT       TOP 1  FTNameTH"
            _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WITH (NOLOCK)"
            _Qry &= vbCrLf & "  WHERE        (FTListName = N'FNMockUpType' AND FNListIndex=1) ORDER BY  FNListIndex "
        Else
            _Qry = "   SELECT       TOP 1  FTNameEN"
            _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WITH (NOLOCK)"
            _Qry &= vbCrLf & "  WHERE        (FTListName = N'FNMockUpType' AND FNListIndex=1) ORDER BY  FNListIndex "

        End If

        With CType(Me.ogcoperation.DataSource, DataTable)
            .AcceptChanges()

            If .Select("FNSam<=0").Length <= 0 Then
                .Rows.Add()
                .Rows(.Rows.Count - 1)!FNSeq = .Rows.Count
                .Rows(.Rows.Count - 1)!FNMockUpType_Hide = 1
                .Rows(.Rows.Count - 1)!FNMockUpType = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")
                .Rows(.Rows.Count - 1)!FNSam = 0
                .Rows(.Rows.Count - 1)!FNCostPerMin = SamCostPrice
                .Rows(.Rows.Count - 1)!FNPrice = 0
                .Rows(.Rows.Count - 1)!FTRemark = ""
                .AcceptChanges()

            End If

        End With
    End Sub
#End Region

#Region "General"


    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ogvoperation_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvoperation.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Down
                With CType(Me.ogcoperation.DataSource, DataTable)

                    .AcceptChanges()


                    Call InitNewRow()



                End With
            Case System.Windows.Forms.Keys.Delete

                If Val(ogvoperation.GetFocusedRowCellValue("FNSeq").ToString) > 1 Then

                    Me.ogvoperation.DeleteRow(Me.ogvoperation.FocusedRowHandle)
                    With CType(Me.ogcoperation.DataSource, DataTable)
                        .AcceptChanges()
                        Dim _RIndx As Integer = 0
                        For Each R In .Rows
                            _RIndx = _RIndx + 1
                            R!FNSeq = _RIndx
                        Next
                        .AcceptChanges()
                    End With
                End If



            Case System.Windows.Forms.Keys.Enter

        End Select
    End Sub


    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click

        CType(Me.ogcoperation.DataSource, DataTable).AcceptChanges()
        If FTSMPOrderNo.Properties.Tag.ToString <> "" And FTSMPOrderNo.Text <> "" Then
            With CType(Me.ogcoperation.DataSource, DataTable)
                .AcceptChanges()

                Dim _FNSeq As Integer = 0
                Dim _Spls As New HI.TL.SplashScreen("Saving...Data Please Wait.", Me.Text)
                Dim _Qry As String = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSam WHERE FTSMPOrderNo ='" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "'"

                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                Try
                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    End If

                    For Each R As DataRow In .Select("FNSam>0", "FNSeq")


                        _FNSeq = _FNSeq + 1

                        _Qry = "Insert INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSam"
                        _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime,  FTSMPOrderNo, FNSeq,  FNMockUpType,FNSam,FNCostPerMin,FNPrice,FTRemark)"
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "' "
                        _Qry &= vbCrLf & " ," & _FNSeq & " "
                        _Qry &= vbCrLf & " ," & Val(R!FNMockUpType_Hide.ToString) & " "
                        _Qry &= vbCrLf & " ," & Val(R!FNSam.ToString) & " "
                        _Qry &= vbCrLf & " ," & Val(R!FNCostPerMin.ToString) & " "
                        _Qry &= vbCrLf & " ," & Val(R!FNPrice.ToString) & " "
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTRemark.ToString) & "' "

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                            _Spls.Close()

                            Exit Sub

                        End If

                    Next

                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Try
                        Call ocmrefresh_Click(ocmrefresh, New System.EventArgs)
                    Catch ex As Exception
                    End Try

                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Catch ex As Exception
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End Try

            End With
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNHSysStyleId_lbl.Text)
            FNHSysStyleId.Focus()
        End If

    End Sub

    Private Sub wOperationByStyle_Load(sender As Object, e As EventArgs) Handles Me.Load
        ogvoperation.OptionsView.ShowAutoFilterRow = False
        ogvoperation.OptionsSelection.MultiSelect = False
        ogvoperation.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect

        FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

        ReposFNMockUpType.Items.Clear()
        ReposFNMockUpType.Items.AddRange(HI.TL.CboList.SetList("" & ReposFNMockUpType.Tag.ToString))

    End Sub

#End Region


    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click

        Me.ogcoperation.DataSource = Nothing
        Call LoadOrderDataInfo(FTSMPOrderNo.Text)
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        ogcoperation.DataSource = Nothing

    End Sub

    Private Sub ogcoperation_Click(sender As Object, e As EventArgs) Handles ogcoperation.Click

    End Sub

    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTSMPOrderNo.EditValueChanged
        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTOrderNo_EditValueChanged), New Object() {sender, e})
        Else
            Call LoadOrderDataInfo(FTSMPOrderNo.Text)

        End If
    End Sub

    Private Sub ReposFNMockUpType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ReposFNMockUpType.SelectedIndexChanged
        Try


            With ogvoperation
                .SetFocusedRowCellValue(.FocusedColumn.FieldName & "_Hide", CType(sender, DevExpress.XtraEditors.ComboBoxEdit).SelectedIndex)
            End With
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ReposFNMockUpType_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles ReposFNMockUpType.EditValueChanging

        Try

            With Me.ogvoperation

                If HI.TL.CboList.GetIndexByText("" & ReposFNMockUpType.Tag.ToString, e.NewValue.ToString) = 0 Then
                    If Val(.GetFocusedRowCellValue("FNSeq").ToString) > 1 Then
                        e.Cancel = True
                    End If
                End If


            End With

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ogvoperation_ShowingEditor(sender As Object, e As CancelEventArgs) Handles ogvoperation.ShowingEditor
        Try

            With Me.ogvoperation

                If .FocusedColumn.FieldName = "FNMockUpType" Then
                    If Val(.GetFocusedRowCellValue("FNSeq").ToString) <= 1 Then
                        e.Cancel = True
                    Else
                        e.Cancel = False
                    End If
                Else
                    e.Cancel = False
                End If



            End With

        Catch ex As Exception

        End Try


    End Sub

    Private Sub RepFNSam_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepFNSam.EditValueChanging
        Try
            With Me.ogvoperation

                Dim sam As Decimal = 0.0
                Dim costpermin As Decimal = 0.0
                Dim price As Decimal = 0

                sam = Val(.GetFocusedRowCellValue("FNSam").ToString)
                costpermin = Val(.GetFocusedRowCellValue("FNCostPerMin").ToString)

                Select Case .FocusedColumn.FieldName.ToLower
                    Case "FNSam".ToLower
                        sam = Val(e.NewValue.ToString)
                    Case "FNCostPerMin".ToLower
                        costpermin = Val(e.NewValue.ToString)
                End Select

                price = CDbl(Format(sam * costpermin, "0.0000"))

                .SetFocusedRowCellValue("FNPrice", price)
            End With
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click

        Try
            Dim _dtprod As DataTable
            With CType(Me.ogcoperation.DataSource, DataTable)
                .AcceptChanges()
                _dtprod = .Copy
            End With


            If _dtprod.Rows.Count <= 0 Then
                InitNewFirstRow()
            Else
                InitNewRow()
            End If

        Catch ex As Exception

        End Try


    End Sub

    Private Sub ocmremove_Click(sender As Object, e As EventArgs) Handles ocmremove.Click
        Try
            Me.ogvoperation.DeleteRow(Me.ogvoperation.FocusedRowHandle)
            With CType(Me.ogcoperation.DataSource, DataTable)
                .AcceptChanges()
                Dim _RIndx As Integer = 0
                For Each R In .Rows
                    _RIndx = _RIndx + 1
                    R!FNSeq = _RIndx
                Next
                .AcceptChanges()
            End With
        Catch ex As Exception

        End Try


    End Sub
End Class