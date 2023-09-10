using Autofac;
using NLayer.Caching;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using NLayer.Service.Mapping;
using NLayer.Service.Services;
using System.Reflection;
using Module = Autofac.Module;
// Module = Autofac.Module
namespace NLayer.API.Modules
{
    //Repos and Services Scope Module
    public class RepoServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Generic Types Scope Build
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();

            //UnitOfWork Scope Build
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var apiAssembly  = Assembly.GetExecutingAssembly();
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();

            //builder.RegisterType<ProductServiceWithCatching>().As<IProductService>();

            //InstancePerLifeTimeScope => Scope
            //InstancePerDependency => Transient
        }
    }
}
