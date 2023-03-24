<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HRCondition
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
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject9 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject10 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject11 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject12 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.otcForm = New DevExpress.XtraTab.XtraTabControl()
        Me.otpemptype = New DevExpress.XtraTab.XtraTabPage()
        Me.opnJobNo = New System.Windows.Forms.Panel()
        Me.FNHSysEmpTypeIdTo_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysEmpTypeId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNEmpTypeCon = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ogdemptype = New DevExpress.XtraGrid.GridControl()
        Me.ogvemptype = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColFTEmpTypeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.olbtypeto = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysEmpTypeId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysEmpTypeIdTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.otpdivision = New DevExpress.XtraTab.XtraTabPage()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.FNHSysDivisonIdTo_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysDivisonId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNDivisionCon = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.olbdivision = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysDivisonId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysDivisonIdTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogddiv = New DevExpress.XtraGrid.GridControl()
        Me.ogvdiv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colDivCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColDivName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.otpdepartment = New DevExpress.XtraTab.XtraTabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.FNHSysDeptIdTo_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysDeptId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNDeptCon = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.olbdeptTo = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysDeptId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysDeptIdTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogddept = New DevExpress.XtraGrid.GridControl()
        Me.ogvdept = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColFTDeptCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTDeptName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.otpsect = New DevExpress.XtraTab.XtraTabPage()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.FNHSysSectIdTo_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysSectId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNSectCon = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysSectId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysSectIdTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogdsect = New DevExpress.XtraGrid.GridControl()
        Me.ogvsect = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColSectCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColSectName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.otpunitsect = New DevExpress.XtraTab.XtraTabPage()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.FNHSysUnitSectIdTo_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysUnitSectId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNUnitSectCon = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysUnitSectId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysUnitSectIdTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogdunitsect = New DevExpress.XtraGrid.GridControl()
        Me.ogvunitsect = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColUnitsectCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColUnitSectName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.otpemployee = New DevExpress.XtraTab.XtraTabPage()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.FNHSysEmpIDTo_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysEmpID_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNEmpCon = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysEmpID = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysEmpIDTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogdemp = New DevExpress.XtraGrid.GridControl()
        Me.ogvemp = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColEMpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColEmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.otcForm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otcForm.SuspendLayout()
        Me.otpemptype.SuspendLayout()
        Me.opnJobNo.SuspendLayout()
        CType(Me.FNHSysEmpTypeIdTo_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysEmpTypeId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNEmpTypeCon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogdemptype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvemptype, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysEmpTypeId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysEmpTypeIdTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otpdivision.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.FNHSysDivisonIdTo_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysDivisonId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNDivisionCon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysDivisonId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysDivisonIdTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogddiv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdiv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otpdepartment.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.FNHSysDeptIdTo_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysDeptId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNDeptCon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysDeptId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysDeptIdTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogddept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdept, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otpsect.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.FNHSysSectIdTo_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysSectId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNSectCon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysSectId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysSectIdTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogdsect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvsect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otpunitsect.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.FNHSysUnitSectIdTo_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysUnitSectId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNUnitSectCon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysUnitSectId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysUnitSectIdTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogdunitsect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvunitsect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otpemployee.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.FNHSysEmpIDTo_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysEmpID_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNEmpCon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysEmpID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysEmpIDTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogdemp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvemp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'otcForm
        '
        Me.otcForm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otcForm.Location = New System.Drawing.Point(0, 0)
        Me.otcForm.Margin = New System.Windows.Forms.Padding(4)
        Me.otcForm.Name = "otcForm"
        Me.otcForm.SelectedTabPage = Me.otpemptype
        Me.otcForm.Size = New System.Drawing.Size(717, 276)
        Me.otcForm.TabIndex = 0
        Me.otcForm.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpemptype, Me.otpdivision, Me.otpdepartment, Me.otpsect, Me.otpunitsect, Me.otpemployee})
        '
        'otpemptype
        '
        Me.otpemptype.Controls.Add(Me.opnJobNo)
        Me.otpemptype.Margin = New System.Windows.Forms.Padding(4)
        Me.otpemptype.Name = "otpemptype"
        Me.otpemptype.Size = New System.Drawing.Size(707, 239)
        Me.otpemptype.Text = "Employee Type"
        '
        'opnJobNo
        '
        Me.opnJobNo.Controls.Add(Me.FNHSysEmpTypeIdTo_None)
        Me.opnJobNo.Controls.Add(Me.FNHSysEmpTypeId_None)
        Me.opnJobNo.Controls.Add(Me.FNEmpTypeCon)
        Me.opnJobNo.Controls.Add(Me.ogdemptype)
        Me.opnJobNo.Controls.Add(Me.olbtypeto)
        Me.opnJobNo.Controls.Add(Me.FNHSysEmpTypeId)
        Me.opnJobNo.Controls.Add(Me.FNHSysEmpTypeIdTo)
        Me.opnJobNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.opnJobNo.Location = New System.Drawing.Point(0, 0)
        Me.opnJobNo.Margin = New System.Windows.Forms.Padding(4)
        Me.opnJobNo.Name = "opnJobNo"
        Me.opnJobNo.Size = New System.Drawing.Size(707, 239)
        Me.opnJobNo.TabIndex = 191
        '
        'FNHSysEmpTypeIdTo_None
        '
        Me.FNHSysEmpTypeIdTo_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysEmpTypeIdTo_None.EnterMoveNextControl = True
        Me.FNHSysEmpTypeIdTo_None.Location = New System.Drawing.Point(343, 42)
        Me.FNHSysEmpTypeIdTo_None.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysEmpTypeIdTo_None.Name = "FNHSysEmpTypeIdTo_None"
        Me.FNHSysEmpTypeIdTo_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysEmpTypeIdTo_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysEmpTypeIdTo_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysEmpTypeIdTo_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysEmpTypeIdTo_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysEmpTypeIdTo_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysEmpTypeIdTo_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysEmpTypeIdTo_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysEmpTypeIdTo_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysEmpTypeIdTo_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysEmpTypeIdTo_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysEmpTypeIdTo_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysEmpTypeIdTo_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysEmpTypeIdTo_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysEmpTypeIdTo_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysEmpTypeIdTo_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysEmpTypeIdTo_None.Properties.ReadOnly = True
        Me.FNHSysEmpTypeIdTo_None.Size = New System.Drawing.Size(331, 23)
        Me.FNHSysEmpTypeIdTo_None.TabIndex = 278
        Me.FNHSysEmpTypeIdTo_None.TabStop = False
        Me.FNHSysEmpTypeIdTo_None.Tag = "2|"
        '
        'FNHSysEmpTypeId_None
        '
        Me.FNHSysEmpTypeId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysEmpTypeId_None.EnterMoveNextControl = True
        Me.FNHSysEmpTypeId_None.Location = New System.Drawing.Point(343, 15)
        Me.FNHSysEmpTypeId_None.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysEmpTypeId_None.Name = "FNHSysEmpTypeId_None"
        Me.FNHSysEmpTypeId_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysEmpTypeId_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysEmpTypeId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysEmpTypeId_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysEmpTypeId_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysEmpTypeId_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysEmpTypeId_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysEmpTypeId_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysEmpTypeId_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysEmpTypeId_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysEmpTypeId_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysEmpTypeId_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysEmpTypeId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysEmpTypeId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysEmpTypeId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysEmpTypeId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysEmpTypeId_None.Properties.ReadOnly = True
        Me.FNHSysEmpTypeId_None.Size = New System.Drawing.Size(331, 23)
        Me.FNHSysEmpTypeId_None.TabIndex = 277
        Me.FNHSysEmpTypeId_None.TabStop = False
        Me.FNHSysEmpTypeId_None.Tag = "2|"
        '
        'FNEmpTypeCon
        '
        Me.FNEmpTypeCon.EditValue = ""
        Me.FNEmpTypeCon.EnterMoveNextControl = True
        Me.FNEmpTypeCon.Location = New System.Drawing.Point(13, 15)
        Me.FNEmpTypeCon.Margin = New System.Windows.Forms.Padding(4)
        Me.FNEmpTypeCon.Name = "FNEmpTypeCon"
        Me.FNEmpTypeCon.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNEmpTypeCon.Properties.Appearance.Options.UseBackColor = True
        Me.FNEmpTypeCon.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNEmpTypeCon.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNEmpTypeCon.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNEmpTypeCon.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNEmpTypeCon.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNEmpTypeCon.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNEmpTypeCon.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNEmpTypeCon.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNEmpTypeCon.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNEmpTypeCon.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNEmpTypeCon.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNEmpTypeCon.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNEmpTypeCon.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNEmpTypeCon.Properties.Tag = "FNReportTypeCondition"
        Me.FNEmpTypeCon.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNEmpTypeCon.Size = New System.Drawing.Size(160, 23)
        Me.FNEmpTypeCon.TabIndex = 195
        Me.FNEmpTypeCon.Tag = "2|"
        '
        'ogdemptype
        '
        Me.ogdemptype.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogdemptype.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ogdemptype.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.ogdemptype.Location = New System.Drawing.Point(181, 71)
        Me.ogdemptype.MainView = Me.ogvemptype
        Me.ogdemptype.Margin = New System.Windows.Forms.Padding(4)
        Me.ogdemptype.Name = "ogdemptype"
        Me.ogdemptype.Size = New System.Drawing.Size(493, 147)
        Me.ogdemptype.TabIndex = 6
        Me.ogdemptype.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvemptype})
        '
        'ogvemptype
        '
        Me.ogvemptype.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColFTEmpTypeCode})
        Me.ogvemptype.GridControl = Me.ogdemptype
        Me.ogvemptype.Name = "ogvemptype"
        Me.ogvemptype.OptionsBehavior.Editable = False
        Me.ogvemptype.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ogvemptype.OptionsView.ShowColumnHeaders = False
        Me.ogvemptype.OptionsView.ShowGroupPanel = False
        Me.ogvemptype.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ogvemptype.OptionsView.ShowIndicator = False
        Me.ogvemptype.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        '
        'ColFTEmpTypeCode
        '
        Me.ColFTEmpTypeCode.Caption = "FTCode"
        Me.ColFTEmpTypeCode.FieldName = "FTCode"
        Me.ColFTEmpTypeCode.Name = "ColFTEmpTypeCode"
        Me.ColFTEmpTypeCode.Visible = True
        Me.ColFTEmpTypeCode.VisibleIndex = 0
        '
        'olbtypeto
        '
        Me.olbtypeto.Appearance.Options.UseTextOptions = True
        Me.olbtypeto.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.olbtypeto.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.olbtypeto.Location = New System.Drawing.Point(13, 46)
        Me.olbtypeto.Margin = New System.Windows.Forms.Padding(4)
        Me.olbtypeto.Name = "olbtypeto"
        Me.olbtypeto.Size = New System.Drawing.Size(160, 21)
        Me.olbtypeto.TabIndex = 194
        Me.olbtypeto.Tag = "1|To|To"
        Me.olbtypeto.Text = "To"
        '
        'FNHSysEmpTypeId
        '
        Me.FNHSysEmpTypeId.Location = New System.Drawing.Point(181, 15)
        Me.FNHSysEmpTypeId.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysEmpTypeId.Name = "FNHSysEmpTypeId"
        Me.FNHSysEmpTypeId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "42", Nothing, True)})
        Me.FNHSysEmpTypeId.Size = New System.Drawing.Size(160, 23)
        Me.FNHSysEmpTypeId.TabIndex = 3
        '
        'FNHSysEmpTypeIdTo
        '
        Me.FNHSysEmpTypeIdTo.Location = New System.Drawing.Point(181, 42)
        Me.FNHSysEmpTypeIdTo.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysEmpTypeIdTo.Name = "FNHSysEmpTypeIdTo"
        Me.FNHSysEmpTypeIdTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "74", Nothing, True)})
        Me.FNHSysEmpTypeIdTo.Size = New System.Drawing.Size(160, 23)
        Me.FNHSysEmpTypeIdTo.TabIndex = 4
        '
        'otpdivision
        '
        Me.otpdivision.Controls.Add(Me.Panel2)
        Me.otpdivision.Margin = New System.Windows.Forms.Padding(4)
        Me.otpdivision.Name = "otpdivision"
        Me.otpdivision.Size = New System.Drawing.Size(710, 242)
        Me.otpdivision.Text = "Devision"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.FNHSysDivisonIdTo_None)
        Me.Panel2.Controls.Add(Me.FNHSysDivisonId_None)
        Me.Panel2.Controls.Add(Me.FNDivisionCon)
        Me.Panel2.Controls.Add(Me.olbdivision)
        Me.Panel2.Controls.Add(Me.FNHSysDivisonId)
        Me.Panel2.Controls.Add(Me.FNHSysDivisonIdTo)
        Me.Panel2.Controls.Add(Me.ogddiv)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(710, 242)
        Me.Panel2.TabIndex = 191
        '
        'FNHSysDivisonIdTo_None
        '
        Me.FNHSysDivisonIdTo_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysDivisonIdTo_None.EnterMoveNextControl = True
        Me.FNHSysDivisonIdTo_None.Location = New System.Drawing.Point(343, 42)
        Me.FNHSysDivisonIdTo_None.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysDivisonIdTo_None.Name = "FNHSysDivisonIdTo_None"
        Me.FNHSysDivisonIdTo_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysDivisonIdTo_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysDivisonIdTo_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysDivisonIdTo_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysDivisonIdTo_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysDivisonIdTo_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDivisonIdTo_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysDivisonIdTo_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysDivisonIdTo_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysDivisonIdTo_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDivisonIdTo_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysDivisonIdTo_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysDivisonIdTo_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysDivisonIdTo_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDivisonIdTo_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysDivisonIdTo_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysDivisonIdTo_None.Properties.ReadOnly = True
        Me.FNHSysDivisonIdTo_None.Size = New System.Drawing.Size(326, 23)
        Me.FNHSysDivisonIdTo_None.TabIndex = 284
        Me.FNHSysDivisonIdTo_None.TabStop = False
        Me.FNHSysDivisonIdTo_None.Tag = "2|"
        '
        'FNHSysDivisonId_None
        '
        Me.FNHSysDivisonId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysDivisonId_None.EnterMoveNextControl = True
        Me.FNHSysDivisonId_None.Location = New System.Drawing.Point(343, 15)
        Me.FNHSysDivisonId_None.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysDivisonId_None.Name = "FNHSysDivisonId_None"
        Me.FNHSysDivisonId_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysDivisonId_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysDivisonId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysDivisonId_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysDivisonId_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysDivisonId_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDivisonId_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysDivisonId_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysDivisonId_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysDivisonId_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDivisonId_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysDivisonId_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysDivisonId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysDivisonId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDivisonId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysDivisonId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysDivisonId_None.Properties.ReadOnly = True
        Me.FNHSysDivisonId_None.Size = New System.Drawing.Size(326, 23)
        Me.FNHSysDivisonId_None.TabIndex = 283
        Me.FNHSysDivisonId_None.TabStop = False
        Me.FNHSysDivisonId_None.Tag = "2|"
        '
        'FNDivisionCon
        '
        Me.FNDivisionCon.EditValue = ""
        Me.FNDivisionCon.EnterMoveNextControl = True
        Me.FNDivisionCon.Location = New System.Drawing.Point(13, 15)
        Me.FNDivisionCon.Margin = New System.Windows.Forms.Padding(4)
        Me.FNDivisionCon.Name = "FNDivisionCon"
        Me.FNDivisionCon.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNDivisionCon.Properties.Appearance.Options.UseBackColor = True
        Me.FNDivisionCon.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNDivisionCon.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNDivisionCon.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNDivisionCon.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNDivisionCon.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNDivisionCon.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNDivisionCon.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNDivisionCon.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNDivisionCon.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNDivisionCon.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNDivisionCon.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNDivisionCon.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNDivisionCon.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNDivisionCon.Properties.Tag = "FNReportTypeCondition"
        Me.FNDivisionCon.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNDivisionCon.Size = New System.Drawing.Size(160, 23)
        Me.FNDivisionCon.TabIndex = 282
        Me.FNDivisionCon.Tag = "2|"
        '
        'olbdivision
        '
        Me.olbdivision.Appearance.Options.UseTextOptions = True
        Me.olbdivision.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.olbdivision.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.olbdivision.Location = New System.Drawing.Point(13, 46)
        Me.olbdivision.Margin = New System.Windows.Forms.Padding(4)
        Me.olbdivision.Name = "olbdivision"
        Me.olbdivision.Size = New System.Drawing.Size(160, 21)
        Me.olbdivision.TabIndex = 281
        Me.olbdivision.Tag = "1|To|To"
        Me.olbdivision.Text = "To"
        '
        'FNHSysDivisonId
        '
        Me.FNHSysDivisonId.Location = New System.Drawing.Point(181, 15)
        Me.FNHSysDivisonId.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysDivisonId.Name = "FNHSysDivisonId"
        Me.FNHSysDivisonId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "46", Nothing, True)})
        Me.FNHSysDivisonId.Size = New System.Drawing.Size(160, 23)
        Me.FNHSysDivisonId.TabIndex = 279
        '
        'FNHSysDivisonIdTo
        '
        Me.FNHSysDivisonIdTo.Location = New System.Drawing.Point(181, 42)
        Me.FNHSysDivisonIdTo.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysDivisonIdTo.Name = "FNHSysDivisonIdTo"
        Me.FNHSysDivisonIdTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", "60", Nothing, True)})
        Me.FNHSysDivisonIdTo.Size = New System.Drawing.Size(160, 23)
        Me.FNHSysDivisonIdTo.TabIndex = 280
        '
        'ogddiv
        '
        Me.ogddiv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogddiv.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ogddiv.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.ogddiv.Location = New System.Drawing.Point(181, 71)
        Me.ogddiv.MainView = Me.ogvdiv
        Me.ogddiv.Margin = New System.Windows.Forms.Padding(4)
        Me.ogddiv.Name = "ogddiv"
        Me.ogddiv.Size = New System.Drawing.Size(485, 156)
        Me.ogddiv.TabIndex = 195
        Me.ogddiv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdiv})
        '
        'ogvdiv
        '
        Me.ogvdiv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colDivCode, Me.ColDivName})
        Me.ogvdiv.GridControl = Me.ogddiv
        Me.ogvdiv.Name = "ogvdiv"
        Me.ogvdiv.OptionsBehavior.Editable = False
        Me.ogvdiv.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ogvdiv.OptionsView.ShowColumnHeaders = False
        Me.ogvdiv.OptionsView.ShowGroupPanel = False
        Me.ogvdiv.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ogvdiv.OptionsView.ShowIndicator = False
        Me.ogvdiv.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        '
        'colDivCode
        '
        Me.colDivCode.Caption = "Department"
        Me.colDivCode.FieldName = "FTCode"
        Me.colDivCode.Name = "colDivCode"
        Me.colDivCode.OptionsColumn.AllowEdit = False
        Me.colDivCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.colDivCode.OptionsColumn.ReadOnly = True
        Me.colDivCode.Visible = True
        Me.colDivCode.VisibleIndex = 0
        Me.colDivCode.Width = 120
        '
        'ColDivName
        '
        Me.ColDivName.Caption = "Name"
        Me.ColDivName.FieldName = "FTName"
        Me.ColDivName.Name = "ColDivName"
        Me.ColDivName.OptionsColumn.AllowEdit = False
        Me.ColDivName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColDivName.OptionsColumn.ReadOnly = True
        Me.ColDivName.Visible = True
        Me.ColDivName.VisibleIndex = 1
        Me.ColDivName.Width = 241
        '
        'otpdepartment
        '
        Me.otpdepartment.Controls.Add(Me.Panel1)
        Me.otpdepartment.Margin = New System.Windows.Forms.Padding(4)
        Me.otpdepartment.Name = "otpdepartment"
        Me.otpdepartment.Size = New System.Drawing.Size(710, 242)
        Me.otpdepartment.Text = "Department"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.FNHSysDeptIdTo_None)
        Me.Panel1.Controls.Add(Me.FNHSysDeptId_None)
        Me.Panel1.Controls.Add(Me.FNDeptCon)
        Me.Panel1.Controls.Add(Me.olbdeptTo)
        Me.Panel1.Controls.Add(Me.FNHSysDeptId)
        Me.Panel1.Controls.Add(Me.FNHSysDeptIdTo)
        Me.Panel1.Controls.Add(Me.ogddept)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(710, 242)
        Me.Panel1.TabIndex = 191
        '
        'FNHSysDeptIdTo_None
        '
        Me.FNHSysDeptIdTo_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysDeptIdTo_None.EnterMoveNextControl = True
        Me.FNHSysDeptIdTo_None.Location = New System.Drawing.Point(343, 42)
        Me.FNHSysDeptIdTo_None.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysDeptIdTo_None.Name = "FNHSysDeptIdTo_None"
        Me.FNHSysDeptIdTo_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysDeptIdTo_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysDeptIdTo_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysDeptIdTo_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysDeptIdTo_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysDeptIdTo_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDeptIdTo_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysDeptIdTo_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysDeptIdTo_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysDeptIdTo_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDeptIdTo_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysDeptIdTo_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysDeptIdTo_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysDeptIdTo_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDeptIdTo_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysDeptIdTo_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysDeptIdTo_None.Properties.ReadOnly = True
        Me.FNHSysDeptIdTo_None.Size = New System.Drawing.Size(334, 23)
        Me.FNHSysDeptIdTo_None.TabIndex = 284
        Me.FNHSysDeptIdTo_None.TabStop = False
        Me.FNHSysDeptIdTo_None.Tag = "2|"
        '
        'FNHSysDeptId_None
        '
        Me.FNHSysDeptId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysDeptId_None.EnterMoveNextControl = True
        Me.FNHSysDeptId_None.Location = New System.Drawing.Point(343, 15)
        Me.FNHSysDeptId_None.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysDeptId_None.Name = "FNHSysDeptId_None"
        Me.FNHSysDeptId_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysDeptId_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysDeptId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysDeptId_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysDeptId_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysDeptId_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDeptId_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysDeptId_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysDeptId_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysDeptId_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDeptId_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysDeptId_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysDeptId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysDeptId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDeptId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysDeptId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysDeptId_None.Properties.ReadOnly = True
        Me.FNHSysDeptId_None.Size = New System.Drawing.Size(334, 23)
        Me.FNHSysDeptId_None.TabIndex = 283
        Me.FNHSysDeptId_None.TabStop = False
        Me.FNHSysDeptId_None.Tag = "2|"
        '
        'FNDeptCon
        '
        Me.FNDeptCon.EditValue = ""
        Me.FNDeptCon.EnterMoveNextControl = True
        Me.FNDeptCon.Location = New System.Drawing.Point(13, 15)
        Me.FNDeptCon.Margin = New System.Windows.Forms.Padding(4)
        Me.FNDeptCon.Name = "FNDeptCon"
        Me.FNDeptCon.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNDeptCon.Properties.Appearance.Options.UseBackColor = True
        Me.FNDeptCon.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNDeptCon.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNDeptCon.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNDeptCon.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNDeptCon.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNDeptCon.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNDeptCon.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNDeptCon.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNDeptCon.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNDeptCon.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNDeptCon.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNDeptCon.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNDeptCon.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNDeptCon.Properties.Tag = "FNReportTypeCondition"
        Me.FNDeptCon.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNDeptCon.Size = New System.Drawing.Size(160, 23)
        Me.FNDeptCon.TabIndex = 282
        Me.FNDeptCon.Tag = "2|"
        '
        'olbdeptTo
        '
        Me.olbdeptTo.Appearance.Options.UseTextOptions = True
        Me.olbdeptTo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.olbdeptTo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.olbdeptTo.Location = New System.Drawing.Point(13, 46)
        Me.olbdeptTo.Margin = New System.Windows.Forms.Padding(4)
        Me.olbdeptTo.Name = "olbdeptTo"
        Me.olbdeptTo.Size = New System.Drawing.Size(160, 21)
        Me.olbdeptTo.TabIndex = 281
        Me.olbdeptTo.Tag = "1|To|To"
        Me.olbdeptTo.Text = "To"
        '
        'FNHSysDeptId
        '
        Me.FNHSysDeptId.Location = New System.Drawing.Point(181, 15)
        Me.FNHSysDeptId.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysDeptId.Name = "FNHSysDeptId"
        Me.FNHSysDeptId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, "", "23", Nothing, True)})
        Me.FNHSysDeptId.Size = New System.Drawing.Size(160, 23)
        Me.FNHSysDeptId.TabIndex = 279
        '
        'FNHSysDeptIdTo
        '
        Me.FNHSysDeptIdTo.Location = New System.Drawing.Point(181, 42)
        Me.FNHSysDeptIdTo.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysDeptIdTo.Name = "FNHSysDeptIdTo"
        Me.FNHSysDeptIdTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject6, "", "59", Nothing, True)})
        Me.FNHSysDeptIdTo.Size = New System.Drawing.Size(160, 23)
        Me.FNHSysDeptIdTo.TabIndex = 280
        '
        'ogddept
        '
        Me.ogddept.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogddept.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ogddept.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.ogddept.Location = New System.Drawing.Point(181, 71)
        Me.ogddept.MainView = Me.ogvdept
        Me.ogddept.Margin = New System.Windows.Forms.Padding(4)
        Me.ogddept.Name = "ogddept"
        Me.ogddept.Size = New System.Drawing.Size(496, 154)
        Me.ogddept.TabIndex = 6
        Me.ogddept.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdept})
        '
        'ogvdept
        '
        Me.ogvdept.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColFTDeptCode, Me.ColFTDeptName})
        Me.ogvdept.GridControl = Me.ogddept
        Me.ogvdept.Name = "ogvdept"
        Me.ogvdept.OptionsBehavior.Editable = False
        Me.ogvdept.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ogvdept.OptionsView.ShowColumnHeaders = False
        Me.ogvdept.OptionsView.ShowGroupPanel = False
        Me.ogvdept.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ogvdept.OptionsView.ShowIndicator = False
        Me.ogvdept.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        '
        'ColFTDeptCode
        '
        Me.ColFTDeptCode.Caption = "Department"
        Me.ColFTDeptCode.FieldName = "FTCode"
        Me.ColFTDeptCode.Name = "ColFTDeptCode"
        Me.ColFTDeptCode.OptionsColumn.AllowEdit = False
        Me.ColFTDeptCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColFTDeptCode.OptionsColumn.ReadOnly = True
        Me.ColFTDeptCode.Visible = True
        Me.ColFTDeptCode.VisibleIndex = 0
        Me.ColFTDeptCode.Width = 120
        '
        'ColFTDeptName
        '
        Me.ColFTDeptName.Caption = "Name"
        Me.ColFTDeptName.FieldName = "FTName"
        Me.ColFTDeptName.Name = "ColFTDeptName"
        Me.ColFTDeptName.OptionsColumn.AllowEdit = False
        Me.ColFTDeptName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColFTDeptName.OptionsColumn.ReadOnly = True
        Me.ColFTDeptName.Visible = True
        Me.ColFTDeptName.VisibleIndex = 1
        Me.ColFTDeptName.Width = 241
        '
        'otpsect
        '
        Me.otpsect.Controls.Add(Me.Panel3)
        Me.otpsect.Margin = New System.Windows.Forms.Padding(4)
        Me.otpsect.Name = "otpsect"
        Me.otpsect.Size = New System.Drawing.Size(710, 242)
        Me.otpsect.Text = "Sect"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.FNHSysSectIdTo_None)
        Me.Panel3.Controls.Add(Me.FNHSysSectId_None)
        Me.Panel3.Controls.Add(Me.FNSectCon)
        Me.Panel3.Controls.Add(Me.LabelControl4)
        Me.Panel3.Controls.Add(Me.FNHSysSectId)
        Me.Panel3.Controls.Add(Me.FNHSysSectIdTo)
        Me.Panel3.Controls.Add(Me.ogdsect)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(710, 242)
        Me.Panel3.TabIndex = 191
        '
        'FNHSysSectIdTo_None
        '
        Me.FNHSysSectIdTo_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysSectIdTo_None.EnterMoveNextControl = True
        Me.FNHSysSectIdTo_None.Location = New System.Drawing.Point(343, 42)
        Me.FNHSysSectIdTo_None.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysSectIdTo_None.Name = "FNHSysSectIdTo_None"
        Me.FNHSysSectIdTo_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysSectIdTo_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysSectIdTo_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysSectIdTo_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysSectIdTo_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysSectIdTo_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSectIdTo_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysSectIdTo_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysSectIdTo_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysSectIdTo_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSectIdTo_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysSectIdTo_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysSectIdTo_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysSectIdTo_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSectIdTo_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysSectIdTo_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysSectIdTo_None.Properties.ReadOnly = True
        Me.FNHSysSectIdTo_None.Size = New System.Drawing.Size(326, 23)
        Me.FNHSysSectIdTo_None.TabIndex = 290
        Me.FNHSysSectIdTo_None.TabStop = False
        Me.FNHSysSectIdTo_None.Tag = "2|"
        '
        'FNHSysSectId_None
        '
        Me.FNHSysSectId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysSectId_None.EnterMoveNextControl = True
        Me.FNHSysSectId_None.Location = New System.Drawing.Point(343, 15)
        Me.FNHSysSectId_None.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysSectId_None.Name = "FNHSysSectId_None"
        Me.FNHSysSectId_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysSectId_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysSectId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysSectId_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysSectId_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysSectId_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSectId_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysSectId_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysSectId_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysSectId_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSectId_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysSectId_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysSectId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysSectId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSectId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysSectId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysSectId_None.Properties.ReadOnly = True
        Me.FNHSysSectId_None.Size = New System.Drawing.Size(326, 23)
        Me.FNHSysSectId_None.TabIndex = 289
        Me.FNHSysSectId_None.TabStop = False
        Me.FNHSysSectId_None.Tag = "2|"
        '
        'FNSectCon
        '
        Me.FNSectCon.EditValue = ""
        Me.FNSectCon.EnterMoveNextControl = True
        Me.FNSectCon.Location = New System.Drawing.Point(13, 15)
        Me.FNSectCon.Margin = New System.Windows.Forms.Padding(4)
        Me.FNSectCon.Name = "FNSectCon"
        Me.FNSectCon.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNSectCon.Properties.Appearance.Options.UseBackColor = True
        Me.FNSectCon.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNSectCon.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNSectCon.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNSectCon.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNSectCon.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNSectCon.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNSectCon.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNSectCon.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNSectCon.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNSectCon.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNSectCon.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNSectCon.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNSectCon.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNSectCon.Properties.Tag = "FNReportTypeCondition"
        Me.FNSectCon.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNSectCon.Size = New System.Drawing.Size(160, 23)
        Me.FNSectCon.TabIndex = 288
        Me.FNSectCon.Tag = "2|"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Options.UseTextOptions = True
        Me.LabelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl4.Location = New System.Drawing.Point(13, 46)
        Me.LabelControl4.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(160, 21)
        Me.LabelControl4.TabIndex = 287
        Me.LabelControl4.Tag = "1|To|To"
        Me.LabelControl4.Text = "To"
        '
        'FNHSysSectId
        '
        Me.FNHSysSectId.Location = New System.Drawing.Point(181, 15)
        Me.FNHSysSectId.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysSectId.Name = "FNHSysSectId"
        Me.FNHSysSectId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject7, "", "55", Nothing, True)})
        Me.FNHSysSectId.Size = New System.Drawing.Size(160, 23)
        Me.FNHSysSectId.TabIndex = 285
        '
        'FNHSysSectIdTo
        '
        Me.FNHSysSectIdTo.Location = New System.Drawing.Point(181, 42)
        Me.FNHSysSectIdTo.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysSectIdTo.Name = "FNHSysSectIdTo"
        Me.FNHSysSectIdTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject8, "", "61", Nothing, True)})
        Me.FNHSysSectIdTo.Size = New System.Drawing.Size(160, 23)
        Me.FNHSysSectIdTo.TabIndex = 286
        '
        'ogdsect
        '
        Me.ogdsect.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogdsect.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ogdsect.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.ogdsect.Location = New System.Drawing.Point(181, 71)
        Me.ogdsect.MainView = Me.ogvsect
        Me.ogdsect.Margin = New System.Windows.Forms.Padding(4)
        Me.ogdsect.Name = "ogdsect"
        Me.ogdsect.Size = New System.Drawing.Size(488, 155)
        Me.ogdsect.TabIndex = 195
        Me.ogdsect.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvsect})
        '
        'ogvsect
        '
        Me.ogvsect.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColSectCode, Me.ColSectName})
        Me.ogvsect.GridControl = Me.ogdsect
        Me.ogvsect.Name = "ogvsect"
        Me.ogvsect.OptionsBehavior.Editable = False
        Me.ogvsect.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ogvsect.OptionsView.ShowColumnHeaders = False
        Me.ogvsect.OptionsView.ShowGroupPanel = False
        Me.ogvsect.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ogvsect.OptionsView.ShowIndicator = False
        Me.ogvsect.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        '
        'ColSectCode
        '
        Me.ColSectCode.Caption = "SectCode"
        Me.ColSectCode.FieldName = "FTCode"
        Me.ColSectCode.Name = "ColSectCode"
        Me.ColSectCode.OptionsColumn.AllowEdit = False
        Me.ColSectCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSectCode.OptionsColumn.ReadOnly = True
        Me.ColSectCode.Visible = True
        Me.ColSectCode.VisibleIndex = 0
        Me.ColSectCode.Width = 120
        '
        'ColSectName
        '
        Me.ColSectName.Caption = "Name"
        Me.ColSectName.FieldName = "FTName"
        Me.ColSectName.Name = "ColSectName"
        Me.ColSectName.OptionsColumn.AllowEdit = False
        Me.ColSectName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColSectName.OptionsColumn.ReadOnly = True
        Me.ColSectName.Visible = True
        Me.ColSectName.VisibleIndex = 1
        Me.ColSectName.Width = 241
        '
        'otpunitsect
        '
        Me.otpunitsect.Controls.Add(Me.Panel4)
        Me.otpunitsect.Margin = New System.Windows.Forms.Padding(4)
        Me.otpunitsect.Name = "otpunitsect"
        Me.otpunitsect.Size = New System.Drawing.Size(710, 242)
        Me.otpunitsect.Text = "Unit Sect"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.FNHSysUnitSectIdTo_None)
        Me.Panel4.Controls.Add(Me.FNHSysUnitSectId_None)
        Me.Panel4.Controls.Add(Me.FNUnitSectCon)
        Me.Panel4.Controls.Add(Me.LabelControl5)
        Me.Panel4.Controls.Add(Me.FNHSysUnitSectId)
        Me.Panel4.Controls.Add(Me.FNHSysUnitSectIdTo)
        Me.Panel4.Controls.Add(Me.ogdunitsect)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(710, 242)
        Me.Panel4.TabIndex = 191
        '
        'FNHSysUnitSectIdTo_None
        '
        Me.FNHSysUnitSectIdTo_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysUnitSectIdTo_None.EnterMoveNextControl = True
        Me.FNHSysUnitSectIdTo_None.Location = New System.Drawing.Point(343, 42)
        Me.FNHSysUnitSectIdTo_None.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysUnitSectIdTo_None.Name = "FNHSysUnitSectIdTo_None"
        Me.FNHSysUnitSectIdTo_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysUnitSectIdTo_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysUnitSectIdTo_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysUnitSectIdTo_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysUnitSectIdTo_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysUnitSectIdTo_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitSectIdTo_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysUnitSectIdTo_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysUnitSectIdTo_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysUnitSectIdTo_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitSectIdTo_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysUnitSectIdTo_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysUnitSectIdTo_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysUnitSectIdTo_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitSectIdTo_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysUnitSectIdTo_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysUnitSectIdTo_None.Properties.ReadOnly = True
        Me.FNHSysUnitSectIdTo_None.Size = New System.Drawing.Size(326, 23)
        Me.FNHSysUnitSectIdTo_None.TabIndex = 284
        Me.FNHSysUnitSectIdTo_None.TabStop = False
        Me.FNHSysUnitSectIdTo_None.Tag = "2|"
        '
        'FNHSysUnitSectId_None
        '
        Me.FNHSysUnitSectId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysUnitSectId_None.EnterMoveNextControl = True
        Me.FNHSysUnitSectId_None.Location = New System.Drawing.Point(343, 15)
        Me.FNHSysUnitSectId_None.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysUnitSectId_None.Name = "FNHSysUnitSectId_None"
        Me.FNHSysUnitSectId_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysUnitSectId_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysUnitSectId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysUnitSectId_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysUnitSectId_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysUnitSectId_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitSectId_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysUnitSectId_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysUnitSectId_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysUnitSectId_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitSectId_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysUnitSectId_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysUnitSectId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysUnitSectId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitSectId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysUnitSectId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysUnitSectId_None.Properties.ReadOnly = True
        Me.FNHSysUnitSectId_None.Size = New System.Drawing.Size(326, 23)
        Me.FNHSysUnitSectId_None.TabIndex = 283
        Me.FNHSysUnitSectId_None.TabStop = False
        Me.FNHSysUnitSectId_None.Tag = "2|"
        '
        'FNUnitSectCon
        '
        Me.FNUnitSectCon.EditValue = ""
        Me.FNUnitSectCon.EnterMoveNextControl = True
        Me.FNUnitSectCon.Location = New System.Drawing.Point(13, 15)
        Me.FNUnitSectCon.Margin = New System.Windows.Forms.Padding(4)
        Me.FNUnitSectCon.Name = "FNUnitSectCon"
        Me.FNUnitSectCon.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNUnitSectCon.Properties.Appearance.Options.UseBackColor = True
        Me.FNUnitSectCon.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNUnitSectCon.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNUnitSectCon.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNUnitSectCon.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNUnitSectCon.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNUnitSectCon.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNUnitSectCon.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNUnitSectCon.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNUnitSectCon.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNUnitSectCon.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNUnitSectCon.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNUnitSectCon.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNUnitSectCon.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNUnitSectCon.Properties.Tag = "FNReportTypeCondition"
        Me.FNUnitSectCon.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNUnitSectCon.Size = New System.Drawing.Size(160, 23)
        Me.FNUnitSectCon.TabIndex = 282
        Me.FNUnitSectCon.Tag = "2|"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Options.UseTextOptions = True
        Me.LabelControl5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl5.Location = New System.Drawing.Point(13, 46)
        Me.LabelControl5.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(160, 21)
        Me.LabelControl5.TabIndex = 281
        Me.LabelControl5.Tag = "1|To|To"
        Me.LabelControl5.Text = "To"
        '
        'FNHSysUnitSectId
        '
        Me.FNHSysUnitSectId.Location = New System.Drawing.Point(181, 15)
        Me.FNHSysUnitSectId.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysUnitSectId.Name = "FNHSysUnitSectId"
        Me.FNHSysUnitSectId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject9, "", "57", Nothing, True)})
        Me.FNHSysUnitSectId.Size = New System.Drawing.Size(160, 23)
        Me.FNHSysUnitSectId.TabIndex = 279
        '
        'FNHSysUnitSectIdTo
        '
        Me.FNHSysUnitSectIdTo.Location = New System.Drawing.Point(181, 42)
        Me.FNHSysUnitSectIdTo.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysUnitSectIdTo.Name = "FNHSysUnitSectIdTo"
        Me.FNHSysUnitSectIdTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject10, "", "62", Nothing, True)})
        Me.FNHSysUnitSectIdTo.Size = New System.Drawing.Size(160, 23)
        Me.FNHSysUnitSectIdTo.TabIndex = 280
        '
        'ogdunitsect
        '
        Me.ogdunitsect.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogdunitsect.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ogdunitsect.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.ogdunitsect.Location = New System.Drawing.Point(181, 71)
        Me.ogdunitsect.MainView = Me.ogvunitsect
        Me.ogdunitsect.Margin = New System.Windows.Forms.Padding(4)
        Me.ogdunitsect.Name = "ogdunitsect"
        Me.ogdunitsect.Size = New System.Drawing.Size(488, 155)
        Me.ogdunitsect.TabIndex = 195
        Me.ogdunitsect.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvunitsect})
        '
        'ogvunitsect
        '
        Me.ogvunitsect.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColUnitsectCode, Me.ColUnitSectName})
        Me.ogvunitsect.GridControl = Me.ogdunitsect
        Me.ogvunitsect.Name = "ogvunitsect"
        Me.ogvunitsect.OptionsBehavior.Editable = False
        Me.ogvunitsect.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ogvunitsect.OptionsView.ShowColumnHeaders = False
        Me.ogvunitsect.OptionsView.ShowGroupPanel = False
        Me.ogvunitsect.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ogvunitsect.OptionsView.ShowIndicator = False
        Me.ogvunitsect.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        '
        'ColUnitsectCode
        '
        Me.ColUnitsectCode.Caption = "Code"
        Me.ColUnitsectCode.FieldName = "FTCode"
        Me.ColUnitsectCode.Name = "ColUnitsectCode"
        Me.ColUnitsectCode.OptionsColumn.AllowEdit = False
        Me.ColUnitsectCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColUnitsectCode.OptionsColumn.ReadOnly = True
        Me.ColUnitsectCode.Visible = True
        Me.ColUnitsectCode.VisibleIndex = 0
        Me.ColUnitsectCode.Width = 120
        '
        'ColUnitSectName
        '
        Me.ColUnitSectName.Caption = "Name"
        Me.ColUnitSectName.FieldName = "FTName"
        Me.ColUnitSectName.Name = "ColUnitSectName"
        Me.ColUnitSectName.OptionsColumn.AllowEdit = False
        Me.ColUnitSectName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColUnitSectName.OptionsColumn.ReadOnly = True
        Me.ColUnitSectName.Visible = True
        Me.ColUnitSectName.VisibleIndex = 1
        Me.ColUnitSectName.Width = 241
        '
        'otpemployee
        '
        Me.otpemployee.Controls.Add(Me.Panel5)
        Me.otpemployee.Margin = New System.Windows.Forms.Padding(4)
        Me.otpemployee.Name = "otpemployee"
        Me.otpemployee.Size = New System.Drawing.Size(710, 242)
        Me.otpemployee.Text = "Employee"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.FNHSysEmpIDTo_None)
        Me.Panel5.Controls.Add(Me.FNHSysEmpID_None)
        Me.Panel5.Controls.Add(Me.FNEmpCon)
        Me.Panel5.Controls.Add(Me.LabelControl6)
        Me.Panel5.Controls.Add(Me.FNHSysEmpID)
        Me.Panel5.Controls.Add(Me.FNHSysEmpIDTo)
        Me.Panel5.Controls.Add(Me.ogdemp)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(710, 242)
        Me.Panel5.TabIndex = 191
        '
        'FNHSysEmpIDTo_None
        '
        Me.FNHSysEmpIDTo_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysEmpIDTo_None.EnterMoveNextControl = True
        Me.FNHSysEmpIDTo_None.Location = New System.Drawing.Point(343, 42)
        Me.FNHSysEmpIDTo_None.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysEmpIDTo_None.Name = "FNHSysEmpIDTo_None"
        Me.FNHSysEmpIDTo_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysEmpIDTo_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysEmpIDTo_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysEmpIDTo_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysEmpIDTo_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysEmpIDTo_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysEmpIDTo_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysEmpIDTo_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysEmpIDTo_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysEmpIDTo_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysEmpIDTo_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysEmpIDTo_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysEmpIDTo_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysEmpIDTo_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysEmpIDTo_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysEmpIDTo_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysEmpIDTo_None.Properties.ReadOnly = True
        Me.FNHSysEmpIDTo_None.Size = New System.Drawing.Size(326, 23)
        Me.FNHSysEmpIDTo_None.TabIndex = 290
        Me.FNHSysEmpIDTo_None.TabStop = False
        Me.FNHSysEmpIDTo_None.Tag = "2|"
        '
        'FNHSysEmpID_None
        '
        Me.FNHSysEmpID_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysEmpID_None.EnterMoveNextControl = True
        Me.FNHSysEmpID_None.Location = New System.Drawing.Point(343, 15)
        Me.FNHSysEmpID_None.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysEmpID_None.Name = "FNHSysEmpID_None"
        Me.FNHSysEmpID_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysEmpID_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysEmpID_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysEmpID_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysEmpID_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysEmpID_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysEmpID_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysEmpID_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysEmpID_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysEmpID_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysEmpID_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysEmpID_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysEmpID_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysEmpID_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysEmpID_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysEmpID_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysEmpID_None.Properties.ReadOnly = True
        Me.FNHSysEmpID_None.Size = New System.Drawing.Size(326, 23)
        Me.FNHSysEmpID_None.TabIndex = 289
        Me.FNHSysEmpID_None.TabStop = False
        Me.FNHSysEmpID_None.Tag = "2|"
        '
        'FNEmpCon
        '
        Me.FNEmpCon.EditValue = ""
        Me.FNEmpCon.EnterMoveNextControl = True
        Me.FNEmpCon.Location = New System.Drawing.Point(13, 15)
        Me.FNEmpCon.Margin = New System.Windows.Forms.Padding(4)
        Me.FNEmpCon.Name = "FNEmpCon"
        Me.FNEmpCon.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNEmpCon.Properties.Appearance.Options.UseBackColor = True
        Me.FNEmpCon.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNEmpCon.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNEmpCon.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNEmpCon.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNEmpCon.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNEmpCon.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNEmpCon.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNEmpCon.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNEmpCon.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNEmpCon.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNEmpCon.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNEmpCon.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNEmpCon.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNEmpCon.Properties.Tag = "FNReportTypeCondition"
        Me.FNEmpCon.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNEmpCon.Size = New System.Drawing.Size(160, 23)
        Me.FNEmpCon.TabIndex = 288
        Me.FNEmpCon.Tag = "2|"
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Options.UseTextOptions = True
        Me.LabelControl6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl6.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl6.Location = New System.Drawing.Point(13, 46)
        Me.LabelControl6.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(160, 21)
        Me.LabelControl6.TabIndex = 287
        Me.LabelControl6.Tag = "1|To|To"
        Me.LabelControl6.Text = "To"
        '
        'FNHSysEmpID
        '
        Me.FNHSysEmpID.Location = New System.Drawing.Point(181, 15)
        Me.FNHSysEmpID.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysEmpID.Name = "FNHSysEmpID"
        Me.FNHSysEmpID.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject11, "", "58", Nothing, True)})
        Me.FNHSysEmpID.Size = New System.Drawing.Size(160, 23)
        Me.FNHSysEmpID.TabIndex = 285
        '
        'FNHSysEmpIDTo
        '
        Me.FNHSysEmpIDTo.Location = New System.Drawing.Point(181, 42)
        Me.FNHSysEmpIDTo.Margin = New System.Windows.Forms.Padding(4)
        Me.FNHSysEmpIDTo.Name = "FNHSysEmpIDTo"
        Me.FNHSysEmpIDTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject12, "", "63", Nothing, True)})
        Me.FNHSysEmpIDTo.Size = New System.Drawing.Size(160, 23)
        Me.FNHSysEmpIDTo.TabIndex = 286
        '
        'ogdemp
        '
        Me.ogdemp.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogdemp.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ogdemp.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.ogdemp.Location = New System.Drawing.Point(181, 71)
        Me.ogdemp.MainView = Me.ogvemp
        Me.ogdemp.Margin = New System.Windows.Forms.Padding(4)
        Me.ogdemp.Name = "ogdemp"
        Me.ogdemp.Size = New System.Drawing.Size(485, 150)
        Me.ogdemp.TabIndex = 195
        Me.ogdemp.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvemp})
        '
        'ogvemp
        '
        Me.ogvemp.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColEMpCode, Me.ColEmpName})
        Me.ogvemp.GridControl = Me.ogdemp
        Me.ogvemp.Name = "ogvemp"
        Me.ogvemp.OptionsBehavior.Editable = False
        Me.ogvemp.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.ogvemp.OptionsView.ShowColumnHeaders = False
        Me.ogvemp.OptionsView.ShowGroupPanel = False
        Me.ogvemp.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.[False]
        Me.ogvemp.OptionsView.ShowIndicator = False
        Me.ogvemp.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        '
        'ColEMpCode
        '
        Me.ColEMpCode.Caption = "Code"
        Me.ColEMpCode.FieldName = "FTCode"
        Me.ColEMpCode.Name = "ColEMpCode"
        Me.ColEMpCode.OptionsColumn.AllowEdit = False
        Me.ColEMpCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEMpCode.OptionsColumn.ReadOnly = True
        Me.ColEMpCode.Visible = True
        Me.ColEMpCode.VisibleIndex = 0
        Me.ColEMpCode.Width = 120
        '
        'ColEmpName
        '
        Me.ColEmpName.Caption = "Name"
        Me.ColEmpName.FieldName = "FTName"
        Me.ColEmpName.Name = "ColEmpName"
        Me.ColEmpName.OptionsColumn.AllowEdit = False
        Me.ColEmpName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColEmpName.OptionsColumn.ReadOnly = True
        Me.ColEmpName.Visible = True
        Me.ColEmpName.VisibleIndex = 1
        Me.ColEmpName.Width = 241
        '
        'HRCondition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.otcForm)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "HRCondition"
        Me.Size = New System.Drawing.Size(717, 276)
        CType(Me.otcForm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otcForm.ResumeLayout(False)
        Me.otpemptype.ResumeLayout(False)
        Me.opnJobNo.ResumeLayout(False)
        CType(Me.FNHSysEmpTypeIdTo_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysEmpTypeId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNEmpTypeCon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogdemptype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvemptype, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysEmpTypeId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysEmpTypeIdTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otpdivision.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.FNHSysDivisonIdTo_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysDivisonId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNDivisionCon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysDivisonId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysDivisonIdTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogddiv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdiv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otpdepartment.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.FNHSysDeptIdTo_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysDeptId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNDeptCon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysDeptId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysDeptIdTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogddept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdept, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otpsect.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.FNHSysSectIdTo_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysSectId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNSectCon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysSectId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysSectIdTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogdsect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvsect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otpunitsect.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.FNHSysUnitSectIdTo_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysUnitSectId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNUnitSectCon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysUnitSectId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysUnitSectIdTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogdunitsect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvunitsect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otpemployee.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        CType(Me.FNHSysEmpIDTo_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysEmpID_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNEmpCon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysEmpID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysEmpIDTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogdemp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvemp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents otcForm As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpemptype As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents otpdepartment As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents otpdivision As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents otpsect As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents otpunitsect As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents otpemployee As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents opnJobNo As System.Windows.Forms.Panel
    Friend WithEvents ogdemptype As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvemptype As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ColFTEmpTypeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents olbtypeto As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ogddept As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdept As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ColFTDeptCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents ColFTDeptName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogddiv As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdiv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colDivCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColDivName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogdsect As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvsect As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ColSectCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColSectName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogdunitsect As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvunitsect As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ColUnitsectCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColUnitSectName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogdemp As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvemp As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ColEMpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColEmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysEmpTypeId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysEmpTypeIdTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNEmpTypeCon As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNHSysEmpTypeIdTo_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysEmpTypeId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysDeptIdTo_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysDeptId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNDeptCon As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents olbdeptTo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysDeptId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysDeptIdTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysDivisonIdTo_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysDivisonId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNDivisionCon As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents olbdivision As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysDivisonId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysDivisonIdTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysSectIdTo_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysSectId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNSectCon As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysSectId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysSectIdTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysUnitSectIdTo_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysUnitSectId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNUnitSectCon As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysUnitSectId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysUnitSectIdTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysEmpIDTo_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysEmpID_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNEmpCon As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysEmpID As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysEmpIDTo As DevExpress.XtraEditors.ButtonEdit

End Class
