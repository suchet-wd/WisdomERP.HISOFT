Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls

Public Class wAddSkillByStyle

    Private StateAddSkill As Boolean = False
    Public Property AddSkill As Boolean
        Get
            Return StateAddSkill
        End Get
        Set(value As Boolean)
            StateAddSkill = value
        End Set
    End Property

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' HI.TL.HandlerControl.AddHandlerObj(Me)
    End Sub

    Private Sub wAddItemPO_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Call HI.ST.Lang.SP_SETxLanguage(Me)
        Me.ogvdetail.OptionsView.ShowAutoFilterRow = True
    End Sub

    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmcancel.Click
        Me.AddSkill = False
        Me.Close()
    End Sub

    Private Sub ocmadd_Click(sender As System.Object, e As System.EventArgs) Handles ocmadd.Click

        Me.AddSkill = True
        Me.Close()

    End Sub

    Private Sub ReposFNSkillByStyle_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles ReposFNSkillByStyle.EditValueChanging
        Try
            With ogvdetail
                If ("" & .GetFocusedRowCellValue("FTSelect").ToString <> "1") Then
                    e.Cancel = True
                Else
                    If IsNumeric(e.NewValue) Then
                        If e.NewValue <= 0 Then
                            e.Cancel = True
                        Else
                            e.Cancel = False
                        End If
                    Else
                        e.Cancel = True
                    End If
                End If
            End With
        Catch ex As Exception
            e.Cancel = True
        End Try
    End Sub

    Private Sub FNSkillByStyle_EditValueChanged(sender As Object, e As EventArgs) Handles FNSkillByStyle.EditValueChanged

    End Sub

    Private Sub FNSkillByStyle_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles FNSkillByStyle.EditValueChanging
        Try

            If IsNumeric(e.NewValue) Then
                If e.NewValue <= 0 Then

                    e.Cancel = True

                Else

                    e.Cancel = False

                    If Not (Me.ogcdetail.DataSource Is Nothing) Then

                        With CType(Me.ogcdetail.DataSource, DataTable)
                            .AcceptChanges()

                            For Each R As DataRow In .Select("FTSelect='1'")
                                R!FNSkillByStyle = e.NewValue
                            Next

                            .AcceptChanges()

                        End With

                    End If

                End If
            Else
                e.Cancel = True
            End If

        Catch ex As Exception
            e.Cancel = True
        End Try

    End Sub

    Private Sub FTStateSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles FTStateSelectAll.CheckedChanged
        Try
            Dim StateSelect As String = "0"


            If FTStateSelectAll.Checked Then
                StateSelect = "1"
            End If


            With CType(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()

                For Each R As DataRow In .Rows
                    R!FTSelect = StateSelect
                Next
                .AcceptChanges()
            End With


        Catch ex As Exception

        End Try
    End Sub
End Class