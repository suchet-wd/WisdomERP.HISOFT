Public Class wCostSheetTracking
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Dim Qry As String = ""
        Dim dt As DataTable
        If VerifyField() Then

            Dim _Spls As New HI.TL.SplashScreen("Loading...Data Please wait.")
            Try

                'Qry = "   SELECT       CFS.FTSeason,CFS.FTStyleCode,CFS.FTInsUser AS ImportBy,convert(varchar(10),convert(datetime,CFS.FDInsDate),103) AS DatImport"
                'Qry &= vbCrLf & ",OS.FTSubOrderNo,OS.FTColorway,OS.FTSizeBreakDown,OS.FNPrice AS FOB"
                'Qry &= vbCrLf & ",CFS.FNImportCMP,CFS.FNFabricAmt,CFS.FNAccessoryAmt"
                'Qry &= vbCrLf & ", CFS.FNImportFabricAmt, CFS.FNImportAccessoryAmt"

                'Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheetFirstSale AS CFS WITH(NOLOCK) LEFT OUTER JOIN"
                'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS Sea WITH(NOLOCK) ON CFS.FTSeason=Sea.FTSeasonCode LEFT OUtER JOIN"
                'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S WITH(NOLOCK) ON CFS.FTStyleCode=S.FTStyleCode LEFT OUTER JOIN"
                'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON S.FNHSysStyleId=O.FNHSysStyleId and Sea.FNHSysSeasonId=O.FNHSysSeasonId LEFT OUTER JOIN"
                'Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS OS WITH(NOLOCK) ON  O.FTOrderNo=OS.FTOrderNo"

                '------modify by joker 2016/08/09 13.37

                Qry &= vbCrLf & "SELECT   DISTINCT    mas.FTSeasonCode as FTSeason,mas.FTStyleCode,isnull(CFS.FTInsUser,'') AS ImportBy,isnull(convert(varchar(10),convert(datetime,CFS.FDInsDate),103),'') AS DatImport"
                Qry &= vbCrLf & ",OS.FTSubOrderNo,OS.FTColorway,OS.FTSizeBreakDown,isnull(OS.FNPriceOrg,0) AS FOB"
                Qry &= vbCrLf & ",isnull(CFS.FNImportCMP,0) AS FNImportCMP,isnull(CFS.FNFabricAmt,0) AS FNFabricAmt,isnull(CFS.FNAccessoryAmt,0) AS FNAccessoryAmt"
                Qry &= vbCrLf & ",isnull(CFS.FNImportFabricAmt,0) AS FNImportFabricAmt,isnull(CFS.FNImportAccessoryAmt,0) AS FNImportAccessoryAmt"
                Qry &= vbCrLf & ",isnull(CFS.FNCMPOrg,isnull(CMP.FNCM,isnull(S.FNCM,0))) AS FNCM"
                'Qry &= vbCrLf & ",isnull(CMP.FNCM,isnull(S.FNCM,0)) AS FNCM"
                Qry &= vbCrLf & ",ISNULL(CMP.FNCMDisPer,S.FNCMDisPer ) AS FNCMDisPer"
                Qry &= vbCrLf & ",ISNULL(CMP.FNCMDisAmt,S.FNCMDisAmt ) AS FNCMDisAmt"
                Qry &= vbCrLf & ",ISNULL(CMP.FNNetCM,ISNULL(S.FNNetCM,0) ) AS FNNetCM"
                Qry &= vbCrLf & ",isnull(X.FTPOref,'') AS FTCustomerPO"
                Qry &= vbCrLf & ",isnull(MI.FTInvoiceNo,'') AS FTInvoiceNo"
                Qry &= vbCrLf & "FROM"
                Qry &= vbCrLf & "(SELECT DISTINCT O.FNHSysStyleId,O.FNHSysSeasonId,S.FTStyleCode,Sea.FTSeasonCode "
                Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK)INNER jOIN"
                Qry &= vbCrLf & "" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle AS S WITH(NOLOCK) ON  O.FNHSysStyleId=S.FNHSysStyleId INNER jOIN"
                Qry &= vbCrLf & "" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMSeason AS Sea WITH(NOLOCK) ON O.FNHSysSeasonId=Sea.FNHSysSeasonId"
                Qry &= vbCrLf & " WHERE o.FNHSysSeasonId>0 and o.FNHSysStyleId>0"

                If Me.FNHSysSeasonId.Text <> "" Then
                    Qry &= vbCrLf & " AND o.FNHSysSeasonId>=" & HI.UL.ULF.rpQuoted(Me.FNHSysSeasonId.Properties.Tag) & " "
                End If

                If Me.FNHSysSeasonIdTo.Text <> "" Then
                    Qry &= vbCrLf & " AND o.FNHSysSeasonId<=" & HI.UL.ULF.rpQuoted(Me.FNHSysSeasonIdTo.Properties.Tag) & " "
                End If

                If Me.FNHSysStyleId.Text <> "" Then
                    Qry &= vbCrLf & " AND o.FNHSysStyleId>=" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Properties.Tag) & ""
                End If

                If Me.FNHSysStyleIdTo.Text <> "" Then
                    Qry &= vbCrLf & " AND o.FNHSysStyleId<=" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleIdTo.Properties.Tag) & ""
                End If

                Qry &= vbCrLf & " union"
                Qry &= vbCrLf & " SELECT CMP.FNHSysStyleId,CMP.FNHSysSeasonId,S.FTStyleCode,Sea.FTSeasonCode "
                Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTSeasonCMPrice CMP WITH(NOLOCK) INNER jOIN"
                Qry &= vbCrLf & "" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle AS S WITH(NOLOCK) ON  CMP.FNHSysStyleId=S.FNHSysStyleId INNER jOIN"
                Qry &= vbCrLf & "" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMSeason AS Sea WITH(NOLOCK) ON CMP.FNHSysSeasonId=Sea.FNHSysSeasonId"
                Qry &= vbCrLf & " WHERE CMP.FNHSysSeasonId>0 and CMP.FNHSysStyleId>0"

                If Me.FNHSysSeasonId.Text <> "" Then
                    Qry &= vbCrLf & "AND CMP.FNHSysSeasonId>=" & HI.UL.ULF.rpQuoted(Me.FNHSysSeasonId.Properties.Tag) & " "
                End If

                If Me.FNHSysSeasonIdTo.Text <> "" Then
                    Qry &= vbCrLf & "AND CMP.FNHSysSeasonId<=" & HI.UL.ULF.rpQuoted(Me.FNHSysSeasonIdTo.Properties.Tag) & " "
                End If

                If Me.FNHSysStyleId.Text <> "" Then
                    Qry &= vbCrLf & "AND CMP.FNHSysStyleId>=" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Properties.Tag) & ""
                End If

                If Me.FNHSysStyleIdTo.Text <> "" Then
                    Qry &= vbCrLf & "AND CMP.FNHSysStyleId<=" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleIdTo.Properties.Tag) & ""
                End If

                Qry &= vbCrLf & ") AS Mas LeFT OUTER JOIN"

                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON Mas.FNHSysStyleId=O.FNHSysStyleId and Mas.FNHSysSeasonId=O.FNHSysSeasonId LEFT OUtER JOIN"
                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS Sea WITH(NOLOCK) ON O.FNHSysSeasonId= Sea.FNHSysSeasonId LEFT OUtER JOIN"
                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S WITH(NOLOCK) ON O.FNHSysStyleId=S.FNHSysStyleId LEFT OUTER JOIN"
                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheetFirstSale AS CFS WITH(NOLOCK) ON Mas.FTSeasonCode=CFS.FTSeason and Mas.FTStyleCode=CFS.FTStyleCode LEFT OUTER JOIN"
                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTSeasonCMPrice AS CMP WITH(NOLOCK) ON Mas.FNHSysSeasonId=CMP.FNHSysSeasonId and Mas.FNHSysStyleId=CMP.FNHSysStyleId LEFT OUTER JOIN"
                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS OS WITH(NOLOCK) ON  O.FTOrderNo=OS.FTOrderNo LEFT OUTER JOIN"
                Qry &= vbCrLf & "(SELECT FTOrderNo, FTPOref,FTColorway,FTSizeBreakDown,FTSubOrderNo"
                Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination"
                Qry &= vbCrLf & "GROUP BY FTOrderNo, FTPOref,FTColorway,FTSizeBreakDown,FTSubOrderNo"
                Qry &= vbCrLf & ") AS X ON OS.FTOrderNo = X.FTOrderNo and OS.FTSizeBreakDown=x.FTSizeBreakDown and OS.FTColorway=X.FTColorway and OS.FTSubOrderNo=X.FTSubOrderNo"
                Qry &= vbCrLf & "LEFT OUTER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo.TACCTFactoryCMInvoice_D AS MI WITH(NOLOCK) ON x.FTPOref=MI.FTCustomerPO and x.FTColorway=mi.FTColorway and x.FTSizeBreakDown=mi.FTSizeBreakDown"

                'Qry &= vbCrLf & "WHERE mas.FNHSysSeasonId>0 and mas.FNHSysStyleId>0"

                'If Me.FNHSysSeasonId.Text <> "" Then
                '    Qry &= vbCrLf & "AND Mas.FTSeasonCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSeasonId.Text) & "' "
                'End If

                'If Me.FNHSysSeasonIdTo.Text <> "" Then
                '    Qry &= vbCrLf & "AND Mas.FTSeasonCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSeasonIdTo.Text) & "' "
                'End If

                'If Me.FNHSysStyleId.Text <> "" Then
                '    Qry &= vbCrLf & "AND Mas.FTStyleCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "' "
                'End If

                'If Me.FNHSysStyleIdTo.Text <> "" Then
                '    Qry &= vbCrLf & "AND Mas.FTStyleCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleIdTo.Text) & "' "
                'End If

                dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MERCHAN)
                ogcCostsheet.DataSource = dt

            Catch ex As Exception

            End Try

            _Spls.Close()
        End If

    End Sub

    Private Sub ocmclearclsr_Click(sender As Object, e As EventArgs) Handles ocmclearclsr.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Function VerifyField() As Boolean

        If Me.FNHSysSeasonId.Text <> "" Or Me.FNHSysSeasonIdTo.Text <> "" Or Me.FNHSysStyleId.Text <> "" Or Me.FNHSysStyleIdTo.Text <> "" Then
            Return True
        Else
            Return False
        End If

    End Function

End Class