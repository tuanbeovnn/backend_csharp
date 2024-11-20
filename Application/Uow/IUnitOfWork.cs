using Application.Repositories;

namespace Application.Uow
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Chứa danh sách các repositories
        /// </summary>

        AccountRepository Accounts { get; }

        TagRepository Tags { get; }
        Task<bool> SaveChangeAsync();
        bool SaveChange();

        bool HasChanges();
    }
}