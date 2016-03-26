Imports System.data
Imports System.Data.OleDb
Imports DataLogic.My.Resources
Imports System.IO
Imports DataLogic.DataAccessVariables


Namespace DBA


    Public Class DataLogic

#Region " Select Methods "

#End Region

#Region " Insert Methods "

#End Region

#Region " Update Methods "

#End Region

#Region " Delete Methods "


#End Region

#Region " Populate Combo Values "

#End Region

#Region " General Database Commands "

        Public Shared Sub SetupConnection()
            'database_location = Application.StartupPath & "/Data/DDA.mdb"

            cn = New OleDb.OleDbConnection
            cn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & database_location & ";"

        End Sub

        Public Shared Function Read(ByVal sSQL As String) As DataSet
            Try
                cn.Open()
            Catch ex As Exception
            End Try

            Dim da As New OleDbDataAdapter
            Dim ds As New DataSet

            Try

                Dim sc As New OleDb.OleDbCommand(sSQL)
                sc.Connection = cn

                da.SelectCommand = sc

                ds = New DataSet
                da.Fill(ds)
                Debug.WriteLine(ds.Tables(0).Rows.Count)

            Catch ex As Exception
                MsgBox("An Error Has Occurred.  Please Make Sure You Are Pointing to a Valid Database Location")
            End Try

            cn.Close()
            Return ds
        End Function


        Public Shared Shadows Sub Update(ByVal sSQL As String)

            Try
                cn.Open()
            Catch ex As Exception
                '' connection already open, ignore
            End Try

            Dim cmd As New OleDb.OleDbCommand(sSQL, cn)
            Dim dbTrans As OleDb.OleDbTransaction

            Try
                dbTrans = cn.BeginTransaction
                cmd.Connection = cn
                cmd.Transaction = dbTrans
                cmd.CommandTimeout = 90
                cmd.CommandType = CommandType.Text
                cmd.ExecuteNonQuery()
                dbTrans.Commit()
            Catch ex As Exception
                MsgBox(ex.Message)
                dbTrans.Rollback()
            End Try

            cn.Close()
        End Sub

        Public Shared Sub PrepareSQL(ByRef item As String)
            '' by ref will be faster
            item = item.Replace("'", "''")
            'item = "'" & item & "'"
        End Sub


        Public Shared Function GetNextID(ByVal tableName As String, ByVal columnName As String) As Integer
            Dim id As Integer

            Dim sql As String
            Dim ds As DataSet

            sql = "SELECT MAX(" & columnName & ") FROM " & tableName

            ds = Read(sql)

            Try
                id = ds.Tables(0).Rows(0).Item(0)
            Catch
                id = 0
            End Try

            id += 1  '' increment id

            Return id
        End Function

#End Region

    End Class

End Namespace
