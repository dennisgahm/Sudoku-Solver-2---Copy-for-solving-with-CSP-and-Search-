Public Class BreadthFirstSearch
    Dim board As Board = New Board
    Public queue As Queue(Of Board) = New Queue(Of Board)()
    Public numNodesExpanded As Integer = 0

    Public Sub New(ByRef puzzle As Board)
        board = puzzle
        queue.Enqueue(board)
    End Sub

    Public Function FindSolution()
        numNodesExpanded = 0
        While queue.Count > 0
            'Board = bfs.queue.Dequeue()
            Dim nodeExpanded As Board = ExpandNode()
            If IsSolution(nodeExpanded) Then
                Return nodeExpanded
                Exit While
            End If
        End While
        Return Nothing
    End Function

    Public Function IsSolution(ByVal puzzle As Board) As Boolean
        Return puzzle.IsComplete()
    End Function


    Public Function ExpandNode() As Board
        If queue.Count <> 0 Then
            Dim nodeToExpand As Board = queue.Dequeue()
            numNodesExpanded += 1

            Dim clone As Board = nodeToExpand.clone()
            Dim clone2 As Board = nodeToExpand.clone()

            Dim blnFinished As Boolean = False
            'Create a node that sets on of the possibilities to true.
            For i As Integer = 0 To 8
                For i2 As Integer = 0 To 8
                    If clone.cells(i, i2).value = 0 Then
                        For i3 As Integer = 0 To 8
                            If clone.cells(i, i2).possibilities(i3) = True Then
                                clone2.cells(i, i2).value = i3 + 1
                                blnFinished = True

                                clone.cells(i, i2).possibilities(i3) = False
                                Exit For
                            End If
                        Next
                    End If

                    If blnFinished Then
                        Exit For
                    End If
                Next

                If blnFinished Then
                    Exit For
                End If
            Next

            'add node to queue
            queue.Enqueue(clone2)

            'Create a node that sets the possibility to be taken and false
            queue.Enqueue(clone)


            Return nodeToExpand
        End If

        Return Nothing


    End Function
End Class
