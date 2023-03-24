Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Filter
Imports DevExpress.XtraPrinting
Imports System.Data.Common
Imports System.Windows.Forms
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.Configuration
Imports System.Diagnostics
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.IO
Imports System.Text
Imports System.Net
Imports Microsoft.Win32
Imports System.Web
Imports System.Runtime.Serialization
Imports System.ComponentModel
Imports System.Xml

Public Class wImportExcelNIKEPOTracking


#Region "Handler Control"

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        'With RepositoryFTMainMatCode
        '    AddHandler .Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
        '    AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
        '    AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
        '    AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave

        'End With

    End Sub


    Private _ProcComplete As Boolean = False
    Public Property ProcComplete As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(value As Boolean)
            _ProcComplete = value
        End Set
    End Property

#End Region

#Region "Property"

    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(ByVal value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(ByVal value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(ByVal value As String)
            _CallMethodParm = value
        End Set
    End Property

#End Region

#Region "MAIN PROC"

    Private Sub wOrderListingInfo_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load


    End Sub


    Private Sub ocmexit_Click_1(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click

        ogcdt1.DataSource = Nothing
        ogvdt1.Columns.Clear()

        If (FTStartGacDate.Text <> "" And FTEndGacDate.Text <> "") Or (FTStartDocDate.Text <> "" And FTEndDocDate.Text <> "") Then

            Dim cmdstring As String = ""



            If FNImportOrderDataType.SelectedIndex = 1 Then
                cmdstring = "  Select *"
                cmdstring &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTNIKEPO_HISTORY As X WITH(NOLOCK) "

                cmdstring &= vbCrLf & " WHERE X.Seq > 0 "
            Else
                cmdstring = "  Select *,'Import Order' AS FTHistoryType "
                cmdstring &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTNIKEPO As X WITH(NOLOCK) "

                cmdstring &= vbCrLf & " WHERE X.Seq > 0 "
            End If


            If (FTStartGacDate.Text <> "" And FTEndGacDate.Text <> "") Then
                cmdstring &= vbCrLf & " AND  Convert(varchar(10),X.[GAC],111) >='" & HI.UL.ULDate.ConvertEnDB(FTStartGacDate.Text) & "' "
                cmdstring &= vbCrLf & " AND  Convert(varchar(10),X.[GAC],111) <='" & HI.UL.ULDate.ConvertEnDB(FTEndGacDate.Text) & "' "

            End If

            If (FTStartDocDate.Text <> "" And FTEndDocDate.Text <> "") Then

                cmdstring &= vbCrLf & " AND  Convert(varchar(10),X.[Document Date],111) >='" & HI.UL.ULDate.ConvertEnDB(FTStartDocDate.Text) & "' "
                cmdstring &= vbCrLf & " AND  Convert(varchar(10),X.[Document Date],111) <='" & HI.UL.ULDate.ConvertEnDB(FTEndDocDate.Text) & "' "


            End If

            Dim Spls As New HI.TL.SplashScreen("Loading data ... Please wait")

            Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

            ogcdt1.DataSource = dt.Copy

            dt.Dispose()

            Spls.Close()


        Else

            HI.MG.ShowMsg.mInfo("Please Select Condition !!!", 220354794, Me.Text,, MessageBoxIcon.Warning)

        End If

    End Sub


#End Region

End Class
