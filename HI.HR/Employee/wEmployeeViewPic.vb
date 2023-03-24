Imports System.IO

Imports System.Drawing

Public Class wEmployeeViewPic
    'Public wEmployee_CVN As wEmployee_CVN
    'Private _AddHealth As wAddEmpHealth
    'Private _AddHealthCost As wAddEmpHealthCost

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        '_AddHealth = New wAddEmpHealth
        '_AddHealthCost = New wAddEmpHealthCost

        'Dim _SystemLang As New ST.SysLanguage

        'Try
        '    Call _SystemLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddHealth.Name.ToString.Trim, _AddHealth)
        'Catch ex As Exception
        'Finally
        'End Try

        'Try

        '    Call _SystemLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddHealthCost.Name.ToString.Trim, _AddHealthCost)
        'Catch ex As Exception
        'Finally
        'End Try

        'HI.TL.HandlerControl.AddHandlerObj(_AddHealth)
        ' HI.TL.HandlerControl.AddHandlerObj(_AddHealthCost)

        ' Call TabChange()

    End Sub

#Region "Procedure"
    Private _FNSeqId As String
    Public Property FNSeqId As String
        Get
            Return _FNSeqId
        End Get
        Set(value As String)
            _FNSeqId = value
        End Set
    End Property

    Private _FTPicType As String
    Public Property FTPicType As String
        Get
            Return _FTPicType
        End Get
        Set(value As String)
            _FTPicType = value
        End Set
    End Property

    Private _FNHSysEmpID As String
    Public Property FNHSysEmpID As String
        Get
            Return _FNHSysEmpID
        End Get
        Set(value As String)
            _FNHSysEmpID = value
        End Set
    End Property
#End Region

#Region "General"

    Private Sub wEmployeeViewPic_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
            Dim _Qry As String = ""
            Dim _oDt As DataTable
            Dim _DocNo As String = ""
            Dim _dtdoc As New DataTable

            Select Case FTPicType
                Case "Passport"

                    _Qry = "SELECT   P.FNPassportSeq,P.FTPassPortNo,P.FTPassportNote,P.FBFileImage,E.FTEmpCode"
                    _Qry &= vbCrLf & ", CASE WHEN ISDATE(P.FDDateofIssue)=1 THEN SUBSTRING(P.FDDateofIssue,9,2)+'/'+SUBSTRING(P.FDDateofIssue,6,2)+'/'+SUBSTRING(P.FDDateofIssue,1,4) ELSE '' END AS FDDateofIssue1"
                    _Qry &= vbCrLf & ", CASE WHEN ISDATE(P.FDDateofExpiry)=1 THEN SUBSTRING(P.FDDateofExpiry,9,2)+'/'+SUBSTRING(P.FDDateofExpiry,6,2)+'/'+SUBSTRING(P.FDDateofExpiry,1,4) ELSE '' END AS FDDateofExpiry"
                    _Qry &= vbCrLf & "FROM    " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Passport AS P WITH (NOLOCK) LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee AS E WITH (NOLOCK) ON P.FNHSysEmpID=E.FNHSysEmpID "
                    _Qry &= vbCrLf & " WHERE E.FNHSysEmpID ='" & _FNHSysEmpID & "' AND P.FNPassportSeq = '" & _FNSeqId & "'"
                    _Qry &= vbCrLf & " ORDER BY P.FNPassportSeq desc"

                Case "Visa"
                    _Qry = "SELECT   V.FNVisaSeq,V.FTVisaNo,V.FTVisatNote,V.FBFileImage,E.FTEmpCode"
                    _Qry &= vbCrLf & ", CASE WHEN ISDATE(V.FDDateofIssue)=1 THEN SUBSTRING(V.FDDateofIssue,9,2)+'/'+SUBSTRING(V.FDDateofIssue,6,2)+'/'+SUBSTRING(V.FDDateofIssue,1,4) ELSE '' END AS FDDateofIssue"
                    _Qry &= vbCrLf & " , CASE WHEN ISDATE(V.FDDateofExpiry)=1 THEN SUBSTRING(V.FDDateofExpiry,9,2)+'/'+SUBSTRING(V.FDDateofExpiry,6,2)+'/'+SUBSTRING(V.FDDateofExpiry,1,4) ELSE '' END AS FDDateofExpiry"
                    _Qry &= vbCrLf & "FROM    " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Visa AS V WITH (NOLOCK) LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee AS E WITH (NOLOCK) ON V.FNHSysEmpID=E.FNHSysEmpID "
                    _Qry &= vbCrLf & " WHERE E.FTEmpCode ='" & _FNHSysEmpID & "' AND V.FNVisaSeq = '" & _FNSeqId & "'"
                    _Qry &= vbCrLf & " ORDER BY V.FNVisaSeq desc"
                Case "Work"
                    _Qry = "SELECT   W.FNWorkpermitSeq,W.FTWorkpermitNo,W.FTWorkpermitNote,W.FBFileImage,E.FTEmpCode"
                    _Qry &= vbCrLf & ", CASE WHEN ISDATE(W.FDDateofIssue)=1 THEN SUBSTRING(W.FDDateofIssue,9,2)+'/'+SUBSTRING(W.FDDateofIssue,6,2)+'/'+SUBSTRING(W.FDDateofIssue,1,4) ELSE '' END AS FDDateofIssue"
                    _Qry &= vbCrLf & " , CASE WHEN ISDATE(W.FDDateofExpiry)=1 THEN SUBSTRING(W.FDDateofExpiry,9,2)+'/'+SUBSTRING(W.FDDateofExpiry,6,2)+'/'+SUBSTRING(W.FDDateofExpiry,1,4) ELSE '' END AS FDDateofExpiry"
                    _Qry &= vbCrLf & "FROM    " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Workpermit AS W WITH (NOLOCK) LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee AS E WITH (NOLOCK) ON W.FNHSysEmpID=E.FNHSysEmpID "
                    _Qry &= vbCrLf & " WHERE E.FTEmpCode ='" & _FNHSysEmpID & "' AND W.FNWorkpermitSeq = '" & _FNSeqId & "'"
                    _Qry &= vbCrLf & " ORDER BY W.FNWorkpermitSeq desc"
                Case "MOU"
                    _Qry = "SELECT    M.FNMOUSeq,M.FTMOUNo,M.FTMOUNote,M.FBFileImage,E.FTEmpCode"
                    _Qry &= vbCrLf & ", CASE WHEN ISDATE(M.FDDateofIssue)=1 THEN SUBSTRING(M.FDDateofIssue,9,2)+'/'+SUBSTRING(M.FDDateofIssue,6,2)+'/'+SUBSTRING(M.FDDateofIssue,1,4) ELSE '' END AS FDDateofIssue"
                    _Qry &= vbCrLf & " , CASE WHEN ISDATE(M.FDDateofExpiry)=1 THEN SUBSTRING(M.FDDateofExpiry,9,2)+'/'+SUBSTRING(M.FDDateofExpiry,6,2)+'/'+SUBSTRING(M.FDDateofExpiry,1,4) ELSE '' END AS FDDateofExpiry"
                    _Qry &= vbCrLf & "FROM    " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_MOU AS M WITH (NOLOCK) LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee AS E WITH (NOLOCK) ON M.FNHSysEmpID=E.FNHSysEmpID "
                    _Qry &= vbCrLf & " WHERE E.FTEmpCode ='" & _FNHSysEmpID & "' AND M.FNMOUSeq = '" & _FNSeqId & "'"
                    _Qry &= vbCrLf & " ORDER BY M.FNMOUSeq desc"
                Case "Other"
                    _Qry = "SELECT   O.FNFileOtherSeq,O.FTFileOtherNo,O.FTFileOtherNote,O.FBFileImage,E.FTEmpCode"
                    _Qry &= vbCrLf & ", CASE WHEN ISDATE(O.FDDateofIssue)=1 THEN SUBSTRING(O.FDDateofIssue,9,2)+'/'+SUBSTRING(O.FDDateofIssue,6,2)+'/'+SUBSTRING(O.FDDateofIssue,1,4) ELSE '' END AS FDDateofIssue"
                    _Qry &= vbCrLf & " , CASE WHEN ISDATE(O.FDDateofExpiry)=1 THEN SUBSTRING(O.FDDateofExpiry,9,2)+'/'+SUBSTRING(O.FDDateofExpiry,6,2)+'/'+SUBSTRING(O.FDDateofExpiry,1,4) ELSE '' END AS FDDateofExpiry"
                    _Qry &= vbCrLf & "FROM    " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_FileOther AS O WITH (NOLOCK) LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee AS E WITH (NOLOCK) ON O.FNHSysEmpID=E.FNHSysEmpID "
                    _Qry &= vbCrLf & " WHERE E.FTEmpCode ='" & _FNHSysEmpID & "' AND O.FNFileOtherSeq = '" & _FNSeqId & "'"
                    _Qry &= vbCrLf & " ORDER BY  O.FNFileOtherSeq desc"

            End Select


            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR,, False)

            Dim width As Integer = 100
            Dim height As Integer = 100
            For Each r In _oDt.Rows
                ' FTEmpPicName.Image = HI.UL.ULImage.LoadImage("" & r!FBFileImage)
                For Each Obj As Object In Me.Controls.Find("FTEmpPicName", True)
                    Dim oArrayOrderImage() As Byte = CType(r!FBFileImage, Byte())
                    If Not oArrayOrderImage Is Nothing And oArrayOrderImage.Length > 0 Then

                        Dim ms As New MemoryStream(oArrayOrderImage)
                        ' Dim fullSizeImg As System.Drawing.Image = System.Drawing.Image.FromStream(ms)

                        ' width = fullSizeImg.Width
                        ' height = fullSizeImg.Height

                        With Obj
                            .Image = Image.FromStream(ms)
                            '.Image = 
                            '.SizeMode = PictureBoxSizeMode.CenterImage

                        End With



                    Else
                        Obj.Image = Nothing
                    End If
                Next
            Next

            'Me.wEmployeeViewPic.Size = New Size(width, height)

        Catch ex As Exception
        End Try
    End Sub




    Private Sub ocmexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub
#End Region

End Class