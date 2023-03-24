Imports System.Management


Friend Class Start
    Public Declare Function GetVolumeSerialNumber Lib "kernel32" Alias "GetVolumeInformationA" (ByVal lpRootPathName As String, ByVal lpVolumeNameBuffer As Long, ByVal nVolumeNameSize As Long, ByVal lpVolumeSerialNumber As Long, ByVal lpMaximumComponentLength As Long, ByVal lpFileSystemFlags As Long, ByVal lpFileSystemNameBuffer As Long, ByVal nFileSystemNameSize As Long) As Long
    Private Sub New()
    End Sub

    <STAThread()> _
    Shared Sub Main()

        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
        System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"

        DevExpress.UserSkins.BonusSkins.Register()
        DevExpress.Skins.SkinManager.EnableFormSkins()
        Application.EnableVisualStyles()
        DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("McSkin")

        Try
            Dim _Theme As String = HI.UL.AppRegistry.ReadRegistry(HI.UL.AppRegistry.KeyName.Theme)

            If _Theme <> "" Then
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(_Theme)
            End If
        Catch ex As Exception
        End Try

        Application.SetCompatibleTextRenderingDefault(False)

        '----------- Read Connecttion String From File XML
        HI.Conn.DB.GetXmlConnectionString()
        '----------- Read Connecttion String From File XML

        'Dim _Qry As String = ""
        'Dim _dtPur As DataTable
        '_Qry = " SELECT        FTPurchaseNo, FNPOGrandAmt, FTPOGrandAmtTH, FTPOGrandAmtEN"
        '_Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase WITH (NOLOCK)"
        ''  _Qry &= vbCrLf & " WHERE FTPurchaseNo='NIPLI1409010002'"
        '_dtPur = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

        'For Each Rx As DataRow In _dtPur.Rows
        '    _Qry = "  UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
        '    _Qry &= vbCrLf & " SET FTPOGrandAmtEN='" & HI.UL.ULF.rpQuoted(HI.UL.ULF.Convert_Bath_EN(Val(Rx!FNPOGrandAmt))) & "'"
        '    _Qry &= vbCrLf & " ,FTPOGrandAmtTH='" & HI.UL.ULF.rpQuoted(HI.UL.ULF.Convert_Bath_TH(Val(Rx!FNPOGrandAmt))) & "'"
        '    _Qry &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Rx!FTPurchaseNo.ToString) & "'"

        '    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PUR)
        'Next

        If HI.Conn.SQLConn.GetField("Select Convert(varchar(10),GetDate(),111)", HI.Conn.DB.DataBaseName.DB_SYSTEM) = "" Then
            MsgBox("Can't Connect Database Please Contact Admin...!!!")
            Application.Exit()
        Else
            HI.ST.UserInfo.UserName = ""
            HI.ST.UserInfo.UserLogInComputer = System.Environment.MachineName
            HI.ST.UserInfo.UserLogInComputerIP = "000.000.000.000"

            Dim _Date As DateTime = HI.UL.ULDate.GetOnServer(HI.Conn.DB.DataBaseName.DB_SYSTEM)
            HI.ST.UserInfo.LogINDate = Format(_Date, "dd/MM/yyyy")
            HI.ST.UserInfo.LogINTime = Format(_Date, "HH:mm:ss")

            HI.ST.SysInfo.AppName = HI.Conn.DB._AppName
            HI.ST.SysInfo.CmpID = 0
            HI.ST.SysInfo.CmpCode = ""
            HI.ST.SysInfo.CmpDefualtCode = ""

            Call PrepareLanguage()
            Call HI.TL.CboList.PrepareList()
            Call LoadSystemConfig()
            Call HI.TL.HandlerControl.CreateManuStripGrid()

            Dim _lang As String = HI.UL.AppRegistry.ReadRegistry(HI.UL.AppRegistry.KeyName.Language)

            If _lang = "" Then
                HI.ST.Lang.Language = HI.ST.Lang.eLang.TH
                HI.UL.AppRegistry.WriteRegistry(HI.UL.AppRegistry.KeyName.Language, HI.ST.Lang.Language)
            Else
                HI.ST.Lang.Language = CType([Enum].Parse(GetType(HI.ST.Lang.eLang), _lang), HI.ST.Lang.eLang)
            End If

            Call UserLogIn()
        End If

    End Sub

    Public Shared Sub PrepareLanguage()
        Dim _Dt As DataTable
        Dim _Strsql As String

        _Strsql = "SELECT TOP 11  FNHSysMessageID, FTMessageTH, FTMessageEN FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_LANG) & "].dbo.HSystemMessasge WITH (NOLOCK) WHERE FNHSysMessageID>=1000000001 AND FNHSysMessageID<=1000000011   "
        _Dt = HI.Conn.SQLConn.GetDataTable(_Strsql, HI.Conn.DB.DataBaseName.DB_SYSTEM)

        For Each Row In _Dt.Rows
            Select Case Long.Parse(Row!FNHSysMessageID.ToString)
                Case 1000000001
                    HI.MG.Msg.SaveData = "|" & Row!FTMessageEN.ToString & "|" & Row!FTMessageTH.ToString
                Case 1000000002
                    HI.MG.Msg.DeleteData = "|" & Row!FTMessageEN.ToString & "|" & Row!FTMessageTH.ToString
                Case 1000000003
                    HI.MG.Msg.SaveDataComplete = "|" & Row!FTMessageEN.ToString & "|" & Row!FTMessageTH.ToString
                Case 1000000004
                    HI.MG.Msg.DeleteDataComplete = "|" & Row!FTMessageEN.ToString & "|" & Row!FTMessageTH.ToString
                Case 1000000005
                    HI.MG.Msg.SaveDataNotComplete = "|" & Row!FTMessageEN.ToString & "|" & Row!FTMessageTH.ToString
                Case 1000000006
                    HI.MG.Msg.DeleteDataNotComplete = "|" & Row!FTMessageEN.ToString & "|" & Row!FTMessageTH.ToString
                Case 1000000007
                    HI.MG.Msg.MSelect = "|" & Row!FTMessageEN.ToString & "|" & Row!FTMessageTH.ToString
                Case 1000000008
                    HI.MG.Msg.Input = "|" & Row!FTMessageEN.ToString & "|" & Row!FTMessageTH.ToString
                Case 1000000009
                    HI.MG.Msg.PleaseWait = "|" & Row!FTMessageEN.ToString & "|" & Row!FTMessageTH.ToString
                Case 1000000010
                    HI.MG.Msg.ExiHSystem = "|" & Row!FTMessageEN.ToString & "|" & Row!FTMessageTH.ToString
                Case 1000000011
                    HI.MG.Msg.GenDocument = "|" & Row!FTMessageEN.ToString & "|" & Row!FTMessageTH.ToString
            End Select
        Next

        _Dt.Dispose()

    End Sub
 

    Public Shared Sub LoadSystemConfig()

        Dim _Str As String = "SELECT * FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEConfig WITH(NOLOCK)"
        Dim _dt As DataTable
        _dt = HI.Conn.SQLConn.GetDataTable(_Str, HI.Conn.DB.DataBaseName.DB_SECURITY)

        For Each R As DataRow In _dt.Rows
            HI.ST.SysInfo.CmpDefualtCode = R!FTCmpCode.ToString
            HI.ST.Config.StateNotShowEmpLeave = (R!FTStateNotShowEmpLeave.ToString = "1")
            Exit For
        Next

    End Sub

    Public Shared Sub UserLogOff(FormMain As Object)
        FormMain.Close()
        Call UserLogIn()
    End Sub

    Public Shared Sub UserLogIn()

        If Initialize() = False Then
            Application.Exit()
        Else


            Dim _WformPo As New HI.QA.wQA


            ' Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
            HI.ST.SysInfo.MenuName = "mnuQA"
            Dim _WShow As New HI.TLF.wShowData(_WformPo, "")
            ' HI.ST.SysInfo.MenuName = _TmpMenu

            With _WShow
                .WindowState = System.Windows.Forms.FormWindowState.Maximized
                .StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                .ShowDialog()
            End With

            'Application.Run(_WShow)
        End If

    End Sub

    Public Shared Function Initialize() As Boolean

        Try

            Return HI.ST.UserInfo.Login()

        Catch ex As Exception

            HI.MG.ShowMsg.mProcessError(ex.Message.GetHashCode(), ex.Message, "System", System.Windows.Forms.MessageBoxIcon.Error)

            Return False
        End Try

    End Function


End Class
