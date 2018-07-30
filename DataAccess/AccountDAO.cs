using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Entities;

namespace DataAccess
{
    public class AccountDAO: IAccountDAO
    {
        private static SqlConnection _connection;
        private SqlCommand _commamd;
        private SqlDataReader _reader;
        public AccountDAO()
        {
            _connection = DatabaseConnection.GetConnectionObject();
            _connection.Open();
            _commamd = _connection.CreateCommand();
            _commamd.CommandType = CommandType.Text;
        }
        public int AddNewAccount(Account anAccount)
        {
            string query = "INSERT INTO Account VALUES( @AccountNumber, @AccountType, @AvailableBalance, @CustomerName, @PhoneNumber, @EmailID)";
            _commamd.CommandText = query;
            _commamd.Parameters.Clear();
            SqlParameter parameter = new SqlParameter("@AccountNumber", anAccount.AccountID);
            _commamd.Parameters.Add(parameter);
            parameter = new SqlParameter("@AccountType", anAccount.TypeOfAccount);
            _commamd.Parameters.Add(parameter);
            parameter = new SqlParameter("@AvailableBalance", anAccount.AccountBalance);
            _commamd.Parameters.Add(parameter);
            parameter = new SqlParameter("@CustomerName", anAccount.AccountHoldersDetails.Name);
            _commamd.Parameters.Add(parameter);
            parameter = new SqlParameter("@PhoneNumber", anAccount.AccountHoldersDetails.PhoneNumber);
            _commamd.Parameters.Add(parameter);
            parameter = new SqlParameter("@EmailID", anAccount.AccountHoldersDetails.EmailAddress);
            _commamd.Parameters.Add(parameter);
            return _commamd.ExecuteNonQuery();
        }

        public int RemoveAccount(int accountId)
        {
            string query = "DELETE FROM Account WHERE AccountNumber = @Id";
            _commamd.CommandText = query;
            SqlParameter parameter = new SqlParameter("@Id", accountId);
            _commamd.Parameters.Clear();
            _commamd.Parameters.Add(parameter);
            return _commamd.ExecuteNonQuery();
        }

        public Account GetAccountByID(int accountId)
        {
            Account account = null;
            string query = "SELECT * FROM Account WHERE AccountNumber = @Id";
            _commamd.CommandText = query;
            _commamd.Parameters.Clear();
            SqlParameter parameter = new SqlParameter("@Id", accountId);
            _commamd.Parameters.Add(parameter);
            using (_reader = _commamd.ExecuteReader())
            {
                account = new Account();
                while (_reader.Read())
                {
               
                    account.AccountID = _reader.GetInt32(_reader.GetOrdinal("AccountNumber"));   
                    account.AccountBalance = Convert.ToDouble(_reader["AvailableBalance"].ToString());
                    account.TypeOfAccount = (AccountType)(Convert.ToInt32(_reader["AccountType"].ToString()));
                    if (account.TypeOfAccount == AccountType.CURRENT)
                    {
                        account.RateOfInterest = 0.01D;
                        account.MinimumBalance = 0.0D;
                    }
                    else if (account.TypeOfAccount == AccountType.SAVING)
                    {
                        account.RateOfInterest = 0.04D;
                        account.MinimumBalance = 0.0D;
                    }
                    else if (account.TypeOfAccount == AccountType.DMAT)
                    {
                        account.RateOfInterest = 0.0D;
                        account.MinimumBalance = -10000.0D;
                    }
                    account.AccountHoldersDetails.Name = _reader["CustomerName"].ToString();
                    account.AccountHoldersDetails.PhoneNumber = _reader["PhoneNumber"].ToString();
                    account.AccountHoldersDetails.EmailAddress = _reader["EmailID"].ToString();
                }
            }
            return account;
        }

        public int updateAvailableBalance(Account account)
        {
            string query = "UPDATE Account set AvailableBalance = @AvailableBalance WHERE AccountNumber = @UpdateId";
            _commamd.CommandText = query;
            _commamd.Parameters.Clear();
            SqlParameter parameter = new SqlParameter("@AvailableBalance", account.AccountBalance);
            _commamd.Parameters.Add(parameter);
            parameter = new SqlParameter("@UpdateId", account.AccountID);
            _commamd.Parameters.Add(parameter);
            return _commamd.ExecuteNonQuery();
        }
    }
}
