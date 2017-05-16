Imports System.IO
Imports System.Net
Imports System.Web.Helpers
Imports System.Data.SQLite
Public Class Form1

    ' Ziel ist die phonetische Dupletten-Suche in SQL
    ' Start mit vorgefertigtem Suchen nach klarem Ergebnis
    ' Vorgesetzt für Anfang
    Public line As String
    Public input As String
    Public zeile As Int16 = 0
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
            Dim ergebnis = array 'Testweise zum Probieren Ohne JSON -> Weil Schneller
            'Dim localhost As New Uri("http://127.0.0.1:8081") 'Verbindungsaufbau LocalHost
            'Dim host As New Uri(ConnectEingabe.Text.ToString) 'Verbindungsaufbau zur Ziel-URI
            'Dim request As WebRequest = WebRequest.Create(host)
            'Dim response As WebResponse = request.GetResponse()
            'Dim dataStream As Stream = response.GetResponseStream()
            'Dim reader As New StreamReader(dataStream)
            'Dim responseFromServer As String = reader.ReadToEnd()
            'Dim ergebnis = jSONConvert(responseFromServer)
            'reader.Close()
            'response.Close()
            'WriteResult(input, result)
            For Each item As String In ergebnis
                Dim test As String = Umwandeln(item)
                Zahl += 1
                If test = result Then
                    WriteResult(item, "")
                    Sicher += 1
                Else
                    If Sicher = 0 And Zahl = ergebnis.ToString.Length Then
                        WriteResult("", "")
                    End If
                End If
            Next
        End If
    End Sub
    Function jSONConvert(args) 'As String) As Object
        Dim ergebnis As Object
        ergebnis = Json.Decode(args)
        Return ergebnis
    End Function
    Sub WriteError()
        ListView1.Items.Add("Ein Fehler ist aufgetreten!")
        Eingabe.Text = ""
    End Sub
    Sub WriteResult(args, args2)
        ' Such-Ergebnis
        Dim SQLconnect As New SQLite.SQLiteConnection()
        Dim SQLcommand As SQLiteCommand
        SQLconnect.ConnectionString = "Data Source=ergebnis.s3db;"
        If args Is String.Empty Then
            ListView1.Items.Add("Leider keine Ergebnisse :( " & vbNewLine)
            'Wenn keine Ergebnisse, kein Datenbank-Eintrag!
        Else
            ListView1.Items.Add(vbNewLine & "Ergebnis: " & args & "." & vbNewLine)
            Dim zeile As String = SQLReihen()
            SQLconnect.Open()
            SQLcommand = SQLconnect.CreateCommand
            SQLcommand.CommandText = "INSERT INTO ergebnis (id, result) VALUES (" + zeile + ", " + "'" + args + "')"
            'Datenbank-Eintrag des gesuchten Begriffes
            SQLcommand.ExecuteNonQuery()
            SQLcommand.Dispose()
            SQLconnect.Close()
            If args2 IsNot String.Empty Then
                ListView1.Items.Add(vbNewLine & "Code: " & args2 & vbNewLine)
                SQLconnect.Open()
                SQLcommand = SQLconnect.CreateCommand
                SQLcommand.CommandText = "INSERT INTO ergebnis (id, result) VALUES (" + zeile + ", " + "'" + args2 + "')"
                'Datenbank-Eintrag des SoundEx-Querverweises zur späteren Überprüfung, falls notwendig
                SQLcommand.ExecuteNonQuery()
                SQLcommand.Dispose()
                SQLconnect.Close()
            End If
        End If
    End Sub
    Function SQLReihen()
        Dim SQLconnect As New SQLite.SQLiteConnection()
        Dim SQLcommand As SQLiteCommand
        SQLconnect.ConnectionString = "Data Source=ergebnis.s3db;"
        SQLcommand = SQLconnect.CreateCommand
        SQLconnect.Open()
        SQLcommand.CommandText = "SELECT COUNT(id) FROM ergebnis"
        Dim resultat As SQLiteDataReader = SQLcommand.ExecuteReader()
        While (resultat.Read())
            zeile = resultat.GetValue(0)
        End While
        SQLconnect.Close()
        SQLcommand.Dispose()
        Return zeile
    End Function
    Sub DataBase()
        If (File.Exists("ergebnis.s3db")) = False Then
            CreateDataBase()
            CreateTables()
        Else
        End If
    End Sub
    Sub CreateDataBase()
        Dim connect As New SQLite.SQLiteConnection()
        connect.ConnectionString = "Data Source=ergebnis.s3db;"
        connect.Open()
        connect.Close()
    End Sub
    Sub CreateTables()
        Dim connect As New SQLite.SQLiteConnection()
        Dim command As SQLiteCommand
        connect.ConnectionString = "Data Source=ergebnis.s3db;"
        connect.Open()
        command = connect.CreateCommand
        command.CommandText = "CREATE TABLE ergebnis (id INTEGER PRIMARY KEY AUTOINCREMENT,result TEXT);"
        command.ExecuteNonQuery()
        command.Dispose()
        connect.Close()

    End Sub
    Sub CreateData()
        Dim SQLconnect As New SQLite.SQLiteConnection()
        Dim SQLcommand As SQLiteCommand
        SQLconnect.ConnectionString = "Data Source=ergebnis.s3db;"
        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand
        SQLcommand.CommandText = "INSERT INTO ergebnis (id, result) VALUES (1, ' ')"
        SQLcommand.ExecuteNonQuery()
        SQLcommand.Dispose()
        SQLconnect.Close()
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
End Class
