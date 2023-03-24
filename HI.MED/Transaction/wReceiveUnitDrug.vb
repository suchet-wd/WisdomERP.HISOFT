﻿Public Class wReceiveUnitDrug


    Private _PurchaseNo As String = ""
    Public Property PurchaseNo() As String
        Get
            Return _PurchaseNo
        End Get
        Set(value As String)
            _PurchaseNo = value
        End Set
    End Property

    Private _ReceiveNo As String = ""
    Public Property ReceiveNo() As String
        Get
            Return _ReceiveNo
        End Get
        Set(value As String)
            _ReceiveNo = value
        End Set
    End Property


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private _ProcessProc As Boolean = False
    Public Property ProcessProc As Boolean
        Get
            Return _ProcessProc
        End Get
        Set(value As Boolean)
            _ProcessProc = value
        End Set
    End Property

    Private Function ValidateData() As Boolean
        Dim _Pass As Boolean = False
        With CType(Me.ogcrcv.DataSource, DataTable)
            .AcceptChanges()
            If .Select("FNHSysDrugUnitIdTo_Hide=0").Length <= 0 Then
                _Pass = True
            End If
        End With

        Return _Pass
    End Function

    Private Sub ocmreceive_Click(sender As System.Object, e As System.EventArgs) Handles ocmreceive.Click

        If ValidateData() Then


            With CType(Me.ogcrcv.DataSource, DataTable)
                .AcceptChanges()
                Dim _Qry As String
                For Each R As DataRow In .Rows
                    _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMECMDrug"
                    _Qry &= vbCrLf & " SET FNHSysDrugUnitId_Rcv=" & Integer.Parse(Val(R!FNHSysDrugUnitIdTo_Hide.ToString)) & " "
                    _Qry &= vbCrLf & " WHERE FTDrugCode='" & HI.UL.ULF.rpQuoted(R!FTDrugCode.ToString) & "' "
                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MEDC)
                Next
            End With

            Me.ProcessProc = True
            Me.Close()
        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการระบุหน่วยที่ต้องการทำการจัดเก็บยาใน Stock !!!", 1505120001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

    End Sub

    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmcancel.Click
        Me.ProcessProc = False
        Me.Close()
    End Sub

End Class