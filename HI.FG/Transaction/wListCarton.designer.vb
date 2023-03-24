<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wListCarton
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wListCarton))
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ocmOK = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclose = New DevExpress.XtraEditors.SimpleButton()
        Me.cFTPackNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNCartonNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTBarCodeCarton = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogcdetail
        '
        Me.ogcdetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcdetail.Location = New System.Drawing.Point(2, 4)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemFTSelect})
        Me.ogcdetail.Size = New System.Drawing.Size(383, 331)
        Me.ogcdetail.TabIndex = 1
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTPackNo, Me.cFNCartonNo, Me.cFTState, Me.cFTBarCodeCarton, Me.cFNQuantity})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.PictureChecked = CType(resources.GetObject("RepositoryItemCheckEdit1.PictureChecked"), System.Drawing.Image)
        Me.RepositoryItemCheckEdit1.PictureUnchecked = CType(resources.GetObject("RepositoryItemCheckEdit1.PictureUnchecked"), System.Drawing.Image)
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'ocmOK
        '
        Me.ocmOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmOK.Location = New System.Drawing.Point(225, 341)
        Me.ocmOK.Name = "ocmOK"
        Me.ocmOK.Size = New System.Drawing.Size(75, 23)
        Me.ocmOK.TabIndex = 2
        Me.ocmOK.Text = "OK"
        '
        'ocmclose
        '
        Me.ocmclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmclose.Location = New System.Drawing.Point(306, 341)
        Me.ocmclose.Name = "ocmclose"
        Me.ocmclose.Size = New System.Drawing.Size(75, 23)
        Me.ocmclose.TabIndex = 2
        Me.ocmclose.Text = "CLOSE"
        '
        'cFTPackNo
        '
        Me.cFTPackNo.Caption = "FTPackNo"
        Me.cFTPackNo.FieldName = "FTPackNo"
        Me.cFTPackNo.Name = "cFTPackNo"
        Me.cFTPackNo.OptionsColumn.AllowEdit = False
        Me.cFTPackNo.OptionsColumn.ReadOnly = True
        Me.cFTPackNo.Visible = True
        Me.cFTPackNo.VisibleIndex = 0
        Me.cFTPackNo.Width = 140
        '
        'cFNCartonNo
        '
        Me.cFNCartonNo.Caption = "FNCartonNo"
        Me.cFNCartonNo.FieldName = "FNCartonNo"
        Me.cFNCartonNo.Name = "cFNCartonNo"
        Me.cFNCartonNo.OptionsColumn.AllowEdit = False
        Me.cFNCartonNo.OptionsColumn.ReadOnly = True
        Me.cFNCartonNo.Visible = True
        Me.cFNCartonNo.VisibleIndex = 1
        Me.cFNCartonNo.Width = 123
        '
        'cFTState
        '
        Me.cFTState.Caption = "FTState"
        Me.cFTState.ColumnEdit = Me.RepositoryItemFTSelect
        Me.cFTState.FieldName = "FTState"
        Me.cFTState.Name = "cFTState"
        Me.cFTState.Visible = True
        Me.cFTState.VisibleIndex = 2
        '
        'RepositoryItemFTSelect
        '
        Me.RepositoryItemFTSelect.AutoHeight = False
        Me.RepositoryItemFTSelect.Caption = "Check"
        Me.RepositoryItemFTSelect.Name = "RepositoryItemFTSelect"
        Me.RepositoryItemFTSelect.ValueChecked = "1"
        Me.RepositoryItemFTSelect.ValueUnchecked = "0"
        '
        'cFTBarCodeCarton
        '
        Me.cFTBarCodeCarton.Caption = "FTBarCodeCarton"
        Me.cFTBarCodeCarton.FieldName = "FTBarCodeCarton"
        Me.cFTBarCodeCarton.Name = "cFTBarCodeCarton"
        '
        'cFNQuantity
        '
        Me.cFNQuantity.Caption = "FNQuantity"
        Me.cFNQuantity.FieldName = "FNQuantity"
        Me.cFNQuantity.Name = "cFNQuantity"
        '
        'wListCarton
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(386, 373)
        Me.Controls.Add(Me.ocmclose)
        Me.Controls.Add(Me.ocmOK)
        Me.Controls.Add(Me.ogcdetail)
        Me.Name = "wListCarton"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wListCarton"
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ocmOK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cFTPackNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNCartonNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTBarCodeCarton As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
End Class
