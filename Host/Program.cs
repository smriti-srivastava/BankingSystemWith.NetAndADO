using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using Entities;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string typeOfDBOperationString = ConfigurationManager.AppSettings["DBOptype"].ToString();
                int  typeOfOperation = Convert.ToInt32(typeOfDBOperationString);                 //currently set to 2 in appconfig file. change it to 1 to access ado.net file
                


                AccountOperations accountOperations = new AccountOperations(typeOfOperation);
                
                Console.WriteLine("--------Select User Type---------");
                Console.WriteLine("1. Customer");
                Console.WriteLine("2. Admin");
                int userType = Convert.ToInt32(Console.ReadLine());
                switch (userType)
                {
                    case 1:
                        {
                            while (true)
                            {
                                Console.WriteLine("Enter Account Number : ");
                                int accountNumber = Convert.ToInt32(Console.ReadLine());
                                if (accountOperations.SearchByAccountId(accountNumber))
                                {
                                    Account account = accountOperations.getAccountDetails(accountNumber);
                                    Console.WriteLine("\n\n------MENU------");
                                    Console.WriteLine("1. Display Account Details");
                                    Console.WriteLine("2. Deposit");
                                    Console.WriteLine("3. Withdraw");
                                    if (account.TypeOfAccount != AccountType.DMAT)
                                    {
                                        Console.WriteLine("4. Calculate Interest");
                                    }
                                    Console.WriteLine("\n\nEnter Your Choice : ");
                                    int choice = Convert.ToInt32(Console.ReadLine());
                                    switch (choice)
                                    {
                                        case 1:
                                            {
                                                Console.WriteLine("\n\n--------------Account Details---------------");
                                                Console.WriteLine("\n\nAccount Number : {0}", account.AccountID);
                                                Console.WriteLine("Account Type : {0}", (account.TypeOfAccount == AccountType.CURRENT) ? "CURRENT" : (account.TypeOfAccount == AccountType.SAVING) ? "SAVING" : "DMAT");
                                                Console.WriteLine("Name : {0}", account.AccountHoldersDetails.Name);
                                                Console.WriteLine("Balance : {0} : ", account.AccountBalance);
                                                Console.WriteLine("Email ID : {0}", account.AccountHoldersDetails.EmailAddress);
                                                Console.WriteLine("Phone Number : {0}", account.AccountHoldersDetails.PhoneNumber);
                                                break;
                                            }
                                        case 2:
                                            {
                                                Console.WriteLine("\n\nEnter The Deposit Amount : ");
                                                double depositAmount = Convert.ToInt32(Console.ReadLine());
                                                if (accountOperations.Deposit(account.AccountID, depositAmount))
                                                    Console.WriteLine("Transaction Successful!");
                                                else
                                                    Console.WriteLine("Something Went Wrong, Transaction Unsuccessful");
                                                break;
                                            }
                                        case 3:
                                            {
                                                Console.WriteLine("\n\nEnter Withdraw Amount : ");
                                                double withdrawAmount = Convert.ToInt32(Console.ReadLine());
                                                if (accountOperations.Withdraw(account.AccountID, withdrawAmount))
                                                    Console.WriteLine("Transaction Successful!");
                                                else
                                                    Console.WriteLine("Something Went Wrong, Transaction Unsuccessful");
                                                break;
                                            }
                                        case 4:
                                            {
                                                if (account.TypeOfAccount != AccountType.DMAT)
                                                {
                                                    Console.WriteLine("\n\nInterest : {0} ", accountOperations.CalculateInterest(account));
                                                }
                                                break;
                                            }
                                        default:
                                            Console.WriteLine("Inavlid Choice Entered");
                                            break;
                                    }



                                    Console.WriteLine("\n\nEnter 'Y' If You Want To Exit or Press Any Other Key To Continue : ");
                                    string ch = Console.ReadLine();
                                    if (ch == "Y" || ch == "y")
                                        Environment.Exit(0);
                                }

                                else
                                {
                                    Console.WriteLine("\n\nInavlid Account Number, Try Again!");
                                }
                            }

                        }
                    case 2:
                        {
                            while (true)
                            {
                                Console.WriteLine("\n\n---------------MENU-----------------");
                                Console.WriteLine("1. Add Account");
                                Console.WriteLine("2. Remove Account");
                                Console.WriteLine("3. Exit");
                                int choice = Convert.ToInt32(Console.ReadLine());
                                switch (choice)
                                {
                                    case 1:
                                        {
                                            AccountHolder userDetails;
                                            Console.WriteLine("Enter The Details for New Account");
                                            Console.WriteLine("Account ID : ");
                                            int accountID = Convert.ToInt32(Console.ReadLine());
                                            if (!accountOperations.SearchByAccountId(accountID))
                                            {
                                                Console.WriteLine("Account Balance : ");
                                                double accountBalance = Convert.ToDouble(Console.ReadLine());
                                                Console.WriteLine("Account Holder's Name : ");
                                                userDetails.Name = Console.ReadLine();
                                                Console.WriteLine("Associated Phone Number : ");
                                                userDetails.PhoneNumber = Console.ReadLine();
                                                Console.WriteLine("Associated Email Address : ");
                                                userDetails.EmailAddress = Console.ReadLine();
                                                Console.WriteLine("Account Type : ");
                                                Console.WriteLine("Enter 1 For CURRENT : ");
                                                Console.WriteLine("Enter 2 For SAVING : ");
                                                Console.WriteLine("Enter 3 for DMAT");
                                                int accountTypeIndex = Convert.ToInt32(Console.ReadLine()) - 1;
                                                Account account = new Account(accountID, (AccountType)accountTypeIndex, accountBalance, userDetails);

                                                if (accountOperations.AddAccount(account))
                                                    Console.WriteLine("\n\nAccount Added Successfully");
                                                else
                                                    Console.WriteLine("\n\nSomething Went Wrong Try Again.");
                                                
                                            }
                                            else
                                            {
                                                Console.WriteLine("\n\nENtered Account Address Already Exists!");
                                            }
                                            break;
                                        }
                                    case 2:
                                        {
                                            Console.WriteLine("Enter The Account Number To Be Deleted : ");
                                            int accountId = Convert.ToInt32(Console.ReadLine());
                                            if (accountOperations.SearchByAccountId(accountId))
                                            {
                                                if (accountOperations.RemoveAccount(accountId))
                                                {
                                                    Console.WriteLine("\n\nAccount Deleted Successfully.");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("\n\nSomething Went Wrong!");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Enter Account Number Not Found.");
                                            }
                                            break;
                                        }
                                    case 3:
                                        Environment.Exit(0);
                                        break;
                                    default:
                                        Console.WriteLine("\n\nInavlid Choice!");
                                        break;
                                }
                            }
                        }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
