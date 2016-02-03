using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace ElaborateSQL
{
    public class TSQLAccess
    {

        private SqlConnection _con = null;
        private SqlCommand _cmd = null;
        string _constr;
        SqlTransaction _tran;


        public TSQLAccess(string constr)
        {
            _constr = constr;
        }

        public bool Open()
        {
            _con = new SqlConnection(_constr);
            _con.Open();
            _tran = _con.BeginTransaction();
            return true;
        }

        public bool Close()
        {
            _tran.Commit();
            _con.Close();
            return true;
        }

        public bool Abort()
        {
            _tran.Rollback();
            _con.Close();
            return true;
        }

        public DataSet Execute(string SQL)
        {

            _cmd = new SqlCommand(SQL, _con);
            _cmd.Transaction = _tran;
            SqlDataAdapter da = new SqlDataAdapter(_cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }

        public SqlDataReader ExecuteQuery(string SQL)
        {

            _cmd = new SqlCommand(SQL, _con);
            _cmd.Transaction = _tran;
            SqlDataReader rs = _cmd.ExecuteReader();

            return rs;
        }

        public bool ExecuteNonQuery(string SQL)
        {
            try
            {

                _cmd = new SqlCommand(SQL, _con);
                _cmd.Transaction = _tran;
                _cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
            {
                e.ToString();
                return false;
            }
        }
    }
}
