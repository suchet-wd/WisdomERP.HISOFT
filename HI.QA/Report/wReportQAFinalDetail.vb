Imports DevExpress.XtraGrid.Columns

Public Class wReportQAFinalDetail

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Call InitialGridMergCell()

    End Sub


    Private Sub LoadDetail()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

            _Cmd = ""
            _Cmd &= vbCrLf & " Declare @ColName nvarchar(max) , @ColNameSum nvarchar(max)"
            _Cmd &= vbCrLf & "Select @ColName= ISNULL(@ColName + ',','')"
            _Cmd &= vbCrLf & "+  QUOTENAME(FTPointName)"
            _Cmd &= vbCrLf & "From (SELECT      Distinct LEFT(C.FTPointSubName,CHARINDEX('-',C.FTPointSubName)-1) AS FTPointName"

            _Cmd &= vbCrLf & "FROM         V_TPRODTQAPreFinal_H AS A LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "                      V_TPRODTQAPreFinal_Detail_D  AS B ON A.FNHSysStyleId = B.FNHSysStyleId AND A.FNHSysUnitSectId = B.FNHSysUnitSectId AND A.FDQADate = B.FDQADate AND A.FTOrderNo = B.FTOrderNo AND "
            _Cmd &= vbCrLf & "                    A.FNHourNo = B.FNHourNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "                     TPRODTQAPreFinal_SubDetail AS C ON A.FNHSysStyleId = C.FNHSysStyleId AND A.FNHSysUnitSectId = C.FNHSysUnitSectId AND A.FTOrderNo = C.FTOrderNo AND A.FDQADate = C.FDQADate"
            _Cmd &= vbCrLf & "   LEFT OUTER JOIN   HITECH_MASTER.dbo.TCNMUnitSect AS D ON A.FNHSysUnitSectId = D.FNHSysUnitSectId  "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN HITECH_MASTER.dbo.TMERMStyle AS E ON A.FNHSysStyleId = E.FNHSysStyleId "

            _Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS OD ON A.FTOrderNo = OD.FTOrderNo "
            _Cmd &= vbCrLf & " WHERE OD.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & ""

            _Cmd &= vbCrLf & " AND     (A.FDQADate >= '" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "') and (A.FDQADate <= '" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "') and C.FTPointSubName is not null "

            If Me.FNHSysStyleId.Text <> "" And FNHSysStyleIdTo.Text <> "" Then
                _Cmd &= vbCrLf & " and (E.FTStyleCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "') and  (E.FTStyleCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleIdTo.Text) & "') "
            End If

            If Me.FNHSysUnitSectId.Text <> "" And FNHSysUnitSectIdTo.Text <> "" Then
                _Cmd &= vbCrLf & " and (D.FTUnitSectCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "') and (D.FTUnitSectCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "')  "
            End If

            If Me.FTOrderNo.Text <> "" And FTOrderNoTo.Text <> "" Then
                _Cmd &= vbCrLf & " and (A.FTOrderNo >='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "') and (A.FTOrderNo <='" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "')"
            End If

            _Cmd &= vbCrLf & ") AS T"

            _Cmd &= vbCrLf & "select @ColNameSum = STUFF((SELECT  ', Max(isnull(' + QUOTENAME(FTPointName)+', 0)) AS ' +QUOTENAME(FTPointName)"
            _Cmd &= vbCrLf & "From (SELECT      Distinct LEFT(C.FTPointSubName,CHARINDEX('-',C.FTPointSubName)-1) AS FTPointName"

            _Cmd &= vbCrLf & "FROM         V_TPRODTQAPreFinal_H AS A LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "       V_TPRODTQAPreFinal_Detail_D  AS B ON A.FNHSysStyleId = B.FNHSysStyleId AND A.FNHSysUnitSectId = B.FNHSysUnitSectId AND A.FDQADate = B.FDQADate AND A.FTOrderNo = B.FTOrderNo AND "
            _Cmd &= vbCrLf & "        A.FNHourNo = B.FNHourNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "         TPRODTQAPreFinal_SubDetail AS C ON A.FNHSysStyleId = C.FNHSysStyleId AND A.FNHSysUnitSectId = C.FNHSysUnitSectId AND A.FTOrderNo = C.FTOrderNo AND A.FDQADate = C.FDQADate"
            _Cmd &= vbCrLf & "   LEFT OUTER JOIN   HITECH_MASTER.dbo.TCNMUnitSect AS D ON A.FNHSysUnitSectId = D.FNHSysUnitSectId  "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN HITECH_MASTER.dbo.TMERMStyle AS E ON A.FNHSysStyleId = E.FNHSysStyleId "

            _Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS OD ON A.FTOrderNo = OD.FTOrderNo "
            _Cmd &= vbCrLf & " WHERE OD.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & ""

            _Cmd &= vbCrLf & " AND     (A.FDQADate >= '" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "') and (A.FDQADate <= '" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "')  and C.FTPointSubName is not null "
            If Me.FNHSysStyleId.Text <> "" And FNHSysStyleIdTo.Text <> "" Then
                _Cmd &= vbCrLf & " and (E.FTStyleCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "') and  (E.FTStyleCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleIdTo.Text) & "') "
            End If
            If Me.FNHSysUnitSectId.Text <> "" And FNHSysUnitSectIdTo.Text <> "" Then
                _Cmd &= vbCrLf & " and (D.FTUnitSectCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "') and (D.FTUnitSectCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "')  "
            End If
            If Me.FTOrderNo.Text <> "" And FTOrderNoTo.Text <> "" Then
                _Cmd &= vbCrLf & " and (A.FTOrderNo >='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "') and (A.FTOrderNo <='" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "')"
            End If
            _Cmd &= vbCrLf & ") AS T"
            _Cmd &= vbCrLf & " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,1,'')"

            _Cmd &= vbCrLf & "   BEGIN Try"
            _Cmd &= vbCrLf & "  drop Table #TmpBk"
            _Cmd &= vbCrLf & "     End Try"
            _Cmd &= vbCrLf & "  Begin Catch"
            _Cmd &= vbCrLf & "  END Catch"


            _Cmd &= vbCrLf & "   SELECT   COUNT(*) AS Qty,  A.FNHSysStyleId, A.FTOrderNo, A.FDQADate,  A.FNHSysQADetailId, B.FTUnitSectCode"
            _Cmd &= vbCrLf & ",LEFT(A.FTPointSubName,CHARINDEX('-',A.FTPointSubName)-1) AS FTPointName,B.FNHSysUnitSectId"
            _Cmd &= vbCrLf & "INTO #TmpBk"
            _Cmd &= vbCrLf & "FROM    TPRODTQAPreFinal_SubDetail AS A INNER JOIN"
            _Cmd &= vbCrLf & "                      HITECH_MASTER.dbo.TCNMUnitSect AS B ON A.FNHSysUnitSectId = B.FNHSysUnitSectId"
            '_Cmd &= vbCrLf & "WHERE     (B.FTUnitSectCode = 'SWA08') AND (A.FDQADate = '2015/02/14')"
            _Cmd &= vbCrLf & "group by A.FNHSysStyleId, A.FTOrderNo, A.FDQADate,  A.FNHSysQADetailId, B.FTUnitSectCode"
            _Cmd &= vbCrLf & ",LEFT(A.FTPointSubName,CHARINDEX('-',A.FTPointSubName)-1),B.FNHSysUnitSectId"

            '_Cmd &= vbCrLf & "SELECT     FDQADate, FNHSysUnitSectId, FTOrderNo, FNHSysStyleId, Colorway,  FNHSysQADetailId, FTPointName, max(FNDefectQty) AS FNDefectQty"
            '_Cmd &= vbCrLf & ",count(*) AS Qty"
            '_Cmd &= vbCrLf & "INTO #TmpBk"
            '_Cmd &= vbCrLf & "FROM         (SELECT     A.FDQADate, A.FNHSysUnitSectId, A.FTOrderNo, A.FNHSysStyleId, dbo.FN_GET_ColorWay(A.FTOrderNo, A.FNHSysUnitSectId) AS Colorway, A.FNMajorQty, A.FNMinorQty, "
            '_Cmd &= vbCrLf & "                               C.FNHSysQADetailId, LEFT(C.FTPointSubName, CHARINDEX('-', C.FTPointSubName) - 1) AS FTPointName, SUM(A.FNMajorQty + A.FNMinorQty) AS FNDefectQty"
            '_Cmd &= vbCrLf & "        FROM          TPRODTQA AS A LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & "                                 TPRODTQA_Detail AS B ON A.FNHSysStyleId = B.FNHSysStyleId AND A.FNHSysUnitSectId = B.FNHSysUnitSectId AND A.FDQADate = B.FDQADate AND A.FTOrderNo = B.FTOrderNo AND "
            '_Cmd &= vbCrLf & "                                  A.FNHourNo = B.FNHourNo LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & "                                   TPRODTQA_SubDetail AS C ON A.FNHSysStyleId = C.FNHSysStyleId AND A.FNHSysUnitSectId = C.FNHSysUnitSectId AND A.FTOrderNo = C.FTOrderNo AND "
            '_Cmd &= vbCrLf & "   A.FDQADate = C.FDQADate"
            '_Cmd &= vbCrLf & "              GROUP BY A.FDQADate, A.FNHSysUnitSectId, A.FTOrderNo, A.FNHSysStyleId, A.FNMajorQty, A.FNMinorQty, C.FNHSysQADetailId, LEFT(C.FTPointSubName, CHARINDEX('-', C.FTPointSubName) - 1)"
            '_Cmd &= vbCrLf & "             having SUM(A.FNMajorQty + A.FNMinorQty) > 0) "
            '_Cmd &= vbCrLf & "            AS T"
            '_Cmd &= vbCrLf & "group by FDQADate, FNHSysUnitSectId, FTOrderNo, FNHSysStyleId, Colorway,   FNHSysQADetailId, FTPointName"



            _Cmd &= vbCrLf & "Declare @Str nvarchar(max)"
            _Cmd &= vbCrLf & "Set @Str = 'Select  FDQADate,FNHSysUnitSectId,FTOrderNo,FNHSysStyleId,Colorway,FNQAInQty,FNQAActualQty"
            _Cmd &= vbCrLf & "                      , FNHSysQADetailId ,'+@ColName+'"
            _Cmd &= vbCrLf & "INTO #Tmp"
            _Cmd &= vbCrLf & "From(SELECT     A.FDQADate, A.FNHSysUnitSectId, A.FTOrderNo, A.FNHSysStyleId" ', dbo.FN_GET_ColorWay(A.FTOrderNo, A.FNHSysUnitSectId) AS Colorway
            _Cmd &= vbCrLf & " , STUFF(("
            _Cmd &= vbCrLf & "    SELECT '','' + FTColorway"
            _Cmd &= vbCrLf & "     From 	( SELECT   distinct B.FTColorway  "
            _Cmd &= vbCrLf & "        	FROM       [HITECH_PRODUCTION].dbo.TPRODBarcodeScanOutline AS X WITH(NOLOCK)    "
            _Cmd &= vbCrLf & "          	 INNER JOIN  [HITECH_PRODUCTION].dbo.TPRODTOrder_CustBarcode AS B WITH(NOLOCK) ON X.FTBarcodeCustRef = B.FTCustBarcodeNo "
            _Cmd &= vbCrLf & "      WHERE  X.FNHSysUnitSectId=A.FNHSysUnitSectId "
            _Cmd &= vbCrLf & "      AND X.FTOrderNo=A.FTOrderNo)  as T     FOR XML PATH('''')    ), 1, 1, '''') as  Colorway "

            _Cmd &= vbCrLf & "              ,max(A.FNQAInQty) AS FNQAInQty, max(A.FNQAActualQty)"
            _Cmd &= vbCrLf & "             AS FNQAActualQty,   C.FNHSysQADetailId , LEFT(C.FTPointSubName,CHARINDEX(''-'',C.FTPointSubName)-1) AS FTPointName"
            _Cmd &= vbCrLf & "               ,(Select Top 1 Qty From #TmpBk"
            _Cmd &= vbCrLf & "    where FDQADate = A.FDQADate And FNHSysUnitSectId = A.FNHSysUnitSectId And FTOrderNo = A.FTOrderNo"
            _Cmd &= vbCrLf & "                 and FNHSysStyleId = A.FNHSysStyleId  and   FNHSysQADetailId = C.FNHSysQADetailId   "
            _Cmd &= vbCrLf & " and       FTPointName  =LEFT(C.FTPointSubName,CHARINDEX(''-'',C.FTPointSubName)-1) "
            _Cmd &= vbCrLf & "                 ) AS FNDefectQty"
            _Cmd &= vbCrLf & "FROM         V_TPRODTQAPreFinal_H AS A  WITH(NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "               V_TPRODTQAPreFinal_Detail_D  AS B  WITH(NOLOCK) ON A.FNHSysStyleId = B.FNHSysStyleId AND A.FNHSysUnitSectId = B.FNHSysUnitSectId AND A.FDQADate = B.FDQADate AND A.FTOrderNo = B.FTOrderNo AND "
            _Cmd &= vbCrLf & "                 A.FNHourNo = B.FNHourNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "                   TPRODTQAPreFinal_SubDetail AS C  WITH(NOLOCK) ON A.FNHSysStyleId = C.FNHSysStyleId AND A.FNHSysUnitSectId = C.FNHSysUnitSectId AND A.FTOrderNo = C.FTOrderNo AND A.FDQADate = C.FDQADate"
            _Cmd &= vbCrLf & "   LEFT OUTER JOIN   HITECH_MASTER.dbo.TCNMUnitSect AS D  WITH(NOLOCK)  ON A.FNHSysUnitSectId = D.FNHSysUnitSectId  "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN HITECH_MASTER.dbo.TMERMStyle AS E  WITH(NOLOCK)  ON A.FNHSysStyleId = E.FNHSysStyleId "


            _Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS OD ON A.FTOrderNo = OD.FTOrderNo "
            _Cmd &= vbCrLf & " WHERE OD.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & ""

            _Cmd &= vbCrLf & "  AND      (A.FDQADate >=''" & HI.UL.ULDate.ConvertEnDB(Me.FDSDate.Text) & "'') and (A.FDQADate <= ''" & HI.UL.ULDate.ConvertEnDB(Me.FDEDate.Text) & "'') "

            If Me.FNHSysStyleId.Text <> "" And FNHSysStyleIdTo.Text <> "" Then
                _Cmd &= vbCrLf & " and (E.FTStyleCode >=''" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'') and  (E.FTStyleCode <=''" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleIdTo.Text) & "'') "
            End If
            If Me.FNHSysUnitSectId.Text <> "" And FNHSysUnitSectIdTo.Text <> "" Then
                _Cmd &= vbCrLf & " and (D.FTUnitSectCode >=''" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "'') and (D.FTUnitSectCode <=''" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "'')  "
            End If
            If Me.FTOrderNo.Text <> "" And FTOrderNoTo.Text <> "" Then
                _Cmd &= vbCrLf & " and (A.FTOrderNo >=''" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'') and (A.FTOrderNo <=''" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'')"
            End If


            _Cmd &= vbCrLf & "Group by  A.FDQADate, A.FNHSysUnitSectId, A.FTOrderNo, A.FNHSysStyleId"
            _Cmd &= vbCrLf & "                  ,   C.FNHSysQADetailId"
            _Cmd &= vbCrLf & "                  ,LEFT(C.FTPointSubName,CHARINDEX(''-'',C.FTPointSubName)-1)  "
            _Cmd &= vbCrLf & "                   ) AS T"
            _Cmd &= vbCrLf & "pivot (sum(FNDefectQty) for FTPointName in ('+@ColName+') ) AS Piv "
            _Cmd &= vbCrLf & "Select  Convert(varchar(10),Convert(datetime,FDQADate) ,103) AS  FDQADate,FNHSysUnitSectId,FTOrderNo,FNHSysStyleId,Colorway"
            _Cmd &= vbCrLf & "					,(Select Count(*) AS Qty FROM       V_TPRODTQAPreFinal_Detail_D "
            _Cmd &= vbCrLf & "           Where FTStateReject <> 0"
            _Cmd &= vbCrLf & "					and FDQADate = A.FDQADate and FNHSysUnitSectId = A.FNHSysUnitSectId "
            _Cmd &= vbCrLf & "                      and FTOrderNo = A.FTOrderNo"
            _Cmd &= vbCrLf & "                     and FNHSysStyleId = A.FNHSysStyleId"
            _Cmd &= vbCrLf & "				) AS FTDefectBody "
            _Cmd &= vbCrLf & "                     , (Select count(*) AS Qty From TPRODTQAPreFinal_SubDetail"
            _Cmd &= vbCrLf & "         where FDQADate = A.FDQADate And FNHSysUnitSectId = A.FNHSysUnitSectId"
            _Cmd &= vbCrLf & "                     and FTOrderNo = A.FTOrderNo"
            _Cmd &= vbCrLf & "                    and FNHSysStyleId = A.FNHSysStyleId"
            _Cmd &= vbCrLf & "                   and FNHSysQADetailId = A.FNHSysQADetailId"
            _Cmd &= vbCrLf & "                    ) AS FTDefect  , FNHSysQADetailId   "
            _Cmd &= vbCrLf & "                  ,(SELECT   sum(FNQAInQty) AS FNQAInQty  "
            _Cmd &= vbCrLf & "        FROM V_TPRODTQAPreFinal_H   WITH(NOLOCK) "
            _Cmd &= vbCrLf & "         where FDQADate = A.FDQADate And FNHSysUnitSectId = A.FNHSysUnitSectId"
            _Cmd &= vbCrLf & "                   and FTOrderNo = A.FTOrderNo"
            _Cmd &= vbCrLf & "                  and FNHSysStyleId = A.FNHSysStyleId ) AS FNQAInQty"
            _Cmd &= vbCrLf & "                   ,(SELECT   sum(FNQAActualQty) AS FNQAActualQty  "
            _Cmd &= vbCrLf & "        FROM V_TPRODTQAPreFinal_H  WITH(NOLOCK) "
            _Cmd &= vbCrLf & "        where FDQADate = A.FDQADate And FNHSysUnitSectId = A.FNHSysUnitSectId"
            _Cmd &= vbCrLf & "and FTOrderNo = A.FTOrderNo"
            _Cmd &= vbCrLf & " and FNHSysStyleId = A.FNHSysStyleId ) AS FNQAActualQty ,'+@ColNameSum+'"

            _Cmd &= vbCrLf & ",(Select   FTQADetailCode From  HITECH_MASTER.dbo.TQAMQADetail  WITH(NOLOCK)  Where FNHSysQADetailId = A.FNHSysQADetailId) AS  FTQADetailCode"
            _Cmd &= vbCrLf & ",(Select "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & "FTQADetailNameTH AS FTQADetailName"
            Else
                _Cmd &= vbCrLf & "FTQADetailNameEN AS FTQADetailName"
            End If
            _Cmd &= vbCrLf & " From  HITECH_MASTER.dbo.TQAMQADetail  WITH(NOLOCK) Where  FNHSysQADetailId = A.FNHSysQADetailId ) AS FTQADetailName"
            _Cmd &= vbCrLf & ",(Select Top 1  FTUnitSectCode FROM         HITECH_MASTER.dbo.TCNMUnitSect  WITH(NOLOCK)  Where FNHSysUnitSectId = A.FNHSysUnitSectId) AS FTUnitSectCode"
            _Cmd &= vbCrLf & ",(Select Top 1 FTStyleCode FROM         HITECH_MASTER.dbo.TMERMStyle  WITH(NOLOCK)  Where FNHSysStyleId = A.FNHSysStyleId ) AS FTStyleCode "

            _Cmd &= vbCrLf & "From #Tmp AS A"
            _Cmd &= vbCrLf & " where isnull(A.FNHSysQADetailId , 0) > 0  "
            _Cmd &= vbCrLf & "Group by    FDQADate,FNHSysUnitSectId,FTOrderNo,FNHSysStyleId,Colorway"
            _Cmd &= vbCrLf & ",  FNHSysQADetailId '"
            _Cmd &= vbCrLf & "EXEC sp_executesql @Str"

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            Dim _colcount As Integer = 0
            With Me.ogvDetail

                For I As Integer = .Columns.Count - 1 To 0 Step -1

                    Select Case .Columns(I).FieldName.ToString.ToUpper

                        Case "FDQADate".ToUpper, "FNHSysUnitSectId".ToUpper, "FTOrderNo".ToUpper, "FNHSysStyleId".ToUpper, "Colorway".ToUpper _
                                  , "FNHSysQADetailId".ToUpper, "Colorway".ToUpper, "FTDefectBody".ToUpper, "FTDefect".ToUpper, "FNQAInQty".ToUpper, "FNQAActualQty".ToUpper, "FTUnitSectCode".ToUpper, "FTStyleCode".ToUpper, "FTQADetailName".ToUpper, "FTQADetailCode".ToUpper
                        Case Else
                            .Columns.Remove(.Columns(I))
                    End Select

                Next

                If Not (_oDt Is Nothing) Then
                    For Each Col As DataColumn In _oDt.Columns

                        Select Case Col.ColumnName.ToString.ToUpper
                            Case "FDQADate".ToUpper, "FNHSysUnitSectId".ToUpper, "FTOrderNo".ToUpper, "FNHSysStyleId".ToUpper, "Colorway".ToUpper _
                                , "FNHSysQADetailId".ToUpper, "Colorway".ToUpper, "FTDefectBody".ToUpper, "FNQAInQty".ToUpper, "FNQAActualQty".ToUpper, "FTUnitSectCode".ToUpper, "FTStyleCode".ToUpper, "FTQADetailName".ToUpper, "FTQADetailCode".ToUpper
                                .Columns(Col.ColumnName.ToString).OptionsColumn.AllowEdit = False
                                .Columns(Col.ColumnName.ToString).OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.Default
                            Case "FTDefect".ToUpper
                                .Columns(Col.ColumnName.ToString).OptionsColumn.AllowEdit = False
                                .Columns(Col.ColumnName.ToString).OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False

                            Case Else
                                _colcount = _colcount + 1
                                Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                                With ColG
                                    .Visible = True
                                    .FieldName = Col.ColumnName.ToString
                                    .Name = "c" & Col.ColumnName.ToString
                                    .Caption = Col.ColumnName.ToString

                                End With

                                .Columns.Add(ColG)

                                With .Columns(Col.ColumnName.ToString)

                                    .OptionsFilter.AllowAutoFilter = False
                                    .OptionsFilter.AllowFilter = False
                                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                    .DisplayFormat.FormatString = "{0:n0}"
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                                    With .OptionsColumn
                                        .AllowMove = False
                                        .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                        .AllowEdit = False
                                        .ReadOnly = True
                                        .AllowMerge = DevExpress.Utils.DefaultBoolean.False
                                    End With

                                End With

                                .Columns(Col.ColumnName.ToString).Width = 45
                                .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                                .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

                        End Select

                    Next

                    'For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                    '    With GridCol
                    '        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    '    End With
                    'Next

                End If

            End With

            Me.ogcDetail.DataSource = _oDt
            _oDt.Dispose()
            _Spls.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If Me.FDSDate.Text = "" Or Me.FDEDate.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FDSDate_lbl.Text)
                If Me.FDSDate.Text = "" Then Me.FDSDate.Focus()
                If Me.FDEDate.Text = "" Then Me.FDEDate.Focus()
                Exit Sub
            End If
            Me.LoadDetail()
        Catch ex As Exception

        End Try
    End Sub



    Private Sub InitialGridMergCell()

        For Each c As GridColumn In ogvDetail.Columns

            Select Case c.FieldName.ToString
                Case "FDQADate".ToUpper, "FTOrderNo".ToUpper, "Colorway".ToUpper _
                                , "Colorway".ToUpper, "FTDefectBody".ToUpper, "FNQAInQty".ToUpper _
                               , "FNQAActualQty".ToUpper, "FTUnitSectCode".ToUpper, "FTStyleCode".ToUpper

                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                Case Else
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End Select
        Next
    End Sub

    Private Sub ogvDetail_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvDetail.CellMerge
        Try
            With Me.ogvDetail


                If .GetRowCellValue(e.RowHandle1, "FTUnitSectCode").ToString = .GetRowCellValue(e.RowHandle2, "FTUnitSectCode").ToString Then


                    If e.Column.FieldName = "FNQAActualQty" Then
                        If .GetRowCellValue(e.RowHandle1, "FDQADate").ToString = .GetRowCellValue(e.RowHandle2, "FDQADate").ToString _
                            And .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString _
                              And .GetRowCellValue(e.RowHandle1, "FTStyleCode").ToString = .GetRowCellValue(e.RowHandle2, "FTStyleCode").ToString _
                              And .GetRowCellValue(e.RowHandle1, "FTUnitSectCode").ToString = .GetRowCellValue(e.RowHandle2, "FTUnitSectCode").ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True

                        Else

                            e.Merge = False
                            e.Handled = True
                        End If
                    End If


                    'If e.Column.FieldName = "FNQuantity" Then
                    '    If .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString Then
                    '        e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                    '        e.Handled = True
                    '    Else
                    '        e.Merge = False
                    '        e.Handled = True

                    '    End If
                    'Else
                    'End If
                Else
                    e.Merge = False
                    e.Handled = True
                End If



            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub wReportQADetail_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvDetail)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub
End Class