﻿Option Explicit On
Option Strict Off

Imports System
Imports System.Data
Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports System.Collections
Imports DevExpress
Imports DevExpress.XtraEditors

Public Class wInsertSeqPackingProc

#Region "Variable Declaration"
    Private sSQL As String
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Order"
#End Region

#Region "Property"

    Private _FTOrderNo As String
    Public Property FTOrderNo As String
        Get
            Return _FTOrderNo
        End Get
        Set(value As String)
            _FTOrderNo = value
        End Set
    End Property

    Private _FTSubOrderNo As String
    Public Property FTSubOrderNo As String
        Get
            Return _FTSubOrderNo
        End Get
        Set(value As String)
            _FTSubOrderNo = value
        End Set
    End Property

#End Region

#Region "Procedure And Function"
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        HI.TL.HandlerControl.AddHandlerObj(Me)

        Dim oSysLang As New HI.ST.SysLanguage

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, Me.Name.ToString.Trim, Me)
        Catch ex As Exception
        End Try

        Call HI.ST.Lang.SP_SETxLanguage(Me)

        With Me.FNInsertPackSeq.Properties
            .Buttons(0).Visible = False
            .Mask.UseMaskAsDisplayFormat = True
            .Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
            '.Mask.EditMask = "###,###,###"

            .TextEditStyle = XtraEditors.Controls.TextEditStyles.Standard

            .Precision = 0

            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            '.DisplayFormat.FormatString = "###,###,###"
        End With

    End Sub

    Private Function PROC_GETnumMaxPackSeq() As Integer
        Dim nFNMaxPackSeq As Integer
        Try
            sSQL = ""
            sSQL = "SELECT TOP 1 A.FNPackSeq AS FNPackSeq"
            sSQL &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Pack AS A (NOLOCK)"
            sSQL &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me._FTOrderNo) & "'"
            sSQL &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(Me._FTSubOrderNo) & "'"
            sSQL &= Environment.NewLine & "ORDER BY A.FNPackSeq DESC;"

            nFNMaxPackSeq = Val(HI.Conn.SQLConn.GetField(sSQL, HI.Conn.DB.DataBaseName.DB_MERCHAN, "0")) + 1

        Catch ex As Exception
            nFNMaxPackSeq = 1
        End Try

        Return nFNMaxPackSeq

    End Function

    Private Function PROC_VALIDATEbINSERTPackSeq() As Boolean
        Dim bValidate As Boolean = False

        Try
            If Me.FNInsertPackSeq.Value > 0 Then
                If Me.FTInsertPackDescription.Text.Trim <> "" Then
                    bValidate = True
                Else
                    HI.MG.ShowMsg.mInvalidData(HI.MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTInsertPackDescription_lbl.Text)
                    Me.FTInsertPackDescription.Focus()
                End If

            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNInsertPackSeq_lbl.Text, "Packing Sequence...")
                Me.FNInsertPackSeq.Focus()
            End If

        Catch ex As Exception
            '...Nothing
        End Try

        Return bValidate

    End Function

    Private Function PROC_SAVEbInsertPackingSeq() As Boolean
        Dim bSaveInsert As Boolean = False

        Try
            Dim tFTImageName$ = ""
            If Not DBNull.Value.Equals(Me.FTInsertImagePacking.Image) And Not Me.FTInsertImagePacking.Image Is Nothing Then
                tFTImageName = Me._FTSubOrderNo & "_" & Me.FNInsertPackSeq.Value.ToString()
                tFTImageName = Microsoft.VisualBasic.Replace(tFTImageName, "-", "_")
            End If

            '...step I ==> ถ้ารายการ Seq. ที่ทำการเพิ่มรายการนั้นตรงกับรายการเดิม (Seq. เดิมในตาราง) ให้ทำการ เพิ่มรายการ Seq. ของทุกรายการ ที่มีค่ามากกว่าหรือเท่ากับ รายการที่ทำการเพิ่มใหม่ แล้วทำการ แทรก รายการ
            sSQL = ""
            sSQL = "UPDATE A"
            sSQL &= Environment.NewLine & "SET  A.[FNPackSeq] = A.[FNPackSeq] + 1"
            sSQL &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Pack AS A"
            sSQL &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me._FTOrderNo) & "'"
            sSQL &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(Me._FTSubOrderNo) & "'"
            sSQL &= Environment.NewLine & "      AND A.FNPackSeq >= " & Me.FNInsertPackSeq.Value & ";"

            '...update รายการ FNSeq ในตาราง ถ้ารายการ Seq.นั้น มีค่ามากกว่ารายการ Seq.ที่ป้อนเข้ามาใหม่ ==> Seq เดิม เท่ากับ Seq เดิม บวก 1
            HI.Conn.SQLConn.ExecuteNonQuery(sSQL, Conn.DB.DataBaseName.DB_MERCHAN)

            sSQL = ""
            sSQL = "UPDATE A"
            sSQL &= Environment.NewLine & "SET  A.[FTUpdUser] = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            sSQL &= Environment.NewLine & "    ,A.[FDUpdDate] = " & HI.UL.ULDate.FormatDateDB & ""
            sSQL &= Environment.NewLine & "    ,A.[FTUpdTime] = " & HI.UL.ULDate.FormatTimeDB & ""
            sSQL &= Environment.NewLine & "    ,A.[FTPackDescription] = N'" & HI.UL.ULF.rpQuoted(Me.FTInsertPackDescription.Text.Trim) & "'"
            sSQL &= Environment.NewLine & "    ,A.[FTPackNote] = N'" & HI.UL.ULF.rpQuoted(Me.FTInsertPackNote.Text.Trim) & "'"
            sSQL &= Environment.NewLine & "    ,A.[FTImage] = N'" & HI.UL.ULImage.SaveImage(Me.FTInsertImagePacking, tFTImageName, "" & _SystemFilePath & "\OrderNo\SubOrderNo\Packing\") & "'"
            sSQL &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Sew AS A"
            sSQL &= Environment.NewLine & "WHERE A.FTOrderNO = N'" & HI.UL.ULF.rpQuoted(Me._FTOrderNo) & "'"
            sSQL &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(Me._FTSubOrderNo) & "'"
            sSQL &= Environment.NewLine & "      AND A.FNPackSeq = " & Me.FNInsertPackSeq.Value & ";"

            If HI.Conn.SQLConn.ExecuteNonQuery(sSQL, Conn.DB.DataBaseName.DB_MERCHAN) = False Then

                sSQL = ""
                sSQL = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_Pack] ([FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser],[FDUpdDate],[FTUpdTime],"
                sSQL &= Environment.NewLine & "                                        [FTOrderNo],[FTSubOrderNo],[FNPackSeq],[FTPackDescription],[FTPackNote],[FTImage])"
                sSQL &= Environment.NewLine & "SELECT N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTInsUser, " & HI.UL.ULDate.FormatDateDB & " AS FDInsDate, " & HI.UL.ULDate.FormatTimeDB & " AS FTInsTime, NULL AS FTUpdUser, NULL AS FDUpdDate, NULL AS FTUpdTime"
                sSQL &= Environment.NewLine & "      , N'" & HI.UL.ULF.rpQuoted(Me._FTOrderNo) & "' AS FTOrderNo, N'" & HI.UL.ULF.rpQuoted(Me._FTSubOrderNo) & "' AS FTSubOrderNo, " & Me.FNInsertPackSeq.Value & " AS FNPackSeq, N'" & HI.UL.ULF.rpQuoted(Me.FTInsertPackDescription.Text.Trim) & "' AS FTPackDescription, N'" & HI.UL.ULF.rpQuoted(Me.FTInsertPackNote.Text.Trim) & "' AS FTPackNote"
                sSQL &= Environment.NewLine & "      , N'" & HI.UL.ULImage.SaveImage(Me.FTInsertImagePacking, tFTImageName, "" & _SystemFilePath & "\OrderNo\SubOrderNo\Packing\") & "' AS FTImage;"

                HI.Conn.SQLConn.ExecuteNonQuery(sSQL, Conn.DB.DataBaseName.DB_MERCHAN)

            End If

            bSaveInsert = True

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
            End If
        End Try

        Return bSaveInsert

    End Function

#End Region

    Private Sub wInsertSeqPackingProc_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub FNInsertPackSeq_ParseEditValue(sender As Object, e As Controls.ConvertEditValueEventArgs) Handles FNInsertPackSeq.ParseEditValue
        '...provide for sewing sequence max sequence no + 1
        Try
            Dim sEditVal As String

            If Not e.Value Is Nothing Then
                sEditVal = e.Value.ToString()
            Else
                sEditVal = "0"
            End If

            For numLoopStr As Integer = e.Value.ToString().Length - 1 To 0 Step -1
                If Char.IsDigit(sEditVal(numLoopStr)) Then
                    Exit For
                Else
                    sEditVal = sEditVal.Remove(sEditVal.Length - 1)
                End If
            Next

            Try
                e.Value = Convert.ToDecimal(sEditVal)

                If Val(e.Value.ToString) < 0 Then e.Value = PROC_GETnumMaxPackSeq()

            Catch ex As Exception
                e.Value = 1
            End Try

            e.Handled = True

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            '    MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.ToString)
            'End If
        End Try

    End Sub

    Private Sub ocmInsertPackProc_Click(sender As Object, e As EventArgs) Handles ocmInsertPackProc.Click
        If PROC_VALIDATEbINSERTPackSeq() = True Then

            If HI.MG.ShowMsg.mConfirmProcess("ท่านต้องการแทรกรายการลำดับขั้นตอนงานแพ็คใช่หรือไม่ !!!", 1501300001, "Packing Sequence : " & Me.FNInsertPackSeq.Value) = True Then
                If PROC_SAVEbInsertPackingSeq() = True Then
                    DialogResult = System.Windows.Forms.DialogResult.OK
                Else
                    '...nothing
                End If

            End If

        End If
    End Sub

    Private Sub ocmCancelPackProc_Click(sender As Object, e As EventArgs) Handles ocmCancelPackProc.Click
        DialogResult = System.Windows.Forms.DialogResult.Cancel
    End Sub

End Class