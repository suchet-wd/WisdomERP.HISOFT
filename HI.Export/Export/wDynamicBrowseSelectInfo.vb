Imports System.Data
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views
Imports DevExpress.Utils
Imports System.Globalization
Imports System.Threading
Imports System.Globalization.DateTimeFormatInfo
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Public Class wDynamicBrowseSelectInfo

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    

    Private Sub GetData(BrwID As Object, Optional ByVal _SpaWhere As String = "")
        Try
            Dim _Qrysql As String = ""

            Dim _dtbrw As System.Data.DataTable
            Dim _dtret As System.Data.DataTable
            Dim _BrowseCmd As String = ""
            Dim _BrowseSortCmd As String = ""
            Dim _BrowseWhereCmd As String = ""
            Dim _FTBrwCmdField As String = ""
            Dim _FTBrwCmdFieldOptional As String = ""
            Dim FTBrwCmdGroupBy As String = ""

            _Qrysql = " SELECT  TOP 1    BrwID, "

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qrysql &= vbCrLf & " FTBrwCmdTH AS FTBrwCmd "
            Else
                _Qrysql &= vbCrLf & " FTBrwCmdEN AS FTBrwCmd "
            End If

            _Qrysql &= vbCrLf & ", BrwRetID,FTConField,FTBrwCmdSort,FTBrwCmdWhere,FTBrwCmdField,FTBrwCmdFieldOptional,FTBrwCmdENGroupBy,FTBrwCmdTHGroupBy "
            _Qrysql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowse  With (NOLOCK) "
            _Qrysql &= vbCrLf & " WHERE BrwID=" & BrwID & " "

            _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)

            _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)
            _Qrysql = ""
            For Each Row As DataRow In _dtbrw.Rows
                _Qrysql = " SELECT  BrwRetID, FNSeq, FTBrwField, FTRetField,Convert(nvarchar(500),'') AS ValuesRet,FTStatePropertyTag "
                _Qrysql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowseRet With (NOLOCK) "
                _Qrysql &= vbCrLf & " WHERE BrwRetID=" & Val(Row!BrwRetID.ToString) & " "

                _dtret = New System.Data.DataTable
                _dtret = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)

                _BrowseCmd = Row!FTBrwCmd.ToString
                _BrowseSortCmd = Row!FTBrwCmdSort.ToString
                _BrowseWhereCmd = Row!FTBrwCmdWhere.ToString

                _FTBrwCmdField = Row!FTBrwCmdField.ToString
                _FTBrwCmdFieldOptional = Row!FTBrwCmdFieldOptional.ToString

                If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                    FTBrwCmdGroupBy = Row!FTBrwCmdTHGroupBy.ToString
                Else
                    FTBrwCmdGroupBy = Row!FTBrwCmdENGroupBy.ToString
                End If

                _Qrysql = Row!FTBrwCmd.ToString
            Next

            If _Qrysql = "" Then Exit Sub

            Dim _Where As String = ""


            Dim I As Integer = 0

            If _SpaWhere <> "" Then
                If _Where = "" Then
                    _Where &= " WHERE " & _SpaWhere
                Else
                    _Where &= " AND " & _SpaWhere
                End If
            End If

            _dtbrw = New System.Data.DataTable
            _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql & "  " & _BrowseWhereCmd & "  " & _Where & "  " & FTBrwCmdGroupBy & "  " & _BrowseSortCmd, Conn.DB.DataBaseName.DB_SYSTEM)
            _dtbrw.Columns.Add("FTSelect", GetType(String))
            With _dtbrw
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    R!FTSelect = "0"
                Next
                .AcceptChanges()
            End With
            Me.Data = _dtbrw
        Catch ex As Exception

        End Try
    End Sub

#Region "Property"
    Private _Proc As Boolean
    Public Property Proc As Boolean
        Get
            Return _Proc
        End Get
        Set(value As Boolean)
            _Proc = value
        End Set
    End Property

    Private _ChoseData As Boolean = False
    Private _Caption As String = "Browse Data"
    Public Property Caption() As String
        Get
            Return _Caption
        End Get
        Set(value As String)
            _Caption = value
        End Set
    End Property

    Private _Data As System.Data.DataTable
    Public Property Data() As System.Data.DataTable
        Get
            Return _Data
        End Get
        Set(value As System.Data.DataTable)
            _Data = value.Copy
        End Set
    End Property

    Private _DataRetField As System.Data.DataTable
    Public Property DataRetField() As System.Data.DataTable
        Get
            Return _DataRetField
        End Get
        Set(value As System.Data.DataTable)
            _DataRetField = value.Copy
        End Set
    End Property

    Private _FormSize As Integer = 300
    Public Property BrowseSize() As Integer

        Get
            Return _FormSize
        End Get

        Set(value As Integer)
            _FormSize = value
        End Set

    End Property

    Private _KeyReturn As String = ""
    Public Property KeyReturn() As String
        Get
            Return _KeyReturn
        End Get
        Set(value As String)
            _KeyReturn = value
        End Set
    End Property

    Private _ValuesReturn As DataRow
    Public Property ValuesReturn() As DataRow
        Get
            Return _ValuesReturn
        End Get
        Set(value As DataRow)
            _ValuesReturn = value
        End Set
    End Property

    Private _BrowseID As Integer
    Public Property BrowseID() As Integer
        Get
            Return _BrowseID
        End Get
        Set(value As Integer)
            _BrowseID = value
        End Set
    End Property

    Private _X As Integer = 0
    Public Property X As Integer
        Get
            Return _X
        End Get
        Set(value As Integer)
            _X = value
        End Set
    End Property

    Private _Y As Integer = 0
    Public Property Y As Integer
        Get
            Return _Y
        End Get
        Set(value As Integer)
            _Y = value
        End Set
    End Property

#End Region

    Private Sub wBrowseItemInfo_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            _Data.Dispose()
            If _ChoseData = False Then
                Me.ValuesReturn = Nothing
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub wBrowseItemInfo_Load(sender As System.Object, e As System.EventArgs) 'Handles MyBase.Load
        Try
            _ChoseData = False

            Dim _BrwSize As Integer = Me.Width
            _BrwSize = _BrwSize + 50
            If _BrwSize > My.Computer.Screen.Bounds.Width Then
                _BrwSize = (My.Computer.Screen.Bounds.Width * 80) / 100
            End If

            Dim X As Integer = 0
            Dim Y As Integer = 0

            If Me.X <> -1 Then
                X = Me.X - 50
            Else
                X = MousePosition.X - 50
            End If

            If X < 0 Then X = 0
            If Me.Y <> -1 Then
                Y = Me.Y - 100
            Else
                Y = MousePosition.Y - 100
            End If

            If Y < 0 Then Y = 0

            If My.Computer.Screen.Bounds.Width < X + Me.Width Then
                X = (My.Computer.Screen.Bounds.Width - Me.Width)
            End If

            If My.Computer.Screen.Bounds.Height < Y + Me.Height + 50 Then
                Y = (My.Computer.Screen.Bounds.Height - Me.Height) - 50
            End If

            Me.ogcbrowse.DataSource = Me.Data
            Me.Location = New System.Drawing.Point(X, Y)

            Me.ValuesReturn = Nothing

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub otbClose_Click(sender As Object, e As EventArgs) Handles otbClose.Click
        Try
            _Proc = False
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub obtSelect_Click_1(sender As Object, e As EventArgs) Handles obtSelect.Click
        Try
            _Proc = True
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub
End Class