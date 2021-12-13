Public Class Cell
    Public possibilities(8) As Boolean
    Public cant_be(8) As Boolean
    Public value As Integer '0-9
    Public row As Integer
    Public col As Integer
    Public reg As Integer

    Public Sub New(v As Integer, r As Integer, c As Integer, reg As Integer)
        value = v
        row = r
        col = c
        Me.reg = reg
    End Sub
End Class
