using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Transight.Interface.Common;
using Transight.Interface.Config;

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


        public static List<StoreList> GetStoreList(DateTime BusinessDate)
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
         

                cmd = new SqlCommand(query, conn);
                prmtr = new SqlParameter("@BusinessDate", SqlDbType.DateTime);
                prmtr.Value = BusinessDate;
                cmd.Parameters.Add(prmtr);
                dtAdptr = new SqlDataAdapter(cmd);
                dtAdptr.Fill(dtTbl);

                conn.Close();

                //List<IceasonCheck> IceasonList = new List<IceasonCheck>();
                List<StoreList> StoreList = new List<StoreList>();

                for (i = 0; i < dtTbl.Rows.Count; i++)
                {


                    StoreList.Add(new StoreList(
                                                     Convert.ToDateTime(dtTbl.Rows[i]["Date"]),
                                                     Convert.ToDecimal(dtTbl.Rows[i]["SalesAmount"]),
                                                     Convert.ToInt32(dtTbl.Rows[i]["TotalTransaction"]),
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
    }
}
