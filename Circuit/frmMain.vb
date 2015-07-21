Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Drawing.Printing
Imports System.Threading

Public Class frmMain

    Private _panel As ElementsPanel
    Private _engine As DigitalEngine

    Private WithEvents Printer As New PrintDocument

    Private _renderThread As Thread
    Private _isRunning As Boolean = False
    Private _lastFPS As Integer = 0
    Private _lastFPSTime As DateTime = DateTime.Now

    Public Sub StartRendering()
        If _isRunning Then
            Return
        End If
        _renderThread = New Thread(AddressOf RenderWorker)
        _isRunning = True
        _renderThread.Start()
    End Sub

    Public Sub StopRendering()
        If Not _isRunning Then
            Return
        End If
        _isRunning = False
        _renderThread = Nothing
        labFPS.Text = "-"
    End Sub

    Public Sub RenderWorker()
        Utilities.Info("RenderWorker started")
        Do While _isRunning
            picMain.Invoke(New Action(AddressOf _panel.Render))

            _lastFPS += 1

            If (DateTime.Now - _lastFPSTime).TotalMilliseconds >= 1000 Then
                labFPS.Text = _lastFPS.ToString()
                _lastFPS = 0
                _lastFPSTime = DateTime.Now
            End If

            Thread.Sleep(0)
        Loop
        Utilities.Info("RenderWorker stopped")
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        _panel = New ElementsPanel(Me)
        _panel.Render()
    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        _panel.AddElement(New Ammeter("A1", New Point(50, 50) - _panel.Origin))
        _panel.Render()
        PropertyGrid1.SelectedObject = _panel.Elements(_panel.Elements.Count - 1)
    End Sub

    Private Sub 删除ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 删除ToolStripMenuItem.Click
        _panel.DeleteSelected()
        _panel.Render()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        _panel.ResetOrigin()
    End Sub

    Private Sub frmMain_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If _panel Is Nothing Then
            Return
        End If
        _panel.Resize()
        _panel.Render()
    End Sub

    Private Sub btnWire_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWire.Click
        _panel.StartDrawWire()
    End Sub

    Private Sub 新建ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 新建ToolStripMenuItem.Click
        NewCircuit()
    End Sub

    Sub NewCircuit()
        _panel.Elements.Clear()
        _panel.ResetOrigin()
        _panel.Render()
    End Sub

    Private Sub PictureBox7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox7.Click
        'Elements.Add(New Element(Element.ElementType.Bell, "B1", New Point(10, 10) - Origin))
        'ReDraw()
    End Sub

    Private Sub PictureBox8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox8.Click
        'Elements.Add(New Element(Element.ElementType.Motor, "M1", New Point(10, 10) - Origin))
        'ReDraw()
    End Sub

    Private Sub PictureBox9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox9.Click
        'Elements.Add(New Element(Element.ElementType.PowerSupply, "", New Point(10, 10) - Origin))
        'ReDraw()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'CanDrawLine = Not CanDrawLine
        'DrawingLine = DrawingLine AndAlso CanDrawLine
        'picMain.Cursor = Cursors.Default
    End Sub

    Private Sub 保存ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 保存ToolStripMenuItem.Click
        Throw New NotImplementedException
        'Using SFD As New SaveFileDialog
        '    SFD.Title = "电路图保存路径"
        '    SFD.Filter = FileFilter
        '    If SFD.ShowDialog = DialogResult.OK Then
        '        'SavePic(SFD.FileName)
        '        WriteCirData(SFD.FileName)
        '    End If
        'End Using
    End Sub

    Sub WriteCirData(ByVal Filename As String)
        Throw New NotImplementedException
        'Dim XD As New XmlDocument()
        'XD.AppendChild(XD.CreateXmlDeclaration("1.0", "UTF-8", String.Empty))
        'Dim root = XD.CreateElement("Circuit")
        'XD.AppendChild(root)
        'For Each e In Elements
        '    Dim XE = XD.CreateElement("Element")
        '    XE.SetAttribute("Title", e.Title)
        '    XE.SetAttribute("Type", e.Type.ToString)
        '    XE.SetAttribute("Rotation", e.Rotation.ToString)
        '    XE.SetAttribute("Location", e.Location.X.ToString & "," & e.Location.Y.ToString)
        '    root.AppendChild(XE)
        'Next

        'For Each e In Lines
        '    Dim XE = XD.CreateElement("Line")
        '    XE.SetAttribute("Point1", e.Point1.X.ToString & "," & e.Point1.Y.ToString)
        '    XE.SetAttribute("Point2", e.Point2.X.ToString & "," & e.Point2.Y.ToString)
        '    root.AppendChild(XE)
        'Next

        'XD.Save(Filename)
    End Sub

    Private Sub 打开ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 打开ToolStripMenuItem.Click
        Throw New NotImplementedException
        'Using OFD As New OpenFileDialog
        '    OFD.Title = "电路图存储路径"
        '    OFD.Filter = FileFilter
        '    If OFD.ShowDialog = DialogResult.OK Then
        '        LoadCirData(OFD.FileName)
        '    End If
        'End Using
    End Sub

    Sub LoadCirData(ByVal Filename As String)
        Throw New NotImplementedException
        'Try
        '    NewCircuit()
        '    Dim XD As New XmlDocument()
        '    XD.Load(Filename)
        '    Dim root = XD.SelectSingleNode("Circuit")
        '    For Each XE As XmlElement In root.SelectNodes("Element")
        '        Dim MyElement As New Element([Enum].Parse(GetType(Element.ElementType), XE.GetAttribute("Type")), XE.GetAttribute("Title"), ToPoint(XE.GetAttribute("Location")))
        '        MyElement.Rotation = (XE.GetAttribute("Rotation") = "True")
        '        Elements.Add(MyElement)
        '    Next

        '    For Each XE As XmlElement In root.SelectNodes("Line")
        '        Dim MyLine As New Line(ToPoint(XE.GetAttribute("Point1")), ToPoint(XE.GetAttribute("Point2")))
        '        Lines.Add(MyLine)
        '    Next
        '    ReDraw()
        'Catch ex As Exception
        '    MsgBox(ex.ToString, 16 + 0, "错误的文件")
        'End Try
    End Sub

    Function ToPoint(ByVal Src As String) As Point
        'Dim PointVal = Split(Src, ",")
        'Return New Point(CInt(PointVal(0)), CInt(PointVal(1)))
    End Function

    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        Throw New NotImplementedException
        'Printer.DocumentName = "电路图"
        'Printer.Print()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If _engine IsNot Nothing Then
            Return
        End If
        _engine = New DigitalEngine()
        _engine.Elements = _panel.Elements
        _engine.Start()
        StartRendering()
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        If _engine Is Nothing Then
            Return
        End If
        _engine.Stop()
        _engine = Nothing
        StopRendering()
    End Sub

    Private Sub ToolStripStatusLabel3_Click(sender As Object, e As EventArgs) Handles ToolStripStatusLabel3.Click

    End Sub
End Class
