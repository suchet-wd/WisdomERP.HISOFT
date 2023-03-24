Imports DevExpress.XtraGrid.Views.Grid

Public Class wQCFabricTracking

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

#End Region


    Private Function Verrify()
        Try
            Dim _Pass As Boolean = False

            If Me.SDate.Text <> "" Then
                _Pass = True
            End If
            If Me.EDate.Text <> "" Then
                _Pass = True
            End If

            If Me.FTPurchaseNo.Text <> "" Then
                _Pass = True
            End If
            If Me.FTPurchaseNoTo.Text <> "" Then
                _Pass = True
            End If

            If Me.FTReceiveNo.Text <> "" Then
                _Pass = True
            End If
            If Me.FTReceiveNoTo.Text <> "" Then
                _Pass = True
            End If

            If Me.FDReceiveDate.Text <> "" Then
                _Pass = True
            End If
            If Me.FDReceiveDateTo.Text <> "" Then
                _Pass = True
            End If

            If Me.FNHSysRawMatId.Text <> "" Then
                _Pass = True
            End If


            If Not (_Pass) Then
                HI.MG.ShowMsg.mInfo("Pls. Enter Key Search...... ", 15052800011, Me.Text, "", System.Windows.Forms.MessageBoxIcon.Stop)
                Me.SDate.Focus()
            End If
            Return _Pass
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If Verrify() Then
                Call LoadData()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadData()
        Dim _Spls As New HI.TL.SplashScreen("Loading...")
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable

            _Cmd = "  Select Cmp.FTCmpCode"
            _Cmd &= vbCrLf & "  , A.FTQCFabNo "
            _Cmd &= vbCrLf & " , A.FTQCFabBy "
            _Cmd &= vbCrLf & " ,CASE WHEN ISDATE( A.FDQCFabDate) = 1 THEN CONVERT(Datetime, A.FDQCFabDate) ELSE NULL END AS FDQCFabDate "
            _Cmd &= vbCrLf & " , A.FTReceiveNo"
            _Cmd &= vbCrLf & " , A.FTPurchaseNo"
            _Cmd &= vbCrLf & " , IM.FTRawMatCode"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & " , IM.FTRawMatNameTH AS FTRawMatName"
                _Cmd &= vbCrLf & " , Cmp.FTCmpNameTH AS FTCmpName"
            Else
                _Cmd &= vbCrLf & " , IM.FTRawMatNameEN AS FTRawMatName"
                _Cmd &= vbCrLf & " , Cmp.FTCmpNameEN AS FTCmpName"
            End If

            _Cmd &= vbCrLf & " , ISNULL(IMC.FTRawMatColorCode,'') AS FTRawMatColorCode"
            _Cmd &= vbCrLf & " , ISNULL(IMS.FTRawMatSizeCode,'') As FTRawMatSizeCode"
            _Cmd &= vbCrLf & " , B.FTBatchNo"
            _Cmd &= vbCrLf & " , B.FTStateCutable"
            _Cmd &= vbCrLf & " , B.FTStateColorMatch"
            _Cmd &= vbCrLf & " , B.FTStateHandfeel"
            _Cmd &= vbCrLf & " , B.FTStateShading"
            _Cmd &= vbCrLf & " , B.FNTotalTicketed"
            _Cmd &= vbCrLf & " , B.FNTotalActual"
            _Cmd &= vbCrLf & " , B.FNDifference"
            _Cmd &= vbCrLf & " , B.FNTotalPoints"
            _Cmd &= vbCrLf & " , B.FNTotalDefects"
            _Cmd &= vbCrLf & " , B.FNTotalPointsPerUOM"
            _Cmd &= vbCrLf & " , B.FTShades	"
            _Cmd &= vbCrLf & " , CASE WHEN ISNUMERIC(C.FTRollNo) = 1 THEN  CONVERT(NUMERIC(18,0),FTRollNo) ELSE NULL END AS  FTRollNo "
            _Cmd &= vbCrLf & " , C.FNQuantity"
            _Cmd &= vbCrLf & " , C.FNActQuantity"
            _Cmd &= vbCrLf & " , C.FNYarn"
            _Cmd &= vbCrLf & " , C.FNContruction"
            _Cmd &= vbCrLf & " , C.FNDyeing"
            _Cmd &= vbCrLf & " , C.FNFinishing"
            _Cmd &= vbCrLf & " , C.FNCleanliness"

            _Cmd &= vbCrLf & " , C.FNYarn"
            _Cmd &= vbCrLf & " + C.FNContruction"
            _Cmd &= vbCrLf & " + C.FNDyeing"
            _Cmd &= vbCrLf & " + C.FNFinishing"
            _Cmd &= vbCrLf & " + C.FNCleanliness AS FNTotalDefect"

            _Cmd &= vbCrLf & " , C.FTFabricFrontSize"
            _Cmd &= vbCrLf & " , C.FTActFabricFrontSize"
            _Cmd &= vbCrLf & " , ISNULL(SH.FTNameTH,'-') AS FTRollShades"
            _Cmd &= vbCrLf & " , C.FTStateReject"
            _Cmd &= vbCrLf & " , A.FTStateAccept, A.FTAcceptUser"
            _Cmd &= vbCrLf & " ,CASE WHEN ISDATE( A.FDAcceptDate) = 1 THEN CONVERT(Datetime, A.FDAcceptDate) ELSE NULL END AS  FDAcceptDate"
            _Cmd &= vbCrLf & " , A.FTStateReject AS FTStateRejectDoc, A.FTRejectUser"
            _Cmd &= vbCrLf & " ,CASE WHEN ISDATE( A.FDRejectDate) = 1 THEN CONVERT(Datetime, A.FDRejectDate) ELSE NULL END AS  FDRejectDate"
            _Cmd &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat As B With(NOLOCK) INNER Join"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric As A With(NOLOCK) On B.FTQCFabNo = A.FTQCFabNo INNER Join"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As IM With(NOLOCK)  On B.FNHSysRawMatId = IM.FNHSysRawMatId INNER Join"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat_Detail As C With(NOLOCK) On B.FTQCFabNo = C.FTQCFabNo And B.FNHSysRawMatId = C.FNHSysRawMatId And B.FTBatchNo = C.FTBatchNo  LEFT OUTER Join"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize As IMS With(NOLOCK) On IM.FNHSysRawMatSizeId = IMS.FNHSysRawMatSizeId LEFT OUTER Join"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor As IMC With(NOLOCK) On IM.FNHSysRawMatColorId = IMC.FNHSysRawMatColorId LEFT OUTER Join"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS RE With(NOLOCK) On A.FTReceiveNo=RE.FTReceiveNo LEFT OUTER Join"
            _Cmd &= vbCrLf & "  (Select FNListIndex, FTNameTH, FTNameEN From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData Where (FTListName = N'FNShades')) AS SH ON ISNULL(C.FTShades,0) = SH.FNListIndex"
            _Cmd &= vbCrLf & "  OUTER APPLY (SELECT FTCmpCode,FTCmpNameTH,FTCmpNameEN FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp As Cmp WITH(NOLOCK) WHERE  Cmp.FNHSysCmpId =A.FNHSysCmpId  ) AS Cmp "

            _Cmd &= vbCrLf & "WHERE A.FTQCFabNo <> ''"

            If Me.SDate.Text <> "" Then
                _Cmd &= vbCrLf & "AND A.FDQCFabDate >='" & HI.UL.ULDate.ConvertEnDB(Me.SDate.Text) & "'"
            End If
            If Me.EDate.Text <> "" Then
                _Cmd &= vbCrLf & "AND A.FDQCFabDate <='" & HI.UL.ULDate.ConvertEnDB(Me.EDate.Text) & "'"
            End If
            If Me.FTPurchaseNo.Text <> "" Then
                _Cmd &= vbCrLf & "AND A.FTPurchaseNo >='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
            End If
            If Me.FTPurchaseNoTo.Text <> "" Then
                _Cmd &= vbCrLf & "AND A.FTPurchaseNo <='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNoTo.Text) & "'"
            End If
            If Me.FTReceiveNo.Text <> "" Then
                _Cmd &= vbCrLf & "AND A.FTReceiveNo >='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'"
            End If
            If Me.FTReceiveNoTo.Text <> "" Then
                _Cmd &= vbCrLf & "AND A.FTReceiveNo <='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNoTo.Text) & "'"
            End If
            If Me.FDReceiveDate.Text <> "" Then
                _Cmd &= vbCrLf & "AND RE.FDReceiveDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FDReceiveDate.Text) & "'"
            End If
            If Me.FDReceiveDateTo.Text <> "" Then
                _Cmd &= vbCrLf & "AND RE.FDReceiveDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FDReceiveDateTo.Text) & "'"
            End If
            If Me.FNHSysRawMatId.Text <> "" Then
                _Cmd &= vbCrLf & "AND IM.FTRawMatCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysRawMatId.Text) & "'"
            End If


            _Cmd &= vbCrLf & " ORDER BY  A.FTQCFabNo,B.FTBatchNo,CASE WHEN ISNUMERIC(C.FTRollNo) = 1 THEN  CONVERT(NUMERIC(18,0),FTRollNo) ELSE NULL END"

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_INVEN)
            Me.ogcDetail.DataSource = _oDt

            _Spls.Close()

        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Try
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvDetail_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles ogvDetail.RowStyle
        Try
            With ogvDetail
                'If .GetRowCellValue(e.RowHandle, "FTStateAccept").ToString = "0" Then
                '    e.Appearance.BackColor = System.Drawing.Color.LightSalmon
                '    e.Appearance.BackColor2 = System.Drawing.Color.WhiteSmoke
                'End If
                'If .GetRowCellValue(e.RowHandle, "FTStateSendApp").ToString = "0" Then
                '    e.Appearance.BackColor = System.Drawing.Color.LightGray
                '    e.Appearance.BackColor2 = System.Drawing.Color.WhiteSmoke
                'End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvDetail_CellMerge(sender As Object, e As CellMergeEventArgs) Handles ogvDetail.CellMerge
        Try
            Select Case e.Column.FieldName
                Case ""
                Case Else

                    With ogvDetail
                        If "" & .GetRowCellValue(e.RowHandle1, "FTQCFabNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTQCFabNo").ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    End With

            End Select
        Catch ex As Exception

        End Try
    End Sub
End Class