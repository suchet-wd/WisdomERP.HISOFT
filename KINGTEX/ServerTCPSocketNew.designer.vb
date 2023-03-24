<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ServerTCPSocketNew
    Inherits System.Windows.Forms.Form

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
        Me.GData = New System.Windows.Forms.DataGridView()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TStart = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BOpenPort = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TData = New System.Windows.Forms.TextBox()
        Me.GPortClose = New System.Windows.Forms.DataGridView()
        Me.Gport = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.BStopPort = New System.Windows.Forms.Button()
        CType(Me.GData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GPortClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GData
        '
        Me.GData.AllowUserToAddRows = False
        Me.GData.AllowUserToDeleteRows = False
        Me.GData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column4, Me.Column1, Me.Column2})
        Me.GData.Location = New System.Drawing.Point(2, 67)
        Me.GData.Name = "GData"
        Me.GData.RowHeadersVisible = False
        Me.GData.Size = New System.Drawing.Size(772, 463)
        Me.GData.TabIndex = 0
        '
        'Column4
        '
        Me.Column4.HeaderText = "time"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column1
        '
        Me.Column1.HeaderText = "Port"
        Me.Column1.Name = "Column1"
        '
        'Column2
        '
        Me.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Column2.HeaderText = "Data"
        Me.Column2.MinimumWidth = 500
        Me.Column2.Name = "Column2"
        '
        'TStart
        '
        Me.TStart.Location = New System.Drawing.Point(107, 9)
        Me.TStart.Name = "TStart"
        Me.TStart.Size = New System.Drawing.Size(86, 20)
        Me.TStart.TabIndex = 1
        Me.TStart.Text = "8081"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(50, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Port Start"
        '
        'BOpenPort
        '
        Me.BOpenPort.Location = New System.Drawing.Point(198, 7)
        Me.BOpenPort.Name = "BOpenPort"
        Me.BOpenPort.Size = New System.Drawing.Size(153, 23)
        Me.BOpenPort.TabIndex = 3
        Me.BOpenPort.Text = "Open Port"
        Me.BOpenPort.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 8000
        '
        'TData
        '
        Me.TData.Location = New System.Drawing.Point(2, 42)
        Me.TData.Name = "TData"
        Me.TData.ReadOnly = True
        Me.TData.Size = New System.Drawing.Size(773, 20)
        Me.TData.TabIndex = 4
        '
        'GPortClose
        '
        Me.GPortClose.AllowUserToAddRows = False
        Me.GPortClose.AllowUserToDeleteRows = False
        Me.GPortClose.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GPortClose.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Gport})
        Me.GPortClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.GPortClose.Location = New System.Drawing.Point(780, 0)
        Me.GPortClose.Name = "GPortClose"
        Me.GPortClose.ReadOnly = True
        Me.GPortClose.RowHeadersVisible = False
        Me.GPortClose.Size = New System.Drawing.Size(166, 540)
        Me.GPortClose.TabIndex = 6
        '
        'Gport
        '
        Me.Gport.HeaderText = "Client Address"
        Me.Gport.Name = "Gport"
        Me.Gport.ReadOnly = True
        Me.Gport.Width = 200
        '
        'Timer2
        '
        Me.Timer2.Enabled = True
        Me.Timer2.Interval = 1000
        '
        'Timer3
        '
        Me.Timer3.Enabled = True
        Me.Timer3.Interval = 500
        '
        'BStopPort
        '
        Me.BStopPort.Enabled = False
        Me.BStopPort.Location = New System.Drawing.Point(357, 7)
        Me.BStopPort.Name = "BStopPort"
        Me.BStopPort.Size = New System.Drawing.Size(153, 23)
        Me.BStopPort.TabIndex = 7
        Me.BStopPort.Text = "Stop Port"
        Me.BStopPort.UseVisualStyleBackColor = True
        '
        'ServerTCPSocketNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(946, 540)
        Me.Controls.Add(Me.BStopPort)
        Me.Controls.Add(Me.GPortClose)
        Me.Controls.Add(Me.TData)
        Me.Controls.Add(Me.BOpenPort)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TStart)
        Me.Controls.Add(Me.GData)
        Me.Name = "ServerTCPSocketNew"
        Me.Text = "Server TCP Socket"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.GData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GPortClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GData As System.Windows.Forms.DataGridView
    Friend WithEvents TStart As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BOpenPort As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents TData As System.Windows.Forms.TextBox
    Friend WithEvents GPortClose As System.Windows.Forms.DataGridView
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents Timer3 As Timer
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Gport As DataGridViewTextBoxColumn
    Friend WithEvents BStopPort As Button
End Class
