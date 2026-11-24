'You can find my shared link of Github Copilot for my code below
' https://github.com/copilot/share/886813ac-43c4-8447-a842-744420d001b9


Imports System.IO

Public Class Form1
    ' Stores the loaded file path for saving in same folder
    Private loadedFilePath As String = ""

    ' ================================================================
    ' btnOpenFile: Open a text file and display raw contents
    ' ================================================================
    Private Sub btnOpenFile_Click(sender As Object, e As EventArgs) Handles btnOpenFile.Click
        Try
            Dim ofd As New OpenFileDialog()
            ofd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            If ofd.ShowDialog() = DialogResult.OK Then
                loadedFilePath = ofd.FileName
                rtbRawData.Text = File.ReadAllText(loadedFilePath)
            End If
        Catch ex As Exception
            MessageBox.Show("Error opening file: " & ex.Message)
        End Try
    End Sub

    ' ================================================================
    ' btnCleanData: Extract, clean, and fix formatting of the data
    ' ================================================================
    Private Sub btnCleanData_Click(sender As Object, e As EventArgs) Handles btnCleanData.Click
        Try
            ' Step 1: Extract data between ?Alice* and \Wonderland\ tags
            Dim rawText As String = rtbRawData.Text
            If String.IsNullOrWhiteSpace(rawText) Then
                MessageBox.Show("Raw data is empty. Please open a file first.")
                Return
            End If

            Dim extracted As String = ExtractAliceData(rawText)
            If String.IsNullOrWhiteSpace(extracted) Then
                MessageBox.Show("No data found between ?Alice* and \Wonderland\ tags.")
                rtbFinalData.Text = ""
                Return
            End If

            ' Step 2: Filter allowed ASCII using Select Case
            Dim filtered As String = FilterAllowedCharacters(extracted)

            ' Step 3: Beautify dashes and spaces
            Dim beautified As String = BeautifyOutput(filtered)

            ' Step 4: Fix stuck-together words (lowercase followed by uppercase)
            Dim spaced As String = FixStuckWords(beautified)

            ' Step 5: Collapse multiple spaces and trim
            Dim finalOutput As String = CollapseSpacesAndTrim(spaced)

            rtbFinalData.Text = finalOutput
        Catch ex As Exception
            MessageBox.Show("Error cleaning data: " & ex.Message)
        End Try
    End Sub

    ' ================================================================
    ' btnSaveFile: Save cleaned data as MuhammadVBQ.txt in same folder
    ' ================================================================
    Private Sub btnSaveFile_Click(sender As Object, e As EventArgs) Handles btnSaveFile.Click
        Try
            If loadedFilePath = "" Then
                MessageBox.Show("No file loaded. Please open a file first.")
                Return
            End If
            If String.IsNullOrWhiteSpace(rtbFinalData.Text) Then
                MessageBox.Show("No cleaned data to save. Please clean data first.")
                Return
            End If
            Dim folderPath As String = Path.GetDirectoryName(loadedFilePath)
            Dim savePath As String = Path.Combine(folderPath, "MuhammadVBQ.txt")
            File.WriteAllText(savePath, rtbFinalData.Text)
            MessageBox.Show("File saved as MuhammadVBQ.txt in " & folderPath)
        Catch ex As Exception
            MessageBox.Show("Error saving file: " & ex.Message)
        End Try
    End Sub

    ' ================================================================
    ' btnCloseApp: Close the application
    ' ================================================================
    Private Sub btnCloseApp_Click(sender As Object, e As EventArgs) Handles btnCloseApp.Click
        Me.Close()
    End Sub

    ' ================================================================
    ' ExtractAliceData: Concatenate all text between ?Alice* and \Wonderland\
    ' ================================================================
    Private Function ExtractAliceData(ByVal text As String) As String
        Try
            Dim result As String = ""
            Dim startPos As Integer = 0
            While True
                Dim aliceStart As Integer = text.IndexOf("?Alice*", startPos, StringComparison.Ordinal)
                If aliceStart = -1 Then Exit While
                aliceStart += "?Alice*".Length
                Dim wonderEnd As Integer = text.IndexOf("\Wonderland\", aliceStart, StringComparison.Ordinal)
                If wonderEnd = -1 Then Exit While
                If wonderEnd > aliceStart Then
                    result &= text.Substring(aliceStart, wonderEnd - aliceStart)
                End If
                startPos = wonderEnd + "\Wonderland\".Length
            End While
            Return result
        Catch
            Return ""
        End Try
    End Function

    ' ================================================================
    ' FilterAllowedCharacters: Only keep allowed ASCII using Select Case
    ' Digits (48-57), lower-case (97-122), upper-case (65-90),
    ' Space (32), comma (44), hyphen (45), period (46), line feed (10)
    ' ================================================================
    Private Function FilterAllowedCharacters(ByVal input As String) As String
        Dim sb As New System.Text.StringBuilder()
        For Each ch As Char In input
            Select Case Asc(ch)
                Case 32, 44, 45, 46, 10 ' space, comma, hyphen, period, line feed
                    sb.Append(ch)
                Case 48 To 57 ' digits
                    sb.Append(ch)
                Case 65 To 90 ' uppercase
                    sb.Append(ch)
                Case 97 To 122 ' lowercase
                    sb.Append(ch)
                Case Else
                    ' skip others
            End Select
        Next
        Return sb.ToString()
    End Function

    ' ================================================================
    ' BeautifyOutput: 
    ' - Replace double/triple dashes with space
    ' - Replace dashes between letters/numbers with space
    ' ================================================================
    Private Function BeautifyOutput(ByVal input As String) As String
        Dim temp As String = input
        temp = Replace(temp, "---", " ")
        temp = Replace(temp, "--", " ")

        Dim result As New System.Text.StringBuilder()
        Dim i As Integer = 0
        While i < temp.Length
            Dim ch As Char = temp(i)
            If ch = "-"c Then
                Dim prevIsWord As Boolean = (i > 0) AndAlso ((Asc(temp(i - 1)) >= 65 And Asc(temp(i - 1)) <= 90) Or (Asc(temp(i - 1)) >= 97 And Asc(temp(i - 1)) <= 122) Or (Asc(temp(i - 1)) >= 48 And Asc(temp(i - 1)) <= 57))
                Dim nextIsWord As Boolean = (i < temp.Length - 1) AndAlso ((Asc(temp(i + 1)) >= 65 And Asc(temp(i + 1)) <= 90) Or (Asc(temp(i + 1)) >= 97 And Asc(temp(i + 1)) <= 122) Or (Asc(temp(i + 1)) >= 48 And Asc(temp(i + 1)) <= 57))
                If prevIsWord And nextIsWord Then
                    result.Append(" "c)
                Else
                    result.Append("-"c)
                End If
            Else
                result.Append(ch)
            End If
            i += 1
        End While
        Return result.ToString()
    End Function

    ' ================================================================
    ' FixStuckWords: Insert space between lowercase followed by uppercase
    ' ================================================================
    Private Function FixStuckWords(ByVal input As String) As String
        Dim sb As New System.Text.StringBuilder()
        For i As Integer = 0 To input.Length - 2
            sb.Append(input(i))
            If Char.IsLower(input(i)) And Char.IsUpper(input(i + 1)) Then
                sb.Append(" ")
            End If
        Next
        If input.Length > 0 Then sb.Append(input(input.Length - 1))
        Return sb.ToString()
    End Function

    ' ================================================================
    ' CollapseSpacesAndTrim: Collapse multiple spaces and trim output
    ' ================================================================
    Private Function CollapseSpacesAndTrim(ByVal input As String) As String
        Dim sb As New System.Text.StringBuilder()
        Dim wasSpace As Boolean = False
        For Each ch As Char In input
            If ch = " "c Then
                If Not wasSpace Then
                    sb.Append(" ")
                    wasSpace = True
                End If
            Else
                sb.Append(ch)
                wasSpace = False
            End If
        Next
        Return sb.ToString().Trim()
    End Function
End Class