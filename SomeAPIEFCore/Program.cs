using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SomeAPIEFCore.Data.Context;
using SomeAPIEFCore.Data.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddExceptionHandler<>

builder.Services.AddDbContext<RoadMapDbContext>(
    i => i.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddAutoMapper(conf => conf.AddProfile<AnyMappingProfile>());



//builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme);

builder.Services.AddCors(setup =>
{
});

//builder.Services.AddAuthorization(options =>
//{
//    // By default, all incoming requests will be authorized according to the default policy.
//    options.FallbackPolicy = options.DefaultPolicy;
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(i =>
{
    i.AllowAnyOrigin();
    i.AllowAnyMethod();
});


using(var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var db = scope.ServiceProvider.GetService<RoadMapDbContext>().Database;

    if( db.CanConnect())
    {
        //db.Migrate();
    }
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
