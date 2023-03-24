<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TCPReceiverPort
    Inherits System.Windows.Forms.Form

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
        Me.components = New System.ComponentModel.Container()
        Me.TReceive = New System.Windows.Forms.RichTextBox()
        Me.LDoor = New System.Windows.Forms.Label()
        Me.LMode = New System.Windows.Forms.Label()
        Me.LLAC = New System.Windows.Forms.Label()
        Me.LCID = New System.Windows.Forms.Label()
        Me.LEngine = New System.Windows.Forms.Label()
        Me.LIMEI = New System.Windows.Forms.Label()
        Me.LSIM = New System.Windows.Forms.Label()
        Me.LPosition = New System.Windows.Forms.Label()
        Me.LSpeed = New System.Windows.Forms.Label()
        Me.LDate = New System.Windows.Forms.Label()
        Me.LTime = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'TReceive
        '
        Me.TReceive.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TReceive.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.TReceive.ForeColor = System.Drawing.Color.Yellow
        Me.TReceive.Location = New System.Drawing.Point(0, 0)
        Me.TReceive.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TReceive.Name = "TReceive"
        Me.TReceive.ReadOnly = True
        Me.TReceive.Size = New System.Drawing.Size(405, 421)
        Me.TReceive.TabIndex = 0
        Me.TReceive.Text = ""
        '
        'LDoor
        '
        Me.LDoor.AutoSize = True
        Me.LDoor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.LDoor.ForeColor = System.Drawing.Color.White
        Me.LDoor.Location = New System.Drawing.Point(24, 247)
        Me.LDoor.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LDoor.Name = "LDoor"
        Me.LDoor.Size = New System.Drawing.Size(43, 17)
        Me.LDoor.TabIndex = 1
        Me.LDoor.Text = "Door"
        Me.LDoor.Visible = False
        '
        'LMode
        '
        Me.LMode.AutoSize = True
        Me.LMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.LMode.ForeColor = System.Drawing.Color.White
        Me.LMode.Location = New System.Drawing.Point(24, 277)
        Me.LMode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LMode.Name = "LMode"
        Me.LMode.Size = New System.Drawing.Size(77, 17)
        Me.LMode.TabIndex = 2
        Me.LMode.Text = "Car Mode"
        Me.LMode.Visible = False
        '
        'LLAC
        '
        Me.LLAC.AutoSize = True
        Me.LLAC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.LLAC.ForeColor = System.Drawing.Color.White
        Me.LLAC.Location = New System.Drawing.Point(24, 306)
        Me.LLAC.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LLAC.Name = "LLAC"
        Me.LLAC.Size = New System.Drawing.Size(37, 17)
        Me.LLAC.TabIndex = 3
        Me.LLAC.Text = "LAC"
        Me.LLAC.Visible = False
        '
        'LCID
        '
        Me.LCID.AutoSize = True
        Me.LCID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.LCID.ForeColor = System.Drawing.Color.White
        Me.LCID.Location = New System.Drawing.Point(24, 336)
        Me.LCID.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LCID.Name = "LCID"
        Me.LCID.Size = New System.Drawing.Size(33, 17)
        Me.LCID.TabIndex = 4
        Me.LCID.Text = "CID"
        Me.LCID.Visible = False
        '
        'LEngine
        '
        Me.LEngine.AutoSize = True
        Me.LEngine.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.LEngine.ForeColor = System.Drawing.Color.White
        Me.LEngine.Location = New System.Drawing.Point(24, 366)
        Me.LEngine.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LEngine.Name = "LEngine"
        Me.LEngine.Size = New System.Drawing.Size(58, 17)
        Me.LEngine.TabIndex = 5
        Me.LEngine.Text = "Engine"
        Me.LEngine.Visible = False
        '
        'LIMEI
        '
        Me.LIMEI.AutoSize = True
        Me.LIMEI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.LIMEI.ForeColor = System.Drawing.Color.White
        Me.LIMEI.Location = New System.Drawing.Point(24, 395)
        Me.LIMEI.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LIMEI.Name = "LIMEI"
        Me.LIMEI.Size = New System.Drawing.Size(38, 17)
        Me.LIMEI.TabIndex = 6
        Me.LIMEI.Text = "IMEI"
        Me.LIMEI.Visible = False
        '
        'LSIM
        '
        Me.LSIM.AutoSize = True
        Me.LSIM.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.LSIM.ForeColor = System.Drawing.Color.White
        Me.LSIM.Location = New System.Drawing.Point(24, 425)
        Me.LSIM.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LSIM.Name = "LSIM"
        Me.LSIM.Size = New System.Drawing.Size(34, 17)
        Me.LSIM.TabIndex = 7
        Me.LSIM.Text = "SIM"
        Me.LSIM.Visible = False
        '
        'LPosition
        '
        Me.LPosition.AutoSize = True
        Me.LPosition.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.LPosition.ForeColor = System.Drawing.Color.White
        Me.LPosition.Location = New System.Drawing.Point(24, 454)
        Me.LPosition.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LPosition.Name = "LPosition"
        Me.LPosition.Size = New System.Drawing.Size(66, 17)
        Me.LPosition.TabIndex = 8
        Me.LPosition.Text = "Position"
        Me.LPosition.Visible = False
        '
        'LSpeed
        '
        Me.LSpeed.AutoSize = True
        Me.LSpeed.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.LSpeed.ForeColor = System.Drawing.Color.White
        Me.LSpeed.Location = New System.Drawing.Point(24, 484)
        Me.LSpeed.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LSpeed.Name = "LSpeed"
        Me.LSpeed.Size = New System.Drawing.Size(54, 17)
        Me.LSpeed.TabIndex = 9
        Me.LSpeed.Text = "Speed"
        Me.LSpeed.Visible = False
        '
        'LDate
        '
        Me.LDate.AutoSize = True
        Me.LDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.LDate.ForeColor = System.Drawing.Color.White
        Me.LDate.Location = New System.Drawing.Point(24, 513)
        Me.LDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LDate.Name = "LDate"
        Me.LDate.Size = New System.Drawing.Size(42, 17)
        Me.LDate.TabIndex = 10
        Me.LDate.Text = "Date"
        Me.LDate.Visible = False
        '
        'LTime
        '
        Me.LTime.AutoSize = True
        Me.LTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.LTime.ForeColor = System.Drawing.Color.White
        Me.LTime.Location = New System.Drawing.Point(24, 542)
        Me.LTime.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LTime.Name = "LTime"
        Me.LTime.Size = New System.Drawing.Size(43, 17)
        Me.LTime.TabIndex = 10
        Me.LTime.Text = "Time"
        Me.LTime.Visible = False
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(403, 419)
        Me.Controls.Add(Me.LTime)
        Me.Controls.Add(Me.LDate)
        Me.Controls.Add(Me.LSpeed)
        Me.Controls.Add(Me.LPosition)
        Me.Controls.Add(Me.LSIM)
        Me.Controls.Add(Me.LIMEI)
        Me.Controls.Add(Me.LEngine)
        Me.Controls.Add(Me.LCID)
        Me.Controls.Add(Me.LLAC)
        Me.Controls.Add(Me.LMode)
        Me.Controls.Add(Me.LDoor)
        Me.Controls.Add(Me.TReceive)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TCPReceiverPort"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Receive Data"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TReceive As System.Windows.Forms.RichTextBox
    Friend WithEvents LDoor As System.Windows.Forms.Label
    Friend WithEvents LMode As System.Windows.Forms.Label
    Friend WithEvents LLAC As System.Windows.Forms.Label
    Friend WithEvents LCID As System.Windows.Forms.Label
    Friend WithEvents LEngine As System.Windows.Forms.Label
    Friend WithEvents LIMEI As System.Windows.Forms.Label
    Friend WithEvents LSIM As System.Windows.Forms.Label
    Friend WithEvents LPosition As System.Windows.Forms.Label
    Friend WithEvents LSpeed As System.Windows.Forms.Label
    Friend WithEvents LDate As System.Windows.Forms.Label
    Friend WithEvents LTime As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer

End Class
