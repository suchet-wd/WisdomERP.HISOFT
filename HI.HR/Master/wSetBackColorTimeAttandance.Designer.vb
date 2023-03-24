<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wSetBackColorTimeAttandance
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTLeaveType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTLeaveName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTColor = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTColor = New DevExpress.XtraEditors.Repository.RepositoryItemColorEdit()
        Me.FTColorString = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTColor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbdetail
        '
        Me.ogbdetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbdetail.Controls.Add(Me.ogc)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdetail.Location = New System.Drawing.Point(0, 0)
        Me.ogbdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.Size = New System.Drawing.Size(1276, 789)
        Me.ogbdetail.TabIndex = 13
        Me.ogbdetail.Tag = "2|"
        Me.ogbdetail.Text = "หัวข้อการอบรม"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsave)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(113, 188)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(1076, 58)
        Me.ogbmainprocbutton.TabIndex = 136
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(128, 14)
        Me.ocmclear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(111, 31)
        Me.ocmclear.TabIndex = 97
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(948, 14)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(10, 14)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(111, 31)
        Me.ocmsave.TabIndex = 93
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "Save"
        '
        'ogc
        '
        Me.ogc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogc.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogc.Location = New System.Drawing.Point(3, 30)
        Me.ogc.MainView = Me.ogv
        Me.ogc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogc.Name = "ogc"
        Me.ogc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFTColor})
        Me.ogc.Size = New System.Drawing.Size(1268, 754)
        Me.ogc.TabIndex = 1
        Me.ogc.TabStop = False
        Me.ogc.Tag = "2|"
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv})
        '
        'ogv
        '
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTLeaveType, Me.FTLeaveName, Me.FTColor, Me.FTColorString})
        Me.ogv.GridControl = Me.ogc
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsCustomization.AllowGroup = False
        Me.ogv.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogv.OptionsView.ShowGroupPanel = False
        Me.ogv.Tag = "2|"
        '
        'FTLeaveType
        '
        Me.FTLeaveType.Caption = "FTLeaveType"
        Me.FTLeaveType.FieldName = "FTLeaveType"
        Me.FTLeaveType.Name = "FTLeaveType"
        Me.FTLeaveType.OptionsColumn.AllowEdit = False
        Me.FTLeaveType.OptionsColumn.ReadOnly = True
        Me.FTLeaveType.Width = 101
        '
        'FTLeaveName
        '
        Me.FTLeaveName.Caption = "FTLeaveName"
        Me.FTLeaveName.FieldName = "FTLeaveName"
        Me.FTLeaveName.Name = "FTLeaveName"
        Me.FTLeaveName.OptionsColumn.AllowEdit = False
        Me.FTLeaveName.OptionsColumn.ReadOnly = True
        Me.FTLeaveName.Visible = True
        Me.FTLeaveName.VisibleIndex = 0
        Me.FTLeaveName.Width = 242
        '
        'FTColor
        '
        Me.FTColor.Caption = "FTColor"
        Me.FTColor.ColumnEdit = Me.RepFTColor
        Me.FTColor.FieldName = "FTColor"
        Me.FTColor.Name = "FTColor"
        Me.FTColor.OptionsColumn.ShowInCustomizationForm = False
        Me.FTColor.Visible = True
        Me.FTColor.VisibleIndex = 1
        Me.FTColor.Width = 829
        '
        'RepFTColor
        '
        Me.RepFTColor.AutoHeight = False
        Me.RepFTColor.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepFTColor.Name = "RepFTColor"
        '
        'FTColorString
        '
        Me.FTColorString.Caption = "FTColorString"
        Me.FTColorString.FieldName = "FTColorString"
        Me.FTColorString.Name = "FTColorString"
        '
        'wSetBackColorTimeAttandance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1276, 789)
        Me.Controls.Add(Me.ogbdetail)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wSetBackColorTimeAttandance"
        Me.Text = "wSetBackColorTimeAttandance"
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTColor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTLeaveType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTLeaveName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTColor As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTColor As DevExpress.XtraEditors.Repository.RepositoryItemColorEdit
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTColorString As DevExpress.XtraGrid.Columns.GridColumn
End Class
