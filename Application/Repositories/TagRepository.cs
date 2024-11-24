using Application.EF;
using Application.Repositories.Core;
using Models;

namespace Application.Repositories
{
    public class TagRepository(BlogDbContext context) : GenericRepository<Tag>(context)
    {
        public IQueryable<Tag> Query => _context.Tags.AsQueryable();
    }

}