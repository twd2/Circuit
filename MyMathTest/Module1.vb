Imports Circuit
Imports System.Drawing

Module Module1

    Public Sub test()
        Dim a As New Point(4, 2)
        Dim b As New Point(2, 4)
        Dim c As New Point(3, 4)

        Dim ans As Double

        Dim sw As New Stopwatch()

        Dim count = 100000000

        sw.Reset()
        sw.Start()
        For i = 1 To count
            ans = MyMath.PointToLine(a, b, c)
        Next
        sw.Stop()
        Console.WriteLine(ans)
        Console.WriteLine(sw.ElapsedMilliseconds.ToString() + "ms")

        sw.Reset()
        sw.Start()
        For i = 1 To count
            ans = MyMath.PointToLine_way2(a, b, c)
        Next
        sw.Stop()
        Console.WriteLine(ans)
        Console.WriteLine(sw.ElapsedMilliseconds.ToString() + "ms")
    End Sub

    Sub Main()
        test()
        Console.ReadKey()
    End Sub

End Module
