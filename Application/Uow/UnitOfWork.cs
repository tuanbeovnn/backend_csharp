using Application.EF;
using Application.Repositories;

namespace Application.Uow
{
    public class UnitOfWork(BlogDbContext context) : IUnitOfWork
    {
        public AccountRepository Accounts => new AccountRepository(context);
        public TagRepository Tags => new TagRepository(context);

        public async Task<bool> SaveChangeAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public bool SaveChange()
        {
            return context.SaveChanges() > 0;
        }

        public bool HasChanges()
        {
            return context.ChangeTracker.HasChanges();
        }
    }
}