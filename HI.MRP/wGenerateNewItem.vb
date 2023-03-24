Public Class wGenerateNewItem



    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private _ProcessProc As Boolean = False
    Public Property ProcessProc As Boolean
        Get
            Return _ProcessProc
        End Get
        Set(value As Boolean)
            _ProcessProc = value
        End Set
    End Property

    Private _FNHSysStyleDevId As Integer = 0
    Public Property SysStyleDevId As Integer
        Get
            Return _FNHSysStyleDevId
        End Get
        Set(value As Integer)
            _FNHSysStyleDevId = value
        End Set
    End Property

    Private _StateGenItem As Boolean = False
    Public Property StateGenItem As Boolean
        Get
            Return _StateGenItem
        End Get
        Set(value As Boolean)
            _StateGenItem = value
        End Set
    End Property

    Private Function SaveData(_dtnewitem As DataTable) As Boolean
        Dim _Spls As New HI.TL.SplashScreen("Generating Data. Please wait....")
        Dim _Qry As String = ""
        Dim _FNHSysMainMatId As Integer = 0
        Dim _FTMainMatCode As String = ""

        Try

            For Each R As DataRow In _dtnewitem.Rows

                _FNHSysMainMatId = HI.TL.RunID.GetRunNoID("TMERMMainMat", "FNHSysMainMatId", Conn.DB.DataBaseName.DB_MASTER)
                _FTMainMatCode = R!FNHSysMatGrpId.ToString & R!FNHSysMatTypeId.ToString & R!FNHSysCustId.ToString & R!FTItemNo.ToString

                If _FNHSysMainMatId > 0 Then
                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat("
                    _Qry &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FNHSysMainMatId, FTMainMatCode,"
                    _Qry &= vbCrLf & "FTMainMatNameTH, FTMainMatNameEN, FNHSysCustId, FTCusItemCodeRef,"
                    _Qry &= vbCrLf & "FNHSysMatGrpId, FNHSysMatTypeId, FNMerMatType,"
                    _Qry &= vbCrLf & " FNHSysSuplId, FTStateNominate, FNHSysUnitId, FNPrice, FNHSysCurId,"
                    _Qry &= vbCrLf & " FTStateDeadStock, FTRemark, FTStateActive, FTStateMainMaterial,"
                    _Qry &= vbCrLf & " FTFabricFrontSize, FNStateZeroInspection, FTStateNotCheckResuorce,"
                    _Qry &= vbCrLf & " FTStateHanger, FTStateOpenPR, FTStateSplitPO"
                    _Qry &= vbCrLf & " )"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & "," & _FNHSysMainMatId & ""
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTMainMatCode) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTItemDesc.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTItemDesc.ToString) & "'"
                    _Qry &= vbCrLf & "," & Val(R!FNHSysCustId_Hide.ToString) & ""
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTItemNo.ToString) & "'"
                    _Qry &= vbCrLf & "," & Val(R!FNHSysMatGrpId_Hide.ToString) & ""
                    _Qry &= vbCrLf & "," & Val(R!FNHSysMatTypeId_Hide.ToString) & ""
                    _Qry &= vbCrLf & "," & Val(R!FNMerMatType_Hide.ToString) & ""
                    _Qry &= vbCrLf & "," & Val(R!FNHSysSuplId_Hide.ToString) & ""
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTStateNominate.ToString) & "'"
                    _Qry &= vbCrLf & "," & Val(R!FNHSysUnitId_Hide.ToString) & ""
                    _Qry &= vbCrLf & "," & Val(R!FNPrice.ToString) & ""
                    _Qry &= vbCrLf & "," & Val(R!FNHSysCurId_Hide.ToString) & ""
                    _Qry &= vbCrLf & ",'0'"
                    _Qry &= vbCrLf & ",''"
                    _Qry &= vbCrLf & ",'1'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTStateMainMaterial.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTFabricFrontSize.ToString) & "'"
                    _Qry &= vbCrLf & ",0"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTStateNotCheckResuorce.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTStateHanger.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTStateOpenPR.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTStateSplitPO.ToString) & "'"

                    If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MASTER) = False Then

                        _Spls.Close()
                        Return True

                    Else
                        _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_Mat set FTItemREfNo='" & HI.UL.ULF.rpQuoted(_FTMainMatCode) & "'"
                        _Qry &= vbCrLf & " WHERE FNHSysStyleDevId =" & Me.SysStyleDevId & " AND FTItemNo='" & HI.UL.ULF.rpQuoted(R!FTItemNo.ToString) & "'  "

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                    End If
                End If
               
            Next

            StateGenItem = True
            _Spls.Close()
            Return True
        Catch ex As Exception


            _Spls.Close()
            Return False
        End Try
    End Function

    Private Sub ocmreceive_Click(sender As System.Object, e As System.EventArgs) Handles ocmok.Click
        Dim _dt As DataTable

        With CType(Me.ogcdetail.DataSource, DataTable)
            .AcceptChanges()
            _dt = .Copy()
        End With

        If _dt.Select("FTFabricFrontSize=''").Length > 0 Then
            HI.MG.ShowMsg.mInfo("พบข้อมูลบางรายการยังไม่ได้ทำการระบุ หน้าผ้า กรุณาทำการตรวจสอบ !!!", 1601110574, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            Exit Sub
        End If

        If _dt.Select("FNMerMatType=''").Length > 0 Then
            HI.MG.ShowMsg.mInfo("พบข้อมูลบางรายการยังไม่ได้ทำการระบุ ประเภท กรุณาทำการตรวจสอบ !!!", 1601110575, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            Exit Sub
        End If

        If _dt.Select("FNHSysMatGrpId=''").Length > 0 Then
            HI.MG.ShowMsg.mInfo("พบข้อมูลบางรายการยังไม่ได้ทำการระบุ กลุ่มวัตถุดิบ กรุณาทำการตรวจสอบ !!!", 1601110576, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            Exit Sub
        End If

        If _dt.Select("FNHSysMatTypeId=''").Length > 0 Then
            HI.MG.ShowMsg.mInfo("พบข้อมูลบางรายการยังไม่ได้ทำการระบุ ประเภทวัตถุดิบ กรุณาทำการตรวจสอบ !!!", 1601110577, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            Exit Sub
        End If

        If _dt.Select("FNHSysCustId=''").Length > 0 Then
            HI.MG.ShowMsg.mInfo("พบข้อมูลบางรายการยังไม่ได้ทำการระบุ ลูกค้า กรุณาทำการตรวจสอบ !!!", 1601110575, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            Exit Sub
        End If

        If _dt.Select("FNHSysUnitId=''").Length > 0 Then
            HI.MG.ShowMsg.mInfo("พบข้อมูลบางรายการยังไม่ได้ทำการระบุ หน่วยนับ กรุณาทำการตรวจสอบ !!!", 1601110575, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            Exit Sub
        End If

        If _dt.Select("FNHSysCurId=''").Length > 0 Then
            HI.MG.ShowMsg.mInfo("พบข้อมูลบางรายการยังไม่ได้ทำการระบุ สกุลเงิน กรุณาทำการตรวจสอบ !!!", 1601110575, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.SaveData(_dt) Then
            Me.ProcessProc = True
            Me.Close()
        Else
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        End If
       
    End Sub

    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmcancel.Click
        Me.ProcessProc = False
        Me.Close()
    End Sub

End Class