using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Transight.Interface.Config;
using Transight.Interface.Common;
using System.Windows.Forms;

namespace Transight.Interface.Config
{
    public static class DataFunctions
    {
        public static SqlConnection SQLConn;
        public static DataSet SQLDataSet = new DataSet();
        public static SqlDataReader SQLReader;
        public static SqlDataAdapter xadapter;
        public static SqlCommandBuilder xcmdbl;
        public static bool Connect()
        {
            bool RetVal = true;
            SQLConn = new SqlConnection(("server="
                            + (AppConfig.dbServer + (";database="
                            + (AppConfig.dbName + (";uid="
                            + (AppConfig.dbUserName + (";password="
                            + (AppConfig.dbPassword + ";")))))))));
            

            //SQLConn = new SqlConnection(("server=localhost;database="
            //                + (AppConfig.dbName + (";Authentication=Windows Authentication; Integrated Security=True;"))));

            try
            {
                SQLConn.Open();
                if ((SQLConn.State == ConnectionState.Open))
                {
                    RetVal = true;
                }

            }
            catch (SqlException ex)
            {
                RetVal = false;
                ErrorTracking.Log("[AppConfig/Load] Error loading configs.");
                ErrorTracking.Log(ex);
                throw ex;
                
            }

            return RetVal;
        }
        public static void Disconnect()
        {
            if ((SQLConn.State == ConnectionState.Open))
            {
                SQLConn.Close();
            }
          

        }

        public static void Execute(string SQLCommandExecute)
        {
            if ((Connect() == true))
            {
                try
                {
                    SqlCommand SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandTimeout = 30;
                    SQLCmd.CommandText = SQLCommandExecute;
                    SQLCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    ErrorTracking.Log("[AppConfig/Load] Error loading configs.");
                    ErrorTracking.Log(ex);
                    throw ex;
                }

                Disconnect();
            }

        }
        public static string GetRecordString(string SQLCommandTxt)
        {
            string RetVal = "";
            if ((Connect() == true))
            {
                try
                {
                    SqlCommand scalarCommand = new SqlCommand(SQLCommandTxt, SQLConn);
                    RetVal = Convert.IsDBNull(scalarCommand.ExecuteScalar()) ? "" : scalarCommand.ExecuteScalar().ToString();

                    // If (RetVal = Nothing Or IsDBNull(RetVal)) Then RetVal = ""
                    scalarCommand = null;
                }
                catch (Exception ex)
                {
                    ErrorTracking.Log("[AppConfig/Load] Error loading configs.");
                    ErrorTracking.Log(ex);
                    throw ex;
                }
                finally
                {
                  
                    Disconnect();
                }

            }

            return RetVal;
        }

        public static int GetRecordCount(string SQLCommandTxt)
        {
            int RetVal = 0;
            if ((Connect() == true))
            {
                try
                {
                    SqlCommand scalarCommand = new SqlCommand(SQLCommandTxt, SQLConn);
                    RetVal = Convert.ToInt32(scalarCommand.ExecuteScalar());

                    // If (RetVal = Nothing Or IsDBNull(RetVal)) Then RetVal = ""
                    scalarCommand = null;
                }
                catch (Exception ex)
                {
                    ErrorTracking.Log("[AppConfig/Load] Error loading configs.");
                    ErrorTracking.Log(ex);
                    throw ex;
                }
                finally
                {

                    Disconnect();
                }

            }

            return RetVal;
        }

        public static void UpdateDataTable(string DataTableName, SqlDataAdapter xadapter)
        {
            if ((Connect() == true))
            {
                try
                {
                    
                    xcmdbl = new SqlCommandBuilder(xadapter);
                    xadapter.Update(SQLDataSet, DataTableName);

                }
                catch (Exception ex)
                {
                    ErrorTracking.Log("[AppConfig/Load] Error loading configs.");
                    ErrorTracking.Log(ex);
                    throw ex;
                }

            }


        }
        public static void FillDataTable(string SQLCommandTxt, string DataTableName)
        {
            if ((Connect() == true))
            {
                try
                {
                    xadapter = new SqlDataAdapter(SQLCommandTxt, SQLConn);
                    if ((SQLDataSet.Tables.Contains(DataTableName) == true))
                    {
                        SQLDataSet.Tables.Remove(DataTableName);
                    }
                    
                    xadapter.Fill(SQLDataSet, DataTableName);
                    //xadapter = null;
                }
                catch (Exception ex)
                {
                    ErrorTracking.Log("[AppConfig/Load] Error loading configs.");
                    ErrorTracking.Log(ex);
                    throw ex;
                }
                finally
                {
                    //Disconnect();
                }

            }

        }

        public static void LoadDataToGrid(string xDataTableName, DataGridView xDataGridView, DataView xDataView, string xQry)
        {
            try
            {
                FillDataTable(xQry, xDataTableName);
                
                System.Windows.Forms.Application.DoEvents();
                
                xDataView.Table = SQLDataSet.Tables[xDataTableName];
                
                xDataGridView.DataSource = xDataView;
             

            }
            catch (Exception ex)
            {
                ErrorTracking.Log("[AppConfig/Load] Error loading configs.");
                ErrorTracking.Log(ex);
                throw ex;
            }

        }

        public static string LoadDataToGridCombo(DataGridViewComboBoxColumn xComboBox, string StrSQL, bool ClearCombo = false, bool AddBlank= false)
        {
            string xArrOut = "";
           
            if (ClearCombo)
            {
                xComboBox.Items.Clear();
            }

            if (AddBlank)
            {
                xComboBox.Items.Add("");
                xArrOut += "|";
            }

            if ((Connect() == true))
            {
                try
                {
                    Read(StrSQL);
                    while (SQLReader.Read())

                    {
                        
                        xComboBox.Items.Add(SQLReader.GetValue(0));
                        if ((SQLReader.FieldCount == 2))
                        {
                            xArrOut += (SQLReader.GetValue(1) + "|");
                        }
                    }

                    EndRead();
                }
                catch (Exception ex)
                {
                    ErrorTracking.Log("[AppConfig/Load] Error loading configs.");
                    ErrorTracking.Log(ex);
                    throw ex;
                }
                finally
                {
                    //Disconnect();
                }

            }

            return xArrOut;
        }

        public static string LoadDataToCombo(ComboBox xComboBox, string StrSQL, bool ClearCombo = false, bool AddBlank = false)
        {
            string xArrOut = "";

            if (ClearCombo)
            {
                xComboBox.Items.Clear();
            }

            if (AddBlank)
            {
                xComboBox.Items.Add("");
                xArrOut += "|";
            }

            if ((Connect() == true))
            {
                try
                {
                    Read(StrSQL);
                    while (SQLReader.Read())

                    {

                        xComboBox.Items.Add(SQLReader.GetValue(0));
                        if ((SQLReader.FieldCount == 2))
                        {
                            xArrOut += (SQLReader.GetValue(1) + "|");
                        }
                    }

                    EndRead();
                }
                catch (Exception ex)
                {
                    ErrorTracking.Log("[AppConfig/Load] Error loading configs.");
                    ErrorTracking.Log(ex);
                    throw ex;
                }
                finally
                {
                    //Disconnect();
                }

            }

            return xArrOut;
        }

        public static bool Read(string SQLCommandTxt)
        {
            SqlCommand SQLCmd = new SqlCommand(SQLCommandTxt, SQLConn);
           
            SQLReader = SQLCmd.ExecuteReader();
            return true;

        }

        public static void EndRead()
        {
            SQLReader.Close();
        }
    }


}
