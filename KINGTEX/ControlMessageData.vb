Public Class ControlMessageData
    Public Sub MessageDataType01(ControlData As String, DataString As String(), Optional ClientIP As String = "")
        Try
            Dim cmdstring As String = ""
            cmdstring = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTMessageType01(FDInsDate,FTInsTime,FTID,FTControlData,FTStateRead,FTClientIP)"
            cmdstring &= vbCrLf & " SELECT " & HI.UL.ULDate.FormatDateDB & ""
            cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
            cmdstring &= vbCrLf & " ,REPLACE(CONVERT(varchar(50),Getdate(),111),'/','') +'-'+ REPLACE(CONVERT(varchar(50),Getdate(),114),':','')"
            cmdstring &= vbCrLf & " ,N'" & ControlData & "'"
            cmdstring &= vbCrLf & " ,'0'"
            cmdstring &= vbCrLf & " ,'" & ClientIP & "'"

            HI.Conn.SQLConn.ExecuteOnly(cmdstring, HI.Conn.DB.DataBaseName.DB_PROD)
        Catch ex As Exception
        End Try
    End Sub

    Public Sub MessageDataType02(ControlData As String, DataString As String(), Optional ClientIP As String = "")
        Try
            Dim cmdstring As String = ""
            cmdstring = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTMessageType02(FDInsDate,FTInsTime,FTID,FTControlData,FTStateRead,FTClientIP)"
            cmdstring &= vbCrLf & " SELECT " & HI.UL.ULDate.FormatDateDB & ""
            cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
            cmdstring &= vbCrLf & " ,REPLACE(CONVERT(varchar(50),Getdate(),111),'/','') +'-'+ REPLACE(CONVERT(varchar(50),Getdate(),114),':','')"
            cmdstring &= vbCrLf & " ,N'" & ControlData & "'"
            cmdstring &= vbCrLf & " ,'0'"
            cmdstring &= vbCrLf & " ,'" & ClientIP & "'"

            HI.Conn.SQLConn.ExecuteOnly(cmdstring, HI.Conn.DB.DataBaseName.DB_PROD)
        Catch ex As Exception
        End Try
    End Sub

    Public Sub MessageDataType03(ControlData As String, DataString As String(), Optional ClientIP As String = "")
        Try
            Dim cmdstring As String = ""
            cmdstring = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTMessageType03(FDInsDate,FTInsTime,FTID,FTControlData,FTStateRead,FTClientIP)"
            cmdstring &= vbCrLf & " SELECT " & HI.UL.ULDate.FormatDateDB & ""
            cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
            cmdstring &= vbCrLf & " ,REPLACE(CONVERT(varchar(50),Getdate(),111),'/','') +'-'+ REPLACE(CONVERT(varchar(50),Getdate(),114),':','')"
            cmdstring &= vbCrLf & " ,N'" & ControlData & "'"
            cmdstring &= vbCrLf & " ,'0'"
            cmdstring &= vbCrLf & " ,'" & ClientIP & "'"

            HI.Conn.SQLConn.ExecuteOnly(cmdstring, HI.Conn.DB.DataBaseName.DB_PROD)
        Catch ex As Exception
        End Try
    End Sub

    Public Sub MessageDataType04(ControlData As String, DataString As String(), Optional ClientIP As String = "")
        Try
            Dim cmdstring As String = ""
            cmdstring = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTMessageType04(FDInsDate,FTInsTime,FTID,FTControlData,FTStateRead,FTClientIP)"
            cmdstring &= vbCrLf & " SELECT " & HI.UL.ULDate.FormatDateDB & ""
            cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
            cmdstring &= vbCrLf & " ,REPLACE(CONVERT(varchar(50),Getdate(),111),'/','') +'-'+ REPLACE(CONVERT(varchar(50),Getdate(),114),':','')"
            cmdstring &= vbCrLf & " ,N'" & ControlData & "'"
            cmdstring &= vbCrLf & " ,'0'"
            cmdstring &= vbCrLf & " ,'" & ClientIP & "'"

            HI.Conn.SQLConn.ExecuteOnly(cmdstring, HI.Conn.DB.DataBaseName.DB_PROD)
        Catch ex As Exception
        End Try
    End Sub

    Public Sub MessageDataType05(ControlData As String, DataString As String(), Optional ClientIP As String = "")
        Try
            Dim cmdstring As String = ""
            cmdstring = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTMessageType05(FDInsDate,FTInsTime,FTID,FTControlData,FTStateRead,FTClientIP)"
            cmdstring &= vbCrLf & " SELECT " & HI.UL.ULDate.FormatDateDB & ""
            cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
            cmdstring &= vbCrLf & " ,REPLACE(CONVERT(varchar(50),Getdate(),111),'/','') +'-'+ REPLACE(CONVERT(varchar(50),Getdate(),114),':','')"
            cmdstring &= vbCrLf & " ,N'" & ControlData & "'"
            cmdstring &= vbCrLf & " ,'0'"
            cmdstring &= vbCrLf & " ,'" & ClientIP & "'"

            HI.Conn.SQLConn.ExecuteOnly(cmdstring, HI.Conn.DB.DataBaseName.DB_PROD)
        Catch ex As Exception
        End Try
    End Sub
    Public Sub MessageDataType06(ControlData As String, DataString As String(), Optional ClientIP As String = "")
        Try
            Dim cmdstring As String = ""
            cmdstring = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTMessageType06(FDInsDate,FTInsTime,FTID,FTControlData,FTStateRead,FTClientIP)"
            cmdstring &= vbCrLf & " SELECT " & HI.UL.ULDate.FormatDateDB & ""
            cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
            cmdstring &= vbCrLf & " ,REPLACE(CONVERT(varchar(50),Getdate(),111),'/','') +'-'+ REPLACE(CONVERT(varchar(50),Getdate(),114),':','')"
            cmdstring &= vbCrLf & " ,N'" & ControlData & "'"
            cmdstring &= vbCrLf & " ,'0'"
            cmdstring &= vbCrLf & " ,'" & ClientIP & "'"

            HI.Conn.SQLConn.ExecuteOnly(cmdstring, HI.Conn.DB.DataBaseName.DB_PROD)
        Catch ex As Exception
        End Try
    End Sub
    Public Sub MessageDataType07(ControlData As String, DataString As String(), Optional ClientIP As String = "")
        Try
            Dim cmdstring As String = ""
            cmdstring = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTMessageType07(FDInsDate,FTInsTime,FTID,FTControlData,FTStateRead,FTClientIP)"
            cmdstring &= vbCrLf & " SELECT " & HI.UL.ULDate.FormatDateDB & ""
            cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
            cmdstring &= vbCrLf & " ,REPLACE(CONVERT(varchar(50),Getdate(),111),'/','') +'-'+ REPLACE(CONVERT(varchar(50),Getdate(),114),':','')"
            cmdstring &= vbCrLf & " ,N'" & ControlData & "'"
            cmdstring &= vbCrLf & " ,'0'"
            cmdstring &= vbCrLf & " ,'" & ClientIP & "'"

            HI.Conn.SQLConn.ExecuteOnly(cmdstring, HI.Conn.DB.DataBaseName.DB_PROD)
        Catch ex As Exception
        End Try
    End Sub
    Public Sub MessageDataType08(ControlData As String, DataString As String(), Optional ClientIP As String = "")
        Try
            Dim cmdstring As String = ""
            cmdstring = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTMessageType08(FDInsDate,FTInsTime,FTID,FTControlData,FTStateRead,FTClientIP)"
            cmdstring &= vbCrLf & " SELECT " & HI.UL.ULDate.FormatDateDB & ""
            cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
            cmdstring &= vbCrLf & " ,REPLACE(CONVERT(varchar(50),Getdate(),111),'/','') +'-'+ REPLACE(CONVERT(varchar(50),Getdate(),114),':','')"
            cmdstring &= vbCrLf & " ,N'" & ControlData & "'"
            cmdstring &= vbCrLf & " ,'0'"
            cmdstring &= vbCrLf & " ,'" & ClientIP & "'"

            HI.Conn.SQLConn.ExecuteOnly(cmdstring, HI.Conn.DB.DataBaseName.DB_PROD)
        Catch ex As Exception
        End Try
    End Sub
    Public Sub MessageDataType09(ControlData As String, DataString As String(), Optional ClientIP As String = "")
        Try
            Dim cmdstring As String = ""
            cmdstring = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTMessageType09(FDInsDate,FTInsTime,FTID,FTControlData,FTStateRead,FTClientIP)"
            cmdstring &= vbCrLf & " SELECT " & HI.UL.ULDate.FormatDateDB & ""
            cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
            cmdstring &= vbCrLf & " ,REPLACE(CONVERT(varchar(50),Getdate(),111),'/','') +'-'+ REPLACE(CONVERT(varchar(50),Getdate(),114),':','')"
            cmdstring &= vbCrLf & " ,N'" & ControlData & "'"
            cmdstring &= vbCrLf & " ,'0'"
            cmdstring &= vbCrLf & " ,'" & ClientIP & "'"

            HI.Conn.SQLConn.ExecuteOnly(cmdstring, HI.Conn.DB.DataBaseName.DB_PROD)
        Catch ex As Exception
        End Try
    End Sub
    Public Sub MessageDataType10(ControlData As String, DataString As String(), Optional ClientIP As String = "")
        Try

            Dim cmdstring As String = ""
            cmdstring = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTMessageType10(FDInsDate,FTInsTime,FTID,FTControlData,FTStateRead,FTClientIP)"
            cmdstring &= vbCrLf & " SELECT " & HI.UL.ULDate.FormatDateDB & ""
            cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
            cmdstring &= vbCrLf & " ,REPLACE(CONVERT(varchar(50),Getdate(),111),'/','') +'-'+ REPLACE(CONVERT(varchar(50),Getdate(),114),':','')"
            cmdstring &= vbCrLf & " ,N'" & ControlData & "'"
            cmdstring &= vbCrLf & " ,'0'"
            cmdstring &= vbCrLf & " ,'" & ClientIP & "'"

            HI.Conn.SQLConn.ExecuteOnly(cmdstring, HI.Conn.DB.DataBaseName.DB_PROD)

        Catch ex As Exception
        End Try

    End Sub

    Public Sub SpliltMessageDataType01(FTID As String, FDInsDate As String, FTInsTime As String, ControlData As String, Optional ClientIP As String = "")
        Try
            Dim DataAll() As String = ControlData.Split("=")
            Dim cmdstring As String = ""
            If DataAll.Length >= 2 Then
                Dim DataSewing() As String = DataAll(1).Split(",")


                Dim FTMachineID As String = ""
                Dim FTRFID01 As String = ""
                Dim FTRFID02 As String = ""
                Dim FTRFID03 As String = ""
                Dim FTBoardNumber As String = ""
                Dim FTSerialNumber As String = ""

                Try
                    FTMachineID = Val(DataSewing(1))
                Catch ex As Exception
                End Try

                Try
                    FTRFID01 = Val(DataSewing(2))
                Catch ex As Exception
                End Try

                Try
                    FTRFID02 = Val(DataSewing(3))
                Catch ex As Exception
                End Try

                Try
                    FTRFID03 = Val(DataSewing(4))
                Catch ex As Exception
                End Try

                Try
                    FTBoardNumber = Val(DataSewing(5))
                Catch ex As Exception
                End Try

                Try
                    FTSerialNumber = Val(DataSewing(6))
                Catch ex As Exception
                End Try

                cmdstring &= vbCrLf & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTData01(FDInsDate,FTInsTime,FTID,FTControlData,FTClientIP,FTMachineID,FTRFID01,FTRFID02,FTRFID03,FTBoardNumber,FTSerialNumber)"
                cmdstring &= vbCrLf & " SELECT  '" & FDInsDate & "'"
                cmdstring &= vbCrLf & " ,'" & FTInsTime & "'"
                cmdstring &= vbCrLf & " ,'" & FTID & "'"
                cmdstring &= vbCrLf & " ,N'" & ControlData & "'"
                cmdstring &= vbCrLf & " ,'" & ClientIP & "'"
                cmdstring &= vbCrLf & " ,'" & FTMachineID & "'"
                cmdstring &= vbCrLf & " ,'" & FTRFID01 & "'"
                cmdstring &= vbCrLf & " ,'" & FTRFID02 & "'"
                cmdstring &= vbCrLf & " ,'" & FTRFID03 & "'"
                cmdstring &= vbCrLf & " ,'" & FTBoardNumber & "'"
                cmdstring &= vbCrLf & " ,'" & FTSerialNumber & "'"

                HI.Conn.SQLConn.ExecuteOnly(cmdstring, HI.Conn.DB.DataBaseName.DB_PROD)

                cmdstring = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTMessageType01 WHERE FTID='" & HI.UL.ULF.rpQuoted(FTID) & "' AND FDInsDate='" & FDInsDate & "' AND FTInsTime='" & FTInsTime & "'"

                HI.Conn.SQLConn.ExecuteOnly(cmdstring, HI.Conn.DB.DataBaseName.DB_PROD)
            Else
                cmdstring = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTMessageType01 WHERE FTID='" & HI.UL.ULF.rpQuoted(FTID) & "'  AND FDInsDate='" & FDInsDate & "' AND FTInsTime='" & FTInsTime & "'"
                cmdstring &= vbCrLf & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTData01(FDInsDate,FTInsTime,FTID,FTControlData,FTClientIP,FTMachineID,FTRFID01,FTRFID02,FTRFID03,FTBoardNumber,FTSerialNumber)"
                cmdstring &= vbCrLf & " SELECT  '" & FDInsDate & "'"
                cmdstring &= vbCrLf & " ,'" & FTInsTime & "'"
                cmdstring &= vbCrLf & " ,'" & FTID & "'"
                cmdstring &= vbCrLf & " ,N'" & ControlData & "'"
                cmdstring &= vbCrLf & " ,'" & ClientIP & "'"
                cmdstring &= vbCrLf & " ,'','','','','',''"

                HI.Conn.SQLConn.ExecuteOnly(cmdstring, HI.Conn.DB.DataBaseName.DB_PROD)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub SpliltMessageDataType02(FTID As String, FDInsDate As String, FTInsTime As String, ControlData As String, Optional ClientIP As String = "")
        Try
            Dim DataAll() As String = ControlData.Split("=")
            Dim cmdstring As String = ""
            If DataAll.Length >= 2 Then
                Dim DataSewing() As String = DataAll(1).Split(",")


                Dim FTMachineID As String = ""
                Dim FTRFID01 As String = ""
                Dim FTRFID02 As String = ""
                Dim FTRFID03 As String = ""
                Dim FTBoardNumber As String = ""
                Dim FTSerialNumber As String = ""

                Try
                    FTMachineID = Val(DataSewing(1))
                Catch ex As Exception
                End Try

                Try
                    FTRFID01 = Val(DataSewing(2))
                Catch ex As Exception
                End Try

                Try
                    FTRFID02 = Val(DataSewing(3))
                Catch ex As Exception
                End Try

                Try
                    FTRFID03 = Val(DataSewing(4))
                Catch ex As Exception
                End Try

                Try
                    FTBoardNumber = Val(DataSewing(5))
                Catch ex As Exception
                End Try

                Try
                    FTSerialNumber = Val(DataSewing(6))
                Catch ex As Exception
                End Try

                cmdstring &= vbCrLf & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTData02(FDInsDate,FTInsTime,FTID,FTControlData,FTClientIP,FTMachineID,FTRFID01,FTRFID02,FTRFID03,FTBoardNumber,FTSerialNumber)"
                cmdstring &= vbCrLf & " SELECT  '" & FDInsDate & "'"
                cmdstring &= vbCrLf & " ,'" & FTInsTime & "'"
                cmdstring &= vbCrLf & " ,'" & FTID & "'"
                cmdstring &= vbCrLf & " ,N'" & ControlData & "'"
                cmdstring &= vbCrLf & " ,'" & ClientIP & "'"
                cmdstring &= vbCrLf & " ,'" & FTMachineID & "'"
                cmdstring &= vbCrLf & " ,'" & FTRFID01 & "'"
                cmdstring &= vbCrLf & " ,'" & FTRFID02 & "'"
                cmdstring &= vbCrLf & " ,'" & FTRFID03 & "'"
                cmdstring &= vbCrLf & " ,'" & FTBoardNumber & "'"
                cmdstring &= vbCrLf & " ,'" & FTSerialNumber & "'"

                HI.Conn.SQLConn.ExecuteOnly(cmdstring, HI.Conn.DB.DataBaseName.DB_PROD)

                cmdstring = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTMessageType02 WHERE FTID='" & HI.UL.ULF.rpQuoted(FTID) & "'  AND FDInsDate='" & FDInsDate & "' AND FTInsTime='" & FTInsTime & "'"

                HI.Conn.SQLConn.ExecuteOnly(cmdstring, HI.Conn.DB.DataBaseName.DB_PROD)
            Else
                cmdstring = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTMessageType02 WHERE FTID='" & HI.UL.ULF.rpQuoted(FTID) & "'  AND FDInsDate='" & FDInsDate & "' AND FTInsTime='" & FTInsTime & "'"
                cmdstring &= vbCrLf & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTData02(FDInsDate,FTInsTime,FTID,FTControlData,FTClientIP,FTMachineID,FTRFID01,FTRFID02,FTRFID03,FTBoardNumber,FTSerialNumber)"
                cmdstring &= vbCrLf & " SELECT  '" & FDInsDate & "'"
                cmdstring &= vbCrLf & " ,'" & FTInsTime & "'"
                cmdstring &= vbCrLf & " ,'" & FTID & "'"
                cmdstring &= vbCrLf & " ,N'" & ControlData & "'"
                cmdstring &= vbCrLf & " ,'" & ClientIP & "'"
                cmdstring &= vbCrLf & " ,'','','','','',''"

                HI.Conn.SQLConn.ExecuteOnly(cmdstring, HI.Conn.DB.DataBaseName.DB_PROD)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub SpliltMessageDataType03(FTID As String, FDInsDate As String, FTInsTime As String, ControlData As String, Optional ClientIP As String = "")
        Try
            Dim DataAll() As String = ControlData.Split("=")
            Dim cmdstring As String = ""
            If DataAll.Length >= 2 Then
                Dim DataSewing() As String = DataAll(1).Split(",")


                Dim FTMachineID As String = ""
                Dim FTRFID01 As String = ""
                Dim FTRFID02 As String = ""
                Dim FTRFID03 As String = ""
                Dim FTBoardNumber As String = ""
                Dim FTSerialNumber As String = ""


                Try
                    FTMachineID = Val(DataSewing(1))
                Catch ex As Exception
                End Try

                Try
                    FTRFID01 = Val(DataSewing(2))
                Catch ex As Exception
                End Try

                Try
                    FTRFID02 = Val(DataSewing(3))
                Catch ex As Exception
                End Try

                Try
                    FTRFID03 = Val(DataSewing(4))
                Catch ex As Exception
                End Try

                Try
                    FTBoardNumber = Val(DataSewing(5))
                Catch ex As Exception
                End Try

                Try
                    FTSerialNumber = Val(DataSewing(6))
                Catch ex As Exception
                End Try

                cmdstring &= vbCrLf & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTData03(FDInsDate,FTInsTime,FTID,FTControlData,FTClientIP,FTMachineID,FTRFID01,FTRFID02,FTRFID03,FTBoardNumber,FTSerialNumber)"
                cmdstring &= vbCrLf & " SELECT  '" & FDInsDate & "'"
                cmdstring &= vbCrLf & " ,'" & FTInsTime & "'"
                cmdstring &= vbCrLf & " ,'" & FTID & "'"
                cmdstring &= vbCrLf & " ,N'" & ControlData & "'"
                cmdstring &= vbCrLf & " ,'" & ClientIP & "'"
                cmdstring &= vbCrLf & " ,'" & FTMachineID & "'"
                cmdstring &= vbCrLf & " ,'" & FTRFID01 & "'"
                cmdstring &= vbCrLf & " ,'" & FTRFID02 & "'"
                cmdstring &= vbCrLf & " ,'" & FTRFID03 & "'"
                cmdstring &= vbCrLf & " ,'" & FTBoardNumber & "'"
                cmdstring &= vbCrLf & " ,'" & FTSerialNumber & "'"

                HI.Conn.SQLConn.ExecuteOnly(cmdstring, HI.Conn.DB.DataBaseName.DB_PROD)

                cmdstring = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTMessageType03 WHERE FTID='" & HI.UL.ULF.rpQuoted(FTID) & "'  AND FDInsDate='" & FDInsDate & "' AND FTInsTime='" & FTInsTime & "'"

                HI.Conn.SQLConn.ExecuteOnly(cmdstring, HI.Conn.DB.DataBaseName.DB_PROD)
            Else
                cmdstring = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTMessageType03 WHERE FTID='" & HI.UL.ULF.rpQuoted(FTID) & "'  AND FDInsDate='" & FDInsDate & "' AND FTInsTime='" & FTInsTime & "'"
                cmdstring &= vbCrLf & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTData03(FDInsDate,FTInsTime,FTID,FTControlData,FTClientIP,FTMachineID,FTRFID01,FTRFID02,FTRFID03,FTBoardNumber,FTSerialNumber)"
                cmdstring &= vbCrLf & " SELECT  '" & FDInsDate & "'"
                cmdstring &= vbCrLf & " ,'" & FTInsTime & "'"
                cmdstring &= vbCrLf & " ,'" & FTID & "'"
                cmdstring &= vbCrLf & " ,N'" & ControlData & "'"
                cmdstring &= vbCrLf & " ,'" & ClientIP & "'"
                cmdstring &= vbCrLf & " ,'','','','','',''"

                HI.Conn.SQLConn.ExecuteOnly(cmdstring, HI.Conn.DB.DataBaseName.DB_PROD)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub SpliltMessageDataType05(FTID As String, FDInsDate As String, FTInsTime As String, ControlData As String, Optional ClientIP As String = "")
        Try
            Dim DataAll() As String = ControlData.Split("=")
            Dim cmdstring As String = ""
            If DataAll.Length >= 2 Then

                Dim DataIdx As Integer = 0
                For Each DataM As String In DataAll(1).Split("S")

                    If DataIdx > 0 Then
                        Dim DataSewing() As String = DataM.Split(",")

                        Dim FNTimeSewing As Integer = 0
                        Dim FNTotalStitches As Integer = 0
                        Dim FNAVGSpeed As Integer = 0
                        Dim FNRunTime As Integer = 0
                        Dim FNStopTime As Integer = 0
                        Dim FNTimeBetweenStartEnd As Integer = 0

                        If DataIdx = 1 Then

                            Try
                                FNTimeSewing = Val(DataSewing(1))
                            Catch ex As Exception
                            End Try

                            Try
                                FNTotalStitches = Val(DataSewing(2))
                            Catch ex As Exception
                            End Try

                            Try
                                FNAVGSpeed = Val(DataSewing(3))
                            Catch ex As Exception
                            End Try

                            Try
                                FNRunTime = Val(DataSewing(4))
                            Catch ex As Exception
                            End Try

                            Try
                                FNStopTime = Val(DataSewing(5))
                            Catch ex As Exception
                            End Try

                            Try
                                FNTimeBetweenStartEnd = Val(DataSewing(6))
                            Catch ex As Exception
                            End Try

                            cmdstring &= vbCrLf & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTData05(FDInsDate,FTInsTime,FTID,FTControlData,FTClientIP,FNTimeSewing,FNTotalStitches,FNAVGSpeed,FNRunTime,FNStopTime,FNTimeBetweenStartEnd)"
                            cmdstring &= vbCrLf & " SELECT  '" & FDInsDate & "'"
                            cmdstring &= vbCrLf & " ,'" & FTInsTime & "'"
                            cmdstring &= vbCrLf & " ,'" & FTID & "'"
                            cmdstring &= vbCrLf & " ,N'" & ControlData & "'"
                            cmdstring &= vbCrLf & " ,'" & ClientIP & "'"
                            cmdstring &= vbCrLf & " ," & FNTimeSewing & ""
                            cmdstring &= vbCrLf & " ," & FNTotalStitches & ""
                            cmdstring &= vbCrLf & " ," & FNAVGSpeed & ""
                            cmdstring &= vbCrLf & " ," & FNRunTime & ""
                            cmdstring &= vbCrLf & " ," & FNStopTime & ""
                            cmdstring &= vbCrLf & " ," & FNTimeBetweenStartEnd & ""

                            HI.Conn.SQLConn.ExecuteOnly(cmdstring, HI.Conn.DB.DataBaseName.DB_PROD)
                        Else


                            Try
                                FNTimeSewing = Val(DataSewing(1))
                            Catch ex As Exception
                            End Try

                            Try
                                FNTotalStitches = Val(DataSewing(2))
                            Catch ex As Exception
                            End Try

                            Try
                                FNAVGSpeed = Val(DataSewing(3))
                            Catch ex As Exception
                            End Try

                            Try
                                FNRunTime = Val(DataSewing(4))
                            Catch ex As Exception
                            End Try

                            Try
                                FNStopTime = Val(DataSewing(5))
                            Catch ex As Exception
                            End Try

                            Try
                                FNTimeBetweenStartEnd = Val(DataSewing(6))
                            Catch ex As Exception
                            End Try

                            cmdstring &= vbCrLf & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTData0501(FDInsDate,FTInsTime,FTID,FNSeq,FNTimeSewing,FNStitches,FNAVGSpeed,FNRunTime,FNStopTime,FNTimeDuringSectionSewing)"
                            cmdstring &= vbCrLf & " SELECT  '" & FDInsDate & "'"
                            cmdstring &= vbCrLf & " ,'" & FTInsTime & "'"
                            cmdstring &= vbCrLf & " ,'" & FTID & "'"
                            cmdstring &= vbCrLf & " ," & DataIdx - 1 & ""
                            cmdstring &= vbCrLf & " ," & FNTimeSewing & ""
                            cmdstring &= vbCrLf & " ," & FNTotalStitches & ""
                            cmdstring &= vbCrLf & " ," & FNAVGSpeed & ""
                            cmdstring &= vbCrLf & " ," & FNRunTime & ""
                            cmdstring &= vbCrLf & " ," & FNStopTime & ""
                            cmdstring &= vbCrLf & " ," & FNTimeBetweenStartEnd & ""

                            HI.Conn.SQLConn.ExecuteOnly(cmdstring, HI.Conn.DB.DataBaseName.DB_PROD)

                        End If


                    End If
                    DataIdx = DataIdx + 1
                Next

                cmdstring = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTMessageType05 WHERE FTID='" & HI.UL.ULF.rpQuoted(FTID) & "'  AND FDInsDate='" & FDInsDate & "' AND FTInsTime='" & FTInsTime & "'"

                HI.Conn.SQLConn.ExecuteOnly(cmdstring, HI.Conn.DB.DataBaseName.DB_PROD)

            Else

                cmdstring = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTMessageType05 WHERE FTID='" & HI.UL.ULF.rpQuoted(FTID) & "'  AND FDInsDate='" & FDInsDate & "' AND FTInsTime='" & FTInsTime & "'"
                cmdstring &= vbCrLf & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTData05(FDInsDate,FTInsTime,FTID,FTControlData,FTClientIP,FNTimeSewing,FNTotalStitches,FNAVGSpeed,FNRunTime,FNStopTime,FNTimeBetweenStartEnd)"
                cmdstring &= vbCrLf & " SELECT  '" & FDInsDate & "'"
                cmdstring &= vbCrLf & " ,'" & FTInsTime & "'"
                cmdstring &= vbCrLf & " ,'" & FTID & "'"
                cmdstring &= vbCrLf & " ,N'" & ControlData & "'"
                cmdstring &= vbCrLf & " ,'" & ClientIP & "'"
                cmdstring &= vbCrLf & " ,0,0,0,0,0,0"

                HI.Conn.SQLConn.ExecuteOnly(cmdstring, HI.Conn.DB.DataBaseName.DB_PROD)

            End If
        Catch ex As Exception

        End Try

    End Sub

End Class
