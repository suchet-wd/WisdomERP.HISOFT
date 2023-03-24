Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Text


Public Class ClsDrawing3d


    Public Class Camera
        Private loc As New Point3d(0, 0, 0)
        Private _d As Double = 500.0
        Private quan As New Quaternion(1, 0, 0, 0)

        Public Property Location() As Point3d
            Get
                Return loc
            End Get
            Set(value As Point3d)
                loc = value
            End Set
        End Property

        Public Property FocalDistance() As Double
            Get
                Return _d
            End Get
            Set(value As Double)
                _d = value
            End Set
        End Property

        Public Property Quaternion() As Quaternion
            Get
                Return quan
            End Get
            Set(value As Quaternion)
                quan = value
            End Set
        End Property

        Public Sub MoveRight(d As Double)
            loc.X += d
        End Sub

        Public Sub MoveLeft(d As Double)
            loc.X -= d
        End Sub

        Public Sub MoveUp(d As Double)
            loc.Y -= d
        End Sub

        Public Sub MoveDown(d As Double)
            loc.Y += d
        End Sub

        Public Sub MoveIn(d As Double)
            loc.Z += d
        End Sub

        Public Sub MoveOut(d As Double)
            loc.Z -= d
        End Sub

        Public Sub Roll(degree As Integer)
            ' rotate around Z axis
            Dim q As New Quaternion()
            q.FromAxisAngle(New Vector3d(0, 0, 1), degree * Math.PI / 180.0)
            quan = q * quan
        End Sub

        Public Sub Yaw(degree As Integer)
            ' rotate around Y axis
            Dim q As New Quaternion()
            q.FromAxisAngle(New Vector3d(0, 1, 0), degree * Math.PI / 180.0)
            quan = q * quan
        End Sub

        Public Sub Pitch(degree As Integer)
            ' rotate around X axis
            Dim q As New Quaternion()
            q.FromAxisAngle(New Vector3d(1, 0, 0), degree * Math.PI / 180.0)
            quan = q * quan
        End Sub

        Public Sub TurnUp(degree As Integer)
            Pitch(-degree)
        End Sub

        Public Sub TurnDown(degree As Integer)
            Pitch(degree)
        End Sub

        Public Sub TurnLeft(degree As Integer)
            Yaw(degree)
        End Sub

        Public Sub TurnRight(degree As Integer)
            Yaw(-degree)
        End Sub

        Public Function GetProjection(pts As Point3d()) As PointF()
            Dim pt2ds As PointF() = New PointF(pts.Length - 1) {}

            ' transform to new coordinates system which origin is camera location
            Dim pts1 As Point3d() = Point3d.Copy(pts)
            Point3d.Offset(pts1, -loc.X, -loc.Y, -loc.Z)

            ' rotate
            quan.Rotate(pts1)

            'project
            For i As Integer = 0 To pts.Length - 1
                If pts1(i).Z > 0.1 Then
                    pt2ds(i) = New PointF(CSng(loc.X + pts1(i).X * _d / pts1(i).Z), CSng(loc.Y + pts1(i).Y * _d / pts1(i).Z))
                Else
                    pt2ds(i) = New PointF(Single.MaxValue, Single.MaxValue)
                End If
            Next
            Return pt2ds
        End Function
    End Class


    Public Class Shape3d
        Protected pts As Point3d() = New Point3d(7) {}
        Public ReadOnly Property Point3dArray() As Point3d()
            Get
                Return pts
            End Get
        End Property

        Protected m_center As New Point3d(0, 0, 0)
        Public Property Center() As Point3d
            Get
                Return m_center
            End Get
            Set(value As Point3d)
                Dim dx As Double = value.X - m_center.X
                Dim dy As Double = value.Y - m_center.Y
                Dim dz As Double = value.Z - m_center.Z
                Point3d.Offset(pts, dx, dy, dz)
                m_center = value
            End Set
        End Property

        Protected m_lineColor As Color = Color.Black
        Public Property LineColor() As Color
            Get
                Return m_lineColor
            End Get
            Set(value As Color)
                m_lineColor = value
            End Set
        End Property

        Public Sub RotateAt(pt As Point3d, q As Quaternion)
            ' transform origin to pt
            Dim copy As Point3d() = Point3d.Copy(pts)
            Point3d.Offset(copy, -pt.X, -pt.Y, -pt.Z)

            ' rotate
            q.Rotate(copy)
            q.Rotate(m_center)

            ' transform to original origin
            Point3d.Offset(copy, pt.X, pt.Y, pt.Z)
            pts = copy
        End Sub

        Public Overridable Sub Draw(g As Graphics, cam As Camera)
        End Sub
    End Class


    Public Class Cuboid
        Inherits Shape3d
        Private m_drawingLine As Boolean = False, m_fillingFace As Boolean = True, m_drawingImage As Boolean = False
        Public Property DrawingLine() As Boolean
            Get
                Return m_drawingLine
            End Get
            Set(value As Boolean)
                m_drawingLine = value
            End Set
        End Property

        Public Property FillingFace() As Boolean
            Get
                Return m_fillingFace
            End Get
            Set(value As Boolean)
                m_fillingFace = value
            End Set
        End Property

        Public Property DrawingImage() As Boolean
            Get
                Return m_drawingImage
            End Get
            Set(value As Boolean)
                m_drawingImage = value
            End Set
        End Property

        Private faceColor As Color() = New Color(5) {Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Purple}
        Public Property FaceColorArray() As Color()
            Get
                Return faceColor
            End Get
            Set(value As Color())
                Dim n As Integer = Math.Min(value.Length, faceColor.Length)
                For i As Integer = 0 To n - 1
                    faceColor(i) = value(i)
                Next
            End Set
        End Property

        Private bmp As Bitmap() = New Bitmap(5) {}
        Public Property FaceImageArray() As Bitmap()
            Get
                Return bmp
            End Get
            Set(value As Bitmap())
                Dim n As Integer = Math.Min(value.Length, 6)
                For i As Integer = 0 To n - 1
                    bmp(i) = value(i)
                Next
                setupFilter()
            End Set
        End Property

        Private filters As YLScsDrawing.Imaging.Filters.FreeTransform() = New YLScsDrawing.Imaging.Filters.FreeTransform(5) {}
        Private Sub setupFilter()
            For i As Integer = 0 To 5
                filters(i) = New YLScsDrawing.Imaging.Filters.FreeTransform()
                filters(i).Bitmap = bmp(i)
            Next
        End Sub

        Public Sub New(a As Double, b As Double, c As Double)
            center = New Point3d(a / 2, b / 2, c / 2)
            pts(0) = New Point3d(0, 0, 0)
            pts(1) = New Point3d(a, 0, 0)
            pts(2) = New Point3d(a, b, 0)
            pts(3) = New Point3d(0, b, 0)
            pts(4) = New Point3d(0, 0, c)
            pts(5) = New Point3d(a, 0, c)
            pts(6) = New Point3d(a, b, c)
            pts(7) = New Point3d(0, b, c)
        End Sub


        Public Overrides Sub Draw(g As Graphics, cam As Camera)
            Dim pts2d As PointF() = cam.GetProjection(pts)

            Dim face As PointF()() = New PointF(5)() {}
            face(0) = New PointF() {pts2d(0), pts2d(1), pts2d(2), pts2d(3)}
            face(1) = New PointF() {pts2d(5), pts2d(1), pts2d(0), pts2d(4)}
            face(2) = New PointF() {pts2d(1), pts2d(5), pts2d(6), pts2d(2)}
            face(3) = New PointF() {pts2d(2), pts2d(6), pts2d(7), pts2d(3)}
            face(4) = New PointF() {pts2d(3), pts2d(7), pts2d(4), pts2d(0)}
            face(5) = New PointF() {pts2d(4), pts2d(7), pts2d(6), pts2d(5)}

            For i As Integer = 0 To 5
                Dim isout As Boolean = False
                For j As Integer = 0 To 3
                    If face(i)(j) = New PointF(Single.MaxValue, Single.MaxValue) Then
                        isout = True
                        Continue For
                    End If
                Next
                If Not isout Then
                    If m_drawingLine Then
                        g.DrawPolygon(New Pen(lineColor), face(i))
                    End If
                    If YLScsDrawing.Geometry.Vector.IsClockwise(face(i)(0), face(i)(1), face(i)(2)) Then
                        ' the face can be seen by camera
                        If m_fillingFace Then
                            g.FillPolygon(New SolidBrush(faceColor(i)), face(i))
                        End If
                        If m_drawingImage AndAlso bmp(i) IsNot Nothing Then
                            filters(i).FourCorners = face(i)
                            g.DrawImage(filters(i).Bitmap, filters(i).ImageLocation)
                        End If
                    End If
                End If
            Next
        End Sub
    End Class


Public Structure Point3d
        Public X As Double, Y As Double, Z As Double
        ' coordinate system follows right-hand rule
        Public Sub New(x__1 As Double, y__2 As Double, z__3 As Double)
            X = x__1
            Y = y__2
            Z = z__3
        End Sub

        Public Sub New(v As Vector3d)
            X = v.X
            Y = v.Y
            Z = v.Z
        End Sub


        Public Function Copy() As Point3d
            Return New Point3d(Me.X, Me.Y, Me.Z)
        End Function

        Public Function ToVector3d() As Vector3d
            Return New Vector3d(X, Y, Z)
        End Function

        Public Sub Offset(x As Double, y As Double, z As Double)
            Me.X += x
            Me.Y += y
            Me.Z += z
        End Sub

        Public Shared Function Copy(pts As Point3d()) As Point3d()
            Dim copy__1 As Point3d() = New Point3d(pts.Length - 1) {}
            For i As Integer = 0 To pts.Length - 1
                copy__1(i) = pts(i).Copy()
            Next
            Return copy__1
        End Function

        Public Shared Sub Offset(pts As Point3d(), offsetX As Double, offsetY As Double, offsetZ As Double)
            For i As Integer = 0 To pts.Length - 1
                pts(i).Offset(offsetX, offsetY, offsetZ)
            Next
        End Sub

        Public Function GetProjectedPoint(d As Double) As PointF
            ' project distance: from eye to screen
            Return New PointF(CSng(Me.X * d / (d + Me.Z)), CSng(Me.Y * d / (d + Me.Z)))
        End Function

        Public Shared Function Project(pts As Point3d(), d As Double) As PointF()
            ' project distance: from eye to screen
            Dim pt2ds As PointF() = New PointF(pts.Length - 1) {}
            For i As Integer = 0 To pts.Length - 1
                pt2ds(i) = pts(i).GetProjectedPoint(d)
            Next
            Return pt2ds
        End Function
    End Structure





    Public Structure Vector3d
        Public X As Double, Y As Double, Z As Double

        Public Sub New(x__1 As Double, y__2 As Double, z__3 As Double)
            X = x__1
            Y = y__2
            Z = z__3
        End Sub

        Public Sub New(pt As Point3d)
            X = pt.X
            Y = pt.Y
            Z = pt.Z
        End Sub

        Public Sub New(startPoint As Point3d, endPoint As Point3d)
            X = endPoint.X - startPoint.X
            Y = endPoint.Y - startPoint.Y
            Z = endPoint.Z - startPoint.Z
        End Sub

        Public ReadOnly Property Magnitude() As Double
            Get
                Return Math.Sqrt(X * X + Y * Y + Z * Z)
            End Get
        End Property

        Public Sub Normalise()
            Dim m As Double = Math.Sqrt(X * X + Y * Y + Z * Z)
            If m > 0.001 Then
                X /= m
                Y /= m
                Z /= m
            End If
        End Sub

        Public Shared Operator +(v1 As Vector3d, v2 As Vector3d) As Vector3d
            Return New Vector3d(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z)
        End Operator

        Public Shared Operator -(v1 As Vector3d, v2 As Vector3d) As Vector3d
            Return New Vector3d(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z)
        End Operator

        Public Shared Operator -(v As Vector3d) As Vector3d
            Return New Vector3d(-v.X, -v.Y, -v.Z)
        End Operator

        ' A x B = |A|*|B|*sin(angle), direction follow right-hand rule
        Public Shared Function CrossProduct(v1 As Vector3d, v2 As Vector3d) As Vector3d
            Return New Vector3d(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X)
        End Function

        Public Shared Function DotProduct(v1 As Vector3d, v2 As Vector3d) As Double
            ' A . B = |A|*|B|*cos(angle)
            Return (v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z)
        End Function

        Public Function CrossProduct(v As Vector3d) As Vector3d
            Return CrossProduct(Me, v)
        End Function

        Public Function DotProduct(v As Vector3d) As Double
            Return DotProduct(Me, v)
        End Function

        Public Shared Function isForeFace(pt1 As Point3d, pt2 As Point3d, pt3 As Point3d) As Boolean
            ' pts on a plane
            Dim v1 As New Vector3d(pt2, pt1)
            Dim v2 As New Vector3d(pt2, pt3)
            Dim v As Vector3d = v1.CrossProduct(v2)
            Return v.DotProduct(New Vector3d(0, 0, 1)) < 0
        End Function

        Public Shared Function isBackFace(pt1 As Point3d, pt2 As Point3d, pt3 As Point3d) As Boolean
            Dim v1 As New Vector3d(pt2, pt1)
            Dim v2 As New Vector3d(pt2, pt3)
            Dim v As Vector3d = v1.CrossProduct(v2)
            Return v.DotProduct(New Vector3d(0, 0, 1)) > 0
        End Function
    End Structure



    Public Structure Quaternion
        Public X As Double, Y As Double, Z As Double, W As Double

        Public Sub New(w__1 As Double, x__2 As Double, y__3 As Double, z__4 As Double)
            W = w__1
            X = x__2
            Y = y__3
            Z = z__4
        End Sub

        Public Sub New(w__1 As Double, v As Vector3d)
            W = w__1
            X = v.X
            Y = v.Y
            Z = v.Z
        End Sub

        Public Property V() As Vector3d
            Get
                Return New Vector3d(X, Y, Z)
            End Get
            Set(value As Vector3d)
                X = value.X
                Y = value.Y
                Z = value.Z
            End Set
        End Property

        Public Sub Normalise()
            Dim m As Double = W * W + X * X + Y * Y + Z * Z
            If m > 0.001 Then
                m = Math.Sqrt(m)
                W /= m
                X /= m
                Y /= m
                Z /= m
            Else
                W = 1
                X = 0
                Y = 0
                Z = 0
            End If
        End Sub

        Public Sub Conjugate()
            X = -X
            Y = -Y
            Z = -Z
        End Sub

        Public Sub FromAxisAngle(axis As Vector3d, angleRadian As Double)
            Dim m As Double = axis.Magnitude
            If m > 0.0001 Then
                Dim ca As Double = Math.Cos(angleRadian / 2)
                Dim sa As Double = Math.Sin(angleRadian / 2)
                X = axis.X / m * sa
                Y = axis.Y / m * sa
                Z = axis.Z / m * sa
                W = ca
            Else
                W = 1
                X = 0
                Y = 0
                Z = 0
            End If
        End Sub

        Public Function Copy() As Quaternion
            Return New Quaternion(W, X, Y, Z)
        End Function

        Public Sub Multiply(q As Quaternion)
            'Me *= q
        End Sub

        '                  -1
        ' V'=q*V*q     ,
        Public Sub Rotate(pt As Point3d)
            Me.Normalise()
            Dim q1 As Quaternion = Me.Copy()
            q1.Conjugate()

            Dim qNode As New Quaternion(0, pt.X, pt.Y, pt.Z)
            qNode = Me * qNode * q1
            pt.X = qNode.X
            pt.Y = qNode.Y
            pt.Z = qNode.Z
        End Sub

        Public Sub Rotate(nodes As Point3d())
            Me.Normalise()
            Dim q1 As Quaternion = Me.Copy()
            q1.Conjugate()
            For i As Integer = 0 To nodes.Length - 1
                Dim qNode As New Quaternion(0, nodes(i).X, nodes(i).Y, nodes(i).Z)
                qNode = Me * qNode * q1
                nodes(i).X = qNode.X
                nodes(i).Y = qNode.Y
                nodes(i).Z = qNode.Z
            Next
        End Sub

        ' Multiplying q1 with q2 is meaning of doing q2 firstly then q1
        Public Shared Operator *(q1 As Quaternion, q2 As Quaternion) As Quaternion
            Dim nw As Double = q1.W * q2.W - q1.X * q2.X - q1.Y * q2.Y - q1.Z * q2.Z
            Dim nx As Double = q1.W * q2.X + q1.X * q2.W + q1.Y * q2.Z - q1.Z * q2.Y
            Dim ny As Double = q1.W * q2.Y + q1.Y * q2.W + q1.Z * q2.X - q1.X * q2.Z
            Dim nz As Double = q1.W * q2.Z + q1.Z * q2.W + q1.X * q2.Y - q1.Y * q2.X
            Return New Quaternion(nw, nx, ny, nz)
        End Operator
    End Structure



    'Geometry Class

    Public Structure Vector
        Private _x As Double, _y As Double

        Public Sub New(x As Double, y As Double)
            _x = x
            _y = y
        End Sub
        Public Sub New(pt As PointF)
            _x = pt.X
            _y = pt.Y
        End Sub
        Public Sub New(st As PointF, [end] As PointF)
            _x = [end].X - st.X
            _y = [end].Y - st.Y
        End Sub

        Public Property X() As Double
            Get
                Return _x
            End Get
            Set(value As Double)
                _x = value
            End Set
        End Property

        Public Property Y() As Double
            Get
                Return _y
            End Get
            Set(value As Double)
                _y = value
            End Set
        End Property

        Public ReadOnly Property Magnitude() As Double
            Get
                Return Math.Sqrt(X * X + Y * Y)
            End Get
        End Property

        Public Shared Operator +(v1 As Vector, v2 As Vector) As Vector
            Return New Vector(v1.X + v2.X, v1.Y + v2.Y)
        End Operator

        Public Shared Operator -(v1 As Vector, v2 As Vector) As Vector
            Return New Vector(v1.X - v2.X, v1.Y - v2.Y)
        End Operator

        Public Shared Operator -(v As Vector) As Vector
            Return New Vector(-v.X, -v.Y)
        End Operator

        Public Shared Operator *(c As Double, v As Vector) As Vector
            Return New Vector(c * v.X, c * v.Y)
        End Operator

        Public Shared Operator *(v As Vector, c As Double) As Vector
            Return New Vector(c * v.X, c * v.Y)
        End Operator

        Public Shared Operator /(v As Vector, c As Double) As Vector
            Return New Vector(v.X / c, v.Y / c)
        End Operator

        ' A * B =|A|.|B|.sin(angle AOB)
        Public Function CrossProduct(v As Vector) As Double
            Return _x * v.Y - v.X * _y
        End Function

        ' A. B=|A|.|B|.cos(angle AOB)
        Public Function DotProduct(v As Vector) As Double
            Return _x * v.X + _y * v.Y
        End Function

        Public Shared Function IsClockwise(pt1 As PointF, pt2 As PointF, pt3 As PointF) As Boolean
            Dim V21 As New Vector(pt2, pt1)
            Dim v23 As New Vector(pt2, pt3)
            Return V21.CrossProduct(v23) < 0
            ' sin(angle pt1 pt2 pt3) > 0, 0<angle pt1 pt2 pt3 <180
        End Function

        Public Shared Function IsCCW(pt1 As PointF, pt2 As PointF, pt3 As PointF) As Boolean
            Dim V21 As New Vector(pt2, pt1)
            Dim v23 As New Vector(pt2, pt3)
            Return V21.CrossProduct(v23) > 0
            ' sin(angle pt2 pt1 pt3) < 0, 180<angle pt2 pt1 pt3 <360
        End Function

        Public Shared Function DistancePointLine(pt As PointF, lnA As PointF, lnB As PointF) As Double
            Dim v1 As New Vector(lnA, lnB)
            Dim v2 As New Vector(lnA, pt)
            v1 /= v1.Magnitude
            Return Math.Abs(v2.CrossProduct(v1))
        End Function

        Public Sub Rotate(Degree As Integer)
            Dim radian As Double = Degree * Math.PI / 180.0
            Dim sin As Double = Math.Sin(radian)
            Dim cos As Double = Math.Cos(radian)
            Dim nx As Double = _x * cos - _y * sin
            Dim ny As Double = _x * sin + _y * cos
            _x = nx
            _y = ny
        End Sub

        Public Function ToPointF() As PointF
            Return New PointF(CSng(_x), CSng(_y))
        End Function
    End Structure




    'Image Data

    Public Class ImageData
        'Implements IDisposable
        Private _red As Byte(,), _green As Byte(,), _blue As Byte(,), _alpha As Byte(,)
        Private _disposed As Boolean = False

        Public Property A() As Byte(,)
            Get
                Return _alpha
            End Get
            Set(value As Byte(,))
                _alpha = value
            End Set
        End Property
        Public Property B() As Byte(,)
            Get
                Return _blue
            End Get
            Set(value As Byte(,))
                _blue = value
            End Set
        End Property
        Public Property G() As Byte(,)
            Get
                Return _green
            End Get
            Set(value As Byte(,))
                _green = value
            End Set
        End Property
        Public Property R() As Byte(,)
            Get
                Return _red
            End Get
            Set(value As Byte(,))
                _red = value
            End Set
        End Property

        Public Function Clone() As ImageData
            Dim cb As New ImageData()
            cb.A = DirectCast(_alpha.Clone(), Byte(,))
            cb.B = DirectCast(_blue.Clone(), Byte(,))
            cb.G = DirectCast(_green.Clone(), Byte(,))
            cb.R = DirectCast(_red.Clone(), Byte(,))
            Return cb
        End Function

#Region "InteropServices.Marshal mathods"
        Public Sub FromBitmap(srcBmp As Bitmap)
            Dim w As Integer = srcBmp.Width
            Dim h As Integer = srcBmp.Height

            _alpha = New Byte(w - 1, h - 1) {}
            _blue = New Byte(w - 1, h - 1) {}
            _green = New Byte(w - 1, h - 1) {}
            _red = New Byte(w - 1, h - 1) {}

            ' Lock the bitmap's bits.  
            Dim bmpData As System.Drawing.Imaging.BitmapData = srcBmp.LockBits(New Rectangle(0, 0, w, h), System.Drawing.Imaging.ImageLockMode.ReadWrite, Imaging.PixelFormat.Format32bppArgb)
            ' Get the address of the first line.
            Dim ptr As IntPtr = bmpData.Scan0

            ' Declare an array to hold the bytes of the bitmap.
            Dim bytes As Integer = bmpData.Stride * srcBmp.Height
            Dim rgbValues As Byte() = New Byte(bytes - 1) {}

            ' Copy the RGB values
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes)

            Dim offset As Integer = bmpData.Stride - w * 4

            Dim index As Integer = 0
            For y As Integer = 0 To h - 1
                For x As Integer = 0 To w - 1
                    _blue(x, y) = rgbValues(index)
                    _green(x, y) = rgbValues(index + 1)
                    _red(x, y) = rgbValues(index + 2)
                    _alpha(x, y) = rgbValues(index + 3)
                    index += 4
                Next
                index += offset
            Next

            ' Unlock the bits.
            srcBmp.UnlockBits(bmpData)
        End Sub

        Public Function ToBitmap() As Bitmap
            Dim width As Integer = 0, height As Integer = 0
            If _alpha IsNot Nothing Then
                width = Math.Max(width, _alpha.GetLength(0))
                height = Math.Max(height, _alpha.GetLength(1))
            End If
            If _blue IsNot Nothing Then
                width = Math.Max(width, _blue.GetLength(0))
                height = Math.Max(height, _blue.GetLength(1))
            End If
            If _green IsNot Nothing Then
                width = Math.Max(width, _green.GetLength(0))
                height = Math.Max(height, _green.GetLength(1))
            End If
            If _red IsNot Nothing Then
                width = Math.Max(width, _red.GetLength(0))
                height = Math.Max(height, _red.GetLength(1))
            End If
            Dim bmp As New Bitmap(width, height, Imaging.PixelFormat.Format32bppArgb)
            Dim bmpData As System.Drawing.Imaging.BitmapData = bmp.LockBits(New Rectangle(0, 0, width, height), System.Drawing.Imaging.ImageLockMode.ReadWrite, Imaging.PixelFormat.Format32bppArgb)

            ' Get the address of the first line.
            Dim ptr As IntPtr = bmpData.Scan0

            ' Declare an array to hold the bytes of the bitmap.
            Dim bytes As Integer = bmpData.Stride * bmp.Height
            Dim rgbValues As Byte() = New Byte(bytes - 1) {}

            ' set rgbValues
            Dim offset As Integer = bmpData.Stride - width * 4
            Dim i As Integer = 0
            For y As Integer = 0 To height - 1
                For x As Integer = 0 To width - 1
                    rgbValues(i) = If(checkArray(_blue, x, y), _blue(x, y), CByte(0))
                    rgbValues(i + 1) = If(checkArray(_green, x, y), _green(x, y), CByte(0))
                    rgbValues(i + 2) = If(checkArray(_red, x, y), _red(x, y), CByte(0))
                    rgbValues(i + 3) = If(checkArray(_alpha, x, y), _alpha(x, y), CByte(255))
                    i += 4
                Next
                i += offset
            Next

            ' Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes)

            ' Unlock the bits.
            bmp.UnlockBits(bmpData)
            Return bmp
        End Function
#End Region

        Private Shared Function checkArray(array As Byte(,), x As Integer, y As Integer) As Boolean
            If array Is Nothing Then
                Return False
            End If
            If x < array.GetLength(0) AndAlso y < array.GetLength(1) Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Sub Dispose()
            Dispose(True)

            ' Use SupressFinalize in case a subclass
            ' of this type implements a finalizer.
            GC.SuppressFinalize(Me)
        End Sub

        Protected Overridable Sub Dispose(disposing As Boolean)
            ' If you need thread safety, use a lock around these 
            ' operations, as well as in your methods that use the resource.
            If Not _disposed Then
                If disposing Then
                    _alpha = Nothing
                    _blue = Nothing
                    _green = Nothing
                    _red = Nothing
                End If

                ' Indicate that the instance has been disposed.
                _disposed = True
            End If
        End Sub
    End Class




    'Class FreeTransForm

    Public Class FreeTransform
        Private vertex As PointF() = New PointF(3) {}
        Private AB As YLScsDrawing.Geometry.Vector, BC As YLScsDrawing.Geometry.Vector, CD As YLScsDrawing.Geometry.Vector, DA As YLScsDrawing.Geometry.Vector
        Private rect As New Rectangle()
        Private srcCB As ImageData = New ImageData()
        Private srcW As Integer = 0
        Private srcH As Integer = 0

        Public Property Bitmap() As Bitmap
            Get
                Return getTransformedBitmap()
            End Get
            Set(value As Bitmap)
                Try
                    srcCB.FromBitmap(value)
                    srcH = value.Height
                    srcW = value.Width
                Catch
                    srcW = 0
                    srcH = 0
                End Try
            End Set
        End Property

        Public Property ImageLocation() As Point
            Get
                Return rect.Location
            End Get
            Set(value As Point)
                rect.Location = value
            End Set
        End Property

        Private isBilinear As Boolean = False
        Public Property IsBilinearInterpolation() As Boolean
            Get
                Return isBilinear
            End Get
            Set(value As Boolean)
                isBilinear = value
            End Set
        End Property

        Public ReadOnly Property ImageWidth() As Integer
            Get
                Return rect.Width
            End Get
        End Property

        Public ReadOnly Property ImageHeight() As Integer
            Get
                Return rect.Height
            End Get
        End Property

        Public Property VertexLeftTop() As PointF
            Get
                Return vertex(0)
            End Get
            Set(value As PointF)
                vertex(0) = value
                setVertex()
            End Set
        End Property

        Public Property VertexTopRight() As PointF
            Get
                Return vertex(1)
            End Get
            Set(value As PointF)
                vertex(1) = value
                setVertex()
            End Set
        End Property

        Public Property VertexRightBottom() As PointF
            Get
                Return vertex(2)
            End Get
            Set(value As PointF)
                vertex(2) = value
                setVertex()
            End Set
        End Property

        Public Property VertexBottomLeft() As PointF
            Get
                Return vertex(3)
            End Get
            Set(value As PointF)
                vertex(3) = value
                setVertex()
            End Set
        End Property

        Public Property FourCorners() As PointF()
            Get
                Return vertex
            End Get
            Set(value As PointF())
                vertex = value
                setVertex()
            End Set
        End Property

        Private Sub setVertex()
            Dim xmin As Single = Single.MaxValue
            Dim ymin As Single = Single.MaxValue
            Dim xmax As Single = Single.MinValue
            Dim ymax As Single = Single.MinValue

            For i As Integer = 0 To 3
                xmax = Math.Max(xmax, vertex(i).X)
                ymax = Math.Max(ymax, vertex(i).Y)
                xmin = Math.Min(xmin, vertex(i).X)
                ymin = Math.Min(ymin, vertex(i).Y)
            Next

            rect = New Rectangle(CInt(Math.Truncate(xmin)), CInt(Math.Truncate(ymin)), CInt(Math.Truncate(xmax - xmin + 2)), CInt(Math.Truncate(ymax - ymin + 2)))

            AB = New YLScsDrawing.Geometry.Vector(vertex(0), vertex(1))
            BC = New YLScsDrawing.Geometry.Vector(vertex(1), vertex(2))
            CD = New YLScsDrawing.Geometry.Vector(vertex(2), vertex(3))
            DA = New YLScsDrawing.Geometry.Vector(vertex(3), vertex(0))

            ' get unit vector
            AB /= AB.Magnitude
            BC /= BC.Magnitude
            CD /= CD.Magnitude
            DA /= DA.Magnitude
        End Sub

        Private Function isOnPlaneABCD(pt As PointF) As Boolean
            '  including point on border
            If Not YLScsDrawing.Geometry.Vector.IsCCW(pt, vertex(0), vertex(1)) Then
                If Not YLScsDrawing.Geometry.Vector.IsCCW(pt, vertex(1), vertex(2)) Then
                    If Not YLScsDrawing.Geometry.Vector.IsCCW(pt, vertex(2), vertex(3)) Then
                        If Not YLScsDrawing.Geometry.Vector.IsCCW(pt, vertex(3), vertex(0)) Then
                            Return True
                        End If
                    End If
                End If
            End If
            Return False
        End Function

        Private Function getTransformedBitmap() As Bitmap
            If srcH = 0 OrElse srcW = 0 Then
                Return Nothing
            End If

            Dim destCB As New ImageData()
            destCB.A = New Byte(rect.Width - 1, rect.Height - 1) {}
            destCB.B = New Byte(rect.Width - 1, rect.Height - 1) {}
            destCB.G = New Byte(rect.Width - 1, rect.Height - 1) {}
            destCB.R = New Byte(rect.Width - 1, rect.Height - 1) {}


            Dim ptInPlane As New PointF()
            Dim x1 As Integer, x2 As Integer, y1 As Integer, y2 As Integer
            Dim dab As Double, dbc As Double, dcd As Double, dda As Double
            Dim dx1 As Single, dx2 As Single, dy1 As Single, dy2 As Single, dx1y1 As Single, dx1y2 As Single, _
                dx2y1 As Single, dx2y2 As Single, nbyte As Single

            For y As Integer = 0 To rect.Height - 1
                For x As Integer = 0 To rect.Width - 1
                    Dim srcPt As New Point(x, y)
                    srcPt.Offset(Me.rect.Location)

                    If isOnPlaneABCD(srcPt) Then
                        dab = Math.Abs((New YLScsDrawing.Geometry.Vector(vertex(0), srcPt)).CrossProduct(AB))
                        dbc = Math.Abs((New YLScsDrawing.Geometry.Vector(vertex(1), srcPt)).CrossProduct(BC))
                        dcd = Math.Abs((New YLScsDrawing.Geometry.Vector(vertex(2), srcPt)).CrossProduct(CD))
                        dda = Math.Abs((New YLScsDrawing.Geometry.Vector(vertex(3), srcPt)).CrossProduct(DA))
                        ptInPlane.X = CSng(srcW * (dda / (dda + dbc)))
                        ptInPlane.Y = CSng(srcH * (dab / (dab + dcd)))

                        x1 = CInt(ptInPlane.X)
                        y1 = CInt(ptInPlane.Y)

                        If x1 >= 0 AndAlso x1 < srcW AndAlso y1 >= 0 AndAlso y1 < srcH Then
                            If isBilinear Then
                                x2 = If((x1 = srcW - 1), x1, x1 + 1)
                                y2 = If((y1 = srcH - 1), y1, y1 + 1)

                                dx1 = ptInPlane.X - CSng(x1)
                                If dx1 < 0 Then
                                    dx1 = 0
                                End If
                                dx1 = 1.0F - dx1
                                dx2 = 1.0F - dx1
                                dy1 = ptInPlane.Y - CSng(y1)
                                If dy1 < 0 Then
                                    dy1 = 0
                                End If
                                dy1 = 1.0F - dy1
                                dy2 = 1.0F - dy1

                                dx1y1 = dx1 * dy1
                                dx1y2 = dx1 * dy2
                                dx2y1 = dx2 * dy1
                                dx2y2 = dx2 * dy2


                                nbyte = srcCB.A(x1, y1) * dx1y1 + srcCB.A(x2, y1) * dx2y1 + srcCB.A(x1, y2) * dx1y2 + srcCB.A(x2, y2) * dx2y2
                                destCB.A(x, y) = CByte(Math.Truncate(nbyte))
                                nbyte = srcCB.B(x1, y1) * dx1y1 + srcCB.B(x2, y1) * dx2y1 + srcCB.B(x1, y2) * dx1y2 + srcCB.B(x2, y2) * dx2y2
                                destCB.B(x, y) = CByte(Math.Truncate(nbyte))
                                nbyte = srcCB.G(x1, y1) * dx1y1 + srcCB.G(x2, y1) * dx2y1 + srcCB.G(x1, y2) * dx1y2 + srcCB.G(x2, y2) * dx2y2
                                destCB.G(x, y) = CByte(Math.Truncate(nbyte))
                                nbyte = srcCB.R(x1, y1) * dx1y1 + srcCB.R(x2, y1) * dx2y1 + srcCB.R(x1, y2) * dx1y2 + srcCB.R(x2, y2) * dx2y2
                                destCB.R(x, y) = CByte(Math.Truncate(nbyte))
                            Else
                                destCB.A(x, y) = srcCB.A(x1, y1)
                                destCB.B(x, y) = srcCB.B(x1, y1)
                                destCB.G(x, y) = srcCB.G(x1, y1)
                                destCB.R(x, y) = srcCB.R(x1, y1)
                            End If
                        End If
                    End If
                Next
            Next
            Return destCB.ToBitmap()
        End Function
    End Class
End Class

