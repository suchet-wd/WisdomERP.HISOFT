Public NotInheritable Class METHOD

    Public Shared Sub CallActiveToolBarFunction(form As Object)
        Try

            Call CallByName(form.Parent.Parent, "ToolBarFunctionActive", CallType.Method, {form})
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub CallShowForm(form As Object, MenuName As String, MethodName As String, Parm() As String)
        Try

            Call CallByName(form.Parent.Parent, "CallWindowForm", CallType.Method, {MenuName, MethodName, Parm})
        Catch ex As Exception
        End Try
    End Sub

End Class
