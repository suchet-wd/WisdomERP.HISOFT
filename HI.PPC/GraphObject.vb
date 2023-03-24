Public Class GraphObject

    Private _GraphNo As String
    Public Property GraphNo() As String
        Get
            Return _GraphNo
        End Get
        Set(ByVal value As String)
            _GraphNo = value
        End Set
    End Property

    Private _FNHSysUnitSectId As Integer
    Public Property FNHSysUnitSectId() As Integer
        Get
            Return _FNHSysUnitSectId
        End Get
        Set(ByVal value As Integer)
            _FNHSysUnitSectId = value
        End Set
    End Property


    Private _SewingLineNo As String
    Public Property SewingLineNo() As String
        Get
            Return _SewingLineNo
        End Get
        Set(ByVal value As String)
            _SewingLineNo = value
        End Set
    End Property

    Private _GraphSDate As String
    Public Property GraphSDate() As String
        Get
            Return _GraphSDate
        End Get
        Set(ByVal value As String)
            _GraphSDate = value
        End Set
    End Property

    Private _GraphEDate As String
    Public Property GraphEDate() As String
        Get
            Return _GraphEDate
        End Get
        Set(ByVal value As String)
            _GraphEDate = value
        End Set
    End Property

    Private _TotalEmp As Integer = 0
    Public Property TotalEmp() As Integer
        Get
            Return _TotalEmp
        End Get
        Set(ByVal value As Integer)
            _TotalEmp = value
        End Set
    End Property

    Private _WorkTimeMin As Integer = 0
    Public Property WorkTimeMin() As Integer
        Get
            Return _WorkTimeMin
        End Get
        Set(ByVal value As Integer)
            _WorkTimeMin = value
        End Set
    End Property

    Private _LostTimeMin As Integer = 0
    Public Property LostTimeMin() As Integer
        Get
            Return _LostTimeMin
        End Get
        Set(ByVal value As Integer)
            _LostTimeMin = value
        End Set
    End Property

    Private _CapacityPerDay As Integer = 0
    Public Property CapacityPerDay() As Integer
        Get
            Return _CapacityPerDay
        End Get
        Set(ByVal value As Integer)
            _CapacityPerDay = value
        End Set
    End Property

    Private _OrderNo As String
    Public Property OrderNo() As String
        Get
            Return _OrderNo
        End Get
        Set(ByVal value As String)
            _OrderNo = value
        End Set
    End Property

    Private _OrderSubNo As String
    Public Property OrderSubNo() As String
        Get
            Return _OrderSubNo
        End Get
        Set(ByVal value As String)
            _OrderSubNo = value
        End Set
    End Property

    Private _StyleNo As String
    Public Property StyleNo() As String
        Get
            Return _StyleNo
        End Get
        Set(ByVal value As String)
            _StyleNo = value
        End Set
    End Property

    Private _SamData As Double = 0
    Public Property SamData() As Double
        Get
            Return _SamData
        End Get
        Set(ByVal value As Double)
            _SamData = value
        End Set
    End Property

    Private _Season As String
    Public Property Season() As String
        Get
            Return _Season
        End Get
        Set(ByVal value As String)
            _Season = value
        End Set
    End Property

    Private _Customer As String
    Public Property Customer() As String
        Get
            Return _Customer
        End Get
        Set(ByVal value As String)
            _Customer = value
        End Set
    End Property

    Private _CustomerPO As String
    Public Property CustomerPO() As String
        Get
            Return _CustomerPO
        End Get
        Set(ByVal value As String)
            _CustomerPO = value
        End Set
    End Property

    Private _Shipment As String
    Public Property Shipment() As String
        Get
            Return _Shipment
        End Get
        Set(ByVal value As String)
            _Shipment = value
        End Set
    End Property

    Private _ShipContinent As String
    Public Property ShipContinent() As String
        Get
            Return _ShipContinent
        End Get
        Set(ByVal value As String)
            _ShipContinent = value
        End Set
    End Property

    Private _ShipCountry As String
    Public Property ShipCountry() As String
        Get
            Return _ShipCountry
        End Get
        Set(ByVal value As String)
            _ShipCountry = value
        End Set
    End Property

    Private _ShipMode As String
    Public Property ShipMode() As String
        Get
            Return _ShipMode
        End Get
        Set(ByVal value As String)
            _ShipMode = value
        End Set
    End Property

    Private _TotalOrder As Integer = 0
    Public Property TotalOrder() As Integer
        Get
            Return _TotalOrder
        End Get
        Set(ByVal value As Integer)
            _TotalOrder = value
        End Set
    End Property

    Private _TotalPlan As Integer = 0
    Public Property TotalPlan() As Integer
        Get
            Return _TotalPlan
        End Get
        Set(ByVal value As Integer)
            _TotalPlan = value
        End Set
    End Property

    Private _TotalFinish As Integer = 0
    Public Property TotalFinish() As Integer
        Get
            Return _TotalFinish
        End Get
        Set(ByVal value As Integer)
            _TotalFinish = value
        End Set
    End Property

    Private _TotalDay As Integer = 0
    Public Property TotalDay() As Integer
        Get
            Return _TotalDay
        End Get
        Set(ByVal value As Integer)
            _TotalDay = value
        End Set
    End Property

    Private _TotalHour As Integer = 0
    Public Property TotalHour() As Integer
        Get
            Return _TotalHour
        End Get
        Set(ByVal value As Integer)
            _TotalHour = value
        End Set
    End Property

    Private _StateLockDate As Boolean = False
    Public Property StateLockDate() As Boolean
        Get
            Return _StateLockDate
        End Get
        Set(ByVal value As Boolean)
            _StateLockDate = value
        End Set
    End Property

    Private _StateLockDateAt As String = ""
    Public Property StateLockDateAt() As String
        Get
            Return _StateLockDateAt
        End Get
        Set(ByVal value As String)
            _StateLockDateAt = value
        End Set
    End Property

    Private _TotalHoliDay As Integer = 0
    Public Property TotalHoliDay() As Integer
        Get
            Return _TotalHoliDay
        End Get
        Set(ByVal value As Integer)
            _TotalHoliDay = value
        End Set
    End Property

    Private _TotalWeekDay As Integer = 0
    Public Property TotalWeekDay() As Integer
        Get
            Return _TotalWeekDay
        End Get
        Set(ByVal value As Integer)
            _TotalWeekDay = value
        End Set
    End Property


    Private _OrderImage As System.Drawing.Image = Nothing
    Public Property OrderImage() As System.Drawing.Image
        Get
            Return _OrderImage
        End Get
        Set(ByVal value As System.Drawing.Image)
            _OrderImage = value
        End Set
    End Property

    Private _GraphRowIdx As Integer = 0
    Public Property GraphRowIdx() As Integer
        Get
            Return _GraphRowIdx
        End Get
        Set(ByVal value As Integer)
            _GraphRowIdx = value
        End Set
    End Property

    Private _GraphStartColIdx As Integer = 0
    Public Property GraphStartColIdx() As Integer
        Get
            Return _GraphStartColIdx
        End Get
        Set(ByVal value As Integer)
            _GraphStartColIdx = value
        End Set
    End Property

    Private _GraphToColIdx As Integer = 0
    Public Property GraphToColIdx() As Integer
        Get
            Return _GraphToColIdx
        End Get
        Set(ByVal value As Integer)
            _GraphToColIdx = value
        End Set
    End Property


    Private _GraphToTalCol As Integer = 0
    Public Property GraphToTalCol() As Integer
        Get
            Return _GraphToTalCol
        End Get
        Set(ByVal value As Integer)
            _GraphToTalCol = value
        End Set
    End Property

    Private _GraphData As String = ""
    Public Property GraphData() As String
        Get
            Return _GraphData
        End Get
        Set(ByVal value As String)
            _GraphData = value
        End Set
    End Property

    Private _GraphUserData As String = ""
    Public Property GraphUserData() As String
        Get
            Return _GraphUserData
        End Get
        Set(ByVal value As String)
            _GraphUserData = value
        End Set
    End Property

    Private _LearningCurve As String = ""
    Public Property LearningCurve() As String
        Get
            Return _LearningCurve
        End Get
        Set(ByVal value As String)
            _LearningCurve = value
        End Set
    End Property

    Private _SkillByStyle As Double = 100.0
    Public Property SkillByStyle() As Double
        Get
            Return _SkillByStyle
        End Get
        Set(ByVal value As Double)
            _SkillByStyle = value
        End Set
    End Property

    Private _GraphColor As System.Drawing.Color = Nothing
    Public Property GraphColor() As System.Drawing.Color
        Get
            Return _GraphColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            _GraphColor = value
        End Set
    End Property


    Private _GraphBGImgIdx As Integer = 0
    Public Property GraphBGImgIdx() As Integer
        Get
            Return _GraphBGImgIdx
        End Get
        Set(ByVal value As Integer)
            _GraphBGImgIdx = value
        End Set
    End Property

    Private _GraphStateNew As Boolean = False
    Public Property GraphStateNew() As Boolean
        Get
            Return _GraphStateNew
        End Get
        Set(ByVal value As Boolean)
            _GraphStateNew = value
        End Set
    End Property

    Private _GraphStateDelete As Boolean = False
    Public Property GraphStateDelete() As Boolean
        Get
            Return _GraphStateDelete
        End Get
        Set(ByVal value As Boolean)
            _GraphStateDelete = value
        End Set
    End Property

    Private _GraphDetail As List(Of GraphObjectDetail) = Nothing
    Public Property GraphDetail() As List(Of GraphObjectDetail)
        Get
            Return _GraphDetail
        End Get
        Set(ByVal value As List(Of GraphObjectDetail))
            _GraphDetail = value
        End Set
    End Property

    Private _GraphDetailColor As List(Of GraphObjectDetailColor) = Nothing
    Public Property GraphDetailColor() As List(Of GraphObjectDetailColor)
        Get
            Return _GraphDetailColor
        End Get
        Set(ByVal value As List(Of GraphObjectDetailColor))
            _GraphDetailColor = value
        End Set
    End Property

    Private _GraphDataWorkTime As DataTable = Nothing
    Public Property GraphDataWorkTime() As DataTable
        Get
            Return _GraphDataWorkTime
        End Get
        Set(ByVal value As DataTable)
            _GraphDataWorkTime = value
        End Set
    End Property

End Class

Public Class GraphObjectDetail

    Private _GraphNo As String
    Public Property GraphNo() As String
        Get
            Return _GraphNo
        End Get
        Set(ByVal value As String)
            _GraphNo = value
        End Set
    End Property

    Private _GraphDate As String
    Public Property GraphDate() As String
        Get
            Return _GraphDate
        End Get
        Set(ByVal value As String)
            _GraphDate = value
        End Set
    End Property

    Private _SamData As Double = 0
    Public Property SamData() As Double
        Get
            Return _SamData
        End Get
        Set(ByVal value As Double)
            _SamData = value
        End Set
    End Property

    Private _TotalEmp As Integer = 0
    Public Property TotalEmp() As Integer
        Get
            Return _TotalEmp
        End Get
        Set(ByVal value As Integer)
            _TotalEmp = value
        End Set
    End Property

    Private _WorkTimeMin As Integer = 0
    Public Property WorkTimeMin() As Integer
        Get
            Return _WorkTimeMin
        End Get
        Set(ByVal value As Integer)
            _WorkTimeMin = value
        End Set
    End Property

    Private _LostTimeMin As Integer = 0
    Public Property LostTimeMin() As Integer
        Get
            Return _LostTimeMin
        End Get
        Set(ByVal value As Integer)
            _LostTimeMin = value
        End Set
    End Property

    Private _LearningCurve As Double = 100.0
    Public Property LearningCurve() As Double
        Get
            Return _LearningCurve
        End Get
        Set(ByVal value As Double)
            _LearningCurve = value
        End Set
    End Property

    Private _CapacityPerDay As Integer = 0
    Public Property CapacityPerDay() As Integer
        Get
            Return _CapacityPerDay
        End Get
        Set(ByVal value As Integer)
            _CapacityPerDay = value
        End Set
    End Property

    Private _TotalHour As Integer = 0
    Public Property TotalHour() As Integer
        Get
            Return _TotalHour
        End Get
        Set(ByVal value As Integer)
            _TotalHour = value
        End Set
    End Property

    Private _TotalPlanQty As Integer = 0
    Public Property TotalPlanQty() As Integer
        Get
            Return _TotalPlanQty
        End Get
        Set(ByVal value As Integer)
            _TotalPlanQty = value
        End Set
    End Property

    Private _FinishQty As Integer = 0
    Public Property FinishQty() As Integer
        Get
            Return _FinishQty
        End Get
        Set(ByVal value As Integer)
            _FinishQty = value
        End Set
    End Property

    Private _FTStateFinishDate As Boolean = False
    Public Property FTStateFinishDate() As Boolean
        Get
            Return _FTStateFinishDate
        End Get
        Set(ByVal value As Boolean)
            _FTStateFinishDate = value
        End Set
    End Property

End Class

Public Class GraphObjectDetailColor

    Private _GraphNo As String
    Public Property GraphNo() As String
        Get
            Return _GraphNo
        End Get
        Set(ByVal value As String)
            _GraphNo = value
        End Set
    End Property

    Private _ColorWay As String
    Public Property ColorWay() As String
        Get
            Return _ColorWay
        End Get
        Set(ByVal value As String)
            _ColorWay = value
        End Set
    End Property

    Private _SizeBreakDown As String
    Public Property SizeBreakDown() As String
        Get
            Return _SizeBreakDown
        End Get
        Set(ByVal value As String)
            _SizeBreakDown = value
        End Set
    End Property

    Private _TotalPlanQty As Integer = 0
    Public Property TotalPlanQty() As Integer
        Get
            Return _TotalPlanQty
        End Get
        Set(ByVal value As Integer)
            _TotalPlanQty = value
        End Set
    End Property

    Private _FinishQty As Integer = 0
    Public Property FinishQty() As Integer
        Get
            Return _FinishQty
        End Get
        Set(ByVal value As Integer)
            _FinishQty = value
        End Set
    End Property

End Class