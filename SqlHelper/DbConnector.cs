using Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace SqlHelper
{
    public enum TransactionTipi
    {
        VerilerKomitEdilmedenOkunabilir,
        VerilerKomitEdilmedenOkunamaz
    }

    public class DbConnector
    {
        public DbConnector()
        {
            memConnectionString = Infrastructure.ConStr.DbConStr;
            this.memTrackingConnectionString = Infrastructure.ConStr.TrackingDbConStr;
        }
        public DbConnector(string parConnectionString)
        {
            memConnectionString = parConnectionString;
        }

        private TransactionTipi _transactionTipi = TransactionTipi.VerilerKomitEdilmedenOkunabilir;

        public DbConnector(bool TrackingEnabled) : this()
        {
            this.memTrackingEnabled = TrackingEnabled;
        }

        public DbConnector(TransactionTipi TransactionTipi) : this()
        {
            this._transactionTipi = TransactionTipi;
        }

        private string memConnectionString;
        public string ConnectionString
        {
            get { return memConnectionString; }
            set { memConnectionString = value; }
        }
        private SqlConnection insConnection = null;
        private SqlTransaction insTransaction = null;

        #region TRACKING members
        private readonly bool memTrackingEnabled = false;
        private string memTrackingConnectionString;
        public string TrackingConnectionString
        {
            get { return memTrackingConnectionString; }
            set { memTrackingConnectionString = value; }
        }
        private SqlConnection trConnection;
        private SqlTransaction trTransaction;
        private List<SqlCommand> trCommandsList;
        private Guid trGuid = Guid.Empty;
        private DateTime trTimestamp;
        private System.Threading.Thread trThread;
        #endregion

        public void OpenConnection()
        {
            if (insConnection == null)
            {
                insConnection = new SqlConnection();
                insConnection.ConnectionString = memConnectionString;
            }
            if (insConnection.State == ConnectionState.Closed)
            {
                try
                {
                    insConnection.Open();
                }
                catch (Exception ex)
                {
                    throw new WiseSqlException("Database Error", ex.Message);
                }

            }
        }
        public void CloseConnecion()
        {
            if (insTransaction == null)
            {
                if (insConnection != null)
                {
                    try
                    {
                        insConnection.Close();
                    }
                    catch (Exception ex)
                    {

                        throw new WiseSqlException("Database Error", ex.Message);
                    }

                    insConnection.Dispose();
                    insConnection = null;
                }
            }
        }

        public void BeginTransaction()
        {
            if (insConnection == null || insConnection.State == ConnectionState.Closed)
            {
                this.OpenConnection();
            }
            if (insTransaction == null)
            {
                try
                {
                    //if (this._transactionTipi == TransactionTipi.VerilerKomitEdilmedenOkunabilir)
                    //{
                    //    insTransaction = insConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                    //}
                    //else
                    //{
                    //    insTransaction = insConnection.BeginTransaction();
                    //}

                    insTransaction = insConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

                }
                catch (Exception ex)
                {

                    throw new WiseSqlException("Database Error", ex.Message);
                }

            }
        }
        public void CommitTransaction()
        {
            if (insTransaction != null)
            {
                try
                {
                    insTransaction.Commit();
                    this.CommitTrackingTransaction();
                }
                catch (Exception ex)
                {

                    throw new WiseSqlException("Database Error", ex.Message);
                }

                insTransaction.Dispose();
                insTransaction = null;
                this.CloseConnecion();
            }
        }
        public void RollbackTransaction()
        {
            if (insTransaction != null)
            {
                try
                {
                    insTransaction.Rollback();
                }
                catch (Exception ex)
                {

                    throw new WiseSqlException("Database Error", ex.Message);
                }

                insTransaction.Dispose();
                insTransaction = null;
                this.CloseConnecion();
            }
        }

        public SqlCommand CreateCommand(string parCommandText, DbParamCollection parDbParameters)
        {
            //DbCommand insDbCommand = insDbProviderFactory.CreateCommand();
            SqlCommand insDbCommand = new SqlCommand();
            insDbCommand.CommandText = parCommandText;
            insDbCommand.CommandType = CommandType.StoredProcedure;
            insDbCommand.CommandTimeout = 120;
            if (parDbParameters != null)
            {
                insDbCommand.Parameters.AddRange(parDbParameters.ToArray());
            }
            insDbCommand.Connection = insConnection;
            if (insTransaction != null)
            {
                insDbCommand.Transaction = insTransaction;
            }
            return insDbCommand;
        }
        public DataTable ExecuteDataTable(string parCommandText, DbParamCollection parDbParameters)
        {
            return ExecuteDataTable(parCommandText, parDbParameters, false);
        }
        public DataTable ExecuteDataTable(string parCommandText, DbParamCollection parDbParameters, bool parIsCached)
        {
            DataTable insDataTable = new DataTable();
            if (parIsCached)
            {
                if (AppDomain.CurrentDomain.GetData(parCommandText) == null)
                {
                    this.OpenConnection();
                    //DbDataAdapter insDbDataAdapter = insDbProviderFactory.CreateDataAdapter();
                    SqlDataAdapter insDbDataAdapter = new SqlDataAdapter();
                    insDbDataAdapter.SelectCommand = CreateCommand(parCommandText, parDbParameters);

                    try
                    {
                        insDbDataAdapter.Fill(insDataTable);
                    }
                    catch (Exception ex)
                    {
                        this.CloseConnecion();
                        throw new WiseSqlException("Database Execution Error", "Command : " + parCommandText + "\r\n\r\n" + ex.Message);
                    }

                    this.CloseConnecion();
                    AppDomain.CurrentDomain.SetData(parCommandText, insDataTable);
                }
                else
                {
                    insDataTable = (DataTable)(AppDomain.CurrentDomain.GetData(parCommandText));
                }
            }
            else
            {
                this.OpenConnection();
                //DbDataAdapter insDbDataAdapter = insDbProviderFactory.CreateDataAdapter();
                SqlDataAdapter insDbDataAdapter = new SqlDataAdapter();
                insDbDataAdapter.SelectCommand = CreateCommand(parCommandText, parDbParameters);
                try
                {
                    insDbDataAdapter.Fill(insDataTable);
                }
                catch (Exception ex)
                {
                    this.CloseConnecion();
                    throw new WiseSqlException("Database Execution Error", "Command : " + parCommandText + "\r\n\r\n" + ex.Message);
                }
                this.CloseConnecion();
            }

            return insDataTable;
        }

        public DataTable ExecuteSqlDataTable(string parCommandText)
        {
            this.OpenConnection();
            SqlDataAdapter insDbDataAdapter = new SqlDataAdapter(parCommandText, this.insConnection);
            DataTable insDataTable = new DataTable();
            try
            {
                insDbDataAdapter.SelectCommand.CommandTimeout = 600000;
                insDbDataAdapter.Fill(insDataTable);
            }
            catch (Exception ex)
            {
                this.CloseConnecion();
                throw new WiseSqlException("Database Execution Error", "Command : " + parCommandText + "\r\n\r\n" + ex.Message);
            }
            this.CloseConnecion();
            return insDataTable;
        }
        public int ExecuteNonQuery(string parCommandText, DbParamCollection parDbParameters)
        {
            this.OpenConnection();
            int countOfRowsAffected;
            try
            {
                countOfRowsAffected = CreateCommand(parCommandText, parDbParameters).ExecuteNonQuery();
                this.CreateTrackingCommand(parCommandText, parDbParameters);
            }
            catch (Exception ex)
            {
                this.CloseConnecion();
                throw new WiseSqlException("Database Execution Error", "Command : " + parCommandText + "\r\n\r\n" + ex.Message);
            }

            this.CloseConnecion();
            return countOfRowsAffected;
        }
        public object ExecuteScalar(string parCommandText, DbParamCollection parDbParameters)
        {
            this.OpenConnection();
            object value;
            try
            {
                value = CreateCommand(parCommandText, parDbParameters).ExecuteScalar();
            }
            catch (Exception ex)
            {
                this.CloseConnecion();
                throw new WiseSqlException("Database Execution Error", "Command : " + parCommandText + "\r\n\r\n" + ex.Message);
            }

            this.CloseConnecion();
            return value;
        }

        #region Tracking
        private void CreateTrackingCommand(string parCommandText, DbParamCollection parDbParameters)
        {
            if (!this.memTrackingEnabled)
            {
                return;
            }

            SqlParameter[] sqlpArray = new SqlParameter[parDbParameters.Count];
            parDbParameters.CopyTo(sqlpArray, 0);
            int i = 0;
            foreach (SqlParameter osqlParam in sqlpArray)
            {
                sqlpArray[i] = new SqlParameter(osqlParam.ParameterName, osqlParam.Value);
                i++;
            }
            if (this.trGuid == Guid.Empty)
            {
                this.trGuid = Guid.NewGuid();
            }
            if (this.trTimestamp == DateTime.MinValue)
            {
                this.trTimestamp = DateTime.Now;
            }

            SqlCommand trlSqlCommand = new SqlCommand();
            trlSqlCommand.Parameters.AddWithValue("@pTranGUID", this.trGuid);
            trlSqlCommand.Parameters.AddRange(sqlpArray);
            //trlSqlCommand.Parameters.AddWithValue("@pTrackedUser", User.KullaniciId);
            trlSqlCommand.Parameters.AddWithValue("@pTrackedDate", this.trTimestamp);
            if (parCommandText.StartsWith("Insert"))
            {
                trlSqlCommand.Parameters.AddWithValue("@pTrackedTranType", "INS");
            }
            if (parCommandText.StartsWith("Update"))
            {
                trlSqlCommand.Parameters.AddWithValue("@pTrackedTranType", "UPD");
            }
            if (parCommandText.StartsWith("Delete"))
            {
                trlSqlCommand.Parameters.AddWithValue("@pTrackedTranType", "DEL");
            }
            trlSqlCommand.CommandType = CommandType.StoredProcedure;
            parCommandText = "Insert" + parCommandText.Substring(6);
            if (parCommandText.Contains("By"))
            {
                parCommandText = parCommandText.Remove(parCommandText.LastIndexOf("By"));
            }

            trlSqlCommand.CommandText = parCommandText;


            if (this.insTransaction != null) // Asıl işlem bir transaction ise o commit olana kadar, gerekli tracking commandlarını depolarız,
            {
                if (this.trCommandsList == null)
                {
                    this.trCommandsList = new List<SqlCommand>();
                }
                this.trCommandsList.Add(trlSqlCommand);
            }
            else // Asıl işlem bir transaction değil ise biz de işimizi hemen gerçekleştiririz.
            {
                SqlConnection trlConnection = new SqlConnection(this.memTrackingConnectionString);
                trlSqlCommand.Connection = trlConnection;
                try
                {
                    trlConnection.Open();
                    trlSqlCommand.ExecuteNonQuery();
                    trlConnection.Close();
                }
                catch (SqlException sqlE)
                {
                    this.RunTrackingSqlErrorProcedure(sqlE);
                }
                catch (Exception e)
                {
                    this.RunTrackingErrorProcedure(e);
                }
                finally
                {
                    trlConnection.Dispose();
                }
            }
        }
        private void CommitTrackingTransaction()
        {
            if (!this.memTrackingEnabled)
            {
                return;
            }
            trThread = new System.Threading.Thread(new System.Threading.ThreadStart(this.InitiateTrackingTransaction));
            trThread.Start();
        }
        private void InitiateTrackingTransaction()
        {
            this.trConnection = new SqlConnection(this.memTrackingConnectionString);
            try
            {
                this.trConnection.Open();
                this.trTransaction = trConnection.BeginTransaction();
                foreach (SqlCommand sqlCmd in this.trCommandsList)
                {
                    sqlCmd.Connection = this.trConnection;
                    sqlCmd.Transaction = this.trTransaction;
                    sqlCmd.ExecuteNonQuery();
                }
                if (this.trTransaction.Connection != null) this.trTransaction.Commit();
                this.trConnection.Close();
            }
            catch (SqlException sqlE)
            {
                if (this.trTransaction != null)
                {
                    this.trTransaction.Rollback();
                }
                this.RunTrackingSqlErrorProcedure(sqlE);
            }
            catch (Exception e)
            {
                if (this.trTransaction != null)
                {
                    this.trTransaction.Rollback();
                }
                this.RunTrackingErrorProcedure(e);
            }
            finally
            {
                this.trConnection.Dispose();
            }
        }
        private void RunTrackingSqlErrorProcedure(SqlException sqlE)
        {
            // TODO: Do st
        }
        private void RunTrackingErrorProcedure(Exception e)
        {
            // TODO: Do st
        }
        #endregion

    }
    public class DbParamCollection : CollectionBase, ICollection<SqlParameter>
    {
        #region ICollection<SqlParameter> Members
        private int _OutputParameterIndex;
        public void Add(SqlParameter item)
        {
            InnerList.Add(item);
        }
        public void Add(string parameterName, object parameterValue)
        {
            SqlParameter insSqlparameter = new SqlParameter();
            insSqlparameter.ParameterName = parameterName;
            if (parameterValue == null)
            {
                parameterValue = System.DBNull.Value;
            }
            insSqlparameter.Value = parameterValue;
            InnerList.Add(insSqlparameter);
        }

        public void AddOutput(string parameterName, DbType parDbType)
        {
            SqlParameter insSqlparameter = new SqlParameter();
            insSqlparameter.ParameterName = parameterName;
            insSqlparameter.DbType = parDbType;
            insSqlparameter.Direction = ParameterDirection.Output;
            _OutputParameterIndex = InnerList.Add(insSqlparameter);
        }
        public SqlParameter GetOutPutParameter()
        {
            return (SqlParameter)InnerList[_OutputParameterIndex];
        }

        public bool Contains(SqlParameter item)
        {
            return InnerList.Contains(item);
        }

        public void CopyTo(SqlParameter[] array, int arrayIndex)
        {
            InnerList.CopyTo(array, arrayIndex);
        }

        public bool IsReadOnly
        {
            get
            {
                return InnerList.IsReadOnly;
            }
        }

        public bool Remove(SqlParameter item)
        {
            InnerList.Remove(item);
            return true;
        }
        public Array ToArray()
        {
            return InnerList.ToArray();
        }

        #endregion

        #region IEnumerable<DbParameter> Members

        public new IEnumerator<SqlParameter> GetEnumerator()
        {
            return (IEnumerator<SqlParameter>)InnerList.GetEnumerator();
        }

        #endregion
    }
}
