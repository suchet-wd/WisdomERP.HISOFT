Imports System.Drawing
Imports System.Threading
Imports System.Windows.Forms

Public Class EfkaDisplayCycleTimeInfo
    Private _StateFTStateDaily As Boolean = False
    Private _TimeSwitchtoSpeed As Integer = 0
    Private _TimeSwitchToHeader As Integer = 1


    Private _TotalEmpFromMasterLine1 As Integer = 0
    Private _TotalEmpHRmorningLine1 As Integer = 0
    Private StateLoad As Boolean = False

    Private _StateWindowsUser As Boolean = False
    Property StateWindowsUser As Boolean
        Get
            Return _StateWindowsUser
        End Get
        Set(value As Boolean)
            _StateWindowsUser = value
        End Set
    End Property

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()


        ' Add any initialization after the InitializeComponent() call.

        StateLoad = False
    End Sub


    Private Sub LCDDisplayIncentive_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LCDDisplayIncentive_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Escape
                Me.Close()
        End Select
    End Sub

    Private Sub EfkaDisplayDataInfo_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized

    End Sub

    Private Sub EfkaDisplayDataInfo_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged

        If Me.WindowState <> System.Windows.Forms.FormWindowState.Maximized Then
            Exit Sub
        End If

        If StateLoad = True Then
            Exit Sub
        End If
        Dim ControlTabIndex As Integer = 5000
        Dim ArrayObject(500) As Object
        Dim ObjectIndex As Integer = 0

        StateLoad = True
        Dim strsql As String = ""
        Dim dt As DataTable

        strsql = "  Select  A.FNHSysUnitSectId "
        strsql &= vbCrLf & " ,A.FNMachineSeq "
        strsql &= vbCrLf & " ,A.FTEfkaSerialNo "
        strsql &= vbCrLf & " ,A.FTEfkaIP "
        strsql &= vbCrLf & " ,B.FTUnitSectCode ,'0' AS FTStateAdd "
        strsql &= vbCrLf & " ,ISNULL(TT.FTAssetTypeNameEN,'') AS FTMachine ,A.FNHSysFixedAssetId"
        strsql &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMConfigEfkaMachineSerial As A  WITH(NOLOCK) INNER Join "
        strsql &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect As B  WITH(NOLOCK)   On A.FNHSysUnitSectId = B.FNHSysUnitSectId "
        strsql &= vbCrLf & "       LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset As AST  WITH(NOLOCK)   On A.FNHSysFixedAssetId = AST.FNHSysFixedAssetId "
        strsql &= vbCrLf & "       LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetType AS TT ON AST.FNHSysAssetTyped = TT.FNHSysAssetTyped "

        dt = HI.Conn.SQLConn.GetDataTable(strsql, Conn.DB.DataBaseName.DB_MASTER)

        If dt.Rows.Count > 0 Then
            Dim grp As List(Of String) = (dt.Select("FTUnitSectCode <> ''", "FTUnitSectCode").CopyToDataTable).AsEnumerable() _
                                                    .Select(Function(r) r.Field(Of String)("FTUnitSectCode")) _
                                                    .Distinct() _
                                                    .ToList()

            For Each unitsect As String In grp

                Dim labelunitsect As New DevExpress.XtraEditors.LabelControl()

                With labelunitsect
                    .Appearance.Options.UseBackColor = True
                    .Appearance.Options.UseFont = True
                    .Appearance.Options.UseForeColor = True
                    .Appearance.Options.UseTextOptions = True
                    .Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                    .Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
                    .AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
                    .Font = New Font("Tahoma", 16, FontStyle.Bold)
                    .Dock = System.Windows.Forms.DockStyle.Top
                    .Size = New System.Drawing.Size(1273, 41)
                    .Text = "   Line No.  " & unitsect
                End With

                ArrayObject(ObjectIndex) = labelunitsect
                ObjectIndex = ObjectIndex + 1
                'Dim Panalunitsect As New DevExpress.XtraEditors.PanelControl

                'Dim unitsectxtrascroll As New DevExpress.XtraEditors.XtraScrollableControl()
                'unitsectxtrascroll.Dock = System.Windows.Forms.DockStyle.Fill
                'Panalunitsect.Controls.Add(unitsectxtrascroll)

                Dim ConDataSeq As Integer = 1

                Do While dt.Select("FTUnitSectCode='" & HI.UL.ULF.rpQuoted(unitsect) & "' AND FTStateAdd<>'1'").Length > 0
                    ControlTabIndex = ControlTabIndex + 1
                    Dim Panalunitsect As New EfkaPanal
                    With Panalunitsect
                        .Dock = System.Windows.Forms.DockStyle.Top
                        .TabIndex = ControlTabIndex
                        .Size = New System.Drawing.Size(1273, 260)
                    End With

                    ' xtrascroll.Controls.Add(Panalunitsect)

                    'ArrayObject(ObjectIndex) = Panalunitsect
                    'ObjectIndex = ObjectIndex + 1

                    ConDataSeq = 1

                    dt.BeginInit()

                    For Each R As DataRow In dt.Select("FTUnitSectCode='" & HI.UL.ULF.rpQuoted(unitsect) & "' AND FTStateAdd<>'1'", "FNMachineSeq")

                        Dim DataCri As New EfkaDataCycleTimeInfo(Val(R!FNMachineSeq.ToString), R!FTEfkaSerialNo.ToString, R!FTEfkaIP.ToString, R!FTMachine.ToString, Val(R!FNHSysUnitSectId.ToString), Val(R!FNHSysFixedAssetId.ToString))

                        'unitsectxtrascroll.Controls.Add(DataCri)

                        DataCri.Dock = System.Windows.Forms.DockStyle.Fill

                        ' Panalunitsect.Controls.Add(DataCri)

                        Select Case ConDataSeq
                            Case 1
                                Panalunitsect.pn1.Controls.Add(DataCri)

                            Case 2
                                Panalunitsect.pn2.Controls.Add(DataCri)

                            Case 3
                                Panalunitsect.pn3.Controls.Add(DataCri)

                            Case 4
                                Panalunitsect.pn4.Controls.Add(DataCri)

                            Case 5
                                Panalunitsect.pn5.Controls.Add(DataCri)

                            Case 6
                                Panalunitsect.pn6.Controls.Add(DataCri)

                        End Select

                        DataCri.StateTimchecker = True
                        DataCri.Timchecker.Enabled = True
                        DataCri.LodDataCycleInfo()

                        R!FTStateAdd = "1"

                        If ConDataSeq >= 6 Then
                            Exit For
                        End If

                        ConDataSeq = ConDataSeq + 1

                    Next
                    dt.EndInit()

                    ArrayObject(ObjectIndex) = Panalunitsect
                    ObjectIndex = ObjectIndex + 1

                Loop

                'xtrascroll.Controls.Add(labelunitsect)

            Next

            For I As Integer = ObjectIndex To 0 Step -1
                If ArrayObject(I) Is Nothing Then
                Else
                    xtrascroll.Controls.Add(ArrayObject(I))
                End If

            Next

            ArrayObject = Nothing

        End If

        dt.Dispose()
    End Sub

    Private Sub xtrascroll_KeyDown(sender As Object, e As KeyEventArgs) Handles xtrascroll.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Escape
                Me.Close()
        End Select
    End Sub
End Class