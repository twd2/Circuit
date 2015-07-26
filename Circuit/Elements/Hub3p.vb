Public Class Hub3p
    Inherits Element

    Private Const sin60 = 0.8660254F
    Private Const cos60 = 0.5F

    Public Sub New(title As String, location As Point)
        MyBase.New(title, location)

        _originalSize = New Size(20 * (1 + cos60), 40 * sin60)
        _originalBoundary = New Rectangle(New Point(-_originalSize.Width / 2, -_originalSize.Height / 2), _originalSize)

        _connectors.Add(New Connector() With {.Owner = Me, .OriginalLocation = New Point(0, -20)})
        _connectors.Add(New Connector() With {.Owner = Me, .OriginalLocation = New Point(20 * sin60, 20 * cos60)})
        _connectors.Add(New Connector() With {.Owner = Me, .OriginalLocation = New Point(-20 * sin60, 20 * cos60)})

        UpdateProperties()
    End Sub

    Protected Overrides Sub InternalDraw(g As Graphics)
        For Each c In _connectors
            Dim myPen As Pen
            If c.Value Then
                myPen = Pens.Red
            Else
                myPen = Pens.Black
            End If
            g.DrawLine(myPen, Point.Empty, c.OriginalLocation)
        Next
        g.FillEllipse(Brushes.Black, New Rectangle(-3, -3, 6, 6))
    End Sub

    Public Overloads Shared Function GetImage() As Image
        Dim e As New Hub3p("", Point.Empty)
        Dim img As New Bitmap(e.Size.Width, e.Size.Height)
        Using g = Graphics.FromImage(img)
            g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            g.TranslateTransform(img.Size.Width / 2, img.Size.Height / 2)
            e.Draw(g)
        End Using
        Return img
    End Function

    Public Overrides Sub UpdateValue(valueChangedConnector As Connector)
        If valueChangedConnector.To IsNot Nothing Then
            valueChangedConnector.To.Value = valueChangedConnector.Value
        Else
            valueChangedConnector.Value = False
        End If

        Dim id = _connectors.FindIndex(Function(c As Connector) c.Equals(valueChangedConnector))

        For i = 0 To _connectors.Count - 1
            If i <> id Then
                _connectors(i).Value = valueChangedConnector.Value
            End If
        Next
    End Sub

End Class
