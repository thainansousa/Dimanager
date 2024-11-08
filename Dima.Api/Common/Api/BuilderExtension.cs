using Dima.Api.Data;
using Dima.Api.Handlers;
using Dima.Core;
using Dima.Core.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Common.Api;

public static class BuilderExtesion {
    public static void AddConfiguration(this WebApplicationBuilder builder) {

        DefaultConfigurations.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

        DefaultConfigurations.BackendURL = builder.Configuration
        .GetValue<string>("BackendURL") ?? string.Empty;

        DefaultConfigurations.BackendURL = builder.Configuration
        .GetValue<string>("FrontendURL") ?? string.Empty;
    }

    public static void AddDocumentation(this WebApplicationBuilder builder){
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x => x.CustomSchemaIds(n => n.FullName));
    }

    public static void AddSecurity(this WebApplicationBuilder builder) {
        builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();
        builder.Services.AddAuthorization();
    }

    public static void AddDataContexts(this WebApplicationBuilder builder){
        builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(DefaultConfigurations.ConnectionString));
    }

    public static void AddCrossOrigin(this WebApplicationBuilder builder){
        builder.Services.AddCors(options => options.AddPolicy(
            DefaultApiConfigurations.CorsPolicyName,
            policy => policy
            .WithOrigins([
                DefaultConfigurations.BackendURL,
                DefaultConfigurations.FrontendURL
            ])
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
        ));
    }
    public static void AddServices(this WebApplicationBuilder builder){
        builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
        builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
    }
}