'Dagan Hartmann
'5/27/2019 - 6/7/2019
'Tapix Tower Defense Main Menu Program

Public Class MenuForm
    Private Sub BtnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        'play sound
        My.Computer.Audio.Play(My.Resources.Click, AudioPlayMode.Background)
        Me.Hide()
        GameForm.Show()
    End Sub

    Private Sub BtnInfo_Click(sender As Object, e As EventArgs) Handles btnInfo.Click
        'play sound
        My.Computer.Audio.Play(My.Resources.Click, AudioPlayMode.Background)
        MsgBox("Welcome to Tapix Tower Defense! A game of shooting enemies before they complete the path! You play this game with the mouse, by placing turrets. Turrets come in three types, maching gun, cannon, and railgun. Turrets of course cost money, which you will get from killing enemies with your turrets! You can also upgrade turrets up to twice. The red upgrade arrow doubles damage, while the yellow doubles fire rate. Upgrading a turret to lvl 2 costs double initial cost, and lvl 3 costs quadruple, so make sure it's worth it! Enemies will come in waves, in greater numbers and strength. Last as long as you can!!", MsgBoxStyle.OkOnly, "Game Information!")
    End Sub
End Class
