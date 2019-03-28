using System.Linq;
using Microsoft.AspNetCore.DataProtection;

namespace MedicNoteBookDatabase.Models
{
    public interface IAccountRepository
    {
        IQueryable<Account> Accounts { get; }

        ///*void*/int SaveAccount(Account account);
        void SaveAccount(Account account);

        void SetUpAccount(int id, Account account, int ContactDetailsID, int AddressID);

        void UpdateAccount(int id, Account account);

        void DeleteAccount(Account account);
        /*IDataProtector protector, IDataProtectionService protectionService, */
        /*bool*/ string validateLogin(string username, string password, string passwordAlt);

        /*int[]*/ /*void*/ int[] getID(string username, string password);

        //int getID(Account account);bool validateLogin(Login login);
    }
}