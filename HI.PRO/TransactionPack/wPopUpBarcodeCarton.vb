

Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraTreeList

Public Class wPopUpBarcodeCarton
    Private Shared _Reason As String
    ' Private Shared _frmReject As wShowReject = Nothing

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        HI.TL.HandlerControl.AddHandlerObj(Me)
        _SelectCartonNo = ""
        _Poss = False
        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name, Me)
        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private _Poss As Boolean = False
    Public Property Poss As Boolean
        Get
            Return _Poss
        End Get
        Set(value As Boolean)
            _Poss = value
        End Set
    End Property

    Private _QtyCarton As Integer = 0
    Public Property QtyCarton As Integer
        Get
            Return _QtyCarton
        End Get
        Set(value As Integer)
            _QtyCarton = value
        End Set
    End Property

    Private _FTPackNo As String = ""
    Public Property FTPackNo As String
        Get
            Return _FTPackNo
        End Get
        Set(value As String)
            _FTPackNo = value
        End Set
    End Property

    Private _FNCartonNo3_lbl As String = ""
    Public Property FNCartonNo3_lbl As String
        Get
            Return _FNCartonNo3_lbl
        End Get
        Set(value As String)
            _FNCartonNo3_lbl = value
        End Set
    End Property

    Private _FNCartonNo2_lbl As String = ""
    Public Property FNCartonNo2_lbl As String
        Get
            Return _FNCartonNo2_lbl
        End Get
        Set(value As String)
            _FNCartonNo2_lbl = value
        End Set
    End Property

    Private _oDt As DataTable
    Public Property oDt As DataTable
        Get
            Return _oDt
        End Get
        Set(value As DataTable)
            _oDt = value
        End Set
    End Property

    Private _SelectCartonNo As String = ""
    Public Property SelectCartonNo As String
        Get
            Return _SelectCartonNo
        End Get
        Set(value As String)
            _SelectCartonNo = value
        End Set
    End Property

    Private Sub SBtnExit_Click(sender As Object, e As EventArgs) Handles SBtnExit.Click
        Try
            Poss = True
            _SelectCartonNo = ""
            For Each R As DataRow In _oDt.Rows
                If _SelectCartonNo <> "" Then _SelectCartonNo &= ","
                _SelectCartonNo &= R!FNCartonNo.ToString
            Next
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SBtnOK_Click(sender As Object, e As EventArgs) Handles SBtnOK.Click
        Try
            'If Me.FNCartonNoBegin.Value > Me.FNQtyCarton.Value Then
            '    HI.MG.ShowMsg.mInfo("Pls Check Carton No. !!!!", 1601061620, Me.Text)
            '    Exit Sub
            'End If
            If _SelectCartonNo = "" Then
                HI.MG.ShowMsg.mInfo("Pls Check Carton No. for Generate Barcode Carton !!!!", 1703111328, Me.Text)
                Exit Sub
            End If

            Poss = True
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNQtyCarton_EditValueChanged(sender As Object, e As EventArgs) 'Handles FNQtyCarton.EditValueChanged, FNCartonNoBegin.EditValueChanged
        Try
            'If Me.FNQtyCarton.Value > _QtyCarton Then
            '    HI.MG.ShowMsg.mInfo("ไม่สามารถใส่จำนวนกล่องเกินใบแพ็คได้!!!!!", 1512141632, Me.Text, "", System.Windows.Forms.MessageBoxIcon.Stop)
            '    Me.FNQtyCarton.Value = _QtyCarton
            'End If
        Catch ex As Exception
        End Try
    End Sub

    Private statechecked As Boolean = False
    Private Sub CreateTreeCarton()
        With Me.otlpack
            .ClearNodes()

            .Columns.Clear()

            .Columns.Add() : .Columns.Add()
            .Columns.Add() : .Columns.Add()
            .Columns.Add() : .Columns.Add()
            .Columns.Add() : .Columns.Add()
            .Columns.Add() : .Columns.Add()

            With .Columns.Item(0)
                .Name = "ColKey"
                .Caption = "FTCartonName"
                .FieldName = "FTCartonName"
                .Visible = True
            End With

            With .Columns.Item(1)
                .Name = "FNCartonNo"
                .Caption = "FNCartonNo"
                .FieldName = "FNCartonNo"
                .Visible = False
            End With

            With .Columns.Item(2)
                .Name = "FNQuantity"
                .Caption = "FNQuantity"
                .FieldName = "FNQuantity"
                .Visible = False
            End With

            With .Columns.Item(3)
                .Name = "FNNetWeight"
                .Caption = "FNNetWeight"
                .FieldName = "FNNetWeight"
                .Visible = False
            End With

            With .Columns.Item(4)
                .Name = "FNHSysCartonId"
                .Caption = "FNHSysCartonId"
                .FieldName = "FNHSysCartonId"
                .Visible = False
            End With

            With .Columns.Item(5)
                .Name = "FTCartonCode"
                .Caption = "FTCartonCode"
                .FieldName = "FTCartonCode"
                .Visible = False
            End With

            With .Columns.Item(6)
                .Name = "FNWeight"
                .Caption = "FNWeight"
                .FieldName = "FNWeight"
                .Visible = False
            End With

            With .Columns.Item(7)
                .Name = "FNPackCartonSubType"
                .Caption = "FNPackCartonSubType"
                .FieldName = "FNPackCartonSubType"
                .Visible = False
            End With

            With .Columns.Item(8)
                .Name = "FNPackPerCarton"
                .Caption = "FNPackPerCarton"
                .FieldName = "FNPackPerCarton"
                .Visible = False
            End With

            With .Columns.Item(9)
                .Name = "FTBarCodeCarton"
                .Caption = "FTBarCodeCarton"
                .FieldName = "FTBarCodeCarton"
                .Visible = False
            End With

            With .OptionsView
                .ShowColumns = False
                .ShowHorzLines = False
                .ShowFocusedFrame = False
                .ShowIndicator = False
                .ShowVertLines = False
            End With

            With .OptionsPrint
                .PrintHorzLines = False
                .PrintVertLines = False
                .UsePrintStyles = True
            End With

            With .OptionsMenu
                .EnableFooterMenu = False
            End With

            With .OptionsBehavior
                .AutoNodeHeight = False
                .Editable = False
                .DragNodes = False
                .ResizeNodes = False
                .AllowExpandOnDblClick = True
            End With

            With .OptionsSelection
                .EnableAppearanceFocusedCell = False
                .EnableAppearanceFocusedRow = True
            End With

            With .Appearance
                With .SelectedRow
                    .BackColor = Color.GreenYellow
                    .ForeColor = Color.Blue
                End With
            End With

            .TreeLineStyle = DevExpress.XtraTreeList.LineStyle.None
            .OptionsView.ShowCheckBoxes = True
        End With

        Call InitNodeCarton(Me.otlpack, Nothing)
        Me.otlpack.ExpandAll()

    End Sub

    Private _oCheckbok As DevExpress.XtraEditors.CheckEdit
    Private _PMaxCarton As Integer = 0

    Private Sub InitNodeCarton(ByVal _Lst As DevExpress.XtraTreeList.TreeList, ByVal _Node As DevExpress.XtraTreeList.Nodes.TreeListNode)

        Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode
        Dim nodeChild As DevExpress.XtraTreeList.Nodes.TreeListNode
        Dim _nodeChild As DevExpress.XtraTreeList.Nodes.TreeListNode
        Try
            If (_Node Is Nothing) Then

                _Lst.OptionsView.ShowCheckBoxes = False

                node = _Lst.AppendNode(New Object() {_FNCartonNo3_lbl & "", "-1", "", "", "", "", "", "", "", ""}, _Node)

            End If

            If (_Node Is Nothing) Then
                node.ImageIndex = 0
                Try
                    node.HasChildren = True
                    node.Tag = True

                    Dim dt As DataTable

                    dt = _oDt
                    For Each R As DataRow In dt.Rows
                        _Lst.OptionsView.ShowCheckBoxes = True
                        nodeChild = _Lst.AppendNode(New Object() {_FNCartonNo2_lbl & "" & R!FNCartonNo.ToString & " (" & R!FTCartonInfo.ToString & ")", R!FNCartonNo.ToString, R!FNQuantity.ToString, R!FNNetWeight.ToString, R!FNHSysCartonId.ToString, R!FTCartonCode.ToString, R!FNWeight.ToString, R!FNPackCartonSubType.ToString, R!FNPackPerCarton.ToString, R!FTBarCodeCarton.ToString}, node)
                        nodeChild.HasChildren = False
                        _PMaxCarton = CInt("0" & R!FNCartonNo.ToString)
                    Next
                Catch ex As Exception
                End Try
            Else
                node.HasChildren = False
            End If
        Catch
        End Try
    End Sub

    Private Sub wPopUpBarcodeCarton_Load(sender As Object, e As EventArgs) Handles Me.Load
        _Poss = False
        SelectCartonNo = ""
        Call CreateTreeCarton()
    End Sub

    Private Sub FTStateSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        ' statechecked = Me.FTStateSelectAll.Checked
        Call CreateTreeCarton()
    End Sub

    Private Sub otlpack_Click(sender As Object, e As EventArgs) Handles otlpack.Click
        Try
            With CType(sender, DevExpress.XtraTreeList.TreeList)
                Dim _hifo As TreeListHitInfo = .CalcHitInfo(.PointToClient(Control.MousePosition))
                If (_hifo.Node IsNot Nothing) Then
                    With _hifo.Node

                        If Convert.ToBoolean(.Tag) = False Then

                            Dim _FNCartonNo As String = .GetValue(1).ToString

                            If .CheckState = CheckState.Checked Then
                                If _SelectCartonNo <> "" Then _SelectCartonNo &= ","
                                _SelectCartonNo &= _FNCartonNo
                            Else

                                If Len(_SelectCartonNo) > 1 Then
                                    Dim p As Integer = InStr(_SelectCartonNo, _FNCartonNo)
                                    If p = 1 Then
                                        _SelectCartonNo = Replace(_SelectCartonNo, _FNCartonNo & ",", "")
                                    Else
                                        _SelectCartonNo = Replace(_SelectCartonNo, "," & _FNCartonNo, "")
                                    End If
                                Else
                                    _SelectCartonNo = ""
                                End If

                            End If

                        End If
                    End With
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub



    Private Sub wPopUpBarcodeCarton_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            If Not (_Poss) Then
                SelectCartonNo = ""
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class