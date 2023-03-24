using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HI.Auditor
{
    public class CreateLog
    {
        public static void CreateLogdata(DataTable original, DataTable changed, string frmName, string tableName, string refDocKey = null) 
        {
            //MyUser original = new MyUser(ID, "Joe", "Bloggs", "joe.bloggs@test.com");
            //MyUser changed = new MyUser(ID, "John", "Bloggs", "john.bloggs@test.com", "Line 1", "Line 2", "City", "ZipCode");
            
            //DataTable original = new DataTable();
            //DataTable changed = new DataTable();      

            Guid id;
            string refKey;
            try
            {
                id = new Guid(original.Rows[0]["guid"].ToString());
            }
            catch
            {
                id = new Guid();                
            }

            if (refDocKey != null || refDocKey != "")
                refKey = refDocKey;
            else
                refKey = id.ToString();


            if (original.Rows.Count < changed.Rows.Count) {
                int rowmax = changed.Rows.Count;
                original.BeginInit();
                for (int i = original.Rows.Count; i < rowmax; i++)
                {
                    original.Rows.Add();
                }
                original.EndInit(); 
            }
            else if (original.Rows.Count > changed.Rows.Count) {

                int rowmax = original.Rows.Count;
                changed.BeginInit();
                for (int i = changed.Rows.Count; i < rowmax; i++)
                {
                    changed.Rows.Add();
                }
                changed.EndInit(); 

            }

            ChangeLogger logger = new ChangeLogger(refKey, original, changed);
            if (!logger.Success)
            {
                //Console.Write(logger.Exception.ToString());
                MessageBox.Show(logger.Exception.ToString());
                return;
            }

            // You can also change the value of the changed class before calling Audit
            //changed.LastName = "Blo";

            logger.Audit();
            if (!logger.Success)
            {
                //Console.Write(logger.Exception.ToString());
                MessageBox.Show(logger.Exception.ToString());
                return;
            }

            //foreach (ChangeLog change in logger.Changes)
            //{
            //    //Console.Write(string.Format("{2} property changed from {3} to {4} for class {1} with id {0}\r\n", change.ObjectId, change.ObjectType, change.Property, change.ValueOld, change.ValueNew));
                
            //}

            WriteLog(logger, frmName, tableName);
        }

        private List<object> GetListByDataTable(DataTable dt)
        {
            var reult = (from rw in dt.AsEnumerable()
                     select new
                     {
                         Name = Convert.ToString(rw["Name"]),
                         ID = Convert.ToInt32(rw["ID"])
                     }).ToList();

            return reult.ConvertAll<object>(o => (object)o);
        }

        private static void WriteLog(ChangeLogger logger, string frmName, string tableName)
        {
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN);
            HI.Conn.SQLConn.SqlConnectionOpen();
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand();
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction();

            string UpdateDate = "";
            string UpdateTime = "";
            string StrSql = "SELECT " + HI.UL.ULDate.FormatDateDB + " AS UpdateDate, " + HI.UL.ULDate.FormatTimeDB + " AS UpdateTime";
            DataTable dtx = null;
            dtx = HI.Conn.SQLConn.GetDataTableOnbeginTrans(StrSql, Conn.DB.DataBaseName.DB_MERCHAN.ToString());

            if (dtx.Rows.Count > 0)
            {
                foreach (DataRow Rx in dtx.Rows)
                {
                    UpdateDate = (Rx["UpdateDate"]).ToString();
                    UpdateTime = (Rx["UpdateTime"]).ToString();
                }
            }

            SqlDataAdapter dataAdapter = CreateAdapter(HI.Conn.SQLConn.Cnn);
            SqlCommand InsertCMD = new SqlCommand(dataAdapter.InsertCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran);

            try
            {
                foreach (ChangeLog change in logger.Changes)
                {
                    // Write changed log to table;
                    InsertCMD.Parameters.AddWithValue("@FTFormName", frmName);
                    InsertCMD.Parameters.AddWithValue("@FTTableName", tableName);
                    InsertCMD.Parameters.AddWithValue("@FTChangeObject", change.Property);
                    InsertCMD.Parameters.AddWithValue("@FTChangeFrom", change.ValueOld);
                    InsertCMD.Parameters.AddWithValue("@FTChangeTo", change.ValueNew);
                    InsertCMD.Parameters.AddWithValue("@FTUpdUser", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName));
                    InsertCMD.Parameters.AddWithValue("@FDUpdDate", UpdateDate);
                    InsertCMD.Parameters.AddWithValue("@FTUpdTime", UpdateTime);
                    InsertCMD.Parameters.AddWithValue("@FTRefGUID", change.ValueGuid);
                    InsertCMD.Parameters.AddWithValue("@FTRefDocKey", change.ValueDocKey);
                    InsertCMD.Parameters.AddWithValue("@FNStateType", change.ValueChangeType );

                    InsertCMD.CommandType = CommandType.Text;
                    InsertCMD.ExecuteNonQuery();
                    InsertCMD.Parameters.Clear();
                }

                HI.Conn.SQLConn.Tran.Commit();
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran);
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd);
            }

            catch (Exception ex)
            {
                HI.Conn.SQLConn.Tran.Rollback();
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran);
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd);
                MessageBox.Show(ex.Message);
            }

        }

        private static SqlDataAdapter CreateAdapter(SqlConnection connection)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            
            //  Create the InsertCommand.
            SqlCommand command = new SqlCommand("INSERT INTO [" + HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_LOG) + "].[dbo].[HSysAuditLog] " +
                            "([FTFormName], [FTTableName], [FTChangeObject], [FTChangeFrom], [FTChangeTo], " +
                            "[FTRefGUID], [FTRefDocKey], [FTUpdUser], [FDUpdDate], [FTUpdTime],[FNStateType]) " +
                            "VALUES (@FTFormName, @FTTableName, @FTChangeObject, @FTChangeFrom, @FTChangeTo, " +
                            "@FTRefGUID, @FTRefDocKey, @FTUpdUser, @FDUpdDate, @FTUpdTime,@FNStateType)", connection);

            //  Add the parameters for the InsertCommand.
            command.Parameters.Add("@FTFormName", SqlDbType.NChar, 255, "[FTFormName]");
            command.Parameters.Add("@FTTableName", SqlDbType.NChar, 255, "[FTTableName]");
            command.Parameters.Add("@FTChangeObject", SqlDbType.NChar, 255, "[FTChangeObject]");
            command.Parameters.Add("@FTChangeFrom", SqlDbType.NChar, 255, "[FTChangeFrom]");
            command.Parameters.Add("@FTChangeTo", SqlDbType.NChar, 255, "[FTChangeTo]");
            command.Parameters.Add("@FTRefGUID", SqlDbType.UniqueIdentifier, 255, "[FTRefGUID]");
            command.Parameters.Add("@FTRefDocKey", SqlDbType.VarChar, 255, "[FTRefDocKey]");
            command.Parameters.Add("@FTUpdUser", SqlDbType.NChar, 255, "FTUpdUser");
            command.Parameters.Add("@FDUpdDate", SqlDbType.VarChar, 10, "FDUpdDate");
            command.Parameters.Add("@FTUpdTime", SqlDbType.VarChar, 8, "FTUpdTime");
            command.Parameters.Add("@FNStateType", SqlDbType.Int, 32, "FNStateType");
            adapter.InsertCommand = command;
            
            return adapter;
        }

        public static void CreateLogDelete(string pMenuName, string pFormName, string pQuery) {

            try
            {

                string StrSql = "";
                StrSql ="INSERT INTO [" +  HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LOG) +  "].dbo.HSysDeleteLog " ;
                StrSql += Environment.NewLine + " (FTDeleteUser, FDDeleteDate, FTDeleteTime, FTMnuName, FTFormName, FTQuery)  ";
                StrSql += Environment.NewLine + " SELECT '" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName )  + "'";
                StrSql += Environment.NewLine + "," + HI.UL.ULDate.FormatDateDB  + "";
                StrSql += Environment.NewLine + "," + HI.UL.ULDate.FormatTimeDB  + "";
                StrSql += Environment.NewLine + ",'" + HI.UL.ULF.rpQuoted(pMenuName) + "'";
                StrSql += Environment.NewLine + ",'" + HI.UL.ULF.rpQuoted(pFormName) + "'";
                StrSql += Environment.NewLine + ",'" + HI.UL.ULF.rpQuoted(pQuery) + "'";

                HI.Conn.SQLConn.ExecuteOnly(StrSql,Conn.DB.DataBaseName.DB_LOG );

            }

            catch
            {
               
            }
        }

        public static void CreateLogBomSheetStyleMat(int SysStyleId, int SysSeasonId)
        {

            try
            {

                string StrSql = "";
                StrSql = "INSERT INTO [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LOG) + "].dbo.TMERTStyle_Mat ";
                StrSql += Environment.NewLine + " (FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysStyleId, FNSeq, FNMerMatSeq";
                StrSql += Environment.NewLine + ", FNHSysMerMatId, FNPart, FTPositionPartName, FNHSysSuplId, FTStateNominate, FNHSysUnitId, ";
                StrSql += Environment.NewLine + "  FNPrice, FNHSysCurId, FNConSmp, FNConSmpPlus, FTOrderNo, FTSubOrderNo, FTStateActive";
                StrSql += Environment.NewLine + ", FTStateCombination, FTStateMainMaterial, FTPositionPartId, FTPart, FTComponent, FTStateFree, FNHSysSeasonId,";
                StrSql += Environment.NewLine + "  FNSeqRef, FNRunSeq, FTEditUser, FDEditDate, FTEditTime)  ";
                StrSql += Environment.NewLine + " SELECT FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysStyleId, FNSeq, FNMerMatSeq";
                StrSql += Environment.NewLine + ", FNHSysMerMatId, FNPart, FTPositionPartName, FNHSysSuplId, FTStateNominate, FNHSysUnitId, ";
                StrSql += Environment.NewLine + "  FNPrice, FNHSysCurId, FNConSmp, FNConSmpPlus, FTOrderNo, FTSubOrderNo, FTStateActive";
                StrSql += Environment.NewLine + ", FTStateCombination, FTStateMainMaterial, FTPositionPartId, FTPart, FTComponent, FTStateFree, FNHSysSeasonId,";
                StrSql += Environment.NewLine + "  FNSeqRef  ";
                StrSql += Environment.NewLine + ",ISNULL((";
                StrSql += Environment.NewLine + "SELECT TOP 1 FNRunSeq FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LOG) + "].dbo.TMERTStyle_Mat AS X WITH(NOLOCK)";
                StrSql += Environment.NewLine + "  WHERE X.FNHSysStyleId=" + SysStyleId + "";
                StrSql += Environment.NewLine + "  AND X.FNHSysSeasonId=" + SysSeasonId + "";
                StrSql += Environment.NewLine + " ORDER BY FNRunSeq DESC ),0) +1 AS FNRunSeq ";
                StrSql += Environment.NewLine + ",'" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                StrSql += Environment.NewLine + "," + HI.UL.ULDate.FormatDateDB + "";
                StrSql += Environment.NewLine + "," + HI.UL.ULDate.FormatTimeDB + "";
                StrSql += Environment.NewLine + " FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTStyle_Mat AS A WITH(NOLOCK) ";
                StrSql += Environment.NewLine + "  WHERE A.FNHSysStyleId=" + SysStyleId + "";
                StrSql += Environment.NewLine + "  AND A.FNHSysSeasonId=" + SysSeasonId + "";

                HI.Conn.SQLConn.ExecuteOnly(StrSql, Conn.DB.DataBaseName.DB_LOG);

            }
            catch
            {}
        }

        public static void CreateLogBomSheetStyleColorWay(int SysStyleId, int SysSeasonId)
        {

            try
            {

                string StrSql = "";
                StrSql = "INSERT INTO [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LOG) + "].dbo.TMERTStyle_ColorWay ";
                StrSql += Environment.NewLine + " (FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysStyleId, FNSeq, FNMerMatSeq, FNColorWaySeq, FTRunColor, FNHSysRawMatColorId, FNHSysMatColorId,  FTRawMatColorNameTH, FTRawMatColorNameEN, FNHSysSeasonId, FNRunSeq, FTEditUser, FDEditDate, FTEditTime)  ";
                StrSql += Environment.NewLine + "SELECT B.FTInsUser, B.FDInsDate, B.FTInsTime, B.FTUpdUser, B.FDUpdDate, B.FTUpdTime, B.FNHSysStyleId, B.FNSeq, B.FNMerMatSeq, B.FNColorWaySeq, B.FTRunColor, B.FNHSysRawMatColorId, B.FNHSysMatColorId, B.FTRawMatColorNameTH, B.FTRawMatColorNameEN  ";
                StrSql += Environment.NewLine + "," + SysSeasonId + "";
                StrSql += Environment.NewLine + ",ISNULL((";
                StrSql += Environment.NewLine + "SELECT TOP 1 FNRunSeq FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LOG) + "].dbo.TMERTStyle_ColorWay AS X WITH(NOLOCK)";
                StrSql += Environment.NewLine + "  WHERE X.FNHSysStyleId=" + SysStyleId + "";
                StrSql += Environment.NewLine + "  AND X.FNHSysSeasonId=" + SysSeasonId + "";
                StrSql += Environment.NewLine + " ORDER BY FNRunSeq DESC ),0) +1 AS FNRunSeq ";
                StrSql += Environment.NewLine + ",'" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                StrSql += Environment.NewLine + "," + HI.UL.ULDate.FormatDateDB + "";
                StrSql += Environment.NewLine + "," + HI.UL.ULDate.FormatTimeDB + "";
                StrSql += Environment.NewLine + " FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTStyle_Mat AS A WITH(NOLOCK) ";
                StrSql += Environment.NewLine + " INNER JOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTStyle_ColorWay AS B WITH(NOLOCK) ON  A.FNHSysStyleId = B.FNHSysStyleId AND A.FNSeq = B.FNSeq ";
                StrSql += Environment.NewLine + "  WHERE A.FNHSysStyleId=" + SysStyleId + "";
                StrSql += Environment.NewLine + "  AND A.FNHSysSeasonId=" + SysSeasonId + "";

                HI.Conn.SQLConn.ExecuteOnly(StrSql, Conn.DB.DataBaseName.DB_LOG);

            }
            catch
            {
            }

        }

        public static void CreateLogBomSheetStyleBreakDown(int SysStyleId, int SysSeasonId)
        {
            try
            {
                string StrSql = "";
                StrSql = "INSERT INTO [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LOG) + "].dbo.TMERTStyle_SizeBreakDown ";
                StrSql += Environment.NewLine + " (FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysStyleId, FNSeq, FNMerMatSeq, FNSieBreakDownSeq, FTSizeBreakDown, FTRunSize, FNHSysRawMatSizeId, FNHSysMatSizeId, FNHSysSeasonId, FNRunSeq, FTEditUser, FDEditDate, FTEditTime)  ";
                StrSql += Environment.NewLine + "SELECT B.FTInsUser, B.FDInsDate, B.FTInsTime, B.FTUpdUser, B.FDUpdDate, B.FTUpdTime, B.FNHSysStyleId, B.FNSeq, B.FNMerMatSeq, B.FNSieBreakDownSeq, B.FTSizeBreakDown, B.FTRunSize, B.FNHSysRawMatSizeId,B.FNHSysMatSizeId  ";
                StrSql += Environment.NewLine + "," + SysSeasonId + "";
                StrSql += Environment.NewLine + ",ISNULL((";
                StrSql += Environment.NewLine + "SELECT TOP 1 FNRunSeq FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LOG) + "].dbo.TMERTStyle_SizeBreakDown AS X WITH(NOLOCK)";
                StrSql += Environment.NewLine + "  WHERE X.FNHSysStyleId=" + SysStyleId + "";
                StrSql += Environment.NewLine + "  AND X.FNHSysSeasonId=" + SysSeasonId + "";
                StrSql += Environment.NewLine + " ORDER BY FNRunSeq DESC ),0) +1 AS FNRunSeq ";
                StrSql += Environment.NewLine + ",'" + HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) + "'";
                StrSql += Environment.NewLine + "," + HI.UL.ULDate.FormatDateDB + "";
                StrSql += Environment.NewLine + "," + HI.UL.ULDate.FormatTimeDB + "";
                StrSql += Environment.NewLine + " FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTStyle_Mat AS A WITH(NOLOCK) ";
                StrSql += Environment.NewLine + " INNER JOIN [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + "].dbo.TMERTStyle_SizeBreakDown AS B WITH(NOLOCK) ON  A.FNHSysStyleId = B.FNHSysStyleId AND A.FNSeq = B.FNSeq ";
                StrSql += Environment.NewLine + "  WHERE A.FNHSysStyleId=" + SysStyleId + "";
                StrSql += Environment.NewLine + "  AND A.FNHSysSeasonId=" + SysSeasonId + "";

                HI.Conn.SQLConn.ExecuteOnly(StrSql, Conn.DB.DataBaseName.DB_LOG);
            }
            catch
            {}
        }


    }
}
