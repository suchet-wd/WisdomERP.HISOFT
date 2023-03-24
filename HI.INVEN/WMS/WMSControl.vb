Imports System
Imports System.ComponentModel
Imports System.IO
Imports System.IO.Ports
Imports System.Text
Imports System.Windows.Forms

Public Class WMSControl


    Private Sub Form_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try



        Catch ex As Exception

        End Try
    End Sub




    Private Sub AutomationReceiver_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
        System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"

        DevExpress.Skins.SkinManager.EnableFormSkins()
        Application.EnableVisualStyles()
        DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("McSkin")

        Try
            'Dim _Theme As String = HI.UL.AppRegistry.ReadRegistry(HI.UL.AppRegistry.KeyName.Theme)

            'If _Theme <> "" Then
            '    DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(_Theme)
            'End If

        Catch ex As Exception
        End Try



    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()


        InitDataLocation()


    End Sub

    Private Sub InitDataLocation()
        Dim StartLoc As Integer = 5
        Dim cmdstring As String = ""
        Dim ObjectNumber As Integer = 0
        Dim dt As DataTable
        cmdstring = "  Select  WH.FTWHFGCode,  LEFT(WHL.FTWHLocCode,LEN(WHL.FTWHLocCode)-3) As FTWHLocCode"
        cmdstring &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseLocationFG As WHL WITH(NOLOCK) INNER Join"
        cmdstring &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseFG As WH WITH(NOLOCK) On WHL.FNHSysWHFGId = WH.FNHSysWHFGId"
        cmdstring &= vbCrLf & "  Where WH.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & ""
        cmdstring &= vbCrLf & "  And  (WHL.FTStateActive = '1') AND (WH.FTStateActive = '1')"
        cmdstring &= vbCrLf & " And LEN(WHL.FTWHLocCode) > 5"
        cmdstring &= vbCrLf & "  GROUP BY WH.FTWHFGCode, Left(WHL.FTWHLocCode, Len(WHL.FTWHLocCode) - 3) "
        cmdstring &= vbCrLf & "  ORDER BY  WH.FTWHFGCode, Left(WHL.FTWHLocCode, Len(WHL.FTWHLocCode) - 3)  "


        dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)

        For Each R As DataRow In dt.Rows

            ObjectNumber = ObjectNumber + 1


            Dim XCI As New UWMSLocation(R!FTWHFGCode.ToString, R!FTWHLocCode.ToString)
            XCI.Name = "SenserDataNumber" & ObjectNumber.ToString
            XCI.DataInOut = UWMSLocation.StateInOut.StateStop
            XCI.Location = New System.Drawing.Point(StartLoc, 5)
            XCI.olbheadloc.Text = R!FTWHLocCode.ToString() & " (" & R!FTWHFGCode.ToString & ")"
            XCI.opnheader.Text = R!FTWHLocCode.ToString() & " (" & R!FTWHFGCode.ToString & ")"
            XCI.Tag = R!FTWHLocCode.ToString()
            olm.Controls.Add(XCI)

            Dim DSenser As New DataWMS
            DSenser.SateRead = False
            DSenser.ID = "SenserDataNumber" & ObjectNumber.ToString
            DSenser.Location = R!FTWHLocCode.ToString
            DSenser.Data1 = "0"
            DSenser.Data2 = "0"
            DSenser.WMSLocation = XCI


            StartLoc = StartLoc + 175


        Next
        dt.Dispose()

    End Sub


    Private Function GetIP(strHostName As String) As String
        Dim _GetIPv4Address As String = ""
        Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName)
        For Each ipheal As System.Net.IPAddress In iphe.AddressList

            If (ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork) Then
                _GetIPv4Address = ipheal.ToString()
                Exit For
            End If

        Next

        Return _GetIPv4Address
    End Function


End Class
Public Class DataWMS
    Property ID As String
    Property Location As String
    Property SateRead As Boolean
    Property Data1 As String
    Property Data2 As String
    Property WMSLocation As UWMSLocation

End Class
