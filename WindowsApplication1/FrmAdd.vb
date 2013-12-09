Imports MySql.Data.MySqlClient

Public Class FrmAdd

    Public sConnection As New MySqlConnection

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If sConnection.State = ConnectionState.Closed Then
            sConnection.ConnectionString = "server=localhost;userid=brunch;password=brunch;database=brunch"
            sConnection.Open()
        End If

        Dim sqlQuery As String = "INSERT INTO tbl_people(fname,mname,lname) VALUES ('" & Txtfname.Text & _
            "','" & Txtmname.Text & "','" & Txtlname.Text & "')"
        Dim sqlCommand As New MySqlCommand

        With sqlCommand
            .CommandText = sqlQuery
            .Connection = sConnection
            .ExecuteNonQuery()
        End With

        MsgBox("Added new Record")
        Dispose()
        Close()

        'Loads the all the data again from our main form
        Form1.LoadPeople()

    End Sub
End Class