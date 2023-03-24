Public Class SCIBFMT
    Public Class Header
        ''' <summary>
        ''' 1 RECORD TYPE Start 1 To 1 Length 1 Type String Defualt 0 "0" = HEADER  RECORD
        ''' </summary>
        Private _RecType As String = "0"
        Public ReadOnly Property F01_RecType As String
            Get
                Dim I As Integer = 1
                Dim _Val As String = _RecType
                'Return (Left(_Val, I) & "").PadRight(I - Len(Left(_Val, I)), "0")
                Return (Left(_Val, I) & "").PadRight(I, "0")
            End Get
        End Property

        ''' <summary>
        ''' 2 RECORD SEQUENCE  Start 2 To 6 Length 5 Type Numeric   มีค่าเป็น "00001" เสมอ
        ''' </summary>
        Private _RecSeq As String = "00001"
        Public ReadOnly Property F02_RecSeq As String
            Get
                Dim I As Integer = 5
                Dim _Val As String = _RecSeq
                ' Return (Left(_Val, I) & "").PadLeft(I - Len(Left(_Val, I)), "0")
                Return (Left(_Val, I) & "").PadLeft(I, "0")
            End Get
 
        End Property

        ''' <summary>
        ''' 3 COMPANY  ID.  Start 7 To 16 Length 10 Type Numeric  รหัสประจำตัวบริษัทที่ธนาคารกำหนดให้  ***
        ''' </summary>
        Private _CmpCode As String = "0"
        Public Property F03_CmpCode As String
            Get
                Dim I As Integer = 10
                Dim _Val As String = _CmpCode
                '  Return (Left(_Val, I) & "").PadLeft(I - Len(Left(_Val, I)), "0")
                Return (Left(_Val, I) & "").PadLeft(I, "0")
            End Get
            Set(ByVal value As String)
                _CmpCode = value
            End Set
        End Property

        ''' <summary>
        ''' 4 EMPLOYEE A/C  Start 17 To 26 Length 10 Type String  รหัสบัญชีเงินฝากของพนักงาน มีค่าเป็น ZERO กรณีที่เป็น HEADER RECORD
        ''' </summary>
        Private _AccountNo As String = "0"
        Public ReadOnly Property F04_AccountNo As String
            Get
                Dim I As Integer = 10
                Dim _Val As String = _AccountNo
                ' Return (Left(_Val, I) & "").PadLeft(I - Len(Left(_Val, I)), "0")
                Return (Left(_Val, I) & "").PadLeft(I, "0")
            End Get
        End Property

        ''' <summary>
        ''' 5 TOTAL - AMOUNT  Start 27 To 39 Length 13 Type Numeric จำนวนเงิน มีค่าเป็น  ZERO กรณีที่เป็น HEADER  RECORD
        ''' </summary>
        Private _TotalAmount As String = "0"
        Public Property F05_TotalAmount As String
            Get

                Dim I As Integer = 13
                Dim _Val As String = _TotalAmount
                'Return (Left(_Val, I) & "").PadLeft(I - Len(Left(_Val, I)), "0")
                Return (Left(_Val, I) & "").PadLeft(I, "0")
            End Get
            Set(ByVal value As String)
                _TotalAmount = value
            End Set
        End Property

        ''' <summary>
        ''' 6 Space  Start 40 To 74 Length 35 Type String   BLANK
        ''' </summary>
        Private _LastSpace As String = " "
        Public ReadOnly Property F06_LastSpace As String
            Get
                Dim I As Integer = 35
                Dim _Val As String = _LastSpace
                ' Return (Left(_Val, I) & "").PadRight(I - Len(Left(_Val, I)), " ")
                Return (Left(_Val, I) & "").PadRight(I, " ")
            End Get
    
        End Property

        ''' <summary>
        ''' 7 EFFECTIVE -DATE OR PayDate  Start 75 To 60 Length 6 Type String  วันที่กำหนดจ่ายเงินเดือน YYMMDD (030130)
        ''' </summary>
        Private _EffDate As String = " "
        Public Property F07_EffDate As String
            Get
                Dim I As Integer = 6
                Dim _Val As String = _EffDate
                ' Return (Left(_Val, I) & "").PadRight(I - Len(Left(_Val, I)), " ")
                Return (Left(_Val, I) & "").PadRight(I, " ")
            End Get
            Set(ByVal value As String)
                _EffDate = value
            End Set
        End Property

    End Class

    Public Class Detail
          ''' <summary>
        ''' 1 RECORD TYPE Start 1 To 1 Length 1 Type String Defualt 1 "1" = DETAIL  RECORD
        ''' </summary>
        Private _RecType As String = "1"
        Public ReadOnly Property F01_RecType As String
            Get
                Dim I As Integer = 1
                Dim _Val As String = _RecType
                '  Return (Left(_Val, I) & "").PadRight(I - Len(Left(_Val, I)), "0")
                Return (Left(_Val, I) & "").PadRight(I, "0")
            End Get
        End Property

    ''' <summary>
        ''' 2 RECORD SEQUENCE  Start 2 To 6 Length 5 Type Numeric   มีค่าเริ่มจาก "00002" 
        ''' </summary>
        Private _RecSeq As String = "00002"
        Public Property F02_RecSeq As String
            Get
                Dim I As Integer = 5
                Dim _Val As String = _RecSeq
                'Return (Left(_Val, I) & "").PadLeft(I - Len(Left(_Val, I)), "0")
                Return (Left(_Val, I) & "").PadLeft(I, "0")
            End Get
            Set(value As String)
                _RecSeq = value
            End Set
        End Property

        ''' <summary>
        ''' 3 COMPANY  ID.  Start 7 To 16 Length 10 Type Numeric  รหัสประจำตัวบริษัทที่ธนาคารกำหนดให้  ***
        ''' </summary>
        Private _CmpCode As String = "0"
        Public Property F03_CmpCode As String
            Get
                Dim I As Integer = 10
                Dim _Val As String = _CmpCode
                ' Return (Left(_Val, I) & "").PadLeft(I - Len(Left(_Val, I)), "0")
                Return (Left(_Val, I) & "").PadLeft(I, "0")
            End Get
            Set(ByVal value As String)
                _CmpCode = value
            End Set
        End Property


        ''' <summary>
        ''' 4 EMPLOYEE A/C  Start 17 To 26 Length 10 Type String  รหัสบัญชีเงินฝากของพนักงาน
        ''' </summary>
        Private _AccountNo As String = "0"
        Public Property F04_AccountNo As String
            Get
                Dim I As Integer = 10
                Dim _Val As String = _AccountNo
                ' Return (Left(_Val, I) & "").PadLeft(I - Len(Left(_Val, I)), "0")
                Return (Left(_Val, I) & "").PadLeft(I, "0")
            End Get
            Set(ByVal value As String)
                _AccountNo = value
            End Set
        End Property

        ''' <summary>
        ''' 5 AMOUNT  Start 27 To 39 Length 13 Type Numeric จำนวนเงินเดือนของพนักงานรวมสตางค์ ไม่ต้องใส่จุด
        ''' </summary>
        Private _Amount As String = "0"
        Public Property F05_Amount As String
            Get

                Dim I As Integer = 13
                Dim _Val As String = _Amount
                '  Return (Left(_Val, I) & "").PadLeft(I - Len(Left(_Val, I)), "0")
                Return (Left(_Val, I) & "").PadLeft(I, "0")
            End Get
            Set(ByVal value As String)
                _Amount = value
            End Set
        End Property

        ''' <summary>
        ''' 6 Space  Start 40 To 74 Length 35 Type String   BLANK
        ''' </summary>
        Private _LastSpace As String = ""
        Public ReadOnly Property F06_LastSpace As String
            Get
                Dim I As Integer = 35
                Dim _Val As String = _LastSpace
                ' Return (Left(_Val, I) & "").PadRight(I - Len(Left(_Val, I)), " ")
                Return (Left(_Val, I) & "").PadLeft(I, " ")
            End Get
        End Property

        ''' <summary>
        ''' 7 EFFECTIVE -DATE OR PayDate  Start 75 To 60 Length 6 Type String  วันที่กำหนดจ่ายเงินเดือน YYMMDD (030130)
        ''' </summary>
        Private _EffDate As String = " "
        Public Property F07_EffDate As String
            Get
                Dim I As Integer = 6
                Dim _Val As String = _EffDate
                'Return (Left(_Val, I) & "").PadRight(I - Len(Left(_Val, I)), " ")
                Return (Left(_Val, I) & "").PadRight(I, " ")
            End Get
            Set(ByVal value As String)
                _EffDate = value
            End Set
        End Property

    End Class

    Public Class Footer
        ''' <summary>
        ''' 1 RECORD TYPE Start 1 To 1 Length 1 Type String Defualt 1 "1" = TOTAL  RECORD
        ''' </summary>
        Private _RecType As String = "9"
        Public ReadOnly Property F01_RecType As String
            Get
                Dim I As Integer = 1
                Dim _Val As String = _RecType
                'Return (Left(_Val, I) & "").PadRight(I - Len(Left(_Val, I)), "0")
                Return (Left(_Val, I) & "").PadRight(I, "0")
            End Get
        End Property

        ''' <summary>
        ''' 2 RECORD SEQUENCE  Start 2 To 6 Length 5 Type Numeric   มีค่าเป็น  LAST SEQUENCE NUMBER
        ''' </summary>
        Private _RecSeq As String = "00002"
        Public Property F02_RecSeq As String
            Get
                Dim I As Integer = 5
                Dim _Val As String = _RecSeq
                ' Return (Left(_Val, I) & "").PadLeft(I - Len(Left(_Val, I)), "0")
                Return (Left(_Val, I) & "").PadLeft(I, "0")
            End Get
            Set(value As String)
                _RecSeq = value
            End Set
        End Property

        ''' <summary>
        ''' 3 COMPANY  ID.  Start 7 To 16 Length 10 Type Numeric  รหัสประจำตัวบริษัทที่ธนาคารกำหนดให้  ***
        ''' </summary>
        Private _CmpCode As String = "0"
        Public Property F03_CmpCode As String
            Get
                Dim I As Integer = 10
                Dim _Val As String = _CmpCode
                '  Return (Left(_Val, I) & "").PadLeft(I - Len(Left(_Val, I)), "0")
                Return (Left(_Val, I) & "").PadLeft(I, "0")
            End Get
            Set(ByVal value As String)
                _CmpCode = value
            End Set
        End Property


        ''' <summary>
        ''' 4 EMPLOYEE A/C  Start 17 To 26 Length 10 Type String  รหัสบัญชีเงินฝากของพนักงาน มีค่าเป็น ZERO
        ''' </summary>
        Private _AccountNo As String = "0"
        Public ReadOnly Property F04_AccountNo As String
            Get
                Dim I As Integer = 10
                Dim _Val As String = _AccountNo
                'Return (Left(_Val, I) & "").PadLeft(I - Len(Left(_Val, I)), "0")
                Return (Left(_Val, I) & "").PadLeft(I, "0")
            End Get
        End Property

        ''' <summary>
        ''' 5 AMOUNT  Start 27 To 39 Length 13 Type Numeric จำนวนเงินเดือนของพนักงานรวมสตางค์ ไม่ต้องใส่จุด
        ''' </summary>
        Private _TotalAmount As String = "0"
        Public Property F05_TotalAmount As String
            Get

                Dim I As Integer = 13
                Dim _Val As String = _TotalAmount
                ' Return (Left(_Val, I) & "").PadLeft(I - Len(Left(_Val, I)), "0")
                Return (Left(_Val, I) & "").PadLeft(I, "0")
            End Get
            Set(ByVal value As String)
                _TotalAmount = value
            End Set
        End Property

        ''' <summary>
        ''' 6 Space  Start 40 To 74 Length 35 Type String   BLANK
        ''' </summary>
        Private _LastSpace As String = ""
        Public ReadOnly Property F06_LastSpace As String
            Get
                Dim I As Integer = 35
                Dim _Val As String = _LastSpace
                'Return (Left(_Val, I) & "").PadRight(I - Len(Left(_Val, I)), " ")
                Return (Left(_Val, I) & "").PadRight(I, " ")
            End Get
        End Property

        ''' <summary>
        ''' 7 EFFECTIVE -DATE OR PayDate  Start 75 To 60 Length 6 Type String  วันที่กำหนดจ่ายเงินเดือน YYMMDD (030130)
        ''' </summary>
        Private _EffDate As String = " "
        Public Property F07_EffDate As String
            Get
                Dim I As Integer = 6
                Dim _Val As String = _EffDate
                'Return (Left(_Val, I) & "").PadRight(I - Len(Left(_Val, I)), " ")
                Return (Left(_Val, I) & "").PadRight(I, " ")
            End Get
            Set(ByVal value As String)
                _EffDate = value
            End Set
        End Property

    End Class

End Class
