using System.Linq.Expressions;
using Application.Repositories.Core;
using Application.Uow;
using Dtos.Core;
using Dtos.Responses;
using Dtos.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Models;

namespace Business;

public class TagService : ServiceBase
{
    public TagService(IUnitOfWork uow) : base(uow)
    {
    }

    public async Task<PagedResult<Tag>> SearchTag(SearchArgs args)
    {
        return new PagedResult<Tag>();
    }

    public async Task<Response<long>> UpsertTag(UpsertTag args)
    {
        Tag tag = ToModelBase<Tag>(args);
        _uow.Tags.Insert(tag);
        bool success = await _uow.SaveChangeAsync();
        return success
            ? new SuccessResponse<long>() { Data = tag.Id }
            : new ErrorResponse<long>();
    }

    public async Task<Response<long>> Delete(BaseArgs args)
    {
        var tag = _uow.Tags.GetById(args.Id);
        if (tag != null)
        {
            _uow.Tags.Update(tag, f => f.DataStatus, DataStatus.Deleted);
            _uow.Tags.Entry(tag).SetProperty(f => f.DataStatus, DataStatus.Published);
            //_uow.Tags.SoftDelete(tag);
            bool success = await _uow.SaveChangeAsync();
            return success
                ? new SuccessResponse<long>() { Data = tag.Id }
                : new ErrorResponse<long>();
        }

        return new ErrorResponse<long>();
    }
}