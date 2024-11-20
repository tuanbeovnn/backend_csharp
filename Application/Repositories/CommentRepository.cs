using Application.EF;
using Application.Repositories.Core;
using Models.Accounts;

namespace Application.Repositories
{
    public class CommentRepository(BlogDbContext context) : GenericRepository<Account>(context)
    {
    }
}