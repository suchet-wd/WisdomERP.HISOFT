Public Class TBANKFMT

    Public Class Header
        ''' <summary>
        ''' 1 Space Start 1 To 8 Length 8 Type String Defualt Space
        ''' </summary>
        Private _Space As String = ""
        Public Property F01_Space As String
            Get
                Dim I As Integer = 8
                Dim _Val As String = _Space
                ' Return (Left(_Val, I) & "").PadRight(I - Len(Left(_Val, I)), " ")
                Return (Left(_Val, I) & "").PadRight(I, " ")
            End Get
            Set(ByVal value As String)
                _Space = value
            End Set
        End Property

        ''' <summary>
        ''' 2 TERM - PAYMENT  Start 9 To 9 Length 1 Type String   M =  MONTH :  H  = HALF MONTH :  W = WEEK
        ''' </summary>
        Private _TermPayment As String = "M"
        Public Property F02_TermPayment As String
            Get
                Dim I As Integer = 1
                Dim _Val As String = _TermPayment
                ' Return (Left(_Val, I) & "").PadRight(I - Len(Left(_Val, I)), " ")
                Return (Left(_Val, I) & "").PadRight(I, "0")
            End Get
            Set(ByVal value As String)
                _TermPayment = value
            End Set
        End Property

        ''' <summary>
        ''' 3 COMPANY - CODE  Start 10 To 15 Length 6 Type Numeric 
        ''' </summary>
        Private _CmpCode As String = "0"
        Public Property F03_CmpCode As String
            Get
                Dim I As Integer = 6
                Dim _Val As String = _CmpCode
                ' Return (Left(_Val, I) & "").PadRight(I - Len(Left(_Val, I)), "0")
                Return (Left(_Val, I) & "").PadRight(I, "0")
            End Get
            Set(ByVal value As String)
                _CmpCode = value
            End Set
        End Property

        ''' <summary>
        ''' 4 COMPANY - NAME  Start 16 To 45 Length 30 Type String 
        ''' </summary>
        Private _CmpName As String = ""
        Public Property F04_CmpName As String
            Get
                Dim I As Integer = 30
                Dim _Val As String = _CmpName
                'Return (Left(_Val, I) & "").PadRight(I - Len(Left(_Val, I)), " ")
                Return (Left(_Val, I) & "").PadRight(I, " ")
            End Get
            Set(ByVal value As String)
                _CmpName = value
            End Set
        End Property

        ''' <summary>
        ''' 5 APPLY - CODE Start 46 To 46 Length 1 Type String  D = direct credit , Payroll , C= direct debit
        ''' </summary>
        Private _ApplyCode As String = "D"
        Public Property F05_ApplyCode As String
            Get

                Dim I As Integer = 1
                Dim _Val As String = _ApplyCode
                ' Return (Left(_Val, I) & "").PadRight(I - Len(Left(_Val, I)), " ")
                Return (Left(_Val, I) & "").PadRight(I, " ")
            End Get
            Set(ByVal value As String)
                _ApplyCode = value
            End Set
        End Property

        ''' <summary>
        ''' 6 STATUS Start 47 To 47 Length 1 Type String  Fix A
        ''' </summary>
        Private _Status As String = "A"
        Public ReadOnly Property F06_Status As String
            Get
                Return _Status
            End Get
        End Property

        ''' <summary>
        ''' 7 EFFECTIVE -DATE OR PayDate  Start 48 To 53 Length 6 Type String  DDMMYY
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

        ''' <summary>
        ''' 8 TOTAL - RECORD  Start 54 To 58 Length 5 Type Numeric
        ''' </summary>
        Private _TotalRecord As String = "0"
        Public Property F08_TotalRecord As String
            Get

                Dim I As Integer = 5
                Dim _Val As String = _TotalRecord
                'Return (Left(_Val, I) & "").PadLeft(I - Len(Left(_Val, I)), "0")
                Return (Left(_Val, I) & "").PadLeft(I, "0")
            End Get
            Set(ByVal value As String)
                _TotalRecord = value
            End Set
        End Property

        ''' <summary>
        ''' 9 TOTAL - AMOUNT  Start 59 To 69 Length 11 Type Numeric
        ''' </summary>
        Private _TotalAmount As String = "0"
        Public Property F09_TotalAmount As String
            Get

                Dim I As Integer = 11
                Dim _Val As String = _TotalAmount
                ' Return (Left(_Val, I) & "").PadLeft( (I - _Val.Lenght), "0")
                Return (Left(_Val, I) & "").PadLeft(I, "0")
            End Get
            Set(ByVal value As String)
                _TotalAmount = value
            End Set
        End Property

        ''' <summary>
        ''' 10 MEDIA - TYPE  Start 70 To 70 Length 1 Type String   T  = TAPE,  D  = DISKETTE,   I   =  INFO,  K  = KEY IN-BY-POOL
        ''' </summary>
        Private _MediaType As String = "T"
        Public Property F10_MediaType As String
            Get
                Dim I As Integer = 1
                Dim _Val As String = _MediaType
                ' Return (Left(_Val, I) & "").PadRight(I - Len(Left(_Val, I)), " ")
                Return (Left(_Val, I) & "").PadRight(I, " ")
            End Get
            Set(ByVal value As String)
                _MediaType = value
            End Set
        End Property

        ''' <summary>
        ''' 11 Space  Start 77 To 80 Length 10 Type String  
        ''' </summary>
        Private _LastSpace As String = "T"
        Public Property F11_LastSpace As String
            Get
                Dim I As Integer = 10
                Dim _Val As String = _LastSpace
                'Return (Left(_Val, I) & "").PadRight(I - Len(Left(_Val, I)), " ")
                Return (Left(_Val, I) & "").PadRight(I, " ")
            End Get
            Set(ByVal value As String)
                _LastSpace = value
            End Set
        End Property
    End Class

    Public Class Detail
        ''' <summary>
        ''' 1 ACCOUNT - NO  Start 1 To 10 Length 10 Type Numeric 
        ''' </summary>
        Private _AccountNo As String = ""
        Public Property F01_AccountNo As String
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
        ''' 2 EFFECTIVE -DATE OR PayDate  Start 11 To 16 Length 6 Type String  DDMMYY
        ''' </summary>
        Private _EffDate As String = " "
        Public Property F02_EffDate As String
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

        ''' <summary>
        ''' 3 Amount  Sign 17 To 17 Length 1 Type Numeric 0 = Credit , 1 = Debit
        ''' </summary>
        Private _AmountSign As String = "0"
        Public Property F03_AmountSign As String
            Get

                Dim I As Integer = 1
                Dim _Val As String = _AmountSign
                'Return (Left(_Val, I) & "").PadRight(I - Len(Left(_Val, I)), "0")
                Return (Left(_Val, I) & "").PadRight(I, "0")
            End Get
            Set(ByVal value As String)
                _AmountSign = value
            End Set
        End Property

        ''' <summary>
        ''' 4 AMOUNT  Start 18 To 28 Length 11 Type Numeric
        ''' </summary>
        Private _Amount As String = "0"
        Public Property F04_Amount As String
            Get

                Dim I As Integer = 11
                Dim _Val As String = _Amount
                ' Return (Left(_Val, I) & "").PadLeft(I - Len(Left(_Val, I)), "0")
                Return (Left(_Val, I) & "").PadLeft(I, "0")
            End Get
            Set(ByVal value As String)
                _Amount = value
            End Set
        End Property

        ''' <summary>
        ''' 5  NAME  Start 29 To 58 Length 30 Type String  Optional
        ''' </summary>
        Private _EmpName As String = ""
        Public Property F05_EmpName As String
            Get
                Dim I As Integer = 30
                Dim _Val As String = _EmpName
                'Return (Left(_Val, I) & "").PadRight(I - Len(Left(_Val, I)), " ")
                Return (Left(_Val, I) & "").PadRight(I, " ")
            End Get
            Set(ByVal value As String)
                _EmpName = value
            End Set
        End Property

        ''' <summary>
        ''' 6 STATUS-RETURN  Start 59 To 59 Length 1 Type String  0=COMPLETE , 1=REJECT bank create Defrault Blank
        ''' </summary>
        Private _StatusReturn As String = " "
        Public Property F06_StatusReturn As String
            Get
                Dim I As Integer = 1
                Dim _Val As String = _StatusReturn
                ' Return (Left(_Val, I) & "").PadRight(I - Len(Left(_Val, I)), " ")
                Return (Left(_Val, I) & "").PadRight(I, " ")
            End Get
            Set(ByVal value As String)
                _StatusReturn = value
            End Set
        End Property

        ''' <summary>
        ''' 7 Space  Start 60 To 80 Length 21 Type String  
        ''' </summary>
        Private _LastSpace As String = "T"
        Public Property F07_LastSpace As String
            Get
                Dim I As Integer = 21
                Dim _Val As String = _LastSpace
                'Return (Left(_Val, I) & "").PadRight(I - Len(Left(_Val, I)), " ")
                Return (Left(_Val, I) & "").PadRight(I, " ")
            End Get
            Set(ByVal value As String)
                _LastSpace = value
            End Set
        End Property

    End Class

End Class
