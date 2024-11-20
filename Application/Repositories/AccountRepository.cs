using Application.EF;
using Application.Repositories.Core;
using Models.Accounts;

namespace Application.Repositories
{
    public class AccountRepository(BlogDbContext context) : GenericRepository<Account>(context)
    {
        public void ChangePassword(int userId, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}