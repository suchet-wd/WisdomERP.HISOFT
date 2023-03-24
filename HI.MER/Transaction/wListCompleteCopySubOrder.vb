﻿Option Explicit On
Option Strict Off

Imports System
Imports System.Data
Imports System.Windows.Forms
Imports System.Windows.Forms.Control

Public Class wListCompleteCopySubOrder
    Private _DTCopyOrder As System.Data.DataTable

    Public Sub New(ByRef paramDTCopyOrder As System.Data.DataTable)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        If Not paramDTCopyOrder Is Nothing Then
            _DTCopyOrder = paramDTCopyOrder.Copy()
        End If

    End Sub

    Private Function PROC_GETbShowBrowseData() As Boolean
        Dim bRet As Boolean = False
        Try
            Me.ogdOrderSubCopy.DataSource = _DTCopyOrder
            ogvCopyOrderSub.OptionsView.ColumnAutoWidth = True
            ogvCopyOrderSub.BestFitColumns()
            Me.ogdOrderSubCopy.Refresh()
            ogvCopyOrderSub.RefreshData()

            bRet = True

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
            End If
        End Try

        Return bRet

    End Function

    Private Sub wListCompleteCopySubOrder_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not DBNull.Value.Equals(Me._DTCopyOrder) AndAlso Me._DTCopyOrder.Rows.Count > 0 Then
            Call PROC_GETbShowBrowseData()
        Else
            Me.ogdOrderSubCopy.DataSource = Nothing
        End If
    End Sub

End Class