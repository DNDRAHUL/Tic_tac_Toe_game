using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Data;
using System;
using System.Data.SqlClient;
using Tictoe.Helper;

namespace Tictoe.DAL
{
    public class ManualDbContext
    {

        IConfiguration _configuration;

        public ManualDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DataSet GetDataSet(string sp, Hashtable mapParam = null, int timeOutInSec = 2400)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationExtensions.GetConnectionString(_configuration, "Local_Conn")))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand(sp, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    int timeOut = Convert.ToInt32(_configuration["CommandTimeout"]);
                    if (timeOut.IsNotNull() && timeOut != 0)
                    {
                        cmd.CommandTimeout = timeOut;
                    }

                    if (mapParam.IsNotNull())
                    {
                        if (mapParam.Count > 0)
                        {
                            foreach (string item in mapParam.Keys)
                            {
                                cmd.Parameters.AddWithValue(item, mapParam[item]);
                            }
                        }

                    }

                    DataSet ds = new DataSet();
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    ad.Fill(ds);
                    con.Close();
                    return ds;


                }
                catch (Exception ex)
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                    //  new Util(_configuration).SaveExceptionLog(ex.Message + ex.StackTrace, nameof(MannualDbContext), "Database");
                    throw ex;
                }
            }
        }


        public DataTable GetDataTable(string sp, Hashtable mapParam)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationExtensions.GetConnectionString(_configuration, "Local_Conn")))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand(sp, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    int timeOut = Convert.ToInt32(_configuration["CommandTimeout"]);
                    if (timeOut.IsNotNull() && timeOut != 0)
                    {
                        cmd.CommandTimeout = timeOut;
                    }

                    if (mapParam != null)
                    {
                        foreach (string item in mapParam.Keys)
                        {
                            cmd.Parameters.AddWithValue(item, mapParam[item]);
                        }
                    }

                    DataTable dt = new DataTable();
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    ad.Fill(dt);
                    con.Close();
                    return dt;

                }

                catch (Exception ex)
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                    //new Util(_configuration).SaveExceptionLog(ex.Message + ex.StackTrace, nameof(MannualDbContext), "Database");
                    throw ex;
                }
            }
        }


        public DataTable GetDataTable(string sp)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationExtensions.GetConnectionString(_configuration, "Local_Conn")))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand(sp, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    int timeOut = Convert.ToInt32(_configuration["CommandTimeout"]);
                    if (timeOut.IsNotNull() && timeOut != 0)
                    {
                        cmd.CommandTimeout = timeOut;
                    }

                    DataTable dt = new DataTable();
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    ad.Fill(dt);
                    con.Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                    //new Util(_configuration).SaveExceptionLog(ex.Message + ex.StackTrace, nameof(MannualDbContext), "Database");
                    throw ex;
                }
            }
        }
    }
}
