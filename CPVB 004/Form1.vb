Public Class Form1
    ' Ziel ist die phonetische Dupletten-Suche in SQL
    ' Start mit vorgefertigtem Suchen nach klarem Ergebnis
    ' Vorgesetzt für Anfang
    Public line As String
    Public input As String
    Public array As String() =
        {"Januar", "Februar", "März", "April", "Mai", "Juni", "Juli", "August", "September", "Oktober", "November", "Dezember"}
    Sub Main()
        input = Eingabe.Text
        Dim Zahl As Int16 = 0
        Dim Sicher As Int16 = 0
        If input = String.Empty Then
            WriteError()
        Else
            Dim result As String = Umwandeln(input)
            Zahl = 0
            Sicher = 0
            WriteResult(input, result)
            For Each item As String In array
                Dim test As String = Umwandeln(item)
                Zahl += 1
                If test = result Then
                    WriteResult(item, "")
                    Sicher += 1
                Else
                    If Sicher = 0 And Zahl = array.Length Then
                        WriteResult("", "")
                    End If
                End If
            Next
        End If
    End Sub
    Sub WriteError()
        ListView1.Items.Add("Ein Fehler ist aufgetreten!")
        Eingabe.Text = ""
    End Sub
    Sub WriteResult(args, args2)
        ' Such-Ergebnis
        If args Is String.Empty Then
            ListView1.Items.Add("Leider keine Ergebnisse :( " & vbNewLine)
        Else
            ListView1.Items.Add(vbNewLine & "Ergebnis: " & args & "." & vbNewLine)
            If args2 IsNot String.Empty Then
                ListView1.Items.Add(vbNewLine & "Code: " & args2 & vbNewLine)
            End If
        End If
    End Sub
    Public Function Umwandeln(ByVal Word As String) As String
        Return SoundEx(Word, 4)
    End Function
    Public Function SoundEx(ByVal Word As String, ByVal Length As Integer) As String
        Dim Value As String = ""
        Dim Size As Integer = Word.Length
        If (Size > 1) Then
            Word = Word.ToUpper()
            Dim Chars() As Char = Word.ToCharArray()
            Dim Buffer As New System.Text.StringBuilder
            Buffer.Length = 0
            Dim PrevCode As Integer = 0
            Dim CurrCode As Integer = 0
            Buffer.Append(Chars(0))
            Dim i As Integer
            Dim LoopLimit As Integer = Size - 1
            For i = 1 To LoopLimit
                Select Case Chars(i)
                    Case "A", "Ä", "E", "I", "O", "Ö", "U", "Ü", "H", "W", "Y"
                        CurrCode = 0
                    Case "B", "F", "P", "V"
                        CurrCode = 1
                    Case "C", "G", "J", "K", "Q", "S", "X", "Z"
                        CurrCode = 2
                    Case "D", "T"
                        CurrCode = 3
                    Case "L"
                        CurrCode = 4
                    Case "M", "N"
                        CurrCode = 5
                    Case "R"
                        CurrCode = 6
                End Select
                If (CurrCode <> PrevCode) Then
                    If (CurrCode <> 0) Then
                        Buffer.Append(CurrCode)
                    End If
                End If
                If (Buffer.Length = Length) Then
                    Exit For
                End If
            Next
            Size = Buffer.Length
            If (Size < Length) Then
                Buffer.Append("0", (Length - Size))
            End If
            Value = Buffer.ToString()
        End If
        Return Value
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ListView1.Items.Clear()
        Main()
    End Sub
    Private Sub Connect_Click(sender As Object, e As EventArgs) Handles Connect.Click

    End Sub
End Class
