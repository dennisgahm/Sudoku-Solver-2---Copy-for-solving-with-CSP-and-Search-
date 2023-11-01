Public Class frmSudokuSolver
    Dim textboxes(80) As TextBox
    Dim board As Board = New Board
    Private Sub frmSudokuSolver_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Create textboxes for input of Sudoku Board
        For i As Integer = 0 To 80
            textboxes(i) = New TextBox()
            textboxes(i).Size = New Drawing.Size(26, 26)
            textboxes(i).Left = 26 * (i Mod 9) + Math.Truncate((i Mod 9) / 3) * 5
            Dim temp As Integer = Math.Truncate(i / 9)
            textboxes(i).Top = 20 * temp + Math.Truncate(temp / 3) * 5
            Panel2.Controls.Add(textboxes(i))
        Next




    End Sub

    Private Sub btnSolve_Click(sender As Object, e As EventArgs) Handles btnSolve.Click
        'If txtBoard is empty, then convert the textboxes into a string and solve
        If txtBoard.Text = "" Then
            For i As Integer = 0 To 80
                If textboxes(i).Text = "" Then
                    txtBoard.AppendText("0")
                Else
                    txtBoard.AppendText(textboxes(i).Text)
                End If
            Next
            CreateBoard()
            Solve()
        Else
            CreateBoard()
            Solve()
        End If

    End Sub

    Private Sub CreateBoard()
        'Creates the board and also fills the textboxes
        For i As Integer = 0 To 80
            Dim c As String = txtBoard.Text.Chars(i)
            Dim temp As Integer = Integer.Parse(c)
            board.cells(Math.Truncate(i / 9), i Mod 9).value = temp
            textboxes(i).Text = temp 'I believe the textboxes are already filled - but this adds 0s in the place of ""
        Next
    End Sub

    Private Sub Solve()
        Dim cont As Boolean = True
        While cont
            Dim boardOriginal As Board = board.clone()
            findAllPossibilities()
            solveOnePossibility()
            solveOnePossibility2()
            possibilitiesCrossOut()
            findAllPossibilities()
            solveOnePossibility()
            solveOnePossibility2()
            If board.sameBoard(boardOriginal, board) Then
                cont = False
            End If
        End While
        Me.Refresh()
    End Sub

    'If a possibility in a region occurs only in a row or column of spaces, 
    'then all spaces in the same row or column outside this region cannot have that possibility.  (Related to another rule)
    Private Sub possibilitiesCrossOut()
        For i As Integer = 0 To 8 'iterate through regions
            Dim region() As Cell = board.getRegion(i)
            For num As Integer = 1 To 9 'iterate through all possible numbers
                Dim cellsWithPossibility() As Cell = board.hasPossibility(region, num)
                If cellsWithPossibility.Length = 0 Then
                    Continue For
                ElseIf cellsWithPossibility.Length = 1 Then
                    Throw New System.Exception("only one possibility in a section when this situation is covered by previous method")
                End If
                If i = 8 And num = 6 Then
                    num = num
                End If
                Dim sameRow As Boolean = True
                Dim sameCol As Boolean = True
                For i2 As Integer = 1 To cellsWithPossibility.Length - 1
                    If cellsWithPossibility(i2 - 1).row <> cellsWithPossibility(i2).row Then
                        sameRow = False
                    End If
                    If cellsWithPossibility(i2 - 1).col <> cellsWithPossibility(i2).col Then
                        sameCol = False
                    End If
                Next
                If sameRow Then
                    Dim row As Integer = cellsWithPossibility(0).row
                    'range of col of region is colStart to colStart+2
                    Dim colStart As Integer = (i Mod 3) * 3
                    Dim rowCells() As Cell = board.GetRow(row)
                    For rowi As Integer = 0 To rowCells.Length - 1
                        If Not (rowCells(rowi).col >= colStart And rowCells(rowi).col <= colStart + 2) Then
                            rowCells(rowi).cant_be(num - 1) = True
                        End If
                    Next
                ElseIf sameCol Then
                    Dim col As Integer = cellsWithPossibility(0).col
                    'range of row of region is rowStart to rowStart+2
                    Dim rowStart As Integer = Math.Truncate(i / 3) * 3
                    Dim colCells() As Cell = board.getColumn(col)
                    For coli As Integer = 0 To colCells.Length - 1
                        If Not (colCells(coli).row >= rowStart And colCells(coli).row <= rowStart + 2) Then
                            colCells(coli).cant_be(num - 1) = True
                        End If
                    Next
                End If
            Next
        Next
    End Sub

    'If there is a possibility appearing once in a row/col/region, then set that cell
    Private Sub solveOnePossibility2()
        'look at every row
        For i As Integer = 0 To 8
            Dim row() As Cell = board.GetRow(i)
            'iNum = looking at every possible number from 0 to 8
            For iNum As Integer = 0 To 8
                Dim col As Integer = -1 'keep track of the col of the cell at which iNum is a possibility
                Dim count As Integer = 0
                For i2 As Integer = 0 To 8
                    If board.cells(i, i2).possibilities(iNum) Then
                        count += 1 'set the count of the number of times iNum is a possibility in this row
                        col = i2
                    End If
                Next
                'if the count of possibilities of iNum = 1
                If count = 1 Then
                    board.cells(i, col).value = iNum + 1
                    findAllPossibilities()
                    solveOnePossibility2()
                    Return
                End If
            Next
        Next

        For i As Integer = 0 To 8
            For iNum As Integer = 0 To 8
                Dim row As Integer = -1
                Dim count As Integer = 0
                For i2 As Integer = 0 To 8
                    If board.cells(i2, i).possibilities(iNum) Then
                        count += 1
                        row = i2
                    End If
                Next
                If count = 1 Then
                    board.cells(row, i).value = iNum + 1
                    findAllPossibilities()
                    solveOnePossibility2()
                    Return
                End If
            Next
        Next

        'iterate through all the regions
        For i As Integer = 0 To 8
            Dim colStart As Integer = (i Mod 3) * 3
            Dim rowStart As Integer = Math.Truncate(i / 3) * 3
            For iNum As Integer = 0 To 8
                If iNum = 7 Then
                    iNum = iNum
                End If
                Dim row As Integer = -1
                Dim col As Integer = -1
                Dim count As Integer = 0
                For i2 As Integer = 0 To 8
                    Dim r As Integer = rowStart + Math.Truncate(i2 / 3)
                    Dim c As Integer = colStart + (i2 Mod 3)
                    If board.cells(rowStart + Math.Truncate(i2 / 3), colStart + (i2 Mod 3)).possibilities(iNum) Then
                        count += 1
                        row = rowStart + Math.Truncate(i2 / 3)
                        col = colStart + (i2 Mod 3)
                    End If
                Next
                If count = 1 Then
                    board.cells(row, col).value = iNum + 1
                    findAllPossibilities()
                    solveOnePossibility2()
                    Return
                End If
            Next
        Next
    End Sub

    'If there's one possibility in the possibilities array, then set the cell
    Private Sub solveOnePossibility()
        'iterate through all the cells
        Dim i As Integer = 0
        While i < 81
            Dim row As Integer = Math.Truncate(i / 9)
            Dim col As Integer = i Mod 9
            If board.cells(row, col).value = 0 Then
                Dim numPossibilitiesCount As Integer = 0
                Dim latestPossibility As Integer = 0
                For i2 As Integer = 0 To 8
                    If board.cells(row, col).possibilities(i2) Then
                        numPossibilitiesCount += 1
                        latestPossibility = i2 + 1
                    End If
                Next
                'if the number of possibilities of this cell = 1
                If numPossibilitiesCount = 1 Then
                    board.cells(row, col).value = latestPossibility
                    i = 0
                    'After setting this cell, we need to recalculate the possibilities of the sections that include this cell -> so we need to call findAllPossibilities()
                    findAllPossibilities()
                Else
                    i += 1
                End If
            Else
                i += 1
            End If
        End While
    End Sub

    'sets the possibilities of all cells
    Private Sub findAllPossibilities()
        'first set all possibilities to false
        For i As Integer = 0 To 8
            For i2 As Integer = 0 To 8
                For i3 As Integer = 0 To 8
                    board.cells(i, i2).possibilities(i3) = False
                Next
            Next
        Next

        'set all possibilities for each cell
        For i As Integer = 0 To 8
            For i2 As Integer = 0 To 8
                If board.cells(i, i2).value = 0 Then
                    findAllPossibilitiesForCell(i, i2)
                End If

            Next
        Next
    End Sub

    'sets all possibilities for a cell
    Private Sub findAllPossibilitiesForCell(ByVal row As Integer, col As Integer)
        For i As Integer = 0 To 8
            If (consistencyCheck(row, col, i + 1)) Then
                If Not board.cells(row, col).cant_be(i) Then
                    board.cells(row, col).possibilities(i) = True
                End If
            End If
        Next
    End Sub

    'checks if a cell has the possibility to be num
    Private Function consistencyCheck(ByVal row As Integer, col As Integer, num As Integer) As Boolean
        Dim region As Integer = Math.Truncate(col / 3)
        region += Math.Truncate(row / 3) * 3
        If board.indexOf(board.GetRow(row), num) <> -1 Or
            board.indexOf(board.getColumn(col), num) <> -1 Or
            board.indexOf(board.getRegion(region), num) <> -1 Then
            Return False
        End If
        Return True
    End Function

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
        'Draw horizontal lines
        Dim height As Integer = Panel1.Size.Height / 9
        Dim thickPen As New Pen(Color.Blue, 4)
        For i As Integer = 0 To 9
            If i Mod 3 = 0 Then
                e.Graphics.DrawLine(thickPen, 0, height * i, Panel1.Size.Width, height * i)
            Else
                e.Graphics.DrawLine(Pens.Black, 0, height * i, Panel1.Size.Width, height * i)
            End If
        Next

        Dim width As Integer = Panel1.Size.Width / 9
        For i As Integer = 0 To 9
            If i Mod 3 = 0 Then
                e.Graphics.DrawLine(thickPen, width * i, 0, width * i, Panel1.Size.Height)
            Else
                e.Graphics.DrawLine(Pens.Black, width * i, 0, width * i, Panel1.Size.Height)
            End If
        Next

        For i As Integer = 0 To 80
            Dim row As Integer = Math.Truncate(i / 9)
            Dim col As Integer = i Mod 9
            If board.cells(row, col).value <> 0 Then
                e.Graphics.DrawString(board.cells(row, col).value, New System.Drawing.Font("Arial", 24), Brushes.Black, width * col + width / 4, height * row + height / 5)
            Else
                For i2 As Integer = 0 To 8
                    If board.cells(row, col).possibilities(i2) Then
                        Dim rowP As Integer = Math.Truncate(i2 / 3)
                        Dim colP As Integer = i2 Mod 3
                        e.Graphics.DrawString(i2 + 1, New System.Drawing.Font("Arial", 10), Brushes.Black, width * col + width / 4 + (width / 7) * colP, height * row + (height / 3) * rowP)
                    End If
                Next
            End If
        Next
    End Sub


    Private Sub TestButton()
        'debug/test
        If txtBoard.Text = "" Then
            For i As Integer = 0 To 80
                If textboxes(i).Text = "" Then
                    txtBoard.AppendText("0")
                Else
                    txtBoard.AppendText(textboxes(i).Text)
                End If
            Next
        End If
        CreateBoard()
        Me.Text = board.IsConsistent()

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'Solver
        If txtBoard.Text = "" Then
            For i As Integer = 0 To 80
                If textboxes(i).Text = "" Then
                    txtBoard.AppendText("0") 'Shows that boards have 0 for empty cells
                Else
                    txtBoard.AppendText(textboxes(i).Text)
                End If
            Next
        End If
        CreateBoard()

        'Pseudocode
        '   While !finished
        '       Check if finished
        '       consistency check
        '       if true, then assign a variable a value
        '           This step uses heuristics
        'Me.Text = board.IsConsistent()

        'note: eventually use the inferences of the search in the form
        '   even in bruteforce search, try implementing the form functions
        Dim blnFinished As Boolean = False
        While Not blnFinished
            'check for infinite loop or search complete and failed
            'If got to this point: blnFinished = false



        End While
    End Sub
End Class
