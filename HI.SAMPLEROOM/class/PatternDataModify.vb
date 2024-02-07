Imports System
Imports DevExpress.Xpo

Public Class PatternDataModify
    Inherits XPObject

    Private Job As String
    Private PTDate As Date
    Private Remark As String

    Public Sub New()
        MyBase.New()
        ' This constructor is used when an object is loaded from a persistent storage.
        ' Do not place any code here.			
    End Sub

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
        ' This constructor is used when an object is loaded from a persistent storage.
        ' Do not place any code here.			
    End Sub

    Public Sub New(job As String, pTDate As Date, remark As String)
        Me.Job = job
        Me.PTDate = pTDate
        Me.Remark = remark
    End Sub

    Public Property PTDate1 As Date
        Get
            Return PTDate
        End Get
        Set(value As Date)
            PTDate = value
        End Set
    End Property

    Public Property Remark1 As String
        Get
            Return Remark
        End Get
        Set(value As String)
            Remark = value
        End Set
    End Property

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place here your initialization code.
    End Sub
End Class