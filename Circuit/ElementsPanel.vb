Public Class ElementsPanel

    Private Enum PanelState
        None
        MovingOrigin
        PendingMovingElement
        MovingElement
        PendingDrawWire
        DrawingWire
        PendingDeleteWire
        DeletingWire
    End Enum

    Public WithEvents picMain As PictureBox
    Public Parent As frmMain
    Public Elements As New List(Of Element)
    Public Origin As New Point(0, 0)

    Private _selectedId As Integer = -1
    Private _image As Bitmap, _g As Graphics
    Private _state As PanelState

    Public Sub New(frm As frmMain)
        Parent = frm
        picMain = frm.picMain

        Resize()

        ResetOrigin()
    End Sub

    Public Sub Resize()
        If picMain.Width > 0 AndAlso picMain.Height > 0 Then
            _image = New Bitmap(picMain.Width, picMain.Height)
            _g = Graphics.FromImage(_image)
            _g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            picMain.Image = _image
        End If
    End Sub

    Public Sub ResetOrigin()
        Origin.X = _image.Size.Width / 2
        Origin.Y = _image.Size.Height / 2
    End Sub

    Private Sub InternalRender()
        'Utilities.Info("Origin: " + Origin.ToString())

        Dim state = _g.Save()

        _g.TranslateTransform(Origin.X, Origin.Y)
        _g.Clear(Color.White)

        '坐标轴
        _g.DrawLine(Pens.Gray, -10, 0, 10, 0)
        _g.DrawLine(Pens.Gray, 0, -10, 0, 10)

        For i = 0 To Elements.Count - 1
            Dim element = Elements(i)
            element.Draw(_g)
            If i = _selectedId Then
                element.DrawBoundary(_g)
            End If
        Next

        If _path.Count >= 2 Then
            _g.DrawLines(Pens.Black, _path.ToArray())
        End If

        _g.Restore(state)
    End Sub

    Public Sub Render()
        InternalRender()
        picMain.Refresh()
    End Sub

    Public Sub DrawWire()
        _state = PanelState.PendingDrawWire
    End Sub

#Region "UI"
    Private Sub onDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles picMain.DoubleClick
        Debug.Print("dc")
        If _selectedId > 0 Then

        End If
    End Sub


    Private _oldObjectLocation As Point '旧的坐标, 原点则相对与picMain, 元件则相对于原点
    Private _downMouseLocation As Point '按下鼠标时的坐标, 相对于picMain
    Private _path As New List(Of Point) '画导线的路径, 坐标相对于原点

    Private Sub [Select](id As Integer)
        If id < 0 Then
            _selectedId = -1
            _oldObjectLocation = Origin
        Else
            _selectedId = id
            _oldObjectLocation = Elements(_selectedId).Location
            Parent.PropertyGrid1.SelectedObject = Elements(_selectedId)
        End If
    End Sub

    Private Sub onMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picMain.MouseDown
        Utilities.Info("Mouse down")

        Dim p = ToRelative(e.Location)
        _downMouseLocation = e.Location

        If _state = PanelState.None Then
            Utilities.Info("State is none")

            Dim ids As New List(Of Integer)

            For i = 0 To Elements.Count - 1
                If Elements(i).Contains(p) Then
                    ids.Add(i)
                End If
            Next

            If ids.Count = 0 Then
                Utilities.Info("No element selected, changing state to MovingOrigin")
                _state = PanelState.MovingOrigin
                [Select](-1)
                picMain.Cursor = Cursors.Hand
            Else
                If ids.Count = 1 Then
                    Utilities.Info("One element selected, changing state to MovingElement")
                    _state = PanelState.MovingElement
                    [Select](ids(0))
                    picMain.Cursor = Cursors.SizeAll
                Else
                    Utilities.Info("More than one elements selected, waiting for menuPendingSelect to be clicked")
                    Dim menuPendingSelect As New ContextMenuStrip
                    menuPendingSelect.Hide()
                    menuPendingSelect.Items.Clear()
                    For i = 0 To ids.Count - 1
                        Dim text = Elements(ids(i)).Title
                        If text = "" Then
                            text = Elements(ids(i)).Type()
                        End If
                        Dim item As New ToolStripMenuItem(String.Format("{0} (ID={1})", text, ids(i)))
                        item.Tag = ids(i)
                        AddHandler item.Click, AddressOf onMenuItemClicked
                        menuPendingSelect.Items.Add(item)
                    Next
                    _state = PanelState.PendingMovingElement
                    menuPendingSelect.Show(picMain, e.X, e.Y)
                End If
            End If
        ElseIf _state = PanelState.PendingDrawWire Then
            Utilities.Info("State is PendingDrawLine, starting drawing line")
            _state = PanelState.DrawingWire
            _path.Clear()
            _path.Add(p)
            picMain.Cursor = Cursors.Cross
        ElseIf _state = PanelState.PendingMovingElement Then
            Utilities.Info("State is PendingMovingElement, changing it to none")
            _state = PanelState.None
        End If

        Render()
    End Sub

    Private Sub onMenuItemClicked(ByVal sender As Object, ByVal e As EventArgs)
        If Not TypeOf sender Is ToolStripMenuItem Then
            Return
        End If
        Dim item = DirectCast(sender, ToolStripMenuItem)
        Debug.Assert(TypeOf item.Tag Is Integer)
        [Select](DirectCast(item.Tag, Integer))
        Utilities.Info("menuPendingSelect clicked")
        If _state = PanelState.PendingMovingElement Then
            _state = PanelState.MovingElement
            picMain.Cursor = Cursors.SizeAll
            Utilities.Info("State is PendingMovingElement, so change it to MovingElement")
        End If
        Render()
    End Sub

    Private Sub onMouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles picMain.MouseMove
        'Utilities.Info("Mouse moving")

        Parent.MainBoxPoint.Text = "(" & e.Location.X.ToString & ", " & e.Location.Y.ToString & ")"
        Parent.OriPoint.Text = "(" & (e.Location - Origin).X.ToString & ", " & (e.Location - Origin).Y.ToString & ")"


        Dim p = ToRelative(e.Location)

        Dim mouseDelta = e.Location - _downMouseLocation

        Select Case _state
            Case PanelState.MovingOrigin
                Origin = _oldObjectLocation + mouseDelta
            Case PanelState.MovingElement
                Debug.Assert(_selectedId >= 0)
                Elements(_selectedId).Location = _oldObjectLocation + mouseDelta
            Case PanelState.DrawingWire
                _path.Add(p)
        End Select
        Render()
    End Sub

    Private Sub onMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picMain.MouseUp
        Utilities.Info("Mouse up")

        Dim p = ToRelative(e.Location)

        If _state = PanelState.PendingMovingElement Then
            Utilities.Info("State is PendingMovingElement, do nothing")
            Return
        End If

        If _state = PanelState.DrawingWire Then
            _path.Add(p)
            If _path.Count >= 3 Then
                Dim wlocation = _path(0)
                For i = 0 To _path.Count - 1
                    _path(i) -= wlocation
                Next
                Dim w As New Wire(_path, wlocation)
                Elements.Add(w)
            End If
            _path.Clear()
        End If

        Utilities.Info("Changing state to none")
        _state = PanelState.None

        picMain.Cursor = Cursors.Arrow

        Render()
    End Sub

    ''' <summary>
    ''' 把在PictureBox的坐标转换为相对于Origin的坐标
    ''' </summary>
    ''' <param name="p">PictureBox的坐标</param>
    ''' <returns>相对于Origin的坐标</returns>
    ''' <remarks></remarks>
    Private Function ToRelative(p As Point) As Point
        Return p - Origin
    End Function

    Public Sub DeleteSelected()
        If _selectedId < 0 Then
            Return
        End If
        Elements.RemoveAt(_selectedId)
        [Select](-1)
    End Sub

    Public Function ViewableBoundary() As Rectangle
        Return New Rectangle(Point.Empty - Origin, picMain.Size)
    End Function
#End Region

End Class
