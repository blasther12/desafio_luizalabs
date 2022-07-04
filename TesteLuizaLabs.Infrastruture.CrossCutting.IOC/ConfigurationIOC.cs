using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLuizaLabs.Application.Interfaces;
using TesteLuizaLabs.Application.Service;
using TesteLuizaLabs.Domain.Core.Interfaces.Repositorys;
using TesteLuizaLabs.Domain.Core.Interfaces.Services;
using TesteLuizaLabs.Domain.Services.Services;
using TesteLuizaLabs.Infrastructure.Repository.Repositorys;
using TesteLuizaLabs.Infrastruture.CrossCutting.Interfaces;
using TesteLuizaLabs.Infrastruture.CrossCutting.Map;
using TesteLuizaLabs.Infrastruture.CrossCutting.Service;

namespace TesteLuizaLabs.Infrastruture.CrossCutting.IOC
{
    public class ConfigurationIOC
    {
        /// <summary>
        /// Centraliza as interfacer para que elas sejam carregadas ao executar o projeto
        /// </summary>
        /// <param name="builder"></param>
        public static void Load(ContainerBuilder builder)
        {
            #region Registra IOC

            #region IOC Application
            builder.RegisterType<ApplicationServiceCustomer>().As<IApplicationServiceCustomer>();
            builder.RegisterType<ApplicationServiceCustomerProduct>().As<IApplicationServiceCustomerProduct>();
            builder.RegisterType<AplicationServiceProduct>().As<IApplicationServiceProduct>();
            builder.RegisterType<AplicationServiceAuth>().As<IApplicationServiceAuth>();
            #endregion

            #region IOC Services
            builder.RegisterType<ServiceCustomer>().As<IServiceCustomer>();
            builder.RegisterType<ServiceCustomerProduct>().As<IServiceCustomerProduct>();
            #endregion

            #region IOC Repositorys
            builder.RegisterType<RepositoryCustomer>().As<IRepositoryCustomer>();
            builder.RegisterType<RepositoryCustomerProduct>().As<IRepositoryCustomerProduct>();
            #endregion

            #region IOC Mapper
            builder.RegisterType<MapperCustomer>().As<IMapperCustomer>();
            builder.RegisterType<MapperCustomerProduct>().As<IMapperCustomerProduct>();
            #endregion

            #region IOC Http
            builder.RegisterType<Http>().As<IHttp>();
            #endregion

            #endregion
        }
    }
}
