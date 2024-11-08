using System.Security.Claims;
using Dima.Api;
using Dima.Api.Common.Api;
using Dima.Api.Data;
using Dima.Api.Endpoints;
using Dima.Api.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();


builder.Services
.AddIdentityCore<User>()
.AddRoles<IdentityRole<long>>()
.AddEntityFrameworkStores<AppDbContext>()
.AddApiEndpoints();

var app = builder.Build();

if(app.Environment.IsDevelopment())
    app.ConfigureDevEnviroment();

app.UseCors(DefaultApiConfigurations.CorsPolicyName);
app.AddSecurity();
app.MapEndpoints();

app.Run();
