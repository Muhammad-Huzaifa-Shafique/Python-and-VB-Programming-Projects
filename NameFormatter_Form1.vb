
'link of my AI(ChatGPT) conversation below

'https://chatgpt.com/share/68ba97d1-db24-8005-9d87-cbbe321dc088

Imports System.IO
Imports System.Globalization
Public Class Form1

    ' Define a structure to store name and contribution
    Structure Contributor
        Public Name As String
        Public Contribution As String
    End Structure

    ' List to store unique contributors
    Private contributors As New List(Of Contributor)

    ' Load file button click
    Private Sub btnLoadFile_Click(sender As Object, e As EventArgs) Handles btnLoadFile.Click
        Dim ofd As New OpenFileDialog()
        ofd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        If ofd.ShowDialog() = DialogResult.OK Then
            LoadContributors(ofd.FileName)
        End If
    End Sub

    ' Load contributors from file
    Private Sub LoadContributors(filePath As String)
        contributors.Clear()
        dgvContributors.Rows.Clear()
        Dim lines() As String = File.ReadAllLines(filePath)

        For Each line As String In lines
            ' Expecting format: FirstName LastName, Contribution
            If line.Contains(",") Then
                Dim parts() As String = line.Split(","c)
                Dim name As String = ProperCase(parts(0).Trim())
                Dim contribution As String = parts(1).Trim()

                ' Format name as LastName FirstName
                Dim nameParts() As String = name.Split(" "c)
                If nameParts.Length > 1 Then
                    Dim formattedName As String = nameParts(nameParts.Length - 1) & " " & String.Join(" ", nameParts, 0, nameParts.Length - 1)
                    name = formattedName
                End If

                ' Avoid duplicates
                If Not contributors.Any(Function(c) c.Name = name AndAlso c.Contribution = contribution) Then
                    contributors.Add(New Contributor With {.Name = name, .Contribution = contribution})
                End If
            End If
        Next

        DisplayContributors(contributors)
    End Sub

    ' Properly capitalize names
    Private Function ProperCase(input As String) As String
        Return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower())
    End Function

    ' Display contributors in DataGridView
    Private Sub DisplayContributors(list As List(Of Contributor))
        dgvContributors.Rows.Clear()
        For Each c In list
            dgvContributors.Rows.Add(c.Name, c.Contribution)
        Next
    End Sub

    ' Sort by name button
    Private Sub btnSortByName_Click(sender As Object, e As EventArgs) Handles btnSortByName.Click
        Dim sortedList = contributors.OrderBy(Function(c) c.Name).ToList()
        DisplayContributors(sortedList)
    End Sub

    ' Sort by contribution then name
    Private Sub btnSortByContribution_Click(sender As Object, e As EventArgs) Handles btnSortByContribution.Click
        Dim sortedList = contributors.OrderBy(Function(c) c.Contribution).ThenBy(Function(c) c.Name).ToList()
        DisplayContributors(sortedList)
    End Sub

    Private Sub btnSaveFile_Click(sender As Object, e As EventArgs) Handles btnSaveFile.Click
        If contributors.Count = 0 Then
            MessageBox.Show("No contributors to save.", "Save File", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Try
            ' Save in the same location as the project executable (ProjectName\bin\Debug\Muhammad Huzaifa Shafique.txt)
            Dim projectPath As String = Application.StartupPath
            Dim fileName As String = Path.Combine(projectPath, "Muhammad Huzaifa Shafique.txt")

            Using writer As New StreamWriter(fileName, False)
                For Each c In contributors
                    writer.WriteLine(c.Name & ", " & c.Contribution)
                Next
            End Using

            MessageBox.Show("Contributors saved successfully!" & Environment.NewLine &
                            "File: " & fileName,
                            "Save File", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error saving file: " & ex.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
