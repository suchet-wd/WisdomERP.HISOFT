<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wRejectOrder
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
        Me.ogbOrderRejectRemark = New DevExpress.XtraEditors.GroupControl()
        Me.FDCancelAppTime = New DevExpress.XtraEditors.TextEdit()
        Me.FDCancelAppDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDCancelAppDate = New DevExpress.XtraEditors.TextEdit()
        Me.FTCancelAppBy = New DevExpress.XtraEditors.TextEdit()
        Me.FTCancelAppBy_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTCancelAppRemark = New DevExpress.XtraEditors.MemoEdit()
        Me.FTCancelAppRemark_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogbCopyOrderNoConfirm = New DevExpress.XtraEditors.GroupControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbOrderRejectRemark, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbOrderRejectRemark.SuspendLayout()
        CType(Me.FDCancelAppTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDCancelAppDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTCancelAppBy.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTCancelAppRemark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbCopyOrderNoConfirm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbCopyOrderNoConfirm.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbOrderRejectRemark
        '
        Me.ogbOrderRejectRemark.Controls.Add(Me.FDCancelAppTime)
        Me.ogbOrderRejectRemark.Controls.Add(Me.FDCancelAppDate_lbl)
        Me.ogbOrderRejectRemark.Controls.Add(Me.FDCancelAppDate)
        Me.ogbOrderRejectRemark.Controls.Add(Me.FTCancelAppBy)
        Me.ogbOrderRejectRemark.Controls.Add(Me.FTCancelAppBy_lbl)
        Me.ogbOrderRejectRemark.Controls.Add(Me.FTCancelAppRemark)
        Me.ogbOrderRejectRemark.Controls.Add(Me.FTCancelAppRemark_lbl)
        Me.ogbOrderRejectRemark.Location = New System.Drawing.Point(5, 5)
        Me.ogbOrderRejectRemark.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbOrderRejectRemark.Name = "ogbOrderRejectRemark"
        Me.ogbOrderRejectRemark.Size = New System.Drawing.Size(855, 305)
        Me.ogbOrderRejectRemark.TabIndex = 0
        Me.ogbOrderRejectRemark.Text = "Remark"
        '
        'FDCancelAppTime
        '
        Me.FDCancelAppTime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FDCancelAppTime.Location = New System.Drawing.Point(671, 276)
        Me.FDCancelAppTime.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDCancelAppTime.Name = "FDCancelAppTime"
        Me.FDCancelAppTime.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FDCancelAppTime.Properties.Appearance.Options.UseBackColor = True
        Me.FDCancelAppTime.Properties.ReadOnly = True
        Me.FDCancelAppTime.Size = New System.Drawing.Size(66, 22)
        Me.FDCancelAppTime.TabIndex = 6
        Me.FDCancelAppTime.TabStop = False
        Me.FDCancelAppTime.Tag = "2|"
        '
        'FDCancelAppDate_lbl
        '
        Me.FDCancelAppDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FDCancelAppDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDCancelAppDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDCancelAppDate_lbl.Location = New System.Drawing.Point(427, 276)
        Me.FDCancelAppDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDCancelAppDate_lbl.Name = "FDCancelAppDate_lbl"
        Me.FDCancelAppDate_lbl.Size = New System.Drawing.Size(149, 23)
        Me.FDCancelAppDate_lbl.TabIndex = 2
        Me.FDCancelAppDate_lbl.Tag = "2|"
        Me.FDCancelAppDate_lbl.Text = "วันที่ทำการยกเลิกรายการ :"
        '
        'FDCancelAppDate
        '
        Me.FDCancelAppDate.Location = New System.Drawing.Point(583, 276)
        Me.FDCancelAppDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDCancelAppDate.Name = "FDCancelAppDate"
        Me.FDCancelAppDate.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FDCancelAppDate.Properties.Appearance.Options.UseBackColor = True
        Me.FDCancelAppDate.Properties.ReadOnly = True
        Me.FDCancelAppDate.Size = New System.Drawing.Size(79, 22)
        Me.FDCancelAppDate.TabIndex = 5
        Me.FDCancelAppDate.TabStop = False
        Me.FDCancelAppDate.Tag = "2|"
        '
        'FTCancelAppBy
        '
        Me.FTCancelAppBy.Location = New System.Drawing.Point(315, 276)
        Me.FTCancelAppBy.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTCancelAppBy.Name = "FTCancelAppBy"
        Me.FTCancelAppBy.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTCancelAppBy.Properties.Appearance.Options.UseBackColor = True
        Me.FTCancelAppBy.Properties.ReadOnly = True
        Me.FTCancelAppBy.Size = New System.Drawing.Size(105, 22)
        Me.FTCancelAppBy.TabIndex = 4
        Me.FTCancelAppBy.TabStop = False
        Me.FTCancelAppBy.Tag = ""
        '
        'FTCancelAppBy_lbl
        '
        Me.FTCancelAppBy_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTCancelAppBy_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTCancelAppBy_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTCancelAppBy_lbl.Location = New System.Drawing.Point(150, 276)
        Me.FTCancelAppBy_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTCancelAppBy_lbl.Name = "FTCancelAppBy_lbl"
        Me.FTCancelAppBy_lbl.Size = New System.Drawing.Size(157, 23)
        Me.FTCancelAppBy_lbl.TabIndex = 1
        Me.FTCancelAppBy_lbl.Tag = "2|"
        Me.FTCancelAppBy_lbl.Text = "ทำการยกเลิกรายการโดย  :"
        '
        'FTCancelAppRemark
        '
        Me.FTCancelAppRemark.EditValue = ""
        Me.FTCancelAppRemark.Location = New System.Drawing.Point(187, 39)
        Me.FTCancelAppRemark.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTCancelAppRemark.Name = "FTCancelAppRemark"
        Me.FTCancelAppRemark.Properties.MaxLength = 500
        Me.FTCancelAppRemark.Size = New System.Drawing.Size(658, 231)
        Me.FTCancelAppRemark.TabIndex = 3
        Me.FTCancelAppRemark.Tag = "2|"
        Me.FTCancelAppRemark.UseOptimizedRendering = True
        '
        'FTCancelAppRemark_lbl
        '
        Me.FTCancelAppRemark_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTCancelAppRemark_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTCancelAppRemark_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTCancelAppRemark_lbl.Location = New System.Drawing.Point(6, 42)
        Me.FTCancelAppRemark_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTCancelAppRemark_lbl.Name = "FTCancelAppRemark_lbl"
        Me.FTCancelAppRemark_lbl.Size = New System.Drawing.Size(174, 23)
        Me.FTCancelAppRemark_lbl.TabIndex = 0
        Me.FTCancelAppRemark_lbl.Tag = "2|"
        Me.FTCancelAppRemark_lbl.Text = "Reject Remark :"
        '
        'ogbCopyOrderNoConfirm
        '
        Me.ogbCopyOrderNoConfirm.Controls.Add(Me.ocmcancel)
        Me.ogbCopyOrderNoConfirm.Controls.Add(Me.ocmok)
        Me.ogbCopyOrderNoConfirm.Location = New System.Drawing.Point(5, 318)
        Me.ogbCopyOrderNoConfirm.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbCopyOrderNoConfirm.Name = "ogbCopyOrderNoConfirm"
        Me.ogbCopyOrderNoConfirm.ShowCaption = False
        Me.ogbCopyOrderNoConfirm.Size = New System.Drawing.Size(855, 50)
        Me.ogbCopyOrderNoConfirm.TabIndex = 1
        Me.ogbCopyOrderNoConfirm.Text = "GroupControl2"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(512, 11)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(169, 31)
        Me.ocmcancel.TabIndex = 1
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "Cancel"
        '
        'ocmok
        '
        Me.ocmok.Location = New System.Drawing.Point(173, 11)
        Me.ocmok.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(155, 31)
        Me.ocmok.TabIndex = 0
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "OK"
        '
        'wRejectOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(864, 373)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogbCopyOrderNoConfirm)
        Me.Controls.Add(Me.ogbOrderRejectRemark)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wRejectOrder"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Reject Order"
        CType(Me.ogbOrderRejectRemark, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbOrderRejectRemark.ResumeLayout(False)
        CType(Me.FDCancelAppTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDCancelAppDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTCancelAppBy.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTCancelAppRemark.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbCopyOrderNoConfirm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbCopyOrderNoConfirm.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbOrderRejectRemark As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTCancelAppRemark_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTCancelAppRemark As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents ogbCopyOrderNoConfirm As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FDCancelAppTime As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FDCancelAppDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDCancelAppDate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTCancelAppBy As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTCancelAppBy_lbl As DevExpress.XtraEditors.LabelControl
End Class
