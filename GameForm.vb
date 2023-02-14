'Dagan Hartmann
'5/27/2019 - 6/7/2019
'Tapix Tower Defense Main Game Program

Public Class GameForm
    Public Const TURRET_SCALE As Integer = 38       'the amount of pixels a tile is, or in other words how much to multiply coordinated by
    Public Const TIMER_TICK_TIME As Integer = 40    'the time it takes the timer to tick

    'This is a 23x11 grid for all squares. It is in (y, x) format, so the board could be easily built here.
    '0 for open, 1 for off limits (path and UI), 2 for turret.
    '                                                       0, 1, 2, 3, 4, 5, 6, 7, 8, 9,10,11,12,13,14,15,16,17,18,19,20,21,22,23
    Public allSquares(,) As Integer = New Integer(10, 23) {{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1}, '0
                                                           {0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1}, '1
                                                           {0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1}, '2
                                                           {1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1}, '3
                                                           {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1}, '4
                                                           {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1}, '5
                                                           {0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1}, '6
                                                           {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1}, '7
                                                           {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1}, '8
                                                           {1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1}, '9
                                                           {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1}} '10

    'list of all towers and all enemies
    Public towers As New List(Of Tower)
    Public enemies As New List(Of Enemy)
    Public projectiles As New List(Of Projectile)

    'define tower types to be added to the list
    Dim prjCannon As Projectile = New Projectile(6, 10, "TProC", New Vector2(0, 0))
    Dim prjMachineGun As Projectile = New Projectile(8, 4, "TProMach", New Vector2(0, 0))
    Dim prjRailgun As Projectile = New Projectile(12, 20, "TProRail", New Vector2(0, 0))

    Dim twrCannon As New Tower(130, 10, 150, 20, "TGunRail_", "TBaseC_", "TTopC_", New Vector2(0, 0), prjCannon, "Shoot_Cannon", False)
    Dim twrMachineGun As New Tower(80, 4, 100, 4, "TGunMach_", "TBaseCr_", "TTopDia_", New Vector2(0, 0), prjMachineGun, "Shoot_Mach", False)
    Dim twrRailgun As New Tower(250, 25, 250, 80, "TGunRailV2_", "TBaseRDim_", "TTopSqr_", New Vector2(16, 0), prjRailgun, "Shoot_Rail", True)

    Dim twrChoice As Tower = twrCannon

    'the mouses possition (for turret placement)
    Dim mousePosition As Vector2
    Dim mousePositionRaw As Vector2

    'define enemy types to be added to the list
    Dim enmTankBlue As New Enemy(3, 5, 10, 5, "TankBlue", enemySpawnPoint)          'basic tank. 3 speed, 05 damage, 010 health, drops 05$
    Dim enmTankGreen As New Enemy(3, 10, 40, 10, "TankGreen", enemySpawnPoint)      'basic tank. 3 speed, 10 damage, 040 health, drops 20$
    Dim enmTankYellow As New Enemy(3, 20, 160, 40, "TankYellow", enemySpawnPoint)   'basic tank. 3 speed, 20 damage, 160 health, drops 40$
    Dim enmTankRed As New Enemy(3, 40, 640, 80, "TankRed", enemySpawnPoint)         'basic tank. 3 speed, 40 damage, 640 health, drops 80$

    'important game variables
    Dim health As Integer = 100
    Dim money As Integer = 250

    'wave logic related variables
    Dim wave As Integer = 0     'what wave it is
    Dim score As Integer        'our score
    Dim multiplierPerWave As Integer = 5    'the amount of enemies that spawn each wave more than the last 
    Dim initialEnemySpawn As Integer = 5    'the amount of enemies that spawn on wave 0
    Dim timePerWave As Integer = 16 * 1000  'this is the time between waves in ms
    Dim totalTime As Long = 0            'this is the total amount of time since the game started in ms
    Dim lastWaveTime As Integer = -20000      'this was the time since start in ms since the last wave
    Dim enemySpawnPoint As Vector2 = New Vector2(-20, 3 * TURRET_SCALE)
    Dim playerLost As Boolean = False       'if we have lost

    'upgrading variables
    Dim upgrading As Boolean = False    'if we are upgrading
    Dim uppingSpeed As Boolean          'the type of upgrade, false for damage, true for speed

    Function distance(a As Vector2, b As Vector2) As Decimal
        Return Math.Sqrt((b.x - a.x) ^ 2 + (b.y - a.y) ^ 2)
    End Function

    Private Sub GameForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Tick.Start()
    End Sub

    Private Sub newTower(copyTower As Tower, position As Vector2)
        If allSquares(mousePosition.y, mousePosition.x) = 1 Then  'if square is taken by path
            Return
        ElseIf allSquares(mousePosition.y, mousePosition.x) = 2 Then  'if square is taken by tower
            Return
        End If

        towers.Add(copyTurret(copyTower))
        towers(towers.Count - 1).position = position
        towers(towers.Count - 1).baseSprite = New PictureBox
        towers(towers.Count - 1).gunSprite = New PictureBox
        towers(towers.Count - 1).topSprite = New PictureBox
        towers(towers.Count - 1).Initialize(Me)
        allSquares(mousePosition.y, mousePosition.x) = 2
    End Sub

    Private Function copyTurret(copy As Tower) As Tower
        Return New Tower(copy.range, copy.damage, copy.cost, copy.fireTime, copy.rotationSpriteName, copy.baseSpriteName, copy.topSpriteName, copy.weaponOffset, copy.projectileType, copy.shootSound, copy.targetReverse)
    End Function

    Private Function copyEnemy(copy As Enemy) As Enemy
        Return New Enemy(copy.speed, copy.damage, copy.health, copy.moneyOnDeath, copy.spriteName, copy.position)
    End Function

    Private Sub GameForm_Close(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.Closed
        'if this form is closed but the player didnt loose, assume the form was manually closed and close the game
        If playerLost = False Then
            MenuForm.Close()
        End If
    End Sub

    Private Sub GameForm_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove

        'convert the mouse position to grid squares
        Dim roundedX As Integer = Math.Round((e.X - 14) / TURRET_SCALE)
        Dim roundedY As Integer = Math.Round((e.Y - 14) / TURRET_SCALE)
        mousePosition = New Vector2(roundedX, roundedY)
        mousePositionRaw = New Vector2(e.X, e.Y)

        'set the highlight to the nearest grid square
        picHighlight.Top = roundedY * TURRET_SCALE
        picHighlight.Left = roundedX * TURRET_SCALE
    End Sub

    Private Sub MouseClick_click(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.Click, picHighlight.Click
        'if the player lost do nothing
        If playerLost Then
            Return
        End If

        'if we are placing a turret vs upgrading turrets
        If upgrading Then
            'if we clicked on a square with a turret
            If allSquares(mousePosition.y, mousePosition.x) = 2 Then
                'find the turret with the matching position
                For i As Integer = 0 To towers.Count - 1
                    If towers(i).position = mousePosition Then
                        'if we can afford it upgrade it
                        'its upgrade costs are built so that upgrading from level one is double original cost
                        'and from level two is quadruple original cost
                        If money - (towers(i).cost * 2 * towers(i).level) >= 0 Then
                            'if the tower is not max level
                            If towers(i).level < 3 Then
                                'subtract money
                                money -= towers(i).cost * 2 * towers(i).level
                                'upgrade the tower
                                towers(i).LevelUp(Me, uppingSpeed)
                                'play upgrade sound
                                My.Computer.Audio.Play(My.Resources.Upgrade, AudioPlayMode.Background)
                            End If
                        End If
                    End If
                Next
            End If
        Else
            'check if player can afford tower
            If money >= twrChoice.cost Then
                'check if the square here is empty
                If allSquares(mousePosition.y, mousePosition.x) = 0 Then
                    newTower(twrChoice, mousePosition)
                    money -= twrChoice.cost
                    'play build sound
                    My.Computer.Audio.Play(My.Resources.Place, AudioPlayMode.Background)
                    Debug.Print("Got a click and placed tower.")
                End If
            Else
                Debug.Print("Got a click but could not afford tower.")
            End If
        End If
    End Sub

    Private Sub SpawnEnemies()
        'equation for finding out how many enemies we want to spawn
        'wave squared times multiplier per wave over 10 plus initial
        Dim enemiesToSpawn = initialEnemySpawn + wave * wave * multiplierPerWave / 3
        wave += 1
        lblWave.Text = "Wave " + CStr(wave)
        Debug.Print("Spawning " + CStr(enemiesToSpawn) + " enemies.")

        Dim spawningNum As Integer = 0

        While enemiesToSpawn > 513
            'spawn a red tank worth 8 yellow tanks
            Dim tempEnemy As Enemy = copyEnemy(enmTankRed)
            tempEnemy.position = New Vector2(enemySpawnPoint.x - 30 * spawningNum, enemySpawnPoint.y)
            enemies.Add(tempEnemy)
            enemies(spawningNum).Initialize()
            enemiesToSpawn -= 256
            spawningNum += 1
        End While
        While enemiesToSpawn > 65 And enemiesToSpawn < 513
            'spawn a yellow tank worth 8 green tanks
            Dim tempEnemy As Enemy = copyEnemy(enmTankYellow)
            tempEnemy.position = New Vector2(enemySpawnPoint.x - 30 * spawningNum, enemySpawnPoint.y)
            enemies.Add(tempEnemy)
            enemies(spawningNum).Initialize()
            enemiesToSpawn -= 64
            spawningNum += 1
        End While
        While enemiesToSpawn > 9 And enemiesToSpawn < 65
            'spawn a green tank worth 8 blue tanks
            Dim tempEnemy As Enemy = copyEnemy(enmTankGreen)
            tempEnemy.position = New Vector2(enemySpawnPoint.x - 30 * spawningNum, enemySpawnPoint.y)
            enemies.Add(tempEnemy)
            enemies(spawningNum).Initialize()
            enemiesToSpawn -= 8
            spawningNum += 1
        End While
        While enemiesToSpawn > 0 And enemiesToSpawn < 9
            'spawn a blue tank
            Dim tempEnemy As Enemy = copyEnemy(enmTankBlue)
            tempEnemy.position = New Vector2(enemySpawnPoint.x - 30 * spawningNum, enemySpawnPoint.y)
            enemies.Add(tempEnemy)
            enemies(spawningNum).Initialize()
            enemiesToSpawn -= 1
            spawningNum += 1
        End While
    End Sub

    Private Sub GameForm_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint

        'draw bullets
        For i As Integer = 0 To projectiles.Count - 1

            'was getting index out of range errors when game slows down and bullets are spawned befoer this loop is finished
            'implimented loop to check
            If i < projectiles.Count Then
                Dim p As Projectile = projectiles(i)

                Dim deltax
                Dim deltay
                If (p.rotation = -1) Then
                    deltax = mousePositionRaw.x - p.position.x * TURRET_SCALE - p.sprite.Image.Width / 2
                    deltay = mousePositionRaw.y - p.position.y * TURRET_SCALE - p.sprite.Image.Height / 2
                    p.rotation = (Math.Atan2(deltay, deltax) * 180 / Math.PI) + 180
                End If
                'was getting problematic bullets who's position is in the millions with no explanation
                'this deletes any bullets without reasonable positions
                If Math.Abs(Math.Round(p.position.x - p.spriteOffset.x)) > 1000 Then
                    projectiles.Remove(p)
                ElseIf Math.Abs(Math.Round(p.position.y - p.spriteOffset.y)) > 1000 Then
                    projectiles.Remove(p)
                End If

                '****rotation for the projectile sprite****
                'decide where to draw - this code uses the upper left corner
                Dim upperLeftDrawPoint As Point = New Point(Math.Round(p.position.x - p.spriteOffset.x), Math.Round(p.position.y - p.spriteOffset.y))
                'calculate the center of the image
                Dim imageCenterOffset As Point = New Point(p.sprite.Image.Width / 2, p.sprite.Image.Height / 2)
                'translate the graphics origin to the destination image center before rotating
                e.Graphics.TranslateTransform(upperLeftDrawPoint.X + imageCenterOffset.X, upperLeftDrawPoint.Y + imageCenterOffset.Y)
                'rotate the matrix around the origin
                e.Graphics.RotateTransform(p.rotation)
                'translate the graphics origin back to the upper left image destination before drawing
                e.Graphics.TranslateTransform((upperLeftDrawPoint.X + imageCenterOffset.X) * -1, (upperLeftDrawPoint.Y + imageCenterOffset.Y) * -1)
                'draw the image
                e.Graphics.DrawImage(p.sprite.Image, upperLeftDrawPoint)
                'reset all changes to the transformation matrix
                e.Graphics.ResetTransform()
            Else
                Debug.Print("Projectile index out of range")
            End If
        Next

        'draw enemies
        For Each en As Enemy In enemies
            If en.sprite.Image IsNot Nothing And en.position IsNot Nothing Then
                '****rotation for the enemy****
                'decide where to draw - this code uses the upper left corner
                Dim upperLeftDrawPoint As Point = New Point(en.position.x, en.position.y)
                'calculate the center of the image
                Dim imageCenterOffset As Point = New Point(en.sprite.Image.Width / 2, en.sprite.Image.Height / 2)
                'translate the graphics origin to the destination image center before rotating
                e.Graphics.TranslateTransform(upperLeftDrawPoint.X + imageCenterOffset.X, upperLeftDrawPoint.Y + imageCenterOffset.Y)
                'rotate the matrix around the origin                
                Select Case en.direction
                    Case 0
                        'north so dont rotate
                        e.Graphics.RotateTransform(0)
                    Case 1
                        'east so rotate 90
                        e.Graphics.RotateTransform(90)
                    Case 2
                        'south so rotate 180
                        e.Graphics.RotateTransform(180)
                    Case 3
                        'west so rotate 270
                        e.Graphics.RotateTransform(270)
                End Select
                'translate the graphics origin back to the upper left image destination before drawing
                e.Graphics.TranslateTransform((upperLeftDrawPoint.X + imageCenterOffset.X) * -1, (upperLeftDrawPoint.Y + imageCenterOffset.Y) * -1)
                'draw the image
                e.Graphics.DrawImage(en.sprite.Image, en.position.x, en.position.y)
                'reset all changes to the transformation matrix
                e.Graphics.ResetTransform()
            End If
        Next

        'draw towers
        For Each t As Tower In towers
            'the construction animation loop
            If t.operational = False Then
                'draw the base
                e.Graphics.DrawImage(t.baseSprite.Image, t.position.x * TURRET_SCALE, t.position.y * TURRET_SCALE)

                If t.buildingPhase < 2 * 8 Then
                ElseIf t.buildingPhase < 4 * 8 And t.buildingPhase >= 2 * 8 Then
                    e.Graphics.DrawImage(t.gunSprite.Image, t.position.x * TURRET_SCALE - t.weaponOffset.x, t.position.y * TURRET_SCALE - t.weaponOffset.y)
                ElseIf t.buildingPhase < 6 * 8 And t.buildingPhase >= 4 * 8 Then
                    e.Graphics.DrawImage(t.gunSprite.Image, t.position.x * TURRET_SCALE - t.weaponOffset.x, t.position.y * TURRET_SCALE - t.weaponOffset.y)
                    e.Graphics.DrawImage(t.topSprite.Image, t.position.x * TURRET_SCALE, t.position.y * TURRET_SCALE)
                ElseIf t.buildingPhase = 8 * 8 Then
                    t.operational = True
                End If
            End If

            t.buildingPhase += 1
            Dim deltax
            Dim deltay
            Dim theta
            If t.target IsNot Nothing Then
                deltax = t.target.position.x - t.position.x * TURRET_SCALE - t.gunSprite.Image.Width / 2
                deltay = t.target.position.y - t.position.y * TURRET_SCALE - t.gunSprite.Image.Height / 2
                theta = (Math.Atan2(deltay, deltax) * 180 / Math.PI) + 180
            Else
                deltax = t.position.x * TURRET_SCALE - t.gunSprite.Image.Width / 2
                deltay = t.position.y * TURRET_SCALE - t.gunSprite.Image.Height / 2
                theta = (Math.Atan2(deltay, deltax) * 180 / Math.PI) + 180
            End If

            If t.operational Then
                'draw the base
                e.Graphics.DrawImage(t.baseSprite.Image, t.position.x * TURRET_SCALE, t.position.y * TURRET_SCALE)

                '****rotation for the gun sprite****
                'decide where to draw - this code uses the upper left corner
                Dim upperLeftDrawPoint As Point = New Point(t.position.x * TURRET_SCALE - t.weaponOffset.x, t.position.y * TURRET_SCALE - t.weaponOffset.y)
                'calculate the center of the image
                Dim imageCenterOffset As Point = New Point(t.gunSprite.Image.Width / 2, t.gunSprite.Image.Height / 2)
                'translate the graphics origin to the destination image center before rotating
                e.Graphics.TranslateTransform(upperLeftDrawPoint.X + imageCenterOffset.X, upperLeftDrawPoint.Y + imageCenterOffset.Y)
                'rotate the matrix around the origin
                e.Graphics.RotateTransform(theta)
                'translate the graphics origin back to the upper left image destination before drawing
                e.Graphics.TranslateTransform((upperLeftDrawPoint.X + imageCenterOffset.X) * -1, (upperLeftDrawPoint.Y + imageCenterOffset.Y) * -1)
                'draw the image
                e.Graphics.DrawImage(t.gunSprite.Image, upperLeftDrawPoint)
                'reset all changes to the transformation matrix
                e.Graphics.ResetTransform()

                '****rotation for the top sprite****
                'decide where to draw - this code uses the upper left corner
                upperLeftDrawPoint = New Point(t.position.x * TURRET_SCALE, t.position.y * TURRET_SCALE)
                'calculate the center of the image
                imageCenterOffset = New Point(t.topSprite.Image.Width / 2, t.topSprite.Image.Height / 2)
                'translate the graphics origin to the destination image center before rotating
                e.Graphics.TranslateTransform(upperLeftDrawPoint.X + imageCenterOffset.X, upperLeftDrawPoint.Y + imageCenterOffset.Y)
                'rotate the matrix around the origin
                e.Graphics.RotateTransform(theta)
                'translate the graphics origin back to the upper left image destination before drawing
                e.Graphics.TranslateTransform((upperLeftDrawPoint.X + imageCenterOffset.X) * -1, (upperLeftDrawPoint.Y + imageCenterOffset.Y) * -1)
                'draw the image
                e.Graphics.DrawImage(t.topSprite.Image, upperLeftDrawPoint)
                'reset all changes to the transformation matrix
                e.Graphics.ResetTransform()
            End If
        Next
    End Sub

    Private Sub Tick_Tick(sender As Object, e As EventArgs) Handles Tick.Tick
        'refresh the form
        Me.Refresh()

        'update enemy positions
        updateEnemies()

        'set health and money labels
        lblHealth.Text = "<3 = " + CStr(health)
        lblMoney.Text = "$$ = " + CStr(money)

        'Debug.Print(CStr((lastWaveTime + timePerWave - totalTime) / 1000) + " more seconds until wave")
        'if more time has passed than the last time we had a wave plus the extra time to wait before another wave
        If lastWaveTime + timePerWave <= totalTime Then
            SpawnEnemies()
            lastWaveTime = totalTime
        End If

        totalTime += TIMER_TICK_TIME

        'check if the turret can fire
        For Each t As Tower In towers
            If t.ticksSinceLastFire >= t.fireTime Then
                'it has been more or equal to than the required time before this turret can shoot again
                'check if there are enemies in range
                'set target to nothing, so if there is an enemy the turret gets a target but if not it stays nothing
                t.target = Nothing
                If t.targetReverse Then
                    'target reversely
                    For en As Integer = enemies.Count - 1 To 0 Step -1
                        If enemies(en).position.Distancce(t.position * TURRET_SCALE) <= t.range Then
                            t.target = enemies(en)
                        End If
                    Next
                Else
                    'target regularly
                    For Each en As Enemy In enemies
                        If en.position.Distancce(t.position * TURRET_SCALE) <= t.range Then
                            t.target = en
                        End If
                    Next
                End If
                t.ticksSinceLastFire = 0
                If t.target IsNot Nothing Then
                    If playerLost Then
                        Return
                    End If
                    t.Shoot(t.target, Me)
                End If
            Else
                t.ticksSinceLastFire += 1
            End If
        Next

        'move projectiles
        For i As Integer = 0 To projectiles.Count - 1
            If (i < projectiles.Count) Then

                Dim p As Projectile = projectiles(i)

                'make a vector for the direction of the enemy
                Dim direction = New Vector2((p.target.position.x - p.position.x), (p.target.position.y - p.position.y))

                'normalize that vector
                If Math.Abs(direction.x) > Math.Abs(direction.y) Then
                    If (direction.x < 0) Then
                        direction = New Vector2(direction.x / (-direction.x), direction.y / (-direction.x))
                    Else
                        direction = New Vector2(direction.x / direction.x, direction.y / direction.x)
                    End If
                ElseIf Math.Abs(direction.y) > Math.Abs(direction.x) Then
                    If (direction.y < 0) Then
                        direction = New Vector2(direction.x / (-direction.y), direction.y / (-direction.y))
                    Else
                        direction = New Vector2(direction.x / direction.y, direction.y / direction.y)
                    End If
                End If

                p.position = New Vector2(p.position.x + (direction.x * p.speed), p.position.y + (direction.y * p.speed))

                If p.ticksSinceShot >= 75 Then
                    'has been three seconds since projectile shot so remove it
                    projectiles.Remove(p)
                Else
                    p.ticksSinceShot += 1
                End If

                If p.position.Distancce(p.target.position) < 10 Then
                    'projectile hit enemy so do damage the enemy and remove projectile
                    p.target.health -= p.damage
                    If p.target.health <= 0 Then
                        If p.target.dead = False Then
                            'destroy enemy
                            enemies.Remove(p.target)
                            'play sound
                            My.Computer.Audio.Play(My.Resources.Explosion, AudioPlayMode.Background)
                            'add money
                            money += p.target.moneyOnDeath
                            'set the target to dead
                            p.target.dead = True
                            Debug.Print("enemy destroyed")
                        End If
                    End If
                    projectiles.Remove(p)
                End If
            End If
        Next
    End Sub

    Private Sub updateEnemies()
        Dim node1 As Vector2 = New Vector2(7 * TURRET_SCALE, 3 * TURRET_SCALE)
        Dim node2 As Vector2 = New Vector2(7 * TURRET_SCALE, 1 * TURRET_SCALE)
        Dim node3 As Vector2 = New Vector2(12 * TURRET_SCALE, 1 * TURRET_SCALE)
        Dim node4 As Vector2 = New Vector2(12 * TURRET_SCALE, 3 * TURRET_SCALE)
        Dim node5 As Vector2 = New Vector2(16 * TURRET_SCALE, 3 * TURRET_SCALE)
        Dim node6 As Vector2 = New Vector2(16 * TURRET_SCALE, 8 * TURRET_SCALE)
        Dim node7 As Vector2 = New Vector2(13 * TURRET_SCALE, 8 * TURRET_SCALE)
        Dim node8 As Vector2 = New Vector2(13 * TURRET_SCALE, 6 * TURRET_SCALE)
        Dim node9 As Vector2 = New Vector2(8 * TURRET_SCALE, 6 * TURRET_SCALE)
        Dim node10 As Vector2 = New Vector2(8 * TURRET_SCALE, 9 * TURRET_SCALE)
        Dim node11 As Vector2 = New Vector2(-20, 9 * TURRET_SCALE)

        For i As Integer = 0 To enemies.Count - 1
            Dim en As Enemy
            If i < enemies.Count Then
                en = enemies(i)
                Select Case en.nodeNum
                    Case 0
                        en.position = New Vector2(en.position.x + en.speed, en.position.y)
                        If en.position.Distancce(node1) <= en.speed Then
                            en.nodeNum += 1
                            en.direction = 0
                        End If
                    Case 1
                        en.position = New Vector2(en.position.x, en.position.y - en.speed)
                        If en.position.Distancce(node2) <= en.speed Then
                            en.nodeNum += 1
                            en.direction = 1
                        End If
                    Case 2
                        en.position = New Vector2(en.position.x + en.speed, en.position.y)
                        If en.position.Distancce(node3) <= en.speed Then
                            en.nodeNum += 1
                            en.direction = 2
                        End If
                    Case 3
                        en.position = New Vector2(en.position.x, en.position.y + en.speed)
                        If en.position.Distancce(node4) <= en.speed Then
                            en.nodeNum += 1
                            en.direction = 1
                        End If
                    Case 4
                        en.position = New Vector2(en.position.x + en.speed, en.position.y)
                        If en.position.Distancce(node5) <= en.speed Then
                            en.nodeNum += 1
                            en.direction = 2
                        End If
                    Case 5
                        en.position = New Vector2(en.position.x, en.position.y + en.speed)
                        If en.position.Distancce(node6) <= en.speed Then
                            en.nodeNum += 1
                            en.direction = 3
                        End If
                    Case 6
                        en.position = New Vector2(en.position.x - en.speed, en.position.y)
                        If en.position.Distancce(node7) <= en.speed Then
                            en.nodeNum += 1
                            en.direction = 0
                        End If
                    Case 7
                        en.position = New Vector2(en.position.x, en.position.y - en.speed)
                        If en.position.Distancce(node8) <= en.speed Then
                            en.nodeNum += 1
                            en.direction = 3
                        End If
                    Case 8
                        en.position = New Vector2(en.position.x - en.speed, en.position.y)
                        If en.position.Distancce(node9) <= en.speed Then
                            en.nodeNum += 1
                            en.direction = 2
                        End If
                    Case 9
                        en.position = New Vector2(en.position.x, en.position.y + en.speed)
                        If en.position.Distancce(node10) <= en.speed Then
                            en.nodeNum += 1
                            en.direction = 3
                        End If
                    Case 10
                        en.position = New Vector2(en.position.x - en.speed, en.position.y)
                        If en.position.Distancce(node11) <= en.speed Then
                            'remove enemy
                            enemies.Remove(en)
                            'damage player
                            health -= en.damage
                            'play player hurt sound
                            My.Computer.Audio.Play(My.Resources.Explosion, AudioPlayMode.Background)
                        End If
                End Select
            End If
            If health <= 0 And playerLost = False Then
                'we are dead. we loose.
                'play the loose sound
                My.Computer.Audio.Play(My.Resources.Loose, AudioPlayMode.Background)
                'set the last wave time to a very high number to stop more waves
                lastWaveTime = 1000000000
                playerLost = True
                Dim response = MsgBox("You lasted for " + CStr(wave) + " waves!", MsgBoxStyle.OkOnly, "GAME OVER")
                If response = vbOK Then
                    MenuForm.Show()
                    Me.Close()
                End If
            End If
        Next
    End Sub

    Private Sub RadTurC_CheckedChanged(sender As Object, e As EventArgs) Handles radTurC.CheckedChanged
        twrChoice = twrCannon
    End Sub

    Private Sub RadTurMach_CheckedChanged(sender As Object, e As EventArgs) Handles radTurMach.CheckedChanged
        twrChoice = twrMachineGun
    End Sub

    Private Sub RadTurRail_CheckedChanged(sender As Object, e As EventArgs) Handles radTurRail.CheckedChanged
        twrChoice = twrRailgun
    End Sub

    Private Sub RadUpgradeSp_CheckedChanged(sender As Object, e As EventArgs) Handles radUpgradeSp.CheckedChanged
        upgrading = radUpgradeSp.Checked
        uppingSpeed = radUpgradeSp.Checked
    End Sub
    Private Sub RadUpgradeDmg_CheckedChanged(sender As Object, e As EventArgs) Handles radUpgradeDmg.CheckedChanged
        upgrading = radUpgradeDmg.Checked
        uppingSpeed = radUpgradeSp.Checked
    End Sub
End Class

Public Class Vector2
    'a class containing a position
    Public x As Decimal
    Public y As Decimal

    Public Sub New(ByVal x As Decimal,
                    ByVal y As Decimal)
        Me.x = x
        Me.y = y
    End Sub

    'return the distancce between two vector2s
    Public Function Distancce(otherVector As Vector2) As Decimal
        Dim a As Vector2 = Me
        Dim b As Vector2 = otherVector

        Return Math.Sqrt((b.x - a.x) ^ 2 + (b.y - a.y) ^ 2)
    End Function

    Public Shared Operator *(ByVal position As Vector2,
                             ByVal multiplier As Integer)
        Return New Vector2(position.x * multiplier, position.y * multiplier)
    End Operator

    Public Shared Operator =(ByVal positiona As Vector2,
                              ByVal positionb As Vector2)
        If positiona.x = positionb.x And positiona.y = positionb.y Then
            Return True
        Else
            Return False
        End If
    End Operator

    Public Shared Operator <>(ByVal positiona As Vector2,
                          ByVal positionb As Vector2)
        If positiona.x = positionb.x And positiona.y = positionb.y Then
            Return False
        Else
            Return True
        End If
    End Operator
End Class

Public Class Tower
    'a class for a tower/turret
    'turret sprite names
    Public rotationSpriteName As String
    Public baseSpriteName As String
    Public topSpriteName As String

    Public projectileType As Projectile    'the turret's projectile

    Public rotation As Integer  '0 to 15 for all the possible turret positions
    Public range As Decimal     'the turret's range
    Public damage As Integer    'the turret's damage
    Public cost As Integer      'the turret's cost
    Public position As Vector2  'the turret's position
    Public fireTime As Integer  'the turret's time in ticks to fires
    Public ticksSinceLastFire  'the frames since the last fire
    Public level As Integer = 1

    Public weaponOffset As Vector2  'the weapon's offset (for graphical purposes)

    Public baseSprite As PictureBox 'the turret's base's actual sprite
    Public topSprite As PictureBox  'the turret's top's actual sprite
    Public gunSprite As PictureBox  'the turret's gun's actual sprite

    Public operational As Boolean = False   'turrets can be stunned and take a few frames to construct
    Public buildingPhase As Integer = 0     'every tick this goes up. every two it adds a part until it is completed

    Public target As Enemy          'the turret's target
    Public targetReverse As Boolean 'if the turret targets in the normal way, or front to back. used to force railgun to target front enemy and not back

    Public shootSound As String 'the name of the turret's sound

    Private Const TURRET_SCALE As Integer = GameForm.TURRET_SCALE

    'for declaring a new turret
    Public Sub New(ByVal range As Decimal,
                ByVal damage As Integer,
                ByVal cost As Integer,
                ByVal fireTime As Integer,
                ByVal rotationSpriteName As String,
                ByVal baseSpriteName As String,
                ByVal topSpriteName As String,
                ByVal weaponOffset As Vector2,
                ByVal projectileType As Projectile,
                ByVal sound As String,
                ByVal targetReverse As Boolean)
        Me.range = range
        Me.damage = damage
        Me.cost = cost
        Me.fireTime = fireTime
        Me.rotationSpriteName = rotationSpriteName
        Me.baseSpriteName = baseSpriteName
        Me.topSpriteName = topSpriteName
        Me.weaponOffset = weaponOffset
        Me.projectileType = projectileType
        shootSound = sound
        Me.targetReverse = targetReverse
    End Sub

    'for setting up the pictureboxes
    Public Sub Initialize(ByVal f As Form)
        Debug.Print("Initializing...")

        'set up the baseSprite
        baseSprite.Width = TURRET_SCALE
        baseSprite.Height = TURRET_SCALE
        baseSprite.Top = position.y * TURRET_SCALE
        baseSprite.Left = position.x * TURRET_SCALE
        baseSprite.BackColor = Color.Transparent
        baseSprite.Image = My.Resources.ResourceManager.GetObject(baseSpriteName + CStr(level - 1))

        'set up the gunSprite
        gunSprite.Parent = baseSprite
        gunSprite.Width = TURRET_SCALE
        gunSprite.Height = TURRET_SCALE
        gunSprite.Top = position.y * TURRET_SCALE
        gunSprite.Left = position.x * TURRET_SCALE
        gunSprite.BackColor = Color.Transparent
        gunSprite.Image = My.Resources.ResourceManager.GetObject(rotationSpriteName + "0")

        'set up the topSprite
        topSprite.Parent = gunSprite
        topSprite.Width = TURRET_SCALE
        topSprite.Height = TURRET_SCALE
        topSprite.Top = position.y * TURRET_SCALE
        topSprite.Left = position.x * TURRET_SCALE
        topSprite.BackColor = Color.Transparent
        topSprite.Image = My.Resources.ResourceManager.GetObject(topSpriteName + CStr(level - 1))
    End Sub

    Private Function CopyProjectile(p As Projectile, target As Enemy, startPosition As Vector2) As Projectile
        Return New Projectile(p.speed, p.damage, p.spriteName, target, startPosition, p.spriteOffset)
    End Function

    Public Sub Shoot(t As Enemy, f As GameForm)
        'make a copy of the projectile, except add in the target and the turret's position
        f.projectiles.Add(CopyProjectile(projectileType, t, New Vector2(position.x * TURRET_SCALE, position.y * TURRET_SCALE)))
        'play shot sound
        My.Computer.Audio.Play(My.Resources.ResourceManager.GetObject(shootSound), AudioPlayMode.Background)
    End Sub

    Public Sub LevelUp(ByVal f As Form, ByVal upgradingSpeed As Boolean)
        If level >= 3 Then
            Return
        End If
        level += 1
        operational = False
        buildingPhase = 0
        Initialize(f)
        If upgradingSpeed Then
            'double the fire rate by halving the time per shot
            fireTime /= 2
        Else
            'double the damage
            damage *= 2
        End If
    End Sub
End Class

Public Class Projectile
    'class for a projectile
    Public speed As Integer     'the enemy's speed
    Public damage As Integer    'the projectile's damage
    Public target As Enemy      'the enemy the projectile is targeting
    Public spriteName As String 'the projectile's sprite
    Public position As Vector2  'the projectile's position
    Public sprite As PictureBox 'the projectile's actual sprite
    Public ticksSinceShot As Integer    'the projectile's time since it was shot
    Public spriteOffset As Vector2      'the sprite's offset for graphics
    Public rotation As Integer = -1     'the projectiles rotation, calculated once for efficiency, set to -1 to start because that is impossible

    'for making an active projectile with a target and a position
    Public Sub New(ByVal speed As Integer,
                   ByVal damage As Integer,
                   ByVal spriteName As String,
                   ByVal target As Enemy,
                   ByVal startPosition As Vector2,
                   ByVal offset As Vector2)
        Me.speed = speed
        Me.damage = damage
        Me.spriteName = spriteName
        Me.target = target
        Me.position = startPosition
        Me.spriteOffset = offset

        'set up the sprite regardless
        sprite = New PictureBox
        sprite.Image = My.Resources.ResourceManager.GetObject(spriteName)

        Initialize()
    End Sub

    'for making a template projectile with no position or target
    Public Sub New(ByVal speed As Integer,
                   ByVal damage As Integer,
                   ByVal spriteName As String,
                   ByVal offset As Vector2)
        Me.speed = speed
        Me.damage = damage
        Me.spriteName = spriteName
        Me.spriteOffset = offset

        'set up the sprite regardless
        sprite = New PictureBox
        sprite.Image = My.Resources.ResourceManager.GetObject(spriteName)
    End Sub

    Private Sub Initialize()
        'set up the sprites
        sprite.Width = 32
        sprite.Height = 32
        sprite.Top = position.x
        sprite.Left = position.y
    End Sub

End Class

Public Class Enemy
    'a class for an enemy
    Public speed As Integer     'the enemy's speed
    Public damage As Integer    'the enemy's damage
    Public nodeNum As Integer = 0   'the node of the path the enemy is on
    Public spriteName As String 'the enemy's sprite
    Public position As Vector2  'the enemy's position
    Public sprite As PictureBox 'the enemy's actual sprite
    Public health As Integer    'the enemy's health
    Public dead As Boolean      'if the enemy has been killed
    Public moneyOnDeath As Integer  'the amount of enemy the player gets on enemy death
    Public direction As Integer = 1 'the direction of the enemy. 0:north, 1:east, 2:south, 3:west

    Public Sub New(ByVal speed As Integer,
                   ByVal damage As Integer,
                   ByVal health As Integer,
                   ByVal moneyOnDeath As Integer,
                   ByVal spriteName As String,
                   ByVal startPosition As Vector2)
        Me.speed = speed
        Me.damage = damage
        Me.health = health
        Me.spriteName = spriteName
        Me.moneyOnDeath = moneyOnDeath
        position = startPosition

        'set up the sprite
        sprite = New PictureBox
        sprite.Image = My.Resources.ResourceManager.GetObject(spriteName)
    End Sub

    Public Sub Initialize()
        'set up the sprite
        sprite.Width = 32
        sprite.Height = 32
        sprite.Top = position.x
        sprite.Left = position.y
    End Sub
End Class

'Test cases:
'-Turrets can target enemies In their range             |   Turrets can target enemies within their range
'-Turrets can only target enemies within their range    |   Turrets can only actively target enemeis in their range
'-Enemies can be targeted And shot                      |   Enemies can be targeted by turrets and shot, causing them to loose health
'-When enemies die the player receives score            |   Score feature replaced with wave number display. Enemies instead give player money on death
'-Turrets can rotate properly                           |   Turrets rotate to target their current enemy
'-You can only place one turret per tile                |   Tile is checked for turrets to prevent placement on other turrets
'-Enemies follow a predetermined path                   |   Enemies follow the predetermined path consistently
'-Enemies can damage the player's base                  |   Enemies damage the player based on enemy type when they complete the path
'-The player can save a high score                      |   Highscore feature canceled
'-High score shows On load                              |   Highscore feature canceled
'-Enemy spawn rate increases                            |   Enemy spawn rate increases exponentially
'-Different enemy types spawn                           |   Harder variations of the tank enemy spawn
'-Form loading and closing works                        |   On game form close the menu closes, and on menu close the game closes
'-Cursor highlight locks to tile                        |   Cursor highlight locks to tile
'-Game loss works                                       |   A game over menu pops up and enemies stop spawning on game over
'-Turrets can be upgraded                               |   Turrets can be upgraded twice in two different ways, and they change sprites, with a max level of three
'-Money system works                                    |   Upgrades and turrets cost money, and can not be built if the player would go below 0$
'-Sound effects work                                    |   Enemies make sound on death, turrets make sound on shoot, buttons make sound, building makes sound