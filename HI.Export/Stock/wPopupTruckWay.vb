Imports System.Windows.Forms
Imports DevExpress.Data
Imports DevExpress.Data.Filtering
Imports DevExpress.Utils
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Public Class wPopupTruckWay

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


    End Sub

    Private _DocNo As String = ""
    Public Property DocNo As String
        Get
            Return _DocNo
        End Get
        Set(value As String)
            _DocNo = value
        End Set
    End Property

    Private _WhId As Integer = 0
    Public Property WhId As Integer
        Get
            Return _WhId
        End Get
        Set(value As Integer)
            _WhId = value
        End Set
    End Property

    Public _CmpId As Integer = 0
    Public Property CmpId As Integer
        Get
            Return _CmpId
        End Get
        Set(value As Integer)
            _CmpId = value
        End Set
    End Property

    Private _StateSetSelectAll As Boolean = True


#Region "Procedure"
    Private _ds As New DataSet
#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        _State = False
        Me.Close()
    End Sub

    Private _oDtselect As DataTable = Nothing
    Public Property oDtselect As DataTable
        Get
            Return _oDtselect
        End Get
        Set(value As DataTable)
            _oDtselect = value
        End Set
    End Property
    Private _State As Boolean = False
    Public Property State As Boolean
        Get
            Return _State
        End Get
        Set(value As Boolean)
            _State = value
        End Set
    End Property


    Private pDt As New DataTable


    Private Sub SaveLayout(ByVal ObjParent As Object, ByVal MainParent As Object)

        On Error Resume Next
        For Each Obj As Object In ObjParent.Controls
            Select Case True
                Case (TypeOf Obj Is DevExpress.XtraGrid.GridControl)
                    HI.UL.AppRegistry.SaveLayoutGridToRegistry(MainParent, CType(Obj, DevExpress.XtraGrid.GridControl).MainView)
                Case False
            End Select

            If Obj.Controls.count > 0 Then
                SaveLayout(Obj, MainParent)
            End If
        Next


    End Sub


    Private Sub LoadLayout(ByVal ObjParent As Object, ByVal MainParent As Object)

        On Error Resume Next
        For Each Obj As Object In ObjParent.Controls
            Select Case True
                Case (TypeOf Obj Is DevExpress.XtraGrid.GridControl)

                    Call HI.UL.AppRegistry.LoadLayoutGridFromRegistry(MainParent, CType(Obj, DevExpress.XtraGrid.GridControl).MainView)

                Case False
            End Select

            If Obj.Controls.count > 0 Then
                LoadLayout(Obj, MainParent)
            End If
        Next


    End Sub

    Private Sub ocmSAVE_Click(sender As Object, e As EventArgs) Handles ocmSAVE.Click
        _State = True
        Me.Close()
    End Sub



End Class