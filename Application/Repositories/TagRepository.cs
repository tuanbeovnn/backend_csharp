using Application.EF;
using Application.Repositories.Core;
using Models;
using Models.Accounts;

namespace Application.Repositories
{
    public class TagRepository(BlogDbContext context) : GenericRepository<Tag>(context)
    {
    }
}