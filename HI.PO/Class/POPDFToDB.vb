Imports System.Data.SqlClient

Public NotInheritable Class POPDFToDB

    Public Shared Function SaveFilePDF(ByVal Temp_PurchaseNO As String, FilePath As String) As Boolean

        Try
            Dim cmd As String = ""
            Dim FileID As String = ""

            Dim FilePDFName As String = FilePath

            If System.IO.File.Exists(FilePDFName) Then


                Try
                    cmd = "Declare @FileId nvarchar(50) ='' "
                    cmd &= vbCrLf & " Select TOP 1  @FileId= ISNULL(MAX(A.FTPurchaseNo),'')   "
                    cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.[TPURTPurchase_PDF] As A With(NOLOCK) WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Temp_PurchaseNO) & "'"
                    cmd &= vbCrLf & "  IF @FileId  ='' "
                    cmd &= vbCrLf & "          BEGIN "
                    cmd &= vbCrLf & "                INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.[TPURTPurchase_PDF] (FTInsUser, FDInsDate, FTInsTime,  FTPurchaseNo)"
                    cmd &= vbCrLf & "                Select ''"
                    cmd &= vbCrLf & "                       ," & HI.UL.ULDate.FormatDateDB
                    cmd &= vbCrLf & "                       ," & HI.UL.ULDate.FormatTimeDB
                    cmd &= vbCrLf & "                      ,'" & HI.UL.ULF.rpQuoted(Temp_PurchaseNO) & "'"
                    cmd &= vbCrLf & "                   IF @@Rowcount > 0 "
                    cmd &= vbCrLf & "                     BEGIN "
                    cmd &= vbCrLf & "                         SET @FileId ='" & HI.UL.ULF.rpQuoted(Temp_PurchaseNO) & "' "
                    cmd &= vbCrLf & "                     END "
                    cmd &= vbCrLf & "          END "
                    cmd &= vbCrLf & "   SELECT @FileId "

                    FileID = (HI.Conn.SQLConn.GetField(cmd, HI.Conn.DB.DataBaseName.DB_PUR, ""))


                    If FileID <> "" Then

                        Dim data As Byte() = System.IO.File.ReadAllBytes(FilePDFName)

                        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PUR)
                        HI.Conn.SQLConn.SqlConnectionOpen()

                        cmd = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.[TPURTPurchase_PDF] "
                        cmd &= " Set  FTStateFIle='1',FDPDFDate=Convert(varchar(10),Getdate(),111),FTPDFTime=Convert(varchar(8),Getdate(),114), FPFile=@FPFile"
                        cmd &= "  Where FTPurchaseNo=@FNFileID"

                        Dim scmd As New SqlCommand(cmd, HI.Conn.SQLConn.Cnn)
                        Dim p6 As New SqlParameter("@FPFile", SqlDbType.VarBinary)
                        p6.Value = data

                        Dim p8 As New SqlParameter("@FNFileID", SqlDbType.NVarChar)
                        p8.Value = Temp_PurchaseNO

                        scmd.Parameters.Add(p6)
                        scmd.Parameters.Add(p8)

                        scmd.ExecuteNonQuery()

                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)

                    End If
                Catch ex As Exception

                End Try

                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Shared Function GetPrefixPO(pOrderNo As String, ByRef psprefix As String) As String
        Dim pPrefix As String = ""
        psprefix = ""


        Return pPrefix
    End Function

End Class
