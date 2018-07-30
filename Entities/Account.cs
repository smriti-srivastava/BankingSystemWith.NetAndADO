using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public enum AccountType
    {
        CURRENT,
        SAVING,
        DMAT
    }

    public struct AccountHolder
    {
        public string Name;
        public string PhoneNumber;
        public string EmailAddress;
    }

    public class Account
    {
        public int AccountID;
        public double AccountBalance;
        public double RateOfInterest;
        public double MinimumBalance;
        public AccountType TypeOfAccount;
        public AccountHolder AccountHoldersDetails;

        public Account() { }
        public Account(int AccountID, AccountType TypeOfAccount, double Balance, AccountHolder AccountHoldersDetails)
        {
            this.AccountID = AccountID;
            this.TypeOfAccount = TypeOfAccount;
            this.AccountBalance = Balance;
            this.AccountHoldersDetails = AccountHoldersDetails;

            if (this.TypeOfAccount == AccountType.CURRENT)
            {
                this.RateOfInterest = 0.01D;
                this.MinimumBalance = 0.0D;
            }
            else if (this.TypeOfAccount == AccountType.SAVING)
            {
                this.RateOfInterest = 0.04D;
                this.MinimumBalance = 1000.0D;
            }
            else if (this.TypeOfAccount == AccountType.DMAT)
            {
                this.RateOfInterest = 0.0D;
                this.MinimumBalance = -10000.0D;
            }
        }

    }
}
