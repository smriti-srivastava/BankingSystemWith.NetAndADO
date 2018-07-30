using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataAccess
{
    public class AccountDAOEntityFW : IAccountDAO
    {
        BankSystemEntities1 entity = new BankSystemEntities1();
        public int AddNewAccount(Account anAccount)
        {
           
            var bankObj = new BankAccount
            {
                AccountNumber = anAccount.AccountID,
                AccountType = Convert.ToString(anAccount.TypeOfAccount),
                AvailableBalance = anAccount.AccountBalance,
                CustomerName = anAccount.AccountHoldersDetails.Name,
                EmailID = anAccount.AccountHoldersDetails.EmailAddress,
                PhoneNumber = anAccount.AccountHoldersDetails.PhoneNumber  

            };
            entity.BankAccounts.Add(bankObj);
            return entity.SaveChanges();
            
        }

        public Account GetAccountByID(int accountId)
        {
            BankAccount bankAccount =  entity.BankAccounts.Find(accountId);
            if (bankAccount != null)
            {
                AccountHolder accountHolderDetails;
                accountHolderDetails.Name = bankAccount.CustomerName;
                accountHolderDetails.EmailAddress = bankAccount.EmailID;
                accountHolderDetails.PhoneNumber = bankAccount.PhoneNumber;
                AccountType accountType = bankAccount.AccountType == "CURRENT" ? AccountType.CURRENT : bankAccount.AccountType == "DMAT" ? AccountType.DMAT : AccountType.SAVING;
                return (new Account(bankAccount.AccountNumber, accountType , Convert.ToDouble(bankAccount.AvailableBalance), accountHolderDetails));
            }
            return null;
        }

        public int RemoveAccount(int accountId)
        {
            try
            {
                entity.BankAccounts.Attach(entity.BankAccounts.Find(accountId));
                accountId = entity.BankAccounts.Remove(entity.BankAccounts.Find(accountId)).AccountNumber;
                entity.SaveChanges();
            }
            catch(Exception e)
            {
                return 0;
            }
            return 1;
        }

        public int updateAvailableBalance(Account account)
        {
            BankAccount bankAccount = entity.BankAccounts.Find(account.AccountID);
            bankAccount.AvailableBalance = account.AccountBalance;
            return entity.SaveChanges();
        }
    }
}
