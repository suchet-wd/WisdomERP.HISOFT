<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wReportMERFactorySubOrderNo
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
        Me.ogbOptRptOrderNo = New DevExpress.XtraEditors.GroupControl()
        Me.ockIncludeSubOrderNoComponent = New DevExpress.XtraEditors.CheckEdit()
        Me.ockIncludeSubOrderNoSizeSpec = New DevExpress.XtraEditors.CheckEdit()
        Me.ockIncludeSubOrderNoPacking = New DevExpress.XtraEditors.CheckEdit()
        Me.ockIncludeSubOrderNoSewing = New DevExpress.XtraEditors.CheckEdit()
        Me.ogbActionBtn = New DevExpress.XtraEditors.GroupControl()
        Me.ocmCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmOK = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbOptRptOrderNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbOptRptOrderNo.SuspendLayout()
        CType(Me.ockIncludeSubOrderNoComponent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ockIncludeSubOrderNoSizeSpec.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ockIncludeSubOrderNoPacking.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ockIncludeSubOrderNoSewing.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbActionBtn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbActionBtn.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbOptRptOrderNo
        '
        Me.ogbOptRptOrderNo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbOptRptOrderNo.Controls.Add(Me.ockIncludeSubOrderNoComponent)
        Me.ogbOptRptOrderNo.Controls.Add(Me.ockIncludeSubOrderNoSizeSpec)
        Me.ogbOptRptOrderNo.Controls.Add(Me.ockIncludeSubOrderNoPacking)
        Me.ogbOptRptOrderNo.Controls.Add(Me.ockIncludeSubOrderNoSewing)
        Me.ogbOptRptOrderNo.Location = New System.Drawing.Point(4, 4)
        Me.ogbOptRptOrderNo.Name = "ogbOptRptOrderNo"
        Me.ogbOptRptOrderNo.Size = New System.Drawing.Size(440, 154)
        Me.ogbOptRptOrderNo.TabIndex = 5
        Me.ogbOptRptOrderNo.Text = "Option"
        '
        'ockIncludeSubOrderNoComponent
        '
        Me.ockIncludeSubOrderNoComponent.EditValue = "1"
        Me.ockIncludeSubOrderNoComponent.Location = New System.Drawing.Point(51, 33)
        Me.ockIncludeSubOrderNoComponent.Name = "ockIncludeSubOrderNoComponent"
        Me.ockIncludeSubOrderNoComponent.Properties.Caption = "Include with Report Factory Sub Order No. Component"
        Me.ockIncludeSubOrderNoComponent.Properties.Tag = "FTStateEmb"
        Me.ockIncludeSubOrderNoComponent.Properties.ValueChecked = "1"
        Me.ockIncludeSubOrderNoComponent.Properties.ValueUnchecked = "0"
        Me.ockIncludeSubOrderNoComponent.Size = New System.Drawing.Size(335, 20)
        Me.ockIncludeSubOrderNoComponent.TabIndex = 475
        Me.ockIncludeSubOrderNoComponent.Tag = "2|"
        '
        'ockIncludeSubOrderNoSizeSpec
        '
        Me.ockIncludeSubOrderNoSizeSpec.EditValue = "1"
        Me.ockIncludeSubOrderNoSizeSpec.Location = New System.Drawing.Point(51, 111)
        Me.ockIncludeSubOrderNoSizeSpec.Name = "ockIncludeSubOrderNoSizeSpec"
        Me.ockIncludeSubOrderNoSizeSpec.Properties.Caption = "Include with Report Factory Sub Order No. Size Spec"
        Me.ockIncludeSubOrderNoSizeSpec.Properties.Tag = "FTStateEmb"
        Me.ockIncludeSubOrderNoSizeSpec.Properties.ValueChecked = "1"
        Me.ockIncludeSubOrderNoSizeSpec.Properties.ValueUnchecked = "0"
        Me.ockIncludeSubOrderNoSizeSpec.Size = New System.Drawing.Size(335, 20)
        Me.ockIncludeSubOrderNoSizeSpec.TabIndex = 474
        Me.ockIncludeSubOrderNoSizeSpec.Tag = "2|"
        '
        'ockIncludeSubOrderNoPacking
        '
        Me.ockIncludeSubOrderNoPacking.EditValue = "1"
        Me.ockIncludeSubOrderNoPacking.Location = New System.Drawing.Point(51, 84)
        Me.ockIncludeSubOrderNoPacking.Name = "ockIncludeSubOrderNoPacking"
        Me.ockIncludeSubOrderNoPacking.Properties.Caption = "Include with Report Factory Sub Order No. Packing"
        Me.ockIncludeSubOrderNoPacking.Properties.Tag = "FTStateEmb"
        Me.ockIncludeSubOrderNoPacking.Properties.ValueChecked = "1"
        Me.ockIncludeSubOrderNoPacking.Properties.ValueUnchecked = "0"
        Me.ockIncludeSubOrderNoPacking.Size = New System.Drawing.Size(335, 20)
        Me.ockIncludeSubOrderNoPacking.TabIndex = 473
        Me.ockIncludeSubOrderNoPacking.Tag = "2|"
        '
        'ockIncludeSubOrderNoSewing
        '
        Me.ockIncludeSubOrderNoSewing.EditValue = "1"
        Me.ockIncludeSubOrderNoSewing.Location = New System.Drawing.Point(51, 58)
        Me.ockIncludeSubOrderNoSewing.Name = "ockIncludeSubOrderNoSewing"
        Me.ockIncludeSubOrderNoSewing.Properties.Caption = "Include with Report Factory Sub Order No. Sewing"
        Me.ockIncludeSubOrderNoSewing.Properties.Tag = "FTStateEmb"
        Me.ockIncludeSubOrderNoSewing.Properties.ValueChecked = "1"
        Me.ockIncludeSubOrderNoSewing.Properties.ValueUnchecked = "0"
        Me.ockIncludeSubOrderNoSewing.Size = New System.Drawing.Size(335, 20)
        Me.ockIncludeSubOrderNoSewing.TabIndex = 472
        Me.ockIncludeSubOrderNoSewing.Tag = "2|"
        '
        'ogbActionBtn
        '
        Me.ogbActionBtn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbActionBtn.Controls.Add(Me.ocmCancel)
        Me.ogbActionBtn.Controls.Add(Me.ocmOK)
        Me.ogbActionBtn.Location = New System.Drawing.Point(4, 164)
        Me.ogbActionBtn.Name = "ogbActionBtn"
        Me.ogbActionBtn.ShowCaption = False
        Me.ogbActionBtn.Size = New System.Drawing.Size(440, 42)
        Me.ogbActionBtn.TabIndex = 6
        Me.ogbActionBtn.Text = "GroupControl1"
        '
        'ocmCancel
        '
        Me.ocmCancel.Location = New System.Drawing.Point(238, 8)
        Me.ocmCancel.Name = "ocmCancel"
        Me.ocmCancel.Size = New System.Drawing.Size(90, 25)
        Me.ocmCancel.TabIndex = 1
        Me.ocmCancel.Text = "CANCEL"
        '
        'ocmOK
        '
        Me.ocmOK.Location = New System.Drawing.Point(92, 8)
        Me.ocmOK.Name = "ocmOK"
        Me.ocmOK.Size = New System.Drawing.Size(90, 25)
        Me.ocmOK.TabIndex = 0
        Me.ocmOK.Text = "O.K."
        '
        'wReportMERFactorySubOrderNo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(448, 210)
        Me.Controls.Add(Me.ogbActionBtn)
        Me.Controls.Add(Me.ogbOptRptOrderNo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wReportMERFactorySubOrderNo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Report Factory Sub Order No."
        CType(Me.ogbOptRptOrderNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbOptRptOrderNo.ResumeLayout(False)
        CType(Me.ockIncludeSubOrderNoComponent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ockIncludeSubOrderNoSizeSpec.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ockIncludeSubOrderNoPacking.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ockIncludeSubOrderNoSewing.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbActionBtn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbActionBtn.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbOptRptOrderNo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ockIncludeSubOrderNoSizeSpec As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ockIncludeSubOrderNoPacking As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ockIncludeSubOrderNoSewing As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ogbActionBtn As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmOK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ockIncludeSubOrderNoComponent As DevExpress.XtraEditors.CheckEdit
End Class
