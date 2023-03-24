<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wListEmpWorkTimeOver
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
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ColFNHSysEmpID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTEmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTEmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTShiftName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTEmpTypeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDeptCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDivisonCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSectCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitSectCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTTimeOut = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFTStateStop = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTStateStop = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryFTApproveState = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ochkselectall = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTStateStop, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTApproveState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ochkselectall.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogc
        '
        Me.ogc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogc.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogc.Location = New System.Drawing.Point(3, 31)
        Me.ogc.MainView = Me.ogv
        Me.ogc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogc.Name = "ogc"
        Me.ogc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTSelect, Me.RepositoryFTApproveState, Me.RepFTStateStop})
        Me.ogc.Size = New System.Drawing.Size(1176, 662)
        Me.ogc.TabIndex = 4
        Me.ogc.TabStop = False
        Me.ogc.Tag = "2|"
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv})
        '
        'ogv
        '
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.ColFNHSysEmpID, Me.FTEmpCode, Me.FTEmpName, Me.FTShiftName, Me.FTEmpTypeCode, Me.FTDeptCode, Me.FTDivisonCode, Me.FTSectCode, Me.FTUnitSectCode, Me.FTTimeOut, Me.GFTStateStop})
        Me.ogv.GridControl = Me.ogc
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsCustomization.AllowGroup = False
        Me.ogv.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogv.OptionsView.ColumnAutoWidth = False
        Me.ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogv.OptionsView.ShowGroupPanel = False
        Me.ogv.Tag = "2|"
        '
        'FTSelect
        '
        Me.FTSelect.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSelect.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSelect.Caption = "FTSelect"
        Me.FTSelect.ColumnEdit = Me.RepositoryFTSelect
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.OptionsColumn.AllowMove = False
        Me.FTSelect.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        Me.FTSelect.Width = 32
        '
        'RepositoryFTSelect
        '
        Me.RepositoryFTSelect.AutoHeight = False
        Me.RepositoryFTSelect.Caption = "Check"
        Me.RepositoryFTSelect.Name = "RepositoryFTSelect"
        Me.RepositoryFTSelect.ValueChecked = "1"
        Me.RepositoryFTSelect.ValueUnchecked = "0"
        '
        'ColFNHSysEmpID
        '
        Me.ColFNHSysEmpID.AppearanceHeader.Options.UseTextOptions = True
        Me.ColFNHSysEmpID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColFNHSysEmpID.Caption = "FNHSysEmpID"
        Me.ColFNHSysEmpID.FieldName = "FNHSysEmpID"
        Me.ColFNHSysEmpID.Name = "ColFNHSysEmpID"
        Me.ColFNHSysEmpID.OptionsColumn.AllowEdit = False
        Me.ColFNHSysEmpID.OptionsColumn.AllowMove = False
        Me.ColFNHSysEmpID.OptionsColumn.ReadOnly = True
        Me.ColFNHSysEmpID.OptionsColumn.TabStop = False
        '
        'FTEmpCode
        '
        Me.FTEmpCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTEmpCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTEmpCode.Caption = "FTEmpCode"
        Me.FTEmpCode.FieldName = "FTEmpCode"
        Me.FTEmpCode.Name = "FTEmpCode"
        Me.FTEmpCode.OptionsColumn.AllowEdit = False
        Me.FTEmpCode.OptionsColumn.AllowMove = False
        Me.FTEmpCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEmpCode.OptionsColumn.ReadOnly = True
        Me.FTEmpCode.OptionsColumn.TabStop = False
        Me.FTEmpCode.Visible = True
        Me.FTEmpCode.VisibleIndex = 6
        Me.FTEmpCode.Width = 112
        '
        'FTEmpName
        '
        Me.FTEmpName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTEmpName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTEmpName.Caption = "FTEmpName"
        Me.FTEmpName.FieldName = "FTEmpName"
        Me.FTEmpName.Name = "FTEmpName"
        Me.FTEmpName.OptionsColumn.AllowEdit = False
        Me.FTEmpName.OptionsColumn.AllowMove = False
        Me.FTEmpName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEmpName.OptionsColumn.ReadOnly = True
        Me.FTEmpName.OptionsColumn.TabStop = False
        Me.FTEmpName.Visible = True
        Me.FTEmpName.VisibleIndex = 7
        Me.FTEmpName.Width = 157
        '
        'FTShiftName
        '
        Me.FTShiftName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTShiftName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTShiftName.Caption = "FTShiftName"
        Me.FTShiftName.FieldName = "FTShiftName"
        Me.FTShiftName.Name = "FTShiftName"
        Me.FTShiftName.OptionsColumn.AllowEdit = False
        Me.FTShiftName.OptionsColumn.AllowMove = False
        Me.FTShiftName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTShiftName.OptionsColumn.ReadOnly = True
        Me.FTShiftName.OptionsColumn.TabStop = False
        '
        'FTEmpTypeCode
        '
        Me.FTEmpTypeCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTEmpTypeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTEmpTypeCode.Caption = "ประเภท"
        Me.FTEmpTypeCode.FieldName = "FTEmpTypeCode"
        Me.FTEmpTypeCode.Name = "FTEmpTypeCode"
        Me.FTEmpTypeCode.OptionsColumn.AllowEdit = False
        Me.FTEmpTypeCode.OptionsColumn.ReadOnly = True
        Me.FTEmpTypeCode.Visible = True
        Me.FTEmpTypeCode.VisibleIndex = 1
        Me.FTEmpTypeCode.Width = 90
        '
        'FTDeptCode
        '
        Me.FTDeptCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTDeptCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTDeptCode.Caption = "ฝ่าย"
        Me.FTDeptCode.FieldName = "FTDeptCode"
        Me.FTDeptCode.Name = "FTDeptCode"
        Me.FTDeptCode.OptionsColumn.AllowEdit = False
        Me.FTDeptCode.OptionsColumn.ReadOnly = True
        Me.FTDeptCode.Visible = True
        Me.FTDeptCode.VisibleIndex = 2
        '
        'FTDivisonCode
        '
        Me.FTDivisonCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTDivisonCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTDivisonCode.Caption = "ส่วน"
        Me.FTDivisonCode.FieldName = "FTDivisonCode"
        Me.FTDivisonCode.Name = "FTDivisonCode"
        Me.FTDivisonCode.OptionsColumn.AllowEdit = False
        Me.FTDivisonCode.OptionsColumn.ReadOnly = True
        Me.FTDivisonCode.Visible = True
        Me.FTDivisonCode.VisibleIndex = 3
        Me.FTDivisonCode.Width = 93
        '
        'FTSectCode
        '
        Me.FTSectCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSectCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSectCode.Caption = "แผนก"
        Me.FTSectCode.FieldName = "FTSectCode"
        Me.FTSectCode.Name = "FTSectCode"
        Me.FTSectCode.OptionsColumn.AllowEdit = False
        Me.FTSectCode.OptionsColumn.ReadOnly = True
        Me.FTSectCode.Visible = True
        Me.FTSectCode.VisibleIndex = 4
        '
        'FTUnitSectCode
        '
        Me.FTUnitSectCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTUnitSectCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTUnitSectCode.Caption = "สังกัด"
        Me.FTUnitSectCode.FieldName = "FTUnitSectCode"
        Me.FTUnitSectCode.Name = "FTUnitSectCode"
        Me.FTUnitSectCode.OptionsColumn.AllowEdit = False
        Me.FTUnitSectCode.OptionsColumn.ReadOnly = True
        Me.FTUnitSectCode.Visible = True
        Me.FTUnitSectCode.VisibleIndex = 5
        Me.FTUnitSectCode.Width = 100
        '
        'FTTimeOut
        '
        Me.FTTimeOut.AppearanceCell.Options.UseTextOptions = True
        Me.FTTimeOut.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTTimeOut.AppearanceHeader.Options.UseTextOptions = True
        Me.FTTimeOut.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTTimeOut.Caption = "FTTimeOut"
        Me.FTTimeOut.FieldName = "FTTimeOut"
        Me.FTTimeOut.Name = "FTTimeOut"
        Me.FTTimeOut.OptionsColumn.AllowEdit = False
        Me.FTTimeOut.OptionsColumn.ReadOnly = True
        Me.FTTimeOut.Visible = True
        Me.FTTimeOut.VisibleIndex = 8
        '
        'GFTStateStop
        '
        Me.GFTStateStop.Caption = "FTStateStop"
        Me.GFTStateStop.ColumnEdit = Me.RepFTStateStop
        Me.GFTStateStop.FieldName = "FTStateStop"
        Me.GFTStateStop.Name = "GFTStateStop"
        Me.GFTStateStop.OptionsColumn.AllowEdit = False
        Me.GFTStateStop.OptionsColumn.ReadOnly = True
        Me.GFTStateStop.Visible = True
        Me.GFTStateStop.VisibleIndex = 9
        '
        'RepFTStateStop
        '
        Me.RepFTStateStop.AutoHeight = False
        Me.RepFTStateStop.Caption = "Check"
        Me.RepFTStateStop.Name = "RepFTStateStop"
        Me.RepFTStateStop.ValueChecked = "1"
        Me.RepFTStateStop.ValueUnchecked = "0"
        '
        'RepositoryFTApproveState
        '
        Me.RepositoryFTApproveState.AutoHeight = False
        Me.RepositoryFTApproveState.Caption = "Check"
        Me.RepositoryFTApproveState.Name = "RepositoryFTApproveState"
        Me.RepositoryFTApproveState.ValueChecked = "1"
        Me.RepositoryFTApproveState.ValueUnchecked = "0"
        '
        'ocmsave
        '
        Me.ocmsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmsave.Location = New System.Drawing.Point(163, 701)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(185, 28)
        Me.ocmsave.TabIndex = 5
        Me.ocmsave.Text = "SAVE"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(729, 701)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(180, 28)
        Me.ocmcancel.TabIndex = 6
        Me.ocmcancel.Text = "CANCEL"
        '
        'ochkselectall
        '
        Me.ochkselectall.Location = New System.Drawing.Point(24, 4)
        Me.ochkselectall.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ochkselectall.Name = "ochkselectall"
        Me.ochkselectall.Properties.Caption = "Select All"
        Me.ochkselectall.Size = New System.Drawing.Size(531, 20)
        Me.ochkselectall.TabIndex = 309
        '
        'wListEmpWorkTimeOver
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1184, 736)
        Me.ControlBox = False
        Me.Controls.Add(Me.ochkselectall)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmsave)
        Me.Controls.Add(Me.ogc)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wListEmpWorkTimeOver"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "List Employee WorkTime Over"
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTStateStop, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTApproveState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ochkselectall.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ColFNHSysEmpID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTEmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTEmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTShiftName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTEmpTypeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTDeptCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTDivisonCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSectCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitSectCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTTimeOut As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFTStateStop As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTStateStop As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryFTApproveState As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ochkselectall As DevExpress.XtraEditors.CheckEdit
End Class
