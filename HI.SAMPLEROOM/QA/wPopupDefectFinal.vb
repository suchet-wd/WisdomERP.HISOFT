Public Class wPopupDefectFinal

#Region "Property"
    Private _StyleId As Integer
    Public Property StyleId As Integer
        Get
            Return _StyleId
        End Get
        Set(value As Integer)
            _StyleId = value
        End Set
    End Property

    Private _UnitSectId As Integer
    Public Property UnitSectId As Integer
        Get
            Return _UnitSectId
        End Get
        Set(value As Integer)
            _UnitSectId = value
        End Set
    End Property

    Private _OrderNo As String
    Public Property OrderNo As String
        Get
            Return _OrderNo
        End Get
        Set(value As String)
            _OrderNo = value
        End Set
    End Property

    Private _Date As String
    Public Property TDate As String
        Get
            Return _Date
        End Get
        Set(value As String)
            _Date = value
        End Set
    End Property

    Private _DateTo As String
    Public Property TDateTo As String
        Get
            Return _DateTo
        End Get
        Set(value As String)
            _DateTo = value
        End Set
    End Property

    Private _State As String = "0"
    Public Property State As String
        Get
            Return _State
        End Get
        Set(value As String)
            _State = value
        End Set
    End Property

#End Region



    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Call InitGrid()
    End Sub

    Private Sub InitGrid()
        Try
            Dim _FSumMain As String = ""

            _FSumMain = "Qty"
            With ogvsubDetail
                .ClearGrouping()
                .ClearDocument()
                .OptionsView.ShowFooter = True
                For Each Str As String In _FSumMain.Split("|")
                    If Str <> "" Then
                        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                    End If
                Next
            End With

        Catch ex As Exception
        End Try
    End Sub


    Private Sub wPopupDefect_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.LoadData(_StyleId, _UnitSectId, _OrderNo, _Date, _DateTo)
    End Sub

    Private Sub LoadData(_FNHSysStyleId As Integer, _FNHSysUnitSectId As Integer, _FTOrderNo As String, _FDQADate As String, _EDate As String)
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable

            If _State = "0" Then
                _Cmd = "SELECT   ROW_NUMBER() over(order by A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo, A.FDQADate, A.FNHSysQADetailId, B.FTQADetailNameTH, B.FTQADetailNameEN) as Row"
                _Cmd &= vbCrLf & ",  COUNT(*) AS Qty, A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo, A.FDQADate, A.FNHSysQADetailId"
                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    _Cmd &= vbCrLf & ", B.FTQADetailNameTH AS FTQADetailName"
                Else
                    _Cmd &= vbCrLf & ", B.FTQADetailNameEN AS FTQADetailName"
                End If
                _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPreFinal_SubDetail AS A LEFT OUTER JOIN"
                _Cmd &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQADetail AS B ON A.FNHSysQADetailId = B.FNHSysQADetailId"
                _Cmd &= vbCrLf & "WHERE     (A.FNHSysStyleId = " & CInt("0" & _FNHSysStyleId) & ") AND (A.FNHSysUnitSectId = " & CInt("0" & _FNHSysUnitSectId) & ") "
                _Cmd &= vbCrLf & "AND (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "') AND (A.FDQADate = '" & HI.UL.ULDate.ConvertEnDB(_FDQADate) & "')"
                _Cmd &= vbCrLf & "GROUP BY A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo, A.FDQADate, A.FNHSysQADetailId, B.FTQADetailNameTH, B.FTQADetailNameEN"
            Else
                _Cmd = "SELECT   ROW_NUMBER() over(order by A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo, A.FDQADate, A.FNHSysQADetailId, B.FTQADetailNameTH, B.FTQADetailNameEN) as Row"
                _Cmd &= vbCrLf & ",  COUNT(*) AS Qty, A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo, A.FDQADate, A.FNHSysQADetailId"
                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    _Cmd &= vbCrLf & ", B.FTQADetailNameTH AS FTQADetailName"
                Else
                    _Cmd &= vbCrLf & ", B.FTQADetailNameEN AS FTQADetailName"
                End If
                _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQAPreFinal_SubDetail AS A LEFT OUTER JOIN"
                _Cmd &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQADetail AS B ON A.FNHSysQADetailId = B.FNHSysQADetailId"
                _Cmd &= vbCrLf & "WHERE        (A.FNHSysUnitSectId = " & CInt("0" & _FNHSysUnitSectId) & ") "
                _Cmd &= vbCrLf & " AND (A.FDQADate >= '" & HI.UL.ULDate.ConvertEnDB(_FDQADate) & "')   AND (A.FDQADate <= '" & HI.UL.ULDate.ConvertEnDB(_EDate) & "')"
                _Cmd &= vbCrLf & "GROUP BY A.FNHSysStyleId, A.FNHSysUnitSectId, A.FTOrderNo, A.FDQADate, A.FNHSysQADetailId, B.FTQADetailNameTH, B.FTQADetailNameEN"

            End If


            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Me.ogcsubDetail.DataSource = _oDt


        Catch ex As Exception

        End Try
    End Sub

    Private Sub oClose_Click(sender As Object, e As EventArgs) Handles oClose.Click
        Me.Close()
    End Sub
End Class