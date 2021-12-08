using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Transight.Interface.Common;
using Transight.Interface.Config;
using System.Configuration;

namespace TransightInterface
{
    public static class Data
    {
        private static SqlConnection conn;
        private static SqlDataAdapter dtAdptr;
        private static SqlCommand cmd;
        private static SqlParameter prmtr;
        private static string query;




        public static DateTime GetBusinessDate()
        {
            try
            {
                conn = new SqlConnection(Program.ConnString);
                conn.Open();

                query = "SELECT busidate FROM system";
                cmd = new SqlCommand(query, conn);
                DateTime dtBusinessDate = Convert.ToDateTime(cmd.ExecuteScalar());

                conn.Close();

                return dtBusinessDate;
            }
            catch (SqlException sex)
            {
                ErrorTracking.Log("[Data/GetBusinessDate] Error getting business date [" + query + "].");
                ErrorTracking.Log(sex);
                throw new ApplicationException("Error getting business date.");
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                conn = null;

                cmd.Dispose();
                cmd = null;
            }
        }

        public static bool CheckConnection()
        {
            try
            {
                conn = new SqlConnection(Program.ConnString);
                conn.Open();
                conn.Close();
                return true;
            }
            catch (SqlException sex)
            {
                ErrorTracking.Log("[Data/CheckConnection] Error opening DB connection [" + Program.ConnString + "].");
                ErrorTracking.Log(sex);
                return false;
            }
            finally
            {
                conn.Dispose();
                conn = null;
            }
        }

        public static void OpenConnection()
        {
            try
            {
                conn = new SqlConnection(Program.ConnString);
                conn.Open();
            }
            catch (SqlException sex)
            {
                ErrorTracking.Log("[Data/OpenConnection] Error opening DB connection [" + Program.ConnString + "].");
                ErrorTracking.Log(sex);
                throw new ApplicationException("Error opening DB connection.");
            }
        }

        public static void CloseConnection()
        {
            try
            {
                if (conn == null)
                    return;

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }
            catch (SqlException sex)
            {
                ErrorTracking.Log("[Data/CloseConnection] Error closing DB connection [" + Program.ConnString + "].");
                ErrorTracking.Log(sex);
                throw new ApplicationException("Error closing DB connection.");
            }
        }

        public static void PrepareTempTable()
        {
            try
            {
                conn = new SqlConnection(Program.ConnString);
                conn.Open();

                //check table exist
                query = "(SELECT COUNT(*) FROM dbo.sysobjects WHERE ID = object_id(N'[dbo].[ParadigmMall_seq]') AND OBJECTPROPERTY(ID, N'IsUserTable') = 1)";
                cmd = new SqlCommand(query, conn);


                if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                {
                    //create table if not exist
                    query = "CREATE TABLE [dbo].[ParadigmMall_seq] (" +
                            "  [SerialTable_Date] [datetime] NULL, " +
                            "  [SerialTable_Count] [int] NULL " +
                            ") ON [PRIMARY]";
                    cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                }


                query = "(SELECT COUNT(*) FROM dbo.sysobjects WHERE ID = object_id(N'[dbo].[ICI_SerialNumber_Payment]') AND OBJECTPROPERTY(ID, N'IsUserTable') = 1)";
                cmd = new SqlCommand(query, conn);


                conn.Close();
            }
            catch (SqlException sex)
            {
                ErrorTracking.Log("[Data/PrepareTempTable] Error checking/creating temp table [" + query + "].");
                ErrorTracking.Log(sex);
                throw new ApplicationException("Error checking/creating temp table.");
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                conn = null;

                cmd.Dispose();
                cmd = null;
            }
        }

        public static int GetSequenceNumber(DateTime BusinessDate)
        {
            try
            {
                int serialnumber = 0;

                conn = new SqlConnection(Program.ConnString);
                conn.Open();

                query = "SELECT count(*) FROM ParadigmMall_seq WHERE SerialTable_Date= @BusinessDate";
                cmd = new SqlCommand(query, conn);
                prmtr = new SqlParameter("@BusinessDate", SqlDbType.DateTime);
                prmtr.Value = BusinessDate;
                cmd.Parameters.Add(prmtr);

                if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                {

                    query = "SELECT SerialTable_Count FROM ParadigmMall_seq WHERE SerialTable_Date= @BusinessDate";
                    cmd = new SqlCommand(query, conn);
                    prmtr = new SqlParameter("@BusinessDate", SqlDbType.DateTime);
                    prmtr.Value = BusinessDate;
                    cmd.Parameters.Add(prmtr);

                    serialnumber = Convert.ToInt32(cmd.ExecuteScalar());
                }
                else
                {
                    query = "INSERT INTO ParadigmMall_seq(SerialTable_Count,SerialTable_Date) VALUES(0,@BusinessDate)";

                    cmd = new SqlCommand(query, conn);
                    prmtr = new SqlParameter("@BusinessDate", SqlDbType.DateTime);
                    prmtr.Value = BusinessDate;
                    cmd.Parameters.Add(prmtr);

                    cmd.ExecuteNonQuery();

                }



                conn.Close();

                return serialnumber;
            }

            catch (SqlException sex)
            {
                ErrorTracking.Log("[Data/GetSequenceNumber] Error getting GetSequenceNumber [" + query + "].");
                ErrorTracking.Log(sex);
                throw new ApplicationException("Error getting GetSequenceNumber");
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                conn = null;

                cmd.Dispose();
                cmd = null;
            }
        }

        public static void GetSequenceNumberUpdate(int SerialNumber, DateTime BusinessDate)
        {
            try
            {

                conn = new SqlConnection(Program.ConnString);
                conn.Open();

                query = "UPDATE ParadigmMall_seq set SerialTable_Count=@SerialNumber where SerialTable_Date=@BusinessDate";
                cmd = new SqlCommand(query, conn);
                prmtr = new SqlParameter("@SerialNumber", SqlDbType.Int);
                prmtr.Value = SerialNumber;
                cmd.Parameters.Add(prmtr);
                prmtr = new SqlParameter("@BusinessDate", SqlDbType.DateTime);
                prmtr.Value = BusinessDate;
                cmd.Parameters.Add(prmtr);
                cmd.ExecuteNonQuery();


                conn.Close();

            }

            catch (SqlException sex)
            {
                ErrorTracking.Log("[Data/GetSequenceNumberUpdate] Error getting GetSequenceNumberUpdate Update[" + query + "].");
                ErrorTracking.Log(sex);
                throw new ApplicationException("Error getting GetSequenceNumberUpdate");
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                conn = null;

                cmd.Dispose();
                cmd = null;
            }
        }
        public static void InsertBatchLogs(string FileName, DateTime BusinessDate)
        {
            try
            {

                conn = new SqlConnection(Program.ConnString);
                conn.Open();

                query = "INSERT INTO MALLINTERFACE_BATCHLOGS(FileName, BusinessDate,date_sent) VALUES (@FileName, @BusinessDate,getdate())";
                cmd = new SqlCommand(query, conn);
                prmtr = new SqlParameter("@FileName", SqlDbType.NVarChar);
                prmtr.Value = FileName;
                cmd.Parameters.Add(prmtr);
                prmtr = new SqlParameter("@BusinessDate", SqlDbType.DateTime);
                prmtr.Value = BusinessDate;
                cmd.Parameters.Add(prmtr);
                cmd.ExecuteNonQuery();


                conn.Close();

            }

            catch (SqlException sex)
            {
                ErrorTracking.Log("[Data/INSERTBatchLogs] Error inserting batch logs Insert[" + query + "].");
                ErrorTracking.Log(sex);
                throw new ApplicationException("Error getting InsertBatchLogs");
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                conn = null;

                cmd.Dispose();
                cmd = null;
            }
        }

        public static int GetBatchNumber(DateTime BusinessDate)
        {
            try
            {
                int batchnum = 0;

                conn = new SqlConnection(Program.ConnString);
                conn.Open();

                query = "SELECT count(*) FROM mallinterface_batchlogs WHERE businessdate = @BusinessDate";
                cmd = new SqlCommand(query, conn);
                prmtr = new SqlParameter("@BusinessDate", SqlDbType.DateTime);
                prmtr.Value = BusinessDate;
                cmd.Parameters.Add(prmtr);

                batchnum = Convert.ToInt32(cmd.ExecuteScalar());

                conn.Close();

                return batchnum;
            }

            catch (SqlException sex)
            {
                ErrorTracking.Log("[Data/GetBatchNumber] Error getting GetBatchNumber [" + query + "].");
                ErrorTracking.Log(sex);
                throw new ApplicationException("Error getting GetBatchNumber");
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                conn = null;

                cmd.Dispose();
                cmd = null;
            }
        }

        //public static string GetLastResetDate
        //{

        //    get
        //    {
        //        try
        //        {
        //            string lastreset = "";

        //            conn = new SqlConnection(Program.ConnString);
        //            conn.Open();

        //            query = "SELECT lastreset FROM system";
        //            cmd = new SqlCommand(query, conn);
        //            //prmtr = new SqlParameter("@BusinessDate", SqlDbType.DateTime);
        //            //prmtr.Value = BusinessDate;
        //            //cmd.Parameters.Add(prmtr);

        //            lastreset = Convert.ToString(cmd.ExecuteScalar());

        //            conn.Close();

        //            return lastreset;
        //        }

        //        catch (SqlException sex)
        //        {
        //            ErrorTracking.Log("[Data/GetBatchNumber] Error getting GetBatchNumber [" + query + "].");
        //            ErrorTracking.Log(sex);
        //            throw new ApplicationException("Error getting GetBatchNumber");
        //        }
        //        finally
        //        {
        //            if (conn.State == ConnectionState.Open) conn.Close();
        //            conn.Dispose();
        //            conn = null;

        //            cmd.Dispose();
        //            cmd = null;
        //        }
        //    }
        //}

        public static DateTime GetLastResetDate()
        {
            try
            {
                conn = new SqlConnection(Program.ConnString);
                conn.Open();

                query = "SELECT lastreset FROM system";
                cmd = new SqlCommand(query, conn);
                DateTime dtLastReset = Convert.ToDateTime(cmd.ExecuteScalar());

                conn.Close();

                return dtLastReset;
            }
            catch (SqlException sex)
            {
                ErrorTracking.Log("[Data/GetLastResetDate] Error getting last reset date [" + query + "].");
                ErrorTracking.Log(sex);
                throw new ApplicationException("Error getting last reset date.");
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                conn = null;

                cmd.Dispose();
                cmd = null;
            }
        }

        public static DateTime GetPrevDate()
        {
            try
            {
                DateTime prevdate;

                conn = new SqlConnection(Program.ConnString);
                conn.Open();

                query = "select top 1 businessdate from mallinterface_batchlogs order by businessdate desc";
                cmd = new SqlCommand(query, conn);
                prevdate = Convert.ToDateTime(cmd.ExecuteScalar());

                conn.Close();

                return prevdate;
            }

            catch (SqlException sex)
            {
                ErrorTracking.Log("[Data/GetBatchNumber] Error getting GetBatchNumber [" + query + "].");
                ErrorTracking.Log(sex);
                throw new ApplicationException("Error getting GetBatchNumber");
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                conn = null;

                cmd.Dispose();
                cmd = null;
            }
        }


        public static List<posmasterSH> GetStoreList(DateTime BusinessDate)
        {
            DataTable dtTbl = new DataTable();

            try
            {
                conn = new SqlConnection(Program.ConnString);
                conn.Open();
                int i;

                //New Added
                /*
                query =           
                "SELECT t.business_date as 'Date',SUM(c.pymnt_ttl) as 'SalesAmount',COUNT(c.chk_seq) as 'TotalTransaction' FROM "+
                "(SELECT * FROM checks) c "+
                "inner join "+
                "(SELECT chk_seq,business_date FROM transactions WHERE business_date = @BusinessDate "+ 
                "GROUP BY business_date,chk_seq) t "+ 
                "ON t.chk_seq=c.chk_seq "+
                "AND c.training = 0 "+
                "AND chk_cancelled = 0 "+
                "AND chk_open = 'F' "+
                "AND NOT EXISTS "+
                "(SELECT trans_seq FROM chk_xfer_dtl x "+
                "WHERE x.chk_num = c.chk_num "+
                "AND x.pc_seq = c.pc_seq "+
                "AND x.chk_open_date_time = c.chk_open_date_time "+
                "AND x.trans_seq IN (SELECT trans_seq FROM transactions WHERE type = 'A' AND training = 0)) " +
                "GROUP BY t.business_date";
                 */

                query =
                "select sum(netsales) as [SalesAmount],count(netsales) as [TotalTransaction],busidate as [Date] " +
                "from salesttl where busidate = @BusinessDate " +
                "group by busidate";
                
                /*From Stored Procedure*/
                //query = "select sum(netsales) as [SalesAmount],count(netsales) as [TotalTransaction],busidate as [Date] " +
                //"from salesttl where busidate = @BusinessDate " +
                //"group by busidate";


                cmd = new SqlCommand(query, conn);
                prmtr = new SqlParameter("@BusinessDate", SqlDbType.DateTime);
                prmtr.Value = BusinessDate;
                cmd.Parameters.Add(prmtr);
                dtAdptr = new SqlDataAdapter(cmd);
                dtAdptr.Fill(dtTbl);

                conn.Close();

                //List<IceasonCheck> IceasonList = new List<IceasonCheck>();
                List<posmasterSH> StoreList = new List<posmasterSH>();

                for (i = 0; i < dtTbl.Rows.Count; i++)
                {


                    StoreList.Add(new posmasterSH(
                                                     Convert.ToDateTime(dtTbl.Rows[i]["Date"]),
                                                     Convert.ToDecimal(dtTbl.Rows[i]["SalesAmount"]),
                                                     Convert.ToInt32(dtTbl.Rows[i]["TotalTransaction"]),
                                                     Convert.ToInt32(dtTbl.Rows[i]["FileNo"]),
                                                     0));
                }

                return StoreList;
            }
            catch (SqlException sex)
            {
                ErrorTracking.Log("[Data/GetStoreList] Error getting GetStoreList List [" + query + "] [" + BusinessDate.ToString(Const.DateFormat) + "].");
                ErrorTracking.Log(sex);
                throw new ApplicationException("Error getting GetStoreList List.");
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                conn = null;

                if (dtAdptr != null)
                {
                    dtAdptr.Dispose();
                    dtAdptr = null;
                }

                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }

                dtTbl.Dispose();
                dtTbl = null;
            }
        }

        public static String GetSequenceSwitch(int SequenceNumber)
        {

            int timeDef = SequenceNumber;
            String ReturnAlpha = "";

            switch (timeDef)
            {
                case 1:
                    ReturnAlpha = "A";
                    break;
                case 2:
                    ReturnAlpha = "B";
                    break;
                case 3:
                    ReturnAlpha = "C";
                    break;
                case 4:
                    ReturnAlpha = "D";
                    break;
                case 5:
                    ReturnAlpha = "E";
                    break;
                case 6:
                    ReturnAlpha = "F";
                    break;
                case 7:
                    ReturnAlpha = "G";
                    break;
                case 8:
                    ReturnAlpha = "H";
                    break;
                case 9:
                    ReturnAlpha = "I";
                    break;
                case 10:
                    ReturnAlpha = "J";
                    break;
                case 11:
                    ReturnAlpha = "K";
                    break;
                case 12:
                    ReturnAlpha = "L";
                    break;
                case 13:
                    ReturnAlpha = "M";
                    break;
                case 14:
                    ReturnAlpha = "N";
                    break;
                case 15:
                    ReturnAlpha = "O";
                    break;
                case 16:
                    ReturnAlpha = "P";
                    break;
                case 17:
                    ReturnAlpha = "Q";
                    break;
                case 18:
                    ReturnAlpha = "R";
                    break;
                case 19:
                    ReturnAlpha = "S";
                    break;
                case 20:
                    ReturnAlpha = "T";
                    break;
                case 21:
                    ReturnAlpha = "U";
                    break;
                case 22:
                    ReturnAlpha = "V";
                    break;
                case 23:
                    ReturnAlpha = "W";
                    break;
                case 24:
                    ReturnAlpha = "X";
                    break;
                case 25:
                    ReturnAlpha = "Y";
                    break;
                case 26:
                    ReturnAlpha = "Z";
                    break;
                default:
                    ReturnAlpha = "Z";
                    break;
            }

            return ReturnAlpha;
        }



        public static List<posmasterSH> GetPosmasterSHList(DateTime GenerateDate)
        {
            DataTable dtTbl = new DataTable();
            List<posmasterSH> PosmasterList = new List<posmasterSH>();

            string SPCALL = ConfigurationManager.AppSettings["SPCALL"];

            string exTen = ConfigurationManager.AppSettings["exTender"]; //1

            //LS  SPCALL ="1" excl.gst, call sp_SalesHourlyGTO1 (e.g MFM)
            //LS  SPCALL ="2" incl.gst, call sp_SalesHourlyGT02 (e.g KFC)

            decimal exAmount = 0;

            try
            {
                SqlConnection connT = new SqlConnection(Program.ConnString);
                #region Daily

                String str = exTen;
                Func.Log("Exec exTen: " + exTen);
                String[] spearator = { "," };
                String[] strlist = str.Split(spearator, StringSplitOptions.RemoveEmptyEntries);


                for (int i = 0; i < strlist.Length; i++)
                {

                    DataTable dtTbl1 = new DataTable();
                    int TenderMedia = GetTenderMedia(strlist[i]);
                    //String TenderMedia = GetTenderMedia(strlist[i]);
                    Func.Log("Tender: " + TenderMedia);
                    int Tm = Convert.ToInt32(TenderMedia);

                    SqlCommand cmd1 = new SqlCommand("sp_SalesDailyGTO1_tender1", connT);

                    cmd1.Parameters.AddWithValue("@sSDate", GenerateDate);
                    cmd1.Parameters.AddWithValue("@sTender", Tm);
                    cmd1.CommandType = CommandType.StoredProcedure;

                    connT.Open();

                    dtAdptr = new SqlDataAdapter(cmd1);
                    dtAdptr.Fill(dtTbl1);


                    connT.Close();
                    //if (dtTbl1.Rows.Count>0)
                    //{
                    //    exAmount = exAmount + Convert.ToDecimal(dtTbl1.Rows[0][0]);
                    //}
                    //MessageBox.Show(Convert.ToString(dtTbl1.Rows[0][0])+" "+ strlist[i]);

                    exAmount = exAmount + Convert.ToDecimal(dtTbl1.Rows[0][0]);
                    dtTbl1.Dispose();
                    dtTbl1 = null;


                }


                if (SPCALL == "1")
                {

                    SqlCommand cmdT = new SqlCommand("sp_SalesDailyGTO1", connT);

                    cmdT.Parameters.AddWithValue("@sSDate", GenerateDate);
                    cmdT.CommandType = CommandType.StoredProcedure;

                    connT.Open();

                    dtAdptr = new SqlDataAdapter(cmdT);
                    dtAdptr.Fill(dtTbl);
                    Func.Log("Execute sp_SalesDailyGTO1." + "StartDate:" + GenerateDate.ToString());

                    Func.Log("Return sp_SalesDailyGTO1. RowCount=" + dtTbl.Rows.Count.ToString());
                }
                else if (SPCALL == "2")
                {

                    SqlCommand cmdT = new SqlCommand("sp_SalesDailyGTO2", connT);

                    cmdT.Parameters.AddWithValue("@sSDate", GenerateDate);
                    cmdT.CommandType = CommandType.StoredProcedure;

                    connT.Open();

                    dtAdptr = new SqlDataAdapter(cmdT);
                    dtAdptr.Fill(dtTbl);

                    Func.Log("Execute sp_SalesDailyGTO2." + "StartDate:" + GenerateDate.ToString());

                    Func.Log("Return sp_SalesDailyGTO2. RowCount=" + dtTbl.Rows.Count.ToString());
                }
                #endregion
                for (int i = 0; i < dtTbl.Rows.Count; i++)
                {
                    // LS- IOICityMall
                    //int SequenceNumber = 0;
                    posmasterSH posmasterData = new posmasterSH();

                    posmasterData.busDate = Convert.ToDateTime(dtTbl.Rows[i]["DATE"]);
                    posmasterData.salesAmount = Convert.ToDecimal(dtTbl.Rows[i]["NetAmount"]) - exAmount;
                    posmasterData.fileNo = Convert.ToInt32(dtTbl.Rows[i]["FileNo"]);

                    posmasterData.sequenceNumber = 0;
                    PosmasterList.Add(posmasterData);

                }

                if (connT.State == ConnectionState.Open)
                    connT.Close();
                connT.Dispose();
                connT = null;

                //if (dtAdptr != null)
                //{
                //    dtAdptr.Dispose();
                //    dtAdptr = null;
                //}

                //if (cmdT != null)
                //{
                //    cmdT.Dispose();
                //    cmdT = null;
                //}

                dtTbl.Dispose();
                dtTbl = null;


                return PosmasterList;
            }
            catch (SqlException sex)
            {
                ErrorTracking.Log("[Data/GetPostmasterSHList] Error getting data data [" + query + "] [" + GenerateDate.ToString(Const.DateFormat) + "]");
                ErrorTracking.Log(sex);
                throw new ApplicationException("Error getting data.");
            }
            finally
            {


            }

        }

        public static List<posmasterSHNew> GetPosmasterSHNewList(DateTime GenerateDate)
        {
            DataTable dtTbl = new DataTable();
            List<posmasterSHNew> PosmasterList = new List<posmasterSHNew>();

            string SPCALL = ConfigurationManager.AppSettings["SPCALL"];
            //LS  SPCALL ="1" excl.gst, call sp_SalesHourlyGTO1 (e.g MFM)
            //LS  SPCALL ="2" incl.gst, call sp_SalesHourlyGT02 (e.g KFC)

            string exTen = ConfigurationManager.AppSettings["exTender"]; //1
            decimal exAmount = 0;

            try
            {

                SqlConnection connT = new SqlConnection(Program.ConnString);

                //--Skipper
                //String str = exTen;
                //Func.Log("Exec exTen: " + exTen);
                //String[] spearator = { "," };
                //String[] strlist = str.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

                //for (int i = 0; i < strlist.Length; i++)
                //{

                //    DataTable dtTbl1 = new DataTable();
                //    int TenderMedia = GetTenderMedia(strlist[i]);
                //    //String TenderMedia = GetTenderMedia(strlist[i]);
                //    Func.Log("Tender: " + TenderMedia);
                //    int Tm = Convert.ToInt32(TenderMedia);

                //    SqlCommand cmd1 = new SqlCommand("sp_SalesDailyGTO1_tender1", connT);

                //    cmd1.Parameters.AddWithValue("@sSDate", GenerateDate);
                //    cmd1.Parameters.AddWithValue("@sTender", Tm);
                //    cmd1.CommandType = CommandType.StoredProcedure;

                //    connT.Open();

                //    dtAdptr = new SqlDataAdapter(cmd1);
                //    dtAdptr.Fill(dtTbl1);


                //    connT.Close();
                //    //if (dtTbl1.Rows.Count>0)
                //    //{
                //    //    exAmount = exAmount + Convert.ToDecimal(dtTbl1.Rows[0][0]);
                //    //}
                //    //MessageBox.Show(Convert.ToString(dtTbl1.Rows[0][0])+" "+ strlist[i]);

                //    exAmount = exAmount + Convert.ToDecimal(dtTbl1.Rows[0][0]);
                //    dtTbl1.Dispose();
                //    dtTbl1 = null;


                //}



                SqlCommand cmdT = new SqlCommand("sp_SalesDailyRobMallTxtfile", connT);
                cmdT.Parameters.AddWithValue("@spDate", GenerateDate);
                cmdT.Parameters.AddWithValue("@spPCSEQ", AppConfig.PC_Seq);
                cmdT.Parameters.AddWithValue("@spFirstRead", AppConfig.StartDate);
                cmdT.CommandType = CommandType.StoredProcedure;

                connT.Open();

                dtAdptr = new SqlDataAdapter(cmdT);
                dtAdptr.Fill(dtTbl);
                Func.Log("Execute sp_SalesDailyRobMallTxtfile." + "StartDate:" + GenerateDate.ToString());

                Func.Log("Return sp_SalesDailyRobMallTxtfile. RowCount=" + dtTbl.Rows.Count.ToString());





                for (int i = 0; i < dtTbl.Rows.Count; i++)
                {
                    //int SequenceNumber = 0;
                    posmasterSHNew posmasterData = new posmasterSHNew();

                   
                    posmasterData.TerminalNo = dtTbl.Rows[i]["TerminalNo"].ToString();
                    posmasterData.GrossSales = dtTbl.Rows[i]["Gross"].ToString();
                    posmasterData.TTax = dtTbl.Rows[i]["Tax"].ToString();
                    posmasterData.TVoid = dtTbl.Rows[i]["TVoid"].ToString();
                    posmasterData.CVoid = dtTbl.Rows[i]["CVoid"].ToString();
                    posmasterData.TDisc = dtTbl.Rows[i]["TDisc"].ToString();
                    posmasterData.CDisc = dtTbl.Rows[i]["CDisc"].ToString();
                    posmasterData.TRefund = dtTbl.Rows[i]["TRefund"].ToString();
                    posmasterData.CRefund = dtTbl.Rows[i]["CRefund"].ToString();
                    posmasterData.TNegAdj = dtTbl.Rows[i]["TNegDisc"].ToString();
                    posmasterData.CNegAdj = dtTbl.Rows[i]["CNegDisc"].ToString();
                    posmasterData.TServChg = dtTbl.Rows[i]["Svchg"].ToString();
                    posmasterData.PrevZCnt = dtTbl.Rows[i]["OZCnt"].ToString();
                    posmasterData.PrevGT = dtTbl.Rows[i]["OGT"].ToString();
                    posmasterData.NewZCnt = dtTbl.Rows[i]["ZCnt"].ToString();
                    posmasterData.NewGT = dtTbl.Rows[i]["NGT"].ToString();
                    posmasterData.BizDate = dtTbl.Rows[i]["Date"].ToString();
                    posmasterData.Novelty = dtTbl.Rows[i]["Novelty"].ToString();
                    posmasterData.Misc = dtTbl.Rows[i]["Misc"].ToString();
                    posmasterData.LocalTax = dtTbl.Rows[i]["LocalTax"].ToString();
                    posmasterData.TCreditSales = dtTbl.Rows[i]["TChrg"].ToString();
                    posmasterData.TCreditTax = dtTbl.Rows[i]["TChrgTax"].ToString();
                    posmasterData.TNVatSales = dtTbl.Rows[i]["NTaxSales"].ToString();
                    posmasterData.Pharma = dtTbl.Rows[i]["PharmaSales"].ToString();
                    posmasterData.NPharma = dtTbl.Rows[i]["NPharmaSales"].ToString();
                    posmasterData.TPWDDisc = dtTbl.Rows[i]["TPWDDisc"].ToString();
                    posmasterData.GrossNotSub = dtTbl.Rows[i]["GSnotSubjectRent"].ToString();
                    posmasterData.TReprint = dtTbl.Rows[i]["ReprintAmt"].ToString();
                    posmasterData.CReprint = dtTbl.Rows[i]["ReprintCnt"].ToString();


                    PosmasterList.Add(posmasterData);

                    //PosmasterList.Add(new posmasterSH(Convert.ToDateTime(dtTbl.Rows[i]["Date"]),Convert.ToDecimal(dtTbl.Rows[i]["SalesAmount"]),Convert.ToInt32(dtTbl.Rows[i]["TotalTransaction"]), 0));
                    //PosmasterList.Add(posmasterData);
                }

                if (connT.State == ConnectionState.Open)
                    connT.Close();
                connT.Dispose();
                connT = null;

                //if (dtAdptr != null)
                //{
                //    dtAdptr.Dispose();
                //    dtAdptr = null;
                //}

                //if (cmdT != null)
                //{
                //    cmdT.Dispose();
                //    cmdT = null;
                //}

                dtTbl.Dispose();
                dtTbl = null;


                return PosmasterList;
            }
            catch (SqlException sex)
            {
                ErrorTracking.Log("[Data/GetPostmasterSHList] Error getting data data [" + query + "] [" + GenerateDate.ToString(Const.DateFormat) + "]");
                ErrorTracking.Log(sex);
                throw new ApplicationException("Error getting data.");
            }
            finally
            {


            }

        }

        public static int GetTenderMedia(string tendermedia)
        {
            try
            {
                conn = new SqlConnection(Program.ConnString);
                conn.Open();

                query = "select number from tender where name1 like '" + tendermedia + "'";
                cmd = new SqlCommand(query, conn);
                //prmtr = new SqlParameter("@BusinessDate", SqlDbType.DateTime);
                //prmtr.Value = BusinessDate;
                //cmd.Parameters.Add(prmtr);

                int number = Convert.ToInt32(cmd.ExecuteScalar());

                //query = "select MALL_TENDERMEDIA from MALLINTERFACE_TENDER where POS_TENDERMEDIA =" + number;
                //cmd = new SqlCommand(query, conn);
                ////cmd.Parameters.Add(prmtr);
                //string tendermediaName = Convert.ToString(cmd.ExecuteScalar());
                //conn.Close();
                //return tendermediaName;

                return number;
            }

            catch (SqlException sex)
            {
                ErrorTracking.Log("[Data/GetSequenceNumber] Error getting GetTenderMediaNumber [" + query + "].");
                ErrorTracking.Log(sex);
                throw new ApplicationException("Error getting GetTenderMediaNumber");
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                conn = null;

                cmd.Dispose();
                cmd = null;
            }
        }
    }
}
