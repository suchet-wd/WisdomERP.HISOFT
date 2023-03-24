
Public Class wMECRemedyPopup


#Region "Property"
    Public _MECGenId As Integer
    Private Property MECGenId As Integer
        Get
            Return _MECGenId
        End Get
        Set(value As Integer)
            _MECGenId = value
        End Set
    End Property

    Public _Proc As Boolean
    Private Property Proc As Boolean
        Get
            Return _Proc
        End Get
        Set(value As Boolean)
            _Proc = value
        End Set
    End Property
    Public _FTStateGen As String
    Private Property FTStateGen As String
        Get
            Return _FTStateGen
        End Get
        Set(value As String)
            _FTStateGen = value
        End Set
    End Property
#End Region
 


    Private Sub ocmclose_Click(sender As Object, e As EventArgs) Handles ocmclose.Click
        Try
            Proc = False
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub


    Private Sub FNHSysDrugId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysDrugId.EditValueChanged
        Try
            If Me.FNHSysDrugId.Text <> "" Then
                Me.FNHSysDrugUnitId.Text = SetDrugUnit(Me.FNHSysDrugId.Properties.Tag)
                Me.FNQuantityBal.Value = GetOnhand(Me.FNHSysDrugId.Properties.Tag)
                Me.FNQuantity.Value = 0
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Function GetOnhand(ByVal _DrugId As Integer) As Double
        Try
            Call ChkEdit()
            Dim _Cmd As String = ""
            _Cmd = "Select   sum(FNQuantity) AS FNQuantity"
            _Cmd &= vbCrLf & " From ("
            _Cmd &= vbCrLf & "SELECT     FTMEDRcvNo, FNHSysDrugId, FNQuantity"
            _Cmd &= vbCrLf & "FROM     TMECTRecieve_Detail  WITH(NOLOCK)"
            _Cmd &= vbCrLf & "WHERE LEFT(FTMEDRcvNo,2) = '" & Microsoft.VisualBasic.Left(HI.ST.SysInfo.CmpRunID, 2) & "'"
            _Cmd &= vbCrLf & "        UNION ALL"
            _Cmd &= vbCrLf & "SELECT     FTDocumentRefNo,FNHSysDrugId, -FNQuantity"
            _Cmd &= vbCrLf & "FROM         TMECTDrugPay WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE FNHSysDrugPayId <>" & Integer.Parse(_DocRunIdEdit)
            _Cmd &= vbCrLf & "AND LEFT(FTDocumentRefNo,2) = '" & Microsoft.VisualBasic.Left(HI.ST.SysInfo.CmpRunID, 2) & "'"
            _Cmd &= vbCrLf & ") AS T"
            _Cmd &= vbCrLf & "WHERE FNHSysDrugId =" & Integer.Parse(_DrugId)
            _Cmd &= vbCrLf & "Group by  FNHSysDrugId"
            Dim _Onhand As Double
            _Onhand = CDbl("0" & HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MEDC, "0"))
            _Onhand = IIf(_Onhand < 0, 0, _Onhand)
            Return _Onhand
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function SetDrugUnit(ByVal _DrugId As Integer) As String
        Try
            Dim _Cmd As String = ""
            _Cmd = "SELECT  Top 1 U.FTDrugUnitCode"
            _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMECMDrug AS D WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMECMDrugUnit AS U WITH (NOLOCK) ON D.FNHSysDrugUnitId_Rcv = U.FNHSysDrugUnitId"
            _Cmd &= vbCrLf & "WHERE D.FNHSysDrugId=" & Integer.Parse(_DrugId)
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MASTER, "")
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub FNQuantity_EditValueChanged(sender As Object, e As EventArgs) Handles FNQuantity.EditValueChanged
        Try
            If Me.FNQuantity.Value > Me.FNQuantityBal.Value Then
                HI.MG.ShowMsg.mInfo("ไม่สามารถจ่ายของได้ เนื่องจากเกินจำนวนที่มีอยู่", 1505130001, Me.Text, "", System.Windows.Forms.MessageBoxIcon.Stop)
                Me.FNQuantity.Value = Me.FNQuantityBal.Value
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmAddDrug_Click(sender As Object, e As EventArgs) Handles ocmAddDrug.Click
        Try
            If VerrifyData() Then
                Call AddDrug(Me.FNHSysDrugId.Properties.Tag, Me.FNQuantity.Value)
                Call LoadDataDrug()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private _DocRunIdEdit As Integer = 0
    Private Sub ChkEdit()
        Try
            _DocRunIdEdit = 0
            Dim _oDt As DataTable = CType(Me.ogcDrugPay.DataSource, DataTable)
            For Each R As DataRow In _oDt.Select("FTDrugCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysDrugId.Text) & "'")
                _DocRunIdEdit = Integer.Parse(R!FNHSysDrugPayId.ToString)
                Exit Sub
            Next
            _DocRunIdEdit = HI.TL.RunID.GetRunNoID("TMECTDrugPay", "FNHSysDrugPayId", Conn.DB.DataBaseName.DB_MEDC)
        Catch ex As Exception
        End Try
    End Sub
    Private Function AddDrug(ByVal _DrugId As Integer, ByVal _IssQty As Double) As Boolean
        Try
            Call ChkEdit()
            Dim _DocId As Integer = _DocRunIdEdit
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _IssCut As Double = 0
            Dim _IssBal As Double = _IssQty
            _Cmd = "Select  FTMEDRcvNo,FNHSysDrugId, sum(FNQuantity) AS FNQuantity"
            _Cmd &= vbCrLf & "From ("
            _Cmd &= vbCrLf & "SELECT     FTMEDRcvNo, FNHSysDrugId, FNQuantity"
            _Cmd &= vbCrLf & "FROM TMECTRecieve_Detail WITH(NOLOCK)"
            _Cmd &= vbCrLf & "WHERE LEFT(FTMEDRcvNo,2) = '" & Microsoft.VisualBasic.Left(HI.ST.SysInfo.CmpRunID, 2) & "'"
            _Cmd &= vbCrLf & "       UNION ALL"
            _Cmd &= vbCrLf & "SELECT     FTDocumentRefNo,FNHSysDrugId, -FNQuantity"
            _Cmd &= vbCrLf & "FROM         TMECTDrugPay WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE FNHSysDrugPayId <>" & Integer.Parse(_DocId)
            _Cmd &= vbCrLf & " AND LEFT(FTDocumentRefNo,2) = '" & Microsoft.VisualBasic.Left(HI.ST.SysInfo.CmpRunID, 2) & "'"
            _Cmd &= vbCrLf & " ) AS T"
            _Cmd &= vbCrLf & "WHERE FNHSysDrugId =" & Integer.Parse(_DrugId)
            _Cmd &= vbCrLf & "Group by  FTMEDRcvNo,FNHSysDrugId"
            _Cmd &= vbCrLf & "Order by FTMEDRcvNo ASC "
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MEDC)

            Dim a As String = ""

            For Each R As DataRow In _oDt.Rows
                If _IssBal > Double.Parse(R!FNQuantity.ToString) And Double.Parse(R!FNQuantity.ToString) > 0 Then
                    _IssCut = Double.Parse(R!FNQuantity.ToString)
                ElseIf _IssBal <= Double.Parse(R!FNQuantity.ToString) And Double.Parse(R!FNQuantity.ToString) > 0 Then

                    _IssCut = _IssBal

                Else
                    _IssCut = 0
                End If

                If _IssCut > 0 Then
                    _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTDrugPay WHERE FNHSysMECGenId=" & Integer.Parse(_MECGenId)
                    _Cmd &= vbCrLf & "And FNHSysDrugId=" & Integer.Parse(_DrugId)
                    _Cmd &= vbCrLf & "And FNHSysDrugPayId=" & Integer.Parse(_DocId)
                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MEDC)

                    _Cmd = "INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTDrugPay "
                    _Cmd &= "( FTInsUser, FDInsDate, FTInsTime,   FNHSysMECGenId, FNHSysDrugId, FNQuantity, FTDocumentRefNo ,FNHSysDrugPayId ,FTStateGen)"
                    _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & "," & Integer.Parse(_MECGenId)
                    _Cmd &= vbCrLf & "," & Integer.Parse(_DrugId)
                    _Cmd &= vbCrLf & "," & Double.Parse(_IssCut)
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTMEDRcvNo.ToString) & "'"
                    _Cmd &= vbCrLf & "," & Integer.Parse(_DocId)
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTStateGen) & "'"

                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MEDC)

                    _IssBal = _IssBal - _IssCut
                End If

                If _IssCut > 0 Then
                    a = "y"
                End If


                If _IssBal <= 0 Then
                    Exit For
                End If
            Next


            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function VerrifyData() As Boolean
        Try
            If Me.FNHSysDrugId.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysDrugId_lbl.Text)
                Me.FNHSysDrugId.Focus()
                Return False
            End If

            If Me.FNHSysDrugUnitId.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysDrugUnitId_lbl.Text)
                Me.FNHSysDrugUnitId.Focus()
                Return False
            End If

            If Me.FNQuantity.Value <= 0 Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNQuantity_lbl.Text)
                Me.FNQuantity.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub LoadDataDrug()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable

            _Cmd = "SELECT   D.FNHSysDrugId ,  P.FNHSysDrugPayId ,  P.FNHSysMECGenId, D.FTDrugCode, U.FTDrugUnitCode, P.FNQuantity"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", D.FTDrugNameTH as FTDrugName"
            Else
                _Cmd &= vbCrLf & ", D.FTDrugNameEN as FTDrugName"
            End If
            _Cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTDrugPay AS P WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMECMDrug AS D WITH (NOLOCK) ON P.FNHSysDrugId = D.FNHSysDrugId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMECMDrugUnit AS U WITH (NOLOCK) ON D.FNHSysDrugUnitId_Rcv = U.FNHSysDrugUnitId"
            _Cmd &= vbCrLf & "WHERE  P.FNHSysMECGenId=" & Integer.Parse(MECGenId)
            _Cmd &= vbCrLf & "AND Isnull(P.FTStateGen,'0') ='" & HI.UL.ULF.rpQuoted(FTStateGen) & "'"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MEDC)
            Me.ogcDrugPay.DataSource = _oDt

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmDelDrug_Click(sender As Object, e As EventArgs) Handles ocmDelDrug.Click
        Try
            Call DeleteData()
            Me.LoadDataDrug()
        Catch ex As Exception
        End Try
    End Sub

    Private Function DeleteData() As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _DrugId As Integer = 0
            Dim _FNHSysDrugPayId As Integer = 0
            With ogvDrugPay
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Return False
                _DrugId = Integer.Parse(.GetRowCellValue(.FocusedRowHandle, "FNHSysDrugId").ToString)
                _FNHSysDrugPayId = Integer.Parse(.GetRowCellValue(.FocusedRowHandle, "FNHSysDrugPayId").ToString)
                _Cmd = "Delete FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTDrugPay  WHERE FNHSysMECGenId =" & Integer.Parse(MECGenId)
                _Cmd &= vbCrLf & "And FNHSysDrugId=" & _DrugId
                _Cmd &= vbCrLf & "and FNHSysDrugPayId=" & _FNHSysDrugPayId
                If HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MEDC) = False Then
                    Return False
                End If
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Sub ogvDrugPay_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvDrugPay.KeyDown
        Try
            If e.KeyCode = System.Windows.Forms.Keys.Delete Then
                Call DeleteData()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            If VerrifyGeneral() Then
                Proc = True
                Me.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function VerrifyGeneral() As Boolean
        Try
            If Me.FDDate.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FDDate_lbl.Text)
                Me.FDDate.Focus()
                Return False
            End If

            If Me.FTTime.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTTime_lbl.Text)
                Me.FTTime.Focus()
                Return False
            End If

            If Me.FNHSysTypeofDiseaseId.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysTypeofDiseaseId_lbl.Text)
                Me.FNHSysTypeofDiseaseId.Focus()
                Return False
            End If

            If Me.FNCauseType.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNCauseType_lbl.Text)
                Me.FNCauseType.Focus()
                Return False
            End If

            If Me.FNHSysOpinionId.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysOpinionId_lbl.Text)
                Me.FNHSysOpinionId.Focus()
                Return False
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub wMECRemedyPopup_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.LoadDataDrug()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNQuantity_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FNQuantity.KeyDown
        Try
            If e.KeyCode = System.Windows.Forms.Keys.Enter Then
                If VerrifyData() Then
                    Call AddDrug(Me.FNHSysDrugId.Properties.Tag, Me.FNQuantity.Value)
                    Call LoadDataDrug()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class