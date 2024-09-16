<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wCompareBOM
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        Me.gcCompare = New DevExpress.XtraGrid.GridControl()
        Me.gvCompare = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.FTStyleDevCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStyleDevOriginalCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCompareType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCompareState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCompareCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCompareDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRemark = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.gcCompare, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCompare, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.ocmcancel)
        Me.GroupControl2.Controls.Add(Me.ocmok)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl2.Location = New System.Drawing.Point(2, 536)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.ShowCaption = False
        Me.GroupControl2.Size = New System.Drawing.Size(1185, 41)
        Me.GroupControl2.TabIndex = 288
        Me.GroupControl2.Text = "GroupControl2"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(914, 9)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(145, 25)
        Me.ocmcancel.TabIndex = 107
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "Cancel"
        '
        'ocmok
        '
        Me.ocmok.Location = New System.Drawing.Point(139, 9)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(133, 25)
        Me.ocmok.TabIndex = 106
        Me.ocmok.TabStop = False
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "OK"
        '
        'gcCompare
        '
        Me.gcCompare.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcCompare.Location = New System.Drawing.Point(2, 23)
        Me.gcCompare.MainView = Me.gvCompare
        Me.gcCompare.Name = "gcCompare"
        Me.gcCompare.Size = New System.Drawing.Size(1185, 513)
        Me.gcCompare.TabIndex = 289
        Me.gcCompare.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvCompare})
        '
        'gvCompare
        '
        Me.gvCompare.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTStyleDevCode, Me.FTStyleDevOriginalCode, Me.FNSeq, Me.FTCompareType, Me.FTCompareState, Me.FTCompareCode, Me.FTCompareDesc, Me.FTRemark})
        Me.gvCompare.GridControl = Me.gcCompare
        Me.gvCompare.Name = "gvCompare"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.gcCompare)
        Me.GroupControl1.Controls.Add(Me.GroupControl2)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1189, 579)
        Me.GroupControl1.TabIndex = 290
        Me.GroupControl1.Text = "GroupControl1"
        '
        'FTStyleDevCode
        '
        Me.FTStyleDevCode.Caption = "FTStyleDevCode"
        Me.FTStyleDevCode.FieldName = "FTStyleDevCode"
        Me.FTStyleDevCode.Name = "FTStyleDevCode"
        Me.FTStyleDevCode.Visible = True
        Me.FTStyleDevCode.VisibleIndex = 0
        Me.FTStyleDevCode.Width = 111
        '
        'FTStyleDevOriginalCode
        '
        Me.FTStyleDevOriginalCode.Caption = "FTStyleDevOriginalCode"
        Me.FTStyleDevOriginalCode.FieldName = "FTStyleDevOriginalCode"
        Me.FTStyleDevOriginalCode.Name = "FTStyleDevOriginalCode"
        Me.FTStyleDevOriginalCode.Visible = True
        Me.FTStyleDevOriginalCode.VisibleIndex = 1
        Me.FTStyleDevOriginalCode.Width = 135
        '
        'FNSeq
        '
        Me.FNSeq.Caption = "FNSeq"
        Me.FNSeq.FieldName = "FNSeq"
        Me.FNSeq.Name = "FNSeq"
        Me.FNSeq.Visible = True
        Me.FNSeq.VisibleIndex = 2
        Me.FNSeq.Width = 78
        '
        'FTCompareType
        '
        Me.FTCompareType.Caption = "FTCompareType"
        Me.FTCompareType.FieldName = "FTCompareType"
        Me.FTCompareType.Name = "FTCompareType"
        Me.FTCompareType.Visible = True
        Me.FTCompareType.VisibleIndex = 3
        Me.FTCompareType.Width = 117
        '
        'FTCompareState
        '
        Me.FTCompareState.Caption = "FTCompareState"
        Me.FTCompareState.FieldName = "FTCompareState"
        Me.FTCompareState.Name = "FTCompareState"
        Me.FTCompareState.Visible = True
        Me.FTCompareState.VisibleIndex = 4
        Me.FTCompareState.Width = 126
        '
        'FTCompareCode
        '
        Me.FTCompareCode.Caption = "FTCompareCode"
        Me.FTCompareCode.FieldName = "FTCompareCode"
        Me.FTCompareCode.Name = "FTCompareCode"
        Me.FTCompareCode.Visible = True
        Me.FTCompareCode.VisibleIndex = 5
        Me.FTCompareCode.Width = 192
        '
        'FTCompareDesc
        '
        Me.FTCompareDesc.Caption = "FTCompareDesc"
        Me.FTCompareDesc.FieldName = "FTCompareDesc"
        Me.FTCompareDesc.Name = "FTCompareDesc"
        Me.FTCompareDesc.Visible = True
        Me.FTCompareDesc.VisibleIndex = 6
        Me.FTCompareDesc.Width = 192
        '
        'FTRemark
        '
        Me.FTRemark.Caption = "FTRemark"
        Me.FTRemark.FieldName = "FTRemark"
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.Visible = True
        Me.FTRemark.VisibleIndex = 7
        Me.FTRemark.Width = 211
        '
        'wCompareBOM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1189, 579)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wCompareBOM"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Show Compare BOM"
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.gcCompare, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCompare, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gcCompare As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvCompare As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTStyleDevCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStyleDevOriginalCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCompareType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCompareState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCompareCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCompareDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRemark As DevExpress.XtraGrid.Columns.GridColumn
End Class
