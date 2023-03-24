Imports DevExpress.XtraScheduler.Native
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Calendar
Imports System.Drawing

Public Class wCfgHRMEmpTypeWeekly

    Private dc As New DatesCollection()
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Me.dc = New DatesCollection
        ' Add any initialization after the InitializeComponent() call.

        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
        System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"

        DateNavigator1.UpdateSelectionWhenNavigating = True
        DateNavigator1.SyncSelectionWithEditValue = False

    End Sub

    Public Class MyDateNavigator
        Inherits DevExpress.XtraScheduler.DateNavigator

        'Protected Overrides Function CreatePainter() As DevExpress.XtraEditors.Drawing.DateEditPainter
        '    Return New MyPainter(Me)
        '    'return base.CreatePainter();
        'End Function

    End Class

    Class MyPainter
        'Inherits DevExpress.XtraEditors.Drawing.DateEditPainter
        'Public Sub New(ByVal nav As DevExpress.XtraScheduler.DateNavigator)
        '    MyBase.New(nav)
        'End Sub
        'Protected Overrides Sub DrawHeader(ByVal info As DevExpress.XtraEditors.Calendar.CalendarObjectInfoArgs)
        '    'base.DrawHeader(info);
        '    With info
        '        .Graphics.DrawString(.CurrentMonth.ToString("MMMM"), .HeaderAppearance.Font, .HeaderAppearance.GetForeBrush(.Cache), .Header, .HeaderAppearance.GetStringFormat(DevExpress.Utils.TextOptions.DefaultOptionsCenteredWithEllipsis))
        '    End With
        'End Sub
    End Class

    Private Sub LoadData()

        ' Dim myDatesCollection As New DevExpress.XtraEditors.Controls.DatesCollection()
        Me.dc.Clear()
        ' DateNavigator1.Selection.Clear()
        Try
            Dim _Qry As String = ""
            '_Qry = "SELECT Convert(Datetime,FDHolidayDate) AS  FDHolidayDate  "
            _Qry = "SELECT CASE WHEN ISDATE(FDHolidayDate) = 1 THEN Convert(Datetime,FDHolidayDate) ELSE NULL END AS  FDHolidayDate  "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmpTypeWeeklySpecial AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE  FNHSysEmpTypeId=" & Integer.Parse(Val(FNHSysEmpTypeId.Properties.Tag.ToString)) & ""
            _Qry &= vbCrLf & "  AND LEFT(FDHolidayDate,4)='" & FNYear.Text & "'"


            _Qry &= vbCrLf & "  AND ( ISNULL(FNHSysCmpId,0)=0 OR ISNULL(FNHSysCmpId,0) = " & Integer.Parse(Val(FNHSysCmpId.Properties.Tag.ToString)) & ")"


            _Qry &= vbCrLf & " Order By FDHolidayDate ASC "


            Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            For Each R As DataRow In dt.Rows

                Try
                    ' Me.dc.Add(CDate(R!FDHolidayDate))
                    ' DateNavigator1.Selection.Add(R!FDHolidayDate)
                    Me.dc.Add(R!FDHolidayDate)
                    'Me.dc.Add(DateTime.Parse(R!FDHolidayDate.ToString))
                Catch ex As Exception
                End Try

            Next

        Catch ex As Exception
        End Try
        DateNavigator1.Selection.Clear()
        DateNavigator1.Selection.AddRange(Me.dc)

        ' DateNavigator1.Selection.Clear()
        ' DateNavigator1.Selection.AddRange(Me.dc)
        DateNavigator1.Refresh()
    End Sub
    Private Sub ocmexit_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub FNYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNYear.SelectedIndexChanged
        Me.DateNavigator1.Selection.Clear()
        Try

            DateNavigator1.Selection.RemoveRange(Me.dc)
        Catch ex As Exception
        End Try



        Try
            Me.DateNavigator1.DateTime = DateTime.Parse("1900/01/01")
            Me.DateNavigator1.Refresh()
        Catch ex As Exception
        End Try

        Try
            Me.DateNavigator1.DateTime = DateTime.Parse(FNYear.Text & "/01/01")
            Me.DateNavigator1.Refresh()
        Catch ex As Exception
        End Try


        Try
            Call LoadData()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNHSysEmpTypeId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysEmpTypeId.EditValueChanged
        Try
            Dim _Qry As String = ""
            _Qry = "SELECT TOP 1  FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WITH(NOLOCK) WHERE FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(FNHSysEmpTypeId.Text) & "'   AND  (FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " OR ISNULL(FNHSysCmpId,0)=0) "
            FNHSysEmpTypeId.Properties.Tag = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0"))

            Call LoadData()
        Catch ex As Exception

        End Try
    End Sub


    Private Sub ocmaddnew_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        If FNHSysCmpId.Text = "" Then Exit Sub
        If Me.FNYear.Text <> "" Then
            If Me.FNHSysEmpTypeId.Text <> "" Then

                Dim _Qry As String = ""
                Dim _StrDate As String = ""
                Try

                    For Each D As Date In Me.dc

                        If _StrDate = "" Then
                            _StrDate = HI.UL.ULDate.ConvertEnDB(D)
                        Else
                            _StrDate = _StrDate & "','" & HI.UL.ULDate.ConvertEnDB(D)
                        End If

                    Next

                    HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
                    HI.Conn.SQLConn.SqlConnectionOpen()
                    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                    _Qry = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmpTypeWeeklySpecial"
                    _Qry &= vbCrLf & "  WHERE  FNHSysEmpTypeId=" & Integer.Parse(Val(FNHSysEmpTypeId.Properties.Tag.ToString)) & ""
                    _Qry &= vbCrLf & "  AND LEFT(FDHolidayDate,4)='" & FNYear.Text & "'"
                    _Qry &= vbCrLf & "  AND NOT(FDHolidayDate IN ('" & _StrDate & "'))"

                    _Qry &= vbCrLf & "  AND ( ISNULL(FNHSysCmpId,0)=0 OR ISNULL(FNHSysCmpId,0) = " & Integer.Parse(Val(FNHSysCmpId.Properties.Tag.ToString)) & ")"


                    HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    For Each D As Date In Me.dc

                        _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmpTypeWeeklySpecial"
                        _Qry &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & "  WHERE  FNHSysEmpTypeId=" & Integer.Parse(Val(FNHSysEmpTypeId.Properties.Tag.ToString)) & ""
                        _Qry &= vbCrLf & "  AND  FDHolidayDate='" & HI.UL.ULDate.ConvertEnDB(D) & "'"
                        _Qry &= vbCrLf & "  AND ( ISNULL(FNHSysCmpId,0)=0 OR ISNULL(FNHSysCmpId,0) = " & Integer.Parse(Val(FNHSysCmpId.Properties.Tag.ToString)) & ")"

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmpTypeWeeklySpecial ("
                            _Qry &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime, FNHSysEmpTypeId, FDHolidayDate,FNHSysCmpId"
                            _Qry &= vbCrLf & " )"
                            _Qry &= vbCrLf & " SELECT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysEmpTypeId.Properties.Tag.ToString)) & ""
                            _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(D) & "'"
                            _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysCmpId.Properties.Tag.ToString)) & ""


                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                                Exit Sub

                            End If
                        End If
                       
                    Next

                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                    Me.LoadData()

                Catch ex As Exception

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                End Try
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysEmpTypeId_lbl.Text)
                FNHSysEmpTypeId.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNYear_lbl.Text)
            FNYear.Focus()
        End If
    End Sub

    'Private Sub DateNavigator1_CustomDrawDayNumberCell(sender As Object, e As CustomDrawDayNumberCellEventArgs) Handles DateNavigator1.CustomDrawDayNumberCell
    '    Try
    '        For Each dt As DateTime In DateNavigator1.Selection
    '            If ((e.Date = dt.Date)) Then
    '                e.Style.ForeColor = Color.Blue
    '                e.Style.Font = New Font("Tahoma", 10, FontStyle.Bold)
    '            End If
    '        Next

    '    Catch ex As Exception
    '    End Try

    'End Sub


    Private Sub DateNavigator1_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles DateNavigator1.MouseClick
        Try

            Dim _StateRemove As Boolean = False
            Dim info As CalendarHitInfo = DateNavigator1.GetHitInfo(e)
            Dim _Date As String = HI.UL.ULDate.ConvertEnDB(info.HitDate)

            Dim _Qry As String = ""
            _Qry = "SELECT   CASE WHEN Convert(varchar(10),GetDate(),111) >='" & _Date & "' THEN   'Y'  ELSE '' END"
            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "") <> "" Then
                'DateNavigator1.Selection.Clear()
                'DateNavigator1.Selection.AddRange(Me.dc)

                Exit Sub
            End If

            If Me.dc.Contains(info.HitDate) Then
                _StateRemove = True
            End If
            'For Each D As Date In Me.dc
            '    If D = info.HitDate Then
            '        _StateRemove = True

            '        Exit For
            '    End If

            'Next

            If Not (_StateRemove) Then
                Me.dc.Add(info.HitDate)
            Else
                Me.dc.Remove(info.HitDate)
            End If

            DateNavigator1.Selection.Clear()
            DateNavigator1.Selection.AddRange(Me.dc)

            DateNavigator1.Refresh()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub wCfgHRMEmpTypeWeekly_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            Me.FNHSysCmpId.Text = "1"
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

            FNYear.SelectedIndex = 1
        Catch ex As Exception
        End Try

    End Sub

    Private Sub ocmexit_Click_1(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub DateNavigator1_CustomDrawDayNumberCell(sender As Object, e As CustomDrawDayNumberCellEventArgs) Handles DateNavigator1.CustomDrawDayNumberCell

        'If (e.Selected) Then
        '    e.Cache.FillRectangle(Brushes.LightPink, e.Bounds)
        '    e.Style.DrawString(e.Cache, e.Date.Day.ToString(), e.Bounds)
        '    e.Style.BackColor = Color.AliceBlue
        '    e.Handled = True
        'End If

        If Me.dc.Contains(e.Date) Then
            Dim SolidBrush As New SolidBrush(Color.GreenYellow)
            'Dim SolidBrush As New SolidBrush(Color.White)
            e.Graphics.FillRectangle(SolidBrush, e.Bounds)
            Dim sf As New StringFormat()
            sf.LineAlignment = StringAlignment.Far
            sf.Alignment = StringAlignment.Far

            e.Graphics.DrawString(e.Date.Day.ToString(), New Font(e.Style.Font.Name, e.Style.Font.Size + 2, FontStyle.Bold), Brushes.Blue, e.Bounds, sf)
            e.Handled = True

        End If

    End Sub

End Class

