Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid.Drawing
Imports DevExpress.Utils.Drawing
Imports DevExpress.XtraGrid.Drawing
Imports DevExpress.Utils
Imports DevExpress.XtraEditors.ViewInfo

Public Class MyGridPainter
    Inherits GridPainter


    Public Sub New(ByVal view As GridView)
        MyBase.New(view)

    End Sub

    Private _IsCustomPainting As Boolean
    Public Property IsCustomPainting() As Boolean
        Get
            Return _IsCustomPainting
        End Get
        Set(ByVal value As Boolean)
            _IsCustomPainting = value
        End Set
    End Property

    Public Overloads Sub DrawMergedCell(ByVal cell As MyMergedCell, ByVal e As PaintEventArgs)
        Dim delta As Integer = cell.Column1.VisibleIndex - cell.Column2.VisibleIndex
        If Math.Abs(delta) > 1 Then
            Return
        End If
        Dim vi As GridViewInfo = TryCast(View.GetViewInfo(), GridViewInfo)
        Dim gridCellInfo1 As GridCellInfo = vi.GetGridCellInfo(cell.RowHandle, cell.Column1)
        Dim gridCellInfo2 As GridCellInfo = vi.GetGridCellInfo(cell.RowHandle, cell.Column2)
        If gridCellInfo1 Is Nothing OrElse gridCellInfo2 Is Nothing Then
            Return
        End If
        Dim targetRect As Rectangle = Rectangle.Union(gridCellInfo1.Bounds, gridCellInfo2.Bounds)
        gridCellInfo1.Bounds = targetRect
        gridCellInfo1.CellValueRect = targetRect
        gridCellInfo2.Bounds = targetRect
        gridCellInfo2.CellValueRect = targetRect
        If delta < 0 Then
            gridCellInfo1 = gridCellInfo2
        End If
        Dim bounds As Rectangle = gridCellInfo1.ViewInfo.Bounds
        bounds.Width = targetRect.Width
        bounds.Height = targetRect.Height
        gridCellInfo1.ViewInfo.Bounds = bounds
        gridCellInfo1.ViewInfo.CalcViewInfo(e.Graphics)
        IsCustomPainting = True
        Dim cache As New GraphicsCache(e.Graphics)
        gridCellInfo1.Appearance.FillRectangle(cache, gridCellInfo1.Bounds)
        DrawRowCell(New GridViewDrawArgs(cache, vi, vi.ViewRects.Bounds), gridCellInfo1)
        IsCustomPainting = False

    End Sub

End Class