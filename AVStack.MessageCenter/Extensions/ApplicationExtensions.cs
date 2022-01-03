using System;
using System.Collections.Generic;
using System.Linq;
using AVStack.MessageCenter.Data;
using AVStack.MessageCenter.Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AVStack.MessageCenter.Extensions
{
    public static class ApplicationExtensions
    {
        private static void InitialSeed(this IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope())
            {
                if (serviceScope != null)
                {
                    // SeedTemplateGroups(serviceScope);
                }
            }
        }

        // public static void SeedTemplateGroups(IServiceScope serviceScope)
        // {
        //     var context = serviceScope.ServiceProvider.GetRequiredService<McDbContext>();
        //     context.Database.Migrate();
        //
        //     if (!context.TemplateGroup.Any())
        //     {
        //         var groups = new List<TemplateGroupEntity>
        //         {
        //             new TemplateGroupEntity()
        //             {
        //                 Id = Guid.NewGuid(),
        //                 Name = "Accounts",
        //             }
        //         }
        //     }
        // }
    }
}