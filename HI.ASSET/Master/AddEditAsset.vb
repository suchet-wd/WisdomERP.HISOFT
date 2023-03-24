Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports System.IO
Imports System.Data.SqlClient

Public Class AddEditAsset

    Sub New(ByVal tParentForm As Object)

        ' This call is required by the designer.
        InitializeComponent()
        Me.Parent_Form = tParentForm

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#Region "Property"
    Private _ActiveLang As HI.ST.Lang.eLang = -1
    Public Property ActiveLang As HI.ST.Lang.eLang
        Get
            Return _ActiveLang
        End Get
        Set(ByVal value As HI.ST.Lang.eLang)
            _ActiveLang = value
        End Set
    End Property
    Private _ProcComplete As Boolean = False
    Public Property ProcComplete As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(ByVal value As Boolean)
            _ProcComplete = value
        End Set
    End Property

    Private _Parent_Form As Object
    Public Property Parent_Form As Object
        Get
            Return _Parent_Form
        End Get
        Set(ByVal value As Object)
            _Parent_Form = value
        End Set
    End Property

#End Region

#Region "Proceducre"
    Private Function InsertData() As Boolean
        Dim Qry As String = ""
        Dim _ID As String = ""
        Dim _StateActive As String = ""
        Dim _StateCritical As String = ""
        Dim _RunNumber As String = ""
        'Dim ms As New MemoryStream()
        Dim _DataPic As Byte()

        If Me.FTStateActive.Checked Then
            _StateActive = "1"
        Else
            _StateActive = "0"
        End If
        If Me.FTStateCritical.Checked Then
            _StateCritical = "1"
        Else
            _StateCritical = "0"
        End If


        Try
            'If FPImage.Image Is Nothing Then
            '    _DataPic = Nothing
            'Else
            '    Me.FPImage.Image.Save(ms, Imaging.ImageFormat.Jpeg)
            '    '_DataPic = ms.GetBuffer()
            'End If

            _DataPic = UL.ULImage.ConvertImageToByteArray(Me.FPImage.Image, UL.ULImage.PicType.Employee)
            _ID = HI.TL.RunID.GetRunNoID("" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "..TASMAsset", "FNHSysFixedAssetId", Conn.DB.DataBaseName.DB_MASTER)
            Dim _FT As String = HI.Conn.SQLConn.GetField("select L.FTReferCode   from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData as L where L.FTListName ='FNFixedAssetType' and FNListIndex='" & Me.FNFixedAssetType.SelectedIndex & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
            Qry = "select max(A.FTAssetCode) AS MaxCode from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A WITH(NOLOCK) where A.FTAssetCode like '%" & _FT & Me.FNHSysAssetGrpId.Text & Me.FNHSysAssetTyped.Text & "%'"
            Dim _AsstCode As String = HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER)
            If _AsstCode = "" Then
                _RunNumber = "00001"
            Else
                _RunNumber = Format(Val(Microsoft.VisualBasic.Right(_AsstCode, 5)) + 1, "00000")
            End If
            Me.FTAssetCode.Text = _FT & Me.FNHSysAssetGrpId.Text & Me.FNHSysAssetTyped.Text & _RunNumber


            'If Me.FTAssetCode.Text = "" Then
            '    If Me.FNFixedAssetType.SelectedIndex <> 1 Then
            '        Me.FTAssetCode.Text = _FT & Me.FNHSysAssetGrpId.Text & Me.FNHSysAssetTyped.Text & Format(Microsoft.VisualBasic.Right(_AsstCode, 5) + 1, "00000")
            '    Else
            '        Me.FTAssetCode.Text = _FT & Me.FNHSysAssetGrpId.Text & Me.FNHSysAssetTyped.Text & Format(Microsoft.VisualBasic.Right(_AsstCode, 5) + 1, "00000")
            '    End If
            'End If
            If VerifyBarcode(Me.FTAssetCode.Text) Then
                If VerifyAssetCode(Me.FTAssetCode.Text) Then

                    HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MASTER)
                    HI.Conn.SQLConn.SqlConnectionOpen()
                    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
                    Qry = "insert into [" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset(FTInsUser, FDInsDate, FTInsTime, FNHSysFixedAssetId, FNHSysCmpId, FNFixedAssetType, FTAssetCode, FTAssetNameTH, FTAssetNameEN, "
                    Qry &= vbCrLf & "FNHSysAssetModelId, FNHSysAssetBrandId, FTSerialNo,FTRefer, FNHSysAssetGrpId, FNHSysAssetTyped, FTProductCode, FNHSysSuplId, FNPrice, FDDateAdd, FDDateUsed, FNLifetime, FNLifetimeType, "
                    Qry &= vbCrLf & "FDDateStartWarranty, FDDateEndWarranty, FNMaxPower, FNHSysUnitSectId, FNHSysEmpID, FNHSysCurId, FTPurchaseNo, FDPurchaseDate, FTPurchaseBy, FTInvoiceNo, FDInvoiceDate, FTReceiveNo, "
                    Qry &= vbCrLf & "FDReceiveDate, FTReceiveBy, FNMinimumStock, FNMaximumStock, FTRemark, FTStateActive, FTStateCritical,FNHSysUnitAssetId,FTLocationAsset)"
                    Qry &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & "," & Val(_ID) & "," & Me.FNHSysCmpId.Properties.Tag & ""
                    'If FNFixedAssetType.SelectedIndex > 0 Then
                    '    Qry &= vbCrLf & "," & Me.FNFixedAssetType.SelectedIndex + 1 & ""
                    'Else
                    '    Qry &= vbCrLf & "," & Me.FNFixedAssetType.SelectedIndex & ""
                    'End If
                    Qry &= vbCrLf & "," & Me.FNFixedAssetType.SelectedIndex & ""
                    Qry &= vbCrLf & ",'" & UL.ULF.rpQuoted(Me.FTAssetCode.Text) & "','" & UL.ULF.rpQuoted(Me.FTAssetNameTH.Text) & "','" & UL.ULF.rpQuoted(Me.FTAssetNameEN.Text) & "'"
                    Qry &= vbCrLf & "," & Me.FNHSysAssetModelId.Properties.Tag & "," & Me.FNHSysAssetBrandId.Properties.Tag & ",'" & UL.ULF.rpQuoted(Me.FTSerialNo.Text) & "','" & UL.ULF.rpQuoted(Me.FTRefer.Text) & "'," & Me.FNHSysAssetGrpId.Properties.Tag & ""
                    Qry &= vbCrLf & "," & Me.FNHSysAssetTyped.Properties.Tag & ",'" & UL.ULF.rpQuoted(Me.FTProductCode.Text) & "'," & IIf(Me.FNHSysSuplId.Properties.Tag.ToString <> "", Me.FNHSysSuplId.Properties.Tag, 0) & "," & Me.FNPrice.Value & ""
                    Qry &= vbCrLf & ",'" & UL.ULDate.ConvertEnDB(Me.FDDateAdd.Text) & "','" & UL.ULDate.ConvertEnDB(Me.FDDateUsed.Text) & "'," & Me.FNLifetime.Value & "," & Me.FNLifetimeType.SelectedIndex & ""
                    Qry &= vbCrLf & ",'" & UL.ULDate.ConvertEnDB(Me.FDDateStartWarranty.Text) & "','" & UL.ULDate.ConvertEnDB(Me.FDDateEndWarranty.Text) & "'," & Me.FNMaxPower.Value & "," & IIf(Me.FNHSysUnitSectId.Properties.Tag.ToString <> "", Me.FNHSysUnitSectId.Properties.Tag, 0) & ""
                    Qry &= vbCrLf & "," & IIf(Me.FNHSysEmpID.Properties.Tag.ToString <> "", Me.FNHSysEmpID.Properties.Tag, 0) & "," & Me.FNHSysCurId.Properties.Tag & ",'" & UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "','" & UL.ULDate.ConvertEnDB(Me.FDPurchaseDate.Text) & "'"
                    Qry &= vbCrLf & ",'" & UL.ULF.rpQuoted(Me.FTPurchaseBy.Text) & "','" & UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "','" & UL.ULDate.ConvertEnDB(Me.FDInvoiceDate.Text) & "','" & UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'"
                    Qry &= vbCrLf & ",'" & UL.ULDate.ConvertEnDB(Me.FDReceiveDate.Text) & "','" & UL.ULF.rpQuoted(Me.FTReceiveBy.Text) & "'," & Me.FNMinimumStock.Value & "," & Me.FNMaximumStock.Value & ",'" & Me.FTRemark.Text & "'"
                    Qry &= vbCrLf & ",'" & _StateActive & "','" & _StateCritical & "'," & FNHSysUnitAssetId.Properties.Tag & ",'" & UL.ULF.rpQuoted(Me.FTLocationAsset.Text) & "'"
                    If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) > 0 Then
                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        If Not (_DataPic Is Nothing) Then
                            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MASTER)
                            HI.Conn.SQLConn.SqlConnectionOpen()
                            Qry = "update[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset SET FPImage=@FPImage where FNHSysFixedAssetId=@ID"
                            Dim cmd As New SqlCommand(Qry, HI.Conn.SQLConn.Cnn)
                            cmd.Parameters.AddWithValue("@ID", Val(_ID))
                            Dim p As New SqlParameter("@FPImage", SqlDbType.Image)
                            p.Value = _DataPic
                            cmd.Parameters.Add(p)
                            cmd.ExecuteNonQuery()
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)
                        End If
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        Me.ProcComplete = True
                        Return True
                    Else
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        Me.ProcComplete = False
                        Return False
                    End If
                Else
                    MG.ShowMsg.mInfo("ข้อมูลเลขที่สินทรัพย์นี้มีอยู่แล้ว", 201611101536, Me.Text)
                    Me.FTAssetCode.Focus()
                    Return False
                End If
            Else
                MG.ShowMsg.mInfo("มีบาร์โค๊ดนี้อยู่แล้ว!!!", 201610201137, Me.Text)
                Me.FTAssetCode.Focus()
                Return False
            End If
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Me.ProcComplete = False
            Return False
        End Try
    End Function

    Private Sub UpdateData()
        Dim Qry As String = ""
        Dim _ID As String = ""
        Dim _StateActive As String = ""
        Dim _StateCritical As String = ""
        Dim ms As New MemoryStream()
        Dim _DataPic As Byte()

        If Me.FTStateActive.Checked Then
            _StateActive = "1"
        Else
            _StateActive = "0"
        End If
        If Me.FTStateCritical.Checked Then
            _StateCritical = "1"
        Else
            _StateCritical = "0"
        End If
        Try
            'If FPImage.Image Is Nothing Then
            '    _DataPic = Nothing
            'Else
            'Me.FPImage.Image.Save(ms, Imaging.ImageFormat.Jpeg)
            '_DataPic = ms.GetBuffer()
            _DataPic = UL.ULImage.ConvertImageToByteArray(Me.FPImage.Image, UL.ULImage.PicType.Employee)
            'End If

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MASTER)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            'Dim _FTAssetCode As String = ""
            'Dim _AsstCode As String = ""
            'Dim _RunNumber As String = ""
            'Dim _FT As String = HI.Conn.SQLConn.GetField("select L.FTReferCode   from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData as L where L.FTListName ='FNFixedAssetType'  and FTNameTH='" & Me.FNFixedAssetType.Text & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "") ' and FNListIndex='" & Me.FNFixedAssetType.SelectedIndex & "'
            '_FTAssetCode = HI.Conn.SQLConn.GetField("select max(A.FTAssetCode) AS MaxCode from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TASMAsset AS A WITH(NOLOCK) where A.FTAssetCode like '% " & _FT & Me.FNHSysAssetGrpId.Text & Me.FNHSysAssetTyped.Text & " %'", Conn.DB.DataBaseName.DB_FIXED)


            'If Me.FNFixedAssetType.SelectedIndex <> 1 Then
            '    _FTAssetCode = _FT & Me.FNHSysAssetGrpId.Text & Me.FNHSysAssetTyped.Text & Format(Microsoft.VisualBasic.Right(_AsstCode, 5) + 1, "00000")
            'Else
            '    _FTAssetCode = _FT & Me.FNHSysAssetGrpId.Text & Me.FNHSysAssetTyped.Text & Format(Microsoft.VisualBasic.Right(_AsstCode, 5) + 1, "00000")
            'End If


            Qry = "update [" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset SET FTUpdUser='" & ST.UserInfo.UserName & "'"
            Qry &= vbCrLf & ", FDUpdDate=" & UL.ULDate.FormatDateDB & ", FTUpdTime=" & UL.ULDate.FormatTimeDB & ""
            Qry &= vbCrLf & ", FNHSysCmpId=" & Me.FNHSysCmpId.Properties.Tag & ""
            'If FNFixedAssetType.SelectedIndex > 0 Then
            '    Qry &= vbCrLf & ", FNFixedAssetType=" & Me.FNFixedAssetType.SelectedIndex + 1 & ""
            'Else
            '    Qry &= vbCrLf & ", FNFixedAssetType=" & Me.FNFixedAssetType.SelectedIndex & ""
            'End If
            Qry &= vbCrLf & ", FNFixedAssetType=" & Me.FNFixedAssetType.SelectedIndex & ""
            Qry &= vbCrLf & ", FTAssetNameTH='" & UL.ULF.rpQuoted(Me.FTAssetNameTH.Text) & "', FTAssetNameEN='" & UL.ULF.rpQuoted(Me.FTAssetNameEN.Text) & "'"
            Qry &= vbCrLf & ",FNHSysAssetModelId=" & Me.FNHSysAssetModelId.Properties.Tag & ", FNHSysAssetBrandId=" & Me.FNHSysAssetBrandId.Properties.Tag & ""
            Qry &= vbCrLf & ", FTSerialNo='" & UL.ULF.rpQuoted(Me.FTSerialNo.Text) & "',FTRefer='" & UL.ULF.rpQuoted(Me.FTRefer.Text) & "', FNHSysAssetGrpId=" & Me.FNHSysAssetGrpId.Properties.Tag & ", FNHSysAssetTyped=" & Me.FNHSysAssetTyped.Properties.Tag & ""
            Qry &= vbCrLf & ", FTProductCode='" & UL.ULF.rpQuoted(Me.FTProductCode.Text) & "', FNHSysSuplId=" & IIf(Me.FNHSysSuplId.Properties.Tag.ToString <> "", Me.FNHSysSuplId.Properties.Tag, 0) & ""
            Qry &= vbCrLf & ", FNPrice=" & Me.FNPrice.Value & ", FDDateAdd='" & UL.ULDate.ConvertEnDB(Me.FDDateAdd.Text) & "', FDDateUsed='" & UL.ULDate.ConvertEnDB(Me.FDDateUsed.Text) & "'"
            Qry &= vbCrLf & ", FNLifetime=" & Me.FNLifetime.Value & ", FNLifetimeType=" & Me.FNLifetimeType.SelectedIndex & ",FDDateStartWarranty='" & UL.ULDate.ConvertEnDB(Me.FDDateStartWarranty.Text) & "'"
            Qry &= vbCrLf & ", FDDateEndWarranty='" & UL.ULDate.ConvertEnDB(Me.FDDateEndWarranty.Text) & "', FNMaxPower=" & Me.FNMaxPower.Value & ""
            Qry &= vbCrLf & ", FNHSysUnitSectId=" & IIf(Me.FNHSysUnitSectId.Properties.Tag.ToString <> "", Me.FNHSysUnitSectId.Properties.Tag, 0) & ", FNHSysEmpID=" & IIf(Me.FNHSysEmpID.Properties.Tag.ToString <> "", Me.FNHSysEmpID.Properties.Tag, 0) & ", FNHSysCurId=" & Me.FNHSysCurId.Properties.Tag & ""
            Qry &= vbCrLf & ", FTPurchaseNo='" & UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "', FDPurchaseDate='" & UL.ULDate.ConvertEnDB(Me.FDPurchaseDate.Text) & "'"
            Qry &= vbCrLf & ", FTPurchaseBy='" & UL.ULF.rpQuoted(Me.FTPurchaseBy.Text) & "', FTInvoiceNo='" & UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "', FDInvoiceDate='" & UL.ULDate.ConvertEnDB(Me.FDInvoiceDate.Text) & "'"
            Qry &= vbCrLf & ", FTReceiveNo='" & UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "',FDReceiveDate='" & UL.ULDate.ConvertEnDB(Me.FDReceiveDate.Text) & "', FTReceiveBy='" & UL.ULF.rpQuoted(Me.FTReceiveBy.Text) & "'"
            Qry &= vbCrLf & ", FNMinimumStock=" & Me.FNMinimumStock.Value & ", FNMaximumStock=" & Me.FNMaximumStock.Value & ", FTRemark='" & Me.FTRemark.Text & "', FTStateActive='" & _StateActive & "', FTStateCritical='" & _StateCritical & "'"
            Qry &= vbCrLf & ", FNHSysUnitAssetId =" & Me.FNHSysUnitAssetId.Properties.Tag & ""
            Qry &= vbCrLf & ", FTLocationAsset ='" & UL.ULF.rpQuoted(Me.FTLocationAsset.Text) & "'"
            Qry &= vbCrLf & "WHERE FNHSysFixedAssetId=" & Me.FTAssetCode.Properties.Tag & ""

            If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) > 0 Then
                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MASTER)
                HI.Conn.SQLConn.SqlConnectionOpen()
                Qry = "update[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset Set FPImage=@FPImage where FNHSysFixedAssetId=@ID"
                Dim cmd As New SqlCommand(Qry, HI.Conn.SQLConn.Cnn)
                cmd.Parameters.AddWithValue("@ID", Me.FTAssetCode.Properties.Tag)

                Dim p As New SqlParameter("@FPImage", SqlDbType.Image)
                p.Value = _DataPic
                If _DataPic Is Nothing Then
                    cmd.Parameters.Add(p).Value = DBNull.Value
                Else
                    cmd.Parameters.Add(p)
                End If
                cmd.ExecuteNonQuery()
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)

                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Me.ProcComplete = True
            Else
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Me.ProcComplete = False
            End If
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Me.ProcComplete = False
        End Try
    End Sub

    Private Sub DeleteData()
        Dim Qry As String = ""
        Try
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MASTER)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Qry = "delete [" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset where FNHSysFixedAssetId=" & Me.FTAssetCode.Properties.Tag & ""
            If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) > 0 Then
                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                Me.ProcComplete = True
            Else
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                Me.ProcComplete = False
            End If
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            Me.ProcComplete = False
        End Try
    End Sub

    Private Sub ClearControl()
        TL.HandlerControl.ClearControl(Me)
    End Sub
#End Region

#Region "Function"
    Private Function Verify() As Boolean
        If Me.FNHSysCmpId.Text <> "" Then


            If Me.FDDateAdd.Text <> "" Then
                If Me.FTAssetNameTH.Text <> "" Then
                    If Me.FTAssetNameEN.Text <> "" Then
                        If Me.FNHSysAssetModelId.Text <> "" Then
                            If Me.FNHSysAssetBrandId.Text <> "" Then
                                'If Me.FTRefer.Text <> "" Then
                                If Me.FNHSysAssetGrpId.Text <> "" Then
                                    If Me.FNHSysAssetTyped.Text <> "" Then
                                        'If Me.FNHSysUnitSectId.Text <> "" Then
                                        'If Me.FNHSysEmpID.Text <> "" Then
                                        'If Me.FNHSysSuplId.Text <> "" Then
                                        '  If Me.FNPrice.Value > 0 Then
                                        If Me.FNHSysCurId.Text <> "" Then
                                            If Me.FNHSysUnitAssetId.Text <> "" Then
                                                'If Me.FTPurchaseNo.Text <> "" Then
                                                '    If Me.FDPurchaseDate.Text <> "" Then
                                                '        If Me.FTPurchaseBy.Text <> "" Then
                                                '            If Me.FTInvoiceNo.Text <> "" Then
                                                '                If Me.FDInvoiceDate.Text <> "" Then
                                                '                    If Me.FTReceiveNo.Text <> "" Then
                                                '                        If Me.FDReceiveDate.Text <> "" Then
                                                '                            If Me.FTReceiveBy.Text <> "" Then
                                                '                                Return True
                                                '                            Else
                                                '                                MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTReceiveBy_lbl.Text)
                                                '                                Me.FTReceiveBy.Focus()
                                                '                                Return False
                                                '                            End If
                                                '                        Else
                                                '                            MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FDReceiveDate_lbl.Text)
                                                '                            Me.FDReceiveDate.Focus()
                                                '                            Return False
                                                '                        End If
                                                '                    Else
                                                '                        MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTReceiveNo_lbl.Text)
                                                '                        Me.FTReceiveNo.Focus()
                                                '                        Return False
                                                '                    End If
                                                '                Else
                                                '                    MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FDInvoiceDate_lbl.Text)
                                                '                    Me.FDInvoiceDate.Focus()
                                                '                    Return False
                                                '                End If
                                                '            Else
                                                '                MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTInvoiceNo_lbl.Text)
                                                '                Me.FTInvoiceNo.Focus()
                                                '                Return False
                                                '            End If
                                                '        Else
                                                '            MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTPurchaseBy_lbl.Text)
                                                '            Me.FTPurchaseBy.Focus()
                                                '            Return False
                                                '        End If
                                                '    Else
                                                '        MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FDPurchaseDate_lbl.Text)
                                                '        Me.FDPurchaseDate.Focus()
                                                '        Return False
                                                '    End If
                                                'Else
                                                '    MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTPurchaseNo_lbl.Text)
                                                '    Me.FTPurchaseNo.Focus()
                                                '    Return False
                                                'End If
                                                Return True
                                            Else
                                                MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysUnitAssetId_lbl.Text)
                                                Me.FNHSysUnitAssetId.Focus()
                                                Return False
                                            End If


                                        Else
                                            MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysCurId_lbl.Text)
                                            Me.FNHSysCurId.Focus()
                                            Return False
                                        End If
                                        'Else
                                        '    MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNPrice_lbl.Text)
                                        '    Me.FNPrice.Focus()
                                        '    Return False
                                        'End If
                                        'Else
                                        '    MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysSuplId_lbl.Text)
                                        '    Me.FNHSysSuplId.Focus()
                                        '    Return False
                                        'End If
                                        'Else
                                        '        MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysEmpID_lbl.Text)
                                        '        Me.FNHSysEmpID.Focus()
                                        '        Return False
                                        '    End If
                                        'Else
                                        '        MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysUnitSectId_lbl.Text)
                                        '        Me.FNHSysUnitSectId.Focus()
                                        '        Return False
                                        '    End If
                                    Else
                                        MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysAssetTyped_lbl.Text)
                                        Me.FNHSysAssetTyped.Focus()
                                        Return False
                                    End If
                                Else
                                    MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysAssetGrpId_lbl.Text)
                                    Me.FNHSysAssetGrpId.Focus()
                                    Return False
                                End If
                                'Else
                                '    MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTRefer_lbl.Text)
                                '    Me.FTRefer.Focus()
                                '    Return False
                                'End If
                            Else
                                MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysAssetBrandId_lbl.Text)
                                Me.FNHSysAssetBrandId.Focus()
                                Return False
                            End If
                        Else
                            MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysAssetModelId_lbl.Text)
                            Me.FNHSysAssetModelId.Focus()
                            Return False
                        End If
                    Else
                        MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTAssetNameEN_lbl.Text)
                        Me.FTAssetNameEN.Focus()
                        Return False
                    End If
                Else
                    MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTAssetNameTH_lbl.Text)
                    Me.FTAssetNameTH.Focus()
                    Return False
                End If
            Else
                MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FDDateAdd_lbl.Text)
                Me.FDDateAdd.Focus()
                Return False
            End If
        Else
            MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysCmpId_lbl.Text)
            Me.FNHSysCmpId.Focus()
            Return False
        End If

    End Function

#End Region

#Region "Event"
    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.ProcComplete = False
        Me.Close()
    End Sub

    Private Sub ocmaddnew_Click(sender As Object, e As EventArgs) Handles ocmaddnew.Click
        Try
            If Verify() Then
                If InsertData() Then
                    Call ClearControl()
                    Parent_Form.LoadData()
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try
            If MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, Me.FTAssetCode.Text) Then
                Call DeleteData()
                Me.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmedit_Click(sender As Object, e As EventArgs) Handles ocmedit.Click
        If Verify() Then
            Call UpdateData()
            Me.Close()
        End If
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Sub AddEditAsset_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim _ListIdx As Integer = 0
        If Me.ActiveLang <> HI.ST.Lang.Language Then
            HI.ST.Lang.SP_SETxLanguage(Me)
            Me.ActiveLang = HI.ST.Lang.Language
        End If
        RemoveHandler Me.FTPurchaseNo.Leave, AddressOf TL.HandlerControl.DynamicButtonedit_LeaveOnly
        RemoveHandler Me.FTPurchaseNo.EditValueChanged, AddressOf TL.HandlerControl.DynamicButtonedit_EditValueChanged
        RemoveHandler Me.FTReceiveNo.Leave, AddressOf TL.HandlerControl.DynamicButtonedit_LeaveOnly
        RemoveHandler Me.FTReceiveNo.EditValueChanged, AddressOf TL.HandlerControl.DynamicButtonedit_EditValueChanged
        Me.Icon = Parent_Form.Icon

        'FNFixedAssetType.Properties.Items.Clear()
        'For Each _str As String In TL.CboList.SetList("FNFixedAssetType")
        '    If _ListIdx <> 1 Then
        '        FNFixedAssetType.Properties.Items.Add(_str)
        '    End If
        '    _ListIdx += 1
        'Next

        Me.FTAssetCode.ReadOnly = True

    End Sub
#End Region



    Private Function VerifyBarcode(_Barcode As String) As Boolean
        Dim Qry As String = ""

        Qry = "Select top 1 FTBarcodeNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode As B With(NOLOCK) WHERE B.FTBarcodeNo='" & _Barcode & "'"
        If (HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED, "") = "") Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function VerifyAssetCode(_AssetCode As String) As Boolean
        Dim Qry As String = ""
        Qry = "select top 1 FTAssetCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A WITH(NOLOCK) WHERE A.FTAssetCode='" & _AssetCode & "'"
        If (HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER, "") = "") Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function GetTypeAsset(PositType As Integer) As String
        Dim _Type As String() = {"M", "SN", "P", "O", "E", "S", "H", "A"}
        Try
            Return _Type(PositType)
        Catch ex As Exception
            Return ""
        End Try

    End Function
    Private Sub FNFixedAssetType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNFixedAssetType.SelectedIndexChanged
        Try
            If FNFixedAssetType.SelectedIndex = 1 Then
                MG.ShowMsg.mInfo("ไม่สามารถเลือกข้อมูลนี้ได้", 1705121623, Me.Text)
                FNFixedAssetType.SelectedIndex = 0
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FTPurchaseNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTPurchaseNo.EditValueChanged

    End Sub
End Class