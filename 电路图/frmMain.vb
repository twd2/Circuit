Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Drawing.Printing

Public Class frmMain

    WithEvents Printer As New PrintDocument
    Dim Elements As New List(Of Element)
    Dim Lines As New List(Of Line)
    Dim CircuitImg As Bitmap, CircuitG As Graphics
    Dim Origin As New Point(0, 0)
    Dim SelectedID As Integer = -1, Selected As Boolean = False, DownLocation As Point, OldLocation As Point
    Dim CanDrawLine As Boolean = False, DrawingLine As Boolean = False, LineID As Integer = 0
    Const FileFilter = "电路图文件(*.cir)|*.cir|XML文件(*.xml)|*.xml"
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '#If Not Debug Then
        '        Using WC As New Net.WebClient
        '            If WC.DownloadString("http://twd2.me/cirvcheck.php?v=e latest hardwar") <> "." Then
        '                MsgBox("该版本已经不被推荐使用")
        '                End
        '            End If
        '        End Using
        '#End If
        CircuitImg = New Bitmap(MainBox.Width, MainBox.Height)
        CircuitG = Graphics.FromImage(CircuitImg)
        CircuitG.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        MainBox.Image = CircuitImg

        ResetOrigin()
        ReDraw()

        MainBox.Refresh()
    End Sub

    Private Sub PrintCir(ByVal sender As Object, ByVal ev As PrintPageEventArgs) Handles Printer.PrintPage
        ev.Graphics.DrawImage(SavePic(), 0, 0)
    End Sub

    Function RotationSize(ByVal S As Size) As Size
        Return New Size(S.Height, S.Width)
    End Function

    Sub ReDraw()
        ReDraw(CircuitG)
    End Sub

    Sub ReDraw(ByVal Grap As Graphics)
        Debug.Print(Origin.ToString)
        Grap.Clear(Color.White)
        Grap.DrawLine(Pens.Gray, Origin.X - 10, Origin.Y, Origin.X + 10, Origin.Y)
        Grap.DrawLine(Pens.Gray, Origin.X, Origin.Y - 10, Origin.X, Origin.Y + 10)

        For i = 0 To Lines.Count - 1
            'CircuitG.DrawLine(Pens.Black, Lines(i).Point1 + Origin, Lines(i).Point2 + Origin)
            If Math.Abs(Lines(i).Point1.X - Lines(i).Point2.X) < Math.Abs(Lines(i).Point1.Y - Lines(i).Point2.Y) Then
                Lines(i).Point2 = New Point(Lines(i).Point1.X, Lines(i).Point2.Y)
                Grap.DrawLine(Pens.Black, Lines(i).Point1 + Origin, New Point(Lines(i).Point1.X, Lines(i).Point2.Y) + Origin)
            ElseIf Math.Abs(Lines(i).Point1.X - Lines(i).Point2.X) > Math.Abs(Lines(i).Point1.Y - Lines(i).Point2.Y) Then
                Lines(i).Point2 = New Point(Lines(i).Point2.X, Lines(i).Point1.Y)
                Grap.DrawLine(Pens.Black, Lines(i).Point1 + Origin, New Point(Lines(i).Point2.X, Lines(i).Point1.Y) + Origin)
            End If
        Next

        For i = 0 To Elements.Count - 1
            Dim anElement = Elements(i)
            Dim Img = Element.GetImage(anElement.Type)
            If anElement.Rotation Then
                Img.RotateFlip(RotateFlipType.Rotate90FlipNone)
            End If
            If SelectedID = i Then
                PropertyGrid1.SelectedObject = anElement
                Dim Rect As New Rectangle(Elements(i).Location + Origin - (New Point(1, 1)), Img.Size + (New Size(1, 1)))
                Grap.DrawRectangle(Pens.Red, Rect)
            End If

            Grap.DrawImage(Img, Origin + anElement.Location)
            Grap.DrawString(anElement.Title, New Font("微软雅黑", 12), Brushes.Black, Origin + anElement.Location + (New Point(Img.Width + 5, -10)))
        Next
        'For Each e In Elements
        '    For Each ee In Elements
        '        CircuitG.DrawLine(Pens.Black, e.Location + Origin, ee.Location + Origin)
        '    Next
        'Next

        MainBox.Refresh()
    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        Elements.Add(New Element(Element.ElementType.Ammeter, "A1", New Point(10, 10) - Origin))
        ReDraw()
    End Sub

    Function IsIn(ByVal P As Point, ByVal R As Rectangle) As Boolean
        Return P.X > R.Left AndAlso P.X < R.Right AndAlso P.Y > R.Top AndAlso P.Y < R.Bottom
    End Function

    Function GetRectangle(ByVal E As Element) As Rectangle
        Dim Img = Element.GetImage(E.Type)
        If E.Rotation Then
            Img.RotateFlip(RotateFlipType.Rotate90FlipNone)
        End If
        Return New Rectangle(E.Location, Img.Size)
    End Function

    Private Sub MainBox_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles MainBox.DoubleClick
        If SelectedID >= 0 AndAlso Not DrawingLine Then
            Elements(SelectedID).Rotation = Not Elements(SelectedID).Rotation
        End If
    End Sub

    Private Sub MainBox_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MainBox.MouseDown
        Dim Relative As Point = e.Location - Origin
        If CanDrawLine Then
            DrawingLine = True
            Lines.Add(New Line(Relative, Relative))
            LineID = Lines.Count - 1
        Else
            SelectedID = -1
            ContextMenuStrip1.Items(0).Visible = False
            For i = Elements.Count - 1 To 0 Step -1
                If IsIn(Relative, GetRectangle(Elements(i))) Then
                    SelectedID = i
                    ContextMenuStrip1.Items(0).Visible = True
                    Exit For
                End If
            Next
            ReDraw()
            DownLocation = e.Location
            If SelectedID = -1 Then
                OldLocation = Origin
            Else
                OldLocation = Elements(SelectedID).Location
                For Each l In Lines
                    l.OldPoint1 = l.Point1
                    l.OldPoint2 = l.Point2
                Next
            End If
            Selected = True
        End If
    End Sub

    Private Sub MainBox_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MainBox.MouseMove
        MainBoxPoint.Text = "(" & e.Location.X.ToString & ", " & e.Location.Y.ToString & ")"
        OriPoint.Text = "(" & (e.Location - Origin).X.ToString & ", " & (e.Location - Origin).Y.ToString & ")"
        Dim Relative As Point = e.Location - Origin
        If CanDrawLine Then
            MainBox.Cursor = Cursors.Cross
        End If
        If DrawingLine Then
            Lines(LineID).Point2 = Relative
            ReDraw()
        End If
        If Selected Then
            MainBox.Cursor = Cursors.SizeAll
            If SelectedID = -1 Then
                Origin = OldLocation + e.Location - DownLocation
            Else
                Elements(SelectedID).Location = OldLocation + e.Location - DownLocation
                Dim Rect = GetRectangle(Elements(SelectedID))
                Rect.Location = OldLocation
                For Each line In Lines.FindAll(Function(l As Line) IsIn(l.OldPoint1, Rect))
                    line.Point1 = line.OldPoint1 + e.Location - DownLocation
                Next
                For Each line In Lines.FindAll(Function(l As Line) IsIn(l.OldPoint2, Rect))
                    line.Point2 = line.OldPoint2 + e.Location - DownLocation
                Next
            End If
            ReDraw()
        End If
    End Sub

    Sub CleanLines()
        Lines.RemoveAll(Function(l As Line) Elements.FindIndex(Function(E As Element) IsIn(l.Point1, GetRectangle(E))) < 0 AndAlso
                Elements.FindIndex(Function(E As Element) IsIn(l.Point2, GetRectangle(E))) < 0)
        'For Each line In Lines
        '    Dim l = line
        '    If  Then
        '        Lines.Remove(l)
        '    End If
        'Next
    End Sub

    Private Sub MainBox_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MainBox.MouseUp
        DrawingLine = False
        Selected = False
        MainBox.Cursor = Cursors.Default
        'CleanLines()
        ReDraw()
    End Sub

    Private Sub 删除ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 删除ToolStripMenuItem.Click
        Elements.RemoveAt(SelectedID)
        SelectedID = -1
        ReDraw()
    End Sub

    Private Sub MainBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainBox.Click

    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        Elements.Add(New Element(Element.ElementType.Light, "L1", New Point(10, 10) - Origin))
        ReDraw()
    End Sub

    Private Sub PictureBox5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox5.Click
        Elements.Add(New Element(Element.ElementType.Voltmeter, "V1", New Point(10, 10) - Origin))
        ReDraw()
    End Sub

    Private Sub PictureBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox3.Click
        Elements.Add(New Element(Element.ElementType.Resistor, "R1", New Point(10, 10) - Origin))
        ReDraw()
    End Sub

    Private Sub PictureBox4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox4.Click
        Elements.Add(New Element(Element.ElementType.Switch, "S1", New Point(10, 10) - Origin))
        ReDraw()
    End Sub

    Private Sub PictureBox6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox6.Click
        Elements.Add(New Element(Element.ElementType.VariableResistor, "R1", New Point(10, 10) - Origin))
        ReDraw()
    End Sub

    Private Sub 生成图片ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 生成图片ToolStripMenuItem.Click
        Using SFD As New SaveFileDialog
            SFD.Title = "图像保存路径"
            SFD.Filter = "PNG|*.png"
            If SFD.ShowDialog = DialogResult.OK Then
                SavePic().Save(SFD.FileName)
            End If
        End Using
    End Sub

    Function SavePic() As Image
        Dim MinPoint As Point
        For Each e In Elements
            MinPoint.X = Math.Min(MinPoint.X, e.Location.X)
            MinPoint.Y = Math.Min(MinPoint.Y, e.Location.Y)
        Next
        For Each l In Lines
            MinPoint.X = Math.Min(MinPoint.X, l.Point1.X)
            MinPoint.Y = Math.Min(MinPoint.Y, l.Point1.Y)
            MinPoint.X = Math.Min(MinPoint.X, l.Point2.X)
            MinPoint.Y = Math.Min(MinPoint.Y, l.Point2.Y)
        Next

        Dim MaxPoint As Point
        For Each e In Elements
            MaxPoint.X = Math.Max(MaxPoint.X, e.Location.X)
            MaxPoint.Y = Math.Max(MaxPoint.Y, e.Location.Y)
        Next
        For Each l In Lines
            MaxPoint.X = Math.Max(MaxPoint.X, l.Point1.X)
            MaxPoint.Y = Math.Max(MaxPoint.Y, l.Point1.Y)
            MaxPoint.X = Math.Max(MaxPoint.X, l.Point2.X)
            MaxPoint.Y = Math.Max(MaxPoint.Y, l.Point2.Y)
        Next

        Dim Img As New Bitmap(Math.Abs(MinPoint.X - MaxPoint.X) + 149, Math.Abs(MinPoint.Y - MaxPoint.Y) + 140)
        Dim OldOrigin = Origin
        Origin = New Point(50, 50) - MinPoint
        ReDraw(Graphics.FromImage(Img))
        Origin = OldOrigin
        Return Img
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim Count = 0
        For i = 0 To Elements.Count - 1
            If Elements(i).Type = Element.ElementType.Ammeter Then
                Count += 1
                Elements(i).Title = Element.GetAbbr(Elements(i).Type) & Count
            End If
        Next

        Count = 0
        For i = 0 To Elements.Count - 1
            If Elements(i).Type = Element.ElementType.Light Then
                Count += 1
                Elements(i).Title = Element.GetAbbr(Elements(i).Type) & Count
            End If
        Next

        Count = 0
        For i = 0 To Elements.Count - 1
            If Elements(i).Type = Element.ElementType.Resistor OrElse Elements(i).Type = Element.ElementType.VariableResistor Then
                Count += 1
                Elements(i).Title = Element.GetAbbr(Elements(i).Type) & Count
            End If
        Next

        Count = 0
        For i = 0 To Elements.Count - 1
            If Elements(i).Type = Element.ElementType.Switch Then
                Count += 1
                Elements(i).Title = Element.GetAbbr(Elements(i).Type) & Count
            End If
        Next

        Count = 0
        For i = 0 To Elements.Count - 1
            If Elements(i).Type = Element.ElementType.Voltmeter Then
                Count += 1
                Elements(i).Title = Element.GetAbbr(Elements(i).Type) & Count
            End If
        Next

        Count = 0
        For i = 0 To Elements.Count - 1
            If Elements(i).Type = Element.ElementType.Bell Then
                Count += 1
                Elements(i).Title = Element.GetAbbr(Elements(i).Type) & Count
            End If
        Next

        Count = 0
        For i = 0 To Elements.Count - 1
            If Elements(i).Type = Element.ElementType.Motor Then
                Count += 1
                Elements(i).Title = Element.GetAbbr(Elements(i).Type) & Count
            End If
        Next

        Count = 0
        For i = 0 To Elements.Count - 1
            If Elements(i).Type = Element.ElementType.PowerSupply Then
                Count += 1
                Elements(i).Title = "" 'Element.GetAbbr(Elements(i).Type) & Count
            End If
        Next

        ReDraw()
    End Sub

    Sub ResetOrigin()
        Origin.X = CircuitImg.Size.Width / 2
        Origin.Y = CircuitImg.Size.Height / 2
        ReDraw()
    End Sub

    Private Sub PropertyGrid1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PropertyGrid1.Click

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ResetOrigin()
    End Sub

    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If MainBox.Width > 0 AndAlso MainBox.Height > 0 Then
            CircuitImg = New Bitmap(MainBox.Width, MainBox.Height)
            CircuitG = Graphics.FromImage(CircuitImg)
            CircuitG.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            MainBox.Image = CircuitImg

            ReDraw()

            MainBox.Refresh()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        CanDrawLine = Not CanDrawLine
        DrawingLine = DrawingLine AndAlso CanDrawLine
        MainBox.Cursor = Cursors.Default
    End Sub

    Private Sub 新建ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 新建ToolStripMenuItem.Click
        NewCir()
    End Sub

    Sub NewCir()
        Lines.Clear()
        Elements.Clear()
        ReDraw()
        ResetOrigin()
    End Sub

    Private Sub PictureBox7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox7.Click
        Elements.Add(New Element(Element.ElementType.Bell, "B1", New Point(10, 10) - Origin))
        ReDraw()
    End Sub

    Private Sub PictureBox8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox8.Click
        Elements.Add(New Element(Element.ElementType.Motor, "M1", New Point(10, 10) - Origin))
        ReDraw()
    End Sub

    Private Sub PictureBox9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox9.Click
        Elements.Add(New Element(Element.ElementType.PowerSupply, "", New Point(10, 10) - Origin))
        ReDraw()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        CanDrawLine = Not CanDrawLine
        DrawingLine = DrawingLine AndAlso CanDrawLine
        MainBox.Cursor = Cursors.Default
    End Sub

    Private Sub 保存ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 保存ToolStripMenuItem.Click
        Using SFD As New SaveFileDialog
            SFD.Title = "电路图保存路径"
            SFD.Filter = FileFilter
            If SFD.ShowDialog = DialogResult.OK Then
                'SavePic(SFD.FileName)
                WriteCirData(SFD.FileName)
            End If
        End Using
    End Sub

    Sub WriteCirData(ByVal Filename As String)
        Dim XD As New XmlDocument()
        XD.AppendChild(XD.CreateXmlDeclaration("1.0", "UTF-8", String.Empty))
        Dim root = XD.CreateElement("Circuit")
        XD.AppendChild(root)
        For Each e In Elements
            Dim XE = XD.CreateElement("Element")
            XE.SetAttribute("Title", e.Title)
            XE.SetAttribute("Type", e.Type.ToString)
            XE.SetAttribute("Rotation", e.Rotation.ToString)
            XE.SetAttribute("Location", e.Location.X.ToString & "," & e.Location.Y.ToString)
            root.AppendChild(XE)
        Next

        For Each e In Lines
            Dim XE = XD.CreateElement("Line")
            XE.SetAttribute("Point1", e.Point1.X.ToString & "," & e.Point1.Y.ToString)
            XE.SetAttribute("Point2", e.Point2.X.ToString & "," & e.Point2.Y.ToString)
            root.AppendChild(XE)
        Next

        XD.Save(Filename)
    End Sub

    Private Sub 打开ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 打开ToolStripMenuItem.Click
        Using OFD As New OpenFileDialog
            OFD.Title = "电路图存储路径"
            OFD.Filter = FileFilter
            If OFD.ShowDialog = DialogResult.OK Then
                LoadCirData(OFD.FileName)
            End If
        End Using
    End Sub

    Sub LoadCirData(ByVal Filename As String)
        Try
            NewCir()
            Dim XD As New XmlDocument()
            XD.Load(Filename)
            Dim root = XD.SelectSingleNode("Circuit")
            For Each XE As XmlElement In root.SelectNodes("Element")
                Dim MyElement As New Element([Enum].Parse(GetType(Element.ElementType), XE.GetAttribute("Type")), XE.GetAttribute("Title"), ToPoint(XE.GetAttribute("Location")))
                MyElement.Rotation = (XE.GetAttribute("Rotation") = "True")
                Elements.Add(MyElement)
            Next

            For Each XE As XmlElement In root.SelectNodes("Line")
                Dim MyLine As New Line(ToPoint(XE.GetAttribute("Point1")), ToPoint(XE.GetAttribute("Point2")))
                Lines.Add(MyLine)
            Next
            ReDraw()
        Catch ex As Exception
            MsgBox(ex.ToString, 16 + 0, "错误的文件")
        End Try
    End Sub

    Function ToPoint(ByVal Src As String) As Point
        Dim PointVal = Split(Src, ",")
        Return New Point(CInt(PointVal(0)), CInt(PointVal(1)))
    End Function

    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        Printer.DocumentName = "电路图"
        Printer.Print()
    End Sub
End Class
