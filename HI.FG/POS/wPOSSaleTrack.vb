
Imports System.Drawing
Imports DevExpress.XtraGrid.Views.Grid

Public Class wPOSSaleTrack

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub wPOSSaleTrack_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadData()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "SELECT        S.FTInvoiceNo, O.FTPORef,  D.FTBarcodeCustNo, D.FNQuantity, D.FNPrice, D.FNHSysWHFGId, D.FTOrderNo, D.FTColorway, D.FTSizeBreakDown  "
            _Cmd &= vbCrLf & ",CASE WHEN ISDATE(S.FDInvoiceDate) = 1 THEN Convert(varchar(10),convert(datetime,S.FDInvoiceDate),103) Else '' END AS FDInvoiceDate "
            _Cmd &= vbCrLf & ",   T.FNHSysStyleId, T.FTStyleCode , (D.FNQuantity * D.FNPrice) AS FNTotal "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ",  T.FTStyleNameTH AS FTStyleName"
            Else
                _Cmd &= vbCrLf & ",  T.FTStyleNameTH AS FTStyleName"
            End If

            _Cmd &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale AS S WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail AS D WITH (NOLOCK) ON S.FTInvoiceNo = D.FTInvoiceNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON D.FTOrderNo = O.FTOrderNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS T WITH (NOLOCK) ON O.FNHSysStyleId = T.FNHSysStyleId"
            _Cmd &= vbCrLf & "WHERE S.FTInvoiceNo <> ''"
            If Me.SFTDateTrans.Text <> "" Then
                _Cmd &= vbCrLf & " And S.FDInvoiceDate >='" & HI.UL.ULDate.ConvertEnDB(Me.SFTDateTrans.Text) & "'"
            End If
            If Me.EFTDateTrans.Text <> "" Then
                _Cmd &= vbCrLf & " And S.FDInvoiceDate <='" & HI.UL.ULDate.ConvertEnDB(Me.EFTDateTrans.Text) & "'"
            End If
            If Me.FTOrderNo.Text <> "" Then
                _Cmd &= vbCrLf & " And D.FTOrderNo >='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            End If
            If Me.FTOrderNoTo.Text <> "" Then
                _Cmd &= vbCrLf & " And D.FTOrderNo <='" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
            End If
            If Me.FNHSysStyleId.Text <> "" Then
                _Cmd &= vbCrLf & " And T.FTStyleCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'"
            End If
            If Me.FNHSysStyleIdTo.Text <> "" Then
                _Cmd &= vbCrLf & " And T.FTStyleCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleIdTo.Text) & "'"
            End If
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_FG)
            Me.ogcdetail.DataSource = _oDt


            _Cmd = "SELECT      sum(  D.FNQuantity ) AS FNQuantity   "
            _Cmd &= vbCrLf & ",CASE WHEN ISDATE(S.FDInvoiceDate) = 1 THEN Convert(varchar(10),convert(datetime,S.FDInvoiceDate),103) Else '' END AS FDInvoiceDate "
            _Cmd &= vbCrLf & ",    T.FTStyleCode ,sum (D.FNQuantity * D.FNPrice) AS FNTotal "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", max( T.FTStyleNameTH ) AS FTStyleName"
            Else
                _Cmd &= vbCrLf & ", max( T.FTStyleNameTH ) AS FTStyleName"
            End If

            _Cmd &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale AS S WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail AS D WITH (NOLOCK) ON S.FTInvoiceNo = D.FTInvoiceNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON D.FTOrderNo = O.FTOrderNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS T WITH (NOLOCK) ON O.FNHSysStyleId = T.FNHSysStyleId"
            _Cmd &= vbCrLf & "WHERE S.FTInvoiceNo <> ''"
            If Me.SFTDateTrans.Text <> "" Then
                _Cmd &= vbCrLf & " And S.FDInvoiceDate >='" & HI.UL.ULDate.ConvertEnDB(Me.SFTDateTrans.Text) & "'"
            End If
            If Me.EFTDateTrans.Text <> "" Then
                _Cmd &= vbCrLf & " And S.FDInvoiceDate <='" & HI.UL.ULDate.ConvertEnDB(Me.EFTDateTrans.Text) & "'"
            End If
            If Me.FTOrderNo.Text <> "" Then
                _Cmd &= vbCrLf & " And D.FTOrderNo >='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            End If
            If Me.FTOrderNoTo.Text <> "" Then
                _Cmd &= vbCrLf & " And D.FTOrderNo <='" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
            End If
            If Me.FNHSysStyleId.Text <> "" Then
                _Cmd &= vbCrLf & " And T.FTStyleCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'"
            End If
            If Me.FNHSysStyleIdTo.Text <> "" Then
                _Cmd &= vbCrLf & " And T.FTStyleCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleIdTo.Text) & "'"
            End If
            _Cmd &= vbCrLf & "Group by T.FTStyleCode,S.FDInvoiceDate"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_FG)
            Me.GridControl1.DataSource = _oDt


        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If VerrifyData() Then
                Call LoadData()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function VerrifyData() As Boolean
        Try
            Dim _State As Boolean = False
            Dim _FieldValidate As String = "SFTDateTrans|EFTDateTrans|FTOrderNo|FTOrderNoTo|FNHSysStyleId|FNHSysStyleIdTo"
            For Each _FieldName As String In _FieldValidate.Split("|")

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                If .Text <> "" Then
                                    _State = True
                                End If
                            End With
                        Case ENM.Control.ControlType.DateEdit
                            Try
                                With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                    If .Text <> "" Then
                                        _State = True
                                    End If
                                End With
                            Catch ex As Exception
                            End Try
                        Case Else
                    End Select
                Next
            Next
            If Not (_State) Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                Me.SFTDateTrans.Focus()
            End If
            Return _State
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Try
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvdetail_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogvdetail.RowStyle
        Try
            If (e.RowHandle Mod 2 = 0) Then
                e.Appearance.BackColor = Color.Salmon
                e.Appearance.BackColor2 = Color.SeaShell
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridView1.RowStyle
        Try
            If (e.RowHandle Mod 2 = 0) Then
                e.Appearance.BackColor = Color.Salmon
                e.Appearance.BackColor2 = Color.SeaShell
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class