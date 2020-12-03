Public Class Chinesepacman
    Dim p1 As Point
    Dim score As Integer
    Dim life As Integer = 3
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        follow(orangeghost)

    End Sub
    'Sub move(p As PictureBox, x As Integer, y As Integer)
    '    p.Location = New Point(p.Location.X + x, p.Location.Y + y)

    'End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Up
                MoveTo(PictureBox1, 0, -5)
            Case Keys.Down
                MoveTo(PictureBox1, 0, 5)
            Case Keys.Left
                MoveTo(PictureBox1, -5, 0)
            Case Keys.Right
                MoveTo(PictureBox1, 5, 0)
            Case Keys.Space
                'bullet.Location = Picturebox1.Location
                'bullet.Visible = True
                'Timer6.Enabled = True
            Case Keys.R
                PictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone)
                Me.Refresh()
        End Select
    End Sub
    Sub follow(p As PictureBox)
        Static headstart As Integer
        Static c As New Collection
        c.Add(Me.PictureBox1.Location)
        headstart = headstart + 1
        If headstart > 20 Then
            p.Location = c.Item(1)
            c.Remove(1)
        End If
    End Sub
    Public Sub chase2(p As PictureBox, pnt As Point)
        Dim x, y As Integer
        If p.Location.X > pnt.X Then
            x = -5
        Else
            x = 5
        End If
        MoveTo(p, x, 0)
        If p.Location.Y < pnt.Y Then
            y = 5
        Else
            y = -5
        End If
        MoveTo(p, x, y)
    End Sub
    Public Sub chase(p As PictureBox)
        Dim x, y As Integer
        If p.Location.X > Me.PictureBox1.Location.X Then
            x = -5
        Else
            x = 5
        End If
        MoveTo(p, x, 0)
        If p.Location.Y < Me.PictureBox1.Location.Y Then
            y = 5
        Else
            y = -5
        End If
        MoveTo(p, x, y)
    End Sub



    Function Collision(p As PictureBox, t As String, Optional ByRef other As Object = vbNull)
        Dim col As Boolean

        For Each c In Controls
            Dim obj As Control
            obj = c
            If obj.Visible AndAlso p.Bounds.IntersectsWith(obj.Bounds) And obj.Name.ToUpper.Contains(t.ToUpper) Then
                col = True
                other = obj
            End If
        Next
        Return col
    End Function
    'Return true or false if moving to the new location is clear of objects ending with t
    Function IsClear(p As PictureBox, distx As Integer, disty As Integer, t As String) As Boolean
        Dim b As Boolean

        p.Location += New Point(distx, disty)
        b = Not Collision(p, t)
        p.Location -= New Point(distx, disty)
        Return b
    End Function
    'Moves and object (won't move onto objects containing  "wall" and shows green if object ends with "win"
    Sub MoveTo(p As PictureBox, distx As Integer, disty As Integer)
        If IsClear(p, distx, disty, "WALL") Then
            p.Location += New Point(distx, disty)
            If life = 0 Then
                loserBox.Visible = True
            End If
        End If
            Dim other As Object = Nothing
        If p.Name = "PictureBox1" And Collision(p, "score", other) Then
            score = score + 1
            If score = 10 Then
                Timer1.Enabled = True
                Timer2.Enabled = True
                Timer3.Enabled = True
                Timer4.Enabled = True
            End If
            If score = 81 Then
                winnerBox.Visible = True
            End If
            other.visible = False
            Return

        End If
        If p.Name = "PictureBox1" And Collision(p, "ghost", other) Then

            If Collision(p, "ghost") Then
                life = life - 1

            End If
        End If
    End Sub


    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        follow(greenghost)
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick

        follow(blueghost)
    End Sub

    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick

        follow(redghost)
        chase2(redghost, p1)

    End Sub

    Private Sub Timer5_Tick(sender As Object, e As EventArgs) Handles Timer5.Tick
        p1 = PictureBox1.Location
    End Sub

    Private Sub Timer6_Tick(sender As Object, e As EventArgs) Handles Timer6.Tick
        'MoveTo(bullet, 0, -5)
        scoreLabel.Text = score
        lifelabel.Text = life
    End Sub

    Private Sub lifelabel_Click(sender As Object, e As EventArgs) Handles lifelabel.Click

    End Sub
End Class