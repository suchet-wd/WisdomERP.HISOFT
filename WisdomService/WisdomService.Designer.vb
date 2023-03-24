<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WisdomService
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.otmdctimer = New System.Windows.Forms.Timer(Me.components)
        Me.otmqafinalleader = New System.Windows.Forms.Timer(Me.components)
        Me.otmsewinglineleader = New System.Windows.Forms.Timer(Me.components)
        Me.otmcheckinvoicecharge = New System.Windows.Forms.Timer(Me.components)
        Me.ocmcheckwhappcm = New System.Windows.Forms.Timer(Me.components)
        Me.otmDirectorAssetPO = New System.Windows.Forms.Timer(Me.components)
        Me.otmAssetPo = New System.Windows.Forms.Timer(Me.components)
        Me.otmDirectorAssetPR = New System.Windows.Forms.Timer(Me.components)
        Me.otmcheckAssetPR = New System.Windows.Forms.Timer(Me.components)
        Me.otmqastyleriskcritical = New System.Windows.Forms.Timer(Me.components)
        Me.otmChkOrderCostApp = New System.Windows.Forms.Timer(Me.components)
        Me.otmChkEmpLeaveApp = New System.Windows.Forms.Timer(Me.components)
        Me.otmcheckappr = New System.Windows.Forms.Timer(Me.components)
        Me.ocmcheckapp = New System.Windows.Forms.Timer(Me.components)
        Me.ocmcheckappdirector = New System.Windows.Forms.Timer(Me.components)
        Me.ocmcheckmanagerfactoryapp = New System.Windows.Forms.Timer(Me.components)
        Me.otmcheckmerapptvw = New System.Windows.Forms.Timer(Me.components)
        Me.ocmcheckappsmp = New System.Windows.Forms.Timer(Me.components)
        Me.ocmcheckappsmpmgr = New System.Windows.Forms.Timer(Me.components)
        Me.otmcheckapprdsam = New System.Windows.Forms.Timer(Me.components)
        Me.otmcheckSafetyPR = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(109, 65)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Label1"
        '
        'otmdctimer
        '
        Me.otmdctimer.Interval = 60000
        '
        'otmqafinalleader
        '
        Me.otmqafinalleader.Interval = 60000
        '
        'otmsewinglineleader
        '
        Me.otmsewinglineleader.Interval = 60000
        '
        'otmcheckinvoicecharge
        '
        Me.otmcheckinvoicecharge.Interval = 600000
        '
        'ocmcheckwhappcm
        '
        Me.ocmcheckwhappcm.Interval = 60000
        '
        'otmDirectorAssetPO
        '
        Me.otmDirectorAssetPO.Interval = 120000
        '
        'otmAssetPo
        '
        Me.otmAssetPo.Interval = 120000
        '
        'otmDirectorAssetPR
        '
        Me.otmDirectorAssetPR.Interval = 120000
        '
        'otmcheckAssetPR
        '
        Me.otmcheckAssetPR.Interval = 120000
        '
        'otmqastyleriskcritical
        '
        Me.otmqastyleriskcritical.Interval = 60000
        '
        'otmChkOrderCostApp
        '
        Me.otmChkOrderCostApp.Interval = 60000
        '
        'otmChkEmpLeaveApp
        '
        Me.otmChkEmpLeaveApp.Interval = 60000
        '
        'otmcheckappr
        '
        Me.otmcheckappr.Interval = 60000
        '
        'ocmcheckapp
        '
        Me.ocmcheckapp.Interval = 120000
        '
        'ocmcheckappdirector
        '
        Me.ocmcheckappdirector.Interval = 60000
        '
        'ocmcheckmanagerfactoryapp
        '
        Me.ocmcheckmanagerfactoryapp.Interval = 60000
        '
        'otmcheckmerapptvw
        '
        Me.otmcheckmerapptvw.Interval = 60000
        '
        'ocmcheckappsmp
        '
        Me.ocmcheckappsmp.Interval = 120000
        '
        'ocmcheckappsmpmgr
        '
        Me.ocmcheckappsmpmgr.Interval = 120000
        '
        'otmcheckapprdsam
        '
        Me.otmcheckapprdsam.Interval = 500000
        '
        'otmcheckSafetyPR
        '
        Me.otmcheckSafetyPR.Interval = 120000
        '
        'WisdomService
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(533, 346)
        Me.Controls.Add(Me.Label1)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "WisdomService"
        Me.Opacity = 0.0R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents otmdctimer As Timer
    Friend WithEvents otmqafinalleader As Timer
    Friend WithEvents otmsewinglineleader As Timer
    Friend WithEvents otmcheckinvoicecharge As Timer
    Friend WithEvents ocmcheckwhappcm As Timer
    Friend WithEvents otmDirectorAssetPO As Timer
    Friend WithEvents otmAssetPo As Timer
    Friend WithEvents otmDirectorAssetPR As Timer
    Friend WithEvents otmcheckAssetPR As Timer
    Friend WithEvents otmqastyleriskcritical As Timer
    Friend WithEvents otmChkOrderCostApp As Timer
    Friend WithEvents otmChkEmpLeaveApp As Timer
    Friend WithEvents otmcheckappr As Timer
    Friend WithEvents ocmcheckapp As Timer
    Friend WithEvents ocmcheckappdirector As Timer
    Friend WithEvents ocmcheckmanagerfactoryapp As Timer
    Friend WithEvents otmcheckmerapptvw As Timer
    Friend WithEvents ocmcheckappsmp As Timer
    Friend WithEvents ocmcheckappsmpmgr As Timer
    Friend WithEvents otmcheckapprdsam As Timer
    Friend WithEvents otmcheckSafetyPR As System.Windows.Forms.Timer
End Class
