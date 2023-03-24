Public Class wMEDResultTracking

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub wMEDResultTracking_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadDataDetail()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "  Select Case when E.FCWeight > 0 and E.FCHeight > 0 Then convert(numeric(18,2),  E.FCWeight   / ((E.FCHeight / 100)*(E.FCHeight / 100))) Else 0 end AS FNBMI"
            _Cmd &= vbCrLf & ",H.FNSeqNo , E.FTEmpCode ,E.FNEmpSex"
            _Cmd &= vbCrLf & ",convert(int,dbo.FN_Get_Emp_Age(E.FDBirthDate) / 12) AS FNAge"
            _Cmd &= vbCrLf & ",case when isdate(H.FDHealth ) = 1 Then convert(nvarchar(10),convert(datetime,H.FDHealth),103) ELse '' END AS FDHealth , E.FCWeight , E.FCHeight , H.FCBloodPressure "
            _Cmd &= vbCrLf & ",H.FNPulse"
            _Cmd &= vbCrLf & ",H.FTMedGeneralResult"
            _Cmd &= vbCrLf & ",H.FTDoctorOpinion"
            _Cmd &= vbCrLf & ",H.FTNumberXRay , H.FTResultsChest , H.FTXRayNote"
            _Cmd &= vbCrLf & ",H.FCCellWBC , H.FCCellHB , H.FCCellHCT , H.FNMCV , H.FNMCH , H.FCCellN , H.FCCellL , H.FCCellM , H.FCCellE , H.FCPlatelets , H.FTCellResults , '' AS FTCellComment"
            _Cmd &= vbCrLf & ",H.FTUrineColor , H.FTUrineColor , H.FTUrineGlucose , H.FCUrineSpGr , H.FCUrinepH , H.FTUrineProtien , H.FTUrineWBC , H.FTUrineRBC , H.FTUrineEpi"
            _Cmd &= vbCrLf & ",H.FTBact , H.FTUrineResults , H.FTUrineNote"
            _Cmd &= vbCrLf & ",H.FTHBVHGs , H.FTHBVAntiHGs , H.FTHBsAgResult , H.FTHBsAgNote"
            _Cmd &= vbCrLf & ",H.FTLiverSGOTUL , H.FTLiverSGPTUL , H.FTTubResult , H.FTTubNote"
            _Cmd &= vbCrLf & ",H.FTKidneyBUNMGDL , H.FTKidneyCreatinineMGDL , H.FTTaiResult , H.FTTaiNote"
            _Cmd &= vbCrLf & ",H.FCChemisUricAcid , H.FTChemisUricAcid , H.FTChemisUricAcidResult  "
            _Cmd &= vbCrLf & ",H.FNFVC   , H.FNMCHC , H.FNFEV1 , H.FNFEVFVCPer , H.FTSpirpmrtry , H.FTLungNote"
            _Cmd &= vbCrLf & ",H.FTMethy ,H.FNMethyMg , H.FTMethyResult"
            _Cmd &= vbCrLf & ",H.FTFBS , H.FTFBSResult , H.FTFBSNote"
            _Cmd &= vbCrLf & ",H.FTFatCholesteralMGDL , H.FTFatCholesteral , H.FTChoResult"
            _Cmd &= vbCrLf & ",H.FTFatTriglyserideMGDL , H.FTFatTriglyseride , H.FTTrygleResult"
            _Cmd &= vbCrLf & ",H.FTFatHDLMGDL, H.FTFatHDL , H.FTHDLResult"
            _Cmd &= vbCrLf & ",H.FCChemisLDL , H.FTChemisLDL , H.FTLDLResult , H.FTUrineAppearance"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ",N.FTPreNameNameTH+' '+E.FTEmpNameTH + '  '+ E.FTEmpSurnameTH AS FTEmpName , x.FTNameTH AS FTSex"
                _Cmd &= vbCrLf & ",U.FTUnitSectNameTH AS FTUnitsectName"
            Else
                _Cmd &= vbCrLf & ",N.FTPreNameNameEN+' '+E.FTEmpNameEN + '  '+ E.FTEmpSurnameEN AS FTEmpName , x.FTNameEN AS FTSex"
                _Cmd &= vbCrLf & ",U.FTUnitSectNameEN AS FTUnitsectName"
            End If


            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee AS E WITH(NOLOCK) LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMSect AS S WITH(NOLOCK) ON E.FNHSysSectId = S.FNHSysSectId"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMUnitSect AS U WITH(NOLOCK) ON E.FNHSysUnitSectId = U.FNHSysUnitSectId"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTEmployeeHealthHistory AS H WITH(NOLOCK) ON E.FNHSysEmpID = H.FNHSysEmpID"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..THRMHospital AS P WITH(NOLOCK) ON H.FNHSysHospitalId = P.FNHSysHospitalId"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..THRMPrename AS N WITH(NOLOCK) ON E.FNHSysPreNameId = N.FNHSysPreNameId"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..THRMEmpType AS T WITH(NOLOCK) ON E.FNHSysEmpTypeId = T.FNHSysEmpTypeId "
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMDivision AS D WITH(NOLOCK) ON E.FNHSysDivisonId = D.FNHSysDivisonId "
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMDepartment AS M WITH(NOLOCK) ON E.FNHSysDeptId = M.FNHSysDeptId " 

            _Cmd &= vbCrLf & "LEFT OUTER JOIN "
            _Cmd &= vbCrLf & "(Select FTNameTH , FTNameEN , FNListIndex From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData WITH(NOLOCK) "
            _Cmd &= vbCrLf & "where FTListName = 'FNEmpSex' ) AS X ON E.FNEmpSex = x.FNListIndex"
            _Cmd &= vbCrLf & "Where E.FTEmpCode <> ''"
            _Cmd &= vbCrLf & " AND E.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "

            If Me.FNHSysEmpTypeId.Text <> "" Then
                _Cmd &= vbCrLf & "and T.FTEmpTypeCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "'"
            End If
            If Me.FNHSysDivisonId.Text <> "" Then
                _Cmd &= vbCrLf & "and D.FTDivisonCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "'"
            End If
            If Me.FNHSysDivisonIdTo.Text <> "" Then
                _Cmd &= vbCrLf & "and D.FTDivisonCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "'"
            End If

            If Me.FNHSysDeptId.Text <> "" Then
                _Cmd &= vbCrLf & "and M.FTDeptCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "'"
            End If
            If Me.FNHSysDeptIdTo.Text <> "" Then
                _Cmd &= vbCrLf & "and M.FTDeptCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "'"
            End If

            If Me.FNHSysSectId.Text <> "" Then
                _Cmd &= vbCrLf & "and S.FTSectCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "'"
            End If
            If Me.FNHSysSectIdTo.Text <> "" Then
                _Cmd &= vbCrLf & "and S.FTSectCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "'"
            End If

            If Me.FNHSysUnitSectId.Text <> "" Then
                _Cmd &= vbCrLf & "and U.FTUnitSectCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "'"
            End If
            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Cmd &= vbCrLf & "and U.FTUnitSectCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "'"
            End If

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR)
            Me.ogcdetail.DataSource = _oDt
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If VerrifyData() Then
                Call LoadDataDetail()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function VerrifyData() As Boolean
        Try
            Dim _State As Boolean = False
            If Me.FNHSysEmpTypeId.Text <> "" Then
                _State = True
            End If
            If Me.FNHSysDivisonId.Text <> "" Then
                _State = True
            End If
            If Me.FNHSysDivisonIdTo.Text <> "" Then
                _State = True
            End If
            If Me.FNHSysDeptId.Text <> "" Then
                _State = True
            End If
            If Me.FNHSysDeptIdTo.Text <> "" Then
                _State = True
            End If
            If Me.FNHSysSectId.Text <> "" Then
                _State = True
            End If
            If Me.FNHSysSectIdTo.Text <> "" Then
                _State = True
            End If
            If Me.FNHSysUnitSectId.Text <> "" Then
                _State = True
            End If
            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _State = True
            End If
            If Me.FNHSysEmpId.Text <> "" Then
                _State = True
            End If
            If Me.FNHSysEmpIdTo.Text <> "" Then
                _State = True
            End If
            If Not (_State) Then
                HI.MG.ShowMsg.mInfo("", 1304110001, Me.Text)
                Me.FNHSysEmpTypeId.Focus()
            End If
            Return _State
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            HI.TL.HandlerControl.ClearControl(Me)
        Catch ex As Exception

        End Try
    End Sub

    
End Class