<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Eingabe = New System.Windows.Forms.TextBox()
        Me.ConnectEingabe = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(175, 219)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(85, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Suchen"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.Location = New System.Drawing.Point(12, 12)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(248, 187)
        Me.ListView1.TabIndex = 1
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 203)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Label1"
        '
        'Eingabe
        '
        Me.Eingabe.ForeColor = System.Drawing.SystemColors.ScrollBar
        Me.Eingabe.Location = New System.Drawing.Point(12, 219)
        Me.Eingabe.Name = "Eingabe"
        Me.Eingabe.Size = New System.Drawing.Size(157, 20)
        Me.Eingabe.TabIndex = 3
        Me.Eingabe.Text = "Suchbegriff"
        '
        'ConnectEingabe
        '
        Me.ConnectEingabe.ForeColor = System.Drawing.SystemColors.ScrollBar
        Me.ConnectEingabe.Location = New System.Drawing.Point(15, 245)
        Me.ConnectEingabe.Name = "ConnectEingabe"
        Me.ConnectEingabe.Size = New System.Drawing.Size(154, 20)
        Me.ConnectEingabe.TabIndex = 4
        Me.ConnectEingabe.Text = "IP"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(269, 275)
        Me.Controls.Add(Me.ConnectEingabe)
        Me.Controls.Add(Me.Eingabe)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents ListView1 As ListView
    Friend WithEvents Label1 As Label
    Friend WithEvents Eingabe As TextBox
    Friend WithEvents ConnectEingabe As TextBox
End Class
