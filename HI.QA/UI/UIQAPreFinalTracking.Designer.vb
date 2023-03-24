<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UIQAPreFinalTracking
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.opnheader = New DevExpress.XtraEditors.PanelControl()
        Me.FNTheQuality_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNTheQuality = New DevExpress.XtraEditors.CalcEdit()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridView()
        CType(Me.opnheader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.opnheader.SuspendLayout()
        CType(Me.FNTheQuality.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'opnheader
        '
        Me.opnheader.Controls.Add(Me.FNTheQuality_lbl)
        Me.opnheader.Controls.Add(Me.FNTheQuality)
        Me.opnheader.Dock = System.Windows.Forms.DockStyle.Top
        Me.opnheader.Location = New System.Drawing.Point(0, 0)
        Me.opnheader.Name = "opnheader"
        Me.opnheader.Size = New System.Drawing.Size(1347, 49)
        Me.opnheader.TabIndex = 0
        '
        'FNTheQuality_lbl
        '
        Me.FNTheQuality_lbl.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNTheQuality_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNTheQuality_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTheQuality_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNTheQuality_lbl.Location = New System.Drawing.Point(865, 14)
        Me.FNTheQuality_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNTheQuality_lbl.Name = "FNTheQuality_lbl"
        Me.FNTheQuality_lbl.Size = New System.Drawing.Size(258, 23)
        Me.FNTheQuality_lbl.TabIndex = 480
        Me.FNTheQuality_lbl.Tag = "2|"
        Me.FNTheQuality_lbl.Text = "ค่าคุณภาพ :"
        '
        'FNTheQuality
        '
        Me.FNTheQuality.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNTheQuality.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.FNTheQuality.EnterMoveNextControl = True
        Me.FNTheQuality.Location = New System.Drawing.Point(1128, 10)
        Me.FNTheQuality.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNTheQuality.Name = "FNTheQuality"
        Me.FNTheQuality.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.FNTheQuality.Properties.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNTheQuality.Properties.Appearance.Options.UseFont = True
        Me.FNTheQuality.Properties.Appearance.Options.UseForeColor = True
        Me.FNTheQuality.Properties.Appearance.Options.UseTextOptions = True
        Me.FNTheQuality.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTheQuality.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNTheQuality.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNTheQuality.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNTheQuality.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNTheQuality.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNTheQuality.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNTheQuality.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNTheQuality.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNTheQuality.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.FNTheQuality.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTheQuality.Properties.Precision = 2
        Me.FNTheQuality.Size = New System.Drawing.Size(175, 30)
        Me.FNTheQuality.TabIndex = 5
        Me.FNTheQuality.Tag = "2|"
        '
        'GridColumn1
        '
        Me.GridColumn1.Name = "GridColumn1"
        '
        'ogcdetail
        '
        Me.ogcdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Location = New System.Drawing.Point(0, 49)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.Size = New System.Drawing.Size(1347, 674)
        Me.ogcdetail.TabIndex = 395
        Me.ogcdetail.TabStop = False
        Me.ogcdetail.Tag = "2|"
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdetail.OptionsPrint.PrintHeader = False
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdetail.OptionsView.ShowColumnHeaders = False
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        '
        'UIQAPreFinalTracking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ogcdetail)
        Me.Controls.Add(Me.opnheader)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "UIQAPreFinalTracking"
        Me.Size = New System.Drawing.Size(1347, 723)
        CType(Me.opnheader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.opnheader.ResumeLayout(False)
        CType(Me.FNTheQuality.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents opnheader As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView
    Friend WithEvents FNTheQuality As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNTheQuality_lbl As DevExpress.XtraEditors.LabelControl


End Class
