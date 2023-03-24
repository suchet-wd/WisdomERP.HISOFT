<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wMailNew2
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
        Me.FTMailSubject = New DevExpress.XtraEditors.TextEdit()
        Me.FTMailTo = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.FNMailStatePriority = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FNMailStatePriority_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.FNMailFileAttach_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.FTMailText2 = New DevExpress.XtraRichEdit.RichEditControl()
        Me.ogcTMAILFileAttach = New DevExpress.XtraGrid.GridControl()
        Me.ogvTMAILFileAttach = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.FTMailSubject.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTMailTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNMailStatePriority.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcTMAILFileAttach, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvTMAILFileAttach, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FTMailSubject
        '
        Me.FTMailSubject.Location = New System.Drawing.Point(85, 80)
        Me.FTMailSubject.Name = "FTMailSubject"
        Me.FTMailSubject.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTMailSubject.Properties.Appearance.Options.UseBackColor = True
        Me.FTMailSubject.Size = New System.Drawing.Size(675, 20)
        Me.FTMailSubject.TabIndex = 8
        '
        'FTMailTo
        '
        Me.FTMailTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTMailTo.Location = New System.Drawing.Point(85, 43)
        Me.FTMailTo.Name = "FTMailTo"
        Me.FTMailTo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTMailTo.Properties.Appearance.Options.UseBackColor = True
        Me.FTMailTo.Size = New System.Drawing.Size(675, 20)
        Me.FTMailTo.TabIndex = 7
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(25, 83)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(43, 13)
        Me.LabelControl2.TabIndex = 6
        Me.LabelControl2.Text = "Subject :"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(49, 50)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(24, 13)
        Me.LabelControl1.TabIndex = 5
        Me.LabelControl1.Text = "TO : "
        '
        'FNMailStatePriority
        '
        Me.FNMailStatePriority.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.FNMailStatePriority.Location = New System.Drawing.Point(928, 13)
        Me.FNMailStatePriority.Name = "FNMailStatePriority"
        Me.FNMailStatePriority.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNMailStatePriority.Properties.Tag = "Priority"
        Me.FNMailStatePriority.Size = New System.Drawing.Size(145, 20)
        Me.FNMailStatePriority.TabIndex = 11
        Me.FNMailStatePriority.Tag = "2|"
        '
        'FNMailStatePriority_lbl
        '
        Me.FNMailStatePriority_lbl.Location = New System.Drawing.Point(873, 20)
        Me.FNMailStatePriority_lbl.Name = "FNMailStatePriority_lbl"
        Me.FNMailStatePriority_lbl.Size = New System.Drawing.Size(44, 13)
        Me.FNMailStatePriority_lbl.TabIndex = 12
        Me.FNMailStatePriority_lbl.Text = "Priority  :"
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.FileName = "OpenFileDialog1"
        '
        'FNMailFileAttach_lbl
        '
        Me.FNMailFileAttach_lbl.Location = New System.Drawing.Point(791, 50)
        Me.FNMailFileAttach_lbl.Name = "FNMailFileAttach_lbl"
        Me.FNMailFileAttach_lbl.Size = New System.Drawing.Size(64, 13)
        Me.FNMailFileAttach_lbl.TabIndex = 16
        Me.FNMailFileAttach_lbl.Text = "Attach File  : "
        '
        'SimpleButton1
        '
        'Me.SimpleButton1.Image = Global.HI.Mail.My.Resources.Resources.AttachmentObject_16x16
        Me.SimpleButton1.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.SimpleButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.SimpleButton1.Location = New System.Drawing.Point(856, 47)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(27, 26)
        Me.SimpleButton1.TabIndex = 15
        '
        'FTMailText2
        '
        Me.FTMailText2.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple
        Me.FTMailText2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.FTMailText2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.FTMailText2.Location = New System.Drawing.Point(13, 116)
        Me.FTMailText2.Name = "FTMailText2"
        Me.FTMailText2.Options.Fields.UseCurrentCultureDateTimeFormat = False
        Me.FTMailText2.Options.MailMerge.KeepLastParagraph = False
        Me.FTMailText2.Size = New System.Drawing.Size(1077, 427)
        Me.FTMailText2.TabIndex = 10
        '
        'ogcTMAILFileAttach
        '
        Me.ogcTMAILFileAttach.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcTMAILFileAttach.Location = New System.Drawing.Point(888, 47)
        Me.ogcTMAILFileAttach.MainView = Me.ogvTMAILFileAttach
        Me.ogcTMAILFileAttach.Name = "ogcTMAILFileAttach"
        Me.ogcTMAILFileAttach.Size = New System.Drawing.Size(185, 63)
        Me.ogcTMAILFileAttach.TabIndex = 154
        Me.ogcTMAILFileAttach.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvTMAILFileAttach})
        '
        'ogvTMAILFileAttach
        '
        Me.ogvTMAILFileAttach.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.ogvTMAILFileAttach.GridControl = Me.ogcTMAILFileAttach
        Me.ogvTMAILFileAttach.Name = "ogvTMAILFileAttach"
        Me.ogvTMAILFileAttach.OptionsView.ShowColumnHeaders = False
        Me.ogvTMAILFileAttach.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "GridColumn1"
        Me.GridColumn1.FieldName = "FTMailId"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.ShowCaption = False
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Attach File  "
        Me.GridColumn2.FieldName = "FNMailFileAttach"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.ReadOnly = True
        Me.GridColumn2.OptionsColumn.ShowCaption = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'wMailNew2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1102, 547)
        Me.Controls.Add(Me.ogcTMAILFileAttach)
        Me.Controls.Add(Me.FTMailText2)
        Me.Controls.Add(Me.FNMailFileAttach_lbl)
        Me.Controls.Add(Me.SimpleButton1)
        Me.Controls.Add(Me.FNMailStatePriority_lbl)
        Me.Controls.Add(Me.FNMailStatePriority)
        Me.Controls.Add(Me.FTMailSubject)
        Me.Controls.Add(Me.FTMailTo)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wMailNew2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wMailNew2"
        CType(Me.FTMailSubject.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTMailTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNMailStatePriority.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcTMAILFileAttach, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvTMAILFileAttach, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents FTMailSubject As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTMailTo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNMailStatePriority As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNMailStatePriority_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents FNMailFileAttach_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTMailText2 As DevExpress.XtraRichEdit.RichEditControl
    Friend WithEvents ogcTMAILFileAttach As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvTMAILFileAttach As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
End Class
