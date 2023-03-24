Imports DevExpress.XtraGrid.Views.Base

Public Class wAdditemMasterPop
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initializatio after the InitializeComponent() call.

    End Sub

#Region "Property"
    Private _Confirm As Boolean = False
    Public Property Confirm As Boolean
        Get
            Return _Confirm
        End Get
        Set(value As Boolean)
            _Confirm = value
        End Set
    End Property

#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Confirm = False
        Me.Close()
    End Sub

    Private Sub wAdditemMasterPop_Load(sender As Object, e As EventArgs) Handles Me.Load
        ST.Lang.SP_SETxLanguage(Me)
        'removehandler of default
        'addhandler my JOKER create
        With Me.FNHSysEmpID
            RemoveHandler .Leave, AddressOf TL.HandlerControl.DynamicResponButtoneditSysHide_Leave
        End With
        With Me.FNHSysUnitSectId
            RemoveHandler .Leave, AddressOf TL.HandlerControl.DynamicResponButtoneditSysHide_Leave
        End With
        With Me.FNHSysSuplId
            RemoveHandler .Leave, AddressOf TL.HandlerControl.DynamicResponButtoneditSysHide_Leave
        End With
        With Me.RepFDDateStartWarranty

            AddHandler .Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_Leave
            AddHandler .Click, AddressOf HI.TL.HandlerControl.RepositoryItemDate_GotFocus
        End With
        With Me.RepFDDateEndWarranty

            AddHandler .Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_Leave
            AddHandler .Click, AddressOf HI.TL.HandlerControl.RepositoryItemDate_GotFocus
        End With
        With Me.RepFDDateUsed

            '    AddHandler .Leave, AddressOf RepositoryItemDate_Leave
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_Leave
            AddHandler .Click, AddressOf HI.TL.HandlerControl.RepositoryItemDate_GotFocus
        End With
        With Me.RepFDPurchaseDate

            AddHandler .Leave, AddressOf RepositoryItemDate_Leave
            AddHandler .Click, AddressOf HI.TL.HandlerControl.RepositoryItemDate_GotFocus
        End With
        With Me.RepFDReceiveDate

            AddHandler .Leave, AddressOf RepositoryItemDate_Leave
            AddHandler .Click, AddressOf HI.TL.HandlerControl.RepositoryItemDate_GotFocus
        End With
        With Me.RepFDInvoiceDate
            'RemoveHandler .Leave, AddressOf TL.HandlerControl.RepositoryItemDate_Leave
            'RemoveHandler .Click, AddressOf TL.HandlerControl.RepositoryItemDate_GotFocus
            AddHandler .Leave, AddressOf RepositoryItemDate_Leave
            AddHandler .Click, AddressOf HI.TL.HandlerControl.RepositoryItemDate_GotFocus
        End With

    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        If VerityData() Then
            Me.Confirm = True
            Me.Close()
        End If
    End Sub

    Private Function VerityData() As Boolean
        Dim _Pass As Boolean = False
        Dim _rowcount As Integer = 0
        _rowcount = CType(ogc.DataSource, DataTable).Rows.Count - 1
        Try
            'Check value in column at NOTNULL
            With ogv
                For i As Integer = 0 To _rowcount Step 1
                    For Each C As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                        If (C.OptionsColumn.AllowEdit) Then
                            'ปรับให้บันทึกได้แค่แถวเดียวได้
                            If .GetRowCellValue(i, C.FieldName).ToString = "" Or .GetRowCellValue(i, C.FieldName).ToString <> "" Then
                                If .GetRowCellValue(i, "FTSerialNo").ToString <> "" Then
                                    If .GetRowCellValue(i, C.FieldName).ToString <> "" Or C.FieldName = "FTRemark" Or C.VisibleIndex = -1 Or C.FieldName = "FNPrice" Or C.FieldName = "FNHSysEmpID" Or C.FieldName = "FNHSysUnitSectId" Or C.FieldName = "FTProductCode" Then
                                        _Pass = True
                                    Else
                                        MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, C.Caption)
                                        .FocusedRowHandle = i
                                        .FocusedColumn = .VisibleColumns(C.AbsoluteIndex)
                                        .ShowEditor()
                                        _Pass = False
                                        Exit For
                                    End If
                                End If
                            Else
                                MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, C.Caption)
                                .FocusedRowHandle = i
                                .FocusedColumn = .VisibleColumns(C.AbsoluteIndex)
                                .ShowEditor()
                                _Pass = False

                                Exit For
                            End If
                        End If
                    Next
                Next
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
            Return _Pass = False
        End Try

        Return _Pass
    End Function

    Private Shared Sub RepositoryItemDate_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                Dim _TDate As String
                Try
                    _TDate = HI.UL.ULDate.ConvertEnDB(CType(sender, DevExpress.XtraEditors.DateEdit).DateTime)
                    If _TDate = "0001/01/01" Then
                        _TDate = ""
                    End If
                Catch ex As Exception
                    _TDate = ""
                End Try

                CType(sender, DevExpress.XtraEditors.DateEdit).Text = _TDate
                .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, HI.UL.ULDate.ConvertEN(_TDate))

            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogv_FocusedColumnChanged(sender As Object, e As FocusedColumnChangedEventArgs) Handles ogv.FocusedColumnChanged

        With Me.RepFDDateStartWarranty
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_Leave
            AddHandler .Click, AddressOf HI.TL.HandlerControl.RepositoryItemDate_GotFocus
        End With
        With Me.RepFDDateEndWarranty
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_Leave
            AddHandler .Click, AddressOf HI.TL.HandlerControl.RepositoryItemDate_GotFocus
        End With
        With Me.RepFDDateUsed
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_Leave
            AddHandler .Click, AddressOf HI.TL.HandlerControl.RepositoryItemDate_GotFocus
        End With
        With Me.RepFDPurchaseDate
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_Leave
            AddHandler .Click, AddressOf HI.TL.HandlerControl.RepositoryItemDate_GotFocus
        End With
        With Me.RepFDReceiveDate
                AddHandler .Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_Leave
                AddHandler .Click, AddressOf HI.TL.HandlerControl.RepositoryItemDate_GotFocus
            End With
        With Me.RepFDInvoiceDate
                AddHandler .Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_Leave
                AddHandler .Click, AddressOf HI.TL.HandlerControl.RepositoryItemDate_GotFocus
            End With

    End Sub
End Class