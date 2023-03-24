<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EfkaDataCycleTimeInfo
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.FTSerialNo = New DevExpress.XtraEditors.LabelControl()
        Me.FTIP = New DevExpress.XtraEditors.LabelControl()
        Me.olbtop = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.FTOperation = New DevExpress.XtraEditors.LabelControl()
        Me.FTTimeBet = New DevExpress.XtraEditors.LabelControl()
        Me.Timchecker = New System.Windows.Forms.Timer(Me.components)
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.FTEmpId = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl13 = New DevExpress.XtraEditors.LabelControl()
        Me.FTMachine = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.FTDowntime = New DevExpress.XtraEditors.LabelControl()
        Me.FTStdtime = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Black
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(10, 119)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(141, 23)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Efka Serial No :"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.Black
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Appearance.Options.UseForeColor = True
        Me.LabelControl2.Appearance.Options.UseTextOptions = True
        Me.LabelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl2.Location = New System.Drawing.Point(10, 145)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(141, 23)
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "Efka IP. :"
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl8.Appearance.ForeColor = System.Drawing.Color.Black
        Me.LabelControl8.Appearance.Options.UseFont = True
        Me.LabelControl8.Appearance.Options.UseForeColor = True
        Me.LabelControl8.Appearance.Options.UseTextOptions = True
        Me.LabelControl8.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl8.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl8.Location = New System.Drawing.Point(10, 176)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(141, 23)
        Me.LabelControl8.TabIndex = 7
        Me.LabelControl8.Text = "Operation :"
        '
        'FTSerialNo
        '
        Me.FTSerialNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTSerialNo.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.FTSerialNo.Appearance.ForeColor = System.Drawing.Color.Green
        Me.FTSerialNo.Appearance.Options.UseFont = True
        Me.FTSerialNo.Appearance.Options.UseForeColor = True
        Me.FTSerialNo.Appearance.Options.UseTextOptions = True
        Me.FTSerialNo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FTSerialNo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTSerialNo.Location = New System.Drawing.Point(159, 122)
        Me.FTSerialNo.Name = "FTSerialNo"
        Me.FTSerialNo.Size = New System.Drawing.Size(153, 23)
        Me.FTSerialNo.TabIndex = 9
        Me.FTSerialNo.Text = "HT91"
        '
        'FTIP
        '
        Me.FTIP.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTIP.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.FTIP.Appearance.ForeColor = System.Drawing.Color.Green
        Me.FTIP.Appearance.Options.UseFont = True
        Me.FTIP.Appearance.Options.UseForeColor = True
        Me.FTIP.Appearance.Options.UseTextOptions = True
        Me.FTIP.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FTIP.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTIP.Location = New System.Drawing.Point(159, 147)
        Me.FTIP.Name = "FTIP"
        Me.FTIP.Size = New System.Drawing.Size(153, 23)
        Me.FTIP.TabIndex = 10
        Me.FTIP.Text = "192.168.1.106"
        '
        'olbtop
        '
        Me.olbtop.Appearance.BackColor = System.Drawing.Color.DarkGray
        Me.olbtop.Appearance.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.olbtop.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.olbtop.Appearance.Options.UseBackColor = True
        Me.olbtop.Appearance.Options.UseFont = True
        Me.olbtop.Appearance.Options.UseForeColor = True
        Me.olbtop.Appearance.Options.UseTextOptions = True
        Me.olbtop.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.olbtop.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.olbtop.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.olbtop.Dock = System.Windows.Forms.DockStyle.Top
        Me.olbtop.Location = New System.Drawing.Point(0, 0)
        Me.olbtop.Name = "olbtop"
        Me.olbtop.Size = New System.Drawing.Size(338, 37)
        Me.olbtop.TabIndex = 19
        Me.olbtop.Text = "Machine No. 1"
        '
        'LabelControl10
        '
        Me.LabelControl10.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl10.Appearance.ForeColor = System.Drawing.Color.Black
        Me.LabelControl10.Appearance.Options.UseFont = True
        Me.LabelControl10.Appearance.Options.UseForeColor = True
        Me.LabelControl10.Appearance.Options.UseTextOptions = True
        Me.LabelControl10.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl10.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl10.Location = New System.Drawing.Point(10, 253)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Size = New System.Drawing.Size(141, 23)
        Me.LabelControl10.TabIndex = 20
        Me.LabelControl10.Text = "Cycle Time (s) :"
        '
        'FTOperation
        '
        Me.FTOperation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTOperation.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.FTOperation.Appearance.ForeColor = System.Drawing.Color.Green
        Me.FTOperation.Appearance.Options.UseFont = True
        Me.FTOperation.Appearance.Options.UseForeColor = True
        Me.FTOperation.Appearance.Options.UseTextOptions = True
        Me.FTOperation.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FTOperation.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.FTOperation.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.FTOperation.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOperation.Location = New System.Drawing.Point(159, 180)
        Me.FTOperation.Name = "FTOperation"
        Me.FTOperation.Size = New System.Drawing.Size(153, 43)
        Me.FTOperation.TabIndex = 23
        Me.FTOperation.Text = "2,000,000"
        '
        'FTTimeBet
        '
        Me.FTTimeBet.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTTimeBet.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.FTTimeBet.Appearance.ForeColor = System.Drawing.Color.Green
        Me.FTTimeBet.Appearance.Options.UseFont = True
        Me.FTTimeBet.Appearance.Options.UseForeColor = True
        Me.FTTimeBet.Appearance.Options.UseTextOptions = True
        Me.FTTimeBet.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FTTimeBet.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTTimeBet.Location = New System.Drawing.Point(159, 254)
        Me.FTTimeBet.Name = "FTTimeBet"
        Me.FTTimeBet.Size = New System.Drawing.Size(153, 23)
        Me.FTTimeBet.TabIndex = 24
        Me.FTTimeBet.Text = "2,000,000"
        '
        'Timchecker
        '
        Me.Timchecker.Interval = 60000
        '
        'LabelControl11
        '
        Me.LabelControl11.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl11.Appearance.ForeColor = System.Drawing.Color.Black
        Me.LabelControl11.Appearance.Options.UseFont = True
        Me.LabelControl11.Appearance.Options.UseForeColor = True
        Me.LabelControl11.Appearance.Options.UseTextOptions = True
        Me.LabelControl11.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl11.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl11.Location = New System.Drawing.Point(10, 45)
        Me.LabelControl11.Name = "LabelControl11"
        Me.LabelControl11.Size = New System.Drawing.Size(141, 23)
        Me.LabelControl11.TabIndex = 25
        Me.LabelControl11.Text = "Emp ID :"
        '
        'FTEmpId
        '
        Me.FTEmpId.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTEmpId.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.FTEmpId.Appearance.ForeColor = System.Drawing.Color.Green
        Me.FTEmpId.Appearance.Options.UseFont = True
        Me.FTEmpId.Appearance.Options.UseForeColor = True
        Me.FTEmpId.Appearance.Options.UseTextOptions = True
        Me.FTEmpId.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FTEmpId.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEmpId.Location = New System.Drawing.Point(159, 48)
        Me.FTEmpId.Name = "FTEmpId"
        Me.FTEmpId.Size = New System.Drawing.Size(153, 23)
        Me.FTEmpId.TabIndex = 26
        Me.FTEmpId.Text = "9999999"
        '
        'LabelControl13
        '
        Me.LabelControl13.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl13.Appearance.ForeColor = System.Drawing.Color.Black
        Me.LabelControl13.Appearance.Options.UseFont = True
        Me.LabelControl13.Appearance.Options.UseForeColor = True
        Me.LabelControl13.Appearance.Options.UseTextOptions = True
        Me.LabelControl13.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl13.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl13.Location = New System.Drawing.Point(10, 72)
        Me.LabelControl13.Name = "LabelControl13"
        Me.LabelControl13.Size = New System.Drawing.Size(141, 23)
        Me.LabelControl13.TabIndex = 27
        Me.LabelControl13.Text = "Machine :"
        '
        'FTMachine
        '
        Me.FTMachine.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTMachine.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.FTMachine.Appearance.ForeColor = System.Drawing.Color.Green
        Me.FTMachine.Appearance.Options.UseFont = True
        Me.FTMachine.Appearance.Options.UseForeColor = True
        Me.FTMachine.Appearance.Options.UseTextOptions = True
        Me.FTMachine.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FTMachine.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.FTMachine.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.FTMachine.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTMachine.Location = New System.Drawing.Point(159, 76)
        Me.FTMachine.Name = "FTMachine"
        Me.FTMachine.Size = New System.Drawing.Size(153, 45)
        Me.FTMachine.TabIndex = 28
        '
        'LabelControl12
        '
        Me.LabelControl12.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl12.Appearance.ForeColor = System.Drawing.Color.Black
        Me.LabelControl12.Appearance.Options.UseFont = True
        Me.LabelControl12.Appearance.Options.UseForeColor = True
        Me.LabelControl12.Appearance.Options.UseTextOptions = True
        Me.LabelControl12.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl12.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl12.Location = New System.Drawing.Point(10, 285)
        Me.LabelControl12.Name = "LabelControl12"
        Me.LabelControl12.Size = New System.Drawing.Size(141, 23)
        Me.LabelControl12.TabIndex = 29
        Me.LabelControl12.Text = "Down Time :"
        '
        'FTDowntime
        '
        Me.FTDowntime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTDowntime.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.FTDowntime.Appearance.ForeColor = System.Drawing.Color.Green
        Me.FTDowntime.Appearance.Options.UseFont = True
        Me.FTDowntime.Appearance.Options.UseForeColor = True
        Me.FTDowntime.Appearance.Options.UseTextOptions = True
        Me.FTDowntime.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FTDowntime.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTDowntime.Location = New System.Drawing.Point(159, 287)
        Me.FTDowntime.Name = "FTDowntime"
        Me.FTDowntime.Size = New System.Drawing.Size(153, 23)
        Me.FTDowntime.TabIndex = 30
        Me.FTDowntime.Text = "-"
        '
        'FTStdtime
        '
        Me.FTStdtime.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTStdtime.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.FTStdtime.Appearance.ForeColor = System.Drawing.Color.Green
        Me.FTStdtime.Appearance.Options.UseFont = True
        Me.FTStdtime.Appearance.Options.UseForeColor = True
        Me.FTStdtime.Appearance.Options.UseTextOptions = True
        Me.FTStdtime.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FTStdtime.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStdtime.Location = New System.Drawing.Point(159, 225)
        Me.FTStdtime.Name = "FTStdtime"
        Me.FTStdtime.Size = New System.Drawing.Size(153, 23)
        Me.FTStdtime.TabIndex = 32
        Me.FTStdtime.Text = "2,000,000"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl4.Appearance.ForeColor = System.Drawing.Color.Black
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Appearance.Options.UseForeColor = True
        Me.LabelControl4.Appearance.Options.UseTextOptions = True
        Me.LabelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl4.Location = New System.Drawing.Point(10, 224)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(141, 23)
        Me.LabelControl4.TabIndex = 31
        Me.LabelControl4.Text = "STD Time (s) :"
        '
        'EfkaDataCycleTimeInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.FTStdtime)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.FTDowntime)
        Me.Controls.Add(Me.LabelControl12)
        Me.Controls.Add(Me.FTMachine)
        Me.Controls.Add(Me.LabelControl13)
        Me.Controls.Add(Me.FTEmpId)
        Me.Controls.Add(Me.LabelControl11)
        Me.Controls.Add(Me.FTTimeBet)
        Me.Controls.Add(Me.FTOperation)
        Me.Controls.Add(Me.LabelControl10)
        Me.Controls.Add(Me.olbtop)
        Me.Controls.Add(Me.FTIP)
        Me.Controls.Add(Me.FTSerialNo)
        Me.Controls.Add(Me.LabelControl8)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Name = "EfkaDataCycleTimeInfo"
        Me.Size = New System.Drawing.Size(338, 324)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSerialNo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTIP As DevExpress.XtraEditors.LabelControl
    Friend WithEvents olbtop As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOperation As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTTimeBet As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Timchecker As System.Windows.Forms.Timer
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTEmpId As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl13 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTMachine As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTDowntime As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStdtime As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
End Class
