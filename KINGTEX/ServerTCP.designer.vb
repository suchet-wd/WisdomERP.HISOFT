<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ServerTCP
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
        Me.GData = New System.Windows.Forms.DataGridView()
        Me.TStart = New System.Windows.Forms.TextBox()
        Me.TEnd = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BOpenPort = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TData = New System.Windows.Forms.TextBox()
        Me.TPortClose = New System.Windows.Forms.TextBox()
        Me.GPortClose = New System.Windows.Forms.DataGridView()
        Me.Gport = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
        Me.GData.Location = New System.Drawing.Point(3, 82)
        Me.GData.Margin = New System.Windows.Forms.Padding(4)
        Me.GData.Name = "GData"
        Me.GData.RowHeadersVisible = False
        Me.GData.Size = New System.Drawing.Size(962, 567)
        Me.GData.TabIndex = 0
        '
        'TStart
        '
        Me.TStart.Location = New System.Drawing.Point(143, 11)
        Me.TStart.Margin = New System.Windows.Forms.Padding(4)
        Me.TStart.Name = "TStart"
        Me.TStart.Size = New System.Drawing.Size(113, 22)
        Me.TStart.TabIndex = 1
        Me.TStart.Text = "8083"
        '
        'TEnd
        '
        Me.TEnd.Location = New System.Drawing.Point(359, 11)
        Me.TEnd.Margin = New System.Windows.Forms.Padding(4)
        Me.TEnd.Name = "TEnd"
        Me.TEnd.Size = New System.Drawing.Size(125, 22)
        Me.TEnd.TabIndex = 1
        Me.TEnd.Text = "8083"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(67, 15)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 17)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Port Start"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(284, 15)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Port End"
        '
        'BOpenPort
        '
        Me.BOpenPort.Location = New System.Drawing.Point(507, 7)
        Me.BOpenPort.Margin = New System.Windows.Forms.Padding(4)
        Me.BOpenPort.Name = "BOpenPort"
        Me.BOpenPort.Size = New System.Drawing.Size(100, 28)
        Me.BOpenPort.TabIndex = 3
        Me.BOpenPort.Text = "OpenPort"
        Me.BOpenPort.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 8000
        '
        'TData
        '
        Me.TData.Location = New System.Drawing.Point(3, 52)
        Me.TData.Margin = New System.Windows.Forms.Padding(4)
        Me.TData.Name = "TData"
        Me.TData.Size = New System.Drawing.Size(1065, 22)
        Me.TData.TabIndex = 4
        '
        'TPortClose
        '
        Me.TPortClose.Location = New System.Drawing.Point(615, 11)
        Me.TPortClose.Margin = New System.Windows.Forms.Padding(4)
        Me.TPortClose.Name = "TPortClose"
        Me.TPortClose.ReadOnly = True
        Me.TPortClose.Size = New System.Drawing.Size(132, 22)
        Me.TPortClose.TabIndex = 5
        '
        'GPortClose
        '
        Me.GPortClose.AllowUserToAddRows = False
        Me.GPortClose.AllowUserToDeleteRows = False
        Me.GPortClose.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GPortClose.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Gport})
        Me.GPortClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.GPortClose.Location = New System.Drawing.Point(973, 0)
        Me.GPortClose.Margin = New System.Windows.Forms.Padding(4)
        Me.GPortClose.Name = "GPortClose"
        Me.GPortClose.ReadOnly = True
        Me.GPortClose.RowHeadersVisible = False
        Me.GPortClose.Size = New System.Drawing.Size(99, 665)
        Me.GPortClose.TabIndex = 6
        '
        'Gport
        '
        Me.Gport.HeaderText = "Port No"
        Me.Gport.Name = "Gport"
        Me.Gport.ReadOnly = True
        '
        'Timer2
        '
        Me.Timer2.Enabled = True
        Me.Timer2.Interval = 1000
        '
        'Column4
        '
        Me.Column4.HeaderText = "time"
        Me.Column4.Name = "Column4"
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
        'ServerTCP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1072, 665)
        Me.Controls.Add(Me.GPortClose)
        Me.Controls.Add(Me.TPortClose)
        Me.Controls.Add(Me.TData)
        Me.Controls.Add(Me.BOpenPort)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TEnd)
        Me.Controls.Add(Me.TStart)
        Me.Controls.Add(Me.GData)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "ServerTCP"
        Me.Text = "Server TCP"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.GData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GPortClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GData As System.Windows.Forms.DataGridView
    Friend WithEvents TStart As System.Windows.Forms.TextBox
    Friend WithEvents TEnd As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents BOpenPort As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents TData As System.Windows.Forms.TextBox
    Friend WithEvents TPortClose As System.Windows.Forms.TextBox
    Friend WithEvents GPortClose As System.Windows.Forms.DataGridView
    Friend WithEvents Gport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
End Class
