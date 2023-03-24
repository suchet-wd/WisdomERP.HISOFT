<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wCopyStylePoint
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFNHSysStyleId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTStyleName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysStyleId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysStyleId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysStyleId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.obtCopy = New DevExpress.XtraEditors.SimpleButton()
        Me.obtClose = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.Controls.Add(Me.ogcdetail)
        Me.GroupControl1.Controls.Add(Me.FNHSysStyleId)
        Me.GroupControl1.Controls.Add(Me.FNHSysStyleId_None)
        Me.GroupControl1.Controls.Add(Me.FNHSysStyleId_lbl)
        Me.GroupControl1.Location = New System.Drawing.Point(2, 1)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.ShowCaption = False
        Me.GroupControl1.Size = New System.Drawing.Size(658, 208)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "GroupControl1"
        '
        'ogcdetail
        '
        Me.ogcdetail.Location = New System.Drawing.Point(140, 35)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.Size = New System.Drawing.Size(506, 168)
        Me.ogcdetail.TabIndex = 444
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFNHSysStyleId, Me.cFTStyleCode, Me.cFTStyleName})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        '
        'cFNHSysStyleId
        '
        Me.cFNHSysStyleId.Caption = "FNHSysStyleId"
        Me.cFNHSysStyleId.FieldName = "FNHSysStyleId"
        Me.cFNHSysStyleId.Name = "cFNHSysStyleId"
        Me.cFNHSysStyleId.OptionsColumn.AllowEdit = False
        Me.cFNHSysStyleId.OptionsFilter.AllowAutoFilter = False
        Me.cFNHSysStyleId.Width = 88
        '
        'cFTStyleCode
        '
        Me.cFTStyleCode.Caption = "FTStyleCode"
        Me.cFTStyleCode.FieldName = "FTStyleCode"
        Me.cFTStyleCode.Name = "cFTStyleCode"
        Me.cFTStyleCode.Visible = True
        Me.cFTStyleCode.VisibleIndex = 0
        Me.cFTStyleCode.Width = 131
        '
        'cFTStyleName
        '
        Me.cFTStyleName.Caption = "FTStyleName"
        Me.cFTStyleName.FieldName = "FTStyleName"
        Me.cFTStyleName.Name = "cFTStyleName"
        Me.cFTStyleName.Visible = True
        Me.cFTStyleName.VisibleIndex = 1
        Me.cFTStyleName.Width = 599
        '
        'FNHSysStyleId
        '
        Me.FNHSysStyleId.Location = New System.Drawing.Point(140, 11)
        Me.FNHSysStyleId.Name = "FNHSysStyleId"
        Me.FNHSysStyleId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "89", Nothing, True)})
        Me.FNHSysStyleId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysStyleId.Properties.Tag = ""
        Me.FNHSysStyleId.Size = New System.Drawing.Size(129, 20)
        Me.FNHSysStyleId.TabIndex = 442
        Me.FNHSysStyleId.Tag = ""
        '
        'FNHSysStyleId_None
        '
        Me.FNHSysStyleId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysStyleId_None.Location = New System.Drawing.Point(274, 11)
        Me.FNHSysStyleId_None.Name = "FNHSysStyleId_None"
        Me.FNHSysStyleId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysStyleId_None.Properties.ReadOnly = True
        Me.FNHSysStyleId_None.Size = New System.Drawing.Size(372, 20)
        Me.FNHSysStyleId_None.TabIndex = 443
        Me.FNHSysStyleId_None.Tag = ""
        '
        'FNHSysStyleId_lbl
        '
        Me.FNHSysStyleId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysStyleId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysStyleId_lbl.Location = New System.Drawing.Point(10, 11)
        Me.FNHSysStyleId_lbl.Name = "FNHSysStyleId_lbl"
        Me.FNHSysStyleId_lbl.Size = New System.Drawing.Size(128, 18)
        Me.FNHSysStyleId_lbl.TabIndex = 440
        Me.FNHSysStyleId_lbl.Tag = "2|"
        Me.FNHSysStyleId_lbl.Text = "To  FNHSysStyleId :"
        '
        'obtCopy
        '
        Me.obtCopy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.obtCopy.Location = New System.Drawing.Point(492, 220)
        Me.obtCopy.Name = "obtCopy"
        Me.obtCopy.Size = New System.Drawing.Size(75, 23)
        Me.obtCopy.TabIndex = 1
        Me.obtCopy.Text = "COPY"
        '
        'obtClose
        '
        Me.obtClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.obtClose.Location = New System.Drawing.Point(573, 220)
        Me.obtClose.Name = "obtClose"
        Me.obtClose.Size = New System.Drawing.Size(75, 23)
        Me.obtClose.TabIndex = 1
        Me.obtClose.Text = "CLOSE"
        '
        'wCopyStylePoint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(661, 250)
        Me.Controls.Add(Me.obtClose)
        Me.Controls.Add(Me.obtCopy)
        Me.Controls.Add(Me.GroupControl1)
        Me.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.Name = "wCopyStylePoint"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Copy Point Style"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents obtCopy As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents obtClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNHSysStyleId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFNHSysStyleId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTStyleName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysStyleId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysStyleId_None As DevExpress.XtraEditors.TextEdit
End Class
