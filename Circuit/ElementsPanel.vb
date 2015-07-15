Public Class ElementsPanel

    Private Enum PanelState
        None
        MovingOrigin
        PendingMovingElement
        MovingElement
        PendingDrawLine
        DrawingLine
        DeletingLine
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

        _g.Restore(state)
    End Sub

    Public Sub Render()
        InternalRender()
        picMain.Refresh()
    End Sub

#Region "UI"
    Private Sub onDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles picMain.DoubleClick
        Debug.Print("dc")
        'If SelectedID >= 0 AndAlso Not DrawingLine Then
        '    Elements(SelectedID).Rotation = Not Elements(SelectedID).Rotation
        'End If
    End Sub


    Private _oldObjectLocation, _downMouseLocation As Point

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

        Dim ids As New List(Of Integer)

        'If _selectedId < 0 Then
        For i = 0 To Elements.Count - 1
            Dim b = Elements(i).Boundary
            If b.Contains(p) Then
                ids.Add(i)
            End If
        Next
        'Else
        '    ids.Add(_selectedId)
        'End If

        If ids.Count = 0 Then
            Utilities.Info("No element selected, changing state to MovingOrigin")
            _state = PanelState.MovingOrigin
            [Select](-1)
        Else
            If ids.Count = 1 Then
                Utilities.Info("One element selected, changing state to MovingElement")
                _state = PanelState.MovingElement
                [Select](ids(0))
            Else
                Utilities.Info("More than one elements selected, waiting for menuPendingSelect to be clicked")
                Dim menuPendingSelect As New ContextMenuStrip
                menuPendingSelect.Hide()
                menuPendingSelect.Items.Clear()
                For i = 0 To ids.Count - 1
                    Dim tsmi As New ToolStripMenuItem(String.Format("{0} (ID={1})", Elements(ids(i)).Title, ids(i)))
                    tsmi.Tag = ids(i)
                    AddHandler tsmi.Click, AddressOf onMenuItemClicked
                    menuPendingSelect.Items.Add(tsmi)
                Next
                _state = PanelState.PendingMovingElement
                menuPendingSelect.Show(picMain, e.X, e.Y)
            End If
        End If

        Render()
        'Dim Relative As Point = e.Location - Origin
        'If CanDrawLine Then
        '    DrawingLine = True
        '    Lines.Add(New Line(Relative, Relative))
        '    LineID = Lines.Count - 1
        'Else
        '    SelectedID = -1
        '    ContextMenuStrip1.Items(0).Visible = False
        '    For i = Elements.Count - 1 To 0 Step -1
        '        If IsIn(Relative, GetRectangle(Elements(i))) Then
        '            SelectedID = i
        '            ContextMenuStrip1.Items(0).Visible = True
        '            Exit For
        '        End If
        '    Next
        '    ReDraw()
        '    DownLocation = e.Location
        '    If SelectedID = -1 Then
        '        OldLocation = Origin
        '    Else
        '        OldLocation = Elements(SelectedID).Location
        '        For Each l In Lines
        '            l.OldPoint1 = l.Point1
        '            l.OldPoint2 = l.Point2
        '        Next
        '    End If
        '    Selected = True
        'End If
    End Sub

    Private Sub onMenuItemClicked(ByVal sender As Object, ByVal e As EventArgs)
        If Not TypeOf sender Is ToolStripMenuItem Then
            Return
        End If
        Dim tsmi = DirectCast(sender, ToolStripMenuItem)
        Debug.Assert(TypeOf tsmi.Tag Is Integer)
        [Select](DirectCast(tsmi.Tag, Integer))
        Utilities.Info("menuPendingSelect clicked")
        If _state = PanelState.PendingMovingElement Then
            _state = PanelState.MovingElement
            Utilities.Info("State is PendingMovingElement, so change it to MovingElement")
        End If
        Render()
    End Sub

    Private Sub onMouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picMain.MouseMove
        'Utilities.Info("Mouse moving")

        Parent.MainBoxPoint.Text = "(" & e.Location.X.ToString & ", " & e.Location.Y.ToString & ")"
        Parent.OriPoint.Text = "(" & (e.Location - Origin).X.ToString & ", " & (e.Location - Origin).Y.ToString & ")"


        Dim p = ToRelative(New Point(e.X, e.Y))

        Dim mouseDelta = e.Location - _downMouseLocation

        Select Case _state
            Case PanelState.MovingOrigin
                picMain.Cursor = Cursors.SizeAll
                Origin = _oldObjectLocation + mouseDelta
            Case PanelState.MovingElement
                Debug.Assert(_selectedId >= 0)
                Elements(_selectedId).Location = _oldObjectLocation + mouseDelta
        End Select
        Render()

        'MainBoxPoint.Text = "(" & e.Location.X.ToString & ", " & e.Location.Y.ToString & ")"
        'OriPoint.Text = "(" & (e.Location - Origin).X.ToString & ", " & (e.Location - Origin).Y.ToString & ")"
        'Dim Relative As Point = e.Location - Origin
        'If CanDrawLine Then
        '    picMain.Cursor = Cursors.Cross
        'End If
        'If DrawingLine Then
        '    Lines(LineID).Point2 = Relative
        '    ReDraw()
        'End If
        'If Selected Then
        '    picMain.Cursor = Cursors.SizeAll
        '    If SelectedID = -1 Then
        '        Origin = OldLocation + e.Location - DownLocation
        '    Else
        '        Elements(SelectedID).Location = OldLocation + e.Location - DownLocation
        '        Dim Rect = GetRectangle(Elements(SelectedID))
        '        Rect.Location = OldLocation
        '        For Each line In Lines.FindAll(Function(l As Line) IsIn(l.OldPoint1, Rect))
        '            line.Point1 = line.OldPoint1 + e.Location - DownLocation
        '        Next
        '        For Each line In Lines.FindAll(Function(l As Line) IsIn(l.OldPoint2, Rect))
        '            line.Point2 = line.OldPoint2 + e.Location - DownLocation
        '        Next
        '    End If
        '    ReDraw()
        'End If
    End Sub

    Private Sub onMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picMain.MouseUp
        Utilities.Info("Mouse up")


        If _state = PanelState.PendingMovingElement Then
            Utilities.Info("State is PendingMovingElement, so do nothing")
            Return
        End If

        Utilities.Info("Changing state to none")
        _state = PanelState.None

        picMain.Cursor = Cursors.Arrow
        'DrawingLine = False
        'Selected = False
        'picMain.Cursor = Cursors.Default
        ''CleanLines()
        'ReDraw()
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
#End Region

End Class
