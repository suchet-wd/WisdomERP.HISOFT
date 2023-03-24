<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wBreakTime
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
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.oGrpDocument = New DevExpress.XtraEditors.GroupControl()
        Me.FNHSysPeriodOfTimeId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNConfigTime = New DevExpress.XtraEditors.CalcEdit()
        Me.FNHSysPeriodOfTimeId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysUnitSectId = New DevExpress.XtraEditors.ButtonEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysPeriodOfTimeId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysUnitSectId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.oGrpDetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmdelete = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ogcTime = New DevExpress.XtraGrid.GridControl()
        Me.ogvTime = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTUnitSectCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNHSysUnitSectId = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.oGrpDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.oGrpDocument.SuspendLayout()
        CType(Me.FNHSysPeriodOfTimeId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNConfigTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysPeriodOfTimeId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysUnitSectId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oGrpDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.oGrpDetail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogcTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'oGrpDocument
        '
        Me.oGrpDocument.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.oGrpDocument.Controls.Add(Me.FNHSysPeriodOfTimeId_None)
        Me.oGrpDocument.Controls.Add(Me.FNConfigTime)
        Me.oGrpDocument.Controls.Add(Me.FNHSysPeriodOfTimeId)
        Me.oGrpDocument.Controls.Add(Me.FNHSysUnitSectId)
        Me.oGrpDocument.Controls.Add(Me.LabelControl3)
        Me.oGrpDocument.Controls.Add(Me.FNHSysPeriodOfTimeId_lbl)
        Me.oGrpDocument.Controls.Add(Me.FNHSysUnitSectId_lbl)
        Me.oGrpDocument.Location = New System.Drawing.Point(0, 2)
        Me.oGrpDocument.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oGrpDocument.Name = "oGrpDocument"
        Me.oGrpDocument.Size = New System.Drawing.Size(1156, 78)
        Me.oGrpDocument.TabIndex = 0
        Me.oGrpDocument.Text = "Document"
        '
        'FNHSysPeriodOfTimeId_None
        '
        Me.FNHSysPeriodOfTimeId_None.Location = New System.Drawing.Point(561, 42)
        Me.FNHSysPeriodOfTimeId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysPeriodOfTimeId_None.Name = "FNHSysPeriodOfTimeId_None"
        Me.FNHSysPeriodOfTimeId_None.Properties.ReadOnly = True
        Me.FNHSysPeriodOfTimeId_None.Size = New System.Drawing.Size(247, 22)
        Me.FNHSysPeriodOfTimeId_None.TabIndex = 3
        Me.FNHSysPeriodOfTimeId_None.Tag = "2|"
        '
        'FNConfigTime
        '
        Me.FNConfigTime.Location = New System.Drawing.Point(892, 42)
        Me.FNConfigTime.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNConfigTime.Name = "FNConfigTime"
        Me.FNConfigTime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.FNConfigTime.Properties.Precision = 0
        Me.FNConfigTime.Size = New System.Drawing.Size(117, 22)
        Me.FNConfigTime.TabIndex = 2
        Me.FNConfigTime.Tag = "2|"
        '
        'FNHSysPeriodOfTimeId
        '
        Me.FNHSysPeriodOfTimeId.Location = New System.Drawing.Point(425, 42)
        Me.FNHSysPeriodOfTimeId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysPeriodOfTimeId.Name = "FNHSysPeriodOfTimeId"
        Me.FNHSysPeriodOfTimeId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "242", Nothing, True)})
        Me.FNHSysPeriodOfTimeId.Size = New System.Drawing.Size(128, 22)
        Me.FNHSysPeriodOfTimeId.TabIndex = 1
        Me.FNHSysPeriodOfTimeId.Tag = "2|"
        '
        'FNHSysUnitSectId
        '
        Me.FNHSysUnitSectId.Location = New System.Drawing.Point(132, 42)
        Me.FNHSysUnitSectId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysUnitSectId.Name = "FNHSysUnitSectId"
        Me.FNHSysUnitSectId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "165", Nothing, True)})
        Me.FNHSysUnitSectId.Size = New System.Drawing.Size(181, 22)
        Me.FNHSysUnitSectId.TabIndex = 0
        Me.FNHSysUnitSectId.Tag = "2|"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl3.Location = New System.Drawing.Point(815, 42)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(69, 25)
        Me.LabelControl3.TabIndex = 0
        Me.LabelControl3.Text = "Cut Time :"
        '
        'FNHSysPeriodOfTimeId_lbl
        '
        Me.FNHSysPeriodOfTimeId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysPeriodOfTimeId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysPeriodOfTimeId_lbl.Location = New System.Drawing.Point(320, 42)
        Me.FNHSysPeriodOfTimeId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysPeriodOfTimeId_lbl.Name = "FNHSysPeriodOfTimeId_lbl"
        Me.FNHSysPeriodOfTimeId_lbl.Size = New System.Drawing.Size(98, 20)
        Me.FNHSysPeriodOfTimeId_lbl.TabIndex = 0
        Me.FNHSysPeriodOfTimeId_lbl.Text = "Hour :"
        '
        'FNHSysUnitSectId_lbl
        '
        Me.FNHSysUnitSectId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNHSysUnitSectId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysUnitSectId_lbl.Location = New System.Drawing.Point(6, 42)
        Me.FNHSysUnitSectId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysUnitSectId_lbl.Name = "FNHSysUnitSectId_lbl"
        Me.FNHSysUnitSectId_lbl.Size = New System.Drawing.Size(119, 25)
        Me.FNHSysUnitSectId_lbl.TabIndex = 0
        Me.FNHSysUnitSectId_lbl.Text = "FNHSysUnitSectId :"
        '
        'oGrpDetail
        '
        Me.oGrpDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.oGrpDetail.Controls.Add(Me.ogbmainprocbutton)
        Me.oGrpDetail.Controls.Add(Me.ogcTime)
        Me.oGrpDetail.Location = New System.Drawing.Point(0, 87)
        Me.oGrpDetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oGrpDetail.Name = "oGrpDetail"
        Me.oGrpDetail.Size = New System.Drawing.Size(1156, 646)
        Me.oGrpDetail.TabIndex = 1
        Me.oGrpDetail.Text = "Detail"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmdelete)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsave)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(97, 294)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(961, 58)
        Me.ogbmainprocbutton.TabIndex = 388
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmdelete
        '
        Me.ocmdelete.Location = New System.Drawing.Point(685, 12)
        Me.ocmdelete.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmdelete.Name = "ocmdelete"
        Me.ocmdelete.Size = New System.Drawing.Size(68, 31)
        Me.ocmdelete.TabIndex = 334
        Me.ocmdelete.TabStop = False
        Me.ocmdelete.Tag = "2|"
        Me.ocmdelete.Text = "DELETE"
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(594, 10)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(84, 31)
        Me.ocmsave.TabIndex = 333
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "SAVE"
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Location = New System.Drawing.Point(433, 15)
        Me.ocmsavelayout.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsavelayout.Name = "ocmsavelayout"
        Me.ocmsavelayout.Size = New System.Drawing.Size(136, 28)
        Me.ocmsavelayout.TabIndex = 332
        Me.ocmsavelayout.Text = "savelayoutgrid"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(829, 14)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(16, 12)
        Me.ocmclear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(111, 31)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        Me.ocmclear.Visible = False
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(133, 17)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(136, 28)
        Me.ocmload.TabIndex = 329
        Me.ocmload.Text = "Load Data"
        '
        'ogcTime
        '
        Me.ogcTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcTime.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcTime.Location = New System.Drawing.Point(2, 24)
        Me.ogcTime.MainView = Me.ogvTime
        Me.ogcTime.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcTime.Name = "ogcTime"
        Me.ogcTime.Size = New System.Drawing.Size(1152, 620)
        Me.ogcTime.TabIndex = 0
        Me.ogcTime.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvTime})
        '
        'ogvTime
        '
        Me.ogvTime.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTUnitSectCode, Me.cFNHSysUnitSectId})
        Me.ogvTime.GridControl = Me.ogcTime
        Me.ogvTime.Name = "ogvTime"
        Me.ogvTime.OptionsFilter.AllowFilterEditor = False
        Me.ogvTime.OptionsFind.AllowFindPanel = False
        Me.ogvTime.OptionsView.ColumnAutoWidth = False
        Me.ogvTime.OptionsView.ShowGroupPanel = False
        '
        'FTUnitSectCode
        '
        Me.FTUnitSectCode.AppearanceCell.BackColor = System.Drawing.Color.Transparent
        Me.FTUnitSectCode.AppearanceCell.BackColor2 = System.Drawing.Color.Transparent
        Me.FTUnitSectCode.AppearanceCell.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.FTUnitSectCode.AppearanceCell.Options.UseBackColor = True
        Me.FTUnitSectCode.AppearanceHeader.BackColor = System.Drawing.Color.Transparent
        Me.FTUnitSectCode.AppearanceHeader.BackColor2 = System.Drawing.Color.White
        Me.FTUnitSectCode.AppearanceHeader.Options.UseBackColor = True
        Me.FTUnitSectCode.Caption = "FTUnitSectCode"
        Me.FTUnitSectCode.FieldName = "FTUnitSectCode"
        Me.FTUnitSectCode.Name = "FTUnitSectCode"
        Me.FTUnitSectCode.OptionsColumn.AllowEdit = False
        Me.FTUnitSectCode.Visible = True
        Me.FTUnitSectCode.VisibleIndex = 0
        Me.FTUnitSectCode.Width = 85
        '
        'cFNHSysUnitSectId
        '
        Me.cFNHSysUnitSectId.Caption = "FNHSysUnitSectId"
        Me.cFNHSysUnitSectId.FieldName = "FNHSysUnitSectIdM"
        Me.cFNHSysUnitSectId.Name = "cFNHSysUnitSectId"
        '
        'wBreakTime
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1156, 734)
        Me.Controls.Add(Me.oGrpDetail)
        Me.Controls.Add(Me.oGrpDocument)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wBreakTime"
        Me.Text = "wBreakTime"
        CType(Me.oGrpDocument, System.ComponentModel.ISupportInitialize).EndInit()
        Me.oGrpDocument.ResumeLayout(False)
        CType(Me.FNHSysPeriodOfTimeId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNConfigTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysPeriodOfTimeId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysUnitSectId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oGrpDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.oGrpDetail.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogcTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents oGrpDocument As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FNHSysPeriodOfTimeId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysUnitSectId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysPeriodOfTimeId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysUnitSectId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents oGrpDetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcTime As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvTime As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTUnitSectCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNConfigTime As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents ocmdelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cFNHSysUnitSectId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysPeriodOfTimeId_None As DevExpress.XtraEditors.TextEdit
End Class
