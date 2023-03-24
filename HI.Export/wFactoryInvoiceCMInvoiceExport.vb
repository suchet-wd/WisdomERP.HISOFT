﻿Public Class wFactoryInvoiceCMInvoiceExport 

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private _StateProc As Boolean = False
    Public Property StateProc As Boolean
        Get
            Return _StateProc
        End Get
        Set(value As Boolean)
            _StateProc = value
        End Set
    End Property

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.StateProc = False
        Me.Close()
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        If Me.FTInvoiceExportNo.Text.Trim() <> "" Then

            If Me.FDInvoiceExportDate.Text <> "" Then
                Me.StateProc = True
                Me.Close()
            Else

                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FDInvoiceExportDate_lbl.Text)
                FDInvoiceExportDate.Focus()
                FDInvoiceExportDate.SelectAll()

            End If

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTInvoiceExportNo_lbl.Text)
            FTInvoiceExportNo.Focus()
            FTInvoiceExportNo.SelectAll()
        End If
    End Sub
End Class