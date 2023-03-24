<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FGWMSInOut
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.olm = New DevExpress.XtraEditors.XtraScrollableControl()
        Me.ocmstart = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmstop = New DevExpress.XtraEditors.SimpleButton()
        Me.FNComport_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ocmRefreshport = New DevExpress.XtraEditors.SimpleButton()
        Me.ogcdataport = New DevExpress.XtraGrid.GridControl()
        Me.ogvdataport = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.CFTComport = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit5 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit6 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit7 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.MemoEdit1 = New DevExpress.XtraEditors.MemoEdit()
        Me.TextEdit1 = New DevExpress.XtraEditors.TextEdit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.ogcdataport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdataport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MemoEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.Controls.Add(Me.olm)
        Me.GroupControl1.Location = New System.Drawing.Point(4, 113)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.ShowCaption = False
        Me.GroupControl1.Size = New System.Drawing.Size(1381, 661)
        Me.GroupControl1.TabIndex = 0
        '
        'olm
        '
        Me.olm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.olm.Location = New System.Drawing.Point(2, 2)
        Me.olm.Name = "olm"
        Me.olm.Size = New System.Drawing.Size(1377, 657)
        Me.olm.TabIndex = 0
        '
        'ocmstart
        '
        Me.ocmstart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmstart.Location = New System.Drawing.Point(1151, 40)
        Me.ocmstart.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmstart.Name = "ocmstart"
        Me.ocmstart.Size = New System.Drawing.Size(212, 31)
        Me.ocmstart.TabIndex = 94
        Me.ocmstart.TabStop = False
        Me.ocmstart.Tag = "2|"
        Me.ocmstart.Text = "START"
        '
        'ocmstop
        '
        Me.ocmstop.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmstop.Location = New System.Drawing.Point(1151, 75)
        Me.ocmstop.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmstop.Name = "ocmstop"
        Me.ocmstop.Size = New System.Drawing.Size(212, 31)
        Me.ocmstop.TabIndex = 95
        Me.ocmstop.TabStop = False
        Me.ocmstop.Tag = "2|"
        Me.ocmstop.Text = "STOP"
        '
        'FNComport_lbl
        '
        Me.FNComport_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNComport_lbl.Appearance.Options.UseForeColor = True
        Me.FNComport_lbl.Appearance.Options.UseTextOptions = True
        Me.FNComport_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNComport_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNComport_lbl.Location = New System.Drawing.Point(18, 13)
        Me.FNComport_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNComport_lbl.Name = "FNComport_lbl"
        Me.FNComport_lbl.Size = New System.Drawing.Size(134, 23)
        Me.FNComport_lbl.TabIndex = 255
        Me.FNComport_lbl.Tag = "2|"
        Me.FNComport_lbl.Text = "COM PORT :"
        '
        'ocmRefreshport
        '
        Me.ocmRefreshport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmRefreshport.Location = New System.Drawing.Point(1151, 5)
        Me.ocmRefreshport.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmRefreshport.Name = "ocmRefreshport"
        Me.ocmRefreshport.Size = New System.Drawing.Size(212, 31)
        Me.ocmRefreshport.TabIndex = 256
        Me.ocmRefreshport.TabStop = False
        Me.ocmRefreshport.Tag = "2|"
        Me.ocmRefreshport.Text = "REFRESH PORT"
        '
        'ogcdataport
        '
        Me.ogcdataport.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdataport.Location = New System.Drawing.Point(162, 8)
        Me.ogcdataport.MainView = Me.ogvdataport
        Me.ogcdataport.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdataport.Name = "ogcdataport"
        Me.ogcdataport.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemCheckEdit2, Me.RepositoryItemCheckEdit3, Me.RepositoryItemCheckEdit4, Me.RepositoryItemCheckEdit5, Me.RepositoryItemCheckEdit6, Me.RepositoryItemCheckEdit7, Me.ReposSelect})
        Me.ogcdataport.Size = New System.Drawing.Size(427, 98)
        Me.ogcdataport.TabIndex = 257
        Me.ogcdataport.TabStop = False
        Me.ogcdataport.Tag = "2|"
        Me.ogcdataport.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdataport})
        '
        'ogvdataport
        '
        Me.ogvdataport.Appearance.EvenRow.BackColor = System.Drawing.Color.LemonChiffon
        Me.ogvdataport.Appearance.EvenRow.Options.UseBackColor = True
        Me.ogvdataport.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFTSelect, Me.CFTComport})
        Me.ogvdataport.GridControl = Me.ogcdataport
        Me.ogvdataport.Name = "ogvdataport"
        Me.ogvdataport.OptionsCustomization.AllowGroup = False
        Me.ogvdataport.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdataport.OptionsView.ColumnAutoWidth = False
        Me.ogvdataport.OptionsView.EnableAppearanceEvenRow = True
        Me.ogvdataport.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdataport.OptionsView.ShowGroupPanel = False
        Me.ogvdataport.Tag = "2|"
        '
        'CFTSelect
        '
        Me.CFTSelect.Caption = "Select"
        Me.CFTSelect.ColumnEdit = Me.ReposSelect
        Me.CFTSelect.FieldName = "FTSelect"
        Me.CFTSelect.Name = "CFTSelect"
        Me.CFTSelect.OptionsColumn.AllowMove = False
        Me.CFTSelect.Visible = True
        Me.CFTSelect.VisibleIndex = 0
        '
        'ReposSelect
        '
        Me.ReposSelect.AutoHeight = False
        Me.ReposSelect.Caption = "Check"
        Me.ReposSelect.Name = "ReposSelect"
        Me.ReposSelect.ValueChecked = "1"
        Me.ReposSelect.ValueUnchecked = "0"
        '
        'CFTComport
        '
        Me.CFTComport.Caption = "Comport"
        Me.CFTComport.FieldName = "FTComport"
        Me.CFTComport.Name = "CFTComport"
        Me.CFTComport.OptionsColumn.AllowEdit = False
        Me.CFTComport.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTComport.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTComport.OptionsColumn.AllowMove = False
        Me.CFTComport.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTComport.OptionsColumn.ReadOnly = True
        Me.CFTComport.Visible = True
        Me.CFTComport.VisibleIndex = 1
        Me.CFTComport.Width = 245
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Caption = "Check"
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        Me.RepositoryItemCheckEdit2.ValueChecked = "1"
        Me.RepositoryItemCheckEdit2.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit3
        '
        Me.RepositoryItemCheckEdit3.AutoHeight = False
        Me.RepositoryItemCheckEdit3.Caption = "Check"
        Me.RepositoryItemCheckEdit3.Name = "RepositoryItemCheckEdit3"
        Me.RepositoryItemCheckEdit3.ValueChecked = "1"
        Me.RepositoryItemCheckEdit3.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit4
        '
        Me.RepositoryItemCheckEdit4.AutoHeight = False
        Me.RepositoryItemCheckEdit4.Caption = "Check"
        Me.RepositoryItemCheckEdit4.Name = "RepositoryItemCheckEdit4"
        Me.RepositoryItemCheckEdit4.ValueChecked = "1"
        Me.RepositoryItemCheckEdit4.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit5
        '
        Me.RepositoryItemCheckEdit5.AutoHeight = False
        Me.RepositoryItemCheckEdit5.Caption = "Check"
        Me.RepositoryItemCheckEdit5.Name = "RepositoryItemCheckEdit5"
        Me.RepositoryItemCheckEdit5.ValueChecked = "1"
        Me.RepositoryItemCheckEdit5.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit6
        '
        Me.RepositoryItemCheckEdit6.AutoHeight = False
        Me.RepositoryItemCheckEdit6.Caption = "Check"
        Me.RepositoryItemCheckEdit6.Name = "RepositoryItemCheckEdit6"
        Me.RepositoryItemCheckEdit6.ValueChecked = "1"
        Me.RepositoryItemCheckEdit6.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit7
        '
        Me.RepositoryItemCheckEdit7.AutoHeight = False
        Me.RepositoryItemCheckEdit7.Caption = "Check"
        Me.RepositoryItemCheckEdit7.Name = "RepositoryItemCheckEdit7"
        Me.RepositoryItemCheckEdit7.ValueChecked = "1"
        Me.RepositoryItemCheckEdit7.ValueUnchecked = "0"
        '
        'MemoEdit1
        '
        Me.MemoEdit1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MemoEdit1.Location = New System.Drawing.Point(595, 5)
        Me.MemoEdit1.Name = "MemoEdit1"
        Me.MemoEdit1.Size = New System.Drawing.Size(550, 66)
        Me.MemoEdit1.TabIndex = 258
        '
        'TextEdit1
        '
        Me.TextEdit1.Location = New System.Drawing.Point(614, 79)
        Me.TextEdit1.Name = "TextEdit1"
        Me.TextEdit1.Size = New System.Drawing.Size(407, 23)
        Me.TextEdit1.TabIndex = 259
        '
        'FGWMSInOut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1390, 786)
        Me.Controls.Add(Me.TextEdit1)
        Me.Controls.Add(Me.MemoEdit1)
        Me.Controls.Add(Me.ogcdataport)
        Me.Controls.Add(Me.ocmRefreshport)
        Me.Controls.Add(Me.FNComport_lbl)
        Me.Controls.Add(Me.ocmstop)
        Me.Controls.Add(Me.ocmstart)
        Me.Controls.Add(Me.GroupControl1)
        Me.Name = "FGWMSInOut"
        Me.Tag = "999"
        Me.Text = "Form1"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.ogcdataport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdataport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MemoEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmstart As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmstop As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNComport_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ocmRefreshport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogcdataport As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdataport As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CFTComport As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit5 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit6 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit7 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents CFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents olm As DevExpress.XtraEditors.XtraScrollableControl
    Friend WithEvents MemoEdit1 As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents TextEdit1 As DevExpress.XtraEditors.TextEdit
End Class
