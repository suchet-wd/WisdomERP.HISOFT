<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class uCQA
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
        Me.ogcDetailMater = New DevExpress.XtraGrid.GridControl()
        Me.ogvDetailMater = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFTQADetailCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTQADetailName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.obtBack = New DevExpress.XtraEditors.SimpleButton()
        Me.RadialMenu = New DevExpress.XtraBars.Ribbon.RadialMenu()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.ogcDetailMater, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvDetailMater, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadialMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogcDetailMater
        '
        Me.ogcDetailMater.Location = New System.Drawing.Point(54, 44)
        Me.ogcDetailMater.MainView = Me.ogvDetailMater
        Me.ogcDetailMater.Name = "ogcDetailMater"
        Me.ogcDetailMater.Size = New System.Drawing.Size(566, 299)
        Me.ogcDetailMater.TabIndex = 0
        Me.ogcDetailMater.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvDetailMater})
        '
        'ogvDetailMater
        '
        Me.ogvDetailMater.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.DarkGray
        Me.ogvDetailMater.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.LightGray
        Me.ogvDetailMater.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.ogvDetailMater.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ogvDetailMater.Appearance.HorzLine.Options.UseBackColor = True
        Me.ogvDetailMater.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTQADetailCode, Me.cFTQADetailName})
        Me.ogvDetailMater.GridControl = Me.ogcDetailMater
        Me.ogvDetailMater.Name = "ogvDetailMater"
        Me.ogvDetailMater.OptionsView.ColumnAutoWidth = False
        Me.ogvDetailMater.OptionsView.ShowGroupPanel = False
        '
        'cFTQADetailCode
        '
        Me.cFTQADetailCode.Caption = "Code"
        Me.cFTQADetailCode.FieldName = "FTQADetailCode"
        Me.cFTQADetailCode.Name = "cFTQADetailCode"
        Me.cFTQADetailCode.OptionsColumn.AllowEdit = False
        Me.cFTQADetailCode.Visible = True
        Me.cFTQADetailCode.VisibleIndex = 0
        Me.cFTQADetailCode.Width = 98
        '
        'cFTQADetailName
        '
        Me.cFTQADetailName.Caption = "Detail"
        Me.cFTQADetailName.FieldName = "FTQADetailName"
        Me.cFTQADetailName.Name = "cFTQADetailName"
        Me.cFTQADetailName.OptionsColumn.AllowEdit = False
        Me.cFTQADetailName.Visible = True
        Me.cFTQADetailName.VisibleIndex = 1
        Me.cFTQADetailName.Width = 401
        '
        'obtBack
        '
        Me.obtBack.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.obtBack.Appearance.BackColor2 = System.Drawing.Color.Transparent
        Me.obtBack.Appearance.BorderColor = System.Drawing.Color.Transparent
        Me.obtBack.Appearance.Options.UseBackColor = True
        Me.obtBack.Appearance.Options.UseBorderColor = True
        Me.obtBack.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.obtBack.Image = Global.HI.QA.My.Resources.Resources.arrow_left
        Me.obtBack.Location = New System.Drawing.Point(3, 0)
        Me.obtBack.Name = "obtBack"
        Me.obtBack.Size = New System.Drawing.Size(54, 48)
        Me.obtBack.TabIndex = 1
        '
        'RadialMenu
        '
        Me.RadialMenu.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem1)})
        Me.RadialMenu.Manager = Me.BarManager1
        Me.RadialMenu.Name = "RadialMenu"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "back"
        Me.BarButtonItem1.Id = 0
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarButtonItem1})
        Me.BarManager1.MaxItemId = 1
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(676, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 381)
        Me.barDockControlBottom.Size = New System.Drawing.Size(676, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 381)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(676, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 381)
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 20.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Location = New System.Drawing.Point(234, 5)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(189, 33)
        Me.LabelControl1.TabIndex = 6
        Me.LabelControl1.Text = "LabelControl1"
        '
        'uCQA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.ogcDetailMater)
        Me.Controls.Add(Me.obtBack)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "uCQA"
        Me.Size = New System.Drawing.Size(676, 381)
        CType(Me.ogcDetailMater, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvDetailMater, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadialMenu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ogcDetailMater As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvDetailMater As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFTQADetailCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTQADetailName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents obtBack As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents RadialMenu As DevExpress.XtraBars.Ribbon.RadialMenu
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl

End Class
