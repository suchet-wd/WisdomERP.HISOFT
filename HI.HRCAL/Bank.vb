Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography
Imports System.Web

Imports System.Globalization
Imports System.Threading
Imports System.Globalization.DateTimeFormatInfo
Imports Microsoft.VisualBasic
Imports System.Math
Imports System.Text

Public Class Bank
    Public Enum Bank As Integer
        NONE = 0
        BBL = 1 ' ธนาคารกรุงเทพ
        TMB = 2 ' ธนาคารทหารไทย
        TBANK = 3 ' ธนาคารธนชาต
        SCIB = 4 ' ธนาคารนครหลวง
    End Enum

    Public Shared Function Export(_BankCode As String, _CmpCode As String, _EmpType As String(), _
                                    _Year As String, _Term As String, _Period As String, _DateStart As String, _DateEnd As String, _DatePay As String, BankExport As Bank, ByRef MsgShow As String) As DataTable


        Dim _File As New DataTable
        Dim _Qry As String = ""

        Dim _DtPayRoll As DataTable
        Dim _Dt As DataTable

        Dim _StateExport As Boolean = False
        Select Case BankExport
            Case Bank.BBL, Bank.TMB, Bank.TBANK, Bank.SCIB
                _StateExport = True
        End Select

        If (_StateExport) Then

            Dim _ComName As String = ""
            Dim _ComID As String = ""
            Dim _ComTaxID As String = ""
            Dim _ComBnkBranchID As String = ""
            Dim _ComAcc As String = ""
            Dim _GAmount As Double = 0
            Dim _TotalRec As Integer = 0

            _Qry = "    SELECT TOP 1 FTCmpCode,   FNHSysCmpTitleId AS  FTCompanyTitle, FTCmpNameEN As FTCmpNamePri, FTCmpNameTH As FTCmpNameSec"
            _Qry &= vbCrLf & "  ,FTTaxNo AS FTCmpTaxNo, FTSocialNo AS FTCmpTSocialNo,FNHSysBankId AS  FTBnkCode "
            _Qry &= vbCrLf & "  ,FTBankBranchCode AS  FTBnkBchCode, FTDepositCode, FTBnkAccNo, FTBnkAccName, FTBchSocial"
            _Qry &= vbCrLf & "  ,FTCmpNameEN AS FTCmpAddrPri1,FTCmpNameTH AS FTCmpAddrPri2"
            _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH (NOLOCK) "
            _Qry &= vbCrLf & " WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "
            _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

            For Each R As DataRow In _Dt.Rows

                _ComName = R!FTBnkAccName.ToString
                _ComID = R!FTDepositCode.ToString
                _ComAcc = R!FTBnkAccNo.ToString.Replace("-", "")
                _ComTaxID = R!FTCmpTaxNo.ToString
                _ComBnkBranchID = R!FTBnkBchCode.ToString

            Next
            'REPLACE(REPLACE(MyField, CHAR(13), ''), CHAR(10), '')
            _Qry = "  SELECT * FROM ( SELECT         M.FTEmpCode, M.FTEmpCodeRefer, M.FTEmpPreCode"
            '_Qry &= vbCrLf & ", M.FTEmpName1,"
            '_Qry &= vbCrLf & " M.FTEmpSurname1"
            '_Qry &= vbCrLf & ", M.FTEmpNickname1"
            '_Qry &= vbCrLf & ", M.FTEmpName2"
            '_Qry &= vbCrLf & ", M.FTEmpSurname2"
            '_Qry &= vbCrLf & ", M.FTEmpNickname2"
            _Qry &= vbCrLf & ",REPLACE(REPLACE(M.FTEmpName1, CHAR(13), ''), CHAR(10), '') AS  FTEmpName1,"
            _Qry &= vbCrLf & " REPLACE(REPLACE(M.FTEmpSurname1, CHAR(13), ''), CHAR(10), '') AS FTEmpSurname1"
            _Qry &= vbCrLf & ", M.FTEmpNickname1"
            _Qry &= vbCrLf & ",REPLACE(REPLACE(M.FTEmpName2, CHAR(13), ''), CHAR(10), '') AS  FTEmpName2"
            _Qry &= vbCrLf & ",REPLACE(REPLACE(M.FTEmpSurname2, CHAR(13), ''), CHAR(10), '') AS FTEmpSurname2"
            _Qry &= vbCrLf & ", M.FTEmpNickname2"
            _Qry &= vbCrLf & " , P.FTPayYear, P.FTPayTerm,  P.FTEmpIdNo, P.FNHSysEmpTypeId,"
            _Qry &= vbCrLf & " P.FTPayDate, "
            _Qry &= vbCrLf & " B.FTBankCode,P.FNHSysBankBranchId AS   FTBranchCode, P.FTAccNo,"
            _Qry &= vbCrLf & " P.FNSalary AS FCSalary , "
            _Qry &= vbCrLf & " P.FNNetpay AS FNNetpay"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll AS P WITH (NOLOCK)"
            _Qry &= vbCrLf & "  INNER JOIN ("
            _Qry &= vbCrLf & "  SELECT      MAX(Emp.FTEmpCode) AS FTEmpCode, MAX(Emp.FTEmpCodeRefer) AS FTEmpCodeRefer, MAX(PN.FTPreNameCode ) As FTEmpPreCode"
            _Qry &= vbCrLf & " , Max(Emp.FTEmpNameEN) As FTEmpName1"
            _Qry &= vbCrLf & " , MAX(Emp.FTEmpSurnameEN) As FTEmpSurname1"
            _Qry &= vbCrLf & " , MAX(Emp.FTEmpNicknameEN) As FTEmpNickname1"
            _Qry &= vbCrLf & " , MAX(Emp.FTEmpNameTH) AS FTEmpName2"
            _Qry &= vbCrLf & " , MAX(Emp.FTEmpSurnameTH) As FTEmpSurname2"
            _Qry &= vbCrLf & " , Max(Emp.FTEmpNicknameTH) AS  FTEmpNickname2 "
            _Qry &= vbCrLf & " , Emp.FNHSysEmpID"
            _Qry &= vbCrLf & " FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Emp WITH (NOLOCK)"
            _Qry &= vbCrLf & " LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS PN WITH(NOLOCK)"
            _Qry &= vbCrLf & " ON Emp.FNHSysPreNameId = PN.FNHSysPreNameId"
            _Qry &= vbCrLf & " WHERE  Emp.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " AND FNHSysEmpTypeId <> 0  "

            _Qry = HI.ST.Security.PermissionEmpType(_Qry)

            _Qry &= vbCrLf & "  GROUP BY FNHSysEmpID"
            _Qry &= vbCrLf & " ) AS M ON P.FNHSysEmpID = M.FNHSysEmpID  "
            _Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS PD WITH(NOLOCK) ON  P.FNHSysEmpTypeId = PD.FNHSysEmpTypeId "
            _Qry &= vbCrLf & " AND P.FTPayTerm = PD.FTPayTerm "
            _Qry &= vbCrLf & " AND P.FTPayYear = PD.FTPayYear "
            _Qry &= vbCrLf & " LEFT JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMBank AS B WITH(NOLOCK)"
            _Qry &= vbCrLf & " ON P.FNHSysBankId = B.FNHSysBankId"
            _Qry &= vbCrLf & "    WHERE (P.FNHSysEmpTypeId <> 0)"
            _Qry &= vbCrLf & " AND (P.FNHSysPayRollPayId IN (SELECT FNHSysPayRollPayId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPaymentType AS PM WITH(NOLOCK) WHERE FTStatePackBank='1'  ) )"

            If _EmpType.Length > 0 Then
                _Qry &= vbCrLf & " AND ( "
                Dim Seq As Integer = 0

                For Each Str As String In _EmpType
                    If Str <> "" Then
                        If Seq = 0 Then
                            _Qry &= vbCrLf & "  P.FNHSysEmpTypeId =" & Val(Str) & " "
                        Else
                            _Qry &= vbCrLf & " OR P.FNHSysEmpTypeId =" & Val(Str) & " "
                        End If

                        Seq = Seq + 1
                    End If

                Next

                _Qry &= vbCrLf & " ) "

            End If

            _Qry &= vbCrLf & " AND  PD.FTPayYear ='" & HI.UL.ULF.rpQuoted(_Year) & "' "
            _Qry &= vbCrLf & " AND  PD.FTPayTerm ='" & HI.UL.ULF.rpQuoted(_Term) & "'  "
            _Qry &= vbCrLf & " AND (B.FTBankCode = N'" & HI.UL.ULF.rpQuoted(_BankCode) & "')"
            _Qry &= vbCrLf & " ) AS TM"
            _Qry &= vbCrLf & " WHERE FNNetpay > 0"

            _DtPayRoll = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            _TotalRec = _DtPayRoll.Rows.Count

            For Each R As DataRow In _DtPayRoll.Rows
                _GAmount = _GAmount + Val(R!FNNetpay.ToString)
            Next

            _GAmount = CDbl(Format(_GAmount, "0.00"))

            If _DtPayRoll.Rows.Count > 0 Then

                Select Case BankExport
                    Case Bank.BBL
                        _File = ExportBBL(_CmpCode, _ComName, _ComID, _ComAcc, _ComTaxID, _ComBnkBranchID, _EmpType, _TotalRec, _GAmount, _Year, _Term, _DatePay, _DtPayRoll)
                    Case Bank.TMB
                        _File = ExportTMB(_CmpCode, _ComName, _ComID, _ComAcc, _ComTaxID, _ComBnkBranchID, _EmpType, _TotalRec, _GAmount, _Year, _Term, _DatePay, _DtPayRoll)
                    Case Bank.TBANK
                        _File = ExportTBANK(_CmpCode, _ComName, _ComID, _ComAcc, _ComTaxID, _ComBnkBranchID, _EmpType, _TotalRec, _GAmount, _Year, _Term, _DatePay, _DtPayRoll)
                    Case Bank.SCIB
                        _File = ExportSCIB(_CmpCode, _ComName, _ComID, _ComAcc, _ComTaxID, _ComBnkBranchID, _EmpType, _TotalRec, _GAmount, _Year, _Term, _DatePay, _DtPayRoll)
                    Case Else
                End Select

                MsgShow = "Total Record : " & (Format(_TotalRec, "#,#0"))
                MsgShow &= vbCrLf & "Total Amount : " & (Format(_GAmount, "#,#0.00"))

            End If
        End If

        Return _File
    End Function

    Private Shared Function GenerateTable(_MaxCol As Integer) As DataTable
        Dim _Dt As New DataTable

        For I = 1 To _MaxCol
            _Dt.Columns.Add("C" & I.ToString, GetType(String))
        Next

        Return _Dt
    End Function

    Private Shared Function ExportTMB(_CmpCode As String, _ComName As String, _ComID As String, _ComAcc As String, _ComTaxID As String, _ComBnkBranchID As String, _EmpType As String(), _
                                   _TotalRec As Integer, _GAmount As Double, _Year As String, _Term As String, _DatePay As String, _PayRoll As DataTable) As DataTable

        Dim _File As DataTable = GenerateTable(16)
        Dim Ind As Integer = 1
        Dim _Amt As Integer = 0
        Dim _Arr() As String
        Dim _EmpName As String

        Dim _PayDate As String = Left(_DatePay, 2) & Mid(_DatePay, 4, 2) & Right(_DatePay, 2)
        Dim _EmpPayDate As String = Right(_DatePay, 2) & Mid(_DatePay, 4, 2) & Left(_DatePay, 2)

        _Arr = {"H",
                Ind.ToString("".PadRight(6, "0")),
                "011",
                Left(_ComAcc, 10) & "".PadRight(10 - Len(Left(_ComAcc, 10)), " "),
                Left(_ComName, 25) & "".PadRight(25 - Len(Left(_ComName, 25)), " "),
                Left(_PayDate, 6) & "".PadRight(6 - Len(Left(_PayDate, 6)), " "),
                "000001",
                "".PadRight(71, " ")}

        _File.Rows.Add(_Arr)

        For Each R As DataRow In _PayRoll.Rows
            Ind = Ind + 1

            _Amt = Integer.Parse(Format((CDbl(Format(Val(R!FNNetpay.ToString), "0.00")) * 100), "0"))
            _EmpName = R!FTEmpName2.ToString & " " & R!FTEmpSurname2.ToString

            _Arr = {"D",
               Ind.ToString("".PadRight(6, "0")),
               "011",
               Left(R!FTAccNo.ToString.Replace("-", ""), 10) & "".PadRight(10 - Len(Left(R!FTAccNo.ToString.Replace("-", ""), 10)), " "),
               "C",
                _Amt.ToString("".PadRight(10, "0")),
               "08",
               "9",
               "".PadRight(9, "0") & "1",
               Left(_EmpPayDate, 6) & "".PadRight(6 - Len(Left(_EmpPayDate, 6)), " "),
               Left(_ComID, 4) & "".PadRight(4 - Len(Left(_ComID, 4)), " "),
               "001",
                "".PadRight(19, "0") & "2",
                "".PadRight(6, " "),
                "".PadRight(10, " "),
               Left(_EmpName, 35) & "".PadRight(35 - Len(Left(_EmpName, 35)), " ")}

            _File.Rows.Add(_Arr)

        Next

        Ind = Ind + 1
        _Amt = Integer.Parse(Format((_GAmount * 100), "0"))

        _Arr = {"T",
              Ind.ToString("".PadRight(6, "0")),
              "011",
              Left(_ComAcc, 10) & "".PadRight(10 - Len(Left(_ComAcc, 10)), " "),
              0.ToString("".PadRight(7, "0")),
              0.ToString("".PadRight(13, "0")),
              _TotalRec.ToString("".PadRight(7, "0")),
              _Amt.ToString("".PadRight(13, "0")),
              "".PadRight(7, "0"),
              "".PadRight(13, "0"),
              "".PadRight(7, "0"),
              "".PadRight(13, "0"),
              "".PadRight(28, " ")}
        _File.Rows.Add(_Arr)

        Return _File
    End Function

    Private Shared Function ExportBBL(_CmpCode As String, _ComName As String, _ComID As String, _ComAcc As String, _ComTaxID As String, _ComBnkBranchID As String, _EmpType As String(), _
                                   _TotalRec As Integer, _GAmount As Double, _Year As String, _Term As String, _DatePay As String, _PayRoll As DataTable) As DataTable

        Dim _File As DataTable = GenerateTable(56)
        Dim Ind As Integer = 1
        Dim _Amt As Double = 0.0
        Dim _Arr() As String
        Dim _EmpName As String

        Dim _PayDate As String = Left(_DatePay, 2) & Mid(_DatePay, 4, 2) & Right(_DatePay, 4)
        _ComBnkBranchID = "batref"
        _Arr = {"001" & "~",
                 Left(_ComID, 20) & "~",
                 Left(_ComTaxID, 15) & "~",
                Left(_ComAcc, 20) & "~",
                Left(_ComBnkBranchID, 25) & "",
                 "~",
                "~",
                Left(_PayDate, 8) & "~",
                Format(Now, "HH:mm:ss").Replace(":", "")}

        _File.Rows.Add(_Arr)

        Ind = 0
        For Each R As DataRow In _PayRoll.Rows
            Ind = Ind + 1

            _Amt = CDbl(Format((CDbl(Format(Val(R!FNNetpay.ToString), "0.00")))))
            _EmpName = R!FTEmpName2.ToString & " " & R!FTEmpSurname2.ToString

            _Arr = {"003" & "~",
                    Left(_ComID, 20) & "~",
                    Ind.ToString & "~",
                    "PYR01" & "~",
                    Left(R!FTAccNo.ToString.Trim().Replace("-", ""), 25) & "~",
                    Left(_PayDate, 8) & "~",
                    "~",
                    "THB" & "~",
                    "ref" & (11110 + Ind).ToString & "~",
                    "~",
                    "~",
                    "~",
                    "~",
                    "~",
                    "~",
                    "~",
                    "~",
                    "~",
                    "N" & "~",
                    "~",
                    "~",
                     "~",
                    "~",
                    "~",
                    "~",
                    "~",
                    "01~",
                    "~",
                    "~",
                    "~",
                    "~",
                    "~",
                    "OUR" & "~",
                    _Amt.ToString.Replace(".", "") & "~",
                    "~",
                    "~",
                    "~",
                    Left("002" & R!FTBankCode.ToString, 3) & "~",
                    Right("0000" & R!FTBranchCode.ToString, 4) & "~",
                    "~",
                    "~",
                    "~",
                    "~",
                    Left(_EmpName, 100) & "",
                    "~",
                    "~",
                    "~",
                    "~",
                    "~",
                    "~",
                    "~",
                    "~",
                    "~",
                    "~",
                    "~",
                    "~"}

            _File.Rows.Add(_Arr)

        Next

        _Amt = _GAmount

        _Arr = {"100" & "~",
              Ind.ToString & "~",
              _Amt.ToString.Replace(".", "") & ""}

        _File.Rows.Add(_Arr)

        Return _File
    End Function

    Private Shared Function ExportTBANK(_CmpCode As String, _ComName As String, _ComID As String, _ComAcc As String, _ComTaxID As String, _ComBnkBranchID As String, _EmpType As String(), _
                                  _TotalRec As Integer, _GAmount As Double, _Year As String, _Term As String, _DatePay As String, _PayRoll As DataTable) As DataTable

        Dim _File As DataTable = GenerateTable(1)
        Dim Ind As Integer = 1
        Dim _Amt As Integer = 0
        Dim _EmpName As String

        Dim _PayDate As String = Left(_DatePay, 2) & Mid(_DatePay, 4, 2) & Right(_DatePay, 2)
        '---- Header
        Dim _H As New TBANKFMT.Header
        With _H
            .F01_Space = ""
            .F02_TermPayment = "M"
            .F03_CmpCode = _ComID
            .F04_CmpName = _ComName
            .F05_ApplyCode = "D"
            .F07_EffDate = _PayDate
            .F08_TotalRecord = _TotalRec
            .F09_TotalAmount = Format((CDbl(Format(_GAmount, "0.00")) * 100), "0") 'Format(_GAmount, "000")
            .F10_MediaType = "I"
            .F11_LastSpace = ""
        End With

        With _H
            _File.Rows.Add(.F01_Space &.F02_TermPayment &.F03_CmpCode & .F04_CmpName &.F05_ApplyCode &.F06_Status &.F07_EffDate &.F08_TotalRecord &.F09_TotalAmount &.F10_MediaType &.F11_LastSpace)
        End With
       
        '---- Header

        '---- Detail
        For Each R As DataRow In _PayRoll.Rows
            Ind = Ind + 1
            _Amt = Integer.Parse(Format((CDbl(Format(Val(R!FNNetpay.ToString), "0.00")) * 100), "0"))
            _EmpName = R!FTEmpName2.ToString & " " & R!FTEmpSurname2.ToString

            Dim _D As New TBANKFMT.Detail
            With _D
                .F01_AccountNo = R!FTAccNo.ToString
                .F02_EffDate = _PayDate
                .F03_AmountSign = "0"
                .F04_Amount = _Amt
                .F05_EmpName = _EmpName
                .F06_StatusReturn = ""
                .F07_LastSpace = ""
            End With

            With _D
                _File.Rows.Add(.F01_AccountNo & .F02_EffDate & .F03_AmountSign &.F04_Amount &.F05_EmpName &.F06_StatusReturn &.F07_LastSpace)
            End With

        Next

        '---- Detail
        Return _File

    End Function

    Private Shared Function ExportSCIB(_CmpCode As String, _ComName As String, _ComID As String, _ComAcc As String, _ComTaxID As String, _ComBnkBranchID As String, _EmpType As String(), _
                               _TotalRec As Integer, _GAmount As Double, _Year As String, _Term As String, _DatePay As String, _PayRoll As DataTable) As DataTable

        Dim _File As DataTable = GenerateTable(1)
        Dim Ind As Integer = 1
        Dim _Amt As Integer = 0
        Dim _Arr() As String
        Dim _EmpName As String

        ' _DatePay ="dd/MM/yyyy"
        Dim _PayDate As String = Right(_DatePay, 2) & Mid(_DatePay, 4, 2) & Left(_DatePay, 2)
        '---- Header
        Dim _H As New SCIBFMT.Header
        With _H
            .F03_CmpCode = _ComID
            .F05_TotalAmount = "0"
            .F07_EffDate = _PayDate
           
        End With

        With _H
            _File.Rows.Add(.F01_RecType & .F02_RecSeq & .F03_CmpCode & .F04_AccountNo & .F05_TotalAmount & .F06_LastSpace & .F07_EffDate)
        End With

        '---- Header

        '---- Detail
        Ind = 1
        For Each R As DataRow In _PayRoll.Rows
            Ind = Ind + 1
            _Amt = Format(Val(R!FNNetpay.ToString), "0.00").Replace(".", "")
            _EmpName = R!FTEmpName2.ToString & " " & R!FTEmpSurname2.ToString

            Dim _D As New SCIBFMT.Detail
            With _D
                .F02_RecSeq = Ind.ToString
                .F03_CmpCode = _ComID
                .F04_AccountNo = R!FTAccNo.ToString
                .F05_Amount = _Amt
                .F07_EffDate = _PayDate     
            End With

            With _D
                _File.Rows.Add(.F01_RecType & .F02_RecSeq & .F03_CmpCode & .F04_AccountNo & .F05_Amount & .F06_LastSpace & .F07_EffDate)
            End With

        Next
        '---- Detail


        'Footer
        Dim _F As New SCIBFMT.Footer
        With _F
            .F02_RecSeq = Ind.ToString
            .F03_CmpCode = _ComID
            .F05_TotalAmount = Format(_GAmount, "0.00").Replace(".", "")
            .F07_EffDate = _PayDate

        End With

        With _F
            _File.Rows.Add(.F01_RecType & .F02_RecSeq & .F03_CmpCode & .F04_AccountNo & .F05_TotalAmount & .F06_LastSpace & .F07_EffDate)
        End With

        'Footer

        Return _File

    End Function


End Class


