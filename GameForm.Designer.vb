<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class GameForm
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GameForm))
        Me.Tick = New System.Windows.Forms.Timer(Me.components)
        Me.picHighlight = New System.Windows.Forms.PictureBox()
        Me.radTurC = New System.Windows.Forms.RadioButton()
        Me.radTurMach = New System.Windows.Forms.RadioButton()
        Me.radTurRail = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblHealth = New System.Windows.Forms.Label()
        Me.lblMoney = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblWave = New System.Windows.Forms.Label()
        Me.radUpgradeSp = New System.Windows.Forms.RadioButton()
        Me.radUpgradeDmg = New System.Windows.Forms.RadioButton()
        CType(Me.picHighlight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Tick
        '
        Me.Tick.Interval = 40
        '
        'picHighlight
        '
        Me.picHighlight.BackColor = System.Drawing.Color.Transparent
        Me.picHighlight.BackgroundImage = Global.Tower_Defense_Game.My.Resources.Resources.HighlightBoxV3
        Me.picHighlight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picHighlight.Location = New System.Drawing.Point(1166, 416)
        Me.picHighlight.Name = "picHighlight"
        Me.picHighlight.Size = New System.Drawing.Size(42, 42)
        Me.picHighlight.TabIndex = 2
        Me.picHighlight.TabStop = False
        '
        'radTurC
        '
        Me.radTurC.BackColor = System.Drawing.Color.Transparent
        Me.radTurC.BackgroundImage = Global.Tower_Defense_Game.My.Resources.Resources.TurretThumbnails_1
        Me.radTurC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.radTurC.Location = New System.Drawing.Point(1017, 90)
        Me.radTurC.Name = "radTurC"
        Me.radTurC.Size = New System.Drawing.Size(111, 45)
        Me.radTurC.TabIndex = 1
        Me.radTurC.TabStop = True
        Me.radTurC.UseVisualStyleBackColor = False
        '
        'radTurMach
        '
        Me.radTurMach.BackColor = System.Drawing.Color.Transparent
        Me.radTurMach.BackgroundImage = Global.Tower_Defense_Game.My.Resources.Resources.TurretThumbnails_2
        Me.radTurMach.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.radTurMach.ForeColor = System.Drawing.SystemColors.ControlText
        Me.radTurMach.Location = New System.Drawing.Point(1017, 125)
        Me.radTurMach.Name = "radTurMach"
        Me.radTurMach.Size = New System.Drawing.Size(111, 45)
        Me.radTurMach.TabIndex = 2
        Me.radTurMach.TabStop = True
        Me.radTurMach.UseVisualStyleBackColor = False
        '
        'radTurRail
        '
        Me.radTurRail.BackColor = System.Drawing.Color.Transparent
        Me.radTurRail.BackgroundImage = Global.Tower_Defense_Game.My.Resources.Resources.TurretThumbnails_0
        Me.radTurRail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.radTurRail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.radTurRail.Location = New System.Drawing.Point(1017, 159)
        Me.radTurRail.Name = "radTurRail"
        Me.radTurRail.Size = New System.Drawing.Size(111, 45)
        Me.radTurRail.TabIndex = 3
        Me.radTurRail.TabStop = True
        Me.radTurRail.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label4.Location = New System.Drawing.Point(1014, 70)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 20)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Towers"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label5.Location = New System.Drawing.Point(1014, 255)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(36, 20)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Info"
        '
        'lblHealth
        '
        Me.lblHealth.AutoSize = True
        Me.lblHealth.BackColor = System.Drawing.Color.Transparent
        Me.lblHealth.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblHealth.Location = New System.Drawing.Point(1014, 287)
        Me.lblHealth.Name = "lblHealth"
        Me.lblHealth.Size = New System.Drawing.Size(68, 17)
        Me.lblHealth.TabIndex = 8
        Me.lblHealth.Text = "<3 = 100 "
        '
        'lblMoney
        '
        Me.lblMoney.AutoSize = True
        Me.lblMoney.BackColor = System.Drawing.Color.Transparent
        Me.lblMoney.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblMoney.Location = New System.Drawing.Point(1014, 314)
        Me.lblMoney.Name = "lblMoney"
        Me.lblMoney.Size = New System.Drawing.Size(64, 17)
        Me.lblMoney.TabIndex = 8
        Me.lblMoney.Text = "$$ = 100"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label6.Location = New System.Drawing.Point(1100, 102)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 17)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "$150"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label7.Location = New System.Drawing.Point(1100, 138)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(40, 17)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "$100"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Label8.Location = New System.Drawing.Point(1100, 173)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(40, 17)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "$250"
        '
        'lblWave
        '
        Me.lblWave.AutoSize = True
        Me.lblWave.BackColor = System.Drawing.Color.Transparent
        Me.lblWave.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWave.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblWave.Location = New System.Drawing.Point(1012, 9)
        Me.lblWave.Name = "lblWave"
        Me.lblWave.Size = New System.Drawing.Size(92, 29)
        Me.lblWave.TabIndex = 10
        Me.lblWave.Text = "Wave 1"
        '
        'radUpgradeSp
        '
        Me.radUpgradeSp.BackColor = System.Drawing.Color.Transparent
        Me.radUpgradeSp.BackgroundImage = Global.Tower_Defense_Game.My.Resources.Resources.UpgradeArrowYellow
        Me.radUpgradeSp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.radUpgradeSp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.radUpgradeSp.Location = New System.Drawing.Point(1017, 201)
        Me.radUpgradeSp.Name = "radUpgradeSp"
        Me.radUpgradeSp.Size = New System.Drawing.Size(51, 34)
        Me.radUpgradeSp.TabIndex = 4
        Me.radUpgradeSp.TabStop = True
        Me.radUpgradeSp.UseVisualStyleBackColor = False
        '
        'radUpgradeDmg
        '
        Me.radUpgradeDmg.BackColor = System.Drawing.Color.Transparent
        Me.radUpgradeDmg.BackgroundImage = Global.Tower_Defense_Game.My.Resources.Resources.UpgradeArrowRed
        Me.radUpgradeDmg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.radUpgradeDmg.ForeColor = System.Drawing.SystemColors.ControlText
        Me.radUpgradeDmg.Location = New System.Drawing.Point(1077, 201)
        Me.radUpgradeDmg.Name = "radUpgradeDmg"
        Me.radUpgradeDmg.Size = New System.Drawing.Size(51, 34)
        Me.radUpgradeDmg.TabIndex = 5
        Me.radUpgradeDmg.TabStop = True
        Me.radUpgradeDmg.UseVisualStyleBackColor = False
        '
        'GameForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImage = Global.Tower_Defense_Game.My.Resources.Resources.Pixel_Tower_Defense_Map_Scaled_Medium_Smal_extra_Part
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ClientSize = New System.Drawing.Size(1206, 517)
        Me.Controls.Add(Me.lblWave)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lblMoney)
        Me.Controls.Add(Me.lblHealth)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.radUpgradeDmg)
        Me.Controls.Add(Me.radUpgradeSp)
        Me.Controls.Add(Me.radTurRail)
        Me.Controls.Add(Me.radTurMach)
        Me.Controls.Add(Me.radTurC)
        Me.Controls.Add(Me.picHighlight)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "GameForm"
        Me.Text = "Tapix Tower Defense - Main Game!"
        Me.TransparencyKey = System.Drawing.Color.Maroon
        CType(Me.picHighlight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Tick As Timer
    Friend WithEvents picHighlight As PictureBox
    Friend WithEvents radTurC As RadioButton
    Friend WithEvents radTurMach As RadioButton
    Friend WithEvents radTurRail As RadioButton
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents lblHealth As Label
    Friend WithEvents lblMoney As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents lblWave As Label
    Friend WithEvents radUpgradeSp As RadioButton
    Friend WithEvents radUpgradeDmg As RadioButton
End Class
