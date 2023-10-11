Imports DevExpress.XtraEditors

Public Class wQAPreFinalSamplePopup
    Private dataSource As SampleDataSource
    Private Ui As uCQA
    Private _Static As Boolean
    Private _Path As String = System.Windows.Forms.Application.StartupPath.ToString


    Sub New()


        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'HI.TL.HandlerControl.AddHandlerObj(Me)

        Dim oSysLang As New ST.SysLanguage
        Dim _ModuleID As String = HI.ST.SysInfo.ModuleID
        Try
            Call oSysLang.LoadObjectLanguage(_ModuleID, Me.Name.ToString.Trim, Me)
        Catch ex As Exception
        Finally
        End Try
    End Sub


#Region "propoty"

    Private _Proc As Boolean = False
    Public Property Proc As Boolean
        Get
            Return _Proc
        End Get
        Set(value As Boolean)
            _Proc = value
        End Set
    End Property

    Private _DocNo As String = ""
    Public Property DocNo As String
        Get
            Return _DocNo
        End Get
        Set(value As String)
            _DocNo = value
        End Set
    End Property

    Private _Barcode As String = ""
    Public Property Barcode As String
        Get
            Return _Barcode
        End Get
        Set(value As String)
            _Barcode = value
        End Set
    End Property


    Private pStyleId As Integer = 0
    Public Property StyleId As Integer
        Get
            Return pStyleId
        End Get
        Set(value As Integer)
            pStyleId = value
        End Set
    End Property


    Private pUnitSectId As Integer = 0
    Public Property UnitSectId As Integer
        Get
            Return pUnitSectId
        End Get
        Set(value As Integer)
            pUnitSectId = value
        End Set
    End Property

    Private pDates As String
    Public Property Dates As String
        Get
            Return pDates
        End Get
        Set(value As String)
            pDates = value
        End Set
    End Property

    Private pTimes As String
    Public Property Times As String
        Get
            Return pTimes
        End Get
        Set(value As String)
            pTimes = value
        End Set
    End Property

    Private pSeq As Integer = 0
    Public Property Seq As Integer
        Get
            Return pSeq
        End Get
        Set(value As Integer)
            pSeq = value
        End Set
    End Property

    Private pOrderNo As String = ""
    Public Property OrderNo As String
        Get
            Return pOrderNo
        End Get
        Set(value As String)
            pOrderNo = value
        End Set
    End Property

    Private pFNBarcodeSeq As Integer = 0
    Public Property FNBarcodeSeq As Integer
        Get
            Return pFNBarcodeSeq
        End Get
        Set(value As Integer)
            pFNBarcodeSeq = value
        End Set
    End Property


#End Region


    Private Sub ocmload_Click(sender As Object, e As EventArgs)
        Try
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs)
        Try
            HI.TL.HandlerControl.ClearControl(Me)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs)
        Try
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Public Sub loadinfo(Qty As Integer, type As Integer)
        Try
            Call CreateGroupDefect(Qty, type)
        Catch ex As Exception
        End Try
    End Sub


    Private _TileGroup As DevExpress.XtraEditors.TileGroup
    Private Sub CreateGroupDefect(qty As Integer, ByVal _FNSendSuplType As String)
        Try
            Dim _Cmd As String = ""
            Try
                TileControl.Groups.Remove(_TileGroup)
                While (TileControl.Groups.Count > 0)
                    TileControl.Groups.RemoveAt(0)
                End While

            Catch ex As Exception
            End Try

            _Cmd = "Select    FNBarcodeSeq    from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo. TSMPTQAPreFinal_Barcode with(nolock) "
            _Cmd &= vbCrLf & " where  FTBarcodeCartonNo = '" & HI.UL.ULF.rpQuoted(_Barcode) & "'"

            Dim _odt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)


            TileControl.ShowGroupText = False

            _TileGroup = New DevExpress.XtraEditors.TileGroup
            _TileGroup.Text = "group 1 "
            TileControl.Groups.Add(_TileGroup)
            TileControl.ItemSize = 80

            Dim _count As String = "0"
            For i As Integer = 1 To qty Step 1
                _count = "0"
                Try
                    _count = _odt.Compute("Count(FNBarcodeSeq) ", "FNBarcodeSeq=" & Val(i.ToString))
                Catch ex As Exception
                    _count = "0"
                End Try



                Dim _i As New DevExpress.XtraEditors.TileItem
                _i.AllowAnimation = True
                _i.BackgroundImageScaleMode = TileItemImageScaleMode.ZoomInside

                If _count = "0" Then
                    _i.AppearanceItem.Normal.BackColor = Color.DodgerBlue
                    _i.AppearanceItem.Normal.BorderColor = Color.LightBlue
                    _i.AppearanceItem.Normal.ForeColor = Color.Black
                Else
                    _i.AppearanceItem.Normal.BackColor = Color.WhiteSmoke
                    _i.AppearanceItem.Normal.BorderColor = Color.LightGray
                    _i.AppearanceItem.Normal.ForeColor = Color.Black
                End If


                _i.ItemSize = TileItemSize.Wide
                _i.ContentAnimation = TileItemContentAnimationType.ScrollLeft
                _i.Name = i.ToString
                Try
                    _i.Text = _count '_odt.Compute("Count(FNBarcodeSeq) ", "FNBarcodeSeq=" & Val(i.ToString))
                Catch ex As Exception
                    _i.Text = "0"
                End Try


                '_i.Image = HI.UL.ULImage.LoadImage(_Path & "\Images\Func\qcfunc\0.JPG")


                _i.Id = CInt(i.ToString)
                Dim Elmt As DevExpress.XtraEditors.TileItemElement
                Elmt = New DevExpress.XtraEditors.TileItemElement
                'Elmt.Text = R!FTQCSupDetailCode.ToString
                Elmt.Text = "ตัวที่ " & i.ToString
                Elmt.TextAlignment = TileItemContentAlignment.BottomLeft
                _i.Elements.Add(Elmt)
                _TileGroup.Items.Add(_i)
            Next

            'AddHandler TileControl.RightItemClick, AddressOf TileControl_RightItemClick
            'AddHandler TileControl.ItemClick, AddressOf TileControl_ItemClick
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TileControl_RightItemClick(sender As Object, e As TileItemEventArgs) Handles TileControl.RightItemClick
        Try

            If e.Item.Checked = True Then
                Seq = Seq + 1
                Call _RemoveDefect(e.Item.Id.ToString, _Barcode, StyleId, UnitSectId, Dates, OrderNo, Seq, Times)
                e.Item.Checked = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TileControl_ItemClick(sender As Object, e As TileItemEventArgs) Handles TileControl.ItemClick
        Try
            'If Me.FNQuantity.Value <= 0 Then
            '    Exit Sub
            'End If
            If e.Item.Checked = False Then
                'If (_StateSave) Then
                '    Me.FNQCActualQty.Value += +1
                '    _StateSave = False
                'End If
                'Call _SaveData()
                'Seq = Seq + 1
                Call _AddDefect(e.Item.Id.ToString, _Barcode, StyleId, UnitSectId, Dates, OrderNo, Seq, Times)
                FNBarcodeSeq = Val(e.Item.Id.ToString)
                e.Item.Checked = True
            End If
            _Proc = True
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub


    Private Sub _RemoveDefect(ByVal _DetailId As Integer, ByVal _barcode As String, _StyleId As Integer, _UnitSectId As Integer, ByVal _Date As String, ByVal _OrderNo As String, ByVal _Seq As Integer, ByVal _Time As String)
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "Select *  From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTQAPreFinal_Barcode WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE FTBarcodeCartonNo ='" & HI.UL.ULF.rpQuoted(_barcode) & "'"
            _Cmd &= vbCrLf & "AND FNBarcodeSeq=" & _DetailId

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)
            If _oDt.Rows.Count <= 0 Then Exit Sub

            _Cmd = "Delete  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTQAPreFinal_Barcode "
            _Cmd &= vbCrLf & " where  FTBarcodeRef='" & HI.UL.ULF.rpQuoted(_DocNo) & "'"
            _Cmd &= vbCrLf & " and  FTBarcodeCartonNo='" & HI.UL.ULF.rpQuoted(_barcode) & "'"
            _Cmd &= vbCrLf & " and  FNBarcodeSeq = " & _DetailId


            '_Cmd &= " ( FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate, FNHourNo, FNSeq, FTBarcodeCartonNo, FNBarcodeSeq, FTBarcodeRef)"
            '_Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '_Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            '_Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            '_Cmd &= vbCrLf & "," & _StyleId
            '_Cmd &= vbCrLf & "," & _UnitSectId
            '_Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
            '_Cmd &= vbCrLf & ",'" & _Date & "'"
            '_Cmd &= vbCrLf & ",'" & _Time & "'"
            '_Cmd &= vbCrLf & "," & _Seq
            '_Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_barcode) & "'"
            '_Cmd &= vbCrLf & "," & _DetailId
            '_Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_DocNo) & "'"

            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)

        Catch ex As Exception
        End Try
    End Sub



    Private Sub _AddDefect(ByVal _DetailId As Integer, ByVal _barcode As String, _StyleId As Integer, _UnitSectId As Integer, ByVal _Date As String, ByVal _OrderNo As String, ByVal _Seq As Integer, ByVal _Time As String)
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "Select *  From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTQAPreFinal_Barcode WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE FTBarcodeCartonNo ='" & HI.UL.ULF.rpQuoted(_barcode) & "'"
            _Cmd &= vbCrLf & "AND FNBarcodeSeq=" & _DetailId

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)
            If _oDt.Rows.Count > 0 Then


                _Cmd = "INSERT INTO " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTQAPreFinal_Barcode "
                _Cmd &= " ( FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate, FNHourNo, FNSeq,FNNo, FTBarcodeCartonNo, FNBarcodeSeq, FTBarcodeRef)"
                _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & "," & _StyleId
                _Cmd &= vbCrLf & "," & _UnitSectId
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
                _Cmd &= vbCrLf & ",'" & _Date & "'"
                _Cmd &= vbCrLf & ",'" & _Time & "'"
                _Cmd &= vbCrLf & "," & _Seq
                _Cmd &= vbCrLf & ", isnull ( ( Select  max(FNNo) as FNSeq From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTQAPreFinal_Barcode WITH(NOLOCK)  WHERE FTBarcodeCartonNo ='" & HI.UL.ULF.rpQuoted(_barcode) & "' AND FNBarcodeSeq=" & _DetailId & "  ) + 1 , 1)"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_barcode) & "'"
                _Cmd &= vbCrLf & "," & _DetailId
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_DocNo) & "'"

                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)
            Else

                _Cmd = "INSERT INTO " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTQAPreFinal_Barcode "
                _Cmd &= " ( FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId, FNHSysUnitSectId, FTOrderNo, FDQADate, FNHourNo, FNSeq,FNNo, FTBarcodeCartonNo, FNBarcodeSeq, FTBarcodeRef)"
                _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & "," & _StyleId
                _Cmd &= vbCrLf & "," & _UnitSectId
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
                _Cmd &= vbCrLf & ",'" & _Date & "'"
                _Cmd &= vbCrLf & ",'" & _Time & "'"
                _Cmd &= vbCrLf & "," & _Seq
                _Cmd &= vbCrLf & ", isnull ( ( Select  max(FNNo) as FNSeq From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTQAPreFinal_Barcode WITH(NOLOCK)  WHERE FTBarcodeCartonNo ='" & HI.UL.ULF.rpQuoted(_barcode) & "' AND FNBarcodeSeq=" & _DetailId & "  ) + 1  , 1)"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_barcode) & "'"
                _Cmd &= vbCrLf & "," & _DetailId
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_DocNo) & "'"

                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)
            End If


        Catch ex As Exception
        End Try
    End Sub




    Private Sub obtSelect_Click(sender As Object, e As EventArgs) Handles obtSelect.Click
        Try

            _Proc = True

            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub obtClose_Click(sender As Object, e As EventArgs) Handles obtClose.Click
        Try
            _Proc = False
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub



    Public Function SaveData() As Boolean
        Try
            Dim _Cmd As String = ""



            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
