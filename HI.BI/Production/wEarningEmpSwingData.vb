Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns

Public Class wEarningEmpSwingData

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean
    Private _FTStateProdSMKToCutQty As Boolean

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub LoadCompany()
        Dim _Str As String

        _Str = " SELECT   '0' AS FTSelect "
        _Str &= vbCrLf & ",M.FNHSysCmpId"
        _Str &= vbCrLf & ",M.FTCmpCode,ISNULL(IPP.FTIPServer,'') AS FTIPServer"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

            _Str &= vbCrLf & " , M.FTCmpNameTH AS FTCmpName "

        Else
            _Str &= vbCrLf & " , M.FTCmpNameEN AS FTCmpName "
        End If

        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS M WITH(NOLOCK) "
        _Str &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSECompanyIPServer AS IPP WITH(NOLOCK) ON M.FNHSysCmpId = IPP.FNHSysCmpId "
        _Str &= vbCrLf & " WHERE ISNULL(M.FTStateActive,'') ='1' AND ISNULL(IPP.FTIPServer,'') <>'' "
        _Str &= vbCrLf & " ORDER BY M.FTCmpCode"

        Me.ogccmp.DataSource = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)

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

    Private _ActualDate As String = ""
    ReadOnly Property ActualDate As String
        Get
            Return _ActualDate
        End Get
    End Property

    Private _ActualNextDate As String = ""
    ReadOnly Property ActualNextDate As String
        Get
            Return _ActualNextDate
        End Get
    End Property


#End Region

#Region "Procedure"


    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FTStartDate.Text <> "" And FTStartDate.Text <> "" Then
            _Pass = True
        End If

        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function

    Private Function LoadDataInfo(Spls As HI.TL.SplashScreen) As Boolean
        Try
            Dim _Qry As String = ""

            Spls.UpdateInformation("Loading.... Data Company   Please wait....")

            Dim datestart As String = Me.FTStartDate.Text
            Dim dateend As String = Me.FTEndDate.Text

            Dim _dtcmp As DataTable
            With CType(Me.ogccmp.DataSource, DataTable)
                .AcceptChanges()
                _dtcmp = .Copy
            End With

            Dim dtdata As DataTable = Nothing
            Dim dtdatadetail As DataTable = Nothing

            Dim _ServerName, _UID, _PWS, _DBName As String
            Dim _ConnectString As String = ""
            Dim _FNHSysCmpId As Integer = 0
            Dim CmpCode As String = ""
            For Each R As DataRow In _dtcmp.Select("FTSelect='1'")

                _FNHSysCmpId = Val(R!FNHSysCmpId.ToString)
                CmpCode = R!FTCmpCode.ToString()

                If HI.Conn.DB.UsedDB(Conn.DB.DataBaseName.DB_PROD) Then

                    _ServerName = R!FTIPServer.ToString
                    _UID = HI.Conn.DB.UIDName
                    _PWS = HI.Conn.DB.PWDName
                    _DBName = HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD)

                    _ConnectString = "SERVER=" & _ServerName & ";UID=" & _UID & ";PWD=" & _PWS & ";Initial Catalog=" & _DBName

                    Spls.UpdateInformation("Loading.... Data Company " & R!FTCmpCode.ToString & "   Please wait....")

                    Try
                        Dim _dt As New DataTable
                        Dim _dtdetail As New DataTable
                        _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_DATA_EMP_SEWING_INFO '" & HI.UL.ULF.rpQuoted(CmpCode) & "','" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) & "'"
                        _dt = HI.Conn.SQLConn.GetDataTableConectstring(_Qry, _ConnectString)


                        _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_DATA_EMP_SEWING_INFO_DETAIL '" & HI.UL.ULF.rpQuoted(CmpCode) & "','" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) & "'"
                        _dtdetail = HI.Conn.SQLConn.GetDataTableConectstring(_Qry, _ConnectString)

                        If dtdata Is Nothing Then
                            dtdata = _dt.Copy
                        Else
                            dtdata.Merge(_dt.Copy)
                        End If

                        If dtdatadetail Is Nothing Then
                            dtdatadetail = _dtdetail.Copy
                        Else
                            dtdatadetail.Merge(_dtdetail.Copy)

                        End If

                        _dt.Dispose()
                        _dtdetail.Dispose()
                    Catch ex22 As Exception
                        ' System.Windows.Forms.MessageBox.Show(ex22.Message())
                    End Try

                End If

            Next

            Me.ogdsum.DataSource = dtdata.Copy
            Me.ogddetail.DataSource = dtdatadetail.Copy

            dtdata.Dispose()
            dtdatadetail.Dispose()
        Catch ex As Exception
        End Try

        Return True
    End Function


#End Region


#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            LoadCompany()

            StateCal = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If VerifyData() Then

            Dim _dtcmp As DataTable
            With CType(Me.ogccmp.DataSource, DataTable)
                .AcceptChanges()
                _dtcmp = .Copy
            End With

            If _dtcmp.Select("FTSelect='1'").Length > 0 Then
                _dtcmp.Dispose()

                Dim _Spls As New HI.TL.SplashScreen("Loading data...   Please Wait  ")

                Try
                    Me.LoadDataInfo(_Spls)
                    Me.otbdata.SelectedTabPageIndex = 0
                Catch ex As Exception
                    ' System.Windows.Forms.MessageBox.Show(ex.Message())
                End Try

                _Spls.Close()

            Else
                _dtcmp.Dispose()

                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Company !!!", 15120508456, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)

            End If

        End If

    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ogvdetail_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvdetail.CellMerge
        With ogvdetail
            Select Case e.Column.FieldName.ToString
                Case "FTCmpCode"
                Case "FTUnitSectCode"

                Case "FTDateTrans"

                    If ("" & .GetRowCellValue(e.RowHandle1, "FTCmpCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTCmpCode").ToString) _
                        And ("" & .GetRowCellValue(e.RowHandle1, "FTUnitSectCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTUnitSectCode").ToString) _
                          And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                        e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                        e.Handled = True

                    Else
                        e.Merge = False
                        e.Handled = True
                    End If

                Case "FNTimeMin"

                    If ("" & .GetRowCellValue(e.RowHandle1, "FTCmpCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTCmpCode").ToString) _
                       And ("" & .GetRowCellValue(e.RowHandle1, "FTUnitSectCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTUnitSectCode").ToString) _
                        And ("" & .GetRowCellValue(e.RowHandle1, "FTDateTrans").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTDateTrans").ToString) _
                         And ("" & .GetRowCellValue(e.RowHandle1, "FNOT1Min").ToString = "" & .GetRowCellValue(e.RowHandle2, "FNOT1Min").ToString) _
                         And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                        e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                        e.Handled = True

                    Else
                        e.Merge = False
                        e.Handled = True
                    End If
                Case "FTStyleNo"
                    If ("" & .GetRowCellValue(e.RowHandle1, "FTCmpCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTCmpCode").ToString) _
                     And ("" & .GetRowCellValue(e.RowHandle1, "FTUnitSectCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTUnitSectCode").ToString) _
                      And ("" & .GetRowCellValue(e.RowHandle1, "FTDateTrans").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTDateTrans").ToString) _                      
                       And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                        e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                        e.Handled = True

                    Else
                        e.Merge = False
                        e.Handled = True
                    End If
                Case "FNOT1Min"

                    If ("" & .GetRowCellValue(e.RowHandle1, "FTCmpCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTCmpCode").ToString) _
                       And ("" & .GetRowCellValue(e.RowHandle1, "FTUnitSectCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTUnitSectCode").ToString) _
                        And ("" & .GetRowCellValue(e.RowHandle1, "FTDateTrans").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTDateTrans").ToString) _
                         And ("" & .GetRowCellValue(e.RowHandle1, "FNTimeMin").ToString = "" & .GetRowCellValue(e.RowHandle2, "FNTimeMin").ToString) _
                         And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                        e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                        e.Handled = True

                    Else
                        e.Merge = False
                        e.Handled = True
                    End If


                Case "FNEmpNomalTime", "FNEmpOTTime"

                    If ("" & .GetRowCellValue(e.RowHandle1, "FTCmpCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTCmpCode").ToString) _
                    And ("" & .GetRowCellValue(e.RowHandle1, "FTUnitSectCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTUnitSectCode").ToString) _
                     And ("" & .GetRowCellValue(e.RowHandle1, "FTDateTrans").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTDateTrans").ToString) _
                          And ("" & .GetRowCellValue(e.RowHandle1, "FNTimeMin").ToString = "" & .GetRowCellValue(e.RowHandle2, "FNTimeMin").ToString) _
                          And ("" & .GetRowCellValue(e.RowHandle1, "FNOT1Min").ToString = "" & .GetRowCellValue(e.RowHandle2, "FNOT1Min").ToString) _
                      And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                        e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                        e.Handled = True

                    Else
                        e.Merge = False
                        e.Handled = True
                    End If


                Case "FNQuantity", "FNSam"
                    If ("" & .GetRowCellValue(e.RowHandle1, "FTCmpCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTCmpCode").ToString) _
                     And ("" & .GetRowCellValue(e.RowHandle1, "FTUnitSectCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTUnitSectCode").ToString) _
                      And ("" & .GetRowCellValue(e.RowHandle1, "FTDateTrans").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTDateTrans").ToString) _
                         And ("" & .GetRowCellValue(e.RowHandle1, "FTStyleNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTStyleNo").ToString) _
                       And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                        e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                        e.Handled = True

                    Else
                        e.Merge = False
                        e.Handled = True
                    End If


            End Select
        End With
    End Sub

    Private Sub ogvsum_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvsum.CellMerge
        With ogvsum
            Select Case e.Column.FieldName.ToString
                Case "FTCmpCode"
                Case "FTUnitSectCode"

                Case "FTDateTrans"

                    If ("" & .GetRowCellValue(e.RowHandle1, "FTCmpCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTCmpCode").ToString) _
                        And ("" & .GetRowCellValue(e.RowHandle1, "FTUnitSectCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTUnitSectCode").ToString) _
                          And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                        e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                        e.Handled = True

                    Else
                        e.Merge = False
                        e.Handled = True
                    End If

                Case "FNTimeMin"

                    If ("" & .GetRowCellValue(e.RowHandle1, "FTCmpCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTCmpCode").ToString) _
                       And ("" & .GetRowCellValue(e.RowHandle1, "FTUnitSectCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTUnitSectCode").ToString) _
                        And ("" & .GetRowCellValue(e.RowHandle1, "FTDateTrans").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTDateTrans").ToString) _
                          And ("" & .GetRowCellValue(e.RowHandle1, "FNOT1Min").ToString = "" & .GetRowCellValue(e.RowHandle2, "FNOT1Min").ToString) _
                         And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                        e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                        e.Handled = True

                    Else
                        e.Merge = False
                        e.Handled = True
                    End If
                Case "FNOT1Min"

                    If ("" & .GetRowCellValue(e.RowHandle1, "FTCmpCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTCmpCode").ToString) _
                       And ("" & .GetRowCellValue(e.RowHandle1, "FTUnitSectCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTUnitSectCode").ToString) _
                        And ("" & .GetRowCellValue(e.RowHandle1, "FTDateTrans").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTDateTrans").ToString) _
                         And ("" & .GetRowCellValue(e.RowHandle1, "FNTimeMin").ToString = "" & .GetRowCellValue(e.RowHandle2, "FNTimeMin").ToString) _
                         And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                        e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                        e.Handled = True

                    Else
                        e.Merge = False
                        e.Handled = True
                    End If
                Case "FTStyleNo", "FTStyleSam", "FTStyleQty"

                    If ("" & .GetRowCellValue(e.RowHandle1, "FTCmpCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTCmpCode").ToString) _
                       And ("" & .GetRowCellValue(e.RowHandle1, "FTUnitSectCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTUnitSectCode").ToString) _
                        And ("" & .GetRowCellValue(e.RowHandle1, "FTDateTrans").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTDateTrans").ToString) _
                         And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                        e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                        e.Handled = True

                    Else
                        e.Merge = False
                        e.Handled = True
                    End If

                Case "FNEmpNomalTime", "FNEmpOTTime"

                    If ("" & .GetRowCellValue(e.RowHandle1, "FTCmpCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTCmpCode").ToString) _
                    And ("" & .GetRowCellValue(e.RowHandle1, "FTUnitSectCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTUnitSectCode").ToString) _
                     And ("" & .GetRowCellValue(e.RowHandle1, "FTDateTrans").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTDateTrans").ToString) _
                          And ("" & .GetRowCellValue(e.RowHandle1, "FNTimeMin").ToString = "" & .GetRowCellValue(e.RowHandle2, "FNTimeMin").ToString) _
                          And ("" & .GetRowCellValue(e.RowHandle1, "FNOT1Min").ToString = "" & .GetRowCellValue(e.RowHandle2, "FNOT1Min").ToString) _
                      And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                        e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                        e.Handled = True

                    Else
                        e.Merge = False
                        e.Handled = True
                    End If


                Case "FNQuantity", "FNSam"
                    If ("" & .GetRowCellValue(e.RowHandle1, "FTCmpCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTCmpCode").ToString) _
                     And ("" & .GetRowCellValue(e.RowHandle1, "FTUnitSectCode").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTUnitSectCode").ToString) _
                      And ("" & .GetRowCellValue(e.RowHandle1, "FTDateTrans").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTDateTrans").ToString) _
                         And ("" & .GetRowCellValue(e.RowHandle1, "FTStyleNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTStyleNo").ToString) _
                       And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                        e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                        e.Handled = True

                    Else
                        e.Merge = False
                        e.Handled = True
                    End If


            End Select
        End With
    End Sub
End Class