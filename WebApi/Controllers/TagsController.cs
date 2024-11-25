using Business.Services;
using Dtos.Core;
using Dtos.Responses;
using Dtos.Tags;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TagsController(TagService tag) : Controller
{
    [HttpGet("Search")]
    public Task<PagedResult<Tag>> Search([FromQuery] SearchArgs query)
    {
        return tag.SearchTag(query);
    }

    [HttpPost("Upsert")]
    public Task<Response<long>> UpsertTag([FromBody] UpsertTag args)
    {
        return tag.UpsertTag(args);
    }

    [HttpDelete("Delete")]
    public Task<Response<long>> Delete([FromBody] BaseArgs args)
    {
        return tag.Delete(args);
    }

}