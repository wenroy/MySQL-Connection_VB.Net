Imports MySql.Data.MySqlClient

Public Class FrmEdit

    Friend id As Integer
    Friend fname As String
    Friend mname As String
    Friend lname As String

    Public sConnection As New MySqlConnection
    Private Sub FrmEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Txtfname.Text = fname
        Txtmname.Text = mname
        Txtlname.Text = lname
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If sConnection.State = ConnectionState.Closed Then
            sConnection.ConnectionString = "server=localhost;userid=brunch;password=brunch;database=brunch"
            sConnection.Open()
        End If

        Dim sqlQuery As String = "UPDATE tbl_people SET fname = '" & Txtfname.Text & _
            "',mname = '" & Txtmname.Text & "',lname = '" & Txtlname.Text & "' WHERE id = '" & id & "'"
        Dim sqlCommand As New MySqlCommand

        With sqlCommand
            .CommandText = sqlQuery
            .Connection = sConnection
            .ExecuteNonQuery()
        End With

        MsgBox("Record updated successfully.", MsgBoxStyle.Exclamation)
        Dispose()
        Close()

        'Loads the all the data again from our main form
        Form1.LoadPeople()
    End Sub
End Class