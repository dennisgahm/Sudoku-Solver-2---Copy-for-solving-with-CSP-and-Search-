Public Class Board
    Public cells(8, 8) As Cell

    Public Function IsComplete() As Boolean
        'Check if all variables assigned
        For i As Integer = 0 To 8
            For i2 As Integer = 0 To 8
                If cells(i, i2).value = 0 Then
                    Return False
                End If
            Next
        Next
        Return IsConsistent()
    End Function

    Public Function IsSectionConsistent(ByVal c As Cell()) As Boolean
        'loop through each cell in region i
        Dim blns(8) As Boolean
        For i2 As Integer = 0 To 8
            Dim num As Integer = c(i2).value
            If num = 0 Then
                Continue For
            End If

            If blns(num - 1) Then
                Return False
            Else
                blns(num - 1) = True
            End If
        Next

        Return True
    End Function

    Public Function IsConsistent() As Boolean

        'Check regions consistent
        'get all regions
        For i As Integer = 0 To 8
            'region number i
            Dim c As Cell() = Me.getRegion(i)
            Dim bln As Boolean = IsSectionConsistent(c)
            If Not bln Then
                Return False
            End If
        Next

        'Check rows consistent
        'get all rows
        For i As Integer = 0 To 8
            'region number i
            Dim c As Cell() = Me.GetRow(i)
            Dim bln As Boolean = IsSectionConsistent(c)
            If Not bln Then
                Return False
            End If
        Next

        'Check columns consistent
        'get all columns
        For i As Integer = 0 To 8
            'region number i
            Dim c As Cell() = Me.getColumn(i)
            Dim bln As Boolean = IsSectionConsistent(c)
            If Not bln Then
                Return False
            End If
        Next

        Return True
    End Function

    Public Sub New()
        For i As Integer = 0 To 80
            If i = 76 Then
                i = i
            End If
            Dim row As Integer = Math.Truncate(i / 9)
            Dim col As Integer = i Mod 9
            Dim temp As Integer = Math.Truncate(row / 3)
            Dim temp2 As Integer = Math.Truncate(col / 3)
            cells(row, i Mod 9) = New Cell(0, row, i Mod 9, (temp) * 3 + temp2)
        Next
    End Sub

    Public Function clone() As Board
        Dim board As New Board
        For i = 0 To 8
            For i2 = 0 To 8
                board.cells(i, i2).value = cells(i, i2).value
                For i3 = 0 To 8
                    board.cells(i, i2).possibilities(i3) = cells(i, i2).possibilities(i3)
                    board.cells(i, i2).cant_be(i3) = cells(i, i2).cant_be(i3)
                Next
            Next
        Next
        Return board
    End Function
    Public Function sameBoard(b1 As Board, b2 As Board) As Boolean
        For i = 0 To 8
            For i2 = 0 To 8
                If b1.cells(i, i2).value <> b2.cells(i, i2).value Then
                    Return False
                End If
                For i3 = 0 To 8
                    If b1.cells(i, i2).possibilities(i3) <> b2.cells(i, i2).possibilities(i3) Or
                    b1.cells(i, i2).cant_be(i3) <> b2.cells(i, i2).cant_be(i3) Then
                        Return False
                    End If
                Next
            Next
        Next
        Return True
    End Function

    Public Function hasPossibility(cells() As Cell, num As Integer) As Cell()
        Dim values As New List(Of Cell)
        For i = 0 To cells.Length - 1
            If cells(i).possibilities(num - 1) = True Then
                values.Add(cells(i))
            End If
        Next
        Return values.ToArray
    End Function

    Public Function indexOf(cells() As Cell, num As Integer) As Integer
        For i As Integer = 0 To cells.Length - 1
            If cells(i).value = num Then
                Return i
            End If
        Next
        Return -1
    End Function

    Public Function GetRow(i2 As Integer) As Cell()
        Dim values(8) As Cell
        For i = 0 To 8
            values(i) = cells(i2, i)
        Next
        Return values
    End Function

    Public Function getColumn(ByVal col As Integer) As Cell()
        Dim values(8) As Cell
        For i As Integer = 0 To 8
            values(i) = cells(i, col)
        Next
        Return values
    End Function

    'reg = 0 to 8
    Public Function getRegion(ByVal reg As Integer) As Cell()
        Dim values(8) As Cell
        Dim colStart As Integer = (reg Mod 3) * 3
        Dim rowStart As Integer = Math.Truncate(reg / 3) * 3

        Dim valI As Integer = 0
        For i As Integer = rowStart To rowStart + 2
            For i2 As Integer = colStart To colStart + 2
                values(valI) = cells(i, i2)
                valI += 1
            Next
        Next
        Return values
    End Function
End Class
