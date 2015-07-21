Public Class Wire
    Inherits Element

    '相对于location的位置, 应当有[0]=(0, 0)
    Public Points As New List(Of Point)

    Public Sub New(path As List(Of Point), location As Point)
        MyBase.New("", location)
        Points.AddRange(path)

        _connectors.Add(New Connector() With {.Owner = Me, .OriginalLocation = Points(0), .Location = .OriginalLocation})
        _connectors.Add(New Connector() With {.Owner = Me, .OriginalLocation = Points(Points.Count - 1), .Location = .OriginalLocation})

        CalcOriginalBoundary()

        UpdateProperties()
    End Sub

    Public Overrides Sub UpdateProperties()
        _boundary = _originalBoundary
        _boundary.Location += _location
    End Sub

    Private Sub CalcOriginalBoundary()
        Dim xmin = Points(0).X, xmax = Points(0).X, ymin = Points(0).Y, ymax = Points(0).Y
        For i = 1 To Points.Count - 1
            Dim p = Points(i)
            If p.X < xmin Then
                xmin = p.X
            End If
            If p.X > xmax Then
                xmax = p.X
            End If
            If p.Y < ymin Then
                ymin = p.Y
            End If
            If p.Y > ymax Then
                ymax = p.Y
            End If
        Next

        _originalBoundary = New Rectangle(xmin - 5, ymin - 5, 10 + xmax - xmin, 10 + ymax - ymin)
    End Sub

    Public Overrides Property Rotation As RotationAngle
        Get
            Return RotationAngle.D0
        End Get
        Set(value As RotationAngle)
            Throw New NotImplementedException("导线不允许旋转")
        End Set
    End Property

    Protected Overrides Sub InternalDraw(g As Graphics)
        Dim myPen As Pen

        If _connectors(0).Value Then
            myPen = Pens.Red
        Else
            myPen = Pens.Black
        End If

        g.DrawLines(myPen, Points.ToArray())
    End Sub

    Public Overrides Sub DrawBoundary(g As Graphics)
        Dim state = g.Save()

        g.TranslateTransform(_location.X, _location.Y)

        g.DrawLines(Pens.Blue, Points.ToArray())

        g.Restore(state)
    End Sub

    Public Overrides Function Contains(p0 As Point) As Boolean
        If Not _boundary.Contains(p0) Then
            Return False
        End If

        Dim p = p0 - _location
        For i = 1 To Points.Count - 1
            If MyMath.PointToLine2(Points(i - 1), Points(i), p) <= 25 Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Overrides Sub UpdateValue(valueChangedConnector As Connector)
        If valueChangedConnector.To IsNot Nothing Then
            valueChangedConnector.To.Value = valueChangedConnector.Value
        Else
            valueChangedConnector.Value = False
        End If

        If valueChangedConnector.Equals(_connectors(0)) Then
            _connectors(1).Value = valueChangedConnector.Value
        Else
            _connectors(0).Value = valueChangedConnector.Value
        End If
    End Sub
End Class
