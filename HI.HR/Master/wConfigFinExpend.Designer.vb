<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wConfigFinExpend
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
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmaddnew = New DevExpress.XtraEditors.SimpleButton()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FNHSysEmpID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysEmpTypeId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTEmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTEmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTEmpTypeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTEmpTypeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFinCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFinDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPayYearBegin = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPayTermBegin = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPayYearEnd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPayTermEnd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FCFinAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysFinExpendID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FCFinAmtTotal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FCFinAmtTotalSysExpend = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStaCompletedPayment = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbdetail
        '
        Me.ogbdetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbdetail.Controls.Add(Me.ogcdetail)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdetail.Location = New System.Drawing.Point(0, 0)
        Me.ogbdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.Size = New System.Drawing.Size(936, 709)
        Me.ogbdetail.TabIndex = 12
        Me.ogbdetail.Tag = "2|"
        Me.ogbdetail.Text = "Config Payment"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmaddnew)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(6, 188)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(916, 58)
        Me.ogbmainprocbutton.TabIndex = 136
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(143, 12)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(116, 35)
        Me.ocmload.TabIndex = 331
        Me.ocmload.Text = "Load Data"
        '
        'ocmexit
        '
        Me.ocmexit.Location = New System.Drawing.Point(723, 14)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmaddnew
        '
        Me.ocmaddnew.Location = New System.Drawing.Point(10, 14)
        Me.ocmaddnew.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmaddnew.Name = "ocmaddnew"
        Me.ocmaddnew.Size = New System.Drawing.Size(111, 31)
        Me.ocmaddnew.TabIndex = 93
        Me.ocmaddnew.TabStop = False
        Me.ocmaddnew.Tag = "2|"
        Me.ocmaddnew.Text = "NEW"
        '
        'ogcdetail
        '
        Me.ogcdetail.Dock = System.Windows.Forms.DockStyle.Top
        Me.ogcdetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Location = New System.Drawing.Point(2, 25)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.ogcdetail.Size = New System.Drawing.Size(932, 615)
        Me.ogcdetail.TabIndex = 1
        Me.ogcdetail.TabStop = False
        Me.ogcdetail.Tag = "2|"
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FNHSysEmpID, Me.FNHSysEmpTypeId, Me.FTEmpCode, Me.FTEmpName, Me.FTEmpTypeCode, Me.FTEmpTypeName, Me.FTFinCode, Me.FTFinDesc, Me.FTPayYearBegin, Me.FTPayTermBegin, Me.FTPayYearEnd, Me.FTPayTermEnd, Me.FCFinAmt, Me.FNHSysFinExpendID, Me.FCFinAmtTotal, Me.FCFinAmtTotalSysExpend, Me.FTStaCompletedPayment})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsCustomization.AllowGroup = False
        Me.ogvdetail.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdetail.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        Me.ogvdetail.Tag = "2|"
        '
        'FNHSysEmpID
        '
        Me.FNHSysEmpID.Caption = "FNHSysEmpID"
        Me.FNHSysEmpID.FieldName = "FNHSysEmpID"
        Me.FNHSysEmpID.Name = "FNHSysEmpID"
        Me.FNHSysEmpID.OptionsColumn.AllowEdit = False
        Me.FNHSysEmpID.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        '
        'FNHSysEmpTypeId
        '
        Me.FNHSysEmpTypeId.Caption = "FNHSysEmpTypeId"
        Me.FNHSysEmpTypeId.FieldName = "FNHSysEmpTypeId"
        Me.FNHSysEmpTypeId.Name = "FNHSysEmpTypeId"
        Me.FNHSysEmpTypeId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        '
        'FTEmpCode
        '
        Me.FTEmpCode.Caption = "FTEmpCode"
        Me.FTEmpCode.FieldName = "FTEmpCode"
        Me.FTEmpCode.Name = "FTEmpCode"
        Me.FTEmpCode.OptionsColumn.AllowEdit = False
        Me.FTEmpCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTEmpCode.Visible = True
        Me.FTEmpCode.VisibleIndex = 0
        '
        'FTEmpName
        '
        Me.FTEmpName.Caption = "FTEmpName"
        Me.FTEmpName.FieldName = "FTEmpName"
        Me.FTEmpName.Name = "FTEmpName"
        Me.FTEmpName.OptionsColumn.AllowEdit = False
        Me.FTEmpName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTEmpName.Visible = True
        Me.FTEmpName.VisibleIndex = 1
        '
        'FTEmpTypeCode
        '
        Me.FTEmpTypeCode.Caption = "FTEmpTypeCode"
        Me.FTEmpTypeCode.FieldName = "FTEmpTypeCode"
        Me.FTEmpTypeCode.Name = "FTEmpTypeCode"
        Me.FTEmpTypeCode.OptionsColumn.AllowEdit = False
        Me.FTEmpTypeCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTEmpTypeCode.Visible = True
        Me.FTEmpTypeCode.VisibleIndex = 2
        '
        'FTEmpTypeName
        '
        Me.FTEmpTypeName.Caption = "FTEmpTypeName"
        Me.FTEmpTypeName.FieldName = "FTEmpTypeName"
        Me.FTEmpTypeName.Name = "FTEmpTypeName"
        Me.FTEmpTypeName.OptionsColumn.AllowEdit = False
        Me.FTEmpTypeName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTEmpTypeName.Visible = True
        Me.FTEmpTypeName.VisibleIndex = 3
        '
        'FTFinCode
        '
        Me.FTFinCode.Caption = "FTFinCode"
        Me.FTFinCode.FieldName = "FTFinCode"
        Me.FTFinCode.Name = "FTFinCode"
        Me.FTFinCode.OptionsColumn.AllowEdit = False
        Me.FTFinCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTFinCode.Visible = True
        Me.FTFinCode.VisibleIndex = 4
        '
        'FTFinDesc
        '
        Me.FTFinDesc.Caption = "FTFinDesc"
        Me.FTFinDesc.FieldName = "FTFinDesc"
        Me.FTFinDesc.Name = "FTFinDesc"
        Me.FTFinDesc.OptionsColumn.AllowEdit = False
        Me.FTFinDesc.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTFinDesc.Visible = True
        Me.FTFinDesc.VisibleIndex = 5
        '
        'FTPayYearBegin
        '
        Me.FTPayYearBegin.Caption = "FTPayYearBegin"
        Me.FTPayYearBegin.FieldName = "FTPayYearBegin"
        Me.FTPayYearBegin.Name = "FTPayYearBegin"
        Me.FTPayYearBegin.OptionsColumn.AllowEdit = False
        Me.FTPayYearBegin.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTPayYearBegin.Visible = True
        Me.FTPayYearBegin.VisibleIndex = 6
        '
        'FTPayTermBegin
        '
        Me.FTPayTermBegin.Caption = "FTPayTermBegin"
        Me.FTPayTermBegin.FieldName = "FTPayTermBegin"
        Me.FTPayTermBegin.Name = "FTPayTermBegin"
        Me.FTPayTermBegin.OptionsColumn.AllowEdit = False
        Me.FTPayTermBegin.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTPayTermBegin.Visible = True
        Me.FTPayTermBegin.VisibleIndex = 7
        '
        'FTPayYearEnd
        '
        Me.FTPayYearEnd.Caption = "FTPayYearEnd"
        Me.FTPayYearEnd.FieldName = "FTPayYearEnd"
        Me.FTPayYearEnd.Name = "FTPayYearEnd"
        Me.FTPayYearEnd.OptionsColumn.AllowEdit = False
        Me.FTPayYearEnd.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTPayYearEnd.Visible = True
        Me.FTPayYearEnd.VisibleIndex = 8
        '
        'FTPayTermEnd
        '
        Me.FTPayTermEnd.Caption = "FTPayTermEnd"
        Me.FTPayTermEnd.FieldName = "FTPayTermEnd"
        Me.FTPayTermEnd.Name = "FTPayTermEnd"
        Me.FTPayTermEnd.OptionsColumn.AllowEdit = False
        Me.FTPayTermEnd.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTPayTermEnd.Visible = True
        Me.FTPayTermEnd.VisibleIndex = 9
        '
        'FCFinAmt
        '
        Me.FCFinAmt.Caption = "FCFinAmt"
        Me.FCFinAmt.FieldName = "FCFinAmt"
        Me.FCFinAmt.Name = "FCFinAmt"
        Me.FCFinAmt.OptionsColumn.AllowEdit = False
        Me.FCFinAmt.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FCFinAmt.Visible = True
        Me.FCFinAmt.VisibleIndex = 10
        '
        'FNHSysFinExpendID
        '
        Me.FNHSysFinExpendID.Caption = "FNHSysFinExpendID"
        Me.FNHSysFinExpendID.FieldName = "FNHSysFinExpendID"
        Me.FNHSysFinExpendID.Name = "FNHSysFinExpendID"
        '
        'FCFinAmtTotal
        '
        Me.FCFinAmtTotal.Caption = "FCFinAmtTotal"
        Me.FCFinAmtTotal.FieldName = "FCFinAmtTotal"
        Me.FCFinAmtTotal.Name = "FCFinAmtTotal"
        Me.FCFinAmtTotal.OptionsColumn.AllowEdit = False
        Me.FCFinAmtTotal.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FCFinAmtTotal.Visible = True
        Me.FCFinAmtTotal.VisibleIndex = 11
        '
        'FCFinAmtTotalSysExpend
        '
        Me.FCFinAmtTotalSysExpend.Caption = "FCFinAmtTotalSysExpend"
        Me.FCFinAmtTotalSysExpend.FieldName = "FCFinAmtTotalSysExpend"
        Me.FCFinAmtTotalSysExpend.Name = "FCFinAmtTotalSysExpend"
        Me.FCFinAmtTotalSysExpend.OptionsColumn.AllowEdit = False
        Me.FCFinAmtTotalSysExpend.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        '
        'FTStaCompletedPayment
        '
        Me.FTStaCompletedPayment.Caption = "FTStaCompletedPayment"
        Me.FTStaCompletedPayment.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.FTStaCompletedPayment.FieldName = "FTStaCompletedPayment"
        Me.FTStaCompletedPayment.Name = "FTStaCompletedPayment"
        Me.FTStaCompletedPayment.OptionsColumn.AllowEdit = False
        Me.FTStaCompletedPayment.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTStaCompletedPayment.Visible = True
        Me.FTStaCompletedPayment.VisibleIndex = 12
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'wConfigFinExpend
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(936, 709)
        Me.Controls.Add(Me.ogbdetail)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wConfigFinExpend"
        Me.Text = "wConfigFinExpend"
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmaddnew As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FNHSysEmpID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysEmpTypeId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTEmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTEmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTEmpTypeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTEmpTypeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFinCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFinDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPayYearBegin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPayTermBegin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPayYearEnd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPayTermEnd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FCFinAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysFinExpendID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FCFinAmtTotal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FCFinAmtTotalSysExpend As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStaCompletedPayment As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
End Class
