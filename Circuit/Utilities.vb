Public Class Utilities

    Public Shared Sub Info(s As String)
        Debug.Print(String.Format("[{0} INFO] {1}", DateTime.Now, s))
    End Sub

    Public Shared Sub Warn(s As String)
        Debug.Print(String.Format("[{0} WARNING] {1}", DateTime.Now, s))
    End Sub

    Public Shared Sub [Error](s As String)
        Debug.Print(String.Format("[{0} ERROR] {1}", DateTime.Now, s))
    End Sub

End Class
