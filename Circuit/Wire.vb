Public Class Wire
    Inherits Element

    '相对于location的位置, 应当有[0]=(0, 0)
    Public Points As New List(Of Point)

    Public Sub New(path As List(Of Point), location As Point)
        MyBase.New("", location)
        Points.AddRange(path)

        _connectors.Add(New Connector() With {.Owner = Me, .OriginalLocation = Points(0)})
        _connectors.Add(New Connector() With {.Owner = Me, .OriginalLocation = Points(Points.Count - 1)})
        UpdateConnectorLocation()
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
        g.RotateTransform(RotationAngleToAngle(_rotation))

        g.DrawLines(Pens.Blue, Points.ToArray())

        g.Restore(state)
    End Sub

    Public Overrides Function Contains(p0 As Point) As Boolean
        For i = 0 To Points.Count - 1
            Dim p1 = Points(i) + _location
            Dim deltaP = p1 - p0
            Dim distance2 = deltaP.X * deltaP.X + deltaP.Y * deltaP.Y
            If distance2 <= 25 Then
                Return True
            End If
        Next
        Return False
    End Function

    Friend Overrides ReadOnly Property OriginalSize As Size
        Get
            Return New Size(0, 0)
        End Get
    End Property

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
