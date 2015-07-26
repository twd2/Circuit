Public Class Ammeter
    Inherits Element

    Public Sub New(title As String, location As Point)
        MyBase.New(title, location)

        _originalSize = New Size(70, 50)
        _originalBoundary = New Rectangle(New Point(-_originalSize.Width / 2, -_originalSize.Height / 2), _originalSize)

        _connectors.Add(New Connector() With {.Owner = Me, .OriginalLocation = New Point(-35, 0)})
        _connectors.Add(New Connector() With {.Owner = Me, .OriginalLocation = New Point(35, 0)})

        UpdateProperties()
    End Sub

    Protected Overrides Sub InternalDraw(g As Graphics)
        g.FillEllipse(Brushes.White, New Rectangle(-25, -25, 50, 50))
        g.DrawEllipse(Pens.Black, New Rectangle(-25, -25, 50, 50))

        Dim str = "A"
        Dim font = New Font("Consolas", 20)

        Dim strSize = g.MeasureString(str, font)

        g.DrawString(str, font, Brushes.Black, -strSize.Width / 2, -strSize.Height / 2)

        Dim myPen As Pen

        If _connectors(0).Value Then
            myPen = Pens.Red
        Else
            myPen = Pens.Black
        End If
        g.DrawLine(myPen, -35, 0, -25, 0)

        If _connectors(1).Value Then
            myPen = Pens.Red
        Else
            myPen = Pens.Black
        End If
        g.DrawLine(myPen, 25, 0, 35, 0)

    End Sub

    Public Overloads Shared Function GetImage() As Image
        Dim e As New Ammeter("", Point.Empty)
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

        If valueChangedConnector.Equals(_connectors(0)) Then
            _connectors(1).Value = valueChangedConnector.Value
        Else
            _connectors(0).Value = valueChangedConnector.Value
        End If
    End Sub

End Class
