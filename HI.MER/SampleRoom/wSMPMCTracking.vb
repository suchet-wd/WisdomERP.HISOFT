Imports System.Data
Imports DevExpress.XtraGrid.GridControl
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns



Public Class wSMPMCTracking

    Private GridDataBefore As String = ""
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'SetColMaster()
      

    End Sub

#Region "Property"
    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(ByVal value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(ByVal value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(ByVal value As String)
            _CallMethodParm = value
        End Set
    End Property
#End Region


#Region "Procedure"

    Public Sub LoadDataInfo(OrderKey As String, OrderSubKey As String)
        Call LoadData()
    End Sub



    Private Sub LoadData()
        Dim Qry As String = ""


        Qry = "SELECT O.FTRDOperationCode,O.FTCmp AS Cmp,P.FTRDPositionPartCode,PP.FTPartCode,MO.FTRDMainOperationCode,M.FTRDMachineTypeCode,S.FTStyleCode,S.FTSeasonCode,S.FTFacProdTypeCode"
        Qry &= vbCrLf & ",ISNULL(S.GE,'0')AS GE,ISNULL(S.GSD,'0')AS GSD,ISNULL(S.SMV,'0')AS SMV,SUM(QQ.FNQuantity)AS FNQuantity,ISNULL((SUM(QQ.FNQuantity)*S.GE ),'0')AS TOTALGE,ISNULL((SUM(QQ.FNQuantity)*S.SMV ),'0')AS TOTALSMV"
        Qry &= vbCrLf & ",ISNULL((SUM(QQ.FNQuantity)*S.GSD ),'0')AS TOTALGSD"
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            Qry &= vbCrLf & ",O.FTRDOperationTH AS Description"
        Else
            Qry &= vbCrLf & ",O.FTRDOperationEN AS Description"
        End If
        Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMRDOperation AS O"

        Qry &= vbCrLf & "LEFT OUTER JOIN (SELECT DISTINCT O.FTRDOperationCode,SUM(GE.FNSamBF)AS GE,SUM(GSD.FNSamBF)AS GSD,SUM(SMV.FNSamBF)AS SMV,  ISNULL ((SELECT TOP 1 STUFF "
        Qry &= vbCrLf & "((SELECT  ', ' + t2.FTStyleCode"
        Qry &= vbCrLf & "FROM      (SELECT O.FTRDOperationCode,S.FTStyleCode"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMRDOperation AS O"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTGESam_Detail AS GE ON O.FNHSysRDOperationId=GE.FNHSysRDOperationId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S ON GE.FNHSysStyleId=S.FNHSysStyleId"
        Qry &= vbCrLf & "WHERE GE.FNHSysStyleId<>''"
        Qry &= vbCrLf & "UNION"
        Qry &= vbCrLf & "SELECT O.FTRDOperationCode,S.FTStyleCode"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMRDOperation AS O"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam_Detail AS GSD ON O.FNHSysRDOperationId=GSD.FNHSysRDOperationId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS SM ON GSD.FTSMPOrderNo=SM.FTSMPOrderNo"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S ON SM.FNHSysStyleId=S.FNHSysStyleId"
        Qry &= vbCrLf & "WHERE SM.FNHSysStyleId<>''"
        Qry &= vbCrLf & "UNION"
        Qry &= vbCrLf & "SELECT O.FTRDOperationCode,S.FTStyleCode"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMRDOperation AS O"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTSMVSam_Detail AS SMV ON O.FNHSysRDOperationId=SMV.FNHSysRDOperationId "
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S ON SMV.FNHSysStyleId=S.FNHSysStyleId"
        Qry &= vbCrLf & "WHERE SMV.FNHSysStyleId<>''"
        Qry &= vbCrLf & ") AS T2"
        Qry &= vbCrLf & " WHERE T2.FTRDOperationCode =O.FTRDOperationCode AND T2.FTStyleCode<>'' FOR XML PATH('')), 1, 2, '')  )"
        Qry &= vbCrLf & ",'') AS FTStyleCode  "

        Qry &= vbCrLf & " ,  ISNULL ((SELECT TOP 1 STUFF "
        Qry &= vbCrLf & "((SELECT  ', ' + t2.FTSeasonCode"
        Qry &= vbCrLf & "FROM      (SELECT O.FTRDOperationCode,S.FTSeasonCode"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMRDOperation AS O"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTGESam_Detail AS GE ON O.FNHSysRDOperationId=GE.FNHSysRDOperationId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS S ON GE.FNHSysSeasonId=S.FNHSysSeasonId"
        Qry &= vbCrLf & "WHERE GE.FNHSysSeasonId<>''"
        Qry &= vbCrLf & " UNION"
        Qry &= vbCrLf & "SELECT O.FTRDOperationCode,S.FTSeasonCode"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMRDOperation AS O"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam_Detail AS GSD ON O.FNHSysRDOperationId=GSD.FNHSysRDOperationId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS SM ON GSD.FTSMPOrderNo=SM.FTSMPOrderNo"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS S ON SM.FNHSysSeasonId=S.FNHSysSeasonId"
        Qry &= vbCrLf & "WHERE SM.FNHSysSeasonId<>''"
        Qry &= vbCrLf & "UNION"
        Qry &= vbCrLf & "SELECT O.FTRDOperationCode,S.FTSeasonCode"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMRDOperation AS O"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTSMVSam_Detail AS SMV ON O.FNHSysRDOperationId=SMV.FNHSysRDOperationId "
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS S ON SMV.FNHSysSeasonId=S.FNHSysSeasonId"
        Qry &= vbCrLf & "WHERE SMV.FNHSysSeasonId<>''"
        Qry &= vbCrLf & ") AS T2 "
        Qry &= vbCrLf & "WHERE T2.FTRDOperationCode =O.FTRDOperationCode AND T2.FTSeasonCode<>'' FOR XML PATH('')), 1, 2, '')  )"
        Qry &= vbCrLf & ",'') AS FTSeasonCode  "

        Qry &= vbCrLf & "  ,ISNULL ((SELECT TOP 1 STUFF "
        Qry &= vbCrLf & "((SELECT  ', ' + t2.FTFacProdTypeCode"
        Qry &= vbCrLf & "FROM      (SELECT O.FTRDOperationCode,M.FTFacProdTypeCode"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMRDOperation AS O"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTGESam_Detail AS GE ON O.FNHSysRDOperationId=GE.FNHSysRDOperationId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S ON GE.FNHSysStyleId=S.FNHSysStyleId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMFacProductType AS M ON S.FNHSysFacProdTypeId=M.FNHSysFacProdTypeId"
        Qry &= vbCrLf & "WHERE GE.FNHSysStyleId<>''"
        Qry &= vbCrLf & "UNION"
        Qry &= vbCrLf & "SELECT O.FTRDOperationCode,M.FTFacProdTypeCode"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMRDOperation AS O"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam_Detail AS GSD ON O.FNHSysRDOperationId=GSD.FNHSysRDOperationId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS SM ON GSD.FTSMPOrderNo=SM.FTSMPOrderNo"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S ON SM.FNHSysStyleId=S.FNHSysStyleId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMFacProductType AS M ON S.FNHSysFacProdTypeId=M.FNHSysFacProdTypeId"
        Qry &= vbCrLf & "WHERE SM.FNHSysStyleId<>''"
        Qry &= vbCrLf & "UNION"
        Qry &= vbCrLf & "SELECT O.FTRDOperationCode,M.FTFacProdTypeCode"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMRDOperation AS O"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTSMVSam_Detail AS SMV ON O.FNHSysRDOperationId=SMV.FNHSysRDOperationId "
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S ON SMV.FNHSysStyleId=S.FNHSysStyleId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMFacProductType AS M ON S.FNHSysFacProdTypeId=M.FNHSysFacProdTypeId"
        Qry &= vbCrLf & "WHERE SMV.FNHSysStyleId<>''"
        Qry &= vbCrLf & ") AS T2"
        Qry &= vbCrLf & "WHERE T2.FTRDOperationCode =O.FTRDOperationCode   FOR XML PATH('')), 1, 2, '')  )"
        Qry &= vbCrLf & ",'') AS FTFacProdTypeCode  "

        Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMRDOperation AS O"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTGESam_Detail AS GE ON O.FNHSysRDOperationId=GE.FNHSysRDOperationId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam_Detail AS GSD ON O.FNHSysRDOperationId=GSD.FNHSysRDOperationId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTSMVSam_Detail AS SMV ON O.FNHSysRDOperationId=SMV.FNHSysRDOperationId "
        Qry &= vbCrLf & "GROUP BY O.FTRDOperationCode"
        Qry &= vbCrLf & ")AS S ON O.FTRDOperationCode=S.FTRDOperationCode"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMRDMainOperation AS MO ON O.FNHSysRDMainOperationId=MO.FNHSysRDMainOperationId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMRDPositionPart AS P ON O.FNHSysRDPositionPartId=P.FNHSysRDPositionPartId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMRDMachineType AS M ON O.FNHSysRDMachineTypeId=M.FNHSysRDMachineTypeId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS  PP ON O.FNHSysPartId=PP.FNHSysPartId"

        Qry &= vbCrLf & " LEFT OUTER JOIN (SELECT O.FTRDOperationCode,SS.FTSeasonCode,S.FTStyleCode,SUM(OB.FNQuantity)AS FNQuantity"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMRDOperation AS O"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTGESam_Detail AS GE ON O.FNHSysRDOperationId=GE.FNHSysRDOperationId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S ON GE.FNHSysStyleId=S.FNHSysStyleId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS SS ON GE.FNHSysSeasonId=SS.FNHSysSeasonId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS OD ON GE.FNHSysStyleId=OD.FNHSysStyleId AND GE.FNHSysSeasonId=OD.FNHSysSeasonId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_BreakDown AS OB ON OD.FTOrderNo=OB.FTOrderNo"
        Qry &= vbCrLf & "WHERE GE.FNHSysStyleId<>''"
        Qry &= vbCrLf & "GROUP BY O.FTRDOperationCode,S.FTStyleCode,SS.FTSeasonCode"
        Qry &= vbCrLf & "  UNION"
        Qry &= vbCrLf & "SELECT DISTINCT O.FTRDOperationCode,S.FTSeasonCode,S.FTStyleCode,S.FNQuantity"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMRDOperation AS O"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam_Detail AS GSD ON O.FNHSysRDOperationId=GSD.FNHSysRDOperationId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS SM ON GSD.FTSMPOrderNo=SM.FTSMPOrderNo"
        Qry &= vbCrLf & "LEFT OUTER JOIN (SELECT S.FTStyleCode,SS.FTSeasonCode,S.FNHSysStyleId,SS.FNHSysSeasonId,SUM(OB.FNQuantity)AS FNQuantity"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS OD "
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_BreakDown AS OB ON OD.FTOrderNo=OB.FTOrderNo"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S ON OD.FNHSysStyleId=S.FNHSysStyleId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS SS ON OD.FNHSysSeasonId=SS.FNHSysSeasonId"
        Qry &= vbCrLf & "WHERE S.FNHSysStyleId<>''"
        Qry &= vbCrLf & "GROUP BY S.FTStyleCode,SS.FTSeasonCode,S.FNHSysStyleId,SS.FNHSysSeasonId)AS S ON SM.FNHSysStyleId=S.FNHSysStyleId AND SM.FNHSysSeasonId=S.FNHSysSeasonId"
        Qry &= vbCrLf & "WHERE SM.FNHSysStyleId<>''"
        Qry &= vbCrLf & " UNION"
        Qry &= vbCrLf & "SELECT O.FTRDOperationCode,SS.FTSeasonCode,S.FTStyleCode,SUM(OB.FNQuantity)AS FNQuantity"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMRDOperation AS O"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTSMVSam_Detail AS SMV ON O.FNHSysRDOperationId=SMV.FNHSysRDOperationId "
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S ON SMV.FNHSysStyleId=S.FNHSysStyleId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS SS ON SMV.FNHSysSeasonId=SS.FNHSysSeasonId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS OD ON SMV.FNHSysStyleId=OD.FNHSysStyleId AND SMV.FNHSysSeasonId=OD.FNHSysSeasonId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_BreakDown AS OB ON OD.FTOrderNo=OB.FTOrderNo"
        Qry &= vbCrLf & "WHERE SMV.FNHSysStyleId<>''"
        Qry &= vbCrLf & "GROUP BY O.FTRDOperationCode,S.FTStyleCode,SS.FTSeasonCode)AS QQ ON O.FTRDOperationCode=QQ.FTRDOperationCode"
        Qry &= vbCrLf & "WHERE S.FTStyleCode<>''"

        If Me.FTCmp.Text <> "" Then
            Qry &= vbCrLf & "    AND O.FTCmp ='" & HI.UL.ULF.rpQuoted(Me.FTCmp.Text) & "' "
        End If
        If Me.FNHSysRDPositionPartId.Text <> "" Then
            Qry &= vbCrLf & "    AND P.FTRDPositionPartCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysRDPositionPartId.Text) & "' "
        End If
        If Me.FNHSysPartId.Text <> "" Then
            Qry &= vbCrLf & "    AND PP.FTPartCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysPartId.Text) & "' "
        End If
        If Me.FNHSysRDMainOperationId.Text <> "" Then
            Qry &= vbCrLf & "    AND MO.FTRDMainOperationCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysRDMainOperationId.Text) & "' "
        End If
        If Me.FNHSysRDMachineTypeId.Text <> "" Then
            Qry &= vbCrLf & "    AND M.FTRDMachineTypeCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysRDMachineTypeId.Text) & "' "
        End If
        If Me.FNHSysStyleId.Text <> "" Then
            Qry &= vbCrLf & "    AND (S.FTStyleCode LIKE N'%" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "%' )"
        End If

        Qry &= vbCrLf & " GROUP BY O.FTRDOperationCode,O.FTCmp,P.FTRDPositionPartCode,PP.FTPartCode,MO.FTRDMainOperationCode,M.FTRDMachineTypeCode,S.FTStyleCode,S.FTSeasonCode,S.FTFacProdTypeCode"
        Qry &= vbCrLf & ",S.GE,S.GSD,S.SMV"
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            Qry &= vbCrLf & ",O.FTRDOperationTH "
        Else
            Qry &= vbCrLf & ",O.FTRDOperationEN "
        End If
        Qry &= vbCrLf & "ORDER BY (SUM(QQ.FNQuantity)*S.SMV ) DESC"

        

        'ogcDetail.DataSource = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_SAMPLE)
        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_SAMPLE)
        Me.ogcDetail.DataSource = _dt
        _dt.Dispose()
    End Sub
#End Region

#Region "Event"
    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub
    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        'If Verify() Then
        Dim _Spls As New TL.SplashScreen("Please Wait.....Loading Data")
        LoadData()
        _Spls.Close()
        'Else
        'If ST.Lang.Language = ST.Lang.eLang.TH Then
        '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
        'Else
        '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
        'End If

        'End If
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click

        TL.HandlerControl.ClearControl(Me)
        'Me.FNSMPOrderType.Text = ""
    End Sub
#End Region

    Public Function Verify() As Boolean
        'If Me.FTSMPOrderNo.Text <> "" Or Me.FTSMPOrderNoTo.Text <> "" Or Me.FTStartShipment.Text <> "" Or Me.FTEndShipment.Text <> "" Or Me.FNHSysStyleId.Text <> "" Or Me.FNHSysCustId.Text <> "" Or Me.FNHSysSeasonId.Text <> "" Or Me.FNHSysMerTeamId.Text <> "" Or Me.CFDSMPOrderDate.Text <> "" Or Me.FDSMPOrderDateEnd.Text <> "" Or Me.FNSMPOrderType.Text <> "" Or Me.FTPgm.Text <> "" Or Me.FNHSysSuplId.Text <> "" Or Me.FTStateReceiptDate.Text <> "" Or Me.FTStateReceiptDateEnd.Text <> "" Then

        '    Return True
        'Else
        '    Return False
        'End If
    End Function



    Private Sub wSMPOrderStatusTrack_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        'With Me.ogvDetail
        '    .Columns.ColumnByFieldName("FTProGacDate").OptionsColumn.AllowEdit = (Me.ocmsave.Enabled)
        '    .Columns.ColumnByFieldName("FDSuplCfmDliDate").OptionsColumn.AllowEdit = (Me.ocmsave.Enabled)

        '    '.Columns.ColumnByFieldName("FTProGacDate").OptionsColumn.AllowEdit = False
        '    '.Columns.ColumnByFieldName("FDSuplCfmDliDate").OptionsColumn.AllowEdit = False
        '    .OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect
        '    .OptionsSelection.MultiSelect = False

        'End With
        'Me.FNSMPOrderType.Text = ""


    End Sub



  
    Private Sub ogvDetail_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs)

        'Select Case e.Column.FieldName
        '    Case "FTProGacDate", "FDSuplCfmDliDate"

        '        If e.DisplayText = "01/01/0001" Then
        '            e.DisplayText = ""
        '        End If
        'End Select

    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs)
        Call LoadData()
    End Sub
    Private Function CheckProcess(key As String, Optional showmsg As Boolean = True) As Boolean
        Dim stateprocess As Boolean = False

        Dim cmd As String = ""

        'cmd = "select top 1 FE.FTStateFree from(select F.FTStateFree,F.FTSMPOrderNo from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_FabricMatList AS F with(nolock) union select M.FTStateFree,M.FTSMPOrderNo from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_MatList AS M )AS FE where FE.FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"

        stateprocess = (HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_SAMPLE, "") = "1")

        If stateprocess Then

            If showmsg Then
                HI.MG.ShowMsg.mInfo("พบข้อมูลบันทึก จบ กระบวนการทำงานแล้ว ไม่สามารถลบหรือแก้ไขได้ !!!", 1809210046, Me.Text, key, System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        End If

        Return stateprocess
    End Function


End Class