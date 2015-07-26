Imports System.Drawing

Public Class MyMath

    ''' <summary>
    ''' 向量的模
    ''' </summary>
    ''' <param name="p">向量的坐标</param>
    ''' <returns>模</returns>
    ''' <remarks></remarks>
    Public Shared Function Distance(p As Point) As Double
        Return Math.Sqrt(p.X * p.X + p.Y * p.Y)
    End Function

    ''' <summary>
    ''' 两点间距离
    ''' </summary>
    ''' <param name="a">一个点</param>
    ''' <param name="b">另外一个点</param>
    ''' <returns>距离</returns>
    ''' <remarks></remarks>
    Public Shared Function Distance(a As Point, b As Point) As Double
        Dim delta = a - b
        Return Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y)
    End Function

    ''' <summary>
    ''' 向量的模的平方
    ''' </summary>
    ''' <param name="p">向量的坐标</param>
    ''' <returns>模的平方</returns>
    ''' <remarks></remarks>
    Public Shared Function Distance2(p As Point) As Integer
        Return p.X * p.X + p.Y * p.Y
    End Function

    ''' <summary>
    ''' 两点间距离的平方
    ''' </summary>
    ''' <param name="a">一个点</param>
    ''' <param name="b">另外一个点</param>
    ''' <returns>距离的平方</returns>
    ''' <remarks></remarks>
    Public Shared Function Distance2(a As Point, b As Point) As Integer
        Dim delta = a - b
        Return delta.X * delta.X + delta.Y * delta.Y
    End Function

    ''' <summary>
    ''' 点到线段的距离的平方
    ''' </summary>
    ''' <param name="a">线段一个端点</param>
    ''' <param name="b">线段一个端点</param>
    ''' <param name="p">待测点</param>
    ''' <returns>距离的平方</returns>
    ''' <remarks></remarks>
    Public Shared Function PointToLine2(a As Point, b As Point, p As Point) As Double
        Dim AP = p - a
        Dim d2AP = Distance2(AP)
        If d2AP = 0 Then
            Return 0.0
        End If

        Dim BP = p - b
        Dim d2BP = Distance2(BP)
        If d2BP = 0 Then
            Return 0.0
        End If

        Dim AB = b - a
        Dim d2AB = Distance2(AB)
        If d2AB = 0 Then '3
            Return d2AP
        End If

        If d2BP > d2AB + d2AP Then '1
            Return d2AP
        End If

        If d2AP > d2AB + d2BP Then '2
            Return d2BP
        End If

        Dim APdotAB As Double = AP.X * AB.X + AP.Y * AB.Y

        'Dim cos2A = (APdotAB * APdotAB) / (d2AP * d2AB)
        'Dim sin2A = 1 - cos2A

        'Return d2AP * sin2A
        Return d2AP - (APdotAB * APdotAB) / d2AB
    End Function


    ''' <summary>
    ''' 点到线段的距离
    ''' </summary>
    ''' <param name="a">线段一个端点</param>
    ''' <param name="b">线段一个端点</param>
    ''' <param name="p">待测点</param>
    ''' <returns>距离</returns>
    ''' <remarks></remarks>
    Public Shared Function PointToLine(a As Point, b As Point, p As Point) As Double
        Return Math.Sqrt(PointToLine2(a, b, p))
    End Function

    Public Shared Function PointToLine_way2(a As Point, b As Point, p As Point) As Double
        Dim AP = p - a
        Dim d2AP = Distance2(AP)
        If d2AP = 0 Then
            Return 0.0
        End If

        Dim BP = p - b
        Dim d2BP = Distance2(BP)
        If d2BP = 0 Then
            Return 0.0
        End If

        Dim AB = b - a
        Dim d2AB = Distance2(AB)
        If d2AB = 0 Then '3
            Return Math.Sqrt(d2AP)
        End If

        If d2BP > d2AB + d2AP Then '1
            Return Math.Sqrt(d2AP)
        End If

        If d2AP > d2AB + d2BP Then '2
            Return Math.Sqrt(d2BP)
        End If

        Dim x = Math.Sqrt(d2AP), y = Math.Sqrt(d2BP), z = Math.Sqrt(d2AB)
        Dim l = (x + y + z) / 2
        Dim s = Math.Sqrt(l * (l - x) * (l - y) * (l - z))
        Return 2 * s / z
    End Function

    ''' <summary>
    ''' 判断矩形是否相交
    ''' </summary>
    ''' <param name="R1">矩形一</param>
    ''' <param name="R2">矩形二</param>
    ''' <returns>是否相交</returns>
    ''' <remarks></remarks>
    Public Shared Function IsCoincide(ByVal R1 As Rectangle, ByVal R2 As Rectangle) As Boolean
        Return R1.Left < R2.Right AndAlso R1.Right > R2.Left AndAlso
     R1.Top < R2.Bottom AndAlso R1.Bottom > R2.Top
    End Function

End Class
