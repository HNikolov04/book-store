using BookStore.Api.Extensions;
using BookStore.Application;
using BookStore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin() 
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApiServices();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins"); 
app.UseEssentialMiddlewares();

app.Run();