Public Class QACritical

    Public Sub New(CmpID As Integer, CmpCode As String, QAType As String, QADate As String, LineNo As String, StyleNo As String _
                                    , CusPO As String, JobNo As String, Qty As Integer, Seq As Integer, DefectId As Integer, DefectCode As String, DefectName As String, positname As String, StyID As Integer, HNo As String, UID As Integer)


        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        If QAType = "1" Then
            olbtop.Text = "END LINE"
            olbtop.Appearance.BackColor = Color.FromArgb(255, 192, 128)
        Else
            olbtop.Text = "FINAL"
            olbtop.Appearance.BackColor = Color.FromArgb(128, 255, 255)
        End If


        FTCmpCode.Text = CmpCode
        FTLine.Text = LineNo
        FTStyleNo.Text = StyleNo
        FTPO.Text = CusPO
        FTOrderNo.Text = JobNo
        FTQty.Text = Format(Qty, "#,##")
        FTDefectCode.Text = DefectCode
        FTDefectDesc.Text = DefectName
        FTPosition.Text = positname
        SysCmpID = CmpID
        SysDefectId = DefectId
        DefectDataSeq = Seq
        DataCriticalType = QAType
        DataCriticalDate = QADate
        SysStyleId = StyID
        HourNo = HNo
        UnitSect = UID

    End Sub

    Private CompanyID As Integer = 0
    Property SysCmpID As Integer
        Get
            Return CompanyID
        End Get
        Set(value As Integer)
            CompanyID = value
        End Set
    End Property

    Private StyleId As Integer = 0
    Property SysStyleId As Integer
        Get
            Return StyleId
        End Get
        Set(value As Integer)
            StyleId = value
        End Set
    End Property


    Private UnitSectId As Integer = 0
    Property UnitSect As Integer
        Get
            Return UnitSectId
        End Get
        Set(value As Integer)
            UnitSectId = value
        End Set
    End Property


    Private CompanyDefectId As Integer = 0
    Property SysDefectId As Integer
        Get
            Return CompanyDefectId
        End Get
        Set(value As Integer)
            CompanyDefectId = value
        End Set
    End Property

    Private DefectDataSeq As Integer = 0
    Property DataSeq As Integer
        Get
            Return DefectDataSeq
        End Get
        Set(value As Integer)
            DefectDataSeq = value
        End Set
    End Property


    Private CriticalType As String = ""
    Property DataCriticalType As String
        Get
            Return CriticalType
        End Get
        Set(value As String)
            CriticalType = value
        End Set
    End Property

    Private CriticalDate As String = ""
    Property DataCriticalDate As String
        Get
            Return CriticalDate
        End Get
        Set(value As String)
            CriticalDate = value
        End Set
    End Property

    Private HourNo As String = ""
    Property DataHourNo As String
        Get
            Return HourNo
        End Get
        Set(value As String)
            HourNo = value
        End Set
    End Property

    Private Sub ocmAcknowledge_Click(sender As Object, e As EventArgs) Handles ocmAcknowledge.Click
        Dim cmdstring As String = ""

        Select Case DataCriticalType
            Case "1"

                cmdstring = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQualityAssurance SET "
                cmdstring &= vbCrLf & " FTStateReadCritical='1' "
                cmdstring &= vbCrLf & ",FTUserReadCritical= '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                cmdstring &= vbCrLf & ",FTUserReadCriticalDate= " & HI.UL.ULDate.FormatDateDB & ""
                cmdstring &= vbCrLf & ",FTUserReadCriticalTime= " & HI.UL.ULDate.FormatTimeDB & ""
                cmdstring &= vbCrLf & " WHERE FNHSysStyleId =" & SysStyleId & ""
                cmdstring &= vbCrLf & "   And  FNHSysUnitSectId=" & UnitSect & ""
                cmdstring &= vbCrLf & "   And  FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'"
                cmdstring &= vbCrLf & "   And  FDQADate='" & DataCriticalDate & "'"
                cmdstring &= vbCrLf & "   And  FNHourNo='" & DataHourNo & "' "
                cmdstring &= vbCrLf & "   And  FNNo=" & DataSeq & ""
                cmdstring &= vbCrLf & "   And  FNHSysCmpId=" & SysCmpID & ""
                cmdstring &= vbCrLf & "   And  FNHSysQADetailId=" & SysDefectId & ""
                cmdstring &= vbCrLf & "   And  FTPointSubName='" & HI.UL.ULF.rpQuoted(FTPosition.Text) & "'"

            Case Else

                cmdstring = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQualityAssuranceFinal SET "
                cmdstring &= vbCrLf & " FTStateReadCritical='1' "
                cmdstring &= vbCrLf & ",FTUserReadCritical= '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                cmdstring &= vbCrLf & ",FTUserReadCriticalDate= " & HI.UL.ULDate.FormatDateDB & ""
                cmdstring &= vbCrLf & ",FTUserReadCriticalTime= " & HI.UL.ULDate.FormatTimeDB & ""
                cmdstring &= vbCrLf & " WHERE FNHSysStyleId =" & SysStyleId & ""
                cmdstring &= vbCrLf & "   And  FNHSysUnitSectId=" & UnitSect & ""
                cmdstring &= vbCrLf & "   And  FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'"
                cmdstring &= vbCrLf & "   And  FDQADate='" & DataCriticalDate & "'"
                cmdstring &= vbCrLf & "   And  FNHourNo='" & DataHourNo & "' "
                cmdstring &= vbCrLf & "   And  FNNo=" & DataSeq & ""
                cmdstring &= vbCrLf & "   And  FNHSysCmpId=" & SysCmpID & ""
                cmdstring &= vbCrLf & "   And  FNHSysQADetailId=" & SysDefectId & ""
                cmdstring &= vbCrLf & "   And  FTPointSubName='" & HI.UL.ULF.rpQuoted(FTPosition.Text) & "'"

        End Select


        If HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_PROD) Then
            Dim PControl As Object = Me.Parent()
            Dim PForm As System.Windows.Forms.Form = Me.FindForm()
            Try
                Select Case HI.ENM.Control.GeTypeControl(PControl)
                    Case ENM.Control.ControlType.XtraTabPage
                        Dim TabControl As DevExpress.XtraTab.XtraTabControl
                        Dim TabPageIndex As Integer = 0
                        With CType(PControl, DevExpress.XtraTab.XtraTabPage)
                            TabControl = .TabControl

                        End With

                        If TabControl.TabPages.Count <= 1 Then
                            Me.FindForm().Close()
                        Else
                            TabControl.TabPages.Remove(PControl)
                        End If

                End Select
            Catch ex As Exception
            End Try

        End If

    End Sub
End Class
