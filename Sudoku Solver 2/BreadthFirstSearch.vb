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
        puzzle.IsComplete()
    End Function


    Public Function ExpandNode() As Board
        If queue.Count <> 0 Then
            Dim nodeToExpand As Board = queue.Dequeue()
            numNodesExpanded += 1


            'Execute all legal moves
            If nodeToExpand.IsLegalMove("u") Then
                Dim node2 As Board = New Board(nodeToExpand)
                node2.Move("u")

                If (Not puzzleOrganizer.BinarySearch(node2) = -1) Then

                    queue.Enqueue(node2)
                End If
            End If

            If nodeToExpand.IsLegalMove("d") Then
                Dim node2 As Board = New Board(nodeToExpand)
                node2.Move("d")
                If (Not puzzleOrganizer.BinarySearch(node2) = -1) Then

                    queue.Enqueue(node2)
                End If
            End If

            If nodeToExpand.IsLegalMove("l") Then
                Dim node2 As Board = New Board(nodeToExpand)
                node2.Move("l")
                If (Not puzzleOrganizer.BinarySearch(node2) = -1) Then

                    queue.Enqueue(node2)
                End If
            End If

            If nodeToExpand.IsLegalMove("r") Then
                Dim node2 As Board = New Board(nodeToExpand)
                node2.Move("r")
                If (Not puzzleOrganizer.BinarySearch(node2) = -1) Then

                    queue.Enqueue(node2)
                End If

            End If

            Return nodeToExpand
        End If

        Return Nothing


    End Function
End Class
