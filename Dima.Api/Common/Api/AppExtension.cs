using System.Security.Claims;
using Dima.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Dima.Api.Common.Api;

public static class AppExtesion{
    public static void ConfigureDevEnviroment(this WebApplication app){
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapSwagger().RequireAuthorization();
    }

    public static void AddSecurity(this WebApplication app){

        app.UseAuthentication();
        app.UseAuthorization();
        
    }
}