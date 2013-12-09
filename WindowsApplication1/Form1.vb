Imports MySql.Data.MySqlClient

Public Class Form1

    Public sConnection As New MySqlConnection
    Public id As Integer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If sConnection.State = ConnectionState.Closed Then
            sConnection.ConnectionString = "server=localhost;userid=brunch;password=brunch;database=brunch"
            sConnection.Open()
        End If

        LoadPeople()
    End Sub

    Public Sub LoadPeople()
        Dim sqlQuery As String = "select * from tbl_people"
        Dim sqlAdapter As New MySqlDataAdapter
        Dim sqlcomand As New MySqlCommand
        Dim Table As New DataTable

        With sqlcomand
            .CommandText = sqlQuery
            .Connection = sConnection
        End With

        With sqlAdapter
            .SelectCommand = sqlcomand
            .Fill(Table)
        End With

        'Fix for istview
        lvPeople.Items.Clear()

        For i = 0 To Table.Rows.Count - 1
            With lvPeople
                .Items.Add(Table.Rows(i)("id"))
                With .Items(.Items.Count - 1).SubItems
                    .Add(Table.Rows(i)("fname"))
                    .Add(Table.Rows(i)("mname"))
                    .Add(Table.Rows(i)("lname"))
                End With
            End With
        Next

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FrmAdd.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If id = Nothing Then
            MsgBox("Please choose a record to edit.", MsgBoxStyle.Exclamation)
        Else
            Dim sqlQuery As String = "SELECT fname,mname,lname FROM tbl_people WHERE id = '" & lvPeople.SelectedItems(0).Text & "'"
            Dim sqlAdapter As New MySqlDataAdapter
            Dim sqlcomand As New MySqlCommand
            Dim sqlTable As New DataTable

            With sqlcomand
                .CommandText = sqlQuery
                .Connection = sConnection
            End With

            With sqlAdapter
                .SelectCommand = sqlcomand
                .Fill(sqlTable)
            End With
            FrmEdit.id = lvPeople.SelectedItems(0).Text
            FrmEdit.fname = sqlTable.Rows(0)("fname")
            FrmEdit.mname = sqlTable.Rows(0)("mname")
            FrmEdit.lname = sqlTable.Rows(0)("lname")
            FrmEdit.ShowDialog()
        End If
    End Sub

    Private Sub lvPeople_MouseClick(sender As Object, e As MouseEventArgs) Handles lvPeople.MouseClick
        id = lvPeople.SelectedItems(0).Text
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If id = Nothing Then
            MsgBox("Please choose an item to delete.", MsgBoxStyle.Exclamation)
        Else
            Dim sqlQuery As String = "DELETE FROM tbl_people WHERE id = '" & id & "'"
            Dim sqlcomand As New MySqlCommand

            With sqlcomand
                .CommandText = sqlQuery
                .Connection = sConnection
                .ExecuteNonQuery()
            End With
            MsgBox("Successfully deleted an item.", MsgBoxStyle.Information)

            LoadPeople()
        End If
    End Sub
End Class
