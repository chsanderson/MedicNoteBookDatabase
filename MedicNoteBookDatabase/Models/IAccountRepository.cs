//Christopher Sanderson
//MedicNoteBook
using System.Linq;
using Microsoft.AspNetCore.DataProtection;

namespace MedicNoteBookDatabase.Models
{
    public interface IAccountRepository
    {
        IQueryable<Account> Accounts { get; }

        //this creates an instance of a method that allows accounts to be created
        void SaveAccount(Account account);

        //this creates an instance of a method that allows the account to be updated to include the address and contact ids
        void SetUpAccount(int id, Account account, int ContactDetailsID, int AddressID);
        /*
        //this creates an instance of a method that allows accounts to be updated
        void UpdateAccount(int id, Account account);

        //this creates an instance of a method that allows accounts to be deleted
        void DeleteAccount(Account account);
        */

        //this creates an instance of a method that allows logins to be validated
        string validateLogin(string username, string password, string passwordAlt);
        
        //this creates an instance of a method that gets the roleID of the user
        int[] getID(string username, string password);
    }
}