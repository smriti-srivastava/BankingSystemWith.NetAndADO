using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DataAccess;

namespace BusinessLogic
{
    public class AccountOperations
    {
        private IAccountDAO _accountDAO;
        //private AccountDAOEntityFW _accountDAOEntity;
        public AccountOperations(int typeOfDBOperation)
        {
            if (typeOfDBOperation == 1)
            {
                _accountDAO = new AccountDAO();
            }
            else if (typeOfDBOperation == 2)
            {
                _accountDAO = new AccountDAOEntityFW();
            }        
        }

        public bool AddAccount(Account account)
        {

            if (!SearchByAccountId(account.AccountID))
            {
                if (_accountDAO.AddNewAccount(account) >= 1)
                    return true;
                else
                    return false;
            }
            return false;
        }
        public bool RemoveAccount(int accountID)
        {

            if (_accountDAO.RemoveAccount(accountID) >= 1)
                return true;
            else
                return false;
        }

         

        public bool Withdraw(int accountID, double withdrawAmount)
        {
            Account account = _accountDAO.GetAccountByID(accountID);

            if (account != null)
            {
                if (withdrawAmount <= (account.AccountBalance - account.MinimumBalance))
                {
                    account.AccountBalance = account.AccountBalance - withdrawAmount;
                    if (_accountDAO.updateAvailableBalance(account) > 0)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public bool Deposit(int accountID, double depositAmount)
        {
            Account account = _accountDAO.GetAccountByID(accountID);
            if (account != null)
            {
                account.AccountBalance += depositAmount;
                if (_accountDAO.updateAvailableBalance(account) > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public double CalculateInterest(Account account)
        {
           
            if (account != null)
            {
                return (account.AccountBalance * account.RateOfInterest); 
            }
            return -1;
        }

        public bool SearchByAccountId(int accountId)
        {
            if (_accountDAO.GetAccountByID(accountId) != null)
                return true;
            else
                return false;
        }

        public Account getAccountDetails(int accountId)
        {
            return _accountDAO.GetAccountByID(accountId);
        }

    }
}
