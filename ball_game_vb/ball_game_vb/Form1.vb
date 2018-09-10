Public Class Form1
    Dim binfo As BallClass
    Dim score, life As Integer
    Public Class BallClass
        Public rad, c As Double
        Public dir As Char
        Public dx, dy As Integer
        Public Sub calculatec()
            Dim pt As Point = Form1.ball.Location
            c = pt.Y - (Math.Tan(rad) * pt.X)
        End Sub
        Public Sub move()
            Dim cur As Point = Form1.ball.Location
            If Me.rad < Math.PI / 18 Then
                Me.rad -= Math.PI / 90
            End If
            If dir = "x" Then
                cur.X += dx
                cur.Y = (Math.Tan(rad) * cur.X) + c
            ElseIf dir = "y" Then
                cur.Y += dy
                cur.X = (cur.Y - c) / Math.Tan(rad)
            End If
            Form1.ball.Location = cur
            Form1.Label3.Text = ((180 / Math.PI) * Me.rad).ToString
        End Sub
    End Class
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim cur As Point = ball.Location
        If cur.X <= 0 Or cur.X + ball.Width >= Me.Width Then

            binfo.dx *= (-1)
            binfo.dir = "x"
            binfo.rad = Math.PI - binfo.rad
            binfo.calculatec()

        ElseIf cur.Y <= 0 Then
            'binfo.dx *= (-1)
            'binfo.dir = "y"
            binfo.rad = (Math.PI - binfo.rad)
            binfo.calculatec()
        ElseIf cur.Y >= Me.Height Then
            life -= 1
            Label1.Text = "Lives: " + life.ToString
            initGame()
            If life = 0 Then
                MessageBox.Show("Your score: " + score.ToString, "Game over!")
                resetGame()
            End If

        ElseIf (cur.Y + ball.Height <= bar.Top + 3) And (cur.Y + ball.Height >= bar.Top) And (ball.Left >= bar.Left) And (ball.Left + (ball.Width / 2) <= bar.Left + bar.Width) Then
            'Label2.Text = "ball left : " + ball.Left.ToString + "ball w: " + ball.Width.ToString + "bar.le: " + bar.Left.ToString + "bar. wi: " + bar.Width.ToString
            ball.Top = bar.Top - ball.Height
            binfo.rad = (Math.PI - binfo.rad)
            binfo.calculatec()
            score += 10
            Label2.Text = "Score: " + score.ToString
            'Timer1.Enabled = False
        End If
        binfo.move()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        resetGame()
    End Sub

    Private Sub initGame()
        Timer1.Enabled = False
        binfo = New BallClass
        binfo.dx = -5
        binfo.dy = -5
        binfo.dir = "x"
        bar.Left = (Me.Width / 2) - (bar.Width / 2)
        ball.Location = New Point(bar.Location.X + (bar.Width / 2) - (ball.Width / 2), bar.Location.Y - ball.Height - 3)
        binfo.rad = (Math.PI / (4 + Rnd(4)))
        binfo.calculatec()
    End Sub
    Private Sub resetGame()
        initGame()
        score = 0
        life = 3
        Label1.Text = "Lives: " + life.ToString
        Label2.Text = "Score: " + score.ToString
    End Sub
    Private Sub bar_keypress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles bar.KeyPress, MyBase.KeyPress
        Dim pt As Point = bar.Location
        If e.KeyChar = "a" Then
            bar.Left -= 50
        ElseIf e.KeyChar = "d" Then
            bar.Left += 50
        ElseIf e.KeyChar = " " Then
            If Timer1.Enabled = False Then
                Timer1.Enabled = True
            End If
        End If
        If bar.Left < 0 Then
            bar.Left = 0
        ElseIf bar.Left + bar.Width > Me.Width Then
            bar.Left = Me.Width - bar.Width
        End If

    End Sub

End Class
