using Application.Extensions;
using Application.Repositories.Core;
using Application.Uow;
using Business.Validation;
using Dtos.Core;
using Dtos.Responses;
using Dtos.Tags;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Business.Services;

public class TagService(IUnitOfWork uow, IValidationFactory validation) : ServiceBase
{
    public async Task<PagedResult<Tag>> SearchTag(SearchArgs args)
    {
        var query = uow.Tags.Query;
        if (args.OwnerId > 0)
        {
            query = query.Where(f => f.OwnerId == args.OwnerId);
        }

        long total = await query.CountAsync();
        query.PagingQuery(args.Page, args.Size);
        var items = await query.ToListAsync();
        return new PagedResult<Tag>()
        {
            Page = args.Page,
            PageSize = args.Size,
            Total = total,
            Items = items
        };
    }

    public async Task<Response<long>> UpsertTag(UpsertTag args)
    {
        ArgumentNullException.ThrowIfNull(args);
        var validate = await validation.ValidateAsync(args);
        if (!validate.Success) return new ErrorResponse<long>(validate.Message);
        Tag tag = ToModelBase<Tag>(args);
        uow.Tags.Insert(tag);
        bool success = await uow.SaveChangeAsync();
        return success
            ? new SuccessResponse<long>() { Data = tag.Id }
            : new ErrorResponse<long>();
    }

    public async Task<Response<long>> Delete(BaseArgs args)
    {
        var tag = uow.Tags.GetById(args.Id);
        if (tag == null) return new ErrorResponse<long>();
        uow.Tags.Update(tag, f => f.DataStatus, DataStatus.Deleted);
        uow.Tags.Entry(tag).SetProperty(f => f.DataStatus, DataStatus.Published);
        //_uow.Tags.SoftDelete(tag);
        bool success = await uow.SaveChangeAsync();
        return success
            ? new SuccessResponse<long>() { Data = tag.Id }
            : new ErrorResponse<long>();
    }
}