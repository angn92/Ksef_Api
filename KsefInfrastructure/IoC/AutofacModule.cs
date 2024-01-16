using Autofac;
using KsefClient.ClientHttp;
using KsefInfrastructure.Command;
using KsefInfrastructure.CQRS;
using KsefInfrastructure.EF;

namespace KsefInfrastructure.IoC
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(IRequestHandler<>))
                .AsImplementedInterfaces();

            builder.RegisterType<RequestDispatcher>()
                .As<IRequestDispatcher>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => x.IsAssignableTo<IRequest>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<AppDbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<KsefApiHttp>()
                .As<IAuthChallenge>()
                .InstancePerLifetimeScope();
        }
    }
}
