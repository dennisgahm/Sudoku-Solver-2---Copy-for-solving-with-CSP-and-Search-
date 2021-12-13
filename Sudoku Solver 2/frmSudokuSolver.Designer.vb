<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSudokuSolver
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnSolve = New System.Windows.Forms.Button()
        Me.txtBoard = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Location = New System.Drawing.Point(16, 15)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 750)
        Me.Panel1.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Location = New System.Drawing.Point(1096, 15)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(533, 716)
        Me.Panel2.TabIndex = 1
        '
        'btnSolve
        '
        Me.btnSolve.Location = New System.Drawing.Point(1637, 15)
        Me.btnSolve.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnSolve.Name = "btnSolve"
        Me.btnSolve.Size = New System.Drawing.Size(100, 32)
        Me.btnSolve.TabIndex = 2
        Me.btnSolve.Text = "Solve"
        Me.btnSolve.UseVisualStyleBackColor = True
        '
        'txtBoard
        '
        Me.txtBoard.Location = New System.Drawing.Point(1637, 56)
        Me.txtBoard.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtBoard.Name = "txtBoard"
        Me.txtBoard.Size = New System.Drawing.Size(461, 31)
        Me.txtBoard.TabIndex = 3
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(1715, 156)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(151, 73)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "test"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmSudokuSolver
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(2656, 870)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtBoard)
        Me.Controls.Add(Me.btnSolve)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmSudokuSolver"
        Me.Text = "Sudoku Solver by Dennis"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents btnSolve As Button
    Friend WithEvents txtBoard As TextBox
    Friend WithEvents Button1 As Button
End Class
