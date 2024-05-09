Imports System.Reflection

Public Class PROD

    Private _MessageCheck As String = ""
    Public Property MessageCheck As String
        Get
            Return _MessageCheck
        End Get
        Set(value As String)
            _MessageCheck = value
        End Set
    End Property

    Public Function CheckOperationAfter(FTStyleCode As String, FTOrderProdNo As String, FTBarcodeNo As String, FNHSysOperationId As Integer, Quantity As Double) As Boolean

        Dim _Qry As String = ""
        Dim _CheckOperBefore As Integer = 0
        Dim dt As DataTable
        _Qry = "SELECT TOP 1 FTOrderProdNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd AS A WITH(NOLOCK) WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(FTOrderProdNo) & "' "

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") = "" Then

            _Qry = "  SELECT    A.FNHSysOperationId"
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMOperationByStyle AS A WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS B WITH(NOLOCK) ON A.FNHSysOperationId = B.FNHSysOperationId"
            _Qry &= vbCrLf & "  WHERE  A.FNHSysStyleId=ISNULL((SELECT TOP 1 FNHSysStyleId  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK) WHERE FTStylecode='" & HI.UL.ULF.rpQuoted(FTStyleCode) & "'),0) "
            _Qry &= vbCrLf & "   AND A.FNHSysOperationIdTo=" & Integer.Parse(FNHSysOperationId) & " "

        Else

            _Qry = " SELECT  A.FNHSysOperationId"
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd AS A WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS B WITH(NOLOCK) ON A.FNHSysOperationId = B.FNHSysOperationId"
            _Qry &= vbCrLf & "  WHERE  A.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(FTOrderProdNo) & "'  "
            _Qry &= vbCrLf & "   AND A.FNHSysOperationIdTo=" & Integer.Parse(FNHSysOperationId) & " "

        End If
        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        For Each R As DataRow In dt.Rows
            _CheckOperBefore = Integer.Parse(Val(R!FNHSysOperationId.ToString))

            If Integer.Parse(_CheckOperBefore) > 0 Then

                Dim dtcheck As DataTable
                _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_CheckOperationBefore '" & HI.UL.ULF.rpQuoted(FTOrderProdNo) & "'," & Integer.Parse(_CheckOperBefore) & ",'" & HI.UL.ULF.rpQuoted(FTBarcodeNo) & "'"
                dtcheck = (HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD))

                If dtcheck.Rows.Count > 0 Then
                    If Val(dtcheck.Rows(0)!FNQuantity.ToString) <= Val(Quantity) Then
                        Me.MessageCheck = HI.MG.ShowMsg.GetMessage("ไม่สามารถ ลบได้  เนื่องจากพบ การ Scan ขั้นตอนถัดไปแล้ว !!!", 1409251104)
                        Return False
                    End If
                End If

                dtcheck.Dispose()
            End If

        Next

        Return True
    End Function

    Public Function GetStyleCodeByOrderNo(FTOrderNo As String) As String
        Dim _Qry As String

        _Qry = "  SELECT   TOP 1  B.FTStyleCode "
        _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B WITH(NOLOCK) ON A.FNHSysStyleId = B.FNHSysStyleId"
        _Qry &= vbCrLf & "  WHERE    (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(FTOrderNo) & "')"

        Return HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "")
    End Function

    Public Function GetOpertionName(OperationID As Integer) As String
        Try
            Dim _Qry As String

            _Qry = "SELECT TOP 1 "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " FTOperationNameTH  "
            Else
                _Qry &= vbCrLf & " FTOperationNameEN "
            End If
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FNHSysOperationId =" & OperationID.ToString & ""

            Return HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")

        Catch ex As Exception
            Return ""
        End Try

    End Function


    Public Shared Sub DynamicButtone_ButtonClick(ByVal sender As Object)
        Try
            Try
                Dim _form2 As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm
                For Each ctrl As Object In _form2.Controls.Find(sender.Name.ToString & "_Browse", True)
                    With CType(ctrl, HI.UCTR.HButtonDropDown)
                        If _form2.ActiveControl.Name.ToString <> sender.Name.ToString & "_Browse" Then
                            If (.Visible) Then
                                .Visible = False
                            End If
                            .DisposeObject()
                        End If

                    End With
                Next
            Catch ex As Exception
            End Try
            Dim brwsedataid As Integer = 0


            Dim _Form As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).FindForm

            Dim T As System.Type = _Form.GetType()

            Dim _CmpH As String = ""
            For Each ctrl As Object In _Form.Controls.Find("FNHSysCmpId", True)

                ' _CmpH = HI.ST.SysInfo.CmpRunID

                Select Case HI.ENM.Control.GeTypeControl(ctrl)
                    Case ENM.Control.ControlType.ButtonEdit
                        With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                            _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                        End With

                        Exit For
                    Case ENM.Control.ControlType.TextEdit
                        With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                            If .Text = "" Then
                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                            Else
                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                            End If

                        End With

                        Exit For
                End Select

            Next



            With CType(sender, DevExpress.XtraEditors.ButtonEdit)

                Dim _pdbnameinfo As PropertyInfo
                Dim _ptablenameinfo As PropertyInfo
                Dim _pdoctypeinfo As PropertyInfo
                Dim _pdocclearinfo As PropertyInfo
                Dim _minfo As MethodInfo
                Dim _minfo2 As MethodInfo

                _pdbnameinfo = T.GetProperty("SysDBName")
                _ptablenameinfo = T.GetProperty("SysTableName")
                _pdoctypeinfo = T.GetProperty("SysDocType")
                _pdocclearinfo = T.GetProperty("SysDocClear")
                _minfo = T.GetMethod("InitData")
                _minfo2 = T.GetMethod("DefaultsData")

                If Not (_pdbnameinfo Is Nothing) AndAlso Not (_ptablenameinfo Is Nothing) AndAlso Not (_pdoctypeinfo Is Nothing) Then

                    If Not (_pdocclearinfo Is Nothing) Then

                        Try

                            If _pdocclearinfo.GetValue(_Form, Nothing) = True Then
                                HI.TL.HandlerControl.ClearControl(_Form)
                            End If

                        Catch ex As Exception
                            HI.TL.HandlerControl.ClearControl(_Form)
                        End Try

                    Else
                        HI.TL.HandlerControl.ClearControl(_Form)
                    End If

                    .Text = HI.TL.Document.GetDocumentNo(_pdbnameinfo.GetValue(_Form, Nothing).ToString, _ptablenameinfo.GetValue(_Form, Nothing).ToString, _pdoctypeinfo.GetValue(_Form, Nothing).ToString, True, _CmpH)

                    If Not (_minfo Is Nothing) Then
                        _minfo.Invoke(_Form, Nothing)
                    End If

                    If Not (_minfo2 Is Nothing) Then
                        _minfo2.Invoke(_Form, Nothing)
                    End If

                End If

            End With


        Catch ex As Exception

        End Try


    End Sub


    Public Sub createPackingplanNonnike(_packing As String)
        Try

            Dim _Cmd As String = ""
            _Cmd = "exec  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.[SP_CREATE_PACKINGPLAN_FROM_OrderPack] @packing ='" & _packing & "' , @UserLogin='" & HI.ST.UserInfo.UserName & "'"
            HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD)
        Catch ex As Exception

        End Try
    End Sub


    Public Function gen_NewMarkCode(_PartCode As String) As String
        Try
            Dim _Cmd As String = "" : Dim MaxMarkCode As String = ""
            _Cmd = " select max(FTMarkCode) FTMarkCode  from   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMURatio_D "
            _Cmd &= vbCrLf & " where  left(FTMarkCode ," & Len(_PartCode) & ")  =  '" & _PartCode & "'"

            MaxMarkCode = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "")

            If MaxMarkCode <> "" Then
                MaxMarkCode = Integer.Parse(Replace(MaxMarkCode, _PartCode, "").ToString()) + 1

            End If


            Return MaxMarkCode

        Catch ex As Exception
            Return ""
        End Try

    End Function


End Class
