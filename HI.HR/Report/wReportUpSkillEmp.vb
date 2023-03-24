Imports System.Data.SqlClient
Imports System.IO
Imports System.Windows.Forms

Public Class wReportUpSkillEmp

    Private _LstReport As HI.RP.ListReport
    Private _AddPopup As wReportUpSkillEmpPopUp
    Sub New(_SysFormName As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        'Me.Name = _SysFormName

        'Condition.PrePareData()
        Call PrePareData()

        _LstReport = New HI.RP.ListReport(_SysFormName)
        FNReportname.Properties.Items.AddRange(_LstReport.GetList)

        If FNReportname.Properties.Items.Count = 1 Then
            ogbreportname.Visible = False
            Me.Height = Me.Height - ogbreportname.Height
        End If

        _AddPopup = New wReportUpSkillEmpPopUp
        HI.TL.HandlerControl.AddHandlerObj(_AddPopup)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddPopup.Name.ToString.Trim, _AddPopup)
        Catch ex As Exception
        Finally
        End Try

    End Sub

#Region "EmployeeType"

    Private m_DbDtEmployeeType As New DataTable
    ReadOnly Property DbDtEmployeeType As DataTable
        Get
            Return m_DbDtEmployeeType
        End Get
    End Property

    Property ShowmEmployeeType As Boolean
        Get
            Return otpemptype.PageVisible
        End Get
        Set(value As Boolean)
            otpemptype.PageVisible = value
        End Set
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

#Region "Department"
    Private m_DbDtDepartment As New DataTable
    ReadOnly Property DbDtDepartment As DataTable
        Get
            Return m_DbDtDepartment
        End Get
    End Property

    Property ShowmDepartment As Boolean
        Get
            Return otpdepartment.PageVisible
        End Get
        Set(ByVal value As Boolean)
            otpdepartment.PageVisible = value
        End Set
    End Property

    Private Sub FNDeptCon_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles FNDeptCon.SelectedIndexChanged

        FNHSysDeptId.Properties.ReadOnly = (FNDeptCon.SelectedIndex = 0)
        FNHSysDeptIdTo.Properties.ReadOnly = Not (FNDeptCon.SelectedIndex = 1)

        FNHSysDeptId.Properties.Buttons(0).Enabled = Not (FNHSysDeptId.Properties.ReadOnly)
        FNHSysDeptIdTo.Properties.Buttons(0).Enabled = Not (FNHSysDeptIdTo.Properties.ReadOnly)

        FNHSysDeptId.Text = ""
        FNHSysDeptIdTo.Text = ""

        m_DbDtDepartment.Rows.Clear()
        m_DbDtDepartment.AcceptChanges()

    End Sub

    Private Sub FNHSysDept_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FNHSysDeptId.KeyDown
        Try
            Select Case Me.FNDeptCon.SelectedIndex
                Case 2
                    Select Case e.KeyCode
                        Case Keys.Enter

                            If FNHSysDeptId.Text = "" Then Exit Sub
                            If FNHSysDeptId.Properties.Tag.ToString = "" Then Exit Sub

                            Dim NewRow As DataRow = m_DbDtDepartment.NewRow
                            NewRow("FTCode") = FNHSysDeptId.Text
                            NewRow("FTName") = FNHSysDeptId_None.Text

                            m_DbDtDepartment.Rows.Add(NewRow)
                            m_DbDtDepartment.AcceptChanges()

                    End Select
                Case Else
            End Select

        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try

    End Sub

    Private Sub ogvdept_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ogvdept.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    Call ogvdept_DoubleClick(ogvemptype, New System.EventArgs)
            End Select
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub

    Private Sub ogvdept_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ogvdept.DoubleClick
        Try
            With ogvdept
                If .FocusedRowHandle < 0 Then Exit Sub
                .DeleteRow(.FocusedRowHandle)
                m_DbDtDepartment.AcceptChanges()
            End With
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub

#End Region

#Region "Division"
    Private m_DbDtDivision As New DataTable
    ReadOnly Property DbDtDivision As DataTable
        Get
            Return m_DbDtDivision
        End Get
    End Property

    Property ShowmDivision As Boolean
        Get
            Return otpdivision.PageVisible
        End Get
        Set(ByVal value As Boolean)
            otpdivision.PageVisible = value
        End Set
    End Property

    Private Sub FNDivisionCon_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles FNDivisionCon.SelectedIndexChanged

        FNHSysDivisonId.Properties.ReadOnly = (FNDivisionCon.SelectedIndex = 0)
        FNHSysDivisonIdTo.Properties.ReadOnly = Not (FNDivisionCon.SelectedIndex = 1)

        FNHSysDivisonId.Properties.Buttons(0).Enabled = Not (FNHSysDivisonId.Properties.ReadOnly)
        FNHSysDivisonIdTo.Properties.Buttons(0).Enabled = Not (FNHSysDivisonIdTo.Properties.ReadOnly)

        FNHSysDeptId.Text = ""
        FNHSysDeptIdTo.Text = ""

        m_DbDtDivision.Rows.Clear()
        m_DbDtDivision.AcceptChanges()

    End Sub

    Private Sub FNHSysDivison_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FNHSysDivisonId.KeyDown

        Try
            Select Case Me.FNDivisionCon.SelectedIndex
                Case 2
                    Select Case e.KeyCode
                        Case Keys.Enter

                            If FNHSysDivisonId.Text = "" Then Exit Sub
                            If FNHSysDivisonId.Properties.Tag.ToString = "" Then Exit Sub

                            Dim NewRow As DataRow = m_DbDtDivision.NewRow
                            NewRow("FTCode") = FNHSysDivisonId.Text
                            NewRow("FTName") = FNHSysDivisonId_None.Text

                            m_DbDtDivision.Rows.Add(NewRow)
                            m_DbDtDivision.AcceptChanges()

                    End Select
                Case Else
            End Select
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub

    Private Sub ogvdiv_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ogvdiv.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    Call ogvdiv_DoubleClick(ogvemptype, New System.EventArgs)
            End Select
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub

    Private Sub ogvdiv_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ogvdiv.DoubleClick
        Try
            With ogvdiv
                If .FocusedRowHandle < 0 Then Exit Sub
                .DeleteRow(.FocusedRowHandle)
                m_DbDtDivision.AcceptChanges()
            End With
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub

#End Region

#Region "Sect"
    Private m_DbDtSect As New DataTable
    ReadOnly Property DbDtSect As DataTable
        Get
            Return m_DbDtSect
        End Get
    End Property

    Property ShowmSect As Boolean
        Get
            Return otpsect.PageVisible
        End Get
        Set(ByVal value As Boolean)
            otpsect.PageVisible = value
        End Set
    End Property

    Private Sub FNSectCon_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles FNSectCon.SelectedIndexChanged

        FNHSysSectId.Properties.ReadOnly = (FNSectCon.SelectedIndex = 0)
        FNHSysSectIdTo.Properties.ReadOnly = Not (FNSectCon.SelectedIndex = 1)

        FNHSysSectId.Properties.Buttons(0).Enabled = Not (FNHSysSectId.Properties.ReadOnly)
        FNHSysSectIdTo.Properties.Buttons(0).Enabled = Not (FNHSysSectIdTo.Properties.ReadOnly)

        FNHSysSectId.Text = ""
        FNHSysSectIdTo.Text = ""

        m_DbDtSect.Rows.Clear()
        m_DbDtSect.AcceptChanges()

    End Sub

    Private Sub FNHSysSectId_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FNHSysSectId.KeyDown
        Try
            Select Case Me.FNSectCon.SelectedIndex
                Case 2
                    Select Case e.KeyCode
                        Case Keys.Enter

                            If FNHSysSectId.Text = "" Then Exit Sub
                            If FNHSysSectId.Properties.Tag.ToString = "" Then Exit Sub

                            Dim NewRow As DataRow = m_DbDtSect.NewRow
                            NewRow("FTCode") = FNHSysSectId.Text
                            NewRow("FTName") = FNHSysSectId_None.Text

                            m_DbDtSect.Rows.Add(NewRow)
                            m_DbDtSect.AcceptChanges()

                    End Select
                Case Else
            End Select
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub

    Private Sub ogvSect_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ogvsect.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    Call ogvsect_DoubleClick(ogvemptype, New System.EventArgs)
            End Select
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub

    Private Sub ogvsect_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ogvsect.DoubleClick
        Try
            With ogvsect
                If .FocusedRowHandle < 0 Then Exit Sub
                .DeleteRow(.FocusedRowHandle)
                m_DbDtSect.AcceptChanges()
            End With
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub

#End Region

#Region "Unit Sect"
    Private m_DbDtUnitSect As New DataTable
    ReadOnly Property DbDtUnitSect As DataTable
        Get
            Return m_DbDtUnitSect
        End Get
    End Property

    Property ShowmUnitSect As Boolean

        Get
            Return otpunitsect.PageVisible
        End Get

        Set(ByVal value As Boolean)
            otpunitsect.PageVisible = value
        End Set

    End Property

    Private Sub FNUnitSectCon_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles FNUnitSectCon.SelectedIndexChanged

        FNHSysUnitSectId.Properties.ReadOnly = (FNUnitSectCon.SelectedIndex = 0)
        FNHSysUnitSectIdTo.Properties.ReadOnly = Not (FNUnitSectCon.SelectedIndex = 1)

        FNHSysUnitSectId.Properties.Buttons(0).Enabled = Not (FNHSysUnitSectId.Properties.ReadOnly)
        FNHSysUnitSectIdTo.Properties.Buttons(0).Enabled = Not (FNHSysUnitSectIdTo.Properties.ReadOnly)

        FNHSysUnitSectId.Text = ""
        FNHSysUnitSectIdTo.Text = ""

        m_DbDtUnitSect.Rows.Clear()
        m_DbDtUnitSect.AcceptChanges()

    End Sub

    Private Sub FNHSysUnitSectId_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FNHSysUnitSectId.KeyDown
        Try
            Select Case Me.FNUnitSectCon.SelectedIndex
                Case 2
                    Select Case e.KeyCode
                        Case Keys.Enter

                            If FNHSysUnitSectId.Text = "" Then Exit Sub
                            If FNHSysUnitSectId.Properties.Tag.ToString = "" Then Exit Sub

                            Dim NewRow As DataRow = m_DbDtUnitSect.NewRow
                            NewRow("FTCode") = FNHSysUnitSectId.Text
                            NewRow("FTName") = FNHSysUnitSectId_None.Text

                            m_DbDtUnitSect.Rows.Add(NewRow)
                            m_DbDtUnitSect.AcceptChanges()

                    End Select
                Case Else
            End Select
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try

    End Sub

    Private Sub ogvunitSect_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ogvunitsect.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    Call ogvunitsect_DoubleClick(ogvemptype, New System.EventArgs)
            End Select
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub

    Private Sub ogvunitsect_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ogvunitsect.DoubleClick
        Try
            With ogvunitsect
                If .FocusedRowHandle < 0 Then Exit Sub
                .DeleteRow(.FocusedRowHandle)
                m_DbDtUnitSect.AcceptChanges()
            End With
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub

#End Region

#Region "Employee"

    Private m_DbDtEmp As New DataTable
    ReadOnly Property DbDtEmp As DataTable
        Get
            Return m_DbDtEmp
        End Get
    End Property

    Property ShowmEmployee As Boolean
        Get
            Return otpemployee.PageVisible
        End Get

        Set(ByVal value As Boolean)
            otpemployee.PageVisible = value
        End Set

    End Property






    Private Sub FNEmpCon_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles FNEmpCon.SelectedIndexChanged

        FNHSysEmpID.Properties.ReadOnly = (FNEmpCon.SelectedIndex = 0)
        FNHSysEmpIDTo.Properties.ReadOnly = Not (FNEmpCon.SelectedIndex = 1)

        FNHSysEmpID.Properties.Buttons(0).Enabled = Not (FNHSysEmpID.Properties.ReadOnly)
        FNHSysEmpIDTo.Properties.Buttons(0).Enabled = Not (FNHSysEmpIDTo.Properties.ReadOnly)

        FNHSysEmpID.Text = ""
        FNHSysEmpIDTo.Text = ""

        m_DbDtEmp.Rows.Clear()
        m_DbDtEmp.AcceptChanges()

    End Sub



    Private Sub ocbemp3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FNHSysEmpID.KeyDown

        Try
            Select Case Me.FNEmpCon.SelectedIndex
                Case 2
                    Select Case e.KeyCode
                        Case Keys.Enter

                            If FNHSysEmpID.Text = "" Then Exit Sub
                            If FNHSysEmpID.Properties.Tag.ToString = "" Then Exit Sub

                            Dim NewRow As DataRow = m_DbDtEmp.NewRow
                            NewRow("FTCode") = FNHSysEmpID.Text
                            NewRow("FTName") = FNHSysEmpID_None.Text

                            m_DbDtEmp.Rows.Add(NewRow)
                            m_DbDtEmp.AcceptChanges()

                    End Select
                Case Else
            End Select
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try

    End Sub
    Private Sub ogvemp_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ogvemp.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    Call ogvemp_DoubleClick(ogvemptype, New System.EventArgs)
            End Select
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub

    Private Sub ogvemp_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ogvemp.DoubleClick
        Try
            With ogvemp
                If .FocusedRowHandle < 0 Then Exit Sub
                .DeleteRow(.FocusedRowHandle)
                m_DbDtEmp.AcceptChanges()
            End With
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub

#End Region

#Region "SectType"

    Private m_DbDtSectType As New DataTable
    ReadOnly Property DbDtSectType As DataTable
        Get
            Return m_DbDtSectType
        End Get
    End Property

    Property ShowmSectType As Boolean
        Get
            Return otpSectType.PageVisible
        End Get

        Set(ByVal value As Boolean)
            otpSectType.PageVisible = value
        End Set

    End Property






    Private Sub FNSectType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles FNSectType.SelectedIndexChanged

        FTSectType.Properties.ReadOnly = (FNSectType.SelectedIndex = 0)
        FTSectTypeTo.Properties.ReadOnly = Not (FNSectType.SelectedIndex = 1)

        FTSectType.Properties.Buttons(0).Enabled = Not (FTSectType.Properties.ReadOnly)
        FTSectTypeTo.Properties.Buttons(0).Enabled = Not (FTSectTypeTo.Properties.ReadOnly)

        FTSectType.Text = ""
        FTSectTypeTo.Text = ""

        m_DbDtSectType.Rows.Clear()
        m_DbDtSectType.AcceptChanges()

    End Sub



    Private Sub FTSectType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FTSectType.KeyDown

        Try
            Select Case Me.FNSectType.SelectedIndex
                Case 2
                    Select Case e.KeyCode
                        Case Keys.Enter

                            If FTSectType.Text = "" Then Exit Sub
                            If FTSectType.Properties.Tag.ToString = "" Then Exit Sub

                            Dim NewRow As DataRow = m_DbDtSectType.NewRow
                            NewRow("FTCode") = FTSectType.Text
                            NewRow("FTName") = "-"

                            m_DbDtSectType.Rows.Add(NewRow)
                            m_DbDtSectType.AcceptChanges()

                    End Select
                Case Else
            End Select
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try

    End Sub
    Private Sub ogvSectType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ogvSectType.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    Call ogvSectType_DoubleClick(ogvemptype, New System.EventArgs)
            End Select
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub

    Private Sub ogvSectType_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ogvSectType.DoubleClick
        Try
            With ogvSectType
                If .FocusedRowHandle < 0 Then Exit Sub
                .DeleteRow(.FocusedRowHandle)
                m_DbDtSectType.AcceptChanges()
            End With
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub

#End Region

    '#Region "Course"
    '    Private m_DbDtCours As New DataTable
    '    ReadOnly Property DbDtCours As DataTable
    '        Get
    '            Return m_DbDtCours
    '        End Get
    '    End Property

    '    Property ShowCourse As Boolean
    '        Get
    '            Return otpcoursecode.PageVisible
    '        End Get
    '        Set(value As Boolean)
    '            otpcoursecode.PageVisible = value
    '        End Set
    '    End Property

    '    Private Sub FTTrainCode_KeyDown(sender As Object, e As KeyEventArgs)
    '        Try
    '            Select Case Me.FNCourse.SelectedIndex
    '                Case 2
    '                    Select Case e.KeyCode
    '                        Case Keys.Enter

    '                            If FTTrainCode.Text = "" Then Exit Sub
    '                            If FTTrainCode.Properties.Tag.ToString = "" Then Exit Sub

    '                            Dim NewRow As DataRow = m_DbDtCours.NewRow
    '                            NewRow("FTCode") = FTTrainCode.Text
    '                            NewRow("FTName") = FTTrainCode_None.Text

    '                            m_DbDtCours.Rows.Add(NewRow)
    '                            m_DbDtCours.AcceptChanges()

    '                    End Select
    '                Case Else
    '            End Select
    '        Catch ex As Exception
    '            HI.MG.ShowMsg.mProcessError(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub FNCourse_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        FTTrainCode.Properties.ReadOnly = (FNCourse.SelectedIndex = 0)
    '        FTTrainCodeTo.Properties.ReadOnly = Not (FNCourse.SelectedIndex = 1)

    '        FTTrainCode.Properties.Buttons(0).Enabled = Not (FTTrainCode.Properties.ReadOnly)
    '        FTTrainCodeTo.Properties.Buttons(0).Enabled = Not (FTTrainCodeTo.Properties.ReadOnly)

    '        FTTrainCode.Text = ""
    '        FTTrainCodeTo.Text = ""

    '        m_DbDtCours.Rows.Clear()
    '        m_DbDtCours.AcceptChanges()
    '    End Sub

    '    Private Sub ogvcourse_DoubleClick(sender As Object, e As EventArgs)
    '        Try
    '            With ogvcourse
    '                If .FocusedRowHandle < 0 Then Exit Sub
    '                .DeleteRow(.FocusedRowHandle)
    '                m_DbDtCours.AcceptChanges()
    '            End With
    '        Catch ex As Exception
    '            HI.MG.ShowMsg.mProcessError(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub ogvcourse_KeyDown(sender As Object, e As KeyEventArgs)
    '        Try
    '            Select Case e.KeyCode
    '                Case Keys.Delete
    '                    Call ogvcourse_DoubleClick(ogvcourse, New EventArgs)
    '            End Select
    '        Catch ex As Exception
    '            HI.MG.ShowMsg.mProcessError(ex.Message)
    '        End Try
    '    End Sub
    '#End Region

    '#Region "Doc"
    '    Private m_DbDtDoc As New DataTable
    '    ReadOnly Property DbDtDoc As DataTable
    '        Get
    '            Return m_DbDtDoc
    '        End Get
    '    End Property

    '    Property ShowDoc As Boolean
    '        Get
    '            Return otpdocno.PageVisible
    '        End Get
    '        Set(value As Boolean)
    '            otpdocno.PageVisible = value
    '        End Set
    '    End Property

    '    Private Sub FNDoc_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        FTDocNo.Properties.ReadOnly = (FNDoc.SelectedIndex = 0)
    '        FTDocNoTo.Properties.ReadOnly = Not (FNDoc.SelectedIndex = 1)

    '        FTDocNo.Properties.Buttons(0).Enabled = Not (FTDocNo.Properties.ReadOnly)
    '        FTDocNoTo.Properties.Buttons(0).Enabled = Not (FTDocNoTo.Properties.ReadOnly)

    '        FTDocNo.Text = ""
    '        FTDocNoTo.Text = ""

    '        m_DbDtDoc.Rows.Clear()
    '        m_DbDtDoc.AcceptChanges()
    '    End Sub

    '    Private Sub FTDocNo_KeyDown(sender As Object, e As KeyEventArgs)
    '        Try
    '            Select Case Me.FNDoc.SelectedIndex
    '                Case 2
    '                    Select Case e.KeyCode
    '                        Case Keys.Enter

    '                            If FTDocNo.Text = "" Then Exit Sub
    '                            If FTDocNo.Properties.Tag.ToString = "" Then Exit Sub

    '                            Dim NewRow As DataRow = m_DbDtDoc.NewRow
    '                            NewRow("FTCode") = FTDocNo.Text

    '                            m_DbDtDoc.Rows.Add(NewRow)
    '                            m_DbDtDoc.AcceptChanges()

    '                    End Select 
    '                Case Else
    '            End Select
    '        Catch ex As Exception
    '            MsgBox(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub ogvdoc_DoubleClick(sender As Object, e As EventArgs)
    '        Try
    '            With ogvdoc
    '                If .FocusedRowHandle < 0 Then Exit Sub
    '                .DeleteRow(.FocusedRowHandle)
    '                m_DbDtDoc.AcceptChanges()
    '            End With
    '        Catch ex As Exception
    '            HI.MG.ShowMsg.mProcessError(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub ogvdoc_KeyDown(sender As Object, e As KeyEventArgs)
    '        Try
    '            Call ogvdoc_DoubleClick(ogvdoc, New EventArgs)
    '        Catch ex As Exception
    '            HI.MG.ShowMsg.mProcessError(ex.Message)
    '        End Try
    '    End Sub
    '#End Region

    '#Region "CourseName"
    '    Private m_DbDtCosName As New DataTable
    '    ReadOnly Property DbDtCosName As DataTable
    '        Get
    '            Return m_DbDtCosName
    '        End Get
    '    End Property

    '    Property ShowCourseName As Boolean
    '        Get
    '            Return otpcoursename.PageVisible
    '        End Get
    '        Set(value As Boolean)
    '            otpcoursename.PageVisible = value
    '        End Set
    '    End Property

    '    Private Sub FNCoursename_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        FTTrainDesc1.Properties.ReadOnly = (FNCoursename.SelectedIndex = 0)
    '        FTTrainDesc1To.Properties.ReadOnly = Not (FNCoursename.SelectedIndex = 1)

    '        FTTrainDesc1.Properties.Buttons(0).Enabled = Not (FTTrainDesc1.Properties.ReadOnly)
    '        FTTrainDesc1To.Properties.Buttons(0).Enabled = Not (FTTrainDesc1To.Properties.ReadOnly)

    '        FTTrainDesc1.Text = ""
    '        FTTrainDesc1To.Text = ""

    '        m_DbDtCosName.Rows.Clear()
    '        m_DbDtCosName.AcceptChanges()
    '    End Sub

    '    Private Sub FTTrainDesc1_KeyDown(sender As Object, e As KeyEventArgs)
    '        Try
    '            Select Case FNCoursename.SelectedIndex
    '                Case 2
    '                    Select Case e.KeyCode
    '                        Case Keys.Enter
    '                            If FTTrainDesc1.Text = "" Then Exit Sub
    '                            If FTTrainDesc1.Properties.Tag.ToString = "" Then Exit Sub

    '                            Dim NewRow As DataRow = m_DbDtCosName.NewRow
    '                            NewRow("FTTrainCode") = FTTrainDesc1.Properties.Tag.ToString
    '                            NewRow("FTName") = FTTrainDesc1.Text
    '                            m_DbDtCosName.Rows.Add(NewRow)
    '                            m_DbDtCosName.AcceptChanges()
    '                    End Select
    '            End Select
    '        Catch ex As Exception
    '            MsgBox(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub ogvcoursename_DoubleClick(sender As Object, e As EventArgs)
    '        Try
    '            With ogvcoursename
    '                If .FocusedRowHandle < 0 Then Exit Sub
    '                .DeleteRow(.FocusedRowHandle)
    '                m_DbDtCosName.AcceptChanges()
    '            End With
    '        Catch ex As Exception
    '            HI.MG.ShowMsg.mProcessError(ex.Message)
    '        End Try
    '    End Sub

    '#End Region

    Public Sub PrePareData()
        Dim tSql As String = ""
        Try
            Dim oDbDt As DataTable

            If otpemptype.PageVisible Then

                tSql = "SELECT FTCode  FROM (SELECT  TOP 0  '' AS FTCode  "
                tSql &= "  FROM THRMEmpType  WITH(NOLOCK) )AS M "
                tSql &= " ORDER BY FTCode "

                oDbDt = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_MASTER)


                m_DbDtEmployeeType = oDbDt.Clone
                ogdemptype.DataSource = m_DbDtEmployeeType

            End If

            If otpdepartment.PageVisible Then

                tSql = "SELECT * FROM (SELECT  TOP 0   '' AS FTCode,'' AS FTName"
                tSql &= " FROM TCNMDepartment  WITH(NOLOCK) ) AS M"
                tSql &= " ORDER BY FTCode"

                oDbDt = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_MASTER)

                m_DbDtDepartment = oDbDt.Clone
                ogddept.DataSource = m_DbDtDepartment

            End If

            If otpdivision.PageVisible Then

                tSql = "SELECT * FROM (SELECT  TOP 0  '' AS FTCode,'' AS FTName "
                tSql &= " FROM TCNMDivision  WITH(NOLOCK) ) AS M"
                tSql &= " ORDER BY FTCode "

                oDbDt = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_MASTER)

                m_DbDtDivision = oDbDt.Clone
                ogddiv.DataSource = m_DbDtDivision
            End If

            If otpsect.PageVisible Then

                tSql = "SELECT  * FROM (SELECT  TOP 0  '' AS FTCode,'' AS FTName "
                tSql &= " FROM TCNMSect  WITH(NOLOCK) ) AS M"
                tSql &= " ORDER BY FTCode "

                oDbDt = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_MASTER)

                m_DbDtSect = oDbDt.Clone
                ogdsect.DataSource = m_DbDtSect
            End If

            If otpunitsect.PageVisible Then
                tSql = "SELECT * FROM (SELECT  TOP 0   '' AS FTCode,'' AS FTName "
                tSql &= " FROM TCNMUnitSect  WITH(NOLOCK) ) AS M"
                tSql &= " ORDER BY FTCode "

                oDbDt = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_MASTER)

                m_DbDtUnitSect = oDbDt.Clone
                ogdunitsect.DataSource = m_DbDtUnitSect
            End If

            If otpemployee.PageVisible Then

                tSql = "SELECT  * FROM (SELECT  TOP 0  '' AS FTCode,'' AS FTName"
                tSql &= " FROM THRMEmployee  WITH(NOLOCK) ) AS M"
                tSql &= " ORDER BY FTCode "

                oDbDt = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_HR)

                m_DbDtEmp = oDbDt.Clone
                ogdemp.DataSource = m_DbDtEmp

            End If

            If otpSectType.PageVisible Then

                tSql = "SELECT  * FROM (SELECT  TOP 0  '' AS FTCode,'' AS FTName"
                tSql &= " FROM HSysListData  WITH(NOLOCK) ) AS M"
                tSql &= " ORDER BY FTCode "

                oDbDt = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_SYSTEM)

                m_DbDtSectType = oDbDt.Clone
                ogdSectType.DataSource = m_DbDtSectType

            End If

            'If otpcoursecode.PageVisible Then
            '    m_DbDtCours.Columns.Add("FTCode")
            '    m_DbDtCours.Columns.Add("FTName")
            '    m_DbDtCours.Rows.Add("", "")
            '    ogccourse.DataSource = m_DbDtCours
            'End If

            'If otpdocno.PageVisible Then
            '    m_DbDtDoc.Columns.Add("FTCode")
            '    m_DbDtDoc.Rows.Add("")
            '    ogcdoc.DataSource = m_DbDtDoc
            'End If

            'If otpcoursename.PageVisible Then
            '    m_DbDtCosName.Columns.Add("FTTrainCode")
            '    m_DbDtCosName.Columns.Add("FTName")
            '    m_DbDtCosName.Rows.Add("", "")
            '    ogccoursename.DataSource = m_DbDtCosName
            'End If

        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        Finally

        End Try
    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmpreview.Click
        Dim _Formular As String = ""
        With _AddPopup
            '*****วันที่เริ่มงาน*********
            If Me.FDWorkStart.Text <> "" Then
                _Formular &= " AND "
                _Formular &= " M.FDDateStart >='" & HI.UL.ULDate.ConvertEnDB(Me.FDWorkStart.Text) & "' "
            End If

            If Me.FDWorkEnd.Text <> "" Then
                _Formular &= " AND "
                _Formular &= " M.FDDateStart <='" & HI.UL.ULDate.ConvertEnDB(Me.FDWorkEnd.Text) & "' "
            End If

            '*****วันที่ผ่าน Pro *********
            If Me.FDSDateProbation.Text <> "" Then
                _Formular &= " AND "
                _Formular &= " M.FDDateProbation >='" & HI.UL.ULDate.ConvertEnDB(Me.FDSDateProbation.Text) & "' "
            End If

            If Me.FDEDateProbation.Text <> "" Then
                _Formular &= " AND "
                _Formular &= " M.FDDateProbation <='" & HI.UL.ULDate.ConvertEnDB(Me.FDEDateProbation.Text) & "' "
            End If

            '*****วันที่ลาออก*********
            If Me.FDResignStart.Text <> "" Then
                _Formular &= " AND "
                _Formular &= " M.FDDateEnd >='" & HI.UL.ULDate.ConvertEnDB(Me.FDResignStart.Text) & "' "
            End If

            If Me.FDResignEnd.Text <> "" Then
                _Formular &= " AND "
                _Formular &= " M.FDDateEnd <='" & HI.UL.ULDate.ConvertEnDB(Me.FDResignEnd.Text) & "' "
            End If

            '*****วันเกิด*********
            If Me.FDBirthStart.Text <> "" Then
                _Formular &= " AND "
                _Formular &= " M.FDBirthDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FDBirthStart.Text) & "' "
            End If

            If Me.FDBirthEnd.Text <> "" Then
                _Formular &= " AND "
                _Formular &= " M.FDBirthDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FDBirthEnd.Text) & "' "
            End If




            .Str = ""
            .Str &= _Formular
            .Str &= GetCriteria()
            .__Qry = Qry

            .ShowDialog()
        End With






    End Sub

    Private Sub wReportMasterCondition_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If FNReportname.Properties.Items.Count < 0 Then
                MsgBox("ไม่พบการกำหนด File Report !!!")
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Dim Qry As String = ""
    Private Function GetCriteria() As String
        Dim _Criteria As String = ""
        '***Empployee Type***
        Dim tText As String = ""
        If (Me.otpemptype.PageVisible) Then
            Select Case Me.FNEmpTypeCon.SelectedIndex
                Case 1

                    If Me.FNHSysEmpTypeId.Text <> "" Then
                        _Criteria &= " AND M.FTEmpTypeCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
                    End If

                    If Me.FNHSysEmpTypeIdTo.Text <> "" Then
                        _Criteria &= " AND M.FTEmpTypeCode  <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeIdTo.Text) & "' "
                    End If

                Case 2
                    tText = ""

                    For Each oRow As DataRow In Me.DbDtEmployeeType.Rows
                        tText &= oRow("FTCode") & "|"
                    Next

                    If tText.Trim <> "" Then
                        tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                        _Criteria &= vbCrLf & " AND M.FTEmpTypeCode IN ('" & tText.Replace("|", "','") & "') "
                    End If

                Case Else
            End Select
        End If

        '***Department***
        If (Me.otpdepartment.PageVisible) Then
            Select Case Me.FNDeptCon.SelectedIndex
                Case 1
                    If Me.FNHSysDeptId.Text <> "" Then
                        _Criteria &= " AND DP.FTDeptCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
                    End If
                    If Me.FNHSysDeptIdTo.Text <> "" Then
                        _Criteria &= " AND DP.FTDeptCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
                    End If
                Case 2
                    tText = ""
                    For Each oRow As DataRow In Me.DbDtDepartment.Rows
                        tText &= oRow("FTCode") & "|"
                    Next
                    If tText.Trim <> "" Then
                        tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                        _Criteria &= vbCrLf & " AND DP.FTDeptCode IN ('" & tText.Replace("|", "','") & "') "
                    End If

                Case Else
            End Select
        End If


        '***Division***
        If (Me.otpdivision.PageVisible) Then
            Select Case Me.FNDivisionCon.SelectedIndex
                Case 1
                    If Me.FNHSysDivisonId.Text <> "" Then
                        _Criteria &= " AND D.FTDivisonCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
                    End If
                    If Me.FNHSysDivisonIdTo.Text <> "" Then
                        _Criteria &= " AND D.FTDivisonCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
                    End If
                Case 2
                    tText = ""

                    For Each oRow As DataRow In Me.DbDtDivision.Rows
                        tText &= oRow("FTCode") & "|"
                    Next

                    If tText.Trim <> "" Then
                        tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                        _Criteria &= vbCrLf & " AND D.FTDivisonCode IN ('" & tText.Replace("|", "','") & "') "
                    End If
                Case Else
            End Select
        End If


        '***Sect***
        If (Me.otpsect.PageVisible) Then
            Select Case FNSectCon.SelectedIndex
                Case 1
                    If Me.FNHSysSectId.Text <> "" Then
                        _Criteria &= " AND S.FTSectCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
                    End If
                    If Me.FNHSysSectIdTo.Text <> "" Then
                        _Criteria &= " AND S.FTSectCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
                    End If
                Case 2
                    tText = ""

                    For Each oRow As DataRow In Me.DbDtSect.Rows
                        tText &= oRow("FTCode") & "|"
                    Next

                    If tText.Trim <> "" Then
                        tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                        _Criteria &= vbCrLf & " AND S.FTSectCode IN ('" & tText.Replace("|", "','") & "') "
                    End If
                Case Else
            End Select
        End If


        '***Unit Sect***
        If (Me.otpunitsect.PageVisible) Then
            Select Case FNUnitSectCon.SelectedIndex
                Case 1
                    If Me.FNHSysUnitSectId.Text <> "" Then
                        _Criteria &= " AND U.FTUnitSectCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
                    End If
                    If Me.FNHSysUnitSectIdTo.Text <> "" Then
                        _Criteria &= " AND U.FTUnitSectCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
                    End If
                Case 2
                    tText = ""

                    For Each oRow As DataRow In Me.DbDtUnitSect.Rows
                        tText &= oRow("FTCode") & "|"
                    Next

                    If tText.Trim <> "" Then
                        tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                        _Criteria &= vbCrLf & " AND U.FTUnitSectCode IN ('" & tText.Replace("|", "','") & "') "
                    End If

                Case Else
            End Select
        End If


        '***Employee***
        If (Me.otpemployee.PageVisible) Then
            Select Case FNEmpCon.SelectedIndex
                Case 1
                    If Me.FNHSysEmpID.Text <> "" Then
                        _Criteria &= " AND M.FTEmpCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpID.Text) & "' "
                    End If
                    If Me.FNHSysEmpIDTo.Text <> "" Then
                        _Criteria &= " AND M.FTEmpCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIDTo.Text) & "' "
                    End If
                Case 2
                    tText = ""

                    For Each oRow As DataRow In Me.DbDtEmp.Rows
                        tText &= oRow("FTCode") & "|"
                    Next

                    If tText.Trim <> "" Then
                        tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                        _Criteria &= vbCrLf & "  AND M.FTEmpCode IN ('" & tText.Replace("|", "','") & "') "
                    End If

                Case Else
            End Select
        End If
        '***SectType***
        Qry = ""
        If (Me.otpSectType.PageVisible) Then
            If ST.Lang.Language = ST.Lang.eLang.TH Then
                Select Case FNSectType.SelectedIndex
                    Case 1
                        If Me.FTSectType.Text <> "" Then
                            Qry &= " AND L.FTNameTH >='" & HI.UL.ULF.rpQuoted(Me.FTSectType.Text) & "' "
                        End If
                        If Me.FTSectTypeTo.Text <> "" Then
                            Qry &= " AND L.FTNameTH <='" & HI.UL.ULF.rpQuoted(Me.FTSectTypeTo.Text) & "' "
                        End If
                    Case 2
                        tText = ""

                        For Each oRow As DataRow In Me.DbDtSectType.Rows
                            tText &= oRow("FTCode") & "|"
                        Next

                        If tText.Trim <> "" Then
                            tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                            Qry &= vbCrLf & " AND L.FTNameTH IN ('" & tText.Replace("|", "','") & "') "
                        End If
                End Select
            Else
                Select Case FNSectType.SelectedIndex
                    Case 1
                        If Me.FTSectType.Text <> "" Then
                            Qry &= " AND L.FTNameEN >='" & HI.UL.ULF.rpQuoted(Me.FTSectType.Text) & "' "
                        End If
                        If Me.FTSectTypeTo.Text <> "" Then
                            Qry &= " AND L.FTNameEN <='" & HI.UL.ULF.rpQuoted(Me.FTSectTypeTo.Text) & "' "
                        End If
                    Case 2
                        tText = ""

                        For Each oRow As DataRow In Me.DbDtSectType.Rows
                            tText &= oRow("FTCode") & "|"
                        Next

                        If tText.Trim <> "" Then
                            tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                            Qry &= vbCrLf & " AND L.FTNameEN IN ('" & tText.Replace("|", "','") & "') "
                        End If
                End Select
            End If

        End If


        Return _Criteria
    End Function


End Class