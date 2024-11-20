using Application.Uow;
using Dtos.Core;
using Models;

namespace Business;

public class ServiceBase
{
    protected readonly IUnitOfWork _uow;

    public ServiceBase(IUnitOfWork uow)
    {
        _uow = uow;
    }

    protected T ToModelBase<T>(BaseArgs args) where T : ModelBase
    {
        T obj = Activator.CreateInstance<T>();
        obj.Id = args.Id;
        obj.CreatedBy = args.CurrentUser;
        obj.ModifiedBy = args.CurrentUser;
        obj.CreatedDate = DateTime.UtcNow;
        obj.ModifiedDate=DateTime.UtcNow;
        return obj;
    }
}