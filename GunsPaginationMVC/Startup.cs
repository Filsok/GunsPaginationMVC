using GT.Sieve.Models;
using GT.Sieve.Services;
using GT.Sieve.Attributes;
using GT.Sieve.Extensions;
using GT.Sieve.Exceptions;
using GunsPaginationMVC.Data;
using GunsPaginationMVC.Models;
using GunsPaginationMVC.Sieve;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;

namespace GunsPaginationMVC;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddScoped<SieveProcessor>();
        services.AddScoped<ISieveProcessor, ApplicationSieveProcessor>();

        services.AddDbContext<GunsContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("GunContext")));
        services.AddScoped<GunSeeder>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GunSeeder seeder)
    {
        //app.MapPost("/sieve", async ([FromBody] SieveModel query, ISieveProcessor sieveProcessor, GunsContext dbContext) =>
        //{
        //    var guns = dbContext.Gun
        //    .AsQueryable();

        //    var dtos = await sieveProcessor
        //    .Apply(query, guns)
        //    .Select(g => new GunDto
        //    {
        //        Name = g.Name,
        //        Cartridge = g.Cartridge
        //    })
        //    .ToList();

        //    var totalCount = await sieveProcessor
        //    .Apply(query, guns, applyFiltering: false, applySorting: false)
        //    .Count();

        //    var result = new PagedResult<GunDto>(dtos, totalCount, query.PageSize.Value, query.Page.Value);

        //    return result;
        //});
        using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetRequiredService<GunsContext>();
            context.Database.Migrate();
        }
        seeder.Seed();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}