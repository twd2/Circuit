<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.新建ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.打开ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.保存ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.生成图片ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.删除ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnAutoNo = New System.Windows.Forms.Button()
        Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MainBoxPoint = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.OriPoint = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.labFPS = New System.Windows.Forms.ToolStripStatusLabel()
        Me.btnWire = New System.Windows.Forms.Button()
        Me.PictureBox9 = New System.Windows.Forms.PictureBox()
        Me.picLight = New System.Windows.Forms.PictureBox()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.picMain = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.picVoltmeter = New System.Windows.Forms.PictureBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.picMotor = New System.Windows.Forms.PictureBox()
        Me.picAmmeter = New System.Windows.Forms.PictureBox()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.picNotGate = New System.Windows.Forms.PictureBox()
        Me.picHub3p = New System.Windows.Forms.PictureBox()
        Me.MenuStrip1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picLight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picVoltmeter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picMotor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picAmmeter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picNotGate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picHub3p, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.新建ToolStripMenuItem, Me.打开ToolStripMenuItem, Me.保存ToolStripMenuItem, Me.生成图片ToolStripMenuItem, Me.PrintToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(8, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(844, 28)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "菜单"
        '
        '新建ToolStripMenuItem
        '
        Me.新建ToolStripMenuItem.Name = "新建ToolStripMenuItem"
        Me.新建ToolStripMenuItem.Size = New System.Drawing.Size(51, 24)
        Me.新建ToolStripMenuItem.Text = "新建"
        '
        '打开ToolStripMenuItem
        '
        Me.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem"
        Me.打开ToolStripMenuItem.Size = New System.Drawing.Size(51, 24)
        Me.打开ToolStripMenuItem.Text = "打开"
        '
        '保存ToolStripMenuItem
        '
        Me.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem"
        Me.保存ToolStripMenuItem.Size = New System.Drawing.Size(51, 24)
        Me.保存ToolStripMenuItem.Text = "保存"
        '
        '生成图片ToolStripMenuItem
        '
        Me.生成图片ToolStripMenuItem.Name = "生成图片ToolStripMenuItem"
        Me.生成图片ToolStripMenuItem.Size = New System.Drawing.Size(81, 24)
        Me.生成图片ToolStripMenuItem.Text = "生成图片"
        '
        'PrintToolStripMenuItem
        '
        Me.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
        Me.PrintToolStripMenuItem.Size = New System.Drawing.Size(51, 24)
        Me.PrintToolStripMenuItem.Text = "打印"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.删除ToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(109, 28)
        '
        '删除ToolStripMenuItem
        '
        Me.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem"
        Me.删除ToolStripMenuItem.Size = New System.Drawing.Size(108, 24)
        Me.删除ToolStripMenuItem.Text = "删除"
        '
        'btnAutoNo
        '
        Me.btnAutoNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAutoNo.Location = New System.Drawing.Point(600, 538)
        Me.btnAutoNo.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAutoNo.Name = "btnAutoNo"
        Me.btnAutoNo.Size = New System.Drawing.Size(228, 29)
        Me.btnAutoNo.TabIndex = 10
        Me.btnAutoNo.Text = "自动编号"
        Me.btnAutoNo.UseVisualStyleBackColor = True
        '
        'PropertyGrid1
        '
        Me.PropertyGrid1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PropertyGrid1.Location = New System.Drawing.Point(600, 35)
        Me.PropertyGrid1.Margin = New System.Windows.Forms.Padding(4)
        Me.PropertyGrid1.Name = "PropertyGrid1"
        Me.PropertyGrid1.Size = New System.Drawing.Size(228, 495)
        Me.PropertyGrid1.TabIndex = 11
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(600, 575)
        Me.btnReset.Margin = New System.Windows.Forms.Padding(4)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(228, 29)
        Me.btnReset.TabIndex = 12
        Me.btnReset.Text = "复位原点"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.MainBoxPoint, Me.ToolStripStatusLabel2, Me.OriPoint, Me.ToolStripStatusLabel3, Me.labFPS})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 687)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 19, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(844, 25)
        Me.StatusStrip1.TabIndex = 13
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(129, 20)
        Me.ToolStripStatusLabel1.Text = "相对于操作框坐标"
        '
        'MainBoxPoint
        '
        Me.MainBoxPoint.Name = "MainBoxPoint"
        Me.MainBoxPoint.Size = New System.Drawing.Size(45, 20)
        Me.MainBoxPoint.Text = "(0, 0)"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(114, 20)
        Me.ToolStripStatusLabel2.Text = "相对于原点坐标"
        '
        'OriPoint
        '
        Me.OriPoint.Name = "OriPoint"
        Me.OriPoint.Size = New System.Drawing.Size(45, 20)
        Me.OriPoint.Text = "(0, 0)"
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(35, 20)
        Me.ToolStripStatusLabel3.Text = "FPS"
        '
        'labFPS
        '
        Me.labFPS.Name = "labFPS"
        Me.labFPS.Size = New System.Drawing.Size(15, 20)
        Me.labFPS.Text = "-"
        '
        'btnWire
        '
        Me.btnWire.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnWire.Location = New System.Drawing.Point(600, 612)
        Me.btnWire.Margin = New System.Windows.Forms.Padding(4)
        Me.btnWire.Name = "btnWire"
        Me.btnWire.Size = New System.Drawing.Size(228, 29)
        Me.btnWire.TabIndex = 14
        Me.btnWire.Text = "导线"
        Me.btnWire.UseVisualStyleBackColor = True
        '
        'PictureBox9
        '
        Me.PictureBox9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PictureBox9.Image = CType(resources.GetObject("PictureBox9.Image"), System.Drawing.Image)
        Me.PictureBox9.Location = New System.Drawing.Point(256, 596)
        Me.PictureBox9.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox9.Name = "PictureBox9"
        Me.PictureBox9.Size = New System.Drawing.Size(49, 49)
        Me.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox9.TabIndex = 17
        Me.PictureBox9.TabStop = False
        '
        'picLight
        '
        Me.picLight.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.picLight.Location = New System.Drawing.Point(172, 538)
        Me.picLight.Margin = New System.Windows.Forms.Padding(4)
        Me.picLight.Name = "picLight"
        Me.picLight.Size = New System.Drawing.Size(70, 50)
        Me.picLight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picLight.TabIndex = 16
        Me.picLight.TabStop = False
        '
        'PictureBox7
        '
        Me.PictureBox7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PictureBox7.Image = CType(resources.GetObject("PictureBox7.Image"), System.Drawing.Image)
        Me.PictureBox7.Location = New System.Drawing.Point(199, 596)
        Me.PictureBox7.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(49, 49)
        Me.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox7.TabIndex = 15
        Me.PictureBox7.TabStop = False
        '
        'picMain
        '
        Me.picMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picMain.ContextMenuStrip = Me.ContextMenuStrip1
        Me.picMain.Location = New System.Drawing.Point(16, 35)
        Me.picMain.Margin = New System.Windows.Forms.Padding(4)
        Me.picMain.Name = "picMain"
        Me.picMain.Size = New System.Drawing.Size(576, 495)
        Me.picMain.TabIndex = 0
        Me.picMain.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(16, 596)
        Me.PictureBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(49, 49)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox3.TabIndex = 5
        Me.PictureBox3.TabStop = False
        '
        'PictureBox6
        '
        Me.PictureBox6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PictureBox6.Image = CType(resources.GetObject("PictureBox6.Image"), System.Drawing.Image)
        Me.PictureBox6.Location = New System.Drawing.Point(130, 596)
        Me.PictureBox6.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(61, 49)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox6.TabIndex = 8
        Me.PictureBox6.TabStop = False
        '
        'picVoltmeter
        '
        Me.picVoltmeter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.picVoltmeter.Location = New System.Drawing.Point(94, 538)
        Me.picVoltmeter.Margin = New System.Windows.Forms.Padding(4)
        Me.picVoltmeter.Name = "picVoltmeter"
        Me.picVoltmeter.Size = New System.Drawing.Size(70, 50)
        Me.picVoltmeter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picVoltmeter.TabIndex = 7
        Me.picVoltmeter.TabStop = False
        '
        'PictureBox4
        '
        Me.PictureBox4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PictureBox4.Image = CType(resources.GetObject("PictureBox4.Image"), System.Drawing.Image)
        Me.PictureBox4.Location = New System.Drawing.Point(73, 596)
        Me.PictureBox4.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(49, 49)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox4.TabIndex = 6
        Me.PictureBox4.TabStop = False
        '
        'picMotor
        '
        Me.picMotor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.picMotor.Location = New System.Drawing.Point(250, 538)
        Me.picMotor.Margin = New System.Windows.Forms.Padding(4)
        Me.picMotor.Name = "picMotor"
        Me.picMotor.Size = New System.Drawing.Size(70, 50)
        Me.picMotor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picMotor.TabIndex = 4
        Me.picMotor.TabStop = False
        '
        'picAmmeter
        '
        Me.picAmmeter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.picAmmeter.Location = New System.Drawing.Point(16, 538)
        Me.picAmmeter.Margin = New System.Windows.Forms.Padding(4)
        Me.picAmmeter.Name = "picAmmeter"
        Me.picAmmeter.Size = New System.Drawing.Size(70, 50)
        Me.picAmmeter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picAmmeter.TabIndex = 2
        Me.picAmmeter.TabStop = False
        '
        'btnStart
        '
        Me.btnStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnStart.Location = New System.Drawing.Point(600, 648)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(108, 29)
        Me.btnStart.TabIndex = 19
        Me.btnStart.Text = "开始"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        Me.btnStop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnStop.Location = New System.Drawing.Point(720, 648)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(108, 29)
        Me.btnStop.TabIndex = 20
        Me.btnStop.Text = "停止"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'picNotGate
        '
        Me.picNotGate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.picNotGate.Location = New System.Drawing.Point(328, 538)
        Me.picNotGate.Margin = New System.Windows.Forms.Padding(4)
        Me.picNotGate.Name = "picNotGate"
        Me.picNotGate.Size = New System.Drawing.Size(70, 50)
        Me.picNotGate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picNotGate.TabIndex = 21
        Me.picNotGate.TabStop = False
        '
        'picHub3p
        '
        Me.picHub3p.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.picHub3p.Location = New System.Drawing.Point(406, 538)
        Me.picHub3p.Margin = New System.Windows.Forms.Padding(4)
        Me.picHub3p.Name = "picHub3p"
        Me.picHub3p.Size = New System.Drawing.Size(70, 50)
        Me.picHub3p.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picHub3p.TabIndex = 22
        Me.picHub3p.TabStop = False
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(844, 712)
        Me.Controls.Add(Me.picHub3p)
        Me.Controls.Add(Me.picNotGate)
        Me.Controls.Add(Me.btnStop)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.PictureBox9)
        Me.Controls.Add(Me.picLight)
        Me.Controls.Add(Me.PictureBox7)
        Me.Controls.Add(Me.btnWire)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.PropertyGrid1)
        Me.Controls.Add(Me.picMain)
        Me.Controls.Add(Me.btnReset)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.PictureBox6)
        Me.Controls.Add(Me.btnAutoNo)
        Me.Controls.Add(Me.picVoltmeter)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.picMotor)
        Me.Controls.Add(Me.picAmmeter)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmMain"
        Me.Text = "Circuit 0.1 Alpha"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picLight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picVoltmeter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picMotor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picAmmeter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picNotGate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picHub3p, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents picMain As System.Windows.Forms.PictureBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents 新建ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 保存ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 生成图片ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents picAmmeter As System.Windows.Forms.PictureBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents 删除ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents picMotor As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents picVoltmeter As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox6 As System.Windows.Forms.PictureBox
    Friend WithEvents btnAutoNo As System.Windows.Forms.Button
    Friend WithEvents PropertyGrid1 As System.Windows.Forms.PropertyGrid
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents MainBoxPoint As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents OriPoint As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents btnWire As System.Windows.Forms.Button
    Friend WithEvents PictureBox7 As System.Windows.Forms.PictureBox
    Friend WithEvents picLight As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox9 As System.Windows.Forms.PictureBox
    Friend WithEvents 打开ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents ToolStripStatusLabel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents labFPS As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents picNotGate As System.Windows.Forms.PictureBox
    Friend WithEvents picHub3p As System.Windows.Forms.PictureBox

End Class
