Public Class wCalculateTime
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#Region "General"
    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmclose.Click
        Me.Close()
    End Sub

    Private Sub ocmcalculate_Click(sender As System.Object, e As System.EventArgs) Handles ocmcalculate.Click

        If HI.UL.ULDate.CheckDate(Me.FTStartDate.Text) <> "" And HI.UL.ULDate.CheckDate(Me.FTEndDate.Text) <> "" Then


            If HI.HRCAL.Time.CheckClosePeriod(FTEndDate.Text, 0, Integer.Parse(Val(Me.FNHSysEmpTypeId.Properties.Tag.ToString))) = True Then
                HI.MG.ShowMsg.mInfo("ไม่สามารถทำการแก้ไขข้อมูลหลังการปิดงวดได้ กรุณาทำการตรวจสอบ", 14040001, Me.Text)
                Exit Sub
            End If

            Dim _Dt As DataTable
            Dim _Qry As String = ""

            _Qry = " SELECT      '0' AS FTSelect,  M.FNHSysEmpID, M.FTEmpCode"
            _Qry &= vbCrLf & " , P.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"

            _Qry &= vbCrLf & " FROM        THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
            _Qry &= vbCrLf & "   THRMTimeShift AS SH WITH (NOLOCK) ON M.FNHSysShiftID = SH.FNHSysShiftID "
            _Qry &= vbCrLf & "   INNER Join "
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON M.FNHSysDivisonId = DI.FNHSysDivisonId "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON M.FNHSysSectId = ST.FNHSysSectId "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId "


            _Qry &= vbCrLf & " WHERE  M.FTEmpCode <> ''  "
            _Qry &= vbCrLf & " AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  "
            _Qry &= vbCrLf & " AND M.FDDateStart <='" & HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) & "' "
            _Qry &= vbCrLf & " AND (M.FDDateEnd ='' OR M.FDDateEnd >'" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "' )   "

            _Qry = HI.ST.Security.PermissionFilterEmployee(_Qry)

            If Me.FNHSysEmpTypeId.Text <> "" Then
                _Qry &= vbCrLf & " AND ET.FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
            End If

            '------Criteria By Employeee Code
            If Me.FNHSysEmpId.Text <> "" Then
                _Qry &= vbCrLf & " AND M.FTEmpCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
            End If

            If Me.FNHSysEmpIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND M.FTEmpCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
            End If

            '------Criteria By Department
            If Me.FNHSysDeptId.Text <> "" Then
                _Qry &= vbCrLf & " AND  Dept.FTDeptCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
            End If

            If Me.FNHSysDeptIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  Dept.FTDeptCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
            End If

            '------Criteria By Division
            If Me.FNHSysDivisonId.Text <> "" Then
                _Qry &= vbCrLf & " AND  DI.FTDivisonCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
            End If

            If Me.FNHSysDivisonIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  DI.FTDivisonCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
            End If

            '------Criteria By Sect
            If Me.FNHSysSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND  ST.FTSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
            End If

            If Me.FNHSysSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  ST.FTSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
            End If

            '------Criteria Unit Sect
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND   US.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
            End If

            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND   US.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
            End If

            _Qry &= vbCrLf & " ORDER BY  M.FTEmpCode  "

            Dim _Spls As New HI.TL.SplashScreen("Prepre Data For Calculate.. Please Wait ")
            _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            Dim _Rec As Integer = 0
            Dim _TotalRec As Integer = _Dt.Rows.Count

            For Each R As DataRow In _Dt.Rows

                _Rec = _Rec + 1
                _Spls.UpdateInformation("Calculate Employee Code " & R!FTEmpCode.ToString & "  Record  " & _Rec.ToString & " Of " & _TotalRec.ToString & "  (" & Format((_Rec * 100.0) / _TotalRec, "0.00") & " % ) ")

                HI.HRCAL.Calculate.CalculateWorkTime(HI.ST.UserInfo.UserName, R!FNHSysEmpID.ToString, HI.UL.ULDate.ConvertEnDB(FTStartDate.Text), HI.UL.ULDate.ConvertEnDB(FTEndDate.Text))

            Next

            HI.HRCAL.Calculate.DisposeObject()
            _Spls.Close()

            HI.MG.ShowMsg.mInfo("", 1105030001, Me.Text)

        Else

            HI.MG.ShowMsg.mInvalidData("", 1104040001, Me.Text)

            If HI.UL.ULDate.CheckDate(Me.FTStartDate.Text) = "" Then
                FTStartDate.Focus()
            ElseIf HI.UL.ULDate.CheckDate(Me.FTEndDate.Text) = "" Then
                FTEndDate.Focus()
            End If

        End If

    End Sub

    Private Sub wCalculateWorkTime_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
    End Sub

#End Region

End Class