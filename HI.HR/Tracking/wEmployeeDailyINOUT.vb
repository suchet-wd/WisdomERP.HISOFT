Imports System.Windows.Forms
Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports Microsoft.VisualBasic

Public Class wEmployeeDailyINOUT
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub



#Region "Property"
    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(value As String)
            _CallMethodParm = value
        End Set
    End Property

    Private _ActualDate As String = ""
    ReadOnly Property ActualDate As String
        Get
            Return _ActualDate
        End Get
    End Property

    Private _ActualNextDate As String = ""
    ReadOnly Property ActualNextDate As String
        Get
            Return _ActualNextDate
        End Get
    End Property

#End Region

#Region "Procedure"
    Private Sub LoadData()
        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Dim Qry As String
        Dim dt As DataTable
        Dim _Day As String = Strings.Left(Date1.Text, 2)
        Dim _Month As String = Date1.Text.Substring(3, 2)
        Dim _Year As String = Strings.Right(Date1.Text, 4)
        Dim _DayInMonth As String = Date.DaysInMonth(_Year, _Month)

        Qry = "select count(Z.FNHSysEmpID) AS FNAmtFirst"
        If ST.Lang.Language = ST.Lang.eLang.TH Then
            Qry &= vbCrLf & ",FTUnitSectNameTH AS FTSectName"
        Else
            Qry &= vbCrLf & ",FTUnitSectNameEN AS FTSectName"
        End If
        Qry &= vbCrLf & ",count(Z.FINCOM) AS FNFirstWeektTranIN"
        Qry &= vbCrLf & ",count(Z.FOUTThis) AS FNFirstWeekTranOUT"
        Qry &= vbCrLf & ",count(Z.Empin) AS FNFirstWeekStartWorkIN"
        Qry &= vbCrLf & ",count(z.Empout) AS FNFirstWeekEndWorkOUT"
        Qry &= vbCrLf & ",((count(Z.FNHSysEmpID)+count(Z.FINCOM)+count(Z.Empin))-count(Z.FOUTThis))-count(z.Empout) AS FNBalanceFirstWeek"
        Qry &= vbCrLf & ",count(Z.LINCOM) as FNLastWeekTranIN"
        Qry &= vbCrLf & ",count(Z.LOUTThis) AS FNLastWeekTranOUT"
        Qry &= vbCrLf & ",count(Z.LEmpIN) AS FNLastWeekStartWorkIN"
        Qry &= vbCrLf & ",count(Z.LEmpOUT) AS FNLastWeekEndWorkOUT"
        Qry &= vbCrLf & ",((((count(Z.FNHSysEmpID)+count(Z.FINCOM)+count(Z.Empin))-count(Z.FOUTThis))-count(z.Empout)+count(Z.LINCOM)+count(Z.LEmpIN))-count(Z.LOUTThis))-count(Z.LEmpOUT) AS FNBalanceLastWeek"
        Qry &= vbCrLf & ",count(Z.FINCOM)+count(Z.Empin)+count(Z.LINCOM)+count(Z.LEmpIN) AS FNAmtOfMonthIN"
        Qry &= vbCrLf & ",count(Z.FOUTThis)+count(z.Empout)+count(Z.LOUTThis)+count(Z.LEmpOUT) AS FNAmtOfMonthOUT"
        Qry &= vbCrLf & ",convert(numeric(3,2),((count(Z.Empin)+count(Z.LEmpIN))*count(Z.FNHSysEmpID))/100) AS FNPercenIN"
        Qry &= vbCrLf & ",convert(numeric(3,2),((count(Z.Empout)+count(Z.LEmpOUT))*count(Z.FNHSysEmpID))/100) AS FNPercenOUT"
        Qry &= vbCrLf & " from ("
        Qry &= vbCrLf & "select U.FTUnitSectNameTH,A.FNHSysEmpID "
        Qry &= vbCrLf & ",FTrIN.FNHSysUnitSectIdTo AS FINCOM"
        Qry &= vbCrLf & ",FTrOUT.FNHSysUnitSectIdTo AS FOUTThis"
        Qry &= vbCrLf & ",FEmpIN.FNHSysEmpID AS Empin"
        Qry &= vbCrLf & ",FEmpOUT.FNHSysEmpID as Empout"
        Qry &= vbCrLf & ",LTrIN.FNHSysUnitSectIdTo AS LINCOM"
        Qry &= vbCrLf & ",LTrOUT.FNHSysUnitSectIdTo AS LOUTThis"
        Qry &= vbCrLf & ",LEmpIN.FNHSysEmpID AS LEmpIN"
        Qry &= vbCrLf & ",LEmpOUT.FNHSysEmpID AS LEmpOUT"
        Qry &= vbCrLf & "from"
        Qry &= vbCrLf & "(select U.FNHSysUnitSectId,U.FTUnitSectNameTH,U.FTUnitSectNameEN from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMUnitSect AS U WiTh(NOLOCK) "
        Qry &= vbCrLf & "where u.FTStateActive='1' "
        If Me.FNHSysUnitSectId.Text <> "" Then
            Qry &= vbCrLf & "and U.FNHSysUnitSectId>=" & Me.FNHSysUnitSectId.Properties.Tag & " "
        End If
        If Me.FNHSysUnitSectIdTo.Text <> "" Then
            Qry &= vbCrLf & "and U.FNHSysUnitSectId<=" & Me.FNHSysUnitSectIdTo.Properties.Tag & ""
        End If

        Qry &= vbCrLf & ") AS U Inner Join"
        Qry &= vbCrLf & "(select E.FNHSysEmpID,E.FNHSysUnitSectId,E.FNHSysEmpTypeId from"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As E With(NOLOCK) INNER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..THRMEmpType AS T WITH(NOLOCK) ON E.FNHSysEmpTypeId = T.FNHSysEmpTypeId "
        'If Me.FNHSysEmpTypeId.Text <> "" Then
        '    Qry &= vbCrLf & " WHERE T.FTEmpTypeCode >= '" & Me.FNHSysEmpTypeId.Text & "'"
        'End If
        'If Me.FNHSysEmpTypeIdTo.Text <> "" Then
        '    Qry &= vbCrLf & " And T.FTEmpTypeCode <= '" & Me.FNHSysEmpTypeIdTo.Text & "'"
        'End If

        Dim _Criteria As String = ""
        '***Empployee Type***
        Dim tText As String = ""

        Select Case Me.FNEmpTypeCon.SelectedIndex
                Case 1

                    If Me.FNHSysEmpTypeId.Text <> "" Then

                    Qry &= vbCrLf & " WHERE T.FTEmpTypeCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
                End If

                    If Me.FNHSysEmpTypeIdTo.Text <> "" Then

                    Qry &= vbCrLf & " AND T.FTEmpTypeCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeIdTo.Text) & "' "
                End If

                Case 2
                    tText = ""

                    For Each oRow As DataRow In Me.DbDtEmployeeType.Rows
                        tText &= oRow("FTCode") & "|"
                    Next

                    If tText.Trim <> "" Then
                        tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                    Qry &= vbCrLf & "  WHERE T.FTEmpTypeCode IN ('" & tText.Replace("|", "','") & "') "
                End If

                Case Else
            End Select


        Qry &= vbCrLf & ") As T On U.FNHSysUnitSectId = T.FNHSysUnitSectId"
        Qry &= vbCrLf & "INNER JOIN"
        Qry &= vbCrLf & "(Select E.FNHSysEmpID, E.FNHSysUnitSectId from"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As E With(NOLOCK)"
        Qry &= vbCrLf & "where E.FDDateEnd ='' and E.FDDateStart<='" & _Year & "/" & _Month & "/" & _Day & "' "
        Qry &= vbCrLf & "OR E.FDDateEnd >= '" & _Year & "/" & _Month & "/01' AND E.FDDateEnd <= '" & _Year & "/" & _Month & "/" & _DayInMonth & "'"
        Qry &= vbCrLf & ") As Emp On T.FNHSysEmpID = Emp.FNHSysEmpID "
        Qry &= vbCrLf & "LEFT OUtER JOIN"
        Qry &= vbCrLf & "(select E.FNHSysEmpID, E.FNHSysUnitSectId from"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee As E With(NOLOCK)"
        Qry &= vbCrLf & "where E.FDDateEnd ='' and E.FDDateStart<'" & _Year & "/" & _Month & "/01' OR E.FDDateEnd >= '" & _Year & "/" & _Month & "/01' AND E.FDDateEnd <= '" & _Year & "/" & _Month & "/" & _DayInMonth & "'"
        Qry &= vbCrLf & ") As A On Emp.FNHSysEmpID=A.FNHSysEmpID "
        Qry &= vbCrLf & "LEFT OUtER JOIN"
        Qry &= vbCrLf & "(Select C.FNHSysUnitSectIdTo,C.FNHSysEmpID from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTEmployeeMasterChange As C With(NOLOCK) "
        Qry &= vbCrLf & "where C.FTEffectiveDate>='" & _Year & "/" & _Month & "/01' and C.FTEffectiveDate<='" & _Year & "/" & _Month & "/15'"
        Qry &= vbCrLf & ") FTrIN ON U.FNHSysUnitSectId=FTrIN.FNHSysUnitSectIdTo and FTrIN.FNHSysEmpID=A.FNHSysEmpID"
        Qry &= vbCrLf & "LEFT OUtER JOIN"
        Qry &= vbCrLf & "(select C.FNHSysUnitSectIdTo,C.FNHSysEmpID from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTEmployeeMasterChange AS C WItH(NOLOCK) "
        Qry &= vbCrLf & "where C.FTEffectiveDate>='" & _Year & "/" & _Month & "/01' and C.FTEffectiveDate<='" & _Year & "/" & _Month & "/15'"
        Qry &= vbCrLf & ") AS FTrOUT ON U.FNHSysUnitSectId<>FTrOUT.FNHSysUnitSectIdTo and FTrOUT.FNHSysEmpID=A.FNHSysEmpID"
        Qry &= vbCrLf & "LEFT OUTER JOIN"
        Qry &= vbCrLf & "(select E.FNHSysEmpID from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee AS E WItH(NOLOCK) where E.FDDateStart>='" & _Year & "/" & _Month & "/01' and E.FDDateStart<='" & _Year & "/" & _Month & "/15') AS FEmpIN ON Emp.FNHSysEmpID=FEmpIN.FNHSysEmpID"
        Qry &= vbCrLf & "LEFT OUTER jOIN"
        Qry &= vbCrLf & "(select E.FNHSysEmpID from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee AS E WItH(NOLOCK) where E.FDDateEnd>='" & _Year & "/" & _Month & "/01' and E.FDDateEnd<='" & _Year & "/" & _Month & "/15') AS FEmpOUT ON A.FNHSysEmpID=FEmpOUT.FNHSysEmpID"
        Qry &= vbCrLf & "LEFT OUtER JOIN"
        Qry &= vbCrLf & "(select C.FNHSysUnitSectIdTo,C.FNHSysEmpID from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTEmployeeMasterChange AS C WItH(NOLOCK) "
        Qry &= vbCrLf & "where C.FTEffectiveDate>='" & _Year & "/" & _Month & "/16' and C.FTEffectiveDate<='" & _Year & "/" & _Month & "/" & _DayInMonth & "'"
        Qry &= vbCrLf & ") LTrIN ON U.FNHSysUnitSectId=LTrIN.FNHSysUnitSectIdTo and LTrIN.FNHSysEmpID=A.FNHSysEmpID"
        Qry &= vbCrLf & "LEFT OUtER JOIN"
        Qry &= vbCrLf & "(select C.FNHSysUnitSectIdTo,C.FNHSysEmpID from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTEmployeeMasterChange AS C WItH(NOLOCK) "
        Qry &= vbCrLf & "where C.FTEffectiveDate>='" & _Year & "/" & _Month & "/16' and C.FTEffectiveDate<='" & _Year & "/" & _Month & "/" & _DayInMonth & "'"
        Qry &= vbCrLf & ") AS LTrOUT ON U.FNHSysUnitSectId<>LTrOUT.FNHSysUnitSectIdTo and LTrOUT.FNHSysEmpID=A.FNHSysEmpID"
        Qry &= vbCrLf & "LEFT OUTER JOIN"
        Qry &= vbCrLf & "(select E.FNHSysEmpID from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee AS E WItH(NOLOCK) where E.FDDateStart>='" & _Year & "/" & _Month & "/16' and E.FDDateStart<='" & _Year & "/" & _Month & "/" & _DayInMonth & "') AS LEmpIN ON Emp.FNHSysEmpID=LEmpIN.FNHSysEmpID"
        Qry &= vbCrLf & "LEFT OUTER jOIN"
        Qry &= vbCrLf & "(select E.FNHSysEmpID from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee AS E WItH(NOLOCK) where E.FDDateEnd>='" & _Year & "/" & _Month & "/16' and E.FDDateEnd<='" & _Year & "/" & _Month & "/" & _DayInMonth & "') AS LEmpOUT ON A.FNHSysEmpID=LEmpOUT.FNHSysEmpID"
        Qry &= vbCrLf & ") AS Z "
        If ST.Lang.Language = ST.Lang.eLang.TH Then
            Qry &= vbCrLf & "group by FTUnitSectNameTH"
        Else
            Qry &= vbCrLf & "group by FTUnitSectNameEN"
        End If

        dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MASTER)
        ogcdetail.DataSource = dt

        Try
            ogvdetail.ExpandAllGroups()
        Catch ex As Exception
        End Try
        _Spls.Close()
        PerCentINOUT()
    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.Date1.Text <> "" Then
            _Pass = True
        End If

        Return _Pass
    End Function

    Sub PerCentINOUT()


        '   Call ShowLeaveInfo(ogvdetail.GetRowCellValue(k, "FNHSysEmpID"))
        ' For Each R As DataRow In CType(ogvdetail.DataSource, DataTable).Rows
        For R As Integer = 0 To CType(ogcdetail.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim EmpAmt As Integer = ogvdetail.GetRowCellValue(R, "FNAmtFirst")
            If EmpAmt = 0 Then
                EmpAmt = 1
            End If
            Dim PerIn As Double = (ogvdetail.GetRowCellValue(R, "FNAmtOfMonthIN") / EmpAmt) * 100
            Dim PerOut As Double = (ogvdetail.GetRowCellValue(R, "FNAmtOfMonthOUT") / EmpAmt) * 100

            ogvdetail.SetRowCellValue(R, "FNPercenIN", PerIn)
            ogvdetail.SetRowCellValue(R, "FNPercenOUT", PerOut)

        Next



    End Sub
#End Region

#Region "General"
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Call InitGrid()
        'Me.FNYear.Text = DateTime.Now.Year

        FNAmtFirst.SummaryItem.SummaryType = SummaryItemType.Sum
        FNBalanceFirstWeek.SummaryItem.SummaryType = SummaryItemType.Sum
        FNFirstWeekEndWorkOUT.SummaryItem.SummaryType = SummaryItemType.Sum
        FNFirstWeekStartWorkIN.SummaryItem.SummaryType = SummaryItemType.Sum
        FNFirstWeekTranOUT.SummaryItem.SummaryType = SummaryItemType.Sum
        FNFirstWeektTranIN.SummaryItem.SummaryType = SummaryItemType.Sum
        FNBalanceLastWeek.SummaryItem.SummaryType = SummaryItemType.Sum
        FNLastWeekEndWorkOUT.SummaryItem.SummaryType = SummaryItemType.Sum
        FNLastWeekStartWorkIN.SummaryItem.SummaryType = SummaryItemType.Sum
        FNLastWeekTranIN.SummaryItem.SummaryType = SummaryItemType.Sum
        FNLastWeekTranOUT.SummaryItem.SummaryType = SummaryItemType.Sum
        'FNPercenIN.SummaryItem.SummaryType = SummaryItemType.Sum
        'FNPercenOUT.SummaryItem.SummaryType = SummaryItemType.Sum
        FNAmtOfMonthIN.SummaryItem.SummaryType = SummaryItemType.Sum
        FNAmtOfMonthOUT.SummaryItem.SummaryType = SummaryItemType.Sum
    End Sub
    Private Sub ocmload_Click(sender As System.Object, e As System.EventArgs) Handles ocmload.Click
        If VerifyData() Then
            Call LoadData()
        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        Date1.Text = Nothing
        ogcdetail.DataSource = Nothing
    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub



#End Region

#Region "EmployeeType"

    Private m_DbDtEmployeeType As New DataTable
    ReadOnly Property DbDtEmployeeType As DataTable
        Get
            Return m_DbDtEmployeeType
        End Get
    End Property



    Property CommandTextDepartment As Object

    Private Sub FNEmpTypeCon_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles FNEmpTypeCon.SelectedIndexChanged

        FNHSysEmpTypeId.Properties.ReadOnly = (FNEmpTypeCon.SelectedIndex = 0)
        FNHSysEmpTypeIdTo.Properties.ReadOnly = Not (FNEmpTypeCon.SelectedIndex = 1)

        FNHSysEmpTypeId.Properties.Buttons(0).Enabled = Not (FNHSysEmpTypeId.Properties.ReadOnly)
        FNHSysEmpTypeIdTo.Properties.Buttons(0).Enabled = Not (FNHSysEmpTypeIdTo.Properties.ReadOnly)

        FNHSysEmpTypeId.Text = ""
        FNHSysEmpTypeIdTo.Text = ""

        m_DbDtEmployeeType.Rows.Clear()
        m_DbDtEmployeeType.AcceptChanges()

        Dim tSql As String = ""
        Try
            Dim oDbDt As DataTable

            tSql = "SELECT FTCode  FROM (SELECT  TOP 0  '' AS FTCode  "
            tSql &= "  FROM THRMEmpType  WITH(NOLOCK) )AS M "
            tSql &= " ORDER BY FTCode "

            oDbDt = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_MASTER)

            m_DbDtEmployeeType = oDbDt.Clone
            ogdemptype.DataSource = m_DbDtEmployeeType

        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        Finally

        End Try


    End Sub

    Private Sub FNHSysEmpType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FNHSysEmpTypeId.KeyDown
        Try
            Select Case Me.FNEmpTypeCon.SelectedIndex
                Case 2
                    Select Case e.KeyCode
                        Case Keys.Enter

                            If FNHSysEmpTypeId.Text = "" Then Exit Sub
                            If FNHSysEmpTypeId.Properties.Tag.ToString = "" Then Exit Sub

                            Dim NewRow As DataRow = m_DbDtEmployeeType.NewRow
                            NewRow("FTCode") = FNHSysEmpTypeId.Text
                            m_DbDtEmployeeType.Rows.Add(NewRow)
                            m_DbDtEmployeeType.AcceptChanges()

                    End Select
                Case Else
            End Select

        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try

    End Sub

    Private Sub ogvemptype_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ogvemptype.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    Call ogvemptype_DoubleClick(ogvemptype, New System.EventArgs)
            End Select
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub

    Private Sub ogvemptype_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ogvemptype.DoubleClick
        Try
            With ogvemptype
                If .FocusedRowHandle < 0 Then Exit Sub
                .DeleteRow(.FocusedRowHandle)
                m_DbDtEmployeeType.AcceptChanges()
            End With
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub

#End Region
End Class
