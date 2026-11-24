

Imports System.Data.OleDb
    Imports System.IO

    Public Class Form1

        Dim filePath As String = ""

        ' Button 1 - Browse
        Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
            Dim ofd As New OpenFileDialog()
            ofd.Filter = "Excel Files|*.xls;*.xlsx"
            ofd.Title = "Select Excel File"

            If ofd.ShowDialog() = DialogResult.OK Then
                filePath = ofd.FileName
                txtFilePath.Text = filePath
            End If
        End Sub

        ' Button 2 - Load Excel File
        Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
            If filePath = "" Or Not File.Exists(filePath) Then
                MessageBox.Show("Please select a valid Excel file first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Try
                Dim connStr As String
                If Path.GetExtension(filePath).ToLower() = ".xls" Then
                    connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & filePath & ";Extended Properties='Excel 8.0;HDR=YES;'"
                Else
                    connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & filePath & ";Extended Properties='Excel 12.0 Xml;HDR=YES;'"
                End If

                Using conn As New OleDbConnection(connStr)
                    conn.Open()

                    ' Get first sheet name
                    Dim dtSheet As DataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                    Dim sheetName As String = dtSheet.Rows(0)("TABLE_NAME").ToString()

                    Dim cmd As New OleDbCommand("SELECT * FROM [" & sheetName & "]", conn)
                    Dim adapter As New OleDbDataAdapter(cmd)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)

                    dataGridView1.DataSource = dt
                End Using

            Catch ex As Exception
                MessageBox.Show("Error loading file: " & ex.Message)
            End Try
        End Sub

        ' Button 3 - Save as CSV (for simplicity)
        Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
            If dataGridView1.DataSource Is Nothing Then
                MessageBox.Show("No data to save.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim sfd As New SaveFileDialog()
            sfd.Filter = "CSV File|*.csv"
            sfd.Title = "Save as CSV"

            If sfd.ShowDialog() = DialogResult.OK Then
                Try
                    Using sw As New StreamWriter(sfd.FileName)
                        ' Write column headers
                        For i As Integer = 0 To dataGridView1.Columns.Count - 1
                            sw.Write(dataGridView1.Columns(i).HeaderText)
                            If i < dataGridView1.Columns.Count - 1 Then sw.Write(",")
                        Next
                        sw.WriteLine()

                        ' Write rows
                        For Each row As DataGridViewRow In dataGridView1.Rows
                            If Not row.IsNewRow Then
                                For i As Integer = 0 To dataGridView1.Columns.Count - 1
                                    sw.Write(row.Cells(i).Value?.ToString())
                                    If i < dataGridView1.Columns.Count - 1 Then sw.Write(",")
                                Next
                                sw.WriteLine()
                            End If
                        Next
                    End Using

                    MessageBox.Show("File saved successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Catch ex As Exception
                    MessageBox.Show("Error saving file: " & ex.Message)
                End Try
            End If
        End Sub


End Class
