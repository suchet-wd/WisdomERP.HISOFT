<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wPurchaseTrackingPIMail
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
        Me.components = New System.ComponentModel.Container()
        Me.ogb1 = New DevExpress.XtraEditors.GroupControl()
        Me.ogbCopyOrderNoConfirm = New DevExpress.XtraEditors.GroupControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsenmail = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.RichEditControl1 = New DevExpress.XtraRichEdit.RichEditControl()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        CType(Me.ogb1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbCopyOrderNoConfirm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbCopyOrderNoConfirm.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogb1
        '
        Me.ogb1.Dock = System.Windows.Forms.DockStyle.Top
        Me.ogb1.Location = New System.Drawing.Point(0, 0)
        Me.ogb1.Name = "ogb1"
        Me.ogb1.ShowCaption = False
        Me.ogb1.Size = New System.Drawing.Size(935, 55)
        Me.ogb1.TabIndex = 0
        '
        'ogbCopyOrderNoConfirm
        '
        Me.ogbCopyOrderNoConfirm.Controls.Add(Me.ocmcancel)
        Me.ogbCopyOrderNoConfirm.Controls.Add(Me.ocmsenmail)
        Me.ogbCopyOrderNoConfirm.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ogbCopyOrderNoConfirm.Location = New System.Drawing.Point(0, 583)
        Me.ogbCopyOrderNoConfirm.Name = "ogbCopyOrderNoConfirm"
        Me.ogbCopyOrderNoConfirm.ShowCaption = False
        Me.ogbCopyOrderNoConfirm.Size = New System.Drawing.Size(935, 41)
        Me.ogbCopyOrderNoConfirm.TabIndex = 290
        Me.ogbCopyOrderNoConfirm.Text = "GroupControl2"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(689, 9)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(145, 25)
        Me.ocmcancel.TabIndex = 1
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "Cancel"
        '
        'ocmsenmail
        '
        Me.ocmsenmail.Location = New System.Drawing.Point(125, 9)
        Me.ocmsenmail.Name = "ocmsenmail"
        Me.ocmsenmail.Size = New System.Drawing.Size(133, 25)
        Me.ocmsenmail.TabIndex = 0
        Me.ocmsenmail.TabStop = False
        Me.ocmsenmail.Tag = "2|"
        Me.ocmsenmail.Text = "Send Mail"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.RichEditControl1)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 55)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.ShowCaption = False
        Me.GroupControl1.Size = New System.Drawing.Size(935, 528)
        Me.GroupControl1.TabIndex = 291
        '
        'RichEditControl1
        '
        Me.RichEditControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichEditControl1.Location = New System.Drawing.Point(2, 2)
        Me.RichEditControl1.MenuManager = Me.BarManager1
        Me.RichEditControl1.Name = "RichEditControl1"
        Me.RichEditControl1.Options.Printing.PrintPreviewFormKind = DevExpress.XtraRichEdit.PrintPreviewFormKind.Bars
        Me.RichEditControl1.Size = New System.Drawing.Size(931, 524)
        Me.RichEditControl1.TabIndex = 0
        Me.RichEditControl1.Text = "RichEditControl1"
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.MaxItemId = 371
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(935, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 624)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(935, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 624)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(935, 0)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 624)
        '
        'wPurchaseTrackingPIMail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(935, 624)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.ogbCopyOrderNoConfirm)
        Me.Controls.Add(Me.ogb1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wPurchaseTrackingPIMail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Send Mail To Vender"
        CType(Me.ogb1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbCopyOrderNoConfirm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbCopyOrderNoConfirm.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ogb1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbCopyOrderNoConfirm As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsenmail As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents RichEditControl1 As DevExpress.XtraRichEdit.RichEditControl
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
End Class
