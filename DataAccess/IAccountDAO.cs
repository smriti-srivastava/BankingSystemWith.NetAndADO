using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataAccess
{
    public interface IAccountDAO
    {
        int AddNewAccount(Account anAccount);

        int RemoveAccount(int accountId);


        Account GetAccountByID(int accountId);


        int updateAvailableBalance(Account account);
       
    }
}
