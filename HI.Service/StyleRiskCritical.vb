Public Class StyleRiskCritical
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


        Dim cmdstring As String = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_QACRITICAL "
        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PROD)

        otpcritical.TabPages.Clear()
        Dim I As Integer = 0

        For Each R As DataRow In dt.Rows

            I = I + 1

            Dim Otp As New DevExpress.XtraTab.XtraTabPage()
            With Otp
                .Name = "Tabpage" & Microsoft.VisualBasic.Right("0000000000000" & I.ToString, 10)
                .Text = R!FTCmpCode.ToString & "-" & R!FTUnitSectCode.ToString
            End With

            Dim DataCri As New QACritical(Val(R!FNHSysCmpId.ToString), R!FTCmpCode.ToString, R!FNCriticalType.ToString, R!FDQADate.ToString, R!FTUnitSectCode.ToString, R!FTStyleCode.ToString, R!FTPORef.ToString, R!FTOrderNo.ToString, Val(R!FNQuantity.ToString), Val(R!FNNo.ToString), Val(R!FNHSysQADetailId.ToString), R!FTQADetailCode.ToString, R!FTQAName.ToString, R!FTPointName.ToString, Val(R!FNHSysStyleId.ToString), R!FTHourNo.ToString, Val(R!FNHSysUnitSectId.ToString))
            Otp.Controls.Add(DataCri)
            DataCri.Dock = DockStyle.Fill
            otpcritical.TabPages.Add(Otp)

        Next

    End Sub
End Class