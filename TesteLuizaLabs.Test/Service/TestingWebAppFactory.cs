using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TesteLuizaLabs.Domain.Entity;
using TesteLuizaLabs.Infrastructure.Data;

namespace TesteLuizaLabs.Test.Service
{
    public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        /// <summary>
        /// Geramos aqui uma base de dados na memoria apenas para efeito de testes
        /// </summary>
        /// <param name="builder"></param>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<TesteLuizaLabsContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddMemoryCache();
                services.AddDbContext<TesteLuizaLabsContext>(options =>
                {
                    options.UseInMemoryDatabase("DBTest");
                });
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                using (var appContext = scope.ServiceProvider.GetRequiredService<TesteLuizaLabsContext>())
                {
                    try
                    {
                        appContext.Database.EnsureCreated();

                        appContext.Add(new Customer
                        {
                            Name = "teste nome1",
                            Email = "email_teste1@outlook.com"
                        });

                        appContext.Add(new Customer
                        {
                            Name = "teste nome2",
                            Email = "email_teste2@outlook.com"
                        });

                        appContext.Add(new Customer
                        {
                            Name = "teste nome3",
                            Email = "email_teste3@outlook.com"
                        });

                        appContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
                }
            });
        }
    }
}
