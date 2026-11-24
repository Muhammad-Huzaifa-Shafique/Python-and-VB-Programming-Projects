Public Class Form1
    Private Sub Button1_Click(Sender As Object, e As EventArgs) Handles Button1.Click
        PictureBox1.Visible = False
        Label1.Text = Int(Rnd() * 20)
        Label2.Text = Int(Rnd() * 20)
        Label3.Text = Int(Rnd() * 20)
        If Label1.Text = 10 Or Label2.Text = 10 Or Label3.Text = 10 Then
            PictureBox1.Visible = True
        End If
    End Sub
    Private Sub Button2_Click(Sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class
