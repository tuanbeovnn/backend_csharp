using Business.Extensions;
using Business.Filters;
using Dtos.Core;
using Microsoft.AspNetCore.Mvc;
using Models.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.AddSerilog();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AutoAddUserFilter(typeof(BaseArgs)));
    options.CacheProfiles.Add(CacheProfiles.Test, new CacheProfile() { Duration = 5, });
    options.CacheProfiles.Add(CacheProfiles.Default1m, new CacheProfile() { Duration = 60, });
    options.CacheProfiles.Add(CacheProfiles.Default1Month, new CacheProfile() { Duration = 2628000, });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddJwtAuthentication(builder.Configuration)
    .AddCaching()
    .AddConfigureOptions()
    .AddMapperAndValidation()
    .AddBlogDbContext(builder.Configuration)
    .AddServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();